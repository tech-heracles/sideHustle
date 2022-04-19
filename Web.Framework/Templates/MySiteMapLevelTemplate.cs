using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using DevExpress.Web;
using System.Web.UI.WebControls;
using System.Resources;
using System.Reflection;
using System.Configuration;
using System.Globalization;



namespace PlatinumWeb.Templates
{
    public class MySiteMapLevelTemplate : ITemplate
    {
        private CultureInfo ci;
        public MySiteMapLevelTemplate(CultureInfo ci)
        {
            this.ci = ci;
        }
        public void InstantiateIn(Control Container)
        {

                ResourceManager rm = new ResourceManager("Resources.Strings",
                         System.Reflection.Assembly.Load("App_GlobalResources"));
          

            NodeTemplateContainer nodeContainer = (NodeTemplateContainer)Container;
            SiteMapNode nyje = (SiteMapNode)nodeContainer.DataItem;
            ASPxHyperLink link1 = new ASPxHyperLink();
           link1.ID = "ASPxHyperLink1";
           link1.ToolTip = rm.GetString("tooltipFiltroRaport", ci);
            link1.ImageUrl = "~/images/filter2.png";
            link1.ImageHeight = 22;
            link1.ImageWidth = 22;
            link1.EnableTheming = false;
            link1.NavigateUrl = nyje["FilterUrl"];

            ASPxHyperLink link2 = new ASPxHyperLink();
            link2.ID = "Label1";
            link2.Text = nyje.Title;
            link2.ForeColor = System.Drawing.Color.Gray;
            link2.EnableTheming = false;
            link2.NavigateUrl = nyje.Url;
           
            Container.Controls.Add(link1);
            Container.Controls.Add(link2);
        }
    }
}