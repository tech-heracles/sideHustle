using DbCore.IMBUtils.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Web;
using System.Web.Services;

namespace PlatinumWeb
{
    /// <summary>
    /// Summary description for WebService_mobile
    /// </summary>
    [WebService(Namespace = "http://alphaweb.al/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class WebService_mobile : System.Web.Services.WebService
    {
        [WebMethod]
        public string HelloWorld()
        {
            return "Yupiii!";
        }

        [WebMethod]
        public string Objekte()
        {
            object[] obj = new object[3];
            obj[0] = new { arg = "milk", val = 2, val1 = 1, val2 = 3 };
            obj[1] = new { arg = "soda", val = 3, val1 = 4, val2 = 5 };
            obj[2] = new { arg = "water", val = 4, val1 = 7, val2 = 7 };
            System.Web.Script.Serialization.JavaScriptSerializer serializues = new System.Web.Script.Serialization.JavaScriptSerializer();
            string serializeid = serializues.Serialize(obj);
            return serializeid;
        }

        [WebMethod]
        public string Ndermarrjet(string username, string password, string data)
        {
            object[] perd = kontrolloLogin(username, password, data);
            if (perd[0].ToString() == "po")
            {
                DbCore.DbAdmin.colNdermarrjet nderm = new DbCore.DbAdmin.colNdermarrjet();
                bool mbush = nderm.mbushNdermarrjetPerdoruesit(int.Parse(perd[0].ToString()));
                if (mbush)
                {
                    System.Web.Script.Serialization.JavaScriptSerializer serializues = new System.Web.Script.Serialization.JavaScriptSerializer();
                    string serializeid = serializues.Serialize(nderm);
                    return serializeid;
                }
                else return "S'ka ndermarrje per kete perdorues!";
            }
            else return "jo";
        }

        protected object[] kontrolloLogin(string username, string password, string data)
        {
            object[] obj = new object[2];
            string lejuar = "jo";
            DbCore.DbAdmin.clsPerdorues user = new DbCore.DbAdmin.clsPerdorues();
            DbCore.DbAdmin.colPerdoruesit colUsers = new DbCore.DbAdmin.colPerdoruesit();
            
            colUsers = DbCore.DbAdmin.clsPerdorues.merrUserNgaLogin(username);
            if (colUsers.Count == 0)
            {
                obj[0] = lejuar; obj[1] = 0;
                return obj;
            }

            string clientDt = data;
            DateTime clDate = DateTime.ParseExact(clientDt, "d/M/yyyy H:m:s", System.Globalization.CultureInfo.InvariantCulture);
            DateTime serverDate = DateTime.Now;
            TimeSpan span = clDate.Subtract(serverDate);
            if (span.Days > 1)
            {
                obj[0] = lejuar; obj[1] = 0;
                return obj;
            }

            int gjendetUser = colUsers.Count;
            if (gjendetUser != 0)
                user = colUsers.ElementAt(0);
            if (user.PerdoruesAktiv == false)
            {
                obj[0] = lejuar; obj[1] = 0;
                return obj;
            }
            if (!user.PerdoruesPassword.Equals(PasswordHelper.HashLogin(username, password)))
            {
                obj[0] = lejuar; obj[1] = 0;
                return obj;
            }

            if (user.PerdoruesPassword.Equals(PasswordHelper.HashLogin(username, password)))
            {
                user = colUsers.ElementAt(0);
                if (user.PerdoruesPassword.Equals(PasswordHelper.HashLogin(username, password)))
                {
                    lejuar = "po";
                    {
                        obj[0] = lejuar; obj[1] = 0;
                        return obj;
                    }                    
                }
                else
                {
                    obj[0] = lejuar; obj[1] = 0;
                    return obj;
                }
            }
            else
            {
                obj[0] = lejuar; obj[1] = 0;
                return obj;
            }
        }

        [WebMethod]
        public string login(string username, string password, string data)
        {
            string lejuar = "jo";
            DbCore.DbAdmin.clsPerdorues user = new DbCore.DbAdmin.clsPerdorues();
            DbCore.DbAdmin.colPerdoruesit colUsers = new DbCore.DbAdmin.colPerdoruesit();
            
            colUsers = DbCore.DbAdmin.clsPerdorues.merrUserNgaLogin(username);
            if (colUsers.Count == 0)
                return lejuar;

            string clientDt = data;
            DateTime clDate = DateTime.ParseExact(clientDt, "d/M/yyyy H:m:s", System.Globalization.CultureInfo.InvariantCulture);
            DateTime serverDate = DateTime.Now;
            TimeSpan span = clDate.Subtract(serverDate);
            if (span.Days > 1)
                return lejuar;

            int gjendetUser = colUsers.Count;
            if (gjendetUser != 0)
                user = colUsers.ElementAt(0);
            if (user.PerdoruesAktiv == false)
                return lejuar;
            if (!user.PerdoruesPassword.Equals(PasswordHelper.HashLogin(username, password)))
                return lejuar;

            if (user.PerdoruesPassword.Equals(PasswordHelper.HashLogin(username, password)))
            {
                user = colUsers.ElementAt(0);
                if (user.PerdoruesPassword.Equals(PasswordHelper.HashLogin(username, password)))
                {

                    lejuar = "po";
                }
                else
                    return lejuar;
            }
            return lejuar;
        }
    }
}