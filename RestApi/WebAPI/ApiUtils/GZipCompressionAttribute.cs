using System.Linq;
using System.Net.Http;
using System.Web.Http.Filters;

namespace RestApi.WebAPI.ApiUtils
{
    public class GZipCompressionAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// atribut i cili i vendoset nje metode ne APiController kur JSON eshte shume i madh per ta kompresuar me Deflate
        /// </summary>
        /// <param name="actContext"></param>
        public override void OnActionExecuted(HttpActionExecutedContext actContext)
        {
            var content = actContext.Response.Content;
            var gzipEncoding = actContext.Request.Headers.FirstOrDefault(x => x.Key == "Accept-Encoding");

            if (!gzipEncoding.Value.Contains("gzip")) return;
            var bytes = content?.ReadAsByteArrayAsync().Result;

            var zlibbedContent = bytes == null ? new byte[0] : CompressionHelper.KompresoMeGZip(bytes);

            actContext.Response.Content = new ByteArrayContent(zlibbedContent);

            actContext.Response.Content.Headers.Remove("Content-Type");

            actContext.Response.Content.Headers.Add("Content-encoding", "GZip");

            actContext.Response.Content.Headers.Add("Content-Type", "application/json");

            base.OnActionExecuted(actContext);
        }
    }
}
