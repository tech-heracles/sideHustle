using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;
using System.Collections.Specialized;
using PlatinumWeb.Templates;
using System.Globalization;
using System.Resources;
using System.Reflection;
using System.Configuration;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class RaportetAll : MyPageBase
    {



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
                //System.Threading.Thread.CurrentThread.CurrentCulture = ci;
                //System.Threading.Thread.CurrentThread.CurrentUICulture = ci;

                mbushSqlDatasource1();
                mbushSqlDataSourceRaportet();
                mbushSqlDataSourceModulet();
                EmratLabelave(ci);
                // Prevent caching, so can't be viewed offline
                //Response.Cache.SetCacheability(HttpCacheability.NoCache);
                DbCore.DbAdmin.clsPeriudhaKontabel periudha = DbCore.mySessionObjects.merrPeriudheKontabel(Session);
                dtdoknga.Value = periudha.FillimiPeriudha;
                dtdokderi.Value = periudha.MbarimiPeriudha;
                //if (CacheLayer.GlobalCacheManager.MySessionCache["LoggedIn"].Equals("No"))
                if (!DbCore.mySessionObjects.isLogedIn(Session))
                {
                    Response.Redirect(DbCore.IMBUtils.Paths.defaultLoginPath);
                }

                int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                //if (CacheLayer.GlobalCacheManager.MySessionCache["KodiNdermarrjes"] == null)
                if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
                {
                    Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerdoruesi);
                }
                int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                int idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);

                stmapmodules.LevelProperties[0].NodeTemplate = new MySiteMapFirstLevelTemplate();
                stmapmodules.LevelProperties[1].NodeTemplate = new MySiteMapLevelTemplate(ci);

                SiteMapDataSource dataSource = GenerateSiteMapHierarchy(idPerdoruesi, idViti, idNdermarrje, SqlDataSource1);
                dataSource.ShowStartingNode = false;
                stmapmodules.DataSource = dataSource;
                stmapmodules.DataBind();
            }
        }

        protected SiteMapDataSource GenerateSiteMapHierarchy(int idPerdoruesi, int idViti, int idNdermarrje, SqlDataSource dataSource)
        {
            SiteMapDataSource ret = new SiteMapDataSource();

            DataSourceSelectArguments arg = new DataSourceSelectArguments("ParentID");
            DataView dataView = dataSource.Select(arg) as DataView;
            DataTable table = dataView.Table;
            DataRow rowRootNode = table.Select("ParentID = 0")[0];

            UnboundSiteMapProvider provider = new UnboundSiteMapProvider(rowRootNode["NavigateURL"].ToString(), rowRootNode["Text"].ToString());
            AddNodeToProviderRecursive(idPerdoruesi, idViti, idNdermarrje, provider.RootNode, rowRootNode["ID"].ToString(), table, provider);
            ret.Provider = provider;
            return ret;
        }

        private void AddNodeToProviderRecursive(int idPerdoruesi, int idViti, int idNdermarrje, SiteMapNode parentNode, string parentID,
            DataTable table, UnboundSiteMapProvider provider)
        {

            DataRow[] childRows = table.Select("ParentID = " + parentID);
            foreach (DataRow row in childRows)
            {
                bool shfaq = true;
                if (row["IDRAP"].ToString() != "")
                {
                    DbCore.DbShare.clsRaporti rap = new DbCore.DbShare.clsRaporti(DbCore.mySessionObjects.ktheGjuhe(Session), int.Parse(row["IDRAP"].ToString()));
                    shfaq = DbCore.clsFunksione.teDrejta(idPerdoruesi, idViti, idNdermarrje, "Raportet.aspx?idmod=" + rap.IdModul);
                }

                if (shfaq)
                {
                    SiteMapNode childNode = CreateSiteMapNode(row, provider);
                    provider.AddSiteMapNode(childNode, parentNode);
                    AddNodeToProviderRecursive(idPerdoruesi, idViti, idNdermarrje, childNode, row["ID"].ToString(), table, provider);
                }
            }
        }

        private SiteMapNode CreateSiteMapNode(DataRow dataRow, UnboundSiteMapProvider provider)
        {
            System.Collections.Specialized.NameValueCollection attributes = new System.Collections.Specialized.NameValueCollection();
            //stmapmodules.NodeTemplate = new MySiteMapLevelTemplate(dataRow["NavigateURL"].ToString(), dataRow["Text"].ToString(), "javascript:filtroButtonClick(" + dataRow["IDRAP"].ToString() + ");");
            //attributes.Add("ImageURL", "~/images/filter2.png");
            //attributes.Add("ImageHeight", "22");
            //attributes.Add("ImageWidth", "22");
            attributes.Add("FilterUrl", "javascript:filtroButtonClick(" + dataRow["IDRAP"].ToString() + ");");
            return provider.CreateNode(dataRow["NavigateURL"].ToString(), dataRow["Text"].ToString(), dataRow["Text"].ToString(), null, attributes);
        }

        protected void btnHelp_Init(object sender, EventArgs e)
        {
            DbCore.DbAdmin.clsKomponente kompRap = new DbCore.DbAdmin.clsKomponente("RaportetAll.aspx");
            this.btnHelp.ClientSideEvents.Click = "function(s,e){openHelpWindow(s,e,\'" + DbCore.clsFunksione.ktheUrlHelpi(kompRap.UrlHelpSuffix).Item1 + "\');}";
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmratLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            listaRaporteve.Text = rm.GetString("labelListaeRaporteve", ci);
            lbld.Text = rm.GetString("labelFilterKryesorDtDokumenti", ci);
            ASPxRadioButtonList1.Items[0].Text = rm.GetString("RadioButtonListEditItemAktuale",ci);
            ASPxRadioButtonList1.Items[1].Text = rm.GetString("RadioButtonListEditItemPeriudha", ci);
            ASPxRadioButtonList1.Items[2].Text = rm.GetString("RadioButtonListEditItemVitiUshtrimor",ci);
            ASPxLabel1.Text = rm.GetString("labelRaportiNga", ci);
            ASPxLabel2.Text = rm.GetString("labelRaportDeri", ci);
            lblpersonalizuara.Text = rm.GetString("labelRaportetePersonalizuar", ci);
            btnHelp.ToolTip = rm.GetString("tooltipBtnHelp", ci);
            btnCollapseAll.ToolTip = rm.GetString("tooltipBtnCollapseAll", ci);
            btnExpandAll.ToolTip = rm.GetString("tooltipBtnExpandAll", ci);

        }

        private void mbushSqlDataSourceRaportet()
        {
            SqlDataSourceRaportet.ConnectionString = ConfigurationManager.ConnectionStrings["connStringAlpha"].ConnectionString;
            SqlDataSourceRaportet.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
            SqlDataSourceRaportet.SelectParameters.Clear();
            SqlDataSourceRaportet.SelectCommand = "prc_Titulli_RaportetAll";
            ControlParameter IDMODULI =
         new ControlParameter("IDMODULI", TypeCode.Int32, "cmbmodules", "Value");
            SqlDataSourceRaportet.SelectParameters.Add(IDMODULI);
            Parameter tmpParam = new Parameter("idgjuha", TypeCode.Int32, DbCore.mySessionObjects.ktheGjuhe(Session).ToString());
            tmpParam.Direction = ParameterDirection.Input;
            SqlDataSourceRaportet.SelectParameters.Add(tmpParam);
        }

        private void mbushSqlDatasource1()
        {
            SqlDataSource1.ConnectionString = ConfigurationManager.ConnectionStrings["connStringAlpha"].ConnectionString;
            SqlDataSource1.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
            SqlDataSource1.SelectParameters.Clear();
            SqlDataSource1.SelectCommand = "prc_RaportetAll_NodeSipasTeDrejtave";
            Parameter tmpParam = new Parameter("idndermarje", TypeCode.Int32, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session).ToString());
            tmpParam.Direction = ParameterDirection.Input;
            SqlDataSource1.SelectParameters.Add(tmpParam);
            tmpParam = new Parameter("idgjuha", TypeCode.Int32, DbCore.mySessionObjects.ktheGjuhe(Session).ToString());
            tmpParam.Direction = ParameterDirection.Input;
            SqlDataSource1.SelectParameters.Add(tmpParam);
            tmpParam = new Parameter("idperdorues", TypeCode.Int32, DbCore.mySessionObjects.ktheIdPerdoruesi(Session).ToString());
            tmpParam.Direction = ParameterDirection.Input;
            SqlDataSource1.SelectParameters.Add(tmpParam);
            tmpParam = new Parameter("idviti", TypeCode.Int32, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session).ToString());
            tmpParam.Direction = ParameterDirection.Input;
            SqlDataSource1.SelectParameters.Add(tmpParam);
        }

        private void mbushSqlDataSourceModulet()
        {
            SqlDataSourceModules.ConnectionString = ConfigurationManager.ConnectionStrings["connStringAlpha"].ConnectionString;
            SqlDataSourceModules.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
            SqlDataSourceModules.SelectParameters.Clear();
            SqlDataSourceModules.SelectCommand = "prc_T_MODULI_merrGjitheModuleteRaporteve";
            Parameter tmpParam = new Parameter("idgjuha", TypeCode.Int32, DbCore.mySessionObjects.ktheGjuhe(Session).ToString());
            tmpParam.Direction = ParameterDirection.Input;
            SqlDataSourceModules.SelectParameters.Add(tmpParam);
        }
    }
}