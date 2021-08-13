using System;
using System.Windows.Forms;

using TOBA.Configuration;
using TOBA.WebLib;

namespace TOBA.UI.Dialogs
{

	using AutoVc;

	using Common;

	internal partial class RequireVcCode : DialogBase
	{
		/// <summary>
		/// 获得或设置是否允许自动识别
		/// </summary>
		public bool EnableAutoVc { get; set; }

		public IVerifyCodeRecognizeResult VcResult { get; private set; }

		public int AutoVcCount { get; set; }

		public RequireVcCode()
		{
			InitializeComponent();

			EnableAutoVc = true;
			this.txtBox.KeyUp += (s, e) =>
			{
				if (txtBox.Text.Length == 4)
				{
					DialogResult = System.Windows.Forms.DialogResult.OK;
					Close();
				}
			};

			verifyCodeBox1.VerifyCodeLoadComplete += (s, e) =>
			{
				ProgressDialog?.SetState(ExecutionState.InterActive, "请输入验证码");
			};
			verifyCodeBox1.EnableAutoVc = Service.VerifyCodeRecognizeServiceLoader.VerifyCodeRecognizeEngine != null && EnableAutoVc && ProgramConfiguration.Instance.AutoEnterLoginVcCode && AutoVcCount < 5;
			verifyCodeBox1.AutoVcFailed += verifyCodeBox1_AutoVcFailed;
			verifyCodeBox1.AutoVcGiveUp += verifyCodeBox1_AutoVcGiveUp;
			verifyCodeBox1.BeginAutoVc += verifyCodeBox1_BeginAutoVc;
			verifyCodeBox1.EndAutoVc += verifyCodeBox1_EndAutoVc;
		}

		void verifyCodeBox1_EndAutoVc(object sender, EventArgs e)
		{
			VcResult = verifyCodeBox1.AutoVcCode;
			txtBox.Text = VcResult.Code;
			DialogResult = DialogResult.OK;
			Close();
		}

		void verifyCodeBox1_BeginAutoVc(object sender, EventArgs e)
		{
			vcStatus.Text = "第" + verifyCodeBox1.AutoVcCount + "次自动识别中...";
		}

		void verifyCodeBox1_AutoVcGiveUp(object sender, EventArgs e)
		{
			vcStatus.Text = "未能自动识别，请手动输入";
		}

		void verifyCodeBox1_AutoVcFailed(object sender, EventArgs e)
		{
			vcStatus.Text = "第" + verifyCodeBox1.AutoVcCount + "次自动识别失败";
		}


		public RandCodeType RandType
		{
			get { return verifyCodeBox1.RandType; }
			set { verifyCodeBox1.RandType = value; }
		}

		/// <summary>
		/// 获得用户填写的验证码
		/// </summary>
		public string Code
		{
			get
			{
				return txtBox.Text;
			}
		}

		private void RequireVcCode_Load(object sender, EventArgs e)
		{
			verifyCodeBox1.LoadVerifyCode();
		}
	}
}
