using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Collections;
using System.Data;
using DbCore.DbRegjistrim;
using System.Globalization;
using System.Resources;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.DbArkaBanka;
using DbCore.DbKontabiliteti;
using DbCore;
using Newtonsoft.Json;
using DbCore.DbShare;

namespace PlatinumWeb
{
    public partial class LupaDokumenta : MyPageBase
    {
        //private DbCore.DbRegjistrim.clsDatabaseRegjistrim dbRegjistrim;
        private String veprimi;
        private int niveli;
        protected void Page_Load(object sender, EventArgs e)
        {
            string vleraQueryString = "";
            if (Request.QueryString["idKonfigAmbjente"] != null && Request.QueryString["idKonfigAmbjente"] != "")
                vleraQueryString = Request.QueryString["idKonfigAmbjente"].ToString();
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idKonfigambjenti = DbCore.clsFunksione.getIdKonfigAmbLupa(vleraQueryString, idNdermarrje, "DOK");
            bool kerkosaposhkruar = true;
            if (DbCore.DbShare.clsAlternativaKushti.getAlternativa(idKonfigambjenti, "KSSH") == "Po")
                kerkosaposhkruar = true;
            else kerkosaposhkruar = false;
            bool endlessScroll = false;
            if (clsAlternativaKushti.getAlternativa(idKonfigambjenti, "ES") == "Po")
                endlessScroll = true;
            if (Request.QueryString["veprimi"] != null)
                veprimi = Request.QueryString["veprimi"];
            if (Request.QueryString["niveli"] != null)
                niveli = int.Parse(Request.QueryString["niveli"]);
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            int idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            if (!IsPostBack)
            {
                EmrateLabelave(ci, rm);
                //Session.Add("Periudha", "1");
                cmbKonfigurimi.Value = idKonfigambjenti.ToString();
                DbCore.mySessionObjects.ruajPeriudheNeSesion(Session, 1);
                dteNga.Date = DateTime.Today;
                AspxWebControlUtils.vendosDateEditMask(dteNga);
                dteDeri.Date = DateTime.Today;
                AspxWebControlUtils.vendosDateEditMask(dteDeri);
                if (veprimi != "RegjistrimDokumentashPaprintuar" && veprimi != "VeprimeBankaPaprintuar")

                    rbAktuale.Checked = true;
                else
                {
                    this.rbNgaDeri.Checked = true;
                    this.dteNga.Date = new DateTime(2000, 01, 01);
                    this.dteDeri.Date = new DateTime(9999, 12, 31);
                }
                //  mbushPopUpListe();  
                mbushPopUpListeDokumentashNgaDB();
                konfiguroPopupGride(idKonfigambjenti, true, idGjuha, kerkosaposhkruar, rm, ci, endlessScroll);

                //DbCore.clsFunksione.konfiguroMenuRuajPerLupa(ASPxMenu1);
                GridUtil.AplikoFilterDefault(gvLupaDok, idKonfigambjenti);
                mbushFilterDefaultNgaQueryString();

                clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idNdermarrje, "gvLupaDok", 1, "LupaDokumenta.aspx");
            }
            else
            {
                mbushPopUpListeDokumentashNgaSession();
                konfiguroPopupGride(idKonfigambjenti, false, idGjuha, kerkosaposhkruar, rm, ci, endlessScroll);
            }

            if (Request.QueryString["veprimi"] == "ShperndarjeShpenzimeshKerko" || Request.QueryString["veprimi"] == "FleteDoganore")
            {
                gvLupaDok.Columns["#"].VisibleIndex = 0;
            }
        }
        private void mbushFilterDefaultNgaQueryString()
        {

            if (!gvLupaDok.FilterExpression.Contains("[Status]"))
            {
                if (gvLupaDok.FilterExpression != "")
                    gvLupaDok.FilterExpression += "and [Status]=1";
                else gvLupaDok.FilterExpression += "[Status]=1";
            }

            
            if (Request.QueryString["niveli"] != null && !gvLupaDok.FilterExpression.Contains("[IdNiveli]"))
                gvLupaDok.FilterExpression += " and [IdNiveli] = " + niveli;
            if (Request.QueryString["njesia"] != null && Request.QueryString["njesia"].ToString() != "" && !gvLupaDok.FilterExpression.Contains("[Njesia]"))
                gvLupaDok.FilterExpression += " and [Njesia] = " + String.Format("'{0}'", Request.QueryString["njesia"]);
            if (Request.QueryString["degeAdmin"] != null && Request.QueryString["degeAdmin"].ToString() != "" && !gvLupaDok.FilterExpression.Contains("[KodDegeAdministrative]"))
                gvLupaDok.FilterExpression += " and [KodDegeAdministrative] = " + String.Format("'{0}'", Request.QueryString["degeAdmin"]);
        }

        private void mbushPopUpListeDokumentashNgaSession()
        {
            Object tmpObject;
            DbCore.mySessionObjects.merrGrideNgaSessioniLupa(Session, out tmpObject);
            if (tmpObject == null)
            {
                mbushPopUpListeDokumentashNgaDB();
                return;
            }
            gvLupaDok.DataSource = tmpObject;
            gvLupaDok.DataBind();
        }
        private void mbushPopUpListeDokumentashNgaDB()
        {//mbush griden e popupit me te dhena            
            if (rbAktuale.Checked)
                //CacheLayer.GlobalCacheManager.MySessionCache["Periudha"] = 1;
                DbCore.mySessionObjects.ruajPeriudheNeSesion(Session, 1);
            else if (rbVitiUshtrimor.Checked)
                //CacheLayer.GlobalCacheManager.MySessionCache["Periudha"] = 2;
                DbCore.mySessionObjects.ruajPeriudheNeSesion(Session, 2);
            else
                //CacheLayer.GlobalCacheManager.MySessionCache["Periudha"] = 3;
                DbCore.mySessionObjects.ruajPeriudheNeSesion(Session, 3);

            //merrSipasPeriudhes(CacheLayer.GlobalCacheManager.MySessionCache["Periudha"].ToString());
            merrSipasPeriudhes(DbCore.mySessionObjects.merrPeriudheNgaSesioni(Session).ToString());
        }

