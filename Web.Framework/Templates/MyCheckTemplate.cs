using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using DevExpress.Web;

namespace PlatinumWeb.Templates
{
    public class MyCheckTemplate : ITemplate
    {
        private bool readOnly;
        private bool disable;

        public MyCheckTemplate(bool readOnly, bool disable)
        {
            this.readOnly = readOnly;
            this.disable = disable;
        }

        public void InstantiateIn(Control Container)
        {
            ASPxCheckBox check = new ASPxCheckBox();
            GridViewDataItemTemplateContainer gridContainer = (GridViewDataItemTemplateContainer)Container;
            check.ID = "cb";
            if (readOnly)
                check.ReadOnly = true;
            if (disable)
                check.Enabled = false;
            if (gridContainer.Text == "&nbsp;")
            {
                check.Checked = false;
            }
            else
            {
                if (gridContainer.Text == "Checked" || gridContainer.Text.ToLower() == "true")
                    check.Checked = true;
                else check.Checked = false;

            }
            Container.Controls.Add(check);
        }
    }
}