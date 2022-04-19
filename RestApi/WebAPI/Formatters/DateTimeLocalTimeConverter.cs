using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace RestApi.WebAPI.Formatters
{

    /// <summary>
    /// Serializon,Deserializon daten sipas Cultures
    /// </summary>
    public class DateTimeLocalTimeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime);
        }
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var converter = new JavaScriptDateTimeConverter();
            converter.WriteJson(writer, value != null ? ((DateTime) value).ToLocalTime() : value, serializer);
        }


        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var converter = new JavaScriptDateTimeConverter();
            return converter.ReadJson(reader, objectType, existingValue != null ? ((DateTime)existingValue).ToLocalTime() : existingValue, serializer);
        }
    }
}
