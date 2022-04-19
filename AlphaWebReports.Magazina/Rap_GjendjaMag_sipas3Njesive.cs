using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
using System.Collections.Generic;
namespace AlphaWebReports.RaportetDs.Magazina
{
    public partial class Rap_GjendjaMag_sipas3Njesive : DevExpress.XtraReports.UI.XtraReport
    {           
		public Rap_GjendjaMag_sipas3Njesive(){InitializeComponent();} 
        string windowWidth = "";
        int shifraPasPresjes = 0;
       
        public Rap_GjendjaMag_sipas3Njesive(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, param.IdGjuha, report)
        {

        }
        public Rap_GjendjaMag_sipas3Njesive(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, int idGjuha, DevExpress.XtraReports.UI.XtraReport raport)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            
            InitializeComponent();
            EmrateLabelave(ci);
            parameter1.Value = raport.Parameters["IdNdermarje"].Value;
            parameter2.Value = raport.Parameters["filterDtDok"].Value;
            parameter3.Value = raport.Parameters["filterDtRegj"].Value;
            parameter4.Value = raport.Parameters["filterKartela"].Value;
            parameter5.Value = raport.Parameters["filterFurnitorArt"].Value;
            parameter7.Value = raport.Parameters["filterkodifikimartP"].Value; 
            parameter6.Value = raport.Parameters["filterkodifikimartD"].Value;
            parameter9.Value = raport.Parameters["filterLlojArtikulli"].Value;
            parameter11.Value = raport.Parameters["filterMagazina"].Value;
            windowWidth = Convert.ToString(raport.Parameters[6].Value);
            shifraPasPresjes = Convert.ToInt32(raport.Parameters["filterFormatNumri"].Value);
            caktoFormatinENumrave();


        }
        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel17.Text = rm.GetString("RapGjendjaMag_3njesi", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
        }

        private void caktoFormatinENumrave()
        {
            xrTableCell89.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell90.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell4.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";

            xrTableCell91.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell8.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell7.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";

            xrTableCell6.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell5.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell11.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";

            xrTableCell9.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell10.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
           xrTableCell12.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";

            xrTableCell15.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell14.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell13.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";


            xrTableCell16.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell20.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell19.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";

            xrTableCell21.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell22.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell23.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";

            xrTableCell18.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell17.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";

            xrTableCell27.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell28.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell29.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell30.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell31.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell32.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell33.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell34.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell35.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell36.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell37.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell38.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell39.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell40.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell41.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell42.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell43.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell44.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell45.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell46.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell47.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
          //  xrTableCell48.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell49.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";

            xrTableCell27.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell28.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell29.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell30.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell31.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell32.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell33.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell34.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell35.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell36.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell37.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell38.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell39.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell40.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell41.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell42.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell43.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell44.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell45.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell46.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell47.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
         //   xrTableCell48.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell49.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";


            xrTableCell89.XlsxFormatString =
                xrTableCell90.XlsxFormatString =
                xrTableCell4.XlsxFormatString
                = xrTableCell91.XlsxFormatString
                = xrTableCell8.XlsxFormatString =
                xrTableCell7.XlsxFormatString =
                xrTableCell6.XlsxFormatString =
                xrTableCell5.XlsxFormatString =
                xrTableCell11.XlsxFormatString =
                xrTableCell9.XlsxFormatString =
                xrTableCell10.XlsxFormatString =
                xrTableCell15.XlsxFormatString =
                xrTableCell14.XlsxFormatString =
                xrTableCell13.XlsxFormatString =
                xrTableCell16.XlsxFormatString =
                xrTableCell20.XlsxFormatString =
                xrTableCell19.XlsxFormatString =
                xrTableCell21.XlsxFormatString =
                xrTableCell22.XlsxFormatString=
                 xrTableCell23.XlsxFormatString =
                xrTableCell18.XlsxFormatString=
                 xrTableCell17.XlsxFormatString=
                  xrTableCell27.XlsxFormatString = 
            xrTableCell28.XlsxFormatString = 
            xrTableCell29.XlsxFormatString=
            xrTableCell30.XlsxFormatString=
            xrTableCell31.XlsxFormatString=
            xrTableCell32.XlsxFormatString=
            xrTableCell33.XlsxFormatString=
            xrTableCell34.XlsxFormatString=
            xrTableCell35.XlsxFormatString=
            xrTableCell36.XlsxFormatString=
            xrTableCell37.XlsxFormatString=
            xrTableCell38.XlsxFormatString=
            xrTableCell39.XlsxFormatString=
            xrTableCell40.XlsxFormatString=
            xrTableCell41.XlsxFormatString=
            xrTableCell42.XlsxFormatString=
            xrTableCell43.XlsxFormatString=
            xrTableCell44.XlsxFormatString=
            xrTableCell45.XlsxFormatString=
            xrTableCell46.XlsxFormatString=
            xrTableCell47.XlsxFormatString=
          //  xrTableCell48.XlsxFormatString=
            xrTableCell49.XlsxFormatString

                = 0.ToString("N" + shifraPasPresjes);
        }
    }
}
