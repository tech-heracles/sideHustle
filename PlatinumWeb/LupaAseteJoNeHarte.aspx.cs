using DbCore.DbRegjistrim;
using DevExpress.Web;
using System;
using System.Data;
using System.Globalization;
using System.Resources;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.DbShare;

namespace PlatinumWeb
{
    public partial class LupaAseteJoNeHarte : MyPageBase
    {
   

        protected void Page_Load(object sender, EventArgs e)
        {
            string vleraQueryString = "";
            if (Request.QueryString["idKonfigAmbjente"] != null && Request.QueryString["idKonfigAmbjente"] != "")
                vleraQueryString = Request.QueryString["idKonfigAmbjente"].ToString();
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idPerdorues = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            //clsNivelRegjistrimi nivel = new clsNivelRegjistrimi();
            //nivel.Kodi = "MAG"; //eshte kodi i lupes, mund te shihet ne DB ne T_NIVELREGJISTRIMI
            //nivel.IdNdermarje = idNdermarrje;
            //nivel = nivel.merrNivelRegjSipasKodi();
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);
            int idKonfigAmbjenti = DbCore.clsFunksione.getIdKonfigAmbLupa(vleraQueryString, idNdermarrje, "LASETE");

            if (!IsPostBack)
            {
                //DbCore.clsFunksione.konfiguroMenuRuajPerLupa(ASPxMenu1);
                cmbKonfigurimi.Value = idKonfigAmbjenti.ToString();
                mbushPopUpListeNgaDB(idNdermarrje, idPerdorues);
                konfiguroPopupGride(idKonfigAmbjenti, true);
                GridUtil.AplikoFilterDefault(gvLupaAsete, idKonfigAmbjenti);
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaAsete", idKonfigAmbjenti, "LupaAseteJoNeHarte.aspx");
            }
            else
            {
                mbushPopUpListeNgaSession(idNdermarrje, idPerdorues);
                konfiguroPopupGride(idKonfigAmbjenti, false);
            }
            String array = Request.QueryString["array"];
        }
        

        private void shtoNivel(string komponente, int idNdermarrje, int idPerdoruesi)
        {
            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            if (typeof(GridViewDataComboBoxColumn) != gvLupaAsete.Columns["IdNivel"].GetType())
            {
                gvLupaAsete.Columns.Remove(gvLupaAsete.Columns["IdNivel"]);
                gvLupaAsete.Columns.Add(colnew);
                DataView nivele;

                nivele = DbCore.DbRegjistrim.colNivelRegjistrimi.ktheGjitheNivelRegjistrimiSipasKategoriDtCombo(2, idNdermarrje, idPerdoruesi, true);
                colnew.PropertiesComboBox.DataSource = nivele;
                colnew.PropertiesComboBox.TextField = "Pershkrimi";
                colnew.PropertiesComboBox.ValueField = "IdNivel";
                colnew.FieldName = "IdNivel";
                DbCore.mySessionObjects.ruajDsComboGrideNeSession(Session, nivele, komponente + "nivele" + idNdermarrje);
            }
        }

        private void mbushPopUpListeNgaSession(int idNdermarrje, int idPerdoruesi)
        {
            Object tmpObject;
            DbCore.mySessionObjects.merrGrideNgaSessioniLupa(Session, out tmpObject);
            if (tmpObject == null)
            {
                mbushPopUpListeNgaDB(idNdermarrje, idPerdoruesi);
                return;
            }
            else
            {
                gvLupaAsete.DataSource = tmpObject;
                gvLupaAsete.DataBind();
            }
        }

