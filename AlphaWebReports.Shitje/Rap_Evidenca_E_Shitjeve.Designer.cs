namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    partial class Rap_Evidenca_E_Shitjeve
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Rap_Evidenca_E_Shitjeve));
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.filtraTable = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.FiltratLabel = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
            this.Ndermarja = new DevExpress.XtraReports.Parameters.Parameter();
            this.DtRegj = new DevExpress.XtraReports.Parameters.Parameter();
            this.KodiPunonjesit = new DevExpress.XtraReports.Parameters.Parameter();
            this.Departamenti = new DevExpress.XtraReports.Parameters.Parameter();
            this.Monedha = new DevExpress.XtraReports.Parameters.Parameter();
            this.NrDok = new DevExpress.XtraReports.Parameters.Parameter();
            this.NenDepartamenti = new DevExpress.XtraReports.Parameters.Parameter();
            this.DtDok = new DevExpress.XtraReports.Parameters.Parameter();
            this.Muaji = new DevExpress.XtraReports.Parameters.Parameter();
            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrLabel35 = new DevExpress.XtraReports.UI.XRLabel();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrPivotGrid2 = new DevExpress.XtraReports.UI.XRPivotGrid();
            this.pRC_RAP_SHITJE_EVIDENCA_E_SHITJEVETableAdapter = new AlphaWebReports.RaportetDs.RAP_SHITJE.Ds_Rap_Evidenca_E_ShitjeveTableAdapters.PRC_RAP_SHITJE_EVIDENCA_E_SHITJEVETableAdapter();
            this.ds_Rap_Evidenca_E_Shitjeve1 = new AlphaWebReports.RaportetDs.RAP_SHITJE.Ds_Rap_Evidenca_E_Shitjeve();
            this.GrupimTop1 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.TitulliRaport = new DevExpress.XtraReports.UI.XRControlStyle();
            this.TabelaKryesore = new DevExpress.XtraReports.UI.XRControlStyle();
            this.PermbajtjaRaport = new DevExpress.XtraReports.UI.XRControlStyle();
            this.GrupimShuma1 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.FiltratKoka = new DevExpress.XtraReports.UI.XRControlStyle();
            this.FiltratPermbajtja = new DevExpress.XtraReports.UI.XRControlStyle();
            this.Copyright = new DevExpress.XtraReports.UI.XRControlStyle();
            this.parametergjuha = new DevExpress.XtraReports.Parameters.Parameter();
            ((System.ComponentModel.ISupportInitialize)(this.filtraTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_Rap_Evidenca_E_Shitjeve1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Expanded = false;
            this.Detail.HeightF = 0F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 8F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 11F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.filtraTable,
            this.FiltratLabel,
            this.xrPictureBox1,
            this.xrLabel12});
            this.PageHeader.HeightF = 122.9582F;
            this.PageHeader.Name = "PageHeader";
            this.PageHeader.Scripts.OnBeforePrint = "PageHeader_BeforePrint";
            // 
            // filtraTable
            // 
            this.filtraTable.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.filtraTable.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.filtraTable.ForeColor = System.Drawing.Color.Gray;
            this.filtraTable.LocationFloat = new DevExpress.Utils.PointFloat(0F, 97.91687F);
            this.filtraTable.Name = "filtraTable";
            this.filtraTable.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.filtraTable.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.filtraTable.Scripts.OnBeforePrint = "filtraTable_BeforePrint";
            this.filtraTable.SizeF = new System.Drawing.SizeF(1115F, 25F);
            this.filtraTable.StyleName = "FiltratPermbajtja";
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell2,
            this.xrTableCell3,
            this.xrTableCell4,
            this.xrTableCell5,
            this.xrTableCell6});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.Text = "xrTableCell1";
            this.xrTableCell1.Weight = 1.65920935441339D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.Text = "xrTableCell2";
            this.xrTableCell2.Weight = 1.94980546690561D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.Text = "xrTableCell3";
            this.xrTableCell3.Weight = 1.9591795446070262D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.Text = "xrTableCell4";
            this.xrTableCell4.Weight = 1.9498054821554203D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.Text = "xrTableCell5";
            this.xrTableCell5.Weight = 0.97490272126838617D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.Text = "xrTableCell6";
            this.xrTableCell6.Weight = 1.9591795569115746D;
            // 
            // FiltratLabel
            // 
            this.FiltratLabel.LocationFloat = new DevExpress.Utils.PointFloat(0F, 72.99996F);
            this.FiltratLabel.Name = "FiltratLabel";
            this.FiltratLabel.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.FiltratLabel.SizeF = new System.Drawing.SizeF(177F, 24.91689F);
            this.FiltratLabel.StyleName = "FiltratKoka";
            this.FiltratLabel.Text = "Filtrat";
            this.FiltratLabel.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrPictureBox1
            // 
            this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrPictureBox1.Name = "xrPictureBox1";
            this.xrPictureBox1.SizeF = new System.Drawing.SizeF(177F, 73F);
            this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.Squeeze;
            // 
            // xrLabel12
            // 
            this.xrLabel12.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold);
            this.xrLabel12.ForeColor = System.Drawing.Color.Green;
            this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(177F, 0F);
            this.xrLabel12.Multiline = true;
            this.xrLabel12.Name = "xrLabel12";
            this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel12.SizeF = new System.Drawing.SizeF(728.9999F, 50F);
            this.xrLabel12.StyleName = "TitulliRaport";
            this.xrLabel12.Text = "Evidenca e shitjeve";
            this.xrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomCenter;
            // 
            // Ndermarja
            // 
            this.Ndermarja.Name = "Ndermarja";
            // 
            // DtRegj
            // 
            this.DtRegj.Name = "DtRegj";
            // 
            // KodiPunonjesit
            // 
            this.KodiPunonjesit.Name = "KodiPunonjesit";
            // 
            // Departamenti
            // 
            this.Departamenti.Name = "Departamenti";
            // 
            // Monedha
            // 
            this.Monedha.Name = "Monedha";
            // 
            // NrDok
            // 
            this.NrDok.Name = "NrDok";
            // 
            // NenDepartamenti
            // 
            this.NenDepartamenti.Name = "NenDepartamenti";
            // 
            // DtDok
            // 
            this.DtDok.Name = "DtDok";
            // 
            // Muaji
            // 
            this.Muaji.Name = "Muaji";
            // 
            // PageFooter
            // 
            this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo1,
            this.xrLabel35});
            this.PageFooter.HeightF = 81.79167F;
            this.PageFooter.Name = "PageFooter";
            // 
            // xrPageInfo1
            // 
            this.xrPageInfo1.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrPageInfo1.ForeColor = System.Drawing.Color.Gray;
            this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(802F, 58.79167F);
            this.xrPageInfo1.Name = "xrPageInfo1";
            this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo1.SizeF = new System.Drawing.SizeF(312.9999F, 23F);
            this.xrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrLabel35
            // 
            this.xrLabel35.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel35.ForeColor = System.Drawing.Color.Empty;
            this.xrLabel35.LocationFloat = new DevExpress.Utils.PointFloat(0F, 37.5F);
            this.xrLabel35.Multiline = true;
            this.xrLabel35.Name = "xrLabel35";
            this.xrLabel35.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel35.SizeF = new System.Drawing.SizeF(385F, 44.29166F);
            this.xrLabel35.StyleName = "Copyright";
            this.xrLabel35.Text = "Copyright © IMB\r\nInstituti i Modelimeve ne Biznes \r\nwww.imb.al";
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPivotGrid2});
            this.GroupHeader1.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("IDDEP", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending),
            new DevExpress.XtraReports.UI.GroupField("IDNENDEP", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending),
            new DevExpress.XtraReports.UI.GroupField("MUAJI", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending),
            new DevExpress.XtraReports.UI.GroupField("IDKOKA", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.GroupHeader1.HeightF = 381.875F;
            this.GroupHeader1.Name = "GroupHeader1";
            this.GroupHeader1.PageBreak = DevExpress.XtraReports.UI.PageBreak.BeforeBand;
            this.GroupHeader1.RepeatEveryPage = true;
            // 
            // xrPivotGrid2
            // 
            this.xrPivotGrid2.Appearance.Cell.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrPivotGrid2.Appearance.Cell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.xrPivotGrid2.Appearance.Cell.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.xrPivotGrid2.Appearance.CustomTotalCell.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrPivotGrid2.Appearance.CustomTotalCell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.xrPivotGrid2.Appearance.CustomTotalCell.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.xrPivotGrid2.Appearance.FieldHeader.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrPivotGrid2.Appearance.FieldHeader.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.xrPivotGrid2.Appearance.FieldHeader.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.xrPivotGrid2.Appearance.FieldHeader.WordWrap = true;
            this.xrPivotGrid2.Appearance.FieldValue.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrPivotGrid2.Appearance.FieldValue.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.xrPivotGrid2.Appearance.FieldValue.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.xrPivotGrid2.Appearance.FieldValueGrandTotal.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrPivotGrid2.Appearance.FieldValueGrandTotal.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.xrPivotGrid2.Appearance.FieldValueGrandTotal.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.xrPivotGrid2.Appearance.FieldValueTotal.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrPivotGrid2.Appearance.FieldValueTotal.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.xrPivotGrid2.Appearance.FieldValueTotal.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.xrPivotGrid2.Appearance.GrandTotalCell.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrPivotGrid2.Appearance.GrandTotalCell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.xrPivotGrid2.Appearance.GrandTotalCell.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.xrPivotGrid2.Appearance.Lines.Font = new System.Drawing.Font("Agency FB", 8F);
            this.xrPivotGrid2.Appearance.TotalCell.Font = new System.Drawing.Font("Agency FB", 8F);
            this.xrPivotGrid2.Appearance.TotalCell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.xrPivotGrid2.Appearance.TotalCell.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.xrPivotGrid2.CellStyleName = "PermbajtjaRaport";
            this.xrPivotGrid2.DataAdapter = this.pRC_RAP_SHITJE_EVIDENCA_E_SHITJEVETableAdapter;
            this.xrPivotGrid2.DataMember = "PRC_RAP_SHITJE_EVIDENCA_E_SHITJEVE";
            this.xrPivotGrid2.FieldHeaderStyleName = "TabelaKryesore";
            this.xrPivotGrid2.FieldValueGrandTotalStyleName = "PermbajtjaRaport";
            this.xrPivotGrid2.FieldValueStyleName = "PermbajtjaRaport";
            this.xrPivotGrid2.FieldValueTotalStyleName = "PermbajtjaRaport";
            this.xrPivotGrid2.FilterSeparatorStyleName = "PermbajtjaRaport";
            this.xrPivotGrid2.GrandTotalCellStyleName = "GrupimShuma1";
            this.xrPivotGrid2.HeaderGroupLineStyleName = "GrupimShuma1";
            this.xrPivotGrid2.LinesStyleName = "PermbajtjaRaport";
            this.xrPivotGrid2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 20.83328F);
            this.xrPivotGrid2.Name = "xrPivotGrid2";
            this.xrPivotGrid2.OptionsDataField.RowHeaderWidth = 70;
            this.xrPivotGrid2.OptionsPrint.FilterSeparatorBarPadding = 3;
            this.xrPivotGrid2.OptionsPrint.PrintColumnAreaOnEveryPage = true;
            this.xrPivotGrid2.OptionsView.ShowColumnGrandTotalHeader = false;
            this.xrPivotGrid2.OptionsView.ShowColumnGrandTotals = false;
            this.xrPivotGrid2.OptionsView.ShowColumnHeaders = false;
            this.xrPivotGrid2.OptionsView.ShowCustomTotalsForSingleValues = true;
            this.xrPivotGrid2.OptionsView.ShowDataHeaders = false;
            this.xrPivotGrid2.OptionsView.ShowFilterHeaders = false;
            this.xrPivotGrid2.OptionsView.ShowGrandTotalsForSingleValues = true;
            this.xrPivotGrid2.OptionsView.ShowTotalsForSingleValues = true;
            this.xrPivotGrid2.SizeF = new System.Drawing.SizeF(1118F, 361.0417F);
            this.xrPivotGrid2.TotalCellStyleName = "GrupimShuma1";
            this.xrPivotGrid2.FieldValueDisplayText += new System.EventHandler<DevExpress.XtraReports.UI.PivotGrid.PivotFieldDisplayTextEventArgs>(this.xrPivotGrid1_FieldValueDisplayText);
            // 
            // pRC_RAP_SHITJE_EVIDENCA_E_SHITJEVETableAdapter
            // 
            this.pRC_RAP_SHITJE_EVIDENCA_E_SHITJEVETableAdapter.ClearBeforeFill = true;
            // 
            // ds_Rap_Evidenca_E_Shitjeve1
            // 
            this.ds_Rap_Evidenca_E_Shitjeve1.DataSetName = "Ds_Rap_Evidenca_E_Shitjeve";
            this.ds_Rap_Evidenca_E_Shitjeve1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
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
            this.PermbajtjaRaport.Font = new System.Drawing.Font("Agency FB", 8F);
            this.PermbajtjaRaport.ForeColor = System.Drawing.Color.Black;
            this.PermbajtjaRaport.Name = "PermbajtjaRaport";
            this.PermbajtjaRaport.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // GrupimShuma1
            // 
            this.GrupimShuma1.Font = new System.Drawing.Font("Agency FB", 8F, System.Drawing.FontStyle.Bold);
            this.GrupimShuma1.ForeColor = System.Drawing.Color.Green;
            this.GrupimShuma1.Name = "GrupimShuma1";
            this.GrupimShuma1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
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
            // parametergjuha
            // 
            this.parametergjuha.Description = "parametergjuha";
            this.parametergjuha.Name = "parametergjuha";
            this.parametergjuha.Type = typeof(short);
            this.parametergjuha.ValueInfo = "0";
            // 
            // Rap_Evidenca_E_Shitjeve
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.PageHeader,
            this.PageFooter,
            this.GroupHeader1});
            this.DataAdapter = this.pRC_RAP_SHITJE_EVIDENCA_E_SHITJEVETableAdapter;
            this.Font = new System.Drawing.Font("Calibri", 10F);
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(25, 25, 8, 11);
            this.PageHeight = 827;
            this.PageWidth = 1169;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.DtDok,
            this.DtRegj,
            this.NrDok,
            this.KodiPunonjesit,
            this.Departamenti,
            this.NenDepartamenti,
            this.Muaji,
            this.Monedha,
            this.Ndermarja,
            this.parametergjuha});
            this.ScriptReferencesString = "AlphaWebReports.Common.dll\r\n";
            this.Scripts.OnBeforePrint = "Rap_Evidenca_E_Shitjeve_BeforePrint";
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
            ((System.ComponentModel.ISupportInitialize)(this.filtraTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_Rap_Evidenca_E_Shitjeve1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.PageFooterBand PageFooter;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader1;
        private DevExpress.XtraReports.UI.XRLabel FiltratLabel;
        private DevExpress.XtraReports.UI.XRPictureBox xrPictureBox1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel12;
        //private Ds_Rap_Evidenca_E_Shitjeve ds_Rap_Evidenca_E_Shitjeve1;
        //private DataSource.Ds_ListepagesatStandarteDinamikeTableAdapters.PRC_RAP_LISTPAGESA_STANDARTE_DINAMIKETableAdapter pRC_RAP_LISTPAGESA_STANDARTE_DINAMIKETableAdapter;
        private DevExpress.XtraReports.UI.XRLabel xrLabel35;
        private DevExpress.XtraReports.Parameters.Parameter Ndermarja;
        private DevExpress.XtraReports.Parameters.Parameter DtRegj;
        private DevExpress.XtraReports.Parameters.Parameter KodiPunonjesit;
        private DevExpress.XtraReports.Parameters.Parameter Departamenti;
        private DevExpress.XtraReports.Parameters.Parameter Monedha;
        private DevExpress.XtraReports.Parameters.Parameter NrDok;
        private DevExpress.XtraReports.Parameters.Parameter NenDepartamenti;
        private DevExpress.XtraReports.Parameters.Parameter DtDok;
        private DevExpress.XtraReports.Parameters.Parameter Muaji;
        private DevExpress.XtraReports.UI.XRPivotGrid xrPivotGrid2;
        private DevExpress.XtraReports.UI.XRControlStyle GrupimTop1;
        private DevExpress.XtraReports.UI.XRControlStyle TitulliRaport;
        private DevExpress.XtraReports.UI.XRControlStyle TabelaKryesore;
        private DevExpress.XtraReports.UI.XRControlStyle PermbajtjaRaport;
        private DevExpress.XtraReports.UI.XRControlStyle GrupimShuma1;
        private DevExpress.XtraReports.UI.XRControlStyle FiltratKoka;
        private DevExpress.XtraReports.UI.XRControlStyle FiltratPermbajtja;
        private DevExpress.XtraReports.UI.XRControlStyle Copyright;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfo1;
        private DevExpress.XtraReports.Parameters.Parameter parametergjuha;
        private Ds_Rap_Evidenca_E_Shitjeve ds_Rap_Evidenca_E_Shitjeve1;
        private Ds_Rap_Evidenca_E_ShitjeveTableAdapters.PRC_RAP_SHITJE_EVIDENCA_E_SHITJEVETableAdapter pRC_RAP_SHITJE_EVIDENCA_E_SHITJEVETableAdapter;
        private DevExpress.XtraReports.UI.XRTable filtraTable;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell2;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell3;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell4;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell5;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell6;
    }
}
