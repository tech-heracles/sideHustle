using System;
using System.Globalization;
using System.Resources;
using DevExpress.XtraReports.UI;

namespace AlphaWebReports.RaportetDs
{
    public partial class Rap_kontKartelaLlogarive_Format2_new : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_kontKartelaLlogarive_Format2_new(){InitializeComponent();} 
        double vleraProgresive = 0;
        double vleraProgresive2 = 0;
        double vleraProgresivGjithsej = 0;
        double vleraProgresivGjithsej2 = 0;
        double gjendjamepare = 0;
        double shumadebi = 0;
        double shumakredi = 0;
        double shumakredimonllog = 0;
        double shumadebimonllog = 0;
        double shumadebigjithsej = 0;
        double shumakredigjithsej = 0;
        double shumadebimonlloggjithsej = 0;
        double shumakredimonlloggjithsej = 0;
        int cnt = 0;
        

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("NRREFERENCEKOKAFLETEKONTABEL") == null || GetCurrentColumnValue("NRREFERENCEKOKAFLETEKONTABEL").ToString() == "")
                e.Cancel = true;
            else
                cnt++;
        }

        public Rap_kontKartelaLlogarive_Format2_new(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, param.ScopeID, report)
        {

        }
        public Rap_kontKartelaLlogarive_Format2_new(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi,string scopeID, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            xrLabel54.Text = raport.Parameters[0].Description;
            parameter1.Value = raport.Parameters[0].Value;
            xrLabel55.Text = raport.Parameters[1].Description;
            parameter2.Value = raport.Parameters[1].Value;
            xrLabel56.Text = raport.Parameters[2].Description;
            parameter3.Value = raport.Parameters[2].Value;
            xrLabel57.Text = raport.Parameters[3].Description;
            parameter4.Value = raport.Parameters[3].Value;
            xrLabel58.Text = raport.Parameters[4].Description;
            parameter5.Value = raport.Parameters[4].Value;
            xrLabel59.Text = raport.Parameters[5].Description;
            parameter6.Value = raport.Parameters[5].Value;
            xrLabel60.Text = raport.Parameters[6].Description;
            parameter7.Value = raport.Parameters[6].Value;
            xrLabel42.Text = raport.Parameters[7].Description;
            parameter8.Value = raport.Parameters[7].Value;
            azhornimLabel.Text = raport.Parameters[8].Description;
            Azhornim.Value = raport.Parameters[8].Value;
            xrLabel69.Text = raport.Parameters[9].Description;
            parameter9.Value = raport.Parameters[9].Value;

            vendosFormatNumrash( xrLabel36, xrLabel37, xrLabel39, xrLabel40, xrLabel67, xrLabel66, xrLabel34, xrLabel33, xrLabel90, xrLabel91);
        }

        private void vendosFormatNumrash(params XRLabel[] labels )
        {
            foreach (XRLabel label in labels)
            {
                label.XlsxFormatString = "#,##0.00";
            }
        }
        
        private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            cnt = 0;
            vleraProgresive = 0;
        }

        private void xrLabel36_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            vleraProgresivGjithsej += vleraProgresive;
            if (vleraProgresive >= 0)
            {
                xrLabel36.Text = String.Format("{0:n2}", vleraProgresive);
                vleraProgresive = 0;
            }
        }

        private void xrLabel37_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (vleraProgresive < 0)
            {
                xrLabel37.Text = String.Format("{0:n2}", Math.Abs(vleraProgresive));
                vleraProgresive = 0;
            }
        }


        private void xrLabel39_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            vleraProgresivGjithsej2 += vleraProgresive2;
            if (vleraProgresive2 > 0)
            {
                xrLabel39.Text = String.Format("{0:n2}", vleraProgresive2);
                vleraProgresive2 = 0;
            }
        }

        private void xrLabel40_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (vleraProgresive2 < 0)
            {
                xrLabel40.Text = String.Format("{0:n2}", Math.Abs(vleraProgresive2));
                vleraProgresive2 = 0;
            }            
        }

        private void xrLabel29_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = shumadebi;
            e.Handled = true;
        }

        private void xrLabel29_SummaryReset(object sender, EventArgs e)
        {
            shumadebi = 0;

        }

        private void xrLabel29_SummaryRowChanged(object sender, EventArgs e)
        {
            if (GetCurrentColumnValue("gjendjambartuar") != null)
            {
                if (GetCurrentColumnValue("gjendjambartuar").ToString() == "1")
                {
                    gjendjamepare = Convert.ToDouble(GetCurrentColumnValue("VLEFTADEBILLOGARIKONTABILITETI").ToString()) - Convert.ToDouble(GetCurrentColumnValue("VLEFTAKREDILLOGARIKONTABILITETI").ToString());
                    if (gjendjamepare > 0)
                    {
                        shumadebi += gjendjamepare;
                        shumadebigjithsej += gjendjamepare;
                    }
                }
                else
                {
                    shumadebi += Convert.ToDouble(GetCurrentColumnValue("VLEFTADEBILLOGARIKONTABILITETI").ToString());
                    shumadebigjithsej += Convert.ToDouble(GetCurrentColumnValue("VLEFTADEBILLOGARIKONTABILITETI").ToString());
                }
            }
        }

        private void xrLabel30_SummaryReset(object sender, EventArgs e)
        {
            shumakredi = 0;
        }

        private void xrLabel30_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = shumakredi;
            e.Handled = true;
        }

        private void xrLabel30_SummaryRowChanged(object sender, EventArgs e)
        {
            if (GetCurrentColumnValue("gjendjambartuar") != null)
            {
                if (GetCurrentColumnValue("gjendjambartuar").ToString() == "1")
                {
                    gjendjamepare = Convert.ToDouble(GetCurrentColumnValue("VLEFTADEBILLOGARIKONTABILITETI").ToString()) - Convert.ToDouble(GetCurrentColumnValue("VLEFTAKREDILLOGARIKONTABILITETI").ToString());
                    if (gjendjamepare < 0)
                    {
                        shumakredi += Math.Abs(gjendjamepare);
                        shumakredigjithsej += Math.Abs(gjendjamepare); ;
                    }
                }
                else
                {
                    shumakredi += Convert.ToDouble(GetCurrentColumnValue("VLEFTAKREDILLOGARIKONTABILITETI").ToString());
                    shumakredigjithsej += Convert.ToDouble(GetCurrentColumnValue("VLEFTAKREDILLOGARIKONTABILITETI").ToString());
                }
            }
        }

        private void xrLabel31_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = shumadebimonllog;
            e.Handled = true;
        }

        private void xrLabel31_SummaryReset(object sender, EventArgs e)
        {
            shumadebimonllog = 0;
        }

        private void xrLabel31_SummaryRowChanged(object sender, EventArgs e)
        {
            if (GetCurrentColumnValue("gjendjambartuar") != null)
            {
                if (GetCurrentColumnValue("gjendjambartuar").ToString() == "1")
                {
                    gjendjamepare = Convert.ToDouble(GetCurrentColumnValue("VLEFTADEBIMONEDHEHUAJ").ToString()) - Convert.ToDouble(GetCurrentColumnValue("VLEFTAKREDIMONEDHEHUAJ").ToString());
                    if (gjendjamepare > 0)
                    {
                        shumadebimonllog += gjendjamepare;
                        shumadebimonlloggjithsej += gjendjamepare;
                    }
                }
                else
                {
                    shumadebimonllog += Convert.ToDouble(GetCurrentColumnValue("VLEFTADEBIMONEDHEHUAJ").ToString());
                    shumadebimonlloggjithsej += Convert.ToDouble(GetCurrentColumnValue("VLEFTADEBIMONEDHEHUAJ").ToString());
                }
            }
        }

        private void xrLabel32_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = shumakredimonllog;
            e.Handled = true;
        }

        private void xrLabel32_SummaryReset(object sender, EventArgs e)
        {
            shumakredimonllog = 0;
        }

        private void xrLabel32_SummaryRowChanged(object sender, EventArgs e)
        {
            if (GetCurrentColumnValue("gjendjambartuar") != null)
            {
                if (GetCurrentColumnValue("gjendjambartuar").ToString() == "1")
                {
                    gjendjamepare = Convert.ToDouble(GetCurrentColumnValue("VLEFTADEBIMONEDHEHUAJ").ToString()) - Convert.ToDouble(GetCurrentColumnValue("VLEFTAKREDIMONEDHEHUAJ").ToString());
                    if (gjendjamepare < 0)
                    {
                        shumakredimonllog += Math.Abs(gjendjamepare);
                        shumakredimonlloggjithsej += Math.Abs(gjendjamepare); ;
                    }
                }
                else
                {
                    shumakredimonllog += Convert.ToDouble(GetCurrentColumnValue("VLEFTAKREDIMONEDHEHUAJ").ToString());
                    shumakredimonlloggjithsej += Convert.ToDouble(GetCurrentColumnValue("VLEFTAKREDIMONEDHEHUAJ").ToString());
                }
            }
        }

        private void xrLabel49_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = shumadebigjithsej;
            e.Handled = true;
        }

        private void xrLabel50_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = shumakredigjithsej;
            e.Handled = true;
        }

        private void xrLabel66_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (vleraProgresivGjithsej >= 0)
            {
                xrLabel66.Text = String.Format("{0:#,#.00}", vleraProgresivGjithsej);
            }
            
        }

        private void xrLabel67_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (vleraProgresivGjithsej < 0)
            {
                //vleraProgresivGjithsej = vleraProgresivGjithsej * (-1);
                xrLabel67.Text = String.Format("{0:#,#.00}", Math.Abs(vleraProgresivGjithsej));
            }
           
        }

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //if (this.PrintingSystem.Document.PageCount == 0)
            //{
            //    e.Cancel = true;
            //    return;
            //}
            
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                      System.Reflection.Assembly.Load("App_GlobalResources"));


            xrLabel17.Text = rm.GetString("RaportKartelaLlogariveTitulliformat2", ci);
            xrLabel23.Text = rm.GetString("RaportKartelaLlogariveTitulliformat2", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel18.Text = rm.GetString("labelNrRef", ci);
            xrLabel11.Text = rm.GetString("FilterDateRegjistrimi", ci);
            xrLabel20.Text = rm.GetString("labelRaportiLloji", ci);
            xrLabel14.Text = rm.GetString("labelRaportiNrDok", ci);
            xrLabel21.Text = rm.GetString("labelRaportiDtDok", ci);
            xrLabel22.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrLabel12.Text = rm.GetString("labelRaportiKredi", ci);
            xrLabel13.Text = rm.GetString("labelRaportiDebi", ci);
            xrLabel15.Text = rm.GetString("labelRaportiGjendMonBaze", ci);
            xrLabel10.Text = rm.GetString("labelRaportiGjendMonLlog", ci);
            xrLabel19.Text = rm.GetString("labelRaportiDebi", ci);
            xrLabel24.Text = rm.GetString("labelRaportiKredi", ci);
            xrLabel51.Text = rm.GetString("labelRaportiGjendjaMePare", ci);
            xrLabel28.Text = rm.GetString("lblLevizja", ci);
            xrLabel38.Text = rm.GetString("labelFilterAvancuarGjendja", ci);
            xrLabel35.Text = rm.GetString("lblLevizjaGjithsej", ci);
            xrLabel65.Text = rm.GetString("labelRaportiGjendjaGjithsej", ci);
            xrLabel64.Text = rm.GetString("labelLogoIMB", ci);

            //report header
            xrLabel71.Text = rm.GetString("labelNrRef", ci);
            xrLabel80.Text = rm.GetString("FilterDateRegjistrimi", ci);
            xrLabel82.Text = rm.GetString("labelRaportiLloji", ci);
            xrLabel72.Text = rm.GetString("labelRaportiNrDok", ci);
            xrLabel76.Text = rm.GetString("labelRaportiDtDok", ci);
            xrLabel77.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrLabel79.Text = rm.GetString("labelRaportiKredi", ci);
            xrLabel73.Text = rm.GetString("labelRaportiDebi", ci);
            xrLabel78.Text = rm.GetString("labelRaportiGjendMonBaze", ci);
            xrLabel81.Text = rm.GetString("labelRaportiGjendMonLlog", ci);
            xrLabel74.Text = rm.GetString("labelRaportiDebi", ci);
            xrLabel75.Text = rm.GetString("labelRaportiKredi", ci);
            xrLabel89.Text = rm.GetString("labelRaportiShuma", ci);
        }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
        }


        private void xrLabel49_SummaryReset(object sender, EventArgs e)
        {
            shumadebigjithsej = 0;
        }

        private void xrLabel50_SummaryReset(object sender, EventArgs e)
        {
            shumakredigjithsej = 0;
        }

        private void xrLabel33_BeforePrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrLabel33.Text = String.Format("{0:#,#.00}", (shumadebi + Convert.ToDouble(GetCurrentColumnValue("Gjendjameparedebi"))));
        }

        private void xrLabel34_BeforePrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrLabel34.Text = String.Format("{0:#,#.00}", (shumakredi + Convert.ToDouble(GetCurrentColumnValue("Gjendjameparekredi"))));
        }

        private void xrLabel90_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrLabel90.Text = String.Format("{0:#,#.00}", (shumadebimonllog + Convert.ToDouble(GetCurrentColumnValue("Gjendjameparedebihuaj"))));
        }

        private void xrLabel91_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrLabel91.Text = String.Format("{0:#,#.00}", (shumakredimonllog + Convert.ToDouble(GetCurrentColumnValue("Gjendjameparekredihuaj"))));
        }

        private void xrTableCell3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            if (GetCurrentColumnValue("VLEFTADEBIMONEDHEHUAJ") != null && GetCurrentColumnValue("VLEFTAKREDIMONEDHEHUAJ").ToString() != "")
            {

                vleraProgresive2 = vleraProgresive2 + Convert.ToDouble(GetCurrentColumnValue("VLEFTADEBIMONEDHEHUAJ").ToString()) - Convert.ToDouble(GetCurrentColumnValue("VLEFTAKREDIMONEDHEHUAJ").ToString());

            }
        }

        private void xrTableCell12_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("VLEFTADEBILLOGARIKONTABILITETI") != null && GetCurrentColumnValue("VLEFTAKREDILLOGARIKONTABILITETI").ToString() != "")
            {

                vleraProgresive = vleraProgresive + Convert.ToDouble(GetCurrentColumnValue("VLEFTADEBILLOGARIKONTABILITETI").ToString()) - Convert.ToDouble(GetCurrentColumnValue("VLEFTAKREDILLOGARIKONTABILITETI").ToString());
            }
        }

        private void xrTableCell2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("KODKONFIGAMBJENTE") != null && GetCurrentColumnValue("KODKONFIGAMBJENTE").ToString() == "NKM")
            {
                xrTableCell2.NavigateUrl = "";
                this.xrTableCell2.ForeColor = System.Drawing.Color.Black;
                return;
            }
            var parametraRaporti = AlphaWebReports.raporteUtil.DeserializoParametraPerKonstruktorRaporti(this.Extensions["parametraRaporti"]);
            xrTableCell2.NavigateUrl = "javascript:window.myFaqeCelje.hapFaqeNeTabTeRi('Shto_FleteKontabel.aspx?scopeID=" + parametraRaporti.ScopeID + "&newScopeId=True&shtim_modifikim=modifikim&id=" + GetCurrentColumnValue("IDKOKAFLETEKONTABEL") + "&numur=" + GetCurrentColumnValue("NRKOKAFLETEKONTABEL") + "')";
                this.xrTableCell2.ForeColor = System.Drawing.Color.SteelBlue;
                xrTableCell2.Target = "_self";
            
        }

        private void xrLabel46_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("vleftadebipare") != null && GetCurrentColumnValue("vleftakredipare") != null
                && !String.IsNullOrEmpty(GetCurrentColumnValue("vleftadebipare").ToString()) && !String.IsNullOrEmpty(GetCurrentColumnValue("vleftakredipare").ToString()))
                //if (cnt == 0)
                vleraProgresive = vleraProgresive + Convert.ToDouble(GetCurrentColumnValue("vleftadebipare").ToString()) - Convert.ToDouble(GetCurrentColumnValue("vleftakredipare").ToString());
        }

        private void xrLabel47_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("vleftadebimonhuajpare") != null && GetCurrentColumnValue("vleftakredimonhuajpare") != null 
                && !String.IsNullOrEmpty(GetCurrentColumnValue("vleftadebimonhuajpare").ToString()) && !String.IsNullOrEmpty(GetCurrentColumnValue("vleftakredimonhuajpare").ToString()))
                //if (cnt == 0)
                vleraProgresive2 = vleraProgresive2 + Convert.ToDouble(GetCurrentColumnValue("vleftadebimonhuajpare").ToString()) - Convert.ToDouble(GetCurrentColumnValue("vleftakredimonhuajpare").ToString());
        }

        #region komentuar

        //private void ReportFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        //{

        //}

        //private void xrLabel66_SummaryReset(object sender, EventArgs e)
        //{
        //    vleraProgresivGjithsej = 0;
        //}

        //private void xrLabel67_SummaryReset(object sender, EventArgs e)
        //{

        //    vleraProgresivGjithsej = 0;
        //}

        //private void xrLabel29_AfterPrint(object sender, EventArgs e)
        //{
        //    //if (GetCurrentColumnValue("VLEFTADEBIMONEDHEHUAJ") != null)
        //    //    shumadebi = Convert.ToDouble(xrLabel29.Text);
        //}

        //private void xrLabel30_AfterPrint(object sender, EventArgs e)
        //{
        //    //if (GetCurrentColumnValue("VLEFTADEBIMONEDHEHUAJ") != null)
        //    //    shumakredi = Convert.ToDouble(xrLabel30.Text);
        //}

        //private void xrLabel6_AfterPrint(object sender, EventArgs e)
        //{
        //    cnt = 0;
        //    //double shumadebi = 0;
        //    //double shumakredi = 0;
        //    //double shumakredimonllog = 0;
        //    //double shumadebimonllog = 0;
        //}

        //private void xrLabel31_AfterPrint(object sender, EventArgs e)
        //{
        //    //if (GetCurrentColumnValue("VLEFTADEBIMONEDHEHUAJ") != null)
        //    //    shumadebimonllog = Convert.ToDouble(xrLabel31.Text);
        //}

        //private void xrLabel32_AfterPrint(object sender, EventArgs e)
        //{
        //    //if (GetCurrentColumnValue("VLEFTADEBIMONEDHEHUAJ") != null)
        //    //    shumakredimonllog = Convert.ToDouble(xrLabel32.Text);
        //}

        #region po u deshen te perdoret llogaritShumeNgaFushaGrupuar 

        //private void xrLabel36_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        //{
        //    double sum = 0;
        //    for (int i = 0; i < e.CalculatedValues.Count; i++)
        //        sum += Convert.ToDouble(e.CalculatedValues[i]);
        //    if (sum > 0)
        //        e.Result = sum;
        //    e.Handled = true;
        //}

        //private void xrLabel37_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        //{
        //    double sum = 0;
        //    for (int i = 0; i < e.CalculatedValues.Count; i++)
        //        sum += Convert.ToDouble(e.CalculatedValues[i]);
        //    if (sum > 0)
        //        e.Result = sum;
        //    e.Handled = true;
        //}

        //private void xrLabel39_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        //{
        //    double sum = 0;
        //    for (int i = 0; i < e.CalculatedValues.Count; i++)
        //        sum += Convert.ToDouble(e.CalculatedValues[i]);
        //    if (sum > 0)
        //        e.Result = sum;
        //    e.Handled = true;
        //}

        //private void xrLabel40_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        //{
        //    double sum = 0;
        //    for (int i = 0; i < e.CalculatedValues.Count; i++)
        //        sum += Convert.ToDouble(e.CalculatedValues[i]);
        //    if (sum > 0)
        //        e.Result = sum;
        //    e.Handled = true;
        //}

        private void llogaritShumeNgaFushaGrupuar(object sender, SummaryGetResultEventArgs e)
        {
            double sum = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                sum += Convert.ToDouble(e.CalculatedValues[i]);
            if (sum > 0)
                e.Result = sum;
            e.Handled = true;
        }
        #endregion


        //private void xrLabel66_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        //{
        //    //double sum = 0;
        //    //for (int i = 0; i < e.CalculatedValues.Count; i++)
        //    //    sum += Convert.ToDouble(e.CalculatedValues[i]);
        //    //if (sum > 0)
        //    //    e.Result = sum;
        //    //e.Handled = true;
        //    e.Result = vleraProgresivGjithsej;
        //    e.Handled = true;
        //}

        //private void xrLabel67_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        //{
        //    //double sum = 0;
        //    //for (int i = 0; i < e.CalculatedValues.Count; i++)
        //    //    sum += Convert.ToDouble(e.CalculatedValues[i]);
        //    //if (sum > 0)
        //    //    e.Result = sum;
        //    //e.Handled = true;
        //    vleraProgresivGjithsej = vleraProgresivGjithsej * (-1);
        //    e.Result = vleraProgresivGjithsej;
        //    e.Handled = true;
        //}

        //private void xrLabel33_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        //{

        //}

        //private void xrLabel4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        //{
        //    //vleraProgresive = 0;
        //    //vleraProgresive2 = 0;
        //    //gjendjamepare = 0;
        //}

        //komentuar anxhela
        //private void xrLabel34_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        //{
        //    if (GetCurrentColumnValue("VLEFTADEBIMONEDHEHUAJ") != null && GetCurrentColumnValue("VLEFTAKREDIMONEDHEHUAJ").ToString() != "")
        //    {
        //        if (cnt == 0)
        //            vleraProgresive2 = vleraProgresive2 + Convert.ToDouble(GetCurrentColumnValue("vleftadebimonhuajpare").ToString()) - Convert.ToDouble(GetCurrentColumnValue("vleftakredimonhuajpare").ToString());
        //        vleraProgresive2 = vleraProgresive2 + Convert.ToDouble(GetCurrentColumnValue("VLEFTADEBIMONEDHEHUAJ").ToString()) - Convert.ToDouble(GetCurrentColumnValue("VLEFTAKREDIMONEDHEHUAJ").ToString());
        //        //   xrLabel34.Text = String.Format("{0:#,#.00}", vleraProgresive2);
        //    }
        //}



        //private void xrLabel9_BeforePrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
        //{
        //    if (GetCurrentColumnValue("VLEFTADEBILLOGARIKONTABILITETI") != null && GetCurrentColumnValue("VLEFTAKREDILLOGARIKONTABILITETI").ToString() != "")
        //    {
        //        if (cnt == 0)
        //            vleraProgresive = vleraProgresive + Convert.ToDouble(GetCurrentColumnValue("vleftadebipare").ToString()) - Convert.ToDouble(GetCurrentColumnValue("vleftakredipare").ToString());
        //        vleraProgresive = vleraProgresive + Convert.ToDouble(GetCurrentColumnValue("VLEFTADEBILLOGARIKONTABILITETI").ToString()) - Convert.ToDouble(GetCurrentColumnValue("VLEFTAKREDILLOGARIKONTABILITETI").ToString());
        //        //  xrLabel33.Text = String.Format("{0:#,#.00}", vleraProgresive);
        //    }
        //}


        #endregion

        private void GroupFooter1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrLabel36.Text = "";
            xrLabel37.Text = "";
            xrLabel39.Text = "";
            xrLabel40.Text = "";
        }

        private void Rap_kontKartelaLlogarive_Format2_new_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBox2.Image = AlphaWebReports.raporteUtil.MerrLogoNdermarrje(this.Extensions["ndermarrjeLogo"]);
        }
    }
}
