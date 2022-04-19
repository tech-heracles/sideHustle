using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using DevExpress.Web;
using System.Web.UI.WebControls;

namespace PlatinumWeb.Templates
{
    public class MyListboxTemplate : ITemplate
    {
        public void InstantiateIn(Control Container)
        {
            ASPxListBox cmb = new  ASPxListBox ();
           
            GridViewDataItemTemplateContainer gridContainer = (GridViewDataItemTemplateContainer)Container;
            cmb.ID = "lbx";
            cmb.SelectionMode = ListEditSelectionMode.CheckColumn;
            if (!(gridContainer.Text == "&nbsp;" || gridContainer.Text == "0"))
            {
                //string text = "";
                //text = gridContainer.Text;

                //text = DbCore.clsFunksione.zevendesoKaraktere(text);
                //cmb..Text = text;
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