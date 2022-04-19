using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class FormatDivel : DevExpress.XtraReports.UI.XtraReport
    {

        public FormatDivel() { InitializeComponent(); }
        public FormatDivel(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public FormatDivel(System.Globalization.CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();


        }
       

    }
}



    

