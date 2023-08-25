using System;
using System.ComponentModel;

namespace TOBA.Configuration
{
	using FSLib.Extension;

	using Newtonsoft.Json;

	using System.IO;
	using System.Runtime.Serialization;
	using System.Timers;

	public abstract class ConfigurationBase : INotifyPropertyChanged
	{
		Timer _saveTimer;
		TimeSpan? _saveTimeSpan;

		/// <summary>
		/// 创建 <see cref="ConfigurationBase" />  的新实例(ConfigurationBase)
		/// </summary>
		public ConfigurationBase()
		{
			AutoSave = true;
			SuspendFlush = true;
		}

		[OnDeserializing]
		internal void OnDeserializingMethod(StreamingContext context)
		{
			SuspendFlush = true;
		}
		[OnDeserialized]
		internal void OnDeserializedMethod(StreamingContext context)
		{
			SuspendFlush = false;
		}
		/// <summary>
		/// 获得或设置是否自动保存
		/// </summary>
		[JsonIgnore]
		public bool AutoSave { get; set; }

		/// <summary>
		/// 是否挂起即时保存机制
		/// </summary>
		[JsonIgnore]
		public bool SuspendFlush { get; set; }

		public TimeSpan? SaveTimeSpan
		{
			get { return _saveTimeSpan; }
			set
			{
				_saveTimeSpan = value;
				if (_saveTimeSpan == null)
				{
					if (_saveTimer != null)
						_saveTimer.Stop();
				}
				else
				{
					if (_saveTimer == null)
					{
						_saveTimer = new Timer();
						_saveTimer.Elapsed += (s, e) => Save();
					}
					_saveTimer.Stop();
					_saveTimer.Interval = _saveTimeSpan.Value.TotalMilliseconds;
					_saveTimer.Start();
				}
			}
		}

		/// <summary>
		/// 获得或设置文件配置路径
		/// </summary>
		[JsonIgnore]
		public string FilePath { get; set; }

		/// <summary>
		/// 请求保存配置
		/// </summary>
		public virtual void Save()
		{
			if (string.IsNullOrEmpty(FilePath) || SuspendFlush)
				return;

			File.WriteAllText(FilePath, JsonConvert.SerializeObject(this));
		}

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (AutoSave && !SuspendFlush)
				Save();
			var handler = PropertyChanged;
			if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
