using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using DbCore;
using DbCore.DbAdmin;
using DbCore.DbInventari;
using DbCore.DbKontabiliteti;
using DbCore.DbQendraKosto;
using DbCore.DbRegjistrim;
using DbCore.DbShare;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Extensions;
using DbCore.IMBUtils.Messages;
using DbCore.IMBUtils.Types;
using DevExpress.Utils;
using DevExpress.Web;
using Newtonsoft.Json;
using NLog;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using PlatinumWeb.Templates;

namespace PlatinumWeb
{
    public partial class Shto_FleteDoganore : MyPageBase
    {

        ASPxComboBox tempcombo;
        ASPxTextBox temptxt;

        clsFleteDoganoreKoka oKoka = new clsFleteDoganoreKoka();

        private const string Komponente = "Shto_FleteDoganore.aspx";
        private string kolFshi;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (hfState.Count == 0)
            {
                if (!mySessionObjects.isLogedIn(Session))
                    clsFunksione.logout(Session, true, "FaqePaautorizuar");

                if (mySessionObjects.ktheKodNdermarrje(Session) == null)
                    Response.Redirect("Login_Ndermarrje.aspx?id=" + IdPerdoruesi);

                if (mySessionObjects.merrPeriudheKontabel(Session) != null)
                {
                    clsPeriudhaKontabel periudha = mySessionObjects.merrPeriudheKontabel(Session);
                    btnPeriudha.Text = periudha.NrPeriudha.ToString();
                    lblPeriudhaAktuale.Text = String.Format("{0}-{1}", periudha.FillimiPeriudha.ToShortDateString(), periudha.MbarimiPeriudha.ToShortDateString());
                }

                PercaktoTemplateMenu();
            }
            kolFshi = MessagesResource.Messages["lblFshiBtn"];
            if (!Page.IsPostBack)
            {
                ShtoVleraTePergjithshmeNeHfState();
                MbushHiddenFieldMePerkthime();
                if (hfShtimModifikim.Value == "")
                {
                    if (Request.QueryString["shtim_modifikim"] == "shtim")
                    {
                        hfShtimModifikim.Value = "shtim";
                        KonfiguroVleraFillestareShto();
                        KonfiguroVleraFillestareTaksat();
                        KonfiguroVleraFillestareTvsh();
                    }
                    else
                    {
                        hfShtimModifikim.Value = "modifikim";
                        KonfiguroVleraFillestare();
                        oKoka = new clsFleteDoganoreKoka(int.Parse(Request.QueryString["id"]));
                        if (oKoka != null)
                        {
                            MerrTedhenat();
                            txtNrDok.Focus();
                        }
                    }
                }
                else if (hfShtimModifikim.Value == "shtim")
                {
                    KonfiguroVleraFillestareShto();
                    KonfiguroVleraFillestareTaksat();
                    KonfiguroVleraFillestareTvsh();
                }
                else if (hfShtimModifikim.Value == "modifikim")
                {
                    KonfiguroVleraFillestare();
                    KonfiguroVleraFillestareTaksat();
                    KonfiguroVleraFillestareTvsh();
                    clsFleteDoganoreKoka oKoka = new clsFleteDoganoreKoka(int.Parse(Request.QueryString["id"]));
                    if (oKoka != null)
                    {
                        MerrTedhenat();
                    }
                }

                clsTeDrejtaRoli tedrejtaInfo = new clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, clsFunksione.GetKomponente(Page.Request));
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                hfTeDrejta.Add("ShtimDraft", tedrejtaInfo.DShtimDraft);
                hfTeDrejta.Add("ModifikimDraft", tedrejtaInfo.DModifikimDraft);
            }

            AspxWebControlUtils.perkthePopUp(popFshi, rm.GetString("labelKujdes", ci), lblMsgbox, rm.GetString("labelAdministrimiMsgJeniSigurt", ci), ButtonCancel, rm.GetString("labelAnullo", ci));

            popupUniversal.HeaderText = rm.GetString("headerPopUpText", ci);

            PercaktoTemplateMenu();

