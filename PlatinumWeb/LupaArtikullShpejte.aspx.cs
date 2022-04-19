using DbCore;
using DbCore.DbAsete;
using DbCore.DbInventari;
using DbCore.DbRegjistrim;
using DbCore.DbShare;
using DevExpress.Web;
using PlatinumWeb.Templates;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Resources;
using System.Web.Script.Serialization;
using System.Web.UI;
using Newtonsoft.Json;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.DbAdmin;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using System.Linq;
using DbCore.IMBUtils.Extensions;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{
    public partial class LupaArtikullShpejte : MyPageBase
    {

        //private string koloneFocus;
        ASPxTextBox temptxt = null;
        ASPxComboBox tempcombo = null;
        private DbCore.DbShare.clsFormatiKonfig formatNrPerKonfigCmimi = new DbCore.DbShare.clsFormatiKonfig();
        private string komponente = "LupaArtikullShpejte.aspx";
        private string guidString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                DbCore.clsFunksione.logout(Session, true, "FaqePaautorizuar");
            }
            int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerdoruesi);
            }
            int idviti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            hfState.Set("idPerdoruesi", idPerdoruesi);
            hfState.Set("idGjuha", idGjuha);
            hfState.Set("idNdermarrje", idNdermarrje);
            guidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            hfState.Set("guidString", guidString);
            System.Globalization.CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (!Page.IsPostBack)
            {
                hfState.Set("kodArt", String.Empty);
                hfShtimModifikim.Value = Request.QueryString["veprimi"];
                hfId.Value = String.IsNullOrEmpty(Request.QueryString["idartikulli"]) ? "" : Request.QueryString["idartikulli"];
                mbushHiddenFieldMePerkthime(cultinf, rm);
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idviti, "CmimeArtikulli.aspx?lloji=shitje");
                hfTeDrejtaCmimi.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejtaCmimi.Add("Modifikim", tedrejtaInfo.DMod);
                hfTeDrejtaCmimi.Add("Amb", tedrejtaInfo.DAmb);
                percaktoFormatNumriCmimet(idNdermarrje);
                hfState.Set("colAutorizime", Newtonsoft.Json.JsonConvert.SerializeObject(new colAutorizimetKoka(IdPerdoruesi)));
                konfiguroVleraFillestare(idGjuha, idPerdoruesi, idNdermarrje, rm, cultinf);
                konfiguroGrideCmimesh(idPerdoruesi, idNdermarrje);
                if (!Page.IsCallback)
                    konfiguroGrideArtikujPerberes();
                merrColKushtetFormula(idGjuha, idNdermarrje);
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idviti, komponente);
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                konfiguroVleraFillestareNorma(idNdermarrje);
                konfiguroGrideNorma(idNdermarrje, idGjuha);
            }
            else
            {
                guidString = (string)hfState["guidString"];
                konfiguroGrideCmimesh(idPerdoruesi, idNdermarrje);
                percaktoFormatNumriCmimet(idNdermarrje);
            }
            percaktoTemplateMenu(idviti, idPerdoruesi, idNdermarrje, ASPxMenu1, rm, cultinf);
            percaktoTemplateCmimi();

        }

        private void mbushHiddenFieldMePerkthime(CultureInfo cultinf, ResourceManager rm)
        {
            konfigurimi_Label.Text = rm.GetString("lblModeli", cultinf);
            popupUniversal.HeaderText = rm.GetString("headerPopUpText", cultinf);
            hfState.Set("msgEkzistonKodiGride", rm.GetString("msgEkzistonKodiGride", cultinf));
            hfState.Set("msgEkzistonArtikullGride", rm.GetString("msgEkzistonArtikullGride", cultinf));
            hfState.Set("msgNukLejohetTeVendoseeVetArtikulli", rm.GetString("msgNukLejohetTeVendoseeVetArtikulli", cultinf));
            hfState.Set("msgFiroVlera", rm.GetString("msgFiroVlera", cultinf));
            hfState.Set("msgFiroNumer", rm.GetString("msgFiroNumer", cultinf));
            hfState.Set("msgKoeficJoZero", rm.GetString("msgKoeficJoZero", cultinf));
            hfState.Set("msgKoeficDuhetNr", rm.GetString("msgKoeficDuhetNr", cultinf));
            hfState.Set("msgLlojiVeprimitIPanjohur", rm.GetString("msgLlojiVeprimitIPanjohur", cultinf));
            hfState.Set("msgZgjidhAktivitetin", rm.GetString("msgZgjidhAktivitetin", cultinf));
            hfState.Set("headerPopUpZgjidhArtikullin", rm.GetString("headerPopUpZgjidhArtikullin", cultinf));
            hfState.Set("msgZgjidhniLlojinEVeprimit", rm.GetString("msgZgjidhniLlojinEVeprimit", cultinf));
            hfState.Set("msgNdodhiGabimGjateMarrjesSeTeDhenave", rm.GetString("msgNdodhiGabimGjateMarrjesSeTeDhenave", cultinf));
            hfState.Set("msgArtikulliEshteInaktiv", rm.GetString("msgArtikulliEshteInaktiv", cultinf));
            hfState.Set("MsgBlerjeShitjeAutorizime", rm.GetString("MsgBlerjeShitjeAutorizime", cultinf));
            hfState.Set("msgZgjidhFormulen", rm.GetString("msgZgjidhFormulen", cultinf));
            hfState.Set("popupAdministrimiUniversal", rm.GetString("popupAdministrimiUniversal", cultinf));
            hfState.Set("msgZgjidhMagazinen", rm.GetString("msgZgjidhMagazinen", cultinf));
            hfState.Set("msgGabimLlogCmim", rm.GetString("msgGabimLlogCmim", cultinf));
            hfState.Set("msgCmimeArtikulliCmimet2DuhenNumerike", rm.GetString("msgCmimeArtikulliCmimet2DuhenNumerike", cultinf));
            hfState.Set("msgCmimeArtikulliCmimetDuhenNumerike", rm.GetString("msgCmimeArtikulliCmimetDuhenNumerike", cultinf));
            hfState.Set("msgCmimeArtikulliSasiteMaxDuhenNumerike", rm.GetString("msgCmimeArtikulliSasiteMaxDuhenNumerike", cultinf));
            hfState.Set("msgCmimeArtikulliSasiteMinimaleDuhenNumerike", rm.GetString("msgCmimeArtikulliSasiteMinimaleDuhenNumerike", cultinf));
            hfState.Set("msgClsArtikulliKjoSkemeNukIPerketKesajKlase", rm.GetString("msgClsArtikulliKjoSkemeNukIPerketKesajKlase", cultinf));
            hfState.Set("headerPopUpZgjidhKodifikiminArtikullit", rm.GetString("headerPopUpZgjidhKodifikiminArtikullit", cultinf));
            hfState.Set("regjisDokZgjidhDokPerTeBashkengjitur", rm.GetString("regjisDokZgjidhDokPerTeBashkengjitur", cultinf));
            hfState.Set("msgPoTransferohetTeDhenatShtypniPerseriRuaj", rm.GetString("msgPoTransferohetTeDhenatShtypniPerseriRuaj", cultinf));
            hfState.Set("headerZgjidhKlientFurnitorin", rm.GetString("headerZgjidhKlientFurnitorin", cultinf));
            hfState.Set("msgSerialetLupa", rm.GetString("msgSerialetLupa", cultinf));
            hfState.Set("msgSasiMaxBosh", rm.GetString("msgSasiMaxBosh", cultinf));
            hfState.Set("msgSasiteMinBosh", rm.GetString("msgSasiteMinBosh", cultinf));
            hfState.Set("msgKoeficJoZeroOseBosh", rm.GetString("msgKoeficJoZeroOseBosh", cultinf));
            hfState.Set("msgKoeficDuhetNje", rm.GetString("msgKoeficDuhetNje", cultinf));
            hfState.Set("MsgNdaluesPlotesoKodinEBarit", rm.GetString("MsgNdaluesPlotesoKodinEBarit", cultinf));
            hfState.Set("headerZgjidhSkemenkont", MessagesResource.Messages["headerZgjidhSkemenkont"]);
            hfState.Set("msgZgjidhKategorineDetajimNje", MessagesResource.Messages["msgZgjidhKategorineDetajimNje"]);
            hfState.Set("msgZgjidhKategorineDetajimDy", MessagesResource.Messages["msgZgjidhKategorineDetajimDy"]);
            hfState.Set("msgZgjidhDetajimArtikulli", MessagesResource.Messages["msgZgjidhDetajimArtikulli"]);
            hfState.Set("msgNukNdryshonDotKategoriDetajimi", MessagesResource.Messages["msgNukNdryshonDotKategoriDetajimi"]);
            hfState.Set("msgNukHiqniDotDetajim", MessagesResource.Messages["msgNukHiqniDotDetajim"]);
            hfState.Set("headerPopUpKodBar", MessagesResource.Messages["headerPopUpKodBar"]);
            hfState.Set("msgZevendesimPlusi", MessagesResource.Messages["msgZevendesimPlusi"]);
        }

        protected void percaktoFormatNumriCmimet(int idNdermarrje)
        {
            if (bool.Parse(hfTeDrejtaCmimi["Amb"].ToString()))
            {
                DbCore.DbShare.clsKonfigurimAmbjenti konfig = new DbCore.DbShare.clsKonfigurimAmbjenti();
                konfig.mbushKonfigAmbjSipasKod("CSH", idNdermarrje);
                formatNrPerKonfigCmimi.mbushFormatNrKonfigSipasIdKonfigAmbjente(konfig.IdKonfigAmbjente);
                if (formatNrPerKonfigCmimi.KonfigTrupi.Count == 0)
                {
                    DbCore.DbShare.clsFormatKonfigTrup trup = new DbCore.DbShare.clsFormatKonfigTrup(DbCore.DbShare.clsFormatKonfigTrup.defaultFormatSasia, DbCore.DbShare.clsFormatKonfigTrup.defaultFormatCmimi, DbCore.DbShare.clsFormatKonfigTrup.defaultFormatVlefta, DbCore.DbShare.clsFormatKonfigTrup.defaultFormatZbritja, DbCore.DbShare.clsFormatKonfigTrup.defaultFormatStringSasia, DbCore.DbShare.clsFormatKonfigTrup.defaultFormatStringCmimi, DbCore.DbShare.clsFormatKonfigTrup.defaultFormatStringVlefta, DbCore.DbShare.clsFormatKonfigTrup.defaultFormatStringZbritja);
                    formatNrPerKonfigCmimi.KonfigTrupi.Add(trup);
                }
            }
            else
            {
                gvCmimet.ClientVisible = false;
                DbCore.DbShare.clsFormatKonfigTrup trup = new DbCore.DbShare.clsFormatKonfigTrup(DbCore.DbShare.clsFormatKonfigTrup.defaultFormatSasia, DbCore.DbShare.clsFormatKonfigTrup.defaultFormatCmimi, DbCore.DbShare.clsFormatKonfigTrup.defaultFormatVlefta, DbCore.DbShare.clsFormatKonfigTrup.defaultFormatZbritja, DbCore.DbShare.clsFormatKonfigTrup.defaultFormatStringSasia, DbCore.DbShare.clsFormatKonfigTrup.defaultFormatStringCmimi, DbCore.DbShare.clsFormatKonfigTrup.defaultFormatStringVlefta, DbCore.DbShare.clsFormatKonfigTrup.defaultFormatStringZbritja);
                formatNrPerKonfigCmimi.KonfigTrupi.Add(trup);
            }
            System.Web.Script.Serialization.JavaScriptSerializer serializusi = new System.Web.Script.Serialization.JavaScriptSerializer();
            hfState.Set("formatMonedheCmimi", serializusi.Serialize(formatNrPerKonfigCmimi));
        }

        private void merrColKushtetFormula(int idGjuha, int idNdermarrje)
        {
            int idKonfigurim = -1;
            if (cmbKonfigurimi.Text != "")//nese eshte zgjedhur nje konfigurim tek combo e konfigurimeve
            {
                DbCore.DbShare.clsKonfigurimAmbjenti clsKonf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                clsKonf.mbushKonfiguriminMeKod(cmbKonfigurimi.Text, idNdermarrje, idGjuha);
                idKonfigurim = clsKonf.IdKonfigAmbjente;
            }
            else
            {//nese nuk ehste zgjedhur asnje konfigurim merret konfigurimi default                
                DbCore.DbShare.clsKonfigurimAmbjenti clsKonf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                clsKonf.mbushKonfigDefaultKomponentes(441, idNdermarrje);
                if (clsKonf != null)
                    clsKonf.mbushKonfigDefaultKomponentes(441, -1);
                idKonfigurim = clsKonf.IdKonfigAmbjente;
            }
            hfKushtet.Set("FPJM", DbCore.DbShare.clsAlternativaKushti.getAlternativa(idKonfigurim, "FPJM"));
            DbCore.DbShare.clsKusht kushtiCFDP = new DbCore.DbShare.clsKusht(idKonfigurim, "CFDP");
            hfKushtet.Set("CFDP", kushtiCFDP.Vlera);
            int idFormule = int.Parse(kushtiCFDP.Vlera.ToString());
            if (idFormule != 0)
            {
                DbCore.DbInventari.clsFormula formula = new DbCore.DbInventari.clsFormula(idFormule);
                hfKushtet.Set("formula", formula.PershkrimFormula);
            }
            else hfKushtet.Set("formula", "");
        }

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="idNderVit"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        private void percaktoTemplateMenu(int idViti, int idPerdorues, int idNdermarrje, ASPxMenu aSPxMenu1, ResourceManager rm, CultureInfo ci)
        {
            clsToolbarConfig.percaktoTemplateMenu((int)hfState["idGjuha"], idViti, idPerdorues, idNdermarrje, aSPxMenu1, komponente, this, MenuInfo, hfShtimModifikim.Value == "modifikim" ? false : true, true, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
            aSPxMenu1.Items.FindByName("Anullo").Text = rm.GetString("MenuItemMbyll", ci);
        }

        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            percaktoTemplateMenu(DbCore.mySessionObjects.ktheNdermarrjeVit(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), ASPxMenu1, rm, ci);
        }


        /// <summary>
        /// perdoret per te trajtuar ngjarjet e butonave te menuse. Shkon ne faqen e modifikimit te artikujve kur perdoruesi klikon butonin modifiko
        /// ose ne faqen e shtimit te artikujve kur perdoruesi klikon butonin shto
        /// </summary>
        /// <param name="source"> derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {//veprimet e menuse
            if (e.Item.Name == "Ruaj")
            {
                Page.Validate("entries");
                ruajArtikull();
            }
        }

        private void konfiguroVleraFillestare(int idGjuha, int idPerdoruesi, int idNdermarrje, System.Resources.ResourceManager rm, System.Globalization.CultureInfo ci)
        { //mbush komboboxet dhe gridat e faqes
            mbushListeCmimesh(idPerdoruesi, idNdermarrje);
            ConfigureAspxComboBox.mbushComboKlasa(cmbKlasa, true);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(btnMagazina,  btneKodifikimi1, btneKodifikimi2, btneKodifikimi3, txtFurnitori);
            ConfigureAspxComboBox.percaktoTemplateCombo(false,false, cmbNivelTvsh);
            ConfigureAspxComboBox.mbushComboKodifikim(idNdermarrje, btneKodifikimi1, 1, Request.QueryString["llojiart"] == "aqt" ? true : false, true);
            ConfigureAspxComboBox.mbushComboKodifikim(idNdermarrje, btneKodifikimi2, 2, Request.QueryString["llojiart"] == "aqt" ? true : false, true);
            ConfigureAspxComboBox.mbushComboKodifikim(idNdermarrje, btneKodifikimi3, 3, Request.QueryString["llojiart"] == "aqt" ? true : false, true);
            ConfigureAspxComboBox.ShtoKolonaPerKf(txtFurnitori);
            ConfigureAspxComboBox.percaktoTemplateComboMeEnableCallback(btneSkema, cmbLlogAmortizimi, btneLlogBle, btneLlogInv, btnLlogShpe, btnLlogPakesim, btneLlogShit, btneLlogTretet);
            ConfigureAspxComboBox.mbushComboMetodeKostoje(cmbMetode, true);
            ConfigureAspxComboBox.mbushComboMagazinat(idNdermarrje, btnMagazina, idPerdoruesi, false, 0, true);
            ConfigureAspxComboBox.mbushComboNjesi(idNdermarrje, cmbNjesia1);
            ConfigureAspxComboBox.mbushComboNjesi(idNdermarrje, cmbNjesia2);
            ConfigureAspxComboBox.mbushComboKategoriDetajimesh(cmbKategoriDetajimi1);
            ConfigureAspxComboBox.mbushComboKategoriDetajimesh(cmbKategoriDetajimi2);
            ConfigureAspxComboBox.KonfiguroComboBoxTaksat(idPerdoruesi, idNdermarrje, cmbNivelTvsh, DbCore.DbRegjistrim.LlojTakse.Nivel_Tvsh, false, false);
            bool lloji = Request.QueryString["llojiart"] == "aqt" ? true : false;
            if (lloji)
                mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 47, "AQTSH", rm, ci, idGjuha);
            else
                mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 47, "ARTSH", rm, ci, idGjuha);
            ConfigureAspxComboBox.shtoKolonaSkemaArtikulli(btneSkema, lloji, idNdermarrje);
            cmbKonfigurimi.SelectedIndex = 0;
            dteDateAk2.Date=new DateTime(2000,1,1);
            AspxWebControlUtils.vendosDateEditMask( dteDateAk2);
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()), idGjuha);
            hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;
            hfSkemaKlasa.Value = ConfigureAspxComboBox.mbushComboSkemaKontabilitetiArtikulli(idNdermarrje, btneSkema, Request.QueryString["llojiart"],"");
            int idMonedheZgjedhur = DbCore.clsFunksione.ktheMonedhePerFormatNumri(idGjuha, int.Parse(cmbKonfigurimi.Value.ToString()), idNdermarrje, 441, " ", -1, true);
            DbCore.DbShare.clsFormatiKonfig formatNrPerKonfig = new DbCore.DbShare.clsFormatiKonfig();
            formatNrPerKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(int.Parse(cmbKonfigurimi.Value.ToString()));
            DbCore.DbShare.clsFormatKonfigTrup formatMonedhe = clsFormatKonfigTrup.ktheFormatNumriMonedhe(formatNrPerKonfig, idMonedheZgjedhur);
            System.Web.Script.Serialization.JavaScriptSerializer serializusi = new System.Web.Script.Serialization.JavaScriptSerializer();
            hfFormatNumri.Set("formatMonedhe", serializusi.Serialize(formatMonedhe));
        }

        public void plotesoFushatSipasArtikullit(DbCore.DbInventari.clsArtikulli artikulli, int idNdermarrje, int idPerdoruesi)
        {
            txtKodi.Text = artikulli.KodArtikulli;
            txtKodi.ClientEnabled = false;
            txtPershkrimi.Text = artikulli.PershkrimArtikulli;
        }

        public void mbushComboKonfigurimeshSipasKategorise(int idPerdoruesi, int idNdermarrje, ASPxComboBox combo, int kat, string nivel, System.Resources.ResourceManager rm, System.Globalization.CultureInfo ci, int idGjuha)
        {//mbush griden e popupit me te dhena

            DbCore.DbShare.colKonfigurimAmbjenti col = new DbCore.DbShare.colKonfigurimAmbjenti();

            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            if (kat != 0)
            {
                konf.IdKategori = kat;
                konf.IdNdermarje = idNdermarrje;
                //DbCore.DbRegjistrim.clsNivelRegjistrimi niv = new DbCore.DbRegjistrim.clsNivelRegjistrimi();
                //niv.mbushNivelRegjistrimiSipasKodiMeKonvertime(nivel, idNdermarrje);
                int idNivel = DbCore.DbRegjistrim.clsNivelRegjistrimi.ktheIdNivelRegjistrimiSipasKodi(nivel, idNdermarrje);
                col.mbushKonfigAmbjSipasIdKategoriIdNivelMeLloj(konf.IdKategori, idNivel, idPerdoruesi);

            }
            else
            {
                col.mbushGjitheKonfigurimeAmbjentesh(idNdermarrje, idPerdoruesi, 1, idGjuha);

            }
            ListBoxColumn colprove = new ListBoxColumn();
            colprove.FieldName = "KodKonfigAmbjente";
            colprove.Caption = rm.GetString("cmbCmimeArtikulliCaptionKodi", ci);
            ListBoxColumn colemer = new ListBoxColumn();
            colemer.FieldName = "PershkrimKonfigAmbjente";
            colemer.Caption = rm.GetString("cmbCmimeArtikulliCaptionPershkrimi", ci);
            combo.TextFormatString = "{0}";
            combo.Columns.Add(colprove);
            combo.Columns.Add(colemer);
            combo.DataSource = col;
            combo.ValueField = "IdKonfigAmbjente";
            combo.IncrementalFilteringMode = DevExpress.Web.IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        private void konfiguroGrideCmimesh(int idPerdoruesi, int idNdermarrje)
        {//konfigurohet grida
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            KonfigurimComboGride.ShtoMonedhe(gvCmimet, idNdermarrje, idPerdoruesi, Session, komponente, guidString, "IdMonedha");
            KonfigurimComboGride.shtoPrindSipasNivelCmimi(gvCmimet, idNdermarrje, Session, komponente, guidString, "IdNivelCmimi");
            GridUtil.percaktoVisibleColumnsSipasKonfigurimit(gvCmimet, "gvCmimet", komponente, Convert.ToInt32(cmbKonfigurimi.Value), true, (int)hfState["idGjuha"], true);
            KonfigurimComboGride.ShtoDetajimet(gvCmimet, idNdermarrje, idPerdoruesi, Session, komponente, guidString, "IdDetajim");


            // DbCore.clsFunksione.percaktoVisibleColumnsMeWidth((int)hfState["idGjuha"], idNdermarrje, gvCmimet, "gvCmimet", "Shto_Artikull.aspx");
            //DbCore.clsFunksione.percaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "gvCmimet", gvCmimet, cmbKonfigurimi.Text, "441", (int)hfState["idGjuha"], !Page.IsPostBack,);
            GridUtil.konfiguroGrideRegjistrimEvogelPaTheme(gvCmimet, "IdCmimArtikulli", false);
            
            gvCmimet.SettingsPager.Mode = GridViewPagerMode.ShowPager;
            gvCmimet.SettingsPager.PageSize = 15;
            gvCmimet.SettingsBehavior.AllowSort = false;
            if (clsAlternativaKushti.getAlternativa(Convert.ToInt32(cmbKonfigurimi.Value), "ES") == "Po")
                gvCmimet.SettingsPager.Mode = GridViewPagerMode.EndlessPaging;
        }

       

        private void mbushListeCmimesh(int idPerdoruesi, int idNdermarrje)
        {//mbushet grida me te dhena    
            DbCore.DbInventari.colCmimeArtikujsh col = new DbCore.DbInventari.colCmimeArtikujsh();
            DataTable dt = DbCore.DbInventari.colNiveleCmimesh.merrNiveleNdermarjeDTBlerjeShitjeMeAutorizime(idNdermarrje, 0, idPerdoruesi);
            JavaScriptSerializer serializusi = new JavaScriptSerializer();
            int[] formatisasi = new int[dt.Rows.Count];
            int[] formaticmim = new int[dt.Rows.Count];
            int[] formatikursi = new int[dt.Rows.Count];
            int j = 0;
            DbCore.DbAdmin.colMonedhat colMon = new DbCore.DbAdmin.colMonedhat();
            colMon.mbushGjitheMonedhat(idNdermarrje, idPerdoruesi);
            foreach (DataRow dr in dt.Rows)
            {
                int idMonedha = int.Parse(dr["IdMonedha"].ToString());
                DbCore.DbAdmin.clsKurset kursi = new DbCore.DbAdmin.clsKurset(idMonedha, DateTime.Now);
                DbCore.DbInventari.clsCmimArtikulli cm = new DbCore.DbInventari.clsCmimArtikulli(0, 0, int.Parse(dr["IdNivelCmimi"].ToString()), 0, idMonedha, new DateTime(DateTime.Today.Year, 1, 1), new DateTime(9999, 12, 31), 0, 0, 0, idPerdoruesi, idNdermarrje, 0, 1, 0, 0, bool.Parse(dr["NjesiTeVarura"].ToString()), kursi.VleraKursi, new DateTime(DateTime.Today.Year, 1, 1, 0, 0, 0), new DateTime(9999, 12, 31, 23, 59, 59), 0, 0, 0, 0, 0,0);
                if (formatNrPerKonfigCmimi.IdFormatKonfig == 0)
                {
                    formatisasi[j] = 2;
                    formaticmim[j] = 2;
                }
                else
                {
                    formatisasi[j] = formatNrPerKonfigCmimi.KonfigTrupi.merrFormatSipasMonedhes(idMonedha).ShifraPasPresjesSasia;
                    formaticmim[j] = formatNrPerKonfigCmimi.KonfigTrupi.merrFormatSipasMonedhes(idMonedha).ShifraPasPresjesCmimi;
                }
                formatikursi[j] = colMon.Find(x => x.IdMonedha ==idMonedha).IdFormatNrKursi;
                j++;
                col.Add(cm);
            }
            hfState.Set("formatisasi", serializusi.Serialize(formatisasi));
            hfState.Set("formaticmim", serializusi.Serialize(formaticmim));
            hfState.Set("formatikursi", serializusi.Serialize(formatikursi));
            gvCmimet.DataSource = col;
            gvCmimet.DataBind();
        }

        private void mbushListeCmimeshMod(int idNdermarrje, int id, int idPerdorues)
        {//mbushet grida me te dhena
            DbCore.DbInventari.colCmimeArtikujsh col = new DbCore.DbInventari.colCmimeArtikujsh();
            col.mbushCmimArtikulliSipasArtikullitMeKostoMeAutorizime(id, idNdermarrje, idPerdorues);
            JavaScriptSerializer serializusi = new JavaScriptSerializer();
            int[] formatisasi = new int[col.Count];
            int[] formaticmim = new int[col.Count];
            int[] formatikursi = new int[col.Count];
            int j = 0;
            DbCore.DbAdmin.colMonedhat colMon = new DbCore.DbAdmin.colMonedhat();
            colMon.mbushGjitheMonedhat(idNdermarrje, idPerdorues);
            foreach (clsCmimArtikulli cm in col)
            {
                int idMonedha = cm.IdMonedha;
                if (formatNrPerKonfigCmimi.IdFormatKonfig == 0)
                {
                    formatisasi[j] = 2;
                    formaticmim[j] = 2;
                }
                else
                {
                    formatisasi[j] = formatNrPerKonfigCmimi.KonfigTrupi.merrFormatSipasMonedhes(idMonedha).ShifraPasPresjesSasia;
                    formaticmim[j] = formatNrPerKonfigCmimi.KonfigTrupi.merrFormatSipasMonedhes(idMonedha).ShifraPasPresjesCmimi;
                }
                formatikursi[j] = colMon.Find(x => x.IdMonedha == idMonedha).IdFormatNrKursi;
                j++;
            }
            hfState.Set("formatisasi", serializusi.Serialize(formatisasi));
            hfState.Set("formaticmim", serializusi.Serialize(formaticmim));
            hfState.Set("formatikursi", serializusi.Serialize(formatikursi));
            gvCmimet.DataSource = col;
            gvCmimet.DataBind();
        }

        private void percaktoTemplateCmimi()
        {//percaktohen templatet per fushat e grides
            GridViewDataDateColumn col2 = gvCmimet.Columns["DateFillimi"] as GridViewDataDateColumn;
            GridViewDataDateColumn col3 = gvCmimet.Columns["DateMbarimi"] as GridViewDataDateColumn;
            GridViewDataDateColumn col22 = gvCmimet.Columns["KoheFillimi"] as GridViewDataDateColumn;
            GridViewDataDateColumn col23 = gvCmimet.Columns["KoheMbarimi"] as GridViewDataDateColumn;
            GridViewDataTextColumn col7 = gvCmimet.Columns["Cmimi"] as GridViewDataTextColumn;
            GridViewDataTextColumn col8 = gvCmimet.Columns["Cmimi2"] as GridViewDataTextColumn;
            GridViewDataTextColumn col9 = gvCmimet.Columns["Kosto"] as GridViewDataTextColumn;
            GridViewDataTextColumn col10 = gvCmimet.Columns["Formula"] as GridViewDataTextColumn;
            GridViewDataCheckColumn col0 = gvCmimet.Columns["Update"] as GridViewDataCheckColumn;
            GridViewDataTextColumn col6 = gvCmimet.Columns["Kursi"] as GridViewDataTextColumn;
            GridViewDataTextColumn col4 = gvCmimet.Columns["SasiMin"] as GridViewDataTextColumn;
            GridViewDataTextColumn col5 = gvCmimet.Columns["SasiMax"] as GridViewDataTextColumn;
            GridViewDataTextColumn col17 = gvCmimet.Columns["CmimiTvsh"] as GridViewDataTextColumn;
            GridViewDataTextColumn col18 = gvCmimet.Columns["Cmimi2Tvsh"] as GridViewDataTextColumn;
            GridViewDataTextColumn col28 = gvCmimet.Columns["Norme"] as GridViewDataTextColumn;
            GridViewDataColumn col16 = gvCmimet.Columns["IdTvsh"] as GridViewDataColumn;
            col2.DataItemTemplate = new MyCalendarTemplate();
            col2.Width = 100;
            col3.DataItemTemplate = new MyCalendarTemplate();
            col3.Width = 100;
            col22.DataItemTemplate = new MyTimeEditTemplate();
            col23.DataItemTemplate = new MyTimeEditTemplate();
            col7.DataItemTemplate = new MyDoubleTemplate(true, 2, "0");
            col8.DataItemTemplate = new MyDoubleTemplate(true, 2, "0");
            col9.DataItemTemplate = new MyDoubleTemplate(true, 2, "0");
            col4.DataItemTemplate = new MyDoubleTemplate(true, 2, "0");
            col5.DataItemTemplate = new MyDoubleTemplate(true, 2, "0");
            col17.DataItemTemplate = new MyDoubleTemplate(true, 2, "0");
            col18.DataItemTemplate = new MyDoubleTemplate(true, 2, "0");
            col28.DataItemTemplate = new MyLabelTemplate();
            col16.DataItemTemplate = new MyComboTemplate();
            col10.DataItemTemplate = new MyComboTemplate();
            col6.DataItemTemplate = new MyDoubleTemplate(true, 2, "0"); //new MyReadOnlyTextTemplate();
            col0.DataItemTemplate = new MyButtonTemplate("Update");
        }
        
        private DbCore.DbInventari.colCmimeArtikujsh krijoCmimeArtikulli(int idNjesi1Artikulli, int idNjesi2Artikulli, int idPerdorues, int idNdermarrje)
        {
            colCmimeArtikujsh cmimet = new colCmimeArtikujsh();
            int idKonfigCmime = DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod("CSH", idNdermarrje);
            colCmimeArtikujsh colCmime = JsonConvert.DeserializeObject<colCmimeArtikujsh>(hfArtikuj.Value,
                new JsonSerializerSettings { DateTimeZoneHandling = DateTimeZoneHandling.Local });
            if (colCmime == null)
                return null;
            foreach (clsCmimArtikulli cm in colCmime)
            {
                cm.IdKonfig = idKonfigCmime;
                cm.IdNjesia = idNjesi1Artikulli;
                cm.IdNjesia2 = idNjesi2Artikulli;
                cm.IdStatusDok = 1;
                cm.IdPerdoruesi = idPerdorues;
                cm.IdNdermarje = idNdermarrje;
                if (cm.IdCmimArtikulli != 0 || cm.Cmimi != 0 || cm.Cmimi2 != 0 || cm.SasiMin != 0 || cm.SasiMax != 0)
                    cmimet.Add(cm);
            }
            return cmimet;
        }
        //sherben per te ruajtur nje artikull
        private void ruajArtikull()
        {
            if (Page.IsValid == false)
                return;
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DbCore.DbInventari.clsArtikulli artikulli;
            int idPerdorues = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), komponente);

            try
            {
                if (hfShtimModifikim.Value == "shtim")
                    artikulli = krijoArtikullin(idNdermarrje, idPerdorues, true, rm, ci);
                else artikulli = krijoArtikullin(idNdermarrje, idPerdorues, false, rm, ci);
            }
            catch (Exception e)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(e.Message);
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, e.Message, pnlMesazhi);
                hfStatusi.Value = "false";
                return;
            }

            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            DbCore.DbInventari.clsArtikullPerberesTemplateKoka template = new DbCore.DbInventari.clsArtikullPerberesTemplateKoka();
            DbCore.DbInventari.colCmimeArtikujsh colCmime = new DbCore.DbInventari.colCmimeArtikujsh();

            colCmime = krijoCmimeArtikulli(artikulli.Njesi1Artikulli, artikulli.Njesi2Artikulli, idPerdorues, idNdermarrje);

            bool eshteShtim;
            if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim")
            {
                if (!tedrejtaInfo.DShtim)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNukKeniTeDrejta", ci), pnlMesazhi);
                    hfStatusi.Value = "true";
                    return;
                }
                if (!nivelTVShIPranueshem())
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNukKeniPercaktuarNivelTVSH", ci), pnlMesazhi);
                    hfStatusi.Value = "false";
                    return;
                }
                mesazh = artikulli.ruaj(hfNrAutoKF, colCmime, template, false, "", "", "", "");
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
                if (!nivelTVShIPranueshem())
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNukKeniPercaktuarNivelTVSH", ci), pnlMesazhi);
                    hfStatusi.Value = "false";
                    return;
                }
                DbCore.DbRegjistrim.clsDatabaseRegjistrim dbRegjistrim = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
                DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                konf.mbushKonfigAmbjSipasId(artikulli.IdKonfig, DbCore.mySessionObjects.ktheGjuhe(Session));
                artikulli.IdArtikulli = int.Parse(hfId.Value.ToString());
                bool lidhur = dbRegjistrim.eshteDokumentiILidhurCelje(artikulli.IdArtikulli.ToString(), konf.IdNivel.ToString());
                if (lidhur.ToString() != hfLidhur.Value)
                {
                    mesazh.Status = false;
                    mesazh.PershkrimMesazhi = rm.GetString("msgShtoArtikullEshteILidhur", ci);
                }
                else
                {
                    //  DbCore.DbInventari.clsArtikulli artvjete = new DbCore.DbInventari.clsArtikulli(artikulli.IdArtikulli);
                    string artvjete = clsArtikulli.ktheKodArtikulliSipasId(artikulli.IdArtikulli);
                    if (artikulli.KodArtikulli != artvjete)
                    {
                        mesazh.Status = false;
                        mesazh.PershkrimMesazhi = rm.GetString("msgShtoArtikullNukMundTeNdryshoniKodin", ci);
                    }
                    else { mesazh = artikulli.modifiko(template, colCmime, false, "0", string.Empty, string.Empty, string.Empty); }
                }
                eshteShtim = false;
                //mesazh = artikulli.modifiko(colArtikujtPerberes, template);
            }

            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                hfStatusi.Value = "false";
            }
            else
            {
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("msgRuajtjeMeSukses", ci), pnlMesazhi);
                if (eshteShtim)
                {
                    DataRow newArtDr = DbCore.DbInventari.colArtikujt.merrSipasArtikujNdermarrjesAndAutorizimeDR(idNdermarrje, idPerdorues, artikulli.IdArtikulli, false, false);
                    DataTable dt = (DbCore.mySessionObjects.merrArtProdhNgaSesioni(Session));
                    if (dt != null) dt.ImportRow(newArtDr);
                    DataTable tmpObject;
                    bool sukses = DbCore.mySessionObjects.merrGrideNgaSessioni(Session, out tmpObject);
                    if (sukses)
                    {
                        tmpObject.ImportRow(newArtDr);
                        DbCore.mySessionObjects.ruajGrideNeSession(Session, tmpObject);
                    }
                }
                hfStatusi.Value = "true";
            }
            pnlMesazhi.Update();
        }

        private DbCore.DbInventari.clsArtikulli krijoArtikullin(int idNdermarrje, int idPerdorues, bool shtim, System.Resources.ResourceManager rm, System.Globalization.CultureInfo ci)
        {
            int idmag = 0;
            DbCore.DbInventari.clsArtikulli artikull = new DbCore.DbInventari.clsArtikulli();

            if (!shtim)
            {
                string kodArt = Request.QueryString["kodArt"];
                artikull.mbushArtikull(kodArt, idNdermarrje);
            }


            if (btnMagazina.Text != "")
            {
                DbCore.DbRegjistrim.clsNjesiAdministrative mag1 = new DbCore.DbRegjistrim.clsNjesiAdministrative(btnMagazina.Text, idNdermarrje, idPerdorues);
                idmag = mag1.IdNjesiAdministrative;
            }

            var controls = this.GetAsPxTextEditIdValue();
            controls.AddRange(ASPxPanel1.GetAsPxTextEditIdValue());

            hfNrAuto = (ASPxHiddenField)NrAuto.VendosVleratNrAuto(hfNrAuto, controls);
            //DbCore.DbAdmin.NrAuto.vendosVleratNrAuto(hfNrAuto, this, null, null, ASPxPanel1);

            int idNjesi1Artikulli = 0, idNjesi2Artikulli = 0, kodifikim1 = 0, kodifikim2 = 0, kodifikim3 = 0, idskema = 0, idfurnitori = 0, idlloginv = 0, idllogble = 0, idllogshit = 0,
                idllogpakesim = 0, idllogshpe = 0, idllogtret = 0, idllogamor = 0, idllogrez = 0, idllogpakrez = 0, idkategoridet1 = 0, idkategoridet2 = 0;

            bool iShitshem, detajim = false; int aplikimDhurate = 1;
            colDetajimePerArt coldet1 = new colDetajimePerArt();
            colDetajimePerArt coldet2 = new colDetajimePerArt();
            string kategoridet1 = "", kategoridet2 = "";
            int stokuMaxVfOne = 0;
            if (!shtim)
            {
                iShitshem = artikull.IShitshem;
                aplikimDhurate = artikull.AplikimDhurate;
                stokuMaxVfOne = artikull.StokuMaxVfOne;
            }
            else
                iShitshem = true;
            decimal koeficient = 1;
            if (cmbNjesia1.Text != "")
            {
                if (cmbNjesia1.Value != null)
                    int.TryParse(cmbNjesia1.Value.ToString(), out idNjesi1Artikulli);
                else
                    idNjesi1Artikulli = clsNjesiArtikulli.ktheIdNjesiArtikulli(cmbNjesia1.Text, idNdermarrje);

                if (shtim)
                {
                    idNjesi2Artikulli = idNjesi1Artikulli;
                }
                else
                {
                    idNjesi2Artikulli = artikull.Njesi2Artikulli;
                    koeficient = artikull.KoeficientArtikulli;
                }
            }
            string kodtakse = cmbNivelTvsh.Text != "" ? DbCore.DbRegjistrim.clsTaksa.ktheKodTakseMeId(Convert.ToInt32(cmbNivelTvsh.Value)) : "";
            if (cmbNjesia2.Text != "")
            {
                if (cmbNjesia2.Value != null)
                    int.TryParse(cmbNjesia2.Value.ToString(), out idNjesi2Artikulli);
                else
                    idNjesi2Artikulli = clsNjesiArtikulli.ktheIdNjesiArtikulli(cmbNjesia2.Text, idNdermarrje);
            }
            int klasa = cmbKlasa.Text != "" ? Convert.ToInt32(cmbKlasa.Value) : 0;
             if (String.IsNullOrEmpty(btneKodifikimi1.Text) && (Request.QueryString["llojiart"] == "aqt"))
            {
                throw new MyException(rm.GetString("msgFushaGrupim1JoBosh", ci));
            }
            if (!String.IsNullOrEmpty(btneKodifikimi1.Text))
                kodifikim1 = clsKodifikimArtikulli.ktheIdKodifikimi(btneKodifikimi1.Text, idNdermarrje, 1, Request.QueryString["llojiart"] == "aqt");
            if (!String.IsNullOrEmpty(btneKodifikimi2.Text))
                kodifikim2 = clsKodifikimArtikulli.ktheIdKodifikimi(btneKodifikimi2.Text, idNdermarrje, 2, Request.QueryString["llojiart"] == "aqt");
            if (!String.IsNullOrEmpty(btneKodifikimi3.Text))
                kodifikim3 = clsKodifikimArtikulli.ktheIdKodifikimi(btneKodifikimi3.Text, idNdermarrje, 3, Request.QueryString["llojiart"] == "aqt");
            DbCore.DbKontabiliteti.clsKlientFurnitor kf = new DbCore.DbKontabiliteti.clsKlientFurnitor();
            if (txtFurnitori.Value != null)
            {
                int.TryParse(txtFurnitori.Value.ToString(), out idfurnitori);
                kf = new DbCore.DbKontabiliteti.clsKlientFurnitor(idfurnitori);
            }
            if (btneSkema.Text != "")
            {
                if (btneSkema.Value != null)
                    int.TryParse(btneSkema.Value.ToString(), out idskema);
                else
                    idskema = clsSkemaKontabilitetiArtikulli.ktheIdSkemaKontabilitetArtikulli(btneSkema.Text, idNdermarrje, false);
            }
            if (btneLlogInv.Text != "")
            {
                if (btneLlogInv.Value != null)
                    int.TryParse(btneLlogInv.Value.ToString(), out idlloginv);
                else
                    idlloginv = DbCore.DbKontabiliteti.clsLlogari.mbushIDLlogariSipasKodit(btneLlogInv.Text, idNdermarrje);
            }
            if (btneLlogBle.Text != "")
            {
                if (btneLlogBle.Value != null)
                    int.TryParse(btneLlogBle.Value.ToString(), out idllogble);
                else
                    idllogble = DbCore.DbKontabiliteti.clsLlogari.mbushIDLlogariSipasKodit(btneLlogBle.Text, idNdermarrje);
            }
            if (btneLlogShit.Text != "")
            {
                if (btneLlogShit.Value != null)
                    int.TryParse(btneLlogShit.Value.ToString(), out idllogshit);
                else
                    idllogshit = DbCore.DbKontabiliteti.clsLlogari.mbushIDLlogariSipasKodit(btneLlogShit.Text, idNdermarrje);
            }
            if (btneLlogTretet.Text != "")
            {
                if (btneLlogTretet.Value != null)
                    int.TryParse(btneLlogTretet.Value.ToString(), out idllogtret);
                else
                    idllogtret = DbCore.DbKontabiliteti.clsLlogari.mbushIDLlogariSipasKodit(btneLlogTretet.Text, idNdermarrje);
            }
            if (btnLlogShpe.Text != "")
            {
                if (btnLlogShpe.Value != null)
                    int.TryParse(btnLlogShpe.Value.ToString(), out idllogshpe);
                else
                    idllogshpe = DbCore.DbKontabiliteti.clsLlogari.mbushIDLlogariSipasKodit(btnLlogShpe.Text, idNdermarrje);
            }
            if (btnLlogPakesim.Text != "")
            {
                if (btnLlogPakesim.Value != null)
                    int.TryParse(btnLlogPakesim.Value.ToString(), out idllogpakesim);
                else
                    idllogpakesim = DbCore.DbKontabiliteti.clsLlogari.mbushIDLlogariSipasKodit(btnLlogPakesim.Text, idNdermarrje);
            }
            if (cmbLlogAmortizimi.Text != "")
            {
                if (cmbLlogAmortizimi.Value != null)
                    int.TryParse(cmbLlogAmortizimi.Value.ToString(), out idllogamor);
                else
                    idllogamor = DbCore.DbKontabiliteti.clsLlogari.mbushIDLlogariSipasKodit(cmbLlogAmortizimi.Text, idNdermarrje);
            }

            detajim = cbDetajim.Checked;
            idkategoridet1 = cmbKategoriDetajimi1.Text != "" ? int.Parse(cmbKategoriDetajimi1.Value.ToString()) : 0;
            kategoridet1 = cmbKategoriDetajimi1.Text;
            idkategoridet2 = cmbKategoriDetajimi2.Text != "" ? int.Parse(cmbKategoriDetajimi2.Value.ToString()) : 0;
            kategoridet2 = cmbKategoriDetajimi2.Text;
            coldet1 = ruajDetajime(idNdermarrje, 1);
            coldet2 = ruajDetajime(idNdermarrje, 2);

            hfNrAutoKF = (ASPxHiddenField)NrAuto.ShtoNeRegjistrime(hfNrAutoKF, hfNrAuto, "txtKodi", "KodArtikulli");


            DbCore.DbInventari.clsArtikulli artikullNew = new DbCore.DbInventari.clsArtikulli(hfId.Value == "" ? 0 : int.Parse(hfId.Value), DbCore.clsFunksione.ktheStringunPaHapesira(txtKodi.Text, true), DbCore.clsFunksione.ktheStringunPaHapesira(txtPershkrimi.Text, false), DbCore.clsFunksione.ktheStringunPaHapesira(txtPershkrimiAng.Text, false), "", txtVendodhja.Text, kodifikim1, kodifikim2, btneKodifikimi1.Text, btneKodifikimi2.Text, "", idNjesi1Artikulli, idNjesi2Artikulli, cmbNjesia1.Text, cmbNjesia2.Text == "" ? cmbNjesia1.Text : cmbNjesia2.Text, txtKoeficienti.Text != "" ? Convert.ToDecimal(txtKoeficienti.Value) : koeficient, idfurnitori, kf.KodKlientFurnitor, txtPeshaBruto.Text != "" ? Convert.ToDecimal(txtPeshaBruto.Value) : Convert.ToDecimal(0), txtPeshaNeto.Text != "" ? Convert.ToDecimal(txtPeshaNeto.Value) : Convert.ToDecimal(0), detajim, klasa, idskema, cmbKlasa.Text, btneSkema.Text, idlloginv, idllogble, idllogshit, idllogtret, idllogpakesim, idllogshpe, idllogamor, idllogrez, idllogpakrez, btneLlogInv.Text, btneLlogBle.Text, btneLlogShit.Text, btnLlogPakesim.Text, btneLlogTretet.Text, btnLlogShpe.Text, cmbLlogAmortizimi.Text, "", "", txtMinimumi.Text != "" ? Convert.ToDecimal(txtMinimumi.Text) : Convert.ToDecimal(0), txtMaximumi.Text != "" ? Convert.ToDecimal(txtMaximumi.Text) : Convert.ToDecimal(0), cmbMetode.Value.ToString() != "" ? Convert.ToInt32(cmbMetode.Value) : 0, cmbMetode.Text, 0, 0, idPerdorues, true, checkKontrollGjendje.Checked, checkKontrollCmimi.Checked, checkKontrollGjendjeArtikulli.Checked, klasa == 4 || klasa == 5 || klasa == 6 ? krijoArtikujtPerberes(true) : null, new colGjendjeArtikulli(), new DbCore.DbInventari.colFurnitoreArtikujsh(), new DbCore.DbInventari.colArtikujtZevendesues(), new DbCore.DbAdmin.colVleraFushaShtese(), null, coldet1, Request.QueryString["llojiart"] == "aqt", idNdermarrje, cmbNivelTvsh.Text != "" ? Convert.ToInt32(cmbNivelTvsh.Value) : 0, kodtakse, cmbKonfigurimi.Value.ToString() != "" ? Convert.ToInt32(cmbKonfigurimi.Value) : 0, cmbAutorizimiHf.Value, shtim, Convert.ToDecimal(1), Convert.ToDecimal(0), cbProdhimMePorosi.Checked, idkategoridet1, kategoridet1, false, idkategoridet2, kategoridet2, coldet2, checkKontrollGjendjeDetajim2.Checked, 0, "", 0, 0, idmag, btnMagazina.Text != "" ? btnMagazina.Text : "", cbRezervueshem.Checked, cbPerTransferim.Checked, cbLoan.Checked, false, aplikimDhurate, 0, 0, "", cbMeSerial.Checked, iShitshem, false, ruajTrupin(), hfArkiva, rm, ci, ruajKodbare(), 0, cbPerPershore.Checked, txtPershkrimiFurnitori.Text, txtSiperfaqjaM2.Text, txtNrKontrate.Text, txtNrPasurie.Text, txtZonaKadastrale.Text, txtShasia.Text, txtMarka.Text, txtModeli.Text, txtVitProdhimi.Text, txtTeDhenaTeknika.Text, false, "", kodifikim3, btneKodifikimi3.Text, false, "", false, new colArtikullVfone(), 0, false, new colNormaAmortizimiRezerva(), 0, false, 0, "", stokuMaxVfOne, txtKodiIBarit.Text, cbIRimbursueshem.Checked);

            return artikullNew;
        }
        private DbCore.DbInventari.colDetajimePerArt ruajDetajime(int idNdermarrje, int lloji)
        {
            DbCore.DbInventari.colDetajimePerArt colDetArt = new DbCore.DbInventari.colDetajimePerArt();
            //lloji: 1-> per kategorine e pare, 2-> per kategorine e dyte
            if (lloji == 1)
            {
                if (btnDetajimi1.Text != "")
                {
                    string[] det = btnDetajimi1.Text.Split(',');
                    for (int i = 0; i < det.Length; i++)
                    {
                        DbCore.DbInventari.clsDetajimArtikulli detArtikulli = new DbCore.DbInventari.clsDetajimArtikulli();
                        detArtikulli.mbushDetajimArtikulli(det[i], idNdermarrje);
                        DbCore.DbInventari.clsDetajimPerArt c = new DbCore.DbInventari.clsDetajimPerArt();
                        c.IdDetajimArtikulli = detArtikulli.IdDetajimArtikulli;
                        c.LlojDetajim = 1;
                        colDetArt.Add(c);
                    }
                }
            }
            else if (lloji == 2)
            {
                if (btnDetajimi2.Text != "")
                {
                    string[] det = btnDetajimi2.Text.Split(',');
                    for (int i = 0; i < det.Length; i++)
                    {
                        DbCore.DbInventari.clsDetajimArtikulli detArtikulli = new DbCore.DbInventari.clsDetajimArtikulli();
                        detArtikulli.mbushDetajimArtikulli(det[i], idNdermarrje);
                        DbCore.DbInventari.clsDetajimPerArt c = new DbCore.DbInventari.clsDetajimPerArt();
                        c.IdDetajimArtikulli = detArtikulli.IdDetajimArtikulli;
                        c.LlojDetajim = 2;
                        colDetArt.Add(c);
                    }
                }
            }
            return colDetArt;
        }

        protected void gvCmimet_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpNoRows"] = gvCmimet.VisibleRowCount;
            e.Properties["cpNoPage"] = gvCmimet.PageIndex;
        }

        protected void gvCmimet_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            try
            {
                DbCore.DbInventari.colCmimeArtikujsh col = new DbCore.DbInventari.colCmimeArtikujsh();
                JavaScriptSerializer serializusi = new JavaScriptSerializer();
                serializusi.MaxJsonLength = 500000000;
                // col = (DbCore.DbInventari.colCmimeArtikujsh)
                if (hfArtikuj.Value != "")
                {
                    object[] o = (object[])serializusi.DeserializeObject(hfArtikuj.Value);
                    foreach (object oo in o)
                    {
                        DbCore.DbInventari.clsCmimArtikulli cm = new DbCore.DbInventari.clsCmimArtikulli();
                        cm = cm.krijoCmim((Dictionary<string, object>)oo);
                        col.Add(cm);
                    }
                    gvCmimet.DataSource = col;
                    gvCmimet.DataBind();
                }
            }
            catch (Exception err)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
                return;
            }
        }

        protected void gvCmimet_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            try
            {
                if (e.Parameters.Split(';').Length == 2)
                {
                    int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                    int idPerdorues = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                    if (e.Parameters.Split(';')[1] == "modifiko")
                    {
                        mbushListeCmimeshMod(idNdermarrje, int.Parse(e.Parameters.Split(';')[0]), idPerdorues);
                    }
                    else
                    {
                        mbushListeCmimesh(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);
                    }
                }
            }
            catch (Exception err)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
                return;
            }
        }

        protected void gvCmimet_DataBound(object sender, EventArgs e)
        {
            
            gvCmimet.KeyFieldName = "IdCmimArtikulli";
            gvCmimet.SettingsBehavior.AllowFocusedRow = true;

        }

        protected void gvCmimet_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            try
            {
                bool ugjet;
                if (e.RowType == DevExpress.Web.GridViewRowType.Data)
                {
                    GridViewDataDateColumn col1 = ((ASPxGridView)sender).Columns["DateFillimi"] as GridViewDataDateColumn;
                    GridViewDataDateColumn col2 = ((ASPxGridView)sender).Columns["DateMbarimi"] as GridViewDataDateColumn;

                    GridViewDataDateColumn col21 = ((ASPxGridView)sender).Columns["KoheFillimi"] as GridViewDataDateColumn;
                    GridViewDataDateColumn col22 = ((ASPxGridView)sender).Columns["KoheMbarimi"] as GridViewDataDateColumn;
                    GridViewDataTextColumn col16 = ((ASPxGridView)sender).Columns["Norme"] as GridViewDataTextColumn;
                    GridViewDataTextColumn col5 = ((ASPxGridView)sender).Columns["Cmimi"] as GridViewDataTextColumn;
                    GridViewDataTextColumn col7 = ((ASPxGridView)sender).Columns["Cmimi2"] as GridViewDataTextColumn;
                    GridViewDataTextColumn col8 = ((ASPxGridView)sender).Columns["Kosto"] as GridViewDataTextColumn;
                    GridViewDataTextColumn col15 = ((ASPxGridView)sender).Columns["CmimiTvsh"] as GridViewDataTextColumn;
                    GridViewDataTextColumn col17 = ((ASPxGridView)sender).Columns["Cmimi2Tvsh"] as GridViewDataTextColumn;
                    GridViewDataTextColumn col10 = ((ASPxGridView)sender).Columns["UpdateBtn"] as GridViewDataTextColumn;
                    GridViewDataTextColumn col6 = ((ASPxGridView)sender).Columns["Kursi"] as GridViewDataTextColumn;
                    GridViewDataTextColumn col11 = ((ASPxGridView)sender).Columns["Formula"] as GridViewDataTextColumn;
                    GridViewDataTextColumn col3 = ((ASPxGridView)sender).Columns["SasiMin"] as GridViewDataTextColumn;
                    GridViewDataTextColumn col4 = ((ASPxGridView)sender).Columns["SasiMax"] as GridViewDataTextColumn;
                    GridViewDataColumn col26 = gvCmimet.Columns["IdTvsh"] as GridViewDataColumn;
                    ASPxDateEdit dt1 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col1, "cal") as ASPxDateEdit;
                    ASPxDateEdit dt2 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col2, "cal") as ASPxDateEdit;
                    ASPxTimeEdit dt11 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col21, "te") as ASPxTimeEdit;
                    ASPxTimeEdit dt12 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col22, "te") as ASPxTimeEdit;
                    ASPxTextBox txt5 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col5, "txtBox") as ASPxTextBox;
                    ASPxTextBox txt6 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col7, "txtBox") as ASPxTextBox;
                    ASPxLabel txt25 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col16, "lbl") as ASPxLabel;
                    ASPxTextBox txt7 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col8, "txtBox") as ASPxTextBox;
                    ASPxButton btn0 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col10, "btn") as ASPxButton;
                    ASPxTextBox txt8 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col6, "txtBox") as ASPxTextBox;
                    ASPxComboBox txt9 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col11, "cmbBox") as ASPxComboBox;
                    ASPxTextBox txt15 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col15, "txtBox") as ASPxTextBox;
                    ASPxTextBox txt16 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col17, "txtBox") as ASPxTextBox;
                    ASPxTextBox txt3 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col3, "txtBox") as ASPxTextBox;
                    ASPxTextBox txt4 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col4, "txtBox") as ASPxTextBox;
                    ASPxComboBox txt26 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col26, "cmbBox") as ASPxComboBox;
                    ugjet = false;
                    bool teDrejtaModCmimi = (bool)hfTeDrejtaCmimi.Get("Modifikim");
                    if (dt1 != null)
                    {
                        dt1.ClientInstanceName = "DateFillimi" + e.VisibleIndex.ToString();
                        dt1.ClientSideEvents.TextChanged = "function(s,e){TextChangedDataFill(s,e," + e.VisibleIndex + ");}";
                        dt1.ClientEnabled = teDrejtaModCmimi;
                    }
                    if (dt2 != null)
                    {
                        dt2.ClientInstanceName = "DateMbarimi" + e.VisibleIndex.ToString();
                        dt2.ClientSideEvents.TextChanged = "function(s,e){TextChangedDataFill(s,e," + e.VisibleIndex + ");}";
                        dt2.ClientEnabled = teDrejtaModCmimi;
                    }
                    if (dt11 != null)
                    {
                        dt11.ClientInstanceName = "KoheFillimi" + e.VisibleIndex.ToString();
                        dt11.ClientSideEvents.DateChanged = "function(s,e){TextChangedKohaFill(s,e," + e.VisibleIndex + ");}";
                        dt11.ClientEnabled = teDrejtaModCmimi;
                    }
                    if (dt12 != null)
                    {
                        dt12.ClientInstanceName = "KoheMbarimi" + e.VisibleIndex.ToString();
                        dt12.ClientSideEvents.DateChanged = "function(s,e){TextChangedKohaMbar(s,e," + e.VisibleIndex + ");}";
                        dt12.ClientEnabled = teDrejtaModCmimi;
                    }
                    if (txt25 != null)
                    {
                        txt25.ClientInstanceName = "Norme" + e.VisibleIndex.ToString();

                        txt25.ClientSideEvents.Init = "function(s,e){Utils.initTxtNumber(s,e)}";

                    }
                    if (txt3 != null)
                    {
                        txt3.ClientInstanceName = "SasiMin" + e.VisibleIndex.ToString();
                        txt3.ClientSideEvents.TextChanged = "function(s,e){TextChangedSasiMaxMin(SasiMin" + e.VisibleIndex.ToString() + ", " + e.VisibleIndex.ToString() + ", false, e);}";
                        txt3.ClientSideEvents.Init = "function(s,e){Utils.initTxtNumber(s,e)}";
                        txt3.ClientSideEvents.GotFocus = "function(s,e){Utils.gotFocusTxtNumer(s,e);}";
                        txt3.ClientSideEvents.LostFocus = "function(s,e){Utils.lostFocusTxtNumer(s,e);TextChangedSasiMaxMin(SasiMin" + e.VisibleIndex.ToString() + ", " + e.VisibleIndex.ToString() + ", false, e); }";
                        txt3.ClientEnabled = teDrejtaModCmimi;
                        if (e.VisibleIndex == ((ASPxGridView)sender).VisibleRowCount - 2)
                        {
                            if (ugjet)
                            {
                                temptxt = txt3;
                                ugjet = false;
                            }
                        }
                    }
                    if (txt4 != null)
                    {
                        txt4.ClientInstanceName = "SasiMax" + e.VisibleIndex.ToString();
                        txt4.ClientSideEvents.TextChanged = "function(s,e){TextChangedSasiMaxMin(SasiMax" + e.VisibleIndex.ToString() + ", " + e.VisibleIndex.ToString() + ", true, e);}";
                        txt4.ClientSideEvents.Init = "function(s,e){Utils.initTxtNumber(s,e)}";
                        txt4.ClientSideEvents.GotFocus = "function(s,e){Utils.gotFocusTxtNumer(s,e);}";
                        txt4.ClientSideEvents.LostFocus = "function(s,e){Utils.lostFocusTxtNumer(s,e); TextChangedSasiMaxMin(SasiMin" + e.VisibleIndex.ToString() + ", " + e.VisibleIndex.ToString() + ", false, e);}";
                        txt4.ClientEnabled = teDrejtaModCmimi;
                        if (e.VisibleIndex == ((ASPxGridView)sender).VisibleRowCount - 2)
                        {
                            if (ugjet)
                            {
                                temptxt = txt4;
                                ugjet = false;
                            }
                        }
                    }

                    if (txt5 != null)
                    {
                        txt5.ClientEnabled = teDrejtaModCmimi;
                        bool njesiTeVarura = false;
                        if (((ASPxGridView)sender).DataSource != null)
                            njesiTeVarura = (bool)((DbCore.DbInventari.colCmimeArtikujsh)((ASPxGridView)sender).DataSource)[e.VisibleIndex].NjesiTeVarura;
                        txt5.ClientInstanceName = "Cmimi" + e.VisibleIndex.ToString();
                        txt5.ClientSideEvents.TextChanged = "function(s,e){TextChangedCmimet(s,e," + e.VisibleIndex + ", '" + njesiTeVarura.ToString() + "', 'Cmimi1');}";
                        txt5.ClientSideEvents.Init = "function(s,e){Utils.initTxtNumber(s,e)}";
                        txt5.ClientSideEvents.GotFocus = "function(s,e){Utils.gotFocusTxtNumer(s,e);}";
                        txt5.ClientSideEvents.LostFocus = "function(s,e){Utils.lostFocusTxtNumer(s,e)}";

                    }


                    if (txt6 != null)
                    {
                        txt6.ClientEnabled = teDrejtaModCmimi;
                        bool njesiTeVarura = false;
                        if (((ASPxGridView)sender).DataSource != null)
                            njesiTeVarura = (bool)((DbCore.DbInventari.colCmimeArtikujsh)((ASPxGridView)sender).DataSource)[e.VisibleIndex].NjesiTeVarura;
                        txt6.ClientInstanceName = "Cmimi2" + e.VisibleIndex.ToString();
                        txt6.ClientSideEvents.TextChanged = "function(s,e){TextChangedCmimet(s,e," + e.VisibleIndex + ", '" + njesiTeVarura.ToString() + "', 'Cmimi2');}";
                        txt6.ClientSideEvents.Init = "function(s,e){Utils.initTxtNumber(s,e)}";
                        txt6.ClientSideEvents.GotFocus = "function(s,e){Utils.gotFocusTxtNumer(s,e);}";
                        txt6.ClientSideEvents.LostFocus = "function(s,e){Utils.lostFocusTxtNumer(s,e)}";
                    }

                    if (txt7 != null)
                    {
                        txt7.ClientEnabled = false;
                        txt7.ClientInstanceName = "Kosto" + e.VisibleIndex.ToString();
                        txt7.ClientSideEvents.Init = "function(s,e){Utils.initTxtNumber(s,e)}";
                        txt7.ClientSideEvents.GotFocus = "function(s,e){Utils.gotFocusTxtNumer(s,e);}";
                        txt7.ClientSideEvents.LostFocus = "function(s,e){Utils.lostFocusTxtNumer(s,e)}";
                    }
                    if (txt15 != null)
                    {
                        txt15.ClientEnabled = teDrejtaModCmimi;
                        bool njesiTeVarura = false;
                        if (((ASPxGridView)sender).DataSource != null)
                            njesiTeVarura = (bool)((DbCore.DbInventari.colCmimeArtikujsh)((ASPxGridView)sender).DataSource)[e.VisibleIndex].NjesiTeVarura;
                        txt15.ClientInstanceName = "CmimiTvsh" + e.VisibleIndex.ToString();
                        txt15.ClientSideEvents.TextChanged = "function(s,e){TextChangedCmimet(s,e," + e.VisibleIndex + ", '" + njesiTeVarura.ToString() + "', 'Cmimi1MeTvsh');}";
                        txt15.ClientSideEvents.Init = "function(s,e){Utils.initTxtNumber(s,e)}";
                        txt15.ClientSideEvents.GotFocus = "function(s,e){Utils.gotFocusTxtNumer(s,e);}";
                        txt15.ClientSideEvents.LostFocus = "function(s,e){Utils.lostFocusTxtNumer(s,e)}";

                    }
                    if (txt26 != null)
                    {
                        txt26.ClientInstanceName = "IdTvsh" + e.VisibleIndex.ToString();
                        txt26.TextFormatString = "{0}";
                        txt26.Columns.Add(new ListBoxColumn("KodTaksa"));
                        txt26.Columns.Add(new ListBoxColumn("NormaPerqindje"));
                        var colTaksa = new colTaksa((int)hfState["idNdermarrje"], DbCore.DbRegjistrim.LlojTakse.Nivel_Tvsh, (int)hfState["idPerdoruesi"]);
                        colTaksa.Insert(0, clsTaksa.krijoTaksePaTVSH());
                        txt26.DataSource = colTaksa;
                        txt26.TextField = "KodTaksa";
                        txt26.ValueField = "IdTaksa";
                        txt26.DataBind();
                        txt26.ClientSideEvents.TextChanged = "function(s,e){TextChangedIdTvsh(s,e," + e.VisibleIndex + ");}";
                        txt26.ClientSideEvents.Init = "function(s,e){Utils.initTxtNumber(s,e)}";
                        txt26.ClientSideEvents.GotFocus = "function(s,e){Utils.gotFocusTxtNumer(s,e);}";
                        txt26.ClientSideEvents.LostFocus = "function(s,e){Utils.lostFocusTxtNumer(s,e)}";
                        txt26.ClientEnabled = teDrejtaModCmimi;
                    }

                    if (txt16 != null)
                    {
                        txt16.ClientEnabled = teDrejtaModCmimi;
                        bool njesiTeVarura = false;
                        if (((ASPxGridView)sender).DataSource != null)
                            njesiTeVarura = (bool)((DbCore.DbInventari.colCmimeArtikujsh)((ASPxGridView)sender).DataSource)[e.VisibleIndex].NjesiTeVarura;
                        txt16.ClientInstanceName = "Cmimi2Tvsh" + e.VisibleIndex.ToString();
                        txt16.ClientSideEvents.TextChanged = "function(s,e){TextChangedCmimet(s,e," + e.VisibleIndex + ", '" + njesiTeVarura.ToString() + "', 'Cmimi2MeTvsh');}";
                        txt16.ClientSideEvents.Init = "function(s,e){Utils.initTxtNumber(s,e)}";
                        txt16.ClientSideEvents.GotFocus = "function(s,e){Utils.gotFocusTxtNumer(s,e);}";
                        txt16.ClientSideEvents.LostFocus = "function(s,e){Utils.lostFocusTxtNumer(s,e)}";

                    }


                    if (btn0 != null)
                    {
                        btn0.ClientInstanceName = "UpdateBtn" + e.VisibleIndex;
                        btn0.ClientSideEvents.Click = "function(s,e){ClickUpdateBtn(s,e," + e.VisibleIndex + ");}";
                        btn0.ClientEnabled = teDrejtaModCmimi;
                    }
                    if (txt8 != null)
                    {
                        txt8.ClientInstanceName = "Kursi" + e.VisibleIndex.ToString();
                        txt8.ClientEnabled = false;
                        txt8.ClientSideEvents.Init = "function(s,e){Utils.initTxtNumber(s,e)}";
                        txt8.ClientSideEvents.GotFocus = "function(s,e){Utils.gotFocusTxtNumer(s,e);}";
                        txt8.ClientSideEvents.LostFocus = "function(s,e){Utils.lostFocusTxtNumer(s,e)}";
                    }
                    if (txt9 != null)
                    {
                        txt9.DropDownButton.Visible = false;
                        txt9.Buttons.Add();
                        txt9.TextFormatString = "{0},{1}";
                        txt9.Columns.Add(new ListBoxColumn("KodFormula"));
                        txt9.Columns.Add(new ListBoxColumn("PershkrimFormula"));
                        DataTable dt = DbCore.DbInventari.colFormulat.merrFormulatSipasNdermarrjes(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                        txt9.DataSource = dt;
                        txt9.TextField = "PershkrimFormula";
                        txt9.ValueField = "IdFormula";
                        txt9.DataBind();
                        dt.Dispose();
                        txt9.ClientInstanceName = "txtFormula" + e.VisibleIndex;
                        txt9.ClientSideEvents.SelectedIndexChanged = "function(s,e){SelectedIndexChangedFormula('txtFormula" + e.VisibleIndex.ToString() + "'," + e.VisibleIndex.ToString() + ");}";
                        txt9.ClientSideEvents.TextChanged = "function(s,e){TextChangedFormula('txtFormula" + e.VisibleIndex.ToString() + "'," + e.VisibleIndex.ToString() + ");}";
                        txt9.ClientSideEvents.KeyPress = String.Format("function(s,e){{var code = _getKeyCode(e.htmlEvent); KeyPressFormula(code,txtFormula{0},{0}); }}", e.VisibleIndex);
                        txt9.ClientSideEvents.LostFocus = "function(s,e){LostFocusFormula('txtFormula" + e.VisibleIndex.ToString() + "'," + e.VisibleIndex.ToString() + ");}";
                        txt9.ClientSideEvents.ButtonClick = "function(s,e){ButtonClickFormula('txtFormula" + e.VisibleIndex.ToString() + "'," + e.VisibleIndex.ToString() + ")}";
                        txt9.DropDownStyle = DropDownStyle.DropDown;

                        if (!teDrejtaModCmimi)
                            txt9.ClientEnabled = false;
                        else
                        {
                            if (Request.QueryString["llojiart"] == "afatshkurter")
                            {
                                if (hfKushtet.Get("FPJM").ToString() == "Po")
                                {
                                    txt9.ClientEnabled = true;
                                    txt9.Enabled = true;
                                }
                                else if(hfKushtet.Get("FPJM").ToString() == "Jo")
                                {
                                    txt9.ClientEnabled = false;
                                    txt9.Enabled = false;
                                }
                                string formula = hfKushtet.Get("formula").ToString();
                                if (formula != "")
                                    txt9.Text = formula;
                                else txt9.Text = "";
                            }
                        }
                        if (e.VisibleIndex == ((ASPxGridView)sender).VisibleRowCount - 1 - nrRreshtatsh)
                        {
                            if (ugjet)
                            {
                                temptxtNormal = txt9;
                                ugjet = false;
                            }
                            //else if (koloneFocus == "Formula")
                            //{
                            //    ugjet = true;
                            //}
                        }
                    }
                }

                if (temptxt != null)
                {
                    temptxt.Focus();
                }
                else if (tempcombo != null)
                {
                    tempcombo.Focus();
                }
            }
            catch (Exception err)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
                return;
            }
        }

  

        protected void cmbNjesia1_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("cmbNjesia1"))
                {
                    ConfigureAspxComboBox.mbushComboNjesi(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), cmbNjesia1);
                }
            }
        }

        protected void btneKodifikimi1_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("btneKodifikimi1"))
                {
                    ConfigureAspxComboBox.mbushComboKodifikim(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), btneKodifikimi1, 1, Request.QueryString["llojiart"] == "aqt" ? true : false, false);

                }
            }
        }

        protected void btneKodifikimi2_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("btneKodifikimi2"))
                {
                    ConfigureAspxComboBox.mbushComboKodifikim(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), btneKodifikimi2, 2, Request.QueryString["llojiart"] == "aqt" ? true : false, false);

                }
            }
        }
        protected void btneKodifikimi3_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("btneKodifikimi3"))
                {
                    ConfigureAspxComboBox.mbushComboKodifikim(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), btneKodifikimi3, 3, Request.QueryString["llojiart"] == "aqt" ? true : false, false);

                }
            }
        }

        private colKodbare ruajKodbare()
        {
            if (String.IsNullOrEmpty(hfKodbaret.Value))
                return new colKodbare();

            JavaScriptSerializer serializusi = new JavaScriptSerializer();
            Object[] kodbaret = (Object[])serializusi.DeserializeObject(hfKodbaret.Value);
            return new colKodbare(kodbaret);
        }


        //pati

        ASPxComboBox temptxtNormal = null;
        private int nrRreshtatsh = 5;
       

        private void konfiguroGrideArtikujPerberes()
        {
            DbCore.DbAdmin.colGridaTrupi vlere;
            DbCore.DbShare.clsKonfigurimAmbjenti konfigurimi = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konfigurimi.mbushKonfigDefaultKomponentes(402, -1);
            vlere = GridUtil.percaktoVisibleColumnsSipasKonfigurimitPerClientSide2("gvArtPerberes", "Shto_Artikull.aspx?llojiart=afatshkurter", 1, (int)hfState["idGjuha"]);
            JavaScriptSerializer serializusi = new JavaScriptSerializer();
            this.HfGridCol.Value = serializusi.Serialize(vlere);
        }

        protected DbCore.DbInventari.colArtikulliPerberes krijoArtikujtPerberes(bool ruaj)
        {
            //ruaj tregon nqs funksioni thirret nga funksioni krijoartikullin, apo nga callback te grides. 
            //Nqs thirret nga callback ath shtohen te colart edhe rreshtat bosh, perndryshe shtohen vetem rreshtat e plotesuar.
            try
            {
                DbCore.DbInventari.colArtikulliPerberes colArt = new DbCore.DbInventari.colArtikulliPerberes();
                JavaScriptSerializer serializues = new JavaScriptSerializer();
                serializues.MaxJsonLength = 500000000;
                int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                object[] artPerb = (object[])serializues.DeserializeObject(hfArtikujtPerberes.Value);
                if (artPerb != null)
                {
                    foreach (object oo in artPerb)
                    {
                        DbCore.DbInventari.clsArtikulliPerberes artPerberes = new DbCore.DbInventari.clsArtikulliPerberes();
                        Dictionary<string, object> rresht = (Dictionary<string, object>)oo;

                        string lloji = rresht["cmbLloji"].ToString();
                        if (lloji == "Artikull") artPerberes.Lloji = 1; else artPerberes.Lloji = 2;
                        if (rresht["txtKoeficienti"].ToString() != null && rresht["txtKoeficienti"].ToString() != "null" && rresht["txtKoeficienti"].ToString() != "")
                            artPerberes.Koeficienti = decimal.Parse(rresht["txtKoeficienti"].ToString());
                        if (rresht["txtFiro"].ToString() != null && rresht["txtFiro"].ToString() != "null" && rresht["txtFiro"].ToString() != "")
                            artPerberes.Scrap = decimal.Parse(rresht["txtFiro"].ToString());
                        artPerberes.GjithmoneNgaStoku = false;//per momentin nuk perdoret
                        artPerberes.Njesia = rresht["txtNjesia"].ToString();
                        artPerberes.DtNdryshimi = DateTime.Today;
                        if (artPerberes.Lloji == 1)
                        {
                            DbCore.DbInventari.clsArtikulli art = new DbCore.DbInventari.clsArtikulli();
                            if (rresht["txtKodi"] == null || rresht["txtKodi"].ToString() == "") continue;
                            art.merrSipasKodArtikullit(rresht["txtKodi"].ToString(), idNdermarrje);
                            artPerberes.IdLidheseArt = art.IdArtikulli;
                            artPerberes.IdLidhese = art.IdArtikulli;
                        }
                        else if (artPerberes.Lloji == 2)
                        {

                            DbCore.DbProdhimi.clsAktiviteteKoka aktivitet = new DbCore.DbProdhimi.clsAktiviteteKoka(rresht["txtKodi"].ToString(), idNdermarrje);
                            artPerberes.IdLidheseAkt = aktivitet.IdKoka;
                            artPerberes.IdLidhese = aktivitet.IdKoka;
                        }

                        if (ruaj == true)
                        {
                            if ((rresht["txtKodi"].ToString() != ""))
                                colArt.Add(artPerberes);
                        }
                        else colArt.Add(artPerberes);
                    }
                }
                return colArt;
            }
            catch (Exception err)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
                return null;
            }
        }

        protected void txtFurnitori_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            try
            {
                if (IsCallback)
                {
                    if (Request.Params["__CALLBACKID"].ToString().Contains("txtFurnitori"))
                    {
                        int value = 0;
                        if (e.Value == null || !Int32.TryParse(e.Value.ToString(), out value))
                            return;
                        ConfigureAspxComboBox.KonfiguroComboBoxKlientFurnitoriById(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), (ASPxComboBox)source, value);
                    }
                }
            }
            catch (Exception err)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
                return;
            }
        }

        protected void txtFurnitori_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            try
            {
                if (IsCallback)
                {
                    if (Request.Params["__CALLBACKID"].ToString().Contains("txtFurnitori"))
                    {
                        ConfigureAspxComboBox.KonfiguroComboBoxKlientFurnitor(e.Filter, e.BeginIndex + 1, e.EndIndex + 1, DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), txtFurnitori, 2);
                    }
                }
            }
            catch (Exception err)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
                return;
            }
        }

        protected void btneSkema_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("btneSkema"))
                {
                    hfSkemaKlasa.Value = ConfigureAspxComboBox.mbushComboSkemaKontabilitetiArtikulli(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), btneSkema, Request.QueryString["llojiart"], e.Value);
                }
            }

        }
        protected void btneSkema_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("btneSkema"))
                {
                    hfSkemaKlasa.Value = ConfigureAspxComboBox.mbushComboSkemaKontabilitetiArtikulli((int)hfState["idNdermarrje"], btneSkema, Request.QueryString["llojiart"], e.Filter, Convert.ToInt32(cmbKlasa.Value));
                }
            }
        }

        protected void btneLlogInv_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("btneLlogInv"))
                {
                    ConfigureAspxComboBox.mbushComboLlogariaPaKolona(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), btneLlogInv, e);
                }
            }
        }
        protected void btneLlogInv_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("btneLlogInv"))
                {
                    ConfigureAspxComboBox.mbushComboLlogariaPaKolona(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), btneLlogInv, e);
                }
            }
        }


        protected void btneLlogBle_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("btneLlogBle"))
                {
                    ConfigureAspxComboBox.mbushComboLlogariaPaKolona(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), btneLlogBle, e);
                }
            }
        }
        protected void btneLlogBle_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("btneLlogBle"))
                {
                    ConfigureAspxComboBox.mbushComboLlogariaPaKolona(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), btneLlogBle, e);
                }
            }
        }

        protected void btneLlogShit_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("btneLlogShit"))
                {
                    ConfigureAspxComboBox.mbushComboLlogariaPaKolona(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), btneLlogShit, e);
                }
            }
        }
        protected void btneLlogShit_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("btneLlogShit"))
                {
                    ConfigureAspxComboBox.mbushComboLlogariaPaKolona(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), btneLlogShit, e);
                }
            }
        }

        protected void btnLlogPakesim_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("btnLlogPakesim"))
                {
                    ConfigureAspxComboBox.mbushComboLlogariaPaKolona(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), btnLlogPakesim, e);
                }
            }
        }
        protected void btnLlogPakesim_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("btnLlogPakesim"))
                {
                    ConfigureAspxComboBox.mbushComboLlogariaPaKolona(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), btnLlogPakesim, e);
                }
            }
        }

        protected void btneLlogTretet_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("btneLlogTretet"))
                {
                    ConfigureAspxComboBox.mbushComboLlogariaPaKolona(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), btneLlogTretet, e);
                }
            }
        }
        protected void btneLlogTretet_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("btneLlogTretet"))
                {
                    ConfigureAspxComboBox.mbushComboLlogariaPaKolona(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), btneLlogTretet, e);
                }
            }
        }

        protected void btnLlogShpe_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("btnLlogShpe"))
                {
                    ConfigureAspxComboBox.mbushComboLlogariaPaKolona(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), btnLlogShpe, e);
                }
            }
        }
        protected void btnLlogShpe_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("btnLlogShpe"))
                {
                    ConfigureAspxComboBox.mbushComboLlogariaPaKolona(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), btnLlogShpe, e);
                }
            }
        }

        protected void cmbLlogAmortizimi_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("cmbLlogAmortizimi"))
                {
                    ConfigureAspxComboBox.mbushComboLlogariaPaKolona(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), cmbLlogAmortizimi, e);
                }
            }
        }
        protected void cmbLlogAmortizimi_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("cmbLlogAmortizimi"))
                {
                    ConfigureAspxComboBox.mbushComboLlogariaPaKolona(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), cmbLlogAmortizimi, e);
                }
            }
        }
        protected void cmbNivelTvsh_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("cmbNivelTvsh"))
                {
                    ConfigureAspxComboBox.KonfiguroComboBoxTaksat((int)hfState["idPerdoruesi"], (int)hfState["idNdermarrje"], cmbNivelTvsh, DbCore.DbRegjistrim.LlojTakse.Nivel_Tvsh, false, false,false);
                }
            }
        }

        protected void cmbNivelTvsh_PreRender(object sender, EventArgs e)
        {
           // cmbNivelTvsh.Items[0].Text = "";
        }
        


        private bool nivelTVShIPranueshem()
        {
            if (!tvshEdetyrueshme())
                return true;

            return !(cmbNivelTvsh.Value == null || Convert.ToInt32(cmbNivelTvsh.Value.ToString()) == -1);
        }

        private bool tvshEdetyrueshme()
        {

            DbCore.DbShare.clsAtributeTrupi atribute = new DbCore.DbShare.clsAtributeTrupi();
            if (atribute.mbushAtributSipasKompKonfDheKontrollit(0, Convert.ToInt32(cmbKonfigurimi.Value.ToString()), "cmbNivelTvsh", 441))
                return atribute.Detyrueshme;

            return false;
        }
        #region grida e amortizimit
        private void konfiguroVleraFillestareNorma(int idNdermarje)
        {
            DbCore.DbAsete.colAseteNormaAmortizimi col = new colAseteNormaAmortizimi();
            int id = 0;
            if (hfShtimModifikim.Value == "modifikim" || hfShtimModifikim.Value == "klonim")
            {
                int.TryParse(hfId.Value.ToString(), out id);
                if (id > 0)
                    col.merrArtikullNormaAmortizimiTeGjitha(id, idNdermarje, dteDateAk2.Date.Date);
                else
                    col.merrArtikullNormaAmortizimiFillestare(idNdermarje, dteDateAk2.Date.Date);
            }
            else
                col.merrArtikullNormaAmortizimiFillestare(idNdermarje, dteDateAk2.Date.Date);
            gvAmortizimi.DataSource = col;
            gvAmortizimi.DataBind();
        }

        private void konfiguroGrideNorma(int idNdermarje, int idGjuha)
        {
            shtoStandart(idNdermarje);
            GridUtil.percaktoVisibleColumnsMeWidth(idGjuha, idNdermarje, gvAmortizimi, "gvAmortizimi", "Shto_Artikull.aspx");
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            GridUtil.konfiguroGrideListeEvogelPaTheme(gvAmortizimi, "IdLidhjeArtikullLlojAmort", false);
            gvAmortizimi.Settings.ShowFilterRow = false;
            gvAmortizimi.SettingsBehavior.AllowSort = false;
            gvAmortizimi.SettingsBehavior.AllowGroup = false;
            gvAmortizimi.Settings.ShowFilterRowMenu = false;
            gvAmortizimi.Settings.ShowHeaderFilterButton = false;
            gvAmortizimi.SettingsEditing.Mode = GridViewEditingMode.Inline;
            gvAmortizimi.Settings.ShowFilterBar = GridViewStatusBarMode.Hidden;
            gvAmortizimi.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
            percaktoTemplate();

        }
        private void percaktoTemplate()
        {
            GridViewDataColumn col7 = gvAmortizimi.Columns["IdLlojAmortizimi"] as GridViewDataColumn;
            col7.DataItemTemplate = new MyComboTemplate();
            GridViewDataColumn col17 = gvAmortizimi.Columns["NormeMagazine"] as GridViewDataColumn;
            col17.DataItemTemplate = new MyComboTemplate();
            GridViewDataColumn col10 = gvAmortizimi.Columns["Norme"] as GridViewDataColumn;
            col10.DataItemTemplate = new MyDoubleTemplate(false, 2, "0");
            GridViewDataColumn col1 = gvAmortizimi.Columns["IdStandartAmortizimi"] as GridViewDataColumn;
            col1.DataItemTemplate = new MyLabelTemplate();
        }
        private void shtoStandart(int idNdermarrje)
        {
            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            if (typeof(GridViewDataComboBoxColumn) != gvAmortizimi.Columns["IdStandartAmortizimi"].GetType())
            {
                gvAmortizimi.Columns.Remove(gvAmortizimi.Columns["IdStandartAmortizimi"]);
                gvAmortizimi.Columns.Add(colnew);
                DbCore.DbAsete.colStandarteAmortizimi col = new DbCore.DbAsete.colStandarteAmortizimi(idNdermarrje);
                col.Insert(0, new DbCore.DbAsete.clsStandarteAmortizim());
                colnew.PropertiesComboBox.DataSource = col;
                colnew.PropertiesComboBox.TextField = "Emertimi";
                colnew.PropertiesComboBox.ValueField = "IdStandarti";
                colnew.FieldName = "IdStandartAmortizimi";
                colnew.Visible = true;
                colnew.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                DbCore.mySessionObjects.ruajDsComboGrideNeSession(Session, col, "colStandarte");
            }
            else
            {
                colnew = (GridViewDataComboBoxColumn)gvAmortizimi.Columns["IdStandartAmortizimi"];
                if (colnew.PropertiesComboBox.Items.Count == 0)
                {
                    colnew.PropertiesComboBox.DataSource = DbCore.mySessionObjects.merrDsComboGrideNeSession(Session, "colStandarte");
                }
            }


        }


        private DbCore.DbAsete.colAseteNormaAmortizimi ruajTrupin()
        {
            JavaScriptSerializer serializusi = new JavaScriptSerializer();
            object[] dokumenti = (object[])serializusi.DeserializeObject(gridDataObject.Value);
            colAseteNormaAmortizimi trupat = new colAseteNormaAmortizimi();
            int idNdermarrje = (int)hfState["idNdermarrje"];
            for (int i = 0; i < dokumenti.Length; i++)
            {
                DbCore.DbAsete.clsAseteNormaAmortizimi trup = new DbCore.DbAsete.clsAseteNormaAmortizimi(idNdermarrje, (Dictionary<string, object>)dokumenti[i], dteDateAk2.Date);
                trupat.Add(trup);
            }
            return trupat;
        }


        protected void gvAmortizimi_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters != "")
            {
                colAseteNormaAmortizimi col = new colAseteNormaAmortizimi();
                int id = 0;
                int.TryParse(e.Parameters, out id);
                col.ktheArtikullNormaAmortizimiSipasIdKodifikimit(id, (int)hfState["idNdermarrje"],dteDateAk2.Date.Date);

                gvAmortizimi.DataSource = col;
                gvAmortizimi.DataBind();
            }
            else konfiguroVleraFillestareNorma((int)hfState["idNdermarrje"]);
            konfiguroGrideNorma((int)hfState["idNdermarrje"], (int)hfState["idGjuha"]);
        }

        protected void gvAmortizimi_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gvAmortizimi.PageIndex;
            e.Properties["cpPageRow"] = gvAmortizimi.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gvAmortizimi.VisibleRowCount;
        }

        protected void gvAmortizimi_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {

            if (e.RowType == DevExpress.Web.GridViewRowType.Data)
            {
                GridViewDataColumn col0 = ((ASPxGridView)sender).Columns["IdLlojAmortizimi"] as GridViewDataColumn;
                ASPxComboBox btn0 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col0, "cmbBox") as ASPxComboBox;
                GridViewDataColumn col1 = ((ASPxGridView)sender).Columns["NormeMagazine"] as GridViewDataColumn;
                ASPxComboBox btn1 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col1, "cmbBox") as ASPxComboBox;
                GridViewDataColumn col10 = gvAmortizimi.Columns["Norme"] as GridViewDataColumn;
                ASPxTextBox btn2 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col10, "txtBox") as ASPxTextBox;
                GridViewDataColumn col11 = gvAmortizimi.Columns["IdStandartAmortizimi"] as GridViewDataColumn;
                ASPxLabel btn3 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col11, "lbl") as ASPxLabel;
                if (btn3 != null)
                {
                    btn3.ClientInstanceName = "lblStandart" + e.VisibleIndex.ToString();
                }
                if (btn0 != null)
                {
                    btn0.ClientInstanceName = "cmbLlojAmortizimi" + e.VisibleIndex.ToString();
                    DbCore.DbAsete.colAseteLlojAmortizimi col = new DbCore.DbAsete.colAseteLlojAmortizimi();
                    col.merrTeGjithaLlojAmortizimesh();
                    btn0.DataSource = col;
                    btn0.TextField = "LlojAmortizimi";
                    btn0.ValueField = "IdLlojAmortizimi";
                    if (btn0.Text == "")
                        btn0.SelectedIndex = 0;
                    btn0.DataBind();

                }
                if (btn1 != null)
                {
                    btn1.ClientInstanceName = "cmbNormeMagazine" + e.VisibleIndex.ToString();
                    btn1.Items.Add("Artikull", "Unchecked");
                    btn1.Items.Add("Magazine", "Checked");
                    btn1.DataBind();
                }
                if (btn2 != null)
                {
                    btn2.ClientInstanceName = "txtNorme" + e.VisibleIndex.ToString();
                    btn2.ClientSideEvents.TextChanged = "function (s,e){if(isNaN(parseFloat(s.GetText()))) {myMesazh.ShtoMesazhGabimi('Norma duhet te jete numer!'); s.SetText(0);} if(parseFloat(s.GetText())<0 ||parseFloat(s.GetText())>100){myMesazh.ShtoMesazhGabimi('Norma duhet te jete midis 0 dhe 100!'); s.SetText(0);} }";
                }
            }
        }
        #endregion
    }
}