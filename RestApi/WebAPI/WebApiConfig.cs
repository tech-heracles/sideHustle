using System.Web;
using System.Web.Http;
using System.Web.Routing;
using System.Web.SessionState;
using Microsoft.AspNet.WebApi.Extensions.Compression.Server;
using System.Net.Http.Extensions.Compression.Core.Compressors;
using Microsoft.Owin.Security.OAuth;
using AlphaWeb.Core.Infrastructure;
using Autofac.Integration.WebApi;

namespace RestApi.WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //Ky route eshte per te kapur API nga brenda alphaweb,nese je i loguar me forms auth
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            //Ky route eshte per te aksesuar api npr token nga app te tjera
            config.Routes.MapHttpRoute(name: "ExternalApi",
                routeTemplate: "External/api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
             );
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            // config.Filters.Add(new AuthorizeAttribute());
            //config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new Formatters.FeatureCollectionConverter());
            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.None;


            //info mbi kompresimin https://github.com/azzlack/Microsoft.AspNet.WebApi.MessageHandlers.Compression
            config.MessageHandlers.Insert(0, new ServerCompressionHandler(4096, new GZipCompressor()));

            config.DependencyResolver = new AutofacWebApiDependencyResolver(EngineContext.Current.ContainerManager.Container);
            
        }
    }
}