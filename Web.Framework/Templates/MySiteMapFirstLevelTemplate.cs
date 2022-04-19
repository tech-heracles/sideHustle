using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using DevExpress.Web;
using System.Web.UI.WebControls;

namespace PlatinumWeb.Templates
{
    public class MySiteMapFirstLevelTemplate : ITemplate
    {
        public void InstantiateIn(Control Container)
        {
            var nodeContainer = (NodeTemplateContainer)Container;
            var nyje = (SiteMapNode)nodeContainer.DataItem;

            var img = new ASPxImage();
            img.ImageUrl = "images/t7.gif";
            img.Height = 20;
            img.Width = 20;

            var label1 = new ASPxLabel();
            label1.Text = "     " + nyje.Title;
            label1.ForeColor = System.Drawing.Color.FromArgb(0xFF, 0x99, 0x00);
            label1.Font.Bold = false;
            label1.Font.Size = FontUnit.Medium;

            Container.Controls.Add(img);
            Container.Controls.Add(label1);
        }
    }
}
