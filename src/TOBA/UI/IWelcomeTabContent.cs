using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.UI
{
	using System.Windows.Forms;

	/// <summary>
	/// 欢迎页初始化
	/// </summary>
	interface IWelcomeTabContent
	{
		/// <summary>
		/// 对欢迎页面的内容进行初始化
		/// </summary>
		/// <param name="panel"></param>
		void InitTabLayout(TableLayoutPanel panel);
	}
}
