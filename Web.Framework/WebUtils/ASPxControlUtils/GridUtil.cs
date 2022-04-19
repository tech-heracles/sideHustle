using DbCore;
using DbCore.DbAdmin;
using DbCore.DbInventari;
using DbCore.DbShare;
using DbCore.IMBUtils.Cache;
using DbCore.IMBUtils.Extensions;
using DbCore.IMBUtils.Logging;
using DbCore.IMBUtils.Messages;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.Web;
using DevExpress.Web.ASPxPivotGrid;
using DevExpress.XtraPrinting;
using NLog;
using PlatinumWeb.ApplicationUtils.Filters;
using PlatinumWeb.Templates;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using MenuItem = DevExpress.Web.MenuItem;
using Page = System.Web.UI.Page;
using TipKoloneBatchEdit = DbCore.DbShare.TipKoloneBatchEdit;

namespace PlatinumWeb
{
    /// <summary>
    /// kjo klase do te jete pergjegjese per te gjitha veprimet qe kane te bejne me gridat
    ///
    /// </summary>
    public static class GridUtil
    {
        public const string CallbackParamFilterGrida = "APPLYMULTICOLUMNFILTER";
        public const string CallbackParamApplyFilter = "APPLYFILTER";
        public const string CallbackParamApplyColumnFilter = "APPLYCOLUMNFILTER";
        public const string CallbackParamFilterEnabled = "SETFILTERENABLED";
        public const string refresh = "CustomButtonRefreshGrid";
        public const string APLIKOFILTERDEFAULT = "APLIKOFILTERDEFAULT";
        /// <summary>
        /// kjo metode do i aplikoj grides cilesi te ndryshme te cilat jane te konfiguruara ne nivel perdoruesi
        /// </summary>
        /// <param name="Grid"></param>
        /// <param name="session"></param>
        public static void PercaktoSettings(ASPxGridView grid, HttpSessionState session)
        {
            grid.KeyboardSupport = true;

            //PercaktoSettingsMeLartesi(grid, Session, 600);
        }

        public static void PercaktoSettingsMeLartesi(ASPxGridView grid, HttpSessionState session, int height)
        {
            throw new NotImplementedException();
            //clsPerdorues p = mySessionObjects.kthePerdorues(Session);

            // //per momentin eshte vetem lloji i pagerit

            // grid.SettingsPager.PageSize = 15;

            // if (p.GridaEndless)
            // {
            //     grid.SettingsPager.Mode = GridViewPagerMode.EndlessPaging;

            //     per momentin po ja vendosim fixe
            //     grid.ClientSideEvents.Init = string.Format("function(s,e){{s.SetHeight({0});}}",height);
            // }
            // else
            //     grid.SettingsPager.Mode = GridViewPagerMode.ShowPager;
        }

        /// <summary>
        /// perckton nje template combo per nje fushe te grides
        /// </summary>
        /// <param name="grid">grida</param>
        /// <param name="textField">fusha tekst qe do shfaqet</param>
        /// <param name="valueField">fusha e cila permban vleren ne gride</param>
        /// <param name="dataSource">datasource i combos</param>
        public static void PercaktoTemplateCombo(ASPxGridView grid, string textField, string valueField, object dataSource, string fieldName = "")
        {
            if (String.IsNullOrWhiteSpace(fieldName))
                fieldName = valueField;
            grid.Columns.Remove(grid.Columns[valueField]);
            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();

            colnew.PropertiesComboBox.DataSource = dataSource;
            colnew.PropertiesComboBox.TextField = textField;
            colnew.PropertiesComboBox.ValueField = valueField;
            colnew.FieldName = valueField;
            colnew.PropertiesComboBox.Items.Add(); //item bosh
            grid.Columns.Add(colnew);
        }

        #region TITLE PANEL
        /// <summary>
        /// i vendos grides nje title panel me butonat e eksportit,dhe opsional vendos renditjen default te grides,e cila perdoret per ruajtjen e filtrit te saj
        /// gjithashtu siguron edhe customizimin e grides
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="page"></param>
        /// <param name="menuMesazhesh"></param>
        /// <param name="pnlMesazhi"></param>
        /// <param name="hfState"></param>
        /// <param name="idPerdoruesi"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="idViti"></param>
        /// <param name="idGjuha"></param>
        /// <param name="idkonfigAmbjenti">idkonfig</param>
        /// <param name="komponente">emri i saj</param>
        /// <param name="idKomponente">idk komp</param>
        /// <param name="renditjeDefault">fusha sipas te ciles do renditet grida ne fillim</param>
        /// <param name="rm">resource manageri</param>
        /// <param name="c">culture info</param>
        /// <param name="meExpandCollapse">nese do te jete nje buton expandcollapse ne headerin e grides</param>
        /// <param name="exportType">menyra e exportit</param>
        public static void PercaktoTitlePanel(this ASPxGridView grid, Page page, ASPxMenu menuMesazhesh, UpdatePanel pnlMesazhi, ASPxHiddenField hfState, int idPerdoruesi, int idNdermarrje, int idViti, int idGjuha, int idkonfigAmbjenti, string komponente, int idKomponente, string renditjeDefault, ResourceManager rm, CultureInfo c, bool meExpandCollapse = false, GridViewExportedRowType exportType = GridViewExportedRowType.Selected, bool vetemSelectButtons = false, bool wysiwygExportOption = true, bool PdfExport = true)
        {
            grid.Settings.ShowTitlePanel = true;
            grid.SettingsBehavior.EnableCustomizationWindow = true;
            grid.Templates.TitlePanel = new MyBaseTitlePanelTemplate(grid.ID, page, menuMesazhesh, pnlMesazhi, hfState, idkonfigAmbjenti, idPerdoruesi, idNdermarrje, idViti, idGjuha, komponente, idKomponente, renditjeDefault, rm, c, meExpandCollapse, exportType, vetemSelectButtons, wysiwygExportOption, false, PdfExport);
        }

        public static void PercaktoTitlePanelMeRefresh(this ASPxGridView grid, Page page, ASPxMenu menuMesazhesh, UpdatePanel pnlMesazhi, ASPxHiddenField hfState, int idPerdoruesi, int idNdermarrje, int idViti, int idGjuha, int idkonfigAmbjenti, string komponente, int idKomponente, string renditjeDefault, ResourceManager rm, CultureInfo c, bool meExpandCollapse = false, GridViewExportedRowType exportType = GridViewExportedRowType.Selected, bool vetemSelectButtons = false, bool meTopRows = false)
        {
                grid.Settings.ShowTitlePanel = true;
                grid.SettingsBehavior.EnableCustomizationWindow = true;
                grid.Templates.TitlePanel = new MyTitlePanelTemplateMeRefresh(grid.ID, page, menuMesazhesh, pnlMesazhi, hfState, idkonfigAmbjenti, idPerdoruesi, idNdermarrje, idViti, idGjuha, komponente, idKomponente, renditjeDefault, rm, c, meExpandCollapse, exportType, vetemSelectButtons, true, meTopRows);
        }

        /// <summary>
        /// percakton default renditjen sipas keyfieldname dhe metoden e eksportit selected
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="page"></param>
        /// <param name="menuMesazhesh"></param>
        /// <param name="pnlMesazhi"></param>
        /// <param name="hfState"></param>
        /// <param name="idPerdoruesi"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="idViti"></param>
        /// <param name="idGjuha"></param>
        /// <param name="idkonfigAmbjenti"></param>
        /// <param name="komponenteEmri"></param>
        /// <param name="rm"></param>
        /// <param name="c"></param>
        public static void PercaktoTitlePanel(this ASPxGridView grid, Page page, ASPxMenu menuMesazhesh, UpdatePanel pnlMesazhi, ASPxHiddenField hfState, int idPerdoruesi, int idNdermarrje, int idViti, int idGjuha, int idkonfigAmbjenti, string komponenteEmri, ResourceManager rm, CultureInfo c, bool wysiwygExportOption = true)
        {
            grid.PercaktoTitlePanel(page, menuMesazhesh, pnlMesazhi, hfState, idPerdoruesi, idNdermarrje, idViti, idGjuha, idkonfigAmbjenti, komponenteEmri, rm, c, GridViewExportedRowType.Selected, wysiwygExportOption);
        }

        public static void PercaktoTitlePanelMeRefresh(this ASPxGridView grid, Page page, ASPxMenu menuMesazhesh, UpdatePanel pnlMesazhi, ASPxHiddenField hfState, int idPerdoruesi, int idNdermarrje, int idViti, int idGjuha, int idkonfigAmbjenti, string komponenteEmri, ResourceManager rm, CultureInfo c, bool meTopRows)
        {
            grid.PercaktoTitlePanelMeRefresh(page, menuMesazhesh, pnlMesazhi, hfState, idPerdoruesi, idNdermarrje, idViti, idGjuha, idkonfigAmbjenti, komponenteEmri, rm, c, GridViewExportedRowType.Selected, meTopRows);
        }

        /// <summary>
        /// percakton default renditjen sipas keyfieldname dhe metoden e eksportit selected
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="page"></param>
        /// <param name="menuMesazhesh"></param>
        /// <param name="pnlMesazhi"></param>
        /// <param name="hfState"></param>
        /// <param name="idPerdoruesi"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="idViti"></param>
        /// <param name="idGjuha"></param>
        /// <param name="idkonfigAmbjenti"></param>
        /// <param name="komponenteEmri"></param>
        /// <param name="rm"></param>
        /// <param name="c"></param>
        /// <param name="exportType"></param>
        public static void PercaktoTitlePanel(this ASPxGridView grid, Page page, ASPxMenu menuMesazhesh, UpdatePanel pnlMesazhi, ASPxHiddenField hfState, int idPerdoruesi, int idNdermarrje, int idViti, int idGjuha, int idkonfigAmbjenti, string komponenteEmri, ResourceManager rm, CultureInfo c, GridViewExportedRowType exportType, bool wysiwygExportOption = true)
        {

            var idKomponente = clsKomponente.MerrIdKomponenteSipasEmrit(komponenteEmri);//kjo te hiqet fare todo getson
            grid.PercaktoTitlePanel(page, menuMesazhesh, pnlMesazhi, hfState, idPerdoruesi, idNdermarrje, idViti, idGjuha, idkonfigAmbjenti, komponenteEmri, idKomponente, grid.KeyFieldName, rm, c, false, exportType, false, wysiwygExportOption);

        }
        public static void PercaktoTitlePanelMeRefresh(this ASPxGridView grid, Page page, ASPxMenu menuMesazhesh, UpdatePanel pnlMesazhi, ASPxHiddenField hfState, int idPerdoruesi, int idNdermarrje, int idViti, int idGjuha, int idkonfigAmbjenti, string komponenteEmri, ResourceManager rm, CultureInfo c, GridViewExportedRowType exportType, bool meTopRows)
        {

            var idKomponente = clsKomponente.MerrIdKomponenteSipasEmrit(komponenteEmri);//kjo te hiqet fare todo getson
            grid.PercaktoTitlePanelMeRefresh(page, menuMesazhesh, pnlMesazhi, hfState, idPerdoruesi, idNdermarrje, idViti, idGjuha, idkonfigAmbjenti, komponenteEmri, idKomponente, grid.KeyFieldName, rm, c, false, exportType, false, meTopRows);

        }

        /// <summary>
        /// percakton template e header-it te grides duke vendosur edhe periudhen aty,duhet te vendoset perpara mbushjes se grides qe te funksionoj
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="page"></param>
        /// <param name="menuMesazhesh"></param>
        /// <param name="pnlMesazhi"></param>
        /// <param name="hfState"></param>
        /// <param name="idPerdoruesi"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="idViti"></param>
        /// <param name="idGjuha"></param>
        /// <param name="idkonfigAmbjenti"></param>
        /// <param name="komponente"></param>
        /// <param name="idKomponente"></param>
        /// <param name="renditjeDefault"></param>
        /// <param name="rm"></param>
        /// <param name="c"></param>
        /// <param name="meExpandCollapse"></param>
        /// <param name="exportType"></param>
        public static void PercaktoTitlePanelMePeriudhe(this ASPxGridView grid, Page page, ASPxMenu menuMesazhesh, UpdatePanel pnlMesazhi, ASPxHiddenField hfState, int idPerdoruesi, int idNdermarrje, int idViti, int idGjuha, int idkonfigAmbjenti, string komponente, int idKomponente, string renditjeDefault, ResourceManager rm, CultureInfo c, bool periudheGjitheVitet, bool meExpandCollapse = false, bool vetemPeriudha = false, GridViewExportedRowType exportType = GridViewExportedRowType.Selected)
        {
            grid.Settings.ShowTitlePanel = true;
            grid.SettingsBehavior.EnableCustomizationWindow = true;
            grid.Templates.TitlePanel = new MyTitlePanelTemplateMePeriudhe(grid.ID, page, menuMesazhesh, pnlMesazhi, hfState, idkonfigAmbjenti, idPerdoruesi, idNdermarrje, idViti, idGjuha, komponente, idKomponente, renditjeDefault, rm, c, meExpandCollapse, exportType, periudheGjitheVitet, vetemPeriudha);
        }

        public static void PercaktoTitlePanelMePeriudheDheTopRows(this ASPxGridView grid, Page page, ASPxMenu menuMesazhesh, UpdatePanel pnlMesazhi, ASPxHiddenField hfState, int idPerdoruesi, int idNdermarrje, int idViti, int idGjuha, int idkonfigAmbjenti, string komponente, int idKomponente, string renditjeDefault, ResourceManager rm, CultureInfo c, bool meExpandCollapse = false, GridViewExportedRowType exportType = GridViewExportedRowType.Selected)
        {
            grid.Settings.ShowTitlePanel = true;
            grid.SettingsBehavior.EnableCustomizationWindow = true;
            grid.Templates.TitlePanel = new MyTitlePanelMePeriudheDheComboTop(grid.ID, page, menuMesazhesh, pnlMesazhi, hfState, idkonfigAmbjenti, idPerdoruesi, idNdermarrje, idViti, idGjuha, komponente, idKomponente, renditjeDefault, rm, c, meExpandCollapse, exportType, true, false);
        }
        public static void PercaktoTitlePanelPerRegjistrimDokumentash(this ASPxGridView grid, Page page, ASPxMenu menuMesazhesh, UpdatePanel pnlMesazhi, ASPxHiddenField hfState, int idPerdoruesi, int idNdermarrje, int idViti, int idGjuha, int idkonfigAmbjenti, string komponente, int idKomponente, string renditjeDefault, ResourceManager rm, CultureInfo c, bool meExpandCollapse = false, GridViewExportedRowType exportType = GridViewExportedRowType.Selected)
        {
            grid.Settings.ShowTitlePanel = true;
            grid.SettingsBehavior.EnableCustomizationWindow = true;
            grid.Templates.TitlePanel = new MyTitlePanelRegjistrimDokumentash(grid.ID, page, menuMesazhesh, pnlMesazhi, hfState, idkonfigAmbjenti, idPerdoruesi, idNdermarrje, idViti, idGjuha, komponente, idKomponente, renditjeDefault, rm, c, meExpandCollapse, exportType, false);
        }


