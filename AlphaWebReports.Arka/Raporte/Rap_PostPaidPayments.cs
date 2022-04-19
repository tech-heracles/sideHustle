using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;

namespace AlphaWebReports.RaportetDs.Arka.Raporte
{
  
    public partial class Rap_PostPaidPayments : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_PostPaidPayments(){InitializeComponent();} 
        public Rap_PostPaidPayments(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, report)
        {

        }
        public Rap_PostPaidPayments(CultureInfo ci, int idNdermarrje, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();

            parameter1.Value = raport.Parameters["filterLlojDokumenti"].Value;
            parameter2.Value = raport.Parameters["filterArkaBankaEmer"].Value;
            parameter4.Value = raport.Parameters["filterDtDok"].Value;
            parameter7.Value = raport.Parameters["filterKompania"].Value;
            EmraTeLabelave(ci);
        }
        private void EmraTeLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            xrTableCell1.Text = rm.GetString("labelRaportiKodiDealer", ci);
            xrTableCell39.Text = rm.GetString("lblRaportiKodiIDyqanit", ci); 
            xrTableCell21.Text = rm.GetString("labelRaportiEmriDyqanit", ci);
            xrTableCell5.Text = rm.GetString("labelRaportiDataEPageses", ci);
            xrTableCell7.Text = rm.GetString("lbelRaportiNumriIDokumentit", ci);
            xrTableCell37.Text = rm.GetString("labelRaportNrSerial", ci);
            xrTableCell8.Text = rm.GetString("labelRaportiVleraPageses", ci);
            xrTableCell9.Text = rm.GetString("labelRaportiEmriIKlientit", ci);
            xrTableCell18.Text = rm.GetString("labelRaportiNrSerialIFatures", ci);
            xrTableCell19.Text = rm.GetString("labelRaportiPeriudhaEFatures", ci);
            xrTableCell20.Text = rm.GetString("labelPagesePlotePjesshme", ci);
            xrTableCell29.Text = rm.GetString("labelRaportiVleraEPerditesuar", ci);
            xrTableCell27.Text = rm.GetString("labelRaportVlera", ci);
            xrTableCell23.Text = rm.GetString("labelRaportiPerfaqesuesiShitjes", ci);
            xrTableCell28.Text = rm.GetString("labelRaportiShenime", ci);
            xrTableCell24.Text = rm.GetString("lblRaportStatusi", ci);
            xrTableCell41.Text = rm.GetString("lblRaportiNrArketimiAnulluar", ci);          
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
        }
    }
}
