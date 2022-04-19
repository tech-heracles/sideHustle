using DbCore;
using DbCore.DbAdmin;
using DevExpress.Web;
using PlatinumWeb.Templates;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using DbCore.IMBUtils.Types;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using System.Web.Configuration;
using DbCore.IMBUtils.Logging;

namespace PlatinumWeb
{
    public partial class ShtoModifiko_Grup_Perdoruesish : MyPageBase
    {
        private int rowIndex = 0;
        private colNdermarrjet ndermarrjet;
        private colNdermarrjet ndermarjesel = new colNdermarrjet(); //ruan ndermarrjet e selektuara tek grida lboxNdermarjetRol
        private string komponente = "ShtoModifiko_Grup_Perdoruesish.aspx";
        private string _guidString, roletEPerdoruesit = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            int idPerdoruesi;
            int idGjuha;
            int idNdermarrje;
            int idViti;
            int idNdermarrjeVit;
            string perdoruesUsername;
            clsPerdorues oPerdorues;
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (hfState.Count == 0)
            {
                if (!DbCore.mySessionObjects.isLogedIn(Session))
                {
                    DbCore.clsFunksione.logout(Session, true, "FaqePaautorizuar");
                }
                idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
                {
                    Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerdoruesi);
                }
                idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
                idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
                idNdermarrjeVit = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
                oPerdorues = DbCore.mySessionObjects.kthePerdorues(Session);

                var idKonfigAmbjente = DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdSipasKategoriDheNderm(49, idNdermarrje);
                if (DbCore.DbShare.clsAlternativaKushti.getAlternativa(idKonfigAmbjente, "MMRT") == "Po")
                    roletEPerdoruesit = clsRolPerdorues.merrKodeRoleshPerPerdoruesin(oPerdorues.IdPerdorues);

