using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.RaportetDs.B_Buxheti.Raporte
{
    public partial class RapRegjistriAnalitikIVeprimeveTeBuxhetit : DevExpress.XtraReports.UI.XtraReport
    {
        public RapRegjistriAnalitikIVeprimeveTeBuxhetit()
        {
            InitializeComponent();  
        }
        public RapRegjistriAnalitikIVeprimeveTeBuxhetit(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) :
            this(param.Ci, report)
        {

        }
        public RapRegjistriAnalitikIVeprimeveTeBuxhetit(CultureInfo ci, DevExpress.XtraReports.UI.XtraReport raport)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",System.Reflection.Assembly.Load("App_GlobalResources"));
            InitializeComponent();
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel2.Text = rm.GetString("RaportiRegjistriAnalitikVeprimeveTeBuxhetit", ci);
            xrTableCell5.Text = rm.GetString("labelNenkategoria", ci);
            xrTableCell6.Text = rm.GetString("labelRaportiLloji", ci);
            xrTableCell7.Text = rm.GetString("lblDateDokumenti", ci);
            xrTableCell8.Text = rm.GetString("labelZeri", ci);
            xrTableCell9.Text = rm.GetString("MenuItemBuxheti", ci);
            xrTableCell10.Text = rm.GetString("labelRaportSasia", ci);
            xrTableCell11.Text = rm.GetString("labelCmimi", ci);
            xrTableCell12.Text = rm.GetString("labelVleraPaTVSH", ci);
            xrTableCell13.Text = rm.GetString("lblRaportNrDok", ci);

        }
    }
}
