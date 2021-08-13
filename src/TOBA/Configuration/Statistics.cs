using System;

namespace TOBA.Configuration
{
	using Query;

	internal class Statistics : ConfigurationBase
	{
		static Statistics _current;

		public static Statistics Current
		{
			get
			{
				if (_current == null)
				{
					_current = AppContext.ExtensionManager.ConfigurationProvider.LoadConfiguration<Statistics>("Statistics");
				}

				return _current;
			}
		}

		public Statistics()
		{
			AutoSave = false;
			SaveTimeSpan = new TimeSpan(0, 10, 0);
			MachineID = Guid.NewGuid();
			FirstStartupTime = DateTime.Now;
		}

		public long StartupCount { get; set; }

		public long WebRequestCount { get; set; }

		public long QueryCount { get; set; }

		public long SubmitCount { get; set; }

		public long SubmitSuccessCount { get; set; }

		public long LoginCount { get; set; }

		public Guid MachineID { get; set; }

		public DateTime FirstStartupTime { get; set; }

		public DateTime LastStartTime { get; set; }

		public DateTime LastShutdownTime { get; set; }

		public TimeSpan RunningTime { get; set; }


		public QueryInterfaceStatus QueryInterfaceStatus { get; set; } = new QueryInterfaceStatus();


		#region Overrides of ConfigurationBase

		/// <summary>
		/// 请求保存配置
		/// </summary>
		public override void Save()
		{
			LastShutdownTime = DateTime.Now;
			RunningTime += LastShutdownTime - LastStartTime;
			LastStartTime = DateTime.Now;
			base.Save();
		}

		#endregion
	}
}
