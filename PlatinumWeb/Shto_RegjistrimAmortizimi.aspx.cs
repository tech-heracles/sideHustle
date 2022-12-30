using System;
using System.Data;
using System.Linq;
using System.Web.UI.HtmlControls;
using DevExpress.Web;
using System.Collections.Generic;
using PlatinumWeb.Templates;
using DbCore;
using DbCore.DbKontabiliteti;
using System.Resources;
using System.Globalization;
using System.Web.UI;
using DbCore.DbShare;
using DbCore.IMBUtils.Extensions;
using DbCore.IMBUtils.Types;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.Messages;
using DbCore.IMBUtils.DataBase;

namespace PlatinumWeb
{
    public partial class Shto_RegjistrimAmortizimi : MyPageBase
    {

        private string komponente = "Shto_RegjistrimAmortizimi.aspx";
        private string guidString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                DbCore.clsFunksione.logout(Session, true, "FaqePaautorizuar");
            }
            var idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);

            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerdoruesi);
                return;
            }
            var idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            var idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            percaktoTemplateMenu(idPerdoruesi, idViti, idNdermarrje, ASPxMenu1);
           
            if (!Page.IsPostBack)
            {
                guidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                hfState.Set("guidString", guidString);
                vendosHfMePerkthime(rm, ci);
                var idgjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
                hfState.Set("idGjuha", idgjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("idPerdoruesi", idPerdoruesi);
                var idNderViti = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
                if (String.IsNullOrEmpty(Request.QueryString["shtim_modifikim"]))
                {
                    hfShtimModifikim.Value = "shtim";
                    konfiguroVleraFillestareShto(idgjuha, idNderViti, idPerdoruesi, idNdermarrje);
                }
                else
                {
                    if (Request.QueryString["shtim_modifikim"] == "shtim")
                    {
                        hfShtimModifikim.Value = "shtim";
                        konfiguroVleraFillestareShto(idgjuha, idNderViti, idPerdoruesi, idNdermarrje);
                    }
                    else
                    {
                        if (Request.QueryString["shtim_modifikim"] == "modifikim")
                        {
                            hfShtimModifikim.Value = "modifikim";
                            konfiguroVleraFillestareModifiko(idgjuha, idNderViti, idNdermarrje, idPerdoruesi);
                        }
                    }
                }
                percaktoTemplateMenu(idPerdoruesi, idViti, idNdermarrje, ASPxMenu1);
                hfMonedhaNder.Value = DbCore.DbAdmin.clsMonedha.ktheMonedhenENdermarrjes(idNdermarrje);
                var tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, komponente);
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                hfTeDrejta.Add("ShtimDraft", tedrejtaInfo.DShtimDraft);
                hfTeDrejta.Add("ModifikimDraft", tedrejtaInfo.DModifikimDraft);
                GridUtil.perktheButonaGride(hfState, mySessionObjects.ktheCultureInfo(Session));
            }
            else
            {
                guidString = (string)hfState["guidString"];
                mbushGridNgaSession(idNdermarrje, idPerdoruesi);
                inicializoGridFaturatSession();
                konfiguroGrideFaturat(idPerdoruesi, true);
            }
            btnLlogarit.Text =MessagesResource.Messages["btnLlogaritAmortizim"];
            ASPxNavBar1.Groups[0].Text =MessagesResource.Messages["headerAnalitike"];
            AspxWebControlUtils.perkthePopUp(popFshi,MessagesResource.Messages["labelKujdes"], lblMsgbox, MessagesResource.Messages["labelAdministrimiMsgJeniSigurt"], ButtonCancel, MessagesResource.Messages["labelAnullo"]);           
            konfiguroGrideArtikuj();
        }

        private void vendosHfMePerkthime(ResourceManager rm, CultureInfo ci)
        {
            hfState.Set("MenuKokeDokumenti", MessagesResource.Messages["MenuKokeDokumenti"]);
            hfState.Set("MenuTrupDokumenti", MessagesResource.Messages["MenuTrupDokumenti"]);
            hfState.Set("MenuFundDokumenti", MessagesResource.Messages["MenuFundDokumenti"]);
            hfState.Set("msgAmortizimiGabimGjatELlogaritjes",MessagesResource.Messages["msgAmortizimiGabimGjatELlogaritjes"]);
            hfState.Set("msgAmortizimiLlogaritjaUNdaluaTek",MessagesResource.Messages["msgAmortizimiLlogaritjaUNdaluaTek"]);
            hfState.Set("msgAmortizimiLlogaritjaPerfundoiMeSukses",MessagesResource.Messages["msgAmortizimiLlogaritjaPerfundoiMeSukses"]);
            hfState.Set("msgZgjidhMagazinen",MessagesResource.Messages["msgZgjidhMagazinen"]);
            hfState.Set("msgPlotesoniTeGjithaFushat",MessagesResource.Messages["msgPlotesoniTeGjithaFushat"]);
            hfState.Set("msgGabimGjateTransferimitTeTeDhenave",MessagesResource.Messages["msgGabimGjateTransferimitTeTeDhenave"]);
            hfState.Set("msgAmortizimiZgjidhniTePakten1Aset",MessagesResource.Messages["msgAmortizimiZgjidhniTePakten1Aset"]);
            hfState.Set("msgZgjidhniNjeDateRegjstrimi",MessagesResource.Messages["msgZgjidhniNjeDateRegjstrimi"]);
            hfState.Set("msgZgjidhniNjeDateDokumenti",MessagesResource.Messages["msgZgjidhniNjeDateDokumenti"]);
            hfState.Set("msgVendosniNumrinEDokumentit",MessagesResource.Messages["msgVendosniNumrinEDokumentit"]);
            hfState.Set("msgZgjidhDokumentin",MessagesResource.Messages["msgZgjidhDokumentin"]);
            hfState.Set("msgShperndarjeNeQendratEKostos",MessagesResource.Messages["msgShperndarjeNeQendratEKostos"]);
            hfState.Set("msgDeshironiShperndarjeQendraKosto",MessagesResource.Messages["msgDeshironiTeBeniShperndarjenNeQendratEKostos"]);
            hfState.Set("msgZgjidhniArtikuj",MessagesResource.Messages["msgZgjidhniArtikuj"]);
            hfState.Set("msgGridaEFaturaveEshteBosh",MessagesResource.Messages["msgGridaEFaturaveEshteBosh"]);
            popupUniversal.HeaderText =MessagesResource.Messages["headerPopUpText"];
            konfigurimi_Label.Text =MessagesResource.Messages["lblModeli"];
            btnZgjidhGjitha.ToolTip =MessagesResource.Messages["btnZgjidhGjitha"];
            gridaSelectTeGjitha.ToolTip =MessagesResource.Messages["btnZgjidhTeGjithe"];
            btnHiqZgjedhjen.ToolTip =MessagesResource.Messages["hiqZgjedhjenBtn"];
        }

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        private void percaktoTemplateMenu(int idPerdoruesi, int idViti, int idNdermarrje, ASPxMenu aSPxMenu1)
        {
            var menu = new DbCore.DbShare.colMenuItem(DbCore.mySessionObjects.ktheGjuhe(Session));
            menu.merrMenuItemSipasKomponentesRegjistrime(DbCore.mySessionObjects.ktheGjuhe(Session), komponente, idPerdoruesi, idNdermarrje, idViti, hfShtimModifikim.Value == "modifikim" ? false : true);

            var kokmag = new DbCore.DbAsete.clsAmortizimiKoka();
            if ((hfShtimModifikim.Value == "modifikim"))
            {
                var id = int.Parse(Request.QueryString["id"]);
                kokmag.ktheAmortizimKokaSipasId(id);
            }
            var kok = new clsKokaFleteKontabel();
            foreach (DbCore.DbShare.clsMenuItem m in menu)
            {
                if (m.Name != "ItemFilter" && m.Name != "ItemFrame")
                {
                    clsToolbarConfig.ShtoMenuItem(this.Theme, aSPxMenu1, m);
                }

                if ((hfLidhur.Value == "True") && m.Name == "Draft")
                {
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                }
                if ((hfShtimModifikim.Value != "modifikim") && m.Name == "PrintPreview")
                {
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                }
                if ((hfShtimModifikim.Value != "modifikim") && m.Name == "Fshi")
                {
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                }
                if (m.Name == "FletaKontabel")
                {
                    if ((hfShtimModifikim.Value != "modifikim"))
                    {
                        aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                    }
                    else
                    {
                        kok = new clsKokaFleteKontabel(int.Parse(Request.QueryString["id"]), 86);

                        if (kok.NrDukumentiKokaFleteKontabel != null)
                        {
                            aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].NavigateUrl = "javascript: myFaqeCelje.kontrolloTeDrejta('Shto_FleteKontabel.aspx?shtim_modifikim=modifikim&id=" + kok.IdKokaFleteKontabel + "&numur=" + kok.NrDukumentiKokaFleteKontabel + "')";
                        }
                        else
                        {
                            aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                        }
                    }
                }
                if (m.Name == "QendraKosto")
                {
                    if ((hfShtimModifikim.Value != "modifikim"))
                    {
                        aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                    }
                    else
                    {
                        if (kok.NrDukumentiKokaFleteKontabel != null)
                        {
                            var qend = new DbCore.DbQendraKosto.clsKokaQendraKosto();
                            qend.KtheKokaQKSipasIDGjeneruesDheKonfig(kok.IdKokaFleteKontabel, kok.IdKonfigAmbjente);


                            if (qend.NrDok != null)
                            {
                                aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].NavigateUrl = "javascript: myButtonClickLupa.LupaUniversal_Click('Shperndarje ne qendrat e kostos','LupaRegjistrimQendraKosto.aspx?shtim_modifikim=modifikim&idDokGjenerues=" + kok.IdKokaFleteKontabel + "&idkonfig=" + kok.IdKonfigAmbjente + "',900,600)";
                            }
                            else
                            {
                                aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                            }
                        }
                        else
                        {
                            aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                        }
                    }
                }
                if (m.Name == "Ruaj")
                {
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = true;
                }

                if (m.Name == "ItemFrame")
                {
                    clsToolbarConfig.ShtoMenuItemPerFrame(this, aSPxMenu1, DbCore.clsFunksione.ktheUrlHelpi(m.UrlHelp).Item1);
                }
                if (m.Name == "Shto" || m.Name == "Kerko" || m.Name == "Pastro" || m.Name == "ItemFilter" || m.Name == "ItemFrame")
                {
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].BeginGroup = true;
                }
            }
            var visible = clsFunksione.merrMenuVisibleDraft(hfShtimModifikim.Value == "modifikim" ? false : true, kokmag.IdStatusDokumenti);
            ASPxMenu1.Items.FindByName("Draft").ClientVisible = visible;
            clsMenuInfo.ShtoMenuItemInfo(this, MenuInfo);
        }

        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.ktheNdermarrjeVit(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), ASPxMenu1);
        }

        private void konfiguroVleraFillestareShto(int idGjuha, int idNderViti, int idPerdoruesi, int idNdermarrje)
        {
            AspxWebControlUtils.vendosDateEditMask(this.dteDtDok);
            AspxWebControlUtils.vendosDateEditMask(dteDtRegjistrimi);
            vendosDataDefault();
            var idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            mbushComboNivelesh(idNdermarrje, cmbLloji);
            cmbLloji.SelectedIndex = 0;
            cmbLloji.ClientSideEvents.Init = "function(s,e){TextChangedLloji();}";
            ConfigureAspxComboBox.mbushComboKonfigurimet(idPerdoruesi, idNdermarrje, this.cmbKonfigurimi, 1003, false, idGjuha);
            cmbKonfigurimi.SelectedIndex = 0;
            var konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()), idGjuha);
            hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmbMagazina);
            ConfigureAspxComboBox.shtoKolonaPerMagazina(cmbMagazina);
            ConfigureAspxComboBox.mbushComboMagazinat(idNdermarrje, cmbMagazina, idPerdoruesi, true, 2, true);
            ConfigureAspxComboBox.mbushComboStandartAmortizimi(cmbStandarti, idNdermarrje);
            cmbStandarti.SelectedIndex = 0;
            var perdoruesi = new DbCore.DbAdmin.clsPerdorues(idPerdoruesi);
            hfPerdoruesAktual.Value = perdoruesi.PerdoruesUsername;
            inicializoGridFaturat();

            var idMonedheZgjedhur = DbCore.clsFunksione.ktheMonedhePerFormatNumri(idGjuha, int.Parse(cmbKonfigurimi.Value.ToString()), mySessionObjects.merrIdNdermarrjeSesioni(Session), 1003, string.Empty, -1, true);
            var formatNrPerKonfig = new DbCore.DbShare.clsFormatiKonfig();
            formatNrPerKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(int.Parse(cmbKonfigurimi.Value.ToString()));
            var formatMonedhe = clsFormatKonfigTrup.ktheFormatNumriMonedhe(formatNrPerKonfig, idMonedheZgjedhur);
            mySessionObjects.ruajFormatVlefta(formatMonedhe.ShifraPasPresjesVlefta, Session);
            konfiguroGrideFaturat(idPerdoruesi, true);
            mbushListeArtikuj(idNdermarrje, idPerdoruesi);
        }
        private void mbushComboNivelesh(int idNdermarrje, ASPxComboBox cmblloji)
        {
            cmblloji.DataSource = DbCore.DbRegjistrim.colNivelRegjistrimi.ktheGjitheNivelRegjistrimiSipasKategoriDtCombo(86, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), false);
            cmblloji.TextField = "Kodi";
            cmblloji.ValueField = "IdNivel";
            cmblloji.DataBind();
            cmblloji.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        }
        private void konfiguroVleraFillestareModifiko(int idGjuha, int idNderViti, int idNdermarrje, int idPerdoruesi)
        {
            AspxWebControlUtils.vendosDateEditMask(dteDtDok);
            AspxWebControlUtils.vendosDateEditMask(dteDtRegjistrimi);
            var idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            mbushComboNivelesh(idNdermarrje, cmbLloji);
            cmbLloji.SelectedIndex = 0;
            ConfigureAspxComboBox.shtoKolonaPerMagazina(cmbMagazina);
            ConfigureAspxComboBox.mbushComboKonfigurimet(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 1003, true, idGjuha);

            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmbMagazina);

            ConfigureAspxComboBox.mbushComboMagazinat(idNdermarrje, cmbMagazina, idPerdoruesi, false, 2, true);
            ConfigureAspxComboBox.mbushComboStandartAmortizimi(cmbStandarti, idNdermarrje);
            var perdoruesi = new DbCore.DbAdmin.clsPerdorues(idPerdoruesi);
            hfPerdoruesAktual.Value = perdoruesi.PerdoruesUsername;
            var id = int.Parse(Request.QueryString["id"]);
            var koka = new DbCore.DbAsete.clsAmortizimiKoka(id);
            mbushListeArtikuj(idNdermarrje, idPerdoruesi);
            MerrTedhenat(idGjuha, idViti, idNdermarrje, idPerdoruesi, koka, rm, ci);
        }

        public void MerrTedhenat(int idGjuha, int idViti, int idNdermarrje, int idPerdoruesi, DbCore.DbAsete.clsAmortizimiKoka l, ResourceManager rm, CultureInfo ci)
        {
            this.txtNrDok.Text = l.NrDok;
            this.dteDtDok.Value = l.DateDokumenti;

            this.txtShenime.Text = l.Shenime;
            this.dteDtRegjistrimi.Value = l.DateRegjistrimi;
            this.cmbLloji.Value = l.IdNiveli.ToString();
            var konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasId(l.IdKonfigurimAmbjenti, idGjuha);
            hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;
            this.cmbKonfigurimi.Value = l.IdKonfigurimAmbjenti.ToString();
            if (l.IdNjesiAdministrative != 0)
            {
                cmbMagazina.Value = l.IdNjesiAdministrative.ToString();
            }
            cmbStandarti.Value = l.IdLlojStandarti.ToString();
            var perdoruesi = new DbCore.DbAdmin.clsPerdorues(l.IdKrijuesi);
            lblKrijuesi.Text = perdoruesi.PerdoruesUsername;
            txtVlefta.Text = l.AmortizimiShteseTotal.ToString();



            if (hfShtimModifikim.Value == "modifikim")
            {
                var dtlidhur = l.merrIdsDokLidhur();
                hfLidhur.Value = (!(dtlidhur.Rows.Count == 0)).ToString();
                AspxWebControlUtils.ShtoLidhje(idPerdoruesi, idViti, idNdermarrje, hl, dtlidhur, l.IdDokGjenerues, l.IdNivelGjenerues, l.IdKonfigGjenerues, idGjuha);
            }
            mbushTrupin(idGjuha, l, idPerdoruesi, rm, ci);
        }


        private void mbushTrupin(int idGjuha, DbCore.DbAsete.clsAmortizimiKoka koka, int idPerdoruesi, ResourceManager rm, CultureInfo ci)
        {
            var idMonedheZgjedhur = DbCore.clsFunksione.ktheMonedhePerFormatNumri(idGjuha, int.Parse(cmbKonfigurimi.Value.ToString()), mySessionObjects.merrIdNdermarrjeSesioni(Session), 1003, string.Empty, -1, true);
            var formatNrPerKonfig = new DbCore.DbShare.clsFormatiKonfig();
            formatNrPerKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(int.Parse(cmbKonfigurimi.Value.ToString()));
            var formatMonedhe = clsFormatKonfigTrup.ktheFormatNumriMonedhe(formatNrPerKonfig, idMonedheZgjedhur);
            mySessionObjects.ruajFormatVlefta(formatMonedhe.ShifraPasPresjesVlefta, Session);

            koka.ColTrupi.merrAmortizimTrupiSipasIdKokaAmortizimi(koka.IdAmortizimi);
            koka.ColTrupiRezerva.merrAmortizimTrupiSipasIdKokaAmortizimi(koka.IdAmortizimi);
            foreach (DbCore.DbAsete.clsAmortizimiTrupiAbstract tr in koka.ColTrupi)
            {
                DbCore.DbInventari.clsArtikulli art = new DbCore.DbInventari.clsArtikulli(tr.IdArtikulli);
                tr.Artikull = art;
            }
            foreach (DbCore.DbAsete.clsAmortizimiTrupiAbstract tr in koka.ColTrupiRezerva)
            {
                DbCore.DbInventari.clsArtikulli art = new DbCore.DbInventari.clsArtikulli(tr.IdArtikulli);
                tr.Artikull = art;
            }
            mySessionObjects.ruajObjectModNeSesion(Session, koka);
            mySessionObjects.ruajObjectNeSesion(Session, new DbCore.DbAsete.clsAmortizimiKoka());
            inicializoGridFaturatSession();
            konfiguroGrideFaturat(idPerdoruesi, true);
        }

        private void vendosDataDefault()
        {
            var sot = new DateTime();
            sot = DateTime.Today;
            var periudha = DbCore.mySessionObjects.merrPeriudheKontabel(Session);
            if (sot >= periudha.FillimiPeriudha && sot <= periudha.MbarimiPeriudha)
            {
                this.dteDtDok.Value = DateTime.Today;
            }
            else
            {
                this.dteDtDok.Value = periudha.FillimiPeriudha;
            }
            dteDtRegjistrimi.Value = DateTime.Today;
        }



        private bool pastroPanelLidhur()
        {
            try
            {
                hl = new HtmlTable();
                pnlLidhur.Update();
                return true;
            }
            catch (Exception err)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
                return false;
            }
        }


        private DbCore.clsMesazh isValid(int idNdermarrje, int draft, ResourceManager rm, CultureInfo ci)
        {
            if (this.dteDtRegjistrimi.Text == string.Empty)
            {
                return new DbCore.clsMesazh(false,MessagesResource.Messages["msgZgjidhniDtRegjistrimi"]);
            }
            if (this.dteDtDok.Text == string.Empty)
            {
                return new DbCore.clsMesazh(false,MessagesResource.Messages["msgLidhjaDokZgjidh1DateDokumenti"]);
            }
            if (this.dteDtDok.Date.Year != new DbCore.DbAdmin.clsNdermarrjeViti(DbCore.mySessionObjects.ktheNdermarrjeVit(Session)).Viti)
            {
                return new DbCore.clsMesazh(false,MessagesResource.Messages["msgDataNukPerketVititUshtrimor"]);
            }

            String mesazhGabimi;
            var periudha = DbCore.mySessionObjects.merrPeriudheKontabel(Session);
            if (!DbCore.clsFunksione.checkPeriudheKontabel(out mesazhGabimi, this.dteDtDok.Date, periudha, draft))
            {
                return new DbCore.clsMesazh(false, mesazhGabimi);
            }
            return new DbCore.clsMesazh(true,MessagesResource.Messages["msgValidimetUKryenMeSukses"]);
        }


        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {
            if (e.Item.Name == "Ruaj")
            {
                Page.Validate();
                ruajRegjistrim(1);
            }
            else
            {
                if (e.Item.Name == "Draft")
                {
                    Page.Validate();
                    ruajRegjistrim(0);
                }
            }
        }

        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            var id = int.Parse(Request.QueryString["id"]);
            var clsKoka = new DbCore.DbAsete.clsAmortizimiKoka(id);
            var mesazhi = new DbCore.clsMesazh();
            var lidhur = clsKoka.eshteILidhur();
            var ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            var rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (clsKoka.DateDokumenti.Year != new DbCore.DbAdmin.clsNdermarrjeViti(DbCore.mySessionObjects.ktheNdermarrjeVit(Session)).Viti)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo,MessagesResource.Messages["msgDataNukPerketVititUshtrimor"], pnlMesazhi);
                return;
            }
            if (lidhur == false)
            {
                var idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                //var periudha = new DbCore.DbAdmin.clsPeriudhaKontabel(clsKoka.DateDokumenti, idNdermarrje);
                //var mesazh = periudha.isPeriudheKycur();
                bool ekycur = DbCore.DbAdmin.clsPeriudhaKontabel.eshteKycurPeriudheSipasDateDheNdermarrjes(clsKoka.DateDokumenti, idNdermarrje);
                if (ekycur)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo,MessagesResource.Messages["msgPeriudhaEKycur"], pnlMesazhi);
                    return;
                }

                if (DbCore.MbylljePeriudhe.PeriodClosing.IsPeriodClosed(clsKoka.DateAmortizimi, MyConnectionsManager.GetSelectedConNameServer(), idNdermarrje, DbCore.DbRegjistrim.KategoriDokumenti.Amortizimi, clsKoka.IdKonfigurimAmbjenti))
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgPeriodIsClosed"], pnlMesazhi);
                    return;
                }

                clsKoka.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                mesazhi = clsKoka.fshiTrans(clsKoka.IdPerdoruesi, 86, false, new DbCore.DbAsete.colAmortizimiTrupi());
            }
            else
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo,MessagesResource.Messages["msgDokLidhurNukFshihet"], pnlMesazhi);
                return;
            }
            if (mesazhi.Status)
            {
                Response.Redirect("RegjistrimAmortizimi.aspx?fshi=po&mesazh=" + mesazhi.PershkrimMesazhi);
                return;
            }
            else
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhi.PershkrimMesazhi, pnlMesazhi);
            }
        }

        /// <summary>
        /// Ruan/Modifikon nje objekt clsKokaEkzekutim.
        /// </summary>
        /// <param name="statusDokumenti">Statusi me te cilin po ruhet dokumenti (I rregullt apo draft)</param>
        private void ruajRegjistrim(int statusdok)
        {
            if (Page.IsValid == false)
            {
                return;
            }
            var shfaqmesazhapolupe = "jo";
            var meKontabilizim = 1;
            var koka = new DbCore.DbAsete.clsAmortizimiKoka();
            var mesazh = new clsMesazh();
            var idndermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            var rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            var ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            mesazh = isValid(idndermarje, statusdok, rm, ci);
            if (mesazh.Status)
            {
                try
                {
                    koka = krijoRegjistrim(statusdok, rm, ci);
                    if (koka.ColTrupi.Count == 0)
                    {
                        hfStatus.Value = "false";
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo,MessagesResource.Messages["msgTrupiDokNukDuhetBosh"], pnlMesazhi, LoadingPanel);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    NLog.LogManager.GetCurrentClassLogger().Error(ex.Message);
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, ex.Message, pnlMesazhi, LoadingPanel);
                    hfStatus.Value = "false";
                    return;
                }
                var idPeriudheZgjedhur = DbCore.mySessionObjects.merrPeriudheKontabel(Session).IdPeriudha;
                var tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idndermarje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), komponente);
                if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim")
                {
                    if ((statusdok == 1 && !tedrejtaInfo.DShtim) || (statusdok == 0 && !tedrejtaInfo.DShtimDraft))
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo,MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                        hfStatus.Value = "false";
                        return;
                    }
                    mesazh = koka.ruajTrans(meKontabilizim, 0, out shfaqmesazhapolupe, 0, new DbCore.DbQendraKosto.colTrupiQendraKosto(), idPeriudheZgjedhur, 86, new DbCore.DbAsete.colSerialetMagazine(), false, hfNrAutoShitje, rm, ci, false);
                }
                else
                {
                    if ((statusdok == 1 && !tedrejtaInfo.DMod) || (statusdok == 0 && !tedrejtaInfo.DModifikimDraft))
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo,MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                        hfStatus.Value = "false";
                        return;
                    }
                    var id = int.Parse(Request.QueryString["id"]);
                    koka.IdAmortizimi = id;
                    var kokavjeter = new DbCore.DbAsete.clsAmortizimiKoka(id);
                    koka.IdKrijuesi = kokavjeter.IdKrijuesi;
                    var lidhur = koka.eshteILidhur();
                    if (lidhur.ToString() != hfLidhur.Value)
                    {
                        mesazh.Status = false;
                        mesazh.PershkrimMesazhi =MessagesResource.Messages["msgDokumentiEshteILidhur"];
                    }
                    else
                    {
                        if (lidhur == true)
                        {
                            mesazh = koka.modifiko(meKontabilizim, true, idPeriudheZgjedhur, 86, out shfaqmesazhapolupe, new DbCore.DbAsete.colSerialetMagazine(), false, new DbCore.DbAsete.colAmortizimiFillestar(), rm, ci);
                        }
                        else
                        {
                            mesazh = koka.modifiko(meKontabilizim, false, idPeriudheZgjedhur, 86, out shfaqmesazhapolupe, new DbCore.DbAsete.colSerialetMagazine(), false, new DbCore.DbAsete.colAmortizimiFillestar(), rm, ci);
                        }
                    }
                }
                if (!mesazh.Status)
                {
                    hfStatus.Value = "false";
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi, LoadingPanel);

                    return;
                }
            }
            else
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi, LoadingPanel);
                hfStatus.Value = "false";
                return;
            }

            hfqkmesazhi.Value = shfaqmesazhapolupe;
            if (shfaqmesazhapolupe != "jo")
            {
                clsKokaFleteKontabel kok = new clsKokaFleteKontabel(koka.IdAmortizimi, 86);

                hfUrl.Value = "LupaRegjistrimQendraKosto.aspx?shtim_modifikim=modifikim&idDokGjenerues=" + kok.IdKokaFleteKontabel + "&idkonfig=" + kok.IdKonfigAmbjente;
            }

            hfStatus.Value = "true";
            clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
        }


        /// <summary>
        /// Krijon nje objekt te tipit clsKokaEkzekutim 
        /// </summary>
        /// <param name="statusDokumenti">Statusi i dokumentit (I rregullt ose draft)</param>
        /// <returns>Kthen nje objekt te tipit DbCore.DbRegjistrim.clsKokaMagazina</returns>
        private DbCore.DbAsete.clsAmortizimiKoka krijoRegjistrim(int statusdok, ResourceManager rm, CultureInfo ci)
        {
            var controls = this.GetAsPxTextEditIdValue();
            controls.AddRange(ASPxSplitter1.GetAsPxTextEditIdValue());

            hfNrAuto = (ASPxHiddenField)DbCore.DbAdmin.NrAuto.VendosVleratNrAuto(hfNrAuto, controls);
            //DbCore.DbAdmin.NrAuto.vendosVleratNrAuto(hfNrAuto, this, null, ASPxSplitter1, null);

            hfNrAutoShitje = (ASPxHiddenField)DbCore.DbAdmin.NrAuto.ShtoNeRegjistrime(hfNrAutoShitje, hfNrAuto, "txtNrDok", "NrDok");
            //DbCore.DbAdmin.NrAuto.shtoNeHfRegjistrime(hfNrAutoShitje, hfNrAuto, "txtNrDok", "NrDok");

            var koka = (DbCore.DbAsete.clsAmortizimiKoka)mySessionObjects.merrObjectModNgaSesioni(Session);
            if (koka.ColTrupi.Count > 0)
            {
                koka.ColTrupi.AddRange(((DbCore.DbAsete.clsAmortizimiKoka)mySessionObjects.merrObjectNgaSesioni(Session)).ColTrupi);
                koka.ColTrupiRezerva.AddRange(((DbCore.DbAsete.clsAmortizimiKoka)mySessionObjects.merrObjectNgaSesioni(Session)).ColTrupiRezerva);
            }
            else
            {
                koka = (DbCore.DbAsete.clsAmortizimiKoka)mySessionObjects.merrObjectNgaSesioni(Session);
            }

            var idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            var idnderviti = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
            var idperdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);

            if (DbCore.MbylljePeriudhe.PeriodClosing.IsPeriodClosed(dteDtDok.Date, MyConnectionsManager.GetSelectedConNameServer(), idNdermarrje, DbCore.DbRegjistrim.KategoriDokumenti.Amortizimi, koka.IdKonfigurimAmbjenti))
                throw new Exception(MessagesResource.Messages["msgPeriodIsClosed"]);


            var idmagazina = 0;
            if (cmbMagazina.Text != string.Empty)
            {
                idmagazina = int.Parse(cmbMagazina.Value.ToString());
            }
            koka.NrDok = txtNrDok.Text;
            koka.DtModifikimi = DateTime.Now;
            koka.DateDokumenti = dteDtDok.Date;
            koka.DateAmortizimi = dteDtDok.Date;
            koka.DateRegjistrimi = dteDtDok.Date;
            koka.Shenime = txtShenime.Text;
            koka.IdPerdoruesi = idperdoruesi;
            koka.IdStatusDokumenti = statusdok;

            if (koka.ColTrupi.Count > 0)
                koka.AmortizimiShteseTotal = koka.ColTrupi.Sum(item => item.AmortizimiShtese);

            if (koka.IdNjesiAdministrative != idmagazina)
            {
                throw new MyException(MessagesResource.Messages["msgAmortizimiMagEZgjedhurENdryshmeNgaMagLlogaritjeve"]);
            }
            if (koka.IdLlojStandarti != int.Parse(cmbStandarti.Value.ToString()))
            {
                throw new MyException(MessagesResource.Messages["msgAmortizimiStandartiZgjedhurINdryshemNgaStandartiLlogaritjeve"]);
            }
            return koka;
        }

        protected void ProgressBar1_RunTask(object sender, EO.Web.ProgressTaskEventArgs e)
        {
            var position = 0;
            e.UpdateProgress(position);
            DbCore.clsMesazh mesazh;

            var magazina = cmbMagazina.Text;
            var fusha = new string[] { "IdArtikulli" };
            var idart = gvAsete.GetSelectedFieldValues(fusha);
            var idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            var idPerdorues = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);

            var clsKonf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            clsKonf.mbushKonfigDefaultKomponentes(1003, idNdermarrje);

            if (this.cmbKonfigurimi.Text != string.Empty)
            {
                clsKonf.mbushKonfigAmbjSipasKod(cmbKonfigurimi.Text, idNdermarrje);
            }
            else
            {
                clsKonf.mbushKonfigAmbjSipasKod("FA", idNdermarrje);
            }
            var idGjuha = (int)hfState["idGjuha"];
            var idMonedheZgjedhur = DbCore.clsFunksione.ktheMonedhePerFormatNumri(idGjuha, clsKonf.IdKonfigAmbjente, mySessionObjects.merrIdNdermarrjeSesioni(Session), 1003, string.Empty, -1, true);
            var formatNrPerKonfig = new DbCore.DbShare.clsFormatiKonfig();
            formatNrPerKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(clsKonf.IdKonfigAmbjente);
            var formatMonedhe = clsFormatKonfigTrup.ktheFormatNumriMonedhe(formatNrPerKonfig, idMonedheZgjedhur);
            mySessionObjects.ruajFormatVlefta(formatMonedhe.ShifraPasPresjesVlefta, Session);
            var idmagazina = 0;
            if (cmbMagazina.Value == null)
            {
                return;
            }
            if (cmbMagazina.Text != string.Empty)
            {
                idmagazina = int.Parse(cmbMagazina.Value.ToString());
            }
            var koka = new DbCore.DbAsete.clsAmortizimiKoka();
            var nrreshti = 0;
            string mesazhmevonshem = "";
            try
            {
                mesazh = koka.krijoDokumentAmortizimiPerLlogaritje(txtNrDok.Text, clsKonf.IdNivel, clsKonf.IdKonfigAmbjente, dteDtDok.Date, dteDtDok.Date, idmagazina, int.Parse(cmbStandarti.Value.ToString()), txtShenime.Text, mySessionObjects.ktheNdermarrjeVit(Session), idNdermarrje, idPerdorues, 1, idPerdorues, out nrreshti, idart, e, out mesazhmevonshem);
                if (mesazh.Status)
                {
                    var grid_faturat = (ASPxGridView)this.ASPxNavBar1.Groups[0].FindControl("grid_faturat");
                    grid_faturat.DataSource = koka.ColTrupi;
                    mySessionObjects.ruajObjectNeSesion(Session, koka);
                    grid_faturat.DataBind();
                    txtVlefta.Text = koka.AmortizimiShteseTotal.ToString();
                    var ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
                    var rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                    konfiguroGrideFaturat(mySessionObjects.ktheIdPerdoruesi(Session), false);
                    e.UpdateProgress(100);
                }
                if (mesazhmevonshem != "")
                    mySessionObjects.ruajMesazhNeSesion(Session, mesazhmevonshem);
            }
            catch (Exception ex)
            {
                mySessionObjects.ruajMesazhNeSesion(Session, ex.Message);
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, ex.Message, pnlMesazhi, LoadingPanel);
            }

        }

        protected void btnXlsxExport_Click(object sender, EventArgs e)
        {
            try
            {
                var rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                var ci = DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo((int)hfState["idGjuha"]);
                var gridExport = (DevExpress.Web.ASPxGridViewExporter)this.ASPxNavBar1.Groups[0].FindControl("gridExport");
                var opt = new DevExpress.XtraPrinting.XlsxExportOptions(DevExpress.XtraPrinting.TextExportMode.Text);
                gridExport.WriteXlsxToResponse(MessagesResource.Messages["msgFaturat"], true, opt);
            }
            catch (Exception)
            {
            }
        }




        /// <summary>
        /// kur grida ben callback
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvAsete_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            gvAsete.DataBind();
        }



        /// <summary>
        /// vendos karakteristika te grides ne javascript
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvAsete_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpNoRows"] = gvAsete.VisibleRowCount;
            e.Properties["cpNoPage"] = gvAsete.PageIndex;
        }

        /// <summary>
        /// kur grida ben databound per te shtuar butonin fshi
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvAsete_DataBound(object sender, EventArgs e)
        {
            if (gvAsete.Columns["#"] == null)
            {
                var check = new GridViewCommandColumn("#") { ShowSelectCheckbox = true, Width = System.Web.UI.WebControls.Unit.Percentage(2) };
                gvAsete.Settings.ShowFilterRow = true;
                gvAsete.Settings.ShowHeaderFilterButton = true;
                gvAsete.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                gvAsete.Settings.ShowFilterRowMenu = true;
                gvAsete.Columns.Add(check);
                gvAsete.Settings.ShowGroupPanel = true;
                gvAsete.KeyFieldName = "IdArtikulli";
                gvAsete.SettingsBehavior.AllowSelectByRowClick = true;
                gvAsete.SettingsBehavior.AllowFocusedRow = true;
            }
        }

        /// <summary>
        /// mbush griden me rreshta bosh
        /// </summary>
        private void mbushListeArtikuj(int idNdermarrje, int idPerdoruesi)
        {
            var dt = DbCore.DbInventari.colArtikujt.ktheArtikujNdermarrjesAndAutorizimeDheLlojDT(idNdermarrje, idPerdoruesi, true, false);
            gvAsete.DataSource = dt;
            mySessionObjects.ruajGrideNeSession(Session, dt);
            gvAsete.DataBind();
            dt.Dispose();
        }
        private void mbushGridNgaSession(int idNdermarrje, int idPerdoruesi)
        {
            DataTable tmpObject;
            var sukses = DbCore.mySessionObjects.merrGrideNgaSessioni(Session, out tmpObject);
            if (!sukses)
            {
                mbushListeArtikuj(idNdermarrje, idPerdoruesi);
            }
            else
            {
                gvAsete.DataSource = tmpObject;
                gvAsete.DataBind();
                tmpObject.Dispose();
            }
        }

        /// <summary>
        /// konfiguron griden
        /// </summary>
        private void konfiguroGrideArtikuj()
        {
            var oKomponente = new DbCore.DbAdmin.clsKomponente(komponente);
            var cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            var rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            GridUtil.percaktoVisibleColumnsMeWidth(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), gvAsete, "gvAsete", komponente);
            GridUtil.konfiguroGrideRegjistrimEvogelPaTheme(gvAsete, "IdArtikulli");
            gvAsete.Settings.UseFixedTableLayout = false;
            gvAsete.SettingsPager.Mode = GridViewPagerMode.ShowPager;
            gvAsete.SettingsPager.PageSize = 10;
            gvAsete.Columns["#"].VisibleIndex = 0;
        }



        private void inicializoGridFaturat()
        {
            var koka = new DbCore.DbAsete.clsAmortizimiKoka();

            var grid_faturat = (ASPxGridView)this.ASPxNavBar1.Groups[0].FindControl("grid_faturat");

            grid_faturat.DataSource = koka.ColTrupi;
            mySessionObjects.ruajObjectNeSesion(Session, koka);
            mySessionObjects.ruajObjectModNeSesion(Session, koka);
            grid_faturat.DataBind();
        }

        private void konfiguroGrideFaturat(int idPerdoruesi, bool visibleindex)
        {
            var idndermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            shtokolona();


            var grid_faturat = (ASPxGridView)this.ASPxNavBar1.Groups[0].FindControl("grid_faturat");
            if (visibleindex)
            {
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idndermarje, "grid_faturat", grid_faturat, cmbKonfigurimi.Text.Split(';')[0], "1003", DbCore.mySessionObjects.ktheGjuhe(Session));
            }
            else
            {
                GridUtil.percaktoVisibleColumnsGridSipasKodKonfigurimiPaVisibleIndex(idndermarje, grid_faturat, cmbKonfigurimi.Text.Split(';')[0], "1003", DbCore.mySessionObjects.ktheGjuhe(Session));
            }
            KonfigurimComboGride.shtoArtikuj(grid_faturat, idndermarje, idPerdoruesi, true, false, Session, komponente, guidString);
            KonfigurimComboGride.ShtoMagazinaSipasSipasLlojit(grid_faturat, idndermarje, idPerdoruesi, 2, Session, komponente, guidString);
            grid_faturat.SettingsPager.PageSize = 15;
            grid_faturat.Columns[MessagesResource.Messages["labelBlerjeShitjeFshi"]].VisibleIndex = 50;
            var col0 = grid_faturat.Columns[MessagesResource.Messages["labelBlerjeShitjeFshi"]] as GridViewDataTextColumn;
            col0.DataItemTemplate = new MyButtonTemplate(string.Empty);
        }



        private void shtoSerial(int idNdermarrje)
        {
            var colnew = new GridViewDataComboBoxColumn();
            var grid_faturat = (ASPxGridView)this.ASPxNavBar1.Groups[0].FindControl("grid_faturat");
            if (typeof(GridViewDataComboBoxColumn) != grid_faturat.Columns["IdAQTSeriali"].GetType())
            {
                grid_faturat.Columns.Remove(grid_faturat.Columns["IdAQTSeriali"]);
                grid_faturat.Columns.Add(colnew);
                var seriale = new DbCore.DbAsete.colAQTSeriale();
                seriale.Add(new DbCore.DbAsete.clsAQTSeriale());
                seriale.ktheAQTSerialSipasIDNdermarje(idNdermarrje);
                colnew.PropertiesComboBox.DataSource = seriale;
                colnew.PropertiesComboBox.TextField = "AqtSerialKod";
                colnew.PropertiesComboBox.ValueField = "IdAQTSerial";
                colnew.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                colnew.FieldName = "IdAQTSeriali";
                DbCore.mySessionObjects.ruajDsComboGrideNeSession(Session, seriale, "seriale");
            }
            else
            {
                colnew = (GridViewDataComboBoxColumn)grid_faturat.Columns["IdAQTSeriali"];
                if (colnew.PropertiesComboBox.Items.Count == 0)
                {
                    colnew.PropertiesComboBox.DataSource = DbCore.mySessionObjects.merrDsComboGrideNeSession(Session, "seriale");
                }
            }
        }

        private void inicializoGridFaturatSession()
        {
            var grid_faturat = (ASPxGridView)this.ASPxNavBar1.Groups[0].FindControl("grid_faturat");
            var trupi = new DbCore.DbAsete.colAmortizimiTrupi();
            trupi.AddRange(((DbCore.DbAsete.clsAmortizimiKoka)DbCore.mySessionObjects.merrObjectModNgaSesioni(Session)).ColTrupi);
            trupi.AddRange(((DbCore.DbAsete.clsAmortizimiKoka)DbCore.mySessionObjects.merrObjectNgaSesioni(Session)).ColTrupi);
            grid_faturat.DataSource = trupi;
            grid_faturat.DataBind();
        }

        private void shtokolona()
        {
            int formatvlefta;
            try
            {
                formatvlefta = mySessionObjects.merrFormatVleftaSesioni(Session);
            }
            catch (MyException ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex.Message);
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, ex.Message, pnlMesazhi);
                return;
            }
            var grid_faturat = (ASPxGridView)this.ASPxNavBar1.Groups[0].FindControl("grid_faturat");
            GridViewDataTextColumn colnew1;
            GridViewDataDateColumn colnew2;
            if (grid_faturat.Columns["IdAmortizimiTrupi"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "IdAmortizimiTrupi";
                colnew1.VisibleIndex = 0;
                grid_faturat.Columns.Add(colnew1);
            }

            if (grid_faturat.Columns["IdAmortizimKoka"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "IdAmortizimKoka";
                colnew1.VisibleIndex = 5;
                grid_faturat.Columns.Add(colnew1);
            }
            if (grid_faturat.Columns["NrRendor"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "NrRendor";
                colnew1.VisibleIndex = 1;
                grid_faturat.Columns.Add(colnew1);
            }
            if (grid_faturat.Columns["IdArtikulli"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.VisibleIndex = 2;
                colnew1.FieldName = "IdArtikulli";
                grid_faturat.Columns.Add(colnew1);
            }
            if (grid_faturat.Columns["DateAmortizimi"] == null)
            {
                colnew2 = new GridViewDataDateColumn();
                colnew2.DataItemTemplate = new MyDateGTemplate(string.Empty);
                colnew2.FieldName = "DateAmortizimi";
                colnew2.VisibleIndex = 3;
                grid_faturat.Columns.Add(colnew2);
            }
            if (grid_faturat.Columns["DateMePareAmortizimi"] == null)
            {
                colnew2 = new GridViewDataDateColumn();
                colnew2.FieldName = "DateMePareAmortizimi";
                colnew2.VisibleIndex = 15;
                grid_faturat.Columns.Add(colnew2);
            }

            if (grid_faturat.Columns["Emertimi"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "Emertimi";
                colnew1.VisibleIndex = 6;
                grid_faturat.Columns.Add(colnew1);
            }
            if (grid_faturat.Columns["IdAQTSeriali"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "IdAQTSeriali";
                grid_faturat.Columns.Add(colnew1);
            }
            if (grid_faturat.Columns["Serial"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.VisibleIndex = 8;
                colnew1.FieldName = "Serial";
                grid_faturat.Columns.Add(colnew1);
            }
            if (grid_faturat.Columns["IdArtikull_LlojAmortizimi"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "IdArtikull_LlojAmortizimi";
                colnew1.VisibleIndex = 9;
                grid_faturat.Columns.Add(colnew1);
            }
            if (grid_faturat.Columns["DateNdryshimStatusMagazine"] == null)
            {
                colnew2 = new GridViewDataDateColumn();
                colnew2.FieldName = "DateNdryshimStatusMagazine";
                colnew2.VisibleIndex = 4;
                grid_faturat.Columns.Add(colnew2);
            }


            if (grid_faturat.Columns["IdNjesiAdministrative"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "IdNjesiAdministrative";
                colnew1.VisibleIndex = 11;
                grid_faturat.Columns.Add(colnew1);
            }
            if (grid_faturat.Columns["VleftaPlusMinus"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.PropertiesEdit.DisplayFormatString = clsFunksione.krijoNumer(formatvlefta, "0");
                colnew1.FieldName = "VleftaPlusMinus";
                colnew1.VisibleIndex = 12;
                grid_faturat.Columns.Add(colnew1);
            }
            else
            {
                ((GridViewDataTextColumn)grid_faturat.Columns["VleftaPlusMinus"]).PropertiesEdit.DisplayFormatString = clsFunksione.krijoNumer(formatvlefta, "0");
            }
            if (grid_faturat.Columns["AmortizimiVjetor"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.PropertiesEdit.DisplayFormatString = clsFunksione.krijoNumer(formatvlefta, "0");
                colnew1.FieldName = "AmortizimiVjetor";
                colnew1.VisibleIndex = 13;
                grid_faturat.Columns.Add(colnew1);
            }
            else
            {
                ((GridViewDataTextColumn)grid_faturat.Columns["AmortizimiVjetor"]).PropertiesEdit.DisplayFormatString = clsFunksione.krijoNumer(formatvlefta, "0");
            }
            if (grid_faturat.Columns["AmortizimAkumuluar"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.PropertiesEdit.DisplayFormatString = clsFunksione.krijoNumer(formatvlefta, "0");
                colnew1.FieldName = "AmortizimAkumuluar";
                colnew1.VisibleIndex = 14;
                grid_faturat.Columns.Add(colnew1);
            }
            else
            {
                ((GridViewDataTextColumn)grid_faturat.Columns["AmortizimAkumuluar"]).PropertiesEdit.DisplayFormatString = clsFunksione.krijoNumer(formatvlefta, "0");
            }
            if (grid_faturat.Columns["NormaAmortizimi"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.PropertiesEdit.DisplayFormatString = clsFunksione.krijoNumer(formatvlefta, "0");
                colnew1.FieldName = "NormaAmortizimi";
                colnew1.VisibleIndex = 16;
                grid_faturat.Columns.Add(colnew1);
            }
            else
            {
                ((GridViewDataTextColumn)grid_faturat.Columns["NormaAmortizimi"]).PropertiesEdit.DisplayFormatString = clsFunksione.krijoNumer(formatvlefta, "0");
            }
            if (grid_faturat.Columns["AmortizimiShtese"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.PropertiesEdit.DisplayFormatString = clsFunksione.krijoNumer(formatvlefta, "0");
                colnew1.FieldName = "AmortizimiShtese";
                colnew1.VisibleIndex = 17;
                grid_faturat.Columns.Add(colnew1);
            }
            else
            {
                ((GridViewDataTextColumn)grid_faturat.Columns["AmortizimiShtese"]).PropertiesEdit.DisplayFormatString = clsFunksione.krijoNumer(formatvlefta, "0");
            }
            if (grid_faturat.Columns["AmortizimiGjithsej"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.PropertiesEdit.DisplayFormatString = clsFunksione.krijoNumer(formatvlefta, "0");
                colnew1.FieldName = "AmortizimiGjithsej";
                colnew1.VisibleIndex = 18;
                grid_faturat.Columns.Add(colnew1);
            }
            else
            {
                ((GridViewDataTextColumn)grid_faturat.Columns["AmortizimiGjithsej"]).PropertiesEdit.DisplayFormatString = clsFunksione.krijoNumer(formatvlefta, "0");
            }
            if (grid_faturat.Columns["HdAmortizimGjithsej"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.PropertiesEdit.DisplayFormatString = clsFunksione.krijoNumer(formatvlefta, "0");
                colnew1.FieldName = "HdAmortizimGjithsej";
                grid_faturat.Columns.Add(colnew1);
            }
            else
            {
                ((GridViewDataTextColumn)grid_faturat.Columns["HdAmortizimGjithsej"]).PropertiesEdit.DisplayFormatString = clsFunksione.krijoNumer(formatvlefta, "0");
            }
            if (grid_faturat.Columns["HdAmortizimVjetor"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.PropertiesEdit.DisplayFormatString = clsFunksione.krijoNumer(formatvlefta, "0");
                colnew1.FieldName = "HdAmortizimVjetor";
                grid_faturat.Columns.Add(colnew1);
            }
            else
            {
                ((GridViewDataTextColumn)grid_faturat.Columns["HdAmortizimVjetor"]).PropertiesEdit.DisplayFormatString = clsFunksione.krijoNumer(formatvlefta, "0");
            }
            if (grid_faturat.Columns["VleftaGjendje"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.PropertiesEdit.DisplayFormatString = clsFunksione.krijoNumer(formatvlefta, "0");
                colnew1.FieldName = "VleftaGjendje";
                grid_faturat.Columns.Add(colnew1);
            }
            else
            {
                ((GridViewDataTextColumn)grid_faturat.Columns["VleftaGjendje"]).PropertiesEdit.DisplayFormatString = clsFunksione.krijoNumer(formatvlefta, "0");
            }
            if (grid_faturat.Columns["DiteAmortizimi"] == null)
            {
                colnew1 = new GridViewDataTextColumn();

                colnew1.FieldName = "DiteAmortizimi";
                grid_faturat.Columns.Add(colnew1);
            }
        }

        /// <summary>
        /// I vendos nje emer identifikues kontrolleve te grides se dok kryesore dhe percakton funksinet qe manipulojne ato ne client side
        /// </summary>
        protected void grid_faturat_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType == GridViewRowType.Data)
            {
                var ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
                var rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                var col0 = ((ASPxGridView)sender).Columns[MessagesResource.Messages["labelBlerjeShitjeFshi"]] as GridViewDataTextColumn;
                var btn0 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col0, "btn") as ASPxButton;

                if (btn0 != null)
                {
                    btn0.ClientInstanceName = "btnFshi" + e.VisibleIndex;
                    btn0.ClientSideEvents.Click = String.Format("function(s,e){{FshiClickedArtPerb({0});}}", e.VisibleIndex);
                    if (hfLidhur.Value == "True")
                    {
                        btn0.ClientEnabled = false;
                    }
                    else
                    {
                        btn0.ClientEnabled = true;
                    }
                }
            }
        }


        protected void grid_faturat_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if (e.Column.FieldName == "IdAQTSeriali" ||
                  e.Column.FieldName == "IdNjesiAdministrative" || e.Column.FieldName == "IdArtikulli")
            {
                if (Converter.ConvertToInt(e.Value) == 0)
                {
                    e.Criteria = null;
                }
            }
        }



        protected void grid_faturat_DataBound(object sender, EventArgs e)
        {
            var grid_faturat = (ASPxGridView)this.ASPxNavBar1.Groups[0].FindControl("grid_faturat");
            var ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            var rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (grid_faturat.Columns[MessagesResource.Messages["labelBlerjeShitjeFshi"]] == null)
            {
                var fshi = new GridViewDataTextColumn() { Caption =MessagesResource.Messages["labelBlerjeShitjeFshi"], Width = 50 };
                grid_faturat.Columns.Add(fshi);
                grid_faturat.Settings.ShowFilterRow = true;
                grid_faturat.Settings.ShowHeaderFilterButton = true;
                grid_faturat.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                grid_faturat.Settings.ShowFilterRowMenu = true;
                grid_faturat.Settings.ShowGroupPanel = false;
                grid_faturat.KeyFieldName = "IdAmortizimiTrupi";
                grid_faturat.SettingsBehavior.AllowSelectByRowClick = true;
                grid_faturat.SettingsBehavior.AllowFocusedRow = true;
                grid_faturat.Settings.ShowTitlePanel = false;
            }
        }

        protected void grid_faturat_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters == "pastro")
            {
                inicializoGridFaturat();
            }
            else
            {
                if (e.Parameters != string.Empty)
                {
                    var key = -1;
                    int.TryParse(e.Parameters, out key);
                    var kokamod = ((DbCore.DbAsete.clsAmortizimiKoka)DbCore.mySessionObjects.merrObjectModNgaSesioni(Session));
                    var koka = ((DbCore.DbAsete.clsAmortizimiKoka)DbCore.mySessionObjects.merrObjectNgaSesioni(Session));
                    if (kokamod.ColTrupi.Count > key)
                    {
                        kokamod.ColTrupi.RemoveAt(key);
                        kokamod.AmortizimiShteseTotal = kokamod.ColTrupi.Sum(item => item.AmortizimiShtese);
                    }
                    else
                    {
                        if (koka.ColTrupi.Count > key - kokamod.ColTrupi.Count)
                        {
                            koka.ColTrupi.RemoveAt(key - kokamod.ColTrupi.Count);
                            koka.AmortizimiShteseTotal = koka.ColTrupi.Sum(item => item.AmortizimiShtese);
                        }
                    }
                    mySessionObjects.ruajObjectNeSesion(Session, koka);
                    mySessionObjects.ruajObjectModNeSesion(Session, kokamod);
                }
            }
        }

        protected void grid_faturat_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            var grid_faturat = (ASPxGridView)this.ASPxNavBar1.Groups[0].FindControl("grid_faturat");
            e.Properties["cpNoRows"] = grid_faturat.VisibleRowCount;
        }

        protected void grid_faturat_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            inicializoGridFaturatSession();
            var ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            var rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            konfiguroGrideFaturat(mySessionObjects.ktheIdPerdoruesi(Session), false);
        }
    }
}
