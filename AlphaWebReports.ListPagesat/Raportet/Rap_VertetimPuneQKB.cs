using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;


namespace AlphaWebReports.RaportetDs.ListPagesat.Raportet
{
    public partial class Rap_VertetimPuneQKB : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_VertetimPuneQKB(){InitializeComponent();}
        public Rap_VertetimPuneQKB(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {
        }
        public Rap_VertetimPuneQKB(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
        }
        
        
    }
}