        /// <summary>
        /// vendos butonat mbi griden batch qe sherben si trup dokumenti
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="page"></param>
        /// <param name="menuMesazhesh"></param>
        /// <param name="pnlMesazhi"></param>
        /// <param name="hfState"></param>
        /// <param name="idPerdoruesi"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="idViti"></param>
        /// <param name="idGjuha"></param>
        /// <param name="idkonfigAmbjenti"></param>
        /// <param name="komponente"></param>
        /// <param name="idKomponente"></param>
        /// <param name="renditjeDefault"></param>
        /// <param name="rm"></param>
        /// <param name="c"></param>
        public static void PercaktoTitlePanelPerTrupDokumenti(ASPxGridView grid, Page page, ASPxMenu menuMesazhesh, UpdatePanel pnlMesazhi, ASPxHiddenField hfState, int idPerdoruesi, int idNdermarrje, int idViti, int idGjuha, int idkonfigAmbjenti, string komponente, int idKomponente, string renditjeDefault, ResourceManager rm, CultureInfo c, bool enableCustomizationWindow)
        {
            grid.Settings.ShowTitlePanel = true;
            grid.SettingsBehavior.EnableCustomizationWindow = enableCustomizationWindow;
            grid.Templates.TitlePanel = new MyBatchEditTitleTemplate(page, grid, menuMesazhesh, pnlMesazhi, hfState, idkonfigAmbjenti, idPerdoruesi, idNdermarrje, idViti, idGjuha, komponente, idKomponente, renditjeDefault, enableCustomizationWindow, rm, c);
        }

        #endregion TITLE PANEL

        public static void PercaktoTemplateCombo(GridViewDataComboBoxColumn comboBoxColumn, string fieldNameGrida, string valueField, string textField, string caption)
        {
            comboBoxColumn.PropertiesComboBox.ValueField = valueField;
            comboBoxColumn.PropertiesComboBox.TextField = textField;
            comboBoxColumn.PropertiesComboBox.ValueType = typeof(string);
            comboBoxColumn.Caption = caption;
            comboBoxColumn.FieldName = fieldNameGrida;
            comboBoxColumn.PropertiesComboBox.DropDownStyle = DropDownStyle.DropDownList;

            comboBoxColumn.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        }

        public static void PercaktoTemplateComboMeButtonLupe(GridViewDataComboBoxColumn comboBoxColumn, string fieldNameGrida, string valueField, string textField, string caption, string buttonClickHandler)
        {
            PercaktoTemplateCombo(comboBoxColumn, fieldNameGrida, valueField, textField, caption);
            if (comboBoxColumn.PropertiesComboBox.Buttons.Count != 0) return;
            comboBoxColumn.PropertiesComboBox.Buttons.Add();
            comboBoxColumn.PropertiesComboBox.ClientSideEvents.ButtonClick = $"function(s,e){{{buttonClickHandler}(s,e);}}";
        }
        public static void PercaktoTemplateTotalSummaryFooter(ASPxGridView grida, string formatString, params string[] fushat)
        {
            grida.Settings.ShowFooter = true;
            for (int i = 0; i < fushat.Length; i++)
                grida.Columns[fushat[i]].FooterTemplate = new MySummaryTotalTemplate(fushat[i], formatString);
        }


        public static void PercaktoBandPerKolona(ASPxGridView grida, string caption, params string[] kolonat)
        {

            GridViewBandColumn banda = new GridViewBandColumn
            {
                Caption = caption,
                VisibleIndex = grida.Columns[kolonat[0]].VisibleIndex

            };
            banda.HeaderStyle.BackColor = Color.LightGray;


            foreach (string kolona in kolonat)
            {
                var col = grida.Columns[kolona];
                grida.Columns.Remove(grida.Columns[kolona]);
                banda.Columns.Add(col);
                banda.Width = Unit.Percentage(banda.Width.Value + col.Width.Value);
                banda.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                // grida.Columns.Remove(grida.Columns[kolonat[i]]);
            }

            grida.Columns.Add(banda);

        }

        public static void PercaktoNgjyrenPerKolonatReadOnly(ASPxGridView grida, params string[] kolonat)
        {
            for (int i = 0; i < kolonat.Length; i++)
            {
                grida.Columns[kolonat[i]].CellStyle.BackColor = Color.WhiteSmoke;
            }
        }

        public static void PercaktoStilHeaderiPerKolona(ASPxGridView grid)
        {
            foreach (GridViewColumn col in grid.AllColumns)
            {
                col.HeaderStyle.Wrap = DefaultBoolean.True;
                col.HeaderStyle.BackColor = Color.LightGray;
            }
        }

        public static void HideKolonaGride(ASPxGridView grida, params string[] kolonat)
        {
            for (int i = 0; i < kolonat.Length; i++)
            {
                grida.Columns[kolonat[i]].Visible = false;
                grida.Columns[kolonat[i]].ShowInCustomizationForm = false;

            }
        }

        public static void ShtoButtonFshi(ASPxGridView grida)
        {
            GridViewCommandColumn col = new GridViewCommandColumn
            {
                VisibleIndex = grida.Columns.Count,
                Width = 20,
                Name = "ButtonFshi"
            };
            GridViewCommandColumnCustomButtonCollection customButtons = col.CustomButtons;
            col.ButtonType = GridCommandButtonRenderMode.Image;
            GridViewCommandColumnCustomButton btnDelete = new GridViewCommandColumnCustomButton();
            btnDelete.ID = "btnDelete";
            btnDelete.Text = "Fshi";
            btnDelete.Visibility = GridViewCustomButtonVisibility.AllDataRows;
            btnDelete.Image.Url = @"images/new/button_cancel-32.png";
            btnDelete.Image.Width = 20;
            btnDelete.Image.ToolTip = "Fshi";
            customButtons.Add(btnDelete);
            if (grida.Columns[col.Name] != null)
                grida.Columns.Remove(grida.Columns[col.Name]);
            grida.Columns.Add(col);
        }

        public static void VendosFormatNumriPerFushatNumerike(ASPxGridView grida, int shifraPasPresjes, params string[] fushat)
        {
            foreach (var fush in fushat)
            {
                var col = grida.DataColumns[fush];
                if (col == null) throw new MyException($"Kolona {fush} nuk ekziston ne griden {grida.ID}");
                VendosFormatNumri(col, shifraPasPresjes);
            }
        }

        public static void VendosFormatNumri(GridViewDataColumn kolona, int shifraPasPresjes)
        {
            kolona.PropertiesEdit.DisplayFormatString = $"n{shifraPasPresjes}";
        }

        public static void AplikoStileBatchEdit(ASPxGridView grida, Color modifiedCellBackColor, Color modifiedCellForColor, int modifiedCellFontSize)
        {

            grida.Styles.BatchEditModifiedCell.BackColor = modifiedCellBackColor;
            grida.Styles.BatchEditModifiedCell.ForeColor = modifiedCellForColor;
            grida.Styles.BatchEditModifiedCell.Font.Size = new FontUnit(modifiedCellFontSize, UnitType.Pixel);
            grida.Styles.SelectedRow.BackColor = Color.Transparent;

        }

        private static void HiqRreshtaNgaDts(int id, string keyFieldName, Dictionary<string, DataTable> myDts)
        {
            for (var i = 0; i < myDts.Count; i++)
            {
                var key = myDts.Keys.ElementAt(i);
                myDts[key] = HiqRreshtaNgaDtsKevi(id, keyFieldName, myDts[key]);
            }
        }

        private static DataTable HiqRreshtaNgaDtsKevi(int id, string keyFieldName, DataTable dt)
        {
            try
            {
                var teRejat = dt.Select($"{keyFieldName} not in ({id})");
                if (teRejat.FirstOrDefault() == null)
                    return null;
                return teRejat.GetDataTable(dt);
            }
            catch (Exception ex)
            {
                throw new MyException("Gabim ne pastrim te rreshtave te grides", ex);
            }

        }

        public static void HiqRreshtaNgaDataSourceGridesNeSession(string emerKomponente, string guidString, int idViti, HttpSessionState session, string keyFieldName, int id)
        {
            var myDts = MerrGrida(emerKomponente, idViti, session, guidString);
            HiqRreshtaNgaDts(id, keyFieldName, myDts);
            RuajGrida( session, myDts);
        }

        /// <summary>
        /// merr datasource e grides per te gjitha periudhat
        /// </summary>
        /// <param name="emerKomponente"></param>
        /// <param name="idViti"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        private static Dictionary<string, DataTable> MerrGrida(string emerKomponente, int idViti, HttpSessionState session, string guidString)
        {
            //TODO GETSON fut guidString
            var myDts = new Dictionary<string, DataTable>();

            foreach (var periudhe in Enum.GetNames(typeof(LlojPeriudhe)))
            {
                var keySessioni = SessionKeyUtils.MerrSessionKeyPerDsGride(emerKomponente, idViti, periudhe,-1);
                mySessionObjects.merrGrideNgaSessioni(keySessioni, session, myDts);
            }
            return myDts;
        }

        /// <summary>
        /// ruan ds e grides perseri pas fshirjes se rreshtit
        /// </summary>
        /// <param name="session"></param>
        /// <param name="myDts"></param>
        private static void RuajGrida(HttpSessionState session, Dictionary<string, DataTable> myDts)
        {
            foreach (var keyDt in myDts)
            {
                mySessionObjects.ruajGrideNeSession(keyDt.Key, session, keyDt.Value);
            }
        }

        /// <summary>
        /// exprorton griden ne pdf ne nje faqe
        /// </summary>
        /// <param name="exporter"></param>
        /// <param name="response"></param>
        /// <param name="fileName"></param>
        public static void ExportPdfFitToPage(ASPxGridViewExporter exporter, HttpResponse response, string fileName)
        {
            using (var ms = new MemoryStream())
            {
                var pcl = new PrintableComponentLink(new PrintingSystem())
                {
                    Component = exporter
                };
                pcl.Margins.Left = pcl.Margins.Right = 50;
                pcl.Landscape = true;
                pcl.CreateDocument(false);

                pcl.PrintingSystem.Document.AutoFitToPagesWidth = 1;
                pcl.ExportToPdf(ms);
                clsFunksione.WriteResponse(response, ms, fileName, "application/pdf", ".pdf");
            }
        }

        public static GridViewDataComboBoxColumn KrijoGridViewDataComboBoxColumnSipasKolonesEkzistuese(GridViewColumn oldColumn)
        {
            return new GridViewDataComboBoxColumn
            {
                HeaderCaptionTemplate = oldColumn.HeaderCaptionTemplate,
                Caption = oldColumn.Caption,
                Name = oldColumn.Name,
                Index = oldColumn.Index,
                VisibleIndex = oldColumn.VisibleIndex,
                Width = oldColumn.Width,
                AllowTextTruncationInAdaptiveMode = oldColumn.AllowTextTruncationInAdaptiveMode,
                Visible = oldColumn.Visible,
                ToolTip = oldColumn.ToolTip
            };
        }

        /// <summary>
        /// kthen nje list me dictionary me rreshtat e selektuar ne grid,nje dictionary eshte nje rresht i selektuar,vlera e seciles fushe kapet ne kete forme r["fusha"]
        /// </summary>
        /// <param name="grida"></param>
        /// <param name="fushat">fushat qe do merret nga rreshtat e selektuar</param>
        /// <returns></returns>
        public static List<Dictionary<string, string>> MerrRreshtatESelektuar(this ASPxGridView grida, params string[] fushat)
        {
            var rowsNgaaGrida = grida.GetSelectedFieldValues(fushat);
            var rreshtatMeVlera = new List<Dictionary<string, string>>(rowsNgaaGrida.Count);

            for (var r = 0; r < rowsNgaaGrida.Count; r++)
            {
                object[] rowArray;
                if (fushat.Length == 1)
                    rowArray = new[] { rowsNgaaGrida[r] };
                else
                    rowArray = rowsNgaaGrida[r] as object[];
                if (rowArray == null)
                    throw new ArgumentNullException($"rreshti {(r + 1)} nga grida null");
                var rreshti = new Dictionary<string, string>(fushat.Length);
                for (int i = 0, count = fushat.Length; i < count; i++)
                {
                    if (!Convert.IsDBNull(rowArray[i]))
                        rreshti[fushat[i]] = rowArray[i].ToString();
                    else
                        rreshti[fushat[i]] = null;
                }
                rreshtatMeVlera.Add(rreshti);
            }

            return rreshtatMeVlera;
        }


        public static void ShtoCommandColumnNeDatabound(ASPxGridView grida, string columnCaption, string keyFieldName, bool allowSelectByRowClick = true, bool allowFocusedRow = true)
        {
            if (grida.Columns[columnCaption] != null) return;
            //behet nepermjet kodit afishimi i checkboxit qe do perdoret per
            //perzgjidh
            GridViewCommandColumn check = new GridViewCommandColumn(columnCaption)
            {
                ShowSelectCheckbox = true,
                Width = Unit.Percentage(2)
            };
            grida.Settings.ShowFilterRow = false;
            grida.Settings.ShowFilterBar = GridViewStatusBarMode.Hidden;
            grida.Settings.ShowFilterRowMenu = false;
            check.VisibleIndex = 0;
            grida.Columns.Insert(0, check);

            grida.KeyFieldName = keyFieldName;
            grida.SettingsBehavior.AllowSelectByRowClick = allowSelectByRowClick;
            grida.SettingsBehavior.AllowFocusedRow = allowFocusedRow;
        }

