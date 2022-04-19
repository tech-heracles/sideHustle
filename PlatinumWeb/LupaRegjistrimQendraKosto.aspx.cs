/// <param name="idKoka"></param>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DbCore;
using System.Data;
using DevExpress.Web;
using System.Web.UI.HtmlControls;
using PlatinumWeb.Templates;
using DbCore.DbRegjistrim;
using System.Web.Script.Serialization;using DbCore.DbShare;
using DbCore.DbKontabiliteti;
using System.Globalization;
using System.Resources;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.DbQendraKosto;

namespace PlatinumWeb
{
    public partial class LupaRegjistrimQendraKosto : MyPageBase
    {  
        System.Web.Script.Serialization.JavaScriptSerializer serializusi = new System.Web.Script.Serialization.JavaScriptSerializer();

        /// <summary>
        /// mbush fushat gjate modifikimit te dokumentit
        /// </summary>
        /// <param name="idPerdoruesi">id e perdoruesit</param>
        /// <param name="idNderViti">id e ndermarje vitit</param>
        /// <param name="idNdermarrje"> id e ndermarjes</param>
        /// <param name="koka">koka e ekzekutimit</param>
        public void MerrTedhenat(int idPerdoruesi, int idViti, int idNdermarrje, DbCore.DbQendraKosto.clsKokaQendraKosto koka)
        {
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasId(koka.IdKonfigAmbjente);
            cmbKonfigurimi.Value = konf.IdKonfigAmbjente.ToString();
            txtNrDok.Text = koka.NrDok;
            dteDtDok.Date = koka.DtDok;
            dteDtRegjistrimi.Date = koka.DtRegj;
            txtNrRef.Text = koka.NrRef.ToString();
            txtShenime.Text = koka.Pershkrimi;
        }

