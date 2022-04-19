using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using DevExpress.Web.ASPxPivotGrid;
using DevExpress.Web;
using PlatinumWeb.Templates;
namespace PlatinumWeb
{
    public partial class FilterPopup : UserControl
    {
        private string _themeNameField = string.Empty;
        private ASPxPivotGrid _pivotGridFld;
        public string PivotGridID { set; get; }
        public ASPxPivotGrid PivotGrid
        {
            get
            {
                if (this._pivotGridFld == null)
                    throw new Exception("mungon Id e pivotgrides");
                return _pivotGridFld;
            }
            set
            {
                _pivotGridFld = value;
                //BindGridView(CurrentField);
                PivotGrid.HeaderTemplate = new MyPivotHeaderFilterTemplate(ThemeName, ASPxPopupControl1.ClientInstanceName);
                PivotGrid.CustomCallback += PivotGrid_CustomCallback;
            }
        }
        private string themeName_Field = string.Empty;
        public string ThemeName
        {
            get
            {
                if (String.IsNullOrEmpty(themeName_Field))
                    return Page.Theme;
                return themeName_Field;
            }
            set { themeName_Field = value; }

        }
        PivotGridField CurrentField => (PivotGridField)PivotGrid.Fields.GetFieldByName((string)CacheLayer.GlobalCacheManager.MySessionCache["CurrentField"]);
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(PivotGridID))
            {
                ASPxPivotGrid pivot = Parent.FindControl(PivotGridID) as ASPxPivotGrid;
                if (pivot != null)
                {
                    PivotGrid = pivot;
                    ASPxPopupControl1.JSProperties["cpPivotGridName"] = pivot.ClientInstanceName;
                }
            }
        }
        
        void PivotGrid_CustomCallback(object sender, PivotGridCustomCallbackEventArgs e)
        {
            string[] values = e.Parameters.Split(",".ToCharArray());
            if (!(values.Contains("hidden") || values.Contains("visible")))
            {
                var newFilter = from v in CurrentField.GetUniqueValues() where !values.Contains(Convert.ToString(v)) select v;
                CurrentField.FilterValues.ValuesExcluded = newFilter.ToArray();
            }
        }
        protected void ASPxGridView1_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            ASPxGridView gridView = (ASPxGridView)sender;
            switch (e.Parameters)
            {
                case "ClearGrid":
                    gridView.DataSource = null;
                    gridView.DataBind();
                    break;
                case "InvertFilter":
                    List<object> selectedValues = gridView.GetSelectedFieldValues(new string[] { "FilterValue" });
                    gridView.Selection.SelectAll();
                    foreach (object val in selectedValues)
                        gridView.Selection.UnselectRowByKey(val);
                    break;
                case "ShowAll":
                    gridView.Selection.SelectAll();
                    break;
                case "HideAll":
                    gridView.Selection.UnselectAll();
                    break;
                default:
                    CacheLayer.GlobalCacheManager.MySessionCache["CurrentField"] = e.Parameters;
                    BindGridView(CurrentField);
                    gridView.PageIndex = 0;
                    gridView.Selection.UnselectAll();
                    gridView.FilterExpression = string.Empty;
                    foreach (object val in CurrentField.FilterValues.ValuesIncluded)
                    {
                        gridView.Selection.SelectRowByKey(Convert.ToString(val));
                    }
                    break;
            }
        }
        public void BindGridView(PivotGridField field)
        {
            if (field == null) return;
            var list = from v in field.GetUniqueValues()
                       select new FilterInfo() { FilterValue = Convert.ToString(v) };
            ASPxGridView1.DataSource = list;
            ASPxGridView1.DataBind();
        }
        public void BindGridView()
        {
            if (CurrentField == null) return;
            ASPxGridView1.DataSource = CurrentField.GetUniqueValues().Select(x => new FilterInfo { FilterValue = x.ToString() });
            ASPxGridView1.DataBind();
        }
        public class FilterInfo
        {
            public string FilterValue { get; set; }
        }



       
  
    


    }
}