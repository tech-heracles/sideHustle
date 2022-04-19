using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_OferteShitjeAlbas : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_OferteShitjeAlbas(){InitializeComponent();} 

        public Rap_OferteShitjeAlbas(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_OferteShitjeAlbas(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();

           
        }

    }
}
