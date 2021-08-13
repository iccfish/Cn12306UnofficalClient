namespace TOBA.WebLib
{
	using System;

	using Newtonsoft.Json;

	class JsonTick2DateTimeConverter : JsonConverter
	{
		/// <inheritdoc />
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (value == null)
				writer.WriteValue((long?)null);
			else
				writer.WriteValue(((DateTime)value).ToJsTicks());
		}

		/// <inheritdoc />
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			switch (reader.TokenType)
			{
				case JsonToken.Integer:
					var ticks = (long)reader.Value;
					return FSLib.Extension.DateTimeEx.FromJsTicks(ticks);
				case JsonToken.String:
					return FSLib.Extension.DateTimeEx.FromJsTicks((reader.Value as string).ToInt64());
				default:
					return null;
			}
		}

		/// <inheritdoc />
		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(DateTime);
		}
	}
}