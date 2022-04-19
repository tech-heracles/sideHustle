using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using DevExpress.Web;

namespace PlatinumWeb.Templates
{
    public class MyLinkKomenteTemplate: ITemplate
    {
     
        public void InstantiateIn(Control Container)
        {
            ASPxHyperLink text = new ASPxHyperLink();
            GridViewDataItemTemplateContainer gridContainer = (GridViewDataItemTemplateContainer)Container;
            text.ID = "txtBox";


            if (gridContainer.Text == "&nbsp;")
            {
                text.Text = "";
                text.NavigateUrl = "";
                text.Width = System.Web.UI.WebControls.Unit.Percentage(100);
            }
            else
            {
                text.Text = gridContainer.Text;
                
                if (gridContainer.Text == "Po")

                    text.ClientSideEvents.Click = "function(s,e){ LinkClick(s,"+gridContainer.VisibleIndex+")}";
                else { text.NavigateUrl = ""; text.Enabled = false; text.DisabledStyle.ForeColor = System.Drawing.Color.Gray; }
                text.Width = System.Web.UI.WebControls.Unit.Percentage(100);
            }
            Container.Controls.Add(text);
        }
    }
}