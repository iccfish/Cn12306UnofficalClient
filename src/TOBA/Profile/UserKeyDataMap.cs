namespace TOBA.Profile
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Runtime.Serialization;

	using Configuration;

	using Otn.Workers;

	internal class UserKeyDataMap : ConfigurationBase
	{
		static UserKeyDataMap _current;
		Dictionary<string, UserKeyData> _userKeyData;

		public static UserKeyDataMap Current => _current ??= AppContext.ExtensionManager.ConfigurationProvider.LoadConfiguration<UserKeyDataMap>("Sessions", "Security");

		[OnDeserialized]
		void OnDeserialized(StreamingContext context)
		{
			ReattachPropertyChangeEvents();
		}
		void ReattachPropertyChangeEvents()
		{
			UserKeyData?.ForEach(
				kvp =>
				{
					kvp.Value.PropertyChanged -= Item_OnPropertyChanged;
					kvp.Value.PropertyChanged += Item_OnPropertyChanged;
				});
		}
		public Dictionary<string, UserKeyData> UserKeyData
		{
			get { return _userKeyData ??= new Dictionary<string, UserKeyData>(); }
			set
			{
				if (Equals(value, _userKeyData))
					return;
				_userKeyData?.ForEach(kvp => kvp.Value.PropertyChanged -= Item_OnPropertyChanged);
				_userKeyData = value;
				ReattachPropertyChangeEvents();
				OnPropertyChanged(nameof(UserKeyData));
				OnPropertyChanged("Item");
			}
		}

		public UserKeyData this[string key]
		{
			get
			{
				var item = UserKeyData.GetValue(key);
				if (item != null)
				{
					item.PropertyChanged -= Item_OnPropertyChanged;
					item.PropertyChanged += Item_OnPropertyChanged;
				}
				return item;
			}
			set
			{
				if (value == null)
				{
					if (UserKeyData.ContainsKey(key))
						UserKeyData.Remove(key);
				}
				else
				{
					UserKeyData[key]      =  value;
					value.PropertyChanged -= Item_OnPropertyChanged;
					value.PropertyChanged += Item_OnPropertyChanged;
				}
				Save();
			}
		}
		private void Item_OnPropertyChanged(object sender, PropertyChangedEventArgs e) => Save();
	}
}