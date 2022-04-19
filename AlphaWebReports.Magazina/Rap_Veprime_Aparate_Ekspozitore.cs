using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.Magazina
{
    public partial class Rap_Veprime_Aparate_Ekspozitore : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_Veprime_Aparate_Ekspozitore(){InitializeComponent();} 

        public Rap_Veprime_Aparate_Ekspozitore(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) :
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, param.IdGjuha, report)
        {

        }
        public Rap_Veprime_Aparate_Ekspozitore(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, int idGjuha, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();;
            parameter1.Value = raport.Parameters[2].Value;
            parameter2.Value = raport.Parameters[1].Value;
            parameter4.Value = raport.Parameters[3].Value;
            parameter5.Value = raport.Parameters[4].Value;
            parameter7.Value = raport.Parameters[6].Value;
            parameter8.Value = raport.Parameters[7].Value;
            parameter10.Value = raport.Parameters["filterKompania"].Value;
            parameterIdNderm.Value = idNdermarrje;
           
        }

    }
}
