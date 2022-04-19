using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using DbCore;
using DbCore.DbAdmin;
using DbCore.DbArkaBanka;
using DbCore.DbKontabiliteti;
using DbCore.DbShare;
using DbCore.IMBUtils.Messages;
using DevExpress.Data;
using DevExpress.Web;
using NLog;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using PlatinumWeb.Templates;
using MenuItem = DevExpress.Web.MenuItem;

namespace PlatinumWeb
{
    public partial class Shto_KategoriShpenzimi : MyPageBase
    {
        private const string PrefixMesazhNjejes = "Kategoria me Kod: ";
        private const string PrefixMesazhShumes = "Kategorite me Kod: ";
        private const string SuffixMesazhNjejesGabimi = " eshte e lidhur dhe nuk mund te fshihet";
        private const string SuffixMesazhShumesGabimi = " jane te lidhur dhe nuk mund te fshihen";
        private const string SuffixMesazhNjejesSuksesi = " u fshi me sukses!";
        private const string SuffixMesazhShumesSuksesi = " u fshine me sukses!";
        private const string LidhesMesazhi = ". Kurse ";
        private const string MesazhZgjidhniNje = "Ju lutem zgjidhni te pakten nje kategori!";
        private const string EmerKomponente = "Shto_KategoriShpenzimi.aspx";
        private const string EmerGrideKategoria = "gvKategoria";
        private const string EmerGrideBuxheti = "gvBuxheti";

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
            if (!Page.IsPostBack)
            {
                hfState.Set("guidString", GuidString);
                hfState.Set("idPerdoruesi", IdPerdoruesi);
                hfState.Set("idGjuha", IdGjuha);
                hfState.Set("idNdermarrje", IdNdermarrja);
                hfState.Set("idViti", IdViti);
                hfState.Set("idNdermarrjeVit", IdNdermarrjeVit);
                EmrateTabeve();
                PerktheLabel();
                ASPxPageControl1.ActiveTabIndex = 0;

                KonfiguroVleraFillestare();
                KonfiguroCombo();
                GridUtil.konfiguroGrideRegjistrimEvogelPaTheme(gvBuxheti, "IdBuxheti", false);
                MbushGridNgaDb();

                var tedrejtaInfo = new clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();

                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, EmerKomponente);
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(IdNdermarrja, EmerGrideKategoria, gvKategoria, cmbKonfigurimi.Text.Split(';')[0], "230", IdGjuha);
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(IdNdermarrja, EmerGrideBuxheti, gvBuxheti, cmbKonfigurimi.Text.Split(';')[0], "230", IdGjuha);
            }
            else
            {
                MbushGridNgaSession();
                ConfigureAspxComboBox.percaktoTemplateComboMeLupe(btneKapitulli);
                ConfigureAspxComboBox.KonfiguroComboBoxSipasLlojitKonfigUrdherPagese(mySessionObjects.merrIdNdermarrjeSesioni(Session), btneKapitulli, Convert.ToInt32(LlojeKonfigurimeUrdherPagese.Kapitull));
            }
            gvKategoria.Columns["#"].VisibleIndex = 0;
            GridUtil.konfigGrideListeEMadhePaTheme(gvKategoria, "Id");
            clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, IdNdermarrja, EmerGrideKategoria, Convert.ToInt32(cmbKonfigurimi.Value), EmerKomponente);
            AspxWebControlUtils.perkthePopUp(popFshi, MessagesResource.Messages["labelKujdes"], lblMsgbox, MessagesResource.Messages["labelAdministrimiMsgJeniSigurt"], ButtonCancel, MessagesResource.Messages["labelAnullo"]);
            PercaktoTemplate();
        }

        private void KonfiguroCombo()
        {
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(btneEmertimPrindi);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(btneKapitulli);
            ConfigureAspxComboBox.KonfiguroComboBoxSipasLlojitKonfigUrdherPagese(mySessionObjects.merrIdNdermarrjeSesioni(Session), btneKapitulli, Convert.ToInt32(LlojeKonfigurimeUrdherPagese.Kapitull));
            ConfigureAspxComboBox.KonfiguroComboBoxKategoriShpenzimi(btneEmertimPrindi, IdNdermarrja, true);

            cmbViti.ConfigureAndFill(() => clsFunksione.ktheVitetPerProjektBuxhetet(Convert.ToString(VitiNdermarrjes)), "VITI", "IDVITI");
            cmbViti.SelectedIndex = 0;
            btneEmertimPrindi.EnableCallbackMode = true;
        }

        /// <summary>
        /// Vendos emrat e tabeve ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateTabeve()
        {
            ASPxPageControl1.TabPages[0].Text = MessagesResource.Messages["labelAdministrimiTePergjithshme"];
            ASPxPageControl1.TabPages[1].Text = MessagesResource.Messages["MenuItemKategoriShpenzimi"];
        }

        public void PerktheLabel()
        {
            lblKodi.Text = MessagesResource.Messages["lblKodi"];
            lblLlojBuxheti.Text = MessagesResource.Messages["lblLlojBuxheti"];
            lblEmertimi.Text = MessagesResource.Messages["lblEmertimi"];
            lblPrindi.Text = MessagesResource.Messages["lblPrindi"];
            lblNiveli.Text = MessagesResource.Messages["lblNiveli"];
        }

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="idNderVit"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        private void PercaktoTemplateMenu()
        {
            clsToolbarConfig.percaktoTemplateMenu(IdGjuha, IdViti, IdPerdoruesi, IdNdermarrja, ASPxMenu1, EmerKomponente, this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, hfShtimModifikim.Value != "modifikim", false, false, Meme);
        }

        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            PercaktoTemplateMenu();
        }

        /// <summary>
        /// perdoret per te shtuar kolonen e selektimit tek grida dhe per te vendosur disa karakteristika te grides
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvKategoria_DataBound(object sender, EventArgs e)
        {// shton colonen # per selektim dhe disa karakteristika te grides
            if (gvKategoria.Columns["#"] != null) return;

            var check = new GridViewCommandColumn("#")
            {
                ShowSelectCheckbox = true,
                Width = Unit.Percentage(2)
            };
            gvKategoria.Settings.ShowFilterRow = true;
            gvKategoria.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
            gvKategoria.Settings.ShowFilterRowMenu = true;
            gvKategoria.Columns.Add(check);
            gvKategoria.Columns["#"].VisibleIndex = 0;
            gvKategoria.KeyFieldName = "Id";
            gvKategoria.SettingsBehavior.AllowSelectByRowClick = true;
            gvKategoria.SettingsBehavior.AllowFocusedRow = true;
        }

        /// <summary>
        ///  thirret sa here qe behet Callback, dhe ben edhe njeher lidhjen me datasource-in
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvKategoria_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            if (e.CallbackName == "COLUMNMOVE" && gvKategoria.AllColumns[int.Parse(e.Args[0])].Width.Value == 0)
                gvKategoria.AllColumns[int.Parse(e.Args[0])].Width = Unit.Percentage(3);
            if (e.CallbackName == "APPLYFILTER" && e.Args[0] == "")
            {
                MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
                var cmbFiltra = ((MenuFilter)itemButton.Template).FindControl("btnFiltra") as ASPxComboBox;
                cmbFiltra.Text = "";
            }
        }

        /// <summary>
        /// sherben per te vendosur filtra tek header-i i grides (mund te ndryshohet me vone per tu
        /// bere  me e konfigurueshme
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvKategoria_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            if (e.Column.FieldName == "Pershkrimi")
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
            //kap item qe ka template ne menune e kesaj faqeje
            var itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            var cmbFiltra = ((MenuFilter)itemButton.Template).FindControl("btnFiltra") as ASPxComboBox;
            var filtra = new clsFiltraGrida();
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra?.Text, IdNdermarrja, new clsGridaKoka(IdGjuha, EmerGrideKategoria, EmerKomponente, IdNdermarrja).IdGridaKoka);

            if (filtra.FiltraKodi == null) return;

            filtra.IdPerdoruesi = IdPerdoruesi;
            var mesazh = filtra.fshi();

            clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, IdNdermarrja, EmerGrideKategoria, 1, EmerKomponente);
            PercaktoTemplateMenu();

            if (mesazh.Status)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

            cmbFiltra.Text = "";
            KonfiguroVleraFillestare();
            hfStatusi.Value = "true";
            gvKategoria.FilterExpression = String.Empty;
        }

        /// <summary>
        /// sherben per te ruajtur filtrin e zgjedhur
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje
            var itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            var cmbFiltra = ((MenuFilter)itemButton.Template).FindControl("btnFiltra") as ASPxComboBox;

            var filtri = new clsFiltraGrida
            {
                FiltraKodi = cmbFiltra?.Text,
                FiltraShenime = cmbFiltra?.Text,
                FiltraUniversal = false,
                GridaKokaId = new clsGridaKoka(IdGjuha, EmerGrideKategoria, EmerKomponente, IdNdermarrja).IdGridaKoka,
                FiltraVlera = gvKategoria.FilterExpression,
                IdPerdoruesi = IdPerdoruesi,
                IdNdermarje = IdNdermarrja,
                IdStatusDok = 1
            };
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("Kodi", gvKategoria);
            //var kolona = gvKategoria.GetSortedColumns();
            //if (kolona.Count > 0)
            //{
            //    filtri.KoloneRenditje = kolona[0].FieldName;
            //    filtri.DrejtimRenditje = kolona[0].SortOrder == ColumnSortOrder.Ascending;
            //}
            //else
            //{
            //    filtri.KoloneRenditje = "Kodi";
            //    filtri.DrejtimRenditje = true;
            //}



            var mesazh = filtri.ruaj();
            clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, IdNdermarrja, EmerGrideKategoria, 1, EmerKomponente);
            PercaktoTemplateMenu();

            if (mesazh.Status)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

            hfStatusi.Value = "true";
            cmbFiltra.Text = "";
        }

        /// <summary>
        /// perdoret per te fshire reshtat e zgjedhur ne gride pasi perdoruesi ka konfirmuar fshirjen
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            var rreshtat = ASPxPageControl1.ActiveTabIndex == 0
                ? gvKategoria.GetSelectedFieldValues("Id")
                : new List<object> { hfId.Value };

            if (rreshtat.Count == 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MesazhZgjidhniNje, pnlMesazhi);
                return;
            }

            List<string> teFshire = new List<string>(), tePaFshire = new List<string>();

            foreach (var id in rreshtat)
            {
                var kategoria = new clsKategoriShpenzimi(Convert.ToInt32(id));

                if (clsKategoriShpenzimi.KaVeprimeKategoriShpenzimi(kategoria.Id))
                {
                    tePaFshire.Add(kategoria.Kodi);
                    continue;
                }
                kategoria.IdPerdoruesi = IdPerdoruesi;

                var mesazhi = kategoria.Fshi();
                if (kategoria.Id == 0)
                    continue;

                if (kategoria.IdStatusDok == 2)
                    continue;

                if (mesazhi.Status)
                {
                    HiqNgaGrida(kategoria.Id);
                    teFshire.Add(kategoria.Kodi);

                    ASPxPageControl1.ActiveTabIndex = 0;
                    hfStatusi.Value = "true";
                }
            }

            string mesazhInfoGabim = "", mesazhInfoSukses = "";
            if (tePaFshire.Count == 1)
                mesazhInfoGabim = string.Format("{0}{1}{2}", PrefixMesazhNjejes, string.Join(";", tePaFshire), SuffixMesazhNjejesGabimi);
            else
                if (tePaFshire.Count > 1)
                mesazhInfoGabim = string.Format("{0}{1}{2}", PrefixMesazhShumes, string.Join(";", tePaFshire), SuffixMesazhShumesGabimi);
            if (teFshire.Count == 1)
                mesazhInfoSukses = string.Format("{0}{1}{2}", PrefixMesazhNjejes, string.Join(";", teFshire), SuffixMesazhNjejesSuksesi);
            else
                if (teFshire.Count > 1)
                mesazhInfoSukses = string.Format("{0}{1}{2}", PrefixMesazhShumes, string.Join(";", teFshire), SuffixMesazhShumesSuksesi);

            if (mesazhInfoGabim != "" && mesazhInfoSukses != "")
                mesazhInfoGabim += LidhesMesazhi + mesazhInfoSukses;

            if (mesazhInfoGabim != "")
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhInfoGabim, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazhInfoSukses, pnlMesazhi);

            gvKategoria.Selection.UnselectAll();
            pnlMesazhi.Update();
        }

        private void HiqNgaGrida(int id)
        {
            if (gvKategoria.DataSource != null)
            {
                var dt = (DataTable)gvKategoria.DataSource;
                var drs = dt.Select("Id = " + id);
                if (drs.Length > 1)
                    throw new Exception("GABIM: Ndodhen 2 kategori me te njejten id ne gride");

                if (drs.Length == 0) return;

                dt.Rows.Remove(drs[0]);
                gvKategoria.DataBind();
            }
            else
                MbushGridNgaDb();
        }

        private void ShtoNeGrid(int id)
        {
            if (gvKategoria.DataSource != null)
            {
                var dt = (DataTable)gvKategoria.DataSource;
                var drs = dt.Select("Id = " + id);
                if (drs.Length > 0)
                    throw new Exception("GABIM: kategoria ekziston ne gride");

                dt.ImportRow(colKategoriShpenzimi.MerrKategoriSipasIdDr(id));
            }
            else
                MbushGridNgaDb();
        }

        private void ModifikoNeGrid(int id)
        {
            if (gvKategoria.DataSource != null)
            {
                var drs = ((DataTable)gvKategoria.DataSource).Select("Id = " + id);
                if (drs.Length > 1)
                    throw new Exception("GABIM: Ndodhen 2 kategori me te njejten id ne gride");

                if (drs.Length == 0) return;

                drs[0].ItemArray = colKategoriShpenzimi.MerrKategoriSipasIdDr(id).ItemArray;
            }
            else
                MbushGridNgaDb();
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
                RuajKategori();
            }
        }

        /// <summary>
        /// perdoret per te shfaqur te gjithe komponentet kur zgjidhet (...)
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumenti</param>
        protected void gvKategoria_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {

        }

        /// <summary>
        /// ruan disa karakteristika te grides
        /// </summary>
        /// <param name="sender">dergues</param>
        /// <param name="e">argumentat</param>
        protected void gridllog_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gvKategoria.PageIndex;
            e.Properties["cpPageRow"] = gvKategoria.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gvKategoria.VisibleRowCount;
        }

        /// <summary>
        /// perdoret per te percaktuar templatet e komboboxeve dhe te mbushe vlerat fillestare te gridave
        /// </summary>
        private void KonfiguroVleraFillestare()
        {
            MbushListeBuxhetesh(true);
            ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(IdPerdoruesi, IdNdermarrja, cmbKonfigurimi, 155, rm, ci, IdGjuha);
            cmbKonfigurimi.SelectedIndex = 0;
            var konf = new clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()), IdGjuha);
            hfKonffillestar.Value = string.Format("{0};{1}", konf.KodKonfigAmbjente, konf.PershkrimKonfigAmbjente);
        }

        private void MbushGridNgaSession()
        {
            DataTable tmpObject;
            var sukses = mySessionObjects.merrGrideNgaSessioni(clsFunksione.GetKomponente(Page.Request), Session, out tmpObject);
            if (!sukses)
                MbushGridNgaDb();
            else
            {
                gvKategoria.DataSource = tmpObject;
                gvKategoria.DataBind();
                tmpObject.Dispose();
            }
        }

        /// <summary>
        /// mbush griden e popupit me te dhena 
        /// </summary>
        private void MbushGridNgaDb()
        {
            var dt = colKategoriShpenzimi.MerrKategorineNdermarjeDt(IdNdermarrja);
            mySessionObjects.ruajGrideNeSession(clsFunksione.GetKomponente(Page.Request), Session, dt);
            gvKategoria.DataSource = dt;
            gvKategoria.DataBind();
            dt.Dispose();
        }

        /// <summary>
        /// mbush griden e buxheteve
        /// </summary>
        private void MbushListeBuxhetesh(bool krijoBuxheteFillestare, int idKategoriShpenzimi = 0)
        {
            colBuxhetet buxhetet;
            if (krijoBuxheteFillestare)
                buxhetet = colBuxhetet.KrijoBuxhetetFillestare(new DateTime(VitiNdermarrjes, 1, 1));
            else
            {
                int idNderViti;
                var eshteProjektBuxheti = false;
                if (cmbViti.IsVisible() && Convert.ToInt32(cmbLlojBuxheti.Value) == 1)
                {
                    idNderViti = Convert.ToInt32(cmbViti.Value);
                    eshteProjektBuxheti = true;
                }
                else
                    idNderViti = mySessionObjects.ktheNdermarrjeVit(Session);

                buxhetet = colBuxhetet.KrijoBuxhetet(idKategoriShpenzimi, "KategoriShpenzimi", idNderViti, dteDateAkt.Date.Date, eshteProjektBuxheti);
            }

            gvBuxheti.ShtoObjectNeGride(buxhetet[1].IdKonfigUrdherPagese, "cpKapitulli");
            gvBuxheti.DataSource = buxhetet;
            gvBuxheti.DataBind();
        }

        private void PercaktoTemplate()
        {// percaktohen tipet e kolonave per griden e buxheteve
            GridViewDataTextColumn col1 = gvBuxheti.Columns["Muaj"] as GridViewDataTextColumn;
            col1.VisibleIndex = 0;
            col1.DataItemTemplate = new MyLabelTemplate();

            GridViewDataTextColumn col2 = gvBuxheti.Columns["Gjendja"] as GridViewDataTextColumn;
            col2.VisibleIndex = 1;
            col2.DataItemTemplate = new MyLabelTemplate();
            col2.PropertiesEdit.DisplayFormatString = "0.00";
            GridViewDataTextColumn col3 = gvBuxheti.Columns["Buxheti_1"] as GridViewDataTextColumn;
            col3.VisibleIndex = 2;
            col3.DataItemTemplate = new MyDoubleTemplate(false, 2, "0"); // "0.00");
            col3.PropertiesEdit.DisplayFormatString = "0.00";
            GridViewDataTextColumn col4 = gvBuxheti.Columns["Buxheti_2"] as GridViewDataTextColumn;
            col4.VisibleIndex = 3;
            col4.DataItemTemplate = new MyDoubleTemplate(false, 2, "0"); // "0.00");
            col4.PropertiesEdit.DisplayFormatString = "0.00";
            GridViewDataTextColumn col5 = gvBuxheti.Columns["Diferenca_1"] as GridViewDataTextColumn;
            col5.VisibleIndex = 4;
            col5.ReadOnly = true;
            col5.DataItemTemplate = new MyReadOnlyTextTemplate();
            col5.PropertiesEdit.DisplayFormatString = "0.00";
            GridViewDataTextColumn col6 = gvBuxheti.Columns["Diferenca_2"] as GridViewDataTextColumn;
            col6.VisibleIndex = 5;
            col6.ReadOnly = true;
            col6.PropertiesEdit.DisplayFormatString = "0.00";
            col6.DataItemTemplate = new MyReadOnlyTextTemplate();
            GridViewDataTextColumn colShenime = gvBuxheti.Columns["Shenime"] as GridViewDataTextColumn;
            colShenime.VisibleIndex = 6;
            colShenime.DataItemTemplate = new MyTextTemplate();
        }

        /// <summary>
        ///sherben per te ruajtur nje kategori 
        /// </summary>
        private void RuajKategori()
        {
            if (!Page.IsValid)
                return;

            clsKategoriShpenzimi kategori;
            int idNderViti;
            var eshteProjektBuxheti = false;

            if (cmbViti.IsVisible() && Convert.ToInt32(cmbLlojBuxheti.Value) == 1)
            {
                idNderViti = Convert.ToInt32(cmbViti.Value);
                eshteProjektBuxheti = true;
            }
            else
                idNderViti = mySessionObjects.ktheNdermarrjeVit(Session);

            try
            {
                kategori = KrijoKategoriShpenzimi(idNderViti, eshteProjektBuxheti);
            }
            catch (MyException ex)
            {
                LogManager.GetCurrentClassLogger().Error(ex.Message);
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, ex.Message, pnlMesazhi);
                hfStatusi.Value = "false";
                return;
            }
            catch (Exception e)
            {
                LogManager.GetCurrentClassLogger().Error(e.Message);
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Nje gabim i papritur ka ndodhur!", pnlMesazhi);
                hfStatusi.Value = "false";
                return;
            }

            clsMesazh mesazh;
            bool eshteShtim;
            var tedrejtaInfo = new clsTeDrejtaRoli();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, EmerKomponente);

            if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim")
            {
                if (!tedrejtaInfo.DShtim)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                    hfStatusi.Value = "false";
                    return;
                }

                mesazh = kategori.Ruaj(idNderViti, eshteProjektBuxheti);
                eshteShtim = true;
            }
            else
            {
                if (!tedrejtaInfo.DMod)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                    hfStatusi.Value = "false";
                    return;
                }

                mesazh = kategori.Modifiko(idNderViti, eshteProjektBuxheti);
                eshteShtim = false;
            }
            if (mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["mesazhRuajtjeMeSukses"], pnlMesazhi);

                if (eshteShtim)
                    ShtoNeGrid(kategori.Id);
                else
                    ModifikoNeGrid(kategori.Id);

                hfStatusi.Value = "true";
                PastroFusha();
                ASPxPageControl1.ActiveTabIndex = 0;
            }
            else
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ndodhi nje gabim. Ruajtja nuk u krye!", pnlMesazhi);
                hfStatusi.Value = "false";
            }

            pnlMesazhi.Update();
        }

        /// <summary>
        /// pastron fushat e faqes
        /// </summary>
        private void PastroFusha()
        {
            MbushListeBuxhetesh(true);
            hfBuxheti1.Value = "";
            hfBuxheti2.Value = "";
            HiddenField2.Value = "";
            hfBuxhetiShenime.Value = "";
        }

        /// <summary>
        /// krijon kategorine e shpenzimit qe do te ruhet
        /// </summary>
        /// <returns>kthen clsKategoriShpenzimi me llogarine qe do te ruhet</returns>
        private clsKategoriShpenzimi KrijoKategoriShpenzimi(int idNderViti, bool eshteProjektBuxheti)
        {
            var buxhete = new colBuxhetet();
            var veprimi = hfShtimModifikim.Value;
            int idkapitull = 0;

            if (btneKapitulli.Text != "")
                idkapitull = new clsKonfigUrdherPagese(btneKapitulli.Text, IdNdermarrja, Convert.ToInt32(LlojeKonfigurimeUrdherPagese.Kapitull)).Id;

            switch (veprimi)
            {
                case "shtim":
                    buxhete = colBuxhetet.KrijoBuxhetetSipasLlojitTeBuxhetit(hfBuxheti1.Value, hfBuxheti2.Value, dteDateAkt.Date.Date, idkapitull, hfBuxhetiShenime.Value);
                    break;
                case "modifikim":
                case "klonim":
                    buxhete = colBuxhetet.KrijoBuxhetetEModifikuara("KategoriShpenzimi", int.Parse(hfId.Value), idNderViti, eshteProjektBuxheti, hfBuxheti1.Value, hfBuxheti2.Value, dteDateAkt.Date.Date, idkapitull, hfBuxhetiShenime.Value, true);
                    break;
            }

            int idprindi = 0;
            var kodprindi = "";
            int niveli = 1;

            if (txtNiveli.Text != string.Empty)
                niveli = Convert.ToInt32(txtNiveli.Text);

            if (btneEmertimPrindi.Text != string.Empty)
            {
                idprindi = Convert.ToInt32(btneEmertimPrindi.Value);
                kodprindi = btneEmertimPrindi.Text;
            }

            return new clsKategoriShpenzimi(veprimi == "modifikim" ? int.Parse(hfId.Value) : 0, txtKodi.Text, txtEmertimi.Text, IdPerdoruesi, IdNdermarrja, IdPerdoruesi, 1, buxhete, veprimi != "modifikim", idprindi, niveli, kodprindi, cbKatAktive.Checked);
        }

        /// <summary>
        ///  thirret sa here qe behet Callback, dhe ben edhe njeher lidhjen me datasource-in
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvBuxheti_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
        }

        /// <summary>
        /// perdoret per te shtuar evente kolonave te buxheteve
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvBuxheti_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        { //sherben per te marre me vone vlerat e futura nga perdoruesi me ane te javascriptit

            if (e.RowType == GridViewRowType.Data)
            {
                var colGjendja = ((ASPxGridView)sender).Columns["Gjendja"] as GridViewDataColumn;
                var colBuxh1 = ((ASPxGridView)sender).Columns["Buxheti_1"] as GridViewDataColumn;
                var colBuxh2 = ((ASPxGridView)sender).Columns["Buxheti_2"] as GridViewDataColumn;
                var colDiff1 = ((ASPxGridView)sender).Columns["Diferenca_1"] as GridViewDataColumn;
                var colDiff2 = ((ASPxGridView)sender).Columns["Diferenca_2"] as GridViewDataColumn;
                var colShenime = ((ASPxGridView)sender).Columns["Shenime"] as GridViewDataTextColumn;
                var lblGjendja = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, colGjendja, "lbl") as ASPxLabel;
                var txtBuxh1 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, colBuxh1, "txtBox") as ASPxTextBox;
                var txtBuxh2 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, colBuxh2, "txtBox") as ASPxTextBox;
                var lblDiff1 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, colDiff1, "txtBox") as ASPxTextBox;
                var lblDiff2 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, colDiff2, "txtBox") as ASPxTextBox;
                var txtShenime = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, colShenime, "txtBox") as ASPxTextBox;

                if (txtBuxh1 != null && txtBuxh2 != null)
                {
                    lblGjendja.ClientInstanceName = "labelGjendja" + e.VisibleIndex;
                    txtBuxh1.ClientInstanceName = "textboxBuxh1" + e.VisibleIndex;
                    txtBuxh2.ClientInstanceName = "textboxBuxh2" + e.VisibleIndex;
                    lblDiff1.ClientInstanceName = "labelDiff1" + e.VisibleIndex;
                    lblDiff2.ClientInstanceName = "labelDiff2" + e.VisibleIndex;
                    txtShenime.ClientInstanceName = "txtShenime" + e.VisibleIndex;
                    if (e.VisibleIndex != 0)
                    {
                        txtBuxh1.ClientSideEvents.TextChanged = "function(s,e){ ShtoBuxhet1(textboxBuxh1" + e.VisibleIndex + ", labelGjendja" + e.VisibleIndex + ",labelDiff1" + e.VisibleIndex + "," + e.VisibleIndex + ");}";
                        txtBuxh2.ClientSideEvents.TextChanged = "function(s,e){ShtoBuxhet2(textboxBuxh2" + e.VisibleIndex + ", labelGjendja" + e.VisibleIndex + ",labelDiff2" + e.VisibleIndex + "," + e.VisibleIndex + ");}";

                    }
                    else
                    {
                        txtBuxh1.ClientSideEvents.TextChanged = "function(s,e){ShtoTotal1(textboxBuxh1" + e.VisibleIndex + ", labelGjendja" + e.VisibleIndex + ",labelDiff1" + e.VisibleIndex + ");}";
                        txtBuxh2.ClientSideEvents.TextChanged = "function(s,e){ShtoTotal2(textboxBuxh2" + e.VisibleIndex + ", labelGjendja" + e.VisibleIndex + ",labelDiff2" + e.VisibleIndex + ");}";
                    }
                }
            }
        }

        /// <summary>
        /// perdoret per te rikonfiguruar griden kur ndryshohet konfigurimi
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvKategoria_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            var arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                if (arr[2] == "")
                    gvKategoria.FilterExpression = "";
                else
                {
                    var koka = new clsGridaKoka(IdGjuha, EmerGrideKategoria, EmerKomponente, IdNdermarrja);
                    var filtra = new clsFiltraGrida();
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], IdNdermarrja, koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        gvKategoria.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvKategoria);

                        KonfiguroVleraFillestare();
                        hfStatusi.Value = "true";
                    }
                    else
                        hfStatusi.Value = "false";
                }
            }

            gvKategoria.Selection.UnselectAll();
        }

        /// <summary>
        /// kur gvBuxheti ben callback per tu mbushur me vlerat per llogarine e zgjedhur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvBuxheti_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            int id = 0;
            if (!string.IsNullOrEmpty(hfId.Value))
                id = int.Parse(hfId.Value);

            MbushListeBuxhetesh(hfShtimModifikim.Value != "modifikim" && hfShtimModifikim.Value != "klonim", id);
        }

        protected void btneEmertimPrindi_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("btneEmertimPrindi"))
                ConfigureAspxComboBox.KonfiguroComboBoxKategoriShpenzimi(btneEmertimPrindi, IdNdermarrja, e);
        }

        protected void btneEmertimPrindi_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("btneEmertimPrindi"))
                ConfigureAspxComboBox.KonfiguroComboBoxKategoriShpenzimi(btneEmertimPrindi, IdNdermarrja, e);
        }
    }
}