namespace AlphaWebReports.RaportetDs
{
    partial class RapGabimesh
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
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary3 = new DevExpress.XtraReports.UI.XRSummary();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.filtraTable = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
            this.FiltratKoka = new DevExpress.XtraReports.UI.XRControlStyle();
            this.FiltratPermbajtja = new DevExpress.XtraReports.UI.XRControlStyle();
            this.Copyright = new DevExpress.XtraReports.UI.XRControlStyle();
            this.TitulliRaport = new DevExpress.XtraReports.UI.XRControlStyle();
            this.TabelaKryesore = new DevExpress.XtraReports.UI.XRControlStyle();
            this.PermbajtjaRaport = new DevExpress.XtraReports.UI.XRControlStyle();
            this.GrupimShuma1 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.GrupimTop1 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.ds_RapGabimesh1 = new AlphaWebReports.RaportetDs.Ds_RapGabimesh();
            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            this.xrLabel32 = new DevExpress.XtraReports.UI.XRLabel();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this.filtraTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_RapGabimesh1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.filtraTable});
            this.Detail.Dpi = 100F;
            this.Detail.HeightF = 25F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // filtraTable
            // 
            this.filtraTable.Dpi = 100F;
            this.filtraTable.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.filtraTable.ForeColor = System.Drawing.Color.Gray;
            this.filtraTable.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.filtraTable.Name = "filtraTable";
            this.filtraTable.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.filtraTable.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
            this.filtraTable.SizeF = new System.Drawing.SizeF(845.9999F, 25F);
            this.filtraTable.StyleName = "PermbajtjaRaport";
            this.filtraTable.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell2,
            this.xrTableCell3});
            this.xrTableRow3.Dpi = 100F;
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Weight = 1D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.CanShrink = true;
            this.xrTableCell1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Gabime.Kodi")});
            this.xrTableCell1.Dpi = 100F;
            this.xrTableCell1.Multiline = true;
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StyleName = "PermbajtjaRaport";
            this.xrTableCell1.Weight = 1.4650772892121968D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.CanShrink = true;
            this.xrTableCell2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Gabime.Gabimi")});
            this.xrTableCell2.Dpi = 100F;
            this.xrTableCell2.Multiline = true;
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StyleName = "PermbajtjaRaport";
            this.xrTableCell2.Weight = 5.4088741001053506D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.CanShrink = true;
            this.xrTableCell3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Gabime.Rreshti")});
            this.xrTableCell3.Dpi = 100F;
            this.xrTableCell3.Multiline = true;
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.StyleName = "PermbajtjaRaport";
            this.xrTableCell3.Weight = 1.2160496121182787D;
            // 
            // TopMargin
            // 
            this.TopMargin.Dpi = 100F;
            this.TopMargin.HeightF = 2F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.Dpi = 100F;
            this.BottomMargin.HeightF = 100F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel3,
            this.xrLabel2,
            this.xrLabel1,
            this.xrLabel12});
            this.PageHeader.Dpi = 100F;
            this.PageHeader.HeightF = 98.95834F;
            this.PageHeader.Name = "PageHeader";
            // 
            // xrLabel3
            // 
            this.xrLabel3.Dpi = 100F;
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(718.8333F, 75.95834F);
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(127.1667F, 23F);
            this.xrLabel3.StyleName = "TabelaKryesore";
            this.xrLabel3.Text = "Rreshti";
            // 
            // xrLabel2
            // 
            this.xrLabel2.Dpi = 100F;
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(153.2083F, 75.95834F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(565.625F, 23F);
            this.xrLabel2.StyleName = "TabelaKryesore";
            this.xrLabel2.Text = "Gabimi";
            // 
            // xrLabel1
            // 
            this.xrLabel1.Dpi = 100F;
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 75.95834F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(153.2083F, 23F);
            this.xrLabel1.StyleName = "TabelaKryesore";
            this.xrLabel1.Text = "Kodi";
            // 
            // xrLabel12
            // 
            this.xrLabel12.Dpi = 100F;
            this.xrLabel12.Font = new System.Drawing.Font("Calibri", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(128)))));
            this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLabel12.Multiline = true;
            this.xrLabel12.Name = "xrLabel12";
            this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel12.SizeF = new System.Drawing.SizeF(845.9999F, 50F);
            this.xrLabel12.StyleName = "TitulliRaport";
            this.xrLabel12.StylePriority.UseTextAlignment = false;
            this.xrLabel12.Text = "Raporti i gabimeve";
            this.xrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomCenter;
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
            this.Copyright.ForeColor = System.Drawing.Color.CadetBlue;
            this.Copyright.Name = "Copyright";
            // 
            // TitulliRaport
            // 
            this.TitulliRaport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TitulliRaport.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TitulliRaport.Font = new System.Drawing.Font("Calibri", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitulliRaport.ForeColor = System.Drawing.Color.CadetBlue;
            this.TitulliRaport.Name = "TitulliRaport";
            // 
            // TabelaKryesore
            // 
            this.TabelaKryesore.BackColor = System.Drawing.Color.LightBlue;
            this.TabelaKryesore.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.TabelaKryesore.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.TabelaKryesore.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TabelaKryesore.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TabelaKryesore.Name = "TabelaKryesore";
            // 
            // PermbajtjaRaport
            // 
            this.PermbajtjaRaport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.PermbajtjaRaport.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PermbajtjaRaport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.PermbajtjaRaport.Name = "PermbajtjaRaport";
            this.PermbajtjaRaport.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // GrupimShuma1
            // 
            this.GrupimShuma1.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GrupimShuma1.ForeColor = System.Drawing.Color.CadetBlue;
            this.GrupimShuma1.Name = "GrupimShuma1";
            // 
            // GrupimTop1
            // 
            this.GrupimTop1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GrupimTop1.ForeColor = System.Drawing.Color.SteelBlue;
            this.GrupimTop1.Name = "GrupimTop1";
            // 
            // ds_RapGabimesh1
            // 
            this.ds_RapGabimesh1.DataSetName = "Ds_RapGabimesh";
            this.ds_RapGabimesh1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // PageFooter
            // 
            this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel32});
            this.PageFooter.Dpi = 100F;
            this.PageFooter.HeightF = 71.37498F;
            this.PageFooter.Name = "PageFooter";
            // 
            // xrLabel32
            // 
            this.xrLabel32.Dpi = 100F;
            this.xrLabel32.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel32.ForeColor = System.Drawing.Color.Empty;
            this.xrLabel32.LocationFloat = new DevExpress.Utils.PointFloat(0F, 27.08333F);
            this.xrLabel32.Multiline = true;
            this.xrLabel32.Name = "xrLabel32";
            this.xrLabel32.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel32.SizeF = new System.Drawing.SizeF(206.25F, 44.29163F);
            this.xrLabel32.StyleName = "Copyright";
            this.xrLabel32.Text = "Copyright © IMB\r\nInstituti i Modelimeve ne Biznes \r\nwww.imb.al";
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel9,
            this.xrLabel8,
            this.xrLabel7});
            this.ReportFooter.Dpi = 100F;
            this.ReportFooter.HeightF = 48.00002F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // xrLabel9
            // 
            this.xrLabel9.Dpi = 100F;
            this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(0F, 23.95833F);
            this.xrLabel9.Name = "xrLabel9";
            this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel9.SizeF = new System.Drawing.SizeF(59.37501F, 23F);
            this.xrLabel9.StyleName = "GrupimShuma1";
            xrSummary1.Func = DevExpress.XtraReports.UI.SummaryFunc.Count;
            this.xrLabel9.Summary = xrSummary1;
            this.xrLabel9.Text = "U gjeten";
            // 
            // xrLabel8
            // 
            this.xrLabel8.Dpi = 100F;
            this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(106.25F, 23.95833F);
            this.xrLabel8.Name = "xrLabel8";
            this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel8.SizeF = new System.Drawing.SizeF(100F, 23F);
            this.xrLabel8.StyleName = "GrupimShuma1";
            xrSummary2.Func = DevExpress.XtraReports.UI.SummaryFunc.Count;
            this.xrLabel8.Summary = xrSummary2;
            this.xrLabel8.Text = "gabime";
            // 
            // xrLabel7
            // 
            this.xrLabel7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Gabime.Gabimi")});
            this.xrLabel7.Dpi = 100F;
            this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(59.375F, 23.95833F);
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel7.SizeF = new System.Drawing.SizeF(46.87499F, 23F);
            this.xrLabel7.StyleName = "GrupimShuma1";
            this.xrLabel7.StylePriority.UseTextAlignment = false;
            xrSummary3.Func = DevExpress.XtraReports.UI.SummaryFunc.Count;
            xrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrLabel7.Summary = xrSummary3;
            this.xrLabel7.Text = "xrLabel7";
            this.xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // RapGabimesh
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.PageHeader,
            this.PageFooter,
            this.ReportFooter});
            this.DataMember = "Gabime";
            this.DataSource = this.ds_RapGabimesh1;
            this.Margins = new System.Drawing.Printing.Margins(2, 2, 2, 100);
            this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.FiltratKoka,
            this.FiltratPermbajtja,
            this.Copyright,
            this.TitulliRaport,
            this.TabelaKryesore,
            this.PermbajtjaRaport,
            this.GrupimShuma1,
            this.GrupimTop1});
            this.Version = "16.1";
            ((System.ComponentModel.ISupportInitialize)(this.filtraTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_RapGabimesh1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.XRControlStyle FiltratKoka;
        private DevExpress.XtraReports.UI.XRControlStyle FiltratPermbajtja;
        private DevExpress.XtraReports.UI.XRControlStyle Copyright;
        private DevExpress.XtraReports.UI.XRControlStyle TitulliRaport;
        private DevExpress.XtraReports.UI.XRControlStyle TabelaKryesore;
        private DevExpress.XtraReports.UI.XRControlStyle PermbajtjaRaport;
        private DevExpress.XtraReports.UI.XRControlStyle GrupimShuma1;
        private DevExpress.XtraReports.UI.XRControlStyle GrupimTop1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel3;
        private DevExpress.XtraReports.UI.XRLabel xrLabel2;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel12;
        private Ds_RapGabimesh ds_RapGabimesh1;
        private DevExpress.XtraReports.UI.PageFooterBand PageFooter;
        private DevExpress.XtraReports.UI.XRLabel xrLabel32;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.XRLabel xrLabel9;
        private DevExpress.XtraReports.UI.XRLabel xrLabel8;
        private DevExpress.XtraReports.UI.XRLabel xrLabel7;
        private DevExpress.XtraReports.UI.XRTable filtraTable;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow3;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell2;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell3;
    }
}
