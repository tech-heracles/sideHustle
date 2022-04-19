using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxPivotGrid;

namespace PlatinumWeb.Templates
{
    public class MyPivotHeaderFilterTemplate : ITemplate
    {
        private string _themeName;
        private string _popupName;
        public MyPivotHeaderFilterTemplate(string themeName,string popupName)
        {
            _themeName = themeName;
            _popupName=popupName;
        }

        public string ThemeName
        {
            get { return _themeName; }
            set { _themeName = value; }
        }

        public string PopupName
        {
            get { return _popupName; }
            set { _popupName = value; }
        }


        private string FilterButtonOnClick(PivotGridHeaderTemplateContainer c)
        {
            return $@"            
            {{
                var rects = this.getClientRects();
                GridView.PerformCallback( '{c.Field.ID}' );                
                window['{_popupName}'].ShowAtPos(rects[0].left, rects[0].bottom);
                window['{_popupName}'].SetHeaderText( '{c.Field.Caption}' );               
            }}";
        }

        public void InstantiateIn(Control container)
        {
            PivotGridHeaderTemplateContainer c = (PivotGridHeaderTemplateContainer)container;
            PivotGridHeaderHtmlTable fieldHeaderTable = c.CreateHeader();
            if (c.Field.Area != DevExpress.XtraPivotGrid.PivotArea.DataArea && c.Field.Options.AllowFilter != DevExpress.Utils.DefaultBoolean.False)
            {

                Image myFilterButton = new Image();
                myFilterButton.Attributes["OnClick"] = FilterButtonOnClick(c);

                string themeSufix = String.IsNullOrEmpty(_themeName) ? string.Empty : "_" + _themeName;
                string cssClassFS = c.Field.FilterValues.HasFilter ? "dxPivotGrid_pgFilterButtonActive{0}" : "dxPivotGrid_pgFilterButton{0}";
                myFilterButton.CssClass = String.Format(cssClassFS, themeSufix);


                TableCell filterButtonCell = new TableCell();
                filterButtonCell.Controls.Add(myFilterButton);
                TableCell defaultFilterCell = fieldHeaderTable.Rows[0].Cells[fieldHeaderTable.Rows[0].Cells.Count - 1];
                fieldHeaderTable.Rows[0].Cells.Remove(defaultFilterCell);
                filterButtonCell.CssClass = "dxpgControl dxpgHeader dxpgHeaderFilter";

                fieldHeaderTable.Rows[0].Cells.Add(filterButtonCell);
            }

            c.Controls.Add(fieldHeaderTable);
        }


    }
}