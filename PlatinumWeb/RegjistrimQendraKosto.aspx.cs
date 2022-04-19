using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Web;
using System.Data;
using DbCore.DbRegjistrim;
using PlatinumWeb.Templates;
using System.Resources;
using System.Globalization;
using DbCore;
using DbCore.DbShare;
using DbCore.DbAdmin;
using DbCore.DbQendraKosto;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Types;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;

namespace PlatinumWeb
{

    /// <summary>
    /// nderfaqja e RegjistrimQendraKostot 
    /// </summary>
    public partial class RegjistrimQendraKosto : MyPageBase
    {

        /// <summary>
        /// id e ndermarjes
        /// </summary>
        private int idndermarje;
        /// <summary>
        /// id e perdoruesit
        /// </summary>
        private int idperdoruesi;
        /// <summary>
        /// id e ndermarje vitit
        /// </summary>
        private int idnderviti;
        private int idviti;
        //private int idgjuha;
        string veprimi = "qendraKosto";
        /// <summary>
        /// vendos themen e faqes
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        private string komponente = "RegjistrimQendraKosto.aspx";
        private string guidString;

        /// <summary>
        /// kur lodohet faqja  mbushet me te dhena dhe kontrollohen ne eshte i autorizuar perdoruesi
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            int idPerdoruesi;
            int idGjuha;
            int idNdermarrje;
            int idViti;
            if (hfState.Count == 0)
            {
                idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
                idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
                hfState.Set("idPerdoruesi", idPerdoruesi);
                hfState.Set("idGjuha", idGjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("idViti", idViti);
                guidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                hfState.Set("guidString", guidString);
            }
            else
            {
                idPerdoruesi = (int)hfState["idPerdoruesi"];
                idGjuha = (int)hfState["idGjuha"];
                idNdermarrje = (int)hfState["idNdermarrje"];
                idViti = (int)hfState["idViti"];
                guidString = (string)hfState["guidString"];
            }
            System.Globalization.CultureInfo ci = DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo(idGjuha);
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            System.Threading.Thread.CurrentThread.CurrentUICulture = ci;

            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                DbCore.clsFunksione.logout(Session,true,"FaqePaautorizuar");
            }
            idperdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idperdoruesi);
            }
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            idndermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            idnderviti = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
            idviti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            percaktoTemplateMenu((int)hfState["idGjuha"], ASPxMenu1, idviti, idperdoruesi, idndermarje);
            if (!IsPostBack)
            {
                vendosHfMePerkthime(rm, ci);
                EmratEKontrolleve(rm, ci);
                ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idperdoruesi, idndermarje, cmbKonfigurimi, 76, "LDRQK", rm, ci, idGjuha);
                cmbKonfigurimi.SelectedIndex = 0;
                DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()), idGjuha);
                hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idperdoruesi, idndermarje, idviti, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaDokumenta = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaDokumenta.merrTeDrejtaPerKeteKomponente(idperdoruesi, idndermarje, idviti, komponente);
                hfTeDrejtaGjitheDok.Value = tedrejtaDokumenta.DGjitheDok.ToString();

                if (tedrejtaDokumenta.DGjitheDok == true)
                    mbushGridNgaDB(true, idNdermarrje, idperdoruesi, idnderviti);
                else
                    mbushGridNgaDB(false, idNdermarrje, idperdoruesi, idnderviti);
                if (gvRegjQK.FilterExpression == "")
                    gvRegjQK.FilterExpression = "[IdStatusDok]=1";
                konfiguroGride(rm, ci, idNdermarrje, idperdoruesi, idGjuha);
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idndermarje, "gvRegjQK", gvRegjQK, cmbKonfigurimi.Text.Split(';')[0], "907", (int)hfState["idGjuha"]);
                if (Request.QueryString["fshi"] == "po")
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("regjisDokMesazhSukesFshirjeDokument", ci), pnlMesazhi);
                else if (Request.QueryString["ruaj"] == "po")
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("regjMagModifikimiPerfundoiMeSukses", ci), pnlMesazhi);
            }
            else
            {
                mbushGridNgaSession(idndermarje, idperdoruesi, idnderviti);
                konfiguroGride(rm, ci, idNdermarrje, idperdoruesi, idGjuha);
            }
            AspxWebControlUtils.perkthePopUp(popFshi, rm.GetString("labelKujdes", ci), lblMsgbox, rm.GetString("labelAdministrimiMsgJeniSigurt", ci), ButtonCancel, rm.GetString("labelAnullo", ci));
            GridUtil.konfigGrideListeEMadhePaTheme(gvRegjQK, "IdKoka");
            hfVeprimi.Value = veprimi;
            clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idndermarje, "gvRegjQK", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
            GridUtil.ToolTipButonaveMbiGride(gvRegjQK, ci, rm);
            if (Request.QueryString["indexrow"] != null)
            {
                gvRegjQK.FocusedRowIndex = int.Parse(Request.QueryString["indexrow"]);
            }
            Container.Attributes["src"] = "";
            gvRegjQK.Columns["#"].VisibleIndex = 0;
            GridUtil.ToolTipButonaveMbiGride(gvRegjQK, ci, rm);
        }

        private void vendosHfMePerkthime(ResourceManager rm, CultureInfo cultinf)
        {
            hfState.Set("msgZgjdhniNjeNgaElementetEListes", rm.GetString("msgZgjdhniNjeNgaElementetEListes", cultinf));
            hfState.Set("regjisDokNukKeniAsnjeDokTeZgjedhur", rm.GetString("regjisDokNukKeniAsnjeDokTeZgjedhur", cultinf));
            hfState.Set("msgNukMunTeKlononi1QKTeGjeneruar", rm.GetString("msgNukMunTeKlononi1QKTeGjeneruar", cultinf));
        }

        private void EmratEKontrolleve(ResourceManager rm, CultureInfo cultinf)
        {
            konfigurimi_Label.Text = rm.GetString("lblModeli", cultinf);
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
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, komponente, this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));

        }

        /// <summary>
        /// ruan konfigurimin e grides dhe filtrin e zgjedhur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RuajKolona_Click(object sender, EventArgs e)
        {
            int idfiltri = 0;
            int idkonf = DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(cmbKonfigurimi.Text.Split(';')[0], (int)hfState["idNdermarrje"]);
            DbCore.clsMesazh mesazh = GridUtil.ruajFiltra((int)hfState["idNdermarrje"], (int)hfState["idPerdoruesi"], (int)hfState["idGjuha"], "gvRegjQK", komponente, "FilterDefault", gvRegjQK.FilterExpression, gvRegjQK, "IdNivel", idkonf, out idfiltri);//ruan filtrin e zgjedhur tek filtrat
            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                return;

            }
            mesazh = GridUtil.ruajkonfigurimgride(gvRegjQK, cmbKonfigurimi.Text, (int)hfState["idNdermarrje"], (int)hfState["idPerdoruesi"], 907, idfiltri, (int)hfState["idViti"], DbCore.mySessionObjects.ktheCultureInfo(Session), DbCore.mySessionObjects.ktheGjuhe(Session));/// ruan konfigurimin e grides dhe filtrin

            clsToolbarConfig.mbushComboBoxFiltra((int)hfState["idGjuha"], (int)hfState["idNdermarrje"], "gvRegjQK", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
            int idViti = (int)hfState["idViti"];
            percaktoTemplateMenu((int)hfState["idGjuha"], ASPxMenu1, idViti, (int)hfState["idPerdoruesi"], (int)hfState["idNdermarrje"]);

            if (mesazh.Status) clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

        }
        protected void btnXlsxExport_Click(object sender, EventArgs e)
        {
            try
            {
                gridExport.WriteXlsxToResponse("faturat", true);
            }
            catch (Exception)
            {
            }
        }
        protected void btnPdfExport_Click(object sender, EventArgs e)
        {
            try
            {
                gridExport.WritePdfToResponse("faturat", true);
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu((int)hfState["idGjuha"], ASPxMenu1, idviti, idperdoruesi, idndermarje);
        }

        /// <summary>
        /// mbush griden me te dhena te ruajtura ne sesion
        /// </summary>
        /// <param name="idNdermarrje"></param>
        private void mbushGridNgaSession(int idNdermarrje, int idPerdoruesi, int idnderviti)
        {
            DataTable tmpObject;
            bool sukses = DbCore.mySessionObjects.merrGrideNgaSessioni(Session, out tmpObject);
            if (!sukses)
            {
                if (hfTeDrejtaGjitheDok.Value.ToString() == "True" || hfTeDrejtaGjitheDok.Value.ToString() == "true")
                    mbushGridNgaDB(true, idNdermarrje, idPerdoruesi, idnderviti);
                else
                    mbushGridNgaDB(false, idNdermarrje, idPerdoruesi, idnderviti);
            }
            else
            {
                gvRegjQK.DataSource = tmpObject;
                if (!IsCallback && Request["__CALLBACKID"] == "gvRegjQK")
                    gvRegjQK.RestoreFilter(idNdermarrje, veprimi, string.Empty);
                gvRegjQK.DataBind();
                gvRegjQK.SaveFilter(idNdermarrje, veprimi);
                tmpObject.Dispose();
            }
        }

        /// <summary>
        /// mbush griden me te dhena nga db
        /// <param name="gjitheDokumentat">true nqs perdoruesi ka te drejta t'i shohe te gjitha dokumentat, false nqs mund te shohe vetem dokumentat e veta</param>
        /// </summary>
        private void mbushGridNgaDB(bool gjitheDokumentat, int idNdermarrje, int idperdorues, int idnderviti)
        {//mbush griden e popupit me te dhena            
            DataTable dt = new DataTable();
            dt = DbCore.DbQendraKosto.colKokaQendraKosto.merrKokaQendraKostoDT(idnderviti, idperdorues);
            DbCore.mySessionObjects.ruajGrideNeSession(Session, dt);
            gvRegjQK.DataSource = dt;
            gvRegjQK.DataBind();
            dt.Dispose();
            gvRegjQK.RestoreFilter(idNdermarrje, veprimi, string.Empty);
        }

        /// <summary>
        /// konfiguron griden
        /// </summary>
        private void konfiguroGride(System.Resources.ResourceManager rm, System.Globalization.CultureInfo ci, int idNdermarrje, int idPerdorues, int idGjuha)
        {
            KonfigurimComboGride.ShtoStatus(gvRegjQK, rm, ci);
            KonfigurimComboGride.ShtoNivel(gvRegjQK, 75, idNdermarrje, idPerdorues, idGjuha, Session, komponente, guidString);
            KonfigurimComboGride.ShtoModel(gvRegjQK, 75, idNdermarrje, idPerdorues, idGjuha, Session, komponente, guidString);

        }


        /// <summary>
        /// kur grida ben databound per ti shtuar kolonen e selektimit
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvRegjQK_DataBound(object sender, EventArgs e)
        {
            if (gvRegjQK.Columns["#"] == null)
            {
                GridViewCommandColumn check = new GridViewCommandColumn("#") { ShowSelectCheckbox = true, Width = System.Web.UI.WebControls.Unit.Percentage(2) };
                gvRegjQK.Settings.ShowFilterRow = true;
                gvRegjQK.Settings.ShowHeaderFilterButton = true;
                gvRegjQK.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                gvRegjQK.Settings.ShowFilterRowMenu = true;
                gvRegjQK.Columns.Add(check);
                gvRegjQK.Settings.ShowGroupPanel = true;
                gvRegjQK.KeyFieldName = "IdKoka";
                gvRegjQK.SettingsBehavior.AllowSelectByRowClick = true;
                gvRegjQK.SettingsBehavior.AllowFocusedRow = true;
            }
            //SaveFilter();
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
            MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;

            DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
            //filtra.mbushFiltraGridaSipasFiltraEmri(cmbFiltra.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvRegjQK", komponente, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), int.Parse(cmbKonfigurimi.Value.ToString()));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                filtra.IdPerdoruesi = idperdoruesi;
                mesazh = filtra.fshi();
                //mbushComboBoxFiltra();
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idndermarje, "gvRegjQK", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
                percaktoTemplateMenu((int)hfState["idGjuha"], ASPxMenu1, idviti, idperdoruesi, idndermarje);
                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                gvRegjQK.FilterExpression = " [IdStatusDok]=1 ";


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
            MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvRegjQK", komponente, idndermarje, int.Parse(cmbKonfigurimi.Value.ToString()));
            DbCore.DbAdmin.clsFiltraGrida filtri = new DbCore.DbAdmin.clsFiltraGrida() { FiltraKodi = cmbFiltra.Text, FiltraShenime = cmbFiltra.Text, FiltraUniversal = false, GridaKokaId = koka.IdGridaKoka, FiltraVlera = gvRegjQK.FilterExpression, IdPerdoruesi = idperdoruesi, IdNdermarje = idndermarje, IdStatusDok = 1 };
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("IdNivel", gvRegjQK);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvRegjQK.GetSortedColumns();
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

            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            mesazh = filtri.ruaj();
            //mbushComboBoxFiltra();
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idndermarje, "gvRegjQK", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
            percaktoTemplateMenu((int)hfState["idGjuha"], ASPxMenu1, idviti, idperdoruesi, idndermarje);
            if (mesazh.Status == true)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

            cmbFiltra.Text = "";
        }

        /// <summary>
        /// eventet e menuse
        /// </summary>
        /// <param name="source">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {
            //int idGjuha = mySessionObjects.ktheGjuhePerdoruesi(Session);
            //CultureInfo cultinf = DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo(idGjuha);
            //ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            //if (e.Item.Name == "Rivleresim")
            //    Riruaj(cultinf, rm, idGjuha);

        }

        //private void Riruaj(CultureInfo cultinf, ResourceManager rm, int idGjuha)
        //{
        //    DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
        //    List<string> TeRuajtur = new List<string>(), Teparuajtur = new List<string>();
        //    pergjigja.Text = "";
        //    List<object> rreshtat = this.gvRegjQK.GetSelectedFieldValues("IdKoka");
        //    pergjigja.Text = "";
        //    DataTable err = new DataTable();
        //    err.Columns.Add("Kodi");
        //    err.Columns.Add("Gabimi");
        //    err.Columns.Add("Rreshti");
        //    int nrreshta = 0;

        //    if (new DbCore.DbQendraKosto.colLlogariShperndarjeQK(idndermarje).Count != 0) 
        //    foreach (object id in rreshtat)
        //    {
        //        nrreshta++;
        //        DbCore.DbQendraKosto.clsKokaQendraKosto clsKoka = new DbCore.DbQendraKosto.clsKokaQendraKosto(Convert.ToInt32(id));
        //        if (clsKoka.IdStatusDok == 2)
        //            continue;
        //        string shfaqmesazhapolupe = "";
        //        DbCore.DbKontabiliteti.clsKokaFleteKontabel kokafl = new DbCore.DbKontabiliteti.clsKokaFleteKontabel(clsKoka.IdGjenerues);
        //        DbCore.DbKontabiliteti.colTrupatFletetKontabel coltrupfk = new DbCore.DbKontabiliteti.colTrupatFletetKontabel(kokafl.IdKokaFleteKontabel);
        //        DbCore.DbQendraKosto.colTrupiQendraKosto coltrup = new DbCore.DbQendraKosto.colTrupiQendraKosto(clsKoka.IdKoka);
        //        int iddegeadm = 0;
        //        switch (kokafl.IdKategoria)
        //        {
        //            case 1:
        //            case 2:
        //                DbCore.DbRegjistrim.clsKokaShitje koksh = new clsKokaShitje();
        //                koksh.mbushKokaShitjeSipasIDPaTrup(kokafl.IdGjenerues);
        //                iddegeadm = koksh.IdDegeAdministrative;
        //                break;
        //            case 6:
        //                clsKokaMagazina kokam = new clsKokaMagazina();
        //                kokam.mbushKokaMagazinaSipasID(kokafl.IdGjenerues);
        //                iddegeadm = kokam.IdDegeAdministrative;
        //                break;

        //            case 3:
        //            case 4:
        //                DbCore.DbArkaBanka.clsVeprimBankaKoka kokab = new DbCore.DbArkaBanka.clsVeprimBankaKoka(kokafl.IdGjenerues);
        //                iddegeadm = kokab.IdDegeAdministrative;
        //                break;
        //            case 78:
        //                clsKokaRezervime kokar = new clsKokaRezervime(kokafl.IdGjenerues);
        //                iddegeadm = kokar.IdDegeAdministrative;
        //                break;
        //            case 95:
        //                clsKokaNdryshimCmimSasi kokanc = new clsKokaNdryshimCmimSasi();
        //                kokanc.mbushKokaNdryshimCmimSasiSipasID(kokafl.IdGjenerues);
        //                iddegeadm = kokanc.IdDegeAdministrative;
        //                break;

        //            default:
        //                iddegeadm = 0;
        //                break;

        //        }
        //        DbCore.DbQendraKosto.colObjektivaKosto objektivat = new DbCore.DbQendraKosto.colObjektivaKosto();
        //        List<double> vleratobjektiva = new List<double>();
        //        List<double> vleratobjektivamonbaze = new List<double>();
        //        List<int> idllogobj = new List<int>();
        //        foreach (DbCore.DbKontabiliteti.clsTrupiFleteKontabel trup in coltrupfk)
        //        {
        //            DbCore.DbQendraKosto.clsObjektivaKosto objekt = new DbCore.DbQendraKosto.clsObjektivaKosto();
        //            DbCore.DbKontabiliteti.clsLlogari llog = new DbCore.DbKontabiliteti.clsLlogari(trup.IdLlogari);
        //            if (llog.IdObjektivaKosto != 0 && llog.IdObjektivaKosto != -1)
        //            {
        //                objekt = new DbCore.DbQendraKosto.clsObjektivaKosto(llog.IdObjektivaKosto);

        //            }
        //            if (objekt.Id != 0 && objekt.Id != -1)
        //            {
        //                DbCore.DbKontabiliteti.clsKokaFleteKontabel.shtoTeDhenaPeObjektivat(clsKoka.DtDok, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, llog, trup, objekt);
        //            }
        //        }
        //        clsKonfigurimAmbjenti konfig = new clsKonfigurimAmbjenti(clsKoka.IdKonfigAmbjente);
        //        DbCore.DbQendraKosto.clsKokaQendraKosto kokaqender = DbCore.DbQendraKosto.clsKokaQendraKosto.krijoQKShitjeQuick(konfig.IdNivel, konfig.IdKonfigAmbjente, clsKoka.NrRef, clsKoka.DtDok, clsKoka.NrDok, clsKoka.IdKoka, 1, clsKoka.IdNdermarrje, clsKoka.IdNdermarrjeVit, idperdoruesi, clsKoka.DtRegj, clsKoka.Pershkrimi, kokafl.IdNivel, kokafl.IdKonfigAmbjente, kokafl.IdKokaFleteKontabel, coltrupfk, iddegeadm, 0, 0, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, out  shfaqmesazhapolupe, coltrup);
        //        kokaqender.IdKoka = clsKoka.IdKoka;
        //        mesazh = kokaqender.modifiko(false);
        //        if (!mesazh.Status)
        //        {
        //            object[] arr = { konfig.KodKonfigAmbjente + " " + clsKoka.NrDok + " " + clsKoka.DtDok.ToShortDateString(), mesazh.PershkrimMesazhi, nrreshta };
        //            err.Rows.Add(arr);
        //            continue;
        //        }
        //    }
        //    else
        //        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Nuk u rillogarit asnje rresht!", pnlMesazhi);
        //    DbCore.mySessionObjects.ruajTabeleGabimeshImporti(Session, err);
        //    if (err.Rows.Count > 0)
        //    {
        //        clsKokaErrorImporti koka = new clsKokaErrorImporti(0, "Nga rillogaritja e qendrave te kostos", 75, idndermarje, idperdoruesi);
        //        koka.ColTrupi.mbushErrorImportiNgaProgrami(err);
        //        mesazh = koka.ruajErrorImporti();
        //        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "U rillogariten " + (rreshtat.Count - err.Rows.Count) + " rreshta dhe deshtuan " + err.Rows.Count + " rreshta! ", pnlMesazhi);
        //        Container.Attributes["src"] = "RaportiShpejte.aspx?Sesioni=false&idraporti=104&printo=false&db=jo";
        //    }
        //    else
        //        clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "U rillogariten te gjitha rreshtat!", pnlMesazhi);
        //    this.gvRegjQK.Selection.UnselectAll();
        //    bool teDrejtaGjitheDok = hfTeDrejtaGjitheDok.Value.ToString() == "True" || hfTeDrejtaGjitheDok.Value.ToString() == "true";
        //    mbushGridNgaDB(teDrejtaGjitheDok, idndermarje, idperdoruesi, idnderviti);
        //    konfiguroGride(rm, cultinf, idndermarje, idperdoruesi, idGjuha);
        //    if (rreshtat.Count == 0)
        //        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("regjMagMesazhZgjidhniNje", cultinf), pnlMesazhi);
        //}

        /// <summary>
        /// fshin rreshtat e selektuar
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            List<string> TeFshire = new List<string>(), TeLidhur = new List<string>(), PeriudheKycur = new List<string>();
            pergjigja.Text = "";
            DbCore.DbRegjistrim.colTrupiMagazina tr = new DbCore.DbRegjistrim.colTrupiMagazina();
            DbCore.DbRegjistrim.colTrupiMagazina trupat = new DbCore.DbRegjistrim.colTrupiMagazina();
            List<object> rreshtat = gvRegjQK.GetSelectedFieldValues("IdKoka");
            pergjigja.Text = "";
            //DbCore.DbAdmin.clsPeriudhaKontabel periudha;
            DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            foreach (object id in rreshtat)
            {
                DbCore.DbQendraKosto.clsKokaQendraKosto clsKoka = new DbCore.DbQendraKosto.clsKokaQendraKosto(Convert.ToInt32(id));
                bool lidhur = dbAdmin.eshteDokumentiILidhur(clsKoka.IdKoka, clsKoka.IdNivel, "T_KOKAQENDRAKOSTO", "IDKOKA");
                if (lidhur)
                {
                    TeLidhur.Add(clsKoka.NrDok);
                    continue;
                }

                if (clsKoka.IdStatusDok == 2)
                    continue;
                //periudha = new DbCore.DbAdmin.clsPeriudhaKontabel(clsKoka.DtDok, idndermarje);
               // DbCore.clsMesazh mesazhi = periudha.isPeriudheKycur();
              
                bool ekycur = DbCore.DbAdmin.clsPeriudhaKontabel.eshteKycurPeriudheSipasDateDheNdermarrjes(clsKoka.DtDok, idndermarje);
                if (ekycur)
                {
                    PeriudheKycur.Add(clsKoka.NrDok);
                    continue;
                }

                int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                mesazh = clsKoka.Fshi(idPerdoruesi,2);
                DbCore.mySessionObjects.ruajTrupatNeSession(Session, trupat);
                if (mesazh.Status)
                {
                    hiqNgaGrida(clsKoka.IdKoka, rm, ci, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), idPerdoruesi, DbCore.mySessionObjects.ktheNdermarrjeVit(Session));
                    TeFshire.Add(clsKoka.NrDok);
                }
            }
            dbAdmin.Dispose();
            string mesazhInfoGabimLidhur = "", mesazhInfoGabimPeridheKycur = "", mesazhInfoSukses = "", mesazhGjendjeNegative = "", mesazhProcesAprovimi = "";

            if (TeLidhur.Count == 1)
                mesazhInfoGabimLidhur = String.Format("{0}{1}{2}", rm.GetString("regjisDokPrefixMesazhNjejes", ci), String.Join(";", TeLidhur), rm.GetString("msgListaLidhjaSuffixNjejesGabimi", ci));
            else
                if (TeLidhur.Count > 1)
                    mesazhInfoGabimLidhur = String.Format("{0}{1}{2}", rm.GetString("regjisDokprefixMesazhShumes", ci), String.Join(";", TeLidhur), rm.GetString("regjMagSuffixMesazhShumesLidhurGabimi", ci));
            if (PeriudheKycur.Count == 1)
                mesazhInfoGabimPeridheKycur = String.Format("{0}{1}{2}", rm.GetString("regjisDokPrefixMesazhNjejes", ci), String.Join(";", PeriudheKycur), rm.GetString("suffixMesazhNjejesPeriudheKycurGabimi", ci));
            else
                if (PeriudheKycur.Count > 1)
                    mesazhInfoGabimPeridheKycur = String.Format("{0}{1}{2}", rm.GetString("regjisDokprefixMesazhShumes", ci), String.Join(";", PeriudheKycur), rm.GetString("suffixMesazhShumesPeriudheKycurGabimi", ci));

            if (TeFshire.Count == 1)
                mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("regjisDokPrefixMesazhNjejes", ci), String.Join(";", TeFshire), rm.GetString("msgCeljeMagazinatSuffixNjejesSuksesi", ci));
            else
                if (TeFshire.Count > 1)
                    mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("regjisDokprefixMesazhShumes", ci), String.Join(";", TeFshire), rm.GetString("regjMagSuffixMesazhShumesSuksesi", ci));

            mesazhInfoGabimLidhur += mesazhInfoGabimPeridheKycur + mesazhGjendjeNegative + mesazhProcesAprovimi;
            if (mesazhInfoGabimLidhur != "" && mesazhInfoSukses != "")
            {
                mesazhInfoGabimLidhur += rm.GetString("msgLidhesMesazhi", ci) + mesazhInfoSukses;
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
                if (mesazhInfoGabimLidhur != "")
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhInfoGabimLidhur, pnlMesazhi);
                else
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazhInfoSukses, pnlMesazhi);

        }

        /// <summary>
        /// heq nga grida reshtin e fshire
        /// </summary>
        /// <param name="idkoka">id e reshtit te fshire</param>
        private void hiqNgaGrida(int idkoka, System.Resources.ResourceManager rm, System.Globalization.CultureInfo ci, int IdNdermarrje, int idPerdoruesi, int idNderVit)
        {
            if (gvRegjQK.DataSource != null)
            {
                DataTable dt = (DataTable)gvRegjQK.DataSource;
                DataRow[] drs = dt.Select("IdKoka = " + idkoka);
                if (drs.Length > 1)
                    throw new DbCore.MyException(rm.GetString("regjisDokNdodhen2DokMeTeNjejtenID", ci));
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                dt.Rows.Remove(dr);
                gvRegjQK.DataSource = dt;
                gvRegjQK.DataBind();
                dt.Dispose();
            }
            else
            {
                if (hfTeDrejtaGjitheDok.Value.ToString() == "True" || hfTeDrejtaGjitheDok.Value.ToString() == "true")
                    mbushGridNgaDB(true, IdNdermarrje, idPerdoruesi, idNderVit);
                else mbushGridNgaDB(false, IdNdermarrje, idPerdoruesi, idNderVit);
            }
        }

        /// <summary>
        /// kur grida ben callback
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvRegjQK_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            if (e.CallbackName == "COLUMNMOVE" && gvRegjQK.AllColumns[int.Parse(e.Args[0])].Width.Value == 0)
                gvRegjQK.AllColumns[int.Parse(e.Args[0])].Width = System.Web.UI.WebControls.Unit.Percentage(3);
            if (e.CallbackName == "APPLYFILTER" && e.Args[0] == "")
            {
                MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
                ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
                cmbFiltra.Text = "";
            }
            gvRegjQK.SaveFilter(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), veprimi);
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            GridUtil.ToolTipButonaveMbiGride(gvRegjQK, cultinf, rm);
            // konfiguroGride();
        }

        /// <summary>
        /// kur perdoruesi i ben grides callbak per te mare konfigurimin e ri
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvRegjQK_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string[] arr = e.Parameters.Split(';');
            GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), "gvRegjQK", gvRegjQK, cmbKonfigurimi.Text.Split(';')[0], "907", DbCore.mySessionObjects.ktheGjuhe(Session));
            if (arr.Length == 3)
            {
                if (arr[2] == "")
                    gvRegjQK.FilterExpression = " [IdStatusDok]=1 ";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvRegjQK", komponente, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), int.Parse(cmbKonfigurimi.Value.ToString()));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        gvRegjQK.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvRegjQK);
                    }
                }
            }


            gvRegjQK.Selection.UnselectAll();
        }

        /// <summary>
        /// perdoret per te vendosur karakteristika te griden ne javascript
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvRegjQK_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gvRegjQK.PageIndex;
            e.Properties["cpPageRow"] = gvRegjQK.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gvRegjQK.VisibleRowCount;
        }

        /// <summary>
        /// perdoret per te hequr filtrimin e grides kur zgjedhim elementin bosh
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvRegjQK_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if (e.Column.FieldName == "IdKonfigAmbjente")
                if (Converter.ConvertToInt(e.Value)==0)
                {
                    e.Criteria = null;
                }

        }

        /// <summary>
        /// perdoret per te shtuar menyra filtrimi
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvRegjQK_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            string TeGjithe = rm.GetString("GridHeaderFilterFillItemTeGjithe", ci);
            string nga = rm.GetString("GridHeaderFilterFillItemNga", ci);
            if (e.Column.FieldName == "NrDok" || e.Column.FieldName == "Pershkrimi")
            {
                e.Values.Clear();
                e.AddValue(TeGjithe, string.Empty, "true");
                e.AddValue(nga + " A-D ", string.Empty, String.Format("{0}>'A     ' and {0} <'DDDDDDD'", e.Column.FieldName));
                e.AddValue(nga + " D-G ", string.Empty, String.Format("{0}>'D     ' and {0}<'GGGGGGG'", e.Column.FieldName));
                e.AddValue(nga + " H-K ", string.Empty, String.Format("{0}>'H     ' and {0}<'KKKKKKK'", e.Column.FieldName));
                e.AddValue(nga + " L-O ", string.Empty, String.Format("{0}>'L     ' and {0}  <'OOOOOOO'", e.Column.FieldName));
                e.AddValue(nga + " P-S ", string.Empty, String.Format("{0}>'P     ' and {0}<'SSSSSSS'", e.Column.FieldName));
                e.AddValue(nga + " T-W ", string.Empty, String.Format("{0}>'T     ' and {0}<'WWWWWWW'", e.Column.FieldName));
                e.AddValue(nga + " X-Z ", string.Empty, String.Format("{0}>'X     ' and {0}<'ZZZZZZZ'", e.Column.FieldName));
            }
            else
            {
                e.Values.Clear();
                e.AddValue(TeGjithe, string.Empty, "true");
            }
        }

        protected void ProgressBar1_RunTask(object sender, EO.Web.ProgressTaskEventArgs e)
        {
            try
            {
                int idGjuha = mySessionObjects.ktheGjuhe(Session);
                CultureInfo cultinf = DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo(idGjuha);
                ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));


                int position = 0;
                e.UpdateProgress(position);

                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                List<string> TeRuajtur = new List<string>(), Teparuajtur = new List<string>();
                pergjigja.Text = "";

                List<object> rreshtat = this.gvRegjQK.GetSelectedFieldValues("IdKoka");
                pergjigja.Text = "";

                DataTable err = new DataTable();
                err.Columns.Add("Kodi");
                err.Columns.Add("Gabimi");
                err.Columns.Add("Rreshti");
                int max = rreshtat.Count;
                System.Threading.Thread.CurrentThread.Priority = System.Threading.ThreadPriority.Lowest;
                ProgressBar1.Maximum = max;
                using (DbData dbData = new clsDatabaseQendraKosto())
                {

                    for (int nrreshta = 0; nrreshta < max; nrreshta++)
                    {
                        clsKokaQendraKosto clsKoka = new clsKokaQendraKosto(Convert.ToInt32(rreshtat[nrreshta]), new clsDatabaseQendraKosto(dbData));
                        DbCore.DbShare.clsKonfigurimAmbjenti konfig = new clsKonfigurimAmbjenti(clsKoka.IdKonfigAmbjente,new clsDatabaseShare(dbData));
                        mesazh = clsKoka.rillogarit(idperdoruesi, konfig,2,new clsDatabaseQendraKosto(dbData));
                        if (!mesazh.Status)
                        {
                            object[] arr = { konfig.KodKonfigAmbjente + " " + clsKoka.NrDok + " " + clsKoka.DtDok.ToShortDateString(), mesazh.PershkrimMesazhi, nrreshta };
                            err.Rows.Add(arr);
                            e.UpdateProgress(e.Value, string.Format("dokumenti me numer : {0} nuk u rillogarit ", clsKoka.NrDok));
                            continue;
                        }
                        if (e.IsStopped)
                        {
                            System.Threading.Thread.CurrentThread.Priority = System.Threading.ThreadPriority.Normal;
                            return;
                        }
                        position++;
                        int roundedPosition = (int)Math.Round((double)((position / (double)max) * 100));
                        if (e.Value != roundedPosition)
                            e.UpdateProgress(roundedPosition, string.Format("U rillogarit dokumenti me numer :{0}", clsKoka.NrDok));
                    }
                }
                System.Threading.Thread.CurrentThread.Priority = System.Threading.ThreadPriority.Normal;
                DbCore.mySessionObjects.ruajTabeleGabimeshImporti(Session, err);
                if (err.Rows.Count > 0)
                {
                    clsKokaErrorImporti koka;

                    koka = new clsKokaErrorImporti(0, "Nga rillogaritja e qendrave te kostos", 75, idndermarje, idperdoruesi);
                    koka.ColTrupi.mbushErrorImportiNgaProgrami(err);
                    mesazh = koka.ruajErrorImporti();
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "U rillogariten " + (rreshtat.Count - err.Rows.Count) + " rreshta dhe deshtuan " + err.Rows.Count + " rreshta! ", pnlMesazhi);
                    Container.Attributes["src"] = "RaportiShpejte.aspx?Sesioni=false&emriReal=gabimeImporti&printo=false&db=jo";
                }
                else
                {
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "U rillogariten te gjitha rreshtat!", pnlMesazhi);

                }
               // this.gvRegjQK.Selection.UnselectAll();
                if (hfTeDrejtaGjitheDok.Value.ToString() == "True" || hfTeDrejtaGjitheDok.Value.ToString() == "true")
                    mbushGridNgaDB(true, idndermarje, idperdoruesi, idnderviti);
                else mbushGridNgaDB(false, idndermarje, idperdoruesi, idnderviti);
              //  konfiguroGride(rm, cultinf, idndermarje, idperdoruesi, idGjuha);
            }
            catch (Exception err)
            {
                string mesazhi = "Gabim i panjohur gjate rillogaritjes";
                NLog.LogManager.GetCurrentClassLogger().Error(mesazhi, err.Message);
                System.Threading.Thread.CurrentThread.Priority = System.Threading.ThreadPriority.Normal;
                e.UpdateProgress(e.Value, mesazhi);                
            }

        }

    }
}