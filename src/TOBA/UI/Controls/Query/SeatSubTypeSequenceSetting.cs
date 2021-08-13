namespace TOBA.UI.Controls.Query
{
	using Data;

	using DevComponents.DotNetBar;

	using Entity;

	using System;
	using System.Drawing;
	using System.Linq;
	using System.Windows.Forms;

	partial class SeatSubTypeSequenceSetting : ControlBase, IOperation
	{
		private readonly QueryParam _query;

		public SeatSubTypeSequenceSetting(Session session, QueryParam query)
		{
			InitializeComponent();

			base.InitSession(session);
			_query = query;

			this.Load += SeatSubTypeSequenceSetting_Load;
		}

		private void SeatSubTypeSequenceSetting_Load(object sender, System.EventArgs e)
		{
			Init();
			BindEvents();
		}

		void Init()
		{
			var ass = _query.AutoPreSubmitConfig;
			Init(ass.SeatSubTypesHighSpeed, pHr);
			Init(ass.SeatSubTypesBed, pBed);
		}

		void Init(EventList<SubType> subTypes, Panel panel)
		{
			foreach (var t in subTypes)
			{
				AddSubTypeUi(panel, subTypes, t);
			}

			CheckEmpty(panel);
		}

		void AddSubType(Panel target, EventList<SubType> list, SubType type)
		{
			if (list.Count > 15)
			{
				ToastNotification.Show(this, "选那么多席位并没有什么用的呀", Properties.Resources.info_16, 2000, eToastGlowColor.Blue);
				return;
			}

			if (target == pHr && list.Count(s => s == type) >= 2)
			{
				ToastNotification.Show(this, "同席位选择不能超过两次哦", Properties.Resources.info_16, 2000, eToastGlowColor.Blue);
				return;
			}
			list.Add(type);

			AddSubTypeUi(target, list, type);

			CheckEmpty(target);
		}

		void AddSubTypeUi(Panel target, EventList<SubType> list, SubType type)
		{
			var ll = new LinkLabel()
			{
				Text = ParamData.SeatSubTypeDisplayName[type],
				Tag = Tuple.Create(list, type),
				LinkColor = Color.RoyalBlue
			};
			ll.LinkClicked += Ll_LinkClicked;
			target.Controls.Add(ll);
		}

		private void Ll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			var ll = (LinkLabel)sender;
			var (item1, item2) = (Tuple<EventList<SubType>, SubType>)ll.Tag;

			var panel = ((Panel)ll.Parent);
			var index = panel.Controls.IndexOf(ll) - 1;
			panel.Controls.Remove(ll);
			item1.RemoveAt(index);
			CheckEmpty(panel);

			//置为可用
			if (panel == pHr)
			{
				pCHr.Controls.OfType<LinkLabel>().First(s => s.Tag.ToString() == item2.ToString()).Enabled = true;
			}
		}

		void CheckEmpty(Panel target)
		{
			var label = (Label)target.Controls[0];
			label.Visible = target.Controls.Count < 2;
		}

		void BindEvents()
		{
			BindEvents(pCHr, _query.AutoPreSubmitConfig.SeatSubTypesHighSpeed);
			BindEvents(pcBed, _query.AutoPreSubmitConfig.SeatSubTypesBed);
		}

		void BindEvents(Panel panel, EventList<SubType> list)
		{
			var targetPanel = panel.Controls.OfType<Panel>().First();
			panel.Controls.OfType<LinkLabel>().ForEach(s =>
			{
				s.Click += (x, y) =>
				{
					var type = (SubType)Enum.Parse(typeof(SubType), s.Tag.ToString());
					AddSubType(targetPanel, list, type);
					if (targetPanel == pHr && list.Count(_ => _ == type) >= 2)
					{
						s.Enabled = false;
					}
				};
			});
		}
	}
}
