using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using DevExpress.Web;
using System.Web.UI.WebControls;

namespace PlatinumWeb.Templates
{
    public class MyLinkTemplate : ITemplate
    {
        private string width;
        public MyLinkTemplate(string width)
        {
            this.width = width;
        }
        public void InstantiateIn(Control Container)
        {
            ASPxHyperLink text = new ASPxHyperLink();
            GridViewDataItemTemplateContainer gridContainer = (GridViewDataItemTemplateContainer)Container;
            text.ID = "txtBox";


            if (gridContainer.Text == "&nbsp;")
            {
                text.Text = "Raporti";
                text.NavigateUrl = "";
                text.Width = Unit.Percentage(100);
            }
            else
            {
                text.Text = "Raporti";
             //   text.Value = "javascript: window.open('" + DbCore.clsFunksione.zevendesoKaraktere(gridContainer.Text) + "')";
                text.ClientSideEvents.Click = "function(s,e){ window.open('" + DbCore.clsFunksione.zevendesoKaraktere(gridContainer.Text) + "&windowWidth="+width+"')}";
            
                text.Width = Unit.Percentage(100);
            }
            Container.Controls.Add(text);
        }
    }
}