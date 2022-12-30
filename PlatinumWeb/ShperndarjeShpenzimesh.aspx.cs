using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Data;
using System.Globalization;
using System.Resources;
using DbCore;
using DbCore.DbShare;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.DataBase;
using DbCore.DbRegjistrim;

namespace PlatinumWeb
{
    public partial class ShperndarjeShpenzimesh : MyPageBase
    {
        private const string prefixMesazhNjejes = "Dokumenti i shpërndarje shpenzimesh me Nr: ";
        private const string prefixMesazhShumes = "Dokumentat e shpërndarje shpenzimesh me Nr: ";
        private const string suffixMesazhNjejesLidhurGabimi = " është i lidhur dhe nuk mund të fshihet! ";
        private const string suffixMesazhShumesLidhurGabimi = " janë të lidhura dhe nuk mund të fshihen! ";
        private const string suffixMesazhNjejesPeriudheKycurGabimi = " i përket një periudhe të kyçur dhe nuk mund të fshihet! ";
        private const string suffixMesazhShumesPeriudheKycurGabimi = " i përkasin periudhave të kycura dhe nuk mund të fshihen! ";
        private const string suffixMesazhNjejesSuksesi = " u fshi me sukses!";
        private const string suffixMesazhShumesSuksesi = " u fshinë me sukses!";
        private const string lidhesMesazhi = ". Kurse ";
        private const string komponente = "ShperndarjeShpenzimesh.aspx";
        private string guidString;

        DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();
        System.Globalization.CultureInfo cultinf;
        System.Resources.ResourceManager rm;

