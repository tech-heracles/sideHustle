using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using AlphaWebReports;
using AlphaWebReports.RaportetDs;
using CacheLayer;
using DbCore;
using DbCore.DbAdmin;
using DbCore.DbShare;
using DbCore.IMBUtils.Logging;
using DbCore.IMBUtils.Types;
using DbCore.Raporte;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Web;
using LiquidEngine.Tools;
using Newtonsoft.Json;
using PlatinumWeb.ApplicationUtils.Pages;
using Web.Framework.WebUtils.Pages;

namespace PlatinumWeb
{
    public partial class RaportiShpejte : MyReportPageBase
    {
        //private Int32 idRaporti = -1;
        private static string styleNamePrefix = "Style_";
        //private static string styleNameDefault = "Default";
        private static string styleNamePostfix = ".repss";
        protected void Page_Load(object sender, EventArgs e)
        {
            HfState = hfState;
            IdRaporti = DbCore.clsFunksione.ktheIdRaporti(Request);
            List<Dictionary<string, string>> teDhenaRap = new List<Dictionary<string, string>>();
            clsPerdorues perdoruesi = new clsPerdorues(IdPerdoruesi);

            ReportObject = new clsRaporti();
            if (!String.IsNullOrEmpty(Request.QueryString["emriReal"])) ReportObject = new clsRaporti(IdGjuha, Request.QueryString["emriReal"]);
            else if (IdRaporti > 0)
                ReportObject = new clsRaporti(IdGjuha, IdRaporti);
            RaportiEmerReal = ReportObject.RaportiEmriReal;

            if (!Page.IsPostBack)
            {
                hfState.Set("idNdermarrje", IdNdermarrja);
                hfState.Set("idPerdoruesi", IdPerdoruesi);
                IdReportDesign = Request.QueryString["iddesign"] != null ? int.Parse(Request.QueryString["iddesign"]) : 0;

                SetReport();
                ReportOrientation = clsRaportDesign._rap_landscape;
                GuidString = Request.QueryString["guidString"];
                if (GuidString == null || GuidString == "undefined")
                    GuidString = Guid.NewGuid().ToString();

                hfState.Set("guidString", GuidString);

                teDhenaRap = MerrTeDhenaRapNgaSesioni(false, IdReportDesign, RaportiEmerReal);
                string idDoks = string.Join(",", teDhenaRap.Select(x => x["IdKoka"]));

                if (Request.QueryString["printo"] != "false")
                {
                    DbCore.DbAdmin.clsKomponente oKomponente = new DbCore.DbAdmin.clsKomponente("RaportiShpejte.aspx");
                    DbCore.DbAdmin.clsLogu logu = new DbCore.DbAdmin.clsLogu(0, oKomponente.IdKomponente, IdNdermarrja, IdPerdoruesi, DateTime.Now, idDoks, ReportObject.IdRaporti, DbCore.mySessionObjects.merrRuajLog(Session));
                }

                EnableStyle = ReportObject.StilEnabled;
                SetReportStyles();
                string stilRaporti = Request["reportStyle"];
                ReportStyle = (stilRaporti != "" && stilRaporti != null) ? stilRaporti : perdoruesi.StilRaportiFileName;

                SetDesigns(out clsRaportDesign rapDes);
                afisho(rapDes.IdRaportDesign, ReportObject.IdRaporti, GuidString, teDhenaRap);
                base.SetReportToolbarTranslations();
            }
            else
                SetReport();
        }

        private List<Dictionary<string, string>> MerrTeDhenaRapNgaSesioni(bool changedDesign, int idDesign, string raportiEmerReal)
        {
            return clsFunksione.merrTeDhenaRaportiPerHapjeRaportiTeShpejte(raportiEmerReal, changedDesign, Request.QueryString["idDokumenti"], idDesign, Session);
        }

