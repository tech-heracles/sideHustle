using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPivotGrid;
using DevExpress.XtraReports.UI.PivotGrid;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.Blerje
{
    public partial class Rap_ListeCmimeshBlerje : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_ListeCmimeshBlerje(){InitializeComponent();} 

        public Rap_ListeCmimeshBlerje(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_ListeCmimeshBlerje(CultureInfo ci, int idNdermarrje, int idViti, XtraReport report)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            InitializeComponent();
            EmrateLabelave(ci);
            Ndermarja.Value = report.Parameters["IdNdermarje"].Value;
            Monedha.Value = report.Parameters["filterDtDok1"].Value;
            Kartela.Value = report.Parameters["filterMonedha"].Value;
            Grupim1.Value = report.Parameters["filterKartela"].Value;
            Grupim2.Value = report.Parameters["filterCmimeArtikulli"].Value;
            DtDok.Value = report.Parameters["filterDtDokCmimArtikulli"].Value;
            Cmimi.Value = report.Parameters["filterNivelCmimi"].Value;
            PershkrimArt.Value = report.Parameters["filterkodifikimartP"].Value;
            XRPivotGridField fieldKartela = new XRPivotGridField("Kartela", PivotArea.RowArea);
            fieldKartela.FieldName = "KODARTIKULLI";
            fieldKartela.Caption = rm.GetString("labelKartela", ci);

            XRPivotGridField fieldKodbari = new XRPivotGridField("Kodbari", PivotArea.RowArea);
            fieldKodbari.FieldName = "KODBARI";
            fieldKodbari.Caption = rm.GetString("labelFilterAvancuarKodbari", ci);
            XRPivotGridField fieldPershkrimArtikulli = new XRPivotGridField("Emertimi", PivotArea.RowArea);
            fieldPershkrimArtikulli.FieldName = "PERSHKRIMARTIKULLI";
            fieldPershkrimArtikulli.Caption = rm.GetString("labelRaportiEmertimi", ci);
            XRPivotGridField fieldNjesia = new XRPivotGridField("Njesia", PivotArea.RowArea);
            if (Grupim2.Value.ToString() == "Çmimi parë")
            {
                fieldNjesia.FieldName = "KODNJESIA";
            }
            else fieldNjesia.FieldName = "njesi2";
            fieldNjesia.Caption = rm.GetString("labelNjesia", ci);
            XRPivotGridField fieldNivelCmimi = new XRPivotGridField("Cmimet", PivotArea.ColumnArea);
            fieldNivelCmimi.FieldName = "KODNIVELCMIMI";
            fieldNivelCmimi.Caption = rm.GetString("labelRaportiCmimet", ci);
            XRPivotGridField fieldVleraCmimi = new XRPivotGridField("Cmimet", PivotArea.DataArea);
            if (Grupim2.Value.ToString() == "Çmimi dytë")
            {
                fieldVleraCmimi.FieldName = "CMIMI2";
            }
            else
            {
                fieldVleraCmimi.FieldName = "CMIMI";
            }
            fieldVleraCmimi.Caption = rm.GetString("labelRaportiCmimet", ci);
            fieldVleraCmimi.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            fieldVleraCmimi.CellFormat.FormatString = "F02";
            cmimetPivotGrid.Fields.AddRange(new XRPivotGridField[] {fieldKartela, fieldKodbari, fieldPershkrimArtikulli,
                fieldNjesia, fieldNivelCmimi, fieldVleraCmimi});
            cmimetPivotGrid.Styles.FieldHeaderStyle = cmimetPivotGrid.Styles.FieldHeaderStyle;
            cmimetPivotGrid.Styles.FieldValueStyle = cmimetPivotGrid.Styles.FieldHeaderStyle;
            cmimetPivotGrid.FieldValueGrandTotalStyleName = cmimetPivotGrid.Styles.FieldHeaderStyle.Name;

        }
        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
            System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel12.Text = rm.GetString("RaportListeCmimeshBlerjeTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel35.Text = rm.GetString("labelLogoIMB", ci);
        }
    }
}
