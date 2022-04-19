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
  
    public partial class Rap_DailyGuaranteePayment : DevExpress.XtraReports.UI.XtraReport
    {   
		public Rap_DailyGuaranteePayment(){InitializeComponent();} 
        public Rap_DailyGuaranteePayment(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, report)
        {

        }
        public Rap_DailyGuaranteePayment(CultureInfo ci, int idNdermarrje, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            
            parameter2.Value = raport.Parameters["filterKompania"].Value;
            parameter4.Value = raport.Parameters["filterArkaBankaEmer"].Value;
            dateDokumenti.Value = raport.Parameters["filterDtDok"].Value;
            EmraTeLabelave(ci);
        }
        private void EmraTeLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            xrTableCell1.Text = rm.GetString("labelRaportiKodiDealer", ci);
            xrTableCell21.Text = rm.GetString("labelRaportiEmriDyqanit", ci);
            xrTableCell5.Text = rm.GetString("labelRaportiEmriIKlientit", ci);
            xrTableCell7.Text = rm.GetString("lblRaportNrLlogarie", ci);
            xrTableCell8.Text = rm.GetString("labelRaportiMSISDN", ci);
            xrTableCell9.Text = rm.GetString("lbelRaportiNumriIDokumentit", ci);
            xrTableCell10.Text = rm.GetString("labelRaportNrSerial", ci);
            xrTableCell11.Text = rm.GetString("labelRaportVlera", ci);
            xrTableCell18.Text = rm.GetString("labelRaportiTipiGarancise", ci);
            xrTableCell20.Text = rm.GetString("labelRaportiShenime", ci);
            xrTableCell23.Text = rm.GetString("labelFilterAvancuarStatusi", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);       
        }
    }
}
