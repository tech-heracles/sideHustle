using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Web.UI.WebControls;
using DbCore;
using DbCore.DbAdmin;
using DbCore.DbKontabiliteti;
using DbCore.DbShare;
using DbCore.IMBUtils.Messages;
using DbCore.IMBUtils.Types;
using DevExpress.Data;
using DevExpress.Web;
using NLog;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using PlatinumWeb.ApplicationUtils.Filters;
using PlatinumWeb.Templates;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using DbCore.IMBUtils.DataBase;

namespace PlatinumWeb
{
    public partial class FleteKontabel : MyPageBase
    {
        private const string Komponente = "FleteKontabel.aspx";
        private string _guidString;
        private TitlePeriudha _periudha;
        public TitlePeriudha Periudha => _periudha ?? (_periudha = this.MerrPeriudhe(hfState));
        protected void Page_Load(object sender, EventArgs e)
        {
            var cultinf = mySessionObjects.ktheCultureInfo(Session);
            var rm = new ResourceManager("Resources.Strings", Assembly.Load("App_GlobalResources"));
            int idgjuha, idviti, idNdermarrje, idPerdoruesi, idNdermarrjeVit;
            string datanga, dataderi;
            var komponente = clsFunksione.GetKomponente(Page.Request);
            if (hfState.Count == 0)
            {
                idPerdoruesi = mySessionObjects.ktheIdPerdoruesi(Session);
                idgjuha = mySessionObjects.ktheGjuhe(Session);
                idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
                idviti = mySessionObjects.ktheIdVitNdermarrje(Session);
                idNdermarrjeVit = mySessionObjects.ktheNdermarrjeVit(Session);
                idNdermarrjeVit = mySessionObjects.ktheNdermarrjeVit(Session);
                hfState.Set("idPerdoruesi", idPerdoruesi);
                hfState.Set("idGjuha", idgjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("idViti", idviti);
                hfState.Set("idNdermarrjeVit", idNdermarrjeVit);
            }
            else
            {
                idPerdoruesi = (int)hfState["idPerdoruesi"];
                idNdermarrje = (int)hfState["idNdermarrje"];
                idviti = (int)hfState["idViti"];
                idgjuha = (int)hfState["idGjuha"];
                idNdermarrjeVit = (int)hfState["idNdermarrjeVit"];
            }
            PercaktoTemplateMenu(idgjuha, ASPxMenu1, idviti, idPerdoruesi, idNdermarrje);
            if (!IsPostBack)
            {
                if (!mySessionObjects.isLogedIn(Session))
                {
                    clsFunksione.logout(Session, true, "FaqePaautorizuar");
                }
                if (mySessionObjects.ktheKodNdermarrje(Session) == null)
                {
                    Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerdoruesi);
                }
                _guidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray()); //Guid.NewGuid().ToString();
                hfState["guidString"] = _guidString;
                EmrateButonave();
                MbushHiddenFieldMePerkthime();
                if (Request.QueryString["ruaj"] == "ok")
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["labelRaportMesazhRuajtjaPerfundoiSukses"], pnlMesazhi);
                else if (Request.QueryString["ruaj"] == "po")
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["regjMagModifikimiPerfundoiMeSukses"], pnlMesazhi);
                                ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 50, rm, cultinf, idgjuha);
                cmbKonfigurimi.SelectedIndex = 0;
                var konf = new clsKonfigurimAmbjenti();
                konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()), idgjuha);
                hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;
                var periudheDok = clsAlternativaKushti.getAlternativa(konf.IdKonfigAmbjente, "SHDPER");
                var oPeriudha = mySessionObjects.merrPeriudheKontabel(Session);
                clsFunksione.ruajPeriudhatNeHiddenField(IsPostBack, hfState, periudheDok, oPeriudha, out datanga, out dataderi);
                mySessionObjects.ruajGrideNeSession(komponente + "&periudhaDok=Aktuale" + idNdermarrjeVit, Session, null);
                mySessionObjects.ruajGrideNeSession(komponente + "&periudhaDok=3 Mujore" + idNdermarrjeVit, Session, null);
                mySessionObjects.ruajGrideNeSession(komponente + "&periudhaDok=Vit ushtrimor" + idNdermarrjeVit, Session, null);
                mySessionObjects.ruajGrideNeSession(komponente + "&periudhaDok=Javore" + idNdermarrjeVit, Session, null);
                mySessionObjects.ruajGrideNeSession(komponente + "&periudhaDok=Ditore" + idNdermarrjeVit, Session, null);
                gvFleteKontabelKoka.PercaktoTitlePanelPerRegjistrimDokumentash(this, MenuInfo, pnlMesazhi, hfState, idPerdoruesi, idNdermarrje, IdViti, idgjuha, int.Parse(cmbKonfigurimi.Value.ToString()), "FleteKontabel.aspx", 117, "IdKokaFleteKontabel", rm, ci);

                MbushGridFleteKontabelNgaDb(komponente, idPerdoruesi, Periudha.PeriudhaDok, Periudha.DataDokNga, Periudha.DataDokDeri);
                gvFleteKontabelKoka.FilterExpression = "[IdStatusDokumenti]=1";
                KonfiguroGride(idPerdoruesi, idNdermarrje, idgjuha, rm, cultinf);
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "gvFleteKontabelKoka", gvFleteKontabelKoka, cmbKonfigurimi.Text.Split(';')[0], "117", idgjuha);
                if (Request.QueryString["fshi"] == "jo")
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, Request.QueryString["mesazh"], pnlMesazhi);
                else if (Request.QueryString["fshi"] == "po")
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, Request.QueryString["mesazh"], pnlMesazhi);
                var tedrejtaInfo = new clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idviti, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
            }
            else
            {
                gvFleteKontabelKoka.PercaktoTitlePanelPerRegjistrimDokumentash(this, MenuInfo, pnlMesazhi, hfState, idPerdoruesi, idNdermarrje, IdViti, idgjuha, Convert.ToInt32(cmbKonfigurimi.Value), "FleteKontabel.aspx", 117, "IdKokaFleteKontabel", rm, ci);
                _guidString = (string)hfState["guidString"];
                MbushGridFleteKontabelNgaSession(komponente, idPerdoruesi, Periudha.PeriudhaDok, Periudha.DataDokNga, Periudha.DataDokDeri);
                Container.Attributes["src"] = "";
                KonfiguroGride(idPerdoruesi, idNdermarrje, idgjuha, rm, cultinf);
            }

            GridUtil.konfigGrideListeEMadhePaTheme(gvFleteKontabelKoka, "IdKokaFleteKontabel");

            AspxWebControlUtils.perkthePopUp(popFshi, rm.GetString("labelKujdes", cultinf), lblMsgbox, MessagesResource.Messages["labelAdministrimiMsgJeniSigurt"], ButtonCancel, MessagesResource.Messages["labelAnullo"]);
            clsToolbarConfig.mbushComboBoxFiltra(mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvFleteKontabelKoka", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
            Container.Attributes["src"] = "";
        }

        /// <summary>
        /// Vendos emrat e butonave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateButonave()
        {
            ButtonCancel.Text = MessagesResource.Messages["labelBlerjeShitjeAnullo"];
            konfigurimi_Label.Text = MessagesResource.Messages["lblModeli"];
        }

        /// <summary>
        /// Metode qe sherben per te kaluar perkthime te js
        /// </summary>
        /// <param name="cultinf">Merr culture info</param>
        /// <param name="rm">Merr Resource Manager</param>
        private void MbushHiddenFieldMePerkthime()
        {
            hfState.Set("msgNukMundTeKlononi", MessagesResource.Messages["msgNukMundTeKlononi"]);
            hfState.Set("zgjidhGrupinEKontabilizimit", MessagesResource.Messages["zgjidhGrupinEKontabilizimit"]);
            hfState.Set("msgZgjdhniNjeNgaElementetEListes", MessagesResource.Messages["msgZgjdhniNjeNgaElementetEListes"]);
        }

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        /// <param name="idViti"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNermarrje"></param>
        private void PercaktoTemplateMenu(int idGjuha, ASPxMenu aSPxMenu1, int idViti, int idPerdorues, int idNdermarrje) =>
            clsToolbarConfig.percaktoTemplateMenu(idGjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1, Komponente, this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, true, false, false, mySessionObjects.merrEshteMemeSesioni(Session));

        protected void RuajKolona_Click(object sender, EventArgs e)
        {
            var idfiltri = 0;
            var idndermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
            var idperdorues = mySessionObjects.ktheIdPerdoruesi(Session);
            var idViti = mySessionObjects.ktheIdVitNdermarrje(Session);
            var idgjuha = mySessionObjects.ktheGjuhe(Session);
            var idkonf = clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(cmbKonfigurimi.Text.Split(';')[0], idndermarrje);
            var  mesazh = GridUtil.ruajkonfigurimgride(gvFleteKontabelKoka, cmbKonfigurimi.Text, idndermarrje, idperdorues, 117, idfiltri, idViti, mySessionObjects.ktheCultureInfo(Session), mySessionObjects.ktheGjuhe(Session));// ruan konfigurimin e grides dhe filtrin
            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                return;
            }
             mesazh = GridUtil.ruajFiltra(idndermarrje, idperdorues, idgjuha, "gvFleteKontabelKoka", Komponente, "FilterDefault", gvFleteKontabelKoka.FilterExpression, gvFleteKontabelKoka, "IdNivel", idkonf, out idfiltri);//ruan filtrin e zgjedhur tek filtrat
            clsToolbarConfig.mbushComboBoxFiltra(idgjuha, idndermarrje, "gvFleteKontabelKoka", int.Parse(cmbKonfigurimi.Value.ToString()), Komponente);
            PercaktoTemplateMenu(idgjuha, ASPxMenu1, idViti, idperdorues, idndermarrje);
            if (mesazh.Status) clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

        }

        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e) =>
            PercaktoTemplateMenu((int)hfState["idGjuha"], ASPxMenu1, (int)hfState["idViti"], (int)hfState["idPerdoruesi"], (int)hfState["idNdermarrje"]);

        private void MbushGridFleteKontabelNgaSession(string komponente, int idPerdoruesi, string periudheDok, string datanga, string dataderi)
        {
            var periudha = this.MerrPeriudhe(hfState);
            DataTable tmpObject = gvFleteKontabelKoka.MerrDataSourceMePeriduheNeSession<DataTable>(Session, komponente, Periudha, _guidString);
            if (tmpObject == null)
            {

                MbushGridFleteKontabelNgaDb(komponente, idPerdoruesi, Periudha.PeriudhaDok, Periudha.DataDokNga, Periudha.DataDokDeri);

            }

            else
            {

                gvFleteKontabelKoka.DataSource = tmpObject;
                gvFleteKontabelKoka.DataBind();
                tmpObject.Dispose();
            }
        }

        /// <summary>
        /// mbush griden e popupit me te dhena
        /// </summary>
        /// <param name="komponente"></param>
        /// <param name="idPerdoruesi"></param>
        /// <param name="periudheDok"></param>
        /// <param name="datanga"></param>
        /// <param name="dataderi"></param>
        private void MbushGridFleteKontabelNgaDb(string komponente, int idPerdoruesi, string periudheDok, string datanga, string dataderi)
        {
            var dt = colKokatFletetKontabel.merrFleteKontabelDT(mySessionObjects.ktheNdermarrjeVit(Session), mySessionObjects.ktheIdPerdoruesi(Session), Periudha.DataDokNga, Periudha.DataDokDeri);

            var periudha = this.MerrPeriudhe(hfState);
            gvFleteKontabelKoka.RuajDataSourceMePeriduheNeSession(Session, komponente, periudha, dt, _guidString);
            gvFleteKontabelKoka.DataSource = dt;
            gvFleteKontabelKoka.DataBind();
            dt.Dispose();
        }

        /// <summary>
        /// shton colonen # per selektim dhe disa karakteristika te grides
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvFleteKontabelKoka_DataBound(object sender, EventArgs e)
        {
            if (gvFleteKontabelKoka.Columns["#"] != null) return;
            //behet nepermjet kodit afishimi i checkboxit qe do perdoret per 
            //perzgjidh
            var check = new GridViewCommandColumn("#")
            {
                ShowSelectCheckbox = true,
                Width = Unit.Percentage(2)
            };
            gvFleteKontabelKoka.Settings.ShowFilterRow = true;
            gvFleteKontabelKoka.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
            gvFleteKontabelKoka.Settings.ShowFilterRowMenu = true;
            gvFleteKontabelKoka.Columns.Add(check);

            gvFleteKontabelKoka.KeyFieldName = "IdKokaFleteKontabel";
            gvFleteKontabelKoka.SettingsBehavior.AllowSelectByRowClick = true;
            gvFleteKontabelKoka.SettingsBehavior.AllowFocusedRow = true;
        }

        private void KonfiguroGride(int idPerdoruesi, int idNdermarrje, int idGjuha, ResourceManager rm, CultureInfo ci)
        {
            KonfigurimComboGride.ShtoGrupetKontabilizimit(gvFleteKontabelKoka, idNdermarrje, Session, Komponente, _guidString);
            KonfigurimComboGride.ShtoSkemaKontabel(gvFleteKontabelKoka, idNdermarrje, idPerdoruesi, Session, Komponente, _guidString);
            KonfigurimComboGride.ShtoNivel(gvFleteKontabelKoka, 5, idNdermarrje, idPerdoruesi, idGjuha, Session, Komponente, _guidString);
            KonfigurimComboGride.ShtoModel(gvFleteKontabelKoka, 5, idNdermarrje, idPerdoruesi, idGjuha, Session, Komponente, _guidString);
            KonfigurimComboGride.ShtoStatus(gvFleteKontabelKoka, rm, ci, "IdStatusDokumenti");
            ShtoKonfigurimGjenerues(idPerdoruesi, idNdermarrje, idGjuha);

            var col3 = gvFleteKontabelKoka.Columns["VleftaFleteKontabel"] as GridViewDataTextColumn;
            col3.PropertiesEdit.DisplayFormatString = "0.00";
            gvFleteKontabelKoka.Columns["#"].VisibleIndex = 0;
        }

        private void ShtoKonfigurimGjenerues(int idperdoruesi, int idNdermarrje, int idGjuha)
        {
            gvFleteKontabelKoka.KonfiguroCombo("IdKonfigGjenerues", "IdKonfigAmbjente", "KodKonfigAmbjente", () =>
            {
                var colKonf = new colKonfigurimAmbjenti();

                colKonf.mbushGjitheKonfigurimeAmbjentesh(idNdermarrje, idperdoruesi, 2, idGjuha);
                var konfmag = new clsKonfigurimAmbjenti();
                konfmag.mbushKonfigAmbjSipasKod("MAG", idNdermarrje);
                colKonf.Add(konfmag);
                return colKonf;
            }, Session, Komponente, _guidString);
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
            var cmbFiltra = ((MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;

            var filtra = new clsFiltraGrida();
            var idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
            var koka = new clsGridaKoka(mySessionObjects.ktheGjuhe(Session), "gvFleteKontabelKoka", Komponente, mySessionObjects.merrIdNdermarrjeSesioni(Session), int.Parse(cmbKonfigurimi.Value.ToString()));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
            if (filtra.FiltraKodi == null) return;
            filtra.IdPerdoruesi = mySessionObjects.ktheIdPerdoruesi(Session);
            var mesazh = new clsMesazh();
            mesazh = filtra.fshi();
            clsToolbarConfig.mbushComboBoxFiltra(mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvFleteKontabelKoka", int.Parse(cmbKonfigurimi.Value.ToString()), Komponente);
            PercaktoTemplateMenu((int)hfState["idGjuha"], ASPxMenu1, (int)hfState["idViti"], (int)hfState["idPerdoruesi"], idNdermarrje);
            if (mesazh.Status)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            cmbFiltra.Text = "";
            gvFleteKontabelKoka.FilterExpression = "[IdStatusDokumenti]=1";
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


            var filtri = new clsFiltraGrida
            {
                FiltraKodi = cmbFiltra.Text,
                FiltraShenime = cmbFiltra.Text,
                FiltraUniversal = false
            };
            var idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
            var koka = new clsGridaKoka(mySessionObjects.ktheGjuhe(Session), "gvFleteKontabelKoka", Komponente, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gvFleteKontabelKoka.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("NrDukumentiKokaFleteKontabel", gvFleteKontabelKoka);
            //var kolona = gvFleteKontabelKoka.GetSortedColumns();
            //if (kolona.Count > 0)
            //{
            //    filtri.KoloneRenditje = kolona[0].FieldName;
            //    filtri.DrejtimRenditje = kolona[0].SortOrder == ColumnSortOrder.Ascending;
            //}
            //else
            //{
            //    filtri.KoloneRenditje = "NrDukumentiKokaFleteKontabel";
            //    filtri.DrejtimRenditje = true;
            //}
            filtri.IdPerdoruesi = mySessionObjects.ktheIdPerdoruesi(Session);
            filtri.IdNdermarje = idNdermarrje;
            var mesazh = new clsMesazh();
            filtri.IdStatusDok = 1;
            mesazh = filtri.ruaj();
            clsToolbarConfig.mbushComboBoxFiltra(mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvFleteKontabelKoka", int.Parse(cmbKonfigurimi.Value.ToString()), Komponente);
            PercaktoTemplateMenu((int)hfState["idGjuha"], ASPxMenu1, (int)hfState["idViti"], (int)hfState["idPerdoruesi"], idNdermarrje);
            if (mesazh.Status)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            cmbFiltra.Text = "";
        }

        /// <summary>
        /// thirret sa here qe behet Callback, dhe ben edhe njeher lidhjen me datasource-in
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvFleteKontabelKoka_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            if (e.CallbackName == "COLUMNMOVE" && gvFleteKontabelKoka.AllColumns[int.Parse(e.Args[0])].Width.Value == 0)
                gvFleteKontabelKoka.AllColumns[int.Parse(e.Args[0])].Width = Unit.Percentage(3);
            if (e.CallbackName == "APPLYFILTER" && e.Args[0] == "")
            {
                var itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
                var cmbFiltra = ((MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
                cmbFiltra.Text = "";
            }
            percaktoTamplate();
            var cultinf = mySessionObjects.ktheCultureInfo(Session);
            var rm = new ResourceManager("Resources.Strings", Assembly.Load("App_GlobalResources"));
           
            GridUtil.ToolTipButonaveMbiGride(gvFleteKontabelKoka, cultinf, rm);
        }

        /// <summary>
        /// sherben per te vendosur filtra tek header-i i grides (mund te ndryshohet me vone per tu bere  me e konfigurueshme
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvFleteKontabelKoka_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            var TeGjithe = MessagesResource.Messages["GridHeaderFilterFillItemTeGjithe"];
            var nga = MessagesResource.Messages["GridHeaderFilterFillItemNga"];
            if (e.Column.FieldName == "PershkrimKokaFleteKontabel")
            {
                e.Values.Clear();
                e.AddValue(TeGjithe, string.Empty, "true");
                e.AddValue(nga + " A-D ", string.Empty, e.Column.FieldName + ">'A     ' and " + e.Column.FieldName + " <'DDDDDDD'");
                e.AddValue(nga + " D-G ", string.Empty, e.Column.FieldName + ">'D     ' and " + e.Column.FieldName + "<'GGGGGGG'");
                e.AddValue(nga + " H-K ", string.Empty, e.Column.FieldName + ">'H     ' and " + e.Column.FieldName + "<'KKKKKKK'");
                e.AddValue(nga + " L-O ", string.Empty, e.Column.FieldName + ">'L     ' and " + e.Column.FieldName + "  <'OOOOOOO'");
                e.AddValue(nga + " P-S ", string.Empty, e.Column.FieldName + ">'P     ' and " + e.Column.FieldName + "<'SSSSSSS'");
                e.AddValue(nga + " T-W ", string.Empty, e.Column.FieldName + ">'T     ' and " + e.Column.FieldName + "<'WWWWWWW'");
                e.AddValue(nga + " X-Z ", string.Empty, e.Column.FieldName + ">'X     ' and " + e.Column.FieldName + "<'ZZZZZZZ'");
            }
            else
            {
                e.Values.Clear();
                e.AddValue(TeGjithe, string.Empty, "true");
            }
        }

        /// <summary>
        /// ben fshirjen e rreshtave te selektuar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            var cultinf = mySessionObjects.ktheCultureInfo(Session);
            var rm = new ResourceManager("Resources.Strings", Assembly.Load("App_GlobalResources"));
            var rreshtat = gvFleteKontabelKoka.GetSelectedFieldValues("IdKokaFleteKontabel");
            if (rreshtat.Count == 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["mesazhZgjidhniNje"], pnlMesazhi);
                return;
            }
            List<string> teFshire = new List<string>(), teLidhur = new List<string>(), periudheKycur = new List<string>(), closedPeriod = new List<string>();
            var idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
            var idPerdoruesi = mySessionObjects.ktheIdPerdoruesi(Session);
            foreach (var id in rreshtat)
            {
                var clsKoka = new clsKokaFleteKontabel(Convert.ToInt32(id));
                var ekycur = clsPeriudhaKontabel.eshteKycurPeriudheSipasDateDheNdermarrjes(clsKoka.DateDokumentiKokaFleteKontabel, idNdermarrje);
                if (ekycur)
                {
                    periudheKycur.Add(clsKoka.NrDukumentiKokaFleteKontabel);
                    continue;
                }

                if (DbCore.MbylljePeriudhe.PeriodClosing.IsPeriodClosed(clsKoka.DateDokumentiKokaFleteKontabel, MyConnectionsManager.GetSelectedConNameServer(), IdNdermarrja, DbCore.DbRegjistrim.KategoriDokumenti.FleteKontabel, clsKoka.IdKonfigAmbjente))
                {
                    closedPeriod.Add(clsKoka.NrDukumentiKokaFleteKontabel);
                    continue;
                }

                var lidhur = clsKoka.EshteILidhur();
                if (lidhur)
                {
                    teLidhur.Add(clsKoka.NrDukumentiKokaFleteKontabel);
                    continue;
                }
                if (clsKoka.IdStatusDokumenti == 2)
                    continue;

                var mesazhi = clsKoka.Fshiupd();
                if (!mesazhi.Status) continue;
                HiqFleteKontabelNgaGrida(idPerdoruesi, clsKoka.IdKokaFleteKontabel);
                teFshire.Add(clsKoka.NrDukumentiKokaFleteKontabel);
            }
            string mesazhInfoGabimLidhur = "", mesazhInfoGabimPeridheKycur = "", mesazhInfoSukses = "", mesazhClosedPeriod = "";
            if (teLidhur.Count == 1)
                mesazhInfoGabimLidhur = String.Format("{0}{1}{2}", rm.GetString("prefixMesazhNjejes", cultinf), String.Join(";", teLidhur), rm.GetString("suffixMesazhNjejesLidhurGabimi", cultinf));
            else
                if (teLidhur.Count > 1)
                mesazhInfoGabimLidhur = String.Format("{0}{1}{2}", rm.GetString("prefixMesazhShumes", cultinf), String.Join(";", teLidhur), rm.GetString("suffixMesazhShumesLidhurGabimi", cultinf));
            if (periudheKycur.Count == 1)
                mesazhInfoGabimPeridheKycur = String.Format("{0}{1}{2}", rm.GetString("prefixMesazhNjejes", cultinf), String.Join(";", periudheKycur), rm.GetString("suffixMesazhNjejesPeriudheKycurGabimi", cultinf));
            else
                if (periudheKycur.Count > 1)
                mesazhInfoGabimPeridheKycur = String.Format("{0}{1}{2}", rm.GetString("prefixMesazhShumes", cultinf), String.Join(";", periudheKycur), rm.GetString("suffixMesazhShumesPeriudheKycurGabimi", cultinf));

            if (closedPeriod.Count == 1)
                mesazhClosedPeriod = String.Format("{0}{1}{2}", rm.GetString("regjisDokPrefixMesazhNjejes", ci), String.Join(", ", closedPeriod), rm.GetString("suffixMesazhClosedPeriodNjejes", ci));
            else if (closedPeriod.Count > 1)
                mesazhClosedPeriod = String.Format("{0}{1}{2}", rm.GetString("regjisDokPrefixMesazhNjejes", ci), String.Join(", ", closedPeriod), rm.GetString("suffixMesazhClosedPeriodShumes", ci));

            if (teFshire.Count == 1)
                mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("prefixMesazhNjejes", cultinf), String.Join(";", teFshire), rm.GetString("suffixMesazhNjejesSuksesi", cultinf));
            else
                if (teFshire.Count > 1)
                mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("prefixMesazhShumes", cultinf), String.Join(";", teFshire), rm.GetString("suffixMesazhShumesSuksesi", cultinf));

            mesazhInfoGabimLidhur += mesazhInfoGabimPeridheKycur + mesazhClosedPeriod;
            if (mesazhInfoGabimLidhur != "" && mesazhInfoSukses != "")
                mesazhInfoGabimLidhur += rm.GetString("lidhesMesazhi", cultinf) + mesazhInfoSukses;
            if (mesazhInfoGabimLidhur != "")
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhInfoGabimLidhur, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazhInfoSukses, pnlMesazhi);
        }

        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {
            switch (e.Item.Name)
            {
                case "PrintPreview":
                    if (gvFleteKontabelKoka.FocusedRowIndex == -1)
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgZgjidhniFleteKontabelPerPrintim"], pnlMesazhi);
                        Container.Attributes["src"] = "";
                    }
                    else
                    {
                        var id = gvFleteKontabelKoka.GetRowValues(gvFleteKontabelKoka.FocusedRowIndex, "IdKokaFleteKontabel").ToString();
                        var clskokaFlete = new clsKokaFleteKontabel(int.Parse(id));
                        var konf = new clsKonfigurimAmbjenti();
                        konf.mbushKonfiguriminMeID(clskokaFlete.IdKonfigAmbjente);
                        Container.Attributes["src"] = "RaportiShpejte.aspx?Sesioni=false&emriReal=fletaKontabel&idDokumenti=" + clskokaFlete.IdKokaFleteKontabel + "&printo=false";
                    }
                    break;
                case "FshiGrup":
                    modifikimGrupi("0");
                    break;
            }
        }

        private void HiqFleteKontabelNgaGrida(int idPerdoruesi, int idkokafletekontabel)
        {
            if (gvFleteKontabelKoka.DataSource != null)
            {
                var dt = (DataTable)gvFleteKontabelKoka.DataSource;
                var drs = dt.Select("IdKokaFleteKontabel = " + idkokafletekontabel);
                if (drs.Length > 1)
                    throw new Exception(MessagesResource.Messages["msgGabimiNdodhenDyFleteKontabelMeNjeID"]);
                if (drs.Length == 0) return;
                var dr = drs[0];
                dt.Rows.Remove(dr);
                gvFleteKontabelKoka.DataSource = dt;
                gvFleteKontabelKoka.DataBind();
                dt.Dispose();
            }
            else
                MbushGridFleteKontabelNgaDb(clsFunksione.GetKomponente(Page.Request), idPerdoruesi, Periudha.PeriudhaDok, Periudha.DataDokNga, Periudha.DataDokDeri);
        }

        protected void btngrupo_Click(object sender, EventArgs e) => modifikimGrupi(txtNrGrupKontabilizimi.Text);

        private void modifikimGrupi(string grup)
        {
            var cultinf = mySessionObjects.ktheCultureInfo(Session);
            var rm = new ResourceManager("Resources.Strings", Assembly.Load("App_GlobalResources"));
            var rreshtat = gvFleteKontabelKoka.GetSelectedFieldValues("IdKokaFleteKontabel");
            List<string> joHequrGrupi = new List<string>(), hequrGrupi = new List<string>();
            var idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
            var idPerdoruesi = mySessionObjects.ktheIdPerdoruesi(Session);
            if (rreshtat.Count == 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["mesazhZgjidhniNje"], pnlMesazhi);
                return;
            }
            var tedrejtaInfo = new clsTeDrejtaRoli();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, mySessionObjects.ktheIdVitNdermarrje(Session), Komponente);
            if (!tedrejtaInfo.DMod)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgAdministrimiNukKeniTeDrejteVeprimi"], pnlMesazhi);

                return;
            }
            var status = true;
            foreach (var id in rreshtat)
            {
                var kokaFletKont = new clsKokaFleteKontabel(Convert.ToInt32(id));
                if (kokaFletKont.IdKokaFleteKontabel == 0) continue;
                if (grup != "")
                {
                    if (grup != "0")
                    {
                        kokaFletKont.IdGrupKontabilizimi = clsGrupKontabilizimi.mbushIDGrupKontabilizim(grup, idNdermarrje);
                        KonfiguroGride(idPerdoruesi, idNdermarrje, mySessionObjects.ktheGjuhe(Session), rm, cultinf);
                        clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["msgGrupiUShtuaMeSukses"], pnlMesazhi);
                    }
                    else
                    {
                        if (kokaFletKont.IdGrupKontabilizimi != 0)
                        {
                            kokaFletKont.IdGrupKontabilizimi = 0;
                            hequrGrupi.Add(kokaFletKont.NrDukumentiKokaFleteKontabel);
                        }
                        else
                        {
                            joHequrGrupi.Add(kokaFletKont.NrDukumentiKokaFleteKontabel);
                            continue;
                        }
                    }
                    var mesazh = kokaFletKont.ModifikoFleteKontabelGrupKontabilizimi();
                    status = status && mesazh.Status;
                }
                else { clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniAsnjeGrupTeCelur"], pnlMesazhi); status = false; }
            }
            string mesazhInfoGabim = "", mesazhInfoSukses = "";

            if (joHequrGrupi.Count == 1)
                mesazhInfoGabim = String.Format("{0}{1}{2}", rm.GetString("prefixMesazhNjejes", cultinf), String.Join("; ", joHequrGrupi), rm.GetString("suffixMesazhGabimGrupNjejes", cultinf));
            else
                if (joHequrGrupi.Count > 1)
                mesazhInfoGabim = String.Format("{0}{1}{2}", rm.GetString("prefixMesazhShumes", cultinf), String.Join("; ", joHequrGrupi), rm.GetString("suffixMesazhGabimGrupShumes", cultinf));
            if (hequrGrupi.Count == 1)
                mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("prefixMesazhSuksesGrupi", cultinf), rm.GetString("suffixMesazhNjejesGrupi", cultinf), String.Join("; ", hequrGrupi));
            else
                if (hequrGrupi.Count > 1)
                mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("prefixMesazhSuksesGrupi", cultinf), rm.GetString("suffixMesazhShumesGrupi", cultinf), String.Join("; ", hequrGrupi));
            if (mesazhInfoGabim != "" && mesazhInfoSukses != "")
                mesazhInfoGabim += rm.GetString("lidhesMesazhi", cultinf) + mesazhInfoSukses;
            if (mesazhInfoGabim != "")
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhInfoGabim, pnlMesazhi);
            else if (mesazhInfoSukses != "")
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazhInfoSukses, pnlMesazhi);

            MbushGridFleteKontabelNgaDb(clsFunksione.GetKomponente(Page.Request), idPerdoruesi, Periudha.PeriudhaDok, Periudha.DataDokNga, Periudha.DataDokDeri);
            percaktoTamplate();
            gvFleteKontabelKoka.Columns["#"].VisibleIndex = 0;
        }

        protected void gvFleteKontabelKoka_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {

        }
        private void percaktoTamplate()
        {
            var col = gvFleteKontabelKoka.Columns["Kontabilizuar"] as GridViewDataColumn;
            col.DataItemTemplate = new MyCheckTemplate(true, false);
        }



        protected void gvFleteKontabelKoka_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpNoRows"] = gvFleteKontabelKoka.VisibleRowCount;
            e.Properties["cpNoPage"] = gvFleteKontabelKoka.PageIndex;
        }

        protected void gvFleteKontabelKoka_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName != "Kontabilizuar") return;
            (e.Editor as ASPxComboBox).Items.Clear();
            (e.Editor as ASPxComboBox).Items.Add("");
            (e.Editor as ASPxComboBox).Items.Add("Te Kontabilizuar", true);
            (e.Editor as ASPxComboBox).Items.Add("Te Pa Kontabilizuar", false);
        }

        protected void gvFleteKontabelKoka_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if (e.Column.FieldName != "IdGrupKontabilizimi" && e.Column.FieldName != "IdKonfigGjenerues" && e.Column.FieldName != "IdKonfigAmbjente") return;
            if (Converter.ConvertToInt(e.Value) == 0)
                e.Criteria = null;
        }

        protected void gvFleteKontabelKoka_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            var arr = e.Parameters.Split(';');
            var idNdermarrje = (int)hfState["idNdermarrje"];
            var idGjuha = (int)hfState["idGjuha"];
            if (arr.Length == 1) //rasti kur behet callback nga filtri i periudhes duhet te ngelen filtrat ne gride dhe te mos aplikohet filtri default
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "gvFleteKontabelKoka", gvFleteKontabelKoka, cmbKonfigurimi.Text.Split(';')[0], "117", idGjuha, false);
            else
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "gvFleteKontabelKoka", gvFleteKontabelKoka, cmbKonfigurimi.Text.Split(';')[0], "117", idGjuha);
            switch (arr.Length)
            {
                case 2:
                    var idPerdoruesi = (int)hfState["idPerdoruesi"];
                    var periudheDok = clsAlternativaKushti.getAlternativa(Convert.ToInt32(cmbKonfigurimi.Value), "SHDPER");
                    string datanga, dataderi;
                    clsFunksione.ruajPeriudhatNeHiddenField(IsPostBack, hfState, periudheDok, null, out datanga, out dataderi);
                    MbushGridFleteKontabelNgaSession(clsFunksione.GetKomponente(Page.Request), idPerdoruesi, Periudha.PeriudhaDok, Periudha.DataDokNga, Periudha.DataDokDeri);
                    break;
                case 3:
                    if (arr[2] == "")
                        gvFleteKontabelKoka.FilterExpression = "[IdStatusDokumenti]=1";
                    else
                    {
                        var filtra = new clsFiltraGrida();
                        var koka = new clsGridaKoka(mySessionObjects.ktheGjuhe(Session), "gvFleteKontabelKoka", Komponente, mySessionObjects.merrIdNdermarrjeSesioni(Session), int.Parse(cmbKonfigurimi.Value.ToString()));
                        filtra.mbushFilterPerGrideSipasKodit(arr[2], mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
                        if (filtra.FiltraKodi != null)
                        {
                            gvFleteKontabelKoka.FilterExpression = filtra.FiltraVlera;
                            GridUtil.renditGriden(filtra.KoloneRenditje, gvFleteKontabelKoka);
                        }
                    }
                    break;
            }
            gvFleteKontabelKoka.Selection.UnselectAll();
        }

        protected void btnXlsxExport_Click(object sender, EventArgs e)
        {
            try
            {
                gridExport.WriteXlsxToResponse(MessagesResource.Messages["exportFleteKontabel"], true);
            }
            catch (Exception err)
            {
                LogManager.GetCurrentClassLogger().Error(err.Message);
            }
        }

        protected void btnPdfExport_Click(object sender, EventArgs e)
        {
            try
            {
                gridExport.WritePdfToResponse(MessagesResource.Messages["exportFleteKontabel"], true);
            }
            catch (Exception err)
            {
                LogManager.GetCurrentClassLogger().Error(err.Message);
            }
        }

        protected void radDtDok_PreRender(object sender, EventArgs e)
        {
            var radDtDok = sender as ASPxRadioButtonList;
            radDtDok.SelectedItem = radDtDok.Items.FindByValue(hfState.Get("Periudha").ToString());
        }
    }
}
