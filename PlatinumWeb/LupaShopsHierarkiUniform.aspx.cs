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
    public partial class ShopsHierarkiUniform : MyPageBase
    {
        DbCore.DbAdmin.clsShopsHierarkiUniform uniform;
        private int idPerdoruesi;
        private string emerGride = "gvLupaShopsHierarkiUniform";
        private string emerKomponente = "LupaShopsHierarkiUniform.aspx";
        
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

            clsToolbarConfig.mbushComboBoxFiltra(MessagesResource.Messages.IdGjuha, idNdermarrje,  emerGride,1, emerKomponente);
      
            if (!IsPostBack)
            {    //Session.Add("mesazh", ":Green");
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
            DbCore.DbAdmin.colShopsHierarkiUniform col = new DbCore.DbAdmin.colShopsHierarkiUniform();
            DbCore.mySessionObjects.ruajGrideNeSession(emerKomponente, Session, col, true);
            gvLupaShopsHierarkiUniform.DataSource = col;
            gvLupaShopsHierarkiUniform.DataBind();
        }

        private void mbushNgaSesioni()
        {
            object tmpObject;
            bool sukses = DbCore.mySessionObjects.merrGrideNgaSessioni(emerKomponente, Session, out tmpObject);
            if (!sukses)
                mbushGrideNgaDb();
            else
            {
                gvLupaShopsHierarkiUniform.DataSource = tmpObject;
                gvLupaShopsHierarkiUniform.DataBind();
            }
        }

        private void konfiguroGride()
        {
            GridUtil.percaktoVisibleColumnsMeWidth(MessagesResource.Messages.IdGjuha, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), gvLupaShopsHierarkiUniform, emerGride, emerKomponente);
            GridUtil.konfiguroGrideListeEvogelPaTheme(gvLupaShopsHierarkiUniform, "IdUniform");
      
            
        }

        protected void gvLupaShopsHierarkiUniform_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            e.Values.Clear();
            e.AddValue("(Te gjithe)", string.Empty, "true");
        }


        protected void gvLupaShopsHierarkiUniform_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            uniform = new DbCore.DbAdmin.clsShopsHierarkiUniform();
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
                uniform.Aktiv = Convert.ToBoolean(e.NewValues["Aktiv"].ToString());
            else uniform.Aktiv = false;
            uniform.PershkrimUniform = e.NewValues["PershkrimUniform"].ToString();
            uniform.IdKrijuesi = idPerdoruesi;
            uniform.IdPerdorues = idPerdoruesi;
            uniform.DtKrijimi = DateTime.Today;
            e.Cancel = true;
            gvLupaShopsHierarkiUniform.CancelEdit();
            mesazh = uniform.ruajShopsHierarkiUniform();
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

        protected void gvLupaShopsHierarkiUniform_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {//validimi ne jane plotesuar gjithe fushat e detyruara
            foreach (GridViewColumn column in gvLupaShopsHierarkiUniform.Columns)
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
                if (e.NewValues["PershkrimUniform"] != null)
                {
                    uniform = new DbCore.DbAdmin.clsShopsHierarkiUniform();

                    uniform.PershkrimUniform = e.NewValues["PershkrimUniform"].ToString();
                    if (DbCore.DbAdmin.clsShopsHierarkiUniform.ekzistonUniformMeKetePershkrim(uniform.PershkrimUniform))
                    {

                        e.RowError = "ekziston nje uniforme me kete pershkrim! Ju lutem zgjidhni nje pershkrim tjeter";
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

        protected void gvLupaShopsHierarkiUniform_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {//kur fillon editimi te behet validimi
            if (hfRuaj.Value == "Ruaj")
                if (!gvLupaShopsHierarkiUniform.IsNewRowEditing)
                {
                    gvLupaShopsHierarkiUniform.DoRowValidation();
                }
        }

        protected void gvLupaShopsHierarkiUniform_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
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
            DbCore.DbAdmin.clsShopsHierarkiUniform uniform = new DbCore.DbAdmin.clsShopsHierarkiUniform();
            string idUniform = e.Keys["IdUniform"].ToString();
            uniform.IdUniform = int.Parse(idUniform);
            uniform.IdKrijuesi = idPerdoruesi;
            if (e.NewValues["Aktiv"] != null)
                uniform.Aktiv = Convert.ToBoolean(e.NewValues["Aktiv"].ToString());
            else
                uniform.Aktiv = false;
            uniform.PershkrimUniform = e.NewValues["PershkrimUniform"].ToString();
            uniform.IdPerdorues = idPerdoruesi;
            uniform.DtModifikimi = DateTime.Today;
            DbCore.clsMesazh m = uniform.modifikoShopsHierarkiUniform();
            if (m.Status)
            {
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, m.PershkrimMesazhi + ":Green");
            }
            else
            {
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, m.PershkrimMesazhi + ":Red");
            }
            gvLupaShopsHierarkiUniform.CancelEdit();
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
                clsToolbarConfig.mbushComboBoxFiltra(MessagesResource.Messages.IdGjuha, idNdermarrje,  emerGride,1, emerKomponente);
                percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);

                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                gvLupaShopsHierarkiUniform.FilterExpression = String.Empty;
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
            filtri.FiltraVlera = gvLupaShopsHierarkiUniform.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("IdUniform", gvLupaShopsHierarkiUniform);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvLupaShopsHierarkiUniform.GetSortedColumns();
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
            //    filtri.KoloneRenditje = "IdUnifom";
            //    filtri.DrejtimRenditje = true;
            //}

            filtri.IdPerdoruesi = idPerdoruesi;
            filtri.IdNdermarje = idNdermarrje;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            filtri.IdStatusDok = 1;

            mesazh = filtri.ruaj();
            //mbushComboBoxFiltra();
            clsToolbarConfig.mbushComboBoxFiltra(MessagesResource.Messages.IdGjuha, idNdermarrje,  emerGride,1, emerKomponente);
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);

            if (mesazh.Status == true)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            cmbFiltra.Text = "";

        }



        protected void gvLupaShopsHierarkiUniform_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                if (arr[2] == "")
                    gvLupaShopsHierarkiUniform.FilterExpression = "";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(MessagesResource.Messages.IdGjuha, emerGride, emerKomponente, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        gvLupaShopsHierarkiUniform.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvLupaShopsHierarkiUniform);
                    }
                }
            }
            konfiguroGride();
        }

        protected void gvLupaShopsHierarkiUniform_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gvLupaShopsHierarkiUniform.PageIndex;
            e.Properties["cpPageRow"] = gvLupaShopsHierarkiUniform.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gvLupaShopsHierarkiUniform.VisibleRowCount;
        }

        protected void gvLupaShopsHierarkiUniform_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            e.NewValues["Aktiv"] = true;
        }
    }
}
