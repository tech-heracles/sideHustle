using DevExpress.XtraReports.UI;
using System;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.RaportetDs.QendraKosto.Raportet
{
    public partial class Rap_BilanciKomponentesKostos : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_BilanciKomponentesKostos(){InitializeComponent();} 
        private float vleftaMonQkShpenzime, vleftaMonQkTeArdhura, vleftaMonllogTeArdhura, vleftaMonBazeTeArdhura,
            vleftaMonllogShpenzime, vleftaMonBazeShpenzime,
            shumaVleraMonllogTeArdhura, shumaVleraMonllogShpenzime, shumaVleraMonQKTeArdhura, shumaVleraMonQKShpenzime, shumaVleraMonBazeShpenzime,
            shumaVleraMonBazeTeArdhura = 0;
        bool monedhaTeNdryshme = false;
        

        public Rap_BilanciKomponentesKostos(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, report)
        {

        }
        public Rap_BilanciKomponentesKostos(CultureInfo ci, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            parameter2.Value = raport.Parameters[1].Value;
            string sintetike = Convert.ToString(raport.Parameters["filterLlogariSintetike"].Value);
            if (sintetike == "Po")
                this.sintetike.Value = true;
            else
                this.sintetike.Value = false;
            EmrateLabelave(ci);
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            //  xrLabel13.Text = rm.GetString("RaportiQendraveTeKostosSipasLlogariveTitulli", ci);
            xrLabel32.Text = rm.GetString("FiltratEmertimi", ci);

            xrLabel11.Text = rm.GetString("labelNrLlogarie", ci);
            xrLabel22.Text = rm.GetString("labelKodQender", ci);
            xrLabel21.Text = rm.GetString("labelRaportiEmertimi", ci);

            // xrLabel17.Text = rm.GetString("labelVleraMonedheLlogari", ci);
            xrLabel20.Text = rm.GetString("labelRaportiDebi", ci);
            xrLabel19.Text = rm.GetString("filterMonedha", ci);
            // xrLabel18.Text = rm.GetString("labelFilterAvancuarGjendja", ci);

            // xrLabel16.Text = rm.GetString("labelVlereMonedheBaze", ci);
            xrLabel23.Text = rm.GetString("labelRaportiDebi", ci);
            xrLabel24.Text = rm.GetString("labelRaportiKredi", ci);
            //    xrLabel25.Text = rm.GetString("labelFilterAvancuarGjendja", ci);

            xrLabel40.Text = rm.GetString("lblRezultati", ci);
            xrLabel44.Text = rm.GetString("labelLogoIMB", ci);
        }

        private void Rap_BilanciKomponentesKostos_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBox1.Image = AlphaWebReports.raporteUtil.MerrLogoNdermarrje(this.Extensions["ndermarrjeLogo"]);
        }


        private void xrLabel29_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //if (this.GetCurrentColumnValue("Grupimi") != null)
            //{
            //    if (Convert.ToBoolean(this.sintetike.Value) | !monedhaTeNdryshme)
            //        if (this.GetCurrentColumnValue("Grupimi").ToString() == "Te ardhura")
            //        {
            //            //e.Result = shumaVleraMonllogTeArdhura;
            //            vleftaMonllogTeArdhura = shumaVleraMonllogTeArdhura;
            //        }

            //        else
            //        {
            //            //e.Result = shumaVleraMonllogShpenzime;
            //            vleftaMonllogShpenzime = shumaVleraMonllogShpenzime;
            //        }
            //    else e.Result = "";
            //}
            //else e.Result = "";
            //e.Handled = true;
        }

       

        private void xrLabel30_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //if (Convert.ToBoolean(this.sintetike.Value) | !monedhaTeNdryshme)
            //xrLabel30.Text = String.Format("{0:#,#.00}", (vleftaMonllogTeArdhura - vleftaMonllogShpenzime).ToString());
            //else xrLabel30.Text = "";
        }

        private void GroupHeader2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //shumaVleraMonllogTeArdhura = 0;
            //shumaVleraMonllogShpenzime = 0;
            //shumaVleraMonQKShpenzime = 0;
            //shumaVleraMonQKTeArdhura = 0;
        }

        private void xrLabel39_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //if (this.GetCurrentColumnValue("Grupimi") != null)
            //{
            //    //if (monedha != "E ndryshme")
            //    if (this.GetCurrentColumnValue("Grupimi").ToString() == "Te ardhura")
            //    {
            //        //e.Result = shumaVleraMonBazeTeArdhura;
            //        vleftaMonBazeTeArdhura = shumaVleraMonBazeTeArdhura;
            //    }

            //    else
            //    {
            //        //e.Result = shumaVleraMonBazeShpenzime;
            //        vleftaMonBazeShpenzime = shumaVleraMonBazeShpenzime;
            //    }
            //    //else e.Result = "";
            //}

            //else e.Result = "";

            //// xrLabel30.Text = (shumaVleraMonllog).ToString();
            //e.Handled = true;
        }

        private void xrLabel41_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //if (Convert.ToBoolean(this.sintetike.Value) | !monedhaTeNdryshme)
            //xrLabel41.Text = String.Format("{0:#,#.00}", (vleftaMonBazeTeArdhura - vleftaMonBazeShpenzime).ToString());
            //else xrLabel41.Text = "";
        }

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (this.PrintingSystem.Document.PageCount == 0)
            {
                e.Cancel = true;
                return;
            }
            
        }

        private void GroupHeader3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //vleftaMonBazeTeArdhura = 0;
            //vleftaMonBazeShpenzime = 0;
            //vleftaMonQkTeArdhura = 0;
            //vleftaMonQkShpenzime = 0;
            //vleftaMonllogTeArdhura = 0;
            //vleftaMonllogShpenzime = 0;
        }

        private void xrLabel1_SummaryRowChanged(object sender, EventArgs e)
        {
            //if (this.GetCurrentColumnValue("monLlogari") != null && this.GetCurrentColumnValue("monLlogari").ToString() != monedha)
            //    monedha = "E ndryshme";
            //else
            //{
            //if (this.GetCurrentColumnValue("Grupimi") != null)
            //{
            //    if (this.GetCurrentColumnValue("Grupimi").ToString() == "Te ardhura")
            //    {
            //        shumaVleraMonllogTeArdhura += float.Parse(this.GetCurrentColumnValue("krediMonLlogari").ToString()) - float.Parse(this.GetCurrentColumnValue("DebiMonLlogari").ToString());
            //    }
            //    else if (this.GetCurrentColumnValue("Grupimi").ToString() == "Shpenzime")
            //    {
            //        shumaVleraMonllogShpenzime += float.Parse(this.GetCurrentColumnValue("DebiMonLlogari").ToString()) - float.Parse(this.GetCurrentColumnValue("krediMonLlogari").ToString());
            //    }
            //}
            //}
        }

        private void xrLabel2_SummaryRowChanged(object sender, EventArgs e)
        {
            //if (this.GetCurrentColumnValue("monLlogari") != null && this.GetCurrentColumnValue("monLlogari").ToString() != monedha)
            //    monedha = "E ndryshme";
            //else
            //{
            //if (this.GetCurrentColumnValue("Grupimi") != null)
            //{
            //    if (this.GetCurrentColumnValue("Grupimi").ToString() == "Te ardhura")
            //    {
            //        shumaVleraMonQKTeArdhura += float.Parse(this.GetCurrentColumnValue("krediMonLlogari").ToString()) - float.Parse(this.GetCurrentColumnValue("DebiMonLlogari").ToString());
            //    }
            //    else if (this.GetCurrentColumnValue("Grupimi").ToString() == "Shpenzime")
            //    {
            //        shumaVleraMonQKShpenzime += float.Parse(this.GetCurrentColumnValue("DebiMonLlogari").ToString()) - float.Parse(this.GetCurrentColumnValue("krediMonLlogari").ToString());
            //    }
            //}
            // }
        }

        private void xrLabel3_SummaryRowChanged(object sender, EventArgs e)
        {
            //if (this.GetCurrentColumnValue("monLlogari") != null && this.GetCurrentColumnValue("monLlogari").ToString() != monedha)
            //    monedha = "E ndryshme";
            //else
            //{
            //if (this.GetCurrentColumnValue("Grupimi") != null)
            //{
            //    if (this.GetCurrentColumnValue("Grupimi").ToString() == "Te ardhura")
            //    {
            //        shumaVleraMonBazeTeArdhura += float.Parse(this.GetCurrentColumnValue("krediMonLlogari").ToString()) - float.Parse(this.GetCurrentColumnValue("DebiMonLlogari").ToString());
            //    }
            //    else if (this.GetCurrentColumnValue("Grupimi").ToString() == "Shpenzime")
            //    {
            //        shumaVleraMonBazeShpenzime += float.Parse(this.GetCurrentColumnValue("DebiMonLlogari").ToString()) - float.Parse(this.GetCurrentColumnValue("krediMonLlogari").ToString());
            //    }
            //}
            //}
        }

        private void ReportFooter_AfterPrint(object sender, EventArgs e)
        {
            //shumaVleraMonBazeTeArdhura = 0;
            //shumaVleraMonBazeShpenzime = 0;
        }
    }
}
