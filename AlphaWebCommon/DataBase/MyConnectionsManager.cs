using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Web;
using AlphaWeb.Infrastructure.Data.AdoNet;
using CacheLayer;
using DbCore.IMBUtils.Logging;

namespace DbCore.IMBUtils.DataBase
{
    /// <summary>
    /// Wraper mbi cache per te menaxhuar listen e serverave mbi db.
    /// ruan/modifikon serverin e zgjedhur nga perdoruesi ne login
    /// </summary>
    public class MyConnectionsManager
    {
        public const string ConStringNameCacheKey = "conStringName";
        public const string ConnStringNameDefault = "connStringAlpha";
        public const string ServeraCacheKey = "servera";



        public static void SetConnectionStringDefault(ConnectionStringSettingsCollection connStrings)
        {
            try
            {
                var connectionStringDefault = connStrings[ConnStringNameDefault].ConnectionString;

                ConnectionStringsManager.Instance.SetConnectionStrings(new Dictionary<string, string>
                {
                    { ConnStringNameDefault, connectionStringDefault }
                });

            }
            catch (Exception ex)
            {
                throw new MyException($"Nuk u vendos connectionstring default..Quick Tip:kontrollo nese ekziston nje connectionstring me emrin {ConnStringNameDefault} ne web.config", ex);
            }
        }
        private static string GetConnectionNameFromClaims()
        {
            var claims = HttpContext.Current.User.Identity as ClaimsIdentity;
            if (claims == null)
                throw new MyException("HttpContext.Current.User.Identity as ClaimsIdentity nuk eshte e vlefshme!");
            var connStringClaim = claims.Claims.FirstOrDefault(x => x.Type == ConStringNameCacheKey);
            if (string.IsNullOrWhiteSpace(connStringClaim?.Value))
                throw new MyException("CONNSTRING NOT FOUND in claims!!!!");
            return connStringClaim.Value;
        }
        /// <summary>
        /// ruan ne cache listen me te gjithe db serverat e vlefshem
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="dt"></param>
        public static void SetListServera(string sessionId, DataTable dt)
        {
            GlobalCacheManager.GetSessionCacheByKey(sessionId)[ServeraCacheKey] = dt;
        }
        public static DataTable GetListServera(string sessionId)
        {
            return GlobalCacheManager.GetSessionCacheByKey(sessionId).Get<DataTable>(ServeraCacheKey);
        }

        public static void SetSelectedConNameServer(string conName)
        {
            if (string.IsNullOrWhiteSpace(conName))
                return;
            if (HttpContext.Current == null)
                throw new MyException("Mungon konteksti i kerkeses!");
            if (HttpContext.Current.Session == null)
                throw new MyException("Mungon sessioni i kerkeses !");
            SetSelectedConNameServer(HttpContext.Current.Session.SessionID, conName);
        }
        public static void SetSelectedConNameServer(string sessionId, string conName)
        {
            GlobalCacheManager.GetSessionCacheByKey(sessionId).Set(ConStringNameCacheKey, conName, false, false);
        }
        /// <summary>
        /// kthen connectionName ne baze te kontekstit te kerkeses ,
        /// shfrytezon HttpContex.Current per te marr sessionID ne menyre implicite
        /// </summary>
        /// <returns></returns>
        public static string GetSelectedConNameServer()
        {

            if (HttpContext.Current == null)
                throw new MyException("Mungon konteksti i kerkeses!",false);
            //nese si menyre autentikimi eshte zgjedhur Bearer ath connectionstring duhet te merret nga claims
            if (HttpContext.Current.User?.Identity.AuthenticationType == "Bearer")
                return GetConnectionNameFromClaims();
            if (HttpContext.Current.Session == null)
                throw new MyException("Mungon sessioni i kerkeses !", false);
            return GetSelectedConNameServer(HttpContext.Current.Session.SessionID);
        }
        /// <summary>
        /// vendos constring ne listen e conn per db managerin
        /// </summary>   
        public static void InitializeConnectionStringsPool( IDictionary<string, string> connectionStringsFromDb)
        {
           ConnectionStringsManager.Instance.SetConnectionStrings(connectionStringsFromDb);
        }
        public static IEnumerable<string> GetPoolConnectionNames()
        {
            return ConnectionStringsManager.Instance.GetConnectionStrings().Keys;
        }

        public static void RefreshConnectionStringsPool(IDictionary<string, string> connectionStringsFromDb, string connStringNameDefault)
        {
            ConnectionStringsManager.Instance.RefreshConnectionStrings(connectionStringsFromDb, connStringNameDefault);
        }

        /// <summary>
        /// merr constrName nga SessionCache sipas sessionit
        /// </summary>
        /// <returns></returns>
        public static string GetSelectedConNameServer(string sessionId)
        {
            var myCache = GlobalCacheManager.GetSessionCacheByKey(sessionId);
            var connString = myCache.Get<string>(ConStringNameCacheKey, false);
            if (string.IsNullOrEmpty(connString))
                return ConnStringNameDefault;  // throw new MyException("CONNSTRING NOT FOUND!");
            return connString;
        }
        public static bool IsConnectionAvailable(string connName)
        {
            try
            {
                return !string.IsNullOrEmpty(ConnectionStringsManager.Instance.GetConnectionString(connName));
            }
            catch (Exception ex)
            {
                ImbLogger.Error($"Mungon connstring '{connName}'", ex);
                return false;
            }
        }
    }

}
