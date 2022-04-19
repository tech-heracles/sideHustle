using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using DevExpress.XtraPrinting;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_Fature_Tatimore_Shitje_Vod : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_Fature_Tatimore_Shitje_Vod()
        {
            InitializeComponent();
        }
        public Rap_Fature_Tatimore_Shitje_Vod(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_Fature_Tatimore_Shitje_Vod(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
        }

      
    int countPrint = 0;
    System.Data.DataTable dt;
    
        private void Rap_Fature_Tatimore_Shitje_Vod_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
            xrPictureBox1.Image = AlphaWebReports.raporteUtil.MerrLogoNdermarrje(this.Extensions["ndermarrjeLogo"]);
            /**/
            dt = ((System.Data.DataSet)(this.DataSource)).Tables[0];
            double totalimetvsh = 0.00;
            double totalipatvsh = 0.00;
            double totalTvsh = 0.00;
            double totali = 0.00;
            double totalipatvshkursi = 0;
            double totalimetvshkursi = 0;
            double furnizimeTePaTatueshme = 0;
            double totalVleftaPaTvshPerArtMeTvsh = 0;
            double totalVleftaPaTvshPerArtPaTvsh = 0;
            double furnizimeTeTatueshme = 0;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (Convert.ToInt32(dt.Rows[i]["grup1"]) == 1)
            {
                if (Convert.ToDouble(dt.Rows[i]["tvsh"]).ToString() != "" && Convert.ToDouble(dt.Rows[i]["tvsh"]) != 0)
                    totalVleftaPaTvshPerArtMeTvsh += Convert.ToDouble(dt.Rows[i]["VLEFTAPATVSH"]);
                else
                    totalVleftaPaTvshPerArtPaTvsh += Convert.ToDouble(dt.Rows[i]["VLEFTAPATVSH"]);
                totalimetvsh += Convert.ToDouble(dt.Rows[i]["VLEFTAMETVSH"]);
                totalTvsh = Convert.ToDouble(dt.Rows[i]["TVSHTOTAL"]);
                totalipatvsh += Convert.ToDouble(dt.Rows[i]["VLEFTAMETVSH"]);
                totali = Convert.ToDouble(dt.Rows[i]["TOTALI"]);
                if (Convert.ToDouble(dt.Rows[i]["TOTALI"]) != 0)
                    furnizimeTeTatueshme = totalVleftaPaTvshPerArtMeTvsh * (1 - (Convert.ToDouble(dt.Rows[i]["ZBRITJE"]) / Convert.ToDouble(dt.Rows[i]["TOTALI"])));
                else
                    furnizimeTeTatueshme = 0.00;
                if (Convert.ToDouble(dt.Rows[i]["TOTALI"]) != 0)
                    furnizimeTePaTatueshme = totalVleftaPaTvshPerArtPaTvsh * (1 - (Convert.ToDouble(dt.Rows[i]["ZBRITJE"]) / Convert.ToDouble(dt.Rows[i]["TOTALI"])));
                else
                    furnizimeTePaTatueshme = 0.00;

                if (dt.Rows.Count > 0)
                    setData(dt.Rows[0], totalimetvsh, totalipatvsh, totalTvsh, totali, furnizimeTeTatueshme, furnizimeTePaTatueshme);
                    shtoRreshtatNeTabele(xrTable2);
                ShtoRreshtaNeTabele2(xrTable12);

            }
        }


    }

    private void xrTable12_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        ShtoRreshtaNeTabele2(xrTable12);
    }


    private void ShtoRreshtaNeTabele2(XRTable xrTable12)
    {
        int i = 0;
        if (dt.Rows.Count > 0)
        {
            for (int j = xrTable12.Rows.Count-1; j >= 1; j--)
            {
                xrTable12.Rows.RemoveAt(j);
                  
                }
                Font arial = new Font("Times New Roman", 9.00F, FontStyle.Regular);
                PaddingInfo padding = new PaddingInfo(2, 2, 9, 0);
                BorderSide borders = ((BorderSide)((BorderSide.Left | BorderSide.Bottom)));
            BorderSide borderLast = ((BorderSide)((BorderSide.Left | BorderSide.Bottom | BorderSide.Right)));
                foreach (System.Data.DataRow row in dt.Rows)
            {
                   
                    if (Convert.ToInt32(row["grup1"]) == 2 && dt.Rows[i]["GR"].ToString() != "2" && dt.Rows[i]["GR"].ToString() != "3")
                {
                    if (row["GR"].ToString() == "2" || row["GR"].ToString() == "3")
                        continue;
                    
                    XRTableRow rr = new XRTableRow();
                    rr.HeightF = 20f;

                    XRTableCell nr = AlphaWebReports.raporteUtil.shtoQelizeNeTabele(32f, "", (i + 1).ToString(), arial, Color.Silver, Color.Transparent, TextAlignment.MiddleLeft, borders, padding);
                    rr.Cells.Add(nr);

                    XRTableCell nrdok = AlphaWebReports.raporteUtil.shtoQelizeNeTabele(70.89f, "", row["NRDOK"].ToString(), arial, Color.Silver, Color.Transparent, TextAlignment.MiddleLeft, borders, padding);
                        rr.Cells.Add(nrdok);

                    XRTableCell nrserial = AlphaWebReports.raporteUtil.shtoQelizeNeTabele(63.64f, "", row["NRSERIAL"].ToString(), arial, Color.Silver, Color.Transparent, TextAlignment.MiddleLeft, borders, padding);
                        rr.Cells.Add(nrserial);

                    XRTableCell kodmag = AlphaWebReports.raporteUtil.shtoQelizeNeTabele(66f, "", row["kodmagazina"].ToString(), arial, Color.Silver, Color.Transparent, TextAlignment.MiddleLeft, borders, padding);
                        rr.Cells.Add(kodmag);
                       

                        XRTableCell data = AlphaWebReports.raporteUtil.shtoQelizeNeTabele(92.33f, "", String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(row["DTDOK"])), arial, Color.Silver, Color.Transparent, TextAlignment.MiddleRight, borders, padding);
                        rr.Cells.Add(data);

                    XRTableCell kodifikimart = AlphaWebReports.raporteUtil.shtoQelizeNeTabele(78.53f, "", row["KODARTIKULLI"].ToString(), arial, Color.Silver, Color.Transparent, TextAlignment.MiddleLeft, borders, padding);
                        rr.Cells.Add(kodifikimart);


                    XRTableCell pershkrimart = AlphaWebReports.raporteUtil.shtoQelizeNeTabele(176f, "", row["PERSHKRIMARTIKULLI"].ToString(), arial, Color.Silver, Color.Transparent, TextAlignment.MiddleLeft, borders, padding);
                        rr.Cells.Add(pershkrimart);

                        XRTableCell kodKF = AlphaWebReports.raporteUtil.shtoQelizeNeTabele(81.14f, "", row["KODIKF"].ToString(), arial, Color.Silver, Color.Transparent, TextAlignment.MiddleLeft, borders, padding);
                        rr.Cells.Add(kodKF);

                    XRTableCell serialK = AlphaWebReports.raporteUtil.shtoQelizeNeTabele(105.83f, "", row["SERIALI_KRYESOR"].ToString(), arial, Color.Silver, Color.Transparent, TextAlignment.MiddleLeft, borders, padding);
                        rr.Cells.Add(serialK);

                    XRTableCell pr = AlphaWebReports.raporteUtil.shtoQelizeNeTabele(30.67f, "", row["PR"].ToString(), arial, Color.Silver, Color.Transparent, TextAlignment.MiddleLeft, borderLast, padding);
                        rr.Cells.Add(pr);
                        
                        xrTable12.Rows.Insert((i + 1), rr);
                    i++;
                }
            }
        }
    }
        
    private void shtoRreshtatNeTabele(XRTable xrTable2)
    {
        int i = 0;
        if (dt.Rows.Count > 0)
        {
            for (int j = xrTable2.Rows.Count - 1; j >= 1; j--)
            {
                xrTable2.Rows.RemoveAt(j);
            }
              
                Font arial = new Font("Times New Roman", 9.00F, FontStyle.Regular);
            PaddingInfo padding = new PaddingInfo(6, 6, 9, 0);
            BorderSide borders = ((BorderSide)((BorderSide.Top | BorderSide.Left | BorderSide.Bottom)));
            BorderSide borderLast = ((BorderSide)((BorderSide.Left | BorderSide.Bottom | BorderSide.Right)));
            foreach (System.Data.DataRow row in dt.Rows)
            {

                if (Convert.ToInt32(row["grup1"]) == 1 && dt.Rows[i]["GR"].ToString() != "2" && dt.Rows[i]["GR"].ToString() != "3")
                {
                    if (row["GR"].ToString() == "2" || row["GR"].ToString() == "3")
                       continue;
                    XRTableRow rr = new XRTableRow();
                   rr.HeightF = 52f;

                    XRTableCell nr = AlphaWebReports.raporteUtil.shtoQelizeNeTabele(14.5f, "", (i + 1).ToString(), arial, Color.Silver, Color.Transparent, TextAlignment.MiddleLeft, borders, padding);
                    rr.Cells.Add(nr);

                    XRTableCell pershkrimi = AlphaWebReports.raporteUtil.shtoQelizeNeTabele(111.6f, "", row["PERSHKRIMI"].ToString(), arial, Color.Silver, Color.Transparent,
                    TextAlignment.MiddleLeft, borders, padding);
                        rr.Cells.Add(pershkrimi);

                    XRTableCell njesia = AlphaWebReports.raporteUtil.shtoQelizeNeTabele(42.8f, "", row["PERSHKRIMNJESIA"].ToString(), arial, Color.Silver, Color.Transparent, TextAlignment.MiddleLeft, borders, padding);
                        rr.Cells.Add(njesia);

                    XRTableCell sasia = AlphaWebReports.raporteUtil.shtoQelizeNeTabele(27.87f, "", row["SASIA"].ToString(), arial, Color.Silver, Color.Transparent, TextAlignment.MiddleRight, borders, padding);
                        rr.Cells.Add(sasia);

                    XRTableCell cmimi = AlphaWebReports.raporteUtil.shtoQelizeNeTabele(48.73f, "", String.Format("{0:N2}", row["CMIMI"]), arial, Color.Silver, Color.Transparent, TextAlignment.MiddleRight, borders, padding);
                        rr.Cells.Add(cmimi);

                    XRTableCell vleraPaTvsh = AlphaWebReports.raporteUtil.shtoQelizeNeTabele(48.73f, "", String.Format("{0:N2}", row["VLEFTAPATVSH"]), arial, Color.Silver, Color.Transparent, TextAlignment.MiddleRight, borders, padding);
                        rr.Cells.Add(vleraPaTvsh);

                    XRTableCell VleftaETVSH = AlphaWebReports.raporteUtil.shtoQelizeNeTabele(48.73f, "", String.Format("{0:N2}", Convert.ToDouble(row["TVSH"]) * Convert.ToDouble(row["VLEFTAPATVSH"])), arial, Color.Silver, Color.Transparent, TextAlignment.MiddleRight, borders, padding);
                        rr.Cells.Add(VleftaETVSH);

                    XRTableCell VleftaMeTVSH = AlphaWebReports.raporteUtil.shtoQelizeNeTabele(48.73f, "", String.Format("{0:N2}", row["VLEFTAMETVSH"]), arial, Color.Silver, Color.Transparent, TextAlignment.MiddleRight, borderLast, padding);
                        rr.Cells.Add(VleftaMeTVSH);

                    xrTable2.Rows.Insert((i + 1), rr);
                    i++;
                }
            }
        }
    }

    private void setData(System.Data.DataRow row, double totalimetvsh, double totalipatvsh, double totalTvsh, double totali, double furnizimeTeTatueshme, double furnizimeTePaTatueshme)
    {
        double zbritje;
        zbritje = Convert.ToDouble(row["ZBRITJE"]);
        xrLabel67.Text = String.Format("{0:N2}", (totalimetvsh / 3 - zbritje));
        xrLabel66.Text = String.Format("{0:N2}", totalTvsh);
        xrLabel65.Text = String.Format("{0:N2}", (totalipatvsh / 3 - zbritje - totalTvsh));
        xrLabel44.Text = String.Format("{0:N2}", furnizimeTeTatueshme / 3);
        xrLabel78.Text = String.Format("{0:N2}", furnizimeTePaTatueshme / 3);
        xrLabel5.Text = String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(row["DTDOK"]));
        xrLabel6.Text = row["NRDOK"].ToString();
        xrLabel10.Text = row["EmerMbiemerPerdorues"].ToString();
        xrTableCell36.Text = row["transportuesi"].ToString();
        xrTableCell38.Text = row["transp_adresa"].ToString();
        xrTableCell40.Text = row["transp_Targa"].ToString();
        xrTableCell42.Text = DateTime.Now.ToString("HH:mm");
        xrTableCell44.Text = row["transp_nipt"].ToString();
        xrTableCell22.Text = row["NRSERIAL"].ToString();
        xrTableCell18.Text = row["NDERMARJEPERSHK"].ToString();
        xrTableCell32.Text = row["adresaMagazina"].ToString();
        xrTableCell67.Text = row["telMagazina"].ToString();
        xrTableCell24.Text = row["NDERMARJENIPT"].ToString();
        xrTableCell26.Text = row["EMERKLIENTI"].ToString();
        xrTableCell28.Text = row["ADRESAFATURIMIT"].ToString();
        xrTableCell30.Text = row["KONTAKTI"].ToString();
        xrTableCell34.Text = row["NIPTK"].ToString();
    }

       
    }
}
