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
    public partial class Rap_FleteInventarizimiMeMagazine : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FleteInventarizimiMeMagazine(){InitializeComponent();}


        string windowWidth = "";
        
        public Rap_FleteInventarizimiMeMagazine(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.IdRaporti, param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, param.IdGjuha, report)
        {

        }
        public Rap_FleteInventarizimiMeMagazine(int idRaporti, CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, int idGjuha, DevExpress.XtraReports.UI.XtraReport raport)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            InitializeComponent();
            EmrateLabelave(ci);
            xrLabel54.Text = raport.Parameters[0].Description;
            parameter1.Value = raport.Parameters[0].Value;
            xrLabel55.Text = raport.Parameters[1].Description;
            parameter2.Value = raport.Parameters[1].Value;
            xrLabel56.Text = raport.Parameters[2].Description;
            parameter3.Value = raport.Parameters[2].Value;
            xrLabel57.Text = raport.Parameters[3].Description;
            parameter4.Value = raport.Parameters[3].Value;
            xrLabel58.Text = raport.Parameters[4].Description;
            parameter5.Value = raport.Parameters[4].Value;
            xrLabel59.Text = raport.Parameters[5].Description;
            parameter6.Value = raport.Parameters[5].Value;
            xrLabel60.Text = raport.Parameters[6].Description;
            parameter7.Value = raport.Parameters[6].Value;
            xrLabel40.Text = raport.Parameters[13].Description;
            parameter8.Value = raport.Parameters[13].Value;
            xrLabel35.Text = raport.Parameters[14].Description;
            parameter9.Value = raport.Parameters[14].Value;
            xrLabel45.Text = raport.Parameters[7].Description;
            parameter10.Value = raport.Parameters[7].Value;
            xrLabel49.Text = raport.Parameters[10].Description;
            parameter11.Value = raport.Parameters[10].Value;
            degaAdminLabel.Text = raport.Parameters[11].Description;
            DegaAdministrative.Value = raport.Parameters[11].Value;
            if (Parameters[12].Description == "" || Parameters[12].Description == "Gjendja:")
                xrLabel50.Text = rm.GetString("filterGjendja", ci) + ":";
            else xrLabel50.Text = Parameters[12].Description;
            if (raport.Parameters[12].Value.ToString() == "1")
                xrLabel51.Text = rm.GetString("labelRaportArtikujMeGjendje", ci);
            else if (raport.Parameters[12].Value.ToString() == "2")
                xrLabel51.Text = rm.GetString("labelRaportArtikujMeGjendjeZero", ci);
            else
                xrLabel51.Text = rm.GetString("cmbboxItemFilterAvancTeGjithe", ci);
            windowWidth = Convert.ToString(raport.Parameters[9].Value);
          
        }
       

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
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
            xrLabel13.Text = rm.GetString("labelKartela", ci);
            xrLabel14.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrLabel15.Text = rm.GetString("labelNjesia", ci);
            xrLabel16.Text = rm.GetString("labelCmimi", ci);
            xrLabel17.Text = rm.GetString("labelRaportTedhenatKontabilitetit", ci);
            xrLabel19.Text = rm.GetString("labelSasia", ci);
            xrLabel20.Text = rm.GetString("labelKategoria", ci);
            xrLabel18.Text = rm.GetString("lblRaportVleftaLeke", ci);
            xrLabel23.Text = rm.GetString("lblRaportTedhenaInventar", ci);
            xrLabel25.Text = rm.GetString("labelSasia", ci);
            xrLabel26.Text = rm.GetString("labelKategoria", ci);
            //TotaliGjithMAgazinave.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel24.Text = rm.GetString("lblRaportVleftaLeke", ci);
            xrLabel29.Text = rm.GetString("lblRaportRezultatet", ci);
            xrLabel43.Text = rm.GetString("lblMungesat", ci);
            xrLabel30.Text = rm.GetString("lblRaportTepricat", ci);
            xrLabel42.Text = rm.GetString("labelSasia", ci);
            xrLabel44.Text = rm.GetString("labelSasia", ci);
            xrLabel74.Text = rm.GetString("lblRaportVerejtje", ci);

            xrLabel37.Text = rm.GetString("labelLogoIMB", ci);
            //xrLabel155.Text = rm.GetString("labelRaportGjendjaeMeparshme", ci);
        }

        private void Rap_FleteInventarizimiMeMagazine_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBox1.Image = AlphaWebReports.raporteUtil.MerrLogoNdermarrje(this.Extensions["ndermarrjeLogo"]);
        }
    }


}
