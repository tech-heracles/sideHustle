using DevExpress.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PlatinumWeb.Templates
{
    public class MyFooterCellTemplate : ITemplate
    {
        private string name;

        private string text;

        public void InstantiateIn(Control Container)
        {
            GridViewFooterCellTemplateContainer gridContainer = (GridViewFooterCellTemplateContainer)Container;
            ASPxLabel label = new ASPxLabel();

            label.ClientInstanceName = "footer_" + name;

            Container.Controls.Add(label);
            label.Width = Unit.Percentage(100);
            label.Text = text;
            label.Font.Size = 12;
        }

        public MyFooterCellTemplate(string name, string text)
        {
            this.name = name;
            this.text = text;
        }
    }
}