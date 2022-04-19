using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;

namespace AlphaWebReports.RaportetDs.ListPagesat.Raportet
{
    public partial class Rap_ListepagesaSipasDep : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_ListepagesaSipasDep(){InitializeComponent();} 

        public Rap_ListepagesaSipasDep(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_ListepagesaSipasDep(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
        }
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                      System.Reflection.Assembly.Load("App_GlobalResources"));


            xrLabel1.Text = rm.GetString("RaportPermbledheseListepagesaveSipasDep", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrTableCell7.Text = rm.GetString("filterKodDep", ci);
            xrTableCell9.Text = rm.GetString("filterKodNenDep", ci);
            xrTableCell1.Text = rm.GetString("labelNrPunonjes", ci);
            xrTableCell2.Text = rm.GetString("labelRaportTeTjera", ci);
            xrTableCell3.Text = rm.GetString("labelRaportEmertimi", ci);
            xrTableCell4.Text = rm.GetString("lblPaga", ci);
            xrTableCell5.Text = rm.GetString("lblShtesaPage", ci);
            xrTableCell7.Text = rm.GetString("labelRaportRoje", ci);
            xrTableCell8.Text = rm.GetString("lblTurne2", ci);
            xrTableCell9.Text = rm.GetString("lblTurne3", ci);
            xrTableCell10.Text = rm.GetString("labelPaaftesi", ci);
            xrTableCell6.Text = rm.GetString("lblRaportiDite", ci);
            xrTableCell94.Text = rm.GetString("labelRaportLEKE2", ci);
            xrTableCell21.Text = rm.GetString("lblShumaBruto", ci);
            xrTableCell22.Text = rm.GetString("labelRaportKontributSigShoq", ci);
            xrTableCell23.Text = rm.GetString("labelRaportKontributetSigShend", ci);
            xrTableCell90.Text = rm.GetString("labelRaportPagaMbiCilenLlogKontribshoq", ci);
            xrTableCell114.Text = rm.GetString("labelRaportPagaMbiCilenLlogKontribshend", ci);
            xrTableCell110.Text = rm.GetString("labelRaportSigshoqerorpunemarresi", ci);
            xrTableCell111.Text = rm.GetString("labelRaportSigshoqerorpunedhenesi15", ci);
            xrTableCell96.Text = rm.GetString("lblTotalikontSigShoq", ci);
            xrTableCell115.Text = rm.GetString("labelRaportSigshendpunemarresi", ci);
            xrTableCell116.Text = rm.GetString("labelRaportSigshendpunedhenesi", ci);
            xrTableCell97.Text = rm.GetString("lblTotalikontSigShend", ci);
            xrTableCell24.Text = rm.GetString("lblSigSupl", ci);
            xrTableCell117.Text = rm.GetString("labelRaportSigshendpunemarresisup", ci);
            xrTableCell118.Text = rm.GetString("labelRaportSigshendpunedhenesisup", ci);
            xrTableCell98.Text = rm.GetString("lblTotalikontSigShendsup", ci);
            xrTableCell25.Text = rm.GetString("lblTAPt", ci);
            xrTableCell26.Text = rm.GetString("labelRaportPagaMujoreNeto", ci);
            xrTableCell112.Text = rm.GetString("lblRaportdebitore", ci);
             xrTableCell28.Text = rm.GetString("lblmjeku", ci);
            xrTableCell101.Text = rm.GetString("lblinfer", ci);
            xrTableCell103.Text = rm.GetString("labelsindikata", ci);
            xrTableCell104.Text = rm.GetString("labelpension", ci);

            xrTableCell113.Text = rm.GetString("lblNdalesatjera", ci);

            xrTableCell29.Text = rm.GetString("lblShumaNdalesa", ci);
            xrTableCell105.Text = rm.GetString("lblShumaNeto", ci);
            xrLabel2.Text = rm.GetString("lblEkonomisti", ci);
            xrLabel4.Text = rm.GetString("lbldrejtekon", ci);

            xrLabel5.Text = rm.GetString("lbldrejBN", ci);
            xrLabel6.Text = rm.GetString("lbldrejPergj", ci);
            xrTableCell156.Text = rm.GetString("lblRastepaaft", ci);
            xrTableCell157.Text = rm.GetString("lblRastepens", ci);
            xrTableCell158.Text = rm.GetString("lblRasteLV", ci);
        }

        private void Rap_ListepagesaSipasDep_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }
    }
}