        /// <summary>
        /// mbush te dhenat kur faqja ben loadim
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                DbCore.clsFunksione.logout(Session,true,"FaqePaautorizuar");
            }
            int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerdoruesi);
            }

            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            percaktoTemplateMenu(idPerdoruesi, idViti, idNdermarrje, ASPxMenu1);
            hfState.Set("msgTrupiDokNukDuhetBosh", rm.GetString("msgTrupiDokNukDuhetBosh", ci));
            hfState.Set("msgShperndarjeNeQendratEKostos", rm.GetString("msgShperndarjeNeQendratEKostos", ci));
            int idDokGjenerues = int.Parse(Request.QueryString["idDokGjenerues"]);
            int idkonfig = int.Parse(Request.QueryString["idkonfig"].Split(';')[0]);

            if (!IsPostBack)
            {
                int idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
                hfState.Set("idGjuha", idGjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("idPerdoruesi", idPerdoruesi);
                hfState.Set("ndermarrjeVit", new DbCore.DbAdmin.clsNdermarrjeViti(DbCore.mySessionObjects.ktheNdermarrjeVit(Session)).Viti);
                System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
                if (hfShtimModifikim.Value == "")
                {
                    if (Request.QueryString["shtim_modifikim"] == "shtim" || String.IsNullOrEmpty(Request.QueryString["shtim_modifikim"]))
                    {
                        hfShtimModifikim.Value = "shtim";
                        konfiguroVleraFillestareShto(idNdermarrje, rm, ci, idGjuha);
                    }
                    else
                    {
                        hfShtimModifikim.Value = "modifikim";
                        konfiguroVleraFillestareModifiko(idDokGjenerues, idkonfig, idPerdoruesi, idViti, idNdermarrje, rm, ci, idGjuha);
                    }
                    percaktoTemplateMenu(idPerdoruesi, idViti, idNdermarrje, ASPxMenu1);
                }
                else
                    if (hfShtimModifikim.Value == "shtim")
                        konfiguroVleraFillestareShto(idNdermarrje, rm, ci, idGjuha);
                    else
                        if (hfShtimModifikim.Value == "modifikim")
                            konfiguroVleraFillestareModifiko(idDokGjenerues, idkonfig, idPerdoruesi, idViti, idNdermarrje, rm, ci, idGjuha);

                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, "LupaRegjistrimQendraKosto.aspx");
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
            }
            inicializoGridFaturat();
            konfiguroGrideFaturat(false);
        }

        public void btnJo_Click(object sender, EventArgs e){}

        public void btnPo_Click(object sender, EventArgs e){}

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        private void percaktoTemplateMenu(int idPerdoruesi, int idViti, int idNdermarrje, ASPxMenu aSPxMenu1)
        {
            DbCore.DbShare.colMenuItem menu = new DbCore.DbShare.colMenuItem(DbCore.mySessionObjects.ktheGjuhe(Session));
            menu.merrMenuItemSipasKomponentes(DbCore.mySessionObjects.ktheGjuhe(Session), "LupaRegjistrimQendraKosto.aspx", idPerdoruesi, idNdermarrje, idViti, hfShtimModifikim.Value == "modifikim" ? false : true);            
            foreach (DbCore.DbShare.clsMenuItem m in menu)
            {

                if (m.Name != "ItemFilter" && m.Name != "ItemFrame")
                {
                    clsToolbarConfig.ShtoMenuItem(this.Theme, aSPxMenu1, m);
                }

                if (m.Name == "Ruaj")
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = true;
                if (m.Name == "ItemFrame")
                    clsToolbarConfig.ShtoMenuItemPerFrame(this, aSPxMenu1, clsFunksione.ktheUrlHelpi(m.UrlHelp).Item1);
                if (m.Name == "Shto" || m.Name == "Kerko" || m.Name == "Pastro" || m.Name == "ItemFilter" || m.Name == "ItemFrame")
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].BeginGroup = true;
            }

            EventHandler handlerPerPo = new EventHandler(btnPo_Click);
            EventHandler handlerPerJo = new EventHandler(btnJo_Click);
            clsMenuInfo.ShtoMenuItemInfo(this, MenuInfo, handlerPerPo, handlerPerJo);
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            aSPxMenu1.Items.FindByName("Anullo").Text = rm.GetString("MenuItemMbyll", ci);
        }

        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.ktheNdermarrjeVit(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), ASPxMenu1);
        }

        /// <summary>
        /// vendos daten default
        /// </summary>
        private void vendosDataDefault()
        {
            DateTime sot = new DateTime();
            sot = DateTime.Today;
            DbCore.DbAdmin.clsPeriudhaKontabel periudha = DbCore.mySessionObjects.merrPeriudheKontabel(Session);
            if (sot >= periudha.FillimiPeriudha && sot <= periudha.MbarimiPeriudha)
                dteDtDok.Value = DateTime.Today;
            else
                dteDtDok.Value = periudha.FillimiPeriudha;
        }

        /// <summary>
        /// Vendos vlerat default kur po behet shtim
        /// </summary>
        /// <param name="idNdermarrje"></param>
        private void konfiguroVleraFillestareShto(int idNdermarrje, System.Resources.ResourceManager rm, System.Globalization.CultureInfo ci, int idgjuha)
        {
            dteDtDok.Date = DateTime.Today;
            AspxWebControlUtils.vendosDateEditMask(dteDtDok);
            dteDtRegjistrimi.Date = DateTime.Today;
            vendosDataDefault();
            AspxWebControlUtils.vendosDateEditMask(dteDtRegjistrimi);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmbQendraKosto);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmbObjektiva);
            ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje, cmbKonfigurimi, 75, rm, ci, idgjuha);
            cmbKonfigurimi.SelectedIndex = 0;
            ConfigureAspxComboBox.mbushComboLlojQendre(cmbLloji, true, rm, ci);
            cmbKonfigurimi.TextFormatString = "{0}";
            inicializoGridFaturat();
            konfiguroGrideFaturat(true);
            DbCore.DbShare.clsFormatiKonfig formatNrPerKonfig = new clsFormatiKonfig();
            formatNrPerKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(int.Parse(cmbKonfigurimi.Value.ToString()));
            if (formatNrPerKonfig.KonfigTrupi.Count == 0)
            {
                DbCore.DbShare.clsFormatKonfigTrup trup = new clsFormatKonfigTrup(clsFormatKonfigTrup.defaultFormatSasia, clsFormatKonfigTrup.defaultFormatCmimi, clsFormatKonfigTrup.defaultFormatVlefta, clsFormatKonfigTrup.defaultFormatZbritja, clsFormatKonfigTrup.defaultFormatStringSasia, clsFormatKonfigTrup.defaultFormatStringCmimi, clsFormatKonfigTrup.defaultFormatStringVlefta, clsFormatKonfigTrup.defaultFormatStringZbritja);
                formatNrPerKonfig.KonfigTrupi.Add(trup);
            }
            
            hfState.Set("formatMonedhe", serializusi.Serialize(formatNrPerKonfig));

        }

        /// <summary>
        /// Vendos vlerat default kur po behet modifikim
        /// </summary>
        /// <param name="idPerdoruesi"></param>
        /// <param name="idNderViti"></param>
        /// <param name="idNdermarrje"></param>
        private void konfiguroVleraFillestareModifiko(int idDokGjenerues, int idkonfig, int idPerdoruesi, int idViti, int idNdermarrje, System.Resources.ResourceManager rm, System.Globalization.CultureInfo ci, int idGjuha)
        {//mbush kombot dhe gridat

            AspxWebControlUtils.vendosDateEditMask(dteDtDok);
            AspxWebControlUtils.vendosDateEditMask(dteDtRegjistrimi);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmbQendraKosto);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmbObjektiva);
            ConfigureAspxComboBox.mbushComboLlojQendre(cmbLloji, true, rm, ci);
            ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 75, rm, ci, idGjuha);
            cmbKonfigurimi.TextFormatString = "{0}";
            DbCore.DbQendraKosto.clsKokaQendraKosto qend = new DbCore.DbQendraKosto.clsKokaQendraKosto();
            qend.KtheKokaQKSipasIDGjeneruesDheKonfig(idDokGjenerues, idkonfig);
            hfState.Set("IdKoka", qend.IdKoka);
            if (qend != null)
            {
                MerrTedhenat(idPerdoruesi, idViti, idNdermarrje, qend);
            }
            inicializoGridFaturat();
            konfiguroGrideFaturat(true);
            DbCore.DbShare.clsFormatiKonfig formatNrPerKonfig = new clsFormatiKonfig();
            formatNrPerKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(int.Parse(cmbKonfigurimi.Value.ToString()));
            if (formatNrPerKonfig.KonfigTrupi.Count == 0)
            {
                DbCore.DbShare.clsFormatKonfigTrup trup = new clsFormatKonfigTrup(clsFormatKonfigTrup.defaultFormatSasia, clsFormatKonfigTrup.defaultFormatCmimi, clsFormatKonfigTrup.defaultFormatVlefta, clsFormatKonfigTrup.defaultFormatZbritja, clsFormatKonfigTrup.defaultFormatStringSasia, clsFormatKonfigTrup.defaultFormatStringCmimi, clsFormatKonfigTrup.defaultFormatStringVlefta, clsFormatKonfigTrup.defaultFormatStringZbritja);
                formatNrPerKonfig.KonfigTrupi.Add(trup);
            }            
            hfState.Set("formatMonedhe", serializusi.Serialize(formatNrPerKonfig));
        }
        /// <summary>
        /// Percakton veprimin qe kryhet kur klikohet nje nga butonat e menuse
        /// </summary>
        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {
        }

        #region  GRIDA E FATURAVE

        private void inicializoGridFaturat()
        {
            ASPxGridView grid_faturat = (ASPxGridView)ASPxNavBar1.Groups[0].FindControl("grid_faturat");
            DbCore.DbKontabiliteti.colTrupatFletetKontabel col = new DbCore.DbKontabiliteti.colTrupatFletetKontabel();
            col.mbushTrupiSipasKokesPerQK(int.Parse(Request.QueryString["idDokGjenerues"]));
            hfState.Set("llogariFK", serializusi.Serialize(col));
            grid_faturat.DataSource = col;
            grid_faturat.DataBind();
        }


        private void konfiguroGrideFaturat(bool visibleindex)
        {
            shtokolona();
            ASPxGridView grid_faturat = (ASPxGridView)ASPxNavBar1.Groups[0].FindControl("grid_faturat");
            if (visibleindex)
                GridUtil.percaktoVisibleColumnsMeWidth(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), grid_faturat, "grid_faturat", "LupaRegjistrimQendraKosto.aspx");
            else GridUtil.percaktoVisibleColumnsMeWidthPaVisibleIndex(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), grid_faturat, "grid_faturat", "LupaRegjistrimQendraKosto.aspx");
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            var endlessScroll = clsAlternativaKushti.getAlternativa(int.Parse(cmbKonfigurimi.Value.ToString()), "ES") == "Po";
            GridUtil.KonfiguroGrideListeMadhePopupiPaTheme(grid_faturat, "IdTrupiFleteKontabel",true, endlessScroll);
            grid_faturat.Columns["#"].VisibleIndex = 0;
        }

        private void shtokolona()
        {
            ASPxGridView grid_faturat = (ASPxGridView)ASPxNavBar1.Groups[0].FindControl("grid_faturat");
            GridViewDataTextColumn colnew1;
            if (grid_faturat.Columns["IdTrupiFleteKontabel"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "IdTrupiFleteKontabel";
                grid_faturat.Columns.Add(colnew1);
            }
            if (grid_faturat.Columns["IdKokaFleteKontabel"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "IdKokaFleteKontabel";
                grid_faturat.Columns.Add(colnew1);
            }
            if (grid_faturat.Columns["IdLlogari"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "IdLlogari";
                grid_faturat.Columns.Add(colnew1);
            }
            if (grid_faturat.Columns["NrLlogari"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "NrLlogari";
                grid_faturat.Columns.Add(colnew1);
            }
            if (grid_faturat.Columns["EmerLlogari"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "EmerLlogari";
                grid_faturat.Columns.Add(colnew1);
            }
            if (grid_faturat.Columns["PershkrimTrupiFleteKontabel"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "PershkrimTrupiFleteKontabel";
                grid_faturat.Columns.Add(colnew1);
            }
            if (grid_faturat.Columns["IdMonedha"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "IdMonedha";
                grid_faturat.Columns.Add(colnew1);
            }
            if (grid_faturat.Columns["KodMonedha"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "KodMonedha";
                grid_faturat.Columns.Add(colnew1);
            }
            if (grid_faturat.Columns["Kursi"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "Kursi";
                grid_faturat.Columns.Add(colnew1);
            }

            if (grid_faturat.Columns["DK"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "DK";
                grid_faturat.Columns.Add(colnew1);
            }
            if (grid_faturat.Columns["VleftaDebiTrupiFleteKontabel"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "VleftaDebiTrupiFleteKontabel";
                colnew1.PropertiesTextEdit.DisplayFormatString = "0.00";
                grid_faturat.Columns.Add(colnew1);
            }

            if (grid_faturat.Columns["VleftaKrediTrupiFleteKontabel"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "VleftaKrediTrupiFleteKontabel";
                grid_faturat.Columns.Add(colnew1);
            }
            if (grid_faturat.Columns["VleftaDebiMonBazeTrupiFleteKontabel"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "VleftaDebiMonBazeTrupiFleteKontabel";
                grid_faturat.Columns.Add(colnew1);
            }
            if (grid_faturat.Columns["VleftaKrediMonBazeTrupiFleteKontabel"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "VleftaKrediMonBazeTrupiFleteKontabel";
                grid_faturat.Columns.Add(colnew1);
            }
            if (grid_faturat.Columns["KodiSkemaKontabel"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "KodiSkemaKontabel";
                grid_faturat.Columns.Add(colnew1);
            }

        }
        protected void grid_faturat_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {

        }

        protected void grid_faturat_DataBound(object sender, EventArgs e)
        {
            ASPxGridView grid_faturat = (ASPxGridView)this.ASPxNavBar1.Groups[0].FindControl("grid_faturat");
            if (grid_faturat.Columns["#"] == null)
            {
                DevExpress.Web.GridViewCommandColumn check = new DevExpress.Web.GridViewCommandColumn("#") { ShowSelectCheckbox = true, Width = System.Web.UI.WebControls.Unit.Percentage(2) };

                grid_faturat.Settings.ShowFilterRow = true;
                grid_faturat.Settings.ShowHeaderFilterButton = true;
                grid_faturat.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                grid_faturat.Settings.ShowFilterRowMenu = true;
                grid_faturat.Columns.Add(check);
                grid_faturat.Settings.ShowGroupPanel = false;
                grid_faturat.KeyFieldName = "IdTrupiFleteKontabel";
                grid_faturat.SettingsBehavior.AllowSelectByRowClick = true;
                grid_faturat.SettingsBehavior.AllowFocusedRow = true;
                grid_faturat.Settings.ShowTitlePanel = false;
                grid_faturat.SettingsText.Title = "Zgjidhni llogarite";
            }
        }

        protected void grid_faturat_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            inicializoGridFaturat();

        }

        protected void grid_faturat_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            ASPxGridView grid_faturat = (ASPxGridView)this.ASPxNavBar1.Groups[0].FindControl("grid_faturat");
            e.Properties["cpNoRows"] = grid_faturat.VisibleRowCount;
        }
        protected void grid_faturat_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {

        }
        protected void grid_faturat_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {

        }

        #endregion

        protected void cmbQendraKosto_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].Contains("cmbQendraKosto"))
                {
                    if (cmbLloji.Value.ToString() == "1")
                        ConfigureAspxComboBox.mbushComboQendraKostoBij(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), cmbQendraKosto);
                    else ConfigureAspxComboBox.mbushComboSkemaQendraKosto(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), cmbQendraKosto);
                }
            }
        }

        protected void cmbQendraKosto_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].Contains("cmbQendraKosto"))
                {
                    if (string.IsNullOrWhiteSpace(e.Filter)) return;
                    DbCore.DbQendraKosto.colQendraKosto col = new DbCore.DbQendraKosto.colQendraKosto();
                    colKokaSkemaQK col1 = new colKokaSkemaQK();
                    if (cmbLloji.Value.ToString() == "1")
                    {
                       col.mbushGjitheQendraKostoBijSipasNdermarjesAktiv(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                        cmbQendraKosto.DataSource = col.Where(x => x.Kodi.IndexOf(e.Filter, StringComparison.InvariantCultureIgnoreCase) > -1).Skip(e.BeginIndex).Take(e.EndIndex + 1);
                        cmbQendraKosto.TextField = "Kodi";
                        cmbQendraKosto.ValueField = "Id";
                    }
                    else
                    {
                        col1.mbushGjitheSkematSipasNdermarjes(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                        cmbQendraKosto.DataSource = col1.Where(x => x.Kodi.IndexOf(e.Filter, StringComparison.InvariantCultureIgnoreCase) > -1).Skip(e.BeginIndex).Take(e.EndIndex + 1);
                        cmbQendraKosto.TextField = "Kodi";
                        cmbQendraKosto.ValueField = "IdKoka";
                      
                    }
              
                    cmbQendraKosto.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                    cmbQendraKosto.DataBind();


                }
            }
        }

        protected void cmbObjektiva_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("cmbObjektiva"))
                ConfigureAspxComboBox.KonfiguroComboBoxObjektivaKosto(cmbObjektiva, IdNdermarrja);
        }
    }
}