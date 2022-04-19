using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Threading;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using DbCore;
using DbCore.DbAdmin;
using DbCore.DbShare;
using DevExpress.Web;
using Newtonsoft.Json;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;

namespace PlatinumWeb.Templates
{
    //TO DO : te rregullohet qe ikonat te vendosen sipas temes,dhe tooltipi sipas gjuhes
    public class MyBaseTitlePanelTemplate : ITemplate
    {
        #region konstante per hidden field ose session

        protected const string gridaKokaKey = "gridaKokaKeyHfState";

        #endregion

        protected int idPerdoruesi;
        protected int idNdermarrje;
        protected int idViti;
        protected int idGjuha;
        protected int idKomponente;
        protected int idKonfig;
        protected clsGridaKoka gridaKoka;
        protected string emerKomponente;
        protected ASPxMenu menuInfo;
        protected string renditjeDefault;
        protected UpdatePanel pnlMesazhi;
        protected ASPxHiddenField hfState;
        protected Page CurrentPage;
        protected ResourceManager rm;
        protected CultureInfo cultinf;
        protected bool meFilterPeriudhe;
        protected bool meExpandCollapse;
        protected bool vetemSelectButtons;
        protected bool wysiwygExportOption;
        protected bool butonPdf;
        public bool VetemPeriudha { get; set; }

        #region kontrollet
        protected ASPxButton btnRuajKolonat;
        protected ASPxButton btnZgjidhkol;
        #endregion

        private ASPxGridViewExporter _exporter;
        protected ScriptManager ScriptManager;

        /// <summary>
        /// konstruktori i templatet 
        /// </summary>
        /// <param name="menuMesazhesh">  
        /// ASPxMenu-ja ne te cilen do shtohet mesazhi ne lidhje me ruajtjen e konfigurimit
        /// </param>
        /// <param name="pnlMesazhi">      UpdatePanel-i ne te cilin ndodhet menuja e mesazheve </param>
        /// <param name="cmbKonfig">      
        /// ASPxComboBox i cili ka llojet e konfigurimit(do perdoret per ruajtjen e konfigurimeve)
        /// </param>
        /// <param name="idPerdoruesi">    idperdoruesi </param>
        /// <param name="idNdermarrje">    idNdermmarje </param>
        /// <param name="idViti">         </param>
        /// <param name="idGjuha">        </param>
        /// <param name="komponente">      emri i komponentes ku jemi </param>
        /// <param name="idKomponente">    id-ja e saj </param>
        /// <param name="renditjeDefault"> fusha sipas te ciles behet renditja default </param>
        /// <param name="page">           
        /// Page,do perdoret per te shtuar kontrollet qe nuk mund te shtohen ne grid
        /// </param>
        public MyBaseTitlePanelTemplate(string emriGrides, Page page, ASPxMenu menuMesazhesh, UpdatePanel pnlMesazhi, ASPxHiddenField hfState, int idkonfigAmbjenti, int idPerdoruesi, int idNdermarrje, int idViti, int idGjuha, string komponente, int idKomponente, string renditjeDefault, ResourceManager rm, CultureInfo cultinf, bool expandCollapse = false, GridViewExportedRowType exportType = GridViewExportedRowType.Selected, bool vetemSelectButtons = false, bool wysiwygExportOption = true, bool vetemPeriudha = false, bool butonPdf=true)
        {
            menuInfo = menuMesazhesh;
            VetemPeriudha = vetemPeriudha;
            this.idKomponente = idKomponente;
            emerKomponente = komponente;
            this.renditjeDefault = renditjeDefault;
            this.pnlMesazhi = pnlMesazhi;
            this.idPerdoruesi = idPerdoruesi;
            this.idNdermarrje = idNdermarrje;
            idKonfig = idkonfigAmbjenti;
            this.idViti = idViti;
            this.idGjuha = idGjuha;
            CurrentPage = page;
            this.cultinf = cultinf;
            this.rm = rm;
            this.hfState = hfState;
            this.vetemSelectButtons = vetemSelectButtons;
            this.wysiwygExportOption = wysiwygExportOption;
            meExpandCollapse = expandCollapse;
            this.butonPdf = butonPdf;

            ExportRowType = exportType;

            if (!CurrentPage.IsPostBack)
            {
                gridaKoka = new clsGridaKoka(emriGrides, komponente, idNdermarrje, idKonfig);
                RuajGridaKokaNeHfState();
            }
            else
                gridaKoka = JsonConvert.DeserializeObject<clsGridaKoka>(hfState.Get(gridaKokaKey).ToString());
            ScriptManager = ScriptManager.GetCurrent(CurrentPage);

        }

