using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using TOBA.UI.Dialogs.Passenger;

namespace TOBA.UI.Controls.Passenger
{
	using Data;

	using Dialogs.Common;

	using Entity;

	using Otn.Workers;

	using TOBA.Configuration;

	using Passenger = Entity.Web.Passenger;

	internal partial class PassengerPanel : ControlBase
	{

		string _filterKey = "";
		bool _inSystemMaintenance;
		PassengerManager _manager = new PassengerManager();
		Dictionary<Entity.Web.Passenger, ListViewItem> _passes = new Dictionary<Entity.Web.Passenger, ListViewItem>();

		readonly SaveFileDialog _saveFileDialog = new SaveFileDialog
		{
			Title = "保存联系人到...",
			Filter = "联系人列表文件(*.lst)|*.lst",
			FileName = "联系人列表.lst"
		};

		public PassengerPanel()
		{
			InitializeComponent();

			if (!Program.IsRunning)
				return;

			Load += PassengerPanel_Load;
			tsFilter.KeyUp += tsFilter_KeyUp;
			tsDelete.Click += tsDelete_Click;
			tsAdd.Click += tsAdd_Click;
			tsEdit.Click += tsEdit_Click;
			list.DoubleClick += tsEdit_Click;
			tsImportToClipboard.Click += (s, e) => CopyToClipboard();
			tsImportToFile.Click += (s, e) => ExportToFile();
			list.KeyUp += list_KeyUp;
			tsImportDlg.Click += (s, e) =>
			{
				GetControl<ImportPassenger>().ShowDialog();
				Session.LoadPassengers();
			};

			//图标
			imgList.Images.Add(UiUtility.Get24PxImageFrom16PxImg(Properties.Resources.user_16));
			imgList.Images.Add(UiUtility.Get24PxImageFrom16PxImg(Properties.Resources.male));
			imgList.Images.Add(UiUtility.Get24PxImageFrom16PxImg(Properties.Resources.female));

			//维护
			_inSystemMaintenance = ParamData.IsSystemMaintenance;
		}

		ListViewItem CreateItem(Entity.Web.Passenger p)
		{
			var lvi = new ListViewItem(p.Name)
			{
				ImageIndex = 0,
				UseItemStyleForSubItems = true
			};
			if (p.IdTypeCode == '0' || p.IdTypeCode == '1')
			{
				var isFemale = (p.IdNo[p.IdNo.Length - 2] - '0') % 2 == 0;
				lvi.ImageIndex = isFemale ? 2 : 1;
			}

			lvi.SubItems.Add(ParamData.PassengerType[p.Type]);
			lvi.SubItems.Add(ParamData.PassengerIdType.GetValue(p.IdTypeCode).DefaultForEmpty("无效证件类型"));
			lvi.SubItems.Add(p.IdNo);
			lvi.SubItems.Add(p.MobileNo);

			//删除信息
			var deleteTime = p.DeleteTime;
			var deleteMsg = deleteTime == null ? "<请刷新列表>" : deleteTime.Value < DateTime.Today ? "可以删除" : $"{deleteTime.Value.ToString("yyyy年MM月dd日")}可删";

			//审核状态
			var status = p.Verification;
			if (status.Verified == null)
			{
				lvi.SubItems.Add("待校验");
				lvi.SubItems.Add(deleteMsg);
				lvi.SubItems.Add(status.VerifyMessage);
				lvi.ForeColor = Color.PaleVioletRed;
			}
			else if (status.Verified == true)
			{
				lvi.SubItems.Add("已通过");
				lvi.SubItems.Add(deleteMsg);
				lvi.SubItems.Add(status.VerifyMessage);
				lvi.ForeColor = Color.Green;
			}
			else
			{
				lvi.SubItems.Add("未通过");
				lvi.SubItems.Add(deleteMsg);
				lvi.SubItems.Add(status.VerifyMessage);
				lvi.ForeColor = Color.Red;
			}

			lvi.Tag = p;

			UiUtility.ApplySubStyle(lvi);

			return lvi;
		}

