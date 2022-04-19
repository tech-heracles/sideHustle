using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DbCore;
using DbCore.DbAdmin;
using DbCore.DbKontabiliteti;
using DbCore.DbQendraKosto;
using DbCore.DbRegjistrim;
using DbCore.DbShare;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Extensions;
using DbCore.IMBUtils.Messages;
using DevExpress.Web;
using NLog;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using PlatinumWeb.Templates;

namespace PlatinumWeb
{
    public partial class Shto_FleteKontabel : MyPageBase
    {
        private colFormatKonfig _colFormat = new colFormatKonfig();

        protected void Page_Init(object sender, EventArgs e)
        {//perdoret per te vene nr automatik te dokumentit                       
            var idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
            _colFormat = new colFormatKonfig(5, idNdermarrje);
            txtNrReference.Text = clsKokaFleteKontabel.GjeneroNrReference(mySessionObjects.ktheNdermarrjeVit(Session)).ToString();
            hfNrRef.Value = txtNrReference.Text;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!mySessionObjects.isLogedIn(Session))
                clsFunksione.logout(Session, true, "FaqePaautorizuar");
            var idPerdoruesi = mySessionObjects.ktheIdPerdoruesi(Session);
            if (mySessionObjects.ktheKodNdermarrje(Session) == null)
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerdoruesi);
            var idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
            var idViti = mySessionObjects.ktheIdVitNdermarrje(Session);
            var rm = new ResourceManager("Resources.Strings", Assembly.Load("App_GlobalResources"));
            var ci = mySessionObjects.ktheCultureInfo(Session);
            if (!Page.IsPostBack)
            {
                var idGjuha = mySessionObjects.ktheGjuhe(Session);
                hfState.Set("idGjuha", idGjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("idPerdoruesi", idPerdoruesi);
                hfState.Set("idViti", idViti);
                if (mySessionObjects.merrPeriudheKontabel(Session) != null)
                {
                    var periudha = mySessionObjects.merrPeriudheKontabel(Session);
                    btnPeriudha.Text = periudha.NrPeriudha.ToString();
                    lblPeriudhaAktuale.Text = $"{periudha.FillimiPeriudha.ToShortDateString()}-{periudha.MbarimiPeriudha.ToShortDateString()}";
                }
                var idNderViti = mySessionObjects.ktheNdermarrjeVit(Session);

                if (string.IsNullOrEmpty(Request.QueryString["shtim_modifikim"]))
                {
                    hfShtimModifikim.Value = "shtim";
                    KonfiguroVleraFillestareShto(idGjuha, idNderViti, idPerdoruesi, idNdermarrje, rm, ci);
                }
                else
                    switch (Request.QueryString["shtim_modifikim"])
                    {
                        case "shtim":
                            hfId.Value = "0";
                            hfShtimModifikim.Value = "shtim";
                            KonfiguroVleraFillestareShto(idGjuha, idNderViti, idPerdoruesi, idNdermarrje, rm, ci);
                            break;
                        case "modifikim":
                            hfId.Value = Request.QueryString["id"];
                            hfShtimModifikim.Value = "modifikim";
                            KonfiguroVleraFillestareModifiko(idGjuha, idNderViti, idNdermarrje, idPerdoruesi, rm, ci);
                            break;
                        case "klonim":
                            hfId.Value = Request.QueryString["id"];
                            hfShtimModifikim.Value = "klonim";
                            KonfiguroVleraFillestareModifiko(idGjuha, idNderViti, idNdermarrje, idPerdoruesi, rm, ci);
                            break;
                    }
                hfIdMonedhaNder.Value = clsMonedha.ktheIdMonedhenENdermarrjes(idNdermarrje).ToString();
                hfMonedhaNder.Value = clsMonedha.ktheMonedhenENdermarrjes(idNdermarrje);
                mbushHfFormate(_colFormat);
                MbushComboBoxSkema();
                var tedrejtaInfo = new clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, mySessionObjects.ktheIdVitNdermarrje(Session), "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, mySessionObjects.ktheIdVitNdermarrje(Session), "Shto_FleteKontabel.aspx");
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                hfTeDrejta.Add("ShtimDraft", tedrejtaInfo.DShtimDraft);
                hfTeDrejta.Add("ModifikimDraft", tedrejtaInfo.DModifikimDraft);
                MbushHiddenFieldMePerkthime(ci, rm);
            }
            PercaktoTemplateMenu(idPerdoruesi, idViti, idNdermarrje, ASPxMenu1);
            AspxWebControlUtils.perkthePopUp(popFshi, rm.GetString("labelKujdes", ci), lblMsgbox, rm.GetString("labelAdministrimiMsgJeniSigurt", ci), ButtonCancel, rm.GetString("labelAnullo", ci));
            Container.Attributes["src"] = "";
        }

