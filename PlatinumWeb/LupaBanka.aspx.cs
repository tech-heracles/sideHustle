using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Data;
using DbCore.DbRegjistrim;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.DbShare;

namespace PlatinumWeb
{
    public partial class LupaBanka : MyPageBase
    {
        int arkabanka;

        protected void Page_Load(object sender, EventArgs e)
        {
            string vleraQueryString = "";
            if (Request.QueryString["idKonfigAmbjente"] != null && Request.QueryString["idKonfigAmbjente"] != "")
                vleraQueryString = Request.QueryString["idKonfigAmbjente"].ToString();
            if (Request.QueryString["arkabanka"] != null)
                arkabanka = int.Parse(Request.QueryString["arkabanka"]);
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
           
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);
            int idKonfigambjenti = DbCore.clsFunksione.getIdKonfigAmbLupa(vleraQueryString, idNdermarrje, "BNK");
            
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
                mbushPopUpListeNgaDB();
                GridUtil.AplikoFilterDefault(gvLupaBanka, idKonfigambjenti);
                konfiguroPopupGride(idKonfigambjenti, true, kerkosaposhkruar, endlessScroll);
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaBanka", idKonfigambjenti, "LupaBanka.aspx");
                if (!String.IsNullOrEmpty(Request.QueryString["arka"]))
                    gvLupaBanka.FilterExpression = "[LlojArkaBanka]=" + Convert.ToBoolean(Request.QueryString["arka"]);
            }
            else
            {
                mbushPopUpListeNgaSession();
                konfiguroPopupGride(idKonfigambjenti, false, kerkosaposhkruar, endlessScroll);
            }
            
            //string monedhaNdermarrje = monnderm.KodiMonedha;
            string monedhaNdermarrje = DbCore.DbAdmin.clsMonedha.ktheMonedhenENdermarrjes(idNdermarrje);
            string monedhaKlient = Request.QueryString["monedha"];
            DbCore.DbAdmin.clsMonedha mon = new DbCore.DbAdmin.clsMonedha();

            if (!String.IsNullOrEmpty(Request.QueryString["monedha"]))
            {
                mon.mbushMonedhePershk(monedhaKlient, idNdermarrje);
                if (monedhaKlient == monedhaNdermarrje) gvLupaBanka.FilterExpression = "[KodMonedha]= '" + mon.KodiMonedha + "'";
                else if (monedhaKlient != monedhaNdermarrje) gvLupaBanka.FilterExpression = "[KodMonedha]= '" + mon.KodiMonedha + "' Or [KodMonedha]= '" + monedhaNdermarrje + "'";
            }

