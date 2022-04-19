using DevExpress.Web;
using System;
using System.Web.UI;
using System.Globalization;
using System.Resources;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class PolitikaFjalekalimi : MyPageBase
    {

         private int idGjuha; 
           
         private CultureInfo ci;
         private ResourceManager rm;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                DbCore.clsFunksione.logout(Session,true,"FaqePaautorizuar");
            }
            int idPerd = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerd);
            }
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            percaktoTemplateMenu(ASPxMenu1, idViti, idPerd, idNdermarrje);
            if (!IsPostBack)
                konfiguroVleraFillestare(idPerd);

            idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            ci = DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo(idGjuha);
            rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            vendosPerkthimet();
        }

        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        }

        /// <summary>
        /// metoda per te thirrur veprimet e menuse kur shtypen butonat
        /// </summary>
        /// <param name="source">derguesi</param>
        /// <param name="e">parametrat</param>
        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {
            if (e.Item.Name == "Ruaj")
            {
                Page.Validate("entries");
                ruajKonfigurimFjalekalimi();
            }
        }

        private void ruajKonfigurimFjalekalimi()
        {
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idPerdorues = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            if (Page.IsValid)
            {
                if (kontrolloKonfigurim())
                {
                    DbCore.DbAdmin.clsKonfigurimeFjalekalimi konfigFjalekalimiRi = new DbCore.DbAdmin.clsKonfigurimeFjalekalimi(0, cbRuajHistorikun.Checked, Convert.ToInt32(spinDiteHistoriku.Text), cbPassKompleks.Checked, cbPassDetyrueshem.Checked, Convert.ToInt32(seGjatesiMin.Text), Convert.ToInt32(sePassSkadonPas.Text), cbBllokoUser.Checked, Convert.ToInt32(seBllokoPas.Text), cbBllokoLogimin.Checked, idPerdorues, Convert.ToInt32(seMaxSession.Value), cbPassSkadon.Checked, cbResetPass.Checked, cbGjeneroPassword.Checked, Convert.ToInt32(seNrKaraktereSpeciale.Text), Convert.ToInt32(seNrShkronjaveKapitale.Text), Convert.ToInt32(seNrKaraktereveNumra.Text), cb2fact.Checked);
                    DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                    DbCore.DbAdmin.clsKonfigurimeFjalekalimi konfigurimVjeterFjalekalimi = new DbCore.DbAdmin.clsKonfigurimeFjalekalimi();
                    int idLicenca = DbCore.DbAdmin.clsKonfigurimeFjalekalimi.kaKonfigurimPerLicencen(idPerdorues);
                    if (idLicenca != 0)//ka konfigurim dhe do modifikohet ekzistuesi.
                    {
                        mesazh = konfigFjalekalimiRi.modifiko(idLicenca);
                    }
                    else//nuk ka konfigurim dhe do krijohet nga fillimi.
                    {
                        mesazh = konfigFjalekalimiRi.ruaj();
                    }

                    if (mesazh.Status)
                        clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("msgRuajtjaEKonfigurimit", ci), pnlMesazhi);
                    else
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgGabimNeRuajtjeKonfigurimi", ci), pnlMesazhi);
                }
            }
        }

        /// <summary>
        /// validon vlerat e plotesuara per konfigurimin
        /// </summary>
        /// <returns></returns>
        private bool kontrolloKonfigurim()
        {
            string mesazh;
            if (cbRuajHistorikun.Checked)
            {
                if (spinDiteHistoriku.Value == null)
                {
                    mesazh = rm.GetString("msgRuajHistorikun", ci);
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh, pnlMesazhi);
                    return false;
                }
            }
            if (spinDiteHistoriku.Value != null)
            {

                if (Convert.ToInt32(spinDiteHistoriku.Value) < 0)
                {
                    mesazh = rm.GetString("msgHistorikPozitiv", ci);
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh, pnlMesazhi);
                    return false;
                }
            }
            if (cbPassSkadon.Checked)
            {
                if (sePassSkadonPas.Value == null)
                {
                    mesazh = rm.GetString("msgNumriIDiteveTeSkadimit", ci);
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh, pnlMesazhi);
                    return false;
                }
            }
            if (sePassSkadonPas.Value != null)
            {
                if (Convert.ToInt32(sePassSkadonPas.Value) < 0)
                {
                    mesazh = rm.GetString("msgSkadencePozitive", ci);
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh, pnlMesazhi);
                    return false;
                }
            }
            if (cbBllokoUser.Checked)
            {
                if (seBllokoPas.Value == null)
                {
                    mesazh = rm.GetString("msgNumerTentativazh", ci);
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh, pnlMesazhi);
                    return false;

                }
                if (seMaxSession.Value == null)
                {
                    mesazh = rm.GetString("msgNumerSesionesh", ci);
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh, pnlMesazhi);
                    return false;
                }
            }
            if (seBllokoPas.Value != null)
            {
                if (Convert.ToInt32(seBllokoPas.Value) < 0)
                {
                    mesazh = rm.GetString("msgTentativaPozitive", ci);
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh, pnlMesazhi);
                    return false;
                }
            }
            if (seGjatesiMin.Value != null)
            {
                int gjatesiMinPass = Convert.ToInt32(seGjatesiMin.Value);
                if (gjatesiMinPass < 0)
                {
                    mesazh = rm.GetString("msgFjalekalimPozitiv", ci);
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh, pnlMesazhi);
                    return false;
                }
                if (gjatesiMinPass < (Convert.ToInt32(seNrKaraktereSpeciale.Text) + Convert.ToInt32(seNrKaraktereveNumra.Text) + Convert.ToInt32(seNrShkronjaveKapitale.Text)))
                {
                    mesazh = rm.GetString("msgFjalekalimLajmerim", ci);
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh, pnlMesazhi);
                    return false;
                }
            }
            if (seMaxSession.Value == null)
            {
                if (Convert.ToInt32(seMaxSession.Value) < 0)
                {
                    mesazh = rm.GetString("msgSessionPozitiv", ci);
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh, pnlMesazhi);
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// mbushet konfigurimin e fjalekalimit sipas perdoruesit te loguar
        /// </summary>
        /// <param name="idPerdoruesi"></param>
        private void konfiguroVleraFillestare(int idPerdoruesi)
        {
            DbCore.DbAdmin.clsKonfigurimeFjalekalimi konf = new DbCore.DbAdmin.clsKonfigurimeFjalekalimi(idPerdoruesi);
            konf.mbushKonfigurimSipasPerdoruesit(idPerdoruesi);
            if (konf.IdKonfigurimePassword > 0)
            {
                cbRuajHistorikun.Checked = konf.RuajHistorikunPass;
                spinDiteHistoriku.Text = Convert.ToString(konf.NrHereRuajHistorikPass);
                cbPassKompleks.Checked = konf.KomplexPassword;
                cbPassDetyrueshem.Checked = konf.NdryshimPasswordiDetyruar;
                seGjatesiMin.Text = Convert.ToString(konf.GjatesiaMinPassword);
                sePassSkadonPas.Text = Convert.ToString(konf.DiteSkadimiPassword);
                cbBllokoUser.Checked = konf.BllokoPerdorues;
                seBllokoPas.Text = Convert.ToString(konf.TentativaBllokUserXSession);
                cbBllokoLogimin.Checked = konf.BllokoLogin;
                cbGjeneroPassword.Checked = konf.GjeneroPassword;
                if (konf.MaxSesioneXPerdorues != 0)
                    seMaxSession.Text = Convert.ToString(konf.MaxSesioneXPerdorues);
                cbResetPass.Checked = konf.ResetPassword;
                cb2fact.Checked = konf.Twofacorauth;
                cbPassSkadon.Checked = konf.SkadoPassword;
                if (!konf.RuajHistorikunPass) spinDiteHistoriku.ClientEnabled = false;
                if (!konf.SkadoPassword) sePassSkadonPas.ClientEnabled = false;
                if (!konf.BllokoPerdorues)
                {
                    seBllokoPas.ClientEnabled = false;
                    seMaxSession.ClientEnabled = false;
                }
                seNrKaraktereveNumra.Text = Convert.ToString(konf.NumbersChars);
                seNrKaraktereSpeciale.Text = Convert.ToString(konf.SpecialChars);
                seNrShkronjaveKapitale.Text = Convert.ToString(konf.UppercaseChars);
            }
        }

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        /// <param name="idViti"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNdermarrje"></param>
        private void percaktoTemplateMenu(ASPxMenu aSPxMenu1, int idViti, int idPerdorues, int idNdermarrje)
        {
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, "PolitikaFjalekalimi.aspx", this, MenuInfo, true, true, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
        }

        private void vendosPerkthimet()
        {
            

            formLayout.Items[0].Caption = rm.GetString("captionHistorikuIFjalekalimit", ci);
            LayoutGroup group = (LayoutGroup) formLayout.Items[0];
            group.Items[1].Caption = rm.GetString("ruajHistorikPer", ci);
            cbRuajHistorikun.Text = rm.GetString("cbRuajHistorikFjalekalimi", ci);
            lblHeretEFundit1.Text = rm.GetString("labelHeretEFundit", ci);

            formLayout.Items[1].Caption = rm.GetString("captionPolitikatEFjalekalimit", ci);
            cbPassKompleks.Text = rm.GetString("cbFjalekalimKompleks", ci);
            group = (LayoutGroup)formLayout.Items[1];
            LayoutItem gItem = (LayoutItem) group.Items[0];
            gItem.HelpText = rm.GetString("textFjalekalimKompleks", ci);
            cbResetPass.Text = rm.GetString("cbLejoResetFjalekalimin", ci);
            cb2fact.Text = rm.GetString("cb2factorauth", ci);
            gItem = (LayoutItem)((LayoutGroup)formLayout.Items[2]).Items[0];
            gItem.HelpText = rm.GetString("textNDryshoHerenPasardhese", ci);
            cbPassDetyrueshem.Text = rm.GetString("cbFjalekalimIdetyrueshem", ci);

            cbPassSkadon.Text = rm.GetString("cbSkadoFjalekalimin", ci);
            gItem = (LayoutItem)((LayoutGroup)formLayout.Items[3]).Items[1];
            gItem.Caption = rm.GetString("cbSkadofjalekalimpas", ci);
            lblDiteSkadence.Text = rm.GetString("labelDitesh", ci) + ".";

            gItem = (LayoutItem)((LayoutGroup)formLayout.Items[4]).Items[1];
            gItem.Caption = rm.GetString("captionMinimalKaraktere", ci);
            lblKaraktere.Text = rm.GetString("labelKarakter", ci) + ".";

            gItem = (LayoutItem)((LayoutGroup)formLayout.Items[5]).Items[1];
            gItem.Caption = rm.GetString("numerKaraktereshSpeciale", ci);

            gItem = (LayoutItem)((LayoutGroup)formLayout.Items[6]).Items[1];
            gItem.Caption = rm.GetString("numerShkronjashKapitale", ci);

            gItem = (LayoutItem)((LayoutGroup)formLayout.Items[7]).Items[1];
            gItem.Caption = rm.GetString("numerKaraktereshNumra", ci);

            group = (LayoutGroup) formLayout.Items[8];
            group.Caption = rm.GetString("bllokimiIPerdoruesit", ci);
            cbBllokoUser.Text = rm.GetString("cbBllokoPerdorues", ci);
            gItem = (LayoutItem)group.Items[1];
            gItem.Caption = rm.GetString("captionBllokoPas", ci);
            lblTentativa.Text = rm.GetString("cbTentativaPerSesion", ci);
            cbGjeneroPassword.Text = rm.GetString("cbGjeneroAutomatik", ci);
            lblNrSession.Text = rm.GetString("labelSesione", ci);
            cbBllokoLogimin.Text = rm.GetString("cbBllokoLogin", ci);
        }
    }
}