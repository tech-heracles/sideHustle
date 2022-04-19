using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using DevExpress.Web;

namespace PlatinumWeb.Templates
{
    public class MyDoubleTemplate : ITemplate
    {
        protected bool paformat;
        protected int shifraPasPresjes;
        protected string vlereDefault;


        public MyDoubleTemplate(bool paformat, int shifraPasPresjes, string vlereDefault)
        {

        }
        public virtual void InstantiateIn(Control Container)
        {
            var text = new ASPxTextBox
            {
                NullText = "0",
                ID = "txtBox"
            };
            var gridContainer = (GridViewDataItemTemplateContainer)Container;
            if (!paformat)
                text.DisplayFormatString = DbCore.clsFunksione.krijoNumer(shifraPasPresjes, vlereDefault);

            if (gridContainer.Text == "&nbsp;")
            {
                text.Value = DbCore.clsFunksione.krijoNumer(shifraPasPresjes, vlereDefault);
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