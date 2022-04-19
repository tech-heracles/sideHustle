using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using DevExpress.XtraReports.UI.PivotGrid;
using DevExpress.XtraPivotGrid;
using System.Reflection;

namespace AlphaWebReports.RaportetDs.Magazina
{
    public partial class Rap_GjendjaCmimeShtijeArtikuj : DevExpress.XtraReports.UI.XtraReport
    {
     


        public Rap_GjendjaCmimeShtijeArtikuj()
        {
            InitializeComponent();

        }
        public Rap_GjendjaCmimeShtijeArtikuj(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_GjendjaCmimeShtijeArtikuj(CultureInfo ci, int idNdermarrje, int idViti, XtraReport report)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                            System.Reflection.Assembly.Load("App_GlobalResources"));
            InitializeComponent();
            EmrateLabelave(ci);
            parameter1.Value = report.Parameters["IdNdermarje"].Value;
            parameter2.Value = report.Parameters["filterKartela"].Value;
            parameter3.Value = report.Parameters["filterFurnitorArt"].Value;

            parameter4.Value = report.Parameters["filterCmimeArtikulli"].Value;

            parameter5.Value = report.Parameters["filterGjendjeZero"].Value;
            parameter8.Value = report.Parameters["filterNivelCmimi"].Value;
            parameter6.Value = report.Parameters["filterkodifikimartP"].Value;
            parameter7.Value = report.Parameters["filterkodifikimartD"].Value;
            

            XRPivotGridField fieldKartela = new XRPivotGridField("Kartela", PivotArea.RowArea);
            fieldKartela.FieldName = "KODARTIKULLI";
            fieldKartela.Caption = rm.GetString("labelKartela", ci);


            XRPivotGridField fieldKodbari = new XRPivotGridField("Kodbari", PivotArea.RowArea);
            fieldKodbari.FieldName = "kodbari";
            fieldKodbari.Caption = rm.GetString("labelFilterAvancuarKodbari", ci);

            XRPivotGridField fieldPartNUmber = new XRPivotGridField("Part Number", PivotArea.RowArea);
            fieldPartNUmber.FieldName = "part_number";
            fieldPartNUmber.Caption = "Part Number";

            XRPivotGridField fieldPershkrimArtikulli = new XRPivotGridField("Pershkrim", PivotArea.RowArea);
            fieldPershkrimArtikulli.FieldName = "PERSHKRIMARTIKULLI";
            fieldPershkrimArtikulli.Caption = rm.GetString("labelRaportiPershkrimi", ci);


            XRPivotGridField fieldGjendja = new XRPivotGridField("GJENDJASASI", PivotArea.RowArea);
            fieldGjendja.FieldName = "GJENDJESASI";
            fieldGjendja.Caption = rm.GetString("labelFilterAvancuarGjendja", ci); 




            //XRPivotGridField fieldNjesia = new XRPivotGridField("Njesia", PivotArea.RowArea);
            //if (Convert.ToInt32(Grupim2.Value.ToString()) == 1)
            //{
            //    fieldNjesia.FieldName = "KODNJESIA";
            //}
            //else fieldNjesia.FieldName = "njesi2";
            //fieldNjesia.Caption = rm.GetString("labelNjesia", ci);
            XRPivotGridField fieldNivelCmimi = new XRPivotGridField("Cmimet", PivotArea.ColumnArea);
            fieldNivelCmimi.FieldName = "PERSHKRIMNIVELCMIMI";
            fieldNivelCmimi.Caption = rm.GetString("labelRaportiCmimet", ci);
            XRPivotGridField fieldVleraCmimi = new XRPivotGridField("Cmimet", PivotArea.DataArea);
            if (report.Parameters["filterCmimeArtikulli"].Value.ToString() == "Çmimi dytë")
            {
                report.Parameters["filterCmimeArtikulli"].Value = rm.GetString("cmbboxItemFilterAvancCmimiD", ci);
                fieldVleraCmimi.FieldName = "CMIMI2";
            }
            else
            {

                report.Parameters["filterCmimeArtikulli"].Value = rm.GetString("cmbboxItemFilterAvancCmimiP", ci);
                fieldVleraCmimi.FieldName = "CMIMI";
            }
            
            fieldVleraCmimi.Caption = rm.GetString("labelRaportiCmimet", ci);
            fieldVleraCmimi.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            fieldVleraCmimi.CellFormat.FormatString = "#,#.00";
            cmimetPivotGrid.Fields.AddRange(new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField[] { fieldKartela,fieldKodbari,fieldPartNUmber, fieldPershkrimArtikulli,
                
                fieldGjendja,

                fieldNivelCmimi, 
                fieldVleraCmimi
            });

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

            xrLabel12.Text = rm.GetString("RaportGjendjaDheCmimetEShitjesSeArtikujveTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel35.Text = rm.GetString("labelLogoIMB", ci);
         

        }

      

    }
}
