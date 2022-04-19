namespace AlphaWebReports.RaportetDs.Amortizimi
{
    partial class Rap_PrintimiSerialeve
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
            DevExpress.XtraPrinting.BarCode.Code128Generator code128Generator1 = new DevExpress.XtraPrinting.BarCode.Code128Generator();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrBarCode1 = new DevExpress.XtraReports.UI.XRBarCode();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.parameter12 = new DevExpress.XtraReports.Parameters.Parameter();
            this.parameter11 = new DevExpress.XtraReports.Parameters.Parameter();
            this.parameter7 = new DevExpress.XtraReports.Parameters.Parameter();
            this.parameter6 = new DevExpress.XtraReports.Parameters.Parameter();
            this.parameter1 = new DevExpress.XtraReports.Parameters.Parameter();
            this.parameter2 = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrControlStyle1 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.GrupimTop1 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.TitulliRaport = new DevExpress.XtraReports.UI.XRControlStyle();
            this.TabelaKryesore = new DevExpress.XtraReports.UI.XRControlStyle();
            this.PermbajtjaRaport = new DevExpress.XtraReports.UI.XRControlStyle();
            this.GrupimShuma1 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.FiltratKoka = new DevExpress.XtraReports.UI.XRControlStyle();
            this.FiltratPermbajtja = new DevExpress.XtraReports.UI.XRControlStyle();
            this.Copyright = new DevExpress.XtraReports.UI.XRControlStyle();
            this.ds_Rap_TabelaAmortizimit1 = new AlphaWebReports.RaportetDs.Amortizimi.Ds_Rap_TabelaAmortizimit();
            this.pRC_RAP_AMORTIZIMI_TABELATableAdapter = new AlphaWebReports.RaportetDs.Amortizimi.Ds_Rap_TabelaAmortizimitTableAdapters.PRC_RAP_AMORTIZIMI_TABELATableAdapter();
            this.ds_Rap_PrintimiSerialeve1 = new AlphaWebReports.RaportetDs.Amortizimi.Ds_Rap_PrintimiSerialeve();
            this.pRC_RAP_AMORTIZIMI_PRINTIMI_SERIALEVETableAdapter = new AlphaWebReports.RaportetDs.Amortizimi.Ds_Rap_PrintimiSerialeveTableAdapters.PRC_RAP_AMORTIZIMI_PRINTIMI_SERIALEVETableAdapter();
            this.xrPanel1 = new DevExpress.XtraReports.UI.XRPanel();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_Rap_TabelaAmortizimit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_Rap_PrintimiSerialeve1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dot;
            this.Detail.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel1});
            this.Detail.HeightF = 119.676F;
            this.Detail.KeepTogether = true;
            this.Detail.MultiColumn.Layout = DevExpress.XtraPrinting.ColumnLayout.AcrossThenDown;
            this.Detail.MultiColumn.Mode = DevExpress.XtraReports.UI.MultiColumnMode.UseColumnCount;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.StylePriority.UseBorderDashStyle = false;
            this.Detail.StylePriority.UseBorders = false;
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrBarCode1
            // 
            this.xrBarCode1.AutoModule = true;
            this.xrBarCode1.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrBarCode1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "PRC_RAP_AMORTIZIMI_PRINTIMI_SERIALEVE.barKodi")});
            this.xrBarCode1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 34.49071F);
            this.xrBarCode1.Module = 3F;
            this.xrBarCode1.Name = "xrBarCode1";
            this.xrBarCode1.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 10, 0, 0, 100F);
            this.xrBarCode1.ShowText = false;
            this.xrBarCode1.SizeF = new System.Drawing.SizeF(200F, 30.50928F);
            this.xrBarCode1.StylePriority.UseBorders = false;
            code128Generator1.CharacterSet = DevExpress.XtraPrinting.BarCode.Code128Charset.CharsetAuto;
            this.xrBarCode1.Symbology = code128Generator1;
            this.xrBarCode1.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrBarCode1_BeforePrint);
            // 
            // xrTable1
            // 
            this.xrTable1.KeepTogether = true;
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0.4906972F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1,
            this.xrTableRow3});
            this.xrTable1.SizeF = new System.Drawing.SizeF(200F, 34F);
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell2});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "PRC_RAP_AMORTIZIMI_PRINTIMI_SERIALEVE.KODARTIKULLI")});
            this.xrTableCell1.Font = new System.Drawing.Font("Calibri", 8F);
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseBorders = false;
            this.xrTableCell1.StylePriority.UseFont = false;
            this.xrTableCell1.Weight = 0.88251670837402341D;
            this.xrTableCell1.WordWrap = false;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "PRC_RAP_AMORTIZIMI_PRINTIMI_SERIALEVE.AQTSERIALKOD")});
            this.xrTableCell2.Font = new System.Drawing.Font("Calibri", 7F);
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseBorders = false;
            this.xrTableCell2.StylePriority.UseFont = false;
            this.xrTableCell2.Weight = 1.1174832916259767D;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell7});
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Weight = 1D;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "PRC_RAP_AMORTIZIMI_PRINTIMI_SERIALEVE.PERSHKRIMARTIKULLI")});
            this.xrTableCell7.Font = new System.Drawing.Font("Calibri", 8F);
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.StylePriority.UseBorders = false;
            this.xrTableCell7.StylePriority.UseFont = false;
            this.xrTableCell7.Weight = 2D;
            this.xrTableCell7.WordWrap = false;
            // 
            // TopMargin
            // 
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // parameter12
            // 
            this.parameter12.Description = "Parameter12";
            this.parameter12.Name = "parameter12";
            // 
            // parameter11
            // 
            this.parameter11.Description = "Parameter11";
            this.parameter11.Name = "parameter11";
            // 
            // parameter7
            // 
            this.parameter7.Description = "Parameter7";
            this.parameter7.Name = "parameter7";
            // 
            // parameter6
            // 
            this.parameter6.Description = "Parameter6";
            this.parameter6.Name = "parameter6";
            // 
            // parameter1
            // 
            this.parameter1.Description = "Parameter1";
            this.parameter1.Name = "parameter1";
            // 
            // parameter2
            // 
            this.parameter2.Description = "Parameter2";
            this.parameter2.Name = "parameter2";
            // 
            // xrControlStyle1
            // 
            this.xrControlStyle1.Name = "xrControlStyle1";
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
            // ds_Rap_TabelaAmortizimit1
            // 
            this.ds_Rap_TabelaAmortizimit1.DataSetName = "Ds_Rap_TabelaAmortizimit";
            this.ds_Rap_TabelaAmortizimit1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // pRC_RAP_AMORTIZIMI_TABELATableAdapter
            // 
            this.pRC_RAP_AMORTIZIMI_TABELATableAdapter.ClearBeforeFill = true;
            // 
            // ds_Rap_PrintimiSerialeve1
            // 
            this.ds_Rap_PrintimiSerialeve1.DataSetName = "Ds_Rap_PrintimiSerialeve";
            this.ds_Rap_PrintimiSerialeve1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // pRC_RAP_AMORTIZIMI_PRINTIMI_SERIALEVETableAdapter
            // 
            this.pRC_RAP_AMORTIZIMI_PRINTIMI_SERIALEVETableAdapter.ClearBeforeFill = true;
            // 
            // xrPanel1
            // 
            this.xrPanel1.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Dash;
            this.xrPanel1.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrPanel1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1,
            this.xrBarCode1});
            this.xrPanel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrPanel1.Name = "xrPanel1";
            this.xrPanel1.SizeF = new System.Drawing.SizeF(200F, 98.95834F);
            this.xrPanel1.StylePriority.UseBorderDashStyle = false;
            this.xrPanel1.StylePriority.UseBorders = false;
            // 
            // Rap_PrintimiSerialeve
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin});
            this.DataAdapter = this.pRC_RAP_AMORTIZIMI_PRINTIMI_SERIALEVETableAdapter;
            this.DataMember = "PRC_RAP_AMORTIZIMI_PRINTIMI_SERIALEVE";
            this.DataSource = this.ds_Rap_PrintimiSerialeve1;
            this.Margins = new System.Drawing.Printing.Margins(130, 105, 100, 100);
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.parameter1,
            this.parameter2,
            this.parameter6,
            this.parameter7,
            this.parameter11,
            this.parameter12});
            this.ReportPrintOptions.DetailCountOnEmptyDataSource = 0;
            this.RequestParameters = false;
            this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.xrControlStyle1,
            this.GrupimTop1,
            this.TitulliRaport,
            this.TabelaKryesore,
            this.PermbajtjaRaport,
            this.GrupimShuma1,
            this.FiltratKoka,
            this.FiltratPermbajtja,
            this.Copyright});
            this.Version = "13.2";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_Rap_TabelaAmortizimit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_Rap_PrintimiSerialeve1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.XRControlStyle xrControlStyle1;
        private DevExpress.XtraReports.UI.XRControlStyle GrupimTop1;
        private DevExpress.XtraReports.UI.XRControlStyle TitulliRaport;
        private DevExpress.XtraReports.UI.XRControlStyle TabelaKryesore;
        private DevExpress.XtraReports.UI.XRControlStyle PermbajtjaRaport;
        private DevExpress.XtraReports.UI.XRControlStyle GrupimShuma1;
        private DevExpress.XtraReports.UI.XRControlStyle FiltratKoka;
        private DevExpress.XtraReports.UI.XRControlStyle FiltratPermbajtja;
        private DevExpress.XtraReports.UI.XRControlStyle Copyright;
        private DevExpress.XtraReports.Parameters.Parameter parameter1;
        private DevExpress.XtraReports.Parameters.Parameter parameter2;
        private Ds_Rap_TabelaAmortizimit ds_Rap_TabelaAmortizimit1;
        private Ds_Rap_TabelaAmortizimitTableAdapters.PRC_RAP_AMORTIZIMI_TABELATableAdapter pRC_RAP_AMORTIZIMI_TABELATableAdapter;
        private DevExpress.XtraReports.Parameters.Parameter parameter6;
        private DevExpress.XtraReports.Parameters.Parameter parameter7;
        private DevExpress.XtraReports.Parameters.Parameter parameter11;
        private DevExpress.XtraReports.Parameters.Parameter parameter12;
        private Ds_Rap_PrintimiSerialeve ds_Rap_PrintimiSerialeve1;
        private Ds_Rap_PrintimiSerialeveTableAdapters.PRC_RAP_AMORTIZIMI_PRINTIMI_SERIALEVETableAdapter pRC_RAP_AMORTIZIMI_PRINTIMI_SERIALEVETableAdapter;
        private DevExpress.XtraReports.UI.XRTable xrTable1;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell2;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow3;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell7;
        private DevExpress.XtraReports.UI.XRBarCode xrBarCode1;
        private DevExpress.XtraReports.UI.XRPanel xrPanel1;
    }
}