        public static void KtheKolonenNeComboNeGride(ASPxGridView grida, string emerKolone, string captionKolone, Action<GridViewDataComboBoxColumn> percaktoTemplate)
        {
            var col = grida.Columns[emerKolone];

            var cmb = new GridViewDataComboBoxColumn
            {
                FieldName = emerKolone,
                Name = emerKolone,
                Width = col.Width,
                Caption = captionKolone
            };
            cmb.HeaderStyle.BackColor = col.HeaderStyle.BackColor;
            cmb.VisibleIndex = col.VisibleIndex;

            grida.Columns.Remove(col);
            percaktoTemplate(cmb);
            grida.Columns.Add(cmb);
        }

        /// <summary>
        /// konfiguron nje kolone qe mbart nje id ne nje kolon combobox ,psh idKonfig => kombo me konfigurimet.
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="grida"></param>
        /// <param name="valueField"></param>
        /// <param name="descriptionField"></param>
        /// <param name="dataSource"></param>
        /// <param name="session"></param>
        /// <param name="komponente"></param>
        public static void KonfiguroCombo<T>(this ASPxGridView grida, string fieldName, string valueField, string textField, Func<T> funcDs, HttpSessionState session, string komponente, string guidString)
        {
            var sessionKey = SessionKeyUtils.MerrSessionKeyPerCmb(komponente, fieldName);
            var oldColumn = grida.Columns[fieldName];
            if (oldColumn == null)
                throw new MyException($"Kolona {fieldName} nuk gjendet ne griden {grida.ID} per komponentetn {komponente}");
            GridViewDataComboBoxColumn colNewCombo;
            T dataSource;
            if (typeof(GridViewDataComboBoxColumn) != oldColumn.GetType())
            {
                colNewCombo = KrijoGridViewDataComboBoxColumnSipasKolonesEkzistuese(oldColumn);
                grida.Columns.Remove(oldColumn);
                grida.Columns.Add(colNewCombo);

                colNewCombo.PropertiesComboBox.TextField = textField;
                colNewCombo.PropertiesComboBox.ValueField = valueField;
                colNewCombo.FieldName = fieldName;
                dataSource = funcDs.Invoke();
                colNewCombo.PropertiesComboBox.DataSource = dataSource;
                colNewCombo.PropertiesComboBox.AllowMouseWheel = true;
                colNewCombo.PropertiesComboBox.AllowNull = true;
                mySessionObjects.RuajNeSession(sessionKey, guidString, dataSource);
                return;
            }
            colNewCombo = ((GridViewDataComboBoxColumn)oldColumn);

            if (colNewCombo == null) throw new MyException($"Kolona {fieldName} ne griden {grida.ID} nuk mund te konvertohet ne GridViewDataComboBoxColumn");

            if (colNewCombo.PropertiesComboBox.DataSource != null) return;

            dataSource = mySessionObjects.MerrNgaSession<T>(session, sessionKey, guidString);
            if (dataSource == null)
            {
                dataSource = funcDs.Invoke();
                mySessionObjects.RuajNeSession(sessionKey, guidString, dataSource);
            }
            colNewCombo.PropertiesComboBox.DataSource = dataSource;
        }

        public static void KonfiguroComboMeItems(this ASPxGridView grid, string fieldName, Func<ListEditItemCollection> sourceItems)
        {

            var oldColumn = grid.Columns[fieldName];
            if (typeof(GridViewDataComboBoxColumn) != oldColumn.GetType())
            {
                var colnew = KrijoGridViewDataComboBoxColumnSipasKolonesEkzistuese(oldColumn);
                grid.Columns.Remove(oldColumn);
                grid.Columns.Add(colnew);
                colnew.PropertiesComboBox.Items.AddRange(sourceItems.Invoke());
                colnew.FieldName = fieldName;
            }
            else
            {
                var colnew = (GridViewDataComboBoxColumn)grid.Columns[fieldName];
                if (colnew.PropertiesComboBox.Items.Count != 0) return;
                colnew.PropertiesComboBox.Items.AddRange(sourceItems.Invoke());
            }
        }

        public static void AplikoFilterDefault(ASPxGridView grida, int idKonfigambjenti, ASPxComboBox cmbFiltra = null)
        {
            var filter = clsFiltraGrida.MerrFilterDefault(idKonfigambjenti);
            if (filter != null && filter.IdFiltra != 0)
            {
                grida.FilterExpression = filter.FiltraVlera;
                renditGriden(filter.KoloneRenditje, grida);
                
                if (cmbFiltra != null)
                {
                    cmbFiltra.Value = filter.FiltraKodi;
                    cmbFiltra.SelectedIndex = cmbFiltra.Items.IndexOfText(filter.FiltraKodi);
                }
            }
        }

        public static string GetSortedColumnFromFilterDefault(int idKonfigambjenti, clsFiltraGrida filter)
        {
            filter = filter != null ? filter : clsFiltraGrida.MerrFilterDefault(idKonfigambjenti);
            if (filter != null && filter.IdFiltra != 0 && !string.IsNullOrEmpty(filter.KoloneRenditje))
            {
                //string sortOrder = filter.DrejtimRenditje ? ColumnSortOrder.Ascending.ToString() : ColumnSortOrder.Descending.ToString();
                return filter.KoloneRenditje; // $"{filter.KoloneRenditje};{sortOrder}";
            }
            return string.Empty;
        }

        /// <summary>
        /// perdoret per te caktuar renditjen dhe nese te shfaqet apo jo nje kolone ne gride dhe aplikon filtrin default te kesaj gride per kete konfigurim nqs ka
        /// </summary>
        /// <param name="idNdermarrje">id e ndermarjes</param>
        /// <param name="emergride"></param>
        /// <param name="grid">grida</param>
        /// <param name="paramKodKonfigurimi">kodi i konfigurimit</param>
        /// <param name="paramIdKomponente">id e komponentes</param>
        public static void PercaktoVisibleColumnsGridSipasKodKonfigurimi(int idNdermarrje, string emergride, ASPxGridView grid, string paramKodKonfigurimi, string paramIdKomponente, int idGjuha, bool mefilterDefault = true, int idKategori = 0, bool meFormatNumri = false)
        {
            int idKonfigurim;
            if (paramKodKonfigurimi != "")
            {
                idKonfigurim = clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(paramKodKonfigurimi, idNdermarrje);
            }
            else
            {
                var colKonf = new colKonfigurimAmbjenti();
                colKonf.mbushKonfigDefaultSipasKomponentes(Int32.Parse(paramIdKomponente), idNdermarrje);
                var clsKonf = idKategori == 0 ? colKonf.FirstOrDefault() : colKonf.Find(konfig => konfig.IdKategori == idKategori);
                idKonfigurim = clsKonf == null || clsKonf.IdKonfigAmbjente == 0 ? 1 : clsKonf.IdKonfigAmbjente;
            }
            var colGrida = new colGridaTrupi(emergride, Int32.Parse(paramIdKomponente), idKonfigurim, idGjuha);
            foreach (var o in colGrida)
            {
                var col = grid.Columns[o.KodiTrupi] as GridViewDataColumn;

                if (col == null)
                {
                    ImbLogger.Error(
                        $"percaktoVisibleColumnsGridSipasKodKonfigurimi({idGjuha}, {idNdermarrje}, {emergride}, {paramKodKonfigurimi}, {paramIdKomponente}, {idGjuha}, {mefilterDefault}) - col == null, Grida == {emergride.ToString()}, Kolona == {o.KodiTrupi}");
                    continue;
                }

                col.HeaderStyle.Wrap = DefaultBoolean.True;
                col.Caption = o.PershkrimiTrupi;
                col.Name = o.KodiTrupi;
                col.VisibleIndex = o.IndexTrupi;
                col.Visible = o.VisibleTrupi;
                col.Width = Unit.Percentage(o.WidthTrupi);
                col.ShowInCustomizationForm = o.VisibleCostumize;
                col.PropertiesEdit.ClientInstanceName = o.KodiTrupi;
                col.Settings.ShowInFilterControl = DefaultBoolean.True;
                col.Settings.AutoFilterCondition = (TipKoloneBatchEdit)o.Tipi == TipKoloneBatchEdit.SpinEdit ? AutoFilterCondition.Equals : AutoFilterCondition.Contains;
                col.ReadOnly = o.ReadonlyTrupi;

                if (col is GridViewDataComboBoxColumn)
                {
                    var c = (col as GridViewDataComboBoxColumn).PropertiesComboBox;
                    c.AllowMouseWheel = true;
                    c.CallbackPageSize = 20;
                    c.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                    c.DropDownStyle = DropDownStyle.DropDownList;
                    c.FilterMinLength = 0;
                }

                if (meFormatNumri)
                    col.PropertiesEdit.DisplayFormatString = clsFormatKonfigTrup.KtheFormatStringSipasKonfigurimFormatNumri(idNdermarrje, idKonfigurim, (LlojFusheFormatNumri)o.LlojFormatFushe);
            }
            if (grid.Columns["#"] != null)
            {
                grid.Columns["#"].VisibleIndex = 0;
                grid.Columns["#"].Width = Unit.Percentage(2);
            }

            if (mefilterDefault)
            {
                if (idKonfigurim == 1)
                {
                    var filtri = new colFiltratGrida(colGrida[0].IdKoka, idNdermarrje).FirstOrDefault();
                    if (filtri != null && filtri.IdFiltra > 0)
                        AplikoFilter(idNdermarrje, grid, filtri.FiltraKodi, filtri.GridaKokaId);
                }
                else
                    AplikoFilterDefault(grid, idKonfigurim);
            }
        }

        public static void PercaktoVisibleColumnsAndTypesGridSipasKodKonfigurimi(int idNdermarrje, string emergride, ASPxGridView grid, string paramKodKonfigurimi, string paramIdKomponente, int idGjuha, bool mefilterDefault = true, string formatString = "n2")
        {
            string idkomponente;
            int idKonfigurim;

            if (paramKodKonfigurimi != "")
            {
                idkomponente = paramIdKomponente;
                idKonfigurim = clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(paramKodKonfigurimi, idNdermarrje);
            }
            else
            {
                idkomponente = paramIdKomponente;
                idKonfigurim = 1;
            }

            var colGrida = new colGridaTrupi(emergride, Int32.Parse(idkomponente), idKonfigurim, idGjuha);

            foreach (var o in colGrida)
            {
                var col = grid.Columns[o.KodiTrupi] as GridViewDataColumn;
                if (col == null)
                    continue;
                if (o.Tipi == (int)TipKoloneBatchEdit.SpinEdit)//spinedit
                {
                    var newCol = new GridViewDataSpinEditColumn
                    {
                        FooterTemplate = col.FooterTemplate,
                        HeaderTemplate = col.HeaderTemplate,
                        DataItemTemplate = col.DataItemTemplate,
                        EditItemTemplate = col.EditItemTemplate,
                        FilterTemplate = col.FilterTemplate,
                        PropertiesEdit =
                        {
                            DisplayFormatString = formatString,
                            ClientInstanceName = o.KodiTrupi
                        },
                        ReadOnly = o.ReadonlyTrupi,
                        Caption = o.PershkrimiTrupi,
                        Name = o.KodiTrupi,
                        FieldName = col.FieldName,
                        VisibleIndex = o.IndexTrupi,
                        Visible = o.VisibleTrupi,
                        Width = Unit.Percentage(o.WidthTrupi),
                        ShowInCustomizationForm = o.VisibleCostumize,
                    };

                    newCol.Settings.ShowInFilterControl = DefaultBoolean.True;
                    newCol.Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                    newCol.PropertiesSpinEdit.AllowMouseWheel = false;

                    grid.Columns.Remove(col);
                    grid.Columns.Add(newCol);
                    continue;
                }
                if (o.Tipi == (int)TipKoloneBatchEdit.SpinEditInteger)//spinedit
                {
                    var newCol = new GridViewDataSpinEditColumn
                    {
                        FooterTemplate = col.FooterTemplate,
                        HeaderTemplate = col.HeaderTemplate,
                        DataItemTemplate = col.DataItemTemplate,
                        EditItemTemplate = col.EditItemTemplate,
                        FilterTemplate = col.FilterTemplate,
                        PropertiesEdit =
                        {
                            DisplayFormatString = formatString,
                            ClientInstanceName = o.KodiTrupi
                        },
                        ReadOnly = o.ReadonlyTrupi,
                        Caption = o.PershkrimiTrupi,
                        Name = o.KodiTrupi,
                        FieldName = col.FieldName,
                        VisibleIndex = o.IndexTrupi,
                        Visible = o.VisibleTrupi,
                        Width = Unit.Percentage(o.WidthTrupi),
                        ShowInCustomizationForm = o.VisibleCostumize,
                    };

                    newCol.Settings.ShowInFilterControl = DefaultBoolean.True;
                    newCol.Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                    newCol.PropertiesSpinEdit.NumberType = SpinEditNumberType.Integer;
                    newCol.PropertiesSpinEdit.AllowMouseWheel = false;

                    grid.Columns.Remove(col);
                    grid.Columns.Add(newCol);
                    continue;
                }
                if (o.Tipi == (int)TipKoloneBatchEdit.ComboBox)
                {
                    var newCol = col as GridViewDataComboBoxColumn;

                    newCol.PropertiesComboBox.AllowMouseWheel = true;
                    newCol.PropertiesComboBox.CallbackPageSize = 20;
                    newCol.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                    newCol.PropertiesComboBox.DropDownStyle = DropDownStyle.DropDownList;
                    newCol.PropertiesComboBox.FilterMinLength = 0;
                    newCol.PropertiesComboBox.AllowMouseWheel = false;
                    newCol.PropertiesEdit.ClientInstanceName = o.KodiTrupi;
                    newCol.Settings.ShowInFilterControl = DefaultBoolean.True;
                    newCol.Settings.AutoFilterCondition = AutoFilterCondition.Contains;

                    newCol.ReadOnly = o.ReadonlyTrupi;
                    newCol.HeaderStyle.Wrap = DefaultBoolean.True;
                    newCol.Caption = o.PershkrimiTrupi;
                    newCol.Name = o.KodiTrupi;
                    newCol.FieldName = col.FieldName;
                    newCol.VisibleIndex = o.IndexTrupi;
                    newCol.Visible = o.VisibleTrupi;
                    newCol.Width = Unit.Percentage(o.WidthTrupi);
                    newCol.ShowInCustomizationForm = o.VisibleCostumize;

                    grid.Columns.Remove(col);
                    grid.Columns.Add(newCol);
                    continue;
                }
                if (o.Tipi == (int)TipKoloneBatchEdit.ComboBoxbuttonEdit)
                {
                    var newCol = col as GridViewDataComboBoxColumn;

                    newCol.PropertiesComboBox.AllowMouseWheel = true;
                    newCol.PropertiesComboBox.CallbackPageSize = 20;
                    newCol.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                    newCol.PropertiesComboBox.DropDownStyle = DropDownStyle.DropDownList;
                    newCol.PropertiesComboBox.FilterMinLength = 0;
                    newCol.PropertiesComboBox.DropDownButton.Visible = false;
                    newCol.PropertiesComboBox.AllowMouseWheel = false;
                    if (newCol.PropertiesComboBox.Buttons.Count == 0)
                        newCol.PropertiesComboBox.Buttons.Add();
                    newCol.PropertiesEdit.ClientInstanceName = o.KodiTrupi;
                    newCol.Settings.ShowInFilterControl = DefaultBoolean.True;
                    newCol.Settings.AutoFilterCondition = AutoFilterCondition.Contains;

                    newCol.ReadOnly = o.ReadonlyTrupi;
                    newCol.HeaderStyle.Wrap = DefaultBoolean.True;
                    newCol.Caption = o.PershkrimiTrupi;
                    newCol.Name = o.KodiTrupi;
                    newCol.FieldName = col.FieldName;
                    newCol.VisibleIndex = o.IndexTrupi;
                    newCol.Visible = o.VisibleTrupi;
                    newCol.Width = Unit.Percentage(o.WidthTrupi);
                    newCol.ShowInCustomizationForm = o.VisibleCostumize;

                    grid.Columns.Remove(col);
                    grid.Columns.Add(newCol);
                    continue;
                }

                if (o.Tipi == (int)TipKoloneBatchEdit.DateEdit)//dateedit
                {
                    var newCol = col as GridViewDataDateColumn;

                    newCol.PropertiesDateEdit.EditFormat = EditFormat.Custom;
                    newCol.PropertiesDateEdit.UseMaskBehavior = true;
                    newCol.PropertiesDateEdit.EditFormatString = "dd/MM/yyyy";
                    newCol.PropertiesDateEdit.DisplayFormatString = "dd/MM/yyyy";
                    newCol.PropertiesDateEdit.AllowMouseWheel = false;
                }

                if (o.Tipi == (int)TipKoloneBatchEdit.TimeEdit)
                {
                    var newCol = new GridViewDataTimeEditColumn
                    {
                        FooterTemplate = col.FooterTemplate,
                        HeaderTemplate = col.HeaderTemplate,
                        DataItemTemplate = col.DataItemTemplate,
                        EditItemTemplate = col.EditItemTemplate,
                        FilterTemplate = col.FilterTemplate
                    };

                    newCol.PropertiesTimeEdit.EditFormat = EditFormat.Custom;
                    newCol.PropertiesTimeEdit.AllowMouseWheel = true;
                    newCol.PropertiesTimeEdit.EditFormatString = "HH:mm";
                    newCol.PropertiesTimeEdit.DisplayFormatString = "HH:mm";
                    newCol.PropertiesTimeEdit.AllowMouseWheel = false;
                    newCol.PropertiesEdit.ClientInstanceName = o.KodiTrupi;
                    newCol.Settings.ShowInFilterControl = DefaultBoolean.True;
                    newCol.Settings.AutoFilterCondition = AutoFilterCondition.Contains;

                    newCol.ReadOnly = o.ReadonlyTrupi;
                    newCol.Caption = o.PershkrimiTrupi;
                    newCol.Name = o.KodiTrupi;
                    newCol.FieldName = col.FieldName;
                    newCol.VisibleIndex = o.IndexTrupi;
                    newCol.Visible = o.VisibleTrupi;
                    newCol.Width = Unit.Percentage(o.WidthTrupi);
                    newCol.ShowInCustomizationForm = o.VisibleCostumize;

                    grid.Columns.Remove(col);
                    grid.Columns.Add(newCol);
                    continue;
                }

                col.PropertiesEdit.ClientInstanceName = o.KodiTrupi;
                col.Settings.ShowInFilterControl = DefaultBoolean.True;
                col.Settings.AutoFilterCondition = AutoFilterCondition.Contains;

                col.ReadOnly = o.ReadonlyTrupi;
                col.Caption = o.PershkrimiTrupi;
                col.Name = o.KodiTrupi;
                col.VisibleIndex = o.IndexTrupi;
                col.Visible = o.VisibleTrupi;
                col.Width = Unit.Percentage(o.WidthTrupi);
                col.ShowInCustomizationForm = o.VisibleCostumize;
            }

            if (mefilterDefault)
                AplikoFilterDefault(grid, idKonfigurim);
        }

