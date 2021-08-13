namespace TOBA.UI.Controls.Popup
{
	using Controls;

	using System;
	using System.Windows.Forms;

	class PopupUserControlBase : ControlBase
	{
		/// <summary>
		/// 获得或设置拥有此控件的窗体
		/// </summary>
		public Control OwnerControl { get; set; }

		/// <summary>
		/// 显示对话框
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="dlg"></param>
		public void ShowModalDialog<T>(T dlg = null, FormStartPosition startPosition = FormStartPosition.CenterParent) where T : Form
		{
			if (dlg == null)
				dlg = Activator.CreateInstance<T>();

			dlg.ShowDialog(OwnerControl);
		}
	}
}
