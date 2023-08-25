using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TOBA.UI.Dialogs.Vc
{
	using System.Threading.Tasks;

	using Controls.Vc;

	using TOBA.Account;

	partial class RequestVerification : DialogBase
	{
		private ISessionLoginService       _service;
		private SlideVcControl             _vcControl;
		private SmsVc                      _smsVc;
		private TaskCompletionSource<bool> _tcs;
		private Timer                      _timer;

		public Task<bool> UserOpTask => _tcs.Task;

		public string CfSessionId => _vcControl?.CfSessionId;

		public string Sig => _vcControl?.Sig;

		public string RandCode => _smsVc?.RandCode;

		public DateTime SmsTime => _smsVc?.SmsTime ?? DateTime.MinValue;
		public VcType CurrentVcType => (VcType)tabControl1.SelectedIndex;
		public RequestVerification(ISessionLoginService service)
		{
			_service = service;
			InitializeComponent();

			InitSession(service.Session);
			Init();
			DialogResult = DialogResult.Cancel;
		}

		void Init()
		{
			var il = new ImageList()
			{
				ImageSize = new Size(20, 20)
			};
			il.Images.Add(UiUtility.Get20PxImageFrom16PxImg(Properties.Resources.cou_16_unlock));
			il.Images.Add(UiUtility.Get20PxImageFrom16PxImg(Properties.Resources.bubble_16));
			tabControl1.ImageList = il;

			// 添加滑动验证

			// 添加验证码验证
			if (_service.NeedSmsLogin)
			{
				_smsVc = new SmsVc(Session, _service)
				{
					Dock = DockStyle.Fill
				};
				_smsVc.RandCodeReady += (_, _) =>
				{
					DialogResult = DialogResult.OK;
					Close();
				};
				var tpSms = new TabPage("短信验证码")
				{
					ImageIndex = 1,
					Controls   = { _smsVc }
				};

				tabControl1.TabPages.Add(tpSms);
			}
			if (_service.NeedSlideVcLogin)
			{
				_vcControl = new SlideVcControl(Session, _service.SlideVcToken, _service.SlideAppId)
				{
					Dock = DockStyle.Fill
				};
				_vcControl.SlideOk += (_, _) =>
				{
					DialogResult = DialogResult.OK;
					Close();
				};
				var tpSlide = new TabPage("滑动验证")
				{
					ImageIndex = 0,
					Controls   = { _vcControl }
				};
				tabControl1.TabPages.Add(tpSlide);
			}

			_tcs = new TaskCompletionSource<bool>();

			Closing += (_, _) =>
			{
				_tcs?.SetResult(DialogResult == DialogResult.OK);
				_tcs = null;
			};

			//60秒自动关闭
			_timer          = new Timer();
			_timer.Interval = 120 * 1000;
			_timer.Tick += (_, _) =>
			{
				_timer.Stop();
				DialogResult = DialogResult.Retry;
				Close();
			};
			_timer.Start();
		}
	}
}
