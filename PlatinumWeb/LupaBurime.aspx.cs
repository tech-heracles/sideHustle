using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using DbCore.DbRegjistrim;
using System.Data;
using DevExpress.Web;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.DbShare;

namespace PlatinumWeb
{
    public partial class LupaBurime : MyPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string vleraQueryString;
            if (Request.QueryString["idKonfigAmbjente"] != null && Request.QueryString["idKonfigAmbjente"] != "")
                vleraQueryString = Request.QueryString["idKonfigAmbjente"].ToString();
            else
                vleraQueryString = "";
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            //nivel.Kodi = "LBUR"; //eshte kodi i lupes, mund te shihet ne DB ne T_NIVELREGJISTRIMI
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);
            int idKonfigAmbjenti = DbCore.clsFunksione.getIdKonfigAmbLupa(vleraQueryString, idNdermarrje, "LBUR");
            bool kerkosaposhkruar = true;
            if (DbCore.DbShare.clsAlternativaKushti.getAlternativa(idKonfigAmbjenti, "KSSH") == "Po")
                kerkosaposhkruar = true;
            else kerkosaposhkruar = false;
            bool endlessScroll = false;
            if (clsAlternativaKushti.getAlternativa(idKonfigAmbjenti, "ES") == "Po")
                endlessScroll = true;
            if (!IsPostBack)
            {
                cmbKonfigurimi.Value = idKonfigAmbjenti.ToString();
                GridUtil.AplikoFilterDefault(gvLupaBurimet, idKonfigAmbjenti);
                mbushPopUpListeNgaDB();
                konfiguroPopupGride(idKonfigAmbjenti, true, kerkosaposhkruar, endlessScroll);
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaBurimet", idKonfigAmbjenti, "LupaBurime.aspx");
            }
            else
            {
                mbushPopUpListeNgaSession();
                konfiguroPopupGride(idKonfigAmbjenti, false, kerkosaposhkruar, endlessScroll);
            }
            
        }

        //        oKusht = colKushtet[0]; //cdo konfigurim ambjenti per LUPAT ka vetem nje kusht qe eshte filtri default i grides


        private void mbushPopUpListeNgaSession()
        {
            Object tmpObject;
            DbCore.mySessionObjects.merrGrideNgaSessioniLupa(Session, out tmpObject);
            if (tmpObject == null)
            { mbushPopUpListeNgaDB(); return; }
            gvLupaBurimet.DataSource = tmpObject;
            gvLupaBurimet.DataBind();
        }
        private void mbushPopUpListeNgaDB()
        {//mbush griden e popupit me te dhena            
            DataTable dt = DbCore.DbProdhimi.colBurimet.merrBurimeNdermarjeDTAktive(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            DbCore.mySessionObjects.ruajGrideNeSessionLupa(Session, dt);
            gvLupaBurimet.DataSource = dt;
            gvLupaBurimet.DataBind();
            dt.Dispose();
        }


        private void konfiguroPopupGride(int idKonfigAmbjenti, bool visibleIndex, bool kerkosaposhkruar, bool endlessScroll)
        {
            shtoTip();
            GridUtil.percaktoVisibleColumnsSipasKonfigurimit(gvLupaBurimet, "gvLupaBurimet", "LupaBurime.aspx", idKonfigAmbjenti, visibleIndex, DbCore.mySessionObjects.ktheGjuhe(Session));
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            GridUtil.KonfiguroGrideListeMadhePopupiPaTheme(gvLupaBurimet, "IdBurimi", kerkosaposhkruar, endlessScroll);
        }
        /// <summary>
        /// perdoret per te shfaqur llojin ne vend te true/false si dhe filtri i llojit te shfaqet ne forme komboje
        /// </summary>
        private void shtoTip()
        {
            int visibleindex = gvLupaBurimet.Columns["Tipi"].VisibleIndex;
            gvLupaBurimet.Columns.Remove(gvLupaBurimet.Columns["Tipi"]);
            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            colnew.PropertiesComboBox.Items.Add("", Convert.ToInt32(DbCore.DbListPagesat.TipPagese.Undefined));
            colnew.PropertiesComboBox.Items.Add(DbCore.DbProdhimi.TipBurimi.Makineri.ToString(), Convert.ToInt32(DbCore.DbProdhimi.TipBurimi.Makineri));
            colnew.PropertiesComboBox.Items.Add(DbCore.DbProdhimi.TipBurimi.Mjet.ToString(), Convert.ToInt32(DbCore.DbProdhimi.TipBurimi.Mjet));
            colnew.PropertiesComboBox.Items.Add(DbCore.DbProdhimi.TipBurimi.Punonjes.ToString(), Convert.ToInt32(DbCore.DbProdhimi.TipBurimi.Punonjes));
            colnew.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            colnew.FieldName = "Tipi";
            colnew.VisibleIndex = visibleindex;
            gvLupaBurimet.Columns.Add(colnew);
        }

        protected void gvLupaBurimet_DataBound(object sender, EventArgs e)
        {
            gvLupaBurimet.Settings.ShowFilterRow = true;
            gvLupaBurimet.KeyFieldName = "IdBurimi";
            gvLupaBurimet.SettingsBehavior.AllowSelectByRowClick = true;
        }

        protected void gvLupaBurimet_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {

            gvLupaBurimet.Selection.UnselectAll();
        }


        //    else


        //            else
        //        else


        //    //using (DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin())
        //    //{
        //    //    if (dbAdmin.ekzistonFilter(Kodi_ASPxTextBox.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session)))
        //    //    else
        //    //}

        protected void gvLupaBurimet_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gvLupaBurimet.PageIndex;
            e.Properties["cpPageRow"] = gvLupaBurimet.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gvLupaBurimet.VisibleRowCount;
        }

        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        }

        protected void gvLupaBurimet_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
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
                    gvLupaBurimet.FilterExpression = "";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaBurimet", "LupaBurime.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], idNdermarrje, koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        gvLupaBurimet.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvLupaBurimet);
                    }
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
            gvLupaBurimet.Selection.UnselectAll();
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
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, "LupaBurime.aspx", this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
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
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaBurimet", "LupaBurime.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gvLupaBurimet.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("IdBurimi", gvLupaBurimet);
            //    else
            //else
            DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();

            oPerdorues = DbCore.mySessionObjects.kthePerdorues(Session);
            filtri.IdPerdoruesi = oPerdorues.IdPerdorues;
            filtri.IdNdermarje = idNdermarrje;
            filtri.IdStatusDok = 1;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            mesazh = filtri.ruaj();
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaBurimet", Convert.ToInt32(cmbKonfigurimi.Value), "LupaBurime.aspx");
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
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaBurimet", "LupaBurime.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaBurimet", Convert.ToInt32(cmbKonfigurimi.Value), "LupaBurime.aspx");
                percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);
                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                gvLupaBurimet.FilterExpression = String.Empty;
            }
        }

        //private void mbushComboBoxFiltra(int idNdermarrje)
    }
}