        public ASPxGridView Grid { get; set; }

        /// <summary>
        /// eksportuesi i grides
        /// </summary>
        public ASPxGridViewExporter Exporter
        {
            get
            {
                if (_exporter != null) return _exporter;
                _exporter = new ASPxGridViewExporter
                {
                    ExportedRowType = ExportRowType,
                    GridViewID = Grid.ID
                };
                CurrentPage.Form.Controls.Add(_exporter);
                return _exporter;
            }
            set { _exporter = value; }
        }

        protected HttpSessionState Session => HttpContext.Current.Session;

        public GridViewExportedRowType ExportRowType { get; }

        public virtual void InstantiateIn(Control container)
        {
            Grid = ((GridViewTitleTemplateContainer)container).Grid;
            var tbl = KrijoTable(1, 8);

            MbushTableMeControle(tbl, BaseControls());
            container.Controls.Add(tbl);
        }

        /// <summary>
        /// kthen nje liste me controllet baze,te cilet i ka cdo grid 
        /// </summary>
        /// <returns></returns>
        protected List<Control> BaseControls()
        {
            var baseControls = new List<Control>();
            if (VetemPeriudha)
                return baseControls;

            var btnExpandCollapse = new ASPxButton();


            var btnExportPdf = new ASPxButton();
            var btnExportXcls = new ASPxButton();
            var btnSelectFaqe = new ASPxButton();
            var btnSelectAll = new ASPxButton();
            var btnUnselect = new ASPxButton();
            var pnlRuaj = new UpdatePanel();

            if (!vetemSelectButtons)
            {
                btnExpandCollapse.AutoPostBack = false;
                btnExpandCollapse.ToolTip = "Expand/Collapse";
                btnExpandCollapse.Image.Url = $"images/theme/{CurrentPage.Theme}/grida/expand.png";
                btnExpandCollapse.Image.UrlHottracked = $"images/theme/{CurrentPage.Theme}/grida/expand_W.png";
                btnExpandCollapse.Image.Height = 16;
                btnExpandCollapse.Font.Size = 8;
                btnExpandCollapse.ClientSideEvents.Click = $"function(s,e){{Utils.ExpandCollapse({Grid.ClientInstanceName},hfState);}}";
                btnExpandCollapse.Visible = meExpandCollapse;
                baseControls.Add(btnExpandCollapse);

                InicializoBtnZgjidhKolonat();
                pnlRuaj.ContentTemplateContainer.Controls.Add(btnZgjidhkol);

                InicializoBtnRuajKonfigurimet();
                pnlRuaj.ContentTemplateContainer.Controls.Add(btnRuajKolonat);
                baseControls.Add(pnlRuaj);
            } 
            btnSelectFaqe.ToolTip = rm.GetString("btnZgjidhTeGjitheFaqen", cultinf);
            btnSelectFaqe.AutoPostBack = false;
            btnSelectFaqe.Image.Url = $"images/theme/{CurrentPage.Theme}/grida/check2.png";
            btnSelectFaqe.Image.UrlHottracked = $"images/theme/{CurrentPage.Theme}/grida/check2_W.png";
            btnSelectFaqe.Image.Height = 16;
            btnSelectFaqe.Font.Size = 8;
            btnSelectFaqe.ClientSideEvents.Click = $"function(s, e) {{ {Grid.ClientInstanceName}.SelectAllRowsOnPage(); }}";
            baseControls.Add(btnSelectFaqe);

            btnSelectAll.ToolTip = rm.GetString("btnZgjidhTeGjithe", cultinf);
            btnSelectAll.AutoPostBack = false;
            btnSelectAll.Image.Url = $"images/theme/{CurrentPage.Theme}/grida/checks.png";
            btnSelectAll.Image.UrlHottracked = $"images/theme/{CurrentPage.Theme}/grida/checks_W.png";
            btnSelectAll.Image.Height = 16;
            btnSelectAll.Font.Size = 8;
            btnSelectAll.ClientSideEvents.Click = $"function(s, e) {{ {Grid.ClientInstanceName}.SelectRows(); }}";
            baseControls.Add(btnSelectAll);

            btnUnselect.ToolTip = rm.GetString("btnFshiZgjedhjen", cultinf);
            btnUnselect.AutoPostBack = false;
            btnUnselect.Image.Url = $"images/theme/{CurrentPage.Theme}/grida/uncheck2.png";
            btnUnselect.Image.UrlHottracked = $"images/theme/{CurrentPage.Theme}/grida/uncheck2_W.png";
            btnUnselect.Image.Height = 16;
            btnUnselect.Font.Size = 8;
            btnUnselect.ClientSideEvents.Click = $"function(s, e) {{ {Grid.ClientInstanceName}.UnselectRows(); }}";
            baseControls.Add(btnUnselect);

            if(vetemSelectButtons == false) { 

            btnExportXcls.ToolTip = rm.GetString("btnExportToXlsx", cultinf);
            btnExportXcls.Image.Url = $"images/theme/{CurrentPage.Theme}/grida/xlsx24.png";
            btnExportXcls.Image.UrlHottracked = $"images/theme/{CurrentPage.Theme}/grida/xlsx24_W.png";
            btnExportXcls.Image.Height = 16;
            btnExportXcls.UseSubmitBehavior = false;

            btnExportXcls.Click += btnXlsxExport_Click;
            baseControls.Add(btnExportXcls);
             if (butonPdf)
            {
            btnExportPdf.ToolTip = rm.GetString("btnExportToPdf", cultinf);
            btnExportPdf.Image.Url = $"images/theme/{CurrentPage.Theme}/grida/pdf_icon.png";
            btnExportPdf.Image.UrlHottracked = $"images/theme/{CurrentPage.Theme}/grida/pdf_icon_W.png";
            btnExportPdf.Image.Height = 16;
            btnExportPdf.UseSubmitBehavior = false;
            btnExportPdf.Click += btnPdfExport_Click;
            baseControls.Add(btnExportPdf);
            }

            if (ExportRowType == GridViewExportedRowType.Selected)
            {
                btnExportXcls.ClientSideEvents.Click = $"function(s,e){{if ({Grid.ClientInstanceName}.GetSelectedRowCount() == 0){{myMesazh.ShtoMesazhGabimi(hfState.Get('msgZgjdhniNjeNgaElementetEListes'));e.processOnServer=false;}}}}";
                btnExportPdf.ClientSideEvents.Click = $"function(s,e){{if ({Grid.ClientInstanceName}.GetSelectedRowCount() == 0){{myMesazh.ShtoMesazhGabimi(hfState.Get('msgZgjdhniNjeNgaElementetEListes'));e.processOnServer=false;}}}}";
            }


            ScriptManager.RegisterPostBackControl(btnExportPdf);
            ScriptManager.RegisterPostBackControl(btnExportXcls);
            }

            return baseControls;
        }

