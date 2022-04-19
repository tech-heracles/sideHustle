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

namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_MarzhiShitjeve_sipasAgjenteve : DevExpress.XtraReports.UI.XtraReport, IUpdateDetail
    {
        Dictionary<string, bool> skippedDetailBands = new Dictionary<string, bool>();
        bool hapurgjitha = false;
        public Dictionary<string, bool> SkippedDetailBands
        {
            get
            {
                if (skippedDetailBands == null)
                    skippedDetailBands = new Dictionary<string, bool>();
                return skippedDetailBands;
            }
            set { skippedDetailBands = value; }
        }

        public Rap_MarzhiShitjeve_sipasAgjenteve(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_MarzhiShitjeve_sipasAgjenteve(CultureInfo ci, int idNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            Ndermarrja.Value = raport.Parameters[0].Value;
            DtDok.Value = raport.Parameters[1].Value;
            Klient.Value = raport.Parameters[2].Value;
            Artikull.Value = raport.Parameters[3].Value;
            Qyteti.Value = raport.Parameters[4].Value;
            Grupim1.Value = raport.Parameters[5].Value;
            Grupim2.Value = raport.Parameters[6].Value;
            PikeShitjeFurnizim.Value = raport.Parameters[7].Value;
            LlojArtikulli.Value = raport.Parameters[8].Value;
            KlasaArtikulli.Value = raport.Parameters[9].Value;
            parameter1.Value = raport.Parameters[10].Value;
            parameter2.Value = raport.Parameters[11].Value;
            parameter3.Value = raport.Parameters[12].Value;
            parameter4.Value = raport.Parameters[14].Value;
            parameter5.Value = raport.Parameters[15].Value;
            parameter6.Value = raport.Parameters[16].Value;
            DegaAdministrative.Value = raport.Parameters[13].Value;
            parameter7.Value = raport.Parameters[17].Value;
            parameter8.Value = raport.Parameters["filterKodbari"].Value;
            parameter9.Value = raport.Parameters["filterAktivitetiKlient"].Value;
            hapurgjitha = Convert.ToBoolean(raport.Parameters["Detajuar"].Value);

        }
        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
            System.Reflection.Assembly.Load("App_GlobalResources"));
            MarzhiShitjeveLabel.Text = rm.GetString("RaportMarzhiShitjeveSipasAgjenteveTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            EmertimiLabel.Text = rm.GetString("labelEmertimAgjenti", ci);
            LabelKartela.Text = rm.GetString("labelKartela", ci);
            ArtikulliLabelKoka.Text = rm.GetString("labelEmertimiArtikullit", ci);
            NjesiaLabelKoka.Text = rm.GetString("labelNjesia", ci);
            SasiaLabelKoka.Text = rm.GetString("labelSasia", ci);
            KostoNjesiLabelKoka.Text = rm.GetString("labelKostoNjesi", ci);
            KMSHLabelKoka.Text = rm.GetString("labelKMSH", ci);
            CmimiLabelKoka.Text = rm.GetString("labelCmimShitje", ci);
            ShitjaMeZbritjeLabelKoka.Text = rm.GetString("labelVleraShitjesMeZbritje", ci);
            MarzhiBrutoZbritjeLabelKoka.Text = rm.GetString("labelMarzhiBrutoMeZbritje", ci);
            MarzhiBrutoPerqindjeLAbelKoka.Text = rm.GetString("labelMarzhiBrutoPerqindje", ci);
            TotaliLabel.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel32.Text = rm.GetString("labelLogoIMB", ci);
            xrLabel27.Text = rm.GetString("labelKodi", ci);           
        }
     

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("KODIAGJENTSHITJE") != null)
            {
                string kodiagjent = GetCurrentColumnValue("KODIAGJENTSHITJE").ToString();
                if (SkippedDetailBands.ContainsKey(kodiagjent))
                    e.Cancel = Convert.ToBoolean(SkippedDetailBands[kodiagjent]);
                else
                {
                    e.Cancel = !hapurgjitha;
                    SkippedDetailBands.Add(kodiagjent, !hapurgjitha);
                }
            }
        }

        public void UpdateDetail(string kodiagjent)
        {
            if (SkippedDetailBands.ContainsKey(kodiagjent))
                SkippedDetailBands[kodiagjent] = !Convert.ToBoolean(SkippedDetailBands[kodiagjent]);
            else
                SkippedDetailBands.Add(kodiagjent, false);
            
        }

        private void xrTableCell7_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel label = sender as XRLabel;
            if (GetCurrentColumnValue("KODIAGJENTSHITJE") != System.DBNull.Value && GetCurrentColumnValue("KODIAGJENTSHITJE") != null)
            {
                string kodiagjent = GetCurrentColumnValue("KODIAGJENTSHITJE").ToString();
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + kodiagjent + ";Detail;MarzhiShitjeve_sipasAgjenteve')";
                if (!SkippedDetailBands.ContainsKey(kodiagjent))
                    if (hapurgjitha)
                        label.Text = "-";
                    else label.Text = "+";
                else if ((bool)SkippedDetailBands[kodiagjent] == false)
                    label.Text = "-";
                else
                    label.Text = "+";
            }
            else
            {
                label.Text = "";
            }
        }
    }

    

    }

