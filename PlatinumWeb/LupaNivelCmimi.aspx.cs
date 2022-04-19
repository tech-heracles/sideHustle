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
using DbCore.DbShare;
using System.Data;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class LupaNivelCmimi : MyPageBase
    {
        public static int idNdermVit = -1;
        int idPerdoruesi;
        int idGjuha;
        int idNdermarrje;
        int idViti;
        int idNdermarrjeVit;
        DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();
        System.Globalization.CultureInfo ci;
        System.Resources.ResourceManager rm;
        string guidString;
        private const string komponente = "LupaNivelCmimi.aspx";

        //private int idKonfigambjenti;

        protected void Page_Load(object sender, EventArgs e)
        {
           
            ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            if (hfState.Count == 0)
            {
                if (!DbCore.mySessionObjects.isLogedIn(Session))
                {
                    DbCore.clsFunksione.logout(Session,true,"FaqePaautorizuar");
                }
                guidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
                idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
                idNdermarrjeVit = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
                hfState.Set("idPerdoruesi", idPerdoruesi);
                hfState.Set("idGjuha", idGjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("idViti", idViti);
                hfState.Set("idNdermarrjeVit", idNdermarrjeVit);
                hfState.Set("guidString", guidString);
                hfState.Set("Meme", DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
            }
            else
            {
                idPerdoruesi = (int)hfState["idPerdoruesi"];
                idGjuha = (int)hfState["idGjuha"];
                idNdermarrje = (int)hfState["idNdermarrje"];
                idViti = (int)hfState["idViti"];
                idNdermarrjeVit = (int)hfState["idNdermarrjeVit"];
                guidString = (string)hfState["guidString"];
            }
            if (!IsPostBack)
            {
                percaktoTemplateMenu(ASPxMenu1, idViti, idPerdoruesi, idNdermarrje);
                string vleraQueryString = "";
                if (Request.QueryString["idKonfigAmbjente"] != null && Request.QueryString["idKonfigAmbjente"] != "")
                    vleraQueryString = Request.QueryString["idKonfigAmbjente"].ToString();
                int idKonfigambjenti = DbCore.clsFunksione.getIdKonfigAmbLupa(vleraQueryString, idNdermarrje, "NivCm");
                if (clsAlternativaKushti.getAlternativa(idKonfigambjenti, "KSSH") == "Po")
                    hfState.Add("KSSH", true);
                else hfState.Add("KSSH", false);
                if (DbCore.DbShare.clsAlternativaKushti.getAlternativa(idKonfigambjenti, "ES") == "Po")
                    hfState.Add("ES", true);
                else hfState.Add("ES", false);
                hfState.Set("idKonfigambjenti", idKonfigambjenti);
                cmbKonfigurimi.Value = idKonfigambjenti.ToString();
                GridUtil.AplikoFilterDefault(gvLupaNivCm, idKonfigambjenti);
                mbushPopUpListeNgaDB(idNdermarrje, idPerdoruesi);
                konfiguroPopupGride();
                GridUtil.percaktoVisibleColumnsSipasKonfigurimit(gvLupaNivCm, "gvLupaNivCm", komponente, idKonfigambjenti, true, DbCore.mySessionObjects.ktheGjuhe(Session));
                clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idNdermarrje, "gvLupaNivCm", idKonfigambjenti, komponente);
            }
            else
            {
                if (!IsCallback || (IsCallback && Request["__CALLBACKID"].ToString().Contains("ASPxMenu1")))
                    percaktoTemplateMenu(ASPxMenu1, idViti, idPerdoruesi, idNdermarrje);

                if (!IsCallback || (IsCallback && Request["__CALLBACKID"].ToString().Contains("gvLupaNivCm")))
                {
                    mbushPopUpListeNgaSession(idNdermarrje, idPerdoruesi);
                    konfiguroPopupGride();
                }
            }
            GridUtil.KonfiguroGrideListeMadhePopupiPaTheme(gvLupaNivCm, "IdNivelCmimi", (bool)hfState["KSSH"], (bool)hfState["ES"]);

           
        }
        private void mbushPopUpListeNgaSession(int idNdermarrje, int idperd)
        {
            Object tmpObject;
            DbCore.mySessionObjects.merrGrideNgaSessioniLupa(Session, out tmpObject);
            if (tmpObject == null)
                mbushPopUpListeNgaDB(idNdermarrje, idperd);
            else
            {
                gvLupaNivCm.DataSource = tmpObject;
                gvLupaNivCm.DataBind();
            }
        }

        private void mbushPopUpListeNgaDB(int idNdermarrje, int idperd)
        {//mbush griden e popupit me te dhena
            DataTable dt = new DataTable();
            if (Request.QueryString["lloji"] != null)
                dt = DbCore.DbInventari.colNiveleCmimesh.merrNiveleNdermarjeDTBlerjeShitje(idNdermarrje, int.Parse(Request.QueryString["lloji"]), idperd);
            else dt = DbCore.DbInventari.colNiveleCmimesh.merrNiveleNdermarjeDT(idNdermarrje, idperd);
            DbCore.mySessionObjects.ruajGrideNeSessionLupa(Session, dt);
            gvLupaNivCm.DataSource = dt;
            gvLupaNivCm.DataBind();
            dt.Dispose();
        }

        private void konfiguroPopupGride()
        {//konfiguron popupgriden
            KonfigurimComboGride.shtoPrioritet(gvLupaNivCm, rm, ci, "PrioritetiNivelCmimi");
            KonfigurimComboGride.shtoLlojNivelCmimi(gvLupaNivCm, rm, ci);
            KonfigurimComboGride.shtoPrindSipasNivelCmimi(gvLupaNivCm, idNdermarrje, Session, komponente, guidString);
            KonfigurimComboGride.ShtoMonedhe(gvLupaNivCm, idNdermarrje, idPerdoruesi, Session, komponente, guidString);
            KonfigurimComboGride.shtoBrutoNeto(gvLupaNivCm, rm, ci);
            KonfigurimComboGride.shtoDetajim(gvLupaNivCm);
        }

     

        protected void gvLupaNivCm_DataBound(object sender, EventArgs e)
        {
            //perdoret per ti vene disa atribute grides
            //behet per te afishuar rreshtin qe do sherbej per filtrim
            gvLupaNivCm.Settings.ShowFilterRow = true;
            gvLupaNivCm.KeyFieldName = "IdNivelCmimi";
            gvLupaNivCm.SettingsBehavior.AllowSelectByRowClick = true;
        }

        protected void gvLupaNivCm_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            //kur popupgrida ben callback
            gvLupaNivCm.Selection.UnselectAll();
        }

    
        protected void gvLupaNivCm_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            int idPerdoruesi = (int)hfState["idPerdoruesi"];
            int idGjuha = (int)hfState["idGjuha"];
            int idNdermarrje = (int)hfState["idNdermarrje"];

            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                if (arr[2] == "")
                    gvLupaNivCm.FilterExpression = "";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuha, "gvLupaNivCm", komponente, idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], idNdermarrje, koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        gvLupaNivCm.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvLupaNivCm);
                    }
                }
            }
            gvLupaNivCm.Selection.UnselectAll();
        }

        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(ASPxMenu1, (int)hfState["idViti"], (int)hfState["idPerdoruesi"], (int)hfState["idNdermarrje"]);
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
            clsToolbarConfig.percaktoTemplateMenu(idGjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1, komponente, this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, true, false, false, meme);
            aSPxMenu1.Items.FindByName("Anullo").Text = rm.GetString("MenuItemMbyll", ci);
        }

        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje
            int idPerdoruesi = (int)hfState["idPerdoruesi"];
            int idGjuha = (int)hfState["idGjuha"];
            int idNdermarrje = (int)hfState["idNdermarrje"];
            int idViti = (int)hfState["idViti"];
            clsMenuInfo.ShtoMenuItemInfo(this, MenuInfo);
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            DbCore.DbAdmin.clsFiltraGrida filtri = new DbCore.DbAdmin.clsFiltraGrida();
            filtri.FiltraKodi = cmbFiltra.Text;
            filtri.FiltraShenime = cmbFiltra.Text;
            filtri.FiltraUniversal = false;// Universal_ASPxCheckBox.Checked;
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuha, "gvLupaNivCm", komponente, idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gvLupaNivCm.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("IdNivelCmimi", gvLupaNivCm);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvLupaNivCm.GetSortedColumns();
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
            //    filtri.KoloneRenditje = "IdNivelCmimi";
            //    filtri.DrejtimRenditje = true;
            //}
            filtri.IdPerdoruesi = idPerdoruesi;
            filtri.IdNdermarje = idNdermarrje;
            filtri.IdStatusDok = 1;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            mesazh = filtri.ruaj();
            clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idNdermarrje, "gvLupaNivCm", Convert.ToInt32(cmbKonfigurimi.Value), komponente);
            percaktoTemplateMenu(ASPxMenu1, idViti, idPerdoruesi, idNdermarrje);
            if (mesazh.Status == true)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            cmbFiltra.Text = "";
        }

        public void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje
            int idGjuha = (int)hfState["idGjuha"];
            int idPerdoruesi = (int)hfState["idPerdoruesi"];
            int idViti = (int)hfState["idViti"];
            int idNdermarrje = (int)hfState["idNdermarrje"];
            clsMenuInfo.ShtoMenuItemInfo(this, MenuInfo);
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuha, "gvLupaNivCm", komponente, idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = idPerdoruesi;
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();
                clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idNdermarrje, "gvLupaNivCm", Convert.ToInt32(cmbKonfigurimi.Value), komponente);
                percaktoTemplateMenu(ASPxMenu1, idViti, idPerdoruesi, idNdermarrje);
                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                cmbFiltra.Text = "";
                gvLupaNivCm.FilterExpression = String.Empty;
            }
        }

    }
}