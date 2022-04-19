using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
using System.Collections.Generic;
using System.Data;
using AlphaWebReports.Common;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_Shitje_Cikli_Konvertimeve : DevExpress.XtraReports.UI.XtraReport, IUpdateDetail
    {
		public Rap_Shitje_Cikli_Konvertimeve(){InitializeComponent();} 
        bool hapurgjitha = false;
        private Hashtable skippedDetailBands;
     
        private int idVit;

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
        public Rap_Shitje_Cikli_Konvertimeve(AlphaWebReports.Common.ParametraRaporti param, XtraReport report)
           : this(param.Ci, param.IdNdermarrje, param.IdViti, param.ScopeID, report) { }

        public Rap_Shitje_Cikli_Konvertimeve(CultureInfo ci, int idNdermarrje, int idViti, string scopeID,  DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            parameter1.Value = raport.Parameters[0].Value;
            parameter2.Value = raport.Parameters[2].Value;
            parameter3.Value = raport.Parameters[11].Value;
            parameter4.Value = raport.Parameters[10].Value;
            parameter5.Value = raport.Parameters[1].Value;
            parameter6.Value = raport.Parameters[9].Value;
            parameter7.Value = raport.Parameters[3].Value;
            parameter8.Value = raport.Parameters[7].Value;
            parameter9.Value = raport.Parameters[5].Value;
            parameter10.Value = raport.Parameters[6].Value;
            parameter11.Value = raport.Parameters[4].Value;
            hapurgjitha = Convert.ToBoolean(raport.Parameters["Detajuar"].Value);
            EmrateLabelave(ci);
            idVit = idViti;

        }

       

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel12.Text = rm.GetString("lblTitulliRaportNdjekjaECiklitTeKonvertimeve", ci);
            xrLabel2.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel7.Text = rm.GetString("labelLogoIMB", ci);
            xrLabel28.Text = rm.GetString("labelRaportiLloji", ci);
            xrLabel29.Text = rm.GetString("labelRaportiNr", ci);
            xrLabel30.Text = rm.GetString("labelRaportiDtDok", ci);
            xrLabel1.Text = rm.GetString("MenuItemKlientFurnitor", ci);
            xrLabel32.Text = rm.GetString("cmbCmimeArtikulliCaptionPershkrimi", ci);
            xrLabel33.Text = rm.GetString("labelFilterAvancuarMonedha", ci);
            xrLabel34.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel44.Text = rm.GetString("labelVleraMbetur", ci);
            xrLabel45.Text = rm.GetString("labelRaportAfatiKohor", ci);
        }


        public void UpdateDetail(string detailID)
        {
            if (SkippedDetailBands.Contains(detailID))
                    SkippedDetailBands[detailID] = !Convert.ToBoolean(SkippedDetailBands[detailID]);
                else
                    SkippedDetailBands.Add(detailID, false);
                  
        }

        private void xrTableCell95_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel label = sender as XRLabel;
            if (GetCurrentColumnValue("IDKRYESOR") != System.DBNull.Value && GetCurrentColumnValue("IDKRYESOR") != null)
            {
                string nrdok = GetCurrentColumnValue("IDPRIND").ToString();
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + nrdok + ";Detail;ndjekjaECiklitTeKonvertimeve')";
                if (!SkippedDetailBands.ContainsKey(nrdok))
                    if (hapurgjitha || (GetCurrentColumnValue("GJETHE").ToString() == "JO"))
                        label.Text = "-";
                    else if ((GetCurrentColumnValue("GJETHE").ToString() == "PO"))
                        { label.Text = " "; }
                    else label.Text = "+";
                else if ((bool)SkippedDetailBands[nrdok] == false)
                    label.Text = "-";
                else
                {  Detail.Visible = false;
                    label.Text = "+";
                    }
            }
            else
            {
                label.Text = "";
            }
        }




        private void xrTableCell96_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            cell.NavigateUrl = ktheUrlSipasLlojDokIdDokNrDok(cell);
        }

        private string ktheUrlSipasLlojDokIdDokNrDok(XRTableCell xrTableCell)
        {
            Object iddok = GetCurrentColumnValue("IDDOK");
            object nrdok = GetCurrentColumnValue("NRDOK");
            var parametraRaporti = AlphaWebReports.raporteUtil.DeserializoParametraPerKonstruktorRaporti(this.Extensions["parametraRaporti"]);

            if (iddok != null && nrdok != null)
            {
                object llojDokMag = GetCurrentColumnValue("LLOJDOKUMENTIMAGAZINE");
                if (llojDokMag != null && llojDokMag != DBNull.Value)
                {
                  
                        xrTableCell.ForeColor = System.Drawing.Color.SteelBlue;
                    xrTableCell.Target = "_self";
                    return "javascript:window.myFaqeCelje.hapFaqeNeTabTeRi('Shto_RegjistrimMagazine.aspx?scopeID=" + parametraRaporti.ScopeID + "&newScopeId=True&lloj=" + llojDokMag.ToString() + "&id=" + iddok.ToString() + "&numer=" + nrdok.ToString() + "&shtim_modifikim=modifikim')";
                       
                }
                object idkatdok = GetCurrentColumnValue("IDKATDOK");
                if (Convert.ToInt32(idkatdok) == 2) //blerje
                {
                  
                        xrTableCell.ForeColor = System.Drawing.Color.SteelBlue;
                    xrTableCell.Target = "_self";
                    return "javascript:window.myFaqeCelje.hapFaqeNeTabTeRi('Shto_RegjistrimDokumentash.aspx?scopeID=" + parametraRaporti.ScopeID + "&newScopeId=True&shitje_blerje=blerje&id=" + iddok.ToString() + "&numer=" + nrdok.ToString() + "&shtim_modifikim=modifikim')";
                }
                if (Convert.ToInt32(idkatdok) == 1) //shitje
                {
                   
                        xrTableCell.ForeColor = System.Drawing.Color.SteelBlue;
                    xrTableCell.Target = "_self";
                    return "javascript:window.myFaqeCelje.hapFaqeNeTabTeRi('Shto_RegjistrimDokumentash.aspx?scopeID=" + parametraRaporti.ScopeID + "&newScopeId=True&shitje_blerje=shitje&id=" + iddok.ToString() + "&numer=" + nrdok.ToString() + "&shtim_modifikim=modifikim')";
                }
            }
            return "";
        }


        private void GroupHeader11_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Detail.Visible = true; 
            object objPrindi = GetCurrentColumnValue("IDKRYESOR");
            if (objPrindi != System.DBNull.Value && objPrindi != null )
            {
                string prindi = objPrindi.ToString();
                if (!SkippedDetailBands.Contains(prindi))
                {
                    SkippedDetailBands.Add(prindi, !hapurgjitha);
                }
            }
           
        }

        private void xrTableCell2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell cell = sender as XRTableCell;
            cell.NavigateUrl = ktheUrlSipasLlojDokIdDokNrDok(cell);
        }

        private void xrTableCell1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel label = sender as XRLabel;
            if (GetCurrentColumnValue("IDPRIND") != System.DBNull.Value && GetCurrentColumnValue("IDPRIND") != null)
            {
                string nrdok = GetCurrentColumnValue("IDDOK").ToString();
                
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + nrdok + ";Detail;ndjekjaECiklitTeKonvertimeve')";
                if (!SkippedDetailBands.ContainsKey(nrdok))
                    if (hapurgjitha && (GetCurrentColumnValue("GJETHE").ToString() == "JO"))
                        label.Text = "-";
                    else if (hapurgjitha && (GetCurrentColumnValue("GJETHE").ToString() == "PO"))
                        label.Text = "";
                else if (GetCurrentColumnValue("GJETHE").ToString() == "PO")
                         label.Text = "";
                    else label.Text = "+";
                else if ((bool)SkippedDetailBands[nrdok] == false)
                    label.Text = "-";

                else
                   
                    label.Text = "+";
              
            }
            else
            {
                label.Text = "";
            }
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (Convert.ToInt32(GetCurrentColumnValue("NIVELI")) == 1)
                e.Cancel = true;

            if (GetCurrentColumnValue("IDPRIND") != null && (Convert.ToInt32(GetCurrentColumnValue("NIVELI")) > 1))
            {
                string idPrind = GetCurrentColumnValue("IDPRIND").ToString();
                if (SkippedDetailBands.ContainsKey(idPrind))
                    e.Cancel = Convert.ToBoolean(SkippedDetailBands[idPrind]);
                else

                {
                    e.Cancel = !hapurgjitha;
                    SkippedDetailBands.Add(idPrind, !hapurgjitha);
                }
            }
        }
    }
}
