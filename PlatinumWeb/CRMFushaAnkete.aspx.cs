using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using DbCore.DbCRM;
using DevExpress.Web.Data;
using System.Globalization;
using System.Resources;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{
    public partial class CRMFushaAnkete : MyPageBase
    {
        DbCore.DbCRM.clsOpsioneAnkete fusha;
        private int idPerd;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                Response.Redirect(DbCore.IMBUtils.Paths.defaultLoginPath);
                return;
            }
            idPerd = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            var idVitNdermarrje = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            hfState.Set("idPerdoruesi", idPerd);
            hfState.Set("idNdermarrje", idNdermarrje);
            hfState.Set("idVitNdermarrje", idVitNdermarrje);
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerd);
                return;
            }
            percaktoTemplateMenu(ASPxMenu1, idVitNdermarrje, idPerd, idNdermarrje);

            if (!IsPostBack)
            {
                //Session.Add("mesazh", ":Green");

                 DbCore.mySessionObjects.ruajMesazhNeSesion(Session, ":");
                if (Request.QueryString["ruaj"] == "ok")
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["mesazhRuajtjeMeSukses"], pnlMesazhi);

                konfiguroVleraFillestare();
                konfiguroGride();
                
              
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerd, idNdermarrje, idNdermarrje, "CRMFushaAnkete.aspx");
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                lblUserEmri.Text = DbCore.mySessionObjects.ktheEmerPerdorues(Session);
            }
            DbCore.mySessionObjects.ruajMesazhNeSesion(Session, ":");
            konfiguroVleraFillestare();
          
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
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, "CRMFushaAnkete.aspx", this, MenuInfo,  hfRuaj.Value == "Ruaj" ? true : false, true, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session), true);
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

        protected void gvFushaAnkete_DataBound(object sender, EventArgs e)
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (gvFushaAnkete.Columns["#"] == null)
            {
                GridViewCommandColumn check = new GridViewCommandColumn("#");
                gvFushaAnkete.Columns.Add(check);
                check.ShowSelectCheckbox = true; check.Width = Unit.Percentage(2);
                //check.SetColVisibleIndex(0);

                gvFushaAnkete.SettingsText.CommandUpdate = rm.GetString("buttonRuaj", cultinf);
                gvFushaAnkete.SettingsText.CommandCancel = rm.GetString("labelAnullo", cultinf);

                gvFushaAnkete.Settings.ShowFilterRow = true;
                gvFushaAnkete.KeyFieldName = "IdOpsioni";
                gvFushaAnkete.SettingsBehavior.AllowSelectByRowClick = true;
                gvFushaAnkete.SettingsBehavior.AllowFocusedRow = true;
                gvFushaAnkete.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                gvFushaAnkete.Settings.ShowFilterRowMenu = true;
            }
        }
        protected void konfiguroGride()
        {

            GridUtil.percaktoVisibleColumnsMeWidth(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), gvFushaAnkete, "gvFushaAnkete", "CRMFushaAnkete.aspx");
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            GridUtil.konfiguroGrideListeEvogelPaTheme(gvFushaAnkete, "IdOpsioni");
            gvFushaAnkete.SettingsBehavior.AllowGroup = false;
            gvFushaAnkete.Columns["#"].VisibleIndex = 0;
        }
        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {
            
        }


        protected void gvFushaAnkete_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gvFushaAnkete.PageIndex;
            e.Properties["cpPageRow"] = gvFushaAnkete.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gvFushaAnkete.VisibleRowCount;
        }

        protected void gvFushaAnkete_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            e.Values.Clear();
            e.AddValue("(Te gjithe)", string.Empty, "true");
        }


        protected void gvFushaAnkete_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            for (int i = 0; i < e.NewValues.Count; i++)
            {
                if (e.NewValues[i] == null)
                {
                    e.Cancel = true;
                    return;
                }
            }
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "CRMFushaAnkete.aspx");
            if (!tedrejtaInfo.DShtim)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNukKeniTeDrejta", cultinf), pnlMesazhi);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, rm.GetString("msgNukKeniTeDrejtaRed", cultinf));
                return;
            }
            DbCore.DbCRM.clsOpsioneAnkete opsion = new DbCore.DbCRM.clsOpsioneAnkete();
            opsion.Emertimi = e.NewValues["Emertimi"].ToString();
            opsion.DtKrijimi = DateTime.Today;
            opsion.IdKrijuesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            opsion.IdNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            opsion.IdStatusDok = 1;
            e.Cancel = true;
           // gvFushaAnkete.CancelEdit();
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            mesazh = opsion.ruaj();
            if (!mesazh.Status)
            {
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, mesazh.PershkrimMesazhi + ":Red");
              //  konfiguroGride();
            }
            else
            {
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, rm.GetString("msgRuajtjeMeSuksesGreen", cultinf));
                gvFushaAnkete.CancelEdit();
                konfiguroVleraFillestare();
            }
        }

              

        

        private void konfiguroVleraFillestare()
        {
            DbCore.DbCRM.colOpsioneAnkete col = new DbCore.DbCRM.colOpsioneAnkete(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            gvFushaAnkete.DataSource = col;
            gvFushaAnkete.DataBind();
        }


     

        protected void gvFushaAnkete_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {//validimi ne jane plotesuar gjithe fushat e detyruara
            DbCore.DbCRM.clsDatabaseCRM dbCRM = new DbCore.DbCRM.clsDatabaseCRM();
            if (hfRuaj.Value == "Modifiko")
            {
                
                    if (dbCRM.kaVeprimeOpsion(int.Parse(e.Keys["IdOpsioni"].ToString())))
                                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ka veprime me kete fusha", pnlMesazhi);
            }           

            foreach (GridViewColumn column in gvFushaAnkete.Columns)
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
            
                if (e.NewValues["Emertimi"] != null)
                {
                    fusha = new DbCore.DbCRM.clsOpsioneAnkete();

                    fusha.Emertimi = e.NewValues["Emertimi"].ToString();
                    if (e.Keys["IdOpsioni"] != null)
                    {
                        fusha.IdOpsioni = int.Parse(e.Keys["IdOpsioni"].ToString());
                    }
                    else fusha.IdOpsioni = -1;
                    fusha.IdNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);

                    if (DbCore.DbCRM.clsOpsioneAnkete.ekzistonFushaAnketeMeEmertim(fusha.IdOpsioni,fusha.Emertimi, fusha.IdNdermarrje))
                    {

                        e.RowError = "Ekziston nje fusha me kete emertim! Ju lutem zgjidhni nje emertim tjeter";
                    }             
                
            }
               // else e.RowError = "Ju lutemi, plotesoni te gjitha fushat";
            if (e.Errors.Count > 0)
            {
                e.RowError = "Ju lutemi, plotesoni te gjitha fushat.";
            }
            if (string.IsNullOrEmpty(e.RowError) && e.Errors.Count > 0)
            {
                e.RowError = "Ju lutemi, korrigjoni te gjithe gabimet.";
            }
        }

        protected void gvFushaAnkete_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {//kur fillon editimi te behet validimi
            if (hfRuaj.Value == "Ruaj")
                if (!gvFushaAnkete.IsNewRowEditing)
                {
                    gvFushaAnkete.DoRowValidation();
                }
        }

        protected void gvFushaAnkete_RowUpdating(object sender,ASPxDataUpdatingEventArgs e)
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "CRMFushaAnkete.aspx");
            if (!tedrejtaInfo.DMod)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("mesazhRaportNukKeniTeDrejta", cultinf), pnlMesazhi);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, rm.GetString("msgNukKeniTeDrejtaRed", cultinf));
                return;
            }
            String id = e.Keys["IdOpsioni"].ToString();
            e.Cancel = true;           
            DbCore.DbCRM.clsOpsioneAnkete fusha = new DbCore.DbCRM.clsOpsioneAnkete();
            fusha.IdOpsioni = int.Parse(id);
            fusha.Emertimi = e.NewValues["Emertimi"].ToString();
            fusha.IdModifikuesi = idPerd;
            fusha.IdNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            fusha.IdStatusDok = 1;
            DbCore.clsMesazh m = fusha.modifiko();
            if (m.Status)
            {
                 DbCore.mySessionObjects.ruajMesazhNeSesion(Session, m.PershkrimMesazhi + ":Green");
            }
            else
            {
                 DbCore.mySessionObjects.ruajMesazhNeSesion(Session, m.PershkrimMesazhi + ":Red");
            }
            gvFushaAnkete.CancelEdit();
           // konfiguroVleraFillestare();
               
        }


        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            int a = gvFushaAnkete.FocusedRowIndex;
            gvFushaAnkete.Selection.SelectRow(a);
            List<object> rreshtat = gvFushaAnkete.GetSelectedFieldValues("IdOpsioni");
            DbCore.DbCRM.clsDatabaseCRM DbCRM = new DbCore.DbCRM.clsDatabaseCRM();
            foreach (int id in rreshtat)
            {
                if (DbCRM.kaVeprimeOpsion(id))
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ka veprime me kete fushe", pnlMesazhi);
                else
                {
                    clsOpsioneAnkete.fshi(id, idPerd);
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["msgFshirjeMeSukses"], pnlMesazhi);
                }

                konfiguroVleraFillestare();
            }
            DbCRM.Dispose();
            pnlGrida.Update();
        }

      

        protected void gvFushaAnkete_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
        }

        protected void gvFushaAnkete_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
              konfiguroVleraFillestare();            
        }

        protected void gvFushaAnkete_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                if (arr[2] == "")
                    gvFushaAnkete.FilterExpression = "";
                else
                {
                    GridUtil.AplikoFilter(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), gvFushaAnkete, arr[2], "gvFushaAnkete", "CRMFushaAnkete.aspx", 1);
                    //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
                    //DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    ////filtra.mbushFiltraGridaSipasFiltraKodi(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    //DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhePerdoruesi(Session), "gvFushaAnkete", "CRMFushaAnkete.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    //filtra.mbushFilterPerGrideSipasKodit(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
                    ////DbCore.DbAdmin.clsFiltraGrida filtra = dbAdmin.merrFiltraGridaSipasFiltraKodi(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    //if (filtra.FiltraKodi != null)
                    //{
                    //    gvFushaAnkete.FilterExpression = filtra.FiltraVlera;
                    //    if (filtra.DrejtimRenditje == true)
                    //        gvFushaAnkete.SortBy(gvFushaAnkete.Columns[filtra.KoloneRenditje], DevExpress.Data.ColumnSortOrder.Ascending);
                    //    else
                    //        gvFushaAnkete.SortBy(gvFushaAnkete.Columns[filtra.KoloneRenditje], DevExpress.Data.ColumnSortOrder.Descending);

                    //    //  konfiguroVleraFillestare();

                    //}
                }
            }
            konfiguroGride();
        }

        //protected void Modifiko_ASPxButton_Click(object sender, EventArgs e)
        //{
        //    int a = gvFushaAnkete.FocusedRowIndex;
        //    gvFushaAnkete.StartEdit(a);
        //} 
    }
}