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
    public partial class Rap_FleteInventarizimi : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FleteInventarizimi(){InitializeComponent();} 
        
        public Rap_FleteInventarizimi(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.IdRaporti, param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, param.IdGjuha, report)
        {

        }



        public Rap_FleteInventarizimi(int idRaporti, CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, int idGjuha, DevExpress.XtraReports.UI.XtraReport raport)
        {

            InitializeComponent();
            EmrateLabelave(ci);
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
            xrTableCell20.Text = rm.GetString("labelKartela", ci);
            xrTableCell21.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell22.Text = rm.GetString("labelNjesia", ci);
            xrTableCell23.Text = rm.GetString("labelCmimi", ci);
            xrTableCell24.Text = rm.GetString("labelRaportTedhenatKontabilitetit", ci);
            xrTableCell35.Text = rm.GetString("labelSasia", ci);
            xrTableCell25.Text = rm.GetString("labelKategoria", ci);
            xrTableCell36.Text = rm.GetString("lblRaportVleftaLeke", ci);
            xrTableCell27.Text = rm.GetString("lblRaportTedhenaInventar", ci);
            xrTableCell14.Text = rm.GetString("labelSasia", ci);
            xrTableCell70.Text = rm.GetString("labelKategoria", ci);
            //TotaliGjithMAgazinave.Text = rm.GetString("labelRaportiTotali", ci);
            xrTableCell15.Text = rm.GetString("lblRaportVleftaLeke", ci);
            xrTableCell29.Text = rm.GetString("lblRaportRezultatet", ci);
            xrTableCell39.Text = rm.GetString("lblMungesat", ci);
            xrTableCell40.Text = rm.GetString("lblRaportTepricat", ci);
            xrTableCell85.Text = rm.GetString("labelSasia", ci);
            xrTableCell86.Text = rm.GetString("labelSasia", ci);
            xrTableCell30.Text = rm.GetString("lblRaportVerejtje", ci);

            xrLabel37.Text = rm.GetString("labelLogoIMB", ci);
            //xrLabel155.Text = rm.GetString("labelRaportGjendjaeMeparshme", ci);
        }
        
    }


}
