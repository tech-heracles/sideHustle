using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_FatureShitjeMeArtikujSelmaniA5_SePDeFn_131547025405951368 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_FatureShitjeMeArtikujSelmaniA5_SePDeFn_131547025405951368()
        {
            InitializeComponent();
        }
        public Rap_FatureShitjeMeArtikujSelmaniA5_SePDeFn_131547025405951368(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FatureShitjeMeArtikujSelmaniA5_SePDeFn_131547025405951368(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
            switch (ci.ToString())
            {
                case "sq-AL": //shqip
                    parameter1.Value = 0;
                    break;
                case "en_US": //anglisht
                    parameter1.Value = 1;
                    break;
                default: break;

            }
        }

    }
}
