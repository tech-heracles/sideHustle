using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.UI.HtmlControls;
using DbCore;
using DbCore.DbAdmin;
using DbCore.DbKontabiliteti;
using DbCore.DbQendraKosto;
using DbCore.DbRegjistrim;
using DbCore.DbShare;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Extensions;
using DbCore.IMBUtils.Messages;
using DbCore.IMBUtils.Types;
using DevExpress.Web;
using NLog;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class Shto_VeprimeKF : MyPageBase
    {
        private ArrayList kflist = new ArrayList();
        private ArrayList kfListMeLlogariKunderpartiDheKurs = new ArrayList();

        private const string Komponente = "Shto_VeprimeKF.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!mySessionObjects.isLogedIn(Session))
                clsFunksione.logout(Session, true, "FaqePaautorizuar");

            if (mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + IdPerdoruesi);
                return;
            }

            if (mySessionObjects.merrPeriudheKontabel(Session) != null)
            {
                var periudha = mySessionObjects.merrPeriudheKontabel(Session);
                btnPeriudha.Text = periudha.NrPeriudha.ToString();
                lblPeriudhaAktuale.Text = string.Format("{0}-{1}", periudha.FillimiPeriudha.ToShortDateString(), periudha.MbarimiPeriudha.ToShortDateString());
            }
         

            if (!IsPostBack)
            {
                VendosHfMePerkthime();

                hfState.Set("idGjuha", IdGjuha);
                hfState.Set("idNdermarrje", IdNdermarrja);
                hfState.Set("idPerdoruesi", IdPerdoruesi);
                hfState.Set("idViti", IdViti);
                hfState.Set("guidString", GuidString);
                hfState.Set("idMonBazeNdermarrje", clsMonedha.ktheIdMonedhenENdermarrjes(IdNdermarrja));

                if (hfShtimModifikim.Value == "")
                {
                    if (string.IsNullOrEmpty(Request.QueryString["shtim_modifikim"]) || Request.QueryString["shtim_modifikim"] == "shtim")
                    {
                        hfShtimModifikim.Value = "shtim";
                        KonfiguroVleraFillestareShto();
                    }
                    else
                    {
                        hfShtimModifikim.Value = "modifikim";
                        KonfiguroVleraFillestareModifiko();
                    }
                }
                else if (hfShtimModifikim.Value == "shtim")
                {
                    KonfiguroVleraFillestareShto();
                    var gridFaturat = (ASPxGridView)ASPxNavBar1.Groups[0].FindControl("grid_faturat");
                    GridUtil.konfigGrideListeEMadhePaTheme(gridFaturat, "IdDokumenti");
                }
                else if (hfShtimModifikim.Value == "modifikim")
                    KonfiguroVleraFillestareModifiko();
                
                hfMonedhaNder.Value = clsMonedha.ktheMonedhenENdermarrjes(IdNdermarrja);

                var tedrejtaInfo = new clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();

                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, "Info KF");
                hfTeDrejtaInfoKF.Value = tedrejtaInfo.DAmb.ToString();

                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, "LupaKlientShpejte.aspx");
                hfTeDrejtaKFRi.Value = tedrejtaInfo.DShtim.ToString();

                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, Komponente);

                var perdoruesi = new clsPerdorues(IdPerdoruesi);
                hfHapurMbyllur.Value = perdoruesi.InfoHapur.ToString();
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                hfTeDrejta.Add("ShtimDraft", tedrejtaInfo.DShtimDraft);
                hfTeDrejta.Add("ModifikimDraft", tedrejtaInfo.DModifikimDraft);

                GridUtil.perktheButonaGride(hfState, ci);
            }
            KonfiguroGrideFaturat(false);
            bool meFatura = (clsAlternativaKushti.getAlternativa(Convert.ToInt32(cmbKonfigurimi.Value), "MF").ToLower() == "po");
            hfMeFatura.Value = meFatura.ToString();            
            if (meFatura)
            {
                InicializoGridFaturat(false);
               
            }

            AspxWebControlUtils.perkthePopUp(popFshi, rm.GetString("labelKujdes", ci), lblMsgbox, rm.GetString("labelAdministrimiMsgJeniSigurt", ci), ButtonCancel, rm.GetString("labelAnullo", ci));
            AspxWebControlUtils.perkthePopUp(popMesazhQK, rm.GetString("labelKujdes", ci), lblMsgbox4, rm.GetString("msgDeshironiTeBeniShperndarjenNeQendratEKostos", ci), ButtonCancelQK, rm.GetString("cmbboxItemFilterAvancJo", ci), ButtonOkQK, rm.GetString("cmbboxItemFilterAvancPo", ci));
            popupUniversal.HeaderText = rm.GetString("headerPopUpText", ci);
            Container1.Attributes["src"] = string.Empty;
            PercaktoTemplateMenu();
        }

        private void VendosHfMePerkthime()
        {
            ASPxNavBar1.Groups[0].Text = MessagesResource.Messages["msgFaturat"];
            hfState.Set("msgGabimGjateTransferimitTeTeDhenave", MessagesResource.Messages["msgGabimGjateTransferimitTeTeDhenave"]);
            hfState.Set("msgGabimKursiMonedha", MessagesResource.Messages["msgGabimKursiMonedha"]);
            hfState.Set("msgShtoVeprimKFKujdesKaKurseTeNdryshemPerTeNjejtenMonedhe", MessagesResource.Messages["msgShtoVeprimKFKujdesKaKurseTeNdryshemPerTeNjejtenMonedhe"]);
            hfState.Set("msgKursiNukMundTeJeteZero", MessagesResource.Messages["msgKursiNukMundTeJeteZero"]);
            hfState.Set("msgKursiRiNdryshonShumeMeKursinMePare", MessagesResource.Messages["msgKursiRiNdryshonShumeMeKursinMePare"]);
            hfState.Set("msgLupaKFEkzistonKyKFNeGride", MessagesResource.Messages["msgLupaKFEkzistonKyKFNeGride"]);
            hfState.Set("msgShtoVeprimKFNukEkzistonKjoLlogari", MessagesResource.Messages["msgShtoVeprimKFNukEkzistonKjoLlogari"]);
            hfState.Set("msgKlientFurnitoriNukEshteAktiv", MessagesResource.Messages["msgKlientFurnitoriNukEshteAktiv"]);
            hfState.Set("popupAdministrimiUniversal", MessagesResource.Messages["popupAdministrimiUniversal"]);
            hfState.Set("msgVleftaDuhetTeJeteNumer", MessagesResource.Messages["msgVleftaDuhetTeJeteNumer"]);
            hfState.Set("msgZgjidhKurs", MessagesResource.Messages["msgZgjidhKurs"]);
            hfState.Set("msgShtoKF", MessagesResource.Messages["msgShtoKF"]);
            hfState.Set("msgTrupiDokumentitNukDuhetLeneBosh", MessagesResource.Messages["msgTrupiDokumentitNukDuhetLeneBosh"]);
            hfState.Set("msgShtoVeprimKaRreshtaPaFaturaNeGride", MessagesResource.Messages["msgShtoVeprimKaRreshtaPaFaturaNeGride"]);
            hfState.Set("msgRreshtaTePavlefshemNeGride", MessagesResource.Messages["msgRreshtaTePavlefshemNeGride"]);
            hfState.Set("msgShperndarjeNeQendratEKostos", MessagesResource.Messages["msgShperndarjeNeQendratEKostos"]);
            hfState.Set("msgDoTeBeshShperndarjenNeQKostoTeDifTeKursit", MessagesResource.Messages["msgDoTeBeshShperndarjenNeQKostoTeDifTeKursit"]);
            hfState.Set("msgShtoVeprimDoniTeBeniShperdrjenNeQKSeDokTeVeprimeveKF", MessagesResource.Messages["msgShtoVeprimDoniTeBeniShperdrjenNeQKSeDokTeVeprimeveKF"]);
            hfState.Set("msgZgjidhniNjeDateRegjstrimi", MessagesResource.Messages["msgZgjidhniNjeDateRegjstrimi"]);
            hfState.Set("msgZgjidhniNjeDateDokumenti", MessagesResource.Messages["msgZgjidhniNjeDateDokumenti"]);
            hfState.Set("msgShtoVeprimShenoniLlogarineKundraparti", MessagesResource.Messages["msgShtoVeprimShenoniLlogarineKundraparti"]);
            hfState.Set("msgShenoniNumrinEDokumentit", MessagesResource.Messages["msgShenoniNumrinEDokumentit"]);
            hfState.Set("msgPoTransferohetTeDhenatShtypniPerseriRuaj", MessagesResource.Messages["msgPoTransferohetTeDhenatShtypniPerseriRuaj"]);
            hfState.Set("msgKursiDuhetNumer", MessagesResource.Messages["msgKursiDuhetNumer"]);
            hfState.Set("msgShtoVeprimNdryshimiNukDuhetTeKalojeVleftenEFatures", MessagesResource.Messages["msgShtoVeprimNdryshimiNukDuhetTeKalojeVleftenEFatures"]);
            hfState.Set("headerZgjidhKlientFurnitorin", MessagesResource.Messages["headerZgjidhKlientFurnitorin"]);
            hfState.Set("msgZgjidhDokumentin", MessagesResource.Messages["msgZgjidhDokumentin"]);
            hfState.Set("msgKujdesKursiKembimitNje", MessagesResource.Messages["msgKujdesKursiKembimitNje"]);
            hfState.Set("MenuKokeDokumenti", MessagesResource.Messages["MenuKokeDokumenti"]);
            hfState.Set("MenuTrupDokumenti", MessagesResource.Messages["MenuTrupDokumenti"]);
            hfState.Set("MenuFundDokumenti", MessagesResource.Messages["MenuFundDokumenti"]);
        }

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="ASPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        private void PercaktoTemplateMenu()
        {
            var menu = new colMenuItem(mySessionObjects.ktheGjuhe(Session));
            menu.merrMenuItemSipasKomponentesRegjistrime(mySessionObjects.ktheGjuhe(Session), Komponente, IdPerdoruesi, IdNdermarrja, IdViti, hfShtimModifikim.Value != "modifikim");

            var kok = new clsKokaFleteKontabel();
            foreach (var m in menu)
            {
                if (m.Name != "ItemFilter" && m.Name != "ItemFrame")
                {
                    clsToolbarConfig.ShtoMenuItem(Theme, ASPxMenu1, m);
                }

                if ((hfLidhur.Value == "True") && m.Name == "Draft")
                {
                    ASPxMenu1.Items[ASPxMenu1.Items.Count - 1].ClientVisible = false;
                    ruaj_draft.ClientEnabled = false;
                }
                if ((hfShtimModifikim.Value != "modifikim") && m.Name == "Fshi")
                    ASPxMenu1.Items[ASPxMenu1.Items.Count - 1].ClientVisible = false;
                if ((hfShtimModifikim.Value != "modifikim") && m.Name == "PrintPreview")
                    ASPxMenu1.Items[ASPxMenu1.Items.Count - 1].ClientVisible = false;
                if (m.Name == "FletaKontabel")
                {
                    if ((hfShtimModifikim.Value != "modifikim"))
                    {
                        ASPxMenu1.Items[ASPxMenu1.Items.Count - 1].ClientVisible = false;

                    }
                    else
                    {
                        kok = new clsKokaFleteKontabel(int.Parse(Request.QueryString["id"]), 20);

                        if (kok.NrDukumentiKokaFleteKontabel != null)
                            ASPxMenu1.Items[ASPxMenu1.Items.Count - 1].NavigateUrl = "javascript: myFaqeCelje.kontrolloTeDrejta('Shto_FleteKontabel.aspx?shtim_modifikim=modifikim&id=" + kok.IdKokaFleteKontabel + "&numur=" + kok.NrDukumentiKokaFleteKontabel + "')";
                        else
                        {
                            ASPxMenu1.Items[ASPxMenu1.Items.Count - 1].ClientVisible = false;
                        }
                    }

                }
                if (m.Name == "QendraKosto")
                {
                    if ((hfShtimModifikim.Value != "modifikim"))
                    {
                        ASPxMenu1.Items[ASPxMenu1.Items.Count - 1].ClientVisible = false;

                    }
                    else
                    {
                        if (kok.NrDukumentiKokaFleteKontabel != null)
                        {
                            var qend = new clsKokaQendraKosto();
                            qend.KtheKokaQKSipasIDGjeneruesDheKonfig(kok.IdKokaFleteKontabel, kok.IdKonfigAmbjente);


                            if (qend.NrDok != null)
                                ASPxMenu1.Items[ASPxMenu1.Items.Count - 1].NavigateUrl = "javascript: myButtonClickLupa.LupaUniversal_Click('" + rm.GetString("msgShperndarjeNeQendratEKostos", ci) + "','LupaRegjistrimQendraKosto.aspx?shtim_modifikim=modifikim&idDokGjenerues=" + kok.IdKokaFleteKontabel + "&idkonfig=" + kok.IdKonfigAmbjente + "',900,600)";
                            else
                                ASPxMenu1.Items[ASPxMenu1.Items.Count - 1].ClientVisible = false;
                        }
                        else
                            ASPxMenu1.Items[ASPxMenu1.Items.Count - 1].ClientVisible = false;
                    }
                }
                if (m.Name == "ItemFrame")
                    clsToolbarConfig.ShtoMenuItemPerFrame(this, ASPxMenu1, clsFunksione.ktheUrlHelpi(m.UrlHelp).Item1);
                if (m.Name == "Ruaj")
                    ASPxMenu1.Items[ASPxMenu1.Items.Count - 1].ClientVisible = true;
                if (m.Name == "Shto" || m.Name == "Kerko" || m.Name == "Pastro" || m.Name == "ItemFilter" || m.Name == "ItemFrame")
                    ASPxMenu1.Items[ASPxMenu1.Items.Count - 1].BeginGroup = true;
            }
            clsMenuInfo.ShtoMenuItemInfo(this, MenuInfo);

            this.ASPxMenu1.Items.FindByName("Draft").ClientVisible = clsFunksione.merrMenuVisibleDraft(hfShtimModifikim.Value != "modifikim", kok.IdStatusDokumenti);
            this.ASPxMenu1.Items.FindByName("PrintPreview").ClientVisible = hfShtimModifikim.Value == "modifikim";
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

        #region Konfigurime Fillestare

        /// <summary>
        /// Vendos vlerat default kur po behet shtim
        /// </summary>
        /// <param name="idGjuha"></param>
        /// <param name="idPerdoruesi"></param>
        private void KonfiguroVleraFillestareShto()
        {
            dteDtDok.Date = DateTime.Today;
            AspxWebControlUtils.vendosDateEditMask(dteDtDok);
            dteDtRegjistrimi.Date = DateTime.Today;
            VendosDataDefault();
            ConfigureAspxComboBox.ShtoKolonaPerLlogarine(cmbLlogariKunderParti);
            ConfigureAspxComboBox.ShtoKolonaPerKf(cmbKFKunderParti);
            AspxWebControlUtils.vendosDateEditMask(dteDtRegjistrimi);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmbLlogariKunderParti);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmbKFKunderParti);
            ConfigureAspxComboBox.KonfiguroComboBoxNivelesh(cmbLloji, 20, IdNdermarrja, IdPerdoruesi, false);
            ConfigureAspxComboBox.KonfiguroComboBoxKonfigurimeshSipasKategoriseDheNivelit(cmbKonfigurimi, cmbLloji.Value, 20, false, IdGjuha, IdNdermarrja, IdPerdoruesi);
            hfKonffillestar.Value = cmbKonfigurimi.SelectedItem.Text;
            cmbLloji.ClientSideEvents.Init = "function(s,e){TextChangedLloji();}";
            ConfigureAspxComboBox.percaktoTemplateComboJoListePaLupe(cmbDegeAdministrative);
            ConfigureAspxComboBox.ShtoKolonaKodiDhePershkrimi(cmbDegeAdministrative, "IdDegeAdministrative");
            ConfigureAspxComboBox.KonfiguroComboBoxDegeAdministrative(IdNdermarrja, cmbDegeAdministrative, false);

            var formatNrPerKonfig = new clsFormatiKonfig();
            formatNrPerKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(int.Parse(cmbKonfigurimi.Value.ToString()));
            if (formatNrPerKonfig.KonfigTrupi.Count == 0)
            {
                var trup1 = new clsFormatKonfigTrup(clsFormatKonfigTrup.defaultFormatSasia, clsFormatKonfigTrup.defaultFormatCmimi, clsFormatKonfigTrup.defaultFormatVlefta, clsFormatKonfigTrup.defaultFormatZbritja, clsFormatKonfigTrup.defaultFormatStringSasia, clsFormatKonfigTrup.defaultFormatStringCmimi, clsFormatKonfigTrup.defaultFormatStringVlefta, clsFormatKonfigTrup.defaultFormatStringZbritja);
                formatNrPerKonfig.KonfigTrupi.Add(trup1);
            }

            var serializusi = new JavaScriptSerializer();
            hfState.Set("formatMonedhe", serializusi.Serialize(formatNrPerKonfig));

            var mon = new colMonedhat();
            mon.mbushGjitheMonedhat(IdNdermarrja, IdPerdoruesi);
            hfState.Set("formatKurset", serializusi.Serialize(mon));
        }

        /// <summary>
        /// Vendos vlerat default kur po behet modifikim
        /// </summary>
        private void KonfiguroVleraFillestareModifiko()
        {
            ConfigureAspxComboBox.ShtoKolonaPerLlogarine(cmbLlogariKunderParti);
            ConfigureAspxComboBox.ShtoKolonaPerKf(cmbKFKunderParti);
            AspxWebControlUtils.vendosDateEditMask(dteDtDok);
            AspxWebControlUtils.vendosDateEditMask(dteDtRegjistrimi);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmbLlogariKunderParti);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmbKFKunderParti);
            ConfigureAspxComboBox.KonfiguroComboBoxNivelesh(cmbLloji, 20, IdNdermarrja, IdPerdoruesi, false);
            cmbLloji.ClientSideEvents.Init = "function(s,e){TextChangedLloji();}";

            ConfigureAspxComboBox.percaktoTemplateComboJoListePaLupe(cmbDegeAdministrative);
            ConfigureAspxComboBox.ShtoKolonaKodiDhePershkrimi(cmbDegeAdministrative, "IdDegeAdministrative");
            ConfigureAspxComboBox.KonfiguroComboBoxDegeAdministrative(IdNdermarrja, cmbDegeAdministrative, true);
            cmbDegeAdministrative.Items.RemoveAt(0);

            MerrTedhenat();
            MbushListeVeprimeKfTrupiModifiko();
        }

        public void MerrTedhenat()
        {
            var koka = new clsVeprimeKFKoka(int.Parse(Request.QueryString["id"]));

            cmbLloji.Text = clsNivelRegjistrimi.ktheKodNivelRegjistrimi(koka.IdNivel);
            ConfigureAspxComboBox.KonfiguroComboBoxKonfigurimeshSipasKategoriseDheNivelit(cmbKonfigurimi, cmbLloji.Value, 20, true, IdGjuha, IdNdermarrja, IdPerdoruesi);
            hfKonffillestar.Value = cmbKonfigurimi.SelectedItem.Text;

            var konf = new clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasId(koka.IdKonfigAmbjente, IdGjuha);
            cmbKonfigurimi.Text = konf.KodKonfigAmbjente;
            int idKonfigurim = Convert.ToInt32(cmbKonfigurimi.Value);
            cbPrinto.Checked = clsAtributeTrupi.merrVleredefaultSipasKontrollitDheKonfigurimit(idKonfigurim, "cbPrinto", 653) == "true" ? true : false;
            hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;
            txtNrDok.Text = koka.NrDok;
            dteDtDok.Date = koka.DtDok;
            dteDtRegjistrimi.Date = koka.DtRegj;
            txtPershkrimi.Text = koka.Pershkrimi;
            if (koka.IdDegeAdministrative != 0)
            {
                cmbDegeAdministrative.Value = koka.IdDegeAdministrative.ToString();
            }

            cmbLlogariKunderParti.Text = new clsLlogari(koka.IdLlogKunderParti).NrLlogari;
            cmbKFKunderParti.Text = new clsKlientFurnitor(koka.IdKfKunderParti).KodKlientFurnitor;
            koka.ColVeprimeKFTrupi = new colVeprimeKFTrupi();
            koka.ColVeprimeKFTrupi.MbushVeprimeKfTrupi(koka.IdVeprimeKFKoka);

            txtVlefta.Text = (koka.Vlefta * koka.ColVeprimeKFTrupi[0].Kursi).ToString();

            bool result;
            if (bool.TryParse(hfMeFatura.Value, out result))
            {
                InicializoGridMod(koka.IdKf);
                KonfiguroGrideFaturat(true);
            }

            var dbAdmin = new clsDatabaseAdmin();
            var dtlidhur = dbAdmin.MerrDokLidhur(koka.IdVeprimeKFKoka, koka.IdNivel, "T_VEPRIMEKFKOKA", "IDVEPRIMKFKOKA");
            hfLidhur.Value = (dtlidhur.Rows.Count != 0).ToString();

            var autorizimet = clsVeprimeKFKoka.KaAutorizime(koka.IdVeprimeKFKoka, IdPerdoruesi);
            hfAutorizimi.Value = autorizimet.ToString();
            if (!autorizimet)
                hfLidhur.Value = "True";

            var serializusi = new JavaScriptSerializer();

            var formatNrPerKonfig = new clsFormatiKonfig();
            formatNrPerKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(int.Parse(cmbKonfigurimi.Value.ToString()));
            if (formatNrPerKonfig.KonfigTrupi.Count == 0)
            {
                var trup1 = new clsFormatKonfigTrup(clsFormatKonfigTrup.defaultFormatSasia, clsFormatKonfigTrup.defaultFormatCmimi, clsFormatKonfigTrup.defaultFormatVlefta, clsFormatKonfigTrup.defaultFormatZbritja, clsFormatKonfigTrup.defaultFormatStringSasia, clsFormatKonfigTrup.defaultFormatStringCmimi, clsFormatKonfigTrup.defaultFormatStringVlefta, clsFormatKonfigTrup.defaultFormatStringZbritja);
                formatNrPerKonfig.KonfigTrupi.Add(trup1);
            }
            hfState.Set("formatMonedhe", serializusi.Serialize(formatNrPerKonfig));

            var mon = new colMonedhat();
            mon.mbushGjitheMonedhat(IdNdermarrja, IdPerdoruesi);
            hfState.Set("formatKurset", serializusi.Serialize(mon));

            AspxWebControlUtils.ShtoLidhje(IdPerdoruesi, IdViti, IdNdermarrja, hl, dtlidhur, koka.IdGjenerues, koka.IdNivelGjenerues, koka.IdKonfigGjenerues, IdGjuha);
            dbAdmin.Dispose();
        }

        private void VendosDataDefault()
        {
            var sot = DateTime.Today;
            var periudha = mySessionObjects.merrPeriudheKontabel(Session);
            if (sot >= periudha.FillimiPeriudha && sot <= periudha.MbarimiPeriudha)
                dteDtDok.Value = DateTime.Today;
            else
                dteDtDok.Value = periudha.FillimiPeriudha;
        }

        /// <summary>
        /// Merr trupin ekzistues te veprime kf qe po modifikohet
        /// </summary>
        private void MbushListeVeprimeKfTrupiModifiko()
        {
            var trupa = new colVeprimeKFTrupi();
            trupa.MbushVeprimeKfTrupi(int.Parse(Request.QueryString["id"]));
            MbushHiddenFieldet(trupa);
        }

        #endregion

        private void MbushHiddenFieldet(colVeprimeKFTrupi col)
        {
            var serializusi = new JavaScriptSerializer { MaxJsonLength = 50000000 };
            HfColTrupBanka.Value = serializusi.Serialize(col);
            HfColKF.Value = serializusi.Serialize(col.KtheColKf());
            HfColLlog.Value = serializusi.Serialize(col.KtheColLLogari());
            HfColKfKundra.Value = serializusi.Serialize(col.KtheColKfKundra());
            HfColFatShitje.Value = serializusi.Serialize(col.KtheColShitje(IdNdermarrja)); //TOCHECK Nestila -- hoqa trupin shitjeve
            hfMonedha.Value = serializusi.Serialize(col.KtheColMonedha());
            hfId.Value = serializusi.Serialize(col.KtheIdFature());
            hfNivele.Value = serializusi.Serialize(col.KtheNivele());
        }

        #region Klikime menuje

        /// <summary>
        /// Percakton veprimin qe kryhet kur klikohet nje nga butonat e menuse
        /// </summary>
        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {
            switch (e.Item.Name)
            {
                case "Ruaj":
                    Page.Validate();
                    RuajDokumentaVeprimeKf(1, cbPrinto.Checked);
                    MbushHiddenFieldet(new colVeprimeKFTrupi());
                    break;
                case "Draft":
                    Page.Validate();
                    RuajDokumentaVeprimeKf(0, cbPrinto.Checked);// 0 = statusi i dokumentit(ne kete rast statusi eshte draft)
                    MbushHiddenFieldet(new colVeprimeKFTrupi());
                    break;
                case "PrintPreview":
                    var id = int.Parse(Request.QueryString["id"]);
                    var design = new clsRaportDesign();
                    design.merrSipasNdermarjedheRaport(IdNdermarrja, "FormatPrintimiVeprimeKF");
                    Container1.Attributes["src"] = "RaportiShpejte.aspx?Sesioni=false&idDokumenti=" + id + "&emriReal=FormatPrintimiVeprimeKF&printo=false&raportdyte=jo&iddesign=" + design.IdRaportDesign;
                    break;
            }
        }

        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            pergjigja.Text = "";
            var veprimeKfKoka = new clsVeprimeKFKoka(int.Parse(Request.QueryString["id"]));
            var dbAdmin = new clsDatabaseAdmin();
            var lidhur = dbAdmin.eshteDokumentiILidhur(veprimeKfKoka.IdVeprimeKFKoka, veprimeKfKoka.IdNivel, "T_VEPRIMEKFKOKA", "IDVEPRIMKFKOKA");
            veprimeKfKoka.IdPerdoruesi = IdPerdoruesi;
            if (lidhur)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgListPagesaDokuEshteILidhurDheNukMundTeFshihet", ci), pnlMesazhi);
                return;
            }

            if (veprimeKfKoka.DtDok.Year != new clsNdermarrjeViti(mySessionObjects.ktheNdermarrjeVit(Session)).Viti)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgDataNukPerketVititUshtrimor", ci), pnlMesazhi);
                return;
            }

            var ekycur = clsPeriudhaKontabel.eshteKycurPeriudheSipasDateDheNdermarrjes(veprimeKfKoka.DtDok, IdNdermarrja);
            if (ekycur)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgPeriudhaEKycur", ci), pnlMesazhi);
                return;
            }
            
            if (DbCore.MbylljePeriudhe.PeriodClosing.IsPeriodClosed(veprimeKfKoka.DtDok, MyConnectionsManager.GetSelectedConNameServer(), IdNdermarrja, KategoriDokumenti.VeprimeKlientFurnitor, veprimeKfKoka.IdKonfigAmbjente))
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgPeriodIsClosed"], pnlMesazhi);
                return;
            }

            var mesazhi = veprimeKfKoka.Fshi();
            dbAdmin.Dispose();
            if (mesazhi.Status)
            {
                Response.Redirect("VeprimeKF.aspx?fshi=po");
                return;
            }
            else
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhi.PershkrimMesazhi, pnlMesazhi);
        }

        /// <summary>
        /// Therritet kur klikohet butoni Ruaj per te ruajtur nje dokument veprimekf te forme te rregullt (jo draft)
        /// Therret funksionin <see cref="RuajDokumentaVeprimeKf"/>
        /// </summary>
        protected void ruaj_Button_Click(object sender, EventArgs e)
        {
            Page.Validate();
            RuajDokumentaVeprimeKf(1, cbPrinto.Checked);
            MbushHiddenFieldet(new colVeprimeKFTrupi());
        }

        /// <summary>
        /// Therritet kur klikohet butoni Ruaj si draft per te ruajtur nje dokument veprimekf si draft
        /// Therret funksionin <see cref="RuajDokumentaVeprimeKf"/>
        /// </summary>
        protected void ruaj_draft_Click(object sender, EventArgs e)
        {
            Page.Validate();
            RuajDokumentaVeprimeKf(0, cbPrinto.Checked);// 0 = statusi i dokumentit(ne kete rast statusi eshte draft)
            MbushHiddenFieldet(new colVeprimeKFTrupi());
        }

        /// <summary>
        /// Therritet kur klikohet butoni Anullo. Dergon perdoruesin te lista e dokumentave.
        /// </summary>
        protected void anullo_Button_Click(object sender, EventArgs e)
        {
            Response.Redirect("VeprimeKF.aspx");
            return;
        }

        #endregion
     
        /// <summary>
        /// Ruan/Modifikon objekte clsVeprimeKFKoka.
        /// </summary>
        /// <param name="statusDokumenti">Statusi me te cilin po ruhet dokumenti (I rregullt apo draft)</param>
        /// <param name="printo"></param>
        private void RuajDokumentaVeprimeKf(int statusDokumenti, bool printo)
        {
            if (Page.IsValid == false)
                return;

            clsMesazh mesazh = new clsMesazh();
            colVeprimeKFKoka colVeprimeKfKoka = new colVeprimeKFKoka();
            clsPeriudhaKontabel periudha = null;
            bool isShtim = false;
            bool meKontabilizim = statusDokumenti == 1 && (hfKontabilizimi.Value == "1" || hfKontabilizimi.Value == "2");
            string shfaqmesazhapolupe = "jo";
            string shfaqmesazhapolupeVdk = "jo";

            if (hfShtimModifikim.Value == "shtim")
            {
                periudha = mySessionObjects.merrPeriudheKontabel(Session);
                isShtim = true;
            }

            if (!IsValidVeprimeKf())
            {
                status1.Value = "false";
                return;
            }

            try
            {
                clsKonfigurimAmbjenti clsKonf = new clsKonfigurimAmbjenti();
                if (cmbKonfigurimi.Text != "")
                    clsKonf.mbushKonfigAmbjSipasKod(cmbKonfigurimi.Text, IdNdermarrja);
                else
                    clsKonf.mbushKonfigDefaultKomponentes(653, IdNdermarrja);

                colVeprimeKFTrupi colVeprimeKfTrupi = KrijoObjekteTrupiVeprimeKf();
                if (Convert.ToBoolean(hfMeFatura.Value) == false)
                {
                    if (hfMeKF.Value != "true")
                    {
                        foreach (clsVeprimeKFTrupi veprimeKfTrupi in colVeprimeKfTrupi)
                        {
                            if (!isShtim)
                                periudha = new clsPeriudhaKontabel(veprimeKfTrupi.Data, IdNdermarrja);

                            string mesazhGabimi;
                            if (!clsFunksione.checkPeriudheKontabel(out mesazhGabimi, veprimeKfTrupi.Data.Date, periudha, statusDokumenti))
                            {
                                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhGabimi, pnlMesazhi);
                                return;
                            }
                            colVeprimeKfKoka.Add(KrijoObjektKokeVeprimeKf(statusDokumenti, veprimeKfTrupi, IdNdermarrja, periudha.IdPeriudha, meKontabilizim, out shfaqmesazhapolupe, out shfaqmesazhapolupeVdk, clsKonf));
                        }
                    }
                    else
                    {
                        for (int f = 0; f < kfListMeLlogariKunderpartiDheKurs.Count; f++)//krijojme nga nje dokument per cdo dyshe klient-klient kunderparti te ndryshem
                        {
                            if (!isShtim)
                                periudha = new clsPeriudhaKontabel(dteDtDok.Date, IdNdermarrja);

                            string mesazhGabimi;
                            if (!clsFunksione.checkPeriudheKontabel(out mesazhGabimi, dteDtDok.Date.Date, periudha, statusDokumenti))
                            {
                                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhGabimi, pnlMesazhi);
                                return;
                            }

                            colVeprimeKFTrupi colVeprimeKfTrupiNew = new colVeprimeKFTrupi();
                            foreach (clsVeprimeKFTrupi veprimeKfTrupi in colVeprimeKfTrupi)
                            {
                                string[] kf = kfListMeLlogariKunderpartiDheKurs[f].ToString().Split(';');
                                if (veprimeKfTrupi.IdKF == Convert.ToInt32(kf[0]) && veprimeKfTrupi.IdKfKunderParti == Convert.ToInt32(kf[1]) && veprimeKfTrupi.Kursi == Convert.ToDouble(kf[2]))
                                    colVeprimeKfTrupiNew.Add(veprimeKfTrupi);
                            }

                            colVeprimeKfKoka.Add(KrijoObjektKokeVeprimeKfGjendje(statusDokumenti, colVeprimeKfTrupiNew, periudha.IdPeriudha, meKontabilizim, out shfaqmesazhapolupe, out shfaqmesazhapolupeVdk, 3, colVeprimeKfTrupiNew[0].IdKfKunderParti, clsKonf));
                        }

                    }
                }
                else
                {
                    for (int f = 0; f < kflist.Count; f++)
                    {
                        if (!isShtim)
                            periudha = new clsPeriudhaKontabel(dteDtDok.Date, IdNdermarrja);

                        string mesazhGabimi;
                        if (!clsFunksione.checkPeriudheKontabel(out mesazhGabimi, dteDtDok.Date.Date, periudha, statusDokumenti))
                        {
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhGabimi, pnlMesazhi);
                            return;
                        }

                        colVeprimeKFTrupi colVeprimeKfTrupiNew = new colVeprimeKFTrupi();
                        foreach (var veprimeKfTrupi in colVeprimeKfTrupi)
                        {
                            string[] kf = kflist[f].ToString().Split(';');
                            if (veprimeKfTrupi.IdKF == Convert.ToInt32(kf[0]) && veprimeKfTrupi.Kursi == Convert.ToDouble(kf[1]))
                                colVeprimeKfTrupiNew.Add(veprimeKfTrupi);
                        }

                        colVeprimeKfKoka.Add(KrijoObjektKokeVeprimeKfGjendje(statusDokumenti, colVeprimeKfTrupiNew, periudha.IdPeriudha, meKontabilizim, out shfaqmesazhapolupe, out shfaqmesazhapolupeVdk, 2, 0, clsKonf));
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.GetCurrentClassLogger().Error(ex.Message);
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, ex.Message, pnlMesazhi);
                status1.Value = "false";
                return;
            }

            RuajVeprimeKFNeDB(colVeprimeKfKoka, statusDokumenti, printo, shfaqmesazhapolupe, shfaqmesazhapolupeVdk);
        }

        /// <summary>
        /// Kontrollon nese jane plotesuar te gjitha fushat e detyrueshme te kokes se dokumentit
        /// </summary>
        private bool IsValidVeprimeKf()
        {
            if (dteDtRegjistrimi.Text == "")
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgListPagesaZgjidhniNjeDateRegjistrimi"], pnlMesazhi);
                return false;
            }

            if (dteDtDok.Text == "")
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgLidhjaDokZgjidh1DateDokumenti"], pnlMesazhi);
                return false;
            }

            if (dteDtDok.Date.Year != new clsNdermarrjeViti(IdNdermarrjeVit).Viti)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgDataNukPerketVititUshtrimor"], pnlMesazhi);
                return false;
            }

            //Kontrollon nese ekzistojne ose kodet e klient/furnitoreve te vendosura ne trupin e veprimeve.
            var kodetKf = hfKodi.Value;
            char[] delimiter = { '|', '|' };
            var kodetRreshtat = kodetKf.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
            var kodetJoEkzistuese = new string[kodetRreshtat.Length];
            if (kodetKf != "")
            {
                int j = 0;
                bool kaGabim = false;
                foreach (var kod in kodetRreshtat)
                {
                    var pars4 = kod.Split(':');
                    var kodi = pars4[1];
                    if (!clsKlientFurnitor.EkzistonKlientFurnitor(kodi, IdNdermarrja))
                    {
                        kaGabim = true;
                        kodetJoEkzistuese[j++] = kodi;
                    }
                }
                if (kaGabim)
                {
                    var kodetStr = string.Join(", ", kodetJoEkzistuese, 0, j);
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgShtoVeprimKFNukEkzistonKFMeKod"] + kodetStr + "!", pnlMesazhi);
                    return false;
                }
            }

            if (cmbLlogariKunderParti.Text != "")
            {
                if (!clsLlogari.ekzistonLlogari(cmbLlogariKunderParti.Text, IdNdermarrja))
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgShtoVeprimKFNukEkzistonKjoLlogari"], pnlMesazhi);
                    return false;
                }

                if (!clsLlogari.eshteLlogariAktive(cmbLlogariKunderParti.Text, IdNdermarrja))
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgKjoLlogariNukEshteAktive"], pnlMesazhi);
                    return false;
                }

                return true;
            }

            if (cmbKFKunderParti.Text != "")
            {
                if (!clsKlientFurnitor.EkzistonKlientFurnitor(cmbKFKunderParti.Text.Split('(')[0], IdNdermarrja))
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgKFnukEkziston"], pnlMesazhi);
                    return false;
                }

                var kf = new clsKlientFurnitor();
                kf.mbushKlientFurnitorSipasKodit(cmbKFKunderParti.Text.Split('(')[0], IdNdermarrja);
                if (!kf.AktivKF)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgKlientFurnitoriNukEshteAktiv"], pnlMesazhi);
                    return false;
                }

                return true;
            }

            return true;
        }

        /// <summary>
        /// Krijon objekte te tipit colVeprimeKFTrupi
        /// </summary>
        /// <returns>Kthen nje collection me objekte te tipit DbCore.DbRegjistrim.colVeprimeKFTrupi</returns>
        private colVeprimeKFTrupi KrijoObjekteTrupiVeprimeKf()
        {
            JavaScriptSerializer serializusi = new JavaScriptSerializer();
            var dokumenti = (object[])serializusi.DeserializeObject(gridDataObject.Value);
            var nivele = (object[])serializusi.DeserializeObject(hfNivele.Value);
            var id = (object[])serializusi.DeserializeObject(hfId.Value);

            colVeprimeKFTrupi trupat = new colVeprimeKFTrupi();
            clsMonedha mon = new clsMonedha();
            mon.mbushMonedhenENdermarrjes(IdNdermarrja);

            for (int i = 0; i < dokumenti.Length; i++)
            {
                try
                {
                    clsVeprimeKFTrupi trupVepKf = new clsVeprimeKFTrupi((Dictionary<string, object>)dokumenti[i], nivele[i], IdNdermarrja, id[i], IdPerdoruesi, hfMeKF.Value == "true", mon.PershkrimiMonedha);
                    if (trupVepKf.IdLlogKunderParti == -1)
                    {
                        throw new MyException(rm.GetString("msgShtoVeprimeKF1NgaLlogarteNukEkziston", ci));
                    }
                    if (trupVepKf.IdKF != -1)
                    {
                        bool ekziston = false;
                        for (int k = 0; k < kflist.Count; k++)
                        {
                            var kf = kflist[k].ToString().Split(';');
                            if (trupVepKf.IdKF == Convert.ToInt32(kf[0]) && trupVepKf.Kursi == Convert.ToDouble(kf[1]))
                            {
                                ekziston = true;
                                break;
                            }
                        }
                        if (!ekziston)
                            kflist.Add(trupVepKf.IdKF + ";" + trupVepKf.Kursi);

                        if (hfMeKF.Value == "true")
                        {
                            bool ekzistonKFmeKF = false;
                            for (int k = 0; k < kfListMeLlogariKunderpartiDheKurs.Count; k++)
                            {
                                var kf = kfListMeLlogariKunderpartiDheKurs[k].ToString().Split(';');

                                if (trupVepKf.IdKF == Convert.ToInt32(kf[0]) && trupVepKf.IdKfKunderParti == Convert.ToInt32(kf[1]) && trupVepKf.Kursi == Convert.ToDouble(kf[2]))
                                {
                                    ekzistonKFmeKF = true;
                                    break;
                                }
                            }
                            if (!ekzistonKFmeKF)
                                kfListMeLlogariKunderpartiDheKurs.Add(trupVepKf.IdKF + ";" + trupVepKf.IdKfKunderParti + ";" + trupVepKf.Kursi);

                        }
                        clsKurset kursiifundit = new clsKurset(trupVepKf.IdMonedha, trupVepKf.Data);
                        double diferenca = Math.Abs(kursiifundit.VleraKursi - trupVepKf.Kursi);

                        if (diferenca / kursiifundit.VleraKursi > 0.2)
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgShtoVeprimKFKursiIRiNdryshonMeKursinEMeparshem", ci), pnlMesazhi);

                        trupat.Add(trupVepKf);
                    }
                }
                catch (Exception ex)
                {
                    LogManager.GetCurrentClassLogger().Error(ex.Message);
                    throw new Exception(ex.Message);
                }
            }
            return trupat;
        }

        /// <summary>
        /// Krijon nje objekt te tipit DbCore.DbRegjistrim.clsVeprimeKFKoka
        /// </summary>
        /// <param name="statusDokumenti">Statusi i dokumentit (I rregullt ose draft)</param>
        /// <param name="trupi">trupi</param>
        /// <returns>Kthen nje objekt te tipit DbCore.DbRegjistrim.clsVeprimeKFKoka</returns>
        private clsVeprimeKFKoka KrijoObjektKokeVeprimeKf(int statusDokumenti, clsVeprimeKFTrupi trupi, int indermarje, int idperiudha, bool mekontabilizim, out string shfaqmesazhapolupe, out string shfaqmesazhapolupeVdk, clsKonfigurimAmbjenti clsKonf)
        {
            if (DbCore.MbylljePeriudhe.PeriodClosing.IsPeriodClosed(trupi.Data.Date, MyConnectionsManager.GetSelectedConNameServer(), IdNdermarrja, KategoriDokumenti.Magazina, clsKonf.IdKonfigAmbjente))
                throw new Exception(MessagesResource.Messages["msgPeriodIsClosed"]);

            clsVeprimeKFKoka veprimeKfKoka = new clsVeprimeKFKoka();

            var controls = this.GetAsPxTextEditIdValue();
            controls.AddRange(ASPxSplitter1.GetAsPxTextEditIdValue());

            hfNrAuto = (ASPxHiddenField)NrAuto.VendosVleratNrAuto(hfNrAuto, controls);
            hfNrAutoShitje = (ASPxHiddenField)NrAuto.ShtoNeRegjistrime(hfNrAutoShitje, hfNrAuto, "txtNrDok", "NrDok");



            clsKonfigurimAmbjenti konflidhes = new clsKonfigurimAmbjenti();
            konflidhes.mbushKonfigAmbjSipasId(clsKonf.IdKonfigurimi, IdGjuha);

            colVeprimeKFTrupi col = new colVeprimeKFTrupi { trupi };

            clsKokaQendraKosto qend = new clsKokaQendraKosto();
            int id = 0;
            if (hfShtimModifikim.Value == "modifikim")
            {
                clsKokaFleteKontabel kok = new clsKokaFleteKontabel(int.Parse(Request.QueryString["id"]), 20);
                qend.KtheKokaQKSipasIDGjeneruesDheKonfigMeTrup(kok.IdKokaFleteKontabel, kok.IdKonfigAmbjente);
                id = int.Parse(Request.QueryString["id"]);
            }

            int iddege = 0;
            if (cmbDegeAdministrative.Text != "")
                iddege = int.Parse(cmbDegeAdministrative.Value.ToString());

            int idllog = 0;
            if (cmbLlogariKunderParti.Text != "")
                idllog = clsLlogari.mbushIDLlogariSipasKodit(cmbLlogariKunderParti.Text, indermarje);

            int idkf = 0;
            if (cmbKFKunderParti.Text != "")
                idkf = clsKlientFurnitor.MerrIdKlientFurnitor(cmbKFKunderParti.Text, indermarje);

            var mesazh = veprimeKfKoka.KrijoVeprimeKf(new DbData(), 1, txtNrDok.Text, trupi.Data, dteDtRegjistrimi.Date, trupi.IdKF, trupi.KodKF, idllog, cmbLlogariKunderParti.Text, trupi.Pershkrimi, trupi.IdMonedha, trupi.KodMonedha, trupi.Vlefta, indermarje, IdNdermarrjeVit, statusDokumenti, int.Parse(cmbLloji.Value.ToString()), clsKonf.IdKonfigAmbjente, 0, 0, 0, 0, IdPerdoruesi, idkf, col, idperiudha, mekontabilizim, clsKonf.KodKonfigAmbjente, Convert.ToBoolean(hfMeFatura.Value), konflidhes, out shfaqmesazhapolupe, out shfaqmesazhapolupeVdk, qend.ColTrupi, id, IdGjuha, rm, ci, iddege);
            if (!mesazh.Status)
                throw new Exception(mesazh.PershkrimMesazhi);

            return veprimeKfKoka;
        }

        /// <summary>
        /// Krijon nje objekt te tipit DbCore.DbRegjistrim.clsVeprimeKFKoka
        /// </summary>
        /// <param name="statusDokumenti">Statusi i dokumentit (I rregullt ose draft)</param>
        /// <param name="trupi">trupi</param>
        /// <returns>Kthen nje objekt te tipit DbCore.DbRegjistrim.clsVeprimeKFKoka</returns>
        private clsVeprimeKFKoka KrijoObjektKokeVeprimeKfGjendje(int statusDokumenti, colVeprimeKFTrupi trupi, int idperiudha, bool mekontabilizim, out string shfaqmesazhapolupe, out string shfaqmesazhapolupeVdk, int llojveprimi, int idkfkunderparti, clsKonfigurimAmbjenti clsKonf)
        {

            if (DbCore.MbylljePeriudhe.PeriodClosing.IsPeriodClosed(dteDtDok.Date.Date, MyConnectionsManager.GetSelectedConNameServer(), IdNdermarrja, KategoriDokumenti.VeprimeKlientFurnitor, clsKonf.IdKonfigAmbjente))
                throw new Exception(MessagesResource.Messages["msgPeriodIsClosed"]);

            clsVeprimeKFKoka veproKfKoka = new clsVeprimeKFKoka();

            var controls = this.GetAsPxTextEditIdValue();
            controls.AddRange(ASPxSplitter1.GetAsPxTextEditIdValue());

            hfNrAuto = (ASPxHiddenField)NrAuto.VendosVleratNrAuto(hfNrAuto, controls);
            hfNrAutoShitje = (ASPxHiddenField)NrAuto.ShtoNeRegjistrime(hfNrAutoShitje, hfNrAuto, "txtNrDok", "NrDok");


            int iddege = 0;
            if (cmbDegeAdministrative.Text != "")
                iddege = int.Parse(cmbDegeAdministrative.Value.ToString());

            clsKonfigurimAmbjenti konflidhes = new clsKonfigurimAmbjenti();
            konflidhes.mbushKonfigAmbjSipasId(clsKonf.IdKonfigurimi, IdGjuha);

            double shuma = trupi.Sum(t => t.Vlefta);

            int idllog = 0;
            if (cmbLlogariKunderParti.Text != "")
                idllog = clsLlogari.mbushIDLlogariSipasKodit(cmbLlogariKunderParti.Text, IdNdermarrja);

            clsKokaQendraKosto qend = new clsKokaQendraKosto();
            int id = 0;
            if (hfShtimModifikim.Value == "modifikim")
            {
                clsKokaFleteKontabel kok = new clsKokaFleteKontabel(int.Parse(Request.QueryString["id"]), 20);
                qend.KtheKokaQKSipasIDGjeneruesDheKonfigMeTrup(kok.IdKokaFleteKontabel, kok.IdKonfigAmbjente);
                id = int.Parse(Request.QueryString["id"]);
            }

            clsMesazh mesazh = veproKfKoka.KrijoVeprimeKf(new DbData(), llojveprimi, txtNrDok.Text, dteDtDok.Date, dteDtRegjistrimi.Date, trupi[0].IdKF, trupi[0].KodKF, idllog, cmbLlogariKunderParti.Text, txtPershkrimi.Text, trupi[0].IdMonedha, trupi[0].KodMonedha, shuma, IdNdermarrja, IdNdermarrjeVit, statusDokumenti, int.Parse(cmbLloji.Value.ToString()), clsKonf.IdKonfigAmbjente, 0, 0, 0, 0, IdPerdoruesi, idkfkunderparti, trupi, idperiudha, mekontabilizim, clsKonf.KodKonfigAmbjente, Convert.ToBoolean(hfMeFatura.Value), konflidhes, out shfaqmesazhapolupe, out shfaqmesazhapolupeVdk, qend.ColTrupi, id, IdGjuha, rm, ci, iddege);
            if (!mesazh.Status)
                throw new Exception(mesazh.PershkrimMesazhi);

            return veproKfKoka;
        }

        public void RuajVeprimeKFNeDB(colVeprimeKFKoka colVeprimeKfKoka, int statusDokumenti, bool printo, string shfaqmesazhapolupe, string shfaqmesazhapolupeVdk)
        {
            clsMesazh mesazh = new clsMesazh();
            clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin();

            clsTeDrejtaRoli tedrejtaInfo = new clsTeDrejtaRoli();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdNdermarrjeVit, Komponente);

            if ((hfShtimModifikim.Value == "shtim" && ((statusDokumenti == 1 && !tedrejtaInfo.DShtim) || (statusDokumenti == 0 && !tedrejtaInfo.DShtimDraft))) || (hfShtimModifikim.Value == "modifikim" && ((statusDokumenti == 1 && !tedrejtaInfo.DMod) || (statusDokumenti == 0 && !tedrejtaInfo.DModifikimDraft))))
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNukKeniTeDrejta", ci), pnlMesazhi);
                status1.Value = "false";
                return;
            }

            clsVeprimeKFKoka veprimeKfKoka = new clsVeprimeKFKoka();
            if (hfShtimModifikim.Value == "shtim")
            {
                (clsMesazh m, clsVeprimeKFKoka dokFundit) = colVeprimeKfKoka.RuajVeprimeKF(hfNrAutoShitje);
                mesazh = m;
                veprimeKfKoka = dokFundit;
            }
            else if (hfShtimModifikim.Value == "modifikim")
            {
                foreach (clsVeprimeKFKoka v in colVeprimeKfKoka)
                {
                    v.IdVeprimeKFKoka = int.Parse(Request.QueryString["id"]);
                    bool lidhur = dbAdmin.eshteDokumentiILidhur(v.IdVeprimeKFKoka, v.IdNivel, "T_VEPRIMEKFKOKA", "IDVEPRIMKFKOKA");
                    if (lidhur.ToString() != hfLidhur.Value)
                    {
                        mesazh.Status = false;
                        mesazh.PershkrimMesazhi = rm.GetString("msgDokumentiEshteILidhur", ci);
                        status1.Value = "false";
                    }
                    else
                        mesazh = v.Modifiko(lidhur);
                    veprimeKfKoka = v;
                }
            }
            KtheMesazhe(mesazh, shfaqmesazhapolupe, shfaqmesazhapolupeVdk, printo, veprimeKfKoka.IdVeprimeKFKoka, veprimeKfKoka.ColDokumentalidhes);
            dbAdmin.Dispose();
        }

        public void KtheMesazhe(clsMesazh mesazh, string shfaqmesazhapolupe, string shfaqmesazhapolupeVdk, bool printo, int idVeprimeKFKoka, colDokumentLidhesKoka colDokumentalidhes)
        {
            if (mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("msgRuajtjeMeSukses", ci), pnlMesazhi);
                hfqkmesazhi.Value = shfaqmesazhapolupe;
                hfqkmesazhiVDK.Value = shfaqmesazhapolupeVdk;
                if (shfaqmesazhapolupe != "jo")
                {
                    clsKokaFleteKontabel kok = new clsKokaFleteKontabel(idVeprimeKFKoka, 20);
                    hfUrl.Value = "LupaRegjistrimQendraKosto.aspx?shtim_modifikim=modifikim&idDokGjenerues=" + kok.IdKokaFleteKontabel + "&idkonfig=" + kok.IdKonfigAmbjente;
                }

                if (shfaqmesazhapolupeVdk != "jo")
                {
                    foreach (clsDokumentLidhesKoka dokumentLidhesKoka in colDokumentalidhes)
                    {
                        clsKokaFleteKontabel kok = new clsKokaFleteKontabel(dokumentLidhesKoka.IdKoka, 10);
                        hfUrlVDK.Value += "LupaRegjistrimQendraKosto.aspx?shtim_modifikim=modifikim&idDokGjenerues=" + kok.IdKokaFleteKontabel + "&idkonfig=" + kok.IdKonfigAmbjente + ";";
                    }
                }

                status1.Value = "true";
                hl = new HtmlTable();
                pnlLidhur.Update();
                hfShtimModifikim.Value = "shtim";
                
                if (Convert.ToBoolean(hfMeFatura.Value))
                {
                    InicializoGridFaturat(true);
                    KonfiguroGrideFaturat(false);
                }
            }
            else
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                status1.Value = "false";
            }
            if (printo)
            {
                clsRaportDesign design = new clsRaportDesign();
                design.merrSipasNdermarjedheRaport(IdNdermarrja, "FormatPrintimiVeprimeKF");
                Container1.Attributes["src"] = "RaportiShpejte.aspx?Sesioni=false&idDokumenti=" + idVeprimeKFKoka + "&emriReal=FormatPrintimiVeprimeKF&printo=false&raportdyte=jo&iddesign=" + design.IdRaportDesign;
            }

        }

        #region Combo Boxes

        protected void cmbLlogariKunderParti_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback && (Request.Params["__CALLBACKID"].Contains("cmbLlogariKunderParti") && e.Value != null))
                ConfigureAspxComboBox.mbushComboLlogariaPaKolona(IdPerdoruesi, IdNdermarrja, cmbLlogariKunderParti, e);
        }

        protected void cmbLlogariKunderParti_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("cmbLlogariKunderParti"))
                ConfigureAspxComboBox.mbushComboLlogariaPaKolona(IdPerdoruesi, IdNdermarrja, cmbLlogariKunderParti, e);
        }

        protected void cmbKFKunderParti_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("cmbKFKunderParti"))
            {
                int value;
                if (e.Value == null || !int.TryParse(e.Value.ToString(), out value))
                    return;

                ConfigureAspxComboBox.KonfiguroComboBoxKlientFurnitoriById(IdPerdoruesi, IdNdermarrja, (ASPxComboBox)source, value);
            }
        }

        protected void cmbKFKunderParti_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("cmbKFKunderParti"))
                ConfigureAspxComboBox.KonfiguroComboBoxKlientFurnitor(e.Filter, e.BeginIndex + 1, e.EndIndex + 1,
                    (ASPxComboBox)source, cmbKonfigurimi.Text, IdPerdoruesi, IdNdermarrja, IdGjuha, 0);
        }

        #endregion

        #region  GRIDA E FATURAVE

        /* Parametri ngaRuajtja sherben per te dalluar rastin kur dataSource-in e grid_faturat eshte ruajtur ne sesion, por nderkohe qe eshte hapur faqja, faturat mund te jene likuiduar. 
            Per kete arsye, rimerret datasource dhe njehere nga databaza per te marre vetem ato qe jane aktualisht te palikuiduara  */
        private void InicializoGridFaturat(bool ngaRuajtja)
        {
            var gridFaturat = (ASPxGridView)ASPxNavBar1.Groups[0].FindControl("grid_faturat");
            var dt = mySessionObjects.merrColFaturatNgaSessioni(Session);
            if (dt == null || ngaRuajtja)
            {
                dt = colDokumentat.mbushGjitheDokumentatRegjistrimDokumentash(IdNdermarrja, IdPerdoruesi);
                mySessionObjects.ruajColFaturatNeSession(Session, dt);
            }
            gridFaturat.DataSource = dt;
            gridFaturat.DataBind();
            dt.Dispose();
        }

        private void InicializoGridMod(int idklienti)
        {
            var gridFaturat = (ASPxGridView)ASPxNavBar1.Groups[0].FindControl("grid_faturat");
            var dt = colDokumentat.mbushKokaShitjeSipasKlientit(idklienti, IdNdermarrja);
            mySessionObjects.ruajColFaturatNeSession(Session, dt);
            gridFaturat.DataSource = dt;
            gridFaturat.DataBind();
            dt.Dispose();
        }

        private void KonfiguroGrideFaturat(bool visibleindex)
        {
            var gridFaturat = (ASPxGridView)ASPxNavBar1.Groups[0].FindControl("grid_faturat");
            Shtokolona();
            KonfigurimComboGride.ShtoModelMeDataSource(gridFaturat, () =>
            {
                var konfigurimet = new colKonfigurimAmbjenti();
                konfigurimet.mbushKonfigAmbjSipasIdKategori(1, IdNdermarrja, IdPerdoruesi, IdGjuha);
                konfigurimet.mbushKonfigAmbjSipasIdKategori(2, IdNdermarrja, IdPerdoruesi, IdGjuha);
                return konfigurimet;
            }, Session, Komponente, GuidString);

            PercaktoTemplateKryesor();

            KonfigurimComboGride.ShtoMonedhe(gridFaturat, IdNdermarrja, IdPerdoruesi, Session, Komponente, GuidString);
            KonfigurimComboGride.ShtoNivelMeDataSource(gridFaturat, () =>
            {
                var nivelet = new colNivelRegjistrimi();
                nivelet.mbushGjitheNivelRegjistrimiSipasSuperKat(IdNdermarrja, IdPerdoruesi, 2);
                return nivelet;
            }, Session, Komponente, GuidString, "IdNiveli");
            KonfigurimComboGride.ShtoKushtPagese(gridFaturat, IdNdermarrja, Session, Komponente, GuidString);

            //funk.konfiguroGrideListeMadhe(grid_faturat, "IdDokumenti");
            GridUtil.konfigGrideListeEMadhePaTheme(gridFaturat, "IdDokumenti");
            GridViewDataTextColumn col3 = gridFaturat.Columns["Vlefta"] as GridViewDataTextColumn;
            col3.PropertiesEdit.DisplayFormatString = "0.00";
            GridViewDataTextColumn col5 = gridFaturat.Columns["VleraMbetur"] as GridViewDataTextColumn;
            col5.PropertiesEdit.DisplayFormatString = "0.00";
            GridViewDataTextColumn col4 = gridFaturat.Columns["Kursi"] as GridViewDataTextColumn;
            col4.PropertiesEdit.DisplayFormatString = "0.00####";

            if (visibleindex)
                GridUtil.percaktoVisibleColumnsMeWidth(IdGjuha, IdNdermarrja, gridFaturat, "grid_faturat", "Shto_VeprimeKF.aspx");
            else
                GridUtil.percaktoVisibleColumnsMeWidthPaVisibleIndex(IdGjuha, IdNdermarrja, gridFaturat, "grid_faturat", "Shto_VeprimeKF.aspx");
        }

        private void Shtokolona()
        {
            ASPxGridView grid_faturat = (ASPxGridView)ASPxNavBar1.Groups[0].FindControl("grid_faturat");
            GridViewDataTextColumn colnew1;
            GridViewDataDateColumn colnew2;
            if (grid_faturat.Columns["IdDokumenti"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "IdDokumenti"; colnew1.VisibleIndex = 0;
                grid_faturat.Columns.Add(colnew1);
            }
            if (grid_faturat.Columns["IdKonfigAmbjente"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "IdKonfigAmbjente"; colnew1.VisibleIndex = 2;
                grid_faturat.Columns.Add(colnew1);
            }
            if (grid_faturat.Columns["IdNiveli"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "IdNiveli"; colnew1.VisibleIndex = 1;
                grid_faturat.Columns.Add(colnew1);

            }
            if (grid_faturat.Columns["DtDokumenti"] == null)
            {
                colnew2 = new GridViewDataDateColumn();
                colnew2.FieldName = "DtDokumenti"; colnew2.VisibleIndex = 4;
                grid_faturat.Columns.Add(colnew2);

            }
            if (grid_faturat.Columns["DtMaturimi"] == null)
            {
                colnew2 = new GridViewDataDateColumn();
                colnew2.FieldName = "DtMaturimi"; colnew2.VisibleIndex = 5;
                grid_faturat.Columns.Add(colnew2);
            }
            if (grid_faturat.Columns["NrDokumenti"] == null)
            {
                colnew1 = new GridViewDataTextColumn(); colnew1.VisibleIndex = 3;
                colnew1.FieldName = "NrDokumenti";
                grid_faturat.Columns.Add(colnew1);
            }
            if (grid_faturat.Columns["IdKlientFurnitori"] == null)
            {
                colnew1 = new GridViewDataTextColumn(); colnew1.VisibleIndex = 6;
                colnew1.FieldName = "IdKlientFurnitori";
                grid_faturat.Columns.Add(colnew1);
            }

            if (grid_faturat.Columns["Kursi"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "Kursi"; colnew1.VisibleIndex = 11;
                grid_faturat.Columns.Add(colnew1);
            }
            if (grid_faturat.Columns["DtAzhornimi"] == null)
            {
                colnew2 = new GridViewDataDateColumn();
                colnew2.FieldName = "DtAzhornimi"; colnew2.VisibleIndex = 12;
                grid_faturat.Columns.Add(colnew2);
            }


            if (grid_faturat.Columns["KursAzhornimi"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "KursAzhornimi"; colnew1.VisibleIndex = 13;
                grid_faturat.Columns.Add(colnew1);
            }
            if (grid_faturat.Columns["IdMonedha"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "IdMonedha"; colnew1.VisibleIndex = 9;
                grid_faturat.Columns.Add(colnew1);
            }
            if (grid_faturat.Columns["Vlefta"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "Vlefta"; colnew1.VisibleIndex = 14;
                grid_faturat.Columns.Add(colnew1);
            }
            if (grid_faturat.Columns["VleraMbetur"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "VleraMbetur"; colnew1.VisibleIndex = 20;
                grid_faturat.Columns.Add(colnew1);
            }
            if (grid_faturat.Columns["VleftaPaLikujduar"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "VleftaPaLikujduar"; colnew1.VisibleIndex = 16;
                grid_faturat.Columns.Add(colnew1);
            }
            if (grid_faturat.Columns["Pershkrimi"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "Pershkrimi";
                grid_faturat.Columns.Add(colnew1);
            }


            if (grid_faturat.Columns["Status"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "Status";
                grid_faturat.Columns.Add(colnew1);
            }

            if (grid_faturat.Columns["Zbritja"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "Zbritja";
                grid_faturat.Columns.Add(colnew1);
            }
            if (grid_faturat.Columns["VleftaLikuiduar"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "VleftaLikuiduar";
                grid_faturat.Columns.Add(colnew1);
            }
            if (grid_faturat.Columns["VleftaLikuiduarMon"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "VleftaLikuiduarMon";
                grid_faturat.Columns.Add(colnew1);
            }
            if (grid_faturat.Columns["KodKlientFurnitor"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "KodKlientFurnitor";
                colnew1.Visible = false;
                grid_faturat.Columns.Add(colnew1);
            }
            if (grid_faturat.Columns["EmertimiKf"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "EmertimiKf";
                grid_faturat.Columns.Add(colnew1);
            }
            if (grid_faturat.Columns["LlojiKf"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "LlojiKf";
                colnew1.Visible = false;
                grid_faturat.Columns.Add(colnew1);
            }
            if (grid_faturat.Columns["IdKushtPagese"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "IdKushtPagese";
                colnew1.Visible = false;
                grid_faturat.Columns.Add(colnew1);
            }
        }

        protected void grid_faturat_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if ((e.Column.FieldName == "IdNiveli" ||
                 e.Column.FieldName == "IdMonedha" ||
                 e.Column.FieldName == "IdKlientFurnitori") &&
                Converter.ConvertToInt(e.Value) == 0)
                e.Criteria = null;
        }

        protected void grid_faturat_DataBound(object sender, EventArgs e)
        {
            var gridFaturat = (ASPxGridView)ASPxNavBar1.Groups[0].FindControl("grid_faturat");
            GridUtil.ShtoCommandColumnNeDatabound(gridFaturat, "#", "IdDokumenti");
            gridFaturat.Settings.ShowFilterRow = true;
            gridFaturat.Settings.ShowHeaderFilterButton = true;
            gridFaturat.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
            gridFaturat.Settings.ShowFilterRowMenu = true;
            gridFaturat.Settings.ShowGroupPanel = false;
            gridFaturat.SettingsBehavior.AllowSelectByRowClick = true;
            gridFaturat.SettingsBehavior.AllowFocusedRow = true;
            gridFaturat.Settings.ShowTitlePanel = false;
            gridFaturat.SettingsText.Title = MessagesResource.Messages["msgZgjidhniFaturat"];
        }

        protected void grid_faturat_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if ((e.Parameters == "pastro" || e.Parameters == "mbush") && Convert.ToBoolean(hfMeFatura.Value))
            {
                InicializoGridFaturat(false);
            }
            else if (!Convert.ToBoolean(hfMeFatura.Value))
            {
                var gridFaturat = (ASPxGridView)ASPxNavBar1.Groups[0].FindControl("grid_faturat");
                gridFaturat.DataSource = null;
                gridFaturat.DataBind();
            }
        }

        protected void grid_faturat_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            var gridFaturat = (ASPxGridView)ASPxNavBar1.Groups[0].FindControl("grid_faturat");
            e.Properties["cpNoRows"] = gridFaturat.VisibleRowCount;
        }

        protected void grid_faturat_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {

        }

        /// <summary>
        /// Template per griden e dokumentave kryesore
        /// </summary>
        private void PercaktoTemplateKryesor()
        {
            var grid_faturat = (ASPxGridView)ASPxNavBar1.Groups[0].FindControl("grid_faturat");
            var col3 = grid_faturat.Columns["Kursi"] as GridViewDataTextColumn;
            col3.PropertiesEdit.DisplayFormatString = "0.00####";
            var col15 = grid_faturat.Columns["KursAzhornimi"] as GridViewDataTextColumn;
            col15.PropertiesEdit.DisplayFormatString = "0.00####";
            col15.Caption = MessagesResource.Messages["msgShtoVeprimKFKursAzhornimi"];
        }

        protected void grid_faturat_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {
            var dtDokumenti = e.GetValue("DtDokumenti").ToString();
            var dtAzhornimi = e.GetValue("DtAzhornimi").ToString();
            var kursAzhornimi = e.GetValue("KursAzhornimi").ToString();

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

        #endregion
    }
}