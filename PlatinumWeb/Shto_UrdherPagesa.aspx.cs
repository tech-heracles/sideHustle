using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Web.Script.Serialization;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Resources;
using System.Globalization;
using DbCore;
using DbCore.DbRegjistrim;
using DbCore.DbShare;
using DbCore.IMBUtils.Extensions;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class Shto_UrdherPagesa : MyPageBase
    {
        //private const string STR_ShumaNukDuhetTeJeteZero = "Shuma nuk duhet të jetë zero!";
        //private const string STR_MungojnëTëDhëna = "Mungojnë të dhëna!";
        //private int idndermarje, idperdoruesi, idnderviti;


       

        /// <summary>
        /// mbush kontrollet me te dhena
        /// </summary>
        /// <param name="koka"></param>
        public void MerrTedhenat(DbCore.DbArkaBanka.clsKokaUrdherPagese koka, int idGjuha)
        {
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasId(koka.IdKonfigAmbjente, idGjuha);
            cmbKonfigurimi.Text = konf.KodKonfigAmbjente;
            hfKonffillestar.Value = String.Format("{0};{1}", konf.KodKonfigAmbjente, konf.PershkrimKonfigAmbjente);
            txtNrDok.Text = koka.NrDok;
            dteDtDok.Date = koka.DtDok;
            txtNrKuponi.Text = koka.NrKuponi;
            txtNrPunonjesve.Text = koka.NrPunonjesve;
            txtEmriPerfitues.Text = koka.EmriPerfituesit;
            txtNipti.Text = koka.Nipti;
            txtEmriBankes.Text = koka.EmriBankes;
            txtNrLlogBankare.Text = koka.NrLlogBankare;
            txtAdresa.Text = koka.Adresa;
            radLlojDok.Value = koka.LlojDokNgjitur.ToString();
            cmbFormatiPrintimit.Value = koka.IdRaportDesing.ToString();
            this.txtNrDokNgjitur.Text = koka.NrDokNgjitur;
            dteDtDokNgjitur.Date = koka.DtDokNgjitur;
            dteDtAprovimi.Date = koka.DtDokAprovimi;
            txtVlefta.Text = koka.Totali.ToString();
            txtUrdheruesi.Text = koka.Urdheruesi;
            txtNenpunesiThesarit.Text = koka.NenpunesThesari;
            txtKontabilisti.Text = koka.Kontabilisti;
            btneArtikulli.Text = "";
            btneKapitulli.Text = "";
            btneGrupi.Text = "";
            btneTitulli.Text = "";
            DataTable dtlidhur = koka.merrIdsDokLidhur();
            hfLidhur.Value = (!(dtlidhur.Rows.Count == 0)).ToString();
            AspxWebControlUtils.ShtoLidhje((int)hfState["idPerdoruesi"], (int)hfState["idViti"], (int)hfState["idNdermarrje"], hl, dtlidhur, koka.IdGjenerues, koka.IdNivelGjenerues, koka.IdKonfigGjenerues, idGjuha);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            int idPerdoruesi;
            int idGjuha;
            int idNdermarrje;
            int idViti;
            int idNdermarrjeVit;
            if (hfState.Count == 0)
            {
                if (!DbCore.mySessionObjects.isLogedIn(Session))
                {
                    DbCore.clsFunksione.logout(Session,true,"FaqePaautorizuar");
                }

                idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
                idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
                idNdermarrjeVit = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
                hfState.Set("idPerdoruesi", idPerdoruesi);
                hfState.Set("idGjuha", idGjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("idViti", idViti);
                hfState.Set("idNdermarrjeVit", idNdermarrjeVit);
                GridUtil.perktheButonaGride(hfState, DbCore.mySessionObjects.ktheCultureInfo(Session));
            }
            else
            {
                idPerdoruesi = (int)hfState["idPerdoruesi"];
                idGjuha = (int)hfState["idGjuha"];
                idNdermarrje = (int)hfState["idNdermarrje"];
                idViti = (int)hfState["idViti"];
                idNdermarrjeVit = (int)hfState["idNdermarrjeVit"];
            }

            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerdoruesi);
            }

            konfigGrid();

            DbCore.DbAdmin.clsPeriudhaKontabel periudha = DbCore.mySessionObjects.merrPeriudheKontabel(Session);
            if (periudha != null)
            {
                lblPeriudhaAktuale.Text = String.Format("{0}-{1}", periudha.FillimiPeriudha.ToShortDateString(), periudha.MbarimiPeriudha.ToShortDateString());
            }

            

            if (!IsPostBack)
            {
                perktheCheckBox(cultinf, rm);
                mbushHiddenFieldMePerkthime(cultinf, rm);
                if (hfShtimModifikim.Value == string.Empty)
                {
                    hfShtimModifikim.Value = String.IsNullOrEmpty(Request.QueryString["shtim_modifikim"]) ? "shtim" : Request.QueryString["shtim_modifikim"];
                }
                switch(hfShtimModifikim.Value)
                {
                    case "shtim":
                        konfiguroVleraFillestareShto(idGjuha, idNdermarrje, idPerdoruesi);
                        break;
                    case "modifikim":
                    case "klonim":
                        konfiguroVleraFillestareModifiko(idGjuha, idNdermarrje, idPerdoruesi);
                        break;
                    default:
                        break;
                }
                //percaktoTemplateMenu(idPerdoruesi, idViti, idNdermarrje, ASPxMenu1);
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, "Shto_UrdherPagesa.aspx");
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                hfTeDrejta.Add("ShtimDraft", tedrejtaInfo.DShtimDraft);
                hfTeDrejta.Add("ModifikimDraft", tedrejtaInfo.DModifikimDraft);
            }
            percaktoTemplateMenu(idPerdoruesi, idViti, idNdermarrje, ASPxMenu1);
            Container.Attributes["src"] = string.Empty;
            AspxWebControlUtils.perkthePopUp(popFshi, rm.GetString("labelKujdes", cultinf), lblMsgbox, rm.GetString("labelAdministrimiMsgJeniSigurt", cultinf), ButtonCancel, rm.GetString("labelAnullo", cultinf));
        }

        /// <summary>
        /// Metode per te perkthyer checkBox
        /// </summary>
        /// <param name="cultinf">Merr culture info</param>
        /// <param name="rm"> Merr resource manager</param>
        private void perktheCheckBox(CultureInfo cultinf, ResourceManager rm) 
        {
            radLlojDok.Items.FindByValue("1").Text = rm.GetString("checkboxRaportCek", cultinf);
            radLlojDok.Items.FindByValue("2").Text = rm.GetString("checkboxRaportXhirim", cultinf);
            radLlojDok.Items.FindByValue("3").Text = rm.GetString("labelTetjera", cultinf);
        }
      

        /// <summary>
        /// Metode qe sherben per te kaluar perkthime te js
        /// </summary>
        /// <param name="cultinf">Merr culture info</param>
        /// <param name="rm">Merr Resource Manager</param>
        private void mbushHiddenFieldMePerkthime(CultureInfo cultinf, ResourceManager rm)
        {
            hfState.Set("MenuKokeDokumenti", rm.GetString("MenuKokeDokumenti", cultinf));
            hfState.Set("MenuTrupDokumenti", rm.GetString("MenuTrupDokumenti", cultinf));
            hfState.Set("MenuFundDokumenti", rm.GetString("MenuFundDokumenti", cultinf));
            hfState.Set("msgZgjidhDokumentin", rm.GetString("msgZgjidhDokumentin", cultinf));
            hfState.Set("msgKodifikimArtikullZgjidhGrup", rm.GetString("msgKodifikimArtikullZgjidhGrup", cultinf));
            hfState.Set("msgZgjidhniTitullin", rm.GetString("msgZgjidhniTitullin", cultinf));
            hfState.Set("msgZgjidhniKapitullin", rm.GetString("msgZgjidhniKapitullin", cultinf));
            hfState.Set("msgZgjidhniLlogarine", rm.GetString("msgZgjidhniLlogarine", cultinf));
            hfState.Set("msgZgjidhniLlojin", rm.GetString("msgZgjidhniLlojin", cultinf));
            hfState.Set("msgShenoniNumrinEDokumentit", rm.GetString("msgShenoniNumrinEDokumentit", cultinf));
            hfState.Set("msgZgjidhniNjeDateDokumenti", rm.GetString("msgZgjidhniNjeDateDokumenti", cultinf));
            hfState.Set("msgZgjidhniNjeDateDokumentiNgjitur", rm.GetString("msgZgjidhniNjeDateDokumentiNgjitur", cultinf));
            hfState.Set("msgZgjidhniNjeDateAprovimi", rm.GetString("msgZgjidhniNjeDateAprovimi", cultinf));
            hfState.Set("msgZgjidhniNjeDateAprovimi", rm.GetString("msgZgjidhniNjeDateAprovimi", cultinf));
            hfState.Set("msgTrupiDokumentitNukDuhetLeneBosh", rm.GetString("msgTrupiDokumentitNukDuhetLeneBosh", cultinf));
            hfState.Set("msgPlotesoniTeGjithaFushat", rm.GetString("msgPlotesoniTeGjithaFushat", cultinf));
            hfState.Set("msgGabimiLlogariKategoriShpenzimi", rm.GetString("msgGabimiLlogariKategoriShpenzimi", cultinf));


        }

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        private void percaktoTemplateMenu(int idPerdoruesi, int idViti, int idNdermarrje, ASPxMenu aSPxMenu1)
        {
            DbCore.DbShare.colMenuItem menu = new DbCore.DbShare.colMenuItem((int)hfState["idGjuha"]);
            menu.merrMenuItemSipasKomponentesRegjistrime((int)hfState["idGjuha"], "Shto_UrdherPagesa.aspx", idPerdoruesi, idNdermarrje, idViti, hfShtimModifikim.Value == "modifikim" ? false : true);

            foreach (DbCore.DbShare.clsMenuItem m in menu)
            {
                if (m.Name != "ItemFilter" && m.Name != "ItemFrame")
                {
                    //if (!m.Enabled) continue;
                    clsToolbarConfig.ShtoMenuItem(this.Theme, aSPxMenu1, m);
                }
                if ((hfLidhur.Value == "True") && m.Name == "Draft")
                {
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                }
                if ((hfShtimModifikim.Value != "modifikim") && m.Name == "Fshi")
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                if ((hfShtimModifikim.Value != "modifikim") && m.Name == "PrintPreview")
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;

                if (m.Name == "Ruaj")
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = true;
                if (m.Name == "ItemFrame")
                    clsToolbarConfig.ShtoMenuItemPerFrame(this, aSPxMenu1, DbCore.clsFunksione.ktheUrlHelpi(m.UrlHelp).Item1);
                if (m.Name == "Shto" || m.Name == "Kerko" || m.Name == "Pastro" || m.Name == "ItemFilter" || m.Name == "ItemFrame")
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].BeginGroup = true;
                if(hfShtimModifikim.Value=="shtim" && m.Name == "Klono")
                {
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                }
            }
            clsMenuInfo.ShtoMenuItemInfo(this, MenuInfo);
        }

        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu((int)hfState["idPerdoruesi"], (int)hfState["idViti"], (int)hfState["idNdermarrje"], ASPxMenu1);
        }

        /// <summary>
        /// vendos datat dhe mujin sipas periudhes
        /// </summary>
        private void vendosDataDefault()
        {
            DateTime sot = new DateTime();
            sot = DateTime.Today;
            DbCore.DbAdmin.clsPeriudhaKontabel periudha = DbCore.mySessionObjects.merrPeriudheKontabel(Session);
            if (sot >= periudha.FillimiPeriudha && sot <= periudha.MbarimiPeriudha)
                this.dteDtDok.Value = DateTime.Today;
            else
                dteDtDok.Value = periudha.FillimiPeriudha;
            dteDtDokNgjitur.Value = sot;
            lblDtAprovimi.Value = sot;
        }

        /// <summary>
        /// Vendos vlerat default kur po behet shtim
        /// </summary>
        /// <param name="idGjuha"></param>
        private void konfiguroVleraFillestareShto(int idGjuha, int idNdermarrje, int idPerdoruesi)
        {
            AspxWebControlUtils.vendosDateEditMask(dteDtDokNgjitur);
            AspxWebControlUtils.vendosDateEditMask(dteDtAprovimi);
            AspxWebControlUtils.vendosDateEditMask(dteDtDok);
            vendosDataDefault();
            mbushComboKonfigurimet(false, idGjuha);
            ConfigureAspxComboBox.mbushComboFormatPrintimi(cmbFormatiPrintimit, 39, idNdermarrje);
            cmbFormatiPrintimit.SelectedIndex = 0;
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(btneGrupi);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(btneKapitulli);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(btneArtikulli);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(btneTitulli);
            ConfigureAspxComboBox.KonfiguroComboBoxSipasLlojitKonfigUrdherPagese(idNdermarrje, btneGrupi, Convert.ToInt32(DbCore.DbArkaBanka.LlojeKonfigurimeUrdherPagese.Grup));
            ConfigureAspxComboBox.KonfiguroComboBoxSipasLlojitKonfigUrdherPagese(idNdermarrje, btneTitulli, Convert.ToInt32(DbCore.DbArkaBanka.LlojeKonfigurimeUrdherPagese.Titull));
            ConfigureAspxComboBox.KonfiguroComboBoxSipasLlojitKonfigUrdherPagese(idNdermarrje, btneKapitulli, Convert.ToInt32(DbCore.DbArkaBanka.LlojeKonfigurimeUrdherPagese.Kapitull));
            ConfigureAspxComboBox.mbushComboLlogariaPaKolona(idPerdoruesi, idNdermarrje, btneArtikulli);
            //txtVlefta.Text = "0.00";
            DbCore.DbShare.clsFormatiKonfig formatNrKonfig = new DbCore.DbShare.clsFormatiKonfig();
            formatNrKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(int.Parse(cmbKonfigurimi.Value.ToString()));
            int idMonedheZgjedhur = DbCore.clsFunksione.ktheMonedhePerFormatNumri(idGjuha, int.Parse(cmbKonfigurimi.Value.ToString()), idNdermarrje, 313, "", -1, true);
            DbCore.DbShare.clsFormatiKonfig formatNrPerKonfig = new DbCore.DbShare.clsFormatiKonfig();
            formatNrPerKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(int.Parse(cmbKonfigurimi.Value.ToString()));
            DbCore.DbShare.clsFormatKonfigTrup formatMonedhe = clsFormatKonfigTrup.ktheFormatNumriMonedhe(formatNrPerKonfig, idMonedheZgjedhur);
            System.Web.Script.Serialization.JavaScriptSerializer serializusi = new System.Web.Script.Serialization.JavaScriptSerializer();
            hfState.Set("formatMonedhe", serializusi.Serialize(formatMonedhe));
            txtVlefta.Text = DbCore.clsFunksione.krijoNumer(formatMonedhe.ShifraPasPresjesVlefta, "0");
        }

        /// <summary>
        /// Vendos vlerat default kur po behet modifikim
        /// </summary>
        /// <param name="idGjuha"></param>
        private void konfiguroVleraFillestareModifiko(int idGjuha, int idNdermarrje, int idPerdoruesi)
        {//mbush kombot dhe gridat
            AspxWebControlUtils.vendosDateEditMask(dteDtDok);
            AspxWebControlUtils.vendosDateEditMask(dteDtDokNgjitur);
            AspxWebControlUtils.vendosDateEditMask(dteDtAprovimi);
            //txtVlefta.Text = "0.00";
            mbushComboKonfigurimet(true, idGjuha);
            ConfigureAspxComboBox.mbushComboFormatPrintimi(cmbFormatiPrintimit, 39, idNdermarrje);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(btneGrupi);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(btneKapitulli);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(btneArtikulli);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(btneTitulli);
            ConfigureAspxComboBox.KonfiguroComboBoxSipasLlojitKonfigUrdherPagese(idNdermarrje, btneTitulli, Convert.ToInt32(DbCore.DbArkaBanka.LlojeKonfigurimeUrdherPagese.Titull));
            ConfigureAspxComboBox.KonfiguroComboBoxSipasLlojitKonfigUrdherPagese(idNdermarrje, btneGrupi, Convert.ToInt32(DbCore.DbArkaBanka.LlojeKonfigurimeUrdherPagese.Grup));
            ConfigureAspxComboBox.KonfiguroComboBoxSipasLlojitKonfigUrdherPagese(idNdermarrje, btneKapitulli, Convert.ToInt32(DbCore.DbArkaBanka.LlojeKonfigurimeUrdherPagese.Kapitull));
            ConfigureAspxComboBox.mbushComboLlogariaPaKolona(idPerdoruesi, idNdermarrje, btneArtikulli);
            int id = int.Parse(Request.QueryString["id"]);
            DbCore.DbArkaBanka.clsKokaUrdherPagese kok = new DbCore.DbArkaBanka.clsKokaUrdherPagese(id);
            if (kok != null)
            {
                MerrTedhenat(kok, idGjuha);
            }
            mbushUrdhereRegjistrimTrupiModifiko(kok);
            int idMonedheZgjedhur = DbCore.clsFunksione.ktheMonedhePerFormatNumri(idGjuha, int.Parse(cmbKonfigurimi.Value.ToString()), idNdermarrje, 313, "", -1, false);
            DbCore.DbShare.clsFormatiKonfig formatNrPerKonfig = new DbCore.DbShare.clsFormatiKonfig();
            formatNrPerKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(int.Parse(cmbKonfigurimi.Value.ToString()));
            DbCore.DbShare.clsFormatKonfigTrup formatMonedhe = clsFormatKonfigTrup.ktheFormatNumriMonedhe(formatNrPerKonfig, idMonedheZgjedhur);
            System.Web.Script.Serialization.JavaScriptSerializer serializusi = new System.Web.Script.Serialization.JavaScriptSerializer();
            hfState.Set("formatMonedhe", serializusi.Serialize(formatMonedhe));
        }

        /// <summary>
        /// Merr trupin ekzistues te dokumentit  qe po modifikohet
        /// </summary>
        private void mbushUrdhereRegjistrimTrupiModifiko(DbCore.DbArkaBanka.clsKokaUrdherPagese koka)
        {//mbush griden me te dhenat
            mbushHiddenFieldet(koka.OColTrupi);
        }

        /// <summary>
        /// mbudh hiddenfieldet me objektet e trupit
        /// </summary>
        /// <param name="col"></param>
        private void mbushHiddenFieldet(DbCore.DbArkaBanka.colTrupiUrdherPagese col)
        {
            JavaScriptSerializer serializusi = new JavaScriptSerializer() { MaxJsonLength = 50000000 };
            HfColTrupMag.Value = serializusi.Serialize(col);
            HfColNjesiArt.Value = serializusi.Serialize(col.ktheGrupe());
            HfColArt.Value = serializusi.Serialize(col.ktheKapituj());
            HfColDetArt.Value = serializusi.Serialize(col.ktheTituj());
            HfColNjesAdminis.Value = serializusi.Serialize(col.ktheLlogari());
            HfColNjesAdminisDest.Value = serializusi.Serialize(col.ktheLlogariAnaliza());
        }

        /// <summary>
        /// kthen konfigurimin e grides
        /// </summary>
        private void konfigGrid()
        {
            const string emriKomponentes = "Shto_UrdherPagesa.aspx";
            DbCore.DbAdmin.clsKomponente oKomponente = new DbCore.DbAdmin.clsKomponente(emriKomponentes);
            DbCore.DbShare.clsKonfigurimAmbjenti konfigurimi = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konfigurimi.mbushKonfigDefaultKomponentes(oKomponente.IdKomponente, (int)hfState["idNdermarrje"]);
            DbCore.DbAdmin.colGridaTrupi trupiGrides = new DbCore.DbAdmin.colGridaTrupi(oKomponente.IdKomponente, konfigurimi.IdKonfigAmbjente, (int)hfState["idGjuha"]);
            JavaScriptSerializer serializusi = new JavaScriptSerializer();
            HfGridCol.Value = serializusi.Serialize(trupiGrides);
        }
        
        private void mbushComboKonfigurimet(bool mod, int idGjuha)
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            DbCore.DbShare.colKonfigurimAmbjenti colKonfig = new DbCore.DbShare.colKonfigurimAmbjenti();
            const int idKategori = 39;//Urdher pagesa
            colKonfig.mbushKonfigAmbjSipasIdKategori(idKategori, (int)hfState["idNdermarrje"], (int)hfState["idPerdoruesi"], idGjuha);
            cmbKonfigurimi.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            DbCore.DbShare.colKonfigurimAmbjenti konfVarura = new DbCore.DbShare.colKonfigurimAmbjenti();
            foreach (DbCore.DbShare.clsKonfigurimAmbjenti konfi in colKonfig)
            {
                var alternativa = clsAlternativaKushti.getAlternativa(konfi.IdKonfigAmbjente, "V");
                if (alternativa == "Po" && !mod)
                    konfVarura.Add(konfi);
            }
            foreach (DbCore.DbShare.clsKonfigurimAmbjenti konfi in konfVarura)
            {
                colKonfig.Remove(konfi);
            }
            ListBoxColumn colprove = new ListBoxColumn() { FieldName = "KodKonfigAmbjente", Caption = rm.GetString("cmbCmimeArtikulliCaptionKodi", cultinf) };
            ListBoxColumn colemer = new ListBoxColumn() { FieldName = "PershkrimKonfigAmbjente", Caption = rm.GetString("cmbCmimeArtikulliCaptionPershkrimi", cultinf) };
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
        /// Percakton veprimin qe kryhet kur klikohet nje nga butonat e menuse
        /// </summary>
        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {
            if (e.Item.Name == "Ruaj")
            {
                Page.Validate();
                ruajRegjistrimUrdherPagese(1);
                mbushHiddenFieldet(new DbCore.DbArkaBanka.colTrupiUrdherPagese());
            }
            if (e.Item.Name == "RuajPrint")
            {
                Page.Validate();
                ruajRegjistrimUrdherPagese(1, true);
                mbushHiddenFieldet(new DbCore.DbArkaBanka.colTrupiUrdherPagese());

            }
            if (e.Item.Name == "Draft")
            {
                Page.Validate();
                ruajRegjistrimUrdherPagese(0);// 0 = statusi i dokumentit(ne kete rast statusi eshte draft)
                mbushHiddenFieldet(new DbCore.DbArkaBanka.colTrupiUrdherPagese());
            }
            if (e.Item.Name == "PrintPreview")
            {

                if (hfShtimModifikim.Value.ToString() == "modifikim")
                {
                    string id = Request.QueryString["id"];
                    DbCore.DbArkaBanka.clsKokaUrdherPagese clsKoka = new DbCore.DbArkaBanka.clsKokaUrdherPagese(int.Parse(Request.QueryString["id"]));
                    Container.Attributes["src"] = "RaportiShpejte.aspx?Sesioni=false&emriReal=formatUrdherPagese&idDokumenti=" + clsKoka.IdKoka + "&printo=false&raportdyte=jo&iddesign=" + cmbFormatiPrintimit.Value;
                }
                else
                {
                    Page.Validate();
                    ruajRegjistrimUrdherPagese(1);
                }
            }
        }

        /// <summary>
        /// fshin dokumentin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            pergjigja.Text = "";

            DbCore.DbArkaBanka.clsKokaUrdherPagese kokam = new DbCore.DbArkaBanka.clsKokaUrdherPagese(int.Parse(Request.QueryString["id"]));

            bool lidhur = kokam.eshteILidhur();

            if (lidhur)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgDokLidhurNukFshihet",cultinf), pnlMesazhi);
                status1.Value = "false";
                return;
            }
            if (kokam.DtDok.Year != new DbCore.DbAdmin.clsNdermarrjeViti(DbCore.mySessionObjects.ktheNdermarrjeVit(Session)).Viti)
            {
                status1.Value = "false";
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgDataNukPerketVititUshtrimor",cultinf), pnlMesazhi);
                return;
            }
            int idNdermarrje = (int)hfState["idNdermarrje"];
            //DbCore.DbAdmin.clsPeriudhaKontabel periudha = new DbCore.DbAdmin.clsPeriudhaKontabel(kokam.DtDok, idNdermarrje);
            //DbCore.clsMesazh mesazhi = periudha.isPeriudheKycur();
            bool ekycur = DbCore.DbAdmin.clsPeriudhaKontabel.eshteKycurPeriudheSipasDateDheNdermarrjes(kokam.DtDok, idNdermarrje);
            if (ekycur)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgPeriudhaEKycur", cultinf), pnlMesazhi);
                return;
            }

            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            kokam.IdPerdoruesi = (int)hfState["idPerdoruesi"];
            mesazh = kokam.fshi();
            if (mesazh.Status)
            {
                Response.Redirect("UrdherPagesa.aspx?fshi=po");
                status1.Value = "true";
            }
            else
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                status1.Value = "false";
                return;
            }


        }

        /// <summary>
        /// Ruan/Modifikon nje objekt clsKokaUrdherPagese.
        /// Therret funksionin <see cref="krijoRegjistrimMagazine"/>
        /// Therret funksionin <see cref="clsKokaMagazina.ruaj"/> ose <see cref="clsKokaMagazina.modifiko"/>
        /// </summary>
        /// <param name="statusDokumenti">Statusi me te cilin po ruhet dokumenti (I rregullt apo draft)</param>
        private void ruajRegjistrimUrdherPagese(int statusDokumenti, bool printo = false)
        {           
            if (Page.IsValid == false)
                return;
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            if (isValidRegjistrimUrdherPagese(statusDokumenti, rm, ci))
            {
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                DbCore.DbArkaBanka.clsKokaUrdherPagese regjistrim = krijoRegjistrimUrdherPagese(statusDokumenti);
                if (regjistrim.OColTrupi.Count == 0)
                {
                    status1.Value = "false";
                    return;
                }
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

                tedrejtaInfo.merrTeDrejtaPerKeteKomponente((int)hfState["idPerdoruesi"], (int)hfState["idNdermarrje"], (int)hfState["idViti"], "Shto_UrdherPagesa.aspx");

                if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim")
                {
                    if ((statusDokumenti == 1 && !tedrejtaInfo.DShtim) || (statusDokumenti == 0 && !tedrejtaInfo.DShtimDraft))
                    //if (!tedrejtaInfo.DShtim)
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("mesazhRaportNukKeniTeDrejta", ci), pnlMesazhi);
                        status1.Value = "false";
                        return;
                    }
                    mesazh = regjistrim.ruaj(hfNrAutoShitje);
                }
                else if (hfShtimModifikim.Value == "modifikim")
                {
                    if ((statusDokumenti == 1 && !tedrejtaInfo.DMod) || (statusDokumenti == 0 && !tedrejtaInfo.DModifikimDraft))
                    //if (!tedrejtaInfo.DMod)
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("mesazhRaportNukKeniTeDrejta", ci), pnlMesazhi);
                        status1.Value = "false";
                        return;
                    }
                    regjistrim.IdKoka = int.Parse(Request.QueryString["id"]);
                    bool lidhur = regjistrim.eshteILidhur();
                    if (lidhur.ToString() != hfLidhur.Value)
                    {
                        mesazh.Status = false;
                        mesazh.PershkrimMesazhi = rm.GetString("msgDokumentiEshteILidhur", ci);
                        status1.Value = "false";
                    }
                    else
                    {
                        if (lidhur == true)
                            mesazh = regjistrim.modifiko(true);
                        else
                            mesazh = regjistrim.modifiko(false);
                    }
                }
                pergjigja.Text = "ruaj";
                if (mesazh.Status == true)
                {
                    if (printo)
                        Container.Attributes["src"] = "RaportiShpejte.aspx?Sesioni=false&emriReal=formatUrdherPagese&idDokumenti=" + regjistrim.IdKoka + "&printo=true&raportdyte=jo&iddesign=" + cmbFormatiPrintimit.Value;
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                    hl = new HtmlTable();
                    pnlLidhur.Update();
                    status1.Value = "true";
                    hfShtimModifikim.Value = "shtim";
                }
                else
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                    status1.Value = "false";
                }

            }
            else
            {
                status1.Value = "false";
            }
            pergjigja.ClientVisible = false;
        }

        /// <summary>
        /// Krijon nje objekt te tipit DbCore.DbArkaBanka.clsKokaUrdherPagese
        /// </summary>
        /// <param name="statusDokumenti">Statusi i dokumentit (I rregullt ose draft)</param>
        /// <returns>Kthen nje objekt te tipit DbCore.DbArkaBanka.clsKokaMagazina</returns>
        private DbCore.DbArkaBanka.clsKokaUrdherPagese krijoRegjistrimUrdherPagese(int statusDokumenti)
        {
            var controls = this.GetAsPxTextEditIdValue();
            controls.AddRange(ASPxSplitter1.GetAsPxTextEditIdValue());

            hfNrAuto = (ASPxHiddenField)DbCore.DbAdmin.NrAuto.VendosVleratNrAuto(hfNrAuto, controls);
            //DbCore.DbAdmin.NrAuto.vendosVleratNrAuto(hfNrAuto, this, null, ASPxSplitter1, null);

            hfNrAutoShitje = (ASPxHiddenField)DbCore.DbAdmin.NrAuto.ShtoNeRegjistrime(hfNrAutoShitje, hfNrAuto, "txtNrDok", "NrDok");
            //DbCore.DbAdmin.NrAuto.shtoNeHfRegjistrime(hfNrAutoShitje, hfNrAuto, "txtNrDok", "NrDok");

            DbCore.DbShare.clsKonfigurimAmbjenti clsKonf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            clsKonf.mbushKonfigDefaultKomponentes(313, (int)hfState["idNdermarrje"]);
            if (cmbKonfigurimi.Value != null && cmbKonfigurimi.Text != "")
            {
                clsKonf.mbushKonfigAmbjSipasKod(cmbKonfigurimi.Text, (int)hfState["idNdermarrje"]);
            }
            decimal total = 0;
            if (txtVlefta.Text != "")
                total = Convert.ToDecimal(txtVlefta.Text);
            int idFormatPrintimi = Convert.ToInt32(cmbFormatiPrintimit.Value);
            DbCore.DbArkaBanka.clsKokaUrdherPagese koka = new DbCore.DbArkaBanka.clsKokaUrdherPagese();
            koka.krijoUrdherPagese(clsKonf.IdNivel, clsKonf.IdKonfigAmbjente, txtNrKuponi.Text, txtNrPunonjesve.Text, dteDtDok.Date, txtNrDok.Text, txtEmriPerfitues.Text, total, txtNipti.Text, 0, txtEmriBankes.Text, statusDokumenti, (int)hfState["idNdermarrje"], (int)hfState["idNdermarrjeVit"], (int)hfState["idPerdoruesi"], dteDtDokNgjitur.Date, txtNrLlogBankare.Text, 0, 0, 0, txtAdresa.Text, Convert.ToInt32(radLlojDok.Value), txtNrDokNgjitur.Text, dteDtAprovimi.Date, idFormatPrintimi, txtUrdheruesi.Text, txtKontabilisti.Text, txtNenpunesiThesarit.Text, ruajTrupin());
            return koka;
        }

        /// <summary>
        /// krijon trupin e dokumentit
        /// </summary>
        /// <returns></returns>
        private DbCore.DbArkaBanka.colTrupiUrdherPagese ruajTrupin()
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            JavaScriptSerializer serializusi = new JavaScriptSerializer();
            object[] dokumenti = (object[])serializusi.DeserializeObject(gridDataObject.Value);

            DbCore.DbArkaBanka.colTrupiUrdherPagese trupat = new DbCore.DbArkaBanka.colTrupiUrdherPagese();
            try
            {
                for (int i = 0; i < dokumenti.Length; i++)
                {
                    DbCore.DbArkaBanka.clsTrupiUrdherPagese trup = new DbCore.DbArkaBanka.clsTrupiUrdherPagese((Dictionary<string, object>)dokumenti[i], (int)hfState["idNdermarrje"]);
                    if (trup.IdGrupi != 0)
                    {
                        if (trup.Shuma == 0)
                        {
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgShumaNukDuhetTeJeteZero",cultinf), pnlMesazhi, LoadingPanel);
                            return new DbCore.DbArkaBanka.colTrupiUrdherPagese();
                        }
                        trupat.Add(trup);
                    }
                    else
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgMungojneTeDhena",cultinf), pnlMesazhi, LoadingPanel);
                        return new DbCore.DbArkaBanka.colTrupiUrdherPagese();
                    }

                }
            }
            catch (Exception e)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(e.Message);
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, e.Message, pnlMesazhi, LoadingPanel);
                return new DbCore.DbArkaBanka.colTrupiUrdherPagese();
            }
            return trupat;
        }

        /// <summary>
        /// Kontrollon nese jane plotesuar te gjitha fushat e detyrueshme te kokes se dokumentit
        /// </summary>
        private bool isValidRegjistrimUrdherPagese(int draft, ResourceManager rm, CultureInfo ci)
        {
           
            bool isValid = true;

            if (dteDtDokNgjitur.Text == "")
            {
                isValid = false;
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgZgjidhniNjeDateDokumentiNgjitur",ci), pnlMesazhi);
                return isValid;
            }
            if (dteDtDok.Text == "")
            {
                isValid = false; clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgLidhjaDokZgjidh1DateDokumenti", ci), pnlMesazhi);
                return isValid;
            }
            if (dteDtAprovimi.Text == "")
            {
                isValid = false; clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgZgjidhniNjeDateAprovimi",ci), pnlMesazhi);
                return isValid;
            }
            if (dteDtDok.Date.Year != new DbCore.DbAdmin.clsNdermarrjeViti((int)hfState["idNdermarrjeVit"]).Viti)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgDataNukPerketVititUshtrimor",ci), pnlMesazhi);
                return false;
            }

            DbCore.DbAdmin.clsPeriudhaKontabel periudha = DbCore.mySessionObjects.merrPeriudheKontabel(Session);
            String mesazhGabimi;
            if (!DbCore.clsFunksione.checkPeriudheKontabel(out mesazhGabimi, dteDtDok.Date, periudha, draft))
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhGabimi, pnlMesazhi);
                return false;
            }
            return isValid;
        }
    }
}