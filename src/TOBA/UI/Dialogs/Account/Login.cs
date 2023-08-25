using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using TOBA.Profile;

namespace TOBA.UI.Dialogs.Account
{
	using Autofac;

	using Configuration;

	using Controls.Misc;

	using DevComponents.DotNetBar;

	using System.Threading.Tasks;

	using TOBA.Account;
	using TOBA.Account.Entities;
	using TOBA.Service;

	using Vc;

	using DialogBase = TOBA.UI.Dialogs.DialogBase;

	internal partial class Login : DialogBase
	{
		private ISessionLoginService _service;
		private bool _relogin;
		private MsgLayer _layer;

		private Session _session;
		private int _retryCount;
		static string _defaultTip = "我做过很多愚蠢的事情，但是我毫不在乎，我的朋友把它叫做自信。";

		/// <summary>
		/// 获得或设置当前的会话
		/// </summary>
		public Session Session
		{
			get { return _session; }
			set
			{
				_session = value;
				_relogin = value != null;

				if (_relogin)
				{
					cbUserName.Enabled = false;
					txtPassword.Enabled = _session.Password.IsNullOrEmpty();
					chkTmp.Enabled = false;
					cbRememberPwd.Enabled = false;

					txtPassword.Text = _session.Password;
					cbUserName.Text = _session.UserName;

					//会话
					tsVirutalLogin.Enabled = false;

					if (_session.TemporaryMode)
					{
						cbRememberPwd.Checked = false;
						chkTmp.Checked = true;
						lnkDelete.Enabled = false;
					}
				}
			}
		}

		async void Prepare()
		{
			//刷新HTTP配置
			Session?.ResetHttpConf();
			tc.SetIfNeedVc(null);

			btnLogin.Enabled = false;
			ts.Image = Properties.Resources._16px_loading_1;
			ts.Text = "正在准备中...";

			_service.Session = _session;

			var result = await _service.PrepareLoginAsync().ConfigureAwait(true);
			if (IsDisposed)
				return;

			if (result == null)
			{
				if (_relogin)
				{
					await _layer.Information("会话未失效，无需重新登录。").ConfigureAwait(true);
				}
				else
				{
					Session.OnUserLogined(_service.Session);
				}

				DialogResult = DialogResult.OK;
				Close();
				return;
			}
			if (result == false)
			{
				ts.Image = Properties.Resources.warning_16;
				ts.Text = _service.State;

				if (await _layer.Confirm("无法登录，12306返回信息：" + _service.State + "，是否重试？").ConfigureAwait(true))
					Prepare();
				else
				{
					DialogResult = DialogResult.Cancel;
					Close();
				}
				return;
			}
			KeyDown += (s, e) =>
			{
				if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter)
				{
					e.Handled = true;
					CheckAndSubmit();
				}
			};

			//fix: 当窗口提前关闭时，不继续执行
			if (Controls.Count == 0)
				return;

			//拉取验证码
			tc.InitSession(_service.Session);
			tc.SetIfNeedVc(_service.NeedVcLogin);
			tc.EnableAutoVc = ProgramConfiguration.Instance.AutoEnterLoginVcCode;
			//tc.LoadVerifyCode();


			if (!_service.NeedVcLogin)
			{
				//无需验证码
				EnterReadyState();
			}
		}

