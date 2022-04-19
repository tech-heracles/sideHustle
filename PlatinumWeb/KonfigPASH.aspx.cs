using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using DbCore;
using DbCore.DbAdmin;
using DbCore.DbKontabiliteti;
using DbCore.IMBUtils.Messages;
using DevExpress.Data;
using DevExpress.Web;
using PlatinumWeb.ApplicationUtils.Pages;
using PlatinumWeb.Templates;
using clsTrupPasqyreFinanciare = DbCore.DbKontabiliteti.clsTrupPasqyreFinanciare;
using colTrupPasqyreFinaciare = DbCore.DbKontabiliteti.colTrupPasqyreFinaciare;

namespace PlatinumWeb
{
    /// <summary>
    /// Kjo klase sherben per te shfaqur listen e konfigurimeve te pash
    /// </summary>
    public partial class KonfigPASH : MyPageBase
    {
        /// <summary>
        /// thirret kur faqja lodohet. Ne te behet kontrolli nese perdoruesi eshte i loguar ne sistem  dhe nqs jo ridrejtohet tek forma e logimit
        /// thirret inicializimi i konfigurimeve fillestare te faqes
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e"> argumenti</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!mySessionObjects.isLogedIn(Session))
                clsFunksione.logout(Session, true, "FaqePaautorizuar");
            
            if (mySessionObjects.ktheKodNdermarrje(Session) == null)
                Response.Redirect("Login_Ndermarrje.aspx?id=" + IdPerdoruesi);
            
            PercaktoTemplateMenu();

            if (!IsPostBack)
            {
                hfKthehu.Value = "kthehu";
                EmrateTabeve();
                PerktheLabel();
                ShtoVleraTePergjithshmeNeHfState();
                ASPxPageControl1.ActiveTabIndex = 0;
                KonfiguroVleraFillestare();
                MbushListeBuxhetesh();
                KonfiguroBuxhetGride();
                KonfiguroGridenZerat();
                KonfiguroGridenLlogarite();
                KonfiguroGride();
                metoda_ASPxComboBox.Items.Add("Direkt");
                metoda_ASPxComboBox.Items.Add("Indirekt");
                clsTeDrejtaRoli tedrejtaInfo = new clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, clsFunksione.GetKomponente(Page.Request));
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
            }

            clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, IdNdermarrja, "gvKonfigPASH", 1, "KonfigPASH.aspx?lloji=Pash");
            PercaktoTemplateBuxhetet();
        }

        /// <summary>
        /// Vendos emrat e tabeve ne baze te gjuhes se perdoruesit
        /// </summary>
        private void EmrateTabeve()
        {
            ASPxPageControl1.TabPages[0].Text = rm.GetString("labelAdministrimiTePergjithshme", ci);
            ASPxPageControl1.TabPages[1].Text = rm.GetString("labelKartela", ci);
            ASPxPageControl1.TabPages[2].Text = rm.GetString("llogariteCashTab", ci);
            ASPxPageControl1.TabPages[3].Text = rm.GetString("fluksShfrytezimiTab", ci);
            ASPxPageControl1.TabPages[4].Text = rm.GetString("fluksInvestuesTab", ci);
            ASPxPageControl1.TabPages[5].Text = rm.GetString("fluksFinanciar", ci);
            ASPxPageControl1.TabPages[6].Text = rm.GetString("buxhetetTab", ci);
        }
        
        public void PerktheLabel()
        {
            kodi_ASPxLabel.Text = rm.GetString("lblKodi", ci);
            emertimi_ASPxLabel.Text = rm.GetString("lblEmertimi", ci);
            ASPxLabel1.Text = rm.GetString("lblMetoda", ci);
        }

        private void PercaktoTemplateMenu() =>
            clsToolbarConfig.percaktoTemplateMenu(IdGjuha, IdViti, IdPerdoruesi, IdNdermarrja, ASPxMenu1, clsFunksione.GetKomponente(Page.Request), this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, hfShtimModifikim.Value != "modifikim", false, false, Meme);
        
        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e) => PercaktoTemplateMenu();

        /// <summary>
        /// perdoret per te inicializuar griden me konfigurimet e pash sipas ndermarjes dhe autorizimeve te perdoruesit
        /// </summary>
        ///  :  <see cref="DbCore.DbKontabiliteti.clsDatabaseKontabilitet.merrPasqyraFinaciareSipasTipit(tipi,idndermarje, idndermarjeviti)"/> 
        private void KonfiguroVleraFillestare()
        {
            switch (Request.QueryString["lloji"])
            {
                case "Pash":
                    gvKonfigPASH.DataSource = new colPasqyratFinaciare("PASH", IdNdermarrja);
                    break;
                case "Buxhetor":
                    gvKonfigPASH.DataSource = new colPasqyratFinaciare("Buxhetor", IdNdermarrja);
                    break;
                case "Bilanc":
                    gvKonfigPASH.DataSource = new colPasqyratFinaciare("Bilanc", IdNdermarrja);
                    break;
                case "PashOJF":
                    gvKonfigPASH.DataSource = new colPasqyratFinaciare("PashOJF", IdNdermarrja);
                    break;
                case "CashflowOJF":
                    gvKonfigPASH.DataSource = new colPasqyratFinaciare("CashflowOJF", IdNdermarrja);
                    break;
                case "BilancOJF":
                    gvKonfigPASH.DataSource = new colPasqyratFinaciare("BilancOJF", IdNdermarrja);
                    break;
                default:
                    gvKonfigPASH.DataSource = new colPasqyratFinaciare("Cash Flow", IdNdermarrja);
                    break;
            }
            
            gvKonfigPASH.DataBind();
        }

        /// <summary>
        /// perdoret per te shtuar kolonen e selektimit tek grida dhe per te vendosur disa karakteristika te grides
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void gvKonfigPASH_DataBound(object sender, EventArgs e)
        {
            if (gvKonfigPASH.Columns["#"] != null) return;

            var check = new GridViewCommandColumn("#")
            {
                ShowSelectCheckbox = true,
                Width = Unit.Percentage(2)
            };

            gvKonfigPASH.Settings.ShowFilterRow = true;
            gvKonfigPASH.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
            gvKonfigPASH.Settings.ShowFilterRowMenu = true;
            gvKonfigPASH.Columns.Add(check);
            gvKonfigPASH.KeyFieldName = "IdPasqyresFin";
            gvKonfigPASH.SettingsBehavior.AllowSelectByRowClick = true;
            gvKonfigPASH.SettingsBehavior.AllowFocusedRow = true;
        }

        /// <summary>
        /// perdoret per te konfiguruar griden. Per te dhenat mbi kolonat qe do te shfaqen nga databaza si dhe percakton disa karakteristika te grides
        /// </summary>
        /// :  <see cref="GridUtil.percaktoVisibleColumns"/>
        /// :  <see cref="DbCore.clsFunksione.konfiguroGrideListeMadhe(grida,celesigrides)"/>
        private void KonfiguroGride()
        {
            GridUtil.percaktoVisibleColumnsMeWidth(IdGjuha, IdNdermarrja, gvKonfigPASH, "gvKonfigPASH", "KonfigPASH.aspx?lloji=Pash");
            GridUtil.konfigGrideListeEMadhePaTheme(gvKonfigPASH, "IdPasqyresFin");
            gvKonfigPASH.Columns["#"].VisibleIndex = 0;
        }

        /// <summary>
        /// perdoret ne rastet kur grida ben callback per ta rimbushur griden
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void gvKonfigPASH_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            if (e.CallbackName == "APPLYFILTER" && e.Args[0] == "")
            {
                var itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
                var cmbFiltra = ((MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
                cmbFiltra.Text = "";
            }

            KonfiguroVleraFillestare();
        }
        
        /// <summary>
        /// perdoret per te vendosur filtrat tek headeri i grides
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvKonfigPASH_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            if (e.Column.FieldName == "KodiPasqyresFin" || e.Column.FieldName == "EmertimiPasqyresFin")
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

        /// <summary>
        /// fshin filtrin e zgjedhur dhe aplikuar mbi gride, fshirje nga DB ku ai eshte ruajtur, dhe jo pastrimi i grides nga aplikimi i filtrit
        /// per kete do sherbeje Clear Filter ne fund te grides
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
            var itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            var cmbFiltra = ((MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            var filtra = new clsFiltraGrida();
            var koka = new clsGridaKoka(IdGjuha, "gvKonfigPASH", "KonfigPASH.aspx?lloji=Pash", IdNdermarrja);
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, IdNdermarrja, koka.IdGridaKoka);

            if (filtra.FiltraKodi == null) return;

            filtra.IdPerdoruesi = IdPerdoruesi;
            var mesazh = new clsMesazh();
            mesazh = filtra.fshi();
            clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, IdNdermarrja, "gvKonfigPASH", 1, "KonfigPASH.aspx?lloji=Pash");
            PercaktoTemplateMenu();

            if (mesazh.Status)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

            cmbFiltra.Text = "";
            KonfiguroVleraFillestare();
            hfStatusi.Value = "true";

            gvKonfigPASH.FilterExpression = string.Empty;
        }

        /// <summary>
        /// sherben per te ruajtur filtrin e zgjedhur
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
            var itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            var cmbFiltra = ((MenuFilter)itemButton.Template).FindControl("btnFiltra") as ASPxComboBox;

            var filtri = new clsFiltraGrida
            {
                FiltraKodi = cmbFiltra.Text,
                FiltraShenime = cmbFiltra.Text,
                FiltraUniversal = false
            };
            
            var koka = new clsGridaKoka(IdGjuha, "gvKonfigPASH", "KonfigPASH.aspx?lloji=Pash", IdNdermarrja);
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gvKonfigPASH.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("KodiPasqyresFin", gvKonfigPASH);
            //var kolona = gvKonfigPASH.GetSortedColumns();
            //if (kolona.Count > 0)
            //{
            //    filtri.KoloneRenditje = kolona[0].FieldName;
            //    filtri.DrejtimRenditje = kolona[0].SortOrder == ColumnSortOrder.Ascending;
            //}
            //else
            //{
            //    filtri.KoloneRenditje = "KodiPasqyresFin";
            //    filtri.DrejtimRenditje = true;
            //}

            filtri.IdPerdoruesi = IdPerdoruesi;
            filtri.IdNdermarje = IdNdermarrja;

            var mesazh = new clsMesazh();
            filtri.IdStatusDok = 1;

            mesazh = filtri.ruaj();
            clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, IdNdermarrja, "gvKonfigPASH", 1, "KonfigPASH.aspx?lloji=Pash");
            PercaktoTemplateMenu();

            if (mesazh.Status)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

            hfStatusi.Value = "true";
            cmbFiltra.Text = "";
        }

        private void KonfiguroGridenZerat()
        {
            var serializusi = new JavaScriptSerializer { MaxJsonLength = 50000000 };
            hfKolonaGride.Value = serializusi.Serialize(GridUtil.percaktoVisibleColumnsGrid(IdGjuha, IdNdermarrja, "grid_zerat", "KonfigPASH.aspx?lloji=Pash"));
        }

        /// <summary>
        /// perdoret per te marre te dhenat mbi konfigurimin e grides nga databaza dhe ruhen tek hfKolonaSubGride e cila perdoret me pas per te inicializuar subgriden
        /// </summary>
        /// / :<see cref=" :clsFunksione.percaktoVisibleColumnsGrid(emergride, emerkomponente"/>
        /// <param name="idNdermarrje"></param>
        private void KonfiguroGridenLlogarite()
        {
            var serializusi = new JavaScriptSerializer { MaxJsonLength = 50000000 };
            hfKolonaSubGride.Value = serializusi.Serialize(GridUtil.percaktoVisibleColumnsGrid(IdGjuha, IdNdermarrja, "grid_llogarite", "KonfigPASH.aspx?lloji=Pash"));
        }

        /// <summary>
        /// perdoret per te fshire rreshtat e zgjedhur te konfigurimeve te pash nqs perdoruesi konfirmon fshirjen
        /// </summary>
        ///  :  <see cref=DbCore.DbKontabiliteti.clsDatabaseKontabilitet.merrPasqyraFinaciareSipasId(id)"/> 
        ///  :  <see cref="DbCore.DbKontabiliteti.clsDatabaseKontabilitet.fshiPasqyraAndBuxhete()"/> 
        /// <param name="sender"> derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            var rreshtat = gvKonfigPASH.GetSelectedFieldValues("IdPasqyresFin");
            if (rreshtat.Count == 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgZgjidhniNjeKonfigurim"], pnlMesazhi);
                return;
            }

            var mesazh = new clsMesazh();

            foreach (int id in rreshtat)
            {
                var pas = new clsPasqyreFinanciare(id);

                if (pas.Model)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, string.Format(MessagesResource.Messages["mgsPaqyreModel"], pas.KodiPasqyresFin), pnlMesazhi);
                    return;
                }

                pas.IdPerdoruesi = mySessionObjects.ktheIdPerdoruesi(Session);
                mesazh = pas.Fshi();
            }

            if (mesazh.Status)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

            hfStatusi.Value = mesazh.Status.ToString().ToLower();
            KonfiguroVleraFillestare();
        }

        /// <summary>
        /// perdoret per te trajtuar ngjarjet e butonave te menuse. Shkon ne faqen e modifikimit te bankave kur perdoruesi klikon butonin modifiko
        /// ose ne faqen e shtimit te bankave kur perdoruesi klikon butonin shto
        /// </summary>
        /// <param name="source"> derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {
            if (e.Item.Name != "Ruaj") return;
            Page.Validate("entries");
            RuajPash();
        }

        /// <summary>
        /// perdoret per te shtuar karakteristika te grides tek javascripti
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvKonfigPASH_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gvKonfigPASH.PageIndex;
            e.Properties["cpPageRow"] = gvKonfigPASH.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gvKonfigPASH.VisibleRowCount;
        }

        #region Buxhetet

        /// <summary>
        /// perdoret per te mbushur griden e buxheteve me konfigurimin fillestar
        /// </summary>
        /// :<see cref=" :DbCore.DbKontabiliteti.clsDatabaseKontabiliteti.BuxhetFillestar()"/>
        private void MbushListeBuxhetesh()
        {
            grid_buxhetet.DataSource = colBuxhetet.KrijoBuxhetetFillestare(new DateTime(1970, 1, 1));
            grid_buxhetet.DataBind();
        }

        /// <summary>
        /// perdoret per te percaktuar tipet e reja te kolonave te grides sipas templateve
        /// </summary>
        private void PercaktoTemplateBuxhetet()
        {
            var col1 = grid_buxhetet.Columns["Muaj"] as GridViewDataTextColumn;
            col1.DataItemTemplate = new MyLabelTemplate();
            var col2 = grid_buxhetet.Columns["Gjendja"] as GridViewDataTextColumn;
            col2.DataItemTemplate = new MyLabelTemplate();
            var col3 = grid_buxhetet.Columns["Buxheti_1"] as GridViewDataTextColumn;
            col3.DataItemTemplate = new MyDoubleTemplate(false, 2, "0"); 
            var col4 = grid_buxhetet.Columns["Buxheti_2"] as GridViewDataTextColumn;
            col4.DataItemTemplate = new MyDoubleTemplate(false, 2, "0"); 
            var col5 = grid_buxhetet.Columns["Diferenca_1"] as GridViewDataTextColumn;
            col5.ReadOnly = true;
            col5.DataItemTemplate = new MyReadOnlyTextTemplate();
            var col6 = grid_buxhetet.Columns["Diferenca_2"] as GridViewDataTextColumn;
            col6.ReadOnly = true;
            col6.DataItemTemplate = new MyReadOnlyTextTemplate();
        }

        /// <summary>
        /// perdoret per te konfiguruar griden. Per te dhenat mbi kolonat qe do te shfaqen nga databaza si dhe percakton disa karakteristika te grides
        /// </summary>
        /// :  <see cref="GridUtil.percaktoVisibleColumns"/>
        /// :  <see cref="DbCore.clsFunksione.konfiguroGrideRegjistrimEvogel(grida,celesigrides)"/>
        private void KonfiguroBuxhetGride()
        {
            GridUtil.percaktoVisibleColumnsMeWidth(IdGjuha, IdNdermarrja, grid_buxhetet, "grid_buxhetet", "KonfigPASH.aspx?lloji=Pash");
            GridUtil.konfiguroGrideRegjistrimEvogelPaTheme(grid_buxhetet, "IdBuxheti");
            grid_buxhetet.Settings.UseFixedTableLayout = false;
        }
        
        /// <summary>
        /// perdoret per t'iu shtuar karakteristika reshtave te grides gjate krijimit te tyre
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e">argumentat</param>
        protected void grid_buxhetet_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType != GridViewRowType.Data) return;

            var colGjendja = ((ASPxGridView)sender).Columns["Gjendja"] as GridViewDataColumn;
            var colBuxh1 = ((ASPxGridView)sender).Columns["Buxheti_1"] as GridViewDataColumn;
            var colBuxh2 = ((ASPxGridView)sender).Columns["Buxheti_2"] as GridViewDataColumn;
            var colDiff1 = ((ASPxGridView)sender).Columns["Diferenca_1"] as GridViewDataColumn;
            var colDiff2 = ((ASPxGridView)sender).Columns["Diferenca_2"] as GridViewDataColumn;
            var lblGjendja = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, colGjendja, "lbl") as ASPxLabel;
            var txtBuxh1 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, colBuxh1, "txtBox") as ASPxTextBox;
            var txtBuxh2 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, colBuxh2, "txtBox") as ASPxTextBox;
            var lblDiff1 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, colDiff1, "txtBox") as ASPxTextBox;
            var lblDiff2 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, colDiff2, "txtBox") as ASPxTextBox;

            if (txtBuxh1 == null || txtBuxh2 == null) return;

            lblGjendja.ClientInstanceName = "labelGjendja" + e.VisibleIndex;
            txtBuxh1.ClientInstanceName = "textboxBuxh1" + e.VisibleIndex;
            txtBuxh2.ClientInstanceName = "textboxBuxh2" + e.VisibleIndex;
            lblDiff1.ClientInstanceName = "labelDiff1" + e.VisibleIndex;
            lblDiff2.ClientInstanceName = "labelDiff2" + e.VisibleIndex;

            if (e.VisibleIndex != 0)
            {
                txtBuxh1.ClientSideEvents.TextChanged = string.Format("function(s,e){{ShtoBuxhet1(textboxBuxh1{0}, labelGjendja{0},labelDiff1{0},{0});}}", e.VisibleIndex);
                txtBuxh2.ClientSideEvents.TextChanged = string.Format("function(s,e){{ShtoBuxhet2(textboxBuxh2{0}, labelGjendja{0},labelDiff2{0},{0});}}", e.VisibleIndex);
            }
            else
            {
                txtBuxh1.ClientSideEvents.TextChanged = string.Format("function(s,e){{ShtoTotal1(textboxBuxh1{0}, labelGjendja{0},labelDiff1{0});}}", e.VisibleIndex);
                txtBuxh2.ClientSideEvents.TextChanged = string.Format("function(s,e){{ShtoTotal2(textboxBuxh2{0}, labelGjendja{0},labelDiff2{0});}}", e.VisibleIndex);
            }
        }

        /// <summary>
        /// perdoret kur perdoruesi i ben callback grides per te rimbushur griden me te dhenat e ruajtura ne hiddenField pas callbackut
        /// </summary>
        /// <param name="sender">derguesi </param>
        /// <param name="e">argumentat</param>
        protected void grid_buxhetet_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {

        }

        #endregion

        /// <summary>
        /// perdoret per te ruajtur nje objekt clsPASH ne databaze
        /// krijohet objekti clsPASH sebashku me trupin me zerat dhe llogarite e buxhetet e tyre
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        /// :<see cref=" DbCore.DbKontabiliteti.clsPASH.ruajPASH() "/>
        ///  :<see cref=" DbCore.DbKontabiliteti.clsPASH.modifikoPASHAndBuxhete() "/>
        protected void RuajPash()
        {
            var kodi = kodi_ASPxTextBox.Text;

            if ((hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim") && clsPasqyreFinanciare.EkzistonPasqyreMeKeteKod(kodi, IdNdermarrja))
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo,
                    "Ekziston nje raport me kete kod. Ju lutemi, zgjidhni nje kod tjeter!", pnlMesazhi);
                hfStatusi.Value = "false";
                return;
            }

            var pash = new clsPasqyreFinanciare
            {
                EmertimiPasqyresFin = emertimi_ASPxTextBox.Text,
                KodiPasqyresFin = kodi,
                Metoda = metoda_ASPxComboBox.Text,
                IdNdermarja = IdNdermarrja,
                Viti = mySessionObjects.ktheVitiNdermarrjes(Session),
                IdPerdoruesi = IdPerdoruesi,
                IdStatusDok = 1,
                Model = false,
                OColTrupi = FormoColZerat()
            };

            switch (Request.QueryString["lloji"])
            {
                case "Pash":
                    pash.TipiPasqyresFin = "PASH";
                    break;
                case "Buxhetor":
                    pash.TipiPasqyresFin = "Buxhetor";
                    break;
                case "Bilanc":
                    pash.TipiPasqyresFin = "Bilanc";
                    break;
                case "PashOJF":
                    pash.TipiPasqyresFin = "PASHOJF";
                    break;
                case "CashflowOJF":
                    pash.TipiPasqyresFin = "CashflowOJF";
                    break;
                case "BilancOJF":
                    pash.TipiPasqyresFin = "BilancOJF";
                    break;
                default:
                    pash.TipiPasqyresFin = "Cash Flow";
                    break;
            }

            var mesazh = new clsMesazh();
            var tedrejtaInfo = new clsTeDrejtaRoli();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, clsFunksione.GetKomponente(Page.Request));

            if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim")
            {
                if (!tedrejtaInfo.DShtim)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["mesazhRaportNukKeniTeDrejta"], pnlMesazhi);
                    hfStatusi.Value = "false";
                    return;
                }

                mesazh = pash.RuajPasqyre(pash.IdPasqyresFin, pash.KodiPasqyresFin, pash.EmertimiPasqyresFin, pash.TipiPasqyresFin, pash.Metoda, pash.IdNdermarja, pash.Viti, pash.IdPerdoruesi, pash.OColTrupi, pash.IdStatusDok);
            }
            else
            {
                if (!tedrejtaInfo.DMod)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["mesazhRaportNukKeniTeDrejta"], pnlMesazhi);
                    hfStatusi.Value = "false";
                    return;
                }

                pash.IdPasqyresFin = Convert.ToInt32(hfId.Value);
                mesazh = pash.ModifikoPasqyre(pash.IdPasqyresFin, pash.KodiPasqyresFin, pash.EmertimiPasqyresFin, pash.TipiPasqyresFin, pash.Metoda, pash.IdPerdoruesi, pash.OColTrupi, pash.IdStatusDok);
            }

            if (mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                hfStatusi.Value = "true";
                ASPxPageControl1.ActiveTabIndex = 0;
                KonfiguroVleraFillestare();
            }
            else
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                hfStatusi.Value = "false";
            }
        }

        private colTrupPasqyreFinaciare FormoColZerat()
        {
            var serializusi = new JavaScriptSerializer();
            var dokumenti = (object[])serializusi.DeserializeObject(HiddenFieldZerat.Value);
            var trupat = new colTrupPasqyreFinaciare();
            trupat.AddRange(dokumenti.Select(t => new clsTrupPasqyreFinanciare((Dictionary<string, object>)t)).Where(trupi => trupi.PershkrimiZerit != ""));
            return trupat;
        }

        protected void gvKonfigPASH_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            var arr = e.Parameters.Split(';');

            if (arr.Length == 3)
            {
                if (arr[2] == "")
                    gvKonfigPASH.FilterExpression = "";
                else
                {
                    var filtra = new clsFiltraGrida();
                    var koka = new clsGridaKoka(IdGjuha, "gvKonfigPASH", "KonfigPASH.aspx?lloji=Pash", IdNdermarrja);
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], IdNdermarrja, koka.IdGridaKoka);

                    if (filtra.FiltraKodi != null)
                    {
                        gvKonfigPASH.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvKonfigPASH);

                        KonfiguroVleraFillestare();
                        hfStatusi.Value = "true";
                    }
                    else
                        hfStatusi.Value = "false";
                }
            }
            
            gvKonfigPASH.Selection.UnselectAll();
        }
    }
}