using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using DevExpress.Web;
using System.Web.UI.WebControls;

namespace PlatinumWeb.Templates
{
    public class MyComboTemplate : ITemplate
    {
        private DropDownStyle _dropDownStyle;
        public MyComboTemplate()
        {
            _dropDownStyle = DropDownStyle.DropDown;
        }
        public MyComboTemplate(DropDownStyle dropDownStyle)
        {
            _dropDownStyle = dropDownStyle;
        }
        public void InstantiateIn(Control Container)
        {
            var cmb = new ASPxComboBox();
            cmb.DropDownButton.Visible = true;
            cmb.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            cmb.DropDownStyle = _dropDownStyle;
            cmb.IncrementalFilteringDelay = 400;
            var gridContainer = (GridViewDataItemTemplateContainer)Container;
            cmb.ID = "cmbBox";
            if (!(gridContainer.Text == "&nbsp;" || gridContainer.Text == "0"))
            {
                var text = string.Empty;
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
