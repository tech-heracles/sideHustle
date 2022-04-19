namespace AlphaWebReports.RaportetDs.PasqyratFinaciare.Raporte
{
    partial class Rap_BilanciVertetuesSipasNdermarrjeve
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Rap_BilanciVertetuesSipasNdermarrjeve));
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrPivotGrid1 = new DevExpress.XtraReports.UI.XRPivotGrid();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.GrupimTop1 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.TitulliRaport = new DevExpress.XtraReports.UI.XRControlStyle();
            this.TabelaKryesore = new DevExpress.XtraReports.UI.XRControlStyle();
            this.PermbajtjaRaport = new DevExpress.XtraReports.UI.XRControlStyle();
            this.GrupimShuma1 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.FiltratKoka = new DevExpress.XtraReports.UI.XRControlStyle();
            this.FiltratPermbajtja = new DevExpress.XtraReports.UI.XRControlStyle();
            this.Copyright = new DevExpress.XtraReports.UI.XRControlStyle();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrLabel20 = new DevExpress.XtraReports.UI.XRLabel();
            this.parameter1 = new DevExpress.XtraReports.Parameters.Parameter();
            this.parameter2 = new DevExpress.XtraReports.Parameters.Parameter();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Dpi = 100F;
            this.Detail.HeightF = 0F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // TopMargin
            // 
            this.TopMargin.Dpi = 100F;
            this.TopMargin.HeightF = 18F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.Dpi = 100F;
            this.BottomMargin.HeightF = 0F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPivotGrid1,
            this.xrLabel1});
            this.ReportHeader.Dpi = 100F;
            this.ReportHeader.HeightF = 115F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrPivotGrid1
            // 
            this.xrPivotGrid1.Appearance.Cell.ForeColor = System.Drawing.Color.Black;
            this.xrPivotGrid1.Appearance.FieldHeader.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.xrPivotGrid1.CellStyleName = "PermbajtjaRaport";
            this.xrPivotGrid1.CustomTotalCellStyleName = "PermbajtjaRaport";
            this.xrPivotGrid1.Dpi = 100F;
            this.xrPivotGrid1.FieldHeaderStyleName = "TabelaKryesore";
            this.xrPivotGrid1.FieldValueGrandTotalStyleName = "PermbajtjaRaport";
            this.xrPivotGrid1.FieldValueStyleName = "PermbajtjaRaport";
            this.xrPivotGrid1.FieldValueTotalStyleName = "GrupimShuma1";
            this.xrPivotGrid1.GrandTotalCellStyleName = "GrupimShuma1";
            this.xrPivotGrid1.HeaderGroupLineStyleName = "PermbajtjaRaport";
            this.xrPivotGrid1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 64.99999F);
            this.xrPivotGrid1.Name = "xrPivotGrid1";
            this.xrPivotGrid1.OptionsPrint.FilterSeparatorBarPadding = 3;
            this.xrPivotGrid1.OptionsPrint.MergeColumnFieldValues = false;
            this.xrPivotGrid1.OptionsPrint.MergeRowFieldValues = false;
            this.xrPivotGrid1.OptionsPrint.PrintHeadersOnEveryPage = true;
            this.xrPivotGrid1.OptionsView.ShowColumnGrandTotalHeader = false;
            this.xrPivotGrid1.OptionsView.ShowColumnHeaders = false;
            this.xrPivotGrid1.OptionsView.ShowColumnTotals = false;
            this.xrPivotGrid1.OptionsView.ShowDataHeaders = false;
            this.xrPivotGrid1.OptionsView.ShowFilterHeaders = false;
            this.xrPivotGrid1.OptionsView.ShowRowGrandTotalHeader = false;
            this.xrPivotGrid1.OptionsView.ShowRowGrandTotals = false;
            this.xrPivotGrid1.OptionsView.ShowRowTotals = false;
            this.xrPivotGrid1.Scripts.OnBeforePrint = "xrPivotGrid1_BeforePrint";
            this.xrPivotGrid1.Scripts.OnCustomCellDisplayText = "xrPivotGrid1_CustomCellDisplayText";
            this.xrPivotGrid1.Scripts.OnCustomSummary = "xrPivotGrid1_CustomSummary";
            this.xrPivotGrid1.SizeF = new System.Drawing.SizeF(898F, 50F);
            // 
            // xrLabel1
            // 
            this.xrLabel1.Dpi = 100F;
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(898F, 65F);
            this.xrLabel1.StyleName = "TitulliRaport";
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "Bilanci vertetues sipas ndermarrjeve";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
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
            this.TitulliRaport.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
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
            this.TabelaKryesore.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // PermbajtjaRaport
            // 
            this.PermbajtjaRaport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.PermbajtjaRaport.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PermbajtjaRaport.ForeColor = System.Drawing.Color.Black;
            this.PermbajtjaRaport.Name = "PermbajtjaRaport";
            this.PermbajtjaRaport.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
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
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel20});
            this.ReportFooter.Dpi = 100F;
            this.ReportFooter.HeightF = 50F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // xrLabel20
            // 
            this.xrLabel20.Dpi = 100F;
            this.xrLabel20.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel20.ForeColor = System.Drawing.Color.Empty;
            this.xrLabel20.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLabel20.Multiline = true;
            this.xrLabel20.Name = "xrLabel20";
            this.xrLabel20.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel20.SizeF = new System.Drawing.SizeF(898F, 50F);
            this.xrLabel20.StyleName = "Copyright";
            this.xrLabel20.StylePriority.UseTextAlignment = false;
            this.xrLabel20.Text = "Copyright © IMB\r\nInstituti i Modelimeve ne Biznes \r\nwww.imb.al";
            this.xrLabel20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomLeft;
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
            // Rap_BilanciVertetuesSipasNdermarrjeve
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.ReportFooter});
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(100, 14, 18, 0);
            this.PageHeight = 850;
            this.PageWidth = 1100;
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.parameter1,
            this.parameter2});
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
            this.Version = "15.2";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
        private DevExpress.XtraReports.UI.XRPivotGrid xrPivotGrid1;
        private DevExpress.XtraReports.UI.XRControlStyle GrupimTop1;
        private DevExpress.XtraReports.UI.XRControlStyle TitulliRaport;
        private DevExpress.XtraReports.UI.XRControlStyle TabelaKryesore;
        private DevExpress.XtraReports.UI.XRControlStyle PermbajtjaRaport;
        private DevExpress.XtraReports.UI.XRControlStyle GrupimShuma1;
        private DevExpress.XtraReports.UI.XRControlStyle FiltratKoka;
        private DevExpress.XtraReports.UI.XRControlStyle FiltratPermbajtja;
        private DevExpress.XtraReports.UI.XRControlStyle Copyright;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.XRLabel xrLabel20;
        private DevExpress.XtraReports.Parameters.Parameter parameter1;
        private DevExpress.XtraReports.Parameters.Parameter parameter2;
    }
}
