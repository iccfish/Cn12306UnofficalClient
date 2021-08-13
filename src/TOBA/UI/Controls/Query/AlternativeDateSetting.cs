using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TOBA.UI.Controls.Query
{
	using Entity;

	partial class AlternativeDateSetting : ControlBase
	{
		private QueryParam _query;

		private Font _highlightFont;

		public AlternativeDateSetting()
		{
			InitializeComponent();
		}

		public void InitQuery(QueryParam param)
		{
			_query = param;

			_query.AlternativeDate.ItemChanged += AlternativeDate_ItemChanged;
			var cancelAlternativeDateChanged = _query.AddPropertyChangedEventHandler(s => s.CurrentAlternativeIndex, AlternativeDateChanged);

			this.Disposed += (s, e) =>
			{
				cancelAlternativeDateChanged();
			};

			Refresh();
			RefreshHighlight();
		}

		private void AlternativeDateChanged(object sender, PropertyChangedEventArgs e)
		{
			RefreshHighlight();
		}

		private void AlternativeDate_ItemChanged(object sender, ItemUpdateEventArgs<DateTime> e)
		{
			Refresh();
		}

		void RefreshHighlight()
		{
			var cdate = _query.CurrentDepartureDate;

			foreach (Label lbl in pContainer.Controls)
			{
				var current = cdate == (DateTime)lbl.Tag;

				lbl.ForeColor = current ? Color.White : panel1.ForeColor;
				lbl.BackColor = current ? Color.RoyalBlue : panel1.BackColor;
			}
		}

		void Refresh()
		{
			this.Visible = _query.AlternativeDate.Count > 0;

			var dates = _query.AlternativeDate;

			pContainer.Controls.OfType<Label>().Skip(dates.Count).ToArray().ForEach(s => pContainer.Controls.Remove(s));
			if (pContainer.Controls.Count < dates.Count)
			{
				pContainer.Controls.AddRange(Enumerable.Repeat(0, dates.Count - pContainer.Controls.Count).Select(_ =>
				{
					var lbl = new Label()
					{
						TextAlign = ContentAlignment.MiddleCenter,
						Size = new Size((int)(50 * Program.ScaleX), Height),
						Dock = DockStyle.Left,
						AutoSize = false,
						Text = "",
						Tag = DateTime.Now,
						Cursor = Cursors.Hand
					};
					lbl.Click += (s, e) =>
					{
						_query.AlternativeDate.Remove((DateTime)((Label)s).Tag);
					};

					return (Control)lbl;
				}).ToArray());
			}

			var controls = pContainer.Controls;
			var index = pContainer.Controls.Count - 1;
			foreach (var date in dates)
			{
				if ((DateTime)pContainer.Controls[index].Tag != date)
				{
					//update
					controls[index].Text = date.ToString("MM-dd");
					controls[index].Tag = date;
				}
				index--;
			}

		}

	}
}
