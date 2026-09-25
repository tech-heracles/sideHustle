using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_LibriShitjevePaMerge : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_LibriShitjevePaMerge(){InitializeComponent();} 
       

        public Rap_LibriShitjevePaMerge(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_LibriShitjevePaMerge(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi,
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


        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {

            ResourceManager rm = new ResourceManager("Resources.Strings",
                        System.Reflection.Assembly.Load("App_GlobalResources"));
           
            xrLabel1.Text = rm.GetString("RaportLibriShitjeveTitulli", ci);


        }
    }
}
