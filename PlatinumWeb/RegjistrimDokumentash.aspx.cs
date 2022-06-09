using DbCore;
using DbCore.DbAdmin;
using DbCore.DbInventari;
using DbCore.DbKontabiliteti;
using DbCore.DbRegjistrim;
using DbCore.DbShare;
using DevExpress.Web;
using PlatinumWeb.Templates;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Reflection;
using System.Resources;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Extensions;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using System.Linq;
using System.Web.Script.Serialization;
using PlatinumWeb.ApplicationUtils.Filters;
using DbCore.IMBUtils.Messages;
using DevExpress.Data.Filtering;
using DbCore.DbArkaBanka;
using DbCore.IMBUtils.Fiskalizimi.API;
using System.Web.Configuration;
using System.Web.UI;
using DbCore.IMBUtils.Fiskalizimi.Controls;

namespace PlatinumWeb
{
    /// <summary>
    /// nderfaqja e RegjistrimDokumentasht 
    /// </summary>
    public partial class RegjistrimDokumentash : MyPageBase
    {

        private string guidString;
        private int idPerdoruesi;
        private int idGjuha;
        private int idNdermarrje;
        private int idViti;
        private int idNdermarrjeVit;
        private CultureInfo ci;
        private ResourceManager rm => new ResourceManager("Resources.Strings", Assembly.Load("App_GlobalResources"));
        private bool eshteMeme;
        private string veprimi;
        private bool eshteOwn;
        private string komponente => DbCore.clsFunksione.GetKomponente(Page.Request);
        private string filterExpressionDefault = "[IdStatusDok]=1 and [Final]='Final'and ([Prodhuar]=0 or [Prodhuar]=1)";

        private TitlePeriudha _periudha;
        public TitlePeriudha Periudha => _periudha ?? (_periudha = this.MerrPeriudhe(hfState));

        /// <summary>
        /// kur lodohet faqja  mbushet me te dhena dhe kontrollohen ne eshte i autorizuar perdoruesi
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKPARAM"].Contains("ROWVALUES"))//kur grida ben callback behet dy here callbacku njehere me ngjarjen qe po ndodh dhe njehere me rowvalues. per te eleminuar marrjen e te dhenave heren e dyte dalim nga funksioni
                return;
            string datanga, dataderi;
            if (!IsPostBack)
            {
                if (!DbCore.mySessionObjects.isLogedIn(Session)) DbCore.clsFunksione.logout(Session, true, "FaqePaautorizuar");

                idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
                idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
                eshteMeme = DbCore.mySessionObjects.merrEshteMemeSesioni(Session);
                guidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                eshteOwn = DbCore.mySessionObjects.merrEshteOwnSesioni(Session);
                veprimi = Request.QueryString["shitje_blerje"];
                idNdermarrjeVit = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
                colTeDrejtaRoli teDrejtaNiveleRregjistrimi = new colTeDrejtaRoli();
                teDrejtaNiveleRregjistrimi.mbushTeDrejtaPerdoruesiNiveleRegjistrimiPerKomponente(idPerdoruesi, idNdermarrje, idViti, komponente);

                hfState.Set("veprimi", veprimi);
                hfState.Set("idNdermarrjeVit", idNdermarrjeVit);
                hfState.Set("OwnShop", eshteOwn);
                hfState.Set("guidString", guidString);
                hfState.Set("idPerdoruesi", idPerdoruesi);
                hfState.Set("idGjuha", idGjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("idViti", idViti);
                hfState.Set("EmriFile", "Faturat e blerjes/shitjes");
                hfState.Set("eshteMeme", eshteMeme);
                hfState.Set("teDrejtaNivele", Newtonsoft.Json.JsonConvert.SerializeObject(teDrejtaNiveleRregjistrimi));
                
                ci = MessagesResource.KtheCultureInfo(idGjuha);
                string kodniveli = veprimi == "shitje" ? "LDSH" : (veprimi == "shitjediscount" ? "LDSHD" : (veprimi == "bazaar" ? "LDSHB" : "LDB"));
                hfState.Set("kodNiveli", kodniveli);
                ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 52, kodniveli, rm, ci, idGjuha);
                mbushHiddenFieldMePerkthime(ci, rm);
                cmbKonfigurimi.SelectedIndex = 0;
                DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()), idGjuha);
                
