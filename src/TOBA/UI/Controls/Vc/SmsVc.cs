using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TOBA.UI.Controls.Vc
{
	using System.Threading.Tasks;

	using Configuration;

	using FSLib.Network.Http;

	using TOBA.Account;

	partial class SmsVc : ControlBase
	{
		private readonly ISessionLoginService _sessionLoginService;

		public SmsVc(Session session, ISessionLoginService sessionLoginService)
		{
			_sessionLoginService = sessionLoginService;
			InitializeComponent();

			InitSession(session);

			//加载用户信息
			txtAppendix.Text =  session.UserKeyData.IdLast4 ?? "";
			Load             += SmsVc_Load;
			btnGetCode.Click += (_, _) => GetSmsCodeAsync();
			txtCode.TextChanged += (_, _) =>
			{
				if (txtCode.TextLength == 6)
					OnRandCodeReady();
			};
			txtAppendix.TextChanged += (_, _) =>
			{
				if (txtAppendix.TextLength == 4)
					btnGetCode.PerformClick();
			};
		}

		private void SmsVc_Load(object sender, EventArgs e)
		{
			if (txtAppendix.TextLength == 4 && ProgramConfiguration.Instance.AutoSendLoginVerifySms)
			{
				GetSmsCodeAsync();
			}
		}

		private DateTime _smsTime;
		public  DateTime SmsTime { get; private set; }
		async Task GetSmsCodeAsync()
		{
			if (txtAppendix.TextLength != 4)
			{
				this.ShowInfoToastMini("请输入身份证号后四位哦 =_=!");
				return;
			}
			Session.UserKeyData.IdLast4 = txtAppendix.Text;

			btnGetCode.Text    = "";
			btnGetCode.Image   = Properties.Resources._16px_loading_1;
			btnGetCode.Enabled = false;

			var client = Session.NetClient;
			var smsService = Session.GetService<ISmsService>();
			var (code, msg) = await smsService.SendLoginVerifySmsAsync(client, _sessionLoginService.UserName, Session.UserKeyData.IdLast4);

			if (code != 0 || !msg.Contains("验证码成功"))
			{
				if (msg.Contains("验证码有误"))
			{
					msg = "未能获得验证码，请重试";
				}
				this.ShowErrorToastMini($"发送验证码失败：{msg}");
				await DelayEnableButtonAsync(0);
			}
			else
			{
				SmsTime = DateTime.Now;
				this.ShowInfoToastMini(msg);

					//保存
					Session.UserKeyData.IdLast4 = txtAppendix.Text;

					txtCode.Clear();
					txtCode.Focus();
					DelayEnableButtonAsync();
			}
		}

		async Task DelayEnableButtonAsync(int time = 60)
		{
			btnGetCode.Image = Properties.Resources.clock_16;
			while (time-- > 0)
			{
				btnGetCode.Text = time.ToString() + "秒";
				await Task.Delay(1000);
			}
			btnGetCode.Image   = Properties.Resources.bubble_16;
			btnGetCode.Enabled = true;
			btnGetCode.Text    = "发送";
		}

		public string RandCode => txtCode.Text;

		public event EventHandler RandCodeReady;

		protected virtual void OnRandCodeReady() { RandCodeReady?.Invoke(this, EventArgs.Empty); }
	}
}
