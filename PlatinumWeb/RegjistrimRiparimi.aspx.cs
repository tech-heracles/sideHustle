using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Web;
using System.Data;
using DbCore.DbRegjistrim;
using PlatinumWeb.Templates;
using System.Collections;
using DbCore.DbInventari;
using System.Globalization;
using System.Resources;
using DbCore;
using DbCore.DbShare;
using DbCore.IMBUtils.Types;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;

namespace PlatinumWeb
{

    /// <summary>
    /// nderfaqja e RegjistrimRiparimi
    /// </summary>
    public partial class RegjistrimRiparimi : MyPageBase
    {
        /// <summary>
        /// konstante per mesazhin e fshirjes ne njejes
        /// </summary>
        private const string prefixMesazhNjejes = "Dokumenti  me Nr: ";
        /// <summary>
        /// konstante per mesazhin e fshirjes ne shumes
        /// </summary>
        private const string prefixMesazhShumes = "Dokumentat  me Nr: ";
        /// <summary>
        /// konstante per mesazhin e mos fshirjes ne njejes per lidhjen
        /// </summary>
        private const string suffixMesazhNjejesLidhurGabimi = " është i lidhur dhe nuk mund të fshihet! ";
        /// <summary>
        /// konstante per mesazhin e mos fshirjes ne shumes per lidhjen
        /// </summary>
        private const string suffixMesazhShumesLidhurGabimi = " janë të lidhur dhe nuk mund të fshihen! ";
        /// <summary>
        /// mesazhi per mos fshirjen ne njejes per periudhen
        /// </summary>
        private const string suffixMesazhNjejesPeriudheKycurGabimi = " i përket një periudhe të kyçur dhe nuk mund të fshihet!  ";
        /// <summary>
        /// mesazhi per mos fshirjen ne shumes per periudhen
        /// </summary>
        private const string suffixMesazhShumesPeriudheKycurGabimi = "  i përkasin periudhave të kyçura dhe nuk mund të fshihen!  ";
        /// <summary>
        /// mesazhi per fshirjen ne njejes
        /// </summary>
        private const string suffixMesazhNjejesSuksesi = " u fshi me sukses!";
        /// <summary>
        /// mesazhi per fshirjen ne shumes
        /// </summary>
        private const string suffixMesazhShumesSuksesi = " u fshinë me sukses!";
        /// <summary>
        /// konstante per lidhezen
        /// </summary>
        private const string lidhesMesazhi = ". Kurse ";
        /// <summary>
        /// mesazh njejes per artikuj me gjendje negative
        /// </summary>
        private const string suffixMesazhNjejesGjendjeNegative = " nuk mund të fshihet sepse krijon artikuj me gjendje negative!";
        /// <summary>
        /// mesazh shumes per artikuj me gjendje negative
        /// </summary>
        private const string suffixMesazhShumesGjendjeNegative = " nuk mund të fshihen sepse krijojnë artikuj me gjendje negative!";

        /// <summary>
        /// mesazhi per mos zgjedhjen e asnje dokumenti
        /// </summary>
        private const string STR_JuLutemZgjidhniTePaktenNjeDokument = "Ju lutem zgjidhni të paktën një dokument!";
        /// <summary>
        /// mesazhi kur ekzistojne dy dokumenta me te njejen id ne grid
        /// </summary>
        private const string STR_GABIMNdodhen2DokumentaListPageseMeTeNjejtenIdNeG = "GABIM: Ndodhen 2 dokumenta me të njëjtën id në gridë";

