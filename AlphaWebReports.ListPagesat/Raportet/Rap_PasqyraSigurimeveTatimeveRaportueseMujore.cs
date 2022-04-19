using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.RaportetDs.ListPagesat.Raportet
{
    public partial class Rap_PasqyraSigurimeveTatimeveRaportueseMujore : DevExpress.XtraReports.UI.XtraReport
    {
        int shifraPasPresjes = 0;
        public Rap_PasqyraSigurimeveTatimeveRaportueseMujore() { InitializeComponent(); }
        public Rap_PasqyraSigurimeveTatimeveRaportueseMujore(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) :
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }

        public Rap_PasqyraSigurimeveTatimeveRaportueseMujore(CultureInfo ci, int idNdermarrje, int idViti, XtraReport report)
        {
            InitializeComponent();
            EmrateLabelave(ci);

        }
        private void EmrateLabelave(CultureInfo ci)
        {
            var rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            xrLabel66.Text = rm.GetString("RptPermbledheseListpgKontributetSigShoqMujore", ci);
            xrTableCell31.Text = rm.GetString("lblNr", ci);
            xrTableCell32.Text = rm.GetString("RptPermListpgKontrSigShoqMujoreNjesite", ci);

            xrTableCell33.Text= "09)" + rm.GetString("lblDiteKalendarikePunuar", ci);
            xrTableCell34.Text = "15)" + rm.GetString("lblPagaBrutoLEK", ci);
            xrTableCell35.Text = "16)" + rm.GetString("lblPagaBrutoPerSigShoq", ci);

            xrTableCell40.Text = rm.GetString("lblKontrubutePerSigShoq", ci); ;
            xrTableCell11.Text = rm.GetString("lblNgaKëto", ci) + ":"; ;
            xrTableCell37.Text = rm.GetString("RptPermListpgKontrSigShoqMujoreKontDetyrim", ci);
            xrTableCell38.Text = rm.GetString("RptPermListpgKontrSigShoqMujoreSuplementare", ci);
            xrTableCell21.Text = "17)" + rm.GetString("lblPunedhenesi", ci);
            xrTableCell22.Text = "18)" + rm.GetString("lblPunemarresi", ci);
            xrTableCell23.Text = "19)" + rm.GetString("lblGjithsej", ci);
            xrTableCell24.Text = "20)" + rm.GetString("lblPunedhenesi", ci);
            xrTableCell25.Text = "21)" + rm.GetString("lblPunemarresi", ci);
            xrTableCell26.Text = "22)" + rm.GetString("lblGjithsej2", ci);

            xrTableCell12.Text = "23)" + rm.GetString("lblTotaliSigShoq", ci);
            xrTableCell43.Text = "24)" + rm.GetString("lblPagaBrutoMbiKontributetSigShend", ci);
            xrTableCell44.Text = "25)" + rm.GetString("lblKontributeSigShend", ci);
            xrTableCell15.Text = "26)" + rm.GetString("lblTAP", ci);
        }
    }
}
