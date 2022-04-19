using System;
using NetTopologySuite.Features;
using NetTopologySuite.IO;
using Newtonsoft.Json;
namespace RestApi.WebAPI.Formatters
{

    /// <summary>
    /// kjo klase sherben si nje serializues custom per objektet e tipit FeatureCollection
    /// per objektet e ketij tipi perdoret  NetTopologySuite.IO.GeoJsonSerializer
    /// </summary>
    public class FeatureCollectionConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(FeatureCollection);
        }
        public override bool CanRead => false;
        public override bool CanWrite => true;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var geoSerializer = new GeoJsonSerializer
            {
                Formatting = Formatting.None
            };
            
            geoSerializer.Serialize(writer, value, typeof(FeatureCollection));
        }


  
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
     
            //kete metode nuk e implementojme sepse nuk eshte i nevojshem deserializimi custom
            throw new NotImplementedException();
        }
    }
}
