using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Collections;
using System.Drawing;
using DevExpress.Web.ASPxTreeList;
using System.Data;
using System.Globalization;
using System.Resources;
using DbCore.IMBUtils.Types;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{
    /// <summary>
    /// Kjo klase sherben per te shfaqur listen e autorizimet dhe per te ruajtur autorizimet e reja
    /// </summary>
    public partial class Shto_Autorizimet : MyPageBase
    {
        DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();
        private string prefixMesazhNjejes = "Autorizimi me Kod: ";
        private string prefixMesazhShumes = "Autorizimet me Kode: ";
        private string suffixMesazhNjejesGabimi = " eshte i lidhur dhe nuk mund te fshihet";
        private string suffixMesazhShumesGabimi = " jane te lidhur dhe nuk mund te fshihen";
        private string suffixMesazhNjejesSuksesi = " u fshi me sukses!";
        private string suffixMesazhShumesSuksesi = " u fshine me sukses!";
        private string lidhesMesazhi = ". Kurse ";
        private string mesazhZgjidhniNje = "Ju lutem zgjidhni te pakten nje autorizim!";
        private string komponente = "Shto_Autorizimet.aspx";
        private string guidString;

        /// <summary>
        /// thirret kur faqja lodohet. Ne te behet kontrolli nese perdoruesi eshte i loguar ne sistem  dhe nqs jo ridrejtohet tek forma e logimit
        /// thirret inicializimi i konfigurimeve fillestare te faqes
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e"> argumenti</param>
        protected void Page_Load(object sender, EventArgs e)
        {//kontrollon nese perdoruesi eshte i loguar
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                DbCore.clsFunksione.logout(Session, true, "FaqePaautorizuar");
            }


            oPerdorues = DbCore.mySessionObjects.kthePerdorues(Session);
            int idviti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            hfState.Set("idPerdoruesi", IdPerdoruesi);
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            percaktoTemplateMenu(idviti, IdPerdoruesi, idNdermarrje, ASPxMenu1);

            if (!Page.IsPostBack)
            {
                guidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                hfState.Set("guidString", guidString);
                hfId.Value = "0";
                EmrateTabeve();
                perkthelabel();
                konfiguroVleraFillestare(IdPerdoruesi, IdGjuha);

                if (Request.QueryString["indexrow"] != null)
                {
                    ASPxGridView_Autorizimet.FocusedRowIndex = int.Parse(Request.QueryString["indexrow"]);
                }


                mbushGridAutorizimeshNgaDB();
                konfiguroGrideLidhjeAutorizim();
                konfiguroGridePerdoruesish();
                GridUtil.percaktoVisibleColumnsMeWidth(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, grid_ListPerdoruesit, "grid_ListPerdoruesit", komponente);
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, idNdermarrje, idviti, komponente);
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                konfiguroGride("", 228);
                GridUtil.percaktoVisibleColumnsMeWidth(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, ASPxGridView_Autorizimet, "ASPxGridView_Autorizimet", komponente);

            }
            else
            {
                guidString = (string)hfState["guidString"];
                mbushGridAutorizimeshNgaSession();
                konfiguroGrideLidhjeAutorizim();
                konfiguroGridePerdoruesish();
                konfiguroGride("", 228);
            }
            mbushListePerdoruesish(IdPerdoruesi);
            mbushLidhjeAutorizimet();
            GridUtil.konfigGrideListeEMadhePaTheme(ASPxGridView_Autorizimet, "IdAutorizimKoka");
            clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, idNdermarrje, "ASPxGridView_Autorizimet", 1, komponente);
        }

        /// <summary>
        /// Vendos emrat e tabeve ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateTabeve()
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            ASPxPageControl1.TabPages[0].Text = rm.GetString("labelBlerjeShitjeTePergjithshme", cultinf);
            ASPxPageControl1.TabPages[1].Text = rm.GetString("MenuItemAutorizimet", cultinf);
            ASPxPageControl1.TabPages[2].Text = rm.GetString("tabAutorizimetLidhja", cultinf);
            ((ASPxButton)ASPxPageControl1.TabPages[1].FindControl("ASPxButton1")).Text = rm.GetString("btnSelektoTeGjithaNeKeteFaqe", cultinf);
            ((ASPxButton)ASPxPageControl1.TabPages[1].FindControl("ASPxButton2")).Text = rm.GetString("btnHiqGjitheSelektimetNeKeteFaqe", cultinf);
            ((ASPxLabel)ASPxPageControl1.TabPages[1].FindControl("lblKodi")).Text = rm.GetString("lblKodi", cultinf);
            ((ASPxLabel)ASPxPageControl1.TabPages[1].FindControl("lblPershkrimi")).Text = rm.GetString("lblPershkrimi", cultinf);
            popFshi.HeaderText = rm.GetString("labelKujdes", cultinf);
            lblMsgbox.Text = rm.GetString("labelAdministrimiMsgJeniSigurt", cultinf);
            ButtonOk.Text = rm.GetString("labelOk", cultinf);
            ButtonCancel.Text = rm.GetString("btnCancel", cultinf);
        }
        public void perkthelabel()
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            prefixMesazhNjejes = rm.GetString("msgAutorizimiMekod", cultinf);
            prefixMesazhShumes = rm.GetString("msgAutorizimiMekode", cultinf);
            suffixMesazhNjejesGabimi = rm.GetString("msgBlerjeShitjeNukFshihetNjejes", cultinf);
            suffixMesazhShumesGabimi = rm.GetString("msgBlerjeShitjeNukFshihetShumes", cultinf);
            suffixMesazhNjejesSuksesi = rm.GetString("suffixMesazhNjejesSuksesi", cultinf);
            suffixMesazhShumesSuksesi = rm.GetString("suffixMesazhShumesSuksesi", cultinf);
            lidhesMesazhi = rm.GetString("msgLidhesMesazhi", cultinf);
            mesazhZgjidhniNje = rm.GetString("msgZgjidhAutorizim", cultinf);
            hfState.Set("msgZgjidhNjeAutorizim", rm.GetString("msgZgjidhNjeAutorizim", cultinf));
            // grid_ListPerdoruesit.SettingsText.Title = rm.GetString("gridPerdoruesit", cultinf);
        }


        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="idNderVit"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        private void percaktoTemplateMenu(int idViti, int idPerdorues, int idNdermarrje, ASPxMenu aSPxMenu1)
        {
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, komponente, this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, hfShtimModifikim.Value == "modifikim" ? false : true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
        }


        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), ASPxMenu1);

        }
        protected void ASPxGridView_Autorizimet_DataBound(object sender, EventArgs e)
        {// shton colonen # per selektim dhe disa karakteristika te grides
            if (this.ASPxGridView_Autorizimet.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per 
                //perzgjidh
                DevExpress.Web.GridViewCommandColumn check = new DevExpress.Web.GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true;
                check.Width = Unit.Percentage(3);
                // check.SetColVisibleIndex(0);
                //behet per te afishuar rreshtin qe do sherbej per filtrim
                ASPxGridView_Autorizimet.Settings.ShowFilterRow = true;
                ASPxGridView_Autorizimet.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                ASPxGridView_Autorizimet.Settings.ShowFilterRowMenu = true;
                ASPxGridView_Autorizimet.Columns.Add(check);

                ASPxGridView_Autorizimet.KeyFieldName = "IdAutorizimKoka";
                ASPxGridView_Autorizimet.SettingsBehavior.AllowSelectByRowClick = true;

                ASPxGridView_Autorizimet.SettingsBehavior.AllowFocusedRow = true;
            }
        }
        private void konfiguroGride(string kodKonfigurimi, int idKomponente)
        {
            this.ASPxGridView_Autorizimet.Columns["#"].VisibleIndex = 0;
        }
        /// <summary>
        /// inicializon gridat e nderfaqes
        /// </summary>
        private void konfiguroVleraFillestare(int idPerdoruesi, int idGjuha)
        {
            ASPxPageControl1.ActiveTabIndex = 0;
            inicializoObjekte();
            mbushListePerdoruesish(idPerdoruesi);
            mbushLidhjeAutorizimet();
        }
        private void mbushGridAutorizimeshNgaSession()
        {
            DataTable tmpObject;
            bool sukses = DbCore.mySessionObjects.merrGrideNgaSessioni(Session, out tmpObject);
            if (!sukses)
                mbushGridAutorizimeshNgaDB();
            else
            {
                ASPxGridView_Autorizimet.DataSource = tmpObject;
                ASPxGridView_Autorizimet.DataBind();
                tmpObject.Dispose();
            }
        }
        private void mbushGridAutorizimeshNgaDB()
        {//mbush griden e popupit me te dhena            
            DataTable dt = DbCore.DbAdmin.colAutorizimetKoka.merrGjitheAutorizimet(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), (int)hfState.Get("idPerdoruesi"));
            DbCore.mySessionObjects.ruajGrideNeSession(Session, dt);
            ASPxGridView_Autorizimet.DataSource = dt;
            ASPxGridView_Autorizimet.DataBind();
            dt.Dispose();
        }
        /// <summary>
        /// inicializon objektin DbCore.DbAdmin.clsDatabaseAdmin
        /// </summary>
        private void inicializoObjekte()
        {
            //dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
        }

        /// <summary>
        /// perdoret per te ruajtur nje autorizim ne databaze
        /// </summary>
        /// :<see cref="DbCore.DbAdmin.clsAutorizimKoka.ruaj()"/>
        private void ruajAutorizim()
        {
            DbCore.DbAdmin.clsAutorizimKoka autorizim;
            //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            if (Page.IsValid == false)
                return;
            else
            {
                if (isValidAutorizim())
                {
                    autorizim = krijoAutorizim();
                    bool eshteShtim;
                    DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

                    tedrejtaInfo.merrTeDrejtaPerKeteKomponente(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), komponente);

                    if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim")
                    {
                        if (!tedrejtaInfo.DShtim)
                        {
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                            hfStatusi.Value = "false";
                            return;
                        }
                        mesazh = autorizim.ruaj();
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
                        autorizim.IdAutorizimKoka = int.Parse(hfId.Value.ToString());
                        mesazh = autorizim.modifiko();
                        eshteShtim = false;
                    }
                    if (mesazh.Status == true)
                    {
                        clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["mesazhRuajtjeMeSukses"], pnlMesazhi);
                        if (eshteShtim)
                            shtoAutorizimNeGrid(autorizim.IdAutorizimKoka);
                        else //modifikim
                            modifikoAutorizimNeGrid(autorizim.IdAutorizimKoka);
                        hfStatusi.Value = "true";
                        pastroFusha();
                        ASPxPageControl1.ActiveTabIndex = 0;
                    }
                    else
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ndodhi nje gabim. Ruajtja nuk u krye!", pnlMesazhi);
                        hfStatusi.Value = "false";
                    }



                }
            }
        }
        /// <summary>
        /// perdoret per te pastruar fushat e nderfaqes
        /// </summary>
        private void pastroFusha()
        {//pastron fushat
            //pergjigja.Text = "";
            txtKodi.Text = "";
            txtPershkrimi.Text = "";
            grid_ListPerdoruesit.FilterExpression = "";
            grid_ListPerdoruesit.Selection.UnselectAll();

        }
        /// <summary>
        /// krijon nje objekt clsAutorizimKoka sipas te dhenave qe ka futur perdoruesi
        /// </summary>
        /// <returns></returns>
        private DbCore.DbAdmin.clsAutorizimKoka krijoAutorizim()
        {//krijon nje autorizim te ri sipas te dhenave te plotesuara nga perdoruesi
            DbCore.DbAdmin.clsAutorizimKoka autorizim = new DbCore.DbAdmin.clsAutorizimKoka();
            autorizim.KodiAutorizim = txtKodi.Text;
            autorizim.PershkrimAutorizim = txtPershkrimi.Text;
            autorizim.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            List<object> rreshtat = this.grid_ListPerdoruesit.GetSelectedFieldValues("IdPerdorues");
            autorizim.OColTrupi = new DbCore.DbAdmin.colAutorizimetTrupi();
            autorizim.IdNdermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            autorizim.IdStatusDok = 1;
            foreach (int id in rreshtat)
            {
                DbCore.DbAdmin.clsAutorizimTrupi trupi = new DbCore.DbAdmin.clsAutorizimTrupi();
                trupi.IdPerdorues = id;
                autorizim.OColTrupi.Add(trupi);
            }

            return autorizim;
        }

        //kontrollon nese ekziston kodi i autorizimit
        private bool isValidAutorizim()
        {
            DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
           
            if (dbAdmin.ekzistonAutorizim(txtKodi.Text) && (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim"))
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ky kod ekziston ne ndermarrjen aktuale ose ne nje ndermarrje tjeter nga e cila eshte çelur!", pnlMesazhi);
                return false;
            }
         
            List<object> rreshtat = this.grid_ListPerdoruesit.GetSelectedFieldValues("IdPerdorues");
            if (rreshtat.Count < 1)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Nuk mund te celni nje autorizim pa asnje perdorues", pnlMesazhi);
                return false;
            }

            dbAdmin.Dispose();
            return true;

        }

        public void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            int idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
            //filtra.mbushFiltraGridaSipasFiltraEmri(cmbFiltra.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuha, "ASPxGridView_Autorizimet", komponente, idNdermarrje);
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            //DbCore.DbAdmin.clsFiltraGrida filtra = dbAdmin.merrFiltraGridaSipasFiltraEmri(cmbFiltra.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = idPerdoruesi;
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();
                //mbushComboBoxFiltra();
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "ASPxGridView_Autorizimet", 1, komponente);
                percaktoTemplateMenu(DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), idPerdoruesi, idNdermarrje, ASPxMenu1);

                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                //ASPxButton btnRuaj = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("Button1") as ASPxButton;
                //ASPxButton btnFshiFilter = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFshi") as ASPxButton;

                //  btnRuaj.ClientEnabled = false;
                //  btnFshiFilter.ClientEnabled = false;
                konfiguroVleraFillestare(idPerdoruesi, idGjuha);
                hfStatusi.Value = "true";
                ASPxGridView_Autorizimet.FilterExpression = String.Empty;
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
            //DbCore.DbAdmin.clsGridaKoka koka = dbAdmin.merrGridaKokaByEmri("ASPxGridView_Autorizimet", "Shto_Llogari.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "ASPxGridView_Autorizimet", komponente, idNdermarrje);
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = ASPxGridView_Autorizimet.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("KodiAutorizim", ASPxGridView_Autorizimet);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = ASPxGridView_Autorizimet.GetSortedColumns();
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
            //    filtri.KoloneRenditje = "KodiAutorizim";
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
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "ASPxGridView_Autorizimet", 1, komponente);
            percaktoTemplateMenu(DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje, ASPxMenu1);

            if (mesazh.Status == true)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            hfStatusi.Value = "true";
            cmbFiltra.Text = "";
            //ASPxButton btnRuaj = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("Button1") as ASPxButton;
            //ASPxButton btnFshiFilter = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFshi") as ASPxButton;

            //  btnRuaj.ClientEnabled = false;
            //  btnFshiFilter.ClientEnabled = false;
            //Kodi_ASPxTextBox.Text = "";
            //Shenime_ASPxTextBox.Text = "";
            ////Universal_ASPxCheckBox.Text = "";
            //popRuaj.ShowOnPageLoad = false;
            //}
        }
        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            List<object> rreshtat;
            if (ASPxPageControl1.ActiveTabIndex == 0)//nqs kemi zgjedhur vetem nje kartele per te pare fshijme vetem ate
                rreshtat = ASPxGridView_Autorizimet.GetSelectedFieldValues("IdAutorizimKoka");
            else
            {
                rreshtat = new List<object>();
                rreshtat.Add(hfId.Value);
            }
            //List<object> rreshtat = ASPxGridView_Autorizimet.GetSelectedFieldValues("IdAutorizimKoka");
            if (rreshtat.Count == 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhZgjidhniNje, pnlMesazhi);
                return;
            }
            bool kaveprim = false;
            //ben fshirjen e nje autorizimi
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            List<string> TeFshire = new List<string>(), TePaFshire = new List<string>();
            DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            //clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "", pnlMesazhi);
            foreach (object id in rreshtat)
            {
                //DbCore.DbAdmin.colAutorizimetKoka colAutorizime = dbAdmin.ktheAutorizim(id);
                DbCore.DbAdmin.clsAutorizimKoka clsAutorizime = new DbCore.DbAdmin.clsAutorizimKoka(Convert.ToInt32(id));
                clsAutorizime.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                //foreach (DbCore.DbAdmin.clsAutorizimKoka l in colAutorizime)
                //{
                kaveprim = dbAdmin.kaVeprimeAutorizim(clsAutorizime.IdAutorizimKoka);
                if (kaveprim)
                {
                    TePaFshire.Add(clsAutorizime.KodiAutorizim);
                    continue;
                }
                clsAutorizime.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                mesazh = clsAutorizime.fshi();
                if (clsAutorizime.IdAutorizimKoka == 0)
                    continue;
                if (mesazh.Status)
                {
                    #region Heq llogarite nga grida

                    // ASPxGridView_Artikull.DataSource = dt;
                    hiqAutorizimNgaGrida(clsAutorizime.IdAutorizimKoka);
                    #endregion
                    TeFshire.Add(clsAutorizime.KodiAutorizim);

                    ASPxPageControl1.ActiveTabIndex = 0;
                    hfStatusi.Value = "true";
                }


                //}
            }
            dbAdmin.Dispose();
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
            ASPxGridView_Autorizimet.Selection.UnselectAll();
            pnlMesazhi.Update();
        }

        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {
            if (e.Item.Name == "Ruaj")
            {
                Page.Validate("entries"); ruajAutorizim();
            }
        }



        #region grida e autorizimeve
        /// <summary>
        /// perdoret ne rastet kur grida ben callback per ta rimbushur griden
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void ASPxGridView_Autorizimet_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            if (e.CallbackName == "APPLYFILTER" && e.Args[0] == "")
            {
                DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
                ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
                cmbFiltra.Text = "";
            }
        }

        protected void ASPxGridView_Autorizimet_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            if (e.Column.FieldName == "KodiAutorizim" || e.Column.FieldName == "PershkrimAutorizim")
            {
                e.Values.Clear();
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
        protected void ASPxGridView_Autorizimet_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {

        }
        protected void ASPxGridView_Autorizimet_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = ASPxGridView_Autorizimet.PageIndex;
            e.Properties["cpPageRow"] = ASPxGridView_Autorizimet.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = ASPxGridView_Autorizimet.VisibleRowCount;
        }
        protected void ASPxGridView_Autorizimet_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string idkomponente = "";
            string kodkonfigurimi = "";

            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                kodkonfigurimi = arr[1];
                idkomponente = arr[0];
                if (arr[2] == "")
                    ASPxGridView_Autorizimet.FilterExpression = "";
                else
                {
                    //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    int idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
                    //filtra.mbushFiltraGridaSipasFiltraKodi(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuha, "ASPxGridView_Autorizimet", komponente, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
                    //DbCore.DbAdmin.clsFiltraGrida filtra = dbAdmin.merrFiltraGridaSipasFiltraKodi(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    if (filtra.FiltraKodi != null)
                    {
                        ASPxGridView_Autorizimet.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, ASPxGridView_Autorizimet);

                        konfiguroVleraFillestare(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idGjuha);
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
            ASPxGridView_Autorizimet.Selection.UnselectAll();
        }
        private void hiqAutorizimNgaGrida(int idautorizim)
        {
            if (this.ASPxGridView_Autorizimet.DataSource != null)
            {
                DataTable dt = (DataTable)ASPxGridView_Autorizimet.DataSource;
                DataRow[] drs = dt.Select("IdAutorizimKoka = " + idautorizim);
                if (drs.Length > 1)
                    throw new Exception("GABIM: Ndodhen 2 autorizime me te njejten id ne gride");
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                dt.Rows.Remove(dr);
                ASPxGridView_Autorizimet.DataBind();
            }
            else mbushGridAutorizimeshNgaDB();
        }
        private void shtoAutorizimNeGrid(int idautorizim)
        {
            if (ASPxGridView_Autorizimet.DataSource != null)
            {
                DataTable dt = (DataTable)ASPxGridView_Autorizimet.DataSource;
                DataRow[] drs = dt.Select("IdAutorizimKoka = " + idautorizim);
                if (drs.Length > 0)
                    throw new Exception("GABIM: Autorizimi ekziston ne gride");
                DataRow newArtDr = DbCore.DbAdmin.colAutorizimetKoka.merrAutorizimDR(idautorizim);
                dt.ImportRow(newArtDr);
            }
            else mbushGridAutorizimeshNgaDB();
        }
        private void modifikoAutorizimNeGrid(int idautorizim)
        {
            if (ASPxGridView_Autorizimet.DataSource != null)
            {
                DataTable dt = (DataTable)ASPxGridView_Autorizimet.DataSource;
                DataRow[] drs = dt.Select("IdAutorizimKoka = " + idautorizim);
                if (drs.Length > 1)
                    throw new Exception("GABIM: Ndodhen 2 autorizime me te njejten id ne gride");
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                DataRow newArtDr = DbCore.DbAdmin.colAutorizimetKoka.merrAutorizimDR(idautorizim);
                //  dt.Rows.Remove(dr);
                // dt.ImportRow(newArtDr);
                object[] arr = newArtDr.ItemArray;
                dr.ItemArray = arr;
            }
            else mbushGridAutorizimeshNgaDB();
        }
        /// <summary>
        /// metoda per te thirrur veprimet e menuse kur shtypen butonat
        /// </summary>
        /// <param name="source">derguesi</param>
        /// <param name="e">parametrat</param>
        #endregion


        #region grid Liste Perdoruesish 


        /// <summary>
        /// perdoret per te shfaqur emertimet e kolonave ne vend te id si dhe filtrat te shfaqet ne forme komboje
        /// </summary>
        private void shtoKolonaCombo()
        {
            KonfigurimComboGride.ShtoQytetet(grid_ListPerdoruesit, -1, Session, komponente, guidString, "IdQyteti");
        }


        /// <summary>
        /// perdoret ne rastet kur grida ben callback per ta rimbushur griden
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void grid_ListPerdoruesit_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            mbushListePerdoruesish(DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
        }

        /// <summary>
        /// perdoret ne rastet kur grida ben callback per ta rimbushur griden
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e"> argumentat</param>


        /// <summary>
        /// perdoret per te shfaqur aktiv /Jo aktiv ne vend te true/ false
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void grid_ListPerdoruesit_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName == "PerdoruesAktiv")
            {
                (e.Editor as ASPxComboBox).Items.Clear();
                (e.Editor as ASPxComboBox).Items.Add("");
                (e.Editor as ASPxComboBox).Items.Add("Aktiv", true);
                (e.Editor as ASPxComboBox).Items.Add("Jo Aktiv", false);
            }
        }
        /// <summary>
        /// perdoret per te shtuar kolonen e selektimit tek grida dhe per te vendosur disa karakteristika te grides
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void grid_ListPerdoruesit_DataBound(object sender, EventArgs e)
        {
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (grid_ListPerdoruesit.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per 
                //perzgjidh
                DevExpress.Web.GridViewCommandColumn check = new DevExpress.Web.GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true;
                check.Width = Unit.Percentage(3);
                // check.SetColVisibleIndex(0);
                //behet per te afishuar rreshtin qe do sherbej per filtrim
                grid_ListPerdoruesit.Settings.ShowFilterRow = true;
                grid_ListPerdoruesit.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                grid_ListPerdoruesit.Settings.ShowFilterRowMenu = true;
                grid_ListPerdoruesit.Columns.Add(check);
                //grid_ListPerdoruesit.Columns["IdPerdorues"].Visible = false;
                grid_ListPerdoruesit.KeyFieldName = "IdPerdorues";
                grid_ListPerdoruesit.SettingsBehavior.AllowSelectByRowClick = true;
                grid_ListPerdoruesit.SettingsBehavior.AllowFocusedRow = true;

                grid_ListPerdoruesit.SettingsText.Title = rm.GetString("gridPerdoruesit", ci);
                grid_ListPerdoruesit.Settings.ShowTitlePanel = true;
            }
        }
        /// <summary>
        /// perdoret per te vendosur filtrat tek headeri i grides
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e">argumentat</param>
        protected void grid_ListPerdoruesit_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            if (e.Column.FieldName == "EmriPerdorues" || e.Column.FieldName == "MbiemriPerdorues" || e.Column.FieldName == "PerdoruesUsername")
            {
                e.Values.Clear();
                //e.AddShowAll();
                e.AddValue("  .      ", string.Empty, "true");
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
        /// perdoret per te shfaqur te gjithe grupet e perdoruesve ose qytetet kur perdoruesi filtron me ...
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void grid_ListPerdoruesit_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if (e.Column.FieldName == "IdGrupiPerdorues" || e.Column.FieldName == "IdQyteti")
            {
                if (Converter.ConvertToInt(e.Value) == -1)
                {
                    e.Criteria = null;
                }
            }
        }
        /// <summary>
        /// perdoret per te inicializuar griden me perdoruesit 
        /// </summary>
        /// :  <see cref="DbCore.DbInventari.clsDatabaseInventari.merrGjithePerdoruesit()"/>
        /// <param name="idPerdoruesi"></param>
        private void mbushListePerdoruesish(int idPerdoruesi)
        {
            DbCore.DbAdmin.colPerdoruesit colPerd = new DbCore.DbAdmin.colPerdoruesit();
            colPerd.mbushGjithePerdoruesitSipasAutorizimit(idPerdoruesi, int.Parse(hfId.Value));
            grid_ListPerdoruesit.DataSource = colPerd;
            grid_ListPerdoruesit.DataBind();

        }

        /// <summary>
        /// perdoret per te konfiguruar griden. Per te dhenat mbi kolonat qe do te shfaqen nga databaza si dhe percakton disa karakteristika te grides
        /// </summary>
        /// :  <see cref="GridUtil.percaktoVisibleColumns"/>
        /// :  <see cref="DbCore.clsFunksione.konfiguroGrideListeMadhe(grida,celesigrides)"/>
        private void konfiguroGridePerdoruesish()
        {
            shtoKolonaCombo();
            GridUtil.konfigGrideListeEMadhePaTheme(grid_ListPerdoruesit, "IdPerdorues");
            grid_ListPerdoruesit.Columns["#"].VisibleIndex = 0;
        }


        /// <summary>
        /// perdoret per te shfaqur pershkrimet e grupeve te perdoruesve ne vend te id si dhe filtri i grupit te perdoruesit te shfaqet ne forme komboje
        /// </summary>
        ///  :  <see cref="DbCore.DbAdmin.clsDatabaseAdmin .ktheGjitheGrupetPerdoruesve()"/> 
        private void shtoGrupetPerdoruesve()
        {
            //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            grid_ListPerdoruesit.Columns.Remove(grid_ListPerdoruesit.Columns["IdGrupiPerdorues"]);

            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            DbCore.DbAdmin.colGrupetPerdoruesve colGrupe = new DbCore.DbAdmin.colGrupetPerdoruesve();
            colGrupe.mbushGjitheGrupetPerdoruesve();
            //colGrupe = dbAdmin.merrGjitheGrupetPerdoruesve();
            colnew.PropertiesComboBox.DataSource = colGrupe;
            colnew.PropertiesComboBox.TextField = "GrupiPerdoruesPershkrimi";
            colnew.PropertiesComboBox.ValueField = "IdGrupiPerdorues";
            colnew.FieldName = "IdGrupiPerdorues";
            grid_ListPerdoruesit.Columns.Add(colnew);
        }


        protected void grid_ListPerdoruesit_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            grid_ListPerdoruesit.FilterExpression = "";
            grid_ListPerdoruesit.Selection.UnselectAll();
            if (e.Parameters != "-1")
            {
                int idautorizim = Convert.ToInt32(this.ASPxGridView_Autorizimet.GetRowValues(int.Parse(e.Parameters), "IdAutorizimKoka"));
                DbCore.DbAdmin.colAutorizimetTrupi oColTrupi = new DbCore.DbAdmin.colAutorizimetTrupi(idautorizim);
                foreach (DbCore.DbAdmin.clsAutorizimTrupi rp in oColTrupi)
                {
                    int index = grid_ListPerdoruesit.FindVisibleIndexByKeyValue(rp.IdPerdorues);
                    grid_ListPerdoruesit.Selection.SelectRow(index);
                }
            }
            else
                if(hfShtimModifikim.Value == "shtim")
            {
                grid_ListPerdoruesit.Selection.SelectRowByKey(IdPerdoruesi);
                //grid_ListPerdoruesit.Selection.SelectRow(grid_ListPerdoruesit.FindVisibleIndexByKeyValue((int)hfState.Get("idPerdoruesi")));
            }
            grid_ListPerdoruesit.PageIndex = 0;
        }


        #endregion


        #region  grida Lidhje Autorizime  


        protected void gridLidhjeAutorizime_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            //mbushLidhjeAutorizimet();
        }
        private void mbushLidhjeAutorizimet()
        {
            DbCore.DbAdmin.colLidhjetAutorizim colLidhjeAutorizim = new DbCore.DbAdmin.colLidhjetAutorizim();
            var dt = colLidhjeAutorizim.ktheLidhjetAutorizimSipasKokesAutorizim(int.Parse(hfId.Value), IdGjuha);
            gridLidhjeAutorizime.DataSource = dt;
            gridLidhjeAutorizime.DataBind();
        }

        protected void gridLidhjeAutorizime_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            gridLidhjeAutorizime.FilterExpression = "";
            gridLidhjeAutorizime.Selection.UnselectAll();
            if (e.Parameters != "-1")
            {
                int idautorizim = Convert.ToInt32(this.ASPxGridView_Autorizimet.GetRowValues(int.Parse(e.Parameters), "IdAutorizimKoka"));
                mbushLidhjeAutorizimet();

            }
            // konfiguroGrideLidhjeAutorizim();
            gridLidhjeAutorizime.PageIndex = 0;
        }
        private void konfiguroGrideLidhjeAutorizim()
        {
            GridUtil.percaktoVisibleColumnsMeWidth(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), gridLidhjeAutorizime, "gridLidhjeAutorizime", komponente);

            KonfigurimComboGride.shtoLlojBuxhet(gridLidhjeAutorizime, Session, komponente, guidString);
            GridUtil.konfigGrideListeEMadhePaTheme(gridLidhjeAutorizime, "IdLidhjeAutorizim");
            gridLidhjeAutorizime.Columns["#"].VisibleIndex = 0;
            //  gridLidhjeAutorizime.DataBind();
        }

        protected void gridLidhjeAutorizime_DataBound(object sender, EventArgs e)
        {
            if (gridLidhjeAutorizime.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per 
                //perzgjidh
                DevExpress.Web.GridViewCommandColumn check = new DevExpress.Web.GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true;
                check.Width = Unit.Percentage(3);
                // check.SetColVisibleIndex(0);
                //behet per te afishuar rreshtin qe do sherbej per filtrim
                gridLidhjeAutorizime.Settings.ShowFilterRow = true;
                gridLidhjeAutorizime.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                gridLidhjeAutorizime.Settings.ShowFilterRowMenu = true;
                gridLidhjeAutorizime.Columns.Add(check);
                //grid_ListPerdoruesit.Columns["IdPerdorues"].Visible = false;
                gridLidhjeAutorizime.KeyFieldName = "IdLidhjeAutorizim";
                gridLidhjeAutorizime.SettingsBehavior.AllowSelectByRowClick = true;
                gridLidhjeAutorizime.SettingsBehavior.AllowFocusedRow = true;
            }
        }
        protected void gridLidhjeAutorizime_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            if (e.Column.FieldName == "Kodi" || e.Column.FieldName == "Pershkrimi")
            {
                e.Values.Clear();
                //e.AddShowAll();
                e.AddValue("  .      ", string.Empty, "true");
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

        #endregion
    }
}