        protected void afisho(int idDesign, int idRaporti, String guidString, List<Dictionary<string, string>> teDhenaRap)
        {
            XtraReport report = new XtraReport();
            string orientim = Request.QueryString["Orientimi"];
            bool azhornim = false;
            int idVit = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            if (!string.IsNullOrEmpty(orientim))
                ReportOrientation = orientim;
            if (Request.QueryString["db"] == "jo" || (Request.QueryString["printoDetyrim"] != null && Request.QueryString["printoDetyrim"] == "po"))  // hapja e raportit te gabimit te importit ose hapja e raportit te printimit te detyrimit
            {
                hapRaportGabimiOseDetyrimi(idRaporti, idDesign, guidString);
                return;
            }
            clsRaporti oRap = null;
            if (Request.QueryString["Sesioni"] == "true")// hapja e subraporteve
            {
                KontrolloTeDrejtaRaporti(idRaporti, IdPerdoruesi, IdNdermarrja, IdViti);
                report = krijoSubRaport(idRaporti, idDesign, guidString);
            }
            else
                if (Request.QueryString["kodfatura"] != null || Request.QueryString["idDokKonv"] != null || (Request.QueryString["kaFiltra"] != null && Request.QueryString["kaFiltra"] == "true"))// hapja e raportit te konvertimit te dokumentave nga lupa e konvertimeve
                report = ReportFunctions.krijoObjektRaporti("", IdGjuha, IdPerdoruesi, idVit, idRaporti, IdNdermarrja, krijoParametratSql(idRaporti), idDesign, ReportOrientation, guidString, Request.QueryString[ScopeManager.ScopeIdKey]);

            else if (teDhenaRap.Count <= 1)
            {
                // hapja e faturave
                report = ReportFunctions.krijoDesignRaportFature(Request.QueryString["wf"] != null ? Convert.ToInt16(Request.QueryString["wf"]) : -1, idDesign, IdPerdoruesi, IdNdermarrja, IdNdermarrjeVit, krijoParametratSql(idRaporti), ReportOrientation, guidString, Request.QueryString[ScopeManager.ScopeIdKey], IdGjuha, idRaporti);
            }

            if (idRaporti >= 0)
                hapRaportFaturashOseSubRaport(report, idRaporti, guidString, teDhenaRap);
        }

        private Ds_PrintimDetyrimesh ktheDsPerDetyrimet()
        {
            Ds_PrintimDetyrimesh ds = new Ds_PrintimDetyrimesh();
            Tuple<string, string, string, string, string, DataTable> faturaNgaBRM = mySessionObjects.MerrFaturatBRMngaSession(Session);
            DataTable dt = faturaNgaBRM.Item6;
            string nrfature = "";
            string periudhaFaturimit = "";
            decimal totaliMujor = 0;
            decimal totaliPerTuPaguar = decimal.Parse(faturaNgaBRM.Item5 ?? "0");

            string kyMuaj = string.Format("{0} {1}", clsFunksione.ktheMuaj(DateTime.Now.Month), DateTime.Now.Year);

            foreach (DataRow dr in dt.Rows)
            {
                object[] data = dr.ItemArray;
                if (kyMuaj.Equals(Convert.ToString(data[7]), StringComparison.InvariantCultureIgnoreCase))
                {
                    periudhaFaturimit = kyMuaj;
                    nrfature = Convert.ToString(data[3]);
                    totaliMujor = Convert.ToDecimal(data[11]);
                    continue;
                }
            }
            ds.detyrime.AdddetyrimeRow((totaliPerTuPaguar - totaliMujor), totaliMujor.ToString("n2"), faturaNgaBRM.Item2, DateTime.Now, nrfature, periudhaFaturimit, faturaNgaBRM.Item1, faturaNgaBRM.Item3, totaliPerTuPaguar.ToString("n2"));
            return ds;
        }

        private Ds_RapGabimesh ktheDsPerRaportGabimesh()
        {
            Ds_RapGabimesh ds = new Ds_RapGabimesh();
            DataTable dt = DbCore.mySessionObjects.merrTabeleGabimeshImporti(Session);
            foreach (DataRow dr in dt.Rows)
            {
                ds.Gabime.Rows.Add(dr[0], dr[1], dr[2]);  // per te marre parasysh rastet kur tabelat e ruajtura kane me shume se 3 kolona
            }
            return ds;
        }

