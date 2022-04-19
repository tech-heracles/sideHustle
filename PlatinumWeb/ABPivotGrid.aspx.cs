using DbCore;
using DbCore.DbAdmin;
using DbCore.DbAnalizeBuxheti;
using DevExpress.Utils;
using DevExpress.Web;
using DevExpress.XtraPivotGrid;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class ABPivotGrid : MyPageBase
    {
      
        System.Globalization.CultureInfo ci;
        System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

        private int idNdermarrje;
        private int idKonfig = 1;
        private int idPerdoruesi;
        private int idGjuha;
        private int idViti;
        private int idNdermVit;
        private string raporti;
        private string komponenteRaporti;
        private int komponenteRaportiID;
        private string komponentePershkrim;
        private int gridWidth = 0;


        protected void Page_PreInit(object sender, EventArgs e)
        {
            base.Page_PreInit(sender, e);
            raporti = Request.QueryString["raporti"];
            gridWidth =Convert.ToInt32(Request.QueryString["width"]);
            if (string.IsNullOrWhiteSpace(raporti))
                Response.Redirect("/");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                DbCore.clsFunksione.logout(Session,true,"FaqePaautorizuar");
            }
            idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerdoruesi);
            }


            if (!IsPostBack)
            {
                idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
                idNdermVit = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);

                idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
                komponenteRaporti = clsFunksione.GetKomponente(Request);
                
                clsKomponente kompon = new clsKomponente(komponenteRaporti);
                komponenteRaportiID = kompon.IdKomponente;
                hfState.Set("idGjuha", idGjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("idPerdoruesi", idPerdoruesi);
                hfState.Set("idViti", idViti);
                hfState.Set("idNdermVit", idNdermVit);
                hfState.Set("komponenteRaporti", komponenteRaporti);
                hfState.Set("komponenteRaportiID", komponenteRaportiID);
                komponentePershkrim = kompon.PershkrimiKomponente_sq;
             
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, komponenteRaporti);
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DPlot.ToString();
                hfTeDrejta.Add("DAmb", tedrejtaInfo.DAmb);


                mbushGridenNgaDB();
                KonfiguroOpsionet();
                KrijoKolonaPivotGrid();


            }
            else
            {

                idNdermarrje = (int)hfState.Get("idNdermarrje");
                idPerdoruesi = (int)hfState.Get("idPerdoruesi");
                idViti = (int)hfState.Get("idViti");
                idGjuha = (int)hfState.Get("idGjuha");
                idNdermVit = (int)hfState.Get("idNdermVit");
                komponenteRaporti = (string)hfState.Get("komponenteRaporti");
                komponenteRaportiID = (int)hfState.Get("komponenteRaportiID");
                mbushGrideNgaSession();

            }
            ci = DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo(idGjuha);

            percaktoTemplateMenu();
            pvgRaproti.Width =Unit.Pixel(gridWidth);
            
        }
        

        /// <summary>
        /// mbush datasourcein e grides direkt nga db
        /// </summary>
        /// <param name="idNdermarrje"></param>
        private void mbushGridenNgaDB()
        {
            DataTable dt = AnalizeBuxheti.MerrTeDhenaRaportiPivotGrid(idNdermarrje, komponenteRaportiID, idNdermVit);
            pvgRaproti.DataSource = dt;
            pvgRaproti.DataBind();
            mySessionObjects.ruajObjectNeSesion(Session, dt, "dataSourcePivoti");
        }

        /// <summary>
        /// merr ds e grides nga sessioni
        /// </summary>
        public void mbushGrideNgaSession()
        {

            object tmp = mySessionObjects.merrObjectNgaSesioni(Session, "dataSourcePivoti");
            DataTable col = (tmp as DataTable) ?? AnalizeBuxheti.MerrTeDhenaRaportiPivotGrid(idNdermarrje,komponenteRaportiID, idNdermVit);
            pvgRaproti.DataSource = tmp;
            pvgRaproti.DataBind();
        }
        private void KonfiguroOpsionet()
        {


            switch (komponenteRaportiID)
            {
                case 3026:
                case 3027:
                    pvgRaproti.OptionsView.ShowColumnGrandTotals = true;
                    pvgRaproti.OptionsView.ShowRowGrandTotals = true;
                    pvgRaproti.OptionsView.ShowColumnTotals = false;
                    pvgRaproti.OptionsView.ShowRowTotals = true;
                    break;
                case 3041:
                    pvgRaproti.OptionsView.ShowColumnGrandTotals = true;
                    pvgRaproti.OptionsView.ShowRowGrandTotals = false;
                    pvgRaproti.OptionsView.ShowColumnTotals = false;
                    pvgRaproti.OptionsView.ShowRowTotals = false;
                    
                    break;
                default:
                    pvgRaproti.OptionsView.ShowColumnGrandTotals = false;
                    pvgRaproti.OptionsView.ShowRowGrandTotals = true;
                    pvgRaproti.OptionsView.ShowColumnTotals = false;
                    pvgRaproti.OptionsView.ShowRowTotals = true;
                    break;
            }
            pvgRaproti.OptionsPager.RowsPerPage = 20;
        }

        private void KrijoKolonaPivotGrid()
        {
            pvgRaproti.Fields.Clear();
            var kolonat = clsPivotGrid.MerrKolonaPerPivotGrid(komponenteRaportiID);
            foreach (var kolon in kolonat)
            {
                DevExpress.Web.ASPxPivotGrid.PivotGridField field = new DevExpress.Web.ASPxPivotGrid.PivotGridField
                {
                    FieldName = kolon.EmriKolones,
                    Caption = kolon.Pershkrimi,
                    AreaIndex = kolon.Indexi,

                };

                switch (kolon.Zona)
                {
                    case "column":
                        field.Area = PivotArea.ColumnArea;
                        break;
                    case "data":
                        field.Area = PivotArea.DataArea;
                        break;
                    case "row":
                        field.Area = PivotArea.RowArea;
                        break;
                    case "filter":
                        field.Area = PivotArea.FilterArea;
                        break;
                    default:
                        field.Area = PivotArea.FilterArea;
                        break;
                }

                if (string.Equals(kolon.Tipi, "float", StringComparison.OrdinalIgnoreCase))
                {
                    field.CellFormat.FormatType = FormatType.Numeric;
                    field.CellFormat.FormatString = "n2";
                    field.GrandTotalCellFormat.FormatString = "n2";
                    field.GrandTotalCellFormat.FormatType = FormatType.Numeric;
                    field.TotalCellFormat.FormatType = FormatType.Numeric;
                    field.TotalCellFormat.FormatString = "n2";
                    //field.ValueFormat.FormatString = "n2";
                    //field.ValueFormat.FormatType = FormatType.Numeric;
                    field.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Sum;
                    
                }
                else if(string.Equals(kolon.Tipi, "int", StringComparison.OrdinalIgnoreCase))
                {
                    field.CellFormat.FormatType = FormatType.Numeric;
                    field.CellFormat.FormatString = "n0";
                    field.GrandTotalCellFormat.FormatString = "n0";
                    field.GrandTotalCellFormat.FormatType = FormatType.Numeric;
                    field.TotalCellFormat.FormatType = FormatType.Numeric;
                    field.TotalCellFormat.FormatString = "n0";
                    //field.ValueFormat.FormatString = "n2";
                    //field.ValueFormat.FormatType = FormatType.Numeric;
                    field.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Sum;

                }
                else if(string.Equals(kolon.Tipi, "date", StringComparison.OrdinalIgnoreCase))
                {
                    field.CellFormat.FormatString = "dd/MM/yyyy";
                    field.GrandTotalCellFormat.FormatString = "dd/MM/yyyy";
                    field.TotalCellFormat.FormatString = "dd/MM/yyyy";
                    field.ValueFormat.FormatString = "dd/MM/yyyy";
                    field.ValueFormat.FormatType = FormatType.DateTime;
                    field.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Max;
                }
                else
                {
                    field.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Max;
                }

                pvgRaproti.Fields.AddField(field);
               
            }


        }
        private void percaktoTemplateMenu()
        {
            clsToolbarConfig.percaktoTemplateMenu(idGjuha, idViti, idPerdoruesi, idNdermarrje, ASPxMenu1, komponenteRaporti, this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, false, true, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session), true);
            EventHandler handlerExportoClick = new EventHandler(exportoRaport);
            
            EventHandler handlerPerOnPreRender = new EventHandler(PreRender_ExportButton);
            clsToolbarConfig.ShtoMenuItemExporto(this, ASPxMenu1, handlerExportoClick, handlerPerOnPreRender);
        }
        public void PreRender_ExportButton(object sender, EventArgs e)
        {
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemExport");
            ASPxButton exportButton = ((PlatinumWeb.MenuExport)(itemButton.Template)).FindControl("exportButton") as ASPxButton;
            ScriptManager ScriptMgr = (ScriptManager)this.FindControl("ScriptManager1");
            ScriptMgr.RegisterPostBackControl(exportButton);
        }
        public void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
        }

        public void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
        }
        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {
        }

        protected void exportoRaport(object sender, EventArgs e)
        {
            CultureInfo ci = DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo((int)hfState["idGjuha"]);
            string fileName = komponentePershkrim;
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemExport");
            ASPxComboBox cmbExport = ((PlatinumWeb.MenuExport)(itemButton.Template)).FindControl("cmbExport") as ASPxComboBox;

            //PrintingSystem printingSystem = new PrintingSystem();

            //using (PrintableComponentLink printableComponentLink = new PrintableComponentLink())
            //{
            //    //PrintableComponentLink printableComponentLink = new PrintableComponentLink();
            //    printableComponentLink.PrintingSystem = printingSystem;
            //    printableComponentLink.Component = ASPxPivotGridExporterRaporti;
            //    //if (ASPxPivotGridRaporti.Data.FieldListFields.FieldItems.Count > 8)
            //    //    printableComponentLink.Landscape = true;
            //    //else printableComponentLink.Landscape = false;
            //    printableComponentLink.CreateDocument();
            //}
            //printingSystem.Document.AutoFitToPagesWidth = 1;
            ASPxPivotGridExporterRaporti.OptionsPrint.PageSettings.Landscape = true;
            
            try
            {
                switch (cmbExport.SelectedIndex)
                {
                    case 0:
                        ASPxPivotGridExporterRaporti.ExportPdfToResponse(fileName, true);
                        break;

                    case 1:
                        //XlsxExportOptions optXlsx = new XlsxExportOptions();
                        //optXlsx.RawDataMode = true;
                        ASPxPivotGridExporterRaporti.ExportXlsxToResponse(fileName, true);
                        break;

                    case 2:
                        XlsExportOptions opts = new XlsExportOptions();
                        opts.Suppress256ColumnsWarning = true;
                        ASPxPivotGridExporterRaporti.ExportXlsToResponse(fileName, opts, true);
                        break;

                    case 3:
                        ASPxPivotGridExporterRaporti.ExportTextToResponse(fileName, true);
                        break;

                    case 4:
                        ASPxPivotGridExporterRaporti.ExportHtmlToResponse(fileName, "utf-8", "ASPxPivotGrid Printing Sample", true, true);
                        break;
                }
            }
            catch (System.OutOfMemoryException err)
            {
                string mesazh = "Kubi ka shume te dhena dhe nuk mund te eksportohet i tere ne excel. Ju lutem filtroni me pak te dhena";
                NLog.LogManager.GetCurrentClassLogger().Error(mesazh, err.Message);
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh, pnlMesazhi);
            }
        }

    }
}