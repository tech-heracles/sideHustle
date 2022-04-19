using System;
using System.Drawing;
using System.Globalization;
using System.Resources;
using DevExpress.XtraReports.UI;


namespace AlphaWebReports.RaportetDs.Raporte_Menaxheriale
{
    public partial class Rap_LogPunonjes : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_LogPunonjes(){InitializeComponent();} 
        DateTime dataRegj;
        public Rap_LogPunonjes(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_LogPunonjes(CultureInfo ci, int idNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            xrLabel3.Text = raport.Parameters[0].Description;
            parameter1.Value = raport.Parameters[0].Value;
            xrLabel7.Text = raport.Parameters[1].Description;
            parameter2.Value = raport.Parameters[1].Value;
            xrLabel5.Text = raport.Parameters[2].Description;
            parameter3.Value = raport.Parameters[2].Value;
            xrLabel9.Text = raport.Parameters[3].Description;
            parameter4.Value = raport.Parameters[3].Value;
        }
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel2.Text = rm.GetString("FiltratEmertimi", ci);
            xrTableCell1.Text = rm.GetString("labelDataVeprimit", ci);
            xrTableCell2.Text = rm.GetString("lblRaportOra", ci);
            xrTableCell3.Text = rm.GetString("perdoruesTab", ci);
            xrTableCell4.Text = rm.GetString("lblraportLlojveprimi", ci);
            xrTableCell5.Text = rm.GetString("labelKodi", ci);
            xrTableCell6.Text = rm.GetString("lblRaportEmriPunonjes", ci);
            xrTableCell7.Text = rm.GetString("lblRaportObjekti", ci);
            xrLabel1.Text = rm.GetString("lblRaportLogetPunonjesve", ci);
        }

        private void xrTableCell8_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("dtveprimi") != null)
            {
                dataRegj = Convert.ToDateTime(GetCurrentColumnValue("dtveprimi").ToString());
                xrTableCell8.Text = dataRegj.ToString("dd.MM.yyyy");
            }
            
        }

        private void xrTableCell9_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("dtveprimi") != null)
            {
                dataRegj = Convert.ToDateTime(GetCurrentColumnValue("dtveprimi").ToString());
                xrTableCell9.Text = dataRegj.ToString("hh:mm:ss");
            }
        }

        private void xrTableCell11_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("LlojVeprimi") != null)
            {
                if (GetCurrentColumnValue("LlojVeprimi").ToString() != "")
                {

                    switch (GetCurrentColumnValue("LlojVeprimi").ToString())
                    {
                        case "Regjistrim": xrTableCell11.ForeColor = Color.Green;
                            break;
                        case "Modifikim": xrTableCell11.ForeColor = Color.Blue;
                            break;
                        case "Fshirje": xrTableCell11.ForeColor = Color.Red;
                            break;
                        default: xrTableCell11.ForeColor = Color.Black;
                            break;


                    }
                }
            }
        }

    }
}
