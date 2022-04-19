using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using DevExpress.Web;
using DbCore.IMBUtils.Messages;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class ShopsHierarkiLeaveReason : MyPageBase
    {
        DbCore.DbAdmin.clsShopsHierarkiLeaveReason leaveReason;

        private int idPerdoruesi;
        private string emerGride = "gvLupaShopsHierarkiLeaveReason";
        private string emerKomponente = "LupaShopsHierarkiLeaveReason.aspx";
        protected void Page_PreInit(object sender, EventArgs e)
        {
            DbCore.clsFunksione.percaktoThemeAmbjenteDheJQuery(Page, DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                Response.Redirect("login.aspx");
            }
            int idPerd = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerd);
            }
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);

            clsToolbarConfig.mbushComboBoxFiltra(MessagesResource.Messages.IdGjuha, idNdermarrje, emerGride, 1, emerKomponente);
            if (!IsPostBack)
            {   
                idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, ":");
                if (Request.QueryString["ruaj"] == "ok")
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["mesazhRuajtjeMeSukses"], pnlMesazhi);

                mbushGrideNgaDb();
                konfiguroGride();
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), emerKomponente);
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
            }
            else
            {
                mbushNgaSesioni();
                konfiguroGride();
            }
            DbCore.mySessionObjects.ruajMesazhNeSesion(Session, ":");
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
            clsToolbarConfig.percaktoTemplateMenu(MessagesResource.Messages.IdGjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1, emerKomponente, this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, hfRuaj.Value == "Ruaj" ? true : false, true, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
           // aSPxMenu1.Items.FindByName("Anullo").Text = "Mbyll";
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

        private void mbushGrideNgaDb()
        {
            DbCore.DbAdmin.colShopsHierarkiLeaveReason col = new DbCore.DbAdmin.colShopsHierarkiLeaveReason();
            DbCore.mySessionObjects.ruajGrideNeSession(emerKomponente, Session, col, true);
            gvLupaShopsHierarkiLeaveReason.DataSource = col;
            gvLupaShopsHierarkiLeaveReason.DataBind();
        }

        private void mbushNgaSesioni()
        {
            object tmpObject;
            bool sukses = DbCore.mySessionObjects.merrGrideNgaSessioni(emerKomponente, Session, out tmpObject);
            if (!sukses)
                mbushGrideNgaDb();
            else
            {
                gvLupaShopsHierarkiLeaveReason.DataSource = tmpObject;
                gvLupaShopsHierarkiLeaveReason.DataBind();
            }
        }

        private void konfiguroGride()
        {
            //DbCore.clsFunksione funk = new DbCore.clsFunksione();
            GridUtil.percaktoVisibleColumnsMeWidth(MessagesResource.Messages.IdGjuha, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), gvLupaShopsHierarkiLeaveReason, emerGride, emerKomponente);
            GridUtil.konfiguroGrideListeEvogelPaTheme(gvLupaShopsHierarkiLeaveReason, "IdLeaveReason");
        }

        protected void gvLupaShopsHierarkiLeaveReason_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            e.Values.Clear();
            e.AddValue("(Te gjithe)", string.Empty, "true");
        }


        protected void gvLupaShopsHierarkiLeaveReason_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            leaveReason = new DbCore.DbAdmin.clsShopsHierarkiLeaveReason();
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), emerKomponente);
            if (!tedrejtaInfo.DShtim)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ju nuk keni te drejta per kete veprim!:Red");
                return;
            }
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            if (e.NewValues["Aktiv"] != null)
                leaveReason.Aktiv = Convert.ToBoolean(e.NewValues["Aktiv"].ToString());
            else leaveReason.Aktiv = false;
            leaveReason.PershkrimLeaveReason = e.NewValues["PershkrimLeaveReason"].ToString();
            leaveReason.IdKrijuesi = idPerdoruesi;
            leaveReason.IdPerdorues = idPerdoruesi;
            leaveReason.DtKrijimi = DateTime.Today;
            e.Cancel = true;
            gvLupaShopsHierarkiLeaveReason.CancelEdit();
            mesazh = leaveReason.ruajShopsHierarkiLeaveReason();
            if (!mesazh.Status)
            {
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ruajtja perfundoi me gabime!:Red");
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ruajtja perfundoi me gabime!", pnlMesazhi);
            }
            else
            {
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, MessagesResource.Messages["msgRuajtjeMeSuksesGreen"]);
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["mesazhRuajtjeMeSukses"], pnlMesazhi);
            }
            mbushGrideNgaDb();
        }

        protected void gvLupaShopsHierarkiLeaveReason_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {//validimi ne jane plotesuar gjithe fushat e detyruara
            foreach (GridViewColumn column in gvLupaShopsHierarkiLeaveReason.Columns)
            {
                if (column.Visible == true)
                {
                    GridViewDataColumn dataColumn = column as GridViewDataColumn;
                    if (dataColumn == null) continue;
                    if (dataColumn.FieldName == "Aktiv") continue;
                    if (e.NewValues[dataColumn.FieldName] == null)//validimi per kolonat e detyrueshme
                        e.Errors[dataColumn] = "Vlera e fushes " + dataColumn.FieldName + " nuk mund te jete null!";
                }
            }
            if (hfRuaj.Value == "Ruaj")
            {
                if (e.NewValues["PershkrimLeaveReason"] != null)
                {
                    leaveReason = new DbCore.DbAdmin.clsShopsHierarkiLeaveReason();

                    leaveReason.PershkrimLeaveReason = e.NewValues["PershkrimLeaveReason"].ToString();
                    if (DbCore.DbAdmin.clsShopsHierarkiLeaveReason.ekzistonLeaveReasonMeKetePershkrim(leaveReason.PershkrimLeaveReason))
                    {

                        e.RowError = "ekziston nje arsye me kete pershkrim! Ju lutem zgjidhni nje pershkrim tjeter";
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

        protected void gvLupaShopsHierarkiLeaveReason_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {//kur fillon editimi te behet validimi
            if (hfRuaj.Value == "Ruaj")
                if (!gvLupaShopsHierarkiLeaveReason.IsNewRowEditing)
                {
                    gvLupaShopsHierarkiLeaveReason.DoRowValidation();
                }
        }

        protected void gvLupaShopsHierarkiLeaveReason_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            e.Cancel = true;
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), emerKomponente);
            if (!tedrejtaInfo.DMod)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ju nuk keni te drejta per kete veprim!:Red");
                return;
            }
            DbCore.DbAdmin.clsShopsHierarkiLeaveReason leaveReason = new DbCore.DbAdmin.clsShopsHierarkiLeaveReason();
            string IdLeaveReason = e.Keys["IdLeaveReason"].ToString();
            leaveReason.IdLeaveReason = int.Parse(IdLeaveReason);
            leaveReason.IdKrijuesi = idPerdoruesi;
            if (e.NewValues["Aktiv"] != null)
                leaveReason.Aktiv = Convert.ToBoolean(e.NewValues["Aktiv"].ToString());
            else
                leaveReason.Aktiv = false;
            leaveReason.PershkrimLeaveReason = e.NewValues["PershkrimLeaveReason"].ToString();
            leaveReason.IdPerdorues = idPerdoruesi;
            leaveReason.DtModifikimi = DateTime.Today;
            DbCore.clsMesazh m = leaveReason.modifikoShopsHierarkiLeaveReason();
            if (m.Status)
            {
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, m.PershkrimMesazhi + ":Green");
            }
            else
            {
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, m.PershkrimMesazhi + ":Red");
            }
            gvLupaShopsHierarkiLeaveReason.CancelEdit();
            mbushGrideNgaDb();
            konfiguroGride();
        }

        public void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemExport");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(MessagesResource.Messages.IdGjuha, emerGride, emerKomponente, idNdermarrje);
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                mesazh = filtra.fshi();
                clsToolbarConfig.mbushComboBoxFiltra(MessagesResource.Messages.IdGjuha, idNdermarrje, emerGride, 1, emerKomponente);
                percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);

                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                gvLupaShopsHierarkiLeaveReason.FilterExpression = String.Empty;
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
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemExport");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            DbCore.DbAdmin.clsFiltraGrida filtri = new DbCore.DbAdmin.clsFiltraGrida();
            filtri.FiltraKodi = cmbFiltra.Text;
            filtri.FiltraShenime = cmbFiltra.Text;
            filtri.FiltraUniversal = false;// Universal_ASPxCheckBox.Checked;
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(MessagesResource.Messages.IdGjuha, emerGride, emerKomponente, idNdermarrje);
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gvLupaShopsHierarkiLeaveReason.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("IdLeaveReason", gvLupaShopsHierarkiLeaveReason);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvLupaShopsHierarkiLeaveReason.GetSortedColumns();
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
            //    filtri.KoloneRenditje = "IdLeaveReason";
            //    filtri.DrejtimRenditje = true;
            //}

            filtri.IdPerdoruesi = idPerdoruesi;
            filtri.IdNdermarje = idNdermarrje;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            filtri.IdStatusDok = 1;

            mesazh = filtri.ruaj();
            //mbushComboBoxFiltra();
            clsToolbarConfig.mbushComboBoxFiltra(MessagesResource.Messages.IdGjuha, idNdermarrje, emerGride, 1, emerKomponente);
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);

            if (mesazh.Status == true)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            cmbFiltra.Text = "";

        }



        protected void gvLupaShopsHierarkiLeaveReason_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                if (arr[2] == "")
                    gvLupaShopsHierarkiLeaveReason.FilterExpression = "";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(MessagesResource.Messages.IdGjuha, emerGride, emerKomponente, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        gvLupaShopsHierarkiLeaveReason.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvLupaShopsHierarkiLeaveReason);
                    }
                }
            }
            konfiguroGride();
        }

        protected void gvLupaShopsHierarkiLeaveReason_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gvLupaShopsHierarkiLeaveReason.PageIndex;
            e.Properties["cpPageRow"] = gvLupaShopsHierarkiLeaveReason.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gvLupaShopsHierarkiLeaveReason.VisibleRowCount;
        }

        protected void gvLupaShopsHierarkiLeaveReason_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            e.NewValues["Aktiv"] = true;
        }
    }
}