		void FilterPassenger()
		{
			list.SuspendLayout();

			list.Items.Clear();
			list.Items.AddRange(_filterKey.IsNullOrEmpty() ? _passes.Values.ToArray() : _passes.Where(s => s.Key.IsMatch(false, _filterKey)).Select(s => s.Value).ToArray());

			list.ResumeLayout();
		}
		void list_KeyUp(object sender, KeyEventArgs e)
		{
			if (!e.Control) return;

			if (e.KeyCode == Keys.C)
			{
				CopyToClipboard();
			}
		}
		void LoadPassengers()
		{
			_passes.Clear();

			if (Session.UserProfile.Passengers == null)
				return;

			foreach (var p in Session.UserProfile.Passengers)
			{
				_passes.Add(p, CreateItem(p));
			}

			Session.UserProfile.Passengers.Added -= Passengers_Added;
			Session.UserProfile.Passengers.Removed -= Passengers_Removed;
			Session.UserProfile.Passengers.Added += Passengers_Added;
			Session.UserProfile.Passengers.Removed += Passengers_Removed;

			FilterPassenger();
			UpdateUsage();
		}

		void PassengerPanel_Load(object sender, EventArgs e)
		{
			loadingtip.Hide();
			loadingtip.KeepCenter();

			if (_inSystemMaintenance)
			{
				ts.Enabled = false;
				loadingtip.SetLoadingError("系统维护期间不可操作");
				loadingtip.Show();
			}

			//工具栏
			tsRefresh.Click += (s, x) =>
			{
				loadingtip.Visible = true;
				Enabled = false;
				Session.AccquireLoadPassengers();
			};
		}

		void Passengers_Added(object sender, ItemEventArgs<Passenger> e)
		{
			var p = e.Item;
			_passes.Add(p, CreateItem(p));
			if (_filterKey.IsNullOrEmpty() || p.IsMatch(false, _filterKey))
				list.Items.Add((_passes[p]));

			UpdateUsage();
		}

		void Passengers_Removed(object sender, ItemEventArgs<Passenger> e)
		{
			var s = e.Item;
			if (s == null || !_passes.ContainsKey(s))
				return;

			list.Items.Remove(_passes[s]);
			_passes.Remove(s);

			UpdateUsage();
		}

		void RefreshItem(ListViewItem item)
		{
			var p = item.Tag as Entity.Web.Passenger;
			item.Text = p.Name;
			item.SubItems[1].Text = ParamData.PassengerType[p.Type];
			item.SubItems[2].Text = ParamData.PassengerIdType[p.IdTypeCode];
			item.SubItems[3].Text = p.IdNo;
			item.SubItems[4].Text = p.MobileNo;
		}

		void tsAdd_Click(object sender, EventArgs e)
		{
			var p = new Entity.Web.Passenger()
			{
				AddDate = DateTime.Now
			};
			var ad = new Dialogs.Passenger.AddPassenger(p);
			if (ad.ShowDialog() != DialogResult.OK)
				return;

			//添加联系人
			var dlg = new YetAnotherWaitingDialog();
			var result = "";
			dlg.Title = "正在记录中, 请稍等.";


			dlg.WorkCallback = () =>
			{
				result = _manager.AddPassenger(p);
			};
			dlg.ShowDialog();

			if (result.IsNullOrEmpty())
			{
				//添加


				AppContext.HostForm.ShowSuccessToastMini("已记录在案...");
			}
			else
			{
				AppContext.HostForm.ShowErrorToastMini("添加联系人可耻地失败了：" + result);
			}
		}

