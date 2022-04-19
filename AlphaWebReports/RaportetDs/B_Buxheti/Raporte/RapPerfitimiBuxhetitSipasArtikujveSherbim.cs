using System;
using System.Collections;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Data;
using System.Resources;

namespace AlphaWebReports.RaportetDs.B_Buxheti.Raporte
{
    public partial class RapPerfitimiBuxhetitSipasArtikujveSherbim : XtraReport
    {
        
        public RapPerfitimiBuxhetitSipasArtikujveSherbim()
        {
            InitializeComponent();
        }

        public RapPerfitimiBuxhetitSipasArtikujveSherbim(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) :
            this(param.Ci, report)
        {

        }

        public RapPerfitimiBuxhetitSipasArtikujveSherbim(CultureInfo ci, XtraReport raport)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            InitializeComponent();

            xrLabel13.Text = rm.GetString("lblRapPerfitimiBuxheteveSipasArtikujveSherbim", ci);
            xrTableCell8.Text = rm.GetString("lblRapBuxhetimiAnaliza", ci);
            xrTableCell9.Text = rm.GetString("labelEmertimi", ci);
            xrTableCell10.Text = rm.GetString("labelRaportSasia", ci);
            xrTableCell5.Text = rm.GetString("labelCmimi", ci);
            xrTableCell6.Text = rm.GetString("labelVleraPaTVSH", ci);
        }        
    }
}

   
      
