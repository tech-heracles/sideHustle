using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using DevExpress.Web;
using System.Web.UI.WebControls;

namespace PlatinumWeb.Templates
{
    public class MyCalendarTemplate : ITemplate
    {
        public void InstantiateIn(Control Container)
        {
            ASPxDateEdit cal = new ASPxDateEdit();

            GridViewDataItemTemplateContainer gridContainer = (GridViewDataItemTemplateContainer)Container;
            cal.ID = "cal";
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