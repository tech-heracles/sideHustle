using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.RaportetDs.Blerje
{
    public partial class Rap_LibriBlerjeveSipasMuajve : DevExpress.XtraReports.UI.XtraReport
    {
      
        public Rap_LibriBlerjeveSipasMuajve()    { InitializeComponent(); }

        public Rap_LibriBlerjeveSipasMuajve(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_LibriBlerjeveSipasMuajve(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi,
          DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
        }

        private void EmrateLabelave(CultureInfo ci)
        {

            ResourceManager rm = new ResourceManager("Resources.Strings",
                        System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel1.Text = rm.GetString("RaportLibriBlerjeveSipasMuajveTitulli", ci);

        }

    }
}
