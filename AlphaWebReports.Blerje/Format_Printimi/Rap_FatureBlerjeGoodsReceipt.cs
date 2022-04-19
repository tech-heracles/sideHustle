using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
using AlphaWebReports.Common;

namespace AlphaWebReports.RaportetDs.Blerje.Format_Printimi
{
    public partial class Rap_FatureBlerjeGoodsReceipt : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FatureBlerjeGoodsReceipt(){InitializeComponent();} 
        public Rap_FatureBlerjeGoodsReceipt(ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FatureBlerjeGoodsReceipt(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
          
        }
      
        

    }
}
