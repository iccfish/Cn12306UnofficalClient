using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TOBA.UI.Controls.Misc
{
	internal class TabWelcome : TabPage
	{
		/// <summary>
		/// 创建 <see cref="TabWelcome" />  的新实例(TabWelcome)
		/// </summary>
		public TabWelcome()
		{
			ImageIndex = 0;
			Controls.Add(new TabWelcomeContent
						{
							Dock = DockStyle.Fill
						});
			Text = "欢迎使用";
		}
	}
}
