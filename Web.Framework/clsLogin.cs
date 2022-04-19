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
using DbCore.IMBUtils.Logging;
using System.Resources;

namespace PlatinumWeb
{
    public class clsLogin
    {
        public static void mbushServerCombo(string session, ASPxComboBox combo)
        {
            DataTable data = DbCore.DbAdmin.clsLicenca.MerrLicencatMeDB(MyConnectionsManager.ConnStringNameDefault);

            MyConnectionsManager.SetListServera(session, data);
            //if (data.Rows.Count <= 1)
            //{
            //    combo.ClientVisible = false;
            //    combo.Enabled = false;
            //}

            combo.DataSource = data;
            combo.TextField = "KODLICENCA";
            combo.ValueField = "IDLICENCA";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }
        public static void mbushServerComboID(string session, ASPxComboBox combo, int value)
        {


            // DataTable data = DbCore.DbAdmin.clsLicenca.MerrLicencatMeDB(MyConnectionsManager.ConnStringNameDefault);


            DataTable data = DbCore.DbAdmin.clsLicenca.MerrLicencatMeDBMeID(MyConnectionsManager.ConnStringNameDefault, value);

            MyConnectionsManager.SetListServera(session, data);
            //if (data.Rows.Count <= 1)
            //{
            //    combo.ClientVisible = false;
            //    combo.Enabled = false;
            //}

            combo.DataSource = data;
            combo.TextField = "KODLICENCA";
            combo.ValueField = "IDLICENCA";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }


        public static clsMesazh setServer(string session, int idLicenca)
        {
            var dtServera = MyConnectionsManager.GetListServera(session);
            if (dtServera == null)
            {
                dtServera = clsLicenca.MerrLicencatMeDB(MyConnectionsManager.ConnStringNameDefault);
                MyConnectionsManager.SetListServera(session, dtServera);
            }
            //if (dtServera.Rows.Count == 1)
            //{
            //    MyConnectionsManager.SetSelectedConNameServer(session, MyConnectionsManager.ConnStringNameDefault);
            //    return new clsMesazh(true, "U zgjodh connstring default");
            //}
            var lic = dtServera.Select("IDLICENCA = " + idLicenca).FirstOrDefault();
            if (lic == null)
            {
                dtServera = clsLicenca.MerrLicencatMeDBMeID(MyConnectionsManager.ConnStringNameDefault, idLicenca);//licenca mund te mos jete ne dt fillestar qe mbush combon, por eshte zgjedhur duke filtruar
                lic = dtServera.Select("IDLICENCA = " + idLicenca).FirstOrDefault();
                if (lic == null)
                    return new clsMesazh(false, "Ju lutem, vendosni lidhjen per kete server!");
            } 
            var conName = lic["DATABASE"] as string;
            if (conName == null)
                return new clsMesazh(false, "Ju lutem, vendosni lidhjen per kete server!");
            if (!MyConnectionsManager.IsConnectionAvailable(conName))
                return new clsMesazh(false, "Ju lutem, vendosni lidhjen per kete server!");
            MyConnectionsManager.SetSelectedConNameServer(session, conName);
            return new clsMesazh(true, "u vendos me sukses");
        }

        /// <summary>
        /// Metode qe perdoret vetem per VODAFONE, per log-in nga eTopUp.
        /// </summary>
        /// <param name="enc">String e enkriptuar e parametrave</param>
        public static void loginETopUpVod(HttpContext httpContext, string enc, System.Resources.ResourceManager rm, CultureInfo ci, ASPxLabel label, System.Web.UI.WebControls.Login Login1, bool validInstall, NameValueCollection myQuery, bool punonjes)
        {
            try
            {

                int sekondaTeToleruara = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["toleroSekonda"]);
                if (myQuery["username"] != "" && myQuery["webService"] != "")
                {
                    TimeSpan diffTime = DateTime.Now - Convert.ToDateTime(myQuery["dataLogin"], new System.Globalization.CultureInfo("en-us", false));
                    if (diffTime.TotalSeconds > sekondaTeToleruara)
                    {
                        label.Text = rm.GetString("msgLoginLinkuPerSingleSignONJoIVlefshem", ci);
                        clsFunksione.logout(httpContext.Session, false, true, true);
                        return;
                    }
                    if (loginAutentification(httpContext, myQuery["username"], myQuery["password"], myQuery["dataLogin"], Boolean.Parse(myQuery["webService"]), rm, ci, Login1, punonjes, myQuery["ndermarrja"], myQuery["ipkasa"], myQuery["emerprinteri"], myQuery["dyqani"]))
                        clsFunksione.avancoPerpara(httpContext.Response, httpContext.Session, mySessionObjects.ktheIdPerdoruesi(httpContext.Session), rm, ci, validInstall);
                    else
                    {
                        label.Text = rm.GetString("msgLoginNukUKryeLogimi", ci);
                        clsFunksione.logout(httpContext.Session, false, true, true);
                    }
                }
            }
            catch (DbCore.MyException m)
            {
                ImbLogger.Error(m);
                label.Text = m.Message;
                clsFunksione.logout(httpContext.Session, false, true, true);
            }
            catch (Exception m)
            {
                ImbLogger.Error(m);
                label.Text = rm.GetString("msgLoginLinkuPerSingleSignONJoIVlefshem", ci);
                clsFunksione.logout(httpContext.Session, false, true, true);
            }
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
                //if (Request.QueryString["em"] != null)
                //{
                //    LabelInfo.Text = DbCore.EmailComposer.DergoEmailNewPassword(Request.QueryString["em"], HttpContext.Current.Request, true).PershkrimMesazhi;
                //    return;
                //}

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