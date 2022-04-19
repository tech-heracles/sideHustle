using System;
using DevExpress.XtraReports.UI;
using System.Resources;
using System.Globalization;


namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_ShitjetSipasIntervaleve : XtraReport
    {
		public Rap_ShitjetSipasIntervaleve(){InitializeComponent();} 
        public Rap_ShitjetSipasIntervaleve(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, report)
        {

        }
        public Rap_ShitjetSipasIntervaleve(CultureInfo ci, XtraReport raport)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                       System.Reflection.Assembly.Load("App_GlobalResources"));
            InitializeComponent();
            EmrateLabelave(ci);
            parameter1.Value = raport.Parameters["filterDtDok"].Value;
            parameter2.Value = raport.Parameters["filterQyteti"].Value;
            parameter3.Value = raport.Parameters["filterKlientFurnitor"].Value;
            parameter4.Value = raport.Parameters["filterPikeshitjeFurnizmi"].Value;
            parameter5.Value = raport.Parameters["filterNumerLlogarie"].Value;
            parameter6.Value = raport.Parameters["filterLlojDokumenti"].Value;
            parameter7.Value = raport.Parameters["filterDegeAdministrative"].Value;
            parameter8.Value = raport.Parameters["filterAgjentShitje"].Value;
            parameter9.Value = raport.Parameters["filterGrupimDokP"].Value;
            parameter10.Value = raport.Parameters["filterGrupimDokD"].Value;
            parameter11.Value = raport.Parameters["filterGrupimDokT"].Value;
          
        }
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                       System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel1.Text = rm.GetString("RaportTitulliShitjetSipasIntervaleve", ci);
            xrLabel23.Text = rm.GetString("labelRaportNrKlienteve", ci);
            xrLabel24.Text = rm.GetString("labelRaportiTotalShitje", ci);
        }
    }
}
