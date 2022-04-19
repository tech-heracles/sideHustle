using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Data;
using PlatinumWeb.Templates;
using DbCore;
using DbCore.DbKontabiliteti;
using DbCore.DbShare;
using System.Web.Script.Serialization;
using DbCore.IMBUtils.Types;
using Newtonsoft.Json;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using DbCore.DbAdmin;
using System.Linq;
using DbCore.DbArkaBanka;
using DbCore.DbInventari;
using DbCore.DbQendraKosto;
using DbCore.DbRegjistrim;
using DbCore.IMBUtils.Extensions;
using DbCore.IMBUtils.Messages;
using DevExpress.Data;
using PlatinumWeb.ApplicationUtils;
using DevExpress.Data.Filtering;
using System.Web.UI;
using Web.Framework.Templates;

namespace PlatinumWeb
{

    public partial class Shto_KlientFurnitor : MyPageBase
    {
        private static string Komponente => "Shto_KlientFurnitor.aspx?kf=klient";
        private ASPxTextBox _temptxt;

        protected void Page_Init(object sender, EventArgs e)
        {
        }

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

            var perdoruesi = new clsPerdorues(IdPerdoruesi);
            hfPerdoruesAktual.Value = perdoruesi.PerdoruesUsername;

            if (!Page.IsPostBack)
            {
                hfState.Set("guidString", GuidString);
                ShtoVleraTePergjithshmeNeHfState();
                EmrateTabeve();
                EmratEKontrolleve();
                hfArkivaDokId.Value = Request.QueryString["id"];
                ASPxPageControl1.ActiveTabIndex = 0;
                KonfiguroVleraFillestare();
                BejVisibleTabet();

                hfState.Set("sortColumn", GridUtil.GetSortedColumnFromFilterDefault(Convert.ToInt32(cmbKonfigurimi.Value), null));
                MbushGrideNgaDb();

                if (ASPxGridView_KF.FilterExpression == "")
                    ASPxGridView_KF.FilterExpression = Request.QueryString["kf"] == "klient" ? "[LlojiKF]=true And [AktivKF] = True" : "[LlojiKF]=false And [AktivKF] = True";

                var tedrejtaInfo = new clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, clsFunksione.GetKomponente(Page.Request));
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                hfTeDrejta.Add("ShtimDraft", tedrejtaInfo.DShtimDraft);
                hfMeme.Value = mySessionObjects.merrEshteMemeSesioni(Session).ToString();
                MbushHiddenFieldMePerkthime();
                ucFushatShtese.KonfiguroVleraFillestare(IdNdermarrja, IdPerdoruesi, IdGjuha, Komponente, "Klient/Furnitor", 0, int.Parse(cmbKonfigurimi.Value.ToString()));
                KonfiguroGride();
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(IdNdermarrja, "ASPxGridView_KF", ASPxGridView_KF, cmbKonfigurimi.Text.Split(';')[0], 123.ToString(), IdGjuha);
            }
            else
            {
                ASPxGridView_KF.Columns.Clear();
                ASPxGridView_KF.AutoGenerateColumns = true;                
                MbushGridKFshNgaSession();
                KonfiguroGride();
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(IdNdermarrja, "ASPxGridView_KF", ASPxGridView_KF, cmbKonfigurimi.Text.Split(';')[0], 123.ToString(), IdGjuha, false);
                OrderColumns();
            }
            GridUtil.konfigGrideListeEMadhePaTheme(ASPxGridView_KF, "IdKlientFurnitor");
            AspxWebControlUtils.perkthePopUp(popFshi, rm.GetString("labelKujdes", ci), lblMsgbox, rm.GetString("msgnumRreshtashSelektuar", ci), ButtonCancel, rm.GetString("labelAnullo", ci));
            popupUniversal.HeaderText = rm.GetString("popupAdministrimiUniversal", ci);
            //GridUtil.konfigGrideListeEMadhePaTheme(ASPxGridView_KF, "IdKlientFurnitor");
            PercaktoTamplate();
            ucFushatShtese.percaktoTemplateFushash();
            clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, IdNdermarrja, "ASPxGridView_KF", int.Parse(cmbKonfigurimi.Value.ToString()), Komponente);
            GridUtil.ToolTipButonaveMbiGride(ASPxGridView_KF, ci, rm);
            ASPxGridView_KF.PercaktoTitlePanelMeRefresh(this, MenuInfo, pnlMesazhi, hfState, IdPerdoruesi, IdNdermarrja, IdViti, IdGjuha, int.Parse(cmbKonfigurimi.Value.ToString()), Komponente, rm, ci, true);
            mbushComboTipiId();
            PercaktoTemplateMenu();
            if (ASPxMenu1.Items.FindByName("Draft") != null)
                ASPxMenu1.Items.FindByName("Draft").Text = rm.GetString("draftKF", ci);
        }


        /// <summary>
        /// Vendos emrat e tabeve ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateTabeve()
        {
            ASPxPageControl1.TabPages[0].Text = MessagesResource.Messages["labelBlerjeShitjeTePergjithshme"];
            ASPxPageControl1.TabPages[1].Text = MessagesResource.Messages["labelAdministrimiInformacion"];
            ASPxPageControl1.TabPages[2].Text = MessagesResource.Messages["labelRaportKontakti"];
            ASPxPageControl1.TabPages[3].Text = MessagesResource.Messages["MenuItemRaportKontabiliteti"];
            ASPxPageControl1.TabPages[4].Text = MessagesResource.Messages["MenuItemRegjistrime"];
            ASPxPageControl1.TabPages[5].Text = MessagesResource.Messages["labelFilterAvancuarArkaBanka"];
            ASPxPageControl1.TabPages[6].Text = MessagesResource.Messages["buxhetiTab"];
            ASPxPageControl1.TabPages[7].Text = MessagesResource.Messages["fushatShteseTab"];
        }

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="idNderVit"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        private void PercaktoTemplateMenu() =>
            clsToolbarConfig.percaktoTemplateMenu(IdGjuha, IdViti, IdPerdoruesi, IdNdermarrja, ASPxMenu1, clsFunksione.GetKomponente(Page.Request), this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, hfShtimModifikim.Value != "modifikim", false, false, Meme);

        protected void RuajKolona_Click(object sender, EventArgs e)
        {
            int idfiltri;
            var idkonf = clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(cmbKonfigurimi.Text.Split(';')[0], IdNdermarrja);

            var mesazh = GridUtil.ruajFiltra(IdNdermarrja, IdPerdoruesi, IdGjuha, "ASPxGridView_KF ", Komponente, "FilterDefault", ASPxGridView_KF.FilterExpression, ASPxGridView_KF, "KodKlientFurnitor", idkonf, out idfiltri, hfState);//ruan filtrin e zgjedhur tek filtrat

            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                return;
            }

            mesazh = GridUtil.ruajkonfigurimgride(ASPxGridView_KF, cmbKonfigurimi.Text, IdNdermarrja, IdPerdoruesi, 123, idfiltri, IdViti, ci, IdGjuha);/// ruan konfigurimin e grides dhe filtrin
            clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, IdNdermarrja, "ASPxGridView_KF ", int.Parse(cmbKonfigurimi.Value.ToString()), Komponente);

            PercaktoTemplateMenu();

            if (mesazh.Status)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
        }
        public void mbushComboTipiId()
        {
            cmbTipiId.Items.Add("NUIS");
            cmbTipiId.Items.Add("ID");
            cmbTipiId.Items.Add("PASS");
            cmbTipiId.Items.Add("VAT");
            cmbTipiId.Items.Add("TAX");
            cmbTipiId.Items.Add("SOC");
            cmbTipiId.Items.Add("");

        }
        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e) => PercaktoTemplateMenu();

        private void BejVisibleTabet()
        {
            ASPxPageControl1.TabPages[2].ClientVisible = false;
            ASPxPageControl1.TabPages[6].ClientVisible = false;
            ASPxPageControl1.TabPages[7].ClientVisible = true;
        }

        /// <summary>
        /// mbush komboboxet dhe gridat e faqes
        /// </summary>
        private void KonfiguroVleraFillestare()
        {
            ConfigureAspxComboBox.KonfiguroComboBoxTitulliKlientFurnitor(cmbTitulli);
            ConfigureAspxComboBox.KonfiguroComboBoxLlojAdresa(cmbLlojAdrese);
            ConfigureAspxComboBox.KonfiguroComboBoxPrioriteti(cmbPrioriteti);
            ConfigureAspxComboBox.ShtoKolonaPerKategoriZbritje(btneKategoriZbritje);
            ConfigureAspxComboBox.KonfiguroComboBoxKategoriZbritje(IdNdermarrja, btneKategoriZbritje);
            ConfigureAspxComboBox.KonfiguroComboBoxComboQytete(IdNdermarrja, txtQyteti, false);
            ConfigureAspxComboBox.KonfiguroComboBoxMaturimi(IdNdermarrja, btneMaturimi);
            ConfigureAspxComboBox.KonfiguroComboBoxLlojPorosie(cmbLlojPorosie);
            ConfigureAspxComboBox.KonfiguroComboBoxMetoda(cmbMetoda, true);
            ConfigureAspxComboBox.ShtoKolonaPerNivelZbritjesh(btneNivelZbritje);
            ConfigureAspxComboBox.KonfiguroComboBoxNiveleZbritjePrind(IdNdermarrja, btneNivelZbritje);
            ConfigureAspxComboBox.ShtoKolonaPerLlogarine(txtLlogDytesor);
            ConfigureAspxComboBox.ShtoKolonaPerLlogarine(txtLlogKons);
            ConfigureAspxComboBox.ShtoKolonaPerLlogarine(txtNr);
            ConfigureAspxComboBox.ShtoKolonaPerLlogarine(txtNr2);
            ConfigureAspxComboBox.ShtoKolonaPerLlogarine(txtNr4);
            ConfigureAspxComboBox.ShtoKolonaPerLlogarine(txtNr7);
            ConfigureAspxComboBox.ShtoKolonaPerNivelCmimesh(btneNivelCmimi);
            ConfigureAspxComboBox.KonfiguroComboBoxObjektivaKosto(cmbObjektiva, IdNdermarrja);
            ConfigureAspxComboBox.KonfiguroComboBoxAgjentesh(IdNdermarrja, IdPerdoruesi, cmbAgjentShitjesh, btnAgjenti2, btnAgjenti3);
            //ConfigureAspxComboBox.KonfiguroComboBoxAgjentesh(btnAgjenti2, IdNdermarrja, IdPerdoruesi);
            //ConfigureAspxComboBox.KonfiguroComboBoxAgjentesh(btnAgjenti3, IdNdermarrja, IdPerdoruesi);
            ConfigureAspxComboBox.KonfiguroComboBoxGrupKf(IdNdermarrja, btneGrupimi1, 1, Request.QueryString["kf"] == "furnitor");
            ConfigureAspxComboBox.KonfiguroComboBoxGrupKf(IdNdermarrja, btneGrupimi2, 2, Request.QueryString["kf"] == "furnitor");
            ConfigureAspxComboBox.KonfiguroComboBoxGrupKf(IdNdermarrja, btneGrupimi3, 3, Request.QueryString["kf"] == "furnitor");
            ConfigureAspxComboBox.KonfiguroComboBoxMenyreTransporti(IdNdermarrja, cmbMenyraTransporti);
            ConfigureAspxComboBox.KonfiguroComboBoxKushteDergimi(IdNdermarrja, cmbKushteDergimi);
            ConfigureAspxComboBox.KonfiguroComboBoxNdermarjeBij(IdNdermarrja, cmbBij);
            ConfigureAspxComboBox.KonfiguroComboBoxKushtPagese(IdNdermarrja, cmbKushtetPageses);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(txtQenderKosto, cmbObjektiva, btneNivelZbritje, cmbAgjentShitjesh, btnAgjenti2, btneGrupimi1, btneGrupimi2, btneGrupimi3, cmbKushtetPageses, cmbKushteDergimi, cmbMenyraTransporti, btneMaturimi, cmbBij, btneKategoriZbritje, btnAgjenti3);
            ConfigureAspxComboBox.percaktoTemplateComboJoList(btneCaktoNeHarte);
            AspxWebControlUtils.vendosDateEditMask(dteDatelindjaKF);
            ConfigureAspxComboBox.percaktoTemplateCombo(false, false, cmbNivelTvsh);
            ConfigureAspxComboBox.KonfiguroComboBoxTaksat(IdPerdoruesi, IdNdermarrja, cmbNivelTvsh, LlojTakse.Nivel_Tvsh, false, false, true);
            ConfigureAspxComboBox.percaktoTemplateComboMeEnableCallback(txtLlogDytesor, txtLlogKons, txtNr4, txtNr2, txtNr, txtNr7, btneNivelCmimi, btneKlientiKryesor, txtEmriBanka);

            ConfigureAspxCheckBoxList.KonfiguroCheckBoxListMarreveshje(cblistLlojMarreveshje);
            ConfigureAspxComboBox.KonfiguroComboBoxKonfigurimeshSipasKategoriseDheNivelit(cmbKonfigurimi, IdPerdoruesi, IdNdermarrja, IdGjuha, 12, Request.QueryString["kf"] == "klient" ? "K" : "F");

            var konf = new clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()), IdGjuha);

            hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;
            hfState.Set("colAutorizime", JsonConvert.SerializeObject(new colAutorizimetKoka(IdPerdoruesi)));
        }

        private void MbushGridKFshNgaSession()
        {
            DataTable tmpObject;
            var sukses = mySessionObjects.merrGrideNgaSessioni(Session, out tmpObject);

            if (!sukses)
                MbushGrideNgaDb();
            else
            {
                ASPxGridView_KF.DataSource = tmpObject;
                if (!IsCallback && Request["__CALLBACKID"] == "ASPxGridView_KF")
                    ASPxGridView_KF.RestoreFilter(IdNdermarrja, Request.QueryString["kf"], string.Empty);
                ASPxGridView_KF.DataBind();
                tmpObject.Dispose();
            }
        }


        public override void MbushGrideNgaDb(bool pastroFiltrinGrides = false)
        {
            string filterDefault = pastroFiltrinGrides ? Request.QueryString["kf"] == "klient" ? "[LlojiKF]=true" : "[LlojiKF]=false" : Request.QueryString["kf"] == "klient" ? "[LlojiKF]=true And [AktivKF] = True" : "[LlojiKF]=false And [AktivKF] = True";

            var op = CriteriaOperator.Parse(string.IsNullOrWhiteSpace(ASPxGridView_KF.FilterExpression) ? filterDefault : ASPxGridView_KF.FilterExpression);

            var filterString = CriteriaToWhereClauseHelper.GetMsSqlWhere(op);
            if (string.IsNullOrWhiteSpace(ASPxGridView_KF.FilterExpression))
                ASPxGridView_KF.FilterExpression = filterDefault;
               
            string topRows = "";
            var topRowControl = this.MerrTopRowsControl(hfState);
                if (topRowControl != null)
                {
                    if (((DataTable)ASPxGridView_KF.DataSource)?.Rows.Count > 50 && string.IsNullOrEmpty(topRowControl.GetTopRowsPerSql()) && (ASPxGridView_KF.FilterExpression.ToLower() != filterDefault.ToLower() && ASPxGridView_KF.FilterExpression.ToLower().Contains(filterDefault.ToLower())) && Request.Params["__CALLBACKPARAM"].Contains("COLUMNFILTER"))
                        return;
                    topRows = topRowControl.GetTopRowsPerSql();
                }
                else
                {
                    clsGridaKoka gridaKoka = new clsGridaKoka("ASPxGridView_KF", Komponente, IdNdermarrja, Convert.ToInt32(cmbKonfigurimi.Value));
                    topRows = (gridaKoka.TopRows != null && gridaKoka.TopRows > 0) ? gridaKoka.TopRows.ToString() : string.Empty;
                }
                if (!string.IsNullOrEmpty(topRows))
                    topRows = $"TOP {topRows}";
            string sort = hfState.Get("sortColumn").ToString();
            var dt = colKlienteFurnitore.merrSipasKFNdermarrjesAndAutorizimeDTFiltered(IdNdermarrja, IdPerdoruesi, Convert.ToInt32(cmbKonfigurimi.Value), filterString,topRows,sort );
            if (dt == null)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ndodhi nje problem gjate leximit te listes! Ju lutem riperserisni veprimin!", pnlMesazhi);
                return;
            }
            mySessionObjects.ruajGrideNeSession(Session, dt);
            ASPxGridView_KF.MbushGride(dt, Session, GuidString, Komponente);
            dt.Dispose();
        }

        /// <summary>
        /// percaktohen templatet per fushat e grides
        /// </summary>
        private void PercaktoTemplateKontakti()
        {
            var col0 = gvKontakti.Columns["Fshi"] as GridViewDataTextColumn;
            col0.DataItemTemplate = new MyButtonTemplate("");
            col0.VisibleIndex = 0;

            var col1 = gvKontakti.Columns["EmerKontakti"] as GridViewDataTextColumn;
            col1.DataItemTemplate = new MyTextTemplate();

            var col2 = gvKontakti.Columns["MbiemerKontakti"] as GridViewDataTextColumn;
            col2.DataItemTemplate = new MyTextTemplate();

            var col3 = gvKontakti.Columns["TelKontakti"] as GridViewDataTextColumn;
            col3.DataItemTemplate = new MyTextTemplate();

            var col4 = gvKontakti.Columns["FaxKontakti"] as GridViewDataTextColumn;
            col4.DataItemTemplate = new MyTextTemplate();

            var col5 = gvKontakti.Columns["CelKontakti"] as GridViewDataTextColumn;
            col5.DataItemTemplate = new MyTextTemplate();

            var col6 = gvKontakti.Columns["EmailKontakti"] as GridViewDataTextColumn;
            col6.DataItemTemplate = new MyTextTemplate();
        }

        private colAdresatKlientFurnitor RuajAdresaDheKodPostar() =>
            string.IsNullOrWhiteSpace(hfAdresa.Value) ? new colAdresatKlientFurnitor() : JsonConvert.DeserializeObject<colAdresatKlientFurnitor>(hfAdresa.Value);

        /// <summary>
        /// ruhet collectioni i kontakteve sipas te dhenave te futura nga perdoruesi
        /// </summary>
        /// <returns></returns>
        private colKontaktiKlientFurnitor RuajKontaktet()
        {
            var kontaktet = new colKontaktiKlientFurnitor();
            var rreshta = gvKontakti.VisibleRowCount + 1;
            var initVal = hfEmer.Value;
            var pars1 = initVal.Split(',');
            var initVal2 = hfMbiemer.Value;
            var pars3 = initVal2.Split(',');
            var initVal3 = hfTel.Value;
            var pars5 = initVal3.Split(',');
            var initVal4 = hfFax.Value;
            var pars7 = initVal4.Split(',');
            var initVal5 = hfCel.Value;
            var pars9 = initVal5.Split(',');
            var initVal6 = hfEmail.Value;
            var pars11 = initVal6.Split(',');

            var emer = new string[rreshta];
            var mbiemer = new string[rreshta];
            var tel = new string[rreshta];
            var fax = new string[rreshta];
            var cel = new string[rreshta];
            var email = new string[rreshta];

            if (initVal != "")//merren te dhenat e hiden fieldeve te trupave te fleteve kontabel nga javascipti
            {
                foreach (var t in pars1)
                {
                    var pars2 = t.Split(':');
                    emer[Convert.ToInt32(pars2[0])] = pars2[1];
                }
            }

            if (initVal2 != "")
            {
                foreach (var t in pars3)
                {
                    var pars4 = t.Split(':');
                    mbiemer[Convert.ToInt32(pars4[0])] = pars4[1];
                }
            }

            if (initVal3 != "")
            {
                foreach (var t in pars5)
                {
                    var pars6 = t.Split(':');
                    tel[Convert.ToInt32(pars6[0])] = pars6[1];
                }
            }

            if (initVal4 != "")
            {
                foreach (var t in pars7)
                {
                    var pars8 = t.Split(':');
                    fax[Convert.ToInt32(pars8[0])] = pars8[1];
                }
            }

            if (initVal5 != "")
            {
                foreach (var t in pars9)
                {
                    var pars10 = t.Split(':');
                    cel[Convert.ToInt32(pars10[0])] = pars10[1];
                }
            }

            if (initVal6 != "")
            {
                foreach (var t in pars11)
                {
                    var pars12 = t.Split(':');
                    email[Convert.ToInt32(pars12[0])] = pars12[1];
                }
            }

            for (var i = 0; i < rreshta - 1; i++)//krijohet kolectioni me trupat e fleteve kontabel e futura nga perdoruesi
            {
                var kontakti = new clsKontaktiKlientFurnitor();
                if (emer[i] != null && emer[i] != "null" && emer[i] != "")
                {
                    kontakti.EmerKontakti = emer[i];

                    if (mbiemer[i] != null && mbiemer[i] != "null" && mbiemer[i] != "")
                        kontakti.MbiemerKontakti = mbiemer[i];

                    if (tel[i] != null && tel[i] != "null" && tel[i] != "")
                        kontakti.TelKontakti = tel[i];

                    if (fax[i] != null && fax[i] != "null" && fax[i] != "")
                        kontakti.FaxKontakti = fax[i];

                    if (cel[i] != null && cel[i] != "null" && cel[i] != "")
                        kontakti.CelKontakti = cel[i];

                    if (email[i] != null && email[i] != "null" && email[i] != "")
                        kontakti.EmailKontakti = email[i];

                    kontaktet.Add(kontakti);
                }
            }

            return kontaktet;
        }

        /// <summary>
        /// konfiguron griden
        /// </summary>
        private void KonfiguroGride()
        {
            string guidString = Convert.ToString(hfState.Get("guidString"));
            if(ASPxGridView_KF.Columns["IdLlogari"] != null)
                KonfigurimComboGride.ShtoLlogari(ASPxGridView_KF, IdPerdoruesi, Session, Komponente, guidString, "IdLlogari");
            if (ASPxGridView_KF.Columns["IdLlogZbritje"] != null)
                KonfigurimComboGride.ShtoLlogari(ASPxGridView_KF, IdPerdoruesi, Session, Komponente, guidString, "IdLlogZbritje");
            if (ASPxGridView_KF.Columns["IdNivelCmimi"] != null)
                KonfigurimComboGride.Shto_NivelCmimi(ASPxGridView_KF, IdNdermarrja, Session, Komponente, guidString);
            if (ASPxGridView_KF.Columns["MaturimiKF"] != null)
                KonfigurimComboGride.Shto_maturimi(ASPxGridView_KF, IdNdermarrja, IdPerdoruesi, Request.QueryString["kf"] == "klient", Session, Komponente, guidString);
            if (ASPxGridView_KF.Columns["QytetiKF"] != null)
                KonfigurimComboGride.ShtoQytetet(ASPxGridView_KF, IdNdermarrja, Session, Komponente, guidString);
            if (ASPxGridView_KF.Columns["IDTVSH"] != null)
                KonfigurimComboGride.ShtoTVSH(ASPxGridView_KF, IdNdermarrja, IdPerdoruesi, Session, Komponente, guidString, "IDTVSH");
            if (ASPxGridView_KF.Columns["TitulliKF"] != null)
                KonfigurimComboGride.ShtoTitull(ASPxGridView_KF, rm, ci);
            if (ASPxGridView_KF.Columns["IdMetoda"] != null)
                KonfigurimComboGride.ShtoMenyrePagese(ASPxGridView_KF, rm, ci, "IdMetoda");
            if (ASPxGridView_KF.Columns["LlojiKF"] != null)
                KonfigurimComboGride.ShtoLlojiKlientFurnitor(ASPxGridView_KF, rm, ci, "LlojiKF");
            if (ASPxGridView_KF.Columns["LlojPorosie"] != null)
                KonfigurimComboGride.ShtoLlojPorosie(ASPxGridView_KF, rm, ci);
            if (ASPxGridView_KF.Columns["Prospekt"] != null)
                KonfigurimComboGride.ShtoProspekt(ASPxGridView_KF, rm, ci);

            PercaktoTamplate();
        }

        /// <summary>
        /// sherben per te ruajtur nje klient furnitor
        /// </summary>
        private void RuajKf()
        {
            if (Page.IsValid == false)
                return;

            bool eshteShtim;

            clsKlientFurnitor kf;
            try
            {
                if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim")
                {
                    kf = KrijoKf(true, hfDraft.Value == "PO");
                    if (hfShtimModifikim.Value == "klonim")
                    {
                        hfArkiva.Set("kopjoArkiven", true);
                        hfArkiva.Set("originalFolder", hfId.Value);
                    }
                }
                else
                    kf = KrijoKf(false, hfDraft.Value == "PO");
            }
            catch (Exception e)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(e.Message);
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, e.Message, pnlMesazhi);
                hfStatusi.Value = "false";
                return;
            }

            var mesazh = new clsMesazh();
            var tedrejtaInfo = new clsTeDrejtaRoli();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, clsFunksione.GetKomponente(Page.Request));

            if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim")
            {
                if (hfDraft.Value == "PO")
                {
                    if (!tedrejtaInfo.DShtimDraft)
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                        hfStatusi.Value = "false";
                        return;
                    }
                }
                else
                {
                    if (!tedrejtaInfo.DShtim)
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                        hfStatusi.Value = "false";
                        return;
                    }
                }
                mesazh = kf.Ruaj(hfNrAutoKF, false, "", "", "", "");
                eshteShtim = true;
            }
            else
            {
                if (hfDraft.Value == "PO")
                {
                    if (!tedrejtaInfo.DModifikimDraft)
                    {

                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                        hfStatusi.Value = "false";
                        return;
                    }
                }
                else if (!tedrejtaInfo.DMod)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                    hfStatusi.Value = "false";
                    return;
                }

                eshteShtim = false;
                var dbRegjistrim = new clsDatabaseRegjistrim();
                var konf = new clsKonfigurimAmbjenti();
                konf.mbushKonfigAmbjSipasId(kf.IdKonfig, IdGjuha);
                kf.IdKlientFurnitor = Convert.ToInt32(hfId.Value);
                var klientiVjeter = new clsKlientFurnitor(kf.IdKlientFurnitor);
                kf.IdKrijuesi = klientiVjeter.IdKrijuesi;
                var lidhur = dbRegjistrim.eshteDokumentiILidhurCelje(kf.IdKlientFurnitor.ToString(), konf.IdNivel.ToString());

                if (!klientiVjeter.Prospekt && hfDraft.Value == "PO")
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Klienti Financiar nuk mund te konvertohet ne klient prospekt!", pnlMesazhi);
                    hfStatusi.Value = "false";
                    return;
                }
                if (lidhur.ToString() != hfLidhur.Value)
                {
                    mesazh.Status = false;
                    mesazh.PershkrimMesazhi = MessagesResource.Messages["lblMsgKfLidhur"];
                    hfStatusi.Value = "false";
                }
                else
                {
                    var dateFunditModKlienti = Convert.ToDateTime(ASPxGridView_KF.GetRowValuesByKeyValue(kf.IdKlientFurnitor, "DtModifikimi"));
                    mesazh = kf.Modifiko(dateFunditModKlienti, false, "0", string.Empty, string.Empty, string.Empty);
                }
                dbRegjistrim.Dispose();
            }

            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                hfStatusi.Value = "false";
            }
            else
            {
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                hfStatusi.Value = "true";
                if (eshteShtim)
                    ShtoKfNeGrid(kf.IdKlientFurnitor);
                else
                    MbushGrideNgaDb();
                    //ModifikoKfNeGrid(kf);//hequr modifikimi sepse mund te ndodh qe modifikimi i bere bie ndesh me filtrin e grides
            }

            pnlMesazhi.Update();
        }

        /// <summary>
        /// krijon nje klientfurnitor sipas te dhenave te futura nga perdoruesi
        /// </summary>
        /// <param name="shtim"></param>
        /// <param name="prospekt"></param>
        /// <param name="idndermarje"></param>
        /// <param name="idperdoruesi"></param>
        /// <param name="rm"></param>
        /// <param name="ci"></param>
        /// <returns></returns>
        private clsKlientFurnitor KrijoKf(bool shtim, bool prospekt)
        {
            var controls = this.GetAsPxTextEditIdValue();
            controls.AddRange(ASPxPageControl1.GetAsPxTextEditIdValue());

            hfNrAuto = (ASPxHiddenField)NrAuto.VendosVleratNrAuto(hfNrAuto, controls);
            hfNrAutoKF = (ASPxHiddenField)NrAuto.ShtoNeRegjistrime(hfNrAutoKF, hfNrAuto, "txtKodi", "KodKlientFurnitor");

            var colLidhje = new colLidhjetAutorizim();

            if (cmbAutorizimiHf.Value != "")
            {
                var pars11 = cmbAutorizimiHf.Value.Split(',');
                colLidhje.AddRange(pars11.Select(kod => new clsLidhjeAutorizim
                {
                    IdAutorizimeKoka = clsAutorizimKoka.ktheIDAutorizim(kod)
                }));
            }

            int idllog = 0,
                titulli,
                idbanka = 0,
                idllogdytesore = 0,
                idllogzbritje = 0,
                limitipara = 0,
                limitibllokues = 0,
                idKatZbritje = 0,
                idNivelCmimi = 0,
                idNivelZbritje = 0,
                grup1 = 0,
                grup2 = 0,
                grup3 = 0,
                idmaturimi = 0,
                idkrijuesi = IdPerdoruesi;
            decimal vleralimit = 0, cmimulet = 0, zbritjetotale = 0;

            if (txtNr2.Text != "")
                idllog = clsLlogari.mbushIDLlogariSipasKodit(txtNr2.Text.Split(';')[0], IdNdermarrja);

            if (cmbTitulli.SelectedIndex == -1)
                titulli = 0;
            else
                int.TryParse(cmbTitulli.SelectedItem.Value.ToString(), out titulli);

            var konfigurimi = new clsKonfigurimAmbjenti
            {
                KodKonfigAmbjente = cmbKonfigurimi.Text,
                IdNdermarje = IdNdermarrja
            };

            konfigurimi = konfigurimi.merrSipasKodit();

            if (clsAlternativaKushti.getAlternativa(konfigurimi.IdKonfigAmbjente, "NI").ToLower() == "po")
            {
                if (string.IsNullOrEmpty(Convert.ToString(txtNipt.Value)))
                    throw new MyException(MessagesResource.Messages["msgPlotesoNIPT"]);

                if (clsKlientFurnitor.EkzistonKlientFurnitorNipt(Convert.ToString(txtNipt.Value), txtKodi.Text.RemoveSpaces(), IdNdermarrja)
                    && txtNipt.Value != null)
                    throw new MyException(MessagesResource.Messages["msgEkzistonkNIPT"]);
            }

            if (!NivelTvshIPranueshem())
                throw new MyException(MessagesResource.Messages["msgNukKeniPercaktuarNivelTVSH"]);

            if (txtEmriBanka.Text != "")
            {
                var banka = new clsBanka();
                banka.mbushBankeSipasKodit(txtEmriBanka.Text, IdNdermarrja);
                idbanka = banka.IdBanka;
            }

            if (!prospekt)
            {
                if (txtLlogKons.Text != "")
                    idllogzbritje = clsLlogari.mbushIDLlogariSipasKodit(txtLlogKons.Text.Split(';')[0], IdNdermarrja);

                if (txtLlogDytesor.Text != "")
                    idllogdytesore = clsLlogari.mbushIDLlogariSipasKodit(txtLlogDytesor.Text.Split(';')[0], IdNdermarrja);
            }

            if (txtLimitiParalajmerues.Text != "")
                int.TryParse(txtLimitiParalajmerues.Text, out limitipara);

            if (txtLimitiBllokues.Text != "")
                int.TryParse(txtLimitiBllokues.Text, out limitibllokues);

            if (txtVleraMin.Text != "")
                decimal.TryParse(txtVleraMin.Text, out vleralimit);

            var prioriteti = cmbPrioriteti.SelectedIndex == -1 ? 0 : int.Parse(cmbPrioriteti.SelectedItem.Value.ToString());

            if (txtCmimUlet.Text != "")
                decimal.TryParse(txtCmimUlet.Text, out cmimulet);

            if (txtZbritjeTotale.Text != "")
                decimal.TryParse(txtZbritjeTotale.Text, out zbritjetotale);

            if (btneKategoriZbritje.Text != "")
                idKatZbritje = clsKokaKategoriZbritje.ktheIdKokaKategoriZbritje(btneKategoriZbritje.Text, IdNdermarrja);

            if (btneNivelZbritje.Text != "")
            {
                if (btneNivelZbritje.Text.Contains(","))
                {
                    throw new MyException("Niveli i zbritjes analitike permban karakteret ',', nuk mund te lidhet me klient/furnitorin");
                }
                idNivelZbritje = DbCore.DbInventari.clsNivelZbritje.ktheIdNivelZbritjeSipasPershkrimit(btneNivelZbritje.Text, IdNdermarrja);
            }

            if (btneGrupimi1.Text != "")
                grup1 = new clsGrupeKF(btneGrupimi1.Text, IdNdermarrja, 1, (cmbLloji.Text == "Klient") ? 0 : 1).IdGrupi;

            if (btneGrupimi2.Text != "")
                grup2 = new clsGrupeKF(btneGrupimi2.Text, IdNdermarrja, 2, (cmbLloji.Text == "Klient") ? 0 : 1).IdGrupi;

            if (btneGrupimi3.Text != "")
                grup3 = new clsGrupeKF(btneGrupimi3.Text, IdNdermarrja, 3, (cmbLloji.Text == "Klient") ? 0 : 1).IdGrupi;

            int idobjektiva;
            if (cmbObjektiva.Text != "")
            {
                var obj = new clsObjektivaKosto(cmbObjektiva.Text, IdNdermarrja);
                idobjektiva = obj.Id;
            }
            else
                idobjektiva = 0;

            var idagjent = 0;
            var idagjent2 = 0;
            var idagjent3 = 0;
            var idklientfurnitorkryesor = 0;

            if (cmbAgjentShitjesh.Text != "")
            {
                var agjent = new clsAgjentShitje(cmbAgjentShitjesh.Text, IdNdermarrja);
                idagjent = agjent.IdAgjentShitje;
            }

            if (btneKlientiKryesor.Text != "")
            {
                var klientfurnitor = new clsKlientFurnitor(btneKlientiKryesor.Text, IdNdermarrja, IdPerdoruesi);
                idklientfurnitorkryesor = klientfurnitor.IdKlientFurnitor;
            }

            if (btnAgjenti2.Text != "")
            {
                var agjent2 = new clsAgjentShitje(btnAgjenti2.Text, IdNdermarrja);
                idagjent2 = agjent2.IdAgjentShitje;
            }
            if (btnAgjenti3.Text != "")
            {
                var agjent3 = new clsAgjentShitje(btnAgjenti3.Text, IdNdermarrja);
                idagjent3 = agjent3.IdAgjentShitje;
            }

            decimal perqindjeAgjent = 0;
            if (txtPerqindjeAgjent.Text != string.Empty)
            {
                var perqindja = decimal.TryParse(txtPerqindjeAgjent.Text, out perqindjeAgjent);
                if (!perqindja)
                    throw new MyException(MessagesResource.Messages["msgPerqindjaEAgjentitNukEshteESakte"]);
            }

            decimal perqindjeAgjent2 = 0;
            if (txtPerqindjeAgjent2.Text != string.Empty)
            {
                var perqindja = decimal.TryParse(txtPerqindjeAgjent2.Text, out perqindjeAgjent2);
                if (!perqindja)
                    throw new MyException(MessagesResource.Messages["msgPeqindjaEAgjentitTeDyteNukEshteESakte"]);
            }

            decimal perqindjeAgjent3 = 0;
            if (txtPerqindjeAgjent3.Text != string.Empty)
            {
                var perqindja = decimal.TryParse(txtPerqindjeAgjent3.Text, out perqindjeAgjent3);
                if (!perqindja)
                    throw new MyException(MessagesResource.Messages["msgPeqindjaEAgjentitTeDyteNukEshteESakte"]);
            }

            var klient = new clsKlientFurnitor();
            var perdoruesi = new clsPerdorues(klient.IdKrijuesi);
            lblKrijuesi.Text = perdoruesi.PerdoruesUsername;

            var monndermarje = clsNdermarrje.ktheIdMonedheNdermSipasID(IdNdermarrja);
            if (btneMaturimi.Text != "")
                int.TryParse(btneMaturimi.Value.ToString(), out idmaturimi);

            var bij = 0;
            if (cmbBij.Text != "")
                int.TryParse(cmbBij.Value.ToString(), out bij);

            var llojporosie = 0;
            if (cmbLlojPorosie.Text != "")
                int.TryParse(cmbLlojPorosie.Value.ToString(), out llojporosie);

            var koordinata = "";
            if (btneCaktoNeHarte.Text != "" && hfState.Contains("geom"))
            {
                var geomsToDeserialize = Convert.ToString(hfState.Get("geom"));
                if (!string.IsNullOrEmpty(geomsToDeserialize))
                {
                    var serializusi = new JavaScriptSerializer();
                    var geoms = (object[])serializusi.DeserializeObject(geomsToDeserialize);
                    koordinata = Convert.ToString(geoms[0]);
                }
            }

            var colFushatShtese = ucFushatShtese.merrFushatShtese();
            var marreveshjet = new colMarreveshjetPerKlient();

            for (var i = 0; i < cblistLlojMarreveshje.SelectedItems.Count; i++)
                marreveshjet.Add(new clsMarreveshjePerKlient(int.Parse(cblistLlojMarreveshje.SelectedItems[i].Value.ToString()), cblistLlojMarreveshje.SelectedItems[i].Text, int.Parse(hfId.Value)));

            var colBuxhet = colBuxhetet.KrijoBuxhetetSipasLlojitTeBuxhetit(hfBuxheti1.Value, hfBuxheti2.Value, new DateTime(1970, 1, 1), 0);
            return new clsKlientFurnitor(txtKodi.Text.RemoveSpaces(), idllog, txtNr2.Text, cmbLloji.Text == "Klient", titulli, cmbTitulli.Text, txtAktiviteti.Text, txtEmertimi.Text.RemoveSpaces(), txtEmerKerkimi.Text, txtNipt.Text, txtQyteti.Text != "" ? Convert.ToInt32(txtQyteti.Value) : 0, txtQyteti.Text, txtShteti.Text, txtTel.Text, txtFax.Text, txtCel.Text, txtEmail.Text, txtWebpage.Text, txtIban.Text, txtLlogariBankare.Text, cbAktiv.Checked, idllogzbritje, txtLlogKons.Text.Split(';')[0], idllogdytesore, txtLlogDytesor.Text.Split(';')[0], (cmbKushtetPageses.Text != "") ? Convert.ToInt32(cmbKushtetPageses.Value) : 0, cmbKushtetPageses.Text, ((cmbMetoda.SelectedIndex != 0 && cmbMetoda.SelectedIndex != -1)) ? Convert.ToInt32(cmbMetoda.Value) : -1, cmbMetoda.Text, idmaturimi, btneMaturimi.Text, idKatZbritje, btneKategoriZbritje.Text, limitipara, limitibllokues, 0, (cmbKushteDergimi.Text != "") ? cmbKushteDergimi.Value.ToString() : "", cmbKushteDergimi.Text, (cmbMenyraTransporti.Text != "") ? cmbMenyraTransporti.Value.ToString() : "", cmbMenyraTransporti.Text, cbOfertaAutomatike.Checked, vleralimit, prioriteti, cmimulet, btneNivelCmimi.Text, idagjent, cmbAgjentShitjesh.Text, 0, 0, idNivelZbritje, btneNivelZbritje.Text, zbritjetotale, IdNdermarrja, IdViti, IdPerdoruesi, konfigurimi.IdKonfigAmbjente, txtLicenca.Text, txtSwift.Text, idbanka, txtEmriBanka.Text, txtAdresaBanka.Text, grup1, grup2, grup3, btneGrupimi1.Text, btneGrupimi2.Text, btneGrupimi3.Text, txtNrTvsh.Text, RuajAdresaDheKodPostar(), RuajKontaktet(), colBuxhet, colFushatShtese, colLidhje, shtim, monndermarje, false, idobjektiva, cmbObjektiva.Text, idkrijuesi, bij, llojporosie, cbKupon.Checked, hfArkiva, rm, ci, koordinata, idagjent2, btnAgjenti2.Text, cbSpecifik.Checked, cbFermer.Checked, cbAutongarkese.Checked, cbShitjePaTvsh.Checked, perqindjeAgjent, perqindjeAgjent2, prospekt, null, txtEmailPerPajisje.Text, marreveshjet, idklientfurnitorkryesor, txtShenime.Text, dteDatelindjaKF.Date, idagjent3, perqindjeAgjent3, btnAgjenti3.Text, cmbNivelTvsh.Text != "" ? Convert.ToInt32(cmbNivelTvsh.Value) : 0, txtEmertimFature.Text, cbLlogaritKomision.Checked, txtKodIntegrimi.Text, txtKodiISKSH.Text, cbMeDogane.Checked,cmbTipiId.Text);
        }

        protected void btnXlsxExport_Click(object sender, EventArgs e)
        {
            try
            {
                gridExport.WriteXlsxToResponse("Klientet", true);
            }
            catch (Exception)
            {
            }
        }

        protected void btnPdfExport_Click(object sender, EventArgs e)
        {
            try
            {
                gridExport.WritePdfToResponse("Klientet", true);
            }
            catch (Exception)
            {
            }
        }

        protected void ASPxGridView_KF_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            var teGjithe = MessagesResource.Messages["GridHeaderFilterFillItemTeGjithe"];
            var nga = MessagesResource.Messages["GridHeaderFilterFillItemNga"];
            if (e.Column.FieldName == "KodKlientFurnitor" || e.Column.FieldName == "EmertimiKF")
            {
                e.Values.Clear();
                e.AddValue(teGjithe, string.Empty, "true");
                e.AddValue(nga + "A-D ", string.Empty, e.Column.FieldName + ">'A     ' and " + e.Column.FieldName + " <'DDDDDDD'");
                e.AddValue(nga + "D-G ", string.Empty, e.Column.FieldName + ">'D     ' and " + e.Column.FieldName + "<'GGGGGGG'");
                e.AddValue(nga + " H-K ", string.Empty, e.Column.FieldName + ">'H     ' and " + e.Column.FieldName + "<'KKKKKKK'");
                e.AddValue(nga + " L-O ", string.Empty, e.Column.FieldName + ">'L     ' and " + e.Column.FieldName + "  <'OOOOOOO'");
                e.AddValue(nga + " P-S ", string.Empty, e.Column.FieldName + ">'P     ' and " + e.Column.FieldName + "<'SSSSSSS'");
                e.AddValue(nga + "T-W ", string.Empty, e.Column.FieldName + ">'T     ' and " + e.Column.FieldName + "<'WWWWWWW'");
                e.AddValue(nga + " X-Z ", string.Empty, e.Column.FieldName + ">'X     ' and " + e.Column.FieldName + "<'ZZZZZZZ'");
            }
            else
            {
                e.Values.Clear();
                e.AddValue(teGjithe, string.Empty, "true");
            }
        }

        /// <summary>
        /// thirret kur grida ben callback
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ASPxGridView_KF_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            if (e.CallbackName == "CUSTOMCALLBACK" && e.Args[0] != "")
            {
                ASPxGridView_KF.FilterExpression = string.Empty;
                MbushGrideNgaDb();
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(IdNdermarrja, "ASPxGridView_KF", ASPxGridView_KF, cmbKonfigurimi.Text.Split(';')[0], 123.ToString(), IdGjuha);
            }
            if (e.CallbackName == "APPLYFILTER" && e.Args[0] == "")
            {
                MbushGrideNgaDb(true);
            }
            else if (this.NdryshimFiltriGrida(ASPxGridView_KF.ID) || e.CallbackName == "SORT")
            {
                MbushGrideNgaDb();
            }
            else if (Request.Params["__CALLBACKPARAM"].Contains("changeConfig"))
            {
                var filterDefault = clsFiltraGrida.MerrFilterDefault(Convert.ToInt32(cmbKonfigurimi.Value));
                if (filterDefault != null && filterDefault.IdFiltra != 0)
                    ASPxGridView_KF.FilterExpression = filterDefault.FiltraVlera;
                hfState.Set("sortColumn", GridUtil.GetSortedColumnFromFilterDefault(Convert.ToInt32(cmbKonfigurimi.Value), filterDefault));
                ASPxGridView_KF.Columns.Clear();
                ASPxGridView_KF.AutoGenerateColumns = true;
                MbushGrideNgaDb();
                KonfiguroGride();
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(IdNdermarrja, "ASPxGridView_KF", ASPxGridView_KF, cmbKonfigurimi.Text.Split(';')[0], 123.ToString(), IdGjuha);
                ASPxGridView_KF.Columns["#"].VisibleIndex = 0;
            }
            else if (e.CallbackName == "COLUMNMOVE" && ASPxGridView_KF.AllColumns[int.Parse(e.Args[0])].Width.Value == 0)
            {
                ASPxGridView_KF.AllColumns[int.Parse(e.Args[0])].Width = Unit.Percentage(3);
                return;
            }

            else if (e.CallbackName == "APPLYFILTER" && e.Args[0] == "")
            {
                var itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
                var cmbFiltra = ((MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
                cmbFiltra.Text = "";

                ASPxGridView_KF.FilterExpression = Request.QueryString["kf"] == "klient" ? "[LlojiKF]=true And [AktivKF] = True" : "[LlojiKF]=false And [AktivKF] = True";
            }

            if (!Request.Params["__CALLBACKPARAM"].Contains("changeConfig") && !string.IsNullOrEmpty(hfState.Get("sortColumn").ToString()))
                OrderColumns();

            ASPxGridView_KF.SaveFilter(IdNdermarrja, Request.QueryString["kf"]);
            PercaktoTamplate();

            GridUtil.ToolTipButonaveMbiGride(ASPxGridView_KF, ci, rm);
        }

        protected void gvBuxheti_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {//thirret kur grida ben callback
        }

        /// <summary>
        /// sherben per te marre me vone vlerat e futura nga perdoruesi me ane te javascriptit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvBuxheti_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType != GridViewRowType.Data) return;

            var colGjendja = ((ASPxGridView)sender).Columns["Gjendja"] as GridViewDataColumn;
            var colBuxh1 = ((ASPxGridView)sender).Columns["Buxheti_1"] as GridViewDataColumn;
            var colBuxh2 = ((ASPxGridView)sender).Columns["Buxheti_2"] as GridViewDataColumn;
            var colDiff1 = ((ASPxGridView)sender).Columns["Diferenca_1"] as GridViewDataColumn;
            var colDiff2 = ((ASPxGridView)sender).Columns["Diferenca_2"] as GridViewDataColumn;
            var lblGjendja = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, colGjendja, "lbl") as ASPxLabel;
            var txtBuxh1 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, colBuxh1, "txtBox") as ASPxTextBox;
            var txtBuxh2 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, colBuxh2, "txtBox") as ASPxTextBox;
            var lblDiff1 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, colDiff1, "txtBox") as ASPxTextBox;
            var lblDiff2 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, colDiff2, "txtBox") as ASPxTextBox;

            if (txtBuxh1 == null || txtBuxh2 == null) return;

            lblGjendja.ClientInstanceName = "labelGjendja" + e.VisibleIndex;
            txtBuxh1.ClientInstanceName = "textboxBuxh1" + e.VisibleIndex;
            txtBuxh2.ClientInstanceName = "textboxBuxh2" + e.VisibleIndex;
            lblDiff1.ClientInstanceName = "labelDiff1" + e.VisibleIndex;
            lblDiff2.ClientInstanceName = "labelDiff2" + e.VisibleIndex;

            if (e.VisibleIndex != 0)
            {
                txtBuxh1.ClientSideEvents.TextChanged = "function(s,e){ ShtoBuxhet1(textboxBuxh1" + e.VisibleIndex + ", labelGjendja" + e.VisibleIndex + ",labelDiff1" + e.VisibleIndex + "," + e.VisibleIndex + ");}";//"," + e.GetValue("Muaj").ToString() + ;
                txtBuxh2.ClientSideEvents.TextChanged = "function(s,e){ShtoBuxhet2(textboxBuxh2" + e.VisibleIndex + ", labelGjendja" + e.VisibleIndex + ",labelDiff2" + e.VisibleIndex + "," + e.VisibleIndex + ");}";//"," + e.GetValue("Muaj").ToString() + ;
            }
            else
            {
                txtBuxh1.ClientSideEvents.TextChanged = "function(s,e){ShtoTotal1(textboxBuxh1" + e.VisibleIndex + ", labelGjendja" + e.VisibleIndex + ",labelDiff1" + e.VisibleIndex + ");}";//"," + e.GetValue("Muaj").ToString() + ;
                txtBuxh2.ClientSideEvents.TextChanged = "function(s,e){ShtoTotal2(textboxBuxh2" + e.VisibleIndex + ", labelGjendja" + e.VisibleIndex + ",labelDiff2" + e.VisibleIndex + ");}";//"," + e.GetValue("Muaj").ToString() + ;
            }
        }

        protected void gvKontakti_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e) =>
            gvKontakti.DataBind();

        /// <summary>
        /// /kur grida ben callback te ruajme te dhenat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvKontakti_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            var key = -1;

            if (e.Parameters != "")
                key = int.Parse(e.Parameters);

            var kontaktet = new colKontaktiKlientFurnitor();
            var rreshta = gvKontakti.VisibleRowCount + 1;
            var initVal = hfEmer.Value;
            var pars1 = initVal.Split(',');
            var initVal2 = hfMbiemer.Value;
            var pars3 = initVal2.Split(',');
            var initVal3 = hfTel.Value;
            var pars5 = initVal3.Split(',');
            var initVal4 = hfFax.Value;
            var pars7 = initVal4.Split(',');
            var initVal5 = hfCel.Value;
            var pars9 = initVal5.Split(',');
            var initVal6 = hfEmail.Value;
            var pars11 = initVal6.Split(',');

            var emer = new string[rreshta];
            var mbiemer = new string[rreshta];
            var tel = new string[rreshta];
            var fax = new string[rreshta];
            var cel = new string[rreshta];
            var email = new string[rreshta];

            if (initVal != "")//merren te dhenat e hiden fieldeve te trupave te fleteve kontabel nga javascipti
            {
                foreach (var s in pars1)
                {
                    var pars2 = s.Split(':');
                    emer[Convert.ToInt32(pars2[0])] = pars2[1];
                }
            }

            if (initVal2 != "")
            {
                foreach (var s in pars3)
                {
                    var pars4 = s.Split(':');
                    mbiemer[Convert.ToInt32(pars4[0])] = pars4[1];
                }
            }

            if (initVal3 != "")
            {
                foreach (var s in pars5)
                {
                    var pars6 = s.Split(':');
                    tel[Convert.ToInt32(pars6[0])] = pars6[1];
                }
            }

            if (initVal4 != "")
            {
                foreach (var s in pars7)
                {
                    var pars8 = s.Split(':');
                    fax[Convert.ToInt32(pars8[0])] = pars8[1];
                }
            }

            if (initVal5 != "")
            {
                foreach (var s in pars9)
                {
                    var pars10 = s.Split(':');
                    cel[Convert.ToInt32(pars10[0])] = pars10[1];
                }
            }

            if (initVal6 != "")
            {
                foreach (var s in pars11)
                {
                    var pars12 = s.Split(':');
                    email[Convert.ToInt32(pars12[0])] = pars12[1];
                }
            }

            for (var i = 0; i < rreshta - 1; i++)//krijohet kolectioni me trupat e fleteve kontabel e futura nga perdoruesi
            {
                var kontakti = new clsKontaktiKlientFurnitor();

                if (emer[i] != null && emer[i] != "null" && emer[i] != "")
                    kontakti.EmerKontakti = emer[i];

                if (mbiemer[i] != null && mbiemer[i] != "null" && mbiemer[i] != "")
                    kontakti.MbiemerKontakti = mbiemer[i];

                if (tel[i] != null && tel[i] != "null" && tel[i] != "")
                    kontakti.TelKontakti = tel[i];

                if (fax[i] != null && fax[i] != "null" && fax[i] != "")
                    kontakti.FaxKontakti = fax[i];

                if (cel[i] != null && cel[i] != "null" && cel[i] != "")
                    kontakti.CelKontakti = cel[i];

                if (email[i] != null && email[i] != "null" && email[i] != "")
                    kontakti.EmailKontakti = email[i];

                kontaktet.Add(kontakti);
            }

            if (key != -1)
                kontaktet.RemoveAt(key);
            else
                kontaktet.Add(new clsKontaktiKlientFurnitor());

            if (kontaktet.Count == 0)
                kontaktet.Add(new clsKontaktiKlientFurnitor());

            gvKontakti.DataSource = kontaktet;
            gvKontakti.DataBind();
            PercaktoTemplateKontakti();
        }

        protected void gvKontakti_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e) =>
            e.Properties["cpNoRows"] = gvKontakti.VisibleRowCount;

        protected void gvKontakti_DataBound(object sender, EventArgs e)
        {
            if (gvKontakti.Columns["Fshi"] != null) return;

            var fshi = new GridViewDataTextColumn
            {
                Caption = MessagesResource.Messages["lblFshiBtn"],
                Width = 50
            };

            gvKontakti.Columns.Add(fshi);
            gvKontakti.KeyFieldName = "IdKontaktiKlientFurnitor";
            gvKontakti.SettingsBehavior.AllowSelectByRowClick = false;
            gvKontakti.SettingsBehavior.AllowFocusedRow = true;
        }

        protected void gvKontakti_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType == GridViewRowType.Data)
            {
                var col0 = ((ASPxGridView)sender).Columns["Fshi"] as GridViewDataTextColumn;
                var col1 = ((ASPxGridView)sender).Columns["EmerKontakti"] as GridViewDataTextColumn;
                var col2 = ((ASPxGridView)sender).Columns["MbiemerKontakti"] as GridViewDataTextColumn;
                var col3 = ((ASPxGridView)sender).Columns["TelKontakti"] as GridViewDataTextColumn;
                var col4 = ((ASPxGridView)sender).Columns["FaxKontakti"] as GridViewDataTextColumn;
                var col5 = ((ASPxGridView)sender).Columns["CelKontakti"] as GridViewDataTextColumn;
                var col6 = ((ASPxGridView)sender).Columns["EmailKontakti"] as GridViewDataTextColumn;
                var btn0 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col0, "btn") as ASPxButton;
                var txt1 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col1, "txtBox") as ASPxTextBox;
                var txt2 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col2, "txtBox") as ASPxTextBox;
                var txt3 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col3, "txtBox") as ASPxTextBox;
                var txt4 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col4, "txtBox") as ASPxTextBox;
                var txt5 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col5, "txtBox") as ASPxTextBox;
                var txt6 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col6, "txtBox") as ASPxTextBox;

                var ugjet = false;

                //vendosen client side eventet e kolonave
                if (btn0 != null)
                {
                    btn0.ClientInstanceName = "btnFshi" + e.VisibleIndex;
                    btn0.ClientSideEvents.Click = "function(s,e){FshiClicked(" + e.VisibleIndex + ");}";
                }

                if (txt1 != null)
                {
                    txt1.ClientInstanceName = "txtEmer" + e.VisibleIndex;
                    txt1.ClientSideEvents.TextChanged = "function(s,e){TextChangedEmer(txtEmer" + e.VisibleIndex + "," + e.VisibleIndex + ");}";

                    if (e.VisibleIndex == ((ASPxGridView)sender).VisibleRowCount - 2)
                    {
                        if (ugjet)
                        {
                            _temptxt = txt1;
                            ugjet = false;
                        }
                    }
                }

                if (txt2 != null)
                {
                    txt2.ClientInstanceName = "txtMbiemer" + e.VisibleIndex;
                    txt2.ClientSideEvents.TextChanged = "function(s,e){TextChangedMbiemer(txtMbiemer" + e.VisibleIndex + "," + e.VisibleIndex + ");}";

                    if (e.VisibleIndex == ((ASPxGridView)sender).VisibleRowCount - 2)
                    {
                        if (ugjet)
                        {
                            _temptxt = txt2;
                            ugjet = false;
                        }
                    }
                }

                if (txt3 != null)
                {
                    txt3.ClientInstanceName = "txtTel" + e.VisibleIndex;
                    txt3.ClientSideEvents.TextChanged = "function(s,e){TextChangedTel(txtTel" + e.VisibleIndex + ", " + e.VisibleIndex + ");}";

                    if (e.VisibleIndex == ((ASPxGridView)sender).VisibleRowCount - 2)
                    {
                        if (ugjet)
                        {
                            _temptxt = txt3;
                            ugjet = false;
                        }
                    }
                }

                if (txt4 != null)
                {
                    txt4.ClientInstanceName = "txtFax" + e.VisibleIndex;
                    txt4.ClientSideEvents.TextChanged = "function(s,e){TextChangedFax(txtFax" + e.VisibleIndex + ", " + e.VisibleIndex + ");}";

                    if (e.VisibleIndex == ((ASPxGridView)sender).VisibleRowCount - 2)
                    {
                        if (ugjet)
                        {
                            _temptxt = txt4;
                            ugjet = false;
                        }
                    }
                }

                if (txt5 != null)
                {
                    txt5.ClientInstanceName = "txtCel" + e.VisibleIndex;
                    txt5.ClientSideEvents.TextChanged = "function(s,e){TextChangedCel(txtCel" + e.VisibleIndex + ", " + e.VisibleIndex + ");}";

                    if (e.VisibleIndex == ((ASPxGridView)sender).VisibleRowCount - 2)
                    {
                        if (ugjet)
                        {
                            _temptxt = txt5;
                            ugjet = false;
                        }
                    }
                }

                if (txt6 != null)
                {
                    txt6.ClientInstanceName = "txtEmail" + e.VisibleIndex;
                    txt6.ClientSideEvents.TextChanged = "function(s,e){TextChangedEmail(txtEmail" + e.VisibleIndex + ", " + e.VisibleIndex + ");}";

                    if (e.VisibleIndex == ((ASPxGridView)sender).VisibleRowCount - 2)
                    {
                        if (ugjet)
                        {
                            _temptxt = txt6;
                            ugjet = false;
                        }
                    }
                }
            }

            if (_temptxt != null)
            {
                _temptxt.Focus();
            }
        }

        private bool NivelTvshIPranueshem()
        {
            if (!clsAtributeTrupi.IsRequiredField(Convert.ToInt32(cmbKonfigurimi.Value.ToString()), "cmbNivelTvsh", 123))
                return true;

            return !(cmbNivelTvsh.Value == null || Convert.ToInt32(cmbNivelTvsh.Value.ToString()) == -1);
        }

        private void PercaktoTamplate()
        {
            if (ASPxGridView_KF.Columns["AktivKF"] == null)
                return;
            var col = ASPxGridView_KF.Columns["AktivKF"] as GridViewDataColumn;
            col.DataItemTemplate = new MyCheckTemplate(true, false);
        }

        protected void cmbLloji_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void ASPxGridView_KF_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            var arr = e.Parameters.Split(';');
            bool getFromDb = false;
            string filterDefault = Request.QueryString["kf"] == "klient" ? "[LlojiKF]=true And [AktivKF] = True" : "[LlojiKF]=false And [AktivKF] = True";

            switch (arr.Length)
            {
                case 1:
                    if (this.NdryshimFiltriGrida(ASPxGridView_KF.ID))
                        return;
                    if (arr[0] == "ndryshimMenyreFiltrimi" && !string.IsNullOrEmpty(ASPxGridView_KF.FilterExpression))
                        getFromDb = true;
                    break;
                case 3:
                    if (arr[2] == "" || arr[2] == "changeConfig")
                    {
                        ASPxGridView_KF.FilterExpression = filterDefault;
                        break;
                    }
                    var filtra = new clsFiltraGrida();
                    var koka = new clsGridaKoka(mySessionObjects.ktheGjuhe(Session), "ASPxGridView_KF", Komponente, IdNdermarrja, int.Parse(cmbKonfigurimi.Value.ToString()));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], IdNdermarrja, koka.IdGridaKoka);

                    if (filtra.FiltraKodi != null)
                    {
                        ASPxGridView_KF.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, ASPxGridView_KF);
                        getFromDb = true;
                        hfStatusi.Value = "true";
                    }
                    else hfStatusi.Value = "false";
                    break;
                default:
                    getFromDb = false;
                    break;
            }

            if (getFromDb)
            {
                MbushGrideNgaDb();
                KonfiguroGride();
            }

            //ASPxGridView_KF.Selection.UnselectAll();
            //var itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            //var cmbFiltra = ((MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            //if (cmbFiltra.Text == "")
            //    ASPxGridView_KF.FilterExpression = filterDefault;
        }       

        protected void txtNr2_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("txtNr2"))
                ConfigureAspxComboBox.KonfiguroComboBoxLlogaria(txtNr2, IdPerdoruesi, IdNdermarrja, e);
        }

        protected void txtNr2_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("txtNr2"))
                ConfigureAspxComboBox.KonfiguroComboBoxLlogaria(txtNr2, IdPerdoruesi, IdNdermarrja, e);
        }

        protected void txtLlogDytesor_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("txtLlogDytesor"))
                ConfigureAspxComboBox.KonfiguroComboBoxLlogaria(txtLlogDytesor, IdPerdoruesi, IdNdermarrja, e);
        }

        protected void txtLlogDytesor_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("txtLlogDytesor"))
                ConfigureAspxComboBox.KonfiguroComboBoxLlogaria(txtLlogDytesor, IdPerdoruesi, IdNdermarrja, e);
        }

        protected void txtLlogKons_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("txtLlogKons"))
                ConfigureAspxComboBox.KonfiguroComboBoxLlogaria(txtLlogKons, IdPerdoruesi, IdNdermarrja, e);
        }

        protected void txtLlogKons_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("txtLlogKons"))
                ConfigureAspxComboBox.KonfiguroComboBoxLlogaria(txtLlogKons, IdPerdoruesi, IdNdermarrja, e);
        }

        protected void txtNr_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].EndsWith("txtNr"))
                ConfigureAspxComboBox.KonfiguroComboBoxLlogaria(txtNr, IdPerdoruesi, IdNdermarrja, e);
        }

        protected void txtNr_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].EndsWith("txtNr"))
                ConfigureAspxComboBox.KonfiguroComboBoxLlogaria(txtNr, IdPerdoruesi, IdNdermarrja, e);
        }

        protected void txtNr4_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("txtNr4"))
                ConfigureAspxComboBox.KonfiguroComboBoxLlogaria(txtNr4, IdPerdoruesi, IdNdermarrja, e);
        }

        protected void txtNr4_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("txtNr4"))
                ConfigureAspxComboBox.KonfiguroComboBoxLlogaria(txtNr4, IdPerdoruesi, IdNdermarrja, e);
        }

        protected void cmbKushteDergimi_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("cmbKushteDergimi"))
                ConfigureAspxComboBox.KonfiguroComboBoxKushteDergimi(cmbKushteDergimi, IdNdermarrja);
        }

        protected void cmbMenyraTransporti_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("cmbMenyraTransporti"))
                ConfigureAspxComboBox.KonfiguroComboBoxMenyraTransporti(cmbMenyraTransporti, IdNdermarrja);
        }

        protected void btneKlientiKryesor_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("btneKlientiKryesor"))
                btneKlientiKryesor.ConfigureAndFill(() => colKlienteFurnitore.merrSipasKFNdermarrjesAndAutorizimeDT(IdNdermarrja, IdPerdoruesi), "KodKlientFurnitor", "IdKlientFurnitor");
        }

        protected void btneKlientiKryesor_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("btneKlientiKryesor") && !string.IsNullOrWhiteSpace(e.Filter))
                ConfigureAspxComboBox.KonfiguroComboBoxKlientFurnitor(e.Filter, e.BeginIndex + 1, e.EndIndex + 1, IdPerdoruesi, IdNdermarrja, btneKlientiKryesor, 0);
        }

        protected void btneNivelCmimi_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("btneNivelCmimi"))
                ConfigureAspxComboBox.KonfiguroComboBoxNiveleCmimeshPrind(btneNivelCmimi, IdNdermarrja, e.Value);
        }

        protected void btneNivelCmimi_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("btneNivelCmimi"))
                ConfigureAspxComboBox.KonfiguroComboBoxNiveleCmimeshPrind(IdNdermarrja, btneNivelCmimi, e.Filter, e.BeginIndex, e.EndIndex);
        }

        protected void cmbNivelTvsh_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("cmbNivelTvsh"))
                ConfigureAspxComboBox.KonfiguroComboBoxTaksat(IdPerdoruesi, IdNdermarrja, cmbNivelTvsh, LlojTakse.Nivel_Tvsh, false);
        }


        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {
            if (e.Item.Name == "Ruaj" || e.Item.Name == "Draft")
            {
                Page.Validate("entries");
                RuajKf();
            }
        }

        protected void ASPxGridView_KF_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = ASPxGridView_KF.PageIndex;
            e.Properties["cpPageRow"] = ASPxGridView_KF.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = ASPxGridView_KF.VisibleRowCount;
            e.Properties["cpSortedColumn"] = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje(String.Empty, ASPxGridView_KF); //GetSortedColumns();
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
            var koka = new clsGridaKoka(IdGjuha, "ASPxGridView_KF", Komponente, IdNdermarrja, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, IdNdermarrja, koka.IdGridaKoka);

            if (filtra.FiltraKodi == null) return;

            filtra.IdPerdoruesi = IdPerdoruesi;

            var mesazh = filtra.fshi();

            cmbFiltra.Text = "";
            clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, IdNdermarrja, "ASPxGridView_KF", int.Parse(cmbKonfigurimi.Value.ToString()), Komponente);

            PercaktoTemplateMenu();

            ASPxGridView_KF.FilterExpression = Request.QueryString["kf"] == "klient" ? "[LlojiKF]=true And [AktivKF] = True" : "[LlojiKF]=false And [AktivKF] = True";

            if (mesazh.Status)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

            hfStatusi.Value = "true";
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
            var koka = new clsGridaKoka(IdGjuha, "ASPxGridView_KF", Komponente, IdNdermarrja, int.Parse(cmbKonfigurimi.Value.ToString()));
            var filtri = new clsFiltraGrida
            {
                FiltraKodi = cmbFiltra.Text,
                FiltraShenime = cmbFiltra.Text,
                FiltraUniversal = false,
                IdPerdoruesi = IdPerdoruesi,
                IdNdermarje = IdNdermarrja,
                IdStatusDok = 1,
                GridaKokaId = koka.IdGridaKoka,
                FiltraVlera = ASPxGridView_KF.FilterExpression
            };
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("KodKlientFurnitor", ASPxGridView_KF);
            //var kolona = ASPxGridView_KF.GetSortedColumns();
            //if (kolona.Count > 0)
            //{
            //    filtri.KoloneRenditje = kolona[0].FieldName;
            //    filtri.DrejtimRenditje = kolona[0].SortOrder == ColumnSortOrder.Ascending;
            //}
            //else
            //{
            //    filtri.KoloneRenditje = "KodKlientFurnitor";
            //    filtri.DrejtimRenditje = true;
            //}

            var mesazh = filtri.ruaj();
            clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, IdNdermarrja, "ASPxGridView_KF", int.Parse(cmbKonfigurimi.Value.ToString()), Komponente);
            PercaktoTemplateMenu();
            if (mesazh.Status)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            hfStatusi.Value = "true";
            cmbFiltra.Text = "";
        }

        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            var rreshtat = ASPxPageControl1.ActiveTabIndex == 0 ? ASPxGridView_KF.GetSelectedFieldValues("IdKlientFurnitor") : new List<object> { hfId.Value };

            if (rreshtat.Count == 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgZgjidhniNjekf"], pnlMesazhi);
                return;
            }

            var mesazh = new clsMesazh();
            List<string> teFshire = new List<string>(), tePaFshire = new List<string>();

            var dbRegjistrim = new clsDatabaseRegjistrim();
            foreach (var id in rreshtat)
            {
                var oKlientFurnitor = new clsKlientFurnitor(Convert.ToInt32(id));
                var konf = new clsKonfigurimAmbjenti();
                konf.mbushKonfigAmbjSipasId(oKlientFurnitor.IdKonfig, IdGjuha);
                oKlientFurnitor.IdPerdoruesi = IdPerdoruesi;
                var lidhur = dbRegjistrim.eshteDokumentiILidhurCelje(oKlientFurnitor.IdKlientFurnitor.ToString(), konf.IdNivel.ToString());

                if (lidhur)
                {
                    tePaFshire.Add(oKlientFurnitor.KodKlientFurnitor);
                    continue;
                }

                mesazh = oKlientFurnitor.FshiStatus();

                if (oKlientFurnitor.IdKlientFurnitor == 0)
                    continue;

                if (!mesazh.Status) continue;

                HiqKfNgaGrida(oKlientFurnitor.IdKlientFurnitor);
                teFshire.Add(oKlientFurnitor.KodKlientFurnitor);
                ASPxPageControl1.ActiveTabIndex = 0;
                hfStatusi.Value = "true";
            }
            dbRegjistrim.Dispose();

            string mesazhInfoGabim = "", mesazhInfoSukses = "";
            if (tePaFshire.Count == 1)
                mesazhInfoGabim = string.Format("{0}{1}{2}", rm.GetString("msgKfMeKod", ci), string.Join(";", tePaFshire), rm.GetString("msgBlerjeShitjeNukFshihetNjejes", ci));
            else if (tePaFshire.Count > 1)
                mesazhInfoGabim = string.Format("{0}{1}{2}", rm.GetString("msgKfMeKodShumes", ci), string.Join(";", tePaFshire), rm.GetString("msgBlerjeShitjeNukFshihetShumes", ci));

            if (teFshire.Count == 1)
                mesazhInfoSukses = string.Format("{0}{1}{2}", rm.GetString("msgKfMeKod", ci), string.Join(";", teFshire), rm.GetString("suffixMesazhNjejesSuksesi", ci));
            else if (teFshire.Count > 1)
                mesazhInfoSukses = string.Format("{0}{1}{2}", rm.GetString("msgKfMeKodShumes", ci), string.Join(";", teFshire), rm.GetString("msgClejeArkaBankaSuffixShumesSuksesi", ci));

            if (mesazhInfoGabim != "" && mesazhInfoSukses != "")
                mesazhInfoGabim += rm.GetString("msgLidhesMesazhi", ci) + mesazhInfoSukses;

            if (mesazhInfoGabim != "")
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhInfoGabim, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazhInfoSukses, pnlMesazhi);

            pnlMesazhi.Update();
        }

        private void HiqKfNgaGrida(int idKf)
        {
            if (ASPxGridView_KF.DataSource != null)
            {
                var dt = (DataTable)ASPxGridView_KF.DataSource;
                var drs = dt.Select("IdKlientFurnitor = " + idKf);

                if (drs.Length > 1)
                    throw new Exception(MessagesResource.Messages["msgExceptionkfGride"]);

                if (drs.Length == 0) return;

                var dr = drs[0];
                dt.Rows.Remove(dr);
                ASPxGridView_KF.DataBind();
            }
            else
                MbushGrideNgaDb();
        }

        private void ShtoKfNeGrid(int idKf)
        {
            if (ASPxGridView_KF.DataSource != null)
            {
                var dt = (DataTable)ASPxGridView_KF.DataSource;
                var drs = dt.Select("IdKlientFurnitor = " + idKf);

                if (drs.Length > 0)
                    throw new Exception(MessagesResource.Messages["msgExceptionkfEkziston"]);

                var newArtDr = colKlienteFurnitore.merrSipasKFNdermarrjesAndAutorizimeDR(IdNdermarrja, IdPerdoruesi, idKf);
                dt.ImportRow(newArtDr);
            }
            else
                MbushGrideNgaDb();
        }

        private void ModifikoKfNeGrid(clsKlientFurnitor Kf)
        {
            if (ASPxGridView_KF.DataSource != null)
            {
                var dt = (DataTable)ASPxGridView_KF.DataSource;


                var op = CriteriaOperator.Parse(ASPxGridView_KF.FilterExpression, 0);
                var filterString = CriteriaToWhereClauseHelper.GetMsSqlWhere(op);
                if (!string.IsNullOrWhiteSpace(filterString))
                    filterString = $"{filterString} AND";
                //plotesojme filtrin dhe kontrollojme neqoftese klienti me modifikimet e bera eshte pjese e ds ne gride 
                var drs = dt.Select("IdKlientFurnitor = " + Kf.IdKlientFurnitor);
                if (drs.Length > 1)
                    throw new Exception(MessagesResource.Messages["msgExceptionkfGride"]);
                if (drs.Length == 0)
                    return;
                dt.Rows.Remove(drs[0]);//fshijme rreshtin e modifikuar 
                var dr = drs[0];
                var newArtDr = colKlienteFurnitore.merrSipasKFNdermarrjesAndAutorizimeDTFiltered(IdNdermarrja, IdPerdoruesi, Convert.ToInt32(cmbKonfigurimi.Value), $"{filterString} IdKlientFurnitor = {Kf.IdKlientFurnitor}", string.Empty, string.Empty);
                if (newArtDr.Rows.Count != 0) {//nqs klienti i modifikuar i  bindet filtrit te grides e shtojm perseri ne ds 
                    var arr = newArtDr.Rows[0].ItemArray;
                    dr.ItemArray = arr;
                }
                ASPxGridView_KF.DataBind();
            }
            else
                MbushGrideNgaDb();
        }

        protected void ASPxGridView_KF_DataBound(object sender, EventArgs e)
        {
            if (ASPxGridView_KF.Columns.Count == 0 || ASPxGridView_KF.Columns["#"] != null)
                return;

            //behet nepermjet kodit afishimi i checkboxit qe do perdoret per 
            //perzgjidh
            var check = new GridViewCommandColumn("#")
            {
                ShowSelectCheckbox = true,
                Width = Unit.Percentage(2)
            };
            ASPxGridView_KF.Settings.ShowFilterRow = true;
            ASPxGridView_KF.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
            ASPxGridView_KF.Settings.ShowFilterRowMenu = true;
            ASPxGridView_KF.Columns.Add(check);
            ASPxGridView_KF.KeyFieldName = "IdKlientFurnitor";
            ASPxGridView_KF.SettingsBehavior.AllowSelectByRowClick = true;
            ASPxGridView_KF.SettingsBehavior.AllowFocusedRow = true;
            ASPxGridView_KF.Columns["#"].VisibleIndex = 0;
        }

        protected void ASPxGridView_KF_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName != "AktivKF") return;

            (e.Editor as ASPxComboBox).Items.Clear();
            (e.Editor as ASPxComboBox).Items.Add("");
            (e.Editor as ASPxComboBox).Items.Add("Aktive", true);
            (e.Editor as ASPxComboBox).Items.Add("Jo Aktive", false);
        }

        protected void ASPxGridView_KF_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            int vlerePerKrahasim = -2;

            switch (e.Column.FieldName)
            {
                case "TitulliKF":
                case "IdNivelCmimi":
                case "IdLlogari":
                case "IdLlogZbritje":
                case "QytetiKF":
                case "MaturimiKF":
                    vlerePerKrahasim = 0;
                    break;
                case "IdMetoda":
                    vlerePerKrahasim = -1;
                    break;
            }

            if (Converter.ConvertToInt(e.Value) == vlerePerKrahasim)
                e.Criteria = null;
        }

        protected void ASPxPageControl1_ActiveTabChanged(object source, TabControlEventArgs e)
        {

        }

        protected void txtEmriBanka_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (!IsCallback)
                return;

            if (!Request.Params["__CALLBACKID"].Contains("txtEmriBanka") || e.Value == null)
                return;

            ConfigureAspxComboBox.KonfiguroComboBoxBankatSipasFiltrit(txtEmriBanka, IdPerdoruesi, IdNdermarrja, new clsLlogari(txtNr2.Text, IdNdermarrja).IdMonedha, e.Value);
        }

        protected void txtEmriBanka_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (!IsCallback)
                return;

            if (!Request.Params["__CALLBACKID"].Contains("txtEmriBanka"))
                return;

            ConfigureAspxComboBox.KonfiguroComboBoxComboBankatSipasFiltrit(IdPerdoruesi, IdNdermarrja, txtEmriBanka, new clsLlogari(txtNr2.Text, IdNdermarrja).IdMonedha, e.Filter, e.BeginIndex, e.EndIndex);
        }

        protected void cmbBij_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("cmbBij"))
                ConfigureAspxComboBox.KonfiguroComboBoxNdermarjeBij(IdNdermarrja, cmbBij);
        }

        /// <summary>
        /// Metode qe sherben per te kaluar perkthime te js
        /// </summary>
        private void MbushHiddenFieldMePerkthime()
        {
            hfState.Set("labelBlerjeShitjeAfateMaturimi", rm.GetString("labelBlerjeShitjeAfateMaturimi", ci));
            hfState.Set("msgZgjdhniNjeNgaElementetEListes", rm.GetString("msgZgjdhniNjeNgaElementetEListes", ci));
            hfState.Set("msgDuhetZgjidhnikf", rm.GetString("msgDuhetZgjidhnikf", ci));
            hfState.Set("regjisDokZgjidhDokPerTeBashkengjitur", rm.GetString("regjisDokZgjidhDokPerTeBashkengjitur", ci));
            hfState.Set("msgZgjidhniAutorizimet", rm.GetString("msgZgjidhniAutorizimet", ci));
            hfState.Set("msgZgjidhKatZbritjeLupa", rm.GetString("msgZgjidhKatZbritjeLupa", ci));
            hfState.Set("msgNivelCmimiPrindLupa", rm.GetString("msgNivelCmimiPrindLupa", ci));
            hfState.Set("msgZgjidhNivelZbritjeLupa", rm.GetString("msgZgjidhNivelZbritjeLupa", ci));
            hfState.Set("msgZgjidhKushtePageseLupa", rm.GetString("msgZgjidhKushtePageseLupa", ci));
            hfState.Set("msgZgjidhKushteDergimiLupa", rm.GetString("msgZgjidhKushteDergimiLupa", ci));
            hfState.Set("msgZgjidhMenyreTransportiLupa", rm.GetString("msgZgjidhMenyreTransportiLupa", ci));
            hfState.Set("msgZgjidhAgjentShitjeLupa", rm.GetString("msgZgjidhAgjentShitjeLupa", ci));
            hfState.Set("msgNivelCmimiNukPerdoretKliente", rm.GetString("msgNivelCmimiNukPerdoretKliente", ci));
            hfState.Set("msgNivelCmimiNukPerdoretFurnitore", rm.GetString("msgNivelCmimiNukPerdoretFurnitore", ci));
            hfState.Set("msgZgjidhniObjektivenEKostos", rm.GetString("msgZgjidhniObjektivenEKostos", ci));
            hfState.Set("msgZgjidhniNdermarjeBij", rm.GetString("msgZgjidhniNdermarjeBij", ci));
            hfState.Set("msgZgjidhgrupinkflupa", rm.GetString("msgZgjidhgrupinkflupa", ci));
            hfState.Set("msgZgjidhArkenBankenLupa", rm.GetString("msgZgjidhArkenBankenLupa", ci));
            hfState.Set("headerZgjidhKlientFurnitorin", rm.GetString("headerZgjidhKlientFurnitorin", ci));
            hfState.Set("msgMonLlogParapNjejte", rm.GetString("msgMonLlogParapNjejte", ci));
            hfState.Set("msgAgjentZgjedhurNjeHere", rm.GetString("msgAgjentZgjedhurNjeHere", ci));
            hfState.Set("msgZgjidhKf", rm.GetString("headerZgjidhKlientFurnitorin", ci));
            hfState.Set("msgPerqindjaDuhetJeteNumer", rm.GetString("msgPerqindjaDuhetJeteNumer", ci));
            hfState.Set("msgPerqindjaVleraNdermjet", rm.GetString("msgPerqindjaVleraNdermjet", ci));
            hfState.Set("msgnumRreshtashSelektuar", rm.GetString("msgnumRreshtashSelektuar", ci));
            hfState.Set("msgZgjidhniNjekf", rm.GetString("msgZgjidhniNjekf", ci));
            hfState.Set("headerPopUpText", MessagesResource.Messages["headerPopUpText"]);
        }

        private void EmratEKontrolleve()
        {
            konfigurimi_Label.Text = MessagesResource.Messages["lblModeli"];
        }

        private void OrderColumns()
        {
            string sortHfString = hfState.Get("sortColumn").ToString();
            if (string.IsNullOrEmpty(sortHfString))
                return;
            GridUtil.renditGriden(sortHfString, ASPxGridView_KF);
            //var sortColumn = sortHfString.Split(';');
            //var fieldName = sortColumn[0];
            //var sortOrder = sortColumn[1] == "Descending" ? DevExpress.Data.ColumnSortOrder.Descending : DevExpress.Data.ColumnSortOrder.Ascending;
            //ASPxGridView_KF.DataColumns[fieldName].SortOrder = sortOrder;
        }

        private string GetSortedColumns()
        {
            var sortedColumns = ASPxGridView_KF.GetSortedColumns();
            if (sortedColumns.Count < 1)
                return string.Empty;
            else
            {
                string fieldName = sortedColumns[0].FieldName;
                string sortOrder = sortedColumns[0].SortOrder.ToString();
                return $"{fieldName} {sortOrder}";
            }
        }
    }
}