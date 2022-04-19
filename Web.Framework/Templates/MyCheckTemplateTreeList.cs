using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using DevExpress.Web;
using DevExpress.Web.ASPxTreeList;
namespace PlatinumWeb.Templates
{
    public class MyCheckTemplateTreeList : ITemplate
    {
        public void InstantiateIn(Control Container)
        {
            ASPxCheckBox check = new ASPxCheckBox();
            TreeListDataCellTemplateContainer

             gridContainer = (TreeListDataCellTemplateContainer)Container;
            check.ID = "cb";
            check.ReadOnly = false;

            if (gridContainer.Text == "&nbsp;")
            {
                check.Checked = false;
            }
            else
            {
                if (gridContainer.Text == "Checked")
                    check.Checked = true;
                else check.Checked = false;

            }
            Container.Controls.Add(check);
        }
    }
}