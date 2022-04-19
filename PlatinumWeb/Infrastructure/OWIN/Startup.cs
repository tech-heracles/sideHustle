using System;
using System.Threading.Tasks;
using Owin;
using Microsoft.Owin.Security.OAuth;
using System.Web.Http;
using Microsoft.Owin;
using PlatinumWeb.Providers;

[assembly: OwinStartup(typeof(PlatinumWeb.Startup))]

namespace PlatinumWeb
{
    public class Startup
    {
        public static string PublicClientId { get; private set; }
        public OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }
        public OAuthAuthorizationServerOptions oAuthAuthorizationServerOptions { get; private set; }

        // Invoked once per request.
        public Task Invoke(IOwinContext context)
        {
            context.Response.ContentType = "text/plain";
            return context.Response.WriteAsync("Hello World");
        }

        public void Configuration(IAppBuilder app)
        {
            //var httpConfig = new HttpConfiguration();
            ConfigureOAuth(app);
            //  WebApiConfig.Register(httpConfig);
            // app.UseCors(CorsOptions.AllowAll);
            //app.UseWebApi(httpConfig);
            app.MapSignalR();
        }

        private void ConfigureOAuth(IAppBuilder app)
        {
            OAuthBearerOptions = new OAuthBearerAuthenticationOptions();
            oAuthAuthorizationServerOptions = new OAuthAuthorizationServerOptions()
            {
                TokenEndpointPath = new PathString("/External/Token"), // pathi
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                AllowInsecureHttp = true,
                Provider = new SimpleAuthorizationServerProvider(),
                RefreshTokenProvider=new SimpleRefreshTokenProvider()
            };
            app.UseOAuthAuthorizationServer(oAuthAuthorizationServerOptions);
            app.UseOAuthBearerAuthentication(OAuthBearerOptions);

        }
    }
}
