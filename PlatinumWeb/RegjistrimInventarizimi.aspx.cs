using DbCore;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DbCore.IMBUtils.Types;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Filters;
using PlatinumWeb.ApplicationUtils.Pages;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;

namespace PlatinumWeb
{
    public partial class RegjistrimInventarizimi : MyPageBase
    {
       
        private static string pershkrimDaljeFK = "Nga daljet e magazinës";
        private static string pershkrimHyrjeFK = "Nga hyrjet e magazinës";
        private string[] periudhat;
        DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();
        private TitlePeriudha _periudha;
        public TitlePeriudha Periudha => _periudha ?? (_periudha = this.MerrPeriudhe(hfState));

        private DbCore.DbRegjistrim.colTrupiMagazina trupat = new DbCore.DbRegjistrim.colTrupiMagazina();
        private string guidString;

        protected void Page_Load(object sender, EventArgs e)
        {
            int idPerdoruesi;
            int idGjuha;
            int idNdermarrje;
            int idViti;
            int idNdermarrjeVit;
            bool eshteOwn;
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (!DbCore.mySessionObjects.isLogedIn(Session))
                DbCore.clsFunksione.logout(Session,true,"FaqePaautorizuar");
            int idPerd = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerd);
            oPerdorues = DbCore.mySessionObjects.kthePerdorues(Session);
            string komponente = DbCore.clsFunksione.GetKomponente(Page.Request);
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
                eshteOwn = DbCore.mySessionObjects.merrEshteOwnSesioni(Session);
                hfState.Set("idPerdoruesi", idPerdoruesi);
                hfState.Set("idGjuha", idGjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("idViti", idViti);

                hfState.Set("idNdermarrjeVit", idNdermarrjeVit);
                hfState.Set("OwnShop", eshteOwn);
                guidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                hfState.Set("guidString", guidString);
            }
            else
            {
                idPerdoruesi = (int)hfState["idPerdoruesi"];
                idGjuha = (int)hfState["idGjuha"];
                idNdermarrje = (int)hfState["idNdermarrje"];
                idViti = (int)hfState["idViti"];

                idNdermarrjeVit = (int)hfState["idNdermarrjeVit"];
                guidString = (string)hfState["guidString"];
            }
            percaktoTemplateMenu(idGjuha, ASPxMenu1, idViti, idPerd, idNdermarrje);
            if (!IsPostBack)
            {
                EmrateButonave(cultinf, rm);
                mbushHiddenFieldMePerkthime(cultinf, rm);
                if (Request.QueryString["lloj"] == "agj")
                    ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idPerd, idNdermarrje, cmbKonfigurimi, 134, "LDIAAGJ", rm, cultinf, idGjuha);
                else ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idPerd, idNdermarrje, cmbKonfigurimi, 134, "LDIAASH", rm, cultinf, idGjuha);
                cmbKonfigurimi.SelectedIndex = 0;
                DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()), idGjuha);
                hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;
               
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaDokumentaAGJ = new DbCore.DbAdmin.clsTeDrejtaRoli();
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaDokumentaASH= new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaDokumentaAGJ.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, "RegjistrimInventarizimi.aspx?lloj=agj");
                hfTeDrejtaGjitheDokHyrje.Value = tedrejtaDokumentaAGJ.DGjitheDok.ToString();
                tedrejtaDokumentaASH.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, "RegjistrimInventarizimi.aspx?lloj=ash");
                hfTeDrejtaGjitheDokDalje.Value = tedrejtaDokumentaASH.DGjitheDok.ToString();

                grid_RegInv.PercaktoTitlePanelMePeriudhe(this, MenuInfo, pnlMesazhi, hfState, idPerdoruesi, idNdermarrje, idViti, idGjuha, int.Parse(cmbKonfigurimi.Value.ToString()), komponente, 513, "IdKoka", rm, cultinf, false);
                mbushGridDokumentInventarizimNgaDB(komponente, tedrejtaDokumentaAGJ.DGjitheDok, tedrejtaDokumentaASH.DGjitheDok);

                grid_RegInv.FilterExpression = " [IdStatusDok]=1";
               
                hfLloji.Value = Request.QueryString["lloj"];
                if (Request.QueryString["fshi"] == "po")
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("regjMagFshirjaPerfundoiMeSukses", cultinf), pnlMesazhi);
              
                else if (Request.QueryString["ruaj"] == "po")
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("regjMagModifikimiPerfundoiMeSukses", cultinf), pnlMesazhi);
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerd, idNdermarrje, idViti, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
                konfiguroGride(idGjuha, idNdermarrje, idPerd, cultinf, rm, komponente);
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "grid_RegInv", grid_RegInv, cmbKonfigurimi.Text.Split(';')[0], "547", idGjuha);
            }

         
            GridUtil.konfigGrideListeEMadhePaTheme(grid_RegInv, "IdKoka");
            if (IsPostBack)
            {
                
                grid_RegInv.PercaktoTitlePanelMePeriudhe(this, MenuInfo, pnlMesazhi, hfState, idPerdoruesi, idNdermarrje, idViti, idGjuha, int.Parse(cmbKonfigurimi.Value.ToString()), "RegjistrimInventarizimi.aspx?lloj=agj", 513, "IdKoka", rm, cultinf, false);
          
                mbushGridDokumentInventarizimNgaSession(komponente);
                Container.Attributes["src"] = "";
                konfiguroGride(idGjuha, idNdermarrje, idPerd, cultinf, rm, komponente);
            }



            //  clsFunksione.ToolTipButonaveMbiGride(grid_RegInv, cultinf, rm);
            AspxWebControlUtils.perkthePopUp(popFshi, rm.GetString("labelKujdes", cultinf), lblMsgbox, rm.GetString("labelAdministrimiMsgJeniSigurt", cultinf), ButtonCancel, rm.GetString("labelAnullo", cultinf));
            clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idNdermarrje, "grid_RegInv", int.Parse(cmbKonfigurimi.Value.ToString()), "RegjistrimInventarizimi.aspx?lloj=agj");
            if (Request.QueryString["indexrow"] != null)
                grid_RegInv.FocusedRowIndex = int.Parse(Request.QueryString["indexrow"]);
            this.grid_RegInv.Columns["#"].VisibleIndex = 0;
        }

        /// <summary>
        /// Metode qe sherben per te kaluar perkthime te js
        /// </summary>
        /// <param name="cultinf">Merr culture info</param>
        /// <param name="rm">Merr Resource Manager</param>
        private void mbushHiddenFieldMePerkthime(CultureInfo cultinf, ResourceManager rm)
        {
            hfState.Set("msgJuKeniZgjedhur", rm.GetString("msgJuKeniZgjedhur", cultinf));
            hfState.Set("msgRreshta", rm.GetString("msgRreshta", cultinf));
            hfState.Set("labelAdministrimiMsgJeniSigurt", rm.GetString("labelAdministrimiMsgJeniSigurt", cultinf));
            hfState.Set("msgZgjdhniNjeNgaElementetEListes", rm.GetString("msgZgjdhniNjeNgaElementetEListes", cultinf));
            hfState.Set("msgDokNukMundTeKonvertohet", rm.GetString("msgDokNukMundTeKonvertohet", cultinf));
            konfigurimi_Label.Text = rm.GetString("lblModeli", cultinf);
        }

        /// <summary>
        /// Vendos emrat e butonave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="cultinf">Merr culture info perkatese</param>
        /// <param name="rm">Merr Resource Manager</param>
        private void EmrateButonave(CultureInfo cultinf, ResourceManager rm)
        {
            ButtonCancel.Text = rm.GetString("btnAdministrimiCancel", cultinf);
        }



       
        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        /// <param name="idViti"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNermarrje"></param>
        private void percaktoTemplateMenu(int idGjuha, ASPxMenu aSPxMenu1, int idViti, int idPerdorues, int idNdermarrje)
        {
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, DbCore.clsFunksione.GetKomponente(Page.Request), this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, btnPo_Click, btnJo_Click, true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session), false);
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
            DbCore.clsMesazh mesazh = GridUtil.ruajFiltra(idndermarrje, idperdorues, idgjuha, "grid_RegInv", "RegjistrimInventarizimi.aspx?lloj=agj", "FilterDefault", grid_RegInv.FilterExpression, grid_RegInv, "IdNivel", idkonf, out idfiltri);//ruan filtrin e zgjedhur tek filtrat
            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                return;

            }
            mesazh = GridUtil.ruajkonfigurimgride(grid_RegInv, cmbKonfigurimi.Text, idndermarrje, idperdorues, 547, idfiltri, idViti, DbCore.mySessionObjects.ktheCultureInfo(Session), DbCore.mySessionObjects.ktheGjuhe(Session));/// ruan konfigurimin e grides dhe filtrin

            clsToolbarConfig.mbushComboBoxFiltra(idgjuha, idndermarrje, "grid_RegInv", int.Parse(cmbKonfigurimi.Value.ToString()), "RegjistrimInventarizimi.aspx?lloj=agj");

            percaktoTemplateMenu(idgjuha, ASPxMenu1, idViti, idperdorues, idndermarrje);


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
            percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));

        }
        private void mbushGridDokumentInventarizimNgaSession(string komponente)
        {
            DataTable tmpObject;
            bool sukses = DbCore.mySessionObjects.merrGrideNgaSessioni(komponente, (int)hfState["idViti"], Periudha.PeriudhaDok, Session, out tmpObject);
            if (!sukses)
            {
                bool gjitheDokumentathyrje = hfTeDrejtaGjitheDokHyrje.Value == "True" || hfTeDrejtaGjitheDokHyrje.Value == "true";
                bool gjitheDokumentatdalje = hfTeDrejtaGjitheDokDalje.Value == "True" || hfTeDrejtaGjitheDokDalje.Value == "true";
                mbushGridDokumentInventarizimNgaDB(komponente, gjitheDokumentathyrje, gjitheDokumentatdalje);
            }
            else
            {
                grid_RegInv.DataSource = tmpObject;
                grid_RegInv.DataBind();
                tmpObject.Dispose();
            }
        }

        private void mbushGridDokumentInventarizimNgaDB(string komponente, bool gjitheDokumentatHyrje, bool gjitheDokumentatDalje)
        {//mbush griden e popupit me te dhena   
            bool lloj = Request.QueryString["lloj"] == "agj";
            DataTable dt = DbCore.DbRegjistrim.colKokaInventarizim.merrKokaInventarizimDT(DbCore.mySessionObjects.ktheNdermarrjeVit(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), Periudha.DataDokNga,Periudha.DataDokDeri, gjitheDokumentatHyrje, gjitheDokumentatDalje, lloj);
            DbCore.mySessionObjects.ruajGrideNeSession(komponente,(int)hfState["idViti"], Periudha.PeriudhaDok, Session, dt);
            grid_RegInv.DataSource = dt;
            grid_RegInv.DataBind();
            dt.Dispose();
        }

     

        private void konfiguroGride(int idGjuha, int idNdermarrje, int idPerdoruesi, CultureInfo ci, ResourceManager rm, string komponente)
        {
            KonfigurimComboGride.ShtoNivel(grid_RegInv, 135, idNdermarrje, idPerdoruesi, idGjuha, Session, komponente, guidString);
            KonfigurimComboGride.ShtoModel(grid_RegInv, 135, idNdermarrje, idPerdoruesi, idGjuha, Session, komponente, guidString);
            KonfigurimComboGride.ShtoStatus(grid_RegInv, rm, ci);
            KonfigurimComboGride.ShtoMagazinaNdermarrje(grid_RegInv, Session, komponente, guidString, "IdMag");
          
            this.grid_RegInv.Columns["#"].VisibleIndex = 0;

        }


        protected void grid_RegInv_DataBound(object sender, EventArgs e)
        {
            if (this.grid_RegInv.Columns["#"] == null)
            {
                DevExpress.Web.GridViewCommandColumn check = new DevExpress.Web.GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true; check.Width = Unit.Percentage(2);
                // check.SetColVisibleIndex(0);
                grid_RegInv.Settings.ShowFilterRow = true;
                grid_RegInv.Settings.ShowHeaderFilterButton = true;
                grid_RegInv.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                grid_RegInv.Settings.ShowFilterRowMenu = true;
                grid_RegInv.Columns.Add(check);
                grid_RegInv.Settings.ShowGroupPanel = true;
                grid_RegInv.KeyFieldName = "IdKoka";
                grid_RegInv.SettingsBehavior.AllowSelectByRowClick = true;
                grid_RegInv.SettingsBehavior.AllowFocusedRow = true;
            } //this.grid_RegInv.Columns["#"].VisibleIndex = 0;
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
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "grid_RegInv", "RegjistrimInventarizimi.aspx?lloj=agj", idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            //DbCore.DbAdmin.clsFiltraGrida filtra = dbAdmin.merrFiltraGridaSipasFiltraEmri(cmbFiltra.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();
                //mbushComboBoxFiltra();
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "grid_RegInv", int.Parse(cmbKonfigurimi.Value.ToString()), "RegjistrimInventarizimi.aspx?lloj=agj");
                percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);

                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                // konfiguroVleraFillestare();

                   grid_RegInv.FilterExpression = " [IdStatusDok]=1";

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
            //DbCore.DbAdmin.clsGridaKoka koka = dbAdmin.merrGridaKokaByEmri("grid_RegInv", "RegjistrimInventarizim.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "grid_RegInv", "RegjistrimInventarizimi.aspx?lloj=agj", idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = grid_RegInv.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("IdNivel", grid_RegInv);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = grid_RegInv.GetSortedColumns();
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
            //    filtri.KoloneRenditje = "IdNivel";
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
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "grid_RegInv", int.Parse(cmbKonfigurimi.Value.ToString()), "RegjistrimInventarizimi.aspx?lloj=agj");
            percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);

            if (mesazh.Status == true)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

            cmbFiltra.Text = "";

        }
        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (e.Item.Name == "PrintPreview")
            {
                if (grid_RegInv.FocusedRowIndex == -1)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("regjMagMesazhZgjidhniFletePerPrintim", cultinf), pnlMesazhi);
                    Container.Attributes["src"] = "";
                }
                else
                {
                    string id = grid_RegInv.GetRowValues(grid_RegInv.FocusedRowIndex, "IdKoka").ToString();
                    DbCore.DbRegjistrim.clsKokaInventarizim clsKoka = new DbCore.DbRegjistrim.clsKokaInventarizim();
                    clsKoka.mbushKokaInventarizimSipasID(int.Parse(id));
                    DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                    konf.mbushKonfiguriminMeID(clsKoka.IdKonfigAmbjente);
                    //if (konf.KodKonfigAmbjente == "FH" || konf.KodKonfigAmbjente == "FD")
                    //{
                    if(konf.KodKonfigAmbjente=="INAASH")
                    Container.Attributes["src"] = "RaportiShpejte.aspx?Sesioni=false&emriReal=dokumentaInvetarizimiAfatshkurter&idDokumenti=" + clsKoka.IdKoka + "&printo=false" + "&printo=false&raportdyte=jo&iddesign=" + clsKoka.IdRaportDesing.ToString();
                    else
                        Container.Attributes["src"] = "RaportiShpejte.aspx?Sesioni=false&emriReal=dokumentaInvetarizimiAfatgjate&idDokumenti=" + clsKoka.IdKoka + "&printo=false" + "&printo=false&raportdyte=jo&iddesign=" + clsKoka.IdRaportDesing.ToString();
                    //  }
                }

            }
            //else if (e.Item.Name == "Riruaj")
            //{
            //    Riruaj();
            //}

        }


   


        //fshin rreshtat e selektuar
        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            pergjigja.Text = "";
            List<object> rreshtat = grid_RegInv.GetSelectedFieldValues("IdKoka");
            if (rreshtat.Count == 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("regjMagMesazhZgjidhniNje", cultinf), pnlMesazhi);
                return;
            }
            List<string> TeFshire = new List<string>(), TeLidhur = new List<string>(), PeriudheKycur = new List<string>(), GjendjeNegative = new List<string>();
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
               //DbCore.DbAdmin.clsPeriudhaKontabel periudha;
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            foreach (object id in rreshtat)
            {
                DbCore.DbRegjistrim.clsKokaInventarizim kok = new  DbCore.DbRegjistrim.clsKokaInventarizim ();
                kok.IdKoka = Convert.ToInt32(id);
                kok.mbushKokaInventarizimSipasID(kok.IdKoka);
               
                bool lidhur = kok.eshteILidhur();
               
                bool autorizimet = DbCore.DbRegjistrim.clsKokaInventarizim.kaAutorizime(kok.IdKoka, mySessionObjects.ktheIdPerdoruesi(Session));
                if (!autorizimet)
                    lidhur = true;
                if (lidhur)
                {
                    TeLidhur.Add(kok.NrDok);
                    continue;
                }
                if (kok.IdStatusDok == 2)
                    continue;
                //periudha = new DbCore.DbAdmin.clsPeriudhaKontabel(kok.DtDok, idNdermarrje);
                //DbCore.clsMesazh mesazhi = periudha.isPeriudheKycur();
          
                bool ekycur = DbCore.DbAdmin.clsPeriudhaKontabel.eshteKycurPeriudheSipasDateDheNdermarrjes(kok.DtDok, idNdermarrje);
                if (ekycur)
                {
                    PeriudheKycur.Add(kok.NrDok);
                    continue;
                }
              
                kok.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                mesazh = kok.fshi();
                DbCore.mySessionObjects.ruajTrupatNeSession(Session, trupat);
                if (mesazh.Status)
                {
                    #region Heq llogarite nga grida

                    // ASPxGridView_Artikull.DataSource = dt;
                    hiqDokumentInventarizimNgaGrida(kok.IdKoka);
                    #endregion
                    TeFshire.Add(kok.NrDok);
                }
            }
            string mesazhInfoGabimLidhur = "", mesazhInfoGabimPeridheKycur = "", mesazhInfoSukses = "", mesazhGjendjeNegative = "";
            if (TeLidhur.Count == 1)
                mesazhInfoGabimLidhur = String.Format("{0}{1}", rm.GetString("regjMagPrefiksMesazhNjejes", cultinf), rm.GetString("regjMagSuffixMesazhNjejesLidhurGabimi", cultinf));
            else
                if (TeLidhur.Count > 1)
                    mesazhInfoGabimLidhur = String.Format("{0}{1}", rm.GetString("regjMagPrefixMesazhShumes", cultinf), rm.GetString("regjMagSuffixMesazhShumesLidhurGabimi", cultinf));
            if (PeriudheKycur.Count == 1)
                mesazhInfoGabimPeridheKycur = String.Format("{0}{1}{2}", rm.GetString("regjMagPrefiksMesazhNjejes", cultinf), String.Join(";", PeriudheKycur), rm.GetString("regjMagSuffixMesazhNjejesPeriudheKycurGabimi", cultinf));
            else
                if (PeriudheKycur.Count > 1)
                    mesazhInfoGabimPeridheKycur = String.Format("{0}{1}{2}", rm.GetString("regjMagPrefixMesazhShumes", cultinf), String.Join(";", PeriudheKycur), rm.GetString("regjMagSuffixMesazhShumesPeriudheKycurGabimi", cultinf));
            if (GjendjeNegative.Count == 1)
                mesazhGjendjeNegative = String.Format("{0}{1}{2}", rm.GetString("regjMagPrefiksMesazhNjejes", cultinf), String.Join(";", GjendjeNegative), rm.GetString("regjMagSuffixMesazhNjejesGjendjeNegative", cultinf));
            else
                if (GjendjeNegative.Count > 1)
                    mesazhGjendjeNegative = String.Format("{0}{1}{2}", rm.GetString("regjMagPrefixMesazhShumes", cultinf), String.Join(";", GjendjeNegative), rm.GetString("regjMagSuffixMesazhShumesGjendjeNegative", cultinf));
            if (TeFshire.Count == 1)
                mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("regjMagPrefiksMesazhNjejes", cultinf), String.Join(";", TeFshire), rm.GetString("regjMagSuffixMesazhNjejesSuksesi", cultinf));
            else
                if (TeFshire.Count > 1)
                    mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("regjMagPrefixMesazhShumes", cultinf), String.Join(";", TeFshire), rm.GetString("regjMagSuffixMesazhShumesSuksesi", cultinf));

            mesazhInfoGabimLidhur += mesazhInfoGabimPeridheKycur + mesazhGjendjeNegative;
            if (mesazhInfoGabimLidhur != "" && mesazhInfoSukses != "")
            {
                mesazhInfoGabimLidhur += rm.GetString("regjMagLidhesMesazhi", cultinf) + mesazhInfoSukses;
                pergjigja.Text = mesazhInfoGabimLidhur;
                pergjigja.ClientVisible = false;
            }
            else
            {
                pergjigja.Text = mesazhInfoSukses;
                pergjigja.ClientVisible = false;
            }
           
                if (mesazhInfoGabimLidhur != "")
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhInfoGabimLidhur, pnlMesazhi);
                else
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazhInfoSukses, pnlMesazhi);

        }
        private void hiqDokumentInventarizimNgaGrida(int idkoka)
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (this.grid_RegInv.DataSource != null)
            {
                DataTable dt = (DataTable)grid_RegInv.DataSource;
                DataRow[] drs = dt.Select("IdKoka = " + idkoka);
                if (drs.Length > 1)
                    throw new Exception(rm.GetString("regjMagMesazhGabimiNdodhen2DokInventarizim", cultinf));
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                dt.Rows.Remove(dr);
                grid_RegInv.DataSource = dt;
                grid_RegInv.DataBind();
                dt.Dispose();
            }

            else
            {

                mbushGridDokumentInventarizimNgaDB(DbCore.clsFunksione.GetKomponente(Page.Request), Convert.ToBoolean(hfTeDrejtaGjitheDokHyrje.Value), Convert.ToBoolean(hfTeDrejtaGjitheDokDalje.Value));
            }
        }
        protected void grid_RegInv_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
          
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
            
        }

        protected void grid_RegInv_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = grid_RegInv.PageIndex;
            e.Properties["cpPageRow"] = grid_RegInv.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = grid_RegInv.VisibleRowCount;
        }

        protected void grid_RegInv_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (grid_RegInv.AplikoFilterDefault(e, Convert.ToInt32(cmbKonfigurimi.Value)))
                return;


            string[] arr = e.Parameters.Split(';');

            if (!arr.Contains(TitlePeriudha.KeyParamNdryshimPeriudhe))
            {
                int idNdermarrje = (int)hfState["idNdermarrje"];
                int idGjuha = (int)hfState["idGjuha"];
                if (arr.Length == 1) //rasti kur behet callback nga filtri i periudhes duhet te ngelen filtrat ne gride dhe te mos aplikohet filtri default
                    GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "grid_RegInv", grid_RegInv, cmbKonfigurimi.Text.Split(';')[0], "547", idGjuha, false);
                else
                    GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "grid_RegInv", grid_RegInv, cmbKonfigurimi.Text.Split(';')[0], "547", idGjuha);
                if (arr.Length == 2)
                {
                    string periudheDok = DbCore.DbShare.clsAlternativaKushti.getAlternativa(Convert.ToInt32(cmbKonfigurimi.Value), "SHDPER");
                    string datanga, dataderi;
                    clsFunksione.ruajPeriudhatNeHiddenField(IsPostBack, hfState, periudheDok, null, out datanga, out dataderi);
                    mbushGridDokumentInventarizimNgaSession(DbCore.clsFunksione.GetKomponente(Page.Request));
                }
                if (arr.Length == 3)
                {
                    if (arr[2] == "")
                    {
                        grid_RegInv.FilterExpression = " [IdStatusDok]=1";
                    }
                    else
                    {
                        //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
                        DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                        //filtra.mbushFiltraGridaSipasFiltraKodi(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                        DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "grid_RegInv", "RegjistrimInventarizimi.aspx?lloj=agj", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), int.Parse(cmbKonfigurimi.Value.ToString()));
                        filtra.mbushFilterPerGrideSipasKodit(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
                        //DbCore.DbAdmin.clsFiltraGrida filtra = dbAdmin.merrFiltraGridaSipasFiltraKodi(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                        if (filtra.FiltraKodi != null)
                        {
                            grid_RegInv.FilterExpression = filtra.FiltraVlera;
                            GridUtil.renditGriden(filtra.KoloneRenditje, grid_RegInv);
                        }
                    }
                }
            }
            grid_RegInv.Selection.UnselectAll();
            this.grid_RegInv.Columns["#"].VisibleIndex = 0;
        }

        protected void grid_RegInv_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if (e.Column.FieldName == "IdMag" || e.Column.FieldName == "IdKonfigAmbjente")
                if (Converter.ConvertToInt(e.Value)==0)
                {
                    e.Criteria = null;
                }
        }

        protected void grid_RegInv_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            string TeGjithe = rm.GetString("GridHeaderFilterFillItemTeGjithe", ci);
            string nga = rm.GetString("GridHeaderFilterFillItemNga", ci);
            if (e.Column.FieldName == "NrDok" || e.Column.FieldName == "Pershkrim")
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


    }
}