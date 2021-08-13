using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace TOBA.UI.Dialogs.Query
{
	using Entity;

	internal partial class QueryManager : DialogBase
	{
		public QueryManager()
		{
			InitializeComponent();
		}

		private void QueryManager_Load(object sender, EventArgs e)
		{
			lstQuery.Items.AddRange(Session.UserProfile.QueryParams.Where(s => s.IsPersistent).Select(s => new ListViewItem(s.Name) { Tag = s }).ToArray());
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (lstQuery.SelectedItems.Count == 0)
				return;

			foreach (ListViewItem selectedItem in lstQuery.SelectedItems.Cast<ListViewItem>().ToArray())
			{
				var query = selectedItem.Tag as QueryParam;

				if (!this.Question("确定要删除情景模式【" + query.Name + "】吗？"))
					continue;

				query.IsPersistent = false;
				selectedItem.Remove();
			}

		}
	}
}
