using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using DevExpress.Web;

namespace PlatinumWeb.Templates
{

    public class MyIntTemplate : ITemplate
    {
        protected bool fillonMenje;
        public MyIntTemplate(bool fillonMenje)
        {
            this.fillonMenje = fillonMenje;
        }
        public  void InstantiateIn(Control Container)
        {
            var text = new ASPxTextBox();

            GridViewDataItemTemplateContainer gridContainer = (GridViewDataItemTemplateContainer)Container;
            text.ID = "txtBox";

            if (gridContainer.Text == "&nbsp;" || gridContainer.Text == "0")
            {
                if (fillonMenje) text.Value = "1";
                else text.Value = "0";

            }
            else
            {
                text.Value = gridContainer.Text;
            }
            text.Width = System.Web.UI.WebControls.Unit.Percentage(100);
            Container.Controls.Add(text);
        }
    }
}