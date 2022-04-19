using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.Prodhimi.Raportet
{
    public partial class Rap_PlanifikimAnalitik : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_PlanifikimAnalitik(){InitializeComponent();} 
     

        
        public Rap_PlanifikimAnalitik(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.IdGjuha, param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, param.ScopeID, report)
        {

        }
        public Rap_PlanifikimAnalitik(int idGjuha, CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, string scopeID,  DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            xrLabel52.Text = raport.Parameters[0].Description;
            parameter1.Value = raport.Parameters[0].Value;
            xrLabel57.Text = raport.Parameters[1].Description;
            parameter2.Value = raport.Parameters[1].Value;
            xrLabel63.Text = raport.Parameters[2].Description;
            parameter3.Value = raport.Parameters[2].Value;
            xrLabel55.Text = raport.Parameters[3].Description;
            parameter4.Value = raport.Parameters[3].Value;
            xrLabel60.Text = raport.Parameters[4].Description;
            parameter5.Value = raport.Parameters[4].Value;
            xrLabel56.Text = raport.Parameters[5].Description;
            switch (idGjuha)
            {
                case 0:
                    if (raport.Parameters[5].Value.ToString() == "1")
                        parameter6.Value = "Te pa ekzekutuar";
                    else if (raport.Parameters[5].Value.ToString() == "0")
                        parameter6.Value = "Te ekzekutuar";
                    else parameter6.Value = "Te gjitha";
                    break;
                case 1:
                      if (raport.Parameters[5].Value.ToString() == "1")
                          parameter6.Value = "Unexecuted";
                    else if (raport.Parameters[5].Value.ToString() == "0")
                          parameter6.Value = "Executed";
                      else parameter6.Value = "All";
                    break;

            }
            xrLabel64.Text = raport.Parameters[6].Description;
            parameter7.Value = raport.Parameters[6].Value;
            xrLabel46.Text = raport.Parameters[7].Description;
            parameter8.Value = raport.Parameters[7].Value;
            xrLabel48.Text = raport.Parameters[8].Description;
            parameter9.Value = raport.Parameters[8].Value;
            xrLabel19.Text = raport.Parameters[9].Description;
            parameter11.Value = raport.Parameters[9].Value;
            xrLabel17.Text = raport.Parameters[10].Description;
            parameter12.Value = raport.Parameters[10].Value;
            xrLabel20.Text = raport.Parameters[11].Description;
            parameter13.Value = raport.Parameters[11].Value;

        }

        private void xrLabel3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var parametraRaporti = AlphaWebReports.raporteUtil.DeserializoParametraPerKonstruktorRaporti(this.Extensions["parametraRaporti"]);
            xrLabel3.NavigateUrl = "javascript:window.myFaqeCelje.hapFaqeNeTabTeRi('Shto_Planifikim.aspx?scopeID=" + parametraRaporti.ScopeID + "&newScopeId=True&shtim_modifikim=modifikim&id=" + GetCurrentColumnValue("IDKOKAPLANIFIKIM") + "&numer=" + GetCurrentColumnValue("NRDOK") + "')";
                this.xrLabel3.ForeColor = System.Drawing.Color.SteelBlue;
                xrLabel3.Target = "_self";
            
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
              ResourceManager rm = new ResourceManager("Resources.Strings",
                         System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel12.Text = rm.GetString("RaportRegjistriAnalitikPlanifikimeveTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel36.Text = rm.GetString("labelRaportiNrDok", ci);
            xrLabel37.Text = rm.GetString("labelRaportiDtDok", ci);
            xrLabel38.Text = rm.GetString("labelDtRegj", ci);
            xrLabel42.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrLabel39.Text = rm.GetString("labelRaportiShenime", ci);
            xrLabel13.Text = rm.GetString("labelFilterAvancuarStatusi", ci);
            xrLabel43.Text = rm.GetString("labelFilterAvancuarMagazina", ci);
            xrLabel40.Text = rm.GetString("labelKartela", ci);
            xrLabel1.Text = rm.GetString("labelNjesia", ci);
            xrLabel2.Text = rm.GetString("labelSasia", ci);
            xrLabel24.Text = rm.GetString("comboItemBlerjeShitjeKlient", ci);
            xrLabel23.Text = rm.GetString("lblRaportDtAfat", ci); 
            xrLabel16.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel35.Text = rm.GetString("labelLogoIMB", ci);
        }

        private void Rap_PlanifikimAnalitik_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBox1.Image = AlphaWebReports.raporteUtil.MerrLogoNdermarrje(this.Extensions["ndermarrjeLogo"]);
        }
    }
}
