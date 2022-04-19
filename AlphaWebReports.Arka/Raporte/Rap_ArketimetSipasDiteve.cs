using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;

namespace AlphaWebReports.RaportetDs.Arka.Raporte
{
  
    public partial class Rap_ArketimetSipasDiteve : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_ArketimetSipasDiteve(){InitializeComponent();} 
       
        public Rap_ArketimetSipasDiteve(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, report)
        {

        }
        public Rap_ArketimetSipasDiteve(CultureInfo ci, int idNdermarrje, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();

            parameter1.Value = raport.Parameters["filterDtDok"].Value;
            parameter2.Value = raport.Parameters["filterArkaBankaEmer"].Value;
            parameter3.Value = raport.Parameters["filterKompania"].Value;

        }

        private void EmraTeLabelave(CultureInfo ci)
        {
            //ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            //xrTableCell1.Text = rm.GetString("labelRaportiDataEPorosise", ci);
            //xrTableCell21.Text = rm.GetString("labelRaportiDataEPorosise", ci);
            //xrTableCell5.Text = rm.GetString("labelRaportiDataEShitjes", ci);
            //xrTableCell7.Text = rm.GetString("labelRaportiShitesiQeBeriOrderin", ci);
            //xrTableCell8.Text = rm.GetString("labelRaportiShitesiQeBeriShitjen", ci);
            //xrTableCell9.Text = rm.GetString("labelFilterAvancuarDyqan", ci);
            //xrTableCell10.Text = rm.GetString("labelRaportiKompania", ci);
            //xrTableCell11.Text = rm.GetString("labelRaportTel", ci);
            //xrTableCell18.Text = rm.GetString("labelRaportProdukti", ci);
            //xrTableCell19.Text = rm.GetString("labelCmimi", ci);
            //xrTableCell20.Text = rm.GetString("labelFilterAvancuarStatusi", ci);

            //xrLabel12.Text = rm.GetString("TitullRaportiStatusiVFONEPOROSI", ci);

            //FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            //xrLabel52.Text = rm.GetString("labelFooterNdermarrja", ci) + ":";
            //xrlabel100.Text = rm.GetString("labelRaportiDtDok", ci) + ":";
            //xrLabel55.Text = rm.GetString("labelFilterAvancuarDyqan", ci) + ":";
        }
       

  

    }
}