        private void InicializoBtnZgjidhKolonat()
        {
            btnZgjidhkol = new ASPxButton();
            btnZgjidhkol.ToolTip = rm.GetString("btnAdministrimiZgjidhKolonat", cultinf);
            btnZgjidhkol.AutoPostBack = false;
            btnZgjidhkol.ClientVisible = false;
            btnZgjidhkol.Image.Url = $"images/theme/{CurrentPage.Theme}/grida/wrench.png";
            btnZgjidhkol.Image.UrlHottracked = $"images/theme/{CurrentPage.Theme}/grida/wrench_W.png";
            btnZgjidhkol.Font.Size = 8;
			btnZgjidhkol.Image.Height = 16;
            btnZgjidhkol.ClientSideEvents.Init = "myFaqeCelje.InitTeDrejtaKonf";
            btnZgjidhkol.ClientSideEvents.Click = $"function(s,e){{myFaqeCelje.buttonKonfiguroClick(s,e,{Grid.ClientInstanceName}, {idKomponente}, {idKonfig}, {gridaKoka.CustomCustomizationWindow.ToString().ToLower()})}}";
            hfState.Set("msgZgjdhniNjeNgaElementetEListes", rm.GetString("msgZgjdhniNjeNgaElementetEListes", cultinf));

        }

