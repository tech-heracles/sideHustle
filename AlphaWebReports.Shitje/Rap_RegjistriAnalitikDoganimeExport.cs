using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_RegjistriAnalitikDoganimeExport : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_RegjistriAnalitikDoganimeExport(){InitializeComponent();} 

        private CultureInfo ci;

        public Rap_RegjistriAnalitikDoganimeExport(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_RegjistriAnalitikDoganimeExport(CultureInfo ci, int idNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)
        {
            this.ci = ci;
            InitializeComponent();
            EmrateLabelave(ci);
            nrdok.Value   = raport.Parameters[3].Value;
            dtdok.Value = raport.Parameters[1].Value;
            dtRegj.Value = raport.Parameters[2].Value;
            KlientFurnitori.Value = raport.Parameters[4].Value;

        }

        private void EmrateLabelave(CultureInfo ci)
        {
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings",
                      System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel3.Text = rm.GetString("RaportRegjAnalitikDogExportTitulli", ci);

            xrLabel19.Text = rm.GetString("labelRaportKlienti", ci);
        }
    }
}
