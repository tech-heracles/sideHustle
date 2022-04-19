using System;
using System.Globalization;
using System.Resources;
using DevExpress.XtraReports.UI;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_LibriShitjeveSunPetrolium : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_LibriShitjeveSunPetrolium(){InitializeComponent();} 

        public Rap_LibriShitjeveSunPetrolium(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, param.ScopeID, report)
        {

        }
        public Rap_LibriShitjeveSunPetrolium(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, string scopeID,
            DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);

        }

        private void Rap_LibriShitjeve_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var parametraRaporti = AlphaWebReports.raporteUtil.DeserializoParametraPerKonstruktorRaporti(this.Extensions["parametraRaporti"]);
            xrLabel100.Text = parametraRaporti.NdermarrjePershkrimi;
            xrLabel101.Text = parametraRaporti.NdermarrjeNipt;
            xrLabel102.Text = parametraRaporti.KodiViti;
        }

        private void xrLabel53_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var parametraRaporti = AlphaWebReports.raporteUtil.DeserializoParametraPerKonstruktorRaporti(this.Extensions["parametraRaporti"]);

            if ( GetCurrentColumnValue("IDSHITJEKOKA") != null && GetCurrentColumnValue("NRDOK") != null)
            {
                xrLabel53.NavigateUrl = "javascript:window.myFaqeCelje.hapFaqeNeTabTeRi('Shto_RegjistrimDokumentash.aspx?scopeID=" + parametraRaporti.ScopeID + "&newScopeId=True&shitje_blerje=shitje&id=" + GetCurrentColumnValue("IDSHITJEKOKA").ToString() + "&numer=" + GetCurrentColumnValue("NRDOK").ToString() + "&shtim_modifikim=modifikim')";
                xrLabel53.Target = "_self";
            }
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            var rm = new ResourceManager("Resources.Strings",
                        System.Reflection.Assembly.Load("App_GlobalResources"));

            xrLabel1.Text = rm.GetString("RaportLibriShitjeveTitulli", ci);
            xrLabel2.Text = rm.GetString("labelShoqeria", ci);
            xrLabel3.Text = rm.GetString("labelNipti", ci);
            xrLabel4.Text = rm.GetString("labelViti", ci);
            xrLabel5.Text = rm.GetString("labelFilterKryesorMuaji", ci);
            xrLabel6.Text = rm.GetString("labelPaVeprimtari", ci);
            xrLabel8.Text = rm.GetString("labelKonfirmimTransaksioni", ci);
            xrLabel9.Text = rm.GetString("labelFature", ci);
            xrLabel10.Text = rm.GetString("labelNrFature", ci);
            xrLabel11.Text = rm.GetString("labelFilterAvancuarNrSerial", ci);
            xrLabel12.Text = rm.GetString("labelDataFormat", ci);
            xrLabel13.Text = rm.GetString("labelBleresi", ci);
            xrLabel14.Text = rm.GetString("labelEmerTregtarPerson", ci);
            xrLabel15.Text = rm.GetString("labelRrethi", ci);
            xrLabel16.Text = rm.GetString("labelNIPT", ci);
            xrLabel17.Text = rm.GetString("labelTotalShitjesh", ci);
            xrLabel19.Text = rm.GetString("labelShitjetePerjashtuara", ci);
            xrLabel20.Text = rm.GetString("labelExporteFurnizime", ci) ;
            xrLabel22.Text = rm.GetString("labelShitjeMeShkalle", ci) + " 20%";
            xrLabel23.Text = rm.GetString("labelShitjeMeShkalle", ci) + " 10%";
            xrLabel29.Text = rm.GetString("labelVleraTatueshme", ci);
            xrLabel30.Text = rm.GetString("labelTVSH", ci);
            xrLabel31.Text = rm.GetString("labelVleraTatueshme", ci);
            xrLabel32.Text = rm.GetString("labelTVSH", ci);
            xrTableCell1.Text = rm.GetString("labelShumaTotale", ci);
            xrTableCell11.Text = rm.GetString("labelKutiaFormularitTeDeklarimitTePageses", ci);

            xrTableCell19.Text = rm.GetString("labelKutia", ci) + " (9)";
            xrTableCell20.Text = rm.GetString("labelKutia", ci) + " (10)";
            xrTableCell21.Text = rm.GetString("labelKutia", ci) + " (11)";
            xrTableCell22.Text = rm.GetString("labelKutia", ci) + " (12)";
            xrTableCell23.Text = rm.GetString("labelKutia", ci) + " (13)";
            xrTableCell24.Text = rm.GetString("labelKutia", ci) + " (14)";
            xrTableCell25.Text = rm.GetString("labelKutia", ci) + " (15)";
            xrLabel96.Text = rm.GetString("labelshpjegim", ci);
            xrLabel97.Text = rm.GetString("labelEmerMbiemer", ci);
            xrLabel95.Text = rm.GetString("labelShpjegimPrintimi", ci);
            xrLabel98.Text = rm.GetString("labelShtimNrRreshtash", ci);
        }

        private void InitializeComponent()
        {
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary3 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary4 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary5 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary6 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary7 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary8 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary9 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary10 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary11 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary12 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary13 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary14 = new DevExpress.XtraReports.UI.XRSummary();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrLabel100 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel103 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel102 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel101 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrLabel9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel13 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel17 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel19 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel20 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel22 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel23 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel18 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrLabel10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel11 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel14 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel15 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel16 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell38 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell39 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell40 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel29 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel30 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel31 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel32 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell45 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell46 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell47 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell48 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell49 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell50 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell51 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell52 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell53 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell54 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell55 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell56 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell57 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell58 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell59 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell60 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell61 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell62 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell63 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell64 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell65 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell66 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell67 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell68 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell69 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell70 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell71 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell72 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell73 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow7 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrLabel35 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel36 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel37 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel38 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel39 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel40 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel41 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel42 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel43 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel47 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel48 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel49 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel50 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel21 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow8 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell88 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell89 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell90 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell91 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell92 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell93 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell94 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell95 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell96 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell97 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell98 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell99 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell100 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell101 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel97 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel96 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel95 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel98 = new DevExpress.XtraReports.UI.XRLabel();
            this.ds_RapLiberShitje1 = new AlphaWebReports.RaportetDs.RAP_SHITJE.ds_RapLiberShitje();
            this.pRC_RAP_LIBERSHITJETableAdapter = new AlphaWebReports.RaportetDs.RAP_SHITJE.ds_RapLiberShitjeTableAdapters.PRC_RAP_LIBERSHITJETableAdapter();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow9 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrLabel53 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel54 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel55 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel56 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel99 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel57 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel58 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel59 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel62 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel63 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel65 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel68 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel69 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel24 = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupHeader2 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.TotaliG = new DevExpress.XtraReports.UI.CalculatedField();
            this.PermbajtjaBold = new DevExpress.XtraReports.UI.XRControlStyle();
            this.Permbajtja = new DevExpress.XtraReports.UI.XRControlStyle();
            this.Titulli = new DevExpress.XtraReports.UI.XRControlStyle();
            this.GroupHeader3 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell24 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell25 = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_RapLiberShitje1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Dpi = 100F;
            this.Detail.HeightF = 0F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.SortFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("DTDOK", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // TopMargin
            // 
            this.TopMargin.Dpi = 100F;
            this.TopMargin.HeightF = 48F;
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
            this.xrLabel100,
            this.xrLabel8,
            this.xrLabel7,
            this.xrLabel103,
            this.xrLabel102,
            this.xrLabel101,
            this.xrLabel3,
            this.xrLabel2,
            this.xrLabel1,
            this.xrLabel6,
            this.xrLabel5,
            this.xrLabel4});
            this.ReportHeader.Dpi = 100F;
            this.ReportHeader.HeightF = 198.0833F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrLabel100
            // 
            this.xrLabel100.Dpi = 100F;
            this.xrLabel100.Font = new System.Drawing.Font("Agency FB", 12F);
            this.xrLabel100.LocationFloat = new DevExpress.Utils.PointFloat(147.0816F, 25F);
            this.xrLabel100.Name = "xrLabel100";
            this.xrLabel100.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel100.SizeF = new System.Drawing.SizeF(93.74977F, 25F);
            this.xrLabel100.StyleName = "Permbajtja";
            // 
            // xrLabel8
            // 
            this.xrLabel8.Dpi = 100F;
            this.xrLabel8.Font = new System.Drawing.Font("Agency FB", 12F);
            this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(147.0816F, 150F);
            this.xrLabel8.Name = "xrLabel8";
            this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(20, 2, 0, 0, 100F);
            this.xrLabel8.SizeF = new System.Drawing.SizeF(440.4116F, 25F);
            this.xrLabel8.StyleName = "Permbajtja";
            this.xrLabel8.StylePriority.UsePadding = false;
            this.xrLabel8.Text = "Ploteso PO nese gjate muajit nuk eshte bere asnje transaksion. ";
            // 
            // xrLabel7
            // 
            this.xrLabel7.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel7.BorderWidth = 2F;
            this.xrLabel7.Dpi = 100F;
            this.xrLabel7.Font = new System.Drawing.Font("Agency FB", 12F);
            this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(78.33163F, 150F);
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel7.SizeF = new System.Drawing.SizeF(68.75014F, 25F);
            this.xrLabel7.StyleName = "Permbajtja";
            this.xrLabel7.StylePriority.UseBorders = false;
            this.xrLabel7.StylePriority.UseBorderWidth = false;
            // 
            // xrLabel103
            // 
            this.xrLabel103.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "PRC_RAP_LIBERSHITJE.PERIUDHA")});
            this.xrLabel103.Dpi = 100F;
            this.xrLabel103.Font = new System.Drawing.Font("Agency FB", 12F);
            this.xrLabel103.LocationFloat = new DevExpress.Utils.PointFloat(147.0816F, 100F);
            this.xrLabel103.Name = "xrLabel103";
            this.xrLabel103.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel103.SizeF = new System.Drawing.SizeF(93.74976F, 25F);
            this.xrLabel103.StyleName = "Permbajtja";
            this.xrLabel103.Text = "xrLabel103";
            // 
            // xrLabel102
            // 
            this.xrLabel102.Dpi = 100F;
            this.xrLabel102.Font = new System.Drawing.Font("Agency FB", 12F);
            this.xrLabel102.LocationFloat = new DevExpress.Utils.PointFloat(147.0816F, 75F);
            this.xrLabel102.Name = "xrLabel102";
            this.xrLabel102.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel102.SizeF = new System.Drawing.SizeF(93.74974F, 25F);
            this.xrLabel102.StyleName = "Permbajtja";
            // 
            // xrLabel101
            // 
            this.xrLabel101.Dpi = 100F;
            this.xrLabel101.Font = new System.Drawing.Font("Agency FB", 12F);
            this.xrLabel101.LocationFloat = new DevExpress.Utils.PointFloat(147.0816F, 50F);
            this.xrLabel101.Name = "xrLabel101";
            this.xrLabel101.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel101.SizeF = new System.Drawing.SizeF(93.74976F, 25F);
            this.xrLabel101.StyleName = "Permbajtja";
            this.xrLabel101.Text = "    ";
            // 
            // xrLabel3
            // 
            this.xrLabel3.Dpi = 100F;
            this.xrLabel3.Font = new System.Drawing.Font("Agency FB", 12F);
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 50F);
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(78.33142F, 25F);
            this.xrLabel3.StyleName = "Permbajtja";
            this.xrLabel3.Text = "Nipti";
            // 
            // xrLabel2
            // 
            this.xrLabel2.Dpi = 100F;
            this.xrLabel2.Font = new System.Drawing.Font("Agency FB", 12F);
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 25F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(78.33142F, 25F);
            this.xrLabel2.StyleName = "Permbajtja";
            this.xrLabel2.Text = "Shoqeria";
            // 
            // xrLabel1
            // 
            this.xrLabel1.Dpi = 100F;
            this.xrLabel1.Font = new System.Drawing.Font("Agency FB", 16F, System.Drawing.FontStyle.Bold);
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(147.0818F, 25F);
            this.xrLabel1.StyleName = "Titulli";
            this.xrLabel1.Text = "Libri i Shitjeve";
            // 
            // xrLabel6
            // 
            this.xrLabel6.Dpi = 100F;
            this.xrLabel6.Font = new System.Drawing.Font("Agency FB", 12F);
            this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(0F, 150F);
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel6.SizeF = new System.Drawing.SizeF(78.33132F, 25F);
            this.xrLabel6.StyleName = "Permbajtja";
            this.xrLabel6.Text = "Pa veprimtari";
            // 
            // xrLabel5
            // 
            this.xrLabel5.Dpi = 100F;
            this.xrLabel5.Font = new System.Drawing.Font("Agency FB", 12F);
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(0F, 100F);
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel5.SizeF = new System.Drawing.SizeF(78.33132F, 25F);
            this.xrLabel5.StyleName = "Permbajtja";
            this.xrLabel5.Text = "Muaji";
            // 
            // xrLabel4
            // 
            this.xrLabel4.Dpi = 100F;
            this.xrLabel4.Font = new System.Drawing.Font("Agency FB", 12F);
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 75F);
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(78.33132F, 25F);
            this.xrLabel4.StyleName = "Permbajtja";
            this.xrLabel4.Text = "Viti";
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable3});
            this.PageHeader.Dpi = 100F;
            this.PageHeader.HeightF = 150F;
            this.PageHeader.Name = "PageHeader";
            // 
            // xrTable3
            // 
            this.xrTable3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable3.BorderWidth = 2F;
            this.xrTable3.Dpi = 100F;
            this.xrTable3.Font = new System.Drawing.Font("Agency FB", 12F);
            this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable3.Name = "xrTable3";
            this.xrTable3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2,
            this.xrTableRow4,
            this.xrTableRow5,
            this.xrTableRow6,
            this.xrTableRow7,
            this.xrTableRow8});
            this.xrTable3.SizeF = new System.Drawing.SizeF(1076.999F, 150F);
            this.xrTable3.StylePriority.UseBorders = false;
            this.xrTable3.StylePriority.UseBorderWidth = false;
            this.xrTable3.StylePriority.UseFont = false;
            this.xrTable3.StylePriority.UsePadding = false;
            this.xrTable3.StylePriority.UseTextAlignment = false;
            this.xrTable3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrLabel9,
            this.xrLabel13,
            this.xrLabel17,
            this.xrLabel19,
            this.xrLabel20,
            this.xrLabel22,
            this.xrLabel23,
            this.xrLabel18});
            this.xrTableRow2.Dpi = 100F;
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // xrLabel9
            // 
            this.xrLabel9.Dpi = 100F;
            this.xrLabel9.Font = new System.Drawing.Font("Agency FB", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel9.Name = "xrLabel9";
            this.xrLabel9.StylePriority.UseFont = false;
            this.xrLabel9.Text = "Fature";
            this.xrLabel9.Weight = 2.4083172607421877D;
            // 
            // xrLabel13
            // 
            this.xrLabel13.Dpi = 100F;
            this.xrLabel13.Font = new System.Drawing.Font("Agency FB", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel13.Name = "xrLabel13";
            this.xrLabel13.StylePriority.UseFont = false;
            this.xrLabel13.Text = "Bleresi";
            this.xrLabel13.Weight = 2.5304378288576062D;
            // 
            // xrLabel17
            // 
            this.xrLabel17.Dpi = 100F;
            this.xrLabel17.Font = new System.Drawing.Font("Agency FB", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel17.Multiline = true;
            this.xrLabel17.Name = "xrLabel17";
            this.xrLabel17.RowSpan = 4;
            this.xrLabel17.StylePriority.UseFont = false;
            this.xrLabel17.Text = "\r\n\r\n\r\nTotali i shitjeve";
            this.xrLabel17.Weight = 0.936176934867406D;
            // 
            // xrLabel19
            // 
            this.xrLabel19.Dpi = 100F;
            this.xrLabel19.Font = new System.Drawing.Font("Agency FB", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel19.Multiline = true;
            this.xrLabel19.Name = "xrLabel19";
            this.xrLabel19.RowSpan = 4;
            this.xrLabel19.StylePriority.UseFont = false;
            this.xrLabel19.Text = "\r\n\r\nShitjet e perjashtuara";
            this.xrLabel19.Weight = 0.833825260212109D;
            // 
            // xrLabel20
            // 
            this.xrLabel20.Dpi = 100F;
            this.xrLabel20.Font = new System.Drawing.Font("Agency FB", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel20.Multiline = true;
            this.xrLabel20.Name = "xrLabel20";
            this.xrLabel20.RowSpan = 4;
            this.xrLabel20.StylePriority.UseFont = false;
            this.xrLabel20.Text = "\r\n\r\nExporte / Furnizime me 0%";
            this.xrLabel20.Weight = 0.73958515369134659D;
            // 
            // xrLabel22
            // 
            this.xrLabel22.Dpi = 100F;
            this.xrLabel22.Font = new System.Drawing.Font("Agency FB", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel22.Name = "xrLabel22";
            this.xrLabel22.StylePriority.UseFont = false;
            this.xrLabel22.Text = "Shitje me shkalle 20 % ";
            this.xrLabel22.Weight = 1.5000062919783803D;
            // 
            // xrLabel23
            // 
            this.xrLabel23.Dpi = 100F;
            this.xrLabel23.Font = new System.Drawing.Font("Agency FB", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel23.Name = "xrLabel23";
            this.xrLabel23.StylePriority.UseFont = false;
            this.xrLabel23.Text = "Shitje me shkalle 10%";
            this.xrLabel23.Weight = 1.3487423838722752D;
            // 
            // xrLabel18
            // 
            this.xrLabel18.Dpi = 100F;
            this.xrLabel18.Font = new System.Drawing.Font("Agency FB", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel18.Multiline = true;
            this.xrLabel18.Name = "xrLabel18";
            this.xrLabel18.RowSpan = 4;
            this.xrLabel18.StylePriority.UseFont = false;
            this.xrLabel18.Text = "Dega \r\n\r\nadm.";
            this.xrLabel18.Weight = 0.47289665812408976D;
            // 
            // xrTableRow4
            // 
            this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrLabel10,
            this.xrLabel11,
            this.xrLabel12,
            this.xrLabel14,
            this.xrLabel15,
            this.xrLabel16,
            this.xrTableCell38,
            this.xrTableCell39,
            this.xrTableCell40,
            this.xrLabel29,
            this.xrLabel30,
            this.xrLabel31,
            this.xrLabel32,
            this.xrTableCell45});
            this.xrTableRow4.Dpi = 100F;
            this.xrTableRow4.Name = "xrTableRow4";
            this.xrTableRow4.Weight = 1D;
            // 
            // xrLabel10
            // 
            this.xrLabel10.Dpi = 100F;
            this.xrLabel10.Font = new System.Drawing.Font("Agency FB", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel10.Name = "xrLabel10";
            this.xrLabel10.RowSpan = 3;
            this.xrLabel10.StylePriority.UseFont = false;
            this.xrLabel10.Text = "Nr Fatures";
            this.xrLabel10.Weight = 0.78331329345703127D;
            // 
            // xrLabel11
            // 
            this.xrLabel11.Dpi = 100F;
            this.xrLabel11.Font = new System.Drawing.Font("Agency FB", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel11.Name = "xrLabel11";
            this.xrLabel11.RowSpan = 3;
            this.xrLabel11.StylePriority.UseFont = false;
            this.xrLabel11.Text = "Numri Serial";
            this.xrLabel11.Weight = 0.68750473022460934D;
            // 
            // xrLabel12
            // 
            this.xrLabel12.Dpi = 100F;
            this.xrLabel12.Font = new System.Drawing.Font("Agency FB", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel12.Multiline = true;
            this.xrLabel12.Name = "xrLabel12";
            this.xrLabel12.RowSpan = 3;
            this.xrLabel12.StylePriority.UseFont = false;
            this.xrLabel12.Text = "\r\nData (dd/mm/yyyy)";
            this.xrLabel12.Weight = 0.93749923706054694D;
            // 
            // xrLabel14
            // 
            this.xrLabel14.Dpi = 100F;
            this.xrLabel14.Font = new System.Drawing.Font("Agency FB", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel14.Multiline = true;
            this.xrLabel14.Name = "xrLabel14";
            this.xrLabel14.RowSpan = 3;
            this.xrLabel14.StylePriority.UseFont = false;
            this.xrLabel14.Text = "\r\nEmri tregtar/personi";
            this.xrLabel14.Weight = 1.4158546229597175D;
            // 
            // xrLabel15
            // 
            this.xrLabel15.Dpi = 100F;
            this.xrLabel15.Font = new System.Drawing.Font("Agency FB", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel15.Multiline = true;
            this.xrLabel15.Name = "xrLabel15";
            this.xrLabel15.RowSpan = 3;
            this.xrLabel15.StylePriority.UseFont = false;
            this.xrLabel15.Text = "\r\n\r\nRrethi";
            this.xrLabel15.Weight = 0.50000293831874854D;
            // 
            // xrLabel16
            // 
            this.xrLabel16.Dpi = 100F;
            this.xrLabel16.Font = new System.Drawing.Font("Agency FB", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel16.Multiline = true;
            this.xrLabel16.Name = "xrLabel16";
            this.xrLabel16.RowSpan = 3;
            this.xrLabel16.StylePriority.UseFont = false;
            this.xrLabel16.Text = "\r\n\r\nNIPT";
            this.xrLabel16.Weight = 0.61458026757914D;
            // 
            // xrTableCell38
            // 
            this.xrTableCell38.Dpi = 100F;
            this.xrTableCell38.Name = "xrTableCell38";
            this.xrTableCell38.RowSpan = 0;
            this.xrTableCell38.Text = "xrTableCell38";
            this.xrTableCell38.Weight = 0.936176934867406D;
            // 
            // xrTableCell39
            // 
            this.xrTableCell39.Dpi = 100F;
            this.xrTableCell39.Name = "xrTableCell39";
            this.xrTableCell39.RowSpan = 0;
            this.xrTableCell39.Text = "xrTableCell39";
            this.xrTableCell39.Weight = 0.833825260212109D;
            // 
            // xrTableCell40
            // 
            this.xrTableCell40.Dpi = 100F;
            this.xrTableCell40.Name = "xrTableCell40";
            this.xrTableCell40.RowSpan = 0;
            this.xrTableCell40.Text = "xrTableCell40";
            this.xrTableCell40.Weight = 0.73958515369134659D;
            // 
            // xrLabel29
            // 
            this.xrLabel29.Dpi = 100F;
            this.xrLabel29.Font = new System.Drawing.Font("Agency FB", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel29.Multiline = true;
            this.xrLabel29.Name = "xrLabel29";
            this.xrLabel29.RowSpan = 3;
            this.xrLabel29.StylePriority.UseFont = false;
            this.xrLabel29.Text = "\r\nVlera e tatueshme";
            this.xrLabel29.Weight = 0.79999983019070076D;
            // 
            // xrLabel30
            // 
            this.xrLabel30.Dpi = 100F;
            this.xrLabel30.Font = new System.Drawing.Font("Agency FB", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel30.Multiline = true;
            this.xrLabel30.Name = "xrLabel30";
            this.xrLabel30.RowSpan = 3;
            this.xrLabel30.StylePriority.UseFont = false;
            this.xrLabel30.Text = "\r\n\r\nTVSH";
            this.xrLabel30.Weight = 0.70000569884840069D;
            // 
            // xrLabel31
            // 
            this.xrLabel31.Dpi = 100F;
            this.xrLabel31.Font = new System.Drawing.Font("Agency FB", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel31.Multiline = true;
            this.xrLabel31.Name = "xrLabel31";
            this.xrLabel31.RowSpan = 3;
            this.xrLabel31.StylePriority.UseFont = false;
            this.xrLabel31.Text = "\r\nVlera e tatueshme";
            this.xrLabel31.Weight = 0.79666498823374421D;
            // 
            // xrLabel32
            // 
            this.xrLabel32.Dpi = 100F;
            this.xrLabel32.Font = new System.Drawing.Font("Agency FB", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel32.Multiline = true;
            this.xrLabel32.Name = "xrLabel32";
            this.xrLabel32.RowSpan = 3;
            this.xrLabel32.StylePriority.UseFont = false;
            this.xrLabel32.Text = "\r\n\r\nTVSH";
            this.xrLabel32.Weight = 0.55207815857780984D;
            // 
            // xrTableCell45
            // 
            this.xrTableCell45.Dpi = 100F;
            this.xrTableCell45.Name = "xrTableCell45";
            this.xrTableCell45.RowSpan = 0;
            this.xrTableCell45.Text = "xrTableCell45";
            this.xrTableCell45.Weight = 0.47289665812408976D;
            // 
            // xrTableRow5
            // 
            this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell46,
            this.xrTableCell47,
            this.xrTableCell48,
            this.xrTableCell49,
            this.xrTableCell50,
            this.xrTableCell51,
            this.xrTableCell52,
            this.xrTableCell53,
            this.xrTableCell54,
            this.xrTableCell55,
            this.xrTableCell56,
            this.xrTableCell57,
            this.xrTableCell58,
            this.xrTableCell59});
            this.xrTableRow5.Dpi = 100F;
            this.xrTableRow5.Name = "xrTableRow5";
            this.xrTableRow5.Weight = 1D;
            // 
            // xrTableCell46
            // 
            this.xrTableCell46.Dpi = 100F;
            this.xrTableCell46.Name = "xrTableCell46";
            this.xrTableCell46.RowSpan = 0;
            this.xrTableCell46.Text = "xrTableCell46";
            this.xrTableCell46.Weight = 0.78331329345703127D;
            // 
            // xrTableCell47
            // 
            this.xrTableCell47.Dpi = 100F;
            this.xrTableCell47.Name = "xrTableCell47";
            this.xrTableCell47.RowSpan = 0;
            this.xrTableCell47.Text = "xrTableCell47";
            this.xrTableCell47.Weight = 0.68750473022460934D;
            // 
            // xrTableCell48
            // 
            this.xrTableCell48.Dpi = 100F;
            this.xrTableCell48.Name = "xrTableCell48";
            this.xrTableCell48.RowSpan = 0;
            this.xrTableCell48.Text = "xrTableCell48";
            this.xrTableCell48.Weight = 0.93749923706054694D;
            // 
            // xrTableCell49
            // 
            this.xrTableCell49.Dpi = 100F;
            this.xrTableCell49.Name = "xrTableCell49";
            this.xrTableCell49.RowSpan = 0;
            this.xrTableCell49.Text = "xrTableCell49";
            this.xrTableCell49.Weight = 1.4158546229597175D;
            // 
            // xrTableCell50
            // 
            this.xrTableCell50.Dpi = 100F;
            this.xrTableCell50.Name = "xrTableCell50";
            this.xrTableCell50.RowSpan = 0;
            this.xrTableCell50.Text = "xrTableCell50";
            this.xrTableCell50.Weight = 0.50000293831874854D;
            // 
            // xrTableCell51
            // 
            this.xrTableCell51.Dpi = 100F;
            this.xrTableCell51.Name = "xrTableCell51";
            this.xrTableCell51.RowSpan = 0;
            this.xrTableCell51.Text = "xrTableCell51";
            this.xrTableCell51.Weight = 0.61458026757914D;
            // 
            // xrTableCell52
            // 
            this.xrTableCell52.Dpi = 100F;
            this.xrTableCell52.Name = "xrTableCell52";
            this.xrTableCell52.RowSpan = 0;
            this.xrTableCell52.Text = "xrTableCell52";
            this.xrTableCell52.Weight = 0.936176934867406D;
            // 
            // xrTableCell53
            // 
            this.xrTableCell53.Dpi = 100F;
            this.xrTableCell53.Name = "xrTableCell53";
            this.xrTableCell53.RowSpan = 0;
            this.xrTableCell53.Text = "xrTableCell53";
            this.xrTableCell53.Weight = 0.833825260212109D;
            // 
            // xrTableCell54
            // 
            this.xrTableCell54.Dpi = 100F;
            this.xrTableCell54.Name = "xrTableCell54";
            this.xrTableCell54.RowSpan = 0;
            this.xrTableCell54.Text = "xrTableCell54";
            this.xrTableCell54.Weight = 0.73958515369134659D;
            // 
            // xrTableCell55
            // 
            this.xrTableCell55.Dpi = 100F;
            this.xrTableCell55.Name = "xrTableCell55";
            this.xrTableCell55.RowSpan = 0;
            this.xrTableCell55.Text = "xrTableCell55";
            this.xrTableCell55.Weight = 0.79999983019070076D;
            // 
            // xrTableCell56
            // 
            this.xrTableCell56.Dpi = 100F;
            this.xrTableCell56.Name = "xrTableCell56";
            this.xrTableCell56.RowSpan = 0;
            this.xrTableCell56.Text = "xrTableCell56";
            this.xrTableCell56.Weight = 0.70000569884840069D;
            // 
            // xrTableCell57
            // 
            this.xrTableCell57.Dpi = 100F;
            this.xrTableCell57.Name = "xrTableCell57";
            this.xrTableCell57.RowSpan = 0;
            this.xrTableCell57.Text = "xrTableCell57";
            this.xrTableCell57.Weight = 0.79666498823374421D;
            // 
            // xrTableCell58
            // 
            this.xrTableCell58.Dpi = 100F;
            this.xrTableCell58.Name = "xrTableCell58";
            this.xrTableCell58.RowSpan = 0;
            this.xrTableCell58.Text = "xrTableCell58";
            this.xrTableCell58.Weight = 0.55207815857780984D;
            // 
            // xrTableCell59
            // 
            this.xrTableCell59.Dpi = 100F;
            this.xrTableCell59.Name = "xrTableCell59";
            this.xrTableCell59.RowSpan = 0;
            this.xrTableCell59.Text = "xrTableCell59";
            this.xrTableCell59.Weight = 0.47289665812408976D;
            // 
            // xrTableRow6
            // 
            this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell60,
            this.xrTableCell61,
            this.xrTableCell62,
            this.xrTableCell63,
            this.xrTableCell64,
            this.xrTableCell65,
            this.xrTableCell66,
            this.xrTableCell67,
            this.xrTableCell68,
            this.xrTableCell69,
            this.xrTableCell70,
            this.xrTableCell71,
            this.xrTableCell72,
            this.xrTableCell73});
            this.xrTableRow6.Dpi = 100F;
            this.xrTableRow6.Name = "xrTableRow6";
            this.xrTableRow6.Weight = 1D;
            // 
            // xrTableCell60
            // 
            this.xrTableCell60.Dpi = 100F;
            this.xrTableCell60.Name = "xrTableCell60";
            this.xrTableCell60.RowSpan = 0;
            this.xrTableCell60.Text = "xrTableCell60";
            this.xrTableCell60.Weight = 0.78331329345703127D;
            // 
            // xrTableCell61
            // 
            this.xrTableCell61.Dpi = 100F;
            this.xrTableCell61.Name = "xrTableCell61";
            this.xrTableCell61.RowSpan = 0;
            this.xrTableCell61.Text = "xrTableCell61";
            this.xrTableCell61.Weight = 0.68750473022460934D;
            // 
            // xrTableCell62
            // 
            this.xrTableCell62.Dpi = 100F;
            this.xrTableCell62.Name = "xrTableCell62";
            this.xrTableCell62.RowSpan = 0;
            this.xrTableCell62.Text = "xrTableCell62";
            this.xrTableCell62.Weight = 0.93749923706054694D;
            // 
            // xrTableCell63
            // 
            this.xrTableCell63.Dpi = 100F;
            this.xrTableCell63.Name = "xrTableCell63";
            this.xrTableCell63.RowSpan = 0;
            this.xrTableCell63.Text = "xrTableCell63";
            this.xrTableCell63.Weight = 1.4158546229597175D;
            // 
            // xrTableCell64
            // 
            this.xrTableCell64.Dpi = 100F;
            this.xrTableCell64.Name = "xrTableCell64";
            this.xrTableCell64.RowSpan = 0;
            this.xrTableCell64.Text = "xrTableCell64";
            this.xrTableCell64.Weight = 0.50000293831874854D;
            // 
            // xrTableCell65
            // 
            this.xrTableCell65.Dpi = 100F;
            this.xrTableCell65.Name = "xrTableCell65";
            this.xrTableCell65.RowSpan = 0;
            this.xrTableCell65.Text = "xrTableCell65";
            this.xrTableCell65.Weight = 0.61458026757914D;
            // 
            // xrTableCell66
            // 
            this.xrTableCell66.Dpi = 100F;
            this.xrTableCell66.Name = "xrTableCell66";
            this.xrTableCell66.RowSpan = 0;
            this.xrTableCell66.Text = "xrTableCell66";
            this.xrTableCell66.Weight = 0.936176934867406D;
            // 
            // xrTableCell67
            // 
            this.xrTableCell67.Dpi = 100F;
            this.xrTableCell67.Name = "xrTableCell67";
            this.xrTableCell67.RowSpan = 0;
            this.xrTableCell67.Text = "xrTableCell67";
            this.xrTableCell67.Weight = 0.833825260212109D;
            // 
            // xrTableCell68
            // 
            this.xrTableCell68.Dpi = 100F;
            this.xrTableCell68.Name = "xrTableCell68";
            this.xrTableCell68.RowSpan = 0;
            this.xrTableCell68.Text = "xrTableCell68";
            this.xrTableCell68.Weight = 0.73958515369134659D;
            // 
            // xrTableCell69
            // 
            this.xrTableCell69.Dpi = 100F;
            this.xrTableCell69.Name = "xrTableCell69";
            this.xrTableCell69.RowSpan = 0;
            this.xrTableCell69.Text = "xrTableCell69";
            this.xrTableCell69.Weight = 0.79999983019070076D;
            // 
            // xrTableCell70
            // 
            this.xrTableCell70.Dpi = 100F;
            this.xrTableCell70.Name = "xrTableCell70";
            this.xrTableCell70.RowSpan = 0;
            this.xrTableCell70.Text = "xrTableCell70";
            this.xrTableCell70.Weight = 0.70000569884840069D;
            // 
            // xrTableCell71
            // 
            this.xrTableCell71.Dpi = 100F;
            this.xrTableCell71.Name = "xrTableCell71";
            this.xrTableCell71.RowSpan = 0;
            this.xrTableCell71.Text = "xrTableCell71";
            this.xrTableCell71.Weight = 0.79666498823374421D;
            // 
            // xrTableCell72
            // 
            this.xrTableCell72.Dpi = 100F;
            this.xrTableCell72.Name = "xrTableCell72";
            this.xrTableCell72.RowSpan = 0;
            this.xrTableCell72.Text = "xrTableCell72";
            this.xrTableCell72.Weight = 0.55207815857780984D;
            // 
            // xrTableCell73
            // 
            this.xrTableCell73.Dpi = 100F;
            this.xrTableCell73.Name = "xrTableCell73";
            this.xrTableCell73.RowSpan = 0;
            this.xrTableCell73.Text = "xrTableCell73";
            this.xrTableCell73.Weight = 0.47289665812408976D;
            // 
            // xrTableRow7
            // 
            this.xrTableRow7.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrLabel35,
            this.xrLabel36,
            this.xrLabel37,
            this.xrLabel38,
            this.xrLabel39,
            this.xrLabel40,
            this.xrLabel41,
            this.xrLabel42,
            this.xrLabel43,
            this.xrLabel47,
            this.xrLabel48,
            this.xrLabel49,
            this.xrLabel50,
            this.xrLabel21});
            this.xrTableRow7.Dpi = 100F;
            this.xrTableRow7.Name = "xrTableRow7";
            this.xrTableRow7.Weight = 1D;
            // 
            // xrLabel35
            // 
            this.xrLabel35.Dpi = 100F;
            this.xrLabel35.Multiline = true;
            this.xrLabel35.Name = "xrLabel35";
            this.xrLabel35.RowSpan = 2;
            this.xrLabel35.Text = "\r\na";
            this.xrLabel35.Weight = 0.78331329345703127D;
            // 
            // xrLabel36
            // 
            this.xrLabel36.Dpi = 100F;
            this.xrLabel36.Multiline = true;
            this.xrLabel36.Name = "xrLabel36";
            this.xrLabel36.RowSpan = 2;
            this.xrLabel36.Text = "\r\nb";
            this.xrLabel36.Weight = 0.68750473022460934D;
            // 
            // xrLabel37
            // 
            this.xrLabel37.Dpi = 100F;
            this.xrLabel37.Multiline = true;
            this.xrLabel37.Name = "xrLabel37";
            this.xrLabel37.RowSpan = 2;
            this.xrLabel37.Text = "\r\nc";
            this.xrLabel37.Weight = 0.93749923706054694D;
            // 
            // xrLabel38
            // 
            this.xrLabel38.Dpi = 100F;
            this.xrLabel38.Multiline = true;
            this.xrLabel38.Name = "xrLabel38";
            this.xrLabel38.RowSpan = 2;
            this.xrLabel38.Text = "\r\nd";
            this.xrLabel38.Weight = 1.4158546229597175D;
            // 
            // xrLabel39
            // 
            this.xrLabel39.Dpi = 100F;
            this.xrLabel39.Multiline = true;
            this.xrLabel39.Name = "xrLabel39";
            this.xrLabel39.RowSpan = 2;
            this.xrLabel39.Text = "\r\ne";
            this.xrLabel39.Weight = 0.50000293831874854D;
            // 
            // xrLabel40
            // 
            this.xrLabel40.Dpi = 100F;
            this.xrLabel40.Multiline = true;
            this.xrLabel40.Name = "xrLabel40";
            this.xrLabel40.RowSpan = 2;
            this.xrLabel40.Text = "\r\nf";
            this.xrLabel40.Weight = 0.61458026757914D;
            // 
            // xrLabel41
            // 
            this.xrLabel41.Dpi = 100F;
            this.xrLabel41.Name = "xrLabel41";
            this.xrLabel41.RowSpan = 2;
            this.xrLabel41.Text = "g=(h+i+j+k+l+m)";
            this.xrLabel41.Weight = 0.936176934867406D;
            // 
            // xrLabel42
            // 
            this.xrLabel42.Dpi = 100F;
            this.xrLabel42.Multiline = true;
            this.xrLabel42.Name = "xrLabel42";
            this.xrLabel42.RowSpan = 2;
            this.xrLabel42.Text = "\r\nh";
            this.xrLabel42.Weight = 0.833825260212109D;
            // 
            // xrLabel43
            // 
            this.xrLabel43.Dpi = 100F;
            this.xrLabel43.Multiline = true;
            this.xrLabel43.Name = "xrLabel43";
            this.xrLabel43.RowSpan = 2;
            this.xrLabel43.Text = "\r\ni";
            this.xrLabel43.Weight = 0.73958515369134659D;
            // 
            // xrLabel47
            // 
            this.xrLabel47.Dpi = 100F;
            this.xrLabel47.Multiline = true;
            this.xrLabel47.Name = "xrLabel47";
            this.xrLabel47.RowSpan = 2;
            this.xrLabel47.Text = "\r\nj";
            this.xrLabel47.Weight = 0.79999983019070076D;
            // 
            // xrLabel48
            // 
            this.xrLabel48.Dpi = 100F;
            this.xrLabel48.Multiline = true;
            this.xrLabel48.Name = "xrLabel48";
            this.xrLabel48.RowSpan = 2;
            this.xrLabel48.Text = "\r\nk";
            this.xrLabel48.Weight = 0.70000592773018433D;
            // 
            // xrLabel49
            // 
            this.xrLabel49.Dpi = 100F;
            this.xrLabel49.Multiline = true;
            this.xrLabel49.Name = "xrLabel49";
            this.xrLabel49.RowSpan = 2;
            this.xrLabel49.Text = "\r\nl";
            this.xrLabel49.Weight = 0.79666475935196057D;
            // 
            // xrLabel50
            // 
            this.xrLabel50.Dpi = 100F;
            this.xrLabel50.Multiline = true;
            this.xrLabel50.Name = "xrLabel50";
            this.xrLabel50.RowSpan = 2;
            this.xrLabel50.Text = "\r\nm";
            this.xrLabel50.Weight = 0.55207815857780984D;
            // 
            // xrLabel21
            // 
            this.xrLabel21.Dpi = 100F;
            this.xrLabel21.Multiline = true;
            this.xrLabel21.Name = "xrLabel21";
            this.xrLabel21.RowSpan = 2;
            this.xrLabel21.Text = "\r\nn";
            this.xrLabel21.Weight = 0.47289665812408976D;
            // 
            // xrTableRow8
            // 
            this.xrTableRow8.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell88,
            this.xrTableCell89,
            this.xrTableCell90,
            this.xrTableCell91,
            this.xrTableCell92,
            this.xrTableCell93,
            this.xrTableCell94,
            this.xrTableCell95,
            this.xrTableCell96,
            this.xrTableCell97,
            this.xrTableCell98,
            this.xrTableCell99,
            this.xrTableCell100,
            this.xrTableCell101});
            this.xrTableRow8.Dpi = 100F;
            this.xrTableRow8.Name = "xrTableRow8";
            this.xrTableRow8.Weight = 1D;
            // 
            // xrTableCell88
            // 
            this.xrTableCell88.Dpi = 100F;
            this.xrTableCell88.Name = "xrTableCell88";
            this.xrTableCell88.RowSpan = 0;
            this.xrTableCell88.Text = "xrTableCell88";
            this.xrTableCell88.Weight = 0.78331329345703127D;
            // 
            // xrTableCell89
            // 
            this.xrTableCell89.Dpi = 100F;
            this.xrTableCell89.Name = "xrTableCell89";
            this.xrTableCell89.RowSpan = 0;
            this.xrTableCell89.Text = "xrTableCell89";
            this.xrTableCell89.Weight = 0.68750473022460934D;
            // 
            // xrTableCell90
            // 
            this.xrTableCell90.Dpi = 100F;
            this.xrTableCell90.Name = "xrTableCell90";
            this.xrTableCell90.RowSpan = 0;
            this.xrTableCell90.Text = "xrTableCell90";
            this.xrTableCell90.Weight = 0.93749923706054694D;
            // 
            // xrTableCell91
            // 
            this.xrTableCell91.Dpi = 100F;
            this.xrTableCell91.Name = "xrTableCell91";
            this.xrTableCell91.RowSpan = 0;
            this.xrTableCell91.Text = "xrTableCell91";
            this.xrTableCell91.Weight = 1.4158546229597175D;
            // 
            // xrTableCell92
            // 
            this.xrTableCell92.Dpi = 100F;
            this.xrTableCell92.Name = "xrTableCell92";
            this.xrTableCell92.RowSpan = 0;
            this.xrTableCell92.Text = "xrTableCell92";
            this.xrTableCell92.Weight = 0.50000293831874854D;
            // 
            // xrTableCell93
            // 
            this.xrTableCell93.Dpi = 100F;
            this.xrTableCell93.Name = "xrTableCell93";
            this.xrTableCell93.RowSpan = 0;
            this.xrTableCell93.Text = "xrTableCell93";
            this.xrTableCell93.Weight = 0.61458026757914D;
            // 
            // xrTableCell94
            // 
            this.xrTableCell94.Dpi = 100F;
            this.xrTableCell94.Name = "xrTableCell94";
            this.xrTableCell94.RowSpan = 0;
            this.xrTableCell94.Text = "xrTableCell94";
            this.xrTableCell94.Weight = 0.936176934867406D;
            // 
            // xrTableCell95
            // 
            this.xrTableCell95.Dpi = 100F;
            this.xrTableCell95.Name = "xrTableCell95";
            this.xrTableCell95.RowSpan = 0;
            this.xrTableCell95.Text = "xrTableCell95";
            this.xrTableCell95.Weight = 0.833825260212109D;
            // 
            // xrTableCell96
            // 
            this.xrTableCell96.Dpi = 100F;
            this.xrTableCell96.Name = "xrTableCell96";
            this.xrTableCell96.RowSpan = 0;
            this.xrTableCell96.Text = "xrTableCell96";
            this.xrTableCell96.Weight = 0.73958515369134659D;
            // 
            // xrTableCell97
            // 
            this.xrTableCell97.Dpi = 100F;
            this.xrTableCell97.Name = "xrTableCell97";
            this.xrTableCell97.RowSpan = 0;
            this.xrTableCell97.Text = "xrTableCell97";
            this.xrTableCell97.Weight = 0.79999983019070076D;
            // 
            // xrTableCell98
            // 
            this.xrTableCell98.Dpi = 100F;
            this.xrTableCell98.Name = "xrTableCell98";
            this.xrTableCell98.RowSpan = 0;
            this.xrTableCell98.Text = "xrTableCell98";
            this.xrTableCell98.Weight = 0.70000592773018433D;
            // 
            // xrTableCell99
            // 
            this.xrTableCell99.Dpi = 100F;
            this.xrTableCell99.Name = "xrTableCell99";
            this.xrTableCell99.RowSpan = 0;
            this.xrTableCell99.Text = "xrTableCell99";
            this.xrTableCell99.Weight = 0.79666475935196057D;
            // 
            // xrTableCell100
            // 
            this.xrTableCell100.Dpi = 100F;
            this.xrTableCell100.Name = "xrTableCell100";
            this.xrTableCell100.RowSpan = 0;
            this.xrTableCell100.Text = "xrTableCell100";
            this.xrTableCell100.Weight = 0.55207815857780984D;
            // 
            // xrTableCell101
            // 
            this.xrTableCell101.Dpi = 100F;
            this.xrTableCell101.Name = "xrTableCell101";
            this.xrTableCell101.RowSpan = 0;
            this.xrTableCell101.Text = "xrTableCell101";
            this.xrTableCell101.Weight = 0.47289665812408976D;
            // 
            // xrLabel97
            // 
            this.xrLabel97.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel97.CanGrow = false;
            this.xrLabel97.Dpi = 100F;
            this.xrLabel97.Font = new System.Drawing.Font("Agency FB", 12F, System.Drawing.FontStyle.Bold);
            this.xrLabel97.LocationFloat = new DevExpress.Utils.PointFloat(744.8346F, 0F);
            this.xrLabel97.Multiline = true;
            this.xrLabel97.Name = "xrLabel97";
            this.xrLabel97.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel97.SizeF = new System.Drawing.SizeF(150.0004F, 25F);
            this.xrLabel97.StyleName = "PermbajtjaBold";
            this.xrLabel97.Text = "Emri Mbiemri";
            this.xrLabel97.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.xrLabel97.WordWrap = false;
            // 
            // xrLabel96
            // 
            this.xrLabel96.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel96.Dpi = 100F;
            this.xrLabel96.Font = new System.Drawing.Font("Agency FB", 12F);
            this.xrLabel96.LocationFloat = new DevExpress.Utils.PointFloat(0F, 25F);
            this.xrLabel96.Multiline = true;
            this.xrLabel96.Name = "xrLabel96";
            this.xrLabel96.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel96.SizeF = new System.Drawing.SizeF(147.0813F, 25F);
            this.xrLabel96.StyleName = "Permbajtja";
            this.xrLabel96.Text = "Shpjegim:";
            this.xrLabel96.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel95
            // 
            this.xrLabel95.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel95.Dpi = 100F;
            this.xrLabel95.Font = new System.Drawing.Font("Agency FB", 12F);
            this.xrLabel95.LocationFloat = new DevExpress.Utils.PointFloat(0F, 50F);
            this.xrLabel95.Multiline = true;
            this.xrLabel95.Name = "xrLabel95";
            this.xrLabel95.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel95.SizeF = new System.Drawing.SizeF(894.8351F, 25F);
            this.xrLabel95.StyleName = "Permbajtja";
            this.xrLabel95.Text = "Ne rastin kur ky liber do te printohet, per qellime te ruajtjes/ mbajtjes se doku" +
    "mentacionit, cdo faqe e printuar duhet te kete ne krye permbajtjen / rreshtat ng" +
    "a 1 ne 12. ";
            this.xrLabel95.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel98
            // 
            this.xrLabel98.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel98.Dpi = 100F;
            this.xrLabel98.Font = new System.Drawing.Font("Agency FB", 12F);
            this.xrLabel98.LocationFloat = new DevExpress.Utils.PointFloat(0F, 75F);
            this.xrLabel98.Multiline = true;
            this.xrLabel98.Name = "xrLabel98";
            this.xrLabel98.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel98.SizeF = new System.Drawing.SizeF(382.4175F, 25F);
            this.xrLabel98.StyleName = "Permbajtja";
            this.xrLabel98.Text = "Nr i rreshtave mund te shtohet ne perputhje me nr e transaksioneve";
            this.xrLabel98.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // ds_RapLiberShitje1
            // 
            this.ds_RapLiberShitje1.DataSetName = "ds_RapLiberShitje";
            this.ds_RapLiberShitje1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // pRC_RAP_LIBERSHITJETableAdapter
            // 
            this.pRC_RAP_LIBERSHITJETableAdapter.ClearBeforeFill = true;
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable4});
            this.GroupHeader1.Dpi = 100F;
            this.GroupHeader1.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("KODKONFIGAMBJENTE", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending),
            new DevExpress.XtraReports.UI.GroupField("NRDOK", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.GroupHeader1.HeightF = 25F;
            this.GroupHeader1.Name = "GroupHeader1";
            // 
            // xrTable4
            // 
            this.xrTable4.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable4.Dpi = 100F;
            this.xrTable4.Font = new System.Drawing.Font("Agency FB", 12F);
            this.xrTable4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable4.Name = "xrTable4";
            this.xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow9});
            this.xrTable4.SizeF = new System.Drawing.SizeF(1076.999F, 25F);
            this.xrTable4.StylePriority.UseBorders = false;
            this.xrTable4.StylePriority.UseFont = false;
            // 
            // xrTableRow9
            // 
            this.xrTableRow9.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrLabel53,
            this.xrLabel54,
            this.xrLabel55,
            this.xrLabel56,
            this.xrLabel99,
            this.xrLabel57,
            this.xrLabel58,
            this.xrLabel59,
            this.xrLabel62,
            this.xrLabel63,
            this.xrLabel65,
            this.xrLabel68,
            this.xrLabel69,
            this.xrLabel24});
            this.xrTableRow9.Dpi = 100F;
            this.xrTableRow9.Name = "xrTableRow9";
            this.xrTableRow9.Weight = 1D;
            // 
            // xrLabel53
            // 
            this.xrLabel53.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "PRC_RAP_LIBERSHITJE.NRDOK")});
            this.xrLabel53.Dpi = 100F;
            this.xrLabel53.Name = "xrLabel53";
            this.xrLabel53.StylePriority.UseTextAlignment = false;
            this.xrLabel53.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrLabel53.Weight = 0.783311996459961D;
            // 
            // xrLabel54
            // 
            this.xrLabel54.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "PRC_RAP_LIBERSHITJE.NRSERIAL")});
            this.xrLabel54.Dpi = 100F;
            this.xrLabel54.Name = "xrLabel54";
            this.xrLabel54.Weight = 0.68750000000000011D;
            // 
            // xrLabel55
            // 
            this.xrLabel55.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "PRC_RAP_LIBERSHITJE.DTDOK", "{0:dd/MM/yyyy}")});
            this.xrLabel55.Dpi = 100F;
            this.xrLabel55.Name = "xrLabel55";
            this.xrLabel55.StylePriority.UseTextAlignment = false;
            this.xrLabel55.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrLabel55.Weight = 0.93750022888183582D;
            // 
            // xrLabel56
            // 
            this.xrLabel56.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "PRC_RAP_LIBERSHITJE.EMERTIMIKF")});
            this.xrLabel56.Dpi = 100F;
            this.xrLabel56.Name = "xrLabel56";
            this.xrLabel56.Weight = 1.4158580135615166D;
            // 
            // xrLabel99
            // 
            this.xrLabel99.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "PRC_RAP_LIBERSHITJE.QYTETIEMRI")});
            this.xrLabel99.Dpi = 100F;
            this.xrLabel99.Name = "xrLabel99";
            this.xrLabel99.Weight = 0.50000429976783423D;
            // 
            // xrLabel57
            // 
            this.xrLabel57.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "PRC_RAP_LIBERSHITJE.NIPTKF")});
            this.xrLabel57.Dpi = 100F;
            this.xrLabel57.Name = "xrLabel57";
            this.xrLabel57.Weight = 0.61457827721607128D;
            // 
            // xrLabel58
            // 
            this.xrLabel58.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "PRC_RAP_LIBERSHITJE.TotaliG")});
            this.xrLabel58.Dpi = 100F;
            this.xrLabel58.Name = "xrLabel58";
            this.xrLabel58.StylePriority.UseTextAlignment = false;
            xrSummary1.FormatString = "{0:#,#.00}";
            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrLabel58.Summary = xrSummary1;
            this.xrLabel58.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrLabel58.Weight = 0.93617774960102118D;
            // 
            // xrLabel59
            // 
            this.xrLabel59.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "PRC_RAP_LIBERSHITJE.SHITJEPERJASHTUAR")});
            this.xrLabel59.Dpi = 100F;
            this.xrLabel59.Name = "xrLabel59";
            this.xrLabel59.StylePriority.UseTextAlignment = false;
            xrSummary2.FormatString = "{0:#,#.00}";
            xrSummary2.Func = DevExpress.XtraReports.UI.SummaryFunc.Max;
            xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrLabel59.Summary = xrSummary2;
            this.xrLabel59.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrLabel59.Weight = 0.83382605711133939D;
            // 
            // xrLabel62
            // 
            this.xrLabel62.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "PRC_RAP_LIBERSHITJE.VLEFTAEXPORTE")});
            this.xrLabel62.Dpi = 100F;
            this.xrLabel62.Name = "xrLabel62";
            this.xrLabel62.StylePriority.UseTextAlignment = false;
            xrSummary3.FormatString = "{0:#,#.00}";
            xrSummary3.Func = DevExpress.XtraReports.UI.SummaryFunc.Max;
            xrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrLabel62.Summary = xrSummary3;
            this.xrLabel62.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrLabel62.Weight = 0.73958166119267876D;
            // 
            // xrLabel63
            // 
            this.xrLabel63.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "PRC_RAP_LIBERSHITJE.VLEFTA20")});
            this.xrLabel63.Dpi = 100F;
            this.xrLabel63.Name = "xrLabel63";
            this.xrLabel63.StylePriority.UseTextAlignment = false;
            xrSummary4.FormatString = "{0:#,#.00}";
            xrSummary4.Func = DevExpress.XtraReports.UI.SummaryFunc.Max;
            xrSummary4.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrLabel63.Summary = xrSummary4;
            this.xrLabel63.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrLabel63.Weight = 0.80000678806059833D;
            // 
            // xrLabel65
            // 
            this.xrLabel65.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "PRC_RAP_LIBERSHITJE.TVSH20")});
            this.xrLabel65.Dpi = 100F;
            this.xrLabel65.Name = "xrLabel65";
            this.xrLabel65.StylePriority.UseTextAlignment = false;
            xrSummary5.FormatString = "{0:#,#.00}";
            xrSummary5.Func = DevExpress.XtraReports.UI.SummaryFunc.Max;
            xrSummary5.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrLabel65.Summary = xrSummary5;
            this.xrLabel65.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrLabel65.Weight = 0.70000616336883126D;
            // 
            // xrLabel68
            // 
            this.xrLabel68.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "PRC_RAP_LIBERSHITJE.VLEFTA10")});
            this.xrLabel68.Dpi = 100F;
            this.xrLabel68.Name = "xrLabel68";
            this.xrLabel68.StylePriority.UseTextAlignment = false;
            xrSummary6.FormatString = "{0:#,#.00}";
            xrSummary6.Func = DevExpress.XtraReports.UI.SummaryFunc.Max;
            xrSummary6.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrLabel68.Summary = xrSummary6;
            this.xrLabel68.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrLabel68.Weight = 0.79666479829612435D;
            // 
            // xrLabel69
            // 
            this.xrLabel69.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "PRC_RAP_LIBERSHITJE.TVSH10")});
            this.xrLabel69.Dpi = 100F;
            this.xrLabel69.Name = "xrLabel69";
            this.xrLabel69.StylePriority.UseTextAlignment = false;
            xrSummary7.FormatString = "{0:#,#.00}";
            xrSummary7.Func = DevExpress.XtraReports.UI.SummaryFunc.Max;
            xrSummary7.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrLabel69.Summary = xrSummary7;
            this.xrLabel69.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrLabel69.Weight = 0.55207859384908464D;
            // 
            // xrLabel24
            // 
            this.xrLabel24.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "PRC_RAP_LIBERSHITJE.degaadministrative")});
            this.xrLabel24.Dpi = 100F;
            this.xrLabel24.Name = "xrLabel24";
            this.xrLabel24.Weight = 0.47289644977200096D;
            // 
            // GroupHeader2
            // 
            this.GroupHeader2.Dpi = 100F;
            this.GroupHeader2.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("DTDOK", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.GroupHeader2.HeightF = 0F;
            this.GroupHeader2.Level = 1;
            this.GroupHeader2.Name = "GroupHeader2";
            // 
            // TotaliG
            // 
            this.TotaliG.DataMember = "PRC_RAP_LIBERSHITJE";
            this.TotaliG.Expression = "[TVSH10] + [TVSH20] + [VLEFTA10] + [VLEFTA20] + [VLEFTAEXPORTE] + [SHITJEPERJASHT" +
    "UAR]";
            this.TotaliG.Name = "TotaliG";
            // 
            // PermbajtjaBold
            // 
            this.PermbajtjaBold.Font = new System.Drawing.Font("Agency FB", 12F, System.Drawing.FontStyle.Bold);
            this.PermbajtjaBold.ForeColor = System.Drawing.Color.Black;
            this.PermbajtjaBold.Name = "PermbajtjaBold";
            // 
            // Permbajtja
            // 
            this.Permbajtja.Font = new System.Drawing.Font("Agency FB", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Permbajtja.ForeColor = System.Drawing.Color.Black;
            this.Permbajtja.Name = "Permbajtja";
            // 
            // Titulli
            // 
            this.Titulli.Font = new System.Drawing.Font("Agency FB", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Titulli.Name = "Titulli";
            // 
            // GroupHeader3
            // 
            this.GroupHeader3.Dpi = 100F;
            this.GroupHeader3.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("IDSHITJEKOKA", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.GroupHeader3.HeightF = 0F;
            this.GroupHeader3.Level = 2;
            this.GroupHeader3.Name = "GroupHeader3";
            // 
            // PageFooter
            // 
            this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo1,
            this.xrLabel96,
            this.xrLabel95,
            this.xrLabel98,
            this.xrLabel97});
            this.PageFooter.Dpi = 100F;
            this.PageFooter.HeightF = 100F;
            this.PageFooter.Name = "PageFooter";
            // 
            // xrPageInfo1
            // 
            this.xrPageInfo1.Dpi = 100F;
            this.xrPageInfo1.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrPageInfo1.ForeColor = System.Drawing.Color.Gray;
            this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(974.5011F, 25F);
            this.xrPageInfo1.Name = "xrPageInfo1";
            this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo1.SizeF = new System.Drawing.SizeF(102.499F, 25F);
            this.xrPageInfo1.StylePriority.UseFont = false;
            this.xrPageInfo1.StylePriority.UseForeColor = false;
            this.xrPageInfo1.StylePriority.UseTextAlignment = false;
            this.xrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.Dpi = 100F;
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.Weight = 0.16656579622483664D;
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "PRC_RAP_LIBERSHITJE.TVSH10")});
            this.xrTableCell8.Dpi = 100F;
            this.xrTableCell8.Name = "xrTableCell8";
            xrSummary8.FormatString = "{0:#,#.00}";
            xrSummary8.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrTableCell8.Summary = xrSummary8;
            this.xrTableCell8.Weight = 0.19445259429676703D;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "PRC_RAP_LIBERSHITJE.VLEFTA10")});
            this.xrTableCell7.Dpi = 100F;
            this.xrTableCell7.Name = "xrTableCell7";
            xrSummary9.FormatString = "{0:#,#.00}";
            xrSummary9.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrTableCell7.Summary = xrSummary9;
            this.xrTableCell7.Weight = 0.28059518847077547D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "PRC_RAP_LIBERSHITJE.TVSH20")});
            this.xrTableCell6.Dpi = 100F;
            this.xrTableCell6.Name = "xrTableCell6";
            xrSummary10.FormatString = "{0:#,#.00}";
            xrSummary10.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrTableCell6.Summary = xrSummary10;
            this.xrTableCell6.Weight = 0.24655234472160031D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "PRC_RAP_LIBERSHITJE.VLEFTA20")});
            this.xrTableCell5.Dpi = 100F;
            this.xrTableCell5.Name = "xrTableCell5";
            xrSummary11.FormatString = "{0:#,#.00}";
            xrSummary11.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrTableCell5.Summary = xrSummary11;
            this.xrTableCell5.Weight = 0.2817751369829789D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "PRC_RAP_LIBERSHITJE.VLEFTAEXPORTE")});
            this.xrTableCell4.Dpi = 100F;
            this.xrTableCell4.Name = "xrTableCell4";
            xrSummary12.FormatString = "{0:#,#.00}";
            xrSummary12.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrTableCell4.Summary = xrSummary12;
            this.xrTableCell4.Weight = 0.26049105181848237D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "PRC_RAP_LIBERSHITJE.SHITJEPERJASHTUAR")});
            this.xrTableCell3.Dpi = 100F;
            this.xrTableCell3.Name = "xrTableCell3";
            xrSummary13.FormatString = "{0:#,#.00}";
            xrSummary13.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrTableCell3.Summary = xrSummary13;
            this.xrTableCell3.Weight = 0.293687183157087D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "PRC_RAP_LIBERSHITJE.TotaliG")});
            this.xrTableCell2.Dpi = 100F;
            this.xrTableCell2.Name = "xrTableCell2";
            xrSummary14.FormatString = "{0:#,#.00}";
            xrSummary14.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrTableCell2.Summary = xrSummary14;
            this.xrTableCell2.Weight = 0.32973586177602626D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrTableCell1.Dpi = 100F;
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseBorders = false;
            this.xrTableCell1.StylePriority.UseTextAlignment = false;
            this.xrTableCell1.Text = "Shuma totale";
            this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableCell1.Weight = 1.7395056630917247D;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell2,
            this.xrTableCell3,
            this.xrTableCell4,
            this.xrTableCell5,
            this.xrTableCell6,
            this.xrTableCell7,
            this.xrTableCell8,
            this.xrTableCell9});
            this.xrTableRow1.Dpi = 100F;
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.StyleName = "PermbajtjaBold";
            this.xrTableRow1.StylePriority.UseBorders = false;
            this.xrTableRow1.StylePriority.UseTextAlignment = false;
            this.xrTableRow1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTable1
            // 
            this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable1.BorderWidth = 2F;
            this.xrTable1.Dpi = 100F;
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0.0001192093F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(1077F, 25F);
            this.xrTable1.StylePriority.UseBorders = false;
            this.xrTable1.StylePriority.UseBorderWidth = false;
            this.xrTable1.StylePriority.UsePadding = false;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.ReportFooter.BorderWidth = 2F;
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2,
            this.xrTable1});
            this.ReportFooter.Dpi = 100F;
            this.ReportFooter.HeightF = 50F;
            this.ReportFooter.Name = "ReportFooter";
            this.ReportFooter.StylePriority.UseBorders = false;
            this.ReportFooter.StylePriority.UseBorderWidth = false;
            // 
            // xrTable2
            // 
            this.xrTable2.Dpi = 100F;
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 25F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
            this.xrTable2.SizeF = new System.Drawing.SizeF(1076.999F, 25F);
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell11,
            this.xrTableCell19,
            this.xrTableCell20,
            this.xrTableCell21,
            this.xrTableCell22,
            this.xrTableCell23,
            this.xrTableCell24,
            this.xrTableCell25});
            this.xrTableRow3.Dpi = 100F;
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.StyleName = "Permbajtja";
            this.xrTableRow3.StylePriority.UseBorders = false;
            this.xrTableRow3.StylePriority.UseTextAlignment = false;
            this.xrTableRow3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrTableRow3.Weight = 1D;
            // 
            // xrTableCell11
            // 
            this.xrTableCell11.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell11.Dpi = 100F;
            this.xrTableCell11.Multiline = true;
            this.xrTableCell11.Name = "xrTableCell11";
            this.xrTableCell11.StylePriority.UseBorders = false;
            this.xrTableCell11.Text = "Kutia sipas Formularit te Deklarimit dhe Pageses se TVSH-se";
            this.xrTableCell11.Weight = 2.0374084375843124D;
            // 
            // xrTableCell19
            // 
            this.xrTableCell19.Dpi = 100F;
            this.xrTableCell19.Multiline = true;
            this.xrTableCell19.Name = "xrTableCell19";
            this.xrTableCell19.Text = "kutia (9)";
            this.xrTableCell19.Weight = 0.28916841442689672D;
            // 
            // xrTableCell20
            // 
            this.xrTableCell20.Dpi = 100F;
            this.xrTableCell20.Multiline = true;
            this.xrTableCell20.Name = "xrTableCell20";
            this.xrTableCell20.Text = "kutia (10)";
            this.xrTableCell20.Weight = 0.2564843280070439D;
            // 
            // xrTableCell21
            // 
            this.xrTableCell21.Dpi = 100F;
            this.xrTableCell21.Multiline = true;
            this.xrTableCell21.Name = "xrTableCell21";
            this.xrTableCell21.Text = "kutia (11)";
            this.xrTableCell21.Weight = 0.27744051409585363D;
            // 
            // xrTableCell22
            // 
            this.xrTableCell22.Dpi = 100F;
            this.xrTableCell22.Multiline = true;
            this.xrTableCell22.Name = "xrTableCell22";
            this.xrTableCell22.Text = "kutia (12)";
            this.xrTableCell22.Weight = 0.24275891731648433D;
            // 
            // xrTableCell23
            // 
            this.xrTableCell23.Dpi = 100F;
            this.xrTableCell23.Multiline = true;
            this.xrTableCell23.Name = "xrTableCell23";
            this.xrTableCell23.Text = "kutia (13)";
            this.xrTableCell23.Weight = 0.276278684589383D;
            // 
            // xrTableCell24
            // 
            this.xrTableCell24.Dpi = 100F;
            this.xrTableCell24.Multiline = true;
            this.xrTableCell24.Name = "xrTableCell24";
            this.xrTableCell24.Text = "kutia (14)";
            this.xrTableCell24.Weight = 0.19146095536449326D;
            // 
            // xrTableCell25
            // 
            this.xrTableCell25.Dpi = 100F;
            this.xrTableCell25.Multiline = true;
            this.xrTableCell25.Name = "xrTableCell25";
            this.xrTableCell25.Text = "kutia (15)";
            this.xrTableCell25.Weight = 0.16399944966064964D;
            // 
            // Rap_LibriShitjeveSunPetrolium
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.PageHeader,
            this.ReportFooter,
            this.GroupHeader1,
            this.GroupHeader2,
            this.GroupHeader3,
            this.PageFooter});
            this.CalculatedFields.AddRange(new DevExpress.XtraReports.UI.CalculatedField[] {
            this.TotaliG});
            this.DataAdapter = this.pRC_RAP_LIBERSHITJETableAdapter;
            this.DataMember = "PRC_RAP_LIBERSHITJE";
            this.DataSource = this.ds_RapLiberShitje1;
            this.Font = new System.Drawing.Font("Calibri", 10F);
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(11, 10, 48, 0);
            this.PageHeight = 927;
            this.PageWidth = 1100;
            this.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.PermbajtjaBold,
            this.Permbajtja,
            this.Titulli});
            this.Version = "16.1";
            this.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Rap_LibriShitjeve_BeforePrint);
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_RapLiberShitje1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
    }
}
