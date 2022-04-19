using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using DbCore;
using DbCore.DbAdmin;
using DbCore.DbKontabiliteti;
using DbCore.DbQendraKosto;
using DbCore.DbRegjistrim;
using DbCore.DbShare;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Messages;
using DbCore.IMBUtils.Types;
using DevExpress.Data;
using DevExpress.Data.Filtering;
using DevExpress.Web;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Filters;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{

    /// <summary>
    /// nderfaqja e VeprimeKFt te prodhimit
    /// </summary>
    public partial class VeprimeKF : MyPageBase
    {
        private const string Komponente = "VeprimeKF.aspx";
        string datanga, dataderi;
        private TitlePeriudha _periudha;
        public TitlePeriudha Periudha => _periudha ?? (_periudha = this.MerrPeriudhe(hfState));
        /// <summary>
        /// kur lodohet faqja  mbushet me te dhena dhe kontrollohen ne eshte i autorizuar perdoruesi
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!mySessionObjects.isLogedIn(Session))
                clsFunksione.logout(Session, true, "FaqePaautorizuar");

            if (mySessionObjects.ktheKodNdermarrje(Session) == null)
                Response.Redirect("Login_Ndermarrje.aspx?id=" + IdPerdoruesi);

            PercaktoTemplateMenu();

            if (!IsPostBack)
            {
                hfState.Set("idGjuha", IdGjuha);
                hfState.Set("idPerdoruesi", IdPerdoruesi);
                hfState.Set("idNdermarrjeVit", IdNdermarrjeVit);
                hfState.Set("idNdermarrje", IdNdermarrja);
                hfState.Set("guidString", GuidString);
                ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(IdPerdoruesi, IdNdermarrja, cmbKonfigurimi, 57, rm, ci, IdGjuha);
                cmbKonfigurimi.SelectedIndex = 0;
                clsKonfigurimAmbjenti konf = new clsKonfigurimAmbjenti();
                konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()), IdGjuha);
                hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;
                grid_VeprimeKF.PercaktoTitlePanelMePeriudheDheTopRows(this, MenuInfo, pnlMesazhi, hfState, IdPerdoruesi, IdNdermarrja, IdViti, IdGjuha, int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()), Komponente, 652, "IdVeprimeKFKoka", rm, ci, false);
                MbushGridNgaDb(true);
                KonfiguroGride();
                var periudheDok = clsAlternativaKushti.getAlternativa(konf.IdKonfigAmbjente, "SHDPER");
                var oPeriudha = mySessionObjects.merrPeriudheKontabel(Session);
                clsFunksione.ruajPeriudhatNeHiddenField(IsPostBack, hfState, periudheDok, oPeriudha, out datanga, out dataderi);
                if (Request.QueryString["fshi"] == "po")
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["regjisDokMesazhSukesFshirjeDokument"], pnlMesazhi);
                else if (Request.QueryString["ruaj"] == "po")
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["regjMagModifikimiPerfundoiMeSukses"], pnlMesazhi);

                var tedrejtaInfo = new clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(IdNdermarrja, "grid_VeprimeKF", grid_VeprimeKF, cmbKonfigurimi.Text.Split(';')[0], "652", IdGjuha);
            }
            else
            {
                //grid_VeprimeKF.Columns.Clear();
                //grid_VeprimeKF.AutoGenerateColumns = true;
                grid_VeprimeKF.PercaktoTitlePanelMePeriudheDheTopRows(this, MenuInfo, pnlMesazhi, hfState, IdPerdoruesi, IdNdermarrja, IdViti, IdGjuha, int.Parse(cmbKonfigurimi.Value.ToString()), Komponente, 652, "IdVeprimeKFKoka", rm, ci, false);
                MbushGridNgaSession();
                KonfiguroGride();
            }
            GridUtil.konfigGrideListeEMadhePaTheme(grid_VeprimeKF, "IdVeprimeKFKoka");
            AspxWebControlUtils.perkthePopUp(popFshi, MessagesResource.Messages["labelKujdes"], lblMsgbox, MessagesResource.Messages["labelAdministrimiMsgJeniSigurt"], ButtonCancel, MessagesResource.Messages["labelAnullo"]);
            clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, IdNdermarrja, "grid_VeprimeKF", int.Parse(cmbKonfigurimi.Value.ToString()), Komponente);
            konfigurimi_Label.Text = MessagesResource.Messages["lblModeli"];
            if (Request.QueryString["indexrow"] != null)
                grid_VeprimeKF.FocusedRowIndex = int.Parse(Request.QueryString["indexrow"]);
            Container.Attributes["src"] = "";
        }

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        private void PercaktoTemplateMenu()
        {
            clsToolbarConfig.percaktoTemplateMenu(IdGjuha, IdViti, IdPerdoruesi, IdNdermarrja, ASPxMenu1, Komponente, this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, true, false, false, Meme);
        }

        /// <summary>
        /// ruan konfigurimin e grides dhe filtrin e zgjedhur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RuajKolona_Click(object sender, EventArgs e)
        {
            int idfiltri;
            var mesazh = GridUtil.ruajFiltra(IdNdermarrja, IdPerdoruesi, IdGjuha, "grid_VeprimeKF", Komponente, "FilterDefault", grid_VeprimeKF.FilterExpression, grid_VeprimeKF, "IdNivel", clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(cmbKonfigurimi.Text.Split(';')[0], IdNdermarrja), out idfiltri);//ruan filtrin e zgjedhur tek filtrat
            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                return;
            }

            mesazh = GridUtil.ruajkonfigurimgride(grid_VeprimeKF, cmbKonfigurimi.Text, IdNdermarrja, IdPerdoruesi, 652, idfiltri, IdViti, ci, IdGjuha);/// ruan konfigurimin e grides dhe filtrin
            clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, IdNdermarrja, "grid_VeprimeKF", int.Parse(cmbKonfigurimi.Value.ToString()), Komponente);

            PercaktoTemplateMenu();
            if (mesazh.Status)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
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
        /// mbush griden me te dhena te ruajtura ne sesion
        /// </summary>
        private void MbushGridNgaSession()
        {
            var tmpObject = grid_VeprimeKF.MerrDataSourceMePeriudheDheTopRowsNeSession<DataTable>(Session, Komponente, Periudha, hfState.Get("guidString").ToString());
            if (tmpObject == null)
                MbushGridNgaDb(true);
            else
            {
                //grid_VeprimeKF.Columns.Clear();
                grid_VeprimeKF.DataSource = tmpObject;
                grid_VeprimeKF.DataBind();
                tmpObject.Dispose();
            }
        }

        /// <summary>
        /// mbush griden me te dhena nga db
        /// </summary>
        private void MbushGridNgaDb(bool pastroFiltrinGrides = false)
        {
            string filterDefault = pastroFiltrinGrides ? "" : "[IdStatusDok]=1";

            var op = CriteriaOperator.Parse(string.IsNullOrWhiteSpace(grid_VeprimeKF.FilterExpression) ? filterDefault : grid_VeprimeKF.FilterExpression, 0);

            var filterString = CriteriaToWhereClauseHelper.GetMsSqlWhere(op);
            if (string.IsNullOrWhiteSpace(grid_VeprimeKF.FilterExpression))
                grid_VeprimeKF.FilterExpression = filterDefault;

            string topRows = Periudha.TopRowsControl.TopRows.ToString();

            if (!string.IsNullOrEmpty(topRows))//nqs topRows (null || empty) merr konfigurimin default qe ka grida per top rows
            {
                if (((DataTable)grid_VeprimeKF.DataSource)?.Rows.Count > 1000 && (grid_VeprimeKF.FilterExpression.ToLower() != filterDefault.ToLower() && grid_VeprimeKF.FilterExpression.ToLower().Contains(filterDefault.ToLower())) && Request.Params["__CALLBACKPARAM"].Contains("COLUMNFILTER"))
                    return;
            }
            else
            {
                clsGridaKoka gridaKoka = new clsGridaKoka("grid_VeprimeKF", Komponente, IdNdermarrja, Convert.ToInt32(cmbKonfigurimi.Value));
                topRows = (gridaKoka.TopRows != null && gridaKoka.TopRows > 0) ? gridaKoka.TopRows.ToString() : string.Empty;
            }
            if (!string.IsNullOrEmpty(topRows))
                topRows = $"TOP {topRows}";
            var dt = colVeprimeKFKoka.merrVeprimeKFSipasPeriudhesDheFiltritDT(IdNdermarrjeVit, IdPerdoruesi, Periudha.DataDokNga, Periudha.DataDokDeri, filterString, topRows == "TOP -1" ? "" : topRows);
            if (dt == null)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ndodhi nje problem gjate leximit te listes! Ju lutem riperserisni veprimin!", pnlMesazhi);
                return;
            }
            //ruajme ne session vetem per ds me filtrin default te grides dhe top rows te gjitha
            if (grid_VeprimeKF.FilterExpression.ToLower() == filterDefault.ToLower() && topRows == "TOP -1")
                grid_VeprimeKF.RuajDataSourceMePeriudheDheTopRowsNeSession(Session, Komponente, Periudha, dt, hfState.Get("guidString").ToString());
            if (grid_VeprimeKF.Columns.Count <= 1)
                grid_VeprimeKF.Columns.Clear();
            grid_VeprimeKF.DataSource = dt;
            grid_VeprimeKF.DataBind();
            dt.Dispose();
        }

        /// <summary>
        /// konfiguron griden
        /// </summary>
        private void KonfiguroGride()
        {
            KonfigurimComboGride.ShtoNivel(grid_VeprimeKF, 20, IdNdermarrja, IdPerdoruesi, IdGjuha, Session, Komponente, hfState.Get("guidString").ToString());
            KonfigurimComboGride.ShtoModel(grid_VeprimeKF, 20, IdNdermarrja, IdPerdoruesi, IdGjuha, Session, Komponente, hfState.Get("guidString").ToString());
            KonfigurimComboGride.ShtoMonedhe(grid_VeprimeKF, IdNdermarrja, IdPerdoruesi, Session, Komponente, hfState.Get("guidString").ToString());
            KonfigurimComboGride.ShtoStatus(grid_VeprimeKF, rm, ci);
            var col3 = grid_VeprimeKF.Columns["Vlefta"] as GridViewDataTextColumn;
            col3.PropertiesEdit.DisplayFormatString = "0.00";
            grid_VeprimeKF.Columns["#"].VisibleIndex = 0;
        }

        /// <summary>
        /// kur grida ben databound per ti shtuar kolonen e selektimit
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void grid_VeprimeKF_DataBound(object sender, EventArgs e)
        {
            if (grid_VeprimeKF.Columns["#"] == null)
            {
                GridViewCommandColumn check = new GridViewCommandColumn("#") { ShowSelectCheckbox = true, Width = Unit.Percentage(2) };
                grid_VeprimeKF.Settings.ShowFilterRow = true;
                grid_VeprimeKF.Settings.ShowHeaderFilterButton = true;
                grid_VeprimeKF.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                grid_VeprimeKF.Settings.ShowFilterRowMenu = true;
                grid_VeprimeKF.Columns.Add(check);
                grid_VeprimeKF.Settings.ShowGroupPanel = true;
                grid_VeprimeKF.KeyFieldName = "IdVeprimeKFKoka";
                grid_VeprimeKF.SettingsBehavior.AllowSelectByRowClick = true;
                grid_VeprimeKF.SettingsBehavior.AllowFocusedRow = true;
            }
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
            var cmbFiltra = ((MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;

            var filtra = new clsFiltraGrida();
            var koka = new clsGridaKoka(IdGjuha, "grid_VeprimeKF", Komponente, IdNdermarrja, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);

            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = IdPerdoruesi;
                var mesazh = filtra.fshi();

                clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, IdNdermarrja, "grid_VeprimeKF", int.Parse(cmbKonfigurimi.Value.ToString()), Komponente);
                PercaktoTemplateMenu();
                if (mesazh.Status)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                grid_VeprimeKF.FilterExpression = " [IdStatusDok]=1 ";
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
            var itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            var cmbFiltra = ((MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            var koka = new clsGridaKoka(IdGjuha, "grid_VeprimeKF", Komponente, IdNdermarrja, int.Parse(cmbKonfigurimi.Value.ToString()));
            var filtri = new clsFiltraGrida
            {
                FiltraKodi = cmbFiltra.Text,
                FiltraShenime = cmbFiltra.Text,
                FiltraUniversal = false,
                GridaKokaId = koka.IdGridaKoka,
                FiltraVlera = grid_VeprimeKF.FilterExpression,
                IdPerdoruesi = IdPerdoruesi,
                IdNdermarje = IdNdermarrja,
                IdStatusDok = 1
            };
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("NrDok", grid_VeprimeKF);
            //var kolona = grid_VeprimeKF.GetSortedColumns();
            //if (kolona.Count > 0)
            //{
            //    filtri.KoloneRenditje = kolona[0].FieldName;
            //    filtri.DrejtimRenditje = kolona[0].SortOrder == ColumnSortOrder.Ascending;
            //}
            //else
            //{
            //    filtri.KoloneRenditje = "NrDok";
            //    filtri.DrejtimRenditje = true;
            //}

            var mesazh = filtri.ruaj();
            clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, IdNdermarrja, "grid_VeprimeKF", int.Parse(cmbKonfigurimi.Value.ToString()), Komponente);
            PercaktoTemplateMenu();
            if (mesazh.Status)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

            cmbFiltra.Text = "";
        }

        /// <summary>
        /// eventet e menuse
        /// </summary>
        /// <param name="source">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {
            switch (e.Item.Name)
            {
                case "Riruaj":
                    Riruaj();
                    break;
                case "PrintPreview":
                    if (grid_VeprimeKF.FocusedRowIndex == -1)
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["regjMagMesazhZgjidhniFletePerPrintim"], pnlMesazhi);
                        Container.Attributes["src"] = "";
                    }
                    else
                    {
                        var id = grid_VeprimeKF.GetRowValues(grid_VeprimeKF.FocusedRowIndex, "IdVeprimeKFKoka").ToString();
                        var design = new clsRaportDesign();
                        design.merrSipasNdermarjedheRaport(IdNdermarrja, "FormatPrintimiVeprimeKF");
                        Container.Attributes["src"] = "RaportiShpejte.aspx?Sesioni=false&idDokumenti=" + id + "&emriReal=FormatPrintimiVeprimeKF&printo=false&raportdyte=jo&iddesign=" + design.IdRaportDesign;
                    }
                    break;
            }
        }

        protected void Riruaj()
        {
            var rreshtat = grid_VeprimeKF.GetSelectedFieldValues("IdVeprimeKFKoka");

            var dbAdmin = new clsDatabaseAdmin();

            var err = new DataTable();
            err.Columns.Add("Kodi");
            err.Columns.Add("Gabimi");
            err.Columns.Add("Rreshti");

            int nrreshta = 0;

            foreach (var id in rreshtat)
            {
                nrreshta++;
                var veprimeKfKoka = new clsVeprimeKFKoka(Convert.ToInt32(id));
                if (veprimeKfKoka.IdStatusDok == 2)
                    continue;

                var konfigurimAmbjenti = new clsKonfigurimAmbjenti(veprimeKfKoka.IdKonfigAmbjente);
                if (veprimeKfKoka.IdMonedha == 0)
                {
                    object[] arr =
                    {
                        konfigurimAmbjenti.KodKonfigAmbjente + " " + veprimeKfKoka.NrDok + " " + veprimeKfKoka.DtDok.ToShortDateString(),
                        MessagesResource.Messages["msgZgjidhniNjeMonedhe"],
                        nrreshta
                    };
                    err.Rows.Add(arr);
                    continue;
                }

                var colVeprimeKfTrupi = new colVeprimeKFTrupi();
                colVeprimeKfTrupi.MbushVeprimeKfTrupi(veprimeKfKoka.IdVeprimeKFKoka);

                var idPeriudheKontabel = clsPeriudhaKontabel.ktheIdPeriudheSipasDatesDheNdermarrjes(veprimeKfKoka.DtDok, IdNdermarrja);
                var lidhur = dbAdmin.eshteDokumentiILidhur(veprimeKfKoka.IdVeprimeKFKoka, veprimeKfKoka.IdNivel, "T_VEPRIMEKFKOKA", "IDVEPRIMKFKOKA");

                var kokaFleteKontabel = new clsKokaFleteKontabel(veprimeKfKoka.IdVeprimeKFKoka, clsKonfigurimAmbjenti.ktheIdKategori(veprimeKfKoka.IdKonfigAmbjente));
                var kokaQendraKosto = new clsKokaQendraKosto();
                kokaQendraKosto.KtheKokaQKSipasIDGjeneruesDheKonfigMeTrup(kokaFleteKontabel.IdKokaFleteKontabel, kokaFleteKontabel.IdKonfigAmbjente);

                #region krijimi i kokes se re

                var meKontabilizim = clsAlternativaKushti.getAlternativa(veprimeKfKoka.IdKonfigAmbjente, "GJK") != "Jo";
                var mefatura = clsAlternativaKushti.getAlternativa(veprimeKfKoka.IdKonfigAmbjente, "MF") == "Po";

                string kodklienti = "", kodmonedhe = "", kodllogari = "";
                if (veprimeKfKoka.IdKf != 0)
                {
                    kodklienti = new clsKlientFurnitor(veprimeKfKoka.IdKf).KodKlientFurnitor;
                }

                if (veprimeKfKoka.IdLlogKunderParti != 0)
                {
                    kodllogari = new clsLlogari(veprimeKfKoka.IdLlogKunderParti).NrLlogari;
                }

                if (veprimeKfKoka.IdMonedha != 0)
                {
                    kodmonedhe = clsMonedha.ktheKodMonedheSipasId(veprimeKfKoka.IdMonedha); //mon.KodiMonedha;
                }

                var konflidhes = new clsKonfigurimAmbjenti();
                konflidhes.mbushKonfigAmbjSipasId(konfigurimAmbjenti.IdKonfigurimi, mySessionObjects.ktheGjuhe(Session));


                string shfaqmesazhapolupe = "Jo", shfaqmesazhapolupemagazina = "Jo";

                var veprimeKfKokaNew = new clsVeprimeKFKoka();
                var mesazh = veprimeKfKokaNew.KrijoVeprimeKf(new DbData(), veprimeKfKoka.LlojVeprimi, veprimeKfKoka.NrDok, veprimeKfKoka.DtDok, veprimeKfKoka.DtRegj, veprimeKfKoka.IdKf, kodklienti, veprimeKfKoka.IdLlogKunderParti, kodllogari, veprimeKfKoka.Pershkrimi, veprimeKfKoka.IdMonedha, kodmonedhe, veprimeKfKoka.Vlefta, veprimeKfKoka.IdNderm, veprimeKfKoka.IdNderViti, 1, veprimeKfKoka.IdNivel, veprimeKfKoka.IdKonfigAmbjente, veprimeKfKoka.IdDokNga, veprimeKfKoka.IdNivelGjenerues, veprimeKfKoka.IdKonfigGjenerues, veprimeKfKoka.IdGjenerues, IdPerdoruesi, veprimeKfKoka.IdKfKunderParti, colVeprimeKfTrupi, idPeriudheKontabel, meKontabilizim, konfigurimAmbjenti.KodKonfigAmbjente, mefatura, konflidhes, out shfaqmesazhapolupe, out shfaqmesazhapolupemagazina, kokaQendraKosto.ColTrupi, veprimeKfKoka.IdVeprimeKFKoka, IdGjuha, rm, ci, veprimeKfKoka.IdDegeAdministrative);
                if (!mesazh.Status)
                {
                    object[] arr = { konfigurimAmbjenti.KodKonfigAmbjente + " " + veprimeKfKoka.NrDok + " " + veprimeKfKoka.DtDok.ToShortDateString(), mesazh.PershkrimMesazhi, nrreshta };
                    err.Rows.Add(arr);
                    continue;

                }

                veprimeKfKokaNew.IdVeprimeKFKoka = veprimeKfKoka.IdVeprimeKFKoka;

                #endregion

                mesazh = veprimeKfKokaNew.Modifiko(lidhur);
                if (!mesazh.Status)
                {
                    object[] arr = { konfigurimAmbjenti.KodKonfigAmbjente + " " + veprimeKfKoka.NrDok + " " + veprimeKfKoka.DtDok.ToShortDateString(), mesazh.PershkrimMesazhi, nrreshta };
                    err.Rows.Add(arr);
                }
            }

            mySessionObjects.ruajTabeleGabimeshImporti(Session, err);
            if (err.Rows.Count > 0)
            {
                var koka = new clsKokaErrorImporti(0, "Nga riruatja e veprime klient/furnitor ", 20, IdNdermarrja, IdPerdoruesi);
                koka.ColTrupi.mbushErrorImportiNgaProgrami(err);
                koka.ruajErrorImporti();
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "U riruajten " + (rreshtat.Count - err.Rows.Count) + " rreshta dhe deshtuan " + err.Rows.Count + " rreshta! ", pnlMesazhi);
                Container.Attributes["src"] = "RaportiShpejte.aspx?Sesioni=false&emriReal=gabimeImporti&printo=false&db=jo";
            }
            else
            {
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "U riruajten te gjitha rreshtat!", pnlMesazhi);

            }

            grid_VeprimeKF.Selection.UnselectAll();
            MbushGridNgaDb();
            KonfiguroGride();
            dbAdmin.Dispose();

            if (rreshtat.Count == 0)
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["regjMagMesazhZgjidhniNje"], pnlMesazhi);
        }

        /// <summary>
        /// fshin rreshtat e selektuar
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            List<string> teFshire = new List<string>(), teLidhur = new List<string>(), periudheKycur = new List<string>(), closedPeriod = new List<string>();
            var rreshtat = grid_VeprimeKF.GetSelectedFieldValues("IdVeprimeKFKoka");

            var dbAdmin = new clsDatabaseAdmin();
            foreach (var id in rreshtat)
            {
                var kok = new clsVeprimeKFKoka(Convert.ToInt32(id));
                var lidhur = dbAdmin.eshteDokumentiILidhur(kok.IdVeprimeKFKoka, kok.IdNivel, "T_VEPRIMEKFKOKA", "IDVEPRIMKFKOKA");
                var autorizimet = clsVeprimeKFKoka.KaAutorizime(kok.IdVeprimeKFKoka, mySessionObjects.ktheIdPerdoruesi(Session));
                if (!autorizimet)
                    lidhur = true;

                if (lidhur)
                {
                    teLidhur.Add(kok.NrDok);
                    continue;
                }

                if (kok.IdStatusDok == 2)
                    continue;

                var ekycur = clsPeriudhaKontabel.eshteKycurPeriudheSipasDateDheNdermarrjes(kok.DtDok, IdNdermarrja);
                if (ekycur)
                {
                    periudheKycur.Add(kok.NrDok);
                    continue;
                }

                if (DbCore.MbylljePeriudhe.PeriodClosing.IsPeriodClosed(kok.DtDok, MyConnectionsManager.GetSelectedConNameServer(), IdNdermarrja, KategoriDokumenti.VeprimeKlientFurnitor, kok.IdKonfigAmbjente))
                {
                    closedPeriod.Add(kok.NrDok);
                    continue;
                }

                kok.IdPerdoruesi = IdPerdoruesi;

                var mesazh = kok.Fshi();
                if (mesazh.Status)
                {
                    HiqNgaGrida(kok.IdVeprimeKFKoka);
                    grid_VeprimeKF.HiqRreshtaNgaSessioniPerGjithePeriudhat(Komponente, IdViti, Session, kok.IdVeprimeKFKoka, hfState.Get("guidString").ToString());
                    teFshire.Add(kok.NrDok);
                }
            }
            dbAdmin.Dispose();

            string mesazhInfoGabimLidhur = "", mesazhInfoGabimPeridheKycur = "", mesazhInfoSukses = "", mesazhClosedPeriod = "";

            if (teLidhur.Count == 1)
                mesazhInfoGabimLidhur = string.Format("{0}{1}{2}", rm.GetString("regjisDokPrefixMesazhNjejes", ci), string.Join(";", teLidhur), rm.GetString("msgListaLidhjaSuffixNjejesGabimi", ci));
            else
                if (teLidhur.Count > 1)
                mesazhInfoGabimLidhur = string.Format("{0}{1}{2}", rm.GetString("regjisDokprefixMesazhShumes", ci), string.Join(";", teLidhur), rm.GetString("regjMagSuffixMesazhShumesLidhurGabimi", ci));
            if (periudheKycur.Count == 1)
                mesazhInfoGabimPeridheKycur = string.Format("{0}{1}{2}", rm.GetString("regjisDokPrefixMesazhNjejes", ci), string.Join(";", periudheKycur), rm.GetString("suffixMesazhNjejesPeriudheKycurGabimi", ci));
            else
                if (periudheKycur.Count > 1)
                mesazhInfoGabimPeridheKycur = string.Format("{0}{1}{2}", rm.GetString("regjisDokprefixMesazhShumes", ci), string.Join(";", periudheKycur), rm.GetString("suffixMesazhShumesPeriudheKycurGabimi", ci));

            if (closedPeriod.Count == 1)
                mesazhClosedPeriod = String.Format("{0}{1}{2}", rm.GetString("regjisDokPrefixMesazhNjejes", ci), String.Join(", ", closedPeriod), rm.GetString("suffixMesazhClosedPeriodNjejes", ci));
            else if (closedPeriod.Count > 1)
                mesazhClosedPeriod = String.Format("{0}{1}{2}", rm.GetString("regjisDokPrefixMesazhNjejes", ci), String.Join(", ", closedPeriod), rm.GetString("suffixMesazhClosedPeriodShumes", ci));

            if (teFshire.Count == 1)
                mesazhInfoSukses = string.Format("{0}{1}{2}", rm.GetString("regjisDokPrefixMesazhNjejes", ci), string.Join(";", teFshire), rm.GetString("msgCeljeMagazinatSuffixNjejesSuksesi", ci));
            else
                if (teFshire.Count > 1)
                mesazhInfoSukses = string.Format("{0}{1}{2}", rm.GetString("regjisDokprefixMesazhShumes", ci), string.Join(";", teFshire), rm.GetString("regjMagSuffixMesazhShumesSuksesi", ci));

            mesazhInfoGabimLidhur += mesazhInfoGabimPeridheKycur + mesazhClosedPeriod;

            if (mesazhInfoGabimLidhur != "" && mesazhInfoSukses != "")
            {
                mesazhInfoGabimLidhur += MessagesResource.Messages["regjMagLidhesMesazhi"] + mesazhInfoSukses;
                pergjigja.Text = mesazhInfoGabimLidhur;
                pergjigja.ClientVisible = false;
            }
            else
            {
                pergjigja.Text = mesazhInfoSukses;
                pergjigja.ClientVisible = false;
            }

            if (rreshtat.Count == 0)
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["regjMagMesazhZgjidhniNje"], pnlMesazhi);
            else if (mesazhInfoGabimLidhur != "")
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhInfoGabimLidhur, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazhInfoSukses, pnlMesazhi);
        }

        /// <summary>
        /// heq nga grida reshtin e fshire
        /// </summary>
        /// <param name="idkoka">id e reshtit te fshire</param>
        private void HiqNgaGrida(int idkoka)
        {
            if (grid_VeprimeKF.DataSource != null)
            {
                var dt = (DataTable)grid_VeprimeKF.DataSource;
                var drs = dt.Select("IdVeprimeKFKoka = " + idkoka);
                if (drs.Length > 1)
                    throw new MyException(MessagesResource.Messages["msgVeprimeKFNdodhen2DokEPMeTeNjejtenIDNeGride"]);

                if (drs.Length == 0)
                    return;

                dt.Rows.Remove(drs[0]);
                grid_VeprimeKF.DataSource = dt;
                grid_VeprimeKF.DataBind();
                dt.Dispose();
            }
            else
                MbushGridNgaDb();
        }

        /// <summary>
        /// kur grida ben callback
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void grid_VeprimeKF_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            if (e.CallbackName == "APPLYFILTER" && e.Args[0] == "")
            {
                MbushGridNgaDb(true);
            }
            else if (this.NdryshimFiltriGrida(grid_VeprimeKF.ID))
            {
                if (e.CallbackName == "APPLYCOLUMNFILTER" || e.CallbackName == "APPLYMULTICOLUMNFILTER")
                    MbushGridNgaDb(true);
                else MbushGridNgaDb();
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(IdNdermarrja, "grid_VeprimeKF", grid_VeprimeKF, cmbKonfigurimi.Text.Split(';')[0], "652", IdGjuha, false);
            }
            else if (Request.Params["__CALLBACKPARAM"].Contains("changeConfig"))
            {
                var filterDefault = clsFiltraGrida.MerrFilterDefault(Convert.ToInt32(cmbKonfigurimi.Value));
                if (filterDefault != null && filterDefault.IdFiltra != 0)
                    grid_VeprimeKF.FilterExpression = filterDefault.FiltraVlera;
                grid_VeprimeKF.Columns.Clear();
                grid_VeprimeKF.AutoGenerateColumns = true;
                MbushGridNgaDb();
                KonfiguroGride();
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(IdNdermarrja, "grid_VeprimeKF", grid_VeprimeKF, cmbKonfigurimi.Text.Split(';')[0], "652", IdGjuha);
                grid_VeprimeKF.Columns["#"].VisibleIndex = 0;
            }
            else if (e.CallbackName == "COLUMNMOVE" && grid_VeprimeKF.AllColumns[int.Parse(e.Args[0])].Width.Value == 0)
            {
                grid_VeprimeKF.AllColumns[int.Parse(e.Args[0])].Width = Unit.Percentage(3);
                return;
            }
            else if (e.CallbackName == "APPLYFILTER" && e.Args[0] == "")
            {
                var itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
                var cmbFiltra = ((MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
                cmbFiltra.Text = "";
                grid_VeprimeKF.FilterExpression = "[IdStatusDok]=1";
            }
            grid_VeprimeKF.SaveFilter(IdNdermarrja, Request.QueryString["kf"]);
        }

        /// <summary>
        /// kur perdoruesi i ben grides callbak per te mare konfigurimin e ri
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void grid_VeprimeKF_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (grid_VeprimeKF.AplikoFilterDefault(e, Convert.ToInt32(cmbKonfigurimi.Value)))
                return;
            var arr = e.Parameters.Split(';');
            bool getFromDb = false;
            string filterDefault = " [IdStatusDok]=1 ";
            switch (arr.Length)
            {
                case 1:
                    if (this.NdryshimFiltriGrida(grid_VeprimeKF.ID))
                        return;
                    if (arr[0] == "ndryshimMenyreFiltrimi" && !string.IsNullOrEmpty(grid_VeprimeKF.FilterExpression))
                        getFromDb = true;
                    break;
                case 2:
                    string periudheDok = clsAlternativaKushti.getAlternativa(Convert.ToInt32(cmbKonfigurimi.Value), "SHDPER");
                    string datanga, dataderi;
                    clsFunksione.ruajPeriudhatNeHiddenField(IsPostBack, hfState, periudheDok, null, out datanga, out dataderi);
                    MbushGridNgaSession();
                    break;
                case 3:
                    if (arr[2] == "" || arr[2] == "changeConfig")
                    {
                        grid_VeprimeKF.FilterExpression = filterDefault;
                        break;
                    }
                    else
                    {
                        var filtra = new clsFiltraGrida();
                        var koka = new clsGridaKoka(IdGjuha, "grid_VeprimeKF", Komponente, IdNdermarrja, int.Parse(cmbKonfigurimi.Value.ToString()));
                        filtra.mbushFilterPerGrideSipasKodit(arr[2], IdNdermarrja, koka.IdGridaKoka);
                        if (filtra.FiltraKodi != null)
                        {
                            grid_VeprimeKF.FilterExpression = filtra.FiltraVlera;
                            GridUtil.renditGriden(filtra.KoloneRenditje, grid_VeprimeKF);
                        }
                    }
                    break;
            }
            grid_VeprimeKF.Selection.UnselectAll();
        }

        /// <summary>
        /// perdoret per te vendosur karakteristika te griden ne javascript
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void grid_VeprimeKF_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = grid_VeprimeKF.PageIndex;
            e.Properties["cpPageRow"] = grid_VeprimeKF.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = grid_VeprimeKF.VisibleRowCount;
        }

        /// <summary>
        /// perdoret per te hequr filtrimin e grides kur zgjedhim elementin bosh
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void grid_VeprimeKF_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if ((e.Column.FieldName == "IdKF"
                || e.Column.FieldName == "IdMonedha"
                || e.Column.FieldName == "IdKonfigAmbjente")
                && Converter.ConvertToInt(e.Value) == 0)
                e.Criteria = null;
        }

        /// <summary>
        /// perdoret per te shtuar menyra filtrimi
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void grid_VeprimeKF_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            var teGjithe = MessagesResource.Messages["GridHeaderFilterFillItemTeGjithe"];
            var nga = MessagesResource.Messages["GridHeaderFilterFillItemNga"];
            if (e.Column.FieldName == "NrDok")
            {
                e.Values.Clear();
                e.AddValue(teGjithe, string.Empty, "true");
                e.AddValue(nga + " A-D ", string.Empty, String.Format("{0}>'A     ' and {0} <'DDDDDDD'", e.Column.FieldName));
                e.AddValue(nga + " D-G ", string.Empty, String.Format("{0}>'D     ' and {0}<'GGGGGGG'", e.Column.FieldName));
                e.AddValue(nga + " H-K ", string.Empty, String.Format("{0}>'H     ' and {0}<'KKKKKKK'", e.Column.FieldName));
                e.AddValue(nga + " L-O ", string.Empty, String.Format("{0}>'L     ' and {0}  <'OOOOOOO'", e.Column.FieldName));
                e.AddValue(nga + " P-S ", string.Empty, String.Format("{0}>'P     ' and {0}<'SSSSSSS'", e.Column.FieldName));
                e.AddValue(nga + " T-W ", string.Empty, String.Format("{0}>'T     ' and {0}<'WWWWWWW'", e.Column.FieldName));
                e.AddValue(nga + " X-Z ", string.Empty, String.Format("{0}>'X     ' and {0}<'ZZZZZZZ'", e.Column.FieldName));
            }
            else
            {
                e.Values.Clear();
                e.AddValue(teGjithe, string.Empty, "true");
            }
        }

        protected void radDtDok_PreRender(object sender, EventArgs e)
        {
            ASPxRadioButtonList radDtDok = sender as ASPxRadioButtonList;
            radDtDok.SelectedItem = radDtDok.Items.FindByValue(hfState.Get("Periudha").ToString());
        }
    }
}