        private colParameter krijoParametratSql(int idraporti)
        {
            clsRaporti rap = new clsRaporti(mySessionObjects.ktheGjuhe(Session), idraporti);

            colParameter parametraSp = new colParameter(rap.IdSp);
            for (int i = 0; i < parametraSp.Count; i++)
            {
                //vlera do i jepet oSp.OColSpTrupi[i].Vlera qe mban vleren qe do marri parametri qe do i kalohet SP-se
                string vlera = "";

                //************************************************************************************//
                //behet kontrolli nqs nuk eshte plotesuar filtri atehere do kalohet si bosh, pa vlere 
                if (parametraSp[i].Emri == "filterLlojDokumenti")
                    vlera = Request.QueryString["kodkonf"];
                else if (parametraSp[i].Emri == "filterNrDokKonvertuar")
                    vlera = Request.QueryString["kodfatura"];
                else if (parametraSp[i].Emri == "filterDtDokKonvertuar")
                    vlera = Request.QueryString["dtdok"];
                else if (parametraSp[i].Emri == "IdNdermarje")
                    vlera = new clsNdermarrje(IdNdermarrja).NdermarrjeKodi;
                else if (parametraSp[i].Emri == "filterIdDokKonvertuara")
                {
                    string idte = Request.QueryString["idDokKonv"];
                    idte = idte.Replace('-', ',');
                    vlera = idte;
                }
                else if (parametraSp[i].Emri == "filterArtGjendjeZero")
                {
                    vlera = Request.QueryString["artGjendjeZero"];
                }
                else if (parametraSp[i].Emri == "idpunonjes")
                {
                    if (!String.IsNullOrEmpty(Request.QueryString["idPunonjes"]))
                        vlera = Request.QueryString["idPunonjes"];
                }
                parametraSp[i].Vlera = vlera;
            }
            return parametraSp;
        }

        protected void ASPxCallbackPanel1_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
        {
            string parameter = Convert.ToString(e.Parameter);
            switch (parameter)
            {
                case "changeDesign":
                    ChangeDesignRaporti();
                    break;
                case "changeOrientation":
                    ChangeReportOrientation();
                    break;
                case "changeStyle":
                    ChangeReportStyle();
                    break;
            }
            //raporteUtil.HapRaportDetails(source, e, GetReport(hfState["guidString"].ToString()), rvRaporti);
        }

        private void ChangeReportOrientation()
        {
            AfishoRaport();
        }

        private void ChangeReportStyle()
        {
            AfishoRaport();
        }

        private void ChangeDesignRaporti()
        {
            clsRaportDesign rapDes = new clsRaportDesign(IdReportDesign);
            //ImbReportToolbar.BtnReportEditVisible = rapDes.IModifikueshem;
            SetOrientations(rapDes);
            AfishoRaport();
        }

        private void AfishoRaport()
        {
            List<Dictionary<string, string>> teDhenaRap = new List<Dictionary<string, string>>();
            teDhenaRap = MerrTeDhenaRapNgaSesioni(true, IdReportDesign, RaportiEmerReal);
            string guidString = hfState.Get("guidString").ToString();
            afisho(IdReportDesign, IdRaporti, guidString, teDhenaRap);
        }

        protected void rvRaporti_Unload(object sender, EventArgs e)
        {
            ((ASPxWebDocumentViewer)sender).OpenReport(new XtraReport());//((ReportViewer)sender).Report = null;
        }


        private XtraReport krijoSubRaport(int idRaporti, int idDesign, string guidString)
        {
            XtraReport report = new XtraReport();
            string emriReal = Request.QueryString["emriReal"];
            int idSubRaporti = Convert.ToInt32(Request.QueryString["idraporti"]);
            if (idSubRaporti == 0)
                idSubRaporti = clsRaporti.KtheIdRaportiSipasEmritReal(emriReal);

            colParameter sqlParamShfaqRaport = mySessionObjects.merrParametratShfaqSubRaportitNgaSesioni(Session, idSubRaporti, guidString);
            for (int i = 0; i < sqlParamShfaqRaport.Count; i++)
            {
                if (Request.QueryString[sqlParamShfaqRaport[i].Emri] != null)
                {
                    sqlParamShfaqRaport[i].Vlera = Convert.ToString(Request.QueryString[sqlParamShfaqRaport[i].Emri]);
                }
            }
            return ReportFunctions.krijoObjektRaporti("", IdGjuha, IdPerdoruesi, IdViti, idRaporti, IdNdermarrja, sqlParamShfaqRaport, idDesign, ReportOrientation, guidString, Request.QueryString[ScopeManager.ScopeIdKey]);
        }

