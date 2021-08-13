using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TOBA.UI.Controls.Misc
{
	internal partial class ForumNews : UserControl
	{
		Workers.BlogNewsGather _net;

		public int Fid { get; set; }

		private bool ShouldSerializeText() => false;

		public ForumNews()
		{
			InitializeComponent();
			Load += ForumNews_Load;
			lnkRefresh.Click += (s, e) =>
			{
				loading1.Show();
				loading1.BringToFront();
				loading1.Reset();
				_net.BeginGet();
			};
		}

		void ForumNews_Load(object sender, EventArgs e)
		{
			if (!Program.IsRunning)
				return;

			_net = new Workers.BlogNewsGather()
			{
				Fid = Fid
			};
			_net.DownloadComplete += (s, ex) =>
			{
				if (_net.Success)
				{
					loading1.Hide();
					LoadNews(_net.SystemNotice);
				}
				else
				{
					loading1.SetLoadingError();
				}
			};
			_net.BeginGet();

			linkLabel1.Click += (s, ee) => Shell.StartUrl("https://blog.iccfish.com/");
			timer.Start();
		}

		void LoadNews(List<Entity.Web.SystemNotice> list)
		{
			plist.SuspendLayout();
			plist.Controls.Clear();
			plist.Controls.Add(loading1);

			var count = 0;
			plist.Controls.AddRange(
				list.Select(s =>
					{
						var title = s.Title.GetSubString(60, "...");
						var desc = s.Title + (s.Description.IsNullOrEmpty() ? "" : "\n============================\n" + s.Description);
						var l = new LinkLabel
						{
							Text = "[" + (++count).ToString("000") + "] " + title + " (" + s.Date.ToShortDateString() + ")",
							Dock = DockStyle.Top,
							LinkBehavior = LinkBehavior.HoverUnderline,
							AutoSize = true,
							LinkColor = s.Important ? Color.Crimson : Color.RoyalBlue
						};

						l.Links[0].LinkData = s.Url;
						l.Links.Clear();
						l.Links.Add(new LinkLabel.Link { LinkData = s.Url, Start = 6, Length = title.Length, Description = desc });
						l.LinkClicked += (x, y) => Shell.StartUrl(y.Link.LinkData.ToString());

						toolTip1.SetToolTip(l, desc);

						return (Control)l;
					}).
					Reverse().
					ToArray()
			);
			plist.ResumeLayout();
		}

		private void timer_Tick(object sender, EventArgs e)
		{
			_net.BeginGet();
		}
	}
}
