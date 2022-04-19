
using DevExpress.Web;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System;
namespace PlatinumWeb.Templates
{
    public class MySummaryGroupFooterTemplate : ITemplate
    {
        private string name;
        private string formatString;
        public void InstantiateIn(Control Container)
        {
            GridViewGroupFooterCellTemplateContainer gridContainer = (GridViewGroupFooterCellTemplateContainer)Container;
            ASPxLabel label = new ASPxLabel();

            label.ClientInstanceName = "group_" + name + gridContainer.VisibleIndex;

            Container.Controls.Add(label);
            label.Width = Unit.Percentage(100);
            label.Font.Size =11;
            label.Font.Bold = true;


            var summaryItem = gridContainer.Grid.GroupSummary.FirstOrDefault(x => x.FieldName == name);

            if (summaryItem != null)
            {
                if (string.IsNullOrEmpty(formatString))
                    label.Text = string.Format("{0}", gridContainer.Grid.GetGroupSummaryValue(gridContainer.VisibleIndex, summaryItem));
                else
                    label.Text =Convert.ToDecimal(gridContainer.Grid.GetGroupSummaryValue(gridContainer.VisibleIndex, summaryItem)).ToString(formatString);
            }
        }

        public MySummaryGroupFooterTemplate(string name,string formatString)
        {
            this.name = name;
            this.formatString = formatString;
        }
    }
}