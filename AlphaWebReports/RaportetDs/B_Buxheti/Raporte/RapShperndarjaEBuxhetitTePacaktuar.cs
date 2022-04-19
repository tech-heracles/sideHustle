using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.RaportetDs.B_Buxheti.Raporte
{
    public partial class RapShperndarjaEBuxhetitTePacaktuar : DevExpress.XtraReports.UI.XtraReport
    {
        decimal totali = 0;
        public RapShperndarjaEBuxhetitTePacaktuar()
        {
            InitializeComponent();
        }
        public RapShperndarjaEBuxhetitTePacaktuar(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) :
            this(param.Ci, report)
        {

        }
        public RapShperndarjaEBuxhetitTePacaktuar(CultureInfo ci, DevExpress.XtraReports.UI.XtraReport raport)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",System.Reflection.Assembly.Load("App_GlobalResources"));
            InitializeComponent();
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel2.Text = rm.GetString("RapShperndarjaEBuxhetitTePacaktuar", ci);
            xrTableCell5.Text = rm.GetString("lblRapArtikullBuxheti", ci);
            xrTableCell6.Text = rm.GetString("qenderShendetesore", ci);
            xrTableCell7.Text = rm.GetString("labelRaportVlera", ci);
            xrTableCell8.Text = rm.GetString("lblRaportiTotaliBankaveArkave", ci);
            totali = 0;

        }
    }
}
