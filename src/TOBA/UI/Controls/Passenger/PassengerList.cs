using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TOBA.UI.Controls.Passenger
{
	using Data;

	using Entity;

	using Entity.Web;

	internal partial class PassengerList : FlowLayoutPanel, IOperation, IDisposable
	{
		public PassengerList(Session session)
		{
			InitializeComponent();

			if (session != null)
			{
				Session = session;
				LoadSessionPassengers();
			}
		}

		private bool? _studentFileter;
		Label _emptyLabel = new Label() { Text = "无可以添加的联系人\n学生票查询时非学生乘客不会显示", AutoSize = false, Size = new Size(200, 40) };
		Label _loadingLabel = new Label { Text = "正在加载中...", AutoSize = false, Size = new Size(60, 20) };

		void LoadSessionPassengers()
		{
			//ensure handle
			var p = Handle;

			Session.PassengerLoadComplete += Session_PassengerLoadComplete;
			if (!Session.UserProfile.IsPassengerLoaded)
			{
				this.Controls.Add(_loadingLabel);
				return;
			}
			Controls.Clear();

			SuspendLayout();
			Controls.AddRange(Session.UserProfile.Passengers.Where(s => s.CanAddIntoOrder).Select(s => (Control)CreateLabel(s)).ToArray());
			Controls.Add(_emptyLabel);
			ResumeLayout();

			_emptyLabel.Visible = (Filter(null) == 0);

			//捕捉事件
			Session.UserProfile.Passengers.Added += Passengers_Added;
			Session.UserProfile.Passengers.Removed += Passengers_Removed;
		}

		void Passengers_Removed(object sender, ItemEventArgs<Passenger> e)
		{
			SuspendLayout();

			var lbl = Controls.OfType<PassengerLabel>().Where(s => s.Passenger == e.Item).FirstOrDefault();
			if (lbl != null)
				Controls.Remove(lbl);

			ResumeLayout();
		}

		void Passengers_Added(object sender, ItemEventArgs<Passenger> e)
		{
			if (e.Item.Verification.Verified == true)
			{
				SuspendLayout();
				Controls.Add(CreateLabel(e.Item));
				ResumeLayout();
			}
		}

		Label CreateLabel(Entity.Web.Passenger s)
		{
			var lbl = new PassengerLabel(s);
			tip.SetToolTip(lbl, String.Format("姓名：{0}\r\n证件：{1}\r\n类型：{2}", s.Name, s.IdNo, ParamData.PassengerType[s.Type]));
			lbl.Enabled = !CheckIsInAvailableFilter(s);
			lbl.Click += LabelClick;
			return lbl;
		}

		void LabelClick(object sender, EventArgs e)
		{
			var lbl = sender as PassengerLabel;
			OnRequestSelectPassenger(new RequestSelectPassengerEventArgs(lbl.Passenger));
			lbl.Enabled = !CheckIsInAvailableFilter(lbl.Passenger);
		}

		void Session_PassengerLoadComplete(object sender, EventArgs e)
		{
			if (!IsHandleCreated)
				return;

			BeginInvoke(new Action(LoadSessionPassengers));
		}

		/// <summary> 
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}

			Session.PassengerLoadComplete -= Session_PassengerLoadComplete;
			if (Session.UserProfile.Passengers != null)
			{
				Session.UserProfile.Passengers.Added -= Passengers_Added;
				Session.UserProfile.Passengers.Removed -= Passengers_Removed;
			}

			base.Dispose(disposing);
			GC.SuppressFinalize(this);
		}

		#region Implementation of IOperation

		/// <summary>
		/// 获得或设置上下文环境
		/// </summary>
		public Session Session { get; set; }

		/// <summary>
		/// 获得或设置学生过滤
		/// </summary>
		public bool? StudentFileter
		{
			get { return _studentFileter; }
			set
			{
				if (_studentFileter == value) return;
				_studentFileter = value;

				SuspendLayout();
				//重设已有的联系人
				_emptyLabel.Visible = (Filter(null) == 0);
				ResumeLayout();
			}
		}

		#endregion

		/// <summary>
		/// 按指定的关键字过滤，并返回符合要求的数量
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public int Filter(string key)
		{
			SuspendLayout();
			var count = Controls.OfType<PassengerLabel>().Count(s => s.Filter(key, StudentFileter));
			ResumeLayout();

			return count;
		}

		/// <summary>
		/// 请求选择联系人
		/// </summary>
		public event EventHandler<RequestSelectPassengerEventArgs> RequestSelectPassenger;

		/// <summary>
		/// 引发 <see cref="RequestSelectPassenger" /> 事件
		/// </summary>
		/// <param name="ea">包含此事件的参数</param>
		protected virtual void OnRequestSelectPassenger(RequestSelectPassengerEventArgs ea)
		{
			var handler = RequestSelectPassenger;
			if (handler != null)
				handler(this, ea);
		}

		/// <summary>
		/// 过滤学生用户
		/// </summary>
		/// <param name="showStudent"></param>
		public void FilterStudent(bool showStudent)
		{
			StudentFileter = showStudent;
		}

		/// <summary>
		/// 获得或设置可用性过滤
		/// </summary>
		public List<Entity.Web.Passenger> AvailableFilter { get; set; }

		/// <summary>
		/// 检测是否位于过滤列表中
		/// </summary>
		/// <returns></returns>
		bool CheckIsInAvailableFilter(Entity.Web.Passenger p)
		{
			if (AvailableFilter == null) return false;

			return AvailableFilter.Any(s => s.Name.IsIgnoreCaseEqualTo(p.Name) && s.IdNo.IsIgnoreCaseEqualTo(p.IdNo));
		}


		/// <summary>
		/// 刷新可用性
		/// </summary>
		public void RefreshAvailable()
		{
			SuspendLayout();
			Controls.OfType<PassengerLabel>().ForEach(s =>
			{
				s.Enabled = !CheckIsInAvailableFilter(s.Passenger);
			});
			ResumeLayout();
		}
	}
}
