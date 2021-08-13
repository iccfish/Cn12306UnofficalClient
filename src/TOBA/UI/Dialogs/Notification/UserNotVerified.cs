using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TOBA.UI.Dialogs.Notification
{
	internal partial class UserNotVerified : DialogBase, IOperation
	{
		public UserNotVerified()
		{
			InitializeComponent();

			Load += UserNotVerified_Load;
		}

		void UserNotVerified_Load(object sender, EventArgs e)
		{
			lblUser.Text = "登录的用户名是：" + Session.UserName;
		}
	}
}
