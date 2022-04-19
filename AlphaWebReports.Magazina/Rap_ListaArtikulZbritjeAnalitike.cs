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
    public partial class Rap_ListaArtikulZbritjeAnalitike : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_ListaArtikulZbritjeAnalitike(){InitializeComponent();} 
        public Rap_ListaArtikulZbritjeAnalitike(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_ListaArtikulZbritjeAnalitike(CultureInfo ci, int idNdermarrje, int idViti, XtraReport report)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",System.Reflection.Assembly.Load("App_GlobalResources"));
            InitializeComponent();
            EmrateLabelave(ci);
            parameter1.Value = report.Parameters[0].Value;
            parameter2.Value = report.Parameters[1].Value;
            parameter3.Value = report.Parameters[2].Value;
            parameter4.Value = report.Parameters[4].Value;          
            parameter5.Value = report.Parameters[3].Value;
            parameter8.Value = report.Parameters[5].Value;
          
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
            //fieldPershkrimArtikulli.Width = 100;
            fieldPershkrimArtikulli.Caption = rm.GetString("labelRaportiPershkrimi", ci);

            XRPivotGridField fieldGrupim1 = new XRPivotGridField("Grupimi 1", PivotArea.RowArea);
            fieldGrupim1.FieldName = "GRUPIM1";
            fieldGrupim1.Caption = rm.GetString("labelGrupimKlientPare", ci);

            XRPivotGridField fieldGrupim2 = new XRPivotGridField("Grupimi 2", PivotArea.RowArea);
            fieldGrupim2.FieldName = "GRUPIM2";
            fieldGrupim2.Caption = rm.GetString("labelGrupimKlientDyte", ci);

            XRPivotGridField fieldGjendja = new XRPivotGridField("GJENDJASASI", PivotArea.RowArea);
            fieldGjendja.FieldName = "GJENDJESASI";
            fieldGjendja.Caption = rm.GetString("labelFilterAvancuarGjendja", ci);

            XRPivotGridField fieldKosto = new XRPivotGridField("KOSTO", PivotArea.RowArea);
            fieldKosto.FieldName = "KOSTO";
            fieldKosto.Caption = rm.GetString("koloneKosto", ci);

            XRPivotGridField fieldVleraCmimi = new XRPivotGridField("CMIMI", PivotArea.RowArea);
            fieldVleraCmimi.FieldName = "CMIMI";
            fieldVleraCmimi.Caption = rm.GetString("lblcmShitje", ci);
         

            XRPivotGridField fieldNivelZbritje = new XRPivotGridField("PERSHKRIMNIVELZBRITJE", PivotArea.ColumnArea);
            fieldNivelZbritje.FieldName = "PERSHKRIMNIVELZBRITJE";
            fieldNivelZbritje.Caption = rm.GetString("MenuItemZbritjeAnalitike", ci);

            XRPivotGridField fieldVleraZbritje = new XRPivotGridField("Zbritje", PivotArea.DataArea);              
            fieldVleraZbritje.FieldName = "ZBRITJA";


            fieldVleraZbritje.Caption = rm.GetString("cmbCmimeArtikulliZbritje", ci);
            fieldVleraZbritje.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            fieldVleraZbritje.CellFormat.FormatString = "#,#.00";
            cmimetPivotGrid.Fields.AddRange(new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField[] { fieldKartela,fieldKodbari, fieldPershkrimArtikulli,fieldGrupim1,fieldGrupim2,fieldGjendja,
            fieldKosto,fieldVleraCmimi, fieldNivelZbritje, fieldVleraZbritje });
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

             xrLabel12.Text = rm.GetString("RaportListaArtikujveZbAnalitikeTitull", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel35.Text = rm.GetString("labelLogoIMB", ci);
         

        }

      

    }
}