            if (!String.IsNullOrEmpty(Request.QueryString["arka"]) && String.IsNullOrEmpty(Request.QueryString["PermbledhesArkeBanke"]))
                gvLupaBanka.FilterExpression = "[LlojArkaBanka]=" + Convert.ToBoolean(Request.QueryString["arka"]);
        }
        
        /// </summary>
        private void shtoLloj()
        {
            int visibleindex = gvLupaBanka.Columns["LlojArkaBanka"].VisibleIndex;
            gvLupaBanka.Columns.Remove(gvLupaBanka.Columns["LlojArkaBanka"]);
            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();
            oPerdorues = DbCore.mySessionObjects.kthePerdorues(Session);
            colnew.PropertiesComboBox.Items.Add("Banka", true);
            colnew.PropertiesComboBox.Items.Add("Arka", false);
            colnew.FieldName = "LlojArkaBanka";
            colnew.VisibleIndex = visibleindex;
            gvLupaBanka.Columns.Add(colnew);
        }
        
        private void mbushPopUpListeNgaSession()
        {
            Object tmpObject;
            DbCore.mySessionObjects.merrGrideNgaSessioniLupa(Session, out tmpObject);
            if (tmpObject == null)
            {
                mbushPopUpListeNgaDB();
                return;
            }
            gvLupaBanka.DataSource = tmpObject;
            gvLupaBanka.DataBind();
        }

        /// <summary>
        /// Mbush griden e popupit me te dhena  
        /// </summary>
        private void mbushPopUpListeNgaDB()
        {
            DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();
            oPerdorues = DbCore.mySessionObjects.kthePerdorues(Session);
            DbCore.DbKontabiliteti.colLlogarite colLlog = new DbCore.DbKontabiliteti.colLlogarite();
            DataTable dt;
            bool MerrNdemarrjeBija = false;
            if (!String.IsNullOrEmpty(Request.QueryString["MerrNdemarrjeBija"]))
                MerrNdemarrjeBija = true;

            if (!String.IsNullOrEmpty(Request.QueryString["PermbledhesArkeBanke"]))
                dt = DbCore.DbArkaBanka.colBankat.merrSipasABNdermarrjesAndAutorizimeDTLupa(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), MerrNdemarrjeBija, true);
            else if (arkabanka == 4)
                dt = DbCore.DbArkaBanka.colBankat.ktheGjitheBankatSipasAutorizimeveSipasLlojit(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), true, MerrNdemarrjeBija);
            else if (arkabanka == 3)
                dt = DbCore.DbArkaBanka.colBankat.ktheGjitheBankatSipasAutorizimeveSipasLlojit(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), false, MerrNdemarrjeBija);
            else
            {
                dt = DbCore.DbArkaBanka.colBankat.merrSipasABNdermarrjesAndAutorizimeDTLupa(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), MerrNdemarrjeBija, true);
            }
            DbCore.mySessionObjects.ruajGrideNeSessionLupa(Session, dt); gvLupaBanka.DataSource = dt;
            gvLupaBanka.DataBind();
            dt.Dispose();
        }

        /// <summary>
        /// Konfiguron popupgriden
        /// </summary>
        /// <param name="idKonfigambjenti"></param>
        /// <param name="visibleIndex"></param>
        /// <param name="kerkosaposhkruar"></param>
        /// <param name="endlessScroll"></param>
        private void konfiguroPopupGride(int idKonfigambjenti, bool visibleIndex, bool kerkosaposhkruar, bool endlessScroll)
        {
            shtoLloj();
            GridUtil.percaktoVisibleColumnsSipasKonfigurimit(gvLupaBanka, "gvLupaBanka", "LupaBanka.aspx", idKonfigambjenti, visibleIndex, DbCore.mySessionObjects.ktheGjuhe(Session));
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            GridUtil.KonfiguroGrideListeMadhePopupiPaTheme(gvLupaBanka, "KodiBanka", kerkosaposhkruar, endlessScroll);
        }

        protected void gvLupaBanka_DataBound(object sender, EventArgs e)
        {
            gvLupaBanka.Settings.ShowFilterRow = true;
            gvLupaBanka.KeyFieldName = "KodiBanka";
            gvLupaBanka.SettingsBehavior.AllowSelectByRowClick = true;
        }

        protected void gvLupaBanka_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            if (String.IsNullOrEmpty(Request.QueryString["vjenNgaRaporti"]))
                gvLupaBanka.Selection.UnselectAll();
        }

        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        }

        protected void gvLupaBanka_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
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
                    gvLupaBanka.FilterExpression = "";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaBanka", "LupaBanka.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], idNdermarrje, koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        gvLupaBanka.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvLupaBanka);
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
            if (String.IsNullOrEmpty(Request.QueryString["vjenNgaRaporti"]))
                gvLupaBanka.Selection.UnselectAll();
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
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, "LupaBanka.aspx", this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
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
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaBanka", "LupaBanka.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gvLupaBanka.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("KodiBanka", gvLupaBanka);
            DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();

            oPerdorues = DbCore.mySessionObjects.kthePerdorues(Session);
            filtri.IdPerdoruesi = oPerdorues.IdPerdorues;
            filtri.IdNdermarje = idNdermarrje;
            filtri.IdStatusDok = 1;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            mesazh = filtri.ruaj();
            //mbushComboBoxFiltra(idNdermarrje);
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaBanka", Convert.ToInt32(cmbKonfigurimi.Value), "LupaBanka.aspx");
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
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaBanka", "LupaBanka.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();
                //mbushComboBoxFiltra(idNdermarrje);
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaBanka", Convert.ToInt32(cmbKonfigurimi.Value), "LupaBanka.aspx");
                percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);
                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                gvLupaBanka.FilterExpression = String.Empty;
            }
        }
    }
}