namespace TOBA.UI.Controls.MainFrame
{
	using Autofac;

	using Data;

	using DevComponents.DotNetBar;

	using Dialogs;
	using Dialogs.Account;
	using Dialogs.Misc;

	using Profile;

	using System;
	using System.Linq;
	using System.Windows.Forms;

	using TOBA.Account.Entities;

	public partial class TopNav : UserControl
	{
		public TopNav()
		{
			InitializeComponent();

			if (!Program.IsRunning)
				return;

			Load += TopNav_Load;
		}

		private void TopNav_Load(object sender, EventArgs _)
		{
			btnLogin.PopupOpen += btnImport_PopupShowing;
			btnLogin_New.Click += (s, e) =>
			{
				new Login().ShowDialog();
			};
			btnOpt.Click += (s, e) => new ConfigCenter().ShowDialog();
			btnQueryWithoutLogin.Click += (s, e) => MainForm.Instance.OpenQueryPageWithoutLogin();
			btnLearn.Click += (s, e) => Shell.StartUrl("https://blog.iccfish.com/docs/%E8%AE%A2%E7%A5%A8%E5%8A%A9%E6%89%8B-net/");
			btnLoginUsingQr.Click += (s, e) =>
			{
				new QrLogin().Show(this.FindForm());
			};

			InitLinks();

			var endDate = ParamData.GetMaxTicketDate(false);
			de.Date = endDate;
			de.DateChanged += (s, e) =>
			{
				de.Date = endDate;
			};

		}

		void InitLinks()
		{
			pSupport.Style.BorderSide &= ~eBorderSide.Left;
			//btnSupportAbout.Click += (s, e) =>
			//{
			//	FSLib.Windows.Interactive.Shell.StartUrl("https://www.fishlee.net/soft/12306/");
			//};
			btnSupportDonate.Click += (s, e) =>
			{
				Shell.StartUrl("https://blog.iccfish.com/about/donate/");
			};
			btnSupportWb.Click += (s, e) =>
			{
				Shell.StartUrl("https://weibo.com/imcfish");
			};
			btnSupportBlog.Click += (s, e) =>
			{
				Shell.StartUrl("https://blog.iccfish.com/");
			};
			//btnSupportAbout.Click += (s, e) => Program.Studio.GetAboutForm().ShowDialog(this.FindForm());
			btnSupportAbout.Click += (s, e) => new AboutMe().ShowDialog(this.FindForm());
		}

		void btnImport_PopupShowing(object sender, EventArgs e)
		{
			/*
			 *
			 				var mc = ctxLogin.Items.OfType<ToolStripMenuItem>();

			 * */
			var pc = btnLogin.SubItems.OfType<ButtonItem>();
			var logined = RunTime.SessionManager.Select(s => s.UserName).MapToHashSet();
			var datas = UserKeyDataMap.Current.UserKeyData.OrderByDescending(s => s, new UserKeyDataComparer()).Select(s => s.Key).ToList();
			foreach (var toolStripMenuItem in pc.Where(s => s.Tag != null && !datas.Contains(s.Tag.ToString())).ToArray())
			{
				btnLogin.SubItems.Remove(toolStripMenuItem);
			}

			foreach (var un in datas.Except(pc.Where(s => s.Tag != null).Select(s => s.Tag as string).MapToHashSet()).OrderBy(s => s).ToArray())
			{
				var u = UserKeyDataMap.Current[un];
				var tsmi = new ButtonItem() { Tag = un };
				tsmi.Click += (x, y) =>
				{
					AppContext.MainForm.Login((x as ButtonItem).Tag as string);
				};
				btnLogin.SubItems.Add(tsmi);
			}

			var beginGroup = true;
			foreach (var buttonItem in pc.Where(s => s.Tag != null))
			{
				buttonItem.BeginGroup = beginGroup;
				beginGroup = false;

				var un = buttonItem.Tag as string;
				var u = UserKeyDataMap.Current[un];
				var islogined = logined.Contains(un);

				buttonItem.FontBold = false;
				buttonItem.Text = $"<b>{u.DisplayName.DefaultForEmpty(un)}</b> ({un})  <font size='-2' color='gray'>[{(islogined ? "已登录/" : "")}{u.LoginTimes:N0} 次登录{u.LastLoginTime:'/最后登录 'yy-MM-dd HH:mm}]</font>";
				//if (!ProgramConfiguration.Instance.EnableConflictLogin)
				//{
				//	if (!islogined)
				//		toolStripMenuItem.Text += ;
				//}
			}

			btnLogin_None.Visible = !pc.Any(s => s.Tag != null);
		}


	}
}
