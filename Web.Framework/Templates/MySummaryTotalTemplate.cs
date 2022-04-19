using DevExpress.Web;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PlatinumWeb.Templates
{
    public class MySummaryTotalTemplate : ITemplate
    {
        private string name;
        private string formatString;

        public void InstantiateIn(Control Container)
        {
            GridViewFooterCellTemplateContainer gridContainer = (GridViewFooterCellTemplateContainer)Container;
            ASPxLabel label = new ASPxLabel();

            label.ClientInstanceName = "footer_" + name;

            Container.Controls.Add(label);
            label.Width = Unit.Percentage(100);
            label.Font.Size = 9;


            var summaryItem = gridContainer.Grid.TotalSummary.FirstOrDefault(x => x.FieldName == name);

            if (summaryItem != null)
            {
                if (string.IsNullOrEmpty(formatString))
                    label.Text = string.Format("{0}", gridContainer.Grid.GetTotalSummaryValue(summaryItem));
                else
                    label.Text = Convert.ToDecimal(gridContainer.Grid.GetTotalSummaryValue(summaryItem)).ToString(formatString);
            }

        }

        public MySummaryTotalTemplate(string name,string formati)
        {
            this.name = name;
            this.formatString = formati;
        }
    }
}