namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    partial class RAP_ShperndarjaKlienteve
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RAP_ShperndarjaKlienteve));
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrPivotGrid1 = new DevExpress.XtraReports.UI.XRPivotGrid();
            this.pRC_RAP_ShperndarjaKlienteveTableAdapter = new AlphaWebReports.RaportetDs.RAP_SHITJE.Ds_ShperndarjaKlienteveTableAdapters.PRC_RAP_ShperndarjaKlienteveTableAdapter();
            this.ds_ShperndarjaKlienteve1 = new AlphaWebReports.RaportetDs.RAP_SHITJE.Ds_ShperndarjaKlienteve();
            this.xrPivotGridField1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.xrPivotGridField2 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.xrPivotGridField3 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.FiltratLabel = new DevExpress.XtraReports.UI.XRLabel();
            this.filtraTable = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            this.xrLabel35 = new DevExpress.XtraReports.UI.XRLabel();
            this.GrupimTop1 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.TitulliRaport = new DevExpress.XtraReports.UI.XRControlStyle();
            this.TabelaKryesore = new DevExpress.XtraReports.UI.XRControlStyle();
            this.PermbajtjaRaport = new DevExpress.XtraReports.UI.XRControlStyle();
            this.PermbajtjaRaportEven = new DevExpress.XtraReports.UI.XRControlStyle();
            this.PermbajtjaRaportOdd = new DevExpress.XtraReports.UI.XRControlStyle();
            this.GrupimShuma1 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.GrupimShuma2 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.EvenStyle = new DevExpress.XtraReports.UI.XRControlStyle();
            this.FiltratKoka = new DevExpress.XtraReports.UI.XRControlStyle();
            this.FiltratPermbajtja = new DevExpress.XtraReports.UI.XRControlStyle();
            this.Copyright = new DevExpress.XtraReports.UI.XRControlStyle();
            this.TabelaKryesoreMajtas = new DevExpress.XtraReports.UI.XRControlStyle();
            this.TabelaKryesoreDjathtas = new DevExpress.XtraReports.UI.XRControlStyle();
            this.GrupimShuma1Majtas = new DevExpress.XtraReports.UI.XRControlStyle();
            this.GrupimShuma1Djathtas = new DevExpress.XtraReports.UI.XRControlStyle();
            this.GrupimShuma2Majtas = new DevExpress.XtraReports.UI.XRControlStyle();
            this.GrupimShuma2Djathtas = new DevExpress.XtraReports.UI.XRControlStyle();
            this.xrControlStyle1 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.xrControlStyle2 = new DevExpress.XtraReports.UI.XRControlStyle();
            ((System.ComponentModel.ISupportInitialize)(this.ds_ShperndarjaKlienteve1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.filtraTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.HeightF = 0F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrPivotGrid1
            // 
            this.xrPivotGrid1.CellStyleName = "xrControlStyle2";
            this.xrPivotGrid1.DataAdapter = this.pRC_RAP_ShperndarjaKlienteveTableAdapter;
            this.xrPivotGrid1.DataMember = "PRC_RAP_ShperndarjaKlienteve";
            this.xrPivotGrid1.DataSource = this.ds_ShperndarjaKlienteve1;
            this.xrPivotGrid1.FieldHeaderStyleName = "TabelaKryesore";
            this.xrPivotGrid1.Fields.AddRange(new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField[] {
            this.xrPivotGridField1,
            this.xrPivotGridField2,
            this.xrPivotGridField3});
            this.xrPivotGrid1.FieldValueGrandTotalStyleName = "TabelaKryesore";
            this.xrPivotGrid1.FieldValueStyleName = "TabelaKryesore";
            this.xrPivotGrid1.FieldValueTotalStyleName = "TabelaKryesore";
            this.xrPivotGrid1.FilterSeparatorStyleName = "PermbajtjaRaport";
            this.xrPivotGrid1.GrandTotalCellStyleName = "GrupimShuma1";
            this.xrPivotGrid1.HeaderGroupLineStyleName = "GrupimShuma1";
            this.xrPivotGrid1.LinesStyleName = "PermbajtjaRaport";
            this.xrPivotGrid1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 177.0834F);
            this.xrPivotGrid1.Name = "xrPivotGrid1";
            this.xrPivotGrid1.OptionsPrint.FilterSeparatorBarPadding = 3;
            this.xrPivotGrid1.OptionsView.ShowColumnGrandTotalHeader = false;
            this.xrPivotGrid1.OptionsView.ShowColumnHeaders = false;
            this.xrPivotGrid1.OptionsView.ShowColumnTotals = false;
            this.xrPivotGrid1.OptionsView.ShowDataHeaders = false;
            this.xrPivotGrid1.OptionsView.ShowGrandTotalsForSingleValues = true;
            this.xrPivotGrid1.OptionsView.ShowRowGrandTotals = false;
            this.xrPivotGrid1.OptionsView.ShowTotalsForSingleValues = true;
            this.xrPivotGrid1.Scripts.OnFieldValueDisplayText = "xrPivotGrid1_FieldValueDisplayText";
            this.xrPivotGrid1.SizeF = new System.Drawing.SizeF(1050F, 100F);
            this.xrPivotGrid1.TotalCellStyleName = "GrupimShuma1";
            // 
            // pRC_RAP_ShperndarjaKlienteveTableAdapter
            // 
            this.pRC_RAP_ShperndarjaKlienteveTableAdapter.ClearBeforeFill = true;
            // 
            // ds_ShperndarjaKlienteve1
            // 
            this.ds_ShperndarjaKlienteve1.DataSetName = "Ds_ShperndarjaKlienteve";
            this.ds_ShperndarjaKlienteve1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // xrPivotGridField1
            // 
            this.xrPivotGridField1.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.xrPivotGridField1.AreaIndex = 0;
            this.xrPivotGridField1.Caption = "QYTETI \\ GRUPI";
            this.xrPivotGridField1.FieldName = "QYTETIEMRI";
            this.xrPivotGridField1.Name = "xrPivotGridField1";
            this.xrPivotGridField1.Options.ShowInFilter = true;
            // 
            // xrPivotGridField2
            // 
            this.xrPivotGridField2.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.xrPivotGridField2.AreaIndex = 0;
            this.xrPivotGridField2.FieldName = "KODGRUPI";
            this.xrPivotGridField2.Name = "xrPivotGridField2";
            this.xrPivotGridField2.Options.ShowInFilter = true;
            // 
            // xrPivotGridField3
            // 
            this.xrPivotGridField3.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.xrPivotGridField3.AreaIndex = 0;
            this.xrPivotGridField3.FieldName = "VLERA";
            this.xrPivotGridField3.Name = "xrPivotGridField3";
            this.xrPivotGridField3.Options.ShowInFilter = true;
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 12F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 15F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.FiltratLabel,
            this.filtraTable,
            this.xrLabel12,
            this.xrPictureBox1,
            this.xrPivotGrid1});
            this.PageHeader.HeightF = 277.0834F;
            this.PageHeader.Name = "PageHeader";
            this.PageHeader.Scripts.OnBeforePrint = "PageHeader_BeforePrint";
            // 
            // FiltratLabel
            // 
            this.FiltratLabel.LocationFloat = new DevExpress.Utils.PointFloat(0F, 103.1248F);
            this.FiltratLabel.Name = "FiltratLabel";
            this.FiltratLabel.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.FiltratLabel.SizeF = new System.Drawing.SizeF(137.0004F, 25.00018F);
            this.FiltratLabel.StyleName = "FiltratKoka";
            this.FiltratLabel.Text = "Filtrat";
            this.FiltratLabel.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // filtraTable
            // 
            this.filtraTable.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.filtraTable.ForeColor = System.Drawing.Color.Gray;
            this.filtraTable.LocationFloat = new DevExpress.Utils.PointFloat(0F, 128.125F);
            this.filtraTable.Name = "filtraTable";
            this.filtraTable.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.filtraTable.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.filtraTable.Scripts.OnBeforePrint = "filtraTable_BeforePrint";
            this.filtraTable.SizeF = new System.Drawing.SizeF(1050F, 25.00002F);
            this.filtraTable.StyleName = "FiltratPermbajtja";
            this.filtraTable.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell2,
            this.xrTableCell3,
            this.xrTableCell4,
            this.xrTableCell5,
            this.xrTableCell6});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StyleName = "FiltratPermbajtja";
            this.xrTableCell1.Weight = 1.7687803725959526D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StyleName = "FiltratPermbajtja";
            this.xrTableCell2.Weight = 1.7159675385021873D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.StyleName = "FiltratPermbajtja";
            this.xrTableCell3.Weight = 2.0577695372727667D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.StyleName = "FiltratPermbajtja";
            this.xrTableCell4.Weight = 2.7538189375149718D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.StyleName = "FiltratPermbajtja";
            this.xrTableCell5.Weight = 2.7394558550825105D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.StyleName = "FiltratPermbajtja";
            this.xrTableCell6.Weight = 2.5361414123739747D;
            // 
            // xrLabel12
            // 
            this.xrLabel12.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold);
            this.xrLabel12.ForeColor = System.Drawing.Color.Green;
            this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(218.2504F, 0F);
            this.xrLabel12.Multiline = true;
            this.xrLabel12.Name = "xrLabel12";
            this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel12.SizeF = new System.Drawing.SizeF(639F, 50F);
            this.xrLabel12.Text = "Shpërndarja e Klienteve";
            this.xrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomCenter;
            // 
            // xrPictureBox1
            // 
            this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrPictureBox1.Name = "xrPictureBox1";
            this.xrPictureBox1.SizeF = new System.Drawing.SizeF(137.0004F, 73F);
            this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.Squeeze;
            // 
            // PageFooter
            // 
            this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel35});
            this.PageFooter.HeightF = 54.29169F;
            this.PageFooter.Name = "PageFooter";
            // 
            // xrLabel35
            // 
            this.xrLabel35.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel35.ForeColor = System.Drawing.Color.Empty;
            this.xrLabel35.LocationFloat = new DevExpress.Utils.PointFloat(0F, 10.00001F);
            this.xrLabel35.Multiline = true;
            this.xrLabel35.Name = "xrLabel35";
            this.xrLabel35.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel35.SizeF = new System.Drawing.SizeF(254.0004F, 44.29166F);
            this.xrLabel35.StyleName = "Copyright";
            this.xrLabel35.Text = "Copyright © IMB\r\nInstituti i Modelimeve ne Biznes \r\nwww.imb.al";
            // 
            // GrupimTop1
            // 
            this.GrupimTop1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GrupimTop1.ForeColor = System.Drawing.Color.Green;
            this.GrupimTop1.Name = "GrupimTop1";
            // 
            // TitulliRaport
            // 
            this.TitulliRaport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TitulliRaport.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TitulliRaport.Font = new System.Drawing.Font("Tahoma", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitulliRaport.ForeColor = System.Drawing.Color.Green;
            this.TitulliRaport.Name = "TitulliRaport";
            this.TitulliRaport.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // TabelaKryesore
            // 
            this.TabelaKryesore.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.TabelaKryesore.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.TabelaKryesore.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.TabelaKryesore.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TabelaKryesore.ForeColor = System.Drawing.Color.Green;
            this.TabelaKryesore.Name = "TabelaKryesore";
            this.TabelaKryesore.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // PermbajtjaRaport
            // 
            this.PermbajtjaRaport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.PermbajtjaRaport.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.PermbajtjaRaport.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.PermbajtjaRaport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PermbajtjaRaport.ForeColor = System.Drawing.Color.Black;
            this.PermbajtjaRaport.Name = "PermbajtjaRaport";
            this.PermbajtjaRaport.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // PermbajtjaRaportEven
            // 
            this.PermbajtjaRaportEven.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.PermbajtjaRaportEven.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PermbajtjaRaportEven.ForeColor = System.Drawing.Color.Black;
            this.PermbajtjaRaportEven.Name = "PermbajtjaRaportEven";
            this.PermbajtjaRaportEven.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // PermbajtjaRaportOdd
            // 
            this.PermbajtjaRaportOdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.PermbajtjaRaportOdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PermbajtjaRaportOdd.ForeColor = System.Drawing.Color.Black;
            this.PermbajtjaRaportOdd.Name = "PermbajtjaRaportOdd";
            this.PermbajtjaRaportOdd.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // GrupimShuma1
            // 
            this.GrupimShuma1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GrupimShuma1.ForeColor = System.Drawing.Color.Green;
            this.GrupimShuma1.Name = "GrupimShuma1";
            // 
            // GrupimShuma2
            // 
            this.GrupimShuma2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GrupimShuma2.ForeColor = System.Drawing.Color.Green;
            this.GrupimShuma2.Name = "GrupimShuma2";
            // 
            // EvenStyle
            // 
            this.EvenStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(255)))), ((int)(((byte)(240)))));
            this.EvenStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EvenStyle.ForeColor = System.Drawing.Color.Black;
            this.EvenStyle.Name = "EvenStyle";
            // 
            // FiltratKoka
            // 
            this.FiltratKoka.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FiltratKoka.ForeColor = System.Drawing.Color.Gray;
            this.FiltratKoka.Name = "FiltratKoka";
            this.FiltratKoka.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.FiltratKoka.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // FiltratPermbajtja
            // 
            this.FiltratPermbajtja.Font = new System.Drawing.Font("Tahoma", 7F);
            this.FiltratPermbajtja.ForeColor = System.Drawing.Color.Gray;
            this.FiltratPermbajtja.Name = "FiltratPermbajtja";
            // 
            // Copyright
            // 
            this.Copyright.Font = new System.Drawing.Font("Times New Roman", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Copyright.ForeColor = System.Drawing.Color.Green;
            this.Copyright.Name = "Copyright";
            // 
            // TabelaKryesoreMajtas
            // 
            this.TabelaKryesoreMajtas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.TabelaKryesoreMajtas.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.TabelaKryesoreMajtas.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.TabelaKryesoreMajtas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TabelaKryesoreMajtas.ForeColor = System.Drawing.Color.Green;
            this.TabelaKryesoreMajtas.Name = "TabelaKryesoreMajtas";
            this.TabelaKryesoreMajtas.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // TabelaKryesoreDjathtas
            // 
            this.TabelaKryesoreDjathtas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.TabelaKryesoreDjathtas.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.TabelaKryesoreDjathtas.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.TabelaKryesoreDjathtas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TabelaKryesoreDjathtas.ForeColor = System.Drawing.Color.Green;
            this.TabelaKryesoreDjathtas.Name = "TabelaKryesoreDjathtas";
            this.TabelaKryesoreDjathtas.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // GrupimShuma1Majtas
            // 
            this.GrupimShuma1Majtas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GrupimShuma1Majtas.ForeColor = System.Drawing.Color.Green;
            this.GrupimShuma1Majtas.Name = "GrupimShuma1Majtas";
            this.GrupimShuma1Majtas.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // GrupimShuma1Djathtas
            // 
            this.GrupimShuma1Djathtas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GrupimShuma1Djathtas.ForeColor = System.Drawing.Color.Green;
            this.GrupimShuma1Djathtas.Name = "GrupimShuma1Djathtas";
            this.GrupimShuma1Djathtas.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // GrupimShuma2Majtas
            // 
            this.GrupimShuma2Majtas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GrupimShuma2Majtas.ForeColor = System.Drawing.Color.Green;
            this.GrupimShuma2Majtas.Name = "GrupimShuma2Majtas";
            this.GrupimShuma2Majtas.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // GrupimShuma2Djathtas
            // 
            this.GrupimShuma2Djathtas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GrupimShuma2Djathtas.ForeColor = System.Drawing.Color.Green;
            this.GrupimShuma2Djathtas.Name = "GrupimShuma2Djathtas";
            this.GrupimShuma2Djathtas.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // xrControlStyle1
            // 
            this.xrControlStyle1.Name = "xrControlStyle1";
            this.xrControlStyle1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            // 
            // xrControlStyle2
            // 
            this.xrControlStyle2.BackColor = System.Drawing.Color.White;
            this.xrControlStyle2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.xrControlStyle2.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrControlStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.xrControlStyle2.ForeColor = System.Drawing.Color.Black;
            this.xrControlStyle2.Name = "xrControlStyle2";
            this.xrControlStyle2.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrControlStyle2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // RAP_ShperndarjaKlienteve
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.PageHeader,
            this.PageFooter});
            this.DataAdapter = this.pRC_RAP_ShperndarjaKlienteveTableAdapter;
            this.DataMember = "PRC_RAP_ShperndarjaKlienteve";
            this.DataSource = this.ds_ShperndarjaKlienteve1;
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(25, 25, 12, 15);
            this.PageHeight = 850;
            this.PageWidth = 1100;
            this.ScriptReferencesString = "AlphaWebReports.Common.dll\r\n";
            this.Scripts.OnBeforePrint = "RAP_ShperndarjaKlienteve_BeforePrint";
            this.ScriptsSource = resources.GetString("$this.ScriptsSource");
            this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.GrupimTop1,
            this.TitulliRaport,
            this.TabelaKryesore,
            this.PermbajtjaRaport,
            this.PermbajtjaRaportEven,
            this.PermbajtjaRaportOdd,
            this.GrupimShuma1,
            this.GrupimShuma2,
            this.EvenStyle,
            this.FiltratKoka,
            this.FiltratPermbajtja,
            this.Copyright,
            this.TabelaKryesoreMajtas,
            this.TabelaKryesoreDjathtas,
            this.GrupimShuma1Majtas,
            this.GrupimShuma1Djathtas,
            this.GrupimShuma2Majtas,
            this.GrupimShuma2Djathtas,
            this.xrControlStyle1,
            this.xrControlStyle2});
            this.Version = "18.2";
            ((System.ComponentModel.ISupportInitialize)(this.ds_ShperndarjaKlienteve1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.filtraTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.PageFooterBand PageFooter;
        private DevExpress.XtraReports.UI.XRPictureBox xrPictureBox1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel12;
        private DevExpress.XtraReports.UI.XRPivotGrid xrPivotGrid1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField xrPivotGridField1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField xrPivotGridField2;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField xrPivotGridField3;
        private Ds_ShperndarjaKlienteve ds_ShperndarjaKlienteve1;
        private Ds_ShperndarjaKlienteveTableAdapters.PRC_RAP_ShperndarjaKlienteveTableAdapter pRC_RAP_ShperndarjaKlienteveTableAdapter;
        private DevExpress.XtraReports.UI.XRControlStyle GrupimTop1;
        private DevExpress.XtraReports.UI.XRControlStyle TitulliRaport;
        private DevExpress.XtraReports.UI.XRControlStyle TabelaKryesore;
        private DevExpress.XtraReports.UI.XRControlStyle PermbajtjaRaport;
        private DevExpress.XtraReports.UI.XRControlStyle PermbajtjaRaportEven;
        private DevExpress.XtraReports.UI.XRControlStyle PermbajtjaRaportOdd;
        private DevExpress.XtraReports.UI.XRControlStyle GrupimShuma1;
        private DevExpress.XtraReports.UI.XRControlStyle GrupimShuma2;
        private DevExpress.XtraReports.UI.XRControlStyle EvenStyle;
        private DevExpress.XtraReports.UI.XRControlStyle FiltratKoka;
        private DevExpress.XtraReports.UI.XRControlStyle FiltratPermbajtja;
        private DevExpress.XtraReports.UI.XRControlStyle Copyright;
        private DevExpress.XtraReports.UI.XRControlStyle TabelaKryesoreMajtas;
        private DevExpress.XtraReports.UI.XRControlStyle TabelaKryesoreDjathtas;
        private DevExpress.XtraReports.UI.XRControlStyle GrupimShuma1Majtas;
        private DevExpress.XtraReports.UI.XRControlStyle GrupimShuma1Djathtas;
        private DevExpress.XtraReports.UI.XRControlStyle GrupimShuma2Majtas;
        private DevExpress.XtraReports.UI.XRControlStyle GrupimShuma2Djathtas;
        private DevExpress.XtraReports.UI.XRControlStyle xrControlStyle1;
        private DevExpress.XtraReports.UI.XRControlStyle xrControlStyle2;
        private DevExpress.XtraReports.UI.XRTable filtraTable;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell2;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell3;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell4;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell5;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell6;
        private DevExpress.XtraReports.UI.XRLabel FiltratLabel;
        private DevExpress.XtraReports.UI.XRLabel xrLabel35;
    }
}
