using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using AlphaWebReports.Common;

namespace AlphaWebReports.RaportetDs.Arka.Raporte
{
    public partial class RAP_ARKA_ARKETIMETDITORE : DevExpress.XtraReports.UI.XtraReport
    {
		public RAP_ARKA_ARKETIMETDITORE(){InitializeComponent();} 
      

        public RAP_ARKA_ARKETIMETDITORE(ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje,param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public RAP_ARKA_ARKETIMETDITORE(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            parameter1.Value = raport.Parameters["IdNdermarje"].Value;
            parameter2.Value = raport.Parameters["filterArkaBankaEmer"].Value;
            parameter4.Value = raport.Parameters["filterDtDok"].Value;

        }    
      
    }
}