        private void mbushPopUpListeNgaDB(int idNdermarrje, int idPerdoruesi)
        {//mbush griden e popupit me te dhena
            DataTable dt;
            int idLlojLayeri = 0;
            int idnderviti = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
            if (!String.IsNullOrEmpty(Request.QueryString["idLlojLayer"]) && Int32.TryParse(Request.QueryString["idLlojLayer"], out idLlojLayeri))
                dt = DbCore.DbAsete.colSerialetMagazine.MerrAseteJoNeHarte(idNdermarrje, idnderviti, idPerdoruesi, idLlojLayeri);
            else
                dt = DbCore.DbAsete.colSerialetMagazine.MerrAseteJoNeHarte(idNdermarrje, idnderviti, idPerdoruesi, 0);
            DbCore.mySessionObjects.ruajGrideNeSessionLupa(Session, dt);
            gvLupaAsete.DataSource = dt;
            gvLupaAsete.DataBind();
            dt.Dispose();
        }

        private void konfiguroPopupGride(int idKonfigAmbjenti, bool visibleIndex)
        {
            shtoNivel("LupaAseteJoNeHarte.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session));

            GridUtil.percaktoVisibleColumnsSipasKonfigurimit(gvLupaAsete, "gvLupaAsete", "LupaAseteJoNeHarte.aspx", idKonfigAmbjenti, visibleIndex, DbCore.mySessionObjects.ktheGjuhe(Session));
            //funk.konfiguroGrideListeMadhe(gvLupaAsete, "IdSeriali");
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            var endlessScroll = clsAlternativaKushti.getAlternativa(idKonfigAmbjenti, "ES") == "Po";
            GridUtil.KonfiguroGrideListeMadhePopupiPaTheme(gvLupaAsete, "IdSeriali", true, endlessScroll);
        }

        protected void gvLupaAsete_DataBound(object sender, EventArgs e)
        {
            gvLupaAsete.Settings.ShowFilterRow = true;
            gvLupaAsete.KeyFieldName = "IdSeriali";
            gvLupaAsete.SettingsBehavior.AllowSelectByRowClick = true;
        }

        protected void gvLupaAsete_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            //percaktoTamplate();
            //if (String.IsNullOrEmpty(Request.QueryString["vjenNgaRaporti"]))
            //    gvLupaAsete.Selection.UnselectAll();
        }

        protected void gvLupaAsete_CustomJSProperties(object sender, DevExpress.Web.ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gvLupaAsete.PageIndex;
            e.Properties["cpPageRow"] = gvLupaAsete.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gvLupaAsete.VisibleRowCount;
        }

        protected void gvLupaAsete_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                if (arr[2] == "")
                    gvLupaAsete.FilterExpression = "";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaAsete", "LupaAseteJoNeHarte", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], idNdermarrje, koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        gvLupaAsete.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvLupaAsete);
                    }
                }
            }
            //if (String.IsNullOrEmpty(Request.QueryString["vjenNgaRaporti"]))
            //    gvLupaAsete.Selection.UnselectAll();
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
        /// <param name="idPerdorues"></param>m
        /// <param name="idNermarrje"></param>
        private void percaktoTemplateMenu(ASPxMenu aSPxMenu1, int idViti, int idPerdorues, int idNdermarrje)
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, "LupaAseteJoNeHarte.aspx", this, aSPxMenu1, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
            aSPxMenu1.Items.FindByName("Anullo").Text = rm.GetString("MenuItemMbyll", cultinf);
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
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaAsete", "LupaAseteJoNeHarte", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gvLupaAsete.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("IdSeriali", gvLupaAsete);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvLupaAsete.GetSortedColumns();
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
            //    filtri.KoloneRenditje = "IdSeriali";
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
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaAsete", Convert.ToInt32(cmbKonfigurimi.Value), "LupaAseteJoNeHarte");
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
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaAsete", "LupaAseteJoNeHarte.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();
                //mbushComboBoxFiltra(idNdermarrje);
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaAsete", Convert.ToInt32(cmbKonfigurimi.Value), "LupaAseteJoNeHarte.aspx");
                percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);
                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                gvLupaAsete.FilterExpression = String.Empty;
            }
        }
    }
}