        /// <summary>
        /// vendos themen e faqes
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        string komponente = "RegjistrimRiparimi.aspx";
        string guidString;
        /// <summary>
        /// kur lodohet faqja  mbushet me te dhena dhe kontrollohen ne eshte i autorizuar perdoruesi
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            int idPerdoruesi;
            int idGjuha;
            int idNdermarrje;
            int idViti;
            int idNdermarrjeVit;
            if (hfState.Count == 0)
            {
                if (!DbCore.mySessionObjects.isLogedIn(Session))
                    DbCore.clsFunksione.logout(Session,true,"FaqePaautorizuar");
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
            System.Globalization.CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Threading.Thread.CurrentThread.CurrentCulture = cultinf;
            System.Threading.Thread.CurrentThread.CurrentUICulture = cultinf;
            percaktoTemplateMenu(idGjuha, ASPxMenu1, idViti, idPerdoruesi, idNdermarrje);
            if (!IsPostBack)
            {
                perktheLabel();
              
                ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 79, "LDRIP", rm, cultinf, idGjuha);
                cmbKonfigurimi.SelectedIndex = 0;
                DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                konf.mbushKonfigAmbjSipasId(Convert.ToInt32(cmbKonfigurimi.SelectedItem.Value), idGjuha);
                hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaDokumenta = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaDokumenta.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, DbCore.clsFunksione.GetKomponente(Page.Request));
                hfTeDrejtaGjitheDok.Value = tedrejtaDokumenta.DGjitheDok.ToString();
                mbushGridNgaDB(idNdermarrjeVit, idNdermarrje, idPerdoruesi);
                if (grid_RegRip.FilterExpression == "")
                    grid_RegRip.FilterExpression = "[IdStatusDok]=1 ";
                konfiguroGride(idNdermarrje, idPerdoruesi, rm, cultinf);
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "grid_RegRip", grid_RegRip, cmbKonfigurimi.Text.Split(';')[0], "541", (int)hfState["idGjuha"]);
                if (Request.QueryString["fshi"] == "rivleresimjo")
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Rivlerësimi përfundoi me gabime!", pnlMesazhi);
                else
                    if (Request.QueryString["fshi"] == "rivleresimpo")
                        clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "Rivlerësimi përfundoi me sukses!", pnlMesazhi);
                    else
                        if (Request.QueryString["fshi"] == "po")
                            clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "Fshirja e dokumentit përfundoi me sukses!", pnlMesazhi);
                        else
                            if (Request.QueryString["ruaj"] == "po")
                                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "Modifikimi përfundoi me sukses!", pnlMesazhi);
                clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idNdermarrje, "grid_RegRip", int.Parse(cmbKonfigurimi.Value.ToString()), "RegjistrimDokumentash.aspx"); 

            }
            else
            {
                mbushGridNgaSession(idNdermarrjeVit, idNdermarrje, idPerdoruesi);
                konfiguroGride(idNdermarrje, idPerdoruesi, rm ,cultinf);
            }
            grid_RegRip.Columns["#"].VisibleIndex = 0;
            if (Request.QueryString["indexrow"] != null)
                grid_RegRip.FocusedRowIndex = int.Parse(Request.QueryString["indexrow"]);
            GridUtil.ToolTipButonaveMbiGride(grid_RegRip, cultinf, new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources")));
        }


        /// <summary>
        /// perkthen label 
        /// </summary>
           public void perktheLabel()
        {
            System.Globalization.CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            konfigurimi_Label.Text = rm.GetString("lblModeli", cultinf);
        }
     
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="idGjuha"></param>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        /// <param name="idViti"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNermarrje"></param>
        private void percaktoTemplateMenu(int idGjuha, ASPxMenu aSPxMenu1, int idViti, int idPerdorues, int idNdermarrje)
        {
            clsToolbarConfig.percaktoTemplateMenu(idGjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1, komponente, this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));

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
            DbCore.clsMesazh mesazh = GridUtil.ruajFiltra(idndermarrje, idperdorues, idgjuha, "grid_RegRip", komponente, "FilterDefault", grid_RegRip.FilterExpression, grid_RegRip, "IdNivel", idkonf, out idfiltri);//ruan filtrin e zgjedhur tek filtrat
            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                return;

            }
            mesazh = GridUtil.ruajkonfigurimgride(grid_RegRip, cmbKonfigurimi.Text, idndermarrje, idperdorues, 541, idfiltri, idViti, DbCore.mySessionObjects.ktheCultureInfo(Session), DbCore.mySessionObjects.ktheGjuhe(Session));/// ruan konfigurimin e grides dhe filtrin

            clsToolbarConfig.mbushComboBoxFiltra(idgjuha, idndermarrje, "grid_RegRip", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);

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
            percaktoTemplateMenu((int)hfState["idGjuha"], ASPxMenu1, (int)hfState["idViti"], (int)hfState["idPerdoruesi"], (int)hfState["idNdermarrje"]);
        }

        /// <summary>
        /// mbush griden me te dhena te ruajtura ne sesion
        /// </summary>
        /// <param name="idNdermarrjeVit"></param>
        /// <param name="veprimi"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="idPerdoruesi"></param>
        private void mbushGridNgaSession(int idNdermarrjeVit, int idNdermarrje, int idPerdoruesi)
        {
            DataTable tmpObject;
            bool sukses = DbCore.mySessionObjects.merrGrideNgaSessioni(Session, out tmpObject);
            if (!sukses)
            {

                mbushGridNgaDB(idNdermarrjeVit, idNdermarrje, idPerdoruesi);
            }
            else
            {
                grid_RegRip.DataSource = tmpObject;
                grid_RegRip.RestoreFilter(idNdermarrje);
                grid_RegRip.DataBind();
                grid_RegRip.SaveFilter(idNdermarrje);
                tmpObject.Dispose();
            }
        }

        /// <summary>
        /// mbush griden me te dhena nga db
        /// 
        /// </summary>
        /// <param name="idNdermarrjeVit"></param>
        /// <param name="veprimi"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="idPerdorues"></param>
        private void mbushGridNgaDB(int idNdermarrjeVit, int idNdermarrje, int idPerdoruesi)
        {//mbush griden e popupit me te dhena            
            DataTable dt = new DataTable();

            dt = DbCore.DbRegjistrim.colKokaRiparime.merrKokaRiparimeDT(idNdermarrjeVit, idPerdoruesi);
            grid_RegRip.DataSource = dt;
            grid_RegRip.DataBind();
            DbCore.mySessionObjects.ruajGrideNeSession(Session, dt);
            dt.Dispose();
            grid_RegRip.RestoreFilter(idNdermarrje);
        }

        /// <summary>
        /// konfiguron griden
        /// </summary>
        /// <param name="veprimi"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="idPerdoruesi"></param>
        private void konfiguroGride(int idNdermarrje, int idPerdoruesi, ResourceManager rm, CultureInfo ci)
        {
            grid_RegRip.Columns["#"].VisibleIndex = 0;

            //shtoNivel( idNdermarrje, idPerdoruesi);
            //shtoModel( idNdermarrje, idPerdoruesi);
            //shtoStatusRiparimi(idNdermarrje, idPerdoruesi);
            //shtoMagazine(idNdermarrje, idPerdoruesi);


            KonfigurimComboGride.ShtoStatus(grid_RegRip, rm, ci);
            grid_RegRip.ShtoPerdorues(idPerdoruesi, Session, komponente, guidString, "IdKrijuesi");
            KonfigurimComboGride.ShtoDorezuar(grid_RegRip, rm, ci);

           GridViewDataDateColumn col4 = grid_RegRip.Columns["DtFillestare"] as GridViewDataDateColumn;
            col4.PropertiesEdit.DisplayFormatString = "dd/MM/yyyy";

            GridViewDataDateColumn col5 = grid_RegRip.Columns["DtTekniku"] as GridViewDataDateColumn;
            col5.PropertiesEdit.DisplayFormatString = "dd/MM/yyyy";

            GridViewDataDateColumn col6 = grid_RegRip.Columns["DtDorezimi"] as GridViewDataDateColumn;
            col6.PropertiesEdit.DisplayFormatString = "dd/MM/yyyy";
            GridUtil.konfigGrideListeEMadhePaTheme(grid_RegRip, "IdKoka");
        }

    
        private void shtoNivel(int idNdermarrje, int idPerdoruesi)
        {
            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            if (typeof(GridViewDataComboBoxColumn) != grid_RegRip.Columns["IdNivel"].GetType())
            {
                grid_RegRip.Columns.Remove(grid_RegRip.Columns["IdNivel"]);
                grid_RegRip.Columns.Add(colnew);
                DataView nivele;

                nivele = DbCore.DbRegjistrim.colNivelRegjistrimi.ktheGjitheNivelRegjistrimiSipasKategoriDtCombo(80, idNdermarrje, idPerdoruesi, true);
                colnew.PropertiesComboBox.DataSource = nivele;
                colnew.PropertiesComboBox.TextField = "Pershkrimi";
                colnew.PropertiesComboBox.ValueField = "IdNivel";
                colnew.FieldName = "IdNivel";
                DbCore.mySessionObjects.ruajDsComboGrideNeSession(Session, nivele, "nivele");
            }
            else
            {
                colnew = (GridViewDataComboBoxColumn)grid_RegRip.Columns["IdNivel"];
                if (colnew.PropertiesComboBox.Items.Count == 0)
                {
                    //grid_RegRip.Columns.Remove(grid_RegRip.Columns["IdNivel"]);
                    colnew.PropertiesComboBox.DataSource = DbCore.mySessionObjects.merrDsComboGrideNeSession(Session, "nivele");
                    //grid_RegRip.Columns.Add(colnew);
                }
            }
        }
        private void shtoStatusRiparimi(int idNdermarrje, int idPerdoruesi)
        {
            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            if (typeof(GridViewDataComboBoxColumn) != grid_RegRip.Columns["IdStatusRiparimi"].GetType())
            {
                grid_RegRip.Columns.Remove(grid_RegRip.Columns["IdStatusRiparimi"]);
                grid_RegRip.Columns.Add(colnew);

                DbCore.DbInventari.colStatusRiparimi status = new DbCore.DbInventari.colStatusRiparimi(idNdermarrje, idPerdoruesi);
                status.Insert(0, new DbCore.DbInventari.clsStatusRiparimi());
                colnew.PropertiesComboBox.DataSource = status;
                colnew.PropertiesComboBox.TextField = "Pershkrimi";
                colnew.PropertiesComboBox.ValueField = "Id";
                colnew.FieldName = "IdStatusRiparimi";
                DbCore.mySessionObjects.ruajDsComboGrideNeSession(Session, status, "statusriparimi");
            }
            else
            {
                colnew = (GridViewDataComboBoxColumn)grid_RegRip.Columns["IdStatusRiparimi"];
                if (colnew.PropertiesComboBox.Items.Count == 0)
                {
                    colnew.PropertiesComboBox.DataSource = DbCore.mySessionObjects.merrDsComboGrideNeSession(Session, "statusriparimi");

                }
            }
        }
        private void shtoMagazine(int idNdermarrje, int idPerdoruesi)
        {
            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            if (typeof(GridViewDataComboBoxColumn) != grid_RegRip.Columns["IdMagazina"].GetType())
            {
                grid_RegRip.Columns.Remove(grid_RegRip.Columns["IdMagazina"]);
                grid_RegRip.Columns.Add(colnew);
                DbCore.DbRegjistrim.colNjesiAdministrative colMagazinat = new DbCore.DbRegjistrim.colNjesiAdministrative();
                colMagazinat.Add(new DbCore.DbRegjistrim.clsNjesiAdministrative(0, "", "", "", 0, false, true, 0, 0, DateTime.Today, 0, 0, 0, new DbCore.DbAdmin.colLidhjetAutorizim(), 0, 0, DateTime.Now, 0, 0, new DbCore.DbAsete.colHistorikStatusMagazine(), "", false, "", "", 1, 0, "", "", 0, 0, 0, false, false, false,"",0));
                colMagazinat.mbushGjitheNjesiAdministrative(idNdermarrje);
                colnew.PropertiesComboBox.DataSource = colMagazinat;
                colnew.PropertiesComboBox.TextField = "Kodi";
                colnew.PropertiesComboBox.ValueField = "IdNjesiAdministrative";
                colnew.FieldName = "IdMagazina";
                DbCore.mySessionObjects.ruajDsComboGrideNeSession(Session, colMagazinat, "magazina");
            }
            else
            {
                colnew = (GridViewDataComboBoxColumn)grid_RegRip.Columns["IdMagazina"];
                if (colnew.PropertiesComboBox.Items.Count == 0)
                {
                    colnew.PropertiesComboBox.DataSource = DbCore.mySessionObjects.merrDsComboGrideNeSession(Session, "magazina");

                }
            }
        }

        #region shtoModel Komentuar
        //private void shtoModel(int idNdermarrje, int idPerdoruesi, int idGjuha)
        //{
        //    GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
        //    if (typeof(GridViewDataComboBoxColumn) != grid_RegRip.Columns["IdKonfigAmbjente"].GetType())
        //    {
        //        grid_RegRip.Columns.Remove(grid_RegRip.Columns["IdKonfigAmbjente"]);
        //        grid_RegRip.Columns.Add(colnew);
        //        DbCore.DbShare.colKonfigurimAmbjenti colKonfig = new DbCore.DbShare.colKonfigurimAmbjenti();
        //        DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();

        //        konf.IdKategori = 80;
        //        konf.IdNdermarje = idNdermarrje;
        //        colKonfig.Add(new DbCore.DbShare.clsKonfigurimAmbjenti(0, "", "", 1, 0, true, 0, 0, 0, 0, 0, 0, 0, ""));
        //        colKonfig.mbushKonfigAmbjSipasIdKategori(konf.IdKategori, idNdermarrje, idPerdoruesi, idGjuha);
        //        colnew.PropertiesComboBox.DataSource = colKonfig;
        //        colnew.PropertiesComboBox.TextField = "KodKonfigAmbjente";
        //        colnew.PropertiesComboBox.ValueField = "IdKonfigAmbjente";
        //        colnew.FieldName = "IdKonfigAmbjente";
        //        DbCore.mySessionObjects.ruajDsComboGrideNeSession(Session, colKonfig, "colKonfig");
        //    }
        //    else
        //    {
        //        colnew = (GridViewDataComboBoxColumn)grid_RegRip.Columns["IdKonfigAmbjente"];
        //        if (colnew.PropertiesComboBox.Items.Count == 0)
        //        {
        //            //grid_RegRip.Columns.Remove(grid_RegRip.Columns["IdKonfigAmbjente"]);
        //            colnew.PropertiesComboBox.DataSource = DbCore.mySessionObjects.merrDsComboGrideNeSession(Session, "colKonfig");
        //            //grid_RegRip.Columns.Add(colnew);
        //        }
        //    }
        //}
        #endregion shtoModel Komentuar


        /// <summary>
        /// kur grida ben databound per ti shtuar kolonen e selektimit
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void grid_RegRip_DataBound(object sender, EventArgs e)
        {
            if (grid_RegRip.Columns["#"] == null)
            {
                GridViewCommandColumn check = new GridViewCommandColumn("#") { ShowSelectCheckbox = true, Width = System.Web.UI.WebControls.Unit.Percentage(2) };
                grid_RegRip.Settings.ShowFilterRow = true;
                grid_RegRip.Settings.ShowHeaderFilterButton = true;
                grid_RegRip.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                grid_RegRip.Settings.ShowFilterRowMenu = true;
                grid_RegRip.Columns.Add(check);
                grid_RegRip.Settings.ShowGroupPanel = true;
                grid_RegRip.KeyFieldName = "IdKoka";
                grid_RegRip.SettingsBehavior.AllowSelectByRowClick = true;
                grid_RegRip.SettingsBehavior.AllowFocusedRow = true;
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
            int idPerdoruesi = (int)hfState["idPerdoruesi"];
            int idNdermarrje = (int)hfState["idNdermarrje"];
            int idGjuha = (int)hfState["idGjuha"];
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuha, "grid_RegRip", komponente, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                filtra.IdPerdoruesi = idPerdoruesi;
                mesazh = filtra.fshi();
                //mbushComboBoxFiltra();
                clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idNdermarrje, "grid_RegRip", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
                int idViti = (int)hfState["idViti"];
                percaktoTemplateMenu(idGjuha, ASPxMenu1, idViti, idPerdoruesi, idNdermarrje);
                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                cmbFiltra.Text = "";
                grid_RegRip.FilterExpression = " [IdStatusDok]=1  ";
                //
            }
        }
        public void btnJo_Click(object sender, EventArgs e)
        {
            if (pergjigja.Text.Contains("lidhur") || pergjigja.Text.Contains("kycur"))
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, pergjigja.Text, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, pergjigja.Text, pnlMesazhi);

            DbCore.mySessionObjects.ruajTrupatNeSession(Session, new DbCore.DbRegjistrim.colTrupiMagazina());
        }
        public void btnPo_Click(object sender, EventArgs e)
        {
            DbCore.DbRegjistrim.colTrupiMagazina trupat = DbCore.mySessionObjects.merrTrupatNgaSesioni(Session);
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh(true, "Rivleresimi perfundoi me sukses!");
            string fileLogPath = Server.MapPath("~/log/log.txt");
            DbCore.DbAdmin.clsLogRivleresimInventari log = new DbCore.DbAdmin.clsLogRivleresimInventari();
            try
            {
                log = new DbCore.DbAdmin.clsLogRivleresimInventari(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            }
            catch (Exception)
            {
                throw new DbCore.MyException(rm.GetString("msgGabimGjateRuajtjesSeRivleresimitNeLog", ci));
            }
            foreach (DbCore.DbRegjistrim.clsTrupiMagazina t in trupat)
            {
                //DbCore.DbInventari.clsArtikulli art = new DbCore.DbInventari.clsArtikulli(t.IdArtikulli);
                mesazh = DbCore.DbInventari.clsArtikulli.rivleresimCmimiMesatar(t.IdArtikulli, DbCore.DbInventari.clsArtikulli.ktheMetodeKostoje(t.IdArtikulli), t.IdMag, t.Data, DateTime.Today, clsArtikulli.ktheKontrollCmimiPerDetajim(t.IdArtikulli),log,ci,rm,DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
                if (!mesazh.Status)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Rivlerësimi përfundoi me gabime!", pnlMesazhi);
                }
            }
            DbCore.mySessionObjects.ruajTrupatNeSession(Session, new DbCore.DbRegjistrim.colTrupiMagazina());
            if (mesazh.Status)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "Rivlerësimi përfundoi me sukses!", pnlMesazhi);
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
            int idNdermarrje = (int)hfState["idNdermarrje"];
            int idPerdoruesi = (int)hfState["idPerdoruesi"];
            int idGjuha = (int)hfState["idGjuha"];
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuha, "grid_RegRip", komponente, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            DbCore.DbAdmin.clsFiltraGrida filtri = new DbCore.DbAdmin.clsFiltraGrida() { FiltraKodi = cmbFiltra.Text, FiltraShenime = cmbFiltra.Text, FiltraUniversal = false, GridaKokaId = koka.IdGridaKoka, FiltraVlera = grid_RegRip.FilterExpression, IdPerdoruesi = idPerdoruesi, IdNdermarje = idNdermarrje, IdStatusDok = 1 };
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("IdNivel", grid_RegRip);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = grid_RegRip.GetSortedColumns();
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
            int idViti = (int)hfState["idViti"];
            clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idNdermarrje, "grid_RegRip", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
            percaktoTemplateMenu(idGjuha, ASPxMenu1, idViti, idPerdoruesi, idNdermarrje);
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


            if (e.Item.Name == "PrintPreview")
            {
                if (grid_RegRip.FocusedRowIndex == -1)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ju lutem zgjidhni një faturë për të printuar!", pnlMesazhi);
                    Container.Attributes["src"] = "";
                }
                else
                {
                    string id = grid_RegRip.GetRowValues(grid_RegRip.FocusedRowIndex, "IdKoka").ToString();

                    clsKokaRiparime regjistrime = new clsKokaRiparime(Convert.ToInt32(id));
                    clsStatusRiparimi status = new clsStatusRiparimi(regjistrime.IdStatusRiparimi);
                    if (status.Pershkrimi == "Kerkese e re")
                    {
                        int idRaporti = 0;
                        //clsGaranciArtikulli garanci = new clsGaranciArtikulli(regjistrime.IdGaranci);
                        //clsArtikulli art = new clsArtikulli(garanci.IdArtikulli);
                 
                        //if (art.Loan)
                        if (regjistrime.ColTrupi.Count > 0 && regjistrime.ColTrupi[0].IdArtLoan > 0) idRaporti = 147; else idRaporti = 146;

                        Container.Attributes["src"] = "RaportiShpejte.aspx?Sesioni=false&idraporti=" + idRaporti + "&idDokumenti=" + regjistrime.IdKoka + "&printo=false";
                    }
                    else if (status.Pershkrimi == "Aparati dorezuar Klientit")
                    {
                        int idRaporti = 0;
                        //clsGaranciArtikulli garanci = new clsGaranciArtikulli(regjistrime.IdGaranci);
                        //clsArtikulli art = new clsArtikulli(garanci.IdArtikulli);
                        //if (art.Loan) 
                        if (regjistrime.ColTrupi.Count > 0 && regjistrime.ColTrupi[0].IdArtLoan > 0) idRaporti = 149; else idRaporti = 148;

                        Container.Attributes["src"] = "RaportiShpejte.aspx?Sesioni=false&idraporti=" + idRaporti + "&idDokumenti=" + regjistrime.IdKoka + "&printo=false";
                    }

                }
            }
        }

        /// <summary>
        /// fshin rreshtat e selektuar
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            System.Globalization.CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            List<string> TeFshire = new List<string>(), TeLidhur = new List<string>(), GjendjeNegative = new List<string>(), PeriudheKycur = new List<string>();
            pergjigja.Text = "";
            DbCore.DbRegjistrim.colTrupiMagazina tr = new DbCore.DbRegjistrim.colTrupiMagazina();
            DbCore.DbRegjistrim.colTrupiMagazina trupat = new DbCore.DbRegjistrim.colTrupiMagazina();
            bool rivleresim = false;
            //clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "", pnlMesazhi);
            List<object> rreshtat = grid_RegRip.GetSelectedFieldValues("IdKoka");
            pergjigja.Text = "";
            //DbCore.DbAdmin.clsPeriudhaKontabel periudha;
            DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            int idPerdoruesi = (int)hfState["idPerdoruesi"];
            int idNdermarrje = (int)hfState["idNdermarrje"];
            int idNdermarrjeVit = (int)hfState["idNdermarrjeVit"];
            foreach (object id in rreshtat)
            {
                DbCore.DbRegjistrim.clsKokaRiparime clsKoka = new clsKokaRiparime(Convert.ToInt32(id));
                bool kontrollorivleresim = clsAlternativaKushti.getAlternativa(clsKoka.IdKonfigAmbjente, "KR") == "Po";
                bool lidhur = dbAdmin.eshteDokumentiILidhur(clsKoka.IdKoka, clsKoka.IdNivel, "T_KOKARIPARIME", "IDKOKA");
                if (lidhur)
                {
                    TeLidhur.Add(clsKoka.IdGaranci.ToString());
                    continue;
                }

                if (clsKoka.IdStatusDok == 2)
                    continue;
                //periudha = new DbCore.DbAdmin.clsPeriudhaKontabel(clsKoka.DtDok, idNdermarrje);
               // DbCore.clsMesazh mesazhi = periudha.isPeriudheKycur();
                bool ekycur = DbCore.DbAdmin.clsPeriudhaKontabel.eshteKycurPeriudheSipasDateDheNdermarrjes(clsKoka.DtDok, idNdermarrje);
                if (ekycur)
                {
                    PeriudheKycur.Add(clsKoka.IdGaranci.ToString());
                    continue;
                }
                clsKokaMagazina kok = new clsKokaMagazina();


                kok.mbushKokaMagazinaSipasIDGjenerues(clsKoka.IdKoka, 2, clsKoka.IdKonfigAmbjente);

                if (kok.IdKokaMagazina != 0)
                {
                    kok.mbushTrupMagazine(false);
                    colArtikujt coleksistues1 = new colArtikujt(kok.IdKokaMagazina, new clsDatabaseInventari());
                    int i2 = 0;
                    foreach (clsTrupiMagazina trup in kok.OcolTrupiMagazina)
                    {
                        if (trup.IdLlojVeprimi == 1)
                            trup.Element = coleksistues1[i2];
                        i2++;
                    }
                    if (!kok.kontrolloGjendjeNeFshirje(new DbCore.DbRegjistrim.colTrupiMagazina(), 0).Status)
                    {
                        GjendjeNegative.Add(kok.NrDok);
                        continue;
                    }
                }
                if (kok.IdKokaMagazina != 0 && kontrollorivleresim && kok.IdStatusDok != 0)
                    if (kok.rivleresim())
                    {
                        rivleresim = true; tr.mbushGjitheTrupiMagazinaNgaKoka(kok.IdKokaMagazina); trupat.AddRange(tr);
                    }
                clsKoka.IdPerdoruesi = idPerdoruesi;
                mesazh = clsKoka.fshi();

                DbCore.mySessionObjects.ruajTrupatNeSession(Session, trupat);
                if (mesazh.Status)
                {
                    hiqNgaGrida(idNdermarrjeVit, idNdermarrje, idPerdoruesi, clsKoka.IdKoka);
                    TeFshire.Add(clsKoka.IdGaranci.ToString());
                }
            }
            dbAdmin.Dispose();
            string mesazhInfoGabimLidhur = "", mesazhInfoGabimPeridheKycur = "", mesazhInfoSukses = "", mesazhGjendjeNegative = "";

            if (TeLidhur.Count == 1)
                mesazhInfoGabimLidhur = String.Format("{0}{1}{2}", prefixMesazhNjejes, String.Join(";", TeLidhur), suffixMesazhNjejesLidhurGabimi);
            else
                if (TeLidhur.Count > 1)
                    mesazhInfoGabimLidhur = String.Format("{0}{1}{2}", prefixMesazhShumes, String.Join(";", TeLidhur), suffixMesazhShumesLidhurGabimi);
            if (PeriudheKycur.Count == 1)
                mesazhInfoGabimPeridheKycur = String.Format("{0}{1}{2}", prefixMesazhNjejes, String.Join(";", PeriudheKycur), suffixMesazhNjejesPeriudheKycurGabimi);
            else
                if (PeriudheKycur.Count > 1)
                    mesazhInfoGabimPeridheKycur = String.Format("{0}{1}{2}", prefixMesazhShumes, String.Join(";", PeriudheKycur), suffixMesazhShumesPeriudheKycurGabimi);
            if (GjendjeNegative.Count == 1)
                mesazhGjendjeNegative = String.Format("{0}{1}{2}", prefixMesazhNjejes, String.Join(";", GjendjeNegative), suffixMesazhNjejesGjendjeNegative);
            else
                if (GjendjeNegative.Count > 1)
                    mesazhGjendjeNegative = String.Format("{0}{1}{2}", prefixMesazhShumes, String.Join(";", GjendjeNegative), suffixMesazhShumesGjendjeNegative);

            if (TeFshire.Count == 1)
                mesazhInfoSukses = String.Format("{0}{1}{2}", prefixMesazhNjejes, String.Join(";", TeFshire), suffixMesazhNjejesSuksesi);
            else
                if (TeFshire.Count > 1)
                    mesazhInfoSukses = String.Format("{0}{1}{2}", prefixMesazhShumes, String.Join(";", TeFshire), suffixMesazhShumesSuksesi);

            mesazhInfoGabimLidhur += mesazhInfoGabimPeridheKycur + mesazhGjendjeNegative;
            if (mesazhInfoGabimLidhur != "" && mesazhInfoSukses != "")
            {
                mesazhInfoGabimLidhur += lidhesMesazhi + mesazhInfoSukses;
                pergjigja.Text = mesazhInfoGabimLidhur;
                pergjigja.ClientVisible = false;
            }
            else
            {
                pergjigja.Text = mesazhInfoSukses;
                pergjigja.ClientVisible = false;
            }
            if (rreshtat.Count == 0)
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, STR_JuLutemZgjidhniTePaktenNjeDokument, pnlMesazhi);
            else if (rivleresim)
                clsMenuInfo.ShtoPyetje(MenuInfo, rm.GetString("regjMagVeprimiSjellNdryshimNeCmimDalje",cultinf), pnlMesazhi, (int)hfState["idGjuha"]);
            else
                if (mesazhInfoGabimLidhur != "")
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhInfoGabimLidhur, pnlMesazhi);
                else
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazhInfoSukses, pnlMesazhi);

        }

        /// <summary>
        /// heq nga grida reshtin e fshire
        /// </summary>
        /// <param name="idNdermarrjeVit"></param>
        /// <param name="veprimi"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="idPerdoruesi"></param>
        /// <param name="idkoka">id e reshtit te fshire</param>
        private void hiqNgaGrida(int idNdermarrjeVit, int idNdermarrje, int idPerdoruesi, int idkoka)
        {
            if (grid_RegRip.DataSource != null)
            {
                DataTable dt = (DataTable)grid_RegRip.DataSource;
                DataRow[] drs = dt.Select("IdKoka = " + idkoka);
                if (drs.Length > 1)
                    throw new Exception(STR_GABIMNdodhen2DokumentaListPageseMeTeNjejtenIdNeG);
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                dt.Rows.Remove(dr);
                grid_RegRip.DataSource = dt;
                grid_RegRip.DataBind();
                dt.Dispose();
            }
            else
            {

                mbushGridNgaDB(idNdermarrjeVit, idNdermarrje, idPerdoruesi);

            }
        }

        /// <summary>
        /// kur grida ben callback
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void grid_RegRip_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            if (e.CallbackName == "COLUMNMOVE" && grid_RegRip.AllColumns[int.Parse(e.Args[0])].Width.Value == 0)
                grid_RegRip.AllColumns[int.Parse(e.Args[0])].Width = System.Web.UI.WebControls.Unit.Percentage(3);
            if (e.CallbackName == "APPLYFILTER" && e.Args[0] == "")
            {
                MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
                ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
                cmbFiltra.Text = "";
            }

            //shtoPikeShitjeFurnizim(veprimi, (int)hfState["idNdermarrje"]);
            grid_RegRip.SaveFilter((int)hfState["idNdermarrje"]);
            // konfiguroGride();
        }

        /// <summary>
        /// kur perdoruesi i ben grides callbak per te mare konfigurimin e ri
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void grid_RegRip_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string[] arr = e.Parameters.Split(';');
            int idNdermarrje = (int)hfState["idNdermarrje"];
            GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "grid_RegRip", grid_RegRip, cmbKonfigurimi.Text.Split(';')[0], "541", DbCore.mySessionObjects.ktheGjuhe(Session));
            if (arr.Length == 3)
            {
                if (arr[2] == "")
                    grid_RegRip.FilterExpression = " [IdStatusDok]=1 ";
                // 
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    //filtra.mbushFiltraGridaSipasFiltraKodi(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka((int)hfState["idGjuha"], "grid_RegRip", "RegjistrimRiparimi.aspx", idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], idNdermarrje, koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        grid_RegRip.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, grid_RegRip);
                    }
                }
            }

            grid_RegRip.Selection.UnselectAll();
        }

        /// <summary>
        /// perdoret per te vendosur karakteristika te griden ne javascript
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void grid_RegRip_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = grid_RegRip.PageIndex;
            e.Properties["cpPageRow"] = grid_RegRip.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = grid_RegRip.VisibleRowCount;
        }

        /// <summary>
        /// perdoret per te hequr filtrimin e grides kur zgjedhim elementin bosh
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void grid_RegRip_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if (e.Column.FieldName == "IdKonfigAmbjente" )
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
        protected void grid_RegRip_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            if (e.Column.FieldName == "NrKontakti" || e.Column.FieldName == "Pershkrimi")
            {
                e.Values.Clear();
                e.AddValue("(Te gjithe)", string.Empty, "true");
                e.AddValue("Nga A-D ", string.Empty, String.Format("{0}>'A     ' and {0} <'DDDDDDD'", e.Column.FieldName));
                e.AddValue("Nga D-G ", string.Empty, String.Format("{0}>'D     ' and {0}<'GGGGGGG'", e.Column.FieldName));
                e.AddValue("Nga H-K ", string.Empty, String.Format("{0}>'H     ' and {0}<'KKKKKKK'", e.Column.FieldName));
                e.AddValue("Nga L-O ", string.Empty, String.Format("{0}>'L     ' and {0}  <'OOOOOOO'", e.Column.FieldName));
                e.AddValue("Nga P-S ", string.Empty, String.Format("{0}>'P     ' and {0}<'SSSSSSS'", e.Column.FieldName));
                e.AddValue("Nga T-W ", string.Empty, String.Format("{0}>'T     ' and {0}<'WWWWWWW'", e.Column.FieldName));
                e.AddValue("Nga X-Z ", string.Empty, String.Format("{0}>'X     ' and {0}<'ZZZZZZZ'", e.Column.FieldName));
            }
            else
            {
                e.Values.Clear();
                e.AddValue("(Te gjithe)", string.Empty, "true");
            }
        }

        protected void ButtonOk2_Click2(object sender, EventArgs e)
        {

        }
    }
}