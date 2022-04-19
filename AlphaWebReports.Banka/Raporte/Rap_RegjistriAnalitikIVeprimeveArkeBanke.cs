using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.Banka.Raporte
{
    public partial class Rap_RegjistriAnalitikIVeprimeveArkeBanke : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_RegjistriAnalitikIVeprimeveArkeBanke()
        {
            InitializeComponent();
        }

        private CultureInfo ci;
        private ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

        public Rap_RegjistriAnalitikIVeprimeveArkeBanke(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) :
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }

        public Rap_RegjistriAnalitikIVeprimeveArkeBanke(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            this.ci = ci;
            InitializeComponent();
            EmrateLabelave(ci);
             parameter1.Value = raport.Parameters[0].Value;
            parameter2.Value = raport.Parameters[1].Value;
            parameter3.Value = raport.Parameters[2].Value;
            parameter4.Value = raport.Parameters[3].Value;
            parameter5.Value = raport.Parameters[4].Value;
            parameter6.Value = raport.Parameters[5].Value;
            parameter7.Value = raport.Parameters[6].Value;
            parameter8.Value = raport.Parameters[7].Value;
            parameter9.Value = raport.Parameters[8].Value;
            parameter10.Value = raport.Parameters[10].Value;
            parameter11.Value = raport.Parameters[11].Value;
            DegaAdministrative.Value = raport.Parameters[9].Value;
          
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>

        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel7.Text = rm.GetString("labelLogoIMB", ci);
            xrTableCell1.Text = rm.GetString("labelRaportiLloji", ci);
            xrTableCell2.Text = rm.GetString("labelRaportData", ci);
            xrTableCell3.Text = rm.GetString("labelNjesia", ci);
            xrTableCell4.Text = rm.GetString("filterDegeAdm", ci);
            xrTableCell5.Text = rm.GetString("labelFilterAvancuarMonedha", ci);
            xrTableCell6.Text = rm.GetString("labelRaportNumer", ci);


            xrTableCell7.Text = rm.GetString("labelKodi", ci);
            xrTableCell9.Text = rm.GetString("labelMonedhaBaze", ci);
            xrTableCell8.Text = rm.GetString("labelMonedhaVeprimit", ci);
            xrTableCell21.Text = rm.GetString("labelRaportVlArketuar", ci);
            xrTableCell22.Text = rm.GetString("labelRaportVlArketuar", ci);
            xrTableCell18.Text = rm.GetString("labelRaportVlPaguar", ci);
            xrTableCell19.Text = rm.GetString("labelRaportVlPaguar", ci);
            xrTableCell10.Text = rm.GetString("labelRaportiProgresivi", ci);

            xrTableCell29.Text = rm.GetString("labelRaportiTotali", ci) + ":";
            xrTableCell32.Text = rm.GetString("labelRaportiGjendMonBaze", ci);

        }
    }
}