		void tsDelete_Click(object sender, EventArgs e)
		{
			//删除联系人
			if (list.SelectedIndices.Count == 0)
			{
				AppContext.HostForm.ShowWarningToastMini("您还没有选择联系人唷");
				return;
			}

			var plist = list.SelectedItems.Cast<ListViewItem>()
							.Select(s => s.Tag as Entity.Web.Passenger)
							.ToArray();

			var self = plist.FirstOrDefault(s => s.IsUserSelf == "Y");
			if (self != null)
			{
				AppContext.HostForm.ShowInfoToastMini("联系人【" + self.Name + "】是注册账户所用的联系人，无法编辑和删除。");
				return;
			}
			if (plist.Length > 20)
			{
				AppContext.HostForm.ShowInfoToastMini("最多一次删除20联系人，您老选的略多...");
				return;
			}

			if (!Question("确定要消灭联系人 【" + plist.Select(s => s.Name).JoinAsString(", ") + "】 吗？", true))
			{
				return;
			}

			var dlg = new YetAnotherWaitingDialog();
			string result = null;
			dlg.Title = "正在扫地中，请稍等.";
			dlg.WorkCallback = () => result = _manager.DeletePassenger(plist);
			dlg.ShowDialog();
			if (result.IsNullOrEmpty())
			{
				//删除
				list.SuspendLayout();
				var pasList = Session.UserProfile.Passengers;
				plist.ForEach(s =>
				{
					pasList.Remove(s);
				});

				list.ResumeLayout();

				AppContext.HostForm.ShowSuccessToastMini("联系人删除成功");
			}
			else
			{
				AppContext.HostForm.ShowErrorToastMini("联系人删除可耻地失败了....错误信息：" + result);
			}
		}



		void tsEdit_Click(object sender, EventArgs e)
		{
			if (ParamData.IsSystemMaintenance)
			{
				return;
			}

			var pas = list.SelectedItems.Cast<ListViewItem>().FirstOrDefault();
			if (pas == null)
			{
				AppContext.HostForm.ShowErrorToastMini("这位客官, 您老倒是翻一个要编辑的人的牌儿啊...");
				return;
			}
			var p = pas.Tag as Entity.Web.Passenger;
			if (p.Type == 3)
			{
				AppContext.HostForm.ShowWarningToastMini("哎呀, 现在还不支持对学生联系人下手哦, 校长重新选一个吧...");
				return;
			}

			////填充完整资料
			//if (p.BornDate == null)
			//{
			//	//没有完整资料，先刷新
			//	var dg = new FSLib.Windows.Dialogs.YetAnotherWaitingDialog();
			//	Entity.Web.Passenger tp = null;
			//	dg.Title = "正在获得详细资料, 请稍等.";
			//	dg.WorkCallback = () => tp = _manager.FillFullInfo(p);
			//	dg.ShowDialog();

			//	if (tp == null)
			//	{
			//		Information("详细资料刷新失败, 这可能是因为您的联系人中重名的太多导致的...");
			//		return;
			//	}
			//	//刷新现有联系人
			//	_passes.Remove(p);
			//	_passes.Add(tp, pas);
			//	pas.Tag = tp;
			//	p = tp;
			//}

			//原名字
			var oldName = p.Name;
			var oldIdType = p.IdTypeCode;
			var oldId = p.IdNo;

			var ad = new AddPassenger(p);
			if (ad.ShowDialog() != DialogResult.OK)
				return;

			//添加联系人
			var dlg = new YetAnotherWaitingDialog();
			var result = "";
			dlg.Title = "正在记录中, 请稍等.";
			dlg.WorkCallback = () => result = _manager.EditPassenger(p, oldName, oldIdType, oldId);
			dlg.ShowDialog();

			if (result.IsNullOrEmpty())
			{
				//刷新显示(已废弃，因为完全刷新)
				//RefreshItem(_passes[p]);

				AppContext.HostForm.ShowSuccessToastMini("已记录在案...");
			}
			else
			{
				AppContext.HostForm.ShowErrorToastMini("编辑联系人可耻地失败了：" + result);
			}
		}

