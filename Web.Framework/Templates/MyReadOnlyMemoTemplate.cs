using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using DevExpress.Web;
using System.Web.UI.WebControls;

namespace PlatinumWeb.Templates
{
    public class MyReadOnlyMemoTemplate : ITemplate

    {
        private int r;
        private int c;
        public MyReadOnlyMemoTemplate(int rows, int columns)
        {
            r = rows;
            c = columns;
        }
        
        public MyReadOnlyMemoTemplate()
        {
            r = 1;
            c = 1;
        }
       
        public void InstantiateIn(Control Container)
        {
            ASPxMemo text = new   ASPxMemo  ();
            GridViewDataItemTemplateContainer gridContainer = (GridViewDataItemTemplateContainer)Container;
            text.ID = "txtBox";
            text.ReadOnly = true;
            text.ClientEnabled = false;
            text.Columns = c;
            text.Rows = r;
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