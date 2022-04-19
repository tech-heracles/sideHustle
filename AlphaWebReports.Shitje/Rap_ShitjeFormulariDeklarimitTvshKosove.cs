using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;


namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_ShitjeFormulariDeklarimitTvshKosove : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_ShitjeFormulariDeklarimitTvshKosove() 
        {
            InitializeComponent();
        }
 
        private string viti;
        private string data;
        private string dtFillimi;
        private string muaji;


        public Rap_ShitjeFormulariDeklarimitTvshKosove(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_ShitjeFormulariDeklarimitTvshKosove(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            data = raport.Parameters["filterDtDok"].Value.ToString();
            dtFillimi = data.Split('-')[0];
            muaji = dtFillimi.Split('/')[1] + " " + dtFillimi.Split('/')[2];
            xrTableCell3.Text = muaji;



        }

        private void Rap_ShitjeFormulariDeklarimitTvshKosove_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var parametraRaporti = AlphaWebReports.raporteUtil.DeserializoParametraPerKonstruktorRaporti(this.Extensions["parametraRaporti"]);
            xrTableCell7.Text = parametraRaporti.NdermarrjeNipt;
            xrTableCell15.Text = parametraRaporti.NdermarrjePershkrimi;
            xrTableCell11.Text = parametraRaporti.NdermarrjeVendi;
            xrTableCell20.Text = parametraRaporti.NdermarrjeTel;
        }
    }
}
