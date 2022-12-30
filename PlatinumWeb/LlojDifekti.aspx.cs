using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Collections;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{
    public partial class LlojDifekti : MyPageBase
    {
        DbCore.DbInventari.clsLlojDifekti llojdifekti;

        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                Response.Redirect(DbCore.IMBUtils.Paths.defaultLoginPath);
                return;
            }
            int idPerd = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerd);
                return;
            }
              int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLlojDifekti",1, "LlojDifekti.aspx");
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (!IsPostBack)
            {   
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, ":");
                if (Request.QueryString["ruaj"] == "ok")
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["mesazhRuajtjeMeSukses"], pnlMesazhi);

                konfiguroVleraFillestare();
                konfiguroGride();
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "LlojDifekti.aspx");
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                            
            }
            
            DbCore.mySessionObjects.ruajMesazhNeSesion(Session, ":");
            konfiguroVleraFillestare();
            GridUtil.ToolTipButonaveMbiGride(gvLlojDifekti, ci, rm);
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
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, "LlojDifekti.aspx", this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, hfRuaj.Value == "Ruaj" ? true : false, true, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
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
        private void konfiguroVleraFillestare()
        {

            DbCore.DbInventari.colLlojDifekti col = new DbCore.DbInventari.colLlojDifekti(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
             gvLlojDifekti.DataSource = col;
            gvLlojDifekti.DataBind();
        }

        private void konfiguroGride()
        {
            GridUtil.percaktoVisibleColumnsMeWidth(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), gvLlojDifekti, "gvLlojDifekti", "LlojDifekti.aspx");
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            GridUtil.konfiguroGrideListeEvogelPaTheme(gvLlojDifekti, "Id");
            gvLlojDifekti.SettingsBehavior.AllowSelectByRowClick = true;
            gvLlojDifekti.Settings.ShowTitlePanel = true; 
        }

        protected void gvLlojDifekti_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            e.Values.Clear();
            e.AddValue("(Te gjithe)", string.Empty, "true");
        }

        protected void gvLlojDifekti_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
        }

        protected void gvLlojDifekti_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {

            llojdifekti = new DbCore.DbInventari.clsLlojDifekti();
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
int idperdoruesi=DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idperdoruesi, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "LlojDifekti.aspx");
            if (!tedrejtaInfo.DShtim)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ju nuk keni te drejta per kete veprim!:Red");
                return;
            }
            llojdifekti.Kodi = e.NewValues["Kodi"].ToString();
            llojdifekti.Pershkrimi = e.NewValues["Pershkrimi"].ToString();
            llojdifekti.IdKrijuesi = idperdoruesi;
            llojdifekti.IdPerdoruesi = idperdoruesi;
            llojdifekti.IdNdermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            llojdifekti.IdStatusDok = 1;
            e.Cancel = true;
            gvLlojDifekti.CancelEdit();
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            if (!DbCore.DbInventari.clsLlojDifekti.ekzistonLlojDifekti(llojdifekti.Kodi, llojdifekti.IdNdermarje))
            {
                mesazh = llojdifekti.ruaj();
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
                konfiguroVleraFillestare();
            }
            else
            {
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ekziston nje njesi me kete kod! Ju lutem zgjidhni nje kod tjeter:Red");
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ekziston nje njesi me kete kod! Ju lutem zgjidhni nje kod tjeter", pnlMesazhi);
                konfiguroGride();
                gvLlojDifekti.AddNewRow();
            }
        }

        protected void gvLlojDifekti_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {//validimi ne jane plotesuar gjithe fushat e detyruara
            foreach (GridViewColumn column in gvLlojDifekti.Columns)
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
            if (hfRuaj.Value == "Ruaj")
            {
                if (e.NewValues["Kodi"] != null)
                {
                    llojdifekti = new DbCore.DbInventari.clsLlojDifekti();

                    llojdifekti.Kodi = e.NewValues["Kodi"].ToString();

                    llojdifekti.IdNdermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);

                    if (DbCore.DbInventari.clsLlojDifekti.ekzistonLlojDifekti(llojdifekti.Kodi, llojdifekti.IdNdermarje))
                    {

                        e.RowError = "Ekziston nje lloj difekti me kete kod! Ju lutem zgjidhni nje kod tjeter";
                    }
                }
                else e.RowError = "Ju lutemi, plotesoni te gjitha fushat";
            }
            if (e.Errors.Count > 0)
            {
                e.RowError = "Ju lutemi, plotesoni te gjitha fushat.";
            }
            if (string.IsNullOrEmpty(e.RowError) && e.Errors.Count > 0)
            {
                e.RowError = "Ju lutemi, korrigjoni te gjithe gabimet.";
            }
        }

        protected void gvLlojDifekti_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {//kur fillon editimi te behet validimi
            if (hfRuaj.Value == "Ruaj")
                if (!gvLlojDifekti.IsNewRowEditing)
                {
                    gvLlojDifekti.DoRowValidation();
                }
        }

        protected void gvLlojDifekti_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            String id = e.Keys["Id"].ToString();
            e.Cancel = true;
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "LlojDifekti.aspx");
            if (!tedrejtaInfo.DMod)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ju nuk keni te drejta per kete veprim!:Red");
                return;
            }
            DbCore.DbInventari.clsLlojDifekti llojdifekti = new DbCore.DbInventari.clsLlojDifekti();
            llojdifekti.Id = int.Parse(id);
            llojdifekti.Kodi = e.NewValues["Kodi"].ToString();
            llojdifekti.Pershkrimi = e.NewValues["Pershkrimi"].ToString();
            llojdifekti.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session); ;
            llojdifekti.IdNdermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            llojdifekti.IdStatusDok = 1;
            DbCore.clsMesazh m = llojdifekti.modifiko();
            if (m.Status)
            {
                 DbCore.mySessionObjects.ruajMesazhNeSesion(Session, m.PershkrimMesazhi + ":Green");
            }
            else
            {
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, m.PershkrimMesazhi + ":Red");
            }
            gvLlojDifekti.CancelEdit();
            konfiguroVleraFillestare();
             
        }
        /// <summary>
        /// fshin filtrin e zgjedhur dhe aplikuar mbi gride, fshirje nga DB ku ai eshte ruajtur, dhe jo pastrimi i grides nga aplikimi i filtrit
        /// per kete do sherbeje Clear Filter ne fund te grides
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
             DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLlojDifekti", "LlojDifekti.aspx", idNdermarrje);
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                mesazh = filtra.fshi();
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLlojDifekti", 1, "LlojDifekti.aspx");
                percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);

                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
               
                gvLlojDifekti.FilterExpression = String.Empty;
            }
        }
        /// <summary>
        /// sherben per te ruajtur filtrin e zgjedhur
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DbCore.DbAdmin.clsFiltraGrida filtri = new DbCore.DbAdmin.clsFiltraGrida();
            filtri.FiltraKodi = cmbFiltra.Text;
            filtri.FiltraShenime = cmbFiltra.Text;
            filtri.FiltraUniversal = false;// Universal_ASPxCheckBox.Checked;
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLlojDifekti", "LlojDifekti.aspx", idNdermarrje);
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gvLlojDifekti.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("Kodi", gvLlojDifekti);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvLlojDifekti.GetSortedColumns();
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
            //    filtri.KoloneRenditje = "Kodi";
            //    filtri.DrejtimRenditje = true;
            //}
            DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();
            oPerdorues = DbCore.mySessionObjects.kthePerdorues(Session);
            filtri.IdPerdoruesi = oPerdorues.IdPerdorues;
            filtri.IdNdermarje = idNdermarrje;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            filtri.IdStatusDok = 1;

            mesazh = filtri.ruaj();
             clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLlojDifekti", 1, "LlojDifekti.aspx");
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);

            if (mesazh.Status == true)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            cmbFiltra.Text = "";

        }

        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            int a = gvLlojDifekti.FocusedRowIndex;
            gvLlojDifekti.Selection.SelectRow(a);
            List<object> rreshtat = gvLlojDifekti.GetSelectedFieldValues("Id");
            DbCore.DbInventari.clsDatabaseInventari dbInventari = new DbCore.DbInventari.clsDatabaseInventari();
            foreach (int id in rreshtat)
            {
                DbCore.DbInventari.clsLlojDifekti col = new DbCore.DbInventari.clsLlojDifekti(id);
                col.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                if (dbInventari.kaVeprimeLlojDifekti(col.Id) || (DbCore.mySessionObjects.merrEshteMemeSesioni(Session) && DbCore.DbInventari.clsLlojDifekti.eshteTransferuarTekBij(col.Kodi, col.IdNdermarje)))

                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ka veprime me kete lloj difekti", pnlMesazhi);
                else
                {
                    col.fshi();
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["msgFshirjeMeSukses"], pnlMesazhi);
                }

                konfiguroVleraFillestare();

            }
            dbInventari.Dispose();
            pnlGrida.Update();
        }

        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {
           
        }

        protected void gvLlojDifekti_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
        }

        protected void gvLlojDifekti_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
                       
        }

        protected void gvLlojDifekti_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
        }

        protected void gvLlojDifekti_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                if (arr[2] == "")
                    gvLlojDifekti.FilterExpression = "";
                else
                {

                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLlojDifekti", "LlojDifekti.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        gvLlojDifekti.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvLlojDifekti);
                    }
                }
            }
            konfiguroGride();
        }

        protected void gvLlojDifekti_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gvLlojDifekti.PageIndex;
            e.Properties["cpPageRow"] = gvLlojDifekti.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gvLlojDifekti.VisibleRowCount;
        }
    }
}