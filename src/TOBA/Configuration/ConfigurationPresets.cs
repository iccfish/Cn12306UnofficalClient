namespace TOBA.Configuration
{
	using Data;

	class ConfigurationPresets
	{
		public static void Apply(RunningMode mode)
		{
			switch (mode)
			{
				case RunningMode.PreSell:
					ApplyPresellMode();
					break;
				case RunningMode.CatchLeak:
					ApplyCatchLeakMode();
					break;
				case RunningMode.Professional:
					ApplyProfessionalMode();
					break;
			}
		}

		static void ApplyPresellMode()
		{
			var p = ProgramConfiguration.Instance;
			p.AutoRelogin = true;

			//查询设置
			var qc = QueryConfiguration.Current;
			qc.StopQueryWhenFoundTicket = true; //启用查到票后停止其它查询
			qc.SpeedingQueryOnOClock = 1000;    //整点附近加速查询
			qc.TimeoutAutoIncreaseSetting = true;
			qc.QueryTimeout = 20 * 1000;
			qc.UseAnonymousQuery = true;
			qc.IgnoreServerError = true;
			qc.IgnoreAlmostIllegalResult = true;

			//查询视图设置
			var qvc = QueryViewConfiguration.Instance;
			qvc.HideExtraFilterOption = false;
			qvc.HideExtraFilterOption = false;
			qvc.EnableSelltip = true;
			qvc.ShowStartAndEndStation = true;

			var ar = AutoResumeRefreshConfiguration.Instance;
			ar.AutoCloseSubmit = true;
			ar.AutoCloseSubmitIfAutoVcFailed = true;
			ar.AutoCloseSubmitIfNoEnoughTicket = true;
			ar.AutoCloseSubmitIfNotSubmitable = true;
			ar.AutoCloseSubmitIfQueueFailedElse = true;
			ar.AutoCloseSubmitIfSubmitFailed = true;
			ar.AutoCloseSubmitTimeout = 1;
			ar.AutoReloadVc = true;
			ar.AutoReloadVcTime = 1000;

			var nc = NetworkConfiguration.Current;
			nc.AutoRetryOnNetworkError = true;
			nc.AutoReloadDnsLimit = 20;
			nc.NetworkRetryCount = 5;
			nc.RetrySleepTime = 200;
			nc.DisableCdn = true;

			var oc = OrderConfiguration.Instance;
			oc.EnableFastSubmitOrder = true;
			oc.EnableOrderArchive = true;

			ProgramConfiguration.Instance.Mode = RunningMode.PreSell;
			AppContext.HostForm.ShowToast("您已经切换到" + ParamData.ModeName[RunningMode.PreSell] + "，此模式不适合长期稳定挂机刷票，挂机请切换至捡漏模式。", Properties.Resources.cou_16_warning);
		}

		static void ApplyCatchLeakMode()
		{
			var p = ProgramConfiguration.Instance;
			p.AutoRelogin = true;

			//查询设置
			var qc = QueryConfiguration.Current;
			qc.StopQueryWhenFoundTicket = false;    //禁用查到票后停止其它查询
			qc.SpeedingQueryOnOClock = null; //禁用整点附近加速查询
			qc.TimeoutAutoIncreaseSetting = true;
			qc.QueryTimeout = 20 * 1000;
			qc.UseAnonymousQuery = true;
			qc.IgnoreServerError = true;
			qc.IgnoreAlmostIllegalResult = true;

			//查询视图设置
			var qvc = QueryViewConfiguration.Instance;
			qvc.HideExtraFilterOption = false;
			qvc.HideExtraFilterOption = false;
			qvc.EnableSelltip = true;
			qvc.ShowStartAndEndStation = true;

			var ar = AutoResumeRefreshConfiguration.Instance;
			ar.AutoCloseSubmit = true;
			ar.AutoCloseSubmitIfAutoVcFailed = true;
			ar.AutoCloseSubmitIfNoEnoughTicket = true;
			ar.AutoCloseSubmitIfNotSubmitable = true;
			ar.AutoCloseSubmitIfQueueFailedElse = true;
			ar.AutoCloseSubmitIfSubmitFailed = true;
			ar.AutoCloseSubmitTimeout = 1;
			ar.AutoReloadVc = true;
			ar.AutoReloadVcTime = 1000;

			var nc = NetworkConfiguration.Current;
			nc.AutoRetryOnNetworkError = true;
			nc.AutoReloadDnsLimit = 20;
			nc.NetworkRetryCount = 5;
			nc.RetrySleepTime = 1000;
			nc.DisableCdn = true;

			var oc = OrderConfiguration.Instance;
			oc.EnableFastSubmitOrder = true;
			oc.EnableOrderArchive = true;

			ProgramConfiguration.Instance.Mode = RunningMode.CatchLeak;
			AppContext.HostForm.ShowToast("您已经切换到" + ParamData.ModeName[RunningMode.CatchLeak], Properties.Resources.cou_16_warning);
		}

		static void ApplyProfessionalMode()
		{
			ProgramConfiguration.Instance.Mode = RunningMode.Professional;
			var p = ProgramConfiguration.Instance;
			p.AutoRelogin = true;

			//查询设置
			var qc = QueryConfiguration.Current;
			qc.StopQueryWhenFoundTicket = false; //禁用查到票后停止其它查询
			qc.SpeedingQueryOnOClock = null; //禁用整点附近加速查询
			qc.TimeoutAutoIncreaseSetting = true;
			qc.QueryTimeout = 20 * 1000;
			qc.UseAnonymousQuery = true;
			qc.IgnoreServerError = true;
			qc.IgnoreAlmostIllegalResult = true;

			//查询视图设置
			var qvc = QueryViewConfiguration.Instance;
			qvc.HideExtraFilterOption = false;
			qvc.HideExtraFilterOption = false;
			qvc.EnableSelltip = true;
			qvc.ShowStartAndEndStation = true;

			var ar = AutoResumeRefreshConfiguration.Instance;
			ar.AutoCloseSubmit = true;
			ar.AutoCloseSubmitIfAutoVcFailed = true;
			ar.AutoCloseSubmitIfNoEnoughTicket = true;
			ar.AutoCloseSubmitIfNotSubmitable = true;
			ar.AutoCloseSubmitIfQueueFailedElse = true;
			ar.AutoCloseSubmitIfSubmitFailed = true;
			ar.AutoCloseSubmitTimeout = 1;
			ar.AutoReloadVc = true;
			ar.AutoReloadVcTime = 1000;

			var nc = NetworkConfiguration.Current;
			nc.AutoRetryOnNetworkError = true;
			nc.AutoReloadDnsLimit = 20;
			nc.NetworkRetryCount = 5;
			nc.RetrySleepTime = 1000;
			nc.DisableCdn = true;

			var oc = OrderConfiguration.Instance;
			oc.EnableFastSubmitOrder = true;
			oc.EnableOrderArchive = true;
			AppContext.HostForm.ShowToast("您已经切换到" + ParamData.ModeName[RunningMode.Professional], Properties.Resources.cou_16_warning);
		}
	}
}