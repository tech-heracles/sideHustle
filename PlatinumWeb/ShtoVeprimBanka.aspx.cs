using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DbCore;
using DbCore.DbAdmin;
using DbCore.DbArkaBanka;
using DbCore.DbInventari;
using DbCore.DbKontabiliteti;
using DbCore.DbQendraKosto;
using DbCore.DbRegjistrim;
using DbCore.DbListPagesat;
using DbCore.DbShare;
using DbCore.IMBUtils.Extensions;
using DbCore.IMBUtils.Logging;
using DevExpress.Utils;
using DevExpress.Web;
using Newtonsoft.Json;
using NLog;
using PlatinumWeb.Templates;
using DbCore.Integrime;
using NLog.Internal;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.Messages;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Fiskalizimi.API;
using DbCore.IMBUtils.Fiskalizimi.Controls;

namespace PlatinumWeb
{
    public partial class ShtoVeprimBanka : MyPageBase
    {
        private clsFormatiKonfig _formatNrKonfig = new clsFormatiKonfig();
        private static string[] _onesMapping =
            {
            "Zero", "Nje", "Dy", "Tre", "Kater", "Pese", "Gjashte", "Shtate", "Tete", "Nente",
            "Dhjete", "Njembedhjete", "Dymbedhjete", "Trembedhjete", "Katermbedhjete", "Pesembedhjete",
            "Gjashtembedhjete", "Shtatembedhjete", "Tetembedhjete", "Nentembedhjete"
        };
        private static string[] _tensMapping =
            {
            "Njezet", "Tridhjete", "Dyzet", "Pesedhjete", "Gjashtedhjete", "Shtatedhjete", "Tetedhjete", "Nentedhjete"
        };
        private static string[] _groupMapping =
            {
            "Qind", "Mije", "Milion", "Miliard", "Trilion"
        };
        private ResourceManager rm => new ResourceManager("Resources.Strings", Assembly.Load("App_GlobalResources"));
        private CultureInfo ci => mySessionObjects.ktheCultureInfo(Session);
        int idKatDokShitje, idKategori;
        private string guidString, veprimi, komponente;
        private bool clsKontrolleFiskalizimi;

