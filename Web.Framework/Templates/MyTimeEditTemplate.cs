using System;

using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using DevExpress.Web;
using System.Web.UI.WebControls;

namespace PlatinumWeb.Templates
{
    public class MyTimeEditTemplate : System.Web.UI.ITemplate
    {
        public void InstantiateIn(Control Container)
        {
          ASPxTimeEdit   cal = new ASPxTimeEdit();

            GridViewDataItemTemplateContainer gridContainer = (GridViewDataItemTemplateContainer)Container;
            cal.ID = "te";
            if (!(gridContainer.Text == "&nbsp;" || gridContainer.Text == "0"))
            {
                cal.Text = gridContainer.Text;
            }
            else
                cal.Value = "";

            cal.Width = Unit.Percentage(100);
            Container.Controls.Add(cal);
        }
    }
}