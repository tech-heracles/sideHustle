using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;

namespace AlphaWebReports.RaportetDs.KlientFurnitor.Raporte
{
    public partial class Rap_Kartolina_ditelindjes_klientit : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_Kartolina_ditelindjes_klientit(){InitializeComponent();} 
        public Rap_Kartolina_ditelindjes_klientit(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje)
        {

        }

        public Rap_Kartolina_ditelindjes_klientit(CultureInfo ci, int idNdermarrje)
        {
            InitializeComponent();
          
        }

      

     
    }
}
