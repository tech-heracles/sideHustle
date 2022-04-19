using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Collections;
using System.Drawing;
using System.Resources;
using System.Globalization;
using System.Data;
using DbCore.DbShare;
using DbCore.IMBUtils.Validation;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{
    public partial class Seriale : MyPageBase
    {

        ArrayList vlerat = new ArrayList();
        private const string emerKomponente = "Seriale.aspx";
        private const string emerGride = "gvSeriale";

        protected void Page_Load(object sender, EventArgs e)
        {
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            int idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            int idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            percaktoTemplateMenu(ASPxMenu1, idViti, idPerdoruesi, idNdermarrje);
            System.Globalization.CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            int idkonfigurim = 0;
            if (!String.IsNullOrEmpty(Request.QueryString["idKonfigAmbjente"]) && Request.QueryString["idKonfigAmbjente"] != "0")
                idkonfigurim = Convert.ToInt32(Request.QueryString["idKonfigAmbjente"]);
            else idkonfigurim = DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod("SER", idNdermarrje);
            if (!Page.IsPostBack)
            {
                hfState.Set("idGjuha", idGjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                EmrateTabeve(rm, cultinf);
                vendosHfMePerkthime(rm, cultinf);
                ASPxPageControl1.ActiveTabIndex = 0;
                mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 116, "SER", idGjuha, rm, cultinf);
                ListEditItem item = cmbKonfigurimi.Items.FindByValue(idkonfigurim.ToString());
                if (item != null)
                    cmbKonfigurimi.SelectedItem = cmbKonfigurimi.Items.FindByValue(idkonfigurim.ToString());
                else
                    cmbKonfigurimi.SelectedIndex = 0;
                DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.Value.ToString()), idGjuha);
                hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;
                mbushPopUpSeriale(idNdermarrje);
                konfiguroPopupGride(idPerdoruesi, idNdermarrje);
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, emerKomponente);
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                if (!tedrejtaInfo.DShtim)
                    gvSeriale.CancelEdit();
                ucFushatShtese.KonfiguroVleraFillestare(idNdermarrje, idPerdoruesi, idGjuha, emerKomponente, "Seriale", 0, konf.IdKonfigAmbjente);
                if (!String.IsNullOrEmpty(Request.QueryString["vjenNga"]) && !String.IsNullOrEmpty(Request.QueryString["idSeriali"]) && Request.QueryString["vjenNga"] == "GIS")
                {
                    int idNjesiPerSelektim = 0;
                    if (Int32.TryParse(Request.QueryString["idSeriali"], out idNjesiPerSelektim))
                    {
                        int rowIndex = gvSeriale.FindVisibleIndexByKeyValue(idNjesiPerSelektim);
                        if (rowIndex == ASPxGridView.InvalidRowIndex)
                            hfState.Set("idNjesiPerSelektim", "0");
                        hfState.Set("idNjesiPerSelektim", rowIndex);
                    }
                }
            }
            else
            {
                mbushPopUpSerialeNgaSession(idNdermarrje);
            }

            ucFushatShtese.percaktoTemplateFushash();
            clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idNdermarrje, emerGride, int.Parse(cmbKonfigurimi.Value.ToString()), emerKomponente);
            GridUtil.ToolTipButonaveMbiGride(gvSeriale, cultinf, rm);
        }

        /// <summary>
        /// Vendos emrat e tabeve ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateTabeve(System.Resources.ResourceManager rm, System.Globalization.CultureInfo cultinf)
        {
            ASPxPageControl1.TabPages[0].Text = rm.GetString("labelBlerjeShitjeTePergjithshme", cultinf);
            ASPxPageControl1.TabPages[1].Text = rm.GetString("labelAdministrimiInformacion", cultinf);
            ASPxPageControl1.TabPages[2].Text = rm.GetString("fushatShteseTab", cultinf);

        }
        private void vendosHfMePerkthime(ResourceManager rm, CultureInfo cultinf)
        {
     
            hfState.Set("msgZgjdhniNjeNgaElementetEListes", rm.GetString("msgZgjdhniNjeNgaElementetEListes", cultinf));
            hfState.Set("regjisDokZgjidhDokPerTeBashkengjitur", rm.GetString("regjisDokZgjidhDokPerTeBashkengjitur", cultinf));
        }

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        /// <param name="idViti"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNermarrje"></param>
        private void percaktoTemplateMenu(ASPxMenu aSPxMenu1, int idViti, int idPerdorues, int idNdermarrje)
        {
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, emerKomponente, this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, true, true, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            aSPxMenu1.Items.FindByName("Anullo").Text = rm.GetString("MenuItemMbyll", ci);
        }

        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        }

        private void mbushPopUpSeriale(int idNdermarrje)
        {//mbush griden e popupit me te dhena
            int idArtikulli = Convert.ToInt32(Request.QueryString["idartikulli"]);
            DataTable dt;
            if (idArtikulli == 0)
                dt =  DbCore.DbAsete.colAQTSeriale.ktheAQTSerialSipasIDNdermarjeDt(idNdermarrje);
            else
                dt = DbCore.DbAsete.colAQTSeriale.merrAQTSerialSipasIDArtikulliDt(idArtikulli, idNdermarrje);            
            DbCore.mySessionObjects.ruajGrideNeSession(emerKomponente, Session, dt);
            gvSeriale.DataSource = dt;
            gvSeriale.DataBind();
            dt.Dispose();
        }

        private void mbushPopUpSerialeNgaSession(int idNdermarrje)
        {
            DataTable tmpObject;
            bool sukses = DbCore.mySessionObjects.merrGrideNgaSessioni(emerKomponente,Session, out tmpObject);
            if (!sukses)
                mbushPopUpSeriale(idNdermarrje);
            else
            {
                gvSeriale.DataSource = tmpObject;
               // DbCore.mySessionObjects.RestoreFilter(idNdermarrje, Session, gvSeriale, Request.QueryString["kf"]);
                gvSeriale.DataBind();
               // DbCore.mySessionObjects.SaveFilter(idNdermarrje, Session, gvSeriale, Request.QueryString["kf"]);
                tmpObject.Dispose();
            }
        }

        private void konfiguroPopupGride(int idPerdorues, int idNdermarrje)
        {//konfiguron popupgriden
            GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, emerGride, gvSeriale, cmbKonfigurimi.Text, "445", DbCore.mySessionObjects.ktheGjuhe(Session));
            //DbCore.clsFunksione.percaktoVisibleColumns(DbCore.mySessionObjects.ktheGjuhePerdoruesi(Session), idNdermarrje, gvSeriale, emerGride, emerKomponente);
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            //DbCore.clsFunksione.konfiguroGrideListeEvogelPopupiPaTheme(gvSeriale, "IdAQTSerial", rm, ci);
            GridUtil.konfigGrideListeEMadhePaTheme(gvSeriale, "IdAQTSerial");
            gvSeriale.Settings.ShowFilterRow = true;
            gvSeriale.Settings.ShowFilterRowMenu = true;
            gvSeriale.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
            gvSeriale.Columns["#"].VisibleIndex = 0;
            gvSeriale.SettingsPager.PageSize = 10;
            //mbushPopUpSeriale(idNdermarrje);
        }

        protected void gvSeriale_DataBound(object sender, EventArgs e)
        {
            if (this.gvSeriale.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per 
                //perzgjidh
                DevExpress.Web.GridViewCommandColumn check = new DevExpress.Web.GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true; check.Width = Unit.Percentage(2);
                gvSeriale.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                gvSeriale.Settings.ShowFilterRowMenu = true;
                check.SetColVisibleIndex(0);
                //behet per te afishuar rreshtin qe do sherbej per filtrim
                gvSeriale.Settings.ShowFilterRow = true;
                gvSeriale.Columns.Add(check);
                gvSeriale.KeyFieldName = "IdAQTSerial";
                gvSeriale.SettingsBehavior.AllowSelectByRowClick = true;
                gvSeriale.SettingsBehavior.AllowFocusedRow = true;
            }
            else
            {
                gvSeriale.KeyFieldName = "IdAQTSerial";
                gvSeriale.SettingsBehavior.AllowSelectByRowClick = true;
                gvSeriale.SettingsBehavior.AllowFocusedRow = true;
            }
        }

        protected void gvSeriale_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        { //kur popupgrida ben callback
            mbushPopUpSeriale(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            System.Globalization.CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            GridUtil.ToolTipButonaveMbiGride(gvSeriale, cultinf, rm);
            //gvSeriale.Selection.UnselectAll();
        }

        public static void mbushComboKonfigurimeshSipasKategorise(int idPerdoruesi, int idNdermarrje, ASPxComboBox combo, int kat, string nivel, int idGjuha, ResourceManager rm, CultureInfo cultinf)
        {//mbush griden e popupit me te dhena
            DbCore.DbShare.colKonfigurimAmbjenti col = new DbCore.DbShare.colKonfigurimAmbjenti();
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            int idNiveli = DbCore.DbRegjistrim.clsNivelRegjistrimi.ktheIdNivelRegjistrimiSipasKodi(nivel, idNdermarrje);
            col.mbushKonfigAmbjSipasIdKategoriIdNivel(kat, idNiveli, idPerdoruesi, idGjuha, true);
            ListBoxColumn colprove = new ListBoxColumn();
            colprove.FieldName = "KodKonfigAmbjente";
            colprove.Caption = rm.GetString("cmbCmimeArtikulliCaptionKodi", cultinf);
            ListBoxColumn colemer = new ListBoxColumn();
            colemer.FieldName = "PershkrimKonfigAmbjente";
            colemer.Caption = rm.GetString("cmbCmimeArtikulliCaptionPershkrimi", cultinf);
            combo.TextFormatString = "{0}";
            combo.Columns.Add(colprove);
            combo.Columns.Add(colemer);
            combo.DataSource = col;
            combo.ValueField = "IdKonfigAmbjente";
            combo.IncrementalFilteringMode = DevExpress.Web.IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        protected void gvSeriale_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
        }

        protected void gvSeriale_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {//merr te dhenat e rreshtit te ri te grides
            for (int i = 0; i < e.NewValues.Count; i++)
            {
                if (e.NewValues[i] == null)
                {
                    e.Cancel = true;
                    return;
                }
            }
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), emerKomponente);
            if (!tedrejtaInfo.DShtim)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ju nuk keni te drejta per kete veprim!:Red");
                return;
            }
            int idArtikulli = int.Parse(Request.QueryString["idartikulli"]);
            bool MeSerial = DbCore.DbInventari.clsArtikulli.EshteMeSerial(idArtikulli);
            DbCore.DbAsete.clsAQTSeriale aqt = new DbCore.DbAsete.clsAQTSeriale();
            aqt.AqtSerialKod = DbCore.clsFunksione.ktheStringunPaHapesira(e.NewValues["AqtSerialKod"].ToString(), true);
            aqt.AqtSerialPershkrim = DbCore.clsFunksione.ktheStringunPaHapesira(e.NewValues["AqtSerialPershkrim"].ToString(), false);
            aqt.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            aqt.IdNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            aqt.IdStatusDokumenti = 1;
            aqt.IdKrijuesi = aqt.IdPerdoruesi;
            aqt.IdAQTArt = idArtikulli;
            aqt.MeSerialPerCope = !MeSerial;
            aqt.IdNjesiAdministrativeAktuale = 0;
            aqt.IdHistorikAktualPaSerial = 0;

            aqt.HistorikSeriali = new DbCore.DbAsete.clsHistorikAQTSeriale();
            if (aqt.MeSerialPerCope)
                aqt.HistorikSeriali = new DbCore.DbAsete.clsHistorikAQTSeriale(aqt.IdAQTSerial,String.Empty, 0, 0, 0, 1, 0, 0, 1, aqt.IdNdermarrje, aqt.IdPerdoruesi, aqt.IdPerdoruesi, aqt.DtKrijimi,  aqt.DtModifikimi);

            e.Cancel = true;

            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            mesazh = aqt.ruaj();
            if (!mesazh.Status == true)
            {
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ndodhi nje gabim. Ruajtja nuk u krye!:Red");
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ndodhi nje gabim. Ruajtja nuk u krye!", pnlMesazhi);
            }
            else
            {
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, MessagesResource.Messages["msgRuajtjeMeSuksesGreen"]);
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["mesazhRuajtjeMeSukses"], pnlMesazhi);
            }
            gvSeriale.CancelEdit();
            mbushPopUpSeriale(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        }

        protected void gvSeriale_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {//validimi ne jane plotesuar gjithe fushat e detyruara
            foreach (GridViewColumn column in gvSeriale.Columns)
            {
                if (column.Visible == true)
                {
                    GridViewDataColumn dataColumn = column as GridViewDataColumn;
                    if (dataColumn == null) continue;

                    if (e.NewValues[dataColumn.FieldName] == null)//validimi per kolonat e detyrueshme
                    {
                        e.Errors[dataColumn] = "Vlera nuk mund te jete null.";
                    }
                }
            }
            if (e.Keys["IdAQTSerial"] == null)
                if (DbCore.DbAsete.clsAQTSeriale.kontrolloEkzistonAQTSerial(DbCore.clsFunksione.ktheStringunPaHapesira(e.NewValues["AqtSerialKod"].ToString(), true), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session)))
                {
                    e.RowError = "Ekziston nje serial me kete kod! Ju lutem zgjidhni nje kod tjeter";
                }
            if (e.Errors.Count > 0)
            {
                e.RowError = "Ju lutemi, plotesoni te gjitha fushat.";
            }

            if (string.IsNullOrEmpty(e.RowError) && e.Errors.Count > 0)
            {
                e.RowError = "Ju lutemi, korrigjoni te gjithe gabimet.";
            }
            mbushPopUpSeriale(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        }

        protected void gvSeriale_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {//kur fillon editimi te behet validimi
            if (!gvSeriale.IsNewRowEditing)
            {
                gvSeriale.DoRowValidation();
            }
        }

        protected void gvSeriale_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {//ben modifikimin e nje rreshti

            for (int i = 0; i < e.NewValues.Count; i++)
            {
                if (e.NewValues[i] == null)
                {
                    e.Cancel = true;
                    return;
                }
            }
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), emerKomponente);
            if (!tedrejtaInfo.DMod)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ju nuk keni te drejta per kete veprim!:Red");
                return;
            }
            int idArtikulli = int.Parse(Request.QueryString["idartikulli"]);
            bool MeSerial = DbCore.DbInventari.clsArtikulli.EshteMeSerial(idArtikulli);
            DbCore.DbAsete.clsAQTSeriale aqt = new DbCore.DbAsete.clsAQTSeriale();
            aqt.IdAQTSerial = int.Parse(e.Keys["IdAQTSerial"].ToString());
            aqt.AqtSerialKod = DbCore.clsFunksione.ktheStringunPaHapesira(e.NewValues["AqtSerialKod"].ToString(), true);
            aqt.AqtSerialPershkrim = DbCore.clsFunksione.ktheStringunPaHapesira(e.NewValues["AqtSerialPershkrim"].ToString(), false);
            aqt.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            aqt.IdNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            aqt.IdStatusDokumenti = 1;
            aqt.IdKrijuesi = aqt.IdPerdoruesi;
            aqt.IdAQTArt = idArtikulli;
            aqt.MeSerialPerCope = !MeSerial;
            aqt.IdNjesiAdministrativeAktuale = 0;
            aqt.IdHistorikAktualPaSerial = 0;
            aqt.HistorikSeriali = new DbCore.DbAsete.clsHistorikAQTSeriale();

            if (aqt.AqtSerialKod != "" && aqt.AqtSerialPershkrim != "")
            {
                e.Cancel = true;
                aqt.modifiko();
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, MessagesResource.Messages["msgRuajtjeMeSuksesGreen"]);
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["mesazhRuajtjeMeSukses"], pnlMesazhi);
                gvSeriale.CancelEdit();
            }
            mbushPopUpSeriale(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        }

        protected void ButtonOk_Click2(object sender, EventArgs e)
        {//fshirja e nje grup kontabilizimi
            List<object> rreshtat = gvSeriale.GetSelectedFieldValues("IdAQTSerial");
            foreach (object id in rreshtat)
            {
                DbCore.DbAsete.clsAQTSeriale serial = new DbCore.DbAsete.clsAQTSeriale();
                serial.merrAQTSerialSipasID(int.Parse(id.ToString()));

                if (DbCore.DbAsete.clsAQTSeriale.kaveprimeAQTSerial(serial.IdAQTSerial, serial.IdNdermarrje))
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ky serial eshte perdorur ne veprime!", pnlMesazhi);
                }
                else
                {
                    serial.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                    DbCore.clsMesazh mesazh = serial.fshi();
                    if (mesazh.Status)
                        hiqSerialNgaGrida(serial.IdAQTSerial, serial.IdNdermarrje);
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["msgFshirjeMeSukses"], pnlMesazhi);
                    mbushPopUpSeriale(serial.IdNdermarrje);
                    hfStatusi.Value = "true";
                }
            }
        }

        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {
            if (e.Item.Name == "Ruaj")
            {
                Page.Validate("entries");
                ruajSeriale();
            }
            ////veprimet e menuse
            //switch (e.Item.Name)
            //{
            //    case "Modifiko":
            //        int indeksi = gvSeriale.FocusedRowIndex;
            //        gvSeriale.StartEdit(indeksi);
            //        break;
            //    case "Pastro":
            //    case "Shto":
            //        gvSeriale.AddNewRow();
            //        break;
            //    default:
            //        break;
            //}
        }


        //sherben per te ruajtur serialin
        private void ruajSeriale()
        {
            DbCore.DbAsete.clsAQTSeriale aqt = new DbCore.DbAsete.clsAQTSeriale();
            if (Page.IsValid == false)
                return;
            else
            {
                CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
                ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                bool eshteShtim;
                try
                {
                    if (hfShtimModifikim.Value == "shtim")
                        aqt = krijoSerial(idPerdoruesi, idNdermarrje, true);
                    else
                        aqt = krijoSerial(idPerdoruesi, idNdermarrje, false);
                }
                catch (Exception e)
                {
                    NLog.LogManager.GetCurrentClassLogger().Error(e.Message);
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, e.Message, pnlMesazhi);
                    hfStatusi.Value = "false";                   
                    return;
                }
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), emerKomponente);

                if (hfShtimModifikim.Value == "shtim")
                {
                    if (!tedrejtaInfo.DShtim)
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNukKeniTeDrejta", cultinf), pnlMesazhi);
                        hfStatusi.Value = "false";
                        return;
                    }
                    mesazh = aqt.ruaj();
                    eshteShtim = true;
                }
                else
                {
                    if (!tedrejtaInfo.DMod)
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNukKeniTeDrejta", cultinf), pnlMesazhi);
                        hfStatusi.Value = "false";
                        return;
                    }
                    mesazh = aqt.modifiko();
                    eshteShtim = false;
                }

                if (!mesazh.Status)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                    hfStatusi.Value = "false";
                }
                else
                {
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                    hfStatusi.Value = "true";
                    if (eshteShtim)
                        shtoSerialNeGrid(idNdermarrje, idPerdoruesi, aqt.IdAQTSerial, rm, cultinf);
                    else //modifikim
                        modifikoSerialNeGrid(idNdermarrje, idPerdoruesi, aqt.IdAQTSerial, rm, cultinf);
                }
            }
            pnlMesazhi.Update();
        }

        private void shtoSerialNeGrid(int idNdermarrje, int idPerdoruesi, int idSerial, ResourceManager rm, CultureInfo cultinf)
        {
            if (gvSeriale.DataSource != null)
            {
                DataTable dt = (DataTable)gvSeriale.DataSource;
                DataRow[] drs = dt.Select("IdAQTSerial = " + idSerial);
                if (drs.Length > 0)
                    throw new Exception("Ekziston ky serial ne gride");
                DataRow newArtDr = DbCore.DbAsete.clsAQTSeriale.ktheAqtSerialSipasId(idSerial);
                dt.ImportRow(newArtDr);
            }
            else
                mbushPopUpSeriale(idNdermarrje);
            konfiguroPopupGride(idPerdoruesi, idNdermarrje);
        }

        private void modifikoSerialNeGrid(int idNdermarrje, int idPerdoruesi, int idSerial, ResourceManager rm, CultureInfo cultinf)
        {
            if (gvSeriale.DataSource != null)
            {
                DataTable dt = (DataTable)gvSeriale.DataSource;
                DataRow[] drs = dt.Select("IdAQTSerial = " + idSerial);
                if (drs.Length > 1)
                    throw new Exception("Ekziston ky serial ne gride");
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                DataRow newArtDr = DbCore.DbAsete.clsAQTSeriale.ktheAqtSerialSipasId(idSerial);
                object[] arr = newArtDr.ItemArray;
                dr.ItemArray = arr;
                gvSeriale.DataBind();
            }
            else
                mbushPopUpSeriale(idNdermarrje);
        }

        private void hiqSerialNgaGrida(int idSerial, int idNdermarrje)
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            if (this.gvSeriale.DataSource != null)
            {
                DataTable dt = (DataTable)gvSeriale.DataSource;
                DataRow[] drs = dt.Select("IdAQTSerial = " + idSerial);
                if (drs.Length > 1)
                    throw new Exception("Ekziston ky serial ne gride");
                if (drs.Length == 0) 
                    return;
                DataRow dr = drs[0];
                dt.Rows.Remove(dr);
                gvSeriale.DataBind();
            }
            else
                mbushPopUpSeriale(idNdermarrje);
        }

        private DbCore.DbAsete.clsAQTSeriale krijoSerial(int idPerdoruesi, int idNdermarrje, bool shtim)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);

            DbCore.DbAsete.clsAQTSeriale aqt = new DbCore.DbAsete.clsAQTSeriale();
            int idArtikulli = int.Parse(Request.QueryString["idartikulli"]);
            bool MeSerial = DbCore.DbInventari.clsArtikulli.EshteMeSerial(idArtikulli);
            if (!shtim)
                aqt.IdAQTSerial = int.Parse(hfId.Value.ToString());
            if (txtKodi.Text == "")
                throw new DbCore.MyException("Plotesoni kodin!");
            DbCore.clsMesazh kontrollKodi = DbCore.clsFunksione.kontrolloKaraktereMeMesazh(DbCore.clsFunksione.ktheStringunPaHapesira(txtKodi.Text, true), FusheKontrolli.Kodi, false);
            if (!kontrollKodi.Status)
                throw new DbCore.MyException(kontrollKodi.PershkrimMesazhi);
            aqt.AqtSerialKod = DbCore.clsFunksione.ktheStringunPaHapesira(txtKodi.Text, true);
            
            if (txtPershkrimi.Text == "")
                throw new DbCore.MyException("Plotesoni pershkrimin!");
            DbCore.clsMesazh kontrollPershkrimi = DbCore.clsFunksione.kontrolloKaraktereMeMesazh(DbCore.clsFunksione.ktheStringunPaHapesira(txtPershkrimi.Text, false), FusheKontrolli.Pershkrimi, true);
            if (!kontrollPershkrimi.Status)
                throw new DbCore.MyException(kontrollPershkrimi.PershkrimMesazhi);
            aqt.AqtSerialPershkrim = DbCore.clsFunksione.ktheStringunPaHapesira(txtPershkrimi.Text, false);

            aqt.IdPerdoruesi = idPerdoruesi;
            aqt.IdNdermarrje = idNdermarrje;
            aqt.IdStatusDokumenti = 1;
            aqt.IdKrijuesi = aqt.IdPerdoruesi;
            aqt.IdAQTArt = idArtikulli;
            aqt.MeSerialPerCope = !MeSerial;
            aqt.IdNjesiAdministrativeAktuale = 0;
            aqt.IdHistorikAktualPaSerial = 0;            
            aqt.OColVleratFushatShtese = ucFushatShtese.merrFushatShtese();
            
            aqt.HistorikSeriali = new DbCore.DbAsete.clsHistorikAQTSeriale();

            aqt.HfArkiva = hfArkiva;
            //colArkiva oArkiva = new colArkiva();
            //if (hfArkiva != null)
            //    oArkiva = clsArkiva.krijoArkiva(aqt.IdAQTSerial, 1, 116,DateTime.Now, idPerdoruesi, hfArkiva);
            //aqt.OArkiva = oArkiva;
            if (aqt.MeSerialPerCope)
                aqt.HistorikSeriali = new DbCore.DbAsete.clsHistorikAQTSeriale(aqt.IdAQTSerial, String.Empty, 0, 0, 0, 1, 0, 0, 1, aqt.IdNdermarrje, aqt.IdPerdoruesi, aqt.IdPerdoruesi, aqt.DtKrijimi, aqt.DtModifikimi);

            if (shtim && DbCore.DbAsete.clsAQTSeriale.kontrolloEkzistonAQTSerial(aqt.AqtSerialKod, idNdermarrje))
                throw new DbCore.MyException("Ekziston nje serial me kete kod! Ju lutem zgjidhni nje kod tjeter");

            return aqt;
        }

        protected void gvSeriale_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
        {

        }

        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            clsMenuInfo.ShtoMenuItemInfo(this, MenuInfo);
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            DbCore.DbAdmin.clsFiltraGrida filtri = new DbCore.DbAdmin.clsFiltraGrida();
            filtri.FiltraKodi = cmbFiltra.Text;
            filtri.FiltraShenime = cmbFiltra.Text;
            filtri.FiltraUniversal = false;// Universal_ASPxCheckBox.Checked;
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), emerGride, emerKomponente, idNdermarrje);
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gvSeriale.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("IdAQTSerial", gvSeriale);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvSeriale.GetSortedColumns();
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
            //    filtri.KoloneRenditje = "IdAQTSerial";
            //    filtri.DrejtimRenditje = true;
            //}
            DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();

            oPerdorues = DbCore.mySessionObjects.kthePerdorues(Session);
            filtri.IdPerdoruesi = oPerdorues.IdPerdorues;
            filtri.IdNdermarje = idNdermarrje;
            filtri.IdStatusDok = 1;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            mesazh = filtri.ruaj();
            //mbushComboBoxFiltra(idNdermarrje);
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, emerGride, 1, emerKomponente);
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);
            if (mesazh.Status == true)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            cmbFiltra.Text = "";
        }

        public void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje
            clsMenuInfo.ShtoMenuItemInfo(this, MenuInfo);
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), emerGride, emerKomponente, idNdermarrje);
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();
                //mbushComboBoxFiltra(idNdermarrje);
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, emerGride, 1, emerKomponente);
                percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);
                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                gvSeriale.FilterExpression = String.Empty;
            }
        }
    }
}