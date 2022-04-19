using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Data;

namespace PlatinumWeb
{
    public partial class VeprimeKF_old : System.Web.UI.Page
    {
        private const string prefixMesazhNjejes = "Dokumenti  me Nr: ";
        private const string prefixMesazhShumes = "dokumenti me Nr: ";
        private const string suffixMesazhNjejesLidhurGabimi = " eshte e lidhur dhe nuk mund te fshihet! ";
        private const string suffixMesazhShumesLidhurGabimi = " jane te lidhur dhe nuk mund te fshihen! ";
        private const string suffixMesazhNjejesPeriudheKycurGabimi = " i perket nje periudhe te kycur dhe nuk mund te fshihet! ";
        private const string suffixMesazhShumesPeriudheKycurGabimi = " i perkasin periudhave te kycura dhe nuk mund te fshihen! ";
        private const string suffixMesazhNjejesSuksesi = " u fshi me sukses!";
        private const string suffixMesazhShumesSuksesi = " u fshine me sukses!";
        private const string lidhesMesazhi = ". Kurse ";
        private const string mesazhZgjidhniNje = "Ju lutem zgjidhni te pakten nje dokument!";

        DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();

        protected void Page_PreInit(object sender, EventArgs e)
        {
            DbCore.clsFunksione.percaktoThemeAmbjenteDheJQuery(Page, mySessionObjects.ktheIdPerdoruesi(Session));
        }
        protected void Page_Init(object sender, EventArgs e)
        {

            if (!IsCallback && Request["__CALLBACKID"] == "grid_VeprimeKF" || (Request["__CALLBACKPARAM"] != null && (Request["__CALLBACKPARAM"].Contains("Apply") || Request["__CALLBACKPARAM"].Contains("COLUMNMOVE") || Request["__CALLBACKPARAM"].Contains("ROWVALUES") || Request["__CALLBACKPARAM"].Contains("COLLAPSEROW") || Request["__CALLBACKPARAM"].Contains("EXPANDROW") || Request["__CALLBACKPARAM"].Contains("PAGERONCLICK"))) || (!IsCallback && IsPostBack) || !IsPostBack)
            {
                mbushGridDokumentNgaDB();
                if (!(Request["__CALLBACKPARAM"] != null && (Request["__CALLBACKPARAM"].Contains("COLLAPSEROW") || Request["__CALLBACKPARAM"].Contains("EXPANDROW") || Request["__CALLBACKPARAM"].Contains("ROWVALUES") || Request["__CALLBACKPARAM"].Contains("COLUMNMOVE") || Request["__CALLBACKPARAM"].Contains("PAGERONCLICK"))))
                {
                    konfiguroGride(); DbCore.clsFunksione.konfigGrideListeEMadhePaTheme(grid_VeprimeKF, "IdVeprimeKFKoka");

                }
                // mbushGridDokumentNgaSession();
            }
            grid_VeprimeKF.KeyFieldName = "IdVeprimeKFKoka";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["LoggedIn"].Equals("No"))
            if (!mySessionObjects.isLogedIn(Session))
            {
                Response.Redirect("login.aspx?arsye=FaqePaautorizuar");
            }
            //if (Session["KodiNdermarrjes"] == null)
            int idPerd = mySessionObjects.ktheIdPerdoruesi(Session);
            if (mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerd);
            }
            //oPerdorues = (DbCore.DbAdmin.clsPerdorues)(Session["oClsPerdoruesi"]);
            oPerdorues = mySessionObjects.kthePerdorues(Session);
            percaktoTemplateMenu(ASPxMenu1);
            percaktoTemplateMenu(ASPxMenu1);
            //mbushComboBoxFiltra();
            clsToolbarConfig.mbushComboBoxFiltra(mySessionObjects.merrIdNdermarrjeSesioni(Session), "grid_VeprimeKF", "VeprimeKF.aspx");
            if (!IsPostBack)
            {
                if (Request.QueryString["fshi"] == "po")
                {
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "Fshirja perfundoi me sukses!", pnlMesazhi);
                }
                konfiguroGride();
                //funk.konfiguroMenu(ASPxMenu1);
                //  funk.percaktoTedrejtatPerKeteFaqe("VeprimeKF.aspx", ASPxMenu1);
                grid_VeprimeKF.FilterExpression = "[IdStatusDok]=1";
            }

            //  konfiguroVleraFillestare();
            if (Request.QueryString["indexrow"] != null)
            {
                grid_VeprimeKF.FocusedRowIndex = int.Parse(Request.QueryString["indexrow"]);
            }
        }
        ///// <summary>
        /////      mbush combon e filtrave
        ///// </summary>
        //private void mbushComboBoxFiltra()
        //{

        //    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka("grid_VeprimeKF", "VeprimeKF.aspx", mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //    //DbCore.DbAdmin.clsGridaKoka koka = dbAdmin.merrGridaKokaByEmri("grid_VeprimeKF", "VeprimeKF.aspx", mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //    DbCore.DbAdmin.colFiltratGrida colFiltra = new DbCore.DbAdmin.colFiltratGrida(koka.IdGridaKoka, mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //    colFiltra.Insert(0, new DbCore.DbAdmin.clsFiltraGrida());//colFiltra = dbAdmin.merrGjitheFiltratGridaByGridaKoka(koka.IdGridaKoka, mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //    MenuFilter.colekstioni = colFiltra;
        //    MenuFilter.ValueField = "IdFiltra";
        //    MenuFilter.TextField = "FiltraShenime";
        //}
        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        private void percaktoTemplateMenu(ASPxMenu aSPxMenu1)
        {
            DbCore.clsFunksione funk = new DbCore.clsFunksione( mySessionObjects.ktheCultureInfo(Session));
            DbCore.DbShare.colMenuItem menu = new DbCore.DbShare.colMenuItem(mySessionObjects.ktheGjuhePerdoruesi(Session));
            DbCore.DbAdmin.clsKomponente komp = new DbCore.DbAdmin.clsKomponente("VeprimeKF.aspx");
            menu.merrMenuItemSipasKomponentes(mySessionObjects.ktheGjuhePerdoruesi(Session), komp.IdKomponente);
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejta = komp.merrTeDrejtaPerKeteAmbjent(mySessionObjects.ktheIdPerdoruesi(Session), mySessionObjects.merrIdNdermarrjeSesioni(Session), mySessionObjects.ktheNdermarrjeVit(Session));
            //objekti qe mban metodat per manipulimin dhe konfigurimin e menuse
            clsToolbarConfig konfigMenu = new clsToolbarConfig();
            foreach (DbCore.DbShare.clsMenuItem m in menu)
            {
                bool enabled = true;
                if (m.Name != "ItemFilter" && m.Name != "ItemFrame")
                {
                    if (tedrejta.DFsh == false && m.Name == "Fshi")
                        enabled = false;
                    if (tedrejta.DMod == false && (m.Name == "Ruaj" || m.Name == "Shto" || m.Name == "Modifiko" || m.Name == "Klono"))
                        enabled = false;
                    if (m.IdPrindi != 0)
                    {
                        DbCore.DbShare.clsMenuItem menuprind = new DbCore.DbShare.clsMenuItem(mySessionObjects.ktheGjuhePerdoruesi(Session), m.IdPrindi);
                        clsToolbarConfig.ShtoMenuSubItem(aSPxMenu1, m, menuprind, enabled);
                    }
                    else clsToolbarConfig.ShtoMenuItem(aSPxMenu1, m, enabled);
                }
                else
                {
                    //krijohen handler per te caktuar evente server side per kontrolle
                    //keto handler i kalohen si parametra user control per filtrat
                    EventHandler handlerPerRuajFilter = new EventHandler(Ruaj_ASPxButton_Click);

                    EventHandler handlerPerFshiFilter = new EventHandler(FshiFilter_ASPxButton_Click);
                    //shtohet ne menu user control per filtrat e grides
                    clsToolbarConfig.ShtoMenuItemPerFilter(this, aSPxMenu1, handlerPerRuajFilter, handlerPerFshiFilter);
                } if (m.Name == "ItemFrame")
                    clsToolbarConfig.ShtoMenuItemPerFrame(this, aSPxMenu1, DbCore.clsFunksione.ktheUrlHelpi(komp.UrlHelpSuffix));

                if (m.Name == "Shto" || m.Name == "Ndihme" || m.Name == "ItemFilter" || m.Name == "Grupo" || m.Name == "ItemFrame")
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].BeginGroup = true;
            }
            clsMenuInfo.ShtoMenuItemInfo(this, MenuInfo);

        }
        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(ASPxMenu1);
        }
        private void konfiguroVleraFillestare()
        {
            DbCore.DbRegjistrim.colVeprimeKFKoka colKoka = new DbCore.DbRegjistrim.colVeprimeKFKoka();
            colKoka.mbushVeprimeKFKoka(mySessionObjects.ktheNdermarrjeVit(Session));
            grid_VeprimeKF.DataSource = colKoka;
            grid_VeprimeKF.DataBind();
        }
        private void mbushGridDokumentNgaSession()
        {
            DataTable tmpObject;
            bool sukses = mySessionObjects.merrGrideNgaSessioni(Session, out tmpObject);
            if (!sukses)
                mbushGridDokumentNgaDB();
            else
            {
                grid_VeprimeKF.DataSource = tmpObject;
                grid_VeprimeKF.DataBind(); grid_VeprimeKF.KeyFieldName = "IdVeprimeKFKoka"; tmpObject.Dispose();
            }
        }
        private void mbushGridDokumentNgaDB()
        {//mbush griden e popupit me te dhena  
            DataTable dt = new DataTable();
            dt = DbCore.DbRegjistrim.colVeprimeKFKoka.merrVeprimeKFDT(mySessionObjects.ktheNdermarrjeVit(Session), mySessionObjects.ktheIdPerdoruesi(Session));

            mySessionObjects.ruajGrideNeSession(Session, dt);
            grid_VeprimeKF.DataSource = dt;
            grid_VeprimeKF.DataBind();            
            grid_VeprimeKF.KeyFieldName = "IdVeprimeKFKoka";
            dt.Dispose();
        }
        private void konfiguroGride()
        {
            shtoNivel();
            shtoModel();
            //shtoKlient();
            shtoMonedhe();
            DbCore.clsFunksione.percaktoVisibleColumnsMeWidth(mySessionObjects.merrIdNdermarrjeSesioni(Session), grid_VeprimeKF, "grid_VeprimeKF", "VeprimeKF.aspx");
            //    funk.konfiguroGrideListeMadhe(grid_VeprimeKF, "IdVeprimeKFKoka");
            GridViewDataTextColumn col3 = grid_VeprimeKF.Columns["Vlefta"] as GridViewDataTextColumn;
            col3.PropertiesEdit.DisplayFormatString = "0.00";
            shtoStatus();
            this.grid_VeprimeKF.Columns["#"].VisibleIndex = 0;
        }

        private void shtoNivel()
        {
            //DbCore.DbRegjistrim.clsDatabaseRegjistrim dbRegjistrim = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
            grid_VeprimeKF.Columns.Remove(grid_VeprimeKF.Columns["IdNivel"]);
            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            DbCore.DbRegjistrim.colNivelRegjistrimi nivelet = new DbCore.DbRegjistrim.colNivelRegjistrimi();
            nivelet.mbushGjitheNivelRegjistrimiSipasKategori(20, mySessionObjects.merrIdNdermarrjeSesioni(Session), oPerdorues.IdPerdorues);
            //nivelet =  dbRegjistrim.merrGjitheNivelRegjistrimiSipasKategori(20,mySessionObjects.merrIdNdermarrjeSesioni(Session), oPerdorues.IdPerdorues);
            colnew.PropertiesComboBox.DataSource = nivelet;
            colnew.PropertiesComboBox.TextField = "Pershkrimi";
            colnew.PropertiesComboBox.ValueField = "IdNivel";
            colnew.FieldName = "IdNivel";
            grid_VeprimeKF.Columns.Add(colnew);
        }
        private void shtoModel()
        {
            //DbCore.DbRegjistrim.clsDatabaseRegjistrim dbRegjistrim = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
            grid_VeprimeKF.Columns.Remove(grid_VeprimeKF.Columns["IdKonfigAmbjente"]);
            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            //DbCore.DbShare.clsDatabaseShare share = new DbCore.DbShare.clsDatabaseShare();
            DbCore.DbShare.colKonfigurimAmbjenti colKonfig = new DbCore.DbShare.colKonfigurimAmbjenti();
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konf.IdKategori = 20;
            konf.IdNdermarje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
            colKonfig.Add(new DbCore.DbShare.clsKonfigurimAmbjenti(0, "", "", 1, 0, true, 0, 0, 0, 0, 0, 0));
            colKonfig.mbushKonfigAmbjSipasIdKategori(konf.IdKategori, konf.IdNdermarje, mySessionObjects.ktheIdPerdoruesi(Session));
            //colKonfig = share.merrKonfigAmbjSipasIdKategori(konf, mySessionObjects.ktheIdPerdoruesi(Session));
            colnew.PropertiesComboBox.DataSource = colKonfig;
            colnew.PropertiesComboBox.TextField = "KodKonfigAmbjente";
            colnew.PropertiesComboBox.ValueField = "IdKonfigAmbjente";
            colnew.FieldName = "IdKonfigAmbjente";
            grid_VeprimeKF.Columns.Add(colnew);
        }
        private void shtoStatus()
        {
            DbCore.DbArkaBanka.clsDatabaseArkaBanka data = new DbCore.DbArkaBanka.clsDatabaseArkaBanka();
            grid_VeprimeKF.Columns.Remove(grid_VeprimeKF.Columns["IdStatusDok"]);
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
            colnew.FieldName = "IdStatusDok";
            colnew.Caption = "Statusi";
            grid_VeprimeKF.Columns.Add(colnew);
        }

        private void shtoMonedhe()
        {
            grid_VeprimeKF.Columns.Remove(grid_VeprimeKF.Columns["IdMonedha"]);
            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            DbCore.DbAdmin.colMonedhat colMonedhat = new DbCore.DbAdmin.colMonedhat();
            colMonedhat.Add(new DbCore.DbAdmin.clsMonedha(0, "", "", true, 0, 0, 0, 0, 0));
            colMonedhat.mbushGjitheMonedhatAktive(mySessionObjects.merrIdNdermarrjeSesioni(Session), mySessionObjects.ktheIdPerdoruesi(Session));
            //DbCore.DbAdmin.colMonedhat colMonedhat = dbAdmin.merrGjitheMonedhatAktive(mySessionObjects.merrIdNdermarrjeSesioni(Session), mySessionObjects.ktheIdPerdoruesi(Session));
            colnew.PropertiesComboBox.DataSource = colMonedhat;
            colnew.PropertiesComboBox.TextField = "KodiMonedha";
            colnew.PropertiesComboBox.ValueField = "IdMonedha";
            colnew.FieldName = "IdMonedha";
            grid_VeprimeKF.Columns.Add(colnew);
        }

        private void shtoKlient()
        {
            //DbCore.DbKontabiliteti.clsDatabaseKontabilitet dbKontab = new DbCore.DbKontabiliteti.clsDatabaseKontabilitet();
            grid_VeprimeKF.Columns.Remove(grid_VeprimeKF.Columns["IdKF"]);
            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            DbCore.DbKontabiliteti.colKlienteFurnitore colKlientet = new DbCore.DbKontabiliteti.colKlienteFurnitore();
            colKlientet.Add(new DbCore.DbKontabiliteti.clsKlientFurnitor(0, "", 0, false, 0, "", "", "", "", 0, "", "", "", "", "", "", "", "", true, 0, 0, 0, 0, 0, 0, 0, 0, 0, "", "", false, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "", "", 0, "", 0, 0, 0, ""));
            colKlientet.mbushKlienteFurnitoreNdermarrjes(mySessionObjects.merrIdNdermarrjeSesioni(Session));
            //DbCore.DbKontabiliteti.colKlienteFurnitore colKlientet = dbKontab.merrKlienteFurnitoreNdermarrjes(mySessionObjects.merrIdNdermarrjeSesioni(Session));

            colnew.PropertiesComboBox.DataSource = colKlientet;
            colnew.PropertiesComboBox.TextField = "EmertimiKF";
            colnew.PropertiesComboBox.ValueField = "IdKlientFurnitor";
            colnew.FieldName = "IdKF";
            grid_VeprimeKF.Columns.Add(colnew);
        }

        protected void grid_VeprimeKF_DataBound(object sender, EventArgs e)
        {
            if (this.grid_VeprimeKF.Columns["#"] == null)
            {
                DevExpress.Web.GridViewCommandColumn check = new DevExpress.Web.GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true; check.Width = Unit.Percentage(2);
                //  check.SetColVisibleIndex(0);
                grid_VeprimeKF.Settings.ShowFilterRow = true;
                grid_VeprimeKF.Settings.ShowHeaderFilterButton = true;
                grid_VeprimeKF.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                grid_VeprimeKF.Settings.ShowFilterRowMenu = true;
                grid_VeprimeKF.Columns.Add(check);
                grid_VeprimeKF.Settings.ShowGroupPanel = true;
                grid_VeprimeKF.KeyFieldName = "IdVeprimeKFKoka";
                grid_VeprimeKF.SettingsBehavior.AllowSelectByRowClick = false;
                grid_VeprimeKF.SettingsBehavior.AllowFocusedRow = true;
            }// this.grid_VeprimeKF.Columns["#"].VisibleIndex = 0;
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
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;

            DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
            //filtra.mbushFiltraGridaSipasFiltraEmri(cmbFiltra.Text, mySessionObjects.merrIdNdermarrjeSesioni(Session));
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka("grid_VeprimeKF", "VeprimeKF.aspx", mySessionObjects.merrIdNdermarrjeSesioni(Session));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
            //DbCore.DbAdmin.clsFiltraGrida filtra = dbAdmin.merrFiltraGridaSipasFiltraEmri(cmbFiltra.Text, mySessionObjects.merrIdNdermarrjeSesioni(Session));
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = mySessionObjects.ktheIdPerdoruesi(Session);
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();
                //mbushComboBoxFiltra();
                clsToolbarConfig.mbushComboBoxFiltra(mySessionObjects.merrIdNdermarrjeSesioni(Session), "grid_VeprimeKF", "VeprimeKF.aspx");
                percaktoTemplateMenu(ASPxMenu1);
                if (mesazh.StatusMesazhi == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                //   konfiguroVleraFillestare();

                grid_VeprimeKF.FilterExpression = "[IdStatusDok]=1";

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
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;

            DbCore.DbAdmin.clsFiltraGrida filtri = new DbCore.DbAdmin.clsFiltraGrida();
            filtri.FiltraKodi = cmbFiltra.Text;
            filtri.FiltraShenime = cmbFiltra.Text;
            filtri.FiltraUniversal = false;// Universal_ASPxCheckBox.Checked;
            //DbCore.DbAdmin.clsGridaKoka koka = dbAdmin.merrGridaKokaByEmri("grid_VeprimeKF", "VeprimeKF.aspx", mySessionObjects.merrIdNdermarrjeSesioni(Session));
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka("grid_VeprimeKF", "VeprimeKF.aspx", mySessionObjects.merrIdNdermarrjeSesioni(Session));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = grid_VeprimeKF.FilterExpression;
            System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = grid_VeprimeKF.GetSortedColumns();
            if (kolona.Count > 0)
            {
                filtri.KoloneRenditje = kolona[0].FieldName;
                if (kolona[0].SortOrder == DevExpress.Data.ColumnSortOrder.Ascending)
                    filtri.DrejtimRenditje = true;
                else
                    filtri.DrejtimRenditje = false;
            }
            else
            {
                filtri.KoloneRenditje = "NrDok";
                filtri.DrejtimRenditje = true;
            }
            DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();
            //oPerdorues = (DbCore.DbAdmin.clsPerdorues)(Session["oClsPerdoruesi"]);
            oPerdorues = mySessionObjects.kthePerdorues(Session);
            filtri.IdPerdoruesi = oPerdorues.IdPerdorues;
            filtri.IdNdermarje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            filtri.IdStatusDok = 1;

            mesazh = filtri.ruaj();
            //mbushComboBoxFiltra();
            clsToolbarConfig.mbushComboBoxFiltra(mySessionObjects.merrIdNdermarrjeSesioni(Session), "grid_VeprimeKF", "VeprimeKF.aspx");
            percaktoTemplateMenu(ASPxMenu1);
            if (mesazh.StatusMesazhi == true)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

            cmbFiltra.Text = "";
        }


        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {
            //if (e.Item.Name == "Shto")
            //    {
            //    Response.Redirect("Shto_VeprimeKF.aspx?shtim_modifikim=shtim");
            //    }
            //else if (e.Item.Name == "Modifiko")
            //    {
            //    int indeksi = grid_VeprimeKF.FocusedRowIndex;
            //    string id;

            //    if (grid_VeprimeKF.GetRowValues(indeksi, "IdVeprimeKFKoka") != null)
            //        id = grid_VeprimeKF.GetRowValues(indeksi, "IdVeprimeKFKoka").ToString();
            //    else id = null;

            //    Response.Redirect("Shto_VeprimeKF.aspx?id=" + id + "&indexrow=" + grid_VeprimeKF.FocusedRowIndex + "&shtim_modifikim=modifikim");
            //    }
        }

        //fshin rreshtat e selektuar
        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            //clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "", pnlMesazhi);

            string[] rreshtat = hfReshtaTeSelektuar.Value.ToString().Split(','); //= grid_RegDok.GetSelectedFieldValues("IdShitjeKoka");
            if (rreshtat.Length == 1 && rreshtat[0] == "")
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhZgjidhniNje, pnlMesazhi);
                return;
            }
            List<string> TeFshire = new List<string>(), TeLidhur = new List<string>(), PeriudheKycur = new List<string>();
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            DbCore.DbAdmin.clsPeriudhaKontabel periudha;
            int idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
           
            foreach (string id in rreshtat)
            {
                DbCore.DbRegjistrim.clsVeprimeKFKoka kok = new DbCore.DbRegjistrim.clsVeprimeKFKoka(Convert.ToInt16(id));

                bool lidhur = dbAdmin.eshteDokumentiILidhur(kok.IdVeprimeKFKoka, kok.IdNivel, "T_VEPRIMEKFKOKA", "IDVEPRIMKFKOKA");

                if (lidhur)
                {
                    TeLidhur.Add(kok.NrDok);
                    continue;
                }
                if (kok.IdStatusDok == 2)
                    continue;

                periudha = new DbCore.DbAdmin.clsPeriudhaKontabel(kok.DtDok, idNdermarrje);
                DbCore.clsMesazh mesazhi = periudha.isPeriudheKycur();
                if (mesazhi.StatusMesazhi)
                {
                    PeriudheKycur.Add(kok.NrDok);
                    // clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhGabimi, pnlMesazhi);
                    continue;
                }
                //DbCore.DbAdmin.clsPeriudhaKontabel periudha = new DbCore.DbAdmin.clsPeriudhaKontabel();
                //periudha.KodiViti = kok.DtDok.Date.Year.ToString();
                //DbCore.DbAdmin.clsViti vit = new DbCore.DbAdmin.clsViti();
                //vit.mbushVitetMet(periudha.KodiViti, mySessionObjects.merrIdNdermarrjeSesioni(Session));
                //periudha.IdViti = vit.IdViti;
                //DbCore.DbAdmin.colPeriudhaKontabel periudhat = new DbCore.DbAdmin.colPeriudhaKontabel();
                //periudhat.merrSipasViti(periudha.IdViti);

                //bool perketPeriudheKycur = false;
                //for (int i = 0; i < periudhat.Count; i++)
                //{
                //    if (kok.DtDok >= periudhat[i].FillimiPeriudha && kok.DtDok <= periudhat[i].MbarimiPeriudha.AddDays(1))
                //    {
                //        if (periudhat[i].Ekycur)
                //        {
                //            PeriudheKycur.Add(kok.NrDok);
                //            perketPeriudheKycur = true;
                //            break;
                //        }
                //    }
                //}
                //if (perketPeriudheKycur)
                //    continue;

                kok.IdPerdoruesi = mySessionObjects.ktheIdPerdoruesi(Session);
                mesazh = kok.fshiVeprimKF();
                if (mesazh.StatusMesazhi)
                {
                    #region Heq veprimet kl/furn nga grida

                    // ASPxGridView_Artikull.DataSource = dt;
                    hiqDokumentNgaGrida(kok.IdVeprimeKFKoka);
                    #endregion
                    TeFshire.Add(kok.NrDok);

                }
            }
            dbAdmin.Dispose();
            string mesazhInfoGabimLidhur = "", mesazhInfoGabimPeridheKycur = "", mesazhInfoSukses = "";
            //if (rreshtat.Length == 0)
            //    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ju lutem zgjidhni te pakten nje dokument!", pnlMesazhi);
            //else
            //{
                if (TeLidhur.Count == 1)
                    mesazhInfoGabimLidhur = String.Format("{0}{1}{2}", prefixMesazhNjejes, String.Join(";", TeLidhur), suffixMesazhNjejesLidhurGabimi);
                else
                    if (TeLidhur.Count > 1)
                        mesazhInfoGabimLidhur = String.Format("{0}{1}{2}", prefixMesazhShumes, String.Join(";", TeLidhur), suffixMesazhShumesLidhurGabimi);
                if (PeriudheKycur.Count == 1)
                    mesazhInfoGabimPeridheKycur = String.Format("{0}{1}{2}", prefixMesazhNjejes, String.Join(";", PeriudheKycur), suffixMesazhNjejesPeriudheKycurGabimi);
                else
                    if (PeriudheKycur.Count > 1)
                        mesazhInfoGabimPeridheKycur = String.Format("{0}{1}{2}", prefixMesazhShumes, String.Join(";", PeriudheKycur), suffixMesazhShumesPeriudheKycurGabimi);

                if (TeFshire.Count == 1)
                    mesazhInfoSukses = String.Format("{0}{1}{2}", prefixMesazhNjejes, String.Join(";", TeFshire), suffixMesazhNjejesSuksesi);
                else
                    if (TeFshire.Count > 1)
                        mesazhInfoSukses = String.Format("{0}{1}{2}", prefixMesazhShumes, String.Join(";", TeFshire), suffixMesazhShumesSuksesi);

                mesazhInfoGabimLidhur += mesazhInfoGabimPeridheKycur;
                if (mesazhInfoGabimLidhur != "" && mesazhInfoSukses != "")
                    mesazhInfoGabimLidhur += lidhesMesazhi + mesazhInfoSukses;
                if (mesazhInfoGabimLidhur != "")
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhInfoGabimLidhur, pnlMesazhi);
                else
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazhInfoSukses, pnlMesazhi);
            //}

        }
        private void hiqDokumentNgaGrida(int idkoka)
        {
            if (this.grid_VeprimeKF.DataSource != null)
            {
                DataTable dt = (DataTable)grid_VeprimeKF.DataSource;
                DataRow[] drs = dt.Select("IdVeprimeKFKoka = " + idkoka);
                if (drs.Length > 1)
                    throw new Exception("GABIM: Ndodhen 2 dokumenta me te njejten id ne gride");
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                dt.Rows.Remove(dr);
                grid_VeprimeKF.DataSource = dt;
                grid_VeprimeKF.DataBind();
                dt.Dispose();
            }
            else mbushGridDokumentNgaDB();
        }

        protected void grid_VeprimeKF_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            if (e.CallbackName == "APPLYFILTER" && e.Args[0] == "")
            {
                DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
                ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
                cmbFiltra.Text = "";


            }
            mbushGridDokumentNgaDB(); DbCore.clsFunksione.konfigGrideListeEMadhePaTheme(grid_VeprimeKF, "IdVeprimeKFKoka");

        }

        protected void grid_VeprimeKF_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                if (arr[2] == "")
                    grid_VeprimeKF.FilterExpression = "[IdStatusDok]=1";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    //filtra.mbushFiltraGridaSipasFiltraKodi(arr[2], mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka("grid_VeprimeKF", "VeprimeKF.aspx", mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
                    //DbCore.DbAdmin.clsFiltraGrida filtra = dbAdmin.merrFiltraGridaSipasFiltraKodi(arr[2], mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    if (filtra.FiltraKodi != null)
                    {
                        grid_VeprimeKF.FilterExpression = filtra.FiltraVlera;
                        if (filtra.DrejtimRenditje == true)
                            grid_VeprimeKF.SortBy(grid_VeprimeKF.Columns[filtra.KoloneRenditje], DevExpress.Data.ColumnSortOrder.Ascending);
                        else
                            grid_VeprimeKF.SortBy(grid_VeprimeKF.Columns[filtra.KoloneRenditje], DevExpress.Data.ColumnSortOrder.Descending);

                        //   konfiguroVleraFillestare();
                    }
                }
            }

            DbCore.clsFunksione funksion = new DbCore.clsFunksione( mySessionObjects.ktheCultureInfo(Session));
            //     konfiguroGride();
            grid_VeprimeKF.Selection.UnselectAll();
        }

        protected void grid_VeprimeKF_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = grid_VeprimeKF.PageIndex;
            e.Properties["cpPageRow"] = grid_VeprimeKF.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = grid_VeprimeKF.VisibleRowCount;
        }

        protected void grid_VeprimeKF_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if (e.Column.FieldName == "IdKF" ||
                  e.Column.FieldName == "IdMonedha" || e.Column.FieldName == "IdKonfigAmbjente" ||
                    e.Column.FieldName == "IdNivel")
                if (Convert.ToInt32(Convert.ToDouble(e.Value.ToString())) == 0)
                {
                    e.Criteria = null;
                }
        }

        protected void grid_VeprimeKF_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            if (e.Column.FieldName == "NrDok")
            {
                e.Values.Clear();
                e.AddValue("(Te gjithe)", string.Empty, "true");
                e.AddValue("Nga A-D ", string.Empty, e.Column.FieldName + ">'A     ' and " + e.Column.FieldName + " <'DDDDDDD'");
                e.AddValue("Nga D-G ", string.Empty, e.Column.FieldName + ">'D     ' and " + e.Column.FieldName + "<'GGGGGGG'");
                e.AddValue("Nga H-K ", string.Empty, e.Column.FieldName + ">'H     ' and " + e.Column.FieldName + "<'KKKKKKK'");
                e.AddValue("Nga L-O ", string.Empty, e.Column.FieldName + ">'L     ' and " + e.Column.FieldName + "  <'OOOOOOO'");
                e.AddValue("Nga P-S ", string.Empty, e.Column.FieldName + ">'P     ' and " + e.Column.FieldName + "<'SSSSSSS'");
                e.AddValue("Nga T-W ", string.Empty, e.Column.FieldName + ">'T     ' and " + e.Column.FieldName + "<'WWWWWWW'");
                e.AddValue("Nga X-Z ", string.Empty, e.Column.FieldName + ">'X     ' and " + e.Column.FieldName + "<'ZZZZZZZ'");
            }
            else
            {
                e.Values.Clear();
                e.AddValue("(Te gjithe)", string.Empty, "true");
            }
        }
    }
}
