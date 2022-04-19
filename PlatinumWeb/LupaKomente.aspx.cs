
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Collections;
using System.Drawing;
using System.Globalization;
using System.Resources;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.DbShare;

namespace PlatinumWeb
{
    public partial class LupaKomente : MyPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            if (!Page.IsPostBack)
            {
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, ":");
                mbushPopUpKomente();
                konfiguroPopupGride();
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "LupaKomente.aspx");
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                if (!tedrejtaInfo.DShtim) gvLupaKomente.CancelEdit();
            }

            DbCore.mySessionObjects.ruajMesazhNeSesion(Session, ":");
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
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, "LupaKomente.aspx", this, MenuInfo, true, true, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));

            DbCore.DbRegjistrim.clsEtapeAprovimi etapa = new DbCore.DbRegjistrim.clsEtapeAprovimi(int.Parse(Request.QueryString["idetapa"]),int.Parse(Request.QueryString["idkategoria"]));
            DbCore.DbAdmin.clsTrupiSkemaWorkFlow tt = new DbCore.DbAdmin.clsTrupiSkemaWorkFlow();
            tt.merrTrupSipasKokesDhePerdoruesit(etapa.IdSkema, DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            if (etapa.Niveli >= tt.Niveli)
            { aSPxMenu1.Items.FindByName("Shto").ClientEnabled = true; aSPxMenu1.Items.FindByName("Ruaj").ClientEnabled = true; }
            else { aSPxMenu1.Items.FindByName("Shto").ClientEnabled = false; aSPxMenu1.Items.FindByName("Ruaj").ClientEnabled = true; }
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
        private void mbushPopUpKomente()
        {//mbush griden e popupit me te dhena
            DbCore.DbRegjistrim.colKomenteAprovimi col = new DbCore.DbRegjistrim.colKomenteAprovimi();

            string idetapa = Request.QueryString["idetapa"]; string nrprocesi = Request.QueryString["nrprocesi"];
            string veprimi = Request.QueryString["veprimi"];
            if (veprimi == "Gjitha")
                col.merrKomenteSipasProcesit(int.Parse(nrprocesi), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));

            else col.merrKomenteSipasEtapes(int.Parse(idetapa));

            gvLupaKomente.DataSource = col;
            gvLupaKomente.DataBind();
        }

        private void konfiguroPopupGride()
        {//konfiguron popupgriden
            GridUtil.percaktoVisibleColumns(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), gvLupaKomente, "gvLupaKomente", "LupaKomente.aspx");
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            var endlessScroll = clsAlternativaKushti.getAlternativa(clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod("LP/Komente", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session)), "ES") == "Po";
            GridUtil.konfiguroGrideListeEvogelPopupiPaTheme(gvLupaKomente, "IdKomenti", endlessScroll);
            gvLupaKomente.CancelEdit();
            ((GridViewDataColumn)gvLupaKomente.Columns["Data"]).EditFormSettings.Visible = DevExpress.Utils.DefaultBoolean.False;
            ((GridViewDataColumn)gvLupaKomente.Columns["NrEtape"]).EditFormSettings.Visible = DevExpress.Utils.DefaultBoolean.False;
            ((GridViewDataColumn)gvLupaKomente.Columns["PerdoruesUsername"]).EditFormSettings.Visible = DevExpress.Utils.DefaultBoolean.False;
            GridViewDataDateColumn col7 = gvLupaKomente.Columns["Data"] as GridViewDataDateColumn;
            col7.PropertiesEdit.DisplayFormatString = "dd/MM/yyyy hh:mm:ss";
            mbushPopUpKomente();
        }

        protected void gvLupaKomente_DataBound(object sender, EventArgs e)
        {
            gvLupaKomente.SettingsText.CommandUpdate = "Ruaj";
            gvLupaKomente.KeyFieldName = "IdKomenti";
            gvLupaKomente.SettingsBehavior.AllowSelectByRowClick = true;
            gvLupaKomente.SettingsBehavior.AllowFocusedRow = true;
        }

        protected void gvLupaKomente_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        { //kur popupgrida ben callback
            mbushPopUpKomente();
            gvLupaKomente.Selection.UnselectAll();
        }

        protected void gvLupaKomente_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
        }

        protected void gvLupaKomente_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {//merr te dhenat e rreshtit te ri te grides
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            if (e.NewValues["Koment"] == null)
            {
                e.Cancel = true;
                return;
            }

            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "LupaKomente.aspx");
            if (!tedrejtaInfo.DShtim)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("mesazhRaportNukKeniTeDrejta", ci), pnlMesazhi);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, rm.GetString("msgNukKeniTeDrejtaRed", ci));
                return;
            }
            DbCore.DbRegjistrim.clsKomenteAprovimi koment = new DbCore.DbRegjistrim.clsKomenteAprovimi();
            koment.Data = DateTime.Now;
            koment.IdEtape = int.Parse(Request.QueryString["idetapa"]);
            koment.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            koment.Koment = e.NewValues["Koment"].ToString();

            e.Cancel = true;

            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            mesazh = koment.ruaj();
            if (!mesazh.Status == true)
            {
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, rm.GetString("msgNdodhiGabimRed", ci));
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("labelRaportMesazhRuajtjaPerfundoiGabime", ci), pnlMesazhi);
            }
            else
            {
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, rm.GetString("msgRuajtjeMeSuksesGreen", ci));
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("labelRaportMesazhRuajtjaPerfundoiSukses", ci), pnlMesazhi);
            }
            gvLupaKomente.CancelEdit(); gvLupaKomente.SettingsEditing.Mode = GridViewEditingMode.Inline;
            mbushPopUpKomente();



        }

        protected void gvLupaKomente_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {//validimi ne jane plotesuar gjithe fushat e detyruara
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);

            foreach (GridViewColumn column in gvLupaKomente.Columns)
            {
                if (column.Visible == true)
                {
                    GridViewDataColumn dataColumn = column as GridViewDataColumn;
                    if (dataColumn == null) continue;

                    if (dataColumn.Name == "Koment" && e.NewValues[dataColumn.Name] == null)//validimi per kolonat e detyrueshme
                    {

                        e.Errors[dataColumn] = rm.GetString("msgStrukturaAdministrativeVlereJoNull", ci);
                    }
                }

            }
            if (e.Errors.Count > 0)
            {
                e.RowError = rm.GetString("msgStrukturaAdministrativePlotesoFushat", ci);

            }

            if (string.IsNullOrEmpty(e.RowError) && e.Errors.Count > 0)
            {
                e.RowError = rm.GetString("msgStrukturaAdministrativeKorrigjoGabimet", ci);

            }
            mbushPopUpKomente();
        }

        protected void gvLupaKomente_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {//kur fillon editimi te behet validimi
            if (!gvLupaKomente.IsNewRowEditing)
            {
                gvLupaKomente.DoRowValidation();
            }

        }


        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {//veprimet e menuse


            if (e.Item.Name == "Shto")
            {
                gvLupaKomente.AddNewRow();
                gvLupaKomente.SettingsEditing.Mode = GridViewEditingMode.EditForm;
            }
        }

        protected void gvLupaKomente_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
        {

        }
    }
}