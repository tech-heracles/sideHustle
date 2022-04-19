using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace AlphaWebReports.RaportetDs.ListPagesat.Raportet
{
    public partial class Rap_kontrata_punes : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_kontrata_punes(){InitializeComponent();} 
       
        private int nr=0;
        private static string[] onesMapping =
          new string[] {
            "Zero", "Një", "Dy", "Tre", "Katër", "Pesë", "Gjashtë", "Shtatë", "Tetë", "Nëntë",
            "Dhjetë", "Njëmbëdhjetë", "Dymbëdhjetë", "Trembëdhjetë", "Katërmbëdhjetë", "Pesëmbëdhjetë",
            "Gjashtëmbëdhjetë", "Shtatëmbëdhjetë", "Tetëmbëdhjetë", "Nëntëmbëdhjetë"
         };
        private static string[] tensMapping =
            new string[] {
            "Njëzet", "Tridhjetë", "Dyzet", "Pesëdhjetë", "Gjashtëdhjetë", "Shtatëdhjetë", "Tetëdhjetë", "Nëntëdhjetë"
        };
        private static string[] groupMapping =
            new string[] {
            "Qind", "Mijë", "Milion", "Miliard", "Trilion"
        };

        private static string dateKontrate;
        public Rap_kontrata_punes(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, report)
        {

        }
        public Rap_kontrata_punes(System.Globalization.CultureInfo ci, int idNdermarrje, XtraReport raport)
        {
            InitializeComponent();

            dateKontrate = String.Format("{0:d/M/yyyy}", raport.Parameters["filterDateKontrate"].Value.ToString());
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
                retVal = onesMapping[hundreds] + groupMapping[0];
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
            catch {; }
            if (val.Length >= 4)
            {
                string fundi = val.Substring(val.Length - 4, 3);
                if (fundi == " E ")
                    val = val.Substring(0, val.Length - 4);
}
                return val;
            
        }
        private string kthenefjale(string paga)
        {
            return changeToWords(paga).ToLower();
        }
        private void xrLabel4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("EMER") == null) return;
            xrLabel4.Text = "Z./Znj. " + GetCurrentColumnValue("EMER").ToString() + " " + GetCurrentColumnValue("MBIEMER").ToString() + ", lindur më " + string.Format("{0:dd/MM/yyyy}", GetCurrentColumnValue("datelindja")) + ", në " + GetCurrentColumnValue("vendlindja").ToString() + ", identifikuar me nr. personal " + GetCurrentColumnValue("NRSIG").ToString() + ", madhor, me zotësi të plotë juridike për të vepruar, më poshtë quajtur Punëmarrësi.";
            
        }


        private void xrLabel21_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("PAGA") == null) return;
            xrLabel21.Text = "Punëmarrësit do t’i paguhet një pagë bazë bruto prej " + GetCurrentColumnValue("PAGA").ToString() + " (" + kthenefjale(GetCurrentColumnValue("PAGA").ToString()) + ") LEKË, nga e cila Shoqëria do të zbresë kontributin e sigurimeve shoqërore dhe shumën e tatimit mbi të ardhurat personale. Paga neto do të paguhet në një llogari personale të Punëmarrësit. Çdo ndryshim i mëvonshëm i pagës i njoftohet Punëmarrësit me shkrim.  Përveç kësaj page, Punëmarrësi nuk ka të drejtën të kërkojë ndonjë shpërblim tjetër nga Shoqëria." + Environment.NewLine + Environment.NewLine + "Shoqëria do të paguajë pagën dhe çdo shpërblim tjetër pas kryerjes së punës, në ditën e fundit të punës të çdo muaji." + Environment.NewLine + Environment.NewLine + "Punëmarrësi ka të drejtën për kompensim me pushim ose kompensim me pagë për orët që ka punuar të cilat kalojnë 40 orë në javë.Kushtet e kompensimit me pushim përcaktohen nga kjo kontratë dhe nga rregulla dhe direktiva të tjera, që mund të formulohen nga Shoqëria." + Environment.NewLine + Environment.NewLine + "Në lidhje me çdo lloj pagese që Shoqëria i bën Punëmarrësit, e cila nuk është e detyrueshme në bazë të ligjit, Shoqëria rezervon të drejtën ta revokojë dhënien e saj në çdo kohë.";
           
        }


        private void xrLabel123_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("PozicioniPunes") == null) return;

            xrLabel123.Text = "Pozicioni i Punëmarrësit është " + GetCurrentColumnValue("PozicioniPunes").ToString() + " (" + GetCurrentColumnValue("PozicioniPunesENG") + ") " + "në Departamentin e " + GetCurrentColumnValue("DepartamentiPrind").ToString() + " për të kryer detyrat e parashikuara në Aneksin V të kontratës, i cili është pjesë përbërëse dhe e pandarë e kësaj kontrate.";
        }

        private void xrLabel169_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("DTFILLIMI") == null) return;

            xrLabel169.Text = $"Kjo kontratë dhe pjesët përbërëse të përmendura në përmbajtjen e saj përbëjnë tërësinë e mirëkuptimit dhe marrëveshjes së Palëve dhe bëjnë të  pavlefshme çdo kontratë/marrëveshje të mëparshme të Palëve, me përjashtim të datës së fillimit të marrëdhënies së punës, e cila është data {string.Format("{0:d/M/yyyy}", GetCurrentColumnValue("DTFILLIMI"))}";
            
        }

        private void xrLabel179_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("EMER") == null) return;
            xrLabel179.Text = GetCurrentColumnValue("EMER").ToString() + " " + GetCurrentColumnValue("MBIEMER").ToString();
        }

        private void xrLabel2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrLabel2.Text = $"Sot më datë {dateKontrate}, në Tiranë, lidhet kjo kontratë midis:";
        }

        private void xrLabel8_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            xrLabel8.Text = $"Kjo kontratë hyn në fuqi nga data {dateKontrate} dhe është me kohëzgjatje të pacaktuar. Data e fillimit të punës do të konsiderohet Data e Fillimit të marrëdhënies së punës e përcaktuar në nenin XX të kësaj kontrate.";
        }

        private void xrLabel195_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("DTFILLIMI") == null) return;

            xrLabel195.Text = $"(\"Data e Fillimit\"), të përcaktuar në nenin I të Kontratës së Punës datë {string.Format("{0:d/M/yyyy}",GetCurrentColumnValue("DTFILLIMI"))} lidhur midis Palëve.";
        }
    }
}
