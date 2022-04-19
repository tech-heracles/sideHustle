using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using DevExpress.Web;
using System.Web.UI.WebControls;
namespace PlatinumWeb.Templates
{
    public class MyTemplateKushtePagese : ITemplate
    {
        public void InstantiateIn(Control Container)
        {

            string text;
            TextBox txt = new TextBox();
            txt.ID = "txt";

            ASPxButton button = new ASPxButton();
            button.CssClass = "dxeButtonEdit_btn";
            button.Text = "...";
            button.ID = "btn";
            button.Width = Unit.Percentage(30);
            button.Height = Unit.Percentage(30);
            button.AutoPostBack = false;
            Table t = new Table();
            t.BorderWidth = 0;

            TableRow tr = new TableRow();

            TableCell tc = new TableCell();
            tc.Controls.Add(txt);
            tc.Width = Unit.Percentage(85);

            TableCell tc1 = new TableCell();
            tc1.Controls.Add(button);
            tc1.Width = Unit.Percentage(15);
            tr.Cells.Add(tc);
            tr.Cells.Add(tc1);
            t.Controls.Add(tr);
            GridViewDataItemTemplateContainer gridContainer = (GridViewDataItemTemplateContainer)Container;
            t.ID = "kot";
             DbCore.DbKontabiliteti.clsKushtPageseKoka clsKoka = new DbCore.DbKontabiliteti.clsKushtPageseKoka(int.Parse(gridContainer.Text));
            if (clsKoka != null)
                text = clsKoka.KodiKushtPagese;
            else text = "";
           
           
                if (text == "0")
                {
                    txt.Text = "";
                }
                else
                {
                    if (text == "&nbsp;")
                    {
                        txt.Text = "";
                    }
                    else
                    {

                        text = gridContainer.Text;

                        text = DbCore.clsFunksione.zevendesoKaraktere(text);
                        txt.Text = text;
                        txt.Width = Unit.Percentage(100);
                    }
                }
           
            Container.Controls.Add(t);
        }
    }
}