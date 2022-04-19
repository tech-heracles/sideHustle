using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Web;
using System.Drawing;
using DevExpress.XtraReports.Parameters;
using System;
using DbCore.IMBUtils.Extensions;
using System.Collections.Generic;
using DbCore.DbShare;
using System.IO;
using Newtonsoft.Json;
using LiquidEngine.Tools;
using AlphaWebReports.Common;
using System.Globalization;

namespace AlphaWebReports
{
    public class raporteUtil
    {
        public static string ArkivaPath;
        private static Func<int, int, List<(string EmerBanka, string NrLlogariBanka)>> _getBanka;



        public static void Initialize(Func<int, int, List<(string EmerBanka, string NrLlogariBanka)>> getBanka)
        {
            _getBanka = getBanka;
        }
        public static void shtoFiltra(XRTable filtraTable, XRTable kokaTableReportHeader, ParameterCollection parametrat)
        {
            if (parametrat == null)
                throw new Exception("Koleksioni i parametrave eshte null");
                
            shtoFiltra(filtraTable, kokaTableReportHeader, parametrat.ToColParameter());
        }
        public static void shtoFiltra(XRTable filtraTable, XRTable kokaTableReportHeader, List<clsParameter> parametrat)
        {
            if (parametrat == null)
                throw new Exception("Koleksioni i parametrave eshte null");
                
            int rowInd = 0, cellInd = 0;
            int nrQelizash = filtraTable.Rows[0].Cells.Count;

            if (nrQelizash % 2 == 1)
                throw new Exception("Tabela e filtrave duhet te kete numer cift qelizash");

            XRTableRow row = filtraTable.Rows[0];
            foreach (XRTableCell cell in row.Controls)
                cell.Text = string.Empty;
            filtraTable.BeginInit();
            filtraTable.Rows.Clear();
            filtraTable.Rows.Add(row);
            foreach (var param in parametrat)
            {
                if (string.IsNullOrEmpty(param.Vlera) || string.IsNullOrEmpty(param.Pershkrimi))
                    continue;
                if (cellInd == nrQelizash)
                {
                    filtraTable.InsertRowBelow(filtraTable.Rows[rowInd++]);
                    cellInd = 0;
                }
                filtraTable.Rows[rowInd].Cells[cellInd++].Text = param.Pershkrimi;
                filtraTable.Rows[rowInd].Cells[cellInd++].Text = param.Vlera;
            }
            filtraTable.AdjustSize();
            filtraTable.EndInit();
            if (kokaTableReportHeader != null)
            {
                kokaTableReportHeader.TopF = filtraTable.BottomF;
            }
        }
        public static XRTableCell shtoQelizeNeTabele(float width, string name, string text, Font font, Color borderColor, Color BackColor, DevExpress.XtraPrinting.TextAlignment textAlignment, DevExpress.XtraPrinting.BorderSide borders, DevExpress.XtraPrinting.PaddingInfo padding)
        {
            XRTableCell cell = new XRTableCell();
            cell.WidthF = width;
            cell.Name = name;
            cell.Font = font;
            cell.Text = text;
            cell.Padding = padding;
            cell.TextAlignment = textAlignment;
            cell.Borders = borders;
            cell.BorderColor = borderColor;
            cell.BackColor = BackColor;
            return cell;


        }
        /// <summary>
        /// Hap detajet e raportit ne onclick te butonit + (te raportet e pasqyrave financiare)
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        /// <param name="raporti">Raporti</param>
        /// <param name="reportV">Report Viewer</param>
        public static void HapRaportDetails<T>(object source, DevExpress.Web.CallbackEventArgsBase e, XtraReport raporti, T reportV)
        {
            if (e.Parameter == "")
                return;
            XtraReport xrReport = new XtraReport();
            switch (e.Parameter.Split(';')[2]){
                case "bilanci":
                case "pasqyreKonsoliduarGjendjeFinanciare":
                case "bilanciQendraKosto":
                case "burimeShpenzimeInvestime":
                case "pasqyraLevizjeFondesh":
                case "pasqyraLevizjesNeCash":
                case "gjendjeNdryshimeAktiveTeQendrueshem":
                case "pasqyraLevizjeveAQT":
                case "pasqyraAmortizimeve":
                case "pasqyraAmortizimitAsete":
                case "teardhuraKesh":
                case "ardhura":
                case "Detail":
                case "bilanciOjf":
                case "pashOjf":
                case "cashFlowOjf":
                case "ardhura_shpenzime_sipas_muajve":
                case "teArdhuraShpenzimeQendraKosto":
                case "cashFlow":
                case "cashFlowQendraKosto":
                case "pasqyreEKonsoliduarTeArdhuraShpenzime":
                case "analizeArdhuraShpenzime":
                case "pasqyrePermbledheseFurnitore":
                case "pasqyrePermbledheseDebitore":
                case "RapPasqyraNdryshimeveNeAktivetNetoFondetNeto":
                case "GjendjaDheNdryshimetEAktiveveAfatgjataVleraNeto":
                    IUpdateDetailKPF reportKpf = (IUpdateDetailKPF)raporti;//Rap_BilanciBuxh,Rap_Bilanci,Rap_BilanciKESH,Rap_BilanciiKontabelFormat2
                    if (e.Parameter.Split(';')[1] == "Detail")
                        reportKpf.UpdateDetail(e.Parameter.Split(';')[0]);
                    else
                        reportKpf.UpdateDetailKPF(e.Parameter.Split(';')[0]);
                    xrReport = (XtraReport)reportKpf;
                    break;
                case "dokumentaKonvertuar":
                case "ekzekutimRezervimeshFormat2":
                case "konvertimeMagazine":
                    IUpdateDetailMeDyId reportMeDyId = (IUpdateDetailMeDyId)raporti;
                    reportMeDyId.UpdateDetail(e.Parameter.Split(';')[0], e.Parameter.Split(';')[1]);
                    xrReport = (XtraReport)reportMeDyId;
                    break;
                case "gjendjaLlogarive":
                case "permbledheseSipasGrupeve":
                case "permbledheseGrupetNivelRaportues":
                case "permbledhesAseteNivelRaportues":
                case "gjendjaPermbledhurArkesNivelRaportues":
                case "konsumPermbledhesBurimeshSkedulim":
                case "gjendjeKerkeseArtikujshPerProdhim":
                case "marzhiShitjeveMagazina":
                case "MaturimiStokutPerArtikujtMeSeriale":
                case "MarzhiShitjeve_sipasAgjenteve":
                case "ndjekjaECiklitTeKonvertimeve":
                case "RezultatiQendraveTeKostosMeZera":
                case "Rap_BuxhetimiSipasShpenzimeve":
                case "Rap_BuxhetimiSipasUrdherPagesave":
                case "gjendjepermbledhurArtikujshNdermarje":
                case "artikujTeShiturDegeAdministrative":
                case "artikujShiturDegeAdm":
                case "artikujTeBlereDegeAdministrative":
                case "RialokimidheEkzekutimiBuxhetitQeveritar":
                case "StrukturaOrganizative":
                case "RaportiPermbledheseRealizimevedheParashikimeveTeArdhuraShpenzime":
                    IUpdateDetail reportDetail1 = (IUpdateDetail)raporti;
                    reportDetail1.UpdateDetail(e.Parameter.Split(';')[0]);
                    xrReport = (XtraReport)reportDetail1;
                    break;
                //case "buxhetimeSipasUrdherPagesave":
                //    if (e.Parameter.Split(';')[1] == "UpdateDetail")
                //    {
                //        IUpdateDetailMeDyId reportDetailMeDyId = (IUpdateDetailMeDyId)raporti;
                //        reportDetailMeDyId.UpdateDetail(e.Parameter.Split(';')[0], e.Parameter.Split(';')[1]);
                //        xrReport = (XtraReport)reportDetailMeDyId;
                //    }
                //    break;
                case "bilanciEnergjitikPermbledhes":
                case "RapEvidenceBuxheti":
                    if (e.Parameter.Split(';')[1] == "Detail")
                    {
                        IUpdateDetail reportDetail2 = (IUpdateDetail)raporti;
                        reportDetail2.UpdateDetail(e.Parameter.Split(';')[0]);
                        xrReport = (XtraReport)reportDetail2;
                    }
                    break;
            }

            if(reportV.GetType() == typeof(ASPxWebDocumentViewer))
            {
                CachedReportSourceWeb cachedReport = new CachedReportSourceWeb(xrReport);
                cachedReport.CreateDocument();
                (reportV as ASPxWebDocumentViewer).OpenReport(cachedReport);
            }
            else
            {
                xrReport.CreateDocument();
                (reportV as ReportViewer).Report = xrReport;
            }
        }
       
