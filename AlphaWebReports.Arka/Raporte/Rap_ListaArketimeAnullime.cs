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
  
    public partial class Rap_ListaArketimeAnullime : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_ListaArketimeAnullime(){InitializeComponent();} 
        public Rap_ListaArketimeAnullime(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, report)
        {

        }
        public Rap_ListaArketimeAnullime(CultureInfo ci, int idNdermarrje, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();

            parameter1.Value = raport.Parameters["filterLlojDokumenti"].Value;
            parameter2.Value = raport.Parameters["filterArkaBankaEmer"].Value;
            parameter3.Value = raport.Parameters["filterNrSerial"].Value;
            parameter4.Value = raport.Parameters["filterDtDok"].Value;
            parameter5.Value = raport.Parameters["filterNumerDokumenti"].Value;
            parameter6.Value = raport.Parameters["filterKompania"].Value;
            parameter7.Value = raport.Parameters["filterCustomerNr"].Value;
            EmraTeLabelave(ci, parameter6.Value.ToString() != "");
        }
        private void EmraTeLabelave(CultureInfo ci, bool shtoFilterKompania)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            xrTableCell1.Text = rm.GetString("labelRaportiKodiDealer", ci);
            xrTableCell21.Text = rm.GetString("labelRaportiEmriDyqanit", ci);
            xrTableCell5.Text = rm.GetString("labelRaportiLlojDokumentit", ci);
            xrTableCell7.Text = rm.GetString("labelRaportData", ci);
            xrTableCell8.Text = rm.GetString("labelNrArketimi", ci);
            xrTableCell9.Text = rm.GetString("labelRaportNrSerial", ci);
            xrTableCell10.Text = rm.GetString("labelRaportiMSISDN", ci);
            xrTableCell11.Text = rm.GetString("labelRaportiVleraPageses", ci);
            xrTableCell18.Text = rm.GetString("labelRaportiPerfaqesuesiShitjes", ci);
            xrTableCell19.Text = rm.GetString("labelRaportiShenime", ci);
            xrTableCell20.Text = rm.GetString("labelFilterAvancuarStatusi", ci);
            xrLabel12.Text = rm.GetString("TitullRaportiArketimeAnullime", ci);
            xrTableCell29.Text = rm.GetString("labelRaportiKodiDyqanit", ci);
            xrTableCell31.Text = rm.GetString("labelRaportiCustomerNR", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
        }
    }
}
