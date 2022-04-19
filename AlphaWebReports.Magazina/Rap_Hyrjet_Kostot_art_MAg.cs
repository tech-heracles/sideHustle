using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.Magazina
{
    public partial class Rap_Hyrjet_Kostot_art_MAg : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_Hyrjet_Kostot_art_MAg(){InitializeComponent();} 
 

        int shifraPasPresjes = 0;
        public Rap_Hyrjet_Kostot_art_MAg(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_Hyrjet_Kostot_art_MAg(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            parameter1.Value = raport.Parameters[2].Value;
            parameter2.Value = raport.Parameters[3].Value;
            parameter3.Value = raport.Parameters[4].Value;
            parameter4.Value = raport.Parameters[5].Value;
            parameter5.Value = raport.Parameters[6].Value;
            parameter6.Value = raport.Parameters[7].Value;
            parameter7.Value = raport.Parameters[10].Value;
            parameter8.Value = raport.Parameters[11].Value;
            parameter9.Value = raport.Parameters[12].Value;
            parameter10.Value = raport.Parameters[13].Value;
            parameter11.Value = raport.Parameters[14].Value;
            parameter12.Value = raport.Parameters[15].Value;
            parameter14.Value = raport.Parameters[16].Value;
            parameter15.Value = raport.Parameters[19].Value;
            shifraPasPresjes = Convert.ToInt32(raport.Parameters[20].Value);
            parameter16.Value = shifraPasPresjes;
            caktoFormatinENumrave();
        }

        private void caktoFormatinENumrave()
        {
            xrTableCell3.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell4.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell5.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell8.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel86.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel70.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";

            xrLabel86.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";

            xrLabel70.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";

                xrTableCell8.XlsxFormatString =
                xrTableCell5.XlsxFormatString =
                xrTableCell4.XlsxFormatString =
                xrTableCell3.XlsxFormatString =
                xrLabel86.XlsxFormatString =
                xrLabel70.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
        }



        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>

        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                       System.Reflection.Assembly.Load("App_GlobalResources"));
            
            xrLabel89.Text = rm.GetString("lblRapHyrjetKosto", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrTableCell16.Text = rm.GetString("labelKartela", ci);
            xrTableCell17.Text = rm.GetString("labelFilterAvancuarKodbari", ci);
            xrTableCell18.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell19.Text = rm.GetString("labelNjesia", ci);
            xrTableCell28.Text = rm.GetString("labelRaportSasia", ci);
            xrTableCell29.Text = rm.GetString("labelCmimi", ci);
            xrTableCell26.Text = rm.GetString("cmbCmimeArtikulliVlere", ci);
            xrTableCell21.Text = rm.GetString("lblKostoMeShperndarje", ci);
            xrTableCell30.Text = rm.GetString("cmbCmimeArtikulliKosto", ci);
            xrTableCell27.Text = rm.GetString("cmbCmimeArtikulliVlere", ci);
            xrTableCell20.Text = rm.GetString("labelRaportHyrje", ci);
            xrLabel69.Text = rm.GetString("labelLogoIMB", ci);
            
        }
    }
}
