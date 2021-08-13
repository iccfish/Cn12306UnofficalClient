using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.UI.Controls.Query
{
	using System.Drawing;
	using System.Windows.Forms;

	using Entity;

	class CalendarQueryLoop : FlowLayoutPanel, IOperation
	{
		public QueryParam QueryParam { get; set; }

		public CalendarQueryLoop(Session session, QueryParam queryParam)
		{
			QueryParam = queryParam;
			Size = new Size(350, 200);
			BorderStyle = BorderStyle.FixedSingle;
			BackColor = SystemColors.Window;
			AutoScroll = true;

			Session = session;
		}

		public DateTime MinDate { get; private set; }

		public DateTime MaxDate { get; private set; }

		public void Refresh(string from, string to, DateTime minDate, DateTime maxDate)
		{
			minDate = minDate.Date;
			maxDate = maxDate.Date;
			SuspendLayout();
			Controls.Clear();

			var curDate = minDate;
			while (curDate <= maxDate)
			{
				var opened = QueryParam.DepartureDate == curDate || QueryParam.AlternativeDate.Contains(curDate);
				var btn = new Button()
				{
					Enabled = !opened,
					Text = curDate.ToString("MM-dd"),
					FlatStyle = FlatStyle.Flat,
					BackColor = opened ? Color.FromArgb(174, 209, 163) : Color.FromArgb(163, 179, 209),
					Size = new Size(100, 28),
					Tag = curDate
				};
				btn.Click += (s, e) =>
				{
					QueryParam.AddAlternativeDate((DateTime)(s as Button).Tag);
					((Button)s).Enabled = false;
					((Button)s).BackColor = Color.FromArgb(174, 209, 163);
				};
				Controls.Add(btn);

				curDate = curDate.AddDays(1);
			}
			ResumeLayout();
		}

		#region Implementation of IOperation

		/// <summary>
		/// 获得或设置上下文环境
		/// </summary>
		public Session Session { get; set; }

		#endregion
	}
}
