using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DbCore;
using DevExpress.Web;
using System.Web.UI.HtmlControls;
using DbCore.DbAdmin;
using PlatinumWeb.Templates;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Resources;
using DbCore.DbInventari;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.Messages;
using DbCore.DbRegjistrim;

namespace PlatinumWeb
{
    public partial class Shto_KonfigurimFormatImporti : MyPageBase
    {
        /// <summary>
        /// konstante per mesazhin e trupit nuk duhet te jete bosh
        /// </summary>
        private const string STR_TrupiIDokumentitNukDuhetTeJeteBosh = "Fushat e zgjedhura nuk duhet të jenë bosh!";
        private string komponente = "Shto_KonfigurimFormatImporti.aspx";
        /// <summary>
        /// mbush fushat gjate modifikimit te dokumentit
        /// </summary>
        /// <param name="idPerdoruesi">id e perdoruesit</param>
        /// <param name="idNdermarrje"> id e ndermarjes</param>
        /// <param name="koka">koka e ekzekutimit</param>
        public void MerrTedhenat(int idPerdoruesi, int idNdermarrje, DbCore.DbAdmin.clsKokaFormatImporti koka)
        {
            if (koka.IdKategori != 0)
                cmbKategoria.Value = koka.IdKategori.ToString();
            txtKodi.Text = koka.Kodi;
            txtShenime.Text = koka.Pershkrimi;
            mbushTrupin(koka.IdKoka);
        }

        private void mbushTrupin(int id)
        {
            DbCore.DbAdmin.colTrupiFormatImporti trupatvis = DbCore.DbAdmin.colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDheVisible(id, true);
            DbCore.DbAdmin.colTrupiFormatImporti trupatinvis = DbCore.DbAdmin.colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDheVisible(id, false);
            gvZgjedhur.DataSource = trupatvis;
            DbCore.mySessionObjects.ruajFormatImportiVisibleNeSession(Session, trupatvis);
            DbCore.mySessionObjects.ruajFormatImportiInVisibleNeSession(Session, trupatinvis);
            lbxFushat.ValueField = "IdTrupi";
            lbxFushat.TextField = "KodKontrolli";
            lbxFushat.DataSource = trupatinvis;
            gvZgjedhur.DataBind();
            lbxFushat.DataBind();
        }

        /// <summary>
        /// mbush te dhenat kur faqja ben loadim
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
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

            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idviti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            int idgjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            if (!IsPostBack)
            {
                perktheLabel();
                hfState.Set("idGjuha", idgjuha);
                //hfState.Set("Artikull", false);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("idPerdoruesi", idPerdoruesi);
                hfState.Set("idVitNdermarrje", idviti);
                System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
                System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                if (hfShtimModifikim.Value == "")
                {
                    if (Request.QueryString["shtim_modifikim"] == "shtim" || String.IsNullOrEmpty(Request.QueryString["shtim_modifikim"]))
                    {
                        hfShtimModifikim.Value = "shtim";
                        konfiguroVleraFillestareShto(idNdermarrje, rm, ci, idgjuha, idviti);
                    }
                    else if (Request.QueryString["shtim_modifikim"] == "modifikim")
                    {
                        hfShtimModifikim.Value = "modifikim";
                        konfiguroVleraFillestareModifiko(idPerdoruesi, idNdermarrje, rm, ci, idgjuha, idviti);
                    }
                    else if (Request.QueryString["shtim_modifikim"] == "klonim")
                    {
                        hfShtimModifikim.Value = "klonim";
                        konfiguroVleraFillestareModifiko(idPerdoruesi, idNdermarrje, rm, ci, idgjuha, idviti);
                    }
                }
                else
                    if (hfShtimModifikim.Value == "shtim")
                        konfiguroVleraFillestareShto(idNdermarrje, rm, ci, idgjuha, idviti);
                    else
                        if (hfShtimModifikim.Value == "modifikim" || hfShtimModifikim.Value == "klonim")
                            konfiguroVleraFillestareModifiko(idPerdoruesi, idNdermarrje, rm, ci, idgjuha, idviti);


                percaktoTemplateMenu(idgjuha, idviti, idPerdoruesi, idNdermarrje, ASPxMenu1);
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idviti, komponente);
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                int id = Convert.ToInt32(Request.QueryString["id"]);
                DbCore.DbAdmin.clsKokaFormatImporti kok = new DbCore.DbAdmin.clsKokaFormatImporti(id);
                if (hfShtimModifikim.Value == "modifikim")
                    hfState.Set("FormatLidhurSQL", clsKokaFormatImporti.eshteFormatLidhurMeSQL(kok.IdKoka));
                else
                    hfState.Set("FormatLidhurSQL", false);
                konfiguroGride();
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(Convert.ToInt32(hfState["idNdermarrje"]), "gvZgjedhur", gvZgjedhur, cmbKonfigurimi.Text.Split(';')[0], (new DbCore.DbAdmin.clsKomponente(komponente)).IdKomponente.ToString(), Convert.ToInt32(hfState["idGjuha"]));

            }
            else
            {
                mbushNgaSessioni();
                konfiguroGride();
            }
               
