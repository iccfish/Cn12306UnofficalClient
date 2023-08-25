using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;

using TOBA.Profile;
using TOBA.QueryResumeManager;

namespace TOBA.UI.Dialogs
{
	using Account;

	using Configuration;

	using Controls.Account;
	using Controls.Misc;

	using Data;

	using DevComponents.DotNetBar;
	using DevComponents.DotNetBar.Controls;

	using FSLib.Extension;

	using Misc;

	using Notification;

	using Properties;

	using Service;

	using System;
	using System.Drawing;
	using System.Threading.Tasks;
	using System.Windows.Forms;

	using TOBA.Account.Entities;
	using TOBA.BackupOrder;
	using TOBA.Media;
	using TOBA.UI.Controls;
	using TOBA.UI.Controls.Query;

	using Workers;

	using Program = TOBA.Program;
	using Timer = System.Windows.Forms.Timer;

	internal partial class MainForm : Office2007Form, IMainForm
	{

		private FormWindowState? _prevState = null;

		public MainForm()
		{
			Instance = this;
			AppContext.MainForm = this;
			InitializeComponent();
			Icon = Properties.Resources.mainForm;
			ni.Icon = this.Icon;

			if (Program.IsRunning)
			{
				HandleGlobalEvents();
				RestoreStatus();
				Shown += MainForm_Shown;
				SizeChanged += MainForm_SizeChanged;
				ni.Click += (s, e) => RestoreForm();
				SetTabPosition(ProgramConfiguration.Instance.MainTabPosition.HasValue ? ProgramConfiguration.Instance.MainTabPosition.Value : GetAutoTabAlignment());
				ProgramConfiguration.Instance.PropertyChanged += Instance_PropertyChanged;
				ClientSizeChanged += (s, e) =>
				{
					if (ProgramConfiguration.Instance.MainTabPosition == null)
						SetTabPosition(GetAutoTabAlignment());
				};
			}
			//tsHome.ToolTipText = "登录到鱼·后花园";
		}

		/// <summary>
		/// 对用户的可见性发生变化
		/// </summary>
		public event EventHandler IsWindowVisibleChanged;

		eTabStripAlignment GetAutoTabAlignment()
		{
			return Width < 1200 * Program.ScaleX ? eTabStripAlignment.Top : eTabStripAlignment.Left;
		}
		#region 全局事件响应

