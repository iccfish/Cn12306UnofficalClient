namespace TOBA.UI.Controls.Common
{
	using Data;

	using System;
	using System.ComponentModel;
	using System.Drawing;
	using System.Globalization;
	using System.Linq;
	using System.Reflection;
	using System.Windows.Forms;

	class DateComboBox : ComboBox
	{
		SolidBrush _dateBrush = new SolidBrush(Color.RoyalBlue);
		SolidBrush _lunisolaryBrush = new SolidBrush(Color.Green);
		SolidBrush _sellTipBrush = new SolidBrush(Color.Gray);
		SolidBrush _bgBrush, _controlBrush;
		Font _dateFont, _lunisolarFont, _sellTipFont;
		ChineseLunisolarCalendar _lunisolarCalendar;
		DateTime _maxDate;
		bool _studentTicket;
		Font _dropBaseFont = new Font("微软雅黑", 12.0F, FontStyle.Regular, GraphicsUnit.Pixel);

		static string[] _weekDayNames = new[] { "日", "一", "二", "三", "四", "五", "六" };

		class DateWrap
		{
			static ChineseLunisolarCalendar _lunisolarCalendar = new ChineseLunisolarCalendar();
			DateTime _date;

			public DateTime Date { get { return _date; } }

			private DateWrap() { }

			public static implicit operator DateTime(DateWrap obj)
			{
				return obj._date;
			}

			public static implicit operator DateWrap(DateTime date)
			{
				return new DateWrap() { _date = date };
			}

			public override string ToString()
			{
				return _date.ToString("yy年MM月dd日") + "星期" + _weekDayNames[(int)_date.DayOfWeek] + " " + _lunisolarCalendar.GetLunisolarMonth(_date) + _lunisolarCalendar.GetLunisolarDay(_date);
			}

			/// <summary>
			/// 获得或设置开始起售时间
			/// </summary>
			public DateTime? BeginSellDate { get; set; }
		}

		public event EventHandler StudentTicketChanged;

		/// <summary>
		/// 引发 <see cref="StudentTicketChanged" /> 事件
		/// </summary>
		protected virtual void OnStudentTicketChanged()
		{
			var handler = StudentTicketChanged;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool StudentTicket
		{
			get { return _studentTicket; }
			set
			{
				if (value == _studentTicket)
					return;

				_studentTicket = value;
				OnStudentTicketChanged();
				RefreshSellStatus();
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DateTime MaxDate
		{
			get { return _maxDate; }
			set
			{
				if (value == _maxDate)
					return;

				_maxDate = value;
				OnMaxDateChanged();
			}
		}

		public event EventHandler MaxDateChanged;

		/// <summary>
		/// 引发 <see cref="MaxDateChanged" /> 事件
		/// </summary>
		protected virtual void OnMaxDateChanged()
		{
			var handler = MaxDateChanged;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		public DateComboBox()
		{
			if (Program.IsRunning)
			{
				var date = DateTime.Now.Date;

				_lunisolarCalendar = new ChineseLunisolarCalendar();
				Items.AddRange(Enumerable.Range(0, 100).Select(s => (object)(DateWrap)date.AddDays(s)).ToArray());

				DrawMode = DrawMode.OwnerDrawVariable;
				MeasureItem += DateComboBox_MeasureItem;
				DrawItem += DateComboBox_DrawItem;
				DropDownHeight = 470;
				DropDownStyle = ComboBoxStyle.DropDownList;
				DoubleBuffered = true;
				SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

				RefreshSellStatus();
			}
		}


		void RefreshSellStatus()
		{
			MaxDate = ParamData.GetMaxTicketDate(StudentTicket);
			foreach (var date in Items.Cast<DateWrap>())
			{
				if (date <= MaxDate)
					date.BeginSellDate = null;
				else
					date.BeginSellDate = ParamData.GetBeginSellTime(date.Date, StudentTicket);
			}
			Invalidate();
		}

		private void DateComboBox_DrawItem(object sender, DrawItemEventArgs e)
		{
			var index = e.Index;
			if (index >= Items.Count)
			{
				return;
			}

			var g = e.Graphics;
			var basicOffset = e.Bounds.Top;
			if (e.State.HasFlag(DrawItemState.ComboBoxEdit))
			{
				var str = SelectedIndex == -1 ? "请选择购票日期" : Items[SelectedIndex].ToString();
				var offsety = (e.Bounds.Height - 16) / 2 + e.Bounds.Top;

				//绘制标题
				g.DrawImage(Properties.Resources.calendar_16, new Point(3, offsety));
				g.DrawString(str, DefaultFont, _controlBrush ?? (_controlBrush = new SolidBrush(DefaultForeColor)), new Point(3 + 16 + 5, offsety + 2));

				return;
			}
			if (index == -1)
			{
				return;
			}

			var item = (DateWrap)Items[e.Index];
			var date = (DateTime)item;
			var nothover = !(e.State.HasFlag(DrawItemState.HotLight) || e.State.HasFlag(DrawItemState.Selected));

			if (_bgBrush == null)
				_bgBrush = new SolidBrush(BackColor);

			//起售？
			var canBuy = item.BeginSellDate == null;
			e.DrawBackground();
			//g.TranslateTransform(0F, -basicOffset);

			//图标
			g.DrawImage(canBuy ? Properties.Resources.cou_32_calendar : Properties.Resources.cou_32_clock, new PointF(5, 5 + basicOffset));

			//标题
			if (_dateFont == null)
			{
				_dateFont = new Font(_dropBaseFont.FontFamily, 14.0F, FontStyle.Bold, GraphicsUnit.Pixel);
			}
			g.DrawString(date.ToString("yy年MM月dd日") + " 星期" + _weekDayNames[(int)date.DayOfWeek], _dateFont, nothover ? _dateBrush : _bgBrush, new Point(37, 5 + basicOffset));
			//农历
			if (_lunisolarFont == null)
			{
				_lunisolarFont = new Font(_dropBaseFont.FontFamily, 12.0F, FontStyle.Regular, GraphicsUnit.Pixel);
			}
			g.DrawString(_lunisolarCalendar.GetLunisolarYear(date) + "[" + _lunisolarCalendar.GetShengXiao(date) + "]" + "年" + _lunisolarCalendar.GetLunisolarMonth(date) + _lunisolarCalendar.GetLunisolarDay(date), _lunisolarFont, nothover ? _lunisolaryBrush : _bgBrush, new Point(37, 25 + basicOffset));
			//不可购买
			if (!canBuy)
			{
				if (_sellTipFont == null)
				{
					_sellTipFont = new Font(_dropBaseFont.FontFamily, 12.0F, GraphicsUnit.Pixel);
				}
				g.DrawString(item.BeginSellDate.Value.ToString("yy年MM月dd日起售"), _sellTipFont, nothover ? _sellTipBrush : _bgBrush, new Point(5, 42 + basicOffset));
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Bindable(true)]
		[Obfuscation(Exclude = false, Feature = "-rename")]
		public DateTime Date
		{
			get
			{
				if (SelectedIndex == -1)
					return DateTime.MinValue;

				return ((DateWrap)(Items[SelectedIndex]));
			}
			set
			{
				for (int i = 0; i < Items.Count; i++)
				{
					if ((DateWrap)Items[i] == value)
					{
						if (SelectedIndex != i)
							SelectedIndex = i;
						break;
					}
				}
			}
		}

		[Obfuscation(Exclude = false, Feature = "-rename")]
		public event EventHandler DateChanged;

		protected virtual void OnDateChanged()
		{
			var handler = DateChanged;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		protected override void OnSelectedItemChanged(EventArgs e)
		{
			base.OnSelectedItemChanged(e);
			//OnDateChanged();
		}

		/// <summary>
		/// 引发 <see cref="E:System.Windows.Forms.ComboBox.SelectedIndexChanged"/> 事件。
		/// </summary>
		/// <param name="e">包含事件数据的 <see cref="T:System.EventArgs"/>。</param>
		protected override void OnSelectedIndexChanged(EventArgs e)
		{
			base.OnSelectedIndexChanged(e);
			OnDateChanged();
		}

		// [ICON] 15年02月15日
		//        正月初一
		//        15年02月11日起售

		private void DateComboBox_MeasureItem(object sender, MeasureItemEventArgs e)
		{
			e.ItemWidth = 100;
			e.ItemHeight = 5    //padding
				+ 5             // padding-bottom
				+ 32            // row-content
				+ (e.Index < Items.Count && e.Index >= 0 && ((DateWrap)Items[e.Index]).BeginSellDate == null ? 0 : 5 + 20); //sell tip
		}
	}
}
