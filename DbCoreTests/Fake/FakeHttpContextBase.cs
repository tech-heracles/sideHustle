using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DbCore.IMBUtils.DataBase;
using System.Web.Configuration;
using DbCore.DbAdmin;

namespace DbCoreTests
{
    /// <summary>
    /// KJO CLASS INICIALIZON HTTPCONTEX DHE CHACHELAYER,DUHET TE TRASHEGOHET NE CDO KLASE TESTI
    /// </summary>
    public class FakeHttpContextBase
    {

        public FakeHttpContextBase()
        {
            HttpContext.Current = Utils.FakeHttpContext("http://dev.alphaweb.al");
            CacheLayer.CacheConfiguration.ConfigureCache(TimeSpan.FromMinutes(60));
            Utils.InitSessionCache(HttpContext.Current.Session.SessionID);
            //  MyConnectionsManager.SetSelectedConNameServer(HttpContext.Current.Session.SessionID, "Testim");
            //vendos ne connectrionString pool gjithe connection stringet 
            MyConnectionsManager.SetConnectionStringDefault(WebConfigurationManager.ConnectionStrings);
            MyConnectionsManager.InitializeConnectionStringsPool(colServerConnectionStrings.GetAllConnectionStringsAsDictionary());
            MyConnectionsManager.SetSelectedConNameServer(HttpContext.Current.Session.SessionID, "Testim");
        }

    }
}