        private void mbushPopUpListe()
        {
            // merrSipasPeriudhes(CacheLayer.GlobalCacheManager.MySessionCache["Periudha"].ToString());
            if (rbAktuale.Checked)
                //CacheLayer.GlobalCacheManager.MySessionCache["Periudha"] = 1;
                DbCore.mySessionObjects.ruajPeriudheNeSesion(Session, 1);
            else if (rbVitiUshtrimor.Checked)
                //CacheLayer.GlobalCacheManager.MySessionCache["Periudha"] = 2;
                DbCore.mySessionObjects.ruajPeriudheNeSesion(Session, 2);
            else
                //CacheLayer.GlobalCacheManager.MySessionCache["Periudha"] = 3;
                DbCore.mySessionObjects.ruajPeriudheNeSesion(Session, 3);
            //merrSipasPeriudhes(CacheLayer.GlobalCacheManager.MySessionCache["Periudha"].ToString());
            merrSipasPeriudhes(DbCore.mySessionObjects.merrPeriudheNgaSesioni(Session).ToString());
        }

        private void konfiguroPopupGride(int idKonfigambjenti, bool visibleIndex, int idGjuha, bool kerkosaposhkruar, ResourceManager rm, CultureInfo ci, bool endlessScroll)
        {
            shtoNivel();
            shtoMonedha();
            shtoStatus();
            shtoKodKonfigAmbjente(idKonfigambjenti, idGjuha);
            if (Request.QueryString["njesia"] != null && !String.IsNullOrEmpty(Request.QueryString["njesia"].ToString()))
                shtoNjesi(idGjuha, rm, ci);
            GridUtil.percaktoVisibleColumnsSipasKonfigurimit(gvLupaDok, "gvLupaDok", "LupaDokumenta.aspx", idKonfigambjenti, visibleIndex, idGjuha);

            if (veprimi == "UrdherPagesa") gvLupaDok.Columns["EmerPerfituesi"].Visible = true;

            GridUtil.KonfiguroGrideListeMadhePopupiPaTheme(gvLupaDok, "IdDokumenti", kerkosaposhkruar, endlessScroll);
            GridViewDataTextColumn col5 = gvLupaDok.Columns["Vlefta"] as GridViewDataTextColumn;
            col5.PropertiesEdit.DisplayFormatString = "N";
            gvLupaDok.SettingsLoadingPanel.Mode = GridViewLoadingPanelMode.Disabled;
        }

        private void shtoNjesi(int idGjuha, ResourceManager rm, CultureInfo ci)
        {
            GridViewDataTextColumn njesia = new GridViewDataTextColumn();
            gvLupaDok.Columns.Remove(gvLupaDok.Columns["Njesia"]);
            njesia.FieldName = "Njesia";
            njesia.VisibleIndex = 2;
            switch (veprimi)
            {
                case "RegjistrimDokumentash":
                    string shitjeBlerje = Request.QueryString["shitje_blerje"].ToString();
                    njesia.Caption = shitjeBlerje == "shitje" ? rm.GetString("pikeShitjeTab", ci) : rm.GetString("msgPikeFurnizimi", ci);
                    break;
                case "RegjistrimMagazine":
                    njesia.Caption = rm.GetString("filterMagazina", ci);
                    break;
                case "VeprimeBanka":
                    string arkabanka = Request.QueryString["lloji"].ToString();
                    njesia.Caption = (arkabanka == "pagese" || arkabanka == "arketim") ? rm.GetString("cmbboxItemFilterAvancArka", ci) : rm.GetString("cmbboxItemFilterAvancBanka", ci);
                    break;
                default:
                    break;
            }
            gvLupaDok.Columns.Add(njesia);
        }

