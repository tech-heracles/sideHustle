using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Collections;
using System.Drawing;
using DbCore.DbRegjistrim;
using System.Resources;
using System.Globalization;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.DbShare;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{
    public partial class CRMLupaAnketa : MyPageBase
    {

        //private DbCore.DbAdmin.clsDatabaseAdmin dbAdmin;
        private int idKonfigambjenti;
        protected void Page_Load(object sender, EventArgs e)
        {
            String array = Request.QueryString["array"];

            string vleraQueryString = "";
            if (Request.QueryString["idKonfigAmbjente"] != null && Request.QueryString["idKonfigAmbjente"] != "")
                vleraQueryString = Request.QueryString["idKonfigAmbjente"].ToString();
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);

            int idNivel = clsNivelRegjistrimi.ktheIdNivelRegjistrimiSipasKodi("LANK", idNdermarrje);
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);
            if (vleraQueryString != "")
            {
                //ketu me intereson id e nivelit
                //ne rastin kur kemi disa konfigurime te lupes per kontrollin i cili gjeneroi thirrjen e kesaj lupe
                //duhet te gjejme cili nga konfigurimet e ka idNivel sa niveli i artikujve
                //i kontrollojme me radhe te gjitha konfigurimet qe i jane kaluar ne query string
                string[] idte = vleraQueryString.Split('-');
                if (idte.Length > 1)
                {
                    DbCore.DbShare.clsKonfigurimAmbjenti konfigLupa = new DbCore.DbShare.clsKonfigurimAmbjenti();
                    for (int i = 0; i < idte.Length - 1; i++)
                    {
                        konfigLupa.IdKonfigAmbjente = Convert.ToInt32(idte[i]);
                        konfigLupa = konfigLupa.merrSipasId();
                        if (konfigLupa.IdNivel == idNivel)
                        {
                            idKonfigambjenti = Convert.ToInt32(idte[i]);
                        }
                    }
                }
                else
                    if (idte.Length == 1)
                    {
                        idKonfigambjenti = Convert.ToInt32(Request.QueryString["idKonfigAmbjente"].ToString());
                        if (idKonfigambjenti == 0)
                            merrKonfiguriminDefaultTeLupes(idNivel);
                    }
                    else
                        merrKonfiguriminDefaultTeLupes(idNivel);

            }
            else
            {
                merrKonfiguriminDefaultTeLupes(idNivel);
            }
            mbushPopUpListeAutorizimesh(idNdermarrje);

            bool kerkosaposhkruar = true;
            if (DbCore.DbShare.clsAlternativaKushti.getAlternativa(idKonfigambjenti, "KSSH") == "Po")
                kerkosaposhkruar = true;
            else kerkosaposhkruar = false;
            bool endlessScroll = false;
            if (clsAlternativaKushti.getAlternativa(idKonfigambjenti, "ES") == "Po")
                endlessScroll = true;
 
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaAnketa", idKonfigambjenti, "CRMLupaAnketa.aspx");
            if (!IsPostBack)
            {
                GridUtil.AplikoFilterDefault(gvLupaAnketa, idKonfigambjenti);
                konfiguroPopupGride(true, kerkosaposhkruar, endlessScroll);
            }
            else
            {
                konfiguroPopupGride(false, kerkosaposhkruar, endlessScroll);
            }

        }

        private void merrKonfiguriminDefaultTeLupes(int idNivel)
        {
            idKonfigambjenti = DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimi(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), idNivel);
        }

        private void mbushPopUpListeAutorizimesh(int idNdermarrje)
        {//mbush griden e popupit me te dhena

            DbCore.DbCRM.colKokaAnketa colAnketa = new DbCore.DbCRM.colKokaAnketa();
            colAnketa.merrKokaAnketeSipasNdermarrjesAktive(idNdermarrje);
            gvLupaAnketa.KeyFieldName = "IdKokaAnketa";
            gvLupaAnketa.DataSource = colAnketa;
            gvLupaAnketa.DataBind();

            if (Request.QueryString["idklienti"] != null && Request.QueryString["idklienti"] != "")
            {
                string idklienti = Request.QueryString["idklienti"];
                DbCore.DbCRM.colKlientAnketa col = new DbCore.DbCRM.colKlientAnketa(int.Parse(idklienti));
                for (int i = 0; i < col.Count; i++)
                {
                    DbCore.DbCRM.clsKokaAnketa autorizim = colAnketa.Find(x => x.IdKokaAnketa == col[i].IdKokaAnketa);
                    if (autorizim != null)
                    {
                        if (!IsPostBack)
                            gvLupaAnketa.Selection.SelectRowByKey(col[i].IdKokaAnketa);
                        colAnketa.Remove(autorizim);
                        colAnketa.Insert(0, autorizim);
                    }
                }
                gvLupaAnketa.DataSource = colAnketa;
                gvLupaAnketa.DataBind();
            }

        }
        private void konfiguroPopupGride(bool visibleIndex, bool kerkosaposhkruar, bool endlessScroll)
        {//konfiguron popupgriden
            GridUtil.percaktoVisibleColumnsSipasKonfigurimit(gvLupaAnketa, "gvLupaAnketa", "CRMLupaAnketa.aspx", idKonfigambjenti, visibleIndex, DbCore.mySessionObjects.ktheGjuhe(Session));
            //funk.konfiguroGrideListeMadhe(gvLupaAnketa, "IdAutorizimKoka");
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            GridUtil.KonfiguroGrideListeMadhePopupiPaTheme(gvLupaAnketa, "IdKokaAnketa", kerkosaposhkruar, endlessScroll);
            this.gvLupaAnketa.Columns["#"].VisibleIndex = 0;
        }

        protected void gvLupaAnketa_DataBound(object sender, EventArgs e)
        {
            if (this.gvLupaAnketa.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per 
                //perzgjidh
                GridViewCommandColumn check = new GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true; check.Width = Unit.Percentage(2);
                //   check.SetColVisibleIndex(0);
                //behet per te afishuar rreshtin qe do sherbej per filtrim
                gvLupaAnketa.Settings.ShowFilterRow = true;
                gvLupaAnketa.Columns.Add(check);

                gvLupaAnketa.KeyFieldName = "IdKokaAnketa";
                gvLupaAnketa.SettingsBehavior.AllowSelectByRowClick = true;

            }



        }

        protected void gvLupaAnketa_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {

        }

        protected void gvLupaAnketa_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {

        }

        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        }

        protected void gvLupaAnketa_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string idkomponente = "";
            string kodkonfigurimi = "";
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                kodkonfigurimi = arr[1];
                idkomponente = arr[0];
                if (arr[2] == "")
                    gvLupaAnketa.FilterExpression = "";
                else
                {
                    GridUtil.AplikoFilter(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, gvLupaAnketa, arr[2], "gvLupaAnketa", "CRMLupaAnketa.aspx", 1);
                    //DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    //DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhePerdoruesi(Session), "gvLupaAnketa", "CRMLupaAnketa.aspx", idNdermarrje);
                    //filtra.mbushFilterPerGrideSipasKodit(arr[2], idNdermarrje, koka.IdGridaKoka);
                    //if (filtra.FiltraKodi != null)
                    //{
                    //    gvLupaAnketa.FilterExpression = filtra.FiltraVlera;
                    //    if (filtra.DrejtimRenditje == true)
                    //        gvLupaAnketa.SortBy(gvLupaAnketa.Columns[filtra.KoloneRenditje], DevExpress.Data.ColumnSortOrder.Ascending);
                    //    else
                    //        gvLupaAnketa.SortBy(gvLupaAnketa.Columns[filtra.KoloneRenditje], DevExpress.Data.ColumnSortOrder.Descending);
                    //}
                }
            }
            else if (arr.Length == 2)//nese eshte zgjedhur nje konfigurim tek combo e konfigurimeve
            {
                kodkonfigurimi = arr[1];
                idkomponente = arr[0];
            }
            else
            {
                idkomponente = e.Parameters;
            }
            gvLupaAnketa.Selection.UnselectAll();
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
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, "CRMLupaAnketa.aspx", this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session), true);
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (aSPxMenu1.Items.FindByName("Anullo") != null)
                aSPxMenu1.Items.FindByName("Anullo").Text = rm.GetString("MenuItemMbyll", ci);
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
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaAnketa", "CRMLupaAnketa.aspx", idNdermarrje);
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gvLupaAnketa.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("IdKokaAnketa", gvLupaAnketa);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvLupaAnketa.GetSortedColumns();
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
            //    filtri.KoloneRenditje = "IdKokaAnketa";
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
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaAnketa", 1, "CRMLupaAnketa.aspx");
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
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaAnketa", "CRMLupaAnketa.aspx", idNdermarrje);
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();
                //mbushComboBoxFiltra(idNdermarrje);
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaAnketa", 1, "CRMLupaAnketa.aspx");
                percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);
                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                gvLupaAnketa.FilterExpression = String.Empty;
            }
        }

        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {
            if (e.Item.Name == "OK")
                ruajAnketa();
        }

        private void ruajAnketa()
        {
            if (Request.QueryString["idklienti"] != null && Request.QueryString["idklienti"] != "")
            {
                List<object> rreshtat = gvLupaAnketa.GetSelectedFieldValues("IdKokaAnketa");
                DbCore.clsMesazh mesazh = DbCore.DbCRM.colKlientAnketa.ruajAnketa(int.Parse(Request.QueryString["idklienti"]), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), rreshtat, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                if (mesazh.Status)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["mesazhRuajtjeMeSukses"], pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                return;
            }

            if (gvLupaAnketa.Selection.Count == 0)
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Zgjidhni nje ankete!", pnlMesazhi);
            else if (gvLupaAnketa.Selection.Count > 1)
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Nuk mund te zgjidhni me shume se nje ankete!", pnlMesazhi);
            else
            {
                List<object> rreshtat = gvLupaAnketa.GetSelectedFieldValues("IdKokaAnketa");
                if (hfKontrollet.Value == "")
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Zgjidhni nje klient!", pnlMesazhi);
                    return;
                }
                string idklient = hfKontrollet.Value;
                string[] arr = idklient.Split(',');
                List<string> klientetelidhur = new List<string>();
                List<string> klientetepalidhur = new List<string>();
                for (int i = 0; i < arr.Length; i++)
                {
                    if (arr[i] == "")
                        continue;

                    if (DbCore.DbCRM.clsKlientAnketa.ekzistonKlientAnketa(int.Parse(arr[i]), (int)rreshtat[0]))
                        continue;
                    DbCore.DbKontabiliteti.clsKlientFurnitor kf = new DbCore.DbKontabiliteti.clsKlientFurnitor(int.Parse(arr[i]));
                    DbCore.DbCRM.clsKokaAnketa kok = new DbCore.DbCRM.clsKokaAnketa((int)rreshtat[0]);
                    //if (DbCore.DbCRM.clsKokaAnketa.kaPrerjeAnketashKlienti(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), int.Parse(arr[i]), kok.DtFillimi, kok.DtMbarimi))
                    //{
                    //    klientetepalidhur.Add(kf.KodKlientFurnitor);
                    //    continue;
                    //}

                    DbCore.DbCRM.clsKlientAnketa klient = new DbCore.DbCRM.clsKlientAnketa(0, int.Parse(arr[i]), (int)rreshtat[0], 1, DateTime.Now, DateTime.Now, DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    DbCore.clsMesazh mesazh = klient.ruaj();

                    if (mesazh.Status)
                        klientetelidhur.Add(kf.KodKlientFurnitor);
                    else
                        klientetepalidhur.Add(kf.KodKlientFurnitor);
                }
                if (klientetepalidhur.Count == 0)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "Lidhja u be me sukses per te gjithe klientet!", pnlMesazhi);
                else
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Lidhja nuk u krye per klientet:" + String.Join(";", klientetepalidhur) + (klientetelidhur.Count > 0 ? " dhe u krye per klientet:" + String.Join(";", klientetelidhur) : "") + "!", pnlMesazhi);
            }
        }
    }
}