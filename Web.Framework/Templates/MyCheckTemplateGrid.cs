using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using DevExpress.Web;
namespace PlatinumWeb.Templates
{
    public class MyCheckTemplateGrid : ITemplate
    {
        private int id;
        private string column;
        private bool checke;

        public MyCheckTemplateGrid(int id, string column, bool check)
        {
            this.id = id;
            this.column = column;
            this.checke = check;
        }

        public void InstantiateIn(Control Container)
        {
            ASPxCheckBox check = new ASPxCheckBox();

            GridViewDataItemTemplateContainer gridContainer = (GridViewDataItemTemplateContainer)Container;
            check.ID = "cb";
            check.Enabled = true;
            check.ClientSideEvents.CheckedChanged = "function (s,e) { CheckedChanged(s," + id + "," + column + ");}";


            if (checke)
                check.Checked = true;
            else check.Checked = false;


            Container.Controls.Add(check);
        }
    }
}