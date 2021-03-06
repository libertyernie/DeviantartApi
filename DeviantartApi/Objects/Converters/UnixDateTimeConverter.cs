﻿using Newtonsoft.Json;
using System;

namespace DeviantartApi.Objects.Converters
{
    internal class UnixDateTimeConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var dateTime = (DateTime)value;
            writer.WriteValue((long)dateTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(long);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var unixTime = reader.Value.GetType() == typeof(string) ? long.Parse((string)reader.Value) : (long)reader.Value;
            var dateTime =
                new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(unixTime).ToLocalTime();
            return dateTime;
        }
    }
}
