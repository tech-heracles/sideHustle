using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace RestApi.WebAPI.Formatters
{
    public class FeatureCollectionFormatter : MediaTypeFormatter
    {

        /// <summary>
        /// nje klase e tille supozohet te perdoret per te shkruar ne response nje format te caktuar
        /// </summary>
        public FeatureCollectionFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));

        }

        /// <summary>
        /// true nese mund te deserializohet me kete formatter
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public override bool CanReadType(Type type)
        {
            return false;
        }
        public override bool CanWriteType(Type type)
        {
            if (typeof(NetTopologySuite.Features.FeatureCollection) == type)
            {
                return true;
            }

            return false;
        }
  

        public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext)
        {
            using (var writer = new StreamWriter(writeStream))
            {

                var featureCollection = value as NetTopologySuite.Features.FeatureCollection;
                if (featureCollection == null)
                {
                    throw new InvalidOperationException("NetTopologySuite.Features.FeatureCollection nuk mund te serializohet!!");
                }

                return writer.WriteAsync("stringu");
            }

        
        }

   
     


    }
}
