using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_KartelaAgjenteveShitjesFormat2_Dizajn2 : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_KartelaAgjenteveShitjesFormat2_Dizajn2(){InitializeComponent();} 
        public Rap_KartelaAgjenteveShitjesFormat2_Dizajn2(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_KartelaAgjenteveShitjesFormat2_Dizajn2(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            parameter1.Value = raport.Parameters[0].Value;
            parameter2.Value = raport.Parameters[1].Value;
            parameter3.Value = raport.Parameters[2].Value;
            parameter4.Value = raport.Parameters[3].Value;
            parameter5.Value = raport.Parameters[4].Value;
            parameter6.Value = raport.Parameters[5].Value;
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {

            ResourceManager rm = new ResourceManager("Resources.Strings",
                        System.Reflection.Assembly.Load("App_GlobalResources"));

            xrLabel17.Text = rm.GetString("RaportKartelaEAgjenteveTeShitjesTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrTableCell23.Text = rm.GetString("labelAgjenti", ci) + ":";
            xrTableCell24.Text = rm.GetString("labelNrRendor", ci);
            xrLabel3.Text = rm.GetString("labelDtRegjistrimi", ci);
            xrTableCell22.Text = rm.GetString("labelLlojDokumenti", ci);
            xrTableCell20.Text = rm.GetString("filterNrDokRezervime", ci);
            xrLabel4.Text = rm.GetString("labelVleraPaTVSH", ci);
            xrTableCell26.Text = rm.GetString("cmbItemBlerjeShitjeperqindje", ci);
            xrLabel6.Text = rm.GetString("labelVleraKomisionit", ci);
            xrLabel2.Text = rm.GetString("labelRaportiTotali", ci) + ":";
            xrLabel50.Text = rm.GetString("labelLogoIMB", ci);
            xrTableCell18.Text = rm.GetString("labelRaportiDtDok", ci);
            xrLabel18.Text = rm.GetString("labelVleraPaTVSH", ci);
            xrLabel29.Text = rm.GetString("labelVleraKomisionit", ci);
            xrTableCell14.Text = rm.GetString("labelKomisionPapaguar", ci);
            xrLabel9.Text = rm.GetString("labelKomisionPapaguar", ci);
            xrTableCell8.Text = rm.GetString("VleraPapaguarPaTVSHEmertim", ci);
            xrTableCell5.Text = rm.GetString("VleraPapaguarPaTVSHEmertim", ci);
            xrTableCell11.Text = rm.GetString("VleraPapaguarMeTVSHEmertim", ci);
            xrLabel30.Text = rm.GetString("VleraPapaguarMeTVSHEmertim", ci);
            xrTableCell18.Text = rm.GetString("labelRaportKlienti", ci);

            


        }
    }
}
