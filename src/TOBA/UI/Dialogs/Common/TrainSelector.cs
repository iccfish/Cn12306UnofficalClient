namespace TOBA.UI.Dialogs.Common
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Windows.Forms;

	using TOBA.Query.Entity;

	internal partial class TrainSelector : Form
	{


		public TrainSelector(QueryResult queryResult, bool ignorePreSelectFlags = false)
		{
			InitializeComponent();

			ts.IgnorePreSelectFlag = ignorePreSelectFlags;
			ts.Items = queryResult;

			btnOK.Click += (s, e) =>
			{
				if (!ts.CheckedTrains.Any())
				{
					this.Information("还没有选择车次哦。");
				}
				else
				{
					DialogResult = DialogResult.OK;
					Close();
				}
			};
		}

		/// <summary>
		/// 已选择的车次
		/// </summary>
		public IEnumerable<QueryResultItem> SelectedTrains => ts.CheckedTrains;
	}
}
