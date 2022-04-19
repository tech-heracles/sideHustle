using DbCore;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;
using Newtonsoft.Json;
using PlatinumWeb.OWIN.Helpers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PlatinumWeb.Providers
{
    public class SimpleRefreshTokenProvider : IAuthenticationTokenProvider
    {
        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            var guid = Guid.NewGuid().ToString();
            var refreshTokenTicket = OAuthHelper.CreateAuthTicket(context);

            var help = new OAuthHelper(guid, refreshTokenTicket);
            string serverName = OAuthHelper.GetServerName(context.OwinContext);

            help.RuajNeDb(serverName);
            context.SetToken(guid);
        }
        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            var hlp = new OAuthHelper(context.Token, OAuthHelper.GetServerName(context.OwinContext));
                var authTicket = hlp.GetAuthTicketByGuid();
                if (authTicket != null)
                    context.SetTicket(authTicket);
            
      
        }

        public void Create(AuthenticationTokenCreateContext context)
        {
            throw new NotImplementedException();
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            throw new NotImplementedException();
        }
    }
}