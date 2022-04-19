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
using System.Resources;
using DbCore.IMBUtils.Types;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class KonfigurimRegjistrimi : MyPageBase
    {
        private DbCore.DbRegjistrim.clsDatabaseRegjistrim dbRegjistrime;
        public static int idNderm = -1;
        //DbCore.DbRegjistrim.clsNivelRegjistrimi niveli;
        private bool shto;
        private  string mesazhZgjidhniNje = "Ju lutem zgjidhni te pakten nje konfigurim!";
        private string komponente = "KonfigurimRegjistrimi.aspx";
        private string guidString;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);

            //if (CacheLayer.GlobalCacheManager.MySessionCache["LoggedIn"].Equals("No"))
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                DbCore.clsFunksione.logout(Session, true, "FaqePaautorizuar");
            }
            int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            //if (CacheLayer.GlobalCacheManager.MySessionCache["KodiNdermarrjes"] == null)
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerdoruesi);
            }
            //oPerdorues = (DbCore.DbAdmin.clsPerdorues)(CacheLayer.GlobalCacheManager.MySessionCache["oClsPerdoruesi"]);
            //idNderm = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            idNderm = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNderm);

            //mbushComboBoxFiltra(idNderm);
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNderm, "gvKategoriDok", 1, komponente);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            System.Globalization.CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);

            if (Page.IsPostBack == false)
            {
                guidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                hfState.Set("guidString", guidString);
                perktheLabel();
                //Session.Add("mesazh", ":Green");
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, ":");
                if (Request.QueryString["ruaj"] == "ok")
                {
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("labelRaportMesazhRuajtjaPerfundoiSukses", cultinf), pnlMesazhi);
                }
                if (Request.QueryString["indexrow"] != null)
                {
                    gvKategoriDok.FocusedRowIndex = int.Parse(Request.QueryString["indexrow"]);
                }
                //shto = false;          
                //new DbCore.clsFunksione().konfiguroMenuPaTheme(ASPxMenu1);
                //ASPxMenu1.AutoPostBack = false;
                //new DbCore.clsFunksione().percaktoTedrejtatPerKeteFaqe("KonfigurimRegjistrimi.aspx", ASPxMenu1);

                konfiguroVleraFillestare(idPerdoruesi);
                konfiguroGride(idPerdoruesi, idNderm);
                GridUtil.percaktoVisibleColumnsMeWidth(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), gvKategoriDok, "gvKategoriDok", komponente);

                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNderm, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), komponente);
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                //shto = Convert.ToBoolean(hfVeprimi.Value.ToString());
            }
            else
            {
                konfiguroVleraFillestare(idPerdoruesi);
                konfiguroGride(idPerdoruesi, idNderm);
            }
            guidString = (string)hfState["guidString"];
            DbCore.mySessionObjects.ruajMesazhNeSesion(Session, ":");
            AspxWebControlUtils.perkthePopUp(popFshi, rm.GetString("labelKujdes", cultinf), lblMsgbox, rm.GetString("labelAdministrimiMsgJeniSigurt", cultinf), ButtonCancel, rm.GetString("labelAnullo", cultinf));

        }
          /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        /// <param name="idViti"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNdermarrje"></param>
        private void percaktoTemplateMenu(ASPxMenu aSPxMenu1, int idViti, int idPerdorues, int idNdermarrje)
        {
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, komponente, this, MenuInfo, RuajFilter_ASPxButton_Click, FshiFilter_ASPxButton_Click, hfRuaj.Value == "Ruaj" ? true : false, true, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
        }


        public void perktheLabel()
        {
            System.Globalization.CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            hfTeDrejta.Set("msgNukKeniDrejtaPerVeprim", rm.GetString("msgNukKeniDrejtaPerVeprim", cultinf));
            mesazhZgjidhniNje =  rm.GetString("msgZgjidhniNjeKonfigurim", cultinf);

        }
        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));

        }

        protected void ASPxMenu1_ItemClick1(object source, MenuItemEventArgs e)
        {

        }

        /// <summary>
        /// sherben per te ruajtur filtrin e zgjedhur
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void RuajFilter_ASPxButton_Click(object sender, EventArgs e)
        {
            idNderm = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            DbCore.DbAdmin.clsFiltraGrida filtri = new DbCore.DbAdmin.clsFiltraGrida();
            filtri.FiltraKodi = cmbFiltra.Text;
            filtri.FiltraShenime = cmbFiltra.Text;
            filtri.FiltraUniversal = false;// Universal_ASPxCheckBox.Checked;
            var idGjuhe = DbCore.mySessionObjects.ktheGjuhe(Session);
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuhe, "gvKategoriDok", komponente, idNderm);
            filtri.GridaKokaId = koka.IdGridaKoka;


            filtri.FiltraVlera = gvKategoriDok.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("IdNivel", gvKategoriDok);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvKategoriDok.GetSortedColumns();
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


            //DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();
            //oPerdorues = (DbCore.DbAdmin.clsPerdorues)(CacheLayer.GlobalCacheManager.MySessionCache["oClsPerdoruesi"]);
            filtri.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            filtri.IdNdermarje = idNderm;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();

            filtri.IdStatusDok = 1;
            mesazh = filtri.ruaj();
            //mbushComboBoxFiltra(idNderm);
            clsToolbarConfig.mbushComboBoxFiltra(idGjuhe, idNderm, "gvKategoriDok",1, komponente);
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNderm);

            if (mesazh.Status)
            {
                ASPxMenu1.Items.FindByText("Ruaj").ClientVisible = false;
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            }
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            cmbFiltra.Text = "";
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
            idNderm = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;

            //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
            //filtra.mbushFiltraGridaSipasFiltraEmri(cmbFiltra.Text, idNderm);
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvKategoriDok", komponente, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
            //DbCore.DbAdmin.clsFiltraGrida filtra = dbAdmin.merrFiltraGridaSipasFiltraEmri(cmbFiltra.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            if (filtra.FiltraKodi != null)
            {
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                mesazh = filtra.fshi();
                //mbushComboBoxFiltra(idNderm);
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNderm, "gvKategoriDok",1, komponente);
                percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNderm);

                if (mesazh.Status)
                {
                    ASPxMenu1.Items.FindByText("Ruaj").ClientVisible = false;
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                }
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                //   konfiguroVleraFillestare();
                gvKategoriDok.FilterExpression = String.Empty;
            }
        }

        private void konfiguroVleraFillestare(int idPerdoruesi)
        { //mbush komboboxet dhe gridat e faqes
            //inicializoObjekte();
            mbushListeNiveleDokumentash(idPerdoruesi);
        }

        protected void gvKategoriDok_DataBound(object sender, EventArgs e)
        {// shton colonen # per selektim dhe disa karakteristika te grides
            if (this.gvKategoriDok.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per 
                //perzgjidh
                DevExpress.Web.GridViewCommandColumn check = new DevExpress.Web.GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true; check.Width = Unit.Percentage(2);
                check.SetColVisibleIndex(0);
                //behet per te afishuar rreshtin qe do sherbej per filtrim
                gvKategoriDok.Settings.ShowFilterRow = true;
                gvKategoriDok.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                gvKategoriDok.Settings.ShowFilterRowMenu = true;
                gvKategoriDok.Columns.Add(check);
                gvKategoriDok.KeyFieldName = "IdNivel";
                //gvKategoriDok.SettingsBehavior.AllowSelectByRowClick = true;
                gvKategoriDok.SettingsBehavior.AllowSelectByRowClick = true;
                gvKategoriDok.SettingsBehavior.AllowFocusedRow = true;
                gvKategoriDok.SettingsText.CommandUpdate = "Ruaj";
                gvKategoriDok.SettingsText.CommandCancel = "Anullo";
            }
            else
            {
                DevExpress.Web.GridViewCommandColumn check = this.gvKategoriDok.Columns["#"] as DevExpress.Web.GridViewCommandColumn;
                check.ShowSelectCheckbox = true; check.Width = Unit.Percentage(2);
                check.SetColVisibleIndex(0);
            }
        }



        private void konfiguroGride(int idPerdoruesi, int idNderm)
        {//konfiguron griden
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            KonfigurimComboGride.shtoKategoriaNivelDok(gvKategoriDok, Session, komponente, guidString, "IdKategori");
            KonfigurimComboGride.shtoAktivPoOseJo(gvKategoriDok, rm, ci, "Aktiv");
            KonfigurimComboGride.shtoAktivPoOseJo(gvKategoriDok, rm, ci, "NrSerialUnik");
            KonfigurimComboGride.shtoKonvertim(gvKategoriDok, idNderm, idPerdoruesi, Session, komponente, guidString, "Konvertohet");
            GridUtil.konfiguroGrideListeEvogelPaTheme(gvKategoriDok, "IdNivel");
        }

        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {//veprimet e menuse
            if (e.Item.Name == "Shto")
            {
                //Response.Redirect("ShtoKategoriRegjistrimi.aspx");
                gvKategoriDok.AddNewRow();

            }
            else if (e.Item.Name == "Modifiko")
            {
                int indeksi = gvKategoriDok.FocusedRowIndex;
                shto = false;
                gvKategoriDok.StartEdit(indeksi);
            }
        }

        private void mbushListeNiveleDokumentash(int idPerdoruesi)
        {//mbush griden me te dhena
            //oPerdorues = (DbCore.DbAdmin.clsPerdorues)(CacheLayer.GlobalCacheManager.MySessionCache["oClsPerdoruesi"]);
            //DbCore.DbRegjistrim.colNivelRegjistrimi colNivele = dbRegjistrime.merrGjitheNivelRegjistrimi(idNderm, oPerdorues.IdPerdorues);
            DbCore.DbRegjistrim.colNivelRegjistrimi colNivele = new DbCore.DbRegjistrim.colNivelRegjistrimi();
            colNivele.mbushNivelRegjistrimiMeKonvertime(idNderm, idPerdoruesi);
            gvKategoriDok.DataSource = colNivele;
            gvKategoriDok.DataBind();
        }

        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            System.Globalization.CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            DbCore.DbRegjistrim.clsDatabaseRegjistrim dbRegj = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();

            List<object> rreshtat = gvKategoriDok.GetSelectedFieldValues("IdNivel");
            if (rreshtat.Count == 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhZgjidhniNje, pnlMesazhi);
                return;
            }
            int idPerdorues = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            foreach (int id in rreshtat)
            {
                //DbCore.DbRegjistrim.clsDatabaseRegjistrim dbRegj = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();                
                DbCore.DbRegjistrim.clsNivelRegjistrimi clsNivel = new DbCore.DbRegjistrim.clsNivelRegjistrimi();
                clsNivel.mbushNivelRegjistrimiSipasIdMeKonvertime(id); 
             
                clsNivel.IdPerdoruesi = idPerdorues;
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();

                mesazh = dbRegj.fshiTeDrejtaPerNivelRegjistrimi(id);
                if(!mesazh.Status)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                    return;
                }
                mesazh = clsNivel.fshi();
                //int visibleIndex = gvKategoriDok.FindVisibleIndexByKeyValue(id);

                if (mesazh.Status)
                {
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("msgFshirjaMeSukses", cultinf), pnlMesazhi);
                    ASPxMenu1.Items.FindByText("Ruaj").ClientVisible = false;
                }
                else clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("msgFshirjaMeGabime", cultinf), pnlMesazhi);
            }            
            pnlMesazhi.Update();
            mbushListeNiveleDokumentash(idPerdorues);
            konfiguroGride(idPerdorues, idNderm);
            pnlKryesor.Update();
        }

        
        protected void gvKategoriDok_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            int idNderm = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            konfiguroVleraFillestare(idPerdoruesi);
            KonfigurimComboGride.shtoKonvertim(gvKategoriDok, idNderm, idPerdoruesi, Session, komponente, guidString, "Konvertohet");
        }

        protected void gvKategoriDok_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            System.Globalization.CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);

            dbRegjistrime = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
            DbCore.DbRegjistrim.clsNivelRegjistrimi nivel = new DbCore.DbRegjistrim.clsNivelRegjistrimi();
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idPerdorues = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            try
            {
                if (e.Keys.Count == 0) //shtim
                {
                    ASPxTextBox txtRadha = (ASPxTextBox)gvKategoriDok.FindEditFormTemplateControl("txtRadha");
                    ASPxComboBox cmbKat = (ASPxComboBox)gvKategoriDok.FindEditFormTemplateControl("cmbKategoria");
                    ASPxTextBox txtKodi = (ASPxTextBox)gvKategoriDok.FindEditFormTemplateControl("txtKodi");
                    ASPxTextBox txtPershkr = (ASPxTextBox)gvKategoriDok.FindEditFormTemplateControl("txtPershkrimi");

                    if (cmbKat.Value != null && cmbKat.Value.ToString() != "" && Convert.ToInt32(cmbKat.Value.ToString()) != -3)
                    {
                        nivel.IdKategori = Convert.ToInt32(cmbKat.Value.ToString());
                        nivel.IdNdermarje = idNdermarrje;

                        if (txtRadha.Text != "")
                        {
                            try
                            {
                                nivel.Radha = Convert.ToInt16(txtRadha.Text.ToString());
                            }
                            catch (NotFiniteNumberException err)
                            {
                                NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
                                e.RowError = rm.GetString("msgRadhaNumer", cultinf);
                                nivel.Radha = 0;
                            }
                        }                         
                        else
                            e.RowError = rm.GetString("msgRadhaNumer", cultinf);

                        DbCore.DbRegjistrim.colNivelRegjistrimi col = new DbCore.DbRegjistrim.colNivelRegjistrimi();
                        col.mbushGjitheNivelRegjistrimiSipasKategoriPlus(nivel.IdKategori, idNdermarrje, idPerdorues);                        
                        foreach (DbCore.DbRegjistrim.clsNivelRegjistrimi n in col)
                        {
                            if (n.Radha == nivel.Radha)
                            {
                                e.RowError = rm.GetString("msgEkzistonNjeNivelRegjistrimi", cultinf);
                                break;
                            }  
                        }

                        if (txtPershkr.Text == "")
                       e.RowError = rm.GetString("msgShkruaniPershkrimin", cultinf);

                        nivel.Kodi = txtKodi.Text;
                        if (txtKodi.Text != "")
                        {
                            if (dbRegjistrime.ekzistonKodNivel(nivel.IdKategori, nivel.Kodi, nivel.IdNdermarje).Status)
                                e.RowError = rm.GetString("msgEkzistonNjeNivelMeKeteKod", cultinf);
                            dbRegjistrime.Dispose();
                        }
                        else e.RowError = rm.GetString("msgShkruaniKodin", cultinf);
                    }
                    else
                        e.RowError = rm.GetString("msgPercaktoniKategorinPerNivel", cultinf); 
                }

                else //modifikim
                {
                    ASPxTextBox txtRadha = (ASPxTextBox)gvKategoriDok.FindEditFormTemplateControl("txtRadha");
                    ASPxComboBox cmbKat = (ASPxComboBox)gvKategoriDok.FindEditFormTemplateControl("cmbKategoria");
                    ASPxTextBox txtKodi = (ASPxTextBox)gvKategoriDok.FindEditFormTemplateControl("txtKodi");
                    ASPxTextBox txtPershkr = (ASPxTextBox)gvKategoriDok.FindEditFormTemplateControl("txtPershkrimi");
                    nivel.Kodi = txtKodi.Text;
                    nivel.IdNivel = int.Parse(e.Keys["IdNivel"].ToString());
                    nivel.IdNdermarje = idNdermarrje;
                    nivel.IdKategori = Convert.ToInt32(cmbKat.Value.ToString());
                   
                    try
                    {
                        nivel.Radha = Convert.ToInt16(txtRadha.Text.ToString());
                    }
                    catch (NotFiniteNumberException err)
                    {
                        NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
                        e.RowError = rm.GetString("msgRadhaNumer", cultinf);
                        nivel.Radha = 0;
                    }

                    DbCore.DbRegjistrim.colNivelRegjistrimi col = new DbCore.DbRegjistrim.colNivelRegjistrimi();
                    col.mbushGjitheNivelRegjistrimiSipasKategoriPlus(nivel.IdKategori, idNdermarrje, idPerdorues);
                    foreach (DbCore.DbRegjistrim.clsNivelRegjistrimi n in col)
                    {
                        if (n.Radha == nivel.Radha && n.IdNivel != nivel.IdNivel)
                        {
                            e.RowError = rm.GetString("msgEkzistonNjeNivelRegjistrimi", cultinf);
                            break;                        
                        }
                    }

                    if (txtPershkr.Text == "")
                        e.RowError = rm.GetString("msgShkruaniPershkrimin", cultinf);
                }
            }
            catch (Exception err)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
                e.RowError = rm.GetString("msgRadhaNumer", cultinf);
                nivel.Radha = 0;
            }
        }

        protected void gvKategoriDok_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            System.Globalization.CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);

            //DbCore.DbRegjistrim.clsDatabaseRegjistrim dbRegjistrime = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            for (int i = 0; i < e.NewValues.Count; i++)
            {
                if (e.NewValues[i] == null)
                {
                    e.Cancel = true;
                    return;
                }
            }
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), komponente);
            if (!tedrejtaInfo.DMod)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNukKeniTeDrejta", cultinf), pnlMesazhi);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session,rm.GetString("msgNukKeniTeDrejtaRed", cultinf));
                return;
            }
            DbCore.DbRegjistrim.clsNivelRegjistrimi nivel = new DbCore.DbRegjistrim.clsNivelRegjistrimi();
            //Kodi
            ASPxTextBox txtKodi = (ASPxTextBox)gvKategoriDok.FindEditFormTemplateControl("txtKodi");
            //Pershkrimi
            ASPxTextBox txtPershkr = (ASPxTextBox)gvKategoriDok.FindEditFormTemplateControl("txtPershkrimi");
            //Radha
            ASPxTextBox txtRadha = (ASPxTextBox)gvKategoriDok.FindEditFormTemplateControl("txtRadha");
            //Aktiv
            ASPxCheckBox chkAktiv = (ASPxCheckBox)gvKategoriDok.FindEditFormTemplateControl("chkAktiv");
            //Kategoria
            ASPxComboBox cmbKat = (ASPxComboBox)gvKategoriDok.FindEditFormTemplateControl("cmbKategoria");
            //Nivelet e konvertimit                
            ASPxDropDownEdit drdEdit = (ASPxDropDownEdit)gvKategoriDok.FindEditFormTemplateControl("ASPxDropDownEdit1");
            //Nr Serial Unik
            ASPxCheckBox cbNrSerialUnik = (ASPxCheckBox)gvKategoriDok.FindEditFormTemplateControl("cbNrSerialUnik");

            nivel.IdNivel = int.Parse(e.Keys["IdNivel"].ToString());
            nivel.Kodi = txtKodi.Text;
            nivel.IdKategori = Convert.ToInt32(cmbKat.Value.ToString());
            nivel.Pershkrimi = txtPershkr.Text;
            nivel.Aktiv = Convert.ToBoolean(chkAktiv.Checked);
            nivel.Radha = Convert.ToInt16(txtRadha.Text.ToString());
            nivel.IdNdermarje = idNdermarrje;
            nivel.IdStatusDok = 1;
            nivel.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            nivel.NrSerialUnik = Convert.ToBoolean(cbNrSerialUnik.Checked);
            string vlera = drdEdit.Text;
            string[] konvertimet = vlera.Split(';');
            string prmSql = ktheColKonvertimNivele(konvertimet);

            nivel.OColNivelRegjistrimi = new DbCore.DbRegjistrim.colNivelRegjistrimi();
            nivel.OColNivelRegjistrimi.mbushDataTableNivelRegjistrimiSipasKodeve(prmSql, idNdermarrje.ToString());
            //nivel.OColNivelRegjistrimi = dbRegjistrime.merrNivelRegjistrimiSipasKodeve(prmSql, idNderm.ToString());

            if (nivel.Kodi != "" && nivel.Pershkrimi != "")
            {
                e.Cancel = true;
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = nivel.modifiko();
                if (mesazh.Status)
                {
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session,rm.GetString("msgModifikimiMeSuksesGreen", cultinf));
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("msgRuajtjeMeSukses", cultinf), pnlMesazhi);
                }
                else
                {
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, rm.GetString("msgModifikimiMeGabimeRed", cultinf) );
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo,rm.GetString("labelRaportMesazhRuajtjaPerfundoiGabime", cultinf), pnlMesazhi);
                }
                gvKategoriDok.CancelEdit();
            }
        }

        private string ktheColKonvertimNivele(string[] konvertimet)
        {
            string parameterSql = "";
            for (int i = 0; i < konvertimet.Count(); i++)
            {
                parameterSql = parameterSql + "'" + konvertimet[i] + "'";
                //kur eshte elementi i fundit, mos shtohet presja ne fund
                if (i < (konvertimet.Count() - 1))
                {
                    parameterSql = parameterSql + ",";
                }
            }
            return parameterSql;
        }

        protected void cmbKategoria_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ASPxGridView gridView = (ASPxGridView)gvKategoriDok;
            ASPxComboBox cmbBox = (ASPxComboBox)gridView.FindEditFormTemplateControl("cmbKategoria");

            ASPxDropDownEdit drdEdit = (ASPxDropDownEdit)gridView.FindEditFormTemplateControl("ASPxDropDownEdit1");
            ASPxListBox lstBox = (ASPxListBox)drdEdit.FindControl("listBox");


            DbCore.DbRegjistrim.colNivelRegjistrimi colNivele = new DbCore.DbRegjistrim.colNivelRegjistrimi();
            //DbCore.DbRegjistrim.clsDatabaseRegjistrim dbRegjistrime = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();

            colNivele = DbCore.DbRegjistrim.clsNivelRegjistrimi.merrGjitheNivelRegjistrimiSipasKategori(idNderm, DbCore.mySessionObjects.ktheIdPerdoruesi(Session), Convert.ToInt32(cmbBox.Value.ToString()));

            lstBox.DataSource = colNivele;
            lstBox.DataBind();
        }

        protected void gvKategoriDok_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
        }

        protected void gvKategoriDok_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            Dictionary<string, object> values = new Dictionary<string, object>();
            if (e.RowType == GridViewRowType.EditForm)
            {
                values.Clear();
                ASPxGridView gridView = (ASPxGridView)sender;
                shto = gridView.IsNewRowEditing;


                if (shto)
                {

                }

                //Kodi
                ASPxTextBox txtBox = (ASPxTextBox)gridView.FindEditFormTemplateControl("txtKodi");
                values.Add("Kodi", txtBox.Text);
                if (txtBox.Text == "" && !shto)
                {
                    txtBox.Text = e.GetValue("Kodi").ToString();
                }
                //Pershkrimi
                txtBox = (ASPxTextBox)gridView.FindEditFormTemplateControl("txtPershkrimi");
                values.Add("Pershkrimi", txtBox.Text);
                if (txtBox.Text == "" && !shto)
                {
                    txtBox.Text = e.GetValue("Pershkrimi").ToString();
                }

                //Radha
                txtBox = (ASPxTextBox)gridView.FindEditFormTemplateControl("txtRadha");
                values.Add("Radha", txtBox.Text);
                if (txtBox.Text == "" && !shto)
                {
                    txtBox.Text = e.GetValue("Radha").ToString();
                }

                //Aktiv
                ASPxCheckBox chkBox = (ASPxCheckBox)gridView.FindEditFormTemplateControl("chkAktiv");
                values.Add("Aktiv", chkBox.Checked);
                if (chkBox.Value == null && !shto)
                {
                    chkBox.Checked = Convert.ToBoolean(e.GetValue("Aktiv").ToString());
                }
                else if (shto)
                    chkBox.Checked = true;

                //Nr Serial Unik
                ASPxCheckBox cbNrSerialUnik = (ASPxCheckBox)gridView.FindEditFormTemplateControl("cbNrSerialUnik");
                values.Add("Nr Serial Unik", cbNrSerialUnik.Checked);
                if (!shto)
                {
                    int idKat = Convert.ToInt32(e.GetValue("IdKategori").ToString());
                    if (idKat != 1 && idKat != 2 && idKat != 3 && idKat != 4)
                    {
                        cbNrSerialUnik.Checked = false;
                        cbNrSerialUnik.ClientEnabled = false;
                    }
                    else
                    {
                        cbNrSerialUnik.Checked = Convert.ToBoolean(e.GetValue("NrSerialUnik").ToString());
                        cbNrSerialUnik.ClientEnabled = true;
                    }
                }
                else if (shto)
                    cbNrSerialUnik.Checked = false;

                //Kategoria
                ASPxComboBox cmbBox = (ASPxComboBox)gridView.FindEditFormTemplateControl("cmbKategoria");
                if (cmbBox.Items.Count == 0)
                {
                    values.Add("IdKategori", cmbBox.Text);

                    DbCore.DbRegjistrim.colKategoriNiveleDok colKategori = new DbCore.DbRegjistrim.colKategoriNiveleDok();
                    colKategori.mbushGjitheKategoriNivelDok();
                    //colKategori = dbRegjistrime.merrGjitheKategoriNivelDok(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    cmbBox.TextField = "Pershkrimi";
                    cmbBox.ValueField = "IdKategori";

                    cmbBox.DataSource = colKategori;
                    cmbBox.DataBind();
                    if (!shto)
                    {
                        int idKat = Convert.ToInt32(e.GetValue("IdKategori").ToString());
                        DbCore.DbRegjistrim.clsKategoriNivelDok oKat = new DbCore.DbRegjistrim.clsKategoriNivelDok();
                        oKat.IdKategori = idKat;
                        oKat = oKat.merrSipasId();
                        cmbBox.Text = oKat.Pershkrimi;
                    }

                }


                //Nivelet e konvertimit                
                ASPxDropDownEdit drdEdit = (ASPxDropDownEdit)gridView.FindEditFormTemplateControl("ASPxDropDownEdit1");
                ASPxListBox lstBox = (ASPxListBox)drdEdit.FindControl("listBox");
                if (drdEdit.Text == "")
                {
                    values.Add("OColNivelRegjistrimi", drdEdit.Text);
                    //marrim idKategori qe ne combon e Konvertimeve te shfaqen vetem nivelet qe i perkasin kesaj kategorie
                    DbCore.DbRegjistrim.clsNivelRegjistrimi oNivele = new DbCore.DbRegjistrim.clsNivelRegjistrimi();
                    DbCore.DbRegjistrim.colNivelRegjistrimi colNivele = new DbCore.DbRegjistrim.colNivelRegjistrimi();

                    DbCore.DbRegjistrim.colNivelRegjistrimi colNiveleMagazina = new DbCore.DbRegjistrim.colNivelRegjistrimi();
                    DbCore.DbRegjistrim.clsNivelRegjistrimi oNiveleMagazina = new DbCore.DbRegjistrim.clsNivelRegjistrimi();
                    DbCore.DbRegjistrim.clsNivelRegjistrimi oNiveleMagazina2 = new DbCore.DbRegjistrim.clsNivelRegjistrimi();

                    //cdo nivel i kategorise shitje apo blerje mund te konvertohet edhe ne cdo nivel te kategorise magazine
                    oNiveleMagazina.IdKategori = 6; oNiveleMagazina2.IdKategori = 6;
                    int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                    if (!shto || cmbBox.Value != null)
                    {
                        oNivele.IdKategori = Convert.ToInt32(cmbBox.Value.ToString());
                        colNivele = DbCore.DbRegjistrim.clsNivelRegjistrimi.merrGjitheNivelRegjistrimiSipasKategori(idNderm, idPerdoruesi, Convert.ToInt32(cmbBox.Value.ToString()));
                        if (oNivele.IdKategori == 1)
                        {
                            oNiveleMagazina.mbushNivelRegjistrimiSipasKoditPaKonvertime("FD", idNderm);
                            colNivele.Add(oNiveleMagazina);
                            oNiveleMagazina2.mbushNivelRegjistrimiSipasKoditPaKonvertime("UD", idNderm);
                            colNivele.Add(oNiveleMagazina2);
                            colNivele.AddRange(DbCore.DbRegjistrim.clsNivelRegjistrimi.merrGjitheNivelRegjistrimiSipasKategori(idNderm, idPerdoruesi, 2));

                        }
                        else if (oNivele.IdKategori == 2)
                        {
                            //    DbCore.DbRegjistrim.colNivelRegjistrimi nivelet = new DbCore.DbRegjistrim.colNivelRegjistrimi();
                            //    nivelet.mbushNivelRegjistrimiSipasKodeve("FH,UH", idNderm);
                            //    foreach (DbCore.DbRegjistrim.clsNivelRegjistrimi niv in nivelet)
                            //        colNivele.Add(niv);
                            oNiveleMagazina.mbushNivelRegjistrimiSipasKoditPaKonvertime("FH", idNderm);
                            colNivele.Add(oNiveleMagazina);
                            oNiveleMagazina2.mbushNivelRegjistrimiSipasKoditPaKonvertime("UH", idNderm);
                            colNivele.Add(oNiveleMagazina2);
                        }
                        else if (oNivele.IdKategori == 78)
                        {
                            oNiveleMagazina.mbushNivelRegjistrimiSipasKoditPaKonvertime("FD", idNderm);
                            colNivele.Add(oNiveleMagazina);
                            colNiveleMagazina = DbCore.DbRegjistrim.clsNivelRegjistrimi.merrGjitheNivelRegjistrimiSipasKategori(idNderm, idPerdoruesi, 1);
                            colNivele.AddRange(colNiveleMagazina);
                        }
                        else if (oNivele.IdKategori == 171)
                        {
                            oNiveleMagazina.mbushNivelRegjistrimiSipasKoditPaKonvertime("MB", idNderm);
                            colNivele.Add(oNiveleMagazina);
                        }
                        else if (oNivele.IdKategori == 170)
                        {
                            oNiveleMagazina.mbushNivelRegjistrimiSipasKoditPaKonvertime("AB", idNderm);
                            colNivele.Add(oNiveleMagazina);
                        }
                        else if (oNivele.IdKategori == 179)
                        {
                            oNiveleMagazina.mbushNivelRegjistrimiSipasKoditPaKonvertime("EB", idNderm);
                            colNivele.Add(oNiveleMagazina);
                            oNiveleMagazina2.mbushNivelRegjistrimiSipasKoditPaKonvertime("IB", idNderm);
                            colNivele.Add(oNiveleMagazina2);
                        }
                    }
                    else if (shto && cmbBox.Value == null)
                        colNivele = oNivele.merrGjitheNivelRegjistrimi(idNderm, idPerdoruesi);

                    lstBox.TextField = "Kodi";
                    lstBox.ValueField = "IdNivel";
                    lstBox.DataSource = colNivele;
                    lstBox.DataBind();

                    if (!shto)
                    {
                        //duhet te chekohen konvertimet ne listBox sipas vlerave te ruajtura ne DB per kete nivel
                        DbCore.DbRegjistrim.clsNivelRegjistrimi oN = new DbCore.DbRegjistrim.clsNivelRegjistrimi();
                        oN.IdNivel = Convert.ToInt32(e.GetValue("IdNivel").ToString());
                        oN.mbushNivelRegjistrimiSipasIdMeKonvertime(oN.IdNivel);


                        //DbCore.DbRegjistrim.clsDatabaseRegjistrim dbregj = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
                        string ndares = "";
                        for (int i = 0; i < lstBox.Items.Count; i++)
                        {
                            DbCore.DbRegjistrim.clsNivelRegjistrimi obj = new DbCore.DbRegjistrim.clsNivelRegjistrimi();
                            int idNiv = Convert.ToInt32(lstBox.Items[i].Value.ToString());
                            foreach (DbCore.DbRegjistrim.clsNivelRegjistrimi o in oN.OColNivelRegjistrimi)
                            {
                                if (o.IdNivel == idNiv)
                                {
                                    lstBox.Items[i].Selected = true;
                                    drdEdit.Text = drdEdit.Text + ndares + o.Kodi;
                                    ndares = ";";
                                }
                            }
                        }
                    }
                }
            }
        }

        protected void gvKategoriDok_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
        }

        protected void gvKategoriDok_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            System.Globalization.CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);

            //DbCore.DbRegjistrim.clsDatabaseRegjistrim dbRegjistrime = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
            for (int i = 0; i < e.NewValues.Count; i++)
            {
                if (e.NewValues[i] == null)
                {
                    e.Cancel = true;
                    return;
                }
            }
            int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), komponente);
            if (!tedrejtaInfo.DShtim)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNukKeniTeDrejta", cultinf), pnlMesazhi);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, rm.GetString("msgNukKeniTeDrejtaRed", cultinf));
                return;
            }
            DbCore.DbRegjistrim.clsNivelRegjistrimi nivel = new DbCore.DbRegjistrim.clsNivelRegjistrimi();
            //Kodi
            ASPxTextBox txtKodi = (ASPxTextBox)gvKategoriDok.FindEditFormTemplateControl("txtKodi");
            //Pershkrimi
            ASPxTextBox txtPershkr = (ASPxTextBox)gvKategoriDok.FindEditFormTemplateControl("txtPershkrimi");
            //Radha
            ASPxTextBox txtRadha = (ASPxTextBox)gvKategoriDok.FindEditFormTemplateControl("txtRadha");
            //Aktiv
            ASPxCheckBox chkAktiv = (ASPxCheckBox)gvKategoriDok.FindEditFormTemplateControl("chkAktiv");
            //Kategoria
            ASPxComboBox cmbKat = (ASPxComboBox)gvKategoriDok.FindEditFormTemplateControl("cmbKategoria");
            //Nivelet e konvertimit                
            ASPxDropDownEdit drdEdit = (ASPxDropDownEdit)gvKategoriDok.FindEditFormTemplateControl("ASPxDropDownEdit1");
            //Nr Serial Unik
            ASPxCheckBox cbNrSerialUnik = (ASPxCheckBox)gvKategoriDok.FindEditFormTemplateControl("cbNrSerialUnik");

            nivel.IdNivel = 0;
            nivel.Kodi = txtKodi.Text;
            nivel.IdKategori = Convert.ToInt32(cmbKat.Value.ToString());
            nivel.Pershkrimi = txtPershkr.Text;
            nivel.Aktiv = Convert.ToBoolean(chkAktiv.Checked);
            nivel.Radha = Convert.ToInt32(txtRadha.Text.ToString());
            nivel.NrSerialUnik = Convert.ToBoolean(cbNrSerialUnik.Checked);
            nivel.IdNdermarje = idNdermarrje;
            nivel.IdStatusDok = 1;
            nivel.IdPerdoruesi = idPerdoruesi;
            string vlera = drdEdit.Text;
            string[] konvertimet = vlera.Split(';');
            string prmSql = ktheColKonvertimNivele(konvertimet);

            nivel.OColNivelRegjistrimi = new DbCore.DbRegjistrim.colNivelRegjistrimi();
            nivel.OColNivelRegjistrimi.mbushDataTableNivelRegjistrimiSipasKodeve(prmSql, idNdermarrje.ToString());
            //nivel.OColNivelRegjistrimi = dbRegjistrime.merrNivelRegjistrimiSipasKodeve(prmSql, idNderm.ToString());
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            if (nivel.Kodi != "" && nivel.Pershkrimi != "")
            {
                e.Cancel = true;
                mesazh = nivel.ruajNivelPlusKonvertim();
                
                if (mesazh.Status == true)
                {
                    gvKategoriDok.CancelEdit();
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, rm.GetString("msgRuajtjeMeSuksesGreen", cultinf));
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("mesazhRuajtjeMeSukses", cultinf), pnlMesazhi);
                }
                else
                {                    
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, rm.GetString("msgNdodhiGabimRed", cultinf));
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo,rm.GetString("labelRaportMesazhRuajtjaPerfundoiGabime", cultinf), pnlMesazhi);
                }
                              
                konfiguroVleraFillestare(idPerdoruesi);
                konfiguroGride(idPerdoruesi, idNderm);
            }
        }

        protected void gvKategoriDok_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            if (!gvKategoriDok.IsNewRowEditing)
            {
                gvKategoriDok.DoRowValidation();
            }
        }

        protected void gvKategoriDok_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
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

        protected void gvKategoriDok_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if (e.Column.FieldName == "IdKategori")
            {
                if (Converter.ConvertToInt(e.Value) == -3)
                {
                    e.Criteria = null;
                }
            }
        }

        protected void gvKategoriDok_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName == "Aktiv")
            {
                (e.Editor as ASPxComboBox).Items.Clear();
                (e.Editor as ASPxComboBox).Items.Add("");
                (e.Editor as ASPxComboBox).Items.Add("Po", true);
                (e.Editor as ASPxComboBox).Items.Add("Jo", false);
            }
        }

        protected void ruaj_Button_Click(object sender, EventArgs e)
        {
            gvKategoriDok.UpdateEdit();

           
        }


        protected void gvKategoriDok_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            int idPerdorues = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);            
            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                if (arr[2] == "")
                    gvKategoriDok.FilterExpression = "";
                else
                {
                    //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    var idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(base.Session), "gvKategoriDok", komponente, idNdermarrje);
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], idNdermarrje, koka.IdGridaKoka);
                    //filtra.mbushFiltraGridaSipasFiltraKodi(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    //DbCore.DbAdmin.clsFiltraGrida filtra = dbAdmin.merrFiltraGridaSipasFiltraKodi(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    if (filtra.FiltraKodi != null)
                    {
                        gvKategoriDok.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvKategoriDok);

                        konfiguroVleraFillestare(idPerdorues);
                    }
                }
            }
            konfiguroGride(idPerdorues, idNderm);
        }

  
    }
}