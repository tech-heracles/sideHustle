using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using DevExpress.Web;
using System.Web.UI.WebControls;
using System.Globalization;

namespace PlatinumWeb.Templates
{
    public class MyComboTemplateFormatNr : ITemplate
    {
        private CultureInfo ci;

        public MyComboTemplateFormatNr(CultureInfo ci)
        {
            this.ci = ci;
        }

        public void InstantiateIn(Control Container)
        {
            var col = new DbCore.DbShare.colFormatNr();
            col.mbushFormatNr();
            var cmb = new ASPxComboBox();



            cmb.DropDownButton.Visible = false;
            var b1 = new EditButton();

            cmb.Buttons.Add(b1);
            cmb.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            cmb.DropDownStyle = DropDownStyle.DropDownList;

            var gridContainer = (GridViewDataItemTemplateContainer)Container;
            cmb.ID = "cmbBox";











            cmb.Items.Add("0", 0);
            cmb.Items.Add("0.0", 1);
            cmb.Items.Add("0.00", 2);
            cmb.Items.Add("0.000", 3);
            cmb.Items.Add("0.0000", 4);
            cmb.Items.Add("0.00000", 5);
            cmb.Items.Add("0.000000", 6);
            cmb.Items.Add("0.0000000", 7);
            cmb.Items.Add("0.00000000", 8);
            cmb.Items.Add("0.000000000", 9);
            cmb.Items.Add("0.0000000000", 10);
            if (gridContainer.Column.FieldName == null)
            {
                if (gridContainer.Text == "0")
                {
                    cmb.SelectedIndex = -1;
                    cmb.Width = Unit.Percentage(100);
                }
                else
                {
                    if (gridContainer.Text == "&nbsp;")
                    {
                        cmb.SelectedIndex = -1;
                        cmb.Width = Unit.Percentage(100);
                    }
                    else
                    {
                        var text = string.Empty;
                        text = gridContainer.Text;
                        text = DbCore.clsFunksione.zevendesoKaraktere(text);
                        cmb.Text = text;
                        cmb.Width = Unit.Percentage(100);
                    }
                }
            }
            else
            {
                if (gridContainer.Text == "&nbsp;")
                {
                    cmb.Text = gridContainer.Text;
                }
                else
                {
                    var text = string.Empty;
                    text = gridContainer.Text;
                    text = DbCore.clsFunksione.zevendesoKaraktere(text);
                    cmb.Text = text;
                    cmb.Width = Unit.Percentage(100);
                }
            }
            Container.Controls.Add(cmb);
        }
    }
}
