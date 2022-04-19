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
namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_CmimeShitje_ArtikullisipasAutorizimeve : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_CmimeShitje_ArtikullisipasAutorizimeve(){InitializeComponent();} 

        public Rap_CmimeShitje_ArtikullisipasAutorizimeve(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_CmimeShitje_ArtikullisipasAutorizimeve(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, XtraReport report)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            InitializeComponent();
            EmrateLabelave(ci);
            Ndermarja.Value = report.Parameters[0].Value;
            Monedha.Value = report.Parameters[1].Value;
            Kartela.Value = report.Parameters[2].Value;
            Grupim1.Value = report.Parameters[6].Value;
            Grupim2.Value = report.Parameters[7].Value;
            DtDok.Value = report.Parameters[4].Value;
            Cmimi.Value = report.Parameters[5].Value;
            PershkrimArt.Value = report.Parameters[8].Value;
            // Percakton kolonat e pivot grides
            XRPivotGridField fieldKartela = new XRPivotGridField("Kartela", PivotArea.RowArea);
            fieldKartela.FieldName = "KODARTIKULLI";
            fieldKartela.Caption = rm.GetString("labelKartela", ci);
            
            XRPivotGridField fieldKodbari = new XRPivotGridField("Kodbari", PivotArea.RowArea);
            fieldKodbari.FieldName = "KODBARI";
            fieldKodbari.Caption = rm.GetString("labelFilterAvancuarKodbari", ci);
            XRPivotGridField fieldPershkrimArtikulli = new XRPivotGridField("Emertimi", PivotArea.RowArea);
            fieldPershkrimArtikulli.FieldName = "PERSHKRIMARTIKULLI";
            fieldPershkrimArtikulli.Caption = rm.GetString("labelRaportiEmertimi", ci);
            fieldPershkrimArtikulli.Appearance.FieldValue.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            fieldPershkrimArtikulli.ColumnValueLineCount = 3;
            XRPivotGridField fieldNjesia = new XRPivotGridField("Njesia", PivotArea.RowArea);

            if (Grupim2.Value.ToString()!= "" && Convert.ToInt32(Grupim2.Value.ToString()) == 2)
            {
                fieldNjesia.FieldName = "njesi2";
               
            }
            else fieldNjesia.FieldName = "KODNJESIA";
            fieldNjesia.Caption = rm.GetString("labelNjesia", ci); 
            XRPivotGridField fieldNivelCmimi = new XRPivotGridField("Cmimet", PivotArea.ColumnArea);
            fieldNivelCmimi.FieldName = "KODNIVELCMIMI";
            fieldNivelCmimi.Caption = rm.GetString("labelRaportiCmimet", ci); 
            XRPivotGridField fieldVleraCmimi = new XRPivotGridField("Cmimet", PivotArea.DataArea);
            if (Grupim2.Value.ToString() != "" && Convert.ToInt32(Grupim2.Value.ToString()) == 2)
            {
                //CmimiValue.Text = rm.GetString("cmbboxItemFilterAvancCmimiD", ci); 
                fieldVleraCmimi.FieldName = "CMIMI2";
            }
            else
            {
                //CmimiValue.Text = rm.GetString("cmbboxItemFilterAvancCmimiP", ci); 
                fieldVleraCmimi.FieldName = "CMIMI";
            }
            fieldVleraCmimi.Caption = rm.GetString("labelRaportiCmimet", ci);
            fieldVleraCmimi.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            fieldVleraCmimi.CellFormat.FormatString = "F02";
            cmimetPivotGrid.Fields.AddRange(new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField[] {fieldKartela, fieldKodbari, fieldPershkrimArtikulli,
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


            xrLabel12.Text = rm.GetString("raportListeCmimeshShitjeSIpasAutorizimeveTeArtikujveTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel35.Text = rm.GetString("labelLogoIMB", ci);
        }
    }
}
