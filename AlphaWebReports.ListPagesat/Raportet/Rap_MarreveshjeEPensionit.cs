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
    public partial class Rap_MarreveshjeEPensionit : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_MarreveshjeEPensionit(){InitializeComponent();} 
        public Rap_MarreveshjeEPensionit(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this()
        {

        }

        private void xrLabel66_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("KarteIdentiteti") == null)
                return;
            xrLabel66.Text = "2. Z/Znj " + Convert.ToString(GetCurrentColumnValue("EmerMb")) + " identifikuar me këtë kartë identiteti nr. " + GetCurrentColumnValue("KarteIdentiteti");
            
        }

        private void xrLabel93_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("KontributMujorAnetari") == null)
                return;
            xrLabel93.Text = "1.2 Punëdhënësi ka pëlqimin e Punëmarrësit që të zbresë nga paga e tij sipas legjislacionit në fuqi, kontributet e Punëmarrësit për fondin e pensionit profesional, në shumën totale prej " + Convert.ToString(GetCurrentColumnValue("KontributMujorAnetari")) + " Lekë.";
        }
        private void xrTableCell3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("DtKontrate") == null) return;
            
            string dataEFormatuar = raporteUtil.MerrDateSipasFormatitOseEmptyString(Convert.ToString(GetCurrentColumnValue("DtKontrate")), "d/M/yyyy");
            xrTableCell3.Text = "Sot më datë " + dataEFormatuar + " në Tiranë, nënshkruhet kjo marrëveshje (këtu e më poshtë e quajtur \"Marrëveshja\") nga:";
        }

      
        private void xrLabel88_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("KontributPunedhenesit") == null) return;
            xrLabel88.Text = Convert.ToString(GetCurrentColumnValue("KontributPunedhenesit")) + " Lekë";
        }

        private void xrLabel90_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("KontributMujorAnetari") == null) return;
            xrLabel90.Text = Convert.ToString(GetCurrentColumnValue("KontributMujorAnetari")) + " Lekë";
        }

        private void xrLabel71_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            if (GetCurrentColumnValue("DtKontratPensioni") == null)
                return;

            string dataEFormatuar = raporteUtil.MerrDateSipasFormatitOseEmptyString(Convert.ToString(GetCurrentColumnValue("DtKontratPensioni")), "d/M/yyyy");
            xrLabel71.Text = $"1- Punëdhënësi dhe Punëmarrësi kanë nënshkruar Kontratën e Punës datë {dataEFormatuar}";
        }


        
    }
}