        public static void AplikoFilter(int idNdermarrje, ASPxGridView grida, string filtraKodi, int idGrida)
        {
            var filtra = new clsFiltraGrida();
            filtra.mbushFilterPerGrideSipasKodit(filtraKodi, idNdermarrje, idGrida);

            if (filtra.FiltraKodi == null) return;

            grida.FilterExpression = filtra.FiltraVlera;
            renditGriden(filtra.KoloneRenditje, grida);            
            //grida.SortBy(grida.Columns[filtra.KoloneRenditje], filtra.DrejtimRenditje
            //    ? ColumnSortOrder.Ascending
            //    : ColumnSortOrder.Descending);
        }

        public static void AplikoFilter(int idGjuha, int idNdermarrje, ASPxGridView grida, string filtraKodi, string emriGrides, string emerKomponente, int idKonfigurimi)
        {
            var koka = idKonfigurimi > 1
                ? new clsGridaKoka(idGjuha, emriGrides, emerKomponente, idNdermarrje, idKonfigurimi)
                : new clsGridaKoka(idGjuha, emriGrides, emerKomponente, idNdermarrje);

            AplikoFilter(idNdermarrje, grida, filtraKodi, koka.IdGridaKoka);
        }

        public static void renditGriden(string kolonaRenditje, ASPxGridView grida, string keyFieldName = "")
        {
            if (String.IsNullOrEmpty(kolonaRenditje))
                return;
            var kolonatRenditura = kolonaRenditje.Split(',');
            foreach (var kolRenditur in kolonatRenditura)
            {
                if (String.IsNullOrEmpty(kolRenditur))
                    continue;
                var kolona = kolRenditur.Split(' ')[0];
                if (String.IsNullOrEmpty(kolona))
                    continue;
                var renditja = kolRenditur.Split(' ')[1];
                if (grida.Columns[kolona] == null && String.IsNullOrEmpty(keyFieldName))
                    continue;
                if (grida.Columns[kolona] == null && !String.IsNullOrEmpty(keyFieldName))
                    kolona = keyFieldName;

                grida.SortBy(grida.Columns[kolona], renditja == "Ascending" ? ColumnSortOrder.Ascending : ColumnSortOrder.Descending);
            }
        }

        /// <summary>
        /// percakton atributet e grides per gridat e vogla te listimit ne te cilat behen dhe veprimet e shtimit dhe te modifikimit, por pa percaktuar temen
        /// </summary>
        /// <param name="grid">grida</param>
        /// <param name="KeyFieldName">celesi i grides</param>
        public static void konfiguroGrideListeEvogelPaTheme(ASPxGridView grid, String KeyFieldName, bool searchPanelVisible = true)
        {
            grid.Settings.ShowGroupPanel = true;
            grid.KeyFieldName = KeyFieldName;
            grid.SettingsPager.PageSize = 20;
            grid.Settings.ShowHeaderFilterButton = true;
            grid.SettingsBehavior.AllowFocusedRow = true;
            grid.Settings.ShowFilterRow = true;
            grid.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
            grid.Settings.ShowFilterRowMenu = true;
            grid.SettingsEditing.Mode = GridViewEditingMode.EditFormAndDisplayRow;
            grid.SettingsText.GroupPanel = MessagesResource.Messages["mesazhDragColumnHeader"];
            grid.SettingsText.EmptyDataRow = MessagesResource.Messages["lblNukKaTeDhena"];
            grid.SettingsCommandButton.CancelButton.Image.Url = "~/images/cancel1.png";
            grid.SettingsCommandButton.UpdateButton.Image.Url = "~/images/save_green.png";
            grid.SettingsCommandButton.UpdateButton.Image.Height = 20;
            grid.SettingsCommandButton.UpdateButton.Image.Width = 20;
            grid.SettingsCommandButton.CancelButton.Image.Height = 20;
            grid.SettingsCommandButton.CancelButton.Image.Width = 20;
            grid.SettingsCommandButton.CancelButton.Text = MessagesResource.Messages["labelAnullo"];
            grid.SettingsCommandButton.UpdateButton.Text = MessagesResource.Messages["buttonRuaj"];
            grid.SettingsCommandButton.UpdateButton.Image.AlternateText = MessagesResource.Messages["buttonRuaj"];
            grid.SettingsCommandButton.CancelButton.Image.AlternateText = MessagesResource.Messages["labelAnullo"];
            GridViewCommandColumn commandCol;
            if (grid.Columns["Action"] == null)
            {
                commandCol = new GridViewCommandColumn("Action");
            }
            else commandCol = grid.Columns["Action"] as GridViewCommandColumn;
            commandCol.Name = "Action";
            commandCol.Visible = false;
            commandCol.ButtonRenderMode = GridCommandButtonRenderMode.Image;
            if (searchPanelVisible)
                grid.KonfiguroSearchPanel();

            if (grid.Columns["Action"] == null)
                grid.Columns.Add(commandCol);
        }

