using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.Magazina
{
    public partial class Rap_Magazina_MaturimiPermbledhes_Stokut : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_Magazina_MaturimiPermbledhes_Stokut(){InitializeComponent();} 

        public Rap_Magazina_MaturimiPermbledhes_Stokut(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, report)
        {

        }
        public Rap_Magazina_MaturimiPermbledhes_Stokut(CultureInfo ci, XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            Ndermarja.Value = raport.Parameters[0].Value;
            DtDok.Value = raport.Parameters[1].Value;
            Magazina.Value = raport.Parameters[2].Value;
            Kartela.Value = raport.Parameters[3].Value;
            Grupim1.Value = raport.Parameters[10].Value;
            Grupim2.Value = raport.Parameters[11].Value;
            if (raport.Parameters["txtInterval3"].Value.ToString() != "")
              //  Int1.Value = int.Parse(raport.Parameters[6].Value.ToString());
                Int3.Value = Int64.Parse(raport.Parameters["txtInterval3"].Value.ToString());
            else Int3.Value = 0;
            // else Int1.Value = 0;
            if (raport.Parameters["txtInterval4"].Value.ToString() != "")
                //Int2.Value = int.Parse(raport.Parameters[7].Value.ToString());
                Int4.Value = Int64.Parse(raport.Parameters["txtInterval4"].Value.ToString());
            //else Int2.Value = 0;
              else Int4.Value = 0;
            if (raport.Parameters["txtInterval5"].Value.ToString() != "")
            //    Int3.Value = int.Parse(raport.Parameters[8].Value.ToString());
            //else Int3.Value = 0;
                Int5.Value = Int64.Parse(raport.Parameters["txtInterval5"].Value.ToString());
            else Int5.Value = 0;
            if (raport.Parameters["txtInterval6"].Value.ToString() != "")
                Int6.Value = Int64.Parse(raport.Parameters["txtInterval6"].Value.ToString());
            else Int6.Value = 0;
            //  Int4.Value = int.Parse(raport.Parameters[9].Value.ToString());
            //else Int4.Value = 0;
            if (raport.Parameters["txtInterval1"].Value.ToString() != "")
            //    Int5.Value = int.Parse(raport.Parameters[10].Value.ToString());
            //else Int5.Value = 0;
                Int1.Value = Int64.Parse(raport.Parameters["txtInterval1"].Value.ToString());
            else Int1.Value = 0;
            if (raport.Parameters["txtInterval2"].Value.ToString() != "")
            //    Int6.Value = int.Parse(raport.Parameters[11].Value.ToString());
            //else Int6.Value = 0;
                Int2.Value = Int64.Parse(raport.Parameters["txtInterval2"].Value.ToString());
            else Int2.Value = 0;
            FurnitorArtikulli.Value = raport.Parameters[12].Value;
            DegaAdministrative.Value = raport.Parameters[13].Value;
        }


        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                       System.Reflection.Assembly.Load("App_GlobalResources"));

            xrLabel12.Text = rm.GetString("RaportMaturimiPermbledhesStokutTitulli", ci);
            xrTableCell7.Text = rm.GetString("labelRaportiArtikull", ci);
            xrTableCell14.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell10.Text = rm.GetString("labelNjesia", ci);
            xrTableCell8.Text = rm.GetString("labelRaportSasiaSipasDiteqendrimit", ci);
            xrTableCell13.Text = rm.GetString("labelKodi", ci);
            xrLabel42.Text = rm.GetString("labelLogoIMB", ci);
            xrTableCell9.Text = rm.GetString("labelRaportMesatareDite", ci);

        }
    }
}
