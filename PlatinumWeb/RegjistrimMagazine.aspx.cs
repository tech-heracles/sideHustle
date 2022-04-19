using DbCore;
using DbCore.DbInventari;
using DbCore.DbKontabiliteti;
using DbCore.DbRegjistrim;
using DbCore.DbShare;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Resources;
using System.Web.UI;
using System.Web.UI.WebControls;
using DbCore.IMBUtils;
using System.Linq;
using DbCore.DbAdmin;
using DbCore.IMBUtils.Types;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Filters;
using PlatinumWeb.ApplicationUtils.Pages;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using DbCore.IMBUtils.Messages;
using DbCore.IMBUtils.Fiskalizimi.Controls;
using DbCore.IMBUtils.Fiskalizimi.API;
using System.Web.Configuration;

namespace PlatinumWeb
{
    public partial class RegjistrimMagazine : MyPageBase
    {

        private static string pershkrimDaljeFK = "Nga daljet e magazinës";
        private static string pershkrimHyrjeFK = "Nga hyrjet e magazinës";
        private string guidString;

        private DbCore.DbRegjistrim.colTrupiMagazina trupat = new DbCore.DbRegjistrim.colTrupiMagazina();
        private TitlePeriudha _periudha;
        public TitlePeriudha Periudha => _periudha ?? (_periudha = this.MerrPeriudhe(hfState));
        //private int _IdKomponente = Request.QueryString["lloj"] == "hyrje" ? 513 : 519;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!mySessionObjects.isLogedIn(Session))
                clsFunksione.logout(Session, true, "FaqePaautorizuar");

            int idPerdoruesi = IdPerdoruesi;
            if (mySessionObjects.ktheKodNdermarrje(Session) == null)
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerdoruesi);

            CultureInfo cultinf = ci; ResourceManager resMng = rm;
            int idNdermarrja = IdNdermarrja, idGjuha = IdGjuha, idViti = IdViti;
            string komponente = clsFunksione.GetKomponente(Page.Request);
            ShtoMenuControlsDheMsgFrame();

            if (hfState.Count == 0)
            {
                hfState.Set("idNdermarrje", idNdermarrja);
                hfState.Set("idPerdoruesi", idPerdoruesi);
                hfState.Set("idGjuha", idGjuha);
                hfState.Set("EmriFile", "Dokumentat e magazines");
                hfState.Set("OwnShop", mySessionObjects.merrEshteOwnSesioni(Session));
                guidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                hfState.Set("guidString", guidString);
                if (Request.QueryString["refuzoDraft"] == "po")
                    clsMenuInfo.ShtoMesazhSuksesi(_menuInfo, MessagesResource.Messages["msgDokURefuzua"], _pnlMesazhi);
            }
            else
                guidString = (string)hfState["guidString"];

