using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TOBA.UI.Dialogs.Account
{
	using DevComponents.DotNetBar;
	using TOBA.Configuration;

	internal partial class MobileNotChecked : Office2007Form, IRequireSessionInit
	{
		public MobileNotChecked(Session session)
		{
			InitializeComponent();

			InitSession(session);

			var pc = ProgramConfiguration.Instance;
			chkEnableNotify.AddDataBinding(pc, s => s.Checked, s => s.NotifyIfMobileNotChecked);
		}

		/// <summary>
		/// 获得或设置上下文环境
		/// </summary>
		public Session Session { get; private set; }

		/// <summary>
		/// 执行初始化
		/// </summary>
		/// <param name="session"></param>
		public void InitSession(Session session)
		{
			Session = session;
			UiUtility.AttachContext(this, session);

			lblTip.Text = $"<b>{session.UserKeyData.DisplayName}</b> ({session.UserName}) 的手机号 <font color='red'><b>{session.UserKeyData.MobileNumber}</b></font> 未通过核验，<br />可能会对订票、更改密码等产生影响，是否现在就核验？";
		}
	}
}
