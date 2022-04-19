using System;
using System.Collections;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPivotGrid;
using DevExpress.XtraReports.UI.PivotGrid;
using System.Globalization;
using System.Resources;
namespace AlphaWebReports.RaportetDs.ListPagesat.Raportet
{
    public partial class Rap_ListepagesaStandarteDinamikeTotale : XtraReport
    {
        public Rap_ListepagesaStandarteDinamikeTotale() { InitializeComponent(); }
     
        private readonly CultureInfo _ci;
        public Rap_ListepagesaStandarteDinamikeTotale(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) :
            this(param.Ci, param.IdNdermarrje, report)
        {

        }
        
        public Rap_ListepagesaStandarteDinamikeTotale(CultureInfo ci, int idNdermarrje, XtraReport raport)
        {
            var rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            _ci = ci;
            InitializeComponent();
            EmrateLabelave(ci);
            Ndermarja.Value = raport.Parameters[0].Value;
            Monedha.Value = raport.Parameters[1].Value;
            DtDok.Value = raport.Parameters[2].Value;
            DtRegj.Value = raport.Parameters[3].Value;
            NrDok.Value = raport.Parameters[4].Value;
            Muaji.Value = (raport.Parameters["filterMuaji"].Value.ToString());
            KodiPunonjesit.Value = raport.Parameters[6].Value;
            Departamenti.Value = raport.Parameters[7].Value;
            NenDepartamenti.Value = raport.Parameters[8].Value;

            // Percakton kolonat e pivot grides

            var fieldNrPunonjesi = new XRPivotGridField("Nr. Personal", PivotArea.RowArea)
            {
                FieldName = "NRPERSONAL",
                Caption = rm.GetString("labelRaportNrPersonal", ci),
                Width = 45,
                SortMode = PivotSortMode.Custom
            };

            var fieldEmerPunonjesi = new XRPivotGridField("Emri", PivotArea.RowArea)
            {
                FieldName = "EMER",
                Caption = rm.GetString("labelRaportEmri", ci),
                Width = 40
            };

            var fieldMbiemerPunonjesi = new XRPivotGridField("Mbiemri", PivotArea.RowArea)
            {
                FieldName = "MBIEMER",
                Caption = rm.GetString("labelRaportMbiemri", ci),
                Width = 40
            };

            var fieldTipiKomponentes = new XRPivotGridField("", PivotArea.ColumnArea)
            {
                FieldName = "TIPI",
                Caption = rm.GetString("labelFilterAvancuarTipi", ci)
            };

            var fieldKomponente = new XRPivotGridField("", PivotArea.ColumnArea)
            {
                FieldName = "KODI", //ci.ToString() == "sq-AL" ? "PERSHKRIMKOMPONENTE" : "PERSHKRIMKOMPONENTE_ENG",
                Caption = "",
                SortMode = PivotSortMode.Custom,
                Width = 62
            };

            var fieldVleraKompon = new XRPivotGridField("", PivotArea.DataArea)
            {
                FieldName = "VLERA",
                Caption = ""
            };
            fieldVleraKompon.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            fieldVleraKompon.CellFormat.FormatString = "{0:#,#.00}";
            fieldVleraKompon.Options.ShowGrandTotal = false;

            var fieldIdKokaListepagese = new XRPivotGridField("IDKOKA", PivotArea.FilterArea)
            {
                FieldName = "IDKOKA",
                Visible = false
            };

            var fieldNrTipiKomponentes = new XRPivotGridField("TIPINR", PivotArea.ColumnArea)
            {
                FieldName = "TIPINR",
                Visible = false
            };

            var fieldrownr = new XRPivotGridField("rownr", PivotArea.ColumnArea)
            {
                FieldName = "rownr",
                Visible = false
            };

            var fieldPagaNeto = new XRPivotGridField
            {
                Area = PivotArea.DataArea,
                FieldName = "PagaNeto",
                Caption = rm.GetString("labelRaportPagaNeto", ci)
            };
            fieldPagaNeto.Options.ShowValues = false;
            fieldPagaNeto.Options.ShowTotals = false;
            fieldPagaNeto.Options.ShowGrandTotal = true;
            fieldPagaNeto.UseNativeFormat = DevExpress.Utils.DefaultBoolean.False;
            fieldPagaNeto.CellFormat.FormatString = "{0:#,#.00}";

            //Shton kolonat ne pivot gride
            xrPivotGrid2.Fields.AddRange(new[] {fieldNrPunonjesi, fieldEmerPunonjesi, fieldMbiemerPunonjesi,
                fieldTipiKomponentes, fieldKomponente, fieldVleraKompon, fieldrownr, fieldIdKokaListepagese, fieldNrTipiKomponentes, fieldPagaNeto});
        }


        private void xrPivotGrid1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("IDKOKA") == null)
                return;

