using DbCore.DbAdmin;
using DbCore;
using DevExpress.Utils;
using DevExpress.Web;
using DevExpress.Web.ASPxPivotGrid;
using DevExpress.XtraCharts;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Resources;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlphaWebCommon.WebUtils.ASPxControlExtensions;
using Newtonsoft.Json;
using PlatinumWeb.ApplicationUtils.Pages;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using DbCore.IMBUtils.Messages;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;

namespace PlatinumWeb
{
    public partial class Raport_PivotGrid : MyPageBase
    {
        private string prefixMesazhNjejes = " ";
        private string prefixMesazhShumes = " ";
        private string suffixMesazhNjejesGabimi = " ";
        private string suffixMesazhShumesGabimi = " ";
        private string suffixMesazhNjejesSuksesi = " ";
        private string suffixMesazhShumesSuksesi = " ";
        private string lidhesMesazhi = " ";
        private bool kaTeDhenaJoNumerike = false;
        int idPerdoruesi, idGjuha, idNdermarrje, idViti, idModuli;
        string emriKomponentes;
        int idKomponente;
        private DbCore.DbRegjistrim.clsKonfigPivotGridaKoka kokaPG;
        




        /// <summary>
        /// Veprimet qe kryhen ne mementin e ngarkimit te faqes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            

            if (!Page.IsPostBack)
            {
                idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
                idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
                idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                idModuli = Convert.ToInt32(Request.QueryString["idModuli"]);

                emriKomponentes = DbCore.clsFunksione.GetKomponente(Page.Request);

                clsKomponente komp = new clsKomponente(emriKomponentes);
                idKomponente = komp.IdKomponente;
                mySessionObjects.RuajNeSession<string>(Session, "", "DataFilterPivotGrid");

                if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
                {
                    Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerdoruesi);
                }
                EmrateTabeve();
                hfState.Set("idGjuha", idGjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("idViti", idViti);
                hfState.Set("idPerdoruesi", idPerdoruesi);
                hfState.Set("idKomponente", idKomponente);
                hfState.Set("emriKomponente", emriKomponentes);
                hfState.Set("idModuli", idModuli);
                
                clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idNdermarrje, "ASPxGridView_KonfigPivotGrid", 1, "Raport_PivotGrid.aspx");
                EmrateLabelave();
                LoadString(HfMsgKonfig);

