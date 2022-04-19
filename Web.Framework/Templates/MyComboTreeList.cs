using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using DevExpress.Web;
using DevExpress.Web.ASPxTreeList;
using System.Web.UI.WebControls;
namespace PlatinumWeb.Templates
{
    public class MyComboTreeList : ITemplate
    {
        public void InstantiateIn(Control Container)
        {
            ASPxComboBox cmb = new ASPxComboBox();
            cmb.DropDownButton.Visible = true;
            cmb.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            cmb.DropDownStyle = DropDownStyle.DropDownList;
            //    cmb.TextFormatString = "{0}";
             TreeListEditCellTemplateContainer    gridContainer = (TreeListEditCellTemplateContainer)Container;
            cmb.ID = "cmbBox";
            if (!(gridContainer.Text == "&nbsp;" || gridContainer.Text == "0"))
            {
                string text = "";
                text = gridContainer.Text;

                text = DbCore.clsFunksione.zevendesoKaraktere(text);
                cmb.Text = text;
                cmb.Width = Unit.Percentage(100);
            }
            else
            {
                cmb.SelectedIndex = -1;
                cmb.Width = Unit.Percentage(100);
            }


            Container.Controls.Add(cmb);
        }
    }

}