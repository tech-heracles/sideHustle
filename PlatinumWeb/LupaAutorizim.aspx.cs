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
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.DbShare;

namespace PlatinumWeb
{
    public partial class LupaAutorizim : MyPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String array = Request.QueryString["array"];

            string vleraQueryString = "";
            if (Request.QueryString["idKonfigAmbjente"] != null && Request.QueryString["idKonfigAmbjente"] != "")
                vleraQueryString = Request.QueryString["idKonfigAmbjente"].ToString();
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            //nivel.Kodi = "AUT"; //mund te shihet ne DB ne T_NIVELREGJISTRIMI
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);
            int idKonfigambjenti = DbCore.clsFunksione.getIdKonfigAmbLupa(vleraQueryString, idNdermarrje, "AUT");
            mbushPopUpListeAutorizimesh(idNdermarrje);
            bool kerkosaposhkruar = true;
            if (DbCore.DbShare.clsAlternativaKushti.getAlternativa(idKonfigambjenti, "KSSH") == "Po")
                kerkosaposhkruar = true;
            else kerkosaposhkruar = false;
            bool endlessScroll = false;
            if (clsAlternativaKushti.getAlternativa(idKonfigambjenti, "ES") == "Po")
                endlessScroll = true;
            if (!IsPostBack)
            {
                cmbKonfigurimi.Value = idKonfigambjenti.ToString();
                GridUtil.AplikoFilterDefault(gvLupaAutorizim, idKonfigambjenti);
                konfiguroPopupGride(idKonfigambjenti, true, kerkosaposhkruar, endlessScroll);
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaAutorizim", idKonfigambjenti, "LupaAutorizim.aspx");
            }
            else
            {
                konfiguroPopupGride(idKonfigambjenti, false, kerkosaposhkruar, endlessScroll);
            }
        }
        private void mbushPopUpListeAutorizimesh(int idNdermarrje)
        {//mbush griden e popupit me te dhena
            DbCore.DbAdmin.colAutorizimetKoka colAutorizimet = new DbCore.DbAdmin.colAutorizimetKoka(DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            gvLupaAutorizim.DataSource = colAutorizimet;
            gvLupaAutorizim.DataBind();
            if (!hfState.Contains("vjenNga") || (!(hfState.Get("vjenNga").ToString()).Equals("vetLupa")))
            {
                if (Request.QueryString["autorizimet"] != null && Request.QueryString["autorizimet"] != "")
                {
                    string autorizimet = Request.QueryString["autorizimet"];
                    string[] auto = autorizimet.Split(',');
                    for (int i = auto.Length - 1; i >= 0; i--)
                    {
                        if (String.IsNullOrWhiteSpace(auto[i]))
                            continue;
                        int idAutorizim = DbCore.DbAdmin.clsAutorizimKoka.ktheIDAutorizim(auto[i].Trim());
                        if (!IsPostBack)
                            gvLupaAutorizim.Selection.SelectRowByKey(idAutorizim);
                        DbCore.DbAdmin.clsAutorizimKoka autorizim = colAutorizimet.Find(x => x.IdAutorizimKoka == idAutorizim);
                        if (autorizim != null)
                        {
                            colAutorizimet.Remove(autorizim);
                            colAutorizimet.Insert(0, autorizim);
                        }
                    }
                    gvLupaAutorizim.DataSource = colAutorizimet;
                    gvLupaAutorizim.DataBind();
                }
            }
        }
        private void konfiguroPopupGride(int idKonfigambjenti, bool visibleIndex, bool kerkosaposhkruar, bool endlessScroll)
        {//konfiguron popupgriden
            GridUtil.percaktoVisibleColumnsSipasKonfigurimit(gvLupaAutorizim, "gvLupaAutorizim", "LupaAutorizim.aspx", idKonfigambjenti, visibleIndex, DbCore.mySessionObjects.ktheGjuhe(Session));
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            GridUtil.KonfiguroGrideListeMadhePopupiPaTheme(gvLupaAutorizim, "IdAutorizimKoka", kerkosaposhkruar, endlessScroll);
            this.gvLupaAutorizim.Columns["#"].VisibleIndex = 0;
        }

        protected void gvLupaAutorizim_DataBound(object sender, EventArgs e)
        {
            if (this.gvLupaAutorizim.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per 
                //perzgjidh
                GridViewCommandColumn check = new GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true; check.Width = Unit.Percentage(2);
                //behet per te afishuar rreshtin qe do sherbej per filtrim
                gvLupaAutorizim.Settings.ShowFilterRow = true;
                gvLupaAutorizim.Columns.Add(check);

                gvLupaAutorizim.KeyFieldName = "IdAutorizimKoka";
                gvLupaAutorizim.SettingsBehavior.AllowSelectByRowClick = true;

            }


        }

        protected void gvLupaAutorizim_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            //kur popupgrida ben callback
        }

        protected void gvLupaAutorizim_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {

        }


        //    else


        //            else
        //        else


        //    //if (dbAdmin.ekzistonFilter(Kodi_ASPxTextBox.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session)))


        //        oKusht = colKushtet[0]; //cdo konfigurim ambjenti per LUPAT ka vetem nje kusht qe eshte filtri default i grides
        //    else

        //    //if (ofilter != null)
        //    try
        //        return; //nuk ben asgje nese filtri nuk gjendet

        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        }

        protected void gvLupaAutorizim_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
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
                    gvLupaAutorizim.FilterExpression = "";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaAutorizim", "LupaAutorizim.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], idNdermarrje, koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        gvLupaAutorizim.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvLupaAutorizim);
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
            gvLupaAutorizim.Selection.UnselectAll();
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
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, "LupaAutorizim.aspx", this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
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
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaAutorizim", "LupaAutorizim.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gvLupaAutorizim.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("IdAutorizimKoka", gvLupaAutorizim);
            //    else
            //else
            DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();

            oPerdorues = DbCore.mySessionObjects.kthePerdorues(Session);
            filtri.IdPerdoruesi = oPerdorues.IdPerdorues;
            filtri.IdNdermarje = idNdermarrje;
            filtri.IdStatusDok = 1;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            mesazh = filtri.ruaj();
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaAutorizim", Convert.ToInt32(cmbKonfigurimi.Value), "LupaAutorizim.aspx");
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
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaAutorizim", "LupaAutorizim.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaAutorizim", Convert.ToInt32(cmbKonfigurimi.Value), "LupaAutorizim.aspx");
                percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);
                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                gvLupaAutorizim.FilterExpression = String.Empty;
            }
        }

        //private void mbushComboBoxFiltra(int idNdermarrje)
    }
}
