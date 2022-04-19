using DbCore;
using DbCore.DbAdmin;
using DbCore.DbShare;
using DbCore.IMBUtils.Logging;
using DbCore.IMBUtils.Messages;
using DevExpress.Web;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Web;
using Newtonsoft.Json;
using PlatinumWeb.ApplicationUtils.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Framework.WebUtils.Pages
{
    public class MyReportPageBase : MyPageBase
    {
        public ASPxHiddenField HfState { get; set; }

        public clsRaporti ReportObject { get; set; }

        public int IdRaporti
        {
            get { if (!HfState.Contains("IdRaporti")) return -1; int.TryParse(HfState["IdRaporti"].ToString(), out int id); return id; }
            set { HfState["IdRaporti"] = value; }
        }

        public string ReportOrientation
        {
            get { return HfState["ReportOrientation"].ToString(); }
            set { HfState["ReportOrientation"] = value; }
        }

        public string ReportStyle
        {
            get { return HfState["ReportStyle"].ToString(); }
            set { HfState["ReportStyle"] = value; }
        }

        public int IdReportDesign
        {
            get { if (!HfState.Contains("IdReportDesign")) return 0; int.TryParse(HfState["IdReportDesign"].ToString(), out int id); return id; }
            set { HfState["IdReportDesign"] = value; }
        }

        public string StylePath
        {
            get { return Server.MapPath(null) + @"\Style Raporte\"; }
        }
        public List<string> Orientations
        {
            get { return JsonConvert.DeserializeObject<List<string>>(HfState.Get("ReportOrientations").ToString()); }
            set { HfState.Set("ReportOrientations", JsonConvert.SerializeObject(value)); }
        }

        public bool EnableStyle
        {
            get { return Convert.ToBoolean(HfState.Get("EnableStyle").ToString()); }
            set { HfState.Set("EnableStyle", value); }
        }
        public List<Dictionary<string, object>> ReportStyles
        {
            get { return JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(HfState.Get("ReportStyles").ToString()); }
            set { HfState.Set("ReportStyles", JsonConvert.SerializeObject(value)); }
        }

        public List<Dictionary<string, object>> ReportDesigns
        {
            get { return JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(HfState.Get("ReportDesigns").ToString()); }
            set { HfState.Set("ReportDesigns", JsonConvert.SerializeObject(value)); }
        }

        public bool ShfaqButonEksportoVeprimtariDitore {
            set { HfState.Set("ShfaqButonEksportoVeprimtariDitore", value); }
        }

        public bool ShfaqButonEksportoPerTatime
        {
            set { HfState.Set("ShfaqButonEksportoPerTatime", value); }
        }

        public bool ShfaqButonEksportoBirthdayCard
        {
            set { HfState.Set("ShfaqButonEksportoBirthdayCard", value); }
        }

        public bool ShfaqButonEdit
        {
            get { return Convert.ToBoolean(HfState.Get("ShfaqButonEdit").ToString()); }
            set { HfState.Set("ShfaqButonEdit", value); }
        }
        public string RaportiEmerReal { get; set; }
        public string GuidString { get; set; }

        public int InitialPageWidth
        {
            get
            {
                if (HfState.Contains("initialPageWidth"))
                    return Convert.ToInt32(HfState.Get("initialPageWidth"));
                else
                    if (ReportOrientation == clsRaportDesign._rap_portrait)
                    return 850;
                else
                    return 1100;
            }
            set
            {
                HfState.Set("initialPageWidth", value);
            }
        }        

        public int InitialPageHeight
        {
            get
            {
                if (HfState.Contains("initialPageHeight"))
                    return Convert.ToInt32(HfState.Get("initialPageHeight"));
                else
                    if (ReportOrientation == clsRaportDesign._rap_portrait)
                    return 1100;
                else
                    return 850;
            }
            set => HfState.Set("initialPageHeight", value);
        }
        public void SetReport()
        {
            if (ReportObject.IdRaporti == 0 && IdReportDesign > 0)
            {
                int id = clsRaporti.KtheIdRaporti(IdGjuha, IdReportDesign);
                ReportObject = new clsRaporti(IdGjuha, id);
                RaportiEmerReal = ReportObject.RaportiEmriReal;
            }
            IdRaporti = ReportObject.IdRaporti;
        }

        public void KonfigFleteRaporti(DevExpress.XtraReports.UI.XtraReport report, clsPerdorues perdoruesi, bool shtoOrePrintimi = true)
        {
            report.Extensions["ndermarrjeLogo"] = JsonConvert.SerializeObject((new clsNdermarrje(IdNdermarrja)).NdermarrjeLogo);

            if (ReportOrientation == clsRaportDesign._rap_portrait)
            {
                if (report.PageWidth > 850)
                    this.InitialPageWidth = report.PageWidth;
            }
            else if (report.PageWidth > 1100)
                this.InitialPageWidth = report.PageWidth;


            report.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            report.PageWidth = Convert.ToInt32(this.InitialPageWidth);
            report.PageHeight = Convert.ToInt32(this.InitialPageHeight);
            report.AllControls<XRTableCell>().ToList().FindAll(x => x.Summary.Running != SummaryRunning.None).ForEach(x => x.CanGrow = false);

            if (shtoOrePrintimi)
            {
                if (perdoruesi.ShfaqDtPrintimi)
                    VendosDtPrintimiRaport(report);
                return;
            }
        }

        public void SetAutoWidthToReport(XtraReport report)
        {
            float scaleFactor = report.PrintingSystem.Document.ScaleFactor;
            if (scaleFactor == 1)
                return;

            DevExpress.XtraPrinting.XtraPageSettingsBase pageSettings = report.PrintingSystem.PageSettings;
            float width = (pageSettings.UsablePageSize.Width / scaleFactor) + pageSettings.RightMargin + pageSettings.LeftMargin;
            float height = (pageSettings.UsablePageSize.Height / scaleFactor) + pageSettings.TopMargin + pageSettings.BottomMargin;

            System.Drawing.Size customPaperSize = System.Drawing.Size.Truncate(new System.Drawing.SizeF(width, report.RollPaper ? height : report.PageHeight));
            DevExpress.XtraPrinting.XtraPageSettingsBase.ApplyPageSettings(report.PrintingSystem.PageSettings, System.Drawing.Printing.PaperKind.Custom, customPaperSize, pageSettings.Margins, pageSettings.MinMargins, false);
                
            report.PrintingSystem.Document.AutoFitToPagesWidth = 0;
            if(report.RollPaper && report.PrintingSystem.Document.Pages.Count > 1)
                report.PrintingSystem.Document.Pages.RemoveAt(1);
        }

        public void VendosDtPrintimiRaport(DevExpress.XtraReports.UI.XtraReport report)
        {
            XRLabel dateOreLabel = new XRLabel();
            dateOreLabel.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold);
            dateOreLabel.ForeColor = System.Drawing.SystemColors.GrayText;
            dateOreLabel.Name = "dateOreLabel";
            dateOreLabel.SizeF = new System.Drawing.SizeF(100F, 23F);
            dateOreLabel.Text = DateTime.Now.ToString();
            dateOreLabel.WidthF = 200F;
            DevExpress.XtraReports.UI.Band band = report.Bands.GetBandByType(typeof(DevExpress.XtraReports.UI.PageFooterBand));
            if (band != null)
            {

                if (report.Bands["PageFooter"].Controls["dateOreLabel"] != null)
                    report.Bands["PageFooter"].Controls.Remove(report.Bands["PageFooter"].Controls["dateOreLabel"]);
                report.Bands["PageFooter"].HeightF += 30F;
                dateOreLabel.LocationFloat = new DevExpress.Utils.PointFloat(0, report.Bands["PageFooter"].HeightF - 25F);
                report.Bands["PageFooter"].Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] { dateOreLabel });
            }
            else
            {
                PageFooterBand band1 = new PageFooterBand();
                band1.Name = "PageFooter";
                band1.HeightF = 30F;
                report.Bands.Add(band1);
                dateOreLabel.LocationFloat = new DevExpress.Utils.PointFloat(0, band1.HeightF - 25F);
                band1.Controls.AddRange(new XRControl[] { dateOreLabel });
            }
        }
        
        public void SetOrientations(clsRaportDesign rapDes)
        {
            List<string> orientations = new List<string>();
             if (!String.IsNullOrEmpty(rapDes.FileName))
            {
                orientations.Add(clsRaportDesign._rap_landscape);
                ReportOrientation = clsRaportDesign._rap_landscape;
            }
            if (!String.IsNullOrEmpty(rapDes.FileNamePortrait))
            {
                orientations.Add(clsRaportDesign._rap_portrait);
                if (String.IsNullOrEmpty(rapDes.FileName))
                    ReportOrientation = clsRaportDesign._rap_portrait;
            }
            Orientations = orientations;
        }
        
        public void SetReportStyles()
        {
            if (!EnableStyle)
                return;
            List<Dictionary<string, object>> reportStyles;
            reportStyles = mySessionObjects.MerrNgaSession<List<Dictionary<string, object>>>(Session, "ReportStyles_", IdGjuha.ToString());
            if (reportStyles == null || reportStyles.Count == 0)
            {
                reportStyles = new List<Dictionary<string, object>>();
                var stilet = DbCore.DbAdmin.ReportStyle.GetReportStyles(IdGjuha);

                foreach (var s in stilet)
                    reportStyles.Add(new Dictionary<string, object>() { { "EmerStili", s.EmerStili }, { "FileName", s.FileName } });

                mySessionObjects.RuajNeSession<List<Dictionary<string, object>>>(Session, reportStyles, "ReportStyles_", IdGjuha.ToString());
            }
            ReportStyles = reportStyles;
        }

        public void SetDesigns(out clsRaportDesign rapDes)
        {
            colRaporteDesign dizajnet = new colRaporteDesign(IdNdermarrja, ReportObject.IdRaporti);
            rapDes = IdReportDesign == 0 ? dizajnet.MerrDizajnTeZgjedhur() : new clsRaportDesign(IdReportDesign);
            IdReportDesign = rapDes.IdRaportDesign;
            SetOrientations(rapDes);
            if (!dizajnet.Any(d => d.IdRaportDesign == IdReportDesign))
                dizajnet.Add(rapDes);

            if (colRolPerdorues.merrRoleSipasPerdoruesiDheKodRoli(mySessionObjects.ktheIdPerdoruesi(Session), "RSU") != -1)
            {
                ReportDesigns = null;
                return;
            }

            List<Dictionary<string, object>> minifiedDesigns = new List<Dictionary<string, object>>();
            foreach (clsRaportDesign item in dizajnet)
                minifiedDesigns.Add(new Dictionary<string, object>() { { "IdRaportDesign", item.IdRaportDesign }, { "Pershkrim", item.Pershkrim }, { "IsEditable", item.IModifikueshem } });
            ReportDesigns = minifiedDesigns;
        }

        public static string NdertoPathStyleSheet(string path, string styleName)
        {
            return path + styleName;
        }
        
        public void KontrolloTeDrejtaRaporti(int idRaporti, int idPerdoruesi, int idNdermarrja, int idViti)
        {
            clsTeDrejtaRaporte teDrejtaRap = new clsTeDrejtaRaporte();
            // teDrejtaRap.merrTeDrejtaPerRaportPerPerdorues(idRaporti, IdPerdoruesi, IdNdermarrja, IdViti);

            switch (teDrejtaRap.DAmb)
            {
                case true:
                    teDrejtaRap.merrTeDrejtaPerRaportPerPerdorues(idRaporti, IdPerdoruesi, IdNdermarrja, IdViti);
                    break;
                case false:
                    teDrejtaRap.merrTeDrejtaPerRaportPerPerdoruesPerSubraportet(idRaporti, IdPerdoruesi, IdNdermarrja, IdViti);
                    break;

                default:
                    Response.Redirect("ThankYou.html");
                    break;
            }
        }

        public void SetReportToolbarTranslations()
        {
            HfState.Set("ReportToolbarButtonRuajStil", MessagesResource.Messages["ReportToolbarButtonRuajStil"]);
            HfState.Set("ReportToolbarButtonZgjidhOrientimin", MessagesResource.Messages["ReportToolbarButtonZgjidhOrientimin"]);
            HfState.Set("ReportToolbarComboBoxStiliTooltip", MessagesResource.Messages["ReportToolbarComboBoxStiliTooltip"]);
            HfState.Set("ReportToolbarButtonZgjidhDizajnin", MessagesResource.Messages["ReportToolbarButtonZgjidhDizajnin"]);
            HfState.Set("ReportToolbarMsgStiliZgjedhurURuajt", MessagesResource.Messages["ReportToolbarMsgStiliZgjedhurURuajt"]);
            HfState.Set("ReportToolbarMsgErrorGjateRuajtjesSeStilit", MessagesResource.Messages["ReportToolbarMsgErrorGjateRuajtjesSeStilit"]);
            HfState.Set("ReportToolbarMsgModifiko", MessagesResource.Messages["ReportToolbarMsgModifiko"]);
            HfState.Set("ReportToolbarButtonFirstPage", MessagesResource.Messages["ReportToolbarButtonFirstPage"]);
            HfState.Set("ReportToolbarButtonPreviousPage", MessagesResource.Messages["ReportToolbarButtonPreviousPage"]);
            HfState.Set("ReportToolbarButtonPageCount", MessagesResource.Messages["ReportToolbarButtonPageCount"]);
            HfState.Set("ReportToolbarButtonNextPage", MessagesResource.Messages["ReportToolbarButtonNextPage"]);
            HfState.Set("ReportToolbarButtonLastPage", MessagesResource.Messages["ReportToolbarButtonLastPage"]);
            HfState.Set("ReportToolbarButtonShumeFaqe", MessagesResource.Messages["ReportToolbarButtonShumeFaqe"]);
            HfState.Set("ReportToolbarButtonZoomOut", MessagesResource.Messages["ReportToolbarButtonZoomOut"]);
            HfState.Set("ReportToolbarButtonZoomToWholePage", MessagesResource.Messages["ReportToolbarButtonZoomToWholePage"]);
            HfState.Set("ReportToolbarButtonZoomIn", MessagesResource.Messages["ReportToolbarButtonZoomIn"]);
            HfState.Set("ReportToolbarButtonPrint", MessagesResource.Messages["ReportToolbarButtonPrint"]);
            HfState.Set("ReportToolbarButtonPrintPage", MessagesResource.Messages["ReportToolbarButtonPrintPage"]);
            HfState.Set("ReportToolbarButtonEksportTo", MessagesResource.Messages["ReportToolbarButtonEksportTo"]);
            HfState.Set("ReportToolbarButtonSearch", MessagesResource.Messages["ReportToolbarButtonSearch"]);
            HfState.Set("ReportToolbarButtonDesigner", MessagesResource.Messages["ReportToolbarButtonDesigner"]);
            HfState.Set("ReportToolbarEdito", MessagesResource.Messages["ReportToolbarEdito"]);
            HfState.Set("ReportToolbarButtonExpKartolinaDitl", MessagesResource.Messages["ReportToolbarButtonExpKartolinaDitl"]);
            HfState.Set("ReportToolbarButtonEksportoPerTatimet", MessagesResource.Messages["ReportToolbarButtonEksportoPerTatimet"]);
            HfState.Set("ReportToolbarButtonEksportoVepDitore", rm.GetString("ReportToolbarButtonEksportoVepDitore", ci));
            HfState.Set("msgShtypShikoEksport", MessagesResource.Messages["msgShtypShikoEksport"]);
            HfState.Set("msgShtypShikoEdit", MessagesResource.Messages["msgShtypShikoEdit"]);
            HfState.Set("msgSingleDesignEdit", MessagesResource.Messages["msgSingleDesignEdit"]);
        }

    }
}
   