namespace TOBA.Platform.TrainBaseInfoStorage
{
	using Entity;

	using System.Collections.Concurrent;

	class TrainBaseInfoStorage : ConcurrentDictionary<string, TrainBaseInfo>
	{
		public TrainBaseInfoStorage()
		{
		}


		#region 单例模式

		static TrainBaseInfoStorage _instance;
		static readonly object _lockObject = new object();
		private static readonly string _filePath = "trainbasedata.dat";

		/// <summary>
		/// 获得 <see cref="TrainBaseInfoStorage"/> 的单例对象
		/// </summary>
		public static TrainBaseInfoStorage Instance
		{
			get
			{
				if (_instance == null)
				{
					lock (_lockObject)
					{
						if (_instance == null)
						{
							_instance = ResourceLoader.LoadResourceFile<TrainBaseInfoStorage>(_filePath, ResourceLocation.AppData) ?? new TrainBaseInfoStorage();
						}
					}
				}

				return _instance;
			}
		}

		#endregion

		public void Save()
		{
			ResourceLoader.SaveResourceFile(_filePath, this, ResourceLocation.AppData);
		}
	}
}