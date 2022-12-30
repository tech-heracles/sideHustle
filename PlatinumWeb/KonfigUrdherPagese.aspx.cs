using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using DbCore.IMBUtils.Messages;
using DevExpress.Web;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class KonfigUrdherPagese : MyPageBase
    {
        private int idperdoruesi, idnderviti;
        private const string gabimEksitimi = "ekziston nje grup me kete kod. Ju lutem shenoni nje tjeter!";
        private const string gabimEksitimiTitulli = "ekziston nje titull me kete kod. Ju lutem shenoni nje tjeter!";
        private const string gabimEksitimiKapitull = "ekziston nje kapitull me kete kod. Ju lutem shenoni nje tjeter!";
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);

            //if (CacheLayer.GlobalCacheManager.MySessionCache["LoggedIn"].Equals("No"))
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                Response.Redirect(DbCore.IMBUtils.Paths.defaultLoginPath);
                return;
            }
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            idperdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            idnderviti = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
            int idviti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            //if (CacheLayer.GlobalCacheManager.MySessionCache["KodiNdermarrjes"] == null)
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idperdoruesi);
                return;
            }

            percaktoTemplateMenu(ASPxMenu1, idviti, idperdoruesi, idNdermarrje);
            //mbushComboBoxFiltra(idNdermarrje);
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvKonfigUrdherPagese", 1, "KonfigUrdherPagese.aspx");
            if (!IsPostBack)
            {
                //Session.Add("mesazh", ":Green");
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, ":");
                if (Request.QueryString["ruaj"] == "ok")
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["mesazhRuajtjeMeSukses"], pnlMesazhi);

                konfiguroVleraFillestare(idNdermarrje);
                konfiguroGride(idNdermarrje);
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idperdoruesi, idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.clsFunksione.GetKomponente(Page.Request));
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);

            }

            DbCore.mySessionObjects.ruajMesazhNeSesion(Session, ":");
            konfiguroVleraFillestare(idNdermarrje);
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
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, DbCore.clsFunksione.GetKomponente(Page.Request), this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, hfRuaj.Value == "Ruaj" ? true : false, true, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
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
        private void konfiguroVleraFillestare(int idNdermarrje)
        {
            int lloji = 0;
            if (Request.QueryString["lloji"] == "Grup")
                lloji = 1;
            else if (Request.QueryString["lloji"] == "Titull")
                lloji = 2;
            else lloji = 3;
            DbCore.DbArkaBanka.colKonfigUrdherPagese col = new DbCore.DbArkaBanka.colKonfigUrdherPagese(idNdermarrje, lloji);
            gvKonfigUrdherPagese.DataSource = col;
            gvKonfigUrdherPagese.DataBind();
        }

        private void konfiguroGride(int idNdermarrje)
        {
            GridUtil.percaktoVisibleColumnsMeWidth(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, gvKonfigUrdherPagese, "gvKonfigUrdherPagese", "KonfigUrdherPagese.aspx");
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            GridUtil.konfiguroGrideListeEvogelPaTheme(gvKonfigUrdherPagese, "Id");
        }

        protected void gvKonfigUrdherPagese_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            e.Values.Clear();
            e.AddValue("(Te gjithe)", string.Empty, "true");
        }

        protected void gvKonfigUrdherPagese_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
        }

        protected void gvKonfigUrdherPagese_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            int lloji = 0;
            if (Request.QueryString["lloji"] == "Grup")
                lloji = 1;
            else
                if (Request.QueryString["lloji"] == "Titull")
                    lloji = 2;
                else
                    lloji = 3;
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.clsFunksione.GetKomponente(Page.Request));
            if (!tedrejtaInfo.DShtim)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ju nuk keni te drejta per kete veprim!:Red");
                return;
            }
            DbCore.DbArkaBanka.clsKonfigUrdherPagese konf = new DbCore.DbArkaBanka.clsKonfigUrdherPagese() { Kodi = e.NewValues["Kodi"].ToString(), Pershkrimi = (e.NewValues["Pershkrimi"] == null) ? "" : e.NewValues["Pershkrimi"].ToString(), IdPerdoruesi = idperdoruesi, IdNdermarje = idNdermarrje, Lloji = lloji, IdStatusDok = 1 };
            e.Cancel = true;
            gvKonfigUrdherPagese.CancelEdit();
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();

            mesazh = konf.ruaj();
            if (!mesazh.Status == true)
            {
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, mesazh.PershkrimMesazhi + ":Red");
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                konfiguroGride(idNdermarrje);
                gvKonfigUrdherPagese.AddNewRow();
            }
            else
            {
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, MessagesResource.Messages["msgRuajtjeMeSuksesGreen"]);
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["mesazhRuajtjeMeSukses"], pnlMesazhi);
            }
            konfiguroVleraFillestare(idNdermarrje);
        }

        protected void gvKonfigUrdherPagese_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {//validimi ne jane plotesuar gjithe fushat e detyruara
            foreach (GridViewColumn column in gvKonfigUrdherPagese.Columns)
            {
                if (column.Visible == true)
                {
                    GridViewDataColumn dataColumn = column as GridViewDataColumn;
                    if (dataColumn == null) continue;

                    if (e.NewValues[dataColumn.FieldName] == null)//validimi per kolonat e detyrueshme
                    {
                        if (dataColumn.FieldName == "Pershkrimi" && (Request.QueryString["lloji"] == "Grup" || Request.QueryString["lloji"] == "Titull")) continue;
                        e.Errors[dataColumn] = "Vlera nuk mund te jete null.";
                    }
                }
            }
            if (hfRuaj.Value == "Ruaj" && e.NewValues["Kodi"] != null)
            {
                int lloji = 0;
                if (Request.QueryString["lloji"] == "Grup")
                    lloji = 1;
                else
                    if (Request.QueryString["lloji"] == "Titull")
                        lloji = 2;
                    else
                        lloji = 3;

                if (DbCore.DbArkaBanka.clsKonfigUrdherPagese.ekziston(e.NewValues["Kodi"].ToString(), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), lloji))
                {
                    switch (lloji)
                    {
                        case 1:
                            e.RowError = gabimEksitimi;
                            break;
                        case 2:
                            e.RowError = gabimEksitimiTitulli;
                            break;
                        case 3:
                            e.RowError = gabimEksitimiKapitull;
                            break;
                        default:
                            break;
                    }
                }
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

        protected void gvKonfigUrdherPagese_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {//kur fillon editimi te behet validimi
            if (hfRuaj.Value == "Ruaj")
                if (!gvKonfigUrdherPagese.IsNewRowEditing)
                {
                    gvKonfigUrdherPagese.DoRowValidation();
                }
        }

        protected void gvKonfigUrdherPagese_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            String id = e.Keys["Id"].ToString();
            e.Cancel = true;

            int lloji = 0;
            if (Request.QueryString["lloji"] == "Grup")
                lloji = 1;
            else if (Request.QueryString["lloji"] == "Titull")
                lloji = 2;
            else lloji = 3;
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.clsFunksione.GetKomponente(Page.Request));
            if (!tedrejtaInfo.DMod)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ju nuk keni te drejta per kete veprim!:Red");
                return;
            }
            DbCore.DbArkaBanka.clsKonfigUrdherPagese konf = new DbCore.DbArkaBanka.clsKonfigUrdherPagese() { Kodi = e.NewValues["Kodi"].ToString(), Pershkrimi = (e.NewValues["Pershkrimi"] == null) ? "" : e.NewValues["Pershkrimi"].ToString(), IdPerdoruesi = idperdoruesi, IdNdermarje = idNdermarrje, Lloji = lloji, IdStatusDok = 1, Id = int.Parse(id) };
            DbCore.clsMesazh m = konf.modifiko();
            if (m.Status)
            {
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, m.PershkrimMesazhi + ":Green");
            }
            else
            {
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, m.PershkrimMesazhi + ":Red");
            }
            gvKonfigUrdherPagese.CancelEdit();
            konfiguroVleraFillestare(idNdermarrje);

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
            MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;

            DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvKonfigUrdherPagese", "KonfigUrdherPagese.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                mesazh = filtra.fshi();
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvKonfigUrdherPagese", 1, "KonfigUrdherPagese.aspx");
                percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);

                if (mesazh.Status)
                {
                    ASPxMenu1.Items.FindByText("Ruaj").ClientVisible = false;
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                }
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";

                gvKonfigUrdherPagese.FilterExpression = String.Empty;
            }
        }
        /// <summary>
        /// sherben per te ruajtur filtrin e zgjedhur
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvKonfigUrdherPagese", "KonfigUrdherPagese.aspx", idNdermarrje);

            DbCore.DbAdmin.clsFiltraGrida filtri = new DbCore.DbAdmin.clsFiltraGrida() { FiltraKodi = cmbFiltra.Text, FiltraShenime = cmbFiltra.Text, FiltraUniversal = false, GridaKokaId = koka.IdGridaKoka, FiltraVlera = gvKonfigUrdherPagese.FilterExpression, IdPerdoruesi = idperdoruesi, IdNdermarje = idNdermarrje, IdStatusDok = 1 };
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("Kodi", gvKonfigUrdherPagese);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvKonfigUrdherPagese.GetSortedColumns();
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


            mesazh = filtri.ruaj();
            //mbushComboBoxFiltra(idNdermarrje);
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvKonfigUrdherPagese", 1, "KonfigUrdherPagese.aspx");
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);

            if (mesazh.Status)
            {
                ASPxMenu1.Items.FindByText("Ruaj").ClientVisible = false;
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            }
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            cmbFiltra.Text = "";

        }

        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            int a = gvKonfigUrdherPagese.FocusedRowIndex;
            gvKonfigUrdherPagese.Selection.SelectRow(a);
            List<object> rreshtat = gvKonfigUrdherPagese.GetSelectedFieldValues("Id");
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            foreach (int id in rreshtat)
            {
                DbCore.DbArkaBanka.clsKonfigUrdherPagese col = new DbCore.DbArkaBanka.clsKonfigUrdherPagese(id);

                if (col.kaVeprime())// me vone kur te lidhet
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ka veprime me kete njesi", pnlMesazhi);
                else
                {
                    DbCore.clsMesazh mesazh = col.fshi(col.Id, idperdoruesi);
                    if (mesazh.Status)
                    {
                        ASPxMenu1.Items.FindByText("Ruaj").ClientVisible = false;
                        clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["msgFshirjeMeSukses"], pnlMesazhi);
                    }
                    else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Fshirja perfundoi me gabime!", pnlMesazhi);
                }

                konfiguroVleraFillestare(idNdermarrje);

            }
            pnlGrida.Update();
        }

        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {

        }

        protected void gvKonfigUrdherPagese_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
        }

        protected void gvKonfigUrdherPagese_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {

        }

        protected void gvKonfigUrdherPagese_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
        }

        protected void gvKonfigUrdherPagese_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string[] arr = e.Parameters.Split(';');
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            if (arr.Length == 3)
            {
                if (arr[2] == "")
                    gvKonfigUrdherPagese.FilterExpression = "";
                else
                {

                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    //filtra.mbushFiltraGridaSipasFiltraKodi(arr[2], idNdermarrje);
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvKonfigUrdherPagese", "KonfigUrdherPagese.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        gvKonfigUrdherPagese.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvKonfigUrdherPagese);
                    }
                }
            }
            konfiguroGride(idNdermarrje);
        }

        protected void gvKonfigUrdherPagese_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gvKonfigUrdherPagese.PageIndex;
            e.Properties["cpPageRow"] = gvKonfigUrdherPagese.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gvKonfigUrdherPagese.VisibleRowCount;
        }
    }
}