using DbCore;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DbCore.DbAuth;
using Microsoft.Owin.Security.DataHandler.Serializer;
using System.Text;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Security;
using DbCore.IMBUtils.Logging;

namespace PlatinumWeb.OWIN.Helpers
{
    public class OAuthHelper
    {
        string _guid;
        string _authTicketSerializuar;

        public string Guid
        {
            get { return _guid; }
            set { _guid = value; }
        }

        public string AuthTicketSerializuar
        {
            get { return _authTicketSerializuar; }
            set { _authTicketSerializuar = value; }
        }

        public OAuthHelper(string guid, AuthenticationTicket authTicket)
        {
            _guid = guid;
            var serializer = new TicketSerializer();
            _authTicketSerializuar = Convert.ToBase64String(serializer.Serialize(authTicket));
        }
        public OAuthHelper(string guid, string serverName)
        {
            _guid = guid;
            _authTicketSerializuar = MerrAuthTicketNgaDb(serverName);
        }

        public clsMesazh RuajNeDb(string serverName)
        {
            try
            {
                return new clsAuthRefreshToken(serverName)
                {
                    Guid = _guid,
                    AuthenticationTicket = _authTicketSerializuar
                }.Ruaj();

            }
            catch (Exception ex)
            {

                ImbLogger.Error(ex);
                return new MesazhGabimi();
            }

        }
        public AuthenticationTicket GetAuthTicketByGuid()
        {
            if (!string.IsNullOrEmpty(_authTicketSerializuar))
            {
                var serializer = new TicketSerializer();
                return serializer.Deserialize(Convert.FromBase64String(_authTicketSerializuar));
            }
            return null;
        }

        public static string GetServerName(IOwinContext context)
        {
            var serverName = context.Get<string>("serverName");

            if (serverName == "Kryesor" || string.IsNullOrWhiteSpace(serverName))
                serverName = MyConnectionsManager.ConnStringNameDefault;
            return serverName;
        }
        public static AuthenticationTicket CreateAuthTicket(AuthenticationTokenCreateContext context)
        {
            var refreshTokenProperties = new AuthenticationProperties(context.Ticket.Properties.Dictionary)
            {
                IssuedUtc = context.Ticket.Properties.IssuedUtc,
                ExpiresUtc = DateTime.UtcNow.AddDays(1),
            };
            return new AuthenticationTicket(context.Ticket.Identity, refreshTokenProperties);
        }
        private string MerrAuthTicketNgaDb(string serverName)
        {
            return new clsAuthRefreshToken(serverName)
            {
                Guid = _guid

            }.MerrAuthTicket();
        }
    }

}