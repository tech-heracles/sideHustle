using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.RaportetDs.Magazina
{
    public partial class Rap_KartelaAnalitike_DokumentaveInventarizimit : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_KartelaAnalitike_DokumentaveInventarizimit()
        {
            InitializeComponent();
        }
        public Rap_KartelaAnalitike_DokumentaveInventarizimit(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) :
            this(param.IdRaporti, param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, param.IdGjuha, report)
        {
        }
        public Rap_KartelaAnalitike_DokumentaveInventarizimit(int idRaporti, CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, int idGjuha, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
        }

        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",System.Reflection.Assembly.Load("App_GlobalResources"));

            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel12.Text = rm.GetString("RaportKartelaAnalitikeDokInventarizimi", ci);
            xrTableCell47.Text = rm.GetString("labelKartela", ci);
            xrTableCell34.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell49.Text = rm.GetString("labelBarkodi", ci);
            xrTableCell36.Text = rm.GetString("labelFilterAvancuarMagazina", ci);
            xrTableCell42.Text = rm.GetString("labelNjesiaMatjes", ci);
            xrTableCell52.Text = rm.GetString("labelRaportKosto", ci);
            xrTableCell44.Text = rm.GetString("labelRaportTedhenatKontabilitetit", ci);
            xrTableCell45.Text = rm.GetString("lblRaportTedhenaInventar", ci);
            xrTableCell46.Text = rm.GetString("lblRaportRezultatet", ci);
            xrTableCell56.Text = xrTableCell57.Text = rm.GetString("lblRaportSasia", ci);
            xrTableCell53.Text = xrTableCell54.Text = rm.GetString("lblRaportVleftaLeke", ci);
            xrTableCell58.Text = xrTableCell55.Text = rm.GetString("labelRaportVlera", ci);
            xrTableCell59.Text = rm.GetString("labelRaportMungesa", ci);
            xrTableCell60.Text = rm.GetString("labelRaportTeprica", ci);
            xrTableCell23.Text = rm.GetString("lblDateDokumenti", ci);
            xrTableCell67.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel37.Text = rm.GetString("labelLogoIMB", ci);

        }

    }
}
