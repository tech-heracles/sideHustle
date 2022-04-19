using DevExpress.XtraReports.UI;
using DevExpress.Web;
using DbCore.DbShare;
using DbCore.Raporte;
using DevExpress.XtraReports.Native;
using System;
using DbCore.DbAdmin;
using DbCore;
using DevExpress.XtraReports.Web;
using static DbCore.mySessionObjects;
using Newtonsoft.Json;
using System.Web;
using System.IO;
using System.Linq;
using DbCore.IMBUtils.Extensions;
using DbCore.IMBUtils.Logging;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class Designer : MyPageBase
    {
        private string guidString = "";
        private bool subRaport;
        private bool newDesign;
        private string emerRaporti;
        private int idDesignRaporti;
        private int idKonfigDokumenti;
        private int idGjuha;
        private string vjenNga;
        private string orientimi;

        private clsReportDesigner repDesigner = new clsReportDesigner();

        static Designer()
        {
            //  SerializationService.RegisterSerializer(CustomUntypedDataSetSerializer.Name, new CustomUntypedDataSetSerializer());
        }


        protected void Page_PreInit(object sender, EventArgs e)
        {
            base.Page_PreInit(sender, e);
            //perfshin automatikisht librarit qe nevojiten per designerin
            ASPxWebControl.GlobalEmbedRequiredClientLibraries = true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                idGjuha = ktheGjuhe(Session);
                KontrolloAutorizim();

                var query = HttpUtility.ParseQueryString(Request.Url.Query).ToDictionary();
                guidString = (string)query["key"];

                bool.TryParse(Request.QueryString["subRaport"], out subRaport);
                emerRaporti = MerrEmerRaporti(Session, guidString);
                int.TryParse(Request.QueryString["idRaportDesign"], out idDesignRaporti);
                int.TryParse(Request.QueryString["idKonfig"], out idKonfigDokumenti);
                vjenNga = Request.QueryString["vjenNga"];
                orientimi = Request.QueryString["orientimi"];
                bool.TryParse(Request.QueryString["RaportIPaImplementuar"], out newDesign);

                hfState.Set("emerRaporti", emerRaporti);
                hfState.Set("idNdermarrje", IdNdermarrja);
                hfState.Set("idPerdoruesi", IdPerdoruesi);
                hfState.Set("idKonfig", idKonfigDokumenti);
                hfState.Set("key", guidString);
                hfState.Set("subRaport", subRaport);
                hfState.Set("IdRaportDesign", idDesignRaporti);
                hfState.Set("vjenNga", vjenNga);
                hfState.Set("urlReferuesi", query["urlReferuesi"]);
                hfState.Set("orientimi", orientimi);
                hfState.Set("idGjuha", idGjuha);
                hfState.Set("newDesign", newDesign);
            }
            else
            {
                emerRaporti = hfState.Get("emerRaporti").ToString();
                idDesignRaporti = (int)hfState.Get("IdRaportDesign");
                idKonfigDokumenti = (int)hfState.Get("idKonfig");
                vjenNga = (string)hfState.Get("vjenNga");
                guidString = (string)hfState.Get("key");
                subRaport = (bool)hfState.Get("subRaport");
                orientimi = (string)hfState.Get("orientimi");
                idGjuha = (int)hfState.Get("idGjuha");
                newDesign = (bool)hfState.Get("newDesign");
            }

            InitDesignerPage(string.Empty);
        }
        
        private void KontrolloAutorizim()
        {
            var superuser = colRolPerdorues.merrRoleSipasPerdoruesiDheKodRoli(IdPerdoruesi, "RSU") == -1;
            if (!superuser) clsFunksione.logout(Session, true, "FaqePaautorizuar");
        }

        protected void ASPxReportDesigner1_SaveReportLayout(object sender, SaveReportLayoutEventArgs e)
        {
            try
            {
                var repDesigner = new clsRaportDesign(idDesignRaporti);
                var repSettings = clsRaportDesign.GetReportDesignSettings(idDesignRaporti);
                repDesigner.RuajNeSession = repSettings.ruajNeSession;
                repDesigner.AutoWidth = repSettings.autoWidth;
                repDesigner.RollPaper = repSettings.rollPaper;
                repDesigner.OldViewer = repSettings.oldViewer;

                var folder = $"{ DbCore.IMBUtils.DataBase.MyConnectionsManager.GetSelectedConNameServer()}/";

                if (!hfState.TryGet("saveMode", out var saveMode))
                    return;

                if (newDesign || MenuAction.Save.ToString().CompareTo(saveMode) == 0)
                {
                    emerRaporti = repDesigner.GetReportName(orientimi);
                    var repDes = new clsReportDesigner();
                    var rapi = repDes.SaveReportToFile(e.ReportLayout, emerRaporti, newDesign ? clsReportDesigner.RepxReportsFolder : folder);
                    RuajRaportNeSession(rapi);
                }
                else if (MenuAction.SaveAs.ToString().CompareTo(saveMode) == 0)
                {
                    hfState.TryGet("vendosDefault", out var vendosDefault);
                    var zgjedhurDefault = vendosDefault == bool.TrueString;

                    var pershkrimiNew = (string)hfState.Get("designName");

                    var raporti = repDesigner.SaveAs(pershkrimiNew, IdNdermarrja, orientimi, e.ReportLayout, zgjedhurDefault);
                    RuajRaportNeSession(raporti);
                }
                ShtoMesazhNeRaportDesigner(new MesazhSuksesi($"Modifikimi ruajt me sukses!"));
            }
            catch (MyException myEx)
            {
                ShtoMesazhNeRaportDesigner(new MesazhGabimi(myEx.Message));
                ImbLogger.Error(myEx);
            }
            catch (Exception ex)
            {
                ImbLogger.Error(ex);
                ShtoMesazhNeRaportDesigner(new MesazhGabimi($"Deshtoi ruajtja ne db e dizajnit me emer {emerRaporti}! kontrolloni loget per me teper detaje!"));
            }
        }

        private void ShtoMesazhNeRaportDesigner(clsMesazh mesazhi)
        {
            reportDesigner.JSProperties["cpMesazhi"] = JsonConvert.SerializeObject(mesazhi);
        }

        /// <summary>
        /// ruan ne session raportin e krijuar
        /// </summary>
        /// <param name="rep"></param>
        private void RuajRaportNeSession(XtraReport rep)
        {
            if (subRaport)
                ruajReportNeSession(Session, guidString, rep);
            else
                ruajMyReportNeSession(Session, guidString, rep);
        }

        protected void reportDesigner_Unload(object sender, EventArgs e)
        {
            //behet me qellim qe te loadohet raporti i ri
            reportDesigner.ShouldDisposeReport = false;
        }

        private void InitDesignerPage(string disajniOrigjinal)
        {
            var report = new XtraReport();
            if (!newDesign)
                report = subRaport ? merrReportNgaSessioni<XtraReport>(Session, guidString) : merrMyReportNgaSessioni<XtraReport>(Session, guidString);
            else
                KonfiguroRaportPerHereTePare(report, disajniOrigjinal);

            if (!newDesign && report == null)
            {
                throw new MyException("Nuk u gjet nje raport i vlefshem ne session,Provoni te hapni edhe njehere raportin dhe me pas shtypni butonin Edit!");
            }

            var parametraRaporti = merrParametraRaporti(Session, guidString);
            repDesigner.KonfiguroDataSourceRaporti(report, parametraRaporti);
            reportDesigner.OpenReport(report);
            InitExtensions(report);
        }

        private void KonfiguroRaportPerHereTePare(XtraReport report, string disajniOrigjinal)
        {
            if (!string.IsNullOrEmpty(disajniOrigjinal))
                NgarkoDisajn(report, disajniOrigjinal);

            report.DataMember = Request.QueryString["SpEmri"];
            var stil = ReportStyle.GetReportStyles(idGjuha).ElementAt(0);
            report.StyleSheet.LoadFromFile(System.IO.Path.Combine(HttpContext.Current.Server.MapPath(null), "Style Raporte", stil.FileName));
        }

        private void NgarkoDisajn(XtraReport report, string designName)
        {
            try
            {
                using (Stream stream = new MemoryStream())
                {
                    XtraReport oldReport = ReportFunctions.MerrRaportinPaParametra(designName, IdNdermarrja);
                    oldReport.SaveLayout(stream);
                    report.LoadLayout(stream);
                    report.DataSource = null;//fshin datasource-in e vjeter qe nuk na nevojitet
                    report.DataAdapter = null;
                }
            }
            catch (NotImplementedException ex)
            {
                ShtoMesazhNeRaportDesigner(new clsMesazh(false, "Ky disajn nuk ekziston ose nuk eshte implementuar!"));
            }
        }

        private static void InitExtensions(XtraReport report)
        {
            report.Extensions[SerializationService.Guid] = CustomUntypedDataSetSerializer.Name;
        }

        protected void CallbackPanel_Callback(object sender, CallbackEventArgsBase e)
        {
            InitDesignerPage(e.Parameter);
        }
    }

    public enum MenuAction
    {
        Save = 0,
        SaveAs = 1
    }

}