        /// <summary>
        /// percakton atributet e grides per popup e vogla ne te cilat behen dhe veprimet e shtimit dhe updatimit, por pa percaktuar temen
        /// </summary>
        /// <param name="grid">grida</param>
        /// <param name="KeyFieldName">celesi i grides</param>
        public static void konfiguroGrideListeEvogelPopupiPaTheme(ASPxGridView grid, String KeyFieldName, bool endlessScroll)
        {
            grid.Settings.ShowGroupPanel = true;
            grid.KeyFieldName = KeyFieldName;
            grid.SettingsPager.PageSize = 10;
            grid.SettingsBehavior.AllowFocusedRow = true;
            grid.SettingsEditing.Mode = GridViewEditingMode.EditForm;
            grid.SettingsCommandButton.CancelButton.Image.Url = "~/images/cancel1.png";
            grid.SettingsCommandButton.UpdateButton.Image.Url = "~/images/save_green.png";
            grid.SettingsCommandButton.UpdateButton.Image.Height = 20;
            grid.SettingsCommandButton.UpdateButton.Image.Width = 20;
            grid.SettingsCommandButton.CancelButton.Image.Height = 20;
            grid.SettingsCommandButton.CancelButton.Image.Width = 20;
            grid.SettingsCommandButton.CancelButton.Text = MessagesResource.Messages["labelAnullo"];
            grid.SettingsCommandButton.UpdateButton.Text = MessagesResource.Messages["buttonRuaj"];
            grid.SettingsCommandButton.UpdateButton.Image.AlternateText = MessagesResource.Messages["buttonRuaj"];
            grid.SettingsCommandButton.CancelButton.Image.AlternateText = MessagesResource.Messages["labelAnullo"];

            GridViewCommandColumn commandCol = new GridViewCommandColumn("Action");
            commandCol.Name = "Action";
            commandCol.Visible = false;
            commandCol.ButtonRenderMode = GridCommandButtonRenderMode.Image;// ButtonType.Image;
            grid.Columns.Add(commandCol);
            grid.KonfiguroSearchPanel();
            //grid.AddNewRow();
            if (endlessScroll)
                grid.SettingsPager.Mode = GridViewPagerMode.EndlessPaging;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="ci"></param>
        /// <param name="KeyFieldName"></param>
        public static void konfigGrideListeEMadhePaTheme(ASPxGridView grid, String KeyFieldName, bool searchPanelVisible = true, bool LoadingPanelAktiv = true)
        {
            grid.Settings.ShowGroupPanel = true;
            grid.KeyFieldName = KeyFieldName;
            grid.SettingsPager.PageSize = 15;
            grid.Settings.ShowHeaderFilterButton = true;
            grid.SettingsBehavior.AllowFocusedRow = true;
            grid.Settings.ShowFilterRow = true;
            grid.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
            grid.Settings.ShowFilterRowMenu = true;
            grid.SettingsBehavior.EnableCustomizationWindow = true;
            grid.SettingsPopup.CustomizationWindow.Height = 250;
            grid.SettingsPopup.CustomizationWindow.HorizontalAlign = PopupHorizontalAlign.Center;
            grid.SettingsPopup.CustomizationWindow.VerticalAlign = PopupVerticalAlign.Middle;
            grid.SettingsPopup.CustomizationWindow.Width = 250;
            grid.SettingsResizing.ColumnResizeMode = ColumnResizeMode.NextColumn;
            grid.SettingsText.GroupPanel = MessagesResource.Messages["mesazhDragColumnHeader"];
            grid.SettingsText.EmptyDataRow = MessagesResource.Messages["lblNukKaTeDhena"];
            grid.SettingsText.CustomizationWindowCaption = MessagesResource.Messages["mesazhZgjidhniFushat"];
            grid.Settings.ShowTitlePanel = true;
            if (searchPanelVisible)
                grid.KonfiguroSearchPanel();
            if (!LoadingPanelAktiv)
                grid.SettingsLoadingPanel.Mode = GridViewLoadingPanelMode.Disabled;
        }
        
        //public static void konfigGrideListeEMadhePaThemePerKonfigurim(ASPxGridView grid, String KeyFieldName, bool searchPanelVisible = true, bool LoadingPanelAktiv = true)
        //{
        //    grid.Settings.ShowGroupPanel = true;
        //    grid.KeyFieldName = KeyFieldName;
        //    grid.SettingsPager.PageSize = 50;
        //    grid.Settings.ShowHeaderFilterButton = true;
        //    grid.SettingsBehavior.AllowFocusedRow = true;
        //    grid.Settings.ShowFilterRow = true;
        //    grid.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
        //    grid.Settings.ShowFilterRowMenu = true;
        //    grid.SettingsBehavior.EnableCustomizationWindow = true;
        //    grid.SettingsPopup.CustomizationWindow.Height = 250;
        //    grid.SettingsPopup.CustomizationWindow.HorizontalAlign = PopupHorizontalAlign.Center;
        //    grid.SettingsPopup.CustomizationWindow.VerticalAlign = PopupVerticalAlign.Middle;
        //    grid.SettingsPopup.CustomizationWindow.Width = 250;
        //    grid.SettingsResizing.ColumnResizeMode = ColumnResizeMode.NextColumn;
        //    grid.SettingsText.GroupPanel = MessagesResource.Messages["mesazhDragColumnHeader"];
        //    grid.SettingsText.EmptyDataRow = MessagesResource.Messages["lblNukKaTeDhena"];
        //    grid.SettingsText.CustomizationWindowCaption = MessagesResource.Messages["mesazhZgjidhniFushat"];
        //    grid.Settings.ShowTitlePanel = true;
        //    if (searchPanelVisible)
        //        grid.KonfiguroSearchPanel();
        //    if (!LoadingPanelAktiv)
        //        grid.SettingsLoadingPanel.Mode = GridViewLoadingPanelMode.Disabled;
        //}

        /// <summary>
        /// percakton atributet e grides per popup e medha, por pa percaktuar temen
        /// </summary>
        /// <param name="grid">grida</param>
        /// <param name="KeyFieldName">celesi i grides</param>
        public static void KonfiguroGrideListeMadhePopupiPaTheme(ASPxGridView grid, String KeyFieldName, bool filtrosaposhkruar, bool endlessScroll)
        {
            grid.KeyFieldName = KeyFieldName;
            grid.SettingsPager.PageSize = 10;
            grid.Settings.ShowHeaderFilterButton = true;
            grid.SettingsBehavior.AllowFocusedRow = true;
            grid.Settings.ShowFilterRow = true;
            grid.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
            grid.Settings.ShowFilterRowMenu = true;
            if (filtrosaposhkruar)
                grid.SettingsBehavior.FilterRowMode = GridViewFilterRowMode.Auto;
            else
                grid.SettingsBehavior.FilterRowMode = GridViewFilterRowMode.OnClick;
            grid.KonfiguroSearchPanel();
            if (endlessScroll)
                grid.SettingsPager.Mode = GridViewPagerMode.EndlessPaging;
        }

        public static void KonfiguroSearchPanel(this ASPxGridView grida)
        {
            grida.SettingsSearchPanel.Visible = true;
            grida.SettingsSearchPanel.GroupOperator = GridViewSearchPanelGroupOperator.And;
            grida.SettingsSearchPanel.ShowClearButton = false;
            grida.SettingsText.SearchPanelEditorNullText = MessagesResource.Messages["panelKerko"];
        }

        /// <summary>
        /// percakton atributet e grides per gridat e vogla, por pa perecaktuar temen
        /// </summary>
        /// <param name="grid">grida</param>
        /// <param name="KeyFieldName">celesi i grides</param>
        public static void konfiguroGrideRegjistrimEvogelPaTheme(ASPxGridView grid, String KeyFieldName, bool searchPanelVisible = true)
        {
            grid.SettingsEditing.NewItemRowPosition = GridViewNewItemRowPosition.Bottom;
            grid.Settings.UseFixedTableLayout = true;
            grid.KeyFieldName = KeyFieldName;
            //grid.CssFilePath = "~/App_Themes/BlackGlass/{0}/styles.css";
            //grid.CssPostfix = "BlackGlass";
            grid.SettingsBehavior.AllowFocusedRow = true;
            grid.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
            grid.SettingsEditing.Mode = GridViewEditingMode.Inline;
            grid.SettingsBehavior.AllowSort = false;
            grid.SettingsBehavior.AllowGroup = false;
            grid.SettingsText.GroupPanel = MessagesResource.Messages["mesazhDragColumnHeader"];
            grid.SettingsText.EmptyDataRow = MessagesResource.Messages["lblNukKaTeDhena"];
            if (searchPanelVisible)
                grid.KonfiguroSearchPanel();
        }

        /// <summary>
        /// percakton atribute per griden e shtimit, por pa percaktuar temen
        /// </summary>
        /// <param name="grid">grida</param>
        /// <param name="KeyFieldName">celesi i grides</param>
        public static void percaktoAtributeTeGridesShtoPaTheme(ASPxGridView grid, String KeyFieldName)
        { //percakton atribute te grides
            grid.KeyFieldName = KeyFieldName;
            //grid.CssFilePath = "~/App_Themes/BlackGlass/{0}/styles.css";
            //grid.CssPostfix = "BlackGlass";
            grid.SettingsPager.PageSize = 20;
            grid.SettingsBehavior.AllowSelectByRowClick = true;
            grid.SettingsBehavior.AllowFocusedRow = true;
            grid.SettingsEditing.Mode = GridViewEditingMode.Inline;
        }

        /// <summary>
        /// percakton kolonat e dukshme te grides per gridat e shtimit
        /// </summary>
        /// <param name="idGjuha"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="grid"> grida</param>
        /// <param name="emriGrides"> emri i grides</param>
        /// <param name="emriKomponentes">emri i komponentes</param>
        public static void percaktoVisibleColumnsShto(int idGjuha, int idNdermarrje, ASPxGridView grid, String emriGrides, String emriKomponentes)
        {
            clsGridaKoka koka = new clsGridaKoka(idGjuha, emriGrides, emriKomponentes, idNdermarrje);
            if (koka.IdGridaKoka == 0)
            {
                LogManager.GetCurrentClassLogger().Error($"percaktoVisibleColumnsShto({idGjuha}, {idNdermarrje}, {grid}, {emriGrides}, {emriKomponentes}) - koka.IdGridaKoka == 0");
                return;
            }
            //DbCore.DbAdmin.clsGridaKoka koka = dbAdmin.merrGridaKokaByEmri(emriGrides, emriKomponentes,  DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            koka.OColGridaTrupi.mbushTrupin(idGjuha, koka.IdGridaKoka);
            foreach (clsGridaTrupi gridaKolone in koka.OColGridaTrupi)
            {
                var gridViewColumn = grid.Columns[gridaKolone.KodiTrupi];
                if (gridViewColumn == null)
                {
                    LogManager.GetCurrentClassLogger().Error(String.Format("percaktoVisibleColumnsShto({0}, {1}, {2}, {3}, {4}) - gridViewColumn == null", idGjuha, idNdermarrje, grid, emriGrides, emriKomponentes));
                    return;
                }
                gridViewColumn.Caption = gridaKolone.PershkrimiTrupi;
                gridViewColumn.VisibleIndex = gridaKolone.IndexTrupi;
                gridViewColumn.Visible = gridaKolone.VisibleTrupi;
                GridViewDataColumn col = gridViewColumn as GridViewDataColumn;
                col.ReadOnly = gridaKolone.ReadonlyTrupi;
                col.PropertiesEdit.ClientInstanceName = gridaKolone.KodiTrupi;
                col.Settings.ShowInFilterControl = DefaultBoolean.True;
                col.Width = Unit.Percentage(gridaKolone.WidthTrupi);
            }
            grid.AddNewRow();
        }

        /// <summary>
        /// percakton kolonat e dukshme te grides
        /// </summary>
        /// <param name="idGjuha"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="grid">grida</param>
        /// <param name="emriGrides">emri i grides</param>
        /// <param name="emriKomponentes">emri i komponentes</param>
        public static void percaktoVisibleColumns(int idGjuha, int idNdermarrje, ASPxGridView grid, String emriGrides, String emriKomponentes)
        {
            //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            clsGridaKoka koka = new clsGridaKoka(idGjuha, emriGrides, emriKomponentes, idNdermarrje);
            if (koka.IdGridaKoka == 0)
            {
                LogManager.GetCurrentClassLogger().Error(String.Format("percaktoVisibleColumns({0}, {1}, {2}, {3}, {4}) - koka.IdGridaKoka == 0", idGjuha, idNdermarrje, grid, emriGrides, emriKomponentes));
                return;
            }
            koka.OColGridaTrupi.mbushTrupin(idGjuha, koka.IdGridaKoka);
            foreach (clsGridaTrupi gridaKolone in koka.OColGridaTrupi)
            {
                var gridViewColumn = grid.Columns[gridaKolone.KodiTrupi];
                if (gridViewColumn == null)
                {
                    LogManager.GetCurrentClassLogger().Error(String.Format("percaktoVisibleColumns({0}, {1}, {2}, {3}, {4}, {5} ) - gridViewColumn == null", idGjuha, idNdermarrje, grid, emriGrides, emriKomponentes, gridaKolone.KodiTrupi));
                    return;
                }
                gridViewColumn.Caption = gridaKolone.PershkrimiTrupi;
                gridViewColumn.VisibleIndex = gridaKolone.IndexTrupi;
                gridViewColumn.Visible = gridaKolone.VisibleTrupi;
                GridViewDataColumn col = gridViewColumn as GridViewDataColumn;
                col.ReadOnly = gridaKolone.ReadonlyTrupi;
                col.PropertiesEdit.ClientInstanceName = gridaKolone.KodiTrupi;
                col.Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                col.Settings.ShowInFilterControl = DefaultBoolean.True;
                col.CellStyle.Wrap = DefaultBoolean.False;
                col.ShowInCustomizationForm = gridaKolone.VisibleCostumize;
                //col.Width = Unit.Percentage(gridaKolone.WidthTrupi);
            }
        }

        /// <summary>
        /// percakton kolonat e dukshme te grides
        /// </summary>
        /// <param name="idGjuha"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="grid">grida</param>
        /// <param name="emriGrides">emri i grides</param>
        /// <param name="emriKomponentes">emri i komponentes</param>
        public static void percaktoVisibleColumnsMeWidth(int idGjuha, int idNdermarrje, ASPxGridView grid, String emriGrides, String emriKomponentes)
        {
            clsGridaKoka koka = new clsGridaKoka(idGjuha, emriGrides, emriKomponentes, idNdermarrje);
            if (koka.IdGridaKoka == 0)
            {
                throw new MyException(String.Format("percaktoVisibleColumnsMeWidth({0}, {1}, {2}, {3}, {4}) - koka.IdGridaKoka == 0", idGjuha, idNdermarrje, grid, emriGrides, emriKomponentes));
            }
            koka.OColGridaTrupi.mbushTrupin(idGjuha, koka.IdGridaKoka);
            foreach (clsGridaTrupi gridaKolone in koka.OColGridaTrupi)
            {
                var gridViewColumn = grid.Columns[gridaKolone.KodiTrupi];
                if (gridViewColumn == null)
                {
                    LogManager.GetCurrentClassLogger().Error(String.Format("percaktoVisibleColumnsMeWidth({0}, {1}, {2}, {3}, {4}, {5}) - gridViewColumn == null", idGjuha, idNdermarrje, grid, emriGrides, emriKomponentes, gridaKolone.KodiTrupi));
                    continue;
                    //  throw new MyException($"percaktoVisibleColumnsMeWidth({idGjuha}, {idNdermarrje}, {grid}, {emriGrides}, {emriKomponentes}) - gridaKolone.KodiTrupi: {gridaKolone.KodiTrupi}");                    
                }
                gridViewColumn.Caption = gridaKolone.PershkrimiTrupi;
                gridViewColumn.VisibleIndex = gridaKolone.IndexTrupi;
                gridViewColumn.Visible = gridaKolone.VisibleTrupi;

                GridViewDataColumn col = gridViewColumn as GridViewDataColumn;

                col.ReadOnly = gridaKolone.ReadonlyTrupi;
                col.ShowInCustomizationForm = gridaKolone.VisibleCostumize;
                col.PropertiesEdit.ClientInstanceName = gridaKolone.KodiTrupi;
                col.Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                col.Settings.ShowInFilterControl = DefaultBoolean.True;
                col.CellStyle.Wrap = DefaultBoolean.True;
                col.Width = Unit.Percentage(gridaKolone.WidthTrupi);
            }
        }

        public static void percaktoVisibleColumnsMeWidthPaVisibleIndex(int idGjuha, int idNdermarrje, ASPxGridView grid, String emriGrides, String emriKomponentes)
        {
            //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            clsGridaKoka koka = new clsGridaKoka(idGjuha, emriGrides, emriKomponentes, idNdermarrje);
            if (koka.IdGridaKoka == 0)
            {
                LogManager.GetCurrentClassLogger().Error(String.Format("percaktoVisibleColumnsMeWidthPaVisibleIndex({0}, {1}, {2}, {3}, {4}) - koka.IdGridaKoka == 0", idGjuha, idNdermarrje, grid, emriGrides, emriKomponentes));
                return;
            }
            koka.OColGridaTrupi.mbushTrupin(idGjuha, koka.IdGridaKoka);
            foreach (clsGridaTrupi gridaKolone in koka.OColGridaTrupi)
            {
                var gridViewColumn = grid.Columns[gridaKolone.KodiTrupi];
                if (gridViewColumn == null)
                {
                    LogManager.GetCurrentClassLogger().Error(String.Format("percaktoVisibleColumnsMeWidthPaVisibleIndex({0}, {1}, {2}, {3}, {4}) - gridViewColumn == null", idGjuha, idNdermarrje, grid, emriGrides, emriKomponentes));
                    return;
                }
                gridViewColumn.Caption = gridaKolone.PershkrimiTrupi;
                // grid.Columns[gridaKolone.KodiTrupi].VisibleIndex = gridaKolone.IndexTrupi;
                gridViewColumn.Visible = gridaKolone.VisibleTrupi;

                GridViewDataColumn col = gridViewColumn as GridViewDataColumn;

                col.ReadOnly = gridaKolone.ReadonlyTrupi;
                col.ShowInCustomizationForm = gridaKolone.VisibleCostumize;
                col.PropertiesEdit.ClientInstanceName = gridaKolone.KodiTrupi;
                col.Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                col.Settings.ShowInFilterControl = DefaultBoolean.True;
                col.CellStyle.Wrap = DefaultBoolean.True;
                col.Width = Unit.Percentage(gridaKolone.WidthTrupi);
            }
        }

        public static void percaktoVisibleColumnsGridSipasKodKonfigurimiPaVisibleIndex(int idNdermarrje, ASPxGridView grid, string paramKodKonfigurimi, string paramIdKomponente, int idGjuha)
        {
            string idkomponente = "";
            int idKonfigurim = -1;

            if (paramKodKonfigurimi != "")
            {
                idkomponente = paramIdKomponente;
                string kodkonfigurimi = paramKodKonfigurimi;
                clsKonfigurimAmbjenti clsKonf = new clsKonfigurimAmbjenti();
                clsKonf.mbushKonfiguriminMeKod(kodkonfigurimi, idNdermarrje, idGjuha);
                //DbCore.DbShare.colKonfigurimAmbjenti colKonf = share.ktheKonfiguriminMeKod(kodkonfigurimi, DbCore.clsFunksione.merrIdNdermarrjeSesioni(Session));
                idKonfigurim = clsKonf.IdKonfigAmbjente;
            }
            else
            {
                idkomponente = paramIdKomponente;
                clsKonfigurimAmbjenti clsKonf = new clsKonfigurimAmbjenti();
                clsKonf.mbushKonfigDefaultKomponentes(Int32.Parse(idkomponente), idNdermarrje);
                idKonfigurim = clsKonf.IdKonfigAmbjente;
            }
            colGridaTrupi colGrida = new colGridaTrupi(Int32.Parse(idkomponente), idKonfigurim, idGjuha);
            //DbCore.DbAdmin.colGridaTrupi colGrida = share.ktheGridenKonfigurimitKomponentes(int.Parse(idkomponente), idKonfigurim);
            foreach (clsGridaTrupi o in colGrida)
            {

                var gridViewColumn = grid.Columns[o.KodiTrupi];
                if (gridViewColumn == null)
                {
                    LogManager.GetCurrentClassLogger().Error(String.Format("percaktoVisibleColumnsGridSipasKodKonfigurimiPaVisibleIndex({0}, {1}, {2}, {3}, {4}) - col == null", idNdermarrje, grid, paramKodKonfigurimi, paramIdKomponente, idGjuha));
                    return;
                }
                gridViewColumn.Caption = o.PershkrimiTrupi;
                gridViewColumn.Visible = o.VisibleTrupi;
                GridViewDataColumn col = gridViewColumn as GridViewDataColumn;
                col.ReadOnly = o.ReadonlyTrupi;
                col.ShowInCustomizationForm = o.VisibleCostumize;
                col.PropertiesEdit.ClientInstanceName = o.KodiTrupi;
                col.Settings.ShowInFilterControl = DefaultBoolean.True;
                col.Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                col.Width = Unit.Percentage(o.WidthTrupi);
            }
        }

        public static void shtoModelAutomjeti(ASPxGridView grida, int idNdermarrje, HttpSessionState Session)
        {
            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            if (typeof(GridViewDataComboBoxColumn) != grida.Columns["ModelAutomjeti"].GetType())
            {
                //int indexi = ASPxGridView_Automjete.Columns["ModelAutomjeti"].VisibleIndex;
                grida.Columns.Remove(grida.Columns["ModelAutomjeti"]);
                grida.Columns.Add(colnew);
                //if (visibleIndex)
                //    colnew.VisibleIndex = indexi;
                colModeleAutomjetesh modelet = new colModeleAutomjetesh();
                modelet.Add(new clsModelAutomjeti(0, "", "", 0, 0, 0, 0));
                modelet.mbushModeleAutomjeteshSipasNdermarrjes(idNdermarrje);
                colnew.PropertiesComboBox.DataSource = modelet;
                colnew.PropertiesComboBox.TextField = "KodModelAutomjeti";
                colnew.PropertiesComboBox.ValueField = "IdModelAutomjeti";
                colnew.FieldName = "ModelAutomjeti";
                colnew.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                mySessionObjects.ruajDsComboGrideNeSession(Session, modelet, "colModelet");
            }
            else
            {
                colnew = (GridViewDataComboBoxColumn)grida.Columns["ModelAutomjeti"];
                //if (colnew.PropertiesComboBox.Items.Count == 0)
                colnew.PropertiesComboBox.DataSource = mySessionObjects.merrDsComboGrideNeSession(Session, "colModelet");
            }
        }

        /// <summary>
        /// percakton kolonat e dukshme te grides
        /// </summary>
        /// <param name="idGjuha"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="emriGrides">emri i grides</param>
        /// <param name="emriKomponentes">emri i komponentes</param>
        /// <returns> stringun me te dhenat mbi kolonat e grides</returns>
        public static colGridaTrupi percaktoVisibleColumnsGrid(int idGjuha, int idNdermarrje, String emriGrides, String emriKomponentes)
        {
            clsGridaKoka koka = new clsGridaKoka(idGjuha, emriGrides, emriKomponentes, idNdermarrje);
            if (koka.IdGridaKoka == 0)
                return koka.OColGridaTrupi;
            koka.OColGridaTrupi.mbushTrupin(idGjuha, koka.IdGridaKoka);
            return koka.OColGridaTrupi;
        }

        /// <summary>
        /// percakton kolonat e dukshme te grides per klient side
        /// </summary>
        /// <param name="emriKomponentes"> emri i komponentes</param>
        /// <param name="idKonfigurim"> id e konfigurimit</param>
        /// <returns> stringun me te dhenat mbi kolonat e grides</returns>
        public static string percaktoVisibleColumnsSipasKonfigurimitPerClientSide(String emriKomponentes, int idKonfigurim, int idGjuha)
        {
            string temp = "";

            clsKomponente oKomponente = new clsKomponente(emriKomponentes);
            colGridaTrupi trupiGrides = new colGridaTrupi(oKomponente.IdKomponente, idKonfigurim, idGjuha);
            foreach (clsGridaTrupi gridaKolone in trupiGrides)
            {
                temp = temp + gridaKolone.KodiTrupi + "|";
                temp = temp + gridaKolone.PershkrimiTrupi + "|";
                temp = temp + gridaKolone.VisibleTrupi + "|";
                temp = temp + gridaKolone.ReadonlyTrupi + "|";
                temp = temp + gridaKolone.WidthTrupi + "|";
                temp = temp + gridaKolone.IndexTrupi;
                temp = temp + "||";
            }
            return temp;
        }

        /// <summary>
        /// percakton kolonat e dukshme te grides sipas konfigurimit
        /// </summary>
        /// <param name="grid"> grida</param>
        /// <param name="emriGrides"> emri i grides</param>
        /// <param name="emriKomponentes"> emri i komponentes</param>
        /// <param name="idKonfigurim">id e konfigurimit</param>
        /// <param name="percaktoIndex">true nqs do i caktohet indexi, false ne te kundert</param>
        public static void percaktoVisibleColumnsSipasKonfigurimit(ASPxGridView grid, String emriGrides, String emriKomponentes, int idKonfigurim, bool percaktoIndex, int idGjuha, bool perdorWidthZero = false)
        {
            colGridaTrupi trupiGrides = new colGridaTrupi();
            trupiGrides.mbushGrideTrupinSipasEmerGrideDheKomponente(idKonfigurim, emriGrides, idGjuha, emriKomponentes);
            foreach (clsGridaTrupi gridaKolone in trupiGrides)
            {
                var col = grid.Columns[gridaKolone.KodiTrupi] as GridViewDataColumn;
                if (col == null)
                {
                    //Komentuar pasi mbushte file-in e logeve me shkrime te shumeta dhe ngadalsonte projektin
                    //ImbLogger.Error($"percaktoVisibleColumnsSipasKonfigurimit({emriGrides}, {emriKomponentes}, {idKonfigurim}, {percaktoIndex}, {idGjuha},{gridaKolone.KodiTrupi}) - col == null");
                    continue;
                }
                
                switch (idGjuha)
                {
                    case 0:
                        col.Caption = gridaKolone.PershkrimiTrupi;
                        break;
                    case 1:
                        col.Caption = gridaKolone.PershkrimiTrupi_en;
                        break;
                    case 2:
                        col.Caption = gridaKolone.PershkrimiTrupi_fr;
                        break;
                }
                if (percaktoIndex)
                    col.VisibleIndex = gridaKolone.IndexTrupi;

                col.ReadOnly = gridaKolone.ReadonlyTrupi;
                col.PropertiesEdit.ClientInstanceName = gridaKolone.KodiTrupi;
                col.Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                col.Settings.ShowInFilterControl = DefaultBoolean.True;
                col.CellStyle.Wrap = DefaultBoolean.True;
                if (perdorWidthZero && !gridaKolone.VisibleTrupi)
                    col.Width = Unit.Percentage(0);
                if (!perdorWidthZero)
                    col.Visible = gridaKolone.VisibleTrupi;
            }

        }

        /// <summary>
        /// ruan konfigurimin e grides dhe filtrat e zgjedhur nga perdoruesi
        /// </summary>
        /// <param name="grida"> grida</param>
        /// <param name="kodkonfigurimi">kodi i konfigurimit</param>
        /// <param name="idndermarje">id e ndermarjes</param>
        /// <param name="idperdoruesi">id e perdoruesit</param>
        /// <param name="idkomponente">id e komponentes</param>
        /// <param name="idfiltri">id e filtrit te grides</param>
        /// <param name="idViti">id e vitit</param>
        /// <returns>kthen nje objekt clsMesazh me statusin nqs ruajtja ka ndodhur me sukses apo jo </returns>
        /// <param name="cultinf"></param>
        public static clsMesazh ruajkonfigurimgride(ASPxGridView grida, string kodkonfigurimi, int idndermarje, int idperdoruesi, int idkomponente, int idfiltri, int idViti, CultureInfo cultinf, int idGjuha)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", Assembly.Load("App_GlobalResources"));

            clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin();
            dbAdmin.beginTransaksion();
            try
            {
                clsTeDrejtaRoli tedrejtaInfo = new clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idperdoruesi, idndermarje, idViti, "Konfigurime Gride");///kontrollon te drejtat

                if (!tedrejtaInfo.DAmb)
                    return new clsMesazh(false, rm.GetString("mesazhRaportNukKeniTeDrejta", cultinf));
                clsKonfigurimAmbjenti clsKonf = new clsKonfigurimAmbjenti();
                clsKonf.mbushKonfiguriminMeKod(kodkonfigurimi, idndermarje, idGjuha);
                colGridaTrupi trupiGrida = new colGridaTrupi(idkomponente, clsKonf.IdKonfigAmbjente, idGjuha);
                double totalipx = 0;
                double totaliper = 0;
                for (int i = 0; i < grida.VisibleColumns.Count; i++)
                {
                    if (grida.VisibleColumns[i].Width.Type == UnitType.Percentage)
                        totaliper += grida.VisibleColumns[i].Width.Value;
                    if (grida.VisibleColumns[i].Width.Type == UnitType.Pixel)
                        totalipx += grida.VisibleColumns[i].Width.Value;
                }
                for (int i = 0; i < trupiGrida.Count; i++)
                {
                    var col = grida.Columns[trupiGrida[i].KodiTrupi];
                    if (col != null)
                    {
                        trupiGrida[i].IndexTrupi = col.VisibleIndex;
                        trupiGrida[i].VisibleTrupi = col.Visible;
                        if (col.Width.Type == UnitType.Percentage)
                            trupiGrida[i].WidthTrupi = (int)col.Width.Value;
                        else if (col.Width.Type == UnitType.Pixel)
                        {
                            double width = totalipx;
                            if (totaliper != 0)
                                width = totalipx * 100 / (100 - totaliper);
                            width = col.Width.Value * 100 / width;
                            if (width < 0)
                                width = 1;
                            trupiGrida[i].WidthTrupi = (int)width;
                            if (trupiGrida[i].WidthTrupi == Int32.MinValue)
                                trupiGrida[i].WidthTrupi = 1;
                        }
                    }
                }
                clsKusht kusht = new clsKusht(clsKonf.IdKonfigAmbjente, "FILTER");///marim kushtin filter
                //kusht.Vlera = idfiltri;

                bool mod = trupiGrida.update(dbAdmin, idGjuha);///ruajme ndryshimet ne gride

                if (!mod)
                {
                    dbAdmin.rollbackTransaksion();
                    return new clsMesazh(false, rm.GetString("mesazhNdodhiNjeGabimGjateRuatjesSeKonfigurimit", cultinf));
                }
                clsDatabaseShare dbshare = new clsDatabaseShare(dbAdmin);

                clsMesazh mesazh;
                if (kusht.IdKusht == 0)
                    mesazh = clsKusht.krijoKushtTemplate(clsKonf.IdKonfigAmbjente, "FILTER", idfiltri, dbshare);
                else
                {
                    kusht.Vlera = idfiltri;
                    mesazh = kusht.modifiko(dbshare);/// ruajme kushtin e modifikuar
                }
                if (!mesazh.Status)
                {
                    dbAdmin.rollbackTransaksion(); return mesazh;
                }
                dbAdmin.commitTransaksion();
                return new clsMesazh(true, rm.GetString("mesazhFiltriDheKonfigurimiSuksess", cultinf));
            }
            catch (Exception)
            {
                dbAdmin.rollbackTransaksion();
                return new clsMesazh(false, rm.GetString("mesazhNdodhiNjeGabimGjateRuatjesSeKonfigurimit", cultinf));
            }
        }

