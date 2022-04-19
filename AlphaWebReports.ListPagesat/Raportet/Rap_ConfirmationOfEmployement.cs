using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;

namespace AlphaWebReports.RaportetDs.ListPagesat.Raportet
{
    public partial class Rap_ConfirmationOfEmployement : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_ConfirmationOfEmployement(){InitializeComponent();} 
        public Rap_ConfirmationOfEmployement(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        CultureInfo ci;
        
        public Rap_ConfirmationOfEmployement(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
        }
       
        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            



        }

        private void pagaFjale_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
            try {
                string fjaliPaga = Convert.ToString(GetCurrentColumnValue("Fjalipaga"));
                String paga = Convert.ToString(GetCurrentColumnValue("PAGA"));
                string pagaNeFjale = "";
                if (paga != null && paga != " " && paga != "")
                {
                    if (paga.IndexOf('.') != -1)
                        paga = paga.Substring(0, paga.IndexOf('.'));
                    if (paga.IndexOf(',') != -1)
                        paga = paga.Substring(0, paga.IndexOf(','));
                    int pagaInt;
                    if (!int.TryParse(paga, out pagaInt))
                        return;
                    string shumaNeFjale = AlphaWebReports.NumberConverter.changeToWords ((String.Format("{0:#,#.00}", paga)), ci).ToLower();
                    pagaNeFjale = " (" + char.ToUpper(shumaNeFjale[0]) + shumaNeFjale.Substring(1) + ") ALL.";
                }
                pagaFjale.Text = "\n" + fjaliPaga + "" + (String.IsNullOrEmpty(paga) ? "" : String.Format("{0:#,#.#}", Convert.ToDecimal(paga))) + pagaNeFjale;
            }
            catch (Exception ex) {
             

               
            }
        }

        private void Rap_ConfirmationOfEmployement_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBox1.Image = AlphaWebReports.raporteUtil.MerrLogoNdermarrje(this.Extensions["ndermarrjeLogo"]);
        }
    }
}