                hfState.Set("idPerdoruesi", idPerdoruesi);
                hfState.Set("idGjuha", idGjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("idViti", idViti);
                hfState.Set("idNdermarrjeVit", idNdermarrjeVit);
                hfState.Set("usernamePerdoruesLoguar", oPerdorues.PerdoruesUsername);
                hfState.Set("roletEPerdoruesit", roletEPerdoruesit);
                _guidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                hfState.Set("guidString", _guidString);
                hfState.Set("invisibleVodafone", !bool.Parse(WebConfigurationManager.AppSettings["BuxhetQK"]));
            }
            else
            {
                idPerdoruesi = (int)hfState["idPerdoruesi"];
                idGjuha = (int)hfState["idGjuha"];
                idNdermarrje = (int)hfState["idNdermarrje"];
                idViti = (int)hfState["idViti"];
                idNdermarrjeVit = (int)hfState["idNdermarrjeVit"];
                perdoruesUsername = hfState["usernamePerdoruesLoguar"].ToString();
                _guidString = (string)hfState["guidString"];
                roletEPerdoruesit = (string)hfState["roletEPerdoruesit"];
            }
            if (!IsPostBack)
            {
                mbushHiddenFieldMePerkthime(ci, rm);
                EmrateTabeve(rm, ci);
                perktheLabel(ci, rm);
                percaktoTemplateMenu(idGjuha, idViti, idPerdoruesi, idNdermarrje, ASPxMenu1);
                konfiguroVleraFillestare(idNdermarrje, idPerdoruesi, rm, ci, idGjuha);
                konfiguroGrideLidhjeRolPerdorues();
                perktheGrideLidhjaERoleve(ci, rm);
                clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idNdermarrje, "roletASPxGridView", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
                mbushGridRoleshNgaDB();
                konfiguroGride(idPerdoruesi);
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "roletASPxGridView", roletASPxGridView, cmbKonfigurimi.Text.Split(';')[0], "160", (int)hfState["idGjuha"]);
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, komponente);
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                HiddenFieldGjuha.Value = idGjuha.ToString();
                roletASPxGridView.Columns["#"].VisibleIndex = 0;
            }
            else
            {
                percaktoTemplateMenu(idGjuha, idViti, idPerdoruesi, idNdermarrje, ASPxMenu1);
                clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idNdermarrje, "roletASPxGridView", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
                if (!IsCallback || (IsCallback && Request["__CALLBACKID"].ToString().Contains("roletASPxGridView")))
                {
                    mbushGridRoleshNgaSession();
                    konfiguroGride(idPerdoruesi);
                }
                if (!IsCallback || (IsCallback && Request["__CALLBACKID"].ToString().Contains("lboxNdermarjetRol")))
                {
                    ndermarjesel = DbCore.mySessionObjects.merrNdermarrjetSelNgaSesioni(Session);
                    mbushListBoxNdermarrjeRol(ndermarjesel);
                }
                colNdermarrjet nderm = DbCore.mySessionObjects.merrNdermarrjetPerPerdoruesDheLicenceNgaSesioni(Session);
                mbushcmbNdermarje(nderm);
            }

            GridUtil.ToolTipButonaveMbiGride(roletASPxGridView, ci, rm);
            AspxWebControlUtils.perkthePopUp(popFshi, rm.GetString("labelKujdes", ci), lblMsgbox, rm.GetString("labelAdministrimiMsgJeniSigurt", ci), ButtonCancel, rm.GetString("labelAnullo", ci));
            AspxWebControlUtils.perkthePopUp(popKlono, rm.GetString("lupaLoginNdermarrjeZgjidhNdermarrjen", ci), lblNdermarja, rm.GetString("filterRaportIdNdermarje", ci), btnAnullo, rm.GetString("labelAnullo", ci), btnOk, rm.GetString("buttonRuaj", ci));
        }

        private void perktheGrideLidhjaERoleve(CultureInfo ci, ResourceManager rm)
        {
            gridLidhjeRole.AllColumns["Emri"].Caption = rm.GetString("labelRaportEmri", ci);
            gridLidhjeRole.AllColumns["Mbiemri"].Caption = rm.GetString("labelRaportMbiemri", ci);
        }

        /// <summary>
        /// Metode qe sherben per te kaluar perkthime te js
        /// </summary>
        /// <param name="ci">Merr culture info</param>
        /// <param name="rm">Merr Resource Manager</param>
        private void mbushHiddenFieldMePerkthime(CultureInfo ci, ResourceManager rm)
        {
            hfState.Set("colHeaderModuli", rm.GetString("colHeaderModuli", ci));
            hfState.Set("colHeaderPershkrimKomponente", rm.GetString("colHeaderPershkrimKomponente", ci));
            hfState.Set("cmbboxItemFilterAvancTeGjitha", rm.GetString("cmbboxItemFilterAvancTeGjitha", ci));
            hfState.Set("colHeaderFshirje", rm.GetString("colHeaderFshirje", ci));
            hfState.Set("colHeaderModifikimi", rm.GetString("colHeaderModifikimi", ci));
            hfState.Set("colHeaderModifikimiDraft", rm.GetString("colHeaderModifikimiDraft", ci));
            hfState.Set("colHeaderShtimi", rm.GetString("colHeaderShtimi", ci));
            hfState.Set("colHeaderShtimiDraft", rm.GetString("colHeaderShtimiDraft", ci));
            hfState.Set("colKerkim", rm.GetString("colKerkim", ci));
            hfState.Set("colEksportim", rm.GetString("colEksportim", ci));
            hfState.Set("colPrintim", rm.GetString("colPrintim", ci));
            hfState.Set("colArkiva", rm.GetString("colArkiva", ci));
            hfState.Set("colKonverto", rm.GetString("btnKonverto", ci));
            hfState.Set("colPezullo", rm.GetString("colPezullo", ci));
            hfState.Set("colAutoKonverto", rm.GetString("colAutoKonverto", ci));
            hfState.Set("colHeaderShikoGjitheDok", rm.GetString("colHeaderShikoGjitheDok", ci));
            hfState.Set("msgRuaniRolinPeraparaSeTeKaloniTeDrejtat", rm.GetString("msgRuaniRolinPeraparaSeTeKaloniTeDrejtat", ci));
            hfState.Set("msgDuhetTeZgjidhniTePaktenNjeRol", rm.GetString("msgDuhetTeZgjidhniTePaktenNjeRol", ci));
            hfState.Set("msgDuhetTeZgjidhniNjeRol", rm.GetString("msgDuhetTeZgjidhniNjeRol", ci));
            hfState.Set("colHeaderShtimiDraft", rm.GetString("colHeaderShtimiDraft", ci));
            hfState.Set("msgMosModifikoRolinTend", rm.GetString("msgMosModifikoRolinTend", ci));
            hfState.Set("buttonRuajNdryshimet", rm.GetString("labelRuajNdryshimet", ci));
            hfState.Set("labelTitulli", rm.GetString("labelZgjidhNdermarrjet", ci));
            hfState.Set("buttonMbyll", rm.GetString("buttonMbyll", ci));
            hfState.Set("msgRuajNdermarrjeRolSukses", rm.GetString("msgRuajNdermarrjeRolSukses", ci));
            hfState.Set("msgRuajNdermarrjeRolGabim", rm.GetString("msgRuajNdermarrjeRolGabim", ci)); 
            hfState.Set("cmbTeGjitha", rm.GetString("cmbTeGjitha", ci));
        }

        /// <summary>
        /// Vendos emrat e tabeve ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateTabeve(System.Resources.ResourceManager rm, System.Globalization.CultureInfo ci)
        {
            tabetASPxPageControl.TabPages[0].Text = rm.GetString("TePergjithshmeTab", ci);
            tabetASPxPageControl.TabPages[1].Text = rm.GetString("roliTab", ci);
            tabetASPxPageControl.TabPages[2].Text = rm.GetString("ndermarrjetTab", ci);
            tabetASPxPageControl.TabPages[3].Text = rm.GetString("teDrejtaTab", ci);
            tabetASPxPageControl.TabPages[4].Text = rm.GetString("lidhjaERoleveTab", ci);
        }

        /// <summary>
        /// Metode qe sherben per te perkthyer label
        /// </summary>
        /// <param name="ci">Merr CultureInfo</param>
        /// <param name="rm">Merr ResourceManager</param>
        public void perktheLabel(CultureInfo ci, ResourceManager rm)
        {
            //perkthimet te tabi te drejta
            lblNdryshoCmimeShitje.Text = rm.GetString("lblNdryshoCmimetEShitjes", ci);
            lblNdryshoCmimeBlerje.Text = rm.GetString("lblNdryshoCmiminEBlerjes", ci);
            lblNdryshoZbritjeAnalitike.Text = rm.GetString("lblNdryshoZbritjeAnalitike", ci);
            lblNdryshoZbritjeTotale.Text = rm.GetString("lblNdryshoZbritjeTotale", ci);
            lblVetemKonvertim.Text = rm.GetString("lblVetemFaturaShitjeTeKonvertuara", ci);
            lblKonvertimSipasUrdherShitje.Text = rm.GetString("lblKonvertimSipasUrdherShitje", ci);
            ASPxLabel6.Text = rm.GetString("koloneLoginNdermarrjeViti", ci);
            ASPxLabel5.Text = rm.GetString("labelFooterNdermarrja", ci);
            //perkthimet te tabi role
            kodiASPxLabel.Text = rm.GetString("labelBlerjeShitjeKodi", ci);
            pershkrimiASPxLabel.Text = rm.GetString("labelBlerjeShitjePershkrimi", ci);
            dateKrijimiASPxLabel.Text = rm.GetString("lblDataKrijimi", ci);
            dateModifikimiASPxLabel.Text = rm.GetString("lblDateModifikimi", ci);
            idKrijuesiASPxLabel.Text = rm.GetString("filterRaportKrijuesi", ci);
            aktivASPxLabel.Text = rm.GetString("lblAktiv", ci);
            lblLicenca.Text = rm.GetString("lblLicenca", ci);
            lblViti.Text = rm.GetString("labelAdministrimiViti", ci);
            konfigurimi_Label.Text = rm.GetString("lblModeli", ci);
        }

        private void mbushTreeGride(int idRoli, int idNdermarrje, int idViti, int idLlojLicence)
        {
            colTeDrejtaRoli rolet = new colTeDrejtaRoli();
            int idGjuha = 0;
            if (HiddenFieldGjuha.Value != null && HiddenFieldGjuha.Value != "")
                idGjuha = int.Parse(HiddenFieldGjuha.Value);
            rolet = rolet.krijoPemePerTreeGrid(idGjuha, idRoli, idNdermarrje, idViti, idLlojLicence);
            JavaScriptSerializer serializusi = new JavaScriptSerializer();
            serializusi.MaxJsonLength = 90000000;
            HfColTeDrejtat.Value = serializusi.Serialize(rolet);
        }

        private void konfiguroVleraFillestare(int idNdermarrje, int idPerdoruesi, System.Resources.ResourceManager rm, System.Globalization.CultureInfo ci, int idGjuha)
        {
            DbCore.DbAdmin.clsNdermarrje n = new DbCore.DbAdmin.clsNdermarrje(idNdermarrje);
            ndermarrjet = new colNdermarrjet();
            ndermarrjet.merrNdermarrjet(idPerdoruesi, n.IdLicenca);
            if (n.IdLicenca != 1)
            {
                cmbLicenca.ClientVisible = false;
                lblLicenca.ClientVisible = false;
            }
            mbushcmbNdermarje(ndermarrjet);
            DbCore.mySessionObjects.ruajNdermarjetPerPerdoruesDheLicenceNeSesion(ndermarrjet, Session);
            hfId.Value = "0";
            mbushComboNdermarrje(ndermarrjet);
            ConfigureAspxComboBox.mbushComboLicencat(cmbLicenca, true);
            cmbLicenca.Value = n.IdLicenca;
            DbCore.mySessionObjects.ruajNdermarjetSelNeSesion(Session, ndermarjesel);
            ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 49, rm, ci, idGjuha);
            cmbKonfigurimi.SelectedIndex = 0;
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()), idGjuha);
            hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;
            mbushTreeGride(0, 0, 0, 0);//si fillim nuk i japim vlera
            mbushLidhjeRolPerdorues(Convert.ToInt32(hfId.Value), idGjuha);
        }

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="idGjuha"></param>
        /// <param name="idNderVit"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        private void percaktoTemplateMenu(int idGjuha, int idViti, int idPerdorues, int idNdermarrje, ASPxMenu aSPxMenu1)
        {
            clsToolbarConfig.percaktoTemplateMenu(idGjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1, komponente, this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, hfShtimModifikim.Value == "modifikim" ? false : true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
        }

        protected void RuajKolona_Click(object sender, EventArgs e)
        {
            int idNdermarrje = (int)hfState["idNdermarrje"];
            int idPerdoruesi = (int)hfState["idPerdoruesi"];
            int idViti = (int)hfState["idViti"];
            int idGjuha = (int)hfState["idGjuha"];
            int idfiltri = 0;
            int idkonf = DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(cmbKonfigurimi.Text.Split(';')[0], idNdermarrje);
            DbCore.clsMesazh mesazh = GridUtil.ruajFiltra(idNdermarrje, idPerdoruesi, idGjuha, "roletASPxGridView ", komponente, "FilterDefault", roletASPxGridView.FilterExpression, roletASPxGridView, "KodRoli", idkonf, out idfiltri);//ruan filtrin e zgjedhur tek filtrat
            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                return;
            }
            mesazh = GridUtil.ruajkonfigurimgride(roletASPxGridView, cmbKonfigurimi.Text, idNdermarrje, idPerdoruesi, 160, idfiltri, idViti, DbCore.mySessionObjects.ktheCultureInfo(Session), DbCore.mySessionObjects.ktheGjuhe(Session));/// ruan konfigurimin e grides dhe filtrin
            clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idNdermarrje, "roletASPxGridView ", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
            percaktoTemplateMenu(idGjuha, idViti, idPerdoruesi, idNdermarrje, ASPxMenu1);
            if (mesazh.Status)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
        }

        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu((int)hfState["idGjuha"], (int)hfState["idViti"], (int)hfState["idPerdoruesi"], (int)hfState["idNdermarrje"], ASPxMenu1);
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
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            int idNdermarrje = (int)hfState["idNdermarrje"];
            int idPerdoruesi = (int)hfState["idPerdoruesi"];
            int idGjuha = (int)hfState["idGjuha"];
            int idViti = (int)hfState["idViti"];
            DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuha, "roletASPxGridView", komponente, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = idPerdoruesi;
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();
                clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idNdermarrje, "roletASPxGridView", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
                percaktoTemplateMenu(idGjuha, idViti, idPerdoruesi, idNdermarrje, ASPxMenu1);
                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                cmbFiltra.Text = "";
                this.roletASPxGridView.FilterExpression = String.Empty;
            }
        }

        /// <summary>
        /// sherben per te ruajtur filtrin e zgjedhur
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            int idNdermarrje = (int)hfState["idNdermarrje"];
            int idPerdoruesi = (int)hfState["idPerdoruesi"];
            int idGjuha = (int)hfState["idGjuha"];
            int idViti = (int)hfState["idViti"];
            DbCore.DbAdmin.clsFiltraGrida filtri = new DbCore.DbAdmin.clsFiltraGrida();
            filtri.FiltraKodi = cmbFiltra.Text;
            filtri.FiltraShenime = cmbFiltra.Text;
            filtri.FiltraUniversal = false;
            DbCore.DbAdmin.clsGridaKoka koka = new clsGridaKoka(idGjuha, "roletASPxGridView", komponente, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = roletASPxGridView.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("KodRoli", roletASPxGridView);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = roletASPxGridView.GetSortedColumns();
            //if (kolona.Count > 0)
            //{
            //    filtri.KoloneRenditje = kolona[0].FieldName;
            //    if (kolona[0].SortOrder == DevExpress.Data.ColumnSortOrder.Ascending)
            //        filtri.DrejtimRenditje = true;
            //    else
            //        filtri.DrejtimRenditje = false;
            //}
            //else
            //{
            //    filtri.KoloneRenditje = "KodRoli";
            //    filtri.DrejtimRenditje = true;
            //}
            filtri.IdPerdoruesi = idPerdoruesi;
            filtri.IdNdermarje = idNdermarrje;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            filtri.IdStatusDok = 1;
            mesazh = filtri.ruaj();
            clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idNdermarrje, "roletASPxGridView", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
            percaktoTemplateMenu(idGjuha, idViti, idPerdoruesi, idNdermarrje, ASPxMenu1);
            if (mesazh.Status == true)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            cmbFiltra.Text = "";
        }

        /// <summary>
        /// perdoret per te fshire reshtat e zgjedhur ne gride pasi perdoruesi ka konfirmuar fshirjen
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            List<object> rreshtat;
            if (tabetASPxPageControl.ActiveTabIndex == 0)//nqs kemi zgjedhur vetem nje kartele per te pare fshijme vetem ate
                rreshtat = roletASPxGridView.GetSelectedFieldValues("IdRoli");
            else
            {
                rreshtat = new List<object>();
                rreshtat.Add(hfId.Value);
            }
            if (rreshtat.Count == 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgJuLutemZgjidhniTePaktenNjeRol", ci), pnlMesazhi);
                return;
            }
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            List<string> teFshire = new List<string>(), tePaFshire = new List<string>();
            List<string> teLidhur = new List<string>();
            bool u_fshi = false;
            foreach (object id in rreshtat)
            {
                DbCore.DbAdmin.clsRoli roli = new clsRoli(Convert.ToInt32(id));
                if (roli.Model == 1)
                {
                    tePaFshire.Add(roli.KodRoli);
                    continue;
                }
                if (DbCore.DbAdmin.clsRolPerdorues.eshteRoliLidhurMePerdorues(roli.IdRoli))
                {
                    teLidhur.Add(roli.KodRoli);
                    continue;
                }
                roli.IdPerdoruesi = (int)hfState["idPerdoruesi"];
                u_fshi = roli.fshi();
                if (roli.IdRoli == 0)
                    continue;
                if (u_fshi)
                {
                    hiqRolNgaGrida(roli.IdRoli, ci, rm);
                    teFshire.Add(roli.KodRoli);
                }
            }
            string mesazhInfoGabim = "", mesazhInfoSukses = "";
            if (tePaFshire.Count == 1)
                mesazhInfoGabim = String.Format("{0} {1} {2}", rm.GetString("msgRoliMeKod", ci), String.Join(";", tePaFshire), rm.GetString("msgEshteDefaultDheNukMundTeFshihet", ci));
            else
                if (tePaFshire.Count > 1)
                mesazhInfoGabim = String.Format("{0} {1} {2}", rm.GetString("msgRoletMeKod", ci), String.Join(";", tePaFshire), rm.GetString("msgJaneDefaultDheNukMundTeFshihen", ci));
            if (teLidhur.Count == 1)
                mesazhInfoGabim += " " + String.Format("{0} {1} {2}", rm.GetString("msgRoliMeKod", ci), String.Join(";", teLidhur), rm.GetString("msgEshteDefaultDheNukMundTeFshihet", ci));
            else
                if (teLidhur.Count > 1)
                mesazhInfoGabim += " " + String.Format("{0} {1} {2}", rm.GetString("msgRoletMeKod", ci), String.Join(";", teLidhur), rm.GetString("msgJaneDefaultDheNukMundTeFshihen", ci));
            if (teFshire.Count == 1)
                mesazhInfoSukses = String.Format("{0} {1} {2}", rm.GetString("msgRoliMeKod", ci), String.Join(";", teFshire), rm.GetString("msgStrukturaAdministrativeSuffixNjejesSuksesi", ci));
            else
                if (teFshire.Count > 1)
                mesazhInfoSukses = String.Format("{0} {1} {2}", rm.GetString("msgRoletMeKod", ci), String.Join(";", teFshire), rm.GetString("regjMagSuffixMesazhShumesSuksesi", ci));
            if (mesazhInfoGabim != "" && mesazhInfoSukses != "")
                mesazhInfoGabim += rm.GetString("msgLidhesMesazhi", ci) + mesazhInfoSukses;
            if (mesazhInfoGabim != "")
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhInfoGabim, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazhInfoSukses, pnlMesazhi);
            this.tabetASPxPageControl.ActiveTabIndex = 0;
            mbushGridRoleshNgaDB();
            pnlMesazhi.Update();
        }

        private void hiqRolNgaGrida(int idRoli, CultureInfo ci, ResourceManager rm)
        {
            if (this.roletASPxGridView.DataSource != null)
            {
                DataTable dt = (DataTable)roletASPxGridView.DataSource;
                DataRow[] drs = dt.Select("IdRoli = " + idRoli);
                if (drs.Length > 1)
                    throw new Exception(rm.GetString("msgGabimNdodhen2RoleMeTeNjejtenIdNeGride", ci));
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                dt.Rows.Remove(dr);
                roletASPxGridView.DataBind();
            }
            else mbushGridRoleshNgaDB();
        }

        private void shtoRolNeGrid(int idRoli, CultureInfo ci, ResourceManager rm)
        {
            if (roletASPxGridView.DataSource != null)
            {
                DataTable dt = (DataTable)roletASPxGridView.DataSource;
                DataRow[] drs = dt.Select("IdRoli = " + idRoli);
                if (drs.Length > 0)
                    throw new Exception(rm.GetString("msgRoliEkzistonNeGride", ci));
                DataRow newArtDr = DbCore.DbAdmin.clsRoli.merrRolDR(idRoli);
                dt.ImportRow(newArtDr);
            }
            else mbushGridRoleshNgaDB();
        }

        private void modifikoRolNeGrid(int idRoli, CultureInfo ci, ResourceManager rm)
        {
            if (roletASPxGridView.DataSource != null)
            {
                DataTable dt = (DataTable)roletASPxGridView.DataSource;
                DataRow[] drs = dt.Select("IdRoli = " + idRoli);
                if (drs.Length > 1)
                    throw new Exception(rm.GetString("msgGabimNdodhen2RoleMeTeNjejtenIdNeGride", ci));
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                DataRow newArtDr = DbCore.DbAdmin.clsRoli.merrRolDR(idRoli);
                object[] arr = newArtDr.ItemArray;
                dr.ItemArray = arr;
            }
            else mbushGridRoleshNgaDB();
        }

        /// <summary>
        /// metoda per te thirrur veprimet e menuse kur shtypen butonat
        /// </summary>
        /// <param name="source">derguesi</param>
        /// <param name="e">parametrat</param>
        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            if (e.Item.Name == "Ruaj")
            {
                ruajRol(ci, rm);
            }
        }

        protected void roletASPxGridView_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            if (e.CallbackName == "COLUMNMOVE" && roletASPxGridView.AllColumns[int.Parse(e.Args[0])].Width.Value == 0)
                roletASPxGridView.AllColumns[int.Parse(e.Args[0])].Width = Unit.Percentage(3);
            if (e.CallbackName == "APPLYFILTER" && e.Args[0] == "")
            {
                DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
                ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
                cmbFiltra.Text = "";
            }
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            GridUtil.ToolTipButonaveMbiGride(roletASPxGridView, ci, rm);
        }

        protected void roletASPxGridView_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (e.Column.FieldName == "AktivRoli")
            {
                (e.Editor as ASPxComboBox).Items.Clear();
                (e.Editor as ASPxComboBox).Items.Add("");
                (e.Editor as ASPxComboBox).Items.Add(rm.GetString("cmbPerdoruesitAktiv", ci), true);
                (e.Editor as ASPxComboBox).Items.Add(rm.GetString("cmbPerdoruesitJoAktiv", ci), false);
            }
        }

        private void mbushGridRoleshNgaSession()
        {
            DataTable tmpObject;
            bool sukses = DbCore.mySessionObjects.merrGrideNgaSessioni(Session, out tmpObject);
            if (!sukses)
                mbushGridRoleshNgaDB();
            else
            {
                roletASPxGridView.DataSource = tmpObject;
                roletASPxGridView.DataBind();
                tmpObject.Dispose();
            }
        }

        private void mbushGridRoleshNgaDB()
        {
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            int idPerdoruesi = (int)hfState["idPerdoruesi"];
            int idLicence = DbCore.DbAdmin.clsLicenca.merrIdLicencePerdoruesi(idPerdoruesi);
            if (idLicence == -1)
                throw new Exception(rm.GetString("msgPerdoruesiNukKaLicence", ci));
            DataTable dt = DbCore.DbAdmin.colRoli.merrRoletDT(idLicence, idPerdoruesi);
            rowIndex = dt.Rows.Count;
            DbCore.mySessionObjects.ruajGrideNeSession(Session, dt);
            roletASPxGridView.DataSource = dt;
            roletASPxGridView.DataBind();
            dt.Dispose();
        }

        private void konfiguroGride(int idPerdoruesi)
        {
            KonfigurimComboGride.ShtoPerdoruesSipasKrijuesit(roletASPxGridView, idPerdoruesi, Session, komponente, _guidString, "IdKrijuesi");
            percaktoTamplate();
            GridUtil.konfigGrideListeEMadhePaTheme(roletASPxGridView, "IdRoli");
            this.roletASPxGridView.Columns["#"].VisibleIndex = 0;
        }

        private void percaktoTamplate()
        {
            GridViewDataColumn col = roletASPxGridView.Columns["AktivRoli"] as GridViewDataColumn;
            col.DataItemTemplate = new MyCheckTemplate(true, false);
        }

        private void mbushComboNdermarrje(colNdermarrjet ndermarrjet)
        {
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (ndermarrjet.Count != 0)
            {
                ASPxComboBoxNdermarrje.DataSource = ndermarrjet;
                ASPxComboBoxNdermarrje.TextField = "ndermarrjeKodi";
                ASPxComboBoxNdermarrje.ValueField = "idNdermarrje";
                ASPxComboBoxNdermarrje.DataBind();
                ASPxComboBoxNdermarrje.SelectedIndex = 0;
                HiddenFieldNdermarrje.Value = ASPxComboBoxNdermarrje.Value.ToString();
                colVitet vitet = new colVitet();
                vitet.mbushVitetTeNdermarjesDheRolit(Convert.ToInt32(hfId.Value), Convert.ToInt32(HiddenFieldNdermarrje.Value));
                mbushComboViti(vitet);
            }
            else
            {
                colVitet vitet = new colVitet();
                mbushComboViti(vitet);
                ASPxComboBoxNdermarrje.Items.Clear();
                ASPxComboBoxNdermarrje.Items.Add(rm.GetString("msgSkaNdermarrjePerKeteRol", ci), 0);
                ASPxComboBoxNdermarrje.SelectedIndex = 0;
                HiddenFieldNdermarrje.Value = "0";
            }
        }

        /// <summary>
        /// mbush combon e ndermarrjes per popupin e klonimit
        /// </summary>
        /// <param name="ndermarrjet"></param>
        private void mbushcmbNdermarje(colNdermarrjet ndermarrjet)
        {
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (ndermarrjet != null && ndermarrjet.Count != 0)
            {
                cmbNdermarja.DataSource = ndermarrjet;
                cmbNdermarja.TextField = "ndermarrjeKodi";
                cmbNdermarja.ValueField = "idNdermarrje";
                cmbNdermarja.DataBind();
                cmbNdermarja.SelectedIndex = 0;
                colVitet vitet = new colVitet((Convert.ToInt32(cmbNdermarja.Value.ToString())));
                mbushcmbViti(vitet);
            }
            else
            {
                colVitet vitet = new colVitet();
                mbushcmbViti(vitet);
                cmbNdermarja.Items.Clear();
                cmbNdermarja.Items.Add(rm.GetString("msgSkaNdermarrje", ci), 0);
                cmbNdermarja.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// mbush kombon e vitit per popupin e klonimit
        /// </summary>
        /// <param name="vitet"></param>
        private void mbushcmbViti(colVitet vitet)
        {
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (vitet.Count != 0)
            {
                cmbViti.DataSource = vitet;
                cmbViti.TextField = "KodiViti";
                cmbViti.ValueField = "IdViti";
                cmbViti.DataBind();
                cmbViti.SelectedIndex = 0;
            }
            else
            {
                cmbViti.Items.Clear();
                cmbViti.Items.Add(rm.GetString("msgSkaVitPerNdermarrjen", ci), 0);
                cmbViti.SelectedIndex = 0;
            }
        }

        private void mbushListBoxNdermarrjeRol(colNdermarrjet ndermarje)
        {
            var ci = mySessionObjects.ktheCultureInfo(Session);
            var rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            lboxNdermarjetRol.Columns.Clear();
            GridViewDataColumn nder = new GridViewDataColumn();
            nder.FieldName = "NdermarrjeKodi";
            nder.Caption = rm.GetString("lblNdermarrjeKodi", ci);
            lboxNdermarjetRol.Columns.Add(nder);
            GridViewDataColumn nderper = new GridViewDataColumn();
            nderper.FieldName = "NdermarrjePershkrimi";
            nderper.Caption = rm.GetString("lblNdermarrjePershkrimi", ci);
            lboxNdermarjetRol.Columns.Add(nderper);
            if (ndermarje.Count > 0)
            {
                colVitet vitet = colVitet.merrGjitheVitetEMundshme();
                string idte = string.Join(",", ndermarje.Select(x => x.IdNdermarrje).ToArray());
                colVitet viteTotale = new colVitet(idte);
                foreach (clsViti v in vitet)
                {
                    GridViewDataCheckColumn ndervit = new GridViewDataCheckColumn();
                    ndervit.DataItemTemplate = new MyCheckTemplate(false, true);
                    ndervit.UnboundType = DevExpress.Data.UnboundColumnType.Boolean;
                    foreach (clsNdermarrje nderm in ndermarje)
                    {
                        var viteNdermarrjes = viteTotale.FindAll(x => x.IdNdermarje == nderm.IdNdermarrje);
                        foreach (clsViti vit in viteNdermarrjes)
                        {
                            if (vit.KodiViti != v.KodiViti) continue;
                            bool check = false;
                            string[] arr = hfVitetSel.Value.Split(';');
                            for (int i = 0; i < arr.Length; i++)
                                if (arr[i].Split(',')[0] == nderm.IdNdermarrje.ToString() &&
                                    arr[i].Split(',')[1] == v.KodiViti)
                                {
                                    check = bool.Parse(arr[i].Split(',')[2]);
                                    break;
                                }
                            ndervit.DataItemTemplate = new MyCheckTemplateGrid(0, v.KodiViti, check);
                        }

                    }
                    ndervit.FieldName = v.KodiViti;
                    lboxNdermarjetRol.Columns.Add(ndervit);
                }
            }

            //{
            //    GridViewDataCheckColumn ndervit = new GridViewDataCheckColumn();
            //    ndervit.DataItemTemplate = new MyCheckTemplate(false, true);
            //    ndervit.UnboundType = DevExpress.Data.UnboundColumnType.Boolean;
            //    if (ndermarje.Count > 0)
            //    {
            //        int id = Convert.ToInt32(ndermarje[0].IdNdermarrje);

            //        foreach (clsViti vit in viteNdermarrje)
            //        {
            //            if (vit.KodiViti != v.KodiViti)
            //                continue;
            //            else
            //            {
            //                bool check = false;
            //                string[] arr = hfVitetSel.Value.Split(';');
            //                for (int i = 0; i < arr.Length; i++)
            //                    if (arr[i].Split(',')[0] == id.ToString() && arr[i].Split(',')[1] == v.KodiViti)
            //                        check = bool.Parse(arr[i].Split(',')[2]);
            //                ndervit.DataItemTemplate = new MyCheckTemplateGrid(0, v.KodiViti, check);
            //            }
            //        }
            //    }
            //    ndervit.FieldName = v.KodiViti;
            //    lboxNdermarjetRol.Columns.Add(ndervit);
            //}


            lboxNdermarjetRol.DataSource = ndermarje;
            lboxNdermarjetRol.KeyFieldName = "IdNdermarrje";
            lboxNdermarjetRol.DataBind();
        }

        private void mbushComboViti(colVitet vitet)
        {
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            if (vitet.Count != 0)
            {
                ASPxComboBoxViti.DataSource = vitet;
                ASPxComboBoxViti.TextField = "KodiViti";
                ASPxComboBoxViti.ValueField = "IdViti";
                ASPxComboBoxViti.DataBind();
                ASPxComboBoxViti.SelectedIndex = 0;
                HiddenFieldViti.Value = ASPxComboBoxViti.Value.ToString();
            }
            else
            {
                ASPxComboBoxViti.Items.Clear();
                ASPxComboBoxViti.Items.Add(rm.GetString("msgSkaVitPerNdermarrjen", ci), 0);
                ASPxComboBoxViti.SelectedIndex = 0;
                HiddenFieldViti.Value = "0";
            }
        }

        private void ruajRol(CultureInfo ci, ResourceManager rm)
        {
            clsRoli roli = new clsRoli();
            clsPerdorues perd = new clsPerdorues();
            int idNdermarrje = (int)hfState["idNdermarrje"];
            int idViti = (int)hfState["idViti"];
            int idPerdoruesi = (int)hfState["idPerdoruesi"];
            int idGjuha = (int)hfState["idGjuha"];
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, komponente);
            clsLicenca lic = new clsLicenca(int.Parse(cmbLicenca.Value.ToString()));
            if (hfShtimModifikim.Value == "shtim") //eshte i ri
            {
                #region Shtim

                if (!tedrejtaInfo.DShtim)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("mesazhRaportNukKeniTeDrejta", ci), pnlMesazhi);
                    return;
                }
                perd = clsPerdorues.merrUserNgaLogin(idKrijuesiASPxTextBox.Text)[0];
                ArrayList tedrejta = new ArrayList();
                string[] arr = hfVitetSel.Value.ToString().Split(';');
                for (int i = 0; i < arr.Length; i++)
                {
                    bool ekziston = false;
                    if (arr[i] != "")
                    {
                        for (int j = 0; j < tedrejta.Count; j++)
                        {
                            if (arr[i].Split(',')[0] == tedrejta[j].ToString().Split(',')[0]
                                && arr[i].Split(',')[1] == tedrejta[j].ToString().Split(',')[1]
                                && arr[i].Split(',')[3] == tedrejta[j].ToString().Split(',')[3])
                            {
                                ekziston = true;
                                tedrejta.RemoveAt(j);
                                break;
                            }
                        }
                        if (!ekziston)
                        {
                            tedrejta.Add(arr[i]);
                        }
                    }
                }
                if (tedrejta.Count == 0)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgZgjidhniNderrmarrjenDheNjeVit", ci), pnlMesazhi);
                    return;
                }
                mesazh = roli.krijoRolDheTeDrejtaBaze(idGjuha, kodiASPxTextBox.Text, pershkrimiASPxTextBox.Text, aktivASPxCheckBox.Checked, perd.IdPerdorues, 2, lic.IdLicenca, tedrejta, 1, lic.IdLlojLicenca);
                if (!mesazh.Status)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                    return;
                }
                else
                {
                    hfShtimModifikim.Value = "modifikim";
                    hfNdermarjeChanged.Value = "false";
                    hfNdryshuarTeDhenaRoli.Value = "false";
                    hfId.Value = roli.IdRoli.ToString();
                    ndermarrjet = new colNdermarrjet(roli.IdRoli);
                    mbushComboNdermarrje(ndermarrjet);
                    mbushcmbNdermarje(DbCore.mySessionObjects.merrNdermarrjetPerPerdoruesDheLicenceNgaSesioni(Session));
                    HiddenFieldNdermarrjeDestinacion.Value = cmbNdermarja.Value.ToString();
                    HiddenFieldVitiDestinacion.Value = cmbViti.Value.ToString();
                    mbushTreeGride(roli.IdRoli, int.Parse(ASPxComboBoxNdermarrje.Value.ToString()), int.Parse(ASPxComboBoxViti.Value.ToString()), lic.IdLlojLicenca);
                    shtoRolNeGrid(roli.IdRoli, ci, rm);
                    konfiguroGride(idPerdoruesi);
                    tabetASPxPageControl.ActiveTabIndex = 3;
                    //lboxNdermarjet.SelectedIndex = -1;
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("msgRuajtjaPerfundoiMeSuksesVendosniTeDrejtat", ci), pnlMesazhi);
                }

                #endregion Shtim
            }
            else //eshte i vjeter
            {
                #region Modifikim

                if (!tedrejtaInfo.DMod)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("mesazhRaportNukKeniTeDrejta", ci), pnlMesazhi);
                    return;
                }
                roli.mbushRolSipasId(int.Parse(this.hfId.Value));
                if (DbCore.DbShare.clsAlternativaKushti.getAlternativa(DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdSipasKategoriDheNderm(49, idNdermarrje), "MMRT") == "Po" && clsRolPerdorues.KaPerdoruesiKeteRol(idPerdoruesi, roli.KodRoli))
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgMosModifikoRolinTend", ci), pnlMesazhi);
                    return;
                }
                if (tabetASPxPageControl.ActiveTabIndex == 3) //ruajme te drejtat
                {
                    colTeDrejtaRoliKoka rolet = new colTeDrejtaRoliKoka();  //colTeDrejtaRoli rolet = new colTeDrejtaRoli();
                    object[] teDrejtat = new object[2];
                    try
                    {
                        //eDrejtat = krijoTeDrejtat();
                        teDrejtat = krijoTeGjitheTeDrejtatNeNje();
                    }
                    catch (DbCore.MyException myExeption)
                    {
                        NLog.LogManager.GetCurrentClassLogger().Error(myExeption.Message);
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, myExeption.Message, pnlMesazhi);
                        return;
                    }
                    catch (Exception err)
                    {
                        NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgGabimRuajtje", ci), pnlMesazhi);
                        return;
                    }
                    mesazh = rolet.updateTeGjitheTeDrejtaNeNje(teDrejtat);
                    if (!mesazh.Status)
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgGabimRuajtje", ci), pnlMesazhi);
                        return;
                    }
                    mbushTreeGride(int.Parse(this.hfId.Value), int.Parse(ASPxComboBoxNdermarrje.Value.ToString()), int.Parse(ASPxComboBoxViti.Value.ToString()), lic.IdLlojLicenca);
                }
                roli.PershkrimRoli = pershkrimiASPxTextBox.Text;
                roli.AktivRoli = aktivASPxCheckBox.Checked;
                roli.IdPerdoruesi = IdPerdoruesi;
                ArrayList tedrejta = new ArrayList();
                string[] arr = hfVitetSel.Value.ToString().Split(';');
                for (int i = arr.Length - 1 ; i >= 0; i--)
                {
                    bool ekziston = false;
                    if (arr[i] == "")
                        continue;
                    
                    for (int j = 0; j < tedrejta.Count; j++)
                    {
                        if (arr[i].Split(',')[0] == tedrejta[j].ToString().Split(',')[0]
                            && arr[i].Split(',')[1] == tedrejta[j].ToString().Split(',')[1]
                            && arr[i].Split(',')[3] == tedrejta[j].ToString().Split(',')[3])
                        {
                            ekziston = true;
                            //tedrejta.RemoveAt(j);
                            break;
                        }
                    }
                    if (!ekziston)
                    {
                        tedrejta.Add(arr[i]);
                    }
                }
                if (tedrejta.Count == 0)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgZgjidhniNderrmarrjenDheNjeVit", ci), pnlMesazhi);
                    return;
                }
                mesazh = roli.updateRolin(idGjuha, tedrejta, lic.IdLlojLicenca, hfNdermarjeChanged.Value);
                if (!mesazh.Status)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                    return;
                }
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("labelRaportMesazhRuajtjaPerfundoiSukses", ci), pnlMesazhi);
                hfNdermarjeChanged.Value = "false";
                hfNdryshuarTeDhenaRoli.Value = "false";
                ndermarrjet = new colNdermarrjet(roli.IdRoli);
                mbushComboNdermarrje(ndermarrjet);
                mbushcmbNdermarje(DbCore.mySessionObjects.merrNdermarrjetPerPerdoruesDheLicenceNgaSesioni(Session));
                HiddenFieldNdermarrjeDestinacion.Value = cmbNdermarja.Value.ToString();
                HiddenFieldVitiDestinacion.Value = cmbViti.Value.ToString();
                mbushTreeGride(roli.IdRoli, int.Parse(ASPxComboBoxNdermarrje.Value.ToString()), int.Parse(ASPxComboBoxViti.Value.ToString()), lic.IdLlojLicenca);
                tabetASPxPageControl.ActiveTabIndex = 3;
                modifikoRolNeGrid(roli.IdRoli, ci, rm);
                konfiguroGride(idPerdoruesi);


                #endregion Modifikim
            }
        }

        /// <summary>
        /// Lexon hidden field HfColTeDrejtat qe permban te gjithe ndryshimet nga perdoruesi
        /// </summary>
        /// <returns>Kthen dy objekte (koken dhe trupin e te drejtave)</returns>
        protected object[] krijoTeGjitheTeDrejtatNeNje()
        {
            colTeDrejtaRoliKoka colTeDrejtaKoka = new colTeDrejtaRoliKoka();
            JavaScriptSerializer serializusi = new JavaScriptSerializer();
            object[] teDrejtat = (object[])serializusi.DeserializeObject(HfColTeDrejtat.Value);

            return colTeDrejtaKoka.krijoTeGjitheTeDrejtatNeNje(teDrejtat);
        }                

        protected void btnOk_Click2(object sender, EventArgs e)
        {
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            int idNdermarrjeDestinacion = int.Parse(HiddenFieldNdermarrjeDestinacion.Value.ToString());
            if (idNdermarrjeDestinacion < 1)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgGabimGjateLeximitTeNdermarrjesDestinacion", ci) + cmbNdermarja.Text + "!", pnlMesazhi);
                return;
            }
            int idVitiDestinacion = int.Parse(HiddenFieldVitiDestinacion.Value.ToString());
            if (idVitiDestinacion <= 0)
            {
                clsViti vit = new clsViti();
                vit.mbushVitetMet(cmbViti.Text, idNdermarrjeDestinacion);
                idVitiDestinacion = vit.IdViti;
            }
            if (idVitiDestinacion <= 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgGabimGjateLeximitTeVititDestinacion", ci) + cmbViti.Text + "!", pnlMesazhi);
                return;
            }
            colTeDrejtaRoli tedrejta = new colTeDrejtaRoli();
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            int idNdermarrje = int.Parse(ASPxComboBoxNdermarrje.Value.ToString());
            int idViti = int.Parse(ASPxComboBoxViti.Value.ToString());
            if (idNdermarrje == idNdermarrjeDestinacion && idViti == idVitiDestinacion)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgKlonimiITeDrejtaveNukMundTeBehetNeTeNjejtenNdermarrjeDheVit", ci), pnlMesazhi);
            }
            else
            {
                if (idViti != 0 && idNdermarrje != 0 && idNdermarrjeDestinacion != 0 && idVitiDestinacion != 0)
                    mesazh = tedrejta.klonoTeDrejtaMeRaport(Convert.ToInt32(hfId.Value.ToString()), idNdermarrje, idViti, idNdermarrjeDestinacion, idVitiDestinacion);

                if (mesazh.Status)
                {
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("msgKlonimiITeDrejtavePerfundoiMeSukses", ci), pnlMesazhi);
                    ndermarrjet = new colNdermarrjet(Convert.ToInt32(hfId.Value.ToString()));
                    mbushComboNdermarrje(ndermarrjet);
                    ndermarjesel = ndermarrjet;
                    DbCore.mySessionObjects.ruajNdermarjetSelNeSesion(Session, ndermarjesel);
                    hfNdermSel.Value = "";
                    hfNderm.Value = "";
                    hfVitetSel.Value = "";
                    foreach (clsNdermarrje nder in ndermarrjet)
                    {
                        colVitet vitet = new colVitet();
                        vitet.mbushVitetTeNdermarjesDheRolit(Convert.ToInt32(hfId.Value.ToString()), nder.IdNdermarrje);
                        foreach (clsViti v in vitet)
                            hfVitetSel.Value += nder.IdNdermarrje + "," + v.KodiViti + "," + true + "," + v.IdViti + ";";
                    }
                    mbushListBoxNdermarrjeRol(ndermarjesel);
                }
                else
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgKlonimiTeDrejtavePerfundoiMeGabime", ci), pnlMesazhi);
                }
            }
        }

        protected void cmbViti_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("cmbViti"))
                {
                    colVitet vitet = new colVitet(new DbCore.DbAdmin.clsNdermarrje(this.cmbNdermarja.Text).IdNdermarrje);
                    mbushcmbViti(vitet);
                }
            }
        }

        protected void roletASPxGridView_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string idkomponente = "";
            string kodkonfigurimi = "";
            int idNdermarrje = (int)hfState["idNdermarrje"];
            int idGjuha = (int)hfState["idGjuha"];
            int idPerdoruesi = (int)hfState["idPerdoruesi"];
            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                kodkonfigurimi = arr[1];
                idkomponente = arr[0];
                if (arr[2] == "")
                    roletASPxGridView.FilterExpression = "";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuha, "roletASPxGridView", komponente, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], idNdermarrje, koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        roletASPxGridView.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, roletASPxGridView);
                        CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
                        ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                        konfiguroVleraFillestare(idNdermarrje, idPerdoruesi, rm, ci, idGjuha);
                    }
                }
            }
            else if (arr.Length == 2)//nese eshte zgjedhur nje konfigurim tek combo e konfigurimeve
            {
                kodkonfigurimi = arr[1];
                idkomponente = arr[0];
            }
            else
            {
                idkomponente = e.Parameters;
            }
            GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "roletASPxGridView", roletASPxGridView, cmbKonfigurimi.Text.Split(';')[0], "160", DbCore.mySessionObjects.ktheGjuhe(Session));
            roletASPxGridView.Selection.UnselectAll();
        }

        protected void roletASPxGridView_DataBound(object sender, EventArgs e)
        {
            {// shton colonen # per selektim dhe disa karakteristika te grides
                if (this.roletASPxGridView.Columns["#"] == null)
                {
                    //behet nepermjet kodit afishimi i checkboxit qe do perdoret per
                    //perzgjidh
                    GridViewCommandColumn check = new GridViewCommandColumn("#");
                    check.ShowSelectCheckbox = true;
                    check.Width = Unit.Percentage(2);
                    //behet per te afishuar rreshtin qe do sherbej per filtrim
                    roletASPxGridView.Settings.ShowFilterRow = true;
                    roletASPxGridView.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                    roletASPxGridView.Settings.ShowFilterRowMenu = true;
                    roletASPxGridView.Columns.Add(check);

                    roletASPxGridView.KeyFieldName = "IdRoli";
                    roletASPxGridView.SettingsBehavior.AllowSelectByRowClick = true;
                    roletASPxGridView.SettingsBehavior.AllowFocusedRow = true;
                }
            }
        }

        protected void roletASPxGridView_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if (e.Column.FieldName == "IdKrijuesi")
            {
                if (DbCore.IMBUtils.Types.Converter.ConvertToInt(e.Value) == 0)
                {
                    e.Criteria = null;
                }
            }
        }

        protected void lboxNdermarjetRol_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (IsCallback && (!IsCallback || !Request["__CALLBACKID"].Contains("lboxNdermarjetRol"))) return;
            if (e.Parameters == "-2")
            {
                ndermarjesel = new colNdermarrjet();
                mySessionObjects.RuajNdermarrjeRolNeSession(Session, _guidString, 0, null);
            }
            else if (e.Parameters != "0")
            {
                var idRoli = int.Parse(e.Parameters);
                var ndermarrjeSession = mySessionObjects.MerrNdermarrjeRolNgaSession(Session, (string)hfState["guidString"], idRoli);
                if (ndermarrjeSession != null)
                {
                    ndermarjesel = new colNdermarrjet();
                    foreach (DataRow row in ndermarrjeSession.Rows)
                        if (row[2].ToString() == "TRUE")
                            ndermarjesel.Add(new clsNdermarrje(Convert.ToInt32(row[0].ToString())));
                }
                else
                    ndermarjesel = new colNdermarrjet(idRoli);
                mbushComboNdermarrje(ndermarjesel);
                if (hfVitetSel.Value == "")
                {
                    foreach (var nder in ndermarjesel)
                    {
                        colVitet vitet = new colVitet();
                        vitet.mbushVitetTeNdermarjesDheRolit(idRoli, nder.IdNdermarrje);
                        foreach (clsViti vit in vitet)
                            hfVitetSel.Value += nder.IdNdermarrje + "," + vit.KodiViti + "," + true + "," + vit.IdViti + ";";
                    }
                }
            }
            else if (e.Parameters == "0")
            {
                ndermarjesel = new colNdermarrjet();
                var ndermarrjetId = mySessionObjects.MerrNdermarrjeRolNgaSession(Session, (string)hfState["guidString"], Convert.ToInt32(e.Parameters));
                if (ndermarrjetId != null)
                {
                    foreach (DataRow row in ndermarrjetId.Rows)
                        if (row[2].ToString() == "TRUE")
                            ndermarjesel.Add(new clsNdermarrje(Convert.ToInt32(row[0].ToString())));
                }
            }
            mySessionObjects.ruajNdermarjetSelNeSesion(Session, ndermarjesel);
            mbushListBoxNdermarrjeRol(ndermarjesel);
        }

        private void konfiguroGrideLidhjeRolPerdorues()
        {
            GridUtil.konfigGrideListeEMadhePaTheme(gridLidhjeRole, "IdRolPerdorues");
            gridLidhjeRole.Columns["#"].VisibleIndex = 0;
            gridLidhjeRole.Columns["IdRolPerdorues"].Visible = false;
            gridLidhjeRole.DataBind();
        }

        /// <summary>
        /// perdoret per te inicializuar griden e lidhjeve te autorizimeve per autorizimin e selektuar
        /// </summary>
        /// <param name="idAutorizim"></param>
        /// <param name="idGjuha"></param>
        private void mbushLidhjeRolPerdorues(int idRoli, int idGjuha)
        {
            DbCore.DbAdmin.colRolPerdorues colRolPerdorues = new DbCore.DbAdmin.colRolPerdorues();
            DataTable dt = colRolPerdorues.ktheRolePerdoruesishSipasIdRoliDT(idRoli);
            gridLidhjeRole.DataSource = dt;
            gridLidhjeRole.DataBind();
        }

        protected void gridLidhjeRole_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            mbushLidhjeRolPerdorues(Convert.ToInt32(hfId.Value), DbCore.mySessionObjects.ktheGjuhe(Session));
        }

        /// <summary>
        /// perdoret per te vendosur filtrat tek headeri i grides
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gridLidhjeRole_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            string TeGjithe = rm.GetString("GridHeaderFilterFillItemTeGjithe", ci);
            string nga = rm.GetString("GridHeaderFilterFillItemNga", ci);
            if (e.Column.FieldName == "Emri" || e.Column.FieldName == "Mbiemri" || e.Column.FieldName == "Username")
            {
                e.Values.Clear();
                e.AddValue(TeGjithe, string.Empty, "true");
                e.AddValue(nga + " A-D", string.Empty, e.Column.FieldName + ">'A     ' and " + e.Column.FieldName + " <'DDDDDDD'");
                e.AddValue(nga + " D-G ", string.Empty, e.Column.FieldName + ">'D     ' and " + e.Column.FieldName + "<'GGGGGGG'");
                e.AddValue(nga + " H-K ", string.Empty, e.Column.FieldName + ">'H     ' and " + e.Column.FieldName + "<'KKKKKKK'");
                e.AddValue(nga + " L-O ", string.Empty, e.Column.FieldName + ">'L     ' and " + e.Column.FieldName + "  <'OOOOOOO'");
                e.AddValue(nga + " P-S ", string.Empty, e.Column.FieldName + ">'P     ' and " + e.Column.FieldName + "<'SSSSSSS'");
                e.AddValue(nga + " T-W ", string.Empty, e.Column.FieldName + ">'T     ' and " + e.Column.FieldName + "<'WWWWWWW'");
                e.AddValue(nga + " X-Z ", string.Empty, e.Column.FieldName + ">'X     ' and " + e.Column.FieldName + "<'ZZZZZZZ'");
            }
            else
            {
                e.Values.Clear();
                e.AddValue(TeGjithe, string.Empty, "true");
            }
        }

        protected void gridLidhjeRole_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            gridLidhjeRole.FilterExpression = "";
            gridLidhjeRole.Selection.UnselectAll();
            int idRoli = Convert.ToInt32(this.roletASPxGridView.GetRowValues(int.Parse(e.Parameters), "IdRoli"));
            mbushLidhjeRolPerdorues(idRoli, DbCore.mySessionObjects.ktheGjuhe(Session));
            konfiguroGrideLidhjeRolPerdorues();
            gridLidhjeRole.DataBind();
            gridLidhjeRole.PageIndex = 0;
        }

        protected void gridLidhjeRole_DataBound(object sender, EventArgs e)
        {
            if (gridLidhjeRole.Columns["#"] == null)
            {
                DevExpress.Web.GridViewCommandColumn check = new DevExpress.Web.GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true;
                check.Width = Unit.Percentage(3);
                gridLidhjeRole.Settings.ShowFilterRow = true;
                gridLidhjeRole.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                gridLidhjeRole.Settings.ShowFilterRowMenu = true;
                gridLidhjeRole.Columns.Add(check);
                gridLidhjeRole.KeyFieldName = "IdRolPerdorues";
                gridLidhjeRole.SettingsBehavior.AllowSelectByRowClick = true;
                gridLidhjeRole.SettingsBehavior.AllowFocusedRow = true;
            }
        }

        protected void lboxNdermarjetRol_OnHtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (((DevExpress.Web.ASPxGridView)(sender)).VisibleColumns.Count == 0 || e.RowType != GridViewRowType.Data)
                return;
            
            colNdermarrjet colNdermarje = DbCore.mySessionObjects.merrNdermarrjetSelNgaSesioni(Session);

            if (!(colNdermarje.Count > 0 && e.VisibleIndex + 1 < colNdermarje.Count))
                return;

            colVitet vitet = colVitet.merrGjitheVitetEMundshme();
            foreach (clsViti v in vitet)
            {
                GridViewDataCheckColumn col2 = ((ASPxGridView)sender).Columns[v.KodiViti] as GridViewDataCheckColumn;

                if(col2 == null)
                {
                    ImbLogger.Warn($"Rolet- lboxNdermarjetRol_OnHtmlRowCreated Kolona per vitin {v.KodiViti} eshte null!");
                    return;
                }

                col2.DataItemTemplate = new MyCheckTemplate(false, true);
                

                int id = colNdermarje[e.VisibleIndex + 1].IdNdermarrje;
                colVitet viteNder = new colVitet(id);
                foreach (clsViti vn in viteNder)
                {
                    if (vn.KodiViti != v.KodiViti)
                        continue;
                    
                    bool check = false;
                    string[] arr = hfVitetSel.Value.ToString().Split(';');
                    for (int i = 0; i < arr.Length; i++)
                        if (arr[i].Split(',')[0] == id.ToString() && arr[i].Split(',')[1] == v.KodiViti)
                            check = bool.Parse(arr[i].Split(',')[2]);
                    col2.DataItemTemplate = new MyCheckTemplateGrid(e.VisibleIndex + 1, v.KodiViti, check);
                    
                }   
                    
                
            }
            
        }
    }
}