        public static clsMesazh ruajkonfigurimgrideMeBanda(ASPxGridView grida, int idKonfigAmbjente, int idndermarje, int idperdoruesi, string emerKomponente, int idfiltri, int idViti, CultureInfo cultinf, int idGjuha)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", Assembly.Load("App_GlobalResources"));

            clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin();
            dbAdmin.beginTransaksion();
            try
            {
                clsTeDrejtaRoli tedrejtaInfo = new clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idperdoruesi, idndermarje, idViti, "Konfigurime Gride");///kontrollon te drejtat

                if (!tedrejtaInfo.DAmb)
                    return new clsMesazh(false, rm.GetString("mesazhRaportNukKeniTeDrejta", cultinf));

                clsGridaKoka gridaKoka = new clsGridaKoka(idGjuha, grida.ID, emerKomponente, idndermarje, idKonfigAmbjente);
                colGridaTrupi trupiGrida = new colGridaTrupi();
                trupiGrida.mbushTrupin(idGjuha, gridaKoka.IdGridaKoka);

                double totalipx = 0;
                double totaliper = 0;

                for (int i = 0; i < grida.AllColumns.Count; i++)
                {
                    GridViewColumn col = grida.AllColumns[i];
                    if (!(col is GridViewBandColumn) && col.Visible)
                    {

                        if (col.Width.Type == UnitType.Percentage)
                            totaliper += col.Width.Value;
                        if (col.Width.Type == UnitType.Pixel)
                            totalipx += col.Width.Value;

                    }
                }
                for (int i = 0; i < trupiGrida.Count; i++)
                {
                    var col = grida.AllColumns[trupiGrida[i].KodiTrupi];
                    if (col != null)
                    {
                        trupiGrida[i].IndexTrupi = col.VisibleIndex;
                        trupiGrida[i].VisibleTrupi = col.Visible;
                        if (col.Width.Type == UnitType.Percentage)
                            trupiGrida[i].WidthTrupi = (int)col.Width.Value;
                        else if (col.Width.Type == UnitType.Pixel)
                        {
                            double width = totalipx;
                            if (totaliper != 0)
                                width = totalipx * 100 / (100 - totaliper);
                            width = col.Width.Value * 100 / width;
                            if (width < 0)
                                width = 1;
                            trupiGrida[i].WidthTrupi = (int)width;
                            if (trupiGrida[i].WidthTrupi == Int32.MinValue)
                                trupiGrida[i].WidthTrupi = 1;
                        }
                    }
                }
                clsKusht kusht = new clsKusht(idKonfigAmbjente, "FILTER");///marim kushtin filter
                //kusht.Vlera = idfiltri;

