using System;
using System.Windows.Forms;

using TOBA.Configuration;

namespace TOBA.UI.Controls.Option
{
	using Dialogs;

	internal partial class SubmitAutoResumeConfig : OptionConfigForm.AbstractOptionConfigUI
	{
		public SubmitAutoResumeConfig()
		{
			InitializeComponent();

			DisplayText = "自动重刷设置";
			Image = Properties.Resources.cou_16_refresh;
			BigImage = Properties.Resources.cou_32_refresh;

			Load += SubmitAutoResumeConfig_Load;
		}

		void SubmitAutoResumeConfig_Load(object sender, EventArgs e)
		{
			var cfg = AutoResumeRefreshConfiguration.Instance;
			chkAutoClose.AddDataBinding(cfg, s => s.Checked, s => s.AutoCloseSubmit);
			chkCloseInitFailed.AddDataBinding(cfg, s => s.Checked, s => s.AutoCloseSubmitIfNotSubmitable);
			chkCloseNoTicket.AddDataBinding(cfg, s => s.Checked, s => s.AutoCloseSubmitIfNoEnoughTicket);
			chkCloseSubmitFailed.AddDataBinding(cfg, s => s.Checked, s => s.AutoCloseSubmitIfSubmitFailed);
			chkCloseVcFailed.AddDataBinding(cfg, s => s.Checked, s => s.AutoCloseSubmitIfAutoVcFailed);
			chkTimeout.AddDataBinding(cfg, s => s.Checked, s => s.LimitSubmitTimeNoPerformTime);
			chkCloseQueueElse.AddDataBinding(cfg, s => s.Checked, s => s.AutoCloseSubmitIfQueueFailedElse);
			nudCloseTimeout.AddDataBinding(cfg, s => s.IntValue, s => s.AutoCloseSubmitTimeout);

			chkAutoClose.CheckedChanged += (s, x) =>
			{
				pOptions.Enabled = chkAutoClose.Checked;
			};
			pOptions.Enabled = chkAutoClose.Checked;
		}
	}
}
