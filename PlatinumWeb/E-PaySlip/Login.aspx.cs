using DbCore;
using DbCore.DbAdmin;
using DbCore.DbListPagesat;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DbCore.IMBUtils.Security;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.Types;
using DbCore.IMBUtils.Extensions;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb.E_PaySlip
{
    public partial class Login : MyPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)//&& !Page.Session.IsNewSession
            {
                ASPxLabel8.Text = string.Format(@"© {0} IMB", DateTime.Now.Year);
                LabelInfo.ClientVisible = false;
                var combo = Login1.FindControl("cmbServerat") as DevExpress.Web.ASPxComboBox;
                //clsFunksione.percaktoTemplateCombo(combo);
                CacheLayer.GlobalCacheManager.MySessionCache["conStringName"] = "connStringAlpha";
                clsLogin.mbushServerCombo(Session.SessionID , combo);

           
                Login1.Focus();

                HtmlForm form = (HtmlForm)this.FindControl("form1");
                DevExpress.Web.ASPxButton LoginButton = (DevExpress.Web.ASPxButton)Login1.FindControl("LoginButton");
                string urlHelpi = DbCore.clsFunksione.ktheUrlHelpi("").Item1;
               // HelpLink.NavigateUrl = urlHelpi;

                if (form != null && LoginButton != null)
                {
                    form.DefaultButton = LoginButton.UniqueID;
                }
                System.Globalization.CultureInfo ci;
                DevExpress.Web.ASPxLabel PasswordRecoveryLink = Login1.FindControl("PasswordRecoveryLink") as DevExpress.Web.ASPxLabel;

                var FushaDomainName = clsServerConfiguration.LexoKonfigurimSipasKey<string>(ServerKonfigKey.DOMAINNAME);

                if (!string.IsNullOrEmpty(FushaDomainName))
                { PasswordRecoveryLink.ClientVisible = false; }
                
                string qsGjuha = Request.QueryString["gjuha"];
                int idGjuha = MerrIdGjuha();
                ci = MessagesResource.KtheCultureInfo(idGjuha);

                System.Resources.ResourceManager rm = MessagesResource.CurrentResourceManager;
                emertoKontrolletSipasGjuhes(rm, ci, PasswordRecoveryLink, LoginButton);
                string decryptedQuery = clsEnDecVodafone.dekriptoMesazh(Request.QueryString["enc"]);
                NameValueCollection myQuery = HttpUtility.ParseQueryString(decryptedQuery);
                if (Request.QueryString["enc"] != null)
                {
                    clsLogin.loginETopUpVod(HttpContext.Current,Request.QueryString["enc"], rm, ci, LabelInfo, Login1, (bool)Application["validInstall"], myQuery, true);
                }
                bool shfaqLinkResetPass = Convert.ToBoolean(System.Web.Configuration.WebConfigurationManager.AppSettings["shfaqLinkResetPass"]);

                if (shfaqLinkResetPass)
                {
                    PasswordRecoveryLink.Visible = shfaqLinkResetPass;
                    if (Request.QueryString["user"] != null) //tregon qe eshte klikuar linku i resetimit te passwordit
                    {
                        clsLogin.resetPass(PasswordRecoveryLink, shfaqLinkResetPass, Login1, Session, LabelInfo, Request, ci, Request.QueryString["user"], Request.QueryString["harroPw"], true, Request.QueryString["user"].ToString(),idGjuha);
                    }
                }
                else
                {
                    if (Request.QueryString["user"] != null || Request.QueryString["harroPw"] != null)
                    {
                        string sulm = rm.GetString("msgLoginResetimFjalekalimiIPaautorizuar", ci);
                        DbCore.DbAdmin.clsTrackUser.shtoUserLoginFail(sulm, this.Login1.UserName, Session.SessionID,Request.UserHostAddress); //Rasti kur po sulmohet per resetim pass dhe linku I resetimin te pass eshte I fshehur
                        LabelInfo.Text = sulm;
                        return;
                    }
                }
                DevExpress.Web.ASPxTextBox txtUsername = Login1.FindControl("UserName") as DevExpress.Web.ASPxTextBox; 
                if(Request.QueryString["user"] == null)// ne rastin kur useri ka kerkuar resetim pass, dhe faqja e kishte arsye=MbarimSessioni ne ate moment, atehere duhet te lihet ai mesazh dhe jo te mbishkruhet nga arsyeja
                    clsLogin.ArsyeLogin(rm,Request.QueryString["arsye"], LabelInfo, Session, ci);

            }
        }

        private void emertoKontrolletSipasGjuhes(System.Resources.ResourceManager rm, CultureInfo ci, DevExpress.Web.ASPxLabel PasswordRecoveryLink, DevExpress.Web.ASPxButton LoginButton)
        {
            //var combo = Login1.FindControl("cmbserverat") as ASPxComboBox;
            //combo.Items.Insert(0, new ListEditItem { Text = rm.GetString("serverKryesor", ci), Value = 0 });



            PasswordRecoveryLink.Text = rm.GetString("msgLoginKeniHaruuarFjalekalimin", ci);
            ASPxLabel9.Text = rm.GetString("msgLoginKontaktoPerNdihme", ci) + " | Tel : +355 4 22 53 466 / 4 22 55 121 / 4 22 55 123";
            ASPxLabel10.Text = rm.GetString("lblLoginKosove", ci) + " : +377 44177110";
            //ASPxLabel11.Text = rm.GetString("lblLoginIVizitoreve", ci);
            lblGjuhaAL.NavigateUrl = $"{DbCore.IMBUtils.Paths.loginPathEpaySlip}?gjuha=AL";
            lblGjuhaEN.NavigateUrl= $"{DbCore.IMBUtils.Paths.loginPathEpaySlip}?gjuha=EN";
            ASPxHiddenField loginHiddenField = Login1.FindControl("loginHiddenField") as ASPxHiddenField;


            loginHiddenField.Set("userlbl", rm.GetString("lblLoginPerdoruesi", ci));
            loginHiddenField.Set("srvlbl", rm.GetString("cmbzgjidhServerin", ci));
            loginHiddenField.Set("passlbl", rm.GetString("labelEmailFjalekalimi", ci));

            ASPxLabel lblHyrje = Login1.FindControl("lblHyrje") as ASPxLabel;
            //   lblHyrje.Text = rm.GetString("lblLoginHyrjeNeSistem", ci);
            if (ci.ToString().Equals("sq-AL"))
            {
                // LoginButton.ImageUrl = "~/images/FaqjaPare/buttonhyrje.jpg";
                LoginButton.Text = "Hyrje";

            }
            else
            {
                //LoginButton.ImageUrl = "~/images/FaqjaPare/buttonhyrje_eng.png";
                LoginButton.Text = "Login";
                //ASPxLabel2.Visible = false;

            }
            PasswordRecoveryLink.Text = rm.GetString("msgLoginKeniHaruuarFjalekalimin", ci);
        }



        private bool validateUser(string username, string paswd)
        {
            return true;
        }

        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {

            string qsGjuha = Request.QueryString["gjuha"];
            int idGjuha = MerrIdGjuha();
            mySessionObjects.ruajGjuhe(Session, idGjuha);

            var combo = Login1.FindControl("cmbServerat") as DevExpress.Web.ASPxComboBox;
            string connName;
            if (combo.SelectedItem != null)
            {
                if (combo.SelectedItem.Text == "Kryesor" || combo.SelectedItem.Text == "Main Server")
                    connName = "connStringAlpha";
                else
                    connName = combo.SelectedItem.Text;
                if (System.Configuration.ConfigurationManager.ConnectionStrings[connName] == null)
                {
                    Login1.FailureText = "Ju lutem, vendosni lidhjen per kete server!";
                    e.Authenticated = false;
                    return;
                }
                CacheLayer.GlobalCacheManager.MySessionCache["conStringName"] = connName;
            }
            clsPunonjes user = new clsPunonjes(this.Login1.UserName);
            if (clsLogin.loginAutentification(HttpContext.Current, this.Login1.UserName, this.Login1.Password, clientDate.Get("clientDate").ToString(), false, rm, ci, this.Login1, true, "", "", "", "", user))
            {
                clsMesazh mesazhPin = clsFunksione.dergoPinEpaySlip(Session, Login1.UserName, user.IdPunonjes, user.IdNdermarje);
                if (!mesazhPin.Status)
                {
                    Login1.FailureText = mesazhPin.PershkrimMesazhi;
                    e.Authenticated = false;
                    return;
                }
                Response.Redirect("OneTimePinForm.aspx");
            }
            else
            {
                NLog.LogManager.GetCurrentClassLogger().Error("Login1_Authenticate(object sender, AuthenticateEventArgs e) - Autentifikimi nuk u krye! clsLogin.loginAutentification() == false");
                e.Authenticated = false;
            }

        }

        private int MerrIdGjuha()
        {
            var qsGjuha = Converter.MerrVlereOseDefault<string>(Request.QueryString["gjuha"]);
            if (!string.IsNullOrWhiteSpace(qsGjuha))
                return "AL".EqualsAnyIgnoreCase(qsGjuha) ? 0 : 1;
            return clsServerConfiguration.LexoKonfigurimSipasKey<int>(ServerKonfigKey.Gjuha_Default);
        }

    }
}