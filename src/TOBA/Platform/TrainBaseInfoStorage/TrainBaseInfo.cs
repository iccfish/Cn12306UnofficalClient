using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.Platform.TrainBaseInfoStorage
{
	using Newtonsoft.Json;



	class TrainBaseInfo : BaseStat
	{
		[JsonProperty("f")]
		public string From { get; set; }

		[JsonProperty("t")]
		public string To { get; set; }

		[JsonProperty("d")]
		public TimeSpan? Departure { get; set; }

		[JsonProperty("a")]
		public TimeSpan? Arrive { get; set; }

		[JsonProperty("e")]
		public TimeSpan? ElapsedTme { get; set; }

		[JsonProperty("s")]
		public Dictionary<string, TrainStopBaseInfo> StopBaseInfos { get; } = new Dictionary<string, TrainStopBaseInfo>();
	}

	class BaseStat
	{
		private static DateTime MinTime = new DateTime(2000, 1, 1);

		static long GetTimeTicks(DateTime time)
		{
			return (long)(time - MinTime).TotalMinutes;
		}

		static DateTime FromTimeTicks(long ticks)
		{
			return MinTime.AddMinutes(ticks);
		}


		[JsonProperty("_c")]
		public int HitCount { get; set; }

		private long _lastUsed;

		[JsonProperty("_u")]
		public long LastUsed
		{
			get => _lastUsed;
			set
			{
				if (value >= 1000000000000)
				{
					value = GetTimeTicks(FishDateTimeExtension.JsTicksStartBase.AddMilliseconds(value));
				}

				_lastUsed = value;
			}
		}

		private long _lastUpdate;

		[JsonProperty("_p")]
		public long LastUpdate
		{
			get => _lastUpdate;
			set
			{
				if (value >= 1000000000000)
				{
					value = GetTimeTicks(FishDateTimeExtension.JsTicksStartBase.AddMilliseconds(value));
				}
				_lastUpdate = value;
			}
		}

		public void SetHit()
		{
			HitCount++;
			LastUsed = GetTimeTicks(DateTime.Now);
		}

		public void SetUpToDate()
		{
			LastUpdate = GetTimeTicks(DateTime.Now);
		}
	}
}