            if (!IsPostBack)
            {
                EmrateButonave();
                mbushHiddenFieldMePerkthime();
                if (Request.QueryString["lloj"] == "hyrje")
                    ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrja, cmbKonfigurimi, 51, "LDMH", resMng, cultinf, idGjuha);
                else
                    ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrja, cmbKonfigurimi, 207, "LDMD", resMng, cultinf, idGjuha);
                cmbKonfigurimi.SelectedIndex = 0;
                clsKonfigurimAmbjenti konf = new clsKonfigurimAmbjenti(Convert.ToInt32(cmbKonfigurimi.SelectedItem.Value), idGjuha);
                hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;

                clsTeDrejtaRoli tedrejtaDokumenta = new clsTeDrejtaRoli();

                int hyrjeDalje = Convert.ToBoolean(Request.QueryString["lloj"] == "hyrje") ? 1 : 2;
                if(hyrjeDalje == 1)
                    tedrejtaDokumenta.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrja, idViti, "RegjistrimMagazine.aspx?lloj=hyrje");
                else
                    tedrejtaDokumenta.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrja, idViti, "RegjistrimMagazine.aspx?lloj=dalje");

                hfTeDrejtaGjitheDok.Value = tedrejtaDokumenta.DGjitheDok.ToString();

                grid_RegMag.PercaktoTitlePanelMePeriudhe(this, _menuInfo, _pnlMesazhi, hfState, idPerdoruesi, idNdermarrja, idViti, idGjuha, konf.IdKonfigAmbjente, "RegjistrimMagazine.aspx", Request.QueryString["lloj"] == "hyrje" ? 513 : 519, "IdNivel", resMng, cultinf, false);

                mbushGridDokumentMagazineNgaDB(komponente, tedrejtaDokumenta.DGjitheDok, hyrjeDalje);
                grid_RegMag.FilterExpression = "[IdStatusDok]=1";
                hfLloji.Value = Request.QueryString["lloj"];

                clsTeDrejtaRoli tedrejtaInfo = new clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrja, idViti, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
                konfiguroGride(idGjuha, idNdermarrja, idPerdoruesi, komponente, resMng, cultinf);
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrja, "grid_RegMag", grid_RegMag, cmbKonfigurimi.Text.Split(';')[0], Request.QueryString["lloj"] == "hyrje" ? "513" : "519", idGjuha);
            }
            GridUtil.konfigGrideListeEMadhePaTheme(grid_RegMag, "IdKokaMagazina");
            if (IsPostBack)
            {
                grid_RegMag.PercaktoTitlePanelMePeriudhe(this, _menuInfo, _pnlMesazhi, hfState, idPerdoruesi, idNdermarrja, idViti, idGjuha, int.Parse(cmbKonfigurimi.Value.ToString()), "RegjistrimMagazine.aspx", Request.QueryString["lloj"] == "hyrje" ? 513 : 519, "IdKokaMagazina", resMng, cultinf, false);

                mbushGridDokumentMagazineNgaSession(komponente);
                Container.Attributes["src"] = "";
                konfiguroGride(idGjuha, idNdermarrja, idPerdoruesi, komponente, resMng, cultinf);
            }
            clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idNdermarrja, "grid_RegMag", int.Parse(cmbKonfigurimi.Value.ToString()), "RegjistrimMagazine.aspx");
            if (Request.QueryString["indexrow"] != null)
                grid_RegMag.FocusedRowIndex = int.Parse(Request.QueryString["indexrow"]);

            if (!IsPostBack)
                ShtoMesazhPerVeprim();
            clsNdermarrje ndermarrje = new clsNdermarrje(idNdermarrja);
            //if (ndermarrje.Fiskalizimi == true)
            //{
            //    using (ScriptManager scriptManager = (ScriptManager)this.FindControl("ScriptManager1"))
            //    {
            //        if (clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizim())
            //            scriptManager.RegisterPostBackControl(_menu);

            //    }
            //}
        }

        /// <summary>
        /// Metode qe sherben per te kaluar perkthime te js
        /// </summary>
        /// <param name="cultinf">Merr culture info</param>
        /// <param name="rm">Merr Resource Manager</param>
        private void mbushHiddenFieldMePerkthime()
        {
            hfState.Set("msgJuKeniZgjedhur", MessagesResource.Messages["msgJuKeniZgjedhur"]);
            hfState.Set("msgRreshta", MessagesResource.Messages["msgRreshta"]);
            hfState.Set("labelAdministrimiMsgJeniSigurt", MessagesResource.Messages["labelAdministrimiMsgJeniSigurt"]);
            hfState.Set("msgZgjdhniNjeNgaElementetEListes", MessagesResource.Messages["msgZgjdhniNjeNgaElementetEListes"]);
            hfState.Set("msgDokNukMundTeKonvertohet", MessagesResource.Messages["msgDokNukMundTeKonvertohet"]);
            hfState.Set("regjisDokZgjidhniTePakten1DokPerKonvertim", MessagesResource.Messages["regjisDokZgjidhniTePakten1DokPerKonvertim"]);
            hfState.Set("regjisDokNukKeniAsnjeDokTeZgjedhur", MessagesResource.Messages["regjisDokNukKeniAsnjeDokTeZgjedhur"]);
            hfState.Set("regjisDokZgjidhDokPerTeBashkengjitur", MessagesResource.Messages["regjisDokZgjidhDokPerTeBashkengjitur"]);
            hfState.Set("msgDokTeJeneTeSeNjejtesNenkategori", MessagesResource.Messages["msgDokTeJeneTeSeNjejtesNenkategori"]);
            hfState.Set("regjMagMesazhZgjidhniNje", MessagesResource.Messages["regjMagMesazhZgjidhniNje"]);
        }

        private void ShtoMesazhPerVeprim()
        {
            string param = Request.QueryString["fshi"];
            switch (param)
            {
                case "po":
                    clsMenuInfo.ShtoMesazhSuksesi(_menuInfo, MessagesResource.Messages["regjMagFshirjaPerfundoiMeSukses"], _pnlMesazhi);
                    break;
                case "rivleresimpo":
                    clsMenuInfo.ShtoMesazhSuksesi(_menuInfo, MessagesResource.Messages["regjMagSuksesFshirjeDokDheRivleresim"], _pnlMesazhi);
                    break;
                case "rivleresimjo":
                    clsMenuInfo.ShtoMesazhGabimi(_menuInfo, MessagesResource.Messages["regjMagMesazhGabimiRivleresim"], _pnlMesazhi);
                    break;
                case "rivleresimruajpo":
                    clsMenuInfo.ShtoMesazhSuksesi(_menuInfo, MessagesResource.Messages["regjMagMesazhSuksesiRuajteDokDheRivleresim"], _pnlMesazhi);
                    break;
                case "rivleresimruajjo":
                    clsMenuInfo.ShtoMesazhGabimi(_menuInfo, MessagesResource.Messages["regjMagMesazhGabimiGjateRivleresimit"], _pnlMesazhi);
                    break;
                default:
                    if (Request.QueryString["ruaj"] == "po")
                        clsMenuInfo.ShtoMesazhSuksesi(_menuInfo, MessagesResource.Messages["regjMagModifikimiPerfundoiMeSukses"], _pnlMesazhi);
                    break;
            }
        }
        /// <summary>
        /// Vendos emrat e butonave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="cultinf">Merr culture info perkatese</param>
        /// <param name="rm">Merr Resource Manager</param>
        private void EmrateButonave() => konfigurimi_Label.Text = MessagesResource.Messages["lblModeli"];

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        /// <param name="idViti"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNermarrje"></param>
        protected void PercaktoTemplateMenu(ASPxMenu menu, ASPxMenu menuInfo)
        {
            clsToolbarConfig.percaktoTemplateMenu(IdGjuha, IdViti, IdPerdoruesi, IdNdermarrja, menu, clsFunksione.GetKomponente(Page.Request), this, menuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, btnPo_Click, btnJo_Click, true, false, false, Meme, false);
        }
        
        
        private void mbushGridDokumentMagazineNgaSession(string komponente)
        {
            var periudha = this.MerrPeriudhe(hfState);
            DataTable tmpObject = grid_RegMag.MerrDataSourceMePeriduheNeSession<DataTable>(Session, komponente, periudha, guidString);

            if (tmpObject == null)
                mbushGridDokumentMagazineNgaDB(komponente, Convert.ToBoolean(hfTeDrejtaGjitheDok.Value), Convert.ToBoolean(Request.QueryString["lloj"] == "hyrje") ? 1 : 2);
            else
            {
                grid_RegMag.DataSource = tmpObject;
                grid_RegMag.DataBind();
                tmpObject.Dispose();
            }
        }

        private void mbushGridDokumentMagazineNgaDB(string komponente, bool gjitheDokumentat, int eshteHyrje)
        {//mbush griden e popupit me te dhena   

            bool meautorizim = clsAlternativaKushti.getAlternativa(int.Parse(cmbKonfigurimi.Value.ToString()), "DMSA") == "Po";   
            DataTable dt = colKokaMagazina.merrKokaMagazinaDT(IdNdermarrjeVit, IdPerdoruesi, Periudha.DataDokNga, Periudha.DataDokDeri, gjitheDokumentat, meautorizim, eshteHyrje);
            var periudha = this.MerrPeriudhe(hfState);
            grid_RegMag.RuajDataSourceMePeriduheNeSession(Session, komponente, periudha, dt, guidString);
            grid_RegMag.DataSource = dt;
            grid_RegMag.DataBind();
            dt.Dispose();
        }

        private void konfiguroGride(int idGjuha, int idNdermarrje, int idPerdoruesi, string komponente, ResourceManager rm, CultureInfo ci)
        {
            KonfigurimComboGride.ShtoNivel(grid_RegMag, 6, idNdermarrje, idPerdoruesi, idGjuha, Session, komponente, guidString);
            KonfigurimComboGride.ShtoModel(grid_RegMag, 6, idNdermarrje, idPerdoruesi, idGjuha, Session, komponente, guidString);
            KonfigurimComboGride.ShtoLlojDokumentMagazine(grid_RegMag, Session, komponente, guidString);
            KonfigurimComboGride.ShtoMagazinaNdermarrje(grid_RegMag, Session, komponente, guidString, "IdMagazina");
            KonfigurimComboGride.shto_DegeAdministrative(grid_RegMag, idNdermarrje, Session, komponente, guidString);
            KonfigurimComboGride.shto_Operatore(grid_RegMag, idNdermarrje, Session, komponente, guidString);
            KonfigurimComboGride.ShtoGrupimDokumentash(grid_RegMag, idNdermarrje, idPerdoruesi, Session, komponente, guidString, "IdGrup1", 1);
            KonfigurimComboGride.ShtoStatus(grid_RegMag, rm, ci);

            GridViewDataTextColumn col3 = grid_RegMag.Columns["Vlefta"] as GridViewDataTextColumn;
            col3.PropertiesEdit.DisplayFormatString = "0.00";
            this.grid_RegMag.Columns["#"].VisibleIndex = 0;
        }

        protected void grid_RegMag_DataBound(object sender, EventArgs e)
        {
            if (this.grid_RegMag.Columns["#"] == null)
            {
                DevExpress.Web.GridViewCommandColumn check = new DevExpress.Web.GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true; check.Width = Unit.Percentage(2);
                grid_RegMag.Settings.ShowFilterRow = true;
                grid_RegMag.Settings.ShowHeaderFilterButton = true;
                grid_RegMag.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                grid_RegMag.Settings.ShowFilterRowMenu = true;
                grid_RegMag.Columns.Add(check);
                grid_RegMag.Settings.ShowGroupPanel = true;
                grid_RegMag.KeyFieldName = "IdKokaMagazina";
                grid_RegMag.SettingsBehavior.AllowSelectByRowClick = true;
                grid_RegMag.SettingsBehavior.AllowFocusedRow = true;
            }
        }
       
        protected void Menu_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {
            switch (e.Item.Name)
            {
                case "PrintPreview":
                    string id = string.Empty, nrDok = string.Empty, idDesign = string.Empty;
                    if (grid_RegMag.FocusedRowIndex > -1)
                    {
                        id = grid_RegMag.GetRowValues(grid_RegMag.FocusedRowIndex, "IdKokaMagazina").ToString();
                        nrDok = grid_RegMag.GetRowValues(grid_RegMag.FocusedRowIndex, "NrDok").ToString();
                        idDesign = grid_RegMag.GetRowValues(grid_RegMag.FocusedRowIndex, "IdRaportDesign").ToString();
                    }

                    int idKoka = 0, idRapDesign = 0;
                    int.TryParse(id, out idKoka);
                    int.TryParse(idDesign, out idRapDesign);

                    List<object> rreshtatKoka = grid_RegMag.GetSelectedFieldValues("IdKokaMagazina", "NrDok", "IdRaportDesign");
                    if (rreshtatKoka.Count < 1 && idKoka > 0)
                        rreshtatKoka.Add(new object[3] { idKoka, nrDok, idRapDesign });

                    if (rreshtatKoka.Count < 1)
                    {
                        clsMenuInfo.ShtoMesazhGabimi(_menuInfo, rm.GetString("regjMagMesazhZgjidhniFaturePerPrintim", ci), _pnlMesazhi);
                        Container.Attributes["src"] = "";
                        return;
                    }

                    string rapEmriReal = "Rap_Format_Printimi_Magazina";
                    int nrRreshtaOk = 0;
                    clsMesazh sms = clsFunksione.ruajTeDhenaRaportiPerHapjeRaportiTeShpejte(rreshtatKoka, rapEmriReal, out nrRreshtaOk, Session);
                    if (!sms)
                        clsMenuInfo.ShtoMesazhGabimi(_menuInfo, string.Format(rm.GetString("regjDokumentaMesazhSkaFormatPerPrintim", ci), sms.PershkrimMesazhi), _pnlMesazhi);
                    
                    grid_RegMag.JSProperties["cpHapFaqe"] = $"RaportiShpejte.aspx?Sesioni=false&emriReal=Rap_Format_Printimi_Magazina&idDokumenti={idKoka}&printo=0&raportdyte=jo&iddesign={idRapDesign}";
                    break;

                case "Riruaj":
                    Riruaj();
                    break;
                case "Fiskalizo":
                    DataTable err = new DataTable();
                    fiskalizo(guidString, DbCore.clsFunksione.GetKomponente(Page.Request), ci, rm, false, IdGjuha, err);
                    break;
                case "DergoEmail":
                    string[] values = grid_RegMag.MerrVleratERreshtit(grid_RegMag.FocusedRowIndex, "IdKokaMagazina", "NrDok", "DtDok", "IdKlientFurnitor", "IdKonfigAmbjente", "IdMagazina", "IdRaportDesign", "Pershkrimi", "IdKategoria");

                    clsNjesiAdministrative magazina = new clsNjesiAdministrative(Convert.ToInt32(values[5]));
                    clsMesazh msg = EmailComposer.dergoEmailFaturenNgaPerdoruesiLoguarNgaMagazina((int)hfState["idGjuha"], ci, (int)hfState["idPerdoruesi"], (int)hfState["idViti"], (int)hfState["idNdermarrje"], int.Parse(values[0]), Convert.ToInt32(values[3]), values[1], Convert.ToDateTime(values[2]), Convert.ToInt32(values[6]), Convert.ToInt32(values[4]), magazina.Email, magazina.Kodi, values[7]);
                    if (!msg.Status)
                        clsMenuInfo.ShtoMesazhGabimi(_menuInfo, msg.PershkrimMesazhi, _pnlMesazhi);
                    else
                        clsMenuInfo.ShtoMesazhSuksesi(_menuInfo, msg.PershkrimMesazhi, _pnlMesazhi);
                    break;

                case "DergoEmailDokArkiva":
                    string[] vlerat = grid_RegMag.MerrVleratERreshtit(grid_RegMag.FocusedRowIndex, "IdKokaMagazina", "NrDok", "DtDok", "IdKlientFurnitor", "IdKonfigAmbjente", "IdMagazina", "IdRaportDesign", "Pershkrimi", "IdKategoria");

                    colArkiva arkiva = new colArkiva(int.Parse(vlerat[0]), int.Parse(vlerat[8]));
                    clsNjesiAdministrative magazina1 = new clsNjesiAdministrative(int.Parse(vlerat[5]));

                    clsMesazh mesazh = EmailComposer.dergoEmailFaturenNgaPerdoruesiLoguarNgaMagazina((int)hfState["idGjuha"], ci, (int)hfState["idPerdoruesi"], (int)hfState["idViti"], (int)hfState["idNdermarrje"], Convert.ToInt32(vlerat[0]), Convert.ToInt32(vlerat[3]), vlerat[1], Convert.ToDateTime(vlerat[2]), Convert.ToInt32(vlerat[6]), Convert.ToInt32(vlerat[4]), magazina1.Email, magazina1.Kodi, vlerat[7], MapPath(null), true, arkiva);

                    if (!mesazh.Status)
                        clsMenuInfo.ShtoMesazhGabimi(_menuInfo, mesazh.PershkrimMesazhi, _pnlMesazhi);
                    else
                        clsMenuInfo.ShtoMesazhSuksesi(_menuInfo, mesazh.PershkrimMesazhi, _pnlMesazhi);
                    break;

                default:
                    break;
            }
        }
        protected void fiskalizo(string guidString, string komponente, CultureInfo ci, ResourceManager rm, bool eshteMeme, int idGjuha, DataTable err)
        {
            int idPerdoruesi = (int)hfState["idPerdoruesi"];
            int idNdermarrje = (int)hfState["idNdermarrje"];

            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            pergjigja.Text = "";

            List<object> rreshtat = grid_RegMag.GetSelectedFieldValues("IdKokaMagazina");
            //List<object> rreshtatKodKlienti = grid_RegMag.GetSelectedFieldValues("idKlientFurnitor");
            List<object> rreshtatOperatori = grid_RegMag.GetSelectedFieldValues("IdOperator");
            List<object> rreshtatNenKategori = grid_RegMag.GetSelectedFieldValues("IdNivel");
            clsNdermarrje nderm = new clsNdermarrje(idNdermarrje);
            DataTable error = new DataTable();
            error.Columns.Add("Kodi");
            error.Columns.Add("Gabimi");
            error.Columns.Add("Rreshti");

            //return;
            string filePath = "";
            string zipName = "";
            if (nderm.Fiskalizimi)
            {

                if (rreshtat.Count > 1)
                {
                    for (var i = 0; i < rreshtat.Count; i++)
                    {
                        int idShitjeMagazina = Int32.Parse(rreshtat[i].ToString());
                        clsKokaMagazina kokaMagazina = new clsKokaMagazina(idShitjeMagazina);
                        if (kokaMagazina.NIVFSH != "")
                        {
                            clsMenuInfo.ShtoMesazhGabimi(_menuInfo, "Nuk mund te ridergoni fatura te fiskalizuara!", _pnlMesazhi);
                            return;
                        }
                        if (rreshtatOperatori[0] == System.DBNull.Value)
                        {
                            clsMenuInfo.ShtoMesazhGabimi(_menuInfo, "Ju Lutem Zgjidhni Operatorin Te Faturat!", _pnlMesazhi);
                            return;
                        }
                        if (kokaMagazina.IdDegeAdministrative == null)
                        {
                            error.Rows.Add("Dega administrative", "Plotesoni degen administrative!");
                        }
                        else
                        {
                            var degeAdministrativeKontroll = clsDegeAdministrative.ktheDegeAdministrativeSipasiD(kokaMagazina.IdDegeAdministrative);
                            if (degeAdministrativeKontroll["KODNJESIEBIZNES"].ToString() == "")
                                error.Rows.Add("Dega administrative", "Plotesoni Kodin e njesise se biznesit te dega administrative!");

                        }

                        if (nderm.NdermarrjeQytetiPershkrimi == "")
                            error.Rows.Add("Emer Qyteti Ndermarrje", "Vendosni Emrin E Qytetit Te Ndermarrjes Per Fiskalizimin!");
                        if (nderm.NdermarrjeNipt == "")
                            error.Rows.Add("Nipt Ndermarrje", "Vendosni Nipt-in e Ndermarrjes Per Fiskalizimin!");
                        if (nderm.NdermarrjeVendi == "")
                            error.Rows.Add("Shtet Ndermarrje", "Vendosni Shtetin e Ndermarrjes Per Fiskalizimin!");
                        if (kokaMagazina.NrDok.StartsWith("0"))
                            error.Rows.Add("Numer Dokumenti", "Numri I Dokumentit Nuk Duhet Te Filloj Me 0 Per Fiskalizimin!");
                        DbCore.clsMesazh mesazherror = new DbCore.clsMesazh();
                        clsKokaErrorImporti kokaErr = new clsKokaErrorImporti();

                    }
                    if (error.Rows.Count > 0)
                    {
                        var kokaErrs = new clsKokaErrorImporti(0, "Nga Fiskalizimi ", 1, nderm.IdNdermarrje, nderm.IdPerdoruesi);
                        kokaErrs.ColTrupi.mbushErrorImportiNgaProgrami(error);
                        var mesazherrors = kokaErrs.ruajErrorImporti();
                        DbCore.mySessionObjects.ruajTabeleGabimeshImporti(Session, error);
                        clsMenuInfo.ShtoMesazhGabimi(_menuInfo, "Kontrolloni Fushat E Gabuara!", _pnlMesazhi);
                        grid_RegMag.JSProperties["cpHapFaqe"] = "RaportiShpejte.aspx?Sesioni=false&emriReal=gabimeImporti&printo=0&db=jo";
                        return;
                    }
                    for (var i = 0; i < rreshtat.Count; i++)
                    {

                        int idShitjeKoke = Int32.Parse(rreshtat[i].ToString());
                        clsKokaMagazina kokaShitje = new clsKokaMagazina(idShitjeKoke);
                        colTrupiMagazina trupiMagazina = new colTrupiMagazina();
                        trupiMagazina.mbushGjitheTrupiMagazinaNgaKoka(idShitjeKoke);
                        var dateMaturimi = kokaShitje.DtTransporti.ToString().Split(' ')[0].Replace('/', '-');
                        string[] dateMaturimiList = dateMaturimi.Split(new[] { '-' }, 3);
                        var dateMaturimiFormatuar = $"{dateMaturimiList[2]}-{dateMaturimiList[1]}-{dateMaturimiList[0]}";
                        var dataTani = DateTime.Now.ToString("dd/MM/yyyy");
                        clsNjesiAdministrative njesiAdministrative = new clsNjesiAdministrative(kokaShitje.KodMagazina, idNdermarrje);
                        var degeAdministrative = clsDegeAdministrative.ktheDegeAdministrativeSipasiD(kokaShitje.IdDegeAdministrative);
                        var operatori = clsOperator.MerrEmerDheMbiemerOperatoriSipasId(kokaShitje.IdOperator, nderm.IdNdermarrje);
                        var emerMbiemerOperatori = operatori.ItemArray[0].ToString() + " " + operatori.ItemArray[1].ToString();
                        string[] emerMbiemerOperatorit = emerMbiemerOperatori.Split(' ');
                        string kodSoftueri = WebConfigurationManager.AppSettings["kodSoftueri"];
                        var kodOperatori = clsOperator.MerrKodOperatoriSipasId(kokaShitje.IdOperator, nderm.IdNdermarrje);
                        var wtnic = clsFunksioneFiskalizimi.GjeneroWTNIC(nderm, rreshtat[i].ToString(), kokaShitje.Vlefta.ToString(), "ur271so291", kodSoftueri);
                        var wtnicSignature = clsFunksioneFiskalizimi.GjeneroWTNICSignature(nderm, rreshtat[i].ToString(), kokaShitje.Vlefta.ToString(), "ur271so291", kodSoftueri);
                        clsNjesiAdministrative njesiAdministrativeDestinacion = new clsNjesiAdministrative(new clsKokaMagazina(clsKokaMagazina.merrIdDokHyrjeNgaTransferimi(kokaShitje.IdKokaMagazina)).IdMagazina);
                        string targa = new clsTransportues(kokaShitje.Transportuesi).Targa;
                        var mesazhInvoice = clsFunksioneFiskalizimi.gjeneroFatureShoqeruese(nderm, wtnic, wtnicSignature, kokaShitje.ShoqerimIKerkuar.ToString(), kokaShitje.MallraTeDjeghsme.ToString(), kokaShitje.Adresa, "Tirana", targa, trupiMagazina, kokaShitje.Vlefta.ToString(), kokaShitje.NrDok, kokaShitje.Transportuesi, njesiAdministrative.TipiMag, njesiAdministrative.Qyteti.ToString(), kokaShitje.DtTransporti.ToString(), true, degeAdministrative["KODNJESIEBIZNES"].ToString(), kokaShitje.Tipi, kokaShitje.Transaksioni,njesiAdministrativeDestinacion,njesiAdministrative, kodOperatori.ItemArray[0].ToString(),true);
                        if (i == 0)
                        {
                            zipName = DateTime.Now.ToString("yyyyMMddHHmmss");
                            filePath = clsFunksioneFiskalizimi.ruajZipFatura(mesazhInvoice[0], wtnic, zipName, false, Response);

                        }
                        else if (i > 0)
                        {

                            filePath = clsFunksioneFiskalizimi.ruajZipFatura(mesazhInvoice[0], wtnic, zipName, true, Response);
                        }
                    }
                }
                else
                {
                    string urlFiskalizimi = WebConfigurationManager.AppSettings["urlFiskalizimi"];
                    if (urlFiskalizimi == null)
                    {
                        clsMenuInfo.ShtoMesazhGabimi(_menuInfo, "!", _pnlMesazhi);
                        return;
                    }

                    if (rreshtatOperatori[0] == System.DBNull.Value)
                    {
                        clsMenuInfo.ShtoMesazhGabimi(_menuInfo, "Ndodhi nje gabim me fiskalizimin, fatura shoqeruese nuk u fiskalizua!", _pnlMesazhi);
                        return;
                    }

                    int idShitjeKoke = Int32.Parse(rreshtat[0].ToString());
                    clsKokaMagazina kokaMag = new clsKokaMagazina(idShitjeKoke);
                    if (kokaMag.NIVFSH != "") 
                    {
                        clsMenuInfo.ShtoMesazhGabimi(_menuInfo, "Nuk mund te ridergoni fatura te fiskalizuara!", _pnlMesazhi);
                        return;
                    }
                    colTrupiMagazina trupMagazine = new colTrupiMagazina();
                    trupMagazine.mbushGjitheTrupiMagazinaNgaKoka(idShitjeKoke);
                    
                    var dateMaturimi = kokaMag.DtTransporti.ToString().Split(' ')[0].Replace('/', '-');
                    string[] dateMaturimiList = dateMaturimi.Split(new[] { '-' }, 3);
                    var dateMaturimiFormatuar = $"{dateMaturimiList[2]}-{dateMaturimiList[1]}-{dateMaturimiList[0]}";
                    var dataTani = DateTime.Now.ToString("dd/MM/yyyy");
                    clsNjesiAdministrative njesiAdministrative = new clsNjesiAdministrative(kokaMag.IdMagazina, idPerdoruesi);
                    var degeAdministrative = clsDegeAdministrative.ktheDegeAdministrativeSipasiD(kokaMag.IdDegeAdministrative);
                    var operatori = clsOperator.MerrEmerDheMbiemerOperatoriSipasId(kokaMag.IdOperator, nderm.IdNdermarrje);
                    var emerMbiemerOperatori = operatori.ItemArray[0].ToString() + " " + operatori.ItemArray[1].ToString();
                    string[] emerMbiemerOperatorit = emerMbiemerOperatori.Split(' ');
                    var kodOperatori = clsOperator.MerrKodOperatoriSipasId(kokaMag.IdOperator, nderm.IdNdermarrje);
                    string kodSoftueri = WebConfigurationManager.AppSettings["kodSoftueri"];
                    string wtnic = "";
                    if (kokaMag.WTNIC == "")
                        wtnic = clsFunksioneFiskalizimi.GjeneroWTNIC(nderm, rreshtat[0].ToString(), kokaMag.Vlefta.ToString(), "ur271so291", kodSoftueri);
                    else
                        wtnic = kokaMag.WTNIC;
                    var wtnicSignature = clsFunksioneFiskalizimi.GjeneroWTNICSignature(nderm, rreshtat[0].ToString(), kokaMag.Vlefta.ToString(), "ur271so291", kodSoftueri);
                    string[] dateTransportimiList = kokaMag.DtTransporti.ToString().Replace('/', '-').Split(new[] { '-' }, 3);
                    var dateTransportimi = $"{dateTransportimiList[2].Split(' ')[0]}-{dateTransportimiList[1]}-{dateTransportimiList[0]}" + "T" + $"{dateTransportimiList[2].Split(' ')[1]}+01:00";
                    clsNjesiAdministrative njesiAdministrativeDestinacion = new clsNjesiAdministrative(new clsKokaMagazina(clsKokaMagazina.merrIdDokHyrjeNgaTransferimi(kokaMag.IdKokaMagazina)).IdMagazina);
                    string targa = new clsTransportues(kokaMag.Transportuesi).Targa;
                    var mesazhInvoice = clsFunksioneFiskalizimi.gjeneroFatureShoqeruese(nderm, wtnic, wtnicSignature, kokaMag.ShoqerimIKerkuar.ToString(), kokaMag.MallraTeDjeghsme.ToString(), kokaMag.Adresa, "Tirana", targa, trupMagazine, kokaMag.Vlefta.ToString(), kokaMag.NrDok, kokaMag.Transportuesi, njesiAdministrative.TipiMag, njesiAdministrative.Qyteti.ToString(), dateTransportimi, false, degeAdministrative["KODNJESIEBIZNES"].ToString(), kokaMag.Tipi, kokaMag.Transaksioni,njesiAdministrativeDestinacion,njesiAdministrative, kodOperatori.ItemArray[0].ToString(),true);
                    if (error.Rows.Count > 0)
                    {
                        var kokaErrs = new clsKokaErrorImporti(0, "Nga Fiskalizimi ", 1, nderm.IdNdermarrje, nderm.IdPerdoruesi);
                        kokaErrs.ColTrupi.mbushErrorImportiNgaProgrami(error);
                        var mesazherrors = kokaErrs.ruajErrorImporti();
                        DbCore.mySessionObjects.ruajTabeleGabimeshImporti(Session, error);
                        Container.Attributes["src"] = "RaportiShpejte.aspx?Sesioni=false&emriReal=gabimeImporti&printo=0&db=jo";
                        return;
                    }
                    var nivfshFature = clsFunksioneFiskalizimi.InvokeService(mesazhInvoice[0], "FWTNIC", false);
                    if (nivfshFature[1] != null)
                    {
                        //var objekti = ktheObjektPerNotify(kokaShitje, "Deshtim", clsKokaShitje.merrTrupiShitje(kokaShitje.IdKokaMagazina), new clsTrupiShitje(), nivfshFature[0], nivfshFature[1], "Fature Shoqeruese");
                        //clsFunksioneFiskalizimi.dergoWebhookNotify(objekti, false);
                        clsMenuInfo.ShtoMesazhGabimi(_menuInfo, "Ndodhi nje gabim me fiskalizimin, fatura shoqeruese nuk u fiskalizua!" + $"Error:{nivfshFature[1]}" + $" Pershkrimi i errorit:{nivfshFature[0]}", _pnlMesazhi);

                    }
                    else
                    {
                        kokaMag.NIVFSH = nivfshFature[0];
                        kokaMag.WTNIC = wtnic;
                        kokaMag.shtoNivfshTeMagazina(idNdermarrje, kokaMag.IdKokaMagazina, nivfshFature[0]);
                        kokaMag.shtoWTNICTeMagazina(idNdermarrje, kokaMag.IdKokaMagazina, wtnic);
                        string serverUrl = clsFunksione.ktheServerUrl(Request);
                        clsKonfigurimAmbjenti konfig = new clsKonfigurimAmbjenti(kokaMag.IdKonfigAmbjente);
                        DataTable riruajtje = colKokaMagazina.riruajMag(ci, rm, ref mesazh, rreshtat, IdPerdoruesi, idNdermarrje, IdNdermarrjeVit, idGjuha, true, hfArkiva, false);
                        clsMenuInfo.ShtoMesazhSuksesi(_menuInfo, "Fisaklizimi u krye me sukses!", _pnlMesazhi);
                        //var objekti = ktheObjektPerNotify(kokaShitje, "Sukses", clsKokaShitje.merrTrupiShitje(kokaShitje.IdKokaMagazina), new clsTrupiShitje(), nivfshFature[0], nivfshFature[1], "Fature Shoqeruese");
                        //clsFunksioneFiskalizimi.dergoWebhookNotify(objekti, false);
                    }

                }
            }
            else
            {
                //clsMenuInfo.ShtoMesazhInformues(MenuInfo, "Ndermarrja nuk ka aplikuar fiskalizimin!", pnlMesazhi);
                return;
            }
            CultureInfo cultInfo = ci; ResourceManager resMng = rm;

            mbushGridDokumentMagazineNgaDB(komponente, Convert.ToBoolean(hfTeDrejtaGjitheDok.Value), Convert.ToBoolean(Request.QueryString["lloj"] == "hyrje") ? 1 : 2);
            konfiguroGride(idGjuha, idNdermarrje, IdPerdoruesi, komponente, resMng, cultInfo);
            bool teDrejtaGjitheDok = hfTeDrejtaGjitheDok.Value.ToString().ToLower() == "true";
            grid_RegMag.Selection.UnselectAll();

            if (rreshtat.Count > 1)
            {

                clsFunksioneFiskalizimi.downloadFileToClientZip(filePath, Response, idPerdoruesi);
            }

        }
        protected void Riruaj()
        {
            CultureInfo cultInfo = ci; ResourceManager resMng = rm;
            clsMesazh mesazh = new clsMesazh();
            List<string> TeRuajtur = new List<string>(), Teparuajtur = new List<string>();
            pergjigja.Text = "";

            List<object> rreshtat = this.grid_RegMag.GetSelectedFieldValues("IdKokaMagazina");
            //int idPerdoruesi = (int)hfState["idPerdoruesi"];
            int idNdermarrje = (int)hfState["idNdermarrje"];
            //int idNdermarrjeVit = (int)hfState["idNdermarrjeVit"];
            int idGjuha = (int)hfState["idGjuha"];
            bool eshteOwn = (bool)hfState["OwnShop"];

            DataTable err = colKokaMagazina.riruajMag(cultInfo, resMng, ref mesazh, rreshtat, IdPerdoruesi, idNdermarrje, IdNdermarrjeVit, idGjuha, eshteOwn, hfArkiva, false);
            DbCore.mySessionObjects.ruajTabeleGabimeshImporti(Session, err);
            if (err.Rows.Count > 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(_menuInfo, "U riruajten " + (rreshtat.Count - err.Rows.Count) + " rreshta dhe deshtuan " + err.Rows.Count + " rreshta! ", _pnlMesazhi);
                Container.Attributes["src"] = "RaportiShpejte.aspx?Sesioni=false&emriReal=gabimeImporti&printo=false&db=jo";
            }
            else
                clsMenuInfo.ShtoMesazhSuksesi(_menuInfo, "U riruajten te gjitha rreshtat!", _pnlMesazhi);

            this.grid_RegMag.Selection.UnselectAll();
            string komponente = clsFunksione.GetKomponente(Page.Request);
            mbushGridDokumentMagazineNgaDB(komponente, Convert.ToBoolean(hfTeDrejtaGjitheDok.Value), Convert.ToBoolean(Request.QueryString["lloj"] == "hyrje") ? 1 : 2);
            konfiguroGride(idGjuha, idNdermarrje, IdPerdoruesi, komponente, resMng, cultInfo);

            if (rreshtat.Count == 0)
                clsMenuInfo.ShtoMesazhGabimi(_menuInfo, MessagesResource.Messages["regjMagMesazhZgjidhniNje"], _pnlMesazhi);
        }        

        public void btnJo_Click(object sender, EventArgs e)
        {
            if (pergjigja.Text.Contains("lidhur") || pergjigja.Text.Contains("kycur"))
                clsMenuInfo.ShtoMesazhGabimi(_menuInfo, pergjigja.Text, _pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhSuksesi(_menuInfo, pergjigja.Text, _pnlMesazhi);
            mySessionObjects.ruajTrupatNeSession(Session, new colTrupiMagazina());
        }

        public void btnPo_Click(object sender, EventArgs e)
        {
            int idNdermarrja = IdNdermarrja, idPerdoruesi = IdPerdoruesi;
            trupat = mySessionObjects.merrTrupatNgaSesioni(Session);
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["regjMagMesazhSuksesRivleresimi"]);

            clsLogRivleresimInventari log = new clsLogRivleresimInventari();
            try
            {
                log = new DbCore.DbAdmin.clsLogRivleresimInventari(idPerdoruesi, idNdermarrja);
            }
            catch (Exception err)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
                throw new MyException(MessagesResource.Messages["msgGabimGjateRuajtjesSeRivleresimitNeLog"]);
            }
            foreach (DbCore.DbRegjistrim.clsTrupiMagazina t in trupat)
            {
                mesazh = DbCore.DbInventari.clsArtikulli.rivleresimCmimiMesatar(t.IdArtikulli, clsArtikulli.ktheMetodeKostoje(t.IdArtikulli), t.IdMag, t.Data, DateTime.Today, clsArtikulli.ktheKontrollCmimiPerDetajim(t.IdArtikulli), log, ci, rm, idNdermarrja, idPerdoruesi);
                if (!mesazh.Status)
                {
                    clsMenuInfo.ShtoMesazhGabimi(_menuInfo, MessagesResource.Messages["regjMagMesazhGabimiRivleresim"], _pnlMesazhi);
                    return;
                }
            }

            DbCore.mySessionObjects.ruajTrupatNeSession(Session, new DbCore.DbRegjistrim.colTrupiMagazina());
            if (mesazh.Status)
                clsMenuInfo.ShtoMesazhSuksesi(_menuInfo, MessagesResource.Messages["regjMagMesazhSuksesRivleresimi"], _pnlMesazhi);
        }

        protected void grid_RegMag_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = grid_RegMag.PageIndex;
            e.Properties["cpPageRow"] = grid_RegMag.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = grid_RegMag.VisibleRowCount;
        }

        protected void grid_RegMag_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            int idNdermarrje = IdNdermarrja, idGjuha = IdGjuha, idKonfig = Convert.ToInt32(cmbKonfigurimi.Value);
            if (grid_RegMag.AplikoFilterDefault(e, idKonfig))
                return;

            string[] arr = e.Parameters.Split(';');
            if (!arr.Contains(TitlePeriudha.KeyParamNdryshimPeriudhe))
            {
                if (arr.Length == 1) //rasti kur behet callback nga filtri i periudhes duhet te ngelen filtrat ne gride dhe te mos aplikohet filtri default
                    GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "grid_RegMag", grid_RegMag, cmbKonfigurimi.Text.Split(';')[0], Request.QueryString["lloj"] == "hyrje" ? "513" : "519", idGjuha, false);
                else
                    GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "grid_RegMag", grid_RegMag, cmbKonfigurimi.Text.Split(';')[0], Request.QueryString["lloj"] == "hyrje" ? "513" : "519", idGjuha);

                if (arr.Length == 2)
                    mbushGridDokumentMagazineNgaSession(clsFunksione.GetKomponente(Page.Request));
                if (arr.Length == 3)
                {
                    if (arr[2] == "")
                    {
                        if (Request.QueryString["lloj"] == "hyrje")
                            grid_RegMag.FilterExpression = "([IdLlojDokumentiMagazine] = 1) and [IdStatusDok]=1";
                        else if (Request.QueryString["lloj"] == "dalje")
                            grid_RegMag.FilterExpression = "([IdLlojDokumentiMagazine] = 2) and [IdStatusDok]=1";
                    }
                    else
                    {
                        clsFiltraGrida filtra = new clsFiltraGrida();
                        clsGridaKoka koka = new clsGridaKoka(idGjuha, "grid_RegMag", "RegjistrimMagazine.aspx", idNdermarrje, idKonfig);
                        filtra.mbushFilterPerGrideSipasKodit(arr[2], idNdermarrje, koka.IdGridaKoka);
                        if (filtra.FiltraKodi != null)
                        {
                            grid_RegMag.FilterExpression = filtra.FiltraVlera;
                            GridUtil.renditGriden(filtra.KoloneRenditje, grid_RegMag);
                        }
                    }
                }
            }

            grid_RegMag.Selection.UnselectAll();
        }

        protected void grid_RegMag_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if ((e.Column.FieldName == "IdMagazina" || e.Column.FieldName == "IdKonfigAmbjente") && DbCore.IMBUtils.Types.Converter.ConvertToInt(e.Value) == 0)
                e.Criteria = null;
        }

        protected void grid_RegMag_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            string TeGjithe = MessagesResource.Messages["GridHeaderFilterFillItemTeGjithe"];
            string nga = MessagesResource.Messages["GridHeaderFilterFillItemNga"];
            if (e.Column.FieldName == "NrDok" || e.Column.FieldName == "Shenime")
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

        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
            menu_msg_Frame.RuajFilter(grid_RegMag, "RegjistrimMagazine.aspx", Convert.ToInt32(cmbKonfigurimi.Value), ref hfStatusi, "IdNivel");
        }

        protected void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
            menu_msg_Frame.FshiFilter(grid_RegMag, "RegjistrimMagazine.aspx", Convert.ToInt32(cmbKonfigurimi.Value), ref hfStatusi);

            if (Request.QueryString["lloj"] == "hyrje")
                grid_RegMag.FilterExpression = "([IdLlojDokumentiMagazine] = 1) and [IdStatusDok]=1";
            else if (Request.QueryString["lloj"] == "dalje")
                grid_RegMag.FilterExpression = "([IdLlojDokumentiMagazine] = 2) and [IdStatusDok]=1";
        }
        private object ktheObjektPerNotify(clsKokaMagazina kokaMagazina, string statusi, colTrupiShitje artikujt, clsTrupiShitje trupiShitje, string pershkrim, string kodErrori, string TipFature)
        {
            string emerDatabaze = clsKontrollePerFiskalizimin.ktheInitialCatalogTeLoguar();
            clsKokaMagazina kokeMagazine = new clsKokaMagazina(kokaMagazina.IdKokaMagazina);
            clsDegeAdministrative degeAdministrative = new clsDegeAdministrative(kokeMagazine.IdDegeAdministrative);
            string kodiINjesiseSeBiznesit = clsDegeAdministrative.ktheDegeAdministrativeSipasiD(kokeMagazine.IdDegeAdministrative)["KODNJESIEBIZNES"].ToString();
            clsNdermarrje ndermarrje = new clsNdermarrje(kokeMagazine.IdNdermarrje);
            clsTransportues transportues = new clsTransportues(kokeMagazine.Transportuesi);
            var emerOperatori = clsOperator.MerrEmerDheMbiemerOperatoriSipasId(kokeMagazine.IdOperator, kokeMagazine.IdNdermarrje);
            return new
            {
                Statusi = statusi,
                meta = new
                {
                    Tipi = TipFature,
                    Pershkrim = pershkrim,
                    KodErrori = kodErrori,
                    NumerDokumenti = kokeMagazine.NrDok,
                    Ndermarrja = new clsNdermarrje(kokeMagazine.IdNdermarrje).NdermarrjeKodi,
                    Organizata = emerDatabaze
                },
                Dokumenti = new
                {
                    Koka = new
                    {
                        DateDokumenti = kokeMagazine.DtDok,
                        NIVFSH = kokeMagazine.NIVFSH,
                        WTNIC = kokeMagazine.WTNIC,
                        Adresa = kokeMagazine.Adresa,
                        DegaAdministrative = degeAdministrative.Kodi,
                        kodiINjesiseSeBiznesit = kodiINjesiseSeBiznesit,
                        Transportuesi = transportues.Emertimi,
                        TargeTransportuesi = transportues.Targa,
                        ShoqerimIKerkuar = kokeMagazine.ShoqerimIKerkuar,
                        MallraTeDjegshme = kokeMagazine.MallraTeDjeghsme,
                        EmerMbiemerOperatori = emerOperatori.ItemArray[0].ToString() + " " + emerOperatori.ItemArray[1].ToString(),
                        KodOperatori = clsOperator.MerrKodOperatoriSipasId(kokeMagazine.IdOperator, kokeMagazine.IdNdermarrje),
                        Ndermarrje = new
                        {
                            Emertimi = ndermarrje.NdermarrjeKodi,
                            Nipt = ndermarrje.NdermarrjeNipt,
                            Qyteti = new clsQyteti(ndermarrje.NdermarrjeQyteti).EmriQyteti,
                            Vendi = ndermarrje.NdermarrjeVendi
                        }
                    },
                    Trupi = new
                    {
                        Artikujt = artikujt
                    }



                },


            };
        }
    }
}