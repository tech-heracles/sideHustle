using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace PlatinumWeb.Templates
{
    public class MySpinTemplate : ITemplate
    {
        public void InstantiateIn(Control Container)
        {
            DevExpress.Web.ASPxTimeEdit text = new DevExpress.Web.ASPxTimeEdit();
            GridViewDataItemTemplateContainer gridContainer = (GridViewDataItemTemplateContainer)Container;
            text.ID = "txtBox";
      
            if (gridContainer.Text == "&nbsp;")
            {
                text.Value = 0;
            }
            else
            {
                text.DateTime =DateTime.Parse( gridContainer.Text);
            }
            text.Width = System.Web.UI.WebControls.Unit.Percentage(100);
            Container.Controls.Add(text);
        }
    }
}