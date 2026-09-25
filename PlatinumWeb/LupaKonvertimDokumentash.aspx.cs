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
using PlatinumWeb.Templates;
using System.Data.SqlClient;
using DbCore.DbShare;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class LupaKonvertimDokumentash : MyPageBase
    {
        private String veprimi;

        protected void Page_Load(object sender, EventArgs e)
        {
            veprimi = Request.QueryString["veprimi"];
            string vleraQueryString = "";
            if (Request.QueryString["idKonfigAmbjente"] != null && Request.QueryString["idKonfigAmbjente"] != "")
                vleraQueryString = Request.QueryString["idKonfigAmbjente"].ToString();
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            //nivel.Kodi = "KonvDok"; //eshte kodi i lupes, mund te shihet ne DB ne T_NIVELREGJISTRIMI
            int idPerdorues = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            int idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            percaktoTemplateMenu(ASPxMenu1, idViti, idPerdorues, idNdermarrje);
            int idKonfigambjenti = DbCore.clsFunksione.getIdKonfigAmbLupa(vleraQueryString, idNdermarrje, "KonvDok");
            hfState.Set("idKonfigAmbjente", idKonfigambjenti);
            bool kerkosaposhkruar = true;
            int idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            if (clsAlternativaKushti.getAlternativa(idKonfigambjenti, "KSSH") == "Po")
                kerkosaposhkruar = true;
            else kerkosaposhkruar = false;
            bool endlessScroll = false;
            if (clsAlternativaKushti.getAlternativa(idKonfigambjenti, "ES") == "Po")
                endlessScroll = true;

            if (!IsPostBack)
            {
                cmbKonfigurimi.Value = idKonfigambjenti.ToString();
                DbCore.mySessionObjects.ruajPeriudheNeSesion(Session, 1);
                dteNga.Date = DateTime.Today;
                AspxWebControlUtils.vendosDateEditMask(dteNga);
                dteDeri.Date = DateTime.Today;
                AspxWebControlUtils.vendosDateEditMask(dteDeri);
                rbAktuale.Checked = true;
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaDokumenta = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaDokumenta.merrTeDrejtaPerKeteKomponente(idPerdorues, idNdermarrje, idViti, "LupaKonvertimDokumentash.aspx");
                hfTeDrejtaGjitheDok.Value = tedrejtaDokumenta.DGjitheDok.ToString();
                GridUtil.AplikoFilterDefault(gvLupaKonvDok, idKonfigambjenti);
                if (hfTeDrejtaGjitheDok.Value.ToString() == "True" || hfTeDrejtaGjitheDok.Value.ToString() == "true")
                    mbushPopUpListe(true, idKonfigambjenti);
                else
                    mbushPopUpListe(false, idKonfigambjenti);
                clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idNdermarrje, "gvLupaKonvDok", idKonfigambjenti, "LupaKonvertimDokumentash.aspx");
            }
            else mbushPopUpListeDokumentashNgaSession(idKonfigambjenti);
            konfiguroPopupGride(idGjuha, idNdermarrje, idPerdorues, kerkosaposhkruar, endlessScroll);

            Container.Attributes["width"] = "400px";
            Container.Attributes["height"] = "450px";
            Container.Attributes["src"] = "LupaFiltra.aspx?grida=gvLupaKonvDok&page=LupaDokumenta.aspx";
        }
        private void mbushPopUpListeDokumentashNgaSession(int idKonfigambjenti)
        {
            Object tmpObject;
            DbCore.mySessionObjects.merrGrideNgaSessioniLupa(Session, out tmpObject);
            if (tmpObject == null)
            {
                if (hfTeDrejtaGjitheDok.Value.ToString() == "True" || hfTeDrejtaGjitheDok.Value.ToString() == "true")
                    mbushPopUpListe(true, idKonfigambjenti);
                else
                    mbushPopUpListe(false, idKonfigambjenti);
                return;
            }
            gvLupaKonvDok.DataSource = tmpObject;
            gvLupaKonvDok.DataBind();
        }
        private void mbushPopUpListe(bool gjithedok, int idKonfigambjenti)
        {

            if (rbAktuale.Checked)
                DbCore.mySessionObjects.ruajPeriudheNeSesion(Session, 1);
            else if (rbVitiUshtrimor.Checked)
                DbCore.mySessionObjects.ruajPeriudheNeSesion(Session, 2);
            else
                DbCore.mySessionObjects.ruajPeriudheNeSesion(Session, 3);
            merrSipasPeriudhes(DbCore.mySessionObjects.merrPeriudheNgaSesioni(Session).ToString(), gjithedok, idKonfigambjenti);
        }

        private void konfiguroPopupGride(int idGjuha, int idNdermarrje, int idPerdoruesi, bool kerkosaposhkruar, bool endlessScroll)
        {

            shtoNivel();
            shtoKodKonfigurimi(idGjuha, idNdermarrje, idPerdoruesi);
            shtoMonedha();
            shtoStatus();
            shtoColor();
            shtoLink();
            GridUtil.percaktoVisibleColumns(idGjuha, idNdermarrje, gvLupaKonvDok, "gvLupaKonvDok", "LupaKonvertimDokumentash.aspx");
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            GridUtil.KonfiguroGrideListeMadhePopupiPaTheme(gvLupaKonvDok, "IdDokumenti", kerkosaposhkruar, endlessScroll);
            this.gvLupaKonvDok.Columns["#"].VisibleIndex = 0;
            GridViewDataTextColumn col4 = gvLupaKonvDok.Columns["Vlefta"] as GridViewDataTextColumn;
            col4.PropertiesEdit.DisplayFormatString = "0.00";
        }

        private void shtoColor()
        {
            GridViewDataColumn g = gvLupaKonvDok.Columns["Ngjyra"] as GridViewDataColumn;
            g.Caption = "  ";
            g.DataItemTemplate = new MyGaugeTemplate();

        }
        private void shtoLink()
        {
            GridViewDataColumn g = gvLupaKonvDok.Columns["Raporti"] as GridViewDataColumn;

            g.DataItemTemplate = new MyLinkTemplate(Request.QueryString["windowWidth"]);

        }
        private void shtoNivel()
        {

            gvLupaKonvDok.Columns.Remove(gvLupaKonvDok.Columns["IdNiveli"]);
            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            colNivelRegjistrimi colniv = new colNivelRegjistrimi();

            clsNivelRegjistrimi niv = new clsNivelRegjistrimi();
            int idNiveli = int.Parse(Request.QueryString["niveli"]);
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idKategoria = clsNivelRegjistrimi.ktheIdKategoriaNivelRegjistrimi(idNiveli);
            if (veprimi == "RegjistrimDokumentash")
            {
                colniv.mbushGjitheNivelRegjistrimiSipasKategoriPaKonvertime(idKategoria, idNdermarrje, DbCore.mySessionObjects.ktheIdPerdoruesi(Session));

                if (idKategoria == 1)
                {
                    niv.mbushNivelRegjistrimiSipasKoditPaKonvertime("FD", idNdermarrje);
                    colniv.Add(niv);
                }
                else
                {
                    niv.mbushNivelRegjistrimiSipasKoditPaKonvertime("FH", idNdermarrje);
                    colniv.Add(niv);
                }
                colnew.PropertiesComboBox.DataSource = colniv;
                colnew.PropertiesComboBox.TextField = "Kodi";
                colnew.PropertiesComboBox.ValueField = "IdNivel";

            }

            colnew.PropertiesComboBox.ClientInstanceName = "IdNiveli";
            colnew.FieldName = "IdNiveli";
            gvLupaKonvDok.Columns.Add(colnew);
        }

        private void shtoKodKonfigurimi(int idGjuha, int idNdermarrje, int idPerdoruesi)
        {
            gvLupaKonvDok.Columns.Remove(gvLupaKonvDok.Columns["IdKonfigAmbjente"]);
            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();


            DbCore.DbShare.colKonfigurimAmbjenti colkonf = new DbCore.DbShare.colKonfigurimAmbjenti();
            DbCore.DbShare.clsKonfigurimAmbjenti konfamb = new DbCore.DbShare.clsKonfigurimAmbjenti();

            if (veprimi == "RegjistrimDokumentash")
            {
                int idKategoria = clsNivelRegjistrimi.ktheIdKategoriaNivelRegjistrimi(int.Parse(Request.QueryString["niveli"]));
                colkonf.mbushKonfigAmbjSipasIdKategori(idKategoria, idNdermarrje, idPerdoruesi, idGjuha);
                if (idKategoria == 1)
                {
                    konfamb.mbushKonfigAmbjSipasKod("FD", idNdermarrje);
                    colkonf.Add(konfamb);
                }
                else
                {
                    konfamb.mbushKonfigAmbjSipasKod("FH", idNdermarrje);
                    colkonf.Add(konfamb);
                }
                colnew.PropertiesComboBox.DataSource = colkonf;
                colnew.PropertiesComboBox.TextField = "KodKonfigAmbjente";
                colnew.PropertiesComboBox.ValueField = "IdKonfigAmbjente";
            }


            colnew.PropertiesComboBox.ClientInstanceName = "IdKonfigAmbjente";
            colnew.FieldName = "IdKonfigAmbjente";
            gvLupaKonvDok.Columns.Add(colnew);
        }


        private void shtoMonedha()
        {
            gvLupaKonvDok.Columns.Remove(gvLupaKonvDok.Columns["IdMonedha"]);

            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            DbCore.DbAdmin.colMonedhat col = new DbCore.DbAdmin.colMonedhat();
            col.mbushGjitheMonedhatAktive(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            DbCore.DbAdmin.clsMonedha mon = new DbCore.DbAdmin.clsMonedha();
            mon.IdMonedha = 0;
            mon.KodiMonedha = "";
            col.Add(mon);
            colnew.PropertiesComboBox.DataSource = col;
            colnew.PropertiesComboBox.TextField = "KodiMonedha";
            colnew.PropertiesComboBox.ValueField = "IdMonedha";
            colnew.FieldName = "IdMonedha";
            gvLupaKonvDok.Columns.Add(colnew);
        }
        private void shtoStatus()
        {
            DbCore.DbArkaBanka.clsDatabaseArkaBanka data = new DbCore.DbArkaBanka.clsDatabaseArkaBanka();
            gvLupaKonvDok.Columns.Remove(gvLupaKonvDok.Columns["Status"]);
            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            DataSet ds = data.merrStatusinDokumentave();
            data.Dispose();
            colnew.PropertiesComboBox.DataSource = ds;
            colnew.PropertiesComboBox.TextField = ds.Tables[0].Columns[1].ToString();
            colnew.PropertiesComboBox.ValueField = ds.Tables[0].Columns[0].ToString();
            colnew.FieldName = "Status";
            gvLupaKonvDok.Columns.Add(colnew);
        }
        protected void gvLupaKonvDok_DataBound(object sender, EventArgs e)
        {
            if (this.gvLupaKonvDok.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per 
                //perzgjidh
                GridViewCommandColumn check = new GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true; check.Width = Unit.Percentage(2);
                //behet per te afishuar rreshtin qe do sherbej per filtrim
                gvLupaKonvDok.Settings.ShowFilterRow = true;
                gvLupaKonvDok.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                gvLupaKonvDok.Settings.ShowFilterRowMenu = true;
                gvLupaKonvDok.Columns.Add(check);

                gvLupaKonvDok.KeyFieldName = "IdDokumenti";
                gvLupaKonvDok.SettingsBehavior.AllowSelectByRowClick = true;
                gvLupaKonvDok.SettingsBehavior.AllowFocusedRow = true;
            }


        }

        protected void gvLupaKonvDok_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            gvLupaKonvDok.Selection.UnselectAll();

        }

        protected void gvLupaKonvDok_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 1)
            {
                DbCore.mySessionObjects.ruajPeriudheNeSesion(Session, int.Parse(e.Parameters.ToString()));
                if (hfTeDrejtaGjitheDok.Value.ToString() == "True" || hfTeDrejtaGjitheDok.Value.ToString() == "true")
                    merrSipasPeriudhes(e.Parameters.ToString(), true, (int)hfState.Get("idKonfigAmbjente"));
                else
                    merrSipasPeriudhes(e.Parameters.ToString(), false, (int)hfState.Get("idKonfigAmbjente"));
            }
            else
            {
                int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);

                if (arr.Length == 3)
                {
                    if (arr[2] == "")
                        gvLupaKonvDok.FilterExpression = "";
                    else
                    {
                        DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                        DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaKonvDok", "LupaKonvertimDokumentash.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
                        filtra.mbushFilterPerGrideSipasKodit(arr[2], idNdermarrje, koka.IdGridaKoka);
                        if (filtra.FiltraKodi != null)
                        {
                            gvLupaKonvDok.FilterExpression = filtra.FiltraVlera;
                            GridUtil.renditGriden(filtra.KoloneRenditje, gvLupaKonvDok);
                        }
                    }
                }
                gvLupaKonvDok.Selection.UnselectAll();
            }
        }


        public void merrSipasPeriudhes(string arr, bool gjitheDok, int idKonfigambjenti)
        {
            DataTable dt = new DataTable();
            DbCore.DbAdmin.clsPeriudhaKontabel oPeriudha = new DbCore.DbAdmin.clsPeriudhaKontabel();

            int idperdorues = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            oPeriudha = DbCore.mySessionObjects.merrPeriudheKontabel(Session);

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
            if (veprimi == "RegjistrimDokumentash")
            {
                bool merrStatusPorosie = false;
                if (clsAlternativaKushti.getAlternativa(idKonfigambjenti, "SHSP") == "Po")
                    merrStatusPorosie = true;
                dt = colDokumentat.mbushGjitheDokumentatRegjistrimDokumentashKonvertim(idndermarje, datanga, dataderi, idperdorues, int.Parse(Request.QueryString["niveli"]), merrStatusPorosie, gjitheDok);
                gvLupaKonvDok.DataSource = dt;
            }
            DbCore.mySessionObjects.ruajGrideNeSessionLupa(Session, dt);
            gvLupaKonvDok.DataBind();
            dt.Dispose();
        }

        protected void gvLupaKonvDok_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpNoRows"] = gvLupaKonvDok.VisibleRowCount;
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
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, "LupaKonvertimDokumentash.aspx", this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            aSPxMenu1.Items.FindByName("Anullo").Text = rm.GetString("MenuItemMbyll", ci);
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
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaKonvDok", "LupaKonvertimDokumentash.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gvLupaKonvDok.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("IdDokumenti", gvLupaKonvDok);
            //    else
            //else
            DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();

            oPerdorues = DbCore.mySessionObjects.kthePerdorues(Session);
            filtri.IdPerdoruesi = oPerdorues.IdPerdorues;
            filtri.IdNdermarje = idNdermarrje;
            filtri.IdStatusDok = 1;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            mesazh = filtri.ruaj();
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaKonvDok", Convert.ToInt32(cmbKonfigurimi.Value), "LupaKonvertimDokumentash.aspx");
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
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaKonvDok", "LupaKonvertimDokumentash.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaKonvDok", Convert.ToInt32(cmbKonfigurimi.Value), "LupaKonvertimDokumentash.aspx");
                percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);
                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                gvLupaKonvDok.FilterExpression = String.Empty;
            }
        }

        //private void mbushComboBoxFiltra(int idNdermarrje)

    }

}
