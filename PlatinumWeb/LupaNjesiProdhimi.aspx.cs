using DbCore.DbRegjistrim;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DbCore.IMBUtils.Types;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.DbShare;

namespace PlatinumWeb
{
    public partial class LupaNjesiProdhimi : MyPageBase
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            int idPerdoruesi;
            int idGjuha;
            int idNdermarrje;
            int idViti;
            int idNdermarrjeVit;
            int idKonfigambjenti;
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
            if (!IsPostBack)
            {
                percaktoTemplateMenu(ASPxMenu1, idViti, idPerdoruesi, idNdermarrje);
                string vleraQueryString = "";
                if (Request.QueryString["idKonfigAmbjente"] != null && Request.QueryString["idKonfigAmbjente"] != "")
                    vleraQueryString = Request.QueryString["idKonfigAmbjente"].ToString();
                int idNivel = clsNivelRegjistrimi.ktheIdNivelRegjistrimiSipasKodi("LupaNjPr", idNdermarrje);
                if (vleraQueryString != "")
                {
                    //ketu me intereson id e nivelit
                    //ne rastin kur kemi disa konfigurime te lupes per kontrollin i cili gjeneroi thirrjen e kesaj lupe
                    //duhet te gjejme cili nga konfigurimet e ka idNivel sa niveli i artikujve
                    //i kontrollojme me radhe te gjitha konfigurimet qe i jane kaluar ne query string
                    string[] idte = vleraQueryString.Split('-');
                    if (idte.Length > 1)
                    {
                        //DbCore.DbShare.clsKonfigurimAmbjenti konfigLupa;
                        for (int i = 0; i < idte.Length; i++)
                        {
                            //konfigLupa = new DbCore.DbShare.clsKonfigurimAmbjenti(Convert.ToInt32(idte[i]));
                            if (DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdNivel(Convert.ToInt32(idte[i])) == idNivel)
                            {
                                idKonfigambjenti = Convert.ToInt32(idte[i]);
                                break;
                            }
                        }
                        idKonfigambjenti = merrKonfiguriminDefaultTeLupes(idNdermarrje, idNivel);
                    }
                    else
                        if (idte.Length == 1)
                        {
                            idKonfigambjenti = Convert.ToInt32(vleraQueryString);
                            if (idKonfigambjenti == 0 || idKonfigambjenti == -1)
                                idKonfigambjenti = merrKonfiguriminDefaultTeLupes(idNdermarrje, idNivel);
                        }
                        else
                            idKonfigambjenti = merrKonfiguriminDefaultTeLupes(0, idNivel);
                }
                else
                    idKonfigambjenti = merrKonfiguriminDefaultTeLupes(idNdermarrje, idNivel);
                cmbKonfigurimi.Value = idKonfigambjenti.ToString();
                GridUtil.AplikoFilterDefault(gvLupaNjesiProdhimi, idKonfigambjenti);
                clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idNdermarrje, "gvLupaNjesiProdhimi", idKonfigambjenti, "LupaNjesiProdhimi.aspx");
                hfState.Set("idKonfigambjenti", idKonfigambjenti);
                if (DbCore.DbShare.clsAlternativaKushti.getAlternativa(idKonfigambjenti, "KSSH") == "Po")
                    hfState.Add("KSSH", true);
                else hfState.Add("KSSH", false);
                if (DbCore.DbShare.clsAlternativaKushti.getAlternativa(idKonfigambjenti, "ES") == "Po")
                    hfState.Add("ES", true);
                else hfState.Add("ES", false);
                mbushPopUpListeNjesiProdhimiNgaDB(idNdermarrje);
                konfiguroPopupGride();
                GridUtil.percaktoVisibleColumnsSipasKonfigurimit(gvLupaNjesiProdhimi, "gvLupaNjesiProdhimi", "LupaNjesiProdhimi.aspx", idKonfigambjenti, true, DbCore.mySessionObjects.ktheGjuhe(Session));
            }
            else
            {
                if (!IsCallback || (IsCallback && Request["__CALLBACKID"].ToString().Contains("ASPxMenu1")))
                    percaktoTemplateMenu(ASPxMenu1, idViti, idPerdoruesi, idNdermarrje);

                if (!IsCallback || (IsCallback && Request["__CALLBACKID"].ToString().Contains("gvLupaNjesiProdhimi")))
                {
                    idKonfigambjenti = (int)hfState["idKonfigambjenti"];
                    mbushPopUpListeNjesiProdhimiNgaSession(idNdermarrje);
                    konfiguroPopupGride();
                }
            }
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            GridUtil.KonfiguroGrideListeMadhePopupiPaTheme(gvLupaNjesiProdhimi, "IdNjesiProdhimi", (bool)hfState["KSSH"], (bool)hfState["ES"]);
        }

        private void mbushPopUpListeNjesiProdhimiNgaDB(int idNdermarrje)
        {//mbush griden e popupit me te dhena
            DataTable dt = DbCore.DbRegjistrim.colNjesiProdhimi.merrNjesiProdhimiSipasNdermarrjeAktiveDt(idNdermarrje);            
            DbCore.mySessionObjects.ruajGrideNeSessionLupa(Session, dt);
            gvLupaNjesiProdhimi.DataSource = dt;
            gvLupaNjesiProdhimi.DataBind();
            dt.Dispose();
        }

        private void mbushPopUpListeNjesiProdhimiNgaSession(int idNdermarrje)
        {
            Object tmpObject;
            DbCore.mySessionObjects.merrGrideNgaSessioniLupa(Session, out tmpObject);
            if (tmpObject == null)
                mbushPopUpListeNjesiProdhimiNgaDB(idNdermarrje);
            else
            {
                gvLupaNjesiProdhimi.DataSource = tmpObject;
                gvLupaNjesiProdhimi.DataBind();
            }
        }

        private void konfiguroPopupGride()
        {//konfiguron popupgriden            
            //gvLupaNjesiProdhimi.Columns["#"].VisibleIndex = 0;
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
            clsToolbarConfig.percaktoTemplateMenu(idGjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1, "LupaNjesiProdhimi.aspx", this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, true, false, false, meme);
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
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaNjesiProdhimi", "LupaNjesiProdhimi.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gvLupaNjesiProdhimi.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("IdNjesiProdhimi", gvLupaNjesiProdhimi);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvLupaNjesiProdhimi.GetSortedColumns();
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
            //    filtri.KoloneRenditje = "IdNjesiProdhimi";
            //    filtri.DrejtimRenditje = true;
            //}
            filtri.IdPerdoruesi = idPerdoruesi;
            filtri.IdNdermarje = idNdermarrje;
            filtri.IdStatusDok = 1;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            mesazh = filtri.ruaj();
            percaktoTemplateMenu(ASPxMenu1, idViti, idPerdoruesi, idNdermarrje);
            //mbushComboBoxFiltra(idNdermarrje);
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaNjesiProdhimi", Convert.ToInt32(cmbKonfigurimi.Value), "LupaNjesiProdhimi.aspx");
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
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaNjesiProdhimi", "LupaNjesiProdhimi.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
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
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaNjesiProdhimi", Convert.ToInt32(cmbKonfigurimi.Value), "LupaNjesiProdhimi.aspx");
                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                gvLupaNjesiProdhimi.FilterExpression = String.Empty;
            }
        }

        protected void gvLupaNjesiProdhimi_DataBound(object sender, EventArgs e)
        {
            //perdoret per ti vene disa atribute grides
            //behet per te afishuar rreshtin qe do sherbej per filtrim
            gvLupaNjesiProdhimi.Settings.ShowFilterRow = true;
            gvLupaNjesiProdhimi.KeyFieldName = "IdNjesiProdhimi";
            gvLupaNjesiProdhimi.SettingsBehavior.AllowSelectByRowClick = true;
        }

        protected void gvLupaNjesiProdhimi_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            gvLupaNjesiProdhimi.Selection.UnselectAll();
        }

        protected void gvLupaNjesiProdhimi_CustomJSProperties(object sender, DevExpress.Web.ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gvLupaNjesiProdhimi.PageIndex;
            e.Properties["cpPageRow"] = gvLupaNjesiProdhimi.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gvLupaNjesiProdhimi.VisibleRowCount;
        }

        protected void gvLupaNjesiProdhimi_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            int idNdermarrje = (int)hfState["idNdermarrje"];
            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                if (arr[2] == "")
                    gvLupaNjesiProdhimi.FilterExpression = "";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaNjesiProdhimi", "LupaNjesiProdhimi.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], idNdermarrje, koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        gvLupaNjesiProdhimi.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvLupaNjesiProdhimi);
                    }
                }
            }
            gvLupaNjesiProdhimi.Selection.UnselectAll();
        }

        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            int idPerdoruesi = (int)hfState["idPerdoruesi"];
            int idNdermarrje = (int)hfState["idNdermarrje"];
            int idViti = (int)hfState["idViti"];
            percaktoTemplateMenu(ASPxMenu1, idViti, idPerdoruesi, idNdermarrje);
        }

        protected void gvLupaNjesiProdhimi_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if (e.Column.FieldName == "IdDegeAdministrative")
                if (Converter.ConvertToInt(e.Value)==0)
                {
                    e.Criteria = null;
                }
        }
    }
}