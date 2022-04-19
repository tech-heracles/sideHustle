using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using DevExpress.Web;
using System.Data;
using PlatinumWeb.Templates;
using System.Web.Script.Serialization;
using System.Globalization;
using System.Resources;
using DbCore.DbAdmin;
using Newtonsoft.Json;
using DbCore;
using DbCore.DbShare;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;

namespace PlatinumWeb
{
    public partial class LupaModelFushaShtese : System.Web.UI.Page
    {
        private int idperdoruesi, idnderviti, idNdermarrje, idviti, idgjuha;
        private const int idstatusdok = 1;
        private string komponente = "LupaModelFushaShtese.aspx";
        private string guidString;
        private int idkonfigurimi;

        private const string gabim2aktivitetenegride = "GABIM: Ndodhen 2 modele me te njejten id ne gride";

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["idTheme"]))
                DbCore.clsFunksione.percaktoThemeAmbjenteDheJQueryMeId(Page, Convert.ToInt32(Request.QueryString["idTheme"]));
            else
                DbCore.clsFunksione.percaktoThemeAmbjenteDheJQuery(Page, DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
        }

        protected void Page_Load(object sender, EventArgs e)
        {//kontrollon nese perdoruesi eshte i loguar
            string vleraQueryString; 
            if (Request.QueryString["idKonfigAmbjente"] != null && Request.QueryString["idKonfigAmbjente"] != "")
                vleraQueryString = Request.QueryString["idKonfigAmbjente"].ToString();
            else
                vleraQueryString = "";
       
            var cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            var rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                Response.Redirect(DbCore.IMBUtils.Paths.defaultLoginPath);
            }
            idperdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idperdoruesi);
            }
            idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            idviti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            idnderviti = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
            idgjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            idkonfigurimi = DbCore.clsFunksione.getIdKonfigAmbLupa(vleraQueryString, idNdermarrje, "LMFSH");
            if (!Page.IsPostBack)
            {
                guidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                hfState.Set("guidString", guidString);
                hfState.Set("roundPanelZgjidhModelin", rm.GetString("roundPanelZgjidhModelin", cultinf));
                percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), idperdoruesi, idNdermarrje);
                konfiguroVleraFillestare(idperdoruesi, idNdermarrje, idgjuha, rm, cultinf);
                mbushGridNgaDB(idNdermarrje, idperdoruesi);
                konfiguroGride(idperdoruesi, idNdermarrje, idkonfigurimi);
               

            }
            else
            {
                mbushGridNgaSession(idNdermarrje);
                hfState.Get("guidString");
                hfState.Set("roundPanelZgjidhModelin", rm.GetString("roundPanelZgjidhModelin", cultinf));
                konfiguroGride(idperdoruesi, idNdermarrje, idkonfigurimi);
            }

            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), idperdoruesi, idNdermarrje);

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
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, "LupaModelFushaShtese.aspx", this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            aSPxMenu1.Items.FindByName("Anullo").Text = rm.GetString("MenuItemMbyll", ci);
        }

        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), idperdoruesi, idNdermarrje);

        }

        protected void gvLupaModele_DataBound(object sender, EventArgs e)
        {

            if (gvLupaModele.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per 
                //perzgjidh

                GridViewCommandColumn check = new GridViewCommandColumn("#") { ShowSelectCheckbox = true, Width = System.Web.UI.WebControls.Unit.Percentage(5), VisibleIndex = 0 };
                //behet per te afishuar rreshtin qe do sherbej per filtrim
                gvLupaModele.Settings.ShowFilterRow = true;
                gvLupaModele.Settings.ShowFilterRowMenu = true;
                gvLupaModele.Columns.Add(check);
                gvLupaModele.KeyFieldName = "IdModeliFushaShtese";
                gvLupaModele.SettingsBehavior.AllowSelectByRowClick = true;
                gvLupaModele.SettingsBehavior.AllowFocusedRow = true;

            }

        }

        private void konfiguroGride(int idPerdoruesi, int idNdermarrje, int idKonfigAmbjenti)
        {
            //konfigurimet e grides

            KonfigurimComboGride.shtoLlojModeliFushaShtese(gvLupaModele, Session, komponente, guidString);
            GridUtil.konfigGrideListeEMadhePaTheme(gvLupaModele, "IdModeliFushaShtese");
            GridUtil.percaktoVisibleColumnsSipasKonfigurimit(gvLupaModele, "gvLupaModele", "LupaModelFushaShtese.aspx", idKonfigAmbjenti, false, DbCore.mySessionObjects.ktheGjuhe(Session));
            if (clsAlternativaKushti.getAlternativa(idKonfigAmbjenti, "ES") == "Po")
                gvLupaModele.SettingsPager.Mode = GridViewPagerMode.EndlessPaging;
        }


        protected void gvLupaModele_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            if (e.Column.FieldName == "Pershkrimi" || e.Column.FieldName == "Kodi")
            {
                e.Values.Clear();
                e.AddValue("(Te gjithe)", string.Empty, "true");
                e.AddValue("Nga A-D ", string.Empty, String.Format("{0}>'A     ' and {0}<'DDDDDDD'", e.Column.FieldName));
                e.AddValue("Nga D-G ", string.Empty, String.Format("{0}>'D     ' and {0}<'GGGGGGG'", e.Column.FieldName));
                e.AddValue("Nga H-K ", string.Empty, String.Format("{0}>'H     ' and {0}<'KKKKKKK'", e.Column.FieldName));
                e.AddValue("Nga L-O ", string.Empty, String.Format("{0}>'L     ' and {0}<'OOOOOOO'", e.Column.FieldName));
                e.AddValue("Nga P-S ", string.Empty, String.Format("{0}>'P     ' and {0}<'SSSSSSS'", e.Column.FieldName));
                e.AddValue("Nga T-W ", string.Empty, String.Format("{0}>'T     ' and {0}<'WWWWWWW'", e.Column.FieldName));
                e.AddValue("Nga X-Z ", string.Empty, String.Format("{0}>'X     ' and {0}<'ZZZZZZZ'", e.Column.FieldName));
            }
            else
            {
                e.Values.Clear();
                e.AddValue("(Te gjithe)", string.Empty, "true");
            }
        }

        /// <summary>
        /// mbush griden nga sesioni
        /// </summary>
        /// <param name="idNdermarrje">id e ndermarjes</param>
        private void mbushGridNgaSession(int idNdermarrje)
        {
            DataTable tmpObject = DbCore.mySessionObjects.MerrNgaSession<DataTable>(Session, komponente);
            if (tmpObject == null)
                mbushGridNgaDB(idNdermarrje, idperdoruesi);
            else
            {
                gvLupaModele.DataSource = tmpObject;
                gvLupaModele.DataBind();
                tmpObject.Dispose();
            }
        }

        /// <summary>
        /// mbush griden nga databaza
        /// </summary>
        /// <param name="idNdermarrje">id e ndermarjes</param>
        private void mbushGridNgaDB(int idNdermarrje, int idperdoruesi)
        {
            //mbush griden e popupit me te dhena         

            DataTable dt = DbCore.DbAdmin.colModeletFushaShtese.ktheGjitheModeletFushaShteseDT(idNdermarrje, idperdoruesi);
            DbCore.mySessionObjects.RuajNeSession(Session, dt, komponente);
            gvLupaModele.DataSource = dt;
            gvLupaModele.DataBind();
            dt.Dispose();
        }
        /// <summary>
        /// perdoret per te trajtuar ngjarjet e butonave te menuse. 
        /// </summary>
        /// <param name="source"> derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e) { }

        private void konfiguroVleraFillestare(int idPerdoruesi, int idNdermarrje, int idGjuha, ResourceManager rm, CultureInfo cultinf) {
     
        }

        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e) { }

        public void FshiFilter_ASPxButton_Click(object sender, EventArgs e) { }

        protected void gvLupaModele_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e) { }

        protected void gvLupaModele_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e) { }
    }
}
