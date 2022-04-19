using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using DevExpress.Web;

namespace PlatinumWeb.Templates
{
    public class MyDoubleBaseTemplate : ITemplate
    {
        protected bool paformat;
        protected int shifraPasPresjes;
        protected string vlereDefault;
        protected ASPxTextEdit text;
        public MyDoubleBaseTemplate(bool paformat, int shifraPasPresjes, string vlereDefault, ASPxTextEdit txtEdit)
        {
            this.paformat = paformat;
            this.shifraPasPresjes = shifraPasPresjes;
            this.vlereDefault = vlereDefault;
            text = txtEdit;
        }
        public virtual void InstantiateIn(Control Container)
        {
            //var text = new ASPxTextBox();
            GridViewDataItemTemplateContainer gridContainer = (GridViewDataItemTemplateContainer)Container;
            text.ID = "txtBox";
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