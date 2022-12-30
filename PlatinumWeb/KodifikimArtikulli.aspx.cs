using DbCore;
using DbCore.DbAdmin;
using DbCore.DbInventari;
using DbCore.IMBUtils.Messages;
using DevExpress.Web;
using DevExpress.Web.ASPxTreeList;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using PlatinumWeb.Templates;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PlatinumWeb
{
    public partial class KodifikimArtikulli : MyPageBase
    {
        ArrayList vlerat = new ArrayList();
        //DbCore.DbInventari.clsKodifikimArtikulli kodifikim;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                Response.Redirect(DbCore.IMBUtils.Paths.defaultLoginPath);
                return;
            }
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + IdPerdoruesi);
                return;
            }

            clsNdermarrje nderm = new clsNdermarrje(IdNdermarrja);
            hfState.Set("llojNdermarrje", nderm.Lloji);
            int idViti = IdViti;
            bool lupe = false;

            clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, IdNdermarrja, "gvKodifikimArtikulli", 1, DbCore.clsFunksione.GetKomponente(Page.Request));

            if (!IsPostBack)
            {
                EmrateTabeve();
                mbushHiddenFieldMePerkthime();
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, ":");
                if (Request.QueryString["ruaj"] == "ok")
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["msgBlerjeShitjeRuajtjeMeSukses"], pnlMesazhi);
                lupe = !String.IsNullOrEmpty(Request.QueryString["lupe"]) && Request.QueryString["lupe"] == "true";
                hfState.Set("lupe", lupe);
                if (lupe)
                {
                    string vleraQueryString = (Request.QueryString["idKonfigAmbjente"] != null && Request.QueryString["idKonfigAmbjente"] != "") ? Request.QueryString["idKonfigAmbjente"].ToString() : String.Empty;
                    int idKonfigambjenti = DbCore.clsFunksione.getIdKonfigAmbLupa(vleraQueryString, IdNdermarrja, "KodArt");
                    bool kerkosaposhkruar = DbCore.DbShare.clsAlternativaKushti.getAlternativa(idKonfigambjenti, "KSSH") == "Po";
                    hfState.Set("kerkosaposhkruar", kerkosaposhkruar);
                }
                konfiguroVleraFillestare();
                ConfigureAspxComboBox.ShtoKolonaPerLlogarine(btneLlogBle);
                ConfigureAspxComboBox.ShtoKolonaPerLlogarine(btneLlogShit);
                ConfigureAspxComboBox.ShtoKolonaPerLlogarine(btneLlogTretet);
                ConfigureAspxComboBox.ShtoKolonaPerLlogarine(btneLlogInv);
                ConfigureAspxComboBox.ShtoKolonaPerLlogarine(cmbLlogAmortizimi);
                ConfigureAspxComboBox.ShtoKolonaPerLlogarine(btnLlogShpe);
                konfiguroCombo(IdNdermarrja);
                konfiguroGridat(lupe);
                konfiguroVleraFillestareNorma();
                konfiguroGrideNorma();
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, idViti, DbCore.clsFunksione.GetKomponente(Page.Request));
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
            }
            else
            {
                lupe = (bool)hfState.Get("lupe");
                if ((!IsCallback || (IsCallback && Request["__CALLBACKID"].ToString().Contains("gvKodifikimArtikulli"))) && (!lupe || (lupe && int.Parse(Request.QueryString["llojKodifikimi"]) == 1)))
                    konfiguroGride(gvKodifikimArtikulli, lupe);
                if ((!IsCallback || (IsCallback && Request["__CALLBACKID"].ToString().Contains("gvKodifikimArtikulliGr2"))) && (!lupe || (lupe && int.Parse(Request.QueryString["llojKodifikimi"]) == 2)))
                    konfiguroGride(gvKodifikimArtikulliGr2, lupe);
                if ((!IsCallback || (IsCallback && Request["__CALLBACKID"].ToString().Contains("gvKodifikimArtikulliGr3"))) && (!lupe || (lupe && int.Parse(Request.QueryString["llojKodifikimi"]) == 3)))
                    konfiguroGride(gvKodifikimArtikulliGr3, lupe);
                konfiguroGrideNorma();
            }
            percaktoTemplateMenu(ASPxMenu1, idViti, IdPerdoruesi, IdNdermarrja, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
            if (!lupe || (lupe && int.Parse(Request.QueryString["llojKodifikimi"]) == 1))
                gvKodifikimArtikulli.PercaktoTitlePanel(this, _menu, pnlMesazhi, hfState, IdPerdoruesi, IdNdermarrja, IdViti, IdGjuha, 1, "KontabilizimDokumenti.aspx", rm, ci);
            if (!lupe || (lupe && int.Parse(Request.QueryString["llojKodifikimi"]) == 2))
                gvKodifikimArtikulliGr2.PercaktoTitlePanel(this, _menu, pnlMesazhi, hfState, IdPerdoruesi, IdNdermarrja, IdViti, IdGjuha, 1, "KontabilizimDokumenti.aspx", rm, ci);
            if (!lupe || (lupe && int.Parse(Request.QueryString["llojKodifikimi"]) == 3))
                gvKodifikimArtikulliGr3.PercaktoTitlePanel(this, _menu, pnlMesazhi, hfState, IdPerdoruesi, IdNdermarrja, IdViti, IdGjuha, 1, "KontabilizimDokumenti.aspx", rm, ci);
            DbCore.mySessionObjects.ruajMesazhNeSesion(Session, ":");
            konfiguroVleraFillestare();
            AspxWebControlUtils.perkthePopUp(popFshi, MessagesResource.Messages["labelKujdes"], lblMsgbox, MessagesResource.Messages["labelAdministrimiMsgJeniSigurt"], ButtonCancel, MessagesResource.Messages["labelAnullo"]);
            popupUniversal.HeaderText = MessagesResource.Messages["headerPopUpText"];
        }

        /// <summary>
        /// Vendos emrat e tabeve ne baze te gjuhes se perdoruesit
        /// </summary>        
        private void EmrateTabeve()
        {
            ASPxPageControl1.TabPages[0].Text = MessagesResource.Messages["labelGrupimKlientPare"];
            ASPxPageControl1.TabPages[1].Text = MessagesResource.Messages["labelGrupimKlientDyte"];
            ASPxPageControl1.TabPages[2].Text = MessagesResource.Messages["labelGrupimKlientTrete"];
            ASPxPageControl1.TabPages[3].Text = MessagesResource.Messages["labelAdministrimiInformacion"];
        }

        private void konfiguroCombo(int idndermarje)
        {
            ConfigureAspxComboBox.mbushComboNrAutomatik(idndermarje, 67, cmbFormati);
            ConfigureAspxComboBox.percaktoTemplateComboMeEnableCallback(btneEmertimPrindi);
            ConfigureAspxComboBox.percaktoTemplateComboMeEnableCallback(btneLlogBle);
            ConfigureAspxComboBox.percaktoTemplateComboMeEnableCallback(btneLlogInv);
            ConfigureAspxComboBox.percaktoTemplateComboMeEnableCallback(btneLlogShit);
            ConfigureAspxComboBox.percaktoTemplateComboMeEnableCallback(btneLlogTretet);
            ConfigureAspxComboBox.percaktoTemplateComboMeEnableCallback(btnLlogShpe);
            ConfigureAspxComboBox.percaktoTemplateComboMeEnableCallback(btnLlogPakesim);
            ConfigureAspxComboBox.percaktoTemplateComboMeEnableCallback(cmbLlogAmortizimi);
            ConfigureAspxComboBox.percaktoTemplateComboMeEnableCallback(btneSkema);
            ConfigureAspxComboBox.shtokolonakodifikime(btneEmertimPrindi);
            if (Request.QueryString["llojiart"] == "aqt")
            {
                ConfigureAspxComboBox.shtoKolonaSkemaArtikulli(btneSkema, true, idndermarje);
            }
            hfSkemaKlasa.Value = ConfigureAspxComboBox.mbushComboSkemaKontabilitetiArtikulli(idndermarje, btneSkema, Request.QueryString["llojiart"], "");
            hfLupaLlogInv.Value = DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(@"LP\Llog InventarAQT", idndermarje).ToString();
            hfLupaLlogBle.Value = DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(@"Lp\LlogVlKontAQT", idndermarje).ToString();
            hfLupaLlogShit.Value = DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(@"Lp\LlogShitjeAQT", idndermarje).ToString();
            hfLupaLlogTretet.Value = DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(@"Lp\LlogAAProcAQT", idndermarje).ToString();
            hfLupaLlogShpe.Value = DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(@"Lp\LlogShpAmoAQT", idndermarje).ToString();
            hfLupaLlogAmortizimi.Value = DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(@"Lp\LlogAmoAQT", idndermarje).ToString();
            hfLupaLlogPakesim.Value = DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(@"Lp\LlogPk", idndermarje).ToString();
        }

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        /// <param name="idViti"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNdermarrje"></param>
        private void percaktoTemplateMenu(ASPxMenu aSPxMenu1, int idViti, int idPerdorues, int idNdermarrje, bool meme)
        {
            clsToolbarConfig.percaktoTemplateMenu(IdGjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1, DbCore.clsFunksione.GetKomponente(Page.Request), this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, hfRuaj.Value == "Ruaj" ? true : false, true, false, meme);
            aSPxMenu1.Items.FindByName("OK").Visible = (bool)hfState.Get("lupe");
        }

        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(ASPxMenu1, IdViti, IdPerdoruesi, IdNdermarrja, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
        }

        protected void gv_DataBound(object sender, EventArgs e)
        {
            ASPxGridView grid = (ASPxGridView)sender;

            if (grid.Columns["#"] == null)
            {
                GridViewCommandColumn check = new GridViewCommandColumn("#") { Index = 0, VisibleIndex = 0, ShowSelectCheckbox = true, Width = System.Web.UI.WebControls.Unit.Percentage(2) };
                grid.Settings.ShowFilterRow = true;
                grid.Settings.ShowHeaderFilterButton = true;
                grid.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                grid.Settings.ShowFilterRowMenu = true;
                grid.Columns.Add(check);
                grid.Settings.ShowGroupPanel = true;
                grid.SettingsBehavior.AllowSelectByRowClick = true;
                grid.SettingsBehavior.AllowFocusedRow = true;
            }
        }


        private void konfiguroVleraFillestare()
        {
            if (!(bool)hfState.Get("lupe"))
            {
                mbushGriden(gvKodifikimArtikulli, 1);
                mbushGriden(gvKodifikimArtikulliGr2, 2);
                mbushGriden(gvKodifikimArtikulliGr3, 3);
            }
            else
            {
                int lloji = int.Parse(Request.QueryString["llojKodifikimi"]);
                ASPxGridView grida = lloji == 1 ? gvKodifikimArtikulli : lloji == 2 ? gvKodifikimArtikulliGr2 : gvKodifikimArtikulliGr3;
                mbushGriden(grida, lloji);
            }
        }

        private void mbushGriden(ASPxGridView grida, int lloji)
        {
            mbushGridenKodifikimeve(grida, lloji);
        }

        private void mbushGridenKodifikimeve(ASPxGridView grida, int lloji)
        {
            bool llojartikulli;
            if (Request.QueryString["llojiart"] == "aqt")
                llojartikulli = true;
            else llojartikulli = false;
            DataTable dt = DbCore.DbInventari.colKodifikimeArtikulli.merrKodifikimArtikulliSipasLlojitDt(lloji, IdNdermarrja, llojartikulli);
            grida.DataSource = dt;
            grida.DataBind();
        }

        private void konfiguroGridat(bool lupe)
        {
            if (!lupe)
            {
                konfiguroGride(gvKodifikimArtikulli, lupe);
                konfiguroGride(gvKodifikimArtikulliGr2, lupe);
                konfiguroGride(gvKodifikimArtikulliGr3, lupe);
            }
            else
            {
                int lloji = int.Parse(Request.QueryString["llojKodifikimi"]);
                switch (lloji)
                {
                    case 1:
                        konfiguroGride(gvKodifikimArtikulli, lupe);
                        break;
                    case 2:
                        konfiguroGride(gvKodifikimArtikulliGr2, lupe);
                        break;
                    case 3:
                        konfiguroGride(gvKodifikimArtikulliGr3, lupe);
                        break;
                }
            }
        }

        private void konfiguroGride(ASPxGridView grida, bool lupe)
        {
            if (grida.Columns.Count != 0)
            {
                grida.Columns["#"].VisibleIndex = 0;
                grida.Columns["#"].Index = 0;
            }
            if ((grida == gvKodifikimArtikulli && hfShtuarGrup1.Value == "true") || (grida == gvKodifikimArtikulliGr2 && hfShtuarGrup2.Value == "true") || (grida == gvKodifikimArtikulliGr3 && hfShtuarGrup3.Value == "true"))
                shtoPrind(grida, true);
            else
                shtoPrind(grida, false);
            shto_Skeme(grida, IdNdermarrja);
            GridUtil.percaktoVisibleColumnsMeWidth(IdGjuha, IdNdermarrja, grida, "gvKodifikimArtikulli", DbCore.clsFunksione.GetKomponente(Page.Request));
            if (!lupe)
                GridUtil.konfigGrideListeEMadhePaTheme(grida, "IdKodifikimi");
            else
                GridUtil.KonfiguroGrideListeMadhePopupiPaTheme(grida, "IdKodifikimi", (bool)hfState.Get("kerkosaposhkruar"), false);
            grida.SettingsBehavior.AllowSelectByRowClick = true;
        }

        private void shto_Skeme(ASPxGridView grida, int idNdermarrje, int idklasa = 0)
        {
            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            if (grida.Columns["IdSkemaKontabel"].Visible)
            {
                if (typeof(GridViewDataComboBoxColumn) != grida.Columns["IdSkemaKontabel"].GetType())
                {
                    int visibleind = grida.Columns["IdSkemaKontabel"].VisibleIndex;
                    grida.Columns.Remove(grida.Columns["IdSkemaKontabel"]);
                    grida.Columns.Add(colnew);
                    DbCore.DbInventari.colSkematKontabilitetiArtikulli skemat = new DbCore.DbInventari.colSkematKontabilitetiArtikulli(idNdermarrje, true, idklasa = 0);
                    skemat.Insert(0, new DbCore.DbInventari.clsSkemaKontabilitetiArtikulli(0, "", "", 0, 0, 0, 0, 0, 0, 0, 0, 0, false));
                    colnew.PropertiesComboBox.DataSource = skemat;
                    colnew.PropertiesComboBox.TextField = "KodiSkemaKontabilitetiArtikulli";
                    colnew.PropertiesComboBox.ValueField = "IdSkemaKontabilitetiArtikulli";
                    colnew.FieldName = "IdSkemaKontabel";
                    colnew.VisibleIndex = visibleind;
                    colnew.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                    DbCore.mySessionObjects.ruajDsComboGrideNeSession(Session, skemat, "skemat");
                }
                else
                {
                    colnew = (GridViewDataComboBoxColumn)grida.Columns["IdSkemaKontabel"];
                    if (colnew.PropertiesComboBox.Items.Count == 0)
                    {
                        colnew.PropertiesComboBox.DataSource = DbCore.mySessionObjects.merrDsComboGrideNeSession(Session, "skemat");

                    }
                }
            }
        }

        private void shtoPrind(ASPxGridView grida, bool mbush)
        {
            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            if (typeof(GridViewDataComboBoxColumn) != grida.Columns["IdPrindi"].GetType() || mbush)
            {
                grida.Columns.Remove(grida.Columns["IdPrindi"]);
                grida.Columns.Add(colnew);
                DbCore.DbInventari.colKodifikimeArtikulli kodifikimet = new DbCore.DbInventari.colKodifikimeArtikulli();
                DbCore.DbInventari.clsKodifikimArtikulli kod = new DbCore.DbInventari.clsKodifikimArtikulli();
                kod.IdKodifikimi = 0;
                kodifikimet.Add(kod);
                bool llojartikulli;
                if (Request.QueryString["llojiart"] == "aqt")
                    llojartikulli = true;
                else llojartikulli = false;
                if (grida == gvKodifikimArtikulli)
                    kodifikimet.merrKodifikimArtikulliSipasLlojit(1, IdNdermarrja, llojartikulli);
                else
                    if (grida == gvKodifikimArtikulliGr2)
                    kodifikimet.merrKodifikimArtikulliSipasLlojit(2, IdNdermarrja, llojartikulli);
                else
                    if (grida == gvKodifikimArtikulliGr3)
                    kodifikimet.merrKodifikimArtikulliSipasLlojit(3, IdNdermarrja, llojartikulli);
                colnew.PropertiesComboBox.DataSource = kodifikimet;
                colnew.PropertiesComboBox.TextField = "PershkrimKodifikimi";
                colnew.PropertiesComboBox.ValueField = "IdKodifikimi";
                colnew.Caption = "Prindi";
                colnew.FieldName = "IdPrindi";
                colnew.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            }
            else
            {
                colnew = (GridViewDataComboBoxColumn)grida.Columns["IdPrindi"];
                bool llojartikulli;
                DbCore.DbInventari.colKodifikimeArtikulli kodifikimet = new DbCore.DbInventari.colKodifikimeArtikulli();
                DbCore.DbInventari.clsKodifikimArtikulli kod = new DbCore.DbInventari.clsKodifikimArtikulli();
                kod.IdKodifikimi = 0;
                kodifikimet.Add(kod);

                if (Request.QueryString["llojiart"] == "aqt")
                    llojartikulli = true;
                else llojartikulli = false;
                if (grida == gvKodifikimArtikulli)
                    kodifikimet.merrKodifikimArtikulliSipasLlojit(1, IdNdermarrja, llojartikulli);
                else
                    if (grida == gvKodifikimArtikulliGr2)
                    kodifikimet.merrKodifikimArtikulliSipasLlojit(2, IdNdermarrja, llojartikulli);
                else
                    if (grida == gvKodifikimArtikulliGr3)
                    kodifikimet.merrKodifikimArtikulliSipasLlojit(3, IdNdermarrja, llojartikulli);
                colnew.PropertiesComboBox.DataSource = kodifikimet;
            }
            percaktoTamplatePrindi(grida);
        }

        private void percaktoTamplatePrindi(ASPxGridView grida)
        {//tempatet per kolonat e Autorizimeve
            GridViewDataComboBoxColumn col7;
            col7 = grida.Columns["IdPrindi"] as GridViewDataComboBoxColumn;
            int idNdermarrje = IdNdermarrja;
            bool llojartikulli;
            if (Request.QueryString["llojiart"] == "aqt")
                llojartikulli = true;
            else llojartikulli = false;
            if (grida.ClientInstanceName == "gvKodifikimArtikulli")
                col7.EditItemTemplate = new MyTemplatePrindiKodifikimArtikulli(idNdermarrje, 1, llojartikulli);
            else if (grida.ClientInstanceName == "gvKodifikimArtikulliGr2") col7.EditItemTemplate = new MyTemplatePrindiKodifikimArtikulli(idNdermarrje, 2, llojartikulli);
            else if (grida.ClientInstanceName == "gvKodifikimArtikulliGr3") col7.EditItemTemplate = new MyTemplatePrindiKodifikimArtikulli(idNdermarrje, 3, llojartikulli);
            if (hfRuaj.Value.ToString() == "Ruaj")
                col7.ReadOnly = false;
            else col7.ReadOnly = false;
        }

        protected void gridat_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            e.Values.Clear();
            e.AddValue("(Te gjithe)", string.Empty, "true");
        }

        protected void startRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {//kur fillon editimi te behet validimi
            ASPxGridView grida = sender as ASPxGridView;
            if (!grida.IsNewRowEditing)
            {
                grida.DoRowValidation();
            }
        }

        protected void rowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            //merr te dhenat e rreshtit te ri te grides
            ASPxGridView gride = sender as ASPxGridView;
            int idndermarje = IdNdermarrja;
            int idperdoruesi = IdPerdoruesi;
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
            bool llojartikulli = Request.QueryString["llojiart"] == "aqt";
            
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idperdoruesi, idndermarje, IdViti, DbCore.clsFunksione.GetKomponente(Page.Request));
            if (!tedrejtaInfo.DShtim)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["mesazhRaportNukKeniTeDrejta"], pnlMesazhi);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, MessagesResource.Messages["msgNukKeniTeDrejtaRed"]);
                return;
            }
            string kodi = DbCore.clsFunksione.ktheStringunPaHapesira(e.NewValues["KodKodifikimi"].ToString(), true);
            string pershkrimi = DbCore.clsFunksione.ktheStringunPaHapesira(e.NewValues["PershkrimKodifikimi"].ToString(), false);
            int llojkodifikimi = 0;
            string kodlloji = "";
            if (gride.ClientInstanceName == "gvKodifikimArtikulli") { llojkodifikimi = 1; kodlloji = "Grupimi 1"; }
            else if (gride.ClientInstanceName == "gvKodifikimArtikulliGr2") { llojkodifikimi = 2; kodlloji = "Grupimi 2"; }
            else if (gride.ClientInstanceName == "gvKodifikimArtikulliGr3") { llojkodifikimi = 3; kodlloji = "Grupimi 3"; }
            int idprindi = 0;
            string kodprindi = "";
            if (hfPrindi.Value != "")
            {
                idprindi = new DbCore.DbInventari.clsKodifikimArtikulli(hfPrindi.Value, idndermarje, llojkodifikimi, llojartikulli).IdKodifikimi;
                kodprindi = hfPrindi.Value;
            }
            int niveli = int.Parse(hfNiveli.Value);
            try
            {
                DbCore.DbInventari.clsKodifikimArtikulli kodifikim = new DbCore.DbInventari.clsKodifikimArtikulli(0, DbCore.clsFunksione.ktheStringunPaHapesira(kodi, true), DbCore.clsFunksione.ktheStringunPaHapesira(pershkrimi, false), idprindi, kodprindi, niveli, idperdoruesi, idndermarje, llojkodifikimi, kodlloji, 0, 0, 0, 0, 0, 0, 0, 0, llojartikulli, new DbCore.DbAsete.colGrupNormaAmortizimi(), true, 0, "", hfArkiva);

                e.Cancel = true;
                gride.CancelEdit();
                DbCore.clsMesazh mesazh = kodifikim.ruaj(false, "", "", "", "");
                if (!mesazh.Status)
                {
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, MessagesResource.Messages["msgNdodhiGabimRed"]);
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgBlerjeShitjeRuajtjeMeGabime"], pnlMesazhi);
                }
                else
                {
                    if (kodifikim.LlojKodifikimi == 1)
                        hfShtuarGrup1.Value = "true";
                    else if (kodifikim.LlojKodifikimi == 2)
                        hfShtuarGrup2.Value = "true";
                    else if (kodifikim.LlojKodifikimi == 3)
                        hfShtuarGrup3.Value = "true";
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, MessagesResource.Messages["msgRuajtjeMeSuksesGreen"]);
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["msgBlerjeShitjeRuajtjeMeSukses"], pnlMesazhi);
                }
                mbushGriden(gride, llojkodifikimi);
                
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, ex.Message + ":Red");
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, ex.Message, pnlMesazhi);
                return;
            }
        }


        protected void rowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            ASPxGridView gride = sender as ASPxGridView;
            String id = e.Keys["IdKodifikimi"].ToString();
            bool llojartikulli;
            if (Request.QueryString["llojiart"] == "aqt")
                llojartikulli = true;
            else llojartikulli = false;
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
            int idNdermarrje = IdNdermarrja;
            int idPerdorues = IdPerdoruesi;
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdorues, idNdermarrje, IdViti, DbCore.clsFunksione.GetKomponente(Page.Request));
            if (!tedrejtaInfo.DMod)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, MessagesResource.Messages["msgNukKeniTeDrejtaRed"]);
                return;
            }
            DbCore.clsMesazh m = new DbCore.clsMesazh();

            DbCore.DbInventari.clsKodifikimArtikulli kodOld = new DbCore.DbInventari.clsKodifikimArtikulli(int.Parse(id));
            string kodi = DbCore.clsFunksione.ktheStringunPaHapesira(e.NewValues["KodKodifikimi"].ToString(), true);
            string pershkrimi = DbCore.clsFunksione.ktheStringunPaHapesira(e.NewValues["PershkrimKodifikimi"].ToString(), false);
            int llojkodifikimi;
            string kodlloj;
            if (gride.ClientInstanceName == "gvKodifikimArtikulli") { llojkodifikimi = 1; kodlloj = "Grupimi 1"; }
            else if (gride.ClientInstanceName == "gvKodifikimArtikulliGr2") { llojkodifikimi = 2; kodlloj = "Grupimi 2"; }
            else { llojkodifikimi = 3; kodlloj = "Grupimi 3"; }
            int niveli = int.Parse(e.NewValues["NivelKodifikimi"].ToString());
            DbCore.DbInventari.clsKodifikimArtikulli prindiRi = new DbCore.DbInventari.clsKodifikimArtikulli(hfPrindi.Value, idNdermarrje, llojkodifikimi, llojartikulli);
            int prindi = prindiRi.IdKodifikimi; //marrim id e prindit
            int nivelPrindiRi = prindiRi.NivelKodifikimi;
            vlerat.Add(kodi);
            vlerat.Add(pershkrimi);
            vlerat.Add(niveli);
            vlerat.Add(llojkodifikimi);
            vlerat.Add(prindi);
            e.Cancel = true;

            bool joCikel = kontrolloPrind(nivelPrindiRi, int.Parse(id), prindi); //true kur nuk formohen cikle, false kur formohen dhe nuk mund t'i caktohet prindi.
            if (!joCikel)
            {
                e.Cancel = true;
                gride.CancelEdit();
                m.Status = false;
                m.PershkrimMesazhi = MessagesResource.Messages["msgPrindiNukEshteVlefshem"];
            }
            else
            {
                if (kodOld.NivelKodifikimi != niveli)
                {
                    if (DbCore.DbInventari.clsKodifikimArtikulli.eshtePrind(int.Parse(id)))
                        ndryshoNivelBijte(niveli, int.Parse(id)); //i kalojme si parametra nivelin e ri qe do kete dhe id e elementit qe do spostojme
                }
                try
                {
                    DbCore.DbInventari.clsKodifikimArtikulli kodifikim = new DbCore.DbInventari.clsKodifikimArtikulli(int.Parse(id), DbCore.clsFunksione.ktheStringunPaHapesira(kodi, true), DbCore.clsFunksione.ktheStringunPaHapesira(pershkrimi, false), prindi, prindiRi.KodKodifikimi, niveli, idPerdorues, idNdermarrje, llojkodifikimi, kodlloj, 0, 0, 0, 0, 0, 0, 0, 0, llojartikulli, new DbCore.DbAsete.colGrupNormaAmortizimi(), false, 0, "", hfArkiva);
                    m = kodifikim.modifiko();
                }
                catch (Exception ex)
                {
                    NLog.LogManager.GetCurrentClassLogger().Error(ex.Message);
                    m.PershkrimMesazhi = ex.Message;
                }
            }

            if (m.Status)
            {
                if (llojkodifikimi == 1)
                    hfShtuarGrup1.Value = "true";
                else if (llojkodifikimi == 2)
                    hfShtuarGrup2.Value = "true";
                else
                    hfShtuarGrup3.Value = "true";
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, m.PershkrimMesazhi + ":Green");
            }
            else
            {
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, m.PershkrimMesazhi + ":Red");
                return;
            }
            gride.CancelEdit();
            mbushGriden(gride, llojkodifikimi);
            hfNiveli.Value = "";
            hfPrindi.Value = "";
        }

        protected bool kontrolloPrind(int niveli, int idKodifikimi, int idPrindi)
        {
            if (niveli > 1)
            {
                if (idKodifikimi != idPrindi)
                {
                    niveli--;
                    idPrindi = new DbCore.DbInventari.clsKodifikimArtikulli(idPrindi).IdPrindi;
                    return kontrolloPrind(niveli, idKodifikimi, idPrindi);//si idKodifikimi do i kalohet idPrindi fillestar; si idPrindi do i kalohet idPrindi e kodifikimit me idPrindi fillestar
                }
                else return false;
            }
            else if (idKodifikimi == idPrindi) return false;
            else return true;
        }

        protected void ndryshoNivelBijte(int niveli, int idKodifikimi)
        {
            DbCore.DbInventari.colKodifikimeArtikulli col = new DbCore.DbInventari.colKodifikimeArtikulli();
            col.ktheBijte(idKodifikimi, IdNdermarrja);
            for (int i = 0; i < col.Count; i++)
            {
                idKodifikimi = col[i].IdKodifikimi;
                DbCore.DbInventari.clsKodifikimArtikulli kodif = new DbCore.DbInventari.clsKodifikimArtikulli(idKodifikimi);
                kodif.NivelKodifikimi = niveli + 1;
                kodif.modifiko();
                ndryshoNivelBijte(kodif.NivelKodifikimi, idKodifikimi);
            }
        }


        protected void rowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {//validimi ne jane plotesuar gjithe fushat e detyruara
            ASPxGridView grida = sender as ASPxGridView;
            foreach (GridViewColumn column in grida.Columns)
            {
                if (column.Visible == true)
                {
                    GridViewDataColumn dataColumn = column as GridViewDataColumn;
                    if (dataColumn == null) continue;

                    if (e.NewValues[dataColumn.FieldName] == null && dataColumn.FieldName != "IdPrindi" && dataColumn.FieldName != "NivelKodifikimi")//validimi per kolonat e detyrueshme
                    {
                        e.Errors[dataColumn] = MessagesResource.Messages["msgStrukturaAdministrativeVlereJoNull"];
                    }
                }
            }
            bool llojartikulli;
            if (Request.QueryString["llojiart"] == "aqt")
                llojartikulli = true;
            else llojartikulli = false;
            DbCore.DbInventari.clsKodifikimArtikulli kodifikimRi = new DbCore.DbInventari.clsKodifikimArtikulli();
            int llojkod;
            if (grida.ClientInstanceName == "gvKodifikimArtikulli") llojkod = 1;
            else if (grida.ClientInstanceName == "gvKodifikimArtikulliGr2") llojkod = 2;
            else llojkod = 3;
            if (e.NewValues["KodKodifikimi"] != null)
                kodifikimRi.KodKodifikimi = e.NewValues["KodKodifikimi"].ToString();
            if (hfPrindi.Value != "")
            {
                DbCore.DbInventari.clsKodifikimArtikulli prindiRi = new DbCore.DbInventari.clsKodifikimArtikulli(hfPrindi.Value, IdNdermarrja, llojkod, llojartikulli);
                kodifikimRi.IdPrindi = prindiRi.IdKodifikimi;
                kodifikimRi.LlojKodifikimi = prindiRi.LlojKodifikimi;
                DbCore.DbInventari.clsDatabaseInventari dbInventari = new DbCore.DbInventari.clsDatabaseInventari();
                if (dbInventari.kaVeprimeKodifikimArtikulliPaStandart(kodifikimRi.IdPrindi))
                {
                    e.RowError = MessagesResource.Messages["msgGrupiEshtePerdorurNeVeprime"];
                }
                dbInventari.Dispose();
                if (llojkod != kodifikimRi.LlojKodifikimi)
                {
                    e.RowError = MessagesResource.Messages["msgPrindiBenPjeseGrupiminTjeter"];
                }
            }

            if (e.Keys["IdKodifikimi"] == null)
                if (DbCore.DbInventari.clsKodifikimArtikulli.ekzistonSipasKodLloj(kodifikimRi.KodKodifikimi, IdNdermarrja, llojkod))
                {
                    e.RowError = MessagesResource.Messages["msgEkzistonGrupiMeKodZgjidhniTjeter"];
                }

            if (e.Errors.Count > 0)
            {
                e.RowError = MessagesResource.Messages["msgStrukturaAdministrativePlotesoFushat"];
                percaktoTamplatePrindi(grida);
            }

            if (string.IsNullOrEmpty(e.RowError) && e.Errors.Count > 0)
            {
                e.RowError = MessagesResource.Messages["msgStrukturaAdministrativeKorrigjoGabimet"];
                percaktoTamplatePrindi(grida);
            }
        }

        /// <summary>
        /// fshin filtrin e zgjedhur dhe aplikuar mbi gride, fshirje nga DB ku ai eshte ruajtur, dhe jo pastrimi i grides nga aplikimi i filtrit
        /// per kete do sherbeje Clear Filter ne fund te grides
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje
            int index = ASPxPageControl1.ActiveTabIndex;
            ASPxGridView grida;
            if (index == 0) grida = gvKodifikimArtikulli;
            else if (index == 1) grida = gvKodifikimArtikulliGr2;
            else grida = gvKodifikimArtikulliGr3;
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            int idNdermarrje = IdNdermarrja;
            DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(IdGjuha, "gvKodifikimArtikulli", DbCore.clsFunksione.GetKomponente(Page.Request), idNdermarrje);
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                filtra.IdPerdoruesi = IdPerdoruesi;
                mesazh = filtra.fshi();
                clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, idNdermarrje, "gvKodifikimArtikulli", 1, DbCore.clsFunksione.GetKomponente(Page.Request));
                percaktoTemplateMenu(ASPxMenu1, IdViti, IdPerdoruesi, idNdermarrje, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));

                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                grida.FilterExpression = String.Empty;
            }
        }
        /// <summary>
        /// sherben per te ruajtur filtrin e zgjedhur
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>

        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje
            int index = ASPxPageControl1.ActiveTabIndex;
            ASPxGridView grida;
            if (index == 0) grida = gvKodifikimArtikulli;
            else if (index == 1) grida = gvKodifikimArtikulliGr2;
            else grida = gvKodifikimArtikulliGr3;

            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            int idNdermarrje = IdNdermarrja;
            DbCore.DbAdmin.clsFiltraGrida filtri = new DbCore.DbAdmin.clsFiltraGrida();
            filtri.FiltraKodi = cmbFiltra.Text;
            filtri.FiltraShenime = cmbFiltra.Text;
            filtri.FiltraUniversal = false;
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(IdGjuha, "gvKodifikimArtikulli", DbCore.clsFunksione.GetKomponente(Page.Request), idNdermarrje);
            filtri.GridaKokaId = koka.IdGridaKoka;

            filtri.FiltraVlera = grida.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("KodKodifikimi", grida);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = grida.GetSortedColumns();
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
            //    filtri.KoloneRenditje = "KodKodifikimi";
            //    filtri.DrejtimRenditje = true;
            //}

            filtri.IdPerdoruesi = IdPerdoruesi;
            filtri.IdNdermarje = idNdermarrje;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();

            filtri.IdStatusDok = 1;
            mesazh = filtri.ruaj();
            clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, idNdermarrje, "gvKodifikimArtikulli", 1, DbCore.clsFunksione.GetKomponente(Page.Request));
            percaktoTemplateMenu(ASPxMenu1, IdViti, IdPerdoruesi, idNdermarrje, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));

            if (mesazh.Status == true)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            cmbFiltra.Text = "";
        }

        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            int index = ASPxPageControl1.ActiveTabIndex;
            ASPxGridView grida;
            int llojKodifikimi = 0;
            if (index == 0) { grida = gvKodifikimArtikulli; llojKodifikimi = 1; }
            else if (index == 1) { grida = gvKodifikimArtikulliGr2; llojKodifikimi = 2; }
            else if (index == 2) { grida = gvKodifikimArtikulliGr3; llojKodifikimi = 3; }
            else
            {
                if (hfGrup.Value == "0")
                {
                    grida = gvKodifikimArtikulli; llojKodifikimi = 1;
                }
                else if (hfGrup.Value == "1")
                {
                    grida = gvKodifikimArtikulliGr2; llojKodifikimi = 2;
                }
                else
                {
                    grida = gvKodifikimArtikulliGr3; llojKodifikimi = 3;
                }
            }

            int a = grida.FocusedRowIndex;
            grida.Selection.SelectRow(a);
            List<object> rreshtat = grida.GetSelectedFieldValues("IdKodifikimi");
            DbCore.DbInventari.clsDatabaseInventari dbInventari = new DbCore.DbInventari.clsDatabaseInventari();
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            var mesazhInfoGabim = string.Empty;
            var mesazhInfoSukses = string.Empty;
            var TeFshire = new List<string>();
            var TePaFshire = new List<string>();
            foreach (object id in rreshtat)
            {
                DbCore.DbInventari.clsKodifikimArtikulli cls = new DbCore.DbInventari.clsKodifikimArtikulli(Convert.ToInt32(id));
                DbCore.DbInventari.colKodifikimeArtikulli prind = new DbCore.DbInventari.colKodifikimeArtikulli();
                prind.mbushKodifikimArtikulliSipasPrindit(cls.IdKodifikimi);
                if (dbInventari.kaVeprimeKodifikimArtikulliPaStandart(cls.IdKodifikimi) || prind.Count > 0 || (DbCore.mySessionObjects.merrEshteMemeSesioni(Session) && DbCore.DbInventari.clsKodifikimArtikulli.eshteTransferuarTekBij(cls.KodKodifikimi, cls.IdNdermarje, cls.LlojKodifikimi)))
                {
                    TePaFshire.Add(cls.KodKodifikimi);
                    hfStatusi.Value = "false";
                }
                else
                {
                    int idPerdoruesi = IdPerdoruesi;
                    cls.IdPerdoruesi = idPerdoruesi;
                    mesazh = cls.fshiKodifikimArtDheLidhjeStandartStatusMag();
                    if (!mesazh.Status)
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                    else
                        TeFshire.Add(cls.KodKodifikimi);
                    hfStatusi.Value = "true";
                }
                mbushGriden(grida, llojKodifikimi);
            }
            if (TePaFshire.Count == 1)
            {
                mesazhInfoGabim = String.Format("{0}{1}{2}", MessagesResource.Messages["msgGrupimiMeKod"], String.Join(";", TePaFshire), MessagesResource.Messages["msgGrupimiNukFshihetNjejes"]);
            }
            else
            {
                if (TePaFshire.Count > 1)
                {
                    mesazhInfoGabim = String.Format("{0}{1}{2}", MessagesResource.Messages["msgGrupimetMeKod"], String.Join(";", TePaFshire), MessagesResource.Messages["msgGrupimetNukFshihenShumes"]);
                }
            }
            if (TeFshire.Count == 1)
            {
                mesazhInfoSukses = String.Format("{0}{1}{2}", MessagesResource.Messages["msgGrupimiMeKod"], String.Join(";", TeFshire), MessagesResource.Messages["msgGrupimiFshirjeMeSukses"]);
            }
            else
            {
                if (TeFshire.Count > 1)
                {
                    mesazhInfoSukses = String.Format("{0}{1}{2}", MessagesResource.Messages["msgGrupimetMeKod"], String.Join(";", TeFshire), MessagesResource.Messages["msgGrupimiFshirjeMeSuksesShumeso"]);
                }
            }
            if (mesazhInfoGabim != string.Empty && mesazhInfoSukses != string.Empty)
            {
                mesazhInfoGabim += MessagesResource.Messages["msgLidhesMesazhi"] + mesazhInfoSukses;
            }
            if (mesazhInfoGabim != string.Empty)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhInfoGabim, pnlMesazhi);
            }
            else
            {
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazhInfoSukses, pnlMesazhi);
            }
            pnlMesazhi.Update();
            dbInventari.Dispose();
            if (Request.QueryString["llojiart"] != "aqt")
                pnlGrida.Update();
        }

        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {
            if (Request.QueryString["llojiart"] == "aqt")
            {
                if (e.Item.Name == "Ruaj")
                {
                    Page.Validate("entries");
                    ruajKodifikim();
                }
            }
        }

        private bool isValidKodifikim(int idNdermarrje)
        {
            bool isValid = true;

            if (Request.QueryString["llojiart"] == "aqt" && hfGrup.Value == "0" && (btneLlogInv.Text == "" || btneLlogBle.Text == "" || btneLlogShit.Text == "" || btneLlogTretet.Text == "" || this.btnLlogShpe.Text == "" || cmbLlogAmortizimi.Text == ""))
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgShtoArtikullPlotesoniSkemenOseLlogarite"], pnlMesazhi);
                isValid = false; hfStatusi.Value = "false";
                return isValid;
            }           

            if (btneLlogInv.Text != "")
            {
                if (new DbCore.DbKontabiliteti.clsLlogari(btneLlogInv.Text, idNdermarrje).IdLlogari == 0)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgShtoArtikullLlogariaEInventaritNukEkziston"], pnlMesazhi);

                    isValid = false; hfStatusi.Value = "false"; return isValid;
                }
            }
            if (btneLlogBle.Text != "")
            {
                if (new DbCore.DbKontabiliteti.clsLlogari(btneLlogBle.Text, idNdermarrje).IdLlogari == 0)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgLlogVlereKontabelNukEkziston"], pnlMesazhi);
                    isValid = false; hfStatusi.Value = "false"; return isValid;
                }
            }
            if (btneLlogShit.Text != "")
            {
                if (new DbCore.DbKontabiliteti.clsLlogari(btneLlogShit.Text, idNdermarrje).IdLlogari == 0)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgShtoArtikullLlogariaEShitjesNukEkziston"], pnlMesazhi);
                    isValid = false; hfStatusi.Value = "false"; return isValid;
                }
            }
            if (btnLlogPakesim.Text != "")
            {
                if (new DbCore.DbKontabiliteti.clsLlogari(btnLlogPakesim.Text, idNdermarrje).IdLlogari == 0)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgLlogPakesimNukEkziston"], pnlMesazhi);

                    isValid = false; hfStatusi.Value = "false"; return isValid;
                }
            }
            if (this.btneLlogTretet.Text != "")
            {
                if (new DbCore.DbKontabiliteti.clsLlogari(btneLlogTretet.Text, idNdermarrje).IdLlogari == 0)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgLlogAANukEkziston"], pnlMesazhi);
                    isValid = false; hfStatusi.Value = "false"; return isValid;
                }
            }
            else if (this.btnLlogShpe.Text != "")
            {
                if (new DbCore.DbKontabiliteti.clsLlogari(btnLlogShpe.Text, idNdermarrje).IdLlogari == 0)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgShtoArtikullLlogariaEShpenzimeveNukEkziston"], pnlMesazhi);
                    isValid = false; hfStatusi.Value = "false"; return isValid;
                }
            }
            if (cmbLlogAmortizimi.Text != "")
            {
                if (new DbCore.DbKontabiliteti.clsLlogari(cmbLlogAmortizimi.Text, idNdermarrje).IdLlogari == 0)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgShtoArtikullLlogariaEAmortizimeveNukEkziston"], pnlMesazhi);
                    isValid = false; hfStatusi.Value = "false"; return isValid;
                }
            }

            int llojkod = 1;
            if (hfGrup.Value == "0") llojkod = 1;
            else if (hfGrup.Value == "1") llojkod = 2;
            else llojkod = 3;
            bool llojartikulli;
            if (Request.QueryString["llojiart"] == "aqt")
                llojartikulli = true;
            else llojartikulli = false;
            if (btneEmertimPrindi.Text != "")
            {

                DbCore.DbInventari.clsKodifikimArtikulli prindiRi = new DbCore.DbInventari.clsKodifikimArtikulli(int.Parse(btneEmertimPrindi.Value.ToString()));
                DbCore.DbInventari.clsDatabaseInventari dbInventari = new DbCore.DbInventari.clsDatabaseInventari();
                if (dbInventari.kaVeprimeKodifikimArtikulliPaStandart(prindiRi.IdKodifikimi))
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgGrupiEshtePerdorurNeVeprime"], pnlMesazhi);
                    isValid = false; hfStatusi.Value = "false"; return isValid;
                }
                dbInventari.Dispose();
                if (llojkod != prindiRi.LlojKodifikimi)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgPrindiBenPjeseGrupiminTjeter"], pnlMesazhi);
                    isValid = false; hfStatusi.Value = "false"; return isValid;
                }
            }

            if (hfShtimModifikim.Value == "shtim")
            {
                if (DbCore.DbInventari.clsKodifikimArtikulli.ekzistonSipasKodLloj(txtKodi.Text, IdNdermarrja, llojkod))
                {

                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgGrupiEkziston"], pnlMesazhi);
                    isValid = false; hfStatusi.Value = "false"; return isValid;
                }
            }
            else
            {
                DbCore.DbInventari.clsKodifikimArtikulli prindiRi = new DbCore.DbInventari.clsKodifikimArtikulli(btneEmertimPrindi.Text, IdNdermarrja, llojkod, llojartikulli);
                bool joCikel = kontrolloPrind(prindiRi.NivelKodifikimi, int.Parse(hfId.Value), prindiRi.IdKodifikimi); //true kur nuk formohen cikle, false kur formohen dhe nuk mund t'i caktohet prindi.
                if (!joCikel)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgPrindiNukEshteVlefshem"], pnlMesazhi);
                    isValid = false; hfStatusi.Value = "false"; return isValid;
                }
                else
                {
                    DbCore.DbInventari.clsKodifikimArtikulli kodOld = new DbCore.DbInventari.clsKodifikimArtikulli(int.Parse(hfId.Value));
                    if (kodOld.NivelKodifikimi != int.Parse(txtNiveli.Text))
                    {
                        if (DbCore.DbInventari.clsKodifikimArtikulli.eshtePrind(int.Parse(hfId.Value)))
                            ndryshoNivelBijte(int.Parse(txtNiveli.Text), int.Parse(hfId.Value)); //i kalojme si parametra nivelin e ri qe do kete dhe id e elementit qe do spostojme
                    }
                }
            }

            return isValid;
        }

        private void ruajKodifikim()
        {
            if (Page.IsValid == false)
                return;
            else
            {
                int idPerdoruesi = IdPerdoruesi;
                int idNdermarrje = IdNdermarrja;
                if (isValidKodifikim(idNdermarrje))
                {
                    bool eshteShtim;
                    if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim")
                    {
                        eshteShtim = true;
                        hfArkiva.Set("kopjoArkiven", hfShtimModifikim.Value == "klonim");
                    }
                    else eshteShtim = false;
                    clsKodifikimArtikulli niveli = new clsKodifikimArtikulli();

                    try
                    {
                        niveli = krijoKodifikim(idNdermarrje, idPerdoruesi, eshteShtim);
                    }
                    catch (Exception ex)
                    {
                        NLog.LogManager.GetCurrentClassLogger().Error(ex.Message);
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, ex.Message, pnlMesazhi);
                        return;
                    }
                    DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                    DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

                    tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, DbCore.clsFunksione.GetKomponente(Page.Request));
                    if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim")
                    {
                        if (!tedrejtaInfo.DShtim)
                        {
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["mesazhRaportNukKeniTeDrejta"], pnlMesazhi);
                            hfStatusi.Value = "false";
                            return;
                        }
                        mesazh = niveli.ruaj(false, "", "", "", "");
                        eshteShtim = true;
                    }
                    else
                    {
                        if (!tedrejtaInfo.DMod)
                        {
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgAdministrimiNukKeniTeDrejteVeprimi"], pnlMesazhi);
                            hfStatusi.Value = "false";
                            return;
                        }
                        eshteShtim = false;                        
                        niveli.IdKodifikimi = int.Parse(hfId.Value.ToString());                    
                        mesazh = niveli.modifiko();
                    }
                    if (mesazh.Status == true)
                    {
                        clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["msgAdministrimiRuajtjaPerfundoiSukses"], pnlMesazhi);
                        hfStatusi.Value = "true";
                    }
                    else
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgBlerjeShitjeRuajtjeMeGabime"], pnlMesazhi);
                        hfStatusi.Value = "false";
                    }
                    ASPxPageControl1.ActiveTabIndex = 0;
                }
                else
                {
                    hfStatusi.Value = "false";
                }
            }
            ASPxGridView grida;
            int llojKodifikimi;
            if (hfGrup.Value == "0")
            {
                grida = gvKodifikimArtikulli; llojKodifikimi = 1;
            }
            else if (hfGrup.Value == "1")
            {
                grida = gvKodifikimArtikulliGr2; llojKodifikimi = 2;
            }
            else
            {
                grida = gvKodifikimArtikulliGr3; llojKodifikimi = 3;
            }
            mbushGriden(grida, llojKodifikimi);
        }

        private DbCore.DbInventari.clsKodifikimArtikulli krijoKodifikim(int idNdermarrje, int idPerdorues, bool shtim)
        {
            int prind = 0, idskema = 0, idlloginv = 0, idllogble = 0, idllogshit = 0, idllogshpe = 0, idllogtret = 0, idllogamor = 0, idllogpak = 0, idformati = 0;
            bool llojartikulli;
            if (Request.QueryString["llojiart"] == "aqt")
                llojartikulli = true;
            else llojartikulli = false;
            clsKodifikimArtikulli kod1 = new clsKodifikimArtikulli();
            kod1.PershkrimKodifikimi = "";
            if (!String.IsNullOrEmpty(this.btneEmertimPrindi.Text))
            {
                kod1 = new clsKodifikimArtikulli(int.Parse(btneEmertimPrindi.Value.ToString()));
                prind = kod1.IdKodifikimi;
            }

            if (btneSkema.Text != "") int.TryParse(btneSkema.Value.ToString(), out idskema);
            if (btneLlogInv.Text != "") idlloginv = DbCore.DbKontabiliteti.clsLlogari.mbushIDLlogariSipasKodit(btneLlogInv.Text, idNdermarrje);
            if (btneLlogBle.Text != "") idllogble = DbCore.DbKontabiliteti.clsLlogari.mbushIDLlogariSipasKodit(btneLlogBle.Text, idNdermarrje);
            if (btneLlogShit.Text != "") idllogshit = DbCore.DbKontabiliteti.clsLlogari.mbushIDLlogariSipasKodit(btneLlogShit.Text, idNdermarrje);
            if (btneLlogTretet.Text != "") idllogtret = DbCore.DbKontabiliteti.clsLlogari.mbushIDLlogariSipasKodit(btneLlogTretet.Text, idNdermarrje);

            if (btnLlogShpe.Text != "") idllogshpe = DbCore.DbKontabiliteti.clsLlogari.mbushIDLlogariSipasKodit(btnLlogShpe.Text, idNdermarrje);
            if (cmbLlogAmortizimi.Text != "") idllogamor = DbCore.DbKontabiliteti.clsLlogari.mbushIDLlogariSipasKodit(cmbLlogAmortizimi.Text, idNdermarrje);
            if (btnLlogPakesim.Text != "") idllogpak = DbCore.DbKontabiliteti.clsLlogari.mbushIDLlogariSipasKodit(btnLlogPakesim.Text, idNdermarrje);

            if (cmbFormati.Text != "")
                idformati = int.Parse(cmbFormati.Value.ToString());

            clsKodifikimArtikulli kodifikim = new clsKodifikimArtikulli(0, DbCore.clsFunksione.ktheStringunPaHapesira(txtKodi.Text, true), DbCore.clsFunksione.ktheStringunPaHapesira(txtEmertimi.Text, false), prind, kod1.KodKodifikimi == null ? "" : kod1.KodKodifikimi, int.Parse(txtNiveli.Text), idPerdorues, idNdermarrje, int.Parse(hfGrup.Value) + 1, hfGrup.Value == "0" ? "Grupimi 1" : (hfGrup.Value == "1" ? "Grupimi 2" : "Grupimi 3"), idskema, idllogble, idllogshit, idlloginv, idllogshpe, idllogtret, idllogamor, idformati, llojartikulli, ruajTrupin(), shtim, idllogpak, btnLlogPakesim.Text, hfArkiva);
            return kodifikim;
        }

        protected void gridat_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if (e.Column.FieldName == "IdPrindi" || e.Column.FieldName == "IdSkemaKontabel")
            {
                if (e.Value == null || string.IsNullOrWhiteSpace(e.Value.ToString()) || double.Parse(e.Value.ToString()) == 0)
                    e.Criteria = null;
            }
        }

        protected void gridat_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            //konfiguroVleraFillestare();
            //int index = ASPxPageControl1.ActiveTabIndex;
            //ASPxGridView grida;
            //if (index == 0) grida = gvKodifikimArtikulli;
            //else if (index == 1) grida = gvKodifikimArtikulliGr2;
            //else grida = gvKodifikimArtikulliGr3;
            ASPxGridView grida = sender as ASPxGridView;
            konfiguroGride(grida, (bool)hfState.Get("lupe"));
            percaktoTamplatePrindi(grida);
        }

        protected void initNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            ASPxGridView grida = sender as ASPxGridView;
            percaktoTamplatePrindi(grida);
        }

        protected void gridat_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            //int index = ASPxPageControl1.ActiveTabIndex;
            //ASPxGridView grida;
            ASPxGridView grida = sender as ASPxGridView;
            //if (index == 0) grida = gvKodifikimArtikulli;
            //else if (index == 1) grida = gvKodifikimArtikulliGr2;
            //else grida = gvKodifikimArtikulliGr3;
            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                if (arr[2] == "")
                    grida.FilterExpression = "";
                else
                {
                    //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    //filtra.mbushFiltraGridaSipasFiltraKodi(arr[2], IdNdermarrja);
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(IdGjuha, "gvKodifikimArtikulli", DbCore.clsFunksione.GetKomponente(Page.Request), IdNdermarrja);
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], IdNdermarrja, koka.IdGridaKoka);
                    //DbCore.DbAdmin.clsFiltraGrida filtra = dbAdmin.merrFiltraGridaSipasFiltraKodi(arr[2], IdNdermarrja);
                    if (filtra.FiltraKodi != null)
                    {
                        grida.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, grida);

                        konfiguroVleraFillestare();

                    }
                }
            }
            konfiguroGride(grida, (bool)hfState.Get("lupe"));
        }

        protected void gridat_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            ASPxGridView grida = sender as ASPxGridView;
            e.Properties["cpPageIndex"] = grida.PageIndex;
            e.Properties["cpPageRow"] = grida.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = grida.VisibleRowCount;
        }

        protected void btneEmertimPrindi_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].ToString().Contains("btneEmertimPrindi"))
            {
                bool llojartikulli;
                if (Request.QueryString["llojiart"] == "aqt")
                    llojartikulli = true;
                else llojartikulli = false;
                ConfigureAspxComboBox.mbushComboKodifikimGjitha(IdNdermarrja, btneEmertimPrindi, int.Parse(hfGrup.Value) + 1, llojartikulli);
            }
        }

        protected void btneEmertimPrindi_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].ToString().Contains("btneEmertimPrindi"))
            {
                bool llojartikulli;
                if (Request.QueryString["llojiart"] == "aqt")
                    llojartikulli = true;
                else llojartikulli = false;
                ConfigureAspxComboBox.mbushComboKodifikimGjitha(IdNdermarrja, btneEmertimPrindi, int.Parse(hfGrup.Value) + 1, llojartikulli, e.Filter);
            }
        }


        protected void btneSkema_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("btneSkema"))
                {
                    hfSkemaKlasa.Value = ConfigureAspxComboBox.mbushComboSkemaKontabilitetiArtikulli(IdNdermarrja, btneSkema, Request.QueryString["llojiart"], e.Value);
                }
            }
        }

        protected void btneSkema_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("btneSkema"))
                {
                    hfSkemaKlasa.Value = ConfigureAspxComboBox.mbushComboSkemaKontabilitetiArtikulli(IdNdermarrja, btneSkema, Request.QueryString["llojiart"], e.Filter);
                }
            }
        }

        protected void btneLlogInv_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("btneLlogInv"))
                {
                    ConfigureAspxComboBox.mbushComboLlogariaPaKolona(IdPerdoruesi, IdNdermarrja, btneLlogInv, e);
                }
            }
        }

        protected void btneLlogInv_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("btneLlogInv"))
                {
                    ConfigureAspxComboBox.mbushComboLlogariaPaKolona(IdPerdoruesi, IdNdermarrja, btneLlogInv, e);
                }
            }
        }

        protected void btneLlogBle_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("btneLlogBle"))
                {
                    ConfigureAspxComboBox.mbushComboLlogariaPaKolona(IdPerdoruesi, IdNdermarrja, btneLlogBle, e);
                }
            }
        }

        protected void btneLlogBle_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("btneLlogBle"))
                {
                    ConfigureAspxComboBox.mbushComboLlogariaPaKolona(IdPerdoruesi, IdNdermarrja, btneLlogBle, e);
                }
            }
        }

        protected void btneLlogShit_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("btneLlogShit"))
                {
                    ConfigureAspxComboBox.mbushComboLlogariaPaKolona(IdPerdoruesi, IdNdermarrja, btneLlogShit, e);
                }
            }
        }

        protected void btneLlogShit_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("btneLlogShit"))
                {
                    ConfigureAspxComboBox.mbushComboLlogariaPaKolona(IdPerdoruesi, IdNdermarrja, btneLlogShit, e);
                }
            }
        }

        protected void btnLlogPakesim_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("btnLlogPakesim"))
                {
                    ConfigureAspxComboBox.mbushComboLlogariaPaKolona(IdPerdoruesi, IdNdermarrja, btnLlogPakesim, e);
                }
            }
        }

        protected void btnLlogPakesim_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("btnLlogPakesim"))
                {
                    ConfigureAspxComboBox.mbushComboLlogariaPaKolona(IdPerdoruesi, IdNdermarrja, btnLlogPakesim, e);
                }
            }
        }

        protected void btneLlogTretet_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("btneLlogTretet"))
                {
                    ConfigureAspxComboBox.mbushComboLlogariaPaKolona(IdPerdoruesi, IdNdermarrja, btneLlogTretet, e);
                }
            }
        }

        protected void btneLlogTretet_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("btneLlogTretet"))
                {
                    ConfigureAspxComboBox.mbushComboLlogariaPaKolona(IdPerdoruesi, IdNdermarrja, btneLlogTretet, e);
                }
            }
        }

        protected void btnLlogShpe_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("btnLlogShpe"))
                {
                    ConfigureAspxComboBox.mbushComboLlogariaPaKolona(IdPerdoruesi, IdNdermarrja, btnLlogShpe, e);
                }
            }
        }

        protected void btnLlogShpe_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("btnLlogShpe"))
                {
                    ConfigureAspxComboBox.mbushComboLlogariaPaKolona(IdPerdoruesi, IdNdermarrja, btnLlogShpe, e);
                }
            }
        }

        protected void cmbLlogAmortizimi_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("cmbLlogAmortizimi"))
                {
                    ConfigureAspxComboBox.mbushComboLlogariaPaKolona(IdPerdoruesi, IdNdermarrja, cmbLlogAmortizimi, e);
                }
            }
        }

        protected void cmbLlogAmortizimi_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("cmbLlogAmortizimi"))
                {
                    ConfigureAspxComboBox.mbushComboLlogariaPaKolona(IdPerdoruesi, IdNdermarrja, cmbLlogAmortizimi, e);
                }
            }
        }


        #region grida e amortizimit
        private void konfiguroVleraFillestareNorma()
        {
            DbCore.DbAsete.colGrupNormaAmortizimi col = new DbCore.DbAsete.colGrupNormaAmortizimi();
            int id = 0;
            if (hfShtimModifikim.Value == "modifikim")
            {
                id = int.Parse(hfId.Value.ToString());
                col.merrGrupNormaAmortizimiTeGjitha(id, IdNdermarrja);
            }
            else col.merrGrupNormaAmortizimiFillestare(IdNdermarrja);
            gvAmortizimi.DataSource = col;
            gvAmortizimi.DataBind();
        }

        private void konfiguroGrideNorma()
        {
            shtoStandart(IdNdermarrja);
            GridUtil.percaktoVisibleColumnsMeWidth(IdGjuha, IdNdermarrja, gvAmortizimi, "gvAmortizimi", "KodifikimArtikulli.aspx");           
            GridUtil.konfiguroGrideListeEvogelPaTheme(gvAmortizimi, "IdLidhjeGrupLlojAmort");
            gvAmortizimi.Settings.ShowFilterRow = false;
            gvAmortizimi.SettingsBehavior.AllowSort = false;
            gvAmortizimi.SettingsBehavior.AllowGroup = false;
            gvAmortizimi.Settings.ShowFilterRowMenu = false;
            gvAmortizimi.Settings.ShowHeaderFilterButton = false;
            gvAmortizimi.SettingsEditing.Mode = GridViewEditingMode.Inline;
            gvAmortizimi.Settings.ShowFilterBar = GridViewStatusBarMode.Hidden;
            gvAmortizimi.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
            percaktoTemplate();
        }

        private void percaktoTemplate()
        {
            GridViewDataColumn col7 = gvAmortizimi.Columns["IdLlojAmortizimi"] as GridViewDataColumn;
            col7.DataItemTemplate = new MyComboTemplate();
            GridViewDataColumn col17 = gvAmortizimi.Columns["NormeMagazine"] as GridViewDataColumn;
            col17.DataItemTemplate = new MyComboTemplate();
            GridViewDataColumn col10 = gvAmortizimi.Columns["Norme"] as GridViewDataColumn;
            col10.DataItemTemplate = new MyDoubleTemplate(false, 2, "0");
            GridViewDataColumn col1 = gvAmortizimi.Columns["IdStandartAmortizimi"] as GridViewDataColumn;
            col1.DataItemTemplate = new MyLabelTemplate();
        }

        private void shtoStandart(int idNdermarrje)
        {
            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            if (typeof(GridViewDataComboBoxColumn) != gvAmortizimi.Columns["IdStandartAmortizimi"].GetType())
            {
                gvAmortizimi.Columns.Remove(gvAmortizimi.Columns["IdStandartAmortizimi"]);
                gvAmortizimi.Columns.Add(colnew);
                DbCore.DbAsete.colStandarteAmortizimi col = new DbCore.DbAsete.colStandarteAmortizimi(idNdermarrje);
                col.Insert(0, new DbCore.DbAsete.clsStandarteAmortizim());
                colnew.PropertiesComboBox.DataSource = col;
                colnew.PropertiesComboBox.TextField = "Emertimi";
                colnew.PropertiesComboBox.ValueField = "IdStandarti";
                colnew.FieldName = "IdStandartAmortizimi";
                colnew.Visible = true;
                colnew.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                DbCore.mySessionObjects.ruajDsComboGrideNeSession(Session, col, "colStandarte");
            }
            else
            {
                colnew = (GridViewDataComboBoxColumn)gvAmortizimi.Columns["IdStandartAmortizimi"];
                if (colnew.PropertiesComboBox.Items.Count == 0)
                {
                    colnew.PropertiesComboBox.DataSource = DbCore.mySessionObjects.merrDsComboGrideNeSession(Session, "colStandarte");
                }
            }
        }

        private DbCore.DbAsete.colGrupNormaAmortizimi ruajTrupin()
        {
            JavaScriptSerializer serializusi = new JavaScriptSerializer();
            object[] dokumenti = (object[])serializusi.DeserializeObject(gridDataObject.Value);
            DbCore.DbAsete.colGrupNormaAmortizimi trupat = new DbCore.DbAsete.colGrupNormaAmortizimi();
            int idNdermarrje = IdNdermarrja;
            for (int i = 0; i < dokumenti.Length; i++)
            {
                DbCore.DbAsete.clsGrupNormaAmortizimi trup = new DbCore.DbAsete.clsGrupNormaAmortizimi(idNdermarrje, IdPerdoruesi, (Dictionary<string, object>)dokumenti[i]);
                trupat.Add(trup);
            }
            return trupat;
        }


        protected void gvAmortizimi_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters != "")
            {
                DbCore.DbAsete.colGrupNormaAmortizimi col = new DbCore.DbAsete.colGrupNormaAmortizimi();
                int id = 0;

                id = int.Parse(e.Parameters);
                col.merrGrupNormaAmortizimiTeGjitha(id, IdNdermarrja);

                gvAmortizimi.DataSource = col;
                gvAmortizimi.DataBind();
            }
            else konfiguroVleraFillestareNorma();
            konfiguroGrideNorma();
        }

        protected void gvAmortizimi_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gvAmortizimi.PageIndex;
            e.Properties["cpPageRow"] = gvAmortizimi.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gvAmortizimi.VisibleRowCount;
        }

        protected void gvAmortizimi_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {

            if (e.RowType == DevExpress.Web.GridViewRowType.Data)
            {
                GridViewDataColumn col0 = ((ASPxGridView)sender).Columns["IdLlojAmortizimi"] as GridViewDataColumn;
                ASPxComboBox btn0 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col0, "cmbBox") as ASPxComboBox;
                GridViewDataColumn col1 = ((ASPxGridView)sender).Columns["NormeMagazine"] as GridViewDataColumn;
                ASPxComboBox btn1 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col1, "cmbBox") as ASPxComboBox;
                GridViewDataColumn col10 = gvAmortizimi.Columns["Norme"] as GridViewDataColumn;
                ASPxTextBox btn2 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col10, "txtBox") as ASPxTextBox;
                GridViewDataColumn col11 = gvAmortizimi.Columns["IdStandartAmortizimi"] as GridViewDataColumn;
                ASPxLabel btn3 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col11, "lbl") as ASPxLabel;
                if (btn3 != null)
                {
                    btn3.ClientInstanceName = "lblStandart" + e.VisibleIndex.ToString();
                }
                if (btn0 != null)
                {
                    btn0.ClientInstanceName = "cmbLlojAmortizimi" + e.VisibleIndex.ToString();
                    DbCore.DbAsete.colAseteLlojAmortizimi col = new DbCore.DbAsete.colAseteLlojAmortizimi();
                    col.merrTeGjithaLlojAmortizimesh();
                    btn0.DataSource = col;
                    btn0.TextField = "LlojAmortizimi";
                    btn0.ValueField = "IdLlojAmortizimi";
                    if (btn0.Text == "")
                        btn0.SelectedIndex = 0;
                    btn0.DataBind();

                }
                if (btn1 != null)
                {
                    btn1.ClientInstanceName = "cmbNormeMagazine" + e.VisibleIndex.ToString();
                    btn1.Items.Add("Artikull", "Unchecked");
                    btn1.Items.Add("Magazine", "Checked");
                    btn1.DataBind();
                }
                if (btn2 != null)
                {
                    btn2.ClientInstanceName = "txtNorme" + e.VisibleIndex.ToString();
                    btn2.ClientSideEvents.TextChanged = "function (s,e){if(isNaN(parseFloat(s.GetText()))) {myMesazh.ShtoMesazhGabimi('Norma duhet te jete numer!'); s.SetText(0);} if(parseFloat(s.GetText())<0 ||parseFloat(s.GetText())>100){myMesazh.ShtoMesazhGabimi('Norma duhet te jete midis 0 dhe 100!'); s.SetText(0);} }";
                }
            }
        }
        #endregion

        private void mbushHiddenFieldMePerkthime()
        {
            hfState.Set("regjisDokZgjidhDokPerTeBashkengjitur", MessagesResource.Messages["regjisDokZgjidhDokPerTeBashkengjitur"]);
            hfState.Set("msgKodifikimArtikullZgjidhGrup", MessagesResource.Messages["msgKodifikimArtikullZgjidhGrup"]);
            hfState.Set("headerPopUpZgjidhKodifikiminArtikullit", MessagesResource.Messages["headerPopUpZgjidhKodifikiminArtikullit"]);
            hfState.Set("msgNukKeniDrejtaPerVeprim", MessagesResource.Messages["msgNukKeniDrejtaPerVeprim"]);
            hfState.Set("headerPopUpTextZgjidhNdermarrjet", MessagesResource.Messages["headerPopUpTextZgjidhNdermarrjet"]);
            hfState.Set("msgRuajGrupinDheCaktoArkive", MessagesResource.Messages["msgRuajGrupinDheCaktoArkive"]);
            hfState.Set("popupAdministrimiUniversal", MessagesResource.Messages["popupAdministrimiUniversal"]);
            hfState.Set("msgZgjidhniNivelCmimi", MessagesResource.Messages["msgZgjidhniNivelCmimi"]);
            lblKodi.Text = MessagesResource.Messages["labelBlerjeShitjeKodi"];
            lblEmertimi.Text = MessagesResource.Messages["labelBlerjeShitjePershkrimi"];
            lblFormati.Text = MessagesResource.Messages["lblFormatiISerialit"];
            lblPrindi.Text = MessagesResource.Messages["lblPrindi"];
            lblNiveli.Text = MessagesResource.Messages["lblNiveli"];
            lblSkema.Text = MessagesResource.Messages["lblSkema"];
            lblLlogBle.Text = MessagesResource.Messages["lblLlogariVlereKontabel"];
            lblLlogShit.Text = MessagesResource.Messages["lblLlogariShitje"];
            lblLlogInv.Text = MessagesResource.Messages["lblLlogariInventar"];
            lblLlogShpe.Text = MessagesResource.Messages["lblLlogariShpenzimAmortizimi"];
            lblLlogTretet.Text = MessagesResource.Messages["lblLlogariAANeProces"];
            ASPxLabel1.Text = MessagesResource.Messages["lblLlogariAmortizimi"];
            lblLlogPakesim.Text = MessagesResource.Messages["lblLlogariPakesimVlereDalje"];
            hfState.Set("labelGrupimKlientPare", MessagesResource.Messages["labelGrupimKlientPare"]);
            hfState.Set("labelGrupimKlientDyte", MessagesResource.Messages["labelGrupimKlientDyte"]);
            hfState.Set("labelGrupimKlientTrete", MessagesResource.Messages["labelGrupimKlientTrete"]);
          
            hfState.Set("headerZgjidhSkemenkont", MessagesResource.Messages["headerZgjidhSkemenkont"]);
        }
    }
}