            var idListePagesa = Convert.ToInt32(GetCurrentColumnValue("IDKOKA").ToString());
            xrPivotGrid2.Prefilter.Criteria = DevExpress.Data.Filtering.CriteriaOperator.Parse("[IDKOKA] = ? ", idListePagesa);
        }

        private string KtheMuajin(string idMuaji)
        {
            var muajiStr = "";
            switch (idMuaji)
            {
                case "1":
                    muajiStr = "Janar";
                    break;
                case "2":
                    muajiStr = "Shkurt";
                    break;
                case "3":
                    muajiStr = "Mars";
                    break;
                case "4":
                    muajiStr = "Prill";
                    break;
                case "5":
                    muajiStr = "Maj";
                    break;
                case "6":
                    muajiStr = "Qershor";
                    break;
                case "7":
                    muajiStr = "Korrik";
                    break;
                case "8":
                    muajiStr = "Gusht";
                    break;
                case "9":
                    muajiStr = "Shtator";
                    break;
                case "10":
                    muajiStr = "Tetor";
                    break;
                case "11":
                    muajiStr = "Nentor";
                    break;
                case "12":
                    muajiStr = "Dhjetor";
                    break;
            }

            return muajiStr;
        }


        private void xrLabel9_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("MUAJI") == null)
                return;

            xrLabel9.Text = KtheMuajin(GetCurrentColumnValue("MUAJI").ToString());
        }

        private void xrPivotGrid1_FieldValueDisplayText(object sender, PivotFieldDisplayTextEventArgs e)
        {
            if (e.ValueType == PivotGridValueType.Total && e.DisplayText == "1 Total")
                e.DisplayText = "Shuma Bruto";

            if (e.ValueType == PivotGridValueType.Total && e.DisplayText == "2 Total")
                e.DisplayText = "Shuma e Ndalesave";
            if (e.Field?.ToString() == "KODI")
            {
                var ds = e.CreateDrillDownDataSource();
                for (int i = 0; i < ds.RowCount; i++)
                {
                    e.DisplayText = ds.GetValue(i, _ci.ToString() == "sq-AL" ? "PERSHKRIMKOMPONENTE" : "PERSHKRIMKOMPONENTE_ENG").ToString();
                }
            }
        }

        /// <summary>
        /// LLogarit pagen neto te punonjesit si diference midis pagesave dhe ndalesave
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xrPivotGrid2_CustomCellDisplayText(object sender, PivotCellDisplayTextEventArgs e)
        {
            if (e.DataField.FieldName == "PagaNeto")
            {
                var ds = e.CreateDrillDownDataSource();
                double customValue = 0;

                for (int i = 0; i < ds.RowCount; i++)
                {
                    object currentType = ds.GetValue(i, "TIPI");
                    if (currentType != null && Convert.ToInt32(currentType.ToString()) == 1)
                        customValue += Convert.ToDouble(ds.GetValue(i, "VLERA"));
                    else
                        customValue -= Convert.ToDouble(ds.GetValue(i, "VLERA"));
                }

                e.DisplayText = string.Format("{0:#,#.00}", customValue);
            }
        }

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            var rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel12.Text = rm.GetString("RaportListëpagesaStandarteDinamikeTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel2.Text = rm.GetString("filterKodDep", ci);
            xrLabel4.Text = rm.GetString("filterKodNenDep", ci);
            xrLabel8.Text = rm.GetString("labelFilterKryesorMuaji", ci);
            xrLabel6.Text = rm.GetString("labelRaportNrListepagese", ci);
            xrLabel35.Text = rm.GetString("labelLogoIMB", ci);
        }

        private void xrPivotGrid2_CustomFieldSort(object sender, PivotGridCustomFieldSortEventArgs e)
        {
            if (e.Field.FieldName == "NRPERSONAL")
            {
                object orderValue1 = e.GetListSourceColumnValue(e.ListSourceRowIndex1, "NRRENDOR"),
                    orderValue2 = e.GetListSourceColumnValue(e.ListSourceRowIndex2, "NRRENDOR");
                int comparison = Comparer.Default.Compare(orderValue1, orderValue2);
                /*nese krahasimi jep 0, pra kane te njejtin numer rendor  xrPivotGrid i grupon rezultatet
                 * dhe dalin keq numrat personale te punonjesve. Ndaj bejme krahasimin e dyte qe garanton
                 * qe NRPERSONAL eshte ndryshe
                 */
                if (comparison == 0)
                {
                    orderValue1 = e.GetListSourceColumnValue(e.ListSourceRowIndex1, "NRPERSONAL");
                    orderValue2 = e.GetListSourceColumnValue(e.ListSourceRowIndex2, "NRPERSONAL");
                    comparison = Comparer.Default.Compare(orderValue1, orderValue2);
                }

                e.Result = comparison;
                e.Handled = true;
                return;
            }
            
              if (e.Field.FieldName == "KODI")
              {
                    object orderValue1 = e.GetListSourceColumnValue(e.ListSourceRowIndex1, "rownr"),
                          orderValue2 = e.GetListSourceColumnValue(e.ListSourceRowIndex2, "rownr");
                    int comparison = Comparer.Default.Compare(orderValue1, orderValue2);

                    if (comparison == 0)
                    {

                        orderValue1 = e.GetListSourceColumnValue(e.ListSourceRowIndex1, "KODI");
                        orderValue2 = e.GetListSourceColumnValue(e.ListSourceRowIndex2, "KODI");
                        comparison = Comparer.Default.Compare(orderValue1, orderValue2);

                    }


                    e.Result = comparison;
                    e.Handled = true;
                    return;
              }
                    
        }

        private void Rap_ListepagesaStandarteDinamikeTotale_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBox1.Image = AlphaWebReports.raporteUtil.MerrLogoNdermarrje(this.Extensions["ndermarrjeLogo"]);
        }
    }
}