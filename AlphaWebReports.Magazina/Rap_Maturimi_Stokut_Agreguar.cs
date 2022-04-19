using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Collections.Generic;

namespace AlphaWebReports.RaportetDs.Magazina
{
    public partial class Rap_Maturimi_Stokut_Agreguar : DevExpress.XtraReports.UI.XtraReport
    {
        bool hapurgjitha = false;
        public Rap_Maturimi_Stokut_Agreguar()
        {
            InitializeComponent();
        }
        public Rap_Maturimi_Stokut_Agreguar(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_Maturimi_Stokut_Agreguar(CultureInfo ci, int idNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            //  hapurgjitha = Convert.ToBoolean(raport.Parameters["Detajuar"].Value);

            }


        }
}
