using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Data;
using DbCore;
using System.Globalization;
using System.Resources;
using DbCore.DbAdmin;
using DbCore.DbShare;
using DbCore.DbKontabiliteti;
using DbCore.DbRegjistrim;
using DbCore.DbArkaBanka;
using DbCore.IMBUtils.Extensions;
using DbCore.IMBUtils.Types;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using PlatinumWeb.ApplicationUtils.Filters;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Messages;
using Newtonsoft.Json;

namespace PlatinumWeb
{
    public partial class VeprimeBanka : MyPageBase
    {
        public const string llojiDerdhje = "derdhje";
        public const string llojiArketim = "arketim";
        public const string llojiTerheqje = "terheqje";
        public const string llojiPagese = "pagese";
        private string komponente => "VeprimeBanka.aspx";
        ResourceManager rm => new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
        CultureInfo ci;
        private int idKategoria => (Request.QueryString["lloji"] == "derdhje" || Request.QueryString["lloji"] == "terheqje") ? 4 : 3;

        string lloji;
        private string guidString;
        string datanga, dataderi;
        private TitlePeriudha _periudha;
        public TitlePeriudha Periudha => _periudha ?? (_periudha = this.MerrPeriudhe(hfState));

        protected void Page_Load(object sender, EventArgs e)
        {
            bool eshteMeme;

            if (!IsPostBack)
            {
                if (!mySessionObjects.isLogedIn(Session)) clsFunksione.logout(Session, true, "FaqePaautorizuar");
                if (mySessionObjects.ktheKodNdermarrje(Session) == null) Response.Redirect("Login_Ndermarrje.aspx?id=" + IdPerdoruesi);

                eshteMeme = mySessionObjects.merrEshteMemeSesioni(Session);
                ci = DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo(IdGjuha);

                hfState.Set("idPerdoruesi", IdPerdoruesi);
                hfState.Set("idGjuha", IdGjuha);
                hfState.Set("idNdermarrje", IdNdermarrja);
                hfState.Set("idViti", IdViti);
                hfState.Set("eshteMeme", eshteMeme);
                hfState.Set("idNdermarrjeVit", IdNdermarrjeVit);
                hfState.Set("guidString", Convert.ToBase64String(Guid.NewGuid().ToByteArray()));
                lloji = Request.QueryString["lloji"];
                hfState.Set("lloji", lloji);
                hfState.Set("idKategori", idKategoria);

                mbushHiddenFieldMePerkthime(ci, rm);
                switch (lloji)
                {
                    case llojiDerdhje:
                        ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(IdPerdoruesi, IdNdermarrja, cmbKonfigurimi, 53, "LDVBD", rm, ci, IdGjuha);
                        break;
                    case llojiArketim:
                        ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(IdPerdoruesi, IdNdermarrja, cmbKonfigurimi, 53, "LDVBA", rm, ci, IdGjuha);
                        break;
                    case llojiTerheqje:
                        ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(IdPerdoruesi, IdNdermarrja, cmbKonfigurimi, 53, "LDVBT", rm, ci, IdGjuha);
                        break;
                    case llojiPagese:
                        ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(IdPerdoruesi, IdNdermarrja, cmbKonfigurimi, 53, "LDVBP", rm, ci, IdGjuha);
                        break;
                    default:
                        break;
                }
                cmbKonfigurimi.SelectedIndex = 0;
                clsKonfigurimAmbjenti konf = new clsKonfigurimAmbjenti();
                konf.mbushKonfigAmbjSipasId(Convert.ToInt32(cmbKonfigurimi.SelectedItem.Value), IdGjuha);
                var periudheDok = clsAlternativaKushti.getAlternativa(konf.IdKonfigAmbjente, "SHDPER");
                var oPeriudha = mySessionObjects.merrPeriudheKontabel(Session);
                clsFunksione.ruajPeriudhatNeHiddenField(IsPostBack, hfState, periudheDok, oPeriudha, out datanga, out dataderi);
                hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;
                grid_veprimeBankaKoka.PercaktoTitlePanelMePeriudhe(this, MenuInfo, pnlMesazhi, hfState, IdPerdoruesi, IdNdermarrja, IdViti, IdGjuha, konf.IdKonfigAmbjente, komponente, 302, "IdKoka", rm, ci, false);
                mbushGridDokumentiNgaDB();
                AplikoFilterExpression();
                konfiguroGride();
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(IdNdermarrja, "grid_veprimeBankaKoka", grid_veprimeBankaKoka, cmbKonfigurimi.Text.Split(';')[0], "302", IdGjuha);

                if (Request.QueryString["fshi"] == "jo")
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("vepBankaMsgDokEshteILidhurNukFshihet", ci), pnlMesazhi);
                
                else if (Request.QueryString["fshi"] == "po")
                { hfObjektRuajtur.Value = Request.QueryString["hfObjektRuajtur"];
                    fshi.Value = "Po";
                    vjennga.Value = "True";
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("regjMagFshirjaPerfundoiMeSukses", ci), pnlMesazhi);
                }
                    
                else if (Request.QueryString["ruaj"] == "po")
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("regjMagModifikimiPerfundoiMeSukses", ci), pnlMesazhi);

                clsTeDrejtaRoli tedrejtaInfo = new clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
                GridUtil.konfigGrideListeEMadhePaTheme(grid_veprimeBankaKoka, "IdKoka");
                AspxWebControlUtils.perkthePopUp(popFshi, rm.GetString("labelKujdes", ci), lblMsgbox, rm.GetString("labelAdministrimiMsgJeniSigurt", ci), ButtonCancel, rm.GetString("labelAnullo", ci));

                clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, IdNdermarrja, "grid_veprimeBankaKoka", int.Parse(cmbKonfigurimi.Value.ToString()), "VeprimeBanka.aspx");
            }
            else
            {
                eshteMeme = (bool)hfState["eshteMeme"];
                guidString = (string)hfState["guidString"];
                lloji = (string)hfState["lloji"];
                ci = DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo(IdGjuha);
                grid_veprimeBankaKoka.PercaktoTitlePanelMePeriudhe(this, MenuInfo, pnlMesazhi, hfState, IdPerdoruesi, IdNdermarrja, IdViti, IdGjuha, int.Parse(cmbKonfigurimi.Value.ToString()), komponente, 302, "IdKoka", rm, ci, false);
                mbushGridDokumentiNgaSession();
                konfiguroGride();
                Container.Attributes["src"] = "";
            }

            if (Request.QueryString["indexrow"] != null)
                grid_veprimeBankaKoka.FocusedRowIndex = int.Parse(Request.QueryString["indexrow"]);

            percaktoTemplateMenu(IdGjuha, ASPxMenu1, IdViti, IdPerdoruesi, IdNdermarrja, eshteMeme);

        }

        private void AplikoFilterExpression()
        {
            switch (lloji)
            {
                case llojiDerdhje:
                    grid_veprimeBankaKoka.FilterExpression = "[LlojiVeprimit] = 'DERDHJE' and [IdStatusDokumenti]=1 ";
                    break;
                case llojiArketim:
                    grid_veprimeBankaKoka.FilterExpression = "[LlojiVeprimit] = 'ARKETIM' and [IdStatusDokumenti]=1 ";
                    break;
                case llojiTerheqje:
                    grid_veprimeBankaKoka.FilterExpression = "[LlojiVeprimit] = 'TERHEQJE' and [IdStatusDokumenti]=1 ";
                    break;
                case llojiPagese:
                    grid_veprimeBankaKoka.FilterExpression = "[LlojiVeprimit] = 'PAGESE' and [IdStatusDokumenti]=1 ";
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Metode qe sherben per te kaluar perkthime te js
        /// </summary>
        /// <param name="cultinf">Merr culture info</param>
        /// <param name="rm">Merr Resource Manager</param>
        private void mbushHiddenFieldMePerkthime(CultureInfo cultinf, ResourceManager rm)
        {
            hfState.Set("regjisDokZgjidhDokPerTeBashkengjitur", rm.GetString("regjisDokZgjidhDokPerTeBashkengjitur", cultinf));
            hfState.Set("msgZgjdhniNjeNgaElementetEListes", rm.GetString("msgZgjdhniNjeNgaElementetEListes", cultinf));
            hfState.Set("regjisDokNukKeniAsnjeDokTeZgjedhur", rm.GetString("regjisDokNukKeniAsnjeDokTeZgjedhur", cultinf));
            hfState.Set("regjisDokMsgFaturaEshtePrintNeKase", rm.GetString("regjisDokMsgFaturaEshtePrintNeKase", cultinf));
            hfState.Set("msgJuKeniZgjedhur", rm.GetString("msgJuKeniZgjedhur", cultinf));
            hfState.Set("msgRreshta", rm.GetString("msgRreshta", cultinf));
            hfState.Set("labelAdministrimiMsgJeniSigurt", rm.GetString("labelAdministrimiMsgJeniSigurt", cultinf));
            konfigurimi_Label.Text = rm.GetString("lblModeli", cultinf);
        }

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        /// <param name="idViti"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNermarrje"></param>
        /// <param name="eshteMeme"></param>
        private void percaktoTemplateMenu(int idGjuha, ASPxMenu aSPxMenu1, int idViti, int idPerdorues, int idNdermarrje, bool eshteMeme)
        {
            clsToolbarConfig.percaktoTemplateMenu(IdGjuha, IdViti, idPerdorues, IdNdermarrja, aSPxMenu1, clsFunksione.GetKomponente(Page.Request), this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, true, false, false, eshteMeme);
        }

        /// <summary>
        /// ruan konfigurimin e grides dhe filtrin e zgjedhur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RuajKolona_Click(object sender, EventArgs e)
        {
            int idfiltri = 0;
            CultureInfo ci = DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo(IdGjuha);
            int idkonf = clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(cmbKonfigurimi.Text.Split(';')[0], IdNdermarrja);
            clsMesazh mesazh = GridUtil.ruajFiltra(IdNdermarrja, IdPerdoruesi, IdGjuha, grid_veprimeBankaKoka.ID, "VeprimeBanka.aspx", "FilterDefault", grid_veprimeBankaKoka.FilterExpression, grid_veprimeBankaKoka, "IdNivel", idkonf, out idfiltri);//ruan filtrin e zgjedhur tek filtrat
            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                return;
            }
            mesazh = GridUtil.ruajkonfigurimgride(grid_veprimeBankaKoka, cmbKonfigurimi.Text, IdNdermarrja, IdPerdoruesi, 302, idfiltri, IdViti, ci, IdGjuha);/// ruan konfigurimin e grides dhe filtrin

            clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, IdNdermarrja, "grid_veprimeBankaKoka", int.Parse(cmbKonfigurimi.Value.ToString()), "VeprimeBanka.aspx");

            percaktoTemplateMenu(IdGjuha, ASPxMenu1, IdViti, IdPerdoruesi, IdNdermarrja, (bool)hfState["eshteMeme"]);

            if (mesazh.Status) clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
        }

        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(IdGjuha, ASPxMenu1, IdViti, IdPerdoruesi, IdNdermarrja, (bool)hfState["eshteMeme"]);
        }

        private void mbushGridDokumentiNgaSession()
        {
            var tmpObject = grid_veprimeBankaKoka.MerrDataSourceMePeriduheNeSession<DataTable>(Session, komponente, Periudha, guidString);
            if (tmpObject == null)
                mbushGridDokumentiNgaDB();
            else
            {
                grid_veprimeBankaKoka.DataSource = tmpObject;
                grid_veprimeBankaKoka.DataBind();
                tmpObject.Dispose();
            }
        }

        private void mbushGridDokumentiNgaDB()
        {
            DataTable dt;
            dt = colVeprimBankaKoka.merrVeprimBankaDT(IdNdermarrjeVit, idKategoria, IdPerdoruesi, Periudha.DataDokNga, Periudha.DataDokDeri, lloji);
            grid_veprimeBankaKoka.RuajDataSourceMePeriduheNeSession(Session, komponente, Periudha, dt, guidString);
            grid_veprimeBankaKoka.DataSource = dt;
            grid_veprimeBankaKoka.DataBind();
            dt.Dispose();
        }

        private void konfiguroVleraFillestare()
        {
            colVeprimBankaKoka colKoka = new colVeprimBankaKoka(IdNdermarrjeVit, idKategoria);
            grid_veprimeBankaKoka.DataSource = colKoka;
            grid_veprimeBankaKoka.DataBind();
        }

        private void konfiguroGride()
        {
            KonfigurimComboGride.ShtoNivel(grid_veprimeBankaKoka, idKategoria, IdNdermarrja, IdPerdoruesi, IdGjuha, Session, komponente, guidString);
            KonfigurimComboGride.ShtoBanke(grid_veprimeBankaKoka, IdNdermarrja, idKategoria, Session, komponente, guidString);
            KonfigurimComboGride.ShtoLlojVeprimiArketime(grid_veprimeBankaKoka, Session, komponente, guidString);
            KonfigurimComboGride.ShtoStatus(grid_veprimeBankaKoka, rm, ci, "IdStatusDokumenti");
            grid_veprimeBankaKoka.ShtoPerdorues(IdPerdoruesi, Session, komponente, guidString);
            KonfigurimComboGride.ShtoModel(grid_veprimeBankaKoka, idKategoria, IdNdermarrja, IdPerdoruesi, IdGjuha, Session, komponente, guidString);
            KonfigurimComboGride.shto_DegeAdministrative(grid_veprimeBankaKoka, IdNdermarrja, Session, komponente, guidString);
            KonfigurimComboGride.ShtoStatusAprovimi(grid_veprimeBankaKoka, rm, ci, "StatusAprovimi");

            GridViewDataTextColumn col3 = grid_veprimeBankaKoka.Columns["Vlera"] as GridViewDataTextColumn;
            col3.PropertiesEdit.DisplayFormatString = "{0:n}";
            GridViewDataTextColumn col4 = grid_veprimeBankaKoka.Columns["Kursi"] as GridViewDataTextColumn;
            col4.PropertiesEdit.DisplayFormatString = "0.00####";
            GridViewDataTextColumn col5 = grid_veprimeBankaKoka.Columns["VleraMonedhaBaze"] as GridViewDataTextColumn;
            col5.PropertiesEdit.DisplayFormatString = "{0:n}";
            grid_veprimeBankaKoka.Columns["#"].VisibleIndex = 0;
        }


        protected void grid_veprimeBankaKoka_DataBound(object sender, EventArgs e)
        {
            if (grid_veprimeBankaKoka.Columns["#"] == null)
            {
                GridViewCommandColumn check = new GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true; check.Width = Unit.Percentage(2);
                grid_veprimeBankaKoka.Settings.ShowFilterRow = true;
                grid_veprimeBankaKoka.Settings.ShowHeaderFilterButton = true;
                grid_veprimeBankaKoka.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                grid_veprimeBankaKoka.Settings.ShowFilterRowMenu = true;
                grid_veprimeBankaKoka.Columns.Add(check);
                grid_veprimeBankaKoka.Settings.ShowGroupPanel = true;
                grid_veprimeBankaKoka.KeyFieldName = "IdKoka";
                grid_veprimeBankaKoka.SettingsBehavior.AllowSelectByRowClick = true;
                grid_veprimeBankaKoka.SettingsBehavior.AllowFocusedRow = true;
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
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            clsFiltraGrida filtra = new clsFiltraGrida();
            clsGridaKoka koka = new clsGridaKoka(IdGjuha, "grid_veprimeBankaKoka", "VeprimeBanka.aspx", IdNdermarrja, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, IdNdermarrja, koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = IdPerdoruesi;
                clsMesazh mesazh = new clsMesazh();
                mesazh = filtra.fshi();
                clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, IdNdermarrja, "grid_veprimeBankaKoka", int.Parse(cmbKonfigurimi.Value.ToString()), "VeprimeBanka.aspx");
                percaktoTemplateMenu(IdGjuha, ASPxMenu1, IdViti, IdPerdoruesi, IdNdermarrja, (bool)hfState["eshteMeme"]);

                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                cmbFiltra.Text = "";
                AplikoFilterExpression();
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
            ASPxComboBox cmbFiltra = ((MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            clsFiltraGrida filtri = new clsFiltraGrida();
            filtri.FiltraKodi = cmbFiltra.Text;
            filtri.FiltraShenime = cmbFiltra.Text;
            filtri.FiltraUniversal = false;
            clsGridaKoka koka = new clsGridaKoka(IdGjuha, "grid_veprimeBankaKoka", "VeprimeBanka.aspx", IdNdermarrja, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = grid_veprimeBankaKoka.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("LlojiVeprimit", grid_veprimeBankaKoka);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = grid_veprimeBankaKoka.GetSortedColumns();
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
            //    filtri.KoloneRenditje = "LlojiVeprimit";
            //    filtri.DrejtimRenditje = true;
            //}
            int idPerdoruesi = (int)hfState["idPerdoruesi"];
            filtri.IdPerdoruesi = IdPerdoruesi;
            filtri.IdNdermarje = IdNdermarrja;
            clsMesazh mesazh = new clsMesazh();
            filtri.IdStatusDok = 1;

            mesazh = filtri.ruaj();

            clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, IdNdermarrja, "grid_veprimeBankaKoka", int.Parse(cmbKonfigurimi.Value.ToString()), "VeprimeBanka.aspx");
            percaktoTemplateMenu(IdGjuha, ASPxMenu1, (int)hfState["idViti"], IdPerdoruesi, IdNdermarrja, (bool)hfState["eshteMeme"]);

            if (mesazh.Status == true)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

            cmbFiltra.Text = "";
        }

        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {
            int idGjuha = (int)hfState["idGjuha"];
            CultureInfo ci = DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo((int)hfState["idGjuha"]);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            switch (e.Item.Name)
            {
                case "PrintPreview":
                    string id = string.Empty, nrDok = string.Empty, idDesign = string.Empty;
                    if (grid_veprimeBankaKoka.FocusedRowIndex > -1)
                    {
                        id = grid_veprimeBankaKoka.GetRowValues(grid_veprimeBankaKoka.FocusedRowIndex, "IdKoka").ToString();
                        nrDok = grid_veprimeBankaKoka.GetRowValues(grid_veprimeBankaKoka.FocusedRowIndex, "NrDokumenti").ToString();
                        idDesign = grid_veprimeBankaKoka.GetRowValues(grid_veprimeBankaKoka.FocusedRowIndex, "IdRaportDesign").ToString();
                    }
                    string rapEmriReal = "mandatArketimPagese";
                    int idKoka = 0, idRapDesign = 0;
                    int.TryParse(id, out idKoka);
                    int.TryParse(idDesign, out idRapDesign);

                    List<object> rreshtatKoka = grid_veprimeBankaKoka.GetSelectedFieldValues("IdKoka", "NrDokumenti", "IdRaportDesign");
                    if (rreshtatKoka.Count < 1 && idKoka > 0)
                        rreshtatKoka.Add(new object[3] { idKoka, nrDok, idRapDesign });

                    if (rreshtatKoka.Count < 1)
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("regjMagMesazhZgjidhniFaturePerPrintim", ci), pnlMesazhi);
                        Container.Attributes["src"] = "";
                        return;
                    }

                    int nrRreshtaOk = 0;
                    clsMesazh sms = clsFunksione.ruajTeDhenaRaportiPerHapjeRaportiTeShpejte(rreshtatKoka, rapEmriReal, out nrRreshtaOk, Session);
                    if (!sms)
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, string.Format(rm.GetString("regjDokumentaMesazhSkaFormatPerPrintim", ci), sms.PershkrimMesazhi), pnlMesazhi);

                    grid_veprimeBankaKoka.JSProperties["cpHapFaqe"] = $"RaportiShpejte.aspx?Sesioni=false&emriReal={rapEmriReal}&idDokumenti={idKoka}&printo=0&iddesign={idRapDesign}";
                    break;

                case "Riruaj":
                    Riruaj((string)hfState["guidString"], clsFunksione.GetKomponente(Page.Request), ci, rm, IdGjuha);
                    break;
            }
        }


        protected void Riruaj(string guidString, string komponente, CultureInfo ci, ResourceManager rm, int idGjuha)
        {
            clsMesazh mesazh = new clsMesazh();
            List<string> TeRuajtur = new List<string>(), Teparuajtur = new List<string>();
            pergjigja.Text = "";

            List<object> rreshtat = grid_veprimeBankaKoka.GetSelectedFieldValues("IdKoka");
            pergjigja.Text = "";
            string serverUrl = clsFunksione.ktheServerUrl(Request);
            clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin();
            int idKategori = (int)hfState["idKategori"];

            DataTable err = new DataTable();
            err.Columns.Add("Kodi");
            err.Columns.Add("Gabimi");
            err.Columns.Add("Rreshti");
            int nrreshta = 0;
            foreach (object id in rreshtat)
            {
                nrreshta++;
                clsVeprimBankaKoka clsKoka = new clsVeprimBankaKoka(Convert.ToInt32(id));
                clsKonfigurimAmbjenti konfig = new clsKonfigurimAmbjenti(clsKoka.IdKonfigAmbjente);
                if (clsKoka.IdStatusDokumenti == 2)
                    continue;

                if (DbCore.MbylljePeriudhe.PeriodClosing.IsPeriodClosed(clsKoka.DateDokumenti, MyConnectionsManager.GetSelectedConNameServer(), IdNdermarrja, (lloji == "derdhje" || lloji == "terheqje") ? KategoriDokumenti.VeprimeBanke : KategoriDokumenti.VeprimeArke, clsKoka.IdKonfigAmbjente))
                {
                    object[] arr = { konfig.KodKonfigAmbjente + " " + clsKoka.NrDokumenti + " " + clsKoka.DateDokumenti.ToShortDateString(), MessagesResource.Messages["msgPeriodIsClosed"], nrreshta };
                    err.Rows.Add(arr);
                    continue;
                }

                clsKusht kushtskema = new clsKusht(clsKoka.IdKonfigAmbjente, "ZSP");
                if ((clsKoka.StatusAprovimi == StatusAprovimi.Undefined && kushtskema.Vlera != 0) || (clsKoka.StatusAprovimi != StatusAprovimi.Undefined && clsKoka.StatusAprovimi != StatusAprovimi.Aprovuar))
                {
                    object[] arr = { konfig.KodKonfigAmbjente + " " + clsKoka.NrDokumenti + " " + clsKoka.DateDokumenti.ToShortDateString(), "Nuk u riruajt pasi eshte ne proces aprovimi!", nrreshta };
                    err.Rows.Add(arr);
                    continue;
                }
                colVeprimBankaTrupi col = new colVeprimBankaTrupi(clsKoka.IdKoka);
                int idPeriudhaKontabel = clsPeriudhaKontabel.ktheIdPeriudheSipasDatesDheNdermarrjes(clsKoka.DateDokumenti, IdNdermarrja);
                bool lidhur = dbAdmin.eshteDokumentiILidhur(clsKoka.IdKoka, clsKoka.IdNivel, "T_VEPRIMBANKAKOKA", "IDKOKA");
                clsKokaFleteKontabel kokfk = new clsKokaFleteKontabel(clsKoka.IdKoka, idKategori);
                DbCore.DbQendraKosto.clsKokaQendraKosto qend = new DbCore.DbQendraKosto.clsKokaQendraKosto();
                qend.KtheKokaQKSipasIDGjeneruesDheKonfigMeTrup(kokfk.IdKokaFleteKontabel, kokfk.IdKonfigAmbjente);

                #region validime
                if (clsKoka.IdBanka == 0)
                {
                    object[] arr = { konfig.KodKonfigAmbjente + " " + clsKoka.NrDokumenti + " " + clsKoka.DateDokumenti.ToShortDateString(), "Zgjidhni nje banke", nrreshta };
                    err.Rows.Add(arr);
                    continue;
                }
                clsBanka bank = new clsBanka();
                clsMesazh mesazhArkBank = bank.mbushBanke(clsKoka.IdBanka);
                if (!mesazhArkBank.Status)
                {
                    object[] arr = { konfig.KodKonfigAmbjente + " " + clsKoka.NrDokumenti + " " + clsKoka.DateDokumenti.ToShortDateString(), mesazhArkBank.PershkrimMesazhi, nrreshta };
                    err.Rows.Add(arr);
                    continue;
                }
                if (bank.AktivBanka == false)
                {
                    object[] arr = { konfig.KodKonfigAmbjente + " " + clsKoka.NrDokumenti + " " + clsKoka.DateDokumenti.ToShortDateString(), "Kjo banke nuk eshte aktive", nrreshta };
                    err.Rows.Add(arr);
                    continue;
                }
                if (bank.LlojArkaBanka == false && (lloji == "derdhje" || lloji == "terheqje"))
                {
                    object[] arr = { konfig.KodKonfigAmbjente + " " + clsKoka.NrDokumenti + " " + clsKoka.DateDokumenti.ToShortDateString(), "Nuk mund te zgjidhni arke per veprimet me banken!", nrreshta };
                    err.Rows.Add(arr);
                    continue;
                }
                if (bank.LlojArkaBanka == true && (lloji == "arketim" || lloji == "pagese"))
                {
                    object[] arr = { konfig.KodKonfigAmbjente + " " + clsKoka.NrDokumenti + " " + clsKoka.DateDokumenti.ToShortDateString(), "Nuk mund te zgjidhni banke per veprimet me arken!", nrreshta };
                    err.Rows.Add(arr);
                    continue;
                }
                if (clsKoka.Kursi == 0)
                {
                    object[] arr = { konfig.KodKonfigAmbjente + " " + clsKoka.NrDokumenti + " " + clsKoka.DateDokumenti.ToShortDateString(), rm.GetString("msgShenoniKursin", ci), nrreshta };
                    err.Rows.Add(arr);
                    continue;
                }
                if (clsKoka.IdGrup1 != 0)
                {
                    clsGrupimDokumentiKoka grup = new clsGrupimDokumentiKoka(clsKoka.IdGrup1);
                    if (grup.IdGrupimKoka < 1)
                    {
                        object[] arr = { konfig.KodKonfigAmbjente + " " + clsKoka.NrDokumenti + " " + clsKoka.DateDokumenti.ToShortDateString(), rm.GetString("msgGrupimiIPareNukEkziston", ci), nrreshta };
                        err.Rows.Add(arr);
                        continue;
                    }
                }
                if (clsKoka.IdGrup2 != 0)
                {
                    clsGrupimDokumentiKoka grup = new clsGrupimDokumentiKoka(clsKoka.IdGrup2);
                    if (grup.IdGrupimKoka < 1)
                    {
                        object[] arr = { konfig.KodKonfigAmbjente + " " + clsKoka.NrDokumenti + " " + clsKoka.DateDokumenti.ToShortDateString(), rm.GetString("msgGrupimiDyteNukEkziston", ci), nrreshta };
                        err.Rows.Add(arr);
                        continue;
                    }
                }
                if (clsKoka.IdGrup3 != 0)
                {
                    clsGrupimDokumentiKoka grup = new clsGrupimDokumentiKoka(clsKoka.IdGrup3);
                    if (grup.IdGrupimKoka < 1)
                    {
                        object[] arr = { konfig.KodKonfigAmbjente + " " + clsKoka.NrDokumenti + " " + clsKoka.DateDokumenti.ToShortDateString(), rm.GetString("msgGrupimiITreteNukEkziston", ci), nrreshta };
                        err.Rows.Add(arr);
                        continue;
                    }

                }
                #endregion

                #region krijimi i kokes se re
                clsVeprimBankaKoka kokare = new clsVeprimBankaKoka();
                bool meKontabilizim = clsAlternativaKushti.getAlternativa(clsKoka.IdKonfigAmbjente, "GJK") != "Jo";
                string koddege = "";
                string menyrepag = "";
                menyrepag = clsFunksione.ktheMenyrePageseSipasID(clsKoka.IdMenyrePagese);

                if (clsKoka.IdDegeAdministrative != 0)
                {
                    clsDegeAdministrative dege = new clsDegeAdministrative(clsKoka.IdDegeAdministrative);
                    koddege = dege.Kodi;
                }

                string shfaqmesazhapolupe = "Jo", shfaqmesazhapolupeVDK = "Jo";
                object[] nivele = new object[col.Count];
                int i = 0;
                foreach (clsVeprimBankaTrupi trupMag in col)
                {
                    nivele[i] = trupMag.IdNivel;
                    i++;
                }
                clsKonfigurimAmbjenti konflidhes = new clsKonfigurimAmbjenti();
                konflidhes.mbushKonfigAmbjSipasId(konfig.IdKonfigurimi, IdGjuha);
                mesazh = kokare.krijoVeprimeBanke(clsKoka.IdBanka, bank.KodiBanka, clsKoka.Kursi, clsKoka.DateDokumenti, clsKoka.DateRegjistrimi, clsKoka.NrDokumenti, clsKoka.NrReference, clsKoka.NrSerial, clsKoka.PershkrimiKoka, clsKoka.IdMenyrePagese, menyrepag, clsKoka.Vlera, clsKoka.VleraMonedhaBaze, clsKoka.KomisioniBankar, clsKoka.KomisioniMonedhaBaze, clsKoka.LlojiVeprimit, IdPerdoruesi, clsKoka.IdLlojDokumenti, 1, clsKoka.IdNderViti, clsKoka.IdKonfigAmbjente, clsKoka.IdNivelGjenerues, clsKoka.IdKonfigGjenerues, clsKoka.IdGjenerues, clsKoka.IdNivel, clsKoka.IdDokNga, clsKoka.IdDegeAdministrative, koddege, clsKoka.IdNdermarje, clsKoka.IdLlogKredite, col, meKontabilizim, kokfk.IdPeriudha > 0 ? kokfk.IdPeriudha : idPeriudhaKontabel, bank.IdMonedhaBanka, clsKoka.NrKredite, clsKoka.IdGrup1, clsKoka.IdGrup2, clsKoka.IdGrup3, konflidhes, nivele, new clsDatabaseArkaBanka(), null, out shfaqmesazhapolupe, out shfaqmesazhapolupeVDK, qend.ColTrupi, clsKoka.IdKoka, clsKoka.Shoqeria, clsKoka.CustomerNumber, clsKoka.IdAutomjet, clsKoka.Targa, clsKoka.IdRaportDesing, clsKoka.Financieri, clsKoka.DhenesiMarresi, clsKoka.Arketari, clsKoka.Kase, clsKoka.StatusAprovimi, clsKoka.ArsyeAnullimi, clsKoka.IdDokAnullimi, clsKoka.NrLlogari, hfArkiva, idKategori, IdPerdoruesi, clsKoka.Printo, clsKoka.IdKrijuesi);
                if (!mesazh.Status)
                {
                    object[] arr = { konfig.KodKonfigAmbjente + " " + clsKoka.NrDokumenti + " " + clsKoka.DateDokumenti.ToShortDateString(), mesazh.PershkrimMesazhi, nrreshta };
                    err.Rows.Add(arr);
                    continue;
                }
                kokare.IdKoka = clsKoka.IdKoka;
                #endregion
                int idskema = kushtskema.Vlera;
                if (clsKoka.StatusAprovimi == StatusAprovimi.Undefined)
                    idskema = 0;
                int idetapa = 0;
                //etapa e fundit kur hapet nga shitja
                clsEtapeAprovimi etapafund = new clsEtapeAprovimi();
                etapafund.ktheEtapeFunditSipasKokaVeprimeBankaDhePerdorues(clsKoka.IdKoka, IdPerdoruesi);
                idetapa = etapafund.IdEtapa;
                mesazh = kokare.modifiko(lidhur, rm, ci, idskema, kokare.StatusAprovimi, idetapa, serverUrl);

                if (!mesazh.Status)
                {
                    object[] arr = { konfig.KodKonfigAmbjente + " " + clsKoka.NrDokumenti + " " + clsKoka.DateDokumenti.ToShortDateString(), mesazh.PershkrimMesazhi, nrreshta };
                    err.Rows.Add(arr);
                    continue;
                }
            }
            mySessionObjects.ruajTabeleGabimeshImporti(Session, err);
            if (err.Rows.Count > 0)
            {
                clsKokaErrorImporti koka;
                koka = new clsKokaErrorImporti(0, "Nga riruatja e veprime arka banka ", idKategori, IdNdermarrja, IdPerdoruesi);
                koka.ColTrupi.mbushErrorImportiNgaProgrami(err);
                mesazh = koka.ruajErrorImporti();
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "U riruajten " + (rreshtat.Count - err.Rows.Count) + " rreshta dhe deshtuan " + err.Rows.Count + " rreshta! ", pnlMesazhi);
                Container.Attributes["src"] = "RaportiShpejte.aspx?Sesioni=false&emriReal=gabimeImporti&printo=false&db=jo";
            }
            else
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "U riruajten te gjitha rreshtat!", pnlMesazhi);

            grid_veprimeBankaKoka.Selection.UnselectAll();
            mbushGridDokumentiNgaDB();
            konfiguroGride();
            dbAdmin.Dispose();

            if (rreshtat.Count == 0)
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("regjMagMesazhZgjidhniNje", ci), pnlMesazhi);

        }


        //fshin rreshtat e selektuar
        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            CultureInfo ci = DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo((int)hfState["idGjuha"]);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            List<object> rreshtat = grid_veprimeBankaKoka.GetSelectedFieldValues("IdKoka");
            if (rreshtat.Count == 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("regjMagMesazhZgjidhniNje", ci), pnlMesazhi);
                return;
            }
            List<string> TeFshire = new List<string>(), TeLidhur = new List<string>(), PeriudheKycur = new List<string>(), procesaprovimi = new List<string>(), closedPeriod = new List<string>();
            clsMesazh mesazh = new clsMesazh();
            pergjigja.Text = "";
            string lloji = (string)hfState["lloji"];
            string komponente = clsFunksione.GetKomponente(Request);
            clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin();
            foreach (object id in rreshtat)
            {
                clsVeprimBankaKoka koka = new clsVeprimBankaKoka(Convert.ToInt32(id));
                bool lidhur = dbAdmin.eshteDokumentiILidhur(koka.IdKoka, koka.IdNivel, "T_VEPRIMBANKAKOKA", "IDKOKA");
                bool autorizimet = clsVeprimBankaKoka.kaAutorizime(koka.IdKoka, IdPerdoruesi);
                if (!autorizimet)
                    lidhur = true;
                if (lidhur)
                {
                    TeLidhur.Add(koka.NrDokumenti);
                    continue;
                }
                if (koka.StatusAprovimi != ((StatusAprovimi.Undefined)) && koka.StatusAprovimi != ((StatusAprovimi.Aprovuar)))
                {
                    if (koka.StatusAprovimi == (StatusAprovimi.Refuzuar))
                    {
                        DbCore.DbShare.clsKusht kusht = new DbCore.DbShare.clsKusht(koka.IdKonfigAmbjente, "ZSP");

                        DbCore.DbAdmin.clsTrupiSkemaWorkFlow trup = new DbCore.DbAdmin.clsTrupiSkemaWorkFlow();
                        trup.merrTrupSipasKokesDhePerdoruesit(kusht.Vlera, DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
                        if (trup.Niveli != 1)
                        {
                            procesaprovimi.Add(koka.NrDokumenti);
                            continue;
                        }
                    }
                    else
                    {
                        procesaprovimi.Add(koka.NrDokumenti);
                        continue;
                    }
                }
                if (koka.IdStatusDokumenti == 2)
                    continue;
                if (clsPeriudhaKontabel.eshteKycurPeriudheSipasDateDheNdermarrjes(koka.DateDokumenti, IdNdermarrja))
                {
                    PeriudheKycur.Add(koka.NrDokumenti);
                    continue;
                }

                if (DbCore.MbylljePeriudhe.PeriodClosing.IsPeriodClosed(koka.DateDokumenti, MyConnectionsManager.GetSelectedConNameServer(), IdNdermarrja, (lloji == "derdhje" || lloji == "terheqje") ? KategoriDokumenti.VeprimeBanke : KategoriDokumenti.VeprimeArke, koka.IdKonfigAmbjente))
                {
                    closedPeriod.Add(koka.NrDokumenti);
                    continue;
                }

                koka.IdPerdoruesi = IdPerdoruesi;
                mesazh = koka.fshi(rm, ci);
                if (mesazh.Status)
                {
                    hiqDokumentNgaGrida(koka.IdKoka, (int)hfState["idKategori"]);
                    TeFshire.Add(koka.NrDokumenti);
                    clsVeprimBankaKoka kokab = new clsVeprimBankaKoka(koka.IdKoka);
                    colVeprimBankaTrupi trupi = new colVeprimBankaTrupi(koka.IdKoka);
                    hfObjektRuajtur.Value = JsonConvert.SerializeObject(koka) + JsonConvert.SerializeObject(trupi);
                    fshi.Value = "Po";
                }
            }
            dbAdmin.Dispose();
            string mesazhInfoGabimLidhur = "", mesazhInfoGabimPeridheKycur = "", mesazhInfoSukses = "", mesazhClosedPeriod = "";
            if (TeLidhur.Count == 1)
                mesazhInfoGabimLidhur = String.Format("{0}{1}{2}", rm.GetString("regjisDokPrefixMesazhNjejes", ci), String.Join(";", TeLidhur), rm.GetString("regjMagSuffixMesazhNjejesLidhurGabimi", ci));
            else
                if (TeLidhur.Count > 1)
                mesazhInfoGabimLidhur = String.Format("{0}{1}{2}", rm.GetString("regjisDokprefixMesazhShumes", ci), String.Join(";", TeLidhur), rm.GetString("regjMagSuffixMesazhShumesLidhurGabimi", ci));
            if (PeriudheKycur.Count == 1)
                mesazhInfoGabimPeridheKycur = String.Format("{0}{1}{2}", rm.GetString("regjisDokPrefixMesazhNjejes", ci), String.Join(";", PeriudheKycur), rm.GetString("suffixMesazhNjejesPeriudheKycurGabimi", ci));
            else
                if (PeriudheKycur.Count > 1)
                mesazhInfoGabimPeridheKycur = String.Format("{0}{1}{2}", rm.GetString("regjisDokprefixMesazhShumes", ci), String.Join(";", PeriudheKycur), rm.GetString("suffixMesazhShumesPeriudheKycurGabimi", ci));

            if (closedPeriod.Count == 1)
                mesazhClosedPeriod = String.Format("{0}{1}{2}", rm.GetString("regjisDokPrefixMesazhNjejes", ci), String.Join(", ", closedPeriod), rm.GetString("suffixMesazhClosedPeriodNjejes", ci));
            else if (closedPeriod.Count > 1)
                mesazhClosedPeriod = String.Format("{0}{1}{2}", rm.GetString("regjisDokPrefixMesazhNjejes", ci), String.Join(", ", closedPeriod), rm.GetString("suffixMesazhClosedPeriodShumes", ci));

            if (TeFshire.Count == 1)
                mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("regjisDokPrefixMesazhNjejes", ci), String.Join(";", TeFshire), rm.GetString("regjMagSuffixMesazhNjejesSuksesi", ci));
            else
                if (TeFshire.Count > 1)
                mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("regjisDokprefixMesazhShumes", ci), String.Join(";", TeFshire), rm.GetString("regjMagSuffixMesazhShumesSuksesi", ci));

            mesazhInfoGabimLidhur += mesazhInfoGabimPeridheKycur + mesazhClosedPeriod;
            if (mesazhInfoGabimLidhur != "" && mesazhInfoSukses != "")
                mesazhInfoGabimLidhur += rm.GetString("msgDegetAdministrativeLidhesMesazhi", ci) + mesazhInfoSukses;
            if (mesazhInfoGabimLidhur != "")
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhInfoGabimLidhur, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazhInfoSukses, pnlMesazhi);
            //}
        }

        private void hiqDokumentNgaGrida(int idkoka, int idKategori)
        {
            if (grid_veprimeBankaKoka.DataSource != null)
            {
                DataTable dt = (DataTable)grid_veprimeBankaKoka.DataSource;
                DataRow[] drs = dt.Select("IdKoka = " + idkoka);
                if (drs.Length > 1)
                    throw new Exception(rm.GetString("regjisDokNdodhen2DokMeTeNjejtenID", ci));
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                dt.Rows.Remove(dr);
                grid_veprimeBankaKoka.DataSource = dt;
                grid_veprimeBankaKoka.DataBind();
                dt.Dispose();
            }
            else
                mbushGridDokumentiNgaDB();
        }

        protected void grid_veprimeBankaKoka_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
        }

        protected void grid_veprimeBankaKoka_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (grid_veprimeBankaKoka.AplikoFilterDefault(e, Convert.ToInt32(cmbKonfigurimi.Value)))
                return;

            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 1) //rasti kur behet callback nga filtri i periudhes duhet te ngelen filtrat ne gride dhe te mos aplikohet filtri default
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(IdNdermarrja, "grid_veprimeBankaKoka", grid_veprimeBankaKoka, cmbKonfigurimi.Text.Split(';')[0], "302", IdGjuha, false);
            else
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(IdNdermarrja, "grid_veprimeBankaKoka", grid_veprimeBankaKoka, cmbKonfigurimi.Text.Split(';')[0], "302", IdGjuha);
            if (arr.Length == 2)
            {
                string periudheDok = clsAlternativaKushti.getAlternativa(Convert.ToInt32(cmbKonfigurimi.Value), "SHDPER");
                string datanga, dataderi;
                clsFunksione.ruajPeriudhatNeHiddenField(IsPostBack, hfState, periudheDok, null, out datanga, out dataderi);
                mbushGridDokumentiNgaSession();
            }
            if (arr.Length == 3)
            {
                if (arr[2] == "")
                    AplikoFilterExpression();
                else
                {
                    clsFiltraGrida filtra = new clsFiltraGrida();
                    clsGridaKoka koka = new clsGridaKoka(IdGjuha, "grid_veprimeBankaKoka", "VeprimeBanka.aspx", IdNdermarrja, int.Parse(cmbKonfigurimi.Value.ToString()));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], IdNdermarrja, koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        grid_veprimeBankaKoka.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, grid_veprimeBankaKoka);
                    }
                    GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(IdNdermarrja, grid_veprimeBankaKoka.ID, grid_veprimeBankaKoka, cmbKonfigurimi.Text.Split(';')[0], "302", IdGjuha, true);
                }
            }
            grid_veprimeBankaKoka.Selection.UnselectAll();
        }

        protected void grid_veprimeBankaKoka_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = grid_veprimeBankaKoka.PageIndex;
            e.Properties["cpPageRow"] = grid_veprimeBankaKoka.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = grid_veprimeBankaKoka.VisibleRowCount;
        }

        protected void grid_veprimeBankaKoka_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if (e.Column.FieldName.ContainsAnyIgnoreCase("IdBanka", "IdKonfigAmbjente", "StatusAprovimi"))
                if (Converter.ConvertToInt(e.Value) == 0)
                    e.Criteria = null;
        }

        protected void grid_veprimeBankaKoka_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            string TeGjithe = rm.GetString("GridHeaderFilterFillItemTeGjithe", ci);
            string nga = rm.GetString("GridHeaderFilterFillItemNga", ci);
            if (e.Column.FieldName == "NrDokumenti" || e.Column.FieldName == "PershkrimiKoka")
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

        protected void btnXlsxExport_Click(object sender, EventArgs e)
        {
            try
            {
                gridExport.WriteXlsxToResponse("faturat", true);
            }
            catch (Exception err)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
            }
        }

        protected void btnPdfExport_Click(object sender, EventArgs e)
        {
            GridUtil.ExportPdfFitToPage(gridExport, Response, "faturat");
        }

        protected void radDtDok_PreRender(object sender, EventArgs e)
        {
            ASPxRadioButtonList radDtDok = sender as ASPxRadioButtonList;
            radDtDok.SelectedItem = radDtDok.Items.FindByValue(hfState.Get("Periudha").ToString());
        }

        protected void grid_veprimeBankaKoka_CustomColumnSort(object sender, CustomColumnSortEventArgs e)
        {
            if (e.Column.FieldName == ("NrDokumenti"))
            {
                int vl1 = 0;
                int vl2 = 0;

                if (int.TryParse(e.Value1.ToString(), out vl1) && int.TryParse(e.Value2.ToString(), out vl2))
                {
                    if (vl1 > vl2)
                        e.Result = 1;
                    else
                        e.Result = vl1 == vl2 ? 0 : -1;
                    e.Handled = true;
                }
                else
                    e.Handled = false;
            }

        }
    }

}

