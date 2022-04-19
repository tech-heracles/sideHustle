using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Text;
using DbCore;
using DbCore.IMBUtils.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RestApi.WebAPI.ApiUtils
{
    public static class ExtensionsMethods
    {
        private static readonly JsonSerializerSettings SerializationSettings = new JsonSerializerSettings
        {
            Culture = CultureInfo.InvariantCulture,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            Formatting = Formatting.None,
            DateTimeZoneHandling = DateTimeZoneHandling.Local
        };
        #region HELPERS


        /// <summary>
        /// krijon pergjigjen me json ne varesi te serializuesit
        /// </summary>
        /// <param name="request"></param>
        /// <param name="pergjigja"></param>
        /// <param name="serializuesi"></param>
        /// <returns></returns>
        private static HttpResponseMessage KrijoPergjigjeJson(HttpRequestMessage request, object pergjigja, Serializues serializuesi)
        {
            string jsonResult;

            switch (serializuesi)
            {
                case Serializues.GeoJson:
                    jsonResult = KrjoJsonMeGeoSerializer(pergjigja);
                    break;
                case Serializues.JsonConvert:
                    jsonResult = JsonConvert.SerializeObject(pergjigja, SerializationSettings);
                    break;
                default:
                    jsonResult = JsonConvert.SerializeObject(pergjigja, SerializationSettings);
                    break;
            }

            var response = request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(jsonResult, Encoding.UTF8, "application/json");
            return response;

        }

        private static string KrjoJsonMeGeoSerializer(object pergjigja)
        {
            var sb = new StringBuilder();
            var serializer = new NetTopologySuite.IO.GeoJsonSerializer()
            {
                Formatting = SerializationSettings.Formatting,
                ReferenceLoopHandling = SerializationSettings.ReferenceLoopHandling,
                Culture = SerializationSettings.Culture
            };
            using (var sw = new System.IO.StringWriter(sb))
            {
                serializer.Serialize(sw, pergjigja);

            }

            return sb.ToString();
        }

        #endregion
        public static HttpResponseMessage KthePergjigjeJsonMeGeoSerializer(this HttpRequestMessage request, object pergjigja)
        {
            return KrijoPergjigjeJson(request, pergjigja, Serializues.GeoJson);

        }
        /// <summary>
        /// kthen pergjigjen e suksesit ne client 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="pergjigja"></param>
        /// <returns></returns>
        public static HttpResponseMessage KthePergjigje(this HttpRequestMessage request, object pergjigja)
        {
            return KrijoPergjigjeJson(request, pergjigja, Serializues.JsonConvert);
        }

        /// <summary>
        /// kthen pergjigjen duke perfshire edhe logun brenda
        /// </summary>
        /// <param name="request"></param>
        /// <param name="parametrat"></param>
        /// <param name="pergjigja"></param>
        /// <returns></returns>
        public static HttpResponseMessage KthePergjigjeJsonDheShkruajInfoNeLog(this HttpRequestMessage request, JObject parametrat, object pergjigja)
        {
            ImbLogger.LogInfoWebApi("u kthye pergjigje per   ws {0} me keto parametra {1}  ", request.RequestUri, parametrat);
            return KrijoPergjigjeJson(request, pergjigja, Serializues.JsonConvert);
        }

        /// <summary>
        /// kthen pergjigjen e gabimit ne client duke perfshire logimin e errorit
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static HttpResponseMessage KthePergjigjeGabim(this HttpRequestMessage request, object parametrat, Exception e)
        {
            ImbLogger.LogErrorWebApi("ndodhi nje gabim ne thirrjen e ws {0} me keto parametra {1} GABIMI : {2}", request.RequestUri, JsonConvert.SerializeObject(parametrat), e);
            return request.CreateResponse(HttpStatusCode.InternalServerError, e);
        }
        public static HttpResponseMessage KthePergjigjeGabim(this HttpRequestMessage request, Exception e)
        {
            return request.KthePergjigjeGabim(null, e);
        }

    }
}
