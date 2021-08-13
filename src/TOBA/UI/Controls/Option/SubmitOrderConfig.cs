using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace TOBA.UI.Controls.Option
{
	using Configuration;

	using Dialogs;

	using Interactive;

	using System.IO;

	internal partial class SubmitOrderConfig : OptionConfigForm.AbstractOptionConfigUI
	{
		public SubmitOrderConfig()
		{
			InitializeComponent();

			DisplayText = "订单设置";
			Image = Properties.Resources.buy_16;
			BigImage = Properties.Resources.cou_32_promotion;
		}

		private void SubmitOrderConfig_Load(object sender, EventArgs e)
		{
			var c = Configuration.SubmitOrder.Current;
			var pc = ProgramConfiguration.Instance;
			var oc = OrderConfiguration.Instance;

			chkAutoCheckTicket.AddDataBinding(c, s => s.Checked, s => s.EnableLiveTicketCheck);
			chkDisableChangeInfoForAutoAdd.AddDataBinding(c, s => s.Checked, s => s.DisableEditNameOfAutoAddedPassenger);
			chkEnableArchive.AddDataBinding(oc, s => s.Checked, s => s.EnableOrderArchive);
			enableFastSubmitOrder.AddDataBinding(oc, s => s.Checked, s => s.EnableFastSubmitOrder);
			chkIgnoreQueueError.AddDataBinding(oc, s => s.Checked, s => s.IgnoreQueueError);
			chkDisableSubmitTicket.AddDataBinding(oc, s => s.Checked, s => s.SubmitFailedNoTicketAutoDisable);
			iDisableSubmitTicketRate.AddDataBinding(oc, s => s.IntValue, s => s.SubmitFailedNoTicketControlRate);
			iDisableSubmitTicketTimeRange.AddDataBinding(oc, s => s.IntValue, s => s.SubmitFailedNoTicketControlTime);
			panel1.AddDataBinding(chkDisableSubmitTicket, s => s.Enabled, s => s.Checked);
			iiAutoRetryLimit.AddDataBinding(oc, s => s.Value, s => s.AutoRetryCountLimit);
			chkTryStopWz.AddDataBinding(oc, s => s.Checked, s => s.TryStopStandTicket);
			chkIgnoreSafeTime.AddDataBinding(oc, s => s.Checked, s => s.IgnoreSafeTime);
			chkTryStopNoTicket.AddDataBinding(oc, s => s.Checked, s => s.TryStopNoTicket);

			var allbrowser = WebBrowserManager.GetWebBrowsers();
			var selectProgramDlg = new OpenFileDialog()
			{
				Title = "选择浏览器主程序...",
				Filter = "浏览器主程序文件(*.exe)|*.exe"
			};
			wbs.Items.AddRange(new object[]
			{
				WebBrowserInfo.SystemDefault,
			}.Concat(allbrowser).Concat(new object[] { WebBrowserInfo.UserDefine }).ToArray());
			if (pc.SubmitOrderBrowser == null)
			{
				wbs.SelectedIndex = 0;
			}
			else
			{
				var idx = wbs.Items.Cast<WebBrowserInfo>().FirstOrDefault(s => s.Name == pc.SubmitOrderBrowser.Name);
				if (idx == null)
				{
					wbs.Items.Insert(wbs.Items.Count - 1, pc.SubmitOrderBrowser);
				}
				else
				{
					wbs.SelectedItem = idx;
				}
			}
			wbs.SelectedIndexChanged += (s, x) =>
			{
				if (wbs.SelectedIndex < 1)
					pc.SubmitOrderBrowser = null;
				else if (wbs.SelectedIndex < wbs.Items.Count - 1)
					pc.SubmitOrderBrowser = wbs.SelectedItem as WebBrowserInfo;
				else
				{
					if (selectProgramDlg.ShowDialog() != DialogResult.OK)
					{
						pc.SubmitOrderBrowser = null;
						wbs.SelectedIndex = 0;
						return;
					}
					pc.SubmitOrderBrowser = new WebBrowserInfo
					{
						Name = Path.GetFileName(selectProgramDlg.FileName),
						Path = selectProgramDlg.FileName
					};
					wbs.Items.Insert(wbs.Items.Count - 1, pc.SubmitOrderBrowser);
					wbs.SelectedItem = pc.SubmitOrderBrowser;
				}
			};
		}
	}
}
