using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_UrdherShitjetSipasMenPageses : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_UrdherShitjetSipasMenPageses(){InitializeComponent();} 
        //var MENYRAPAGESE= new Array({ id: 0, pershkrim: "Me mirebesim" },{ id: 4, pershkrim: "Pagese"},{ id: 5, pershkrim: "Pagese Automatike"},{ id: 7, pershkrim: "Me parapagim"},{ id: 8, pershkrim: "Arke"},{ id: 9, pershkrim: "Karte krediti"});

        public Rap_UrdherShitjetSipasMenPageses(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_UrdherShitjetSipasMenPageses(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
        }
    }
}
