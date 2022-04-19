using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Collections;
using System.Drawing;
using System.Data;
using PlatinumWeb.Templates;
using System.Globalization;
using System.Resources;
using DbCore.IMBUtils.Extensions;
using DbCore.IMBUtils.Types;
using DbCore.DbShare;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{
    public partial class Shto_PikeShitjeFurnizimi : MyPageBase
    {
        public static int idNderm = -1;
        public static bool isShtim = true;
        private int idgjuha, idPerdorues, idviti, idNdermarrje;
        DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();
        DbCore.DbKontabiliteti.clsLlogari oLlog = new DbCore.DbKontabiliteti.clsLlogari();
        public static int id = 0;
        public static DbCore.DbRegjistrim.clsPikeShitjeFurnizimi njesia;
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
            {
                id = int.Parse(Request.QueryString["id"]);
            }
            else
                id = 0;

            if (id != 0)
            {
                njesia = new DbCore.DbRegjistrim.clsPikeShitjeFurnizimi(id);
            }
            else
                njesia = new DbCore.DbRegjistrim.clsPikeShitjeFurnizimi();
            // mbushListePikaShitjeFurnizimi();
            //   konfiguroGride();
        }
        private string guidString;
        private string komponente = "Shto_PikeShitjeFurnizimi.aspx";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                DbCore.clsFunksione.logout(Session,true,"FaqePaautorizuar");
            }
            idPerdorues = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerdorues);
            }
            oPerdorues = DbCore.mySessionObjects.kthePerdorues(Session);
            idviti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            idgjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            percaktoTemplateMenu(idgjuha, idviti, idPerdorues, idNdermarrje, ASPxMenu1);
            CultureInfo ci = DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo(idgjuha);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (!Page.IsPostBack)
            {
                guidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                hfState.Set("guidString", guidString);
                vendosHfMePerkthime(rm, ci);
                EmrateTabeve(rm, ci);
                hfState.Set("idGjuha", idgjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                ASPxPageControl1.ActiveTabIndex = 0;
                konfiguroVleraFillestare(idNdermarrje, rm, ci, idgjuha);
                if (id != 0)
                    njesia = new DbCore.DbRegjistrim.clsPikeShitjeFurnizimi(id);
                else
                    njesia = new DbCore.DbRegjistrim.clsPikeShitjeFurnizimi();
                mbushGridPikeshNgaDB();
                if (Request.QueryString["sf"] == "shitje")
                    gvPikeShitjeFurnizimi.FilterExpression = "[ShitjeFurnizimi]=true";
                else
                    gvPikeShitjeFurnizimi.FilterExpression = "[ShitjeFurnizimi]=false";
                konfiguroGride(cmbKonfigurimi.Text.Split(';')[0], 524, rm, ci);
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "gvPikeShitjeFurnizimi", gvPikeShitjeFurnizimi, cmbKonfigurimi.Text.Split(';')[0], "524", (int)hfState["idGjuha"]);
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdorues, idNdermarrje, idviti, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdorues, idNdermarrje, idviti, DbCore.clsFunksione.GetKomponente(Page.Request));
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                gvPikeShitjeFurnizimi.Columns["#"].VisibleIndex = 0;
            }
            else
            {
                guidString = (string)hfState["guidString"];
                mbushGridPikeshNgaSession();
                konfiguroGride(cmbKonfigurimi.Text.Split(';')[0], 524, rm, ci);
            }
            GridUtil.konfigGrideListeEMadhePaTheme(gvPikeShitjeFurnizimi, "IdPikeShitjeFurnizimi");
            AspxWebControlUtils.perkthePopUp(popFshi, rm.GetString("labelKujdes", ci), lblMsgbox, rm.GetString("labelAdministrimiMsgJeniSigurt", ci), ButtonCancel, rm.GetString("labelAnullo", ci));
            GridUtil.ToolTipButonaveMbiGride(gvPikeShitjeFurnizimi, ci, rm);
            popupUniversal.HeaderText = rm.GetString("popupAdministrimiUniversal", ci);
            string konfigValue = cmbKonfigurimi.Value != null ? cmbKonfigurimi.Value.ToString() : clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(cmbKonfigurimi.Text.Split(';')[0], idNdermarrje).ToString();
            clsToolbarConfig.mbushComboBoxFiltra(idgjuha, idNdermarrje, "gvPikeShitjeFurnizimi", int.Parse(konfigValue), komponente);            
        }

        private void vendosHfMePerkthime(ResourceManager rm, CultureInfo ci)
        {
            konfigurimi_Label.Text = rm.GetString("lblModeli", ci);
            hfState.Set("msgPikeShitjeFurnDuhetTeZgjidhni1PikeShitjeFurn", rm.GetString("msgPikeShitjeFurnDuhetTeZgjidhni1PikeShitjeFurn", ci));
            hfState.Set("pikeShitjeTab", rm.GetString("pikeShitjeTab", ci));
            hfState.Set("msgPikeFurnizimi", rm.GetString("msgPikeFurnizimi", ci));
            hfState.Set("headerPopUpZgjidhAutorizimet", rm.GetString("headerPopUpZgjidhAutorizimet", ci));
        }

        /// <summary>
        /// Vendos emrat e tabeve ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateTabeve(ResourceManager rm, CultureInfo cultinf)
        {
            ASPxPageControl1.TabPages[0].Text = rm.GetString("TePergjithshmeTab", cultinf);
            ASPxPageControl1.TabPages[1].Text = rm.GetString("pikeShitjeTab", cultinf);
        }

        ///// <summary>
        ///// mbush combon e filtrave
        ///// </summary>
        ///// <param name="idNdermarrje"></param>
        //private void mbushComboBoxFiltra(int idNdermarrje)
        //{
        //    //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
        //    //DbCore.DbAdmin.clsGridaKoka koka = dbAdmin.merrGridaKokaByEmri("gvPikeShitjeFurnizimi", "Shto_NjesiAdministrative.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka("gvPikeShitjeFurnizimi", "Shto_PikeShitjeFurnizimi.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //    DbCore.DbAdmin.colFiltratGrida colFiltra = new DbCore.DbAdmin.colFiltratGrida(koka.IdGridaKoka, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //    colFiltra.Insert(0, new DbCore.DbAdmin.clsFiltraGrida()); //colFiltra = dbAdmin.merrGjitheFiltratGridaByGridaKoka(koka.IdGridaKoka, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //    MenuFilter.colekstioni = colFiltra;
        //    MenuFilter.ValueField = "IdFiltra";
        //    MenuFilter.TextField = "FiltraShenime";
        //}
        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="idNderVit"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        private void percaktoTemplateMenu(int idGjuha, int idViti, int idPerdorues, int idNdermarrje, ASPxMenu aSPxMenu1)
        {
            clsToolbarConfig.percaktoTemplateMenu(idGjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1, DbCore.clsFunksione.GetKomponente(Page.Request), this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, hfShtimModifikim.Value == "modifikim" ? false : true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
        }

        protected void RuajKolona_Click(object sender, EventArgs e)
        {
            int idfiltri = 0;
            int idkonf = DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(cmbKonfigurimi.Text.Split(';')[0], idNdermarrje);
            DbCore.clsMesazh mesazh = GridUtil.ruajFiltra(idNdermarrje, idPerdorues, idgjuha, "gvPikeShitjeFurnizimi", komponente, "FilterDefault", gvPikeShitjeFurnizimi.FilterExpression, gvPikeShitjeFurnizimi, "Kodi", idkonf, out idfiltri);//ruan filtrin e zgjedhur tek filtrat
            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                return;

            }
            mesazh = GridUtil.ruajkonfigurimgride(gvPikeShitjeFurnizimi, cmbKonfigurimi.Text, idNdermarrje, idPerdorues, 524, idfiltri, idviti, DbCore.mySessionObjects.ktheCultureInfo(Session), DbCore.mySessionObjects.ktheGjuhe(Session));/// ruan konfigurimin e grides dhe filtrin


            string konfigValue = cmbKonfigurimi.Value != null ? cmbKonfigurimi.Value.ToString() : clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(cmbKonfigurimi.Text.Split(';')[0], idNdermarrje).ToString();
            clsToolbarConfig.mbushComboBoxFiltra(idgjuha, idNdermarrje, "gvPikeShitjeFurnizimi", int.Parse(konfigValue), komponente);

            percaktoTemplateMenu(idgjuha, idviti, idPerdorues, idNdermarrje, ASPxMenu1);
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
            percaktoTemplateMenu(idgjuha, idviti, idPerdorues, idNdermarrje, ASPxMenu1);

        }
        protected void gvPikeShitjeFurnizimi_DataBound(object sender, EventArgs e)
        {// shton colonen # per selektim dhe disa karakteristika te grides
            if (this.gvPikeShitjeFurnizimi.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per 
                //perzgjidh
                DevExpress.Web.GridViewCommandColumn check = new DevExpress.Web.GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true; check.Width = Unit.Percentage(2);
                //  check.SetColVisibleIndex(0);
                //behet per te afishuar rreshtin qe do sherbej per filtrim
                gvPikeShitjeFurnizimi.Settings.ShowFilterRow = true;
                gvPikeShitjeFurnizimi.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                gvPikeShitjeFurnizimi.Settings.ShowFilterRowMenu = true;
                gvPikeShitjeFurnizimi.Columns.Add(check);
                gvPikeShitjeFurnizimi.KeyFieldName = "IdPikeShitjeFurnizimi";
                gvPikeShitjeFurnizimi.SettingsBehavior.AllowSelectByRowClick = true;
                gvPikeShitjeFurnizimi.SettingsBehavior.AllowFocusedRow = true;
            }
            //  this.gvPikeShitjeFurnizimi.Columns["#"].VisibleIndex = 0;
        }
        private void konfiguroGride(string kodKonfigurimi, int idKomponente, ResourceManager rm, CultureInfo ci)
        {//konfiguron griden

            percaktoTamplateAutorizime();

            KonfigurimComboGride.shtoPikeShitjeFurnizimi(gvPikeShitjeFurnizimi, rm, ci);
            KonfigurimComboGride.shto_DegeAdministrative(gvPikeShitjeFurnizimi, idNdermarrje, Session, komponente, guidString);
            this.gvPikeShitjeFurnizimi.Columns["#"].VisibleIndex = 0;
           
        }

 
        //thirret sa here qe behet Callback, dhe ben edhe njeher lidhjen me datasource-in
        protected void gvPikeShitjeFurnizimi_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            if (e.CallbackName == "COLUMNMOVE" && gvPikeShitjeFurnizimi.AllColumns[int.Parse(e.Args[0])].Width.Value == 0)
                gvPikeShitjeFurnizimi.AllColumns[int.Parse(e.Args[0])].Width = Unit.Percentage(3);
            if (e.CallbackName == "APPLYFILTER" && e.Args[0] == "")
            {
                DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
                ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
                cmbFiltra.Text = "";
                if (Request.QueryString["sf"] == "shitje")
                {
                    gvPikeShitjeFurnizimi.FilterExpression = "[ShitjeFurnizimi]=true";
                }
                else
                {
                    gvPikeShitjeFurnizimi.FilterExpression = "[ShitjeFurnizimi]=false";
                }
            }
            percaktoTamplateAutorizime();
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            GridUtil.ToolTipButonaveMbiGride(gvPikeShitjeFurnizimi, ci, rm);
        }

        //bere  me e konfigurueshme
        protected void gvPikeShitjeFurnizimi_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            string TeGjithe = rm.GetString("GridHeaderFilterFillItemTeGjithe", ci);
            string nga = rm.GetString("GridHeaderFilterFillItemNga", ci);
            if (e.Column.FieldName == "Kodi" || e.Column.FieldName == "Pershkrimi")
            {
                e.Values.Clear();
                //e.AddShowAll();
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

            //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            //filtra.mbushFiltraGridaSipasFiltraEmri(cmbFiltra.Text, idNdermarrje);
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvPikeShitjeFurnizimi", komponente, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), int.Parse(cmbKonfigurimi.Value.ToString()));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
            //DbCore.DbAdmin.clsFiltraGrida filtra = dbAdmin.merrFiltraGridaSipasFiltraEmri(cmbFiltra.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();
                //mbushComboBoxFiltra(idNdermarrje);
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvPikeShitjeFurnizimi", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
                percaktoTemplateMenu(idgjuha, idviti, idPerdorues, idNdermarrje, ASPxMenu1);

                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                if (Request.QueryString["sf"] == "shitje")
                {
                    gvPikeShitjeFurnizimi.FilterExpression = "[ShitjeFurnizimi]=true";


                }
                else
                {
                    gvPikeShitjeFurnizimi.FilterExpression = "[ShitjeFurnizimi]=false";


                }
                cmbFiltra.Text = "";
                //  konfiguroVleraFillestare();
                hfStatusi.Value = "true";

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

            //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            DbCore.DbAdmin.clsFiltraGrida filtri = new DbCore.DbAdmin.clsFiltraGrida();
            filtri.FiltraKodi = cmbFiltra.Text;
            filtri.FiltraShenime = cmbFiltra.Text;
            filtri.FiltraUniversal = false;// Universal_ASPxCheckBox.Checked;
            //DbCore.DbAdmin.clsGridaKoka koka = dbAdmin.merrGridaKokaByEmri("gvPikeShitjeFurnizimi", "Shto_NjesiAdministrative.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvPikeShitjeFurnizimi", komponente, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gvPikeShitjeFurnizimi.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("Kodi", gvPikeShitjeFurnizimi);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvPikeShitjeFurnizimi.GetSortedColumns();
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
            //    filtri.KoloneRenditje = "Kodi";
            //    filtri.DrejtimRenditje = true;
            //}
            DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();
            //oPerdorues = (DbCore.DbAdmin.clsPerdorues)(CacheLayer.GlobalCacheManager.MySessionCache["oClsPerdoruesi"]);
            oPerdorues = DbCore.mySessionObjects.kthePerdorues(Session);
            filtri.IdPerdoruesi = oPerdorues.IdPerdorues;
            filtri.IdNdermarje = idNdermarrje;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            filtri.IdStatusDok = 1;

            mesazh = filtri.ruaj();
            //mbushComboBoxFiltra(idNdermarrje);
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvPikeShitjeFurnizimi", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
            percaktoTemplateMenu(idgjuha, idviti, idPerdorues, idNdermarrje, ASPxMenu1);

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
                rreshtat = gvPikeShitjeFurnizimi.GetSelectedFieldValues("IdPikeShitjeFurnizimi");
            else
            {
                rreshtat = new List<object>();
                rreshtat.Add(hfId.Value);
            }
            //List<object> rreshtat = gvPikeShitjeFurnizimi.GetSelectedFieldValues("IdPikeShitjeFurnizimi");
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            if (rreshtat.Count == 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgPikeShitjeFurnZgjidhni1PikeShitjeFurn", ci), pnlMesazhi);
                return;
            }
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            //clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "", pnlMesazhi);
            List<string> TeFshire = new List<string>(), TePaFshire = new List<string>();
            DbCore.DbRegjistrim.clsDatabaseRegjistrim dbRegjistrim = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
            foreach (object id in rreshtat)
            {
                DbCore.DbRegjistrim.clsPikeShitjeFurnizimi oNjesiAdm = new DbCore.DbRegjistrim.clsPikeShitjeFurnizimi(Convert.ToInt32(id));
                DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                konf.IdKonfigAmbjente = oNjesiAdm.IdKonfig;
                konf.mbushKonfigAmbjSipasId(konf.IdKonfigAmbjente);
                //konf = new DbCore.DbShare.clsDatabaseShare().merrKonfigAmbjSipasId(konf)[0];

                bool lidhur = dbRegjistrim.eshteDokumentiILidhurCelje(oNjesiAdm.IdPikeShitjeFurnizimi.ToString(), konf.IdNivel.ToString());
                if (lidhur)
                {
                    TePaFshire.Add(oNjesiAdm.Kodi);
                    continue;
                } oNjesiAdm.IdPerdorues = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                mesazh = oNjesiAdm.fshi();
                if (oNjesiAdm.IdPikeShitjeFurnizimi == 0) continue;
                if (mesazh.Status)
                {
                    #region Heq llogarite nga grida

                    // ASPxGridView_Artikull.DataSource = dt;
                    hiqPikeNgaGrida(oNjesiAdm.IdPikeShitjeFurnizimi);
                    #endregion
                    TeFshire.Add(oNjesiAdm.Kodi);

                    ASPxPageControl1.ActiveTabIndex = 0;
                    hfStatusi.Value = "true";
                }

            }
            dbRegjistrim.Dispose();
            string mesazhInfoGabim = "", mesazhInfoSukses = "";
            if (TePaFshire.Count == 1)
                mesazhInfoGabim = String.Format("{0}{1}{2}", rm.GetString("msgPikeShitjeFurnPrefixNjejes", ci), String.Join(";", TePaFshire), rm.GetString("msgShtoLlogariSuffixNjejesGabimi", ci));
            else
                if (TePaFshire.Count > 1)
                    mesazhInfoGabim = String.Format("{0}{1}{2}", rm.GetString("msgPikeShitjeFurnPrefixShumes", ci), String.Join(";", TePaFshire), rm.GetString("msgShtoArtikullSuffixShumesGabimi", ci));
            if (TeFshire.Count == 1)
                mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("msgPikeShitjeFurnPrefixNjejes", ci), String.Join(";", TeFshire), rm.GetString("regjMagSuffixMesazhNjejesSuksesi", ci));
            else
                if (TeFshire.Count > 1)
                    mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("msgPikeShitjeFurnPrefixShumes", ci), String.Join(";", TeFshire), rm.GetString("msgCeljeMagazinatSuffixShumesSuksesi", ci));
            if (mesazhInfoGabim != "" && mesazhInfoSukses != "")
                mesazhInfoGabim += rm.GetString("msgLidhesMesazhi", ci) + mesazhInfoSukses;
            if (mesazhInfoGabim != "")
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhInfoGabim, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazhInfoSukses, pnlMesazhi);

        }
        private void hiqPikeNgaGrida(int idpike)
        {
            if (this.gvPikeShitjeFurnizimi.DataSource != null)
            {
                DataTable dt = (DataTable)gvPikeShitjeFurnizimi.DataSource;
                DataRow[] drs = dt.Select("IdPikeShitjeFurnizimi = " + idpike);
                if (drs.Length > 1)
                    throw new Exception("GABIM: Ndodhen 2 pika shitje/furnizimi me te njejten id ne gride");
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                dt.Rows.Remove(dr);
                gvPikeShitjeFurnizimi.DataBind();
            }
            else mbushGridPikeshNgaDB();
        }
        private void shtoPikeNeGrid(int idNdermarrje, int idPerdorues, int idpike)
        {
            if (gvPikeShitjeFurnizimi.DataSource != null)
            {
                DataTable dt = (DataTable)gvPikeShitjeFurnizimi.DataSource;
                DataRow[] drs = dt.Select("IdPikeShitjeFurnizimi = " + idpike);
                if (drs.Length > 0)
                    throw new Exception("GABIM: Pika e shitjes/furnizimit ekziston ne gride");
                DataRow newArtDr = DbCore.DbRegjistrim.colPikaShitjeFurnizimi.merrSipasPikeNdermarrjesDR(idNdermarrje, idpike);
                dt.ImportRow(newArtDr);
            }
            else
                mbushGridPikeshNgaDB();
            //konfiguroGride(cmbKonfigurimi.Text.Split(';')[0], 524);
        }
        private void modifikoPikeNeGrid(int idNdermarrje, int idPerdorues, int idpike)
        {
            if (gvPikeShitjeFurnizimi.DataSource != null)
            {
                DataTable dt = (DataTable)gvPikeShitjeFurnizimi.DataSource;
                DataRow[] drs = dt.Select("IdPikeShitjeFurnizimi = " + idpike);
                if (drs.Length > 1)
                    throw new Exception("GABIM: Ndodhen 2 pika shitje/furnizimi me te njejten id ne gride");
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                DataRow newArtDr = DbCore.DbRegjistrim.colPikaShitjeFurnizimi.merrSipasPikeNdermarrjesDR(idNdermarrje, idpike);
                //  dt.Rows.Remove(dr);
                // dt.ImportRow(newArtDr);
                object[] arr = newArtDr.ItemArray;
                dr.ItemArray = arr;
            }
            else mbushGridPikeshNgaDB();
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
                Page.Validate("entries"); ruajPikeShitjeFurnizimi();
            }
        }
        protected void gvPikeShitjeFurnizimi_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if (e.Column.FieldName == "IdDegeAdministrative" || e.Column.FieldName == "IdDegeAdministrative")
            {
                if (Converter.ConvertToInt(e.Value) == -3 || Converter.ConvertToInt(e.Value) == -1
                    || Converter.ConvertToInt(e.Value)==0)
                {
                    e.Criteria = null;
                }
            }
        }

        protected void gvPikeShitjeFurnizimi_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName == "Aktiv")
            {
                (e.Editor as ASPxComboBox).Items.Clear();
                (e.Editor as ASPxComboBox).Items.Add("");
                (e.Editor as ASPxComboBox).Items.Add("Po", true);
                (e.Editor as ASPxComboBox).Items.Add("Jo", false);
            }
        }
        protected void gvPikeShitjeFurnizimi_CustomJSProperties(object sender, DevExpress.Web.ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gvPikeShitjeFurnizimi.PageIndex;
            e.Properties["cpPageRow"] = gvPikeShitjeFurnizimi.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gvPikeShitjeFurnizimi.VisibleRowCount;
        }
        private void inicializoObjekte()
        {
            //dbRegjistrime = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
        }

        private void konfiguroVleraFillestare(int idNdermarrje, ResourceManager rm, CultureInfo ci, int idGjuha)
        { //mbush komboboxet dhe gridat e faqes  

            inicializoObjekte();
            //   mbushListePikaShitjeFurnizimi();
            ConfigureAspxComboBox.KonfiguroComboBoxDegeAdministrative(idNdermarrje, cmbDegeAdministrative, false);
            ConfigureAspxComboBox.percaktoTemplateComboMeEnableCallback(btneCaktoNeHarte);
            //  funk.percaktoTemplateComboMeEnableCallback(cmbDegeAdministrative);
            if (Request.QueryString["sf"] == "shitje")
            {
                mbushComboKonfigurimeshSipasKategorise(cmbKonfigurimi, 31, "PSH", rm, ci, idGjuha, idNdermarrje);
            }
            else mbushComboKonfigurimeshSipasKategorise(cmbKonfigurimi, 31, "PF", rm, ci, idGjuha, idNdermarrje);
            cmbKonfigurimi.SelectedIndex = 0;
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();

            konf.IdKonfigAmbjente = int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString());
            konf.mbushKonfigAmbjSipasId(konf.IdKonfigAmbjente, idgjuha);
            //konf = new DbCore.DbShare.clsDatabaseShare().merrKonfigAmbjSipasId(konf)[0];
            hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;
            //cmbKonfigurimi.SelectedIndex = -1;
        }
        public void mbushComboKonfigurimeshSipasKategorise(ASPxComboBox combo, int kat, string nivel, ResourceManager rm, CultureInfo ci, int idGjuha, int idNdermarrje)
        {//mbush griden e popupit me te dhena
            //DbCore.DbShare.clsDatabaseShare dbShare = new DbCore.DbShare.clsDatabaseShare();
            //dbShare = new DbCore.DbShare.clsDatabaseShare();
            DbCore.DbShare.colKonfigurimAmbjenti col = new DbCore.DbShare.colKonfigurimAmbjenti();
            //if (Request.QueryString.ToString() == "")
            //{
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            if (kat != 0)
            {
                konf.IdKategori = kat;
                konf.IdNdermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                //DbCore.DbRegjistrim.clsNivelRegjistrimi niv = new DbCore.DbRegjistrim.clsNivelRegjistrimi();
                //niv.mbushNivelRegjistrimiSipasKodiMeKonvertime(nivel, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                int idNiveli = DbCore.DbRegjistrim.clsNivelRegjistrimi.ktheIdNivelRegjistrimiSipasKodi(nivel, idNdermarrje);
                col.mbushKonfigAmbjSipasIdKategoriIdNivelMeLloj(konf.IdKategori, idNiveli, DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
                //col = dbShare.merrKonfigAmbjSipasIdKategori(konf, DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            }
            else
            {
                col.mbushGjitheKonfigurimeAmbjentesh(idNdermarrje, DbCore.mySessionObjects.ktheIdPerdoruesi(Session), 1, idGjuha);    //celje
                //col = dbShare.merrGjitheKonfigurimeAmbjentesh(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            }
            ListBoxColumn colprove = new ListBoxColumn();
            colprove.FieldName = "KodKonfigAmbjente";
            colprove.Caption = rm.GetString("cmbCmimeArtikulliCaptionKodi", ci);
            ListBoxColumn colemer = new ListBoxColumn();
            colemer.FieldName = "PershkrimKonfigAmbjente";
            colemer.Caption = rm.GetString("cmbCmimeArtikulliCaptionPershkrimi", ci);
            combo.TextFormatString = "{0};{1}";
            combo.Columns.Add(colprove);
            combo.Columns.Add(colemer);
            combo.DataSource = col;
            combo.ValueField = "IdKonfigAmbjente";
            // combo.TextField = "KodKonfigAmbjente";
            combo.IncrementalFilteringMode = DevExpress.Web.IncrementalFilteringMode.Contains;
            combo.DataBind();
        }
        private void mbushListePikaShitjeFurnizimi()
        {//mbush griden me te dhena
            //dbRegjistrime = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
            //oPerdorues = (DbCore.DbAdmin.clsPerdorues)(CacheLayer.GlobalCacheManager.MySessionCache["oClsPerdoruesi"]);
            oPerdorues = DbCore.mySessionObjects.kthePerdorues(Session);
            DbCore.DbRegjistrim.colPikaShitjeFurnizimi col = new DbCore.DbRegjistrim.colPikaShitjeFurnizimi();
            col.mbushGjithePikeShitjeFurnizimi(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            gvPikeShitjeFurnizimi.DataSource = col;
            gvPikeShitjeFurnizimi.DataBind();
        }

        private void percaktoTamplateAutorizime()
        {//templatet per kolonat e Autorizimeve

            GridViewDataColumn col = gvPikeShitjeFurnizimi.Columns["Aktiv"] as GridViewDataColumn;
            col.DataItemTemplate = new MyCheckTemplate(true, false);
        }

        private void mbushGridPikeshNgaSession()
        {
            DataTable tmpObject;
            bool sukses = DbCore.mySessionObjects.merrGrideNgaSessioni(Session, out tmpObject);
            if (!sukses)
                mbushGridPikeshNgaDB();
            else
            {
                gvPikeShitjeFurnizimi.DataSource = tmpObject;
                gvPikeShitjeFurnizimi.DataBind();
                tmpObject.Dispose();
            }
        }

        private void mbushGridPikeshNgaDB()
        {//mbush griden e popupit me te dhena            
            DataTable dt = DbCore.DbRegjistrim.colPikaShitjeFurnizimi.merrSipasPikeNdermarrjesDT(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            DbCore.mySessionObjects.ruajGrideNeSession(Session, dt);
            gvPikeShitjeFurnizimi.DataSource = dt;
            gvPikeShitjeFurnizimi.DataBind();
            dt.Dispose();
        }

        private void ruajPikeShitjeFurnizimi()
        {
            //dbRegjistrime = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
            njesia = new DbCore.DbRegjistrim.clsPikeShitjeFurnizimi();
            if (Page.IsValid == false)
                return;
            else
            {
                bool eshteShtim;
                if (isValidPikeShitjeFurnizimi())
                {
                    if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim") eshteShtim = true;
                    else eshteShtim = false;
                    try
                    {
                        njesia = krijoPikeShitjeFurnizimi(eshteShtim);
                    }
                    catch (Exception e)
                    {
                        NLog.LogManager.GetCurrentClassLogger().Error(e.Message);
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, e.Message, pnlMesazhi);
                        hfStatusi.Value = "false";
                        return;
                    }
                    DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                    DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

                    tedrejtaInfo.merrTeDrejtaPerKeteKomponente(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.clsFunksione.GetKomponente(Page.Request));

                    if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim")
                    {
                        if (!tedrejtaInfo.DShtim)
                        {
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                            hfStatusi.Value = "false";
                            return;
                        }
                        mesazh = njesia.ruaj(hfNrAutoKF);
                        eshteShtim = true;
                    }
                    else
                    {
                        if (!tedrejtaInfo.DMod)
                        {
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                            hfStatusi.Value = "false";
                            return;
                        }
                        DbCore.DbRegjistrim.clsDatabaseRegjistrim dbRegjistrim = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
                        DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                        konf.IdKonfigAmbjente = njesia.IdKonfig;
                        konf.mbushKonfigAmbjSipasId(konf.IdKonfigAmbjente);
                        //konf = new DbCore.DbShare.clsDatabaseShare().merrKonfigAmbjSipasId(konf)[0];
                        eshteShtim = false;
                        njesia.IdPikeShitjeFurnizimi = int.Parse(hfId.Value.ToString());
                        bool lidhur = dbRegjistrim.eshteDokumentiILidhurCelje(njesia.IdPikeShitjeFurnizimi.ToString(), konf.IdNivel.ToString());
                        if (lidhur.ToString() != hfLidhur.Value)
                        {
                            mesazh.Status = false;
                            mesazh.PershkrimMesazhi = "Pika e shitjes/furnizimit  eshte e lidhur";
                        }
                        else
                            mesazh = njesia.modifiko();
                        dbRegjistrim.Dispose();
                    }
                    if (mesazh.Status == true)
                    {
                        clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                        hfStatusi.Value = "true";
                        if (eshteShtim)
                            shtoPikeNeGrid(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), njesia.IdPikeShitjeFurnizimi);
                        else //modifikim
                            modifikoPikeNeGrid(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), njesia.IdPikeShitjeFurnizimi);
                    }
                    else
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ndodhi nje gabim. Ruajtja nuk u krye!", pnlMesazhi);
                        hfStatusi.Value = "false";
                    }
                    ASPxPageControl1.ActiveTabIndex = 0;

                    //  mbushListePikaShitjeFurnizimi();
                }
                else
                {
                    // mbushListePikaShitjeFurnizimi();
                }
            }
        }

        /// <summary>
        /// krijon magazinen qe do te ruhet
        /// </summary>
        /// <returns>kthen clsNjesiAdministrative me magazinen qe do te ruhet</returns>
        private DbCore.DbRegjistrim.clsPikeShitjeFurnizimi krijoPikeShitjeFurnizimi(bool shtim)
        {//krijon nje llogari sipas te dhenave te futura nga perdoruesi
            var controls = this.GetAsPxTextEditIdValue();
            controls.AddRange(ASPxPageControl1.GetAsPxTextEditIdValue());

            hfNrAuto = (ASPxHiddenField)DbCore.DbAdmin.NrAuto.VendosVleratNrAuto(hfNrAuto, controls);
            //DbCore.DbAdmin.NrAuto.vendosVleratNrAuto(hfNrAuto, this, this.ASPxPageControl1, null, null);

            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DbCore.DbShare.clsKonfigurimAmbjenti konfig = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konfig.mbushKonfigAmbjSipasKod(cmbKonfigurimi.Text, idNdermarrje);
            bool shitjefur = false;
            if (Request.QueryString["sf"] == "shitje")
                shitjefur = true;
            int degeadm = 0;
            if (cmbDegeAdministrative.Text != "")
                degeadm = int.Parse(cmbDegeAdministrative.Value.ToString());

            hfNrAutoKF = (ASPxHiddenField)DbCore.DbAdmin.NrAuto.ShtoNeRegjistrime(hfNrAutoKF, hfNrAuto, "txtKodi", "Kodi");
            //DbCore.DbAdmin.NrAuto.shtoNeHfRegjistrime(hfNrAutoKF, hfNrAuto, "txtKodi", "Kodi");

            string koordinata = "";
            if (btneCaktoNeHarte.Text != "" && hfState.Contains("geom"))
            {
                string geomsToDeserialize = Convert.ToString(hfState.Get("geom"));
                if (!String.IsNullOrEmpty(geomsToDeserialize))
                {
                    System.Web.Script.Serialization.JavaScriptSerializer serializusi = new System.Web.Script.Serialization.JavaScriptSerializer();
                    object[] geoms = (object[])serializusi.DeserializeObject(geomsToDeserialize);
                    koordinata = Convert.ToString(geoms[0]);
                }
            }
            DbCore.DbRegjistrim.clsPikeShitjeFurnizimi njesi = new DbCore.DbRegjistrim.clsPikeShitjeFurnizimi(0, DbCore.clsFunksione.ktheStringunPaHapesira(txtKodi.Text, true), DbCore.clsFunksione.ktheStringunPaHapesira(txtPershkrimi.Text, false), txtAdresa.Text, cbAktiv.Checked, idNdermarrje, DbCore.mySessionObjects.ktheIdPerdoruesi(Session), dteDtRegjistrimi.Date, konfig.IdKonfigAmbjente, shitjefur, degeadm, cmbDegeAdministrative.Text, shtim, koordinata, rm, ci);
            return njesi;
        }


        private bool isValidPikeShitjeFurnizimi()
        {
            bool isValid;
            isValid = true;

            if (njesia == null)
            {
                isValid = false;
            }
            else
            {
                //DbCore.DbRegjistrim.clsDatabaseRegjistrim dbRegjistrime = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
                //if (dbRegjistrime.ekzistonKodPikeShitjeFurnizim(txtKodi.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session)) && (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim"))
                //{
                //    isValid = false;
                //    hfStatusi.Value = "false";
                //    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ekziston nje pike shitje/furnizimi me kete kod! Ju lutemi zgjidhni nje kod tjeter.", pnlMesazhi);
                //    return isValid;
                //}
                //dbRegjistrime.Dispose();
            }
            if (this.cmbDegeAdministrative.Text != "")
            {
                DbCore.DbRegjistrim.clsDegeAdministrative dege = new DbCore.DbRegjistrim.clsDegeAdministrative(cmbDegeAdministrative.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                if (dege.IdDegeAdministrative == -1)
                {
                    isValid = false; clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Kjo dege administrative nuk ekziston!", pnlMesazhi);
                    hfStatusi.Value = "false";
                    return isValid;
                }
                else
                {
                    dege = new DbCore.DbRegjistrim.clsDegeAdministrative(cmbDegeAdministrative.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));

                    if (dege.Aktiv == false)
                    {
                        isValid = false; clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Kjo dege administrative nuk eshte aktive!", pnlMesazhi);
                        hfStatusi.Value = "false";
                        return isValid;
                    }
                }
            }
            return isValid;
        }

        /// <summary>
        /// perdoret per te rikonfiguruar griden kur ndryshohet konfigurimi
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvPikeShitjeFurnizimi_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string idkomponente = "";
            string kodkonfigurimi = "";

            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                kodkonfigurimi = arr[1];
                idkomponente = arr[0];
                if (arr[2] == "")
                    if (Request.QueryString["sf"] == "shitje")
                    {
                        gvPikeShitjeFurnizimi.FilterExpression = "[ShitjeFurnizimi]=true";
                    }
                    else
                    {
                        gvPikeShitjeFurnizimi.FilterExpression = "[ShitjeFurnizimi]=false";
                    }
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvPikeShitjeFurnizimi", komponente, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), int.Parse(cmbKonfigurimi.Value.ToString()));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        gvPikeShitjeFurnizimi.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvPikeShitjeFurnizimi);
                        hfStatusi.Value = "true";
                    }
                    else hfStatusi.Value = "false";
                }
            }
            else if (arr.Length == 2)//nese eshte zgjedhur nje konfigurim tek combo e konfigurimeve
            {
                kodkonfigurimi = arr[1];
                idkomponente = arr[0];
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "gvPikeShitjeFurnizimi", gvPikeShitjeFurnizimi, kodkonfigurimi, idkomponente.ToString(), (int)hfState["idGjuha"]);
                gvPikeShitjeFurnizimi.Columns["#"].VisibleIndex = 0;
            }
            else
            {
                idkomponente = e.Parameters;
            }
            gvPikeShitjeFurnizimi.Selection.UnselectAll();
        }
    }

}

