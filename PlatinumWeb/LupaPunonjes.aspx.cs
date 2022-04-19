using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Data;
using System.Web.Script.Serialization;
using System.Globalization;
using System.Resources;
using PlatinumWeb.ApplicationUtils.Pages;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using DbCore.DbShare;

namespace PlatinumWeb
{
    public partial class LupaPunonjes : MyPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string vleraQueryString;
            if (Request.QueryString["idKonfigAmbjente"] != null && Request.QueryString["idKonfigAmbjente"] != "")
                vleraQueryString = Request.QueryString["idKonfigAmbjente"];
            else
                vleraQueryString = "";
            bool raportuese = (Request.QueryString["Raportuese"] != null);
            int idNdermarrje;
            int idViti;
            int idGjuha;
            int idKonfigambjenti;
            var idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);       
            if (!IsPostBack)
            {
                idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
                idKonfigambjenti = DbCore.clsFunksione.getIdKonfigAmbLupa(vleraQueryString, idNdermarrje, "LPun");
                cmbKonfigurimi.Value = idKonfigambjenti.ToString();
                idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
                mbushPopUpListeNgaDB(raportuese);
                GridUtil.AplikoFilterDefault(gvLupaPunonjes, idKonfigambjenti);
                CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
                ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                EmratEKontrolleve(rm, cultinf);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("idPerdoruesi", idPerdoruesi);
                hfState.Set("idViti", idViti);
                hfState.Set("idKonfig", idKonfigambjenti);
                hfState.Set("idGjuha", idGjuha);
                konfiguroPopupGride(idKonfigambjenti, true);
                clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idNdermarrje, "gvLupaPunonjes", idKonfigambjenti, "LupaPunonjes.aspx");
            }
            else
            {
                idNdermarrje = (int)hfState.Get("idNdermarrje");
                idPerdoruesi = (int)hfState.Get("idPerdoruesi");
                idViti = (int)hfState.Get("idViti");
                idKonfigambjenti = (int)hfState.Get("idKonfig");
                idGjuha =(int) hfState.Get("idGjuha");
                mbushPopUpListeNgaSession();
                konfiguroPopupGride(idKonfigambjenti, false);
            }

            percaktoTemplateMenu(ASPxMenu1, idViti, idPerdoruesi, idNdermarrje);
        }

        private void EmratEKontrolleve(ResourceManager rm, CultureInfo cultinf)
        {
            lblApliko.Text = rm.GetString("msgLupaPunonjesAplikoTeNjejtinNrDiteshPerTeSelektuarit", cultinf);
            lblNrDitesh.Text = rm.GetString("msgLupaPunonjesNrDitesh", cultinf);
            gridaSelectFaqe.ToolTip = rm.GetString("btnZgjidhTeGjitheFaqen", cultinf);
            gridaSelectTeGjitha.ToolTip = rm.GetString("btnZgjidhTeGjithe", cultinf);
            gridaUnSelectTeGjitha.ToolTip = rm.GetString("btnFshiZgjedhjen", cultinf);
            hfState.Set("msgLupaPunonjesekzistonPunonjesi", rm.GetString("msgLupaPunonjesekzistonPunonjesi", cultinf));
            hfState.Set("msgLupaPunonjesNeGride", rm.GetString("msgLupaPunonjesNeGride", cultinf)); ;
            hfState.Set("msgLupaPunonjesPunonjesi", rm.GetString("msgLupaPunonjesPunonjesi", cultinf));
            hfState.Set("msgLupaPunonjesNukKaKomponentePerKeteDate", rm.GetString("msgLupaPunonjesNukKaKomponentePerKeteDate", cultinf));
        }

        /// <summary>
        /// mbush lupen nga sessioni
        /// </summary>
        private void mbushPopUpListeNgaSession()
        {
            Object tmpObject;
            DbCore.mySessionObjects.merrGrideNgaSessioniLupa(Session, out tmpObject);
            if (tmpObject == null)
                mbushPopUpListeNgaDB(Request.QueryString["Raportuese"] != null);
            else
            {
                gvLupaPunonjes.DataSource = tmpObject;
                gvLupaPunonjes.DataBind();
            }
        }
        /// <summary>
        /// mbush lupen nga db
        /// </summary>
        private void mbushPopUpListeNgaDB(bool raportuese)
        {//mbush griden e popupit me te dhena   
            DataTable dt = new DataTable();
            int.TryParse(Request.QueryString["dep"], out int dep);
            int.TryParse(Request.QueryString["nendep"], out int nendep);

            if (Request.QueryString["vjenNga"] == "ListPagesa")
                dt = DbCore.DbListPagesat.colPunonjes.merrPunonjesNdermarjeDTAktivJoTeLarguar(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DateTime.Parse("01/" + Request.QueryString["muaj"] + "/" + DbCore.mySessionObjects.merrPeriudheKontabel(Session).FillimiPeriudha.Year), dep, nendep);
            else dt = DbCore.DbListPagesat.colPunonjes.merrPunonjesNdermarjeDTAktiv(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), raportuese);
            DbCore.mySessionObjects.ruajGrideNeSessionLupa(Session, dt);
            gvLupaPunonjes.DataSource = dt;
            gvLupaPunonjes.DataBind();
            dt.Dispose();
        }

        /// <summary>
        /// konfiguron griden
        /// </summary>
        private void konfiguroPopupGride(int idKonfigambjenti, bool visibleIndex)
        {//konfiguron popupgriden
            var idndermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            KonfigurimComboGride.ShtoDepartament(gvLupaPunonjes, idndermarrje, Session, "LupaPunonjes.aspx", "", "IdDepartament");
            KonfigurimComboGride.ShtoNenDepartament(gvLupaPunonjes, idndermarrje, Session, "LupaPunonjes.aspx", "", "IdNenDepartament");
            KonfigurimComboGride.ShtoQytetet(gvLupaPunonjes, idndermarrje, Session, "LupaPunonjes.aspx", "", "IdQyteti");
            KonfigurimComboGride.shtoGrupPunonjes(gvLupaPunonjes, idndermarrje, Session, "LupaPunonjes.aspx", "");
            KonfigurimComboGride.shtoSeks(gvLupaPunonjes, rm, ci);
            KonfigurimComboGride.shtoEdukimi(gvLupaPunonjes, idndermarrje, DbCore.mySessionObjects.ktheGjuhe(Session), Session, "LupaPunonjes.aspx", "");
            GridUtil.percaktoVisibleColumnsSipasKonfigurimit(gvLupaPunonjes, "gvLupaPunonjes", "LupaPunonjes.aspx", idKonfigambjenti, visibleIndex, DbCore.mySessionObjects.ktheGjuhe(Session));
            gvLupaPunonjes.Columns["#"].VisibleIndex = 0;
            if (clsAlternativaKushti.getAlternativa(idKonfigambjenti, "ES") == "Po")
            {
                gvLupaPunonjes.SettingsPager.Mode = GridViewPagerMode.EndlessPaging;
                gvLupaPunonjes.Columns["#"].Width = 25;
            }
        }
        
       
        /// <summary>
        /// data boundi i grides
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvLupaPunonjes_DataBound(object sender, EventArgs e)
        {
            if (gvLupaPunonjes.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per 
                //perzgjidh
                GridViewCommandColumn check = new GridViewCommandColumn("#") { ShowSelectCheckbox = true, Width = Unit.Percentage(2) };
                //behet per te afishuar rreshtin qe do sherbej per filtrim
                gvLupaPunonjes.Settings.ShowFilterRow = true;
                gvLupaPunonjes.Settings.ShowFilterRowMenu = true;
                gvLupaPunonjes.Columns.Add(check);
                gvLupaPunonjes.KeyFieldName = "IdPunonjes";
                gvLupaPunonjes.SettingsBehavior.AllowSelectByRowClick = true;
                gvLupaPunonjes.SettingsBehavior.AllowFocusedRow = true;
            }

        }
        /// <summary>
        /// kur lupa ben callback
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvLupaPunonjes_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            //  gvLupaPunonjes.Selection.UnselectAll();
        }



        protected void gvLupaPunonjes_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                if (arr[2] == "")
                    gvLupaPunonjes.FilterExpression = "";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaPunonjes", "LupaPunonjes.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], idNdermarrje, koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        gvLupaPunonjes.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvLupaPunonjes);                        
                    }
                }
            }
            gvLupaPunonjes.Selection.UnselectAll();
        }

        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
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
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, "LupaPunonjes.aspx", this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
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
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaPunonjes", "LupaPunonjes.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gvLupaPunonjes.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("NrPersonal", gvLupaPunonjes);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvLupaPunonjes.GetSortedColumns();
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
            //    filtri.KoloneRenditje = "NrPersonal";
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
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaPunonjes", Convert.ToInt32(cmbKonfigurimi.Value), "LupaPunonjes.aspx");
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
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaPunonjes", "LupaPunonjes.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();
                //mbushComboBoxFiltra(idNdermarrje);
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaPunonjes", Convert.ToInt32(cmbKonfigurimi.Value), "LupaPunonjes.aspx");
                percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);
                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                gvLupaPunonjes.FilterExpression = String.Empty;
            }
        }

        //private void mbushComboBoxFiltra(int idNdermarrje)
        //{
        //    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka("gvLupaPunonjes", "LupaPunonjes.aspx", idNdermarrje);
        //    DbCore.DbAdmin.colFiltratGrida colFiltra = new DbCore.DbAdmin.colFiltratGrida(koka.IdGridaKoka, idNdermarrje);
        //    colFiltra.Insert(0, new DbCore.DbAdmin.clsFiltraGrida());
        //    MenuFilter.colekstioni = colFiltra;
        //    MenuFilter.ValueField = "IdFiltra";
        //    MenuFilter.TextField = "FiltraShenime";
        //}
    }
}