		void HandleGlobalEvents()
		{
			Session.UserLogined += (s, e) =>
			{

				var session = (Session)s;
				//var tab = new UserTab(session);
				//tabUser.TabPages.Add(tab);
				//tabUser.SelectedTab = tab;

				//提示信息。
				if (!session.LoginNotification.IsNullOrEmpty() && ProgramConfiguration.Instance.ShowMessageFrom12306)
				{
					new MsgFrom12306(session.LoginNotification).ShowDialog(this);
					session.LoginNotification = null;
				}

				using (InitializingUserPage.Show(this))
				{
					st.SelectedTab = new UserTabDnb(session, st);
				}
				if (session.UserProfile.Passengers == null || session.UserProfile.Passengers.Any(x => x.TotalTimes == 0))
					session.AccquireLoadPassengers();
				HandleSession(session);
			};
			Session.UserVerificationStateChanged += (s, e) =>
			{
				var session = s as Session;
				if (session.IsUserVerified == false)
					this.Invoke(() => new UserNotVerified() { Session = session }.ShowDialog());
			};
			Session.MobileCheckStateChanged += (s, e) =>
			{
				var session = s as Session;
				if (session.IsMobileChecked == false)
					this.BeginInvoke(new Action(() =>
					{
						var tip = new MobileNotChecked(session);
						if (tip.ShowDialog(this) == DialogResult.Yes)
						{
							new AccountMobileCheck(session).ShowDialog(this);
						}
					}));
			};
			Session.Logout += (s, e) => this.Invoke(() =>
			{
				var session = s as Session;
				st.Tabs.OfType<UserTabDnb>().FirstOrDefault(x => x.Session == session)?.Remove();
				GC.Collect();
			});
			Session.ForceLogout += this.SafeInvoke(new EventHandler<GeneralEventArgs<bool>>((s, e) =>
			{
				var session = (Session)s;
				if (session == null)
					return;

				//撤消改签
				session.UserProfile.QueryParams.Where(x => x.Resign).ToArray().ForEach(x => x.IsLoaded = false);

				ni.ShowBalloonTip(5000, "账户已被踢", "账户【" + session.UserKeyData?.DisplayName + "】：您已被踢。如果需要继续订票，请重新登录。", ToolTipIcon.Warning);

				var (isOpen, _) = ParamData.GetSystemMaintenanceTime();
				if (MediaConfiguration.Instance.EnableForceLogoutAudioPrompt && isOpen)
					LosingSoundPlayer.Instance.PlayAsync();
			}));
			//Session.MayNeedRelogin += (s, e) =>
			//{
			//	var session = s as Session;
			//	if (session == null)
			//		return;

			//	e.Canceled = this.Invoke<Session, bool, bool>(CheckNeedRelogin, session, e.Data);
			//};
			//Session.LoginConflict += (s, e) =>
			//{
			//	this.Invoke(() =>
			//	{
			//		if (!ProgramConfiguration.Instance.DismissedDialogs.Contains("login_confliect"))
			//		{
			//			new LoginConflict().ShowDialog(this);
			//		}
			//	});
			//};
			Session.PreInputedVcMissed += (s, e) =>
			{
				var session = s as Session;
#if DEBUG
				ni.ShowBalloonTip(3000, "验证码已失效", string.Format("账户【{0}】提前输入的验证码已经失效，历时 {1} 分钟", session.UserName, (DateTime.Now - session.LastVerifyCodeInputTime.Value).TotalMinutes.ToString("#0.00")), ToolTipIcon.Warning);
#else
				ni.ShowBalloonTip(2000, "验证码已失效", string.Format("账户【{0}】提前输入的验证码已经失效", session.UserName), ToolTipIcon.Warning);
#endif
			};
			Session.OrderSubmitSuccess += (s, e) =>
			{
			};
			ControllerSession.NotificationCreated += (s, e) => UiInvoke(() =>
			{
				ShowBalloonTip(1500, "订单自动操作通知", e.Data, ToolTipIcon.Warning);
			});
		}

		void HandleSession(Session session)
		{
			session.AddPropertyChangedEventHandler(_ => _.FaceCheckStatus,
				(_1, _2) =>
				{
					var s1 = _1 as Session;
					if (s1.FaceCheckStatus == false)
					{
						var msg = $"账号【{s1.UserName}】没有通过12306人脸认证哦，请尽快通过12306官方APP完成认证，否则部分功能无法使用唷。";
						this.ShowWarningToast(msg);
						ni.ShowBalloonTip(5000, "人脸验证提醒", msg, ToolTipIcon.Warning);
					}
				});
			var hip = session.GetService<IHbInfoProvider>();
			hip.TrainHbAvailable += this.SafeInvoke(new EventHandler<TrainHbAvailableEventArgs>((s, e) =>
			{
				var msg = $"车次 {e.ResultItem.Code} 的 【{ParamData.GetSeatTypeName(e.TicketData.Code)}】候补状态已变更为可候补（{e.CacheItem.Info}），请留意哦";
				this.ShowInfoToastMini(msg);
				ni.ShowBalloonTip(5000, "可候补提示", msg, ToolTipIcon.Info);
			}));
		}

		#endregion

		#region 后台任务管理

