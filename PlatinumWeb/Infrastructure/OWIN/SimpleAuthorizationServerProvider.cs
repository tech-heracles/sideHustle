using DbCore.DbAdmin;
using DbCore.IMBUtils;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using PlatinumWeb.OWIN.Helpers;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Security;

namespace PlatinumWeb.Providers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            var client_id = context.Parameters["client_id"];
            context.OwinContext.Set("serverName", context.Parameters.Get("serverName"));

            if (string.IsNullOrEmpty(client_id)) context.Validated();

            else context.Validated(client_id);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");
            string serverName =OAuthHelper.GetServerName(context.OwinContext);

            if (allowedOrigin == null) allowedOrigin = "*";

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

            var user = new clsPerdorues(context.UserName, serverName);

            var isValid = !string.IsNullOrWhiteSpace(user.PerdoruesUsername) && PasswordHelper.ValidoPassword(context.UserName, context.Password, user.PerdoruesPassword);

            if (isValid)
            {
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);

                identity.AddClaim(new Claim("userName", context.UserName));
                identity.AddClaim(new Claim("role", "user"));
                identity.AddClaim(new Claim(MyConnectionsManager.ConStringNameCacheKey, serverName));
                AuthenticationProperties properties = CreateProperties(context, user.PerdoruesUsername);
                AuthenticationTicket ticket = new AuthenticationTicket(identity, properties);
                context.Validated(ticket);
            }
            else
            {
                context.SetError("invalid_grant", "Username/password incorrect");
            }
        }



        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            var originalClient = context.Ticket.Properties.Dictionary["as:client_id"];
            var currentClient = context.ClientId;

            if (!string.IsNullOrWhiteSpace(originalClient) && originalClient != currentClient)
            {
                context.SetError("invalid_clientId", "Ky refresh token nuk eshte leshuar per kete client_id");
                return Task.FromResult<object>(null);
            }

            // Change auth ticket for refresh token requests
            var newIdentity = new ClaimsIdentity(context.Ticket.Identity);

            var newClaim = newIdentity.Claims.Where(c => c.Type == "newClaim").FirstOrDefault();
            if (newClaim != null)
            {
                newIdentity.RemoveClaim(newClaim);
            }
            newIdentity.AddClaim(new Claim("newClaim", "newValue"));

            var newTicket = new AuthenticationTicket(newIdentity, context.Ticket.Properties);
            context.Validated(newTicket);

            return Task.FromResult<object>(null);
        }
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }
        public override Task TokenEndpointResponse(OAuthTokenEndpointResponseContext context)
        {
            //ketu mund te kapet access token
            return base.TokenEndpointResponse(context);
        }
        //public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        //{

        //    Uri expectedRootUri = new Uri(context.Request.Uri, "/");
        //    if (expectedRootUri.AbsoluteUri == context.RedirectUri)
        //    {
        //        context.Validated();
        //    }
        //    return Task.FromResult<object>(null);
        //}
        public static AuthenticationProperties CreateProperties(OAuthGrantResourceOwnerCredentialsContext context, string userName)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "userName", userName },
                { "as:client_id", (context.ClientId == null) ? string.Empty : context.ClientId }
            };
            return new AuthenticationProperties(data);
        }
    }
}