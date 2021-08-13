namespace TOBA.Profile
{
	using System;
	using System.Collections.Generic;

	using Configuration;

	using Otn.Workers;

	internal class UserKeyDataMap : ConfigurationBase
	{
		static UserKeyDataMap _current;
		Dictionary<string, UserKeyData> _userKeyData;

		public static UserKeyDataMap Current => _current ?? (_current = AppContext.ExtensionManager.ConfigurationProvider.LoadConfiguration<UserKeyDataMap>("Sessions", "Security"));

		public Dictionary<string, UserKeyData> UserKeyData
		{
			get { return _userKeyData ?? (_userKeyData = new Dictionary<string, UserKeyData>()); }
			set
			{
				if (Equals(value, _userKeyData))
					return;
				_userKeyData = value;
				OnPropertyChanged("UserKeyData");
				OnPropertyChanged("Item");
			}
		}

		public UserKeyData this[string key]
		{
			get { return UserKeyData.GetValue(key); }
			set
			{
				if (value == null)
				{
					if (UserKeyData.ContainsKey(key))
						UserKeyData.Remove(key);
				}
				else UserKeyData[key] = value;
				Save();
			}
		}
	}
}