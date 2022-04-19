using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.RaportetDs.KlientFurnitor.Raporte
{
    public partial class Rap_SituacionKlient_EkspozimVjetersi : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_SituacionKlient_EkspozimVjetersi(){InitializeComponent();} 
        public Rap_SituacionKlient_EkspozimVjetersi(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, param.IdGjuha, param.ScopeID, report)
        {

        }
 
        public Rap_SituacionKlient_EkspozimVjetersi(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, int idGjuha, string scopeID , DevExpress.XtraReports.UI.XtraReport raport)
        {
            count = 0;
            InitializeComponent();
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            EmrateLabelave(ci);
            monedha.Value = raport.Parameters["filterMonedha"].Value;
            klient.Value = raport.Parameters["filterKlientFurnitor"].Value;
            dtDok.Value = raport.Parameters["filterDtDok"].Value;
            vlere.Value = raport.Parameters["filterVlereShitje"].Value;
            nderrmarje.Value = raport.Parameters["IdNdermarje"].Value;
            qyteti.Value = raport.Parameters["filterQyteti"].Value;
            nrLlogarie.Value = raport.Parameters["filterNrLlogaria"].Value;
            agjentShitje.Value = raport.Parameters["filterAgjentShitje"].Value;
            monedheKlientiFurnitor.Value = raport.Parameters["monedhaKF"].Value;
            if (monedheKlientiFurnitor.Value.ToString() == "False")
                xrLabel18.Text = rm.GetString("labelRaportJo", ci);
            else
                xrLabel18.Text = rm.GetString("labelRaportPo", ci);
            windowWidth = Convert.ToString(raport.Parameters[7].Value);
             
        }

        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel12.Text = rm.GetString("RaportSituacioniKlientitEkspozimTitulli", ci);
            xrLabel1.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel2.Text = rm.GetString("labelFooterNdermarrja", ci);
            xrLabel4.Text = rm.GetString("filterFurnitor", ci);
            xrLabel6.Text = rm.GetString("labelRaportVlera", ci);
            xrLabel19.Text = rm.GetString("labelFilterAvancuarMonedha", ci);
            xrLabel10.Text = rm.GetString("labelQyteti", ci);
            xrLabel11.Text = rm.GetString("labelRaportNrLlogarise", ci);
            xrLabel13.Text = rm.GetString("MenuItemAgjentetShitjes", ci);
            xrLabel14.Text = rm.GetString("labelFilterAvancuarMonedheKF", ci);
            xrLabel20.Text = rm.GetString("labelLogoIMB", ci);

            //header
            xrLabel22.Text = rm.GetString("labelRaportNrRendor", ci);
            xrLabel23.Text = rm.GetString("labelRaportGrupi", ci);
            xrLabel24.Text = rm.GetString("label_KODI", ci);
            xrLabel25.Text = rm.GetString("label_KODI", ci) + " 2";
            xrLabel26.Text = rm.GetString("labelRaportEmertimiKlientit", ci);
            xrLabel27.Text = rm.GetString("labelNrLlogarie", ci);
            xrLabel28.Text = rm.GetString("lblRaportDetyrimIMeparshem", ci);
            xrLabel29.Text = rm.GetString("labelRaportShumaDebi", ci);
            xrLabel30.Text = rm.GetString("labelRaportShumaKredi", ci);
            xrLabel31.Text = rm.GetString("labelRaportDetyrimi", ci);
            xrLabel32.Text = rm.GetString("labelRaportLimitiParalajmerues", ci);
            xrLabel33.Text = rm.GetString("labelRaportiDiferenca", ci);
            xrLabel34.Text = rm.GetString("labelRaportiShenja", ci);
            xrTableCell14.Text = rm.GetString("labelRaportiTotali", ci);

        }


       

        private void xrLabel35_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            count++;
            xrTableCell1.Text = count.ToString();
            var parametraRaporti = AlphaWebReports.raporteUtil.DeserializoParametraPerKonstruktorRaporti(this.Extensions["parametraRaporti"]);

            if (GetCurrentColumnValue("KODKLIENTFURNITOR") != null)
            {
                    xrTableCell1.NavigateUrl = "javascript:window.myFaqeCelje.hapFaqeNeTabTeRi('RaportiShpejte.aspx?scopeID=" + parametraRaporti.ScopeID + "&idraporti=41&filterFurnitor=" + GetCurrentColumnValue("KODKLIENTFURNITOR") + "&printo=0&Sesioni=true')";
                    xrTableCell1.Target = "_self";
            }
            
        }

        private int count;
        string windowWidth = "";

        private void Rap_SituacionIKlienteve_Ekspozim_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            count = 0;
        }

        private void xrTableCell13_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Color c = new Color();

            try
            {
                double limit = Convert.ToDouble(GetCurrentColumnValue("LIMITPARALAJMERUES"));
                double detyrimi = Convert.ToDouble(GetCurrentColumnValue("totalikrahasues"));
                if (limit == 0)
                    c = Color.White;

                else
                {
                    double value = (100 * (detyrimi - limit)) / limit;
                    if (value < 0)
                        c = Color.Green;
                    else if (value <= 10)
                        c = Color.Orange;
                    else
                        c = Color.Red;
                }
            }
            catch (FormatException ex)
            {
                c = Color.White;
            }
            xrTableCell13.ForeColor = c;
            xrTableCell13.BackColor = c;
            xrTableCell13.BorderColor = c;
            
        }

    }
}
