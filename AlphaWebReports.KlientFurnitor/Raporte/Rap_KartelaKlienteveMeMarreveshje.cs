using System;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.RaportetDs.KlientFurnitor.Raporte
{
    public partial class Rap_KartelaKlienteveMeMarreveshje : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_KartelaKlienteveMeMarreveshje(){InitializeComponent();} 

        public Rap_KartelaKlienteveMeMarreveshje(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.IdRaporti, param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_KartelaKlienteveMeMarreveshje(int idRaporti, CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            
        }



    }
    }

