namespace TOBA.Data
{
	using FSLib.Extension;

	using System;
	using System.Collections.Generic;
	using System.Timers;

	/// <summary>
	/// 一个用于统计一段时间内发生次数的计数器
	/// </summary>
	public class MaxOccourTimesCounter
	{
		/// <summary>
		/// 获得或设置减少检测周期
		/// </summary>
		public int Interval { get; private set; }

		/// <summary>
		/// 获得或设置用于统计的超时时间
		/// </summary>
		public TimeSpan Timeout { get; set; }

		/// <summary>
		/// 获得或设置当统计溢出后是否自动复位
		/// </summary>
		public bool AutoResetIfExceed { get; set; }

		/// <summary>
		/// 最大容许的次数
		/// </summary>
		public int MaxCount { get; set; }

		Dictionary<object, Queue<long>> _dictionary;
		Timer _timer;

		/// <summary>
		/// 创建 <see cref="SelfCounter" />  的新实例(SelfCounter)
		/// </summary>
		/// <param name="interval"></param>
		/// <param name="timeout"></param>
		public MaxOccourTimesCounter(TimeSpan timeout, int maxCount, int interval = 1000)
		{
			MaxCount = maxCount;
			Interval = interval;
			Timeout = timeout;
			_dictionary = new Dictionary<object, Queue<long>>();

			if (interval <= 0)
				interval = 1000;

			_timer = new Timer(interval);
			_timer.AutoReset = false;
			_timer.Elapsed += _timer_Elapsed;
		}

		void _timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			RemoveExpiresData();

			if (_dictionary.Count > 0)
				_timer.Start();
		}

		/// <summary>
		/// 存在次数超出规定
		/// </summary>
		public event EventHandler<GeneralEventArgs<object>> OccourTimesExceed;


		/// <summary>
		/// 引发 <see cref="OccourTimesExceed" /> 事件
		/// </summary>
		/// <param name="ea">包含此事件的参数</param>
		protected virtual void OnOccourTimesExceed(GeneralEventArgs<object> ea)
		{
			var handler = OccourTimesExceed;
			if (handler != null)
				handler(this, ea);
		}

		/// <summary>
		/// 将当前指定对象的计数加1
		/// </summary>
		/// <param name="key"></param>
		public void EnqueueData(object key)
		{
			lock (_dictionary)
			{
				Queue<long> queue;
				if (!_dictionary.TryGetValue(key, out queue))
				{
					queue = new Queue<long>();
					_dictionary.Add(key, queue);
				}
				queue.Enqueue(DateTime.Now.Ticks);
				RemoveExpiresData();

				if (_dictionary.Count > 0)
					_timer.Start();
			}
		}

		/// <summary>
		/// 移除过期数据
		/// </summary>
		void RemoveExpiresData()
		{
			lock (_dictionary)
			{
				var minTicks = DateTime.Now.Add(Timeout).Ticks;

				var removedKey = new List<object>();
				foreach (var key in _dictionary.Keys)
				{
					var queue = _dictionary[key];
					while (queue.Peek() < minTicks)
					{
						queue.Dequeue();
					}

					if (queue.Count > MaxCount)
					{
						OnOccourTimesExceed(new GeneralEventArgs<object>(key));

						if (AutoResetIfExceed)
						{
							removedKey.Add(key);
						}
					}
					else if (queue.Count == 0)
					{
						removedKey.Add(key);
					}
				}

				foreach (var o in removedKey)
				{
					_dictionary.Remove(o);
				}
			}
		}
	}
}
