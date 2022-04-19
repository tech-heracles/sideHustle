using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
using System.Data;
using System.Drawing.Printing;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraPrinting;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{


    public partial class Rap_FormatKerkeseShitjeVodafone : DevExpress.XtraReports.UI.XtraReport
        {
        public Rap_FormatKerkeseShitjeVodafone() { InitializeComponent(); }
        public Rap_FormatKerkeseShitjeVodafone(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) :
          this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
            {

            }
        public Rap_FormatKerkeseShitjeVodafone(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
            {
            InitializeComponent();
           

        }
    }
    }