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
    public partial class Rap_KontrataEPensionit : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_KontrataEPensionit()
        { InitializeComponent();
        } 

        public Rap_KontrataEPensionit(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_KontrataEPensionit(CultureInfo ci, int idNdermarrje, int idViti, XtraReport report)
        {
            InitializeComponent();
            EmrateLabelave(ci);
        }

        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel51.Text = rm.GetString("LabelAdministratorPergjithshem", ci);
            xrLabel52.Text = rm.GetString("LabelAdminPergjithshem", ci);
            xrLabel53.Text = rm.GetString("LabelZvAdministratorPergjithshem", ci);
            xrLabel54.Text = rm.GetString("LabelZvAdminPergjithshem", ci);
            xrLabel23.Text = "e krijuar në përputhje me legjislacionin shqiptar, regjistruar si person juridik me Vendimin e Gjykatës së Shkallës së Parë, Tiranë, nr. 33825, datë 18 / 07 / 2005, me NIPT nr.K51928001I,  me seli në adresën: Blv. “Bajram Curri”, ETC, Kati i 9 - të, Tiranë, përfaqësuar nga " + rm.GetString("LabelZZNJAdministratorPergjithshem", ci) + ", " + rm.GetString("LabelAdminPergjithshem", ci) + " dhe " + rm.GetString("LabelZZNJZvAdministratorPergjithshem", ci) + ", " + rm.GetString("LabelZvAdminPergjithshem", ci) + ".";
        }
        private void xrTableCell1_BeforePrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("Datelindja") == null) return;
            string dataEFormatuar = raporteUtil.MerrDateSipasFormatitOseEmptyString(Convert.ToString(GetCurrentColumnValue("Datelindja")), "dd/MM/yyyy");
            xrTableCell1.Text = GetCurrentColumnValue("EmerMbAtesi") + ", " + GetCurrentColumnValue("Kombesia") + "e, Nr. i dokumentit të identifikimit ID " + GetCurrentColumnValue("KarteIdentiteti") + "  (bashkëlidhur kopje e dok. të identifikimit), Datëlindja " + dataEFormatuar + ", " + GetCurrentColumnValue("Vendlindja") + " Adresa " + GetCurrentColumnValue("Adresa") + ", Tel " + GetCurrentColumnValue("Telefon") + ", email" + " " + GetCurrentColumnValue("Email") + ", Nr. i pashaportës (vetëm për shtetasit e huaj) " + GetCurrentColumnValue("NRPASHAPORTE") + " " + " " + "i punësuar  në Vodafone Albania Sh.A., adresa e punëdhënësit  Rr. "+'"'+"Pavarësia"+'"'+", Nr. 61, Autostrada Tiranë-Durrës, Kashar, Tiranë.";
        }

        private void xrLabel33_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("KontributMujorAnetari") == null) return;
            xrLabel33.Text = GetCurrentColumnValue("KontributMujorAnetari")  + " Lekë";
        }

        private void xrLabel34_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("KontributMujorAnetari") == null) return;
            xrLabel34.Text = GetCurrentColumnValue("TotalKontribut") + " Lekë";
        }

        
    }
}