		async Task DoLogin()
		{
			lblError.Visible = false;
			btnLogin.Enabled = false;
			ts.Image = Properties.Resources._16px_loading_1;
			ts.Text = "正在登录中...";

			_service.UserName = SelectedUserName;
			_service.Password = txtPassword.Text;
			_service.RandCode = tc.Code;
			_service.TempMode = chkTmp.Checked;
			_service.StorePwd = cbRememberPwd.Checked;
			_service.VcType   = VcType.None;
			// 加载用户关键数据
			_service.Session.UserKeyData = UserKeyDataMap.Current[_service.UserName] ?? _service.Session.UserKeyData;

			//自动识别引起的？
			var vcResult = tc.AutoVcCode;
			var result = await _service.LoginAsync().ConfigureAwait(true);

			if (!result)
			{
				using (var dlg = new RequestVerification(_service))
				{
					var ret = dlg.ShowDialog(this);
					if (ret != DialogResult.OK)
					{
						_blockAutoSubmitOnce = ret is DialogResult.Cancel;
						Prepare();
						return;
					}

					_service.SmsTime     = dlg.SmsTime;
					_service.CfSessionId = dlg.CfSessionId;
					_service.Sig = dlg.Sig;
					_service.RandCode    = dlg.RandCode;
					_service.VcType      = dlg.CurrentVcType;

					result = await _service.CompleteVcAsync();
				}
			}

			if (result)
			{
				if (!_relogin)
				{
					Session.OnUserLogined(_service.Session);
				}
				if (_service.LoginConflict)
				{
					//UiUtility.PlaceFormAtCenter(new LoginConflict(), AppContext.HostForm);
					AppContext.HostForm.ShowToast("之前的的12306登录已经被踢掉。如果要多开账户登录，请使用多个12306账户，现在一个账户不可多次登录。", timeout: 10000);
				}

				DialogResult = DialogResult.OK;
				Close();

				return;
			}

			var isVcError = _service.State?.IndexOf("验证码") != -1;
			lblError.Text = $"12306返回信息：{_service.State}";
			lblError.Visible = true;

			if (isVcError && _service.NeedVcLogin)
			{
				_service.Session.HttpConf.IsLoginPassCode = true;

				if (vcResult != null && VerifyCodeRecognizeServiceLoader.VerifyCodeRecognizeEngine != null)
				{
					VerifyCodeRecognizeServiceLoader.VerifyCodeRecognizeEngine.MarkResult(vcResult, false);
				}

				if (_retryCount++ >= 2)
				{
					_retryCount = 0;
					Prepare();
				}
				else
					tc.LoadVerifyCode();
			}
			else
			{

				Prepare();
			}
		}

		void EnterReadyState()
		{
			Activate();
			BringToFront();
			tc.Enabled = true;
			ts.Image = Properties.Resources.tick_16;
			ts.Text = "可以登录了...";
			btnLogin.Enabled = true;

			//无需验证码？
			AutoSubmitLogin();
		}

		private bool _blockAutoSubmitOnce;
		void AutoSubmitLogin()
		{
			if (_service.Session?.HttpConf == null)
				return;

			// 在上次验证取消后，停止自动登录一次
			if (_blockAutoSubmitOnce)
			{
				_blockAutoSubmitOnce = false;
				return;
			}

			if (!_service.NeedVcLogin && btnLogin.Enabled && !cbUserName.Text.IsNullOrEmpty() && !txtPassword.Text.IsNullOrEmpty())
			{
				btnLogin.PerformClick();
			}
		}

		public Login()
		{
			InitializeComponent();

			_layer = new MsgLayer()
			{
				DlgSize = new Size(350, 150)
			};
			Controls.Add(_layer);
			_layer.BringToFront();

			Icon = Properties.Resources.icon_login;
			_service = AppContext.ExtensionManager.GlobalKernel.Resolve<ISessionLoginService>();
			_service.StateChanged += (s, e) => ts.Text = _service.State;
			tc.VerifyCodeLoadComplete += (s, e) =>
			{
				EnterReadyState();
			};
			tc.VerifyCodeLoadFailed += (s, e) =>
			{
				tc.Enabled = true;
				ts.Image = Properties.Resources.tick_16;
				ts.Text = "被验证码无情地抛弃了..请刷新验证码";
			};
			tc.VerifyCodeEnterComplete += (s, e) =>
			{
				CheckAndSubmit();
			};
			tc.EndAutoVc += (s, e) =>
			{
				CheckAndSubmit();
			};
			tc.ResizeParent(this, ProgramConfiguration.Instance.CaptchaZoom);

			chkTmp.CheckedChanged += (s, e) =>
			{
				cbRememberPwd.Enabled = !chkTmp.Checked;
			};
			tsQrLogin.Click += (s, e) =>
			{
				new QrLogin().Show(AppContext.HostForm);
				Close();
			};

			cbUserName.TextChanged += (s, e) =>
			{
				var name = SelectedUserName;
				var pwd = UserKeyDataMap.Current[name].SelectValue(x => x.Password) ?? _session?.Password;
				if (string.IsNullOrEmpty(pwd))
				{
					txtPassword.Text = "";
					cbRememberPwd.Checked = false;
					txtPassword.Enabled = true;
				}
				else
				{
					txtPassword.Text = pwd;
					cbRememberPwd.Checked = true;

					AutoSubmitLogin();
				}
			};
			txtPassword.PasswordChar = Configuration.ProgramConfiguration.Instance.PasswordChar;
			Load += (s, e) => Prepare();

			tsVirutalLogin.Click += (s, e) =>
			{
				var user = SelectedUserName;
				if (user.IsNullOrEmpty())
				{
					this.ShowToast("请选择一个已经登录过的用户。");
					return;
				}
				if (txtPassword.TextLength == 0)
				{
					this.ShowToast("请输入登录密码");
					return;
				}

				Session.OnUserLogined(new Session(user, false) { Password = txtPassword.Text });
				Close();
			};
			FormClosing += (s, e) =>
							{
								pbLogin.Image = null;
							};
		}

