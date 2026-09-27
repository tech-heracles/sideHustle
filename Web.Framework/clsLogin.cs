using CacheLayer;
using DbCore;
using DbCore.DbAdmin;
using DbCore.DbListPagesat;
using DevExpress.Utils.OAuth.Provider;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Licencimi;
using DbCore.IMBUtils.Logging;
using System.Resources;

namespace PlatinumWeb
{
    public class clsLogin
    {
        /// <summary>
        /// Mbush listen e kompanive ne login nga licencat ne Firebase (<see cref="LicencatAvec"/>).
        /// Kthen null, ose mesazhin per perdoruesin kur lista nuk mund te merret.
        /// </summary>
        public static string mbushServerCombo(string session, ASPxComboBox combo)
        {
            combo.TextField = "Emri";
            combo.ValueField = "Id";
            combo.ValueType = typeof(string);
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            try
            {
                combo.DataSource = LicencatAvec.Merr().Where(l => l.Aktive).OrderBy(l => l.Emri).ToList();
                combo.DataBind();
                return null;
            }
            catch (LicencaAvecException ex)
            {
                combo.DataSource = new List<LicencaAvec>();
                combo.DataBind();
                return ex.Message;
            }
        }

        /// <summary>
        /// Vendos per sesionin lidhjen me databazen e kompanise se zgjedhur, nese licenca e saj e lejon sot.
        /// </summary>
        public static clsMesazh setServer(string session, string idKompanie)
        {
            if (string.IsNullOrWhiteSpace(idKompanie))
                return new clsMesazh(false, "Zgjidhni kompanine.");
            try
            {
                return vendosLidhjen(session, LicencatAvec.Gjej(idKompanie));
            }
            catch (LicencaAvecException ex)
            {
                return new clsMesazh(false, ex.Message);
            }
        }

        /// <summary>
        /// Si <see cref="setServer"/>, per sherbimet qe e dergojne kompanine me emer ("organization") ose me id.
        /// </summary>
        public static clsMesazh setServerFromOrgName(string session, string orgName)
        {
            try
            {
                return vendosLidhjen(session, LicencatAvec.GjejSipasIdOseEmrit(orgName));
            }
            catch (LicencaAvecException ex)
            {
                return new clsMesazh(false, ex.Message);
            }
        }

        private static clsMesazh vendosLidhjen(string session, LicencaAvec licenca)
        {
            if (licenca == null)
                return new clsMesazh(false, "Kjo kompani nuk ekziston ne kete server.");
            string gabimi = licenca.Kontrollo(DateTime.Now);
            if (gabimi != null)
                return new clsMesazh(false, gabimi);
            MyConnectionsManager.SetSelectedConNameServer(session, licenca.EmriLidhjes);
            return new clsMesazh(true, "u vendos me sukses");
        }

        public static bool loginAutentification(HttpContext httpContext, string username, string pass, string data, bool webServise, System.Resources.ResourceManager rm, System.Globalization.CultureInfo ci, System.Web.UI.WebControls.Login Login1, bool punonjes, string ndermarrjaWS = "", string ipKasaWS = "", string emerPrinteriWS = "", string dyqaniWS = "", clsPunonjes user = null)
        {
            clsMesazh mesazh = new clsMesazh();
            if (punonjes)
            {
                mesazh = DbCore.clsFunksione.validoPunonjesinNeLogin(httpContext, username, pass, Login1.RememberMeSet, data, webServise, rm, ci, user, ndermarrjaWS, ipKasaWS, emerPrinteriWS, dyqaniWS);
            }
            else
                mesazh = DbCore.clsFunksione.validoPerdoruesinNeLogin(httpContext, username, pass, Login1.RememberMeSet, data, webServise, rm, ci, ndermarrjaWS, ipKasaWS, emerPrinteriWS, dyqaniWS, false);

            if (!mesazh.Status)
            {
                Login1.FailureText = mesazh.PershkrimMesazhi;
                return false;
            }

            
            return true;
        }

