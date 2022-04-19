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
using System.Globalization;
using System.Resources;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb

{
    public partial class Shto_NjesiVartese : MyPageBase
    {
        private const string prefixMesazhNjejes = "Njesia me Kod: ";
        private const string prefixMesazhShumes = "Njesite me Kod: ";
        private const string suffixMesazhNjejesGabimi = " eshte e lidhur dhe nuk mund te fshihet";
        private const string suffixMesazhShumesGabimi = " jane te lidhur dhe nuk mund te fshihen";
        private const string suffixMesazhNjejesSuksesi = " u fshi me sukses!";
        private const string suffixMesazhShumesSuksesi = " u fshine me sukses!";
        private const string lidhesMesazhi = ". Kurse ";
        private const string mesazhZgjidhniNje = "Ju lutem zgjidhni te pakten nje njesi!";

        private int idndermarje, idperdoruesi, idnderviti, idgjuha, idPerdoruesi, idviti;

        protected void Page_Init(object sender, EventArgs e)
        {


        }
        private string komponente = "Shto_NjesiVartese.aspx";
        private string guidString;

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
            idndermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            idperdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            idnderviti = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
            idviti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            idgjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            percaktoTemplateMenu(idgjuha, idviti, idPerdoruesi, idndermarje, ASPxMenu1);
            if (!Page.IsPostBack)
            {
               
                ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                EmrateTabeve(rm, ci);
                hfState.Set("idGjuha", idgjuha);
                hfState.Set("idndermarje", idndermarje);
                guidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                hfState.Set("guidString", guidString);
                ASPxPageControl1.ActiveTabIndex = 0;
                konfiguroVleraFillestare(idperdoruesi, idndermarje, rm, ci, idgjuha);
                mbushGridNjesishNgaDB();
                konfiguroGride(cmbKonfigurimi.Text.Split(';')[0], 440);
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idndermarje, "gvNjesiVartese", gvNjesiVartese, cmbKonfigurimi.Text.Split(';')[0], 440.ToString(), (int)hfState["idGjuha"]);
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idndermarje, idviti, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idndermarje, idviti, komponente);
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
            }
            else
            {
                guidString = (string)hfState["guidString"];
                mbushGridNjesishNgaSession();
                konfiguroGride(cmbKonfigurimi.Text.Split(';')[0], 440);
            }
            GridUtil.konfigGrideListeEMadhePaTheme(gvNjesiVartese, "IdNjesiVartese");

            //mbushComboBoxFiltra();
            clsToolbarConfig.mbushComboBoxFiltra(idgjuha, idndermarje, "gvNjesiVartese", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
            //clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "", pnlMesazhi);
            GridUtil.EmrateButonaveMbiGride(gvNjesiVartese);
        }

        /// <summary>
        /// Vendos emrat e tabeve ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateTabeve(System.Resources.ResourceManager rm, System.Globalization.CultureInfo ci)
        {
            ASPxPageControl1.TabPages[0].Text = rm.GetString("TePergjithshmeTab", ci);
            ASPxPageControl1.TabPages[1].Text = rm.GetString("MenuItemNjesiVartese", ci);
        }


        ///// <summary>
        /////      mbush combon e filtrave
        ///// </summary>
        //private void mbushComboBoxFiltra()
        //{
        //    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka("gvNjesiVartese", "Shto_NjesiVartese.aspx", idndermarje);
        //    DbCore.DbAdmin.colFiltratGrida colFiltra = new DbCore.DbAdmin.colFiltratGrida(koka.IdGridaKoka, idndermarje);
        //    colFiltra.Insert(0, new DbCore.DbAdmin.clsFiltraGrida());
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
            clsToolbarConfig.percaktoTemplateMenu(idGjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1, komponente, this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, hfShtimModifikim.Value == "modifikim" ? false : true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
        }

        protected void RuajKolona_Click(object sender, EventArgs e)
        {
            int idfiltri = 0;
            int idkonf = DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(cmbKonfigurimi.Text.Split(';')[0], idndermarje);
            DbCore.clsMesazh mesazh = GridUtil.ruajFiltra(idndermarje, idPerdoruesi, idgjuha, "gvNjesiVartese", komponente, "FilterDefault", gvNjesiVartese.FilterExpression, gvNjesiVartese, "Kodi", idkonf, out idfiltri);//ruan filtrin e zgjedhur tek filtrat
            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                return;

            }
            mesazh = GridUtil.ruajkonfigurimgride(gvNjesiVartese, cmbKonfigurimi.Text, idndermarje, idPerdoruesi, 440, idfiltri, idviti, DbCore.mySessionObjects.ktheCultureInfo(Session), DbCore.mySessionObjects.ktheGjuhe(Session));/// ruan konfigurimin e grides dhe filtrin

            clsToolbarConfig.mbushComboBoxFiltra(idgjuha, idndermarje, "gvNjesiVartese", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);

            percaktoTemplateMenu(idgjuha, idviti, idPerdoruesi, idndermarje, ASPxMenu1);

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
            percaktoTemplateMenu(idgjuha, idviti, idPerdoruesi, idndermarje, ASPxMenu1);

        }
        protected void gvNjesiVartese_DataBound(object sender, EventArgs e)
        {// shton colonen # per selektim dhe disa karakteristika te grides
            if (this.gvNjesiVartese.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per 
                //perzgjidh
                DevExpress.Web.GridViewCommandColumn check = new DevExpress.Web.GridViewCommandColumn("#")
                {
                    ShowSelectCheckbox = true,
                    Width = Unit.Percentage(2)
                };
                gvNjesiVartese.Settings.ShowFilterRow = true;
                gvNjesiVartese.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                gvNjesiVartese.Settings.ShowFilterRowMenu = true;
                gvNjesiVartese.Columns.Add(check);
                gvNjesiVartese.KeyFieldName = "IdNjesiVartese";
                gvNjesiVartese.SettingsBehavior.AllowSelectByRowClick = true;
                gvNjesiVartese.SettingsBehavior.AllowFocusedRow = true;
            }

        }
        private void konfiguroGride(string kodKonfigurimi, int idKomponente)
        {//konfiguron griden

            KonfigurimComboGride.ShtoLlogariSipasNdermarrjes(gvNjesiVartese, idndermarje, Session, komponente, guidString, false);
            this.gvNjesiVartese.Columns["#"].VisibleIndex = 0;

        }

      
        //thirret sa here qe behet Callback, dhe ben edhe njeher lidhjen me datasource-in
        protected void gvNjesiVartese_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            if (e.CallbackName == "COLUMNMOVE" && gvNjesiVartese.AllColumns[int.Parse(e.Args[0])].Width.Value == 0)
                gvNjesiVartese.AllColumns[int.Parse(e.Args[0])].Width = Unit.Percentage(3);
            if (e.CallbackName == "APPLYFILTER" && e.Args[0] == "")
            {
                DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
                ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
                cmbFiltra.Text = "";


            }

        }

        //bere  me e konfigurueshme
        protected void gvNjesiVartese_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            if (e.Column.FieldName == "Kodi" || e.Column.FieldName == "Pershkrimi")
            {
                e.Values.Clear();
                //e.AddShowAll();
                e.AddValue("(Te gjithe)", string.Empty, "true");
                e.AddValue("Nga A-D ", string.Empty, e.Column.FieldName + ">'A     ' and " + e.Column.FieldName + " <'DDDDDDD'");
                e.AddValue("Nga D-G ", string.Empty, e.Column.FieldName + ">'D     ' and " + e.Column.FieldName + "<'GGGGGGG'");
                e.AddValue("Nga H-K ", string.Empty, e.Column.FieldName + ">'H     ' and " + e.Column.FieldName + "<'KKKKKKK'");
                e.AddValue("Nga L-O ", string.Empty, e.Column.FieldName + ">'L     ' and " + e.Column.FieldName + "  <'OOOOOOO'");
                e.AddValue("Nga P-S ", string.Empty, e.Column.FieldName + ">'P     ' and " + e.Column.FieldName + "<'SSSSSSS'");
                e.AddValue("Nga T-W ", string.Empty, e.Column.FieldName + ">'T     ' and " + e.Column.FieldName + "<'WWWWWWW'");
                e.AddValue("Nga X-Z ", string.Empty, e.Column.FieldName + ">'X     ' and " + e.Column.FieldName + "<'ZZZZZZZ'");
            }
            else
            {
                e.Values.Clear();
                e.AddValue("(Te gjithe)", string.Empty, "true");
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
            //filtra.mbushFiltraGridaSipasFiltraEmri(cmbFiltra.Text, idndermarje);
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvNjesiVartese", komponente, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();
                //mbushComboBoxFiltra();
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvNjesiVartese", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
                percaktoTemplateMenu(idgjuha, idviti, idPerdoruesi, idndermarje, ASPxMenu1);

                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                hfStatusi.Value = "true";
                gvNjesiVartese.FilterExpression = String.Empty;
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
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvNjesiVartese", komponente, idndermarje, int.Parse(cmbKonfigurimi.Value.ToString()));
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DbCore.DbAdmin.clsFiltraGrida filtri = new DbCore.DbAdmin.clsFiltraGrida() { FiltraKodi = cmbFiltra.Text, FiltraShenime = cmbFiltra.Text, FiltraUniversal = false, GridaKokaId = koka.IdGridaKoka, FiltraVlera = gvNjesiVartese.FilterExpression, IdPerdoruesi = idperdoruesi, IdNdermarje = idNdermarrje, IdStatusDok = 1 };
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("Kodi", gvNjesiVartese);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvNjesiVartese.GetSortedColumns();
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
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();


            mesazh = filtri.ruaj();
            //mbushComboBoxFiltra();
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvNjesiVartese", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
            percaktoTemplateMenu(idgjuha, idviti, idPerdoruesi, idndermarje, ASPxMenu1);

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
                rreshtat = gvNjesiVartese.GetSelectedFieldValues("IdNjesiVartese");
            else
            {
                rreshtat = new List<object>();
                rreshtat.Add(hfId.Value);
            }
            // List<object> rreshtat = gvNjesiVartese.GetSelectedFieldValues("IdNjesiVartese");
            if (rreshtat.Count == 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhZgjidhniNje, pnlMesazhi);
                return;
            }
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            //clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "", pnlMesazhi);
            List<string> TeFshire = new List<string>(), TePaFshire = new List<string>();
            DbCore.DbRegjistrim.clsDatabaseRegjistrim dbRegjistrim = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
            foreach (object id in rreshtat)
            {
                DbCore.DbInventari.clsNjesiVartese oNjesiAdm = new DbCore.DbInventari.clsNjesiVartese(Convert.ToInt32(id));
                DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                konf.IdKonfigAmbjente = oNjesiAdm.IdKonfig;
                konf.mbushKonfigAmbjSipasId(konf.IdKonfigAmbjente);

                bool lidhur = dbRegjistrim.eshteDokumentiILidhurCelje(oNjesiAdm.IdNjesiVartese.ToString(), konf.IdNivel.ToString());
                if (lidhur)
                {
                    TePaFshire.Add(oNjesiAdm.Kodi);
                    continue;
                }

                oNjesiAdm.IdPerdorues = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                mesazh = oNjesiAdm.fshi();
                if (oNjesiAdm.IdNjesiVartese == 0)
                    continue;
                if (mesazh.Status)
                {
                    #region Heq llogarite nga grida

                    // ASPxGridView_Artikull.DataSource = dt;
                    hiqNjesiNgaGrida(oNjesiAdm.IdNjesiVartese);
                    #endregion
                    TeFshire.Add(oNjesiAdm.Kodi);

                    ASPxPageControl1.ActiveTabIndex = 0;
                    hfStatusi.Value = "true";
                }


            }
            dbRegjistrim.Dispose();
            string mesazhInfoGabim = "", mesazhInfoSukses = "";
            if (TePaFshire.Count == 1)
                mesazhInfoGabim = String.Format("{0}{1}{2}", prefixMesazhNjejes, String.Join(";", TePaFshire), suffixMesazhNjejesGabimi);
            else
                if (TePaFshire.Count > 1)
                    mesazhInfoGabim = String.Format("{0}{1}{2}", prefixMesazhShumes, String.Join(";", TePaFshire), suffixMesazhShumesGabimi);
            if (TeFshire.Count == 1)
                mesazhInfoSukses = String.Format("{0}{1}{2}", prefixMesazhNjejes, String.Join(";", TeFshire), suffixMesazhNjejesSuksesi);
            else
                if (TeFshire.Count > 1)
                    mesazhInfoSukses = String.Format("{0}{1}{2}", prefixMesazhShumes, String.Join(";", TeFshire), suffixMesazhShumesSuksesi);
            if (mesazhInfoGabim != "" && mesazhInfoSukses != "")
                mesazhInfoGabim += lidhesMesazhi + mesazhInfoSukses;
            if (mesazhInfoGabim != "")
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhInfoGabim, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazhInfoSukses, pnlMesazhi);
        }

        private void hiqNjesiNgaGrida(int idmag)
        {
            if (this.gvNjesiVartese.DataSource != null)
            {
                DataTable dt = (DataTable)gvNjesiVartese.DataSource;
                DataRow[] drs = dt.Select("IdNjesiVartese = " + idmag);
                if (drs.Length > 1)
                    throw new Exception("GABIM: Ndodhen 2 njesi me te njejten id ne gride");
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                dt.Rows.Remove(dr);
                gvNjesiVartese.DataBind();
            }
            else mbushGridNjesishNgaDB();
        }
        private void shtoNjesiNeGrid(int idNdermarrje, int idPerdorues, int idmag)
        {
            if (gvNjesiVartese.DataSource != null)
            {
                DataTable dt = (DataTable)gvNjesiVartese.DataSource;
                DataRow[] drs = dt.Select("IdNjesiVartese = " + idmag);
                if (drs.Length > 0)
                    throw new Exception("GABIM: Njesia ekziston ne gride");
                DataRow newArtDr = DbCore.DbInventari.colNjesiVartese.merrSipasNjesiNdermarrjesDR(idmag);
                dt.ImportRow(newArtDr);
            }
            else mbushGridNjesishNgaDB();
        }
        private void modifikoNjesiNeGrid(int idNdermarrje, int idPerdorues, int idmag)
        {
            if (gvNjesiVartese.DataSource != null)
            {
                DataTable dt = (DataTable)gvNjesiVartese.DataSource;
                DataRow[] drs = dt.Select("IdNjesiVartese = " + idmag);
                if (drs.Length > 1)
                    throw new Exception("GABIM: Ndodhen 2 Njesi me te njejten id ne gride");
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                DataRow newArtDr = DbCore.DbInventari.colNjesiVartese.merrSipasNjesiNdermarrjesDR(idmag);
                object[] arr = newArtDr.ItemArray;
                dr.ItemArray = arr;
            }
            else mbushGridNjesishNgaDB();
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
                Page.Validate("entries"); ruajNjesi();
            }
        }
        protected void gvNjesiVartese_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if (e.Column.FieldName == "IdLlogari")
            {
                //if (ImbUtil.ConvertToInt(e.Value)==0)
                //{
                //    e.Criteria = null;
                //}
            }
        }

        protected void gvNjesiVartese_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {

        }
        protected void gvNjesiVartese_CustomJSProperties(object sender, DevExpress.Web.ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gvNjesiVartese.PageIndex;
            e.Properties["cpPageRow"] = gvNjesiVartese.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gvNjesiVartese.VisibleRowCount;
        }

        private void konfiguroVleraFillestare(int idPerdoruesi, int idNdermarrje, System.Resources.ResourceManager rm, System.Globalization.CultureInfo ci, int idGjuha)
        {
            //mbush komboboxet dhe gridat e faqes  
            ConfigureAspxComboBox.ShtoKolonaPerLlogarine(cmbLlogari);
            ConfigureAspxComboBox.percaktoTemplateComboMeEnableCallback(cmbLlogari);
            ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 41, rm, ci, idGjuha);
            cmbKonfigurimi.SelectedIndex = 0;
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konf.IdKonfigAmbjente = int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString());
            konf.mbushKonfigAmbjSipasId(konf.IdKonfigAmbjente, idgjuha);
            hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;
            // cmbKonfigurimi.SelectedIndex = -1;
        }
        private void mbushGridNjesishNgaSession()
        {
            DataTable tmpObject;
            bool sukses = DbCore.mySessionObjects.merrGrideNgaSessioni(Session, out tmpObject);
            if (!sukses)
                mbushGridNjesishNgaDB();
            else
            {
                gvNjesiVartese.DataSource = tmpObject;
                gvNjesiVartese.DataBind();
                tmpObject.Dispose();
            }
        }
        private void mbushGridNjesishNgaDB()
        {//mbush griden e popupit me te dhena            
            DataTable dt = DbCore.DbInventari.colNjesiVartese.merrSipasNjesiNdermarrjesDT(idndermarje);
            DbCore.mySessionObjects.ruajGrideNeSession(Session, dt);
            gvNjesiVartese.DataSource = dt;
            gvNjesiVartese.DataBind();
            dt.Dispose();
        }

      

        private void ruajNjesi()
        {
            DbCore.DbInventari.clsNjesiVartese njesia = new DbCore.DbInventari.clsNjesiVartese();
            if (Page.IsValid == false)
                return;
            else
            {

                if (isValidNjesiVartese())
                {
                    njesia = krijoNjesi(); bool eshteShtim;
                    DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                    DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

                    tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idperdoruesi, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), komponente);

                    if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim")
                    {
                        if (!tedrejtaInfo.DShtim)
                        {
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
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
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                            hfStatusi.Value = "false";
                            return;
                        }
                        DbCore.DbRegjistrim.clsDatabaseRegjistrim dbRegjistrim = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
                        DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                        konf.IdKonfigAmbjente = njesia.IdKonfig;
                        konf.mbushKonfigAmbjSipasId(konf.IdKonfigAmbjente);
                        eshteShtim = false;
                        njesia.IdNjesiVartese = int.Parse(hfId.Value.ToString());
                        bool lidhur = dbRegjistrim.eshteDokumentiILidhurCelje(njesia.IdNjesiVartese.ToString(), konf.IdNivel.ToString());
                        if (lidhur.ToString() != hfLidhur.Value)
                        {
                            mesazh.Status = false;
                            mesazh.PershkrimMesazhi = "Njesia  eshte e lidhur";
                        }
                        else
                            mesazh = njesia.modifiko();
                        dbRegjistrim.Dispose();
                    }
                    if (mesazh.Status == true)
                    {
                        clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["mesazhRuajtjeMeSukses"], pnlMesazhi);
                        if (eshteShtim)
                            shtoNjesiNeGrid(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), njesia.IdNjesiVartese);
                        else //modifikim
                            modifikoNjesiNeGrid(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), njesia.IdNjesiVartese);
                        hfStatusi.Value = "true";
                    }
                    else
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ndodhi nje gabim. Ruajtja nuk u krye!", pnlMesazhi);
                        hfStatusi.Value = "false";
                    }
                    ASPxPageControl1.ActiveTabIndex = 0;
                    konfiguroGride(cmbKonfigurimi.Text.Split(';')[0], 440);
                }
            }
        }

        /// <summary>
        /// krijon magazinen qe do te ruhet
        /// </summary>
        /// <returns>kthen clsNjesiVartese me magazinen qe do te ruhet</returns>
        private DbCore.DbInventari.clsNjesiVartese krijoNjesi()
        {//krijon nje llogari sipas te dhenave te futura nga perdoruesi
            DbCore.DbInventari.clsNjesiVartese njesi = new DbCore.DbInventari.clsNjesiVartese() { Kodi = txtKodi.Text, Pershkrimi = txtPershkrimi.Text, Adresa = txtAdresa.Text, IdLlogari = DbCore.DbKontabiliteti.clsLlogari.mbushIDLlogariSipasKodit(cmbLlogari.Text.Split(';')[0], idndermarje), IdNdermarje = idndermarje, IdPerdorues = idperdoruesi };
            DbCore.DbShare.clsKonfigurimAmbjenti konfig = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konfig.mbushKonfigAmbjSipasKod(cmbKonfigurimi.Text, idndermarje);
            njesi.IdKonfig = konfig.IdKonfigAmbjente;
            return njesi;
        }

        /// <summary>
        /// perdoret per te mbushur combon e autorizimit ne momentin qe perdoruesi fillon te shkruaj
        /// </summary>
        /// <param name="source">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void cmbLlogari_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("cmbLlogari"))
                {
                    ConfigureAspxComboBox.mbushComboLlogariaPaKolona(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), cmbLlogari,e);
                }
            }
        }
        protected void cmbLlogari_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("cmbLlogari"))
                {
                    ConfigureAspxComboBox.mbushComboLlogariaPaKolona(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), cmbLlogari,e);
                }
            }
        }
        private bool isValidNjesiVartese()
        {
            bool isValid;
            isValid = true;

            if (DbCore.DbInventari.clsNjesiVartese.ekziston(txtKodi.Text, idndermarje) && (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim"))
            {
                isValid = false;
                hfStatusi.Value = "false";
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ekziston nje njesi me kete kod! Ju lutemi zgjidhni nje kod tjeter.", pnlMesazhi);
                return isValid;
            }
            if (this.cmbLlogari.Text != "")
            {
                //DbCore.DbKontabiliteti.clsLlogari dege = new DbCore.DbKontabiliteti.clsLlogari(cmbLlogari.Text.Split(';')[0], idndermarje);
                if (!DbCore.DbKontabiliteti.clsLlogari.ekzistonLlogari(cmbLlogari.Text.Split(';')[0], idndermarje))
                {
                    isValid = false; clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Kjo llogari nuk ekziston!", pnlMesazhi);
                    hfStatusi.Value = "false";
                    return isValid;
                }
                else if (!DbCore.DbKontabiliteti.clsLlogari.eshteLlogariAktive(cmbLlogari.Text.Split(';')[0], idndermarje))
                {
                    isValid = false; clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Kjo llogari nuk eshte aktive!", pnlMesazhi);
                    hfStatusi.Value = "false";
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
        protected void gvNjesiVartese_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string idkomponente = "";
            string kodkonfigurimi = "";

            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                kodkonfigurimi = arr[1];
                idkomponente = arr[0];
                if (arr[2] == "")
                    gvNjesiVartese.FilterExpression = "";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    //filtra.mbushFiltraGridaSipasFiltraKodi(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvNjesiVartese", komponente, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), int.Parse(cmbKonfigurimi.Value.ToString()));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        gvNjesiVartese.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvNjesiVartese);
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
            gvNjesiVartese.Selection.UnselectAll();

        }
    }

}

