namespace TOBA.UI.Controls.Common
{
	using System.Windows.Forms;

	public class DataGridViewButtonExtendColumn : DataGridViewButtonColumn
	{
		/// <summary>
		/// 创建 <see cref="DataGridViewButtonExtendColumn" />  的新实例(DataGridViewButtonExtendColumn)
		/// </summary>
		public DataGridViewButtonExtendColumn()
		{
			CellTemplate = new DataGridViewButtonExtendCell();
		}
	}
}
