using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.Blerje
{
    public partial class Rap_RegjistriAnalitikDoganimeImport : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_RegjistriAnalitikDoganimeImport(){InitializeComponent();} 

        private CultureInfo ci;

        public Rap_RegjistriAnalitikDoganimeImport(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_RegjistriAnalitikDoganimeImport(CultureInfo ci, int idNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)
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
        }
    }
}
