using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Collections;
using System.Drawing;
using System.Data;
using PlatinumWeb.Templates;
using DbCore.DbRegjistrim;
using DbCore.IMBUtils.Types;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.DbShare;

namespace PlatinumWeb
{
    public partial class LupaAutomjeti : MyPageBase
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            int idPerdoruesi;
            int idGjuha;
            int idNdermarrje;
            int idViti;
            int idNdermarrjeVit;
            if (hfState.Count == 0)
            {
                if (!DbCore.mySessionObjects.isLogedIn(Session))
                {
                    DbCore.clsFunksione.logout(Session,true,"FaqePaautorizuar");
                }
                idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
                {
                    Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerdoruesi);
                }
                idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
                idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
                idNdermarrjeVit = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
                hfState.Set("idPerdoruesi", idPerdoruesi);
                hfState.Set("idGjuha", idGjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("idViti", idViti);
                hfState.Set("idNdermarrjeVit", idNdermarrjeVit);
                hfState.Set("Meme", DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
            }
            else
            {
                idPerdoruesi = (int)hfState["idPerdoruesi"];
                idGjuha = (int)hfState["idGjuha"];
                idNdermarrje = (int)hfState["idNdermarrje"];
                idViti = (int)hfState["idViti"];
                idNdermarrjeVit = (int)hfState["idNdermarrjeVit"];
            }
            string vleraQueryString = "";
            if (Request.QueryString["idKonfigAmbjente"] != null && Request.QueryString["idKonfigAmbjente"] != "")
                vleraQueryString = Request.QueryString["idKonfigAmbjente"].ToString();
            int idKonfigambjenti = DbCore.clsFunksione.getIdKonfigAmbLupa(vleraQueryString, idNdermarrje, "LupaAuto");
            if (!IsPostBack)
            {
                cmbKonfigurimi.Value = idKonfigambjenti.ToString();
                percaktoTemplateMenu(ASPxMenu1, idViti, idPerdoruesi, idNdermarrje);
                clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idNdermarrje, "gvLupaAutomjet", idKonfigambjenti, "LupaAutomjeti.aspx");
                GridUtil.AplikoFilterDefault(gvLupaAutomjet, idKonfigambjenti);
                hfState.Set("idKonfigambjenti", idKonfigambjenti);
                //DbCore.DbShare.clsKusht kushtkss = new DbCore.DbShare.clsKusht(idKonfigambjenti, "KSSH");
                //DbCore.DbShare.clsAlternativaKushti alterkss = new DbCore.DbShare.clsAlternativaKushti(kushtkss.Vlera);
                if (DbCore.DbShare.clsAlternativaKushti.getAlternativa(idKonfigambjenti, "KSSH") == "Po")
                    hfState.Add("KSSH", true);
                else hfState.Add("KSSH", false);
                if (DbCore.DbShare.clsAlternativaKushti.getAlternativa(idKonfigambjenti, "ES") == "Po")
                    hfState.Add("ES", true);
                else hfState.Add("ES", false);
                mbushPopUpListeAutomjeteshNgaDB(idNdermarrje);
                konfiguroPopupGride(idNdermarrje, idPerdoruesi);
                GridUtil.percaktoVisibleColumnsSipasKonfigurimit(gvLupaAutomjet, "gvLupaAutomjet", "LupaAutomjeti.aspx", idKonfigambjenti, true, DbCore.mySessionObjects.ktheGjuhe(Session));
                hfState.Set("btnShtoAutomjet", DbCore.IMBUtils.Messages.MessagesResource.Messages["btnShtoAutomjet"]);
            }
            else
            {
                if (!IsCallback || (IsCallback && Request["__CALLBACKID"].ToString().Contains("ASPxMenu1")))
                    percaktoTemplateMenu(ASPxMenu1, idViti, idPerdoruesi, idNdermarrje);

                if (!IsCallback || (IsCallback && Request["__CALLBACKID"].ToString().Contains("gvLupaAutomjet")))
                {
                    idKonfigambjenti = (int)hfState["idKonfigambjenti"];
                    mbushPopUpListeAutomjeteshNgaSession(idNdermarrje);
                    konfiguroPopupGride(idNdermarrje, idPerdoruesi);
                }
            }
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            GridUtil.KonfiguroGrideListeMadhePopupiPaTheme(gvLupaAutomjet, "IdAutomjeti", (bool)hfState["KSSH"], (bool)hfState["ES"]);
        }

        private void mbushPopUpListeAutomjeteshNgaDB(int idNdermarrje)
        {//mbush griden e popupit me te dhena
            DataTable dt = new DataTable();
            if (!String.IsNullOrEmpty(Request.QueryString["klienti"]))
            {
                int idKlienti = int.Parse(Request.QueryString["klienti"]);
                dt = DbCore.DbInventari.colAutomjete.merrAutomjetetSipasNdermarrjesDheKlientit(idNdermarrje, idKlienti);
            }
            else
                dt = DbCore.DbInventari.colAutomjete.merrAutomjetetSipasNdermarrjes(idNdermarrje);
            DbCore.mySessionObjects.ruajGrideNeSessionLupa(Session, dt);
            gvLupaAutomjet.DataSource = dt;
            gvLupaAutomjet.DataBind();
            dt.Dispose();
        }

        private void mbushPopUpListeAutomjeteshNgaSession(int idNdermarrje)
        {
            Object tmpObject;
            DbCore.mySessionObjects.merrGrideNgaSessioniLupa(Session, out tmpObject);
            if (tmpObject == null)
                mbushPopUpListeAutomjeteshNgaDB(idNdermarrje);
            else
            {
                gvLupaAutomjet.DataSource = tmpObject;
                gvLupaAutomjet.DataBind();
            }
        }

        private void konfiguroPopupGride(int idNdermarrje, int idPerdorues)
        {//konfiguron popupgriden
            shtoModelAutomjeti(idNdermarrje);
            shtoKlient(idNdermarrje, idPerdorues);
            //gvLupaAutomjet.Columns["#"].VisibleIndex = 0;
        }

        /// <summary>
        /// perdoret per te shfaqur kodet e modelit te automjetit ne vend te id si dhe filtri i modelit te auto te shfaqet ne forme komboje
        /// </summary>
        /// :  <see cref="DbCore.DbInventari.clsDatabaseInventari.merrGjitheKodifikimetArtikulliSipasNdermarrjesJoPrind(idndermarje)"/>
        /// <param name="idNdermarrje"></param>
        private void shtoModelAutomjeti(int idNdermarrje)
        {
            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            if (typeof(GridViewDataComboBoxColumn) != gvLupaAutomjet.Columns["ModelAutomjeti"].GetType())
            {
                gvLupaAutomjet.Columns.Remove(gvLupaAutomjet.Columns["ModelAutomjeti"]);
                gvLupaAutomjet.Columns.Add(colnew);
                DbCore.DbInventari.colModeleAutomjetesh modelet = new DbCore.DbInventari.colModeleAutomjetesh();
                modelet.Add(new DbCore.DbInventari.clsModelAutomjeti(0, "", "", 0, 0, 0, 0));
                modelet.mbushModeleAutomjeteshSipasNdermarrjes(idNdermarrje);
                colnew.PropertiesComboBox.DataSource = modelet;
                colnew.PropertiesComboBox.TextField = "KodModelAutomjeti";
                colnew.PropertiesComboBox.ValueField = "IdModelAutomjeti";
                colnew.FieldName = "ModelAutomjeti";
                colnew.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                DbCore.mySessionObjects.ruajDsComboGrideNeSession(Session, modelet, "colModelet");
            }
            else
            {
                colnew = (GridViewDataComboBoxColumn)gvLupaAutomjet.Columns["ModelAutomjeti"];
                if (colnew.PropertiesComboBox.Items.Count == 0)
                {
                    colnew.PropertiesComboBox.DataSource = DbCore.mySessionObjects.merrDsComboGrideNeSession(Session, "colModelet");
                }
            }
        }

        /// <summary>
        /// perdoret per te shfaqur kodet e klienteve ne vend te id si dhe filtri i klientit te shfaqet ne forme komboje
        /// </summary>
        /// :  <see cref="DbCore.DbInventari.clsDatabaseInventari.merrGjitheKodifikimetArtikulliSipasNdermarrjesJoPrind(idndermarje)"/>
        /// <param name="idNdermarrje"></param>
        private void shtoKlient(int idNdermarrje, int idPerdorues)
        {
            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            if (typeof(GridViewDataComboBoxColumn) != gvLupaAutomjet.Columns["IdKlienti"].GetType())
            {
                gvLupaAutomjet.Columns.Remove(gvLupaAutomjet.Columns["IdKlienti"]);
                gvLupaAutomjet.Columns.Add(colnew);
                DbCore.DbKontabiliteti.colKlienteFurnitore klientet = new DbCore.DbKontabiliteti.colKlienteFurnitore();
                klientet.Add(new DbCore.DbKontabiliteti.clsKlientFurnitor());
                klientet.mbushKlienteFurnitoreNdermarrjes(idNdermarrje);
                colnew.PropertiesComboBox.DataSource = klientet;
                colnew.PropertiesComboBox.TextField = "KodKlientFurnitor";
                colnew.PropertiesComboBox.ValueField = "IdKlientFurnitor";
                colnew.FieldName = "IdKlienti";
                colnew.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                DbCore.mySessionObjects.ruajDsComboGrideNeSession(Session, klientet, "colKlient");
            }
            else
            {
                colnew = (GridViewDataComboBoxColumn)gvLupaAutomjet.Columns["IdKlienti"];
                if (colnew.PropertiesComboBox.Items.Count == 0)
                {
                    colnew.PropertiesComboBox.DataSource = DbCore.mySessionObjects.merrDsComboGrideNeSession(Session, "colKlient");
                }
            }
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
            int idGjuha = (int)hfState["idGjuha"];
            bool meme = (bool)hfState["Meme"];
            clsToolbarConfig.percaktoTemplateMenu(idGjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1, "LupaAutomjeti.aspx", this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, true, false, false, meme);
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            aSPxMenu1.Items.FindByName("Anullo").Text = rm.GetString("MenuItemMbyll", ci);
        }

        private int merrKonfiguriminDefaultTeLupes(int idNdermarrje, int idNivel)
        {
            //do marr konfigurimin default per kete nivel regjistrimi i cili eshte i vetem per nje ndermarrje
            //DbCore.DbShare.clsKonfigurimAmbjenti ambj = new DbCore.DbShare.clsKonfigurimAmbjenti(idNdermarrje, idNivel);
            //return ambj.IdKonfigAmbjente;
            return DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimi(idNdermarrje, idNivel);
        }

        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje
            int idNdermarrje = (int)hfState["idNdermarrje"];
            int idPerdoruesi = (int)hfState["idPerdoruesi"];
            int idViti = (int)hfState["idViti"];
            clsMenuInfo.ShtoMenuItemInfo(this, MenuInfo);
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            DbCore.DbAdmin.clsFiltraGrida filtri = new DbCore.DbAdmin.clsFiltraGrida();
            filtri.FiltraKodi = cmbFiltra.Text;
            filtri.FiltraShenime = cmbFiltra.Text;
            filtri.FiltraUniversal = false;// Universal_ASPxCheckBox.Checked;
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaAutomjet", "LupaAutomjeti.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gvLupaAutomjet.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("IdAutomjeti", gvLupaAutomjet);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvLupaAutomjet.GetSortedColumns();
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
            //    filtri.KoloneRenditje = "IdAutomjeti";
            //    filtri.DrejtimRenditje = true;
            //}
            filtri.IdPerdoruesi = idPerdoruesi;
            filtri.IdNdermarje = idNdermarrje;
            filtri.IdStatusDok = 1;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            mesazh = filtri.ruaj();
            percaktoTemplateMenu(ASPxMenu1, idViti, idPerdoruesi, idNdermarrje);
            //mbushComboBoxFiltra(idNdermarrje);
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaAutomjet", Convert.ToInt32(cmbKonfigurimi.Value), "LupaAutomjeti.aspx");
            //percaktoTemplateMenu(ASPxMenu1);
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
            int idNdermarrje = (int)hfState["idNdermarrje"];
            DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaAutomjet", "LupaAutomjeti.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                int idPerdoruesi = (int)hfState["idPerdoruesi"];
                int idViti = (int)hfState["idViti"];
                filtra.IdPerdoruesi = idPerdoruesi;
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();
                percaktoTemplateMenu(ASPxMenu1, idViti, idPerdoruesi, idNdermarrje);
                //mbushComboBoxFiltra(idNdermarrje);
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaAutomjet", Convert.ToInt32(cmbKonfigurimi.Value), "LupaAutomjeti.aspx");
                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                gvLupaAutomjet.FilterExpression = String.Empty;
            }
        }

        protected void gvLupaAutomjet_DataBound(object sender, EventArgs e)
        {
            //perdoret per ti vene disa atribute grides
            //behet per te afishuar rreshtin qe do sherbej per filtrim
            gvLupaAutomjet.Settings.ShowFilterRow = true;
            gvLupaAutomjet.KeyFieldName = "IdAutomjeti";
            gvLupaAutomjet.SettingsBehavior.AllowSelectByRowClick = true;
        }

        protected void gvLupaAutomjet_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            gvLupaAutomjet.Selection.UnselectAll();
        }

        protected void gvLupaAutomjet_CustomJSProperties(object sender, DevExpress.Web.ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gvLupaAutomjet.PageIndex;
            e.Properties["cpPageRow"] = gvLupaAutomjet.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gvLupaAutomjet.VisibleRowCount;
        }

        protected void gvLupaAutomjet_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            int idNdermarrje = (int)hfState["idNdermarrje"];
            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                if (arr[2] == "")
                    gvLupaAutomjet.FilterExpression = "";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaAutomjet", "LupaAutomjeti.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], idNdermarrje, koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        gvLupaAutomjet.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvLupaAutomjet);
                    }
                }
            }
            gvLupaAutomjet.Selection.UnselectAll();
        }

        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            int idPerdoruesi = (int)hfState["idPerdoruesi"];
            int idNdermarrje = (int)hfState["idNdermarrje"];
            int idViti = (int)hfState["idViti"];
            percaktoTemplateMenu(ASPxMenu1, idViti, idPerdoruesi, idNdermarrje);
        }

        protected void gvLupaAutomjet_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if (e.Column.FieldName == "ModelAutomjeti" || e.Column.FieldName == "IdKlienti")
                if (Converter.ConvertToInt(e.Value)==0)
                {
                    e.Criteria = null;
                }
        }
    }
}