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

namespace AlphaWebReports.RaportetDs.Blerje.Format_Printimi
    {


    public partial class Rap_FormatKerkeseBlerjeVodafone : DevExpress.XtraReports.UI.XtraReport
        {
        public Rap_FormatKerkeseBlerjeVodafone() { InitializeComponent(); }
        public Rap_FormatKerkeseBlerjeVodafone(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) :
          this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
            {

            }

        public Rap_FormatKerkeseBlerjeVodafone(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
            {
            InitializeComponent();
            //System.Data.DataTable dt = ((System.Data.DataSet)(this.DataSource)).Tables[0];
            //System.Data.DataView view = new System.Data.DataView(dt);
            //System.Data.DataTable aprovuesi = view.ToTable(true, dt.ToString());

        }

        private void detailBand1_BeforePrint(object sender, PrintEventArgs e)
        {
            if (GetCurrentColumnValue("RowNumber") != null && GetCurrentColumnValue("RowNumber").ToString() != "1")
                e.Cancel = true;
        }
    }
    }