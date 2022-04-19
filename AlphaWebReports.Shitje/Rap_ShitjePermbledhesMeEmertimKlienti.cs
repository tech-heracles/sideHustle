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
    public partial class Rap_ShitjePermbledhesMeEmertimKlienti : DevExpress.XtraReports.UI.XtraReport
    {        
		public Rap_ShitjePermbledhesMeEmertimKlienti(){InitializeComponent();} 
       
        public Rap_ShitjePermbledhesMeEmertimKlienti(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_ShitjePermbledhesMeEmertimKlienti(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
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
            parameter10.Value = raport.Parameters[9].Value;  
            parameter11.Value = raport.Parameters[10].Value;  
            parameter12.Value = raport.Parameters[12].Value;  
            parameter13.Value = raport.Parameters[13].Value;  
            parameter14.Value = raport.Parameters[14].Value;
            DegaAdministrative.Value = raport.Parameters[11].Value;
            parameter15.Value = raport.Parameters[15].Value;
            adresaFaturimit.Value = raport.Parameters[20].Value;
            parameter16.Value = raport.Parameters[16].Value;
        }
        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {

            ResourceManager rm = new ResourceManager("Resources.Strings",
                        System.Reflection.Assembly.Load("App_GlobalResources"));
           
            xrLabel17.Text = rm.GetString("RaportRegjistriPermbledhesShitjeveTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel31.Text = rm.GetString("labelNrRendor", ci);
            xrLabel45.Text = rm.GetString("labelRaportiLloji", ci);
            xrLabel27.Text = rm.GetString("labelDokumenti", ci);
            xrLabel29.Text = rm.GetString("labelMonedhaFature", ci);
            xrLabel33.Text = rm.GetString("labelMonedhaBaze", ci);
            xrLabel1.Text = rm.GetString("labelRaportiNr", ci);
            xrLabel4.Text = rm.GetString("labelRaportiDtDok", ci);
            xrLabel9.Text = rm.GetString("labelFilterAvancuarMonedha", ci);
            xrLabel5.Text = rm.GetString("labelKursi", ci);          
            xrLabel14.Text = rm.GetString("labelNentotal", ci);
            xrLabel22.Text = rm.GetString("labelZbritje", ci);
            xrLabel23.Text = rm.GetString("labelTVSH", ci);
            xrLabel24.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel35.Text = rm.GetString("labelTVSH", ci);
            xrLabel34.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel42.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel50.Text = rm.GetString("labelLogoIMB", ci);
        }      
    }
}
