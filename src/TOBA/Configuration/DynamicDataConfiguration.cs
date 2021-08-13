using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.Configuration
{
	internal class DynamicDataConfiguration : ConfigurationBase
	{
		#region 单例模式

		static DynamicDataConfiguration _instance;
		static readonly object _lockObject = new object();
		List<HashSet<string>> _sameStationData;

		public static DynamicDataConfiguration Instance
		{
			get
			{
				if (_instance == null)
				{
					lock (_lockObject)
					{
						if (_instance == null)
						{
							_instance = AppContext.ExtensionManager.ConfigurationProvider.LoadConfiguration<DynamicDataConfiguration>("data", "app");
						}
					}
				}

				return _instance;
			}
		}

		#endregion

		public List<HashSet<string>> SameStationData
		{
			get { return _sameStationData; }
			set
			{
				if (Equals(value, _sameStationData))
					return;
				_sameStationData = value;
				OnPropertyChanged("SameStationData");
			}
		}
	}
}