        /// <summary>
        /// perdoret per te shfaqur kolonen e nivelit si kombo me kodet perkatese
        /// </summary>
        private void shtoNivel()
        {
            int visibleindex = gvLupaDok.Columns["IdNiveli"].VisibleIndex;
            gvLupaDok.Columns.Remove(gvLupaDok.Columns["IdNiveli"]);
            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            DbCore.DbAdmin.colLlojDokumenti col = new DbCore.DbAdmin.colLlojDokumenti();
            DbCore.DbRegjistrim.colNivelRegjistrimi colniv = new DbCore.DbRegjistrim.colNivelRegjistrimi();
            DbCore.DbRegjistrim.colTrupiShitje colFaturat = new DbCore.DbRegjistrim.colTrupiShitje();
            int idKategoria = clsNivelRegjistrimi.ktheIdKategoriaNivelRegjistrimi(niveli);
            int idndermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idperdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            switch (veprimi)
            {
                case "RegjistrimDokumentash":
                case "RegjistrimDokumentashPaprintuar":
                    colniv.mbushGjitheNivelRegjistrimiSipasKategoriPaKonvertime(idKategoria, idndermarje, idperdoruesi);
                    break;
                case "VeprimeBanka":
                case "VeprimeBankaPaprintuar":

                    colniv.mbushGjitheNivelRegjistrimiSipasKategoriPaKonvertime(idKategoria, idndermarje, idperdoruesi);
                    break;
                case "RegjistrimMagazine":
                    colniv.mbushGjitheNivelRegjistrimiSipasKategoriPaKonvertime(6, idndermarje, idperdoruesi);
                    break;
                case "RegjistrimNdryshimCmimSasi":
                    colniv.mbushGjitheNivelRegjistrimiSipasKategoriPaKonvertime(95, idndermarje, idperdoruesi);
                    break;
                case "RegjistrimInventarizimi":
                    colniv.mbushGjitheNivelRegjistrimiSipasKategoriPaKonvertime(135, idndermarje, idperdoruesi);
                    break;
                case "ShperndarjeShpenzimeshKerkoMenu":
                    colniv.mbushGjitheNivelRegjistrimiSipasKategoriPaKonvertime(7, idndermarje, idperdoruesi);
                    break;
                case "VeprimeKF":
                    colniv.mbushGjitheNivelRegjistrimiSipasKategoriPaKonvertime(20, idndermarje, idperdoruesi);
                    break;
                case "QendraKosto":
                    colniv.mbushGjitheNivelRegjistrimiSipasKategoriPaKonvertime(75, idndermarje, idperdoruesi);
                    break;
                case "Amortizimi":
                    colniv.mbushGjitheNivelRegjistrimiSipasKategoriPaKonvertime(86, idndermarje, idperdoruesi);
                    break;
                case "RivleresimeAmortizimi":
                    colniv.mbushGjitheNivelRegjistrimiSipasKategoriPaKonvertime(90, idndermarje, idperdoruesi);
                    break;
                case "AzhornimKF":
                    colniv.mbushGjitheNivelRegjistrimiSipasKategoriPaKonvertime(11, idndermarje, idperdoruesi);
                    break;
                case "MbylljeKF":
                    colniv.mbushGjitheNivelRegjistrimiSipasKategoriPaKonvertime(64, idndermarje, idperdoruesi);
                    break;
                case "LidhjaDokumentave":
                    colniv.mbushGjitheNivelRegjistrimiSipasKategoriPaKonvertime(10, idndermarje, idperdoruesi);
                    break;
                case "FleteDoganore":
                    colniv.mbushGjitheNivelRegjistrimiSipasKategoriPaKonvertime(8, idndermarje, idperdoruesi);
                    break;
                case "FleteKontabel":
                    colniv.mbushGjitheNivelRegjistrimiSipasKategoriPaKonvertime(5, idndermarje, idperdoruesi);
                    break;
                case "ShperndarjeShpenzimeshKerko":
                    colniv.mbushGjitheNivelRegjistrimiSipasKategoriPaKonvertime(2, idndermarje, idperdoruesi);
                    break;
                case "ListPagesa":
                    colniv.mbushGjitheNivelRegjistrimiSipasKategoriPaKonvertime(38, idndermarje, idperdoruesi);
                    break;
                case "Planifikim":
                    colniv.mbushGjitheNivelRegjistrimiSipasKategoriPaKonvertime(44, idndermarje, idperdoruesi);
                    break;
                case "EkzekutimProdhimi":
                    colniv.mbushGjitheNivelRegjistrimiSipasKategoriPaKonvertime(45, idndermarje, idperdoruesi);
                    break;
                case "UrdherPagesa":
                    colniv.mbushGjitheNivelRegjistrimiSipasKategoriPaKonvertime(39, idndermarje, idperdoruesi);
                    break;
                case "GjeneroProjekt":
                    colniv.mbushGjitheNivelRegjistrimiSipasKategoriPaKonvertime(1, idndermarje, idperdoruesi);
                    break;
                case "RegjistrimRezervimi":
                    colniv.mbushGjitheNivelRegjistrimiSipasKategoriPaKonvertime(78, idndermarje, idperdoruesi);
                    break;
                case "RegjistrimRiparimi":
                    colniv.mbushGjitheNivelRegjistrimiSipasKategoriPaKonvertime(80, idndermarje, idperdoruesi);
                    break;
                case "SkedulimProdhimi":
                    colniv.mbushGjitheNivelRegjistrimiSipasKategoriPaKonvertime(97, idndermarje, idperdoruesi);
                    break;
                case "RecetaOptike":
                    colniv.mbushGjitheNivelRegjistrimiSipasKategoriPaKonvertime(160, idndermarje, idperdoruesi);
                    break;
                default:
                    break;
            }
            colnew.PropertiesComboBox.DataSource = colniv;
            colnew.PropertiesComboBox.TextField = "Kodi";
            colnew.PropertiesComboBox.ValueField = "IdNivel";
            string kat = "";
            foreach (DbCore.DbRegjistrim.clsNivelRegjistrimi n in colniv)
            {
                kat += String.Format("{0}:{1},", n.IdNivel, n.IdKategori);
            }
            kategori.Value = kat;
            colnew.PropertiesComboBox.ClientInstanceName = "IdNiveli";
            colnew.FieldName = "IdNiveli";
            colnew.VisibleIndex = visibleindex;
            gvLupaDok.Columns.Add(colnew);
        }
        /// <summary>
        /// perdoret per te shfaqur kolonen e konfigurimit si kombo me kodet perkatese
        /// </summary>
        private void shtoKodKonfigAmbjente(int idKonfigambjenti, int idGjuha)
        {
            int visibleindex = gvLupaDok.Columns["IdKonfigAmbjente"].VisibleIndex;
            gvLupaDok.Columns.Remove(gvLupaDok.Columns["IdKonfigAmbjente"]);
            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            DbCore.DbAdmin.colLlojDokumenti col = new DbCore.DbAdmin.colLlojDokumenti();
            DbCore.DbRegjistrim.colTrupiShitje colFaturat = new DbCore.DbRegjistrim.colTrupiShitje();
            int idKategoria = DbCore.DbRegjistrim.clsNivelRegjistrimi.ktheIdKategoriaNivelRegjistrimi(niveli);
            DbCore.DbShare.colKonfigurimAmbjenti colkonf = new DbCore.DbShare.colKonfigurimAmbjenti();
            DbCore.DbShare.clsKonfigurimAmbjenti konfamb = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konfamb.mbushKonfigAmbjSipasId(idKonfigambjenti, idGjuha);
            int idndermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idperdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            switch (veprimi)
            {
                case "RegjistrimDokumentash":
                case "RegjistrimDokumentashPaprintuar":
                    colkonf.mbushKonfigAmbjSipasIdKategori(idKategoria, idndermarje, idperdoruesi, idGjuha);
                    break;
                case "VeprimeBanka":
                case "VeprimeBankaPaprintuar":
                    colkonf.mbushKonfigAmbjSipasIdKategori(idKategoria, idndermarje, idperdoruesi, idGjuha);
                    break;
                case "RegjistrimMagazine":
                    colkonf.mbushKonfigAmbjSipasIdKategori(6, idndermarje, idperdoruesi, idGjuha);
                    break;
                case "RegjistrimNdryshimCmimSasi":
                    colkonf.mbushKonfigAmbjSipasIdKategori(95, idndermarje, idperdoruesi, idGjuha);
                    break;
                case "RegjistrimInventarizimi":
                    colkonf.mbushKonfigAmbjSipasIdKategori(135, idndermarje, idperdoruesi, idGjuha);
                    break;
                case "GjeneroProjekt":
                    colkonf.mbushKonfigAmbjSipasIdKategori(1, idndermarje, idperdoruesi, idGjuha);
                    break;
                case "ShperndarjeShpenzimeshKerkoMenu":
                    colkonf.mbushKonfigAmbjSipasIdKategori(7, idndermarje, idperdoruesi, idGjuha);
                    break;
                case "VeprimeKF":
                    colkonf.mbushKonfigAmbjSipasIdKategori(20, idndermarje, idperdoruesi, idGjuha);
                    break;
                case "AzhornimKF":
                    colkonf.mbushKonfigAmbjSipasIdKategori(11, idndermarje, idperdoruesi, idGjuha);
                    break;
                case "MbylljeKF":
                    colkonf.mbushKonfigAmbjSipasIdKategori(64, idndermarje, idperdoruesi, idGjuha);
                    break;
                case "QendraKosto":
                    colkonf.mbushKonfigAmbjSipasIdKategori(75, idndermarje, idperdoruesi, idGjuha);
                    break;
                case "Amortizimi":
                    colkonf.mbushKonfigAmbjSipasIdKategori(86, idndermarje, idperdoruesi, idGjuha);
                    break;
                case "RivleresimeAmortizimi":
                    colkonf.mbushKonfigAmbjSipasIdKategori(90, idndermarje, idperdoruesi, idGjuha);
                    break;
                case "LidhjaDokumentave":
                    colkonf.mbushKonfigAmbjSipasIdKategori(10, idndermarje, idperdoruesi, idGjuha);
                    break;
                case "FleteDoganore":
                    colkonf.mbushKonfigAmbjSipasIdKategori(8, idndermarje, idperdoruesi, idGjuha);
                    break;
                case "FleteKontabel":
                    colkonf.mbushKonfigAmbjSipasIdKategori(5, idndermarje, idperdoruesi, idGjuha);
                    break;
                case "ShperndarjeShpenzimeshKerko":
                    colkonf.mbushKonfigAmbjSipasIdKategori(2, idndermarje, idperdoruesi, idGjuha);
                    break;
                case "ListPagesa":
                    colkonf.mbushKonfigAmbjSipasIdKategori(38, idndermarje, idperdoruesi, idGjuha);
                    break;
                case "Planifikim":
                    colkonf.mbushKonfigAmbjSipasIdKategori(44, idndermarje, idperdoruesi, idGjuha);
                    break;
                case "EkzekutimProdhimi":
                    colkonf.mbushKonfigAmbjSipasIdKategori(45, idndermarje, idperdoruesi, idGjuha);
                    break;
                case "UrdherPagesa":
                    colkonf.mbushKonfigAmbjSipasIdKategori(39, idndermarje, idperdoruesi, idGjuha);
                    break;
                case "RegjistrimRezervimi":
                    colkonf.mbushKonfigAmbjSipasIdKategori(78, idndermarje, idperdoruesi, idGjuha);
                    break;
                case "RegjistrimRiparimi":
                    colkonf.mbushKonfigAmbjSipasIdKategori(80, idndermarje, idperdoruesi, idGjuha);
                    break;
                case "SkedulimProdhimi":
                    colkonf.mbushKonfigAmbjSipasIdKategori(97, idndermarje, idperdoruesi, idGjuha);
                    break;
                case "RecetaOptike":
                    colkonf.mbushKonfigAmbjSipasIdKategori(160, idndermarje, idperdoruesi, idGjuha);
                    break;
                default:
                    break;
            }
            colnew.PropertiesComboBox.DataSource = colkonf;
            colnew.PropertiesComboBox.TextField = "KodKonfigAmbjente";
            colnew.PropertiesComboBox.ValueField = "IdKonfigAmbjente";
            colnew.PropertiesComboBox.ClientInstanceName = "IdKonfigAmbjente";
            colnew.FieldName = "IdKonfigAmbjente";
            colnew.VisibleIndex = visibleindex;
            gvLupaDok.Columns.Add(colnew);
        }


