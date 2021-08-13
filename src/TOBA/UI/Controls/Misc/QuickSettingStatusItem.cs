using System;
using System.Collections.Generic;

namespace TOBA.UI.Controls.Misc
{
	using Configuration;

	using Data;

	using System.Drawing;
	using System.Windows.Forms;

	class QuickSettingStatusItem : ToolStripDropDownButton
	{
		internal QuickSettingStatusItem()
		{
			InitializeComponent();
			if (Program.IsRunning)
				Init();
		}

		static Dictionary<RunningMode, Image> _icons = new Dictionary<RunningMode, Image>()
		{
			[RunningMode.PreSell] = Properties.Resources.cou_16_clock,
			[RunningMode.CatchLeak] = Properties.Resources.cou_16_search,
			[RunningMode.Professional] = Properties.Resources.cou_16_user
		};
		private ToolStripMenuItem tsModePre;
		private ToolStripMenuItem tsModeLeak;
		private ToolStripMenuItem tsModePro;
		private ToolStripSeparator toolStripSeparator2;
		static Dictionary<RunningMode, Color> _colors = new Dictionary<RunningMode, Color>()
		{
			[RunningMode.PreSell] = Color.MediumVioletRed,
			[RunningMode.CatchLeak] = Color.DarkOrchid,
			[RunningMode.Professional] = Color.Brown
		};


		void RefreshText()
		{
			var mode = ProgramConfiguration.Instance.Mode;

			Image = _icons[mode];
			ForeColor = _colors[mode];
			Text = $"{ParamData.ModeName[mode]}[{QueryConfiguration.Current.QuerySleep:#0.0}秒]";
		}

		void RefreshMode()
		{
			RefreshText();

			tsModeLeak.Checked = tsModePre.Checked = tsModePro.Checked = false;
			tsAutoStopOther.Enabled = false;
			switch (ProgramConfiguration.Instance.Mode)
			{
				case RunningMode.PreSell:
					tsModePre.Checked = true;
					break;
				case RunningMode.CatchLeak:
					tsModeLeak.Checked = true;
					break;
				case RunningMode.Professional:
					tsAutoStopOther.Enabled = true;
					tsModePro.Checked = true;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		void Init()
		{
			tsModePre.ForeColor = _colors[RunningMode.PreSell];
			tsModeLeak.ForeColor = _colors[RunningMode.CatchLeak];
			tsModePro.ForeColor = _colors[RunningMode.Professional];
			RefreshMode();

			void HandleChangeSleep(object sender, EventArgs e)
			{
				var time = (int)((ToolStripMenuItem)sender).Tag;
				QueryConfiguration.Current.QuerySleep = time;
			}

			for (int i = 1; i <= 15; i++)
			{
				var item = new ToolStripMenuItem($"{i}秒") { Tag = i };
				item.Click += HandleChangeSleep;
				DropDownItems.Add(item);
			}

			QueryConfiguration.Current.PropertyChanged += (s, e) =>
			{
				if (e.PropertyName == nameof(QueryConfiguration.QuerySleep))
					RefreshText();
				if (e.PropertyName == nameof(QueryConfiguration.StopQueryWhenFoundTicket))
					tsAutoStopOther.Checked = QueryConfiguration.Current.StopQueryWhenFoundTicket;
			};
			ProgramConfiguration.Instance.ModeChanged += (s, e) =>
			{
				RefreshMode();
			};
			tsAutoStopOther.Checked = QueryConfiguration.Current.StopQueryWhenFoundTicket;
			tsAutoStopOther.Click += (_1, _2) => { QueryConfiguration.Current.StopQueryWhenFoundTicket = !QueryConfiguration.Current.StopQueryWhenFoundTicket; };
			tsModeLeak.Click += (_1, _2) => ConfigurationPresets.Apply(RunningMode.CatchLeak);
			tsModePre.Click += (_1, _2) => ConfigurationPresets.Apply(RunningMode.PreSell);
			tsModePro.Click += (_1, _2) => ConfigurationPresets.Apply(RunningMode.Professional);
		}

		private ToolStripMenuItem tsAutoStopOther;
		private ToolStripSeparator toolStripSeparator1;

		private void InitializeComponent()
		{
			this.tsAutoStopOther = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsModePre = new System.Windows.Forms.ToolStripMenuItem();
			this.tsModeLeak = new System.Windows.Forms.ToolStripMenuItem();
			this.tsModePro = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			// 
			// tsAutoStopOther
			// 
			this.tsAutoStopOther.Name = "tsAutoStopOther";
			this.tsAutoStopOther.Size = new System.Drawing.Size(218, 22);
			this.tsAutoStopOther.Text = "查到票时自动停止其它查询";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(215, 6);
			// 
			// tsModePre
			// 
			this.tsModePre.Image = global::TOBA.Properties.Resources.cou_16_clock;
			this.tsModePre.Name = "tsModePre";
			this.tsModePre.Size = new System.Drawing.Size(218, 22);
			this.tsModePre.Text = "本日起售模式";
			// 
			// tsModeLeak
			// 
			this.tsModeLeak.Image = global::TOBA.Properties.Resources.cou_16_search;
			this.tsModeLeak.Name = "tsModeLeak";
			this.tsModeLeak.Size = new System.Drawing.Size(218, 22);
			this.tsModeLeak.Text = "捡漏模式";
			// 
			// tsModePro
			// 
			this.tsModePro.Image = global::TOBA.Properties.Resources.cou_16_user;
			this.tsModePro.Name = "tsModePro";
			this.tsModePro.Size = new System.Drawing.Size(218, 22);
			this.tsModePro.Text = "熟练抢票模式";
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(215, 6);
			// 
			// QuickSettingStatusItem
			// 
			this.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.tsModePre,
			this.tsModeLeak,
			this.tsModePro,
			this.toolStripSeparator2,
			this.tsAutoStopOther,
			this.toolStripSeparator1});
			this.ToolTipText = "快速调整查票设置";

		}
	}
}
