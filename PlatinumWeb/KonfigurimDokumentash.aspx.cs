using System;
using System.Data;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using DbCore;
using DbCore.DbAdmin;
using DbCore.DbArkaBanka;
using DbCore.DbInventari;
using DbCore.DbKontabiliteti;
using DbCore.DbRegjistrim;
using DbCore.DbShare;
using DbCore.IMBUtils.Extensions;
using DbCore.IMBUtils.Logging;
using DevExpress.Web;
using Newtonsoft.Json;
using PlatinumWeb.ApplicationUtils.Pages;
using PlatinumWeb.Templates;
using DbCore.IMBUtils.Messages;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;

//Ne kete ambient pervec konfigurimit te secilit dokument, regjistrimi ose jo, konfigurohen edhe lupat
//Nje lupe eshte nje faqe e vecante qe hapet si popup ne momentin e klikimit mbi nje kontroll te percaktuar
//Zakonisht nje lupe permban disa kontrolle qe sherbejne si filtra dhe nje gride qe sherben per listimin e te dhenave te filtruara
//Nje lupe celet si dokument, faqe. Pra ka nje kategori (Lapa me IDKATDOK = 9) dhe nivelin perkates qe percakton llojin e lupes
//Niveli ose lloji i lupes mund te jete psh. Kliente, Artikuj, Llogari, Magazina, Dokumenta etj
//Per secilin prej tyre pervec variantit default mund te celen edhe konfigurime te ndryshme per secilen lupe
//Keto konfigurime qe celen per lupat mund t'i caktohen kontrolleve qe hapin lupa ne cdo dokument tjeter
//Konfigurimet per lupat mund t'i caktohen kontrolleve normale te faqes ose kontrolleve (kolonave) te grides qe 
//hapin Lupa. Dmth ne grid_kontrolle dhe grid_trupi ka nje kolone ku zgjidhet lloji i lupes (T_AtributeTrupi dhe T_GridaTrupi)
// kjo fushe ka vlere -1 kur kontrolli (normal apo i grides) nuk kontrollon lupe (eshte label, textBox etj)
//ka vleren 1 kur kontrolli kontrollon disa lupa (shiko griden FS ku ne varesi te llojit Artilull, Makro, llogari etj, fusha Kodi kontrollon grida te ndryshme)
//ne kete rast vlerat e konfigurimit te lupes ruhen ne tabele tjeter dhe jo ne kolonen ne fjale (T_LupaMultiple)

namespace PlatinumWeb
{
    public partial class KonfigurimDokumentash : MyPageBase
    {
        private colKusht _colKush = new colKusht();
        private colAtributeTrupi _colAtribut = new colAtributeTrupi();
        private colGridaTrupi _oColTrupi = new colGridaTrupi();
        private readonly clsKonfigurimAmbjenti _konf = new clsKonfigurimAmbjenti();
        private int _idKomponente = -1;

        TextBox temptxtNormal;
        ASPxComboBox tempcombo;
        ASPxComboBox tempcomboTrupi;
        ASPxTextBox temptxt;
        ASPxTextBox temptxtTrupi;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!mySessionObjects.isLogedIn(Session))
                clsFunksione.logout(Session, true, "FaqePaautorizuar");

