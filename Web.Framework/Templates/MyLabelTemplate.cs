using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using DevExpress.Web;
using System.Web.UI.WebControls;
namespace PlatinumWeb.Templates
{
    public class MyLabelTemplate : ITemplate
    {
        public void InstantiateIn(Control Container)
        {

            GridViewDataItemTemplateContainer gridContainer = (GridViewDataItemTemplateContainer)Container;
            ASPxLabel label = new ASPxLabel();
            label.ID = "lbl";
            if (gridContainer.Text == "&nbsp;")
            {
                label.Value = "";
            }
            else
            {
                label.Value = gridContainer.Text;
            }
            Container.Controls.Add(label);
            label.Width = Unit.Percentage(100);
            label.EncodeHtml = false;
        }
    }
}