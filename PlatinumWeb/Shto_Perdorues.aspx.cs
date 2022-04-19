using DbCore;
using DbCore.DbAdmin;
using DbCore.DbShare;
using DevExpress.Web;
using PlatinumWeb.Templates;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Resources;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using DbCore.IMBUtils.Types;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.Security;

namespace PlatinumWeb
{
    public partial class Shto_Perdorues : MyPageBase
    {
        private int idNdermarrje, idviti, idNdermarjeVit, idPerdoruesi;
        private const string komponente = "Shto_Perdorues.aspx";
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
               
                if (Request.QueryString["arsyeja"] == null || Request.QueryString["arsyeja"] != "ngaEmailKerkeseAprovim")
                    DbCore.clsFunksione.logout(Session,true,"FaqePaautorizuar");
                else Response.Redirect("login.aspx?arsyeja=ngaEmailKerkeseAprovim&perdorues=" + Request.QueryString["perdorues"]);
            }
            idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerdoruesi);
            }
            idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            idviti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            idNdermarjeVit = mySessionObjects.ktheNdermarrjeVit(Session);
            clsPerdorues perdoruesi = mySessionObjects.kthePerdorues(Session);
            hfState.Set("PerdoruesUsername", perdoruesi.PerdoruesUsername);
            hfState.Set("MMRT", clsAlternativaKushti.getAlternativa(clsKonfigurimAmbjenti.ktheIdSipasKategoriDheNderm(49, idNdermarrje), "MMRT"));
            int idgjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            if (!Page.IsPostBack)
            {


                if (Request.QueryString["arsyeja"] != null && Request.QueryString["arsyeja"] == "ngaEmailKerkeseAprovim")
                {
                    hfShtimModifikim.Value = "modifikim";
                    hfNgaEmailKerkeseAprovimi.Value = "true";
                    int id = (Request.QueryString["perdorues"] != null) ? clsPerdorues.ktheIdPerdoruesSipasUsernamePateDrejta(Request.QueryString["perdorues"].ToString(), idNdermarrje) : -1;
                    if (id == -1) Response.Redirect("login.aspx?arsye=PerdoruesiNukEkziston");
                    hfId.Value = id.ToString();
                }
                guidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                hfState.Set("guidString", guidString);
                EmrateTabeve(rm, ci);
                EmratELabelave(rm, ci);
                mbushHiddenFieldMePerkthime(rm, ci);
                hfState.Set("idGjuha", idgjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                if (hfId.Value == "")
                    hfId.Value = "0";
                ASPxPageControl1.ActiveTabIndex = 0;
                konfiguroVleraFillestare(idPerdoruesi, idNdermarrje, rm, ci, idgjuha);
                bool kushtiSHTK = (clsAlternativaKushti.getAlternativa(Convert.ToInt32(cmbKonfigurimi.Value), "SHTK") == "Po");
                if (!kushtiSHTK)
                    ASPxPageControl1.TabPages[2].ClientVisible = false;
                bool kushtiSHTR = (clsAlternativaKushti.getAlternativa(Convert.ToInt32(cmbKonfigurimi.Value), "SHTR") == "Po");
                if (!kushtiSHTR)
                    ASPxPageControl1.TabPages[3].ClientVisible = false;
                mbushGridPerdoruesishNgaDB();
                konfiguroGride(cmbKonfigurimi.Text.Split(';')[0], 100);
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), "grid_ListPerdoruesit", grid_ListPerdoruesit, cmbKonfigurimi.Text.Split(';')[0], 100.ToString(), idgjuha);
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idviti, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idviti, komponente);
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                clsKonfigurimeFjalekalimi konfigPass = new clsKonfigurimeFjalekalimi(idPerdoruesi);
                hfMinGjatesiPassword.Set("GjatesiMinPass", konfigPass.GjatesiaMinPassword);
                if (hfId.Value != "0")
                    hfIndexId.Value = grid_ListPerdoruesit.FindVisibleIndexByKeyValue(hfId.Value).ToString();
            }
            else
            {
                ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(IdPerdoruesi, IdNdermarrja, cmbKonfigurimi, 21, rm, ci, idgjuha);

                guidString = (string)hfState["guidString"];
                mbushGridPerdoruesishNgaSession();
                konfiguroGride(cmbKonfigurimi.Text.Split(';')[0], 100);

            }
            GridUtil.konfigGrideListeEMadhePaTheme(grid_ListPerdoruesit, "IdPerdorues");
            mbushlisteRolesh();
            konfiguroGrideRoli();

             percaktoTemplateMenu(idgjuha, idviti, idPerdoruesi, idNdermarrje, ASPxMenu1);
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "grid_ListPerdoruesit", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
            percaktoTamplate();
            grid_ListPerdoruesit.PercaktoTitlePanel(this, MenuInfo, pnlMesazhi, hfState, idPerdoruesi, idNdermarrje, idviti, idgjuha, int.Parse(cmbKonfigurimi.Value.ToString()), komponente, rm, ci);
            gvRolet.PercaktoTitlePanel(this, MenuInfo, pnlMesazhi, hfState, idPerdoruesi, idNdermarrje, idviti, idgjuha, int.Parse(cmbKonfigurimi.Value.ToString()), komponente, rm, ci);

            AspxWebControlUtils.perkthePopUp(popUpConfirm, rm.GetString("lblKonfirmo", ci), lblMsgbox, rm.GetString("lblJeniSigurtPerNdryshiminERolit", ci), btnCancel, rm.GetString("labelAnullo", ci));
            AspxWebControlUtils.perkthePopUp(popFshi, rm.GetString("labelKujdes", ci), lblMsgbox, rm.GetString("labelAdministrimiMsgJeniSigurt", ci), ButtonCancel, rm.GetString("labelAnullo", ci));
        }

        private void EmratELabelave(ResourceManager rm, CultureInfo ci)
        {
            konfigurimi_Label.Text = rm.GetString("lblModeli", ci);
            //((ASPxButton)ASPxPageControl1.TabPages[3].FindControl("ASPxButton1")).Text = rm.GetString("btnSelektoTeGjithaNeKeteFaqe", ci);
            //((ASPxButton)ASPxPageControl1.TabPages[3].FindControl("ASPxButton2")).Text = rm.GetString("btnHiqGjitheSelektimetNeKeteFaqe", ci);
            popupUniversal.HeaderText = rm.GetString("headerPopUpText", ci);
            popUpConfirm.HeaderText = rm.GetString("lblKonfirmo", ci);
            lblMsgConfirm.Text = rm.GetString("lblJeniSigurtPerNdryshiminERolit", ci);
            btnCancel.Text = rm.GetString("btnCancel", ci);   
        }

        private void mbushHiddenFieldMePerkthime(ResourceManager rm, CultureInfo ci)
        {
            hfState.Set("msgPerdoruesitRolJoAktiv", rm.GetString("msgPerdoruesitRolJoAktiv", ci));
            hfState.Set("msgPerdoruesitMinGjatesiPassword", rm.GetString("msgPerdoruesitMinGjatesiPassword", ci));
            hfState.Set("msgPerdoruesitMinKarakterePass", rm.GetString("msgPerdoruesitMinKarakterePass", ci));
            hfState.Set("msgPerdoruesitZgjidhniNjePerdorues", rm.GetString("msgPerdoruesitZgjidhniNjePerdorues", ci));
            hfState.Set("msgambjentindefaultmodulimobile", rm.GetString("msgambjentindefaultmodulimobile", ci));
            hfState.Set("msgZgjidhArsyeLargimi", rm.GetString("msgZgjidhArsyeLargimi", ci));
            hfState.Set("msgStatus", rm.GetString("msgStatus", ci));
            hfState.Set("msgUniform", rm.GetString("msgUniform", ci));
            lblAmbjenti.Text = rm.GetString("lblPerdoruesiAmbjentiDefault", ci);
        }

        /// <summary>
        /// Vendos emrat e tabeve ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateTabeve(ResourceManager rm, CultureInfo ci)
        {
            ASPxPageControl1.TabPages[0].Text = rm.GetString("TePergjithshmeTab", ci);
            ASPxPageControl1.TabPages[1].Text = rm.GetString("perdoruesTab", ci);
            ASPxPageControl1.TabPages[2].Text = rm.GetString("labelAdministrimiKontakt", ci);
            ASPxPageControl1.TabPages[3].Text = rm.GetString("MenuItemRolet", ci);
            ASPxPageControl1.TabPages[4].Text = rm.GetString("MenuItemAutorizimet", ci);      
        }

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="idNderVit"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        private void percaktoTemplateMenu(int idgjuha, int idViti, int idPerdorues, int idNdermarrje, ASPxMenu aSPxMenu1)
        {
            clsToolbarConfig.percaktoTemplateMenu(idgjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1, komponente, this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, hfShtimModifikim.Value == "modifikim" ? false : true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
        }

        protected void btnKonfirmo_Click(object sender, EventArgs e)
        {
            Page.Validate("entries");
            ruajPerdorues();
        }

        protected void RuajKolona_Click(object sender, EventArgs e)
        {
            int idfiltri = 0;
            int idkonf = DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(cmbKonfigurimi.Text.Split(';')[0], idNdermarrje);
            DbCore.clsMesazh mesazh = GridUtil.ruajFiltra(idNdermarrje, idPerdoruesi, (int)hfState.Get("idgjuha"), "grid_ListPerdoruesit", komponente, "FilterDefault", grid_ListPerdoruesit.FilterExpression, grid_ListPerdoruesit, "PerdoruesUsername", idkonf, out idfiltri);//ruan filtrin e zgjedhur tek filtrat
            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                return;

            }
            mesazh = GridUtil.ruajkonfigurimgride(grid_ListPerdoruesit, cmbKonfigurimi.Text, idNdermarrje, idPerdoruesi, 100, idfiltri, idviti, DbCore.mySessionObjects.ktheCultureInfo(Session), DbCore.mySessionObjects.ktheGjuhe(Session));/// ruan konfigurimin e grides dhe filtrin
            int idgjuha = (int)hfState.Get("idgjuha");
            clsToolbarConfig.mbushComboBoxFiltra(idgjuha, idNdermarrje, "grid_ListPerdoruesit", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);

            percaktoTemplateMenu(idgjuha, idviti, idPerdoruesi, idNdermarrje, ASPxMenu1);

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
            percaktoTemplateMenu((int)hfState.Get("idgjuha"), idviti, idPerdoruesi, idNdermarrje, ASPxMenu1);

        }

        /// <summary>
        /// perdoret per te shtuar kolonen e selektimit tek grida dhe per te vendosur disa karakteristika te grides
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void grid_ListPerdoruesit_DataBound(object sender, EventArgs e)
        {// shton colonen # per selektim dhe disa karakteristika te grides
            if (this.grid_ListPerdoruesit.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per 
                //perzgjidh
                GridViewCommandColumn check = new GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true;
                check.Width = Unit.Percentage(2);
                //  check.SetColVisibleIndex(0);
                //behet per te afishuar rreshtin qe do sherbej per filtrim
                grid_ListPerdoruesit.Settings.ShowFilterRow = true;
                grid_ListPerdoruesit.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                grid_ListPerdoruesit.Settings.ShowFilterRowMenu = true;
                grid_ListPerdoruesit.Columns.Add(check);
                grid_ListPerdoruesit.KeyFieldName = "IdPerdorues";
                grid_ListPerdoruesit.SettingsBehavior.AllowSelectByRowClick = true;
                grid_ListPerdoruesit.SettingsBehavior.AllowFocusedRow = true;
            }
        }

        /// <summary>
        /// ben konfigurimin e grides sipas konfigurimit te zgjedhur
        /// </summary>
        private void konfiguroGride(string kodKonfigurimi, int idKomponente)
        {

            percaktoTamplate();
            KonfigurimComboGride.ShtoQytetet(grid_ListPerdoruesit, -1, Session, komponente, guidString, "IdQyteti");
            KonfigurimComboGride.ShtoStatusAprovimi_Perd(grid_ListPerdoruesit, rm, ci);
            KonfigurimComboGride.shto_Gjini(grid_ListPerdoruesit, rm, ci);
            KonfigurimComboGride.shtoISigururar(grid_ListPerdoruesit, rm, ci);
            KonfigurimComboGride.Shto_Gjuhe(grid_ListPerdoruesit, rm, ci);
            grid_ListPerdoruesit.Columns["#"].VisibleIndex = 0;
           
        }

        /// <summary>
        ///  thirret sa here qe behet Callback, dhe ben edhe njeher lidhjen me datasource-in
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void grid_ListPerdoruesit_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            if (e.CallbackName == "COLUMNMOVE" && grid_ListPerdoruesit.AllColumns[int.Parse(e.Args[0])].Width.Value == 0)
                grid_ListPerdoruesit.AllColumns[int.Parse(e.Args[0])].Width = Unit.Percentage(3);
            if (e.CallbackName == "APPLYFILTER" && e.Args[0] == "")
            {
                DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
                ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
                cmbFiltra.Text = "";
            }
            percaktoTamplate();
            
        }

        /// <summary>
        ///   sherben per te vendosur filtra tek header-i i grides (mund te ndryshohet me vone per tu
        ///bere  me e konfigurueshme
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grid_ListPerdoruesit_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            string TeGjithe = rm.GetString("GridHeaderFilterFillItemTeGjithe", ci);
            string nga = rm.GetString("GridHeaderFilterFillItemNga", ci);
            if (e.Column.FieldName != "IdQyteti")
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
        /// perdoret per te shfaqur Aktiv jo aktiv tek filtrimi
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void grid_ListPerdoruesit_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName == "PerdoruesAktiv")
            {
                System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
                System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

                (e.Editor as ASPxComboBox).Items.Clear();
                (e.Editor as ASPxComboBox).Items.Add("");
                (e.Editor as ASPxComboBox).Items.Add(rm.GetString("cmbPerdoruesitAktiv", ci), true);
                (e.Editor as ASPxComboBox).Items.Add(rm.GetString("cmbPerdoruesitJoAktiv", ci), false);
            }
        }

        /// <summary>
        /// fshin filtrin e zgjedhur dhe aplikuar mbi gride, fshirje nga DB ku ai eshte ruajtur, dhe jo pastrimi i grides nga aplikimi i filtrit
        /// per kete do sherbeje Clear Filter ne fund te grides
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        public void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
            //filtra.mbushFiltraGridaSipasFiltraEmri(cmbFiltra.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "grid_ListPerdoruesit", komponente, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            //DbCore.DbAdmin.clsFiltraGrida filtra = dbAdmin.merrFiltraGridaSipasFiltraEmri(cmbFiltra.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();
                //mbushComboBoxFiltra();
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "grid_ListPerdoruesit", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
                percaktoTemplateMenu((int)hfState.Get("idgjuha"), idviti, idPerdoruesi, idNdermarrje, ASPxMenu1);

                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                cmbFiltra.Text = "";
                ASPxButton btnRuaj = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("Button1") as ASPxButton;
                //ASPxButton btnFshiFilter = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFshi") as ASPxButton;

                //btnRuaj.ClientEnabled = false;
                //btnFshiFilter.ClientEnabled = false;
                // konfiguroVleraFillestare();
                hfStatusi.Value = "true";
                this.grid_ListPerdoruesit.FilterExpression = String.Empty;
            }
        }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        protected void PastroOTP_Button_Click(object sender, EventArgs e)
        {
            var userid = hfId.Value;
            if(clsPerdorues.modifikoOtp(Convert.ToInt32(userid), string.Empty));
           
            {
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "Tokeni u risetua me sukses", pnlMesazhi);
                return;

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
            filtri.FiltraUniversal = false;
            DbCore.DbAdmin.clsGridaKoka koka = new clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "grid_ListPerdoruesit", komponente, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = grid_ListPerdoruesit.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("PerdoruesUsername", grid_ListPerdoruesit);
            
            filtri.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            filtri.IdNdermarje = idNdermarrje;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            filtri.IdStatusDok = 1;

            mesazh = filtri.ruaj();
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "grid_ListPerdoruesit", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
            percaktoTemplateMenu((int)hfState.Get("idGjuha"), idviti, idPerdoruesi, idNdermarrje, ASPxMenu1);

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
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            if (ASPxPageControl1.ActiveTabIndex == 0)//nqs kemi zgjedhur vetem nje kartele per te pare fshijme vetem ate
                rreshtat = grid_ListPerdoruesit.GetSelectedFieldValues("IdPerdorues");
            else
            {
                rreshtat = new List<object>();
                rreshtat.Add(hfId.Value);
            }
            // List<object> rreshtat = this.grid_ListPerdoruesit.GetSelectedFieldValues("IdPerdorues");
            if (rreshtat.Count == 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgPerdoruesitZgjidhniPerd", ci), pnlMesazhi);
                return;
            }
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            //clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "", pnlMesazhi);
            List<string> TeFshire = new List<string>(), TePaFshire = new List<string>();
            foreach (object id in rreshtat)
            {
                DbCore.DbAdmin.clsPerdorues clsPerd = new clsPerdorues(Convert.ToInt32(id));
                clsPerd.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                if (Convert.ToInt32(id) != DbCore.mySessionObjects.ktheIdPerdoruesi(Session))
                {
                    bool vazhdo = true;
                    //DbCore.DbAdmin.colRolPerdorues rolper = new colRolPerdorues();
                    DbCore.DbAdmin.colRolPerdorues rolper1 = new colRolPerdorues();
                    rolper1.mbushRolePerdoruesSipasPerdoruesi(clsPerd.IdPerdorues);
                    int idroli = DbCore.DbAdmin.colRolPerdorues.merrRoleSipasPerdoruesiDheKodRoli(clsPerd.IdPerdorues, "RA");
                    colRolPerdorues nrperrol = new colRolPerdorues();
                    nrperrol.mbushRolePerdoruesSipasRoli(idroli);
                    if (nrperrol.Count == 1)
                    {
                        vazhdo = false;

                    }

                   
                    if (vazhdo)
                    {

                        DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                        //konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()));
                        konf.mbushKonfigAmbjSipasKod(cmbKonfigurimi.Text, idNdermarrje);
                        DbCore.DbRegjistrim.clsDatabaseRegjistrim dbregjistrim = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
                        bool lidhur = dbregjistrim.eshteDokumentiILidhurCelje(clsPerd.IdPerdorues.ToString(), konf.IdNivel.ToString());
                        if (lidhur) clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgPerdoruesitLidhur", ci), pnlMesazhi);
                        else
                        {

                            if (!clsPerd.kaVeprime(clsPerd.IdPerdorues))
                            {
                                mesazh = clsPerd.fshi();
                                if (clsPerd.IdPerdorues == 0)
                                    continue;
                                if (mesazh.Status)
                                {
                                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("msgFshirjeMeSukses", ci), pnlMesazhi);
                                    hiqPerdoruesNgaGrida(clsPerd.IdPerdorues, ci, rm);
                                }
                                else
                                {
                                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                                }
                            }
                            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgPerdoruesitKaVeprime", ci), pnlMesazhi);
                        }
                    }
                    else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgPerdoruesitNukMundTeFshihet", ci), pnlMesazhi);

                }
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgPerdoruesitNukMundTeFshihetPerdLoguar", ci), pnlMesazhi);
            }
            ASPxPageControl1.ActiveTabIndex = 0;
            //  konfiguroGride();
            // mbushListePerdoruesish(); 
            hfStatusi.Value = "true";
            pnlMesazhi.Update();
        }

        private void hiqPerdoruesNgaGrida(int idperdorues, CultureInfo ci, ResourceManager rm)
        {
            if (this.grid_ListPerdoruesit.DataSource != null)
            {
                DataTable dt = (DataTable)grid_ListPerdoruesit.DataSource;
                DataRow[] drs = dt.Select("IdPerdorues = " + idperdorues);
                if (drs.Length > 1)
                    throw new DbCore.MyException(rm.GetString("msgPerdoruesitGabim2UserTeNjejteNeGrid", ci));
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                dt.Rows.Remove(dr);
                grid_ListPerdoruesit.DataBind();
            }
            else mbushGridPerdoruesishNgaDB();
        }

        private void shtoPerdoruesNeGrid(int idper, ResourceManager rm, CultureInfo ci)
        {
            if (grid_ListPerdoruesit.DataSource != null)
            {
                DataTable dt = (DataTable)grid_ListPerdoruesit.DataSource;
                DataRow[] drs = dt.Select("IdPerdorues = " + idper);
                if (drs.Length > 0)
                    throw new DbCore.MyException(rm.GetString("msgPerdoruesitGabimUserEkzistonNeGrid", ci));
                DataRow newArtDr = DbCore.DbAdmin.colPerdoruesit.merrPerdoruesDR(idper);
                dt.ImportRow(newArtDr);
            }
            else mbushGridPerdoruesishNgaDB();
        }

        private void modifikoPerdoruesNeGrid(int idper, ResourceManager rm, CultureInfo ci)
        {
            if (grid_ListPerdoruesit.DataSource != null)
            {
                DataTable dt = (DataTable)grid_ListPerdoruesit.DataSource;
                DataRow[] drs = dt.Select("IdPerdorues = " + idper);
                if (drs.Length > 1)
                    throw new DbCore.MyException(rm.GetString("msgPerdoruesitGabim2UserTeNjejteNeGrid", ci));
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                DataRow newArtDr = DbCore.DbAdmin.colPerdoruesit.merrPerdoruesDR(idper);
                object[] arr = newArtDr.ItemArray;
                dr.ItemArray = arr;
            }
            else mbushGridPerdoruesishNgaDB();
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
                ruajPerdorues();
            }
        }

        /// <summary>
        /// perdoret per te shfaqur te gjithe komponentet kur zgjidhet (...)
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumenti</param>
        protected void grid_ListPerdoruesit_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if (e.Column.FieldName == "IdQyteti")
            {
                if (Converter.ConvertToInt(e.Value) == -1)
                {
                    e.Criteria = null;
                }
            }
        }

        /// <summary>
        /// ruan disa karakteristika te grides
        /// </summary>
        /// <param name="sender">dergues</param>
        /// <param name="e">argumentat</param>
        protected void grid_ListPerdoruesit_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = grid_ListPerdoruesit.PageIndex;
            e.Properties["cpPageRow"] = grid_ListPerdoruesit.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = grid_ListPerdoruesit.VisibleRowCount;
        }

        /// <summary>
        /// perdoret per te percaktuar templatet e komboboxeve dhe te mbushe vlerat fillestare te gridave
        /// </summary>
        /// <param name="idPerdoruesi"></param>
        /// <param name="idNdermarrje"></param>
        private void konfiguroVleraFillestare(int idPerdoruesi, int idNdermarrje, ResourceManager rm, CultureInfo ci, int idgjuha)
        {
            ASPxPageControl1.ActiveTabIndex = 0;
            inicializoObjekte();
            ConfigureAspxComboBox.mbushComboQytetedef(qyteti_ASPxComboBox);
            int idLicenca = DbCore.DbAdmin.clsLicenca.merrIdLicencePerdoruesi(idPerdoruesi);
            clsLicenca licenca = new clsLicenca(idLicenca);

            ConfigureAspxComboBox.mbushComboAmbjente(cmbAmbjenti, licenca.IdLlojLicenca, idgjuha, false);
            ConfigureAspxComboBox.mbushComboAmbjente(cmbAmbjentiMobile, licenca.IdLlojLicenca, idgjuha, true);
            ConfigureAspxComboBox.mbushComboGjuha(cmbGjuha);
            ConfigureAspxComboBox.mbushShopsHierarkiStatus(cmbStatus);
            ConfigureAspxComboBox.mbushShopsHierarkiUniform(cmbUniform);
            ConfigureAspxComboBox.mbushShopsHierarkiLeaveReason(cmbLeaveReason);
            ConfigureAspxComboBox.mbushComboISiguruar(cmbIsInsured, ci, rm);
            ConfigureAspxComboBox.mbushComboStatusAprovimi(cmbStatusAprovimi, ci, rm);
            ConfigureAspxComboBox.mbushComboGjini(cmbGender, ci, rm);
            ConfigureAspxComboBox.mbushComboKonfigurimKase(cmbKonfigKase, idNdermarrje);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmbStatus, cmbLeaveReason, cmbUniform);
            ConfigureAspxComboBox.percaktoTemplateComboJoListePaLupe(cmbKonfigKase);
            //    mbushListePerdoruesish();
            ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 21, rm, ci, idgjuha);
            cmbKonfigurimi.SelectedIndex = (hfNgaEmailKerkeseAprovimi.Value == "true") ? 1 : 0;
            //cmbKonfigurimi.SelectedIndex = 0;
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()), idgjuha);
            //konf.IdKonfigAmbjente = int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString());
            //konf = new DbCore.DbShare.clsDatabaseShare().merrKonfigAmbjSipasId(konf)[0];
            hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;
            //cmbKonfigurimi.SelectedIndex = -1;
            DbCore.DbAdmin.colAutorizimetKoka autorizimet = new DbCore.DbAdmin.colAutorizimetKoka();
            ASPxGridView_Autorizimet.DataSource = autorizimet;
            ASPxGridView_Autorizimet.DataBind();
            konfiguroGrideAutorizimesh();
            DbCore.DbAdmin.clsKonfigurimeFjalekalimi konfig = new DbCore.DbAdmin.clsKonfigurimeFjalekalimi(idPerdoruesi);
            if (konfig.GjeneroPassword)
            {
                hfState.Set("passwordiGjeneruar", PasswordHelper.GjeneroPassword(konfig.GjatesiaMinPassword, konfig.SpecialChars, konfig.UppercaseChars, konfig.NumbersChars));
                hfState.Set("gjeneroPassword", konfig.GjeneroPassword);
                hfState.Set("gjatesiPassword", konfig.GjatesiaMinPassword);
            }
           
        }

        /// <summary>
        /// inicializon dbKontabilitetin
        /// </summary>
        private void inicializoObjekte()
        {
            //dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
        }

        private void mbushGridPerdoruesishNgaSession()
        {
            DataTable tmpObject;
            bool sukses = DbCore.mySessionObjects.merrGrideNgaSessioni(Session, out tmpObject);
            if (!sukses)
                mbushGridPerdoruesishNgaDB();
            else
            {
                grid_ListPerdoruesit.DataSource = tmpObject;
                grid_ListPerdoruesit.DataBind();
                tmpObject.Dispose();
            }
        }

        private void mbushGridPerdoruesishNgaDB()
        {
            //mbush griden e popupit me te dhena 
            colRolPerdorues rp = new colRolPerdorues();
            rp.mbushRolePerdoruesSipasPerdoruesi(DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            clsRoli ro = new clsRoli(rp[0].IdRoli);
            DataTable dt = DbCore.DbAdmin.colPerdoruesit.merrPerdoruesitSipasLicencesDT(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), ro.IdLicenca);
            DbCore.mySessionObjects.ruajGrideNeSession(Session, dt);
            grid_ListPerdoruesit.DataSource = dt;
            grid_ListPerdoruesit.DataBind();
            dt.Dispose();
        }

        public DbCore.DbAdmin.clsPerdorues krijoPerdorues(string emri, string mbiemri, string username, string tel, string fax, string email, string adresa, int idQyteti, bool infoHapur, bool perdoruesAktiv, string password, int idPerdoruesi, int idStatusDok, int idStilRaporti, bool passwordIPerkohshem, bool perdoruesIKycur, int idAmbjent, int idgjuha, bool kontrollPassword, bool shfaqDtPrintimi, bool kycurMobile, bool shfaqPerdoruesMenu, bool shfaqNjoftime, colRolPerdorues rolet, int idAmbjentMobile, String shopcode, String shopname, String dealername, String usericrm, String userEtopuP, String iDEtopUp, String typeofDevice, String salesrepMobileNumber, String salesrepMPesaMSISDN, int gjini, DateTime salesrepStartDateVod, DateTime salesrepTrainingStart, DateTime salesrepStartDateShop, DateTime salesrepMaternityLeaveStart, DateTime leavedateVod, DateTime leavedateShop, DateTime maternityleaveEndDate, DateTime trainingendDate, string commentsretailSales, string accountexecutive, string idNumber, int isinsured, string commentsretailOpSpecialist, string regionalsupervisor, string retailsalesAccountExecutive, string retailsalesAreaManager, DateTime birthDate, string sitecode, string districti, string shopmainCode, string latitude, string longitude, int status, int leaveReason, int uniform, string shenime, int statusAprovimi, bool njoftimEmailAprovim, int idkonfigkase)
        {
            String pass;
            pass = PasswordHelper.HashLogin(username, password);
            DbCore.DbAdmin.clsPerdorues perdorues = new DbCore.DbAdmin.clsPerdorues(idPerdoruesi);
            perdorues.EmriPerdorues = emri;
            perdorues.MbiemriPerdorues = mbiemri;
            perdorues.PerdoruesUsername = username;
            perdorues.PerdoruesTel = tel;
            perdorues.PerdoruesFax = fax;
            perdorues.PerdoruesEmail = email;
            perdorues.PerdoruesAdresa = adresa;
            perdorues.IdQyteti = idQyteti;
            perdorues.InfoHapur = infoHapur;
            perdorues.PerdoruesAktiv = perdoruesAktiv;
            perdorues.PerdoruesPassword = pass;
            perdorues.IdPerdoruesi = idPerdoruesi;
            perdorues.IdStatusDok = idStatusDok;
            perdorues.IdStilRaporti = idStilRaporti;
            perdorues.PasswordIPerkohshem = passwordIPerkohshem;
            perdorues.PerdoruesIKycur = perdoruesIKycur;
            perdorues.IdAmbjent = idAmbjent;
            perdorues.IdGjuha = idgjuha;
            perdorues.KontrollPassword = kontrollPassword;
            perdorues.ShfaqDtPrintimi = shfaqDtPrintimi;
            perdorues.KycurMobile = kycurMobile;
            perdorues.ShfaqPerdoruesMenu = shfaqPerdoruesMenu;
            perdorues.ShfaqMesazhePopup = shfaqNjoftime;
            perdorues.OColRolPerdoruesi = rolet;
            perdorues.IdAmbjentMobile = idAmbjentMobile;
          
            perdorues.ShopCode = shopcode;
            perdorues.ShopName = shopname;
            perdorues.DealerName = dealername;
            perdorues.UseriCRM = usericrm;
            perdorues.UserEtopUP = userEtopuP;
            perdorues.IDETopUp = iDEtopUp;
            perdorues.TypeOfDevice = typeofDevice;
            perdorues.SalesRepMobileNumber = salesrepMobileNumber;
            perdorues.SalesRepMPesaMSISDN = salesrepMPesaMSISDN;
            perdorues.Gjinia = gjini;
            perdorues.SalesRepStartDateVod = salesrepStartDateVod;
            perdorues.SalesRepStartDateShop =salesrepStartDateShop;
            perdorues.SalesRepTrainingStart = salesrepTrainingStart;
            perdorues.SalesRepMaternityLeaveStart = salesrepMaternityLeaveStart;
            perdorues.LeaveDateVod = leavedateVod;
            perdorues.LeaveDateShop = leavedateShop;
            perdorues.MaternityLeaveEndDate = maternityleaveEndDate;
            perdorues.TrainingEndDate = trainingendDate ;
            perdorues.CommentsRetailSales = commentsretailSales;
            perdorues.AccountExecutive =accountexecutive ;
            perdorues.IDNumber =idNumber ;
            perdorues.IsInsured = isinsured;
            perdorues.CommentsRetailOpSpecialist = commentsretailOpSpecialist;
            perdorues.RegionalSupervisor =regionalsupervisor ;
            perdorues.RetailSalesAccountExecutive = retailsalesAccountExecutive;
            perdorues.RetailSalesAreaManager =retailsalesAreaManager ;
            perdorues.Birthdate = birthDate;
            perdorues.SiteCode = sitecode;
            perdorues.District = districti ;
            perdorues.ShopMainCode = shopmainCode;
            perdorues.Latitude = latitude;
            perdorues.Longitude = longitude;
            perdorues.Status = status;
            perdorues.LeaveReason = leaveReason;
            perdorues.Uniform = uniform;
            perdorues.StatusAprovimi = statusAprovimi;
            perdorues.Shenime =shenime;
            perdorues.NjoftimEmailAprovim = njoftimEmailAprovim;
            perdorues.IdKonfigKasa = idkonfigkase;
            return perdorues;
        }

        /// <summary>
        /// krijon perdoruesin qe do te ruhet
        /// </summary>
        /// <returns>kthen clsPerdorues me perdoruesin qe do te ruhet</returns>
        /// <param name="idPerdoruesi"></param>
        private DbCore.DbAdmin.clsPerdorues krijoPerdorues(int idPerdoruesi)
        {
            int idQyteti = -1;
            if (qyteti_ASPxComboBox.SelectedItem != null)
                idQyteti = int.Parse(qyteti_ASPxComboBox.SelectedItem.Value.ToString());

            int Gjinia=0;
            if (cmbGender.SelectedItem != null)
                Gjinia = int.Parse(cmbGender.SelectedItem.Value.ToString());
            int IsInsured=0;
            if (cmbIsInsured.SelectedItem != null)
                IsInsured = int.Parse(cmbIsInsured.SelectedItem.Value.ToString());
            int StatusAprovimi=0;
            if (cmbStatusAprovimi.Value != null)
               StatusAprovimi = int.Parse(cmbStatusAprovimi.Value.ToString());
            int statusi = 0;
            if (!String.IsNullOrEmpty(cmbStatus.Text))
            {
                if (cmbStatus.Value == null)
                    throw new Exception("Statusi i punonjesit nuk ekziston!");
                bool konvertuar = int.TryParse(cmbStatus.Value.ToString(), out statusi);
                if (!konvertuar)
                    throw new Exception("Statusi i punonjesit nuk ekziston!");
            }
            int leaveReason = 0;
            if (!String.IsNullOrEmpty(cmbLeaveReason.Text))
            {
                if (cmbLeaveReason.Value == null)
                    throw new Exception("Arsyeja e largimit nuk ekziston!");
                bool konvertuar = int.TryParse(cmbLeaveReason.Value.ToString(), out leaveReason);
                if (!konvertuar)
                    throw new Exception("Arsyeja e largimit nuk ekziston!");
            }
            int uniform = 0;
            if (!String.IsNullOrEmpty(cmbUniform.Text))
            {
                if (cmbUniform.Value == null)
                    throw new Exception("Uniforma nuk ekziston!");
                bool konvertuar = int.TryParse(cmbUniform.Value.ToString(), out uniform);
                if (!konvertuar)
                    throw new Exception("Uniforma nuk ekziston!");
            }
            int KonfigKase = 0;
            if (cmbKonfigKase.Value != null)
                KonfigKase = int.Parse(cmbKonfigKase.Value.ToString());

            return krijoPerdorues(emri_TextBox.Text, mbiemri_TextBox.Text, username_TextBox.Text, tel_TextBox.Text, fax_TextBox.Text, email_TextBox.Text, adresa_TextBox.Text, idQyteti, true, aktiv_CheckBox.Checked, password_TextBox.Text, idPerdoruesi, 1, 2, cbPassPerkohshem.Checked, cbKycur.Checked, Convert.ToInt32(cmbAmbjenti.Value), Convert.ToInt32(cmbGjuha.SelectedItem.Value), kontrolloPassword_CheckBox.Checked, dtPrintimi_CheckBox.Checked, cbKycurMobile.Checked, shfaqPerdoruesMenu_CheckBox.Checked, cbShfaqNjoftime.Checked, ruajRole(), Convert.ToInt32(cmbAmbjentiMobile.Value), txtKodiDyqanit.Text, txtShopName.Text, txtDealer.Text, txtUserCRM.Text, txtUserEtopUP.Text,txtIDETopUp.Text, txtTypeDeviceSalesRep.Text, txtSalesRepMobile.Text, txtSalesRepMPesaMSISDN.Text, Gjinia, 
               dteSalesRepStartDate.Date, dteSalesRepTrainingDate.Date, dteSalesRepStartDateShop.Date, dteSalesRepStartMaternityLeave.Date,dteLeaveDateVodafoneVod.Date,dteLeaveDateShop.Date, dteMaternityLeaveEndDate.Date,
             dteTrainingEndDate.Date,txtComRetSal.Text,txtAccountExecutive.Text,txtIDNumber.Text, IsInsured, txtComROS.Text,txtRegSup.Text, txtRetailSAE.Text,txtRetailSAM.Text, dteBirthday.Date,txtSiteCode.Text,txtDistrict.Text, txtShopMainCode.Text, txtLatitude.Text,txtLongitude.Text, statusi, leaveReason, uniform, txtShenime.Text, StatusAprovimi, cbNjoftimEmailAprovim.Checked, KonfigKase);
        }

        /// <summary>
        ///sherben per te ruajtur nje perdorues
        /// </summary>
        private void ruajPerdorues()
        {
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            String password;
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            if (!Page.IsValid)
                return;
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            int idKonfigurim = ktheIdKonfig();
            bool kushtiMA = (clsAlternativaKushti.getAlternativa(Convert.ToInt32(cmbKonfigurimi.Value), "MA") == "Po");
            bool kushtiMMA = (clsAlternativaKushti.getAlternativa(Convert.ToInt32(cmbKonfigurimi.Value), "MMA") == "Po");
            bool kushtiSHTR = (clsAlternativaKushti.getAlternativa(Convert.ToInt32(cmbKonfigurimi.Value), "SHTR") == "Po");
            if (isValidPerdorues(ci, rm, idPerdoruesi))
            {
                bool eshteShtim;
                string passwordGjeneruar = String.Empty;
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), komponente);
                if (hfShtimModifikim.Value == "shtim" && kushtiMA)
                    cmbStatusAprovimi.Value = 1;

                if (hfShtimModifikim.Value == "modifikim" && kushtiMMA && Convert.ToInt32(cmbStatusAprovimi.Value) == 0)
                    cmbStatusAprovimi.Value = 1;

                clsAtributeTrupi statusaprovimi = new DbCore.DbShare.clsAtributeTrupi();
                statusaprovimi.mbushAtributSipasKompKonfDheKontrollit(0,idKonfigurim, "cmbStatusAprovimi", 100); 
                if (hfShtimModifikim.Value == "modifikim" && kushtiMMA
                     && statusaprovimi.Enabled == false)
                    cmbStatusAprovimi.Value = 1;
                clsPerdorues perdorues = new clsPerdorues();
                try
                {
                    perdorues = krijoPerdorues(idPerdoruesi);
                }
                catch (Exception ex)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, ex.Message, pnlMesazhi);
                    hfStatusi.Value = "false";
                    return;
                }
                

             colRolPerdorues rp = new colRolPerdorues();
                rp.mbushRolePerdoruesSipasPerdoruesi(idPerdoruesi);
                clsRoli ro = new clsRoli(rp[0].IdRoli);
                clsLicenca licenca = new clsLicenca(ro.IdLicenca);
                if (!licenca.isValid())
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgLicencaJoValid", ci), pnlMesazhi);
                    return;
                }
                int nrPerdoruesishPerLicence = DbCore.DbAdmin.colPerdoruesit.merrNrPerdoruesishSipasLicences(idPerdoruesi, licenca.IdLicenca);
                string roletOLD = "";
                if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim")
                {
                    if (!tedrejtaInfo.DShtim)
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNukKeniTeDrejta", ci), pnlMesazhi);
                        return;
                    }


                    if (licenca.NrPerdoruesish <= nrPerdoruesishPerLicence && (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim"))
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgPerdoruesitKaluarNrMax", ci), pnlMesazhi);
                        return;
                    }
                    DbCore.DbAdmin.clsKonfigurimeFjalekalimi konfigPass = new DbCore.DbAdmin.clsKonfigurimeFjalekalimi(idPerdoruesi);
                    if (konfigPass.GjeneroPassword)
                    {
                        passwordGjeneruar = PasswordHelper.GjeneroPassword(konfigPass.GjatesiaMinPassword, konfigPass.SpecialChars, konfigPass.UppercaseChars, konfigPass.NumbersChars);
                        perdorues.PerdoruesPassword = PasswordHelper.HashLogin(perdorues.PerdoruesUsername, passwordGjeneruar);
                        perdorues.PasswordIPerkohshem = true;
                    }
                    else
                    {
                        mesazh = konfigPass.isValidPassword(password_TextBox.Text, perdorues.PerdoruesPassword,ci,rm, hfShtimModifikim.Value);
                        if (!mesazh.Status)
                        {
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                            return;
                        }
                    }
                    perdorues.DateKrijimiPassword = DateTime.Now;
                    string ruajPassNeHistorik = konfigPass.RuajHistorikunPass && !konfigPass.GjeneroPassword ? "true" : "false";
                    hfNdryshuarPass.Set("PassIRi", ruajPassNeHistorik);//eshte password i ri mqnse po shtohet nje perdorues i ri, ky hf perdoret per te ruajtur ne historik pass e ri
                    mesazh = perdorues.ruaj(idNdermarrje, idNdermarjeVit, "", false, ruajPassNeHistorik, passwordGjeneruar);
                    eshteShtim = true;
                }
                else
                {
                    clsPerdorues perdoruesVjeter = new clsPerdorues(int.Parse(hfId.Value));
                    if (!perdoruesVjeter.PerdoruesAktiv && aktiv_CheckBox.Checked && licenca.NrPerdoruesish < nrPerdoruesishPerLicence + 1) //ne rast se ai behet nga inaktiv ne aktiv, atehere duhet te kontrollojme nese tejkalohet nr i perdoruesve per licence
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgPerdoruesitKaluarNrMax", ci), pnlMesazhi);
                        return;
                    }
                    if (!tedrejtaInfo.DMod)
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNukKeniTeDrejta", ci), pnlMesazhi);
                        hfStatusi.Value = "false";
                        return;
                    }
                    eshteShtim = false;
                    perdorues.IdPerdorues = Convert.ToInt32(hfId.Value);
                    clsPerdorues perd = new clsPerdorues(perdorues.IdPerdorues);
                    //clsPerdorues perd = new clsPerdorues(perdorues.IdPerdorues, false);
                    perdorues.IdStilRaporti = perd.IdStilRaporti;
                    perdorues.InfoHapur = perd.InfoHapur;
                    perdorues.DateKrijimiPassword = perd.DateKrijimiPassword;
                    String pass_vjeter = this.txtPasswordieksistues.Text;
                    String pass_ri = password_TextBox.Text;
                    String konfirmo_pass = this.konfirmo_Textbox.Text;
                    //if (perd.idgjuha != perdorues.idgjuha)
                    //    hfRedirect.Value = "true";
                    //else hfRedirect.Value = "false";
                    hfNdryshuarPass.Set("PassIRi", "false");
                    DbCore.DbAdmin.clsKonfigurimeFjalekalimi konfigPass = new DbCore.DbAdmin.clsKonfigurimeFjalekalimi(idPerdoruesi);
                    if (konfigPass.GjeneroPassword && perdorues.PasswordIPerkohshem) //rasti kur behet shkycja e nje perdoruesi dhe eshte chekuar fjalekalim i perkohshem duhet te gjenerohet password nese te politikat e fjalekalimi eshte zgjedhur opsioni i gjenerimit te pass per perdoruesit.
                    {
                        passwordGjeneruar = PasswordHelper.GjeneroPassword(konfigPass.GjatesiaMinPassword, konfigPass.SpecialChars, konfigPass.UppercaseChars, konfigPass.NumbersChars);
                        perdorues.PerdoruesPassword = PasswordHelper.HashLogin(username_TextBox.Text, passwordGjeneruar);
                    }
                    else
                    {
                        password = PasswordHelper.HashLogin(username_TextBox.Text, pass_ri);
                        if (pass_vjeter == "" && pass_ri == "" && konfirmo_pass == "")
                            perdorues.PerdoruesPassword = perd.PerdoruesPassword; //perdoruesit i lihet passwordi i vjeter prandaj nuk ruhet ne historik
                        else
                        {
                            if (pass_ri == konfirmo_pass)
                            {
                                perdorues.DateKrijimiPassword = DateTime.Now;
                                mesazh = konfigPass.isValidPassword(pass_ri, perdorues.PerdoruesPassword, Convert.ToInt32(hfId.Value),ci,rm, hfShtimModifikim.Value);
                                if (!mesazh.Status)
                                {
                                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                                    return;
                                }
                                if (pass_vjeter == "" && cbPassPerkohshem.Checked) //admin reseton passwordin e perdoruesi qe eshte i kycur
                                {
                                    perdorues.PerdoruesPassword = password;
                                }
                                else
                                    if (PasswordHelper.HashLogin(username_TextBox.Text, pass_vjeter) == perd.PerdoruesPassword) //modifikim pass
                                    {
                                        perdorues.PerdoruesPassword = password;
                                    }
                                    else
                                    {
                                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgPerdoruesitPasswordIGabuar", ci), pnlMesazhi);
                                        perdorues.PerdoruesPassword = perd.PerdoruesPassword;
                                        return;
                                    }
                                hfNdryshuarPass.Set("PassIRi", "true");//rasti kur ka ndryshuar passwordi i perdoruesit dhe do ruhet ne historikun e passwordeve
                            }
                            else
                            {
                                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgPerdoruesitPasswordetNukPerkojne", ci), pnlMesazhi);
                                return;
                            }
                        }
                    }

                    bool vazhdo = true;
                    int roli = DbCore.DbAdmin.colRolPerdorues.merrRoleSipasPerdoruesiDheKodRoli(perdorues.IdPerdorues, "RA");
                    colRolPerdorues nrperrol = new colRolPerdorues();
                    nrperrol.mbushRolePerdoruesSipasRoli(roli);
                    if (nrperrol.Count == 1)
                    {
                        vazhdo = false;
                        foreach (clsRolPerdorues nrp in perdorues.OColRolPerdoruesi)
                        {
                            if (nrp.IdRoli == roli)
                            {
                                vazhdo = true;
                                break;
                            }
                        }
                    }


                    roletOLD = clsPerdorues.merrKodeteRolevesipasPerdoruesit(perdorues.IdPerdorues, licenca.IdLicenca);//merren rolet e vjetra te perdoruesit qe po modifikohet perpara se te behen nderyshimet e roleve
                    if (vazhdo)
                        // mesazh = perdorues.modifiko(hfNdryshuarPass.Get("PassIRi").ToString(), passwordGjeneruar);
                    
                    mesazh = perdorues.modifiko( hfNdryshuarPass.Get("PassIRi").ToString(), passwordGjeneruar, idNdermarrje, kushtiMA, idNdermarjeVit);
                    

                    else { clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgPerdoruesitTePakten1Admin", ci), pnlMesazhi); return; }
                }
                if (mesazh.Status)
                {
                    {
                        clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("msgRuajtjeMeSukses", ci), pnlMesazhi);
                    }

                    if (DbCore.mySessionObjects.ktheIdPerdoruesi(Session) == perdorues.IdPerdorues)
                    {
                        DbCore.mySessionObjects.ruajPerdoruesNeSesion(Session, perdorues);
                    }
                    pastroFusha();
                    hfStatusi.Value = "true";
                    if (eshteShtim)
                        shtoPerdoruesNeGrid(perdorues.IdPerdorues, rm, ci);
                    else //modifikim
                    {
                        modifikoPerdoruesNeGrid(perdorues.IdPerdorues, rm, ci);
                        if (clsAlternativaKushti.getAlternativa(Convert.ToInt32(cmbKonfigurimi.Value), "EMAILNDRYSHIMROLI") == "Po" && (hfState.Contains("dergoEmailNdryshimRoli") && Convert.ToBoolean(hfState.Get("dergoEmailNdryshimRoli"))))
                        {
                            clsMesazh msgEmail = EmailComposer.emailRoletENdryshuaraTePerdoruesit(idNdermarrje, perdorues.IdPerdorues, licenca.IdLicenca, perdorues.EmriPerdorues, perdorues.MbiemriPerdorues, roletOLD);
                            if (!msgEmail.Status)
                            {
                                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, msgEmail.PershkrimMesazhi, pnlMesazhi);
                                hfStatusi.Value = "false";
                            }
                        }
                    }
                }
                else
                {
                    string mesazhGabimi = String.IsNullOrEmpty(mesazh.PershkrimMesazhi) ? rm.GetString("msgGabimiNeRuajtje", ci) : mesazh.PershkrimMesazhi;
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhGabimi, pnlMesazhi);
                    hfStatusi.Value = "false";
                }
                ASPxPageControl1.ActiveTabIndex = 0;
            }
            else
            {
                hfStatusi.Value = "false";
            }
            pnlMesazhi.Update();
            mbushlisteRolesh();
        }

        /// <summary>
        /// pastron fushat e faqes
        /// </summary>
        /// 
        public int ktheIdKonfig()
        {
            int idKonfigurim = -1;
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            if (!String.IsNullOrEmpty(cmbKonfigurimi.Text))  //nese eshte zgjedhur nje konfigurim tek combo e konfigurimeve
            {
                if (cmbKonfigurimi.Value != null)
                    idKonfigurim = int.Parse(cmbKonfigurimi.Value.ToString());
                else
                    idKonfigurim = clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(cmbKonfigurimi.Text, idNdermarrje);
            }
            else
            {//nese nuk ehste zgjedhur asnje konfigurim merret konfigurimi default                
                clsKonfigurimAmbjenti clsKonf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                clsKonf.mbushKonfigDefaultKomponentes(100, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                if (clsKonf != null)
                    clsKonf.mbushKonfigDefaultKomponentes(100, idNdermarrje);
                idKonfigurim = clsKonf.IdKonfigAmbjente;
            }
            return idKonfigurim;
        }
        private void pastroFusha()
        {
            hfVleratFushaAutomatike.Clear();
            qyteti_ASPxComboBox.SelectedIndex = -1;
        }

        private colRolPerdorues ruajRole()
        {
            colRolPerdorues roleper = new colRolPerdorues();
            List<object> rreshtat = this.gvRolet.GetSelectedFieldValues("IdRoli");

            foreach (object id in rreshtat)
            {
                clsRolPerdorues rp = new clsRolPerdorues();
                rp.IdRoli = Convert.ToInt32(id);
                roleper.Add(rp);
            }
            return roleper;
        }

        /// <summary>
        /// kontrollon nese te dhenat qe jane plotesuara jane 
        ///te lejueshme apo jo
        /// </summary>
        /// <returns> true  ose false</returns>
        private bool isValidPerdorues(CultureInfo ci, ResourceManager rm, int idPerdoruesi)
        {
            bool isValid = true;
            using (DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin())
            {
                if (gvRolet.Selection.Count == 0)
                {
                    isValid = false;
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgPerdoruesitZgjidhRolin", ci), pnlMesazhi);
                    return isValid;
                }
                if (!cbPassPerkohshem.Checked)
                {
                    if (password_TextBox.Text != "" && txtPasswordieksistues.Text == "" && hfShtimModifikim.Value == "modifikim")
                    {
                        isValid = false;
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgPerdoruesitPlotesoPassword", ci), pnlMesazhi);
                        return isValid;
                    }
                }
                if (password_TextBox.Text != konfirmo_Textbox.Text)
                {
                    isValid = false;
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgPerdoruesitPasswordGabuar", ci), pnlMesazhi);
                    return isValid;
                }

                if (!String.IsNullOrEmpty(txtIDNumber.Text))
                {
                    string mesazh = "";
                    bool isValidNrPersonal = clsFunksione.isValidNrPersonal(txtIDNumber.Text, out mesazh);
                    if (!isValidNrPersonal)
                    {
                        isValid = false;
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh, pnlMesazhi);
                        return isValid;
                    }
                }

                String mesazhGabimi;
                if (!String.IsNullOrEmpty(email_TextBox.Text) && !DbCore.clsFunksione.isValidEmail(out mesazhGabimi, email_TextBox.Text) && clsAlternativaKushti.getAlternativa(Convert.ToInt32(cmbKonfigurimi.Value), "VFE") == "Po")
                {
                    isValid = false;
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Fusha e emailit te jene ne format emaili!", pnlMesazhi);
                    return isValid;
                }
                if (!String.IsNullOrEmpty(cmbStatus.Text) )
                {
                    clsShopsHierarkiStatus StatusAktiv = new clsShopsHierarkiStatus(cmbStatus.Text);
                    if (StatusAktiv.Aktiv == false)

                    {
                        isValid = false;
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Statusi i perdoruesit nuk eshte Aktiv", pnlMesazhi);
                        return isValid;

                    }
                }

                if (!String.IsNullOrEmpty(cmbLeaveReason.Text))
                {
                    clsShopsHierarkiLeaveReason LeaveReasonAktiv = new clsShopsHierarkiLeaveReason(cmbLeaveReason.Text);
                    if (LeaveReasonAktiv.Aktiv == false)

                    {
                        isValid = false;
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Arsyeja e largimit nuk eshte Aktive", pnlMesazhi);
                        return isValid;

                    }
                }

                if (!String.IsNullOrEmpty(cmbUniform.Text))
                {
                    clsShopsHierarkiUniform UniformAktiv = new clsShopsHierarkiUniform(cmbUniform.Text);
                    if (UniformAktiv.Aktiv == false)

                    {
                        isValid = false;
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Uniforma nuk eshte Aktive", pnlMesazhi);
                        return isValid;

                    }
                }
                if ((cmbStatus.Text == "Voluntary Leave" || cmbStatus.Text == "Involuntary Leave") && String.IsNullOrEmpty(txtSalesRepMPesaMSISDN.Text))
                {
                    isValid = false;
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Plotesoni fushen Revoke Eforms!", pnlMesazhi);
                    return isValid;

                }
                

                if (!String.IsNullOrEmpty(txtSalesRepMobile.Text))
                {
                    string mesazh = "";
                    bool isValidMSISDN = clsFunksione.isValidMSISDN(txtSalesRepMobile.Text, out mesazh);
                    if (!isValidMSISDN)
                    {
                        isValid = false;
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh, pnlMesazhi);
                        return isValid;
                    }
                }
                if (dbAdmin.ekzistonPerdoruesi(username_TextBox.Text) && (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim"))
                {
                    isValid = false;
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgPerdoruesitEkzistonPerdoruesi", ci), pnlMesazhi);
                    return isValid;
                }
            }
            return isValid;
        }

        /// <summary>
        /// perdoret per te rikonfiguruar griden kur ndryshohet konfigurimi
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void grid_ListPerdoruesit_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string idkomponente = "";
            string kodkonfigurimi = "";
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                kodkonfigurimi = arr[1];
                idkomponente = arr[0];
                if (arr[2] == "")
                    grid_ListPerdoruesit.FilterExpression = "";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                    //filtra.mbushFiltraGridaSipasFiltraKodi(arr[2], idNdermarrje);
                    int idgjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idgjuha, "grid_ListPerdoruesit", komponente, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], idNdermarrje, koka.IdGridaKoka);

                    GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), "grid_ListPerdoruesit", grid_ListPerdoruesit, cmbKonfigurimi.Text.Split(';')[0], 100.ToString(), idgjuha);
                    
                    //DbCore.DbAdmin.clsFiltraGrida filtra = dbAdmin.merrFiltraGridaSipasFiltraKodi(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    if (filtra.FiltraKodi != null)
                    {
                        this.grid_ListPerdoruesit.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, grid_ListPerdoruesit);

                        konfiguroVleraFillestare(idPerdoruesi, idNdermarrje, rm, ci, idgjuha);
                        hfStatusi.Value = "true";
                    }
                    else hfStatusi.Value = "false";
                }
            }
            else
                if (arr.Length == 2)//nese eshte zgjedhur nje konfigurim tek combo e konfigurimeve
                {

                int idgjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
                kodkonfigurimi = arr[1];
                    idkomponente = arr[0];
                    GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), "grid_ListPerdoruesit", grid_ListPerdoruesit, cmbKonfigurimi.Text.Split(';')[0], 100.ToString(), idgjuha);
            }
                else
                {
                    idkomponente = e.Parameters;
                }
            //  konfiguroGride();
            //  funksion.percaktoVisibleColumnsGridSipasKodKonfigurimi(grid_ListPerdoruesit, kodkonfigurimi, idkomponente);
            grid_ListPerdoruesit.Selection.UnselectAll();

        }

        /// <summary>
        /// percakton templatin per checkun e aktivit
        /// </summary>
        private void percaktoTamplate()
        {
            GridViewDataColumn col = grid_ListPerdoruesit.Columns["PerdoruesAktiv"] as GridViewDataColumn;
            col.DataItemTemplate = new MyCheckTemplate(true, false);
        }

        protected void ASPxGridView_Autorizimet_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            colAutorizimetKoka autorizimet = new colAutorizimetKoka();
            if (e.Parameters != "-1")
                autorizimet = new colAutorizimetKoka(int.Parse(this.grid_ListPerdoruesit.GetRowValues(int.Parse(e.Parameters), "IdPerdorues").ToString()));
            //autorizimet = dbAdmin.merrAutorizimKokaPerPerdorues(int.Parse(this.grid_ListPerdoruesit.GetRowValues(int.Parse(e.Parameters),"IdPerdorues").ToString()));
            ASPxGridView_Autorizimet.DataSource = autorizimet;
            ASPxGridView_Autorizimet.DataBind();
        }

        private void konfiguroGrideAutorizimesh()
        {//konfigurimet e grides
            GridUtil.percaktoVisibleColumnsMeWidth(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), ASPxGridView_Autorizimet, "ASPxGridView_Autorizimet", "Shto_Autorizimet.aspx");
            //funk.konfiguroGrideListeMadhe(ASPxGridView_Autorizimet, "IdAutorizimKoka");
            GridUtil.konfigGrideListeEMadhePaTheme(ASPxGridView_Autorizimet, "IdAutorizimKoka");
        }

        protected void gvRolet_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            percaktoTamplateRoli();
            mbushlisteRolesh();
        }

        private void konfiguroGrideRoli()
        {
            KonfigurimComboGride.ShtoPerdoruesSipasKrijuesit(gvRolet, idPerdoruesi, Session, komponente, guidString, "IdKrijuesi");

            percaktoTamplateRoli();
            //funk.konfiguroGrideListeMadhe(gvRolet, "IdRoli");  
            GridUtil.konfigGrideListeEMadhePaTheme(gvRolet, "IdRoli");
            gvRolet.Columns["#"].VisibleIndex = 0;
            gvRolet.SettingsLoadingPanel.Mode = GridViewLoadingPanelMode.Disabled;
        }

     
        protected void gvRolet_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName == "AktivRoli")
            {
                CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
                ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                (e.Editor as ASPxComboBox).Items.Clear();
                (e.Editor as ASPxComboBox).Items.Add("");
                (e.Editor as ASPxComboBox).Items.Add(rm.GetString("cmbPerdoruesitAktiv", ci), true);
                (e.Editor as ASPxComboBox).Items.Add(rm.GetString("cmbPerdoruesitJoAktiv", ci), false);
            }
        }

        private void mbushlisteRolesh()
        {
            int idLicenca = DbCore.DbAdmin.clsLicenca.merrIdLicencePerdoruesi(idPerdoruesi);
            DataTable dt = DbCore.DbAdmin.colRoli.merrRoletDTSipasLidhes(idLicenca, idPerdoruesi, int.Parse(hfId.Value));
            gvRolet.DataSource = dt;
            gvRolet.KeyFieldName = "IdRoli";
            gvRolet.DataBind();
            dt.Dispose();
        }

        protected void gvRolet_DataBound(object sender, EventArgs e)
        {
            if (this.gvRolet.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per 
                //perzgjidh
                GridViewCommandColumn check = new GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true; check.Width = Unit.Percentage(2);
                //   check.SetColVisibleIndex(0);
                //behet per te afishuar rreshtin qe do sherbej per filtrim
                gvRolet.Settings.ShowFilterRow = true;
                gvRolet.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                gvRolet.Settings.ShowFilterRowMenu = true;
                gvRolet.Columns.Add(check);
                gvRolet.KeyFieldName = "IdRoli";
                gvRolet.SettingsBehavior.AllowSelectByRowClick = true;
                gvRolet.SettingsBehavior.AllowFocusedRow = true;
            }

            GridUtil.percaktoVisibleColumnsMeWidth(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), gvRolet, "gvRolet", komponente);
        }

        private void percaktoTamplateRoli()
        {
            GridViewDataColumn col = gvRolet.Columns["AktivRoli"] as GridViewDataColumn;
            col.DataItemTemplate = new MyCheckTemplate(true, false);
        }

        protected void gvRolet_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            gvRolet.Selection.UnselectAll();
            gvRolet.FilterExpression = "";
            if (e.Parameters != "-1" && e.Parameters.Split(';').Length<2)
            {
                colRolPerdorues rolPer = new colRolPerdorues();
                rolPer.mbushRolePerdoruesSipasPerdoruesi(int.Parse(this.grid_ListPerdoruesit.GetRowValues(int.Parse(e.Parameters), "IdPerdorues").ToString()));
                GridUtil.percaktoVisibleColumnsMeWidth(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), gvRolet, "gvRolet", komponente);
                foreach (clsRolPerdorues rp in rolPer)
                {
                    int index = gvRolet.FindVisibleIndexByKeyValue(rp.IdRoli);
                    gvRolet.Selection.SelectRow(index);
                }
            }
            gvRolet.PageIndex = 0;
        }
    }
}