using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using DevExpress.Web;
using System.Web.UI.WebControls;
namespace PlatinumWeb.Templates
{
    public class MyTextTemplate : ITemplate
    {
        public void InstantiateIn(Control Container)
        {
            ASPxTextBox text = new ASPxTextBox();
            GridViewDataItemTemplateContainer gridContainer = (GridViewDataItemTemplateContainer)Container;
            text.ID = "txtBox";


            if (gridContainer.Text == "&nbsp;")
            {
                text.Value = ""; 
                text.Width = Unit.Percentage(100);
            }
            else
            {
                string text1 = "";
                text1 = gridContainer.Text;

                text1 = DbCore.clsFunksione.zevendesoKaraktere(text1);
                text.Value = text1;
                text.Width = Unit.Percentage(100);
            }
            Container.Controls.Add(text);
        }
    }
}