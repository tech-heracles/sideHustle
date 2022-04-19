using DbCore.DbAdmin;
using DevExpress.Web;
using DevExpress.Web.ASPxTreeList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.Extensions;

namespace PlatinumWeb
{
    public partial class GrupimeLocaleGlobale : MyPageBase
    {
        private  string kaveprime = "Ka veprime me kete grupim";
        private  string mesazhfshirjesukses = "Fshirja perfundoi me sukses!";
        private  string mesazhfshirjegabimi = "Fshirja perfundoi me gabime!";
        private  string gabimEkzistence = "Ekziston nje grupim global me kete kod. Ju lutem shenoni nje tjeter!";
        private  string gabimEkzistenceDitore = "Ekziston nje grupim lokal pune me kete kod. Ju lutem shenoni nje tjeter!";
        private int idndermarje, idperdoruesi, idnderviti, idviti;
        private string komponente = "GrupimeLocaleGlobale.aspx";
        private string guidString;
      
        /// <summary>
        /// kur faqja lodohet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);

            //if (CacheLayer.GlobalCacheManager.MySessionCache["LoggedIn"].Equals("No"))
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                Response.Redirect(DbCore.IMBUtils.Paths.defaultLoginPath);
            }
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            idndermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            idperdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            idnderviti = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
            idviti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            //if (CacheLayer.GlobalCacheManager.MySessionCache["KodiNdermarrjes"] == null)
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idperdoruesi);
            }

            percaktoTemplateMenu(ASPxMenu1, idviti, idperdoruesi, idndermarje);

            //mbushComboBoxFiltra();
        //    clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhePerdoruesi(Session), idndermarje, "gvGrupime", 1, "GrupimeLocaleGlobale.aspx");
            if (!IsPostBack)
            {
                perktheLabel();
                EmrateTabeve();
                //Session.Add("mesazh", ":Green");
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, ":");
                if (Request.QueryString["ruaj"] == "ok")
              clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("mesazhRuajtjeMeSukses", ci), pnlMesazhi);
                ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idperdoruesi, idndermarje, cmbKonfigurimi, 108, rm, ci, DbCore.mySessionObjects.ktheGjuhe(Session));
                guidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                hfState.Set("guidString", guidString);
                hfState.Set("idPerdoruesi", idperdoruesi);
                hfState.Set("idGjuha", DbCore.mySessionObjects.ktheGjuhe(Session));
                hfState.Set("idNdermarrje", idndermarje);
                cmbKonfigurimi.SelectedIndex = 0;
                DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()), DbCore.mySessionObjects.ktheGjuhe(Session));
                hfKonffillestar.Value = String.Format("{0};{1}", konf.KodKonfigAmbjente, konf.PershkrimKonfigAmbjente);
                konfiguroVleraFillestare(idndermarje,rm,ci,DbCore.mySessionObjects.ktheGjuhe(Session));
                konfiguroVleraFillestareG();
                konfiguroGride(idndermarje);
                TreeListUtil.percaktoVisibleColumnsKonf(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), gvGrupime, cmbKonfigurimi.Text.Split(';')[0], "721");

                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idperdoruesi, idndermarje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), komponente);
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idndermarje, "gvGrupime", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
            }
            guidString = (string)hfState["guidString"];
            DbCore.mySessionObjects.ruajMesazhNeSesion(Session, ":");
           AspxWebControlUtils.perkthePopUp(popFshi, rm.GetString("labelKujdes", ci), lblMsgbox, rm.GetString("labelAdministrimiMsgJeniSigurt", ci), ButtonCancel, rm.GetString("labelAnullo", ci));
            konfiguroGride(idndermarje);
            konfiguroVleraFillestareG();
        }


        public void perktheLabel()
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            kaveprime = rm.GetString("msgKaVeprimeMeKeteGrupim", cultinf);
            mesazhfshirjesukses = rm.GetString("msgFshirjaMeSukses", cultinf);
            gabimEkzistence = rm.GetString("msgEkzistonNjeGrupimGlobal", cultinf);
            gabimEkzistenceDitore = rm.GetString("msgEkzistonNjeGrupimLokale", cultinf);
            mesazhfshirjegabimi = rm.GetString("msgFshirjaMeGabime", cultinf);
            hfState.Set("msgNukKeniDrejtaPerVeprim", rm.GetString("msgNukKeniDrejtaPerVeprim", cultinf));
            hfState.Set("msgQKDuhetTeZgjdhni1Qender", rm.GetString("msgQKDuhetTeZgjdhni1Qender", cultinf));
            hfState.Set("msgZgjidhPrindin", rm.GetString("msgZgjidhPrindin", cultinf));
        }
        /// <summary>
        /// Vendos emrat e tabeve ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="cultinf"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateTabeve()
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
             ASPxPageControl1.TabPages[0].Text = rm.GetString("TePergjithshmeTab", cultinf);
             ASPxPageControl1.TabPages[1].Text = rm.GetString("GrupimeTab", cultinf);
            konfigurimi_Label.Text = rm.GetString("lblModeli", cultinf);
        }

        /// <summary>
        /// mbush gridat me te dhena
        /// </summary>

        /// <summary>
        /// konfiguron gridat

        /// <summary>
        /// konfiguron griden
        /// </summary>
        /// <param name="grida">grida</param>
        private void konfiguroGride(int idndermarje)
        {

            KonfigurimComboGride.Shto_Prind(gvGrupime, idndermarje, 1, Session, komponente, guidString, "IdPrindi");            

            TreeListUtil.konfiguroTreeListeEvogelPaTheme(gvGrupime, "Id", "IdPrindi", true);
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
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, komponente, this, MenuInfo, hfShtimModifikim.Value == "modifikim" ? false : true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));

        }
        protected void gvGrupime_DataBound(object sender, EventArgs e)
        {
            gvGrupime.KeyFieldName = "Id";
            gvGrupime.ParentFieldName = "IdPrindi";
            gvGrupime.SettingsSelection.Recursive = false;
            gvGrupime.SettingsBehavior.AllowDragDrop = false;
            gvGrupime.SettingsEditing.AllowNodeDragDrop = false;
            gvGrupime.SettingsBehavior.AutoExpandAllNodes = true;
        }

        /// <summary>
        /// ben databound menune
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(ASPxMenu1, idviti, idperdoruesi, idndermarje);
        }

    
        /// <summary>
        /// fshin rreshtin e fokusuar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonOk_Click2(object sender, EventArgs e)
        { //per t'u rregulluar sipas kodifikimit te kf

            List<TreeListNode> rreshtat = gvGrupime.GetSelectedNodes();
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
                DbCore.DbListPagesat.clsGrupimeLocaleGlobale kat = new DbCore.DbListPagesat.clsGrupimeLocaleGlobale(Convert.ToInt32(id.Key));

                if (DbCore.DbListPagesat.clsGrupimeLocaleGlobale.kaVeprimeGrupime(kat.Id))
                {
                    TePaFshire.Add(kat.Kodi);
                    continue;
                }
                else
                {
                    kat.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                     mesazh = kat.fshi();

                    if (mesazh.Status)
                    {
                        #region Heq llogarite nga grida
                        hiqNgaGrida(kat.Id, kat.IdNdermarje, rm, ci);
                        #endregion
                        TeFshire.Add(kat.Kodi);

                        ASPxPageControl1.ActiveTabIndex = 0;
                        hfStatusi.Value = "true";
                    }
                }
            }
            dbRegjistrim.Dispose();
            string mesazhInfoGabim = "", mesazhInfoSukses = "";
            if (TePaFshire.Count == 1)
                mesazhInfoGabim = String.Format("{0} {1} {2}", rm.GetString("msgQKPrefixNjejesi", ci), String.Join("; ", TePaFshire), rm.GetString("msgShtoLlogariSuffixNjejesGabimi", ci));
            else
                if (TePaFshire.Count > 1)
                    mesazhInfoGabim = String.Format("{0} {1} {2}", rm.GetString("msgQKPrefixShumesi", ci), String.Join("; ", TePaFshire), rm.GetString("msgShtoLlogariSuffixShumesGabimi", ci));
            if (TeFshire.Count == 1)
                mesazhInfoSukses = String.Format("{0} {1} {2}", rm.GetString("msgQKPrefixNjejesi", ci), String.Join("; ", TeFshire), rm.GetString("regjMagSuffixMesazhNjejesSuksesi", ci));
            else
                if (TeFshire.Count > 1)
                    mesazhInfoSukses = String.Format("{0} {1} {2}", rm.GetString("msgQKPrefixShumesi", ci), String.Join("; ", TeFshire), rm.GetString("msgCeljeMagazinatSuffixShumesSuksesi", ci));
            if (mesazhInfoGabim != "" && mesazhInfoSukses != "")
                mesazhInfoGabim += rm.GetString("msgGrupimeLidhesMesazhi", ci) + mesazhInfoSukses;
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
            if (gvGrupime.DataSource != null)
            {
                DataTable dt = (DataTable)gvGrupime.DataSource;
                DataRow[] drs = dt.Select(String.Format("Id = '{0}'", idburim));
                if (drs.Length > 1)
                    throw new DbCore.MyException(rm.GetString("msgQKNdodhen2QKMeTeNjejteIDNeGride", ci));
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                dt.Rows.Remove(dr);
                gvGrupime.DataBind();
            }
            else konfiguroVleraFillestareG();
        }

        /// <summary>
        /// shton nte gride rreshtat e rinj
        /// </summary>
        /// <param name="id"> id </param>
        /// <param name="idNdermarrje">id e ndermarjes</param>
        private void shtoNeGrid(int idNdermarrje, int id, ResourceManager rm, CultureInfo ci)
        {
            if (gvGrupime.DataSource != null)
            {
                DataTable dt = (DataTable)gvGrupime.DataSource;
                DataRow[] drs = dt.Select("Id = " + id);
                if (drs.Length > 0)
                    throw new DbCore.MyException(rm.GetString("msgQKNdodhen2QKMeTeNjejteIDNeGride", ci));
                DataRow newArtDr = DbCore.DbQendraKosto.colQendraKosto.merrQendraKostoDR(id);
                if (newArtDr != null)
                    dt.ImportRow(newArtDr);
                else konfiguroVleraFillestareG();
            }
            else konfiguroVleraFillestareG();
        }
        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {//kryen veprimet e menuse

            if (e.Item.Name == "Ruaj")
            {
                Page.Validate("entries");
                CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
                ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                ruajGrupim(rm, cultinf);
              //  clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("mesazhRuajtjeMeSukses",cultinf), pnlMesazhi);

            }
        }

        /// <summary>
        /// konfiguron vlerat fillestare
        /// </summary>
        /// <param name="idNdermarrje">id e ndermarjes</param>
        private void konfiguroVleraFillestare(int idNdermarrje, ResourceManager rm, CultureInfo cultinf, int idGjuha)
        { //mbush komboboxet dhe gridat e faqes
            ASPxPageControl1.ActiveTabIndex = 0;
           // DbCore.clsFunksione.mbushComboQendraKostoPrindKolona(cmbPrindi, rm, cultinf);

            ConfigureAspxComboBox.percaktoTemplateComboMeEnableCallback(cmbPrindi);
          //  DbCore.clsFunksione.mbushComboKonfigurimeshSipasKategorise(idperdoruesi, idNdermarrje, cmbKonfigurimi, 108, rm, cultinf, idGjuha);
            //cmbKonfigurimi.SelectedIndex = 0;
            //DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            //konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()));
            //hfKonffillestar.Value = String.Format("{0};{1}", konf.KodKonfigAmbjente, konf.PershkrimKonfigAmbjente);

        }

        /// <summary>
        /// mbush griden nga sesioni
        /// </summary>
        /// <param name="idNdermarrje">id e ndermarjes</param>

        private void konfiguroVleraFillestareG()
        {
            DataTable dt = DbCore.DbListPagesat.colGrupimeLocaleGlobale.merrGrupimeLocaleGlobaleDT(idndermarje);
            // DbCore.DbListPagesat.colGrupimeLocaleGlobale col1 = new DbCore.DbListPagesat.colGrupimeLocaleGlobale(idndermarje, 1);
            gvGrupime.DataSource = dt;
            gvGrupime.DataBind();

            //DbCore.DbListPagesat.colGrupimeLocaleGlobale col2 = new DbCore.DbListPagesat.colGrupimeLocaleGlobale(idndermarje, 2);
            //gvGlobale.DataSource = col2;
            //gvGlobale.DataBind();


        }

        /// <summary>
        /// ruan qender kosto 
        /// </summary>
        private void ruajGrupim(ResourceManager rm, CultureInfo ci)
        {
            DbCore.DbListPagesat.clsGrupimeLocaleGlobale qender = new DbCore.DbListPagesat.clsGrupimeLocaleGlobale();
            if (Page.IsValid == false)
                return;
            else
            {
                try
                {
                    if(cmbPrindi.Text!="")
                       if(!DbCore.DbListPagesat.clsGrupimeLocaleGlobale.ekzistonGrupimeLocaleGlobale(cmbPrindi.Text,idndermarje))
                       {
                           clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNukEkzistonNjeGrupimGlobal", ci), pnlMesazhi);
                           hfStatusi.Value = "false";
                           return;
                       }
                   
                    qender = krijoGrup();
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

                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idperdoruesi, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), komponente);

                if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim")
                {
                    if (!tedrejtaInfo.DShtim)
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNukKeniTeDrejta", ci), pnlMesazhi);
                        hfStatusi.Value = "false";
                        return;
                    }
                    //mesazh = burime.ruaj(null);
                    mesazh = qender.ruaj(hfNrAutoKF);
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
                    if (!lidhur.ToString().EqualsAnyIgnoreCase(hfLidhur.Value))
                    {
                        mesazh.Status = false;
                        mesazh.PershkrimMesazhi = rm.GetString("msgQKEshteELidhur", ci);
                    }
                    else


                        mesazh = qender.modifiko();
                    dbRegjistrim.Dispose();
                }
                if (mesazh.Status == true)
                {
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                    if (eshteShtim)
                        shtoNeGrid(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), qender.Id, rm, ci);
                    else //modifikim
                        konfiguroVleraFillestareG();
                    hfStatusi.Value = "true";
                    gvGrupime.SettingsBehavior.AutoExpandAllNodes = true;
                }
                else
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                    hfStatusi.Value = "false";
                }
                ASPxPageControl1.ActiveTabIndex = 0;
            }


        }

        private DbCore.DbListPagesat.clsGrupimeLocaleGlobale krijoGrup()
        {//krijon nje banke sipas te dhenave te futura nga perdoruesi
            int idPrindi;

            var controls = this.GetAsPxTextEditIdValue();
            controls.AddRange(ASPxPageControl1.GetAsPxTextEditIdValue());

            hfNrAuto = (ASPxHiddenField)NrAuto.VendosVleratNrAuto(hfNrAuto, controls);
            //DbCore.DbAdmin.NrAuto.vendosVleratNrAuto(hfNrAuto, this, this.ASPxPageControl1, null, null);

            if (cmbPrindi.Text != "")
            {
                DbCore.DbListPagesat.clsGrupimeLocaleGlobale prind = new DbCore.DbListPagesat.clsGrupimeLocaleGlobale(cmbPrindi.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));

                idPrindi = prind.Id;
            }
            else
                idPrindi = 0;

            hfNrAutoKF = (ASPxHiddenField)NrAuto.ShtoNeRegjistrime(hfNrAutoKF, hfNrAuto, "txtKodi", "Kodi");
            //DbCore.DbAdmin.NrAuto.shtoNeHfRegjistrime(hfNrAutoKF, hfNrAuto, "txtKodi", "Kodi");

            DbCore.DbShare.clsKonfigurimAmbjenti konfig = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konfig.mbushKonfigAmbjSipasKod(cmbKonfigurimi.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            DbCore.DbListPagesat.clsGrupimeLocaleGlobale qender = new DbCore.DbListPagesat.clsGrupimeLocaleGlobale(0, txtKodi.Text, txtEmertimi.Text, idperdoruesi, cbAktiv.Checked, idPrindi > 0 ? 2 : 1, idperdoruesi, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), konfig.IdKonfigAmbjente, 1, idPrindi);
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
                    ConfigureAspxComboBox.mbushComboGrupimeGlobalLocal(cmbPrindi, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), 1,Convert.ToInt32(e.Value));
                }
            }
        }
        /// <summary>
        /// kur grida ben callback nga perdoruesi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvGrupime_CustomCallback1(object sender, TreeListCustomCallbackEventArgs e)
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
    }
}