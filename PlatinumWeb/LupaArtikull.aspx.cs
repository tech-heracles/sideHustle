using System;
using System.Data;
using System.Web.UI.WebControls;
using DbCore;
using DbCore.DbAdmin;
using DbCore.DbInventari;
using DbCore.DbShare;
using DbCore.IMBUtils.Types;
using DevExpress.Data;
using DevExpress.Data.Filtering;
using DevExpress.Web;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class LupaArtikull : MyPageBase
    {
        private const string Komponente = "LupaArtikull.aspx";
        private string _guidString;
        private int IdNdermarrjePerListe;
        protected void Page_Load(object sender, EventArgs e)
        {
            int idKonfigambjenti;
            bool ruajFilterGrid;
            IdNdermarrjePerListe = !(String.IsNullOrEmpty(Request.QueryString["idNdermarrje"])) ? Convert.ToInt32(Request.QueryString["idNdermarrje"]) : base.IdNdermarrja;
            var postStringTvsh = rm.GetString("postStringTvsh", ci);
            hfState.Set(clsArtikulli.postStringTvsh, rm.GetString("postStringTvsh", ci));
            if (!IsPostBack)
            {
                if (!mySessionObjects.isLogedIn(Session))
                    clsFunksione.logout(Session, true, "FaqePaautorizuar");
                var vleraQueryString = "";
                if (Request.QueryString["idKonfigAmbjente"] != null && Request.QueryString["idKonfigAmbjente"] != "")
                    vleraQueryString = Request.QueryString["idKonfigAmbjente"];
                idKonfigambjenti = clsFunksione.getIdKonfigAmbLupaMeAutorizim(vleraQueryString, IdNdermarrja, "ARTIKULL", IdPerdoruesi);
                var kodKonfigLupa = clsKonfigurimAmbjenti.ktheKodKonfigurimi(idKonfigambjenti);
                var artikujTeShitshem = !string.IsNullOrEmpty(Request.QueryString["shitshem"]) && bool.Parse(Request.QueryString["shitshem"]);
                ruajFilterGrid = MerrVlereRuajFliter(idKonfigambjenti);
                _guidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                hfState.Set("guidString", _guidString);
                hfState.Set("idPerdoruesi", IdPerdoruesi);
                hfState.Set("idGjuha", IdGjuha);
                hfState.Set("idNdermarrje", IdNdermarrja);
                hfState.Set("idViti", IdViti);
                hfState.Set("idNdermarrjeVit", IdNdermarrjeVit);
                hfState.Set("Meme", mySessionObjects.merrEshteMemeSesioni(Session));
                hfState.Set("artikujTeShitshem", artikujTeShitshem);
                hfState.Set("idKonfigambjenti", idKonfigambjenti);
                hfState.Set("kodKonfigLupa", kodKonfigLupa);
                cmbKonfigurimi.Value = idKonfigambjenti;
                MbushHiddenFieldMePerkthime();
                PercaktoTemplateMenu();
                clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, IdNdermarrja, "gvLupaArtikull", (int)hfState["idKonfigambjenti"], Komponente);
                mySessionObjects.ruajMosMerrNgaDbNeSesion(Session, false);
                hfState.Add("MerrDB", clsAlternativaKushti.getAlternativa(idKonfigambjenti, "SHVF") == "Po");
                hfState.Add("KSSH", clsAlternativaKushti.getAlternativa(idKonfigambjenti, "KSSH") == "Po");
                hfState.Add("ES", clsAlternativaKushti.getAlternativa(idKonfigambjenti, "ES") == "Po");

                var kosto = false;
                var gjendja = false;
                var idMagazina = -1;
                var cmime = false;
                var cmimeMeTvsh = false;
                var atrGjendja = clsAtributeTrupi.merrVleredefaultSipasKontrollitDheKonfigurimit(idKonfigambjenti, "cbGjendje", 610);
                var atrKosto = clsAtributeTrupi.merrVleredefaultSipasKontrollitDheKonfigurimit(idKonfigambjenti, "cbKosto", 610);
                var atrCmime = clsAtributeTrupi.merrVleredefaultSipasKontrollitDheKonfigurimit(idKonfigambjenti, "cbCmime", 610);
                var atrCmimeMeTvsh = clsAtributeTrupi.merrVleredefaultSipasKontrollitDheKonfigurimit(idKonfigambjenti, "cbCmimeMeTvsh", 610);
                var dtgjendje = clsAtributeTrupi.merrVleredefaultSipasKontrollitDheKonfigurimit(idKonfigambjenti, "dtDataGjendje", 610);
                var kodifikimi1 = clsAtributeTrupi.merrVleredefaultSipasKontrollitDheKonfigurimit(idKonfigambjenti, "btneKodifikimi1", 610);
                var kodifikimi2 = clsAtributeTrupi.merrVleredefaultSipasKontrollitDheKonfigurimit(idKonfigambjenti, "btneKodifikimi2", 610);
                var kodifikimi3 = clsAtributeTrupi.merrVleredefaultSipasKontrollitDheKonfigurimit(idKonfigambjenti, "btneKodifikimi3", 610);
                if (dtgjendje == "") dtgjendje = DateTime.Now.ToShortDateString();
                dtDataGjendje.Text = dtgjendje;
                if (!string.IsNullOrEmpty(Request.QueryString["idMag"]))
                    int.TryParse(Request.QueryString["idMag"], out idMagazina);
                else
                {
                    var atrMag = clsAtributeTrupi.merrVleredefaultSipasKontrollitDheKonfigurimit(idKonfigambjenti, "btneMagazina", 610);
                    if (atrMag != "")
                        int.TryParse(atrMag, out idMagazina);
                }
                var dhurataVfOne = false;
                if (Request.QueryString["grup"] != null && Request.QueryString["grup"] != "")
                {
                    var grup = Request.QueryString["grup"].ToLower();
                    if (grup != "dhurate" && (grup != string.Empty || grup != "te gjitha" || grup != "dhuratevfone"))
                    {
                        if (grup != "dhuratevfone")
                        {
                            kodifikimi2 = grup == "aparate ekspozitore" ? "aparate" : grup;
                            kodifikimi1 = string.Empty;
                            kodifikimi3 = string.Empty;
                        }
                        else
                            dhurataVfOne = true;
                    }
                }
                int idKodifikimi1 = -1, idKodifikimi2 = -1, idKodifikimi3 = -1;
                if (!string.IsNullOrEmpty(kodifikimi1))
                    idKodifikimi1 = clsKodifikimArtikulli.ktheIdKodifikimi(kodifikimi1, IdNdermarrja, 1, false);
                if (!string.IsNullOrEmpty(kodifikimi2))
                {
                    idKodifikimi2 = clsKodifikimArtikulli.ktheIdKodifikimi(kodifikimi2, IdNdermarrja, 2, false);
                    if (idKodifikimi2 == 0)
                        idKodifikimi2 = -1;
                }
                if (!string.IsNullOrEmpty(kodifikimi3))
                    idKodifikimi3 = clsKodifikimArtikulli.ktheIdKodifikimi(kodifikimi3, IdNdermarrja, 3, false);
                if (atrGjendja == "true")
                {
                    gjendja = true;
                    cbGjendje.Checked = true;
                }
                if (atrKosto == "true")
                {
                    kosto = true;
                    cbKosto.Checked = true;
                }
                if (atrCmime == "true")
                {
                    cmime = true;
                    cbCmime.Checked = true;
                }
                if (atrCmimeMeTvsh == "true")
                {
                    cmimeMeTvsh = true;
                    cbCmimeMeTvsh.Checked = true;
                }
                if (Request.QueryString["aqt"] != null && Request.QueryString["aqt"] == "aqt")
                    gvLupaArtikull.FilterExpression = "[LlojiArt] = 1";

                ConfigureAspxComboBox.percaktoTemplateComboMeLupe(btneMagazina);
                ConfigureAspxComboBox.mbushComboMagazinat(IdNdermarrja, btneMagazina, IdPerdoruesi, false, 0, true);
                ConfigureAspxComboBox.percaktoTemplateComboMeLupe(btneKodifikimi1, btneKodifikimi2, btneKodifikimi3);
                ConfigureAspxComboBox.mbushComboKodifikimPrind(IdNdermarrja, btneKodifikimi1, 1, Request.QueryString["llojiart"] == "aqt", true);
                ConfigureAspxComboBox.mbushComboKodifikimPrind(IdNdermarrja, btneKodifikimi2, 2, Request.QueryString["llojiart"] == "aqt", true);
                ConfigureAspxComboBox.mbushComboKodifikimPrind(IdNdermarrja, btneKodifikimi3, 3, Request.QueryString["llojiart"] == "aqt", true);
                var kodeNivCmimi = MbushKodeNivelCmimiDheRuajNeSesion(IdNdermarrja, IdPerdoruesi);
                cbRuajFilter.Checked = ruajFilterGrid;
                GridUtil.AplikoFilterDefault(gvLupaArtikull, idKonfigambjenti);
                gvLupaArtikull.Columns.Clear();
                MbushPopUpListeArtikujshNgaDb(IdPerdoruesi, IdNdermarrjePerListe, kosto || gjendja, idMagazina, cmime, cmimeMeTvsh, artikujTeShitshem, (bool)hfState["MerrDB"], dtgjendje, idKodifikimi1, idKodifikimi2, idKodifikimi3, dhurataVfOne);
                //hfState.Set("MerrDB", false);
                KonfiguroPopupGride(IdNdermarrjePerListe, kodeNivCmimi, cmime, cmimeMeTvsh, postStringTvsh);
                kodeNivCmimi.Dispose();
                GridUtil.percaktoVisibleColumnsSipasKonfigurimit(gvLupaArtikull, "gvLupaArtikull", Komponente, idKonfigambjenti, true, IdGjuha);
            }
            else
            {
                idKonfigambjenti = (int)hfState["idKonfigambjenti"];
                _guidString = (string)hfState["guidString"];
                ruajFilterGrid = MerrVlereRuajFliter(idKonfigambjenti);
                postStringTvsh = (string)hfState.Get(clsArtikulli.postStringTvsh);
                if (!IsCallback || IsCallback && Request["__CALLBACKID"].Contains("ASPxMenu1"))
                    PercaktoTemplateMenu();
                cbRuajFilter.Checked = ruajFilterGrid;
                if (!IsCallback || IsCallback && Request["__CALLBACKID"].Contains("gvLupaArtikull"))
                {
                    MbushPopUpListeArtikujshNgaSession();
                    KonfiguroPopupGride(IdNdermarrjePerListe, null, cbCmime.Checked, cbCmimeMeTvsh.Checked, postStringTvsh);
                   // GridUtil.percaktoVisibleColumnsSipasKonfigurimit(gvLupaArtikull, "gvLupaArtikull", Komponente, idKonfigambjenti, true, (int)hfState["idGjuha"]);
                }
            }
            gridaSelectButtons.Visible = (Request.QueryString["vjenNgaRaporti"] == "true");
            gvLupaArtikull.Columns["Gjendje"].Visible = cbGjendje.Checked;
            gvLupaArtikull.Columns["Kosto"].Visible = cbKosto.Checked;
            GridUtil.KonfiguroGrideListeMadhePopupiPaTheme(gvLupaArtikull, "IdArtikulli", (bool)hfState["KSSH"], (bool)hfState["ES"]);
            gridaSelectFaqe.ToolTip = rm.GetString("btnZgjidhTeGjitheFaqen", ci);
            gridaSelectTeGjitha.ToolTip = rm.GetString("btnZgjidhTeGjithe", ci);
            gridaUnSelectTeGjitha.ToolTip = rm.GetString("btnFshiZgjedhjen", ci);
            lblGjendje.ToolTip = rm.GetString("lblKaGjendje", ci);
        }

        protected DataTable MbushKodeNivelCmimiDheRuajNeSesion(int idNdermarrje, int idPerdorues)
        {
            var dtKodeNivCmimi = clsNivelCmimi.merrKodeNiveleCmimiSipasNdermarrjesDhePerdoruesit(idNdermarrje, idPerdorues);
            mySessionObjects.ruajGrideNeSession("KodeNivelCmimi", Session, dtKodeNivCmimi);
            return dtKodeNivCmimi;
        }

        protected DataTable MerrKodeNivelCmimiNgaSesioni(int idNdermarrje, int idPerdorues)
        {
            DataTable dtKodeNivCmimi;
            mySessionObjects.merrGrideNgaSessioni("KodeNivelCmimi", Session, out dtKodeNivCmimi);
            if (dtKodeNivCmimi == null)
            {
                dtKodeNivCmimi = clsNivelCmimi.merrKodeNiveleCmimiSipasNdermarrjesDhePerdoruesit(idNdermarrje, idPerdorues);
                mySessionObjects.ruajGrideNeSession("KodeNivelCmimi", Session, dtKodeNivCmimi);
            }
            return dtKodeNivCmimi;
        }

        private void MbushHiddenFieldMePerkthime()
        {
            popupUniversal.HeaderText = rm.GetString("headerPopUpText", ci);
            hfState.Set("msgZgjidhMagazinen", rm.GetString("msgZgjidhMagazinen", ci));
            hfState.Set("headerShtoFilter", rm.GetString("headerShtoFilter", ci));
            hfState.Set("JQgridShtoArtikull", rm.GetString("JQgridShtoArtikull", ci));
            hfState.Set("msgSelektoniNjeRresht", rm.GetString("msgSelektoniNjeRresht", ci));
            hfState.Set("headerPopUpKlonoArtikull", rm.GetString("headerPopUpKlonoArtikull", ci));
            hfState.Set("JQgridShtoArtikullAqt", rm.GetString("JQgridShtoArtikullAqt", ci));
            hfState.Set("postStringTvsh", rm.GetString("postStringTvsh", ci));
            hfState.Set("headerPopUpZgjidhKodifikiminArtikullit", rm.GetString("headerPopUpZgjidhKodifikiminArtikullit", ci));
        }

        /// <summary>
        /// Mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        /// <param name="idViti"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNermarrje"></param>
        private void PercaktoTemplateMenu()
        {
            var idGjuha = (int)hfState["idGjuha"];
            var meme = (bool)hfState["Meme"];
            clsToolbarConfig.percaktoTemplateMenu(idGjuha, IdViti, IdPerdoruesi, IdNdermarrja, ASPxMenu1, Komponente, this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, true, false, false, meme);
            ASPxMenu1.Items.FindByName("Anullo").Text = rm.GetString("MenuItemMbyll", ci);
        }

        private void MbushPopUpListeArtikujshNgaSession()
        {
            object tmpObject;
            mySessionObjects.merrGrideNgaSessioniLupaArtikull(Session, out tmpObject);
            if (tmpObject == null)
            {
                int idMagazina = -1, idKodifikimi1 = -1, idKodifikimi2 = -1, idKodifikimi3 = -1;
                if (btneMagazina.Text != "")
                    idMagazina = int.Parse(btneMagazina.Value.ToString());
                if (!string.IsNullOrEmpty(btneKodifikimi1.Text))
                    idKodifikimi1 = clsKodifikimArtikulli.ktheIdKodifikimi(btneKodifikimi1.Text, IdNdermarrja, 1, false);
                if (!string.IsNullOrEmpty(btneKodifikimi2.Text))
                    idKodifikimi2 = clsKodifikimArtikulli.ktheIdKodifikimi(btneKodifikimi1.Text, IdNdermarrja, 2, false);
                if (!string.IsNullOrEmpty(btneKodifikimi3.Text))
                    idKodifikimi3 = clsKodifikimArtikulli.ktheIdKodifikimi(btneKodifikimi1.Text, IdNdermarrja, 3, false);
                MbushPopUpListeArtikujshNgaDb(IdPerdoruesi, IdNdermarrjePerListe, cbKosto.Checked || cbGjendje.Checked, idMagazina, cbCmime.Checked, cbCmimeMeTvsh.Checked, (bool)hfState["artikujTeShitshem"], false, dtDataGjendje.Value.ToString(), idKodifikimi1, idKodifikimi2, idKodifikimi3, false);
            }
            else
            {
                gvLupaArtikull.DataSource = tmpObject;
                gvLupaArtikull.DataBind();
                if (cbRuajFilter.Checked)
                    gvLupaArtikull.RestoreFilter(IdNdermarrja);
            }
        }

        /// <summary>
        /// Mbush griden e popupit me te dhena
        /// </summary>
        /// <param name="idPerdoruesi"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="kostoSasi"></param>
        /// <param name="idMag"></param>
        /// <param name="cmime"></param>
        /// <param name="cmimeMeTvsh"></param>
        /// <param name="iShitshem"></param>
        /// <param name="filtro"></param>
        /// <param name="dategjendjederi"></param>
        /// <param name="idKodifikimi1"></param>
        /// <param name="idKodifikimi2"></param>
        /// <param name="idKodifikimi3"></param>
        /// <param name="dhurataVfOne"></param>
        private void MbushPopUpListeArtikujshNgaDb(int idPerdoruesi, int idNdermarrje, bool kostoSasi, int idMag, bool cmime, bool cmimeMeTvsh, bool iShitshem, bool filtro, string dategjendjederi, int idKodifikimi1, int idKodifikimi2, int idKodifikimi3, bool dhurataVfOne)
        {
            DataTable dt;
            var op = CriteriaOperator.Parse(gvLupaArtikull.FilterExpression, 0);
            var postStringTvsh = rm.GetString("postStringTvsh", ci);
            hfState.Set(clsArtikulli.postStringTvsh, postStringTvsh);
            var filterString = CriteriaToWhereClauseHelper.GetMsSqlWhere(op);
            if (Request.QueryString["klasa"] != null)
            {
                //TODO Arsen - te kalohen ne nje
                switch (Convert.ToInt32(Request.QueryString["klasa"]))
                {
                    case 4:
                        dt = colArtikujt.merrSipasArtikujNdermarrjesAndAutorizimePerLupeArtikulliJoPerbProdhim(idNdermarrje, idPerdoruesi, kostoSasi, idMag, cmime, cmimeMeTvsh, postStringTvsh, iShitshem, filtro, filterString, idKodifikimi1, idKodifikimi2, idKodifikimi3);
                        break;
                    case 44:
                        dt = colArtikujt.merrSipasArtikujNdermarrjesAndAutorizimePerLupeArtikulliPerbere(idNdermarrje, idPerdoruesi, kostoSasi, idMag, cmime, cmimeMeTvsh, postStringTvsh, iShitshem, filtro, filterString, idKodifikimi1, idKodifikimi2, idKodifikimi3);
                        break;
                    case 5:
                    case 6:
                        dt = colArtikujt.merrSipasArtikujNdermarrjesAndAutorizimePerLupeArtikulliPerProdhim(idNdermarrje, idPerdoruesi, kostoSasi, idMag, cmime, cmimeMeTvsh, postStringTvsh, iShitshem, filtro, filterString, idKodifikimi1, idKodifikimi2, idKodifikimi3);
                        break;
                    case 0:
                        dt = colArtikujt.merrSipasArtikujNdermarrjesAndAutorizimePerLupeArtikulliProdhimi(idNdermarrje, idPerdoruesi, kostoSasi, idMag, cmime, cmimeMeTvsh, postStringTvsh, iShitshem, filtro, filterString, idKodifikimi1, idKodifikimi2, idKodifikimi3);
                        break;
                    case 55:
                        dt = colArtikujt.merrSipasArtikujNdermarrjesAndAutorizimePerLupeArtikulliProdhimiPlanifikimi(idNdermarrje, idPerdoruesi, kostoSasi, idMag, cmime, cmimeMeTvsh, postStringTvsh, iShitshem, filtro, filterString, Convert.ToInt32(Request.QueryString["idplanifikimi"]), idKodifikimi1, idKodifikimi2, idKodifikimi3);
                        break;
                    default:
                        dt = colArtikujt.merrSipasArtikujNdermarrjesAndAutorizimePerLupeArtikulli(idNdermarrje, idPerdoruesi, kostoSasi, idMag, cmime, cmimeMeTvsh, postStringTvsh, iShitshem, filtro, filterString, dategjendjederi, idKodifikimi1, idKodifikimi2, idKodifikimi3, dhurataVfOne);
                        break;
                }
            }
            else
            {
                if (Request.QueryString["vjenNgaKartelaArt"] != null && Request.QueryString["vjenNgaKartelaArt"] == "true")
                    dt = colArtikujt.merrSipasArtikujNdermarrjesAndAutorizimePerLupeArtikulli(mySessionObjects.ktheNdermRaportuese(Session), idPerdoruesi, kostoSasi, idMag, cmime, cmimeMeTvsh, postStringTvsh, iShitshem, filtro, filterString, dategjendjederi, idKodifikimi1, idKodifikimi2, idKodifikimi3, dhurataVfOne);
                else
                    dt = colArtikujt.merrSipasArtikujNdermarrjesAndAutorizimePerLupeArtikulli(idNdermarrje, idPerdoruesi, kostoSasi, idMag, cmime, cmimeMeTvsh, postStringTvsh, iShitshem, filtro, filterString, dategjendjederi, idKodifikimi1, idKodifikimi2, idKodifikimi3, dhurataVfOne);
            }
            mySessionObjects.ruajGrideNeSessionLupaArtikull(Session, dt);
            gvLupaArtikull.DataSource = dt;
            gvLupaArtikull.DataBind();
            if (cbRuajFilter.Checked)
                gvLupaArtikull.RestoreFilter(idNdermarrje);
            if (Request.QueryString["filterKlasa"] != null)
                gvLupaArtikull.FilterExpression += (gvLupaArtikull.FilterExpression != string.Empty ? " AND " : string.Empty) + "[Klasa] = " + Request.QueryString["filterKlasa"];
            dt.Dispose();
        }

        private void KonfiguroPopupGride(int idNdermarrje, DataTable kodeNivelCmimi, bool cmime, bool cmimeMeTvsh, string postStringTvsh)
        {
            if (Request.QueryString["vjenNgaKartelaArt"] != null && Request.QueryString["vjenNgaKartelaArt"] == "true")
                idNdermarrje = mySessionObjects.ktheNdermRaportuese(Session);
            KonfigurimComboGride.shtoLlojiArtikullitLup(gvLupaArtikull, rm, ci);
            KonfigurimComboGride.shto_KlasaArtikulli(gvLupaArtikull, Session, Komponente, _guidString, "Klasa");
            KonfigurimComboGride.shto_KodifikimArtikulli(gvLupaArtikull, idNdermarrje, false, Session, Komponente, _guidString);
            KonfigurimComboGride.ShtoAutorizim(gvLupaArtikull, Session, Komponente, _guidString, "Autorizimet");
            KonfigurimComboGride.shto_NjesiArtikulli(gvLupaArtikull, idNdermarrje, Session, Komponente, _guidString, "Njesi1Artikulli");
            KonfigurimComboGride.shto_SkemeKontabilitetiArtikulli(gvLupaArtikull, idNdermarrje, Session, Komponente, _guidString, "IdSkemaKontabilitetiArtikulli");
            if (Request.QueryString["prodhimporosi"] != null && Request.QueryString["prodhimporosi"] == "po")
            {
                var filterExp = gvLupaArtikull.FilterExpression;
                if (string.IsNullOrEmpty(filterExp))
                    filterExp += "[ProdhimMePorosi] = True";
                if (!string.IsNullOrEmpty(filterExp) && !filterExp.Contains("[ProdhimMePorosi] = True"))
                {
                    filterExp += " And ";
                    filterExp += "[ProdhimMePorosi] = True";
                }
                gvLupaArtikull.FilterExpression = filterExp;
            }
            if (kodeNivelCmimi == null)
                kodeNivelCmimi = MerrKodeNivelCmimiNgaSesioni(idNdermarrje, IdPerdoruesi);
            foreach (DataRow dr in kodeNivelCmimi.Rows)
            {
                gvLupaArtikull.Columns[dr.ItemArray[0].ToString()].Visible = cmime;
                gvLupaArtikull.Columns[dr.ItemArray[0] + postStringTvsh].Visible = cmimeMeTvsh;
            }
            kodeNivelCmimi.Dispose();
        }

        /// <summary>
        /// Perdoret per ti vene disa atribute grides, behet per te afishuar rreshtin qe do sherbej per filtrim
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvLupaArtikull_DataBound(object sender, EventArgs e)
        {
            if (gvLupaArtikull.Columns["#"] != null) return;
            var check = new GridViewCommandColumn("#") { ShowSelectCheckbox = true, Width = Unit.Percentage(2) };
            gvLupaArtikull.Settings.ShowFilterRow = true;
            gvLupaArtikull.Settings.ShowHeaderFilterButton = true;
            gvLupaArtikull.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
            gvLupaArtikull.Settings.ShowFilterRowMenu = true;
            gvLupaArtikull.Columns.Add(check);
            gvLupaArtikull.Settings.ShowGroupPanel = true;
            gvLupaArtikull.KeyFieldName = "IdArtikulli";
            gvLupaArtikull.SettingsBehavior.AllowSelectByRowClick = true;
            gvLupaArtikull.SettingsBehavior.AllowFocusedRow = true;
        }

        /// <summary>
        /// Kap item qe ka template ne menune e kesaj faqeje
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
            var idKonfig = (int)hfState["idKonfigambjenti"];
            clsMenuInfo.ShtoMenuItemInfo(this, MenuInfo);
            var itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            var cmbFiltra = ((MenuFilter)itemButton.Template).FindControl("btnFiltra") as ASPxComboBox;
            var filtri = new clsFiltraGrida
            {
                FiltraKodi = cmbFiltra.Text,
                FiltraShenime = cmbFiltra.Text,
                FiltraUniversal = false
            };
            var koka = new clsGridaKoka(IdGjuha, "gvLupaArtikull", Komponente, IdNdermarrja, idKonfig);
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gvLupaArtikull.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("IdArtikulli", gvLupaArtikull);
            //var kolona = gvLupaArtikull.GetSortedColumns();
            //if (kolona.Count > 0)
            //{
            //    filtri.KoloneRenditje = kolona[0].FieldName;
            //    filtri.DrejtimRenditje = kolona[0].SortOrder == ColumnSortOrder.Ascending;
            //}
            //else
            //{
            //    filtri.KoloneRenditje = "IdArtikulli";
            //    filtri.DrejtimRenditje = true;
            //}
            filtri.IdPerdoruesi = IdPerdoruesi;
            filtri.IdNdermarje = IdNdermarrja;
            filtri.IdStatusDok = 1;
            var mesazh = new clsMesazh();
            mesazh = filtri.ruaj();
            PercaktoTemplateMenu();
            clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, IdNdermarrja, "gvLupaArtikull", idKonfig, Komponente);
            if (mesazh.Status)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            cmbFiltra.Text = "";
            gvLupaArtikull.FilterExpression = string.Empty;
            GridUtil.AplikoFilterDefault(gvLupaArtikull, (int)hfState.Get("idKonfigambjenti"));
        }

        /// <summary>
        /// Kap item qe ka template ne menune e kesaj faqeje
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
            var idGjuha = (int)hfState["idGjuha"];
            clsMenuInfo.ShtoMenuItemInfo(this, MenuInfo);
            var itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            var cmbFiltra = ((MenuFilter)itemButton.Template).FindControl("btnFiltra") as ASPxComboBox;
            var idNdermarrje = (int)hfState["idNdermarrje"];
            var filtra = new clsFiltraGrida();
            var koka = new clsGridaKoka(idGjuha, "gvLupaArtikull", Komponente, idNdermarrje, (int)hfState["idKonfigambjenti"]);
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            if (filtra.FiltraKodi == null) return;
            filtra.IdPerdoruesi = IdPerdoruesi;
            var mesazh = new clsMesazh();
            mesazh = filtra.fshi();
            PercaktoTemplateMenu();
            clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idNdermarrje, "gvLupaArtikull", (int)hfState["idKonfigambjenti"], Komponente);
            if (mesazh.Status)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            cmbFiltra.Text = "";
            gvLupaArtikull.FilterExpression = string.Empty;
            GridUtil.AplikoFilterDefault(gvLupaArtikull, (int)hfState.Get("idKonfigambjenti"));
        }

        protected void gvLupaArtikull_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            var idNdermarrje = (int)hfState["idNdermarrje"];
            var arr = e.Parameters.Split(';');
            if (arr[0] == "610") //610 eshte id e komponentes
            {
                var idkomponente = "";
                var kodkonfigurimi = "";
                if (arr.Length == 3)
                {
                    kodkonfigurimi = arr[1];
                    idkomponente = arr[0];
                    if (arr[2] == "")
                        gvLupaArtikull.FilterExpression = "";
                    else
                    {
                        var filtra = new clsFiltraGrida();
                        var koka = new clsGridaKoka(mySessionObjects.ktheGjuhe(Session), "gvLupaArtikull", "LupaArtikull.aspx", idNdermarrje, (int)hfState["idKonfigambjenti"]);
                        filtra.mbushFilterPerGrideSipasKodit(arr[2], idNdermarrje, koka.IdGridaKoka);
                        if (filtra.FiltraKodi != null)
                        {
                            gvLupaArtikull.FilterExpression = filtra.FiltraVlera;
                            GridUtil.renditGriden(filtra.KoloneRenditje, gvLupaArtikull);
                        }
                    }
                }
                else if (arr.Length == 2)//nese eshte zgjedhur nje konfigurim tek combo e konfigurimeve
                {
                    kodkonfigurimi = arr[1];
                    idkomponente = arr[0];
                }
                else
                    idkomponente = e.Parameters;
                gvLupaArtikull.Selection.UnselectAll();
            }
        }

        protected void gvLupaArtikull_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            if (e.CallbackName == "APPLYMULTICOLUMNFILTER" || e.CallbackName == "APPLYFILTER" || (e.Args.Length > 0 && e.Args[0] == "APPLYCOLUMNFILTER"))
            {
                var filtripara = mySessionObjects.merrFilterNgaSessioni(Session);
                if (!string.IsNullOrEmpty(filtripara) && gvLupaArtikull.FilterExpression.Contains(filtripara) || cbRuajFilter.Checked)
                    gvLupaArtikull.SaveFilter((int)hfState["idNdermarrje"]);
                var dtgjendje = dtDataGjendje.Value?.ToString() ?? DateTime.Now.ToShortDateString();
                var postStringTvsh = (string)hfState.Get(clsArtikulli.postStringTvsh);
                int idMagazina = -1, idKodifikimi1 = -1, idKodifikimi2 = -1, idKodifikimi3 = -1;
                if (btneMagazina.Text != "")
                    idMagazina = int.Parse(btneMagazina.Value.ToString());
                if (!string.IsNullOrEmpty(btneKodifikimi1.Text))
                    idKodifikimi1 = clsKodifikimArtikulli.ktheIdKodifikimi(btneKodifikimi1.Text, IdNdermarrja, 1, false);
                if (!string.IsNullOrEmpty(btneKodifikimi2.Text))
                    idKodifikimi2 = clsKodifikimArtikulli.ktheIdKodifikimi(btneKodifikimi1.Text, IdNdermarrja, 2, false);
                if (!string.IsNullOrEmpty(btneKodifikimi3.Text))
                    idKodifikimi3 = clsKodifikimArtikulli.ktheIdKodifikimi(btneKodifikimi1.Text, IdNdermarrja, 3, false);
                var idKonfigambjenti = (int)hfState["idKonfigambjenti"];
                //var op = CriteriaOperator.Parse(gvLupaArtikull.FilterExpression, 0);
                //var filterString = CriteriaToWhereClauseHelper.GetMsSqlWhere(op);
                //if (filterString == "" && idMagazina == -1 && idKodifikimi1 == -1 && idKodifikimi2 == -1 && idKodifikimi3 == -1)
                //    MbushPopUpListeArtikujshNgaDb(IdPerdoruesi, IdNdermarrja, cbKosto.Checked || cbGjendje.Checked, idMagazina, cbCmime.Checked, cbCmimeMeTvsh.Checked, (bool)hfState["artikujTeShitshem"], false, dtgjendje, idKodifikimi1, idKodifikimi2, idKodifikimi3, false);
                //else
                //    MbushPopUpListeArtikujshNgaDb(IdPerdoruesi, IdNdermarrja, cbKosto.Checked || cbGjendje.Checked, idMagazina, cbCmime.Checked, cbCmimeMeTvsh.Checked, (bool)hfState["artikujTeShitshem"], true, dtgjendje, idKodifikimi1, idKodifikimi2, idKodifikimi3, false);
                MbushPopUpListeArtikujshNgaDb(IdPerdoruesi, IdNdermarrjePerListe, cbKosto.Checked || cbGjendje.Checked, idMagazina, cbCmime.Checked, cbCmimeMeTvsh.Checked, (bool)hfState["artikujTeShitshem"], false, dtgjendje, idKodifikimi1, idKodifikimi2, idKodifikimi3, false);
                KonfiguroPopupGride(IdNdermarrjePerListe, null, cbCmime.Checked, cbCmimeMeTvsh.Checked, postStringTvsh);
                GridUtil.percaktoVisibleColumnsSipasKonfigurimit(gvLupaArtikull, "gvLupaArtikull", Komponente, idKonfigambjenti, true, (int)hfState["idGjuha"]);
                gvLupaArtikull.Columns["Gjendje"].Visible = cbGjendje.Checked;
                gvLupaArtikull.Columns["Kosto"].Visible = cbKosto.Checked;
            }
            if (cbRuajFilter.Checked)
            {
                gvLupaArtikull.SaveFilter((int)hfState["idNdermarrje"]);
                return;
            }
            if (string.IsNullOrEmpty(Request.QueryString["vjenNgaRaporti"]))
                gvLupaArtikull.Selection.UnselectAll();
        }

        protected void gvLupaArtikull_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if ((e.Column.FieldName == "Klasa"
                || e.Column.FieldName == "IdSkemaKontabilitetiArtikulli"
                || e.Column.FieldName == "Kodifikimi1Artikulli"
                || e.Column.FieldName == "Njesi1Artikulli"
                || e.Column.FieldName == "Njesi2Artikulli"
                || e.Column.FieldName == "AplikimDhurate")
                && Converter.ConvertToInt(e.Value) == 0)
                e.Criteria = null;
        }

        protected void ASPxMenu1_DataBound(object sender, EventArgs e) => PercaktoTemplateMenu();

        protected void gvLupaArtikull_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName == "PershkrimArtikulli")
                e.Editor.ClientInstanceName = e.Column.FieldName;
        }

        protected void gvLupaArtikull_ProcessOnClickRowFilter(object sender, ASPxGridViewOnClickRowFilterEventArgs e)
        {
        }

        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {
        }

        private bool MerrVlereRuajFliter(int idKonfigambjenti) => 
            mySessionObjects.ktheRuajFilter(Session.SessionID) ?? Converter.MerrVlereOseDefault<bool>(clsAtributeTrupi.merrVleredefaultSipasKontrollitDheKonfigurimit(idKonfigambjenti, "cbRuajFilter", 610));
        
        protected void gvLupaArtikull_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e) =>
            e.Properties["cpFilterExpression"] = gvLupaArtikull.FilterExpression;
    }
}