using System;
using System.Windows.Forms;

namespace TOBA.UI.Controls.Option
{
	using Dialogs;

	using TOBA.Configuration;

	internal partial class UIConfig : OptionConfigForm.AbstractOptionConfigUI
	{
		public UIConfig()
		{
			InitializeComponent();
			Load += UIConfig_Load;

			DisplayText = "UI设置";
			Image = Properties.Resources.monitor_window_16;
			BigImage = Properties.Resources.monitor_window;

		}

		private void UIConfig_Load(object sender, EventArgs ee)
		{
			var c = Configuration.SubmitOrder.Current;
			var pc = ProgramConfiguration.Instance;
			var uc = UiConfiguration.Instance;

			chkTabsOnTop.AddDataBinding(pc, s => s.Checked, s => s.TabsOnTop);
			chkAutoTopMost.AddDataBinding(c, s => s.Checked, s => s.AutoTopMost);
			chkOrderDlgCenter.AddDataBinding(pc, s => s.Checked, s => s.OrderDlgCenterMainform);
			chkShowInfoFrom12306.AddDataBinding(pc, s => s.Checked, s => s.ShowMessageFrom12306);
			chkShowIpBlockWarning.AddDataBinding(pc, s => s.Checked, s => s.ShowIPBlockTip);

			cbCaptchaZoom.SelectedIndex = pc.CaptchaZoom == 10 ? 0 : pc.CaptchaZoom == 15 ? 1 : 2;
			cbCaptchaZoom.SelectedIndexChanged += (s, e) =>
												{
													pc.CaptchaZoom = new[] { 10, 15, 20 }[cbCaptchaZoom.SelectedIndex];
												};
			ckEnableAni.AddDataBinding(uc, s => s.Checked, s => s.EnableAnimation);
			ckEnableAutoArr.AddDataBinding(uc, s => s.Checked, s => s.AutoArrangeOrderDlg);

			epNotify.Visible = RunTime.IsInProfessonalMode;
		}
	}
}
