using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Drawing;
using DevExpress.Web;
using AjaxControlToolkit;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using PlatinumWeb.Templates;
using DbCore;
using DbCore.DbQendraKosto;
using DbCore.DbKontabiliteti;
using DbCore.DbShare;
using System.Globalization;
using System.Resources;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class Shto_RegjistrimQendraKosto : MyPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                DbCore.clsFunksione.logout(Session,true,"FaqePaautorizuar");
            }
            int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);

            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerdoruesi);
                return;
            }
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            percaktoTemplateMenu(idPerdoruesi, idViti, idNdermarrje, ASPxMenu1);
            if (!Page.IsPostBack)
            {
                EmratEKontrolleve(rm, ci);
                int idgjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
                hfState.Set("idGjuha", idgjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("idPerdoruesi", idPerdoruesi);
                int idNderViti = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
                if (String.IsNullOrEmpty(Request.QueryString["shtim_modifikim"]))
                {
                    hfShtimModifikim.Value = "shtim";
                    konfiguroVleraFillestareShto(idgjuha, idNderViti, idPerdoruesi, idNdermarrje, rm, ci);
                }
                else
                    if (Request.QueryString["shtim_modifikim"] == "shtim")
                    {
                        hfShtimModifikim.Value = "shtim";
                        konfiguroVleraFillestareShto(idgjuha, idNderViti, idPerdoruesi, idNdermarrje, rm, ci);
                    }
                    else
                        if (Request.QueryString["shtim_modifikim"] == "modifikim")
                        {
                            hfShtimModifikim.Value = "modifikim";
                            konfiguroVleraFillestareModifiko(idgjuha, idNderViti, idNdermarrje, idPerdoruesi, rm, ci);
                        }
                        else
                            if (Request.QueryString["shtim_modifikim"] == "klonim")
                            {
                                hfShtimModifikim.Value = "klonim";
                                konfiguroVleraFillestareModifiko(idgjuha, idNderViti, idNdermarrje, idPerdoruesi, rm, ci);
                            }

                percaktoTemplateMenu(idPerdoruesi, idViti, idNdermarrje, ASPxMenu1);
                hfMonedhaNder.Value = DbCore.DbAdmin.clsMonedha.ktheMonedhenENdermarrjes(idNdermarrje);
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, "Shto_RegjistrimQendraKosto.aspx");
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                hfTeDrejta.Add("ShtimDraft", tedrejtaInfo.DShtimDraft);
                hfTeDrejta.Add("ModifikimDraft", tedrejtaInfo.DModifikimDraft);
            }
            AspxWebControlUtils.perkthePopUp(popFshi, rm.GetString("labelKujdes", ci), lblMsgbox, rm.GetString("labelAdministrimiMsgJeniSigurt", ci), ButtonCancel, rm.GetString("labelAnullo", ci));
        }

        private void EmratEKontrolleve(System.Resources.ResourceManager rm, System.Globalization.CultureInfo cultinf)
        {
            konfigurimi_Label.Text = rm.GetString("lblModeli", cultinf);
            ASPxPopupControl1.HeaderText = rm.GetString("headerPopUpText", cultinf);
            hfState.Set("msgZgjidhDokumentin", rm.GetString("msgZgjidhDokumentin", cultinf));
            hfState.Set("msgGabimGjateTransferimitTeTeDhenave", rm.GetString("msgGabimGjateTransferimitTeTeDhenave", cultinf));
            hfState.Set("msgVendosniNumrinEDokumentit", rm.GetString("msgVendosniNumrinEDokumentit", cultinf));
            hfState.Set("msgZgjidhniNjeDateDokumenti", rm.GetString("msgZgjidhniNjeDateDokumenti", cultinf));
            hfState.Set("msgZgjidhniNjeDateRegjstrimi", rm.GetString("msgZgjidhniNjeDateRegjstrimi", cultinf));
            hfState.Set("msgVleftaMonBazeDuhetTeJeteNumerPozitiv", rm.GetString("msgVleftaMonBazeDuhetTeJeteNumerPozitiv", cultinf));
            hfState.Set("msgVleftaMonBazeDuhetTeJeteNumer", rm.GetString("msgVleftaMonBazeDuhetTeJeteNumer", cultinf));
            hfState.Set("msgVleftaEQKDuheTeJeteNumerPozitiv", rm.GetString("msgVleftaEQKDuheTeJeteNumerPozitiv", cultinf));
            hfState.Set("msgVleftaEQKDuhetTeJeteNumer", rm.GetString("msgVleftaEQKDuhetTeJeteNumer", cultinf));
            hfState.Set("msgVleftaDuheTeJeteNumerPozitiv", rm.GetString("msgVleftaDuheTeJeteNumerPozitiv", cultinf));
            hfState.Set("msgVleftaDuhetTeJeteNumer", rm.GetString("msgVleftaDuhetTeJeteNumer", cultinf));
            hfState.Set("msgZgjidhniLlogarine", rm.GetString("msgZgjidhniLlogarine", cultinf));
            hfState.Set("msgZgjidhniObjektivenEKostos", rm.GetString("msgZgjidhniObjektivenEKostos", cultinf));
            hfState.Set("headerPopUpZgjidhQendrenKostos", rm.GetString("headerPopUpZgjidhQendrenKostos", cultinf));
            hfState.Set("msgLlogariaNukEshtePerTuShperndareNeQendraKosto", rm.GetString("msgLlogariaNukEshtePerTuShperndareNeQendraKosto", cultinf));
            hfState.Set("msgKjoLlogariNukEkziston", rm.GetString("msgKjoLlogariNukEkziston", cultinf));
            hfState.Set("msgKyObjektivNukEshteAktivNeKeteDate", rm.GetString("msgKyObjektivNukEshteAktivNeKeteDate", cultinf));
            hfState.Set("msgKyObjektivNukEshteAktiv", rm.GetString("msgKyObjektivNukEshteAktiv", cultinf));
            hfState.Set("msgKyObjektivNukEziston", rm.GetString("msgKyObjektivNukEziston", cultinf));
            hfState.Set("msgKjoQenderNukEkziston", rm.GetString("msgKjoQenderNukEkziston", cultinf));
            hfState.Set("headerPopUpZgjidhSkemenKostos", rm.GetString("headerPopUpZgjidhSkemenKostos", cultinf));
            hfState.Set("msgPlotesoniTeGjithaFushat", rm.GetString("msgPlotesoniTeGjithaFushat", cultinf));
            hfState.Set("MenuKokeDokumenti", rm.GetString("MenuKokeDokumenti", cultinf));
            hfState.Set("MenuTrupDokumenti", rm.GetString("MenuTrupDokumenti", cultinf));
            hfState.Set("MenuFundDokumenti", rm.GetString("MenuFundDokumenti", cultinf));
            hfState.Set("msgVeprimiNukEshteIKuadruar", rm.GetString("msgVeprimiNukEshteIKuadruar", cultinf));
            hfState.Set("msgTrupiDokNukDuhetBosh", rm.GetString("msgTrupiDokNukDuhetBosh", cultinf));            
        }

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        private void percaktoTemplateMenu(int idPerdoruesi, int idViti, int idNdermarrje, ASPxMenu aSPxMenu1)
        {
            DbCore.DbShare.colMenuItem menu = new DbCore.DbShare.colMenuItem(DbCore.mySessionObjects.ktheGjuhe(Session));
            menu.merrMenuItemSipasKomponentesRegjistrime(DbCore.mySessionObjects.ktheGjuhe(Session), "Shto_RegjistrimQendraKosto.aspx", idPerdoruesi, idNdermarrje, idViti, hfShtimModifikim.Value == "modifikim" ? false : true);

            foreach (DbCore.DbShare.clsMenuItem m in menu)
            {
                if (m.Name != "ItemFilter" && m.Name != "ItemFrame")
                {
                    //if (!m.Enabled) continue;
                    clsToolbarConfig.ShtoMenuItem(this.Theme, aSPxMenu1, m);
                }
                if ((this.hfLidhur.Value == "True") && m.Name == "Kontabilizo")
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                if ((hfShtimModifikim.Value != "modifikim") && m.Name == "Fshi")
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                if ((hfShtimModifikim.Value != "modifikim") && m.Name == "PrintPreview")
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                if ((hfShtimModifikim.Value == "shtim" || this.hfLidhur.Value == "True") && m.Name == "Klono")
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                if (m.Name == "ItemFrame")
                    clsToolbarConfig.ShtoMenuItemPerFrame(this, aSPxMenu1, DbCore.clsFunksione.ktheUrlHelpi(m.UrlHelp).Item1);
                if (m.Name == "Ruaj")
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = true;
                if (m.Name == "Shto" || m.Name == "Kerko" || m.Name == "Pastro" || m.Name == "ItemFilter" || m.Name == "ItemFrame")
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].BeginGroup = true;
                if ((hfLidhur.Value == "True") && m.Name == "Draft")
                {
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                }
            }
            EventHandler handlerPerPo = new EventHandler(btnPo_Click);
            EventHandler handlerPerJo = new EventHandler(btnJo_Click);
            clsMenuInfo.ShtoMenuItemInfo(this, MenuInfo, handlerPerPo, handlerPerJo);
            if (Request.QueryString["id"] != null)
            {
                int idStatusDok = DbCore.DbQendraKosto.clsKokaQendraKosto.KtheIdStatusDokSipasID(int.Parse(Request.QueryString["id"]));
                bool visible = clsFunksione.merrMenuVisibleDraft(hfShtimModifikim.Value == "modifikim" ? false : true, idStatusDok);
                ASPxMenu1.Items.FindByName("Draft").ClientVisible = visible;
            }
        }

        public void btnJo_Click(object sender, EventArgs e) { }
        public void btnPo_Click(object sender, EventArgs e) { }

        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.ktheNdermarrjeVit(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), ASPxMenu1);
        }

        private void konfiguroVleraFillestareShto(int idGjuha, int idNderViti, int idPerdoruesi, int idNdermarrje, System.Resources.ResourceManager rm, System.Globalization.CultureInfo ci)
        {
            AspxWebControlUtils.vendosDateEditMask(this.dteDtDok);
            AspxWebControlUtils.vendosDateEditMask(dteDtRegjistrimi);
            vendosDataDefault();
            int idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            //   DbCore.clsFunksione.mbushComboPeriudhatAktuale(idViti, btnPeriudha);
            ConfigureAspxComboBox.mbushComboKonfigurimetVetemKodiSiTekst(idPerdoruesi, idNdermarrje, this.cmbKonfigurimi, 906, false, idGjuha);
            //vendoset fiks sepse ai eshte konfigurimi default per ambjentin e flets kontabel
            cmbKonfigurimi.SelectedIndex = 0;
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()), idGjuha);
            hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;
            ConfigureAspxComboBox.mbushComboLlojQendre(cmbLloji, false, rm, ci);

            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmbQendraKosto);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmbObjektiva);
            ConfigureAspxComboBox.KonfiguroComboBoxObjektivaKosto(cmbObjektiva, IdNdermarrja);

            int idMonedheZgjedhur = DbCore.clsFunksione.ktheMonedhePerFormatNumri(idGjuha, int.Parse(cmbKonfigurimi.Value.ToString()), idNdermarrje, 906, "", -1, true);
            DbCore.DbShare.clsFormatiKonfig formatNrPerKonfig = new clsFormatiKonfig();
            formatNrPerKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(int.Parse(cmbKonfigurimi.Value.ToString()));
            if (formatNrPerKonfig.KonfigTrupi.Count == 0)
            {
                DbCore.DbShare.clsFormatKonfigTrup trup = new clsFormatKonfigTrup(clsFormatKonfigTrup.defaultFormatSasia, clsFormatKonfigTrup.defaultFormatCmimi, clsFormatKonfigTrup.defaultFormatVlefta, clsFormatKonfigTrup.defaultFormatZbritja, clsFormatKonfigTrup.defaultFormatStringSasia, clsFormatKonfigTrup.defaultFormatStringCmimi, clsFormatKonfigTrup.defaultFormatStringVlefta, clsFormatKonfigTrup.defaultFormatStringZbritja);
                formatNrPerKonfig.KonfigTrupi.Add(trup);
            }
            System.Web.Script.Serialization.JavaScriptSerializer serializusi = new System.Web.Script.Serialization.JavaScriptSerializer();
            hfState.Set("formatMonedhe", serializusi.Serialize(formatNrPerKonfig));
            hfState.Set("idDokGjenerues", 0);
        }

        private void konfiguroVleraFillestareModifiko(int idGjuha, int idNderViti, int idNdermarrje, int idPerdoruesi, System.Resources.ResourceManager rm, System.Globalization.CultureInfo ci)
        {//mbush kombot dhe gridat

            AspxWebControlUtils.vendosDateEditMask(dteDtDok);
            AspxWebControlUtils.vendosDateEditMask(dteDtRegjistrimi);
            int idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            //DbCore.clsFunksione.mbushComboPeriudhatAktuale(idViti, btnPeriudha);
            ConfigureAspxComboBox.mbushComboKonfigurimetVetemKodiSiTekst(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 906, true, idGjuha);

            ConfigureAspxComboBox.mbushComboLlojQendre(cmbLloji, false, rm, ci);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmbQendraKosto);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmbObjektiva);

            int id = int.Parse(Request.QueryString["id"]);

            MerrTedhenat(idViti, idNdermarrje, idPerdoruesi, new DbCore.DbQendraKosto.clsKokaQendraKosto(id), idGjuha);
            int idMonedheZgjedhur = DbCore.clsFunksione.ktheMonedhePerFormatNumri(idGjuha, int.Parse(cmbKonfigurimi.Value.ToString()), idNdermarrje, 906, "", -1, false);
            DbCore.DbShare.clsFormatiKonfig formatNrPerKonfig = new clsFormatiKonfig();
            formatNrPerKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(int.Parse(cmbKonfigurimi.Value.ToString()));
            if (formatNrPerKonfig.KonfigTrupi.Count == 0)
            {
                DbCore.DbShare.clsFormatKonfigTrup trup = new clsFormatKonfigTrup(clsFormatKonfigTrup.defaultFormatSasia, clsFormatKonfigTrup.defaultFormatCmimi, clsFormatKonfigTrup.defaultFormatVlefta, clsFormatKonfigTrup.defaultFormatZbritja, clsFormatKonfigTrup.defaultFormatStringSasia, clsFormatKonfigTrup.defaultFormatStringCmimi, clsFormatKonfigTrup.defaultFormatStringVlefta, clsFormatKonfigTrup.defaultFormatStringZbritja);
                formatNrPerKonfig.KonfigTrupi.Add(trup);
            }

            System.Web.Script.Serialization.JavaScriptSerializer serializusi = new System.Web.Script.Serialization.JavaScriptSerializer();
            hfState.Set("formatMonedhe", serializusi.Serialize(formatNrPerKonfig));
            hfState.Set("vlerallog", serializusi.Serialize(0));
            hfState.Set("vleraqk", serializusi.Serialize(0));
        }

        public void MerrTedhenat(int idViti, int idNdermarrje, int idPerdoruesi, DbCore.DbQendraKosto.clsKokaQendraKosto l, int idGjuha)
        {//funksioni qe mbush fushat
            hfState.Set("id", l.IdKoka);
            hfState.Set("idDokGjenerues", l.IdGjenerues);
            this.txtNrDok.Text = l.NrDok;
            this.dteDtDok.Value = l.DtDok;
            if (hfShtimModifikim.Value == "modifikim")
            {
                if (l.NrRef != 0)
                    this.txtNrRef.Text = l.NrRef.ToString();
            }
            this.txtShenime.Text = l.Pershkrimi;
            this.dteDtRegjistrimi.Value = l.DtRegj;
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasId(l.IdKonfigAmbjente, idGjuha);
            hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;
            this.cmbKonfigurimi.Value = l.IdKonfigAmbjente.ToString();
            DbCore.DbQendraKosto.colTrupiQendraKosto trup = new DbCore.DbQendraKosto.colTrupiQendraKosto(l.IdKoka);
            double debimon = 0;
            double kredimon = 0;
            foreach (DbCore.DbQendraKosto.clsTrupiQendraKosto t in trup)
            {
                if (t.DebiKredi == 1)
                    debimon += t.VleftaMonBaze;
                else kredimon += t.VleftaMonBaze;
            }
            this.txtDebiMonedhaBaze.Text = debimon.ToString("################################0.00");
            this.txtKrediMonedhaBaze.Text = kredimon.ToString("################################0.00"); ;
            if ((debimon - kredimon) > 0)
            {
                this.txtDebiDiferenca.Text = (debimon - kredimon).ToString("################################0.00");
                txtKrediDiferenca.Text = "0.00";
            }
            else
            {
                txtDebiDiferenca.Text = "0.00";
                this.txtKrediDiferenca.Text = (kredimon - debimon).ToString("###############################0.00");
            }

            if (hfShtimModifikim.Value == "modifikim")
            {
                DataTable dtlidhur = l.MerrIdsDokLidhur();
                hfLidhur.Value = (!(dtlidhur.Rows.Count == 0)).ToString();
                AspxWebControlUtils.ShtoLidhje(idPerdoruesi, idViti, idNdermarrje, hl, dtlidhur, l.IdGjenerues, l.IdNivelGjenerues, l.IdKonfigGjenerues, idGjuha);
            }
        }

        private void vendosDataDefault()
        {
            DateTime sot = new DateTime();
            sot = DateTime.Today;
            DbCore.DbAdmin.clsPeriudhaKontabel periudha = DbCore.mySessionObjects.merrPeriudheKontabel(Session);
            if (sot >= periudha.FillimiPeriudha && sot <= periudha.MbarimiPeriudha)
                this.dteDtDok.Value = DateTime.Today;
            else
                this.dteDtDok.Value = periudha.FillimiPeriudha;
            dteDtRegjistrimi.Value = DateTime.Today;
        }

        private void konfiguroGriden()
        {//konfigurohet grida
            DbCore.DbShare.clsKonfigurimAmbjenti konfigurimi = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konfigurimi.mbushKonfigDefaultKomponentes(906, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        }

        //nestila nuk perdoret me ky buton per ruajtjen e skemave por ato ruhen nga menuja siper
        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {

        }

        private bool pastroPanelLidhur()
        {
            try
            {
                hl = new HtmlTable(); //boshatis linkun
                pnlLidhur.Update();
                return true;
            }
            catch (Exception err)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
                return false;
            }
        }

        private void pastroFusha(int idNdermarrje)
        {//pastron fushat
            this.txtNrDok.Text = "";
            this.txtShenime.Text = "";
            this.dteDtDok.Text = "";
            this.dteDtRegjistrimi.Text = "";
            this.txtKrediMonedhaBaze.Text = "0";
            this.txtKrediDiferenca.Text = "0";
            this.txtDebiMonedhaBaze.Text = "0";
            this.txtDebiDiferenca.Text = "0";
            this.hfDebi.Value = "";
            this.hfEmerLlogaria.Value = "";
            this.hfGrupKontabilizimi.Value = "";
            this.hfKredi.Value = "";
            this.hfKursi.Value = "";
            this.hfMonedha.Value = "";
            this.hfNrLlogaria.Value = "";
            hfPershkrimi.Value = "";
            this.hfDebiMon.Value = "";
            this.hfKrediMon.Value = "";
            this.hfSkemaKontabel.Value = "";
        }

        protected void ButtonOk_Click2(object sender, EventArgs e)
        {//ben fshirjen e rreshtave te selektuar

            int id = int.Parse(Request.QueryString["id"]);
            DbCore.DbQendraKosto.clsKokaQendraKosto clsKoka = new DbCore.DbQendraKosto.clsKokaQendraKosto(id);
            DbCore.clsMesazh mesazhi = new DbCore.clsMesazh();


            bool lidhur = clsKoka.EshteILidhur();
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            if (clsKoka.DtDok.Year != new DbCore.DbAdmin.clsNdermarrjeViti(DbCore.mySessionObjects.ktheNdermarrjeVit(Session)).Viti)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgDataNukPerketVititUshtrimor", ci), pnlMesazhi);
                return;
            }
            if (lidhur == false)
            {
                int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                //DbCore.DbAdmin.clsPeriudhaKontabel periudha = new DbCore.DbAdmin.clsPeriudhaKontabel(clsKoka.DtDok, idNdermarrje);
                //DbCore.clsMesazh mesazh = periudha.isPeriudheKycur();
                bool ekycur = DbCore.DbAdmin.clsPeriudhaKontabel.eshteKycurPeriudheSipasDateDheNdermarrjes(clsKoka.DtDok, idNdermarrje);
                if (ekycur)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgPeriudhaEKycur", ci), pnlMesazhi);
                    return;
                }  
                mesazhi = clsKoka.Fshi(DbCore.mySessionObjects.ktheIdPerdoruesi(Session),2);

            }
            else
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgListPagesaDokuEshteILidhurDheNukMundTeFshihet", ci), pnlMesazhi);
                return;
            }
            if (mesazhi.Status)
            {
                Response.Redirect("RegjistrimQendraKosto.aspx?fshi=po&mesazh=" + mesazhi.PershkrimMesazhi);
                return;
            }
            else
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhi.PershkrimMesazhi, pnlMesazhi);

        }



        #region autocomplete
        protected void cmbQendraKosto_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].Contains("cmbQendraKosto"))
                {
                    if (cmbLloji.Value.ToString() == "1")
                        ConfigureAspxComboBox.mbushComboQendraKostoBij(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), cmbQendraKosto);
                    else ConfigureAspxComboBox.mbushComboSkemaQendraKosto(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), cmbQendraKosto);
                }
            }
        }

        protected void cmbQendraKosto_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].Contains("cmbQendraKosto"))
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
                        cmbQendraKosto.ValueField = "Id";
                        cmbQendraKosto.DataSource = dsReal.ToList();
                        cmbQendraKosto.DataBind();
                    }
                        

                }
            }
        }

        protected void cmbObjektiva_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("cmbObjektiva"))
                ConfigureAspxComboBox.KonfiguroComboBoxObjektivaKosto(cmbObjektiva, IdNdermarrja);
        }

        protected void cmbObjektiva_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].Contains("cmbObjektiva"))
                {
                    if (string.IsNullOrWhiteSpace(e.Filter)) return;


                    DbCore.DbQendraKosto.colObjektivaKosto col = new DbCore.DbQendraKosto.colObjektivaKosto();
                    col.mbushGjitheObjketivatKostoSipasNdermarjesAktiv(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    var dsReal = col.Where(x => x.Kodi.IndexOf(e.Filter, StringComparison.InvariantCultureIgnoreCase) > -1).Skip(e.BeginIndex).Take(e.EndIndex + 1);
                    cmbObjektiva.TextField = "Kodi";
                    cmbObjektiva.ValueField = "Id";
                    cmbObjektiva.DataSource = dsReal.ToList();
                    cmbObjektiva.DataBind();
                    

                }
            }
        }
        #endregion autocomplete
    }
}