namespace TOBA.Query.Entity
{
	using System;
	using System.Linq;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Text.RegularExpressions;

	internal class TrainStopCollection : List<TrainStopInfo>
	{

		/// <summary>
		/// 根据上车站时间推算全部时间
		/// </summary>
		/// <param name="currentDate"></param>
		/// <param name="startTrainDate"></param>
		public void ApplyTime(QueryResultItem train, DateTime? startTrainDate, DateTime currentDate)
		{
			if (Count == 0)
				return;

			var classL = Regex.IsMatch(train.Code, @"^(K[456]\d{3})$", RegexOptions.IgnoreCase);


			if (startTrainDate != null)
			{
				var start = this[0];
				var baseDate = startTrainDate.Value;
				Debug.Assert(start.DepartureTime != null, "start.DepartureTime != null");
				start.DepartureFullTime = baseDate + start.DepartureTime.Value;

				for (var i = 1; i < Count; i++)
				{
					var t = this[i];
					t.ArriveFullTime = baseDate.Add(t.ArriveTime.Value);
					if (t.ArriveFullTime.Value < this[i - 1].DepartureFullTime.Value)
					{
						baseDate = baseDate.AddDays(1);
						t.ArriveFullTime = baseDate.Add(t.ArriveTime.Value);
					}

					if (classL && (t.ArriveFullTime.Value - start.DepartureFullTime.Value).TotalMinutes <= 60)
					{
						baseDate = baseDate.AddDays(1);
						t.ArriveFullTime = t.ArriveFullTime.Value.AddHours(24);
					}

					if (t.DepartureTime != null)
					{
						if (t.StopHoverTime == null)
						{
							t.StopHoverTime = 0;
						}

						t.DepartureFullTime = baseDate.Add(t.DepartureTime.Value);
						if (t.DepartureFullTime.Value < t.ArriveFullTime.Value)
						{
							baseDate = t.ArriveFullTime.Value.Date.AddDays(1);
							t.ArriveFullTime = baseDate.Add(t.DepartureTime.Value);
						}
					}
				}
			}
			else
			{
				var currentStation = this.First(s => s.IsEnabled);
				//var endStation = this.Last(s => s.IsEnabled);

				currentStation.DepartureFullTime = currentDate.Date + currentStation.DepartureTime.Value;
				currentStation.ArriveFullTime = currentDate.Date + currentStation.ArriveTime;
				if (currentStation.ArriveFullTime.HasValue && currentStation.ArriveFullTime.Value < currentStation.ArriveFullTime.Value)
					currentStation.ArriveFullTime = currentStation.ArriveFullTime.Value.AddDays(1);

				//往前推测时间
				TrainStopInfo current = null;
				foreach (var source in this.TakeWhile(s => s != currentStation).Reverse())
				{
					current = current ?? currentStation;
					source.DepartureFullTime = current.ArriveFullTime.Value.Date + source.DepartureTime.Value;
					if (source.DepartureFullTime.Value >= current.DepartureFullTime.Value)
						source.DepartureFullTime = source.DepartureFullTime.Value.AddDays(-1);
					else if (classL && (current.ArriveFullTime.HasValue && (current.ArriveFullTime.Value - source.DepartureFullTime.Value).TotalMinutes <= 60))
					{
						source.DepartureFullTime = source.DepartureFullTime.Value.AddDays(-1);
					}

					if (source.ArriveTime != null)
					{
						source.ArriveFullTime = source.DepartureFullTime.Value.AddMinutes(-source.StopHoverTime.Value);
					}

					current = source;
				}

				//往后推测时间
				current = null;
				foreach (var source in this.SkipWhile(s => s != currentStation).Skip(1))
				{
					current = current ?? currentStation;
					source.ArriveFullTime = current.DepartureFullTime.Value.Date + source.ArriveTime.Value;
					if (source.ArriveFullTime.Value <= current.DepartureFullTime.Value || (classL && (source.ArriveFullTime.Value - current.DepartureFullTime.Value).TotalMinutes <= 60))
						source.ArriveFullTime = source.ArriveFullTime.Value.AddDays(1);

					if (source.DepartureTime != null && source.StopHoverTime != null)
						source.DepartureFullTime = source.ArriveFullTime.Value.AddMinutes(source.StopHoverTime.Value);

					current = source;
				}
			}
		}

	}
}