		private void btnLogin_Click(object sender, EventArgs e)
		{
			var txt = SelectedUserName;
			if (string.IsNullOrEmpty(txt))
				ToastNotification.Show(this, "请输入用户名");
			else if (!Regex.IsMatch(txt, @"^[\d-a-zA-Z\._@]+$"))
				ToastNotification.Show(this, "用户名不正确");
			else if (txtPassword.Text.IsNullOrEmpty())
				ToastNotification.Show(this, "请输入密码");
			else if (_service.NeedVcLogin && tc.Code.IsNullOrEmpty())
				ToastNotification.Show(this, "请输入验证码");
			else if (!ProgramConfiguration.Instance.EnableConflictLogin && RunTime.SessionManager.IsLogined(txt, _session) && !_relogin)
			{
				DialogResult = DialogResult.OK;
				MainForm.Instance.SelectedSession = RunTime.SessionManager.Find(s => s.UserName == txt);
				Close();
			}
			else
			{
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
				DoLogin();
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
			}
		}

		void CheckAndSubmit()
		{
			if (SelectedUserName.IsNullOrEmpty() || txtPassword.TextLength == 0 || tc.Code.IsNullOrEmpty())
				return;

			DoLogin();
		}

		/// <summary>
		/// 初始化删除用户信息逻辑
		/// </summary>
		void InitUserDelete()
		{
			lnkDelete.DropDownOpening += (s, e) =>
			{
				var txt = SelectedUserName;
				cmDeleteUser.Enabled = UserKeyDataMap.Current.UserKeyData.ContainsKey(txt) && !RunTime.SessionManager.IsLogined(txt);
				cmDeletePassword.Enabled = cmDeleteUser.Enabled && !string.IsNullOrEmpty(UserKeyDataMap.Current[txt].SelectValue(x => x.Password));
			};
			cmDeletePassword.Click += (s, e) =>
			{
				var user = UserKeyDataMap.Current[SelectedUserName];
				if (user != null)
				{
					user.Password = null;
					UserKeyDataMap.Current.Save();
				}
				cbRememberPwd.Checked = false;
				txtPassword.Text = "";
				this.ShowToast("保存的用户密码已经被删除。。。");
			};
			cmDeleteUser.Click += async (s, e) =>
			{
				var name = SelectedUserName;
				if (!await _layer.Confirm("确定要删除此用户吗？所有已保存的资料以及偏好设置等都将会被扫出地球。").ConfigureAwait(true))
					return;
				Profile.Root.TryDeleteUser(name);
				UserKeyDataMap.Current[name] = null;
				if (cbUserName.Items.Contains(cbUserName.Text)) cbUserName.Items.Remove(cbUserName.Text);
				cbRememberPwd.Checked = false;
				txtPassword.Text = "";
				cbUserName.Text = "";
				this.ShowToast("已送回火星，小主请放心。");
			};
			chkAutoVc.AddDataBinding(ProgramConfiguration.Instance, s => s.Checked, s => s.AutoEnterLoginVcCode);

			if (Service.VerifyCodeRecognizeServiceLoader.VerifyCodeRecognizeEngine != null)
			{
				chkAutoVc.Visible = true;
			}
		}



		private void Login_Load(object sender, EventArgs e)
		{
			cbUserName.Items.AddRange(UserKeyDataMap.Current.UserKeyData.OrderByDescending(s => s, new UserKeyDataComparer()).Select(s => string.IsNullOrEmpty(s.Value.DisplayName) ? s.Key : s.Value.DisplayName + " (" + s.Key + ")").ToArray());

			//append images
			InitUserDelete();

			cbUserName.Text = PreSelectUser ?? "";
		}

		string SelectedUserName
		{
			get
			{
				var txt = cbUserName.Text;
				var m = Regex.Match(txt, @"([^\(]+)\)$");

				return m.Success ? m.Groups[1].Value : txt;
			}
		}

		/// <summary>
		/// 获得当前的上下文环境
		/// </summary>
		public Session Context { get; set; }

		public string PreSelectUser { get; set; }
	}
}
