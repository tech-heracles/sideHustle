using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using DevExpress.Web;
using System.Web.UI.WebControls;
using System.Drawing;
namespace PlatinumWeb.Templates
{

    public class MyDateGTemplate : ITemplate
    {
        string color;
        public MyDateGTemplate(string color)
        {
            this.color = color;
        }
        public void InstantiateIn(Control Container)
        {
            ASPxLabel check = new ASPxLabel();
            GridViewDataItemTemplateContainer gridContainer = (GridViewDataItemTemplateContainer)Container;
            check.ID = "lbl";
            if (color == "Red")
                check.ForeColor = Color.Red;
            else if (color == "Green")
                check.ForeColor = Color.Green;
            else check.ForeColor = Color.Black;
            if (!(gridContainer.Text == "&nbsp;"))
            {
                if (gridContainer.Text == "01/01/1900")
                    check.Text = "";
                else
                    check.Text = gridContainer.Text;
            }
            else
            {
                check.Text = "";
            }
            Container.Controls.Add(check);
        }
    }
}