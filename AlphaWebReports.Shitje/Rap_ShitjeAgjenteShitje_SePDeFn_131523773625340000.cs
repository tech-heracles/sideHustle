using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_ShitjeAgjenteShitje_SePDeFn_131523773625340000 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_ShitjeAgjenteShitje_SePDeFn_131523773625340000()
        {
            InitializeComponent();
        }
        int count = 1;
        double vleraShiturTotal;
        int nrFaturashTotal, nrRreshtashTotal;
        public Rap_ShitjeAgjenteShitje_SePDeFn_131523773625340000(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_ShitjeAgjenteShitje_SePDeFn_131523773625340000(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();

            parameter1.Value = raport.Parameters["IdNdermarje"].Value;

            parameter4.Value = raport.Parameters["filterDtDok"].Value;
            parameter5.Value = raport.Parameters["filterDegeAdministrative"].Value;
            parameter7.Value = raport.Parameters["filterDtRegj"].Value;
            parameter10.Value = raport.Parameters["filterAgjentShitje"].Value;
            DegaAdministrative.Value = raport.Parameters["filterKlientFurnitor"].Value;
            parameter11.Value = raport.Parameters["filterkodifikimartP"].Value;
            parameter13.Value = raport.Parameters["filterkodifikimartD"].Value;
            parameter15.Value = raport.Parameters["filterQyteti"].Value;


            parameter16.Value = raport.Parameters["filterGrupimDokP"].Value;
            parameter18.Value = raport.Parameters["filterGrupimDokD"].Value;
            PershkrimDetajimArt.Value = raport.Parameters["filterGrupimDokT"].Value;
            parameter21.Value = raport.Parameters[21].Value;


            count = 1;
            vleraShiturTotal = 0;
            nrFaturashTotal = 0;
            nrRreshtashTotal = 0;
        }

    }
}
