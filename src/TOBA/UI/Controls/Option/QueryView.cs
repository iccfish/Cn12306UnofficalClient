using System;
using System.Windows.Forms;

using TOBA.Configuration;

namespace TOBA.UI.Controls.Option
{
	using Dialogs;

	internal partial class QueryView : OptionConfigForm.AbstractOptionConfigUI
	{
		public QueryView()
		{
			InitializeComponent();
			DisplayText = "查询视图设置";
			Image = Properties.Resources.search_16;
			BigImage = Properties.Resources.onebit_02;

		}


		private void QueryViewConfiguration_Load(object sender, EventArgs e)
		{
			chkDoubleClickToClose.AddDataBinding(ProgramConfiguration.Instance, s => s.Checked, s => s.DoubleClickHeaderToCloseTab);
			var source = QueryViewConfiguration.Instance;

			chkHideExtraFilter.AddDataBinding(source, s => s.Checked, s => s.HideExtraFilterOption);
			cbGridLine.AddDataBinding(source, s => s.SelectedIndex, s => s.QueryResultListGridLine);
			chkEnableAutoAddQueryParam.AddDataBinding(source, s => s.Checked, s => s.DoubleClickOnEmptyAreaAddQuery);

			chkEnableSellTip.Visible = Program.EnableSellTip;
			chkEnableSellTip.AddDataBinding(source, s => s.Checked, s => s.EnableSelltip);

			chkIgnoreTempClass.AddDataBinding(QueryConfiguration.Current, s => s.Checked, s => s.IgnoreTempTrainClass);
			ckEnablePresellTip.AddDataBinding(QueryConfiguration.Current, s => s.Checked, s => s.EnableStartStationTip);

			chkShowStartAndEnd.AddDataBinding(source, s => s.Checked, s => s.ShowStartAndEndStation);
			chkHideColumnIfNotSelect.AddDataBinding(source, s => s.Checked, s => s.HideResultColumnIfNotSelected);
		}
	}
}
