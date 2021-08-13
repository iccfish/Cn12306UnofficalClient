using System;

namespace TOBA.UI.Dialogs.Account
{
	using Autofac;

	using DevComponents.DotNetBar;

	using Passport;

	using Platform;

	using System.Threading.Tasks;

	using TOBA.Account;
	using TOBA.Account.Entities;

	public partial class QrLogin : Office2007Form
	{
		private IQrLogin12306Service _qrLogin12306Service;

		public QrLogin()
		{
			InitializeComponent();

			_qrLogin12306Service = AppContext.ExtensionManager.GlobalKernel.Resolve<IQrLogin12306Service>();
		}

		private void QrLogin_Load(object sender, EventArgs e)
		{
			Run();
		}

		async void DelayCloseAsync()
		{
			await Task.Delay(2500);
			Close();
		}

		async void Run()
		{
			st.Image = Properties.Resources.loading_16_3;
			st.Text = "准备中……";

			var session = await _qrLogin12306Service.InitQrLoginAsync();
			if (session == null)
			{
				this.ShowErrorToastMini("[QR-ERR-01] 会话初始化失败.");
				DelayCloseAsync();
				return;
			}
			if (!await session.InitHttpConfAsync())
			{
				this.ShowErrorToastMini("[QR-ERR-02] HttpConf初始化失败");
				DelayCloseAsync();
				return;
			}
			var (code, msg, image, uuid) = await _qrLogin12306Service.CreateQrImageAsync(session);

			if (code != 0)
			{
				this.ShowErrorToastMini(msg.DefaultForEmpty("[QR-ERR-03] 未知错误"));
				DelayCloseAsync();
				return;
			}

			pbQr.Image = image;

			void SetState(LoginQrState s)
			{
				switch (s)
				{
					case LoginQrState.Valid:
						st.Image = Properties.Resources.cou_16_clock;
						st.Text = "请扫码";
						break;
					case LoginQrState.Success:
						st.Image = Properties.Resources.cou_16_clock;
						st.Text = "扫码成功，请确认登录";
						break;
					case LoginQrState.LoggedIn:
						st.Image = Properties.Resources.tick_16;
						st.Text = "扫码成功";
						break;
					case LoginQrState.Expired:
						st.Image = Properties.Resources.stop_16;
						st.Text = "二维码已失效";
						break;
					case LoginQrState.SystemError:
						st.Image = Properties.Resources.stop_16;
						st.Text = "系统错误";
						break;
					default:
						break;
				}
			}

			var state = LoginQrState.Valid;
			while (state == LoginQrState.Valid && !IsDisposed)
			{
				var (s1, msg1, uamtk) = await _qrLogin12306Service.CheckLoginStateAsync(session, uuid);

				if (s1 != null)
				{
					SetState(s1.Value);

					if (s1 == LoginQrState.LoggedIn)
					{
						break;
					}
					if (s1 == LoginQrState.Expired || s1 == LoginQrState.SystemError)
					{
						this.ShowErrorToastMini(msg1.DefaultForEmpty(st.Text));
						DelayCloseAsync();
						return;
					}
				}
				await Task.Delay(1000);
			}

			st.Image = Properties.Resources.loading_16_3;
			st.Text = "扫码成功，正在初始化中……";

			var uamService = AppContext.ExtensionManager.GlobalKernel.Resolve<IUamAuthService>();
			var (uamOk, uamMsg, userName) = await uamService.AuthTkAsync(session.NetClient, null).ConfigureAwait(true);
			if (uamOk != true)
			{
				this.ShowErrorToastMini(uamMsg);
				DelayCloseAsync();
				return;
			}
			//登录成功，初始化信息
			var accService = session.GetService<IAccountService>();
			var response = await accService.GetQueryInfoResponseAsync();
			if (response == null)
			{
				this.ShowErrorToastMini("未能初始化用户信息，请重试");
				DelayCloseAsync();
				return;
			}

			var username = response.UserInfo.LoginUserInfo.UserName;
			var newSession = new Session(username, false, false, session.NetClient);

			//自动关闭之前登录的
			var sess = RunTime.SessionManager.Find(s => s.UserName == username);
			if (sess != null)
			{
				Session.OnLogout(sess, LogoutReason.UserManually);
			}

			Session.OnUserLogined(newSession);
			Close();
		}
	}
}
