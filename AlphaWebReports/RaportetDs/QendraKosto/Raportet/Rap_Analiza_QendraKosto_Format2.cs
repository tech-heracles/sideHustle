using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;


namespace AlphaWebReports.RaportetDs.QendraKosto.Raportet
{
    public partial class Rap_Analiza_QendraKosto_Format2 : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_Analiza_QendraKosto_Format2(){InitializeComponent();} 
        double kredi=0.0;
        double debi = 0.0;
        double kredipara = 0.0;
        double debipara = 0.0;
        double gjendjallogari = 0.0;
        public Rap_Analiza_QendraKosto_Format2(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi,param.ScopeID, report)
        {

        }
        public Rap_Analiza_QendraKosto_Format2(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, string scopeID, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            parameter1.Value = raport.Parameters[0].Value;
            parameter2.Value = raport.Parameters[1].Value;
            parameter3.Value = raport.Parameters[2].Value;
            parameter4.Value = raport.Parameters[3].Value;
            EmrateLabelave(ci);  
           
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel12.Text = rm.GetString("RaportiAnalizaQendresSeKostosFormat2", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);

            xrTableCell1.Text = rm.GetString("labelRaportiLloji", ci);
            xrTableCell3.Text = rm.GetString("labelDate", ci);
            xrTableCell4.Text = rm.GetString("labelPershkrimDok", ci);
            xrTableCell7.Text = rm.GetString("labelVleraMonQK", ci);
            xrTableCell15.Text = rm.GetString("labelRaportiDebi", ci);
            xrTableCell16.Text = rm.GetString("labelRaportiKredi", ci);
            xrTableCell8.Text = rm.GetString("labelKursi", ci);
            xrTableCell9.Text = rm.GetString("labelVleftaNeMonBaze", ci);

            xrLabel15.Text = rm.GetString("labelZeri", ci) + " 5:";
            xrLabel45.Text = rm.GetString("labelZeri", ci) + " 4:";
            xrLabel19.Text = rm.GetString("labelZeri", ci) + " 3:";
            xrLabel28.Text = rm.GetString("labelZeri", ci) + " 2:";
            xrLabel40.Text = rm.GetString("labelZeri", ci) + " 1:";

            xrLabel31.Text = rm.GetString("labelShumaLlogaria", ci) + ":";
            xrLabel34.Text = rm.GetString("labelTotaliPerZerin", ci) + " 1:";
            xrLabel37.Text = rm.GetString("labelTotaliPerZerin", ci) + " 2:";
            xrLabel38.Text = rm.GetString("labelTotaliPerZerin", ci) + " 3:";
            xrLabel39.Text = rm.GetString("labelTotaliPerZerin", ci) + " 4:";
            xrLabel43.Text = rm.GetString("labelTotaliPerZerin", ci) + " 5:";
            xrTableCell2.Text = rm.GetString("lblRaportiDokumentiNr", ci);
            xrLabel44.Text = rm.GetString("labelLogoIMB", ci);
            xrLabel68.Text = rm.GetString("lblRaportTotaliLlogarise", ci);

        }

        private void xrLabel71_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var parametraRaporti = AlphaWebReports.raporteUtil.DeserializoParametraPerKonstruktorRaporti(this.Extensions["parametraRaporti"]);