        private void hapRaportGabimiOseDetyrimi(int idRaporti, int idDesign, string guidString)
        {
            XtraReport report;
            clsPerdorues perdoruesi = mySessionObjects.kthePerdorues(Session);
            clsRaportDesign oRaportDesign = null;
            string vjen = "";
            string emriReal = Request.QueryString["emriReal"];
            if (Request.QueryString["vjen"] != null)
                vjen = Request.QueryString["vjen"];

            if (!String.IsNullOrEmpty(emriReal))
                oRaportDesign = new clsRaportDesign(emriReal, IdNdermarrja);
            report = ReportFunctions.krijoObjektRaporti(vjen, IdGjuha, IdPerdoruesi, IdViti, string.IsNullOrEmpty(emriReal) ? idRaporti : oRaportDesign.IdRaporti, IdNdermarrja, null, string.IsNullOrEmpty(emriReal) ? idDesign : oRaportDesign.IdRaportDesign, ReportOrientation, guidString, Request.QueryString[ScopeManager.ScopeIdKey]);

            if (Request.QueryString["db"] == "jo")
                report.DataSource = ktheDsPerRaportGabimesh();
            else if (Request.QueryString["printoDetyrim"] != null && Request.QueryString["printoDetyrim"] == "po")
                report.DataSource = ktheDsPerDetyrimet();

            report.StyleSheet.LoadFromFile(NdertoPathStyleSheet(StylePath, ReportStyle));
            KonfigFleteRaporti(report, perdoruesi);

            bool RSU = colRolPerdorues.merrRoleSipasPerdoruesiDheKodRoli(mySessionObjects.ktheIdPerdoruesi(Session), "RSU") == -1;

            if (((System.Data.DataSet)report.DataSource).Tables[0].Rows.Count < 10000 || RSU)
                report.CreateDocument();

            if (RSU)
                DbCore.mySessionObjects.ruajReportNeSession(Session, guidString, report);

            CachedReportSourceWeb cachedReport = new CachedReportSourceWeb(report);
            rvRaporti.OpenReport(cachedReport);
        }

        private void hapRaportFaturashOseSubRaport(XtraReport report, int idRaporti, string guidString, List<Dictionary<string, string>> teDhenaRap)
        {
            shtoFaturaOseSubRaportNeRaport(idRaporti, report, guidString, teDhenaRap);
            report.StyleSheet.LoadFromFile(NdertoPathStyleSheet(StylePath, ReportStyle));
            report.PrintingSystem.ContinuousPageNumbering = false;

            //if (teDhenaRap.Count > 1)
            //{
            //    exportReportToPdf(report);
            //    return;
            //}

            if (teDhenaRap.Count <= 1)
            {
                hfState.Set("isMultiDesignReport", false);
                bool RSU = colRolPerdorues.merrRoleSipasPerdoruesiDheKodRoli(mySessionObjects.ktheIdPerdoruesi(Session), "RSU") == -1;

                if (((System.Data.DataSet)report.DataSource).Tables[0].Rows.Count < 10000 || RSU)
                    report.CreateDocument();

                if (RSU)
                    DbCore.mySessionObjects.ruajReportNeSession(Session, guidString, report);
            }
            else
                hfState.Set("isMultiDesignReport", true);

            CachedReportSourceWeb cachedReport = new CachedReportSourceWeb(report);
            rvRaporti.OpenReport(cachedReport);
        }

        private void exportReportToPdf(XtraReport report)
        {
            using (MemoryTributary ms = new MemoryTributary())
            {
                PdfExportOptions options = new PdfExportOptions();
                options.ConvertImagesToJpeg = true;
                options.ImageQuality = PdfJpegImageQuality.Lowest;
                report.ExportToPdf(ms, options);
                report.Dispose();
                Response.ClearContent();
                Response.ClearHeaders();
                Response.ContentType = "Application/pdf";
                Response.AddHeader("Content-Disposition", "attachment;filename=DokumentatPerPrintim.pdf");
                foreach (byte[] block in ms.Blocks)
                {
                    Response.BinaryWrite(block);
                    Response.Flush();
                }
                Response.CacheControl = "No-cache";
                Response.End();
            }
        }

