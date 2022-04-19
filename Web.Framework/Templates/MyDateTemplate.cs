using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using PlatinumWeb.ApplicationUtils;

namespace PlatinumWeb.Templates
{
    public class MyDateTemplate:ITemplate
    {

        public MyDateTemplate()
        {

        }
        public void InstantiateIn(Control Container)
        {
            GridViewDataItemTemplateContainer gContainer = (GridViewDataItemTemplateContainer)Container;
            
            ASPxDateEdit date = new ASPxDateEdit();

            AspxWebControlUtils.vendosDateEditMask(date);

            GridViewDataItemTemplateContainer gridContainer = (GridViewDataItemTemplateContainer)Container;

            date.ClientInstanceName = gContainer.Column.FieldName+gContainer.KeyValue.ToString();
            date.ClientSideEvents.Init = $"function(s,e){{if(s.GetValue()==null)s.SetDate(new Date())}}";
            //date.ClientSideEvents.TextChanged = @"function(s,e){dataVlefshmerie[key] = { DtFillimi: window['DtFillimi" + gContainer.KeyValue + "'].GetText(), DtMbarimi: window['DtMbarimi" + gContainer.KeyValue + "'].GetText() };}";
            //date.ClientSideEvents.ValueChanged = @"function(s,e){dataVlefshmerie[key] = { DtFillimi: window['DtFillimi" + gContainer.KeyValue + "'].GetText(), DtMbarimi: window['DtMbarimi" + gContainer.KeyValue + "'].GetText() };}";
            if (!(gridContainer.Text == "&nbsp;"))
            {
                if (gridContainer.Text == "01/01/1900")
                    date.Text = "";
                else
                    date.Text = gridContainer.Text;
            }
            else
            {
                date.Text = "";
            }
            Container.Controls.Add(date);
        }
    }
}
