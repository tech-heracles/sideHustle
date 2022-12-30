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

namespace PlatinumWeb
{
    public partial class GISLupaObjekte : System.Web.UI.Page
    {
        private int idperdoruesi, idnderviti, idNdermarrje, idviti, idgjuha;
        private const int idstatusdok = 1;
        private string komponente = "GISLupaObjekte.aspx";
        private string guidString;
        private int idkonfigurimi;



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
                return;
            }
            idperdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idperdoruesi);
                return;
            }
            idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            idviti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            idnderviti = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
            idgjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            idkonfigurimi = DbCore.clsFunksione.getIdKonfigAmbLupa(vleraQueryString, idNdermarrje, "GIS/LO");
            if (!Page.IsPostBack)
            {
                guidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                hfState.Set("guidString", guidString);
                percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), idperdoruesi, idNdermarrje);
                konfiguroVleraFillestare(idperdoruesi, idNdermarrje, idgjuha, rm, cultinf);
                mbushGridNgaDB(idNdermarrje, idgjuha, idnderviti);
                konfiguroGride(idperdoruesi, idNdermarrje, idkonfigurimi);


            }
            else
            {
                mbushGridNgaSession(idNdermarrje, idnderviti);
                hfState.Get("guidString");
                konfiguroGride(idperdoruesi, idNdermarrje, idkonfigurimi);
            }
            gvLupaObjekteGIS.Columns["#"].VisibleIndex = 0;
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
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, "GISLupaObjekte.aspx", this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
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

        protected void gvLupaObjekteGIS_DataBound(object sender, EventArgs e)
        {

            if (gvLupaObjekteGIS.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per 
                //perzgjidh

                GridViewCommandColumn check = new GridViewCommandColumn("#") { ShowSelectCheckbox = true, Width = System.Web.UI.WebControls.Unit.Percentage(4) };
                //behet per te afishuar rreshtin qe do sherbej per filtrim
                gvLupaObjekteGIS.Settings.ShowFilterRow = true;
                gvLupaObjekteGIS.Settings.ShowFilterRowMenu = true;
                gvLupaObjekteGIS.Columns.Add(check);
                gvLupaObjekteGIS.KeyFieldName = "gid";
                gvLupaObjekteGIS.SettingsBehavior.AllowSelectByRowClick = true;
                gvLupaObjekteGIS.SettingsBehavior.AllowFocusedRow = true;

            }

        }

        private void konfiguroGride(int idPerdoruesi, int idNdermarrje, int idKonfigAmbjenti)
        {
            //konfigurimet e grides
            GridUtil.konfigGrideListeEMadhePaTheme(gvLupaObjekteGIS, "gid");
            GridUtil.percaktoVisibleColumnsSipasKonfigurimit(gvLupaObjekteGIS, "gvLupaObjekteGIS", "GISLupaObjekte.aspx", idKonfigAmbjenti, true, DbCore.mySessionObjects.ktheGjuhe(Session));

        }


        protected void gvLupaObjekteGIS_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
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
        private void mbushGridNgaSession(int idNdermarrje, int idnderviti)
        {
            DataTable tmpObject = DbCore.mySessionObjects.MerrNgaSession<DataTable>(Session, komponente);
            if (tmpObject == null)
                mbushGridNgaDB(idNdermarrje, idperdoruesi, idnderviti);
            else
            {
                gvLupaObjekteGIS.DataSource = tmpObject;
                gvLupaObjekteGIS.DataBind();
                tmpObject.Dispose();
            }
        }

        /// <summary>
        /// mbush griden nga databaza
        /// </summary>
        /// <param name="idNdermarrje">id e ndermarjes</param>
        private void mbushGridNgaDB(int idNdermarrje, int idgjuha, int idnderviti)
        {
            //mbush griden e popupit me te dhena         

            DataTable dt = DbCore.DbGIS.colLidhjeObjekteWebGis.ktheGjitheObjektetGIS(idNdermarrje, idnderviti, idgjuha);
            DbCore.mySessionObjects.RuajNeSession(Session, dt, komponente);
            gvLupaObjekteGIS.DataSource = dt;
            gvLupaObjekteGIS.DataBind();
            dt.Dispose();
        }
        /// <summary>
        /// perdoret per te trajtuar ngjarjet e butonave te menuse. 
        /// </summary>
        /// <param name="source"> derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e) { }

        private void konfiguroVleraFillestare(int idPerdoruesi, int idNdermarrje, int idGjuha, ResourceManager rm, CultureInfo cultinf)
        {

        }

        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e) { }

        public void FshiFilter_ASPxButton_Click(object sender, EventArgs e) { }

        protected void gvLupaModele_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e) { }

        protected void gvLupaModele_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e) { }
    }
}
