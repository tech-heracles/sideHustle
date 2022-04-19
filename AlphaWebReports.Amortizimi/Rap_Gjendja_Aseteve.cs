using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
using AlphaWebReports.Common;

namespace AlphaWebReports.RaportetDs.Amortizimi
{
    public partial class Rap_Gjendja_Aseteve : XtraReport
    {
        public Rap_Gjendja_Aseteve(ParametraRaporti param, XtraReport report) :
            this(param.Ci, param.IdNdermarrje, param.IdViti, report){ }
        public Rap_Gjendja_Aseteve() { InitializeComponent(); }
        public Rap_Gjendja_Aseteve(CultureInfo ci, int IdNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                           System.Reflection.Assembly.Load("App_GlobalResources"));
            InitializeComponent();
            EmrateLabelave(ci);
            parameter1.Value = raport.Parameters[9].Value;
            parameter2.Value = raport.Parameters[1].Value;
            parameter3.Value = raport.Parameters[2].Value;
            parameter4.Value = raport.Parameters[3].Value;
            parameterIdNderm.Value = IdNdermarrje;
            String grup1 = Convert.ToString(raport.Parameters[4].Value);
            parameter7.Value = raport.Parameters[7].Value;
            parameter6.Value = raport.Parameters["filterDtDok"].Value.ToString().Split('-')[1];
            parameter8.Value = raport.Parameters[12].Value;
            EmrateLabelave(ci);

        }
        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                       System.Reflection.Assembly.Load("App_GlobalResources"));

            xrLabel17.Text = rm.GetString("labelRaportGjendjaAseteveTitull", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrTableCell1.Text = rm.GetString("labelKodi", ci);
            xrTableCell2.Text = rm.GetString("labelRaportPershkrimi", ci);
            xrTableCell3.Text = rm.GetString("filterSeriali", ci);
            xrTableCell4.Text = rm.GetString("labelFilterAvancuarMagazina", ci);
            xrTableCell5.Text = rm.GetString("labelRaportPershkrimiMag", ci);
            xrTableCell6.Text = rm.GetString("labelRaportStatusMagazina", ci);
            xrTableCell7.Text = rm.GetString("labelRaportGrupi", ci);
            xrTableCell8.Text = rm.GetString("labelRaportDataBlerjes", ci);
            xrTableCell9.Text = rm.GetString("labelRaportDtFillimAmort", ci);
            xrTableCell10.Text = rm.GetString("labelRaportNorma", ci);
            xrTableCell11.Text = rm.GetString("labelRaportGjendjeFillestare", ci);
            xrTableCell12.Text = rm.GetString("labelRaportShtesaViti", ci);
            xrTableCell13.Text = rm.GetString("lblRaportTotalGjendje", ci);
            xrTableCell14.Text = rm.GetString("labelRaportAmortAkum", ci) + ":";
            xrTableCell15.Text = rm.GetString("labelRaportShtesaVitiAm", ci);
            xrTableCell17.Text = rm.GetString("labelRaportTotalAmort", ci);
            xrTableCell16.Text = rm.GetString("labelRaportVlMbeturFill", ci);
            xrTableCell18.Text = rm.GetString("labelRaportVlMbetur", ci);
            xrTableCell19.Text = rm.GetString("labelRaportDiteAmort", ci);
            xrTableCell20.Text = rm.GetString("labelRaportSerialiFillestar", ci);
            xrLabel71.Text = rm.GetString("labelLogoIMB", ci);
            xrTableCell21.Text = rm.GetString("labelRaportGrupiCategory", ci) + ":";
        }
    }
}