            if (mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + IdPerdoruesi);
            }
            if (!IsPostBack)
            {
                _colKush = new colKusht();
                _colAtribut = new colAtributeTrupi();
                _oColTrupi = new colGridaTrupi();
                ShtoVleraTePergjithshmeNeHfState();
                if (hfShtimModifikim.Value == "")
                {
                    if (string.IsNullOrEmpty(Request.QueryString["shtim_modifikim"]) ||
                        Request.QueryString["shtim_modifikim"] == "shtim")
                        hfShtimModifikim.Value = "shtim";
                    else if (Request.QueryString["shtim_modifikim"] == "modifikim")
                        hfShtimModifikim.Value = "modifikim";
                    else if (Request.QueryString["shtim_modifikim"] == "klonim")
                        hfShtimModifikim.Value = "klonim";
                }

                PercaktoTemplateMenu();
                mySessionObjects.ruajAtributetNeSession(Session, _colAtribut);
                mySessionObjects.ruajGridaTrupiNeSesion(Session, _oColTrupi);
                mySessionObjects.ruajKushteNeSesion(Session, _colKush);

                if (Kategoria_ComboBox.Value == null || Kategoria_ComboBox.Value.ToString() == "" ||
                    int.Parse(Kategoria_ComboBox.Value.ToString()) == 0 ||
                    int.Parse(Kategoria_ComboBox.Value.ToString()) == -1)
                    ConfigureAspxComboBox.mbushComboKonfigFormatiNumrash(IdNdermarrja, cmbFormatNumri);
                else
                    ConfigureAspxComboBox.mbushComboKonfigFormatiNumrashSipasKategorise(IdNdermarrja,
                        int.Parse(Kategoria_ComboBox.Value.ToString()), cmbFormatNumri);
                ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmbFormatNumri);
                ConfigureAspxComboBox.mbushLlojiCombo(Lloji_cmb);

                int idSuperKat;
                if (int.TryParse(Request.QueryString["idsuperkat"], out idSuperKat))
                    ConfigureAspxComboBox.mbushComboKategori(Kategoria_ComboBox, idSuperKat);

                if (Request.QueryString["id"] != null)
                {
                    _konf.mbushkonfigPaLloj(int.Parse(Request.QueryString["id"]));
                    _konf.mbushAutorizime();
                    MerrTedhenat(_konf);
                }

                MbushListeNivele();
                PerktheLabel();
                KonfiguroVleraFillestare(IdGjuha);
                GridUtil.percaktoVisibleColumnsMeWidth(IdGjuha, IdNdermarrja, grid_kushte, "grid_kushte", "KonfigurimDokumentash.aspx");

                var tedrejtaInfo = new clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, clsFunksione.GetKomponente(Page.Request));
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                //GridUtil.konfigGrideListeEMadhePaThemePerKonfigurim(grid_kontrollet, "IDKONTROLL");
                GridUtil.percaktoVisibleColumnsMeWidth(IdGjuha, IdNdermarrja, grid_kontrollet, "grid_kontrollet", "KonfigurimDokumentash.aspx");
                grid_kontrollet.GetFilteredSelectedValues();

            }
            else
            {
                PercaktoTemplateMenu();
                if (IsCallback && Request.Params["__CALLBACKID"].Contains("cmbBox"))
                {
                    KonfiguroGrideKontrollet();
                    KonfiguroGrideKushtet();
                }
                //GridUtil.konfigGrideListeEMadhePaThemePerKonfigurim(grid_kontrollet, "IDKONTROLL");


            }

            if (hfShtimModifikim.Value == "shtim")
                ASPxMenu1.Items[0].ClientEnabled = true;

        }
        public void PerktheLabel()
        {
            lblKategoria.Text = MessagesResource.Messages["lblKategori"];
            lblNenkategoria.Text = MessagesResource.Messages["lblNenkategoria"];
            lblAutorizimi.Text = MessagesResource.Messages["lblAutorizimi"];
            radhaLabel.Text = MessagesResource.Messages["lblRadha"];
            lblLloji.Text = MessagesResource.Messages["lblLloji"];
            kod_Label.Text = MessagesResource.Messages["lblKodKonfigurimi"];
            pershkrimi_Label.Text = MessagesResource.Messages["lblPershkrimiShqip"];
            pershkrimiEng_Label.Text = MessagesResource.Messages["lblPershkrimiEng"];
            lblFormatNumri.Text = MessagesResource.Messages["lblFormatNr"];
            ASPxLabel2.Text = MessagesResource.Messages["lblKontrollet"];
            ASPxLabel3.Text = MessagesResource.Messages["lblGrida"];
            ASPxLabel5.Text = MessagesResource.Messages["lblKushtet"];
            pershkrimiFr_Label.Text = MessagesResource.Messages["lblPershkrimiFrengjisht"];
        }

        /// <summary>
        /// funksioni qe mbush fushat
        /// </summary>
        /// <param name="IdGjuha"></param>
        /// <param name="l"></param>
        public void MerrTedhenat(clsKonfigurimAmbjenti l)
        {
            kodKonfig_TextBox.Text = l.KodKonfigAmbjente;
            pershkrimKonfig_TextBox.Text = l.PershkrimKonfigAmbjente;
            pershkrimKonfigEng_TextBox.Text = l.PershkrimKonfigAmbjenteEng;
            hfSkemaKontabelRegjistrime.Value = l.IdSkemeKontabel.ToString();
            Kategoria_ComboBox.Value = l.IdKategori.ToString();
            cmbNivelRegj.Value = l.IdNivel.ToString();
            cmbAutorizimiHf.Value = l.IdNivelAutorizimi;
            Lloji_cmb.Value = l.Lloji;
            pershkrimKonfigFr_TextBox.Text = l.PershkrimKonfigAmbjente_fr;

            if (l.IdKonfigFormatNr != 0)
            {
                var formati = new clsFormatiKonfig();
                formati.mbushFormatNrKonfigSipasId(l.IdKonfigFormatNr);
                cmbFormatNumri.Value = l.IdKonfigFormatNr;
                cmbFormatNumri.Text = formati.Kodi;
            }
            else
            {
                cmbFormatNumri.Value = 0;
                cmbFormatNumri.Text = "";
            }

            radhaTextBox.Text = Convert.ToString(l.Radha);
            var colAtr = new colAtributeTrupi();
            colAtr.mbushAtributetKontrolleveSipasKonfigurimit(l.IdKonfigAmbjente);
            _colAtribut = colAtr;
            grid_kontrollet.DataSource = colAtr;
            grid_kontrollet.DataBind();
            _oColTrupi = new colGridaTrupi(l.IdKonfigAmbjente, IdGjuha);
            grid_trupi.DataSource = _oColTrupi;
            grid_trupi.DataBind();
            var colKushte = new colKusht();
            colKushte.mbushGjitheKushteKonfigurimi(l.IdKonfigAmbjente);

            var gjdronsh = colKushte.Find(x => x.Kodi == "GJDRONSH");
            if (gjdronsh != null)
                hfState.Set("AlternativeGJDRONSH", clsAlternativaKushti.getAlternativa(l.IdKonfigAmbjente, "GJDRONSH"));

            var gjdv = colKushte.Find(x => x.Kodi == "GJDV");
            if (gjdv != null)
                hfState.Set("AlternativeGJDV", clsAlternativaKushti.getAlternativa(l.IdKonfigAmbjente, "GJDV"));

            var rbart = colKushte.Find(x => x.Kodi == "RBART");
            if (rbart != null)
                hfState.Set("AlternativeRBART", clsAlternativaKushti.getAlternativa(l.IdKonfigAmbjente, "RBART"));

            foreach (var t in colKushte)
            {
                var kodKushti = t.Kodi.Trim();

                if (kodKushti == "LLD")
                {
                    clsKonfLlojRreshti konfLlojRreshti;
                    if (_konf.IdKategori == 1 || _konf.IdKategori == 2)
                        konfLlojRreshti = new clsKonfLlojRreshti(t.IdKushtTemplate, "Shitje");
                    else
                        konfLlojRreshti = new clsKonfLlojRreshti(t.IdKushtTemplate, "ArkaBanka");
                    hfIdKushTemplate.Value = konfLlojRreshti.IdKushTemplate.ToString();

                    hfPrioriteti.Value = String.Join(",", konfLlojRreshti.ColKonfLlojRreshtiVlere.Select(x => x.IdLlojRreshti));// (",", (current, t1) => current + t1.IdLlojRreshti);
                }

                if ((kodKushti == "ZKDM" || kodKushti == "ZKDM2") && t.Vlera != 0)
                {
                    var konfmag = new clsKonfigurimAmbjenti();
                    konfmag.mbushKonfigAmbjSipasId(t.Vlera, IdGjuha, false);
                    konfmag.IdNdermarje = l.IdNdermarje;
                    konfmag.mbushKonfigAmbjSipasKod(konfmag.KodKonfigAmbjente, konfmag.IdNdermarje, false);
                    t.Vlera = konfmag.IdKonfigAmbjente;

                    if (kodKushti == "ZKDM")
                        hfMagazina.Value = t.Vlera.ToString();
                }

                if ((kodKushti == "ZKDSH" || kodKushti == "ZKDSHI" || kodKushti == "ZKDSHKLSP" || kodKushti == "ZDVFONE") && t.Vlera != 0)
                {
                    var konfmag = new clsKonfigurimAmbjenti();
                    konfmag.mbushKonfigAmbjSipasId(t.Vlera, IdGjuha, false);
                    konfmag.IdNdermarje = l.IdNdermarje;
                    konfmag.mbushKonfigAmbjSipasKod(konfmag.KodKonfigAmbjente, konfmag.IdNdermarje, false);
                    t.Vlera = konfmag.IdKonfigAmbjente;
                    hfShitja.Value = t.Vlera.ToString();
                }

                if (kodKushti == "ZKR" && t.Vlera != 0)
                {
                    var konfmag = new clsKonfigurimAmbjenti();
                    konfmag.mbushKonfigAmbjSipasId(t.Vlera, IdGjuha, false);
                    konfmag.IdNdermarje = l.IdNdermarje;
                    konfmag.mbushKonfigAmbjSipasKod(konfmag.KodKonfigAmbjente, konfmag.IdNdermarje, false);
                    t.Vlera = konfmag.IdKonfigAmbjente;
                    hfState.Set("IdKonfigRezervime", t.Vlera.ToString());
                }

                if (kodKushti == "ZFK" && t.Vlera != 0)
                {
                    var konfmag = new clsKonfigurimAmbjenti();
                    konfmag.mbushKonfigAmbjSipasId(t.Vlera, IdGjuha, false);
                    konfmag.IdNdermarje = l.IdNdermarje;
                    konfmag.mbushKonfigAmbjSipasKod(konfmag.KodKonfigAmbjente, konfmag.IdNdermarje, false);
                    t.Vlera = konfmag.IdKonfigAmbjente;
                    hfSkemaKontabelRegjistrime.Value = t.Vlera.ToString();
                }

                if (kodKushti == "ZRQK" && t.Vlera != 0)
                {
                    var konfmag = new clsKonfigurimAmbjenti();
                    konfmag.mbushKonfigAmbjSipasId(t.Vlera, IdGjuha, false);
                    konfmag.IdNdermarje = l.IdNdermarje;
                    konfmag.mbushKonfigAmbjSipasKod(konfmag.KodKonfigAmbjente, konfmag.IdNdermarje, false);
                    t.Vlera = konfmag.IdKonfigAmbjente;
                    hfQK.Value = t.Vlera.ToString();
                }

                if (kodKushti == "DL" && t.Vlera != 0)
                {
                    var konfmag = new clsKonfigurimAmbjenti();
                    konfmag.mbushKonfigAmbjSipasId(t.Vlera, IdGjuha, false);
                    konfmag.IdNdermarje = l.IdNdermarje;
                    konfmag.mbushKonfigAmbjSipasKod(konfmag.KodKonfigAmbjente, konfmag.IdNdermarje, false);
                    t.Vlera = konfmag.IdKonfigAmbjente;
                    hfMagazina.Value = t.Vlera.ToString();
                }

                if (kodKushti == "ZDAM" && t.Vlera != 0)
                {
                    var konfmag = new clsKonfigurimAmbjenti();
                    konfmag.mbushKonfigAmbjSipasId(t.Vlera, IdGjuha, false);
                    konfmag.IdNdermarje = l.IdNdermarje;
                    konfmag.mbushKonfigAmbjSipasKod(konfmag.KodKonfigAmbjente, konfmag.IdNdermarje, false);
                    t.Vlera = konfmag.IdKonfigAmbjente;
                    hfAmortizimi.Value = t.Vlera.ToString();
                }

                if (kodKushti == "ZFH" && t.Vlera != 0)
                {
                    var konfmag = new clsKonfigurimAmbjenti();
                    konfmag.mbushKonfigAmbjSipasId(t.Vlera, IdGjuha, false);
                    konfmag.IdNdermarje = l.IdNdermarje;
                    konfmag.mbushKonfigAmbjSipasKod(konfmag.KodKonfigAmbjente, konfmag.IdNdermarje, false);
                    t.Vlera = konfmag.IdKonfigAmbjente;
                    hfVleraFleteHyrje.Value = t.Vlera.ToString();
                }

                if (kodKushti == "ZKDA" && t.Vlera != 0)
                {
                    var konfark = new clsKonfigurimAmbjenti();
                    konfark.mbushKonfigAmbjSipasId(t.Vlera, IdGjuha, false);
                    konfark.IdNdermarje = l.IdNdermarje;
                    konfark.mbushKonfigAmbjSipasKod(konfark.KodKonfigAmbjente, konfark.IdNdermarje, false);
                    t.Vlera = konfark.IdKonfigAmbjente;
                    hfArketim.Value = t.Vlera.ToString();
                }

                if (kodKushti == "ZKDPAG" && t.Vlera != 0)
                {
                    var konfpag = new clsKonfigurimAmbjenti();
                    konfpag.mbushKonfigAmbjSipasId(t.Vlera, IdGjuha, false);
                    konfpag.IdNdermarje = l.IdNdermarje;
                    konfpag.mbushKonfigAmbjSipasKod(konfpag.KodKonfigAmbjente, konfpag.IdNdermarje, false);
                    t.Vlera = konfpag.IdKonfigAmbjente;
                    hfPagese.Value = t.Vlera.ToString();
                }

                if (kodKushti == "ZKDB" && t.Vlera != 0)
                {
                    var konfbler = new clsKonfigurimAmbjenti();
                    konfbler.mbushKonfigAmbjSipasId(t.Vlera, IdGjuha, false);
                    konfbler.IdNdermarje = l.IdNdermarje;
                    konfbler.mbushKonfigAmbjSipasKod(konfbler.KodKonfigAmbjente, konfbler.IdNdermarje, false);
                    t.Vlera = konfbler.IdKonfigAmbjente;
                    hfBlere.Value = t.Vlera.ToString();
                }
            }

            _colKush = colKushte;
            grid_kushte.DataSource = colKushte;
            grid_kushte.DataBind();
            mySessionObjects.ruajAtributetNeSession(Session, _colAtribut);
            mySessionObjects.ruajGridaTrupiNeSesion(Session, _oColTrupi);
            mySessionObjects.ruajKushteNeSesion(Session, _colKush);

            if (l.DefaultKonfigAmbjente && colRolPerdorues.merrRoleSipasPerdoruesiDheKodRoli(IdPerdoruesi, "RSU") != -1)
                ASPxMenu1.Items[0].ClientEnabled = false;
        }

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        private void PercaktoTemplateMenu() =>
            clsToolbarConfig.percaktoTemplateMenu(IdGjuha, IdViti, IdPerdoruesi, IdNdermarrja, ASPxMenu1, clsFunksione.GetKomponente(Page.Request), this, MenuInfo, hfShtimModifikim.Value != "modifikim", true, false, Meme);

        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e) => PercaktoTemplateMenu();

        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {
            if (e.Item.Name == "Ruaj")
            {
                Page.Validate();
                Ruaj();
            }
        }

        private void KonfiguroVleraFillestare(int idGjuha)
        {
            MbushGrideKontrollet();
            MbushGrideTrupi();
            MbushGrideKushte(idGjuha);
            KonfiguroGrideTrupi();
            KonfiguroGrideKushtet();
            KonfiguroGrideKontrollet();
            mySessionObjects.hiqObjectNeSesion(Session, "NrAutomatik");
            hfState.Set("colAutorizime", JsonConvert.SerializeObject(new colAutorizimetKoka(IdPerdoruesi)));
        }

        #region gridakontrolle

        private void KonfiguroGrideKontrollet()
        {
            ShtoKod();
            PercaktoTemplate();
            GridUtil.konfiguroGrideRegjistrimEvogelPaTheme(grid_kontrollet, "IdKontroll", false);
            grid_kontrollet.Columns["IdKonfigAmbjente"].Visible = false;
            grid_kontrollet.Columns["IdKonfigAmbjenteLupa"].Visible = false;
            grid_kontrollet.Columns["KodLupa"].Caption = "Lupa";
            grid_kontrollet.SettingsPager.Mode = GridViewPagerMode.ShowPager; 
            grid_kontrollet.SettingsPager.PageSize = 50;
            grid_kontrollet.SettingsBehavior.AllowSort = false;
        }

        private void ShtoKod()
        {
            var col = new colKontrolle();
            var oKatDok = new clsKategoriNivelDok();
            if (Kategoria_ComboBox.SelectedItem != null)
            {
                string idKatDok;
                if (hfShtimModifikim.Value == "modifikim" || hfShtimModifikim.Value == "klonim")
                {
                    var konf = new clsKonfigurimAmbjenti();
                    konf.mbushkonfigPaLloj(int.Parse(Request.QueryString["id"]));
                    idKatDok = konf.IdKategori.ToString();
                }
                else
                    idKatDok = Kategoria_ComboBox.SelectedItem.Value.ToString();

                oKatDok.IdKategori = int.Parse(idKatDok);
                oKatDok = oKatDok.merrSipasId();

                if (cmbNivelRegj.SelectedItem != null)
                {
                    var oNivel = new clsNivelRegjistrimi();
                    oNivel.mbushNivelRegjistrimiSipasIdPaKonvertime(Convert.ToInt32(cmbNivelRegj.SelectedItem.Value));
                    if (oNivel.IdNivel > 0)
                    {
                        oKatDok.IdKategori = oNivel.IdKategori;
                        oKatDok = oKatDok.merrSipasId();
                    }
                    col.merrKontrolletKomponentes(oKatDok.IdKategori != 9 ? oKatDok.IdKomponente : oNivel.Radha);
                }
                else
                    col.merrKontrolletKomponentes(oKatDok.IdKomponente);
            }
            else
                col.merrKontrolletKomponentes(-1);

            grid_kontrollet.Columns.Remove(grid_kontrollet.Columns["IdKontroll"]);
            var colnew = new GridViewDataComboBoxColumn();
            colnew.PropertiesComboBox.DataSource = col;
            colnew.PropertiesComboBox.TextField = "KodKontrolli";
            colnew.PropertiesComboBox.ValueField = "IdKontrolli";
            colnew.FieldName = "IdKontroll";
            colnew.Caption = "Kodi";
            colnew.VisibleIndex = 0;
            grid_kontrollet.Columns.Add(colnew);
        }

        private void MbushGrideKontrollet()
        {
            _colAtribut = new colAtributeTrupi();
            if (hfShtimModifikim.Value == "modifikim" || hfShtimModifikim.Value == "klonim")
            {
                _colAtribut.mbushAtributetKontrolleveSipasKonfigurimit(int.Parse(Request.QueryString["id"]));
                grid_kontrollet.DataSource = _colAtribut;
            }
            else
            {
                if (Lloji_cmb.Value == null || Lloji_cmb.Value.ToString() == "0")
                    Lloji_cmb.Value = 1;//default alphaweb
                if (Kategoria_ComboBox.SelectedItem == null || Kategoria_ComboBox.SelectedIndex == -1)//kur jane bosh
                {
                    _colAtribut.mbushAtributetKontrolleveKomponentes(-1, -1);
                    grid_kontrollet.DataSource = _colAtribut;
                }
                else
                {
                    var idKategori = Kategoria_ComboBox.SelectedItem.Value.ToString();
                    var idNivel = cmbNivelRegj.SelectedItem != null
                        ? Convert.ToInt32(cmbNivelRegj.SelectedItem.Value.ToString())
                        : -1;

                    var oNivel = new clsNivelRegjistrimi();
                    oNivel.mbushNivelRegjistrimiSipasIdPaKonvertime(idNivel);

                    var oKonfig = new clsKonfigurimAmbjenti
                    {
                        IdNivel = idNivel,
                        IdKategori = Convert.ToInt32(idKategori)
                    };

                    if (oKonfig.IdKategori == 0)
                        oKonfig.IdNivel = -1;

                    oKonfig = oKonfig.merrSipasIdKategoriIdNivel(IdPerdoruesi, false);

                    var oKategori = new clsKategoriNivelDok { IdKategori = Convert.ToInt32(idKategori) };
                    oKategori = oKategori.merrSipasId();
                    if (oKategori != null && oKategori.IdKategori != 9) //kategori e ndryshme nga lupat
                    {
                        _idKomponente = oKategori.IdKomponente;
                        if (_idKomponente == 508)
                            _idKomponente = 506;
                        _colAtribut.mbushAtributetKontrolleveSipasKonfigurimit(oKonfig.IdKonfigAmbjente);
                        grid_kontrollet.DataSource = _colAtribut;
                    }
                    else if (oKategori.IdKategori == 9)//lupat
                    {
                        _idKomponente = oNivel.Radha;
                        _colAtribut.mbushAtributetKontrolleveSipasKonfigurimit(oKonfig.IdKonfigAmbjente);
                        grid_kontrollet.DataSource = _colAtribut;
                    }
                }
            }

            mySessionObjects.ruajAtributetNeSession(Session, _colAtribut);
            grid_kontrollet.DataBind();
            //grid_kontrollet.FilterExpression
            hfVleratFillestareKontrollet.Value = JsonConvert.SerializeObject(_colAtribut);
        }

        private void PercaktoTemplate()
        {
            GridViewDataTextColumn col3 = grid_kontrollet.Columns["VlereDefault"] as GridViewDataTextColumn;
            GridViewDataTextColumn col5 = grid_kontrollet.Columns["VlereDefaultEng"] as GridViewDataTextColumn;
            GridViewDataTextColumn col19 = grid_kontrollet.Columns["VlereDefault_fr"] as GridViewDataTextColumn;

            if (grid_kontrollet.PageIndex * 50 < _colAtribut.Count)
            {
                if (EshteVleraDefaultCombo(_colAtribut[grid_kontrollet.PageIndex * 50].PershkrimKontroll))
                {
                    col3.DataItemTemplate = new MyComboTemplate();
                    col5.DataItemTemplate = new MyComboTemplate();
                    col19.DataItemTemplate = new MyComboTemplate();
                }
                else
                {
                    col3.DataItemTemplate = new MyTextTemplate();
                    col5.DataItemTemplate = new MyTextTemplate();
                    col19.DataItemTemplate = new MyTextTemplate();
                }
            }
            GridViewDataCheckColumn col4 = grid_kontrollet.Columns["Visible"] as GridViewDataCheckColumn;
            col4.DataItemTemplate = new MyComboTemplate();
            GridViewDataCheckColumn col9 = grid_kontrollet.Columns["Enabled"] as GridViewDataCheckColumn;
            col9.DataItemTemplate = new MyComboTemplate();
            GridViewDataTextColumn col1 = grid_kontrollet.Columns["Identifikues"] as GridViewDataTextColumn;
            col1.DataItemTemplate = new MyComboTemplate();
            GridViewDataCheckColumn col13 = grid_kontrollet.Columns["Detyrueshme"] as GridViewDataCheckColumn;
            col13.DataItemTemplate = new MyComboTemplate();
            GridViewDataTextColumn col110 = grid_kontrollet.Columns["KodLupa"] as GridViewDataTextColumn;
            col110.DataItemTemplate = new MyTemplateLupa();
            col110.Width = 200;
            GridViewDataTextColumn col11 = grid_kontrollet.Columns["Rreshti"] as GridViewDataTextColumn;
            col11.DataItemTemplate = new MyIntTemplate(false);
            GridViewDataTextColumn col12 = grid_kontrollet.Columns["Kolona"] as GridViewDataTextColumn;
            col12.DataItemTemplate = new MyIntTemplate(false);
            GridViewDataTextColumn col18 = grid_kontrollet.Columns["IdNrAutomatik"] as GridViewDataTextColumn;
            col18.DataItemTemplate = new MyComboTemplate();
            GridViewDataCheckColumn col20 = grid_kontrollet.Columns["ShfaqMobile"] as GridViewDataCheckColumn;
            col20.DataItemTemplate = new MyComboTemplate();
            GridViewDataTextColumn col21 = grid_kontrollet.Columns["RenditjaMobile"] as GridViewDataTextColumn;
            col21.DataItemTemplate = new MyTextTemplate();
            GridViewDataCheckColumn col22 = grid_kontrollet.Columns["Unike"] as GridViewDataCheckColumn;
            col22.DataItemTemplate = new MyComboTemplate();
        }

        private static bool EshteVleraDefaultCombo(string pershkrimKontrolli)
        {
            return pershkrimKontrolli.EqualsAnyIgnoreCase("caktimi i monedhes",
                "caktimi i kursit"
                , "caktimit i subjektit", "caktimi i menyres se pageses"
                , "caktimi i menyres se transportit", "caktimi i  kushtit te dergimit"
                , "caktimi i agjentit", "caktimi i maturimit"
                , "caktimi i kushtit te pageses", "caktimi i subjektit", "caktimi i autorizimit"
                , "caktimi i metodes", "caktimi i magazines destinacion"
                , "caktimi i skemes", "caktimi i deges administrative", "caktimi i magazines", "caktimi i pikes se shitjes furnizimit", "caktimi i bankes", "caktimi i formatit te printimit", "caktimi i grupimit 1 te k/f", "caktimi i kursit"
                , "caktimi i kursit te pageses"
                , "caktimi i njesise se prodhimit"
                , "caktimi i llojit te layerit"
                , "caktimi i transportuesit"
                , "caktimi i llog"
                , "caktimi i llogarise dytesore"
                , "caktimi i metodes ftp"
                , "caktimi i kases"
                , "caktimi i formatit te printimit te mobile"
                ) || (pershkrimKontrolli.ContainsAnyIgnoreCase("caktimi i llogarise") && !pershkrimKontrolli.EqualsIgnoreCase("caktimi i llogarise bankare"));
        }

        protected void grid_kontrollet_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            _colAtribut = mySessionObjects.merrAtributetNgaSessioni(Session);
            if (e.RowType == GridViewRowType.Data)
            {
                GridViewDataColumn col3 = ((ASPxGridView)sender).Columns["VlereDefault"] as GridViewDataColumn;
                GridViewDataCheckColumn col4 = ((ASPxGridView)sender).Columns["Visible"] as GridViewDataCheckColumn;
                GridViewDataCheckColumn col9 = ((ASPxGridView)sender).Columns["Enabled"] as GridViewDataCheckColumn;
                GridViewDataColumn col1 = ((ASPxGridView)sender).Columns["Identifikues"] as GridViewDataColumn;
                GridViewDataColumn col13 = ((ASPxGridView)sender).Columns["Detyrueshme"] as GridViewDataColumn;
                GridViewDataColumn col110 = ((ASPxGridView)sender).Columns["KodLupa"] as GridViewDataColumn;
                GridViewDataColumn col11 = ((ASPxGridView)sender).Columns["Rreshti"] as GridViewDataColumn;
                GridViewDataColumn col12 = ((ASPxGridView)sender).Columns["Kolona"] as GridViewDataColumn;
                GridViewDataColumn col14 = ((ASPxGridView)sender).Columns["IdNrAutomatik"] as GridViewDataColumn;
                GridViewDataColumn col15 = ((ASPxGridView)sender).Columns["VlereDefaultEng"] as GridViewDataColumn;
                GridViewDataColumn col19 = ((ASPxGridView)sender).Columns["VlereDefault_fr"] as GridViewDataColumn;
                GridViewDataCheckColumn col20 = ((ASPxGridView)sender).Columns["ShfaqMobile"] as GridViewDataCheckColumn;
                GridViewDataColumn col21 = ((ASPxGridView)sender).Columns["RenditjaMobile"] as GridViewDataColumn;
                GridViewDataCheckColumn col22 = ((ASPxGridView)sender).Columns["Unike"] as GridViewDataCheckColumn;
                ASPxComboBox cmb1 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col4, "cmbBox") as ASPxComboBox;
                ASPxComboBox cmb2 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col9, "cmbBox") as ASPxComboBox;
                ASPxComboBox cmb5 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col1, "cmbBox") as ASPxComboBox;
                ASPxComboBox cmb6 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col13, "cmbBox") as ASPxComboBox;
                TextBox txt110 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col110, "txt") as TextBox;
                ASPxButton btn110 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col110, "btn") as ASPxButton;
                ASPxTextBox txt11 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col11, "txtBox") as ASPxTextBox;
                ASPxTextBox txt12 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col12, "txtBox") as ASPxTextBox;
                ASPxComboBox cmb14 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col14, "cmbBox") as ASPxComboBox;
                ASPxComboBox cmb16 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col20, "cmbBox") as ASPxComboBox;
                ASPxTextBox txt17 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col21, "txtBox") as ASPxTextBox;
                ASPxComboBox cmb18 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col22, "cmbBox") as ASPxComboBox;

                var ugjet = false;
                if (_colAtribut.Count > e.VisibleIndex)
                {

                    string txtVleraDefault = IdGjuha == 0 ? "txtVleraDefault" : IdGjuha == 1 ? "txtVleraDefaultEng" : "txtVleraDefault_fr";
                    var pershkrimiKontrolli = _colAtribut[e.VisibleIndex].PershkrimKontroll.ToLower();
                    if (EshteVleraDefaultCombo(pershkrimiKontrolli))
                    {
                        var combo = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, IdGjuha == 0 ? col3 : IdGjuha == 1 ? col15 : col19, "cmbBox") as ASPxComboBox;
               
                        if (combo != null)
                        {
                            switch (pershkrimiKontrolli.Trim())
                            {
                                case "caktimi i formatit te printimit":
                                    int idkat = MerrIdKategorie();
                                    colRaporteDesign col = new colRaporteDesign();
                                    col.merrSipasKategorise(idkat, IdNdermarrja);
                                    combo.DataSource = col;
                                    combo.TextField = "Pershkrim";
                                    combo.ValueField = "IdRaportDesign";
                                    combo.DataBind();
                                    break;

                                case "caktimi i formatit te printimit te mobile":
                                   int  idkatMob = MerrIdKategorie();
                                    combo.DataSource = clsRaporti.merrSipasKategorisePerMobile(idkatMob, IdNdermarrja);
                                    combo.TextField = "Kodi";
                                    combo.ValueField = "IDAUTO";
                                    combo.DataBind();
                                    break;

                                case "caktimi i magazines":
                                    ConfigureAspxComboBox.percaktoTemplateComboMeLupe(combo);
                                    DataTable dtn = colNjesiAdministrative.merrSipasNjesiNdermarjePerLupeDege(IdNdermarrja, IdPerdoruesi, false, -1, true);
                                    combo.DataSource = dtn;
                                    combo.ValueField = "IdNjesiAdministrative";
                                    combo.TextField = "Kodi";
                                    combo.DataBind();
                                    dtn.Dispose();
                                    combo.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedNJA(" + txtVleraDefault + e.VisibleIndex + "," + e.VisibleIndex + "); }";
                                    break;

                                case "caktimi i skemes":
                                    ConfigureAspxComboBox.percaktoTemplateComboMeLupe(combo);
                                    colSkematKontabilitetiArtikulli colsk = new colSkematKontabilitetiArtikulli(IdNdermarrja);
                                    bool afatgjate = cmbNivelRegj.Text == "Artikuj Afatgjate";
                                    ConfigureAspxComboBox.shtoKolonaSkemaArtikulli(combo, afatgjate, IdNdermarrja);
                                    combo.DataSource = colsk;
                                    combo.ValueField = "IdSkemaKontabilitetiArtikulli";
                                    combo.TextField = "KodiSkemaKontabilitetiArtikulli";
                                    combo.DataBind();
                                    combo.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedSKA(" + txtVleraDefault + e.VisibleIndex + "," + e.VisibleIndex + "); }";
                                    combo.ClientSideEvents.TextChanged = "function(s,e){TextChangedVleraSKA(" + txtVleraDefault + e.VisibleIndex + ",'Vlera Default'," + e.VisibleIndex + ");}";
                                    break;

                                case "caktimi i metodes":
                                    colMetodeKostoje metode = new colMetodeKostoje();
                                    metode.mbushGjitheMetodeKostoje();
                                    combo.DataSource = metode;
                                    combo.ValueField = "IdMetodeKostoje";
                                    combo.TextField = "Kodi";
                                    combo.DataBind();
                                    if (combo.Text == "")
                                        combo.SelectedIndex = 0;
                                    break;

                                case "caktimi i llogarise":
                                case "caktimi i llogarise kredit":
                                case "caktimi i llogarise debi":
                                case "caktimi i llogarise kredi":
                                case "caktimi i llogarise se inventarit":
                                case "caktimi i llogarise se blerjes":
                                case "caktimi i llogarise se shitjes":
                                case "caktimi i llogarise se shpenzimeve":
                                case "caktimi i llogarise konsoliduese":
                                case "caktimi i llogarise korresponduese":
                                case "caktimi i llogarise kunderparti":
                                case "caktimi i llogarise se fitimit":
                                case "caktimi i llogarise se humbjes":
                                case "caktimi i llogarise se mbylljes se vitit":
                                case "caktimi i llogarise se doganes":
                                case "caktimi i llogarise se amortizimit":
                                case "caktimi i llogarise se pakesimit vlere dalje":
                                case "caktimi i llogarise dytesore":
                                case "caktimi i llogarise me te tretet":
                                case "caktimi i llog":
                                case "caktimi i llogarise se komisionit":

                                    ConfigureAspxComboBox.percaktoTemplateComboMeLupe(combo);
                                    DataTable dtl = colLlogarite.merrSipasLlogariteNdermarrjesAndAutorizimeDT(IdNdermarrja, IdPerdoruesi, IdGjuha);
                                    combo.DataSource = dtl;
                                    combo.ValueField = "IdLlogari";
                                    combo.TextField = "NrLlogari";
                                    combo.DataBind();
                                    combo.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedLL(" + txtVleraDefault + e.VisibleIndex + "," + e.VisibleIndex + "); }";
                                    dtl.Dispose();
                                    break;

                                case "caktimi i deges administrative":
                                    ConfigureAspxComboBox.percaktoTemplateComboMeLupe(combo);
                                    colDegeAdministrative colDege = new colDegeAdministrative();
                                    colDege.mbushGjitheDegeAdministrativeAktivePerKonfigurim(IdNdermarrja);
                                    combo.DataSource = colDege;
                                    combo.TextField = "Kodi";
                                    combo.ValueField = "IdDegeAdministrative";
                                    combo.DataBind();
                                    combo.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedDege(" + txtVleraDefault + e.VisibleIndex + "," + e.VisibleIndex + "); }";
                                    break;

                                case "caktimi i autorizimit":
                                    ConfigureAspxComboBox.percaktoTemplateComboMeLupe(combo);
                                    colAutorizimetKoka colAutorizim = new colAutorizimetKoka(IdPerdoruesi);
                                    combo.DataSource = colAutorizim;
                                    combo.TextField = "KodiAutorizim";
                                    combo.ValueField = "KodiAutorizim";
                                    combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                                    combo.DataBind();
                                    combo.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedAutorizime('txtVleraDefault" + e.VisibleIndex + "'," + e.VisibleIndex + "); }";
                                    break;

                                case "caktimi i grupimit 1 te k/f":
                                    ConfigureAspxComboBox.percaktoTemplateComboMeLupe(combo);
                                    colGrupeKF grupet = new colGrupeKF();
                                    if (cmbNivelRegj.Text.IndexOf("Klient") != -1)
                                        grupet.MbushGjitheGrupetKfSipasNdermarrjesJoPrindDheLlojit(IdNdermarrja, 1, false);
                                    else if (cmbNivelRegj.Text.IndexOf("Furnitor") != -1)
                                        grupet.MbushGjitheGrupetKfSipasNdermarrjesJoPrindDheLlojit(IdNdermarrja, 1, true);
                                    combo.DataSource = grupet;
                                    combo.TextField = "KodGrupi";
                                    combo.ValueField = "KodGrupi";
                                    combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                                    combo.DataBind();
                                    combo.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedGrupimKlienti('txtVleraDefault" + e.VisibleIndex + "'," + e.VisibleIndex + "); }";
                                    break;

                                case "caktimi i njesise se prodhimit":
                                    ConfigureAspxComboBox.percaktoTemplateComboMeLupe(combo);
                                    DataTable dt = colNjesiProdhimi.merrNjesiProdhimiSipasNdermarrjeAktiveDt(IdNdermarrja);
                                    combo.DataSource = dt;
                                    combo.ValueField = "IdNjesiProdhimi";
                                    combo.TextField = "Kodi";
                                    combo.DataBind();
                                    combo.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedNjesiProdhimi('txtVleraDefault" + e.VisibleIndex + "'," + e.VisibleIndex + "); }";
                                    break;

                                case "caktimi i llojit te layerit":
                                    //DbCore.clsFunksione.percaktoTemplateCombo(combo);
                                    ConfigureAspxComboBox.mbushComboLlojLayerMagazine(combo);
                                    //combo.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedNjesiProdhimi('txtVleraDefault" + e.VisibleIndex.ToString() + "'," + e.VisibleIndex.ToString() + "); }";
                                    break;

                                case "caktimi i kursit te pageses":
                                    ConfigureAspxComboBox.mbushComboLlojeKursi(combo);
                                    combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                                    break;

                                case "caktimi i monedhes":
                                    DataTable dt1 = colMonedhat.merrMonedhaNdermarjeDTAktiv(IdNdermarrja, IdPerdoruesi);
                                    combo.DataSource = dt1;
                                    combo.TextField = "KodiMonedha";
                                    combo.ValueField = "IdMonedha";
                                    combo.DataBind();
                                    combo.ClientSideEvents.SelectedIndexChanged = "function(s,e){SelectedIndexChanged(" + txtVleraDefault + e.VisibleIndex + ",'Vlera Default'," + e.VisibleIndex + ");}";
                                    dt1.Dispose();

                                    break;

                                case "caktimi i kursit":
                                    int monedhaDefault = MerrMonedhenDefault();
                                    if (monedhaDefault == 0)
                                    {
                                        combo.Items.Clear();
                                        combo.Items.Add("Kursi1", "1");
                                        combo.Items.Add("Kursi2", "2");
                                        combo.Items.Add("Kursi3", "3");
                                        combo.Items.Add("Kursi4", "4");
                                        combo.Items.Add("Kursi5", "5");
                                        combo.Items.Add("Kursi6", "6");
                                        combo.Items.Add("Kursi7", "7");
                                        combo.Items.Add("Kursi8", "8");
                                        combo.Items.Add("Kursi9", "9");
                                        combo.Items.Add("Kursi10", "10");
                                        combo.Items.Add("Kursi11", "11");
                                        combo.Items.Add("Kursi12", "12");
                                        combo.Items.Add("Kursi13", "13");
                                        combo.Items.Add("Kursi14", "14");
                                        combo.Items.Add("Kursi15", "15");
                                        combo.Items.Add("Kursi16", "16");
                                        combo.Items.Add("Kursi17", "17");
                                        combo.Items.Add("Kursi18", "18");
                                        combo.Items.Add("Kursi19", "19");
                                        combo.Items.Add("Kursi20", "20");
                                    }
                                    else
                                    {
                                        combo.Items.Clear();
                                        colKurset col2 = new colKurset();
                                        col2.mbushKursetMonedhes(monedhaDefault);
                                        foreach (clsKurset k in col2)
                                            combo.Items.Add(k.PershkrimLlojKursi, k.LlojKursi);
                                        for (int i = col2.Count + 1; i <= 20; i++)
                                            combo.Items.Add("Kursi" + i, i.ToString());
                                    }
                                    break;

                                case "caktimit i subjektit":
                                case "caktimi i subjektit":
                                case "Caktimi i klientit/furnitorit vartes":
                                    var vlDefault = ((ASPxGridView)sender).GetRowValuesByKeyValue(e.KeyValue, "VlereDefault");
                                    int id = 0;
                                    if (vlDefault != null)
                                        int.TryParse(vlDefault.ToString(), out id);
                                    ConfigureAspxComboBox.percaktoTemplateComboMeLupe(combo);
                                    ConfigureAspxComboBox.KonfiguroComboBoxKlientFurnitoriById(combo, id, true);
                                    combo.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedKF(" + txtVleraDefault + e.VisibleIndex + "," + e.VisibleIndex + "); }";
                                    combo.ClientSideEvents.TextChanged = "function(s,e){ CmbTextChanged(" + txtVleraDefault + e.VisibleIndex + "," + e.VisibleIndex + "); }";
                                    break;

                                case "caktimi i transportuesit":
                                    ConfigureAspxComboBox.percaktoTemplateComboMeLupe(combo);
                                    ConfigureAspxComboBox.mbushComboTransportues(IdNdermarrja, combo);
                                    combo.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedTransportues(" + txtVleraDefault + e.VisibleIndex + "," + e.VisibleIndex + "); }"; combo.ClientSideEvents.TextChanged = "function(s,e){ CmbTextChanged(" + txtVleraDefault + e.VisibleIndex + "," + e.VisibleIndex + "); }";
                                    break;

                                case "caktimi i bankes":
                                    ConfigureAspxComboBox.percaktoTemplateComboMeLupe(combo);
                                    DataTable dtb;
                                    if (Kategoria_ComboBox.Text == "Arka" || Kategoria_ComboBox.Text == "Veprime Arke")
                                        dtb = colBankat.ktheGjitheBankatSipasAutorizimeveSipasLlojit(IdNdermarrja, IdPerdoruesi, false, false);
                                    else
                                        dtb = colBankat.ktheGjitheBankatSipasAutorizimeveSipasLlojit(IdNdermarrja, IdPerdoruesi, true, false);
                                    combo.DataSource = dtb;
                                    combo.ValueField = "IdBanka";
                                    combo.TextField = "KodiBanka";
                                    combo.DataBind();
                                    combo.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedBanka(" + txtVleraDefault + e.VisibleIndex + "," + e.VisibleIndex + "); }";
                                    dtb.Dispose();
                                    break;

                                case "caktimi i menyres se transportit":
                                    ConfigureAspxComboBox.percaktoTemplateComboMeLupe(combo);
                                    colMenyraTransporti colMen = new colMenyraTransporti(IdNdermarrja);
                                    combo.DataSource = colMen;
                                    combo.ValueField = "IdMenyreTransporti";
                                    combo.TextField = "KodiMenyreTransporti";
                                    combo.DataBind();
                                    combo.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedMT(" + txtVleraDefault + e.VisibleIndex + "," + e.VisibleIndex + "); }";
                                    break;

                                case "caktimi i  kushtit te dergimit":
                                    ConfigureAspxComboBox.percaktoTemplateComboMeLupe(combo);
                                    colKushteDergimi colKushDer = new colKushteDergimi(IdNdermarrja);
                                    combo.DataSource = colKushDer;
                                    combo.ValueField = "IdKushtDergimi";
                                    combo.DataBind();
                                    combo.TextField = "KodiKushtDergimi";
                                    combo.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedKD(" + txtVleraDefault + e.VisibleIndex + "," + e.VisibleIndex + "); }";
                                    break;

                                case "caktimi i agjentit":
                                    ConfigureAspxComboBox.percaktoTemplateComboMeLupe(combo);
                                    colAgjenteShitje colAgjent = new colAgjenteShitje(IdNdermarrja);
                                    combo.DataSource = colAgjent;
                                    combo.ValueField = "IdAgjentShitje";
                                    combo.TextField = "KodiAgjentShitje";
                                    combo.DataBind();
                                    combo.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedAGJ(" + txtVleraDefault + e.VisibleIndex + "," + e.VisibleIndex + "); }";
                                    break;

                                case "caktimi i maturimit":
                                    ConfigureAspxComboBox.percaktoTemplateComboMeLupe(combo);
                                    colMaturimet colMat = new colMaturimet(IdNdermarrja);
                                    combo.DataSource = colMat;
                                    combo.ValueField = "IdMaturimi";
                                    combo.TextField = "KodMaturimi";
                                    combo.DataBind();
                                    combo.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedM(" + txtVleraDefault + e.VisibleIndex + "," + e.VisibleIndex + "); }";
                                    break;

                                case "caktimi i kushtit te pageses":
                                    ConfigureAspxComboBox.percaktoTemplateComboMeLupe(combo);
                                    colKushtPageseKoka colKushtePag = new colKushtPageseKoka(IdNdermarrja);
                                    combo.DataSource = colKushtePag;
                                    combo.ValueField = "IdKoka";
                                    combo.TextField = "KodiKushtPagese";
                                    combo.DataBind();
                                    combo.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedKP(" + txtVleraDefault + e.VisibleIndex + "," + e.VisibleIndex + "); }";
                                    break;

                                case "caktimi i magazines destinacion":
                                    ConfigureAspxComboBox.percaktoTemplateComboMeLupe(combo);
                                    DataTable dtn1 = colNjesiAdministrative.merrSipasNjesiNdermarjePerLupeDege(IdNdermarrja, IdPerdoruesi, false, -1, true);
                                    combo.DataSource = dtn1;
                                    combo.ValueField = "IdNjesiAdministrative";
                                    combo.TextField = "Kodi";
                                    combo.DataBind();
                                    dtn1.Dispose();
                                    combo.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedNJA(" + txtVleraDefault + e.VisibleIndex + "," + e.VisibleIndex + "); }";
                                    break;

                                case "caktimi i pikes se shitjes furnizimit":
                                    ConfigureAspxComboBox.percaktoTemplateComboMeLupe(combo);
                                    DataTable dtp;
                                    if (Kategoria_ComboBox.SelectedItem.Value.ToString() == "1")
                                        dtp = colPikaShitjeFurnizimi.mbushGjithePikaShitjeDtSmall(IdNdermarrja);
                                    else if (Kategoria_ComboBox.SelectedItem.Value.ToString() == "2")
                                        dtp = colPikaShitjeFurnizimi.mbushGjithePikaFurnizimiDtSmall(IdNdermarrja);
                                    else
                                        dtp = new DataTable();
                                    combo.DataSource = dtp;
                                    combo.ValueField = "IdPikeShitjeFurnizimi";
                                    combo.TextField = "Kodi";
                                    combo.DataBind();
                                    combo.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedPkShF(" + txtVleraDefault + e.VisibleIndex + "," + e.VisibleIndex + "); }";
                                    dtp.Dispose();
                                    break;

                                case "caktimi i arkes te shitja":
                                    ConfigureAspxComboBox.percaktoTemplateComboMeLupe(combo);
                                    var dtt = colBankat.ktheGjitheBankatSipasAutorizimeveSipasLlojit(IdNdermarrja, IdPerdoruesi, false, false);
                                    combo.DataSource = dtt;
                                    combo.ValueField = "IdBanka";
                                    combo.TextField = "KodiBanka";
                                    combo.DataBind();
                                    combo.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedArka(" + txtVleraDefault + e.VisibleIndex + "," + e.VisibleIndex + "); }";
                                    dtt.Dispose();
                                    break;

                                case "caktimi i menyres se pageses":
                                    combo.Items.Add("Mirebesim", 0);
                                    combo.Items.Add("Kesh", 2);
                                    combo.Items.Add("Pagese", 4);
                                    combo.Items.Add("Pagese Automatike", 5);
                                    combo.Items.Add("Cash & Bank", 6);
                                    combo.Items.Add("Arke", 8);
                                    combo.Items.Add("Karte krediti", 9);
                                    combo.Items.Add("Pezull", 10);
                                    combo.Items.Add("Banke", 11);
                                    break;
                                case "caktimi i metodes ftp":
                                    combo.Items.Add("Manual", 1);
                                    combo.Items.Add("Automatik", 2);
                                    if (combo.Text == "")
                                        combo.SelectedIndex = 0;
                                    break;
                                case "caktimi i kases":
                                    ConfigureAspxComboBox.mbushComboKonfigurimKase(combo, IdNdermarrja);
                                    break;
                            }
                            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                            combo.ClientInstanceName = $"{txtVleraDefault}{e.VisibleIndex}";
                            combo.CallbackPageSize = 10000;
                        }

                    }
                    else
                    {
                        var txt = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, IdGjuha == 0 ? col3 : IdGjuha == 1 ? col15 : col19, "txtBox") as ASPxTextBox;
                        if (txt != null)
                        {
                            txt.ClientInstanceName = $"{txtVleraDefault}{e.VisibleIndex}";
                            if (e.VisibleIndex == ((ASPxGridView)sender).VisibleRowCount - 2)
                            {
                                if (ugjet)
                                {
                                    temptxt = txt;
                                    ugjet = false;
                                }

                            }
                        }
                    }

                    if (cmb1 != null)
                    {
                        cmb1.ClientInstanceName = "cmbVisible" + e.VisibleIndex;
                        cmb1.Native = true;
                        cmb1.Items.Add("Jo", "Unchecked", "~/images/white.png");
                        cmb1.Items.Add("Po", "Checked", "~/images/red.png");
                        if (e.VisibleIndex == ((ASPxGridView)sender).VisibleRowCount - 2)
                        {
                            if (ugjet)
                            {
                                tempcombo = cmb1;
                                ugjet = false;
                            }

                        }
                    }




                    if (cmb2 != null)
                    {
                        clsKontroll k = new clsKontroll(_colAtribut[e.VisibleIndex].IdKontroll);
                        cmb2.ClientInstanceName = "cmbEnabled" + e.VisibleIndex;
                        cmb2.Native = true;
                        cmb2.Items.Add("Jo", "Unchecked", "~/images/white.png");
                        cmb2.Items.Add("Po", "Checked", "~/images/red.png");
                        cmb2.ClientSideEvents.TextChanged = "function(s,e){TextChangedEnabled(cmbEnabled" + e.VisibleIndex + ",'Enabled'," + e.VisibleIndex + ", " + k.IdTipiKontrollit + ");}";
                        if (e.VisibleIndex == ((ASPxGridView)sender).VisibleRowCount - 2)
                        {
                            if (ugjet)
                            {
                                tempcombo = cmb2;
                                ugjet = false;
                            }

                        }
                    }

                    if (cmb5 != null)
                    {

                        cmb5.ClientInstanceName = "cmbIdentifikues" + e.VisibleIndex;
                        cmb5.Items.Add("Identifikues", "1", "~/images/Identifikues.png");
                        cmb5.Native = false;
                        cmb5.ShowImageInEditBox = true;
                        cmb5.Items.Add("I pa modifikueshem", "2", "~/images/red.png");

                        cmb5.Items.Add("I modifikueshem", "3", "~/images/green.png");
                        cmb5.ClientSideEvents.TextChanged = "function(s,e){TextChangedIdentifikues(cmbIdentifikues" + e.VisibleIndex + ",'Identifikues'," + e.VisibleIndex + ");}";
                        if (e.VisibleIndex == ((ASPxGridView)sender).VisibleRowCount - 2)
                        {
                            if (ugjet)
                            {
                                tempcombo = cmb5;
                                ugjet = false;
                            }

                        }
                    }

                    if (cmb6 != null)
                    {
                        cmb6.Native = false;
                        cmb6.ShowImageInEditBox = true;
                        if (cmb6.SelectedItem != null && cmb6.SelectedItem.Value.ToString() == "1")
                            cmb6.ClientEnabled = false;
                        cmb6.ClientInstanceName = "cmbDetyrueshme" + e.VisibleIndex;

                        cmb6.Items.Add("Jo", "Unchecked", "~/images/white.png");
                        cmb6.Items.Add("Po", "Checked", "~/images/red.png");
                        if (e.VisibleIndex == ((ASPxGridView)sender).VisibleRowCount - 2)
                        {
                            if (ugjet)
                            {
                                tempcombo = cmb6;
                                ugjet = false;
                            }

                        }
                    }

                    if (cmb14 != null)
                    {
                        clsKontroll k = new clsKontroll(_colAtribut[e.VisibleIndex].IdKontroll);
                        if (k.IdTipiKontrollit != 1 || (k.IdTipiKontrollit == 1 && !_colAtribut[e.VisibleIndex].Enabled))
                            cmb14.ClientEnabled = false;
                        cmb14.Native = false;
                        cmb14.DataSource = MbushNumratAutomatik();
                        cmb14.ValueField = "IdNrAutom";
                        cmb14.TextField = "KodiNrAutom";
                        cmb14.DataBind();
                        cmb14.ClientInstanceName = "cmbNrAutomatik" + e.VisibleIndex;
                        cmb14.ClientSideEvents.TextChanged = "function(s,e){TextChangedNrAutomatik(cmbNrAutomatik" + e.VisibleIndex + ",'NrAutomatik'," + e.VisibleIndex + ");}";
                        if (e.VisibleIndex == ((ASPxGridView)sender).VisibleRowCount - 2)
                        {
                            if (ugjet)
                            {
                                tempcombo = cmb14;
                                ugjet = false;
                            }

                        }
                    }
                    if (cmb16 != null)
                    {
                        cmb16.ClientInstanceName = "cmbShfaqMobile" + e.VisibleIndex;
                        cmb16.Native = true;
                        cmb16.Items.Add("Jo", "Unchecked", "~/images/white.png");
                        cmb16.Items.Add("Po", "Checked", "~/images/red.png");
                        //cmb16.ClientSideEvents.SelectedIndexChanged = "function(s,e){TextChangedShfaqMobile(cmbShfaqMobile" + e.VisibleIndex.ToString() + ",'ShfaqMobile'," + e.VisibleIndex.ToString() + ");}"; Hequr sepse s'perdorej
                        if (e.VisibleIndex == ((ASPxGridView)sender).VisibleRowCount - 2)
                        {
                            if (ugjet)
                            {
                                tempcombo = cmb16;
                                ugjet = false;
                            }

                        }
                        if (int.Parse(Lloji_cmb.Value.ToString()) == 1)
                        {
                            cmb16.ClientEnabled = false;
                        }
                    }

                    if (txt17 != null)
                    {
                        txt17.ClientInstanceName = "txtRenditjaMobile" + e.VisibleIndex;
                        if (e.VisibleIndex == ((ASPxGridView)sender).VisibleRowCount - 2)
                        {
                            if (ugjet)
                            {
                                temptxt = txt17;
                                ugjet = false;
                            }

                        }
                        if (int.Parse(Lloji_cmb.Value.ToString()) == 1)
                        {
                            txt17.ClientEnabled = false;
                        }
                    }

                    if (cmb18 != null)
                    {
                        cmb18.ClientInstanceName = "cmbUnike" + e.VisibleIndex;
                        cmb18.Native = true;
                        cmb18.Items.Add("Jo", "Unchecked", "~/images/white.png");
                        cmb18.Items.Add("Po", "Checked", "~/images/red.png");
                        if (e.VisibleIndex == ((ASPxGridView)sender).VisibleRowCount - 2)
                        {
                            if (ugjet)
                            {
                                tempcombo = cmb18;
                                ugjet = false;
                            }

                        }
                    }

                }

                if (e.VisibleIndex + 1 < _colAtribut.Count)
                {
                    var pershkrimKontrolli = _colAtribut[e.VisibleIndex + 1].PershkrimKontroll.ToLower();
                    if (EshteVleraDefaultCombo(pershkrimKontrolli))
                    {
                        col3.DataItemTemplate = new MyComboTemplate();
                        col15.DataItemTemplate = new MyComboTemplate();
                        col19.DataItemTemplate = new MyComboTemplate();
                    }
                    else
                    {
                        col3.DataItemTemplate = new MyTextTemplate();
                        col15.DataItemTemplate = new MyTextTemplate();
                        col19.DataItemTemplate = new MyTextTemplate();
                    }
                }

                if (txt110 != null)
                {
                    if (e.VisibleIndex + 1 <= _colAtribut.Count)
                    {
                        txt110.ID = "txtKodLupa" + e.VisibleIndex;
                        txt110.Attributes["onkeypress"] = "javascript: KeyPressLupa('" + txt110.ClientID + "'," + e.VisibleIndex + ");";
                        btn110.ClientSideEvents.Click = "function(s,e){ButtonClickedLupa('" + txt110.ClientID + "'," + e.VisibleIndex + ");}";

                        if (_colAtribut[e.VisibleIndex].IdKonfigAmbjenteLupa == 0)
                        {
                            txt110.Enabled = false;
                            btn110.ClientEnabled = false;
                        }
                        if (e.VisibleIndex == ((ASPxGridView)sender).VisibleRowCount - 2)
                        {
                            if (ugjet)
                            {
                                temptxtNormal = txt110;
                                ugjet = false;
                            }

                        }
                    }
                }
                if (txt11 != null)
                {
                    txt11.ClientInstanceName = "txtRreshti" + e.VisibleIndex;

                }
                if (txt12 != null)
                {
                    txt12.ClientInstanceName = "txtKolona" + e.VisibleIndex;

                }
                if (temptxtNormal != null)
                {
                    temptxtNormal.Focus();
                }
                if (IdGjuha == 0)
                {
                    col3.Width = 100;
                    col15.Width = 0;
                    col19.Width = 0;
                }
                else if (IdGjuha == 1)
                {
                    col15.Width = 100;
                    col3.Width = 0;
                    col19.Width = 0;
                }
                else
                {
                    col19.Width = 100;
                    col3.Width = 0;
                    col15.Width = 0;
                }
            }
        }

        private int MerrIdKategorie()
        {
            int idkat = 0;
            switch (Kategoria_ComboBox.Text)
            {
                case "Shitje":
                    idkat = 1;
                    break;
                case "Blerje":
                    idkat = 2;
                    break;
                case "Arka":
                    idkat = 3;
                    break;
                case "Veprime Arke":
                    idkat = 3;
                    break;
                case "Veprime Banke":
                    idkat = 4;
                    break;
                case "Banka":
                    idkat = 4;
                    break;
                case "Magazina":
                    idkat = 6;
                    break;
                case "Flete Kontabel":
                    idkat = 5;
                    break;
                case "Planifikim Prodhimi":
                    idkat = 44;
                    break;
                case "UrdherPagesa":
                    idkat = 39;
                    break;
                case "Inventarizimi":
                    idkat = 135;
                    break;
                case "Miratim Buxheti":
                    idkat = 170;
                    break;
                case "Planifikim Buxheti":
                    idkat = 171;
                    break;
                case "Alokim Buxheti":
                    idkat = 172;
                    break;
                case "Rialokim Buxheti":
                    idkat = 175;
                    break;
                case "Perfitim Buxheti":
                    idkat = 177;
                    break;
                case "Planifikim Ekzekutim Buxheti":
                    idkat = 179;
                    break;
                case "Ekzekutim Buxheti":
                    idkat = 181;
                    break;
            }

            return idkat;
        }

        private int MerrMonedhenDefault()
        {
            int vleraDefault;
            int.TryParse(_colAtribut.Find(x => x.PershkrimKontroll.Equals("caktimi i monedhes", StringComparison.InvariantCultureIgnoreCase))?.VlereDefault, out vleraDefault);
            return vleraDefault;
        }

        protected void grid_kontrollet_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpNoPage"] = grid_kontrollet.PageIndex;
            e.Properties["cpNoRows"] = grid_kontrollet.VisibleRowCount;

            if (grid_kontrollet != null)
            {
                var filter = grid_kontrollet.FilterExpression;
                if (string.IsNullOrEmpty(filter) == false)
                {
                    var keys = "";
                    for (int i = 0; i < grid_kontrollet.VisibleRowCount; i++)
                    {
                        string vlereDefault = grid_kontrollet.GetRowValues(i, "VlereDefault").ToString();
                        string vlereDefaultEng = grid_kontrollet.GetRowValues(i, "VlereDefaultEng").ToString();
                        string vlereDefaultFr = grid_kontrollet.GetRowValues(i, "VlereDefault_fr").ToString();
                        bool detyrueshme = bool.Parse(grid_kontrollet.GetRowValues(i, "Detyrueshme").ToString());
                        string kodLupa = grid_kontrollet.GetRowValues(i, "KodLupa").ToString();
                        int kolona = int.Parse(grid_kontrollet.GetRowValues(i, "Kolona").ToString());
                        int rreshti = int.Parse(grid_kontrollet.GetRowValues(i, "Rreshti").ToString());
                        bool visible = bool.Parse(grid_kontrollet.GetRowValues(i, "Visible").ToString());
                        bool unike = bool.Parse(grid_kontrollet.GetRowValues(i, "Unike").ToString());
                        bool shfaqeMobile = bool.Parse(grid_kontrollet.GetRowValues(i, "ShfaqMobile").ToString());
                        int idKontroll = int.Parse(grid_kontrollet.GetRowValues(i, "IdKontroll").ToString());
                        for(var j = 1; j < _colAtribut.Count; j++)
                        {
                            if(_colAtribut[j].IdKontroll == idKontroll)
                            {
                                _colAtribut[j].VlereDefault = vlereDefault;
                                _colAtribut[j].VlereDefaultEng = vlereDefaultEng;
                                _colAtribut[j].VlereDefault_fr = vlereDefaultFr;
                                _colAtribut[j].Detyrueshme = detyrueshme;
                                _colAtribut[j].KodLupa = kodLupa;
                                _colAtribut[j].Kolona = kolona;
                                _colAtribut[j].Rreshti = rreshti;
                                _colAtribut[j].Unike = unike;
                                _colAtribut[j].ShfaqMobile = shfaqeMobile;
                                _colAtribut[j].Visible = visible;
                            }
                        }
                    }

                }
                mySessionObjects.ruajAtributetNeSession(Session, _colAtribut);

            }
        }

        protected void grid_kontrollet_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            grid_kontrollet.DataSource = mySessionObjects.merrAtributetNgaSessioni(Session);
            grid_kontrollet.DataBind();
            KonfiguroGrideKontrollet();
        }

        #endregion

        #region GridaTrupi

        private void KonfiguroGrideTrupi()
        {
            if (grid_trupi.AllColumns.Count == 0)
                return;
            PercaktoTemplateGridTrupi();
            GridUtil.percaktoAtributeTeGridesShtoPaTheme(grid_trupi, "IdTrupi");
            grid_trupi.Columns["IdTrupi"].Visible = false;
            grid_trupi.Columns["IdKoka"].Visible = false;
            grid_trupi.Columns["KodiTrupi"].VisibleIndex = 0;
            grid_trupi.Columns["KodiTrupi"].Caption = "Kodi";
            grid_trupi.Columns["PershkrimiTrupi"].VisibleIndex = 1;
            grid_trupi.Columns["PershkrimiTrupi"].Caption = "Pershkrimi";
            grid_trupi.Columns["IndexTrupi"].Caption = "Renditja";
            grid_trupi.Columns["VisibleTrupi"].Caption = "Visible";
            grid_trupi.Columns["ReadonlyTrupi"].Caption = "ReadOnly";
            grid_trupi.Columns["WidthTrupi"].Caption = "Width";
            grid_trupi.Columns["IndexTrupiOrigjinal"].Visible = false;
            grid_trupi.Columns["IdKonfigAmbjenteLupa"].Visible = false;
            grid_trupi.Columns["KodLupa"].Caption = "Lupa";
            grid_trupi.Columns["IdKonfigLupaMultiple"].Visible = false;
            grid_trupi.Columns["PershkrimiTrupi_en"].Caption = "Description";
            grid_trupi.Columns["PershkrimiTrupi_fr"].Caption = "La Description";
            grid_trupi.Columns["Tipi"].Visible = false;
            grid_trupi.Columns["ShfaqMobile"].Caption = "Shfaq Mobile";
            grid_trupi.Columns["RenditjaMobile"].Caption = "Renditja Mobile";
            grid_trupi.Columns["GridKokaEmri"].Caption = "Emer Gride";
            grid_trupi.Columns["LlojFormatFushe"].Caption = "Lloj Format Fushe";
            grid_trupi.Columns["LlojFormatFushe"].Visible = false;
            grid_trupi.SettingsPager.Mode = GridViewPagerMode.ShowPager;
            grid_trupi.SettingsPager.PageSize = 15;
            grid_trupi.SettingsBehavior.AllowSort = false;
        }
        private void mbushGridNgaSession()
        {
            DataTable tmpObject = DbCore.mySessionObjects.MerrNgaSession<DataTable>(Session, "grid_kontrollet");
            if (tmpObject == null)
            {
                //mbushGridNgaDB(komponente, periudheDok, idNdermarrjeVit, veprimi, idNdermarrje, idPerdoruesi, gjitheDokumentat, datanga, dataderi);
            }
            else
            {
                grid_kontrollet.DataSource = tmpObject;
                //grid_kontrollet.SaveFilter(idNdermarrje, veprimi);
                grid_kontrollet.DataBind();
                tmpObject.Dispose();
            }
        }
        private void MbushGrideTrupi()
        {
            _oColTrupi = new colGridaTrupi();
            if (hfShtimModifikim.Value == "modifikim" || hfShtimModifikim.Value == "klonim")
            {
                var kofigurimAmbjenti = new clsKonfigurimAmbjenti();
                kofigurimAmbjenti.mbushkonfigPaLloj(int.Parse(Request.QueryString["id"]));
                _oColTrupi = new colGridaTrupi(kofigurimAmbjenti.IdKonfigAmbjente, IdGjuha);
                grid_trupi.DataSource = _oColTrupi;
            }
            else
            {
                if (Kategoria_ComboBox.SelectedItem == null || Kategoria_ComboBox.SelectedIndex == -1)
                {
                    _oColTrupi = new colGridaTrupi(-1, -1, IdGjuha);
                    grid_trupi.DataSource = _oColTrupi;
                }
                else
                {
                    var idKategori = Convert.ToInt32(Kategoria_ComboBox.SelectedItem.Value.ToString());
                    int idNivel = -1;
                    if (cmbNivelRegj.SelectedItem != null)
                        idNivel = Convert.ToInt32(cmbNivelRegj.SelectedItem.Value);

                    var oNivel = new clsNivelRegjistrimi();
                    oNivel.mbushNivelRegjistrimiSipasIdPaKonvertime(idNivel);

                    var oKonfig = new clsKonfigurimAmbjenti
                    {
                        IdNivel = idNivel,
                        IdKategori = idKategori
                    };

                    if (oKonfig.IdKategori == 0)
                    {
                        oKonfig.IdNivel = -1;
                    }

                    oKonfig = oKonfig.merrSipasIdKategoriIdNivel(IdPerdoruesi, false);

                    var oKategori = new clsKategoriNivelDok { IdKategori = idKategori };
                    oKategori = oKategori.merrSipasId();

                    if (oKategori != null && oKategori.IdKategori != 9)
                    {
                        _idKomponente = oKategori.IdKomponente;
                        if (_idKomponente == 508)
                            _idKomponente = 506;
                        _oColTrupi.mbushGrideTrupin(_idKomponente, oKonfig.IdKonfigAmbjente, IdGjuha);
                        grid_trupi.DataSource = _oColTrupi;
                    }
                    else if (oKategori.IdKategori == 9 && oKategori != null)
                    {
                        _idKomponente = oNivel.Radha;
                        _oColTrupi.mbushGrideTrupin(_idKomponente, oKonfig.IdKonfigAmbjente, IdGjuha);
                        grid_trupi.DataSource = _oColTrupi;
                    }
                }
            }

            mySessionObjects.ruajGridaTrupiNeSesion(Session, _oColTrupi);
            grid_trupi.DataBind();

            var serializusi = new JavaScriptSerializer { MaxJsonLength = 500000000 };
            hfVleratFillestareTrupi.Value = serializusi.Serialize(_oColTrupi);
        }

        private void PercaktoTemplateGridTrupi()
        {
            var col13 = grid_trupi.Columns["IndexTrupi"] as GridViewDataTextColumn;
            col13.DataItemTemplate = new MyIntTemplate(false);

            var col14 = grid_trupi.Columns["VisibleTrupi"] as GridViewDataCheckColumn;
            col14.DataItemTemplate = new MyComboTemplate();

            var col15 = grid_trupi.Columns["ReadonlyTrupi"] as GridViewDataCheckColumn;
            col15.DataItemTemplate = new MyComboTemplate();

            var col16 = grid_trupi.Columns["WidthTrupi"] as GridViewDataTextColumn;
            col16.DataItemTemplate = new MyTextTemplate();

            var col171 = grid_trupi.Columns["KodLupa"] as GridViewDataTextColumn;
            col171.DataItemTemplate = new MyTemplateLupa();
            col171.Width = 250;

            var col20 = grid_trupi.Columns["ShfaqMobile"] as GridViewDataCheckColumn;
            col20.DataItemTemplate = new MyComboTemplate();

            var col21 = grid_trupi.Columns["RenditjaMobile"] as GridViewDataTextColumn;
            col21.DataItemTemplate = new MyTextTemplate();
        }

        protected void grid_trupi_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            _oColTrupi = mySessionObjects.merrGridaTrupiNgaSessioni(Session);
            bool ugjet;
            if (e.RowType == GridViewRowType.Data)
            {
                GridViewDataColumn col13 = ((ASPxGridView)sender).Columns["IndexTrupi"] as GridViewDataColumn;
                GridViewDataCheckColumn col14 = ((ASPxGridView)sender).Columns["VisibleTrupi"] as GridViewDataCheckColumn;
                GridViewDataCheckColumn col15 = ((ASPxGridView)sender).Columns["ReadonlyTrupi"] as GridViewDataCheckColumn;
                GridViewDataColumn col16 = ((ASPxGridView)sender).Columns["WidthTrupi"] as GridViewDataColumn;
                GridViewDataColumn col171 = ((ASPxGridView)sender).Columns["KodLupa"] as GridViewDataColumn;
                GridViewDataColumn col18 = ((ASPxGridView)sender).Columns["IndexTrupi"] as GridViewDataColumn;
                GridViewDataCheckColumn col19 = ((ASPxGridView)sender).Columns["VisibleTrupi"] as GridViewDataCheckColumn;
                GridViewDataCheckColumn col20 = ((ASPxGridView)sender).Columns["ShfaqMobile"] as GridViewDataCheckColumn;
                GridViewDataColumn col21 = ((ASPxGridView)sender).Columns["RenditjaMobile"] as GridViewDataColumn;

                ASPxTextBox cmb13 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col13, "txtBox") as ASPxTextBox;
                ASPxComboBox cmb11 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col14, "cmbBox") as ASPxComboBox;
                ASPxComboBox cmb12 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col15, "cmbBox") as ASPxComboBox;
                ASPxTextBox txt11 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col16, "txtBox") as ASPxTextBox;

                TextBox txt171 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col171, "txt") as TextBox;
                ASPxButton btn171 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col171, "btn") as ASPxButton;
                ASPxComboBox cmb16 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col20, "cmbBox") as ASPxComboBox;
                ASPxTextBox txt17 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col21, "txtBox") as ASPxTextBox;

                ugjet = false;

                if (cmb13 != null)
                {
                    cmb13.ClientInstanceName = "cmbIndex" + e.VisibleIndex;
                    if (e.VisibleIndex == ((ASPxGridView)sender).VisibleRowCount - 2)
                    {
                        if (ugjet)
                        {
                            temptxtTrupi = cmb13;
                            ugjet = false;
                        }
                        //else if (koloneFocusTrupi == "IndexTrupi")
                        //{
                        //    ugjet = true;
                        //}
                    }
                }

                if (txt11 != null)
                {
                    txt11.ClientInstanceName = "txtWidthTrupi" + e.VisibleIndex;
                    if (e.VisibleIndex == ((ASPxGridView)sender).VisibleRowCount - 2)
                    {
                        if (ugjet)
                        {
                            temptxtTrupi = txt11;
                            ugjet = false;
                        }
                        //else if (koloneFocusTrupi == "WidthTrupi")
                        //{
                        //    ugjet = true;
                        //}
                    }
                }

                if (cmb11 != null)
                {
                    cmb11.ClientInstanceName = "cmbVisibleTrupi" + e.VisibleIndex;
                    cmb11.Native = true;
                    cmb11.Items.Add("Jo", "Unchecked", "~/images/white.png");
                    cmb11.Items.Add("Po", "Checked", "~/images/red.png");


                    if (e.VisibleIndex == ((ASPxGridView)sender).VisibleRowCount - 2)
                    {
                        if (ugjet)
                        {
                            tempcomboTrupi = cmb11;
                            ugjet = false;
                        }
                        //else if (koloneFocusTrupi == "VisibleTrupi")
                        //{
                        //    ugjet = true;
                        //}
                    }
                }

                if (cmb12 != null)
                {
                    cmb12.ClientInstanceName = "cmbReadonlyTrupi" + e.VisibleIndex;
                    cmb12.Native = true;
                    cmb12.Items.Add("Jo", "Unchecked", "~/images/white.png");
                    cmb12.Items.Add("Po", "Checked", "~/images/red.png");
                    if (e.VisibleIndex == ((ASPxGridView)sender).VisibleRowCount - 2)
                    {
                        if (ugjet)
                        {
                            tempcomboTrupi = cmb12;
                            ugjet = false;
                        }
                        //else if (koloneFocusTrupi == "ReadonlyTrupi")
                        //{
                        //    ugjet = true;
                        //}
                    }
                }

                if (cmb16 != null)
                {
                    cmb16.ClientInstanceName = "cmbShfaqMobileGrida" + e.VisibleIndex;
                    cmb16.Native = true;
                    cmb16.Items.Add("Jo", "Unchecked", "~/images/white.png");
                    cmb16.Items.Add("Po", "Checked", "~/images/red.png");
                    if (e.VisibleIndex == ((ASPxGridView)sender).VisibleRowCount - 2)
                    {
                        if (ugjet)
                        {
                            tempcombo = cmb16;
                            ugjet = false;
                        }

                    }
                    if (int.Parse(Lloji_cmb.Value.ToString()) == 1)
                    {
                        cmb16.ClientEnabled = false;
                    }
                }

                if (txt17 != null)
                {
                    txt17.ClientInstanceName = "txtRenditjaMobileGrida" + e.VisibleIndex;
                    if (e.VisibleIndex == ((ASPxGridView)sender).VisibleRowCount - 2)
                    {
                        if (ugjet)
                        {
                            temptxt = txt17;
                            ugjet = false;
                        }

                    }
                    if (int.Parse(Lloji_cmb.Value.ToString()) == 1)
                    {
                        txt17.ClientEnabled = false;
                    }
                }


                if (txt171 != null)
                {
                    if (_oColTrupi != null)
                    {
                        if (e.VisibleIndex + 1 <= _oColTrupi.Count)
                        {
                            txt171.ID = "txtKodLupaTrupi" + e.VisibleIndex;
                            txt171.Attributes["onkeypress"] = "javascript: KeyPressLupa('" + txt171.ClientID + "'," + e.VisibleIndex + ");";
                            btn171.ClientSideEvents.Click = "function(s,e){ButtonClickedLupa('" + txt171.ClientID + "'," + e.VisibleIndex + ");}";

                            if (_oColTrupi[e.VisibleIndex].IdKonfigAmbjenteLupa == 0)
                            {
                                txt171.Enabled = false;
                                btn171.ClientEnabled = false;
                                txt171.Text = "Pa Lupe";
                            }

                            if (e.VisibleIndex == ((ASPxGridView)sender).VisibleRowCount - 2)
                            {
                                if (ugjet)
                                {
                                    temptxtNormal = txt171;
                                    ugjet = false;
                                }
                                //else if (koloneFocus == "KodLupa")
                                //{
                                //    ugjet = true;
                                //}
                            }
                        }
                    }
                }
                if (tempcomboTrupi != null)
                {
                    tempcomboTrupi.Focus();
                }
                else if (temptxtTrupi != null)
                {
                    temptxtTrupi.Focus();
                }

            }
        }

        protected void grid_trupi_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            grid_trupi.DataSource = mySessionObjects.merrGridaTrupiNgaSessioni(Session);
            grid_trupi.DataBind();
            KonfiguroGrideTrupi();
        }

        protected void grid_trupi_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpNoPage"] = grid_trupi.PageIndex;
            e.Properties["cpNoRows"] = grid_trupi.VisibleRowCount;
        }

        #endregion

        #region Kushtet

        private void KonfiguroGrideKushtet()
        {
            PercaktoTemplateKushte();
            GridUtil.percaktoAtributeTeGridesShtoPaTheme(grid_kushte, "IdKushtTemplate");
            grid_kushte.SettingsBehavior.AllowSort = false;
            grid_kushte.SettingsPager.PageSize = 150;
        }

        private void MbushGrideKushte(int idGjuha)
        {
            _colKush = new colKusht();
            if (hfShtimModifikim.Value == "modifikim" || hfShtimModifikim.Value == "klonim")
            {
                _colKush.mbushGjitheKushteKonfigurimi(int.Parse(Request.QueryString["id"]));

                foreach (var t in _colKush)
                {
                    var kodKushti = t.Kodi.Trim();
                    if ((kodKushti == "ZKDM" || kodKushti == "ZKDM2") && t.Vlera != 0)
                    {
                        var konfmag = new clsKonfigurimAmbjenti();
                        konfmag.IdKonfigAmbjente = t.Vlera;
                        konfmag.mbushKonfigAmbjSipasId(konfmag.IdKonfigAmbjente, idGjuha, false);
                        konfmag.mbushKonfigAmbjSipasKod(konfmag.KodKonfigAmbjente, IdNdermarrja, false);
                        t.Vlera = konfmag.IdKonfigAmbjente;

                        if (kodKushti == "ZKDM")
                            hfMagazina.Value = t.Vlera.ToString();
                    }

                    if ((kodKushti == "ZKDSH" || kodKushti == "ZKDSHI" || kodKushti == "ZKDSHKLSP" || kodKushti == "ZDVFONE") && t.Vlera != 0)
                    {
                        var konfmag = new clsKonfigurimAmbjenti();
                        konfmag.IdKonfigAmbjente = t.Vlera;
                        konfmag.mbushKonfigAmbjSipasId(konfmag.IdKonfigAmbjente, idGjuha, false);
                        konfmag.mbushKonfigAmbjSipasKod(konfmag.KodKonfigAmbjente, IdNdermarrja, false);
                        t.Vlera = konfmag.IdKonfigAmbjente;
                        hfShitja.Value = t.Vlera.ToString();
                    }

                    if (kodKushti == "ZKR" && t.Vlera != 0)
                    {
                        var konfmag = new clsKonfigurimAmbjenti();
                        konfmag.IdKonfigAmbjente = t.Vlera;
                        konfmag.mbushKonfigAmbjSipasId(konfmag.IdKonfigAmbjente, idGjuha, false);
                        konfmag.mbushKonfigAmbjSipasKod(konfmag.KodKonfigAmbjente, IdNdermarrja, false);
                        t.Vlera = konfmag.IdKonfigAmbjente;
                        hfState.Set("IdKonfigRezervime", t.Vlera.ToString());
                    }

                    if (kodKushti == "ZFH" && t.Vlera != 0)
                    {
                        var konfmag = new clsKonfigurimAmbjenti();
                        konfmag.IdKonfigAmbjente = t.Vlera;
                        konfmag.mbushKonfigAmbjSipasId(konfmag.IdKonfigAmbjente, idGjuha, false);
                        konfmag.mbushKonfigAmbjSipasKod(konfmag.KodKonfigAmbjente, IdNdermarrja, false);
                        t.Vlera = konfmag.IdKonfigAmbjente;
                        hfVleraFleteHyrje.Value = t.Vlera.ToString();
                    }

                    if (kodKushti == "ZFK" && t.Vlera != 0)
                    {
                        var konfmag = new clsKonfigurimAmbjenti();
                        konfmag.IdKonfigAmbjente = t.Vlera;
                        konfmag.mbushKonfigAmbjSipasId(konfmag.IdKonfigAmbjente, idGjuha, false);
                        konfmag.mbushKonfigAmbjSipasKod(konfmag.KodKonfigAmbjente, IdNdermarrja, false);
                        t.Vlera = konfmag.IdKonfigAmbjente;
                        hfSkemaKontabelRegjistrime.Value = t.Vlera.ToString();
                    }

                    if (kodKushti == "ZRQK" && t.Vlera != 0)
                    {
                        var konfmag = new clsKonfigurimAmbjenti();
                        konfmag.IdKonfigAmbjente = t.Vlera;
                        konfmag.mbushKonfigAmbjSipasId(konfmag.IdKonfigAmbjente, idGjuha, false);
                        konfmag.mbushKonfigAmbjSipasKod(konfmag.KodKonfigAmbjente, IdNdermarrja, false);
                        t.Vlera = konfmag.IdKonfigAmbjente;
                        hfQK.Value = t.Vlera.ToString();
                    }

                    if (kodKushti == "DL" && t.Vlera != 0)
                    {
                        var konfmag = new clsKonfigurimAmbjenti();
                        konfmag.IdKonfigAmbjente = t.Vlera;
                        konfmag.mbushKonfigAmbjSipasId(konfmag.IdKonfigAmbjente, idGjuha, false);
                        konfmag.mbushKonfigAmbjSipasKod(konfmag.KodKonfigAmbjente, IdNdermarrja, false);
                        t.Vlera = konfmag.IdKonfigAmbjente;
                        hfMagazina.Value = t.Vlera.ToString();
                    }

                    if (kodKushti == "ZDAM" && t.Vlera != 0)
                    {
                        var konfmag = new clsKonfigurimAmbjenti();
                        konfmag.IdKonfigAmbjente = t.Vlera;
                        konfmag.mbushKonfigAmbjSipasId(konfmag.IdKonfigAmbjente, idGjuha, false);
                        konfmag.mbushKonfigAmbjSipasKod(konfmag.KodKonfigAmbjente, IdNdermarrja, false);
                        t.Vlera = konfmag.IdKonfigAmbjente;
                        hfAmortizimi.Value = t.Vlera.ToString();
                    }

                    if (kodKushti == "CFDP" && t.Vlera != 0)
                        hfFormula.Value = t.Vlera.ToString();

                    if (kodKushti == "ZKDA" && t.Vlera != 0)
                    {
                        var konfark = new clsKonfigurimAmbjenti();
                        konfark.IdKonfigAmbjente = t.Vlera;
                        konfark.mbushKonfigAmbjSipasId(konfark.IdKonfigAmbjente, idGjuha, false);
                        konfark.mbushKonfigAmbjSipasKod(konfark.KodKonfigAmbjente, IdNdermarrja, false);
                        t.Vlera = konfark.IdKonfigAmbjente;
                        hfArketim.Value = t.Vlera.ToString();
                    }

                    if (kodKushti == "ZKDPAG" && t.Vlera != 0)
                    {
                        var konfpag = new clsKonfigurimAmbjenti();
                        konfpag.IdKonfigAmbjente = t.Vlera;
                        konfpag.mbushKonfigAmbjSipasId(konfpag.IdKonfigAmbjente, idGjuha, false);
                        konfpag.mbushKonfigAmbjSipasKod(konfpag.KodKonfigAmbjente, IdNdermarrja, false);
                        t.Vlera = konfpag.IdKonfigAmbjente;
                        hfPagese.Value = t.Vlera.ToString();
                    }

                    if (kodKushti == "ZKDB" && t.Vlera != 0)
                    {
                        var konfbler = new clsKonfigurimAmbjenti();
                        konfbler.IdKonfigAmbjente = t.Vlera;
                        konfbler.mbushKonfigAmbjSipasId(konfbler.IdKonfigAmbjente, idGjuha, false);
                        konfbler.mbushKonfigAmbjSipasKod(konfbler.KodKonfigAmbjente, IdNdermarrja, false);
                        t.Vlera = konfbler.IdKonfigAmbjente;
                        hfBlere.Value = t.Vlera.ToString();
                    }

                }
            }
            else
            {
                if (Kategoria_ComboBox.SelectedItem == null || Kategoria_ComboBox.SelectedIndex == -1)
                    _colKush.mbushGjitheKushteDefaultNivel(-1, -1);
                else
                {
                    var idKategori = Kategoria_ComboBox.SelectedItem.Value.ToString();
                    var idNivel = "-1";
                    if (cmbNivelRegj.SelectedItem != null)
                        idNivel = cmbNivelRegj.SelectedItem.Value.ToString();

                    var oKonfig = new clsKonfigurimAmbjenti
                    {
                        IdNivel = int.Parse(idNivel),
                        IdKategori = Convert.ToInt32(idKategori)
                    };
                    oKonfig = oKonfig.merrSipasIdKategoriIdNivel(IdPerdoruesi, false);

                    if (oKonfig.IdKategori == 0)
                    {
                        oKonfig.IdNivel = -1;

                        oKonfig = oKonfig.merrSipasIdKategoriIdNivel(IdPerdoruesi, false);
                    }

                    _colKush.mbushGjitheKushteKonfigurimi(oKonfig.IdKonfigAmbjente);
                    foreach (var t in _colKush)
                    {
                        string kodKushti = t.Kodi.Trim();
                        if ((kodKushti == "ZKDM" || kodKushti == "ZKDM2") && t.Vlera != 0)
                        {
                            var konfmag = new clsKonfigurimAmbjenti();
                            konfmag.IdKonfigAmbjente = t.Vlera;
                            konfmag.mbushKonfigAmbjSipasId(konfmag.IdKonfigAmbjente, idGjuha, false);
                            konfmag.mbushKonfigAmbjSipasKod(konfmag.KodKonfigAmbjente, IdNdermarrja, false);
                            t.Vlera = konfmag.IdKonfigAmbjente;
                            if (kodKushti == "ZKDM")
                                hfMagazina.Value = t.Vlera.ToString();
                        }

                        if ((kodKushti == "ZKDSH" || kodKushti == "ZKDSHI" || kodKushti == "ZKDSHKLSP" || kodKushti == "ZDVFONE") && t.Vlera != 0)
                        {
                            var konfmag = new clsKonfigurimAmbjenti();
                            konfmag.IdKonfigAmbjente = t.Vlera;
                            konfmag.mbushKonfigAmbjSipasId(konfmag.IdKonfigAmbjente, idGjuha, false);
                            konfmag.mbushKonfigAmbjSipasKod(konfmag.KodKonfigAmbjente, IdNdermarrja, false);
                            t.Vlera = konfmag.IdKonfigAmbjente;
                            hfShitja.Value = t.Vlera.ToString();
                        }

                        if (kodKushti == "ZKR" && t.Vlera != 0)
                        {
                            var konfmag = new clsKonfigurimAmbjenti();
                            konfmag.IdKonfigAmbjente = t.Vlera;
                            konfmag.mbushKonfigAmbjSipasId(konfmag.IdKonfigAmbjente, idGjuha, false);
                            konfmag.mbushKonfigAmbjSipasKod(konfmag.KodKonfigAmbjente, IdNdermarrja, false);
                            t.Vlera = konfmag.IdKonfigAmbjente;
                            hfState.Set("IdKonfigRezervime", t.Vlera.ToString());
                        }

                        if (kodKushti == "DL" && t.Vlera != 0)
                        {
                            var konfmag = new clsKonfigurimAmbjenti();
                            konfmag.IdKonfigAmbjente = t.Vlera;
                            konfmag.mbushKonfigAmbjSipasId(konfmag.IdKonfigAmbjente, idGjuha, false);
                            konfmag.mbushKonfigAmbjSipasKod(konfmag.KodKonfigAmbjente, IdNdermarrja, false);
                            t.Vlera = konfmag.IdKonfigAmbjente;
                            hfMagazina.Value = t.Vlera.ToString();
                        }

                        if (kodKushti == "ZDAM" && t.Vlera != 0)
                        {
                            var konfmag = new clsKonfigurimAmbjenti();
                            konfmag.IdKonfigAmbjente = t.Vlera;
                            konfmag.mbushKonfigAmbjSipasId(konfmag.IdKonfigAmbjente, idGjuha, false);
                            konfmag.mbushKonfigAmbjSipasKod(konfmag.KodKonfigAmbjente, IdNdermarrja, false);
                            t.Vlera = konfmag.IdKonfigAmbjente;
                            hfAmortizimi.Value = t.Vlera.ToString();
                        }

                        if (kodKushti == "ZFK" && t.Vlera != 0)
                        {
                            var konfmag = new clsKonfigurimAmbjenti();
                            konfmag.IdKonfigAmbjente = t.Vlera;
                            konfmag.mbushKonfigAmbjSipasId(konfmag.IdKonfigAmbjente, idGjuha, false);
                            konfmag.mbushKonfigAmbjSipasKod(konfmag.KodKonfigAmbjente, IdNdermarrja, false);
                            t.Vlera = konfmag.IdKonfigAmbjente;
                            hfSkemaKontabelRegjistrime.Value = t.Vlera.ToString();
                        }
                        if (kodKushti == "ZRQK" && t.Vlera != 0)
                        {
                            var konfmag = new clsKonfigurimAmbjenti();
                            konfmag.IdKonfigAmbjente = t.Vlera;
                            konfmag.mbushKonfigAmbjSipasId(konfmag.IdKonfigAmbjente, idGjuha, false);
                            konfmag.mbushKonfigAmbjSipasKod(konfmag.KodKonfigAmbjente, IdNdermarrja, false);
                            t.Vlera = konfmag.IdKonfigAmbjente;
                            hfQK.Value = t.Vlera.ToString();
                        }

                        if (kodKushti == "CFDP" && t.Vlera != 0)
                            hfFormula.Value = t.Vlera.ToString();

                        if (kodKushti == "ZFH" && t.Vlera != 0)
                        {
                            var konfmag = new clsKonfigurimAmbjenti();
                            konfmag.IdKonfigAmbjente = t.Vlera;
                            konfmag.mbushKonfigAmbjSipasId(konfmag.IdKonfigAmbjente, idGjuha, false);
                            konfmag.mbushKonfigAmbjSipasKod(konfmag.KodKonfigAmbjente, IdNdermarrja, false);
                            t.Vlera = konfmag.IdKonfigAmbjente;
                            hfVleraFleteHyrje.Value = t.Vlera.ToString();
                        }

                        if (kodKushti == "ZKDA" && t.Vlera != 0)
                        {
                            var konfark = new clsKonfigurimAmbjenti();
                            konfark.IdKonfigAmbjente = t.Vlera;
                            konfark.mbushKonfigAmbjSipasId(konfark.IdKonfigAmbjente, idGjuha, false);
                            konfark.mbushKonfigAmbjSipasKod(konfark.KodKonfigAmbjente, IdNdermarrja, false);
                            t.Vlera = konfark.IdKonfigAmbjente;
                            hfArketim.Value = t.Vlera.ToString();
                        }

                        if (kodKushti == "ZKDPAG" && t.Vlera != 0)
                        {
                            var konfpag = new clsKonfigurimAmbjenti();
                            konfpag.IdKonfigAmbjente = t.Vlera;
                            konfpag.mbushKonfigAmbjSipasId(konfpag.IdKonfigAmbjente, idGjuha, false);
                            konfpag.mbushKonfigAmbjSipasKod(konfpag.KodKonfigAmbjente, IdNdermarrja, false);
                            t.Vlera = konfpag.IdKonfigAmbjente;
                            hfPagese.Value = t.Vlera.ToString();
                        }

                        if (kodKushti == "ZKDB" && t.Vlera != 0)
                        {
                            var konfbler = new clsKonfigurimAmbjenti();
                            konfbler.IdKonfigAmbjente = t.Vlera;
                            konfbler.mbushKonfigAmbjSipasId(konfbler.IdKonfigAmbjente, idGjuha, false);
                            konfbler.mbushKonfigAmbjSipasKod(konfbler.KodKonfigAmbjente, IdNdermarrja, false);
                            t.Vlera = konfbler.IdKonfigAmbjente;
                            hfBlere.Value = t.Vlera.ToString();
                        }
                    }
                }
            }

            mySessionObjects.ruajKushteNeSesion(Session, _colKush);
            var serializusi = new JavaScriptSerializer { MaxJsonLength = 500000000 };
            hfVleratFillestareKushtet.Value = serializusi.Serialize(_colKush);
            grid_kushte.DataSource = _colKush;
            grid_kushte.DataBind();
        }

        private void PercaktoTemplateKushte()
        {
            var col19 = grid_kushte.Columns["Vlera"] as GridViewDataTextColumn;
            col19.DataItemTemplate = new MyComboTemplate();
        }

        protected void grid_kushte_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            grid_kushte.DataSource = mySessionObjects.merrKushteNgaSessioni(Session);
            grid_kushte.DataBind();
            KonfiguroGrideKontrollet();
        }

        protected void grid_kushte_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpNoPage"] = grid_kushte.PageIndex;
            e.Properties["cpNoRows"] = grid_kushte.VisibleRowCount;
        }

        protected void grid_kushte_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            _colKush = mySessionObjects.merrKushteNgaSessioni(Session);
            if (e.RowType != GridViewRowType.Data)
                return;

            var col19 = ((ASPxGridView)sender).Columns["Vlera"] as GridViewDataTextColumn;
            var checkVlera = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col19, "cmbBox") as ASPxComboBox;

            if (checkVlera == null)
                return;
            if (_colKush.Count > e.VisibleIndex)
            {
                switch (_colKush[e.VisibleIndex].Kodi.Trim())
                {
                    case "GJSI":
                    case "GJLLB":
                        checkVlera.DropDownButton.Visible = false;
                        break;
                    case "PVMPDMVP":
                        checkVlera.DropDownButton.Visible = false;
                        var vleraKushti = clsAlternativaKushti.ktheVlereReKushtShitje(_colKush[e.VisibleIndex].Vlera);
                        checkVlera.Items.Add(vleraKushti == null ? "" : vleraKushti.ToString(), _colKush[e.VisibleIndex].Vlera);
                        checkVlera.SelectedIndex = 0;
                        checkVlera.ClientSideEvents.LostFocus = "function(s,e){ LostFocusKushtMinimumShitje(Vlera" + e.VisibleIndex + "," + e.VisibleIndex + "); }";
                        break;
                    case "LLD":
                        ConfigureAspxComboBox.percaktoTemplateComboMeLupe(checkVlera);
                        if (_colKush[e.VisibleIndex].Vlera != 0)
                        {
                            string[] ids =  hfPrioriteti.Value.Split(',');
                            string showedText = "";
                            for (int i = 0; i < ids.Length; i++)
                            {
                                clsKonfLlojRreshtiVlere llojRreshtiVlere = new clsKonfLlojRreshtiVlere(Convert.ToInt32(ids[i].ToString()));
                                showedText += llojRreshtiVlere.KodLlojRreshti;
                                if (i != ids.Length - 1)
                                    showedText += ", ";
                            }
                            checkVlera.Text = showedText;
                        }
                        checkVlera.ClientSideEvents.Init = "function(s,e){ InitLloji(Vlera" + e.VisibleIndex + "," + e.VisibleIndex + "); }";
                        checkVlera.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedLloji(Vlera" + e.VisibleIndex + "," + e.VisibleIndex + "); }";
                        checkVlera.ClientSideEvents.LostFocus = "function(s,e){ LostFocusLloji(Vlera" + e.VisibleIndex + "," + e.VisibleIndex + "); }";
                        break;
                    case "ZKDM":
                        hfVleraMag.Value = e.VisibleIndex.ToString();
                        ConfigureAspxComboBox.percaktoTemplateComboMeLupe(checkVlera);
                        ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorisePaKolona(IdPerdoruesi, IdNdermarrja, checkVlera, 6, 2, IdGjuha); //regjistrime
                        checkVlera.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedMag(Vlera" + e.VisibleIndex + "," + e.VisibleIndex + "); }";
                        checkVlera.ClientSideEvents.LostFocus = "function(s,e){ LostFocusMag(Vlera" + e.VisibleIndex + "," + e.VisibleIndex + "); }";
                        break;
                    case "ZKDSH":
                    case "ZKDSHI":
                    case "ZDVFONE":
                        hfVleraShit.Value = e.VisibleIndex.ToString();
                        ConfigureAspxComboBox.percaktoTemplateComboMeLupe(checkVlera);
                        ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorisePaKolona(IdPerdoruesi, IdNdermarrja, checkVlera, 1, 2, IdGjuha); //regjistrime
                        checkVlera.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedShit(Vlera" + e.VisibleIndex + "," + e.VisibleIndex + "); }";
                        checkVlera.ClientSideEvents.LostFocus = "function(s,e){ LostFocusShit(Vlera" + e.VisibleIndex + "," + e.VisibleIndex + "); }";
                        break;
                    case "ZKDSHKLSP":
                    case "KVDN":
                        ConfigureAspxComboBox.percaktoTemplateComboMeLupe(checkVlera);
                        ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorisePaKolona(IdPerdoruesi, IdNdermarrja, checkVlera, _konf.IdKategori, 2, IdGjuha); //regjistrime
                        checkVlera.ClientSideEvents.ButtonClick = _konf.IdKategori == 3
                            ? "function(s,e){ ButtonClickedArk(Vlera" + e.VisibleIndex + "," + e.VisibleIndex + "); }"
                            : "function(s,e){ ButtonClickedShit(Vlera" + e.VisibleIndex + "," + e.VisibleIndex + "); }";
                        checkVlera.ClientSideEvents.LostFocus = "function(s,e){ LostFocusShit(Vlera" + e.VisibleIndex + "," + e.VisibleIndex + "); }";
                        break;
                    case "PEMAIL":
                    case "MAILVODAFONE":
                    case "MAILDEALER":
                        ConfigureAspxComboBox.percaktoTemplateComboMeLupe(checkVlera);
                        checkVlera.IncrementalFilteringMode = IncrementalFilteringMode.Contains;

                        checkVlera.TextField = "PerdoruesUsername";
                        checkVlera.ValueField = "IdPerdorues";

                        var dt = colPerdoruesit.merrPerdoruesitSipasLicencesDT(mySessionObjects.ktheIdPerdoruesi(Session), clsLicenca.merrIdLicencePerdoruesi(mySessionObjects.ktheIdPerdoruesi(Session)));
                        checkVlera.DataSource = dt;
                        checkVlera.DataBind();
                        checkVlera.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedPer(Vlera" + e.VisibleIndex + "," + e.VisibleIndex + "); }";
                        checkVlera.ClientSideEvents.LostFocus = "function(s,e){ LostFocusPer(Vlera" + e.VisibleIndex + "," + e.VisibleIndex + "); }";
                        break;
                    case "ZKR":
                        hfRezervim.Value = e.VisibleIndex.ToString();
                        ConfigureAspxComboBox.percaktoTemplateComboMeLupe(checkVlera);
                        ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorisePaKolona(IdPerdoruesi, IdNdermarrja, checkVlera, 78, 2, IdGjuha); //regjistrime
                        checkVlera.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedRez(Vlera" + e.VisibleIndex + "," + e.VisibleIndex + "); }";
                        checkVlera.ClientSideEvents.LostFocus = "function(s,e){ LostFocusRez(Vlera" + e.VisibleIndex + "," + e.VisibleIndex + "); }";
                        break;
                    case "ZKDM2":
                        hfVleraMag2.Value = e.VisibleIndex.ToString();
                        ConfigureAspxComboBox.percaktoTemplateComboMeLupe(checkVlera);
                        ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorisePaKolona(IdPerdoruesi, IdNdermarrja, checkVlera, 6, 2, IdGjuha); //regjistrime
                        checkVlera.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedMag2(Vlera" + e.VisibleIndex + "," + e.VisibleIndex + "); }";
                        checkVlera.ClientSideEvents.LostFocus = "function(s,e){ LostFocusMag2(Vlera" + e.VisibleIndex + "," + e.VisibleIndex + "); }";
                        break;
                    case "ZKDP":
                        hfVleraMag2.Value = e.VisibleIndex.ToString();
                        ConfigureAspxComboBox.percaktoTemplateComboMeLupe(checkVlera);
                        ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorisePaKolona(IdPerdoruesi, IdNdermarrja, checkVlera, 44, 2, IdGjuha); //regjistrime
                        checkVlera.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedPlan(Vlera" + e.VisibleIndex + "," + e.VisibleIndex + "); }";
                        checkVlera.ClientSideEvents.LostFocus = "function(s,e){ LostFocusPlan(Vlera" + e.VisibleIndex + "," + e.VisibleIndex + "); }";
                        break;
                    case "ZSP":
                        ConfigureAspxComboBox.percaktoTemplateComboMeLupe(checkVlera);
                        var ds = colKokaSkemaWorkFlow.merrKokaSkemaWorkFlowNdermarrjesDT(IdNdermarrja);
                        var toInsert = ds.NewRow();
                        ds.Rows.InsertAt(toInsert, 0);
                        checkVlera.DataSource = ds;
                        checkVlera.ValueField = "IdKoka";
                        checkVlera.TextField = "Kodi";
                        checkVlera.DataBind();
                        checkVlera.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedSkemaWF(Vlera" + e.VisibleIndex + "," + e.VisibleIndex + "); }";
                        checkVlera.ClientSideEvents.LostFocus = "function(s,e){ LostFocusSkemaWF(Vlera" + e.VisibleIndex + "," + e.VisibleIndex + "); }";
                        hfZSP.Value = e.VisibleIndex.ToString();
                        break;
                    case "ZFK":
                        hfVleraKont.Value = e.VisibleIndex.ToString();
                        ConfigureAspxComboBox.percaktoTemplateComboMeLupe(checkVlera);
                        ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorisePaKolona(IdPerdoruesi, IdNdermarrja, checkVlera, 5, 2, IdGjuha);//regjistrime
                        checkVlera.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedFK(Vlera" + e.VisibleIndex + "," + e.VisibleIndex + "); }";
                        checkVlera.ClientSideEvents.LostFocus = "function(s,e){ LostFocusFK(Vlera" + e.VisibleIndex + "," + e.VisibleIndex + "); }";
                        break;
                    case "AD":
                        ConfigureAspxComboBox.percaktoTemplateComboMeLupe(checkVlera);
                        DataTable dtArt = colArtikujt.merrSipasArtikujNdermarrjesAndAutorizimeDT(IdNdermarrja, IdPerdoruesi, false, false, false, false, "20");
                        checkVlera.DataSource = dtArt;
                        checkVlera.ValueField = "IdArtikulli";
                        checkVlera.TextField = "KodArtikulli";
                        checkVlera.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                        checkVlera.DataBind();
                        checkVlera.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedAD(Vlera" + e.VisibleIndex + "," + e.VisibleIndex + "); }";
                        dtArt.Dispose();
                        break;
                    case "FIDA":
                        hfLidhjeArketim.Value = e.VisibleIndex.ToString();
                        checkVlera.DataSource = new colAlternativatKushti(_colKush[e.VisibleIndex].IdKusht);
                        checkVlera.TextField = "Alternativa";
                        checkVlera.ValueField = "IdAlternativaKushti";
                        checkVlera.DataBind();
                        break;
                    case "FIDK":
                        var colalt = new colAlternativatKushti(_colKush[e.VisibleIndex].IdKusht);
                        hfFIDK.Value = colalt.Find(x => x.IdAlternativaKushti == _colKush[e.VisibleIndex].Vlera).Alternativa;

                        checkVlera.DataSource = colalt;
                        checkVlera.TextField = "Alternativa";
                        checkVlera.ValueField = "IdAlternativaKushti";
                        checkVlera.DataBind();
                        break;
                    case "LSD":
                        hfLlojSubjektiDefaultVlera.Value = e.VisibleIndex.ToString();

                        checkVlera.DataSource = new colAlternativatKushti(_colKush[e.VisibleIndex].IdKusht);
                        checkVlera.TextField = "Alternativa";
                        checkVlera.ValueField = "IdAlternativaKushti";
                        checkVlera.DataBind();
                        checkVlera.DropDownStyle = DropDownStyle.DropDownList;
                        checkVlera.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                        checkVlera.ClientSideEvents.ValueChanged = "function(s,e){cmbLlojSubjektiDefaultSelectedIndexChanged(); }";

                        break;
                    case "SD":
                        hfSubjektiDefaultVlera.Value = e.VisibleIndex.ToString();
                        ConfigureAspxComboBox.percaktoTemplateCombo(true, false, checkVlera);

                        var vlDefault = ((ASPxGridView)sender).GetRowValuesByKeyValue(e.KeyValue, "Vlera");
                        int id = 0;
                        if (vlDefault != null)
                            int.TryParse(vlDefault.ToString(), out id);
                        MbushComboPerSubjektinDefaultDheRuajNeHiddenField(id);

                        checkVlera.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedSubjektiDefault(Vlera" + e.VisibleIndex + "," + e.VisibleIndex + " ); }";
                        break;
                    case "NrAutoKodi":
                        ConfigureAspxComboBox.mbushComboNrAutomatik(mySessionObjects.merrIdNdermarrjeSesioni(Session), int.Parse(Kategoria_ComboBox.Value.ToString()), checkVlera);
                        break;
                    case "NrAutoKodi2":
                        ConfigureAspxComboBox.mbushComboNrAutomatik(mySessionObjects.merrIdNdermarrjeSesioni(Session), int.Parse(Kategoria_ComboBox.Value.ToString()), checkVlera);
                        break;
                    case "MPPFPGJ":
                    case "PMPFPDMPA":
                        ConfigureAspxComboBox.mbushComboMenyrePagese(checkVlera, false);
                        if (_colKush[e.VisibleIndex].Vlera == 0)
                            checkVlera.SelectedIndex = 1;
                        break;
                    case "NRDA":
                        hfNrAutoDok.Value = e.VisibleIndex.ToString();
                        checkVlera.DataSource = new colAlternativatKushti(_colKush[e.VisibleIndex].IdKusht);
                        checkVlera.TextField = "Alternativa";
                        checkVlera.ValueField = "IdAlternativaKushti";
                        checkVlera.DataBind();
                        break;
                    case "ZRQK":
                        hfQK.Value = e.VisibleIndex.ToString();
                        ConfigureAspxComboBox.percaktoTemplateComboMeLupe(checkVlera);
                        ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorisePaKolona(IdPerdoruesi, IdNdermarrja, checkVlera, 75, 2, IdGjuha);//regjistrime
                        checkVlera.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedRQK(Vlera" + e.VisibleIndex + "," + e.VisibleIndex + "); }";
                        checkVlera.ClientSideEvents.LostFocus = "function(s,e){ LostFocusRQK(Vlera" + e.VisibleIndex + "," + e.VisibleIndex + "); }";
                        break;
                    case "SK":
                        hfVleraKont.Value = e.VisibleIndex.ToString();
                        ConfigureAspxComboBox.percaktoTemplateComboMeLupe(checkVlera);
                        ConfigureAspxComboBox.mbushComboSkemaKontabelRegjistrime(IdNdermarrjeVit, checkVlera);
                        checkVlera.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedSK(Vlera" + e.VisibleIndex + "," + e.VisibleIndex + "); }";
                        checkVlera.ClientSideEvents.LostFocus = "function(s,e){ LostFocusFK(Vlera" + e.VisibleIndex + "," + e.VisibleIndex + "); }";
                        break;
                    case "DL":
                        hfVleraMag.Value = e.VisibleIndex.ToString();
                        ConfigureAspxComboBox.percaktoTemplateComboMeLupe(checkVlera);
                        ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorisePaKolona(IdPerdoruesi, IdNdermarrja, checkVlera, 10, 2, IdGjuha);
                        checkVlera.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedDL(Vlera" + e.VisibleIndex + "," + e.VisibleIndex + "); }";
                        checkVlera.ClientSideEvents.LostFocus = "function(s,e){ LostFocusDL(Vlera" + e.VisibleIndex + "," + e.VisibleIndex + "); }";
                        break;
                    case "ZDAM":
                        hfVleraAmor.Value = e.VisibleIndex.ToString();
                        ConfigureAspxComboBox.percaktoTemplateComboMeLupe(checkVlera);
                        ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorisePaKolona(IdPerdoruesi, IdNdermarrja, checkVlera, 86, 2, IdGjuha);
                        checkVlera.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedAM(Vlera" + e.VisibleIndex + "," + e.VisibleIndex + "); }";
                        checkVlera.ClientSideEvents.LostFocus = "function(s,e){ LostFocusAM(Vlera" + e.VisibleIndex + "," + e.VisibleIndex + "); }";
                        break;
                    case "ZIA":
                        ConfigureAspxComboBox.mbushComboInfoArtikulli(IdNdermarrja, checkVlera, Convert.ToInt32(LlojInfo.Artikulli));
                        if (_colKush[e.VisibleIndex].Vlera == 0)
                            checkVlera.Text = "JO";
                        else if (_colKush[e.VisibleIndex].Vlera == -1)
                            checkVlera.Text = "INFDEF";
                        break;
                    case "ZIKF":
                        ConfigureAspxComboBox.mbushComboInfoArtikulli(IdNdermarrja, checkVlera, Convert.ToInt32(LlojInfo.KlientFurnitori));
                        if (_colKush[e.VisibleIndex].Vlera == 0)
                            checkVlera.Text = "JO";
                        else if (_colKush[e.VisibleIndex].Vlera == -2)
                            checkVlera.Text = "INFDEFKF";
                        break;
                    case "ZILL":
                        ConfigureAspxComboBox.mbushComboInfoArtikulli(IdNdermarrja, checkVlera, Convert.ToInt32(LlojInfo.Llogari));
                        if (_colKush[e.VisibleIndex].Vlera == 0)
                            checkVlera.Text = "JO";
                        break;
                    case "FILTER":
                        hfFiltri.Value = _colKush[e.VisibleIndex].Vlera.ToString();
                        int idNivel = -1;
                        if (cmbNivelRegj.SelectedItem != null)
                            idNivel = Convert.ToInt32(cmbNivelRegj.SelectedItem.Value);

                        var oKonfig = new clsKonfigurimAmbjenti();
                        oKonfig.IdNivel = idNivel;
                        oKonfig.IdKategori = Convert.ToInt32(Kategoria_ComboBox.SelectedItem.Value.ToString());
                        oKonfig.IdNdermarje = IdNdermarrja;
                        oKonfig.KodKonfigAmbjente = kodKonfig_TextBox.Text;
                        oKonfig = oKonfig.merrSipasKodit();

                        var colFiltra = new colFiltratGrida();
                        colFiltra.merrFiltraSipasKonfigurimit(oKonfig.IdKonfigAmbjente, IdNdermarrja);
                        colFiltra.Insert(0, new clsFiltraGrida());

                        ConfigureAspxComboBox.mbushComboFiltra(checkVlera, colFiltra, "IdFiltra", "FiltraKodi");
                        checkVlera.ClientSideEvents.Init = "function(s,e){ InitFiltri(Vlera" + e.VisibleIndex + "," + e.VisibleIndex + "); }";
                        if (_oColTrupi != null && _oColTrupi.Count > 0)
                            checkVlera.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedFiltri(Vlera" + e.VisibleIndex + "," + e.VisibleIndex + "," + _oColTrupi[0].IdKoka + "); }";
                        checkVlera.ClientSideEvents.LostFocus = "function(s,e){ LostFocusFiltri(Vlera" + e.VisibleIndex + "," + e.VisibleIndex + "); }";
                        break;
                    case "CFDP":
                        ConfigureAspxComboBox.percaktoTemplateComboMeLupe(checkVlera);
                        ConfigureAspxComboBox.mbushComboFormulat(IdNdermarrja, checkVlera);
                        checkVlera.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedFormula(Vlera" + e.VisibleIndex + "," + e.VisibleIndex + "); }";
                        checkVlera.ClientSideEvents.LostFocus = "function(s,e){ LostFocusFormula(Vlera" + e.VisibleIndex + "," + e.VisibleIndex + "); }";
                        break;
                    case "LLFK":
                        hfFletaKont.Value = e.VisibleIndex.ToString();
                        checkVlera.DataSource = new colAlternativatKushti(_colKush[e.VisibleIndex].IdKusht);
                        checkVlera.TextField = "Alternativa";
                        checkVlera.ValueField = "IdAlternativaKushti";
                        checkVlera.DataBind();
                        break;
                    case "F":
                        hfVleraBarkod.Value = e.VisibleIndex.ToString();
                        checkVlera.DataSource = new colAlternativatKushti(_colKush[e.VisibleIndex].IdKusht);
                        checkVlera.TextField = "Alternativa";
                        checkVlera.ValueField = "IdAlternativaKushti";
                        checkVlera.DataBind();
                        break;
                    case "KDPe":
                        hfPezull.Value = e.VisibleIndex.ToString();
                        checkVlera.DataSource = new colAlternativatKushti(_colKush[e.VisibleIndex].IdKusht);
                        checkVlera.TextField = "Alternativa";
                        checkVlera.ValueField = "IdAlternativaKushti";
                        checkVlera.DataBind();
                        break;
                    case "KDPr":
                        hfProces.Value = e.VisibleIndex.ToString();
                        checkVlera.DataSource = new colAlternativatKushti(_colKush[e.VisibleIndex].IdKusht);
                        checkVlera.TextField = "Alternativa";
                        checkVlera.ValueField = "IdAlternativaKushti";
                        checkVlera.DataBind();
                        break;
                    case "KSSH":
                        hfProces.Value = e.VisibleIndex.ToString();
                        checkVlera.DataSource = new colAlternativatKushti(_colKush[e.VisibleIndex].IdKusht);
                        checkVlera.TextField = "Alternativa";
                        checkVlera.ValueField = "IdAlternativaKushti";
                        checkVlera.DataBind();
                        break;
                    case "ZFH":
                        hfVleraFleteHyrje.Value = e.VisibleIndex.ToString();
                        ConfigureAspxComboBox.percaktoTemplateComboMeLupe(checkVlera);
                        ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorisePaKolona(IdPerdoruesi, IdNdermarrja, checkVlera, 6, 2, IdGjuha);
                        checkVlera.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedFleteHyrje(Vlera" + e.VisibleIndex + "," + e.VisibleIndex + "); }";
                        checkVlera.ClientSideEvents.LostFocus = "function(s,e){ LostFocusFH(Vlera" + e.VisibleIndex + "," + e.VisibleIndex + "); }";
                        break;
                    case "ZTK":

                        hfZevendesim.Value = e.VisibleIndex.ToString();
                        checkVlera.DataSource = new colAlternativatKushti(_colKush[e.VisibleIndex].IdKusht);
                        checkVlera.TextField = "Alternativa";
                        checkVlera.ValueField = "IdAlternativaKushti";
                        checkVlera.DataBind();
                        break;
                    case "ZT":

                        hfZevendesimT.Value = e.VisibleIndex.ToString();
                        hfZevendesimVlera.Value = new clsAlternativaKushti(_colKush[e.VisibleIndex].Vlera).Alternativa;
                        checkVlera.DataSource = new colAlternativatKushti(_colKush[e.VisibleIndex].IdKusht);
                        checkVlera.TextField = "Alternativa";
                        checkVlera.ValueField = "IdAlternativaKushti";
                        checkVlera.DataBind();
                        break;

                    case "V":
                        checkVlera.DataSource = new colAlternativatKushti(_colKush[e.VisibleIndex].IdKusht);
                        hfVleraDokVartes.Value = new clsAlternativaKushti(_colKush[e.VisibleIndex].Vlera).Alternativa;
                        checkVlera.TextField = "Alternativa";
                        checkVlera.ValueField = "IdAlternativaKushti";
                        checkVlera.DataBind();
                        checkVlera.ClientSideEvents.ValueChanged = "function(s,e){cmbDokumtVarteSelectedIndexChanged(s,e); }";
                        break;
                    case "RBART":
                        hfBart.Value = e.VisibleIndex.ToString();
                        checkVlera.DataSource = new colAlternativatKushti(_colKush[e.VisibleIndex].IdKusht);
                        checkVlera.TextField = "Alternativa";
                        checkVlera.ValueField = "IdAlternativaKushti";
                        checkVlera.DataBind();
                        break;
                    case "LSHNRB":
                        checkVlera.DataSource = new colAlternativatKushti(_colKush[e.VisibleIndex].IdKusht);
                        checkVlera.TextField = "Alternativa";
                        checkVlera.ValueField = "IdAlternativaKushti";
                        checkVlera.DataBind();
                        break;
                    case "ZKDA":
                        hfVleraArk.Value = e.VisibleIndex.ToString();
                        ConfigureAspxComboBox.percaktoTemplateComboMeLupe(checkVlera);
                        ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorisePaKolona(IdPerdoruesi, IdNdermarrja, checkVlera, 3, 2, IdGjuha); //regjistrime
                        checkVlera.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedArk(Vlera" + e.VisibleIndex + "," + e.VisibleIndex + "); }";
                        checkVlera.ClientSideEvents.LostFocus = "function(s,e){ LostFocusArk(Vlera" + e.VisibleIndex + "," + e.VisibleIndex + "); }";
                        break;
                    case "SHDPER":
                        colAlternativatKushti col = new colAlternativatKushti(_colKush[e.VisibleIndex].IdKusht);
                        if (Kategoria_ComboBox.Text != "Lidhje dokumentash")
                            col.FindAndRemove(x => x.Alternativa == "Gjithe Vitet");
                        if (Kategoria_ComboBox.Text == "Lista Dokumenta shitje/blerje")
                            col.FindAndRemove(x => x.Alternativa == "3 Ditore");
                        checkVlera.DataSource = col;
                        checkVlera.TextField = "Alternativa";
                        checkVlera.ValueField = "IdAlternativaKushti";
                        checkVlera.DataBind();
                        break;
                    case "ZKDPAG":
                        hfVleraPagese.Value = e.VisibleIndex.ToString();
                        ConfigureAspxComboBox.percaktoTemplateComboMeLupe(checkVlera);
                        ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorisePaKolona(IdPerdoruesi, IdNdermarrja, checkVlera, 3, 2, IdGjuha); //regjistrime
                        checkVlera.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedPag(Vlera" + e.VisibleIndex + "," + e.VisibleIndex + "); }";
                        checkVlera.ClientSideEvents.LostFocus = "function(s,e){ LostFocusPag(Vlera" + e.VisibleIndex + "," + e.VisibleIndex + "); }";
                        break;
                    case "ZKDB":
                        hfVleraBlere.Value = e.VisibleIndex.ToString();
                        ConfigureAspxComboBox.percaktoTemplateComboMeLupe(checkVlera);
                        ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorisePaKolona(IdPerdoruesi, IdNdermarrja, checkVlera, 2, 2, IdGjuha); //regjistrime
                        checkVlera.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedBler(Vlera" + e.VisibleIndex + "," + e.VisibleIndex + "); }";
                        checkVlera.ClientSideEvents.LostFocus = "function(s,e){ LostFocusBler(Vlera" + e.VisibleIndex + "," + e.VisibleIndex + "); }";
                        break;
                    case "RSKDMD":
                        hfVleraRSKDMD.Value = new clsAlternativaKushti(_colKush[e.VisibleIndex].Vlera).Alternativa;
                        checkVlera.DataSource = new DbCore.DbShare.colAlternativatKushti(_colKush[e.VisibleIndex].IdKusht);
                        checkVlera.TextField = "Alternativa";
                        checkVlera.ValueField = "IdAlternativaKushti";
                        checkVlera.DataBind();
                        break;
                    case "SHDQKPMD":
                        hfSHDQKPMD.Value = e.VisibleIndex.ToString();
                        checkVlera.DataSource = new DbCore.DbShare.colAlternativatKushti(_colKush[e.VisibleIndex].IdKusht);
                        checkVlera.TextField = "Alternativa";
                        checkVlera.ValueField = "IdAlternativaKushti";
                        checkVlera.DataBind();
                        break;
                    case "TKLL_B":
                        hfTKLL_B.Value = new clsAlternativaKushti(_colKush[e.VisibleIndex].Vlera).Alternativa;
                        checkVlera.DataSource = new DbCore.DbShare.colAlternativatKushti(_colKush[e.VisibleIndex].IdKusht);
                        checkVlera.TextField = "Alternativa";
                        checkVlera.ValueField = "IdAlternativaKushti";
                        checkVlera.DataBind();
                        break;
                    case "ZLL_B":
                        hfZLL_B.Value = e.VisibleIndex.ToString();
                        ConfigureAspxComboBox.percaktoTemplateComboMeLupe(checkVlera);
                        ConfigureAspxComboBox.mbushComboLlogaria(IdNdermarrja, IdPerdoruesi, checkVlera, true);
                        string vleraKushtit = clsAlternativaKushti.ktheVlereKushtiSipasIdAlternativeMultiselect(_colKush[e.VisibleIndex].Vlera);
                        checkVlera.Text = vleraKushtit;
                        checkVlera.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedLLogaria(Vlera" + e.VisibleIndex + "," + e.VisibleIndex + ", 'KonfigurimDokumentashBuxhetim'); }";
                        checkVlera.ClientSideEvents.LostFocus = "function(s,e){ LostFocusKushtLlogariBuxheti(Vlera" + e.VisibleIndex + "," + e.VisibleIndex + "); }";
                        break;
                    case "NKAKNKA":
                        hfNKAKNKA.Value = e.VisibleIndex.ToString();
                        checkVlera.DataSource = new DbCore.DbShare.colAlternativatKushti(_colKush[e.VisibleIndex].IdKusht);
                        checkVlera.TextField = "Alternativa";
                        checkVlera.ValueField = "IdAlternativaKushti";
                        checkVlera.DataBind();
                        break;
                    case "SDAF":
                        hfSDAF.Value = e.VisibleIndex.ToString();
                        checkVlera.DataSource = new DbCore.DbShare.colAlternativatKushti(_colKush[e.VisibleIndex].IdKusht);
                        checkVlera.TextField = "Alternativa";
                        checkVlera.ValueField = "IdAlternativaKushti";
                        checkVlera.DataBind();
                        break;
                    case "NCDNC":
                        hfSDAF.Value = e.VisibleIndex.ToString();
                        var niveleCmimesh = colNiveleCmimesh.GetNiveleCmimeshLookupSimpleTable(IdNdermarrja, kodKonfig_TextBox.Text == "CB" ? 1 : 0, IdPerdoruesi);
                        var newRow = niveleCmimesh.NewRow(); newRow["IdNivelCmimi"] = -1; newRow["PershkrimNivelCmimi"] = "Te Gjithe";
                        niveleCmimesh.Rows.InsertAt(newRow, 0);
                        checkVlera.DataSource = niveleCmimesh;
                        checkVlera.TextField = "PershkrimNivelCmimi";
                        checkVlera.ValueField = "IdNivelCmimi";
                        checkVlera.DataBind();
                        break;
                    default:
                        checkVlera.DataSource = new colAlternativatKushti(_colKush[e.VisibleIndex].IdKusht);
                        checkVlera.TextField = "Alternativa";
                        checkVlera.ValueField = "IdAlternativaKushti";
                        checkVlera.DataBind();
                        break;
                }
            }

            checkVlera.ClientInstanceName = "Vlera" + e.VisibleIndex;
            checkVlera.CallbackPageSize = 1000;
            checkVlera.ClientSideEvents.SelectedIndexChanged = "function(s,e){ValueChangedVleraKusht(Vlera" + e.VisibleIndex + ",'Vlera'," + e.VisibleIndex + ", '" + _colKush[e.VisibleIndex].Kodi.Trim() + "');}";
        }

        private void MbushComboPerSubjektinDefaultDheRuajNeHiddenField(int idKlient)
        {
            var colVlera = grid_kushte.Columns["Vlera"] as GridViewDataTextColumn;
            var comboLlojSubjekti = grid_kushte.FindRowCellTemplateControl(Convert.ToInt32(hfLlojSubjektiDefaultVlera.Value), colVlera, "cmbBox") as ASPxComboBox;
            var comboVlera = grid_kushte.FindRowCellTemplateControl(Convert.ToInt32(hfSubjektiDefaultVlera.Value), colVlera, "cmbBox") as ASPxComboBox;

            switch (comboLlojSubjekti.Text)
            {
                case "Klient":
                case "Furnitor":
                    var dt = clsKlientFurnitor.MbushKlienteOseFurnitoreMeId(idKlient, IdNdermarrja, IdPerdoruesi);
                    dt.AddRow();
                    comboVlera.DataSource = dt;
                    comboVlera.ValueField = "IdKlientFurnitor";
                    comboVlera.TextField = "KodKlientFurnitor";
                    comboVlera.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                    comboVlera.DataBind();
                    if (comboVlera.Items.Count != 0)
                        comboVlera.Items[0].Selected = true;
                    break;
                case "Llogari":
                    comboVlera.DataSource = colLlogarite.merrSipasLlogariteNdermarrjesAndAutorizimeAktivDT(IdNdermarrja, IdPerdoruesi);
                    comboVlera.TextField = "NrLlogari";
                    comboVlera.ValueField = "IdLlogari";
                    comboVlera.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                    comboVlera.DataBind();
                    break;
                case "Punonjes":
                    comboVlera.DataSource = DbCore.DbListPagesat.colPunonjes.merrPunonjesNdermarjeDTAktiv(IdNdermarrja, false);
                    comboVlera.TextField = "NrPersonal";
                    comboVlera.ValueField = "IdPunonjes";
                    comboVlera.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                    comboVlera.DataBind();
                    break;
            }
        }

        #endregion

        private void MbushListeNivele()
        {
            colNivelRegjistrimi colNivele;

            if (Kategoria_ComboBox.SelectedItem != null)
            {
                colNivele = clsNivelRegjistrimi.merrGjitheNivelRegjistrimiSipasKategori(IdNdermarrja, IdPerdoruesi, Convert.ToInt32(Kategoria_ComboBox.SelectedItem.Value));
            }
            else
            {
                var oNivel = new clsNivelRegjistrimi();
                colNivele = oNivel.merrGjitheNivelRegjistrimi(IdNdermarrja, IdPerdoruesi);
            }

            cmbNivelRegj.DataSource = colNivele;
            cmbNivelRegj.ValueField = "IdNivel";
            cmbNivelRegj.TextField = "Pershkrimi";
            cmbNivelRegj.DataBind();
            cmbNivelRegj.IncrementalFilteringMode = IncrementalFilteringMode.Contains;

            if (cmbNivelRegj.Text == "" && Kategoria_ComboBox.Text != "")
                cmbNivelRegj.SelectedIndex = 0;
        }

        protected void Ruaj()
        {
            if (!Page.IsValid)
                return;

            if (!IsValidKonfigurim())
                return;

            var tedrejtaInfo = new clsTeDrejtaRoli();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, clsFunksione.GetKomponente(Page.Request));

            if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim")
            {
                if (!tedrejtaInfo.DShtim)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                    return;
                }
            }
            else
            {
                if (!tedrejtaInfo.DMod)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                    return;
                }
            }

            try
            {
                int idKonfigAmbjent;
                int.TryParse(Request.QueryString["id"], out idKonfigAmbjent);
                var serializusi = new JavaScriptSerializer { MaxJsonLength = 500000000 };
                var atributet = (object[])serializusi.DeserializeObject(hfVleratFillestareKontrollet.Value);
                var kushtet = (object[])serializusi.DeserializeObject(hfVleratFillestareKushtet.Value);

                var kofigurAmbjenti = new clsKonfigurimAmbjenti();
                kofigurAmbjenti.Ruaj(IdGjuha, IdPerdoruesi, IdNdermarrja, int.Parse(Kategoria_ComboBox.SelectedItem.Value.ToString()), hfMagazina.Value != "" ? int.Parse(hfMagazina.Value) : 0, Convert.ToInt32(cmbNivelRegj.Value), cmbFormatNumri.Text, hfShtimModifikim.Value, pershkrimKonfig_TextBox.Text, pershkrimKonfigEng_TextBox.Text, pershkrimKonfigFr_TextBox.Text, kodKonfig_TextBox.Text, cmbAutorizimiHf.Value, Lloji_cmb.Value.ToString(), hfPrioriteti.Value, atributet, kushtet, radhaTextBox.Text, hfSkemaKontabelRegjistrime.Value != "" ? int.Parse(hfSkemaKontabelRegjistrime.Value) : 0, ref _idKomponente, JsonConvert.DeserializeObject<colGridaTrupi>(hfVleratFillestareTrupi.Value), idKonfigAmbjent);
                Response.Redirect("KonfigDokumentash.aspx?idsuperkat=" + Request.QueryString["idsuperkat"] + "&ruaj=ok&indexrow=" + Request.QueryString["indexrow"]);
            }
            catch (MyException ex)
            {
                ImbLogger.Error(ex.Message);
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, ex.Message, pnlMesazhi);
            }
            catch (Exception ex)
            {
                var mesazhi = MessagesResource.Messages["msgAdministrimiRuajtjaPerfundoiGabime"];
                ImbLogger.Error(ex.Message);
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhi, pnlMesazhi);
            }
        }

        private bool IsValidKonfigurim()
        {
            int idKonfigAmbjente = Request.QueryString["id"] == null || hfShtimModifikim.Value != "modifikim" ? 0 : int.Parse(Request.QueryString["id"]);

            using (var dbShare = new clsDatabaseShare())
            {
                if (hfShtimModifikim.Value != "modifikim" &&
                    dbShare.ekzistonKonfigurim(kodKonfig_TextBox.Text, IdNdermarrja))
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ekziston nje konfigurim me kete kod! Ju lutemi shenoni nje kod tjeter.", pnlMesazhi);
                    return false;
                }

                if (dbShare.ekzistonKonfigurimSipasRadhes(Convert.ToInt32(cmbNivelRegj.SelectedItem.Value.ToString()),
                    Convert.ToInt32(radhaTextBox.Text), idKonfigAmbjente,
                    Convert.ToInt32(Kategoria_ComboBox.SelectedItem.Value.ToString())))
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ekziston nje konfigurim me kete radhe! Ju lutemi shenoni nje radhe tjeter.", pnlMesazhi);
                    return false;
                }

                if (cmbFormatNumri.Text != "")
                {
                    if (!clsFormatiKonfig.ekzistonKonfigSipasKodit(cmbFormatNumri.Text, IdNdermarrja))
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Formati i numrit nuk ekziston!", pnlMesazhi);
                        return false;
                    }

                    var formati = new clsFormatiKonfig();
                    formati.mbushFormatNrKonfigSipasKodit(cmbFormatNumri.Text, IdNdermarrja);
                    if (formati.IdKategoria != int.Parse(Kategoria_ComboBox.Value.ToString()))
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Formati i numrit nuk i perket kesaj kategorie!", pnlMesazhi);
                        return false;
                    }
                }
            }

            return true;
        }

        protected void ASPxCallbackPanel1_Callback(object sender, CallbackEventArgsBase e)
        {
            KonfiguroVleraFillestare(IdGjuha);
            ASPxMenu1.Items[0].ClientEnabled = true;
        }

        protected colNrAutom MbushNumratAutomatik()
        {
            var nraut = (colNrAutom)mySessionObjects.merrObjectNgaSesioni(Session, "NrAutomatik");
            if (nraut == null)
            {
                nraut = new colNrAutom { new clsNrAutom() };
                nraut.mbushGjitheNumratAutomatikeSipasKategorise(IdNdermarrja, Kategoria_ComboBox.Value.ToString() != "" ? int.Parse(Kategoria_ComboBox.Value.ToString()) : 0);
                mySessionObjects.ruajObjectNeSesion(Session, nraut, "NrAutomatik");
            }
            return nraut;
        }

        protected void cmbFormatNumri_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (!IsCallback || !Request.Params["__CALLBACKID"].Contains("cmbFormatNumri")) return;

            if (Kategoria_ComboBox.Value == null
                || Kategoria_ComboBox.Value.ToString() == ""
                || int.Parse(Kategoria_ComboBox.Value.ToString()) == 0
                || int.Parse(Kategoria_ComboBox.Value.ToString()) == -1)
                ConfigureAspxComboBox.mbushComboKonfigFormatiNumrash(IdNdermarrja, cmbFormatNumri);
            else
                ConfigureAspxComboBox.mbushComboKonfigFormatiNumrashSipasKategorise(IdNdermarrja,
                    int.Parse(Kategoria_ComboBox.Value.ToString()), cmbFormatNumri);
        }
    }
}