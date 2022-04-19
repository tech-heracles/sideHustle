using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.RaportetDs.Magazina
{
    public partial class Rap_Magazina_MZHUKostoShtese_Turizem : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_Magazina_MZHUKostoShtese_Turizem() { InitializeComponent(); }
        public Rap_Magazina_MZHUKostoShtese_Turizem(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) :
            this(param.Ci, param.IdNdermarrje, report)
        {

        }
 
        public Rap_Magazina_MZHUKostoShtese_Turizem(CultureInfo ci, int idNdermarrje, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            parameter2.Value = raport.Parameters[1].Value;
            parameterIdNderm.Value = idNdermarrje;
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            lblTitullRpt.Text = rm.GetString("RaportMZHUPerllogaritjaKostosShtese_Title", ci);
        }
    }
}
