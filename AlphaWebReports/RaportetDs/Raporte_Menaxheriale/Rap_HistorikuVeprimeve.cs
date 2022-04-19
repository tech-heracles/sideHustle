using System;
using System.Drawing;
using System.Globalization;
using DevExpress.XtraReports.UI;

namespace AlphaWebReports.RaportetDs.Raporte_Menaxheriale
{
    public partial class Rap_HistorikuVeprimeve : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_HistorikuVeprimeve(){InitializeComponent();} 
        public Rap_HistorikuVeprimeve(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.ScopeID, report)
        {

        }

        public Rap_HistorikuVeprimeve(CultureInfo ci, int idNdermarrje, int idViti, string scopeID, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            DtDokLabel.Text = raport.Parameters[0].Description;
            parameter1.Value = raport.Parameters[0].Value;
            xrLabel39.Text = raport.Parameters[1].Description;
            parameter2.Value = raport.Parameters[1].Value;
            xrLabel20.Text = raport.Parameters[2].Description;
            parameter3.Value = raport.Parameters[2].Value;
            xrLabel21.Text = raport.Parameters[3].Description;
            parameter4.Value = raport.Parameters[3].Value;
            xrLabel27.Text = raport.Parameters[4].Description;
            parameter5.Value = raport.Parameters[4].Value;
            xrLabel26.Text = raport.Parameters[5].Description;
            parameter6.Value = raport.Parameters[5].Value;
            xrLabel28.Text = raport.Parameters[6].Description;
            parameter7.Value = raport.Parameters[6].Value;
            xrLabel29.Text = raport.Parameters[7].Description;
            parameter8.Value = raport.Parameters[7].Value;
            xrLabel37.Text = raport.Parameters[8].Description;
            parameter9.Value = raport.Parameters[8].Value;
            xrLabel36.Text = raport.Parameters[9].Description;
            parameter10.Value = raport.Parameters[9].Value;
            xrLabel35.Text = raport.Parameters[10].Description;
            parameter11.Value = raport.Parameters[10].Value;
        }
           /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {

        }

        private void xrLabel14_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("TypeofEvent") != null)
            {
                if (GetCurrentColumnValue("TypeofEvent").ToString() != "")
                {

                    switch (GetCurrentColumnValue("TypeofEvent").ToString())
                    {
                        case "Regjistrim": xrLabel14.ForeColor = Color.Green;
                            break;
                        case "Modifikim": xrLabel14.ForeColor = Color.Blue;
                            break;
                        case "Fshirje": xrLabel14.ForeColor = Color.Red;
                            break;
                        default: xrLabel14.ForeColor = Color.Black;
                            break;


                    }
                }
            }
        }
        private void krijoUrlRaport(object sender, int idDok, bool ngaHistoriku)
        {
            XRLabel myLabel = (XRLabel)sender;
            int idDesign = Convert.ToInt32(GetCurrentColumnValue("IDRAPORTDESING"));
            int idRapToOpen = Convert.ToInt32(GetCurrentColumnValue("IDRAPORTI"));
            int lidhur = ngaHistoriku ? Convert.ToInt32(GetCurrentColumnValue("LIDHUR")) : 0;
            var parametraRaporti = AlphaWebReports.raporteUtil.DeserializoParametraPerKonstruktorRaporti(this.Extensions["parametraRaporti"]);

            if (idDok == 0 || idDesign == 0 || idRapToOpen == 0)
            {
                myLabel.NavigateUrl = "";
                return;
            }
            myLabel.NavigateUrl = "javascript:window.myFaqeCelje.hapFaqeNeTabTeRi('RaportiShpejte.aspx?scopeID=" + parametraRaporti.ScopeID + "&Sesioni=false&idraporti=" + idRapToOpen + "&idDokumenti=" + idDok + "&printo=0&raportdyte=jo&iddesign=" + idDesign + "&lidhur=" + lidhur + "&ngaHistoriku=true')";
            myLabel.Target = "_self";
        }
        private void xrLabel42_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            krijoUrlRaport(sender, Convert.ToInt32(GetCurrentColumnValue("IDDOKNGA")), true);
        }

        private void xrLabel43_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            krijoUrlRaport(sender, Convert.ToInt32(GetCurrentColumnValue("IDDOK")), false);
        }
    }
}
