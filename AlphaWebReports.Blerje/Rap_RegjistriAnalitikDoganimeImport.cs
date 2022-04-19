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
            //xrLabel24.Text = rm.GetString("labelRaportiTotali", ci) + ":";
            //xrLabel3.Text = rm.GetString("RaportFleteDoganoreTitulli", ci);
            //xrTableCell1.Text = rm.GetString("labelNrFleteDoganore", ci);
            //xrTableCell2.Text = rm.GetString("labelRaportData", ci);
            //xrTableCell16.Text = rm.GetString("labelVlFatures", ci);
            //xrTableCell14.Text = rm.GetString("labelRaportMon", ci);
            //xrTableCell15.Text = rm.GetString("labelKursi", ci);
            //xrTableCell21.Text = rm.GetString("labelRaportVlefta", ci);
            //xrTableCell18.Text = rm.GetString("labelTransport", ci);
            //xrTableCell19.Text = rm.GetString("labelSiguracion", ci);
            //xrTableCell20.Text = rm.GetString("labelRefer", ci);
            //xrTableCell17.Text = rm.GetString("labelVlereDogane", ci);
            //xrTableCell22.Text = rm.GetString("labelTakseDoganore", ci);
            //xrTableCell24.Text = rm.GetString("labelAkciz", ci);
            //xrTableCell25.Text = rm.GetString("labelTetjera", ci);
            //xrTableCell23.Text = rm.GetString("labelRaportVlPaTVSH", ci);
            //xrTableCell3.Text = rm.GetString("labelTVSH", ci);
            //xrLabel50.Text = rm.GetString("labelLogoIMB", ci);
            //xrLabel8.Text = rm.GetString("FiltratEmertimi", ci);
        }
    }
}
