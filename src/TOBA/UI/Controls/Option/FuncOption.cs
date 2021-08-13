using System;
using System.Windows.Forms;

namespace TOBA.UI.Controls.Option
{
	using Configuration;

	using Dialogs;

	partial class FuncOption : OptionConfigForm.AbstractOptionConfigUI
	{
		public FuncOption()
		{
			InitializeComponent();

			DisplayText = "功能控制";
			BigImage = Properties.Resources.onebit2_18_32;
			Image = Properties.Resources.onebit2_18_16;
			Description = "启用或关闭内置功能，以及相关设置";

			this.Load += FuncOption_Load;
		}

		private void FuncOption_Load(object sender, EventArgs e)
		{
			var fo = FuncConfiguration.Instance;

			hbAutoCheckOption.AddDataBinding(hbAutoCheckEnable, s => s.Enabled, s => s.Checked);
			hbAutoCheckEnable.AddDataBinding(fo, s => s.Checked, s => s.EnableHbStatusAutoCheck);
			hbAutoCheckOptionInterval.Value = fo.HbStateQueryInterval;
			hbAutoCheckOptionInterval.ValueChanged += (o, args) => fo.HbStateQueryInterval = (int)hbAutoCheckOptionInterval.Value;

			priceAutoQueryEnable.AddDataBinding(fo, s => s.Checked, s => s.EnableTicketPriceAutoQuery);
			priceAutoQueryOption.AddDataBinding(priceAutoQueryEnable, s => s.Enabled, s => s.Checked);
			priceAutoQueryInterval.Value = fo.TicketPriceQuerySleepTimeNormal;
			priceAutoQueryInterval.ValueChanged += (o, args) => fo.TicketPriceQuerySleepTimeNormal = (int)priceAutoQueryInterval.Value;
		}
	}
}
