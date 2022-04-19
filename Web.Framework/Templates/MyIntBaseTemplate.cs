using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using DevExpress.Web;

namespace PlatinumWeb.Templates
{

    public class MyIntBaseTemplate : ITemplate
    {
        protected bool fillonMenje;
        protected ASPxTextEdit text;

        public MyIntBaseTemplate(bool fillonMenje,ASPxTextEdit txtEdit)
        {
            this.fillonMenje = fillonMenje;
            text = txtEdit;

        }
        public virtual void InstantiateIn(Control Container)
        {


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