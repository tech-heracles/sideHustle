using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_ShitjeAnalitikMeMarzh : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_ShitjeAnalitikMeMarzh()
        {
            InitializeComponent();
        }
        int shifraPasPresjes = 0;
        public Rap_ShitjeAnalitikMeMarzh(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_ShitjeAnalitikMeMarzh(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            parameter1.Value = raport.Parameters[0].Value;
            parameter2.Value = raport.Parameters[1].Value;
            parameter3.Value = raport.Parameters[2].Value;
            parameter4.Value = raport.Parameters[3].Value;
            parameter5.Value = raport.Parameters[4].Value;
            parameter6.Value = raport.Parameters[5].Value;
            parameter7.Value = raport.Parameters[6].Value;
            parameter8.Value = raport.Parameters[7].Value;
            parameter9.Value = raport.Parameters[8].Value;
            parameter10.Value = raport.Parameters[9].Value;
            parameter11.Value = raport.Parameters[10].Value;
            parameter12.Value = raport.Parameters[11].Value; 
            parameter13.Value = raport.Parameters[13].Value; 
            parameter14.Value = raport.Parameters[14].Value;  
            parameter15.Value = raport.Parameters[15].Value;
            DegaAdministrative.Value = raport.Parameters[12].Value;
            parameter16.Value = raport.Parameters[16].Value;
            parameter17.Value = raport.Parameters[17].Value;
            parameter18.Value = raport.Parameters[18].Value;
            parameter19.Value = raport.Parameters[19].Value;
            parameter20.Value = raport.Parameters[20].Value;
            parameter21.Value = raport.Parameters[21].Value;
            PershkrimDetajimArt.Value = raport.Parameters[27].Value;
            parameter22.Value = raport.Parameters[28].Value; 
            adresaFaturimit.Value = raport.Parameters[29].Value;
            parameter30.Value = raport.Parameters[30].Value;
            parameter23.Value = raport.Parameters["filterKodbari"].Value;
        }
        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
          ResourceManager  rm = new ResourceManager("Resources.Strings",
                     System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel17.Text = rm.GetString("RaportRegjistriAnalitikShitjesMeMarzhTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel1.Text = rm.GetString("lblNrFature2", ci);
            xrLabel11.Text = rm.GetString("labelDate", ci);
            xrTableCell13.Text = rm.GetString("lblKlienti", ci);
            xrLabel5.Text = rm.GetString("lblRaportiKodArtikulli", ci);
            xrLabel18.Text = rm.GetString("lblPershkrimArtikulli", ci);
            xrTableCell19.Text = rm.GetString("labelNjesia", ci);
            xrLabel6.Text = rm.GetString("labelRaportSasia", ci);
            xrLabel16.Text = rm.GetString("labelRaportCmimiPaTVSH", ci);
            xrTableCell9.Text = rm.GetString("labelVleftapaTVSH", ci);
            xrTableCell10.Text = rm.GetString("labelTVSH", ci);
            xrTableCell11.Text = rm.GetString("labelVleftameTVSH", ci);
            xrTableCell12.Text = rm.GetString("labelKMSH", ci);
            xrTableCell14.Text = rm.GetString("labelKostoNjesi", ci);
            xrTableCell15.Text = rm.GetString("lblFitimiBruto", ci);
            xrTableCell16.Text = rm.GetString("lblFitimiBrutoPerqindje", ci);
            xrLabel43.Text = rm.GetString("labelLogoIMB", ci);
        }
    }
}
