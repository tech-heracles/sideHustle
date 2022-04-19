using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.ListPagesat.Raportet
{
    public partial class Rap_LibriPagave : DevExpress.XtraReports.UI.XtraReport
    {
        int formatNumri = 0;
        public Rap_LibriPagave()
        {
            InitializeComponent();
        }
        public Rap_LibriPagave(AlphaWebReports.Common.ParametraRaporti param, DevExpress.XtraReports.UI.XtraReport raport):
		this(param.Ci, param.IdNdermarrje,param.IdViti,param.IdPerdoruesi,raport){ }
        public Rap_LibriPagave(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {

            InitializeComponent();
            caktoFormatinENumrave();
          
        }
        private void caktoFormatinENumrave()
        {
            xrTableCell116.XlsxFormatString = xrTableCell128.XlsxFormatString = xrTableCell115.XlsxFormatString = xrTableCell117.XlsxFormatString = xrTableCell118.XlsxFormatString = xrTableCell119.XlsxFormatString = xrTableCell120.XlsxFormatString 
                = 0.ToString("N" + formatNumri);
        }

    }
}

