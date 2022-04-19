using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using DevExpress.Web;
using System.Web.UI.WebControls;
namespace PlatinumWeb.Templates
{
    public class MyLabelGTemplate : ITemplate
    {
        public void InstantiateIn(Control Container)
        {

            GridViewDataItemTemplateContainer gridContainer = (GridViewDataItemTemplateContainer)Container;
            ASPxLabel label = new ASPxLabel() { ID = "lbl" };
         
            if (gridContainer.Text == "&nbsp;" || gridContainer.Text == "0.00")
            {
                label.Value = "";
            }
            else
            {
                label.Value = gridContainer.Text;
            }
            Container.Controls.Add(label);
        }
    }
}