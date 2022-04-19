using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using DevExpress.XtraPivotGrid;
using DevExpress.XtraReports.UI.PivotGrid;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;
using System.Reflection;
using DevExpress.Data.PivotGrid;


namespace AlphaWebReports.RaportetDs.ListPagesat.Raportet
{
    public partial class Rap_ListepagesaStandarteDinamike2 : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_ListepagesaStandarteDinamike2(){InitializeComponent();} 
  		
		public Rap_ListepagesaStandarteDinamike2(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):this(param.Ci,param.IdNdermarrje,param.IdViti,report){}

        public Rap_ListepagesaStandarteDinamike2(CultureInfo ci,int idNdermarrje, int idViti, XtraReport raport)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                          System.Reflection.Assembly.Load("App_GlobalResources"));
            InitializeComponent();
            EmrateLabelave(ci);
            Ndermarja.Value = raport.Parameters[0].Value;
            Monedha.Value = raport.Parameters[1].Value;
            DtDok.Value = raport.Parameters[2].Value;
            DtRegj.Value = raport.Parameters[3].Value;
            NrDok.Value = raport.Parameters[4].Value;
            Muaji.Value = raport.Parameters["filterMuaji"].Value.ToString();
            KodiPunonjesit.Value = raport.Parameters[6].Value;
            Departamenti.Value = raport.Parameters[7].Value;
            NenDepartamenti.Value = raport.Parameters[8].Value;

            // Percakton kolonat e pivot grides

            XRPivotGridField fieldNrPunonjesi = new XRPivotGridField("Nr. Personal", PivotArea.RowArea);
            fieldNrPunonjesi.FieldName = "NRPERSONAL";
            fieldNrPunonjesi.Caption = rm.GetString("labelRaportNrPersonal", ci);
            fieldNrPunonjesi.Width = 45;

            XRPivotGridField fieldEmerPunonjesi = new XRPivotGridField("Emri", PivotArea.RowArea);
            fieldEmerPunonjesi.FieldName = "EMER";
            fieldEmerPunonjesi.Caption = rm.GetString("labelRaportEmri", ci);
            fieldEmerPunonjesi.Width = 40;

            XRPivotGridField fieldMbiemerPunonjesi = new XRPivotGridField("Mbiemri", PivotArea.RowArea);
            fieldMbiemerPunonjesi.FieldName = "MBIEMER";
            fieldMbiemerPunonjesi.Caption = rm.GetString("labelRaportMbiemri", ci);
            fieldMbiemerPunonjesi.Width = 40;

            XRPivotGridField fieldTipiKomponentes = new XRPivotGridField("", PivotArea.ColumnArea);
            fieldTipiKomponentes.FieldName = "TIPI";
            fieldTipiKomponentes.Caption = rm.GetString("labelFilterAvancuarTipi", ci);

            XRPivotGridField fieldKomponente = new XRPivotGridField("", PivotArea.ColumnArea);
            if (ci.ToString() == "sq-AL")
                fieldKomponente.FieldName = "PERSHKRIMKOMPONENTE";
            else
                fieldKomponente.FieldName = "PERSHKRIMKOMPONENTE_ENG";
            fieldKomponente.Caption = "";
            fieldKomponente.Width = 62;

            XRPivotGridField fieldVleraKompon = new XRPivotGridField("", PivotArea.DataArea);
            fieldVleraKompon.FieldName = "VLERA";
            fieldVleraKompon.Caption = rm.GetString("labelRaportVlera", ci);
            fieldVleraKompon.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            fieldVleraKompon.CellFormat.FormatString = "{0:#,#.00}";
            fieldVleraKompon.Width = 55;
            fieldVleraKompon.Options.ShowGrandTotal = false;

            XRPivotGridField fieldDitet = new XRPivotGridField("", PivotArea.DataArea);
            fieldDitet.FieldName = "SHENIME";
            fieldDitet.Caption = rm.GetString("lblRaportDitet", ci);
            fieldDitet.Width = 40;
            fieldDitet.Options.ShowGrandTotal = false;
            fieldDitet.Options.ShowTotals = false;
            fieldDitet.Options.ShowCustomTotals = false;
            //fieldDitet.TotalsVisibility = PivotTotalsVisibility.AutomaticTotals;
            fieldDitet.SummaryType = PivotSummaryType.Min;

            XRPivotGridField fieldIDKokaListepagese = new XRPivotGridField("IDKOKA", PivotArea.FilterArea);
            fieldIDKokaListepagese.FieldName = "IDKOKA";
            fieldIDKokaListepagese.Visible = false;

            XRPivotGridField fieldNrTipiKomponentes = new XRPivotGridField("TIPINR", PivotArea.ColumnArea);
            fieldNrTipiKomponentes.FieldName = "TIPINR";
            fieldNrTipiKomponentes.Visible = false;

            XRPivotGridField fieldPagaNeto = new XRPivotGridField();
            fieldPagaNeto.Area = PivotArea.DataArea;
            fieldPagaNeto.FieldName = "PagaNeto";
            fieldPagaNeto.Caption = rm.GetString("labelRaportPagaNeto", ci);
            fieldPagaNeto.Options.ShowValues = false;
            fieldPagaNeto.Options.ShowTotals = false;
            fieldPagaNeto.Options.ShowGrandTotal = true;
            fieldPagaNeto.UseNativeFormat = DevExpress.Utils.DefaultBoolean.False;
            fieldPagaNeto.CellFormat.FormatString = "{0:#,#.00}";
            //Shton kolonat ne pivot gride
            xrPivotGrid2.Fields.AddRange(new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField[] {fieldNrPunonjesi, fieldEmerPunonjesi, fieldMbiemerPunonjesi,
                fieldTipiKomponentes, fieldKomponente, fieldVleraKompon, fieldDitet, fieldIDKokaListepagese, fieldNrTipiKomponentes, fieldPagaNeto});

            xrPivotGrid2.Fields["NRPERSONAL"].SortMode = PivotSortMode.Custom;

        }

        private void xrPivotGrid2_CustomFieldSort(object sender, DevExpress.XtraReports.UI.PivotGrid.PivotGridCustomFieldSortEventArgs e)
        {
            if (e.Field.FieldName == "NRPERSONAL")
            {

                object orderValue1 = e.GetListSourceColumnValue(e.ListSourceRowIndex1, "EMER"),
                        orderValue2 = e.GetListSourceColumnValue(e.ListSourceRowIndex2, "EMER");
                int comparison = Comparer.Default.Compare(orderValue1, orderValue2);
                /*nese krahasimi jep 0, pra kane te njejtin numer rendor  xrPivotGrid i grupon rezultatet
                 * dhe dalin keq numrat personale te punonjesve. Ndaj bejme krahasimin e dyte qe garanton
                 * qe NRPERSONAL eshte ndryshe
                 */
                if (comparison == 0)
                {
                    orderValue1 = e.GetListSourceColumnValue(e.ListSourceRowIndex1, "EMER");
                    orderValue2 = e.GetListSourceColumnValue(e.ListSourceRowIndex2, "EMER");
                    comparison = Comparer.Default.Compare(orderValue1, orderValue2);

                }

                e.Result = comparison;
                e.Handled = true;
                return;

            }
        }

        private void xrPivotGrid2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("IDKOKA") != null)
            {
                int idListePagesa = Convert.ToInt32(GetCurrentColumnValue("IDKOKA").ToString());
                this.xrPivotGrid2.Prefilter.Criteria = DevExpress.Data.Filtering.CriteriaOperator.Parse("[IDKOKA] = ? ", idListePagesa);
            }
        }
        // <summary>
        // LLogarit pagen neto te punonjesit si diference midis magesave dhe ndalesave
        // </summary>
        // <param name="sender"></param>
        // <param name="e"></param>
        private void xrPivotGrid2_CustomCellDisplayText(object sender, DevExpress.XtraReports.UI.PivotGrid.PivotCellDisplayTextEventArgs e)
        {
            if (e.DataField.FieldName == "PagaNeto")
            {
                PivotDrillDownDataSource ds = e.CreateDrillDownDataSource();
                Double customValue = 0;
                for (int i = 0; i < ds.RowCount; i++)
                {
                    object currentType = ds.GetValue(i, "TIPI");
                    // object currentType = ds.GetValue(i, "TIPINR");
                    if (currentType != null && Convert.ToInt32(currentType.ToString()) == 1)
                        customValue += Convert.ToDouble(ds.GetValue(i, "VLERA"));
                    else
                        customValue -= Convert.ToDouble(ds.GetValue(i, "VLERA"));
                }
                e.DisplayText = String.Format("{0:#,#.00}", customValue);
            }
        }


   

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>

        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                      System.Reflection.Assembly.Load("App_GlobalResources"));
            
            xrLabel12.Text = rm.GetString("RaportListëpagesaStandarteDinamikeTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel2.Text = rm.GetString("filterKodDep", ci);
            xrLabel4.Text = rm.GetString("filterKodNenDep", ci);
            xrLabel8.Text = rm.GetString("labelFilterKryesorMuaji", ci);
            xrLabel6.Text = rm.GetString("labelRaportNrListepagese", ci);
             xrLabel35.Text = rm.GetString("labelLogoIMB", ci);

        }

    }
}
