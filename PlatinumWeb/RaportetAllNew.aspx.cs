using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Collections.Specialized;
using DbCore.DbShare;
using System.Resources;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class RaportetAllNew : MyPageBase
    {


   

        protected void Page_Load(object sender, EventArgs e)
        {


            int width = Request.Browser.ScreenPixelsWidth;// !string.IsNullOrWhiteSpace(Request.QueryString["width"])?Convert.ToInt32(Request["width"]) : Request.Browser.ScreenPixelsWidth;
            if (width > 0)
            {

                int cols = width / 300;
                sitemap.ColumnCount = Convert.ToByte(cols > 4 ? 4 : cols);


            }

            DbCore.DbShare.clsDatabaseShare db = new clsDatabaseShare();
            DbCore.DbAdmin.clsPeriudhaKontabel periudha = DbCore.mySessionObjects.merrPeriudheKontabel(Session);
            dtdoknga.Value = periudha.FillimiPeriudha;
            dtdokderi.Value = periudha.MbarimiPeriudha;

            DataTable dt = db.merrSiteMap(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.ktheIdVitNdermarrje(Session));

            SiteMapDataSource dataSource = GenerateSiteMapHierarchy(dt);
            dataSource.ShowStartingNode = false;

            sitemap.DataSource = dataSource;
            sitemap.DataBind();
            EmratLabelave();
        }
        protected SiteMapDataSource GenerateSiteMapHierarchy(DataTable dataSource)
        {
            SiteMapDataSource ret = new SiteMapDataSource();
            DataTable table = dataSource.Columns["ParentID"].Table;
            DataRow rowRootNode = table.Select("ParentID = 0")[0];
            UnboundSiteMapProvider provider = new UnboundSiteMapProvider(rowRootNode["NavigateURL"].ToString(), rowRootNode["Text"].ToString());
            AddNodeToProviderRecursive(provider.RootNode, rowRootNode["ID"].ToString(), table, provider);
            ret.Provider = provider;
            return ret;
        }

        private void AddNodeToProviderRecursive(SiteMapNode parentNode, string parentID,
            DataTable table, UnboundSiteMapProvider provider)
        {

            DataRow[] childRows = table.Select("ParentID = " + parentID);
            foreach (DataRow row in childRows)
            {
                SiteMapNode childNode = CreateSiteMapNode(row, provider);
                provider.AddSiteMapNode(childNode, parentNode);
                AddNodeToProviderRecursive(childNode, row["ID"].ToString(), table, provider);
            }
        }
        private SiteMapNode CreateSiteMapNode(DataRow dataRow, UnboundSiteMapProvider provider)
        {


          //  System.Collections.Specialized.NameValueCollection attributes = new System.Collections.Specialized.NameValueCollection();
           
          //  attributes.Add("FilterUrl", "javascript:filtroButtonClick(" + dataRow["IDRAP"].ToString() + ");");
            //return provider.CreateNode(dataRow["NavigateURL"].ToString(), dataRow["Text"].ToString(), dataRow["Text"].ToString(), null, attributes);

            return provider.CreateNode(dataRow["NavigateURL"].ToString(), dataRow["Text"].ToString(), "", null, new NameValueCollection());
        }
        protected void btnHelp_Init(object sender, EventArgs e)
        {
            DbCore.DbAdmin.clsKomponente kompRap = new DbCore.DbAdmin.clsKomponente("RaportetAllNew.aspx");
            this.btnHelp.ClientSideEvents.Click = "function(s,e){openHelpWindow(s,e,\'" + DbCore.clsFunksione.ktheUrlHelpi(kompRap.UrlHelpSuffix).Item1 + "\');}";
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmratLabelave( )
        {
             System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            listaRaporteve.Text = rm.GetString("labelListaeRaporteve", ci);
            lbld.Text = rm.GetString("labelFilterKryesorDtDokumenti", ci);
            ASPxRadioButtonList1.Items[0].Text = rm.GetString("RadioButtonListEditItemAktuale", ci);
            ASPxRadioButtonList1.Items[1].Text = rm.GetString("RadioButtonListEditItemPeriudha", ci);
            ASPxRadioButtonList1.Items[2].Text = rm.GetString("RadioButtonListEditItemVitiUshtrimor", ci);
            ASPxLabel1.Text = rm.GetString("labelRaportiNga", ci);
            ASPxLabel2.Text = rm.GetString("labelRaportDeri", ci);
            lblpersonalizuara.Text = rm.GetString("labelRaportetePersonalizuar", ci);
            btnHelp.ToolTip = rm.GetString("tooltipBtnHelp", ci);
            btnCollapseAll.ToolTip = rm.GetString("tooltipBtnCollapseAll", ci);
            btnExpandAll.ToolTip = rm.GetString("tooltipBtnExpandAll", ci);

        }

        protected void sitemap_DataBound(object sender, EventArgs e)
        {
            ASPxSiteMapControl sitemap = sender as ASPxSiteMapControl;
            
            
        }
    }
}