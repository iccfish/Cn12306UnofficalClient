using System;

namespace TOBA.Configuration
{

	using Newtonsoft.Json;

	internal class SubmitOrder : ConfigurationBase
	{
		#region 静态内容

		static SubmitOrder _current;
		int _safeTime;
		bool _enableLiveTicketCheck;
		int _checkTicketFrequency;
		bool _autoSubmitAfterEnterCode;
		char[] _defaultSeatPreferOrder;
		int _orderSbumitDelay;

		public static SubmitOrder Current
		{
			get { return _current ?? (_current = AppContext.ExtensionManager.ConfigurationProvider.LoadConfiguration<SubmitOrder>("main", "Submit")); }
		}

		#endregion
		bool _disableEditNameOfAutoAddedPassenger;
		bool _autoTopMost;


		public SubmitOrder()
		{
			_current = this;
			_enableLiveTicketCheck = true;
			_safeTime = 0;
			_checkTicketFrequency = 8;
			_autoSubmitAfterEnterCode = true;
			_defaultSeatPreferOrder = "OM7P83B142690*".ToCharArray();
			_orderSbumitDelay = 0;
			_disableEditNameOfAutoAddedPassenger = true;
			_autoTopMost = true;
		}

		/// <summary>
		/// 安全时间
		/// </summary>
		public int SafeTime
		{
			get { return _safeTime; }
			set
			{
				if (Math.Abs(value - _safeTime) < 0.01) return;
				_safeTime = value;
				OnPropertyChanged("SafeTime");
			}
		}

		/// <summary>
		/// 在检测完成后休息的时间
		/// </summary>
		public int OrderSubmitDelay
		{
			get { return _orderSbumitDelay; }
			set
			{
				if (value == _orderSbumitDelay)
					return;
				_orderSbumitDelay = value;
				OnPropertyChanged("OrderSbumitDelay");
			}
		}

		/// <summary>
		/// 获得或设置是否允许启用动态检测余票
		/// </summary>
		public bool EnableLiveTicketCheck
		{
			get { return _enableLiveTicketCheck; }
			set
			{
				if (value.Equals(_enableLiveTicketCheck)) return;
				_enableLiveTicketCheck = value;
				OnPropertyChanged("EnableLiveTicketCheck");
			}
		}

		/// <summary>
		/// 检测余票时间间隔
		/// </summary>
		public int CheckTicketFrequency
		{
			get { return _checkTicketFrequency; }
			set
			{
				if (value == _checkTicketFrequency) return;
				_checkTicketFrequency = value;
				OnPropertyChanged("CheckTicketFrequency");
			}
		}

		/// <summary>
		/// 在输入验证码后是否立刻自动提交
		/// </summary>
		public bool AutoSubmitAfterEnterCode
		{
			get { return _autoSubmitAfterEnterCode; }
			set
			{
				if (value.Equals(_autoSubmitAfterEnterCode)) return;
				_autoSubmitAfterEnterCode = value;
				OnPropertyChanged("AutoSubmitAfterEnterCode");
			}
		}

		/// <summary>
		/// 默认的席别优选策略
		/// </summary>
		[JsonIgnore]
		public char[] DefaultSeatPreferOrder
		{
			get { return _defaultSeatPreferOrder; }
			set
			{
				if (Equals(value, _defaultSeatPreferOrder)) return;
				_defaultSeatPreferOrder = value;
				OnPropertyChanged("DefaultSeatPreferOrder");
			}
		}

		public bool DisableEditNameOfAutoAddedPassenger
		{
			get { return _disableEditNameOfAutoAddedPassenger; }
			set
			{
				if (value.Equals(_disableEditNameOfAutoAddedPassenger))
					return;
				_disableEditNameOfAutoAddedPassenger = value;
				OnPropertyChanged("DisableEditNameOfAutoAddedPassenger");
			}
		}

		public bool AutoTopMost
		{
			get { return _autoTopMost; }
			set
			{
				if (value.Equals(_autoTopMost))
					return;
				_autoTopMost = value;
				OnPropertyChanged("AutoTopMost");
			}
		}
	}
}
