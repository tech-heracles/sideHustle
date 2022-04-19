//using DevExpress.XtraReports.UI.PivotGrid;

namespace AlphaWebReports.RaportetDs.KlientFurnitor.Raporte
{
    partial class Rap_DetyrimiKlienteveSipasMuajve
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Rap_DetyrimiKlienteveSipasMuajve));
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.xrPivotGrid1 = new DevExpress.XtraReports.UI.XRPivotGrid();
            this.pRC_Rap_DetyrimiKlienteveSipasMuajveTableAdapter = new AlphaWebReports.RaportetDs.KlientFurnitor.DataSource.Ds_Rap_DetyrimiKlienteveSipasMuajveTableAdapters.PRC_Rap_DetyrimiKlienteveSipasMuajveTableAdapter();
            this.ds_Rap_DetyrimiKlienteveSipasMuajve1 = new AlphaWebReports.RaportetDs.KlientFurnitor.DataSource.Ds_Rap_DetyrimiKlienteveSipasMuajve();
            this.fieldmuaj1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldEMERTIMIKF1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldAgjentiEmer1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldMONEDHAKOD1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldtotalikrahasues1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldLIMITBLLOKUES1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldQYTETIEMRI1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel20 = new DevExpress.XtraReports.UI.XRLabel();
            this.GrupimTop1 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.TitulliRaport = new DevExpress.XtraReports.UI.XRControlStyle();
            this.TabelaKryesore = new DevExpress.XtraReports.UI.XRControlStyle();
            this.PermbajtjaRaport = new DevExpress.XtraReports.UI.XRControlStyle();
            this.GrupimShuma1 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.FiltratKoka = new DevExpress.XtraReports.UI.XRControlStyle();
            this.FiltratPermbajtja = new DevExpress.XtraReports.UI.XRControlStyle();
            this.Copyright = new DevExpress.XtraReports.UI.XRControlStyle();
            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            this.prC_Rap_DetyrimiKlienteveSipasMuajveTableAdapter1 = new AlphaWebReports.RaportetDs.KlientFurnitor.DataSource.Ds_Rap_DetyrimiKlienteveSipasMuajveTableAdapters.PRC_Rap_DetyrimiKlienteveSipasMuajveTableAdapter();
            this.ds_Rap_DetyrimiKlienteveSipasMuajve2 = new AlphaWebReports.RaportetDs.KlientFurnitor.DataSource.Ds_Rap_DetyrimiKlienteveSipasMuajve();
            this.parameter6 = new DevExpress.XtraReports.Parameters.Parameter();
            this.parameter5 = new DevExpress.XtraReports.Parameters.Parameter();
            this.parameter4 = new DevExpress.XtraReports.Parameters.Parameter();
            this.parameter3 = new DevExpress.XtraReports.Parameters.Parameter();
            this.parameter2 = new DevExpress.XtraReports.Parameters.Parameter();
            this.parameter1 = new DevExpress.XtraReports.Parameters.Parameter();
            ((System.ComponentModel.ISupportInitialize)(this.ds_Rap_DetyrimiKlienteveSipasMuajve1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_Rap_DetyrimiKlienteveSipasMuajve2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.HeightF = 1.041667F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 21F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 25F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrPivotGrid1
            // 
            this.xrPivotGrid1.Appearance.Cell.ForeColor = System.Drawing.Color.Black;
            this.xrPivotGrid1.CellStyleName = "PermbajtjaRaport";
            this.xrPivotGrid1.CustomTotalCellStyleName = "PermbajtjaRaport";
            this.xrPivotGrid1.DataAdapter = this.pRC_Rap_DetyrimiKlienteveSipasMuajveTableAdapter;
            this.xrPivotGrid1.DataMember = "PRC_Rap_DetyrimiKlienteveSipasMuajve";
            this.xrPivotGrid1.DataSource = this.ds_Rap_DetyrimiKlienteveSipasMuajve1;
            this.xrPivotGrid1.FieldHeaderStyleName = "TabelaKryesore";
            this.xrPivotGrid1.Fields.AddRange(new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField[] {
            this.fieldmuaj1,
            this.fieldEMERTIMIKF1,
            this.fieldAgjentiEmer1,
            this.fieldMONEDHAKOD1,
            this.fieldtotalikrahasues1,
            this.fieldLIMITBLLOKUES1,
            this.fieldQYTETIEMRI1});
            this.xrPivotGrid1.FieldValueGrandTotalStyleName = "PermbajtjaRaport";
            this.xrPivotGrid1.FieldValueStyleName = "PermbajtjaRaport";
            this.xrPivotGrid1.FieldValueTotalStyleName = "GrupimShuma1";
            this.xrPivotGrid1.GrandTotalCellStyleName = "GrupimShuma1";
            this.xrPivotGrid1.HeaderGroupLineStyleName = "PermbajtjaRaport";
            this.xrPivotGrid1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 64.99999F);
            this.xrPivotGrid1.Name = "xrPivotGrid1";
            this.xrPivotGrid1.OptionsPrint.FilterSeparatorBarPadding = 3;
            this.xrPivotGrid1.OptionsPrint.PrintColumnAreaOnEveryPage = true;
            this.xrPivotGrid1.OptionsView.ShowColumnGrandTotalHeader = false;
            this.xrPivotGrid1.OptionsView.ShowColumnGrandTotals = false;
            this.xrPivotGrid1.OptionsView.ShowColumnHeaders = false;
            this.xrPivotGrid1.OptionsView.ShowColumnTotals = false;
            this.xrPivotGrid1.OptionsView.ShowDataHeaders = false;
            this.xrPivotGrid1.OptionsView.ShowFilterHeaders = false;
            this.xrPivotGrid1.Scripts.OnBeforePrint = "xrPivotGrid1_BeforePrint";
            this.xrPivotGrid1.Scripts.OnCustomFieldSort = "xrPivotGrid1_CustomFieldSort";
            this.xrPivotGrid1.Scripts.OnFieldValueDisplayText = "xrPivotGrid1_FieldValueDisplayText";
            this.xrPivotGrid1.SizeF = new System.Drawing.SizeF(1055F, 50F);
            this.xrPivotGrid1.TotalCellStyleName = "GrupimShuma1";
            // 
            // pRC_Rap_DetyrimiKlienteveSipasMuajveTableAdapter
            // 
            this.pRC_Rap_DetyrimiKlienteveSipasMuajveTableAdapter.ClearBeforeFill = true;
            // 
            // ds_Rap_DetyrimiKlienteveSipasMuajve1
            // 
            this.ds_Rap_DetyrimiKlienteveSipasMuajve1.DataSetName = "Ds_Rap_DetyrimiKlienteveSipasMuajve";
            this.ds_Rap_DetyrimiKlienteveSipasMuajve1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // fieldmuaj1
            // 
            this.fieldmuaj1.AreaIndex = 0;
            this.fieldmuaj1.FieldName = "muaji";
            this.fieldmuaj1.Name = "fieldmuaj1";
            this.fieldmuaj1.Options.ShowInFilter = true;
            this.fieldmuaj1.SortMode = DevExpress.XtraPivotGrid.PivotSortMode.Custom;
            // 
            // fieldEMERTIMIKF1
            // 
            this.fieldEMERTIMIKF1.AreaIndex = 1;
            this.fieldEMERTIMIKF1.FieldName = "EMERTIMIKF";
            this.fieldEMERTIMIKF1.Name = "fieldEMERTIMIKF1";
            this.fieldEMERTIMIKF1.Options.ShowInFilter = true;
            // 
            // fieldAgjentiEmer1
            // 
            this.fieldAgjentiEmer1.AreaIndex = 2;
            this.fieldAgjentiEmer1.FieldName = "AgjentiEmer";
            this.fieldAgjentiEmer1.Name = "fieldAgjentiEmer1";
            this.fieldAgjentiEmer1.Options.ShowInFilter = true;
            // 
            // fieldMONEDHAKOD1
            // 
            this.fieldMONEDHAKOD1.AreaIndex = 3;
            this.fieldMONEDHAKOD1.FieldName = "MONEDHAKOD";
            this.fieldMONEDHAKOD1.Name = "fieldMONEDHAKOD1";
            this.fieldMONEDHAKOD1.Options.ShowInFilter = true;
            // 
            // fieldtotalikrahasues1
            // 
            this.fieldtotalikrahasues1.AreaIndex = 4;
            this.fieldtotalikrahasues1.FieldName = "totalikrahasues";
            this.fieldtotalikrahasues1.Name = "fieldtotalikrahasues1";
            this.fieldtotalikrahasues1.Options.ShowInFilter = true;
            // 
            // fieldLIMITBLLOKUES1
            // 
            this.fieldLIMITBLLOKUES1.AreaIndex = 5;
            this.fieldLIMITBLLOKUES1.FieldName = "LIMITBLLOKUES";
            this.fieldLIMITBLLOKUES1.Name = "fieldLIMITBLLOKUES1";
            this.fieldLIMITBLLOKUES1.Options.ShowInFilter = true;
            // 
            // fieldQYTETIEMRI1
            // 
            this.fieldQYTETIEMRI1.AreaIndex = 6;
            this.fieldQYTETIEMRI1.FieldName = "QYTETIEMRI";
            this.fieldQYTETIEMRI1.Name = "fieldQYTETIEMRI1";
            this.fieldQYTETIEMRI1.Options.ShowInFilter = true;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPivotGrid1,
            this.xrLabel1});
            this.ReportHeader.HeightF = 136.4583F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrLabel1
            // 
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(992.9999F, 38.625F);
            this.xrLabel1.StyleName = "TitulliRaport";
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "Detyrimi i klienteve sipas muajve";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel20
            // 
            this.xrLabel20.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel20.ForeColor = System.Drawing.Color.Empty;
            this.xrLabel20.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLabel20.Multiline = true;
            this.xrLabel20.Name = "xrLabel20";
            this.xrLabel20.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel20.SizeF = new System.Drawing.SizeF(992.9999F, 48.45832F);
            this.xrLabel20.StyleName = "Copyright";
            this.xrLabel20.Text = "Copyright © IMB\r\nInstituti i Modelimeve ne Biznes \r\nwww.imb.al";
            // 
            // GrupimTop1
            // 
            this.GrupimTop1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GrupimTop1.ForeColor = System.Drawing.Color.OliveDrab;
            this.GrupimTop1.Name = "GrupimTop1";
            // 
            // TitulliRaport
            // 
            this.TitulliRaport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TitulliRaport.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TitulliRaport.Font = new System.Drawing.Font("Calibri", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitulliRaport.ForeColor = System.Drawing.Color.Green;
            this.TitulliRaport.Name = "TitulliRaport";
            // 
            // TabelaKryesore
            // 
            this.TabelaKryesore.BackColor = System.Drawing.Color.Honeydew;
            this.TabelaKryesore.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.TabelaKryesore.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.TabelaKryesore.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TabelaKryesore.ForeColor = System.Drawing.Color.Green;
            this.TabelaKryesore.Name = "TabelaKryesore";
            // 
            // PermbajtjaRaport
            // 
            this.PermbajtjaRaport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.PermbajtjaRaport.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PermbajtjaRaport.ForeColor = System.Drawing.Color.Black;
            this.PermbajtjaRaport.Name = "PermbajtjaRaport";
            this.PermbajtjaRaport.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // GrupimShuma1
            // 
            this.GrupimShuma1.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GrupimShuma1.ForeColor = System.Drawing.Color.Green;
            this.GrupimShuma1.Name = "GrupimShuma1";
            // 
            // FiltratKoka
            // 
            this.FiltratKoka.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FiltratKoka.ForeColor = System.Drawing.Color.Gray;
            this.FiltratKoka.Name = "FiltratKoka";
            this.FiltratKoka.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.FiltratKoka.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // FiltratPermbajtja
            // 
            this.FiltratPermbajtja.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.FiltratPermbajtja.ForeColor = System.Drawing.Color.Gray;
            this.FiltratPermbajtja.Name = "FiltratPermbajtja";
            // 
            // Copyright
            // 
            this.Copyright.Font = new System.Drawing.Font("Times New Roman", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Copyright.ForeColor = System.Drawing.Color.Green;
            this.Copyright.Name = "Copyright";
            // 
            // PageFooter
            // 
            this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel20});
            this.PageFooter.HeightF = 60.41667F;
            this.PageFooter.Name = "PageFooter";
            // 
            // prC_Rap_DetyrimiKlienteveSipasMuajveTableAdapter1
            // 
            this.prC_Rap_DetyrimiKlienteveSipasMuajveTableAdapter1.ClearBeforeFill = true;
            // 
            // ds_Rap_DetyrimiKlienteveSipasMuajve2
            // 
            this.ds_Rap_DetyrimiKlienteveSipasMuajve2.DataSetName = "Ds_Rap_DetyrimiKlienteveSipasMuajve";
            this.ds_Rap_DetyrimiKlienteveSipasMuajve2.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // parameter6
            // 
            this.parameter6.Description = "Parameter6";
            this.parameter6.Name = "parameter6";
            // 
            // parameter5
            // 
            this.parameter5.Description = "Parameter5";
            this.parameter5.Name = "parameter5";
            // 
            // parameter4
            // 
            this.parameter4.Description = "Parameter4";
            this.parameter4.Name = "parameter4";
            // 
            // parameter3
            // 
            this.parameter3.Description = "Parameter3";
            this.parameter3.Name = "parameter3";
            // 
            // parameter2
            // 
            this.parameter2.Description = "Parameter2";
            this.parameter2.Name = "parameter2";
            // 
            // parameter1
            // 
            this.parameter1.Description = "Parameter1";
            this.parameter1.Name = "parameter1";
            // 
            // Rap_DetyrimiKlienteveSipasMuajve
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.PageFooter,
            this.ReportHeader});
            this.DataMember = "PRC_Rap_DetyrimiKlienteveSipasMuajve";
            this.DataSource = this.ds_Rap_DetyrimiKlienteveSipasMuajve1;
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(100, 7, 21, 25);
            this.PageHeight = 827;
            this.PageWidth = 1169;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.parameter1,
            this.parameter2,
            this.parameter3,
            this.parameter4,
            this.parameter5,
            this.parameter6});
            this.ScriptReferencesString = "AlphaWebReports.Common.dll\r\n";
            this.Scripts.OnBeforePrint = "Rap_DetyrimiKlienteveSipasMuajve_BeforePrint";
            this.ScriptsSource = resources.GetString("$this.ScriptsSource");
            this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.GrupimTop1,
            this.TitulliRaport,
            this.TabelaKryesore,
            this.PermbajtjaRaport,
            this.GrupimShuma1,
            this.FiltratKoka,
            this.FiltratPermbajtja,
            this.Copyright});
            this.Version = "18.2";
            ((System.ComponentModel.ISupportInitialize)(this.ds_Rap_DetyrimiKlienteveSipasMuajve1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_Rap_DetyrimiKlienteveSipasMuajve2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
        private DevExpress.XtraReports.UI.XRControlStyle GrupimTop1;
        private DevExpress.XtraReports.UI.XRControlStyle TitulliRaport;
        private DevExpress.XtraReports.UI.XRControlStyle TabelaKryesore;
        private DevExpress.XtraReports.UI.XRControlStyle PermbajtjaRaport;
        private DevExpress.XtraReports.UI.XRControlStyle GrupimShuma1;
        private DevExpress.XtraReports.UI.XRControlStyle FiltratKoka;
        private DevExpress.XtraReports.UI.XRControlStyle FiltratPermbajtja;
        private DevExpress.XtraReports.UI.XRControlStyle Copyright;
        private DevExpress.XtraReports.UI.XRLabel xrLabel20;
        private DevExpress.XtraReports.Parameters.Parameter parameter6;
        private DevExpress.XtraReports.Parameters.Parameter parameter5;
        private DevExpress.XtraReports.Parameters.Parameter parameter4;
        private DevExpress.XtraReports.Parameters.Parameter parameter3;
        private DevExpress.XtraReports.Parameters.Parameter parameter2;
        private DevExpress.XtraReports.Parameters.Parameter parameter1;
        private DataSource.Ds_Rap_DetyrimiKlienteveSipasMuajveTableAdapters.PRC_Rap_DetyrimiKlienteveSipasMuajveTableAdapter prC_Rap_DetyrimiKlienteveSipasMuajveTableAdapter1;
        private DataSource.Ds_Rap_DetyrimiKlienteveSipasMuajve ds_Rap_DetyrimiKlienteveSipasMuajve1;
        private DataSource.Ds_Rap_DetyrimiKlienteveSipasMuajveTableAdapters.PRC_Rap_DetyrimiKlienteveSipasMuajveTableAdapter pRC_Rap_DetyrimiKlienteveSipasMuajveTableAdapter;
        public DevExpress.XtraReports.UI.XRPivotGrid xrPivotGrid1;
        private DevExpress.XtraReports.UI.PageFooterBand PageFooter;
        private DataSource.Ds_Rap_DetyrimiKlienteveSipasMuajve ds_Rap_DetyrimiKlienteveSipasMuajve2;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
       private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldmuaj1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldAgjentiEmer1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldEMERTIMIKF1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldMONEDHAKOD1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldtotalikrahasues1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldLIMITBLLOKUES1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldQYTETIEMRI1;


    }
}
