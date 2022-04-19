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
    public partial class Rap_FleteInventarizimi_MA : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FleteInventarizimi_MA(){InitializeComponent();} 
    
        int formatNumri = 0;
        string windowWidth = "";
        public Rap_FleteInventarizimi_MA(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.IdRaporti, param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, param.IdGjuha, report)
        {

        }
        public Rap_FleteInventarizimi_MA(int idRaporti, CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, int idGjuha, DevExpress.XtraReports.UI.XtraReport raport)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            InitializeComponent();
            EmrateLabelave(ci);
            parameter1.Value = raport.Parameters[0].Value;
            parameter2.Value = raport.Parameters[1].Value;
            parameter3.Value = raport.Parameters[2].Value;
            parameter4.Value = raport.Parameters[3].Value;
            parameter5.Value = raport.Parameters[4].Value;
            parameter6.Value = raport.Parameters[5].Value;
            parameter7.Value = raport.Parameters[6].Value;
            parameter8.Value = raport.Parameters[13].Value;
            parameter9.Value = raport.Parameters[14].Value;
            parameter10.Value = raport.Parameters[7].Value;
            parameter11.Value = raport.Parameters[10].Value;
            DegaAdministrative.Value = raport.Parameters[11].Value;
            windowWidth = Convert.ToString(raport.Parameters[9].Value);

        }
   

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel12.Text = rm.GetString("RaportFleteInventarizimiTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrTableCell42.Text = rm.GetString("labelKartela", ci);
            xrTableCell21.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell33.Text = rm.GetString("labelNjesia", ci);
            xrTableCell23.Text = rm.GetString("lblcmShitje", ci);
            xrTableCell24.Text = rm.GetString("labelRaportTedhenatKontabilitetit", ci);
            xrTableCell35.Text = rm.GetString("labelSasia", ci);
            xrTableCell46.Text = rm.GetString("labelKategoria", ci);
            xrTableCell36.Text = rm.GetString("lblRaportVleftaLeke", ci);
            xrTableCell27.Text = rm.GetString("lblRaportTedhenaInventar", ci);
            xrTableCell37.Text = rm.GetString("labelSasia", ci);
            xrTableCell48.Text = rm.GetString("labelKategoria", ci);
            //TotaliGjithMAgazinave.Text = rm.GetString("labelRaportiTotali", ci);
            xrTableCell38.Text = rm.GetString("lblRaportVleftaLeke", ci);
            xrTableCell29.Text = rm.GetString("lblRaportRezultatet", ci);
            xrTableCell39.Text = rm.GetString("lblMungesat", ci);
            xrTableCell40.Text = rm.GetString("lblRaportTepricat", ci);
            xrTableCell50.Text = rm.GetString("labelSasia", ci);
            xrTableCell51.Text = rm.GetString("labelSasia", ci);
            xrTableCell41.Text = rm.GetString("lblRaportVerejtje", ci);

            xrLabel37.Text = rm.GetString("labelLogoIMB", ci);
            //xrLabel155.Text = rm.GetString("labelRaportGjendjaeMeparshme", ci);
        }
        
    }


}