        private void shtoKlientFurnitor()
        {
            //DbCore.DbKontabiliteti.clsDatabaseKontabilitet dbKontabiliteti = new DbCore.DbKontabiliteti.clsDatabaseKontabilitet();
            int visibleindex = gvLupaDok.Columns["IdKlientFurnitori"].VisibleIndex;
            gvLupaDok.Columns.Remove(gvLupaDok.Columns["IdKlientFurnitori"]);
            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            DbCore.DbKontabiliteti.colKlienteFurnitore col = new DbCore.DbKontabiliteti.colKlienteFurnitore();
            col.mbushKlienteFurnitoreNdermarrjes(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            //col = dbKontabiliteti.merrKlienteFurnitoreNdermarrjes(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            DbCore.DbKontabiliteti.clsKlientFurnitor kf = new DbCore.DbKontabiliteti.clsKlientFurnitor();
            kf.IdKlientFurnitor = 0;
            kf.KodKlientFurnitor = "";
            col.Add(kf);
            colnew.PropertiesComboBox.DataSource = col;
            colnew.PropertiesComboBox.ClientInstanceName = "IdKlientFurnitori";
            colnew.PropertiesComboBox.ClientSideEvents.KeyPress = "function(s,e){var code =_getKeyCode(e.htmlEvent);KeyPresKF(code,IdKlientFurnitori,0); }";
            colnew.PropertiesComboBox.ClientSideEvents.TextChanged = "function(s,e){TextChangedKF(0); }";
            colnew.PropertiesComboBox.ClientSideEvents.LostFocus = "function(s,e){LostFocusKF(0);}";
            colnew.PropertiesComboBox.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedKF(IdKlientFurnitori, 0) }";
            colnew.PropertiesComboBox.DropDownButton.Visible = false;
            // cmb.ClientSideEvents.Init = "function(s,e){InitKPF()}";
            EditButton b1 = new EditButton();

            colnew.PropertiesComboBox.Buttons.Add(b1);
            colnew.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            colnew.PropertiesComboBox.DropDownStyle = DropDownStyle.DropDownList;
            colnew.PropertiesComboBox.TextFormatString = "{0}";
            // GridViewDataItemHorizontalAppointmentTemplateContainer gridContainer = (GridViewDataItemHorizontalAppointmentTemplateContainer)Container;
            //   colnew.PropertiesComboBox. = "cmbBox";
            colnew.VisibleIndex = visibleindex;
            colnew.PropertiesComboBox.TextField = "KodKlientFurnitor";
            colnew.PropertiesComboBox.ValueField = "IdKlientFurnitor";
            colnew.FieldName = "IdKlientFurnitori";

            gvLupaDok.Columns.Add(colnew);
        }