        private void InicializoBtnRuajKonfigurimet()
        {
            btnRuajKolonat = new ASPxButton();
            btnRuajKolonat.ToolTip = rm.GetString("btnBlerjeShitjeRuajKolonat", cultinf);
            btnRuajKolonat.AutoPostBack = true;
            btnRuajKolonat.ClientVisible = false;
            btnRuajKolonat.Image.Url = $"images/theme/{CurrentPage.Theme}/grida/disk_blue (3).png";
            btnRuajKolonat.Image.UrlHottracked = $"images/theme/{CurrentPage.Theme}/grida/disk_blue (3)_W.png";
            btnRuajKolonat.Font.Size = 8;
			btnRuajKolonat.Image.Height = 16;
            btnRuajKolonat.Click += RuajKolona_Click;
            btnRuajKolonat.ClientSideEvents.Init = "myFaqeCelje.InitTeDrejtaKonf";

        }

        /// <summary>
        /// mbush tabelen me controllet 
        /// </summary>
        /// <param name="tbl">     </param>
        /// <param name="controls"></param>
        protected void MbushTableMeControle(Table tbl, params List<Control>[] controls)
        {
            var indexitd = 0;
            foreach (var controlbrenda in controls.SelectMany(listMeKontrolle => listMeKontrolle))
            {
                tbl.Rows[0].Cells[indexitd].Controls.Add(controlbrenda);
                indexitd++;
            }
        }


        protected Table KrijoTable(int numRows, int numCells)
        {
            var tbl = new Table();
            for (var i = 0; i < numRows; i++)
            {
                tbl.Controls.Add(new TableRow());
                for (var j = 0; j < numCells; j++)
                    tbl.Rows[i].Controls.Add(new TableCell());
            }
            return tbl;
        }

        /// <summary>
        /// ruan ose riruan griden ne hfState,psh nese eshte bere nje modifikim
        /// </summary>
        protected void RuajGridaKokaNeHfState()
        {
            hfState.Set(gridaKokaKey, JsonConvert.SerializeObject(gridaKoka));
        }
        #region ClientSideEvents


        #endregion



        #region FUNKSIONE GRIDE



        #endregion



        #region EVENTET E KONTROLLEVE TE HEADERIT

        protected void btnXlsxExport_Click(object sender, EventArgs e)
        {
            try
            {
                var emri = GjejEmrinPerExport();
                if (wysiwygExportOption)
                    Exporter.WriteXlsxToResponse(emri, true, new DevExpress.XtraPrinting.XlsxExportOptionsEx() { ExportType = DevExpress.Export.ExportType.WYSIWYG });
                else
                    Exporter.WriteXlsxToResponse(emri, true);
            }
            catch (ThreadAbortException)
            {
                //eshte i lejueshem
            }
        }

        protected void btnPdfExport_Click(object sender, EventArgs e)
        {
            try
            {
                var emri = GjejEmrinPerExport();
                GridUtil.ExportPdfFitToPage(Exporter, CurrentPage.Response, emri);
            }
            catch (ThreadAbortException)
            {
                //eshte i lejueshem
            }
        }

        private string GjejEmrinPerExport()
        {
            return hfState.Contains("EmriFile") ? hfState.Get("EmriFile").ToString() : string.IsNullOrEmpty(Grid.ToolTip) ? Grid.ID : Grid.ToolTip;
        }

        protected void RuajKolona_Click(object sender, EventArgs e)
        {


            var idfiltri = 0;

            var mesazh = GridUtil.ruajFiltra(idNdermarrje, idPerdoruesi, idGjuha, Grid.ID, emerKomponente, "FilterDefault", Grid.FilterExpression, Grid, renditjeDefault, idKonfig, out idfiltri, hfState);//ruan filtrin e zgjedhur tek filtrat
            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(menuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                return;
            }
            if (idKonfig == 0 || idKonfig == 1)
            {
                GridUtil.ruajkonfigurimgridePaKonfigurim(Grid, idNdermarrje, idPerdoruesi, emerKomponente, idViti, cultinf, idGjuha);
            }
            else
                mesazh = GridUtil.ruajkonfigurimgrideMeBanda(Grid, idKonfig, idNdermarrje, idPerdoruesi, emerKomponente, idfiltri, idViti, cultinf, idGjuha);

            if (mesazh.Status)
                clsMenuInfo.ShtoMesazhSuksesi(menuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhGabimi(menuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
        }



        #endregion EVENTET E KONTROLLEVE TE HEADERIT
    }
}