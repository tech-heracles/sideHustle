using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.XtraCharts;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Resources;
using System.Reflection;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_GrafikuShitjeveSipasMuajve : XtraReport    
    {        
		public Rap_GrafikuShitjeveSipasMuajve(){InitializeComponent();} 
        ResourceManager rm = new ResourceManager("Resources.Strings",System.Reflection.Assembly.Load("App_GlobalResources"));
        CultureInfo ci;
        private int gjuha;
        public Rap_GrafikuShitjeveSipasMuajve(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.IdGjuha, param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_GrafikuShitjeveSipasMuajve( int idGjuha,CultureInfo ci, int idNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)
        {
            this.ci = ci;
    
            InitializeComponent();
            EmrateLabelave(ci);
            gjuha = idGjuha;
            parametergjuha.Value = idGjuha;
            Ndermarja.Value = raport.Parameters[0].Value;
            DtDok.Value = raport.Parameters["filterDtDok"].Value;
            KartelaArtikullit.Value = raport.Parameters[3].Value;
            Qyteti.Value = raport.Parameters[4].Value;
            Grupim1.Value = raport.Parameters[5].Value;
            Grupim2.Value = raport.Parameters[6].Value;
            LlojArtikulli.Value = raport.Parameters["filterLlojArtikulli"].Value;
            ShfaqVlera.Value = raport.Parameters["filterShfaqVlerat"].Value;
            PikeShitjeFurnizim.Value = raport.Parameters[11].Value;
            KlasaArtikullit.Value = raport.Parameters[12].Value;
            parameter1.Value = raport.Parameters[13].Value;
            parameter2.Value = raport.Parameters[14].Value;
            parameter3.Value = raport.Parameters[15].Value;
            parameter4.Value = raport.Parameters[17].Value;
            parameter5.Value = raport.Parameters[18].Value;
            parameter6.Value = raport.Parameters[19].Value;
            paramGjuha.Value = idGjuha;
            DegaAdministrative.Value = raport.Parameters[16].Value;
            
           
        }



        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            MarzhiShitjeveLabel.Text = rm.GetString("RaportGrafikShitjeSipasMuajve", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel4.Text = rm.GetString("RadioButtonListEditItemPeriudha", ci);
            xrLabel8.Text = rm.GetString("lblvleramertvsh", ci);
            xrLabel32.Text = rm.GetString("labelLogoIMB", ci);
        }        
    }
}
