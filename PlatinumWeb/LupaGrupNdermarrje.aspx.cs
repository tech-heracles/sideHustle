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
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class LupaGrupNdermarrje : MyPageBase
    {
        DbCore.DbAdmin.clsGrupNdermarrje grupoverview = new DbCore.DbAdmin.clsGrupNdermarrje();

        protected void Page_Load(object sender, EventArgs e)
        {
     
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            if (!Page.IsPostBack)
            {
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, ":");
                mbushPopUpListeGrupe();
                konfiguroPopupGride(true);
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "LupaGrupNdermarrje.aspx");
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                if (!tedrejtaInfo.DShtim) gvLupaGrNdermarje.CancelEdit();
            }
            DbCore.DbAdmin.clsPerdorues oPerdorues = DbCore.mySessionObjects.kthePerdorues(Session);
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
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, "LupaGrupNdermarrje.aspx", this, MenuInfo, true, true, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
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

        private void mbushPopUpListeGrupe()
        {//mbush griden e popupit me te dhena
            DbCore.DbAdmin.clsNdermarrje nderm = new DbCore.DbAdmin.clsNdermarrje(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            DbCore.DbAdmin.colGrupNdermarje colGrupe = new DbCore.DbAdmin.colGrupNdermarje(nderm.IdLicenca);
            gvLupaGrNdermarje.DataSource = colGrupe;
            gvLupaGrNdermarje.DataBind();
        }

        private void konfiguroPopupGride(bool kerkosaposhkruar)
        {//konfiguron popupgriden            
            mbushPopUpListeGrupe();
            GridUtil.percaktoVisibleColumns(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), gvLupaGrNdermarje, "gvLupaGrNdermarje", "LupaGrupNdermarrje.aspx");
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            GridUtil.KonfiguroGrideListeMadhePopupiPaTheme(gvLupaGrNdermarje, "Id", kerkosaposhkruar, false);
        }

        protected void gvLupaGrNdermarje_DataBound(object sender, EventArgs e)
        {
            gvLupaGrNdermarje.SettingsText.CommandUpdate = "Ruaj";
            gvLupaGrNdermarje.KeyFieldName = "Id";
            gvLupaGrNdermarje.SettingsBehavior.AllowSelectByRowClick = true;
            gvLupaGrNdermarje.SettingsBehavior.AllowFocusedRow = true;
        }

        protected void gvLupaGrNdermarje_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        { //kur popupgrida ben callback
            mbushPopUpListeGrupe();
            gvLupaGrNdermarje.Selection.UnselectAll();
        }

        protected void gvLupaGrNdermarje_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
        }

        protected void gvLupaGrNdermarje_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {//merr te dhenat e rreshtit te ri te grides
              ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);

            for (int i = 0; i < e.NewValues.Count; i++)
            {
                if (e.NewValues[i] == null)
                {
                    e.Cancel = true;
                    return;
                }
            }
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "LupaGrupNdermarrje.aspx");
            if (!tedrejtaInfo.DShtim)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("mesazhRaportNukKeniTeDrejta", cultinf), pnlMesazhi);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, rm.GetString("msgNdodhiGabimRed", cultinf));
                return;
            }
            grupoverview = new DbCore.DbAdmin.clsGrupNdermarrje();
            grupoverview.Kodi = e.NewValues["Kodi"].ToString();
            grupoverview.Pershkrimi = e.NewValues["Pershkrimi"].ToString();
            grupoverview.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            DbCore.DbAdmin.clsNdermarrje nderm = new DbCore.DbAdmin.clsNdermarrje(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            grupoverview.IdLicenca = nderm.IdLicenca;
            grupoverview.IdKrijuesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            grupoverview.IdStatusDok = 1;
            e.Cancel = true;
            if (isValidGrupBankeOverview())
            {
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = grupoverview.ruaj();
                if (!mesazh.Status == true)
                {
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session,rm.GetString("msgNdodhiGabimRed", cultinf));
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("labelRaportMesazhRuajtjaPerfundoiGabime",cultinf), pnlMesazhi);
                }
                else
                {
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, rm.GetString("msgRuajtjeMeSuksesGreen", cultinf));
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("labelRaportMesazhRuajtjaPerfundoiSukses", cultinf), pnlMesazhi);
                }
                gvLupaGrNdermarje.CancelEdit(); gvLupaGrNdermarje.SettingsEditing.Mode = GridViewEditingMode.Inline;
                mbushPopUpListeGrupe();

                gvLupaGrNdermarje.AddNewRow();
            }
            else
            {
                mbushPopUpListeGrupe();

            }
        }

        protected void gvLupaGrNdermarje_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {//validimi ne jane plotesuar gjithe fushat e detyruara
            ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);


            foreach (GridViewColumn column in gvLupaGrNdermarje.Columns)
            {
                if (column.Visible == true)
                {
                    GridViewDataColumn dataColumn = column as GridViewDataColumn;
                    if (dataColumn == null) continue;

                    if (e.NewValues[dataColumn.FieldName] == null)//validimi per kolonat e detyrueshme
                    {
                       
                        e.Errors[dataColumn] = rm.GetString("msgStrukturaAdministrativeVlereJoNull", cultinf);
                    }
                }

            }
            if (e.Errors.Count > 0)
            {
                e.RowError = rm.GetString("msgStrukturaAdministrativePlotesoFushat", cultinf);

            }

            if (string.IsNullOrEmpty(e.RowError) && e.Errors.Count > 0)
            {
                e.RowError = rm.GetString("msgStrukturaAdministrativeKorrigjoGabimet", cultinf); 

            }
            mbushPopUpListeGrupe();
        }

        protected void gvLupaGrNdermarje_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {//kur fillon editimi te behet validimi
            if (!gvLupaGrNdermarje.IsNewRowEditing)
            {
                gvLupaGrNdermarje.DoRowValidation();
            }
        }

        private void ruajGrupBankeOverview()
        {//ben ruajtjen  e nje rreshti te ri
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);

            if (Page.IsValid == false)
                return;
            else
            {

                gvLupaGrNdermarje.UpdateEdit();
                if (isValidGrupBankeOverview())
                {
                    DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                    mesazh = grupoverview.ruaj();
                    if (!mesazh.Status == true)
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo,rm.GetString("labelRaportMesazhRuajtjaPerfundoiGabime", cultinf), pnlMesazhi);
                    }
                    else
                    {
                        clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("labelRaportMesazhRuajtjaPerfundoiSukses", cultinf), pnlMesazhi);
                    }
                    mbushPopUpListeGrupe();
                    konfiguroPopupGride(true);
                    gvLupaGrNdermarje.AddNewRow();
                }
                else
                {
                    mbushPopUpListeGrupe();
                }
            }
        }

        //kontrollon nese grupi i bankes ekziston
        private bool isValidGrupBankeOverview()
        {
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);

            bool isValid;
            isValid = true;
            DbCore.DbAdmin.clsDatabaseAdmin dbadm = new DbCore.DbAdmin.clsDatabaseAdmin();
            DbCore.DbAdmin.clsNdermarrje nderm = new DbCore.DbAdmin.clsNdermarrje(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            if (dbadm.ekzistonGrupNdermarrje(grupoverview.Kodi, nderm.IdLicenca))
            {
                isValid = false;
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, rm.GetString("msgEkzistonGrupMeKeteKodRed", cultinf));
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgEkzistonGrupMeKeteKod",cultinf), pnlMesazhi);
                return isValid;
            }
            dbadm.Dispose();
            return isValid;
        }

        protected void gvLupaGrNdermarje_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {//ben modifikimin e nje rreshti
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);

            for (int i = 0; i < e.NewValues.Count; i++)
            {
                if (e.NewValues[i] == null)
                {
                    e.Cancel = true;
                    return;
                }
            }
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "LupaGrupNdermarrje.aspx");
            if (!tedrejtaInfo.DMod)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("mesazhRaportNukKeniTeDrejta", cultinf), pnlMesazhi);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, rm.GetString("msgNukKeniTeDrejtaRed", cultinf));
                return;
            }
            DbCore.DbAdmin.clsGrupNdermarrje grup = new DbCore.DbAdmin.clsGrupNdermarrje();
            grup.Id = int.Parse(e.Keys["Id"].ToString());
            grup.Kodi = e.NewValues["Kodi"].ToString();
            grup.Pershkrimi = e.NewValues["Pershkrimi"].ToString();
            grup.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            grup.IdStatusDok = 1;
            DbCore.DbAdmin.clsNdermarrje nderm = new DbCore.DbAdmin.clsNdermarrje(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            grup.IdLicenca = nderm.IdLicenca;
            DbCore.DbAdmin.clsGrupNdermarrje grupveter = new DbCore.DbAdmin.clsGrupNdermarrje(grup.Id);
            grup.IdKrijuesi = grupveter.IdKrijuesi;
            if (grup.Pershkrimi != "" && grup.Kodi != "")
            {
                e.Cancel = true;
                grup.modifiko();
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, rm.GetString("msgRuajtjeMeSuksesGreen", cultinf));
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("labelRaportMesazhRuajtjaPerfundoiSukses", cultinf), pnlMesazhi);
                gvLupaGrNdermarje.CancelEdit();
            }
            gvLupaGrNdermarje.SettingsEditing.Mode = GridViewEditingMode.Inline;
            mbushPopUpListeGrupe();
        }

        protected void ButtonOk_Click2(object sender, EventArgs e)
        {//fshirja e nje grup kontabilizimi
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);

            List<object> rreshtat = gvLupaGrNdermarje.GetSelectedFieldValues("Id");
            DbCore.DbAdmin.clsDatabaseAdmin dbAdm = new DbCore.DbAdmin.clsDatabaseAdmin();
            foreach (int id in rreshtat)
            { 
                
                DbCore.DbAdmin.clsGrupNdermarrje grupveter = new DbCore.DbAdmin.clsGrupNdermarrje(id);

                if (dbAdm.kaNdermarje(grupveter.Id))
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgGrupNukNdermarrje", cultinf) , pnlMesazhi);
                }
                else
                {
                    grupveter.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                    grupveter.fshi();
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("msgFshirjaMeSukses", cultinf), pnlMesazhi);
                    mbushPopUpListeGrupe();
                }
            }
            dbAdm.Dispose();
        }


        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {//veprimet e menuse
            if (e.Item.Name == "Modifiko")
            {
                int indeksi = gvLupaGrNdermarje.FocusedRowIndex;
                gvLupaGrNdermarje.StartEdit(indeksi);
                gvLupaGrNdermarje.SettingsEditing.Mode = GridViewEditingMode.EditForm;
            }
            else if (e.Item.Name == "Pastro")
            {
                gvLupaGrNdermarje.SettingsEditing.Mode = GridViewEditingMode.Inline;
                gvLupaGrNdermarje.AddNewRow();
            }
            else if (e.Item.Name == "Shto")
            {
                gvLupaGrNdermarje.AddNewRow();
                gvLupaGrNdermarje.SettingsEditing.Mode = GridViewEditingMode.Inline;
            }
        }

        protected void gvLupaGrNdermarje_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
        {

        }
    }
}
