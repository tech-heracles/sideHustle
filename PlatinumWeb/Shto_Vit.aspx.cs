using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Drawing;
using System.Data;
using PlatinumWeb.Templates;
using System.Globalization;
using System.Resources;
using DbCore;
using System.Web.Configuration;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class Shto_Vit : MyPageBase
    {
        DbCore.DbAdmin.clsViti viti;
        private int idPerdoruesi, idNdermarrje, idviti, idgjuha;
        private string komponente = "Shto_Vit.aspx";
        private string guidString;
       
        /// <summary>
        /// thirret kur faqja lodohet. Ne te behet kontrolli nese perdoruesi eshte i loguar ne sistem  dhe nqs jo ridrejtohet tek forma e logimit
        /// thirret inicializimi i konfigurimeve fillestare te faqes
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e"> argumenti</param>
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                DbCore.clsFunksione.logout(Session,true,"FaqePaautorizuar");
            }
            idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);

            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerdoruesi);
            }
            idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            idviti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            idgjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            percaktoTemplateMenu(idgjuha, idviti, idPerdoruesi, idNdermarrje, ASPxMenu1);
            if (!Page.IsPostBack)
            {
                emertimetDheMesazhetEPerkthyera(rm, ci);
                hfState.Set("idGjuha", idgjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                konfiguroVleraFillestare(idPerdoruesi, idNdermarrje, rm, ci, idgjuha);
                ASPxPageControl1.ActiveTabIndex = 0;
                mbushGridViteshNgaDB();
                konfiguroGride(idPerdoruesi, cmbKonfigurimi.Text.Split(';')[0], 215, rm, ci);
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), "ASPxGridView_Vitet", ASPxGridView_Vitet, cmbKonfigurimi.Text.Split(';')[0], 215.ToString(), DbCore.mySessionObjects.ktheGjuhe(Session));
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), komponente);
               
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                hfVod.Value = WebConfigurationManager.AppSettings["BuxhetQK"];
                guidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                hfState.Set("guidString", guidString);
            }
            else
            {
                guidString = (string)hfState["guidString"];
                mbushGridViteshNgaSession();
                konfiguroGride(idPerdoruesi, cmbKonfigurimi.Text.Split(';')[0], 215, rm, ci);
            }
            GridUtil.konfigGrideListeEMadhePaTheme(ASPxGridView_Vitet, "IdViti");
            AspxWebControlUtils.perkthePopUp(popFshi, rm.GetString("labelKujdes", ci), lblMsgbox, rm.GetString("labelAdministrimiMsgJeniSigurt", ci), ButtonCancel, rm.GetString("labelAnullo", ci));
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "ASPxGridView_Vitet", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
            this.ASPxGridView_Vitet.Columns["#"].VisibleIndex = 0;
            GridUtil.ToolTipButonaveMbiGride(ASPxGridView_Vitet, ci, rm);
        }

        /// <summary>
        /// Vendos emrat e tabeve ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void emertimetDheMesazhetEPerkthyera(ResourceManager rm, CultureInfo ci)
        {
            ASPxPageControl1.TabPages[0].Text = rm.GetString("labelAdministrimiTePergjithshme", ci);
            ASPxPageControl1.TabPages[1].Text = rm.GetString("labelViti", ci);
            ASPxPageControl1.TabPages[2].Text = rm.GetString("RadioButtonListEditItemPeriudha", ci);
            konfigurimi_Label.Text = rm.GetString("lblModeli", ci);
            popupUniversal.HeaderText = rm.GetString("headerPopUpText", ci);
            hfState.Set("msgVitetDitaEFillimitTeVititDuhetTeJeteEParaEMuajitTeZgjedhur", rm.GetString("msgVitetDitaEFillimitTeVititDuhetTeJeteEParaEMuajitTeZgjedhur", ci));
            hfState.Set("headerPopUpText", rm.GetString("headerPopUpText", ci));
            hfState.Set("msgVitetDuhetTeZgjidhni1Vit", rm.GetString("msgVitetDuhetTeZgjidhni1Vit", ci));
            hfState.Set("msgVitetVitiDuhetTeJeteIPlote", rm.GetString("msgVitetVitiDuhetTeJeteIPlote", ci));
            hfState.Set("msgVitetPopUpMbylljeVitiAzhurnimiKlientetLlogarite", rm.GetString("msgVitetPopUpMbylljeVitiAzhurnimiKlientetLlogarite", ci));
        }

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
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

            int idfiltri = 0;
            int idndermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idperdorues = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            int idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            int idgjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            int idkonf = DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(cmbKonfigurimi.Text.Split(';')[0], idndermarrje);
            DbCore.clsMesazh mesazh = GridUtil.ruajFiltra(idndermarrje, idperdorues, idgjuha, "ASPxGridView_Vitet", komponente, "FilterDefault", ASPxGridView_Vitet.FilterExpression, ASPxGridView_Vitet, "KodiViti", idkonf, out idfiltri);//ruan filtrin e zgjedhur tek filtrat
            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                return;

            }
            mesazh = GridUtil.ruajkonfigurimgride(ASPxGridView_Vitet, cmbKonfigurimi.Text, idndermarrje, idperdorues, 215, idfiltri, idViti, DbCore.mySessionObjects.ktheCultureInfo(Session), DbCore.mySessionObjects.ktheGjuhe(Session));/// ruan konfigurimin e grides dhe filtrin

            clsToolbarConfig.mbushComboBoxFiltra(idgjuha, idndermarrje, "ASPxGridView_Vitet", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);

            percaktoTemplateMenu(idgjuha, idViti, idperdorues, idndermarrje, ASPxMenu1);
            if (mesazh.Status) clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

        }

        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(idgjuha, idviti, idPerdoruesi, idNdermarrje, ASPxMenu1);

        }
        /// <summary>
        /// perdoret per te shtuar kolonen e selektimit tek grida dhe per te vendosur disa karakteristika te grides
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxGridView_Vitet_DataBound(object sender, EventArgs e)
        {
            if (this.ASPxGridView_Vitet.Columns["#"] == null)
            {
                GridViewCommandColumn check = new GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true; check.Width = Unit.Percentage(2);
                ASPxGridView_Vitet.Settings.ShowFilterRow = true;
                ASPxGridView_Vitet.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                ASPxGridView_Vitet.Settings.ShowFilterRowMenu = true;
                ASPxGridView_Vitet.Columns.Add(check);

                ASPxGridView_Vitet.KeyFieldName = "IdViti";
                ASPxGridView_Vitet.SettingsBehavior.AllowSelectByRowClick = true;
                ASPxGridView_Vitet.SettingsBehavior.AllowFocusedRow = true;
            }
        }
        /// <summary>
        /// ben konfigurimin e grides sipas konfigurimit te zgjedhur
        /// </summary>
        /// <param name="idPerdoruesi"></param>
        private void konfiguroGride(int idPerdoruesi, string kodKonfigurimi, int idKomponente, ResourceManager rm, CultureInfo ci)
        {
            KonfigurimComboGride.ShtoLlogari(ASPxGridView_Vitet, idPerdoruesi, Session, komponente, guidString, "IdLlogMbylljeViti");
            KonfigurimComboGride.ShtoLlojSipasPeriudhesSeVitit(ASPxGridView_Vitet, rm, ci);
            percaktoTamplate();
            this.ASPxGridView_Vitet.Columns["#"].VisibleIndex = 0;
        }

        protected void ASPxGridView_Vitet_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            if (e.CallbackName == "COLUMNMOVE" && ASPxGridView_Vitet.AllColumns[int.Parse(e.Args[0])].Width.Value == 0)
                ASPxGridView_Vitet.AllColumns[int.Parse(e.Args[0])].Width = Unit.Percentage(3);
            if (e.CallbackName == "APPLYFILTER" && e.Args[0] == "")
            {
                DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
                ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
                cmbFiltra.Text = "";
            }
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            GridUtil.ToolTipButonaveMbiGride(ASPxGridView_Vitet, ci, rm);
        }
        protected void ASPxGridView_Vitet_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            string TeGjithe = rm.GetString("GridHeaderFilterFillItemTeGjithe", ci);
            string nga = rm.GetString("GridHeaderFilterFillItemNga", ci);
            if (e.Column.FieldName == "KodiViti")
            {
                e.Values.Clear();
                e.AddValue(TeGjithe, string.Empty, "true");
                e.AddValue(nga + " A-D ", string.Empty, e.Column.FieldName + ">'A     ' and " + e.Column.FieldName + " <'DDDDDDD'");
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
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "ASPxGridView_Vitet", komponente, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();
                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                cmbFiltra.Text = "";
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "ASPxGridView_Vitet", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
                percaktoTemplateMenu(idgjuha, idviti, idPerdoruesi, idNdermarrje, ASPxMenu1);
                
                hfStatusi.Value = "true";
                this.ASPxGridView_Vitet.FilterExpression = String.Empty;
            }
        }
        /// <summary>
        /// Aplikon filtrin e zgjedhur mbi gride
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Apliko_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;

            DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuha, "ASPxGridView_Vitet", komponente, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                ASPxGridView_Vitet.FilterExpression = filtra.FiltraVlera;
                GridUtil.renditGriden(filtra.KoloneRenditje, ASPxGridView_Vitet);

                int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
                ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                konfiguroVleraFillestare(idPerdoruesi, idNdermarrje, rm, ci, idGjuha);
                hfStatusi.Value = "true";
                ASPxButton btnFshiFilter = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFshi") as ASPxButton;
                btnFshiFilter.ClientEnabled = true;

            }
            else hfStatusi.Value = "false";
            if (cmbFiltra.Text != "" && filtra.FiltraKodi == null)
            {
                ASPxButton btnRuaj = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("Button1") as ASPxButton;

                btnRuaj.ClientEnabled = true;

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
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DbCore.DbAdmin.clsFiltraGrida filtri = new DbCore.DbAdmin.clsFiltraGrida();
            filtri.FiltraKodi = cmbFiltra.Text;
            filtri.FiltraShenime = cmbFiltra.Text;
            filtri.FiltraUniversal = false;// Universal_ASPxCheckBox.Checked;
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "ASPxGridView_Vitet", komponente, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = ASPxGridView_Vitet.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("KodiViti", ASPxGridView_Vitet);

            filtri.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            filtri.IdNdermarje = idNdermarrje;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            filtri.IdStatusDok = 1;

            mesazh = filtri.ruaj();
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "ASPxGridView_Vitet", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
            percaktoTemplateMenu(idgjuha, idviti, idPerdoruesi, idNdermarrje, ASPxMenu1);

            if (mesazh.Status == true)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

            hfStatusi.Value = "true";
            cmbFiltra.Text = "";
        }
        /// <summary>
        /// perdoret per te fshire reshtat e zgjedhur ne gride pasi perdoruesi ka konfirmuar fshirjen
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            List<object> rreshtat;
            if (ASPxPageControl1.ActiveTabIndex == 0)//nqs kemi zgjedhur vetem nje kartele per te pare fshijme vetem ate
                rreshtat = ASPxGridView_Vitet.GetSelectedFieldValues("IdViti");
            else
            {
                rreshtat = new List<object>();
                rreshtat.Add(hfId.Value);
            }
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (rreshtat.Count == 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgVitetZgjidhniTePakten1Vit", ci), pnlMesazhi);
                return;
            }
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            List<string> TeFshire = new List<string>(), TePaFshire = new List<string>();
            DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            foreach (object id in rreshtat)
            {
                DbCore.DbAdmin.clsViti clsVitet = new DbCore.DbAdmin.clsViti();
                clsVitet.mbushVitetMet(Convert.ToInt32(id));

                DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                konf.mbushKonfigAmbjSipasId(clsVitet.IdKonfig, DbCore.mySessionObjects.ktheGjuhe(Session));
               
                bool lidhur = dbAdmin.ekzistonNdermarrjeSipasIdViti(clsVitet.IdViti);
                if (lidhur)
                {
                    TePaFshire.Add(clsVitet.KodiViti);
                    continue;
                }
                clsVitet.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                mesazh = clsVitet.fshi();
                if (clsVitet.IdViti == 0)
                    continue;
                if (mesazh.Status)
                {
                    #region Heq llogarite nga grida

                    // ASPxGridView_Artikull.DataSource = dt;
                    hiqVitNgaGrida(clsVitet.IdViti, rm, ci);
                    #endregion
                    TeFshire.Add(clsVitet.KodiViti);

                    ASPxPageControl1.ActiveTabIndex = 0;
                    hfStatusi.Value = "true";
                }

            }
            dbAdmin.Dispose();
            string mesazhInfoGabim = "", mesazhInfoSukses = "";
            if (TePaFshire.Count == 1)
                mesazhInfoGabim = String.Format("{0}{1}{2}", rm.GetString("msgVitetPrefixNjejes", ci), String.Join(";", TePaFshire), rm.GetString("msgVitetSuffixNjejesGabimi", ci));
            else
                if (TePaFshire.Count > 1)
                    mesazhInfoGabim = String.Format("{0}{1}{2}", rm.GetString("msgVitetPrefixShumes", ci), String.Join(";", TePaFshire), rm.GetString("msgVitetSuffixShumesGabimi", ci));
            if (TeFshire.Count == 1)
                mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("msgVitetPrefixNjejes", ci), String.Join(";", TeFshire), rm.GetString("regjMagSuffixMesazhNjejesSuksesi", ci));
            else
                if (TeFshire.Count > 1)
                    mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("msgVitetPrefixShumes", ci), String.Join(";", TeFshire), rm.GetString("msgCeljeMagazinatSuffixShumesSuksesi", ci));
            if (mesazhInfoGabim != "" && mesazhInfoSukses != "")
                mesazhInfoGabim += rm.GetString("msgLidhesMesazhi", ci) + mesazhInfoSukses;
            if (mesazhInfoGabim != "")
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhInfoGabim, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazhInfoSukses, pnlMesazhi);
            ASPxGridView_Vitet.Selection.UnselectAll();
        }

        protected void ButtonOk2_Click(object sender, EventArgs e)
        {
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            DbCore.DbAdmin.clsViti viti = new DbCore.DbAdmin.clsViti();
            viti.mbushVitetMet(int.Parse(hfId.Value.ToString()));
            DbCore.DbKontabiliteti.clsKokaFleteKontabel fk = new DbCore.DbKontabiliteti.clsKokaFleteKontabel();
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konf.mbushKonfigDefaultKomponentes(116, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));//id komponente e shto flete kontabel
            DbCore.DbShare.clsKonfigurimAmbjenti konfv = new DbCore.DbShare.clsKonfigurimAmbjenti();
            DbCore.DbShare.colKonfigurimAmbjenti colKonf = new DbCore.DbShare.colKonfigurimAmbjenti();
            colKonf.mbushKonfigAmbjSipasIdKategori(22, -1, DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.ktheGjuhe(Session));
            konfv = colKonf[0];
            DbCore.DbShare.clsKonfigurimAmbjenti konfvkos = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konfvkos.IdKategori = 22;
            konfvkos.IdNdermarje = -3;
            DbCore.DbShare.colKonfigurimAmbjenti colKonfkos = new DbCore.DbShare.colKonfigurimAmbjenti();
            colKonfkos.mbushKonfigAmbjSipasIdKategori(konfvkos.IdKategori, konfvkos.IdNdermarje, DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.ktheGjuhe(Session));
            if (colKonfkos.Count > 0) konfvkos = colKonfkos[0];

            DbCore.DbAdmin.clsNdermarrjeViti ndervit = new DbCore.DbAdmin.clsNdermarrjeViti(DbCore.mySessionObjects.ktheNdermarrjeVit(Session));
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (ndervit.IdViti != viti.IdViti)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgVitetNukMundTeMbyllniVitinKuJeniLoguar", ci), pnlMesazhi);
                hfStatusi.Value = "false";
                return;
            }
            mesazh = fk.MbyllVitin(DbCore.mySessionObjects.ktheNdermarrjeVit(Session), viti, DateTime.Now, konf, DbCore.mySessionObjects.ktheIdPerdoruesi(Session), 20, 5, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), konfv.IdKonfigAmbjente, konfvkos.IdKonfigAmbjente);//5 idkategoria//20 llojdokumenti mbyllje viti

            if (mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("msgVitetVitiUshtrimorUMbyllMeSukses", ci), pnlMesazhi);
                hfStatusi.Value = "true";
                mbushGridViteshNgaDB();

            }
            else
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                hfStatusi.Value = "false";
            }

        }
        private void hiqVitNgaGrida(int idviti, ResourceManager rm, CultureInfo ci)
        {
            if (this.ASPxGridView_Vitet.DataSource != null)
            {
                DataTable dt = (DataTable)ASPxGridView_Vitet.DataSource;
                DataRow[] drs = dt.Select("IdViti = " + idviti);
                if (drs.Length > 1)
                    throw new DbCore.MyException(rm.GetString("msgVitetNdodhen2ViteMeTeNjejtenIDNeGride", ci));
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                dt.Rows.Remove(dr);
                ASPxGridView_Vitet.DataBind();
            }
            else mbushGridViteshNgaDB();
        }
        private void shtoVitNeGrid(int idNdermarrje, int idviti, ResourceManager rm, CultureInfo ci)
        {
            if (ASPxGridView_Vitet.DataSource != null)
            {
                DataTable dt = (DataTable)ASPxGridView_Vitet.DataSource;
                DataRow[] drs = dt.Select("IdViti = " + idviti);
                if (drs.Length > 0)
                    throw new DbCore.MyException(rm.GetString("msgVitetEkzistonVitiNeGride", ci));
                DataRow newArtDr = DbCore.DbAdmin.colVitet.merrViteSipasNdermarjesDR(idNdermarrje, idviti);
                dt.ImportRow(newArtDr);
            }
            else mbushGridViteshNgaDB();
        }
        private void modifikoVitNeGrid(int idNdermarrje, int idviti, ResourceManager rm, CultureInfo ci)
        {
            if (ASPxGridView_Vitet.DataSource != null)
            {
                DataTable dt = (DataTable)ASPxGridView_Vitet.DataSource;
                DataRow[] drs = dt.Select("IdViti = " + idviti);
                if (drs.Length > 1)
                    throw new DbCore.MyException(rm.GetString("msgVitetNdodhen2ViteMeTeNjejtenIDNeGride", ci));
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                DataRow newArtDr = DbCore.DbAdmin.colVitet.merrViteSipasNdermarjesDR(idNdermarrje, idviti);
                object[] arr = newArtDr.ItemArray;
                dr.ItemArray = arr;
            }
            else mbushGridViteshNgaDB();
        }

        /// <summary>
        /// metoda per te thirrur veprimet e menuse kur shtypen butonat
        /// </summary>
        /// <param name="source">derguesi</param>
        /// <param name="e">parametrat</param>
        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {
            if (e.Item.Name == "Ruaj")
            {
                Page.Validate("entries");
                ruajVit();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumenti</param>
        protected void ASPxGridView_Vitet_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if (e.Value.ToString() == "0")
            {
                e.Criteria = null;
            }
        }
        /// <summary>
        /// ruan disa karakteristika te grides
        /// </summary>
        /// <param name="sender">dergues</param>
        /// <param name="e">argumentat</param>
        protected void ASPxGridView_Vitet_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = ASPxGridView_Vitet.PageIndex;
            e.Properties["cpPageRow"] = ASPxGridView_Vitet.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = ASPxGridView_Vitet.VisibleRowCount;
        }

        private void konfiguroVleraFillestare(int idPerdoruesi, int idNdermarrje, ResourceManager rm, CultureInfo ci, int idGjuha)
        {
            mbushComboLloji(cmbPeriudhaLloji);
            ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 22, rm, ci, idGjuha);
            ConfigureAspxComboBox.percaktoTemplateComboMeEnableCallback(txtLlogMbylljeViti);
            cmbKonfigurimi.SelectedIndex = 0;
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            mbushGridePeriudha();
            konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()), idGjuha);
            hfKonffillestar.Value = String.Format("{0};{1}", konf.KodKonfigAmbjente, konf.PershkrimKonfigAmbjente);
        }

        private void mbushGridViteshNgaSession()
        {
            DataTable tmpObject;
            bool sukses = DbCore.mySessionObjects.merrGrideNgaSessioni(Session, out tmpObject);
            if (!sukses)
                mbushGridViteshNgaDB();
            else
            {
                ASPxGridView_Vitet.DataSource = tmpObject;
                ASPxGridView_Vitet.DataBind();
                tmpObject.Dispose();
            }
        }
        private void mbushGridViteshNgaDB()
        {//mbush griden e popupit me te dhena            
            DataTable dt = DbCore.DbAdmin.colVitet.merrVitetNdermarjeDT(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            DbCore.mySessionObjects.ruajGrideNeSession(Session, dt);
            ASPxGridView_Vitet.DataSource = dt;
            ASPxGridView_Vitet.DataBind();
            dt.Dispose();
        }
        public void mbushGrideVitesh()
        {
            DbCore.DbAdmin.colVitet colVitet = new DbCore.DbAdmin.colVitet();
            colVitet.merrGjitheVitetENdermarjes(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            ASPxGridView_Vitet.DataSource = colVitet;
            ASPxGridView_Vitet.DataBind();
        }
        public void mbushGridePeriudha()
        {
        }
        public void mbushComboLloji(ASPxComboBox cmbPeriudhaLloji)
        {
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            cmbPeriudhaLloji.Items.Add(rm.GetString("cmbItemNjeMujore", ci), 1);
            cmbPeriudhaLloji.Items.Add(rm.GetString("cmbItemDyMujore", ci), 2);
            cmbPeriudhaLloji.Items.Add(rm.GetString("cmbItemTreMujore", ci), 3);
            cmbPeriudhaLloji.Items.Add(rm.GetString("cmbItemKaterMujore", ci), 4);
            cmbPeriudhaLloji.Items.Add(rm.GetString("cmbItemGjashteMujore", ci), 5);
            cmbPeriudhaLloji.IncrementalFilteringMode = DevExpress.Web.IncrementalFilteringMode.Contains;

        }
        private void konfiguroGridePeriudhash()
        {//konfigurohet grida
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            percaktoTamplatePeriudha();
            GridUtil.percaktoVisibleColumnsMeWidth(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), gvPeriudha, "gvPeriudha", komponente);
            GridUtil.konfiguroGrideRegjistrimEvogelPaTheme(gvPeriudha, "IdPeriudha", false);
            GridViewDataColumn col1 = gvPeriudha.Columns["PostoEpayslip"] as GridViewDataColumn;
            col1.Visible = false;
            GridViewDataColumn col2 = gvPeriudha.Columns["PostoMemoBonus"] as GridViewDataColumn;
            col2.Visible = false;
            GridViewDataColumn col3 = gvPeriudha.Columns["PostoAnnualDeclaration"] as GridViewDataColumn;
            col3.Visible = false;
        }

        private void percaktoTamplate()
        {
            GridViewDataColumn col = ASPxGridView_Vitet.Columns["PeriudhaHapjes"] as GridViewDataColumn;
            col.DataItemTemplate = new MyCheckTemplate(true, false);
            GridViewDataColumn col1 = ASPxGridView_Vitet.Columns["PeriudhaMbylljes"] as GridViewDataColumn;
            col1.DataItemTemplate = new MyCheckTemplate(true, false);
        }
        private void percaktoTamplatePeriudha()
        {
            GridViewDataColumn col = gvPeriudha.Columns["Ekycur"] as GridViewDataColumn;
            col.DataItemTemplate = new PlatinumWeb.Templates.MyCheckTemplate(false, false);
        }
        public void ruajVit()
        {
            DbCore.DbAdmin.clsViti viti;
            if (Page.IsValid == false)
                return;

            int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (isValidVit(idNdermarrje, rm, ci))
            {
                bool eshteShtim;
                viti = krijoVit(idPerdoruesi);
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                konf.IdKategori = 22;
                konf.IdNdermarje = -1;
                DbCore.DbShare.colKonfigurimAmbjenti colKonf = new DbCore.DbShare.colKonfigurimAmbjenti();
                colKonf.mbushKonfigAmbjSipasIdKategori(konf.IdKategori, konf.IdNdermarje, idPerdoruesi, idGjuha);
                konf = colKonf[0];
                DbCore.DbShare.clsKonfigurimAmbjenti konfvkos = new DbCore.DbShare.clsKonfigurimAmbjenti();
                konfvkos.IdKategori = 22;
                konfvkos.IdNdermarje = -3;
                DbCore.DbShare.colKonfigurimAmbjenti colKonfkos = new DbCore.DbShare.colKonfigurimAmbjenti();
                colKonfkos.mbushKonfigAmbjSipasIdKategori(konfvkos.IdKategori, konfvkos.IdNdermarje, idPerdoruesi, idGjuha);
                if (colKonfkos.Count > 0) konfvkos = colKonfkos[0];
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), komponente);

                if (!tedrejtaInfo.DMod)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNukKeniTeDrejta", ci), pnlMesazhi);
                    hfStatusi.Value = "false";
                    return;
                }
                eshteShtim = false;
                DbCore.DbRegjistrim.clsDatabaseRegjistrim dbRegjistrim = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
                konf.mbushKonfigAmbjSipasId(viti.IdKonfig, idGjuha);
                
                viti.IdViti = int.Parse(hfId.Value.ToString());
                bool lidhur = dbRegjistrim.eshteDokumentiILidhurCelje(viti.IdViti.ToString(), konf.IdNivel.ToString());
                if (lidhur.ToString() != hfLidhur.Value)
                {
                    mesazh.Status = false;
                    mesazh.PershkrimMesazhi = rm.GetString("msgVitetKaVeprimeMeKeteVit", ci);
                }
                else
                    mesazh = viti.modifiko(ruajPeriudha(viti.IdViti), Session, idGjuha);
                dbRegjistrim.Dispose();

                if (mesazh.Status)
                {
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("labelRaportMesazhRuajtjaPerfundoiSukses", ci), pnlMesazhi);
                    hfStatusi.Value = "true";
                    if (eshteShtim)
                        shtoVitNeGrid(idNdermarrje, viti.IdViti, rm, ci);
                    else //modifikim
                        modifikoVitNeGrid(idNdermarrje, viti.IdViti, rm, ci);
                }
                else
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgAdministrimiRuajtjaPerfundoiGabime", ci), pnlMesazhi);
                    hfStatusi.Value = "false";
                }
                ASPxPageControl1.ActiveTabIndex = 0;

            }
            pnlMesazhi.Update();
        }

        public DbCore.DbAdmin.clsViti krijoVit(int idPerdoruesi)
        {
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DbCore.DbShare.clsKonfigurimAmbjenti konfig = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konfig.mbushKonfigAmbjSipasKod(cmbKonfigurimi.Text, idNdermarrje);
            int idllogari = DbCore.DbKontabiliteti.clsLlogari.mbushIDLlogariSipasKodit(this.txtLlogMbylljeViti.Text.Split(';')[0], idNdermarrje);
            DateTime mbyllurme = new DateTime();
            if (this.lblDateMbyllurMe.Text != "")
                mbyllurme = DateTime.Parse(lblDateMbyllurMe.Text);
            viti = new DbCore.DbAdmin.clsViti(txtKodi.Text, dteFillimiViti.Date, dteMbarimiViti.Date, cmbPeriudhaLloji.Value.ToString(), cbPeriudhaHapjes.Checked,
                cbPeriudhaMbylljes.Checked, idPerdoruesi, konfig.IdKonfigAmbjente, idNdermarrje, 1, mbyllurme, idllogari);
            return viti;
        }
        private DbCore.DbAdmin.colPeriudhaKontabel ruajPeriudha(int idviti)
        {
            int llojperiudha = 1;
            if (cmbPeriudhaLloji.Value != null)
                int.TryParse(cmbPeriudhaLloji.Value.ToString(), out llojperiudha);

            DbCore.DbAdmin.colPeriudhaKontabel periudhat = new DbCore.DbAdmin.colPeriudhaKontabel();
            periudhat.merrSipasViti(idviti);
            string[] kyc = hfKycje.Value.ToString().Split(',');
            int i = 0;
            foreach (DbCore.DbAdmin.clsPeriudhaKontabel p in periudhat)
            {
                p.Ekycur = bool.Parse(kyc[i].Split(':')[1]);

                if (!p.Ekycur)
                {
                    p.PostoEpayslip = false;
                    p.PostoAnnualDeclaration = false;
                    p.PostoMemoBonus = false;

                }
                i++;
            }
            return periudhat;
        }
        private DbCore.DbAdmin.colPeriudhaKontabel llogaritPeriudhat(int llojperiudha)
        {
            DbCore.DbAdmin.colPeriudhaKontabel periudhat = new DbCore.DbAdmin.colPeriudhaKontabel();
            String[] muajt = { "Janar", "Shkurt", "Mars", "Prill", "Maj", "Qershor", "Korrik", "Gusht", "Shtator", "Tetor", "Nentor", "Dhjetor" };
            if (cbPeriudhaHapjes.Checked == true)
            {
                DbCore.DbAdmin.clsPeriudhaKontabel per = new DbCore.DbAdmin.clsPeriudhaKontabel();
                per.NrPeriudha = 0;
                per.EmerPeriudha = DbCore.DbAdmin.clsPeriudhaKontabel.PeriudheFillestare;
                per.Ekycur = false;
                per.FillimiPeriudha = dteFillimiViti.Date;
                per.MbarimiPeriudha = dteFillimiViti.Date;
                periudhat.Add(per);
            }
            int periudhavjetore = ((dteMbarimiViti.Date.Month - dteFillimiViti.Date.Month) + 1) + 12 * (dteMbarimiViti.Date.Year - dteFillimiViti.Date.Year);

            if (llojperiudha == 1)
            {

                for (int i = 0; i < periudhavjetore; i++)
                {
                    int month = (dteFillimiViti.Date.Month + i);
                    DbCore.DbAdmin.clsPeriudhaKontabel per = new DbCore.DbAdmin.clsPeriudhaKontabel();
                    per.NrPeriudha = i + 1;
                    per.EmerPeriudha = muajt[(month - 1) % 12];
                    per.Ekycur = false;
                    per.FillimiPeriudha = dteFillimiViti.Date.AddMonths(i);
                    per.MbarimiPeriudha = dteFillimiViti.Date.AddMonths(i + 1).AddDays(-1);
                    if (month == dteMbarimiViti.Date.Month + 12 * (dteMbarimiViti.Date.Year - dteFillimiViti.Date.Year))
                        per.MbarimiPeriudha = dteMbarimiViti.Date;
                    periudhat.Add(per);
                }

            }
            else if (llojperiudha == 2)
            {
                for (int i = 0; i < periudhavjetore / 2; i++)
                {
                    int month = dteFillimiViti.Date.Month + 2 * i;
                    DbCore.DbAdmin.clsPeriudhaKontabel per = new DbCore.DbAdmin.clsPeriudhaKontabel();
                    per.NrPeriudha = i + 1;
                    per.EmerPeriudha = muajt[(month - 1) % 12] + "-" + muajt[month % 12];
                    per.Ekycur = false;
                    per.FillimiPeriudha = dteFillimiViti.Date.AddMonths(2 * i);
                    per.MbarimiPeriudha = dteFillimiViti.Date.AddMonths(2 * i + 2).AddDays(-1);
                    if (month == dteMbarimiViti.Date.Month + 12 * (dteMbarimiViti.Date.Year - dteFillimiViti.Date.Year))
                        per.MbarimiPeriudha = dteMbarimiViti.Date;
                    periudhat.Add(per);
                }
                if (periudhavjetore % 2 == 1)
                {
                    int month = dteFillimiViti.Date.Month + periudhavjetore - 1;
                    DbCore.DbAdmin.clsPeriudhaKontabel per = new DbCore.DbAdmin.clsPeriudhaKontabel();
                    per.NrPeriudha = periudhavjetore / 2 + 1;
                    per.EmerPeriudha = muajt[(month - 1) % 12];
                    per.Ekycur = false;
                    per.FillimiPeriudha = dteFillimiViti.Date.AddMonths(periudhavjetore - 1);
                    per.MbarimiPeriudha = dteMbarimiViti.Date;

                    periudhat.Add(per);
                }
            }
            else if (llojperiudha == 3)
            {
                for (int i = 0; i < periudhavjetore / 3; i++)
                {
                    DbCore.DbAdmin.clsPeriudhaKontabel per = new DbCore.DbAdmin.clsPeriudhaKontabel();
                    int month = dteFillimiViti.Date.Month + 3 * i;
                    per.NrPeriudha = i + 1;
                    per.EmerPeriudha = muajt[(month - 1) % 12] + "-" + muajt[month % 12] + "-" + muajt[(month + 1) % 12];
                    per.Ekycur = false;
                    per.FillimiPeriudha = dteFillimiViti.Date.AddMonths(3 * i);
                    per.MbarimiPeriudha = dteFillimiViti.Date.AddMonths(3 * i + 3).AddDays(-1);
                    if (month == dteMbarimiViti.Date.Month + 12 * (dteMbarimiViti.Date.Year - dteFillimiViti.Date.Year))
                        per.MbarimiPeriudha = dteMbarimiViti.Date;
                    periudhat.Add(per);
                }
                if (periudhavjetore % 3 == 1)
                {
                    int month = dteFillimiViti.Date.Month + periudhavjetore - 1;
                    DbCore.DbAdmin.clsPeriudhaKontabel per = new DbCore.DbAdmin.clsPeriudhaKontabel();
                    per.NrPeriudha = periudhavjetore / 3 + 1;
                    per.EmerPeriudha = muajt[(month - 1) % 12];
                    per.Ekycur = false;
                    per.FillimiPeriudha = dteFillimiViti.Date.AddMonths(periudhavjetore - 1);
                    per.MbarimiPeriudha = dteMbarimiViti.Date;

                    periudhat.Add(per);
                }
                else if (periudhavjetore % 3 == 2)
                {
                    int month = dteFillimiViti.Date.Month + periudhavjetore - 2;
                    DbCore.DbAdmin.clsPeriudhaKontabel per = new DbCore.DbAdmin.clsPeriudhaKontabel();
                    per.NrPeriudha = periudhavjetore / 3 + 1;
                    per.EmerPeriudha = muajt[(month - 1) % 12] + "-" + muajt[month % 12];
                    per.Ekycur = false;
                    per.FillimiPeriudha = dteFillimiViti.Date.AddMonths(periudhavjetore - 2);
                    per.MbarimiPeriudha = dteMbarimiViti.Date;
                    periudhat.Add(per);
                }
            }
            else if (llojperiudha == 4)
            {
                for (int i = 0; i < periudhavjetore / 4; i++)
                {
                    DbCore.DbAdmin.clsPeriudhaKontabel per = new DbCore.DbAdmin.clsPeriudhaKontabel();
                    int month = dteFillimiViti.Date.Month + 4 * i;
                    per.NrPeriudha = i + 1;
                    per.EmerPeriudha = muajt[(month - 1) % 12] + "-" + muajt[month % 12] + "-" + muajt[(month + 1) % 12] + "-" + muajt[(month + 2) % 12];
                    per.Ekycur = false;
                    per.FillimiPeriudha = dteFillimiViti.Date.AddMonths(4 * i);
                    per.MbarimiPeriudha = dteFillimiViti.Date.AddMonths(4 * i + 4).AddDays(-1);
                    if (month == dteMbarimiViti.Date.Month + 12 * (dteMbarimiViti.Date.Year - dteFillimiViti.Date.Year))
                        per.MbarimiPeriudha = dteMbarimiViti.Date;
                    periudhat.Add(per);
                }
                if (periudhavjetore % 4 == 1)
                {
                    int month = dteFillimiViti.Date.Month + periudhavjetore - 1;
                    DbCore.DbAdmin.clsPeriudhaKontabel per = new DbCore.DbAdmin.clsPeriudhaKontabel();
                    per.NrPeriudha = periudhavjetore / 4 + 1;
                    per.EmerPeriudha = muajt[(month - 1) % 12];
                    per.Ekycur = false;
                    per.FillimiPeriudha = dteFillimiViti.Date.AddMonths(periudhavjetore - 1);
                    per.MbarimiPeriudha = dteMbarimiViti.Date;
                    periudhat.Add(per);
                }
                else if (periudhavjetore % 4 == 2)
                {
                    int month = dteFillimiViti.Date.Month + periudhavjetore - 2;
                    DbCore.DbAdmin.clsPeriudhaKontabel per = new DbCore.DbAdmin.clsPeriudhaKontabel();
                    per.NrPeriudha = periudhavjetore / 4 + 1;
                    per.EmerPeriudha = muajt[(month - 1) % 12] + "-" + muajt[month % 12];
                    per.Ekycur = false;
                    per.FillimiPeriudha = dteFillimiViti.Date.AddMonths(periudhavjetore - 2);
                    per.MbarimiPeriudha = dteMbarimiViti.Date;
                    periudhat.Add(per);
                }
                else if (periudhavjetore % 4 == 3)
                {
                    int month = dteFillimiViti.Date.Month + periudhavjetore - 3;
                    DbCore.DbAdmin.clsPeriudhaKontabel per = new DbCore.DbAdmin.clsPeriudhaKontabel();
                    per.NrPeriudha = periudhavjetore / 4 + 1;
                    per.EmerPeriudha = muajt[(month - 1) % 12] + "-" + muajt[month % 12] + "-" + muajt[(month + 1) % 12];
                    per.Ekycur = false;
                    per.FillimiPeriudha = dteFillimiViti.Date.AddMonths(periudhavjetore - 3);
                    per.MbarimiPeriudha = dteMbarimiViti.Date;
                    periudhat.Add(per);
                }

            }
            else if (llojperiudha == 5)
            {
                for (int i = 0; i < periudhavjetore / 6; i++)
                {
                    DbCore.DbAdmin.clsPeriudhaKontabel per = new DbCore.DbAdmin.clsPeriudhaKontabel();
                    int month = dteFillimiViti.Date.Month + 6 * i;
                    per.NrPeriudha = i + 1;
                    per.EmerPeriudha = muajt[(month - 1) % 12] + "-" + muajt[month % 12] + "-" + muajt[(month + 1) % 12] + "-" + muajt[(month + 2) % 12] + "-" + muajt[(month + 3) % 12] + "-" + muajt[(month + 4) % 12];
                    per.Ekycur = false;
                    per.FillimiPeriudha = dteFillimiViti.Date.AddMonths(6 * i);
                    per.MbarimiPeriudha = dteFillimiViti.Date.AddMonths(6 * i + 6).AddDays(-1);
                    if (month == dteMbarimiViti.Date.Month + 12 * (dteMbarimiViti.Date.Year - dteFillimiViti.Date.Year))
                        per.MbarimiPeriudha = dteMbarimiViti.Date;
                    periudhat.Add(per);
                }
                if (periudhavjetore % 6 == 1)
                {
                    int month = dteFillimiViti.Date.Month + periudhavjetore - 1;
                    DbCore.DbAdmin.clsPeriudhaKontabel per = new DbCore.DbAdmin.clsPeriudhaKontabel();
                    per.NrPeriudha = periudhavjetore / 6 + 1;
                    per.EmerPeriudha = muajt[(month - 1) % 12];
                    per.Ekycur = false;
                    per.FillimiPeriudha = dteFillimiViti.Date.AddMonths(periudhavjetore - 1);
                    per.MbarimiPeriudha = dteMbarimiViti.Date;
                    periudhat.Add(per);
                }
                else if (periudhavjetore % 6 == 2)
                {
                    int month = dteFillimiViti.Date.Month + periudhavjetore - 2;
                    DbCore.DbAdmin.clsPeriudhaKontabel per = new DbCore.DbAdmin.clsPeriudhaKontabel();
                    per.NrPeriudha = periudhavjetore / 6 + 1;
                    per.EmerPeriudha = muajt[(month - 1) % 12] + "-" + muajt[month % 12];
                    per.Ekycur = false;
                    per.FillimiPeriudha = dteFillimiViti.Date.AddMonths(periudhavjetore - 2);
                    per.MbarimiPeriudha = dteMbarimiViti.Date;
                    periudhat.Add(per);
                }
                else if (periudhavjetore % 6 == 3)
                {
                    int month = dteFillimiViti.Date.Month + periudhavjetore - 3;
                    DbCore.DbAdmin.clsPeriudhaKontabel per = new DbCore.DbAdmin.clsPeriudhaKontabel();
                    per.NrPeriudha = periudhavjetore / 6 + 1;
                    per.EmerPeriudha = muajt[(month - 1) % 12] + "-" + muajt[month % 12] + "-" + muajt[(month + 1) % 12];
                    per.Ekycur = false;
                    per.FillimiPeriudha = dteFillimiViti.Date.AddMonths(periudhavjetore - 3);
                    per.MbarimiPeriudha = dteMbarimiViti.Date;
                    periudhat.Add(per);
                }
                else if (periudhavjetore % 6 == 4)
                {
                    int month = dteFillimiViti.Date.Month + periudhavjetore - 4;
                    DbCore.DbAdmin.clsPeriudhaKontabel per = new DbCore.DbAdmin.clsPeriudhaKontabel();
                    per.NrPeriudha = periudhavjetore / 6 + 1;
                    per.EmerPeriudha = muajt[(month - 1) % 12] + "-" + muajt[month % 12] + "-" + muajt[(month + 1) % 12] + "-" + muajt[(month + 2) % 12];
                    per.Ekycur = false;
                    per.FillimiPeriudha = dteFillimiViti.Date.AddMonths(periudhavjetore - 4);
                    per.MbarimiPeriudha = dteMbarimiViti.Date;
                    periudhat.Add(per);
                }
                else if (periudhavjetore % 6 == 5)
                {
                    int month = dteFillimiViti.Date.Month + periudhavjetore - 5;
                    DbCore.DbAdmin.clsPeriudhaKontabel per = new DbCore.DbAdmin.clsPeriudhaKontabel();
                    per.NrPeriudha = periudhavjetore / 6 + 1;
                    per.EmerPeriudha = muajt[(month - 1) % 12] + "-" + muajt[month % 12] + "-" + muajt[(month + 1) % 12] + "-" + muajt[(month + 2) % 12] + "-" + muajt[(month + 3) % 12];
                    per.Ekycur = false;
                    per.FillimiPeriudha = dteFillimiViti.Date.AddMonths(periudhavjetore - 5);
                    per.MbarimiPeriudha = dteMbarimiViti.Date;
                    periudhat.Add(per);
                }
            }
            if (this.cbPeriudhaMbylljes.Checked == true)
            {
                DbCore.DbAdmin.clsPeriudhaKontabel per = new DbCore.DbAdmin.clsPeriudhaKontabel();

                per.NrPeriudha = periudhavjetore;
                per.EmerPeriudha = DbCore.DbAdmin.clsPeriudhaKontabel.PeriudheMbyllje;
                per.Ekycur = false;
                per.FillimiPeriudha = dteMbarimiViti.Date;
                per.MbarimiPeriudha = dteMbarimiViti.Date;
                periudhat.Add(per);
            }
            return periudhat;
        }

        private bool isValidVit(int idNdermarrje, ResourceManager rm, CultureInfo ci)
        {
            bool isValid;
            isValid = true;
            DbCore.DbAdmin.clsViti viti = new DbCore.DbAdmin.clsViti();
            viti.KodiViti = txtKodi.Text;
            viti.IdNdermarje = idNdermarrje;
            DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            if (this.txtLlogMbylljeViti.Text != "")
            {
                if (!DbCore.DbKontabiliteti.clsLlogari.ekzistonLlogari(this.txtLlogMbylljeViti.Text.Split(';')[0], idNdermarrje))
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgCeljeArkaBankaLlogariaNukEkziston", ci), pnlMesazhi);
                    hfStatusi.Value = "false";
                    return false;
                }
                else if (!DbCore.DbKontabiliteti.clsLlogari.eshteLlogariAktive(this.txtLlogMbylljeViti.Text.Split(';')[0], idNdermarrje))
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgCeljeArkaBankaLlogariaNukEshteAktive", ci), pnlMesazhi);
                    hfStatusi.Value = "false";
                    return false;
                }
                else
                {
                    DbCore.DbKontabiliteti.clsLlogari llog = new DbCore.DbKontabiliteti.clsLlogari(this.txtLlogMbylljeViti.Text.Split(';')[0], idNdermarrje);
                    if (llog.IdMonedha != DbCore.DbAdmin.clsNdermarrje.ktheIdMonedheNdermSipasID(idNdermarrje))
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgLlogariaDuhetTeJetENeMonedhenBaze", ci), pnlMesazhi);
                        hfStatusi.Value = "false";
                        return false;
                    }
                }
            }
            if (!dbAdmin.ekzistonVit(viti.KodiViti, viti.IdNdermarje).Status && (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim"))
            {
                isValid = false;
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgVitetEkziston1VitMeKeteKod", ci), pnlMesazhi);

                hfStatusi.Value = "false";
                return isValid;
            }
            dbAdmin.Dispose();
            return isValid;
        }

        /// <summary>
        /// perdoret per te rikonfiguruar griden kur ndryshohet konfigurimi
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxGridView_Vitet_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string idkomponente = "";
            string kodkonfigurimi = "";

            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                kodkonfigurimi = arr[1];
                idkomponente = arr[0];
                if (arr[2] == "")
                    ASPxGridView_Vitet.FilterExpression = "";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "ASPxGridView_Vitet", komponente, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), int.Parse(cmbKonfigurimi.Value.ToString()));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        ASPxGridView_Vitet.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, ASPxGridView_Vitet);
                        hfStatusi.Value = "true";
                    }
                    else hfStatusi.Value = "false";
                }
            }
            else
                if (arr.Length == 2)//nese eshte zgjedhur nje konfigurim tek combo e konfigurimeve
                {
                    kodkonfigurimi = arr[1];
                    idkomponente = arr[0];
                }
                else
                {
                    idkomponente = e.Parameters;
                }
            this.ASPxGridView_Vitet.Selection.UnselectAll();
        }

        protected void gvPeriudha_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gvPeriudha.PageIndex;
            e.Properties["cpPageRow"] = gvPeriudha.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gvPeriudha.VisibleRowCount;
        }

        protected void gvPeriudha_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {//thirret kur grida ben callback

        }

        protected void txtLlogMbylljeViti_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].Contains("txtLlogMbylljeViti"))
                {
                    ConfigureAspxComboBox.mbushComboLlogariaPaKolona(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), txtLlogMbylljeViti, e);
                }
            }
        }

        protected void gvPeriudha_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        { //sherben per te marre me vone vlerat e futura nga perdoruesi me ane te javascriptit

            if (e.RowType == GridViewRowType.Data)
            {
                GridViewDataColumn colkycur = ((ASPxGridView)sender).Columns["Ekycur"] as GridViewDataColumn;
                ASPxCheckBox kycur = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, colkycur, "cb") as ASPxCheckBox;

                if (kycur != null)
                {
                    kycur.ClientInstanceName = "kycur" + e.VisibleIndex.ToString();
                    kycur.ClientSideEvents.Init = "function (s,e){ShtoKycje(kycur" + e.VisibleIndex.ToString() + "," + e.VisibleIndex + "," + gvPeriudha.VisibleRowCount + ");}";
                }
            }
        }

        /// <summary>
        /// kur gvBuxheti ben callback per tu mbushur me vlerat per llogarine e zgjedhur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvPeriudha_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            int llojperiudha = 1;
            if (cmbPeriudhaLloji.Value != null)

                int.TryParse(cmbPeriudhaLloji.Value.ToString(), out llojperiudha);
            string[] argumenta = e.Parameters.Split(',');
            if (argumenta[0] == "-1")
            {
                DbCore.DbAdmin.colPeriudhaKontabel periudhat = new DbCore.DbAdmin.colPeriudhaKontabel();
                periudhat = llogaritPeriudhat(llojperiudha);
                hfKycje.Value = "";
                for (int i = 0; i < periudhat.Count; i++)
                {
                    hfKycje.Value += i + ":" + periudhat[i].Ekycur + ";";
                }
                gvPeriudha.DataSource = periudhat;
                gvPeriudha.DataBind();
                konfiguroGridePeriudhash();
            }
            else
            {
                int id = 0;
                CultureInfo ci = DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo(idgjuha);
                DbCore.DbAdmin.colPeriudhaKontabel periudhat = new DbCore.DbAdmin.colPeriudhaKontabel();
                int.TryParse(this.ASPxGridView_Vitet.GetRowValues(int.Parse(argumenta[0]), "IdViti").ToString(), out id);
                periudhat.merrSipasVitiDheGjuhes(id, idgjuha);
                    //periudhat = new DbCore.DbAdmin.clsDatabaseAdmin().merrPeriudhaSipasViti(id);
                if (cbPeriudhaHapjes.Checked)
                {
                    if (periudhat[0].NrPeriudha != 0)
                    {
                        DbCore.DbAdmin.clsPeriudhaKontabel per = new DbCore.DbAdmin.clsPeriudhaKontabel();

                        per.NrPeriudha = 0;
                        per.EmerPeriudha = DbCore.DbAdmin.clsPeriudhaKontabel.PeriudheFillestare;
                        per.Ekycur = false;
                        periudhat.Insert(0, per);
                    }
                }
                else if (periudhat[0].NrPeriudha == 0)
                {
                    periudhat.RemoveAt(0);
                }
                if (cbPeriudhaMbylljes.Checked)
                {
                    if (periudhat[periudhat.Count - 1].EmerPeriudha != periudhat[13].EmerPeriudha) 

                    {
                        DbCore.DbAdmin.clsPeriudhaKontabel per = new DbCore.DbAdmin.clsPeriudhaKontabel();

                        per.NrPeriudha = periudhat.Count + 1;
                        per.EmerPeriudha = periudhat[13].EmerPeriudha;                                  
                        per.Ekycur = false;
                        periudhat.Add(per);
                    }
                }
                else if (periudhat[periudhat.Count - 1].EmerPeriudha == DbCore.DbAdmin.clsPeriudhaKontabel.PeriudheMbyllje)
                {
                    periudhat.RemoveAt(periudhat.Count - 1);
                }
                hfKycje.Value = "";
                for (int i = 0; i < periudhat.Count; i++)
                {
                    hfKycje.Value += i + ":" + periudhat[i].Ekycur + ";";
                }
                gvPeriudha.DataSource = periudhat;
                gvPeriudha.DataBind();
                konfiguroGridePeriudhash();
                
            }

        }
        protected void txtLlogMbylljeViti_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].Contains("txtLlogMbylljeViti"))
                {
                    ConfigureAspxComboBox.mbushComboLlogariaPaKolona(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), txtLlogMbylljeViti,e);
                }
            }

        }
    }
}