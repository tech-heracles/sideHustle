using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.Banka.Raporte
{
    public partial class rap_MandatArketimiVodafone : DevExpress.XtraReports.UI.XtraReport
    {
		public rap_MandatArketimiVodafone(){InitializeComponent();} 
        private static string[] onesMapping =
         new string[] {
            "Zero", "Nje", "Dy", "Tre", "Kater", "Pese", "Gjashte", "Shtate", "Tete", "Nente",
            "Dhjete", "Njembedhjete", "Dymbedhjete", "Trembedhjete", "Katermbedhjete", "Pesembedhjete",
            "Gjashtembedhjete", "Shtatembedhjete", "Tetembedhjete", "Nentembedhjete"
        };
        private static string[] tensMapping =
            new string[] {
            "Njezet", "Tridhjete", "Dyzet", "Pesedhjete", "Gjashtedhjete", "Shtatedhjete", "Tetedhjete", "Nentedhjete"
        };
        private static string[] groupMapping =
            new string[] {
            "Qind", "Mije", "Milion", "Miliard", "Trilion"
        };
        private ResourceManager rm = new ResourceManager("Resources.Strings",
                       System.Reflection.Assembly.Load("App_GlobalResources"));
        private CultureInfo ci;

        public rap_MandatArketimiVodafone(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje)
        {

        }
        public rap_MandatArketimiVodafone(CultureInfo ci, int idNdermarrje)
        {
            this.ci = ci;
            InitializeComponent();
            EmrateLabelave(ci);
        }

        private void xrTableCell2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("LLOJIVEPRIMIT") != null)
            {
                if (GetCurrentColumnValue("LLOJIVEPRIMIT").ToString() == "Arketim")
                    xrTableCell2.Text = rm.GetString("labelRaportMAndatArketimi", ci);
                if (GetCurrentColumnValue("LLOJIVEPRIMIT").ToString() == "Pagese")
                    xrTableCell2.Text = rm.GetString("labelRaportMandatPagese", ci); 
                if (GetCurrentColumnValue("LLOJIVEPRIMIT").ToString() == "Derdhje")
                    xrTableCell2.Text = rm.GetString("labelRaportDerdhje", ci); 
                if (GetCurrentColumnValue("LLOJIVEPRIMIT").ToString() == "Terheqje")
                    xrTableCell2.Text = rm.GetString("labelRaportTerheqje", ci); 
            }
        }

        private void xrTableCell20_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("LLOJIVEPRIMIT") != null)
            {
                if (GetCurrentColumnValue("LLOJIVEPRIMIT").ToString() == "Arketim")
                {
                    xrTableCell20.Text = "";
                    xrTableCell20.Visible = false;
                }
                if (GetCurrentColumnValue("LLOJIVEPRIMIT").ToString() == "Pagese")
                    xrTableCell20.Text = rm.GetString("labelRaportMarresi", ci); 
                if (GetCurrentColumnValue("LLOJIVEPRIMIT").ToString() == "Derdhje")
                {
                    xrTableCell20.Text = "";
                    xrTableCell20.Visible = false;
                }
  if (GetCurrentColumnValue("LLOJIVEPRIMIT").ToString() == "Terheqje")
                {
                    xrTableCell20.Text = "";
                    xrTableCell20.Visible = false;
                }
            }
        }

        private void xrTableCell9_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
           
        }

        public String changeToWords(String numb)
        {
            String val = "", wholeNo = numb, points = "", andStr = "", pointStr = "";
            String endStr = ("");
            if (numb.Trim() == "")
                return "";
            try
            {
                int decimalPlace = numb.IndexOf(".");
                int idxPresjes = numb.IndexOf(",");
                if (decimalPlace > 0 && idxPresjes <= 0)
                {
                    wholeNo = numb.Substring(0, decimalPlace);
                    points = numb.Substring(decimalPlace + 1);
                    if (Convert.ToInt32(points) > 0)
                    {
                        andStr = ("Dhe");
                        pointStr = (" ") + EnglishFromNumber(int.Parse(points)) + (" Qindarka");
                    }
                }
                else if (idxPresjes > 0 && decimalPlace <= 0)
                {
                    string[] pjeset = numb.Split(',');
                    string numri = "";
                    for (int i = 0; i < pjeset.Length; i++)
                        numri += pjeset[i];
                    wholeNo = numri;
                }
                else if (idxPresjes > 0 && decimalPlace > 0)
                {
                    string[] pjeset = numb.Split(',');
                    string numri = "";
                    for (int i = 0; i < pjeset.Length; i++)
                        numri += pjeset[i];
                    numb = numri;
                    decimalPlace = numb.IndexOf(".");
                    wholeNo = numb.Substring(0, decimalPlace);
                    points = numb.Substring(decimalPlace + 1);
                    if (Convert.ToInt32(points) > 0)
                    {
                        andStr = ("Dhe");
                        pointStr = (" ") + EnglishFromNumber(int.Parse(points)) + (" Qindarka");
                    }
                }
                val = String.Format("{0} {1}{2} {3}", EnglishFromNumber(int.Parse(wholeNo)).Trim(), andStr, pointStr, endStr);
            }
            catch { ;}

            string fundi = val.Substring(val.Length - 4, 3);
            if (fundi == " E ")
                val = val.Substring(0, val.Length - 4);

            return val;
        }

        public static string EnglishFromNumber(long number)
        {
            if (number == 0)
            {
                return onesMapping[number];
            }

            string retVal = null;
            int group = 0;
            while (number > 0)
            {
                int numberToProcess = (int)(number % 1000);
                number = number / 1000;

                string groupDescription = ProcessGroup(numberToProcess);
                if (groupDescription != null)
                {
                    if (group > 0)
                    {
                        retVal = groupMapping[group] + " E " + retVal;
                    }
                    retVal = groupDescription + " " + retVal;
                }

                group++;
            }

            return /*sign + */" " + retVal;
        }

        private static string ProcessGroup(int number)
        {
            int tens = number % 100;
            int hundreds = number / 100;

            string retVal = null;
            if (hundreds > 0)
            {
                retVal = onesMapping[hundreds] + " " + groupMapping[0];
            }
            if (tens > 0)
            {
                if (tens < 20)
                {
                    retVal += ((retVal != null) ? " E " : "") + onesMapping[tens];
                }
                else
                {
                    int ones = tens % 10;
                    tens = (tens / 10) - 2; // 20's offset

                    retVal += ((retVal != null) ? "  E " : "") + tensMapping[tens];

                    if (ones > 0)
                    {
                        retVal += ((retVal != null) ? " E " : "") + onesMapping[ones];
                    }
                }
            }

            return retVal;
        }

        private void xrTableCell12_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("VLERA") != null)
            {
                string vlera = String.Format("{0:#,#.00}", GetCurrentColumnValue("VLERA"));
                if (Convert.ToDouble(vlera) > 1)
                    xrTableCell12.Text = changeToWords(vlera);
            }
        }

        private void xrTableCell26_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("LLOJIVEPRIMIT") != null)
            {
                if (GetCurrentColumnValue("LLOJIVEPRIMIT").ToString() == "Arketim")
                    xrTableCell26.Text = "";
                if (GetCurrentColumnValue("LLOJIVEPRIMIT").ToString() == "Pagese")
                    xrTableCell26.Text = "___________________";
                if (GetCurrentColumnValue("LLOJIVEPRIMIT").ToString() == "Derdhje")
                    xrTableCell26.Text = "";
                if (GetCurrentColumnValue("LLOJIVEPRIMIT").ToString() == "Terheqje")
                    xrTableCell26.Text = "";
            }
        }


        private void xrTableCell38_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
            {
            xrTableCell38.Text = GetCurrentColumnValue("NDERMARJEKODI").ToString();
            }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
          
            xrTableCell35.Text = rm.GetString("labelRaportiNr", ci);
            xrTableCell36.Text = rm.GetString("labelRaportData", ci);
            xrTableCell7.Text = rm.GetString("labelRaportiNr", ci);
            xrTableCell41.Text = rm.GetString("labelRaportArketoniNga", ci);
            xrTableCell37.Text = rm.GetString("labelRaportLeke", ci) ;
            xrTableCell34.Text = rm.GetString("labelRaportPer", ci) ;
           // xrTableCell15.Text = rm.GetString("labelRaportiShuma", ci) + ":";
            xrTableCell19.Text = rm.GetString("labelRaportFinancieri", ci);
            xrTableCell23.Text = rm.GetString("labelRaportArketari", ci);
            xrTableCell30.Text = rm.GetString("labelRaportProdhuarNgaIMB", ci);
            xrTableCell31.Text = rm.GetString("filterRaportIdNdermarje", ci);
            xrTableCell39.Text = rm.GetString("labelRaportBarcode", ci);
            xrTableCell32.Text = rm.GetString("labelRaportPrintuarMe", ci);
            xrTableCell9.Text = rm.GetString("labelRaportiShuma", ci) ;
          
           
        }

        private void xrTableCell42_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //if (GetCurrentColumnValue("LLOJIVEPRIMIT") != null)
            //{
            //    if (GetCurrentColumnValue("LLOJIVEPRIMIT").ToString() == "Arketim")
            //        xrTableCell9.Text = rm.GetString("labelRaportArketoniNga", ci) + ":";
            //    if (GetCurrentColumnValue("LLOJIVEPRIMIT").ToString() == "Pagese")
            //        xrTableCell9.Text = rm.GetString("labelRAportUPaguaPer", ci) + ":";
            //    if (GetCurrentColumnValue("LLOJIVEPRIMIT").ToString() == "Derdhje")
            //        xrTableCell9.Text = rm.GetString("labelRaportUrdherXhirimKlienti", ci);
            //    if (GetCurrentColumnValue("LLOJIVEPRIMIT").ToString() == "Terheqje")
            //        xrTableCell9.Text = rm.GetString("labelRaportUrdherXhirimYne", ci);
            //}
        }

        private void xrTableCell23_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("LLOJIVEPRIMIT") != null)
            {
                if (GetCurrentColumnValue("LLOJIVEPRIMIT").ToString() == "Arketim")
                    xrTableCell20.Text = rm.GetString("labelRaportKlienti", ci);
                if (GetCurrentColumnValue("LLOJIVEPRIMIT").ToString() == "Pagese")
                    xrTableCell20.Text = rm.GetString("labelRaportArketari", ci);
                if (GetCurrentColumnValue("LLOJIVEPRIMIT").ToString() == "Derdhje")
                    xrTableCell20.Text = "";
                if (GetCurrentColumnValue("LLOJIVEPRIMIT").ToString() == "Terheqje")
                    xrTableCell20.Text = "";
            }
        }

        private void xrTableCell1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
        }

        private void xrTableCell41_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("LLOJIVEPRIMIT") != null)
            {
                if (GetCurrentColumnValue("LLOJIVEPRIMIT").ToString() == "Arketim")
                    xrTableCell20.Text = rm.GetString("labelRaportArketoniNga", ci);
                if (GetCurrentColumnValue("LLOJIVEPRIMIT").ToString() == "Pagese")
                    xrTableCell20.Text = rm.GetString("labelRaportPaguani", ci);
                if (GetCurrentColumnValue("LLOJIVEPRIMIT").ToString() == "Derdhje")
                    xrTableCell20.Text = "";
                if (GetCurrentColumnValue("LLOJIVEPRIMIT").ToString() == "Terheqje")
                    xrTableCell20.Text = "";
            }
        }
    }
}