                bool mod = trupiGrida.update(dbAdmin, idGjuha);///ruajme ndryshimet ne gride

                if (!mod)
                {
                    dbAdmin.rollbackTransaksion();
                    return new clsMesazh(false, rm.GetString("mesazhNdodhiNjeGabimGjateRuatjesSeKonfigurimit", cultinf));
                }
                clsDatabaseShare dbshare = new clsDatabaseShare(dbAdmin);

                clsMesazh mesazh;
                if (kusht.IdKusht == 0)
                    mesazh = clsKusht.krijoKushtTemplate(idKonfigAmbjente, "FILTER", idfiltri, dbshare);
                else
                {
                    kusht.Vlera = idfiltri;
                    mesazh = kusht.modifiko(dbshare);/// ruajme kushtin e modifikuar
                }
                if (!mesazh.Status)
                {
                    dbAdmin.rollbackTransaksion(); return mesazh;
                }
                dbAdmin.commitTransaksion();
                return new clsMesazh(true, rm.GetString("mesazhFiltriDheKonfigurimiSuksess", cultinf));
            }
            catch (Exception)
            {
                dbAdmin.rollbackTransaksion();
                return new clsMesazh(false, rm.GetString("mesazhNdodhiNjeGabimGjateRuatjesSeKonfigurimit", cultinf));
            }
        }

        /// <summary>
        /// Vendos emrat e butonave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="grid">Merr griden te ciles do ti perkthejme butonat</param>
        /// <param name="cultinf">marrim culture info</param>
        /// <param name="rm"> marrim resource manager per lidhjen me resource strings</param>
        public static void EmrateButonaveMbiGride(ASPxGridView grid)
        {
            (grid.FindTitleTemplateControl("ASPxButton2") as ASPxButton).Text = MessagesResource.Messages["btnAdministrimiZgjidhKolonat"];
            (grid.FindTitleTemplateControl("ASPxButton3") as ASPxButton).Text = MessagesResource.Messages["btnBlerjeShitjeRuajKolonat"];
        }

        /// <summary>
        /// percakton kolonat e dukshme te grides per klient side
        /// </summary>
        /// <param name="emriGrides"> emri i grides</param>
        /// <param name="emriKomponentes"> emri i komponentes</param>
        /// <param name="idKonfigurim"> id e konfigurimit</param>
        /// <returns> stringun me te dhenat mbi kolonat e grides</returns>
        public static colGridaTrupi percaktoVisibleColumnsSipasKonfigurimitPerClientSide2(String emriGrides, String emriKomponentes, int idKonfigurim, int idGjuha)
        {
            clsKomponente oKomponente = new clsKomponente(emriKomponentes);
            colGridaTrupi trupiGrides = new colGridaTrupi(emriGrides, oKomponente.IdKomponente, idKonfigurim, idGjuha);
            return trupiGrides;
        }

        /// <summary>
        /// Vendos ToolTip te butonave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="grid">Merr griden te ciles do ti perkthejme butonat</param>
        /// <param name="cultinf">marrim culture info</param>
        /// <param name="rm"> marrim resource manager per lidhjen me resource strings</param>
        public static void ToolTipButonaveMbiGride(ASPxGridView grid, CultureInfo cultinf, ResourceManager rm)
        {
            if ((grid.FindTitleTemplateControl("ASPxButton2") as ASPxButton) != null)
            {
                (grid.FindTitleTemplateControl("ASPxButton2") as ASPxButton).ToolTip = rm.GetString("btnAdministrimiZgjidhKolonat", cultinf);
            }

            if ((grid.FindTitleTemplateControl("ASPxButton3") as ASPxButton) != null)
            {
                (grid.FindTitleTemplateControl("ASPxButton3") as ASPxButton).ToolTip = rm.GetString("btnBlerjeShitjeRuajKolonat", cultinf);
            }
            if ((grid.FindTitleTemplateControl("gridaSelectFaqe") as ASPxButton) != null)
            {
                (grid.FindTitleTemplateControl("gridaSelectFaqe") as ASPxButton).ToolTip = rm.GetString("btnZgjidhTeGjitheFaqen", cultinf);
            }
            if ((grid.FindTitleTemplateControl("gridaSelectTeGjitha") as ASPxButton) != null)
            {
                (grid.FindTitleTemplateControl("gridaSelectTeGjitha") as ASPxButton).ToolTip = rm.GetString("btnZgjidhTeGjithe", cultinf);
            }
            if ((grid.FindTitleTemplateControl("gridaUnSelectTeGjitha") as ASPxButton) != null)
            {
                (grid.FindTitleTemplateControl("gridaUnSelectTeGjitha") as ASPxButton).ToolTip = rm.GetString("btnFshiZgjedhjen", cultinf);
            }
            if ((grid.FindTitleTemplateControl("btnXlsxExport") as ASPxButton) != null)
            {
                (grid.FindTitleTemplateControl("btnXlsxExport") as ASPxButton).ToolTip = rm.GetString("btnExportToXlsx", cultinf);
            }
            if ((grid.FindTitleTemplateControl("btnPdfExport") as ASPxButton) != null)
            {
                (grid.FindTitleTemplateControl("btnPdfExport") as ASPxButton).ToolTip = rm.GetString("btnExportToPdf", cultinf);
            }
            if ((grid.FindTitleTemplateControl("konvertimetBtn") as ASPxButton) != null)
            {
                (grid.FindTitleTemplateControl("konvertimetBtn") as ASPxButton).ToolTip = rm.GetString("regjisDokToolTipProcedimProdhimi", cultinf);
            }
            if ((grid.FindTitleTemplateControl("pagesaBtn") as ASPxButton) != null)
            {
                (grid.FindTitleTemplateControl("pagesaBtn") as ASPxButton).ToolTip = rm.GetString("RaportPorosiDealerTitulli", cultinf);
            }
            if ((grid.FindTitleTemplateControl("filterDefault") as ASPxButton) != null)
            {
                (grid.FindTitleTemplateControl("filterDefault") as ASPxButton).ToolTip = rm.GetString("regjisDokBtnToolTipAplikoFilterDefault", cultinf);
            }
            if ((grid.FindTitleTemplateControl("lblPeriudha") as ASPxLabel) != null)
            {
                (grid.FindTitleTemplateControl("lblPeriudha") as ASPxLabel).Text = rm.GetString("RadioButtonListEditItemPeriudha", cultinf);
            }

            if ((grid.FindTitleTemplateControl("ASPxButton1") as ASPxButton) != null)
            {
                (grid.FindTitleTemplateControl("ASPxButton1") as ASPxButton).Text = rm.GetString("btnZgjidhTeGjithe", cultinf);          ///ASPxButton1
            }

            if ((grid.FindTitleTemplateControl("gidaSelectMbrapa") as ASPxButton) != null)
            {
                (grid.FindTitleTemplateControl("gidaSelectMbrapa") as ASPxButton).Text = rm.GetString("GridaZgjidhMbrapa", cultinf);
            }
            if ((grid.FindTitleTemplateControl("btnHiqZgjedhjen") as ASPxButton) != null)
            {
                (grid.FindTitleTemplateControl("btnHiqZgjedhjen") as ASPxButton).Text = rm.GetString("hiqZgjedhjenBtn", cultinf);
            }
            if ((grid.FindTitleTemplateControl("btnRefresh") as ASPxButton) != null)
            {
                (grid.FindTitleTemplateControl("btnRefresh") as ASPxButton).Text = rm.GetString("btnRefresh", cultinf);
            }
            if ((grid.FindTitleTemplateControl("radDtDok") as ASPxRadioButtonList) != null)
            {
                ASPxRadioButtonList radDtDok = grid.FindTitleTemplateControl("radDtDok") as ASPxRadioButtonList;
                radDtDok.Items[0].Text = rm.GetString("RadioButtonListEditItemDitore", cultinf);
                radDtDok.Items[1].Text = rm.GetString("RadioButtonListEditItemJavore", cultinf);
                radDtDok.Items[2].Text = rm.GetString("RadioButtonListEditItemAktuale", cultinf);
                radDtDok.Items[3].Text = rm.GetString("RadioButtonListEditItem3Mujore", cultinf);
                radDtDok.Items[4].Text = rm.GetString("RadioButtonListEditItemVitUshtrimor", cultinf);
            }
        }

        public static void perktheButonaGride(IDictionary<string, object> hfState, CultureInfo cultinf)
        {
            clsFunksione.ShtoPerkthimNeHfState(hfState, "JQgridShtoFurnitor", "JQgridShtoKlient", "JQgridModifikoArtikull", "JQgridShtoArtikull", "JQgridRuajKolona", "JQgridZgjidhKolona", "JQgridShikoPerberesit", "JQgridShtoArtikullAqt", "JQgridGrupimPerberesit", "JQgridImazhe", "JQgridExport");
        }

        public static void konfiguroGridaPerBatchEditing(ASPxGridView grid, bool allowInsert = true, bool allowDelete = true, bool showFilter = true, bool showConfirmMessage = true, int pageSize = 20, bool aplikoHeaderStyle = true, bool allowGroup = false, bool newRowOnTop = true)
        {
            if (grid == null) return;
            grid.SettingsBehavior.ColumnResizeMode = ColumnResizeMode.NextColumn;

            grid.ClientInstanceName = grid.ID;
            grid.AutoGenerateColumns = true;

            grid.Settings.ShowFilterBar = showFilter ? GridViewStatusBarMode.Visible : GridViewStatusBarMode.Hidden;
            grid.Settings.ShowFilterRow = showFilter;
            grid.Settings.ShowFooter = true;
            grid.Settings.ShowFilterRowMenu = showFilter;

            grid.SettingsBehavior.AllowSelectByRowClick = false;
            // grid.SettingsBehavior.AllowSelectSingleRowOnly = true;

            grid.SettingsBehavior.FilterRowMode = GridViewFilterRowMode.Auto;
            grid.SettingsBehavior.AllowFocusedRow = false;
            grid.SettingsBehavior.AllowGroup = allowGroup;
            grid.SettingsDataSecurity.AllowDelete = allowDelete;
            grid.SettingsDataSecurity.AllowEdit = true;
            grid.SettingsDataSecurity.AllowInsert = allowInsert;
            grid.SettingsEditing.NewItemRowPosition = newRowOnTop ? GridViewNewItemRowPosition.Top : GridViewNewItemRowPosition.Bottom;
            grid.SettingsEditing.BatchEditSettings.ShowConfirmOnLosingChanges = showConfirmMessage;
            grid.SettingsText.ConfirmOnLosingBatchChanges = "Jeni i sigurt qe doni te largoheni pa ruajtur ndryshimet?";
            grid.SettingsEditing.Mode = GridViewEditingMode.Batch;
            grid.SettingsEditing.BatchEditSettings.StartEditAction = GridViewBatchStartEditAction.Click;
            grid.SettingsEditing.BatchEditSettings.EditMode = GridViewBatchEditMode.Cell;
            grid.SettingsPager.PageSize = 20;
            //  grid.SettingsPager.Visible = false;
            grid.SettingsLoadingPanel.Mode = GridViewLoadingPanelMode.Disabled;
            grid.Styles.BatchEditModifiedCell.BackColor = Color.Transparent;
            grid.Styles.FocusedRow.BackColor = Color.Transparent;


            if (!aplikoHeaderStyle) return;
            foreach (GridViewColumn col in grid.AllColumns)
            {
                col.HeaderStyle.Wrap = DefaultBoolean.True;
                col.HeaderStyle.BackColor = Color.LightGray;
            }
        }

        public static clsMesazh ruajkonfigurimgridePaKonfigurim(ASPxGridView grida, int idndermarje, int idperdoruesi, string emerKomponente, int idViti, CultureInfo cultinf, int idGjuha)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", Assembly.Load("App_GlobalResources"));

            clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin();
            dbAdmin.beginTransaksion();
            try
            {
                clsTeDrejtaRoli tedrejtaInfo = new clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idperdoruesi, idndermarje, idViti, "Konfigurime Gride");///kontrollon te drejtat

                if (!tedrejtaInfo.DAmb)
                    return new clsMesazh(false, rm.GetString("mesazhRaportNukKeniTeDrejta", cultinf));

                clsGridaKoka gridaKoka = new clsGridaKoka(idGjuha, grida.ID, emerKomponente, idndermarje, 1);
                colGridaTrupi trupiGrida = new colGridaTrupi();
                trupiGrida.mbushTrupin(idGjuha, gridaKoka.IdGridaKoka);

                double totalipx = 0;
                double totaliper = 0;
                for (int i = 0; i < grida.VisibleColumns.Count; i++)
                {
                    if (grida.VisibleColumns[i].Width.Type == UnitType.Percentage)
                        totaliper += grida.VisibleColumns[i].Width.Value;
                    if (grida.VisibleColumns[i].Width.Type == UnitType.Pixel)
                        totalipx += grida.VisibleColumns[i].Width.Value;
                }
                for (int i = 0; i < trupiGrida.Count; i++)
                {
                    trupiGrida[i].IndexTrupi = grida.Columns[trupiGrida[i].KodiTrupi].VisibleIndex;
                    trupiGrida[i].VisibleTrupi = grida.Columns[trupiGrida[i].KodiTrupi].Visible;
                    if (grida.Columns[trupiGrida[i].KodiTrupi].Width.Type == UnitType.Percentage)
                        trupiGrida[i].WidthTrupi = (int)grida.Columns[trupiGrida[i].KodiTrupi].Width.Value;
                    else if (grida.Columns[trupiGrida[i].KodiTrupi].Width.Type == UnitType.Pixel)
                    {
                        double width = totalipx;
                        if (totaliper != 0)
                            width = totalipx * 100 / (100 - totaliper);
                        width = grida.Columns[trupiGrida[i].KodiTrupi].Width.Value * 100 / width;
                        if (width < 0)
                            width = 1;
                        trupiGrida[i].WidthTrupi = (int)width;
                        if (trupiGrida[i].WidthTrupi == Int32.MinValue)
                            trupiGrida[i].WidthTrupi = 1;
                    }
                }

                bool mod = trupiGrida.update(dbAdmin, idGjuha);///ruajme ndryshimet ne gride

                if (!mod)
                {
                    dbAdmin.rollbackTransaksion();
                    return new clsMesazh(false, rm.GetString("mesazhNdodhiNjeGabimGjateRuatjesSeKonfigurimit", cultinf));
                }

                dbAdmin.commitTransaksion();
                return new clsMesazh(true, rm.GetString("mesazhKonfigurimiSuksess", cultinf));
            }
            catch (Exception)
            {
                dbAdmin.rollbackTransaksion();
                return new clsMesazh(false, rm.GetString("mesazhNdodhiNjeGabimGjateRuatjesSeKonfigurimit", cultinf));
            }
        }

        /// <summary>
        /// ruan filtrin e zgjedhur ne gride
        /// </summary>
        /// <param name="idNdermarrje">id e ndermarjes</param>
        /// <param name="idPerdoruesi">id e perdoruesit</param>
        /// <param name="idGjuha">id e gjuhes</param>
        /// <param name="emergride">emeri i grides</param>
        /// <param name="emerkomponente">emri i komponentes</param>
        /// <param name="kodfiltri">kodi i filtrit</param>
        /// <param name="filtriexp">filtri</param>
        /// <param name="grida">grida</param>
        /// <param name="renditjedefault">kolona e renditjes default nqs grida nuk eshte e renditur</param>
        /// <param name="idfiltri">parameter output id e filtrit te ruajtur</param>
        /// <returns>kthen nje objekt clsMesazh qe tregon nese ruajtja eshte kryer me sukses apo jo</returns>
        public static clsMesazh ruajFiltra(int idNdermarrje, int idPerdoruesi, int idGjuha, string emergride, string emerkomponente, string kodfiltri, string filtriexp, ASPxGridView grida, string renditjedefault, int idkonf, out int idfiltri, ASPxHiddenField hfState = null)
        {
            clsGridaKoka koka = new clsGridaKoka(idGjuha, emergride, emerkomponente, idNdermarrje, idkonf);
            clsFiltraGrida filtri = new clsFiltraGrida() { FiltraKodi = kodfiltri, FiltraShenime = kodfiltri, FiltraUniversal = false, GridaKokaId = koka.IdGridaKoka, FiltraVlera = filtriexp, IdPerdoruesi = idPerdoruesi, IdNdermarje = idNdermarrje, IdStatusDok = 1 };
            filtri.KoloneRenditje = ktheKolRenditjeNgaGridaPerRuajtje(renditjedefault, grida, hfState);
            filtri.DrejtimRenditje = true; //kjo kolone s'do perdoret me
            //ReadOnlyCollection<GridViewDataColumn> kolona = grida.GetSortedColumns();
            //var kolonatRenditura = String.Empty;
            //if (kolona.Count > 0)
            //{
            //    foreach(GridViewDataColumn kol in kolona)
            //    {
            //        kolonatRenditura += kol.FieldName + " " + kol.SortOrder + ",";
            //    }
            //    filtri.KoloneRenditje = kolonatRenditura; //kolona[0].FieldName;
            //    if (kolona[0].SortOrder == ColumnSortOrder.Ascending)
            //        filtri.DrejtimRenditje = true;
            //    else
            //        filtri.DrejtimRenditje = false;
            //}
            //else
            //    if (hfState != null && hfState.Contains("sortColumn") && hfState.Get("sortColumn") != null)
            //    {
            //        if (hfState.Get("sortColumn") != null) {
            //            var sortColumn = hfState.Get("sortColumn").ToString().Split(';');
            //            filtri.KoloneRenditje = sortColumn[0];
            //            filtri.DrejtimRenditje = sortColumn[1] != "Descending";
            //        }   
            //    }
            //else
            //{
            //    filtri.KoloneRenditje = renditjedefault;
            //    filtri.DrejtimRenditje = true;
            //}

            clsFiltraGrida filtraekzistues = new clsFiltraGrida();
            if (kodfiltri == "FilterDefault")   //nqs filtri eshte filtri default qe ruhet tek konfigurimi i dokumentit kontrollojme nqs ky filter ekziston dhe e modifikojme. filtri default eshte filtri i konfigurimit. filtrat e tjere jane filtrat qe shfaqen tek comboja lart e filtrave qe i ruan perdoruesi
            {
                filtraekzistues.mbushFilterPerGrideSipasKodit("FilterDefault", idNdermarrje, koka.IdGridaKoka);
            }
            clsMesazh mesazh;
            if (filtraekzistues.IdFiltra > 0)
            {
                filtri.IdFiltra = filtraekzistues.IdFiltra;
                mesazh = filtri.modifiko();
            }
            else

                mesazh = filtri.ruaj();
            idfiltri = filtri.IdFiltra;
            return mesazh;
        }

        public static string ktheKolRenditjeNgaGridaPerRuajtje(string renditjedefault, ASPxGridView grida, ASPxHiddenField hfState = null)
        {
            string kolonaPerRenditje = String.IsNullOrWhiteSpace(renditjedefault) ? "" : renditjedefault + " " + "Ascending";

            ReadOnlyCollection<GridViewDataColumn> kolona = grida.GetSortedColumns();
            if (kolona.Count > 0)
            {
                var kolonatRenditura = String.Empty;
                foreach (GridViewDataColumn kol in kolona)
                {
                    kolonatRenditura += kol.FieldName + " " + kol.SortOrder + ",";
                }
                kolonaPerRenditje = kolonatRenditura;
            }
            else if (hfState != null && hfState.Contains("sortColumn") && hfState.Get("sortColumn") != null)
            {
                if (hfState.Get("sortColumn") != null)
                {
                    //var sortColumn = hfState.Get("sortColumn").ToString().Split(';');
                    kolonaPerRenditje = hfState.Get("sortColumn").ToString(); // sortColumn[0];
                }
            }

            return kolonaPerRenditje;
        }

        #region PIVOTGRID
        public static void HideEmptyValues(PivotCustomFieldValueCellsEventArgs e)
        {
            HideEmptyValues(true, e);
            HideEmptyValues(false, e);
        }
        public static void HideEmptyValues(bool isColumn, PivotCustomFieldValueCellsEventArgs e)
        {
            for (int i = e.GetCellCount(isColumn) - 1; i >= 0; i--)
            {
                FieldValueCell cell = e.GetCell(isColumn, i);
                if (cell == null) continue;
                if (cell.EndLevel == e.GetLevelCount(isColumn) - 1)
                {
                    if (IsValueEmpty(isColumn, cell.MaxIndex, e))
                    {
                        e.Remove(cell);
                    }
                }
            }
        }

        private static bool IsValueEmpty(bool isColumn, int valueIndex, PivotCustomFieldValueCellsEventArgs e)
        {
            if (isColumn)
                return IsCollumnEmpty(valueIndex, e);
            return IsRowEmpty(valueIndex, e);
        }

        private static bool IsRowEmpty(int rowIndex, PivotCustomFieldValueCellsEventArgs e)
        {
            for (int j = 0; j < e.ColumnCount; j++)
            {
                decimal value;
                if (Decimal.TryParse(Convert.ToString(e.GetCellValue(j, rowIndex) ?? 0), out value) && value != 0)
                    return false;
            }
            return true;
        }

        private static bool IsCollumnEmpty(int columnIndex, PivotCustomFieldValueCellsEventArgs e)
        {

            for (int j = 0; j < e.RowCount; j++)
            {
                decimal value;
                if (Decimal.TryParse(Convert.ToString(e.GetCellValue(columnIndex, j) ?? 0), out value) && value != 0)
                    return false;
            }
            return true;
        }
        #endregion PIVOTGRID

        #region gridViewFunctions

        public static void GridHeaderFilterFillItem(Object s, ASPxGridViewHeaderFilterEventArgs e, ResourceManager rm, CultureInfo ci, params string[] fieldNames)
        {
            string TeGjithe = rm.GetString("GridHeaderFilterFillItemTeGjithe", ci);
            string nga = rm.GetString("GridHeaderFilterFillItemNga", ci);
            if (fieldNames.Contains(e.Column.FieldName))
            {
                e.Values.Clear();
                e.AddValue(TeGjithe, String.Empty, "true");
                e.AddValue(nga + "A-D ", String.Empty, e.Column.FieldName + ">'A     ' and " + e.Column.FieldName + " <'DDDDDDD'");
                e.AddValue(nga + "D-G ", String.Empty, e.Column.FieldName + ">'D     ' and " + e.Column.FieldName + "<'GGGGGGG'");
                e.AddValue(nga + " H-K ", String.Empty, e.Column.FieldName + ">'H     ' and " + e.Column.FieldName + "<'KKKKKKK'");
                e.AddValue(nga + " L-O ", String.Empty, e.Column.FieldName + ">'L     ' and " + e.Column.FieldName + "  <'OOOOOOO'");
                e.AddValue(nga + " P-S ", String.Empty, e.Column.FieldName + ">'P     ' and " + e.Column.FieldName + "<'SSSSSSS'");
                e.AddValue(nga + "T-W ", String.Empty, e.Column.FieldName + ">'T     ' and " + e.Column.FieldName + "<'WWWWWWW'");
                e.AddValue(nga + " X-Z ", String.Empty, e.Column.FieldName + ">'X     ' and " + e.Column.FieldName + "<'ZZZZZZZ'");
            }
            else
            {
                e.Values.Clear();
                e.AddValue(TeGjithe, String.Empty, "true");
            }
        }

        public static void GridDataBound(object s, EventArgs e, ASPxGridView gridView, string keyFieldName)
        {
            if (gridView.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per 
                //perzgjidh
                GridViewCommandColumn check = new GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true; check.Width = Unit.Percentage(2);
                // check.SetColVisibleIndex(0);
                //behet per te afishuar rreshtin qe do sherbej per filtrim
                gridView.Settings.ShowFilterRow = true;
                gridView.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                gridView.Settings.ShowFilterRowMenu = true;
                gridView.Columns.Add(check);

                gridView.KeyFieldName = keyFieldName;
                gridView.SettingsBehavior.AllowSelectByRowClick = true;
                gridView.SettingsBehavior.AllowFocusedRow = true;
                gridView.Columns["#"].VisibleIndex = 0;
            }
        }

        public static void GridCustomJsProperties(object s, ASPxGridViewClientJSPropertiesEventArgs e, ASPxGridView gridView)
        {
            e.Properties["cpPageIndex"] = gridView.PageIndex;
            e.Properties["cpPageRow"] = gridView.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gridView.VisibleRowCount;
        }

        public static void GridAfterPerformCallback(object s, ASPxGridViewAfterPerformCallbackEventArgs e, ASPxGridView gridView, ASPxMenu menu)
        {
            if (e.CallbackName == "COLUMNMOVE" && gridView.AllColumns[Int32.Parse(e.Args[0])].Width.Value == 0)
                gridView.AllColumns[Int32.Parse(e.Args[0])].Width = Unit.Percentage(3);
            if (e.CallbackName == "APPLYFILTER" && e.Args[0] == "")
            {
                MenuItem itemButton = menu.Items.FindByName("TemplatedItemFilter");
                ASPxComboBox cmbFiltra = ((UserControl)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
                cmbFiltra.Text = "";
                gridView.Selection.UnselectAll();
            }
        }

        public static void GridCustomCallbackDefault(object sender, ASPxGridViewCustomCallbackEventArgs e, ASPxGridView gridView, string komponente, int idNdermarrja, int idGjuha, ASPxComboBox cmbKonfigurimi, ref HiddenField hfStatusi)
        {
            var arr = e.Parameters.Split(';');
            switch (arr.Length)
            {
                case 3:
                    if (arr[2] == "")
                        gridView.FilterExpression = "";
                    else
                    {
                        var filtra = new clsFiltraGrida();
                        clsGridaKoka koka;
                        if (cmbKonfigurimi != null)
                            koka = new clsGridaKoka(idGjuha, gridView.ID, komponente, idNdermarrja, Int32.Parse(cmbKonfigurimi.Value.ToString()));
                        else
                            koka = new clsGridaKoka(idGjuha, gridView.ID, komponente, idNdermarrja);
                        filtra.mbushFilterPerGrideSipasKodit(arr[2], idNdermarrja, koka.IdGridaKoka);

                        if (filtra.FiltraKodi != null)
                        {
                            gridView.FilterExpression = filtra.FiltraVlera;
                            GridUtil.renditGriden(filtra.KoloneRenditje, gridView, gridView.KeyFieldName);
                            //if (filtra.DrejtimRenditje)
                            //    gridView.SortBy(gridView.Columns[filtra.KoloneRenditje] != null ? gridView.Columns[filtra.KoloneRenditje] : gridView.Columns[gridView.KeyFieldName], ColumnSortOrder.Ascending);
                            //else
                            //    gridView.SortBy(gridView.Columns[filtra.KoloneRenditje] != null ? gridView.Columns[filtra.KoloneRenditje] : gridView.Columns[gridView.KeyFieldName], ColumnSortOrder.Descending);

                            // konfiguroVleraFillestare();
                            hfStatusi.Value = "true";
                        }
                        else hfStatusi.Value = "false";
                    }
                    break;
                case 2:
                    var kodkonfigurimi = arr[1];
                    var idkomponente = arr[0];
                    PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrja, gridView.ID, gridView, kodkonfigurimi, idkomponente, idGjuha);
                    break;
            }
            gridView.Selection.UnselectAll();
        }

        #endregion

  
    }
}