using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.Configuration
{
	using Newtonsoft.Json;
	using Newtonsoft.Json.Serialization;

	internal class OrderConfiguration : ConfigurationBase
	{
		#region 单例模式

		static OrderConfiguration _instance;
		static readonly object _lockObject = new object();
		bool _enableOrderArchive;
		bool _enableFastSubmitOrder;
		bool _ignoreQueueError;
		bool _submitFailedNoTicketAutoDisable = true;
		int _submitFailedNoTicketControlTime = 2;
		int _submitFailedNoTicketControlRate = 3;
		int _autoRetryCountLimit;
		private bool _tryStopStandTicket = true;

		public static OrderConfiguration Instance
		{
			get
			{
				if (_instance == null)
				{
					lock (_lockObject)
					{
						if (_instance == null)
						{
							_instance = AppContext.ExtensionManager.ConfigurationProvider.LoadConfiguration<OrderConfiguration>("settings", "order");
						}
					}
				}

				return _instance;
			}
		}

		#endregion

		/// <summary>
		/// 创建 <see cref="OrderConfiguration" />  的新实例(OrderConfiguration)
		/// </summary>
		public OrderConfiguration()
		{
			_enableOrderArchive = true;
			_enableFastSubmitOrder = true;
			_ignoreQueueError = true;
			_autoRetryCountLimit = 3;
		}

		/// <summary>
		/// 获得或设置是否启用订单存档
		/// </summary>
		public bool EnableOrderArchive
		{
			get { return _enableOrderArchive; }
			set
			{
				if (value.Equals(_enableOrderArchive))
					return;
				_enableOrderArchive = value;
				OnPropertyChanged(nameof(EnableOrderArchive));
			}
		}

		/// <summary>
		/// 设置是否启用快速提交模式
		/// </summary>
		[JsonProperty("enableFastSubmitOrder_v1")]
		public bool EnableFastSubmitOrder
		{
			get { return _enableFastSubmitOrder; }
			set
			{
				if (value.Equals(_enableFastSubmitOrder))
					return;
				_enableFastSubmitOrder = value;
				OnPropertyChanged("EnableFastSubmitOrder");
			}
		}

		/// <summary>
		/// 获得或设置是否忽略排队人数错误
		/// </summary>
		public bool IgnoreQueueError
		{
			get { return _ignoreQueueError; }
			set
			{
				if (value.Equals(_ignoreQueueError))
					return;
				_ignoreQueueError = value;
				OnPropertyChanged("IgnoreQueueError");
			}
		}

		private bool _ignoreSafeTime = false;

		[JsonIgnore]
		public bool IgnoreSafeTime
		{
			get { return _ignoreSafeTime; }
			set
			{
				if (value == _ignoreSafeTime)
					return;
				_ignoreSafeTime = value;
				OnPropertyChanged(nameof(IgnoreSafeTime));
			}
		}

		/// <summary>
		/// 余票不足的时候，是否自动禁用提交
		/// </summary>
		public bool SubmitFailedNoTicketAutoDisable
		{
			get { return _submitFailedNoTicketAutoDisable; }
			set
			{
				if (value == _submitFailedNoTicketAutoDisable) return;
				_submitFailedNoTicketAutoDisable = value;
				OnPropertyChanged("SubmitFailedNoTicketAutoDisable");
			}
		}

		/// <summary>
		/// 余票不足的时候，每隔多少时间里出现相同票数指定次数后禁止提交的单位时间
		/// </summary>
		public int SubmitFailedNoTicketControlTime
		{
			get { return _submitFailedNoTicketControlTime; }
			set
			{
				if (value == _submitFailedNoTicketControlTime) return;
				_submitFailedNoTicketControlTime = value;
				OnPropertyChanged("SubmitFailedNoTicketControlTime");
			}
		}

		/// <summary>
		/// 余票不足的时候，每隔多少时间里出现相同票数指定次数后禁止提交的阈值
		/// </summary>
		public int SubmitFailedNoTicketControlRate
		{
			get { return _submitFailedNoTicketControlRate; }
			set
			{
				if (value == _submitFailedNoTicketControlRate) return;
				_submitFailedNoTicketControlRate = value;
				OnPropertyChanged("SubmitFailedNoTicketControlRate");
			}
		}

		/// <summary>
		/// 自动重试提交订单的次数限制
		/// </summary>
		public int AutoRetryCountLimit
		{
			get { return _autoRetryCountLimit; }
			set
			{
				if (value == _autoRetryCountLimit)
					return;
				_autoRetryCountLimit = value;
				OnPropertyChanged(nameof(AutoRetryCountLimit));
			}
		}

		/// <summary>
		/// 尝试屏蔽无座
		/// </summary>
		public bool TryStopStandTicket
		{
			get { return _tryStopStandTicket; }
			set
			{
				if (value == _tryStopStandTicket)
					return;
				_tryStopStandTicket = value;
				OnPropertyChanged(nameof(TryStopStandTicket));
			}
		}

		private bool _tryStopNoTicket = true;

		/// <summary>
		/// 是否拦截无票的预定请求
		/// </summary>
		public bool TryStopNoTicket
		{
			get => _tryStopNoTicket;
			set
			{
				if (value == _tryStopNoTicket)
					return;
				_tryStopNoTicket = value;
				OnPropertyChanged(nameof(TryStopNoTicket));
			}
		}
	}
}