        protected void Page_Load(object sender, EventArgs e)
        {
            komponente = clsFunksione.GetKomponente(Page.Request);
            ASPxGridView gridFaturat = null;
            if (!IsPostBack)
            {
                if (mySessionObjects.ktheKodNdermarrje(Session) == "")
                {
                    Response.Redirect("Login_Ndermarrje.aspx?id=" + IdPerdoruesi);
                    return;
                }
                if (!mySessionObjects.isLogedIn(Session))
                    clsFunksione.logout(Session, true, "FaqePaautorizuar");

                var periudha = mySessionObjects.merrPeriudheKontabel(Session);
                hfHapurMbyllur.Value = DbCore.DbAdmin.clsPerdorues.ktheInfoHapur(IdPerdoruesi);
                if (periudha != null)
                {
                    btnPeriudha.Text = periudha.NrPeriudha.ToString();
                    lblPeriudhaAktuale.Text = $"{periudha.FillimiPeriudha.ToShortDateString()}-{periudha.MbarimiPeriudha.ToShortDateString()}";
                }
                if (KlientSpecifik.VodafoneShops.ToString().EqualsIgnoreCase(clsServerConfiguration.LexoKonfigurimSipasKey<string>(ServerKonfigKey.Klienti)))
                    hfState.Set("vodafoneShops", true);
                else
                    hfState.Set("vodafoneShops", false);
                veprimi = Request.QueryString["lloji"] != "" ? Request.QueryString["lloji"] : "Terheqje";
                idKatDokShitje = (veprimi.EqualsAnyIgnoreCase("terheqje", "pagese")) ? 2 : 1;
                idKategori = (veprimi.EqualsAnyIgnoreCase("derdhje", "terheqje")) ? 4 : 3;
                hfArkivaDokId.Value = Request.QueryString["id"];
                hfState.Set("idKatDokShitje", idKatDokShitje);
                hfState.Set("idKategori", idKategori);
                hfState.Set("idPerdoruesi", IdPerdoruesi);
                hfState.Set("idGjuha", IdGjuha);
                hfState.Set("komponente", komponente);
                hfState.Set("idNdermarrje", IdNdermarrja);
                hfState.Set("idViti", IdViti);
                hfState.Set("idNdermarrjeVit", IdNdermarrjeVit);
                hfState.Set("veprimi", veprimi);
                guidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                hfState.Set("guidString", guidString);
                hfState.Add("IdMonedha", clsNdermarrje.ktheIdMonedheNdermSipasID(IdNdermarrja));
                hfState.Set("periudha", JsonConvert.SerializeObject(mySessionObjects.merrPeriudheKontabel(Session)));
                hfMonedhaNder.Value = clsMonedha.ktheMonedhenENdermarrjes(IdNdermarrja);
                hfIdMonedhaNder.Value = clsNdermarrje.ktheIdMonedheNdermSipasID(IdNdermarrja).ToString();
                MbushHiddenFieldMePerkthime(ci, rm);
                referenca_TextBox.Text = clsKokaFleteKontabel.GjeneroNrReference(IdNdermarrjeVit).ToString();
                hfNrRef.Value = referenca_TextBox.Text;
                ASPxNavBar1.Groups[0].Expanded = false;
                gridFaturat = (ASPxGridView)ASPxNavBar1.Groups[0].FindControl("grid_faturat");
                clsTeDrejtaRoli tedrejtaInfo = new clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, "Info KF");
                hfTeDrejtaInfoKF.Value = tedrejtaInfo.DAmb.ToString();
                switch (Request.QueryString["shtim_modifikim"])
                {
                    case "shtim":
                        hfShtimModifikim.Value = "shtim";
                        KonfiguroVleraFillestareShto(periudha, gridFaturat);
                        break;
                    case "anullim":
                        hfShtimModifikim.Value = "anullim";
                        KonfiguroVleraFillestareShto(periudha, gridFaturat);
                        if (Request.QueryString["idkonfigurimi"] != null)
                        {
                            var konf = new clsKonfigurimAmbjenti(int.Parse(Request.QueryString["idkonfigurimi"]));
                            konfigurimi_ComboBox.Value = konf.IdKonfigAmbjente.ToString();
                            hfKonffillestar.Value = konfigurimi_ComboBox.SelectedItem.Text;
                            veprimi_ComboBox.Value = konf.IdNivel.ToString();
                            var idanullimi = int.Parse(Request.QueryString["idanullimi"]);
                            var colKoka = new clsVeprimBankaKoka(idanullimi);
                            var colTrupi = new colVeprimBankaTrupi(idanullimi);
                            MbushHiddenFieldet(colTrupi);
                            MerrTedhenatAnullime(colKoka, konf.IdKonfigAmbjente);
                        }
                        break;
                    case "klonim":
                        hfShtimModifikim.Value = "klonim";
                        KonfiguroVleraFillestareModifiko(periudha, gridFaturat);
                        break;
                    default:
                        hfShtimModifikim.Value = "modifikim";
                        KonfiguroVleraFillestareModifiko(periudha, gridFaturat);
                        break;
                }
                if (Request.QueryString["idfatura"] != null)
                {
                    var serializues = new JavaScriptSerializer();
                    var id = (object[])serializues.DeserializeObject(Request.QueryString["idfatura"]);
                    MbushVeprimeBankaNgaFatura(id, gridFaturat);
                    hfState.Set("gjenerimAuto", true);
                }
                VendosFormatNumri(gridFaturat);
                PercaktoTemplateMenu(ASPxMenu1);
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, "LupaKlientShpejte.aspx");
                hfTeDrejtaKFRi.Value = tedrejtaInfo.DShtim.ToString();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, komponente);
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                hfTeDrejta.Add("ShtimDraft", tedrejtaInfo.DShtimDraft);
                hfTeDrejta.Add("ModifikimDraft", tedrejtaInfo.DModifikimDraft);
                var col = new colOpsionePagese((veprimi == "terheqje" || veprimi == "pagese") ? LlojVeprimi.Derdhje : LlojVeprimi.Arketim)
                {
                    new clsOpsionePagese(0, "")
                };
                hfOpsione.Value = JsonConvert.SerializeObject(col);
                VendosPeriudhenKlientSide(periudha);
            }
            else
            {
                guidString = (string)hfState["guidString"];
                idKatDokShitje = (int)hfState["idKatDokShitje"];
                idKategori = (int)hfState["idKategori"];
                veprimi = (string)hfState["veprimi"];
                gridFaturat = (ASPxGridView)ASPxNavBar1.Groups[0].FindControl("grid_faturat");
                if (IsCallback && Request.Params["__CALLBACKID"].Contains("grid_faturat") && clsAlternativaKushti.getAlternativa(Convert.ToInt32(konfigurimi_ComboBox.Value), "ALF") == "Po")
                {
                    MerrGridFaturatSession(gridFaturat);
                    KonfiguroGrideFaturat(gridFaturat);
                }
                Container55.Attributes["src"] = "";
            }
            MbushMonedhatDhekursetFunditSipasKonfigurimit(DateTime.Today);
            PercaktoTemplateMenu(ASPxMenu1);
            GridUtil.konfigGrideListeEMadhePaTheme(gridFaturat, "IdDokumenti");
            veprimi_ComboBox.ClientEnabled = false;
            AspxWebControlUtils.perkthePopUp(popFshi, MessagesResource.Messages["labelAdministrimiKujdes"], lblMsgbox, MessagesResource.Messages["labelAdministrimiMsgJeniSigurt"], ButtonCancel, MessagesResource.Messages["labelAnullo"]);
            AspxWebControlUtils.perkthePopUp(popMesazhQK, MessagesResource.Messages["labelAdministrimiKujdes"], lblMsgbox4, MessagesResource.Messages["msgDeshironiTeBeniShperndarjenNeQendratEKostos"], ButtonCancelQK, MessagesResource.Messages["btnJO"], ButtonOkQK, MessagesResource.Messages["btnPO"]);
        }

        /// <summary>
        /// Metode qe sherben per te kaluar perkthime te js
        /// </summary>
        /// <param name="cultinf">Merr culture info</param>
        /// <param name="rm">Merr Resource Manager</param>
        private void MbushHiddenFieldMePerkthime(CultureInfo cultinf, ResourceManager rm)
        {
            hfState.Set("MenuKokeDokumenti", MessagesResource.Messages["MenuKokeDokumenti"]);
            hfState.Set("MenuTrupDokumenti", MessagesResource.Messages["MenuTrupDokumenti"]);
            hfState.Set("MenuFundDokumenti", MessagesResource.Messages["MenuFundDokumenti"]);
            hfState.Set("msgNukMundTeKryheniVeprimeMeKF", MessagesResource.Messages["msgNukMundTeKryheniVeprimeMeKF"]);
            hfState.Set("msgKlientFurnitoriNukEshteAktiv", MessagesResource.Messages["msgKlientFurnitoriNukEshteAktiv"]);
            hfState.Set("msgArkaBankaNukEkziston", MessagesResource.Messages["msgArkaBankaNukEkziston"]);
            hfState.Set("msgNukKeniAutorizimPerAsnjeDokument", MessagesResource.Messages["msgNukKeniAutorizimPerAsnjeDokument"]);
            hfState.Set("msgVendosniKursinEMonedhes", MessagesResource.Messages["msgVendosniKursinEMonedhes"]);
            hfState.Set("msgDeshironiTeBeniShperndarjenNeQendraKosto", MessagesResource.Messages["msgDeshironiTeBeniShperndarjenNeQendraKosto"]);
            hfState.Set("msgDoTeBeshShperndarjenNeQKostoTeDifTeKursit", MessagesResource.Messages["msgDoTeBeshShperndarjenNeQKostoTeDifTeKursit"]);
            hfState.Set("msgZgjidhDokumentin", MessagesResource.Messages["msgZgjidhDokumentin"]);
            hfState.Set("popupAdministrimiUniversal", MessagesResource.Messages["popupAdministrimiUniversal"]);
            hfState.Set("msgZgjidhKF", MessagesResource.Messages["msgZgjidhKF"]);
            hfState.Set("msgZgjidhFurnitorin", MessagesResource.Messages["msgZgjidhFurnitorin"]);
            hfState.Set("msgZgjidhKlientin", MessagesResource.Messages["msgZgjidhKlientin"]);
            hfState.Set("msgZgjidhPunonjesin", MessagesResource.Messages["msgZgjidhPunonjesin"]); 
            hfState.Set("msgZgjidhPunonjes", MessagesResource.Messages["msgZgjidhPunonjes"]);
            hfState.Set("regjisDokZgjidhDokPerTeBashkengjitur", MessagesResource.Messages["regjisDokZgjidhDokPerTeBashkengjitur"]);
            hfState.Set("msgShperndarjeNeQendratEKostos", MessagesResource.Messages["msgShperndarjeNeQendratEKostos"]);
            hfState.Set("msgPoTransferohetTeDhenatShtypniPerseriRuaj", MessagesResource.Messages["msgPoTransferohetTeDhenatShtypniPerseriRuaj"]);
            hfState.Set("msgShenoniNumrinEDokumentit", MessagesResource.Messages["msgShenoniNumrinEDokumentit"]);
            hfState.Set("msgKursiRiNdryshonShumeMeKursinMePare", MessagesResource.Messages["msgKursiRiNdryshonShumeMeKursinMePare"]);
            hfState.Set("msgZgjidhniBanken", MessagesResource.Messages["msgZgjidhniBanken"]);
            hfState.Set("msgZgjidhniArken", MessagesResource.Messages["msgZgjidhniArken"]);
            hfState.Set("msgZgjidhniNjeDateDokumenti", MessagesResource.Messages["msgZgjidhniNjeDateDokumenti"]);
            hfState.Set("msgZgjidhniNjeDateRegjstrimi", MessagesResource.Messages["msgZgjidhniNjeDateRegjstrimi"]);
            hfState.Set("msgShenoniVleren", MessagesResource.Messages["msgShenoniVleren"]);
            hfState.Set("msgShenoniKursin", MessagesResource.Messages["msgShenoniKursin"]);
            hfState.Set("msgVeprimiNukEshteIKuadruar", MessagesResource.Messages["msgVeprimiNukEshteIKuadruar"]);
            hfState.Set("msgNukKeniAutorizimPerTeRuajturKeteDok", MessagesResource.Messages["msgNukKeniAutorizimPerTeRuajturKeteDok"]);
            hfState.Set("msgNukKeniAutorizimPerTeFshireDok", MessagesResource.Messages["msgNukKeniAutorizimPerTeFshireDok"]);
            hfState.Set("msgRreshtaTePavlefshemNeGride", MessagesResource.Messages["msgRreshtaTePavlefshemNeGride"]);
            hfState.Set("msgKursiNukMundTeJeteZero", MessagesResource.Messages["msgKursiNukMundTeJeteZero"]);
            hfState.Set("msgZgjidhPeriudheKontabel", MessagesResource.Messages["msgZgjidhPeriudheKontabel"]);
            hfState.Set("msgShtoKF", MessagesResource.Messages["msgShtoKF"]);
            hfState.Set("msgNdodhiGabimGjateMarrjesSeTargesSeAutomjetit", MessagesResource.Messages["msgNdodhiGabimGjateMarrjesSeTargesSeAutomjetit"]);
            hfState.Set("msgZgjidhFaturatTerminated", MessagesResource.Messages["msgZgjidhFaturatTerminated"]);
            hfState.Set("msgZgjidhCaFatura", MessagesResource.Messages["msgZgjidhCaFatura"]);
            hfState.Set("msgNukLejohetPageseMeEMadheSeTotaliIFaturave", MessagesResource.Messages["msgNukLejohetPageseMeEMadheSeTotaliIFaturave"]);
            hfState.Set("msgNukLejohetPageseMeEMadheSeDetyrim", MessagesResource.Messages["msgNukLejohetPageseMeEMadheSeDetyrim"]);
            hfState.Set("msgZgjidhFaturaTerminated", MessagesResource.Messages["msgZgjidhFaturaTerminated"]);
            hfState.Set("msgTrupiDokNukDuhetBosh", MessagesResource.Messages["msgTrupiDokNukDuhetBosh"]);
            hfState.Set("msgKujdesKursiKembimitNje", MessagesResource.Messages["msgKujdesKursiKembimitNje"]);
            hfState.Set("msgDokNeProcesAprovimi", MessagesResource.Messages["msgDokNeProcesAprovimi"]);
            hfState.Set("msgNukMundTeKryheniVeprimeMePunonjes", MessagesResource.Messages["msgNukMundTeKryheniVeprimeMePunonjes"]);
            hfState.Set("msgPunonjesiPrefixNjejes", MessagesResource.Messages["msgPunonjesiPrefixNjejes"]);
            hfState.Set("msgPunonjesiPrefixShumes", MessagesResource.Messages["msgPunonjesiPrefixShumes"]);
            hfState.Set("msgPunonjesPaLlogariPageseNjejes", MessagesResource.Messages["msgPunonjesPaLlogariPageseNjejes"]);
            hfState.Set("msgPunonjesPaLlogariPageseShumes", MessagesResource.Messages["msgPunonjesPaLlogariPageseShumes"]);
            hfState.Set("msgPunonjesiNukEzistonOseJoAktiv", MessagesResource.Messages["msgPunonjesiNukEzistonOseJoAktiv"]);
            GridUtil.perktheButonaGride(hfState, cultinf);
            ASPxNavBar1.Groups[0].Text = MessagesResource.Messages["msgFaturat"];
        }

        /// <summary>
        /// ruan Periudhen e marre nga Sessioni ne hiddenField-in hfPeriudhaKontabelBanka
        /// </summary>
        /// <param name="periudha"></param>
        private void VendosPeriudhenKlientSide(clsPeriudhaKontabel periudha)
        {
            if (periudha != null)
            {
                hfPeriudhaKontabelBanka.Clear();
                hfPeriudhaKontabelBanka.Set("idPeriudha", periudha.IdPeriudha);
                hfPeriudhaKontabelBanka.Set("emerPeriudha", periudha.EmerPeriudha);
                hfPeriudhaKontabelBanka.Set("fillimiPeriudha", periudha.FillimiPeriudha);
                hfPeriudhaKontabelBanka.Set("mbarimiPeriudha", periudha.MbarimiPeriudha);
            }
        }

        private void MbushMonedhatDhekursetFunditSipasKonfigurimit(DateTime data)
        {
            string[] vlereKursiMonedhe = clsKurset.merrKursinFunditTeKonfigPerMonedhatNdermarrjesSipasDates(IdNdermarrja, IdPerdoruesi, int.Parse(konfigurimi_ComboBox.Value.ToString()), data);
            var serializusi = new JavaScriptSerializer();
            hfState.Set("monedhatKurse", serializusi.Serialize(vlereKursiMonedhe));
        }

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="idPerdoruesi"></param>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        /// <param name="idGjuha"></param>
        private void PercaktoTemplateMenu(ASPxMenu aSPxMenu1)
        {
            var menu = new colMenuItem(IdGjuha);
            menu.merrMenuItemSipasKomponentesRegjistrime(IdGjuha, clsFunksione.GetKomponente(Page.Request), IdPerdoruesi, IdNdermarrja, IdViti, hfShtimModifikim.Value != "modifikim");
            var kok = new clsKokaFleteKontabel();

            foreach (var m in menu)
            {
                if (m.Name != "ItemFilter" && m.Name != "ItemFrame")
                    clsToolbarConfig.ShtoMenuItem(Theme, aSPxMenu1, m);
                if ((hfLidhur.Value == "True") && m.Name == "Draft")
                {
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                    ruaj_draft.ClientEnabled = false;
                }
                if ((hfShtimModifikim.Value != "modifikim") && m.Name == "Fshi")
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                if ((hfShtimModifikim.Value != "modifikim") && m.Name == "Klono")
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                if (m.Name == "FletaKontabel")
                {
                    if ((hfShtimModifikim.Value != "modifikim"))
                        aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                    else
                    {
                        kok = new clsKokaFleteKontabel(int.Parse(Request.QueryString["id"]), 3);
                        if (kok.NrDukumentiKokaFleteKontabel != null)
                            aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].NavigateUrl = "javascript: myFaqeCelje.kontrolloTeDrejta('Shto_FleteKontabel.aspx?shtim_modifikim=modifikim&id=" + kok.IdKokaFleteKontabel + "&numur=" + kok.NrDukumentiKokaFleteKontabel + "')";
                        else
                        {
                            kok = new clsKokaFleteKontabel(int.Parse(Request.QueryString["id"]), 4);
                            if (kok.NrDukumentiKokaFleteKontabel != null)
                                aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].NavigateUrl = "javascript: myFaqeCelje.kontrolloTeDrejta('Shto_FleteKontabel.aspx?shtim_modifikim=modifikim&id=" + kok.IdKokaFleteKontabel + "&numur=" + kok.NrDukumentiKokaFleteKontabel + "')";
                            else
                                aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                        }
                    }
                }
                if (m.Name == "QendraKosto")
                {
                    if ((hfShtimModifikim.Value != "modifikim"))
                        aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                    else
                    {
                        if (kok.NrDukumentiKokaFleteKontabel != null)
                        {
                            var qend = new clsKokaQendraKosto();
                            qend.KtheKokaQKSipasIDGjeneruesDheKonfig(kok.IdKokaFleteKontabel, kok.IdKonfigAmbjente);
                            if (qend.NrDok != null)
                                aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].NavigateUrl = "javascript: myButtonClickLupa.LupaUniversal_Click('Shperndarje ne qendrat e kostos','LupaRegjistrimQendraKosto.aspx?shtim_modifikim=modifikim&idDokGjenerues=" + kok.IdKokaFleteKontabel + "&idkonfig=" + kok.IdKonfigAmbjente + "',900,600)";
                            else
                                aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                        }
                        else aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                    }
                }
                if ((hfShtimModifikim.Value != "modifikim") && (m.Name == "PrintPreview" || m.Name == "DergoEmail"))
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                if (m.Name == "Ruaj")
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = true;
                if (m.Name == "ItemFrame")
                    clsToolbarConfig.ShtoMenuItemPerFrame(this, aSPxMenu1, clsFunksione.ktheUrlHelpi(m.UrlHelp).Item1);
                if (m.Name == "Shto" || m.Name == "Kerko" || m.Name == "Pastro" || m.Name == "ItemFilter" || m.Name == "ItemFrame" || m.Name == "Arkiva" || m.Name == "Konverto" || m.Name == "Pezullo")
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].BeginGroup = true;
            }
            clsMenuInfo.ShtoMenuItemInfo(this, MenuInfo);
            bool visible = Request.QueryString["id"] == null ? true : clsFunksione.merrMenuVisibleDraft(hfShtimModifikim.Value != "modifikim", clsVeprimBankaKoka.ktheIdStatusDokumenti(int.Parse(Request.QueryString["id"])));
            ASPxMenu1.Items.FindByName("Draft").ClientVisible = visible;
            int id = 0;
            if (hfShtimModifikim.Value == "modifikim")
                int.TryParse(Request.QueryString["id"], out id);
            visibleMenu(id);
        }
        private void visibleMenu(int id)
        {
            clsKonfigurimAmbjenti konf = new clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasKod(konfigurimi_ComboBox.Text.Split(';')[0], mySessionObjects.merrIdNdermarrjeSesioni(Session));
            clsKusht kusht = new clsKusht(konf.IdKonfigAmbjente, "ZSP");
            bool[] visible = clsFunksione.merrMenu(IdPerdoruesi, kusht.Vlera, hfShtimModifikim.Value == "modifikim" ? false : true, lblStatusAprovimi.Text, id, konfigurimi_ComboBox.Text, 3);
            try
            {
                ASPxMenu1.Items.FindByName("Ruaj").ClientVisible = visible[0];
            }
            catch (Exception ex)
            {
            }
            ASPxMenu1.Items.FindByName("RuajPrint").ClientVisible = visible[0];
            if (hfShtimModifikim.Value == "kthim")
                ASPxMenu1.Items.FindByName("Draft").ClientVisible = false;
            else
                ASPxMenu1.Items.FindByName("Draft").ClientVisible = visible[1];
            ASPxMenu1.Items.FindByName("Aprovo").ClientVisible = visible[2];
            ASPxMenu1.Items.FindByName("Refuzo").ClientVisible = visible[3];
            ASPxMenu1.Items.FindByName("Delego").ClientVisible = visible[4];
            ASPxMenu1.Items.FindByName("Komento").ClientVisible = visible[5];
            ASPxMenu1.Items.FindByName("Modifiko").ClientVisible = visible[6];
            ASPxMenu1.Items.FindByName("Shto").ClientVisible = visible[8];
            ASPxMenu1.Items.FindByName("Fshi").ClientVisible = visible[10];

            if (visible[6])
                hfTeDrejtaModSkema.Value = "True";
            else hfTeDrejtaModSkema.Value = "False";
        }

        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            PercaktoTemplateMenu(ASPxMenu1);
        }

        private void VendosDataDefault(clsPeriudhaKontabel periudha)
        {
            DateTime sot = DateTime.Today;
            if (sot >= periudha.FillimiPeriudha && sot <= periudha.MbarimiPeriudha)
                data_DateEdit.Value = DateTime.Today;
            else
                data_DateEdit.Value = periudha.FillimiPeriudha;
            data_regj_DateEdit.Value = DateTime.Today;
        }

        private void KonfiguroVleraFillestareShto(clsPeriudhaKontabel periudha, ASPxGridView gridFaturat)
        {
            btnPeriudha.Text = periudha.NrPeriudha.ToString();
            lblPeriudhaAktuale.Text = periudha.FillimiPeriudha.ToShortDateString() + "-" + periudha.MbarimiPeriudha.ToShortDateString();
            AspxWebControlUtils.vendosDateEditMask(data_DateEdit);
            AspxWebControlUtils.vendosDateEditMask(data_regj_DateEdit);
            VendosDataDefault(periudha);
            ConfigureAspxComboBox.shtoKolonaPerArkaBanka(banka_ComboBox);
            shuma_TextBox.ReadOnly = true;
            kursi_TextBox.Text = "1";
            MbushComboNivelet();
            switch (veprimi)
            {
                case "derdhje":
                case "arketim":
                    veprimi_ComboBox.SelectedIndex = 0;
                    MbushComboKonfigurimet();
                    konfigurimi_ComboBox.SelectedIndex = 0;
                    break;
                case "arketimLlogariKlienti":
                    veprimi_ComboBox.SelectedIndex = 0;
                    MbushComboKonfigurimet();
                    konfigurimi_ComboBox.SelectedIndex = 1;
                    break;
                case "arketimAbonent":
                    veprimi_ComboBox.SelectedIndex = 0;
                    MbushComboKonfigurimet();
                    konfigurimi_ComboBox.SelectedIndex = 2;
                    break;
                case "terheqje":
                case "pagese":
                    veprimi_ComboBox.SelectedIndex = 1;
                    MbushComboKonfigurimet();
                    konfigurimi_ComboBox.SelectedIndex = 0;
                    break;
            }
            ConfigureAspxComboBox.shtoKolonaPerGrupim(cmbGrup1);
            ConfigureAspxComboBox.mbushComboGrupeDokumentashSipasKonfigurimit(cmbGrup1, IdNdermarrja, 1, int.Parse(konfigurimi_ComboBox.Value.ToString()), IdPerdoruesi);
            ConfigureAspxComboBox.shtoKolonaPerGrupim(cmbGrup2);
            ConfigureAspxComboBox.mbushComboGrupeDokumentashSipasKonfigurimit(cmbGrup2, IdNdermarrja, 2, int.Parse(konfigurimi_ComboBox.Value.ToString()), IdPerdoruesi);
            ConfigureAspxComboBox.shtoKolonaPerGrupim(cmbGrup3);
            ConfigureAspxComboBox.mbushComboGrupeDokumentashSipasKonfigurimit(cmbGrup3, IdNdermarrja, 3, int.Parse(konfigurimi_ComboBox.Value.ToString()), IdPerdoruesi);
            if (konfigurimi_ComboBox.SelectedItem != null)
                hfKonffillestar.Value = konfigurimi_ComboBox.SelectedItem.Text;
            ConfigureAspxComboBox.percaktoTemplateComboJoListePaLupe(cmbDegeAdministrative, cmbFormatiPrintimit);
            ConfigureAspxComboBox.mbushComboFormatPrintimi(cmbFormatiPrintimit, idKategori, IdNdermarrja);
            ConfigureAspxComboBox.ShtoKolonaPerKf(furnitori_ComboBox);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe( banka_ComboBox, furnitori_ComboBox, btneAutomjet, btnPeriudha);
            ConfigureAspxComboBox.percaktoTemplateComboMeEnableCallback(kredite_ButtonEdit);
            ConfigureAspxComboBox.KonfiguroComboBoxMetoda(menyrePagese_ComboBox, false);
            ConfigureAspxComboBox.percaktoTemplateComboJoList(kursi_TextBox);
            ConfigureAspxComboBox.mbushComboPeriudhatAktuale(IdViti, btnPeriudha);
            ConfigureAspxComboBox.ShtoKolonaKodiDhePershkrimi(cmbDegeAdministrative, "IdDegeAdministrative");
            ConfigureAspxComboBox.KonfiguroComboBoxDegeAdministrative(IdNdermarrja, cmbDegeAdministrative, false);
            cmbDegeAdministrative.Items.RemoveAt(0);
            ConfigureAspxComboBox.mbushComboKonfigurimKase(cmbKonfigurimKase, IdNdermarrja);
            cmbKonfigurimKase.SelectedIndex = 0;
            hfTeDrejtaGjitheDokPerTuLikujduar.Set("kushtDokPerLikujdim", clsAlternativaKushti.getAlternativa(Convert.ToInt32(konfigurimi_ComboBox.Value), "SHDPL"));
            if (Request.QueryString["idfatura"] == null && clsAlternativaKushti.getAlternativa(Convert.ToInt32(konfigurimi_ComboBox.Value), "ALF") == "Po")
            {
                InicializoGridFaturat(gridFaturat);
                //KonfiguroGrideFaturat(gridFaturat);
            }
            VendosVlerenKursit();
        }

        private void VendosFormatNumri(ASPxGridView gridFaturat)
        {
            var idMonedheZgjedhur = 0;
            if (Request.QueryString["idfatura"] == null)
                idMonedheZgjedhur = clsFunksione.ktheMonedhePerFormatNumri(IdGjuha, int.Parse(konfigurimi_ComboBox.Value.ToString()), IdNdermarrja, 301, "banka_ComboBox", -1, true);
            var formatNrPerKonfig = new clsFormatiKonfig();
            formatNrPerKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(int.Parse(konfigurimi_ComboBox.Value.ToString()));
            var formatMonedhe = DbCore.DbShare.clsFormatKonfigTrup.ktheFormatNumriMonedhe(formatNrPerKonfig, idMonedheZgjedhur);
            var serializusi = new JavaScriptSerializer();
            hfState.Set("formatMonedhe", serializusi.Serialize(formatMonedhe));
        }

        protected void btneLlogInv_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (!IsCallback) return;
            if (Request.Params["__CALLBACKID"].Contains("kredite_ButtonEdit"))
            {
                ConfigureAspxComboBox.mbushComboLlogariaPaKolona(IdPerdoruesi, IdNdermarrja, kredite_ButtonEdit, e);
            }
        }

        private void MbushVeprimeBankaNgaFatura(object[] ids, ASPxGridView gridFaturat)
        {
            bool LLMKF = clsAlternativaKushti.getAlternativa(int.Parse(konfigurimi_ComboBox.Value.ToString()), "LLMKF").ToString().ToLower() == "po";
            int IdMonedheNdermarrje = clsNdermarrje.ktheIdMonedheNdermSipasID(IdNdermarrja);
            double shuma = 0;
            var colTrupi = new colVeprimBankaTrupi();
            var formatMonedhe = new clsFormatKonfigTrup();
            var idmonedhabanka = 0;
            foreach (int id in ids)
            {
                var shitje = new clsKokaShitje();
                shitje.mbushKokaShitjeSipasIDPaTrup(id);
                if (banka_ComboBox.Text == "")
                    if (shitje.IdKlientFurnitor != 0)
                    {
                        var idBankeKf = (shitje.IdArka > 0 ? shitje.IdArka : clsKlientFurnitor.MerrIdBanke(shitje.IdKlientFurnitor));
                        if (idBankeKf == 0)
                        {
                            string bankab = clsAtributeTrupi.merrVleredefaultSipasKontrollitKodKonfigDheNderm(konfigurimi_ComboBox.Text, IdNdermarrja, "banka_ComboBox");
                            int.TryParse(bankab, out idBankeKf);
                        }
                        if (idBankeKf != 0)
                        {
                            clsBanka banka = new clsBanka();
                            banka.mbushBanke(idBankeKf);
                            ConfigureAspxComboBox.mbushComboArkaBankaById(banka_ComboBox, idBankeKf);
                            _formatNrKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(int.Parse(konfigurimi_ComboBox.Value.ToString()));
                            DbCore.DbShare.clsFormatKonfigTrup.ktheFormatNumriMonedhe(_formatNrKonfig, banka.IdMonedhaBanka);
                            monedha_Label.Text = clsMonedha.ktheKodMonedheSipasId(banka.IdMonedhaBanka); //monedha.KodiMonedha;
                            gjendja_Label.Text = clsBanka.ktheGjendjeBanke(idBankeKf, banka.LlojArkaBanka, IdPerdoruesi, IdNdermarrja, DateTime.Today).ToString("{0,0.00}");
                            int llojkursi = 1;
                            int.TryParse(clsAtributeTrupi.merrVleredefaultSipasKontrollitKodKonfigDheNderm(konfigurimi_ComboBox.Text, IdNdermarrja, "kursi_TextBox"), out llojkursi);
                            var kurs = new clsKurset(banka.IdMonedhaBanka, DateTime.Today, llojkursi);
                            if (kurs.VleraKursi == 0)
                                kurs.VleraKursi = 1;
                            kursi_TextBox.Text = kurs.VleraKursi.ToString();
                            hfKursiEkzistues.Value = kurs.VleraKursi.ToString();
                            idmonedhabanka = banka.IdMonedhaBanka;
                        }
                    }
                
                if (monedha_Label.Text == "Monedha")
                {
                    //clsNdermarrje nder = new clsNdermarrje(idNdermarrje);
                    idmonedhabanka = clsNdermarrje.ktheIdMonedheNdermSipasID(IdNdermarrja);
                }
                formatMonedhe = _formatNrKonfig.IdFormatKonfig > 0 ? _formatNrKonfig.KonfigTrupi.merrFormatSipasMonedhes(idmonedhabanka) : new clsFormatKonfigTrup(clsFormatKonfigTrup.defaultFormatSasia, clsFormatKonfigTrup.defaultFormatCmimi, clsFormatKonfigTrup.defaultFormatVlefta, clsFormatKonfigTrup.defaultFormatZbritja, clsFormatKonfigTrup.defaultFormatStringSasia, clsFormatKonfigTrup.defaultFormatStringCmimi, clsFormatKonfigTrup.defaultFormatStringVlefta, clsFormatKonfigTrup.defaultFormatStringZbritja);
                txtShoqeria.Text = shitje.EmerKlienti;
                txtCustomerNr.Text = "";
                nrDokumenti_TextBox.Text = shitje.NrDok;
                referenca_TextBox.Text = clsKokaFleteKontabel.GjeneroNrReference(IdNdermarrjeVit).ToString();
                pershkrimi_Memo.Text = shitje.Pershkrimi;
                data_DateEdit.Date = DateTime.Today;
                data_regj_DateEdit.Date = DateTime.Today;
                menyrePagese_ComboBox.Text = "Pagese";
                if (shitje.IdDegeAdministrative > 0)
                    cmbDegeAdministrative.Value = shitje.IdDegeAdministrative.ToString();
                var kursmonfat = new clsKurset(shitje.IdMonedha, DateTime.Today);
                kursmonfat.VleraKursi = (LLMKF && idmonedhabanka == IdMonedheNdermarrje) ? shitje.Kursi : kursmonfat.VleraKursi;
                if (kursmonfat.VleraKursi == 0)
                {
                    kursmonfat.VleraKursi = 1;
                }
                var shumaPaKurs = shitje.VleraMbetur;
                var isVleraTotalPozitive = shumaPaKurs > 0;
                shumaPaKurs = isVleraTotalPozitive ? shumaPaKurs : -shumaPaKurs;
                if (idmonedhabanka == shitje.IdMonedha)
                    shuma = shumaPaKurs;
                else
                    shuma = shumaPaKurs * kursmonfat.VleraKursi / double.Parse(kursi_TextBox.Text);

                var tr = new clsVeprimBankaTrupi
                {
                    IdFatura = shitje.IdShitjeKoka,
                    IdNivel = shitje.IdNivel,
                    IdOpsionePagese = 1,
                    OpsionePagese = "Pagese fature",
                    Lloji = clsKlientFurnitor.MerrLlojin(shitje.IdKlientFurnitor) ? "Klient" : "Furnitor",
                    IdSubjekti = shitje.IdKlientFurnitor,
                    MeKursFature = false
                };
                ///pagese fature
                switch (veprimi)
                {
                    case "terheqje":
                    case "pagese":
                        tr.DebiKredi = "Debi";
                        break;
                    case "derdhje":
                    case "arketim":
                        tr.DebiKredi = "Kredi";
                        break;
                    default:
                        tr.DebiKredi = "";
                        break;
                }
                if (idmonedhabanka == shitje.IdMonedha)
                {
                    tr.VleraPaguar = shumaPaKurs;
                    tr.VleraPaArketueshme = shumaPaKurs;
                    tr.VleraPaguarMonedhaBaze = shumaPaKurs * double.Parse(kursi_TextBox.Text);
                }
                else
                {
                    tr.VleraPaguar = shumaPaKurs * kursmonfat.VleraKursi / double.Parse(kursi_TextBox.Text);
                    tr.VleraPaArketueshme = shumaPaKurs * kursmonfat.VleraKursi / double.Parse(kursi_TextBox.Text);
                    tr.VleraPaguarMonedhaBaze = shumaPaKurs * kursmonfat.VleraKursi;
                    if (LLMKF && IdMonedheNdermarrje == idmonedhabanka)
                        tr.MeKursFature = true;
                }

                tr.KMK = kursmonfat.VleraKursi;// shitje.Kursi;
                colTrupi.Add(tr);
            }
            MbushHiddenFieldet(colTrupi);
            vlera_TextBox.Value = shuma.ToString();
            var shumaFjale = ChangeToWords(Math.Round(shuma, formatMonedhe.ShifraPasPresjesVlefta).ToString());
            shuma_TextBox.Text = shumaFjale;
            vleraMonedhaBaze_TextBox.Value = (shuma * double.Parse(kursi_TextBox.Text)).ToString();
            hfKthehu.Value = "kthehu";
            if (clsAlternativaKushti.getAlternativa(Convert.ToInt32(konfigurimi_ComboBox.Value), "ALF") == "Po")
            {
                InicializoGridFaturat(gridFaturat);
                //KonfiguroGrideFaturat( gridFaturat);
            }
        }

        /// <summary>
        /// Mbush me te dhena combon e niveleve
        /// </summary>
        /// <param name="veprimi"></param>
        /// <param name="idKategori"></param>
        private void MbushComboNivelet()
        {
            var nivelet = new colNivelRegjistrimi();
            veprimi_ComboBox.DataSource = colNivelRegjistrimi.ktheGjitheNivelRegjistrimiSipasKategoriDtCombo(idKategori, IdNdermarrja, IdPerdoruesi, false);
            veprimi_ComboBox.TextField = "Pershkrimi";
            veprimi_ComboBox.ValueField = "IdNivel";
            veprimi_ComboBox.DataBind();
            veprimi_ComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            veprimi_ComboBox.SelectedIndex = 0;
        }

        private void MbushComboKonfigurimet()
        {
            var colKonfig = new colKonfigurimAmbjenti();
            var konf = new clsKonfigurimAmbjenti();
            if (veprimi.EqualsAnyIgnoreCase("arketim", "pagese", "arketimAbonent", "arketimLlogariKlienti"))
                konf.IdKategori = 3;   //arka
            else
                konf.IdKategori = 4;  //banka
            konf.IdNdermarje = IdNdermarrja;
            if (veprimi_ComboBox.Value != null)
            {
                konf.IdNivel = int.Parse(veprimi_ComboBox.Value.ToString());
                colKonfig.mbushKonfigAmbjSipasIdKategoriIdNivelMeLloj(konf.IdKategori, konf.IdNivel, IdPerdoruesi);
            }
            else
                colKonfig.mbushKonfigAmbjSipasIdKategori(konf.IdKategori, konf.IdNdermarje, IdPerdoruesi, IdGjuha);
            //colKonfig = share.merrKonfigAmbjSipasIdKategori(konf, DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            konfigurimi_ComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;

            var colprove = new ListBoxColumn
            {
                FieldName = "KodKonfigAmbjente",
                Caption = MessagesResource.Messages["cmbCmimeArtikulliCaptionKodi"]
            };
            var colemer = new ListBoxColumn
            {
                FieldName = "PershkrimKonfigAmbjente",
                Caption = MessagesResource.Messages["cmbCmimeArtikulliCaptionPershkrimi"]
            };
            konfigurimi_ComboBox.TextFormatString = "{0}"; //;{1}";
            konfigurimi_ComboBox.Columns.Add(colprove);
            konfigurimi_ComboBox.Columns.Add(colemer);
            konfigurimi_ComboBox.DataSource = colKonfig;
            konfigurimi_ComboBox.ValueField = "IdKonfigAmbjente";
            konfigurimi_ComboBox.DataBind();
            konfigurimi_ComboBox.SelectedIndex = 0;
            hfKonffillestar.Value = konfigurimi_ComboBox.SelectedItem.Text;
        }

        /// <summary>
        /// Vendos vlerat default kur po behet modifikim
        /// </summary>
        /// <param name="veprimi"></param>
        /// <param name="idKatDokShitje"></param>
        /// <param name="idKategori"></param>
        /// <param name="periudha"></param>
        /// <param name="komponente"></param>
        /// <param name="gridFaturat"></param>
        private void KonfiguroVleraFillestareModifiko(clsPeriudhaKontabel periudha, ASPxGridView gridFaturat)
        {
            var id = int.Parse(Request.QueryString["id"]);
            var colKoka = new clsVeprimBankaKoka(id);

            AspxWebControlUtils.vendosDateEditMask(data_DateEdit);
            AspxWebControlUtils.vendosDateEditMask(data_regj_DateEdit);
            VendosDataDefault(periudha);
            shuma_TextBox.ReadOnly = true;
            MbushComboNivelet();
            if (idKategori == 4) //veprimi == "derdhje" || veprimi == "terheqje")            
                ConfigureAspxComboBox.mbushComboBankat(IdPerdoruesi, IdNdermarrja, banka_ComboBox, false);
            else
                ConfigureAspxComboBox.mbushComboArkat(IdNdermarrja, IdPerdoruesi, banka_ComboBox, false);
            ConfigureAspxComboBox.percaktoTemplateComboJoListePaLupe(cmbDegeAdministrative, cmbFormatiPrintimit);
            ConfigureAspxComboBox.mbushComboFormatPrintimi(cmbFormatiPrintimit, idKategori, IdNdermarrja);
            ConfigureAspxComboBox.ShtoKolonaPerKf(furnitori_ComboBox);
            //clsFunksione.mbushComboKlientFurnitori(IdPerdoruesi, idNdermarrje, furnitori_ComboBox, 0);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(banka_ComboBox);
            ConfigureAspxComboBox.percaktoTemplateComboJoList(kursi_TextBox);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(furnitori_ComboBox);
            ConfigureAspxComboBox.percaktoTemplateComboMeEnableCallback(kredite_ButtonEdit);
            ConfigureAspxComboBox.KonfiguroComboBoxMetoda(menyrePagese_ComboBox, false);
            ConfigureAspxComboBox.mbushComboPeriudhatAktuale(IdViti, btnPeriudha);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(btnPeriudha);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(btneAutomjet);
            ConfigureAspxComboBox.ShtoKolonaKodiDhePershkrimi(cmbDegeAdministrative, "IdDegeAdministrative");
            ConfigureAspxComboBox.KonfiguroComboBoxDegeAdministrative(IdNdermarrja, cmbDegeAdministrative, false);
            cmbDegeAdministrative.Items.RemoveAt(0);
            ConfigureAspxComboBox.shtoKolonaPerArkaBanka(banka_ComboBox);
            ConfigureAspxComboBox.mbushComboKonfigurimKase(cmbKonfigurimKase, IdNdermarrja);
            cmbKonfigurimKase.SelectedIndex = 0;
            VendosVlerenKursit();
            MerrTedhenat(colKoka);
            hfTeDrejtaGjitheDokPerTuLikujduar.Set("kushtDokPerLikujdim", clsAlternativaKushti.getAlternativa(Convert.ToInt32(konfigurimi_ComboBox.Value), "SHDPL"));
            bool mbushfatura = clsAlternativaKushti.getAlternativa(Convert.ToInt32(konfigurimi_ComboBox.Value), "ALF") == "Po";
            MbushListeVeprimeBankaTrupiModifiko(gridFaturat,mbushfatura);
            if (mbushfatura)
            {
                InicializoGridFaturat(gridFaturat);
                //KonfiguroGrideFaturat(gridFaturat);
            }
        }

        /// <summary>
        /// Mbush griden me te dhena kur po bejme modifikim te veprimit te bankes
        /// </summary>
        /// <param name="veprimi"></param>
        /// <param name="idKatDokShitje"></param>
        /// <param name="komponente"></param>
        /// <param name="gridFaturat"></param>
        /// <param name="mbushfatura"></param>
        private void MbushListeVeprimeBankaTrupiModifiko(ASPxGridView gridFaturat, bool mbushfatura)
        {
            var colTrupi = new colVeprimBankaTrupi(int.Parse(Request.QueryString["id"]));
            var klientFurn = new clsKlientFurnitor();
            var idKlientFurnitor = -1;
            var idkliente = colTrupi.Where(t => t.Lloji.EqualsAnyIgnoreCase("Klient", "Furnitor")).Select(x => x.IdSubjekti).Distinct();
            if (idkliente.Count() == 1)
                idKlientFurnitor = idkliente.First();
            var koka = new clsVeprimBankaKoka(int.Parse(Request.QueryString["id"]));
            var id = int.Parse(Request.QueryString["id"]);
            var cls = new colDokumentLidhesKoka();
            var idKategoria = clsNivelRegjistrimi.ktheIdKategoriaNivelRegjistrimi(koka.IdNivel);
            if (new colDokumentLidhesKoka(id, idKategoria).Count > 0)
                cls = new colDokumentLidhesKoka(id, idKategoria);

            if (idKlientFurnitor != -1)
            {
                klientFurn.MbushKlientFurnitorSipasId(idKlientFurnitor);
                ConfigureAspxComboBox.KonfiguroComboBoxKlientFurnitoriById(furnitori_ComboBox, idKlientFurnitor);
            }

            var teDrejtaShikoGjitheDokPerTuLikujduar = Convert.ToString(hfTeDrejtaGjitheDokPerTuLikujduar.Get("kushtDokPerLikujdim")); 
            MbushHiddenFieldet(colTrupi);            
        }

        private void MbushHiddenFieldet(colVeprimBankaTrupi col)
        {
            var serializusi = new JavaScriptSerializer();
            serializusi.MaxJsonLength = 50000000;
            HfColTrupBanka.Value = serializusi.Serialize(col);
            HfColKF.Value = serializusi.Serialize(col.ktheColKF());
            HfColLlog.Value = serializusi.Serialize(col.ktheColLLogari());
            HfColFatShitje.Value = serializusi.Serialize(col.ktheColShitje(IdNdermarrja)); //TOCHECK Nestila -- i hoqa trupin shitjeve
            HfColFatVeprime.Value = serializusi.Serialize(col.ktheColVeprimekf(IdNdermarrja));
            hfId.Value = serializusi.Serialize(col.ktheIdFature());
            hfNivele.Value = serializusi.Serialize(col.ktheNivele());
            hfKMK.Value = serializusi.Serialize(col.ktheKMK());
            hfColPunonjes.Value= serializusi.Serialize(col.ktheColPunonjes());

        }

        /// <summary>
        /// Merren te dhenat kokes se dokumetit te bankes qe po modifikohet dhe vendosen vlerat neper kontrolle
        /// </summary>
        /// <param name="idPerdoruesi"></param>
        /// <param name="idNderViti"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="koka">Objekt i tipit clsVeprimBankaKoka nga ku merren te dhenat</param>
        public void MerrTedhenat(clsVeprimBankaKoka koka)
        {
            veprimi_ComboBox.Value = koka.IdNivel.ToString();
            MbushComboKonfigurimet();
            ConfigureAspxComboBox.shtoKolonaPerGrupim(cmbGrup1);
            ConfigureAspxComboBox.mbushComboGrupeDokumentashSipasKonfigurimit(cmbGrup1, IdNdermarrja, 1, koka.IdKonfigAmbjente, IdPerdoruesi);
            ConfigureAspxComboBox.shtoKolonaPerGrupim(cmbGrup2);
            ConfigureAspxComboBox.mbushComboGrupeDokumentashSipasKonfigurimit(cmbGrup2, IdNdermarrja, 2, koka.IdKonfigAmbjente, IdPerdoruesi);
            ConfigureAspxComboBox.shtoKolonaPerGrupim(cmbGrup3);
            ConfigureAspxComboBox.mbushComboGrupeDokumentashSipasKonfigurimit(cmbGrup3, IdNdermarrja, 3, koka.IdKonfigAmbjente, IdPerdoruesi);

            var banka = new clsBanka();
            banka.mbushBanke(koka.IdBanka);
            ConfigureAspxComboBox.mbushComboArkaBankaById(banka_ComboBox, koka.IdBanka);
            _formatNrKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(int.Parse(konfigurimi_ComboBox.Value.ToString()));
            DbCore.DbShare.clsFormatKonfigTrup.ktheFormatNumriMonedhe(_formatNrKonfig, banka.IdMonedhaBanka);
            monedha_Label.Text = clsMonedha.ktheKodMonedheSipasId(banka.IdMonedhaBanka); //monedha.KodiMonedha;
            gjendja_Label.Text = clsBanka.ktheGjendjeBanke(koka.IdBanka, banka.LlojArkaBanka, IdPerdoruesi, IdNdermarrja, koka.DateDokumenti).ToString("{0,0.00}"); // + hfFormatNumri["FormatZgjedhurVlefta"].ToString());
            kursi_TextBox.Text = koka.Kursi.ToString();
            nrDokumenti_TextBox.Text = koka.NrDokumenti;
            referenca_TextBox.Text = koka.NrReference.ToString();
            pershkrimi_Memo.Text = koka.PershkrimiKoka;
            nrSerial_TextBox.Text = koka.NrSerial;
            txtShoqeria.Text = koka.Shoqeria;
            txtCustomerNr.Text = koka.CustomerNumber;
            data_DateEdit.Date = koka.DateDokumenti;
            if (hfShtimModifikim.Value == "klonim")
                data_regj_DateEdit.Date = DateTime.Today.Date;
            else
                data_regj_DateEdit.Date = koka.DateRegjistrimi;
            menyrePagese_ComboBox.Text = "Mirebesim";
            if (koka.IdDegeAdministrative != 0)
                cmbDegeAdministrative.Value = koka.IdDegeAdministrative.ToString();
            vlera_TextBox.Value = koka.Vlera.ToString();
            shuma_TextBox.Text = ChangeToWords(vlera_TextBox.Value.ToString());
            vleraMonedhaBaze_TextBox.Value = koka.VleraMonedhaBaze.ToString();
            komision_TextBox.Value = koka.KomisioniBankar.ToString();
            konfigurimi_ComboBox.Value = koka.IdKonfigAmbjente.ToString();
            hfKursiEkzistues.Value = koka.Kursi.ToString();
            if (koka.IdGrup1 > 0)
                cmbGrup1.Value = koka.IdGrup1.ToString();
            if (koka.IdGrup2 > 0)
                cmbGrup2.Value = koka.IdGrup2.ToString();
            if (koka.IdGrup3 > 0)
                cmbGrup3.Value = koka.IdGrup3.ToString();
            kredite_ButtonEdit.Text = new clsLlogari(koka.IdLlogKredite).NrLlogari;
            cmbFormatiPrintimit.Value = koka.IdRaportDesing.ToString();
            txtFinancieri.Text = koka.Financieri;
            txtDhenesiMarresi.Text = koka.DhenesiMarresi;
            txtArketari.Text = koka.Arketari;
            txtNrLlogari.Text = koka.NrLlogari;
            txtArsye.Text = koka.ArsyeAnullimi;
            cbPrinto.Checked = koka.Printo;
            if (koka.StatusAprovimi != 0)
                lblStatusAprovimi.Text = koka.StatusAprovimi.ToString().Replace('_', ' ');
            if (koka.IdAutomjet != 0)
            {
                ConfigureAspxComboBox.mbushComboAutomjetiByID(btneAutomjet, koka.IdAutomjet);
                txtTarga.Text = (clsAutomjete.ktheTargeAutomjetSipasId(koka.IdAutomjet));
            }
            var konf = new clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasId(koka.IdKonfigAmbjente, IdGjuha);
            hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;
            if (hfShtimModifikim.Value == "modifikim")
            {
                var dbAdmin = new clsDatabaseAdmin();
                var dtlidhur = dbAdmin.MerrDokLidhur(koka.IdKoka, koka.IdNivel, "T_VEPRIMBANKAKOKA", "IDKOKA");
                hfLidhur.Value = (dtlidhur.Rows.Count != 0).ToString();
                var autorizimet = clsVeprimBankaKoka.kaAutorizime(koka.IdKoka, IdPerdoruesi);
                hfAutorizimi.Value = autorizimet.ToString();
                if (!autorizimet)
                    hfLidhur.Value = "True";
                AspxWebControlUtils.ShtoLidhje(IdPerdoruesi, IdViti, IdNdermarrja, hl, dtlidhur, koka.IdGjenerues, koka.IdNivelGjenerues, koka.IdKonfigGjenerues, IdGjuha);
                dbAdmin.Dispose();
            }
            var niv = new clsNivelRegjistrimi();
            var idKategoria = clsNivelRegjistrimi.ktheIdKategoriaNivelRegjistrimi(koka.IdNivel);

            MbushMonedhatDhekursetFunditSipasKonfigurimit(koka.DateDokumenti);
        }
        public void MerrTedhenatAnullime(clsVeprimBankaKoka koka, int idkonfigurim)
        {
            ConfigureAspxComboBox.shtoKolonaPerGrupim(cmbGrup1);
            ConfigureAspxComboBox.mbushComboGrupeDokumentashSipasKonfigurimit(cmbGrup1, IdNdermarrja, 1, idkonfigurim, IdPerdoruesi);
            ConfigureAspxComboBox.shtoKolonaPerGrupim(cmbGrup2);
            ConfigureAspxComboBox.mbushComboGrupeDokumentashSipasKonfigurimit(cmbGrup2, IdNdermarrja, 2, idkonfigurim, IdPerdoruesi);
            ConfigureAspxComboBox.shtoKolonaPerGrupim(cmbGrup3);
            ConfigureAspxComboBox.mbushComboGrupeDokumentashSipasKonfigurimit(cmbGrup3, IdNdermarrja, 3, idkonfigurim, IdPerdoruesi);
            if (idKategori == 4) //veprimi == "derdhje" || veprimi == "terheqje")            
                ConfigureAspxComboBox.mbushComboBankat(IdPerdoruesi, IdNdermarrja, banka_ComboBox, false);
            else
                ConfigureAspxComboBox.mbushComboArkat(IdNdermarrja, IdPerdoruesi, banka_ComboBox, false);
            ConfigureAspxComboBox.shtoKolonaPerArkaBanka(banka_ComboBox);
            if (koka.StatusAprovimi != 0)
                lblStatusAprovimi.Text = koka.StatusAprovimi.ToString().Replace('_', ' ');

            var banka = new clsBanka();
            banka.mbushBanke(koka.IdBanka);
            banka_ComboBox.Value = koka.IdBanka;
            ConfigureAspxComboBox.mbushComboArkaBankaById(banka_ComboBox, koka.IdBanka);
            var monedha = new clsMonedha(banka.IdMonedhaBanka);
            monedha_Label.Text = monedha.KodiMonedha;
            gjendja_Label.Text = clsBanka.ktheGjendjeBanke(koka.IdBanka, banka.LlojArkaBanka, IdPerdoruesi, IdNdermarrja, koka.DateDokumenti).ToString();
            kursi_TextBox.Text = koka.Kursi.ToString();
            pershkrimi_Memo.Text = koka.PershkrimiKoka;
            txtNrLlogari.Text = koka.NrLlogari;
            txtShoqeria.Text = koka.Shoqeria;
            txtCustomerNr.Text = koka.CustomerNumber;
            menyrePagese_ComboBox.Text = "Mirebesim";
            cmbDegeAdministrative.Value = koka.IdDegeAdministrative.ToString();
            vlera_TextBox.Value = koka.Vlera.ToString();
            txtTotaliPaguar.Value = koka.Vlera.ToString();
            var shumaFjale = ChangeToWords(vlera_TextBox.Value.ToString());
            shuma_TextBox.Text = shumaFjale;
            vleraMonedhaBaze_TextBox.Value = koka.VleraMonedhaBaze.ToString();
            if (koka.KomisioniBankar != 0)
                komision_TextBox.Value = koka.KomisioniBankar.ToString();

            hfKursiEkzistues.Value = koka.Kursi.ToString();
            if (koka.IdGrup1 > 0)
                cmbGrup1.Value = koka.IdGrup1.ToString();
            if (koka.IdGrup2 > 0)
                cmbGrup2.Value = koka.IdGrup2.ToString();
            if (koka.IdGrup3 > 0)
                cmbGrup3.Value = koka.IdGrup3.ToString();
            var llog = new clsLlogari(koka.IdLlogKredite);
            kredite_ButtonEdit.Text = llog.NrLlogari;
            hfLidhur.Value = "True";

        }

        /// <summary>
        /// Ruan veprimin e bankes
        /// Gjeneron kontabilitetin
        /// Gjeneron dokumentin lidhes kur po lidhim nje fature me nje veprim banke
        /// </summary>
        /// <param name="statusi">Statusi i ruajtjes se dokumentit (i rregullt ose draft)</param>
        /// <param name="idNdermarje"></param>
        /// <param name="idPerdoruesi"></param>
        /// <param name="idNdermarrjeVit"></param>
        /// <param name="ci"></param>
        /// <param name="veprimi"></param>
        /// <param name="idKatDokShitje"></param>
        /// <param name="rm"></param>
        /// <param name="idKategori"></param>
        /// <param name="idViti"></param>
        /// <param name="idGjuha"></param>
        /// <param name="komponente"></param>
        /// <param name="gridFaturat"></param>
        protected void RuajVeprimBanke(int statusi, bool printo, ASPxGridView gridFaturat, StatusAprovimi statusAprovimi)
        {
           
            string hfShtimModifikimValue = hfShtimModifikim.Value;
            string shfaqmesazhapolupe = "jo";
            string shfaqmesazhapolupeVdk = "jo";
            bool perBRM = (veprimi == "arketimLlogariKlienti" || veprimi == "arketimAbonent");
            if (clsAlternativaKushti.getAlternativa(Convert.ToInt32(konfigurimi_ComboBox.Value), "ALF") == "Po")
                MerrGridFaturatSession(gridFaturat);

            #region VEPRIM BANKE KOKA

            Dictionary<string,string> cotrols = this.GetAsPxTextEditIdValue();
            cotrols.AddRange(ASPxSplitter1.GetAsPxTextEditIdValue());
            
            hfNrAuto = (ASPxHiddenField)NrAuto.VendosVleratNrAuto(hfNrAuto, cotrols);
            hfNrAutoBanka = (ASPxHiddenField)NrAuto.ShtoNeRegjistrime(hfNrAutoBanka, hfNrAuto, "nrDokumenti_TextBox", "NrDokumenti");
            hfNrAutoBanka = (ASPxHiddenField)NrAuto.ShtoNeRegjistrime(hfNrAutoBanka, hfNrAuto, "nrSerial_TextBox", "NrSerial");

            clsVeprimBankaKoka koka = new clsVeprimBankaKoka();
            int id = 0;
            int idetapa = 0;
            int idNgaQueryString;
            int.TryParse(Request.QueryString["id"], out idNgaQueryString);
            string serverUrl = clsFunksione.ktheServerUrl(Request);

            if (hfShtimModifikimValue == "modifikim")
            {
                if (Request.QueryString["vjenNga"] != null) // etapa e hapur
                    idetapa = int.Parse(Request.QueryString["idetapa"]);
                else
                {//etapa e fundit kur hapet nga shitja
                    clsEtapeAprovimi etapafund = new clsEtapeAprovimi();
                    etapafund.ktheEtapeFunditSipasKokaVeprimeBankaDhePerdorues(idNgaQueryString, IdPerdoruesi);
                    idetapa = etapafund.IdEtapa;
                }
            }
            try
            {
                koka = krijoDokumentArkaBanka(serverUrl, idetapa, statusi, statusAprovimi, out shfaqmesazhapolupe, out shfaqmesazhapolupeVdk, id, perBRM);
            }
            catch (Exception ex)
            {
                LogManager.GetCurrentClassLogger().Error(ex.Message);
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, ex.Message, pnlMesazhi);
                status1.Value = "false";
                return;
            }

            int idskema = int.Parse(hfSkema.Value);
            if (hfShtimModifikimValue != "modifikim" && statusAprovimi == StatusAprovimi.Undefined)
                idskema = 0;

            #endregion

            clsMesazh mesazh;
            clsTeDrejtaRoli tedrejtaInfo = new clsTeDrejtaRoli();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, clsFunksione.GetKomponente(Page.Request));
            if (clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizimV6() && (koka.LlojiVeprimit == "Arketim" || koka.LlojiVeprimit == "Pagese") && Dergo.Checked)
            {
                DateTime ditaSot = data_DateEdit.Date;
                var gjendjeArkeDitore = clsGjendjeArkeDitore.merrGjendjeDitoreSipasIdArkeDheDitesSot(koka.IdBanka, ditaSot);
                if (!gjendjeArkeDitore)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Regjistroni me pare balancen ditore te arkes!", pnlMesazhi);
                    return;
                }
            }
            switch (hfShtimModifikimValue)
            {
                case "shtim":
                case "anullim":
                case "klonim":
                    if ((statusi == 1 && !tedrejtaInfo.DShtim) || (statusi == 0 && !tedrejtaInfo.DShtimDraft))
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                        status1.Value = "false";
                        return;
                    }
                    mesazh = koka.ruaj(hfNrAutoBanka, false, "", "", "", "", idskema, statusAprovimi, idetapa, serverUrl, perBRM);
                    break;
                case "modifikim":
                    if ((statusi == 1 && !tedrejtaInfo.DMod) || (statusi == 0 && !tedrejtaInfo.DModifikimDraft))
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                        status1.Value = "false";
                        return;
                    }
                    koka.IdKoka = int.Parse(Request.QueryString["id"]);
                    bool lidhur = koka.eshteDokumentiILidhur();
                    if (lidhur.ToString() != hfLidhur.Value)
                    {
                        status1.Value = "false";
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Dokumenti është i lidhur", pnlMesazhi);
                        return;
                    }
                    clsVeprimBankaKoka kokaekzistuese = new clsVeprimBankaKoka(koka.IdKoka);
                    if (statusAprovimi == StatusAprovimi.Undefined)
                        koka.StatusAprovimi = kokaekzistuese.StatusAprovimi;
                    koka.IdDokAnullimi = kokaekzistuese.IdDokAnullimi;
                    if (lblStatusAprovimi.Text != string.Empty && lblStatusAprovimi.Text != StatusAprovimi.Aprovuar.ToString())
                        lidhur = true;
                    mesazh = koka.modifiko(lidhur, rm, ci, idskema, statusAprovimi, idetapa, serverUrl);
                    break;
                default:
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ndodhi një gabim gjatë ruajtjes së dokumentit", pnlMesazhi);
                    status1.Value = "false";
                    return;
            }

            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                status1.Value = "false";
                return;
            }
            if(mesazh.Status && Dergo.Checked && (hfShtimModifikimValue== "shtim" || hfShtimModifikimValue == "klonim"))
            {
                string veprimi = "";
                clsBanka arkaBanka = new clsBanka(koka.IdBanka);
                if (koka.LlojiVeprimit == "Arketim")
                    veprimi = "DEPOSIT";
                else if (koka.LlojiVeprimit == "Pagese")
                    veprimi = "WITHDRAW";
                if (veprimi != "")
                {
                    string xml = clsFunksioneFiskalizimi.gjeneroVeprimeMeArken(new clsNdermarrje(IdNdermarrja), koka.Vlera.ToString(), koka.DateDokumenti.ToString(), arkaBanka.KodiBanka, arkaBanka.KodiTCR, veprimi);
                    string[] result = clsFunksioneFiskalizimi.InvokeService(xml, "", false);
                    if (result[0] == null && veprimi == "DEPOSIT")
                        clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "Arka u depozitua me sukses ne self-care", pnlMesazhi);
                    if (result[0] == null && veprimi == "WITHDRAW")
                        clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "Arka u terhoqe me sukses ne self-care", pnlMesazhi);
                }

            }
            hfqkmesazhi.Value = shfaqmesazhapolupe;
            hfqkmesazhiVDK.Value = shfaqmesazhapolupeVdk;
            if (shfaqmesazhapolupe != "jo")
            {
                clsKokaFleteKontabel kok = new clsKokaFleteKontabel(koka.IdKoka, idKategori);
                hfUrl.Value = "LupaRegjistrimQendraKosto.aspx?shtim_modifikim=modifikim&idDokGjenerues=" + kok.IdKokaFleteKontabel + "&idkonfig=" + kok.IdKonfigAmbjente;
            }
            if (shfaqmesazhapolupeVdk != "jo")
            {
                foreach (var d in koka.ODokumentLidhes)
                {
                    clsKokaFleteKontabel kok = new clsKokaFleteKontabel(d.IdKoka, 10);
                    hfUrlVDK.Value += "LupaRegjistrimQendraKosto.aspx?shtim_modifikim=modifikim&idDokGjenerues=" + kok.IdKokaFleteKontabel + "&idkonfig=" + kok.IdKonfigAmbjente + ";";
                }
            }
            if (hfShtimModifikimValue == "anullim")
                Response.Redirect("VeprimeBanka.aspx?lloji=" + Request.QueryString["lloji"] + "&ruaj=po");
            if (printo || cbPrinto.Checked)
                Container55.Attributes["src"] = "RaportiShpejte.aspx?Sesioni=false&emriReal=mandatArketimPagese&idDokumenti=" + koka.IdKoka + "&printo=true&raportdyte=jo&iddesign=" + cmbFormatiPrintimit.Value;

            if (cbKasa.Checked && koka.IdStatusDokumenti == 1)
            {
                clsPerdorues per = new clsPerdorues(IdPerdoruesi);
                clsMesazh mesazhkasa = koka.PrintoArketimeNeKase(IdPerdoruesi, IdNdermarrja, mySessionObjects.merrIPKasaNgaWebServisi(Session), Convert.ToInt32(cmbKonfigurimKase.Value));
                clsMenuInfo.ShtoMesazh(MenuInfo, mesazhkasa, pnlMesazhi);                
            }
            clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            if (cbDergoMeEmail.Checked && koka.IdStatusDokumenti == 1)
            {
                try
                {
                    (clsMesazh mes, clsMesazh informim) = ArketimeMailHelper.DergoFatureMeEmail(koka.IdKoka.ToString(), IdGjuha, IdPerdoruesi, IdNdermarrjeVit, IdNdermarrja);
                    if (informim.PershkrimMesazhi != "")
                        clsMenuInfo.ShtoMesazh(MenuInfo, informim, pnlMesazhi);
                    if (mes.PershkrimMesazhi != "")
                        clsMenuInfo.ShtoMesazh(MenuInfo, mes, pnlMesazhi);
                }
                catch (Exception ex)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Nje gabim ndodhi gjate dergimit me email.", pnlMesazhi);
                }
            }     

            InicializoGridFaturatSession(gridFaturat); //reseton griden e faturave ne sesion
            ASPxNavBar1.Groups[0].Expanded = false;
            status1.Value = "true";
            string url = "";
            if (hfShtimModifikimValue == "shtim" && Request.QueryString["idfatura"] != null)
            {
                switch (veprimi)
                {
                    case "terheqje":
                    case "pagese":
                        url = Request.QueryString["vjenNga"] == "Shto_RegjistrimDokumentash" ? "Shto_RegjistrimDokumentash.aspx?shitje_blerje=blerje&shtim_modifikim=shtim" : "RegjistrimDokumentash.aspx?shitje_blerje=blerje";
                        break;
                    case "derdhje":
                    case "arketim":
                        url = Request.QueryString["vjenNga"] == "Shto_RegjistrimDokumentash" ? "Shto_RegjistrimDokumentash.aspx?shitje_blerje=shitje&shtim_modifikim=shtim" : "RegjistrimDokumentash.aspx?shitje_blerje=shitje";
                        break;
                }
                status1.Value = "kthehu";
            }
            hfUrl1.Value = url;
            hfShtimModifikimValue = "shtim";
            hl = new HtmlTable();
            pnlLidhur.Update();
            referenca_TextBox.Text = clsKokaFleteKontabel.GjeneroNrReference(IdNdermarrjeVit).ToString();
            hfNrRef.Value = clsKokaFleteKontabel.GjeneroNrReference(IdNdermarrjeVit).ToString();
            object[] kokaWeb = koka.krijoObjektPerWebhook(koka, hfShtimModifikimValue, veprimi);
            colVeprimBankaTrupi trupi = new colVeprimBankaTrupi(koka.IdKoka);

            Object kokaDheTrupi;
            kokaDheTrupi = new
            {
                meta = kokaWeb[0],
                koka = kokaWeb[1],
                trupi
            };
            hfObjektRuajtur.Value = JsonConvert.SerializeObject(kokaDheTrupi);
            visibleMenu(id);
            clsFunksione.dergoWebhookDatasetEndpoint(kokaDheTrupi);
        }

        private clsMesazh validoDok(clsBanka bank, int statusi, clsKonfigurimAmbjenti konf, out clsPeriudhaKontabel periudha)
        {
            int idBanka = 0;
            bool kontroll = Int32.TryParse(banka_ComboBox.Value.ToString(), out idBanka);
            periudha = hfShtimModifikim.Value == "modifikim" ? new clsPeriudhaKontabel(data_DateEdit.Date, IdNdermarrja) : mySessionObjects.merrPeriudheKontabel(Session);
            if (kontroll)
            {
                clsMesazh mesazhArkBank = bank.mbushBanke(idBanka);
                if (!mesazhArkBank.Status)
                    return mesazhArkBank;
            }
            else
                return new clsMesazh(false, "ID e bankes nuk mund te parsohet ne int");
            if (!bank.AktivBanka)
                return new clsMesazh(false, "Kjo banke nuk eshte aktive");
            if (!bank.LlojArkaBanka && (veprimi == "derdhje" || veprimi == "terheqje"))
                return new clsMesazh(false, "Nuk mund te zgjidhni arke per veprimet me banken!");
            if (bank.LlojArkaBanka && (veprimi == "arketim" || veprimi == "pagese"))
                return new clsMesazh(false, "Nuk mund te zgjidhni banke per veprimet me arken!");
            if (kredite_ButtonEdit.Text != "")
            {
                if (!clsLlogari.ekzistonLlogari(kredite_ButtonEdit.Text, IdNdermarrja))
                    return new clsMesazh(false, "Kjo Llogari nuk ekziston");
                if (!clsLlogari.eshteLlogariAktive(kredite_ButtonEdit.Text, IdNdermarrja))
                    return new clsMesazh(false, "Kjo Llogari nuk eshte aktive!");
            }
            if (cmbDegeAdministrative.Text != string.Empty && cmbDegeAdministrative.Text != " ()")
            {
                clsDegeAdministrative dege = new clsDegeAdministrative(cmbDegeAdministrative.Text.Split(' ')[0], IdNdermarrja);
                if (dege.IdDegeAdministrative == -1)
                    return new clsMesazh(false, "Kjo dege administrative nuk ekziston!");
                if (dege.Aktiv == false)
                    return new clsMesazh(false, "Kjo dege administrative nuk eshte aktive!");
            }

            String mesazhGabimi;
            if (!clsFunksione.checkPeriudheKontabel(out mesazhGabimi, data_DateEdit.Date, periudha, statusi))
                return new clsMesazh(false, mesazhGabimi);

            if (DbCore.MbylljePeriudhe.PeriodClosing.IsPeriodClosed(data_DateEdit.Date, MyConnectionsManager.GetSelectedConNameServer(), IdNdermarrja, (veprimi == "derdhje" || veprimi == "terheqje") ? KategoriDokumenti.VeprimeBanke : KategoriDokumenti.VeprimeArke, konf.IdKonfigAmbjente))
                return new MesazhGabimi(MessagesResource.Messages["msgPeriodIsClosed"]);

            if (data_DateEdit.Date.Year != new clsNdermarrjeViti(IdNdermarrjeVit).Viti)
                return new clsMesazh(false, "Data nuk i perket vitit ushtrimor te zgjedhur");

            return new clsMesazh(true, "Kontrollet u kaluan me sukses!");
        }

        /// <summary>
        /// Merr vlerat e hidden field-eve ku jane vendosur vlerat e zgjedhura te trupi i dokumentit dhe gjenereon nje collection me objekte te tipit clsVeprimBankaTrupi
        /// </summary>
        /// <param name="serverUrl"></param>
        /// <param name="idetapa"></param>
        /// <param name="statusi"></param>
        /// <param name="veprimi"></param>
        /// <param name="statusAprovimi"></param>
        /// <param name="shfaqmesazhapolupe"></param>
        /// <param name="shfaqmesazhapolupeVdk"></param>
        /// <param name="id"></param>
        /// <param name="perBRM"></param>
        /// <returns>Kthen nje collection me objekte te tipit clsVeprimBankaTrupi</returns>
        private clsVeprimBankaKoka krijoDokumentArkaBanka(string serverUrl, int idetapa, int statusi, StatusAprovimi statusAprovimi, out string shfaqmesazhapolupe, out string shfaqmesazhapolupeVdk, int id, bool perBRM)
        {
            clsVeprimBankaKoka koka = new clsVeprimBankaKoka();
            clsKonfigurimAmbjenti konf = new clsKonfigurimAmbjenti();
            clsBanka bank = new clsBanka();
            clsPeriudhaKontabel periudha;

            if (konfigurimi_ComboBox.Text == "")
                konf.mbushKonfigAmbjSipasKod("A", IdNdermarrja);
            else
                konf.mbushKonfigAmbjSipasKod(konfigurimi_ComboBox.Text, IdNdermarrja);

            clsMesazh mesazhValidimi = validoDok(bank, statusi, konf, out periudha);
            if (!mesazhValidimi.Status)
                throw new DbCore.MyException(mesazhValidimi.PershkrimMesazhi);
            
            clsKonfigurimAmbjenti konflidhes = new clsKonfigurimAmbjenti();
            clsKokaQendraKosto qend = new clsKokaQendraKosto();
            int idllojdokumenti = (veprimi_ComboBox.Text == "Terheqje" || veprimi_ComboBox.Text == "Pagese") ? 3 : 4; //kujdes nuk eshte njesoj me kategorine por ka te beje me drejtimin e lekeve hyjne apo dalin 
            int idanullimi = (Request.QueryString["idanullimi"] != null) ? int.Parse(Request.QueryString["idanullimi"]) : 0;
            int iddege = 0, idllog = 0, idgrup = 0, idgrup2 = 0, idgrup3 = 0, idAutomjet = 0;
            bool mekontabilizim = false;
            string targa = "";
            double kursi = 1;

            if (cmbDegeAdministrative.Text != string.Empty && cmbDegeAdministrative.Text != " ()")
                iddege = int.Parse(cmbDegeAdministrative.Value.ToString());

            #region Grupimet
            if (!String.IsNullOrEmpty(cmbGrup1.Text))
            {
                bool parse = false;
                parse = int.TryParse(cmbGrup1.Value.ToString(), out idgrup);
                if (!parse)
                    throw new DbCore.MyException(String.Format("Grupimi i pare me kod {0} nuk ekziston!", cmbGrup1.Text));
                if (!DbCore.DbRegjistrim.clsGrupimDokumentiKoka.ekzistonGrup(cmbGrup1.Text, IdNdermarrja, 1))
                    throw new DbCore.MyException(String.Format("Grupimi i pare me kod {0} nuk ekziston!", cmbGrup1.Text));
            }
            if (!String.IsNullOrEmpty(cmbGrup2.Text))
            {
                bool parse = false;
                parse = int.TryParse(cmbGrup2.Value.ToString(), out idgrup2);
                if (!parse)
                    throw new DbCore.MyException(String.Format("Grupimi i dyte me kod {0} nuk ekziston!", cmbGrup2.Text));
                if (!DbCore.DbRegjistrim.clsGrupimDokumentiKoka.ekzistonGrup(cmbGrup2.Text, IdNdermarrja, 2))
                    throw new DbCore.MyException(String.Format("Grupimi i dyte me kod {0} nuk ekziston!", cmbGrup2.Text));
            }
            if (!String.IsNullOrEmpty(cmbGrup3.Text))
            {
                bool parse = false;
                parse = int.TryParse(cmbGrup3.Value.ToString(), out idgrup3);
                if (!parse)
                    throw new DbCore.MyException(String.Format("Grupimi i trete me kod {0} nuk ekziston!", cmbGrup3.Text));
                if (!DbCore.DbRegjistrim.clsGrupimDokumentiKoka.ekzistonGrup(cmbGrup3.Text, IdNdermarrja, 3))
                    throw new DbCore.MyException(String.Format("Grupimi i trete me kod {0} nuk ekziston!", cmbGrup3.Text));
            }
            #endregion

            if (!String.IsNullOrEmpty(kursi_TextBox.Text))
                double.TryParse(kursi_TextBox.Text, out kursi);
            else
                kursi = 1;
            
            double komision = Convert.ToDouble(komision_TextBox.Value);
            if (komision != 0)
            {
                if (bank.Komisioni == 0)
                    throw new DbCore.MyException("Kjo banke nuk ka llogari komisioni");
                koka.KomisioniBankar = komision;
            }

            if (kredite_ButtonEdit.Text != "")
                idllog = clsLlogari.mbushIDLlogariSipasKodit(kredite_ButtonEdit.Text, IdNdermarrja);

            if (btneAutomjet.Text != "")
            {
                idAutomjet = int.Parse(btneAutomjet.Value.ToString());
                targa = txtTarga.Text;
            }

            if (hfKontabilizimi.Value != "0" && statusi == 1)//Nese eshte me kontabilizim
                mekontabilizim = true;

            JavaScriptSerializer serializusi = new JavaScriptSerializer();
            object[] nivele = (object[])serializusi.DeserializeObject(hfNivele.Value);
            
            konflidhes.mbushKonfigAmbjSipasId(konf.IdKonfigurimi, IdGjuha);
            colVeprimBankaTrupi trupi = RuajTrupinVeprimitBankes(bank.IdMonedhaBanka, perBRM);
            if (trupi.Count == 0)
                throw new Exception(MessagesResource.Messages["msgTrupiDokNukDuhetBosh"]);
            if (hfShtimModifikim.Value == "modifikim")
            {
                clsKokaFleteKontabel kok = new clsKokaFleteKontabel();
                if (veprimi == "terheqje" || veprimi == "derdhje")
                    kok = new clsKokaFleteKontabel(int.Parse(Request.QueryString["id"]), 4);
                else
                    kok = new clsKokaFleteKontabel(int.Parse(Request.QueryString["id"]), 3);

                qend.KtheKokaQKSipasIDGjeneruesDheKonfigMeTrup(kok.IdKokaFleteKontabel, kok.IdKonfigAmbjente);
                id = int.Parse(Request.QueryString["id"]);
            }
            int idPerdoruesi = IdPerdoruesi;
            if (statusAprovimi == StatusAprovimi.Deleguar)
            {
                clsEtapeAprovimi etapa = new clsEtapeAprovimi(idetapa, 3);
                if (etapa.LlojAprovuesi == 2)   //nqs eshte rol
                {
                    colRolPerdorues col = new colRolPerdorues();
                    col.mbushRolePerdoruesSipasRoli(etapa.IdAprovuesi);
                    if (col.Count > 0)
                        idPerdoruesi = col[0].IdPerdorues;
                }
            }
            clsMesazh mesazh = koka.krijoVeprimeBanke(bank.IdBanka, bank.KodiBanka, kursi, data_DateEdit.Date, data_regj_DateEdit.Date, nrDokumenti_TextBox.Text, int.Parse(referenca_TextBox.Text), nrSerial_TextBox.Text, pershkrimi_Memo.Text, 1, "Me mirebesim", Convert.ToDouble(vlera_TextBox.Value), Convert.ToDouble(vleraMonedhaBaze_TextBox.Value), komision, komision * kursi, veprimi_ComboBox.Text, idPerdoruesi, idllojdokumenti, statusi, IdNdermarrjeVit, konf.IdKonfigAmbjente, 0, 0, 0, konf.IdNivel, 0, iddege, cmbDegeAdministrative.Text.Split(' ')[0], IdNdermarrja, idllog, trupi, mekontabilizim, periudha.IdPeriudha, bank.IdMonedhaBanka, kredite_ButtonEdit.Text, idgrup, idgrup2, idgrup3, konflidhes, nivele, new clsDatabaseArkaBanka(), null, out shfaqmesazhapolupe, out shfaqmesazhapolupeVdk, qend.ColTrupi, id, txtShoqeria.Text, txtCustomerNr.Text, idAutomjet, targa, Convert.ToInt32(cmbFormatiPrintimit.Value), txtFinancieri.Text, txtDhenesiMarresi.Text, txtArketari.Text, cbKasa.Checked, statusAprovimi, txtArsye.Text, idanullimi, txtNrLlogari.Text, hfArkiva, idKategori, idPerdoruesi, cbPrinto.Checked, idPerdoruesi);
            if (!mesazh.Status)
                throw new DbCore.MyException(mesazh.PershkrimMesazhi);
            if (koka.oColTrupi.Count < 1)
                throw new Exception("Ju lutem plotesoni trupin e dokumentit!");

            return koka;
        }

        public colVeprimBankaTrupi RuajTrupinVeprimitBankes(int idmonedhabanka, bool perBRM)
        {
            colVeprimBankaTrupi trupat = new colVeprimBankaTrupi();

            JavaScriptSerializer serializusi = new JavaScriptSerializer();
            object[] dokumenti = (object[])serializusi.DeserializeObject(gridDataObject.Value);
            object[] nivele = (object[])serializusi.DeserializeObject(hfNivele.Value);
            object[] id = (object[])serializusi.DeserializeObject(hfId.Value);
            //object[] kmk = (object[])serializusi.DeserializeObject(hfKMK.Value);

            for (int i = 0; i < dokumenti.Length; i++)
            {
                clsVeprimBankaTrupi trupArkaBanka = new clsVeprimBankaTrupi((Dictionary<string, object>)dokumenti[i], nivele[i], IdNdermarrja, data_DateEdit.Date, idmonedhabanka, kursi_TextBox.Text, hfShtimModifikim.Value, hfKursiEkzistues.Value, id[i], IdPerdoruesi, perBRM);
                if (trupArkaBanka.IdSubjekti == -1 && String.IsNullOrEmpty(trupArkaBanka.Lloji))
                    continue;

                if (!String.IsNullOrEmpty(trupArkaBanka.Lloji) && (trupArkaBanka.IdSubjekti == -1 || trupArkaBanka.IdSubjekti == 0))
                {
                    var mesazhi = trupArkaBanka.Lloji == "Llogari" ? MessagesResource.Messages["msgLlogariaNukEzistonOseJoAktive"] : trupArkaBanka.Lloji == "Punonjes" ? MessagesResource.Messages["msgPunonjesiNukEzistonOseJoAktiv"] : MessagesResource.Messages["msgKFnukEkziston"];
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhi, pnlMesazhi);
                    status1.Value = "false";
                    return new colVeprimBankaTrupi();
                }
                clsMonedha monedha = new clsMonedha();
                switch (trupArkaBanka.Lloji)
                {
                    case "Llogari":
                        monedha.mbushMonedhenSipasLlogari(trupArkaBanka.IdSubjekti);
                        break;
                    case "Furnitor":
                    case "Klient":
                        clsKlientFurnitor kf = new clsKlientFurnitor(trupArkaBanka.IdSubjekti);
                        monedha.mbushMonedhePershk(kf.Monedha, IdNdermarrja);
                        break;
                    case "Punonjes":
                        clsPunonjes p = new clsPunonjes(trupArkaBanka.IdSubjekti);
                        monedha.mbushMonedhePershk(p.Monedha, IdNdermarrja);
                        break;
                }
                if (hfMonedhaNder.Value != clsMonedha.ktheKodMonedheSipasId(idmonedhabanka) && monedha.KodiMonedha != hfMonedhaNder.Value && monedha.IdMonedha != idmonedhabanka && monedha.IdMonedha != 0)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukMundTeKryheniVeprimeMeKF"], pnlMesazhi);
                    status1.Value = "false";
                    return new colVeprimBankaTrupi();
                }
                if (trupArkaBanka.IdSubjekti != -1)
                    trupat.Add(trupArkaBanka);
            }
            return trupat;
        }

        /// <summary>
        /// Llogarit diferencen nga kursi sipas formules se paracaktuar
        /// </summary>
        /// <param name="kv">Kursi i bankes</param>
        /// <param name="kk">Kursi i fatures</param>
        /// <param name="kmk">Kursi i monedhes se fatures ne daten e dokumentit te bankes</param>
        /// <param name="vv">Vlera e dokumentit te bankes</param>
        /// <returns>Kthen vleren e diferences nga kursi</returns>
        protected double LlogaritDiferenceKursi(double kv, double kk, double kmk, double vv)
        {
            if (kmk != 0)
                return vv * kv - vv * kv * kk / kmk;
            return 0;
        }

        /// <summary>
        /// Kur klikohet butoni ruaj ne fund te faqes. Thirret funksioni <see cref="RuajVeprimBanke"/> per te ruajtur dokumentin si dokument i rregullt
        /// </summary>
        protected void ruaj_Button_Click(object sender, EventArgs e)
        {
            Page.Validate();
            var idGjuha = (int)hfState["idGjuha"];
            RuajVeprimBanke(1, false, (ASPxGridView)ASPxNavBar1.Groups[0].FindControl("grid_faturat"), StatusAprovimi.Undefined);
        }

        /// <summary>
        /// Kur klikohet butoni ruaj si draft ne fund te faqes. Thirret funksioni <see cref="RuajVeprimBanke"/> per te ruajtur dokumentin si draft
        /// </summary>
        protected void ruaj_draft_Click(object sender, EventArgs e)
        {
            Page.Validate();
            var idGjuha = (int)hfState["idGjuha"];
            RuajVeprimBanke(0, false, (ASPxGridView)ASPxNavBar1.Groups[0].FindControl("grid_faturat"), StatusAprovimi.Undefined);
        }

        /// <summary>
        /// Percakton veprimin qe do te kryhet sipas zgjedhjes qe eshte bere duke klikuar tek menuja ne krye te faqes
        /// </summary>
        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {//veprimet e menuse
            var veprimi = Request.QueryString["lloji"];
            var idKatDokShitje = (int)hfState["idKatDokShitje"];
            var idKategori = (int)hfState["idKategori"];

            var komponente = clsFunksione.GetKomponente(Page.Request);
            var gridFaturat = (ASPxGridView)ASPxNavBar1.Groups[0].FindControl("grid_faturat");
            switch (e.Item.Name)
            {
                case "Ruaj":
                    Page.Validate();
                    RuajVeprimBanke(1, false, gridFaturat, StatusAprovimi.Undefined);
                    break;
                case "Draft":
                    Page.Validate();
                    RuajVeprimBanke(0, false, gridFaturat, StatusAprovimi.Undefined);
                    break;
                case "PrintPreview":

                    if (hfShtimModifikim.Value == "modifikim")
                    {
                        Container55.Attributes["src"] = "RaportiShpejte.aspx?Sesioni=false&emriReal=mandatArketimPagese&idDokumenti=" + Request.QueryString["id"] + "&printo=false&raportdyte=jo&iddesign=" + cmbFormatiPrintimit.Value;

                    }
                    else
                    {
                        Page.Validate();
                        RuajVeprimBanke(1, false, gridFaturat, StatusAprovimi.Undefined);
                    }
                    break;
                case "RuajPrint":
                    Page.Validate();
                    RuajVeprimBanke(1, true, gridFaturat, StatusAprovimi.Undefined);
                    break;
                case "Fshi":
                    var id = int.Parse(Request.QueryString["id"]);
                    var colKoka = new colVeprimBankaKoka(id, 4);
                    colKoka[0].fshi(rm, ci);
                    Response.Redirect("VeprimeBanka.aspx?lloji=" + veprimi);
                    break;
                case "Aprovo":
                    Page.Validate();
                    RuajVeprimBanke(0, false, gridFaturat, StatusAprovimi.Per_Aprovim);
                    break;
                case "Refuzo":
                    Page.Validate();
                    RuajVeprimBanke(0, false, gridFaturat, StatusAprovimi.Refuzuar);
                    break;
                case "Delego":
                    Page.Validate();
                    RuajVeprimBanke(0, false, gridFaturat, StatusAprovimi.Deleguar);
                    break;
            }
        }

        //fshin rreshtat e selektuar
        protected void ButtonOk_Click2(object sender, EventArgs e)
        {

            pergjigja.Text = "";
            var id = int.Parse(Request.QueryString["id"]);
            var koka = new clsVeprimBankaKoka(id);
            var mesazhi = new clsMesazh();
            koka.IdPerdoruesi = IdPerdoruesi;
            var lidhur = koka.eshteDokumentiILidhur();
            if (lidhur)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["vepBankaMsgDokEshteILidhurNukFshihet"], pnlMesazhi);
                return;
            }
            if (koka.DateDokumenti.Year != new clsNdermarrjeViti((int)hfState["idNdermarrjeVit"]).Viti)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgDataNukPerketVititUshtrimor"], pnlMesazhi);
                return;
            }
            var idNdermarrje = (int)hfState["idNdermarrje"];
            var ekycur = clsPeriudhaKontabel.eshteKycurPeriudheSipasDateDheNdermarrjes(koka.DateDokumenti, idNdermarrje);
            if (ekycur)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgPeriudhaEKycur"], pnlMesazhi);
                return;
            }

            bool isVeprimBanke = (veprimi.EqualsAnyIgnoreCase("derdhje", "terheqje"));
            if (DbCore.MbylljePeriudhe.PeriodClosing.IsPeriodClosed(koka.DateDokumenti, MyConnectionsManager.GetSelectedConNameServer(), idNdermarrje, isVeprimBanke ? KategoriDokumenti.VeprimeBanke : KategoriDokumenti.VeprimeArke, koka.IdKonfigAmbjente))
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgPeriodIsClosed"], pnlMesazhi);
                return;
            }

            mesazhi = koka.fshi(rm, ci);
            if (mesazhi.Status)
            {
                object[] kokaWeb = koka.krijoObjektPerWebhook(koka,"Fshirje", veprimi);
                colVeprimBankaTrupi trupi = new colVeprimBankaTrupi(koka.IdKoka);

                Object kokaDheTrupi;
                kokaDheTrupi = new
                {
                    meta = kokaWeb[0],
                    koka = kokaWeb[1],
                    trupi
                };
                hfObjektRuajtur.Value = JsonConvert.SerializeObject(kokaDheTrupi);
                Response.Redirect("VeprimeBanka.aspx?lloji=" + Request.QueryString["lloji"] + "&fshi=po" + "&hfObjektRuajtur="+hfObjektRuajtur.Value);
                
            }
            else
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhi.PershkrimMesazhi, pnlMesazhi);
        }

        /// <summary>
        /// Kur klikohet butoni anullo ne fund te faqes. Perdoruai dergohet te lista e veprimeve te bankes
        /// </summary>
        protected void anullo_Button_Click(object sender, EventArgs e)
        {
            Response.Redirect("VeprimeBanka.aspx?lloji=" + Request.QueryString["lloji"]);
        }

        protected void banka_CustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = true;
            if (banka_ComboBox.Text == "")
                args.IsValid = false;
            var periudha = mySessionObjects.merrPeriudheKontabel(Session);
            if (Convert.ToDateTime(data_DateEdit.Text) < periudha.FillimiPeriudha || Convert.ToDateTime(data_DateEdit.Text) > periudha.MbarimiPeriudha)
                args.IsValid = false;
        }

        /// <summary>
        /// Merr nje numer dhe e kthen vleren e tij me fjale
        /// </summary>
        /// <param name="numb">Numri qe duhet te kthehet ne fjale</param>
        public String ChangeToWords(String numb)
        {
            String val = "", wholeNo = numb, points = "", andStr = "", pointStr = "";
            var endStr = ("");
            if (numb.Trim() == "")
                return "";
            try
            {
                var decimalPlace = numb.IndexOf(".");
                var idxPresjes = numb.IndexOf(",");
                if (decimalPlace > 0 && idxPresjes <= 0)
                {
                    wholeNo = numb.Substring(0, decimalPlace);
                    points = numb.Substring(decimalPlace + 1);
                    if (Convert.ToInt32(points) > 0)
                    {
                        andStr = ("Dhe");
                        pointStr = (" ") + EnglishFromNumber(int.Parse(points)) + (" Qindarka");
                    }
                }
                else if (idxPresjes > 0 && decimalPlace <= 0)
                {
                    var pjeset = numb.Split(',');
                    var numri = pjeset.Aggregate("", (current, t) => current + t);
                    wholeNo = numri;
                }
                else if (idxPresjes > 0 && decimalPlace > 0)
                {
                    var pjeset = numb.Split(',');
                    var numri = pjeset.Aggregate("", (current, t) => current + t);
                    numb = numri;
                    decimalPlace = numb.IndexOf(".");
                    wholeNo = numb.Substring(0, decimalPlace);
                    points = numb.Substring(decimalPlace + 1);
                    if (Convert.ToInt32(points) > 0)
                    {
                        andStr = ("Dhe");
                        pointStr = (" ") + EnglishFromNumber(int.Parse(points)) + (" Qindarka");
                    }
                }
                val = $"{EnglishFromNumber(int.Parse(wholeNo)).Trim()} {andStr}{pointStr} {endStr}";
            }
            catch (Exception err)
            {
                LogManager.GetCurrentClassLogger().Error(err.Message);
            }

            var fundi = "";
            if (val != "")
                fundi = val.Substring(val.Length - 4, 3);
            if (fundi == " E ")
                val = val.Substring(0, val.Length - 4);

            return val;
        }

        public static string EnglishFromNumber(long number)
        {
            if (number == 0)
            {
                return _onesMapping[number];
            }

            string retVal = null;
            var group = 0;
            while (number > 0)
            {
                var numberToProcess = (int)(number % 1000);
                number = number / 1000;

                var groupDescription = ProcessGroup(numberToProcess);
                if (groupDescription != null)
                {
                    if (group > 0)
                    {
                        retVal = _groupMapping[group] + " E " + retVal;
                    }
                    retVal = groupDescription + " " + retVal;
                }
                group++;
            }
            return /*sign + */" " + retVal;
        }

        private static string ProcessGroup(int number)
        {
            var tens = number % 100;
            var hundreds = number / 100;

            string retVal = null;
            if (hundreds > 0)
            {
                retVal = _onesMapping[hundreds] + " " + _groupMapping[0];
            }
            if (tens > 0)
            {
                if (tens < 20)
                {
                    retVal += ((retVal != null) ? " E " : "") + _onesMapping[tens];
                }
                else
                {
                    var ones = tens % 10;
                    tens = (tens / 10) - 2; // 20's offset

                    retVal += ((retVal != null) ? "  E " : "") + _tensMapping[tens];

                    if (ones > 0)
                    {
                        retVal += ((retVal != null) ? " E " : "") + _onesMapping[ones];
                    }
                }
            }
            return retVal;
        }

        /// <summary>
        /// Thirret sa here ndryshohet vlera te koka e dokumentit per te marre vleren ne fjale te numrit te vendosur
        /// </summary>
        protected void ASPxCallback1_Callback(object source, CallbackEventArgs e)
        {
            e.Result = ChangeToWords(vlera_TextBox.Value.ToString());
        }

        /// <summary>
        /// Thirret kur hapet dokumenti per shtim apo modifikim. Vendos si vlere kursi kursin e fundit te monedhes se bankes se zgjedhur te koka e dokuemntit
        /// </summary>
        /// <param name="idNdermarrje"></param>
        protected void VendosVlerenKursit()
        {
            var banka = new clsBanka();
            if (banka_ComboBox.Text != "" && banka_ComboBox.Value != null)
                banka.mbushBanke(int.Parse(banka_ComboBox.Value.ToString()));
            if (banka.IdBanka != 0)
            {
                var kurset = new colKurset();
                kurset.mbushKursetFunditMonedhes(banka.IdMonedhaBanka);
                foreach (var kurs in kurset)
                    if (kurs.LlojKursi == 1 && kurs.DataKursit == data_DateEdit.Date)
                        kursi_TextBox.Text = kurs.VleraKursi.ToString();
            }
        }

        protected void btnPeriudha_TextChanged(object sender, EventArgs e)
        {
            VendosDataDefault(mySessionObjects.merrPeriudheKontabel(Session));
        }

        protected void btneAutomjet_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].Contains("btneAutomjet"))
                {
                    var value = 0;
                    if (e.Value == null || !Int32.TryParse(e.Value.ToString(), out value))
                        return;
                    ConfigureAspxComboBox.mbushComboAutomjetiByID((ASPxComboBox)source, value);
                }
            }
        }

        protected void btneAutomjet_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].Contains("btneAutomjet"))
                {
                    var idNdermarrje = (int)hfState["idNdermarrje"];
                    ConfigureAspxComboBox.mbushComboAutomjetesh( idNdermarrje, btneAutomjet,e);
                }
            }
        }

        protected void banka_ComboBox_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].Contains("banka_ComboBox"))
                {
                    var value = 0;
                    if (e.Value == null || !Int32.TryParse(e.Value.ToString(), out value))
                        return;
                    ConfigureAspxComboBox.mbushComboArkaBankaById((ASPxComboBox)source, value);
                }
            }
        }

        protected void banka_ComboBox_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("banka_ComboBox"))
                ConfigureAspxComboBox.mbushComboArkaBankaSipasFilter(e.Filter, e.BeginIndex + 1, e.EndIndex + 1, (ASPxComboBox)source, Request.QueryString["lloji"], IdPerdoruesi, IdNdermarrja);
        }

        protected void kredite_ButtonEdit_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("kredite_ButtonEdit"))
                ConfigureAspxComboBox.mbushComboLlogariaPaKolona(IdPerdoruesi, IdNdermarrja, kredite_ButtonEdit, e);
        }

        #region  GRIDA E FATURAVE

        private void InicializoGridFaturat(ASPxGridView gridFaturat)
        {
            var dt = new DataTable();
            clsKonfigurimAmbjenti konf = new clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasKod(konfigurimi_ComboBox.Text.Split(';')[0], mySessionObjects.merrIdNdermarrjeSesioni(Session));
            dt = colDokumentat.mbushKokaShitjePaLikuiduar(IdNdermarrja, idKatDokShitje, Convert.ToString(hfTeDrejtaGjitheDokPerTuLikujduar.Get("kushtDokPerLikujdim")), IdPerdoruesi, Int32.Parse(konfigurimi_ComboBox.Value.ToString()));
            mySessionObjects.ruajGridFaturatNeSession(komponente + IdNdermarrja + idKatDokShitje, Session, dt);
            if (gridFaturat.Columns.Count <= 1)
                gridFaturat.Columns.Clear();
            gridFaturat.DataSource = dt;
            gridFaturat.DataBind();
            dt.Dispose();
        }

        private string MerrKodKlientiPerFiltrim()
        {
            bool faturaNgaBRM = (Request.QueryString["lloji"] == "arketimLlogariKlienti" || Request.QueryString["lloji"] == "arketimAbonent");
            if (faturaNgaBRM)
                return string.Empty;
            if (Request.QueryString["shtim_modifikim"] == "modifikim")
                return string.Empty;
            if (furnitori_ComboBox.Text == "")
                return string.Empty;

            return  (furnitori_ComboBox.Text.Substring(0, furnitori_ComboBox.Text.IndexOf(' '))).ToString();
        }

        private void KonfiguroGrideFaturat( ASPxGridView gridFaturat)
        {

            KonfigurimComboGride.ShtoNivelMeDataSource(gridFaturat, () =>
            {
                var nivelet = new colNivelRegjistrimi();
                nivelet.mbushGjitheNivelRegjistrimiSipasSuperKat(IdNdermarrja, IdPerdoruesi, 2);
                return nivelet;
            }, Session, komponente, guidString, "IdNiveli");
            KonfigurimComboGride.ShtoModelMeDataSource(gridFaturat, () =>
            {
                var konfigurimet = new colKonfigurimAmbjenti();
                konfigurimet.mbushKonfigAmbjSipasIdKategori(20, IdNdermarrja, IdPerdoruesi, IdGjuha, false);
                konfigurimet.mbushKonfigAmbjSipasIdKategori(1, IdNdermarrja, IdPerdoruesi, IdGjuha, false);
                konfigurimet.mbushKonfigAmbjSipasIdKategori(2, IdNdermarrja, IdPerdoruesi, IdGjuha, false);
                return konfigurimet;
            }, Session, komponente, guidString);
            KonfigurimComboGride.ShtoMonedhe(gridFaturat, IdNdermarrja, IdPerdoruesi, Session, komponente, guidString);
            GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(IdNdermarrja, "grid_faturat", gridFaturat, konfigurimi_ComboBox.Text, "301", IdGjuha);
            gridFaturat.Columns["#"].VisibleIndex = 0;
            GridUtil.konfigGrideListeEMadhePaTheme(gridFaturat, "IdDokumenti");
            PercaktoTemplateKryesor(gridFaturat);
            if (!IsPostBack)
            {
                if (gridFaturat.FilterExpression == "")
                {
                    string kodKlientFurnitor = MerrKodKlientiPerFiltrim();
                    gridFaturat.FilterExpression = String.IsNullOrWhiteSpace(kodKlientFurnitor) ? "" : $"[KodiKlientit] = '{kodKlientFurnitor}'";
                }
            }
        }

        private void PershtatKolonaPerFaturaNgaBRM(ASPxGridView grid)
        {
            foreach (GridViewDataColumn col in grid.DataColumns)
            {
                switch (col.FieldName)
                {
                    case "NrDokumenti":
                        col.Caption = "Kod fature";
                        col.Visible = true;
                        break;
                    case "Pershkrimi":
                        col.Caption = "Muaj fature";
                        col.Visible = true;
                        break;
                    case "VleftaPaLikujduar":
                        col.Caption = "Vlera e mbetur";
                        col.Visible = true;
                        break;
                    case "Vlefta":
                        col.Caption = "Vlera fillestare";
                        col.Visible = true;
                        break;
                    case "StatusFature":
                        col.Caption = "Status Fature";
                        col.Visible = true;
                        break;
                    default:
                        col.Visible = false;
                        break;
                }
            }
        }

        private void InicializoGridFaturatSession(ASPxGridView gridFaturat)
        {
            gridFaturat.DataSource = null;
            mySessionObjects.ruajGridFaturatNeSession(komponente + IdNdermarrja + idKatDokShitje, Session, null);
            gridFaturat.DataBind();
        }

        private void MerrGridFaturatSession(ASPxGridView gridFaturat)
        {
            var objNgaSessioni = mySessionObjects.merrGridFaturatNgaSessioni(komponente + IdNdermarrja + idKatDokShitje, Session);
            if (objNgaSessioni == null)
                InicializoGridFaturat(gridFaturat);
            else
            {
                gridFaturat.DataSource = objNgaSessioni;
                if (gridFaturat.Columns.Count <= 1)
                    gridFaturat.Columns.Clear();
                gridFaturat.DataBind();
                objNgaSessioni.Dispose();
            }
        }    

        protected void grid_faturat_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {
            string dtDokumenti = e.GetValue("DtDokumenti").ToString();
            string dtAzhornimi = e.GetValue("DtAzhornimi").ToString();
            string kursAzhornimi = e.GetValue("KursAzhornimi").ToString();
            switch (e.DataColumn.FieldName)
            {
                case "Kursi":
                    if (dtAzhornimi == "" || Convert.ToDateTime(dtAzhornimi) <= Convert.ToDateTime(dtDokumenti))
                        e.Cell.ForeColor = Color.Green;
                    break;
                case "KursAzhornimi":
                    if (kursAzhornimi == "&nbsp;" || string.IsNullOrWhiteSpace(kursAzhornimi) || Convert.ToDouble(kursAzhornimi) == 0)
                        e.Cell.Text = "";
                    else if (Convert.ToDateTime(dtAzhornimi) > Convert.ToDateTime(dtDokumenti))
                        e.Cell.ForeColor = Color.Green;
                    break;
                case "DtAzhornimi":
                    if (dtAzhornimi == "&nbsp;" || dtAzhornimi.ContainsAnyIgnoreCase("1900"))
                        e.Cell.Text = "";
                    else if (Convert.ToDateTime(dtAzhornimi) > Convert.ToDateTime(dtDokumenti))
                        e.Cell.ForeColor = Color.Green;
                    break;
            }
        }
        
        protected void furnitori_ComboBox_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (!IsCallback || !Request.Params["__CALLBACKID"].Contains("furnitori_ComboBox"))
                return;
            int value = 0;
            if (e.Value == null || !Int32.TryParse(e.Value.ToString(), out value))
                return;

            ConfigureAspxComboBox.KonfiguroComboBoxKlientFurnitoriById(IdPerdoruesi, IdNdermarrja, (ASPxComboBox)source, value);
        }

        protected void furnitori_ComboBox_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("furnitori_ComboBox"))
                ConfigureAspxComboBox.KonfiguroComboBoxKlientFurnitor(e.Filter, e.BeginIndex + 1, e.EndIndex + 1, (ASPxComboBox)source, konfigurimi_ComboBox.Text, IdPerdoruesi, IdNdermarrja, IdGjuha, 0);
        }

        protected void grid_faturat_DataBound(object sender, EventArgs e)
        {
            if (!(sender is ASPxGridView)) return;
            var gridFaturat = (ASPxGridView)sender;
            if (gridFaturat.Columns["#"] != null) return;

            var check = new GridViewCommandColumn("#");
            check.ShowSelectCheckbox = true; check.Width = Unit.Percentage(2);
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
            var gridFaturat = (ASPxGridView)sender;
            var dt = new DataTable();
            var idKatDokShitje = (int)hfState["idKatDokShitje"];
            var komponente = clsFunksione.GetKomponente(Page.Request);

            if (e.Parameters == "mbush" || e.Parameters == "kushtDokPerLikujdim")
            {
                if (Request.QueryString["lloji"] == "arketimLlogariKlienti" || Request.QueryString["lloji"] == "arketimAbonent")
                {
                    dt = mySessionObjects.MerrFaturatBRMngaSession(Session).Item6;
                    grid_faturat_VendosTotaletEDetyrimeve(dt, gridFaturat, e.Parameters.Contains("pastro"));
                    if (e.Parameters == "mbush")
                    {
                        if (!ValidoKlientin(dt))
                        {
                            gridFaturat.JSProperties["cpMesazhKlientTerminated"] = MessagesResource.Messages["msgKlientMeStatusTerminated"];
                            gridFaturat.JSProperties["cpKlientTerminated"] = "True";
                        }
                        else
                            gridFaturat.JSProperties["cpKlientTerminated"] = "";
                    }
                }
                else
                    dt = mySessionObjects.merrGridFaturatNgaSessioni(komponente + IdNdermarrja + idKatDokShitje, Session);
                //dt = colDokumentat.mbushKokaShitjePaLikuiduar(IdNdermarrja, idKatDokShitje, Convert.ToString(hfTeDrejtaGjitheDokPerTuLikujduar.Get("kushtDokPerLikujdim")), IdPerdoruesi, Int32.Parse(konfigurimi_ComboBox.Value.ToString()));
                //mySessionObjects.ruajGridFaturatNeSession(komponente + IdNdermarrja + idKatDokShitje, Session, dt);
            }
            else if (e.Parameters == "pastro")
            {
                gridFaturat.FocusedRowIndex = -1;
                gridFaturat.Selection.UnselectAll();
                dt = mySessionObjects.merrGridFaturatNgaSessioni(komponente + IdNdermarrja + idKatDokShitje, Session);
                gridFaturat.FilterExpression = "";
            }

            else if (e.Parameters == "pastroP")
            {
                gridFaturat.FocusedRowIndex = -1;
                gridFaturat.Selection.UnselectAll();
                dt = mySessionObjects.merrGridFaturatNgaSessioni(komponente + IdNdermarrja + idKatDokShitje, Session);
                string kodKlientFurnitor = MerrKodKlientiPerFiltrim();
                gridFaturat.FilterExpression = String.IsNullOrWhiteSpace(kodKlientFurnitor) ? "" : "[KodiKlientit] = '" + kodKlientFurnitor;
            }

            else if (e.Parameters == "pastroKF")
            {
                gridFaturat.FocusedRowIndex = -1;
                gridFaturat.Selection.UnselectAll();
                dt = mySessionObjects.merrGridFaturatNgaSessioni(komponente + IdNdermarrja + idKatDokShitje, Session);
                string kodKlientFurnitor = MerrKodKlientiPerFiltrim();
                gridFaturat.FilterExpression = String.IsNullOrWhiteSpace(kodKlientFurnitor) ? "" : "[KodiKlientit] = ''" + kodKlientFurnitor;
            }  
         
            gridFaturat.KeyFieldName = "IdDokumenti";
            gridFaturat.DataSource = dt;
            gridFaturat.DataBind();
            
            if (dt != null)
            {
                //KonfiguroGrideFaturat(gridFaturat);
                dt.Dispose();
            }         
        }
        protected void grid_faturat_VendosTotaletEDetyrimeve(DataTable dt, ASPxGridView grida, bool pastro)
        {
            if (pastro)
            {
                grida.JSProperties["cpDetyrimiMbeturActive"] = 0;
                grida.JSProperties["cpDetyrimiMbeturTerminated"] = 0;
                grida.JSProperties["cpNrFaturashActive"] = 0;
                grida.JSProperties["cpNrFaturashTerminated"] = 0;
                return;
            }
            try
            {
                if (dt == null || dt.Rows.Count == 0)
                    return;
                int nrFaturashTerminated = 0, nrFaturashActive = 0;
                decimal detyrimiMbeturActive = 0, detyrimiMbeturTerminated = 0;
                foreach (DataRow row in dt.Rows)
                {
                    if ((string)row["StatusFature"] == "Terminated" && (decimal)row["VleftaPaLikujduar"] > 0)
                    {
                        detyrimiMbeturTerminated += (decimal)row["VleftaPaLikujduar"];
                        nrFaturashTerminated++;
                    }
                    else if ((string)row["StatusFature"] == "Active")
                    {
                        detyrimiMbeturActive += (decimal)row["VleftaPaLikujduar"];
                        nrFaturashActive++;
                    }
                }
                grida.JSProperties["cpDetyrimiMbeturActive"] = detyrimiMbeturActive.ToString();
                grida.JSProperties["cpDetyrimiMbeturTerminated"] = detyrimiMbeturTerminated.ToString();
                grida.JSProperties["cpNrFaturashActive"] = nrFaturashActive.ToString();
                grida.JSProperties["cpNrFaturashTerminated"] = nrFaturashTerminated.ToString();
            }
            catch (Exception ex)
            {
                ImbLogger.Error(ex, $"Ndodhi nje problem ne mbledhjen e totalit te palikujduar  dt:{Newtonsoft.Json.JsonConvert.SerializeObject(dt)}");
            }
        }
        
        protected bool ValidoKlientin(DataTable dt)
        {
            if (string.IsNullOrEmpty(txtNrLlogari.Text))
                return true;
            colKlienteTerminated klienteTerminated = new colKlienteTerminated();
            if (!klienteTerminated.Contains(txtNrLlogari.Text.Trim()))
                return true;
            if (dt.Rows.Count == 0)
                return false;
            return (dt.AsEnumerable().Count(row => row["StatusFature"].ToString() == "Active") > 0);
        }

        protected void grid_faturat_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            var gridFaturat = (ASPxGridView)sender;
            e.Properties["cpNoRows"] = gridFaturat.VisibleRowCount;
            Dictionary<object, int> visibleIndices = new Dictionary<object, int>();
            
            for (int i = 0; i < gridFaturat.VisibleRowCount; i++)
            {
                if (gridFaturat.GetRowValues(i, gridFaturat.KeyFieldName) == null)
                    return;
                visibleIndices.Add(gridFaturat.GetRowValues(i, gridFaturat.KeyFieldName), i);
            }  
            e.Properties["cpIndices"] = visibleIndices;
        }

        /// <summary>
        /// Template per griden e dokumentave kryesore
        /// </summary>
        private void PercaktoTemplateKryesor(ASPxGridView gridFaturat)
        {
            gridFaturat.Columns["#"].VisibleIndex = 0;

            var kursi = gridFaturat.Columns["Kursi"] as GridViewDataTextColumn;
            kursi.PropertiesEdit.DisplayFormatString = "0.00####";

            var kursAzhornimi = gridFaturat.Columns["KursAzhornimi"] as GridViewDataTextColumn;
            kursAzhornimi.PropertiesEdit.DisplayFormatString = "0.00####";
            kursAzhornimi.Caption = "Kurs Azhornimi";

            var pershkrimi = gridFaturat.Columns["Pershkrimi"] as GridViewDataTextColumn;
            pershkrimi.CellStyle.Wrap = DefaultBoolean.True;

            var vlefta = gridFaturat.Columns["Vlefta"] as GridViewDataTextColumn;
            vlefta.PropertiesEdit.DisplayFormatString = "0.00";

            var vleftaPaLikujduar = gridFaturat.Columns["VleftaPaLikujduar"] as GridViewDataTextColumn;
            vleftaPaLikujduar.PropertiesEdit.DisplayFormatString = "0.00";

            var VleftaMonBaze = gridFaturat.Columns["VleftaMonBaze"] as GridViewDataTextColumn;
            VleftaMonBaze.PropertiesEdit.DisplayFormatString = "0.00";

            var VleftaPaLikujduarMonBaze = gridFaturat.Columns["VleftaPaLikujduarMonBaze"] as GridViewDataTextColumn;
            VleftaPaLikujduarMonBaze.PropertiesEdit.DisplayFormatString = "0.00";


            if (Request.QueryString["lloji"] == "arketimLlogariKlienti" || Request.QueryString["lloji"] == "arketimAbonent")
                PershtatKolonaPerFaturaNgaBRM(gridFaturat);
        }
        #endregion


    }
}