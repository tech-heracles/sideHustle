using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.RaportetDs.Blerje.Format_Printimi
{
    public partial class Rap_FatureBlerjeZodiac : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_FatureBlerjeZodiac() { InitializeComponent(); }
        public Rap_FatureBlerjeZodiac(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) :
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FatureBlerjeZodiac(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
            EmrateLabelave(ci);
        }


        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                       System.Reflection.Assembly.Load("App_GlobalResources"));
        }

    }
}
