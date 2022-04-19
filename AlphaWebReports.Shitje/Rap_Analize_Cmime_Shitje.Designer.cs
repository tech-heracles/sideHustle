namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    partial class Rap_Analize_Cmime_Shitje
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Rap_Analize_Cmime_Shitje));
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.cmimetPivotGrid = new DevExpress.XtraReports.UI.XRPivotGrid();
            this.pRC_RAP_Analize_Cmime_ShitjeTableAdapter = new AlphaWebReports.RaportetDs.RAP_SHITJE.Ds_Analize_Cmime_ShitjeTableAdapters.PRC_RAP_Analize_Cmime_ShitjeTableAdapter();
            this.ds_Analize_Cmime_Shitje1 = new AlphaWebReports.RaportetDs.RAP_SHITJE.Ds_Analize_Cmime_Shitje();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.xrLabel35 = new DevExpress.XtraReports.UI.XRLabel();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.filtraTable = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.FiltratLabel = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
            this.Grupim1 = new DevExpress.XtraReports.Parameters.Parameter();
            this.Furnitor = new DevExpress.XtraReports.Parameters.Parameter();
            this.NiveliCmimit = new DevExpress.XtraReports.Parameters.Parameter();
            this.Magazina = new DevExpress.XtraReports.Parameters.Parameter();
            this.KodArtikulli = new DevExpress.XtraReports.Parameters.Parameter();
            this.LlojArtikulli = new DevExpress.XtraReports.Parameters.Parameter();
            this.Grupim2 = new DevExpress.XtraReports.Parameters.Parameter();
            this.GrupimTop1 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.TitulliRaport = new DevExpress.XtraReports.UI.XRControlStyle();
            this.TabelaKryesore = new DevExpress.XtraReports.UI.XRControlStyle();
            this.PermbajtjaRaport = new DevExpress.XtraReports.UI.XRControlStyle();
            this.GrupimShuma1 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.FiltratKoka = new DevExpress.XtraReports.UI.XRControlStyle();
            this.FiltratPermbajtja = new DevExpress.XtraReports.UI.XRControlStyle();
            this.Copyright = new DevExpress.XtraReports.UI.XRControlStyle();
            ((System.ComponentModel.ISupportInitialize)(this.ds_Analize_Cmime_Shitje1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.filtraTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.cmimetPivotGrid});
            this.Detail.HeightF = 67.70834F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // cmimetPivotGrid
            // 
            this.cmimetPivotGrid.CellStyleName = "TabelaKryesore";
            this.cmimetPivotGrid.DataAdapter = this.pRC_RAP_Analize_Cmime_ShitjeTableAdapter;
            this.cmimetPivotGrid.DataMember = "PRC_RAP_Analize_Cmime_Shitje";
            this.cmimetPivotGrid.DataSource = this.ds_Analize_Cmime_Shitje1;
            this.cmimetPivotGrid.FieldHeaderStyleName = "TabelaKryesore";
            this.cmimetPivotGrid.FieldValueGrandTotalStyleName = "PermbajtjaRaport";
            this.cmimetPivotGrid.FieldValueStyleName = "PermbajtjaRaport";
            this.cmimetPivotGrid.FieldValueTotalStyleName = "PermbajtjaRaport";
            this.cmimetPivotGrid.FilterSeparatorStyleName = "PermbajtjaRaport";
            this.cmimetPivotGrid.GrandTotalCellStyleName = "GrupimShuma1";
            this.cmimetPivotGrid.HeaderGroupLineStyleName = "GrupimShuma1";
            this.cmimetPivotGrid.LinesStyleName = "PermbajtjaRaport";
            this.cmimetPivotGrid.LocationFloat = new DevExpress.Utils.PointFloat(0F, 17.70834F);
            this.cmimetPivotGrid.Name = "cmimetPivotGrid";
            this.cmimetPivotGrid.OptionsPrint.FilterSeparatorBarPadding = 3;
            this.cmimetPivotGrid.OptionsPrint.PrintColumnAreaOnEveryPage = true;
            this.cmimetPivotGrid.OptionsView.ShowColumnGrandTotalHeader = false;
            this.cmimetPivotGrid.OptionsView.ShowColumnGrandTotals = false;
            this.cmimetPivotGrid.OptionsView.ShowColumnHeaders = false;
            this.cmimetPivotGrid.OptionsView.ShowColumnTotals = false;
            this.cmimetPivotGrid.OptionsView.ShowDataHeaders = false;
            this.cmimetPivotGrid.OptionsView.ShowFilterHeaders = false;
            this.cmimetPivotGrid.OptionsView.ShowRowGrandTotalHeader = false;
            this.cmimetPivotGrid.OptionsView.ShowRowGrandTotals = false;
            this.cmimetPivotGrid.OptionsView.ShowRowTotals = false;
            this.cmimetPivotGrid.SizeF = new System.Drawing.SizeF(1051.082F, 50F);
            this.cmimetPivotGrid.TotalCellStyleName = "GrupimShuma1";
            // 
            // pRC_RAP_Analize_Cmime_ShitjeTableAdapter
            // 
            this.pRC_RAP_Analize_Cmime_ShitjeTableAdapter.ClearBeforeFill = true;
            // 
            // ds_Analize_Cmime_Shitje1
            // 
            this.ds_Analize_Cmime_Shitje1.DataSetName = "Ds_Analize_Cmime_Shitje";
            this.ds_Analize_Cmime_Shitje1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 10F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel35});
            this.BottomMargin.HeightF = 99.99997F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel35
            // 
            this.xrLabel35.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel35.ForeColor = System.Drawing.Color.Green;
            this.xrLabel35.LocationFloat = new DevExpress.Utils.PointFloat(0F, 45.70834F);
            this.xrLabel35.Multiline = true;
            this.xrLabel35.Name = "xrLabel35";
            this.xrLabel35.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel35.SizeF = new System.Drawing.SizeF(417F, 44.29166F);
            this.xrLabel35.StyleName = "Copyright";
            this.xrLabel35.Text = "Copyright © IMB\r\nInstituti i Modelimeve ne Biznes \r\nwww.imb.al";
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.filtraTable,
            this.FiltratLabel,
            this.xrPictureBox1,
            this.xrLabel12});
            this.PageHeader.HeightF = 151.4583F;
            this.PageHeader.Name = "PageHeader";
            this.PageHeader.Scripts.OnBeforePrint = "PageHeader_BeforePrint";
            // 
            // filtraTable
            // 
            this.filtraTable.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.filtraTable.ForeColor = System.Drawing.Color.Gray;
            this.filtraTable.LocationFloat = new DevExpress.Utils.PointFloat(0F, 126.4583F);
            this.filtraTable.Name = "filtraTable";
            this.filtraTable.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.filtraTable.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow});
            this.filtraTable.Scripts.OnBeforePrint = "filtraTable_BeforePrint";
            this.filtraTable.SizeF = new System.Drawing.SizeF(728F, 25F);
            this.filtraTable.StyleName = "FiltratPermbajtja";
            this.filtraTable.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableRow
            // 
            this.xrTableRow.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell2,
            this.xrTableCell3,
            this.xrTableCell4,
            this.xrTableCell5,
            this.xrTableCell6});
            this.xrTableRow.Name = "xrTableRow";
            this.xrTableRow.Weight = 1D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.Weight = 1.04D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.Weight = 1.04D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.Weight = 1.04D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.Weight = 1.05D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.Weight = 2.09D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.Weight = 1.02D;
            // 
            // FiltratLabel
            // 
            this.FiltratLabel.LocationFloat = new DevExpress.Utils.PointFloat(0F, 101.4582F);
            this.FiltratLabel.Name = "FiltratLabel";
            this.FiltratLabel.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.FiltratLabel.SizeF = new System.Drawing.SizeF(104F, 25.00018F);
            this.FiltratLabel.StyleName = "FiltratKoka";
            this.FiltratLabel.Text = "Filtrat";
            this.FiltratLabel.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrPictureBox1
            // 
            this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrPictureBox1.Name = "xrPictureBox1";
            this.xrPictureBox1.SizeF = new System.Drawing.SizeF(104F, 73F);
            this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.Squeeze;
            // 
            // xrLabel12
            // 
            this.xrLabel12.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold);
            this.xrLabel12.ForeColor = System.Drawing.Color.Green;
            this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(208F, 0F);
            this.xrLabel12.Multiline = true;
            this.xrLabel12.Name = "xrLabel12";
            this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel12.SizeF = new System.Drawing.SizeF(520F, 50F);
            this.xrLabel12.StyleName = "TitulliRaport";
            this.xrLabel12.Text = "Analize Cmime Shitje";
            this.xrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomCenter;
            // 
            // Grupim1
            // 
            this.Grupim1.Name = "Grupim1";
            // 
            // Furnitor
            // 
            this.Furnitor.Name = "Furnitor";
            // 
            // NiveliCmimit
            // 
            this.NiveliCmimit.Name = "NiveliCmimit";
            // 
            // Magazina
            // 
            this.Magazina.Description = "Magazina";
            this.Magazina.Name = "Magazina";
            // 
            // KodArtikulli
            // 
            this.KodArtikulli.Name = "KodArtikulli";
            // 
            // LlojArtikulli
            // 
            this.LlojArtikulli.Name = "LlojArtikulli";
            // 
            // Grupim2
            // 
            this.Grupim2.Name = "Grupim2";
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
            // Rap_Analize_Cmime_Shitje
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.PageHeader});
            this.DataAdapter = this.pRC_RAP_Analize_Cmime_ShitjeTableAdapter;
            this.DataMember = "PRC_RAP_Analize_Cmime_Shitje";
            this.DataSource = this.ds_Analize_Cmime_Shitje1;
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(10, 10, 10, 100);
            this.PageHeight = 850;
            this.PageWidth = 1100;
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.Magazina,
            this.NiveliCmimit,
            this.KodArtikulli,
            this.LlojArtikulli,
            this.Furnitor,
            this.Grupim1,
            this.Grupim2});
            this.ScriptReferencesString = "AlphaWebReports.Common.dll\r\n";
            this.Scripts.OnBeforePrint = "Rap_Analize_Cmime_Shitje_BeforePrint";
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
            ((System.ComponentModel.ISupportInitialize)(this.ds_Analize_Cmime_Shitje1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.filtraTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.XRLabel xrLabel35;
        private Ds_Analize_Cmime_ShitjeTableAdapters.PRC_RAP_Analize_Cmime_ShitjeTableAdapter pRC_RAP_Analize_Cmime_ShitjeTableAdapter;
        private Ds_Analize_Cmime_Shitje ds_Analize_Cmime_Shitje1;
        public DevExpress.XtraReports.UI.XRPivotGrid cmimetPivotGrid;
        private DevExpress.XtraReports.UI.XRPictureBox xrPictureBox1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel12;
        private DevExpress.XtraReports.UI.XRControlStyle GrupimTop1;
        private DevExpress.XtraReports.UI.XRControlStyle TitulliRaport;
        private DevExpress.XtraReports.UI.XRControlStyle TabelaKryesore;
        private DevExpress.XtraReports.UI.XRControlStyle PermbajtjaRaport;
        private DevExpress.XtraReports.UI.XRControlStyle GrupimShuma1;
        private DevExpress.XtraReports.UI.XRControlStyle FiltratKoka;
        private DevExpress.XtraReports.UI.XRControlStyle FiltratPermbajtja;
        private DevExpress.XtraReports.UI.XRControlStyle Copyright;
        private DevExpress.XtraReports.UI.XRLabel FiltratLabel;
        private DevExpress.XtraReports.Parameters.Parameter Magazina;
        private DevExpress.XtraReports.Parameters.Parameter Grupim1;
        private DevExpress.XtraReports.Parameters.Parameter Furnitor;
        private DevExpress.XtraReports.Parameters.Parameter NiveliCmimit;
        private DevExpress.XtraReports.Parameters.Parameter KodArtikulli;
        private DevExpress.XtraReports.Parameters.Parameter LlojArtikulli;
        private DevExpress.XtraReports.Parameters.Parameter Grupim2;
        private DevExpress.XtraReports.UI.XRTable filtraTable;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell2;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell3;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell4;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell5;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell6;
    }
}
