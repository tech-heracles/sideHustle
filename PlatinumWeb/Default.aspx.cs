using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DbCore.DbShare;
using System.Data.SqlClient;
using System.Data;
using DevExpress.Web;
using System.Configuration;
using System.IO;
using System.Globalization;
using System.Resources;
using System.Reflection;
using DbCore.DbAdmin;
using DbCore;
using DbCore.IMBUtils.Extensions;
using DbCore.IMBUtils.Logging;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class _Default : MyPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
                EmrateLabelave(ci);

                if (Request.QueryString["kodi"] != null)
                    mySessionObjects.ruajKodNdermarrje(Request.QueryString["kodi"], Session);
                int idNdermarrje = 0;
                if (Request.QueryString["id"] != null)
                {
                    idNdermarrje = Convert.ToInt32(Request.QueryString["id"]);
                    DbCore.mySessionObjects.ruajIdNdermarrjeNeSesion(Session, idNdermarrje.ToString());
                }

                if (Request.QueryString["viti"] != null)

                    DbCore.mySessionObjects.ruajVitiNdermarrjes(Session, Request.QueryString["viti"]);
                int idNderViti = 0;
                if (Request.QueryString["idnderviti"] != null)
                {
                    idNderViti = Convert.ToInt32(Request.QueryString["idnderviti"]);
                    DbCore.mySessionObjects.ruajIdNdermarrjeVit(idNderViti.ToString(), Session);
                }

                //do merret perdoruesi sebashku me te drejtat dhe objekti i krijuar
                //do ruhet ne sesion. Ne menyre qe me vone te kontrollohen te drejtat
                //per kete perdorues.
                try
                {
                    if (idNdermarrje == 0)
                        idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                }
                catch (Exception err)
                {
                    ImbLogger.Error(err);
                    Response.Redirect("Login_Ndermarrje.aspx");
                    return;
                }
                var nderm = new DbCore.DbAdmin.clsNdermarrje(idNdermarrje);
                DbCore.mySessionObjects.ruajRuajLogNeSesion(Session, nderm.LogNdermarrje);
                String kodiNdermarrjes = DbCore.mySessionObjects.ktheKodNdermarrje(Session);
                String vitiNdermarrjes = DbCore.mySessionObjects.ktheVitiNdermarrjes(Session).ToString();
                DbCore.mySessionObjects.ruajEshteNdermarjeMemeNeSesion(Session, nderm.Prind);
                DbCore.mySessionObjects.ruajEshteNdermarjeOwnNeSesion(Session, nderm.OwnShop);
                DbCore.mySessionObjects.ruajNdermRaportuese(Session, nderm.Raportuesi);
                int idPerdoruesi = mySessionObjects.ktheIdPerdoruesi(Session);
                DbCore.DbAdmin.clsPerdorues perdorues = new DbCore.DbAdmin.clsPerdorues(idPerdoruesi);
                if (perdorues.IdPerdorues > 0)
                {
                    DbCore.mySessionObjects.ruajPerdoruesNeSesion(Session, perdorues);
                }
                else
                {
                    DbCore.mySessionObjects.ruajPerdoruesNeSesion(Session, new clsPerdorues());
                }
                if (Request.QueryString["kontrollodefault"] == "true")
                {
                    //string komponente = clsKomponente.merrKomponenteDefaultPerdoruesi(idPerdoruesi);
                    string komponente = clsFunksione.ktheKomponenteDefaultPerPerdorues(idPerdoruesi, idNdermarrje, String.Empty, vitiNdermarrjes);
                    if (komponente != "")
                    {
                        Response.Redirect(komponente);
                        return;
                    }
                }

                DbCore.DbAdmin.clsViti vit = new DbCore.DbAdmin.clsViti();
                vit.mbushVitetMet(vitiNdermarrjes, idNdermarrje);
                mbushTeDhenaPerNdermarjen(nderm, vit);
                mbushHiperLinkePerHelp();
                shfaqLinqetSipasTeDrejtave(idPerdoruesi, idNdermarrje, vit.IdViti);

                var PershkrimiNdermarrjes = nderm.NdermarrjePershkrimi;
                var KodiVitit = vit.KodiViti;
                var queryString = "Welcome_HR.html?" + "NdermarrjePershkrimi=" + PershkrimiNdermarrjes + '&' + "KodiViti=" + KodiVitit;
                if (KlientSpecifik.Vodafone.ToString().EqualsIgnoreCase(clsServerConfiguration.LexoKonfigurimSipasKey<string>(ServerKonfigKey.Klienti)))
                {
                    Response.Redirect(queryString, true);
                    return;
                }
            }
        }
        /// <summary>
        /// kjo metode sherben per te mbushur linqet ne client-side 
        /// </summary>
        private void mbushHiperLinkePerHelp()
        {
            DbCore.DbAdmin.clsKomponente komp = new DbCore.DbAdmin.clsKomponente("Shto_Ndermarrje.aspx");
            hlAdministrimi.ClientSideEvents.Click = "function(s,e){openHelpWindow(s,e,\'" + DbCore.clsFunksione.ktheUrlHelpi(komp.UrlHelpSuffix).Item1 + "\');}";
            DbCore.DbAdmin.clsKomponente kompKonf = new DbCore.DbAdmin.clsKomponente("Shto_KPF.aspx");
            this.hlKonfigurimet.ClientSideEvents.Click = "function(s,e){openHelpWindow(s,e,\'" + DbCore.clsFunksione.ktheUrlHelpi(kompKonf.UrlHelpSuffix).Item1 + "\');}";
            DbCore.DbAdmin.clsKomponente kompInv = new DbCore.DbAdmin.clsKomponente("Shto_NjesiAdministrative.aspx");
            this.hlInventare.ClientSideEvents.Click = "function(s,e){openHelpWindow(s,e,\'" + DbCore.clsFunksione.ktheUrlHelpi(kompInv.UrlHelpSuffix).Item1 + "\');}";
            DbCore.DbAdmin.clsKomponente kompSHB = new DbCore.DbAdmin.clsKomponente("Shto_KlientFurnitor.aspx?kf=klient");
            this.hlShitjeBlerje.ClientSideEvents.Click = "function(s,e){openHelpWindow(s,e,\'" + DbCore.clsFunksione.ktheUrlHelpi(kompSHB.UrlHelpSuffix).Item1 + "\');}";
            DbCore.DbAdmin.clsKomponente kompAB = new DbCore.DbAdmin.clsKomponente("Shto_Banka.aspx?ab=banka");
            this.hlArkaBanka.ClientSideEvents.Click = "function(s,e){openHelpWindow(s,e,\'" + DbCore.clsFunksione.ktheUrlHelpi(kompAB.UrlHelpSuffix).Item1 + "\');}";
            DbCore.DbAdmin.clsKomponente kompKont = new DbCore.DbAdmin.clsKomponente("FleteKontabel.aspx");
            this.hlKontabilitet.ClientSideEvents.Click = "function(s,e){openHelpWindow(s,e,\'" + DbCore.clsFunksione.ktheUrlHelpi(kompKont.UrlHelpSuffix).Item1 + "\');}";
            DbCore.DbAdmin.clsKomponente kompRap = new DbCore.DbAdmin.clsKomponente("RaportetAllNew.aspx");
            this.hlRaporte.ClientSideEvents.Click = "function(s,e){openHelpWindow(s,e,\'" + DbCore.clsFunksione.ktheUrlHelpi(kompRap.UrlHelpSuffix).Item1 + "\');}";
        }

        private void mbushTeDhenaPerNdermarjen(DbCore.DbAdmin.clsNdermarrje nderm, DbCore.DbAdmin.clsViti viti)
        {
            lblEmerNderm.Text = nderm.NdermarrjePershkrimi;
            lblEmerNIpti.Text = nderm.NdermarrjeNipt;
            lblEmerViti.Text = viti.KodiViti;
            lblEmerQyteti.Text = String.Format("{0}   /     {1}", nderm.NdermarrjeQytetiPershkrimi, nderm.NdermarrjeVendi);
            lblNrTel.Text = nderm.NdermarrjeTel;
            lblEmerWebPage.Text = nderm.NdermarrjeEMail;

        }

        private void shfaqLinqetSipasTeDrejtave(int idPerdoruesi, int idNdermarrje, int idViti)
        {
            DbCore.DbAdmin.clsKomponente komp = new DbCore.DbAdmin.clsKomponente();
            DataTable dt = DbCore.DbAdmin.colTeDrejtaRoli.merrTeDrejtaRoliMeEmraKomponentesh(idPerdoruesi, idNdermarrje, idViti);
            #region Administrimi
            int count = 0;
            DataRow[] dr = dt.Select("KOMPONEMRI= '" + this.hlCeljaNdermarje.NavigateUrl.Remove(0, 42).Split('\'')[0] + "'");
            if (dr.Length > 0 && dr[0]["D_AMB"].ToString() == "1")
            {
                hlCeljaNdermarje.Visible = true; count++;
            }
            else
                hlCeljaNdermarje.Visible = false;
            dr = dt.Select("KOMPONEMRI= '" + this.hlCeljaRole.NavigateUrl.Remove(0, 42).Split('\'')[0] + "'");
            if (dr.Length > 0 && dr[0]["D_AMB"].ToString() == "1")
            {
                hlCeljaRole.Visible = true; count++;
            }
            else
                hlCeljaRole.Visible = false;
            dr = dt.Select("KOMPONEMRI= '" + this.hlCeljaPerdoruesve.NavigateUrl.Remove(0, 42).Split('\'')[0] + "'");
            if (dr.Length > 0 && dr[0]["D_AMB"].ToString() == "1")
            {
                hlCeljaPerdoruesve.Visible = true; count++;
            }
            else
                hlCeljaPerdoruesve.Visible = false;
            if (count == 0)
            { hlAdministrimi.Visible = false; lblAdministrimi.Visible = false; imgAdministrimi.Visible = false; }
            else { hlAdministrimi.Visible = true; lblAdministrimi.Visible = true; imgAdministrimi.Visible = true; }
            #endregion
            #region Konfigurimet
            count = 0;
            dr = dt.Select("KOMPONEMRI= '" + this.hlKPF.NavigateUrl.Remove(0, 42).Split('\'')[0] + "'");
            if (dr.Length > 0 && dr[0]["D_AMB"].ToString() == "1")
            {
                hlKPF.Visible = true; count++;
            }
            else
                hlKPF.Visible = false;
            dr = dt.Select("KOMPONEMRI= '" + this.hfLlogari.NavigateUrl.Remove(0, 42).Split('\'')[0] + "'");
            if (dr.Length > 0 && dr[0]["D_AMB"].ToString() == "1")
            { hfLlogari.Visible = true; count++; }
            else
                hfLlogari.Visible = false;
            dr = dt.Select("KOMPONEMRI= '" + this.hlTaksa.NavigateUrl.Remove(0, 42).Split('\'')[0] + "'");
            if (dr.Length > 0 && dr[0]["D_AMB"].ToString() == "1")
            { hlTaksa.Visible = true; count++; }
            else
                hlTaksa.Visible = false;
            dr = dt.Select("KOMPONEMRI= '" + this.hlVitet.NavigateUrl.Remove(0, 42).Split('\'')[0] + "'");
            if (dr.Length > 0 && dr[0]["D_AMB"].ToString() == "1")
            { hlVitet.Visible = true; count++; }
            else
                hlVitet.Visible = false;
            if (count == 0)
            { this.hlKonfigurimet.Visible = false; this.lblKonfigurimi.Visible = false; imgKonfigurimet.Visible = false; }
            else { hlKonfigurimet.Visible = true; lblKonfigurimi.Visible = true; imgKonfigurimet.Visible = true; }
            #endregion
            #region Inventar
            count = 0;
            dr = dt.Select("KOMPONEMRI= '" + this.hlMagazina.NavigateUrl.Remove(0, 42).Split('\'')[0] + "'");
            if (dr.Length > 0 && dr[0]["D_AMB"].ToString() == "1")
            { hlMagazina.Visible = true; count++; }
            else
                hlMagazina.Visible = false;
            dr = dt.Select("KOMPONEMRI= '" + this.hlArtikuj.NavigateUrl.Remove(0, 42).Split('\'')[0] + "'");
            if (dr.Length > 0 && dr[0]["D_AMB"].ToString() == "1")
            { hlArtikuj.Visible = true; count++; }
            else
                hlArtikuj.Visible = false;
            dr = dt.Select("KOMPONEMRI= '" + this.hlAQT.NavigateUrl.Remove(0, 42).Split('\'')[0] + "'");
            if (dr.Length > 0 && dr[0]["D_AMB"].ToString() == "1")
            { hlAQT.Visible = true; count++; }
            else
                hlAQT.Visible = false;
            dr = dt.Select("KOMPONEMRI= '" + this.hlRegjMag.NavigateUrl.Remove(0, 42).Split('\'')[0] + "'");
            if (dr.Length > 0 && dr[0]["D_AMB"].ToString() == "1")
            { hlRegjMag.Visible = true; count++; }
            else
                hlRegjMag.Visible = false;
            dr = dt.Select("KOMPONEMRI= '" + this.hlRivleresim.NavigateUrl.Remove(0, 42).Split('\'')[0] + "'");
            if (dr.Length > 0 && dr[0]["D_AMB"].ToString() == "1")
            { hlRivleresim.Visible = true; count++; }
            else
                hlRivleresim.Visible = false;
            if (count == 0)
            { this.hlInventare.Visible = false; this.lblInventare.Visible = false; imgInventare.Visible = false; }
            else { hlInventare.Visible = true; lblInventare.Visible = true; imgInventare.Visible = true; }
            #endregion
            #region Blerje dhe shitje
            count = 0;
            dr = dt.Select("KOMPONEMRI= '" + this.hlKlient.NavigateUrl.Remove(0, 42).Split('\'')[0] + "'");
            if (dr.Length > 0 && dr[0]["D_AMB"].ToString() == "1")
            { hlKlient.Visible = true; count++; }
            else
                hlKlient.Visible = false;
            dr = dt.Select("KOMPONEMRI= '" + this.hlFurnitor.NavigateUrl.Remove(0, 42).Split('\'')[0] + "'");
            if (dr.Length > 0 && dr[0]["D_AMB"].ToString() == "1")
            { hlFurnitor.Visible = true; count++; }
            else
                hlFurnitor.Visible = false;
            dr = dt.Select("KOMPONEMRI= '" + this.hlShitje.NavigateUrl.Remove(0, 42).Split('\'')[0] + "'");
            if (dr.Length > 0 && dr[0]["D_AMB"].ToString() == "1")
            { hlShitje.Visible = true; count++; }
            else
                hlShitje.Visible = false;
            dr = dt.Select("KOMPONEMRI= '" + this.hlBlerje.NavigateUrl.Remove(0, 42).Split('\'')[0] + "'");
            if (dr.Length > 0 && dr[0]["D_AMB"].ToString() == "1")
            { hlBlerje.Visible = true; count++; }
            else
                hlBlerje.Visible = false;
            if (count == 0)
            { this.hlShitjeBlerje.Visible = false; this.lblBlerjeShitje.Visible = false; imgShitjeBlerje.Visible = false; }
            else { hlShitjeBlerje.Visible = true; lblBlerjeShitje.Visible = true; imgShitjeBlerje.Visible = true; }
            #endregion
            #region Arka dhe banka
            count = 0;
            dr = dt.Select("KOMPONEMRI= '" + this.hlArka.NavigateUrl.Remove(0, 42).Split('\'')[0] + "'");
            if (dr.Length > 0 && dr[0]["D_AMB"].ToString() == "1")
            { hlArka.Visible = true; count++; }
            else
                hlArka.Visible = false;
            dr = dt.Select("KOMPONEMRI= '" + this.hlBanka.NavigateUrl.Remove(0, 42).Split('\'')[0] + "'");
            if (dr.Length > 0 && dr[0]["D_AMB"].ToString() == "1")
            { hlBanka.Visible = true; count++; }
            else
                hlBanka.Visible = false;
            dr = dt.Select("KOMPONEMRI= '" + this.hlRegjArka.NavigateUrl.Remove(0, 42).Split('\'')[0] + "'");
            if (dr.Length > 0 && dr[0]["D_AMB"].ToString() == "1")
            { hlRegjArka.Visible = true; count++; }
            else
                hlRegjArka.Visible = false;
            dr = dt.Select("KOMPONEMRI= '" + this.hlRegjBanka.NavigateUrl.Remove(0, 42).Split('\'')[0] + "'");
            if (dr.Length > 0 && dr[0]["D_AMB"].ToString() == "1")
            { hlRegjBanka.Visible = true; count++; }
            else
                hlRegjBanka.Visible = false;
            dr = dt.Select("KOMPONEMRI= '" + this.hlLidhja.NavigateUrl.Remove(0, 42).Split('\'')[0] + "'");
            if (dr.Length > 0 && dr[0]["D_AMB"].ToString() == "1")
            { hlLidhja.Visible = true; count++; }
            else
                hlLidhja.Visible = false;
            if (count == 0)
            { this.hlArkaBanka.Visible = false; this.lblArkaBanka.Visible = false; imgArkaBanka.Visible = false; }
            else { hlArkaBanka.Visible = true; lblArkaBanka.Visible = true; imgArkaBanka.Visible = true; }
            #endregion
            #region Kontabilitet
            count = 0;
            dr = dt.Select("KOMPONEMRI= '" + this.hlAmbjentKont.NavigateUrl.Remove(0, 42).Split('\'')[0] + "'");
            if (dr.Length > 0 && dr[0]["D_AMB"].ToString() == "1")
            { hlAmbjentKont.Visible = true; count++; }
            else
                hlAmbjentKont.Visible = false;

            if (count == 0)
            { this.hlKontabilitet.Visible = false; this.lblKontabilitet.Visible = false; imgKontabilitet.Visible = false; }
            else { hlKontabilitet.Visible = true; lblKontabilitet.Visible = true; imgKontabilitet.Visible = true; }
            #endregion
            #region Raportet
            count = 0;
            dr = dt.Select("KOMPONEMRI= '" + this.hlRaportimi.NavigateUrl.Remove(0, 42).Split('\'')[0] + "'");
            if (dr.Length > 0 && dr[0]["D_AMB"].ToString() == "1")
            { hlRaportimi.Visible = true; count++; }
            else
                hlRaportimi.Visible = false;

            if (count == 0)
            { this.hlRaporte.Visible = false; this.lblRaportet.Visible = false; imgRaporte.Visible = false; }
            else { hlRaporte.Visible = true; lblRaportet.Visible = true; imgRaporte.Visible = true; }
            #endregion
        }

        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                      System.Reflection.Assembly.Load("App_GlobalResources"));

            lblNdermarje.Text = rm.GetString("labelMirseviniNeNdermarrjen", ci) + ":";
            lblNIPT.Text = rm.GetString("labelNIPT", ci) + ":";
            lblViti.Text = rm.GetString("labelViti", ci) + ":";
            lblQyteti.Text = rm.GetString("labelQytetiVendi", ci) + ":";
            lblTel.Text = rm.GetString("labelRaportTel", ci) + ":";
            lblWebPage.Text = rm.GetString("labelWebPage", ci) + ":";
            hlAdministrimi.Text = rm.GetString("labelFillimiPunes", ci);
            hlKonfigurimet.Text = rm.GetString("MenuItemKonfigurime", ci);
            hlInventare.Text = rm.GetString("MenuItemRaportInventari", ci);
            lblAdministrimi.Text = rm.GetString("labelAdministrimi", ci);
            lblKonfigurimi.Text = rm.GetString("labelKonfigurimi", ci);
            lblInventare.Text = rm.GetString("labelInvetare", ci);
            hlCeljaNdermarje.Text = rm.GetString("MenuItemNdermarrjet", ci);
            hlKPF.Text = rm.GetString("MenuItemStrukturatLlogarive", ci);
            hlMagazina.Text = rm.GetString("MenuItemMagazinat", ci);
            hlCeljaRole.Text = rm.GetString("MenuItemRolet", ci);
            hfLlogari.Text = rm.GetString("MenuItemLlogarite", ci);
            hlArtikuj.Text = rm.GetString("MenuItemArtikujt", ci);
            hlCeljaPerdoruesve.Text = rm.GetString("MenuItemPerdoruesit", ci);
            hlTaksa.Text = rm.GetString("MenuItemTaksat", ci);
            hlAQT.Text = rm.GetString("MenuItemArtikujtAfatgjate", ci);
            hlVitet.Text = rm.GetString("MenuItemVitet", ci);
            hlRivleresim.Text = rm.GetString("MenuItemRivleresimInventarit", ci);
            hlRegjMag.Text = rm.GetString("labelDokEHyrjeve", ci);
            hlShitjeBlerje.Text = rm.GetString("NavBarItemBlerjetDheShitjet", ci);
            hlArkaBanka.Text = rm.GetString("MenuItemRaportArkadheBanka", ci);
            hlKontabilitet.Text = rm.GetString("MenuItemRaportKontabiliteti", ci);
            ASPxLabel1.Text = rm.GetString("labelKonfigFurnitorRegjShitjeBlerje", ci);
            ASPxLabel2.Text = rm.GetString("labelKonfigNjesieArkaBanka", ci);
            ASPxLabel3.Text = rm.GetString("labelKontabilizimiRegjistrimeve", ci);
            hlKlient.Text = rm.GetString("MenuItemKlientet", ci);
            hlArka.Text = rm.GetString("MenuItemCeljaArkave", ci);
            hlAmbjentKont.Text = rm.GetString("NavBarItemAmbjentiKontabilizimit", ci);
            hlFurnitor.Text = rm.GetString("MenuItemFurnitoret", ci);
            hlBanka.Text = rm.GetString("MenuItemCeljaBankave", ci);
            hlShitje.Text = rm.GetString("MenuItemFaturatShitjeve", ci);
            hlRegjArka.Text = rm.GetString("MenuItemArketimet", ci);
            hlRaporte.Text = rm.GetString("MenuItemRaportet", ci);
            hlBlerje.Text = rm.GetString("MenuItemFaturatBlerjeve", ci);
            hlRegjBanka.Text = rm.GetString("MenuItemPagesat", ci);
            hlLidhja.Text = rm.GetString("MenuItemLidhjaDokumentave", ci);
            ASPxLabel4.Text = rm.GetString("labelInfoTeDetajuaraPerFunksione", ci);
            hlRaportimi.Text = rm.GetString("labelRaportimi", ci);

        }
    }
}
