using System;
using System.Linq;
using System.Windows.Forms;

namespace TOBA.UI.Dialogs.Misc
{
	public partial class AboutMe : Form
	{
		public AboutMe()
		{
			InitializeComponent();
			Load += AboutMe_Load;
		}

		private void AboutMe_Load(object sender, EventArgs e)
		{
			panel1.Controls.OfType<LinkLabel>().ForEach(s =>
			{
				s.Click += S_Click;
			});

		}


		private void S_Click(object sender, EventArgs e)
		{
			var link = sender as LinkLabel;
			Shell.StartUrl(link.Tag as string);
		}
	}
}