            if (Page.IsPostBack == false)
            {
                KonfiguroGrideTaksat();
                KonfiguroGrideTvsh();
            }
            KonfiguroSubGrideTrupi();
            InicializoGridFaturat();
            KonfiguroGrideFaturat(false);
            GridUtil.perktheButonaGride(hfState, ci);
            ASPxNavBar1.Groups[0].Text = rm.GetString("lblFature", ci);
        }

        public void MerrTedhenat()
        {
            clsKonfigurimAmbjenti konf = new clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasId(oKoka.IdKonfigAmbjente, IdGjuha);
            cmbKonfigurimi.Text = konf.KodKonfigAmbjente;
            hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;

            txtNrDok.Text = oKoka.NrDok;
            txtDtDok.Date = oKoka.DtDok;
            txtDtRegj.Date = oKoka.DtRegjistrimi;

            cmbMonedha.Text = clsMonedha.ktheKodMonedheSipasId(oKoka.IdMonedha);
            txtKursi.Text = oKoka.Kursi.ToString();
            txtVlFatura.Text = oKoka.VlFaturuar.ToString();
            txtVlMb.Text = (oKoka.Kursi * oKoka.VlFaturuar).ToString();
            txtTransporti.Text = oKoka.VlTransport.ToString();
            txtSiguracion.Text = oKoka.VlSiguracion.ToString();
            txtTjera.Text = oKoka.VlTjera.ToString();
            txtVlDoganore.Text = oKoka.VlDoganim.ToString();
            hfIdFaturave.Value = "";
            hfKodi.Value = "";
            hfPershkrimi.Value = "";
            hfVlera.Value = "";
            hfTvsh.Value = "";

            string strFatura = "";
            List<int> strIdFatura = new List<int>();

            colFleteDoganoreTrupi trupat = new colFleteDoganoreTrupi(int.Parse(Request.QueryString["id"]));

            foreach (clsFleteDoganoreTrupi tr in trupat)//ne gride vendosen vetem llogarite qe jane prekur ne kredi (sepse ne debi jane prekur llogarite e artikujve)
            {
                ASPxGridView gridFaturat = (ASPxGridView)ASPxNavBar1.Groups[0].FindControl("grid_faturat");
                gridFaturat.Selection.SelectRowByKey(tr.IdFatura);
                strIdFatura.Add(tr.IdFatura);
                strFatura += tr.NrDok + ",";
            }

            hfIdFaturave.Value = JsonConvert.SerializeObject(strIdFatura);
            strFatura = strFatura.Substring(0, strFatura.Length - 1);

            int id = -1;
            List<string> idfat = JsonConvert.DeserializeObject<List<string>>(hfIdFaturave.Value);

            if (idfat != null)
                id = int.Parse(idfat[0]);

            int idMonedheZgjedhur = clsFunksione.ktheMonedhePerFormatNumri(IdGjuha, int.Parse(cmbKonfigurimi.Value.ToString()), IdNdermarrja, 522, "", id, false);
            clsFormatiKonfig formatNrPerKonfig = new clsFormatiKonfig();
            formatNrPerKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(int.Parse(cmbKonfigurimi.Value.ToString()));
            clsFormatKonfigTrup formatMonedhe = clsFormatKonfigTrup.ktheFormatNumriMonedhe(formatNrPerKonfig, idMonedheZgjedhur);

            hfState.Set("formatMonedhe", JsonConvert.SerializeObject(formatMonedhe));
            mySessionObjects.ruajFormatVlefta(formatMonedhe.ShifraPasPresjesVlefta, Session);

            gvTaksat.DataSource = new colFleteDoganoreTaksa(oKoka.IdFleteDoganoreKoka) { new clsFleteDoganoreTaksa() }; ;
            gvTaksat.DataBind();
            KonfiguroGrideTaksat();
            txtTotalTaksa.Text = ((colFleteDoganoreTaksa)gvTaksat.DataSource).AsQueryable().Sum(x => x.Vlefta).ToString();

            gvTVSH.DataSource = new colFleteDoganoreTVSH(oKoka.IdFleteDoganoreKoka) { new clsFleteDoganoreTVSH() }; ;
            gvTVSH.DataBind();
            KonfiguroGrideTvsh();
            txtTotalTVSH.Text = ((colFleteDoganoreTVSH)gvTVSH.DataSource).AsQueryable().Sum(x => x.VleftaTvsh).ToString();

            using (clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin())
            {
                System.Data.DataTable dtlidhur = dbAdmin.MerrDokLidhur(oKoka.IdFleteDoganoreKoka, oKoka.IdNivel, "T_FLETEDOGANOREKOKA", "IDFLETEDOGANORE");
                hfLidhur.Value = (dtlidhur.Rows.Count != 0).ToString();
                AspxWebControlUtils.ShtoLidhje(IdPerdoruesi, IdViti, IdNdermarrja, hl, dtlidhur, oKoka.IdGjenerues, oKoka.IdNivelGjenerues, oKoka.IdKonfigGjenerues, IdGjuha);
            }

            MbushFushatFaturesModifkim(idfat);
        }

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        private void PercaktoTemplateMenu()
        {
            colMenuItem menu = new colMenuItem(IdGjuha);
            menu.merrMenuItemSipasKomponentesRegjistrime(IdGjuha, clsFunksione.GetKomponente(Page.Request), IdPerdoruesi, IdNdermarrja, IdViti, hfShtimModifikim.Value != "modifikim");
            clsKokaFleteKontabel kok = new clsKokaFleteKontabel();
            foreach (clsMenuItem m in menu)
            {
                if (m.Name != "ItemFilter" && m.Name != "ItemFrame")
                    clsToolbarConfig.ShtoMenuItem(Theme, ASPxMenu1, m);

                if (hfLidhur.Value == "True" && m.Name == "Draft")
                    ASPxMenu1.Items[ASPxMenu1.Items.Count - 1].ClientVisible = false;

                if (hfShtimModifikim.Value != "modifikim" && m.Name == "PrintPreview")
                    ASPxMenu1.Items[ASPxMenu1.Items.Count - 1].ClientVisible = false;

                if (hfShtimModifikim.Value != "modifikim" && m.Name == "Fshi")
                    ASPxMenu1.Items[ASPxMenu1.Items.Count - 1].ClientVisible = false;

                switch (m.Name)
                {
                    case "FletaKontabel":
                        if (hfShtimModifikim.Value != "modifikim")
                            ASPxMenu1.Items[ASPxMenu1.Items.Count - 1].ClientVisible = false;
                        else
                        {
                            kok = new clsKokaFleteKontabel(int.Parse(Request.QueryString["id"]), 8);

                            if (kok.NrDukumentiKokaFleteKontabel != null)
                                ASPxMenu1.Items[ASPxMenu1.Items.Count - 1].NavigateUrl = "javascript: myFaqeCelje.kontrolloTeDrejta('Shto_FleteKontabel.aspx?shtim_modifikim=modifikim&id=" + kok.IdKokaFleteKontabel + "&numur=" + kok.NrDukumentiKokaFleteKontabel + "')";
                            else
                                ASPxMenu1.Items[ASPxMenu1.Items.Count - 1].ClientVisible = false;
                        }
                        break;
                    case "QendraKosto":
                        if ((hfShtimModifikim.Value != "modifikim"))
                        {
                            ASPxMenu1.Items[ASPxMenu1.Items.Count - 1].ClientVisible = false;
                        }
                        else
                        {
                            if (kok.NrDukumentiKokaFleteKontabel != null)
                            {
                                clsKokaQendraKosto qend = new clsKokaQendraKosto();
                                qend.KtheKokaQKSipasIDGjeneruesDheKonfig(kok.IdKokaFleteKontabel, kok.IdKonfigAmbjente);
                                if (qend.NrDok != null)
                                    ASPxMenu1.Items[ASPxMenu1.Items.Count - 1].NavigateUrl = "javascript: myButtonClickLupa.LupaUniversal_Click('Shperndarje ne qendrat e kostos','LupaRegjistrimQendraKosto.aspx?shtim_modifikim=modifikim&idDokGjenerues=" + kok.IdKokaFleteKontabel + "&idkonfig=" + kok.IdKonfigAmbjente + "',900,600)";
                                else
                                    ASPxMenu1.Items[ASPxMenu1.Items.Count - 1].ClientVisible = false;
                            }
                            else
                                ASPxMenu1.Items[ASPxMenu1.Items.Count - 1].ClientVisible = false;
                        }
                        break;
                    case "Ruaj":
                        ASPxMenu1.Items[ASPxMenu1.Items.Count - 1].ClientVisible = true;
                        break;
                    case "ItemFrame":
                        clsToolbarConfig.ShtoMenuItemPerFrame(this, ASPxMenu1, clsFunksione.ktheUrlHelpi(m.UrlHelp).Item1);
                        break;
                }
                if (m.Name == "Shto" || m.Name == "Kerko" || m.Name == "Pastro" || m.Name == "ItemFilter" || m.Name == "ItemFrame")
                    ASPxMenu1.Items[ASPxMenu1.Items.Count - 1].BeginGroup = true;
            }

            clsMenuInfo.ShtoMenuItemInfo(this, MenuInfo);
            ASPxMenu1.Items.FindByName("Draft").ClientVisible = clsFunksione.merrMenuVisibleDraft(hfShtimModifikim.Value != "modifikim", kok.IdStatusDokumenti);
        }

        protected void ASPxMenu1_DataBound(object sender, EventArgs e) => PercaktoTemplateMenu();

        private void VendosDataDefault()
        {
            DateTime sot = DateTime.Today;
            clsPeriudhaKontabel periudha = mySessionObjects.merrPeriudheKontabel(Session);
            if (sot >= periudha.FillimiPeriudha && sot <= periudha.MbarimiPeriudha)
                txtDtDok.Value = DateTime.Today;
            else
                txtDtDok.Value = periudha.FillimiPeriudha;
            txtDtRegj.Value = DateTime.Today;
        }

        private void KonfiguroVleraFillestareShto()
        {
            AspxWebControlUtils.vendosDateEditMask(txtDtDok);
            AspxWebControlUtils.vendosDateEditMask(txtDtRegj);
            VendosDataDefault();
            ConfigureAspxComboBox.mbushComboMonedha(IdPerdoruesi, IdNdermarrja, false, cmbMonedha);

            MbushComboKonfigurimeshSipasKategorise(cmbKonfigurimi, 8, Request.QueryString["lloji"] == "import" ? "FLDI" : "FLDE");

            cmbKonfigurimi.SelectedIndex = 0;
            hfKonffillestar.Value = cmbKonfigurimi.SelectedItem.Text;
            InicializoGridFaturat();
            KonfiguroGrideFaturat(true);
            ConfigureAspxComboBox.percaktoTemplateComboJoList(txtKursi);

            int idMonedheZgjedhur = clsFunksione.ktheMonedhePerFormatNumri(IdGjuha, int.Parse(cmbKonfigurimi.Value.ToString()), IdNdermarrja, 522, "", -1, true);
            clsFormatiKonfig formatNrPerKonfig = new clsFormatiKonfig();
            formatNrPerKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(int.Parse(cmbKonfigurimi.Value.ToString()));

            clsFormatKonfigTrup formatMonedhe = clsFormatKonfigTrup.ktheFormatNumriMonedhe(formatNrPerKonfig, idMonedheZgjedhur);
            hfState.Set("formatMonedhe", JsonConvert.SerializeObject(formatMonedhe));

            mySessionObjects.ruajFormatVlefta(formatMonedhe.ShifraPasPresjesVlefta, Session);
        }

        /// <summary>
        /// mbush griden e popupit me te dhena
        /// </summary>
        /// <param name="combo"></param>
        /// <param name="kat"></param>
        /// <param name="nivel"></param>
        public void MbushComboKonfigurimeshSipasKategorise(ASPxComboBox combo, int kat, string nivel)
        {
            colKonfigurimAmbjenti col = new colKonfigurimAmbjenti();
            clsKonfigurimAmbjenti konf = new clsKonfigurimAmbjenti();
            if (kat != 0)
            {
                konf.IdKategori = kat;
                konf.IdNdermarje = IdNdermarrja;
                int idNiveli = clsNivelRegjistrimi.ktheIdNivelRegjistrimiSipasKodi(nivel, IdNdermarrja);
                hfNiveli.Value = idNiveli.ToString();
                col.mbushKonfigAmbjSipasIdKategoriIdNivelMeLloj(konf.IdKategori, idNiveli, IdPerdoruesi);
            }
            else
                col.mbushGjitheKonfigurimeAmbjentesh(IdNdermarrja, IdPerdoruesi, 2, IdGjuha);

            combo.TextFormatString = "{0}";
            combo.Columns.Add(new ListBoxColumn
            {
                FieldName = "KodKonfigAmbjente",
                Caption = rm.GetString("cmbCmimeArtikulliCaptionKodi", ci)
            });
            combo.Columns.Add(new ListBoxColumn
            {
                FieldName = "PershkrimKonfigAmbjente",
                Caption = rm.GetString("cmbCmimeArtikulliCaptionPershkrimi", ci)
            });
            combo.DataSource = col;
            combo.ValueField = "IdKonfigAmbjente";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        /// <summary>
        /// Mbush komboboxet dhe gridat e faqes
        /// </summary>
        private void KonfiguroVleraFillestare()
        {
            AspxWebControlUtils.vendosDateEditMask(txtDtDok);
            AspxWebControlUtils.vendosDateEditMask(txtDtRegj);
            ConfigureAspxComboBox.percaktoTemplateComboJoList(txtKursi);
            VendosDataDefault();
            ConfigureAspxComboBox.mbushComboMonedha(IdPerdoruesi, IdNdermarrja, false, cmbMonedha);
            MbushComboKonfigurimeshSipasKategorise(cmbKonfigurimi, 8, Request.QueryString["lloji"] == "import" ? "FLDI" : "FLDE");
            InicializoGridFaturat();
            KonfiguroGrideFaturat(true);
        }

        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            pergjigja.Text = "";

            clsFleteDoganoreKoka koka = new clsFleteDoganoreKoka(int.Parse(Request.QueryString["id"]));
            clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin();
            koka.IdPerdoruesi = IdPerdoruesi;
            if (dbAdmin.eshteDokumentiILidhur(koka.IdFleteDoganoreKoka, koka.IdNivel, "T_FLETEDOGANOREKOKA", "IDFLETEDOGANORE"))
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("vepBankaMsgDokEshteILidhurNukFshihet", ci), pnlMesazhi);
                return;
            }
            if (koka.DtDok.Year != new clsNdermarrjeViti(IdNdermarrjeVit).Viti)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgDataNukPerketVititUshtrimor", ci), pnlMesazhi);
                return;
            }

            if (clsPeriudhaKontabel.eshteKycurPeriudheSipasDateDheNdermarrjes(koka.DtDok, IdNdermarrja))
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgPeriudhaEKycur", ci), pnlMesazhi);
                return;
            }

            if (DbCore.MbylljePeriudhe.PeriodClosing.IsPeriodClosed(koka.DtDok, MyConnectionsManager.GetSelectedConNameServer(), IdNdermarrja,  KategoriDokumenti.FleteDoganore, koka.IdKonfigAmbjente))
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgPeriodIsClosed"], pnlMesazhi);
                return;
            }

            clsMesazh mesazhi = new clsMesazh();
            mesazhi = koka.fshi();
            dbAdmin.Dispose();

            if (mesazhi.Status)
                Response.Redirect("FleteDoganore.aspx?lloji=" + Request.QueryString["lloji"] + "&fshi=po");
            else
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhi.PershkrimMesazhi, pnlMesazhi);
        }

        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {
            switch (e.Item.Name)
            {
                case "Ruaj":
                    Page.Validate("entries");
                    RuajFleteDoganore(1);
                    break;
                case "Draft":
                    Page.Validate("entries");
                    RuajFleteDoganore(0);
                    break;
            }
        }

        private void KonfiguroSubGrideTrupi()
        {
            clsKonfigurimAmbjenti konfigurimi = new clsKonfigurimAmbjenti();
            konfigurimi.mbushKonfigDefaultKomponentes(522, -1);
            colGridaTrupi vlere = GridUtil.percaktoVisibleColumnsSipasKonfigurimitPerClientSide2("gvSubGridFleteDoganore", "Shto_FleteDoganore.aspx?lloji=import", 1, IdGjuha);
            hfKolonaSubGride.Value = JsonConvert.SerializeObject(vlere);
        }

        private void RuajFleteDoganore(int statusDokumenti)
        {
            clsMesazh mesazh = new clsMesazh();
            if (!Page.IsValid)
                return;

            bool meKontabilizim = false;
            try
            {
                if (IsValid(statusDokumenti))
                {
                    if (statusDokumenti == 1)
                    {
                        if (hfKontabilizimi.Value == "1" || hfKontabilizimi.Value == "2")
                            meKontabilizim = true;
                    }

                    string shfaqmesazhapolupe;
                    clsFleteDoganoreKoka fleteDoganimKoka = KrijoFleteDoganoreKoka(statusDokumenti, mySessionObjects.merrPeriudheKontabel(Session).IdPeriudha, meKontabilizim, out shfaqmesazhapolupe);

                    fleteDoganimKoka.OFleteKontabel.Kontabilizuar = hfKontabilizimi.Value == "1";

                    clsTeDrejtaRoli tedrejtaInfo = new clsTeDrejtaRoli();
                    tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdNdermarrjeVit, clsFunksione.GetKomponente(Page.Request));

                    if (hfShtimModifikim.Value == "shtim")
                    {
                        if ((statusDokumenti == 1 && !tedrejtaInfo.DShtim) || (statusDokumenti == 0 && !tedrejtaInfo.DShtimDraft))
                        {
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgAdministrimiNukKeniTeDrejteVeprimi", ci), pnlMesazhi);
                            status1.Value = "false";
                            return;
                        }

                        if ((hfKontabilizimi.Value == "1" || hfKontabilizimi.Value == "2") && fleteDoganimKoka.OFleteKontabel.OColTrupi.Count != 0)
                            mesazh = fleteDoganimKoka.ruaj(hfNrAutoShitje, true, rm, ci);
                        else
                            mesazh = fleteDoganimKoka.ruaj(hfNrAutoShitje, false, rm, ci);
                    }
                    else if (hfShtimModifikim.Value == "modifikim")
                    {
                        if ((statusDokumenti == 1 && !tedrejtaInfo.DMod) || (statusDokumenti == 0 && !tedrejtaInfo.DModifikimDraft))
                        {
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgAdministrimiNukKeniTeDrejteVeprimi", ci), pnlMesazhi);
                            status1.Value = "false";
                            return;
                        }

                        fleteDoganimKoka.IdFleteDoganoreKoka = int.Parse(Request.QueryString["id"]);
                        clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin();

                        bool lidhur = dbAdmin.eshteDokumentiILidhur(fleteDoganimKoka.IdFleteDoganoreKoka, fleteDoganimKoka.IdNivel, "T_FLETEDOGANOREKOKA", "IDFLETEDOGANORE");
                        if (lidhur.ToString() != hfLidhur.Value)
                        {
                            mesazh.Status = false;
                            mesazh.PershkrimMesazhi = rm.GetString("msgDokumentiEshteILidhur", ci);
                        }
                        else if (lidhur)
                        {
                            if ((hfKontabilizimi.Value == "1" || hfKontabilizimi.Value == "2") && fleteDoganimKoka.OFleteKontabel.OColTrupi.Count != 0)
                                mesazh = fleteDoganimKoka.modifiko(true, true, rm, ci);
                            else
                                mesazh = fleteDoganimKoka.modifiko(false, true, rm, ci);
                        }
                        else if ((hfKontabilizimi.Value == "1" || hfKontabilizimi.Value == "2") && fleteDoganimKoka.OFleteKontabel.OColTrupi.Count != 0)
                            mesazh = fleteDoganimKoka.modifiko(true, false, rm, ci);
                        else
                            mesazh = fleteDoganimKoka.modifiko(false, false, rm, ci);

                        dbAdmin.Dispose();
                    }

                    if (mesazh.Status)
                    {
                        clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("msgRuajtjeMeSukses", ci), pnlMesazhi);
                        status1.Value = "true";
                        KonfiguroVleraFillestare();
                        KonfiguroVleraFillestareTaksat();
                        KonfiguroVleraFillestareTvsh();
                        KonfiguroGrideTaksat();
                        KonfiguroGrideTvsh();
                        VendosDataDefault();
                        hfqkmesazhi.Value = shfaqmesazhapolupe;

                        if (shfaqmesazhapolupe != "jo")
                        {
                            clsKokaFleteKontabel kok = new clsKokaFleteKontabel(fleteDoganimKoka.IdFleteDoganoreKoka, 8);
                            hfUrl.Value = "LupaRegjistrimQendraKosto.aspx?shtim_modifikim=modifikim&idDokGjenerues=" + kok.IdKokaFleteKontabel + "&idkonfig=" + kok.IdKonfigAmbjente;
                        }
                    }
                    else
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                        status1.Value = "false";
                        KonfiguroGrideTaksat();
                        KonfiguroGrideTvsh();
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.GetCurrentClassLogger().Error(ex.Message);
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, ex.Message, pnlMesazhi);
            }

            colFleteDoganoreTaksa taksat = KrijoTaksaFleteDoganore();
            taksat.Add(new clsFleteDoganoreTaksa());
            gvTaksat.DataSource = taksat;
            gvTaksat.DataBind();
            KonfiguroGrideTaksat();

            colFleteDoganoreTVSH tvsh = KrijoTvshFleteDoganore();
            tvsh.Add(new clsFleteDoganoreTVSH());
            gvTVSH.DataSource = tvsh;
            gvTVSH.DataBind();
            KonfiguroGrideTvsh();
        }

        private clsFleteDoganoreKoka KrijoFleteDoganoreKoka(int statusDokumenti, int periudha, bool mekontabilizim, out string shfaqmesazhapolupe)
        {
            clsKonfigurimAmbjenti clsKonf = new clsKonfigurimAmbjenti();
            if (cmbKonfigurimi.Text != "")
                clsKonf.mbushKonfigAmbjSipasKod(cmbKonfigurimi.Text, IdNdermarrja);
            else
                clsKonf.mbushKonfigDefaultKomponentes(522, IdNdermarrja);

            if (DbCore.MbylljePeriudhe.PeriodClosing.IsPeriodClosed(txtDtDok.Date, MyConnectionsManager.GetSelectedConNameServer(), IdNdermarrja, KategoriDokumenti.FleteDoganore, clsKonf.IdKonfigAmbjente))
                throw new Exception(MessagesResource.Messages["msgPeriodIsClosed"]);

            var controls = this.GetAsPxTextEditIdValue();
            controls.AddRange(ASPxSplitter1.GetAsPxTextEditIdValue());

            hfNrAuto = (ASPxHiddenField)NrAuto.VendosVleratNrAuto(hfNrAuto, controls);
            hfNrAutoShitje = (ASPxHiddenField)NrAuto.ShtoNeRegjistrime(hfNrAutoShitje, hfNrAuto, "txtNrDok", "NrDok");

            int importexport = Request.QueryString["lloji"] == "import" ? 1 : 2;

            clsFleteDoganoreKoka koka = new clsFleteDoganoreKoka();
            int idMon = cmbMonedha.Value == null ? 0 : int.Parse(cmbMonedha.Value.ToString());

            clsKokaQendraKosto qend = new clsKokaQendraKosto();
            if (hfShtimModifikim.Value == "modifikim")
            {
                clsKokaFleteKontabel kok = new clsKokaFleteKontabel(int.Parse(Request.QueryString["id"]), 8);
                qend.KtheKokaQKSipasIDGjeneruesDheKonfigMeTrup(kok.IdKokaFleteKontabel, kok.IdKonfigAmbjente);
            }

            clsMesazh mesazh = koka.krijoFleteDoganore(txtNrDok.Text, txtDtDok.Date, txtDtRegj.Date, idMon, cmbMonedha.Text, Convert.ToDecimal((txtKursi.Text == "") ? "0" : txtKursi.Text), Convert.ToDecimal((txtVlFatura.Text == "") ? "0" : txtVlFatura.Text), Convert.ToDecimal((txtVlMb.Text == "") ? "0" : txtVlMb.Text), Convert.ToDecimal((txtTransporti.Text == "") ? "0" : txtTransporti.Text), Convert.ToDecimal((txtSiguracion.Text == "") ? "0" : txtSiguracion.Text), Convert.ToDecimal((txtTjera.Text == "") ? "0" : txtTjera.Text), Convert.ToDecimal((txtVlDoganore.Text == "") ? "0" : txtVlDoganore.Text), statusDokumenti, IdNdermarrja, mySessionObjects.ktheNdermarrjeVit(Session), clsKonf.IdKonfigAmbjente, clsKonf.IdNivel, 0, 0, 0, 0, importexport, IdPerdoruesi, KrijoTrupinFleteDoganore(), KrijoTaksaFleteDoganore(), KrijoTvshFleteDoganore(), periudha, mekontabilizim, out shfaqmesazhapolupe, qend.ColTrupi, mySessionObjects.ktheGjuhe(Session), rm, ci);

            if (!mesazh)
                throw new Exception(mesazh.PershkrimMesazhi);

            return koka;
        }

        private bool IsValid(int draft)
        {
            if (txtDtRegj.Text == "")
            {
                status1.Value = "false";
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgZgjidhniDtRegjistrimi", ci), pnlMesazhi);
                return false;
            }

            if (txtDtDok.Text == "")
            {
                status1.Value = "false";
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgZgjidhniDtDok", ci), pnlMesazhi);
                return false;
            }

            if (txtDtDok.Date.Year != new clsNdermarrjeViti(IdNdermarrjeVit).Viti)
            {
                status1.Value = "false";
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgDataNukPerketVititUshtrimor", ci), pnlMesazhi);
                return false;
            }

            clsPeriudhaKontabel periudha = mySessionObjects.merrPeriudheKontabel(Session);

            string mesazhGabimi;
            if (!clsFunksione.checkPeriudheKontabel(out mesazhGabimi, txtDtDok.Date, periudha, draft))
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhGabimi, pnlMesazhi);
                return false;
            }

            if (txtNrDok.Text == "")
            {
                status1.Value = "false";
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgVendosniNrDok", ci), pnlMesazhi);
                return false;
            }

            colFleteDoganoreTrupi trupat = KrijoTrupinFleteDoganore();
            if (trupat.Count == 0)
            {
                status1.Value = "false";
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgZgjidhniFature", ci), pnlMesazhi);
                return false;
            }

            if (!trupat.Any(t => t.DtDok > txtDtDok.Date)) return true;

            status1.Value = "false";
            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgDtFDMadheDtFature", ci), pnlMesazhi);
            return false;
        }

        private colFleteDoganoreTrupi KrijoTrupinFleteDoganore()
        {
            List<Dictionary<string, object>> dokumenti = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(hfNrFature.Value);
            List<string> transport = JsonConvert.DeserializeObject<List<string>>(hfTransport.Value);
            List<string> siguracion = JsonConvert.DeserializeObject<List<string>>(hfSiguracion.Value);
            List<string> tjera = JsonConvert.DeserializeObject<List<string>>(hfTjera.Value);
            List<string> vldog = JsonConvert.DeserializeObject<List<string>>(hfVlDogane.Value);
            List<string> taksa = JsonConvert.DeserializeObject<List<string>>(hfTaksa.Value);
            List<List<Dictionary<string, object>>> vlerasub = JsonConvert.DeserializeObject<List<List<Dictionary<string, object>>>>(hfVleraSub.Value);
            decimal marzhiGabimit = Convert.ToDecimal(clsFunksione.krijoNumer(mySessionObjects.merrFormatVleftaSesioni(Session), "0") + "1");
            colFleteDoganoreTrupi col = new colFleteDoganoreTrupi();
            try
            {
                for (int i = 0; i < dokumenti.Count; i++)
                {
                    if (dokumenti[i] == null) continue;

                    clsFleteDoganoreTrupi trup = new clsFleteDoganoreTrupi(IdNdermarrja, dokumenti[i], transport[i], siguracion[i], tjera[i], vldog[i], taksa[i], vlerasub[i], marzhiGabimit);
                    if (trup.IdFatura != -1)
                        col.Add(trup);
                }
            }
            catch (Exception ex)
            {
                LogManager.GetCurrentClassLogger().Error(ex.Message);
                throw new Exception(ex.Message);
            }
            return col;
        }

        private colFleteDoganoreTaksa KrijoTaksaFleteDoganore()
        {
            List<string> kodi = JsonConvert.DeserializeObject<List<string>>(hfKodi.Value);
            List<string> pershkrimi = JsonConvert.DeserializeObject<List<string>>(hfPershkrimi.Value);
            List<string> vlera = JsonConvert.DeserializeObject<List<string>>(hfVlera.Value);
            List<string> tvsh = JsonConvert.DeserializeObject<List<string>>(hfTvsh.Value);
            List<string> norma = JsonConvert.DeserializeObject<List<string>>(hfNorma.Value);
            colFleteDoganoreTaksa col = new colFleteDoganoreTaksa();

            if (kodi == null) return col;

            for (int i = 0; i < kodi.Count; i++)
            {
                if (kodi[i] == null) continue;

                clsFleteDoganoreTaksa trup = new clsFleteDoganoreTaksa(kodi[i], pershkrimi[i], vlera[i], tvsh[i], norma[i], IdNdermarrja);
                if (trup.IdTaksa != -1)
                    col.Add(trup);
            }
            return col;
        }

        private colFleteDoganoreTVSH KrijoTvshFleteDoganore()
        {
            List<string> kodi = JsonConvert.DeserializeObject<List<string>>(hfKodiTVSH.Value);
            List<string> pershkrimi = JsonConvert.DeserializeObject<List<string>>(hfPershkrimiTVSH.Value);
            List<string> vlera = JsonConvert.DeserializeObject<List<string>>(hfVleraFaturuarTVSH.Value);
            List<string> tvsh = JsonConvert.DeserializeObject<List<string>>(hfVleftaTvsh.Value);
            List<string> norma = JsonConvert.DeserializeObject<List<string>>(hfNormaTVSH.Value);
            List<string> aqt = JsonConvert.DeserializeObject<List<string>>(hfAQT.Value);
            colFleteDoganoreTVSH col = new colFleteDoganoreTVSH();

            if (kodi == null) return col;

            for (int i = 0; i < kodi.Count; i++)
            {
                if (kodi[i] == null) continue;

                clsFleteDoganoreTVSH trup = new clsFleteDoganoreTVSH(kodi[i], pershkrimi[i], vlera[i], tvsh[i], norma[i], IdNdermarrja, aqt[i]);
                if (trup.IdTaksa != -1)
                    col.Add(trup);
            }
            return col;
        }

        /// <summary>
        /// funksioni therritet ne rastin kur jemi duke bere modifikim per te mbushur hidden fields me vlerat e pershtatshme
        /// ne rastin kur kemi shtim, hidden fields mbushen nepermjet nje funksioni ekuivalent me te meposhtmin, por qe thirret nga nje web service 
        /// funksioni qe ekzekutohet me Web Service eshte: public string[] ktheFaturaPerFleteDoganore(string filtrat)
        /// </summary>
        /// <param name="vleratIdFatura"></param>
        private void MbushFushatFaturesModifkim(List<string> pars)
        {
            colKokaShitje colKokaSh = new colKokaShitje();
            clsKokaShitje oKokaSh = new clsKokaShitje();

            int k;
            int j;

            colKokaSh = new colKokaShitje();
            for (k = 0; k < pars.Count; k++)
            {
                if (pars[k] == "") continue;

                oKokaSh = new clsKokaShitje();
                oKokaSh.mbushKokaShitjeSipasIDPaTrup(Convert.ToInt32(pars[k]));
                oKokaSh.mbushTrupShitje();
                colKokaSh.Add(oKokaSh);
            }

            colFleteDoganoreTrupi trupi = new colFleteDoganoreTrupi(int.Parse(Request.QueryString["id"]));
            List<decimal> transport = new List<decimal>();
            List<decimal> siguracion = new List<decimal>();
            List<decimal> tjera = new List<decimal>();
            List<decimal> vldog = new List<decimal>();
            List<decimal> taksa = new List<decimal>();
            List<List<object>> sub = new List<List<object>>();
            List<colArtikujt> arti = new List<colArtikujt>();
            List<colLlogarite> llog = new List<colLlogarite>();
            List<colNjesiAdministrative> magazina = new List<colNjesiAdministrative>();
            List<colNjesiteArtikulli> njesi = new List<colNjesiteArtikulli>();
            colKlienteFurnitore colkf = new colKlienteFurnitore();

            for (k = 0; k < colKokaSh.Count; k++)
            {
                colArtikujt colart = new colArtikujt();
                colLlogarite colllog = new colLlogarite();
                colNjesiAdministrative colmag = new colNjesiAdministrative();
                colNjesiteArtikulli colnjesi = new colNjesiteArtikulli();

                for (j = 0; j < colKokaSh[k].OColTrupiShitje.Count; j++)
                {
                    if (colKokaSh[k].OColTrupiShitje[j].IdLlojVeprimi == 1)
                    {
                        colart.Add(new clsArtikulli(colKokaSh[k].OColTrupiShitje[j].IdKodi));
                        colllog.Add(new clsLlogari());
                    }
                    else if (colKokaSh[k].OColTrupiShitje[j].IdLlojVeprimi == 3)
                    {
                        colllog.Add(new clsLlogari(colKokaSh[k].OColTrupiShitje[j].IdKodi));
                        colart.Add(new clsArtikulli());
                    }

                    colmag.Add(new clsNjesiAdministrative(colKokaSh[k].OColTrupiShitje[j].IdMagazina));
                    colnjesi.Add(new clsNjesiArtikulli(colKokaSh[k].OColTrupiShitje[j].IdNjesia));
                }

                foreach (clsFleteDoganoreTrupi t in trupi)
                {
                    if (t.IdFatura != colKokaSh[k].IdShitjeKoka) continue;

                    List<object> subrresht = new List<object>();
                    colFleteDoganoreDetajim det = new colFleteDoganoreDetajim(t.IdFleteDoganoreTrupi);

                    if (det.Count > 0)
                    {
                        transport.Add(det[0].VlTransport);
                        siguracion.Add(det[0].VlSiguracion);
                        tjera.Add(det[0].VlTjera);
                        vldog.Add(det[0].VlDoganim);
                        taksa.Add(det[0].VlTaksa);
                    }

                    colFleteDoganoreDetajimSub col = new colFleteDoganoreDetajimSub(det[0].IdFleteDoganoreDetajim);
                    foreach (clsFleteDoganoreDetajimSub s in col)
                    {
                        object detajuar = new { Taksa = s.VlTaksa, Dogana = s.VlDoganim, Tjera = s.VlTjera, Siguracioni = s.VlSiguracion, Transporti = s.VlTransport };
                        subrresht.Add(detajuar);
                    }

                    sub.Add(subrresht);
                }
                arti.Add(colart);
                llog.Add(colllog);
                magazina.Add(colmag);
                njesi.Add(colnjesi);
                colkf.Add(new clsKlientFurnitor(colKokaSh[k].IdKlientFurnitor));
            }

            hfNrFature.Value = JsonConvert.SerializeObject(colKokaSh);
            hfTransport.Value = JsonConvert.SerializeObject(transport);
            hfSiguracion.Value = JsonConvert.SerializeObject(siguracion);
            hfTjera.Value = JsonConvert.SerializeObject(tjera);
            hfVlDogane.Value = JsonConvert.SerializeObject(vldog);
            hfTaksa.Value = JsonConvert.SerializeObject(taksa);
            hfFurnitori.Value = JsonConvert.SerializeObject(colkf);
            hfNjesia.Value = JsonConvert.SerializeObject(njesi);
            hfMagazina.Value = JsonConvert.SerializeObject(magazina);
            hfArtikulli.Value = JsonConvert.SerializeObject(arti);
            hfLlog.Value = JsonConvert.SerializeObject(llog);
            hfVleraSub.Value = JsonConvert.SerializeObject(sub);

        }

        #region tvsh

        private void KonfiguroVleraFillestareTvsh()
        {
            MbushListeTvsh(oKoka.IdFleteDoganoreKoka);
        }

        private void KonfiguroGrideTvsh()
        {
            PercaktoTamplateTvsh();
            GridUtil.percaktoVisibleColumnsMeWidth(IdGjuha, IdNdermarrja, gvTVSH, "gvTVSH", Komponente);
            GridUtil.konfiguroGrideRegjistrimEvogelPaTheme(gvTVSH, "IdFleteDoganoreTVSH", false);
            gvTaksat.Settings.UseFixedTableLayout = false;
        }

        /// <summary>
        /// shton colonen Fshi per te lejuar fshirjen e nje apo disa rreshtave te grides
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvTVSH_DataBound(object sender, EventArgs e)
        {
            if (gvTVSH.Columns[kolFshi] != null) return;

            GridViewDataTextColumn fshi = new GridViewDataTextColumn
            {
                Caption = MessagesResource.Messages["lblFshiBtn"],
                Width = Unit.Percentage(7)
            };

            gvTVSH.Columns.Add(fshi);
            gvTVSH.KeyFieldName = "IdFleteDoganoreTVSH";
            gvTVSH.SettingsBehavior.AllowSelectByRowClick = false;
            gvTVSH.SettingsBehavior.AllowFocusedRow = true;
        }

        /// <summary>
        /// mbush griden me te dhena
        /// </summary>
        /// <param name="idKoka"></param>
        private void MbushListeTvsh(int idKoka)
        {
            colFleteDoganoreTVSH col = new colFleteDoganoreTVSH();

            if (hfShtimModifikim.Value != "modifikim")
                col.Add(new clsFleteDoganoreTVSH());

            col.AddRange(new colFleteDoganoreTVSH(idKoka));

            foreach (clsFleteDoganoreTVSH tr in col)
            {
                tr.KodTVSH = new clsTaksa(tr.IdTaksa).KodTaksa;
                tr.NormaTVSH = new clsTaksa(tr.IdTaksa).NormaPerqindje.ToString();
            }

            col.Add(new clsFleteDoganoreTVSH());

            gvTVSH.DataSource = col;
            gvTVSH.DataBind();
        }


        private void PercaktoTamplateTvsh()
        {
            int formatvlefta;
            try
            {
                formatvlefta = mySessionObjects.merrFormatVleftaSesioni(Session);
            }
            catch (MyException ex)
            {
                LogManager.GetCurrentClassLogger().Error(ex.Message);
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, ex.Message, pnlMesazhi);
                return;
            }

            GridViewDataTextColumn col0 = gvTVSH.Columns[kolFshi] as GridViewDataTextColumn;
            col0.DataItemTemplate = new MyButtonTemplate("");
            col0.VisibleIndex = 10;
            GridViewDataColumn col1 = gvTVSH.Columns["IdTaksa"] as GridViewDataColumn;
            GridViewDataColumn col2 = gvTVSH.Columns["Pershkrimi"] as GridViewDataColumn;
            GridViewDataColumn col3 = gvTVSH.Columns["VleftaFaturuar"] as GridViewDataColumn;
            GridViewDataColumn col4 = gvTVSH.Columns["VleftaTvsh"] as GridViewDataColumn;
            GridViewDataColumn col5 = gvTVSH.Columns["NormaTVSH"] as GridViewDataColumn;
            GridViewDataColumn col6 = gvTVSH.Columns["Aqt"] as GridViewDataColumn;
            col4.Settings.AllowAutoFilter = DefaultBoolean.False;
            col1.DataItemTemplate = new MyComboTemplate();
            col2.DataItemTemplate = new MyReadOnlyMemoTemplate();
            col5.DataItemTemplate = new MyReadOnlyTextTemplate();
            col6.DataItemTemplate = new MyComboTemplate();
            col2.Width = 100;
            col3.DataItemTemplate = new MyDoubleTemplate(false, formatvlefta, "0");
            col3.Width = 100;
            col4.DataItemTemplate = new MyDoubleTemplate(false, formatvlefta, "0");
        }

        /// <summary>
        /// krijon rreshat sipas modelit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvTVSH_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType == GridViewRowType.Data)
            {
                GridViewDataTextColumn col0 = gvTVSH.Columns[kolFshi] as GridViewDataTextColumn;
                GridViewDataColumn col1 = gvTVSH.Columns["IdTaksa"] as GridViewDataColumn;
                GridViewDataColumn col2 = gvTVSH.Columns["Pershkrimi"] as GridViewDataColumn;
                GridViewDataColumn col3 = gvTVSH.Columns["VleftaFaturuar"] as GridViewDataColumn;
                GridViewDataColumn col4 = gvTVSH.Columns["VleftaTvsh"] as GridViewDataColumn;
                GridViewDataColumn col5 = gvTVSH.Columns["NormaTVSH"] as GridViewDataColumn;
                GridViewDataColumn col6 = gvTVSH.Columns["Aqt"] as GridViewDataColumn;
                ASPxButton btn0 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col0, "btn") as ASPxButton;
                ASPxComboBox cmb1 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col1, "cmbBox") as ASPxComboBox;
                ASPxMemo txt2 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col2, "txtBox") as ASPxMemo;
                ASPxTextBox txt3 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col3, "txtBox") as ASPxTextBox;
                ASPxTextBox txt4 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col4, "txtBox") as ASPxTextBox;
                ASPxTextBox txt5 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col5, "txtBox") as ASPxTextBox;
                ASPxComboBox cmb2 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col6, "cmbBox") as ASPxComboBox;

                bool ugjet = false;

                if (btn0 != null)
                {
                    btn0.ClientInstanceName = "btnFshiTVSH" + e.VisibleIndex;
                    btn0.ClientSideEvents.Click = "function(s,e){FshiClickedTVSH(" + e.VisibleIndex + ");}";
                }

                colTaksa coltaksa = new colTaksa(IdNdermarrja, LlojTakse.Nivel_Tvsh, IdPerdoruesi);

                if (cmb1 != null)
                {
                    cmb1.Columns.Add(new ListBoxColumn { FieldName = "KodTaksa" });
                    cmb1.Columns.Add(new ListBoxColumn { FieldName = "Pershkrimi" });
                    cmb1.Columns.Add(new ListBoxColumn { FieldName = "NormaPerqindje" });
                    cmb1.ValueField = "IdTaksa";
                    cmb1.ValueType = typeof(string);
                    cmb1.TextFormatString = "{0}";
                    cmb1.DataSource = coltaksa;
                    cmb1.DataBind();
                    cmb1.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                    cmb1.ClientInstanceName = "txtKodiTVSH" + e.VisibleIndex;
                    cmb1.ClientSideEvents.TextChanged = "function(s,e){TextChangedKodiTVSH(txtKodiTVSH" + e.VisibleIndex + ", txtPershkrimiTVSH" + e.VisibleIndex + ",txtVleraFaturuar" + e.VisibleIndex + "," + e.VisibleIndex + ");}";

                    if (e.VisibleIndex == ((ASPxGridView)sender).VisibleRowCount - 2 && ugjet)
                    {
                        tempcombo = cmb1;
                        ugjet = false;
                    }
                }

                if (cmb2 != null)
                {
                    cmb2.Items.Add("Mallra", "Unchecked");
                    cmb2.Items.Add("Investime", "Checked");
                    cmb2.ClientInstanceName = "txtAQT" + e.VisibleIndex;

                    if (e.VisibleIndex == ((ASPxGridView)sender).VisibleRowCount - 2 && ugjet)
                    {
                        tempcombo = cmb1;
                        ugjet = false;
                    }
                }

                if (txt2 != null)
                    txt2.ClientInstanceName = "txtPershkrimiTVSH" + e.VisibleIndex;

                if (txt5 != null)
                    txt5.ClientInstanceName = "txtNormaTVSH" + e.VisibleIndex;

                if (txt3 != null)
                {
                    txt3.ClientInstanceName = "txtVleraFaturuar" + e.VisibleIndex;
                    txt3.ClientSideEvents.TextChanged = "function(s,e){TextChangedVleftaFaturuar(txtVleraFaturuar" + e.VisibleIndex + ", " + e.VisibleIndex + ");}";

                    if (e.VisibleIndex == ((ASPxGridView)sender).VisibleRowCount - 2)
                    {
                        if (ugjet)
                        {
                            temptxt = txt3;
                            ugjet = false;
                        }
                    }
                }

                if (txt4 != null)
                {
                    txt4.ClientInstanceName = "txtVleftaTvsh" + e.VisibleIndex;
                    txt4.ClientSideEvents.TextChanged = "function(s,e){TextChangedVleftaTVSH(txtVleftaTvsh" + e.VisibleIndex + ", " + e.VisibleIndex + ");}";
                    if (e.VisibleIndex == ((ASPxGridView)sender).VisibleRowCount - 2)
                    {
                        if (ugjet)
                        {
                            temptxt = txt4;
                            ugjet = false;
                        }
                    }
                }

            }

            if (temptxt != null)
            {
                temptxt.Focus();
            }
        }

        protected void gvTVSH_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e) =>
            e.Properties["cpNoRows"] = gvTVSH.VisibleRowCount;

        protected void gvTVSH_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e) =>
            gvTVSH.DataBind();

        /// <summary>
        /// kur grida ben callback te ruajme te dhenat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvTVSH_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            int key = -1;

            if (e.Parameters.Split('|').Length == 1 && e.Parameters != "")
                key = int.Parse(e.Parameters);

            colFleteDoganoreTVSH fdTaksat = new colFleteDoganoreTVSH();

            if (e.Parameters.Split('|').Length == 2)
            {
                List<string> arrFiltrat = JsonConvert.DeserializeObject<List<string>>(e.Parameters.Split('|')[1]);
                colTaksa colt = new colTaksa(IdNdermarrja, LlojTakse.Nivel_Tvsh, IdPerdoruesi);
                int[] nivelTvsh = new int[colt.Count * 2];
                int[] aqt = new int[colt.Count * 2];

                for (int m = 0; m < nivelTvsh.Length; m++)
                {
                    nivelTvsh[m] = -10;
                    aqt[m] = 0;
                }

                double[] vlerafaturuar = new double[colt.Count * 2];

                decimal shumataksa = 0;
                List<string> kodi = JsonConvert.DeserializeObject<List<string>>(hfKodi.Value);
                List<string> vlera = JsonConvert.DeserializeObject<List<string>>(hfVlera.Value);
                List<string> tvsh = JsonConvert.DeserializeObject<List<string>>(hfTvsh.Value);

                if (kodi.Count != 0 && vlera.Count != 0 && tvsh.Count != 0)
                    for (int i = 0; i < kodi.Count; i++)
                        if (kodi[i] != "" && Boolean.Parse(tvsh[i]))
                            shumataksa += Convert.ToDecimal(vlera[i]);

                double totali = 0;
                clsTaksa taksaNderm = new clsTaksa();
                taksaNderm.mbushTakseDefaultNdermarrje(IdNdermarrja);
                foreach (string filter in arrFiltrat)
                {
                    if (filter == "") continue;

                    colTrupiShitje trupi = new colTrupiShitje();
                    trupi.mbushGjitheTrupiShitjeNgaKoka(Convert.ToInt32(filter));

                    clsKokaShitje koka = new clsKokaShitje();
                    koka.mbushKokaShitjeSipasIDPaTrup(Convert.ToInt32(filter));

                    foreach (clsTrupiShitje t in trupi)
                    {
                        int idTvsh = 0;
                        int aqtart = 0;

                        if (t.IdLlojVeprimi == 1)
                        {
                            clsArtikulli art = new clsArtikulli(t.IdKodi);
                            if (art.LlojiArt)
                                aqtart = 1;
                            idTvsh = art.IdTvsh;
                        }

                        if (t.IdLlojVeprimi == 3)
                        {
                            idTvsh = clsLlogari.ktheNivelTakse(t.IdKodi);
                            if (t.Kodi.StartsWith("2"))
                                aqtart = 1;
                        }

                        clsTaksa tak = new clsTaksa(idTvsh);
                        if (!tak.Aktiv)
                            idTvsh = 0;

                        if (idTvsh == 0)
                            idTvsh = taksaNderm.IdTaksa;

                        for (int j = 0; j < nivelTvsh.Length; j++)
                        {
                            if (nivelTvsh[j] != idTvsh || aqt[j] != aqtart)
                            {
                                if (nivelTvsh[j] == -10)
                                {
                                    aqt[j] = aqtart;
                                    nivelTvsh[j] = idTvsh;
                                    vlerafaturuar[j] = t.VleftaPaTvsh * (1 - koka.Zbritje / koka.Totali);
                                    break;
                                }
                            }
                            else
                            {
                                vlerafaturuar[j] += t.VleftaPaTvsh * (1 - koka.Zbritje / koka.Totali);
                                break;
                            }
                        }

                        totali += t.VleftaPaTvsh * (1 - koka.Zbritje / koka.Totali);
                    }
                }

                double kursifundit = cmbMonedha.Value != null && cmbMonedha.Value.ToString() != ""
                    ? new clsKurset(int.Parse(cmbMonedha.Value.ToString()), DateTime.Today).VleraKursi
                    : 1;

                for (int i = 0; i < nivelTvsh.Length; i++)
                {
                    clsFleteDoganoreTVSH fdTaksa = new clsFleteDoganoreTVSH();

                    if (nivelTvsh[i] == -10) continue;

                    clsTaksa taksa = new clsTaksa(nivelTvsh[i]);
                    fdTaksa.IdTaksa = taksa.IdTaksa;
                    fdTaksa.KodTVSH = taksa.KodTaksa;
                    fdTaksa.Pershkrimi = taksa.Pershkrimi;
                    fdTaksa.Aqt = Convert.ToBoolean(aqt[i]);
                    fdTaksa.VleftaFaturuar = Convert.ToDecimal(vlerafaturuar[i]);
                    fdTaksa.VleftaTvsh = Convert.ToDecimal((Convert.ToDecimal(vlerafaturuar[i]) * Convert.ToDecimal(kursifundit) + (Convert.ToDecimal((txtTransporti.Text == "") ? "0" : txtTransporti.Text) + Convert.ToDecimal(txtSiguracion.Text == "" ? "0" : txtSiguracion.Text) + Convert.ToDecimal(txtTjera.Text == "" ? "0" : txtTjera.Text) + shumataksa) * Convert.ToDecimal(vlerafaturuar[i] / totali)) * taksa.NormaPerqindje / 100);
                    fdTaksa.NormaTVSH = taksa.NormaPerqindje.ToString(); ;
                    fdTaksat.Add(fdTaksa);
                }
            }
            else
                fdTaksat = KrijoTvshFleteDoganore();

            fdTaksat.Add(new clsFleteDoganoreTVSH());

            if (key != -1)
                fdTaksat.RemoveAt(key);

            if (fdTaksat.Count == 0)
                fdTaksat.Add(new clsFleteDoganoreTVSH());

            gvTVSH.DataSource = fdTaksat;
            gvTVSH.DataBind();
            KonfiguroGrideTvsh();
        }

        #endregion

        #region taksat

        private void KonfiguroVleraFillestareTaksat()
        {
            MbushListeTaksat(oKoka.IdFleteDoganoreKoka);
        }


        private void KonfiguroGrideTaksat()
        {
            gvTaksat.KonfiguroCombo("KodTaksa", "IdTaksa", "KodTaksa", () =>
            {
                colTaksa col = new colTaksa(IdNdermarrja, LlojTakse.Takse_Doganore, IdPerdoruesi);
                hfState.Set("taksatCombo", JsonConvert.SerializeObject(col));
                return col;
            }, Session, Komponente, GuidString);
            PercaktoTamplateTaksa();
            GridUtil.percaktoVisibleColumnsMeWidth(IdGjuha, IdNdermarrja, gvTaksat, "gvTaksat", Komponente);
            GridUtil.konfiguroGrideRegjistrimEvogelPaTheme(gvTaksat, "IdFleteDoganoreTaksa", false);
            gvTaksat.Settings.UseFixedTableLayout = false;
        }

        /// <summary>
        /// Shton colonen Fshi per te lejuar fshirjen e nje apo disa rreshtave te grides
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvTaksat_DataBound(object sender, EventArgs e)
        {
            if (gvTaksat.Columns[kolFshi] != null) return;

            GridViewDataTextColumn fshi = new GridViewDataTextColumn
            {
                Caption = MessagesResource.Messages["lblFshiBtn"],
                Width = Unit.Percentage(5)
            };

            gvTaksat.Columns.Add(fshi);
            gvTaksat.KeyFieldName = "IdFleteDoganoreTaksa";
            gvTaksat.SettingsBehavior.AllowSelectByRowClick = false;
            gvTaksat.SettingsBehavior.AllowFocusedRow = true;
        }

        private void MbushListeTaksat(int idKoka)
        {
            colFleteDoganoreTaksa col = new colFleteDoganoreTaksa();

            if (hfShtimModifikim.Value != "modifikim")
                col.Add(new clsFleteDoganoreTaksa());
            col.AddRange(new colFleteDoganoreTaksa(idKoka));

            foreach (clsFleteDoganoreTaksa tr in col)
            {
                tr.KodTaksa = new clsTaksa(tr.IdTaksa).KodTaksa;
                tr.NormaTaksa = new clsTaksa(tr.IdTaksa).NormaPerqindje.ToString();
            }

            col.Add(new clsFleteDoganoreTaksa());

            gvTaksat.DataSource = col;
            gvTaksat.DataBind();
        }

        private void PercaktoTamplateTaksa()
        {
            int formatvlefta;
            try
            {
                formatvlefta = mySessionObjects.merrFormatVleftaSesioni(Session);
            }
            catch (MyException ex)
            {
                LogManager.GetCurrentClassLogger().Error(ex.Message);
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, ex.Message, pnlMesazhi);
                return;
            }

            GridViewDataTextColumn col0 = gvTaksat.Columns[kolFshi] as GridViewDataTextColumn;
            col0.DataItemTemplate = new MyButtonTemplate("");
            col0.VisibleIndex = 10;
            GridViewDataColumn col1 = gvTaksat.Columns["KodTaksa"] as GridViewDataColumn;
            GridViewDataColumn col2 = gvTaksat.Columns["Pershkrimi"] as GridViewDataColumn;
            GridViewDataColumn col3 = gvTaksat.Columns["Vlefta"] as GridViewDataColumn;
            GridViewDataCheckColumn col4 = gvTaksat.Columns["Tvsh"] as GridViewDataCheckColumn;
            GridViewDataColumn col5 = gvTaksat.Columns["NormaTaksa"] as GridViewDataColumn;

            col4.Settings.AllowAutoFilter = DefaultBoolean.False;
            col1.DataItemTemplate = new MyComboTemplate();
            col2.DataItemTemplate = new MyReadOnlyMemoTemplate();
            col5.DataItemTemplate = new MyReadOnlyTextTemplate();
            col2.Width = 100;
            col3.DataItemTemplate = new MyDoubleTemplate(false, formatvlefta, "0");
            col3.Width = 100;
            col4.DataItemTemplate = new MyCheckTemplate(false, false);
        }

        /// <summary>
        /// krijon rreshat sipas modelit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvTaksa_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType == GridViewRowType.Data)
            {
                GridViewDataTextColumn col0 = ((ASPxGridView)sender).Columns[kolFshi] as GridViewDataTextColumn;
                GridViewDataTextColumn col1 = ((ASPxGridView)sender).Columns["KodTaksa"] as GridViewDataTextColumn;
                GridViewDataTextColumn col2 = ((ASPxGridView)sender).Columns["Pershkrimi"] as GridViewDataTextColumn;
                GridViewDataTextColumn col3 = ((ASPxGridView)sender).Columns["Vlefta"] as GridViewDataTextColumn;
                GridViewDataCheckColumn col4 = ((ASPxGridView)sender).Columns["Tvsh"] as GridViewDataCheckColumn;
                GridViewDataColumn col5 = ((ASPxGridView)sender).Columns["NormaTaksa"] as GridViewDataColumn;
                ASPxButton btn0 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col0, "btn") as ASPxButton;
                ASPxComboBox cmb1 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col1, "cmbBox") as ASPxComboBox;
                ASPxMemo txt2 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col2, "txtBox") as ASPxMemo;
                ASPxTextBox txt3 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col3, "txtBox") as ASPxTextBox;
                ASPxCheckBox chk4 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col4, "cb") as ASPxCheckBox;
                ASPxTextBox txt5 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col5, "txtBox") as ASPxTextBox;

                bool ugjet = false;

                if (btn0 != null)
                {
                    btn0.ClientInstanceName = "btnFshi" + e.VisibleIndex;
                    btn0.ClientSideEvents.Click = "function(s,e){FshiClicked(" + e.VisibleIndex + ");}";
                }

                colTaksa coltaksa = new colTaksa(IdNdermarrja, LlojTakse.Takse_Doganore, IdPerdoruesi);
                coltaksa.Insert(0, new clsTaksa());
                if (cmb1 != null)
                {
                    cmb1.Columns.Add(new ListBoxColumn { FieldName = "KodTaksa" });
                    cmb1.Columns.Add(new ListBoxColumn { FieldName = "Pershkrimi" });
                    cmb1.Columns.Add(new ListBoxColumn { FieldName = "NormaPerqindje" });
                    cmb1.Columns.Add(new ListBoxColumn { FieldName = "Njesia" });
                    cmb1.DataSource = coltaksa;
                    cmb1.DataBind();
                    cmb1.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                    cmb1.ClientInstanceName = "txtKodi" + e.VisibleIndex;
                    cmb1.ClientSideEvents.TextChanged = "function(s,e){TextChangedKodi(txtKodi" + e.VisibleIndex + ", txtPershkrimi" + e.VisibleIndex + ",txtVlera" + e.VisibleIndex + ",txtTvsh" + e.VisibleIndex + "," + e.VisibleIndex + ");}";

                    if (e.VisibleIndex == ((ASPxGridView)sender).VisibleRowCount - 2)
                    {
                        if (ugjet)
                        {
                            tempcombo = cmb1;
                            ugjet = false;
                        }
                    }
                }

                if (txt2 != null)
                {
                    txt2.ClientInstanceName = "txtPershkrimi" + e.VisibleIndex;
                    txt2.ClientSideEvents.TextChanged = "function(s,e){TextChangedPershkrimi(txtPershkrimi" + e.VisibleIndex + ", " + e.VisibleIndex + ");}";
                }

                if (txt5 != null)
                {
                    txt5.ClientInstanceName = "txtNorma" + e.VisibleIndex;

                    if (e.VisibleIndex == ((ASPxGridView)sender).VisibleRowCount - 2)
                    {
                        if (ugjet)
                        {
                            temptxt = txt5;
                            ugjet = false;
                        }
                    }
                }

                if (txt3 != null)
                {
                    txt3.ClientInstanceName = "txtVlera" + e.VisibleIndex;
                    txt3.ClientSideEvents.TextChanged = "function(s,e){TextChangedVlefta(txtVlera" + e.VisibleIndex + ", " + e.VisibleIndex + ");}";

                    if (e.VisibleIndex == ((ASPxGridView)sender).VisibleRowCount - 2)
                    {
                        if (ugjet)
                        {
                            temptxt = txt3;
                            ugjet = false;
                        }
                    }
                }

                if (chk4 != null)
                {
                    if (Request.QueryString["lloji"] == "export")
                        chk4.ClientEnabled = false;

                    chk4.ClientInstanceName = "txtTvsh" + e.VisibleIndex;
                    chk4.ClientSideEvents.CheckedChanged = "function(s,e){CheckedChangedTaksa(txtTvsh" + e.VisibleIndex + ", " + e.VisibleIndex + ");}";
                }
            }

            if (temptxt != null)
            {
                temptxt.Focus();
            }
        }

        protected void gvTaksat_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e) =>
            e.Properties["cpNoRows"] = gvTaksat.VisibleRowCount;


        protected void gvTaksat_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e) =>
            gvTaksat.DataBind();

        /// <summary>
        /// kur grida ben callback te ruajme te dhenat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvTaksat_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            int key = -1;
            if (e.Parameters != "")
                key = int.Parse(e.Parameters);
            int id = -1;

            List<string> idfat = JsonConvert.DeserializeObject<List<string>>(hfIdFaturave.Value);
            if (idfat != null && idfat.Count != 0)
                id = int.Parse(idfat[0]);

            int idMonedheZgjedhur = clsFunksione.ktheMonedhePerFormatNumri(IdGjuha, int.Parse(cmbKonfigurimi.Value.ToString()), IdNdermarrja, 522, "", id, true);
            clsFormatiKonfig formatNrPerKonfig = new clsFormatiKonfig();
            formatNrPerKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(int.Parse(cmbKonfigurimi.Value.ToString()));
            clsFormatKonfigTrup formatMonedhe = clsFormatKonfigTrup.ktheFormatNumriMonedhe(formatNrPerKonfig, idMonedheZgjedhur);
            mySessionObjects.ruajFormatVlefta(formatMonedhe.ShifraPasPresjesVlefta, Session);

            colFleteDoganoreTaksa col = KrijoTaksaFleteDoganore();
            col.Add(new clsFleteDoganoreTaksa());

            if (key != -1)
                col.RemoveAt(key);

            if (col.Count == 0)
                col.Add(new clsFleteDoganoreTaksa());

            gvTaksat.DataSource = col;
            gvTaksat.DataBind();
            KonfiguroGrideTaksat();
        }

        #endregion

        #region  GRIDA E FATURAVE

        private void InicializoGridFaturat()
        {
            ASPxGridView gridFaturat = (ASPxGridView)ASPxNavBar1.Groups[0].FindControl("grid_faturat");
            gridFaturat.DataSource = colDokumentat.mbushGjitheDokumentatFleteDoganore(IdNdermarrja, new DateTime().ToShortDateString(), new clsNdermarrjeViti(IdNdermarrjeVit).NdermarrjeVitiFund.ToShortDateString(), Request.QueryString["lloji"] == "import" ? 2 : 1, IdPerdoruesi);
            gridFaturat.DataBind();
        }

        private void KonfiguroGrideFaturat(bool visibleindex)
        {
            ASPxGridView gridFaturat = (ASPxGridView)ASPxNavBar1.Groups[0].FindControl("grid_faturat");
            Shtokolona();
            ShtoNivel();

            if (visibleindex)
                GridUtil.percaktoVisibleColumnsMeWidth(IdGjuha, IdNdermarrja, gridFaturat, "grid_faturat", Komponente);
            else
                GridUtil.percaktoVisibleColumnsMeWidthPaVisibleIndex(IdGjuha, IdNdermarrja, gridFaturat, "grid_faturat", Komponente);

            KonfigurimComboGride.ShtoMonedhe(gridFaturat, IdNdermarrja, IdPerdoruesi, Session, Komponente, GuidString);
            KonfigurimComboGride.ShtoModelMeDataSource(gridFaturat, () =>
            {
                colKonfigurimAmbjenti konfigurimet = new colKonfigurimAmbjenti();
                konfigurimet.mbushKonfigAmbjSipasIdKategori(1, IdNdermarrja, IdPerdoruesi, IdGjuha);
                konfigurimet.mbushKonfigAmbjSipasIdKategori(2, IdNdermarrja, IdPerdoruesi, IdGjuha);
                return konfigurimet;
            }, Session, Komponente, GuidString);

            GridUtil.konfigGrideListeEMadhePaTheme(gridFaturat, "IdDokumenti");

            GridViewDataTextColumn col3 = gridFaturat.Columns["Vlefta"] as GridViewDataTextColumn;
            col3.PropertiesEdit.DisplayFormatString = "0.00";
            gridFaturat.Columns["#"].VisibleIndex = 0;
        }

        private void ShtoNivel()
        {
            ASPxGridView grid_faturat = (ASPxGridView)this.ASPxNavBar1.Groups[0].FindControl("grid_faturat");
            int visibleindex = grid_faturat.Columns["IdNiveli"].VisibleIndex;
            grid_faturat.Columns.Remove(grid_faturat.Columns["IdNiveli"]);
            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            DbCore.DbShare.colKonfigurimAmbjenti nivelet = new DbCore.DbShare.colKonfigurimAmbjenti();
            nivelet.Add(new DbCore.DbShare.clsKonfigurimAmbjenti(0, "", "", 0, 0, false, 0, 0, 0, 0, 1, 0, 0, 0, 1));
            if (Request.QueryString["lloji"] == "import")
                nivelet.mbushKonfigAmbjSipasIdKategori(2, IdNdermarrja, IdPerdoruesi, IdGjuha);
            else
                nivelet.mbushKonfigAmbjSipasIdKategori(1, IdNdermarrja, IdPerdoruesi, IdGjuha);
            colnew.PropertiesComboBox.DataSource = nivelet;
            colnew.PropertiesComboBox.TextField = "KodKonfigAmbjente";
            colnew.PropertiesComboBox.ValueField = "IdKonfigAmbjente";
            colnew.Caption = "Lloji";
            colnew.FieldName = "IdNiveli";
            colnew.VisibleIndex = visibleindex;
            grid_faturat.Columns.Add(colnew);

        }

        private void Shtokolona()
        {
            ASPxGridView gridFaturat = (ASPxGridView)ASPxNavBar1.Groups[0].FindControl("grid_faturat");

            if (gridFaturat.Columns["IdDokumenti"] == null)
                gridFaturat.Columns.Add(new GridViewDataTextColumn
                {
                    FieldName = "IdDokumenti",
                    VisibleIndex = 0
                });

            if (gridFaturat.Columns["IdKonfigAmbjente"] == null)
                gridFaturat.Columns.Add(new GridViewDataTextColumn
                {
                    FieldName = "IdKonfigAmbjente",
                    VisibleIndex = 2
                });

            if (gridFaturat.Columns["IdNiveli"] == null)
                gridFaturat.Columns.Add(new GridViewDataTextColumn
                {
                    FieldName = "IdNiveli",
                    VisibleIndex = 1
                });

            if (gridFaturat.Columns["NrDokumenti"] == null)
                gridFaturat.Columns.Add(new GridViewDataTextColumn
                {
                    FieldName = "NrDokumenti",
                    VisibleIndex = 3
                });

            if (gridFaturat.Columns["DtDokumenti"] == null)
                gridFaturat.Columns.Add(new GridViewDataDateColumn
                {
                    FieldName = "DtDokumenti",
                    VisibleIndex = 4
                });

            if (gridFaturat.Columns["IdKlientFurnitori"] == null)
                gridFaturat.Columns.Add(new GridViewDataTextColumn { FieldName = "IdKlientFurnitori" });

            if (gridFaturat.Columns["EmertimiKf"] == null)
                gridFaturat.Columns.Add(new GridViewDataTextColumn
                {
                    FieldName = "EmertimiKf",
                    VisibleIndex = 5
                });

            if (gridFaturat.Columns["IdMonedha"] == null)
                gridFaturat.Columns.Add(new GridViewDataTextColumn
                {
                    FieldName = "IdMonedha",
                    VisibleIndex = 6
                });

            if (gridFaturat.Columns["Vlefta"] == null)
                gridFaturat.Columns.Add(new GridViewDataTextColumn
                {
                    FieldName = "Vlefta",
                    VisibleIndex = 7
                });

            if (gridFaturat.Columns["Pershkrimi"] == null)
                gridFaturat.Columns.Add(new GridViewDataTextColumn { FieldName = "Pershkrimi" });

            if (gridFaturat.Columns["Status"] == null)
                gridFaturat.Columns.Add(new GridViewDataTextColumn { FieldName = "Status" });

        }

        /// <summary>
        /// I vendos nje emer identifikues kontrolleve te grides se dok kryesore dhe percakton funksinet qe manipulojne ato ne client side
        /// </summary>
        protected void grid_faturat_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {

        }

        protected void grid_faturat_DataBound(object sender, EventArgs e)
        {
            ASPxGridView gridFaturat = (ASPxGridView)ASPxNavBar1.Groups[0].FindControl("grid_faturat");

            if (gridFaturat.Columns["#"] != null) return;

            GridViewCommandColumn check = new GridViewCommandColumn("#")
            {
                ShowSelectCheckbox = true,
                Width = Unit.Percentage(2)
            };

            gridFaturat.Settings.ShowFilterRow = true;
            gridFaturat.Settings.ShowHeaderFilterButton = true;
            gridFaturat.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
            gridFaturat.Settings.ShowFilterRowMenu = true;
            gridFaturat.Columns.Add(check);
            gridFaturat.Settings.ShowGroupPanel = false;
            gridFaturat.KeyFieldName = "IdDokumenti";
            gridFaturat.SettingsBehavior.AllowSelectByRowClick = true;
            gridFaturat.SettingsBehavior.AllowFocusedRow = true;
            gridFaturat.Settings.ShowTitlePanel = false;
            gridFaturat.SettingsText.Title = MessagesResource.Messages["msgZgjidhniFaturat"];
        }

        protected void grid_faturat_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {

        }

        protected void grid_faturat_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e) =>
            e.Properties["cpNoRows"] = ((ASPxGridView)ASPxNavBar1.Groups[0].FindControl("grid_faturat")).VisibleRowCount;

        protected void grid_faturat_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {

        }

        protected void grid_faturat_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if ((e.Column.FieldName == "IdNiveli"
                 || e.Column.FieldName == "IdMonedha"
                 || e.Column.FieldName == "IdKlientFurnitori") && Converter.ConvertToInt(e.Value) == 0)
                e.Criteria = null;
        }

        #endregion

        /// <summary>
        /// Metode qe sherben per te kaluar perkthime te js
        /// </summary>
        /// <param name="ci">Merr culture info</param>
        /// <param name="rm">Merr Resource Manager</param>
        private void MbushHiddenFieldMePerkthime()
        {
            hfState.Set("MenuKokeDokumenti", rm.GetString("MenuKokeDokumenti", ci));
            hfState.Set("MenuTrupDokumenti", rm.GetString("MenuTrupDokumenti", ci));
            hfState.Set("MenuFundDokumenti", rm.GetString("MenuFundDokumenti", ci));
            hfState.Set("msgGabimGjateTransferimitTeTeDhenave", rm.GetString("msgGabimGjateTransferimitTeTeDhenave", ci));
            hfState.Set("msgKursiNukMundTeJeteZero", rm.GetString("msgKursiNukMundTeJeteZero", ci));
            hfState.Set("msgKursiNumerPozitiv", rm.GetString("msgKursiNumerPozitiv", ci));
            hfState.Set("msgShenoniNumrinEDokumentit", rm.GetString("msgShenoniNumrinEDokumentit", ci));
            hfState.Set("msgZgjidhniNjeDateDokumenti", rm.GetString("msgZgjidhniNjeDateDokumenti", ci));
            hfState.Set("msgZgjidhniNjeDateRegjstrimi", rm.GetString("msgZgjidhniNjeDateRegjstrimi", ci));
            hfState.Set("msgVleraTransportiNdryshme", rm.GetString("msgVleraTransportiNdryshme", ci));
            hfState.Set("msgSiguracionNdryshme", rm.GetString("msgSiguracionNdryshme", ci));
            hfState.Set("msgVleraTjeraNdryshme", rm.GetString("msgVleraTjeraNdryshme", ci));
            hfState.Set("msgVleraDoganeNdryshme", rm.GetString("msgVleraDoganeNdryshme", ci));
            hfState.Set("msgVleraTaksaNdryshme", rm.GetString("msgVleraTaksaNdryshme", ci));
            hfState.Set("msgVleraTransportNumer", rm.GetString("msgVleraTransportNumer", ci));
            hfState.Set("msgVleftaSiguracionNumer", rm.GetString("msgVleftaSiguracionNumer", ci));
            hfState.Set("msgVleftaTjeraNumer", rm.GetString("msgVleftaTjeraNumer", ci));
            hfState.Set("msgVleftaTaksaNumer", rm.GetString("msgVleftaTaksaNumer", ci));
            hfState.Set("msgGabimKursiMonedha", rm.GetString("msgGabimKursiMonedha", ci));
            hfState.Set("msgFaturaMonedhaNdryshme", rm.GetString("msgFaturaMonedhaNdryshme", ci));
            hfState.Set("msgZgjidhKurs", rm.GetString("msgZgjidhKurs", ci));
            hfState.Set("msgShperndarjeNeQendratEKostos", rm.GetString("msgShperndarjeNeQendratEKostos", ci));
            hfState.Set("msgZgjidhDokumentin", rm.GetString("msgZgjidhDokumentin", ci));
            hfState.Set("msgZgjidhniNjeFature", rm.GetString("msgZgjidhniNjeFature", ci));
            hfState.Set("msgDeshironiShperndarjeQendraKosto", rm.GetString("msgDeshironiTeBeniShperndarjenNeQendratEKostos", ci));
        }
    }
}