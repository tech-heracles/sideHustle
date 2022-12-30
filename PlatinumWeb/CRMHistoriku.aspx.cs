using DbCore;
using DbCore.DbAdmin;
using DbCore.DbShare;
using DevExpress.Data.Filtering;
using DevExpress.Web;
using PlatinumWeb.Templates;
using System;
using System.Data;
using System.Globalization;
using System.Resources;
using System.Web.Script.Serialization;
using System.Web.UI;
using DbCore.IMBUtils.Types;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Filters;
using PlatinumWeb.ApplicationUtils.Pages;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{
    public partial class CRMHistoriku : MyPageBase
    {
        private const string komponente = "CRMHistoriku.aspx";
        private const int idKomponente = 2003;
        private const string mesazhZgjidhniNje = "Ju lutem zgjidhni te pakten nje Takim!";
        private const int idstatusdok = 1;       
        private int idKonfig;
        private TitlePeriudha _periudha;
        public TitlePeriudha Periudha => _periudha ?? (_periudha = this.MerrPeriudhe(hfState));

        /// <summary>
        /// thirret kur faqja lodohet. Ne te behet kontrolli nese perdoruesi eshte i loguar ne
        /// sistem dhe nqs jo ridrejtohet tek forma e logimit thirret inicializimi i konfigurimeve
        /// fillestare te faqes
        /// </summary>
        /// <param name="sender"> derguesi </param>
        /// <param name="e">      argumenti </param>
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

            if (!Page.IsPostBack)
            {
                hfState.Set("idGjuha", IdGjuha);
                hfState.Set("idNdermarrje", IdNdermarrja);
                hfState.Set("idPerdoruesi", IdPerdoruesi);
                hfState.Set("idNdermarrjeVit", IdNdermarrjeVit);
                hfState.Set("idViti", IdViti);
                lblUserEmri.Text = DbCore.mySessionObjects.ktheEmerPerdorues(Session);

                hfState.Set("msgZgjdhniNjeNgaElementetEListes", MessagesResource.Messages["msgZgjdhniNjeNgaElementetEListes"]);
                DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();

                konfiguroVleraFillestare();
                idKonfig = int.Parse(cmbKonfigurimi.Value.ToString());

                konf.mbushKonfigAmbjSipasId(idKonfig, IdGjuha);

                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, komponente);                
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
            }
            else
            {
                idKonfig = int.Parse(cmbKonfigurimi.Value.ToString());
            }
            gvHistoriku.PercaktoTitlePanelMePeriudhe(this, MenuInfo, pnlMesazhi, hfState, IdPerdoruesi, IdNdermarrja, IdViti, IdGjuha, idKonfig, komponente, idKomponente, "KodiAgjenti", rm, ci, false);
          
            mbushGrideHistoriku(!IsPostBack);

            if (!IsPostBack)
            {
                konfiguroGrideHistoriku();
                if (!string.IsNullOrWhiteSpace(Request.QueryString["idKlienti"]))
                {
                    KrijoFilterExpression("IdKlienti", Request.QueryString["idKlienti"], gvHistoriku);
                    KrijoFilterExpression("DtVizita", Convert.ToDateTime(Request.QueryString["data"]), gvHistoriku);
                }
            }
            percaktoTemplateMenu();
            clsToolbarConfig.mbushComboBoxFiltraMeValFieldTextField(IdGjuha, IdNdermarrja, "gvHistoriku", komponente, "IdFiltra", "FiltraShenime" , idKonfig);
        }

        #region TE PERGJITSHME AMBJENTI

        /// <summary>
        /// krijo filter expression per gridat
        /// </summary>
        /// <param name="FieldName"></param>
        /// <param name="value"></param>
        /// <param name="grid"></param>
        public void KrijoFilterExpression(string FieldName, object value, ASPxGridView grid)
        {
            var criterias = CriteriaColumnAffinityResolver.SplitByColumns(CriteriaOperator.Parse(grid.FilterExpression));

            BinaryOperatorType operatorType;

            operatorType = BinaryOperatorType.Equal;

            if (!criterias.Keys.Contains(new OperandProperty(FieldName)))
                criterias.Add(new OperandProperty(FieldName), new BinaryOperator(FieldName, value, operatorType));
            else
                criterias[new OperandProperty(FieldName)] = new BinaryOperator(FieldName, value, operatorType);
            grid.FilterExpression = CriteriaOperator.ToString(GroupOperator.And(criterias.Values));
        }

        /// <summary>
        /// fshin filtrin e zgjedhur dhe aplikuar mbi gride, fshirje nga DB ku ai eshte ruajtur, dhe
        /// jo pastrimi i grides nga aplikimi i filtrit per kete do sherbeje Clear Filter ne fund te grides
        /// </summary>
        /// <param name="sender"> derguesi </param>
        /// <param name="e">      argumentat </param>
        public void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje
            MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            idKonfig = int.Parse(cmbKonfigurimi.Value.ToString());

            DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();

            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(IdGjuha, "gvHistoriku", komponente, IdNdermarrja, idKonfig);
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, IdNdermarrja, koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();

                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                cmbFiltra.Text = "";

                hfStatusi.Value = "true";
            }
        }

        /// <summary>
        /// ndodh kur menuja ben bound 
        /// </summary>
        /// <param name="sender"> derguesi </param>
        /// <param name="e">      argumentat </param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            //percaktoTemplateMenu(idGjuha, idViti, idPerdoruesi, idNdermarrje, ASPxMenu1);
        }

        /// <summary>
        /// perdoret per te shtuar kolonen e selektimit tek grida dhe per te vendosur disa
        /// karakteristika te grides
        /// </summary>
        /// <param name="sender"> derguesi </param>
        /// <param name="e">      argumentat </param>
        /// <summary>
        /// sherben per te ruajtur filtrin e zgjedhur 
        /// </summary>
        /// <param name="sender"> derguesi </param>
        /// <param name="e">      argumentat </param>
        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje
            MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            idKonfig = int.Parse(cmbKonfigurimi.Value.ToString());

            DbCore.DbAdmin.clsFiltraGrida filtri = new DbCore.DbAdmin.clsFiltraGrida() { FiltraKodi = cmbFiltra.Text, FiltraShenime = cmbFiltra.Text, FiltraUniversal = false };

            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(IdGjuha, "gvHistoriku", komponente, IdNdermarrja, idKonfig);
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gvHistoriku.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("KodiAgjenti", gvHistoriku);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvHistoriku.GetSortedColumns();
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
            //    filtri.KoloneRenditje = "KodiAgjenti";
            //    filtri.DrejtimRenditje = true;
            //}

            filtri.IdPerdoruesi = IdPerdoruesi;
            filtri.IdNdermarje = IdNdermarrja;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            filtri.IdStatusDok = 1;

            mesazh = filtri.ruaj();

            if (mesazh.Status == true)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            hfStatusi.Value = "true";
            cmbFiltra.Text = "";
        }

        /// <summary>
        /// perdoret per te trajtuar ngjarjet e butonave te menuse. 
        /// </summary>
        /// <param name="source"> derguesi </param>
        /// <param name="e">      argumentat </param>
        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {//kryen veprimet e menuse
        }

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes 
        /// </summary>
        /// <param name="idNderVit">   </param>
        /// <param name="idPerdorues"> </param>
        /// <param name="idNdermarrje"></param>
        /// <param name="aSPxMenu1">    menuja ne te cilat do te shtohen kontrollet </param>
        private void percaktoTemplateMenu()
        {
            clsToolbarConfig.percaktoTemplateMenu(IdGjuha, IdViti, IdPerdoruesi, IdNdermarrja, ASPxMenu1, komponente, this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, hfShtimModifikim.Value == "modifikim" ? false : true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session), true);
        }


        /// <summary>
        /// konfiguron vlerat fillestare
        /// </summary>
        /// <param name="idNdermarrje">id e ndermarjes</param>
        private void konfiguroVleraFillestare()
        { //mbush komboboxet dhe gridat e faqes
            ASPxPageControl1.ActiveTabIndex = 0;
            mbushGridTrupAnkete(-1, -1, null, -1);//-1 sepse duam te marrim vetem emrat e kolonave
            mbushGridDetyrash(-1, -1, null, -1);
            ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(IdPerdoruesi, IdNdermarrja, cmbKonfigurimi, 104, rm, ci, IdGjuha);
            cmbKonfigurimi.SelectedIndex = 0;
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()));
            hfKonffillestar.Value = String.Format("{0};{1}", konf.KodKonfigAmbjente, konf.PershkrimKonfigAmbjente);
            ConfigureAspxComboBox.percaktoTemplateComboMeEnableCallback(btnCaktoNeHarte);
            ConfigureAspxComboBox.percaktoTemplateComboMeEnableCallback(btnKoordinatatEKlientit);
        }

        #endregion TE PERGJITSHME AMBJENTI

        #region GRIDA E MADHE E HISTORIKUT

        /// <summary>
        /// cakton properti ne javascript 
        /// </summary>
        /// <param name="sender"> derguesi </param>
        /// <param name="e">      argumentat </param>
        protected void gvHistoriku_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
        }

        /// <summary>
        /// kur grida ben callback 
        /// </summary>
        /// <param name="sender"> derguesi </param>
        /// <param name="e">      argumentat </param>
        protected void gvHistoriku_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (gvHistoriku.AplikoFilterDefault(e, Convert.ToInt32(cmbKonfigurimi.Value)))
                return;

            if (e.Parameters.Contains(TitlePeriudha.KeyParamNdryshimPeriudhe))
                mbushGrideHistoriku(!IsPostBack);
        }

        /// <summary>
        /// perdoret per te shfaqur po jo tek komboja e aktivit 
        /// </summary>
        /// <param name="sender"> derguesi </param>
        /// <param name="e">      argumentat </param>
        protected void gvHistoriku_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
        }

        /// <summary>
        /// per filtrimin me elementin bosh 
        /// </summary>
        /// <param name="sender"> derguesi </param>
        /// <param name="e">      argumentat </param>
        protected void gvHistoriku_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if (e.Column.FieldName == "NjesiKohe")
            {
                if (Converter.ConvertToInt(e.Value) == 0)
                {
                    e.Criteria = null;
                }
            }
        }

        protected void gvHistoriku_DataBound(object sender, EventArgs e)
        {// shton colonen # per selektim dhe disa karakteristika te grides
            if (gvHistoriku.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per
                //perzgjidh
                GridViewCommandColumn check = new GridViewCommandColumn("#") { ShowSelectCheckbox = true, Width = System.Web.UI.WebControls.Unit.Percentage(2) };
                //behet per te afishuar rreshtin qe do sherbej per filtrim
                gvHistoriku.Settings.ShowFilterRow = true;
                gvHistoriku.Settings.ShowFilterRowMenu = true;
                gvHistoriku.Settings.ShowFilterBar = GridViewStatusBarMode.Auto;
                gvHistoriku.Columns.Add(check);
                gvHistoriku.KeyFieldName = "IdAgjenti;IdKlienti;DtVizita";
                gvHistoriku.SettingsBehavior.AllowSelectByRowClick = true;
                gvHistoriku.SettingsBehavior.AllowFocusedRow = true;
                gvHistoriku.SettingsPager.PageSize = 20;
            }
        }

        /// <summary>
        /// perdoret ne rastet kur grida ben callback per ta rimbushur griden 
        /// </summary>
        /// <param name="sender"> derguesi </param>
        /// <param name="e">      argumentat </param>
        protected void gvHistoriku_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            if (e.CallbackName == "COLUMNMOVE" && gvHistoriku.AllColumns[int.Parse(e.Args[0])].Width.Value == 0)
                gvHistoriku.AllColumns[int.Parse(e.Args[0])].Width = System.Web.UI.WebControls.Unit.Percentage(3);
            if (e.CallbackName == "APPLYFILTER" && e.Args[0] == "")
            {
                MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
                ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
                cmbFiltra.Text = "";
            }
        }

        /// <summary>
        /// perdoret per te vendosur filtrat tek headeri i grides 
        /// </summary>
        /// <param name="sender"> derguesi </param>
        /// <param name="e">      argumentat </param>
        protected void gvHistoriku_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            var grida = sender as ASPxGridView;
            var columns = grida.AllColumns;
            foreach (var col in columns)
            {
                if (col.GetType() == typeof(string) && e.Column.FieldName.Equals(col.Name))
                {
                    e.Values.Clear();
                    e.AddValue("(Te gjithe)", string.Empty, "true");
                    e.AddValue("Nga A-D ", string.Empty, String.Format("{0}>'A     ' and {0}<'DDDDDDD'", e.Column.FieldName));
                    e.AddValue("Nga D-G ", string.Empty, String.Format("{0}>'D     ' and {0}<'GGGGGGG'", e.Column.FieldName));
                    e.AddValue("Nga H-K ", string.Empty, String.Format("{0}>'H     ' and {0}<'KKKKKKK'", e.Column.FieldName));
                    e.AddValue("Nga L-O ", string.Empty, String.Format("{0}>'L     ' and {0}<'OOOOOOO'", e.Column.FieldName));
                    e.AddValue("Nga P-S ", string.Empty, String.Format("{0}>'P     ' and {0}<'SSSSSSS'", e.Column.FieldName));
                    e.AddValue("Nga T-W ", string.Empty, String.Format("{0}>'T     ' and {0}<'WWWWWWW'", e.Column.FieldName));
                    e.AddValue("Nga X-Z ", string.Empty, String.Format("{0}>'X     ' and {0}<'ZZZZZZZ'", e.Column.FieldName));
                }
                else
                {
                    e.Values.Clear();
                    e.AddValue("(Te gjithe)", string.Empty, "true");
                }
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="datanga">  fillim periudha </param>
        /// <param name="dataderi"> mbarim periudha </param>
        /// <param name="ngaDB">    tru nese do merren te dhenat nga DB else nga sessioni </param>
        private void mbushGrideHistoriku(bool ngaDB)
        {
            DataTable tmpObject;
            if (ngaDB)
            {
                tmpObject = DbCore.DbCRM.colSkeduler.merrHistorikTakimesh(IdNdermarrja, IdPerdoruesi, Periudha.DataDokNga, Periudha.DataDokDeri);
                DbCore.mySessionObjects.ruajGrideNeSession(komponente, IdViti,Periudha.PeriudhaDok, Session, tmpObject);
            }
            else
            {
                bool sukses = DbCore.mySessionObjects.merrGrideNgaSessioni(komponente, IdViti, Periudha.PeriudhaDok, Session, out tmpObject);
                if (!sukses)
                {
                    tmpObject = DbCore.DbCRM.colSkeduler.merrHistorikTakimesh(IdNdermarrja, IdPerdoruesi, Periudha.DataDokNga, Periudha.DataDokDeri);
                    DbCore.mySessionObjects.ruajGrideNeSession(komponente, IdViti, Periudha.PeriudhaDok, Session, tmpObject);
                }
            }
            gvHistoriku.DataSource = tmpObject;
            gvHistoriku.DataBind();
            tmpObject.Dispose();
        }

        /// <summary>
        /// perdoret per te konfiguruar griden. Per te dhenat mbi kolonat qe do te shfaqen nga
        /// databaza si dhe percakton disa karakteristika te grides
        /// </summary>
        /// : 

        private void konfiguroGrideHistoriku()
        {
            string kodi = clsKonfigurimAmbjenti.ktheKodKonfigurimi(idKonfig);
            GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(IdNdermarrja, "gvHistoriku", gvHistoriku, kodi, idKomponente.ToString(), IdGjuha);
            gvHistoriku.Columns["#"].VisibleIndex = 0;
        }

        #endregion GRIDA E MADHE E HISTORIKUT

        #region ANKETAT
        
        /// <summary>
        /// perdoret per te shtuar kolonen e selektimit tek grida dhe per te vendosur disa
        /// karakteristika te grides
        /// </summary>
        /// <param name="sender"> derguesi </param>
        /// <param name="e">      argumentat </param>
        protected void gvTrupi_DataBound(object sender, EventArgs e)
        {//shton butonin fshi
            if (this.gvTrupi.Columns["Shiko"] == null)
            {
                GridViewDataTextColumn shiko = new GridViewDataTextColumn();
                shiko.Caption = "Shiko";
                shiko.Width = 30;
                gvTrupi.Columns.Add(shiko);
            }
            GridViewDataTextColumn col0 = gvTrupi.Columns["Shiko"] as GridViewDataTextColumn;
            col0.DataItemTemplate = new MyButtonTemplate("Shiko");
            col0.VisibleIndex = 5;
            GridUtil.percaktoVisibleColumnsMeWidth(IdGjuha, IdNdermarrja, gvTrupi, "gvTrupi", komponente);
            GridUtil.konfiguroGrideRegjistrimEvogelPaTheme(gvTrupi, "Id");

            gvTrupi.Settings.ShowFooter = false;
            gvTrupi.Settings.ShowTitlePanel = true;
        }

        /// <summary>
        /// kur krijohen rreshtat e grides 
        /// </summary>
        /// <param name="sender"> derguesi </param>
        /// <param name="e">      argumentat </param>
        protected void gvTrupi_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {//krijon rreshat sipas modelit
            if (e.RowType == DevExpress.Web.GridViewRowType.Data)
            {
                GridViewDataTextColumn col0 = ((ASPxGridView)sender).Columns["Shiko"] as GridViewDataTextColumn;

                ASPxButton btn0 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col0, "btn") as ASPxButton;

                //vendosen client side eventet e kolonave
                if (btn0 != null)
                {
                    btn0.CssClass = "bt";
                    btn0.ClientInstanceName = String.Format("btnShiko{0}", e.VisibleIndex);
                    btn0.ClientSideEvents.Click = String.Format("function(s,e){{ShikoFoto({0},{1},'{2}');}}", e.KeyValue, 103, "Foto Per Anketa");
                    if (DbCore.DbShare.clsArkiva.ekzistonDokumenti(llojDok: 103, idDok: Convert.ToInt32(e.KeyValue)))
                    {
                        btn0.BackgroundImage.ImageUrl = "~/images/CRM/kafoto.png";
                        return;
                    }
                    btn0.Enabled = false;
                }
            }
        }

        private void mbushGridTrupAnkete(int idAgjenti, int idKlienti, string dtTakimi, int idPerdoruesi)
        {//mbushet grida me te dhena
            DataTable dt = DbCore.DbCRM.colTrupiKlientAnketaAgjent.merrTrupKlientAnketaAgjent(IdNdermarrja, idAgjenti, idKlienti, dtTakimi, idPerdoruesi);
            gvTrupi.DataSource = dt;
            gvTrupi.DataBind();
        }

        #endregion ANKETAT

        #region Detyrat

        protected void gvDetyrat_DataBound(object sender, EventArgs e)
        {
            gvDetyrat.KeyFieldName = "IdKlientDetyreAgjent";
            gvDetyrat.Settings.ShowFilterRow = false;
            gvDetyrat.Settings.ShowFooter = false;
            gvDetyrat.SettingsBehavior.AllowSelectByRowClick = false;
            gvDetyrat.SettingsBehavior.AllowFocusedRow = true;
            gvDetyrat.SettingsText.Title = "Informacion mbi detyrat";
            gvDetyrat.Settings.ShowTitlePanel = true;
            if (this.gvDetyrat.Columns["Shiko"] == null)
            {
                GridViewDataTextColumn shiko = new GridViewDataTextColumn();
                shiko.Caption = "Shiko";
                shiko.Width = 30;
                gvDetyrat.Columns.Add(shiko);
            }
            GridViewDataTextColumn col0 = gvDetyrat.Columns["Shiko"] as GridViewDataTextColumn;
            col0.DataItemTemplate = new MyButtonTemplate("Shiko");
            col0.VisibleIndex = gvDetyrat.Columns.Count - 1;

            if (!IsPostBack)
                GridUtil.percaktoVisibleColumnsMeWidth(IdGjuha, IdNdermarrja, gvDetyrat, "gvDetyrat", komponente);

            GridUtil.konfiguroGrideRegjistrimEvogelPaTheme(gvDetyrat, "IdKlientDetyreAgjent");
        }        

        protected void gvDetyrat_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType == DevExpress.Web.GridViewRowType.Data)
            {
                GridViewDataTextColumn col0 = ((ASPxGridView)sender).Columns["Shiko"] as GridViewDataTextColumn;

                ASPxButton btn0 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col0, "btn") as ASPxButton;

                //vendosen client side eventet e kolonave
                if (btn0 != null)
                {
                    btn0.CssClass = "bt";
                    btn0.ClientInstanceName = String.Format("btnShiko{0}", e.VisibleIndex);
                    btn0.ClientSideEvents.Click = String.Format("function(s,e){{ShikoFoto({0},{1},'{2}');}}", e.KeyValue, 110, "Foto Per Detyra");
                    if (DbCore.DbShare.clsArkiva.ekzistonDokumenti(llojDok: 110, idDok: Convert.ToInt32(e.KeyValue)))
                    {
                        btn0.BackgroundImage.ImageUrl = "~/images/CRM/kafoto.png";
                        return;
                    }
                    btn0.Enabled = false;
                }
            }
        }

        /// <summary>
        /// merr si parameter ID e detyres 
        /// </summary>
        /// <param name="key"></param>
        private void mbushGridDetyrash(int idAgjenti, int idKlienti, string dtTakimi, int idPerdoruesi)
        {
            DataTable dt = DbCore.DbCRM.colDetyreKlientAgjent.MerrDetyrat(IdNdermarrja, idAgjenti, idKlienti, dtTakimi, idPerdoruesi);
            gvDetyrat.DataSource = dt;
            gvDetyrat.DataBind();
        }


        #endregion Detyrat

        protected void DetajetPanel_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].ToString().Contains("DetajetPanel"))
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string[] vlerat = e.Parameter.Split(';');
                int idPerdoruesi = mySessionObjects.ktheIdPerdoruesi(Session);
                int idAgjenti = int.Parse(vlerat[0]);
                int idKlienti = int.Parse(vlerat[1]);

                string data = serializer.Deserialize<DateTime>(vlerat[2]).ToString("yyyyMMdd");
                string anketa = vlerat[3].ToString();
                mbushGridDetyrash(idAgjenti, idKlienti, data, idPerdoruesi);
                mbushGridTrupAnkete(idAgjenti, idKlienti, data, idPerdoruesi);
                gvTrupi.SettingsText.Title = string.Format("Informacion mbi anketen({0})", anketa);
            }
        }
    }
}