		/// <summary>
		///     初始化后台任务
		/// </summary>
		void InitBackgroundTask()
		{
			TaskManager tm = TaskManager.Instance;
			tm.TaskStarted += (s, e) =>
			{
				return;
				//this.Invoke(() =>
				//{
				//	stuProg.Visible = true;
				//	stuProg.Style = ProgressBarStyle.Marquee;
				//	stuInfo.Text = "正在执行任务 " + e.Task.TaskName;
				//	stuInfo.Image = FSLib.Windows.Properties.Resources.right;
				//});
			};
			tm.AllTaskFinished += (s, e) =>
			{
				return;
				//this.Invoke(() =>
				//{
				//	stuProg.Visible = false;
				//	stuInfo.Text = "已完成所有后台任务";
				//	stuInfo.Image = Resources.tick_16;
				//});
			};
			//stuInfo.Visible = false;
		}

		#endregion

		void Instance_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "MainTabPosition")
				st.TabAlignment = ProgramConfiguration.Instance.MainTabPosition.HasValue ? ProgramConfiguration.Instance.MainTabPosition.Value : GetAutoTabAlignment();
		}

		/// <summary>
		///     加载事件，初始化界面
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void MainForm_Load(object sender, EventArgs e)
		{
			InitializeUI();
			InitializeEvents();
			InitServerTime();
			InitBackgroundTask();

			//自动恢复会话
			if (RunTime.SessionManager.Count > 0)
			{
				//tabUser.TabPages.AddRange(RunTime.SessionManager.Select(s => (TabPage)new UserTab(s)).ToArray());
				if (ProgramConfiguration.Instance.EnablePromotion == null)
				{
					using (InitializingUserPage.Show(this))
					{
						RunTime.SessionManager.ForEach(s =>
						{
							HandleSession(s);
							new UserTabDnb(s, st);
						});
					}

				}
				else
				{
					st.SelectedTab = RunTime.SessionManager.Select(s => new UserTabDnb(s, st)).Last();
				}

				RunTime.SessionManager.Where(s => !s.UserProfile.IsPassengerLoaded).ForEach(s => s.AccquireLoadPassengers());
			}

			//在系统关闭时间，自动打开查询页
			var (isOpen, _) = ParamData.GetSystemMaintenanceTime();
			if (!isOpen)
			{
				OpenQueryPageWithoutLogin();
			}
		}

		void MainForm_Shown(object sender, EventArgs e)
		{
			//init auto vc
			var autovccfg = AutoVcConfig.Instance;
			if (VerifyCodeRecognizeServiceLoader.VerifyCodeRecognizeEngine != null)
			{
				var engine = VerifyCodeRecognizeServiceLoader.VerifyCodeRecognizeEngine;
				engine.UserName = autovccfg.UserName;
				engine.Password = autovccfg.Pasword;
				Task.Factory.StartNew(() =>
				{
					try
					{
						engine.DoLogin();
					}
					catch (Exception ex)
					{
						TOBA.Events.OnError(this, new EventInfoArgs($"远程打码登录错误：{ex.Message}"));
					}
				});
			}

			//if (VerifyCodeRecognizeServiceLoader.VerifyCodeRecognizeEngine != null && VerifyCodeRecognizeServiceLoader.VerifyCodeRecognizeEngine.GetType() != typeof(Platform.Vc.VerifyCodeUuWiseExtension))
			//	new VerifyCodeRecogniseWarning().ShowDialog();

			//if (!ProgramConfiguration.Instance.NotSaleVerification)
			//	new NotForSaleNotification().ShowDialog();

			AppContext.OnMainProgramLoaded(this);
		}

		void MainForm_SizeChanged(object sender, EventArgs e)
		{
			if (WindowState == FormWindowState.Minimized)
			{
				if (ProgramConfiguration.Instance.MinimizeToTray)
				{
					Visible = false;
					ni.ShowBalloonTip(2000, "提示", "订票助手.NET正在后台运行！查到票后将会自动恢复显示！\n强烈建议您随时关注状态！", ToolTipIcon.Info);
				}
			}
		}

		void SetTabPosition(eTabStripAlignment alignment)
		{
			st.TabAlignment = alignment;
			st.FixedTabSize = alignment == eTabStripAlignment.Top || alignment == eTabStripAlignment.Bottom ? Size.Empty : new Size(180, 0);
		}

		protected virtual void OnIsWindowVisibleChanged()
		{
			IsWindowVisibleChanged?.Invoke(this, EventArgs.Empty);
		}
		/// <summary>
		/// 引发 <see cref="E:System.Windows.Forms.Control.SizeChanged"/> 事件。
		/// </summary>
		/// <param name="e">包含事件数据的 <see cref="T:System.EventArgs"/>。</param>
		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);

			if (_prevState != WindowState)
			{
				_prevState = WindowState;
				OnIsWindowVisibleChanged();
			}
		}

		/// <summary>
		/// 引发 <see cref="E:System.Windows.Forms.Control.VisibleChanged"/> 事件。
		/// </summary>
		/// <param name="e">一个包含事件数据的 <see cref="T:System.EventArgs"/>。</param>
		protected override void OnVisibleChanged(EventArgs e)
		{
			base.OnVisibleChanged(e);
			OnIsWindowVisibleChanged();
		}

		#region 程序集方法

		/// <summary>
		/// 返回样式控制器
		/// </summary>
		internal StyleManager StyleManager { get { return sm; } }

		#endregion


		/// <summary>
		/// 按照内容类型查找Tab
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="createIfNull">如果没有找到，是否新建</param>
		/// <param name="active">如果已找到，是否激活</param>
		/// <returns></returns>
		public SuperTabItem FindTabByContentType<T>(bool createIfNull = false, bool active = false) where T : ControlBase, new()
		{
			var tab = st.Tabs.OfType<SuperTabItem>().FirstOrDefault(s => s.AttachedControl != null && s.AttachedControl.GetType() == typeof(T));
			if (tab == null && createIfNull)
			{
				var control = new T()
				{
					Dock = DockStyle.Fill
				};
				tab = new SuperTabItem
				{
					AttachedControl = control,
					Text = control.Text,
					Image = control.Image
				};
				st.Tabs.Add(tab);
			}

			if (active && tab != null)
				st.SelectedTab = tab;

			return tab;
		}
		/// <summary>
		/// 转到不登录直接查票页面
		/// </summary>
		public void OpenQueryPageWithoutLogin()
		{
			var tab = st.Tabs.OfType<QueryNonLogin>().FirstOrDefault();
			if (tab != null)
			{
				st.SelectedTab = tab;
				return;
			}

			st.SelectedTab = new QueryNonLogin(st);
		}

		public void RestoreForm()
		{
			if (Visible)
				return;

			Visible = true;
			if (WindowState == FormWindowState.Minimized)
				WindowState = FormWindowState.Normal;
		}

		/// <summary>
		///     获得主窗体的实例
		/// </summary>
		public static MainForm Instance { get; private set; }

		/// <summary>
		/// 获得主窗口是否对用户可见
		/// </summary>
		public bool IsWindowVisible => Visible && WindowState != FormWindowState.Minimized;

		#region UI处理

		string _versionPrefix;

		void RestoreStatus()
		{
			var pc = ProgramConfiguration.Instance;

			if (pc.DnbGlobalStyle.HasValue)
			{
				sm.ManagerStyle = pc.DnbGlobalStyle.Value;
			}
			if (pc.GlobalColorHint.HasValue)
				sm.ManagerColorTint = pc.GlobalColorHint.Value;

			if (QueryViewConfiguration.Instance.MainFormWindowState != FormWindowState.Minimized)
				WindowState = QueryViewConfiguration.Instance.MainFormWindowState;
			if (!QueryViewConfiguration.Instance.MainFormSize.IsEmpty)
				Size = QueryViewConfiguration.Instance.MainFormSize;

			Point formLocation = QueryViewConfiguration.Instance.MainFormLocation;
			if (!formLocation.IsEmpty && formLocation.X >= 0 && formLocation.Y >= 0 && formLocation.X + Width <= Screen.PrimaryScreen.Bounds.Width && formLocation.Y + Height <= Screen.PrimaryScreen.Bounds.Height)
			{
				StartPosition = FormStartPosition.Manual;
				Location = QueryViewConfiguration.Instance.MainFormLocation;
			}
			SizeChanged += (s, e) =>
			{
				QueryViewConfiguration.Instance.MainFormWindowState = WindowState;
				if (WindowState == FormWindowState.Normal)
				{
					QueryViewConfiguration.Instance.MainFormSize = Size;
					QueryViewConfiguration.Instance.MainFormLocation = Location;
				}
			};
			LocationChanged += (s, e) =>
			{
				if (WindowState == FormWindowState.Normal)
					QueryViewConfiguration.Instance.MainFormLocation = Location;
			};
		}

		void InitializeUI()
		{
			//StudioPackage.AboutMenuItem addonMenu = Program.Studio.GetAboutAddonMenu(true, true, true);
			//tsMain.Items.Add(addonMenu);
			//addonMenu.CheckUpdateRequested += (s, e) => { Updater.CheckUpdateSimple(); };
			//addonMenu.TraceModeRequired += (s, e) =>
			//{
			//	if (!MessageDialog.Question("确定要进入调试模式吗？\n调试模式下订票助手.NET会自动将运行日志记录到『桌面』的“订票助手.NET 跟踪日志.txt”文件中，但是运行速度会下降。\n如果不是因为订票助手运行遇到问题，请不要使用。"))
			//		return;

			//	Program.EnableTrace();
			//};
			statusStrip1.Items.Insert(statusStrip1.Items.Count - 2, new QuickSettingStatusItem());
			//add 内存警告
			statusStrip1.Items.Insert(statusStrip1.Items.Count - 2, new MemoryMonitoringLabel());
			//邮件
			statusStrip1.Items.Insert(statusStrip1.Items.Count - 2, new MailStatus());

			//初始化标签图标
			tabImgList.Images.Add("user", Resources.user_16);
			tabImgList.Images.Add("userbusy", Resources.monitor_16);
			tabImgList.Images.Add("protected", Resources.cou_16_protection);

			//tabUser.TabPages.Add(new TabWelcome());
			//tabUser.TabPages.Add(new ServerTab());
			//tabUser.TabPages.Add(new SysLogTab());
			//TODO 处理注册

			//状态栏
			stuAdv.Click += (_, __) => Shell.StartUrl("https://github.com/iccfish/Cn12306UnofficalClient/issues");

			stuMute.Visible = false;

			//tsViewMoreFilterOption.CheckOnClick = true;
			//tsViewMoreFilterOption.Checked = !QueryViewConfiguration.Instance.HideExtraFilterOption;
			//tsViewMoreFilterOption.CheckedChanged += (s, e) => QueryViewConfiguration.Instance.HideExtraFilterOption = !tsViewMoreFilterOption.Checked;

			RefreshTitle();

			//初始化声音提示
			InitTicketMedia();
		}

		void RefreshTitle()
		{
			var title = $"订票助手.NET V{AppContext.ClientVersion.FileMajorPart} ({AppContext.ClientVersion.FileVersion}/开源版";
			title += " - 木魚诚意发布 - " + (DateTime.Now.Year - 2012) + "年相伴，愿岁月静好";

			Text = title;
		}


		#endregion

		#region 事件响应

		void InitializeEvents()
		{
			st.TabItemClose += (s, e) =>
			{
				var tab = e.Tab as UserTabDnb;
				if (tab != null)
				{
					e.Cancel = !MessageDialog.Question("确定要把放出来的账号【" + tab.Session.UserKeyData.DisplayName + "】残忍地关回小黑屋里？", true);
					if (!e.Cancel)
					{
						Session.OnLogout(tab.Session, LogoutReason.UserManually);
						e.Cancel = true;
					}
				}
				else if (e.Tab is SuperTabItem)
				{
					st.Tabs.Remove(e.Tab);
					st.Controls.Remove((e.Tab as SuperTabItem).AttachedControl);
					st.SelectedTabIndex = 0;
				}
			};

			//状态统计
			stuStatistics.DropDownOpening += (s, e) =>
			{
				UpdateStatistics();
			};
			stuStatisticsQuery.DropDownOpening += (s, e) => UpdateQueryStatistics();

			Shown += (s, e) =>
			{
				var (isOpen, _) = ParamData.GetSystemMaintenanceTime();

				if (ProgramConfiguration.Instance.AutoShowLoginDialog && isOpen && !st.Tabs.OfType<UserTabDnb>().Any())
					Login();
			};

			//IP被封
			var lastIpBlockTipTime = (DateTime?)null;
			TOBA.Events.IpBlocked += this.SafeInvoke((s, e) =>
			{
				if (!ProgramConfiguration.Instance.ShowIPBlockTip)
					return;
				if (lastIpBlockTipTime != null && (DateTime.Now - lastIpBlockTipTime.Value).TotalMinutes <= 1)
					return;

				lastIpBlockTipTime = DateTime.Now;
				DesktopAlert.Show(
					"<b>检测到您的IP已经被12306封锁。</b>\n请重启路由器（拨号上网）\n或使用代理服务器（固定IP或局域网）。\n可能是其他人过于频繁刷票导致此问题的，揍丫的。",
					Properties.Resources.cou_32_warning,
					eDesktopAlertColor.Red,
					eAlertPosition.BottomRight,
					30,
					1L,
					_ =>
					{
						var dlg = new UI.Dialogs.ConfigCenter();
						dlg.Shown += (ss, ee) =>
						{
							dlg.SelectedConfig = dlg.FindConfigUI<UI.Controls.Option.NetworkConfig>().First();
						};

						dlg.ShowDialog();
					}
				);
			});
		}

		void UpdateStatistics()
		{
			if (stuStatistics.Tag == null)
			{
				foreach (var toolStripMenuItem in stuStatistics.DropDownItems.OfType<ToolStripMenuItem>().Where(s => s.Text.IndexOf('#') != -1))
				{
					toolStripMenuItem.Tag = toolStripMenuItem.Text;
				}
				stuStatistics.Tag = string.Empty;
			}
			var st = Configuration.Statistics.Current;
			var type = st.GetType();
			st.Save();
			foreach (var toolStripMenuItem in stuStatistics.DropDownItems.OfType<ToolStripMenuItem>().Where(s => s.Tag is string))
			{
				var tpl = toolStripMenuItem.Tag as string;
				if (tpl.IsNullOrEmpty())
					continue;

				toolStripMenuItem.Text = Regex.Replace(tpl, @"#([a-zA-Z\d]+)#", _ =>
				{
					var p = type.GetProperty(_.Groups[1].Value);
					if (p == null) return _.Value;

					var value = p.GetValue(st, null);
					if (value == null)
						return "";

					var vt = value.GetType();
					if (vt == typeof(int))
						return ((int)value).ToString("N0");
					if (vt == typeof(long))
						return ((long)value).ToString("N0");
					if (vt == typeof(TimeSpan))
						return ((TimeSpan)value).ToString("dd\"天\"hh\"小时\"mm\"分\"ss\"秒\"");
					if (vt.IsValueType)
						return value.ToString();
					return value.ToString();
				});
			}

			TOBA.UI.UIEvents.OnRequireUpdateStatistics(this, new RequireUpdateStatisticsEventArgs() { ToolStripDropDownButton = stuStatistics });
		}

		void UpdateQueryStatistics()
		{
			if (stuStatisticsQuery.Tag == null)
			{
				foreach (var toolStripMenuItem in stuStatisticsQuery.DropDownItems.OfType<ToolStripMenuItem>())
				{
					toolStripMenuItem.Tag = toolStripMenuItem.Text;
				}
				stuStatisticsQuery.Tag = string.Empty;
			}
			var st = Configuration.Statistics.Current.QueryInterfaceStatus;
			var type = st.GetType();
			foreach (var toolStripMenuItem in stuStatisticsQuery.DropDownItems.OfType<ToolStripMenuItem>().Where(s => s.Tag is string))
			{
				var tpl = toolStripMenuItem.Tag as string;
				toolStripMenuItem.Text = Regex.Replace(tpl, @"#([a-zA-Z\d]+)#", _ =>
				{
					var p = type.GetProperty(_.Groups[1].Value);
					if (p == null) return "???";

					var value = p.GetValue(st, null);
					if (value == null)
						return "";

					var vt = value.GetType();
					if (vt == typeof(int))
						return ((int)value).ToString("N0");
					if (vt == typeof(long))
						return ((long)value).ToString("N0");
					if (vt == typeof(TimeSpan))
						return ((TimeSpan)value).ToString("dd\"天\"hh\"小时\"mm\"分\"ss\"秒\"");
					if (vt.IsValueType)
						return value.ToString();
					return value.ToString();
				});
			}
		}

		/// <summary>
		///     会话标签页已经切换
		/// </summary>
		public void UserTabSelected(TabControlEventArgs e)
		{
			TabPage tab = e.TabPage;
			bool isUser = tab is UserTab;

			//菜单可用性
			//tsToolOpenBrowser.Enabled = tbRelogin.Enabled = isUser;
		}

		/// <summary>
		///     会话标签页已经切换
		/// </summary>
		public void UserTabSelecting(TabControlCancelEventArgs e)
		{
		}

		/// <summary>
		///     会话标签页已经切换
		/// </summary>
		public void UserTabDeselected(TabControlEventArgs e)
		{
		}

		/// <summary>
		///     会话标签页已经切换
		/// </summary>
		public void UserTabDeselecting(TabControlCancelEventArgs e)
		{
		}

		#endregion

		#region 属性

		/// <summary>
		///     获得或设置当前选定的会话
		/// </summary>
		public Session SelectedSession
		{
			get { return (st.SelectedTab as UserTabDnb).SelectValue(s => s.Session); }
			set
			{
				if (value == null)
				{
					//tabUser.SelectedIndex = 0;
					st.SelectedTabIndex = 0;
					return;
				}
				//var tab = tabUser.TabPages.OfType<UserTab>().FirstOrDefault(s => s.Session == value);
				var tab = st.Tabs.OfType<UserTabDnb>().FirstOrDefault(s => s.Session == value);
				if (tab != null)
					st.SelectedTab = tab;
				else
					st.SelectedTabIndex = 0;
			}
		}

		/// <summary>
		///     获得或设置当前选定的用户页
		/// </summary>
		public UserTabDnb SelectedUserTab
		{
			get { return st.SelectedTab as UserTabDnb; }
			set
			{
				if (value == null)
				{
					st.SelectedTabIndex = 0;
					return;
				}
				st.SelectedTab = value;
			}
		}

		#endregion

		#region 公开方法

		/// <summary>
		///     登录用户，此方法仅能登录已经保存密码的用户
		/// </summary>
		public void Login(string username)
		{
			if (!ProgramConfiguration.Instance.EnableConflictLogin && RunTime.SessionManager.IsLogined(username))
			{
				SelectedSession = RunTime.SessionManager.Find(s => s.UserName == username);
				return;
			}

			var pwd = UserKeyDataMap.Current[username].SelectValue(s => s.Password);
			//if (string.IsNullOrEmpty(pwd))
			//{
			using (var logindlg = new Login()
			{
				PreSelectUser = username
			})
			{
				logindlg.ShowDialog();
			}
			//}
			//else
			//{
			//	using (var ulogin = new UserLoginComponent() { EnableFallback = true, OwnerForm = this })
			//	{
			//		//执行登录过程
			//		var worker = new RequireSessionLoginWorker()
			//		{
			//			Session = null,
			//			UserName = username,
			//			Password = UserKeyDataMap.Current[username].SelectValue(s => s.Password),
			//			TempMode = false,
			//			StorePwd = true
			//		};
			//		ulogin.RunLoginProcedure(worker);
			//		if (worker.Session != null)
			//		{
			//			Session.OnUserLogined(worker.Session);
			//		}
			//	}
			//}
		}


		/// <summary>
		///     登录用户
		/// </summary>
		public void Login()
		{
			UiUtility.PlaceFormAtCenter(new Login());
		}

		/// <summary>
		/// 在UI线程上回调
		/// </summary>
		/// <param name="action"></param>
		public void UiInvoke(Action action)
		{
			if (this.IsHandleAvailable())
				this.Invoke(action);
			else action();
		}

		#endregion

		#region 服务器时间

		/// <summary>
		///     服务器时间
		/// </summary>
		public DateTime? ServerTime { get; set; }

		/// <summary>
		///     初始化服务器时间调用
		/// </summary>
		void InitServerTime()
		{
			var prompted = false;

			var timer = new Timer();
			timer.Interval = 1000;
			timer.Tick += (s, e) =>
			{
				if (RunTime.ServerTimeOffset == null)
					return;
				ServerTime = DateTime.Now.Add(RunTime.ServerTimeOffset.Value);

				double seconds = RunTime.ServerTimeOffset.Value.TotalSeconds;
				tsStatusServerTime.Text = ServerTime.Value.ToString("HH:mm:ss") + "(" + (seconds < 0 ? "慢" : "快") + Math.Abs(seconds).ToString("#0.00") + "秒)";

				if (!prompted && Math.Abs(seconds) > 3600)
				{
					prompted = true;
					new LocalTimeDifferenceTooLarge().ShowDialog(this);
				}
			};

			timer.Start();
		}

		#endregion

		#region 多媒体

		void InitTicketMedia()
		{
			Operation.Instance.MusicPlayStarted += (s, e) =>
			{
				stuMute.Visible = true;
			};
			Operation.Instance.MusicPlayStoped += (s, e) =>
			{
				stuMute.Visible = false;
			};

			stuMute.Click += (s, e) => StopPlayTicketMusic(true);
		}

		/// <summary>
		///     播放有票的声音提示
		/// </summary>
		public void PlayTicketMusic()
		{
			ni.ShowBalloonTip(3000, "有票了！", "刷到可以定的票了！", ToolTipIcon.Info);

			if (ProgramConfiguration.Instance.EnableMusicPrompt)
			{
				Operation.Instance.PlayMusic();
			}
		}

		/// <summary>
		///     停止音乐
		/// </summary>
		public void StopPlayTicketMusic(bool force = false)
		{
			if (!Operation.Instance.TicketPromptMusic.IsPlaying)
				return;

			//如果还有提示有票，那么也忽略
			if (!force && RunTime.SessionManager.Any(s => s.UserProfile.QueryParams.HasTicket))
				return;

			Operation.Instance.StopMusic();
		}

		/// <summary>
		/// 显示弹出气泡通知
		/// </summary>
		/// <param name="timeout"></param>
		/// <param name="title"></param>
		/// <param name="content"></param>
		/// <param name="icon"></param>
		public void ShowBalloonTip(int timeout, string title, string content, ToolTipIcon icon)
		{
			ni.ShowBalloonTip(timeout, title, content, icon);
		}

		#endregion

	}
}