        public static void resetPass(ASPxLabel PasswordRecoveryLink, bool shfaqLinkResetPass, System.Web.UI.WebControls.Login Login1, HttpSessionState Session, ASPxLabel LabelInfo, HttpRequest Request, System.Globalization.CultureInfo ci, String user, String harroPw, bool punonjes, string username, int idgjuha)
        {

            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (shfaqLinkResetPass)
            {
                PasswordRecoveryLink.Visible = shfaqLinkResetPass;
                if (Request.QueryString["user"] != null) //tregon qe eshte klikuar linku i resetimit te passwordit
                {
                    if (!username.Equals(""))

                    {

                        if (punonjes)
                        {
                            clsMesazh resetPassMesazh = clsFunksione.dergoVerificationLink(username, true, idgjuha);
                            LabelInfo.Text = resetPassMesazh.PershkrimMesazhi;
                            return;

                        }
                        else
                        {
                            clsMesazh resetPassMesazh = clsFunksione.dergoVerificationLink(username, false, idgjuha);


                            if (resetPassMesazh.KodMesazhi == 1) //kodmesazhi 1 ne rastin kur licenca e perdoruesit nuk lejon resetimin e passwordit per perdoruesit e saj te percaktuar te konfigurimet e fjalekalimit.
                            {
                                PasswordRecoveryLink.Enabled = false;
                                PasswordRecoveryLink.Visible = false;
                            }
                            LabelInfo.Text = resetPassMesazh.PershkrimMesazhi;
                            return;
                        }

                    }
                    else
                    {
                        LabelInfo.Text = rm.GetString("msgLoginPlotesoPerodruesin", ci);
                        return;
                    }

                }

            }
            else
            {

                if (user == null || harroPw != null)
                {
                    string sulm = rm.GetString("msgLoginResetimFjalekalimiIPaautorizuar", ci);
                    DbCore.DbAdmin.clsTrackUser.shtoUserLoginFail(sulm, Login1.UserName, Session.SessionID, HttpContext.Current.Request.UserHostAddress); //Rasti kur po sulmohet per resetim pass dhe linku I resetimin te pass eshte I fshehur
                    LabelInfo.Text = sulm;
                    return;
                }
            }

        }
        public static void ArsyeLogin(ResourceManager rm, String arsye, ASPxLabel labelInfo, HttpSessionState Session, CultureInfo ci)
        {
            if (arsye != null)
            {
                string mesazhErrori = "";
                if (arsye.Contains("error"))
                    mesazhErrori = arsye.Split('_')[1];
                if (!mesazhErrori.Equals(""))
                {
                    labelInfo.Text = mesazhErrori;
                    clsFunksione.logout(Session, true, true, true);
                    return;
                }
                switch (arsye)
                {
                    case "FaqePaautorizuar":
                        labelInfo.Text = rm.GetString("msgLoginNukKeniTeDrejtaPerTuLoguarNeFaqe", ci);
                        return;
                    case "perfundoiLicenca":
                        labelInfo.Text = rm.GetString("msgLoginLicencaKaSkaduar", ci);
                        return;
                    case "problemLicenca":
                        labelInfo.Text = rm.GetString("msgLoginProblemLicence", ci);
                        return;
                    case "MbarimSessioni":
                        labelInfo.Text = rm.GetString("msgLoginSessionKaMbaruar", ci);
                        return;
                    case "logout":
                        labelInfo.Text = rm.GetString("msgLoginLidhjaUShkeputMeSukses", ci);
                        clsFunksione.logout(Session, true, true, true);
                        return;
                    case "double":
                        labelInfo.Text = rm.GetString("msgLoginLidhjaUShkeputSeULoguatNeVendTjeter", ci);
                        return;
                    case "aprovimDokNdermarrjeGabuar":
                        labelInfo.Text = rm.GetString("msgAprovimDokNdermarrjeGabuar", ci);
                        return;
                    case "skaduarPin":
                        clsFunksione.logout(Session, true, true, true);
                        labelInfo.Text = rm.GetString("msgKohaKaMbaruar", ci);
                        break;
                    case "failSendigPinEpayslip":
                        clsFunksione.logout(Session, true, true, true);
                        labelInfo.Text = rm.GetString("msgGabimGjenerimPin",ci);
                        return;
                    case "LinkIPavlefshem":
                        labelInfo.Text = rm.GetString("linkJoIVlefshem", ci);
                        return;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// ben logout perdoruesit
        /// </summary>
        /// <param name="Session"></param>
        public static void signOutUser(string sessionID)
        {
            var onlUser = new clsTrackUser(MyConnectionsManager.GetSelectedConNameServer(sessionID))
            {
                SessionID = sessionID,
                LogoutDatetime = DateTime.Now.ToString()
            };
            onlUser.modifiko();
        }
        /// <summary>
        /// pastron cache per kete session
        /// </summary>
        /// <param name="sessionID"></param>
        public static void ClearSessionCache(string sessionID)
        {
            var connString = MyConnectionsManager.GetSelectedConNameServer(sessionID);
            GlobalCacheManager.DestroySessionCache(sessionID);
            MyConnectionsManager.SetSelectedConNameServer(sessionID, connString);
        }
    }
}