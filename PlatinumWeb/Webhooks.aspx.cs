using DbCore;
using DevExpress.Web;
using System;
using System.Web.UI;
using System.Globalization;
using System.Resources;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using DbCore.IMBUtils.Logging;
using DbCore.IMBUtils.Types;
using DbCore.IMBUtils.Messages;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.DbAdmin;

namespace PlatinumWeb
{
    public partial class Webhooks : MyPageBase
    {
        private const string komponenteEmri = "Webhooks.aspx";
        private const int idKomponente = 20044;
        private int idKonfig;
        private string guidString;

        protected void Page_Load(object sender, EventArgs e)
       {
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                DbCore.clsFunksione.logout(Session, true, "FaqePaautorizuar");
            }
            int idPerd = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            //if (CacheLayer.GlobalCacheManager.MySessionCache["KodiNdermarrjes"] == null)
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerd);
            }
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            var rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            int idgjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            var cultinf = DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo(idgjuha);

            if (!IsPostBack)
            {
                EmrateTabeve();
                //mbushHiddenFieldMePerkthime(cultinf, rm);
                hfState.Set("idGjuha", IdGjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                guidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                hfState.Set("guidString", guidString);
                konfiguroVleraFillestare(idPerd, idNdermarrje, rm, cultinf, idgjuha);
                mbushListeWebhooks(idNdermarrje);
                var tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerd, idNdermarrje, idViti, "Konfigurime Gride");
                //hfTeDrejtaKonfGride.Value = tedrejtaInfo.DPlot.ToString();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerd, idNdermarrje, idViti, komponenteEmri);
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                konfiguroGride(idNdermarrje, cmbKonfigurimi.Text.Split(';')[0]);
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "gvWebhooks", gvWebhooks, cmbKonfigurimi.Text.Split(';')[0], "20044", idgjuha);
            }
            else
            {
                guidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                mbushListeWebhookNgaSessioni();
                konfiguroGride(idNdermarrje, cmbKonfigurimi.Text.Split(';')[0]);
            }
          
            GridUtil.konfigGrideListeEMadhePaTheme(gvWebhooks, "IdWebhook");
            percaktoTemplateMenu(ASPxMenu1, idViti, idPerd, idNdermarrje);
            idKonfig = int.Parse(cmbKonfigurimi.Value.ToString());
            gvWebhooks.PercaktoTitlePanel(this, MenuInfo, pnlMesazhi, hfState, idPerd, idNdermarrje, idViti, idgjuha, idKonfig, komponenteEmri, rm, cultinf);
            //clsToolbarConfig.mbushComboBoxFiltra(idgjuha, idNdermarrje, "gvWebhooks", idKonfig, komponenteEmri);
            AspxWebControlUtils.perkthePopUp(popFshi, MessagesResource.Messages["labelKujdes"], lblMsgbox, MessagesResource.Messages["labelAdministrimiMsgJeniSigurt"], ButtonCancel, MessagesResource.Messages["labelAnullo"]);

        }
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        }
        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {
            if (e.Item.Name == "Ruaj")
            {
                Page.Validate("entries");
              ruajWebhook();
            }
        }
        private void ruajWebhook()
        {
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idPerdorues = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);

            if (!Page.IsValid)
            {
                hfStatusi.Value = "false";
                return;
            }
            try
            {
                DbCore.clsMesazh mesazh = kontrolloWebhook();
                if (!mesazh.Status)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                    return;
                }

                int idStatusDok = 1;

                clsWebhooks webhooks = new clsWebhooks(0, txtKodi.Text, txtUrlPritese.Text,  idNdermarrje, idPerdorues, cbAktive.Checked, idStatusDok, cmbKategoria.SelectedIndex, cmbEventi.SelectedIndex);

               webhooks.IdWebhook = int.Parse(hfId.Value.ToString());
                if (hfShtimModifikim.Value == "shtim")
                    mesazh = webhooks.Ruaj();
                else // modifikim                     
                    mesazh = webhooks.Modifiko();
                if (mesazh.Status)
                {
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["msgRuajtjaEWebhook"], pnlMesazhi);
                    hfStatusi.Value = "true";

                    if (hfShtimModifikim.Value == "shtim")
                        shtoWebhookNeGrid(idNdermarrje, webhooks.IdWebhook);
                    else //modifikim
                        modifikoWebhookNeGrid(idNdermarrje, webhooks.IdWebhook);
                }
                else
                {
                    hfStatusi.Value = "false";
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgGabimNeRuajtjeWebhook"], pnlMesazhi);
                }
                ASPxPageControl1.ActiveTabIndex = 0;
            }
            catch (Exception ex)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgGabimNeRuajtjeWebhook"], pnlMesazhi);
            }
        }
        private DbCore.clsMesazh kontrolloWebhook()
        {
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            if (txtKodi.Text == "")
                return new clsMesazh(false, MessagesResource.Messages["msgPlotesoniKodin"]);
            if (hfShtimModifikim.Value == "shtim" && DbCore.DbAdmin.clsWebhooks.ekzistonWebhookFtp(txtKodi.Text, idNdermarrje))
                return new clsMesazh(false, String.Format("Ekziston Webhook me kod {0}!", txtKodi.Text));
            if (txtUrlPritese.Text == "")
                return new clsMesazh(false, MessagesResource.Messages["msgPlotesoniUrl"]);
            if (cmbKategoria.SelectedIndex == -1)
                return new clsMesazh(false, MessagesResource.Messages["msgPlotesoniKategori"]);
            if (cmbEventi.SelectedIndex == -1)
                return new clsMesazh(false, MessagesResource.Messages["msgPlotesoniEvent"]);
            

            return new clsMesazh(true);
        }
        private void shtoWebhookNeGrid(int idNdermarrje, int idwebhooks)
        {
            if (gvWebhooks.DataSource != null)
            {
                DataTable dt = (DataTable)gvWebhooks.DataSource;
                DataRow[] drs = dt.Select("IdWebhook = " + idwebhooks);
                if (drs.Length > 0)
                    throw new Exception(MessagesResource.Messages["labelRaportMesazhWebhookEkziston"]);
                DataRow newArtDr = DbCore.DbAdmin.clsWebhooks.merrWebhookSipasIdDR(idwebhooks);
                dt.ImportRow(newArtDr);
            }
            else
                mbushListeWebhooks(idNdermarrje);

            konfiguroGride(idNdermarrje, cmbKonfigurimi.Text.Split(';')[0]);
        }
        private void modifikoWebhookNeGrid(int idNdermarrje, int idwebhooks)
        {
            if (gvWebhooks.DataSource != null)
            {
                DataTable dt = (DataTable)gvWebhooks.DataSource;
                DataRow[] drs = dt.Select("IdWebhook = " + idwebhooks);
                if (drs.Length > 1)
                    throw new DbCore.MyException(MessagesResource.Messages["labelRaportMesazhGabimiDyWebhookNeGride"]);
                if (drs.Length == 0)
                    return;
                DataRow dr = drs[0];
                DataRow newArtDr = DbCore.DbAdmin.clsWebhooks.merrWebhookSipasIdDR(idwebhooks);
                //dt.Rows.Remove(dr);
                //dt.ImportRow(newArtDr);
                object[] arr = newArtDr.ItemArray;
                dr.ItemArray = arr;
            }
            else
                mbushListeWebhooks(idNdermarrje);
        }
        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            List<object> rreshtat;
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            if (ASPxPageControl1.ActiveTabIndex == 0)
            {
                rreshtat = gvWebhooks.GetSelectedFieldValues("IdWebhook");
            }
            else
            {
                rreshtat = new List<object>();
                rreshtat.Add(hfId.Value);
            }

            if (rreshtat.Count == 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgZgjidhniNjeWebhook"], pnlMesazhi);
                return;
            }
            var mesazh = new DbCore.clsMesazh();

            var TeFshire = new List<string>();
            var TePaFshire = new List<string>();
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            foreach (object id in rreshtat)
            {
                DbCore.DbAdmin.clsWebhooks clsWebhooks = new DbCore.DbAdmin.clsWebhooks(Convert.ToInt32(id), idNdermarrje);

                konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.Value.ToString()));


                mesazh = clsWebhooks.Fshi();
                if (clsWebhooks.IdWebhook == 0)
                    continue;
                if (mesazh.Status)
                {
                    hiqWebhookNgaGrida(clsWebhooks.IdWebhook);
                    TeFshire.Add(clsWebhooks.KodiWebhook);

                    ASPxPageControl1.ActiveTabIndex = 0;
                    hfStatusi.Value = "true";
                }
            }
            var mesazhInfoGabim = string.Empty;
            var mesazhInfoSukses = string.Empty;

           
            if (TeFshire.Count == 1)
                mesazhInfoSukses = String.Format("{0}{1}{2}", MessagesResource.Messages["labelRaportWebhookMeKod"] + " ", String.Join(";", TeFshire), MessagesResource.Messages["msgCeljeMagazinatSuffixNjejesSuksesi"]);

            else if (TeFshire.Count > 1)
                mesazhInfoSukses = String.Format("{0}{1}{2}", MessagesResource.Messages["labelRaportWebhookMeKod"] + " ", String.Join(";", TeFshire), MessagesResource.Messages["msgCeljeMagazinatSuffixShumesSuksesi"]);

            if (mesazhInfoGabim != string.Empty && mesazhInfoSukses != string.Empty)
                mesazhInfoGabim += MessagesResource.Messages["msgLidhesMesazhi"] + mesazhInfoSukses;
            if (mesazhInfoGabim != string.Empty)
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhInfoGabim, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazhInfoSukses, pnlMesazhi);
        }
        private void hiqWebhookNgaGrida(int idWebhook)
        {
            if (this.gvWebhooks.DataSource != null)
            {
                DataTable dt = (DataTable)gvWebhooks.DataSource;
                DataRow[] drs = dt.Select("IdWebhook = " + idWebhook);
                if (drs.Length > 1)
                    throw new Exception(MessagesResource.Messages["labelRaportMesazhGabimiDyWebhookNeGride"]);
                if (drs.Length == 0)
                    return;
                DataRow dr = drs[0];
                dt.Rows.Remove(dr);
                gvWebhooks.DataBind();
            }
            else
                mbushListeWebhooks(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        }
        private void EmrateTabeve()
        {
            ASPxPageControl1.TabPages[0].Text = MessagesResource.Messages[""];
            ASPxPageControl1.TabPages[1].Text = MessagesResource.Messages["MenuItemWebhooks"];
        }
        private void konfiguroVleraFillestare(int idPerdoruesi, int idNdermarrje, ResourceManager rm, CultureInfo cultinf, int idGjuha)
        {
            ASPxPageControl1.ActiveTabIndex = 0;
            ConfigureAspxComboBox.mbushComboKategoriaWebhooks(cmbKategoria);
            ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 208, rm, cultinf, idGjuha);

            ConfigureAspxComboBox.mbushComboEventi(cmbEventi);
            cmbKonfigurimi.SelectedIndex = 0;
            var konf = new DbCore.DbShare.clsKonfigurimAmbjenti();

            konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()), idGjuha);
            hfKonfillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;
        }
        private void mbushListeWebhooks(int idNdermarrje)
        {
            var dt = DbCore.DbAdmin.clsWebhooks.merrWebhookSipasNderm(idNdermarrje);
            DbCore.mySessionObjects.ruajGrideNeSession(Session, dt);
            gvWebhooks.DataSource = dt;
            gvWebhooks.DataBind();
            dt.Dispose();
        }
        private void konfiguroGride(int idNdermarrje, string kodKonfigurimi)
        {
           ApplicationUtils.ASPxControlUtils.KonfigurimComboGride.shtoKategoriWebhook(gvWebhooks);
            ApplicationUtils.ASPxControlUtils.KonfigurimComboGride.shtoEventWebhook(gvWebhooks);
            GridUtil.konfigGrideListeEMadhePaTheme(gvWebhooks, "IdWebhook");
            gvWebhooks.Columns["#"].Width = 30;
            gvWebhooks.Columns["#"].VisibleIndex = 0;
        }
        private void mbushListeWebhookNgaSessioni()
        {
            DataTable tmpObject;
            bool sukses = DbCore.mySessionObjects.merrGrideNgaSessioni(Session, out tmpObject);
            if (!sukses)
                mbushListeWebhooks((int)hfState["idNdermarrje"]);
            else
            {
                gvWebhooks.DataSource = tmpObject;
                gvWebhooks.DataBind();
                tmpObject.Dispose();
            }
        }
        protected void gvWebhooks_DataBound(object sender, EventArgs e)
        {
            if (gvWebhooks.Columns["#"] == null)
            {
                var check = new DevExpress.Web.GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true;
                check.SetColVisibleIndex(0);
                check.Width = Unit.Percentage(2);

                gvWebhooks.Settings.ShowFilterRow = true;
                gvWebhooks.Columns.Add(check);
                gvWebhooks.KeyFieldName = "IdWebhook";
                gvWebhooks.SettingsBehavior.AllowSelectByRowClick = true;
                gvWebhooks.SettingsBehavior.AllowFocusedRow = true;
                gvWebhooks.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                gvWebhooks.Settings.ShowFilterRowMenu = true;
            }
        }

        protected void gvWebhooks_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            var cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            if (e.CallbackName == "COLUMNMOVE" && gvWebhooks.AllColumns[int.Parse(e.Args[0])].Width.Value == 0)
            {
                gvWebhooks.AllColumns[int.Parse(e.Args[0])].Width = Unit.Percentage(3);
            }
            //mbushListeKonfigurimeshFtp(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            GridUtil.ToolTipButonaveMbiGride(gvWebhooks, cultinf, rm);
        }

        protected void gvWebhooks_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if (e.Column.FieldName == "IdWebhook")
            {
                try
                {
                    var vlera = Converter.ConvertToInt(e.Value);
                    if (vlera == -3 || vlera == 0)
                    {
                        e.Criteria = null;
                    }
                }
                catch (Exception err)
                {
                    ImbLogger.Error(err.Message);
                    e.Criteria = null;
                }
            }
           
        }

        /// <summary>
        /// Metoda qe thirret kur grida ben callback
        /// </summary>
        /// <param name="sender">Derguesi</param>
        /// <param name="e">Argumentat</param>
        protected void gvWebhooks_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            var idkomponente = string.Empty;
            var kodkonfigurimi = string.Empty;

            var arr = e.Parameters.Split(';');
            if (arr.Length == 2)
            {
                kodkonfigurimi = arr[1];
                idkomponente = arr[0];
            }
            else
            {
                idkomponente = e.Parameters;
            }

            gvWebhooks.Selection.UnselectAll();
        }

        private void percaktoTemplateMenu(ASPxMenu aSPxMenu1, int idViti, int idPerdorues, int idNdermarrje)
        {
            clsToolbarConfig.percaktoTemplateMenu(IdGjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1, "Webhooks.aspx", this, MenuInfo, hfShtimModifikim.Value == "modifikim" ? false : true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
        }
    }

}
