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
using DbCore.IMBUtils.Licencimi;

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

        /// <summary>
        /// Lidhja per token-in: "serverName" eshte id-ja e kompanise ne licencat AVEC (ose emri i saj).
        /// Pa kompani perdoret e vetmja kompani e instalimit, nese ka vetem nje. Kthen null kur kompania
        /// nuk gjendet ose licenca e saj nuk lejon hyrjen sot.
        /// </summary>
        public static string GetServerName(IOwinContext context)
        {
            var serverName = context.Get<string>("serverName");
            try
            {
                LicencaAvec licenca;
                if (string.IsNullOrWhiteSpace(serverName) || serverName == "Kryesor")
                {
                    var aktive = LicencatAvec.Merr().Where(l => l.Aktive).ToList();
                    licenca = aktive.Count == 1 ? aktive[0] : null;
                }
                else
                    licenca = LicencatAvec.GjejSipasIdOseEmrit(serverName);
                return licenca != null && licenca.Kontrollo(DateTime.Now) == null ? licenca.EmriLidhjes : null;
            }
            catch (LicencaAvecException)
            {
                return null;
            }
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