namespace TOBA.Platform.DeviceFingerprint
{
	using Newtonsoft.Json;

	using System;

	class DeviceData : IComparable
	{
		[JsonProperty("key")]
		public string Key { get; set; }

		[JsonProperty("value")]
		public string Value { get; set; }

		public DeviceData() { }

		public DeviceData(string key, string value)
		{
			Key = key;
			Value = value;
		}

		public int CompareTo(object o)
		{
			if (o == null || o.GetType() != typeof(DeviceData))
			{
				return 1;
			}
			var oo = (DeviceData)o;
			return StringComparer.Ordinal.Compare(this.Key, oo.Key);
		}
	}
}