        public static void vendosFormatinEFushesSeRaportit(string formati, string formatiNeExcel, bool vendosDheSummary, params XRControl[] controls)
        {
            foreach (XRControl control in controls)
            {
                if (control.DataBindings.Count > 0)
                    control.DataBindings[0].FormatString = formati;
                control.XlsxFormatString = formatiNeExcel;
                if (!vendosDheSummary) continue;
                if (control.GetType() == typeof(XRTableCell))
                {
                    ((XRTableCell)control).Summary.FormatString = formati;
                    ((XRTableCell)control).TextFormatString = formati;
                }
                else if (control.GetType() == typeof(XRLabel))
                {
                    ((XRLabel)control).Summary.FormatString = formati;
                    ((XRTableCell)control).TextFormatString = formati;
                }

            }

        }
        public static void vendosFormatinEFushesSeRaportit(string formati, string formatiNeExcel, bool vendosDheSummary, XRControlCollection controls)
        {
            vendosFormatinEFushesSeRaportit(formati, formatiNeExcel, vendosDheSummary, ConvertCollectionToArray(controls));
        }
        public static XRControl[] ConvertCollectionToArray(XRControlCollection controls)
        {
            XRControl[] array = new XRControl[controls.Count];
            int i = 0;
            foreach (XRControl control in controls)
                array[i++] = control;
            return array;
        }