        private void shtoFaturaOseSubRaportNeRaport(int idRaporti, XtraReport report, string guidString, List<Dictionary<string, string>> teDhenaRap)
        {
            var azhornim = false;
            if (report.Extensions["filtrat"] != null)
            {
                var parameters = JsonConvert.DeserializeObject<colParameter>(report.Extensions["filtrat"]);
                var paramAzh = parameters?.Find(x => x.Emri == "Azhornim");
                if (paramAzh != null && (paramAzh.Vlera == "Po" || paramAzh.Vlera == "Yes"))
                    azhornim = true;
            }

            foreach (var item in teDhenaRap)
            {
                var idDesignRap = string.IsNullOrEmpty(item["IdDesign"]) ? 0 : int.Parse(item["IdDesign"]);
                mySessionObjects.hiqObjectNeSesion(Session, $"clsReportRradhes_{idDesignRap}");
            }

            var dtmbarimi = DateTime.Today;
            var wfStatus = Request.QueryString["wf"] != null ? Convert.ToInt16(Request.QueryString["wf"]) : -1;
            var test = Request.QueryString["vjen"] == "testim";
            var idPeriudhaKontabel = clsPeriudhaKontabel.ktheIdPeriudheSipasDatesDheNdermarrjes(dtmbarimi, IdNdermarrja);
            var perdoruesi = new clsPerdorues(IdPerdoruesi);
            var idKonfigAmbjentiNkm = clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod("NKM", IdNdermarrja);
            //var oRap = new clsRaporti(IdGjuha, idRaporti);

            foreach (var item in teDhenaRap)
            {
                int.TryParse(item["IdKoka"], out var id);
                int idDesignRap = string.IsNullOrEmpty(item["IdDesign"]) ? 0 : int.Parse(item["IdDesign"]);

                if (teDhenaRap.Count > 1)
                {
                    var oRap = merrObjektRaporti(idDesignRap);

                    var colParams = report.Extensions["filtrat"] != null
                        ? JsonConvert.DeserializeObject<colParameter>(report.Extensions["filtrat"])
                        : null;

                    var raportiRradhes = ReportFunctions.krijoDesignRaportFature(wfStatus, idDesignRap, IdPerdoruesi, IdNdermarrja, IdNdermarrjeVit, colParams, ReportOrientation, guidString, Request.QueryString[ScopeManager.ScopeIdKey], IdGjuha, oRap.IdRaporti);

                    raportiRradhes = ReportFunctions.mbushRaportNgaDB(Request, Session, raportiRradhes, IdNdermarrja, IdPerdoruesi, azhornim, IdNdermarrjeVit, dtmbarimi, idKonfigAmbjentiNkm, idPeriudhaKontabel, guidString, id, idDesignRap, test, this.RaportiEmerReal, oRap);

                    KonfigFleteRaporti(raportiRradhes, perdoruesi);
                    raportiRradhes.CreateDocument();
                    var ps = ClonePrintingSystem(raportiRradhes.PrintingSystem);
                    report.Pages.AddRange(ps.Pages);
                    raportiRradhes.Dispose();
                }
                else
                {
                    var oRap = new clsRaporti(IdGjuha, idRaporti);
                    oRap.MbushParametra();
                    report = ReportFunctions.mbushRaportNgaDB(Request, Session, report, IdNdermarrja, IdPerdoruesi, azhornim,
                        IdNdermarrjeVit, dtmbarimi, idKonfigAmbjentiNkm, idPeriudhaKontabel, guidString, id,
                        idDesignRap, test, this.RaportiEmerReal, oRap);
                    KonfigFleteRaporti(report, perdoruesi);
                    return;
                }
            }
        }

        private clsRaporti merrObjektRaporti(int idDesignRap)
        {
            clsRaporti oRap = mySessionObjects.MerrNgaSession<clsRaporti>(Session, $"clsReportRradhes_{idDesignRap}");
            if (oRap == null)
            {
                oRap = new clsRaporti(IdGjuha, clsRaporti.KtheIdRaporti(IdGjuha, idDesignRap));
                oRap.MbushParametra();
                mySessionObjects.RuajNeSession(Session, oRap, $"clsReportRradhes_{idDesignRap}");
            }
            return oRap;
        }

        PrintingSystemBase ClonePrintingSystem(PrintingSystemBase source)
        {
            using (MemoryTributary stream = new MemoryTributary())
            {
                source.SaveDocument(stream);
                PrintingSystemBase clone = new PrintingSystemBase();
                clone.LoadDocument(stream);
                return clone;
            }
        }
    }
}