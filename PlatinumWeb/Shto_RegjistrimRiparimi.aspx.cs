using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using DevExpress.Web;
using System.Collections.Generic;
using DbCore.DbKontabiliteti;
using DbCore.DbRegjistrim;
using System.Web.Script.Serialization;
using DbCore;
using DbCore.DbInventari;
using DbCore.DbShare;
using DbCore.DbAdmin;
using System.Web.UI.WebControls;
using PlatinumWeb.Templates;
using System.Resources;
using System.Globalization;
using DbCore.IMBUtils.Extensions;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{
    public partial class Shto_RegjistrimRiparimi : MyPageBase
    {

        private static string STR_zgjidhniDtRegj = "Zgjidhni nje datë regjistrimi!";


        private colTrupiMagazina trupat = new colTrupiMagazina();
        public void MerrTedhenat(int idPerdoruesi, int idViti, int idNdermarrje, clsKokaRiparime koka)
        {

            //string kodNiveli = DbCore.DbRegjistrim.clsNivelRegjistrimi.ktheKodNivelRegjistrimi(koka.IdNivel);
            //cmbLloji.Text = kodNiveli;
            //mbushComboKonfigurimet(true);
            //DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            //konf.mbushKonfigAmbjSipasId(koka.IdKonfigAmbjente);
            //cmbKonfigurimi.Text = konf.KodKonfigAmbjente;
            //hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;
            //if (koka.IdMagazina != 0)
            //    btneMagazina.Text = DbCore.DbRegjistrim.clsNjesiAdministrative.mbushKodiNjesiAdministrativeSipasiD(koka.IdMagazina, idPerdoruesi);
            txtNrKontakti.Text = koka.NrKontakti;

            txtAksesor.Text = koka.Aksesor;
            DbCore.DbInventari.clsLlojDifekti difekti = new clsLlojDifekti(koka.IdDifekti);
            cmbDifekti.Text = difekti.Kodi;
            DbCore.DbInventari.clsStatusRiparimi statusrip = new clsStatusRiparimi(koka.IdStatusRiparimi);
            cmbStatus.Text = statusrip.Pershkrimi;

            cmbDorezuar.Value = koka.Dorezuar.ToString();
            inicializoGridAktuale();
            konfiguroGrideAktuale(idPerdoruesi, false);
            inicializoGridHistoriku();
            konfiguroGrideHistoriku(idPerdoruesi, false);
            inicializoGridLoan();
            konfiguroGrideLoan(idPerdoruesi, false);

            colTrupiRiparime trupivjeter = new colTrupiRiparime(koka.IdKoka, idNdermarrje);
            if (trupivjeter.Count > 0)
            {
                txtIMEISwap.Text = trupivjeter[0].DetSwap;
                txtProdukti.Text = trupivjeter[0].ArtSwap;
            }
            //DataTable dtlidhur = koka.merrIdsDokLidhur();// new DbCore.DbAdmin.clsDatabaseAdmin().MerrDokLidhur(koka.IdKokaMagazina, koka.IdNivel, "T_KOKAMAGAZINA", "IDKOKAMAGAZINA");
            //hfLidhur.Value = (!(dtlidhur.Rows.Count == 0)).ToString();
            //DbCore.clsFunksione.ShtoLidhje(idPerdoruesi, idViti, idNdermarrje, hl, dtlidhur, koka.IdGjenerues, koka.IdNivelGjenerues, koka.IdKonfigGjenerues);
        }


   

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                DbCore.clsFunksione.logout(Session,true,"FaqePaautorizuar");
            }
            int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerdoruesi);
                return;
            }
            if (DbCore.mySessionObjects.merrPeriudheKontabel(Session) != null)
            {
                DbCore.DbAdmin.clsPeriudhaKontabel periudha = DbCore.mySessionObjects.merrPeriudheKontabel(Session);
            }
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            int idNdermarrjeVit = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            percaktoTemplateMenu(idPerdoruesi, idViti, idNdermarrje, ASPxMenu1);
            int idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);

            if (!IsPostBack)
            {
                perktheLabel();
                clsNivelCmimi nivelBaze = new clsNivelCmimi();
                nivelBaze.mbushNivelCmimiBaze(idNdermarrje, 0);
                hfState.Add("idNdermarje", idNdermarrje);
                hfState.Add("idperdoruesi", idPerdoruesi);
                hfState.Add("nivelibaze", nivelBaze.IdNivelCmimi);
                hfState.Set("idGjuha", idGjuha);
                hfState.Set("idNdermarrjeVit", idNdermarrjeVit);
     

                DbCore.DbAdmin.clsPerdorues per = new DbCore.DbAdmin.clsPerdorues(idPerdoruesi);
                DbCore.mySessionObjects.ruajdtNeSession(Session, new DataTable());
                System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
                if (hfShtimModifikim.Value == "")
                {
                    if (string.IsNullOrEmpty(Request.QueryString["shtim_modifikim"]) || Request.QueryString["shtim_modifikim"] == "shtim")
                    {
                        hfShtimModifikim.Value = "shtim";
                        konfiguroVleraFillestareShto(idNdermarrje, idGjuha, idPerdoruesi, rm, ci);
                    }

                    else
                    {
                        hfShtimModifikim.Value = "riparim";
                        konfiguroVleraFillestareModifiko(idPerdoruesi, idViti, idNdermarrje, idGjuha, rm, ci);
                    } percaktoTemplateMenu(idPerdoruesi, idViti, idNdermarrje, ASPxMenu1);

                }
                else
                    if (hfShtimModifikim.Value == "shtim")
                        konfiguroVleraFillestareShto(idNdermarrje, idGjuha, idPerdoruesi, rm, ci);
                    else
                        if (hfShtimModifikim.Value == "riparim")
                            konfiguroVleraFillestareModifiko(idPerdoruesi, idViti, idNdermarrje, idGjuha, rm, ci);

                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "Shto_RegjistrimRiparimi.aspx");
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                hfTeDrejta.Add("ShtimDraft", tedrejtaInfo.DShtimDraft);
                hfTeDrejta.Add("ModifikimDraft", tedrejtaInfo.DModifikimDraft);
            }
            else merrArtikujLoanSession();

        }

         public void perktheLabel()
        {
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ASPxNavBar1.Groups[0].Text = rm.GetString("lblArtikullGaranci", ci);
            lblZgjidhProduktin.Text = rm.GetString("lblZgjidhProduktin", ci);
        }
        public void btnJo_Click(object sender, EventArgs e)
        {
            DbCore.mySessionObjects.ruajTrupatNeSession(Session, new DbCore.DbRegjistrim.colTrupiMagazina());
            if (pergjigja.Text == "fshi")
            {
                Response.Redirect("RegjistrimRiparimi.aspx?fshi=po");
                return;
            }
            else if (pergjigja.Text == "ruaj")
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "Ruajtja përfundoi me sukses!", pnlMesazhi);

        }

        public void btnPo_Click(object sender, EventArgs e)
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            trupat = DbCore.mySessionObjects.merrTrupatNgaSesioni(Session);
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh(true, "Rivlerësimi përfundoi me sukses!");
            string fileLogPath = Server.MapPath("~/log/log.txt");
            DbCore.DbAdmin.clsLogRivleresimInventari log = new DbCore.DbAdmin.clsLogRivleresimInventari();
            try
            {
                log = new DbCore.DbAdmin.clsLogRivleresimInventari((int)hfState.Get("idperdoruesi"), (int)hfState.Get("idNdermarje"));
            }
            catch (Exception err)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
                throw new DbCore.MyException(rm.GetString("msgGabimGjateRuajtjesSeRivleresimitNeLog", DbCore.mySessionObjects.ktheCultureInfo(Session)));
            }
            foreach (DbCore.DbRegjistrim.clsTrupiMagazina t in trupat)
            {

                //DbCore.DbInventari.clsArtikulli art = new DbCore.DbInventari.clsArtikulli(t.IdArtikulli);
                mesazh = DbCore.DbInventari.clsArtikulli.rivleresimCmimiMesatar(t.IdArtikulli, DbCore.DbInventari.clsArtikulli.ktheMetodeKostoje(t.IdArtikulli), t.IdMag, t.Data, DateTime.Today, clsArtikulli.ktheKontrollCmimiPerDetajim(t.IdArtikulli),log,cultinf,rm, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
                if (!mesazh.Status)
                {
                    Response.Redirect("RegjistrimRiparimi.aspx?fshi=rivleresimjo");
                    return;
                }
            }

            DbCore.mySessionObjects.ruajTrupatNeSession(Session, new DbCore.DbRegjistrim.colTrupiMagazina());
            if (mesazh.Status)
            {
                Response.Redirect("RegjistrimRiparimi.aspx?fshi=rivleresimpo");
                return;
            }
        }


        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        private void percaktoTemplateMenu(int idPerdoruesi, int idViti, int idNdermarrje, ASPxMenu aSPxMenu1)
        {
            DbCore.DbShare.colMenuItem menu = new DbCore.DbShare.colMenuItem(DbCore.mySessionObjects.ktheGjuhe(Session));
            menu.merrMenuItemSipasKomponentesRegjistrime(DbCore.mySessionObjects.ktheGjuhe(Session), "Shto_RegjistrimRiparimi.aspx", idPerdoruesi, idNdermarrje, idViti, hfShtimModifikim.Value == "modifikim" ? false : true);

            foreach (DbCore.DbShare.clsMenuItem m in menu)
            {
                if (m.Name != "ItemFilter" && m.Name != "ItemFrame")
                {
                    //if (!m.Enabled) continue;
                    clsToolbarConfig.ShtoMenuItem(this.Theme, aSPxMenu1, m);
                }

                if ((hfLidhur.Value == "True") && m.Name == "Draft")
                {
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                }

                if ((hfShtimModifikim.Value != "modifikim") && m.Name == "Fshi")
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;

                if (m.Name == "Ruaj")
                {
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = true;
                }

                if (m.Name == "ItemFrame")
                    clsToolbarConfig.ShtoMenuItemPerFrame(this, aSPxMenu1, DbCore.clsFunksione.ktheUrlHelpi(m.UrlHelp).Item1);
                if (m.Name == "Shto" || m.Name == "Kerko" || m.Name == "Pastro" || m.Name == "ItemFilter" || m.Name == "ItemFrame")
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].BeginGroup = true;
            }

            clsMenuInfo.ShtoMenuItemInfo(this, MenuInfo);
        }

        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.ktheNdermarrjeVit(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), ASPxMenu1);

        }

        private void vendosDataDefault()
        {
            DateTime sot = new DateTime();
            sot = DateTime.Today;
            DbCore.DbAdmin.clsPeriudhaKontabel periudha = DbCore.mySessionObjects.merrPeriudheKontabel(Session);
            if (sot >= periudha.FillimiPeriudha && sot <= periudha.MbarimiPeriudha)
                this.dteDtDok.Value = DateTime.Today;
            else
                dteDtDok.Value = periudha.FillimiPeriudha;
        }

        /// <summary>
        /// Vendos vlerat default kur po behet shtim
        /// </summary>
        /// <param name="idNdermarrje"></param>
        private void konfiguroVleraFillestareShto(int idNdermarrje, int idGjuha, int idPerdoruesi, ResourceManager rm, CultureInfo cultinf)
        {
            dteDtDok.Date = DateTime.Today;
            AspxWebControlUtils.vendosDateEditMask(dteDtDok);
            dteDtRegjistrimi.Date = DateTime.Today;
            vendosDataDefault();
            AspxWebControlUtils.vendosDateEditMask(dteDtRegjistrimi);
            inicializoGridFaturat(idNdermarrje);
            konfiguroGrideFaturat(idPerdoruesi, true);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(btneMagazina);
            mbushComboNivelesh(idNdermarrje, cmbLloji);
            cmbLloji.SelectedIndex = 0;
            mbushComboKonfigurimet(false, idGjuha, idNdermarrje, idPerdoruesi, rm, cultinf);
            cmbKonfigurimi.SelectedIndex = 0;
            ConfigureAspxComboBox.mbushComboStatusRiparimi(idNdermarrje, cmbStatus); ;
            ConfigureAspxComboBox.mbushComboDorezuar(cmbDorezuar); ;
            cmbStatus.SelectedIndex = 0;
            ConfigureAspxComboBox.mbushComboLlojDifekti(idNdermarrje, cmbDifekti); ;
            cmbDifekti.SelectedIndex = 0;
            ConfigureAspxComboBox.mbushComboMagazinat(idNdermarrje, btneMagazina, idPerdoruesi, true, 1,true);
            cmbLloji.ClientSideEvents.Init = "function(s,e){TextChangedLloji();}";
        }

        private void mbushComboNivelesh(int idNdermarrje, ASPxComboBox cmblloji)
        {
            cmblloji.DataSource = DbCore.DbRegjistrim.colNivelRegjistrimi.ktheGjitheNivelRegjistrimiSipasKategoriDtCombo(80, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), false);
            cmblloji.TextField = "Kodi";
            cmblloji.ValueField = "IdNivel";
            cmblloji.DataBind();
            cmblloji.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        }

        /// <summary>
        /// Vendos vlerat default kur po behet modifikim
        /// </summary>
        /// <param name="idPerdoruesi"></param>
        /// <param name="idNderViti"></param>
        /// <param name="idNdermarrje"></param>
        private void konfiguroVleraFillestareModifiko(int idPerdoruesi, int idViti, int idNdermarrje, int idGjuha, ResourceManager rm, CultureInfo cultinf)
        {//mbush kombot dhe gridat
            // txtNrDok.Enabled = false;

            AspxWebControlUtils.vendosDateEditMask(dteDtDok);
            AspxWebControlUtils.vendosDateEditMask(dteDtRegjistrimi);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(btneMagazina);
            vendosDataDefault();
            mbushComboNivelesh(idNdermarrje, cmbLloji);
            cmbLloji.SelectedIndex = 1;
            mbushComboKonfigurimet(false, idGjuha, idNdermarrje, idPerdoruesi, rm, cultinf);
            cmbKonfigurimi.SelectedIndex = 0;
            ConfigureAspxComboBox.mbushComboStatusRiparimi(idNdermarrje, cmbStatus); ;
            ConfigureAspxComboBox.mbushComboLlojDifekti(idNdermarrje, cmbDifekti); ;
            ConfigureAspxComboBox.mbushComboDorezuar(cmbDorezuar); ;
            ConfigureAspxComboBox.mbushComboMagazinat(idNdermarrje, btneMagazina, idPerdoruesi, true, 1,true);

            int id = int.Parse(Request.QueryString["id"]);
            clsKokaRiparime kok = new clsKokaRiparime();


            kok.IdKoka = id;
            kok.mbushKokaRiparimSipasID(kok.IdKoka);
            if (kok != null)
            {
                MerrTedhenat(idPerdoruesi, idViti, idNdermarrje, kok);
            }


        }



        private void mbushComboKonfigurimet(bool mod, int idGjuha, int idNdermarje, int idPerdoruesi, ResourceManager rm, CultureInfo cultinf)
        {
            DbCore.DbShare.colKonfigurimAmbjenti colKonfig = new DbCore.DbShare.colKonfigurimAmbjenti();
            int idKategori = 80;//magazina
            if (cmbLloji.Value != null)
            {
                int idNivel = int.Parse(cmbLloji.Value.ToString());
                colKonfig.mbushKonfigAmbjSipasIdKategoriIdNivelMeLloj(idKategori, idNivel, idPerdoruesi);

            }
            else
                colKonfig.mbushKonfigAmbjSipasIdKategori(idKategori, idNdermarje, idPerdoruesi, idGjuha);
            cmbKonfigurimi.IncrementalFilteringMode = DevExpress.Web.IncrementalFilteringMode.Contains;
            DbCore.DbShare.colKonfigurimAmbjenti konfVarura = new DbCore.DbShare.colKonfigurimAmbjenti();
            foreach (DbCore.DbShare.clsKonfigurimAmbjenti konfi in colKonfig)
            {
                var alternativa = clsAlternativaKushti.getAlternativa(konfi.IdKonfigAmbjente, "V");
                if (alternativa == "Po" && !mod)
                    konfVarura.Add(konfi);
            }
            foreach (DbCore.DbShare.clsKonfigurimAmbjenti konfi in konfVarura)
            {
                colKonfig.Remove(konfi);
            }
            ListBoxColumn colprove = new ListBoxColumn();
            colprove.FieldName = "KodKonfigAmbjente";
            colprove.Caption = rm.GetString("cmbCmimeArtikulliCaptionKodi", cultinf);
            ListBoxColumn colemer = new ListBoxColumn();
            colemer.FieldName = "PershkrimKonfigAmbjente";
            colemer.Caption = rm.GetString("cmbCmimeArtikulliCaptionPershkrimi", cultinf);
            colemer.Width = 300;
            cmbKonfigurimi.TextFormatString = "{0};{1}";
            cmbKonfigurimi.Columns.Add(colprove);
            cmbKonfigurimi.Columns.Add(colemer);
            cmbKonfigurimi.DataSource = colKonfig;
            cmbKonfigurimi.ValueField = "IdKonfigAmbjente";
            //  cmbKonfigurimi.TextField = "KodKonfigAmbjente";
            cmbKonfigurimi.DataBind();
            cmbKonfigurimi.SelectedIndex = 0;
            hfKonffillestar.Value = cmbKonfigurimi.SelectedItem.Text;
        }


        /// <summary>
        /// Percakton veprimin qe kryhet kur klikohet nje nga butonat e menuse
        /// </summary>
        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {
            if (e.Item.Name == "Ruaj")
            {
                Page.Validate();
                ruajRegjistrimRiparim(1, false);
            }
            if (e.Item.Name == "Draft")
            {
                Page.Validate();
                ruajRegjistrimRiparim(0, false);// 0 = statusi i dokumentit(ne kete rast statusi eshte draft)
            }
            if (e.Item.Name == "RuajPrint")
            {
                Page.Validate();
                ruajRegjistrimRiparim(1, true);
            }
            if (e.Item.Name == "PrintPreview")
            {
                int id = int.Parse(Request.QueryString["id"]);
                clsKokaRiparime regjistrime = new clsKokaRiparime(id);
                clsStatusRiparimi status = new clsStatusRiparimi(regjistrime.IdStatusRiparimi);
                if (status.Pershkrimi == "Kerkese e re")
                {
                    int idRaporti = 0;
                  //  clsGaranciArtikulli garanci = new clsGaranciArtikulli(regjistrime.IdGaranci);
                  ////  clsArtikulli art = new clsArtikulli(garanci.IdArtikulli);
                  //   loan = clsArtikulli.ktheArtLoan(garanci.IdArtikulli);
                  // if (loan) 
                    if (regjistrime.ColTrupi.Count > 0 && regjistrime.ColTrupi[0].IdArtLoan > 0) idRaporti = 147; 
                    else 
                        idRaporti = 146;
                    Container.Attributes["src"] = "RaportiShpejte.aspx?Sesioni=false&idraporti=" + idRaporti + "&idDokumenti=" + regjistrime.IdKoka + "&printo=false";
                }
                else if (status.Pershkrimi == "Aparati dorezuar Klientit")
                {
                    int idRaporti = 0;
                  //  clsGaranciArtikulli garanci = new clsGaranciArtikulli(regjistrime.IdGaranci);
                  // // clsArtikulli art = new clsArtikulli(garanci.IdArtikulli);
                  //bool loan=  clsArtikulli.ktheArtLoan(garanci.IdArtikulli);
                  //if (loan) 
                    if (regjistrime.ColTrupi.Count > 0 && regjistrime.ColTrupi[0].IdArtLoan > 0) idRaporti = 149; 
                    else 
                        idRaporti = 148;
                    Container.Attributes["src"] = "RaportiShpejte.aspx?Sesioni=false&idraporti=" + idRaporti + "&idDokumenti=" + regjistrime.IdKoka + "&printo=false";
                }

                //if (hfShtimModifikim.Value == "riparim")
                //{
                //   
                //   
                //   clsStatusRiparimi status=new clsStatusRiparimi (kokaekzistuese.IdStatusRiparimi)
                //   if(status.Pershkrimi=="Kerkese e re")


                //}
                //id = int.Parse(Request.QueryString["id"]);
                //clsKoka = new DbCore.DbRegjistrim.clsKokaShitje();
                //clsKoka.mbushKokaShitjeSipasID(id);
                //int idRaporti = DbCore.DbShare.clsRaporti.ktheIdRaporti(idGjuha, clsKoka.IdRaportDesing);
                //Container.Attributes["src"] = "RaportiShpejte.aspx?Sesioni=false&idraporti=" + idRaporti + "&idDokumenti=" + clsKoka.IdShitjeKoka + "&printo=false&iddesign=" + cmbFormatiPrintimit.Value;
                // Page.Validate();
                // ruajRegjistrimRiparim(1, true);
            }
        }

        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            pergjigja.Text = "";
            DbCore.DbRegjistrim.colTrupiMagazina tr = new DbCore.DbRegjistrim.colTrupiMagazina();
            bool rivleresim = false;
            clsKokaRiparime kokam = new clsKokaRiparime();
            kokam.IdKoka = int.Parse(Request.QueryString["id"]);
            kokam.mbushKokaRiparimSipasID(kokam.IdKoka);
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            bool lidhur = kokam.eshteILidhur();

            if (lidhur)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Dokumenti është i lidhur dhe nuk mund të fshihet!", pnlMesazhi);
                this.status1.Value = "false";
                return;
            }
            if (kokam.DtDok.Year != new DbCore.DbAdmin.clsNdermarrjeViti(DbCore.mySessionObjects.ktheNdermarrjeVit(Session)).Viti)
            {
                this.status1.Value = "false";
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Data nuk i përket vitit ushtrimor të zgjedhur", pnlMesazhi);
                return;
            }
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            //DbCore.DbAdmin.clsPeriudhaKontabel periudha = new DbCore.DbAdmin.clsPeriudhaKontabel(kokam.DtDok, idNdermarrje);
            //DbCore.clsMesazh mesazhi = periudha.isPeriudheKycur();
            bool ekycur = DbCore.DbAdmin.clsPeriudhaKontabel.eshteKycurPeriudheSipasDateDheNdermarrjes(kokam.DtDok, idNdermarrje);
            if (ekycur)
            {

                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgPeriudhaEKycur", cultinf), pnlMesazhi);
                return;
            }
            clsKokaMagazina kok = new clsKokaMagazina();
            kok.mbushKokaMagazinaSipasIDGjenerues(kokam.IdKoka, 2, kokam.IdKonfigAmbjente);

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
                mesazh = kok.kontrolloGjendjeNeFshirje(new DbCore.DbRegjistrim.colTrupiMagazina(), 0);
                if (!mesazh.Status)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                    return;
                }
            }
            if (kok.IdKokaMagazina != 0 && hfKontrollRivleresim.Value.ToLower() == "true" && kok.IdStatusDok != 0)
                if (kok.rivleresim())
                {
                    rivleresim = true; tr.mbushGjitheTrupiMagazinaNgaKoka(kok.IdKokaMagazina); trupat.AddRange(tr);
                }
            kokam.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            mesazh = kokam.fshi();
            DbCore.mySessionObjects.ruajTrupatNeSession(Session, trupat);

            if (mesazh.Status)
            {
                if (rivleresim)
                {
                    clsMenuInfo.ShtoPyetje(MenuInfo, rm.GetString("regjMagVeprimiSjellNdryshimNeCmimDalje", cultinf), pnlMesazhi, (int)hfState["idGjuha"]);
                    pergjigja.Text = "fshi";
                    pergjigja.ClientVisible = false;
                    return;
                }

                Response.Redirect("RegjistrimRiparimi.aspx?fshi=po");
                this.status1.Value = "true";
                return;
            }
            else
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                this.status1.Value = "false";
                return;
            }
        }

        /// <summary>
        /// Ruan/Modifikon nje objekt clsKokaMagazina.
        /// Therret funksionin <see cref="krijoRegjistrimRiparim"/>
        /// </summary>
        /// <param name="statusDokumenti">Statusi me te cilin po ruhet dokumenti (I rregullt apo draft)</param>
        private void ruajRegjistrimRiparim(int statusDokumenti, bool printo)
        {

            clsKokaRiparime regjistrime = new clsKokaRiparime();
            if (Page.IsValid == false)
                return;
            string shfaqmesazhapolupe = "jo";
            bool eshteOwn = DbCore.mySessionObjects.merrEshteOwnSesioni(Session);
            bool rivleresim = false; colTrupiMagazina tr = new colTrupiMagazina();
            DbCore.DbAdmin.clsPeriudhaKontabel periudha = new DbCore.DbAdmin.clsPeriudhaKontabel();
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            if (isValidRegjistrimRiparime(statusDokumenti, rm, ci))
            {
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);

                try
                {
                    regjistrime = krijoRegjistrimRiparim(statusDokumenti);
                }
                catch (DbCore.MyException myEx)
                {
                    NLog.LogManager.GetCurrentClassLogger().Error(myEx.Message);
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, myEx.Message, pnlMesazhi);
                    status1.Value = "false";
                    return;
                }
                catch (Exception e)
                {
                    NLog.LogManager.GetCurrentClassLogger().Error(e.Message);
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, e.Message, pnlMesazhi);
                    status1.Value = "false";
                    return;
                }
                int idPeriudheZgjedhur = DbCore.mySessionObjects.merrPeriudheKontabel(Session).IdPeriudha;


                if (hfShtimModifikim.Value == "shtim" && cbLoan.Checked && regjistrime.ColTrupi.Count == 0)
                {
                    status1.Value = "false";
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Jepni artikullin loan!", pnlMesazhi);
                    return;
                }
                bool checkPeriudha = false;
                if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "konvertim")
                {
                    periudha = DbCore.mySessionObjects.merrPeriudheKontabel(Session);
                    checkPeriudha = true;
                }

                if (!checkPeriudha)
                    periudha = new DbCore.DbAdmin.clsPeriudhaKontabel(regjistrime.DtDok, idNdermarrje);
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "Shto_RegjistrimRiparimi.aspx");
                bool gjenerodokmag = clsAlternativaKushti.getAlternativa(regjistrime.IdKonfigAmbjente, "GJDM") == "Po";;
                if (hfShtimModifikim.Value == "shtim")
                {
                    if ((statusDokumenti == 1 && !tedrejtaInfo.DShtim) || (statusDokumenti == 0 && !tedrejtaInfo.DShtimDraft))
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                        status1.Value = "false";
                        return;
                    }
                    DbCore.DbRegjistrim.clsKokaMagazina mag = new DbCore.DbRegjistrim.clsKokaMagazina();
                    mesazh = regjistrime.ruaj(hfNrAutoShitje, periudha.IdPeriudha, gjenerodokmag, out shfaqmesazhapolupe, eshteOwn, rm, ci);
                    if (!mesazh.Status)
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                        status1.Value = "false";
                        return;
                    }
                    mag.mbushKokaMagazinaSipasIDGjenerues(regjistrime.IdKoka, 2, regjistrime.IdKonfigAmbjente);
                    if (mag.IdKokaMagazina != 0 && hfKontrollRivleresim.Value.ToLower() == "true" && statusDokumenti != 0)
                        if (mag.rivleresim())
                        {
                            rivleresim = true;
                            tr.mbushGjitheTrupiMagazinaNgaKoka(mag.IdKokaMagazina);
                            trupat.AddRange(tr);
                        }
                }
                else if (hfShtimModifikim.Value == "riparim")
                {
                    if ((statusDokumenti == 1 && !tedrejtaInfo.DMod) || (statusDokumenti == 0 && !tedrejtaInfo.DModifikimDraft))
                    //if (!tedrejtaInfo.DMod)
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                        status1.Value = "false";
                        return;
                    }

                    DbCore.DbRegjistrim.clsKokaMagazina mag = new DbCore.DbRegjistrim.clsKokaMagazina();
                    mesazh = regjistrime.ruaj(hfNrAutoShitje, periudha.IdPeriudha, gjenerodokmag, out shfaqmesazhapolupe, eshteOwn, rm, ci);
                    if (!mesazh.Status)
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                        status1.Value = "false";
                        return;
                    }
                   
                    if (hfKontrollRivleresim.Value.ToLower() == "true")
                    {
                        mag.mbushKokaMagazinaSipasIDGjenerues(regjistrime.IdKoka, 2, regjistrime.IdKonfigAmbjente);
                        if (mag.IdKokaMagazina != 0 && statusDokumenti != 0)
                            if (mag.rivleresimPas())
                            {
                                rivleresim = true;
                                tr.mbushGjitheTrupiMagazinaNgaKoka(mag.IdKokaMagazina);
                                trupat.AddRange(tr);
                            }
                    }

                }

                DbCore.mySessionObjects.ruajTrupatNeSession(Session, trupat);
                pergjigja.Text = "ruaj";

                if (printo)
                {
                    int idRaporti = 0;

                    clsStatusRiparimi status = new clsStatusRiparimi(regjistrime.IdStatusRiparimi);
                    if (status.Pershkrimi == "Kerkese e re")
                    {

                      //  clsGaranciArtikulli garanci = new clsGaranciArtikulli(regjistrime.IdGaranci);
                      ////  clsArtikulli art = new clsArtikulli(garanci.IdArtikulli);
                      //  bool loan=clsArtikulli.ktheArtLoan(garanci.IdArtikulli);
                      //  if (loan) 
                        if (regjistrime.ColTrupi.Count > 0 && regjistrime.ColTrupi[0].IdArtLoan > 0) idRaporti = 147; else idRaporti = 146;

                        Container.Attributes["src"] = "RaportiShpejte.aspx?Sesioni=false&idraporti=" + idRaporti + "&idDokumenti=" + regjistrime.IdKoka + "&printo=true";
                    }
                    else
                        if (status.Pershkrimi == "Aparati dorezuar Klientit")
                        {
                           // clsGaranciArtikulli garanci = new clsGaranciArtikulli(regjistrime.IdGaranci);
                           //// clsArtikulli art = new clsArtikulli(garanci.IdArtikulli);
                           // bool loan = clsArtikulli.ktheArtLoan(garanci.IdArtikulli);
                           // if (loan) 
                            if (regjistrime.ColTrupi.Count > 0 && regjistrime.ColTrupi[0].IdArtLoan > 0) idRaporti = 149; else idRaporti = 148;

                            Container.Attributes["src"] = "RaportiShpejte.aspx?Sesioni=false&idraporti=" + idRaporti + "&idDokumenti=" + regjistrime.IdKoka + "&printo=true";
                        }

                    //if (hfShtimModifikim.Value == "riparim")
                    //{
                    //    int id = int.Parse(Request.QueryString["id"]);
                    //    clsKokaRiparime kokaekzistuese = new clsKokaRiparime(id);
                    //  
                    //   if(status.Pershkrimi=="Kerkese e re")


                    //}

                }
                if (mesazh.Status == true)
                {

                    hfqkmesazhi.Value = shfaqmesazhapolupe;
                    if (shfaqmesazhapolupe != "jo")
                    {
                        clsKokaFleteKontabel kok = new clsKokaFleteKontabel(regjistrime.OKokaMagazina.IdKokaMagazina, 6);

                        hfUrl.Value = "LupaRegjistrimQendraKosto.aspx?shtim_modifikim=modifikim&idDokGjenerues=" + kok.IdKokaFleteKontabel + "&idkonfig=" + kok.IdKonfigAmbjente;
                    }

                    hfShtimModifikim.Value = "shtim";
                    percaktoTemplateMenu(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), idNdermarrje, ASPxMenu1);
                    if (rivleresim)
                        clsMenuInfo.ShtoPyetje(MenuInfo, rm.GetString("regjMagVeprimiSjellNdryshimNeCmimDalje", ci), pnlMesazhi, (int)hfState["idGjuha"]);
                    else
                    {
                        clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                    }

                    hl = new HtmlTable();
                    pnlLidhur.Update();
                    status1.Value = "true";
                }
                else
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi, LoadingPanel);
                    status1.Value = "false";
                }

            }
            else
            {
                status1.Value = "false";
            }
            pergjigja.ClientVisible = false;
        }

        /// <summary>
        /// Krijon nje objekt te tipit DbCore.DbRegjistrim.clsKokaMagazina
        /// </summary>
        /// <param name="statusDokumenti">Statusi i dokumentit (I rregullt ose draft)</param>
        /// <param name="klienti">Klienti</param>
        /// <returns>Kthen nje objekt te tipit DbCore.DbRegjistrim.clsKokaMagazina</returns>
        private clsKokaRiparime krijoRegjistrimRiparim(int statusDokumenti)
        {
            var controls = this.GetAsPxTextEditIdValue();
            controls.AddRange(ASPxSplitter1.GetAsPxTextEditIdValue());

            hfNrAuto = (ASPxHiddenField)NrAuto.VendosVleratNrAuto(hfNrAuto, controls);
            //DbCore.DbAdmin.NrAuto.vendosVleratNrAuto(hfNrAuto, this, null, ASPxSplitter1, null);
            //DbCore.DbAdmin.NrAuto.shtoNeHfRegjistrime(hfNrAutoShitje, hfNrAuto, "txtNrDok", "NrDok");
            //DbCore.DbAdmin.NrAuto.shtoNeHfRegjistrime(hfNrAutoShitje, hfNrAuto, "txtNrProjekti", "NrProjekt");
            clsKokaRiparime koka = new clsKokaRiparime();

            int idnivel = int.Parse(cmbLloji.Value.ToString());
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DbCore.DbShare.clsKonfigurimAmbjenti clsKonf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            clsKonf.mbushKonfigDefaultKomponentes(542, idNdermarrje);
            if (cmbKonfigurimi.Text != "") //cmbKonfigurimi.Value != null && 
            {
                clsKonf.mbushKonfigAmbjSipasKod(cmbKonfigurimi.Text, idNdermarrje);
            }

            koka = krijoRegjistrimRiparimi(statusDokumenti, clsKonf, idnivel, cmbLloji.Text, ruajTrupinERiparimit());

            return koka;
        }

        /// <summary>
        /// Krijon nje objekt te tipit DbCore.DbRegjistrim.clsKokaMagazina ne rastin kur kemi hyrje
        /// </summary>
        /// <param name="statusDokumenti">Statusi i dokumentit (I rregullt ose draft)</param>
        /// <param name="eshteTransferim">Tregon nese eshte transferim apo jo</param>
        /// <param name="klienti">Klienti</param>
        /// <returns>Kthen nje objekt te tipit DbCore.DbRegjistrim.clsKokaMagazina</returns>
        private clsKokaRiparime krijoRegjistrimRiparimi(int statusDokumenti, DbCore.DbShare.clsKonfigurimAmbjenti clsKonf, int idnivel, string kodniveli, colTrupiRiparime coltrupi)
        {
            clsKokaRiparime mag = new clsKokaRiparime();
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);

            int idnderviti = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
            int idmagazina = 0;

            string magazina = "";
            double cmimi = 0;
            if (btneMagazina.Text != "")
            {
                idmagazina = int.Parse(btneMagazina.Value.ToString());
                magazina = btneMagazina.Text;
            }
            int idstatusriparimi = 0;
            if (cmbStatus.Text != "")
                idstatusriparimi = int.Parse(cmbStatus.Value.ToString());
            clsGaranciArtikulli garanci = new clsGaranciArtikulli();
            if (hfShtimModifikim.Value == "shtim")
            {
                colGaranciArtikulli col = new colGaranciArtikulli();
                object o = new object();
                DbCore.mySessionObjects.merrGrideNgaSessioni(Session, out o);
                col = (colGaranciArtikulli)o;

                if (col.Count > 0)
                    garanci = col[0];
                else throw new Exception("Nuk keni asnje garanci!");
            }
            else
            {
                int id = int.Parse(Request.QueryString["id"]);
                clsKokaRiparime kokaekzistuese = new clsKokaRiparime(id);
                garanci = new clsGaranciArtikulli(kokaekzistuese.IdGaranci);

                DataTable dt = colTrupiRiparime.ktheTrupiRiparimeDT(id);
                if (dt.Rows.Count > 0)
                    double.TryParse(dt.Rows[0]["Cmimi"].ToString(), out cmimi);
                clsStatusRiparimi status = new clsStatusRiparimi(kokaekzistuese.IdStatusRiparimi);
                if (status.Pershkrimi == cmbStatus.Text)
                    throw new Exception("Dokumenti eshte tashme me kete status");
            }
            int idperdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);

            int iddifekti = 0;
            if (cmbDifekti.Text != "")
                iddifekti = int.Parse(cmbDifekti.Value.ToString());
            int dorezuar = 0;
            if (cmbDorezuar.Text != "")
                dorezuar = int.Parse(cmbDorezuar.Value.ToString());
            bool gjenerodokmag = clsAlternativaKushti.getAlternativa(clsKonf.IdKonfigAmbjente, "GJDM") == "Po";
            DbCore.DbShare.clsKusht kusht2 = new DbCore.DbShare.clsKusht(clsKonf.IdKonfigAmbjente, "ZKDM2");
            DbCore.DbShare.clsKonfigurimAmbjenti konfmag = new DbCore.DbShare.clsKonfigurimAmbjenti(clsKonf.IdKonfigurimi);
            DbCore.DbShare.clsKonfigurimAmbjenti konfmag2 = new DbCore.DbShare.clsKonfigurimAmbjenti(kusht2.Vlera);
            clsMesazh mesazh = mag.krijoRiparim(idnivel, clsKonf.IdKonfigAmbjente, garanci.IdGarancia, txtAksesor.Text, idmagazina, dteDtDok.Date, txtNrKontakti.Text, statusDokumenti, idNdermarrje, idnderviti, idperdoruesi, dteDtRegjistrimi.Date, txtPershkrimi.Text, idstatusriparimi, iddifekti, idperdoruesi, dorezuar, magazina, konfmag, gjenerodokmag, kodniveli, cmbStatus.Text, konfmag2, cmimi, coltrupi);
            if (!mesazh.Status)
                throw new Exception(mesazh.PershkrimMesazhi);
            return mag;
        }

        private colTrupiRiparime ruajTrupinERiparimit()
        {

            colTrupiRiparime trupat = new colTrupiRiparime();
            clsDetajimArtikulli det = new clsDetajimArtikulli();
            clsTrupiRiparime trupi = new clsTrupiRiparime();
            if (hfShtimModifikim.Value == "shtim")
            {
                if (cbLoan.Checked)
                {
                    det.mbushDetajimArtikulli(hfIMEI.Value.ToString(), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    DataTable dt = DbCore.mySessionObjects.merrDtNgaSessioni(Session);
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[int.Parse(hfrreshti.Value.ToString())];
                        int idart = int.Parse(dr["IdArtikulli"].ToString());
                        trupi = new clsTrupiRiparime(0, 0, idart, det.IdDetajimArtikulli, hfAksesor.Value.ToString(), 0, 0);
                    }
                    else return trupat;
                }
                else return trupat;
            }
            else
            {
                int id = int.Parse(Request.QueryString["id"]);
                colTrupiRiparime trupivjeter = new colTrupiRiparime(id, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));

                det.mbushDetajimArtikulli(txtIMEISwap.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                if (trupivjeter.Count > 0 && trupivjeter[0].IdDetSwap > 0 && det.KodDetajimArtikulli == null)
                {
                    return trupivjeter;
                }
                clsArtikulli art = new clsArtikulli();
                art.ktheArtikullSipasDetajimit(det.KodDetajimArtikulli, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                if (trupivjeter.Count > 0)
                    trupi = new clsTrupiRiparime(0, 0, trupivjeter[0].IdArtLoan, trupivjeter[0].IdDetLoan, trupivjeter[0].Aksesor, art.IdArtikulli, det.IdDetajimArtikulli);
                else if (art.IdArtikulli > 0) trupi = new clsTrupiRiparime(0, 0, 0, 0, "", art.IdArtikulli, det.IdDetajimArtikulli);
                else return trupat;

            }
            trupat.Add(trupi);
            return trupat;
        }

        /// <summary>
        /// Kontrollon nese jane plotesuar te gjitha fushat e detyrueshme te kokes se dokumentit
        /// </summary>
        private bool isValidRegjistrimRiparime(int draft, ResourceManager rm, CultureInfo ci)
        {
            bool isValid;
            isValid = true;
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idPerdorues = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            if (this.btneMagazina.Text == "")
            {
                isValid = false;
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Zgjidhni nje dyqan", pnlMesazhi, LoadingPanel);
                return isValid;
            }

            if (this.dteDtRegjistrimi.Text == "")
            {
                isValid = false;
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, STR_zgjidhniDtRegj, pnlMesazhi, LoadingPanel);

                return isValid;
            }
            if (this.dteDtDok.Text == "")
            {
                isValid = false; clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Zgjidhni një datë dokumenti!", pnlMesazhi, LoadingPanel);

                return isValid;
            }
            if (dteDtDok.Date.Year != new DbCore.DbAdmin.clsNdermarrjeViti(DbCore.mySessionObjects.ktheNdermarrjeVit(Session)).Viti)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Data nuk i përket vitit ushtrimor të zgjedhur", pnlMesazhi);
                return false;
            }
            DbCore.DbAdmin.clsPeriudhaKontabel periudha = DbCore.mySessionObjects.merrPeriudheKontabel(Session);
            String mesazhGabimi;
            if (!DbCore.clsFunksione.checkPeriudheKontabel(out mesazhGabimi, dteDtDok.Date, periudha, draft))
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhGabimi, pnlMesazhi);
                return false;
            }

            if (btneMagazina.Text != "")
            {
                DbCore.DbRegjistrim.clsNjesiAdministrative mag = new clsNjesiAdministrative(btneMagazina.Text, idNdermarrje, idPerdorues);
                if (mag.IdNjesiAdministrative == -1)
                {
                    isValid = false; clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ky dyqanin nuk ekziston!", pnlMesazhi, LoadingPanel);

                    return isValid;
                }
                else
                {
                    mag = new clsNjesiAdministrative(btneMagazina.Text, idNdermarrje, idPerdorues);
                    if (mag.Aktiv == false)
                    {
                        isValid = false; clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ky dyqanin nuk është aktiv!", pnlMesazhi, LoadingPanel);

                        return isValid;
                    }
                }
            }
            if (cmbStatus.Value != null)
            {
                int idstatus = int.Parse(cmbStatus.Value.ToString());
                DbCore.DbInventari.clsStatusRiparimi status = new clsStatusRiparimi(idstatus, idPerdorues);
                if (status.Id < 1)
                {
                    isValid = false; clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ju nuk keni te drejta per kete status!", pnlMesazhi, LoadingPanel);

                    return isValid;
                }


            }
            if (hfShtimModifikim.Value == "shtim")
            {
                colGaranciArtikulli col = new colGaranciArtikulli();
                object o = new object();
                DbCore.mySessionObjects.merrGrideNgaSessioni(Session, out o);
                col = (colGaranciArtikulli)o;
                if (col.Count == 0)
                {
                    isValid = false; clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Nuk keni zgjedhur asnje artikull me garanci!", pnlMesazhi, LoadingPanel);

                    return isValid;
                }
            }
            return isValid;
        }



        #region  GRIDA E garancise

        private void inicializoGridFaturat(int idNdermarrje)
        {
            ASPxGridView grid_faturat = (ASPxGridView)this.ASPxNavBar1.Groups[0].FindControl("grid_faturat");
            colGaranciArtikulli col = new colGaranciArtikulli();
            int idNdermarrjeVit = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
            DbCore.mySessionObjects.ruajGrideNeSession(string.Empty, Session, col);
            grid_faturat.DataSource = col;
            grid_faturat.DataBind();

        }

        private void konfiguroGrideFaturat(int idPerdoruesi, bool visibleindex)
        {
            shtokolona();
            ASPxGridView grid_faturat = (ASPxGridView)this.ASPxNavBar1.Groups[0].FindControl("grid_faturat");

            if (visibleindex)
                GridUtil.percaktoVisibleColumnsMeWidth(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), grid_faturat, "grid_faturat", "Shto_RegjistrimRiparimi.aspx");
            else GridUtil.percaktoVisibleColumnsMeWidthPaVisibleIndex(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), grid_faturat, "grid_faturat", "Shto_RegjistrimRiparimi.aspx");

            GridViewDataTextColumn col4 = grid_faturat.Columns["Kohezgjatje"] as GridViewDataTextColumn;
            col4.PropertiesEdit.DisplayFormatString = "0.#";
            grid_faturat.Columns["#"].VisibleIndex = 0;
        }

        private void shtokolona()
        {
            ASPxGridView grid_faturat = (ASPxGridView)this.ASPxNavBar1.Groups[0].FindControl("grid_faturat");
            GridViewDataTextColumn colnew1;
            GridViewDataDateColumn colnew2;
            if (grid_faturat.Columns["IdGarancia"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "IdGarancia"; colnew1.VisibleIndex = 0;
                grid_faturat.Columns.Add(colnew1);
            }
            if (grid_faturat.Columns["IdNiveli"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "IdNiveli"; colnew1.VisibleIndex = 1;
                grid_faturat.Columns.Add(colnew1);
            }
            if (grid_faturat.Columns["Niveli"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "Niveli"; colnew1.VisibleIndex = 2;
                grid_faturat.Columns.Add(colnew1);
            }
            if (grid_faturat.Columns["DtDok"] == null)
            {
                colnew2 = new GridViewDataDateColumn();
                //  colnew2.DataItemTemplate = new MyDateGTemplate("");
                colnew2.FieldName = "DtDok"; colnew2.VisibleIndex = 3;
                grid_faturat.Columns.Add(colnew2);

            }
            if (grid_faturat.Columns["IdKrijuesi"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "IdKrijuesi"; colnew1.VisibleIndex = 4;
                grid_faturat.Columns.Add(colnew1);
            }
            if (grid_faturat.Columns["Shites"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "Shites"; colnew1.VisibleIndex = 5;
                grid_faturat.Columns.Add(colnew1);
            }
            if (grid_faturat.Columns["IdArtikulli"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "IdArtikulli"; colnew1.VisibleIndex = 6;
                grid_faturat.Columns.Add(colnew1);
            } if (grid_faturat.Columns["Produkti"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "Produkti"; colnew1.VisibleIndex = 7;
                grid_faturat.Columns.Add(colnew1);
            }

            if (grid_faturat.Columns["IdMagazina"] == null)
            {
                colnew1 = new GridViewDataTextColumn(); colnew1.VisibleIndex = 8;
                colnew1.FieldName = "IdMagazina";
                grid_faturat.Columns.Add(colnew1);
            } if (grid_faturat.Columns["Dyqani"] == null)
            {
                colnew1 = new GridViewDataTextColumn(); colnew1.VisibleIndex = 9;
                colnew1.FieldName = "Dyqani";
                grid_faturat.Columns.Add(colnew1);
            }
            if (grid_faturat.Columns["IdNdermarje"] == null)
            {
                colnew1 = new GridViewDataTextColumn();

                colnew1.FieldName = "IdNdermarje"; colnew1.VisibleIndex = 10;
                grid_faturat.Columns.Add(colnew1);
            } if (grid_faturat.Columns["Kompania"] == null)
            {
                colnew1 = new GridViewDataTextColumn();

                colnew1.FieldName = "Kompania"; colnew1.VisibleIndex = 11;
                grid_faturat.Columns.Add(colnew1);
            }
            if (grid_faturat.Columns["IdDetajimi"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "IdDetajimi"; colnew1.VisibleIndex = 12;
                grid_faturat.Columns.Add(colnew1);
            } if (grid_faturat.Columns["IMEI"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "IMEI"; colnew1.VisibleIndex = 13;
                grid_faturat.Columns.Add(colnew1);
            }
            if (grid_faturat.Columns["Kodi"] == null)
            {
                colnew1 = new GridViewDataTextColumn();

                colnew1.FieldName = "Kodi"; colnew1.VisibleIndex = 14;
                grid_faturat.Columns.Add(colnew1);
            }
            if (grid_faturat.Columns["IdKokaShitje"] == null)
            {
                colnew1 = new GridViewDataTextColumn(); colnew1.VisibleIndex = 15;
                colnew1.FieldName = "IdKokaShitje";
                grid_faturat.Columns.Add(colnew1);
            }

            if (grid_faturat.Columns["EmerKlienti"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "EmerKlienti"; colnew1.VisibleIndex = 16;
                grid_faturat.Columns.Add(colnew1);
            }
            if (grid_faturat.Columns["Kontakti"] == null)
            {
                colnew1 = new GridViewDataTextColumn();

                colnew1.FieldName = "Kontakti"; colnew1.VisibleIndex = 17;
                grid_faturat.Columns.Add(colnew1);
            }
            if (grid_faturat.Columns["DtMbarimi"] == null)
            {
                colnew2 = new GridViewDataDateColumn();
                //  colnew2.DataItemTemplate = new MyDateGTemplate("");
                colnew2.FieldName = "DtMbarimi"; colnew2.VisibleIndex = 20;
                grid_faturat.Columns.Add(colnew2);

            } if (grid_faturat.Columns["Kohezgjatje"] == null)
            {
                colnew1 = new GridViewDataTextColumn();

                colnew1.FieldName = "Kohezgjatje"; colnew1.VisibleIndex = 18;
                colnew1.PropertiesEdit.DisplayFormatString = "0.#";
                grid_faturat.Columns.Add(colnew1);
            }
            if (grid_faturat.Columns["LlojGaranci"] == null)
            {
                colnew1 = new GridViewDataTextColumn();

                colnew1.FieldName = "LlojGaranci"; colnew1.VisibleIndex = 19;
                grid_faturat.Columns.Add(colnew1);
            }
            if (grid_faturat.Columns["Loan"] == null)
            {
                colnew1 = new GridViewDataTextColumn();

                colnew1.FieldName = "Loan"; colnew1.VisibleIndex = 20;
                grid_faturat.Columns.Add(colnew1);
            }
        }

        /// <summary>
        /// I vendos nje emer identifikues kontrolleve te grides se dok kryesore dhe percakton funksinet qe manipulojne ato ne client side
        /// </summary>
        protected void grid_faturat_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {

        }

        protected void grid_faturat_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {

        }


        protected void grid_faturat_DataBound(object sender, EventArgs e)
        {
            ASPxGridView grid_faturat = (ASPxGridView)this.ASPxNavBar1.Groups[0].FindControl("grid_faturat");
            if (grid_faturat.Columns["#"] == null)
            {
                DevExpress.Web.GridViewCommandColumn check = new DevExpress.Web.GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true; check.Width = Unit.Percentage(2);
                grid_faturat.Settings.ShowFilterRow = true;
                grid_faturat.Settings.ShowHeaderFilterButton = true;
                grid_faturat.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                grid_faturat.Settings.ShowFilterRowMenu = true;
                grid_faturat.Columns.Add(check);
                grid_faturat.Settings.ShowGroupPanel = false;
                grid_faturat.KeyFieldName = "IdGarancia";
                grid_faturat.SettingsBehavior.AllowSelectByRowClick = true;
                grid_faturat.SettingsBehavior.AllowFocusedRow = true;
                grid_faturat.Settings.ShowTitlePanel = false;
                grid_faturat.SettingsText.Title = "Artikulli per garanci";
            }
        }

        protected void grid_faturat_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {

            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idNdermarrjeVit = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);

            colGaranciArtikulli col = new colGaranciArtikulli();
            if (e.Parameters == "pastro")
            {
                col = new colGaranciArtikulli();

            }
            else
            {
                clsGaranciArtikulli garanci = new clsGaranciArtikulli(txtGaranci.Text, txtIMEI.Text);
                if (garanci.IdGarancia > 0)
                    col.Add(garanci);

            }
            DbCore.mySessionObjects.ruajGrideNeSession(string.Empty, Session, col);
            ASPxGridView grid_faturat = (ASPxGridView)this.ASPxNavBar1.Groups[0].FindControl("grid_faturat");
            grid_faturat.KeyFieldName = "IdGarancia";
            grid_faturat.DataSource = col;
            grid_faturat.DataBind();
            int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            konfiguroGrideFaturat(idPerdoruesi, false);
            merrArtikujLoanDB(col);

        }

        protected void grid_faturat_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            ASPxGridView grid_faturat = (ASPxGridView)this.ASPxNavBar1.Groups[0].FindControl("grid_faturat");
            e.Properties["cpNoRows"] = grid_faturat.VisibleRowCount;
        }

        protected void grid_faturat_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            colGaranciArtikulli col = new colGaranciArtikulli();
            object o = new object();
            DbCore.mySessionObjects.merrGrideNgaSessioni(Session, out o);
            col = (colGaranciArtikulli)o;
            ASPxGridView grid_faturat = (ASPxGridView)this.ASPxNavBar1.Groups[0].FindControl("grid_faturat");
            grid_faturat.KeyFieldName = "IdGarancia";
            grid_faturat.DataSource = col;
            grid_faturat.DataBind();
            int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            konfiguroGrideFaturat(idPerdoruesi, false);

        }



        #endregion
        #region  GRIDA E statusit aktual

        private void inicializoGridAktuale()
        {

            int id = int.Parse(Request.QueryString["id"]);
            DataRow dr = clsKokaRiparime.mbushKokaRiparimSipasIDDR(id);
            DataTable dt = dr.Table;

            grid_aktuale.DataSource = dt;
            grid_aktuale.DataBind();

        }

        private void konfiguroGrideAktuale(int idPerdoruesi, bool visibleindex)
        {

            if (visibleindex)
                GridUtil.percaktoVisibleColumnsMeWidth(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), grid_aktuale, "grid_aktuale", "Shto_RegjistrimRiparimi.aspx");
            else GridUtil.percaktoVisibleColumnsMeWidthPaVisibleIndex(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), grid_aktuale, "grid_aktuale", "Shto_RegjistrimRiparimi.aspx");
            grid_aktuale.SettingsBehavior.AllowDragDrop = false;
            grid_aktuale.SettingsBehavior.AllowSort = false;
            grid_aktuale.SettingsBehavior.AllowGroup = false;
        }


        protected void grid_aktuale_DataBound(object sender, EventArgs e)
        {
            grid_aktuale.KeyFieldName = "IdKoka";
            grid_aktuale.Settings.ShowTitlePanel = false;
            grid_aktuale.SettingsText.Title = "Statusi aktual";

        }



        protected void grid_aktuale_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {

            e.Properties["cpNoRows"] = grid_aktuale.VisibleRowCount;
        }

        #endregion
        #region  GRIDA E historikut

        private void inicializoGridHistoriku()
        {

            int id = int.Parse(Request.QueryString["id"]);
            DataTable dt = colKokaRiparime.ktheHistorikuDT(id);


            grid_historiku.DataSource = dt;
            grid_historiku.DataBind();

        }

        private void konfiguroGrideHistoriku(int idPerdoruesi, bool visibleindex)
        {

            if (visibleindex)
                GridUtil.percaktoVisibleColumnsMeWidth(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), grid_historiku, "grid_historiku", "Shto_RegjistrimRiparimi.aspx");
            else GridUtil.percaktoVisibleColumnsMeWidthPaVisibleIndex(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), grid_historiku, "grid_historiku", "Shto_RegjistrimRiparimi.aspx");
            grid_historiku.SettingsBehavior.AllowDragDrop = false;
            grid_historiku.SettingsBehavior.AllowSort = false;
            grid_historiku.SettingsBehavior.AllowGroup = false;
        }


        protected void grid_historiku_DataBound(object sender, EventArgs e)
        {
            grid_historiku.KeyFieldName = "IdKoka";
            grid_historiku.Settings.ShowTitlePanel = false;
            grid_historiku.SettingsText.Title = "Historiku";

        }



        protected void grid_historiku_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {

            e.Properties["cpNoRows"] = grid_historiku.VisibleRowCount;
        }

        #endregion
        #region  GRIDA loan

        private void inicializoGridLoan()
        {

            int id = int.Parse(Request.QueryString["id"]);

            DataTable dt = colTrupiRiparime.ktheTrupiRiparimeDT(id);

            grid_Loan.DataSource = dt;
            grid_Loan.DataBind();

        }

        private void konfiguroGrideLoan(int idPerdoruesi, bool visibleindex)
        {
            percaktoTemplateLoan();
            if (visibleindex)
                GridUtil.percaktoVisibleColumnsMeWidth(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), grid_Loan, "grid_Loan", "Shto_RegjistrimRiparimi.aspx");
            else GridUtil.percaktoVisibleColumnsMeWidthPaVisibleIndex(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), grid_Loan, "grid_Loan", "Shto_RegjistrimRiparimi.aspx");
            grid_Loan.SettingsBehavior.AllowDragDrop = false;
            grid_Loan.SettingsBehavior.AllowSort = false;
            grid_Loan.SettingsBehavior.AllowGroup = false;
        }
        protected void grid_Loan_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType == DevExpress.Web.GridViewRowType.Data)
            {
                GridViewDataTextColumn col2 = ((ASPxGridView)sender).Columns["Cmimi"] as GridViewDataTextColumn;
                ASPxLabel lbl = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col2, "lbl") as ASPxLabel;
                //vendosen client side eventet e kolonave

                if (lbl != null)
                {
                    int idndermarje = (int)hfState.Get("idNdermarje");
                    int idperdoruesi = (int)hfState.Get("idperdoruesi");
                    int idnivel = (int)hfState.Get("nivelibaze");

                    clsArtikulli art = new clsArtikulli(int.Parse(e.KeyValue.ToString()));//llogarit cmimin sipas nenniveleve te nivelit baze

                    lbl.Text = clsFunksione.merrCmimSipasNivelit(idnivel, art.KodArtikulli, idperdoruesi, art.KodNjesia1, "LEK", dteDtDok.Date.ToShortDateString(), 1, 1, idndermarje, 0, 0)[0].ToString();
                }


            }
        }
        private void percaktoTemplateLoan()
        {//percaktohen templatet per fushat e grides


            GridViewDataTextColumn col3 = grid_Loan.Columns["Cmimi"] as GridViewDataTextColumn;
            col3.DataItemTemplate = new MyLabelTemplate();

        }

        protected void grid_Loan_DataBound(object sender, EventArgs e)
        {
            grid_Loan.KeyFieldName = "IdArtikulli";
            grid_Loan.Settings.ShowTitlePanel = false;
            grid_Loan.SettingsText.Title = "Loan";

        }



        protected void grid_Loan_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {

            e.Properties["cpNoRows"] = grid_Loan.VisibleRowCount;
        }

        #endregion
        #region grida e art loan
        private void merrArtikujLoanDB(colGaranciArtikulli col)
        {
            DataTable dt = new DataTable();
            int idartikulli = 0;
            if (col.Count > 0)
            {
                idartikulli = col[0].IdArtikulli;
            }
            dt = colArtikujt.ktheArtikujLoanDT(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), btneMagazina.Text == "" ? 0 : int.Parse(btneMagazina.Value.ToString()), idartikulli, dteDtDok.Date);

            DbCore.mySessionObjects.ruajdtNeSession(Session, dt);
            gvArtLoan.DataSource = dt;
            gvArtLoan.KeyFieldName = "IdArtikulli";
            gvArtLoan.DataBind();

        }
        private void percaktoTemplate()
        {//percaktohen templatet per fushat e grides

            GridViewDataTextColumn col1 = gvArtLoan.Columns["IMEI"] as GridViewDataTextColumn;
            col1.DataItemTemplate = new MyTextTemplate();
            GridViewDataTextColumn col2 = gvArtLoan.Columns["Aksesor"] as GridViewDataTextColumn;
            col2.DataItemTemplate = new MyTextTemplate();
            GridViewDataTextColumn col3 = gvArtLoan.Columns["Cmimi"] as GridViewDataTextColumn;
            col3.DataItemTemplate = new MyLabelTemplate();
        }
        private void merrArtikujLoanSession()
        {
            DataTable dt = DbCore.mySessionObjects.merrDtNgaSessioni(Session);
            gvArtLoan.DataSource = dt;
            gvArtLoan.KeyFieldName = "IdArtikulli";
            gvArtLoan.DataBind();
        }

        protected void gvArtLoan_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            e.Values.Clear();
            e.AddValue("(Te gjithe)", string.Empty, "true");
        }

        protected void gvArtLoan_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
        }



        protected void gvArtLoan_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
        }

        protected void gvArtLoan_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            merrArtikujLoanSession();
            konfiguroGride();
        }

        protected void gvArtLoan_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
        }

        protected void gvArtLoan_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters == "pastro")
            {

                DataTable dt = colArtikujt.ktheArtikujLoanDT(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), 0, 0, dteDtDok.Date);
                DbCore.mySessionObjects.ruajdtNeSession(Session, dt);
                gvArtLoan.DataSource = dt;
                gvArtLoan.KeyFieldName = "IdArtikulli";
                gvArtLoan.DataBind();
            }
            else if (e.Parameters == "merr")
            {
                colGaranciArtikulli col = new colGaranciArtikulli();
                object o = new object();
                DbCore.mySessionObjects.merrGrideNgaSessioni(Session, out o);
                col = (colGaranciArtikulli)o;
                merrArtikujLoanDB(col);
            }
            merrArtikujLoanSession();
            konfiguroGride();
        }
        private void konfiguroGride()
        {
            GridUtil.percaktoVisibleColumnsMeWidth(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), gvArtLoan, "gvArtLoan", "Shto_RegjistrimRiparimi.aspx");
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            GridUtil.konfiguroGrideListeEvogelPaTheme(gvArtLoan, "IdArtikulli");
            percaktoTemplate();
            gvArtLoan.Settings.ShowFilterRow = false;
            gvArtLoan.Settings.ShowFilterRow = false;
            gvArtLoan.Settings.ShowHeaderFilterButton = false;
            gvArtLoan.Settings.ShowFilterBar = GridViewStatusBarMode.Hidden;
            gvArtLoan.Settings.ShowFilterRowMenu = false;
            //  gvArtLoan.Columns.Add(check);
            gvArtLoan.Settings.ShowGroupPanel = false;
            gvArtLoan.SettingsBehavior.AllowDragDrop = false;
            gvArtLoan.SettingsBehavior.AllowSort = false;
            gvArtLoan.SettingsBehavior.AllowGroup = false;
            gvArtLoan.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
            //gvArtLoan.Columns["#"].VisibleIndex = 0;
            //gvArtLoan.SettingsEditing.EditFormColumnCount = 2;

            //((GridViewDataColumn)gvArtLoan.Columns["Produkti"]).EditFormSettings.Visible = DevExpress.Utils.DefaultBoolean.False;
            //((GridViewDataColumn)gvArtLoan.Columns["Barkodi"]).EditFormSettings.Visible = DevExpress.Utils.DefaultBoolean.False;
            //((GridViewDataColumn)gvArtLoan.Columns["Cmimi"]).EditFormSettings.Visible = DevExpress.Utils.DefaultBoolean.False;
            //((GridViewDataColumn)gvArtLoan.Columns["Gjendje"]).EditFormSettings.Visible = DevExpress.Utils.DefaultBoolean.False;

        }
        protected void gvArtLoan_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gvArtLoan.PageIndex;
            e.Properties["cpPageRow"] = gvArtLoan.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gvArtLoan.VisibleRowCount;
        }

        protected void gvArtLoan_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType == DevExpress.Web.GridViewRowType.Data)
            {
                GridViewDataTextColumn col2 = ((ASPxGridView)sender).Columns["Cmimi"] as GridViewDataTextColumn;
                GridViewDataTextColumn col0 = ((ASPxGridView)sender).Columns["IMEI"] as GridViewDataTextColumn;
                GridViewDataTextColumn col1 = ((ASPxGridView)sender).Columns["Aksesor"] as GridViewDataTextColumn;
                ASPxTextBox txt1 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col0, "txtBox") as ASPxTextBox;
                ASPxTextBox txt2 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col1, "txtBox") as ASPxTextBox;
                ASPxLabel lbl = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col2, "lbl") as ASPxLabel;

                //vendosen client side eventet e kolonave
                if (txt1 != null)
                {
                    txt1.ClientInstanceName = String.Format("txtIMEI{0}", e.VisibleIndex);
                    txt1.ClientSideEvents.TextChanged = String.Format("function(s,e){{TextChangedImei({0});}}", e.VisibleIndex);
                }

                if (txt2 != null)
                {
                    txt2.ClientInstanceName = String.Format("txtAksesor{0}", e.VisibleIndex);
                }

                if (lbl != null)
                {

                    clsArtikulli art = new clsArtikulli(int.Parse(e.KeyValue.ToString()));//llogarit cmimin sipas nenniveleve te nivelit baze
                    int idndermarje = (int)hfState.Get("idNdermarje");
                    int idperdoruesi = (int)hfState.Get("idperdoruesi");
                    int idnivel = (int)hfState.Get("nivelibaze");
                    lbl.Text = clsFunksione.merrCmimSipasNivelit(idnivel, art.KodArtikulli, idperdoruesi, art.KodNjesia1, "LEK", dteDtDok.Date.ToShortDateString(), 1, 1, idndermarje, 0, 0)[0].ToString();
                }
            }


        }
        protected void gvArtLoan_DataBound(object sender, EventArgs e)
        {
            //if (gvArtLoan.Columns["#"] == null)
            {
                //DevExpress.Web.GridViewCommandColumn check = new DevExpress.Web.GridViewCommandColumn("#");
                //check.ShowSelectCheckbox = true; check.Width = Unit.Percentage(2);
                gvArtLoan.Settings.ShowFilterRow = false;
                gvArtLoan.Settings.ShowHeaderFilterButton = false;
                gvArtLoan.Settings.ShowFilterBar = GridViewStatusBarMode.Hidden;
                gvArtLoan.Settings.ShowFilterRowMenu = false;
                //  gvArtLoan.Columns.Add(check);
                gvArtLoan.Settings.ShowGroupPanel = false;
                gvArtLoan.KeyFieldName = "IdArtikulli";
                gvArtLoan.SettingsBehavior.AllowSelectSingleRowOnly = true;
                gvArtLoan.SettingsBehavior.AllowFocusedRow = true;
                //  gvArtLoan.Settings.ShowTitlePanel = false;
            }
        }
        #endregion


    }
}