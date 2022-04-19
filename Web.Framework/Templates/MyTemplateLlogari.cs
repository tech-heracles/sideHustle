using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using DevExpress.Web;
using System.Web.UI.WebControls;
namespace PlatinumWeb.Templates
{
    public class MyTemplateLlogari : ITemplate
    {
        private int idNdermarrje;
        public MyTemplateLlogari(int idNdermarrje)
        {
            this.idNdermarrje = idNdermarrje;
        }
        public void InstantiateIn(Control Container)
        {
            DbCore.DbKontabiliteti.colLlogarite col = new DbCore.DbKontabiliteti.colLlogarite();
            col.mbushLLogariteNdermarrjes(idNdermarrje);
             if (col.Count > 0)
                col.RemoveAt(0);
            ASPxComboBox cmb = new ASPxComboBox();
            cmb.ClientInstanceName = "IdLlogari";
            cmb.ClientSideEvents.KeyPress = "function(s,e){var code =_getKeyCode(e.htmlEvent);KeyPresLlogari(code,IdLlogari,0); }";
            cmb.ClientSideEvents.TextChanged = "function(s,e){TextChangedLlogari(0); }";
            cmb.ClientSideEvents.LostFocus = "function(s,e){LostFocusLlogari(0);}";
            cmb.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedLlogari(IdLlogari, 0) }";
            cmb.DropDownButton.Visible = false;
            cmb.ClientSideEvents.Init = "function(s,e){InitLlogari()}";
            EditButton b1 = new EditButton();

            cmb.Buttons.Add(b1);
            cmb.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            cmb.DropDownStyle = DropDownStyle.DropDownList;
            cmb.TextFormatString = "{0}";
            GridViewDataItemTemplateContainer gridContainer = (GridViewDataItemTemplateContainer)Container;
            cmb.ID = "cmbBox";
            ListBoxColumn colprove = new ListBoxColumn();
            colprove.FieldName = "NrLlogari";
            cmb.Columns.Add(colprove);
            cmb.DataSource = col;
            cmb.DataBind();

            if (gridContainer.Text == "0")
            {
                //cmb.Text = "";
                cmb.SelectedIndex = -1;
                cmb.Width = Unit.Percentage(100);

            }
            else
            {
                if (gridContainer.Text == "&nbsp;")
                {
                    //cmb.Text = "";
                    cmb.SelectedIndex = -1;
                    cmb.Width = Unit.Percentage(100);
                }
                else
                {
                    string text = "";
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