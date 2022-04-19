using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using DbCore;
using DbCore.DbAdmin;
using DbCore.DbKontabiliteti;
using DbCore.DbListPagesat;
using DbCore.DbQendraKosto;
using DbCore.DbRegjistrim;
using DbCore.DbShare;
using DevExpress.Web;
using Newtonsoft.Json;
using RestApi.WebAPI.Models;
using System.Web;
using AlphaWebCommon.WebUtils.ASPxControlExtensions;
using DbCore.IMBUtils.Cache;
using DbCore.IMBUtils.Logging;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.Messages;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;

namespace PlatinumWeb
{
    public partial class Shto_ListPagesa : MyPageBase
    {
        private ResourceManager _rm;
        private CultureInfo _ci;
        private int idNdermarrje;
        private int idGjuha;
        private ResourceManager Rm => _rm ?? (_rm = new ResourceManager("Resources.Strings", Assembly.Load("App_GlobalResources")));
        private CultureInfo Ci => _ci ?? (_ci = mySessionObjects.ktheCultureInfo(Session));





        protected void Page_Load(object sender, EventArgs e)
        {
            int idGjuha;
            //keto jane te dhena sensitive dhe duhet te merren patjeter nga serveri
            int idPerdoruesi, idNdermarrje, idViti, idNdermarrjeVit;
            CultureInfo ci;
            AspxWebControlUtils.perkthePopUp(popFshiRresht, Rm.GetString("labelKujdes", Ci), lblMsgboxRreshti, Rm.GetString("labelAdministrimiMsgJeniSigurt", Ci), ButtonCancel22, Rm.GetString("labelAnullo", Ci));
            AspxWebControlUtils.perkthePopUp(popMesazhQK, Rm.GetString("labelKujdes", Ci), lblMsgbox4, Rm.GetString("msgDeshironiTeBeniShperndarjenNeQendratEKostos", Ci), ButtonCancelQK, Rm.GetString("cmbboxItemFilterAvancJo", Ci), ButtonOkQK, Rm.GetString("cmbboxItemFilterAvancPo", Ci));
            if (!IsPostBack)
            {
                CacheDataProvider.ClearSessionCache("StrukturaAdministrative", "colStrukturatAdministrative", "sipasPrindit");
                if (!mySessionObjects.isLogedIn(Session))
                {
                    clsFunksione.logout(Session, true, "FaqePaautorizuar");
                }
                idPerdoruesi = mySessionObjects.ktheIdPerdoruesi(Session);
                if (mySessionObjects.ktheKodNdermarrje(Session) == null)
                {
                    Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerdoruesi);
                }
                if (mySessionObjects.ktheKodNdermarrje(Session) == null)
                {
                    Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerdoruesi);
                }
                idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
                idViti = mySessionObjects.ktheIdVitNdermarrje(Session);
                idNdermarrjeVit = mySessionObjects.ktheNdermarrjeVit(Session);
                idGjuha = mySessionObjects.ktheGjuhe(Session);
                hfState.Set("idPerdoruesi", idPerdoruesi);
                hfState.Set("idGjuha", idGjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("idViti", idViti);
                hfState.Set("idNdermarrjeVit", idNdermarrjeVit);
                hfState.Set("ServerUrl", clsFunksione.ktheServerUrl(Request));
                hfState.Set("StatusAprovimi", "");
                hfState.Set("labelAdministrimiMsgJeniSigurt", rm.GetString("labelAdministrimiMsgJeniSigurt", Ci));
                hfPerdoruesi.Value = idPerdoruesi.ToString();
                ci = MessagesResource.KtheCultureInfo(idGjuha);
                var periudha = mySessionObjects.merrPeriudheKontabel(Session);
                if (periudha != null)
                {
                    lblPeriudhaAktuale.Text = String.Format("{0}-{1}", periudha.FillimiPeriudha.ToShortDateString(), periudha.MbarimiPeriudha.ToShortDateString()); //tocheck Getsoni
                }
                GridUtil.perktheButonaGride(hfState, mySessionObjects.ktheCultureInfo(Session));
                konfigGrid(idNdermarrje, idGjuha);
                vendosPeriudhenKlientSide();
                if (Request.QueryString["idNderm"] != null && string.IsNullOrEmpty(Request.QueryString["vjenNga"]) && Convert.ToInt32(Request.QueryString["idNderm"]) != idNdermarrje && Request.QueryString["vjenNga"] == "aprovim")
                    clsFunksione.logout(Session, true, "aprovimDokNdermarrjeGabuar");
                var currentContext = HttpContext.Current;
                Parallel.Invoke(new ParallelOptions
                {
                    MaxDegreeOfParallelism = 2
                }, () =>
                {
                    HttpContext.Current = currentContext;
                    if (hfShtimModifikim.Value == string.Empty)
                    {
                        if (Request.QueryString["shtim_modifikim"] == "shtim" || string.IsNullOrEmpty(Request.QueryString["shtim_modifikim"]))
                        {
                            hfShtimModifikim.Value = "shtim";
                            konfiguroVleraFillestareShto(idPerdoruesi, idNdermarrje, idGjuha, Rm, ci);
                        }
                        else
                        {
                            var idKokaLp = int.Parse(Request.QueryString["id"]);
                            if (Request.QueryString["shtim_modifikim"] == "klonim")
                            {
                                hfShtimModifikim.Value = "klonim";
                                hfArkivaDokId.Value = idKokaLp.ToString();
                                konfiguroVleraFillestareModifiko(idPerdoruesi, idNdermarrje, idKokaLp, idGjuha, Rm, ci);
                            }
                            else
                            {
                                hfShtimModifikim.Value = "modifikim";
                                konfiguroVleraFillestareModifiko(idPerdoruesi, idNdermarrje, idKokaLp, idGjuha, Rm, ci);
                            }
                        }
                    }
                    else
                    {
                        switch (hfShtimModifikim.Value)
                        {
                            case "shtim":
                                konfiguroVleraFillestareShto(idPerdoruesi, idNdermarrje, idGjuha, Rm, ci);
                                break;
                            case "modifikim":
                                var idKokaLp = int.Parse(Request.QueryString["id"]);
                                konfiguroVleraFillestareModifiko(idPerdoruesi, idNdermarrje, idKokaLp, idGjuha, Rm, ci);
                                break;
                        }
                    }
                }, () =>
                {
                    HttpContext.Current = currentContext;
                    vendosHfMePerkthime(Rm, ci);
                    EmratEKontrolleve(Rm, ci);
                    TeDrejtat(idPerdoruesi, idNdermarrje, idViti, ci);
                });
            }
            else
            {
                idGjuha = (int)hfState["idGjuha"];
                idPerdoruesi = (int)hfState["idPerdoruesi"];
                idNdermarrje = (int)hfState["idNdermarrje"];
                idViti = (int)hfState["idViti"];
                ci = MessagesResource.KtheCultureInfo(idGjuha);
            }
            percaktoTemplateMenu(idPerdoruesi, idViti, idNdermarrje, ASPxMenu1, Rm, ci);
            perktheLabel();
        }

