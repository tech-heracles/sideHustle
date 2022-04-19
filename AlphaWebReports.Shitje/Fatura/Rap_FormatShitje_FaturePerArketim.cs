using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_FormatShitje_FaturePerArketim : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FormatShitje_FaturePerArketim(){InitializeComponent();} 
		public Rap_FormatShitje_FaturePerArketim(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
		this(param.Ci,param.IdNdermarrje,param.IdPerdoruesi){}
        public Rap_FormatShitje_FaturePerArketim(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
            EmrateLabelave(ci);

            switch (ci.ToString())
            {
                case "sq-AL": //shqip
                    parameter1.Value = 0;
                    break;
                case "en_US": //anglisht
                    parameter1.Value = 1;
                    break;
                default: break;

            }
          
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                       System.Reflection.Assembly.Load("App_GlobalResources"));

            xrLabel7.Text = rm.GetString("labelEmriiInstitucionitperfitues", ci);
            xrLabel9.Text = rm.GetString("labelKodiiDegesseThesaritkuvepron", ci);
            xrLabel2.Text = rm.GetString("labelNumer:", ci) + "Numer:";
            xrTableCell3.Text = rm.GetString("label7.......", ci) + " 7.......";
            xrTableCell5.Text = rm.GetString("labelObjektiiDetyrimit", ci) + " 2";
            xrTableCell11.Text = rm.GetString("labelKodiiLlog.Ekonomike", ci) + " Kodi i Llog. Ekonomike";
            xrTableCell7.Text = rm.GetString("labelKodiiLlog.Ekonomike", ci);
            xrTableCell16.Text = rm.GetString("labelShumaqeArketohet(Leke)", ci) + " Shuma qe Arketohet (Leke)";
            xrTableCell10.Text = rm.GetString("labelEmertimi", ci);
            xrTableCell6.Text = rm.GetString("labelEmertimi", ci) + " 2";

        }

       

       
    }
}
