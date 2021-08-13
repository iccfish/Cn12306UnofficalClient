namespace TOBA.Query.Entity
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text.RegularExpressions;
	using Newtonsoft.Json;

	using TOBA.Entity;

	/// <summary>
	/// 停靠站列表
	/// </summary>
	internal class TrainStopInfo : Dto
	{
		[JsonProperty("station_no")]
		public int StationNo { get; set; }

		/// <summary>
		/// 站点名
		/// </summary>
		[JsonProperty("station_name")]
		public string StationName { get; set; }

		[JsonProperty("start_time")]
		public string DepartureTimeString
		{
			get
			{
				return DepartureTime == null ? "--:--" : DepartureTime.Value.ToString("hh':'mm");
			}
			set
			{
				if (value.IndexOf('-') != -1) DepartureTime = null;
				else DepartureTime = TimeSpan.Parse(value);
			}
		}

		/// <summary>
		/// 发车时间
		/// </summary>
		[JsonIgnore]
		public TimeSpan? DepartureTime { get; set; }

		[JsonProperty("arrive_time")]
		public string ArriveTimeString
		{
			get
			{
				return ArriveTime == null ? "----" : ArriveTime.Value.ToString("hh':'mm");
			}
			set
			{
				if (value.IndexOf('-') != -1) ArriveTime = null;
				else ArriveTime = TimeSpan.Parse(value);
			}
		}

		/// <summary>
		/// 到达时间
		/// </summary>
		[JsonIgnore]
		public TimeSpan? ArriveTime { get; set; }

		///// <summary>
		///// 到达时间
		///// </summary>
		//[JsonIgnore]
		//public TimeSpan? StopOverTIme { get; set; }

		private string _stopOverTimeString;

		[JsonProperty("stopover_time")]
		public string StopOverTimeString
		{
			get { return _stopOverTimeString; }
			set
			{
				_stopOverTimeString = value;

				if (value.IsNullOrEmpty())
					StopHoverTime = null;
				else
				{
					StopHoverTime = (Regex.Match(value, @"^\d+").GetGroupValue(0) ?? "").ToInt32Nullable();
				}
			}
		}

		[JsonProperty("stophover_time")]
		public int? StopHoverTime { get; set; }

		[JsonProperty("isEnabled")]
		public bool IsEnabled { get; set; }

		/// <summary>
		/// ServiceType
		/// </summary>
		[JsonProperty("service_type")]
		public int ServiceType { get; set; }

		[JsonProperty("train_class_name")]
		public string TrainClassName { get; set; }

		#region 完整时间信息

		[JsonIgnore]
		public DateTime? ArriveFullTime { get; set; }

		[JsonIgnore]
		public DateTime? DepartureFullTime { get; set; }

		#endregion
	}
}
