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
	using AutoVc;

	using Controls.Vc;
	using TOBA.Configuration;
	using TOBA.Service;
	using WebLib;

	internal partial class TouchClickVcSimpleForm : DialogBase
	{
		TouchClickSimple vc;

		public TouchClickVcSimpleForm(Session session)
		{
			InitializeComponent();

			vc = new TouchClickSimple(session) { Zoom = ProgramConfiguration.Instance.CaptchaZoom };
			vc.Dock = DockStyle.Fill;
			Controls.Add(vc);
			Size = vc.MinSize + new Size(0, 40);

			vc.VerifyCodeEnterComplete += (s, e) =>
			{
				AutoVcResult = vc.AutoVcCode;
				DialogResult = DialogResult.OK;
				Close();
			};
			vc.EndAutoVc += (s, e) =>
			{
				AutoVcResult = vc.AutoVcCode;
				DialogResult = DialogResult.OK;
				Close();
			};
			vc.VerifyCodeLoadComplete += (s, e) =>
			{
				WindowState = FormWindowState.Normal;
				BringToFront();
				Activate();
			};

			InitSession(session);
			Load += (s, e) =>
			{
				vc.OkButton.Focus();
				LoadCode();
			};
			AcceptButton = vc.OkButton;

			KeyPress += TouchClickVcSimpleForm_KeyPress;
		}

		private void TouchClickVcSimpleForm_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == ' ' || e.KeyChar == '\n')
			{
				e.Handled = true;

				AutoVcResult = vc.AutoVcCode;
				DialogResult = DialogResult.OK;
				Close();
			}
		}

		/// <summary>
		/// Gets or sets the type of the rand code.
		/// </summary>
		/// <value>The type of the rand code.</value>
		public RandCodeType RandCodeType
		{
			get { return vc.RandType; }
			set { vc.RandType = value; }
		}

		/// <summary>
		/// 加载验证码
		/// </summary>
		public void LoadCode()
		{
			vc.LoadVerifyCode();
		}

		public string Code => vc.Code;

		public bool EnableAutoVc
		{
			get { return vc.EnableAutoVc; }
			set { vc.EnableAutoVc = value; }
		}

		/// <summary>
		/// 获得当前的结果是否是由自动打码而来的
		/// </summary>
		public IVerifyCodeRecognizeResult AutoVcResult { get; private set; }
	}
}
