using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using DevExpress.Web;
using System.Web.UI.WebControls;
namespace PlatinumWeb.Templates
{
    public class MyMemoTemplate : ITemplate
    {

        private int r;
  
        public MyMemoTemplate(int rows)
        {
            r = rows;
           
        }

        public MyMemoTemplate()
        {
            r = 1;

        }
        public void InstantiateIn(Control Container)
        {
            ASPxMemo text = new ASPxMemo();
            GridViewDataItemTemplateContainer gridContainer = (GridViewDataItemTemplateContainer)Container;
            text.ID = "txtBox";
            text.Width = Unit.Percentage(100);
            text.Rows = r;

            if (gridContainer.Text == "&nbsp;")
            {
                text.Value = ""; 
            }
            else
            {
                string text1 = "";
                text1 = gridContainer.Text;
                text.Width = Unit.Percentage(100);
                text1 = DbCore.clsFunksione.zevendesoKaraktere(text1);
                text.Value = text1;
               
            }
            Container.Controls.Add(text);
        }
    }
}