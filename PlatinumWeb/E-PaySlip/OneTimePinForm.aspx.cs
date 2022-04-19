using DbCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb.E_PaySlip
{
    public partial class OneTimePinForm : MyPageBase
    {
        public const string ONETIMEPINFORM_TIMER_START_DATE = "ONETIMEPINFORM_TIMER_START_DATE";
        public static System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
        System.Resources.ResourceManager rm => new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
        private static int milisekonda = 180000;
        System.Globalization.CultureInfo ci => DbCore.mySessionObjects.ktheCultureInfo(Session);

        protected void Page_Load(object sender, EventArgs e)
        {



            if (!IsPostBack)
            {

                mySessionObjects.RuajNeSession<bool>(Session, false, "EPaySlipPinAuthentification");
                if (!DbCore.mySessionObjects.isLogedIn(Session))
                {
                    var cookie = Request.Cookies["adresa"];
                    if (cookie == null)
                    {
                        cookie = new HttpCookie("adresa");
                    }
                    cookie.Value = DbCore.IMBUtils.Paths.loginPathEpaySlip;
                    cookie.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Add(cookie);
                    clsFunksione.logout(Session, true, "FaqePaautorizuar");

                };

                vendosEmraLabela();
                Start();

            }


        }



        protected void Start()
        {
            string key = $"{Session.SessionID}_{ONETIMEPINFORM_TIMER_START_DATE}";
            object date = HttpRuntime.Cache[key];
            if (date == null)
            {

                milisekonda = 180000;
                Label1.Text = "03:00";


                HttpRuntime.Cache.Insert(key, DateTime.Now, null, DateTime.Now.AddMinutes(5), System.Web.Caching.Cache.NoSlidingExpiration);
            }
            else
            {
                milisekonda = 180000 - Convert.ToInt32((DateTime.Now - (DateTime)date).TotalMilliseconds);
                if (milisekonda <= 0)
                {
                    //              var ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
                    var gjuha = ci.Name == "sq-AL" ? "AL" : "EN";
                    Response.Redirect($"{DbCore.IMBUtils.Paths.loginPathEpaySlip}?arsye=skaduarPin&gjuha={gjuha}");
                }
                Label1.Text = getTimeLeft();
            }
            sw.Start();
        }


        protected void vendosEmraLabela()
        {

            //  dalje.Text = "Log out";
            dalje.InnerText = rm.GetString("lblDalje", ci);
            DevExpress.Web.ASPxButton LoginButton = (DevExpress.Web.ASPxButton)KodHyresForm.FindControl("butonPerTeHyre");
            LoginButton.Text = rm.GetString("lblHyrje", ci);
            KodHyresForm.FailureText = rm.GetString("msgPinIPasakteProvoniPerseri", ci);

        }
        protected void Login2_Authenticate(object sender, AuthenticateEventArgs e)
        {
            DevExpress.Web.ASPxTextBox txtPin = KodHyresForm.FindControl("fusheKodi") as DevExpress.Web.ASPxTextBox;
            var dergoPinMeSMS = DbCore.DbAdmin.clsServerConfiguration.LexoKonfigurimSipasKey<int>(DbCore.DbAdmin.ServerKonfigKey.DERGO_PIN_SMS);
            if (dergoPinMeSMS != 1 || DbCore.DbAdmin.clsGjenerimPIN.eshteIVlefshemPIN(txtPin.Text, DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DateTime.Now))
            {
                //e.Authenticated = true;
                DbCore.clsFunksione.krijoTicket(Session, DbCore.mySessionObjects.ktheEmerPerdorues(Session));
                string key = $"{Session.SessionID}_{ONETIMEPINFORM_TIMER_START_DATE}";
                HttpRuntime.Cache.Remove(key);
                mySessionObjects.RuajNeSession<bool>(Session, true, "EPaySlipPinAuthentification");
                Response.Redirect("ListaRaporte.aspx?vjenNga=Pini");
            }
            else
            {
                NLog.LogManager.GetCurrentClassLogger().Error("DbCore.DbAdmin.clsGjenerimPIN.eshteIVlefshemPIN(...) == false! Nuk eshte i vleshem autentifimi i PIN-it ");
                //e.Authenticated = false;
            }
        }

        protected string getTimeLeft()
        {
            var timeSpan = TimeSpan.FromMilliseconds(Convert.ToDouble(milisekonda));
            string Sec = string.Empty;
            string Min = string.Empty;

            var seconds = timeSpan.Seconds;
            var minutes = timeSpan.Minutes;
            if (seconds.ToString().Length.Equals(1))
            {
                Sec = "0" + seconds.ToString();
            }
            else
            {
                Sec = seconds.ToString();
            }
            if (minutes.ToString().Length.Equals(1))
            {
                Min = "0" + minutes.ToString();
            }
            else
            {
                Min = minutes.ToString();
            }

            return Min + ":" + Sec;
        }

        protected void tm1_Tick(object sender, EventArgs e)
        {
            if (sw != null)
            {
                milisekonda = milisekonda - 1000;
                
                if (milisekonda <= 0)
                {
                    var ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
                    Label1.Visible = false;
                    Label2.Visible = true;
                    Label2.Text = rm.GetString("msgKohaKaMbaruar", ci);
                    sw.Stop();
                    var gjuha = ci.Name == "sq-AL" ? "AL" : "EN";
                    Response.Redirect($"{DbCore.IMBUtils.Paths.loginPathEpaySlip}?arsye=skaduarPin&gjuha={gjuha}");
                    return;
                }
                else
                {
                    Label1.Text = getTimeLeft();
                }
                

            }
        }

        protected void tm1_Init(object sender, EventArgs e)
        {

        }
    }
}