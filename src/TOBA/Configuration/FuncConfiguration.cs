using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.Configuration
{
	class FuncConfiguration : ConfigurationBase
	{
		#region 单例模式

		static FuncConfiguration _instance;
		static readonly object _lockObject = new object();

		/// <summary>
		/// 获得 <see cref="FuncConfiguration"/> 的单例对象
		/// </summary>
		public static FuncConfiguration Instance
		{
			get
			{
				if (_instance == null)
				{
					lock (_lockObject)
					{
						if (_instance == null)
						{
							_instance = AppContext.ExtensionManager.ConfigurationProvider.LoadConfiguration<FuncConfiguration>("func");
						}
					}
				}

				return _instance;
			}
		}

		#endregion

		#region 私有构造函数

		public FuncConfiguration()
		{
		}

		#endregion

		private bool _enableHbStatusAutoCheck = false;

		/// <summary>
		/// 是否启用候补状态自动查询
		/// </summary>
		public bool EnableHbStatusAutoCheck
		{
			get => _enableHbStatusAutoCheck;
			set
			{
				if (value == _enableHbStatusAutoCheck) return;
				_enableHbStatusAutoCheck = value;
				OnPropertyChanged(nameof(EnableHbStatusAutoCheck));
			}
		}


		private bool _enableTicketPriceAutoQuery = false;

		/// <summary>
		/// 是否启用票价自动查询
		/// </summary>
		public bool EnableTicketPriceAutoQuery
		{
			get => _enableTicketPriceAutoQuery;
			set
			{
				if (value == _enableTicketPriceAutoQuery) return;
				_enableTicketPriceAutoQuery = value;
				OnPropertyChanged(nameof(EnableTicketPriceAutoQuery));
			}
		}


		private int _ticketPriceQuerySleepTimeNormal = 5000;

		/// <summary>
		/// 票价查询休息时间
		/// </summary>
		/// <value>
		/// The ticket price query sleep time normal.
		/// </value>
		public int TicketPriceQuerySleepTimeNormal
		{
			get => _ticketPriceQuerySleepTimeNormal;
			set
			{
				if (value == _ticketPriceQuerySleepTimeNormal) return;
				_ticketPriceQuerySleepTimeNormal = value;
				OnPropertyChanged(nameof(TicketPriceQuerySleepTimeNormal));
			}
		}

		private int _hbStateQueryInterval = 5000;

		/// <summary>
		/// 候补状态监测间隔
		/// </summary>
		public int HbStateQueryInterval
		{
			get => _hbStateQueryInterval;
			set
			{
				if (value == _hbStateQueryInterval) return;
				_hbStateQueryInterval = value;
				OnPropertyChanged(nameof(HbStateQueryInterval));
			}
		}
	}
}