            percaktoTemplate();
            gvZgjedhur.Columns["#"].VisibleIndex = 0;
            gvZgjedhur.Columns["#"].Width = 50;
            gvZgjedhur.Columns["#"].HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
        }

        public void perktheLabel()
        {
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            btnSiper.Text = rm.GetString("lblSiper", ci);
            btnPoshte.Text = rm.GetString("lblPoshte", ci);
        }
        private void percaktoTemplateMenu(int idGjuha, int idViti, int idPerdorues, int idNdermarrje, ASPxMenu aSPxMenu1)
        {
            clsToolbarConfig.percaktoTemplateMenu(idGjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1, komponente, this, MenuInfo, hfShtimModifikim.Value == "modifikim" ? false : true, true, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));

            if ((hfShtimModifikim.Value != "modifikim"))
            {
                aSPxMenu1.Items.FindByName("Fshi").Visible = false;
                aSPxMenu1.Items.FindByName("Klono").Visible = false;
                aSPxMenu1.Items.FindByName("Eksporto").Visible = false;
            }
            else
            {
                clsTeDrejtaRoli tedrejtaInfo = new clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponenteDheKategori(Convert.ToInt32(hfState["idPerdoruesi"]), Convert.ToInt32(hfState["idNdermarrje"]), Convert.ToInt32(hfState["idVitNdermarrje"]), komponente, Convert.ToInt32(cmbKategoria.Value));

                aSPxMenu1.Items.FindByName("Fshi").Enabled = tedrejtaInfo.DFsh;
                aSPxMenu1.Items.FindByName("Ruaj").Enabled = tedrejtaInfo.DMod;
                aSPxMenu1.Items.FindByName("Klono").Enabled = tedrejtaInfo.DShtim;
                
            }
        }

        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(Convert.ToInt32(hfState["idGjuha"]), Convert.ToInt32(hfState["idVitNdermarrje"]), Convert.ToInt32(hfState["idPerdoruesi"]), Convert.ToInt32(hfState["idNdermarrje"]), ASPxMenu1);
        }

        /// <summary>
        /// Vendos vlerat default kur po behet shtim
        /// </summary>
        /// <param name="idNdermarrje"></param>
        private void konfiguroVleraFillestareShto(int idNdermarrje, System.Resources.ResourceManager rm, System.Globalization.CultureInfo ci, int idGjuha, int idViti)
        {
            int idPerdoruesi = Convert.ToInt32(hfState["idPerdoruesi"]);
            ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 66, rm, ci, idGjuha);
            cmbKonfigurimi.SelectedIndex = 0;
            hfKonffillestar.Value = cmbKonfigurimi.Text;
            ConfigureAspxComboBox.KonfiguroComboBoxKategoriPerImport(cmbKategoria, false, idNdermarrje, idViti, idPerdoruesi, komponente);
            mbushPopUpListeNgaDB();
        }

        /// <summary>
        /// Vendos vlerat default kur po behet modifikim
        /// </summary>
        /// <param name="idPerdoruesi"></param>
        /// <param name="idNdermarrje"></param>
        private void konfiguroVleraFillestareModifiko(int idPerdoruesi, int idNdermarrje, System.Resources.ResourceManager rm, System.Globalization.CultureInfo ci, int idGjuha, int idViti)
        {//mbush kombot dhe gridat
            ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 66, rm, ci, idGjuha);
            cmbKonfigurimi.SelectedIndex = 0;

            hfKonffillestar.Value = cmbKonfigurimi.Text;
            ConfigureAspxComboBox.KonfiguroComboBoxKategoriPerImport(cmbKategoria, false, idNdermarrje, idViti, idPerdoruesi, komponente);
            int id = int.Parse(Request.QueryString["id"]);
            DbCore.DbAdmin.clsKokaFormatImporti kok = new DbCore.DbAdmin.clsKokaFormatImporti(id);

            if (kok != null)
            {
                MerrTedhenat(idPerdoruesi, idNdermarrje, kok);
            }
        }

        /// <summary>
        /// Percakton veprimin qe kryhet kur klikohet nje nga butonat e menuse
        /// </summary>
        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {
            if (e.Item.Name == "Ruaj")
            {
                Page.Validate();
                ruajKonfigurim(1);
            }
        }

        /// <summary>
        /// fshin dokumentin
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            DbCore.DbAdmin.clsKokaFormatImporti kokam = new DbCore.DbAdmin.clsKokaFormatImporti(int.Parse(Request.QueryString["id"]));
            clsMesazh mesazh = new clsMesazh();

            int idNdermarrje = Convert.ToInt32(hfState["idNdermarrje"]);
            int idPerdoruesi = Convert.ToInt32(hfState["idPerdoruesi"]);
            int idViti = Convert.ToInt32(hfState["idVitNdermarrje"]);

            clsTeDrejtaRoli teDrejtaKategoria = new clsTeDrejtaRoli();
            teDrejtaKategoria.merrTeDrejtaPerKeteKomponenteDheKategori(idPerdoruesi,idNdermarrje, idViti, komponente, kokam.IdKategori);
            if (teDrejtaKategoria.DFsh == false)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNukKeniTeDrejta", ci), pnlMesazhi);
                return;
            }

            kokam.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            if (clsKokaFormatImporti.kaveprime(kokam.IdKoka))
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ka veprime me kete konfigurim!", pnlMesazhi);
                status1.Value = "false";
                return;
            }
            mesazh = kokam.fshiFormatImporti();
            if (mesazh.Status)
            {
                Response.Redirect("KonfigurimFormatImporti.aspx?fshi=po");
                status1.Value = "true";
                return;
            }
            else
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                status1.Value = "false";
                return;
            }
        }

        /// <summary>
        /// Ruan/Modifikon nje objekt clsKokaEkzekutim.
        /// </summary>
        /// <param name="statusDokumenti">Statusi me te cilin po ruhet dokumenti (I rregullt apo draft)</param>
        private void ruajKonfigurim(int statusDokumenti)
        {
            if (Page.IsValid == false)
                return;

            clsMesazh mesazh;
            DbCore.DbAdmin.clsKokaFormatImporti koka;
            try
            {
                koka = krijoKonfigurim(statusDokumenti);

                if (koka.ColTrupi.Count == 0)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, STR_TrupiIDokumentitNukDuhetTeJeteBosh, pnlMesazhi, LoadingPanel);
                    status1.Value = "false";
                    return;
                }
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex.Message);
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, ex.Message, pnlMesazhi, LoadingPanel);
                status1.Value = "false";               
                return;
            }
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

            tedrejtaInfo.merrTeDrejtaPerKeteKomponenteDheKategori(Convert.ToInt32(hfState["idPerdoruesi"]), Convert.ToInt32(hfState["idNdermarrje"]), Convert.ToInt32(hfState["idVitNdermarrje"]), komponente, koka.IdKategori);


            if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim")
            {
                if (!tedrejtaInfo.DShtim)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                    status1.Value = "false";
                    return;
                }
                mesazh = koka.ruajFormatImporti();
            }
            else
            {
                if (!tedrejtaInfo.DMod)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                    status1.Value = "false";
                    return;
                }
                koka.IdKoka = int.Parse(Request.QueryString["id"]);
                mesazh = koka.modifikoFormatImporti();
            }

            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi, LoadingPanel);
                status1.Value = "false";
                return;
            }

            clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            DbCore.mySessionObjects.ruajFormatImportiVisibleNeSession(Session, new colTrupiFormatImporti());
            DbCore.mySessionObjects.ruajFormatImportiInVisibleNeSession(Session, new colTrupiFormatImporti());
            hl = new HtmlTable();
            pnlLidhur.Update();
            status1.Value = "true";
            hfShtimModifikim.Value = "shtim";
        }

        /// <summary>
        /// Krijon nje objekt te tipit clsKokaEkzekutim
        /// </summary>
        /// <param name="statusDokumenti">Statusi i dokumentit (I rregullt ose draft)</param>

        /// <returns>Kthen nje objekt te tipit clsKokaEkzekutim</returns>
        private DbCore.DbAdmin.clsKokaFormatImporti krijoKonfigurim(int statusDokumenti)
        {
            DbCore.DbAdmin.clsKokaFormatImporti koka = new DbCore.DbAdmin.clsKokaFormatImporti();
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DbCore.DbShare.clsKonfigurimAmbjenti clsKonf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            clsKonf.mbushKonfigDefaultKomponentes(175, idNdermarrje);
            if (cmbKonfigurimi.Text != "") //cmbKonfigurimi.Value != null && 
            {
                clsKonf.mbushKonfigAmbjSipasKod(cmbKonfigurimi.Text, idNdermarrje);
            }
            try
            {
                int idkategori = 0;
                if (cmbKategoria.Text != "")
                {
                    idkategori = int.Parse(cmbKategoria.Value.ToString());
                }
                koka = krijoKonfigurim(statusDokumenti, clsKonf, krijoTrup(idkategori), idkategori);
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex.Message);
                throw new Exception(ex.Message);
            }
            return koka;
        }

        /// <summary>
        /// Krijon nje objekt te tipit clsKokaEkzekutim 
        /// </summary>
        /// <param name="statusDokumenti">Statusi i dokumentit (I rregullt ose draft)</param>
        /// <returns>Kthen nje objekt te tipit DbCore.DbRegjistrim.clsKokaMagazina</returns>
        private clsKokaFormatImporti krijoKonfigurim(int statusDokumenti, DbCore.DbShare.clsKonfigurimAmbjenti clsKonf, DbCore.DbAdmin.colTrupiFormatImporti coltrupi, int idkategori)
        {
            clsKokaFormatImporti kokaFormati = new clsKokaFormatImporti(0, txtKodi.Text, txtShenime.Text, idkategori, Convert.ToInt32(hfState["idNdermarrje"]), Convert.ToInt32(hfState["idPerdoruesi"]), statusDokumenti);
            kokaFormati.ColTrupi = coltrupi;
            return kokaFormati;
        }

        private void mbushPopUpListeNgaDB()
        {//mbush griden e popupit me te dhena     
            colTrupiFormatImporti trupatvis = new colTrupiFormatImporti(), trupatinvis = new colTrupiFormatImporti();

            gvZgjedhur.DataSource = trupatvis;
            DbCore.mySessionObjects.ruajFormatImportiVisibleNeSession(Session, trupatvis);
            DbCore.mySessionObjects.ruajFormatImportiInVisibleNeSession(Session, trupatinvis);
            lbxFushat.ValueField = "IdTrupi";
            lbxFushat.TextField = "KodKontrolli";
            lbxFushat.DataSource = trupatinvis;
            gvZgjedhur.DataBind();
            lbxFushat.DataBind();
        }
        private void mbushNgaSessioni()
        {
            colTrupiFormatImporti trupavis = DbCore.mySessionObjects.merrFormatImportiVisibleNgaSesioni(Session);
            colTrupiFormatImporti trupainvis = DbCore.mySessionObjects.merrFormatImportiInVisibleNgaSesioni(Session);
            gvZgjedhur.DataSource = trupavis;
            lbxFushat.ValueField = "IdTrupi";
            lbxFushat.TextField = "KodKontrolli";
            lbxFushat.DataSource = trupainvis;
            gvZgjedhur.DataBind();
            lbxFushat.DataBind();
        }

        protected void gvZgjedhur_AfterPerformCallback(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            gvZgjedhur.DataBind();
        }

        protected void gvZgjedhur_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            if (IsCallback)
            {
                colTrupiFormatImporti trupatvis = new colTrupiFormatImporti();
                colTrupiFormatImporti trupatinvis = new colTrupiFormatImporti();

                if (Request.Params["__CALLBACKID"].ToString().Contains("gvZgjedhur"))
                {
                    if (e.Parameters.ToString().Contains("pastro"))
                    {
                        trupatvis = new colTrupiFormatImporti();
                        trupatinvis = new colTrupiFormatImporti();
                    }
                    DbCore.mySessionObjects.ruajFormatImportiInVisibleNeSession(Session, trupatinvis);
                    DbCore.mySessionObjects.ruajFormatImportiVisibleNeSession(Session, trupatvis);
                    gvZgjedhur.DataSource = trupatvis;
                    gvZgjedhur.DataBind();
                    percaktoTemplate();
                }
            }
        }

        private void konfiguroGride()
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            
            GridUtil.konfiguroGrideRegjistrimEvogelPaTheme(gvZgjedhur, "IdTrupi", false);
            gvZgjedhur.Settings.UseFixedTableLayout = false;
        }

        protected void gvZgjedhur_CustomJSProperties(object sender, DevExpress.Web.ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpNoRows"] = gvZgjedhur.VisibleRowCount;
            e.Properties["cpNoPage"] = gvZgjedhur.PageIndex;
        }

        protected void gvZgjedhur_DataBound(object sender, EventArgs e)
        {
            if (gvZgjedhur.Columns["#"] == null)
            {
                GridViewCommandColumn check = new GridViewCommandColumn("#") { ShowSelectCheckbox = true, Width = System.Web.UI.WebControls.Unit.Percentage(2) };
                gvZgjedhur.Columns.Add(check);
                gvZgjedhur.SettingsBehavior.AllowSelectByRowClick = true;
                gvZgjedhur.KeyFieldName = "IdTrupi";
                gvZgjedhur.SettingsBehavior.AllowFocusedRow = true;
            }
        }

        protected void gvZgjedhur_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
        {
            //bool ugjet;
            if (e.RowType != GridViewRowType.Data || cmbKategoria.Value == null)
                return;

            colTrupiFormatImporti trupavis = DbCore.mySessionObjects.merrFormatImportiVisibleNgaSesioni(Session);
            int idNdermarrje = Convert.ToInt32(hfState["idNdermarrje"]);
            int idperdorues = Convert.ToInt32(hfState["idPerdoruesi"]);
            int idGjuha = Convert.ToInt32(hfState["idGjuha"]);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            GridViewDataTextColumn col1 = ((ASPxGridView)sender).Columns["EmerImporti"] as GridViewDataTextColumn;
            GridViewDataTextColumn col2 = ((ASPxGridView)sender).Columns["VleraDefault"] as GridViewDataTextColumn;
            GridViewDataCheckColumn col3 = ((ASPxGridView)sender).Columns["Detyrueshme"] as GridViewDataCheckColumn;
            GridViewDataCheckColumn col6 = ((ASPxGridView)sender).Columns["Shfaq"] as GridViewDataCheckColumn;
            GridViewDataTextColumn col0 = ((ASPxGridView)sender).Columns["KodKontrolli"] as GridViewDataTextColumn;

            ASPxLabel lbl = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col0, "lbl") as ASPxLabel;
            ASPxTextBox txt12 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col1, "txtBox") as ASPxTextBox;
            ASPxCheckBox txt2 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col3, "cb") as ASPxCheckBox;
            ASPxCheckBox txt5 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col6, "cb") as ASPxCheckBox;
            
            // ugjet = false;
            if (trupavis.Count > e.VisibleIndex)
            {
                if (trupavis[e.VisibleIndex].TipKontrolli == 2 || trupavis[e.VisibleIndex].TipKontrolli == 4 || (trupavis[e.VisibleIndex].TipKontrolli == 1 && trupavis[e.VisibleIndex].KodKontrolli == "Perdoruesi"))
                {
                    ASPxComboBox cmb3 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col2, "cmbBox") as ASPxComboBox;
                    if (cmb3 != null)
                    {
                        cmb3.ClientInstanceName = "txtVlera" + e.VisibleIndex;
                        cmb3.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                        cmb3.DropDownStyle = DropDownStyle.DropDown;

                        switch (trupavis[e.VisibleIndex].KodKontrolli)
                        {
                            case "Nenkategoria":
                                ApplicationUtils.ASPxControlUtils.ConfigureAspxComboBox.KonfiguroComboBoxNiveletSipasKategoriDtCombo(cmb3, idNdermarrje, idperdorues, Convert.ToInt32(cmbKategoria.Value), true, false,false);
                                cmb3.ClientInstanceName = "txtVleraNenkategoria";
                                cmb3.ClientSideEvents.SelectedIndexChanged = "function(s,e){ SelectedIndexChangedNenkategoria(txtVleraNenkategoria, " + e.VisibleIndex.ToString() + ", s, e); }";
                                cmb3.ClientSideEvents.Init = "function(s,e){ SelectedIndexChangedNenkategoria(txtVleraNenkategoria, " + e.VisibleIndex.ToString() + ", s, e); }";
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVleraNenkategoria,'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Lloji i subjektit":
                                ConfigureAspxComboBox.mbushComboLlojiSubjekti(cmb3);
                                cmb3.ClientInstanceName = "txtLlojSubjekti";
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtLlojSubjekti,'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Lloj Dokumenti":
                            case "Lloj dok":
                            case "Lloj dok hyrje":
                                switch (cmbKategoria.Text)
                                {
                                    case "Shitje":
                                        ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idperdorues, idNdermarrje, cmb3, 1, rm, ci, idGjuha);
                                        break;
                                    case "Blerje":
                                        ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idperdorues, idNdermarrje, cmb3, 2, rm, ci, idGjuha);
                                        break;
                                    case "Magazina":
                                        ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idperdorues, idNdermarrje, cmb3, 6, rm, ci, idGjuha);
                                        break;
                                    case "Shperndarje shpenzimesh":
                                        ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idperdorues, idNdermarrje, cmb3, 6, "FH", rm, ci, idGjuha);
                                        break;
                                    case "Ekzekutim Prodhimi":
                                        ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idperdorues, idNdermarrje, cmb3, 45, rm, ci, idGjuha);
                                        break;
                                    case "Perfitim Buxheti":
                                        ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idperdorues, idNdermarrje, cmb3, 177, rm, ci, idGjuha);
                                        break;
                                    case "Planifikim Ekzekutim Buxheti":
                                        ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idperdorues, idNdermarrje, cmb3, 179, rm, ci, idGjuha);
                                        break;
                                    case "Ekzekutim Buxheti":
                                        ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idperdorues, idNdermarrje, cmb3, 181, rm, ci, idGjuha);
                                        break;
                                    case "Alokim Buxheti":
                                        ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idperdorues, idNdermarrje, cmb3, 172, rm, ci, idGjuha);
                                        cmb3.ClientSideEvents.SelectedIndexChanged = "function(s,e){ SelectedIndexChangedLlojDokumenti(txtVleraLlojDokumenti, " + e.VisibleIndex.ToString() + ", s, e); }";
                                        cmb3.ClientSideEvents.Init = "function(s,e){ SelectedIndexChangedLlojDokumenti(txtVleraLlojDokumenti, " + e.VisibleIndex.ToString() + ", s, e); }";
                                        break;
                                    case "Rialokim Buxheti":
                                        ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idperdorues, idNdermarrje, cmb3, 175, rm, ci, idGjuha);
                                        break;
                                }
                                cmb3.ClientInstanceName = "txtVleraLlojDokumenti";
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVleraLlojDokumenti,'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Lloj veprimi":
                                switch (Convert.ToInt32(cmbKategoria.Value))
                                {
                                    case 12:
                                    case 13:
                                        cmb3.Items.Add("Shtim", 1);
                                        cmb3.Items.Add("Modifikim", 2);
                                        cmb3.ClientInstanceName = "cmbLlojVeprimi";
                                        cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(cmbLlojVeprimi,'txtVlera', {0});}}", e.VisibleIndex);
                                        break;
                                    default:
                                        if (Convert.ToInt32(cmbKategoria.Value) == 177)
                                        {
                                            cmb3.Items.Add("Arketim", 1);
                                            cmb3.Items.Add("Shitje", 2);
                                        }
                                        else
                                        {
                                            cmb3.Items.Add("Pagese", 3);
                                            cmb3.Items.Add("Blerje", 4);
                                        }
                                        cmb3.ClientInstanceName = "cmbLlojVeprimiPerfitimBuxheti";
                                        cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(cmbLlojVeprimiPerfitimBuxheti,'txtVlera', {0});}}", e.VisibleIndex);
                                        break;

                                }
                                break;
                            case "Entiteti":
                                ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmb3);
                                cmb3.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedEntiteti(txtVlera" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + "); }";
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Buxheti":
                                cmb3.Items.Add("buxhetiqeveritar", true);
                                cmb3.Items.Add("buxhetisekondar", false);
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Artikull Buxhetimi":
                                ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmb3);
                                cmb3.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedKategoriBuxhetimi(txtVlera" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + "); }";
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Viti":
                                cmb3.Items.Add("", 1);
                                cmb3.ClientInstanceName = "cmbViteBuxheti";
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(cmbViteBuxheti,'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Klient/Furnitori":
                            case "Kod klient/furnitori":
                            case "Klient/Furnitor Kunderparti":
                                ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmb3);
                                ConfigureAspxComboBox.mbushComboKlientFurnitori(idperdorues, idNdermarrje, cmb3, 0);
                                cmb3.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedKF(txtVlera" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + "); }";
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Klient/Furnitori Kryesor":
                            case "Klienti":
                                ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmb3);
                                switch (cmbKategoria.Value.ToString())
                                {
                                    case "12":
                                    case "117":
                                    case "163":
                                        ConfigureAspxComboBox.mbushComboKlientFurnitori(idperdorues, idNdermarrje, cmb3, 1); break;
                                    case "31":
                                        ConfigureAspxComboBox.mbushComboKlientFurnitori(idperdorues, idNdermarrje, cmb3, 2); break;
                                    default:
                                        ConfigureAspxComboBox.mbushComboKlientFurnitori(idperdorues, idNdermarrje, cmb3, 0); break;
                                }
                                cmb3.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedKF1(txtVlera" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + "); }";
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Subjekti":
                                ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmb3);
                                ConfigureAspxComboBox.mbushComboKlientFurnitori(idperdorues, idNdermarrje, cmb3, 0);
                                cmb3.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedSubjekti(txtVlera" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + ", txtLlojSubjekti); }";
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Agjent Shitje 1":
                            case "Agjent Shitje 2":
                            case "Agjent Shitje 3":
                                ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmb3);
                                ConfigureAspxComboBox.KonfiguroComboBoxAgjentesh(IdNdermarrja, 0, cmb3);
                                cmb3.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedAgjenti(txtVlera" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + "); }";
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Kursi":
                                cmb3.DropDownButton.Visible = false;
                                //cmb3.ClientSideEvents.ButtonClick = "function(s,e){ Kursi_Click(txtVlera" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + "); }";
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Menyre Pagese":
                                ConfigureAspxComboBox.KonfiguroComboBoxMetoda(cmb3, true);
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Lloji i marreveshjes":
                                ConfigureAspxComboBox.mbushComboLlojMarreveshje(cmb3);
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Grupim Dokumenti 1":
                            case "Grupim Dok. 1":
                                if (cmbKategoria.Text == "Shitje")
                                    ConfigureAspxComboBox.mbushComboGrupeDokumentashSipasKategorise(cmb3, idNdermarrje, 1, "1", idperdorues);
                                else if (cmbKategoria.Text == "Blerje")
                                    ConfigureAspxComboBox.mbushComboGrupeDokumentashSipasKategorise(cmb3, idNdermarrje, 1, "2", idperdorues);
                                else if (cmbKategoria.Text == "Ekzekutim Prodhimi")
                                    ConfigureAspxComboBox.mbushComboGrupeDokumentashSipasKategorise(cmb3, idNdermarrje, 1, "45", idperdorues);
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Grupim Dokumenti 2":
                            case "Grupim Dok. 2":
                                if (cmbKategoria.Text == "Shitje")
                                    ConfigureAspxComboBox.mbushComboGrupeDokumentashSipasKategorise(cmb3, idNdermarrje, 2, "1", idperdorues);
                                else if (cmbKategoria.Text == "Blerje")
                                    ConfigureAspxComboBox.mbushComboGrupeDokumentashSipasKategorise(cmb3, idNdermarrje, 2, "2", idperdorues);
                                else if (cmbKategoria.Text == "Ekzekutim Prodhimi")
                                    ConfigureAspxComboBox.mbushComboGrupeDokumentashSipasKategorise(cmb3, idNdermarrje, 2, "45", idperdorues);
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Grupim Dokumenti 3":
                            case "Grupim Dok. 3":
                                if (cmbKategoria.Text == "Shitje")
                                    ConfigureAspxComboBox.mbushComboGrupeDokumentashSipasKategorise(cmb3, idNdermarrje, 3, "1", idperdorues);
                                else if (cmbKategoria.Text == "Blerje")
                                    ConfigureAspxComboBox.mbushComboGrupeDokumentashSipasKategorise(cmb3, idNdermarrje, 3, "2", idperdorues);
                                else if (cmbKategoria.Text == "Ekzekutim Prodhimi")
                                    ConfigureAspxComboBox.mbushComboGrupeDokumentashSipasKategorise(cmb3, idNdermarrje, 3, "45", idperdorues);
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Grupim Dok Magazine 1":
                                ConfigureAspxComboBox.mbushComboGrupeDokumentashSipasKategorise(cmb3, idNdermarrje, 1, "6", idperdorues);
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Grupim Dok Magazine 2":
                                ConfigureAspxComboBox.mbushComboGrupeDokumentashSipasKategorise(cmb3, idNdermarrje, 2, "6", idperdorues);
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Grupim Dok Magazine 3":
                                ConfigureAspxComboBox.mbushComboGrupeDokumentashSipasKategorise(cmb3, idNdermarrje, 3, "6", idperdorues);
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Grupim Likuiditete 1":
                                if (cmbKategoria.Text == "Veprime Arke")
                                    ConfigureAspxComboBox.mbushComboGrupeDokumentashSipasKategorise(cmb3, idNdermarrje, 1, "3", idperdorues);
                                else
                                    ConfigureAspxComboBox.mbushComboGrupeDokumentashSipasKategorise(cmb3, idNdermarrje, 1, "4", idperdorues);
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Grupim Likuiditete 2":
                                if (cmbKategoria.Text == "Veprime Arke")
                                    ConfigureAspxComboBox.mbushComboGrupeDokumentashSipasKategorise(cmb3, idNdermarrje, 2, "3", idperdorues);
                                else
                                    ConfigureAspxComboBox.mbushComboGrupeDokumentashSipasKategorise(cmb3, idNdermarrje, 2, "4", idperdorues);
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Grupim Likuiditete 3":
                                if (cmbKategoria.Text == "Veprime Arke")
                                    ConfigureAspxComboBox.mbushComboGrupeDokumentashSipasKategorise(cmb3, idNdermarrje, 3, "3", idperdorues);
                                else
                                    ConfigureAspxComboBox.mbushComboGrupeDokumentashSipasKategorise(cmb3, idNdermarrje, 3, "4", idperdorues);
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Automjeti":
                                ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmb3);
                                ConfigureAspxComboBox.mbushComboAutomjete(idNdermarrje, cmb3);
                                cmb3.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedAtomjet(txtVlera" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + "); }";
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Transportues":
                                ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmb3);
                                ConfigureAspxComboBox.mbushComboTransportues(idNdermarrje, cmb3);
                                cmb3.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedTransportues(txtVlera" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + "); }";
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Dogana":
                                cmb3.Items.Add("Jo", 0);
                                cmb3.Items.Add("Po", 1);
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Njesia 1":
                            case "Njesia 2":
                            case "Njesia":
                            case "Njesia e Produktit":
                            case "Njesia e Receptures":
                                if (cmbKategoria.Text == "Kodbar artikulli")
                                {
                                    cmb3.Items.Add("1", 1);
                                    cmb3.Items.Add("2", 2);
                                    cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                    break;
                                }
                                ConfigureAspxComboBox.mbushComboNjesi(idNdermarrje, cmb3);
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Monedha":
                                ConfigureAspxComboBox.mbushComboMonedha(idperdorues, idNdermarrje, false, cmb3);
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Grupimi 1":
                                ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmb3);
                                switch (cmbKategoria.Text)
                                {
                                    case "Artikuj afatshkurter/afatgjate":
                                        ConfigureAspxComboBox.mbushComboKodifikim(idNdermarrje, cmb3, 1, false, true);
                                        cmb3.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedKodifikim(txtVlera" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + ",1); }";
                                        break;
                                    case "Klient/Furnitor":
                                        ConfigureAspxComboBox.KonfiguroComboBoxGrupKf(idNdermarrje, cmb3, 1);
                                        cmb3.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedGrupKf(txtVlera" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + ",1); }";
                                        break;
                                }
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Grupimi 2":
                                ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmb3);
                                switch (cmbKategoria.Text)
                                {
                                    case "Artikuj afatshkurter/afatgjate":
                                        ConfigureAspxComboBox.mbushComboKodifikim(idNdermarrje, cmb3, 2, false, true);
                                        cmb3.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedKodifikim(txtVlera" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + ",2); }";
                                        break;
                                    case "Klient/Furnitor":
                                        ConfigureAspxComboBox.KonfiguroComboBoxGrupKf(idNdermarrje, cmb3, 2);
                                        cmb3.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedGrupKf(txtVlera" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + ",2); }";
                                        break;
                                }
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Grupimi 3":
                                ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmb3);
                                switch (cmbKategoria.Text)
                                {
                                    case "Artikuj afatshkurter/afatgjate":
                                        ConfigureAspxComboBox.mbushComboKodifikim(idNdermarrje, cmb3, 3, false, true);
                                        cmb3.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedKodifikim(txtVlera" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + ",3); }";
                                        break;
                                    case "Klient/Furnitor":
                                        ConfigureAspxComboBox.KonfiguroComboBoxGrupKf(idNdermarrje, cmb3, 3);
                                        cmb3.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedGrupKf(txtVlera" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + ",3); }";
                                        break;
                                }
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Arka/Banka":
                                ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmb3);
                                ConfigureAspxComboBox.mbushComboBankatJoSipasLlojit(idperdorues, idNdermarrje, cmb3, 0, false);
                                cmb3.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedBankat(txtVlera" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + "); }";
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Arka":
                                ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmb3);
                                ConfigureAspxComboBox.mbushComboBankatJoSipasLlojit(idperdorues, idNdermarrje, cmb3, 0, false);
                                cmb3.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedBankat(txtVlera" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + "); }";
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Detajimi 1":
                            case "Detajim 1":
                            case "Detajim 2":
                            case "Detajimi 2":
                                ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmb3);
                                cmb3.ReadOnly = true;
                                cmb3.ClientSideEvents.ButtonClick = "function(s,e){ DetajimArtikulli_Click(txtVlera" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + "); }";
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Prindi":
                                if (cmbKategoria.Text == "Grupimet e klient/furnitoreve")
                                {
                                    ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmb3);
                                    cmb3.ReadOnly = true;
                                    cmb3.ClientSideEvents.ButtonClick = "function(s,e){ PrindGrupeKF_Click(txtVlera" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + "); }";
                                    cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                }
                                else if (cmbKategoria.Text == "Kategori Shpenzimi")
                                {
                                    ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmb3);
                                    ConfigureAspxComboBox.KonfiguroComboBoxKategoriShpenzimi(cmb3, IdNdermarrja, true);
                                    cmb3.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedKategoriShpenzimi(txtVlera" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + ",false); }";
                                    cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                }
                                else
                                {
                                    ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmb3);
                                    cmb3.ReadOnly = true;
                                    cmb3.ClientSideEvents.ButtonClick = "function(s,e){ PrindKodifikim_Click(txtVlera" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + "); }";
                                    cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                }
                                break;
                            case "Debi/Kredi":
                                cmb3.Items.Add("Debi", 1);
                                cmb3.Items.Add("Kredi", 2);
                                cmb3.ClientInstanceName = "txtDebiKredi";
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtDebiKredi,'txtVlera', {0});}}", e.VisibleIndex);
                                break;

                            case "Lloji klient/furnitor":
                                cmb3.Items.Add("Klient", 1);
                                cmb3.Items.Add("Furnitor", 2);
                                cmb3.ClientSideEvents.SelectedIndexChanged = "function(s,e){ changeKlientFurnitor(txtVlera" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + "); }";
                                cmb3.ClientSideEvents.Init = "function(s,e){ changeKlientFurnitor(txtVlera" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + "); }";
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;

                            case "Lloji":
                                switch (cmbKategoria.Text)
                                {
                                    case "Arka":
                                    case "Veprime Arke":
                                        ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idperdorues, idNdermarrje, cmb3, 3, rm, ci, idGjuha);
                                        cmb3.ClientInstanceName = "txtVleraLlojDokumenti";
                                        cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVleraLlojDokumenti,'txtVlera', {0});}}", e.VisibleIndex);
                                        break;
                                    case "Banka":
                                    case "Veprime Banke":
                                        ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idperdorues, idNdermarrje, cmb3, 4, rm, ci, idGjuha);
                                        cmb3.ClientInstanceName = "txtVleraLlojDokumenti";
                                        cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVleraLlojDokumenti,'txtVlera', {0});}}", e.VisibleIndex);
                                        break;
                                    case "Receptura":
                                        cmb3.Items.Add("Artikull", 1);
                                        cmb3.Items.Add("Aktivitete", 2);
                                        cmb3.DataBind();
                                        cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                        break;
                                    case "Veprime Klient Furnitor":
                                        ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idperdorues, idNdermarrje, cmb3, 20, rm, ci, idGjuha);
                                        cmb3.ClientInstanceName = "txtVleraLlojDokumenti";
                                        cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVleraLlojDokumenti,'txtVlera', {0});}}", e.VisibleIndex);
                                        break;
                                    case "List MSISDN":
                                        cmb3.Items.Add(DbCore.DbRegjistrim.LlojMsisdn.DeviceWithDiscount.ToString(), 1);
                                        cmb3.Items.Add(DbCore.DbRegjistrim.LlojMsisdn.Bazaar.ToString(), 2);
                                        break;
                                    case "Detajime artikulli":
                                        cmb3.DataSource = KonfigurimComboGride.MerrItemsPerLlojDetajimArtikulli(rm, ci);
                                        cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                        break;
                                    case "Flete Kontabel":
                                        ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idperdorues, idNdermarrje, cmb3, 5, rm, ci, idGjuha);
                                        cmb3.ClientInstanceName = "txtVleraLlojDokumenti";
                                        cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVleraLlojDokumenti,'txtVlera', {0});}}", e.VisibleIndex);
                                        break;
                                    case "Perfitim Buxheti":
                                    case "Ekzekutim Buxheti":
                                    case "Planifikim Ekzekutim Buxheti":
                                        cmb3.Items.Add("Llogari", 3);
                                        cmb3.Items.Add("Artikull", 1);
                                        cmb3.ClientInstanceName = "cmbLlojiPerfitimBuxheti";
                                        cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(cmbLlojiPerfitimBuxheti,'txtVlera', {0});}}", e.VisibleIndex);
                                        break;
                                    case "Shperndarje shpenzimesh":
                                        ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idperdorues, idNdermarrje, cmb3, 7, rm, ci, idGjuha);
                                        break;
                                }
                                //if (cmbKategoria.Text == "Veprime Arke" || cmbKategoria.Text == "Veprime Banke")
                                //    break;
                                if (cmbKategoria.Text == "Artikuj afatshkurter/afatgjate")
                                {
                                    ConfigureAspxComboBox.mbushComboLlojArt(cmb3);
                                    cmb3.ClientSideEvents.SelectedIndexChanged = "function(s,e){ changeArtikull(txtVlera" + e.VisibleIndex.ToString() + ",'txtVlera', " + e.VisibleIndex.ToString() + "); }";
                                    cmb3.ClientSideEvents.Init = "function(s,e){ changeArtikull(txtVlera" + e.VisibleIndex.ToString() + ", 'txtVlera', " + e.VisibleIndex.ToString() + "); }";
                                }
                                if (cmbKategoria.Text == "Shitje" || cmbKategoria.Text == "Blerje")
                                {
                                    cmb3.Items.Add("Artikull", 1);
                                    cmb3.Items.Add("Makro", 2);
                                    cmb3.Items.Add("Llogari", 3);
                                }
                                if (cmbKategoria.Text == "Rivleresime Amortizimi")
                                {
                                    ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idperdorues, idNdermarrje, cmb3, 90, rm, ci, idGjuha);
                                }
                                if (cmbKategoria.Text == "Grupet e Artikujve" || cmbKategoria.Text == "Grupimet e klient/furnitoreve")
                                {
                                    cmb3.Items.Add("", 0);
                                    cmb3.Items.Add("Grupimi 1", 1);
                                    cmb3.Items.Add("Grupimi 2", 2);
                                    cmb3.Items.Add("Grupimi 3", 3);
                                    cmb3.ClientSideEvents.SelectedIndexChanged = "function(s,e){ changeLlojKodifikimi(txtVlera" + e.VisibleIndex.ToString() + ", 'txtVlera', " + e.VisibleIndex.ToString() + "); }";
                                    //cmb3.ClientSideEvents.Init = "function(s,e){ changeLlojKodifikimi(txtVlera" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + "); }";
                                }
                                if (cmbKategoria.Text == "Njesi Administrative")
                                {
                                    ConfigureAspxComboBox.mbushComboLlojMagazine(cmb3);
                                    cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                }
                                break;

                            case "Pike Shitje/Furnizimi":
                                if (cmbKategoria.Text == "Shitje")
                                {
                                    ConfigureAspxComboBox.mbushComboPikeShitjeFurnizimi(idNdermarrje, cmb3, true, false);
                                }
                                else if (cmbKategoria.Text == "Blerje")
                                {
                                    ConfigureAspxComboBox.mbushComboPikeShitjeFurnizimi(idNdermarrje, cmb3, false, false);
                                }
                                else
                                {
                                    cmb3.Items.Add("Pike Shitje", true);
                                    cmb3.Items.Add("Pike Furnizimi", false);
                                }
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Klient/Furnitor":
                                cmb3.Items.Add("Klient", true);
                                cmb3.Items.Add("Furnitor", false);
                                cmb3.ClientSideEvents.SelectedIndexChanged = "function(s,e){ changeKlientFurnitor(txtVlera" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + "); }";
                                cmb3.ClientSideEvents.Init = "function(s,e){ changeKlientFurnitor(txtVlera" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + "); }";
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Klient/Furnitori vartes":
                                ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmb3);
                                switch (cmbKategoria.Value.ToString())
                                {
                                    case "1": ConfigureAspxComboBox.mbushComboKlientFurnitori(idperdorues, idNdermarrje, cmb3, 1); break;
                                    case "2": ConfigureAspxComboBox.mbushComboKlientFurnitori(idperdorues, idNdermarrje, cmb3, 2); break;
                                    default: ConfigureAspxComboBox.mbushComboKlientFurnitori(idperdorues, idNdermarrje, cmb3, 0); break;
                                }

                                cmb3.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedShitjeBlerje(txtVlera" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + "); }";
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Llogari Amortizimi":
                            case "Llogari":
                            case "Llogari Kunderparti":
                            case "Llogari kunderparti":
                            case "Llogari Inventari":
                            case "Llogari Blerje":
                            case "Llogari Shitje":
                            case "Llogari tek te Tretet":
                            case "Llogari Shpenzimi":
                            case "Nr Llogari":
                            case "Llogari Pakesimi":
                            case "Llogari Rezerve":
                            case "Llogari Pakesim Rezerve":
                            case "Llogari Komisioni":
                            case "Zeri":
                            case "Llogaria":
                            case "Nr llogari pagese":
                                ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmb3);
                                //DbCore.clsFunksione.mbushComboLlogariaPaKolona(idperdorues, idNdermarrje, cmb3);
                                cmb3.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedLL(txtVlera" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + "); }";
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Skema":
                                ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmb3);
                                bool afatgjate = false; //cmbNivelRegj.Text == "Artikuj Afatgjate" ? true : false;
                                ConfigureAspxComboBox.mbushComboSkemaKontabilitetiArtikulli(idNdermarrje, cmb3, "afatshkurter", "");
                                ConfigureAspxComboBox.shtoKolonaSkemaArtikulli(cmb3, afatgjate, idNdermarrje);
                                cmb3.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedSKA(txtVlera" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + "); }";
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Metode Kostoje":
                                ConfigureAspxComboBox.mbushComboMetodeKostoje(cmb3, false);
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Roli":
                                if(cmbKategoria.Value.ToString() == "21")
                                {
                                    ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmb3);
                                    ConfigureAspxComboBox.mbushComboRolet(cmb3, idperdorues, false);
                                    cmb3.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedRole(txtVlera" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + ",false); }";
                                    cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);

                                }
                                else {
                                    ConfigureAspxComboBox.mbushComboStatusi(cmb3, ci, rm);
                                    cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                }
                                break;
                            case "Tip Kontrate":
                                ConfigureAspxComboBox.mbushComboTipKontrate(cmb3, idNdermarrje, idGjuha);
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Kode profesione":
                                ConfigureAspxComboBox.mbushComboKodeProfesione(cmb3, idNdermarrje);
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Grupim Punonjesish":
                                ConfigureAspxComboBox.mbushComboGrupePunonjesish(cmb3, idNdermarrje);
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;

                            case "Departamenti":
                                ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmb3);
                                cmb3.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedDep(txtVlera" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + ",false); }";
                                ConfigureAspxComboBox.mbushComboStrukturaAdm(cmb3, 0, idNdermarrje);
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Nendepartamenti":
                                ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmb3);
                                cmb3.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedNendep(txtVlera" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + ",false); }";
                                // DbCore.clsFunksione.mbushComboStrukturaAdm(cmb3, 0, idNdermarrje);
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Qendra Kosto 1":
                                ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmb3);
                                cmb3.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedQK1(txtVlera" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + ",false); }";
                                //   DbCore.clsFunksione.mbushComboQendraKostoPrindNiveli1(idNdermarrje, cmb3);
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Qendra Kosto 2":
                                ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmb3);
                                cmb3.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedQK2(txtVlera" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + ",false); }";
                                //    DbCore.clsFunksione.mbushComboQendraKostoBij(idNdermarrje, cmb3);
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Grupim Global":
                                ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmb3);
                                cmb3.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedGlobal(txtVlera" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + ",false); }";
                                //   DbCore.clsFunksione.mbushComboQendraKostoPrindNiveli1(idNdermarrje, cmb3);
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Grupim Local":
                                ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmb3);
                                cmb3.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedLocal(txtVlera" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + ",false); }";
                                //    DbCore.clsFunksione.mbushComboQendraKostoBij(idNdermarrje, cmb3);
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Profesioni":
                                ConfigureAspxComboBox.mbushComboProfesioneTituj(cmb3, idNdermarrje, 1, idGjuha);
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Pozicioni":
                                ConfigureAspxComboBox.mbushComboProfesioneTituj(cmb3, idNdermarrje, 2, idGjuha);
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Ndryshim Pozicioni":
                                ConfigureAspxComboBox.mbushComboNdryshimPozicioni(cmb3, idGjuha);
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Dege Administrative":
                                ConfigureAspxComboBox.KonfiguroComboBoxDegeAdministrative(idNdermarrje, cmb3, false);
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Statusi":
                                ConfigureAspxComboBox.mbushComboStatusMagazine(idNdermarrje, cmb3);
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Kod Artikulli":
                            case "Kod artikulli":
                                //cmb3.ReadOnly = true;
                                ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmb3);
                                ConfigureAspxComboBox.mbushComboArtikulli(idperdorues, idNdermarrje, rm.GetString("postStringTvsh", ci), cmb3);
                                cmb3.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedArt(txtVlera" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + "); }";
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Niveli":
                                ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmb3);
                                if (cmbKategoria.Value.ToString() == "18")
                                {
                                    cmb3.ConfigureAndFill(() =>  colNiveleZbritjesh.merrNiveleZbritjeshNdermarjeDT(idNdermarrje), "KodNivelZbritje", "IdNivelZbritje");
                                    cmb3.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedNivelZbritje(txtVlera" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + ",false); }";
                                    cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                }
                                else
                                {
                                    ConfigureAspxComboBox.mbushComboNiveleCmimeshSipasLlojit(idNdermarrje, cmb3, 0, idperdorues);
                                    cmb3.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedNivelCmimi(txtVlera" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + ",false); }";
                                    cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                }
                                break;
                            case "Objektiva kosto":
                            case "Objektiva Kosto":
                            case "Objektiva e kostos":
                                ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmb3);
                                ConfigureAspxComboBox.KonfiguroComboBoxObjektivaKosto(cmb3, IdNdermarrja);
                                cmb3.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedObjektiva(txtVlera" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + ",false); }";
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Lloj qendre":
                            case "Lloj Qendre":
                                ConfigureAspxComboBox.mbushComboLlojQendrePerImport(cmb3, true, rm, ci);
                                cmb3.ClientSideEvents.Init = "function(s,e){ MerrVlere(txtVlera" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + ",false); }";
                                cmb3.ClientSideEvents.SelectedIndexChanged = "function(s,e){ MerrVlere(txtVlera" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + ",false); }";
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Qender kosto":
                            case "Qendra e Kostos":
                                ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmb3);
                                cmb3.ReadOnly = true;
                                cmb3.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedQendra(txtVlera" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + ",false); }";
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Karta":
                                ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmb3);
                                cmb3.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedKarta(txtVlera" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + ",false); }";
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;

                            case "Magazina":
                            case "Njesi Vartese":
                            case "Mag destinacion":
                            case "Magazina e Receptures":
                            case "Magazina e Produktit":
                                ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmb3);
                                hfMag.Value = DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod("LP/Mag", idNdermarrje).ToString();
                                if (cmbKategoria.Value != null && cmbKategoria.Value.ToString() == "135")
                                {
                                    ConfigureAspxComboBox.mbushComboMagazinat(idNdermarrje, cmb3, idperdorues, false, 1, true);

                                }
                                else if (cmbKategoria.Value != null && cmbKategoria.Value.ToString() == "136")
                                {
                                    ConfigureAspxComboBox.mbushComboMagazinat(idNdermarrje, cmb3, idperdorues, false, 2, true);
                                    hfMag.Value = DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod("LP/Mag2", idNdermarrje).ToString();
                                }
                                else ConfigureAspxComboBox.mbushComboMagazinat(idNdermarrje, cmb3, idperdorues, false, 0, true);
                                cmb3.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedMagazina(txtVlera" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + ",false," + hfMag.Value + "); }";
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Kod punonjesi":
                                ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmb3);
                                DbCore.DbListPagesat.colPunonjes pun = new DbCore.DbListPagesat.colPunonjes(idNdermarrje);
                                cmb3.DataSource = pun;
                                cmb3.ValueField = "IdPunonjes";
                                cmb3.TextField = "NrPersonal";
                                cmb3.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                                cmb3.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedPunonjes(txtVlera" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + ",false); }";
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Komponente":
                                if (cmbKategoria.Value != null)
                                {
                                    DbCore.DbListPagesat.colKomponentePage colc = new DbCore.DbListPagesat.colKomponentePage();
                                    if (cmbKategoria.Value.ToString() == "99")
                                        colc.ktheGjitheKomponentePageSipasNdermarjesDheLlojitDheDatesMeTeAfertFormule(idNdermarrje, true, DateTime.Today);
                                    else if (cmbKategoria.Value.ToString() == "111" || cmbKategoria.Value.ToString() == "115")
                                        colc.ktheGjitheKomponentePageSipasNdermarjesDheLlojitDheDatesMeTeAfertNr(idNdermarrje, true, DateTime.Today);
                                    else colc.ktheGjitheKomponentePageSipasNdermarjesDheLlojitDheDatesMeTeAfertNr(idNdermarrje, false, DateTime.Today);
                                    cmb3.DataSource = colc;
                                    cmb3.ValueField = "IdKomponentePage";
                                    cmb3.TextField = "Kodi";
                                    cmb3.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                                    cmb3.DataBind();
                                    cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                }
                                break;
                            case "Lloji i garancise":
                                ConfigureAspxComboBox.mbushComboGarancite(cmb3);
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Grupi":
                                ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmb3);
                                ConfigureAspxComboBox.mbushComboGrupeLlogarish(idNdermarrje, cmb3, DbCore.mySessionObjects.ktheGjuhe(Session));
                                cmb3.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedGrupi(txtVlera" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + ",false); }";
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Nengrupi":
                                ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmb3);
                                cmb3.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedNengrupi(txtVlera" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + ",false); }";
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Struktura 1":
                                ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmb3);
                                ConfigureAspxComboBox.mbushComboKPFBij(idperdorues, idNdermarrje, cmb3, 1);
                                cmb3.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedKpf1(txtVlera" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + ",false); }";
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Struktura 2":
                                ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmb3);
                                ConfigureAspxComboBox.mbushComboKPFBij(idperdorues, idNdermarrje, cmb3, 2);
                                cmb3.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedKpf2(txtVlera" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + ",false); }";
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Struktura 3":
                                ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmb3);
                                ConfigureAspxComboBox.mbushComboKPFBij(idperdorues, idNdermarrje, cmb3, 3);
                                cmb3.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedKpf3(txtVlera" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + ",false); }";
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Kategori Shpenzimi":
                                ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmb3);
                                ConfigureAspxComboBox.KonfiguroComboBoxKategoriShpenzimi(cmb3, IdNdermarrja, false);
                                cmb3.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedKategoriShpenzimi(txtVlera" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + ",false); }";
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Niveli Tvsh":
                            case "TVSH":
                                ConfigureAspxComboBox.KonfiguroComboBoxTaksat(idperdorues, idNdermarrje, cmb3, DbCore.DbRegjistrim.LlojTakse.Nivel_Tvsh, true);
                                cmb3.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedNivelTvsh(txtVlera" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + ",false); }";
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Standarti i amortizimit":
                                ConfigureAspxComboBox.mbushComboStandartAmortizimiKodi(cmb3, idNdermarrje);
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Metode amortizimi":
                                DbCore.DbAsete.colAseteLlojAmortizimi colas = new DbCore.DbAsete.colAseteLlojAmortizimi();
                                colas.merrTeGjithaLlojAmortizimesh();
                                cmb3.DataSource = colas;
                                cmb3.TextField = "LlojAmortizimi";
                                cmb3.ValueField = "IdLlojAmortizimi";
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Metode Amortizimi":
                                DbCore.DbAsete.colAseteLlojAmortizimi cola = new DbCore.DbAsete.colAseteLlojAmortizimi();
                                cola.merrTeGjithaLlojAmortizimesh();
                                cmb3.DataSource = cola;
                                cmb3.TextField = "LlojAmortizimi";
                                cmb3.ValueField = "IdLlojAmortizimi";
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Lloji i normes":
                                cmb3.Items.Add("Artikull", false);
                                cmb3.Items.Add("Magazine", true);
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Lloj zbritje":
                                cmb3.Items.Add("", 0);
                                cmb3.Items.Add("Perqindje", 1);
                                cmb3.Items.Add("Vlere", 2);
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Perdoruesi":
                                ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmb3);
                                cmb3.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedPerdorues(txtVlera" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + ",false); }";
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Standarti":
                                ConfigureAspxComboBox.mbushComboStandartAmortizimi(cmb3, idNdermarrje);
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Njesi Prodhimi":
                                ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmb3);
                                ConfigureAspxComboBox.mbushComboNjesiProdhimi(idNdermarrje, cmb3, false);
                                cmb3.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedNjesiProdhimi(txtVlera" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + "); }";
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Lloji i Receptures":
                                cmb3.Items.Add("Artikull", 1);
                                cmb3.Items.Add("Burim", 2);
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Autorizimi":
                                ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmb3);
                                cmb3.ReadOnly = true;
                                cmb3.ClientSideEvents.ButtonClick = "function(s,e){ Autorizimi_Click(txtVlera" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + "); }";
                                cmb3.ClientSideEvents.TextChanged = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Lloj Layeri":
                                DbCore.DbGIS.clsDatabaseGIS dbGis = new DbCore.DbGIS.clsDatabaseGIS();
                                cmb3.DataSource = dbGis.ktheGjitheLlojLayerMagazine();
                                dbGis.Dispose();
                                cmb3.ValueField = "IDLLOJLAYER";
                                cmb3.TextField = "PERSHKRIMI";
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Gjinia":
                                if (cmbKategoria.Value.ToString() != "21")
                                {
                                    ConfigureAspxComboBox.mbushComboGjinia(cmb3, ci, rm);
                                    cmb3.ClientSideEvents.TextChanged = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                }
                                else ConfigureAspxComboBox.mbushComboGjini(cmb3, ci, rm);
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Edukimi":
                                ConfigureAspxComboBox.mbushComboEdukimi(cmb3, idNdermarrje, idGjuha);
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Puna Meparshme":
                                ConfigureAspxComboBox.KonfiguroComboBoxPunaMeparshme(cmb3);
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Vendndodhjet":
                                ConfigureAspxComboBox.mbushComboVendndodhjet(idNdermarrje, cmb3);
                                cmb3.TextField = "Kodi";
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Kombesia":
                                ConfigureAspxComboBox.mbushComboKombesi(cmb3, idGjuha);
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;

                            case "Furnitori Kryesor":
                                ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmb3);
                                ConfigureAspxComboBox.mbushComboKlientFurnitori(idperdorues, idNdermarrje, cmb3, 2);
                                cmb3.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedKF(txtVlera" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + "); }";
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Nivel TVSH-je":
                                DbCore.DbRegjistrim.colTaksa col = new DbCore.DbRegjistrim.colTaksa(idNdermarrje, DbCore.DbRegjistrim.LlojTakse.Nivel_Tvsh, idperdorues);
                                cmb3.DataSource = col;
                                cmb3.ValueField = "IdTaksa";
                                cmb3.TextField = "KodTaksa";
                                cmb3.DataBind();
                                cmb3.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Kategori Detajimi":
                            case "Kategori Detajimi 2":
                                ConfigureAspxComboBox.mbushComboKategoriDetajimesh(cmb3);
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Klasa":
                                ConfigureAspxComboBox.mbushComboKlasa(cmb3, false);
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;

                            case "Titulli":
                                ConfigureAspxComboBox.KonfiguroComboBoxTitulliKlientFurnitor(cmb3);
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Lloj Adrese":
                                ConfigureAspxComboBox.KonfiguroComboBoxLlojAdresa(cmb3);
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Qyteti":
                                ConfigureAspxComboBox.KonfiguroComboBoxComboQytete(IdNdermarrja, cmb3);
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Menyre pagese":
                                ConfigureAspxComboBox.KonfiguroComboBoxMetoda(cmb3, true);
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                         
                            case "Kategori Zbritje":
                            case "Kategori zbritje":
                                ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmb3);
                                ConfigureAspxComboBox.KonfiguroComboBoxKategoriZbritje(IdNdermarrja, cmb3);
                                cmb3.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedKategoriZbritje(txtVlera" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + "); }";
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Nivel Cmimi":
                                ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmb3);
                                ConfigureAspxComboBox.mbushComboNiveleCmimeshPrind(idNdermarrje, cmb3);
                                cmb3.TextFormatString = "{1}";
                                cmb3.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedNivelCmimi(txtVlera" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + ",true); }";
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Zbritje Analitike":
                                ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmb3);
                                ConfigureAspxComboBox.KonfiguroComboBoxNiveleZbritjePrind(IdNdermarrja, cmb3);
                                cmb3.TextFormatString = "{1}";
                                cmb3.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedNivelZbritje(txtVlera" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + "); }";
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Maturimi":
                                ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmb3);
                                ConfigureAspxComboBox.KonfiguroComboBoxMaturimi(IdNdermarrja, cmb3);
                                cmb3.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedMaturimi(txtVlera" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + "); }";
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Politike":
                                ConfigureAspxComboBox.KonfiguroComboBoxPolitika(cmb3, IdNdermarrja, false);
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Kategori":
                                cmb3.DataSource = KonfigurimComboGride.MerrItemsPerKategoriDetajimi(rm, ci);
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                    break;
                            case "Lloj Zbritje Totale":
                                cmb3.Items.Add("", 0);
                                cmb3.Items.Add("Perqindje", 1);
                                cmb3.Items.Add("Vlere", 2);
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Format Seriali":
                                ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmb3);
                                ConfigureAspxComboBox.mbushComboFormateSeriali(idNdermarrje, cmb3);
                                cmb3.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedFormatSeriali(txtVlera" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + "); }";
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;                            

                            case "Lloji i veprimit":
                                cmb3.Items.Add("", 0);
                                cmb3.Items.Add("Shtim", 1);
                                cmb3.Items.Add("Modifikim", 2);
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;

                            case "Gjuha":
                                ConfigureAspxComboBox.mbushComboGjuha(cmb3);
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Status":
                                ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmb3);
                                ConfigureAspxComboBox.mbushShopsHierarkiStatus(cmb3);
                                cmb3.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedStatus(txtVlera" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + ",false); }";
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;

                            case "Leave Reason":
                                ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmb3);
                                ConfigureAspxComboBox.mbushShopsHierarkiLeaveReason(cmb3);
                                cmb3.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedLeaveReason(txtVlera" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + ",false); }";
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;

                            case "Uniform":
                                ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmb3);
                                ConfigureAspxComboBox.mbushShopsHierarkiUniform(cmb3);
                                cmb3.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedUniform(txtVlera" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + ",false); }";
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;

                            case "Status aprovimi":
                                ConfigureAspxComboBox.mbushComboStatusAprovimi(cmb3, ci, rm);
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;

                            case "I Siguruar (Po/Jo)":
                                ConfigureAspxComboBox.mbushComboISiguruar(cmb3, ci, rm);
                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                            case "Operatori":
                                clsKokaShitje kok = new clsKokaShitje();
                                var Op = kok.ktheKodOperatori(idNdermarrje);
                                cmb3.Items.Add("");
                                for (var i = 0; i < Op.Rows.Count; i++)
                                {
                                    
                                    if (Op.Rows[i].ItemArray[0].ToString() == " ()")
                                        cmb3.Items.Add("");
                                    else
                                    {
                                        cmb3.Items.Add(Op.Rows[i].ItemArray[0].ToString());
                                        cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                    }
                                }
                                break;
                            case "E-invoice Type":
                                clsKokaShitje koka = new clsKokaShitje();
                                var dt = koka.ktheKodTipiEinvoice();
                                for (var i = 0; i < dt.Rows.Count; i++)
                                {
                                    if (dt.Rows[i].ItemArray[0].ToString() == " ()")
                                        cmb3.Items.Add("");
                                    else 
                                    { 
                                        cmb3.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                                       cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                    }
                                }
                                break;
                            case "Procesi":
                                clsKokaShitje koke = new clsKokaShitje();
                                var dtp = koke.ktheKodProcesi();
                                for (var i = 0; i < dtp.Rows.Count; i++)
                                {
                                    if (dtp.Rows[i].ItemArray[0].ToString() == " ()")
                                        cmb3.Items.Add("");
                                    else
                                    {
                                        cmb3.Items.Add(dtp.Rows[i].ItemArray[0].ToString());
                                        cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                    }
                                }
                                break;
                            case "Tipi i vetefaturimit":
                                cmb3.Items.Add("", 0);
                                cmb3.Items.Add("AGREEMENT", 1);
                                cmb3.Items.Add("DOMESTIC", 2);
                                cmb3.Items.Add("ABROAD", 3);
                                cmb3.Items.Add("SELF", 4);
                                cmb3.Items.Add("OTHER", 5);

                                cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                                break;
                                //case "Gjinia":
                                //    DbCore.clsFunksione.mbushComboGjinia(cmb3);
                                //    break;

                        }
                        cmb3.DropDownStyle = DropDownStyle.DropDown;
                    }
                }
                else if (trupavis[e.VisibleIndex].TipKontrolli == 3)
                {
                    ASPxDateEdit cmb3 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col2, "cal") as ASPxDateEdit;
                    if (cmb3 != null)
                    {
                        cmb3.ClientInstanceName = "txtVlera" + e.VisibleIndex;
                        cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                    }
                }
                else if (trupavis[e.VisibleIndex].TipKontrolli == 11)
                {
                    ASPxTimeEdit cmb3 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col2, "txtBox") as ASPxTimeEdit;
                    if (cmb3 != null)
                    {
                        cmb3.ClientInstanceName = "txtVlera" + e.VisibleIndex;
                        cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                    }
                }
                else if (trupavis[e.VisibleIndex].TipKontrolli == 8)
                {
                    ASPxCheckBox cmb3 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col2, "cb") as ASPxCheckBox;
                    if (cmb3 != null)
                    {
                        cmb3.ClientInstanceName = "txtVlera" + e.VisibleIndex;
                        cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{CheckedChanged(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                    }
                }
                else
                {
                    ASPxTextBox cmb3 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col2, "txtBox") as ASPxTextBox;
                    if (cmb3 != null)
                    {
                        cmb3.ClientInstanceName = "txtVlera" + e.VisibleIndex;
                        cmb3.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedEmri(txtVlera{0},'txtVlera', {0});}}", e.VisibleIndex);
                    }
                }
                if (lbl != null)
                {
                    if (trupavis[e.VisibleIndex].DetyrueshmeDefault)
                        lbl.ForeColor = Color.Red;
                }
                if (txt12 != null)
                {

                    txt12.ClientInstanceName = "txtEmri" + e.VisibleIndex;
                    if (Convert.ToBoolean(hfState.Get("FormatLidhurSQL")))
                        txt12.ClientEnabled = false;
                    else
                        txt12.ClientEnabled = true;
                    txt12.ClientSideEvents.TextChanged = String.Format("function(s,e){{TextChangedEmri(txtEmri{0},'txtEmri', {0});}}", e.VisibleIndex);
                }
                if (txt2 != null)
                {
                    if (trupavis[e.VisibleIndex].DetyrueshmeDefault)
                        txt2.Enabled = false;
                    txt2.ClientInstanceName = "cbDetyrueshem" + e.VisibleIndex;
                    txt2.ClientSideEvents.CheckedChanged = String.Format("function(s,e){{CheckedChanged(cbDetyrueshem{0},'cbDetyrueshem', {0});}}", e.VisibleIndex);
                    if (Convert.ToBoolean(hfState.Get("FormatLidhurSQL")))
                        txt2.ClientEnabled = false;
                    else
                        txt2.ClientEnabled = true;
                }
                if (txt5 != null)
                {
                    txt5.ClientInstanceName = "cbShfaq" + e.VisibleIndex;
                    txt5.ClientSideEvents.CheckedChanged = String.Format("function(s,e){{CheckedChanged(cbShfaq{0},'cbShfaq', {0});}}", e.VisibleIndex);
                    if (Convert.ToBoolean(hfState.Get("FormatLidhurSQL")))
                        txt5.ClientEnabled = false;
                    else
                        txt5.ClientEnabled = true;
                }
            }
            if (e.VisibleIndex + 1 < trupavis.Count)
            {
                if (trupavis[e.VisibleIndex + 1].KodKontrolli == "Kodbari")
                    col2.DataItemTemplate = new MyTextTemplate();
                else if (trupavis[e.VisibleIndex + 1].TipKontrolli == 2 || trupavis[e.VisibleIndex + 1].TipKontrolli == 4)
                    col2.DataItemTemplate = new MyComboTemplate();
                else if (trupavis[e.VisibleIndex + 1].TipKontrolli == 3)
                    col2.DataItemTemplate = new MyCalendarTemplate();
                else if (trupavis[e.VisibleIndex + 1].TipKontrolli == 12)
                    col2.DataItemTemplate = new MySpinTemplate();
                else if (trupavis[e.VisibleIndex + 1].TipKontrolli == 8)
                    col2.DataItemTemplate = new MyCheckTemplate(false, false);
                else col2.DataItemTemplate = new MyTextTemplate();
            }
            else if (trupavis.Count > 0)
                if (trupavis[0].KodKontrolli == "Kodbari")
                    col2.DataItemTemplate = new MyTextTemplate();
                else if (trupavis[0].TipKontrolli == 2 || trupavis[0].TipKontrolli == 4)
                    col2.DataItemTemplate = new MyComboTemplate();
                else if (trupavis[0].TipKontrolli == 3)
                    col2.DataItemTemplate = new MyCalendarTemplate();
                else if (trupavis[0].TipKontrolli == 11)
                    col2.DataItemTemplate = new MySpinTemplate();
                else if (trupavis[0].TipKontrolli == 8)
                    col2.DataItemTemplate = new MyCheckTemplate(false, false);
                else col2.DataItemTemplate = new MyTextTemplate();
            else
            {
                colTrupiFormatImporti trupainvis = DbCore.mySessionObjects.merrFormatImportiInVisibleNgaSesioni(Session);
                int index = lbxFushat.SelectedIndex;
                if (index >= 0 && index < trupainvis.Count)
                {
                    if (trupainvis[index].KodKontrolli == "Kodbari")
                        col2.DataItemTemplate = new MyTextTemplate();
                    else if (trupainvis[index].TipKontrolli == 2 || trupainvis[index].TipKontrolli == 4)
                        col2.DataItemTemplate = new MyComboTemplate();
                    else if (trupainvis[index].TipKontrolli == 3)
                        col2.DataItemTemplate = new MyCalendarTemplate();
                    else if (trupainvis[index].TipKontrolli == 11)
                        col2.DataItemTemplate = new MySpinTemplate();
                    else if (trupainvis[index].TipKontrolli == 8)
                        col2.DataItemTemplate = new MyCheckTemplate(false, false);
                    else col2.DataItemTemplate = new MyTextTemplate();
                }
            }
        }

        protected void lbxFushat_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            if (IsCallback)
            {
                colTrupiFormatImporti trupatinvis = new colTrupiFormatImporti();

                if (Request.Params["__CALLBACKID"].ToString().Contains("lbxFushat"))
                {
                    if (e.Parameter.ToString().Contains("pastro"))
                    {
                        trupatinvis = new colTrupiFormatImporti();
                    }
                    DbCore.mySessionObjects.ruajFormatImportiInVisibleNeSession(Session, trupatinvis);
                    lbxFushat.DataSource = trupatinvis;
                    lbxFushat.ValueField = "IdTrupi";
                    lbxFushat.TextField = "KodKontrolli";
                    lbxFushat.DataBind();
                }
            }
        }

        protected void btnDjathtas1_Click(object sender, EventArgs e)
        {
            colTrupiFormatImporti trupavis = DbCore.mySessionObjects.merrFormatImportiVisibleNgaSesioni(Session);
            colTrupiFormatImporti trupainvis = DbCore.mySessionObjects.merrFormatImportiInVisibleNgaSesioni(Session);
            int index = lbxFushat.SelectedIndex;
            if (index >= 0 && index < trupainvis.Count)
            {
                trupavis.Add(trupainvis[index]);
                trupainvis.RemoveAt(index);
            }

            DbCore.mySessionObjects.ruajFormatImportiVisibleNeSession(Session, trupavis);
            DbCore.mySessionObjects.ruajFormatImportiInVisibleNeSession(Session, trupainvis);
            gvZgjedhur.DataSource = trupavis;
            gvZgjedhur.DataBind();
            lbxFushat.DataSource = trupainvis;
            lbxFushat.DataBind();
            lbxFushat.SelectedIndex = index;
            percaktoTemplate();
        }

        protected void btnDjathtasGjitha_Click(object sender, EventArgs e)
        {
            colTrupiFormatImporti trupavis = DbCore.mySessionObjects.merrFormatImportiVisibleNgaSesioni(Session);
            colTrupiFormatImporti trupainvis = DbCore.mySessionObjects.merrFormatImportiInVisibleNgaSesioni(Session);
            trupavis.AddRange(trupainvis);
            trupainvis = new colTrupiFormatImporti();
            DbCore.mySessionObjects.ruajFormatImportiVisibleNeSession(Session, trupavis);
            DbCore.mySessionObjects.ruajFormatImportiInVisibleNeSession(Session, trupainvis);
            gvZgjedhur.DataSource = trupavis;
            gvZgjedhur.DataBind();
            lbxFushat.DataSource = trupainvis;
            lbxFushat.DataBind();
            percaktoTemplate();
        }

        protected void btnMajtas1_Click(object sender, EventArgs e)
        {
            colTrupiFormatImporti trupavis = DbCore.mySessionObjects.merrFormatImportiVisibleNgaSesioni(Session);
            colTrupiFormatImporti trupainvis = DbCore.mySessionObjects.merrFormatImportiInVisibleNgaSesioni(Session);
            int count = trupavis.Count;
            int index = 0;
            for (int i = count - 1; i >= 0; i--)
            {
                if (gvZgjedhur.Selection.IsRowSelected(i))
                {
                    index = i;
                    if (!trupavis[i].DetyrueshmeDefault)
                    {
                        trupainvis.Add(trupavis[i]);
                        trupavis.RemoveAt(i);
                    }
                    else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Fushat e detyrueshme nuk mund te hiqen!", pnlMesazhi);
                }
            }
            DbCore.mySessionObjects.ruajFormatImportiVisibleNeSession(Session, trupavis);
            DbCore.mySessionObjects.ruajFormatImportiInVisibleNeSession(Session, trupainvis);
            gvZgjedhur.DataSource = trupavis;
            gvZgjedhur.DataBind();
            lbxFushat.DataSource = trupainvis;
            lbxFushat.DataBind();
            gvZgjedhur.Selection.UnselectAll();
            if (index > trupavis.Count - 1)
                index = trupavis.Count - 1;
            gvZgjedhur.Selection.SelectRow(index);
            gvZgjedhur.FocusedRowIndex = index;
        }

        private void percaktoTemplate()
        {
            colTrupiFormatImporti trupavis = DbCore.mySessionObjects.merrFormatImportiVisibleNgaSesioni(Session);
            GridViewDataTextColumn col0 = gvZgjedhur.Columns["KodKontrolli"] as GridViewDataTextColumn;
            col0.DataItemTemplate = new MyLabelTemplate();
            GridViewDataTextColumn col1 = gvZgjedhur.Columns["EmerImporti"] as GridViewDataTextColumn;
            col1.DataItemTemplate = new MyTextTemplate();
            GridViewDataTextColumn col2 = gvZgjedhur.Columns["VleraDefault"] as GridViewDataTextColumn;
            if (trupavis.Count > 0)
                if (trupavis[0].KodKontrolli == "Kodbari")
                    col2.DataItemTemplate = new MyTextTemplate();
                else if (trupavis[0].TipKontrolli == 2 || trupavis[0].TipKontrolli == 4 || (trupavis[0].TipKontrolli == 1 && trupavis[0].KodKontrolli == "Perdoruesi"))
                    col2.DataItemTemplate = new MyComboTemplate();
                else if (trupavis[0].TipKontrolli == 3)
                    col2.DataItemTemplate = new MyCalendarTemplate();
                else if (trupavis[0].TipKontrolli == 11)
                    col2.DataItemTemplate = new MySpinTemplate();
                else if (trupavis[0].TipKontrolli == 8)
                    col2.DataItemTemplate = new MyCheckTemplate(false, false);
                else col2.DataItemTemplate = new MyTextTemplate();
            else col2.DataItemTemplate = new MyTextTemplate();
            GridViewDataCheckColumn col3 = gvZgjedhur.Columns["Detyrueshme"] as GridViewDataCheckColumn;
            col3.DataItemTemplate = new MyCheckTemplate(false, false);
            GridViewDataCheckColumn col6 = gvZgjedhur.Columns["Shfaq"] as GridViewDataCheckColumn;
            col6.DataItemTemplate = new MyCheckTemplate(false, false);
        }

        protected void btnMajtaGjitha_Click(object sender, EventArgs e)
        {
            colTrupiFormatImporti trupavis = DbCore.mySessionObjects.merrFormatImportiVisibleNgaSesioni(Session);
            colTrupiFormatImporti trupainvis = DbCore.mySessionObjects.merrFormatImportiInVisibleNgaSesioni(Session);
            colTrupiFormatImporti trupiri = new colTrupiFormatImporti();
            foreach (clsTrupiFormatImporti trup in trupavis)
            {
                if (!trup.DetyrueshmeDefault)
                    trupainvis.Add(trup);
                else trupiri.Add(trup);
            }
            trupavis = trupiri;
            DbCore.mySessionObjects.ruajFormatImportiVisibleNeSession(Session, trupavis);
            DbCore.mySessionObjects.ruajFormatImportiInVisibleNeSession(Session, trupainvis);
            gvZgjedhur.DataSource = trupavis;
            gvZgjedhur.DataBind();
            lbxFushat.DataSource = trupainvis;
            lbxFushat.DataBind();
            gvZgjedhur.Selection.UnselectAll();
        }

        protected void btnSiper_Click(object sender, EventArgs e)
        {
            colTrupiFormatImporti trupavis = DbCore.mySessionObjects.merrFormatImportiVisibleNgaSesioni(Session);
            int index = gvZgjedhur.FocusedRowIndex;
            if (index > 0)
            {
                clsTrupiFormatImporti trup = trupavis.ElementAt<clsTrupiFormatImporti>(index);
                trupavis.RemoveAt(index);
                trupavis.Insert(index - 1, trup);
                gvZgjedhur.DataSource = trupavis;
                gvZgjedhur.DataBind();
                gvZgjedhur.FocusedRowIndex = index - 1;
                DbCore.mySessionObjects.ruajFormatImportiVisibleNeSession(Session, trupavis);
            }
        }

        protected void btnPoshte_Click(object sender, EventArgs e)
        {
            colTrupiFormatImporti trupavis = DbCore.mySessionObjects.merrFormatImportiVisibleNgaSesioni(Session);

            int index = gvZgjedhur.FocusedRowIndex;
            if (index != -1 && index < trupavis.Count-1)
            {
                clsTrupiFormatImporti trup = trupavis.ElementAt<clsTrupiFormatImporti>(index);
                trupavis.RemoveAt(index);
                trupavis.Insert(index + 1, trup);
                gvZgjedhur.DataSource = trupavis;
                gvZgjedhur.DataBind();
                gvZgjedhur.FocusedRowIndex = index + 1;
                DbCore.mySessionObjects.ruajFormatImportiVisibleNeSession(Session, trupavis);
            }
        }

        protected void lbxFushat_DataBound(object sender, EventArgs e)
        {

        }

        private colTrupiFormatImporti krijoTrup(int idkategori)
        {
            colTrupiFormatImporti trupi = new colTrupiFormatImporti();
            colTrupiFormatImporti trupavis = DbCore.mySessionObjects.merrFormatImportiVisibleNgaSesioni(Session);
            colTrupiFormatImporti trupainvis = DbCore.mySessionObjects.merrFormatImportiInVisibleNgaSesioni(Session);
            int i = 0;
            foreach (clsTrupiFormatImporti tru in trupavis)
            {
                IEnumerable<DbCore.DbAdmin.clsTrupiFormatImporti> t = from c in trupi
                                                                      where c.EmerImporti == tru.EmerImporti
                                                                      select c;
                if (t.Count<DbCore.DbAdmin.clsTrupiFormatImporti>() > 0)
                    throw new Exception("Nuk mund te kete dy fusha me te njejtin emer importi!");
                if (tru.EmerImporti == "")
                    throw new Exception("Nuk mund te kete fusha pa emertim!");
                tru.Rendi = i;
                tru.Visible = true;
                trupi.Add(tru);
                i++;
            }
            foreach (clsTrupiFormatImporti tru in trupainvis)
            {
                IEnumerable<DbCore.DbAdmin.clsTrupiFormatImporti> t = from c in trupi
                                                                      where c.EmerImporti == tru.EmerImporti
                                                                      select c;
                if (t.Count<DbCore.DbAdmin.clsTrupiFormatImporti>() > 0)
                    throw new Exception("Nuk mund te kete dy fusha me te njejtin emer importi!");
                tru.Rendi = i;
                tru.Visible = false;
                trupi.Add(tru);
                i++;
            }
            if (idkategori == 98)
            {
                string tr = trupi.Where(x => x.KodKontrolli == "Fillim periudhe").First().VleraDefault;
                string tr1 = trupi.Where(x => x.KodKontrolli == "Mbarim periudhe").First().VleraDefault;
                if (tr != "" && tr1 != "" && DateTime.Parse(tr) > DateTime.Parse(tr1))
                {
                    throw new MyException("Mbarimi i periudhes duhet te jete me e madhe se fillimi i saj!");
                }
            }
            return trupi;
        }

        protected void cmbKategoria_SelectedIndexChanged(object sender, EventArgs e)
        {

            if ((IsCallback && Request.Params["__CALLBACKID"].ToString().Contains("cmbKategoria")) || (IsPostBack && Request["__EVENTTARGET"].ToString().Contains("cmbKategoria")))
            {
                colTrupiFormatImporti trupatvis = new colTrupiFormatImporti();
                colTrupiFormatImporti trupatinvis = new colTrupiFormatImporti();
                gvZgjedhur.DataSource = null;
                gvZgjedhur.DataBind();

                if (cmbKategoria.Value != null)
                    switch (cmbKategoria.Value.ToString())
                    {
                        case "13":
                            trupatvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-1, true);
                            trupatinvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-1, false);
                            //hfState.Set("Artikull", false);
                            break;
                        case "67":
                            trupatvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-2, true);
                            trupatinvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-2, false);
                            break;
                        case "23":
                            trupatvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-3, true);
                            trupatinvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-3, false);
                            break;
                        case "31":
                            trupatvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-4, true);
                            trupatinvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-4, false);
                            break;
                        case "32":
                            trupatvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-5, true);
                            trupatinvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-5, false);
                            break;
                        case "37":
                            trupatvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-35, true);
                            trupatinvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-35, false);
                            break;
                        case "138":
                            trupatvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-36, true);
                            trupatinvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-36, false);
                            break;
                        case "139":
                            trupatvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-37, true);
                            trupatinvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-37, false);
                            break;
                        case "12":
                            trupatvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-6, true);
                            trupatinvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-6, false);
                            break;
                        case "17":
                            trupatvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-8, true);
                            trupatinvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-8, false);
                            break;
                        case "14":
                            trupatvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-9, true);
                            trupatinvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-9, false);
                            break;
                        case "93":
                            trupatvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-11, true);
                            trupatinvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-11, false);
                            break;
                        case "92":
                            break;
                        case "1":
                            trupatvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-12, true);
                            trupatinvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-12, false);
                            break;
                        case "2":
                            trupatvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-13, true);
                            trupatinvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-13, false);
                            break;
                        case "6":
                            trupatvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-16, true);
                            trupatinvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-16, false);
                            break;
                        case "90":
                            trupatvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-20, true);
                            trupatinvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-20, false);
                            break;
                        case "98":
                            trupatvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-22, true);
                            trupatinvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-22, false);
                            break;
                        case "99":
                            trupatvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-23, true);
                            trupatinvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-23, false);
                            break;
                        case "111":
                            trupatvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-27, true);
                            trupatinvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-27, false);
                            break;
                        case "112":
                            trupatvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-28, true);
                            trupatinvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-28, false);
                            break;
                        case "113":
                            trupatvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-29, true);
                            trupatinvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-29, false);
                            break;
                        case "114":
                            trupatvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-30, true);
                            trupatinvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-30, false);
                            break;
                        case "115":
                            trupatvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-31, true);
                            trupatinvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-31, false);
                            break;
                        case "133":
                            trupatvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-32, true);
                            trupatinvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-32, false);
                            break;
                        case "135":
                            trupatvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-33, true);
                            trupatinvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-33, false);
                            break;
                        case "136":
                            trupatvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-34, true);
                            trupatinvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-34, false);
                            break;
                        case "3":
                            trupatvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-24, true);
                            trupatinvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-24, false);
                            break;
                        case "4":
                            trupatvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-25, true);
                            trupatinvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-25, false);
                            break;
                        case "45":
                            trupatvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-26, true);
                            trupatinvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-26, false);
                            break;
                        case "18":
                            trupatvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-38, true);
                            trupatinvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-38, false);
                            break;
                        case "20":
                            trupatvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-39, true);
                            trupatinvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-39, false);
                            break;
                        case "117":
                            trupatvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-40, true);
                            trupatinvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-40, false);
                            break;
                        case "157":
                            trupatvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-46, true);
                            trupatinvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-46, false);
                            break;
                        case "71":
                            trupatvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-47, true);
                            trupatinvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-47, false);
                            break;
                        case "5":
                            trupatvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-52, true);
                            trupatinvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-52, false);
                            break;
                        case "21":
                            trupatvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-51, true);
                            trupatinvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-51, false);
                            break;
                        case "177":
                            trupatvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-53, true);
                            trupatinvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-53, false);
                            break;
                        case "172":
                            trupatvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-54, true);
                            trupatinvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-54, false);
                            break;
                        case "179":
                            trupatvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-56, true);
                            trupatinvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-56, false);
                            break;
                        case "163":
                            trupatvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-49, true);
                            trupatinvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-49, false);
                            break;
                        case "147":
                            trupatvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-41, true);
                            trupatinvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-41, false);
                            break;
                        case "164":
                            trupatvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-50, true);
                            trupatinvis = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDetyrueshme(-50, false);
                            break;

                        default:
                            trupatvis = new colTrupiFormatImporti();
                            trupatinvis = new colTrupiFormatImporti();
                            break;
                    }
                DbCore.mySessionObjects.ruajFormatImportiInVisibleNeSession(Session, trupatinvis);
                DbCore.mySessionObjects.ruajFormatImportiVisibleNeSession(Session, trupatvis);
                gvZgjedhur.DataSource = trupatvis;
                gvZgjedhur.DataBind();
                lbxFushat.DataSource = trupatinvis;
                lbxFushat.ValueField = "IdTrupi";
                lbxFushat.TextField = "KodKontrolli";
                lbxFushat.DataBind();
                percaktoTemplate();
            }
        }

        protected void cmbKategoria_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            ConfigureAspxComboBox.KonfiguroComboBoxKategoriPerImport(cmbKategoria, false, Convert.ToInt32(hfState.Get("idNdermarrje")),Convert.ToInt32(hfState.Get("idVitNdermarrje")), Convert.ToInt32(hfState.Get("idPerdoruesi")), komponente);
            cmbKategoria.SelectedIndex = -1;
        }
    }
}