using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace AlphaWebReports.RaportetDs.ListPagesat.Raportet
{
    public partial class Rap_KartelaPages : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_KartelaPages(){InitializeComponent();} 
        public Rap_KartelaPages(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje)
        {

        }
     
        public Rap_KartelaPages(System.Globalization.CultureInfo ci, int idNdermarrje)
        {
            InitializeComponent();
         
        }

      


        

    }
}
