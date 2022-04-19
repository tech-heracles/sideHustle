using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using DbCore;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.Resources;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using PlatinumWeb.Templates;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{
    public partial class CRMAnketa : MyPageBase
    {
        /// <summary>
        /// konstante per mesazhin e trupit nuk duhet te jete bosh
        /// </summary>
        private const string STR_TrupiIDokumentitNukDuhetTeJeteBosh = "Fushat e zgjedhura nuk duhet të jenë bosh!";
        private int idNdermarrje;

        /// <summary>
        /// mbush fushat gjate modifikimit te dokumentit
        /// </summary>
        /// <param name="idPerdoruesi">id e perdoruesit</param>
        /// <param name="idNdermarrje"> id e ndermarjes</param>
        /// <param name="koka">koka e ekzekutimit</param>
        public void MerrTedhenat(int idPerdoruesi, int idNdermarrje, DbCore.DbCRM.clsKokaAnketa koka)
        {

            txtKodi.Text = koka.Kodi;
            txtShenime.Text = koka.Pershkrimi;
            dteDtFillimi.Date = koka.DtFillimi;
            dteDtMbarimi.Date = koka.DtMbarimi;
            mbushTrupin(koka.IdKokaAnketa, idNdermarrje);
            hfState.Set("Artikull", false);
        }

        private void mbushTrupin(int id, int idndermarje)
        {
            DbCore.DbCRM.colTrupiAnketa trupatvis = new DbCore.DbCRM.colTrupiAnketa();
            trupatvis.mbushTrupiAnketeSipasIdKoka(id);

            DbCore.DbCRM.colTrupiAnketa trupatinvis = new DbCore.DbCRM.colTrupiAnketa();
            trupatinvis.merrTrupAnketeOpsioneTePamaraSipasNdermarje(idndermarje, id);
            gvZgjedhur.DataSource = trupatvis;
            DbCore.mySessionObjects.ruajFormatImportiVisibleNeSession(Session, trupatvis);
            DbCore.mySessionObjects.ruajFormatImportiInVisibleNeSession(Session, trupatinvis);
            lbxFushat.ValueField = "IdTrupiAnketa";
            lbxFushat.TextField = "Opsioni";
            lbxFushat.DataSource = trupatinvis;
            gvZgjedhur.DataBind();
            lbxFushat.DataBind();
        }
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
            }

             idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idviti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            int idgjuha = DbCore.mySessionObjects.ktheGjuhe(Session);

            if (!IsPostBack)
            {
                hfState.Set("idGjuha", idgjuha);
                hfState.Set("Artikull", false);
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
                        konfiguroVleraFillestareShto(idNdermarrje, rm, ci, idgjuha);
                    }
                    else if (Request.QueryString["shtim_modifikim"] == "modifikim" || Request.QueryString["shtim_modifikim"]=="klonim")
                    {
                        hfShtimModifikim.Value = Request.QueryString["shtim_modifikim"];
                        konfiguroVleraFillestareModifiko(idPerdoruesi, idNdermarrje, rm, ci, idgjuha);
                    }


                }
                else
                    if (hfShtimModifikim.Value == "shtim")
                        konfiguroVleraFillestareShto(idNdermarrje, rm, ci, idgjuha);
                    else
                        if (hfShtimModifikim.Value == "modifikim")
                            konfiguroVleraFillestareModifiko(idPerdoruesi, idNdermarrje, rm, ci, idgjuha);

                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idviti, "CRMAnketa.aspx");
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
               
                int id = Convert.ToInt32(Request.QueryString["id"]);
                lblUserEmri.Text = DbCore.mySessionObjects.ktheEmerPerdorues(Session);
            }
            else
                mbushNgaSessioni();
            percaktoTemplateMenu(idgjuha, idviti, idPerdoruesi, idNdermarrje, ASPxMenu1);
            konfiguroGride();
            percaktoTemplate();
            gvZgjedhur.Columns["#"].VisibleIndex = 0;
            gvZgjedhur.Columns["#"].Width = 50;
            gvZgjedhur.Columns["#"].HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
        }
        private void percaktoTemplateMenu(int idGjuha, int idViti, int idPerdorues, int idNdermarrje, ASPxMenu aSPxMenu1)
        {
            EventHandler handlerPerPo = new EventHandler(btnPo_Click);
            EventHandler handlerPerJo = new EventHandler(btnJo_Click);
            clsMenuInfo.ShtoMenuItemInfo(this, MenuInfo, handlerPerPo, handlerPerJo);
            clsToolbarConfig.percaktoTemplateMenu(idGjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1, "CRMAnketa.aspx", this, MenuInfo, null, null, handlerPerPo, handlerPerJo, hfShtimModifikim.Value == "modifikim" ? false : true, true, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session), false, true);
            if ((hfShtimModifikim.Value != "modifikim"))
            {
                aSPxMenu1.Items.FindByName("Fshi").ClientVisible = false;

            }
        }
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(Convert.ToInt32(hfState["idGjuha"]), Convert.ToInt32(hfState["idVitNdermarrje"]), Convert.ToInt32(hfState["idPerdoruesi"]), Convert.ToInt32(hfState["idNdermarrje"]), ASPxMenu1);
        }
        /// <summary>
        /// Vendos vlerat default kur po behet shtim
        /// </summary>
        /// <param name="idNdermarrje"></param>
        private void konfiguroVleraFillestareShto(int idNdermarrje, System.Resources.ResourceManager rm, System.Globalization.CultureInfo ci, int idGjuha)
        {
            ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(Convert.ToInt32(hfState["idPerdoruesi"]), idNdermarrje, cmbKonfigurimi, 103, rm, ci, idGjuha);
            cmbKonfigurimi.SelectedIndex = 0;
            hfKonffillestar.Value = cmbKonfigurimi.Text;

            mbushPopUpListeNgaDB();
        }

        /// <summary>
        /// Vendos vlerat default kur po behet modifikim
        /// </summary>
        /// <param name="idPerdoruesi"></param>
        /// <param name="idNdermarrje"></param>
        private void konfiguroVleraFillestareModifiko(int idPerdoruesi, int idNdermarrje, System.Resources.ResourceManager rm, System.Globalization.CultureInfo ci, int idGjuha)
        {//mbush kombot dhe gridat
            ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 103, rm, ci, idGjuha);
            cmbKonfigurimi.SelectedIndex = 0;

            hfKonffillestar.Value = cmbKonfigurimi.Text;
            int id = int.Parse(Request.QueryString["id"]);
            DbCore.DbCRM.clsKokaAnketa kok = new DbCore.DbCRM.clsKokaAnketa(id);

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

        public void btnJo_Click(object sender, EventArgs e)
        {

        }

        public void btnPo_Click(object sender, EventArgs e)
        {
            Page.Validate();
            ruajKonfigurim(1);
        }

        /// <summary>
        /// fshin dokumentin
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            DbCore.DbCRM.clsKokaAnketa kokam = new DbCore.DbCRM.clsKokaAnketa(int.Parse(Request.QueryString["id"]));
            clsMesazh mesazh = new clsMesazh();
            int idNdermarrje = Convert.ToInt32(hfState["idNdermarrje"]);
            kokam.IdModifikues = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
        
            if (kokam.kaVeprimeAnketa())
            {  // MessageBox.Show("Ka veprime me kete fusha");
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ka veprime me kete ankete", pnlMesazhi);
                return;
            }
            kokam.fshi();

            //mesazh = DbCore.DbCRM.clsKokaAnketa.fshi(kokam.IdKokaAnketa, kokam.IdModifikues);
            if (mesazh.Status)
            {
                Response.Redirect("CRMListaAnketa.aspx?fshi=po");
                status1.Value = "true";
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
            DbCore.DbCRM.clsKokaAnketa koka;
            try
            {
                if (IsValidAnketa())
                {
                    koka = krijoKonfigurim(statusDokumenti);

                    if (koka.ColTrupi.Count == 0)
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, STR_TrupiIDokumentitNukDuhetTeJeteBosh, pnlMesazhi, LoadingPanel);
                        status1.Value = "false";
                        return;
                    }
                }
                else return;
            }
            catch (Exception ex)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, ex.Message, pnlMesazhi, LoadingPanel);
                status1.Value = "false";
                return;
            }
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(Convert.ToInt32(hfState["idPerdoruesi"]), Convert.ToInt32(hfState["idNdermarrje"]), Convert.ToInt32(hfState["idVitNdermarrje"]), "CRMAnketa.aspx");


            if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim")
            {
                if (!tedrejtaInfo.DShtim)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                    status1.Value = "false";
                    return;
                }
                mesazh = koka.ruaj();
            }
            else
            {
                if (!tedrejtaInfo.DMod)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                    status1.Value = "false";
                    return;
                }
                koka.IdKokaAnketa = int.Parse(Request.QueryString["id"]);
                mesazh = koka.modifiko();
            }

            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi, LoadingPanel);
                status1.Value = "false";
                return;
            }

            clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            mbushPopUpListeNgaDB();
            hl = new HtmlTable();
            pnlLidhur.Update();
            status1.Value = "true";
            hfShtimModifikim.Value = "shtim";
        }

        private bool IsValidAnketa()
        {
            int id = -1;

            if (!clsFunksione.LejoVetemAlphaNumerik(txtKodi.Text))
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Kodi i anketes nuk eshte i rregullt!", pnlMesazhi);
                return false;
            }

            if (hfShtimModifikim.Value == "modifikim")
            {
                id = int.Parse(Request.QueryString["id"]);
                DbCore.DbCRM.clsKokaAnketa kok = new DbCore.DbCRM.clsKokaAnketa(id);
                if (kok == null)
                    return false;

                if (kok.DtFillimi == dteDtFillimi.Date && kok.DtMbarimi == dteDtMbarimi.Date)
                {
                    return true;
                }
                if (kok.DtFillimi == dteDtFillimi.Date && kok.DtMbarimi != dteDtMbarimi.Date)
                {
                    if (dteDtMbarimi.Date >= DateTime.Today && dteDtMbarimi.Date < kok.DtMbarimi)
                        return true;
                }
                //if (kok.DtFillimi <= DateTime.Today)
                //{
                //    if (dteDtMbarimi.Date > DateTime.Today && dteDtMbarimi.Date < kok.DtMbarimi)
                //    {
                //        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Data e Fillimit te kesaj ankete nuk mund te ndryshohet!", pnlMesazhi);
                //        dteDtFillimi.Date = kok.DtFillimi;
                //    }
                //    else
                //    {
                //        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Data e Fillimit dhe e Mbarimit te kesaj ankete nuk mund te ndryshohet!", pnlMesazhi);
                //        dteDtMbarimi.Date = kok.DtMbarimi;
                //        dteDtFillimi.Date = kok.DtFillimi;
                //    }
                //    return false;
                //}
                //else if (dteDtFillimi.Date <= DateTime.Today)
                //{
                //    if (dteDtMbarimi.Date > DateTime.Today && dteDtMbarimi.Date < kok.DtMbarimi)
                //    {
                //        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Data e Fillimit te kesaj ankete nuk mund te ndryshohet!", pnlMesazhi);
                //        dteDtFillimi.Date = kok.DtFillimi;
                //    }
                //    else
                //    {
                //        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Data e Fillimit dhe e Mbarimit te kesaj ankete nuk mund te ndryshohet!", pnlMesazhi);
                //        dteDtMbarimi.Date = kok.DtMbarimi;
                //        dteDtFillimi.Date = kok.DtFillimi;
                //    }
                //    return false;
                //}
                //else if (AnketaLidhur(id))
                //{
                //    if (dteDtMbarimi.Date > DateTime.Today && dteDtMbarimi.Date < kok.DtMbarimi)
                //    {
                //        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Anketa eshte e lidhur! Data e Fillimit te kesaj ankete nuk mund te ndryshohet!", pnlMesazhi);
                //        dteDtFillimi.Date = kok.DtFillimi;
                //    }
                //    else
                //    {
                //        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Anketa eshte e lidhur! Data e Fillimit dhe e Mbarimit te kesaj ankete nuk mund te ndryshohet!", pnlMesazhi);
                //        dteDtMbarimi.Date = kok.DtMbarimi;
                //        dteDtFillimi.Date = kok.DtFillimi;
                //    }
                //    return false;
                //}
            }
            return true;
        }

        /// <summary>
        /// Krijon nje objekt te tipit clsKokaAnketa
        /// </summary>
        /// <param name="statusDokumenti">Statusi i dokumentit (I rregullt ose draft)</param>

        /// <returns>Kthen nje objekt te tipit clsKokaAnketa</returns>
        private DbCore.DbCRM.clsKokaAnketa krijoKonfigurim(int statusDokumenti)
        {
            DbCore.DbCRM.clsKokaAnketa koka = new DbCore.DbCRM.clsKokaAnketa();
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DbCore.DbShare.clsKonfigurimAmbjenti clsKonf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            clsKonf.mbushKonfigDefaultKomponentes(2002, idNdermarrje);
            if (cmbKonfigurimi.Text != "") //cmbKonfigurimi.Value != null && 
            {
                clsKonf.mbushKonfigAmbjSipasKod(cmbKonfigurimi.Text, idNdermarrje);
            }
            try
            {

                koka = krijoKonfigurim(statusDokumenti, clsKonf, krijoTrup());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return koka;
        }

        /// <summary>
        /// Krijon nje objekt te tipit clsKokaAnketa 
        /// </summary>
        /// <param name="statusDokumenti">Statusi i dokumentit (I rregullt ose draft)</param>
        /// <returns>Kthen nje objekt te tipit DbCore.DbRegjistrim.clsKokaMagazina</returns>
        private DbCore.DbCRM.clsKokaAnketa krijoKonfigurim(int statusDokumenti, DbCore.DbShare.clsKonfigurimAmbjenti clsKonf, DbCore.DbCRM.colTrupiAnketa coltrupi)
        {
           
            DbCore.DbCRM.clsKokaAnketa mag = new DbCore.DbCRM.clsKokaAnketa(0, txtKodi.Text, txtShenime.Text, dteDtFillimi.Date, dteDtMbarimi.Date, statusDokumenti, Convert.ToInt32(hfState["idPerdoruesi"]), Convert.ToInt32(hfState["idPerdoruesi"]), Convert.ToInt32(hfState["idNdermarrje"]));
            mag.ColTrupi = coltrupi;
            return mag;
        }

        private void mbushPopUpListeNgaDB()
        {//mbush griden e popupit me te dhena     
            DbCore.DbCRM.colTrupiAnketa trupatvis = new DbCore.DbCRM.colTrupiAnketa(), trupatinvis = new DbCore.DbCRM.colTrupiAnketa();
            trupatinvis.merrTrupAnketeAllOpsioneSipasNdermarje(mySessionObjects.merrIdNdermarrjeSesioni(Session));
            gvZgjedhur.DataSource = trupatvis;
            DbCore.mySessionObjects.ruajFormatImportiVisibleNeSession(Session, trupatvis);
            DbCore.mySessionObjects.ruajFormatImportiInVisibleNeSession(Session, trupatinvis);
            lbxFushat.ValueField = "IdTrupiAnketa";
            lbxFushat.TextField = "Opsioni";
            lbxFushat.DataSource = trupatinvis;
            gvZgjedhur.DataBind();
            lbxFushat.DataBind();
        }
        private void mbushNgaSessioni()
        {
            DbCore.DbCRM.colTrupiAnketa trupavis = DbCore.mySessionObjects.merrAnketaVisibleNgaSesioni(Session);
            DbCore.DbCRM.colTrupiAnketa trupainvis = DbCore.mySessionObjects.merrAnketaInVisibleNgaSesioni(Session);
            gvZgjedhur.DataSource = trupavis;
            lbxFushat.ValueField = "IdTrupiAnketa";
            lbxFushat.TextField = "Opsioni";
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
                DbCore.DbCRM.colTrupiAnketa trupatvis = new DbCore.DbCRM.colTrupiAnketa();
                DbCore.DbCRM.colTrupiAnketa trupatinvis = new DbCore.DbCRM.colTrupiAnketa();

                if (Request.Params["__CALLBACKID"].ToString().Contains("gvZgjedhur"))
                {
                    mbushPopUpListeNgaDB();
                    percaktoTemplate();
                }
            }
        }

        private void konfiguroGride()
        {
            const string emriKomponentes = "CRMAnketa.aspx";
            DbCore.DbAdmin.clsKomponente oKomponente = new DbCore.DbAdmin.clsKomponente(emriKomponentes);
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(Convert.ToInt32(hfState["idNdermarrje"]), "gvZgjedhur", gvZgjedhur, cmbKonfigurimi.Text.Split(';')[0], oKomponente.IdKomponente.ToString(), Convert.ToInt32(hfState["idGjuha"]));
            GridUtil.konfiguroGrideRegjistrimEvogelPaTheme(gvZgjedhur, "IdTrupiAnketa");
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
                gvZgjedhur.KeyFieldName = "IdTrupiAnketa";
                gvZgjedhur.SettingsBehavior.AllowFocusedRow = true;
            }
        }

        protected void gvZgjedhur_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
        {
            //bool ugjet;
            if (e.RowType == GridViewRowType.Data)
            {
                DbCore.DbCRM.colTrupiAnketa trupavis = DbCore.mySessionObjects.merrAnketaVisibleNgaSesioni(Session);
                int idNdermarrje = Convert.ToInt32(hfState["idNdermarrje"]);
                int idperdorues = Convert.ToInt32(hfState["idPerdoruesi"]);
                int idGjuha = Convert.ToInt32(hfState["idGjuha"]);
                System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);

                GridViewDataCheckColumn col3 = ((ASPxGridView)sender).Columns["Detyrueshme"] as GridViewDataCheckColumn;

                GridViewDataTextColumn col0 = ((ASPxGridView)sender).Columns["Opsioni"] as GridViewDataTextColumn;

                ASPxLabel lbl = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col0, "lbl") as ASPxLabel;
                ASPxCheckBox txt2 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col3, "cb") as ASPxCheckBox;

                if (txt2 != null)
                {

                    txt2.ClientInstanceName = "cbDetyrueshem" + e.VisibleIndex;
                    txt2.ClientSideEvents.CheckedChanged = String.Format("function(s,e){{CheckedChanged(cbDetyrueshem{0},'cbDetyrueshem', {0});}}", e.VisibleIndex);

                }



            }
        }

        protected void lbxFushat_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            if (IsCallback)
            {
                DbCore.DbCRM.colTrupiAnketa trupatinvis = new DbCore.DbCRM.colTrupiAnketa();

                if (Request.Params["__CALLBACKID"].ToString().Contains("lbxFushat"))
                {
                    if (e.Parameter.ToString().Contains("pastro"))
                    {
                        mbushPopUpListeNgaDB();
                    }

                }
            }
        }

        protected void btnDjathtas1_Click(object sender, EventArgs e)
        {
            if (hfShtimModifikim.Value == "modifikim")
            {
                int id = int.Parse(Request.QueryString["id"]);
                DbCore.DbCRM.clsKokaAnketa koka = new DbCore.DbCRM.clsKokaAnketa(id);


                if (koka.kaVeprimeAnketa() & koka.DtFillimi <= DateTime.Today)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Kjo ankete ka veprime dhe  nuk mund te modifikohet!", pnlMesazhi);
                    mbushTrupin(koka.IdKokaAnketa, idNdermarrje);
                    return;
                }

            }


            DbCore.DbCRM.colTrupiAnketa trupavis = DbCore.mySessionObjects.merrAnketaVisibleNgaSesioni(Session);
            DbCore.DbCRM.colTrupiAnketa trupainvis = DbCore.mySessionObjects.merrAnketaInVisibleNgaSesioni(Session);
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
            if (hfShtimModifikim.Value == "modifikim")
            {
                int id = int.Parse(Request.QueryString["id"]);
                DbCore.DbCRM.clsKokaAnketa koka = new DbCore.DbCRM.clsKokaAnketa(id);
                if (koka.kaVeprimeAnketa() & koka.DtFillimi <= DateTime.Today)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Kjo ankete ka veprime dhe  nuk mund te modifikohet!", pnlMesazhi);
                    mbushTrupin(koka.IdKokaAnketa, idNdermarrje);
                    return;
                }

            }

            DbCore.DbCRM.colTrupiAnketa trupavis = DbCore.mySessionObjects.merrAnketaVisibleNgaSesioni(Session);
            DbCore.DbCRM.colTrupiAnketa trupainvis = DbCore.mySessionObjects.merrAnketaInVisibleNgaSesioni(Session);
            trupavis.AddRange(trupainvis);
            trupainvis = new DbCore.DbCRM.colTrupiAnketa();
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
            if (hfShtimModifikim.Value == "modifikim")
            {
                int id = int.Parse(Request.QueryString["id"]);
                DbCore.DbCRM.clsKokaAnketa koka = new DbCore.DbCRM.clsKokaAnketa(id);
                if (koka.kaVeprimeAnketa() & koka.DtFillimi <= DateTime.Today)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Kjo ankete ka veprime dhe  nuk mund te modifikohet!", pnlMesazhi);
                    mbushTrupin(koka.IdKokaAnketa, idNdermarrje);
                    return;
                }

            }
            {
                DbCore.DbCRM.colTrupiAnketa trupavis = DbCore.mySessionObjects.merrAnketaVisibleNgaSesioni(Session);
                DbCore.DbCRM.colTrupiAnketa trupainvis = DbCore.mySessionObjects.merrAnketaInVisibleNgaSesioni(Session);
                int count = trupavis.Count;
                int index = 0;
                for (int i = count - 1; i >= 0; i--)
                {
                    if (gvZgjedhur.Selection.IsRowSelected(i))
                    {
                        index = i;
                        if (!trupavis[i].Detyrueshme)
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
        }

        private void percaktoTemplate()
        {
            DbCore.DbCRM.colTrupiAnketa trupavis = DbCore.mySessionObjects.merrAnketaVisibleNgaSesioni(Session);
            GridViewDataTextColumn col0 = gvZgjedhur.Columns["Opsioni"] as GridViewDataTextColumn;
            col0.DataItemTemplate = new MyLabelTemplate();

            GridViewDataCheckColumn col3 = gvZgjedhur.Columns["Detyrueshme"] as GridViewDataCheckColumn;
            col3.DataItemTemplate = new MyCheckTemplate(false, false);

        }

        protected void btnMajtaGjitha_Click(object sender, EventArgs e)
        {
            if (hfShtimModifikim.Value == "modifikim")
            {
                int id = int.Parse(Request.QueryString["id"]);
                DbCore.DbCRM.clsKokaAnketa koka = new DbCore.DbCRM.clsKokaAnketa(id);
                if (koka.kaVeprimeAnketa() & koka.DtFillimi <= DateTime.Today)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Fushat e kesaj ankete nuk mund te ndryshohen!", pnlMesazhi);
                    mbushTrupin(koka.IdKokaAnketa, idNdermarrje);
                    return;
                }

            }


            DbCore.DbCRM.colTrupiAnketa trupavis = DbCore.mySessionObjects.merrAnketaVisibleNgaSesioni(Session);
            DbCore.DbCRM.colTrupiAnketa trupainvis = DbCore.mySessionObjects.merrAnketaInVisibleNgaSesioni(Session);
            DbCore.DbCRM.colTrupiAnketa trupiri = new DbCore.DbCRM.colTrupiAnketa();
            foreach (DbCore.DbCRM.clsTrupiAnketa trup in trupavis)
            {
                if (!trup.Detyrueshme)
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


        protected void lbxFushat_DataBound(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// ruajme vetem fushat visible
        /// </summary>
        /// <returns></returns>
        private DbCore.DbCRM.colTrupiAnketa krijoTrup()
        {
            DbCore.DbCRM.colTrupiAnketa trupi = new DbCore.DbCRM.colTrupiAnketa();
            DbCore.DbCRM.colTrupiAnketa trupavis = DbCore.mySessionObjects.merrAnketaVisibleNgaSesioni(Session);
            //    DbCore.DbCRM.colTrupiAnketa trupainvis = DbCore.mySessionObjects.merrAnketaInVisibleNgaSesioni(Session);
            int i = 0;
            foreach (DbCore.DbCRM.clsTrupiAnketa tru in trupavis)
            {
                trupi.Add(tru);
                i++;
            }
            //foreach (DbCore.DbCRM.clsTrupiAnketa tru in trupainvis)
            //{
            //    trupi.Add(tru);
            //    i++;
            //}

            return trupi;
        }


        //private bool AnketaLidhur(int idKokaAnketa)
        //{
        //    DbCore.DbCRM.clsDatabaseCRM dbCRM = new DbCore.DbCRM.clsDatabaseCRM();
        //    bool lidhur = dbCRM.kaVeprimeCRMListaAnketa(idKokaAnketa);
        //    return lidhur;
        //}

       
    }
}