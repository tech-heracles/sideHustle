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
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class Shto_DegeAdministrative : MyPageBase
    {
        private int idgjuha, idviti, idNdermarrje, idPerdoruesi;

        //private DbCore.DbRegjistrim.clsDatabaseRegjistrim dbRegjistrime;
        public static bool isShtim = true;
        //DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();

        DbCore.DbKontabiliteti.clsLlogari oLlog = new DbCore.DbKontabiliteti.clsLlogari();
        public static int id = 0;
        public static DbCore.DbRegjistrim.clsDegeAdministrative njesia;
        protected void Page_Init(object sender, EventArgs e)
        {
            //dbRegjistrime = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
            if (Request.QueryString["id"] != null)
            {
                id = int.Parse(Request.QueryString["id"]);
            }
            else
                id = 0;

            if (id != 0)
            {
                njesia = new DbCore.DbRegjistrim.clsDegeAdministrative(id);

            }
            else
                njesia = new DbCore.DbRegjistrim.clsDegeAdministrative();
            //mbushListeDegeAdministrative();
            //konfiguroGride();
        }

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
            percaktoTemplateMenu(idgjuha, idviti, idPerdoruesi, idNdermarrje, ASPxMenu1);
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (!Page.IsPostBack)
            {
                vendosHfMePerkthime(rm, cultinf);
                EmrateTabeve(rm, cultinf);
                hfState.Set("idGjuha", idgjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                ASPxPageControl1.ActiveTabIndex = 0;
                konfiguroVleraFillestare(idPerdoruesi, idNdermarrje, rm, cultinf, idgjuha);
                if (id != 0)
                {
                    njesia = new DbCore.DbRegjistrim.clsDegeAdministrative(id);

                }
                else
                    njesia = new DbCore.DbRegjistrim.clsDegeAdministrative();
                mbushGridDegeshNgaDB();
                konfiguroGride(cmbKonfigurimi.Text.Split(';')[0], 163);
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), "gvDegeAdministrative", gvDegeAdministrative, cmbKonfigurimi.Text.Split(';')[0], "163", (int)hfState["idGjuha"]);

                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "Shto_DegeAdministrative.aspx");
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                gvDegeAdministrative.Columns["#"].VisibleIndex = 0;

            }
            else
            {
                mbushGridDegeshNgaSession();
                konfiguroGride(cmbKonfigurimi.Text.Split(';')[0], 163);
            } 
            GridUtil.konfigGrideListeEMadhePaTheme(gvDegeAdministrative, "IdDegeAdministrative");
            AspxWebControlUtils.perkthePopUp(popFshi, rm.GetString("labelKujdes", cultinf), lblMsgbox, rm.GetString("labelAdministrimiMsgJeniSigurt", cultinf), ButtonCancel, rm.GetString("labelAnullo", cultinf));
            GridUtil.EmrateButonaveMbiGride(gvDegeAdministrative);

            //  konfiguroGride();
            //mbushComboBoxFiltra();
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvDegeAdministrative", int.Parse(cmbKonfigurimi.Value.ToString()), "Shto_DegeAdministrative.aspx");
            //clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "", pnlMesazhi);
        }

        private void vendosHfMePerkthime(ResourceManager rm, CultureInfo cultinf)
        {
            hfState.Set("headerPopUpZgjidhQendrenKostos", rm.GetString("headerPopUpZgjidhQendrenKostos", cultinf));
            hfState.Set("headerPopUpZgjidhSkemenKostos", rm.GetString("headerPopUpZgjidhSkemenKostos", cultinf));
            hfState.Set("msgDegetAdministrativeZgjidhDegen", rm.GetString("msgDegetAdministrativeZgjidhDegen", cultinf));
        }

        /// <summary>
        /// Vendos emrat e tabeve ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateTabeve(ResourceManager rm, CultureInfo cultinf)
        {
            ASPxPageControl1.TabPages[0].Text = rm.GetString("labelAdministrimiTePergjithshme", cultinf);
            ASPxPageControl1.TabPages[1].Text = rm.GetString("labelFilterAvancuarDegeAdministrative", cultinf);
            konfigurimi_Label.Text = rm.GetString("lblModeli", cultinf);
        }


        ///// <summary>
        /////      mbush combon e filtrave
        ///// </summary>
        //private void mbushComboBoxFiltra()
        //{
        //    //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
        //    //DbCore.DbAdmin.clsGridaKoka koka = dbAdmin.merrGridaKokaByEmri("gvDegeAdministrative", "Shto_NjesiAdministrative.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka("gvDegeAdministrative", "Shto_DegeAdministrative.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //    DbCore.DbAdmin.colFiltratGrida colFiltra = new DbCore.DbAdmin.colFiltratGrida(koka.IdGridaKoka, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //    colFiltra.Insert(0, new DbCore.DbAdmin.clsFiltraGrida());  //colFiltra = dbAdmin.merrGjitheFiltratGridaByGridaKoka(koka.IdGridaKoka, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
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
            clsToolbarConfig.percaktoTemplateMenu(idGjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1, "Shto_DegeAdministrative.aspx", this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, hfShtimModifikim.Value == "modifikim" ? false : true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
        }

        protected void RuajKolona_Click(object sender, EventArgs e)
        {
            int idfiltri = 0;
            int idkonf = DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(cmbKonfigurimi.Text.Split(';')[0], idNdermarrje);
            DbCore.clsMesazh mesazh = GridUtil.ruajFiltra(idNdermarrje, idPerdoruesi, idgjuha, "gvDegeAdministrative ", "Shto_DegeAdministrative.aspx", "FilterDefault", gvDegeAdministrative.FilterExpression, gvDegeAdministrative, "Kodi", idkonf, out idfiltri);//ruan filtrin e zgjedhur tek filtrat
            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                return;

            }
            mesazh = GridUtil.ruajkonfigurimgride(gvDegeAdministrative, cmbKonfigurimi.Text, idNdermarrje, idPerdoruesi, 163, idfiltri, idviti, DbCore.mySessionObjects.ktheCultureInfo(Session), DbCore.mySessionObjects.ktheGjuhe(Session));/// ruan konfigurimin e grides dhe filtrin

            clsToolbarConfig.mbushComboBoxFiltra(idgjuha, idNdermarrje, "gvDegeAdministrative ", int.Parse(cmbKonfigurimi.Value.ToString()), "Shto_DegeAdministrative.aspx");

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
            percaktoTemplateMenu(idgjuha, idviti, idPerdoruesi, idNdermarrje, ASPxMenu1);

        }
        protected void gvDegeAdministrative_DataBound(object sender, EventArgs e)
        {// shton colonen # per selektim dhe disa karakteristika te grides
            if (this.gvDegeAdministrative.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per 
                //perzgjidh
                DevExpress.Web.GridViewCommandColumn check = new DevExpress.Web.GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true;
                check.Width = Unit.Percentage(2);
                //  check.SetColVisibleIndex(0);
                //behet per te afishuar rreshtin qe do sherbej per filtrim
                gvDegeAdministrative.Settings.ShowFilterRow = true;
                gvDegeAdministrative.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                gvDegeAdministrative.Settings.ShowFilterRowMenu = true;
                gvDegeAdministrative.Columns.Add(check);
                gvDegeAdministrative.KeyFieldName = "IdDegeAdministrative";
                gvDegeAdministrative.SettingsBehavior.AllowSelectByRowClick = true;
                gvDegeAdministrative.SettingsBehavior.AllowFocusedRow = true;
            }
            //  this.gvDegeAdministrative.Columns["#"].VisibleIndex = 0;
        }
        private void konfiguroGride(string kodKonfigurimi, int idKomponente)
        {//konfiguron griden

            percaktoTamplateAutorizime();
            //dbRegjistrime = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();

            this.gvDegeAdministrative.Columns["#"].VisibleIndex = 0;
            // funksione.percaktoVisibleColumnsGridSipasKodKonfigurimi(gvDegeAdministrative, cmbKonfigurimi.Text.Split(';')[0], "163");
            //funksione.konfiguroGrideListeMadhe(gvDegeAdministrative, "IdDegeAdministrative");
            //  mbushListeDegeAdministrative();
        }

        //thirret sa here qe behet Callback, dhe ben edhe njeher lidhjen me datasource-in
        protected void gvDegeAdministrative_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            if (e.CallbackName == "COLUMNMOVE" && gvDegeAdministrative.AllColumns[int.Parse(e.Args[0])].Width.Value == 0)
                gvDegeAdministrative.AllColumns[int.Parse(e.Args[0])].Width = Unit.Percentage(3);

            if (e.CallbackName == "APPLYFILTER" && e.Args[0] == "")
            {
                DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
                ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
                cmbFiltra.Text = "";
            }
            percaktoTamplateAutorizime();            
            GridUtil.EmrateButonaveMbiGride(gvDegeAdministrative);
        }

        //bere  me e konfigurueshme
        protected void gvDegeAdministrative_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            string TeGjithe = rm.GetString("GridHeaderFilterFillItemTeGjithe", ci);
            string nga = rm.GetString("GridHeaderFilterFillItemNga", ci);
            if (e.Column.FieldName == "Kodi" || e.Column.FieldName == "Pershkrimi")
            {
                e.Values.Clear();
                //e.AddShowAll();
                e.AddValue(TeGjithe, string.Empty, "true");
                e.AddValue(nga +" A-D ", string.Empty, e.Column.FieldName + ">'A     ' and " + e.Column.FieldName + " <'DDDDDDD'");
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
            //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
            //filtra.mbushFiltraGridaSipasFiltraEmri(cmbFiltra.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvDegeAdministrative", "Shto_DegeAdministrative.aspx", idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            //DbCore.DbAdmin.clsFiltraGrida filtra = dbAdmin.merrFiltraGridaSipasFiltraEmri(cmbFiltra.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();
                //mbushComboBoxFiltra();
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvDegeAdministrative", int.Parse(cmbKonfigurimi.Value.ToString()), "Shto_DegeAdministrative.aspx");
                percaktoTemplateMenu(idgjuha, idviti, idPerdoruesi, idNdermarrje, ASPxMenu1);

                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                //konfiguroVleraFillestare();
                hfStatusi.Value = "true";
                gvDegeAdministrative.FilterExpression = String.Empty;
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
            //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            DbCore.DbAdmin.clsFiltraGrida filtri = new DbCore.DbAdmin.clsFiltraGrida();
            filtri.FiltraKodi = cmbFiltra.Text;
            filtri.FiltraShenime = cmbFiltra.Text;
            filtri.FiltraUniversal = false;// Universal_ASPxCheckBox.Checked;
            //DbCore.DbAdmin.clsGridaKoka koka = dbAdmin.merrGridaKokaByEmri("gvDegeAdministrative", "Shto_NjesiAdministrative.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvDegeAdministrative", "Shto_DegeAdministrative.aspx", idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gvDegeAdministrative.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("Kodi", gvDegeAdministrative);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvDegeAdministrative.GetSortedColumns();
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
            //DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();
            //oPerdorues = (DbCore.DbAdmin.clsPerdorues)(CacheLayer.GlobalCacheManager.MySessionCache["oClsPerdoruesi"]);
            filtri.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            filtri.IdNdermarje = idNdermarrje;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            filtri.IdStatusDok = 1;

            mesazh = filtri.ruaj();
            //mbushComboBoxFiltra();
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvDegeAdministrative", int.Parse(cmbKonfigurimi.Value.ToString()), "Shto_DegeAdministrative.aspx");
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
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            List<object> rreshtat;
            if (ASPxPageControl1.ActiveTabIndex == 0)//nqs kemi zgjedhur vetem nje kartele per te pare fshijme vetem ate
                rreshtat = gvDegeAdministrative.GetSelectedFieldValues("IdDegeAdministrative");
            else
            {
                rreshtat = new List<object>();
                rreshtat.Add(hfId.Value);
            }
            //List<object> rreshtat = gvDegeAdministrative.GetSelectedFieldValues("IdDegeAdministrative");
            if (rreshtat.Count == 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgDegetAdministrativeZgjidhniNjeDokument"), pnlMesazhi);
                return;
            }
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            //clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "", pnlMesazhi);
            List<string> TeFshire = new List<string>(), TePaFshire = new List<string>();
            DbCore.DbRegjistrim.clsDatabaseRegjistrim dbRegjistrim = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();

            foreach (object id in rreshtat)
            {
                DbCore.DbRegjistrim.clsDegeAdministrative oNjesiAdm = new DbCore.DbRegjistrim.clsDegeAdministrative(Convert.ToInt32(id));
                DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                konf.IdKonfigAmbjente = oNjesiAdm.IdKonfig;
                konf.mbushKonfigAmbjSipasId(konf.IdKonfigAmbjente);
                //konf = new DbCore.DbShare.clsDatabaseShare().merrKonfigAmbjSipasId(konf)[0];

                bool lidhur = dbRegjistrim.eshteDokumentiILidhurCelje(oNjesiAdm.IdDegeAdministrative.ToString(), konf.IdNivel.ToString());
                if (lidhur)
                {
                    TePaFshire.Add(oNjesiAdm.Kodi);
                    continue;
                }
                oNjesiAdm.IdPerdorues = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                mesazh = oNjesiAdm.fshi();
                if (oNjesiAdm.IdDegeAdministrative == 0)
                    continue;
                if (mesazh.Status)
                {
                    #region Heq llogarite nga grida

                    // ASPxGridView_Artikull.DataSource = dt;
                    hiqDegeNgaGrida(oNjesiAdm.IdDegeAdministrative, rm, ci);
                    #endregion
                    TeFshire.Add(oNjesiAdm.Kodi);

                    ASPxPageControl1.ActiveTabIndex = 0;
                    hfStatusi.Value = "true";
                }

            }
            dbRegjistrim.Dispose();
            string mesazhInfoGabim = "", mesazhInfoSukses = "";
            if (TePaFshire.Count == 1)
                mesazhInfoGabim = String.Format("{0}{1}{2}", rm.GetString("msgDegetAdministrativePrefixNjejes", ci), String.Join(";", TePaFshire), rm.GetString("msgDegetAdministrativeSuffixNjejesGabimi", ci));
            else
                if (TePaFshire.Count > 1)
                    mesazhInfoGabim = String.Format("{0}{1}{2}", rm.GetString("msgDegetAdministrativePrefixShumes", ci), String.Join(";", TePaFshire), rm.GetString("msgDegetAdministrativeSuffixShumesGabimi", ci));
            if (TeFshire.Count == 1)
                mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("msgDegetAdministrativePrefixNjejes", ci), String.Join(";", TeFshire), rm.GetString("msgDegetAdministrativeSuffixNjejesSuksesi", ci));
            else
                if (TeFshire.Count > 1)
                    mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("msgDegetAdministrativePrefixShumes", ci), String.Join(";", TeFshire), rm.GetString("msgDegetAdministrativeSuffixShumesSuksesi", ci));
            if (mesazhInfoGabim != "" && mesazhInfoSukses != "")
                mesazhInfoGabim += rm.GetString("msgLidhesMesazhi", ci) + mesazhInfoSukses;
            if (mesazhInfoGabim != "")
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhInfoGabim, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazhInfoSukses, pnlMesazhi);
        }
        private void hiqDegeNgaGrida(int iddege, ResourceManager rm, CultureInfo ci)
        {
            if (this.gvDegeAdministrative.DataSource != null)
            {
                DataTable dt = (DataTable)gvDegeAdministrative.DataSource;
                DataRow[] drs = dt.Select("IdDegeAdministrative = " + iddege);
                if (drs.Length > 1)
                    throw new DbCore.MyException(rm.GetString("msgDegetAdministrativeIDDegeNjejte", ci));
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                dt.Rows.Remove(dr);
                gvDegeAdministrative.DataBind();
            }
            else mbushGridDegeshNgaDB();
        }
        private void shtoDegeNeGrid(int idNdermarrje, int idPerdorues, int iddege, ResourceManager rm, CultureInfo ci)
        {
            if (gvDegeAdministrative.DataSource != null)
            {
                DataTable dt = (DataTable)gvDegeAdministrative.DataSource;
                DataRow[] drs = dt.Select("IdDegeAdministrative = " + iddege);
                if (drs.Length > 0)
                    throw new DbCore.MyException(rm.GetString("msgDegetAdministrativeEkzistonDegaNeGride", ci));
                DataRow newArtDr = DbCore.DbRegjistrim.colDegeAdministrative.merrSipasDegeNdermarrjesDR(iddege);
                dt.ImportRow(newArtDr);
            }
            else mbushGridDegeshNgaDB();
        }
        private void modifikoDegeNeGrid(int idNdermarrje, int idPerdorues, int iddege, ResourceManager rm, CultureInfo ci)
        {
            if (gvDegeAdministrative.DataSource != null)
            {
                DataTable dt = (DataTable)gvDegeAdministrative.DataSource;
                DataRow[] drs = dt.Select("IdDegeAdministrative = " + iddege);
                if (drs.Length > 1)
                    throw new DbCore.MyException(rm.GetString("msgDegetAdministrativeIDDegeNjejte", ci));
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                DataRow newArtDr = DbCore.DbRegjistrim.colDegeAdministrative.merrSipasDegeNdermarrjesDR(iddege);
                //  dt.Rows.Remove(dr);
                // dt.ImportRow(newArtDr);
                object[] arr = newArtDr.ItemArray;
                dr.ItemArray = arr;
            }
            else mbushGridDegeshNgaDB();
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
                Page.Validate("entries");      ruajDegeAdministrative();
            }
        }
        protected void gvDegeAdministrative_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {

        }

        protected void gvDegeAdministrative_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName == "Aktiv")
            {
                ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
                (e.Editor as ASPxComboBox).Items.Clear();
                (e.Editor as ASPxComboBox).Items.Add("");
                (e.Editor as ASPxComboBox).Items.Add(rm.GetString("cmbFilterPo", ci), true);
                (e.Editor as ASPxComboBox).Items.Add(rm.GetString("cmbFilterJo", ci), false);
            }
        }
        protected void gvDegeAdministrative_CustomJSProperties(object sender, DevExpress.Web.ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gvDegeAdministrative.PageIndex;
            e.Properties["cpPageRow"] = gvDegeAdministrative.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gvDegeAdministrative.VisibleRowCount;
        }
        private void inicializoObjekte()
        {
            //dbRegjistrime = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
        }

        private void konfiguroVleraFillestare(int idPerdoruesi, int idNdermarrje, System.Resources.ResourceManager rm, System.Globalization.CultureInfo ci, int idGjuha)
        { //mbush komboboxet dhe gridat e faqes  

            inicializoObjekte();
            //  mbushListeDegeAdministrative();
            ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 32, rm, ci, idGjuha);
            cmbKonfigurimi.SelectedIndex = 0;
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            ConfigureAspxComboBox.percaktoTemplateComboMeEnableCallback(qendraKostos_TextBox);
            ConfigureAspxComboBox.mbushComboLlojQendre(cmbLloji, true, rm, ci);
            konf.IdKonfigAmbjente = int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString());
            konf.mbushKonfigAmbjSipasId(konf.IdKonfigAmbjente, idGjuha);
            //konf = new DbCore.DbShare.clsDatabaseShare().merrKonfigAmbjSipasId(konf)[0];
            hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;
            //cmbKonfigurimi.SelectedIndex = -1;
        }
        private void mbushGridDegeshNgaSession()
        {
            DataTable tmpObject;
            bool sukses = DbCore.mySessionObjects.merrGrideNgaSessioni(Session, out tmpObject);
            if (!sukses)
                mbushGridDegeshNgaDB();
            else
            {
                gvDegeAdministrative.DataSource = tmpObject;
                gvDegeAdministrative.DataBind();
                tmpObject.Dispose();
            }
        }
        private void mbushGridDegeshNgaDB()
        {//mbush griden e popupit me te dhena            
            DataTable dt = DbCore.DbRegjistrim.colDegeAdministrative.merrSipasDegeNdermarrjesDT(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            DbCore.mySessionObjects.ruajGrideNeSession(Session, dt);
            gvDegeAdministrative.DataSource = dt;
            gvDegeAdministrative.DataBind();
            dt.Dispose();
        }
        private void mbushListeDegeAdministrative()
        {//mbush griden me te dhena
            //dbRegjistrime = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
            //oPerdorues = (DbCore.DbAdmin.clsPerdorues)(CacheLayer.GlobalCacheManager.MySessionCache["oClsPerdoruesi"]);
            DbCore.DbRegjistrim.colDegeAdministrative col = new DbCore.DbRegjistrim.colDegeAdministrative();
            col.mbushGjitheDegeAdministrative(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            gvDegeAdministrative.DataSource = col;
            gvDegeAdministrative.DataBind();
        }

        private void percaktoTamplateAutorizime()
        {//templatet per kolonat e Autorizimeve

            GridViewDataColumn col = gvDegeAdministrative.Columns["Aktiv"] as GridViewDataColumn;
            col.DataItemTemplate = new MyCheckTemplate(true, false);


        }

        
        private void ruajDegeAdministrative()
        {
            //dbRegjistrime = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
            njesia = new DbCore.DbRegjistrim.clsDegeAdministrative();
            if (!Page.IsValid)
                return;
            try 
            {
                bool eshteShtim;
                int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                int idPerdorues = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                int idVit = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
                ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
                if (isValidNjesiAdministrative(rm, ci))
                {
                    if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim") eshteShtim = true;
                    else eshteShtim = false;
                   
                        njesia = krijoDegeAdministrative(eshteShtim);
                    
                   
                    DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                    DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

                    tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdorues, idNdermarrje, idVit, "Shto_DegeAdministrative.aspx");

                    if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim")
                    {
                        if (!tedrejtaInfo.DShtim)
                        {
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNukKeniTeDrejta", ci), pnlMesazhi);
                            hfStatusi.Value = "false";
                            return;
                        }
                        mesazh = njesia.ruaj();
                        eshteShtim = true;
                    }
                    else
                    {
                        if (!tedrejtaInfo.DMod)
                        {
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNukKeniTeDrejta", ci), pnlMesazhi);
                            hfStatusi.Value = "false";
                            return;
                        }
                        DbCore.DbRegjistrim.clsDatabaseRegjistrim dbRegjistrim = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
                        DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                        konf.IdKonfigAmbjente = njesia.IdKonfig;
                        konf.mbushKonfigAmbjSipasId(konf.IdKonfigAmbjente);
                        //konf = new DbCore.DbShare.clsDatabaseShare().merrKonfigAmbjSipasId(konf)[0];

                        njesia.IdDegeAdministrative = int.Parse(hfId.Value.ToString());
                        bool lidhur = dbRegjistrim.eshteDokumentiILidhurCelje(njesia.IdDegeAdministrative.ToString(), konf.IdNivel.ToString());
                        if (lidhur.ToString() != hfLidhur.Value)
                        {
                            mesazh.Status = false;
                            mesazh.PershkrimMesazhi = rm.GetString("msgDegetAdministrativeDegaELidhur", ci);
                        }
                        else
                            mesazh = njesia.modifiko();
                        eshteShtim = false;
                        dbRegjistrim.Dispose();
                    }
                    if (mesazh.Status == true)
                    {
                        clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("msgNjesiaAdministrativeURuajtMeSukses", ci), pnlMesazhi);
                        if (eshteShtim)
                            shtoDegeNeGrid(idNdermarrje, idPerdorues, njesia.IdDegeAdministrative, rm, ci);
                        else //modifikim
                            modifikoDegeNeGrid(idNdermarrje, idPerdorues, njesia.IdDegeAdministrative, rm, ci);
                        hfStatusi.Value = "true";
                    }
                    else
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgDegetAdministrativeRuajtjeMeGabime", ci), pnlMesazhi);
                        hfStatusi.Value = "false";
                    }
                    ASPxPageControl1.ActiveTabIndex = 0;

            
                }
             
            }
            catch (Exception ex)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, ex.Message, pnlMesazhi);
                hfStatusi.Value = "false";
                return;
            }
        }
        /// <summary>
        /// krijon magazinen qe do te ruhet
        /// </summary>
        /// <returns>kthen clsNjesiAdministrative me magazinen qe do te ruhet</returns>
        private DbCore.DbRegjistrim.clsDegeAdministrative krijoDegeAdministrative(bool shtim)
        {//krijon nje llogari sipas te dhenave te futura nga perdoruesi
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            DbCore.DbShare.clsKonfigurimAmbjenti konfig = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konfig.mbushKonfigAmbjSipasKod(cmbKonfigurimi.Text, idNdermarrje);
            int idqendra, idskema;
            if (this.qendraKostos_TextBox.Text != "")
            {
                if (cmbLloji.Value.ToString() == "1")
                {
                    DbCore.DbQendraKosto.clsQendraKosto obj = new DbCore.DbQendraKosto.clsQendraKosto(qendraKostos_TextBox.Text, idNdermarrje);
                    idqendra = obj.Id;
                    idskema = 0;
                }
                else
                {
                    DbCore.DbQendraKosto.clsKokaSkemaQK obj = new DbCore.DbQendraKosto.clsKokaSkemaQK(qendraKostos_TextBox.Text, idNdermarrje);
                    idskema = obj.IdKoka;
                    idqendra = 0;
                }
            }
            else
            {
                idqendra = 0; idskema = 0;
            }

            DbCore.DbRegjistrim.clsDegeAdministrative njesi = new DbCore.DbRegjistrim.clsDegeAdministrative(0, DbCore.clsFunksione.ktheStringunPaHapesira(txtKodi.Text, true), DbCore.clsFunksione.ktheStringunPaHapesira(txtPershkrimi.Text, false), txtAdresa.Text, cbAktiv.Checked, idNdermarrje, DbCore.mySessionObjects.ktheIdPerdoruesi(Session), dteDtRegjistrimi.Date, konfig.IdKonfigAmbjente, idqendra, idskema, int.Parse(cmbLloji.Value.ToString()), qendraKostos_TextBox.Text, shtim, rm, ci,txtKodBiznesi.Text);

            return njesi;
        }


        private bool isValidNjesiAdministrative(ResourceManager rm, CultureInfo ci)
        {
            bool isValid;
            isValid = true;

            if (njesia == null)
            {
                isValid = false;
            }
            else
            {
                DbCore.DbRegjistrim.clsDatabaseRegjistrim dbRegjistrime = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
                if (dbRegjistrime.ekzistonKodDegeAdministrative(DbCore.clsFunksione.ktheStringunPaHapesira(txtKodi.Text, true), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session)) && (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim"))
                {
                    isValid = false;
                    hfStatusi.Value = "false";
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgDegetAdministrativeEkzistonDega", ci), pnlMesazhi);
                    return isValid;
                }
                dbRegjistrime.Dispose();
            }
            return isValid;
        }
        /// <summary>
        /// perdoret per te rikonfiguruar griden kur ndryshohet konfigurimi
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvDegeAdministrative_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string idkomponente = "";
            string kodkonfigurimi = "";

            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                kodkonfigurimi = arr[1];
                idkomponente = arr[0];
                if (arr[2] == "")
                    gvDegeAdministrative.FilterExpression = "";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                    int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                    int idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
                    //filtra.mbushFiltraGridaSipasFiltraKodi(arr[2], idNdermarrje);
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuha, "gvDegeAdministrative", "Shto_DegeAdministrative.aspx", idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], idNdermarrje, koka.IdGridaKoka);
                    //DbCore.DbAdmin.clsFiltraGrida filtra = dbAdmin.merrFiltraGridaSipasFiltraKodi(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    if (filtra.FiltraKodi != null)
                    {
                        gvDegeAdministrative.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvDegeAdministrative);
                        CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
                        ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                        konfiguroVleraFillestare(idPerdoruesi, idNdermarrje, rm, ci, idGjuha);
                        hfStatusi.Value = "true";
                    }
                    else hfStatusi.Value = "false";
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
            //  konfiguroGride();
            //  funksion.percaktoVisibleColumnsGridSipasKodKonfigurimi(gvDegeAdministrative, kodkonfigurimi, idkomponente);
            gvDegeAdministrative.Selection.UnselectAll();

        }

        protected void qendraKostos_TextBox_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].Contains("qendraKostos_TextBox"))
                {
                    if (cmbLloji.Value.ToString() == "1")
                        ConfigureAspxComboBox.mbushComboQendraKostoBij(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), qendraKostos_TextBox);
                    else ConfigureAspxComboBox.mbushComboSkemaQendraKosto(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), qendraKostos_TextBox);
                }
            }
        }


        protected void qendraKostos_TextBox_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].Contains("qendraKostos_TextBox"))
                {
                    if (string.IsNullOrWhiteSpace(e.Filter)) return;
                    DbCore.DbQendraKosto.colQendraKosto col = new DbCore.DbQendraKosto.colQendraKosto();
                    if (qendraKostos_TextBox.Value != null)
                        col.mbushQendraSipasPrindit(int.Parse(qendraKostos_TextBox.Value.ToString()));
                    else
                        col.mbushGjitheQendraKostoBijSipasNdermarjesAktiv(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    qendraKostos_TextBox.DataSource = col.Where(x => x.Kodi.IndexOf(e.Filter, StringComparison.InvariantCultureIgnoreCase) > -1).Skip(e.BeginIndex).Take(e.EndIndex + 1);

                    qendraKostos_TextBox.TextField = "Kodi";
                    qendraKostos_TextBox.ValueField = "Id";
                    qendraKostos_TextBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                    qendraKostos_TextBox.DataBind();


                }
            }
        }
    }
}

