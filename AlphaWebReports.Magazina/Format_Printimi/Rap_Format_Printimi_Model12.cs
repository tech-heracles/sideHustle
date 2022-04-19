using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
using System.Collections.Generic;
using System.Data;
namespace AlphaWebReports.RaportetDs.Magazina.Format_Printimi
{
    public partial class Rap_Format_Printimi_Model12 : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_Format_Printimi_Model12(){InitializeComponent();} 
        public Rap_Format_Printimi_Model12(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_Format_Printimi_Model12(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();

        }

       


    }
}
