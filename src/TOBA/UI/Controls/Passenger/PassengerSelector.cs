using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace TOBA.UI.Controls.Passenger
{
	using Data;

	using Entity;
	using Entity.Web;

	using Popup;

	using System.Diagnostics;

	internal partial class PassengerSelector : ControlBase
	{

		ListViewItem _lastItem;

		string _searchKey;

		bool _showOnlyStudent;

		public PassengerSelector()
			: this(null)
		{

		}

		public PassengerSelector(Session session)
		{
			InitializeComponent();

			//ICON
			ilList.Images.Add("1", Properties.Resources.id_ic);
			ilList.Images.Add("2", Properties.Resources.id_ic);
			ilList.Images.Add("G", Properties.Resources.id_it);
			ilList.Images.Add("C", Properties.Resources.id_ig);
			ilList.Images.Add("B", Properties.Resources.id_ip);

			InitSession(session);
			lnkReload.Click += (s, e) =>
			{
				Session?.AccquireLoadPassengers();
			};
			txtSearch.KeyUp += (x, y) =>
			{
				SearchKey = txtSearch.Text;
			};

			lnkAdd.Click += (s, x) => OnRequestAddPassenger();
			lstPas.MouseDoubleClick += (x, y) =>
			{
				var item = lstPas.GetItemAt(y.X, y.Y);
				if (item == null)
					return;

				OnRequestSelectPassenger(new RequestSelectPassengerEventArgs(item.Tag as Passenger));
			};
			lstPas.MouseMove += LstPas_MouseMove;
			ParentChanged += (s, e) =>
			{
				if (Parent is Popup)
				{
					(Parent as Popup).Closed += (x, y) => HideToolTips();
				}
			};
			MouseLeave += (s, e) => HideToolTips();
			Disposed += (s, e) =>
						{
							Session.PassengerLoadComplete -= Session_PassengerLoadComplete;
						};

			pEmpty.KeepCenter(lstPas);
		}

		/// <summary>
		/// 请求添加联系人
		/// </summary>
		public event EventHandler RequestAddPassenger;


		/// <summary>
		/// 请求选择联系人
		/// </summary>
		public event EventHandler<RequestSelectPassengerEventArgs> RequestSelectPassenger;

		ListViewItem CreatePassengerItem(Passenger passenger)
		{
			if (passenger == null)
				return null;

			var item = new ListViewItem()
			{
				Text = passenger.Name,
				Tag = passenger,
				ToolTipText = $"姓名：{passenger.Name}\r\n证件：{passenger.IdNo}\r\n类型：{ParamData.PassengerType[passenger.Type]}",
				ImageKey = passenger.IdTypeCode.ToString()
			};

			return item;
		}


		void LoadPassengers()
		{
			Session.PassengerLoadComplete -= Session_PassengerLoadComplete;
			Session.PassengerLoadComplete += Session_PassengerLoadComplete;
			if (Session.UserProfile.Passengers == null)
			{
				pLoading.Visible = true;

				return;
			}

			pLoading.Visible = false;

			var passengers = Session.UserProfile.Passengers;
			passengers.Added += Passengers_Added;
			passengers.Removed += Passengers_Removed;

			RefreshPassengerList();
		}

		private void Session_PassengerLoadComplete(object sender, EventArgs x)
		{
			AppContext.HostForm.Invoke(LoadPassengers);
		}

		private void LstPas_MouseMove(object sender, MouseEventArgs e)
		{
			var item = lstPas.GetItemAt(e.X, e.Y);
			if (_lastItem == item)
				return;
			_lastItem = item;
			if (item == null)
			{
				Debug.WriteLine("hide passenger tooltip.");
				tt.Hide(this);
				return;
			}
			//var bounds = item.Bounds;
			Debug.WriteLine("showing passenger tooltip.");

			tt.Show(item.ToolTipText, this, e.X + 20, e.Y + 30);
		}

		private void Passengers_Added(object sender, ItemEventArgs<Passenger> e)
		{
			if (IsFiltered(e.Item) || !e.Item.IsMatch(ShowOnlyStudent, SearchKey))
				return;

			lstPas.Items.Add(CreatePassengerItem(e.Item));
			pEmpty.Visible = false;
		}

		private void Passengers_Removed(object sender, ItemEventArgs<Passenger> e)
		{
			lstPas.Items.Cast<ListViewItem>().FirstOrDefault(s => s.Tag == e.Item)?.Remove();
			pEmpty.Visible = lstPas.Items.Count == 0;
		}

		public void RefreshPassengerList()
		{
			var currentList = Session.UserProfile.Passengers;
			lstPas.BeginUpdate();
			lstPas.Items.Clear();
			if (currentList != null)
				lstPas.Items.AddRange(currentList.Where(s => !IsFiltered(s) && s.IsMatch(ShowOnlyStudent, SearchKey)).Select(CreatePassengerItem).ToArray());
			lstPas.EndUpdate();
			pEmpty.Visible = lstPas.Items.Count == 0;
		}

		/// <summary>
		/// 过滤器
		/// </summary>
		public List<Func<Passenger, bool>> FilterExprList { get; } = new List<Func<Passenger, bool>>();

		bool IsFiltered(Passenger pass)
		{
			if (FilterExprList.Any(s => s(pass)))
				return true;

			return AvailableFilter?.Any(s => s.IdType == pass.IdTypeCode && s.IdNo == pass.IdNo && s.Name == pass.Name) != false;
		}

		/// <summary>
		/// 引发 <see cref="RequestAddPassenger" /> 事件
		/// </summary>
		protected virtual void OnRequestAddPassenger()
		{
			var handler = RequestAddPassenger;
			handler?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		/// 引发 <see cref="RequestSelectPassenger" /> 事件
		/// </summary>
		/// <param name="ea">包含此事件的参数</param>
		protected virtual void OnRequestSelectPassenger(RequestSelectPassengerEventArgs ea)
		{
			var handler = RequestSelectPassenger;
			handler?.Invoke(this, ea);
		}

		protected override void OnVisibleChanged(EventArgs e)
		{
			base.OnVisibleChanged(e);

			if (Visible && Session != null)
			{
				Trace.TraceInformation("已刷新联系人列表项");
				RefreshPassengerList();
			}
			else
			{
				Trace.TraceInformation("已清理联系人列表项");
				lstPas.Items.Clear();
			}
		}

		public void HideToolTips()
		{
			tt.Hide(this);
			_lastItem = null;
		}

		private Session _session;

		/// <summary>
		/// 执行初始化
		/// </summary>
		/// <param name="session"></param>
		public override void InitSession(Session session)
		{
			base.InitSession(session);

			if (session != null && _session != session)
			{
				_session = session;
				LoadPassengers();
			}
		}

		private List<TOBA.Entity.PassengerInTicket> _availableFilter;

		/// <summary>
		/// 获得或设置可用性过滤
		/// </summary>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public List<TOBA.Entity.PassengerInTicket> AvailableFilter
		{
			get { return _availableFilter ??= new List<TOBA.Entity.PassengerInTicket>(5); }
			set => _availableFilter = value;
		}

		/// <summary>
		/// 获得或设置搜索关键字
		/// </summary>
		public string SearchKey
		{
			get => _searchKey;
			set
			{
				if (string.Compare(value, _searchKey, StringComparison.OrdinalIgnoreCase) == 0)
					return;
				_searchKey = value;

				RefreshPassengerList();
			}
		}

		/// <summary>
		/// 获得或设置是否允许显示增加
		/// </summary>
		public bool ShowAddLink
		{
			get => lnkAdd.Visible;
			set => lnkAdd.Visible = value;
		}

		/// <summary>
		/// 获得或设置是否仅显示学生
		/// </summary>
		public bool ShowOnlyStudent
		{
			get => _showOnlyStudent;
			set
			{
				if (_showOnlyStudent == value)
					return;
				_showOnlyStudent = value;
				RefreshPassengerList();
			}
		}

	}
}
