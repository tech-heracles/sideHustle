using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using DevExpress.Web;
using System.Web.UI.WebControls;

namespace PlatinumWeb.Templates
{
    public class MyReadOnlyTextTemplate : ITemplate
    {
        public void InstantiateIn(Control Container)
        {
            ASPxTextBox text = new   ASPxTextBox();
            GridViewDataItemTemplateContainer gridContainer = (GridViewDataItemTemplateContainer)Container;
            text.ID = "txtBox";
            text.ReadOnly = true;
            text.ClientEnabled = false;
           
            if (gridContainer.Text == "&nbsp;")
            {
                text.Value = "";
            }
            else
            {
                text.Value = DbCore.clsFunksione.zevendesoKaraktere(gridContainer.Text);
                text.Width = Unit.Percentage(100);
            }
            text.Width = Unit.Percentage(100);
            Container.Controls.Add(text);
        }
    }
}