        private void shtoMonedha()
        {
            int visibleindex = gvLupaDok.Columns["IdMonedha"].VisibleIndex;
            gvLupaDok.Columns.Remove(gvLupaDok.Columns["IdMonedha"]);
            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            DbCore.DbAdmin.colMonedhat col = new DbCore.DbAdmin.colMonedhat();
            col.mbushGjitheMonedhatAktive(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            //col = dbAdmin.merrGjitheMonedhatAktive(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            DbCore.DbAdmin.clsMonedha mon = new DbCore.DbAdmin.clsMonedha();
            mon.IdMonedha = 0;
            mon.KodiMonedha = "";
            col.Add(mon);
            colnew.PropertiesComboBox.DataSource = col;
            colnew.PropertiesComboBox.TextField = "KodiMonedha";
            colnew.PropertiesComboBox.ValueField = "IdMonedha";
            colnew.FieldName = "IdMonedha";
            colnew.VisibleIndex = visibleindex;
            gvLupaDok.Columns.Add(colnew);
        }

        private void shtoStatus()
        {
            int visibleindex = gvLupaDok.Columns["Status"].VisibleIndex;
            DbCore.DbArkaBanka.clsDatabaseArkaBanka data = new DbCore.DbArkaBanka.clsDatabaseArkaBanka();
            gvLupaDok.Columns.Remove(gvLupaDok.Columns["Status"]);
            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            DataSet ds = data.merrStatusinDokumentave();
            data.Dispose();
            DataRow dr = ds.Tables[0].NewRow();
            object[] rowArray = new object[2]; rowArray[0] = null; rowArray[1] = "";
            dr.ItemArray = rowArray;
            ds.Tables[0].Rows.InsertAt(dr, 0);
            colnew.PropertiesComboBox.DataSource = ds;
            colnew.PropertiesComboBox.TextField = ds.Tables[0].Columns[1].ToString();
            colnew.PropertiesComboBox.ValueField = ds.Tables[0].Columns[0].ToString();
            colnew.FieldName = "Status";
            colnew.VisibleIndex = visibleindex;
            gvLupaDok.Columns.Add(colnew);
            ds.Dispose();
        }

        protected void gvLupaDok_DataBound(object sender, EventArgs e)
        {
            if (Request.QueryString["veprimi"] == "ShperndarjeShpenzimeshKerko" || Request.QueryString["veprimi"] == "FleteDoganore")
            {
                if (this.gvLupaDok.Columns["#"] == null)
                {
                    DevExpress.Web.GridViewCommandColumn check = new DevExpress.Web.GridViewCommandColumn("#");
                    check.ShowSelectCheckbox = true; check.Width = Unit.Percentage(2);
                    //  check.SetColVisibleIndex(0);
                    gvLupaDok.Columns.Add(check);
                    gvLupaDok.SettingsBehavior.AllowSelectByRowClick = true;
                }
            }
            gvLupaDok.KeyFieldName = "IdDokumenti";
            gvLupaDok.Settings.ShowFilterRow = true;
            gvLupaDok.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
            gvLupaDok.Settings.ShowFilterRowMenu = true;
            //gvLupaDok.SettingsBehavior.AllowFocusedRow = true;
            gvLupaDok.SettingsBehavior.AllowSelectByRowClick = true;

        }

        protected void gvLupaDok_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            gvLupaDok.Selection.UnselectAll();

        }