        public static void shfaqLlogariteBankare(int idNdermarrje, int idPerdoruesi, XRPanel xrPanel1, GroupBand GroupFooter1, float distancaLartesi, float koordinataY, 
            float koordinataX, float koordinataLlogariX, string familyName, float emSize, FontStyle style, Color color, 
            float widthEmer, float heightEmer , float widthnrLlogari, float heightnrLlogari, int llojGrupimi = 0,
            DevExpress.XtraPrinting.BorderSide Borders = DevExpress.XtraPrinting.BorderSide.None)
        {
            var bankat = _getBanka(idNdermarrje, idPerdoruesi);
            
            for (int i = 0; i < bankat.Count; i++)
            {
                if (bankat[i].NrLlogariBanka == "")
                    continue;
                XRLabel emerBanke = new XRLabel();
                emerBanke.LocationFloat = new DevExpress.Utils.PointFloat(koordinataX, koordinataY);
                emerBanke.Font = new System.Drawing.Font(familyName, emSize, style);
                emerBanke.ForeColor = color;
                emerBanke.Name = "emerBanke" + i + DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second + "_" + DateTime.Now.Millisecond;
                emerBanke.Borders = Borders;
                emerBanke.SizeF = new System.Drawing.SizeF(widthEmer, heightEmer);
                emerBanke.Text = bankat[i].EmerBanka + ":";
                XRLabel nrLlogariBanke = new XRLabel();
                nrLlogariBanke.LocationFloat = new DevExpress.Utils.PointFloat(koordinataLlogariX, koordinataY);
                nrLlogariBanke.Font = new System.Drawing.Font(familyName, emSize);
                nrLlogariBanke.Borders = Borders;
                nrLlogariBanke.ForeColor = color;
                nrLlogariBanke.Name = "nrLlogariBanke" + i + DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second + "_" + DateTime.Now.Millisecond;
                nrLlogariBanke.SizeF = new System.Drawing.SizeF(widthnrLlogari, heightnrLlogari);
                nrLlogariBanke.Text = bankat[i].NrLlogariBanka;
               switch (llojGrupimi)
                {
                    case 1:
                        xrPanel1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] { emerBanke, nrLlogariBanke });
                        break;
                    case 2:
                        GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] { emerBanke, nrLlogariBanke });
                        break;
                    default:                    
                        break; 
                }
                koordinataY += distancaLartesi;
            }
        }

        public static string MerrDateSipasFormatitOseEmptyString(string data, string formati)
        {
            DateTime dateTime;
            return DateTime.TryParse(data, out dateTime) ? dateTime.ToString(formati) : string.Empty;

        }
        public static string kthePathArkiva(string pathRelativ)
        {
            ArkivaPath = ArkivaPath.Replace("\\Arkiva", "");
            if (pathRelativ.Substring(0, 1) == "~")
                pathRelativ = pathRelativ.Substring(1, pathRelativ.Length -1);
            string pathAb = $"{ArkivaPath}{pathRelativ}";
            return pathAb;
        }
        public static void InitializeArkivaPath(string path)
        {
            ArkivaPath = path;
        }

        public static void BashkangjitImazh(XRPictureBox pictureBox, string pathRelativ)
        {
            string path = kthePathArkiva(Convert.ToString(pathRelativ));
            Bitmap initBitmap = new Bitmap(path);
            Bitmap bitMap = new Bitmap(initBitmap);
            initBitmap.Dispose();
            initBitmap = null;
            pictureBox.Image = bitMap;
        }
        public static bool KontrolloEkzistencImazhi(string pathRelativ)
        {
            string path = kthePathArkiva(Convert.ToString(pathRelativ));
            if (File.Exists(path) && IsRecognisedImageFile(pathRelativ))
                return true;
            else
                return false;
        }
        public static bool IsRecognisedImageFile(string fileName)
        {
            string targetExtension = System.IO.Path.GetExtension(fileName);
            if (String.IsNullOrEmpty(targetExtension))
                return false;
            else
                targetExtension = "*" + targetExtension.ToLowerInvariant();

            List<string> recognisedImageExtensions = new List<string>();

            foreach (System.Drawing.Imaging.ImageCodecInfo imageCodec in System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders())
                recognisedImageExtensions.AddRange(imageCodec.FilenameExtension.ToLowerInvariant().Split(";".ToCharArray()));

            foreach (string extension in recognisedImageExtensions)
            {
                if (extension.Equals(targetExtension))
                {
                    return true;
                }
            }
            return false;

        }

        /// <summary>
        /// Therritet nga designs te raporteve per deserializimin e parametrave (filtrave)
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static List<clsParameter> DeserializoParametrat(string param)
        {
            return Deserializo<List<clsParameter>>(param);
        }

        public static ParametraRaporti DeserializoParametraPerKonstruktorRaporti(string param)
        {
            return Deserializo<ParametraRaporti>(param);
        }

        public static T Deserializo<T>(string param)
        {
            return JsonConvert.DeserializeObject<T>(param);
        }

        public static Image MerrLogoNdermarrje(string param)
        {
            try
            {
                Image logo = Image.FromStream(new MemoryTributary(JsonConvert.DeserializeObject<byte[]>(param)));
                return logo;
            }
            catch
            {
                return null;
            }   
        }

        /// <summary>
        /// Perdoret ne designet e raporteve qe hapin ambiente (label-i duhet qe te kete Target="_self")
        /// </summary>
        /// <param name="llojDok"></param>
        /// <param name="idDokumenti"></param>
        /// <param name="nrDokumenti"></param>
        /// <param name="scopeID"></param>
        /// <returns></returns>
        public static string ktheUrlDokumentiRaportKlientFurnitor(string llojDok, string idDokumenti, string nrDokumenti, string scopeID)
        {
            string url = "";
            switch (llojDok)
            {
                case "FSH":
                case "USH":
                        url = "javascript:window.myFaqeCelje.hapFaqeNeTabTeRi('Shto_RegjistrimDokumentash.aspx?scopeID=" + scopeID + "&newScopeId=True&shitje_blerje=shitje&id=" + idDokumenti + "&numer=" + nrDokumenti + "&shtim_modifikim=modifikim')";
                    break;

                case "FB":
                        url = "javascript:window.myFaqeCelje.hapFaqeNeTabTeRi('Shto_RegjistrimDokumentash.aspx?scopeID=" + scopeID + "&newScopeId=True&shitje_blerje=blerje&id=" + idDokumenti + "&numer=" + nrDokumenti + "&shtim_modifikim=modifikim')";
                    break;

                case "DERDHJE":
                        url = "javascript:window.myFaqeCelje.hapFaqeNeTabTeRi('ShtoVeprimBanka.aspx?scopeID=" + scopeID + "&newScopeId=True&lloji=derdhje&id=" + idDokumenti + "&numer=" + nrDokumenti + "&shtim_modifikim=modifikim')";
                    break;

                case "TERHEQJE":
                        url = "javascript:window.myFaqeCelje.hapFaqeNeTabTeRi('ShtoVeprimBanka.aspx?scopeID=" + scopeID + "&newScopeId=True&lloji=terheqje&id=" + idDokumenti + "&numer=" + nrDokumenti + "&shtim_modifikim=modifikim')";
                    break;

                case "ARKETIM":
                        url = "javascript:window.myFaqeCelje.hapFaqeNeTabTeRi('ShtoVeprimBanka.aspx?scopeID=" + scopeID + "&newScopeId=True&lloji=arketim&id=" + idDokumenti + "&numer=" + nrDokumenti + "&shtim_modifikim=modifikim')";
                    break;

                case "PAGESE":
                        url = "javascript:window.myFaqeCelje.hapFaqeNeTabTeRi('ShtoVeprimBanka.aspx?scopeID=" + scopeID + "&newScopeId=True&lloji=pagese&id=" + idDokumenti + "&numer=" + nrDokumenti + "&shtim_modifikim=modifikim')";
                    break;

                case "VKF":
                case "ND":
                        url = "javascript:window.myFaqeCelje.hapFaqeNeTabTeRi('Shto_VeprimeKF.aspx?scopeID=" + scopeID + "&newScopeId=True&shtim_modifikim=modifikim&id=" + idDokumenti + "&numer=" + nrDokumenti + "')";
                    break;

                case "DL":
                        url = "javascript:window.myFaqeCelje.hapFaqeNeTabTeRi('LidhjaDokumentave.aspx?scopeID=" + scopeID + "&newScopeId=True&shtim_modifikim=modifikim&id=" + idDokumenti + "&numer=" + nrDokumenti + "')";
                    break;
                //voo
                case "hyrje":
                case "dalje":
                        url = String.Format("javascript:window.myFaqeCelje.hapFaqeNeTabTeRi('Shto_RegjistrimMagazine.aspx?scopeID=" + scopeID + "&newScopeId=True&lloj={0}&id={1}&numer={2}&shtim_modifikim=modifikim')", llojDok, idDokumenti, nrDokumenti);

                    break;

                case "MKF":
                        url = String.Format("javascript:window.myFaqeCelje.hapFaqeNeTabTeRi('Shto_AzhornimKlientFurnitor.aspx?scopeID=" + scopeID + "&newScopeId=True&vep=mbyllje&lloj={0}&id={1}&numer={2}&shtim_modifikim=modifikim')", llojDok, idDokumenti, nrDokumenti);
                    break;

                case "AKF":
                        url = String.Format("javascript:window.myFaqeCelje.hapFaqeNeTabTeRi('Shto_AzhornimKlientFurnitor.aspx?scopeID=" + scopeID + "&newScopeId=True&vep=azhornim&lloj={0}&id={1}&numer={2}&shtim_modifikim=modifikim')", llojDok, idDokumenti, nrDokumenti);
                    break;

                default:
                    break;
            }
            return url;
        }

        public static string ktheVlereFiltriTipGrafiku(string TipGrafiku, CultureInfo ci)
        {
             System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (TipGrafiku == rm.GetString("cmbboxRaportiVija", ci))
                  return TipGrafiku = "Line";
            else if (TipGrafiku == rm.GetString("cmbboxRaportiVija", ci) + " 3D")
                return  TipGrafiku = "Line3D";
            else if (TipGrafiku == rm.GetString("cmbboxRaportiSiperfaqe", ci))
                return  TipGrafiku = "Area";
            else if (TipGrafiku == rm.GetString("cmbboxRaportiSiperfaqe", ci) + " 3D")
                return  TipGrafiku = "Area3D";
            else if (TipGrafiku == rm.GetString("cmbboxRaportiKolona", ci))
                return  TipGrafiku = "Bar";
            else return TipGrafiku = "Bar3D";
        }


}

    }
