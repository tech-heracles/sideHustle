using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Format_Preventiv_2 : DevExpress.XtraReports.UI.XtraReport
    {
		public Format_Preventiv_2(){InitializeComponent();} 

        public Format_Preventiv_2(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Format_Preventiv_2(System.Globalization.CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();


        }

    }
}



    

