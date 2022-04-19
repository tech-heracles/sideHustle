using System.Globalization;
using System.Resources;
using DevExpress.XtraPivotGrid;
using DevExpress.XtraReports.UI.PivotGrid;
using System;
using System.Collections;
using AlphaWebReports.Common;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class RAP_ShitjetSipasMuajveKrahasues : DevExpress.XtraReports.UI.XtraReport
    {
		public RAP_ShitjetSipasMuajveKrahasues(){InitializeComponent();} 
        ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
        public RAP_ShitjetSipasMuajveKrahasues(ParametraRaporti param, DevExpress.XtraReports.UI.XtraReport raport):
            this(param.Ci, raport)
        { }
        public RAP_ShitjetSipasMuajveKrahasues(CultureInfo ci, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            parameter1.Value = raport.Parameters["filterMonedha"].Value;
            parameter2.Value = raport.Parameters["monedhaKF"].Value;

            XRPivotGridField fieldAgjent = new XRPivotGridField("AGJENTSHITJE", PivotArea.RowArea);
            fieldAgjent.FieldName = "EMRIAGJENTSHITJE";
            fieldAgjent.Caption = rm.GetString("labelAgjent", ci);
            fieldAgjent.Options.ShowGrandTotal = true;
            fieldAgjent.SortMode = PivotSortMode.Custom;
            


            XRPivotGridField fieldEmertimi = new XRPivotGridField("Emertimi", PivotArea.RowArea);
            fieldEmertimi.FieldName = "EMERTIMIKF";
            fieldEmertimi.Caption = rm.GetString("labelRaportiEmertimi", ci);

            XRPivotGridField fieldkodi = new XRPivotGridField("kodKlienti", PivotArea.RowArea);
            fieldkodi.FieldName = "kodKlienti";
            fieldkodi.Caption = "Kod Klienti";
        

            XRPivotGridField fieldmuaji = new XRPivotGridField("muaj", PivotArea.ColumnArea);
            fieldmuaji.FieldName = "muaj";
            fieldmuaji.Caption = "muaj";
            fieldmuaji.SortMode = PivotSortMode.Custom;
            fieldmuaji.GroupInterval = PivotGroupInterval.Custom;


            XRPivotGridField fieldShitje = new XRPivotGridField("ShitjeMuaji", PivotArea.DataArea);
            fieldShitje.FieldName = "ShitjeMuaji";
            fieldShitje.Caption = "Shitje";
           

            XRPivotGridField fieldQytet = new XRPivotGridField("Qyteti", PivotArea.RowArea);
            fieldQytet.FieldName = "QYTETIEMRI";
            fieldQytet.Caption = rm.GetString("labelQyteti", ci);

            fieldShitje.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            fieldShitje.CellFormat.FormatString = "{0:#,#.#0}";

            xrPivotGrid1.Fields.AddRange(new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField[] {fieldAgjent ,fieldkodi, fieldEmertimi,fieldQytet,
              fieldmuaji, fieldShitje});

            if (parameter1.Value.ToString() == "" && parameter2.Value.ToString() == "True")
            {
                xrPivotGrid1.OptionsView.ShowColumnTotals = false;
                xrPivotGrid1.OptionsView.ShowRowTotals = false;
                xrPivotGrid1.OptionsView.ShowRowGrandTotalHeader = false;
                xrPivotGrid1.OptionsView.ShowColumnGrandTotalHeader = false;
                xrPivotGrid1.OptionsView.ShowCustomTotalsForSingleValues = false;
                xrPivotGrid1.OptionsView.ShowRowGrandTotals = false;


            }
            xrPivotGrid1.Styles.FieldHeaderStyle = xrPivotGrid1.Styles.FieldHeaderStyle;
            xrPivotGrid1.Styles.FieldValueStyle = xrPivotGrid1.Styles.FieldHeaderStyle;
            xrPivotGrid1.FieldValueGrandTotalStyleName = xrPivotGrid1.Styles.FieldHeaderStyle.Name;
        }
        private void EmrateLabelave(CultureInfo ci)
        {

            xrLabel1.Text = rm.GetString("lblRaportShitjeSipasMuajve", ci);
        }

        private void xrPivotGrid1_CustomFieldSort(object sender, DevExpress.XtraReports.UI.PivotGrid.PivotGridCustomFieldSortEventArgs e)
        {

            //if (e.Field.FieldName == "muaj")
            //{
            //    int vl1;
            //    int vl2;
            //    int m1;
            //    int m2;

            //    if (e.GetListSourceColumnValue(e.ListSourceRowIndex1, "ROWID") != null)
            //    {
            //        vl1 = Convert.ToInt32(e.GetListSourceColumnValue(e.ListSourceRowIndex1, "ROWID"));
            //        vl2 = Convert.ToInt32(e.GetListSourceColumnValue(e.ListSourceRowIndex2, "ROWID"));
            //    }
            //    else
            //    {
            //        vl1 = 0;
            //        vl2 = 0;
            //    }
            //    if ((e.GetListSourceColumnValue(e.ListSourceRowIndex1, "muajnr")) != null)
            //    {
            //        m1 = Convert.ToInt32(e.GetListSourceColumnValue(e.ListSourceRowIndex1, "muajnr"));
            //        m2 = Convert.ToInt32(e.GetListSourceColumnValue(e.ListSourceRowIndex2, "muajnr"));
            //    }
            //    else
            //    {
            //        m1 = 0;
            //        m2 = 0;
            //    }
            //    if (m1 == m2)
            //    {
            //        e.Result = -1 * vl1.CompareTo(vl2);

            //    }
            //    else
            //        e.Result = m1.CompareTo(m2);
            //    e.Handled = true;
            //}

            

        }


        private void xrPivotGrid1_CustomGroupInterval(object sender, PivotCustomGroupIntervalEventArgs e)
        {
            //e.GroupValue = e.Value.ToString();
        }
    }
}
