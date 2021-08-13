using System.Windows.Forms;

using TOBA.Configuration;

namespace TOBA.UI.Controls.Option
{
	using DevComponents.DotNetBar;

	using Dialogs;

	internal partial class QueryConfig : OptionConfigForm.AbstractOptionConfigUI
	{
		public QueryConfig()
		{
			InitializeComponent();

			DisplayText = "查询设置";
			Image = Properties.Resources.diagram_16;
			BigImage = Properties.Resources.cou_32_process;

			Init();
		}

		void Init()
		{
			var qc = Configuration.QueryConfiguration.Current;
			chkAutoStopAllWhenFound.AddDataBinding(qc, s => s.Checked, s => s.StopQueryWhenFoundTicket);

			nudSleepTime.Value = qc.QuerySleep;
			nudSleepTimeE.Value = qc.QuerySleepError;
			nudAutoDelayOClock.Value = (decimal)qc.AutoDelayAfterOClock;
			nudSleepTime.ValueChanged += (ss, ee) =>
			{
				toQuickWarning.Visible = nudSleepTime.Value < 3.0m;
				qc.QuerySleep = nudSleepTime.Value;
			};
			nudSleepTimeE.ValueChanged += (ss, ee) =>
			{
				qc.QuerySleepError = nudSleepTimeE.Value;
			};
			toQuickWarning.Visible = nudSleepTime.Value < 3.0m;
			nudAutoDelayOClock.ValueChanged += (ss, ee) =>
			{
				qc.AutoDelayAfterOClock = (double)nudAutoDelayOClock.Value;
			};
			chkIgnoreServerError.AddDataBinding(qc, s => s.Checked, s => s.IgnoreServerError);
			chkAnonymousQuery.AddDataBinding(qc, s => s.Checked, s => s.UseAnonymousQuery);
			nudAqRate.Value = qc.AnonymousQueryRate;
			nudAqRate.ValueChanged += (ss, ee) =>
			{
				qc.AnonymousQueryRate = (int)nudAqRate.Value;
			};

			chkIngoreAlmostIllegal.AddDataBinding(qc, s => s.Checked, s => s.IgnoreAlmostIllegalResult);
			if (ApiConfiguration.Instance.DisableIllegalDetect)
				chkIngoreAlmostIllegal.Enabled = false;
			chkAlwaysSendQueryLog.AddDataBinding(qc, s => s.Checked, s => s.AlwaysSendingQueryLog);
			//chkAlwaysSendQueryLog.Visible = ApiConfiguration.Instance.EnableAlwaysSendQueryLog;
			chkQueryProtector.AddDataBinding(qc, s => s.Checked, s => s.EnableSpeedProtect);

			//整点附近加速查询
			chkBoostSpeed.Checked = qc.SpeedingQueryOnOClock != null;
			nudBoostSpeed.AddDataBinding(chkBoostSpeed, s => s.Enabled, s => s.Checked);
			nudBoostSpeed.Value = qc.SpeedingQueryOnOClock ?? 1500;

			chkBoostSpeed.CheckedChanged += (s, ex) =>
			{
				if (!chkBoostSpeed.Checked)
					qc.SpeedingQueryOnOClock = null;
				else
				{
					qc.SpeedingQueryOnOClock = (int)nudBoostSpeed.Value;
					ShowWarning();
				}
			};
			nudBoostSpeed.ValueChanged += (s, ex) =>
			{
				qc.SpeedingQueryOnOClock = (int)nudBoostSpeed.Value;
			};
			nudQueryTimeout.Value = QueryConfiguration.Current.QueryTimeout / 1000;
			chkAutoIncreaseTimeout.AddDataBinding(QueryConfiguration.Current, s => s.Checked, s => s.TimeoutAutoIncreaseSetting);


			//查询时间随机化
			chkSleepRandom.Checked = qc.RandomQueryDelay != null;
			nudSleepRandom.Value = qc.RandomQueryDelay ?? 1500;
			nudSleepRandom.AddDataBinding(chkSleepRandom, s => s.Enabled, s => s.Checked);
			chkSleepRandom.CheckedChanged += (_1, _2) =>
			{
				qc.RandomQueryDelay = chkSleepRandom.Checked ? (int?)nudSleepRandom.Value : null;
			};
			nudSleepRandom.ValueChanged += (_1, _2) =>
			{
				qc.RandomQueryDelay = chkSleepRandom.Checked ? (int?)nudSleepRandom.Value : null;
			};

			//自动查询候补
			var fo = FuncConfiguration.Instance;
			chkAutoDetectNOSelectedTrainsHb.AddDataBinding(fo, s => s.Enabled, s => s.EnableHbStatusAutoCheck);
			chkAutoDetectSelectedTrainsHb.AddDataBinding(fo, s => s.Enabled, s => s.EnableHbStatusAutoCheck);
			nudDetectHbNonSelectInterval.AddDataBinding(qc, s => s.Enabled, s => s.DetectAllHbQueue);
			nudDetectHbSelectInterval.AddDataBinding(qc, s => s.Enabled, s => s.AutoDetectHbQueue);
			nudDetectHbNonSelectInterval.AddDataBinding(qc, s => s.Value, s => s.HbQueueInNonSelectedTrainsInterval);
			nudDetectHbSelectInterval.AddDataBinding(qc, s => s.Value, s => s.HbQueueInSelectedTrainsInterval);
			chkAutoDetectNOSelectedTrainsHb.AddDataBinding(qc, s => s.Checked, s => s.DetectAllHbQueue);
			chkAutoDetectSelectedTrainsHb.AddDataBinding(qc, s => s.Checked, s => s.AutoDetectHbQueue);
			chkNotifyMeIfHbOk.AddDataBinding(qc, s => s.Checked, s => s.NotifyWhenHbAvailable);
		}

		void ShowWarning()
		{
			ToastNotification.Show(FindForm(), "请谨慎选择加速查询，过高的速度可能会导致IP被封。局域网用户请注意。", Properties.Resources.cou_16_warning, 5000);
		}

		public override bool Save()
		{
			QueryConfiguration.Current.QueryTimeout = (int)(nudQueryTimeout.Value * 1000);

			return base.Save();
		}
	}
}