        int idGjuha;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                DbCore.clsFunksione.logout(Session,true,"FaqePaautorizuar");
            }
            int idPerd = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerd);
                return;
            }
            oPerdorues = DbCore.mySessionObjects.kthePerdorues(Session);
            int idviti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
           
            cultinf = DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo(idGjuha);
            rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (!IsPostBack)
            {
                guidString = Guid.NewGuid().ToString();
                hfState["guidString"] = guidString;
                EmrateLabelave(cultinf, rm);
                ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idPerd, idNdermarrje, cmbKonfigurimi, 56, rm, cultinf, idGjuha);
                cmbKonfigurimi.SelectedIndex = 0;
                DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()));
                
                hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;
                mbushGridSHSHNgaDB();
                grid_ShperndarjeShpenzimesh.FilterExpression = "[IdStatusDok]=1";
                konfiguroGride(rm, cultinf);
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), "grid_ShperndarjeShpenzimesh", grid_ShperndarjeShpenzimesh, cmbKonfigurimi.Text.Split(';')[0], "517", DbCore.mySessionObjects.ktheGjuhe(Session));

                if (Request.QueryString["fshi"] == "rivleresimjo")
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("regjisDokMesazhGabimRivleresimi", cultinf), pnlMesazhi);
                }
                else if (Request.QueryString["fshi"] == "rivleresimpo")
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("regjisDokMesazhSuksesiRivleresim", cultinf), pnlMesazhi);
                else if (Request.QueryString["fshi"] == "po")
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("regjisDokMesazhSukesFshirjeDokument", cultinf), pnlMesazhi);
                else if (Request.QueryString["ruaj"] == "po")
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("regjMagModifikimiPerfundoiMeSukses", cultinf), pnlMesazhi);
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerd, idNdermarrje, idviti, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
            }
            else
            {
                guidString = hfState["guidString"].ToString();
                mbushGridSHSHNgaSession();
                konfiguroGride(rm, cultinf);
            }
            GridUtil.konfigGrideListeEMadhePaTheme(grid_ShperndarjeShpenzimesh, "IdKokaShperndarjeShpenz");
            clsToolbarConfig.mbushComboBoxFiltra(idPerd, idNdermarrje, "grid_ShperndarjeShpenzimesh", int.Parse(cmbKonfigurimi.Value.ToString()), "ShperndarjeShpenzimesh.aspx");
            if (Request.QueryString["indexrow"] != null)
                grid_ShperndarjeShpenzimesh.FocusedRowIndex = int.Parse(Request.QueryString["indexrow"]);
            AspxWebControlUtils.perkthePopUp(popFshi, rm.GetString("labelKujdes", cultinf), lblMsgbox, rm.GetString("labelAdministrimiMsgJeniSigurt", cultinf), ButtonCancel, rm.GetString("labelAnullo", cultinf));
            percaktoTemplateMenu(idGjuha, ASPxMenu1, idviti, idPerd, idNdermarrje);
            grid_ShperndarjeShpenzimesh.PercaktoTitlePanel(this, MenuInfo, pnlMesazhi, hfState, idPerd, idNdermarrje, idviti, idGjuha, int.Parse(cmbKonfigurimi.Value.ToString()), komponente, rm, cultinf);

        }

        ///// <summary>
        /////      mbush combon e filtrave
        ///// </summary>
        //private void mbushComboBoxFiltra()
        //{
        //    int idNdermarrja = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
        //    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka("grid_ShperndarjeShpenzimesh", "ShperndarjeShpenzimesh.aspx", idNdermarrja);
        //    DbCore.DbAdmin.colFiltratGrida colFiltra = new DbCore.DbAdmin.colFiltratGrida(koka.IdGridaKoka, idNdermarrja);
        //    colFiltra.Insert(0, new DbCore.DbAdmin.clsFiltraGrida()); //colFiltra = dbAdmin.merrGjitheFiltratGridaByGridaKoka(koka.IdGridaKoka, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //    MenuFilter.colekstioni = colFiltra;
        //    MenuFilter.ValueField = "IdFiltra";
        //    MenuFilter.TextField = "FiltraShenime";
        //}
        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        /// <param name="idViti"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNermarrje"></param>
        private void percaktoTemplateMenu(int idGjuha, ASPxMenu aSPxMenu1, int idViti, int idPerdorues, int idNdermarrje)
        {
            clsToolbarConfig.percaktoTemplateMenu(idGjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1, komponente, this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, btnPo_Click, btnJo_Click, true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session), false);
        }

        /// <summary>
        /// ruan konfigurimin e grides dhe filtrin e zgjedhur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RuajKolona_Click(object sender, EventArgs e)
        {
            int idfiltri = 0;
            int idndermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idperdorues = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            int idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            int idgjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            int idkonf = DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(cmbKonfigurimi.Text.Split(';')[0], idndermarrje);
            DbCore.clsMesazh mesazh = GridUtil.ruajFiltra(idndermarrje, idperdorues, idgjuha, "grid_ShperndarjeShpenzimesh", komponente, "FilterDefault", grid_ShperndarjeShpenzimesh.FilterExpression, grid_ShperndarjeShpenzimesh, "IdNivel", idkonf, out idfiltri);//ruan filtrin e zgjedhur tek filtrat
            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                return;

            }
            mesazh = GridUtil.ruajkonfigurimgride(grid_ShperndarjeShpenzimesh, cmbKonfigurimi.Text, idndermarrje, idperdorues, 517, idfiltri, idViti, DbCore.mySessionObjects.ktheCultureInfo(Session), DbCore.mySessionObjects.ktheGjuhe(Session));/// ruan konfigurimin e grides dhe filtrin

            clsToolbarConfig.mbushComboBoxFiltra(idgjuha, idndermarrje, "grid_ShperndarjeShpenzimesh", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);

            percaktoTemplateMenu(idgjuha, ASPxMenu1, idViti, idperdorues, idndermarrje);

            if (mesazh.Status) clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

        }
        public void btnJo_Click(object sender, EventArgs e)
        {
            //clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "", pnlMesazhi);
            if (pergjigja.Text.Contains("lidhur") || pergjigja.Text.Contains("kycur"))
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, pergjigja.Text, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, pergjigja.Text, pnlMesazhi);
            //CacheLayer.GlobalCacheManager.MySessionCache["trupat"] = new DbCore.DbRegjistrim.colTrupiMagazina();
            DbCore.mySessionObjects.ruajTrupatNeSession(Session, new DbCore.DbRegjistrim.colTrupiMagazina());
        }
        public void btnPo_Click(object sender, EventArgs e)
        {
            System.Globalization.CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            //DbCore.DbRegjistrim.colTrupiMagazina     trupat = (DbCore.DbRegjistrim.colTrupiMagazina)CacheLayer.GlobalCacheManager.MySessionCache["trupat"];            
            DbCore.DbRegjistrim.colTrupiMagazina trupat = DbCore.mySessionObjects.merrTrupatNgaSesioni(Session);
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh(true, rm.GetString("regjisDokMesazhSuksesiRivleresim", cultinf));
            string fileLogPath = Server.MapPath("~/log/log.txt");
            DbCore.DbAdmin.clsLogRivleresimInventari log = new DbCore.DbAdmin.clsLogRivleresimInventari();
            try
            {
                log = new DbCore.DbAdmin.clsLogRivleresimInventari(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex.Message);
                throw new DbCore.MyException(rm.GetString("msgGabimGjateRuajtjesSeRivleresimitNeLog",cultinf));
            }
            //db.krijoManager();
            foreach (DbCore.DbRegjistrim.clsTrupiMagazina t in trupat)
            {
                //DbCore.DbInventari.clsArtikulli art = new DbCore.DbInventari.clsArtikulli();
                //art.mbushArtikull(t.IdArtikulli);
                //mesazh = art.rivleresimCmimiMesatar(t.IdMag, t.Data, DateTime.Today);          
                //DbCore.DbInventari.clsArtikulli art = new DbCore.DbInventari.clsArtikulli(t.IdArtikulli);
                mesazh = DbCore.DbInventari.clsArtikulli.rivleresimCmimiMesatar(t.IdArtikulli, DbCore.DbInventari.clsArtikulli.ktheMetodeKostoje(t.IdArtikulli), t.IdMag, t.Data, DateTime.Today, DbCore.DbInventari.clsArtikulli.ktheKontrollCmimiPerDetajim(t.IdArtikulli),log,cultinf,rm, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
                if (!mesazh.Status)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("regjisDokMesazhGabimRivleresimi", cultinf), pnlMesazhi);
                    return;
                }
            }
            //CacheLayer.GlobalCacheManager.MySessionCache["trupat"] = new DbCore.DbRegjistrim.colTrupiMagazina();
            DbCore.mySessionObjects.ruajTrupatNeSession(Session, new DbCore.DbRegjistrim.colTrupiMagazina());
            if (mesazh.Status)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("regjisDokMesazhSuksesiRivleresim", cultinf), pnlMesazhi);
        }

        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));

        }
        private void mbushGridSHSHNgaSession()
        {
            DataTable tmpObject;
            bool sukses = DbCore.mySessionObjects.merrGrideNgaSessioni(Session, out tmpObject);
            if (!sukses)
                mbushGridSHSHNgaDB();
            else
            {
                grid_ShperndarjeShpenzimesh.DataSource = tmpObject;
                grid_ShperndarjeShpenzimesh.DataBind();
                tmpObject.Dispose();
            }
        }
        private void mbushGridSHSHNgaDB()
        {//mbush griden e popupit me te dhena            
            DataTable dt = DbCore.DbRegjistrim.colShperndarjeShpenzimeKoka.merrSHSHDT(DbCore.mySessionObjects.ktheNdermarrjeVit(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            DbCore.mySessionObjects.ruajGrideNeSession(Session, dt);
            grid_ShperndarjeShpenzimesh.DataSource = dt;
            grid_ShperndarjeShpenzimesh.DataBind();
            dt.Dispose();
        }
        private void konfiguroVleraFillestare()
        {
            DbCore.DbRegjistrim.colShperndarjeShpenzimeKoka colKoka = new DbCore.DbRegjistrim.colShperndarjeShpenzimeKoka(DbCore.mySessionObjects.ktheNdermarrjeVit(Session));
            grid_ShperndarjeShpenzimesh.DataSource = colKoka;
            grid_ShperndarjeShpenzimesh.DataBind();
        }

        private void konfiguroGride(ResourceManager rm, CultureInfo ci)
        {
            KonfigurimComboGride.ShtoStatus(grid_ShperndarjeShpenzimesh, rm, ci);
       

            GridViewDataTextColumn col3 = grid_ShperndarjeShpenzimesh.Columns["VleraTotale"] as GridViewDataTextColumn;
            col3.PropertiesEdit.DisplayFormatString = "0.00";
            this.grid_ShperndarjeShpenzimesh.Columns["#"].VisibleIndex = 0;
            this.grid_ShperndarjeShpenzimesh.ToolTip = rm.GetString("TitullShperndarjetEShpenzimeve", cultinf);
        }

        //private void shtoStatus()
        //{
        //    DbCore.DbArkaBanka.clsDatabaseArkaBanka data = new DbCore.DbArkaBanka.clsDatabaseArkaBanka();
        //    grid_ShperndarjeShpenzimesh.Columns.Remove(grid_ShperndarjeShpenzimesh.Columns["IdStatusDok"]);
        //    GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
        //    DataSet ds = data.merrStatusinDokumentave();
        //    data.Dispose();
        //    DataRow dr = ds.Tables[0].NewRow();
        //    object[] rowArray = new object[2]; rowArray[0] = null; rowArray[1] = "";
        //    dr.ItemArray = rowArray;
        //    ds.Tables[0].Rows.InsertAt(dr, 0);
        //    colnew.PropertiesComboBox.DataSource = ds;
        //    colnew.PropertiesComboBox.TextField = ds.Tables[0].Columns[1].ToString();
        //    colnew.PropertiesComboBox.ValueField = ds.Tables[0].Columns[0].ToString();
        //    colnew.FieldName = "IdStatusDok";
        //    grid_ShperndarjeShpenzimesh.Columns.Add(colnew);
        //    ds.Dispose();
        //}

        protected void grid_ShperndarjeShpenzimesh_DataBound(object sender, EventArgs e)
        {
            if (this.grid_ShperndarjeShpenzimesh.Columns["#"] == null)
            {
                DevExpress.Web.GridViewCommandColumn check = new DevExpress.Web.GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true; check.Width = Unit.Percentage(2);
                // check.SetColVisibleIndex(0);
                grid_ShperndarjeShpenzimesh.Settings.ShowFilterRow = true;
                grid_ShperndarjeShpenzimesh.Settings.ShowHeaderFilterButton = true;
                grid_ShperndarjeShpenzimesh.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                grid_ShperndarjeShpenzimesh.Settings.ShowFilterRowMenu = true;
                grid_ShperndarjeShpenzimesh.Columns.Add(check);
                grid_ShperndarjeShpenzimesh.Settings.ShowGroupPanel = true;
                grid_ShperndarjeShpenzimesh.KeyFieldName = "IdKokaShperndarjeShpenz";
                grid_ShperndarjeShpenzimesh.SettingsBehavior.AllowSelectByRowClick = true;
                grid_ShperndarjeShpenzimesh.SettingsBehavior.AllowFocusedRow = true;
            } //this.grid_ShperndarjeShpenzimesh.Columns["#"].VisibleIndex = 0;
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
            //filtra.mbushFiltraGridaSipasFiltraEmri(cmbFiltra.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "grid_ShperndarjeShpenzimesh", komponente, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            //DbCore.DbAdmin.clsFiltraGrida filtra = dbAdmin.merrFiltraGridaSipasFiltraEmri(cmbFiltra.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            if (filtra.FiltraKodi != null)
            {
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                mesazh = filtra.fshi();
                //mbushComboBoxFiltra();
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "grid_ShperndarjeShpenzimesh", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
                percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);

                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                //  konfiguroVleraFillestare();
                grid_ShperndarjeShpenzimesh.FilterExpression = " [IdStatusDokumenti]=1 ";


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

            DbCore.DbAdmin.clsFiltraGrida filtri = new DbCore.DbAdmin.clsFiltraGrida();
            filtri.FiltraKodi = cmbFiltra.Text;
            filtri.FiltraShenime = cmbFiltra.Text;
            filtri.FiltraUniversal = false;// Universal_ASPxCheckBox.Checked;
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "grid_ShperndarjeShpenzimesh", komponente, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = grid_ShperndarjeShpenzimesh.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("NrDok", grid_ShperndarjeShpenzimesh);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = grid_ShperndarjeShpenzimesh.GetSortedColumns();
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
            //    filtri.KoloneRenditje = "NrDok";
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
            //mbushComboBoxFiltra();
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "grid_ShperndarjeShpenzimesh", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
            percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);

            if (mesazh.Status == true)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

            cmbFiltra.Text = "";
        }



        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {
            //if (e.Item.Name == "Shto")
            //    {
            //    Response.Redirect("Shto_ShperndarjeShpenzimesh.aspx?shtim_modifikim=shtim");
            //    }
            //else if (e.Item.Name == "Modifiko")
            //    {
            //    int indeksi = grid_ShperndarjeShpenzimesh.FocusedRowIndex;
            //    string id;

            //    if (grid_ShperndarjeShpenzimesh.GetRowValues(indeksi, "IdKokaShperndarjeShpenz") != null)
            //        id = grid_ShperndarjeShpenzimesh.GetRowValues(indeksi, "IdKokaShperndarjeShpenz").ToString();
            //    else id = null;
            //    Response.Redirect("Shto_ShperndarjeShpenzimesh.aspx?id=" + id + "&indexrow=" + grid_ShperndarjeShpenzimesh.FocusedRowIndex + "&shtim_modifikim=modifikim");
            //    }
        }

        //fshin rreshtat e selektuar
        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            DbCore.DbRegjistrim.colTrupiMagazina tr = new DbCore.DbRegjistrim.colTrupiMagazina();
            DbCore.DbRegjistrim.colTrupiMagazina trupat = new DbCore.DbRegjistrim.colTrupiMagazina();
            bool rivleresim = false;
            List<string> TeFshire = new List<string>(), TeLidhur = new List<string>(), PeriudheKycur = new List<string>(), closedPeriod = new List<string>();
            DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            //clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "", pnlMesazhi);
            List<object> rreshtat = grid_ShperndarjeShpenzimesh.GetSelectedFieldValues("IdKokaShperndarjeShpenz");
            pergjigja.Text = "";
           // DbCore.DbAdmin.clsPeriudhaKontabel periudha;
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            foreach (object id in rreshtat)
            {
                DbCore.DbRegjistrim.clsShperndarjeShpenzimeKoka clsKoka = new DbCore.DbRegjistrim.clsShperndarjeShpenzimeKoka(Convert.ToInt32(id));
                bool ekycur = DbCore.DbAdmin.clsPeriudhaKontabel.eshteKycurPeriudheSipasDateDheNdermarrjes(clsKoka.DtDok, idNdermarrje);
                if (ekycur)
                {
                    PeriudheKycur.Add(clsKoka.NrDok);
                    continue;
                }

                if (DbCore.MbylljePeriudhe.PeriodClosing.IsPeriodClosed(clsKoka.DtDok, MyConnectionsManager.GetSelectedConNameServer(), idNdermarrje, KategoriDokumenti.ShperndarjeShpenzimesh, clsKoka.IdKonfigAmbjente))
                {
                    closedPeriod.Add(clsKoka.NrDok);
                    continue;
                }

                bool kontrollorivleresim = clsAlternativaKushti.getAlternativa(clsKoka.IdKonfigAmbjente, "KR") == "Po";
                bool lidhur = dbAdmin.eshteDokumentiILidhur(clsKoka.IdKokaShperndarjeShpenz, clsKoka.IdNivel, "T_SHPERNDARJESHPENZIMEKOKA", "IDSHPERNDARJESHPENZ");
                if (lidhur)
                {
                    TeLidhur.Add(clsKoka.NrDok);
                    continue;
                }             

                if (kontrollorivleresim)
                {
                    DbCore.DbRegjistrim.colKokaMagazina col = new DbCore.DbRegjistrim.colKokaMagazina();
                    col.mbushKokaMagazinaSipasIDGjenerues(clsKoka.IdKonfigAmbjente, clsKoka.IdKokaShperndarjeShpenz);
                    foreach (DbCore.DbRegjistrim.clsKokaMagazina m in col)
                    {
                        if (m.IdStatusDok == 0)
                        {
                            rivleresim = false;
                            continue;
                        }

                        if (m.IdKokaMagazina != 0 && m.IdStatusDok != 0)
                            if (m.rivleresim())
                            {
                                rivleresim = true;
                                tr.mbushGjitheTrupiMagazinaNgaKoka(m.IdKokaMagazina);
                                trupat.AddRange(tr);
                            }
                    }
                }
                clsKoka.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                mesazh = clsKoka.fshi();
                if (mesazh.Status)
                {
                    #region Heq llogarite nga grida

                    // ASPxGridView_Artikull.DataSource = dt;
                    hiqSHSHNgaGrida(clsKoka.IdKokaShperndarjeShpenz, rm, ci);
                    #endregion
                    TeFshire.Add(clsKoka.NrDok);
                }
                //    }
                //else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Dokumenti eshte i lidhur dhe nuk mund te fshihet", pnlMesazhi);

            }
            //Session.Add("trupat", trupat);
            DbCore.mySessionObjects.ruajTrupatNeSession(Session, trupat);
            dbAdmin.Dispose();
            string mesazhInfoGabimLidhur = "", mesazhInfoGabimPeridheKycur = "", mesazhInfoSukses = "", mesazhClosedPeriod = "";

            if (TeLidhur.Count == 1)
                mesazhInfoGabimLidhur = String.Format("{0}{1}{2}", rm.GetString("msgDokShperndarjeShpenzNr", ci), String.Join(";", TeLidhur), rm.GetString("suffixMesazhNjejesLidhurGabimi", ci));
            else
                if (TeLidhur.Count > 1)
                    mesazhInfoGabimLidhur = String.Format("{0}{1}{2}", rm.GetString("msgDokShperndarjeShpenzNrShumes", ci), String.Join(";", TeLidhur), rm.GetString("suffixMesazhShumesLidhurGabimi", ci));
            if (PeriudheKycur.Count == 1)
                mesazhInfoGabimPeridheKycur = String.Format("{0}{1}{2}", rm.GetString("msgDokShperndarjeShpenzNr", ci), String.Join(";", PeriudheKycur), rm.GetString("suffixMesazhNjejesPeriudheKycurGabimi", ci));
            else
                if (PeriudheKycur.Count > 1)
                    mesazhInfoGabimPeridheKycur = String.Format("{0}{1}{2}", rm.GetString("msgDokShperndarjeShpenzNrShumes", ci), String.Join(";", PeriudheKycur), rm.GetString("suffixMesazhShumesPeriudheKycurGabimi", ci));


            if (closedPeriod.Count == 1)
                mesazhClosedPeriod = String.Format("{0}{1}{2}", rm.GetString("regjisDokPrefixMesazhNjejes", ci), String.Join(", ", closedPeriod), rm.GetString("suffixMesazhClosedPeriodNjejes", ci));
            else if (closedPeriod.Count > 1)
                mesazhClosedPeriod = String.Format("{0}{1}{2}", rm.GetString("regjisDokPrefixMesazhNjejes", ci), String.Join(", ", closedPeriod), rm.GetString("suffixMesazhClosedPeriodShumes", ci));

            if (TeFshire.Count == 1)
                mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("msgDokShperndarjeShpenzNr", ci), String.Join(";", TeFshire), rm.GetString("suffixMesazhNjejesSuksesi", ci));
            else
                if (TeFshire.Count > 1)
                    mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("msgDokShperndarjeShpenzNrShumes", ci), String.Join(";", TeFshire), rm.GetString("suffixMesazhShumesSuksesi", ci));

            mesazhInfoGabimLidhur += mesazhInfoGabimPeridheKycur + mesazhClosedPeriod;
            if (mesazhInfoGabimLidhur != "" && mesazhInfoSukses != "")
            {
                mesazhInfoGabimLidhur += rm.GetString("lidhesMesazhi") + mesazhInfoSukses;
                pergjigja.Text = mesazhInfoGabimLidhur;
                pergjigja.ClientVisible = false;
            }
            else
            {
                pergjigja.Text = mesazhInfoSukses;
                pergjigja.ClientVisible = false;
            }

            if (rreshtat.Count == 0)
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("regjMagMesazhZgjidhniNje", ci), pnlMesazhi);
            else

                if (rivleresim)
                    clsMenuInfo.ShtoPyetje(MenuInfo, rm.GetString("msgVeprimNdryshimeRivleresim", ci), pnlMesazhi, idGjuha);
                else
                    if (mesazhInfoGabimLidhur != "")
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhInfoGabimLidhur, pnlMesazhi);
                    else
                        clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazhInfoSukses, pnlMesazhi);

        }
        private void hiqSHSHNgaGrida(int idkokafletekontabel, ResourceManager rm, CultureInfo ci)
        {
            if (this.grid_ShperndarjeShpenzimesh.DataSource != null)
            {
                DataTable dt = (DataTable)grid_ShperndarjeShpenzimesh.DataSource;
                DataRow[] drs = dt.Select("IdKokaShperndarjeShpenz = " + idkokafletekontabel);
                if (drs.Length > 1)
                    throw new Exception(rm.GetString("msgGabimDyShperndShpenz", ci));
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                dt.Rows.Remove(dr);
                grid_ShperndarjeShpenzimesh.DataSource = dt;
                grid_ShperndarjeShpenzimesh.DataBind();
                dt.Dispose();
            }
            else mbushGridSHSHNgaDB();
        }
        protected void grid_ShperndarjeShpenzimesh_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (e.CallbackName == "COLUMNMOVE" && grid_ShperndarjeShpenzimesh.AllColumns[int.Parse(e.Args[0])].Width.Value == 0)
                grid_ShperndarjeShpenzimesh.AllColumns[int.Parse(e.Args[0])].Width = Unit.Percentage(3);

            if (e.CallbackName == "APPLYFILTER" && e.Args[0] == "")
            {
                DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
                ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
                cmbFiltra.Text = "";
            }
            GridUtil.ToolTipButonaveMbiGride(grid_ShperndarjeShpenzimesh, ci, rm);
        }
        protected void grid_ShperndarjeShpenzimesh_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string[] arr = e.Parameters.Split(';');
            GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), "grid_ShperndarjeShpenzimesh", grid_ShperndarjeShpenzimesh, cmbKonfigurimi.Text.Split(';')[0], "517", DbCore.mySessionObjects.ktheGjuhe(Session));
            if (arr.Length == 3)
            {
                if (arr[2] == "")
                    grid_ShperndarjeShpenzimesh.FilterExpression = " [IdStatusDokumenti]=1 ";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    //filtra.mbushFiltraGridaSipasFiltraKodi(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "grid_ShperndarjeShpenzimesh", komponente, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), int.Parse(cmbKonfigurimi.Value.ToString()));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
                    //DbCore.DbAdmin.clsFiltraGrida filtra = dbAdmin.merrFiltraGridaSipasFiltraKodi(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    if (filtra.FiltraKodi != null)
                    {
                        grid_ShperndarjeShpenzimesh.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, grid_ShperndarjeShpenzimesh);
                    }
                }
            }


            grid_ShperndarjeShpenzimesh.Selection.UnselectAll();
        }

        protected void grid_ShperndarjeShpenzimesh_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = grid_ShperndarjeShpenzimesh.PageIndex;
            e.Properties["cpPageRow"] = grid_ShperndarjeShpenzimesh.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = grid_ShperndarjeShpenzimesh.VisibleRowCount;
        }

        protected void grid_ShperndarjeShpenzimesh_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {

        }

        protected void grid_ShperndarjeShpenzimesh_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            string TeGjithe = rm.GetString("GridHeaderFilterFillItemTeGjithe", ci);
            string nga = rm.GetString("GridHeaderFilterFillItemNga", ci);
            if (e.Column.FieldName == "NrDok" || e.Column.FieldName == "Shenime")
            {
                e.Values.Clear();
                e.AddValue(TeGjithe, string.Empty, "true");
                e.AddValue(nga + "A-D ", string.Empty, e.Column.FieldName + ">'A     ' and " + e.Column.FieldName + " <'DDDDDDD'");
                e.AddValue(nga + " D-G ", string.Empty, e.Column.FieldName + ">'D     ' and " + e.Column.FieldName + "<'GGGGGGG'");
                e.AddValue(nga + " H-K ", string.Empty, e.Column.FieldName + ">'H     ' and " + e.Column.FieldName + "<'KKKKKKK'");
                e.AddValue(nga + "L-O ", string.Empty, e.Column.FieldName + ">'L     ' and " + e.Column.FieldName + "  <'OOOOOOO'");
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
        /// Vendos emrat e label ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo cultinf, ResourceManager rm)
        {
            konfigurimi_Label.Text = rm.GetString("lblLloji", cultinf);
        }
    }
}