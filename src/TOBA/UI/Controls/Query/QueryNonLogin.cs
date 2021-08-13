using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.UI.Controls.Query
{
	using System.Windows.Forms;
	using DevComponents.DotNetBar;
	using TOBA.Configuration;
	using TOBA.Entity;

	class QueryNonLogin : SuperTabItem
	{
		SuperTabControlPanel _contentPanel;
		SuperTabControl _control;
		Session _session;

		public QueryNonLogin(SuperTabControl parent)
		{
			_session = new Session("temp", true);
			_control = parent;
			_contentPanel = new SuperTabControlPanel()
			{
				TabItem = this,
			};
			AttachedControl = _contentPanel;
			_contentPanel.Controls.Add(new QueryPage(_session, ProgramConfiguration.Instance.DefaultQuery) { Dock = DockStyle.Fill });
			parent.Controls.Add(_contentPanel);
			_control.Tabs.Add(this, 1);

			Text = "查票(未登录)";
			Image = Properties.Resources.testtube_16;
		}
	}
}
