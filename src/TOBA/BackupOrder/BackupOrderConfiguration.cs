using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.BackupOrder
{
	using Configuration;

	class BackupOrderConfiguration : ConfigurationBase
	{
		#region 单例模式

		static BackupOrderConfiguration _instance;
		static readonly object _lockObject = new object();

		/// <summary>
		/// 获得 <see cref="BackupOrderConfiguration"/> 的单例对象
		/// </summary>
		public static BackupOrderConfiguration Instance
		{
			get
			{
				if (_instance == null)
				{
					lock (_lockObject)
					{
						if (_instance == null)
						{
							_instance = AppContext.ExtensionManager.ConfigurationProvider.LoadConfiguration<BackupOrderConfiguration>("backup_order");
						}
					}
				}

				return _instance;
			}
		}

		#endregion

		#region 私有构造函数

		public BackupOrderConfiguration()
		{
		}

		#endregion

		private int _queryBackupOrderQueueTime = 3000;

		/// <summary>
		/// 查询候补订单队列的时间间隔
		/// </summary>
		public int QueryBackupOrderQueueTime
		{
			get => _queryBackupOrderQueueTime;
			set
			{
				if (value == _queryBackupOrderQueueTime) return;
				_queryBackupOrderQueueTime = value;
				OnPropertyChanged(nameof(QueryBackupOrderQueueTime));
			}
		}

		private int _autoSubmitOrderInterval = 5000;

		/// <summary>
		/// 自动重新提交候补订单的时间间隔
		/// </summary>
		public int AutoSubmitOrderInterval
		{
			get => _autoSubmitOrderInterval;
			set
			{
				if (value == _autoSubmitOrderInterval) return;
				_autoSubmitOrderInterval = value;
				OnPropertyChanged(nameof(AutoSubmitOrderInterval));
			}
		}

		private bool _playMusicOnAutoSubmitSuccess = true;

		/// <summary>
		/// 自动提交成功时，播放音乐
		/// </summary>
		public bool PlayMusicOnAutoSubmitSuccess
		{
			get => _playMusicOnAutoSubmitSuccess;
			set
			{
				if (value == _playMusicOnAutoSubmitSuccess) return;
				_playMusicOnAutoSubmitSuccess = value;
				OnPropertyChanged(nameof(PlayMusicOnAutoSubmitSuccess));
			}
		}
	}
}