                konfiguroVleraFillestare(idGjuha, idModuli);
                int idRaporti = DbCore.DbShare.clsRaporti.KtheIdRaportPivotGridSipasIdModuli(idModuli);
                hfState.Add("idRaporti", idRaporti);
                clsTeDrejtaRaporte teDrejtaRap = new clsTeDrejtaRaporte();
                teDrejtaRap.merrTeDrejtaPerRaportPerPerdorues(idRaporti, idPerdoruesi, idNdermarrje, idViti);
                hfTeDrejtaRaporti.Set("teDrejtaRap", JsonConvert.SerializeObject(teDrejtaRap));
                mbushGridKonfigurimeshNgaDB(idNdermarrje, idModuli, idPerdoruesi);
                DbCore.mySessionObjects.resetTeDhenaPivotGrideNgaSession(Session);

                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, emriKomponentes);
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);

                CallbackState.Set("firstCallback", "0");//kjo tregon se eshte callback-u i pare,keshtu qe vlerat duhet te merren nga DB,ne rast te kundert nuk do ndryshohen vlerat
                konfiguroGride(idGjuha, idNdermarrje);
                GridUtil.percaktoVisibleColumnsMeWidth(idGjuha, idNdermarrje, ASPxGridView_KonfigPivotGrid, "ASPxGridView_KonfigPivotGrid", "Raport_PivotGrid.aspx");
            }
            else
            {
                idPerdoruesi = (int)hfState["idPerdoruesi"];
                idGjuha = (int)hfState["idGjuha"];
                idNdermarrje = (int)hfState["idNdermarrje"];
                idViti = (int)hfState["idViti"];
                idModuli = (int)hfState["idModuli"];
                idKomponente = (int)hfState.Get("idKomponente");
                emriKomponentes = (string)hfState.Get("emriKomponente");
                mbushGridKonfigurimeshNgaSession(idNdermarrje, idModuli);

                if (ASPxPageControl1.ActiveTabIndex == 2)
                {
                   
                    shtoTeDhenatPivotGrid(idNdermarrje, idPerdoruesi, idGjuha, idModuli);
                }
                //  FilterPopup.BindGridView();
                konfiguroGride(idGjuha, idNdermarrje);
            }
            percaktoTemplateMenu(idGjuha, idViti, idPerdoruesi, idNdermarrje, ASPxMenu1);
            if (!IsPostBack && !IsCallback)
            {
                StateController.ClearState();
                SaveCurrentStateAndUpdateControlsState();
            }
            //  GridUtil.PercaktoTitlePanel(ASPxGridView_KonfigPivotGrid, Page, ASPxMenu1, pnlMesazhi,null, idPerdoruesi, idNdermarrje, idViti, idGjuha, emriKomponentes, idKomponente, "EmriPivotGridKoka", rm, ci);
            GridUtil.konfigGrideListeEMadhePaTheme(ASPxGridView_KonfigPivotGrid, "IdKonfPivotGridaKoka");
        }

        

        /// <summary>
        /// Vendos emrat e tabeve ne baze te gjuhes se perdoruesit
        /// </summary>
        private void EmrateTabeve()
        {
            ASPxPageControl1.TabPages[0].Text = MessagesResource.Messages["labelAdministrimiTePergjithshme"];
            ASPxPageControl1.TabPages[1].Text = MessagesResource.Messages["labelRaportKonfigurimi"];
            ASPxPageControl1.TabPages[2].Text = MessagesResource.Messages["labelFilterKryesorRaporti"];
        }

        /// <summary>
        /// percakton nese do shfaqen ose jo totalet e kolonave,rreshtave dhe grand totalet
        /// kjo metode thirret ne cdo callback te pivotgrides
        /// </summary>
        private void SetTotalsVisibility()
        {
            int remainder;
            int indeksiFundit = Math.DivRem(ASPxPivotGridRaporti.RowCount, ASPxPivotGridRaporti.OptionsPager.RowsPerPage, out remainder);
            if (remainder > 0)
                indeksiFundit = indeksiFundit + 1;
            //if (ASPxPivotGridRaporti.OptionsPager.PageIndex == indeksiFundit - 1)
            //    ASPxPivotGridRaporti.OptionsView.ShowRowGrandTotals = true;
            //else
            //    ASPxPivotGridRaporti.OptionsView.ShowRowGrandTotals = false;

            ASPxPivotGridRaporti.OptionsView.ShowColumnGrandTotals = ColumnGrandTotal.Checked;
            ASPxPivotGridRaporti.OptionsView.ShowColumnTotals = ColumnTotal.Checked;
            ASPxPivotGridRaporti.OptionsView.ShowRowGrandTotals = (RowGrandTotal.Checked && (ASPxPivotGridRaporti.OptionsPager.PageIndex == indeksiFundit - 1));
            ASPxPivotGridRaporti.OptionsView.ShowRowTotals = RowTotal.Checked;
        }

        /// <summary>
        /// Merr te dhenat e pivot grides nga db
        /// </summary>
        /// <returns></returns>
        /// <param name="kolonatString"></param>
        /// <param name="shikoGjitheDok"></param>
        /// <param name="idModuli"></param>
        private DataView mbushTeDhenaPivotGridNgaDB(string kolonatString, int idNdermarrje, int idPerdorues, int gjuha, int shikoGjitheDok, string kodModuli, string kodKubi, string data)
        {
            using (DbCore.DbRegjistrim.clsDatabaseRegjistrim dbRegj = new DbCore.DbRegjistrim.clsDatabaseRegjistrim())
            {
                DataView teDhenatPivotGrid = dbRegj.ktheTeDhenaPivotGrid(kolonatString, idNdermarrje, kodModuli, shikoGjitheDok, idPerdorues, gjuha, kodKubi, DbCore.mySessionObjects.ktheNdermarrjeVit(Session), data);
                DbCore.mySessionObjects.ruajTeDhenaPivotGrideNeSession(kolonatString, Session, teDhenatPivotGrid);
                return teDhenatPivotGrid;
            }
        }

        /// <summary>
        /// Merr te dhenat e pivot grides nga sessioni
        /// </summary>
        /// <returns></returns>
        /// <param name="kolonatString"></param>
        /// <param name="idModuli"></param>
        private DataView mbushTeDhenaPivotGridNgaSession(string kolonatString, int idNdermarrje, int idPerdorues, int gjuha, int idModuli)
        {
            DataView tmpObject = null;
            try
            {
                string data = "";
                if (hfState.Get("DateDokumentiVisibility").ToString() == "visible")
                    data = ktheFilterDatePerSp();

                string kodModuli = clsModuli.merrKodModuli(idGjuha, idModuli);
                var dataMeparshme = mySessionObjects.MerrNgaSession<string>(Session,  "DataFilterPivotGrid");
                DbCore.mySessionObjects.merrTeDhenaPivotGrideNgaSession(kolonatString, Session, out tmpObject);
                if(data != dataMeparshme)
                    mySessionObjects.RuajNeSession<string>(Session, data, "DataFilterPivotGrid");
                if ((tmpObject == null && !String.IsNullOrEmpty(kolonatString)) || (kodModuli == "M_BURIME_NJEREZORE" && data != dataMeparshme))
                {
                    var kodKubi = String.Empty;
                    var konfigurimiKubit = ASPxGridView_KonfigPivotGrid.GetRowValues(ASPxGridView_KonfigPivotGrid.FocusedRowIndex, "EmriPivotGridKoka");

                    if (konfigurimiKubit != null && idModuli == 17)
                        kodKubi = konfigurimiKubit.ToString();
                    var teDrejtaRap = JsonConvert.DeserializeObject<clsTeDrejtaRaporte>(hfTeDrejtaRaporti.Get<string>("teDrejtaRap"));
                    tmpObject = mbushTeDhenaPivotGridNgaDB(kolonatString, idNdermarrje, idPerdorues, gjuha, Convert.ToInt32(teDrejtaRap.DGjitheDok), kodModuli, kodKubi, data);
                }

                if (tmpObject != null)
                {
                    try
                    {

                        tmpObject.RowFilter = ktheFilterDataView();
                    }
                    catch (Exception ex)
                    {
                        throw new MyException("Gabim ne aplikimin e filtrit: {1}  ex:{1} ", ktheFilterDataView(), ex);
                    }
                }
                //mos e nderpresi punen per shkak se nuk i punon filtri
                return tmpObject;
            }
            catch (MyException ex)
            {
                return tmpObject;
            }
            catch (Exception ex)
            {
                //nuk ka kuptim te vazhdoj
                throw new MyException("Gabim ne mbushjen e datasource-it te pivotgrides.kolonat {0}  ex:{1} ", kolonatString, ex);
            }
        }

        /// <summary>
        /// Konfiguron griden emrit te grides dhe emrit te komponentes
        /// </summary>
        /// <param name="idGjuha"></param>
        /// <param name="kodKonfigurimi">Kodi konfigurmit</param>
        /// <param name="idKomponente">Id e komponentes</param>
        private void konfiguroGride(int idGjuha, int idNdermarrje)
        {

            ASPxGridView_KonfigPivotGrid.SettingsEditing.NewItemRowPosition = DevExpress.Web.GridViewNewItemRowPosition.Top;

            ASPxGridView_KonfigPivotGrid.Columns["#"].VisibleIndex = 0;
            ASPxGridView_KonfigPivotGrid.Columns["#"].Width = 25;
        }

        /// <summary>
        /// Konfiguron vlerat fillestare te ambjentit
        ///
        /// </summary>
        /// <param name="idGjuha"></param>
        /// <param name="idModuli"></param>
        private void konfiguroVleraFillestare(int idGjuha, int idModuli)
        {
            //inicializoObjekte();
            ASPxPageControl1.ActiveTabIndex = 0;
            int windowWidth = Convert.ToInt32(Request.QueryString["windowWidth"]);
            WebChart.Width = Convert.ToInt32(windowWidth / 1.04);
            clsPeriudhaKontabel periudha = DbCore.mySessionObjects.merrPeriudheKontabel(Session);
            if (periudha != null)
            {
                ((ASPxDateEdit)navBarFiltrat.Groups[0].FindControl("txtNgaDok")).Date = periudha.FillimiPeriudha;
                ((ASPxDateEdit)navBarFiltrat.Groups[0].FindControl("txtDeriDok")).Date = periudha.MbarimiPeriudha;
                AspxWebControlUtils.vendosDateEditMask(((ASPxDateEdit)navBarFiltrat.Groups[0].FindControl("txtNgaDok")), ((ASPxDateEdit)navBarFiltrat.Groups[0].FindControl("txtDeriDok")));
            }
            ConfigureAspxComboBox.percaktoTemplateComboMeEnableCallback(cmbAutorizimi);

            //Mbushet comboja e tipit te grafikut
            ASPxComboBox cmbGrafiku = (ASPxComboBox)navBarFiltrat.Groups[0].FindControl("cmbGrafiku");
            ((ASPxRadioButtonList)navBarFiltrat.Groups[0].FindControl("radDtDok")).SelectedIndex = 0;
            //var arr = Enum.GetValues(typeof(ViewType));
            //merr te gjitha llojet e grafikut
            cmbGrafiku.Items.AddRange(Enum.GetValues(typeof(ViewType)));

            cmbSaVleraNeGrafik.Items.AddRange(new String[] { "10", "50", "100", "200" });
            cmbSaVleraNeGrafik.Items.Add(idGjuha == 1 ? "Te gjitha" : "All");
            //switch (idGjuha)
            //{
            //    case 0: //shqip
            //        cmbSaVleraNeGrafik.Items.AddRange(new String[] { "10", "50", "100", "200", "Te gjitha" });
            //        break;
            //    case 1: //anglisht
            //        cmbSaVleraNeGrafik.Items.AddRange(new String[] { "10", "50", "100", "200", "All" });
            //        break;
            //    default:
            //        break;
            //}

            cmbSaVleraNeGrafik.SelectedIndex = 4;
            cmbGrafiku.SelectedItem = cmbGrafiku.Items.FindByText(ViewType.Line.ToString());
            WebChart.SeriesTemplate.ChangeView((ViewType)Enum.Parse(typeof(ViewType), cmbGrafiku.SelectedItem.Text));
            DbCore.DbRegjistrim.colGrupimeKolone_PivotGrid grupimet = DbCore.DbRegjistrim.colGrupimeKolone_PivotGrid.ktheGrupimetKolonaveSipasIdModulit(idGjuha, idModuli);
            JavaScriptSerializer serializusi = new JavaScriptSerializer();
            HfGrupimKolone.Value = serializusi.Serialize(grupimet);
        }

        /// <summary>
        /// Krijon nje instance te klases clsDatabaseRegjistrim
        /// </summary>
        private void inicializoObjekte()
        {
            //dbRegj = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
        }

        /// <summary>
        /// Ne rastin kur shtohet nje konfigurim i ri pivot gride, behet shtimi edhe ne datasource-n e grides ASPxGridView_KonfigPivotGrid
        /// </summary>
        /// <param name="idKonfigPG"></param>
        /// <param name="idModuli"></param>
        private void shtoKonfigPGNeGrid(int idKonfigPG, int idModuli)
        {
            if (ASPxGridView_KonfigPivotGrid.DataSource != null)
            {
                DataTable dt = (DataTable)ASPxGridView_KonfigPivotGrid.DataSource;
                DataRow[] drs = dt.Select("IdKonfPivotGridaKoka = " + idKonfigPG);
                if (drs.Length > 0)
                    throw new DbCore.MyException(MessagesResource.Messages["labelRaportMesazhKonfiguimiEkziston"]);
                //"GABIM: Konfigurimi ekziston ne gride");
                DataRow newArtDr = DbCore.DbRegjistrim.clsKonfigPivotGridaKoka.ktheKonfigPGSipasID(idKonfigPG);
                dt.ImportRow(newArtDr);
                dt.Rows[dt.Rows.Count - 1].ItemArray = newArtDr.ItemArray;
            }
            else
                mbushGridKonfigurimeshNgaDB(idNdermarrje, idModuli, idPerdoruesi);
        }

        /// <summary>
        /// Ndryshon datasource-n e grides ASPxGridView_KonfigPivotGrid kur modifikohet nje konfigurim ekzistues
        /// </summary>
        /// <param name="idKonfigPG">Id e infos se artikullit qe u modifikua</param>
        /// <param name="idModuli"></param>
        private void modifikoKonfigPGNeGrid(int idKonfigPG, int idModuli)
        {
            if (ASPxGridView_KonfigPivotGrid.DataSource != null)
            {
                DataTable dt = (DataTable)ASPxGridView_KonfigPivotGrid.DataSource;
                DataRow[] drs = dt.Select("IdKonfPivotGridaKoka = " + idKonfigPG);
                if (drs.Length > 1)
                    throw new DbCore.MyException(MessagesResource.Messages["labelRaportMesazhGabimiDyKonfigurimeNeGride"]);
                //"GABIM: Ndodhen 2 konfigurime me te njejten id ne gride");
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                DataRow newArtDr = DbCore.DbRegjistrim.clsKonfigPivotGridaKoka.ktheKonfigPGSipasID(idKonfigPG);
                object[] arr = newArtDr.ItemArray;
                dr.ItemArray = arr;
            }
            else mbushGridKonfigurimeshNgaDB(idNdermarrje, idModuli, idPerdoruesi);
        }

        /// <summary>
        /// Ndryshon datasource-n e grides ASPxGridView_KonfigPivotGrid duke hequr konfigurimin  qe eshte fshire.
        /// </summary>
        /// <param name="idKonfigPG"></param>
        /// <param name="idModuli"></param>
        private void hiqKonfigNgaGrida(int idKonfigPG, int idModuli)
        {
            if (this.ASPxGridView_KonfigPivotGrid.DataSource != null)
            {
                DataTable dt = (DataTable)ASPxGridView_KonfigPivotGrid.DataSource;
                DataRow[] drs = dt.Select("IdKonfPivotGridaKoka = " + idKonfigPG);
                if (drs.Length > 1)
                    throw new DbCore.MyException(MessagesResource.Messages["labelRaportMesazhGabimiDyKonfigurimeNeGride"]);
                if (drs.Length == 0)
                    return;
                DataRow dr = drs[0];
                dt.Rows.Remove(dr);
                ASPxGridView_KonfigPivotGrid.DataBind();
            }
            else
                mbushGridKonfigurimeshNgaDB(idNdermarrje, idModuli, idPerdoruesi);
        }

        /// <summary>
        /// Merr te dhenat e konfigurimeve nga DB per griden ASPxGridView_KonfigPivotGrid
        /// </summary>
        /// <param name="idModuli"></param>
        private void mbushGridKonfigurimeshNgaDB(int idNdermarrje, int idModuli, int idPerdoruesi)
        {
            DataTable dt = DbCore.DbRegjistrim.clsKonfigPivotGridaKoka.ktheKonfigPGPerGride(idNdermarrje, idPerdoruesi, idModuli);
            DbCore.mySessionObjects.ruajGrideNeSession(Session, dt);
            ASPxGridView_KonfigPivotGrid.DataSource = dt;
            ASPxGridView_KonfigPivotGrid.DataBind();
            dt.Dispose();
        }

        /// <summary>
        /// Mbush griden e konfigurimeve me te dhenat e ruajtura ne session
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="idModuli"></param>
        private void mbushGridKonfigurimeshNgaSession(int idNdermarrje, int idModuli)
        {
            DataTable tmpObject;
            bool sukses = DbCore.mySessionObjects.merrGrideNgaSessioni(Session, out tmpObject);
            if (!sukses)
                mbushGridKonfigurimeshNgaDB(idNdermarrje, idModuli, idPerdoruesi);
            else
            {
                ASPxGridView_KonfigPivotGrid.DataSource = tmpObject;
                ASPxGridView_KonfigPivotGrid.DataBind();
                tmpObject.Dispose();
            }
        }

        /// <summary>
        /// Pastron fushat pasi eshte bere nje ruajtje e suksesshme
        /// </summary>
        private void pastroFusha()
        {
        }

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="idGjuha"></param>
        /// <param name="idNderVit"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        private void percaktoTemplateMenu(int idGjuha, int idViti, int idPerdorues, int idNdermarrje, ASPxMenu aSPxMenu1)
        {//"Raport_PivotGrid.aspx?idModuli=" + idModuli
            clsToolbarConfig.percaktoTemplateMenu(idGjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1, emriKomponentes, this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, null, null, hfShtimModifikim.Value == "modifikim" ? false : true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session), true, Request.QueryString["vjenNga"] != null && Request.QueryString["vjenNga"] == "CRM" ? true : false);
            EventHandler handlerExportoClick = new EventHandler(exportoRaport);
            EventHandler handlerPerOnPreRender = new EventHandler(PreRender_ExportButton);
            clsToolbarConfig.ShtoMenuItemExporto(this, aSPxMenu1, handlerExportoClick, handlerPerOnPreRender);
        }

        ///// <summary>
        ///// Mbush combon e filtrave te grides ASPxGridView_KonfigPivotGrid
        ///// </summary>
        //private void mbushComboBoxFiltra()
        //{
        //    //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
        //    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka("ASPxGridView_KonfigPivotGrid", "Raport_PivotGrid.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //    DbCore.DbAdmin.colFiltratGrida colFiltra = new DbCore.DbAdmin.colFiltratGrida(koka.IdGridaKoka, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //    colFiltra.Insert(0, new DbCore.DbAdmin.clsFiltraGrida());
        //    MenuFilter.colekstioni = colFiltra;
        //    MenuFilter.ValueField = "IdFiltra";
        //    MenuFilter.TextField = "FiltraShenime";
        //}

        /// <summary>
        /// Per eksporimin e raporteve te gjeneruara
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void PreRender_ExportButton(object sender, EventArgs e)
        {
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemExport");
            ASPxButton exportButton = ((PlatinumWeb.MenuExport)(itemButton.Template)).FindControl("exportButton") as ASPxButton;
            ScriptManager ScriptMgr = (ScriptManager)this.FindControl("ScriptManager1");
            ScriptMgr.RegisterPostBackControl(exportButton);
        }

        /// <summary>
        /// Sherben per te ruajtur filtrin e zgjedhur te grides
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idPerdorues = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            int idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            DbCore.DbAdmin.clsFiltraGrida filtri = new DbCore.DbAdmin.clsFiltraGrida();
            filtri.FiltraKodi = cmbFiltra.Text;
            filtri.FiltraShenime = cmbFiltra.Text;
            filtri.FiltraUniversal = false;
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuha, "ASPxGridView_KonfigPivotGrid", "Raport_PivotGrid.aspx", idNdermarrje);
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = ASPxGridView_KonfigPivotGrid.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("Kodi", ASPxGridView_KonfigPivotGrid);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = ASPxGridView_KonfigPivotGrid.GetSortedColumns();
            //if (kolona.Count > 0)
            //{
            //    filtri.KoloneRenditje = kolona[0].FieldName;
            //    if (kolona[0].SortOrder == DevExpress.Data.ColumnSortOrder.Ascending)
            //        filtri.DrejtimRenditje = true;
            //    else
            //        filtri.DrejtimRenditje = false;
            //}
            //else
            //{
            //    filtri.KoloneRenditje = "Kodi";
            //    filtri.DrejtimRenditje = true;
            //}
            filtri.IdPerdoruesi = idPerdorues;
            filtri.IdNdermarje = idNdermarrje;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            filtri.IdStatusDok = 1;

            mesazh = filtri.ruaj();
            percaktoTemplateMenu(idGjuha, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), idPerdorues, idNdermarrje, ASPxMenu1);

            //mbushComboBoxFiltra();
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "ASPxGridView_KonfigPivotGrid", 1, "Raport_PivotGrid.aspx");
            if (mesazh.Status == true)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            hfStatusi.Value = "true";
            cmbFiltra.Text = "";
        }

        /// <summary>
        /// Fshin filtrin e zgjedhur dhe aplikuar mbi gride, fshirje nga DB ku ai eshte ruajtur, dhe jo pastrimi i grides nga aplikimi i filtrit
        /// per kete do sherbeje Clear Filter ne fund te grides
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
            //Kap item qe ka template ne menune e kesaj faqeje
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idPerdorues = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            int idGjuha = (int)hfState.Get("idGjuha");
            //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
            //filtra.mbushFiltraGridaSipasFiltraEmri(cmbFiltra.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuha, "ASPxGridView_KonfigPivotGrid", "Raport_PivotGrid.aspx", idNdermarrje);
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = idPerdorues;
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();
                //mbushComboBoxFiltra();
                clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idNdermarrje, "ASPxGridView_KonfigPivotGrid", 1, "Raport_PivotGrid.aspx");
                percaktoTemplateMenu(idGjuha, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), idPerdorues, idNdermarrje, ASPxMenu1);

                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                hfStatusi.Value = "true";
                ASPxGridView_KonfigPivotGrid.FilterExpression = String.Empty;
            }
        }

        /// <summary>
        /// Behet bound i menuse duke thirrur metoden percaktoTemplateMenu
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), ASPxMenu1);
        }

        /// <summary>
        /// Metoda per te thirrur veprimet e menuse kur shtypen butonat
        /// </summary>
        /// <param name="source">derguesi</param>
        /// <param name="e">parametrat</param>
        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {
            if (e.Item.Name == "Ruaj")
            {
                Page.Validate("entries");
                ruajKonfigurimPG((int)hfState["idModuli"]);
            }
        }

        /// <summary>
        /// Ruan nje mkonfigurim te ri te raporteve, ne rastin kur shtohet nje konfigurim i ri ose modifikohet nje ekzistues
        /// </summary>
        /// <param name="ci"></param>
        /// <param name="idModuli"></param>
        private void ruajKonfigurimPG(int idModuli)
        {
            if (!Page.IsValid)
                return;
            if (!isValidKonfigurimPG(idModuli))
                return;
            
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();

            var shtimMod = hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim";

            var konfigPG = krijoKonfigPivotGride(idModuli, shtimMod);
            bool eshteShtim;
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

            int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.clsFunksione.GetKomponente(Page.Request));

            if (shtimMod)
            {
                if (!tedrejtaInfo.DShtim)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["mesazhRaportNukKeniTeDrejta"], pnlMesazhi);
                    hfStatusi.Value = "false";
                    return;
                }
                mesazh = konfigPG.ruajKonfigurimPG();
                eshteShtim = true;
            }
            else
            {
                if (!tedrejtaInfo.DMod)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["mesazhRaportNukKeniTeDrejta"], pnlMesazhi);
                    hfStatusi.Value = "false";
                    return;
                }

                var idKonfigPivotGrida = ASPxGridView_KonfigPivotGrid.GetRowValues(ASPxGridView_KonfigPivotGrid.FocusedRowIndex, "IdKonfPivotGridaKoka");

                konfigPG.IdKonfPivotGridaKoka = Convert.ToInt32(idKonfigPivotGrida);
                mesazh = konfigPG.modifikoKonfigurimPG(DbCore.mySessionObjects.ktheGjuhe(Session));
                eshteShtim = false;
            }
            if (mesazh)
            {
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["labelRaportMesazhRuajtjaPerfundoiSukses"], pnlMesazhi);
                if (eshteShtim)
                    shtoKonfigPGNeGrid(konfigPG.IdKonfPivotGridaKoka, idModuli);
                else //modifikim
                    modifikoKonfigPGNeGrid(konfigPG.IdKonfPivotGridaKoka, idModuli);
                hfStatusi.Value = "true";
                pastroFusha();
                ASPxPageControl1.ActiveTabIndex = 0;
            }
            else
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["labelRaportMesazhRuajtjaPerfundoiGabime"], pnlMesazhi);
                hfStatusi.Value = "false";
            }
        }

        /// <summary>
        /// Krijon objektin konfigurim raporti duke marre te dhenat qe ka vendosur perdoruesi
        /// gjate shtimit te nje objekti te ri ose modifikimit te nje objekti ekzistues
        /// </summary>
        /// <returns>Objektin e krijuar te klases clsInfoArtikulliKoka</returns>
        /// <param name="idModuli"></param>
        private DbCore.DbRegjistrim.clsKonfigPivotGridaKoka krijoKonfigPivotGride(int idModuli, bool Shtim)
        {
            DbCore.DbRegjistrim.clsKonfigPivotGridaKoka koka = new DbCore.DbRegjistrim.clsKonfigPivotGridaKoka();
            koka.EmerPivotGridaKoka = kodi_TextBox.Text;
            koka.PershkrimiPivotGridaKoka = pershkrimi_TextBox.Text;
            koka.IdModuli = idModuli;
            koka.IdNdermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            koka.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            koka.IdStatusDok = 1;
            if (!Shtim)
            {
                koka.FieldStateLayout = ASPxPivotGridRaporti.SaveCollapsedStateToString();
            }
            koka.GrandTotalKolona = ColumnGrandTotal.Checked;
            koka.TotalKolona = ColumnTotal.Checked;
            koka.GrandTotalRreshta = RowGrandTotal.Checked;
            koka.TotalRreshta = RowTotal.Checked;
            koka.HiqVleraZero = HiqVleraZero.Checked;
            koka.FilterExpression = ASPxPivotGridRaporti.Prefilter.CriteriaString;

            DbCore.DbAdmin.colLidhjetAutorizim colLidhje;
            if (cmbAutorizimi.Text == "")
                colLidhje = new DbCore.DbAdmin.colLidhjetAutorizim();
            else
            {
                DbCore.DbAdmin.colLidhjetAutorizim colLidhjet = new DbCore.DbAdmin.colLidhjetAutorizim();
                string[] pars11 = cmbAutorizimi.Text.Split(',');
                for (int i = 0; i < pars11.Length; i++)
                {
                    DbCore.DbAdmin.clsLidhjeAutorizim lidhje = new DbCore.DbAdmin.clsLidhjeAutorizim();
                    lidhje.IdAutorizimeKoka = DbCore.DbAdmin.clsAutorizimKoka.ktheIDAutorizim(pars11[i]);
                    colLidhjet.Add(lidhje);
                }
                colLidhje = colLidhjet;
            }

            koka.OLidhjetAutorizim = colLidhje;

            DbCore.DbRegjistrim.colKonfigPivotGridaTrupi trupat = DbCore.DbRegjistrim.colKonfigPivotGridaTrupi.ktheKonfigShto(DbCore.mySessionObjects.ktheGjuhe(Session), idModuli);

            JavaScriptSerializer serializusi = new JavaScriptSerializer();
            object[] fushatPivot = (object[])serializusi.DeserializeObject(fushatRaporti.Value);
            if (fushatPivot != null)
            {
                String[] kolonat = new String[fushatPivot.Length];
                for (int i = 0; i < fushatPivot.Length; i++)
                {
                    Dictionary<string, object> fushaKonfig = (Dictionary<string, object>)fushatPivot[i];

                    int grupimi = 0;
                    if (HfGrupimi.Contains(Convert.ToString(fushaKonfig["emerKoloneDB"])))
                        grupimi = Convert.ToInt32(HfGrupimi.Get(fushaKonfig["emerKoloneDB"].ToString()));
                    trupat.modifikoTrupKonfigSipasIDKolona(Convert.ToInt32(fushaKonfig["idKolonaPivotGrid"].ToString()), true, fushaKonfig["zona"].ToString(), Convert.ToInt32(fushaKonfig["rendi"].ToString()), fushaKonfig["width"].ToString(), grupimi);
                }
            }
            koka.KonfigPivotGridaTrupi = trupat;
            return koka;
        }

        /// <summary>
        /// Kontrollon nese ekziston ne db nje konfigurim raporti me kete kod
        /// </summary>
        /// <returns></returns>
        /// <param name="idModuli"></param>
        private bool isValidKonfigurimPG(int idModuli)
        {
            DbCore.DbRegjistrim.clsDatabaseRegjistrim dbRegj = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
            bool isValid;
            isValid = true;
            if (dbRegj.ekzistonKonfigPGKokaSipasKodNdermarje(kodi_TextBox.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), idModuli) && (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim"))
            {
                isValid = false;
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["labelRaportMesazhEkzostonKonfRap"], pnlMesazhi);
                //"Ekziston nje konfigurim raporti me kete kod!Ju lutemi shenoni nje kod tjeter.",
                return isValid;
            }
            dbRegj.Dispose();
            return isValid;
        }

        /// <summary>
        /// Perdoret per te fshire reshtat e zgjedhur ne gride pasi perdoruesi ka konfirmuar fshirjen
        /// </summary>
        /// <param name="sender">dergues</param>
        /// <param name="e">argumentat</param>
        protected void fshiKonfigurimRaporti(object sender, EventArgs e)
        {
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            List<object> rreshtat = this.ASPxGridView_KonfigPivotGrid.GetSelectedFieldValues("IdKonfPivotGridaKoka");
            //clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "", pnlMesazhi);
            List<string> TeFshire = new List<string>(), TePaFshire = new List<string>();
            int idModuli = (int)hfState["idModuli"];
            if (rreshtat.Count == 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, String.Format("{0}", MessagesResource.Messages["lblRaportMesazhNukKeniZgjedhur"]), pnlMesazhi);
                return;
            }
            foreach (object id in rreshtat)
            {
                DbCore.DbRegjistrim.clsKonfigPivotGridaKoka konfigKoka = new DbCore.DbRegjistrim.clsKonfigPivotGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), Convert.ToInt32(id));

                mesazh = konfigKoka.fshiKonfigurimPG();
                if (mesazh.Status)
                {
                    hiqKonfigNgaGrida(konfigKoka.IdKonfPivotGridaKoka, idModuli);
                    TeFshire.Add(konfigKoka.EmerPivotGridaKoka);

                    ASPxPageControl1.ActiveTabIndex = 0;
                    hfStatusi.Value = "true";
                }
                else
                {
                    TePaFshire.Add(konfigKoka.EmerPivotGridaKoka);
                }
            }
            String mesazhInfoSukses = "", mesazhInfoGabim = "";
            prefixMesazhNjejes = MessagesResource.Messages["labelRaportKonfigurimiMeKod"] + ":";
            prefixMesazhShumes = MessagesResource.Messages["labelRaportKonfigurimetMeKode"] + ":";
            suffixMesazhNjejesGabimi = MessagesResource.Messages["labelRaportMesazhNukUFshi"] + ":";
            suffixMesazhShumesGabimi = MessagesResource.Messages["labelRaportMesazhNukUFshine"] + ":";
            suffixMesazhNjejesSuksesi = MessagesResource.Messages["labelRaportMesazhUFshi"];
            suffixMesazhShumesSuksesi = MessagesResource.Messages["labelRaportMesazhUFshine"];
            lidhesMesazhi = ". " + MessagesResource.Messages["labelRaportMesazhKurse"];
            if (TePaFshire.Count == 1)
                mesazhInfoGabim = String.Format("{0}{1}{2}", prefixMesazhNjejes, String.Join(";", TePaFshire), suffixMesazhNjejesGabimi);
            else
                if (TePaFshire.Count > 1)
                mesazhInfoGabim = String.Format("{0}{1}{2}", prefixMesazhShumes, String.Join(";", TePaFshire), suffixMesazhShumesGabimi);
            if (TeFshire.Count == 1)
                mesazhInfoSukses = String.Format("{0}{1}{2}", prefixMesazhNjejes, String.Join(";", TeFshire), suffixMesazhNjejesSuksesi);
            else
                if (TeFshire.Count > 1)
                mesazhInfoSukses = String.Format("{0}{1}{2}", prefixMesazhShumes, String.Join(";", TeFshire), suffixMesazhShumesSuksesi);
            if (mesazhInfoGabim != "" && mesazhInfoSukses != "")
                mesazhInfoGabim += lidhesMesazhi + mesazhInfoSukses;
            if (mesazhInfoGabim != "")
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhInfoGabim, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazhInfoSukses, pnlMesazhi);
            pnlMesazhi.Update();
        }

        /// <summary>
        /// ruan disa karakteristika te grides
        /// </summary>
        /// <param name="sender">dergues</param>
        /// <param name="e">argumentat</param>
        protected void ASPxGridView_KonfigPivotGrid_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = ASPxGridView_KonfigPivotGrid.PageIndex;
            e.Properties["cpPageRow"] = ASPxGridView_KonfigPivotGrid.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = ASPxGridView_KonfigPivotGrid.VisibleRowCount;
        }

        /// <summary>
        ///Sherben per te vendosur filtra tek header-i i grides (mund te ndryshohet me vone per tu
        ///bere  me e konfigurueshme
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ASPxGridView_KonfigPivotGrid_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            if (e.Column.FieldName == "EmriPivotGridKoka" || e.Column.FieldName == "PershkrimiPivotGridKoka")
            {
                e.Values.Clear();
                e.AddValue("(Te gjithe)", string.Empty, "true");
                e.AddValue("Nga A-D ", string.Empty, e.Column.FieldName + ">'A     ' and " + e.Column.FieldName + " <'DDDDDDD'");
                e.AddValue("Nga D-G ", string.Empty, e.Column.FieldName + ">'D     ' and " + e.Column.FieldName + "<'GGGGGGG'");
                e.AddValue("Nga H-K ", string.Empty, e.Column.FieldName + ">'H     ' and " + e.Column.FieldName + "<'KKKKKKK'");
                e.AddValue("Nga L-O ", string.Empty, e.Column.FieldName + ">'L     ' and " + e.Column.FieldName + "  <'OOOOOOO'");
                e.AddValue("Nga P-S ", string.Empty, e.Column.FieldName + ">'P     ' and " + e.Column.FieldName + "<'SSSSSSS'");
                e.AddValue("Nga T-W ", string.Empty, e.Column.FieldName + ">'T     ' and " + e.Column.FieldName + "<'WWWWWWW'");
                e.AddValue("Nga X-Z ", string.Empty, e.Column.FieldName + ">'X     ' and " + e.Column.FieldName + "<'ZZZZZZZ'");
            }
            else
            {
                e.Values.Clear();
                e.AddValue("(Te gjithe)", string.Empty, "true");
            }
        }

        /// <summary>
        /// Percakton opsione te grides ne lidhje me filtrat, selektimin e rreshtave
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ASPxGridView_KonfigPivotGrid_DataBound(object sender, EventArgs e)
        {
            if (ASPxGridView_KonfigPivotGrid.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per perzgjidh
                DevExpress.Web.GridViewCommandColumn check = new DevExpress.Web.GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true; check.Width = Unit.Percentage(2);
                //behet per te afishuar rreshtin qe do sherbej per filtrim
                ASPxGridView_KonfigPivotGrid.Settings.ShowFilterRow = true;
                ASPxGridView_KonfigPivotGrid.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                ASPxGridView_KonfigPivotGrid.Settings.ShowFilterRowMenu = true;
                ASPxGridView_KonfigPivotGrid.Columns.Add(check);
                ASPxGridView_KonfigPivotGrid.KeyFieldName = "IdKonfPivotGridaKoka";
                //ASPxGridView_KonfigPivotGrid.SettingsBehavior.AllowSelectByRowClick = true; //obsolete
                ASPxGridView_KonfigPivotGrid.SettingsBehavior.AllowSelectByRowClick = true;
                ASPxGridView_KonfigPivotGrid.SettingsBehavior.AllowFocusedRow = true;
                ASPxGridView_KonfigPivotGrid.Columns["CollapsedStateLayout"].Visible = false;
                ASPxGridView_KonfigPivotGrid.Columns["FilterExpression"].Visible = false;
            }
        }

        /// <summary>
        /// perdoret per te rikonfiguruar griden kur ndryshohet konfigurimi
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxGridView_KonfigPivotGrid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string idkomponente = "";
            string kodkonfigurimi = "";
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                kodkonfigurimi = arr[1];
                idkomponente = arr[0];
                if (arr[2] == "")
                    ASPxGridView_KonfigPivotGrid.FilterExpression = "";
                else
                {
                    //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    //filtra.mbushFiltraGridaSipasFiltraKodi(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "ASPxGridView_KonfigPivotGrid", "Raport_PivotGrid.aspx", idNdermarrje);
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], idNdermarrje, koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        this.ASPxGridView_KonfigPivotGrid.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, ASPxGridView_KonfigPivotGrid);

                        konfiguroVleraFillestare(idGjuha, (int)hfState["idModuli"]);
                        hfStatusi.Value = "true";
                    }
                    else hfStatusi.Value = "false";
                }
            }
            else
                if (arr.Length == 2)//nese eshte zgjedhur nje konfigurim tek combo e konfigurimeve
            {
                kodkonfigurimi = arr[1];
                idkomponente = arr[0];
            }
            else
            {
                idkomponente = e.Parameters;
            }
            ASPxGridView_KonfigPivotGrid.Selection.UnselectAll();
        }

        /// <summary>
        /// Thirret sa here qe behet Callback dhe ben edhe njeher lidhjen me datasource-in
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ASPxGridView_KonfigPivotGrid_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            if (e.CallbackName == "APPLYFILTER" && e.Args[0] == "")
            {
                DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
                ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
                cmbFiltra.Text = "";
            }
        }

        /// <summary>
        /// Ne callback te pivot grides behet rimbushja me te dhena ne varesi te konfigurimit te zgjedhur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ASPxPivotGridRaporti_CustomCallback(object sender, PivotGridCustomCallbackEventArgs e)
        {

            //nese eshte hera e pare ngarko konfigun nga db
            if (CallbackState.Get("firstCallback").Equals("1"))
            {
                object rreshtat = ASPxGridView_KonfigPivotGrid.GetRowValues(ASPxGridView_KonfigPivotGrid.FocusedRowIndex, "IdKonfPivotGridaKoka");
                if (rreshtat != null)
                {
                    kokaPG = new DbCore.DbRegjistrim.clsKonfigPivotGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), Convert.ToInt32(rreshtat));
                    ASPxPivotGridRaporti.JSProperties["cpGrandTotalKolona"] = kokaPG.GrandTotalKolona;
                    ASPxPivotGridRaporti.JSProperties["cpColumnTotal"] = kokaPG.TotalKolona;
                    ASPxPivotGridRaporti.JSProperties["cpRowGrandTotal"] = kokaPG.GrandTotalRreshta;
                    ASPxPivotGridRaporti.JSProperties["cpRowTotal"] = kokaPG.TotalRreshta;
                    ASPxPivotGridRaporti.JSProperties["cpHiqVleraZero"] = kokaPG.HiqVleraZero;
                    // ASPxPivotGridRaporti.JSProperties["cpFirstCallback"] = false;
                    ColumnGrandTotal.Checked = kokaPG.GrandTotalKolona;
                    ColumnTotal.Checked = kokaPG.TotalKolona;
                    RowTotal.Checked = kokaPG.TotalRreshta;
                    RowGrandTotal.Checked = kokaPG.GrandTotalRreshta;
                    HiqVleraZero.Checked = kokaPG.HiqVleraZero;
                    RestoreStoredStateFromDB(kokaPG);
                    CacheLayer.GlobalCacheManager.MySessionCache["pivotStoredState"] = kokaPG;


                }
            }
            else
            {//merr vlerat per te bere update-in client side
                ASPxPivotGridRaporti.JSProperties["cpGrandTotalKolona"] = ColumnGrandTotal.Checked;
                ASPxPivotGridRaporti.JSProperties["cpColumnTotal"] = ColumnTotal.Checked;
                ASPxPivotGridRaporti.JSProperties["cpRowGrandTotal"] = RowGrandTotal.Checked;
                ASPxPivotGridRaporti.JSProperties["cpRowTotal"] = RowTotal.Checked;
                ASPxPivotGridRaporti.JSProperties["cpHiqVleraZero"] = HiqVleraZero.Checked;


            }

            if (e.Parameters == "UNDO")
            {
                StateController.LoadPrevState();
                CommitStateChanges();
            }
            else if (e.Parameters == "REDO")
            {
                StateController.LoadNextState();
                CommitStateChanges();
            }
            else
            {


                String filtriDtVisibility = e.Parameters;
                konfiguroFushatPivotGride(filtriDtVisibility);
                StateController.ClearState();

                SaveCurrentStateAndUpdateControlsState();
            }
            SetTotalsVisibility();
        }

        /// <summary>
        /// Kjo metode vendos bosh datasource-in e chartit ne rastin kur te dhenat e pivotgrides jane jonumerike
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ASPxPivotGridRaporti_CustomChartDataSourceData(object sender, PivotCustomChartDataSourceDataEventArgs e)
        {
            if (e.ItemType == DevExpress.XtraPivotGrid.PivotChartItemType.CellItem)
            {
                decimal decimalValue;
                if (!decimal.TryParse(e.Value.ToString(), out decimalValue))
                    e.Value = null;
                else if (e.Value == DBNull.Value || (CellValueThreshold.Value != null && (decimal)e.Value < Convert.ToDecimal(CellValueThreshold.Value)))
                    e.Value = 0;
            }
            //if ((e.ItemType == DevExpress.XtraPivotGrid.PivotChartItemType.CellItem) && (!Microsoft.VisualBasic.Information.IsNumeric(e.Value)))
            //    e.Value = null;
        }

        /// <summary>
        /// Vendos tipin e grafikut, dhe nqs te dhenat nuk jane te tipit numeric e vendos datasourc-in e chartit = null
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void WebChart_CustomCallback(object sender, DevExpress.XtraCharts.Web.CustomCallbackEventArgs e)
        {
            ASPxComboBox cmbGrafiku = (ASPxComboBox)navBarFiltrat.Groups[0].FindControl("cmbGrafiku");
            WebChart.SeriesTemplate.ChangeView((ViewType)Enum.Parse(typeof(ViewType), cmbGrafiku.SelectedItem.Text));
        }

        /// <summary>
        /// Ben bind pivotgriden me datasource-in perkates, sipas konfigurimit te zgjedhur
        /// </summary>
        /// <param name="idModuli"></param>
        private void shtoTeDhenatPivotGrid(int idNdermarrje, int idPerdorues, int gjuha, int idModuli)
        {

            var fushatPivot = JsonConvert.DeserializeObject<Dictionary<string, string>[]>(fushatRaporti.Value);

            if (fushatPivot != null)
            {
                if (fushatPivot.Length == 0) kaTeDhenaJoNumerike = true;
                string[] kolonat = new String[fushatPivot.Length];
                for (int i = 0; i < fushatPivot.Length; i++)
                {
                    var fushaKonfig = fushatPivot[i];
                    kolonat[i] = fushaKonfig["emerKoloneDB"];

                    if (fushaKonfig["zona"].Equals("Data") && !fushaKonfig["tipiKolones"].Equals("Numeric"))
                        kaTeDhenaJoNumerike = true;
                }
                string kolonatString = string.Join(",", kolonat);
                DataView dv = mbushTeDhenaPivotGridNgaSession(kolonatString, idNdermarrje, idPerdorues, gjuha, idModuli);

                if (dv != null)
                {
                    ASPxPivotGridRaporti.DataSource = dv.ToTable();
                    ASPxPivotGridRaporti.DataBind();
                }
                if (WebChart.Visible)
                {
                    if (!kaTeDhenaJoNumerike)
                        WebChart.DataSourceID = "ASPxPivotGridRaporti";
                    else
                        //WebChart.DataSourceID = null;
                        WebChart.DataSourceID = String.Empty;
                    WebChart.DataBind();
                }

            }
        }



        /// <summary>
        /// Percakton fushat e pivotgrides dhe ndarjen e tyre ne zona: rreshta, shtylla, kolona, data apo filter
        /// </summary>
        /// <param name="filtriDtVisibility"></param>
        private void konfiguroFushatPivotGride(String filtriDtVisibility)
        {
            JavaScriptSerializer serializusi = new JavaScriptSerializer();
            object[] fushatPivot = (object[])serializusi.DeserializeObject(fushatRaporti.Value);
            CacheLayer.GlobalCacheManager.MySessionCache["fushatPivotGrid"] = fushatPivot;
            ASPxPivotGridRaporti.Fields.Clear();
            String[] query = new String[fushatPivot.Length];
            for (int i = 0; i < fushatPivot.Length; i++)
            {
                Dictionary<string, object> fushaKonfig = (Dictionary<string, object>)fushatPivot[i];
                PivotGridField fusha = new PivotGridField();
                fusha.ID = "field" + fushaKonfig["emerKoloneDB"].ToString();
                fusha.FieldName = fushaKonfig["emerKoloneDB"].ToString();
                fusha.Caption = string.Format("{0}({1})", fushaKonfig["emrikolonesshfaq"], fushaKonfig["grupimKolone"]);

                string tipiKolones = fushaKonfig["tipiKolones"].ToString();
                if (tipiKolones.Equals("DateTime"))
                {
                    fusha.ValueFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    fusha.ValueFormat.FormatString = "dd/MM/yyyy";

                    fusha.CellFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    fusha.CellFormat.FormatString = "dd/MM/yyyy";
                }
                switch (fushaKonfig["zona"].ToString())
                {
                    case "Row":
                        fusha.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
                        break;

                    case "Column":
                        fusha.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
                        break;

                    case "Data":
                        fusha.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;


                        if (tipiKolones.Equals("Numeric") || tipiKolones.Equals("Int"))
                        {
                            string formati = tipiKolones.Equals("Numeric") ? "n2" : "n0";

                            peraktoSummaryType(fusha);

                            fusha.CellFormat.FormatType = FormatType.Numeric;
                            fusha.CellFormat.FormatString = formati;
                            fusha.TotalCellFormat.FormatType = FormatType.Numeric;
                            fusha.TotalCellFormat.FormatString = formati;
                            fusha.GrandTotalCellFormat.FormatType = FormatType.Numeric;
                            fusha.GrandTotalCellFormat.FormatString = formati;

                        }
                        else
                        {
                            fusha.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Max;

                        }
                        break;

                    case "Filter":
                        fusha.Area = DevExpress.XtraPivotGrid.PivotArea.FilterArea;
                        break;

                    default: break;
                }
                fusha.AreaIndex = Convert.ToInt32(fushaKonfig["rendi"]);
                if (fushaKonfig["width"].ToString() != String.Empty)
                    fusha.Width = Convert.ToInt32(fushaKonfig["width"]);

                //if (shfaqSubtotalet.Checked == true)
                //{
                //    fusha.CustomTotals.Clear();
                //    fusha.TotalsVisibility = DevExpress.XtraPivotGrid.PivotTotalsVisibility.CustomTotals;
                //    fusha.CustomTotals.Add(DevExpress.Data.PivotGrid.PivotSummaryType.Sum);
                //}
                //else
                //    fusha.TotalsVisibility = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None;

                ASPxPivotGridRaporti.Fields.Add(fusha);

                if (tipiKolones.Equals("DateTime"))
                {
                    percaktoGroupInterval(fusha);
                }
            }
            //if (filtriDtVisibility.Equals("visible"))
            //{
            //}
            //else
            //    ASPxPivotGridRaporti.Prefilter.Clear();
            //ASPxPivotGridRaporti.OptionsView.ShowGrandTotalsForSingleValues = false;
            //ASPxPivotGridRaporti.OptionsView.ShowColumnGrandTotals = true;
        }

        private Tuple<DateTime, DateTime> MerrDateFillimiDheMbarimi()
        {
            ASPxComboBox cmbFilterDate = (ASPxComboBox)navBarFiltrat.Groups[0].FindControl("filterDate");
           
            if (cmbFilterDate.Text != null)
            {
                DateTime dt1 = DateTime.MinValue;
                DateTime dt2 = DateTime.MinValue;
                
                switch (((ASPxRadioButtonList)navBarFiltrat.Groups[0].FindControl("radDtDok")).SelectedItem.Value.ToString())
                {
                    case "Aktuale":
                        clsPeriudhaKontabel periudhaKontabel = DbCore.mySessionObjects.merrPeriudheKontabel(Session);
                        dt1 = periudhaKontabel.FillimiPeriudha;
                        dt2 = periudhaKontabel.MbarimiPeriudha;
                        break;

                    case "Periudha":
                        dt1 = ((ASPxDateEdit)navBarFiltrat.Groups[0].FindControl("txtNgaDok")).Date;
                        dt2 = ((ASPxDateEdit)navBarFiltrat.Groups[0].FindControl("txtDeriDok")).Date;
                        break;

                    case "VitiUshtrimor":
                        clsNdermarrjeViti ndermviti = new clsNdermarrjeViti(DbCore.mySessionObjects.ktheNdermarrjeVit(Session));
                        dt1 = ndermviti.NdermarrjeVitiFillim;
                        dt2 = ndermviti.NdermarrjeVitiFund;
                        break;

                    case "GjitheVitet":
                        clsNdermarrjeViti ndermvit = new clsNdermarrjeViti();
                        ndermvit.mbushNdermVitFillimFundPerGjitheVitetSipasNdermarjes(idNdermarrje);
                        dt1 = ndermvit.NdermarrjeVitiFillim;
                        dt2 = ndermvit.NdermarrjeVitiFund;
                        break;
                    default:
                        break;
                }
                return new Tuple<DateTime, DateTime>(dt1, dt2);

            }

            return null;
        }
        private string ktheFilterDatePerSp()
        {
            var datat = MerrDateFillimiDheMbarimi();
            if (datat == null)
                return "";
            return String.Format(" AND ({0}>=convert(datetime,'{1}',103) and {0} <=convert(datetime,'{2}',103) )", "T_KOKALISTPAGESE.DTDOK", datat.Item1.ToShortDateString(), datat.Item2.ToShortDateString());

        }

        /// <summary>
        /// kthen nje filter per te filtruar datasource-in duke u bazuar tek periudha e zgjedhur
        /// </summary>
        /// <returns></returns>
        private string ktheFilterDataView()

        {
           
            string rowFilter = "";
            string fusha;
            var datat = MerrDateFillimiDheMbarimi();
            if (datat != null)
            {
                ASPxComboBox cmbFilterDate = (ASPxComboBox)navBarFiltrat.Groups[0].FindControl("filterDate");
                if (cmbFilterDate.Value != null)
                 {
                    fusha = cmbFilterDate.Value.ToString();
                    rowFilter = string.Format("{0} >=#{1}# and {2} <= #{3}#", fusha, datat.Item1.ToString("MM/dd/yyyy"), fusha, datat.Item2.ToString("MM/dd/yyyy"));
                 }
                     
            }
            return rowFilter;
        }
        /// <summary>
        /// Krijon filtrin e dates sipas periudhes qe ka filtruar perdouresi ne ambjent
        /// </summary>
        /// <returns></returns>
        /// <param name="ci"></param>
        /// <summary>
        /// Percakton llojin e summary qe do i behet te dhenave sipas zgjedhjes qe ka bere perdoruesi te filtri perkates
        /// </summary>
        /// <returns></returns>
        private void peraktoSummaryType(PivotGridField fusha)
        {
            ASPxComboBox cmbVepLlogaritese = (ASPxComboBox)navBarFiltrat.Groups[0].FindControl("cmbVepLlogaritese");
            int grupim = -1;

            if (HfGrupimi.Contains(fusha.FieldName))
            {
                grupim = Convert.ToInt32(HfGrupimi.Get(fusha.FieldName));
                fusha.Caption = string.Format("{0}({1})", fusha.Caption, cmbVepLlogaritese.Items[grupim].Text);
            }

            switch (grupim)
            {
                case 0:
                    fusha.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Sum;
                    break;

                case 1:
                    fusha.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Min;
                    break;

                case 2:
                    fusha.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Max;
                    break;

                case 3:
                    fusha.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Average;
                    break;

                case 4:
                    fusha.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Count;
                    break;

                default:
                    fusha.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Sum;
                    break;
            }
        }

      

        private void percaktoGroupInterval(PivotGridField fushaKonfig)
        {
            int idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            string labelViti = MessagesResource.Messages["labelViti"];
            string labelRaport3Mujore = MessagesResource.Messages["labelRaport3Mujore"];
            string labelFilterKryesorMuaji = MessagesResource.Messages["labelFilterKryesorMuaji"];
            String[] fushat = { labelViti, labelRaport3Mujore, labelFilterKryesorMuaji };
            //String[] fushat = { "Viti", "3Mujori", "Muaji" };
            //string zona = fushaKonfig["zona"].ToString();
            foreach (String fusheString in fushat)
            {
                //PivotGridField fusha = new PivotGridField() { ID = "field" + fusheString, FieldName = "DTDOK", Caption = fusheString };
                PivotGridField fusha = new PivotGridField();

                fusha.ID = "field" + fushaKonfig.FieldName + fusheString;
                fusha.FieldName = fushaKonfig.FieldName;
                fusha.Caption = string.Format("{0} ({1})", fusheString, fushaKonfig.Caption);

                fusha.Area = fushaKonfig.Area;

                if (fusheString == labelViti)
                {
                    fusha.GroupInterval = DevExpress.XtraPivotGrid.PivotGroupInterval.DateYear;
                    fusha.AreaIndex = 1;
                }
                if (fusheString == labelRaport3Mujore)
                {
                    fusha.GroupInterval = DevExpress.XtraPivotGrid.PivotGroupInterval.DateQuarter;
                    fusha.AreaIndex = 2;
                    fusha.ValueFormat.FormatType = FormatType.Numeric;
                    fusha.ValueFormat.FormatString = "3 mujori {0}";
                }
                if (fusheString == labelFilterKryesorMuaji)
                {
                    fusha.GroupInterval = DevExpress.XtraPivotGrid.PivotGroupInterval.DateMonth;
                    fusha.AreaIndex = 3;
                }
                fusha.Visible = false;
                ASPxPivotGridRaporti.Fields.Add(fusha);
            }

            ASPxPivotGridRaporti.Fields[fushaKonfig.FieldName].Visible = false;
            //ASPxComboBox cmbPerioda = (ASPxComboBox)navBarFiltrat.Groups[0].FindControl("cmbPerioda");

            //ASPxComboBox fushatDateTime = navBarFiltrat.Groups[0].FindControl("fushatDateTimePG") as ASPxComboBox;

            int grupim = 5;

            if (HfGroupIntervalet.Contains(fushaKonfig.FieldName))
            {
                grupim = Convert.ToInt32(HfGroupIntervalet.Get(fushaKonfig.FieldName));
            }
            switch (grupim)
            {
                case 0:
                    ASPxPivotGridRaporti.Fields["field" + fushaKonfig.FieldName + labelViti].Visible = true;
                    break;

                case 1:
                    ASPxPivotGridRaporti.Fields["field" + fushaKonfig.FieldName + labelRaport3Mujore].Visible = true;
                    break;

                case 2:
                    ASPxPivotGridRaporti.Fields["field" + fushaKonfig.FieldName + labelFilterKryesorMuaji].Visible = true;
                    break;

                case 3:
                    {
                        ASPxPivotGridRaporti.Fields["field" + fushaKonfig.FieldName + labelViti].Visible = true;
                        ASPxPivotGridRaporti.Fields["field" + fushaKonfig.FieldName + labelRaport3Mujore].Visible = true;
                        break;
                    }
                case 4:
                    {
                        ASPxPivotGridRaporti.Fields["field" + fushaKonfig.FieldName + labelViti].Visible = true;
                        ASPxPivotGridRaporti.Fields["field" + fushaKonfig.FieldName + labelRaport3Mujore].Visible = true;
                        ASPxPivotGridRaporti.Fields["field" + fushaKonfig.FieldName + labelFilterKryesorMuaji].Visible = true;
                        break;
                    }
                case 5:
                    {
                        ASPxPivotGridRaporti.Fields[fushaKonfig.FieldName].Visible = true;
                        break;
                    }
                default: throw new DbCore.MyException("Periudhe e pa njohur");
            }
        }

        /// <summary>
        /// Ben exportin e raportit te gjeneruar ne formatin e zgjedhur nga perdoruesi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void exportoRaport(object sender, EventArgs e)
        {
            string fileName = MessagesResource.Messages["labelFilterKryesorRaporti"];
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemExport");
            ASPxComboBox cmbExport = ((PlatinumWeb.MenuExport)(itemButton.Template)).FindControl("cmbExport") as ASPxComboBox;
            ASPxPivotGridExporterRaporti.OptionsPrint.PageSettings.Landscape = true;
            ASPxPivotGridExporterRaporti.OptionsPrint.PrintDataHeaders = DefaultBoolean.False;
            try
            {
                switch (cmbExport.SelectedIndex)
                {
                    case 0:
                        ASPxPivotGridExporterRaporti.ExportPdfToResponse(fileName, true);
                        break;

                    case 1:
                        ASPxPivotGridExporterRaporti.ExportXlsxToResponse(fileName, new XlsxExportOptionsEx() { ExportType = DevExpress.Export.ExportType.WYSIWYG }, true);
                        break;

                    case 2:
                        var opts = new XlsExportOptionsEx
                        {
                            Suppress256ColumnsWarning = true,
                            ExportType = DevExpress.Export.ExportType.WYSIWYG
                        };

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
                string mesazhi = "Kubi ka shume te dhena dhe nuk mund te eksportohet i tere ne excel. Ju lutem filtroni me pak te dhena";
                NLog.LogManager.GetCurrentClassLogger().Error(mesazhi, err.Message);
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhi, pnlMesazhi);
            }
        }

        protected void ASPxPivotGridRaporti_PageIndexChanged(object sender, EventArgs e)
        {
            SetTotalsVisibility();
        }

        protected void cmbSaVleraNeGrafik_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSaVleraNeGrafik.Text != "")
            {
                if (cmbSaVleraNeGrafik.Text == "Te gjitha")
                {
                    ASPxPivotGridRaporti.OptionsChartDataSource.MaxAllowedPointCountInSeries = 0;
                    ASPxPivotGridRaporti.OptionsChartDataSource.MaxAllowedSeriesCount = 0;
                }
                else
                {
                    ASPxPivotGridRaporti.OptionsChartDataSource.MaxAllowedPointCountInSeries = int.Parse(cmbSaVleraNeGrafik.Text);
                    ASPxPivotGridRaporti.OptionsChartDataSource.MaxAllowedSeriesCount = int.Parse(cmbSaVleraNeGrafik.Text);
                }
            }
        }

        protected void sipasKolonaveChk_ValueChanged(object sender, EventArgs e)
        {
            ASPxPivotGridRaporti.OptionsChartDataSource.ProvideDataByColumns = sipasKolonaveChk.Checked;
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        private void EmrateLabelave()
        {
            //(ASPxPageControl1.TabPages[2].FindControl("RowGrandTotal") as ASPxCheckBox).Text =
            RowGrandTotal.Text = MessagesResource.Messages["rowGrandTotal"];
            //(ASPxPageControl1.TabPages[2].FindControl("RowTotal") as ASPxCheckBox).Text =
            RowTotal.Text = MessagesResource.Messages["rowTotal"];
            //(ASPxPageControl1.TabPages[2].FindControl("ColumnGrandTotal") as ASPxCheckBox).Text =
            ColumnGrandTotal.Text = MessagesResource.Messages["columnGrandTotal"];
            //(ASPxPageControl1.TabPages[2].FindControl("ColumnTotal") as ASPxCheckBox).Text =
            ColumnTotal.Text = MessagesResource.Messages["columnTotal"];
            HiqVleraZero.Text = MessagesResource.Messages["lblFshiVleraZero"];
            popFshi.HeaderText = MessagesResource.Messages["labelRaportKujdes"];
            lblMsgbox.Text = MessagesResource.Messages["labelRaportMesazhJeniSigurt"];
            ButtonOk.Text = MessagesResource.Messages["labelRaportOKButtonCancel"];
            ButtonCancel.Text = MessagesResource.Messages["labelRaportAnullo"];
            ASPxPageControl1.TabPages[0].Text = MessagesResource.Messages["labelRaportTePergjithshme"];
            ASPxPageControl1.TabPages[1].Text = MessagesResource.Messages["labelRaportKonfigurimi"];
            ((ASPxLabel)ASPxPageControl1.TabPages[1].FindControl("lblKodi")).Text = MessagesResource.Messages["filterNrPersonalPunonjesi"];
            ((ASPxLabel)ASPxPageControl1.TabPages[1].FindControl("lblAutorizimi")).Text = MessagesResource.Messages["lblNivelAutorizimi"];
            ((ASPxTextBox)ASPxPageControl1.TabPages[1].FindControl("kodi_TextBox")).ValidationSettings.RegularExpression.ErrorText = MessagesResource.Messages["labelRaportMesazhNrKaraketereshKodi"];
            ((ASPxLabel)ASPxPageControl1.TabPages[1].FindControl("lblPershkrimi")).Text = MessagesResource.Messages["filterRaportPershkrimi"];
            ASPxPageControl1.TabPages[2].Text = MessagesResource.Messages["labelFilterKryesorRaporti"];
            navBarFiltrat.Groups[0].Text = MessagesResource.Messages["FiltratEmertimi"];
            ((ASPxLabel)navBarFiltrat.Groups[0].FindControl("lblDtDok")).Text = MessagesResource.Messages["labelFilterZgjidhPeriudhen"];
            ((ASPxRadioButtonList)navBarFiltrat.Groups[0].FindControl("radDtDok")).Items[0].Text = MessagesResource.Messages["RadioButtonListEditItemAktuale"];
            ((ASPxRadioButtonList)navBarFiltrat.Groups[0].FindControl("radDtDok")).Items[1].Text = MessagesResource.Messages["RadioButtonListEditItemPeriudha"];
            ((ASPxRadioButtonList)navBarFiltrat.Groups[0].FindControl("radDtDok")).Items[2].Text = MessagesResource.Messages["RadioButtonListEditItemVitiUshtrimor"];
            ((ASPxRadioButtonList)navBarFiltrat.Groups[0].FindControl("radDtDok")).Items[3].Text = MessagesResource.Messages["RadioButtonListEditItemGjitheVitet"];





            ((ASPxLabel)navBarFiltrat.Groups[0].FindControl("lblNgaDok")).Text = MessagesResource.Messages["labelRaportiNga"];
            ((ASPxLabel)navBarFiltrat.Groups[0].FindControl("lblDeriDok")).Text = MessagesResource.Messages["labelRaportDeri"];
            ((ASPxLabel)navBarFiltrat.Groups[0].FindControl("lblPerioda")).Text = MessagesResource.Messages["labelRaportPerioda"];
            ((ASPxComboBox)navBarFiltrat.Groups[0].FindControl("cmbPerioda")).Items[0].Text = MessagesResource.Messages["labelRaportVjetore"];
            ((ASPxComboBox)navBarFiltrat.Groups[0].FindControl("cmbPerioda")).Items[1].Text = MessagesResource.Messages["labelRaport3Mujore"];
            ((ASPxComboBox)navBarFiltrat.Groups[0].FindControl("cmbPerioda")).Items[2].Text = MessagesResource.Messages["labelRaportMujore"];
            ((ASPxComboBox)navBarFiltrat.Groups[0].FindControl("cmbPerioda")).Items[3].Text = MessagesResource.Messages["labelRaportVjetore3Mujore"];
            ((ASPxComboBox)navBarFiltrat.Groups[0].FindControl("cmbPerioda")).Items[4].Text = MessagesResource.Messages["labelRaportVjetore3MujoreMujore"];
            ((ASPxComboBox)navBarFiltrat.Groups[0].FindControl("cmbPerioda")).Items[5].Text = MessagesResource.Messages["labelRaportDitorePerioda"];
            ((ASPxLabel)navBarFiltrat.Groups[0].FindControl("ASPxLabel1")).Text = MessagesResource.Messages["labelRaportVeprimeLlogaritese"];
            ((ASPxComboBox)navBarFiltrat.Groups[0].FindControl("cmbVepLlogaritese")).Items[0].Text = MessagesResource.Messages["labelRaportiShuma"];
            ((ASPxComboBox)navBarFiltrat.Groups[0].FindControl("cmbVepLlogaritese")).Items[1].Text = MessagesResource.Messages["cmbItemRaportMin"];
            ((ASPxComboBox)navBarFiltrat.Groups[0].FindControl("cmbVepLlogaritese")).Items[2].Text = MessagesResource.Messages["cmbItemRaportMax"];
            ((ASPxComboBox)navBarFiltrat.Groups[0].FindControl("cmbVepLlogaritese")).Items[3].Text = MessagesResource.Messages["cmbItemRaportMesatare"];
            ((ASPxComboBox)navBarFiltrat.Groups[0].FindControl("cmbVepLlogaritese")).Items[4].Text = MessagesResource.Messages["cmbItemRaportNumri"];
            ((ASPxLabel)navBarFiltrat.Groups[0].FindControl("tipiGrafikutLabel")).Text = MessagesResource.Messages["labelRaportTipiGrafikut"];
            ((ASPxButton)ASPxPageControl1.TabPages[2].FindControl("ButtonUndo")).Text = MessagesResource.Messages["btnRaportUndo"];
            ((ASPxButton)ASPxPageControl1.TabPages[2].FindControl("ButtonRedo")).Text = MessagesResource.Messages["btnRaportRedo"];
            ((ASPxButton)ASPxPageControl1.TabPages[2].FindControl("btnGrafik")).Text = MessagesResource.Messages["btnRaportMeGrafik"];
            ((ASPxButton)ASPxPageControl1.TabPages[2].FindControl("showFilterbtn")).Text = MessagesResource.Messages["showFilterbtn"];
            ((ASPxButton)ASPxPageControl1.TabPages[2].FindControl("btnPAGrafik")).Text = MessagesResource.Messages["btnRaportPaGrafik"];
            ((ASPxCheckBox)ASPxPageControl1.TabPages[2].FindControl("sipasKolonaveChk")).Text = MessagesResource.Messages["checkboxRaportTeDhenaSipasKolonave"];
            ((ASPxLabel)ASPxPageControl1.TabPages[2].FindControl("shfaqZeroLabel")).Text = MessagesResource.Messages["labelRaportTeBarabartaMeZeroVlerat"];
            ((ASPxLabel)ASPxPageControl1.TabPages[2].FindControl("saVleraNeGrafikLabel")).Text = MessagesResource.Messages["labelRaportNrVlNeGrafik"];
            hfState.Set("msgZgjidhniAutorizimet", MessagesResource.Messages["msgZgjidhniAutorizimet"]);
            hfState.Set("lblRaportMesazhNukKeniZgjedhur", MessagesResource.Messages["lblRaportMesazhNukKeniZgjedhur"]);
        }

        private void LoadString(ASPxHiddenField HfMsgKonfig)
        {
            HfMsgKonfig.Clear();
            HfMsgKonfig.Add("MsgZgjidhKonfig", MessagesResource.Messages["MsgRaportZgjidhKonfig"]);

        }

        #region UNDO && REDO PivotGrid

        private PivotStateController stateController;

        private PivotStateController StateController
        {
            get
            {
                if (stateController == null)
                    stateController = new PivotStateController(ASPxPivotGridRaporti, Session);
                return stateController;
            }
        }

        private bool HasStateController { get { return stateController != null; } }

        /// <summary>
        ///kthen filterexpression si dhe  konfigurimin collapsed/expanded per fushat e pivot grides nga DB
        /// e merr nga sessioni per te mos krijuar e kerkuar dy here nga db,
        /// layouti merret nga db ne callback te callbackPanel-it te checkbox-eve
        /// </summary>
        private void RestoreStoredStateFromDB(DbCore.DbRegjistrim.clsKonfigPivotGridaKoka state)
        {

            if (!string.IsNullOrWhiteSpace(state.FieldStateLayout))
                ASPxPivotGridRaporti.LoadCollapsedStateFromString(state.FieldStateLayout);
            ASPxPivotGridRaporti.Prefilter.CriteriaString = state.FilterExpression;

        }

        private void SaveCurrentStateAndUpdateControlsState()
        {
            StateController.SaveCurrentState();
            CommitStateChanges();
        }

        private void CommitStateChanges()
        {
            SetUpdateButtonsJSProperties();
            StateController.SaveStateToSession(Session);
        }

        private void SetUpdateButtonsJSProperties()
        {
            ASPxPivotGridRaporti.JSProperties["cpIsUndoEnabled"] = StateController.CanLoadPrevState;
            ASPxPivotGridRaporti.JSProperties["cpIsRedoEnabled"] = StateController.CanLoadNextState;
        }

        protected void ASPxPivotGridRaporti_GridLayout(object sender, EventArgs e)
        {
            if (!HasStateController && !ASPxPivotGridRaporti.IsPrefilterPopupVisible)
                SaveCurrentStateAndUpdateControlsState();
        }

        private class PivotStateController
        {
            private StateStorage storage;
            private const string SessionStateStorageKey = "StateStorage";

            public PivotStateController(ASPxPivotGrid pivotGrid, HttpSessionState session)
            {
                PivotGrid = pivotGrid;
                this.storage = (StateStorage)CacheLayer.GlobalCacheManager.MySessionCache[SessionStateStorageKey];
                if (this.storage == null)
                    this.storage = new StateStorage();
            }

            private StateStorage Storage { get { return storage; } }

            private ASPxPivotGrid PivotGrid { get; }

            public bool CanLoadPrevState { get { return !Storage.IsFirstLocation; } }

            public bool CanLoadNextState { get { return !Storage.IsLastLocation; } }

            public void ClearState()
            {
                Storage.Clear();
            }

            public void SaveCurrentState()
            {
                string layoutState = PivotGrid.SaveLayoutToString();
                string collapsedState = PivotGrid.SaveCollapsedStateToString();
                Storage.AddNewState(new StateRecord(layoutState, collapsedState));
            }

            public void LoadPrevState()
            {
                StateRecord state = Storage.GetPrevState();
                LoadState(state);
            }

            public void LoadNextState()
            {
                StateRecord state = Storage.GetNextState();
                LoadState(state);
            }

            private void LoadState(StateRecord state)
            {
                PivotGrid.LoadLayoutFromString(state.LayoutState);
                PivotGrid.LoadCollapsedStateFromString(state.CollapsedState);
            }

            public void SaveStateToSession(HttpSessionState session)
            {
                CacheLayer.GlobalCacheManager.MySessionCache[SessionStateStorageKey] = storage;
            }
        }

        [Serializable]
        private class StateStorage
        {
            private List<StateRecord> records;

            public StateStorage()
            {
                records = new List<StateRecord>();
                ClearState();
            }

            public void Clear()
            {
                ClearState();
            }

            private void ClearState()
            {
                Records.Clear();
                CurrentLocation = -1;
            }

            public List<StateRecord> Records { get { return records; } }

            public int CurrentLocation { get; set; }

            private bool IsEmpty { get { return CurrentLocation == -1; } }

            public bool IsFirstLocation
            {
                get
                {
                    if (IsEmpty)
                        return true;
                    return CurrentLocation == 0;
                }
            }

            public bool IsLastLocation
            {
                get
                {
                    if (IsEmpty)
                        return true;
                    return CurrentLocation == Records.Count - 1;
                }
            }

            public void AddNewState(StateRecord state)
            {
                if (!IsEmpty && CurrentLocation != Records.Count - 1)
                    Records.RemoveRange(CurrentLocation + 1, Records.Count - (CurrentLocation + 1));
                Records.Insert(++CurrentLocation, state);
            }

            public StateRecord GetPrevState()
            {
                if (IsEmpty || IsFirstLocation)
                    throw new Exception("Incorrect usage");
                return Records[--CurrentLocation];
            }

            public StateRecord GetNextState()
            {
                if (IsEmpty || IsLastLocation)
                    throw new Exception("Incorrect usage");
                return Records[++CurrentLocation];
            }
        }

        protected void ASPxPivotGridRaporti_CustomFieldValueCells(object sender, PivotCustomFieldValueCellsEventArgs e)
        {
            if (!HiqVleraZero.Checked) return;
            GridUtil.HideEmptyValues(e);
        }

        [Serializable]
        public class StateRecord
        {
            public StateRecord(string layoutState, string collapsedState)
            {
                LayoutState = layoutState;
                CollapsedState = collapsedState;
            }

            public string LayoutState { get; set; }

            public string CollapsedState { get; set; }
        }

        #endregion UNDO && REDO PivotGrid

        protected void shfaqSubtotalet_ValueChanged(object sender, EventArgs e)
        {
            //ASPxPivotGridRaporti.OptionsView.ShowGrandTotalsForSingleValues = shfaqSubtotalet.Checked;
            //showCustomTotals();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void callbackCheckBox_Callback(object sender, CallbackEventArgsBase e)
        {
            if (ASPxPageControl1.ActiveTabIndex == 2)
                shtoTeDhenatPivotGrid(idNdermarrje, idPerdoruesi, idGjuha, idModuli);
            if (CallbackState.Get("firstCallback").ToString() == "0")
            {
                object rreshtat = ASPxGridView_KonfigPivotGrid.GetRowValues(ASPxGridView_KonfigPivotGrid.FocusedRowIndex, "IdKonfPivotGridaKoka");
                if (rreshtat != null)
                {
                    kokaPG = new DbCore.DbRegjistrim.clsKonfigPivotGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), Convert.ToInt32(rreshtat));
                    ColumnGrandTotal.Checked = kokaPG.GrandTotalKolona;
                    ColumnTotal.Checked = kokaPG.TotalKolona;
                    RowGrandTotal.Checked = kokaPG.GrandTotalRreshta;
                    HiqVleraZero.Checked = kokaPG.HiqVleraZero;
                    RowTotal.Checked = kokaPG.TotalRreshta;
                    CacheLayer.GlobalCacheManager.MySessionCache["pivotStoredState"] = kokaPG;
                    CallbackState.Set("firstCallback", "1");

                }
            }
            if (e.Parameter == "UNDO")
            {
                StateController.LoadPrevState();
                CommitStateChanges();
            }
            else if (e.Parameter == "REDO")
            {
                StateController.LoadNextState();
                CommitStateChanges();
            }
            else
            {


                String filtriDtVisibility = e.Parameter;
                konfiguroFushatPivotGride(filtriDtVisibility);
                StateController.ClearState();

                SaveCurrentStateAndUpdateControlsState();
            }
            SetTotalsVisibility();

        }


        protected void cmbAutorizimi_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("cmbAutorizimi"))
                {
                    ConfigureAspxComboBox.mbushComboAutorizime((int)hfState.Get("idPerdoruesi"), cmbAutorizimi);
                }
            }
        }

    }
}