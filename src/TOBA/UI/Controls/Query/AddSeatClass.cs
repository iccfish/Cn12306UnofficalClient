using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TOBA.UI.Controls.Query
{

	using Data;

	using FSLib.Extension;

	internal partial class AddSeatClass : ControlBase
	{
		/// <summary>
		/// 获得或设置当前的查询
		/// </summary>
		public Entity.QueryParam Query { get; set; }

		public AddSeatClass()
		{
			InitializeComponent();

			Load += AddSeatClass_Load;
		}

		void AddSeatClass_Load(object sender, EventArgs e)
		{
			var index = 0;
			//添加席别
			ParamData.SeatType.ForEach(s =>
			{
				var btn = new Button
				{
					Text = s.Value,
					Size = new Size(p.Width - 2, 26),
					Tag = s.Key,
					Location = new Point(1, 27 * (index++)),
					BackColor = Color.White,
					FlatStyle = FlatStyle.Flat
				};
				btn.FlatAppearance.BorderColor = Color.White;
				btn.Click += (x, y) =>
				{
					OnRequestSelectSeat(new GeneralEventArgs<char>((char)(x as Button).Tag));
					(x as Button).Enabled = false;
				};
				p.Controls.Add(btn);
			});
		}

		protected override void OnVisibleChanged(EventArgs e)
		{
			base.OnVisibleChanged(e);

			if (Session == null || Query == null) return;

			p.Controls.Cast<Button>().ForEach(s =>
			{
				s.Enabled = !Query.AutoPreSubmitConfig.SeatList.Contains((char)s.Tag);
			});
		}

		public event EventHandler<GeneralEventArgs<char>> RequestSelectSeat;

		/// <summary>
		/// 引发 <see cref="RequestSelectSeat" /> 事件
		/// </summary>
		/// <param name="ea">包含此事件的参数</param>
		protected virtual void OnRequestSelectSeat(GeneralEventArgs<char> ea)
		{
			var handler = RequestSelectSeat;
			if (handler != null)
				handler(this, ea);
		}
	}
}
