using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
using System.Collections.Generic;
using AlphaWebReports.Common;

namespace AlphaWebReports.RaportetDs.Magazina
{
    public partial class Rap_EkzekutimiRezervimeveFormat2 : DevExpress.XtraReports.UI.XtraReport, IUpdateDetailMeDyId
     
    {
		public Rap_EkzekutimiRezervimeveFormat2(){InitializeComponent();} 
        private Hashtable skippedDetailBands;
        bool hapurgjitha = false;
        public Hashtable SkippedDetailBands
        {
            get
            {
                if (skippedDetailBands == null)
                    skippedDetailBands = new Hashtable();

                return skippedDetailBands;
            }
            set { skippedDetailBands = value; }
        }
        public Rap_EkzekutimiRezervimeveFormat2(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this( param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, param.IdGjuha, report)
        {

        }
        public Rap_EkzekutimiRezervimeveFormat2(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, int idGjuha, DevExpress.XtraReports.UI.XtraReport raport)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            InitializeComponent();
            EmrateLabelave(ci);
            gjuha.Value = idGjuha;
            parameter1.Value = raport.Parameters["filterKlientFurnitor"].Value;
            parameter2.Value = raport.Parameters["IdNdermarje"].Value;
            parameter3.Value = raport.Parameters["filterMagazina"].Value;
            parameter4.Value = raport.Parameters["filterKartela"].Value;
            parameter5.Value = raport.Parameters["filterDegeAdministrative"].Value;
            parameter6.Value = raport.Parameters["filterDtDok"].Value;
            parameter7.Value = raport.Parameters["filterDtRegj"].Value;
            parameter8.Value = raport.Parameters["filterNrDokRezervime"].Value;
            parameter9.Value = raport.Parameters["filterkodifikimartP"].Value;
            parameter10.Value = raport.Parameters["filterkodifikimartD"].Value;
            parameter12.Value = raport.Parameters["filterLlojDokumenti"].Value;
            hapurgjitha = Convert.ToBoolean(raport.Parameters["Detajuar"].Value);
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel12.Text = rm.GetString("lblRaportEkzekutimRezervimFormat2", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrTableCell9.Text = rm.GetString("lblRaportEkzekutimet", ci);
            xrTableCell5.Text = rm.GetString("lblRaportDiferencat", ci);
            xrTableCell16.Text = rm.GetString("labelLlojDokumenti", ci);
            xrTableCell14.Text = rm.GetString("labelRaportiSubjekti", ci);
            xrTableCell17.Text = rm.GetString("labelRaportiNrDok", ci);
            xrTableCell18.Text = rm.GetString("labelRaportiDtDok", ci);
            xrTableCell13.Text = rm.GetString("labelKartela", ci);
            xrTableCell19.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell8.Text = rm.GetString("lblRaportDokHyrjeRezervim", ci);
            xrTableCell20.Text = rm.GetString("labelNjesia", ci);
            xrTableCell21.Text = rm.GetString("lblRaportSasiaRezervuar", ci);
            xrTableCell10.Text = rm.GetString("lblRaportMagazina", ci);
            xrTableCell11.Text = rm.GetString("labelSasia", ci);
            xrTableCell11.Text = rm.GetString("lblRaportMagazina", ci);
            xrTableCell23.Text = rm.GetString("labelSasia", ci);
            xrTableCell12.Text = rm.GetString("lblRaportStatusi", ci);
            xrLabel37.Text = rm.GetString("labelLogoIMB", ci);
            xrTableCell15.Text = rm.GetString("lblRaportGjeneruarNga", ci);
        }

        private void xrLabel43_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel label = sender as XRLabel;
            object objIdkoka = GetCurrentColumnValue("IDKOKAREZERVIME");
            object objKodArtikull = GetCurrentColumnValue("KODARTIKULLI");
            object objKodKonfigAmbjente2 = GetCurrentColumnValue("KODKONFIGAMBJENTE2");
            if (objIdkoka != DBNull.Value && objIdkoka != null && objKodArtikull !=DBNull.Value && objKodArtikull!=null && objKodKonfigAmbjente2 != null && objKodKonfigAmbjente2.ToString() != string.Empty)
            {
                string idkoka = Convert.ToString(objIdkoka);
                string kodartikull = Convert.ToString(objKodArtikull);
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + idkoka + ";"+kodartikull+";ekzekutimRezervimeshFormat2')";
                if (!SkippedDetailBands.ContainsKey(idkoka + ";" + kodartikull))
                    if (hapurgjitha)
                        label.Text = "-";
                    else label.Text = "+";
                else if (((bool)SkippedDetailBands[idkoka + ";" + kodartikull] == false))
                    label.Text = "-";
                else
                    label.Text = "+";
            }
            else
            {
                label.Text = "";
            }
        }
        public void UpdateDetail(string kodPrind,string kodartikull)
        {
            if (SkippedDetailBands.ContainsKey(kodPrind+ ";" + kodartikull))
            {
                SkippedDetailBands[kodPrind+ ";" + kodartikull] = !Convert.ToBoolean(SkippedDetailBands[kodPrind + ";" + kodartikull]);
            }
            else
            {
                SkippedDetailBands.Add(kodPrind + ";" + kodartikull, false);
            }
            
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            object objIdkoka = GetCurrentColumnValue("IDKOKAREZERVIME");
            object objKodArtikull = GetCurrentColumnValue("KODARTIKULLI");
            if (objIdkoka != DBNull.Value && objIdkoka != null && objKodArtikull!=DBNull.Value && objKodArtikull!=null)
            {
                string idkoka = Convert.ToString(objIdkoka);
                string kodartikull= Convert.ToString(objKodArtikull);
                if (SkippedDetailBands.ContainsKey(idkoka + ";" + kodartikull))
                    e.Cancel = Convert.ToBoolean(SkippedDetailBands[idkoka + ";" + kodartikull]);
                else
                {
                    e.Cancel = !hapurgjitha;
                    SkippedDetailBands.Add(idkoka+";"+ kodartikull, !hapurgjitha);
                }
            }
        }
    }
}
