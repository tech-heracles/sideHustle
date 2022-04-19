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
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.DbAdmin;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;

namespace PlatinumWeb
{
    public partial class Shto_KPF : MyPageBase
    {
        public static string grida = "";
        public static string page = "";
        private int idgjuha, idviti, idPerdoruesi, idNdermarrje;

        /// <summary>
        /// perdoret per te vendosur kodin sipas numrit automatik kur e kemi lidhur me nje nr automatik
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void Page_Init(object sender, EventArgs e)
        {//perdoret per te vene nr automatik te dokumentit
            int idregj = DbCore.DbAdmin.clsListeAmbjenteCeljeRegjistrim.ktheIdCR("CKPF");
            int idlloji = DbCore.DbAdmin.clsLlojKodi.ktheIDLlojKodi("Kod");
          
        }
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
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (!Page.IsPostBack)
            {
                EmrateTabeve(rm, ci);
                vendosHfMePerkthime(rm, ci);
                hfState.Set("idGjuha", idgjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("idPerdoruesi", IdPerdoruesi);
                ASPxPageControl1.ActiveTabIndex = 0; HiddenField4.Value = "0";
                ASPxPageControl2.ActiveTabIndex = 0;
                konfiguroVleraFillestare(idPerdoruesi, idNdermarrje, rm, ci, idgjuha);
                konfiguroListe();
                hfId.Value = "0";
                konfiguroListeLlogarish();
                mbushGridKPFshNgaDB();
                hfState.Set("colAutorizime", Newtonsoft.Json.JsonConvert.SerializeObject(new colAutorizimetKoka(IdPerdoruesi)));
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, IdNdermarrjeVit, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, IdNdermarrjeVit, "Shto_KPF.aspx");
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                konfiguroGride(cmbKonfigurimi.Text.Split(';')[0], 110);
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "grid_ListKPFsh1", grid_ListKPFsh1, cmbKonfigurimi.Text.Split(';')[0], 110.ToString(), idgjuha);
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "grid_ListKPFsh1", grid_ListKPFsh2, cmbKonfigurimi.Text.Split(';')[0], 110.ToString(), idgjuha);

                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "grid_ListKPFsh1", grid_ListKPFsh3, cmbKonfigurimi.Text.Split(';')[0], 110.ToString(), idgjuha);

            }
            else
            {
                mbushGridKPFshNgaSession();
                konfiguroGride(cmbKonfigurimi.Text.Split(';')[0], 110);
            }
            konfigurimi_Label.Text = rm.GetString("lblModeli", ci);
            popupUniversal.HeaderText = rm.GetString("popupAdministrimiUniversal", ci);
            AspxWebControlUtils.perkthePopUp(popFshi, rm.GetString("labelKujdes", ci), lblMsgbox, rm.GetString("labelAdministrimiMsgJeniSigurt", ci), ButtonCancel, rm.GetString("labelAnullo", ci));
            GridUtil.konfigGrideListeEMadhePaTheme(grid_ListKPFsh1, "IdKPF");
            GridUtil.konfigGrideListeEMadhePaTheme(grid_ListKPFsh2, "IdKPF");
            GridUtil.konfigGrideListeEMadhePaTheme(grid_ListKPFsh3, "IdKPF");
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "grid_ListKPFsh", int.Parse(cmbKonfigurimi.Value.ToString()), "Shto_KPF.aspx");
            GridUtil.EmrateButonaveMbiGride(grid_ListKPFsh1);
        }

        private void vendosHfMePerkthime(ResourceManager rm, CultureInfo ci)
        {
            hfState.Set("headerPopUpZgjidhLlogarineStandarte", rm.GetString("headerPopUpZgjidhLlogarineStandarte", ci));
            hfState.Set("headerPopUpZgjidhAutorizimet", rm.GetString("headerPopUpZgjidhAutorizimet", ci));
            hfState.Set("msgDuhetTeZgjidhni1StruktureLlogarie", rm.GetString("msgDuhetTeZgjidhni1StruktureLlogarie", ci));
        }

        /// <summary>
        /// Vendos emrat e tabeve ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateTabeve(ResourceManager rm, CultureInfo cultinf)
        {

            ASPxPageControl1.TabPages[0].Text = rm.GetString("labelAdministrimiTePergjithshme", cultinf);
            ASPxPageControl2.TabPages[0].Text = rm.GetString("struktura1Tab", cultinf);
            ASPxPageControl2.TabPages[1].Text = rm.GetString("struktura2Tab", cultinf);
            ASPxPageControl2.TabPages[2].Text = rm.GetString("struktura3Tab", cultinf);
            ASPxPageControl1.TabPages[1].Text = rm.GetString("llogariStandarteTab", cultinf);
            ASPxPageControl1.TabPages[2].Text = rm.GetString("labelRaportiShenime", cultinf);
            ASPxPageControl1.TabPages[3].Text = rm.GetString("MenuItemLlogarite", cultinf);
            ((ASPxLabel)ASPxPageControl1.TabPages[3].FindControl("lblKodiLlog")).Text = rm.GetString("labelBlerjeShitjeKodi", cultinf);
            ((ASPxLabel)ASPxPageControl1.TabPages[3].FindControl("lblEmertimiLlog")).Text = rm.GetString("labelBlerjeShitjeEmertimi", cultinf);
            ((ASPxLabel)ASPxPageControl1.TabPages[3].FindControl("ASPxLabel13")).Text = rm.GetString("lblLlogarite", cultinf);
        }

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="idNderVit"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        private void percaktoTemplateMenu(int idGjuha, int idViti, int idPerdorues, int idNdermarrje, ASPxMenu aSPxMenu1)
        {
            clsToolbarConfig.percaktoTemplateMenu(idGjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1, "Shto_KPF.aspx", this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, hfShtimModifikim.Value == "modifikim" ? false : true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
        }

        protected void RuajKolona_Click(object sender, EventArgs e)
        {
            int idfiltri = 0;
            int idkonf = DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(cmbKonfigurimi.Text.Split(';')[0], idNdermarrje);
            DbCore.clsMesazh mesazh = GridUtil.ruajFiltra(idNdermarrje, idPerdoruesi, idgjuha, grid_ListKPFsh1.ID, "Shto_KPF.aspx", "FilterDefault", grid_ListKPFsh1.FilterExpression, grid_ListKPFsh1, "KodiKPF", idkonf, out idfiltri);//ruan filtrin e zgjedhur tek filtrat
            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                return;

            }
            mesazh = GridUtil.ruajkonfigurimgride(grid_ListKPFsh1, cmbKonfigurimi.Text, idNdermarrje, idPerdoruesi, 110, idfiltri, idviti, DbCore.mySessionObjects.ktheCultureInfo(Session), DbCore.mySessionObjects.ktheGjuhe(Session));/// ruan konfigurimin e grides dhe filtrin

            clsToolbarConfig.mbushComboBoxFiltra(idgjuha, idNdermarrje, "grid_ListKPFsh ", int.Parse(cmbKonfigurimi.Value.ToString()), "Shto_KPF.aspx");

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
        /// <summary>
        /// perdoret per te percaktuar templatet e komboboxeve dhe te mbushe vlerat fillestare te gridave
        /// </summary>
        /// <param name="idPerdoruesi"></param>
        /// <param name="idNdermarrje"></param>
        private void konfiguroVleraFillestare(int idPerdoruesi, int idNdermarrje, ResourceManager rm, CultureInfo ci, int idGjuha)
        { //mbush komboboxet dhe gridat e faqes
            inicializoObjekte();
            ConfigureAspxComboBox.mbushComboNivelesh(cmbNiveleKPF);
            ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 16, rm, ci, idGjuha);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmbPrindi);
          // DbCore.clsFunksione.shtoKPFPrind(cmbPrindi);
            ConfigureAspxComboBox.mbushComboKPF(idPerdoruesi, idNdermarrje, cmbPrindi, 1);
            cmbKonfigurimi.SelectedIndex = 0;
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()), idGjuha);
            hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;
        }
        /// <summary>
        /// inicializon dbKontabilitetin
        /// </summary>
        private void inicializoObjekte()
        {
        }
      

        //sherben per te ruajtur nje KPF
        /// <summary>
        /// sherben per te ruajtur nje kpf
        /// </summary>
        private void ruajKPF()
        {

            DbCore.DbKontabiliteti.clsKPF KPF;

            if (Page.IsValid == false)
                return;
            else
            {
                int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
                ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                if (isValidKPF(rm, ci))
                {
                    bool eshteShtim;
                    KPF = krijoKPF(idPerdoruesi);
                    DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                    DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

                    tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "Shto_KPF.aspx");

                    if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim")
                    {
                        if (!tedrejtaInfo.DShtim)
                        {
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNukKeniTeDrejta", ci), pnlMesazhi);
                            hfStatusi.Value = "false";
                            return;
                        }
                        mesazh = KPF.ruaj();
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
                        konf.mbushKonfigAmbjSipasId(KPF.IdKonfig);
                        //konf.IdKonfigAmbjente = KPF.IdKonfig;
                        //konf = new DbCore.DbShare.clsDatabaseShare().merrKonfigAmbjSipasId(konf)[0];

                        KPF.IdKPF = int.Parse(hfId.Value.ToString());
                        bool lidhur = dbRegjistrim.eshteDokumentiILidhurCelje(KPF.IdKPF.ToString(), konf.IdNivel.ToString());
                        if (lidhur.ToString() != hfLidhur.Value)
                        {
                            mesazh.Status = false;
                            mesazh.PershkrimMesazhi = rm.GetString("msgShtoKPFLlogariaStandarteEshteELidhur", ci);

                        }
                        else
                            mesazh = KPF.modifiko();
                        eshteShtim = false;
                        dbRegjistrim.Dispose();
                    }
                    if (mesazh.Status == true)
                    {
                        clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("msgRuajtjeMeSukses", ci), pnlMesazhi);
                        if (eshteShtim)
                            shtoKPFNeGrid(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), KPF.IdKPF, rm, ci);
                        else //modifikim
                            modifikoKPFNeGrid(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), KPF.IdKPF, rm, ci);

                        pastroFusha();
                        hfStatusi.Value = "true";
                    }
                    else
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgDegetAdministrativeRuajtjeMeGabime", ci), pnlMesazhi);
                        percaktoTamplate1();
                        percaktoTamplate2();
                        percaktoTamplate3(); hfStatusi.Value = "false";
                    }
                    ASPxPageControl1.ActiveTabIndex = 0;
                    if (KPF.GrupiKPF == 1)
                    {
                        // konfiguroVleraFillestare1();

                        ASPxPageControl2.ActiveTabIndex = 0;

                    }
                    else if (KPF.GrupiKPF == 2)
                    {
                        // konfiguroVleraFillestare2();
                        ASPxPageControl2.ActiveTabIndex = 1;

                    }
                    else if (KPF.GrupiKPF == 3)
                    {
                        // konfiguroVleraFillestare3();
                        ASPxPageControl2.ActiveTabIndex = 2;

                    }
                }
                else
                {
                    if (ASPxPageControl2.ActiveTabIndex == 0)
                    {
                        //  konfiguroVleraFillestare1();
                        percaktoTamplate1();
                        percaktoTamplate2();
                        percaktoTamplate3();
                    }
                    else if (ASPxPageControl2.ActiveTabIndex == 1)
                    {
                        //  konfiguroVleraFillestare2();
                        percaktoTamplate1();
                        percaktoTamplate2();
                        percaktoTamplate3();
                    }
                    else if (ASPxPageControl2.ActiveTabIndex == 2)
                    {
                        //  konfiguroVleraFillestare3();
                        percaktoTamplate1();
                        percaktoTamplate2();
                        percaktoTamplate3();
                    }
                    hfStatusi.Value = "false";
                }
            }
            pnlMesazhi.Update();
        }
        /// <summary>
        /// pastron fushat e faqes
        /// </summary>
        private void pastroFusha()
        {//pastron fushat
            this.kodi_TextBox.Text = "";
            this.txtKodi.Text = "";
            this.txtEmertimi.Text = "";
            this.emertimi_TextBox.Text = "";
            this.txtShenime.Text = "";
            this.txtEmerLlogarieFr.Text = "";
            this.cmbNiveleKPF.SelectedIndex = -1;
            this.cmbAutorizimiHf.Value = "";
            this.cmbPrindi.SelectedIndex = -1;
            this.cbInaktiv.Checked = false;


            HiddenField1.Value = "";
            HiddenField2.Value = "";
            HiddenField3.Value = "";
            hfBuxheti1.Value = "";
            hfBuxheti2.Value = "";
            int idregj = DbCore.DbAdmin.clsListeAmbjenteCeljeRegjistrim.ktheIdCR("CKPF");
            int idlloji = DbCore.DbAdmin.clsLlojKodi.ktheIDLlojKodi("Kod");
            
            hfAutorizime1.Value = "";
            hfAutorizime2.Value = "";
            hfAutorizime3.Value = "";
        }
        /// <summary>
        /// krijon llogarine standarte qe do te ruhet
        /// </summary>
        /// <returns>kthen nje clsKPF qe permban llogarine standarte</returns>
        /// <param name="idPerdoruesi"></param>
        private DbCore.DbKontabiliteti.clsKPF krijoKPF(int idPerdoruesi)
        {//krijon nje KPF sipas te dhenave te futura nga perdoruesi
            DbCore.DbKontabiliteti.clsKPF KPF = new DbCore.DbKontabiliteti.clsKPF();
            KPF.KodiKPF = txtKodi.Text;
            KPF.EmertimiKPF = txtEmertimi.Text;
            KPF.NiveliKPF = int.Parse(this.cmbNiveleKPF.SelectedItem.Value.ToString());
            KPF.IdAutorizimi = cmbAutorizimiHf.Value;
            KPF.Inaktiv = !cbInaktiv.Checked;
            KPF.ShenimeKPF = txtShenime.Text;
            KPF.EmertimiKPF_fr = txtEmerLlogarieFr.Text;
            DbCore.DbShare.clsKonfigurimAmbjenti konfig = new DbCore.DbShare.clsKonfigurimAmbjenti();
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            konfig.mbushKonfigAmbjSipasKod(cmbKonfigurimi.Text, idNdermarrje);
            KPF.IdKonfig = konfig.IdKonfigAmbjente;
            KPF.IdNdermarje = idNdermarrje;
            KPF.IdStatusDok = 1;
            if (ASPxPageControl2.ActiveTabIndex == 0)
                KPF.GrupiKPF = 1;
            else if (ASPxPageControl2.ActiveTabIndex == 1)
                KPF.GrupiKPF = 2;
            else if (ASPxPageControl2.ActiveTabIndex == 2)
                KPF.GrupiKPF = 3;
            KPF.IdPerdoruesi = idPerdoruesi;
            //KPF.OColBuxhetet = ruajBuxhet();
            return KPF;
        }
        /// <summary>
        /// kontrollon nese te dhenat qe jane plotesuara jane 
        ///te lejueshme apo jo
        /// </summary>
        /// <returns> true  ose false</returns>
        private bool isValidKPF(ResourceManager rm, CultureInfo ci)
        {
            bool isValid;
            isValid = true;

            if (cmbNiveleKPF.SelectedIndex == -1)
            {
                isValid = false;
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgShtoKPFZgjidhniNivelin", ci), pnlMesazhi);
                percaktoTamplate1();
                percaktoTamplate2();
                percaktoTamplate3(); hfStatusi.Value = "false";
            }
            else
            {
                DbCore.DbKontabiliteti.clsDatabaseKontabilitet dbKontabiliteti = new DbCore.DbKontabiliteti.clsDatabaseKontabilitet();
                if (cmbPrindi.Text != "" && !dbKontabiliteti.ekzistonKPF(this.cmbPrindi.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), int.Parse(HiddenField4.Value.ToString()) + 1) && hfShtimModifikim.Value == "shtim")
                {
                    isValid = false;
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgShtoKPFLlogariaPrindNukEkziston", ci), pnlMesazhi);
                    percaktoTamplate1();
                    percaktoTamplate2();
                    percaktoTamplate3(); hfStatusi.Value = "false";
                    dbKontabiliteti.Dispose();
                    return isValid;
                }

                if (dbKontabiliteti.ekzistonKPF(txtKodi.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), int.Parse(HiddenField4.Value.ToString()) + 1) && (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim"))
                {
                    isValid = false;
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgShtoKPFEkziston1LlogariStandarteMeKeteKod", ci), pnlMesazhi);
                    percaktoTamplate1();
                    percaktoTamplate2();
                    percaktoTamplate3(); hfStatusi.Value = "false";
                    dbKontabiliteti.Dispose();
                    return isValid;
                }
                dbKontabiliteti.Dispose();
            }
            return isValid;
        }
        /// <summary>
        /// perdoret per te rikonfiguruar griden kur ndryshohet konfigurimi
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void grid_ListKPFsh1_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {

            string idkomponente = "";
            string kodkonfigurimi = "";

            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                kodkonfigurimi = arr[1];
                idkomponente = arr[0];
                if (arr[2] == "")
                    grid_ListKPFsh1.FilterExpression = "";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "grid_ListKPFsh", "Shto_KPF.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), int.Parse(cmbKonfigurimi.Value.ToString()));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        if (ASPxPageControl2.ActiveTabIndex == 0)
                        {
                            grid_ListKPFsh1.FilterExpression = filtra.FiltraVlera;
                            GridUtil.renditGriden(filtra.KoloneRenditje, grid_ListKPFsh1);
                            konfiguroVleraFillestare1();
                            percaktoTamplate1();
                        }
                        hfStatusi.Value = "true";

                    }
                    else hfStatusi.Value = "false";
                }
            }
            else
                if (arr.Length == 2)//nese eshte zgjedhur nje konfigurim tek combo e konfigurimeve
                {
                    kodkonfigurimi = arr[1];
                    idkomponente = arr[0];
                }
                else
                {
                    idkomponente = e.Parameters;
                }
            grid_ListKPFsh1.Selection.UnselectAll();
        }
        /// <summary>
        /// perdoret per te rikonfiguruar griden kur ndryshohet konfigurimi
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void grid_ListKPFsh2_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string idkomponente = "";
            string kodkonfigurimi = "";

            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                kodkonfigurimi = arr[1];
                idkomponente = arr[0];
                if (arr[2] == "")
                    grid_ListKPFsh2.FilterExpression = "";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "grid_ListKPFsh", "Shto_KPF.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), int.Parse(cmbKonfigurimi.Value.ToString()));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        if (ASPxPageControl2.ActiveTabIndex == 1)
                        {
                            grid_ListKPFsh2.FilterExpression = filtra.FiltraVlera;
                            GridUtil.renditGriden(filtra.KoloneRenditje, grid_ListKPFsh2);
                            konfiguroVleraFillestare2(DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
                            percaktoTamplate2();
                        }
                        hfStatusi.Value = "true";
                    }
                    else hfStatusi.Value = "false";
                }
            }
            else
                if (arr.Length == 2)//nese eshte zgjedhur nje konfigurim tek combo e konfigurimeve
                {
                    kodkonfigurimi = arr[1];
                    idkomponente = arr[0];
                }
                else
                {
                    idkomponente = e.Parameters;
                }
            GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), "grid_ListKPFsh1", grid_ListKPFsh2, kodkonfigurimi, idkomponente, DbCore.mySessionObjects.ktheGjuhe(Session));
            grid_ListKPFsh1.Selection.UnselectAll();

        }
        /// <summary>
        /// perdoret per te rikonfiguruar griden kur ndryshohet konfigurimi
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void grid_ListKPFsh3_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string idkomponente = "";
            string kodkonfigurimi = "";

            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                kodkonfigurimi = arr[1];
                idkomponente = arr[0];
                if (arr[2] == "")
                    grid_ListKPFsh3.FilterExpression = "";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "grid_ListKPFsh", "Shto_KPF.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), int.Parse(cmbKonfigurimi.Value.ToString()));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        if (ASPxPageControl2.ActiveTabIndex == 2)
                        {
                            grid_ListKPFsh3.FilterExpression = filtra.FiltraVlera;
                            GridUtil.renditGriden(filtra.KoloneRenditje, grid_ListKPFsh3);
                            int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                            konfiguroVleraFillestare3(idPerdoruesi);
                            percaktoTamplate3();
                        }

                        hfStatusi.Value = "true";

                    }
                    else hfStatusi.Value = "false";
                }
            }
            else
                if (arr.Length == 2)//nese eshte zgjedhur nje konfigurim tek combo e konfigurimeve
                {
                    kodkonfigurimi = arr[1];
                    idkomponente = arr[0];
                }
                else
                {
                    idkomponente = e.Parameters;
                }
            GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), "grid_ListKPFsh1", grid_ListKPFsh3, kodkonfigurimi, idkomponente, DbCore.mySessionObjects.ktheGjuhe(Session));

            grid_ListKPFsh1.Selection.UnselectAll();
        }
        private void mbushGridKPFshNgaSession()
        {
            DataTable tmpObject;
            DataTable tmpObject2;
            DataTable tmpObject3;
            bool sukses = DbCore.mySessionObjects.merrGrideNgaSessioni("grid_ListKPFsh1",Session, out tmpObject);
            if (!sukses)
                mbushGridKPFNgaDbPerGride(grid_ListKPFsh1);
            else
            {
                grid_ListKPFsh1.DataSource = tmpObject;
                grid_ListKPFsh1.DataBind();
                tmpObject.Dispose();
            }

            sukses = DbCore.mySessionObjects.merrGrideNgaSessioni("grid_ListKPFsh2", Session, out tmpObject2);
            if (!sukses)
                mbushGridKPFNgaDbPerGride(grid_ListKPFsh2);
            else
            {
                grid_ListKPFsh2.DataSource = tmpObject2;
                grid_ListKPFsh2.DataBind();
                tmpObject2.Dispose();
            }

            sukses = DbCore.mySessionObjects.merrGrideNgaSessioni("grid_ListKPFsh3", Session, out tmpObject3);
            if (!sukses)
                mbushGridKPFNgaDbPerGride(grid_ListKPFsh3);
            else
            {
                grid_ListKPFsh3.DataSource = tmpObject3;
                grid_ListKPFsh3.DataBind();
                tmpObject3.Dispose();
            }
        }

        private void mbushGridKPFNgaDbPerGride(ASPxGridView grida)
        {
            int lloji = 0;
            switch (grida.ClientInstanceName)
            {
                case "grid_ListKPFsh1":
                    lloji = 1;
                    break;
                case "grid_ListKPFsh2":
                    lloji = 2;
                    break;
                case "grid_ListKPFsh3":
                    lloji = 3;
                    break;
            }
            DataTable dt = DbCore.DbKontabiliteti.colKPFte.merrSipasKPFNdermarrjesAndAutorizimeDTGrupit(lloji, IdNdermarrja, IdPerdoruesi);
            DbCore.mySessionObjects.ruajGrideNeSession(grida.ClientInstanceName, Session, dt);
            grid_ListKPFsh1.DataSource = dt;
            grid_ListKPFsh1.DataBind();

        
            dt.Dispose();
        }

        private void mbushGridKPFshNgaDB()
        {//mbush griden e popupit me te dhena            
            DataTable dt = DbCore.DbKontabiliteti.colKPFte.merrSipasKPFNdermarrjesAndAutorizimeDTGrupit(1, IdNdermarrja, IdPerdoruesi);
            DataTable dt2 = DbCore.DbKontabiliteti.colKPFte.merrSipasKPFNdermarrjesAndAutorizimeDTGrupit(2, IdNdermarrja, IdPerdoruesi);
            DataTable dt3 = DbCore.DbKontabiliteti.colKPFte.merrSipasKPFNdermarrjesAndAutorizimeDTGrupit(3, IdNdermarrja, IdPerdoruesi);
            DbCore.mySessionObjects.ruajGrideNeSession("grid_ListKPFsh1", Session, dt);
            DbCore.mySessionObjects.ruajGrideNeSession("grid_ListKPFsh2", Session, dt2);
            DbCore.mySessionObjects.ruajGrideNeSession("grid_ListKPFsh3", Session, dt3);
            grid_ListKPFsh1.DataSource = dt;
            grid_ListKPFsh1.DataBind();
            grid_ListKPFsh2.DataSource = dt2;
            grid_ListKPFsh2.DataBind();
            grid_ListKPFsh3.DataSource = dt3;
            grid_ListKPFsh3.DataBind();
            dt.Dispose();
            dt2.Dispose();
            dt3.Dispose();
        }
        /// <summary>
        /// mbush griden me te dhenat
        /// </summary>
        private void konfiguroVleraFillestare1()
        {//mbush griden me te dhena
            DbCore.DbKontabiliteti.colKPFte colKPFte = new DbCore.DbKontabiliteti.colKPFte();
            colKPFte.mbushGjitheKPFteSipasGrupitPozitiveAndAutorizime(1, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            this.grid_ListKPFsh1.DataSource = colKPFte;
            this.grid_ListKPFsh1.DataBind();
        }
        /// <summary>
        /// mbush griden me te dhenat
        /// </summary>
        /// <param name="idPerdoruesi"></param>
        private void konfiguroVleraFillestare2(int idPerdoruesi)
        {//mbush griden me te dhena
            DbCore.DbKontabiliteti.colKPFte colKPFte = new DbCore.DbKontabiliteti.colKPFte();
            colKPFte.mbushGjitheKPFteSipasGrupitPozitiveAndAutorizime(2, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), idPerdoruesi);
            this.grid_ListKPFsh2.DataSource = colKPFte;
            this.grid_ListKPFsh2.DataBind();
        }
        /// <summary>
        /// mbush griden me te dhenat
        /// </summary>
        /// <param name="idPerdoruesi"></param>
        private void konfiguroVleraFillestare3(int idPerdoruesi)
        {//mbush griden me te dhena
            DbCore.DbKontabiliteti.colKPFte colKPFte = new DbCore.DbKontabiliteti.colKPFte();
            colKPFte.mbushGjitheKPFteSipasGrupitPozitiveAndAutorizime(3, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), idPerdoruesi);
            this.grid_ListKPFsh3.DataSource = colKPFte;
            this.grid_ListKPFsh3.DataBind();
        }
        /// <summary>
        /// perdoret per te shtuar kolonen e selektimit tek grida dhe per te vendosur disa karakteristika te grides
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void grid_ListKPFsh1_DataBound(object sender, EventArgs e)
        {// shton colonen # per selektim dhe disa karakteristika te grides
            if (this.grid_ListKPFsh1.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per 
                //perzgjidh
                GridViewCommandColumn check = new GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true;
                check.Width = Unit.Percentage(2);
                grid_ListKPFsh1.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                grid_ListKPFsh1.Settings.ShowFilterRowMenu = true;
                grid_ListKPFsh1.Settings.ShowFilterRow = true;
                grid_ListKPFsh1.Columns.Add(check);

                grid_ListKPFsh1.KeyFieldName = "IdKPF";
                grid_ListKPFsh1.SettingsBehavior.AllowSelectByRowClick = true;
                grid_ListKPFsh1.SettingsBehavior.AllowFocusedRow = true;
            }
        }
        /// <summary>
        /// perdoret per te shtuar kolonen e selektimit tek grida dhe per te vendosur disa karakteristika te grides
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void grid_ListKPFsh2_DataBound(object sender, EventArgs e)
        {// shton colonen # per selektim dhe disa karakteristika te grides
            if (this.grid_ListKPFsh2.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per 
                //perzgjidh
                DevExpress.Web.GridViewCommandColumn check = new DevExpress.Web.GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true;
                check.Width = Unit.Percentage(2);
                //behet per te afishuar rreshtin qe do sherbej per filtrim
                grid_ListKPFsh2.Settings.ShowFilterRow = true;
                grid_ListKPFsh2.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                grid_ListKPFsh2.Settings.ShowFilterRowMenu = true;
                grid_ListKPFsh2.Columns.Add(check);

                grid_ListKPFsh2.KeyFieldName = "IdKPF";
                grid_ListKPFsh2.SettingsBehavior.AllowSelectByRowClick = true;
                grid_ListKPFsh2.SettingsBehavior.AllowFocusedRow = true;
            }
        }
        /// <summary>
        /// perdoret per te shtuar kolonen e selektimit tek grida dhe per te vendosur disa karakteristika te grides
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void grid_ListKPFsh3_DataBound(object sender, EventArgs e)
        {// shton colonen # per selektim dhe disa karakteristika te grides
            if (this.grid_ListKPFsh3.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per 
                //perzgjidh
                DevExpress.Web.GridViewCommandColumn check = new DevExpress.Web.GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true;
                check.Width = Unit.Percentage(2);
                //behet per te afishuar rreshtin qe do sherbej per filtrim
                grid_ListKPFsh3.Settings.ShowFilterRow = true;
                grid_ListKPFsh3.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                grid_ListKPFsh3.Settings.ShowFilterRowMenu = true;
                grid_ListKPFsh3.Columns.Add(check);

                grid_ListKPFsh3.KeyFieldName = "IdKPF";
                grid_ListKPFsh3.SettingsBehavior.AllowSelectByRowClick = true;
                grid_ListKPFsh3.SettingsBehavior.AllowFocusedRow = true;
            }
        }
        /// <summary>
        /// ben konfigurimin e grides sipas konfigurimit te zgjedhur
        /// </summary>
        private void konfiguroGride(string kodKonfigurimi, int idKomponente)
        {
            shtoNivelKPF(grid_ListKPFsh1);
            shtoKolonaFolder(grid_ListKPFsh1);
            percaktoTamplate1(); shtoNivelKPF(grid_ListKPFsh2);
            shtoKolonaFolder(grid_ListKPFsh2);
            percaktoTamplate2(); shtoNivelKPF(grid_ListKPFsh3);
            shtoKolonaFolder(grid_ListKPFsh3);
            percaktoTamplate3();

                        this.grid_ListKPFsh1.Columns["#"].VisibleIndex = 0;
            this.grid_ListKPFsh2.Columns["#"].VisibleIndex = 0;
            this.grid_ListKPFsh3.Columns["#"].VisibleIndex = 0;
        }
        /// <summary>
        /// ben konfigurimin e grides sipas konfigurimit te zgjedhur
        /// </summary>
        private void konfiguroGride2()
        {
            shtoNivelKPF(grid_ListKPFsh2);
            shtoKolonaFolder(grid_ListKPFsh2);
            percaktoTamplate2();
            GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), "grid_ListKPFsh1", grid_ListKPFsh2, cmbKonfigurimi.Text.Split(';')[0], "110", DbCore.mySessionObjects.ktheGjuhe(Session));
            GridUtil.konfigGrideListeEMadhePaTheme(grid_ListKPFsh2, "IdKPF");
        }
        /// <summary>
        /// ben konfigurimin e grides sipas konfigurimit te zgjedhur
        /// </summary>
        private void konfiguroGride3()
        {
            shtoNivelKPF(grid_ListKPFsh3);
            shtoKolonaFolder(grid_ListKPFsh3);
            percaktoTamplate3();
            GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), "grid_ListKPFsh1", grid_ListKPFsh3, cmbKonfigurimi.Text.Split(';')[0], "110", DbCore.mySessionObjects.ktheGjuhe(Session));
            GridUtil.konfigGrideListeEMadhePaTheme(grid_ListKPFsh3, "IdKPF");
        }
        /// <summary>
        ///  sherben per ta bere ne forme combo-je shtyllen e niveleve 
        /// </summary>
        private void shtoNivelKPF(ASPxGridView grid)
        {

            var oldColumn = grid.Columns["NiveliKPF"];
            var newComboColumn = oldColumn as GridViewDataComboBoxColumn;
            if (newComboColumn == null)
            {
                newComboColumn = new GridViewDataComboBoxColumn() { FieldName= "NiveliKPF"};
                grid.Columns.Remove(oldColumn);
                grid.Columns.Add(newComboColumn);
            }
            List<string> colNivelKPF = new List<string>();
            colNivelKPF.Add("");
            colNivelKPF.Add("1");
            colNivelKPF.Add("2");
            colNivelKPF.Add("3");
            colNivelKPF.Add("4");
            colNivelKPF.Add("5");
            colNivelKPF.Add("6");
            colNivelKPF.Add("7");
            newComboColumn.PropertiesComboBox.DataSource = colNivelKPF;
        }
        /// <summary>
        ///  sherben per te shtuar kolona folder
        /// </summary>
        private void shtoKoloneFolderNiveli(ASPxGridView grid, string emerkolone)
        {
            var oldColumn = grid.Columns[emerkolone];
            var newColumn = oldColumn as GridViewDataImageColumn;
            if (newColumn == null)
            {
                newColumn = new GridViewDataImageColumn { FieldName = emerkolone };
                grid.Columns.Add(newColumn);
                grid.Columns.Remove(oldColumn);

            }
          
            
        }
        /// <summary>
        ///  sherben per te shtuar te gjitha kolonat me foldera
        /// </summary>
        private void shtoKolonaFolder(ASPxGridView grid)
        {
            shtoKoloneFolderNiveli(grid, "UrlImage1");
            shtoKoloneFolderNiveli(grid, "UrlImage2");
            shtoKoloneFolderNiveli(grid, "UrlImage3");
            shtoKoloneFolderNiveli(grid, "UrlImage4");
            shtoKoloneFolderNiveli(grid, "UrlImage5");
            shtoKoloneFolderNiveli(grid, "UrlImage6");
            shtoKoloneFolderNiveli(grid, "UrlImage7");
        }
        /// <summary>
        /// percakton templatin per kolonen aktiv qe te dale si checkbox
        /// </summary>
        private void percaktoTamplate1()
        {
            GridViewDataColumn col = grid_ListKPFsh1.Columns["Inaktiv"] as GridViewDataColumn;
            col.DataItemTemplate = new MyCheckTemplate(true, false);
        }
        /// <summary>
        /// percakton templatin per kolonen aktiv qe te dale si checkbox
        /// </summary>
        private void percaktoTamplate2()
        {
            GridViewDataColumn col = grid_ListKPFsh2.Columns["Inaktiv"] as GridViewDataColumn;
            col.DataItemTemplate = new MyCheckTemplate(true, false);
        }
        /// <summary>
        /// percakton templatin per kolonen aktiv qe te dale si checkbox
        /// </summary>
        private void percaktoTamplate3()
        {
            GridViewDataColumn col = grid_ListKPFsh3.Columns["Inaktiv"] as GridViewDataColumn;
            col.DataItemTemplate = new MyCheckTemplate(true, false);
        }
        /// <summary>
        ///  thirret sa here qe behet Callback, dhe ben edhe njeher lidhjen me datasource-in
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void grid_ListKPFsh1_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            if (e.CallbackName == "COLUMNMOVE" && grid_ListKPFsh1.AllColumns[int.Parse(e.Args[0])].Width.Value == 0)
                grid_ListKPFsh1.AllColumns[int.Parse(e.Args[0])].Width = Unit.Percentage(3);
            if (e.CallbackName == "APPLYFILTER" && e.Args[0] == "")
            {
                DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
                ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
                cmbFiltra.Text = "";
            }
            percaktoTamplate1();
            percaktoTamplate2();
            percaktoTamplate3();
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            GridUtil.EmrateButonaveMbiGride(grid_ListKPFsh1);

        }
        /// <summary>
        ///   sherben per te vendosur filtra tek header-i i grides (mund te ndryshohet me vone per tu
        ///bere  me e konfigurueshme
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void grid_ListKPFsh1_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            string TeGjithe = rm.GetString("GridHeaderFilterFillItemTeGjithe", ci);
            string nga = rm.GetString("GridHeaderFilterFillItemNga", ci);
            if (e.Column.FieldName == "EmertimiKPF")
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
        /// perdoret per te shfaqur kolonen aktive me kombo aktiv dhe inaktiv
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void grid_ListKPFsh1_AutoFilterCellEditorInitialize1(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName == "Inaktiv")
            {
                CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
                ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                (e.Editor as ASPxComboBox).Items.Clear();
                (e.Editor as ASPxComboBox).Items.Add("");
                (e.Editor as ASPxComboBox).Items.Add(rm.GetString("msgShtoKPFInaktiv", ci), false);
                (e.Editor as ASPxComboBox).Items.Add(rm.GetString("labelRaportAktiv", ci), true);
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
            int idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            //filtra.mbushFiltraGridaSipasFiltraEmri(cmbFiltra.Text, idNdermarrje);
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuha, "grid_ListKPFsh", "Shto_KPF.aspx", idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            //DbCore.DbAdmin.clsFiltraGrida filtra = dbAdmin.merrFiltraGridaSipasFiltraEmri(cmbFiltra.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = idPerdoruesi;
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();
                //mbushComboBoxFiltra();
                clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idNdermarrje, "grid_ListKPFsh", int.Parse(cmbKonfigurimi.Value.ToString()), "Shto_KPF.aspx");
                percaktoTemplateMenu(idgjuha, idviti, idPerdoruesi, idNdermarrje, ASPxMenu1);
                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                //ASPxButton btnRuaj = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("Button1") as ASPxButton;
                //ASPxButton btnFshiFilter = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFshi") as ASPxButton;

                //btnRuaj.ClientEnabled = false;
                //btnFshiFilter.ClientEnabled = false;
                CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
                ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                konfiguroVleraFillestare(idPerdoruesi, idNdermarrje, rm, ci, idGjuha);
                hfStatusi.Value = "true";
                if (ASPxPageControl2.ActiveTabIndex == 0)
                    grid_ListKPFsh1.FilterExpression = String.Empty;
                else if (ASPxPageControl2.ActiveTabIndex == 1)
                    grid_ListKPFsh2.FilterExpression = String.Empty;
                else if (ASPxPageControl2.ActiveTabIndex == 2)
                    grid_ListKPFsh3.FilterExpression = String.Empty;
            }
        }
        /// <summary>
        /// Aplikon filtrin e zgjedhur mbi gride
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected void Apliko_ASPxButton_Click(object sender, EventArgs e)
        //    {
        //    //kap item qe ka template ne menune e kesaj faqeje
        //    DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
        //    ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;

        //    DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
        //    DbCore.DbAdmin.clsFiltraGrida filtra = dbAdmin.merrFiltraGridaSipasFiltraKodi(cmbFiltra.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //    if (filtra.FiltraKodi != null)
        //        {
        //        if (ASPxPageControl2.ActiveTabIndex == 0)
        //            {
        //            grid_ListKPFsh1.FilterExpression = filtra.FiltraVlera;
        //            if (filtra.DrejtimRenditje == true)
        //                grid_ListKPFsh1.SortBy(grid_ListKPFsh1.Columns[filtra.KoloneRenditje], DevExpress.Data.ColumnSortOrder.Ascending);
        //            else
        //                grid_ListKPFsh1.SortBy(grid_ListKPFsh1.Columns[filtra.KoloneRenditje], DevExpress.Data.ColumnSortOrder.Descending);
        //            konfiguroVleraFillestare1();
        //            percaktoTamplate1();
        //            }
        //        else if (ASPxPageControl2.ActiveTabIndex == 1)
        //            {   grid_ListKPFsh2.FilterExpression = filtra.FiltraVlera;
        //            if (filtra.DrejtimRenditje == true)
        //                grid_ListKPFsh2.SortBy(grid_ListKPFsh2.Columns[filtra.KoloneRenditje], DevExpress.Data.ColumnSortOrder.Ascending);
        //            else
        //                grid_ListKPFsh2.SortBy(grid_ListKPFsh2.Columns[filtra.KoloneRenditje], DevExpress.Data.ColumnSortOrder.Descending);
        //            konfiguroVleraFillestare2();
        //            percaktoTamplate2();
        //            }
        //        else if (ASPxPageControl2.ActiveTabIndex == 2)
        //            { grid_ListKPFsh3.FilterExpression = filtra.FiltraVlera;
        //            if (filtra.DrejtimRenditje == true)
        //                grid_ListKPFsh3.SortBy(grid_ListKPFsh3.Columns[filtra.KoloneRenditje], DevExpress.Data.ColumnSortOrder.Ascending);
        //            else
        //                grid_ListKPFsh3.SortBy(grid_ListKPFsh3.Columns[filtra.KoloneRenditje], DevExpress.Data.ColumnSortOrder.Descending);
        //            konfiguroVleraFillestare3();
        //            percaktoTamplate3();
        //            }

        //        hfStatusi.Value = "true";
        //        ASPxButton btnFshiFilter = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFshi") as ASPxButton;
        //        btnFshiFilter.ClientEnabled = true;

        //        }
        //    else hfStatusi.Value = "false";
        //    if (cmbFiltra.Text != "" && filtra.FiltraKodi == null)
        //        {
        //        ASPxButton btnRuaj = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("Button1") as ASPxButton;

        //        btnRuaj.ClientEnabled = true;

        //        }
        //    // this.Filtri_ASPxTextBox.Text = "";
        //    }
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
            filtri.FiltraUniversal = false;// Universal_ASPxCheckBox.Checked;
            //DbCore.DbAdmin.clsGridaKoka koka = dbAdmin.merrGridaKokaByEmri("grid_ListKPFsh", "Shto_KPF.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "grid_ListKPFsh", "Shto_KPF.aspx", idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtri.GridaKokaId = koka.IdGridaKoka;
            if (ASPxPageControl2.ActiveTabIndex == 0)
            {
                filtri.FiltraVlera = grid_ListKPFsh1.FilterExpression;
                filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("KodiKPF", grid_ListKPFsh1);
                //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = grid_ListKPFsh1.GetSortedColumns();
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
                //    filtri.KoloneRenditje = "KodiKPF";
                //    filtri.DrejtimRenditje = true;
                //}
            }
            else
                if (ASPxPageControl2.ActiveTabIndex == 1)
            {
                filtri.FiltraVlera = grid_ListKPFsh2.FilterExpression;
                filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("KodiKPF", grid_ListKPFsh2);
                //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = grid_ListKPFsh2.GetSortedColumns();
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
                //    filtri.KoloneRenditje = "KodiKPF";
                //    filtri.DrejtimRenditje = true;
                //}
            }
            else
                    if (ASPxPageControl2.ActiveTabIndex == 2)
            {
                filtri.FiltraVlera = grid_ListKPFsh3.FilterExpression;
                filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("KodiKPF", grid_ListKPFsh3);
                //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = grid_ListKPFsh3.GetSortedColumns();
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
                //    filtri.KoloneRenditje = "KodiKPF";
                //    filtri.DrejtimRenditje = true;
                //}
            }
            //DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();
            //oPerdorues = (DbCore.DbAdmin.clsPerdorues)(CacheLayer.GlobalCacheManager.MySessionCache["oClsPerdoruesi"]);
            filtri.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            filtri.IdNdermarje = idNdermarrje;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            filtri.IdStatusDok = 1;
            mesazh = filtri.ruaj();
            //mbushComboBoxFiltra();
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "grid_ListKPFsh", int.Parse(cmbKonfigurimi.Value.ToString()), "Shto_KPF.aspx");
            percaktoTemplateMenu(idgjuha, idviti, idPerdoruesi, idNdermarrje, ASPxMenu1);
            if (mesazh.Status == true)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

            hfStatusi.Value = "true";
            cmbFiltra.Text = "";
            //ASPxButton btnRuaj = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("Button1") as ASPxButton;
            //ASPxButton btnFshiFilter = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFshi") as ASPxButton;

            //btnRuaj.ClientEnabled = false;
            //btnFshiFilter.ClientEnabled = false;

        }
        /// <summary>
        /// sherben per te validuar kodin e filtrit
        /// </summary>
        /// <param name="source">derguesi</param>
        /// <param name="args">argumentat</param>
        //protected void Kodi_CustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
        //    {
        //    args.IsValid = true;
        //    DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
        //    if (dbAdmin.ekzistonFilter(Kodi_ASPxTextBox.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session)))
        //        args.IsValid = false;
        //    }
        /// <summary>
        /// perdoret per te fshire reshtat e zgjedhur ne gride pasi perdoruesi ka konfirmuar fshirjen
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            //clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "", pnlMesazhi);
            List<object> rreshtat = null;

            if (ASPxPageControl1.ActiveTabIndex == 0)//nqs kemi zgjedhur vetem nje kartele per te pare fshijme vetem ate
                if (ASPxPageControl2.ActiveTabIndex == 0)
                    rreshtat = grid_ListKPFsh1.GetSelectedFieldValues("IdKPF");
                else if (ASPxPageControl2.ActiveTabIndex == 1)
                    rreshtat = grid_ListKPFsh2.GetSelectedFieldValues("IdKPF");
                else rreshtat = grid_ListKPFsh3.GetSelectedFieldValues("IdKPF");
            else
            {
                rreshtat = new List<object>();
                rreshtat.Add(hfId.Value);
            }
            //
            //if (ASPxPageControl2.ActiveTabIndex == 0)
            //    rreshtat = grid_ListKPFsh1.GetSelectedFieldValues("IdKPF");
            //else if (ASPxPageControl2.ActiveTabIndex == 1)
            //    rreshtat = grid_ListKPFsh2.GetSelectedFieldValues("IdKPF");
            //else rreshtat = grid_ListKPFsh3.GetSelectedFieldValues("IdKPF");
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (rreshtat.Count == 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgShtoKPFZgjidhni1StruktureLlogarie", ci), pnlMesazhi);
                return;
            }
            List<string> TeFshire = new List<string>(), TePaFshire = new List<string>();
            DbCore.DbRegjistrim.clsDatabaseRegjistrim dbRegjistrim = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
            int idPerdorues = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            foreach (object id in rreshtat)
            {
                //DbCore.DbKontabiliteti.clsDatabaseKontabilitet dbKontabilitet = new DbCore.DbKontabiliteti.clsDatabaseKontabilitet();
                //DbCore.DbKontabiliteti.colKPFte colKPFte = dbKontabilitet.ktheKPF(id);
                DbCore.DbKontabiliteti.clsKPF clskpf = new DbCore.DbKontabiliteti.clsKPF(Convert.ToInt32(id));
                clskpf.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                //foreach (DbCore.DbKontabiliteti.clsKPF k in colKPFte)
                //    {
                DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                konf.mbushKonfigAmbjSipasId(clskpf.IdKonfig);
                //konf.IdKonfigAmbjente = clskpf.IdKonfig;
                //konf = new DbCore.DbShare.clsDatabaseShare().merrKonfigAmbjSipasId(konf)[0];

                bool lidhur = dbRegjistrim.eshteDokumentiILidhurCelje(clskpf.IdKPF.ToString(), konf.IdNivel.ToString());
                if (lidhur)
                {
                    TePaFshire.Add(clskpf.KodiKPF);
                    continue;
                }
                DbCore.clsMesazh mesazh = clskpf.fshiKPFAndBuxhete(Convert.ToInt32(id), idPerdorues);

                if (clskpf.IdKPF == 0)
                    continue;
                if (mesazh.Status)
                {
                    #region Heq artikujt nga grida

                    // ASPxGridView_Artikull.DataSource = dt;
                    hiqKPFNgaGrida(clskpf.IdKPF, rm, ci);
                    #endregion
                    TeFshire.Add(clskpf.KodiKPF);

                    ASPxPageControl1.ActiveTabIndex = 0;
                    hfStatusi.Value = "true";
                }
            }
            dbRegjistrim.Dispose();
            string mesazhInfoGabim = "", mesazhInfoSukses = "";
            if (TePaFshire.Count == 1)
                mesazhInfoGabim = String.Format("{0}{1}{2}", rm.GetString("msgShtoKPFPrefixNjejes", ci), String.Join(";", TePaFshire), rm.GetString("msgShtoArtikullSuffixNjejesGabimi", ci));
            else
                if (TePaFshire.Count > 1)
                    mesazhInfoGabim = String.Format("{0}{1}{2}", rm.GetString("msgShtoKPFPrefixShumes", ci), String.Join(";", TePaFshire), rm.GetString("msgShtoArtikullSuffixShumesGabimi", ci));
            if (TeFshire.Count == 1)
                mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("msgShtoKPFPrefixNjejes", ci), String.Join(";", TeFshire), rm.GetString("regjMagSuffixMesazhNjejesSuksesi", ci));
            else
                if (TeFshire.Count > 1)
                    mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("msgShtoKPFPrefixShumes", ci), String.Join(";", TeFshire), rm.GetString("msgCeljeMagazinatSuffixShumesSuksesi", ci));
            if (mesazhInfoGabim != "" && mesazhInfoSukses != "")
                mesazhInfoGabim += rm.GetString("msgLidhesMesazhi", ci) + mesazhInfoSukses;
            if (mesazhInfoGabim != "")
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhInfoGabim, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazhInfoSukses, pnlMesazhi);
            pnlMesazhi.Update();
        }
        private void hiqKPFNgaGrida(int idKpf, ResourceManager rm, CultureInfo ci)
        {
            if (grid_ListKPFsh1.DataSource != null)
            {
                DataTable dt;
                if (ASPxPageControl2.ActiveTabIndex == 0)
                    dt = (DataTable)grid_ListKPFsh1.DataSource;
                else if (ASPxPageControl2.ActiveTabIndex == 1)
                    dt = (DataTable)grid_ListKPFsh2.DataSource;
                else dt = (DataTable)grid_ListKPFsh3.DataSource;

                DataRow[] drs = dt.Select("IdKPF = " + idKpf);
                if (drs.Length > 1)
                    throw new DbCore.MyException(rm.GetString("msgShtoKPFJane2StrukturaLlogMeTeNjejtenIdNeGride", ci));
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                dt.Rows.Remove(dr);

                if (ASPxPageControl1.ActiveTabIndex == 0)
                    grid_ListKPFsh1.DataBind();
                else if (ASPxPageControl1.ActiveTabIndex == 1)
                    grid_ListKPFsh2.DataBind();
                else grid_ListKPFsh3.DataBind();

                    

            }
            else mbushGridKPFshNgaDB();
        }
        private void shtoKPFNeGrid(int idNdermarrje, int idPerdorues, int idKPF, ResourceManager rm, CultureInfo ci)
        {
            if (grid_ListKPFsh1.DataSource != null)
            {
                DataTable dt;
                if (ASPxPageControl2.ActiveTabIndex == 0)
                    dt = (DataTable)grid_ListKPFsh1.DataSource;
                else if (ASPxPageControl2.ActiveTabIndex == 1)
                    dt = (DataTable)grid_ListKPFsh2.DataSource;
                else dt = (DataTable)grid_ListKPFsh3.DataSource;
                DataRow[] drs = dt.Select("IdKPF = " + idKPF);
                if (drs.Length > 0)
                    throw new DbCore.MyException(rm.GetString("msgShtoKPFStrukturaLlogEkzistonNeGride", ci));
                DataRow newArtDr = DbCore.DbKontabiliteti.colKPFte.merrSipasKPFNdermarrjesAndAutorizimeDR(idNdermarrje, idPerdorues, idKPF);
                dt.ImportRow(newArtDr);
            }
            else mbushGridKPFshNgaDB();
        }
        private void modifikoKPFNeGrid(int idNdermarrje, int idPerdorues, int idKPF, ResourceManager rm, CultureInfo ci)
        {
            if (grid_ListKPFsh1.DataSource != null)
            {
                DataTable dt;
                if (ASPxPageControl2.ActiveTabIndex == 0)
                    dt = (DataTable)grid_ListKPFsh1.DataSource;
                else if (ASPxPageControl2.ActiveTabIndex == 1)
                    dt = (DataTable)grid_ListKPFsh2.DataSource;
                else dt = (DataTable)grid_ListKPFsh3.DataSource;
                DataRow[] drs = dt.Select("IdKPF = " + idKPF);
                if (drs.Length > 1)
                    throw new DbCore.MyException(rm.GetString("msgShtoKPFJane2StrukturaLlogMeTeNjejtenIdNeGride", ci));
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                DataRow newArtDr = DbCore.DbKontabiliteti.colKPFte.merrSipasKPFNdermarrjesAndAutorizimeDR(idNdermarrje, idPerdorues, idKPF);
                //  dt.Rows.Remove(dr);
                // dt.ImportRow(newArtDr);
                object[] arr = newArtDr.ItemArray;
                dr.ItemArray = arr;
            }
            else mbushGridKPFshNgaDB();
        }

        /// <summary>
        /// metoda per te thirrur veprimet e menuse kur shtypen butonat
        /// </summary>
        /// <param name="source">derguesi</param>
        /// <param name="e">parametrat</param>
        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {
            if (e.Item.Name == "Ruaj")
            {
                Page.Validate("entries"); ruajKPF();
            }
        }
        /// <summary>
        /// perdoret per te shfaqur te gjithe komponentet kur zgjidhet (...)
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumenti</param>
        protected void grid_ListKPFsh1_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if (e.Column.FieldName == "NiveliKPF")
            {
                if (e.Value.ToString() == "")
                {
                    e.Criteria = null;
                }
            }
        }
        /// <summary>
        /// perdoret per te shfaqur te gjithe komponentet kur zgjidhet (...)
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumenti</param>
        protected void grid_ListKPFsh3_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if (e.Column.FieldName == "NiveliKPF")
            {
                if (e.Value.ToString() == "")
                {
                    e.Criteria = null;
                }
            }
        }
        /// <summary>
        /// perdoret per te shfaqur te gjithe komponentet kur zgjidhet (...)
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumenti</param>
        protected void grid_ListKPFsh2_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if (e.Column.FieldName == "NiveliKPF")
            {
                if (e.Value.ToString() == "")
                {
                    e.Criteria = null;
                }
            }
        }
        /// <summary>
        /// perdoret per te shtuar properti ne javascript
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void grid_ListKPFsh1_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = grid_ListKPFsh1.PageIndex;
            e.Properties["cpPageRow"] = grid_ListKPFsh1.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = grid_ListKPFsh1.VisibleRowCount;
        }
        /// <summary>
        /// perdoret per te shtuar properti ne javascript
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void grid_ListKPFsh2_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = grid_ListKPFsh2.PageIndex;
            e.Properties["cpPageRow"] = grid_ListKPFsh2.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = grid_ListKPFsh2.VisibleRowCount;
        }
        /// <summary>
        /// perdoret per te shtuar properti ne javascript
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void grid_ListKPFsh3_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = grid_ListKPFsh3.PageIndex;
            e.Properties["cpPageRow"] = grid_ListKPFsh3.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = grid_ListKPFsh3.VisibleRowCount;
        }
        /// <summary>
        /// konfiguron listboxin e llogarive te lidhura me kete llogari standarte
        /// </summary>
        private void konfiguroListe()
        {

            //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            //DbCore.DbAdmin.clsGridaKoka koka = dbAdmin.merrGridaKokaByEmri("listBoxLlogarite", "Modifiko_KPF.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            var idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuha, "listBoxLlogarite", "Shto_KPF.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(base.Session), 1);
            koka.OColGridaTrupi.mbushTrupinEng(idGjuha, koka.IdGridaKoka);
            int i = 0;
            foreach (DbCore.DbAdmin.clsGridaTrupi grida in koka.OColGridaTrupi)
            {
                ListBoxColumn a = new ListBoxColumn(grida.KodiTrupi);
                listBoxLlogarite.Columns.Add(a);
                a.Width = 90;

                listBoxLlogarite.Columns[i].Caption = grida.PershkrimiTrupi;
                listBoxLlogarite.Columns[i].VisibleIndex = grida.IndexTrupi;
                listBoxLlogarite.Columns[i].Visible = grida.VisibleTrupi;
                i++;
            }

            percaktoAtributeTeListes();
        }

        protected void listBoxLlogarite_DataBound(object sender, EventArgs e)
        {// shton colonen # per selektim dhe disa karakteristika te grides

        }
        /// <summary>
        /// percakton atribute te listboxit te llogarive
        /// </summary>
        private void percaktoAtributeTeListes()
        {

            listBoxLlogarite.CssFilePath = "~/App_Themes/BlackGlass/{0}/styles.css";
            listBoxLlogarite.CssPostfix = "BlackGlass";

        }

        protected void cmbPrindi_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {

            if (!IsCallback) return;

            if (Request.Params["__CALLBACKID"].ToString().Contains("cmbPrindi"))
            {
                int index = 0;
                int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                if (int.TryParse(hfTab.Value.ToString(), out index))
             
                    ConfigureAspxComboBox.mbushComboKPFMeKolona(idPerdoruesi, idNdermarrje, cmbPrindi, index + 1,e);
                else
                    ConfigureAspxComboBox.mbushComboKPFMeKolona(idPerdoruesi, idNdermarrje, cmbPrindi, 1,e);
            }
        }

        /// <summary>
        /// mbush listboxin e llogarive me te dhena
        /// </summary>
        private void konfiguroListeLlogarish()
        {
            DbCore.DbKontabiliteti.colLlogarite colKPFte = new DbCore.DbKontabiliteti.colLlogarite();
            DataTable dt = DbCore.DbKontabiliteti.colLlogarite.mbushLlogariteNgaKPF(-5);
            this.listBoxLlogarite.DataSource = dt;
            this.listBoxLlogarite.DataBind();
            dt.Dispose();
        }
     
        /// <summary>
        /// perdoret per te mbushur combon e prindit ne momentin qe perdoruesi fillon te shkruaj
        /// </summary>
        /// <param name="source">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void cmbPrindi_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {

            if (!IsCallback) return;
            
                if (Request.Params["__CALLBACKID"].ToString().Contains("cmbPrindi"))
                {
                    int index = 0;
                    int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                    int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                    if (int.TryParse(hfTab.Value.ToString(), out index))
                        ConfigureAspxComboBox.mbushComboKPFMeKolona(idPerdoruesi, idNdermarrje, index + 1, cmbPrindi,e);
                    else
                        ConfigureAspxComboBox.mbushComboKPFMeKolona(idPerdoruesi, idNdermarrje, 1, cmbPrindi,e);
                }
            
        }
        /// <summary>
        /// perdoret per te marre llogarite sipas llogarise standarte te zgjedhur
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void listBoxLlogarite_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            //DbCore.DbKontabiliteti.clsDatabaseKontabilitet dbKontabilitet = new DbCore.DbKontabiliteti.clsDatabaseKontabilitet();
            int id = -1; DbCore.DbKontabiliteti.colLlogarite colKPFte = new DbCore.DbKontabiliteti.colLlogarite();
            DataTable dt = DbCore.DbKontabiliteti.colLlogarite.mbushLlogariteNgaKPF(-5);
            if (e.Parameter != "-1")
            {
                int index = 0;
                int.TryParse(e.Parameter, out index);
                if (ASPxPageControl2.ActiveTabIndex == 0)
                    int.TryParse(grid_ListKPFsh1.GetRowValues(index, "IdKPF").ToString(), out id);
                if (ASPxPageControl2.ActiveTabIndex == 1)
                    int.TryParse(grid_ListKPFsh2.GetRowValues(index, "IdKPF").ToString(), out id);
                if (ASPxPageControl2.ActiveTabIndex == 2)
                    int.TryParse(grid_ListKPFsh3.GetRowValues(index, "IdKPF").ToString(), out id);
                dt = DbCore.DbKontabiliteti.colLlogarite.mbushLlogariteNgaKPF(id);
            }
            //DbCore.DbKontabiliteti.colLlogarite colKPFte = dbKontabilitet.merrLlogariteNgaKPF(id);

            this.listBoxLlogarite.DataSource = dt;// colKPFte;
            this.listBoxLlogarite.DataBind();
            dt.Dispose();
        }
    }
}