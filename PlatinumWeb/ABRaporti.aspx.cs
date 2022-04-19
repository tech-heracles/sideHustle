using DbCore;
using DbCore.DbAdmin;
using DbCore.DbAnalizeBuxheti;
using DevExpress.Web;
using PlatinumWeb.Templates;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class ABRaporti : MyPageBase
    {
        private const string komponenteDefault = "ABRaporti.aspx";
        System.Globalization.CultureInfo ci;
        System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));


        private string raporti;
        public int IdNdermarrje
        {
            get { return (int)hfState.Get("idNdermarrje"); }
            set { hfState.Set("idNdermarrje", value); }
        }

        public int IdKonfig { get { return 1; } }
        public int IdPerdoruesi
        {
            get { return (int)hfState.Get("idPerdoruesi"); }
            set { hfState.Set("idPerdoruesi", value); }
        }
        public int IdGjuha
        {
            get { return (int)hfState.Get("idGjuha"); }
            set { hfState.Set("idGjuha", value); }

        }
        public int IdViti
        {
            get { return (int)hfState.Get("idViti"); }
            set { hfState.Set("idViti", value); }
        }
        public int IdNdermVit
        {
            get { return (int)hfState.Get("idNdermVit"); }
            set { hfState.Set("idNdermVit", value); }
        }
        public string KomponenteRaporti
        {
            get { return (string)hfState.Get("komponenteRaporti"); }
            set { hfState.Set("komponenteRaporti", value); }
        }
        public int KomponenteRaportiID
        {
            get { return (int)hfState.Get("komponenteRaportiID"); }
            set { hfState.Set("komponenteRaportiID", value); }
        }
        public string EmriFile
        {
            get { return (string)hfState.Get("EmriFile"); }
            set { hfState.Set("EmriFile", value); }
        }
        public bool Raportuese
        {
            get { return (bool)hfState.Get("Raportuese"); }
            set { hfState.Set("Raportuese", value); }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            base.Page_PreInit(sender, e);
            raporti = Request.QueryString["raporti"];

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
            IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + IdPerdoruesi);
            }
         

            if (!IsPostBack)
            {
                IdNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                IdViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
                IdGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
                IdNdermVit = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
                KomponenteRaporti = clsFunksione.GetKomponente(Request);
                clsKomponente kompon = new clsKomponente(KomponenteRaporti);
                KomponenteRaportiID = kompon.IdKomponente;
                Raportuese = clsNdermarrje.EshteRaportuese(IdNdermarrje);
                
                EmriFile = kompon.PershkrimiKomponente_sq;

                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrje, IdViti, KomponenteRaporti);
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DPlot.ToString();
                hfTeDrejta.Add("DAmb", tedrejtaInfo.DAmb);

                
                konfiguroKontrollet();
                mbushGridenNgaDB();

                konfiguroGride();

            }
            else
            {
          
                mbushGrideNgaSession();
                if (KomponenteRaportiID == 3014 || KomponenteRaportiID == 3029 || KomponenteRaportiID == 3013 || KomponenteRaportiID == 3038)
                {
                    gvRaporti.GroupBy(gvRaporti.Columns["KODI"]);
                    krijoGrupSummary();
                }
                krijoTotalSummary();
            }
           
            ci = DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo(IdGjuha);
            gvRaporti.PercaktoTitlePanel( this, MenuInfo, pnlMesazhi, hfState, IdPerdoruesi, IdNdermarrje, IdViti, IdGjuha, IdKonfig, KomponenteRaporti, KomponenteRaportiID, "RreshtiId", rm, ci, false, GridViewExportedRowType.Selected, false, false, false);
            percaktoTemplateMenu();

            gvRaporti.SettingsBehavior.ColumnResizeMode = DevExpress.Web.ColumnResizeMode.Control;
            gvRaporti.Settings.HorizontalScrollBarMode = DevExpress.Web.ScrollBarMode.Auto;
        }

       
        /// <summary>
        /// mbush datasourcein e grides direkt nga db
        /// </summary>
        /// <param name="idNdermarrje"></param>
        private void mbushGridenNgaDB()
        {
            DataTable dt=null;

            Dictionary<string, Object> parametra = new Dictionary<string, Object>();
            parametra.Add("idndermarrje", IdNdermarrje);
            parametra.Add("idndermvit", IdNdermVit);
            if (KomponenteRaportiID == 3038)
                shtoFiltraPerRaportin(parametra);
            dt = AnalizeBuxheti.MerrTeDhenatERaportiPerGride(raporti, parametra, Raportuese);
                
            
            gvRaporti.DataSource = dt;
            gvRaporti.DataBind();
            

            mySessionObjects.ruajObjectNeSesion(Session, dt, "dataSourceRaporti");
        }

        private void shtoFiltraPerRaportin(Dictionary<string, object> parametra)
        {
            string ndermarrjet = ((ASPxComboBox)navBarFiltrat.Groups[0].FindControl("cmbNdermarrje")).Text;
            string periudha = ((ASPxComboBox)navBarFiltrat.Groups[0].FindControl("cmbPeriudha")).Value.ToString();

            parametra.Add("periudha", periudha);
            ndermarrjet = String.IsNullOrWhiteSpace(ndermarrjet) ? "" : $"'{ndermarrjet.Replace(",", "', '")}'";
            parametra.Add("ndermarrje", ndermarrjet);
            
        }



        /// <summary>
        /// merr ds e grides nga sessioni
        /// </summary>
        public void mbushGrideNgaSession()
        {
            
            DataTable dt = mySessionObjects.merrObjectNgaSesioni(Session, "dataSourceRaporti") as DataTable;

            if (dt == null)
            {

                Dictionary<string, Object> parametra = new Dictionary<string, Object>();
                parametra.Add("idndermarrje", IdNdermarrje);
                parametra.Add("idndermvit", IdNdermVit);
                if (KomponenteRaportiID == 3038)
                    shtoFiltraPerRaportin(parametra);
                dt = AnalizeBuxheti.MerrTeDhenatERaportiPerGride(raporti, parametra, Raportuese);

                mySessionObjects.ruajObjectNeSesion(Session, dt, "dataSourceRaporti");
            }
            gvRaporti.DataSource = dt;
            gvRaporti.DataBind();
        }

        private void konfiguroGride()
        {

            GridUtil.PercaktoVisibleColumnsAndTypesGridSipasKodKonfigurimi(IdNdermarrje, "gvRaporti", gvRaporti, string.Empty, KomponenteRaportiID.ToString(), IdGjuha);
            if (KomponenteRaportiID == 3014 || KomponenteRaportiID == 3029 || KomponenteRaportiID == 3013 || KomponenteRaportiID == 3038)
            {
                gvRaporti.GroupBy(gvRaporti.Columns["KODI"]);
                krijoGrupSummary();
            }
                    
            vendosBandaPerKolonat();
            VendosEmerPerKolonat();
           
            ShtoCheckColumn();
            GridUtil.PercaktoStilHeaderiPerKolona(gvRaporti);
            GridUtil.konfigGrideListeEMadhePaTheme(gvRaporti, "RreshtiId");
          
            krijoTotalSummary();
        }

        private void konfiguroKontrollet()
        {
            if (KomponenteRaportiID != 3038)
                navBarFiltrat.Visible = false;
            else
            {
                navBarFiltrat.Visible = true;
                ConfigureAspxComboBox.percaktoTemplateComboJoList((ASPxComboBox)navBarFiltrat.Groups[0].FindControl("cmbNdermarrje"));
                ConfigureAspxComboBox.mbushComboPeriudhaKaterMujore((ASPxComboBox)navBarFiltrat.Groups[0].FindControl("cmbPeriudha"), true, true);
            }

        }
        protected void gvRaporti_DataBound(object sender, EventArgs e)
        {
            gvRaporti.Settings.ShowFooter = true;
       

        }
        private void ShtoCheckColumn()
        {
            if (gvRaporti.Columns["#"] == null)
            {
                GridViewCommandColumn check = new GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true;
                check.Width = Unit.Percentage(2);
                check.VisibleIndex = 0;
                gvRaporti.Columns.Insert(0, check);
            }
        }
        private void krijoGrupSummary()
        {
            foreach (GridViewColumn col in gvRaporti.VisibleColumns)
            {
               
                if (col.GetType() == typeof(GridViewDataSpinEditColumn))
                {
                    gvRaporti.PercaktoTemplateGroupSummaryFooter( "n2", col.Name);
                    gvRaporti.ShtoGroupSummary("n2", DevExpress.Data.SummaryItemType.Sum, col.Name);

                }
            }
        }

        private void percaktoTemplateMenu()
        {
            clsToolbarConfig.percaktoTemplateMenu(IdGjuha, IdViti, IdPerdoruesi, IdNdermarrje, ASPxMenu1, komponenteDefault, this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, false, true, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session), true);
        }

        public void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
        }

        public void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
        }

        protected void gvRaporti_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters != "filtrim")
                return;
            mbushGridenNgaDB();
        }

        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {
        }

        private void krijoTotalSummary()
        {
            foreach (GridViewColumn col in gvRaporti.VisibleColumns)
            {
                if (col.Name == "Emertimi" || col.Name == "EMERTIMI" || col.Name == "GrupiTitulliKapitulli" || col.Name == "FUNKSIONI" || col.Name == "PERSHKRIMI" || col.Name == "TOTAL_ARTIKULLI")
                {
                    col.FooterTemplate = new MyFooterCellTemplate(col.Name, "Totali ");
                    continue;
                }


                if (KomponenteRaportiID == 3013 && (col.Name == "PAGA_BAZE" || col.Name == "PAGA_POZICION"))
                    continue;

                if (col.GetType() == typeof(GridViewDataSpinEditColumn))
                {

                    gvRaporti.ShtoTotalSummary("n2", DevExpress.Data.SummaryItemType.Sum, col.Name);
                    GridUtil.PercaktoTemplateTotalSummaryFooter(gvRaporti, "n2", col.Name);
                }
            }
        }

        private void vendosBandaPerKolonat()
        {
            int vit = int.Parse(new clsViti(IdViti).KodiViti);
            switch (KomponenteRaportiID)
            {
                case 3024:
                    
                    
                    GridUtil.PercaktoBandPerKolona(gvRaporti, "Kerkesa " + (vit + 1), "ARTIKULLI600", "FONDIVECANTE", "ARTIKULLI601");
                    break;
                case 3012:
                    GridUtil.PercaktoBandPerKolona(gvRaporti, "Fakti i vitit " + (vit - 1), "TE_ARDHURATOTALE_PARAARDHES");
                    GridUtil.PercaktoBandPerKolona(gvRaporti, "I pritshmi " + vit, "ITAKOJNE_INSTITUCIONIT_AKTUALE", "DERDHEN_NE_BUXHET_AKTUALE");
                    GridUtil.PercaktoBandPerKolona(gvRaporti, "Parashikimi viti " + (vit + 1), "ITAKOJNE_INSTITUCIONIT_PASARDHES", "DERDHEN_NE_BUXHET_PASARDHES");
                    break;
                case 3013:
                    GridUtil.PercaktoBandPerKolona(gvRaporti, "Paga baze", "PAGA_BAZE", "VJETERSIA_MESATARE", "VJETERSIA_VITE", "PAGA_POZICION", "FONDI_VJETOR");
                    GridUtil.PercaktoBandPerKolona(gvRaporti, "Shtesa per page ( te dhena ne total)", "NIVELI_SHTESES", "FONDI_NIVELI_SHTESES", "FONDI_PAGA_SHTESA_JASHTORARIT", "SHTESA_PAGE_TE_RREGULLUARA", "FONDI_PAGA_SHTESA_TE_RREGULLUARA", "TOTAL_FONDI_SHTESA_TE_PAGAVE");
                    break;
                case 3014:
                    GridUtil.PercaktoBandPerKolona(gvRaporti, "FAKTI Viti i kaluar sipas situacionit", "NGABUXHETI_PARAARDHES", "NGATEARDHURAT_PARAARDHES", "TOTAL_PARAARDHES");
                    GridUtil.PercaktoBandPerKolona(gvRaporti, "I pritshmi viti aktual", "NJESIA_AKTUALE", "NGABUXHETI_AKTUALE", "NGATEARDHURAT_AKTUALE", "TOTAL_AKTUALE");
                    GridUtil.PercaktoBandPerKolona(gvRaporti, "Kerkesa per vitin e pare te PBA-se", "NJESIA_ARDHME", "KOSTO_PER_NJESI_ARDHME", "SHPENZIME_PLANIFIKUAR_ARDHME", "LIMITI_ARDHME", "KERKESA_GJYKATES_ARDHME", "DIFERENCA_KERKESE_LIMIT_ARDHME", "SHPENZIME_TEPLANIFIKUAR_TEARDHURA_ARDHME", "TOTALKERKESA_TEARDHURA_ARDHME", "VLERSIMIZYRES_ARDHME");
                    break;
                case 3015:
                    GridUtil.PercaktoBandPerKolona(gvRaporti, "Fakti i vitit " + (vit - 1), "BUXHETI_PARAARDHES", "TEARDHURAT_PARAARDHES");
                    GridUtil.PercaktoBandPerKolona(gvRaporti, "I pritshmi vitit " + vit, "BUXHETI_AKTUAL", "TEARDHURAT_AKTUAL");
                    GridUtil.PercaktoBandPerKolona(gvRaporti, "Parashikimi v." + (vit + 1), "BUXHETI_PLUS1", "TEARDHURAT_PLUS1");
                    GridUtil.PercaktoBandPerKolona(gvRaporti, "Parashikimi v." + (vit + 2), "BUXHETI_PLUS2", "TEARDHURAT_PLUS2");
                    GridUtil.PercaktoBandPerKolona(gvRaporti, "Parashikimi v." + (vit + 3), "BUXHETI_PLUS3", "TEARDHURAT_PLUS3");
                    break;
                case 3016:
                    GridUtil.PercaktoBandPerKolona(gvRaporti, "Sasia e planifikuar " + (vit + 1), "SASIOR_PLUS1", "VLEROR_PLUS1");
                    GridUtil.PercaktoBandPerKolona(gvRaporti, "Sasia e planifikuar " + (vit + 2), "SASIOR_PLUS2", "VLEROR_PLUS2");
                    GridUtil.PercaktoBandPerKolona(gvRaporti, "Sasia e planifikuar " + (vit + 3), "SASIOR_PLUS3", "VLEROR_PLUS3");
                    break;
                case 3017:
                    GridUtil.PercaktoBandPerKolona(gvRaporti, "Projekti", "EMERTIMI");
                    GridUtil.PercaktoBandPerKolona(gvRaporti, (vit - 1).ToString(), "TOTAL_PARARDHES");
                    GridUtil.PercaktoBandPerKolona(gvRaporti, (vit).ToString(), "PRITSHMI_AKTUAL");
                    GridUtil.PercaktoBandPerKolona(gvRaporti, "Parashikimi i shpenzimeve per vitin " + (vit + 1), "SHPENZ_KAPITALE_PATRUPEZUAR_ARDHME", "SHPENZ_KAPITALE_TRUPEZUAR_ARDHME", "TRANSFERIM_KAPITAL_ARDHME", "TOTALI_ARDHME");
                    GridUtil.PercaktoBandPerKolona(gvRaporti, (vit + 2).ToString(), "PARASHIKIM_TOTALI_PLUS2");
                    GridUtil.PercaktoBandPerKolona(gvRaporti, (vit + 3).ToString(), "PARASHIKIM_TOTALI_PLUS3");
                    break;
                case 3029:
                    GridUtil.PercaktoBandPerKolona(gvRaporti, "Kerkesa per vitin e dyte te PBA-se", "LIMITI_ARDHME_PLUS1", "KERKESA_GJYKATES_ARDHME_PLUS1", "DIFERENCA_KERKESE_LIMIT_ARDHME_PLUS1", "VLERSIMIZYRES_ARDHME_PLUS1");
                    GridUtil.PercaktoBandPerKolona(gvRaporti, "Kerkesa per vitin e trete te PBA-se", "LIMITI_ARDHME_PLUS2", "KERKESA_GJYKATES_ARDHME_PLUS2", "DIFERENCA_KERKESE_LIMIT_ARDHME_PLUS2", "VLERSIMIZYRES_ARDHME_PLUS2");
                    break;
            }
        }

        private void VendosEmerPerKolonat()
        {
            int vit = int.Parse(new clsViti(IdViti).KodiViti);
            switch (KomponenteRaportiID)
            {
                case 3030:
                    gvRaporti.PercaktoEmerPerKolonen("Nr. i ceshtjeve " + (vit - 1), "NRCESHTJEPARAARDHES");
                    gvRaporti.PercaktoEmerPerKolonen("Nr. i ceshtjeve " + (vit), "NRCESHTJEVITIAKTUAL");
                    gvRaporti.PercaktoEmerPerKolonen("Nr. i ceshtjeve " + (vit + 1), "NRCESHTJEVITIPASARDHES");
                    gvRaporti.PercaktoEmerPerKolonen("Shpenzime faktike 602, viti " + (vit - 1), "TOTAL_PARAARDHES");
                    gvRaporti.PercaktoEmerPerKolonen("I pritshmi " + (vit), "NGABUXHETI_AKTUALE");
                    gvRaporti.PercaktoEmerPerKolonen("Limiti " + (vit), "LIMITI_ARDHME");
                    gvRaporti.PercaktoEmerPerKolonen("Kerkesa e gjykatave " + (vit + 1), "KERKESA_GJYKATES_ARDHME");
                    gvRaporti.PercaktoEmerPerKolonen("Diferenca kerkese limit " + (vit + 1), "DIFERENCA_KERKESE_LIMIT_ARDHME");
                    gvRaporti.PercaktoEmerPerKolonen("Kerkesa e gjykatave " + (vit + 2), "KERKESA_GJYKATES_ARDHME_PLUS1");
                    gvRaporti.PercaktoEmerPerKolonen("Kerkesa e gjykatave " + (vit + 3), "KERKESA_GJYKATES_ARDHME_PLUS2");
                    break;
                case 3012:
                    gvRaporti.PercaktoEmerPerKolonen("Parashikimi per vitin " + (vit + 2), "PARASHIKIMI_PLUS2");
                    gvRaporti.PercaktoEmerPerKolonen("Parashikimi per vitin " + (vit + 3), "PARASHIKIMI_PLUS3");
                    break;
            }
        }
        protected void gvRaporti_HtmlFooterCellPrepared(object sender, ASPxGridViewTableFooterCellEventArgs e)
        {
        }

        protected void gvRaporti_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
        }
    }
}