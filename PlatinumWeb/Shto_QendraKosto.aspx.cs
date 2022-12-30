using DbCore.DbKontabiliteti;
using DevExpress.Web;
using DevExpress.Web.ASPxTreeList;
using PlatinumWeb.Templates;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Resources;
using System.Web.UI;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{
    public partial class Shto_QendraKosto : MyPageBase
    {
        private int idPerdoruesi, idnderviti, idNdermarrje, idViti, idGjuha;
        private const int idStatusDok = 1;
        bool pergjigje=false;
        /// <summary>
        /// perdoret per te vendosur themen e devexpresit faqes
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>

        /// <summary>
        /// thirret kur faqja lodohet. Ne te behet kontrolli nese perdoruesi eshte i loguar ne sistem  dhe nqs jo ridrejtohet tek forma e logimit
        /// thirret inicializimi i konfigurimeve fillestare te faqes
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e"> argumenti</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                Response.Redirect(DbCore.IMBUtils.Paths.defaultLoginPath);
                return;
            }
            idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerdoruesi);
                return;
            }
            idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            idnderviti = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (!Page.IsPostBack)
            {
                EmrateTabeve(rm, cultinf);
                vendosHfMePerkthime(rm, cultinf);
                hfState.Set("idGjuha", idGjuha);
                hfState.Set("idnderviti", idnderviti);
                hfState.Set("idNdermarrje", idNdermarrje);
                ASPxPageControl1.ActiveTabIndex = 0;
                percaktoTemplateMenu(idViti, idPerdoruesi, idNdermarrje, ASPxMenu1);
                konfiguroVleraFillestare(idNdermarrje, rm, cultinf, idGjuha);
                mbushListeBuxhetesh();
                konfiguroBuxhetGride();
                mbushGridNgaDB(idNdermarrje);
                konfiguroGride(idNdermarrje, cmbKonfigurimi.Text.Split(';')[0], 903);
                TreeListUtil.percaktoVisibleColumnsKonf(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, trlQendra, cmbKonfigurimi.Text.Split(';')[0], 903.ToString());
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "Shto_QendraKosto.aspx");
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                ASPxPageControl1.TabPages[1].ClientVisible = !(Request.QueryString["lupe"] == "true");
                ASPxPageControl1.TabPages[2].ClientVisible = !(Request.QueryString["lupe"] == "true");
            }
          
            konfigurimi_Label.Text = MessagesResource.Messages["lblModeli"];
            popupUniversal.HeaderText = MessagesResource.Messages["popupAdministrimiUniversal"];
            AspxWebControlUtils.perkthePopUp(popFshi, MessagesResource.Messages["labelKujdes"], lblMsgbox, MessagesResource.Messages["labelAdministrimiMsgJeniSigurt"], ButtonCancel, MessagesResource.Messages["labelAnullo"]);
            mbushGridNgaSession(idNdermarrje);
            konfiguroGride(idNdermarrje, cmbKonfigurimi.Text.Split(';')[0], 903);
            percaktoTemplateMenu(idViti, idPerdoruesi, idNdermarrje, ASPxMenu1);
            percaktoTemplate();
        }
      
        private void konfiguroBuxhetGride()
        {//konfiguron griden
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            GridUtil.percaktoVisibleColumnsMeWidth(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), gvBuxheti, "gvBuxheti", "Shto_QendraKosto.aspx");
            GridUtil.konfiguroGrideRegjistrimEvogelPaTheme(gvBuxheti, "IdBuxheti", false);
        }

        private void vendosHfMePerkthime(ResourceManager rm, CultureInfo cultinf)
        {
            hfState.Set("msgQKDuhetTeZgjdhni1Qender", rm.GetString("msgQKDuhetTeZgjdhni1Qender", cultinf));
            hfState.Set("msgQKZgjidhniPrindin", rm.GetString("msgQKZgjidhniPrindin", cultinf));
            hfState.Set("msgPoTransferohetTeDhenatShtypniPerseriRuaj", rm.GetString("msgPoTransferohetTeDhenatShtypniPerseriRuaj", cultinf));
            hfState.Set("msgJoVeprimeMeQendrenPrind", rm.GetString("msgJoVeprimeMeQendrenPrind", cultinf));
        }


        /// <summary>
        /// Vendos emrat e tabeve ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateTabeve(ResourceManager rm, CultureInfo cultinf)
        {
            ASPxPageControl1.TabPages[0].Text = rm.GetString("labelAdministrimiTePergjithshme", cultinf);

            ASPxPageControl1.TabPages[1].Text = rm.GetString("MenuItemQendraKosto", cultinf);
            ASPxPageControl1.TabPages[2].Text = rm.GetString("MenuItemBuxhetet", cultinf);
            if (Convert.ToBoolean(System.Web.Configuration.WebConfigurationManager.AppSettings["BuxhetQK"]) == false)
                ASPxPageControl1.TabPages[2].ClientVisible = false;
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
            EventHandler handlerPerPo = new EventHandler(btnPo_Click);
            EventHandler handlerPerJo = new EventHandler(btnJo_Click);
            clsMenuInfo.ShtoMenuItemInfo(this, MenuInfo, handlerPerPo, handlerPerJo);
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, "Shto_QendraKosto.aspx", this, MenuInfo, null, null, handlerPerPo, handlerPerJo, hfShtimModifikim.Value == "modifikim" ? false : true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session), false, false);
            aSPxMenu1.Items.FindByName("OK").Visible = Request.QueryString["lupe"] == "true";
            aSPxMenu1.Items.FindByName("Anullo").Visible = Request.QueryString["lupe"] == "true";
            aSPxMenu1.Items.FindByName("Anullo").Text = DbCore.IMBUtils.Messages.MessagesResource.Messages["MenuItemMbyll"];
        }

        public void btnPo_Click(object sender, EventArgs e)
        {

            pergjigje = true;
            Page.Validate("entries");
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            ruajQenderKosto(rm, cultinf);

        }

        public void btnJo_Click(object sender, EventArgs e)
        {
            pergjigje = false;

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

        /// <summary>
        /// perdoret per te shtuar kolonen e selektimit tek grida dhe per te vendosur disa karakteristika te grides
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void trlQendra_DataBound(object sender, EventArgs e)
        {
            trlQendra.KeyFieldName = "Id";
            trlQendra.ParentFieldName = "IdPrindi";
            trlQendra.SettingsSelection.Recursive = false;
            trlQendra.SettingsBehavior.AllowDragDrop = false;
            trlQendra.SettingsEditing.AllowNodeDragDrop = false;
            trlQendra.SettingsBehavior.AutoExpandAllNodes = true;
        }

        /// <summary>
        /// perdoret per te konfiguruar griden. Per te dhenat mbi kolonat qe do te shfaqen nga databaza si dhe percakton disa karakteristika te grides
        /// </summary>
        /// :  <see cref="GridUtil.percaktoVisibleColumns"/>
        /// :  <see cref="DbCore.clsFunksione.konfiguroGrideListeMadhe(grida,celesigrides)"/>
        /// <param name="idNdermarrje">id e ndermarjes</param>
        /// <param name="idKomponente"> id e komponentes</param>
        /// <param name="kodKonfigurimi">kodi i konfigurimit</param>
        private void konfiguroGride(int idNdermarrje, string kodKonfigurimi, int idKomponente)
        {
            bool allowSelection = true;
            if (!String.IsNullOrEmpty(Request.QueryString["lupe"]) && Request.QueryString["lupe"] == "true" && Request.QueryString["vjenNga"] != "raporti")
                allowSelection = false;
            TreeListUtil.konfiguroTreeListeEvogelPaTheme(trlQendra, "Id", "IdPrindi", allowSelection);
        }

        /// <summary>
        /// perdoret per te fshire rreshtat e zgjedhur te bankave nqs perdoruesi konfirmon fshirjen
        /// </summary>
        /// 
        /// <param name="sender"> derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ButtonOk_Click2(object sender, EventArgs e)
        {//fshin rreshtat e selektuar            
            List<TreeListNode> rreshtat = trlQendra.GetSelectedNodes();
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (rreshtat.Count == 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgQKZgjidhni1QK", ci), pnlMesazhi);
                return;
            }
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            //clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "", pnlMesazhi);
            List<string> TeFshire = new List<string>(), TePaFshire = new List<string>();
            DbCore.DbRegjistrim.clsDatabaseRegjistrim dbRegjistrim = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();

            foreach (TreeListNode id in rreshtat)
            {
                DbCore.DbQendraKosto.clsQendraKosto qendra = new DbCore.DbQendraKosto.clsQendraKosto(Convert.ToInt32(id.Key));
                if (qendra.Kodi == "QKP")// nuk duhet te fshihet qendra e kostos qkp
                {
                    TePaFshire.Add(qendra.Kodi);
                    continue;
                }
                DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti() { IdKonfigAmbjente = qendra.IdKonfig };
                konf.mbushKonfigAmbjSipasId(konf.IdKonfigAmbjente);
                bool lidhur = dbRegjistrim.eshteDokumentiILidhurCelje(qendra.Id.ToString(), konf.IdNivel.ToString());
                if (lidhur)
                {
                    TePaFshire.Add(qendra.Kodi);
                    continue;
                }
                qendra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                //mesazh = burim.fshi(null);
                mesazh = qendra.fshi();
                if (qendra.Id == 0)
                    continue;
                if (mesazh.Status)
                {
                    #region Heq llogarite nga grida
                    hiqNgaGrida(qendra.Id, qendra.IdNdermarje, rm, ci);
                    #endregion
                    TeFshire.Add(qendra.Kodi);

                    ASPxPageControl1.ActiveTabIndex = 0;
                    hfStatusi.Value = "true";
                }
            }
            dbRegjistrim.Dispose();
            string mesazhInfoGabim = "", mesazhInfoSukses = "";
            if (TePaFshire.Count == 1)
                mesazhInfoGabim = String.Format("{0}{1}{2}", rm.GetString("msgQKPrefixNjejes", ci), String.Join(";", TePaFshire), rm.GetString("msgShtoLlogariSuffixNjejesGabimi", ci));
            else
                if (TePaFshire.Count > 1)
                    mesazhInfoGabim = String.Format("{0}{1}{2}", rm.GetString("msgQKPrefixShumes", ci), String.Join(";", TePaFshire), rm.GetString("msgShtoLlogariSuffixShumesGabimi", ci));
            if (TeFshire.Count == 1)
                mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("msgQKPrefixNjejes", ci), String.Join(";", TeFshire), rm.GetString("regjMagSuffixMesazhNjejesSuksesi", ci));
            else
                if (TeFshire.Count > 1)
                    mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("msgQKPrefixShumes", ci), String.Join(";", TeFshire), rm.GetString("msgCeljeMagazinatSuffixShumesSuksesi", ci));
            if (mesazhInfoGabim != "" && mesazhInfoSukses != "")
                mesazhInfoGabim += rm.GetString("msgLidhesMesazhi", ci) + mesazhInfoSukses;
            if (mesazhInfoGabim != "")
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhInfoGabim, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazhInfoSukses, pnlMesazhi);

        }

        /// <summary>
        /// heq nga grida rreshtat e fshire
        /// </summary>
        /// <param name="idndermarje">id e ndermarjes</param>
        ///<param name="idburim"> id e burimit</param>
        private void hiqNgaGrida(int idburim, int idndermarje, ResourceManager rm, CultureInfo ci)
        {
            if (trlQendra.DataSource != null)
            {
                DataTable dt = (DataTable)trlQendra.DataSource;
                DataRow[] drs = dt.Select(String.Format("Id = '{0}'", idburim));
                if (drs.Length > 1)
                    throw new DbCore.MyException(rm.GetString("msgQKNdodhen2QKMeTeNjejteIDNeGride", ci));
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                dt.Rows.Remove(dr);
                trlQendra.DataBind();
            }
            else mbushGridNgaDB(idndermarje);
        }

        /// <summary>
        /// shton nte gride rreshtat e rinj
        /// </summary>
        /// <param name="id"> id </param>
        /// <param name="idNdermarrje">id e ndermarjes</param>
        private void shtoNeGrid(int idNdermarrje, int id, ResourceManager rm, CultureInfo ci)
        {
            if (trlQendra.DataSource != null)
            {
                DataTable dt = (DataTable)trlQendra.DataSource;
                DataRow[] drs = dt.Select("Id = " + id);
                if (drs.Length > 0)
                    throw new DbCore.MyException(rm.GetString("msgQKNdodhen2QKMeTeNjejteIDNeGride", ci));
                DataRow newArtDr = DbCore.DbQendraKosto.colQendraKosto.merrQendraKostoDR(id);
                if (newArtDr != null)
                    dt.ImportRow(newArtDr);
                else mbushGridNgaDB(idNdermarrje);
            }
            else mbushGridNgaDB(idNdermarrje);
        }

        /// <summary>
        /// modifikon ne gride komponenten e modifikuar
        /// </summary>
        /// <param name="id"> id </param>
        /// <param name="idNdermarrje">id e ndermarjes</param>
        private void modifikoNeGrid(int idNdermarrje, int id, ResourceManager rm, CultureInfo ci)
        {
            if (trlQendra.DataSource != null)
            {
                DataTable dt = (DataTable)trlQendra.DataSource;
                DataRow[] drs = dt.Select("Id = " + id);
                if (drs.Length > 1)
                    throw new DbCore.MyException(rm.GetString("msgQKNdodhen2QKMeTeNjejteIDNeGride", ci));
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                DataRow newArtDr = DbCore.DbQendraKosto.colQendraKosto.merrQendraKostoDR(id);
                if (newArtDr != null)
                {
                    object[] arr = newArtDr.ItemArray;
                    dr.ItemArray = arr;
                }
                else mbushGridNgaDB(idNdermarrje);
            }
            else mbushGridNgaDB(idNdermarrje);
        }

        /// <summary>
        /// perdoret per te trajtuar ngjarjet e butonave te menuse. 
        /// </summary>
        /// <param name="source"> derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {//kryen veprimet e menuse
            if (e.Item.Name == "Ruaj")
            {
                Page.Validate("entries");
                CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
                ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                ruajQenderKosto(rm, cultinf);
            }
        }

        /// <summary>
        /// konfiguron vlerat fillestare
        /// </summary>
        /// <param name="idNdermarrje">id e ndermarjes</param>
        private void konfiguroVleraFillestare(int idNdermarrje, ResourceManager rm, CultureInfo cultinf, int idGjuha)
        { //mbush komboboxet dhe gridat e faqes
            ASPxPageControl1.ActiveTabIndex = 0;
            //DbCore.clsFunksione.mbushComboQendraKostoPrindKolona(cmbPrindi, rm, cultinf);
            ConfigureAspxComboBox.mbushComboMonedha(idPerdoruesi, idNdermarrje, false, cmbMonedha);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmbPrindi);
            ConfigureAspxComboBox.mbushComboQendraKostoPrind(idNdermarrje, cmbPrindi);
            if (Request.QueryString["lupe"] == "true")
            {
                string vleraQueryString;
                if (Request.QueryString["idKonfigAmbjente"] != null && Request.QueryString["idKonfigAmbjente"] != "")
                    vleraQueryString = Request.QueryString["idKonfigAmbjente"].ToString();
                else
                    vleraQueryString = "";
                string kodKonfi = Request.QueryString["llojLupe"] == "plote" ? "LP/QKPl" : Request.QueryString["llojLupe"] == "prind" ? "LP/QKP" : "LP/QK";
                int idKonfigAmbjenti = DbCore.clsFunksione.getIdKonfigAmbLupa(vleraQueryString, idNdermarrje, kodKonfi);
                if (idKonfigAmbjenti == 0)
                    idKonfigAmbjenti = DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(kodKonfi, idNdermarrje);
                ConfigureAspxComboBox.mbushComboKonfigurimeshSipasIdKonfigurimi(cmbKonfigurimi, idKonfigAmbjenti);
                //cmbKonfigurimi.ClientVisible = false;
            }
            else
                ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 72, rm, cultinf, idGjuha);
            cmbKonfigurimi.SelectedIndex = 0;
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()), idGjuha);
            hfKonffillestar.Value = String.Format("{0};{1}", konf.KodKonfigAmbjente, konf.PershkrimKonfigAmbjente);
        }

        /// <summary>
        /// mbush griden nga sesioni
        /// </summary>
        /// <param name="idNdermarrje">id e ndermarjes</param>
        private void mbushGridNgaSession(int idNdermarrje)
        {
            if ((!String.IsNullOrEmpty(Request.QueryString["lupe"]) && Request.QueryString["lupe"] != "true") || String.IsNullOrEmpty(Request.QueryString["lupe"]))
            {
                DataTable tmpObject;
                bool sukses = DbCore.mySessionObjects.merrGrideNgaSessioni(Session, out tmpObject);
                if (!sukses)
                    mbushGridNgaDB(idNdermarrje);
                else
                {
                    trlQendra.DataSource = tmpObject;
                    trlQendra.DataBind();
                    tmpObject.Dispose();
                }
            }
            else
            {
                Object tmpObject;
                DbCore.mySessionObjects.merrGrideNgaSessioniLupa(Session, out tmpObject);
                if (tmpObject == null)
                    mbushGridNgaDB(idNdermarrje);
                else
                {
                    trlQendra.DataSource = tmpObject;
                    trlQendra.DataBind();
                }
            }
        }

        /// <summary>
        /// mbush griden nga databaza
        /// </summary>
        /// <param name="idNdermarrje"> id e ndermarjes</param>
        private void mbushGridNgaDB(int idNdermarrje)
        {//mbush griden e popupit me te dhena           
            if ((!String.IsNullOrEmpty(Request.QueryString["lupe"]) && Request.QueryString["lupe"] != "true") || String.IsNullOrEmpty(Request.QueryString["lupe"]))
            {
                DataTable dt = DbCore.DbQendraKosto.colQendraKosto.merrQendraKostoDT(idNdermarrje);
                DbCore.mySessionObjects.ruajGrideNeSession(Session, dt);
                trlQendra.DataSource = dt;
                trlQendra.DataBind();
                dt.Dispose();
            }
            else
            {
                DbCore.DbQendraKosto.colQendraKosto col = new DbCore.DbQendraKosto.colQendraKosto();
                if (Request.QueryString["llojLupe"] == "qk")
                {
                    if (Request.QueryString["vjenNga"] != null && (Request.QueryString["vjenNga"] == "PunonjesQK2" || Request.QueryString["vjenNga"] == "PunonjesQK2Import"))
                    {
                        if (Request.QueryString["idPrindi"] != null && Request.QueryString["idPrindi"] != "0")
                            col.mbushQendraSipasPrindit(int.Parse(Request.QueryString["idPrindi"]));
                        else col.mbushGjitheQendraKostoBijSipasNdermarjesAktiv(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    }
                    else col.mbushGjitheQendraKostoBijSipasNdermarjesAktiv(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                }
                else if (Request.QueryString["llojLupe"] == "prind")
                {
                    if (Request.QueryString["vjenNga"] != null && (Request.QueryString["vjenNga"] == "PunonjesQK1" || Request.QueryString["vjenNga"] == "PunonjesQK1Import"))
                        col.ktheGjitheQendraKostoPrindiNiveli1SipasNdermarjesAktiv(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    else col.mbushGjitheQendraKostoPrindiSipasNdermarjesAktiv(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                }
                else
                    col.mbushGjitheQendraKostoSipasNdermarjesAktiv(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                DbCore.mySessionObjects.ruajGrideNeSessionLupa(Session, col);
                trlQendra.DataSource = col;
                trlQendra.DataBind();
            }
        }

        /// <summary>
        /// ruan qender kosto 
        /// </summary>
        private void ruajQenderKosto(ResourceManager rm, CultureInfo ci)
        {
            DbCore.DbQendraKosto.clsQendraKosto qender = new DbCore.DbQendraKosto.clsQendraKosto();
            if (Page.IsValid == false)
                return;
            else
            {
                int idNderViti = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
                try
                {
                    if (txtKodi.Text == "QKP")
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgQKEshteQenderDefaultDheNukModifikohet", ci), pnlMesazhi);
                        hfStatusi.Value = "false";
                        return;
                    }              
                      qender = krijoQender(idNderViti);
                }
                catch (Exception e)
                {
                    NLog.LogManager.GetCurrentClassLogger().Error(e.Message);
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, e.Message, pnlMesazhi);
                    hfStatusi.Value = "false";
                    return;
                }
                bool eshteShtim;
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "Shto_QendraKosto.aspx");

                if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim")
                {
                    if (!tedrejtaInfo.DShtim)
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNukKeniTeDrejta", ci), pnlMesazhi);
                        hfStatusi.Value = "false";
                        return;
                    }
                    //mesazh = burime.ruaj(null);
                    mesazh = qender.ruajQenderKosto(idNderViti,pergjigje);
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
                    DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti() { IdKonfigAmbjente = qender.IdKonfig };
                    konf.mbushKonfigAmbjSipasId(konf.IdKonfigAmbjente);
                    eshteShtim = false;
                    qender.Id = int.Parse(hfId.Value);
                    bool lidhur = dbRegjistrim.eshteDokumentiILidhurCelje(qender.Id.ToString(), konf.IdNivel.ToString());
                    if (lidhur.ToString() != hfLidhur.Value)
                    {
                        mesazh.Status = false;
                        mesazh.PershkrimMesazhi = rm.GetString("msgQKEshteELidhur", ci);
                    }
                    else


                        mesazh = qender.modifikoQenderKosto(idNderViti,pergjigje);
                    dbRegjistrim.Dispose();
                }

                if ((mesazh.PershkrimMesazhi =="Buxhetit 1 e tejkalon buxhetin e zerit prind. Doni te vazhdoni?") && (pergjigje == false))
                {
                    clsMenuInfo.ShtoPyetje(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi, idGjuha);
                    hfStatusi.Value = "false";
                }
                else
                if (mesazh.Status == true)
                {
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["mesazhRuajtjeMeSukses"], pnlMesazhi);
                    if (eshteShtim)
                        shtoNeGrid(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), qender.Id, rm, ci);
                    else //modifikim
                        mbushGridNgaDB(idNdermarrje);
                    hfStatusi.Value = "true";
                }
                else
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                    hfStatusi.Value = "false";
                }
                ASPxPageControl1.ActiveTabIndex = 0;
            }


        }

        /// <summary>
        /// krijon qendren sipas te dhenave
        /// </summary>
        /// <returns> qendra me te dhenat</returns>
        private DbCore.DbQendraKosto.clsQendraKosto krijoQender(int idNderViti)
        {//krijon nje banke sipas te dhenave te futura nga perdoruesi
            int idPrindi = 0;
            int monedha = 0;
            int niveli = 1;
            if (cmbPrindi.Text != "")
            {
                DbCore.DbQendraKosto.clsQendraKosto prind = new DbCore.DbQendraKosto.clsQendraKosto(cmbPrindi.Text, idNdermarrje);
                idPrindi = prind.Id;
                niveli += prind.Niveli;
            }
            monedha = Convert.ToInt32(cmbMonedha.Value);
            colBuxhetet buxhete = new colBuxhetet();
            string veprimi = hfShtimModifikim.Value;
            switch (veprimi)
            {
                case "shtim":
                    buxhete = colBuxhetet.KrijoBuxhetetSipasLlojitTeBuxhetit(hfBuxheti1.Value, hfBuxheti2.Value, dteDateAkt.Date.Date,0,hfBuxhetiShenime.Value);
                    break;
                case "modifikim":
                case "klonim":
                    buxhete = colBuxhetet.KrijoBuxhetetEModifikuara("QendraKosto", int.Parse(hfId.Value), idNderViti, false, hfBuxheti1.Value, hfBuxheti2.Value,dteDateAkt.Date.Date,0, hfBuxhetiShenime.Value, true);
                    break;
            }
            DbCore.DbShare.clsKonfigurimAmbjenti konfig = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konfig.mbushKonfigAmbjSipasKod(cmbKonfigurimi.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            DbCore.DbQendraKosto.clsQendraKosto qender = new DbCore.DbQendraKosto.clsQendraKosto(0, txtKodi.Text, txtEmertimi.Text, monedha, cbAktiv.Checked, konfig.IdKonfigAmbjente, idPrindi, idNdermarrje, idPerdoruesi, idStatusDok, cmbPrindi.Text, cmbMonedha.Text, (veprimi == "shtim" || veprimi == "klonim"), DbCore.mySessionObjects.ktheGjuhe(Session),niveli, buxhete);
            return qender;
        }


        /// <summary>
        /// per filtrimin e llogarise 
        /// </summary>
        /// <param name="source">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void cmbPrindi_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].Contains("cmbPrindi"))
                {
                    ConfigureAspxComboBox.mbushComboQendraKostoPrind(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), cmbPrindi);
                }
            }
        }

        protected void trlQendra_CustomCallback1(object sender, TreeListCustomCallbackEventArgs e)
        {
            string idkomponente = "";
            string kodkonfigurimi = "";

            string[] arr = e.Argument.ToString().Split(';');
            if (arr.Length == 2)//nese eshte zgjedhur nje konfigurim tek combo e konfigurimeve
            {
                kodkonfigurimi = arr[1];
                idkomponente = arr[0];
            }
            else
            {
                idkomponente = e.Argument;
            }
        }

        /// <summary>
        /// perdoret per te shtuar evente kolonave te buxheteve
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvBuxheti_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        { //sherben per te marre me vone vlerat e futura nga perdoruesi me ane te javascriptit

            if (e.RowType == GridViewRowType.Data)
            {
                GridViewDataColumn colBuxh1 = ((ASPxGridView)sender).Columns["Buxheti_1"] as GridViewDataColumn;
                GridViewDataColumn colBuxh2 = ((ASPxGridView)sender).Columns["Buxheti_2"] as GridViewDataColumn;
                GridViewDataColumn colBuxh1Max = ((ASPxGridView)sender).Columns["Buxheti_1Max"] as GridViewDataColumn;
                GridViewDataColumn colBuxh2Max = ((ASPxGridView)sender).Columns["Buxheti_2Max"] as GridViewDataColumn;
                GridViewDataTextColumn colShenime = ((ASPxGridView)sender).Columns["Shenime"] as GridViewDataTextColumn;
                ASPxTextBox txtBuxh1 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, colBuxh1, "txtBox") as ASPxTextBox;
                ASPxTextBox txtBuxh2 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, colBuxh2, "txtBox") as ASPxTextBox;
                ASPxTextBox txtShenime = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, colShenime, "txtBox") as ASPxTextBox;

                if (txtBuxh1 != null && txtBuxh2 != null)
                {
                    txtBuxh1.ClientInstanceName = "textboxBuxh1" + e.VisibleIndex.ToString();
                    txtBuxh2.ClientInstanceName = "textboxBuxh2" + e.VisibleIndex.ToString();
                    txtShenime.ClientInstanceName = "txtShenime" + e.VisibleIndex.ToString();
                    if (e.VisibleIndex != 0)
                    {
                        txtBuxh1.ClientSideEvents.TextChanged = "function(s,e){ ShtoBuxhet1(textboxBuxh1" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + ");}";
                        txtBuxh2.ClientSideEvents.TextChanged = "function(s,e){ShtoBuxhet2(textboxBuxh2" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + ");}";

                    }
                    else
                    {
                        txtBuxh1.ClientSideEvents.TextChanged = "function(s,e){ShtoTotal1(textboxBuxh1" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + ");}";
                        txtBuxh2.ClientSideEvents.TextChanged = "function(s,e){ShtoTotal2(textboxBuxh2" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + ");}";

                    }
                }

            }

        }

        /// <summary>
        /// kur gvBuxheti ben callback per tu mbushur me vlerat per llogarine e zgjedhur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvBuxheti_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            if (hfShtimModifikim.Value == "modifikim" || hfShtimModifikim.Value == "klonim")
            {
                int id = int.Parse(hfId.Value);
                int idNderViti = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
                DataTable dt = DbCore.DbKontabiliteti.colBuxhetet.ktheBuxhetetPerQendratEKostos(id, idNderViti, dteDateAkt.Date,0);
                DataRow r = dt.NewRow();
                decimal shuma1 = 0;
                decimal shuma2 = 0;
                decimal shumaMax1 = 0;
                decimal shumaMax2 = 0;
                if (dt != null && dt.Rows.Count > 0)
                { 
                    for (int i = 0; i < 12; i++)//me vone duhet shtuar edhe gjendja
                    {
                        shuma1 += Convert.ToDecimal(dt.Rows[i]["Buxheti_1"]);
                        shuma2 += Convert.ToDecimal(dt.Rows[i]["Buxheti_2"]);
                        shumaMax1 += Convert.ToDecimal(dt.Rows[i]["Buxheti_1Max"]);
                        shumaMax2 += Convert.ToDecimal(dt.Rows[i]["Buxheti_2Max"]);
                    }
                    r["IdBuxheti"] = -1;
                    r["IdLlojBuxheti"] = 23;
                    r["IdLidhese"] = id;
                    r["Muaj"] = "Total";
                    r["Buxheti_1"] = shuma1;
                    r["Buxheti_1Max"] = shumaMax1;
                    r["Buxheti_2"] = shuma2;
                    r["Buxheti_2Max"] = shumaMax2;
                    r["IdNderViti"] = idNderViti;
                    dt.Rows.InsertAt(r, 0);
                    gvBuxheti.DataSource = dt;
                    gvBuxheti.DataBind();
                }
                else
                    mbushListeBuxhetesh();
            }
            else
                mbushListeBuxhetesh();
        }
        /// <summary>
        /// mbush griden e buxheteve
        /// </summary>
        private void mbushListeBuxhetesh()
        {//mbush griden me te dhena

            DataTable dt = new DataTable();
            dt.TableName = "Buxhetet";

            DataRow row;
            String[] muaj = { "Janar", "Shkurt", "Mars", "Prill", "Maj", "Qershor", "Korrik", "Gusht", "Shtator", "Tetor", "Nentor", "Dhjetor" };
            DataColumn colIdBuxheti = new DataColumn();
            colIdBuxheti.DataType = System.Type.GetType("System.Int32");
            colIdBuxheti.ColumnName = "IdBuxheti";
            colIdBuxheti.AutoIncrement = true;
            dt.Columns.Add(colIdBuxheti);
            DataColumn colIdLlojBuxheti = new DataColumn();
            colIdLlojBuxheti.DataType = System.Type.GetType("System.Int32");
            colIdLlojBuxheti.ColumnName = "IdLlojBuxheti";
            dt.Columns.Add(colIdLlojBuxheti);
            DataColumn colIdLidhese = new DataColumn();
            colIdLidhese.DataType = System.Type.GetType("System.Int32");
            colIdLidhese.ColumnName = "IdLidhese";
            dt.Columns.Add(colIdLidhese);
            DataColumn colMuaji = new DataColumn();
            colMuaji.DataType = System.Type.GetType("System.String");
            colMuaji.ColumnName = "Muaj";
            dt.Columns.Add(colMuaji);
            DataColumn colBuxheti_1 = new DataColumn();
            colBuxheti_1.DataType = System.Type.GetType("System.Decimal");
            colBuxheti_1.ColumnName = "Buxheti_1";
            dt.Columns.Add(colBuxheti_1);
            DataColumn colBuxheti_1Max = new DataColumn();
            colBuxheti_1Max.DataType = System.Type.GetType("System.Decimal");
            colBuxheti_1Max.ColumnName = "Buxheti_1Max";
            dt.Columns.Add(colBuxheti_1Max);
            DataColumn colBuxheti_2 = new DataColumn();
            colBuxheti_2.DataType = System.Type.GetType("System.Decimal");
            colBuxheti_2.ColumnName = "Buxheti_2";
            dt.Columns.Add(colBuxheti_2);
            DataColumn colBuxheti_2Max = new DataColumn();
            colBuxheti_2Max.DataType = System.Type.GetType("System.Decimal");
            colBuxheti_2Max.ColumnName = "Buxheti_2Max";
            dt.Columns.Add(colBuxheti_2Max);
            DataColumn colIdNderViti = new DataColumn();
            colIdNderViti.DataType = System.Type.GetType("System.Int32");
            colIdNderViti.ColumnName = "IdNderViti";
            dt.Columns.Add(colIdNderViti);
            DataRow rowTotal = dt.NewRow();
            DataColumn colShenime = new DataColumn();
            colShenime.DataType = System.Type.GetType("System.String");
            colShenime.ColumnName = "Shenime";
            dt.Columns.Add(colShenime);
            rowTotal["Muaj"] = "Totali";
            rowTotal["Buxheti_1"] = "0";
            rowTotal["Buxheti_1Max"] = "0";
            rowTotal["Buxheti_2"] = "0";
            rowTotal["Buxheti_2Max"] = "0";
            dt.Rows.InsertAt(rowTotal, 0);
            for (int i = 0; i < 12; i++)
            {

                row = dt.NewRow();
                row["Muaj"] = muaj[i];
                row["Buxheti_1"] = "0";
                row["Buxheti_1Max"] = "0";
                row["Buxheti_2"] = "0";
                row["Buxheti_2Max"] = "0";
                dt.Rows.InsertAt(row, i+1);
            }
            gvBuxheti.DataSource = dt;
            gvBuxheti.DataBind();
        }

        protected void btnXlsxExport_Click1(object sender, EventArgs e)
        {
            try
            {
                ASPxTreeListExporter1.WriteXlsxToResponse("Qendrat", true);
            }
            catch (Exception)
            {
            }
        }
        protected void btnPdfExport_Click1(object sender, EventArgs e)
        {
            try
            {
                ASPxTreeListExporter1.WritePdfToResponse("Qendrat", true);
            }
            catch (Exception)
            {
            }
        }

        protected void btnXlsxExport_Click2(object sender, EventArgs e)
        {
            try
            {
                gridExport.WriteXlsxToResponse("Buxhetet", true);
            }
            catch (Exception)
            {
            }
        }
        protected void btnPdfExport_Click2(object sender, EventArgs e)
        {
            try
            {
                gridExport.WritePdfToResponse("Buxhetet", true);
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// percakton templatet per griden e buxheteve
        /// </summary>
        private void percaktoTemplate()
        {// percaktohen tipet e kolonave per griden e buxheteve
            GridViewDataTextColumn col1 = gvBuxheti.Columns["Muaj"] as GridViewDataTextColumn;
            col1.VisibleIndex = 0;
            col1.DataItemTemplate = new MyLabelTemplate();
            GridViewDataTextColumn col3 = gvBuxheti.Columns["Buxheti_1"] as GridViewDataTextColumn;
            col3.VisibleIndex = 1;
            col3.DataItemTemplate = new MyDoubleTemplate(false, 2, "0"); // "0.00");
            col3.PropertiesEdit.DisplayFormatString = "0.00";
            GridViewDataTextColumn col4 = gvBuxheti.Columns["Buxheti_2"] as GridViewDataTextColumn;
            col4.VisibleIndex = 3;
            col4.DataItemTemplate = new MyDoubleTemplate(false, 2, "0"); // "0.00");
            col4.PropertiesEdit.DisplayFormatString = "0.00";
            GridViewDataTextColumn col5 = gvBuxheti.Columns["Buxheti_1Max"] as GridViewDataTextColumn;
            col5.VisibleIndex = 2;
            col5.ReadOnly = true;
            col5.DataItemTemplate = new MyReadOnlyTextTemplate();
            col5.PropertiesEdit.DisplayFormatString = "0.00";
            GridViewDataTextColumn col6 = gvBuxheti.Columns["Buxheti_2Max"] as GridViewDataTextColumn;
            col6.VisibleIndex = 4;
            col6.ReadOnly = true;
            col6.PropertiesEdit.DisplayFormatString = "0.00";
            col6.DataItemTemplate = new MyReadOnlyTextTemplate();
            GridViewDataTextColumn colShenime = gvBuxheti.Columns["Shenime"] as GridViewDataTextColumn;
            colShenime.VisibleIndex = 5;
            colShenime.DataItemTemplate = new MyTextTemplate();
        }
    }
}