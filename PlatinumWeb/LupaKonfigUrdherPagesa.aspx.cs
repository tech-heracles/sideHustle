using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using DbCore.DbShare;
using DevExpress.Web;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class LupaKonfigUrdherPagesa : MyPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string vleraQueryString;
            if (Request.QueryString["idKonfigAmbjente"] != null && Request.QueryString["idKonfigAmbjente"] != "")
                vleraQueryString = Request.QueryString["idKonfigAmbjente"];
            else
                vleraQueryString = "";

            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            //nivel.Kodi = "LKUP"; // mund te shihet ne DB ne T_NIVELREGJISTRIMI
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);
            int idKonfigambjenti = DbCore.clsFunksione.getIdKonfigAmbLupa(vleraQueryString, idNdermarrje, "LKUP");
            bool kerkosaposhkruar = true;
            if (DbCore.DbShare.clsAlternativaKushti.getAlternativa(idKonfigambjenti, "KSSH") == "Po")
                kerkosaposhkruar = true;
            else kerkosaposhkruar = false;
            if (!IsPostBack)
            {
                cmbKonfigurimi.Value = idKonfigambjenti.ToString();
                GridUtil.AplikoFilterDefault(gvLupaKonfig, idKonfigambjenti);
                mbushPopUpListeNgaDB();
                konfiguroPopupGride(idKonfigambjenti, true, kerkosaposhkruar);
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaKonfig", idKonfigambjenti, "LupaKonfigUrdherPagesa.aspx");
            }
            else
            {
                mbushPopUpListeNgaSession();
                konfiguroPopupGride(idKonfigambjenti, false, kerkosaposhkruar);
            }
        }
        ///// <summary>
        ///// merr filtrin default
        ///// </summary>
        ///// <param name="idkonfigAmbjenti"></param>
        ///// <returns></returns>

        //        oKusht = colKushtet[0]; //cdo konfigurim ambjenti per LUPAT ka vetem nje kusht qe eshte filtri default i grides
        ///// <summary>
        ///// aplikon filtrin 
        ///// </summary>
        /// <summary>
        /// mbush griden nga sessioni
        /// </summary>
        private void mbushPopUpListeNgaSession()
        {
            Object tmpObject;
            DbCore.mySessionObjects.merrGrideNgaSessioniLupa(Session, out tmpObject);
            if (tmpObject == null)
            { mbushPopUpListeNgaDB(); return; }
            gvLupaKonfig.DataSource = tmpObject;
            gvLupaKonfig.DataBind();
        }
        /// <summary>
        /// mbush griden nga databaza
        /// </summary>
        private void mbushPopUpListeNgaDB()
        {//mbush griden e popupit me te dhena            
            DbCore.DbArkaBanka.colKonfigUrdherPagese col = new DbCore.DbArkaBanka.colKonfigUrdherPagese();
            if (Request.QueryString["vjenNga"] == "Grupi")
            {
                col = new DbCore.DbArkaBanka.colKonfigUrdherPagese(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), Convert.ToInt32(DbCore.DbArkaBanka.LlojeKonfigurimeUrdherPagese.Grup));
            }
            else if (Request.QueryString["vjenNga"] == "Titulli")
            {
                col = new DbCore.DbArkaBanka.colKonfigUrdherPagese(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), Convert.ToInt32(DbCore.DbArkaBanka.LlojeKonfigurimeUrdherPagese.Titull));
            }
            else if (Request.QueryString["vjenNga"] == "Kapitulli")
            {
                col = new DbCore.DbArkaBanka.colKonfigUrdherPagese(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), Convert.ToInt32(DbCore.DbArkaBanka.LlojeKonfigurimeUrdherPagese.Kapitull));
            }
            DbCore.mySessionObjects.ruajGrideNeSessionLupa(Session, col); gvLupaKonfig.DataSource = col;
            gvLupaKonfig.DataBind();
        }

        /// <summary>
        /// konfiguron griden
        /// </summary>
        private void konfiguroPopupGride(int idKonfigambjenti, bool visibleIndex, bool kerkosaposhkruar)
        {//konfiguron popupgriden

            GridUtil.percaktoVisibleColumnsSipasKonfigurimit(gvLupaKonfig, "gvLupaKonfig", "LupaKonfigUrdherPagesa.aspx", idKonfigambjenti, visibleIndex, DbCore.mySessionObjects.ktheGjuhe(Session));
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            var endlessScroll = clsAlternativaKushti.getAlternativa(idKonfigambjenti, "ES") == "Po";
            GridUtil.KonfiguroGrideListeMadhePopupiPaTheme(gvLupaKonfig, "Id", kerkosaposhkruar, endlessScroll);
        }

        /// <summary>
        /// data boundi i grides
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvLupaKonfig_DataBound(object sender, EventArgs e)
        {
            gvLupaKonfig.Settings.ShowFilterRow = true;
            gvLupaKonfig.KeyFieldName = "Id";
            gvLupaKonfig.SettingsBehavior.AllowSelectByRowClick = true;
        }
        /// <summary>
        /// kur grida ben callback
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvLupaKonfig_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            gvLupaKonfig.Selection.UnselectAll();
        }

        ///// <summary>
        ///// aplikon filtrin 
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //    else

        ///// <summary>
        ///// ruan filtrin 
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>

        //            else
        //        else

        ///// <summary>
        ///// validon nese ekziston filtri
        ///// </summary>
        ///// <param name="source"></param>
        ///// <param name="args"></param>
        //    //using (DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin())
        //    //{
        //    //    if (dbAdmin.ekzistonFilter(Kodi_ASPxTextBox.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session)))
        //    //    else
        //    //}
        protected void gvLupaKonfig_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                if (arr[2] == "")
                    gvLupaKonfig.FilterExpression = "";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaKonfig", "LupaKonfigUrdherPagesa.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], idNdermarrje, koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        gvLupaKonfig.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvLupaKonfig);
                    }
                }
            }
            gvLupaKonfig.Selection.UnselectAll();
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
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, "LupaKonfigUrdherPagesa.aspx", this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
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
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaKonfig", "LupaKonfigUrdherPagesa.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gvLupaKonfig.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("Kodi", gvLupaKonfig);
            //    else
            //else
            DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();

            oPerdorues = DbCore.mySessionObjects.kthePerdorues(Session);
            filtri.IdPerdoruesi = oPerdorues.IdPerdorues;
            filtri.IdNdermarje = idNdermarrje;
            filtri.IdStatusDok = 1;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            mesazh = filtri.ruaj();
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaKonfig", Convert.ToInt32(cmbKonfigurimi.Value), "LupaKonfigUrdherPagesa.aspx");
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
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaKonfig", "LupaKonfigUrdherPagesa.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaKonfig", Convert.ToInt32(cmbKonfigurimi.Value), "LupaKonfigUrdherPagesa.aspx");
                percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);
                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                gvLupaKonfig.FilterExpression = String.Empty;
            }
        }

        //private void mbushComboBoxFiltra(int idNdermarrje)
    }
}