            if (GetCurrentColumnValue("idgjenerues") != null && GetCurrentColumnValue("IDKATEGORIA") != null )
            {
                switch (GetCurrentColumnValue("IDKATEGORIA").ToString())
                {
                    case "1":
                            xrLabel71.NavigateUrl = "javascript:window.myFaqeCelje.hapFaqeNeTabTeRi('Shto_RegjistrimDokumentash.aspx?scopeID=" + parametraRaporti.ScopeID + "&newScopeId=True&shitje_blerje=shitje&id=" + GetCurrentColumnValue("idgjenerues").ToString() + "&shtim_modifikim=modifikim')";
                            this.xrLabel71.ForeColor = System.Drawing.Color.SteelBlue;
                            xrLabel71.Target = "_self";
                        break;

                    case "2":
                            xrLabel71.NavigateUrl = "javascript:window.myFaqeCelje.hapFaqeNeTabTeRi('Shto_RegjistrimDokumentash.aspx?scopeID=" + parametraRaporti.ScopeID + "&newScopeId=True&shitje_blerje=blerje&id=" + GetCurrentColumnValue("idgjenerues").ToString() + "&shtim_modifikim=modifikim')";
                            this.xrLabel71.ForeColor = System.Drawing.Color.SteelBlue;
                            xrLabel71.Target = "_self";
                        break;

                    case "3":
                    case "4":
                        if (GetCurrentColumnValue("LlojiVeprimit") != null)
                        { 
                            xrLabel71.NavigateUrl = "javascript:window.myFaqeCelje.hapFaqeNeTabTeRi('ShtoVeprimBanka.aspx?scopeID=" + parametraRaporti.ScopeID + "&newScopeId=True&lloji=" + GetCurrentColumnValue("LlojiVeprimit").ToString().ToLower() + "&id=" + GetCurrentColumnValue("idgjenerues").ToString() + "&shtim_modifikim=modifikim')";
                            this.xrLabel71.ForeColor = System.Drawing.Color.SteelBlue;
                            xrLabel71.Target = "_self";
                        }
                        break;

                    case "5":
                            xrLabel71.NavigateUrl = "javascript:window.myFaqeCelje.hapFaqeNeTabTeRi('Shto_FleteKontabel.aspx?scopeID=" + parametraRaporti.ScopeID + "&newScopeId=True&shtim_modifikim=modifikim&id=" + GetCurrentColumnValue("idgjenerues").ToString() + "')";
                            this.xrLabel71.ForeColor = System.Drawing.Color.SteelBlue;
                            xrLabel71.Target = "_self";
                        break;

                    case "6":
                        string lloji = "";
                       
                         if(GetCurrentColumnValue("IdLlojDokumentiMagazine") != null )
                        { 
                        if (Convert.ToInt32(GetCurrentColumnValue("IdLlojDokumentiMagazine")) == 1)
                            lloji = "hyrje";
                        else lloji = "dalje";
                        
                            xrLabel71.NavigateUrl = "javascript:window.myFaqeCelje.hapFaqeNeTabTeRi('Shto_RegjistrimMagazine.aspx?scopeID=" + parametraRaporti.ScopeID + "&newScopeId=True&lloj=" + lloji + "&id=" + GetCurrentColumnValue("idgjenerues").ToString() + "&shtim_modifikim=modifikim')";
                            this.xrLabel71.ForeColor = System.Drawing.Color.SteelBlue;
                            xrLabel71.Target = "_self";

                        }
                        break;

                    case "7":
                            xrLabel71.NavigateUrl = "javascript:window.myFaqeCelje.hapFaqeNeTabTeRi('Shto_ShperndarjeShpenzimesh.aspx?scopeID=" + parametraRaporti.ScopeID + "&newScopeId=True&shtim_modifikim=modifikim&id=" + GetCurrentColumnValue("idgjenerues").ToString() + "')";
                            this.xrLabel71.ForeColor = System.Drawing.Color.SteelBlue;
                            xrLabel71.Target = "_self";
                        break;

                    case "8":
                        lloji = "";
                        if(GetCurrentColumnValue("ImportExport") != null)
                        { 
                        if (Convert.ToInt32(GetCurrentColumnValue("ImportExport")) == 1)
                            lloji = "import";
                        else
                            lloji = "export";
                            xrLabel71.NavigateUrl = "javascript:window.myFaqeCelje.hapFaqeNeTabTeRi('Shto_FleteDoganore.aspx?scopeID=" + parametraRaporti.ScopeID + "&newScopeId=True&lloji=" + lloji + "&id=" + GetCurrentColumnValue("idgjenerues").ToString() + "&shtim_modifikim=modifikim')";
                            this.xrLabel71.ForeColor = System.Drawing.Color.SteelBlue;
                            xrLabel71.Target = "_self";
                        }
                        break;

                    case "10":
                            xrLabel71.NavigateUrl = "javascript:window.myFaqeCelje.hapFaqeNeTabTeRi('LidhjaDokumentave.aspx?scopeID=" + parametraRaporti.ScopeID + "&newScopeId=True&shtim_modifikim=modifikim&id=" + GetCurrentColumnValue("idgjenerues").ToString() + "')";
                            this.xrLabel71.ForeColor = System.Drawing.Color.SteelBlue;
                            xrLabel71.Target = "_self";
                        break;

                    case "11":
                            xrLabel71.NavigateUrl = "javascript:window.myFaqeCelje.hapFaqeNeTabTeRi('Shto_AzhornimKlientFurnitor.aspx?scopeID=" + parametraRaporti.ScopeID + "&newScopeId=True&vep=azhornim&id=" + GetCurrentColumnValue("idgjenerues").ToString() + "&shtim_modifikim=modifikim')";
                            this.xrLabel71.ForeColor = System.Drawing.Color.SteelBlue;
                            xrLabel71.Target = "_self";
                        break;

                    case "20":
                            xrLabel71.NavigateUrl = "javascript:window.myFaqeCelje.hapFaqeNeTabTeRi('Shto_VeprimeKF.aspx?scopeID=" + parametraRaporti.ScopeID + "&newScopeId=True&shtim_modifikim=modifikim&id=" + GetCurrentColumnValue("idgjenerues").ToString() + "')";
                            this.xrLabel71.ForeColor = System.Drawing.Color.SteelBlue;
                            xrLabel71.Target = "_self";
                        break;

                    case "38":
                            xrLabel71.NavigateUrl = "javascript:window.myFaqeCelje.hapFaqeNeTabTeRi('Shto_ListPagesa.aspx?scopeID=" + parametraRaporti.ScopeID + "&newScopeId=True&shtim_modifikim=modifikim&id=" + GetCurrentColumnValue("idgjenerues").ToString() + "')";
                            this.xrLabel71.ForeColor = System.Drawing.Color.SteelBlue;
                            xrLabel71.Target = "_self";
                        break;

                    case "45":
                            xrLabel71.NavigateUrl = "javascript:window.myFaqeCelje.hapFaqeNeTabTeRi('Shto_Ekzekutim.aspx?scopeID=" + parametraRaporti.ScopeID + "&newScopeId=True&shtim_modifikim=modifikim&id=" + GetCurrentColumnValue("idgjenerues").ToString() + "')";
                            this.xrLabel71.ForeColor = System.Drawing.Color.SteelBlue;
                            xrLabel71.Target = "_self";
                        break;

                    case "64":
                            xrLabel71.NavigateUrl = "javascript:window.myFaqeCelje.hapFaqeNeTabTeRi('Shto_AzhornimKlientFurnitor.aspx?scopeID=" + parametraRaporti.ScopeID + "&newScopeId=True&vep=mbyllje&id=" + GetCurrentColumnValue("idgjenerues").ToString() + "&shtim_modifikim=modifikim')";
                            this.xrLabel71.ForeColor = System.Drawing.Color.SteelBlue;
                            xrLabel71.Target = "_self";
                        break;

                    case "86":
                            xrLabel71.NavigateUrl = "javascript:window.myFaqeCelje.hapFaqeNeTabTeRi('Shto_RegjistrimAmortizimi.aspx?scopeID=" + parametraRaporti.ScopeID + "&newScopeId=True&shtim_modifikim=modifikim&id=" + GetCurrentColumnValue("idgjenerues").ToString() + "')";
                            this.xrLabel71.ForeColor = System.Drawing.Color.SteelBlue;
                            xrLabel71.Target = "_self";
                        break;

                    case "95":
                            xrLabel71.NavigateUrl = "javascript:window.myFaqeCelje.hapFaqeNeTabTeRi('Shto_RegjistrimNdryshimCmimSasi.aspx?scopeID=" + parametraRaporti.ScopeID + "&newScopeId=True&shtim_modifikim=modifikim&id=" + GetCurrentColumnValue("idgjenerues").ToString() + "')";
                            this.xrLabel71.ForeColor = System.Drawing.Color.SteelBlue;
                            xrLabel71.Target = "_self";
                        break;

                    case "90":
                        

                        string llojiam = "amortizim";
                        if(GetCurrentColumnValue("KODINIVELIT") != null)
                        { 
                        if (GetCurrentColumnValue("KODINIVELIT").ToString() == "RIAM")
                            llojiam = "rivleresim";
                       
                            xrLabel71.NavigateUrl = "javascript:window.myFaqeCelje.hapFaqeNeTabTeRi('Shto_RivleresimeAmortizimi.aspx?scopeID=" + parametraRaporti.ScopeID + "&newScopeId=True&lloj=" + llojiam + "&id=" + GetCurrentColumnValue("idgjenerues").ToString() + "&shtim_modifikim=modifikim')";
                            this.xrLabel71.ForeColor = System.Drawing.Color.SteelBlue;
                            xrLabel71.Target = "_self";
                        }
                        break;
                }
            }
        }

        private void xrLabel21_AfterPrint(object sender, EventArgs e)
        {
            if (GetCurrentColumnValue("Debi") != null && GetCurrentColumnValue("Debi").ToString() != "")
            {   debi += double.Parse(GetCurrentColumnValue("Debi").ToString()) ;
                    
                }
            }

        private void xrLabel22_AfterPrint(object sender, EventArgs e)
        {
         if (GetCurrentColumnValue("kredi") != null  && GetCurrentColumnValue("kredi").ToString() != "")
            {   kredi += double.Parse(GetCurrentColumnValue("kredi").ToString()) ;
                    
                }
            }

        private void xrLabel73_AfterPrint(object sender, EventArgs e)
        {
            if (GetCurrentColumnValue("debipara") != null && GetCurrentColumnValue("debipara").ToString() != "")
            { 
            if (GetCurrentColumnValue("debipara").ToString() != null && GetCurrentColumnValue("debipara").ToString() != "")
            {   debipara = double.Parse(GetCurrentColumnValue("debipara").ToString()) ;
                    
                }
            }
        }

        private void xrLabel74_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void xrLabel33_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            gjendjallogari += kredipara + kredi - debi - debipara;
            xrLabel33.Text = String.Format("{0:#,#.00}", kredipara + kredi - debi - debipara);
        }

        private void xrLabel74_AfterPrint(object sender, EventArgs e)
        {
            if (GetCurrentColumnValue("kredipara") != null )
            {
                if (GetCurrentColumnValue("kredipara").ToString() != null && GetCurrentColumnValue("kredipara").ToString() != "")
                {
                    kredipara = double.Parse(GetCurrentColumnValue("kredipara").ToString());

                }
            }
        }

        private void GroupFooter1_AfterPrint(object sender, EventArgs e)
        {
            kredipara = 0.0;
            debipara = 0.0;
            kredi = 0.0;
            debi = 0.0;
        }

    }
}
