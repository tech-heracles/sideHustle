using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace AlphaWebReports.RaportetDs.ListPagesat.Raportet
{
    public partial class Rap_Amendament : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_Amendament(){InitializeComponent();} 
        public Rap_Amendament(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje)
        {

        }

        public Rap_Amendament(System.Globalization.CultureInfo ci, int idNdermarrje)
        {
            InitializeComponent();
        }

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
            "qind", "Mijë", "Milion", "Miliard", "Trilion"
        };
        string shtetas = "";
        string bije = "";
        string mbajtes = "";

        private void Rap_Amendament_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //System.Data.DataSet ds = ((System.Data.DataSet)this.DataSource);
            //if (ds == null || ds.Tables[0].Rows.Count == 0)
            //{
            //    GroupHeader1.Visible = false;
            //    Detail.Visible = false;
            //    PageFooter.Visible = false;
            //}
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
                    for (int i = 0; i<pjeset.Length; i++)
                        numri += pjeset[i];
                    wholeNo = numri;
                }
                else if (idxPresjes > 0 && decimalPlace > 0)
                {
                    string[] pjeset = numb.Split(',');
string numri = "";
                    for (int i = 0; i<pjeset.Length; i++)
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
        private string kthenefjale(string paga)
        {
                string pagaFjale = changeToWords(paga);
            return pagaFjale;
        }
        private void xrLabel28_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            object objNdryshim = GetCurrentColumnValue("NDRYSHIMPAGA");
            if (objNdryshim != null && objNdryshim != DBNull.Value)
                xrLabel28.Visible = !(Convert.ToString(objNdryshim) == "");
        }

        private void xrLabel6_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            object objNdryshim = GetCurrentColumnValue("PozicioniPunes");
            if (objNdryshim != null && objNdryshim != DBNull.Value)
                xrLabel6.Visible = !(Convert.ToString(objNdryshim) == "");
        }

        private void xrLabel18_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("gjinia") == null) return;
            //xrLabel18.Text = GetCurrentColumnValue("EmerMbiemer") + " \"Punëmarrësi\").";
            if (GetCurrentColumnValue("gjinia").ToString() == "Z.")
            {
                shtetas = ", shtetas ";
                mbajtes = ", mbajtës i ";
                bije = ", i biri i ";

            }
            else
            {
                shtetas = ", shtetase ";

                mbajtes = ", mbajtëse e ";
                bije = ", e bija ";
            }

            xrLabel18.Text =GetCurrentColumnValue("gjinia").ToString()+ " "+GetCurrentColumnValue("EmerMbiemer").ToString() + shtetas + GetCurrentColumnValue("kombesia").ToString() + bije + GetCurrentColumnValue("atesia").ToString() + ", lindur më " + String.Format("{0:dd/MM/yyyy}", GetCurrentColumnValue("datelindje"))+ ", në " + GetCurrentColumnValue("vendlindja").ToString() + mbajtes+"kartës së identitetit me nr. " + GetCurrentColumnValue("NRSIG").ToString()+ " (këtu e më poshtë referuar “Punëmarrësi”).";
            //+ GetCurrentColumnValue("Ditelindja").ToString() + " ,ne " + GetCurrentColumnValue("Qyteti").ToString() + " ,  mbajtese (e) i/e kartes se identitetit me nr." + GetCurrentColumnValue("NRIDENTITETI").ToString() + "(ketu e me poshte referuar ";
        }

        private void xrLabel4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
           // xrLabel4.Text =" , në " + GetCurrentColumnValue("vendlindja").ToString() + " ,  mbajtëse (e) i/e kartës së identitetit me nr." + GetCurrentColumnValue("NRSIG").ToString() ;

        }

        private void xrLabel27_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("NDRYSHIMPAGA") == null) return;
            if (GetCurrentColumnValue("NDRYSHIMPAGA").ToString() != "" && GetCurrentColumnValue("Ndryshimpoz").ToString() == "")
                xrLabel27.Text = "Në nenin ____ “Paga Mujore” e  Kontratës së Punës, fjalia e parë e paragrafit te parë, ndryshon si vijon:"+Environment.NewLine;
             if (GetCurrentColumnValue("Ndryshimpoz").ToString() != "" && GetCurrentColumnValue("NDRYSHIMPAGA").ToString() == "")
                xrLabel27.Text = "Në nenin I “Detyrat dhe Kohëzgjatja e Kontratës”, paragrafi i parë  dhe i dytë, ndryshon si vijon:" ;
            if (GetCurrentColumnValue("NDRYSHIMPAGA").ToString() != "" && GetCurrentColumnValue("Ndryshimpoz").ToString() != "")
                xrLabel27.Text = "Në nenin I “Detyrat dhe Kohëzgjatja e Kontratës”, paragrafi i parë  dhe i dytë, ndryshon si vijon:" + Environment.NewLine;
        }

        private void xrLabel6_BeforePrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("NDRYSHIMPAGA") == null) return;

            if (GetCurrentColumnValue("NDRYSHIMPAGA").ToString() != "" && GetCurrentColumnValue("Ndryshimpoz").ToString() == "")
                xrLabel6.Text =Environment.NewLine+ GetCurrentColumnValue("NDRYSHIMPAGA").ToString()+" ("+kthenefjale(GetCurrentColumnValue("PAGA").ToString())+ ") Lekë, nga e cila Shoqëria do të zbresë kontributin e sigurimeve shoqërore dhe shumën e tatimit mbi të ardhurat personale”.";
            if (GetCurrentColumnValue("Ndryshimpoz").ToString() != "" && GetCurrentColumnValue("NDRYSHIMPAGA").ToString() == "" )
                xrLabel6.Text = Environment.NewLine + GetCurrentColumnValue("Ndryshimpoz").ToString();
            if (GetCurrentColumnValue("NDRYSHIMPAGA").ToString() != "" && GetCurrentColumnValue("Ndryshimpoz").ToString() != "")
                xrLabel6.Text = Environment.NewLine + GetCurrentColumnValue("Ndryshimpoz").ToString(); 
        }

        private void xrLabel33_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("NDRYSHIMPAGA") == null) return;
            if (GetCurrentColumnValue("NDRYSHIMPAGA").ToString() != "" && GetCurrentColumnValue("Ndryshimpoz").ToString() != "")
                xrLabel33.Text = "Në nenin ____ “Paga Mujore” e  Kontratës së Punës, fjalia e parë e paragrafit te parë, ndryshon si vijon:" + Environment.NewLine + Environment.NewLine + GetCurrentColumnValue("NDRYSHIMPAGA").ToString()+" (" + kthenefjale(GetCurrentColumnValue("PAGA").ToString()) + ") Lekë, nga e cila Shoqëria do të zbresë kontributin e sigurimeve shoqërore dhe shumën e tatimit mbi të ardhurat personale”."; 
            else
                xrLabel33.Text = "Ky Amendament i bashkëngjitet Kontratës së Punës dhe është pjesë përbërëse dhe e pandashme e saj." + Environment.NewLine + GetCurrentColumnValue("HynNeFuqi").ToString(); ;
        }

        private void xrLabel36_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("NDRYSHIMPAGA") == null) return;
            if (GetCurrentColumnValue("NDRYSHIMPAGA").ToString() != "" && GetCurrentColumnValue("Ndryshimpoz").ToString() != "")
                xrLabel36.Text = "Ky Amendament i bashkëngjitet Kontratës së Punës dhe është pjesë përbërëse dhe e pandashme e saj."+Environment.NewLine +GetCurrentColumnValue("HynNeFuqi").ToString();
            else xrLabel36.Text = "Të gjitha afatet dhe kushtet e kësaj Marrëveshjeje, të cilat nuk preken nga ky Amendament, mbeten plotësisht në fuqi. ";
        }

        private void xrLabel39_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("NDRYSHIMPAGA") == null) return;
            if (GetCurrentColumnValue("NDRYSHIMPAGA").ToString() != "" && GetCurrentColumnValue("Ndryshimpoz").ToString() != "")
                xrLabel39.Text = "Të gjitha afatet dhe kushtet e Kontratës së Punës të cilat nuk preken nga ky Amendament, mbeten plotësisht në fuqi.";
            else xrLabel39.Text = "Ky amendament hartohet në 2 (dy) kopje origjinale në gjuhën Shqipe, një për secilën Palë.";
        }

        private void xrPanel1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("NDRYSHIMPAGA") == null) return;
            if (GetCurrentColumnValue("NDRYSHIMPAGA").ToString() != "" && GetCurrentColumnValue("Ndryshimpoz").ToString() != "")
                xrPanel1.Visible = true;

            else xrPanel1.Visible = false;
        }

        private void xrLabel31_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
        
              
        }

        private void xrLabel34_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

             
        }

        private void xrLabel37_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            
                
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
          
        }

        private void xrPanel2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("Hyrje") != null && GetCurrentColumnValue("Hyrje").ToString() != "")
            {
                xrPanel2.Visible = true;

            }

        }

        private void xrLabel7_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var hyrje = GetCurrentColumnValue("Hyrje") == null ? "" : GetCurrentColumnValue("Hyrje").ToString();
            xrLabel7.Text=$"{hyrje} (këtu më poshtë referuar si \"Kontrata e Punës\"), midis palëve të mëposhtme:";
        }
    }
}