                merrColKushtet(konf.IdKonfigAmbjente);
                shfaqButonaRaporti(idPerdoruesi, idNdermarrje, idViti, "porosiDealerVodafone", "procedimProdhimi", "ofertBlerje", "AparateTeShitur","KartaTeShitura", "RingarkuesTeShitur", "ShitjeDheLikujdimePermbledhese", "FaturaShitjeEinvoice", "FaturaBlerjeEinvoice");
                hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaDokumenta = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaDokumenta.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, komponente);
                hfTeDrejtaGjitheDok.Value = tedrejtaDokumenta.DGjitheDok.ToString();                
                clsPeriudhaKontabel oPeriudha = DbCore.mySessionObjects.merrPeriudheKontabel(Session);               
                DbCore.mySessionObjects.RuajNeSession<DataTable>(Session, null, komponente + "&periudhaDok=Aktuale" + idNdermarrjeVit);
                DbCore.mySessionObjects.RuajNeSession<DataTable>(Session, null, komponente + "&periudhaDok=3 Mujore" + idNdermarrjeVit);
                DbCore.mySessionObjects.RuajNeSession<DataTable>(Session, null, komponente + "&periudhaDok=Vit ushtrimor" + idNdermarrjeVit);
                DbCore.mySessionObjects.RuajNeSession<DataTable>(Session, null, komponente + "&periudhaDok=Javore" + idNdermarrjeVit);
                DbCore.mySessionObjects.RuajNeSession<DataTable>(Session, null, komponente + "&periudhaDok=Ditore" + idNdermarrjeVit);
                grid_RegDok.PercaktoTitlePanelPerRegjistrimDokumentash(this, MenuInfo, pnlMesazhi, hfState, idPerdoruesi, idNdermarrje, IdViti, idGjuha, int.Parse(cmbKonfigurimi.Value.ToString()), "RegjistrimDokumentash.aspx?shitje_blerje=shitje", 505, "IdShitjeKoka", rm, ci);
                mbushGridNgaDB(komponente, Periudha.PeriudhaDok, idNdermarrjeVit, veprimi, idNdermarrje, idPerdoruesi, tedrejtaDokumenta.DGjitheDok, Periudha.DataDokNga, Periudha.DataDokDeri);
                konfiguroGride(komponente, veprimi, idNdermarrje, idPerdoruesi, idGjuha, rm, ci);
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "grid_RegDok", grid_RegDok, cmbKonfigurimi.Text.Split(';')[0], "505", idGjuha, true);

                if ((bool)hfKushte["RFF"] && !String.IsNullOrEmpty(grid_RegDok.FilterExpression))
                    grid_RegDok.RestoreFilter(idNdermarrje, veprimi, grid_RegDok.FilterExpression);
                                  
                if (Request.QueryString["ruaj"] == "po")
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("regjMagModifikimiPerfundoiMeSukses", ci), pnlMesazhi);
                if (Request.QueryString["refuzoDraft"] == "po")
                        clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("msgDokURefuzua", ci), pnlMesazhi);
                hfVeprimi.Value = veprimi;
                clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idNdermarrje, "grid_RegDok", int.Parse(cmbKonfigurimi.Value.ToString()), "RegjistrimDokumentash.aspx?shitje_blerje=shitje");
            }
            else
            {
                idPerdoruesi = (int)hfState["idPerdoruesi"];
                idNdermarrje = (int)hfState["idNdermarrje"];
                idViti = (int)hfState["idViti"];
                idGjuha = (int)hfState["idGjuha"];
                eshteMeme = (bool)hfState["eshteMeme"];
                ci = MessagesResource.KtheCultureInfo(idGjuha);
                veprimi = (string)hfState["veprimi"];
                idNdermarrjeVit = (int)hfState["idNdermarrjeVit"];
                guidString = (string)hfState["guidString"];           
                konfiguroGride(komponente, veprimi, idNdermarrje, idPerdoruesi, idGjuha, rm, ci);
                grid_RegDok.PercaktoTitlePanelPerRegjistrimDokumentash(this, MenuInfo, pnlMesazhi, hfState, idPerdoruesi, idNdermarrje, IdViti, idGjuha, Convert.ToInt32(cmbKonfigurimi.Value), "RegjistrimDokumentash.aspx?shitje_blerje=shitje", 505, "IdShitjeKoka", rm, ci);
                mbushGridNgaSession(komponente, Periudha.PeriudhaDok, idNdermarrjeVit, veprimi, idNdermarrje, idPerdoruesi, Periudha.DataDokNga, Periudha.DataDokDeri, cmbKonfigurimi.Text);
                merrColKushtet(Convert.ToInt32(cmbKonfigurimi.Value));
            }
            if (Request.QueryString["indexrow"] != null)
            {
                grid_RegDok.FocusedRowIndex = int.Parse(Request.QueryString["indexrow"]);
            }
            percaktoTemplateMenu(idGjuha, ASPxMenu1, idViti, idPerdoruesi, idNdermarrje, eshteMeme);
            clsNdermarrje ndermarrje = new clsNdermarrje(idNdermarrje);
            //if (ndermarrje.Fiskalizimi == true && veprimi == "shitje")
            //{
            //    using (ScriptManager scriptManager = (ScriptManager)this.FindControl("ScriptManager1"))
            //    {
            //        if(clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizim())
            //            scriptManager.RegisterPostBackControl(ASPxMenu1);

            //    }
            //}
        }

        private void merrColKushtet(int idKonfigurim)
        {
            Dictionary<string, clsAlternativaKushti> kushtet = colAlternativatKushti.MerrAlternativaKushtiSipasIdKonfigurimi(idKonfigurim);
            hfKushte.Set("RFF", kushtet.ContainsKey("RFF") ? kushtet["RFF"].Alternativa == "Po" : false);
            hfKushte.Set("LKDKN", kushtet.ContainsKey("LKDKN") ? kushtet["LKDKN"].Alternativa == "Po" : false);
        }

        /// <summary>
        /// Metode qe sherben per te kaluar perkthime te js
        /// </summary>
        /// <param name="cultinf">Merr culture info</param>
        /// <param name="rm">Merr Resource Manager</param>
        private void mbushHiddenFieldMePerkthime(CultureInfo cultinf, ResourceManager rm)
        {
            clsFunksione.ShtoPerkthimNeHfState(hfState, "hapListenPerMeShumeInfo",
            "regjisDokMsgFaturaEshtePrintNeKase",
            "labelAdministrimiMsgJeniSigurt",
            "regjisDokNukKeniAsnjeDokTeZgjedhur",
            "regjisDokZgjidhniNjeDokument", 
            "regjisDokNukKeniAsnjeDokTeZgjedhur", 
            "regjisDokZgjidhDokPerTeBashkengjitur", 
            "regjisDokZgjidhniTePakten1DokPerKonvertim", 
            "regjisDokZgjidhniTePakten1DokPerLidhje", 
            "msgDokTeJeneTeSeNjejtesNenkategori",
            "msgDokTeKeneTeNjejtinKF", 
            "msgDokNukMundTeKonvertohet", 
            "msgNukKeniTeDrejtaNeKeteAmbjent",
            "msgNukMundTeLikuidoniNjeFatureDraft",
            "msgNukKonvertohenDokumentatFatOferteKerkese", 
            "msgDokILikuiduar", 
            "msgZgjdhniNjeNgaElementetEListes", 
            "msgDokNukJaneUrdherShitje", 
            "msgDokNukJaneOferteShitje", 
            "regjisDokZgjidhniTePakten1Dok", 
            "regjisDokJoKthimDisaFat", 
            "msgJoKthimFatureDraft", 
            "msgJoKthimDokFatOferteKerkese", 
            "msgJoKthimDokILikuiduar", 
            "msgJoKthimFatureVlereNegative", 
            "msgJuKeniZgjedhur", 
            "msgRreshta", 
            "msgDokTeKeneTeNjejtenMonedhe", 
            "msgRreshta", 
            "msgRreshtatDeshtuan", 
            "msgUKonvertua", 
            "msgAllConverted", 
            "regjMagMesazhZgjidhniNje",
            "msgDokTeKeneTeNjejtinKlient",
            "regjMagMesazhZgjidhniFaturePerPrintim",
            "regjShitjeMesazhSkaFormatPerPrintim",
            "msgNukLikuidohetTotali0"
            );
            konfigurimi_Label.Text = rm.GetString("lblModeli", cultinf);
        }


        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="idGjuha"></param>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        /// <param name="idViti"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNermarrje"></param>
        /// <param name="eshteMeme"></param>
        private void percaktoTemplateMenu(int idGjuha, ASPxMenu aSPxMenu1, int idViti, int idPerdorues, int idNdermarrje, bool eshteMeme)
        {
            clsToolbarConfig.percaktoTemplateMenu(idGjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1, DbCore.clsFunksione.GetKomponente(Page.Request), this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, btnPo_Click, btnJo_Click, true, false, false, eshteMeme, false);
            if ((string)hfState["kodNiveli"] == "LDSHD" || (string)hfState["kodNiveli"] == "LDSHB")
                aSPxMenu1.Items[5].Text = "Ekzekuto shitje";
        }

        
        public void btnJo_Click(object sender, EventArgs e)
        {

            if (pergjigja.Text.Contains("lidhur") || pergjigja.Text.Contains("kycur"))
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, pergjigja.Text, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, pergjigja.Text, pnlMesazhi);

            DbCore.mySessionObjects.ruajTrupatNeSession(hfState["guidString"].ToString(), Session, new DbCore.DbRegjistrim.colTrupiMagazina());
        }

        public void btnPo_Click(object sender, EventArgs e)
        {
            CultureInfo cultinf = MessagesResource.KtheCultureInfo((int)hfState["idGjuha"]);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            string guidString = hfState["guidString"].ToString();
            DbCore.DbRegjistrim.colTrupiMagazina trupat = DbCore.mySessionObjects.merrTrupatNgaSesioni(guidString, Session);
            string fileLogPath = Server.MapPath("~/log/log.txt");
            DbCore.DbAdmin.clsLogRivleresimInventari log = new DbCore.DbAdmin.clsLogRivleresimInventari();
            try
            {
                log = new DbCore.DbAdmin.clsLogRivleresimInventari(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            }
            catch (Exception err)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
                throw new DbCore.MyException(rm.GetString("msgGabimGjateRuajtjesSeRivleresimitNeLog", cultinf));
            }
            foreach (DbCore.DbRegjistrim.clsTrupiMagazina t in trupat)
            {
                if (!DbCore.DbInventari.clsArtikulli.rivleresimCmimiMesatar(t.IdArtikulli, clsArtikulli.ktheMetodeKostoje(t.IdArtikulli), t.IdMag, t.Data, DateTime.Today, clsArtikulli.ktheKontrollCmimiPerDetajim(t.IdArtikulli), log, cultinf, rm,  DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session),mySessionObjects.ktheIdPerdoruesi(Session)).Status)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("regjisDokMesazhGabimRivleresimi", cultinf), pnlMesazhi);
                }
            }

            DbCore.mySessionObjects.ruajTrupatNeSession(guidString, Session, new DbCore.DbRegjistrim.colTrupiMagazina());
            clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("regjMagMesazhSuksesRivleresimi", cultinf), pnlMesazhi);
        }

        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu((int)hfState["idGjuha"], ASPxMenu1, (int)hfState["idViti"], (int)hfState["idPerdoruesi"], (int)hfState["idNdermarrje"], (bool)hfState["eshteMeme"]);
        }

        /// <summary>
        /// mbush griden me te dhena te ruajtura ne sesion
        /// </summary>
        /// <param name="komponente"></param>
        /// <param name="idNdermarrjeVit"></param>
        /// <param name="veprimi"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="idPerdoruesi"></param>
        private void mbushGridNgaSession(string komponente, string periudheDok, int idNdermarrjeVit, string veprimi, int idNdermarrje, int idPerdoruesi, string datanga, string dataderi,string kodKonfig)
        {
            DataTable tmpObject = DbCore.mySessionObjects.MerrNgaSession<DataTable>(Session, komponente + periudheDok + idNdermarrjeVit);
            tmpObject = grid_RegDok.MerrDataSourceMePeriduheNeSession<DataTable>(Session, komponente, Periudha, kodKonfig, guidString);
            if (tmpObject == null)
            {
                bool gjitheDokumentat = hfTeDrejtaGjitheDok.Value == "True" || hfTeDrejtaGjitheDok.Value == "true";
                mbushGridNgaDB(komponente, periudheDok, idNdermarrjeVit, veprimi, idNdermarrje, idPerdoruesi, gjitheDokumentat, datanga, dataderi);
            }
            else
            {
                grid_RegDok.DataSource = tmpObject;
                if (!IsCallback && Request["__CALLBACKID"] == "grid_RegDok" && (bool)hfKushte.Get("RFF"))
                    grid_RegDok.RestoreFilter(idNdermarrje, veprimi, filterExpressionDefault);
                grid_RegDok.SaveFilter(idNdermarrje, veprimi);
                grid_RegDok.DataBind();
                tmpObject.Dispose();
            }
        }

        /// <summary>
        /// mbush griden me te dhena nga db
        /// <param name="gjitheDokumentat">true nqs perdoruesi ka te drejta t'i shohe te gjitha dokumentat, false nqs mund te shohe vetem dokumentat e veta</param>
        /// </summary>
        /// <param name="komponente"></param>
        /// <param name="idNdermarrjeVit"></param>
        /// <param name="veprimi"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="idPerdorues"></param>
        private void mbushGridNgaDB(string komponente, string periudheDok, int idNdermarrjeVit, string veprimi, int idNdermarrje, int idPerdoruesi, bool gjitheDokumentat, string datanga, string dataderi)
        {//mbush griden e popupit me te dhena            
            DataTable dt = new DataTable();
            bool merrStatusPorosie = false;
            bool merrProdhuar = false;
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasKod(cmbKonfigurimi.Text.Split(';')[0], idNdermarrje);
            int discountdevice = veprimi == "shitjediscount" ? 1 : veprimi == "bazaar" ? 2 : 0;
            bool merrKonvertuarPlote = (clsAlternativaKushti.getAlternativa(konf.IdKonfigAmbjente, "SHK") == "Po");
            bool merrDokKonvertuar = (clsAlternativaKushti.getAlternativa(konf.IdKonfigAmbjente, "SHSKD") == "Po");
            if (veprimi == "shitje" || veprimi == "shitjediscount" || veprimi == "bazaar")
            {
                if (clsAlternativaKushti.getAlternativa(konf.IdKonfigAmbjente, "SHSP") == "Po")
                    merrStatusPorosie = true;
                if (clsAlternativaKushti.getAlternativa(konf.IdKonfigAmbjente, "SHP") == "Po")
                    merrProdhuar = true;

                dt = DbCore.DbRegjistrim.colKokaShitje.merrKokaShitjeDT(idNdermarrjeVit, 1, idPerdoruesi, idNdermarrje, merrStatusPorosie, gjitheDokumentat, merrProdhuar, merrKonvertuarPlote, datanga, dataderi, discountdevice, merrDokKonvertuar,konf.KodKonfigAmbjente);
            }
            else
                if (DbCore.mySessionObjects.merrEshteMemeSesioni(Session))
                dt = DbCore.DbRegjistrim.colKokaShitje.merrKokaShitjeDTKthimBlerjePrind(idNdermarrje);

            else dt = DbCore.DbRegjistrim.colKokaShitje.merrKokaShitjeDT(idNdermarrjeVit, 2, idPerdoruesi, idNdermarrje, merrStatusPorosie, gjitheDokumentat, merrProdhuar, merrKonvertuarPlote, datanga, dataderi, discountdevice, merrDokKonvertuar, konf.KodKonfigAmbjente);
            if (dt == null)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ndodhi nje problem gjate leximit te listes! Ju lutem riperserisni veprimin!", pnlMesazhi);
                return;
            }
            grid_RegDok.DataSource = dt;
            grid_RegDok.DataBind();
            grid_RegDok.RuajDataSourceMePeriduheNeSession(Session, komponente, Periudha,konf.KodKonfigAmbjente, dt, guidString);
            //DbCore.mySessionObjects.RuajNeSession<DataTable>(Session, dt, komponente + periudheDok + idNdermarrjeVit);
            dt.Dispose();
            
        }

        /// <summary>
        /// ben te dukshem ose jo butonat qe te cojne te nje raport i caktuar telista e shitjes, ne varesi ne qofte se perdoruesi ka te drejta mbi ate raport ose jo
        /// </summary>
        /// <param name="idPerdoruesi"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="idViti"></param>
        /// <param name="idRaporti"></param>
        private void shfaqButonaRaporti(int idPerdoruesi, int idNdermarrje, int idViti, params string[] emerRaporti)
        {
                        
            for (int i = 0; i < emerRaporti.Length; i++)
            {
                var drejtarap = new clsTeDrejtaRaporte();
                drejtarap.merrTeDrejtaPerRaportPerPerdorues(emerRaporti[i], idPerdoruesi, idNdermarrje, idViti);
                hfState.Set(emerRaporti[i].ToLower(), drejtarap.DAmb);
            }
            
        }


        /// <summary>
        /// konfiguron griden
        /// </summary>
        /// <param name="komponente"></param>
        /// <param name="veprimi"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="idPerdoruesi"></param>
        /// <param name="ci"></param>
        private void konfiguroGride(string komponente, string veprimi, int idNdermarrje, int idPerdoruesi, int idGjuha, ResourceManager rm, CultureInfo ci)
        {
            if (grid_RegDok.Columns.Count != 0)
            {
                grid_RegDok.Columns["#"].VisibleIndex = 0;
            }
            grid_RegDok.SettingsPager.PageSize = 20;
            var idKategoria = (veprimi == "shitje" || veprimi == "shitjediscount" || veprimi == "bazaar") ? 1 : 2;
            KonfigurimComboGride.ShtoNivel(grid_RegDok, idKategoria, idNdermarrje, idPerdoruesi, idGjuha, Session, komponente, guidString);
            KonfigurimComboGride.ShtoModel(grid_RegDok, idKategoria, idNdermarrje, idPerdoruesi, idGjuha, Session, komponente, guidString);
            KonfigurimComboGride.ShtoMonedhe(grid_RegDok, idNdermarrje, idPerdoruesi, Session, komponente, guidString);
            KonfigurimComboGride.shto_DegeAdministrative(grid_RegDok, idNdermarrje, Session, komponente, guidString, "IdDegeAdministrative");
            KonfigurimComboGride.shto_Operatore(grid_RegDok, idNdermarrje, Session, komponente, guidString);
            KonfigurimComboGride.ShtoGrupimDokumentashMeDataSource(grid_RegDok, () =>
            {
                var dt = colGrupimDokumentiKoka.merrGrupeSipasGrupitDtSmall(1, idNdermarrje, idPerdoruesi);
                dt.Rows.InsertAt(dt.NewRow(), 0);
                return dt;
            }, Session, komponente, guidString, "IdGrup1", 1);


            KonfigurimComboGride.ShtoStatus(grid_RegDok, rm, ci);
            KonfigurimComboGride.ShtoPikeShitjeFurnizim(grid_RegDok, idNdermarrje, Session, komponente, guidString, "IdPikeShitjeFurnizimi", veprimi);
            KonfigurimComboGride.ShtoTransportues(grid_RegDok, idNdermarrje, Session, komponente, guidString, "IdTransportues");
            KonfigurimComboGride.ShtoStatusAprovimi(grid_RegDok, rm, ci);
            KonfigurimComboGride.ShtoStatusMarreveshje(grid_RegDok);
            KonfigurimComboGride.ShtoStatusStransferimi(grid_RegDok, rm, ci);
            KonfigurimComboGride.ShtoLlojiKlientFurnitor(grid_RegDok, rm, ci);
            KonfigurimComboGride.ShtoProdhuar(grid_RegDok, rm, ci);
            KonfigurimComboGride.ShtoMenyrePagese(grid_RegDok, rm, ci);

            //// shtoAutomjet(idNdermarrje);
            GridUtil.konfigGrideListeEMadhePaTheme(grid_RegDok, "IdShitjeKoka");
            GridViewDataTextColumn col3 = grid_RegDok.Columns["TotaliMeZbritjeMeTVSH"] as GridViewDataTextColumn;
            col3.PropertiesEdit.DisplayFormatString = "#,#0.00";
            GridViewDataDateColumn colDtDok = grid_RegDok.Columns["DtDok"] as GridViewDataDateColumn;
            colDtDok.PropertiesEdit.DisplayFormatString = "dd/MM/yyyy";
            colDtDok.PropertiesDateEdit.EditFormatString = "dd/MM/yyyy";
            GridViewDataTextColumn col1 = grid_RegDok.Columns["Tvsh"] as GridViewDataTextColumn;
            col1.PropertiesEdit.DisplayFormatString = "#,#0.00";
            GridViewDataTextColumn col4 = grid_RegDok.Columns["VleraMbetur"] as GridViewDataTextColumn;
            col4.PropertiesEdit.DisplayFormatString = "#,#0.00";
            GridViewDataTextColumn col5 = grid_RegDok.Columns["Kursi"] as GridViewDataTextColumn;
            col5.PropertiesEdit.DisplayFormatString = "#,#0.00";
        }

        /// <summary>
        /// kur grida ben databound per ti shtuar kolonen e selektimit
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void grid_RegDok_DataBound(object sender, EventArgs e)
        {
            ASPxGridView grid = (ASPxGridView)sender;
            if (grid.Columns["#"] == null)
            {
                GridViewCommandColumn check = new GridViewCommandColumn("#") { ShowSelectCheckbox = true, Width = System.Web.UI.WebControls.Unit.Percentage(2) };
                grid.Settings.ShowFilterRow = true;
                grid.Settings.ShowHeaderFilterButton = true;
                grid.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                grid.Settings.ShowFilterRowMenu = true;
                grid.Columns.Add(check);
                grid.Settings.ShowGroupPanel = true;
                grid.KeyFieldName = "IdShitjeKoka";
                grid.SettingsBehavior.AllowSelectByRowClick = true;
                grid.SettingsBehavior.AllowFocusedRow = true;
            }
            if (!IsPostBack)
                grid.FilterExpression = filterExpressionDefault;
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
            int idPerdoruesi = (int)hfState["idPerdoruesi"];
            int idNdermarrje = (int)hfState["idNdermarrje"];
            int idGjuha = (int)hfState["idGjuha"];
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuha, "grid_RegDok", "RegjistrimDokumentash.aspx", idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                filtra.IdPerdoruesi = idPerdoruesi;
                mesazh = filtra.fshi();
                //mbushComboBoxFiltra();
                clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idNdermarrje, "grid_RegDok", int.Parse(cmbKonfigurimi.Value.ToString()), "RegjistrimDokumentash.aspx");
                int idViti = (int)hfState["idViti"];
                percaktoTemplateMenu(idGjuha, ASPxMenu1, idViti, idPerdoruesi, idNdermarrje, (bool)hfState["eshteMeme"]);
                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                cmbFiltra.Text = "";
                grid_RegDok.FilterExpression = filterExpressionDefault;
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
            int idNdermarrje = (int)hfState["idNdermarrje"];
            int idPerdoruesi = (int)hfState["idPerdoruesi"];
            int idGjuha = (int)hfState["idGjuha"]; int idfiltri = 0;
            DbCore.clsMesazh mesazh = GridUtil.ruajFiltra(idNdermarrje, idPerdoruesi, idGjuha, "grid_RegDok", "RegjistrimDokumentash.aspx", cmbFiltra.Text, grid_RegDok.FilterExpression, grid_RegDok, "IdNivel", int.Parse(cmbKonfigurimi.Value.ToString()), out idfiltri);
            //mbushComboBoxFiltra();
            int idViti = (int)hfState["idViti"];
            clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idNdermarrje, "grid_RegDok", int.Parse(cmbKonfigurimi.Value.ToString()), "RegjistrimDokumentash.aspx");
            percaktoTemplateMenu(idGjuha, ASPxMenu1, idViti, idPerdoruesi, idNdermarrje, (bool)hfState["eshteMeme"]);
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
            int idGjuha = (int)hfState["idGjuha"];
            CultureInfo cultinf = MessagesResource.KtheCultureInfo(idGjuha);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            DataTable err = new DataTable();
            err.Columns.Add("Kodi");
            err.Columns.Add("Gabimi");
            err.Columns.Add("Rreshti");
            int nrreshta = 0;
            int idNdermarrjeOwn = clsNdermarrje.merrIdNdermarrjeOwn();

            switch (e.Item.Name)
            {
                case "PrintPreview":
                    string idKoka = string.Empty, nrDok = string.Empty, idDesign = string.Empty, pershkrim = string.Empty, idstatusdok = string.Empty;
                    List<string> paDesign = new List<string>();
                    if (grid_RegDok.FocusedRowIndex > -1)
                    {
                        idKoka = grid_RegDok.GetRowValues(grid_RegDok.FocusedRowIndex, "IdShitjeKoka").ToString();
                        nrDok = grid_RegDok.GetRowValues(grid_RegDok.FocusedRowIndex, "NrDok").ToString();
                        pershkrim = grid_RegDok.GetRowValues(grid_RegDok.FocusedRowIndex, "PershkrimKonfigDokumenti").ToString().ToLower();
                        idstatusdok = grid_RegDok.GetRowValues(grid_RegDok.FocusedRowIndex, "IdStatusDok").ToString();
                        idDesign = grid_RegDok.GetRowValues(grid_RegDok.FocusedRowIndex, "IdRaportDesing").ToString();
                    }

                    if ((pershkrim.Contains("vodafone one") && idstatusdok == "0") || pershkrim.Contains("porosi bazaar") || pershkrim.Contains("porosi summer promo"))
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Nuk mund te printoni porosi!", pnlMesazhi);
                        return;
                    }

                    int idShitjeKoka = 0, idRapDesign = 0;
                    int.TryParse(idKoka, out idShitjeKoka);
                    int.TryParse(idDesign, out idRapDesign);

                    List<object> rreshtatKoka = grid_RegDok.GetSelectedFieldValues("IdShitjeKoka", "NrDok", "IdRaportDesing");
                    if (rreshtatKoka.Count < 1 && idShitjeKoka > 0)
                        rreshtatKoka.Add(new object[3] { idShitjeKoka, nrDok, idRapDesign });

                    if (rreshtatKoka.Count < 1)
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("regjMagMesazhZgjidhniFaturePerPrintim", cultinf), pnlMesazhi);
                        return;
                    }
                    else if (idRapDesign == 0)
                    {

                        foreach (object[] item in rreshtatKoka)
                            if (Convert.ToString(item[2]) != string.Empty && Convert.ToString(item[2]) != "0")
                            {
                                idShitjeKoka = Convert.ToInt32(item[0]);
                                idRapDesign = Convert.ToInt32(item[2]);
                                break;
                            }
                            else
                                paDesign.Add(Convert.ToString(item[1]));

                        if (idShitjeKoka > 0 && idRapDesign == 0)
                        {
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, string.Format(rm.GetString("regjDokumentaMesazhSkaFormatPerPrintim", cultinf), string.Join(",", paDesign)), pnlMesazhi);
                            return;
                        }
                    }


                    int idRaporti = DbCore.DbShare.clsRaporti.KtheIdRaporti(idGjuha, idRapDesign);
                    clsRaporti raporti = new clsRaporti(IdGjuha, idRaporti);

                    int nrRreshtaOk = 0;
                    clsMesazh mesazhG = clsFunksione.ruajTeDhenaRaportiPerHapjeRaportiTeShpejte(rreshtatKoka, raporti.RaportiEmriReal, out nrRreshtaOk, Session);
                    if (!mesazhG)
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, string.Format(rm.GetString("regjDokumentaMesazhSkaFormatPerPrintim", cultinf), mesazhG.PershkrimMesazhi), pnlMesazhi);

                    grid_RegDok.JSProperties["cpHapFaqe"] = $"RaportiShpejte.aspx?Sesioni=false&idraporti={idRaporti}&idDokumenti={idShitjeKoka}&printo=0&raportdyte=jo&iddesign={idRapDesign}";

                    if (rreshtatKoka.Count == 1)
                    {
                        colGaranciArtikulli garancia = new colGaranciArtikulli(idShitjeKoka);
                        if (garancia.Count > 0)
                        {
                            int idrapgarancia = 142;
                            grid_RegDok.JSProperties["cpHapFaqe1"] = $"RaportiShpejte.aspx?Sesioni=false&idraporti={idrapgarancia}&idDokumenti={idShitjeKoka}&printo=0&raportdyte=po";

                        }
                    }

                    konfiguroGride(DbCore.clsFunksione.GetKomponente(Page.Request), (string)hfState["veprimi"], (int)hfState["idNdermarrje"], (int)hfState["idPerdoruesi"], idGjuha, rm, cultinf);
                    break;
                case "Trasfero":
                    string[] fusha = { "IdShitjeKoka", "IdNivel", "StatusTransferimi", "IdKlientFurnitor", "KartaPaPagese", "IdGrup1" };
                    List<object> rreshtat = grid_RegDok.GetSelectedFieldValues(fusha);

                    List<int> shitjepertrasferim = new List<int>();
                    List<int> shitjepertrasferimKPP = new List<int>();
                    List<int> shitjepertrasferimOwn = new List<int>();
                    int i = 0;
                    int rreshtaOk = 0, rreshtaJoOk = 0;
                    foreach (object id in rreshtat)
                    {

                        if (int.Parse(((object[])(rreshtat[i]))[2].ToString()) == (int)DbCore.DbRegjistrim.StatusTrasferimi.PaTransferuar)
                        {
                            int idshitjekoka = Convert.ToInt32(((object[])(rreshtat[i]))[0]);
                            string kodniveli = DbCore.DbRegjistrim.clsNivelRegjistrimi.ktheKodNivelRegjistrimi(int.Parse(((object[])(rreshtat[i]))[1].ToString()));
                            bool own = clsKlientFurnitor.eshteNdermarrjeKlientiOwn(int.Parse(((object[])(rreshtat[i]))[3].ToString()));
                            if (kodniveli == "USH")
                            {

                                bool mungojneEntitete = clsKokaShitje.mungojneEntiteteNeNdermarrje(idNdermarrjeOwn, err, ref nrreshta, idshitjekoka);
                                if (mungojneEntitete)
                                {
                                    rreshtaJoOk++;
                                    nrreshta++;
                                }
                                else
                                {
                                    rreshtaOk++;
                                    if (own)
                                        shitjepertrasferimOwn.Add(idshitjekoka);
                                    else
                                    {
                                        clsGrupimDokumentiKoka grupim = new clsGrupimDokumentiKoka(int.Parse(((object[])(rreshtat[i]))[5].ToString()));
                                        if (bool.Parse(((object[])(rreshtat[i]))[4].ToString()) && grupim.Kodi.EqualsIgnoreCase("KARTA"))
                                            shitjepertrasferimKPP.Add(idshitjekoka);
                                        else
                                            shitjepertrasferim.Add(idshitjekoka);
                                    }
                                }
                            }
                        }
                        i++;
                    }
                    if (shitjepertrasferim.Count == 0 && shitjepertrasferimOwn.Count == 0 && shitjepertrasferimKPP.Count == 0)
                    {
                        if (err.Rows.Count == 0)
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("regjisDokMesazhAsnjeDokPerTransferim", cultinf), pnlMesazhi);
                        else
                        {
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "U transferuan 0 dokumenta dhe deshtuan " + rreshtaJoOk + " dokumenta! ", pnlMesazhi);
                            DbCore.mySessionObjects.ruajTabeleGabimeshImporti(Session, err);
                            grid_RegDok.JSProperties["cpHapFaqe"] = "RaportiShpejte.aspx?Sesioni=false&emriReal=gabimeImporti&printo=0&db=jo";
                        }
                    }
                    else
                    {
                        int idPerdoruesi = (int)hfState["idPerdoruesi"];
                        int idNdermarrje = (int)hfState["idNdermarrje"];
                        clsMesazh mesazh = new clsMesazh(true, "Transferimi perfundoi me sukses!");



                        if (shitjepertrasferim.Count > 0)
                        {
                            mesazh = clsFunksione.eksportAutomatikDokumentesh(clsServerConfiguration.LexoKonfigurimSipasKey<string>(ServerKonfigKey.FORMAT_EKSPORTI_TRANSFER_USH), idNdermarrje, idPerdoruesi, shitjepertrasferim, true);
                            nrreshta += shitjepertrasferim.Count;
                        }

                        if (!mesazh)
                        {
                            rreshtaJoOk += shitjepertrasferim.Count;
                            rreshtaOk += shitjepertrasferim.Count;
                            clsFunksione.ShtoNeTabeleGabimesh(err, DateTime.Now.ToShortDateString(), mesazh.PershkrimMesazhi, 0);
                        }

                        if (shitjepertrasferimKPP.Count > 0)
                        {

                            mesazh = clsFunksione.eksportAutomatikDokumentesh(clsServerConfiguration.LexoKonfigurimSipasKey<string>(ServerKonfigKey.FORMAT_EKSPORTI_TRANSFER_USH_KPP), idNdermarrje, idPerdoruesi, shitjepertrasferimKPP, true);
                            nrreshta += shitjepertrasferimKPP.Count;
                        }

                        if (!mesazh)
                        {
                            rreshtaJoOk += shitjepertrasferim.Count;
                            rreshtaOk += shitjepertrasferim.Count;
                            clsFunksione.ShtoNeTabeleGabimesh(err, DateTime.Now.ToShortDateString(), mesazh.PershkrimMesazhi, 0);
                        }

                        if (shitjepertrasferimOwn.Count > 0)
                        {
                            int nrOk = 0;
                            clsKokaShitje.KonvertoDokumentaShitjeNeDokMagazine(shitjepertrasferimOwn, IdPerdoruesi, err, ref nrreshta, idNdermarrjeOwn, ref nrOk, new DbData(), true);
                            rreshtaJoOk += (shitjepertrasferimOwn.Count - nrOk);
                            rreshtaOk -= (shitjepertrasferimOwn.Count - nrOk);
                        }
                        if (err.Rows.Count > 0)
                        {
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "U transferuan " + rreshtaOk + " dokumenta dhe deshtuan " + rreshtaJoOk + " dokumenta! ", pnlMesazhi);
                            DbCore.mySessionObjects.ruajTabeleGabimeshImporti(Session, err);
                            grid_RegDok.JSProperties["cpHapFaqe"] = "RaportiShpejte.aspx?Sesioni=false&emriReal=gabimeImporti&printo=0&db=jo";
                        }
                        else
                        {
                            clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                        }

                        mbushGridNgaDB(komponente, Periudha.PeriudhaDok, (int)hfState["idNdermarrjeVit"], (string)hfState["veprimi"], idNdermarrje, idPerdoruesi, hfTeDrejtaGjitheDok.Value.ToString() == "True" || hfTeDrejtaGjitheDok.Value.ToString() == "true", Periudha.DataDokNga, Periudha.DataDokDeri);
                    }
                    break;
                case "TransferoM":
                    string[] fushaM = { "IdShitjeKoka", "IdNivel", "StatusTransferimi", "IdStatusDok" };
                    List<object> rreshtatM = grid_RegDok.GetSelectedFieldValues(fushaM);
                    List<int> shitjepertrasferimM = new List<int>();
                    int im = 0;
                    foreach (object id in rreshtatM)
                    {

                        if (int.Parse(((object[])(rreshtatM[im]))[2].ToString()) == (int)DbCore.DbRegjistrim.StatusTrasferimi.PaTransferuar && int.Parse(((object[])(rreshtatM[im]))[3].ToString()) == 1)
                        {
                            int idshitjekoka = Convert.ToInt32(((object[])(rreshtatM[im]))[0]);
                            bool mungojneEntitete = clsKokaShitje.mungojneEntiteteNeNdermarrje(idNdermarrjeOwn, err, ref nrreshta, idshitjekoka);

                            if (mungojneEntitete)
                            {
                                nrreshta++;
                            }
                            else
                            {
                                shitjepertrasferimM.Add(Convert.ToInt32(((object[])(rreshtatM[im]))[0]));
                            }

                        }
                        im++;
                    }
                    if (shitjepertrasferimM.Count == 0)
                    {
                        if (err.Rows.Count == 0)
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("regjisDokMesazhAsnjeDokPerTransferim", cultinf), pnlMesazhi);
                        else
                        {
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "U transferuan 0 dokumenta dhe deshtuan " + err.Rows.Count + " dokumenta! ", pnlMesazhi);
                            DbCore.mySessionObjects.ruajTabeleGabimeshImporti(Session, err);
                            grid_RegDok.JSProperties["cpHapFaqe"] = "RaportiShpejte.aspx?Sesioni=false&emriReal=gabimeImporti&printo=0&db=jo";
                        }
                    }
                    else
                    {
                        int idPerdoruesi = (int)hfState["idPerdoruesi"];
                        int idNdermarrje = (int)hfState["idNdermarrje"];
                        clsMesazh mesazh = clsFunksione.eksportAutomatikDokumentesh(clsServerConfiguration.LexoKonfigurimSipasKey<string>(ServerKonfigKey.FORMAT_EKSPORTI_TRANSFER_KTHIM), idNdermarrje, idPerdoruesi, shitjepertrasferimM, true);
                        if (mesazh.Status)
                            clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                        else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                        string veprimi = (string)hfState["veprimi"];
                        int idNdermarrjeVit = (int)hfState["idNdermarrjeVit"];
                        string komponente = DbCore.clsFunksione.GetKomponente(Page.Request);
                        if (hfTeDrejtaGjitheDok.Value.ToString() == "True" || hfTeDrejtaGjitheDok.Value.ToString() == "true")
                            mbushGridNgaDB(komponente, Periudha.PeriudhaDok, idNdermarrjeVit, veprimi, idNdermarrje, idPerdoruesi, true, Periudha.DataDokNga, Periudha.DataDokDeri);
                        else
                            mbushGridNgaDB(komponente, Periudha.PeriudhaDok, idNdermarrjeVit, veprimi, idNdermarrje, idPerdoruesi, false, Periudha.DataDokNga, Periudha.DataDokDeri);
                    }
                    break;
                case "Riruaj":
                    Riruaj((string)hfState["guidString"], DbCore.clsFunksione.GetKomponente(Page.Request), cultinf, rm, (bool)hfState["eshteMeme"], idGjuha, err);
                    break;
                case "Fiskalizo":
                    if (clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizim())
                        fiskalizo((string)hfState["guidString"], DbCore.clsFunksione.GetKomponente(Page.Request), cultinf, rm, (bool)hfState["eshteMeme"], idGjuha, err);
                    break;
                case "DergoEmail":
                    string[] values = grid_RegDok.MerrVleratERreshtit(grid_RegDok.FocusedRowIndex, "IdShitjeKoka", "NrDok", "DtDok", "IdKlientFurnitor", "IdKonfigAmbjente");
                    int iddok = int.Parse(values[0]);
                    bool dergoemailMag = clsAlternativaKushti.getAlternativa(Convert.ToInt32(values[4]), "DEFM") == "Po";
                    (clsMesazh gabim, clsMesazh sukses) = DbCore.clsFunksione.dergoMesazhKlientitFaturat((int)hfState["idGjuha"], (int)hfState["idPerdoruesi"], (int)hfState["idViti"], (int)hfState["idNdermarrje"], cultinf, iddok, values[1], Convert.ToDateTime(values[2]), Convert.ToInt32(values[3]), Convert.ToInt32(values[4]), (dergoemailMag) ? (clsFunksione.MerrIdMagsTrupiPerDok(iddok)).Rows.Cast<DataRow>().Select(x => Convert.ToInt32(x[0])).ToArray() : new int[] { }, dergoemailMag);
                    if (gabim.PershkrimMesazhi != "")
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, gabim.PershkrimMesazhi, pnlMesazhi);
                    if (sukses.PershkrimMesazhi != "")
                        clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, sukses.PershkrimMesazhi, pnlMesazhi);
                    break;
                default:
                    break;
            }
        }
        protected void fiskalizo(string guidString, string komponente, CultureInfo ci, ResourceManager rm, bool eshteMeme, int idGjuha, DataTable err)
        {
            if(!clsFunksioneFiskalizimi.ktheNeseCertifikataEFiskalizimitKaSkaduar(idNdermarrje))
            {
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                pergjigja.Text = "";

                List<object> rreshtat = grid_RegDok.GetSelectedFieldValues("IdShitjeKoka");
                List<object> rreshtatKodKlienti = grid_RegDok.GetSelectedFieldValues("KodKlientFurnitor");
                List<object> rreshtatOperatori = grid_RegDok.GetSelectedFieldValues("IdOperator");
                List<object> rreshtatNenKategori = grid_RegDok.GetSelectedFieldValues("IdNivel");
                List<object> rreshtatNrDok = grid_RegDok.GetSelectedFieldValues("NrDok");
                clsNdermarrje nderm = new clsNdermarrje(idNdermarrje);
                DataTable error = new DataTable();
                error.Columns.Add("Kodi");
                error.Columns.Add("Gabimi");
                error.Columns.Add("Rreshti");
                clsNivelRegjistrimi nivelRegjistrimi = new clsNivelRegjistrimi("FSH", idNdermarrje);
                //return;
                string filePath = "";
                string zipName = "";
                if (nderm.Fiskalizimi)
                {

                    if (rreshtat.Count > 1)
                    {

                        for (var i = 0; i < rreshtat.Count; i++)
                        {
                            int idShitjeKoke = Int32.Parse(rreshtat[i].ToString());
                            clsKokaShitje kokaShitje = new clsKokaShitje(idShitjeKoke);

                            if (kokaShitje.NIVF != "")
                            {
                                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Nuk mund te ridergoni fatura te fiskalizuara!", pnlMesazhi);
                                return;
                            }
                            if (rreshtatOperatori[0] == System.DBNull.Value)
                            {
                                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ju Lutem Zgjidhni Operatorin Te Faturat!", pnlMesazhi);
                                return;
                            }
                            if (rreshtatNenKategori[i].ToString() != nivelRegjistrimi.IdNivel.ToString())
                            {
                                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ju Lutem Zgjidhni Fatura Shitjeje!", pnlMesazhi);
                                return;
                            }
                            clsKlientFurnitor kF = new clsKlientFurnitor();
                            kF.mbushKlientFurnitorSipasKodit(rreshtatKodKlienti[i].ToString(), nderm.IdNdermarrje);
                            if (kokaShitje.IdDegeAdministrative == null)
                            {
                                error.Rows.Add("Dega administrative", "Plotesoni degen administrative!");
                            }
                            else
                            {
                                var degeAdministrativeKontroll = clsDegeAdministrative.ktheDegeAdministrativeSipasiD(kokaShitje.IdDegeAdministrative);
                                if (degeAdministrativeKontroll["KODNJESIEBIZNES"].ToString() == "")
                                    error.Rows.Add("Dega administrative", "Plotesoni Kodin e njesise se biznesit te dega administrative!");

                            }
                            if (kF.TipiId != "" || kF.AutoNgarkese == true)
                            {
                                if (kF.EmriQytetitKF == "")
                                    error.Rows.Add("Emer Qyteti Klienti", "Vendosni Emrin E Qytetit Te Klientit Per Fiskalizimin!");
                                if (kF.NiptiKF == "")
                                    error.Rows.Add("Nipt Klienti", "Vendosni Nipt-in E Klientit Per Fiskalizimin!");
                            }
                            if (rreshtatOperatori[i].ToString() == "")
                                error.Rows.Add("Operatori", "Vendosni Operatorin Per Fiskalizimin!");
                            if (nderm.NdermarrjeQytetiPershkrimi == "")
                                error.Rows.Add("Emer Qyteti Ndermarrje", "Vendosni Emrin E Qytetit Te Ndermarrjes Per Fiskalizimin!");
                            if (nderm.NdermarrjeNipt == "")
                                error.Rows.Add("Nipt Ndermarrje", "Vendosni Nipt-in e Ndermarrjes Per Fiskalizimin!");
                            if (nderm.NdermarrjeVendi == "")
                                error.Rows.Add("Shtet Ndermarrje", "Vendosni Shtetin e Ndermarrjes Per Fiskalizimin!");
                            if (kokaShitje.NrDok.StartsWith("0"))
                                error.Rows.Add("Numer Dokumenti", "Numri I Dokumentit Nuk Duhet Te Filloj Me 0 Per Fiskalizimin!");
                            DbCore.clsMesazh mesazherror = new DbCore.clsMesazh();
                            clsKokaErrorImporti kokaErr = new clsKokaErrorImporti();

                        }
                        if (error.Rows.Count > 0)
                        {
                            var kokaErrs = new clsKokaErrorImporti(0, "Nga Fiskalizimi ", 1, nderm.IdNdermarrje, nderm.IdPerdoruesi);
                            kokaErrs.ColTrupi.mbushErrorImportiNgaProgrami(error);
                            var mesazherrors = kokaErrs.ruajErrorImporti();
                            DbCore.mySessionObjects.ruajTabeleGabimeshImporti(Session, error);
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ju lutem plotesoni fushat e kerkuara ne listen e gabimeve!", pnlMesazhi);
                            grid_RegDok.JSProperties["cpHapFaqe"] = "RaportiShpejte.aspx?Sesioni=false&emriReal=gabimeImporti&printo=0&db=jo";
                            return;
                        }
                        for (var i = 0; i < rreshtat.Count; i++)
                        {

                            int idShitjeKoke = Int32.Parse(rreshtat[i].ToString());
                            clsKokaShitje kokaShitje = new clsKokaShitje(idShitjeKoke);
                            var degeAdministrative = clsDegeAdministrative.ktheDegeAdministrativeSipasiD(kokaShitje.IdDegeAdministrative);
                            var trupiShitje = kokaShitje.merrTrupShitje();
                            var dateMaturimi = kokaShitje.DtMaturimi.ToString().Split(' ')[0].Replace('/', '-');
                            string[] dateMaturimiList = dateMaturimi.Split(new[] { '-' }, 3);
                            var dateMaturimiFormatuar = $"{dateMaturimiList[2]}-{dateMaturimiList[1]}-{dateMaturimiList[0]}";
                            var arka = new clsBanka(kokaShitje.IdArka);
                            var dataTani = kokaShitje.DtKrijimi;
                            DateTime dataKrijimi = DateTime.UtcNow;
                            var operatori = clsOperator.MerrEmerDheMbiemerOperatoriSipasId(Convert.ToInt32(rreshtatOperatori[i]), nderm.IdNdermarrje);
                            var kodOperatori = clsOperator.MerrKodOperatoriSipasId(Convert.ToInt32(rreshtatOperatori[i]), nderm.IdNdermarrje);
                            string kodSoftueri = WebConfigurationManager.AppSettings["kodSoftueri"];
                            string menyrePagese = "";
                            switch (kokaShitje.IdMenyrePagese)
                            {
                                case 0:
                                    menyrePagese = "Me_mirebesim";
                                    break;
                                case 4:
                                    menyrePagese = "Pagese";
                                    break;
                                case 5:
                                    menyrePagese = "Pagese Automatike";
                                    break;
                                case 6:
                                    menyrePagese = "Cash_1_Bank";
                                    break;
                                case 8:
                                    menyrePagese = "Arke";
                                    break;
                                case 9:
                                    menyrePagese = "Karte krediti";
                                    break;
                                case 10:
                                    menyrePagese = "Pezull";
                                    break;
                                case 11:
                                    menyrePagese = "Banke";
                                    break;
                            }
                            var iic = clsFunksione.GjeneroIIC(nderm, rreshtat[i].ToString(), kokaShitje.Totali.ToString(), "ur271so291", kodSoftueri);
                            if (iic == "Ju lutem ngarkoni filen e passwordit!")
                            {
                                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ju lutem ngarkoni filen e passwordit!", pnlMesazhi);
                                return;
                            }
                            else if (iic == "Ju lutem ngarkoni certifikaten e sigurise!")
                            {

                                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ju lutem ngarkoni certifikaten e sigurise!", pnlMesazhi);
                                return;
                            }
                            DateTimeOffset dtKrijimiPajisjeOffset = new DateTimeOffset(kokaShitje.DtKrijimiPajisje);
                            var timeZone = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");
                            var dtKrijimiOffset = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZone);
                            var iicSignature = clsFunksioneFiskalizimi.ktheIICSignature(nderm, rreshtat[i].ToString(), kokaShitje.Totali.ToString(), "ur271so291", kodSoftueri, dataKrijimi);
                            var viti = new clsViti(Periudha.IdViti).KodiViti;
                            var mesazhInvoice = clsFunksioneFiskalizimi.GjeneroMesazhInvoice(nderm, rreshtatNrDok[0].ToString(), kokaShitje.Totali.ToString(), "ur271so291", kodSoftueri, iic, iicSignature, kokaShitje.AdresaFaturimit.ToString(), kokaShitje.Pershkrimi.ToString(), kokaShitje.Totali.ToString(), dateMaturimiFormatuar, kokaShitje.Totali.ToString(),
                                                kokaShitje.Tvsh.ToString(), kokaShitje.PerqindjeZbritje.ToString(), rreshtatKodKlienti[i].ToString(), "", kokaShitje.Totali.ToString(), "", "", kokaShitje.Pershkrimi, "", trupiShitje, arka.KodiTCR, menyrePagese, nderm, rreshtatKodKlienti[i].ToString(), kokaShitje.Zbritje.ToString(), kokaShitje.Totali.ToString(),
                                                "", Convert.ToDateTime(kokaShitje.DtDok.ToString().Split(' ')[0] + ' ' + dtKrijimiPajisjeOffset.UtcDateTime.ToString().Split(' ')[1]), operatori.ItemArray[0].ToString() + " " + operatori.ItemArray[1].ToString(), true, kokaShitje.Kursi.ToString(), false, degeAdministrative["KODNJESIEBIZNES"].ToString(), kokaShitje.TipiIVetefaturimit, kodOperatori.ItemArray[0].ToString(), true, true, kokaShitje.Dogana, kokaShitje.DtMbarimi.ToString(), kokaShitje.DtFillimi.ToString(), kokaShitje.DtDok.ToString(), dtKrijimiOffset, idNdermarrje, IdPerdoruesi, viti);
                            if (i == 0)
                            {
                                zipName = DateTime.Now.ToString("yyyyMMddHHmmss");
                                filePath = clsFunksioneFiskalizimi.ruajZipFatura(mesazhInvoice[0], iic, zipName, false, Response);

                            }
                            else if (i > 0)
                            {

                                filePath = clsFunksioneFiskalizimi.ruajZipFatura(mesazhInvoice[0], iic, zipName, true, Response);
                            }
                        }
                    }
                    else
                    {
                        string urlFiskalizimi = WebConfigurationManager.AppSettings["urlFiskalizimi"];

                        if (urlFiskalizimi == null)
                        {
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Fiskalizimi nuk pergjigjet!", pnlMesazhi);
                            return;
                        }
                        if (rreshtatOperatori[0] == System.DBNull.Value)
                        {
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ju Lutem Zgjidhni Operatorin Te Faturat!", pnlMesazhi);
                            return;
                        }

                        int idShitjeKoke = Int32.Parse(rreshtat[0].ToString());
                        clsKokaShitje kokaShitje = new clsKokaShitje(idShitjeKoke);
                        if (kokaShitje.NIVF != "")
                        {
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Fatura eshte e fiskalizuar!", pnlMesazhi);
                            return;
                        }
                        var degeAdministrative = clsDegeAdministrative.ktheDegeAdministrativeSipasiD(kokaShitje.IdDegeAdministrative);
                        var trupiShitje = kokaShitje.merrTrupShitje();
                        var dateMaturimi = kokaShitje.DtMaturimi.ToString().Split(' ')[0].Replace('/', '-');
                        string[] dateMaturimiList = dateMaturimi.Split(new[] { '-' }, 3);
                        var dateMaturimiFormatuar = $"{dateMaturimiList[2]}-{dateMaturimiList[1]}-{dateMaturimiList[0]}";
                        var operatori = clsOperator.MerrEmerDheMbiemerOperatoriSipasId(Convert.ToInt32(rreshtatOperatori[0]), nderm.IdNdermarrje);
                        var arka = new clsBanka(kokaShitje.IdArka);
                        var dataTani = kokaShitje.DtKrijimi;
                        string menyrePagese = "";
                        switch (kokaShitje.IdMenyrePagese)
                        {
                            case 0:
                                menyrePagese = "Me_mirebesim";
                                break;
                            case 4:
                                menyrePagese = "Pagese";
                                break;
                            case 5:
                                menyrePagese = "Pagese Automatike";
                                break;
                            case 6:
                                menyrePagese = "Cash_1_Bank";
                                break;
                            case 8:
                                menyrePagese = "Arke";
                                break;
                            case 9:
                                menyrePagese = "Karte krediti";
                                break;
                            case 10:
                                menyrePagese = "Pezull";
                                break;
                            case 11:
                                menyrePagese = "Banke";
                                break;
                        }
                        DateTime dataKrijimit = DateTime.UtcNow;
                        string kodSoftueri = WebConfigurationManager.AppSettings["kodSoftueri"];
                        var kodOperatori = clsOperator.MerrKodOperatoriSipasId(Convert.ToInt32(rreshtatOperatori[0]), nderm.IdNdermarrje);
                        string iic = "";
                        if (kokaShitje.IIC == "")
                        {
                            iic = clsFunksione.GjeneroIIC(nderm, rreshtatNrDok[0].ToString(), kokaShitje.Totali.ToString(), "ur271so291", kodSoftueri);
                            if (iic == "Ju lutem ngarkoni filen e passwordit!")
                            {
                                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ju lutem ngarkoni filen e passwordit!", pnlMesazhi);
                                return;
                            }
                            else if (iic == "Ju lutem ngarkoni certifikaten e sigurise!")
                            {

                                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ju lutem ngarkoni certifikaten e sigurise!", pnlMesazhi);
                                return;
                            }
                        }
                        else
                            iic = kokaShitje.IIC;
                        DateTimeOffset dtKrijimiPajisjeOffset = new DateTimeOffset(kokaShitje.DtKrijimiPajisje);
                        var timeZone = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");
                        var dtKrijimiOffset = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZone);
                        var iicSignature = clsFunksioneFiskalizimi.ktheIICSignature(nderm, rreshtatNrDok[0].ToString(), kokaShitje.Totali.ToString(), "ur271so291", kodSoftueri, dataKrijimit);
                        var viti = new clsViti(Periudha.IdViti).KodiViti;
                        var mesazhInvoice = clsFunksioneFiskalizimi.GjeneroMesazhInvoice(nderm, rreshtatNrDok[0].ToString(), kokaShitje.Totali.ToString(), "ur271so291", kodSoftueri, iic, iicSignature, kokaShitje.AdresaFaturimit.ToString(), kokaShitje.Pershkrimi.ToString(), kokaShitje.Totali.ToString(), dateMaturimiFormatuar, kokaShitje.Totali.ToString(),
                            kokaShitje.Tvsh.ToString(), kokaShitje.PerqindjeZbritje.ToString(), rreshtatKodKlienti[0].ToString(), "", (kokaShitje.Totali - kokaShitje.Tvsh).ToString(), "", "", kokaShitje.Pershkrimi, "", trupiShitje, arka.KodiTCR, menyrePagese, nderm, rreshtatKodKlienti[0].ToString(), kokaShitje.Zbritje.ToString(), kokaShitje.Totali.ToString(),
                            kokaShitje.NivfKthim, Convert.ToDateTime(kokaShitje.DtDok.ToString().Split(' ')[0] + ' ' + dtKrijimiPajisjeOffset.UtcDateTime.ToString().Split(' ')[1]), operatori.ItemArray[0].ToString() + " " + operatori.ItemArray[1].ToString(), false, kokaShitje.Kursi.ToString(), false, degeAdministrative["KODNJESIEBIZNES"].ToString(), kokaShitje.TipiIVetefaturimit, kodOperatori.ItemArray[0].ToString(), true, true, kokaShitje.Dogana, kokaShitje.DtMbarimi.ToString(), kokaShitje.DtFillimi.ToString(), kokaShitje.DtDok.ToString(), dtKrijimiOffset, idNdermarrje, IdPerdoruesi, viti);
                        if (error.Rows.Count > 0)
                        {
                            var kokaErrs = new clsKokaErrorImporti(0, "Nga Fiskalizimi ", 1, nderm.IdNdermarrje, nderm.IdPerdoruesi);
                            kokaErrs.ColTrupi.mbushErrorImportiNgaProgrami(error);
                            var mesazherrors = kokaErrs.ruajErrorImporti();
                            DbCore.mySessionObjects.ruajTabeleGabimeshImporti(Session, error);
                            Container1.Attributes["src"] = "RaportiShpejte.aspx?Sesioni=false&emriReal=gabimeImporti&printo=0&db=jo";
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ju lutem plotesoni fushat e kerkuara ne listen e gabimeve!", pnlMesazhi);
                            return;
                        }
                        var nivfFature = clsFunksioneFiskalizimi.InvokeService(mesazhInvoice[0], "FIC", false);
                        if (nivfFature[1] != null)
                        {
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ndodhi nje gabim me fiskalizimin, fatura nuk u fiskalizua!" + $"Error:{nivfFature[1]}" + $" Pershkrimi i errorit:{nivfFature[0]}", pnlMesazhi);
                            clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "Fisaklizimi u krye me sukses!", pnlMesazhi);
                            //var objekti = ktheObjektPerNotify(kokaShitje, false, "Deshtim", clsKokaShitje.merrTrupiShitje(kokaShitje.IdShitjeKoka), new clsTrupiShitje(), nivfFature[0], nivfFature[1], "Fature Fiskalizimi");
                            return;
                        }
                        else
                        {
                            kokaShitje.NIVF = nivfFature[0];
                            kokaShitje.IIC = iic;
                            string serverUrl = clsFunksione.ktheServerUrl(Request);
                            clsKonfigurimAmbjenti konfig = new clsKonfigurimAmbjenti(kokaShitje.IdKonfigAmbjente);
                            kokaShitje.Riruaj(false, idNdermarrje, idPerdoruesi, konfig, idGjuha, eshteMeme, eshteOwn, rm, ci, hfArkiva, serverUrl);
                            clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "Fisaklizimi u krye me sukses!", pnlMesazhi);
                            //var objekti = ktheObjektPerNotify(kokaShitje, false, "Sukses", clsKokaShitje.merrTrupiShitje(kokaShitje.IdShitjeKoka), new clsTrupiShitje(), nivfFature[0], nivfFature[1], "Fature Fiskalizimi");
                            //clsFunksioneFiskalizimi.dergoWebhookNotify(objekti, false);
                        }

                    }
                }
                else
                {
                    clsMenuInfo.ShtoMesazhInformues(MenuInfo, "Ndermarrja nuk ka aplikuar fiskalizimin!", pnlMesazhi);
                    return;
                }


                bool teDrejtaGjitheDok = hfTeDrejtaGjitheDok.Value.ToString().ToLower() == "true";
                grid_RegDok.Selection.UnselectAll();
                mbushGridNgaDB(komponente, Periudha.PeriudhaDok, idNdermarrjeVit, veprimi, idNdermarrje, idPerdoruesi, teDrejtaGjitheDok, Periudha.DataDokNga, Periudha.DataDokDeri);

                konfiguroGride(komponente, veprimi, idNdermarrje, idPerdoruesi, idGjuha, rm, ci);
                if (rreshtat.Count > 1)
                {
                    clsFunksioneFiskalizimi.downloadFileToClientZip(filePath, Response, idPerdoruesi);

                }
            }
            

        }
        protected void Riruaj(string guidString, string komponente, CultureInfo ci, ResourceManager rm, bool eshteMeme, int idGjuha, DataTable err)
        {
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            pergjigja.Text = "";

            List<object> rreshtat = grid_RegDok.GetSelectedFieldValues("IdShitjeKoka");
            string serverUrl = DbCore.clsFunksione.ktheServerUrl(Request);
            DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            string veprimi = (string)hfState["veprimi"];
            bool eshteOwn = (bool)hfState["OwnShop"];
            int nrreshta = 0;
            string mesazhmevonshem = "";

            clsTeDrejtaRoli teDrejtaInfo = new clsTeDrejtaRoli();
            Dictionary<string,List<string>> paTeDrejta = new Dictionary<string, List<string>>();
            colNivelRegjistrimi niveleRregjistrimi = new colNivelRegjistrimi();

            foreach (object id in rreshtat)
            {
                nrreshta++;
                DbCore.DbRegjistrim.clsKokaShitje clsKoka = new clsKokaShitje();
                clsKoka.mbushKokaShitjeSipasIDPaTrup(Convert.ToInt32(id));
                clsKonfigurimAmbjenti konfig = new clsKonfigurimAmbjenti(clsKoka.IdKonfigAmbjente);

                if (veprimi.Equals("shitje") || veprimi.Equals("blerje"))
                {
                    int idKatDok = veprimi.Equals("shitje") ? 1 : 2;
                    if (!clsFunksione.kaTeDrejtePerVepriminMeDokumentin(clsKoka.IdNivel, clsKoka.NrDok, teDrejtaInfo, ref niveleRregjistrimi, idKatDok, "DMod", idNdermarrje, idPerdoruesi, idViti, ref paTeDrejta, komponente))
                        continue;
                }

                if (clsKoka.IdStatusDok == 2)
                    continue;

                bool isShitje = (veprimi == "shitje" || veprimi == "shitjediscount" || veprimi == "bazaar");
                if (DbCore.MbylljePeriudhe.PeriodClosing.IsPeriodClosed(clsKoka.DtDok, MyConnectionsManager.GetSelectedConNameServer(), clsKoka.IdNdermarrje, isShitje ? KategoriDokumenti.Shitje : KategoriDokumenti.Blerje, clsKoka.IdKonfigAmbjente))
                {
                    object[] arr = { konfig.KodKonfigAmbjente + " " + clsKoka.NrDok + " " + clsKoka.DtDok.ToShortDateString(), MessagesResource.Messages["msgPeriodIsClosed"], nrreshta };
                    err.Rows.Add(arr);
                    continue;
                }

                mesazh = clsKoka.Riruaj(false, idNdermarrje, idPerdoruesi, konfig, idGjuha, eshteMeme, eshteOwn, rm, ci, hfArkiva, serverUrl);

                if (!mesazh.Status)
                {
                    object[] arr = { konfig.KodKonfigAmbjente + " " + clsKoka.NrDok + " " + clsKoka.DtDok.ToShortDateString(), mesazh.PershkrimMesazhi, nrreshta };
                    err.Rows.Add(arr);
                    continue;
                }
            }
            DbCore.mySessionObjects.ruajTabeleGabimeshImporti(Session, err);
            string mesazhPerTeDrejtat = clsFunksione.ktheMesazhPerTeDrejtatNivelRregjistrimi(paTeDrejta, "Riruaj", rm, ci);
            if (err.Rows.Count > 0)
            {
                clsKokaErrorImporti koka;
                if (Request.QueryString["shitje_blerje"] == "shitje")
                    koka = new clsKokaErrorImporti(0, "Nga riruatja e shitjeve ", 1, idNdermarrje, idPerdoruesi);
                else
                    koka = new clsKokaErrorImporti(0, "Nga riruatja e blerjeve", 2, idNdermarrje, idPerdoruesi);
                koka.ColTrupi.mbushErrorImportiNgaProgrami(err);
                mesazh = koka.ruajErrorImporti();
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "U riruajten " + (rreshtat.Count - err.Rows.Count) + " rreshta dhe deshtuan " + err.Rows.Count + " rreshta! ", pnlMesazhi);
                grid_RegDok.JSProperties["cpHapFaqe"] = "RaportiShpejte.aspx?Sesioni=false&emriReal=gabimeImporti&printo=0&db=jo";
            }
            else if (!String.IsNullOrEmpty(mesazhPerTeDrejtat))
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "U riruajten " + (rreshtat.Count - paTeDrejta.First().Value.Count) + " rreshta dhe deshtuan " + paTeDrejta.First().Value.Count + " rreshta! ", pnlMesazhi);
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhPerTeDrejtat, pnlMesazhi);
            }
            else
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "U riruajten te gjitha rreshtat!", pnlMesazhi);
            grid_RegDok.Selection.UnselectAll();
            bool teDrejtaGjitheDok = hfTeDrejtaGjitheDok.Value.ToString().ToLower() == "true";
            mbushGridNgaDB(komponente,Periudha.PeriudhaDok, idNdermarrjeVit, veprimi, idNdermarrje, idPerdoruesi, teDrejtaGjitheDok, Periudha.DataDokNga, Periudha.DataDokDeri);

            konfiguroGride(komponente, veprimi, idNdermarrje, idPerdoruesi, idGjuha, rm, ci);
            dbAdmin.Dispose();

            if (rreshtat.Count == 0)
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("regjMagMesazhZgjidhniNje", ci), pnlMesazhi);
        }

        /// <summary>
        /// kur grida ben callback
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void grid_RegDok_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            if (e.CallbackName == "COLUMNMOVE" && grid_RegDok.AllColumns[int.Parse(e.Args[0])].Width.Value == 0)
                grid_RegDok.AllColumns[int.Parse(e.Args[0])].Width = System.Web.UI.WebControls.Unit.Percentage(3);
            if (e.CallbackName == "APPLYFILTER" && e.Args[0] == "")
            {
                MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
                ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
                cmbFiltra.Text = "";
            }

            string veprimi = (string)hfState["veprimi"];
            grid_RegDok.SaveFilter((int)hfState["idNdermarrje"], veprimi);

            CultureInfo cultinf = MessagesResource.KtheCultureInfo((int)hfState["idGjuha"]);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            GridUtil.ToolTipButonaveMbiGride(grid_RegDok, cultinf, rm);
        }

        /// <summary>
        /// kur perdoruesi i ben grides callbak per te mare konfigurimin e ri
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void grid_RegDok_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (grid_RegDok.AplikoFilterDefault(e, Convert.ToInt32(cmbKonfigurimi.Value)))
                return;

            string[] arr = e.Parameters.Split(';');
            int idNdermarrje = (int)hfState["idNdermarrje"];
            int idGjuha = (int)hfState["idGjuha"];
            if (e.Parameters == "rilodo")
                if (hfTeDrejtaGjitheDok.Value.ToString() == "True" || hfTeDrejtaGjitheDok.Value.ToString() == "true")
                    mbushGridNgaDB(DbCore.clsFunksione.GetKomponente(Page.Request),Periudha.PeriudhaDok, idNdermarrjeVit, veprimi, idNdermarrje, idPerdoruesi, true, Periudha.DataDokNga, Periudha.DataDokDeri);
                else
                    mbushGridNgaDB(DbCore.clsFunksione.GetKomponente(Page.Request),Periudha.PeriudhaDok, idNdermarrjeVit, veprimi, idNdermarrje, idPerdoruesi, false, Periudha.DataDokNga, Periudha.DataDokDeri);
            if (arr.Length == 1)
            {//rasti kur behet callback nga filtri i periudhes duhet te ngelen filtrat ne gride dhe te mos aplikohet filtri default
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "grid_RegDok", grid_RegDok, cmbKonfigurimi.Text.Split(';')[0], "505", idGjuha, false);
            }
            else
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "grid_RegDok", grid_RegDok, cmbKonfigurimi.Text.Split(';')[0], "505", idGjuha);
            if (arr.Length == 2)
            {
                int idPerdoruesi = (int)hfState["idPerdoruesi"];
                int idNdermarrjeVit = (int)hfState["idNdermarrjeVit"];
                string veprimi = (string)hfState["veprimi"];
                string periudheDok = clsAlternativaKushti.getAlternativa(Convert.ToInt32(cmbKonfigurimi.Value), "SHDPER");
                string datanga, dataderi;
               // clsFunksione.ruajPeriudhatNeHiddenField(IsPostBack, hfState, periudheDok, null, out datanga, out dataderi);
                mbushGridNgaSession(DbCore.clsFunksione.GetKomponente(Page.Request),Periudha.PeriudhaDok, idNdermarrjeVit, veprimi, idNdermarrje, idPerdoruesi, Periudha.DataDokNga, Periudha.DataDokDeri,cmbKonfigurimi.Text);
                GridUtil.AplikoFilterDefault(grid_RegDok, Convert.ToInt32(cmbKonfigurimi.Value));
            }

            if (arr.Length == 3)
            {
                if (arr[2] == "")
                    grid_RegDok.FilterExpression = filterExpressionDefault;
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    //filtra.mbushFiltraGridaSipasFiltraKodi(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuha, "grid_RegDok", "RegjistrimDokumentash.aspx", idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], idNdermarrje, koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        grid_RegDok.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, grid_RegDok);
                    }
                }
            }
            grid_RegDok.Selection.UnselectAll();
        }

        /// <summary>
        /// perdoret per te vendosur karakteristika te griden ne javascript
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void grid_RegDok_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = grid_RegDok.PageIndex;
            e.Properties["cpPageRow"] = grid_RegDok.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = grid_RegDok.VisibleRowCount;
            e.Properties["cpLKDKN"] = (bool)hfKushte["LKDKN"];
        }

        /// <summary>
        /// perdoret per te shtuar menyra filtrimi
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void grid_RegDok_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            CultureInfo ci = MessagesResource.KtheCultureInfo((int)hfState["idGjuha"]);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            string TeGjithe = rm.GetString("GridHeaderFilterFillItemTeGjithe", ci);
            string nga = rm.GetString("GridHeaderFilterFillItemNga", ci);
            switch (e.Column.FieldName)
            {
                case "NrDok":
                case "Pershkrimi":
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
                        break;
                    }
                case "NrSerial":
                    break;
                default:
                    {
                        e.Values.Clear();
                        e.AddValue(TeGjithe, string.Empty, "true");
                        break;
                    }

            }
        }
        protected void grid_RegDok_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if (e.Column.FieldName == "TotaliMeZbritjeMeTVSH")
            {
                double vlera = 0;
                bool parse = Double.TryParse(e.Value.ToString(), out vlera);
                if ((parse && vlera == 0) || !parse)
                    e.Criteria = null;
            }
        }

        protected void ButtonOk2_Click2(object sender, EventArgs e)
        { }

        protected void btnXlsxExport_Click_Hidden(object sender, EventArgs e)
        {
            try
            {
                System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                CultureInfo ci = MessagesResource.KtheCultureInfo((int)hfState["idGjuha"]);
                gridExport.WriteXlsxToResponse(rm.GetString("msgFaturat", ci), true);
            }
            catch (Exception err)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
            }
        }
        protected void btnPdfExport_Click_Hidden(object sender, EventArgs e)
        {
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            CultureInfo ci = MessagesResource.KtheCultureInfo((int)hfState["idGjuha"]);
            GridUtil.ExportPdfFitToPage(gridExport, Response, rm.GetString("msgFaturat", ci));
        }

        protected void radDtDok_PreRender(object sender, EventArgs e)
        {
            ASPxRadioButtonList radDtDok = sender as ASPxRadioButtonList;
            radDtDok.SelectedItem = radDtDok.Items.FindByValue(hfState.Get("Periudha").ToString());
        }

        protected void grid_RegDok_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {
            if (!e.DataColumn.FieldName.ContainsAnyIgnoreCase("ColorVleraMbetur", "Konvertuar")) return;
            switch (e.CellValue.ToString())
            {

                case "gri":
                    e.Cell.BackColor = System.Drawing.Color.LightGray;
                    break;
                case "verdhe":
                    e.Cell.BackColor = System.Drawing.Color.LightYellow;
                    break;
                case "kuqe":
                    e.Cell.BackColor = System.Drawing.Color.OrangeRed;
                    break;
                case "gjelber":
                    e.Cell.BackColor = System.Drawing.Color.LightGreen;
                    break;
                case "bojeqielli":
                    e.Cell.BackColor = System.Drawing.Color.SkyBlue;
                    break;
            }
            e.Cell.Text = string.Empty;
        }
        protected void grid_RegDok_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            //var grida =(ASPxGridView)sender;
            //if (e.RowType != GridViewRowType.Data) return;

            //GridViewDataColumn col1 = grida.Columns["ColorVleraMbetur"] as GridViewDataColumn;
            //col1.DataItemTemplate = new MyGaugeTemplate();

        }

        private object ktheObjektPerNotify(clsKokaShitje kokaShitje, bool einvoice, string statusi, colTrupiShitje artikujt, clsTrupiShitje trupiShitje, string pershkrim, string kodErrori, string TipFature)
        {
            string emerDatabaze = clsKontrollePerFiskalizimin.ktheInitialCatalogTeLoguar();
            clsKokaShitje kokeShitje = new clsKokaShitje(kokaShitje.IdShitjeKoka);
            clsDegeAdministrative degeAdministrative = new clsDegeAdministrative(kokeShitje.IdDegeAdministrative);
            string kodiINjesiseSeBiznesit = clsDegeAdministrative.ktheDegeAdministrativeSipasiD(kokeShitje.IdDegeAdministrative)["KODNJESIEBIZNES"].ToString();
            clsKlientFurnitor klienti = new clsKlientFurnitor(kokeShitje.IdKlientFurnitor);
            clsNdermarrje ndermarrje = new clsNdermarrje(kokeShitje.IdNdermarrje);
            return new
            {
                Statusi = statusi,
                meta = new
                {
                    Tipi = TipFature,
                    Pershkrim = pershkrim,
                    KodErrori = kodErrori,
                    NumerDokumenti = kokeShitje.NrDok,
                    Ndermarrja = new clsNdermarrje(kokeShitje.IdNdermarrje).NdermarrjeKodi,
                    Organizata = emerDatabaze
                },
                Dokumenti = new
                {
                    Koka = new
                    {
                        DateDokumenti = kokeShitje.DtDok,
                        IIC = kokeShitje.IIC,
                        NIVF = kokeShitje.NIVF,
                        NIFVKTHIM = kokeShitje.NivfKthim,
                        EIC = kokeShitje.EIC,
                        Procesi = kokeShitje.ktheKodDhePershkrimProcesi(kokeShitje.Procesi).Rows[0].ItemArray[0].ToString(),
                        TipiEinvoice = kokeShitje.ktheKodDhePershkrimTipiEinvoice(kokeShitje.EInvoiceType).Rows[0].ItemArray[0].ToString(),
                        AdresaEFaturimit = kokeShitje.AdresaFaturimit,
                        Kursi = kokeShitje.Kursi,
                        DegaAdministrative = degeAdministrative.Kodi,
                        kodiINjesiseSeBiznesit = kodiINjesiseSeBiznesit,
                        operatori = clsOperator.MerrKodOperatoriSipasId(kokeShitje.IdOperator, kokeShitje.IdNdermarrje).ItemArray[0].ToString(),
                        KodiTCR = new clsBanka(kokeShitje.IdArka).KodiTCR,
                        EmerMbiemerOperatori = clsOperator.MerrEmerDheMbiemerOperatoriSipasId(kokeShitje.IdOperator, kokeShitje.IdNdermarrje).ItemArray[0].ToString() + " " + clsOperator.MerrEmerDheMbiemerOperatoriSipasId(kokeShitje.IdOperator, kokeShitje.IdNdermarrje).ItemArray[1].ToString(),
                        Klienti = new
                        {
                            Emertimi = klienti.EmertimiKF,
                            Nipt = klienti.NiptiKF,
                            TipiId = klienti.TipiId,
                            AutoNgarkes = klienti.AutoNgarkese,
                            Qyteti = new clsQyteti(klienti.QytetiKF).EmriQyteti,
                            Shteti = klienti.ShtetiKF
                        },
                        Ndermarrje = new
                        {
                            Emertimi = ndermarrje.NdermarrjeKodi,
                            Nipt = ndermarrje.NdermarrjeNipt,
                            Qyteti = new clsQyteti(ndermarrje.NdermarrjeQyteti).EmriQyteti,
                            Vendi = ndermarrje.NdermarrjeVendi
                        }
                    },
                    Trupi = new
                    {
                        Artikujt = artikujt
                    }



                },


            };
        }

    }
}