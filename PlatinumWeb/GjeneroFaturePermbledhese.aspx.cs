using System;
using System.Collections.Generic;
using DevExpress.Web;
using System.Data;
using DbCore.DbRegjistrim;
using DbCore;
using DbCore.DbShare;
using DbCore.DbAdmin;
using DevExpress.Data.Filtering;
using DbCore.IMBUtils.Extensions;
using DbCore.IMBUtils.Logging;
using DbCore.IMBUtils.Messages;
using DbCore.IMBUtils.Types;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Filters;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.Cache;

namespace PlatinumWeb
{
    /// <summary>
    /// nderfaqja e GjeneroFaturePermbledheset 
    /// </summary>
    public partial class GjeneroFaturePermbledhese : MyPageBase
    {
        private const string EmriKomponente = "GjeneroFaturePermbledhese.aspx";
        private TitlePeriudha _periudha;
        private string _guidString;

        public TitlePeriudha Periudha => _periudha ?? (_periudha = this.MerrPeriudhe(hfState));

        /// <summary>
        /// kur lodohet faqja  mbushet me te dhena dhe kontrollohen ne eshte i autorizuar perdoruesi
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!mySessionObjects.isLogedIn(Session))
                    clsFunksione.logout(Session, true, "FaqePaautorizuar");