		void tsFilter_KeyUp(object sender, KeyEventArgs e)
		{
			var key = tsFilter.Text;

			//过滤
			if (key == _filterKey)
				return;
			_filterKey = key;
			FilterPassenger();
		}

		void UpdateUsage()
		{
			var current = Session.UserProfile.Passengers.Count;
			var max = ApiConfiguration.Instance.MaxPassengerCount;

			usage.Maximum = max;
			usage.Value = Math.Min(current, max);

			lblUsage.Text = $"已有 <font color='#ED1C24'><b>{current}</b></font> 人，最多 <font color='#ED1C24'><b>{max}</b></font> 人";
			lblUsage.Location = new Point(panel1.Width - 300 - lblUsage.Size.Width, lblUsage.Location.Y);
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
			if (Session != null && Session.UserProfile.Passengers != null)
			{
				Session.UserProfile.Passengers.Added -= Passengers_Added;
				Session.UserProfile.Passengers.Removed -= Passengers_Removed;
			}

			base.Dispose(disposing);
			_saveFileDialog.Dispose();
			_saveFileDialog.Dispose();
		}

		#region 联系人导入导出

		void ExportToFile()
		{
			//复制
			var pas = list.SelectedItems.Cast<ListViewItem>().Select(s => s.Tag as Entity.Web.Passenger).ToArray();
			if (pas.IsEmpty())
			{
				Information("没有选择联系人");
				return;
			}
			if (_saveFileDialog.ShowDialog() != DialogResult.OK)
				return;

			var fileLines = new List<string>();
			fileLines.Add("#12306订票助手.NET");
			//文本
			foreach (var passenger in pas)
			{
				fileLines.Add(string.Format("{0}, {1}, {2}, {3}, {4}", ParamData.PassengerType.GetValue(passenger.Type), passenger.Name, ParamData.PassengerIdType.GetValue(passenger.IdTypeCode), passenger.IdNo, passenger.MobileNo));
			}
			File.WriteAllLines(_saveFileDialog.FileName, fileLines);

			Information("很愉快地导出到文件了 ♪(´ε｀)");
		}

		/// <summary>
		/// 复制到剪贴板
		/// </summary>
		void CopyToClipboard()
		{
			//复制
			var pas = list.SelectedItems.Cast<ListViewItem>().Select(s => s.Tag as Entity.Web.Passenger).ToArray();
			if (pas.IsEmpty())
			{
				Information("没有选择联系人");
				return;
			}

			var data = new System.Windows.Forms.DataObject();
			//联系人
			var sb = new StringBuilder();
			foreach (var passenger in pas)
			{
				sb.AppendLine(string.Format("{0}, {1}, {2}, {3}, {4}", ParamData.PassengerType.GetValue(passenger.Type), passenger.Name, ParamData.PassengerIdType.GetValue(passenger.IdTypeCode), passenger.IdNo, passenger.MobileNo));
			}
			data.SetData(DataFormats.Text, sb.ToString());
			Clipboard.SetDataObject(data, true);
			Information("已将联系人信息复制到剪贴板");
		}



		#endregion



		#region Overrides of ControlBase

		/// <summary>
		/// 执行初始化
		/// </summary>
		/// <param name="session"></param>
		public override void InitSession(Session session)
		{
			base.InitSession(session);

			session.PassengerLoadComplete += (sender, args) =>
			{
				AppContext.HostForm.Invoke(value_PassengerLoadComplete);
			};
			if (session.UserProfile.Passengers != null)
			{
				AppContext.HostForm.Invoke(value_PassengerLoadComplete);
			}
			_manager.Session = session;
		}


		void value_PassengerLoadComplete()
		{
			if (IsDisposed)
				return;

			loadingtip.Visible = _inSystemMaintenance;
			LoadPassengers();
		}

		#endregion

	}
}
