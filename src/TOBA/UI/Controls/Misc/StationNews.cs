using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TOBA.UI.Controls.Misc
{
	using Workers;

	internal partial class StationNews : UserControl
	{
		public StationNews()
		{
			InitializeComponent();
			loading1.Hide();

			Load += StationNews_Load;
			lnkRefresh.Click += (s, e) =>
			{
				LoadNews();
			};
		}

		private bool _inload = false;

		async void LoadNews()
		{
			if (_inload)
				return;
			_inload = true;

			loading1.Show();
			loading1.BringToFront();
			loading1.Reset();

			var worker = new ForumNewsGather();
			var result = await worker.LoadAsync();

			if (result != null)
			{
				LoadNews(result);
				loading1.Hide();
			}
			else
			{
				loading1.SetLoadingError();
			}

			_inload = false;
		}

		void StationNews_Load(object sender, EventArgs e)
		{
			if (!Program.IsRunning)
				return;

			linkLabel1.Click += (s, ee) => Shell.StartUrl("https://forum.iccfish.com/");
			timer.Start();
			LoadNews();
		}

		void LoadNews(List<Entity.Web.SystemNotice> list)
		{
			plist.SuspendLayout();
			plist.Controls.Clear();

			var count = 0;
			plist.Controls.AddRange(
				list.Select(s =>
					{
						var title = s.Title.GetSubString(60, "...");
						var l = new LinkLabel
						{
							Text = "[" + (++count).ToString("000") + "] " + title + " (" + s.Date.ToShortDateString() + ")",
							Dock = DockStyle.Top,
							LinkBehavior = LinkBehavior.HoverUnderline,
							AutoSize = true,
							LinkColor = s.Important ? Color.Crimson : Color.RoyalBlue
						};
						l.Links.Clear();
						l.Links.Add(new LinkLabel.Link { LinkData = s.Url, Start = 6, Length = title.Length, Description = s.Title });
						l.LinkClicked += (x, y) => Shell.StartUrl(y.Link.LinkData.ToString());

						toolTip1.SetToolTip(l, s.Title);

						return (Control)l;
					}).
					Reverse().
					ToArray()
			);
			plist.ResumeLayout();
		}

		#region Overrides of Control

		/// <summary>
		/// 引发 <see cref="E:System.Windows.Forms.Control.SizeChanged"/> 事件。
		/// </summary>
		/// <param name="e">包含事件数据的 <see cref="T:System.EventArgs"/>。</param>
		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);
			loading1.Location = new Point((plist.Width - loading1.Width) / 2, (plist.Height - loading1.Height) / 2);
		}

		#endregion

		private void timer_Tick(object sender, EventArgs e)
		{
			LoadNews();
		}
	}
}
