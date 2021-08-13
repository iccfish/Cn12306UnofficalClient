using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TOBA.Configuration;
using TOBA.Profile;

namespace TOBA.UI.Controls.Misc
{
	internal partial class QuickOperation : UserControl
	{
		public QuickOperation()
		{
			InitializeComponent();

			ctxLogin.Opening += (sx, e) =>
			{
				var mc = ctxLogin.Items.OfType<ToolStripMenuItem>();
				var logined = RunTime.SessionManager.Select(s => s.UserName).MapToHashSet();
				var datas = UserKeyDataMap.Current.UserKeyData.Keys.MapToHashSet();
				foreach (var toolStripMenuItem in mc.Where(s => s.Tag != null && !datas.Contains(s.Tag.ToString())).ToArray())
				{
					ctxLogin.Items.Remove(toolStripMenuItem);
				}
				foreach (var un in datas.Except(mc.Where(s => s.Tag != null).Select(s => s.Tag as string).MapToHashSet()).ToArray())
				{
					var u = UserKeyDataMap.Current[un];
					var tsmi = new ToolStripMenuItem() { Tag = un };
					tsmi.Click += (x, y) =>
					{
						AppContext.MainForm.Login((x as ToolStripMenuItem).Tag as string);
					};
					ctxLogin.Items.Insert(0, tsmi);
				}
				foreach (var toolStripMenuItem in mc.Where(s => s.Tag != null))
				{
					var un = toolStripMenuItem.Tag as string;
					var u = UserKeyDataMap.Current[un];
					toolStripMenuItem.Text = string.IsNullOrEmpty(u.DisplayName) ? un : u.DisplayName + " (" + un + ")";
					if (!ProgramConfiguration.Instance.EnableConflictLogin)
					{
						toolStripMenuItem.Enabled = !logined.Contains(un);
						if (!toolStripMenuItem.Enabled)
							toolStripMenuItem.Text += " [已登录]";
					}
				}
				tsLoginNone.Visible = !mc.Any(s => s.Tag != null);
			};

		}

		private void btnLogin_Click(object sender, EventArgs e)
		{
			ctxLogin.Show(btnLogin, new Point(0, btnLogin.Height));
		}

		private void tsNewLogin_Click(object sender, EventArgs e)
		{
			AppContext.MainForm.Login();
		}
	}
}
