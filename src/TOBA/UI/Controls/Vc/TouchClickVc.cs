namespace TOBA.UI.Controls.Vc
{
	using System;
	using System.Collections.Generic;
	using System.Drawing;
	using System.Linq;
	using System.Windows.Forms;

	using Controls;

	using DevComponents.DotNetBar;

	internal partial class TouchClickVc : ControlBase
	{
		List<Point> _points;
		List<string> _strlist;
		List<Panel> _panelList;

		public TouchClickVc()
		{
			InitializeComponent();

			_points = new List<Point>();
			_strlist = new List<string>();
			_panelList = new List<Panel>();
			pb.MouseClick += pb_MouseClick;
		}

		public event EventHandler VcFinished;

		/// <summary>
		/// 引发 <see cref="VcFinished" /> 事件
		/// </summary>
		protected virtual void OnVcFinished()
		{
			var handler = VcFinished;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		void pb_MouseClick(object sender, MouseEventArgs e)
		{
			if (_points.Count >= _strlist.Count)
				return;

			_points.Add(e.Location);
			//add control
			var p = new Panel()
			{
				BackColor = Color.White,
				BorderStyle = BorderStyle.FixedSingle,
				Size = new Size(20, 20)
			};
			var pt = PointToClient(pb.PointToScreen(e.Location));
			pt.Offset(-10, -10);
			p.Location = pt;
			Controls.Add(p);
			p.BringToFront();
			_panelList.Add(p);
			p.Controls.Add(new Label() { Text = _strlist[_points.Count - 1], ForeColor = Color.RoyalBlue, AutoSize = false, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleCenter });

			RefreshStatus();

			if (_points.Count >= _strlist.Count)
			{
				OnVcFinished();
			}
		}


		public void InitVc(Image img, params string[] strList)
		{
			pb.Image = img;
			Size = new Size(Math.Max(320, img.Width), img.Height + ps.Height + panelEx1.Height);

			var bound = Screen.PrimaryScreen.WorkingArea;
			Location = new Point((bound.Width - Width) / 2, (bound.Height - Height) / 2);

			_strlist.Clear();
			_strlist.AddRange(strList);
			_points.Clear();
			foreach (var panel in _panelList)
			{
				Controls.Remove(panel);
			}
			_panelList.Clear();

			//列表更新
			ps.Items.RemoveRange(ps.Items.Cast<BaseItem>().Skip(1).ToArray());
			ps.Items.AddRange(strList.Select(s =>
			{
				var item = (BaseItem)new StepItem(s, s);
				item.Click += item_Click;
				return item;
			}).ToArray());
			RefreshStatus();
		}

		void item_Click(object sender, EventArgs e)
		{
			var item = (StepItem)sender;
			if (!item.HotTracking)
				return;

			//index
			var index = ps.Items.Cast<StepItem>().FindIndex(s => s == item);
			_points.RemoveRange(index - 1, _points.Count - index + 1);
			foreach (var panel in _panelList.Skip(index - 1))
			{
				Controls.Remove(panel);
			}
			_panelList.RemoveRange(index - 1, _panelList.Count - index + 1);

			RefreshStatus();
		}

		void RefreshStatus()
		{
			var charcount = _points.Count;
			for (var i = 0; i < ps.Items.Count; i++)
			{
				var item = ps.Items[i] as StepItem;
				item.HotTracking = i > 0 && i - 1 < charcount;
				item.Value = item.HotTracking || i == 0 ? 100 : 0;
				item.SymbolColor = item.TextColor = !item.HotTracking && i > 0 ? SystemColors.ControlText : Color.White;
			}
		}
	}
}