                _guidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                hfState.Set("guidString", _guidString);
                hfState.Set("EmriFile", "Lista dokumenta per fature permbledhese");
                var now = DateTime.Now;
                txtNgaDok.Date = new DateTime(now.Year, now.Month, 1);
                txtDeriDok.Date = txtNgaDok.Date.AddMonths(1).AddDays(-1);
                ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(IdPerdoruesi, IdNdermarrja, cmbKonfigurimi, 91, rm, ci, IdGjuha);
                cmbKonfigurimi.SelectedIndex = 0;
                var konf = new clsKonfigurimAmbjenti();
                konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.Value.ToString()), IdGjuha);
                hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;
                var tedrejtaInfo = new clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
                var tedrejtaDokumenta = new clsTeDrejtaRoli();
                tedrejtaDokumenta.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, EmriKomponente);
                hfTeDrejtaGjitheDok.Value = (tedrejtaDokumenta.DGjitheDok &&
                              (clsAlternativaKushti.getAlternativa(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()), "SHFP") == "Jo")).ToString();

                clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, IdNdermarrja, "grid_RegDok", int.Parse(cmbKonfigurimi.Value.ToString()), EmriKomponente);

                EmrateButonave();
            }
            else
            {
                _guidString = (string)hfState["guidString"];

                Container.Attributes["src"] = ""; Container1.Attributes["src"] = "";
            }

            grid_RegDok.PercaktoTitlePanelMePeriudheDheTopRows(this, MenuInfo, pnlMesazhi, hfState, IdPerdoruesi, IdNdermarrja, IdViti, IdGjuha, int.Parse(cmbKonfigurimi.Value.ToString()), EmriKomponente, 543, "IdNivel", rm, ci);
            if (!IsPostBack)
            {
                MbushGridNgaDb(bool.Parse(hfTeDrejtaGjitheDok.Value));
                KonfiguroGride();
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(IdNdermarrja, "grid_RegDok", grid_RegDok, cmbKonfigurimi.Text.Split(';')[0], "543", IdGjuha);
            }
            else
            {
                MbushGridNgaSession();
                KonfiguroGride();
            }

            if (Request.QueryString["indexrow"] != null)
            {
                grid_RegDok.FocusedRowIndex = int.Parse(Request.QueryString["indexrow"]);
            }
            PercaktoTemplateMenu();

        }

        /// <summary>
        /// Vendos emrat e butonave ne baze te gjuhes se perdoruesit
        /// </summary>
        private void EmrateButonave()
        {
            konfigurimi_Label.Text = MessagesResource.Messages["lblModeli"];
            lblPeriudha.Text = MessagesResource.Messages["labelPeriudha"];
            lblNgaDok.Text = MessagesResource.Messages["lblNga"];
            lblDeriDok.Text = MessagesResource.Messages["lblDeri"];
        }

        protected void mbushKonfigurimAmbjenti(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Mbush menune me buttonat perkates sipas faqes
        /// </summary>
        private void PercaktoTemplateMenu() =>
            clsToolbarConfig.percaktoTemplateMenu(IdGjuha, IdViti, IdPerdoruesi, IdNdermarrja, ASPxMenu1, EmriKomponente, this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, true, false, false, mySessionObjects.merrEshteMemeSesioni(Session));

        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Mbush griden me te dhena te ruajtura ne sesion
        /// </summary>
        private void MbushGridNgaSession()
        {
            var periudha = this.MerrPeriudhe(hfState);

            DataTable tmpObject;
            if (!mySessionObjects.merrGrideNgaSessioni(EmriKomponente, IdViti, periudha.PeriudhaDok, Session, out tmpObject))
            {
                if (hfTeDrejtaGjitheDok.Value == "True" || hfTeDrejtaGjitheDok.Value == "true")
                    MbushGridNgaDb(true);
                else
                    MbushGridNgaDb(false);
            }
            else
            {
                grid_RegDok.DataSource = tmpObject;
                grid_RegDok.DataBind();
                tmpObject.Dispose();
            }
        }

        /// <summary>
        /// mbush griden me te dhena nga db
        /// </summary>
        /// <param name="gjitheDokumentat"></param>
        private void MbushGridNgaDb(bool gjitheDokumentat)
        {
            var periudha = this.MerrPeriudhe(hfState);
            var filter = grid_RegDok.FilterEnabled
                ? CriteriaToWhereClauseHelper.GetMsSqlWhere(CriteriaOperator.Parse(grid_RegDok.FilterExpression, 0))
                : "";
            var dt = colKokaShitje.merrKokaShitjePaFatureTatimoreDTNew(IdPerdoruesi, IdNdermarrja, gjitheDokumentat, IdNdermarrjeVit, periudha.DataDokNga, periudha.DataDokDeri, periudha.TopRowsControl.GetTopRowsPerSql(), filter);

            grid_RegDok.DataSource = dt;
            grid_RegDok.DataBind();

            mySessionObjects.ruajGrideNeSession(EmriKomponente, periudha.IdViti, periudha.PeriudhaDok, Session, dt);
            dt.Dispose();
        }

        /// <summary>
        /// konfiguron griden
        /// </summary>
        private void KonfiguroGride()
        {
            grid_RegDok.Columns["#"].VisibleIndex = 0;
            grid_RegDok.SettingsPager.PageSize = 20;
            KonfigurimComboGride.ShtoMenyrePagese(grid_RegDok, rm, ci);
            KonfigurimComboGride.shtoPikeShitjeFurnizim(grid_RegDok, IdNdermarrja, Session, EmriKomponente, _guidString);
            KonfigurimComboGride.ShtoTransportuesSipasNdermarrrjes(grid_RegDok, IdNdermarrja, Session, EmriKomponente, _guidString);
            KonfigurimComboGride.ShtoLlojiKlientFurnitor(grid_RegDok, rm, ci);
            KonfigurimComboGride.ShtoStatus(grid_RegDok, rm, ci);
            KonfigurimComboGride.ShtoGrupimDokumentash(grid_RegDok, IdNdermarrja, IdPerdoruesi, Session, EmriKomponente, _guidString, "IdGrup1", 1);
            KonfigurimComboGride.shto_DegeAdministrative(grid_RegDok, IdNdermarrja, Session, EmriKomponente, _guidString);
            KonfigurimComboGride.ShtoMonedhe(grid_RegDok, IdNdermarrja, IdPerdoruesi, Session, EmriKomponente, _guidString);
            KonfigurimComboGride.ShtoModel(grid_RegDok, 1, IdNdermarrja, IdPerdoruesi, IdGjuha, Session, EmriKomponente, _guidString);
            KonfigurimComboGride.ShtoNivel(grid_RegDok, 1, IdNdermarrja, IdPerdoruesi, IdGjuha, Session, EmriKomponente, _guidString);

            GridUtil.konfigGrideListeEMadhePaTheme(grid_RegDok, "IdShitjeKoka");
            var col3 = grid_RegDok.Columns["TotaliMeZbritjeMeTVSH"] as GridViewDataTextColumn;
            col3.PropertiesEdit.DisplayFormatString = "0.00";
        }

        /// <summary>
        /// kur grida ben databound per ti shtuar kolonen e selektimit
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void grid_RegDok_DataBound(object sender, EventArgs e)
        {
            var grid = (ASPxGridView)sender;
            if (grid.Columns["#"] != null) return;
            var check = new GridViewCommandColumn("#")
            {
                ShowSelectCheckbox = true,
                Width = System.Web.UI.WebControls.Unit.Percentage(2)
            };
            grid.Settings.ShowFilterRow = true;
            grid.Settings.ShowHeaderFilterButton = true;
            grid.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
            grid.Settings.ShowFilterRowMenu = true;
            grid.Columns.Add(check);
            grid.Settings.ShowGroupPanel = true;
            grid.KeyFieldName = "IdShitjeKoka";
            grid.SettingsBehavior.AllowSelectByRowClick = true;
            grid.SettingsBehavior.AllowFocusedRow = true;
        }

        /// <summary>
        /// fshin filtrin e zgjedhur dhe aplikuar mbi gride, fshirje nga DB ku ai eshte ruajtur, dhe jo pastrimi i grides nga aplikimi i filtrit
        /// per kete do sherbeje Clear Filter ne fund te grides
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        public void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
            var itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            var cmbFiltra = ((MenuFilter)itemButton.Template).FindControl("btnFiltra") as ASPxComboBox;

            var filtra = new clsFiltraGrida();
            var koka = new clsGridaKoka(IdGjuha, "grid_RegDok", EmriKomponente, IdNdermarrja);
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, IdNdermarrja, koka.IdGridaKoka);

            if (filtra.FiltraKodi == null) return;
            filtra.IdPerdoruesi = IdPerdoruesi;

            var mesazh = filtra.fshi();
            clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, IdNdermarrja, "grid_RegDok", int.Parse(cmbKonfigurimi.Value.ToString()), EmriKomponente);

            if (mesazh.Status)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

            cmbFiltra.Text = "";
            grid_RegDok.FilterExpression = "";
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
            int idfiltri;
            var mesazh = GridUtil.ruajFiltra(IdNdermarrja, IdPerdoruesi, IdGjuha, "grid_RegDok", EmriKomponente, cmbFiltra.Text, grid_RegDok.FilterExpression, grid_RegDok, "IdNivel", int.Parse(cmbKonfigurimi.Value.ToString()), out idfiltri);
            clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, IdNdermarrja, "grid_RegDok", int.Parse(cmbKonfigurimi.Value.ToString()), EmriKomponente);
            PercaktoTemplateMenu();
            if (mesazh.Status)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            grid_RegDok.FilterExpression = string.Empty;
            MbushGridNgaDb(bool.Parse(hfTeDrejtaGjitheDok.Value));
            KonfiguroGride();
            GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(IdNdermarrja, "grid_RegDok", grid_RegDok, cmbKonfigurimi.Text.Split(';')[0], "543", IdGjuha);
            cmbFiltra.Text = "";
        }

        /// <summary>
        /// eventet e menuse
        /// </summary>
        /// <param name="source">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {
            if (e.Item.Name == "Gjenero")
                Gjenero();
        }

        private void Gjenero()
        {
            var tedrejtaInfo = new clsTeDrejtaRoli();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, EmriKomponente);

            if (!tedrejtaInfo.DShtim)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                status1.Value = "false";
                return;
            }

            var myWatchTotal = new System.Diagnostics.Stopwatch();
            myWatchTotal.Start();
            
            DataRow[] dr;

            if (grid_RegDok.Selection.Count == 0)
            {
                var periudha = this.MerrPeriudhe(hfState);

                DataTable tmpObject;
                if (!mySessionObjects.merrGrideNgaSessioni(EmriKomponente, IdViti, periudha.PeriudhaDok, Session, out tmpObject))
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Nuk u morr dot grida nga sessioni", pnlMesazhi);
                    status1.Value = "false";
                    return;
                }

                var filterString = CriteriaToWhereClauseHelper.GetDataSetWhere(CriteriaOperator.Parse(grid_RegDok.FilterExpression, 0));
                dr = tmpObject.Select(filterString);
            }
            else
            {
                var fushat = ((DataTable)grid_RegDok.DataSource).GetAllColumnNames();
                var idte = grid_RegDok.GetSelectedFieldValues(fushat);
                dr = new DataRow[idte.Count];
                var i = 0;
                foreach (var o in idte)
                {
                    var newdr = ((DataTable)grid_RegDok.DataSource).NewRow();
                    newdr.ItemArray = (object[])o;
                    dr[i++] = newdr;
                }
            }

            Tuple<DataTable, List<int>, clsMesazh, int, int, List<object>> gjenerimi;
            bool printoFaturePermbledhese = clsAlternativaKushti.getAlternativa(Convert.ToInt32(cmbKonfigurimi.Value), "PF").ToLower() == "po";
            try
            {
                gjenerimi = clsKokaShitje.GjeneroFaturePermbledhese(IdGjuha, IdNdermarrja, clsFunksione.ktheServerUrl(Request), dr, Meme, txtNgaDok.Date, txtDeriDok.Date, mySessionObjects.merrPeriudheKontabel(Session).IdPeriudha, false, printoFaturePermbledhese);
            }
            catch (MyException myEx)
            {
                ImbLogger.Error(myEx.Message);
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, myEx.Message, pnlMesazhi);
                status1.Value = "false";
                return;
            }
            catch (Exception e)
            {
                ImbLogger.Error(e.Message);
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, e.Message, pnlMesazhi);
                status1.Value = "false";
                return;
            }

            myWatchTotal.Stop();
            System.Diagnostics.Debug.WriteLine("myWatch TOTAL: " + myWatchTotal.Elapsed);

            var mesazh = gjenerimi.Item3;

            mySessionObjects.ruajTabeleGabimeshImporti(Session, gjenerimi.Item1);
            if (gjenerimi.Item1.Rows.Count > 0)
            {
                var koka = new clsKokaErrorImporti(0, "Nga ruatja e fatures permbledhese", 91, IdNdermarrja, IdPerdoruesi);
                koka.ColTrupi.mbushErrorImportiNgaProgrami(gjenerimi.Item1);
                koka.ruajErrorImporti();
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "U ruajten " + (gjenerimi.Item4 - gjenerimi.Item1.Rows.Count) + " fatura dhe deshtuan " + gjenerimi.Item1.Rows.Count + " fatura! ", pnlMesazhi);
                Container.Attributes["src"] = "RaportiShpejte.aspx?Sesioni=false&emriReal=gabimeImporti&printo=false&db=jo";
            }
            else
            {
                if (gjenerimi.Item5 == 0)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgDokNukPlokKushtetFP"], pnlMesazhi);
                    MbushGridNgaSession();
                    grid_RegDok.Selection.UnselectAll();
                    return;
                }
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            }

            mySessionObjects.RemoveGridRowsInSessionByIdPC(SessionKeyUtils.MerrSessionKeyPerDsGride(EmriKomponente, IdViti, this.MerrPeriudhe(hfState).PeriudhaDok), grid_RegDok.KeyFieldName, gjenerimi.Item2);
            MbushGridNgaSession();
            grid_RegDok.Selection.UnselectAll();


            if (printoFaturePermbledhese && gjenerimi.Item6.Count > 0)
            {
                clsRaporti raporti = new clsRaporti();
                var rapMeDesign = gjenerimi.Item6.Find(x => !string.IsNullOrEmpty(Convert.ToString(((object[])x)[2])) && Convert.ToInt32(((object[])x)[2]) > 0);
                if(rapMeDesign != null)
                {
                    int idRaporti = DbCore.DbShare.clsRaporti.KtheIdRaporti(IdGjuha, Convert.ToInt32(((object[])rapMeDesign)[2]));
                    raporti = new clsRaporti(IdGjuha, idRaporti);
                }

                int nrRreshtaOk = 0;
                clsMesazh sms = clsFunksione.ruajTeDhenaRaportiPerHapjeRaportiTeShpejte(gjenerimi.Item6, raporti.RaportiEmriReal, out nrRreshtaOk, Session);
                if (!sms)
                    clsMenuInfo.ShtoMesazhInformues(MenuInfo, string.Format(rm.GetString("regjDokumentaMesazhSkaFormatPerPrintimFaturaPermbledhese", ci), sms.PershkrimMesazhi), pnlMesazhi);

                if(nrRreshtaOk > 0)
                {
                    grid_RegDok.JSProperties["cpHapFaqe"] = $"RaportiShpejte.aspx?Sesioni=false&emriReal={raporti.RaportiEmriReal}&idDokumenti={((object[])rapMeDesign)[0]}&printo=0&iddesign={((object[])rapMeDesign)[2]}";
                }
            }
        }

        /// <summary>
        /// kur grida ben callback
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void grid_RegDok_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            try
            {
                if (this.NdryshimFiltriGrida(grid_RegDok.ID))
                    MbushGridNgaDb(bool.Parse(hfTeDrejtaGjitheDok.Value));
            }
            catch (Exception ex)
            {
                grid_RegDok.ShtoMesazhErrori("ndodhi nje gabim ne marrjen e te dhenave nga db ", ex);
            }
        }

        /// <summary>
        /// kur perdoruesi i ben grides callbak per te mare konfigurimin e ri
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void grid_RegDok_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (grid_RegDok.AplikoFilterDefault(e, Convert.ToInt32(cmbKonfigurimi.Value)))
                return;

            var arr = e.Parameters.Split(';');

            if (arr.Length == 3)
            {
                if (!this.NdryshimFiltriGrida(grid_RegDok.ID))
                {
                    GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(IdNdermarrja, "grid_RegDok", grid_RegDok, cmbKonfigurimi.Text.Split(';')[0], "543", mySessionObjects.ktheGjuhe(Session));
                    if (arr[2] == "")
                        grid_RegDok.FilterExpression = "";
                    else
                    {
                        var filtra = new clsFiltraGrida();
                        var koka = new clsGridaKoka(IdGjuha, "grid_RegDok", EmriKomponente, IdNdermarrja);
                        filtra.mbushFilterPerGrideSipasKodit(arr[2], IdNdermarrja, koka.IdGridaKoka);
                        if (filtra.FiltraKodi != null)
                        {
                            grid_RegDok.FilterExpression = filtra.FiltraVlera;
                            GridUtil.renditGriden(filtra.KoloneRenditje, grid_RegDok);
                        }
                    }
                }
            }

            grid_RegDok.Selection.UnselectAll();
        }

        /// <summary>
        /// perdoret per te vendosur karakteristika te griden ne javascript
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void grid_RegDok_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = grid_RegDok.PageIndex;
            e.Properties["cpPageRow"] = grid_RegDok.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = grid_RegDok.VisibleRowCount;
        }

        /// <summary>
        /// perdoret per te shtuar menyra filtrimi
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void grid_RegDok_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            if (e.Column.FieldName == "NrDok" || e.Column.FieldName == "Pershkrimi")
            {
                e.Values.Clear();
                e.AddValue("(Te gjithe)", string.Empty, "true");
                e.AddValue("Nga A-D ", string.Empty, string.Format("{0}>'A     ' and {0} <'DDDDDDD'", e.Column.FieldName));
                e.AddValue("Nga D-G ", string.Empty, string.Format("{0}>'D     ' and {0}<'GGGGGGG'", e.Column.FieldName));
                e.AddValue("Nga H-K ", string.Empty, string.Format("{0}>'H     ' and {0}<'KKKKKKK'", e.Column.FieldName));
                e.AddValue("Nga L-O ", string.Empty, string.Format("{0}>'L     ' and {0}  <'OOOOOOO'", e.Column.FieldName));
                e.AddValue("Nga P-S ", string.Empty, string.Format("{0}>'P     ' and {0}<'SSSSSSS'", e.Column.FieldName));
                e.AddValue("Nga T-W ", string.Empty, string.Format("{0}>'T     ' and {0}<'WWWWWWW'", e.Column.FieldName));
                e.AddValue("Nga X-Z ", string.Empty, string.Format("{0}>'X     ' and {0}<'ZZZZZZZ'", e.Column.FieldName));
            }
            else
            {
                e.Values.Clear();
                e.AddValue("(Te gjithe)", string.Empty, "true");
            }
        }

        /// <summary>
        /// perdoret per te hequr filtrimin e grides kur zgjedhim elementin bosh kur eshte zgjedhur menyra e filtrimit automatik
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void grid_RegDok_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if (e.Column.FieldName.EqualsAnyIgnoreCase("IdKlientFurnitor", "IdMonedha", "IdDegeAdministrative", "IdTransportues", "IdGrup1", "IdKonfigAmbjente", "IdPikeShitjeFurnizimi", "IdDegeAdministrative", "IdGrup1") && string.IsNullOrEmpty(Converter.ConvertToInt(e.Value).ToString()))
                e.Criteria = null;
            if (e.Column.FieldName == "IdMenyrePagese" && Converter.ConvertToInt(e.Value) == -1)
                e.Criteria = null;
        }

        /// <summary>
        /// perdoret per te hequr filtrimin e grides kur zgjedhim elementin bosh kur eshte zgjedhur menyra e filtrimit manual,
        /// (me buton ose enter) kur ka te zgjedhur me shume se nje kolone per te filtruar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grid_RegDok_ProcessOnClickRowFilter(object sender, ASPxGridViewOnClickRowFilterEventArgs e)
        {
            foreach (var column in e.Values)
            {
                if (column.Key.EqualsAnyIgnoreCase("IdKlientFurnitor", "IdMonedha", "IdDegeAdministrative", "IdTransportues", "IdGrup1", "IdKonfigAmbjente", "IdPikeShitjeFurnizimi", "IdDegeAdministrative", "IdGrup1") && string.IsNullOrEmpty(Converter.ConvertToInt(column.Value).ToString()))
                    e.Criteria[column.Key] = null;

                if (column.Key == "IdMenyrePagese" && Converter.ConvertToInt(column.Value) == -1)
                    e.Criteria[column.Key] = null;
            }
        }
    }
}