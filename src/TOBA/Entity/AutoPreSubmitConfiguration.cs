using System.Linq;

namespace TOBA.Entity
{
	using Data;

	using FSLib.Extension;

	using Newtonsoft.Json;

	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Text.RegularExpressions;

	/// <summary>
	/// 自动预定设置
	/// </summary>
	internal class AutoPreSubmitConfiguration : INotifyPropertyChanged
	{
		bool _autoSelectTrain = true;
		bool _autoWaitToSell;
		bool _enableOClockRefresh;
		bool _enablePartialSubmit;
		bool _hideOtherTrains;
		List<PassengerInTicket> _passenger;

		Dictionary<char, List<ISeatCheckRule>> _seatCheckRules = new Dictionary<char, List<ISeatCheckRule>>();
		bool _seatFirst = true;
		EventList<char> _seatList;

		private char[] _seatListForSelect;
		EventList<string> _trainList;
		List<Regex> _trainReg;

		/// <summary>
		/// 获得是否已经设置
		/// </summary>
		[JsonIgnore]
		public bool AllSetOk
		{
			get { return SeatList.Count > 0 && Passenger.Count > 0 && TrainList.Count > 0; }
		}
		//bool _autoMode;

		/// <summary>
		/// 获得或设置在席别优先时是否自动优选车次
		/// </summary>
		public bool AutoSelectTrain
		{
			get { return _autoSelectTrain; }
			set
			{
				if (value.Equals(_autoSelectTrain)) return;
				_autoSelectTrain = value;
				OnPropertyChanged("AutoSelectTrain");
			}
		}

		/// <summary>
		/// 获得或设置如果没有查到票，是否自动等待到起售期
		/// </summary>
		public bool AutoWaitToSell
		{
			get { return _autoWaitToSell; }
			set
			{
				if (value.Equals(_autoWaitToSell))
					return;
				_autoWaitToSell = value;
				OnPropertyChanged("AutoWaitToSell");
			}
		}

		/// <summary>
		/// 获得或设置是否允许整点刷新
		/// </summary>
		public bool EnableOClockRefresh
		{
			get { return _enableOClockRefresh; }
			set
			{
				if (value.Equals(_enableOClockRefresh))
					return;
				_enableOClockRefresh = value;
				OnPropertyChanged("EnableOClockRefresh");
			}
		}

		/// <summary>
		/// 获得或设置是否允许部分提交
		/// </summary>
		public bool EnablePartialSubmit
		{
			get { return _enablePartialSubmit; }
			set
			{
				if (value.Equals(_enablePartialSubmit))
					return;
				_enablePartialSubmit = value;
				OnPropertyChanged("EnablePartialSubmit");
			}
		}

		/// <summary>
		/// 获得或设置是否隐藏其它车次
		/// </summary>
		public bool HideOtherTrains
		{
			get { return _hideOtherTrains; }
			set
			{
				if (value.Equals(_hideOtherTrains))
					return;
				_hideOtherTrains = value;
				OnPropertyChanged("HideOtherTrains");
			}
		}

		/// <summary>
		/// 获得或设置选定的乘客
		/// </summary>
		public List<PassengerInTicket> Passenger
		{
			get { return _passenger ?? (_passenger = new List<PassengerInTicket>()); }
			set
			{
				if (_passenger == value)
					return;

				_passenger = value;
				OnPropertyChanged("Passenger");
			}
		}

		/// <summary>
		/// 获得或设置是否席别优先
		/// </summary>
		public bool SeatFirst
		{
			get { return _seatFirst; }
			set
			{
				if (value.Equals(_seatFirst))
					return;
				_seatFirst = value;
				OnPropertyChanged("SeatFirst");
			}
		}

		/// <summary>
		/// 获得或设置选定的席别列表
		/// </summary>
		public EventList<char> SeatList
		{
			get
			{
				if (_seatList == null)
				{
					_seatList = new EventList<char>();
					_seatList.ItemChanged += (sender, args) =>
					{
						_seatListForSelect = null;
					};
				}

				return _seatList;
			}
		}

		public bool AddSeat(char code)
		{
			if (SeatList.Contains(code)) return false;

			SeatList.Add(code);
			return true;
		}


		/// <summary>
		/// 获得用于
		/// </summary>
		/// <returns></returns>
		[JsonIgnore]
		public char[] SeatListForSelect
		{
			get
			{
				if (_seatListForSelect == null)
				{
					_seatListForSelect = SeatList.Select(ParamData.GetSeatCompatibleMap).ExceptNull().SelectMany(s => s).Distinct().ToArray();
				}

				return _seatListForSelect;
			}
		}

		/// <summary>
		/// 席别检测规则
		/// </summary>
		public Dictionary<char, List<ISeatCheckRule>> SeatCheckRules
		{
			get { return _seatCheckRules; }
			set
			{
				if (Equals(value, _seatCheckRules)) return;
				_seatCheckRules = value;
				OnPropertyChanged(nameof(SeatCheckRules));
			}
		}

		/// <summary>
		/// 获得或设置车次列表
		/// </summary>
		public EventList<string> TrainList
		{
			get
			{
				if (_trainList == null)
				{
					_trainList = new EventList<string>();
					_trainList.ItemChanged += (sender, args) =>
					{
						_trainReg = null;
					};
				}
				return _trainList;
			}
		}

		public bool AddTrainCode(string code)
		{
			if (TrainList.Contains(code)) return false;
			TrainList.Add(code);

			return true;
		}

		/// <summary>
		/// 获得或设置车次列表
		/// </summary>
		[JsonIgnore]
		public List<Regex> TrainReg
		{
			get { return _trainReg ?? (_trainReg = TrainList.Select(s => new Regex("^(" + ParamData.FrequencyTrainCodeMap.GetValue(s).DefaultForEmpty(s) + ")$", RegexOptions.Singleline | RegexOptions.IgnoreCase)).ToList()); }
		}

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null)
				handler(this, new PropertyChangedEventArgs(propertyName));
		}

		///// <summary>
		///// 获得或设置是否使用自动模式
		///// </summary>
		//public bool AutoMode
		//{
		//	get { return _autoMode; }
		//	set
		//	{
		//		if (value.Equals(_autoMode)) return;
		//		_autoMode = value;
		//		OnPropertyChanged("AutoMode");
		//	}
		//}

		private EventList<SubType> _seatSubTypesBed = new EventList<SubType>();

		/// <summary>
		/// 卧铺的席位优选次序
		/// </summary>
		public EventList<SubType> SeatSubTypesBed
		{
			get { return _seatSubTypesBed; }
			set
			{
				if (Equals(value, _seatSubTypesBed))
					return;
				_seatSubTypesBed = value;
				OnPropertyChanged(nameof(SeatSubTypesBed));
			}
		}

		private EventList<SubType> _seatSubTypesHighSpeed = new EventList<SubType>();

		/// <summary>
		/// 高铁动车的席位优选顺序
		/// </summary>
		public EventList<SubType> SeatSubTypesHighSpeed
		{
			get { return _seatSubTypesHighSpeed; }
			set
			{
				if (Equals(value, _seatSubTypesHighSpeed))
					return;
				_seatSubTypesHighSpeed = value;
				OnPropertyChanged(nameof(SeatSubTypesHighSpeed));
			}
		}
	}
}