        /// <summary>
        /// Metode qe sherben per te kaluar perkthime te js
        /// </summary>
        /// <param name="cultinf">Merr culture info</param>
        /// <param name="rm">Merr Resource Manager</param>
        private void MbushHiddenFieldMePerkthime(CultureInfo cultinf, ResourceManager rm)
        {
            hfState.Set("msgGabimGjateTransferimitTeTeDhenave", rm.GetString("msgGabimGjateTransferimitTeTeDhenave", cultinf));
            hfState.Set("msgKursiRiNdryshonShumeMeKursinMePare", rm.GetString("msgKursiRiNdryshonShumeMeKursinMePare", cultinf));
            hfState.Set("msgKursiNukMundTeJeteZero", rm.GetString("msgKursiNukMundTeJeteZero", cultinf));
            hfState.Set("msgKursiNukMundTeJeteNegativOseZero", rm.GetString("msgKursiNukMundTeJeteNegativOseZero", cultinf));
            hfState.Set("msgVendosniDateEDokumentit", rm.GetString("msgVendosniDateEDokumentit", cultinf));
            hfState.Set("msgZgjidhDokumentin", rm.GetString("msgZgjidhDokumentin", cultinf));
            hfState.Set("MsgBlerjeShitjeAutorizime", rm.GetString("MsgBlerjeShitjeAutorizime", cultinf));
            hfState.Set("msgRreshtaTePavlefshemNeGride", rm.GetString("msgRreshtaTePavlefshemNeGride", cultinf));
            hfState.Set("msgGrupKontabilizimi", rm.GetString("msgGrupKontabilizimi", cultinf));
            hfState.Set("popupAdministrimiUniversal", rm.GetString("popupAdministrimiUniversal", cultinf));
            hfState.Set("msgVleraEDebiseDuhetTeJeteNumerike", rm.GetString("msgVleraEDebiseDuhetTeJeteNumerike", cultinf));
            hfState.Set("msgJepniVlerenEDebise", rm.GetString("msgJepniVlerenEDebise", cultinf));
            hfState.Set("msgVleraEKursitDuhetTeJeteNumerike", rm.GetString("msgVleraEKursitDuhetTeJeteNumerike", cultinf));
            hfState.Set("msgVleraEKrediseDuhetTeJeteNumerike", rm.GetString("msgVleraEKrediseDuhetTeJeteNumerike", cultinf));
            hfState.Set("msgJepniVlerenEKredise", rm.GetString("msgJepniVlerenEKredise", cultinf));
            hfState.Set("msgJuLutemPlotesoniLlogarine", rm.GetString("msgJuLutemPlotesoniLlogarine", cultinf));
            hfState.Set("msgJuLutemPlotesoniMonedhen", rm.GetString("msgJuLutemPlotesoniMonedhen", cultinf));
            hfState.Set("msgZgjidhLlojinEDokumentit", rm.GetString("msgZgjidhLlojinEDokumentit", cultinf));
            hfState.Set("msgZgjidhSkemenKontabel", rm.GetString("msgZgjidhSkemenKontabel", cultinf));
            hfState.Set("msgZgjidhSkemenFleteKontabel", rm.GetString("msgZgjidhSkemenFleteKontabel", cultinf));
            hfState.Set("msgZgjidhPeriudhen", rm.GetString("msgZgjidhPeriudhen", cultinf));
            hfState.Set("msgTrupiDokumentitNukDuhetLeneBosh", rm.GetString("msgTrupiDokumentitNukDuhetLeneBosh", cultinf));
            hfState.Set("msgDeshironiShperndarjeQendraKosto", rm.GetString("msgDeshironiShperndarjeQendraKosto", cultinf));
            hfState.Set("msgVendosniNumrinEDokumentit", rm.GetString("msgVendosniNumrinEDokumentit", cultinf));
            hfState.Set("msgZgjidhniNjeDateDokumenti", rm.GetString("msgZgjidhniNjeDateDokumenti", cultinf));
            hfState.Set("msgZgjidhniNjeDateRegjstrimi", rm.GetString("msgZgjidhniNjeDateRegjstrimi", cultinf));
            hfState.Set("msgPlotesoniTeGjithaFushat", rm.GetString("msgPlotesoniTeGjithaFushat", cultinf));
            hfState.Set("headerPopUpZgjidhQendrenKostos", rm.GetString("headerPopUpZgjidhQendrenKostos", cultinf));
            hfState.Set("headerPopUpZgjidhSkemenKostos", rm.GetString("headerPopUpZgjidhSkemenKostos", cultinf));
            hfState.Set("msgZgjidhniObjektivenEKostos", rm.GetString("msgZgjidhniObjektivenEKostos", cultinf));
            hfState.Set("msgZgjidhniLlogarine", rm.GetString("msgZgjidhniLlogarine", cultinf));
            hfState.Set("msgKjoQenderNukEkziston", rm.GetString("msgKjoQenderNukEkziston", cultinf));
            hfState.Set("msgKyObjektivNukEziston", rm.GetString("msgKyObjektivNukEziston", cultinf));
            hfState.Set("msgKyObjektivNukEshteAktiv", rm.GetString("msgKyObjektivNukEshteAktiv", cultinf));
            hfState.Set("msgKyObjektivNukEshteAktivNeKeteDate", rm.GetString("msgKyObjektivNukEshteAktivNeKeteDate", cultinf));
            hfState.Set("msgLlogariaNukEshtePerTuShperndareNeQendraKosto", rm.GetString("msgLlogariaNukEshtePerTuShperndareNeQendraKosto", cultinf));
            hfState.Set("msgVleftaDuhetTeJeteNumer", rm.GetString("msgVleftaDuhetTeJeteNumer", cultinf));
            hfState.Set("msgVleftaDuheTeJeteNumerPozitiv", rm.GetString("msgVleftaDuheTeJeteNumerPozitiv", cultinf));
            hfState.Set("msgVleftaEQKDuhetTeJeteNumer", rm.GetString("msgVleftaEQKDuhetTeJeteNumer", cultinf));
            hfState.Set("msgVleftaEQKDuheTeJeteNumerPozitiv", rm.GetString("msgVleftaEQKDuheTeJeteNumerPozitiv", cultinf));
            hfState.Set("msgVleftaMonBazeDuhetTeJeteNumer", rm.GetString("msgVleftaMonBazeDuhetTeJeteNumer", cultinf));
            hfState.Set("msgVleftaMonBazeDuhetTeJeteNumerPozitiv", rm.GetString("msgVleftaMonBazeDuhetTeJeteNumerPozitiv", cultinf));
            hfState.Set("MenuKokeDokumenti", rm.GetString("MenuKokeDokumenti", cultinf));
            hfState.Set("MenuTrupDokumenti", rm.GetString("MenuTrupDokumenti", cultinf));
            hfState.Set("MenuFundDokumenti", rm.GetString("MenuFundDokumenti", cultinf));
            hfState.Set("msgTrupiDokNukDuhetBosh", rm.GetString("msgTrupiDokNukDuhetBosh", cultinf));
            GridUtil.perktheButonaGride(hfState, cultinf);
        }

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        private void PercaktoTemplateMenu(int idPerdoruesi, int idViti, int idNdermarrje, ASPxMenu aSPxMenu1)
        {
            var menu = new colMenuItem(mySessionObjects.ktheGjuhe(Session));
            menu.merrMenuItemSipasKomponentesRegjistrime(mySessionObjects.ktheGjuhe(Session), "Shto_FleteKontabel.aspx", idPerdoruesi, idNdermarrje, idViti, hfShtimModifikim.Value != "modifikim");
            var id = int.Parse(hfId.Value);
            var kok = new clsKokaFleteKontabel(id);
            foreach (var m in menu)
            {
                if (m.Name != "ItemFilter" && m.Name != "ItemFrame")
                    clsToolbarConfig.ShtoMenuItem(Theme, aSPxMenu1, m);
                if ((hfLidhur.Value == "True" || kok.IdLlojDok == 20) && (m.Name == "Kontabilizo" || m.Name == "Draft"))
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                if (hfShtimModifikim.Value != "modifikim" && m.Name == "Fshi")
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                if (hfShtimModifikim.Value != "modifikim" && m.Name == "PrintPreview")
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                if ((hfShtimModifikim.Value == "shtim" || hfLidhur.Value == "True" || kok.IdLlojDok == 20) && m.Name == "Klono")
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                switch (m.Name)
                {
                    case "ItemFrame":
                        clsToolbarConfig.ShtoMenuItemPerFrame(this, aSPxMenu1, clsFunksione.ktheUrlHelpi(m.UrlHelp).Item1);
                        break;
                    case "ItemFilter":
                        EventHandler handlerPerRuajFilter = Ruaj_ASPxButton_ClickSkema;
                        EventHandler handlerPerFshiFilter = FshiFilter_ASPxButton_Click;
                        clsToolbarConfig.ShtoMenuItemPerFilter(this, aSPxMenu1, handlerPerRuajFilter, handlerPerFshiFilter);
                        break;
                    case "QendraKosto":
                        aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].Text = "Ruaj Qendra Kosto";
                        if (hfShtimModifikim.Value != "modifikim")
                            aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                        else
                        {
                            if (kok.NrDukumentiKokaFleteKontabel != null)
                            {
                                var qend = new clsKokaQendraKosto();
                                qend.KtheKokaQKSipasIDGjeneruesDheKonfig(kok.IdKokaFleteKontabel, kok.IdKonfigAmbjente);
                                aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = qend.NrDok != null;
                            }
                            else aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                        }
                        break;
                    case "Draft":
                        if (kok.IdKokaFleteKontabel > 0 && kok.IdStatusDokumenti == 1)
                            aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                        break;
                }
                if (m.Name == "Shto" || m.Name == "Kerko" || m.Name == "Pastro" || m.Name == "ItemFilter" || m.Name == "ItemFrame" || m.Name == "Klono")
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].BeginGroup = true;
                

            }
            if ( hfShtimModifikim.Value == "klonim")
            {

                ASPxMenu1.Items.FindByName("Klono").ClientVisible = false;
                ASPxMenu1.Items.FindByName("Draft").ClientVisible = true;

            }
            var handlerPerPo = new EventHandler(btnPo_Click);
            var handlerPerJo = new EventHandler(btnJo_Click);
            clsMenuInfo.ShtoMenuItemInfo(this, MenuInfo, handlerPerPo, handlerPerJo);
        }

        private void MbushComboBoxSkema()
        {
            var col = new colKokatSkematFletetKontabel(mySessionObjects.merrIdNdermarrjeSesioni(Session), mySessionObjects.ktheIdPerdoruesi(Session));
            col.Insert(0, new clsKokaSkemaFleteKontabel());
            var model = new MyMenuFilterModel
            {
                ValueField = "IdKokaSkemaFK",
                TextField = "KodiKokaSkemaFK",
                DataSource = col
            };
            mySessionObjects.RuajNeSession(HttpContext.Current.Session, model, $"filtraGride_{1}");
        }

        public void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje
            var itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            var cmbFiltra = ((MenuFilter)itemButton.Template).FindControl("btnFiltra") as ASPxComboBox;
            var koka = new clsKokaSkemaFleteKontabel(cmbFiltra.Text, mySessionObjects.merrIdNdermarrjeSesioni(Session));
            if (koka.KodiKokaSkemaFK == null) return;
            koka.IdPerdoruesi = mySessionObjects.ktheIdPerdoruesi(Session);
            var mesazh = new clsMesazh();
            var rm = new ResourceManager("Resources.Strings", Assembly.Load("App_GlobalResources"));
            var ci = mySessionObjects.ktheCultureInfo(Session);
            mesazh = koka.fshi(rm, ci);
            MbushComboBoxSkema();
            PercaktoTemplateMenu(mySessionObjects.ktheIdPerdoruesi(Session), mySessionObjects.ktheNdermarrjeVit(Session), mySessionObjects.merrIdNdermarrjeSesioni(Session), ASPxMenu1);
            if (mesazh.Status)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            cmbFiltra.Text = "";
            hfStatusRuajtje.Value = "false";
        }

        /// <summary>
        /// sherben per te ruajtur filtrin e zgjedhur
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void Ruaj_ASPxButton_ClickSkema(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje
            var itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            var cmbFiltra = ((MenuFilter)itemButton.Template).FindControl("btnFiltra") as ASPxComboBox;
            var mesazh = new clsMesazh();
            var koka = new clsKokaSkemaFleteKontabel
            {
                KodiKokaSkemaFK = cmbFiltra.Text,
                PershkrimiKokaSkemaFK = cmbFiltra.Text,
                IdStatusDok = 1,
                IdPerdoruesi = mySessionObjects.ktheIdPerdoruesi(Session),
                IdNdermarje = mySessionObjects.merrIdNdermarrjeSesioni(Session)
            };
            try
            {
                koka.OColTrupi = RuajTrupiSkema(koka.IdNdermarje);
            }
            catch (Exception ex)
            {
                LogManager.GetCurrentClassLogger().Error(ex.Message);
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, ex.Message, pnlMesazhi);
                hfStatusRuajtje.Value = "false";
                return;
            }
            var rm = new ResourceManager("Resources.Strings", Assembly.Load("App_GlobalResources"));
            var ci = mySessionObjects.ktheCultureInfo(Session);
            mesazh = koka.ruaj(rm, ci);
            MbushComboBoxSkema();
            PercaktoTemplateMenu(mySessionObjects.ktheIdPerdoruesi(Session), mySessionObjects.ktheNdermarrjeVit(Session), mySessionObjects.merrIdNdermarrjeSesioni(Session), ASPxMenu1);
            if (mesazh.Status)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            hfStatusRuajtje.Value = "false";
            cmbFiltra.Text = "";
        }

        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e) =>
            PercaktoTemplateMenu(mySessionObjects.ktheIdPerdoruesi(Session), mySessionObjects.ktheNdermarrjeVit(Session), mySessionObjects.merrIdNdermarrjeSesioni(Session), ASPxMenu1);

        private void KonfiguroVleraFillestareShto(int idGjuha, int idNderViti, int idPerdoruesi, int idNdermarrje, ResourceManager rm, CultureInfo ci)
        {
            AspxWebControlUtils.vendosDateEditMask(Data_DateEdit);
            AspxWebControlUtils.vendosDateEditMask(DateRegjistrimi_DateEdit);
            VendosDataDefault();
            var idViti = mySessionObjects.ktheIdVitNdermarrje(Session);
            ConfigureAspxComboBox.mbushComboPeriudhatAktuale(idViti, btnPeriudha);
            ConfigureAspxComboBox.mbushComboKonfigurimetVetemKodiSiTekst(idPerdoruesi, idNdermarrje, konfigurimi_ComboBox, 116, false, idGjuha);
            //vendoset fiks sepse ai eshte konfigurimi default per ambjentin e flets kontabel
            konfigurimi_ComboBox.SelectedIndex = 0;
            var konf = new clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasId(int.Parse(konfigurimi_ComboBox.SelectedItem.Value.ToString()), idGjuha);
            hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;
            ConfigureAspxComboBox.mbushComboLlojQendre(cmbLloji, true, rm, ci);
            ConfigureAspxComboBox.mbushComboGrupKontabilizimi(idNdermarrje, txtNrGrupKontabilizimi);
            ConfigureAspxComboBox.mbushComboSkemaFleteKontabel(idPerdoruesi, idNderViti, btneSkemaFK);
            ConfigureAspxComboBox.mbushComboAutorizime(idPerdoruesi, cmbAutorizimi);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmbQendraKosto);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmbObjektiva);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(btneSkemaFK);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(txtNrGrupKontabilizimi);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(btnPeriudha);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmbAutorizimi);
            var formatNrPerKonfig = new clsFormatiKonfig();
            formatNrPerKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(int.Parse(konfigurimi_ComboBox.Value.ToString()));
            if (formatNrPerKonfig.KonfigTrupi.Count == 0)
            {
                var trup = new clsFormatKonfigTrup(clsFormatKonfigTrup.defaultFormatSasia, clsFormatKonfigTrup.defaultFormatCmimi, clsFormatKonfigTrup.defaultFormatVlefta, clsFormatKonfigTrup.defaultFormatZbritja, clsFormatKonfigTrup.defaultFormatStringSasia, clsFormatKonfigTrup.defaultFormatStringCmimi, clsFormatKonfigTrup.defaultFormatStringVlefta, clsFormatKonfigTrup.defaultFormatStringZbritja);
                formatNrPerKonfig.KonfigTrupi.Add(trup);
            }
            var serializusi = new JavaScriptSerializer();
            hfState.Set("formatMonedhe", serializusi.Serialize(formatNrPerKonfig));
            var mon = new colMonedhat();
            mon.mbushGjitheMonedhat(idNdermarrje, idPerdoruesi);
            hfState.Set("formatKurset", serializusi.Serialize(mon));
            hfState.Set("vlerallog", serializusi.Serialize(new int[0]));
            hfState.Set("vleraqk", serializusi.Serialize(new int[0]));
        }

        /// <summary>
        /// mbush kombot dhe gridat
        /// </summary>
        /// <param name="idGjuha"></param>
        /// <param name="idNderViti"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="idPerdoruesi"></param>
        /// <param name="rm"></param>
        /// <param name="ci"></param>
        private void KonfiguroVleraFillestareModifiko(int idGjuha, int idNderViti, int idNdermarrje, int idPerdoruesi, ResourceManager rm, CultureInfo ci)
        {
            AspxWebControlUtils.vendosDateEditMask(Data_DateEdit);
            AspxWebControlUtils.vendosDateEditMask(DateRegjistrimi_DateEdit);
            var idViti = mySessionObjects.ktheIdVitNdermarrje(Session);
            ConfigureAspxComboBox.mbushComboPeriudhatAktuale(idViti, btnPeriudha);
            ConfigureAspxComboBox.mbushComboKonfigurimetVetemKodiSiTekst(idPerdoruesi, idNdermarrje, konfigurimi_ComboBox, 116, true, idGjuha);
            ConfigureAspxComboBox.mbushComboGrupKontabilizimi(idNdermarrje, txtNrGrupKontabilizimi);
            ConfigureAspxComboBox.mbushComboSkemaFleteKontabel(idPerdoruesi, idNderViti, btneSkemaFK);
            ConfigureAspxComboBox.mbushComboAutorizime(idPerdoruesi, cmbAutorizimi);
            ConfigureAspxComboBox.mbushComboLlojQendre(cmbLloji, true, rm, ci);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(btneSkemaFK);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(txtNrGrupKontabilizimi);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(btnPeriudha);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmbAutorizimi);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmbQendraKosto);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmbObjektiva);
            var id = int.Parse(hfId.Value);
            MbushHiddenFieldet(new colTrupatFletetKontabel(id));
            if (new clsKokaFleteKontabel(id).NrDukumentiKokaFleteKontabel != null)
                MerrTedhenat(idGjuha, idViti, idNdermarrje, idPerdoruesi, new clsKokaFleteKontabel(id));
        }

        /// <summary>
        /// funksioni qe mbush fushat
        /// </summary>
        /// <param name="idGjuha"></param>
        /// <param name="idViti"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="idPerdoruesi"></param>
        /// <param name="l"></param>
        public void MerrTedhenat(int idGjuha, int idViti, int idNdermarrje, int idPerdoruesi, clsKokaFleteKontabel l)
        {
            txtNrDokumenti.Text = l.NrDukumentiKokaFleteKontabel;
            Data_DateEdit.Value = l.DateDokumentiKokaFleteKontabel;
            if (hfShtimModifikim.Value == "modifikim")
                txtNrReference.Text = l.NrReferenceKokaFleteKontabel;
            txtPershkrimi.Text = l.PershkrimKokaFleteKontabel;
            if (l.IdGrupKontabilizimi != 0)
                txtNrGrupKontabilizimi.Text = new clsGrupKontabilizimi(l.IdGrupKontabilizimi).NrGrupKontabilizimi;
            DateRegjistrimi_DateEdit.Value = l.DateRegjistrimiKokaFleteKontabel;
            var konf = new clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasId(l.IdKonfigAmbjente, idGjuha);
            hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;
            konfigurimi_ComboBox.Value = l.IdKonfigAmbjente.ToString();
            var trup = new colTrupatFletetKontabel(l.IdKokaFleteKontabel);
            double debimon = 0;
            double kredimon = 0;
            foreach (var t in trup)
            {
                debimon += t.VleftaDebiMonBazeTrupiFleteKontabel;
                kredimon += t.VleftaKrediMonBazeTrupiFleteKontabel;
            }
            txtDebiMonedhaBaze.Text = debimon.ToString();
            txtKrediMonedhaBaze.Text = kredimon.ToString();
            if ((debimon - kredimon) > 0)
            {
                txtDebiDiferenca.Text = (debimon - kredimon).ToString();
                txtKrediDiferenca.Text = "0.00";
            }
            else
            {
                txtDebiDiferenca.Text = "0.00";
                txtKrediDiferenca.Text = (kredimon - debimon).ToString();
            }
            if (hfShtimModifikim.Value == "modifikim")
            {
                var dtlidhur = l.MerrIdsDokLidhur();
                hfLidhur.Value = (dtlidhur.Rows.Count != 0).ToString();
                AspxWebControlUtils.ShtoLidhje(idPerdoruesi, idViti, idNdermarrje, hl, dtlidhur, l.IdGjenerues, l.IdNivelGjenerues, l.IdKonfigGjenerues, idGjuha);
            }

            var qend = new clsKokaQendraKosto();
            qend.KtheKokaQKSipasIDGjeneruesDheKonfig(l.IdKokaFleteKontabel, l.IdKonfigAmbjente);
            hfIdKonfigAmbjenteQK.Value = qend.IdKonfigAmbjente.ToString();

            var formatNrPerKonfig = new clsFormatiKonfig();
            formatNrPerKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(int.Parse(konfigurimi_ComboBox.Value.ToString()));
            if (formatNrPerKonfig.KonfigTrupi.Count == 0)
            {
                var trup1 = new clsFormatKonfigTrup(clsFormatKonfigTrup.defaultFormatSasia, clsFormatKonfigTrup.defaultFormatCmimi, clsFormatKonfigTrup.defaultFormatVlefta, clsFormatKonfigTrup.defaultFormatZbritja, clsFormatKonfigTrup.defaultFormatStringSasia, clsFormatKonfigTrup.defaultFormatStringCmimi, clsFormatKonfigTrup.defaultFormatStringVlefta, clsFormatKonfigTrup.defaultFormatStringZbritja);
                formatNrPerKonfig.KonfigTrupi.Add(trup1);
            }
            var serializusi = new JavaScriptSerializer();
            hfState.Set("formatMonedhe", serializusi.Serialize(formatNrPerKonfig));
            var mon = new colMonedhat();
            mon.mbushGjitheMonedhat(idNdermarrje, idPerdoruesi);
            hfState.Set("formatKurset", serializusi.Serialize(mon));
            hfState.Set("vlerallog", serializusi.Serialize(0));
            hfState.Set("vleraqk", serializusi.Serialize(0));
        }

        private void VendosDataDefault()
        {
            var sot = new DateTime();
            sot = DateTime.Today;
            var periudha = mySessionObjects.merrPeriudheKontabel(Session);
            if (sot >= periudha.FillimiPeriudha && sot <= periudha.MbarimiPeriudha)
                Data_DateEdit.Value = DateTime.Today;
            else
                Data_DateEdit.Value = periudha.FillimiPeriudha;
            DateRegjistrimi_DateEdit.Value = DateTime.Today;
        }

        private void MbushHiddenFieldet(colTrupatFletetKontabel col)
        {
            var serializusi = new JavaScriptSerializer { MaxJsonLength = 50000000 };
            HfColTrup.Value = serializusi.Serialize(col);
            HfColLlog.Value = serializusi.Serialize(col.ktheColLlogari());
            HfColMon.Value = serializusi.Serialize(col.KtheColMonedha());
        }

        //TODO Testing the array
        private void mbushHfFormate(colFormatKonfig col)
        {
            var nrReshtash = col.Count + 1;
            var arrFormatNr = new string[nrReshtash];
            for (var i = 0; i < col.Count; i++)
            {
                //arrFormatNr[i] = col[i].KodiMonedha + ":" + col[i].IdFormatVlefta + ":" + col[i].FormatVlefta;
            }
            hfFormatNr.Value = string.Join("||", arrFormatNr);
        }

        private colTrupatSkematFletetKontabel RuajTrupiSkema(int idNdermarrje)
        {
            var objektivat = new colObjektivaKosto();
            List<double> vleratobjektiva = new List<double>(),
                vleratobjektivamonbaze = new List<double>();
            var idllogobj = new List<int>();
            var serializusi = new JavaScriptSerializer();
            var dokumenti = (object[])serializusi.DeserializeObject(gridDataObject.Value);
            var krijoTrup = colTrupatFletetKontabel.KrijoTrup(idNdermarrje, out objektivat, out vleratobjektiva, out vleratobjektivamonbaze, out idllogobj, dokumenti, Convert.ToBoolean(hfAzhornim.Value), Data_DateEdit.Date);
            if (krijoTrup.Item2)
                clsMenuInfo.ShtoMesazhInformues(MenuInfo, MessagesResource.Messages["msgKursiRiNdryshonShumeMeKursinMePare"], pnlMesazhi);
            var coltrupatskema = new colTrupatSkematFletetKontabel();
            coltrupatskema.AddRange(krijoTrup.Item1.Select(t => new clsTrupiSkemaFleteKontabel
            {
                IdLlogari = t.IdLlogari,
                IdMonedha = t.IdMonedha,
                Kursi = t.Kursi,
                Pershkrimi = t.PershkrimTrupiFleteKontabel,
                VleftaDebi = t.VleftaDebiTrupiFleteKontabel,
                VleftaKredi = t.VleftaKrediTrupiFleteKontabel,
                VleftaDebiMon = t.VleftaDebiMonBazeTrupiFleteKontabel,
                VleftaKrediMon = t.VleftaKrediMonBazeTrupiFleteKontabel
            }));
            return coltrupatskema;
        }

        /// <summary>
        /// kontrollon nese ky kod per filtra ekziston
        /// </summary>
        /// <param name="source"></param>
        /// <param name="args"></param>
        protected void Kodi_CustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = true;
            using (var dbKontabiliteti = new clsDatabaseKontabilitet())
            {
                if (dbKontabiliteti.ekzistonKokaSkemaFleteKontabel(Kodi_ASPxTextBox.Text, mySessionObjects.merrIdNdermarrjeSesioni(Session)))
                    args.IsValid = false;
            }
        }

        private void RuajFleteKontabel(int idNdermarrje, int statusDokumenti, bool kontabilizuar)
        {
            if (Page.IsValid == false)
                return;
            var tedrejtaInfo = new clsTeDrejtaRoli();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje, mySessionObjects.ktheIdVitNdermarrje(Session), "Shto_FleteKontabel.aspx");
            if (statusDokumenti == 1 && !tedrejtaInfo.DShtim || statusDokumenti == 0 && !tedrejtaInfo.DShtimDraft)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                hfStatusRuajtje.Value = "false";
                return;
            }
            var mesazh = new clsMesazh();
            mesazh = IsValidFleteKontabel(statusDokumenti);
            if (mesazh.Status)
            {
                var idGrupKontabilizimi = txtNrGrupKontabilizimi.Text != ""
                    ? clsGrupKontabilizimi.mbushIDGrupKontabilizim(txtNrGrupKontabilizimi.Text, idNdermarrje)
                    : 0;
                var serializusi = new JavaScriptSerializer();
                var dokumenti = (object[])serializusi.DeserializeObject(gridDataObject.Value);
                var idNdervit = mySessionObjects.ktheNdermarrjeVit(Session);
                var idPerdorues = mySessionObjects.ktheIdPerdoruesi(Session);
                var idPeriudha = mySessionObjects.merrPeriudheKontabel(Session).IdPeriudha;
                var idGjuha = mySessionObjects.ktheGjuhe(Session);

                var controls = this.GetAsPxTextEditIdValue();
                controls.AddRange(ASPxSplitter1.GetAsPxTextEditIdValue());

                hfNrAuto = (ASPxHiddenField)NrAuto.VendosVleratNrAuto(hfNrAuto, controls);
                hfNrAutoShitje = (ASPxHiddenField)NrAuto.ShtoNeRegjistrime(hfNrAutoShitje, hfNrAuto, "txtNrDokumenti", "NrDokumentiKokaFleteKontabel");

                var ruaj = clsKokaFleteKontabel.RuajFleteKontabel(idNdermarrje, idNdervit, idPerdorues, statusDokumenti,
                    kontabilizuar, dokumenti, bool.Parse(hfAzhornim.Value), Data_DateEdit.Date,
                    DateRegjistrimi_DateEdit.Date, konfigurimi_ComboBox.Text, txtNrDokumenti.Text, txtNrReference.Text,
                    txtPershkrimi.Text, idPeriudha, this, hfNrAutoShitje, idGrupKontabilizimi,
                    hfShtimModifikim.Value, hfLidhur.Value, int.Parse(hfId.Value), idGjuha);
                if (ruaj.Item1.Status)
                {
                    hfId.Value = ruaj.Item3.ToString();
                    hfqkmesazhi.Value = ruaj.Item4;
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, ruaj.Item1.PershkrimMesazhi, pnlMesazhi);
                    hfStatusRuajtje.Value = "true";
                    hfShtimModifikim.Value = "shtim";
                    PastroPanelLidhur();
                    hfNrRef.Value = txtNrReference.Text = clsKokaFleteKontabel.GjeneroNrReference(idNdervit).ToString();
                    hfIdKonfigAmbjenteQK.Value = ruaj.Item6.ToString();
                }
                else
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, ruaj.Item1.PershkrimMesazhi, pnlMesazhi);
                    hfStatusRuajtje.Value = "false";
                }
            }
            else
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                hfStatusRuajtje.Value = "false";
            }
        }

        private void PastroPanelLidhur()
        {
            try
            {
                hl = new HtmlTable(); //boshatis linkun
                pnlLidhur.Update();
            }
            catch (Exception err)
            {
                LogManager.GetCurrentClassLogger().Error(err.Message);
            }
        }

        /// <summary>
        /// Pastron fushat
        /// </summary>
        private void PastroFusha()
        {
            txtNrDokumenti.Text = "";
            txtPershkrimi.Text = "";
            Data_DateEdit.Text = "";
            DateRegjistrimi_DateEdit.Text = "";
            txtKrediMonedhaBaze.Text = "0";
            txtKrediDiferenca.Text = "0";
            txtDebiMonedhaBaze.Text = "0";
            txtDebiDiferenca.Text = "0";
            lblKodiSkemaKontabel.Text = "";
            txtNrGrupKontabilizimi.Text = "";
            hfDebi.Value = "";
            hfEmerLlogaria.Value = "";
            hfGrupKontabilizimi.Value = "";
            hfKredi.Value = "";
            hfKursi.Value = "";
            hfMonedha.Value = "";
            hfNrLlogaria.Value = "";
            hfPershkrimi.Value = "";
            hfDebiMon.Value = "";
            hfKrediMon.Value = "";
            hfSkemaKontabel.Value = "";
            txtNrReference.Text = clsKokaFleteKontabel.GjeneroNrReference(mySessionObjects.ktheNdermarrjeVit(Session)).ToString();
        }

        private clsMesazh IsValidFleteKontabel(int draft)
        {
            if (DateRegjistrimi_DateEdit.Text == "")
                return new clsMesazh(false, MessagesResource.Messages["msgZgjidhniNjeDateRegjstrimi"]);
            if (Data_DateEdit.Text == "")
                return new clsMesazh(false, MessagesResource.Messages["msgZgjidhniNjeDateDokumenti"]);
            if (Data_DateEdit.Date.Year != new clsNdermarrjeViti(mySessionObjects.ktheNdermarrjeVit(Session)).Viti)
                return new clsMesazh(false, MessagesResource.Messages["msgDataNukPerketVititUshtrimor"]);
            var periudha = mySessionObjects.merrPeriudheKontabel(Session);
            string mesazhGabimi;

            if (!clsFunksione.checkPeriudheKontabel(out mesazhGabimi, Data_DateEdit.Date, periudha, draft))
                return new clsMesazh(false, mesazhGabimi);

            int idKonfig = clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(konfigurimi_ComboBox.Text, IdNdermarrja);
            if (DbCore.MbylljePeriudhe.PeriodClosing.IsPeriodClosed(Data_DateEdit.Date, MyConnectionsManager.GetSelectedConNameServer(), IdNdermarrja, DbCore.DbRegjistrim.KategoriDokumenti.FleteKontabel, idKonfig))
                return new MesazhGabimi(MessagesResource.Messages["msgPeriodIsClosed"]);

            return new clsMesazh(true, MessagesResource.Messages["msgValidimetUKryenMeSukses"]);
        }

        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {
            switch (e.Item.Name)
            {
                case "PrintPreview":
                    if (hfShtimModifikim.Value == "modifikim")
                    {
                        var id = hfId.Value;
                        var clskokaFlete = new clsKokaFleteKontabel(int.Parse(id));
                        var konf = new clsKonfigurimAmbjenti();
                        konf.mbushKonfiguriminMeID(clskokaFlete.IdKonfigAmbjente);
                        Container.Attributes["src"] = "RaportiShpejte.aspx?Sesioni=false&emriReal=fletaKontabel&idDokumenti=" + clskokaFlete.IdKokaFleteKontabel + "&printo=false";
                    }
                    else
                    {
                        Page.Validate();
                        RuajFleteKontabel(mySessionObjects.merrIdNdermarrjeSesioni(Session), 1, false);
                        MbushHiddenFieldet(new colTrupatFletetKontabel());
                    }
                    break;
                case "Ruaj":
                    Page.Validate();
                    RuajFleteKontabel(mySessionObjects.merrIdNdermarrjeSesioni(Session), 1, false);// 1= statusi i dokumentit
                    break;
                case "Draft":
                    Page.Validate();
                    RuajFleteKontabel(mySessionObjects.merrIdNdermarrjeSesioni(Session), 0, false);// 0 = statusi i dokumentit(ne kete rast statusi eshte draft)
                    break;
                case "Kontabilizo":
                    Page.Validate();
                    RuajFleteKontabel(mySessionObjects.merrIdNdermarrjeSesioni(Session), 1, true);
                    break;
                case "Shto":
                    PastroFusha();
                    break;
                case "Anullo":
                    Response.Redirect("FleteKontabel.aspx");
                    break;
            }
        }

        /// <summary>
        /// ben fshirjen e rreshtave te selektuar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            var id = int.Parse(hfId.Value);
            var clsKoka = new clsKokaFleteKontabel(id);
            var mesazhi = new clsMesazh();
            var lidhur = clsKoka.EshteILidhur();
            if (clsKoka.DateDokumentiKokaFleteKontabel.Year != new clsNdermarrjeViti(mySessionObjects.ktheNdermarrjeVit(Session)).Viti)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgDataNukPerketVititUshtrimor"], pnlMesazhi);
                return;
            }
            if (lidhur == false)
            {
                var idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
                var ekycur = clsPeriudhaKontabel.eshteKycurPeriudheSipasDateDheNdermarrjes(clsKoka.DateDokumentiKokaFleteKontabel, idNdermarrje);
                if (ekycur)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgPeriudhaEKycur"], pnlMesazhi);
                    return;
                }

                if (DbCore.MbylljePeriudhe.PeriodClosing.IsPeriodClosed(clsKoka.DateDokumentiKokaFleteKontabel, MyConnectionsManager.GetSelectedConNameServer(), idNdermarrje, KategoriDokumenti.FleteKontabel, clsKoka.IdKonfigAmbjente))
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgPeriodIsClosed"], pnlMesazhi);
                    return;
                }
                mesazhi = clsKoka.Fshiupd();
            }
            else
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["vepBankaMsgDokEshteILidhurNukFshihet"], pnlMesazhi);
                return;
            }
            if (mesazhi.Status)
                Response.Redirect("FleteKontabel.aspx?fshi=po&mesazh=" + MessagesResource.Messages["msgFshirjaPerfundoiMeSukses"]);
            else
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhi.PershkrimMesazhi, pnlMesazhi);
        }


        private void PlotesoGrideSipasSkemes()
        {
            var itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            var cmbFiltra = ((MenuFilter)itemButton.Template).FindControl("btnFiltra") as ASPxComboBox;
            var koka = new clsKokaSkemaFleteKontabel(cmbFiltra.Text, mySessionObjects.merrIdNdermarrjeSesioni(Session));

            var trupat = new colTrupatFletetKontabel();
            koka.OColTrupi = new colTrupatSkematFletetKontabel(koka.IdKokaSkemaFK);
            if (koka.OColTrupi == null || koka.OColTrupi.Count == 0) return;
            foreach (var oSkemaKontabelTrupi in koka.OColTrupi)
            {
                var trupi = new clsTrupiFleteKontabel
                {
                    IdLlogari = oSkemaKontabelTrupi.IdLlogari,
                    NrLlogari = oSkemaKontabelTrupi.NrLlogari,
                    EmerLlogari = oSkemaKontabelTrupi.EmerLlogari,
                    PershkrimTrupiFleteKontabel = oSkemaKontabelTrupi.Pershkrimi,
                    IdMonedha = oSkemaKontabelTrupi.IdMonedha,
                    KodMonedha = oSkemaKontabelTrupi.KodMonedha
                };
                var kurs = new clsKurset(oSkemaKontabelTrupi.IdMonedha, Data_DateEdit.Date);
                trupi.Kursi = kurs.VleraKursi;
                trupi.VleftaDebiTrupiFleteKontabel = oSkemaKontabelTrupi.VleftaDebi;
                trupi.VleftaKrediTrupiFleteKontabel = oSkemaKontabelTrupi.VleftaKredi;
                trupi.VleftaDebiMonBazeTrupiFleteKontabel = oSkemaKontabelTrupi.VleftaDebi * kurs.VleraKursi;
                trupi.VleftaKrediMonBazeTrupiFleteKontabel = oSkemaKontabelTrupi.VleftaKredi * kurs.VleraKursi;
                trupat.Add(trupi);
            }
            MbushHiddenFieldet(trupat);
        }

        protected void ASPxCallbackPanel_Callback1(object sender, CallbackEventArgsBase e)
        {
            switch (e.Parameter)
            {
                case "filtra":
                    PlotesoGrideSipasSkemes();
                    break;
                case "azhornim":
                    var rm = new ResourceManager("Resources.Strings", Assembly.Load("App_GlobalResources"));
                    var koka = new clsKokaFleteKontabel();
                    var konf = new clsKonfigurimAmbjenti();
                    konf.mbushKonfigAmbjSipasKod("NKM", mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    var idPeriudhaKontabel = clsPeriudhaKontabel.ktheIdPeriudheSipasDatesDheNdermarrjes(Data_DateEdit.Date, mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    koka = clsKokaFleteKontabel.GjeneroKontabilizimFleteAzhornim(mySessionObjects.merrIdNdermarrjeSesioni(Session), mySessionObjects.ktheNdermarrjeVit(Session), Data_DateEdit.Date, mySessionObjects.ktheIdPerdoruesi(Session), konf.IdKonfigAmbjente, idPeriudhaKontabel, rm, mySessionObjects.ktheCultureInfo(Session));
                    if (koka.OColTrupi.Count == 0)
                        koka.OColTrupi.Add(new clsTrupiFleteKontabel());
                    MbushHiddenFieldet(koka.OColTrupi);
                    break;
            }
        }

        public void btnPeriudha_TextChanged(object sender, EventArgs e) => VendosDataDefault();

        public void btnJo_Click(object sender, EventArgs e) { }

        public void btnPo_Click(object sender, EventArgs e) { }

        protected void cmbQendraKosto_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (!IsCallback || !Request.Params["__CALLBACKID"].Contains("cmbQendraKosto")) return;
            if (cmbLloji.Value.ToString() == "1")
                ConfigureAspxComboBox.mbushComboQendraKostoBij(mySessionObjects.merrIdNdermarrjeSesioni(Session), cmbQendraKosto);
            else
                ConfigureAspxComboBox.mbushComboSkemaQendraKosto(mySessionObjects.merrIdNdermarrjeSesioni(Session), cmbQendraKosto);
        }
        protected void cmbQendraKosto_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("cmbQendraKosto"))
            {
                if (string.IsNullOrWhiteSpace(e.Filter)) return;
                if (cmbLloji.Value.ToString() == "1")
                {
                    DbCore.DbQendraKosto.colQendraKosto col = new DbCore.DbQendraKosto.colQendraKosto();
                    col.mbushGjitheQendraKostoBijSipasNdermarjesAktiv(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    var dsReal = col.Where(x => x.Kodi.IndexOf(e.Filter, StringComparison.InvariantCultureIgnoreCase) > -1).Skip(e.BeginIndex).Take(e.EndIndex + 1);
                    cmbQendraKosto.TextField = "Kodi";
                    cmbQendraKosto.ValueField = "Id";
                    cmbQendraKosto.DataSource = dsReal.ToList();
                    cmbQendraKosto.DataBind();
                }
                else
                {
                    DbCore.DbQendraKosto.colKokaSkemaQK col = new DbCore.DbQendraKosto.colKokaSkemaQK();
                    col.mbushGjitheSkematSipasNdermarjes(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    var dsReal = col.Where(x => x.Kodi.IndexOf(e.Filter, StringComparison.InvariantCultureIgnoreCase) > -1).Skip(e.BeginIndex).Take(e.EndIndex + 1);
                    cmbQendraKosto.TextField = "Kodi";
                    cmbQendraKosto.ValueField = "IdKoka";
                    cmbQendraKosto.DataSource = dsReal.ToList();
                    cmbQendraKosto.DataBind();
                }
            }
        }

        protected void cmbObjektiva_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (!IsCallback || !Request.Params["__CALLBACKID"].Contains("cmbObjektiva")) return;
            ConfigureAspxComboBox.KonfiguroComboBoxObjektivaKosto(cmbObjektiva, IdNdermarrja);
        }

        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
        }
    }
}