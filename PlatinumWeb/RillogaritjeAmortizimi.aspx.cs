using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Data;
using PlatinumWeb.Templates;
using System.Web.Script.Serialization;
using DbCore.DbInventari;
using System.Globalization;
using System.Resources;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class RillogaritjeAmortizimi : MyPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                DbCore.clsFunksione.logout(Session,true,"FaqePaautorizuar");
            }
            int idPerd = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);

            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerd);
            }

            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (!IsPostBack)
            {
                mbushHiddenFieldMePerkthime(ci, rm);
                hfState.Set("idPerdoruesi", idPerd);
                hfState.Set("idNdermarrje", idNdermarrje);
                DbCore.mySessionObjects.ruajColArtikull2NeSesion(Session, DbCore.DbInventari.colArtikujt.ktheArtikujNdermarrjesAndAutorizimeDheLlojDT(-5, -5, true, false));
                DbCore.mySessionObjects.ruajColArtikullNeSesion(Session, DbCore.DbInventari.colArtikujt.ktheArtikujNdermarrjesAndAutorizimeDheLlojDT(-5, -5, true, false));
                konfiguroVleraFillestare(idNdermarrje, rm, ci);

            }
            DataTable dt1 = DbCore.mySessionObjects.merrColArtikullNgaSesioni(Session);
            DataTable dt2 = DbCore.mySessionObjects.merrColArtikull2NgaSesioni(Session);
            gvRivleresim2.DataSource = dt2;
            gvRivleresim2.DataBind();

            gvRivleresim.DataSource = dt1;
            gvRivleresim.DataBind();
            perktheKontrollet(rm, ci);
            percaktoTemplate();
        }

        private void perktheKontrollet(ResourceManager rm, CultureInfo ci)
        {
            btnZgjidhGjitha.ToolTip = rm.GetString("btnZgjidhGjitha", ci);
            gridaSelectTeGjitha.ToolTip = rm.GetString("btnZgjidhTeGjithe", ci);
            btnHiqZgjedhjen.ToolTip = rm.GetString("hiqZgjedhjenBtn", ci);
            btnZgjidhGjitha2.ToolTip = rm.GetString("btnZgjidhGjitha", ci);
            ASPxButton1.ToolTip = rm.GetString("btnZgjidhTeGjithe", ci);
            btnHiqZgjedhjen2.ToolTip = rm.GetString("hiqZgjedhjenBtn", ci);
            lblPeriudha.Text = rm.GetString("lblDateFillimi", ci);
            lblLloji.Text = rm.GetString("lblLloji2", ci);
            lblStandarti.Text = rm.GetString("filterRaportiStandarti", ci);
            lblArtikujtPerRillogaritje.Text = rm.GetString("lblArtikujtPerRillogaritje", ci);
            ASPxRoundPanel1.HeaderText = rm.GetString("MenuItemRillogaritjeAmortizimi", ci);
        }

        private void mbushHiddenFieldMePerkthime(CultureInfo ci, ResourceManager rm)
        {
            hfState.Set("msgZgjidhniSerialet", rm.GetString("msgZgjidhniSerialet", ci));
            hfState.Set("msgNdodhi1GabimGjateRillogaritjes", rm.GetString("msgNdodhi1GabimGjateRillogaritjes", ci));
            hfState.Set("msgRillogaritjaUNdaluaTek", rm.GetString("msgRillogaritjaUNdaluaTek", ci));
            hfState.Set("msgRillogaritjaPerfundoiMeSukses", rm.GetString("msgRillogaritjaPerfundoiMeSukses", ci));
            hfState.Set("msgKaArtikujPaSerialeNeGride", rm.GetString("msgKaArtikujPaSerialeNeGride", ci));
            hfState.Set("msgDuhetTeKaloniArtikujtQeDoniTeLlogarisniTekGridaPoshte", rm.GetString("msgDuhetTeKaloniArtikujtQeDoniTeLlogarisniTekGridaPoshte", ci));
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
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, "RillogaritjeAmortizimi.aspx", this, MenuInfo, true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
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
        /// <summary>
        /// Percakton veprimin qe kryhet kur klikohet nje nga butonat e menuse
        /// </summary>
        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {
            if (e.Item.Name == "Rivleresim")
            {
                Page.Validate();

            }
        }

        /// <summary>
        /// Vendos vlerat default kur po behet shtim
        /// </summary>
        /// <param name="idNdermarrje"></param>
        private void konfiguroVleraFillestare(int idNdermarrje, ResourceManager rm, CultureInfo ci)
        {

            AspxWebControlUtils.vendosDateEditMask(dtePeriudhaNga);
            vendosDataDefault();
            ConfigureAspxComboBox.mbushComboStandartAmortizimi(cmbStandarti, idNdermarrje);
            cmbStandarti.SelectedIndex = 0;
            mbushComboLloji(cmbLloji, rm, ci);

            mbushListeArtikujsh();
            konfiguroGride();
        }

        private void mbushComboLloji(DevExpress.Web.ASPxComboBox cmbLloji, ResourceManager rm, CultureInfo ci)
        {
            cmbLloji.Items.Add(rm.GetString("headerAnalitike", ci), false);
            cmbLloji.Items.Add(rm.GetString("cmbLlojiPermbledhese", ci), true);
            cmbLloji.SelectedIndex = 0;
        }
        private void vendosDataDefault()
        {
            DateTime sot = new DateTime();
            sot = DateTime.Today;
            DbCore.DbAdmin.clsPeriudhaKontabel periudha = DbCore.mySessionObjects.merrPeriudheKontabel(Session);
            if (sot >= periudha.FillimiPeriudha && sot <= periudha.MbarimiPeriudha)
                this.dtePeriudhaNga.Value = DateTime.Today;
            else
                this.dtePeriudhaNga.Value = periudha.FillimiPeriudha;
        }
        private void mbushListeArtikujsh()
        {//mbush griden e popupit me te dhena  

            DbCore.DbInventari.colArtikujt colArtikujt = new DbCore.DbInventari.colArtikujt();

            DataTable dt = DbCore.DbInventari.colArtikujt.ktheArtikujNdermarrjesAndAutorizimeDheLlojDT(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), true, false);

            gvRivleresim.DataSource = dt;//CacheLayer.GlobalCacheManager.MySessionCache["colArtikull"] = dt;
            DbCore.mySessionObjects.ruajColArtikullNeSesion(Session, dt);
            gvRivleresim.DataBind();
            dt.Dispose();
            DataTable dt2 = DbCore.DbInventari.colArtikujt.ktheArtikujNdermarrjesAndAutorizimeDheLlojDT(-5, -5, true, false);
            DbCore.mySessionObjects.ruajColArtikull2NeSesion(Session, dt2);
            gvRivleresim2.DataSource = dt2;
            gvRivleresim2.DataBind();
            dt2.Dispose();
        }

        private void percaktoTemplate()
        {
            GridViewDataTextColumn col1 = gvRivleresim2.Columns["Autorizimet"] as GridViewDataTextColumn;
            col1.DataItemTemplate = new MyComboTemplate();
            col1.Caption = "Seriale";
            if (!bool.Parse(cmbLloji.Value.ToString()))


                col1.Visible = true;
            else col1.Visible = false;
        }
        private void konfiguroGride()
        {
            GridUtil.percaktoVisibleColumnsSipasKonfigurimit(gvRivleresim, "gvRivleresim", "RillogaritjeAmortizimi.aspx", 1, true, DbCore.mySessionObjects.ktheGjuhe(Session));
            GridUtil.percaktoVisibleColumnsSipasKonfigurimit(gvRivleresim2, "gvRivleresim", "RillogaritjeAmortizimi.aspx", 1, true, DbCore.mySessionObjects.ktheGjuhe(Session));


            percaktoTemplate();
        }
        protected void gvRivleresim_DataBound(object sender, EventArgs e)
        {
            if (this.gvRivleresim.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per 
                //perzgjidh
                GridViewCommandColumn check = new GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true; check.Width = Unit.Percentage(2);
                check.SetColVisibleIndex(0);
                //behet per te afishuar rreshtin qe do sherbej per filtrim
                gvRivleresim.Settings.ShowFilterRow = true;
                gvRivleresim.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                gvRivleresim.Settings.ShowFilterRowMenu = true;
                gvRivleresim.Columns.Add(check);


                gvRivleresim.KeyFieldName = "IdArtikulli";
                gvRivleresim.SettingsBehavior.AllowSelectByRowClick = true;
                gvRivleresim.SettingsBehavior.AllowFocusedRow = true;
            }
            this.gvRivleresim.Columns["#"].VisibleIndex = 0;

        }
        protected void gvRivleresim2_DataBound(object sender, EventArgs e)
        {
            if (this.gvRivleresim2.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per 
                //perzgjidh
                GridViewCommandColumn check = new GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true; check.Width = Unit.Percentage(2);
                check.SetColVisibleIndex(0);
                //behet per te afishuar rreshtin qe do sherbej per filtrim
                gvRivleresim2.Settings.ShowFilterRow = true;
                gvRivleresim2.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                gvRivleresim2.Settings.ShowFilterRowMenu = true;
                gvRivleresim2.Columns.Add(check);


                gvRivleresim2.KeyFieldName = "IdArtikulli";
                gvRivleresim2.SettingsBehavior.AllowSelectByRowClick = true;
                gvRivleresim2.SettingsBehavior.AllowFocusedRow = true;
            }
            this.gvRivleresim2.Columns["#"].VisibleIndex = 0;

        }

        protected void gvRivleresim_AfterPerformCallback(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            mbushListaCallback();
        }
        protected void gvRivleresim2_AfterPerformCallback(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            mbushListaCallback();
        }

        protected void gvRivleresim_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters.ToString() != "")
            {
                mbushListeArtikujsh();
                percaktoTemplate();
            }
            else
            {
                mbushListaCallback();
            }
        }
        private void mbushListaCallback()
        {

            gvRivleresim2.DataSource = DbCore.mySessionObjects.merrColArtikull2NgaSesioni(Session);
            gvRivleresim2.DataBind();
            gvRivleresim.DataSource = DbCore.mySessionObjects.merrColArtikullNgaSesioni(Session);
            gvRivleresim.DataBind();
        }
        protected void gvRivleresim2_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            mbushListaCallback();
        }

        protected void gvRivleresim_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpNoRows"] = gvRivleresim.VisibleRowCount;
            e.Properties["cpNoPage"] = gvRivleresim.PageIndex;
        }
        protected void gvRivleresim2_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpNoRows"] = gvRivleresim2.VisibleRowCount;
            e.Properties["cpNoPage"] = gvRivleresim2.PageIndex;
        }

        protected void btnDjathtas1_Click(object sender, EventArgs e)
        {
            DataTable dt1 = DbCore.mySessionObjects.merrColArtikullNgaSesioni(Session);
            DataTable dt2 = DbCore.mySessionObjects.merrColArtikull2NgaSesioni(Session);
            gvRivleresim2.DataSource = dt2;
            gvRivleresim2.DataBind();
            DataColumn[] keys = new DataColumn[1];
            keys[0] = dt1.Columns[0];
            dt1.PrimaryKey = keys;
            gvRivleresim.DataSource = dt1;
            gvRivleresim.DataBind();
            List<object> id = gvRivleresim.GetSelectedFieldValues("IdArtikulli");
            foreach (object i in id)
            {
                DataRow dd = dt1.Rows.Find(i);
                dt2.Rows.Add(dd.ItemArray);
                //DataRow dr = DbCore.DbInventari.colArtikujt.merrSipasArtikujNdermarrjesAndAutorizimeDR(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), Convert.ToInt32(i));
                //dt2.ImportRow(dr);
                //DataRow[] drs = dt1.Select("IdArtikulli = " + i);
                //if (drs.Length > 1)
                //    throw new Exception("GABIM: Ndodhen 2 artikuj me te njejten id ne gride");
                //if (drs.Length == 0) return;
                //DataRow dr1 = drs[0];
                dt1.Rows.Remove(dd);

            }

            DbCore.mySessionObjects.ruajColArtikullNeSesion(Session, dt1);
            DbCore.mySessionObjects.ruajColArtikull2NeSesion(Session, dt2);
            gvRivleresim2.Selection.UnselectAll();
            gvRivleresim.Selection.UnselectAll();
            gvRivleresim2.DataSource = dt2;
            gvRivleresim2.DataBind();
            dt2.Dispose();
            gvRivleresim.DataSource = dt1;
            gvRivleresim.DataBind();
            dt1.Dispose();
        }

        protected void btnDjathtasGjitha_Click(object sender, EventArgs e)
        {
            DataTable dt1 = DbCore.mySessionObjects.merrColArtikullNgaSesioni(Session);
            DataTable dt2 = DbCore.mySessionObjects.merrColArtikull2NgaSesioni(Session);
            dt2.Merge(dt1);
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            dt1 = DbCore.DbInventari.colArtikujt.merrSipasArtikujNdermarrjesAndAutorizimeDT(-5, -5, false,false, rm.GetString("postStringTvsh"));
            DbCore.mySessionObjects.ruajColArtikullNeSesion(Session, dt1);
            DbCore.mySessionObjects.ruajColArtikull2NeSesion(Session, dt2);
            gvRivleresim2.Selection.UnselectAll();
            gvRivleresim.Selection.UnselectAll();
            gvRivleresim2.DataSource = dt2;
            gvRivleresim2.DataBind();
            dt2.Dispose();
            gvRivleresim.DataSource = dt1;
            gvRivleresim.DataBind();
            dt1.Dispose();
        }

        protected void btnMajtas1_Click(object sender, EventArgs e)
        {
            DataTable dt1 = DbCore.mySessionObjects.merrColArtikullNgaSesioni(Session);
            DataTable dt2 = DbCore.mySessionObjects.merrColArtikull2NgaSesioni(Session);
            DataColumn[] keys = new DataColumn[1];
            keys[0] = dt2.Columns[0];
            dt2.PrimaryKey = keys;
            gvRivleresim2.DataSource = dt2;
            gvRivleresim2.DataBind();

            gvRivleresim.DataSource = dt1;
            gvRivleresim.DataBind();
            List<object> id = gvRivleresim2.GetSelectedFieldValues("IdArtikulli");

            foreach (object i in id)
            {
                DataRow dd = dt2.Rows.Find(i);
                dt1.Rows.Add(dd.ItemArray);
                //DataRow dr = DbCore.DbInventari.colArtikujt.merrSipasArtikujNdermarrjesAndAutorizimeDR(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), Convert.ToInt32(i));
                //dt1.ImportRow(dr);
                //DataRow[] drs = dt2.Select("IdArtikulli = " + i);
                //if (drs.Length > 1)
                //    throw new Exception("GABIM: Ndodhen 2 artikuj me te njejten id ne gride");
                //if (drs.Length == 0) return;
                //DataRow dr1 = drs[0];
                dt2.Rows.Remove(dd);

            }

            DbCore.mySessionObjects.ruajColArtikullNeSesion(Session, dt1);
            DbCore.mySessionObjects.ruajColArtikull2NeSesion(Session, dt2);
            gvRivleresim2.Selection.UnselectAll();
            gvRivleresim.Selection.UnselectAll();
            gvRivleresim2.DataSource = dt2;
            gvRivleresim2.DataBind();
            dt2.Dispose();
            gvRivleresim.DataSource = dt1;
            gvRivleresim.DataBind();
            dt1.Dispose();
        }

        protected void btnMajtaGjitha_Click(object sender, EventArgs e)
        {
            DataTable dt1 = DbCore.mySessionObjects.merrColArtikullNgaSesioni(Session);
            DataTable dt2 = DbCore.mySessionObjects.merrColArtikull2NgaSesioni(Session);
            dt1.Merge(dt2);
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            dt2 = DbCore.DbInventari.colArtikujt.merrSipasArtikujNdermarrjesAndAutorizimeDT(-5, -5, false, false, rm.GetString("postStringTvsh"));
            DbCore.mySessionObjects.ruajColArtikullNeSesion(Session, dt1);
            DbCore.mySessionObjects.ruajColArtikull2NeSesion(Session, dt2);
            gvRivleresim2.Selection.UnselectAll();
            gvRivleresim.Selection.UnselectAll();
            gvRivleresim2.DataSource = dt2;
            gvRivleresim2.DataBind();
            dt2.Dispose();
            gvRivleresim.DataSource = dt1;// col;
            gvRivleresim.DataBind();
            dt1.Dispose();
        }

        protected void ProgressBar1_RunTask(object sender, EO.Web.ProgressTaskEventArgs e)
        {
            int position = 0;
            e.UpdateProgress(position);
            DataTable dt = DbCore.mySessionObjects.merrColArtikull2NgaSesioni(Session);
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idPerdorues = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            DbCore.clsMesazh mesazhi = new DbCore.clsMesazh(true, "Rillogaritja perfundoi me sukses!");
            var rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            var ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            try
            {
                //  DbCore.DbRegjistrim.colNjesiAdministrative magazinat = new DbCore.DbRegjistrim.colNjesiAdministrative();
                //magazinat.mbushGjitheNjesiAdministrativeAktive(idNdermarrje, idPerdorues);
                //int max = magazinat.Count * dt.Rows.Count;
                System.Threading.Thread.CurrentThread.Priority = System.Threading.ThreadPriority.Lowest;
                DbCore.DbAsete.colAQTSeriale colArtikujMeSerial = new DbCore.DbAsete.colAQTSeriale();
                DbCore.DbAsete.colAQTSeriale colArtikujPaSerial = new DbCore.DbAsete.colAQTSeriale();
                foreach (DataRow dr in dt.Rows)
                {
                    int idArt = Convert.ToInt32(dr["IdArtikulli"]);
                    //Nese jane artikuj me serial mbushet nje collection per keto artikuj
                    if (bool.Parse(dr["MESERIAL"].ToString()))
                    {
                        if (!bool.Parse(cmbLloji.Value.ToString()))
                        {
                            colArtikujMeSerial.AddRange(krijoSeriale(idArt, true, idNdermarrje));
                        }
                        else
                        {
                            DbCore.DbAsete.colAQTSeriale colart = new DbCore.DbAsete.colAQTSeriale();
                            colart.merrAQTSerialSipasIDArtikulliArtikullMeSerial(idArt, idNdermarrje);
                            colArtikujMeSerial.AddRange(colart);
                        }
                    }
                    //Nese jane artikuj pa serial mbushet nje tjeter collection
                    else
                    {
                        if (!bool.Parse(cmbLloji.Value.ToString()))
                        {
                            //TODO per tu bere si do merren serialet specifike qe vendosen ne rastin analitik
                            colArtikujPaSerial.AddRange(krijoSeriale(idArt, false, idNdermarrje));
                        }
                        else
                        {
                            DbCore.DbAsete.colAQTSeriale colart = new DbCore.DbAsete.colAQTSeriale();
                            colart.merrAQTSerialSipasIDArtikulliArtikullPaSerial_VetemPrind(idArt, idNdermarrje);
                            colArtikujPaSerial.AddRange(colart);
                        }
                    }
                }
                DbCore.DbAsete.clsAmortizimiKoka amortizimiPerRillogaritje = new DbCore.DbAsete.clsAmortizimiKoka();
                DbCore.DbAsete.colAmortizimiKoka gjitheKokat = new DbCore.DbAsete.colAmortizimiKoka(); //idNdermarrje, int.Parse(cmbStandarti.Value.ToString()), dtePeriudhaNga.Date
                int standarti = int.Parse(cmbStandarti.Value.ToString());
                string StandartiEmertim = (cmbStandarti.Text.ToString());
                gjitheKokat.merrAmortizimKoka(dtePeriudhaNga.Date, standarti, idNdermarrje);

                if (colArtikujMeSerial.Count > 0)//kaluar dhe emertimi StandartiEmertim sepse e perdor ne msg qe skemi dokument amortizimi mes datave 
                    mesazhi = amortizimiPerRillogaritje.rillogaritAmortizim(gjitheKokat, idNdermarrje, standarti, StandartiEmertim, dtePeriudhaNga.Date, colArtikujMeSerial, idPerdorues, true, e, rm,ci);
                if (!mesazhi.Status)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhi.PershkrimMesazhi, pnlMesazhi);
                    JavaScriptSerializer serializusi = new JavaScriptSerializer();
                    e.UpdateProgress((mesazhi.Status) ? 100 : e.Value, serializusi.Serialize(mesazhi));
                    return;
                }
                if (colArtikujPaSerial.Count > 0)
                    mesazhi = amortizimiPerRillogaritje.rillogaritAmortizim(gjitheKokat, idNdermarrje, standarti, StandartiEmertim, dtePeriudhaNga.Date, colArtikujPaSerial, idPerdorues, false, e, rm,ci);
                else
                 
                    e.UpdateProgress(   (mesazhi.Status)?100:e.Value, mesazhi.PershkrimMesazhi);

                if (!mesazhi.Status)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhi.PershkrimMesazhi, pnlMesazhi);
                    e.UpdateProgress((mesazhi.Status) ? 100 : e.Value, mesazhi.PershkrimMesazhi);
                }

                System.Threading.Thread.CurrentThread.Priority = System.Threading.ThreadPriority.Normal;
            }
            catch (DbCore.MyException m)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(m.Message);
                e.UpdateProgress(e.Value, m.Message);
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, m.Message, pnlMesazhi);
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex.Message);
                System.Threading.Thread.CurrentThread.Priority = System.Threading.ThreadPriority.Normal;
                e.UpdateProgress(e.Value, ex.Message);
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, ex.Message, pnlMesazhi);
            }
        }

        protected void gvRivleresim2_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType == GridViewRowType.Data)
            {
                GridViewDataTextColumn col1 = ((ASPxGridView)sender).Columns["KodiArtikull"] as GridViewDataTextColumn;
                ASPxComboBox cmb1 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col1, "cmbBox") as ASPxComboBox;
                if (cmb1 != null)
                {

                    cmb1.DropDownButton.Visible = false;
                    cmb1.Buttons.Add();
                    cmb1.AutoPostBack = false;
                    cmb1.EnableCallbackMode = false;

                    cmb1.ClientInstanceName = "Serial" + e.VisibleIndex;


                    cmb1.ClientSideEvents.KeyUp = String.Format("function(s,e){{KeyPressKodi('Serial{0}',{0},e);}}", e.VisibleIndex);
                    cmb1.ClientSideEvents.LostFocus = String.Format("function(s,e){{LostFocusKodi('Serial{0}',{0});}}", e.VisibleIndex);
                    cmb1.ClientSideEvents.ButtonClick = String.Format("function(s,e){{ButtonClickKodi('Serial{0}',{0})}}", e.VisibleIndex);
                }
            }
        }
        private DbCore.DbAsete.colAQTSeriale krijoSeriale(int idartikulli, bool meSerial, int idNdermarrje)
        {
            DbCore.DbAsete.colAQTSeriale col = new DbCore.DbAsete.colAQTSeriale();
            JavaScriptSerializer serializusi = new JavaScriptSerializer();

            //clsArtikulli art = new clsArtikulli(idartikulli);
            if (hfSeriale.Contains(idartikulli + "_0"))
            {
                object[] dokumenti = (object[])serializusi.DeserializeObject(hfSeriale.Get(idartikulli + "_0").ToString());
                for (int i = 0; i < dokumenti.Length; i++)
                {
                    DbCore.DbAsete.clsAQTSeriale serial = new DbCore.DbAsete.clsAQTSeriale((Dictionary<string, object>)dokumenti[i]);

                    if (!meSerial)
                    {
                        DbCore.DbAsete.clsHistorikAQTSeriale historiku = new DbCore.DbAsete.clsHistorikAQTSeriale();
                        historiku.merrHistorikAQTSerialSipasID(serial.IdHistorikAktualPaSerial, idNdermarrje);
                        if (historiku.IdPrindiFillestar > 0)
                            serial.merrAQTSerialSipasID(historiku.IdPrindiFillestar);
                    }

                    if (!col.Exists(x => x.IdAQTSerial == serial.IdAQTSerial))
                        col.Add(serial);
                }
            }

            return col;
        }

    }
}