        protected void gvLupaDok_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 1)
            {
                //string arr = e.Parameters.ToString();
                //CacheLayer.GlobalCacheManager.MySessionCache["Periudha"] = arr;
                DbCore.mySessionObjects.ruajPeriudheNeSesion(Session, int.Parse(e.Parameters.ToString()));
                merrSipasPeriudhes(e.Parameters.ToString());
                return;
            }
        
            if (arr.Length == 2)
            {
                PrintoNeKase();
                return;
            }
            else
            {
                int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);

                if (arr.Length == 3)
                {
                    if (arr[2] == "")
                        gvLupaDok.FilterExpression = "";
                    else
                    {
                        DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                        DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaDok", "LupaDokumenta.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
                        filtra.mbushFilterPerGrideSipasKodit(arr[2], idNdermarrje, koka.IdGridaKoka);
                        if (filtra.FiltraKodi != null)
                        {
                            gvLupaDok.FilterExpression = filtra.FiltraVlera;
                            GridUtil.renditGriden(filtra.KoloneRenditje, gvLupaDok);
                        }
                    }
                }
                gvLupaDok.Selection.UnselectAll();
            }
        }

        //public void mbushDokTePeriudhes(object sender, ASPxRadioButton e)
        //{
        //    //string arr = aspxg.Parameters.ToString();
        //    //CacheLayer.GlobalCacheManager.MySessionCache["Periudha"] = arr;
        //    //merrSipasPeriudhes(arr);
        //}

        public void merrSipasPeriudhes(string arr)
        {
            //  DbCore.DbRegjistrim.colDokumentat col = new colDokumentat();
            //dbRegjistrim = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
            DbCore.DbAdmin.clsPeriudhaKontabel oPeriudha = new DbCore.DbAdmin.clsPeriudhaKontabel();
            int idperdorues = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            //oPeriudha = (DbCore.DbAdmin.clsPeriudhaKontabel)CacheLayer.GlobalCacheManager.MySessionCache["oPeriudhaAktuale"];
            oPeriudha = DbCore.mySessionObjects.merrPeriudheKontabel(Session);
            DataTable dt = new DataTable();
            string kodNivel = DbCore.DbRegjistrim.clsNivelRegjistrimi.ktheKodNivelRegjistrimi(niveli);
            int idKategori = DbCore.DbRegjistrim.clsNivelRegjistrimi.ktheIdKategoriaNivelRegjistrimi(niveli);
            string datanga = "";
            string dataderi = "";
            int idndermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            if (arr == "1")
            {
                datanga = "01/" + DateTime.Today.Month + "/" + DateTime.Today.Year;
                datanga = oPeriudha.FillimiPeriudha.ToShortDateString();

                dataderi = oPeriudha.MbarimiPeriudha.ToShortDateString();
            }
            else if (arr == "2")
            {
                datanga = "01/01/" + oPeriudha.MbarimiPeriudha.Year.ToString();
                dataderi = "31/12/" + oPeriudha.MbarimiPeriudha.Year.ToString();
            }
            else
            {
                datanga = dteNga.Text;
                dataderi = dteDeri.Text;
            }

            if (veprimi == "VeprimeBanka")
            {
                //clsNivelRegjistrimi nive = new clsNivelRegjistrimi();
                //nive.mbushNivelRegjistrimiSipasIdMeKonvertime(niveli);
                int idKategoria = clsNivelRegjistrimi.ktheIdKategoriaNivelRegjistrimi(niveli);
                dt = DbCore.DbRegjistrim.colDokumentat.mbushGjitheDokumentatVeprimeBanka(idndermarje, datanga, dataderi, idKategoria, idperdorues, niveli);

                gvLupaDok.DataSource = dt;
            }
            if (veprimi == "VeprimeBankaPaprintuar")
            {
                //clsNivelRegjistrimi nive = new clsNivelRegjistrimi();
                //nive.mbushNivelRegjistrimiSipasIdMeKonvertime(niveli);
                int idKategoria = clsNivelRegjistrimi.ktheIdKategoriaNivelRegjistrimi(niveli);
                dt = DbCore.DbRegjistrim.colDokumentat.mbushGjitheDokumentatVeprimeBankaPaPrintuar(idndermarje, datanga, dataderi, idperdorues, false, int.Parse(Request.QueryString["idshop"]), idKategoria);
                //col = dbRegjistrim.merrGjitheDokumentatVeprimeBanka(funk.ktheNdermarrjeVit(), datanga, dataderi);
                gvLupaDok.DataSource = dt;
            }
            if (veprimi == "RegjistrimDokumentash")
            {
                dt = DbCore.DbRegjistrim.colDokumentat.mbushGjitheDokumentatRegjistrimDokumentash(idndermarje, datanga, dataderi, idperdorues, idKategori);
                gvLupaDok.DataSource = dt;
            }
            if (veprimi == "RegjistrimDokumentashPaprintuar")
            {
                dt = DbCore.DbRegjistrim.colDokumentat.mbushGjitheDokumentatRegjistrimDokumentashPaPrintuar(idndermarje, datanga, dataderi, idperdorues, false, int.Parse(Request.QueryString["idshop"]));
                //col = dbRegjistrim.merrGjitheDokumentatRegjistrimDokumentash(funk.ktheNdermarrjeVit(), datanga, dataderi);
                gvLupaDok.DataSource = dt;
            }
            if (veprimi == "GjeneroProjekt")
            {
                dt = DbCore.DbRegjistrim.colDokumentat.ktheGjitheDokumentatRegjistrimDokumentashMeKushtUPPDheIntervalDate(idndermarje, datanga, dataderi, idperdorues);
                //col = dbRegjistrim.merrGjitheDokumentatRegjistrimDokumentash(funk.ktheNdermarrjeVit(), datanga, dataderi);
                gvLupaDok.DataSource = dt;
            }
            if (veprimi == "AzhornimKF")
            {
                dt = DbCore.DbRegjistrim.colDokumentat.mbushGjitheDokumentatAzhornimDokumentash(idndermarje, datanga, dataderi, idperdorues);
                //col = dbRegjistrim.merrGjitheDokumentatRegjistrimDokumentash(funk.ktheNdermarrjeVit(), datanga, dataderi);
                gvLupaDok.DataSource = dt;
            }
            if (veprimi == "MbylljeKF")
            {
                dt = DbCore.DbRegjistrim.colDokumentat.mbushGjitheDokumentatMbylljeDokumentash(idndermarje, datanga, dataderi, idperdorues);
                //col = dbRegjistrim.merrGjitheDokumentatRegjistrimDokumentash(funk.ktheNdermarrjeVit(), datanga, dataderi);
                gvLupaDok.DataSource = dt;
            }
            if (veprimi == "QendraKosto")
            {
                dt = DbCore.DbRegjistrim.colDokumentat.ktheGjitheDokumentatQendraKostoDokumentash(idndermarje, datanga, dataderi, idperdorues);
                gvLupaDok.DataSource = dt;
            }
            if (veprimi == "Amortizimi" )
            {
                dt = DbCore.DbRegjistrim.colDokumentat.ktheGjitheDokumentatAmortizimiDokumentash(idndermarje, datanga, dataderi, idperdorues);
                gvLupaDok.DataSource = dt;
            }
            
            if ( veprimi == "RivleresimeAmortizimi")
            {
                if (kodNivel == "AMFI" || kodNivel == "AMFIR")
                  dt = DbCore.DbRegjistrim.colDokumentat.ktheGjitheDokumentatRivleresimAmortizimiDokumentash(idndermarje, datanga, dataderi, idperdorues, "AMFI;AMFIR");
                if (kodNivel == "RIAM" || kodNivel == "RIAMR")
                    dt = DbCore.DbRegjistrim.colDokumentat.ktheGjitheDokumentatRivleresimAmortizimiDokumentash(idndermarje, datanga, dataderi, idperdorues, "RIAM;RIAMR");
                gvLupaDok.DataSource = dt;
                
            }
            
            if (veprimi == "FleteKontabel")
            {
                dt = DbCore.DbRegjistrim.colDokumentat.mbushGjitheDokumentatFleteKontabel(idndermarje, datanga, dataderi, idperdorues);
                gvLupaDok.DataSource = dt;
            }
            if (veprimi == "RegjistrimMagazine")
               
                {
                if (kodNivel == "FH" || kodNivel == "UH")
                    dt = DbCore.DbRegjistrim.colDokumentat.mbushGjitheDokumentatMagazines(idndermarje, datanga, dataderi, idperdorues, 1);
                if (kodNivel == "FD" || kodNivel == "UD")
                    dt = DbCore.DbRegjistrim.colDokumentat.mbushGjitheDokumentatMagazines(idndermarje, datanga, dataderi, idperdorues, 2);
               
                gvLupaDok.DataSource = dt;

            }
            if (veprimi == "RegjistrimNdryshimCmimSasi")
            {
                dt = DbCore.DbRegjistrim.colDokumentat.ktheGjitheDokumentatNdryshimCmimSasi(idndermarje, datanga, dataderi, idperdorues);
                gvLupaDok.DataSource = dt;

            }
            if (veprimi == "RegjistrimInventarizimi")
            {
                dt = DbCore.DbRegjistrim.colDokumentat.ktheGjitheDokumentatInventarizimi(idndermarje, datanga, dataderi, idperdorues);
                gvLupaDok.DataSource = dt;

            }
            if (veprimi == "RegjistrimRezervimi")
            {
                
                dt = DbCore.DbRegjistrim.colDokumentat.mbushGjitheDokumentatRezervimit(idndermarje, datanga, dataderi, idperdorues, niveli);
                gvLupaDok.DataSource = dt;

            }
            if (veprimi == "RegjistrimRiparimi")
            {
                dt = DbCore.DbRegjistrim.colDokumentat.mbushGjitheDokumentatRiparimi(idndermarje, datanga, dataderi, idperdorues);
                gvLupaDok.DataSource = dt;

            }
            if (veprimi == "SkedulimProdhimi")
            {
                dt = DbCore.DbRegjistrim.colDokumentat.ktheGjitheDokumentatSkedulim(idndermarje, datanga, dataderi, idperdorues);
                gvLupaDok.DataSource = dt;

            }
            if (veprimi == "VeprimeKF")
            {
                dt = DbCore.DbRegjistrim.colDokumentat.mbushGjitheDokumentatVeprimeKF(idndermarje, datanga, dataderi, idperdorues);
                gvLupaDok.DataSource = dt;

            }
            if (veprimi == "LidhjaDokumentave")
            {
                dt = DbCore.DbRegjistrim.colDokumentat.mbushGjitheDokumentatLidhesdok(idndermarje, datanga, dataderi, idperdorues);
                gvLupaDok.DataSource = dt;

            }
            if (veprimi == "ShperndarjeShpenzimeshKerkoMenu")
            {
                dt = DbCore.DbRegjistrim.colDokumentat.mbushGjitheDokumentatShperndarjeShpenzimesh(idndermarje, datanga, dataderi, idperdorues);
                gvLupaDok.DataSource = dt;
            }
            if (veprimi == "ShperndarjeShpenzimeshKerko")
            {
                dt = DbCore.DbRegjistrim.colDokumentat.mbushGjitheRegjistrimDokumentashKerkimiSHSH(idndermarje, datanga, dataderi);
                gvLupaDok.DataSource = dt;
            }
            if (veprimi == "FleteDoganore")
            {
               dt = DbCore.DbRegjistrim.colDokumentat.mbushGjitheDokumentatFleteDoganoreKerkimi(idndermarje, datanga, dataderi, idperdorues, niveli);
               gvLupaDok.DataSource = dt;
            }
            if (veprimi == "ListPagesa")
            {
                dt = DbCore.DbRegjistrim.colDokumentat.ktheGjitheDokumentatListPagesa(idndermarje, datanga, dataderi, idperdorues);
                gvLupaDok.DataSource = dt;
            }
            if (veprimi == "Planifikim")
            {
                dt = DbCore.DbRegjistrim.colDokumentat.ktheGjitheDokumentatPlanifikim(idndermarje, datanga, dataderi, idperdorues);
                gvLupaDok.DataSource = dt;
            }
            if (veprimi == "EkzekutimProdhimi")
            {
                dt = DbCore.DbRegjistrim.colDokumentat.ktheGjitheDokumentatEkzekutim(idndermarje, datanga, dataderi, idperdorues);
                gvLupaDok.DataSource = dt;
            }
            if (veprimi == "UrdherPagesa")
            {
                dt = DbCore.DbRegjistrim.colDokumentat.ktheGjitheDokumentatUrdherPagesa(idndermarje, datanga, dataderi, idperdorues);
                gvLupaDok.DataSource = dt;
            }
            if (veprimi == "RecetaOptike")
            {
                dt = DbCore.DbRegjistrim.colDokumentat.ktheGjitheDokPerRecetaOptike(idndermarje, datanga, dataderi, idperdorues);
                gvLupaDok.DataSource = dt;
            }
            DbCore.mySessionObjects.ruajGrideNeSessionLupa(Session, dt);
            gvLupaDok.DataBind();
            dt.Dispose();
        }

        protected void gvLupaDok_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpNoRows"] = gvLupaDok.VisibleRowCount;
        }
        
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        }
        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        /// <param name="idViti"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNermarrje"></param>
        private void percaktoTemplateMenu(ASPxMenu aSPxMenu1, int idViti, int idPerdorues, int idNdermarrje)
        {
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, "LupaDokumenta.aspx", this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            aSPxMenu1.Items.FindByName("Anullo").Text = rm.GetString("MenuItemMbyll", ci);
            var printo = aSPxMenu1.Items.FindByName("Printo");
            if (veprimi == "VeprimeBankaPaprintuar" || veprimi == "RegjistrimDokumentashPaprintuar")
            {
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdorues, idNdermarrje, idViti, "LupaDokumenta.aspx");
                printo.Text = "Printo ne kase";
                if (!tedrejtaInfo.DShtim)
                    printo.ClientVisible = false;
            }
            else
                printo.ClientVisible = false;
        }

       
        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            clsMenuInfo.ShtoMenuItemInfo(this, MenuInfo);
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            DbCore.DbAdmin.clsFiltraGrida filtri = new DbCore.DbAdmin.clsFiltraGrida();
            filtri.FiltraKodi = cmbFiltra.Text;
            filtri.FiltraShenime = cmbFiltra.Text;
            filtri.FiltraUniversal = false;// Universal_ASPxCheckBox.Checked;
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaDok", "LupaDokumenta.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gvLupaDok.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("IdDokumenti", gvLupaDok);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvLupaDok.GetSortedColumns();
            //if (kolona.Count > 0)
            //{
            //    filtri.KoloneRenditje = kolona[0].FieldName;
            //    if (kolona[0].SortOrder == DevExpress.Data.ColumnSortOrder.Ascending)
            //        filtri.DrejtimRenditje = true;
            //    else
            //        filtri.DrejtimRenditje = false;
            //}
            //else
            //{
            //    filtri.KoloneRenditje = "IdDokumenti";
            //    filtri.DrejtimRenditje = true;
            //}
            DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();

            oPerdorues = DbCore.mySessionObjects.kthePerdorues(Session);
            filtri.IdPerdoruesi = oPerdorues.IdPerdorues;
            filtri.IdNdermarje = idNdermarrje;
            filtri.IdStatusDok = 1;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            mesazh = filtri.ruaj();
            //mbushComboBoxFiltra(idNdermarrje);
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaDok", Convert.ToInt32(cmbKonfigurimi.Value), "LupaDokumenta.aspx");
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);
            if (mesazh.Status == true)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            cmbFiltra.Text = "";
        }

        public void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje
            clsMenuInfo.ShtoMenuItemInfo(this, MenuInfo);
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaDok", "LupaDokumenta.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();
                //mbushComboBoxFiltra(idNdermarrje);
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaDok", Convert.ToInt32(cmbKonfigurimi.Value), "LupaDokumenta.aspx");
                percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);
                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                gvLupaDok.FilterExpression = String.Empty;
            }
        }
        public void PrintoNeKase()
        {
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "LupaDokumenta.aspx");
            if (!tedrejtaInfo.DShtim)
            {
                gvLupaDok.JSProperties["cpMesazh"] = JsonConvert.SerializeObject(new clsMesazh(false, "Ju nuk keni te drejta per kryer kete veprim!"));
                return;
            }
            var ids = gvLupaDok.GetSelectedFieldValues("IdDokumenti");
            clsMesazh mesazh = new clsMesazh();
            if (ids.Count == 0 && gvLupaDok.FocusedRowIndex > -1)
            {
                ids.Add(gvLupaDok.GetRowValues(gvLupaDok.FocusedRowIndex, "IdDokumenti"));
            }
            if (ids.Count == 0)
            {
                mbushPopUpListeDokumentashNgaSession();
                gvLupaDok.JSProperties["cpMesazh"] = JsonConvert.SerializeObject(new clsMesazh(false, "Nuk keni asnje rresht te selektuar!"));
                return;
            }
            if (veprimi == "RegjistrimDokumentashPaprintuar")
            {
                foreach (var id in ids)
                {
                    clsKokaShitje shitje = new clsKokaShitje(Convert.ToInt32(id));
                    clsKlientFurnitor kf = new clsKlientFurnitor(shitje.IdKlientFurnitor);
                    var fatureshitjekupontatimorngaurdhershitja = shitje.ktheFatureMeKuponNgaUSH();
                    mesazh.PershkrimMesazhi = "Gabim gjate ruajtjes se printimit ne kase!";
                    mesazh = shitje.PrintoNeKase(fatureshitjekupontatimorngaurdhershitja, DbCore.mySessionObjects.kthePerdorues(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), kf, DbCore.mySessionObjects.merrIPKasaNgaWebServisi(Session)).Item1;
                }
            }
            else if (veprimi == "VeprimeBankaPaprintuar")
            {
                foreach (var id in ids)
                {
                    clsVeprimBankaKoka koka = new clsVeprimBankaKoka(Convert.ToInt32(id));
                    mesazh = koka.PrintoArketimeNeKase(DbCore.mySessionObjects.kthePerdorues(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.merrIPKasaNgaWebServisi(Session));
                }
            }
            merrSipasPeriudhes(DbCore.mySessionObjects.merrPeriudheNgaSesioni(Session).ToString());
            gvLupaDok.Selection.UnselectAll();
            gvLupaDok.JSProperties["cpMesazh"] = JsonConvert.SerializeObject(mesazh);
        }
        /// <summary>
        /// Vendos emrat e label ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo cultinf, ResourceManager rm)
        {
            lblPeriudha.Text = rm.GetString("RadioButtonListEditItemPeriudha", cultinf);
            rbAktuale.Text = rm.GetString("RadioButtonListEditItemAktuale", cultinf);
            rbVitiUshtrimor.Text = rm.GetString("RadioButtonListEditItemVitiUshtrimor", cultinf);
            rbNgaDeri.Text = rm.GetString("GridHeaderFilterFillItemNga", cultinf);
            lblDeri.Text = rm.GetString("labelRaportDeri", cultinf);
            btnKerko.Text = rm.GetString("ReportToolbarButtonSearch", cultinf);

        }
    }
}