        private void TeDrejtat(int idPerdoruesi, int idNdermarrje, int idViti, CultureInfo ci)
        {
            var tedrejtaInfo = new clsTeDrejtaRoli();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, "Konfigurime Gride");
            hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, "Shto_ListPagesa.aspx");
            hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
            hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
            hfTeDrejta.Add("ShtimDraft", tedrejtaInfo.DShtimDraft);
            hfTeDrejta.Add("ModifikimDraft", tedrejtaInfo.DModifikimDraft);
            hfTeDrejta.Add("Arkiva", tedrejtaInfo.DArkiva);
            clsNdermarrje nderm = new clsNdermarrje(idNdermarrje);
            hfState.Set("idMonedhaNdermarrje", nderm.NdermarrjeMonedha);
            //clsFunksione.perkthePopUp(popFshi, Rm.GetString("labelKujdes", ci), lblMsgbox, Rm.GetString("labelAdministrimiMsgJeniSigurt", ci), ButtonCancel, Rm.GetString("labelAnullo", ci));
            //clsFunksione.perkthePopUp(popFshiRresht, Rm.GetString("labelKujdes", ci), lblMsgboxRreshti, Rm.GetString("labelAdministrimiMsgJeniSigurt", ci), ButtonCancel22, Rm.GetString("labelAnullo", ci));
            //clsFunksione.perkthePopUp(popMesazhQK, Rm.GetString("labelKujdes", ci), lblMsgbox4, Rm.GetString("msgDeshironiTeBeniShperndarjenNeQendratEKostos", ci), ButtonCancelQK, Rm.GetString("cmbboxItemFilterAvancJo", ci), ButtonOkQK, Rm.GetString("cmbboxItemFilterAvancPo", ci));
        }

        /// <summary>
        ///     mbush kontrollet me te dhena
        /// </summary>
        /// <param name="idGjuha"></param>
        /// <param name="koka"></param>
        public void MerrTedhenat(int idGjuha, clsKokaListPagese koka)
        {
            var konf = new clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasId(koka.IdKonfigAmbjente, idGjuha);
            cmbKonfigurimi.Text = konf.KodKonfigAmbjente;
            hfKonffillestar.Value = $"{konf.KodKonfigAmbjente};{konf.PershkrimKonfigAmbjente}";
            if (hfShtimModifikim.Value == "modifikim")
            {
                txtNrDok.Text = koka.NrDok;
            }
            hfState.Set("IdStatusdok", koka.IdStatusDok);
            dteDtDok.Date = koka.DtDok;
            if (hfShtimModifikim.Value == "klonim")
                dteDtRegjistrimi.Value = DateTime.Now.Date;
            else dteDtRegjistrimi.Date = koka.DtRegjistrimi;
            txtShenime.Text = koka.Shenime;
            txtVlefta.Text = koka.Totali.ToString(CultureInfo.InvariantCulture);
            if (koka.IdDepartamenti != 0)
            {
                cmbDepartamenti.Value = koka.IdDepartamenti.ToString();
            }
            ConfigureAspxComboBox.mbushComboStrukturaAdm(cmbNenDepartamenti, koka.IdDepartamenti, (int)hfState["idNdermarrje"]);
            if (koka.IdNenDepartamenti != 0)
            {
                cmbNenDepartamenti.Value = koka.IdNenDepartamenti.ToString();
            }

            if (koka.StatusAprovimi != 0 && hfShtimModifikim.Value != "klonim")
            {
                lblStatusAprovimi.Text = clsFunksione.merrStatusAprovimi(koka.StatusAprovimi, Rm, Ci);
                hfState.Set("StatusAprovimi", koka.StatusAprovimi.ToString().Replace('_', ' '));
            }
            cmbMonedha.Value = koka.IdMonedha.ToString();
            txtKursi.Text = koka.Kursi.ToString(CultureInfo.InvariantCulture);
            cmbMuaji.Value = koka.Muaji.ToString();
            txtNrDok.Text = koka.NrDok;
            var dtlidhur = koka.merrIdsDokLidhur();
            hfLidhur.Value = (dtlidhur.Rows.Count != 0).ToString();
            AspxWebControlUtils.ShtoLidhje((int)hfState["idPerdoruesi"], (int)hfState["idViti"], (int)hfState["idNdermarrje"], hl, dtlidhur, koka.IdGjenerues, koka.IdNivelGjenerues, koka.IdKonfigGjenerues, idGjuha);
            var idMonedheZgjedhur = clsFunksione.ktheMonedhePerFormatNumri(idGjuha, int.Parse(cmbKonfigurimi.Value.ToString()), (int)hfState["idNdermarrje"], 710, string.Empty, koka.IdMonedha, false);

            var formatNrPerKonfig = new clsFormatiKonfig();
            formatNrPerKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(int.Parse(cmbKonfigurimi.Value.ToString()));
            var formatMonedhe = DbCore.DbShare.clsFormatKonfigTrup.ktheFormatNumriMonedhe(formatNrPerKonfig, idMonedheZgjedhur);
            hfState.Set("formatMonedhe", JsonConvert.SerializeObject(formatMonedhe));
        }

        private void vendosPeriudhenKlientSide()
        {
            var periudha = mySessionObjects.merrPeriudheKontabel(Session);
            if (periudha == null)
            {
                ImbLogger.Warn("periudha qe u morr nga sesioni eshte  bosh!");
                return;
            }
            hfPeriudhaKontabel.Clear();
            hfPeriudhaKontabel.Set("idPeriudha", periudha.IdPeriudha);
            hfPeriudhaKontabel.Set("emerPeriudha", periudha.EmerPeriudha);
            hfPeriudhaKontabel.Set("fillimiPeriudha", periudha.FillimiPeriudha);
            hfPeriudhaKontabel.Set("mbarimiPeriudha", periudha.MbarimiPeriudha);
        }



        private void VisibleMenu(int idPerdoruesi, int id)
        {
            var kusht = new clsKusht(Convert.ToInt32(cmbKonfigurimi.Value), "ZSP");
            var visible = clsFunksione.merrMenu(idPerdoruesi, kusht.Vlera, hfShtimModifikim.Value != "modifikim", hfState.Get<string>("StatusAprovimi"), id, cmbKonfigurimi.Text, 38);
            ASPxMenu1.Items.FindByName("Ruaj").ClientVisible = visible[0];
            ASPxMenu1.Items.FindByName("Draft").ClientVisible = hfShtimModifikim.Value != "kthim" && visible[1]; //tocheck Nestila

            ASPxMenu1.Items.FindByName("Aprovo").ClientVisible = visible[2];
            ASPxMenu1.Items.FindByName("Refuzo").ClientVisible = visible[3];
            ASPxMenu1.Items.FindByName("Delego").ClientVisible = visible[4];
            ASPxMenu1.Items.FindByName("Komento").ClientVisible = visible[5];
            ASPxMenu1.Items.FindByName("Modifiko").ClientVisible = visible[6];

            ASPxMenu1.Items.FindByName("Shto").ClientVisible = visible[8];
            ASPxMenu1.Items.FindByName("Fshi").ClientVisible = visible[10];

            hfTeDrejtaModSkema.Value = visible[6] ? "True" : "False";
        }

        private void vendosHfMePerkthime(ResourceManager rm, CultureInfo cultinf)
        {
            hfState.Set("msgGabimKursiMonedha", rm.GetString("msgGabimKursiMonedha", cultinf));
            hfState.Set("msgKursiNukMundTeJeteZero", rm.GetString("msgKursiNukMundTeJeteZero", cultinf));
            hfState.Set("msgKursiRiNdryshonShumeMeKursinMePare", rm.GetString("msgKursiRiNdryshonShumeMeKursinMePare", cultinf));
            hfState.Set("msgKursiDuhetNumer", rm.GetString("msgKursiDuhetNumer", cultinf));
            hfState.Set("msgZgjidhDokumentin", rm.GetString("msgZgjidhDokumentin", cultinf));
            hfState.Set("msgZgjidhniPunonjesin", rm.GetString("msgZgjidhniPunonjesin", cultinf));
            hfState.Set("msgZgjidhniNenDepartamentin", rm.GetString("msgZgjidhniNenDepartamentin", cultinf));
            hfState.Set("msgZgjidhniLlojin", rm.GetString("msgZgjidhniLlojin", cultinf));
            hfState.Set("msgShenoniNumrinEDokumentit", rm.GetString("msgShenoniNumrinEDokumentit", cultinf));
            hfState.Set("msgZgjidhniNjeDateDokumenti", rm.GetString("msgZgjidhniNjeDateDokumenti", cultinf));
            hfState.Set("msgZgjidhniNjeDateRegjstrimi", rm.GetString("msgZgjidhniNjeDateRegjstrimi", cultinf));
            hfState.Set("msgShperndarjeNeQendratEKostos", rm.GetString("msgShperndarjeNeQendratEKostos", cultinf));
            hfState.Set("msgPlotesoniTeGjithaFushat", rm.GetString("msgPlotesoniTeGjithaFushat", cultinf));
            hfState.Set("msgTrupiDokumentitNukDuhetLeneBosh", rm.GetString("msgTrupiDokumentitNukDuhetLeneBosh", cultinf));
            hfState.Set("msgTeDhenatEPages", rm.GetString("msgTeDhenatEPages", cultinf));
            hfState.Set("msgZgjidhKurs", rm.GetString("msgZgjidhKurs", cultinf));
            hfState.Set("msgZgjidhniDepartamentin", rm.GetString("msgZgjidhniDepartamentin", cultinf));
            hfState.Set("msgFshiniPunonjesin", rm.GetString("msgFshiniPunonjesin", cultinf));
            hfState.Set("msgBlerjeShitjeRuajtjeMeGabime", rm.GetString("msgBlerjeShitjeRuajtjeMeGabime", cultinf));
            hfState.Set("msgJuLutemPrisniDisaSekonda", rm.GetString("msgJuLutemPrisniDisaSekonda", cultinf));
            hfState.Set("msgDeshironiTeBeniShperndarjenNeQendratEKostos", rm.GetString("msgDeshironiTeBeniShperndarjenNeQendratEKostos", cultinf));
            hfState.Set("regjisDokZgjidhDokPerTeBashkengjitur", rm.GetString("regjisDokZgjidhDokPerTeBashkengjitur", cultinf));
        }

        private void EmratEKontrolleve(ResourceManager rm, CultureInfo cultinf)
        {
        }

        private void perktheLabel()
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            AspxWebControlUtils.perkthePopUp(popAprovo, rm.GetString("labelRaportKujdes", cultinf), lblMsgbox1, rm.GetString("msgPranimi", cultinf), ButtonCancel1, rm.GetString("labelAnullo", cultinf));
            konfigurimi_Label.Text = rm.GetString("labelModeli", cultinf);
            popupUniversal.HeaderText = rm.GetString("headerPopUpText", cultinf);
            btnImporti.Text = rm.GetString("btnRimerrVlerat", cultinf);
        }

        /// <summary>
        ///     mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        private void percaktoTemplateMenu(int idPerdoruesi, int idViti, int idNdermarrje, ASPxMenu aSPxMenu1, ResourceManager rm, CultureInfo ci)
        {
            var menu = new colMenuItem((int)hfState["idGjuha"]);
            menu.merrMenuItemSipasKomponentesRegjistrime((int)hfState["idGjuha"], "Shto_ListPagesa.aspx", idPerdoruesi, idNdermarrje, idViti, hfShtimModifikim.Value == "modifikim" ? false : true);
            var kok = new clsKokaFleteKontabel();
            foreach (var m in menu)
            {
                if (m.Name != "ItemFilter" && m.Name != "ItemFrame")
                {
                    clsToolbarConfig.ShtoMenuItem(Theme, aSPxMenu1, m);
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
                        kok = new clsKokaFleteKontabel(int.Parse(Request.QueryString["id"]), 38);

                        if (kok.NrDukumentiKokaFleteKontabel != null)
                        {
                            aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].NavigateUrl = string.Format("javascript: myFaqeCelje.kontrolloTeDrejta('Shto_FleteKontabel.aspx?shtim_modifikim=modifikim&id={0}&numur={1}')", kok.IdKokaFleteKontabel, kok.NrDukumentiKokaFleteKontabel);
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
                            var qend = new clsKokaQendraKosto();
                            qend.KtheKokaQKSipasIDGjeneruesDheKonfig(kok.IdKokaFleteKontabel, kok.IdKonfigAmbjente);

                            if (qend.NrDok != null)
                            {
                                aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].NavigateUrl = "javascript: myButtonClickLupa.LupaUniversal_Click('" + rm.GetString("msgShperndarjeNeQendratEKostos", ci) + "','LupaRegjistrimQendraKosto.aspx?shtim_modifikim=modifikim&idDokGjenerues=" + kok.IdKokaFleteKontabel + "&idkonfig=" + kok.IdKonfigAmbjente + "',900,600)";
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
                    clsToolbarConfig.ShtoMenuItemPerFrame(this, aSPxMenu1, clsFunksione.ktheUrlHelpi(m.UrlHelp).Item1);
                }
                if (m.Name == "Shto" || m.Name == "Kerko" || m.Name == "Pastro" || m.Name == "ItemFilter" || m.Name == "ItemFrame")
                {
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].BeginGroup = true;
                }
            }
            clsMenuInfo.ShtoMenuItemInfo(this, MenuInfo);
            var visible = clsFunksione.merrMenuVisibleDraft(hfShtimModifikim.Value != "modifikim", kok.IdStatusDokumenti);
            ASPxMenu1.Items.FindByName("Draft").ClientVisible = visible;
            var id = 0;
            if (hfShtimModifikim.Value == "modifikim")
            {
                int.TryParse(Request.QueryString["id"], out id);
                var iddok = int.Parse(Request.QueryString["id"]);
                hfArkivaDokId.Value = iddok.ToString();
            }
            VisibleMenu(idPerdoruesi, id);
            
        }

        /// <summary>
        ///     ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            var ci = mySessionObjects.ktheCultureInfo(Session);
            percaktoTemplateMenu((int)hfState["idPerdoruesi"], (int)hfState["idViti"], (int)hfState["idNdermarrje"], ASPxMenu1, Rm, ci);
        }

        /// <summary>
        ///     vendos datat dhe mujin sipas periudhes
        /// </summary>
        private void vendosDataDefault()
        {
            var periudha = mySessionObjects.merrPeriudheKontabel(Session);
            dteDtDok.Value = periudha.MbarimiPeriudha;
            dteDtRegjistrimi.Value = periudha.MbarimiPeriudha;
            cmbMuaji.SelectedIndex = periudha.MbarimiPeriudha.Month - 1;
        }

        /// <summary>
        ///     Vendos vlerat default kur po behet shtim
        /// </summary>
        /// <param name="idGjuha"></param>
        private void konfiguroVleraFillestareShto(int idPerdoruesi, int idNdermarrje, int idGjuha, ResourceManager rm, CultureInfo ci)
        {
            ConfigureAspxComboBox.percaktoTemplateComboMeEnableCallback(cmbNenDepartamenti);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmbDepartamenti);
            ConfigureAspxComboBox.mbushComboStrukturaAdm(cmbDepartamenti, 0, idNdermarrje,false);
            ConfigureAspxComboBox.percaktoTemplateComboJoList(txtKursi);
            ConfigureAspxComboBox.mbushComboMonedha(idPerdoruesi, idNdermarrje, false, cmbMonedha);
            cmbMonedha.SelectedIndex = 0;
            dteDtRegjistrimi.Value = DateTime.Now.Date;
            if (ci.Name == "en-US")
            {
                ConfigureAspxComboBox.mbushComboMuajiEng(cmbMuaji);
            }
            else
            {

                ConfigureAspxComboBox.mbushComboMuaji(cmbMuaji);
            }

            AspxWebControlUtils.vendosDateEditMask(dteDtDok, dteDtRegjistrimi);
           // vendosDataDefault();
            mbushComboKonfigurimet(idPerdoruesi, idNdermarrje, false, rm, ci, idGjuha);
            var idKonfig = int.Parse(cmbKonfigurimi.Value.ToString());
            var idMonedheZgjedhur = clsFunksione.ktheMonedhePerFormatNumri(idGjuha, idKonfig, idNdermarrje, 710, "cmbMonedha", -1, true);
            var formatNrPerKonfig = new clsFormatiKonfig();
            formatNrPerKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(idKonfig);
            var formatMonedhe = DbCore.DbShare.clsFormatKonfigTrup.ktheFormatNumriMonedhe(formatNrPerKonfig, idMonedheZgjedhur);

            hfState.Set("formatMonedhe", JsonConvert.SerializeObject(formatMonedhe));
            txtVlefta.Text = clsFunksione.krijoNumer(formatMonedhe.ShifraPasPresjesVlefta, "0");
        }



        /// <summary>
        ///     Vendos vlerat default kur po behet modifikim
        /// </summary>
        /// <param name="idGjuha"></param>
        private void konfiguroVleraFillestareModifiko(int idPerdoruesi, int idNdermarrje, int idKokaLP, int idGjuha, ResourceManager rm, CultureInfo ci)
        {
            AspxWebControlUtils.vendosDateEditMask(dteDtDok, dteDtRegjistrimi);
            ConfigureAspxComboBox.percaktoTemplateComboMeEnableCallback(cmbNenDepartamenti);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmbDepartamenti);
            ConfigureAspxComboBox.percaktoTemplateComboJoList(txtKursi);
            ConfigureAspxComboBox.mbushComboMonedha(idPerdoruesi, idNdermarrje, false, cmbMonedha);
            ConfigureAspxComboBox.mbushComboStrukturaAdm(cmbDepartamenti, 0, idNdermarrje);

            if (ci.Name == "en-US")
            {
                ConfigureAspxComboBox.mbushComboMuajiEng(cmbMuaji);
            }
            else
            {

                ConfigureAspxComboBox.mbushComboMuaji(cmbMuaji);
            }

            mbushComboKonfigurimet(idPerdoruesi, idNdermarrje, true, rm, ci, idGjuha);
            var kok = new clsKokaListPagese(idKokaLP, true);
            bool rimerrVlera = false;
            if (kok.IdKoka != 0)
            {
                rimerrVlera = clsKokaListPagese.KaTeDhenaPerTeMarre(kok.IdKoka) && clsAlternativaKushti.getAlternativa(kok.IdKonfigAmbjente, "RVNH") == "Po" && kok.IdStatusDok == 0;
                hfState.Set("rimerrVlera", rimerrVlera);
            }

            MerrTedhenat(idGjuha, kok);
        }
        /// <summary>
        ///     kthen konfigurimin e grides
        /// </summary>
        private void konfigGrid(int idNdermarrje, int idGjuha)
        {
            const string emriKomponentes = "Shto_ListPagesa.aspx";
            var oKomponente = new clsKomponente(emriKomponentes);
            var konfigurimi = new clsKonfigurimAmbjenti();
            konfigurimi.mbushKonfigDefaultKomponentes(oKomponente.IdKomponente, idNdermarrje);
            var trupiGrides = new colGridaTrupi(oKomponente.IdKomponente, konfigurimi.IdKonfigAmbjente, idGjuha);
            HfGridCol.Value = JsonConvert.SerializeObject(trupiGrides);
        }

        private void mbushComboKonfigurimet(int idPerdoruesi, int idNdermarrje, bool mod, ResourceManager rm, CultureInfo ci, int idGjuha)
        {
            var colKonfig = new colKonfigurimAmbjenti();
            const int idKategori = 38;
            colKonfig.mbushKonfigAmbjSipasIdKategoriKodNivelPaVartese(idKategori, "LP", idNdermarrje, idPerdoruesi, idGjuha, false);
            cmbKonfigurimi.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            var colprove = new ListBoxColumn
            {
                FieldName = "KodKonfigAmbjente",
                Caption = rm.GetString("cmbCmimeArtikulliCaptionKodi", ci)
            };
            var colemer = new ListBoxColumn
            {
                FieldName = "PershkrimKonfigAmbjente",
                Caption = rm.GetString("cmbCmimeArtikulliCaptionPershkrimi", ci)
            };
            cmbKonfigurimi.TextFormatString = "{0}";
            cmbKonfigurimi.Columns.Add(colprove);
            cmbKonfigurimi.Columns.Add(colemer);
            cmbKonfigurimi.DataSource = colKonfig;
            cmbKonfigurimi.ValueField = "IdKonfigAmbjente";
            cmbKonfigurimi.DataBind();
            cmbKonfigurimi.SelectedIndex = 0;
            hfKonffillestar.Value = cmbKonfigurimi.SelectedItem.Text;
        }

        /// <summary>
        ///     perdoret per te mbushur combon e departamenteve ne momentin qe perdoruesi fillon te shkruaj
        /// </summary>
        /// <param name="source">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void cmbDepartamenti_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].Contains("cmbDepartamenti"))
                {
                    ConfigureAspxComboBox.mbushComboStrukturaAdm(cmbDepartamenti, 0, (int)hfState["idNdermarrje"]);
                }
            }
        }

        protected void cmbDepartamenti_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].Contains("cmbDepartamenti"))
                {
                    DbCore.DbListPagesat.colStrukturatAdministrative col = new DbCore.DbListPagesat.colStrukturatAdministrative();
                    col.mbushGjitheStrukturaAdmPrindiSipasNdermarjes(idNdermarrje,true);

                    var dsReal = col.Where(x => x.Emri.IndexOf(e.Filter, StringComparison.InvariantCultureIgnoreCase) > -1).Skip(e.BeginIndex).Take(e.EndIndex + 1);
                    cmbDepartamenti.TextField = "Emri";
                    cmbDepartamenti.ValueField = "IdStrukturaAdm";
                    cmbDepartamenti.DataSource = dsReal.ToList();
                    cmbDepartamenti.DataBind();
                }
            }
        }

        /// <summary>
        ///     perdoret per te mbushur combon e nendepartamenteve ne momentin qe perdoruesi fillon te shkruaj
        /// </summary>
        /// <param name="source">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void cmbNenDepartamenti_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].Contains("cmbNenDepartamenti"))
                {
                    if (e.Value == null) return;
                    int id;
                    if (cmbDepartamenti.Text != "")
                        id = Convert.ToInt32(cmbDepartamenti.Value);
                    else
                        id = 0;

                    DbCore.DbListPagesat.colStrukturatAdministrative col = new DbCore.DbListPagesat.colStrukturatAdministrative();
                    if (id == 0)
                        col.mbushGjitheStrukturaAdmPrindiSipasNdermarjes(idNdermarrje,true);
                    else col.mbushStrukturaAdmSipasPrindit(id,true);
                    cmbNenDepartamenti.DataSource = col.Where(x => x.IdStrukturaAdm == Convert.ToInt32(e.Value));
                    cmbNenDepartamenti.TextField = "Emri";
                    cmbNenDepartamenti.ValueField = "IdStrukturaAdm";

                    //clsFunksione.mbushComboStrukturaAdm(cmbNenDepartamenti, id, mySessionObjects.merrIdNdermarrjeSesioni(Session));
                }
            }
        }

        protected void cmbNenDepartamenti_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].Contains("cmbNenDepartamenti"))
                {
                    if (string.IsNullOrWhiteSpace(e.Filter)) return;
                    int id;
                    if (cmbDepartamenti.Text != "")
                        id = Convert.ToInt32(cmbDepartamenti.Value);
                    else
                        id = 0;

                    DbCore.DbListPagesat.colStrukturatAdministrative col = new DbCore.DbListPagesat.colStrukturatAdministrative();
                    if (id == 0)
                        col.mbushGjitheStrukturaAdmPrindiSipasNdermarjes(idNdermarrje,true);
                    else col.mbushStrukturaAdmSipasPrindit(id,true);
                    var dsReal = col.Where(x => x.Emri.IndexOf(e.Filter, StringComparison.InvariantCultureIgnoreCase) > -1).Skip(e.BeginIndex).Take(e.EndIndex + 1);
                    cmbNenDepartamenti.TextField = "Emri";
                    cmbNenDepartamenti.ValueField = "IdStrukturaAdm";
                    cmbNenDepartamenti.DataSource = dsReal.ToList();
                    cmbNenDepartamenti.DataBind();

                }
            }
        }
    }
}