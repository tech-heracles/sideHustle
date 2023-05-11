using DbCore;
using DbCore.DbInventari;
using DbCore.DbShare;
using DbCore.DbRegjistrim;
using DevExpress.Web;
using PlatinumWeb.Templates;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Resources;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using DbCore.DbAsete;
using Newtonsoft.Json;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.DbAdmin;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Messages;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using System.Linq;
using DbCore.IMBUtils.Extensions;

namespace PlatinumWeb
{
    public partial class Shto_Artikull : MyPageBase
    {
        //private static int numFurnitoreshFillim = 3;
        //private string koloneFocus;
        ASPxTextBox temptxt = null;
        ASPxComboBox tempcombo = null;
        private DbCore.DbShare.clsFormatiKonfig formatNrPerKonfigCmimi = new DbCore.DbShare.clsFormatiKonfig();
        private string komponente = "Shto_Artikull.aspx";
        private string guidString;

        protected void Page_Load(object sender, EventArgs e)
        {
            int idPerdoruesi;
            int idGjuha;
            int idNdermarrje;
            int idViti;
            int idNdermarrjeVit;
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            string postStringTvsh = "";
            pubSubButton.ClientVisible = false;
            if (!Page.IsPostBack)
            {
                if (!DbCore.mySessionObjects.isLogedIn(Session))
                {
                    DbCore.clsFunksione.logout(Session, true, "FaqePaautorizuar");
                }
                idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
                {
                    Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerdoruesi);
                }
                idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
                idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
                idNdermarrjeVit = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
                guidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                hfState.Set("guidString", guidString);
                hfState.Set("idPerdoruesi", idPerdoruesi);
                hfState.Set("idGjuha", idGjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("idViti", idViti);
                hfState.Set("idNdermarrjeVit", idNdermarrjeVit);
                hfState.Set("Meme", DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
                postStringTvsh = rm.GetString("postStringTvsh", cultinf);
                hfState.Set(clsArtikulli.postStringTvsh, postStringTvsh);
                mbushHiddenFieldMePerkthime(cultinf, rm);
                EmrateTabeve(rm, cultinf);
                hfArkivaDokId.Value = Request.QueryString["id"];
                ASPxPageControl1.TabPages[5].ClientVisible = false;
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, "CmimeArtikulli.aspx?lloji=shitje");
                hfTeDrejtaCmimi.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejtaCmimi.Add("Modifikim", tedrejtaInfo.DMod);
                hfTeDrejtaCmimi.Add("Amb", tedrejtaInfo.DAmb);
                percaktoFormatNumriCmimet(idNdermarrje);

                konfiguroVleraFillestare(idPerdoruesi, idNdermarrje, rm, cultinf, idGjuha, postStringTvsh);

                bool kosto = false;
                bool gjendja = false;
                bool cmime = false;
                bool cmimeMeTvsh = false;
                int idKonfigurim = Convert.ToInt32(cmbKonfigurimi.Value.ToString());
                
                if (Request.QueryString["llojiart"] == "afatshkurter")
                {
                    string atrGjendja = DbCore.DbShare.clsAtributeTrupi.merrVleredefaultSipasKontrollitDheKonfigurimit(idKonfigurim, "cbGjendje", 402);
                    string atrKosto = DbCore.DbShare.clsAtributeTrupi.merrVleredefaultSipasKontrollitDheKonfigurimit(idKonfigurim, "cbKosto", 402);
                    string atrCmime = DbCore.DbShare.clsAtributeTrupi.merrVleredefaultSipasKontrollitDheKonfigurimit(idKonfigurim, "cbCmime", 402);
                    string atrCmimeMeTvsh = DbCore.DbShare.clsAtributeTrupi.merrVleredefaultSipasKontrollitDheKonfigurimit(idKonfigurim, "cbCmimeMeTvsh", 402);
                    if (atrGjendja == "true")
                        gjendja = true;
                    if (atrKosto == "true")
                        kosto = true;
                    if (atrCmime == "true")
                        cmime = true;
                    if (atrCmimeMeTvsh == "true")
                        cmimeMeTvsh = true;
                }
                ASPxPageControl1.ActiveTabIndex = 0;

                konfiguroBuxhetGride(idNdermarrje);
                //per momentin nuk duhet
              //  GridUtil.percaktoVisibleColumnsMeWidth((int)hfState["idGjuha"], idNdermarrje, gvBuxheti, "gvBuxheti", komponente); 

                konfiguroGrideCmimesh(idPerdoruesi, idNdermarrje);
                //DbCore.clsFunksione.percaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "gvCmimet", gvCmimet, cmbKonfigurimi.Text, Convert.ToString(402), (int)hfState["idGjuha"], !Page.IsPostBack);
                GridUtil.percaktoVisibleColumnsSipasKonfigurimit(gvCmimet, "gvCmimet", "Shto_Artikull.aspx?llojiart=afatshkurter", idKonfigurim, true, idGjuha, true);
                //DbCore.clsFunksione.percaktoVisibleColumnsMeWidth((int)hfState["idGjuha"], idNdermarrje, gvCmimet, "gvCmimet", komponente);

                if (!Page.IsCallback)
                {
                    konfiguroGrideArtikujPerberes("gvArtPerberes");
                    konfiguroGrideArtikujPerberes("gvGjendjeArt");
                    konfiguroGrideArtikujPerberes("gvArtVfone");
                }
                
                int id = 0;
                int.TryParse(hfId.Value, out id);
                ucFushatShtese.KonfiguroVleraFillestare(idNdermarrje, idPerdoruesi, idGjuha, komponente, "Artikulli", id, int.Parse(cmbKonfigurimi.Value.ToString()));
                postStringTvsh = (string)hfState.Get(clsArtikulli.postStringTvsh);
                mbushGridArtikujshNgaDB(idPerdoruesi, idNdermarrje, kosto, gjendja, cmime, cmimeMeTvsh,postStringTvsh );
                if (Request.QueryString["llojiart"] == "afatshkurter")
                {
                    ASPxGridView_Artikull.FilterExpression = "[LlojiArt]=false";
                }
                else
                {
                    ASPxGridView_Artikull.FilterExpression = "[LlojiArt]=true";
                    ASPxPageControl1.TabPages[7].ClientVisible = false;
                }
                DataTable kodeNivCmimi = mbushKodeNivelCmimiDheRuajNeSesion(idNdermarrje, idPerdoruesi);
                konfiguroGride(idNdermarrje, cmbKonfigurimi.Text, 402, kosto, gjendja, cultinf, rm, kodeNivCmimi, idPerdoruesi, cmime, cmimeMeTvsh, postStringTvsh);
                kodeNivCmimi.Dispose();
                konfiguroVleraFillestareNorma();
                konfiguroGrideNorma(idNdermarrje);
                konfiguroVleraFillestareNormaAmortizim();
                konfiguroGrideNormaAmortizimi(idNdermarrje);

                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, DbCore.clsFunksione.GetKomponente(Page.Request));
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                hfMeme.Value = DbCore.mySessionObjects.merrEshteMemeSesioni(Session).ToString();
                ASPxGridView_Artikull.Columns["#"].VisibleIndex = 0;
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "ASPxGridView_Artikull", ASPxGridView_Artikull, cmbKonfigurimi.Text, Convert.ToString(402), (int)hfState["idGjuha"], !Page.IsPostBack);
                GridUtil.percaktoVisibleColumnsMeWidth(idGjuha,idNdermarrje, gvAmortizimi, "gvAmortizimi", komponente);
                GridUtil.percaktoVisibleColumnsMeWidth(idGjuha, idNdermarrje, gvNormaAmortizimi, "gvNormaAmortizimi", komponente);
            }
            else
            {
                guidString = (string)hfState["guidString"];
                idPerdoruesi = (int)hfState["idPerdoruesi"];
                idGjuha = (int)hfState["idGjuha"];
                idNdermarrje = (int)hfState["idNdermarrje"];
                idViti = (int)hfState["idViti"];
                idNdermarrjeVit = (int)hfState["idNdermarrjeVit"];
                postStringTvsh = (string)hfState.Get(clsArtikulli.postStringTvsh);
                mbushGridArtikujshNgaSession(idPerdoruesi, idNdermarrje, postStringTvsh);
                konfiguroGride(idNdermarrje, cmbKonfigurimi.Text, 402, cbKosto.Checked, cbGjendje.Checked, cultinf, rm, null, idPerdoruesi, cbCmime.Checked, cbCmimeMeTvsh.Checked, postStringTvsh);
                konfiguroGrideNorma(idNdermarrje);
                konfiguroGrideCmimesh(idPerdoruesi, idNdermarrje);
            }
            AspxWebControlUtils.perkthePopUp(popFshi, rm.GetString("labelKujdes", cultinf), lblMsgbox, rm.GetString("msgnumRreshtashSelektuar", ci), ButtonCancel, rm.GetString("labelAnullo", cultinf));
            GridUtil.konfigGrideListeEMadhePaTheme(ASPxGridView_Artikull, "IdArtikulli");

            percaktoTemplateMenu((int)hfState["idGjuha"], idViti, idPerdoruesi, idNdermarrje, ASPxMenu1);
            clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idNdermarrje, "ASPxGridView_Artikull", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);

            ucFushatShtese.percaktoTemplateFushash();
            
            GridUtil.ToolTipButonaveMbiGride(ASPxGridView_Artikull, cultinf, rm);
            ASPxGridView_Artikull.PercaktoTitlePanelMeRefresh(this, MenuInfo, pnlMesazhi, hfState, IdPerdoruesi, IdNdermarrja, IdViti, IdGjuha, int.Parse(cmbKonfigurimi.Value.ToString()), komponente, rm, ci, false);
            lblGjendje.Text = rm.GetString("lblKaGjendje", cultinf);
            lblKosto.Text = rm.GetString("lblShfaqKoston", cultinf);
            emratELabel();
        }

        protected DataTable mbushKodeNivelCmimiDheRuajNeSesion(int idNdermarrje, int idPerdorues)
        {
            DataTable dtKodeNivCmimi = clsNivelCmimi.merrKodeNiveleCmimiSipasNdermarrjesDhePerdoruesit(idNdermarrje, idPerdorues);
            DbCore.mySessionObjects.ruajGrideNeSession("KodeNivelCmimi", Session, dtKodeNivCmimi);
            return dtKodeNivCmimi;
        }

        protected DataTable merrKodeNivelCmimiNgaSesioni(int idNdermarrje, int idPerdorues)
        {
            DataTable dtKodeNivCmimi = null;
            DbCore.mySessionObjects.merrGrideNgaSessioni("KodeNivelCmimi", Session, out dtKodeNivCmimi);
            if (dtKodeNivCmimi == null)
            {
                dtKodeNivCmimi = clsNivelCmimi.merrKodeNiveleCmimiSipasNdermarrjesDhePerdoruesit(idNdermarrje, idPerdorues);
                DbCore.mySessionObjects.ruajGrideNeSession("KodeNivelCmimi", Session, dtKodeNivCmimi);
            }
            return dtKodeNivCmimi;
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateTabeve(ResourceManager rm, CultureInfo cultinf)
        {
            ASPxPageControl1.TabPages[0].Text = rm.GetString("TePergjithshmeTab", cultinf);
            ASPxPageControl1.TabPages[1].Text = rm.GetString("labelAdministrimiInformacion", cultinf);
            ASPxPageControl1.TabPages[2].Text = rm.GetString("MenuItemRaportInventari", cultinf);
            ASPxPageControl1.TabPages[3].Text = rm.GetString("MenuItemRaportKontabiliteti", cultinf);
            ASPxPageControl1.TabPages[4].Text = rm.GetString("MenuItemRegjistrime", cultinf);
            ASPxPageControl1.TabPages[5].Text = rm.GetString("buxhetiTab", cultinf);
            ASPxPageControl1.TabPages[6].Text = rm.GetString("fushatShteseTab", cultinf);
            ASPxPageControl1.TabPages[7].Text = rm.GetString("labelRaportiCmimet", cultinf);
            ASPxPageControl1.TabPages[8].Text = rm.GetString("MenuItemAmortizimi", cultinf);
            popupHelp.HeaderText = rm.GetString("headerTextNdihma", cultinf);
            popupUniversal.HeaderText = rm.GetString("headerPopUpText", cultinf);
            konfigurimi_Label.Text = rm.GetString("lblModeli", cultinf);
            btneAutomjeti.Text = rm.GetString("btnShtoAutomjet", cultinf);
        }

        public void emratELabel()
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            lblLloji.Text = rm.GetString("lblLloji", cultinf);
            lblPershkrimiAng.Text = rm.GetString("lblPershkrimiDy", cultinf);
            
        }
        protected void percaktoFormatNumriCmimet(int idNdermarrje)
        {
            if (bool.Parse(hfTeDrejtaCmimi["Amb"].ToString()))
            {
                DbCore.DbShare.clsKonfigurimAmbjenti konfig = new DbCore.DbShare.clsKonfigurimAmbjenti();
                konfig.mbushKonfigAmbjSipasKod("CSH", idNdermarrje);
                formatNrPerKonfigCmimi.mbushFormatNrKonfigSipasIdKonfigAmbjente(konfig.IdKonfigAmbjente);
                if (formatNrPerKonfigCmimi.KonfigTrupi.Count == 0)
                {
                    DbCore.DbShare.clsFormatKonfigTrup trup = new DbCore.DbShare.clsFormatKonfigTrup(DbCore.DbShare.clsFormatKonfigTrup.defaultFormatSasia, DbCore.DbShare.clsFormatKonfigTrup.defaultFormatCmimi, DbCore.DbShare.clsFormatKonfigTrup.defaultFormatVlefta, DbCore.DbShare.clsFormatKonfigTrup.defaultFormatZbritja, DbCore.DbShare.clsFormatKonfigTrup.defaultFormatStringSasia, DbCore.DbShare.clsFormatKonfigTrup.defaultFormatStringCmimi, DbCore.DbShare.clsFormatKonfigTrup.defaultFormatStringVlefta, DbCore.DbShare.clsFormatKonfigTrup.defaultFormatStringZbritja);
                    formatNrPerKonfigCmimi.KonfigTrupi.Add(trup);
                }
            }
            else
            {
                ASPxPageControl1.TabPages[7].ClientVisible = false;
                DbCore.DbShare.clsFormatKonfigTrup trup = new DbCore.DbShare.clsFormatKonfigTrup(DbCore.DbShare.clsFormatKonfigTrup.defaultFormatSasia, DbCore.DbShare.clsFormatKonfigTrup.defaultFormatCmimi, DbCore.DbShare.clsFormatKonfigTrup.defaultFormatVlefta, DbCore.DbShare.clsFormatKonfigTrup.defaultFormatZbritja, DbCore.DbShare.clsFormatKonfigTrup.defaultFormatStringSasia, DbCore.DbShare.clsFormatKonfigTrup.defaultFormatStringCmimi, DbCore.DbShare.clsFormatKonfigTrup.defaultFormatStringVlefta, DbCore.DbShare.clsFormatKonfigTrup.defaultFormatStringZbritja);
                formatNrPerKonfigCmimi.KonfigTrupi.Add(trup);
            }
            System.Web.Script.Serialization.JavaScriptSerializer serializusi = new System.Web.Script.Serialization.JavaScriptSerializer();
            hfState.Set("formatMonedheCmimi", serializusi.Serialize(formatNrPerKonfigCmimi));
        }

        protected void btnXlsxExport_Click(object sender, EventArgs e)
        {
            try
            {
                gridExport.WriteXlsxToResponse("Artikujt", true);
            }
            catch (Exception)
            {
            }
        }
        protected void btnPdfExport_Click(object sender, EventArgs e)
        {
            try
            {
                gridExport.WritePdfToResponse("Artikujt", true);
            }
            catch (Exception)
            {
            }
        }

        private void merrColKushtet(int idKonfigurim)
        {
            
            Dictionary<string, clsAlternativaKushti> kushtet = colAlternativatKushti.MerrAlternativaKushtiSipasIdKonfigurimi(idKonfigurim);
            
            hfKushtet.Set("FPJM", kushtet.ContainsKey("FPJM") ? kushtet["FPJM"].Alternativa : "");
            hfKushtet.Set("CB", kushtet.ContainsKey("CB") ? kushtet["CB"].Alternativa : "Jo");


            clsKusht kushtiCFDP = new clsKusht(idKonfigurim, "CFDP");
            int idFormule = int.Parse(kushtiCFDP.Vlera.ToString());
            if (idFormule != 0)
            {
                clsFormula formula = new clsFormula(idFormule);
                hfKushtet.Set("formula", formula.PershkrimFormula);
            }
            else hfKushtet.Set("formula", "");
        }

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="idNderVit"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        private void percaktoTemplateMenu(int idGjuha, int idViti, int idPerdorues, int idNdermarrje, ASPxMenu aSPxMenu1)
        {
            clsToolbarConfig.percaktoTemplateMenu(idGjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1, DbCore.clsFunksione.GetKomponente(Page.Request), this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, hfShtimModifikim.Value == "modifikim" ? false : true, false, false, (bool)hfState["Meme"]);
        }

        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu((int)hfState["idGjuha"], (int)hfState["idViti"], (int)hfState["idPerdoruesi"], (int)hfState["idNdermarrje"], ASPxMenu1);
        }
        
        protected void SendItemsToPubSub(object sender, EventArgs e)
        {
            
            string[] columnNames = new string[1];
            columnNames[0] = "IdArtikulli";
            List<object> items = ASPxGridView_Artikull.GetSelectedFieldValues(columnNames);
            if (items.Count > 0)
            {
                //clsMenuInfo.ShtoMesazhInformues(MenuInfo, "Artikujt po sinkronizohen!", pnlMesazhi);
                List<int> artikujt = new List<int>();
                List<object> objForPubSub = new List<object>();
                PubSub PubSub = new PubSub("alphaweb", "alpha_items", "AlphaToFatura_Items");
                foreach (object item in items)
                {
                    artikujt.Add(int.Parse(item.ToString()));
                }
                colArtikujt itemList = new colArtikujt(artikujt);
                foreach (clsArtikulli art in itemList)
                {
                    PubSub.PublishPubSub(3, 1, 2, 1, art.krijoObjektPerPubSub());
                    //objForPubSub.Add(art.krijoObjektPerPubSub());
                }
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "Artikujt u derguan me sukses!", pnlMesazhi);
            }
            else clsMenuInfo.ShtoMesazhInformues(MenuInfo, "Ju lutem zgjidhni te pakten 1 rresht!", pnlMesazhi);
        }

        /// <summary>
        /// perdoret per te shtuar kolonen e selektimit tek grida dhe per te vendosur disa karakteristika te grides
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void ASPxGridView_Artikull_DataBound(object sender, EventArgs e)
        {// shton colonen # per selektim dhe disa karakteristika te grides
            if (ASPxGridView_Artikull.Columns.Count == 0 || ASPxGridView_Artikull.Columns["#"] != null)
                return;

            //behet nepermjet kodit afishimi i checkboxit qe do perdoret per 
            //perzgjidh
            DevExpress.Web.GridViewCommandColumn check = new DevExpress.Web.GridViewCommandColumn("#");
            check.ShowSelectCheckbox = true;
            check.Width = Unit.Percentage(2);
            //check.SetColVisibleIndex(0);
            //behet per te afishuar rreshtin qe do sherbej per filtrim
            ASPxGridView_Artikull.Settings.ShowFilterRow = true;
            ASPxGridView_Artikull.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
            ASPxGridView_Artikull.Settings.ShowFilterRowMenu = true;
            ASPxGridView_Artikull.Columns.Add(check);

            ASPxGridView_Artikull.KeyFieldName = "IdArtikulli";
            ASPxGridView_Artikull.SettingsBehavior.AllowSelectByRowClick = true;
            ASPxGridView_Artikull.SettingsBehavior.AllowFocusedRow = true;
        }

        /// <summary>
        /// perdoret per te konfiguruar griden. Per te dhenat mbi kolonat qe do te shfaqen nga databaza si dhe percakton disa karakteristika te grides
        /// </summary>
        /// :  <see cref="GridUtil.percaktoVisibleColumns"/>
        /// :  <see cref="DbCore.clsFunksione.konfiguroGrideListeMadhe(grida,celesigrides)"/>
        /// <param name="idNdermarrje"></param>
        private void konfiguroGride(int idNdermarrje, string kodKonfigurimi, int idKomponente, bool kosto, bool gjendje, CultureInfo cultinf, ResourceManager rm, DataTable kodeNivelCmimi, int idPerdoruesi, bool cmime, bool cmimeMeTvsh, string postStringTvsh)
        {
            //Konfigurimi i grides            
            this.ASPxGridView_Artikull.Columns["#"].VisibleIndex = 0;
            //  GridViewDataTextColumn col4 = ASPxGridView_Artikull.Columns["Garancia"] as GridViewDataTextColumn;
            shtokolona(idNdermarrje);
            //   col4.PropertiesEdit.DisplayFormatString = "0.#";

            if (kosto)
            {
                ASPxGridView_Artikull.Columns["Kosto"].Visible = true;
                ASPxGridView_Artikull.Columns["Kosto"].Width = 50;
                GridViewDataTextColumn colKosto = ASPxGridView_Artikull.Columns["Kosto"] as GridViewDataTextColumn;
                colKosto.PropertiesEdit.DisplayFormatString = "0.00";
                ASPxGridView_Artikull.Columns["Kosto"].Caption = rm.GetString("koloneKosto", cultinf);
            }
            else
            {
                ASPxGridView_Artikull.Columns["Kosto"].Visible = false;
                ASPxGridView_Artikull.Columns["Kosto"].Width = 0;
            }
            if (gjendje)
            {
                ASPxGridView_Artikull.Columns["Gjendje"].Visible = true;
                ASPxGridView_Artikull.Columns["Gjendje"].Width = 50;
                GridViewDataTextColumn colKosto = ASPxGridView_Artikull.Columns["Gjendje"] as GridViewDataTextColumn;
                colKosto.PropertiesEdit.DisplayFormatString = "0.00";
                ASPxGridView_Artikull.Columns["Gjendje"].Caption = rm.GetString("koloneGjendja", cultinf);
            }
            else
            {
                ASPxGridView_Artikull.Columns["Gjendje"].Visible = false;
                ASPxGridView_Artikull.Columns["Gjendje"].Width = 0;
            }

            if (kodeNivelCmimi == null)
                kodeNivelCmimi = merrKodeNivelCmimiNgaSesioni(idNdermarrje, idPerdoruesi);

            foreach (DataRow dr in kodeNivelCmimi.Rows)
            {
                ASPxGridView_Artikull.Columns[dr.ItemArray[0].ToString()].Visible = cmime;
                ASPxGridView_Artikull.Columns[dr.ItemArray[0].ToString()+ postStringTvsh].Visible = cmimeMeTvsh;
            }
            
            kodeNivelCmimi.Dispose();
        }

        private void shtokolona(int idNdermarrje)
        {
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            KonfigurimComboGride.shtoLlojiArtikullit(ASPxGridView_Artikull, rm, ci);
            KonfigurimComboGride.shto_AplikimDhurate(ASPxGridView_Artikull, rm, ci);
            KonfigurimComboGride.shto_NjesiArtikulli(ASPxGridView_Artikull, idNdermarrje, Session, komponente, guidString, "Njesi1Artikulli"); 
            KonfigurimComboGride.shto_NjesiArtikulli(ASPxGridView_Artikull, idNdermarrje, Session, komponente, guidString, "Njesi2Artikulli");
            KonfigurimComboGride.shto_KlasaArtikulli(ASPxGridView_Artikull, Session, komponente, guidString, "Klasa");
            KonfigurimComboGride.shto_MetodeKostoje(ASPxGridView_Artikull, Session, komponente, guidString, "MetodeKostojeArtikulli");
            KonfigurimComboGride.shto_SkemeKontabilitetiArtikulli(ASPxGridView_Artikull, idNdermarrje, Session, komponente, guidString, "IdSkemaKontabilitetiArtikulli");
            KonfigurimComboGride.shto_LlojGarancia(ASPxGridView_Artikull, Session, komponente, guidString);
            KonfigurimComboGride.shtoFormatSeriali(ASPxGridView_Artikull, idNdermarrje, Session, komponente, guidString, "IdKategoriSeriali");

            if (Request.QueryString["llojiart"] == "aqt")
            {
                KonfigurimComboGride.shto_KodifikimArtikulli(ASPxGridView_Artikull, idNdermarrje, true, Session, komponente, guidString);
            }
            else
                KonfigurimComboGride.shto_KodifikimArtikulli(ASPxGridView_Artikull, idNdermarrje, false, Session, komponente, guidString);
        }



        //private void shto_Kodifikim(int idNdermarrje)
        //{
        //    GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
        //    if (typeof(GridViewDataComboBoxColumn) != ASPxGridView_Artikull.Columns["Kodifikimi1Artikulli"].GetType())
        //    {
        //        ASPxGridView_Artikull.Columns.Remove(ASPxGridView_Artikull.Columns["Kodifikimi1Artikulli"]);
        //        ASPxGridView_Artikull.Columns.Add(colnew);
        //        DbCore.DbInventari.colKodifikimeArtikulli kodifikimet = new DbCore.DbInventari.colKodifikimeArtikulli();
        //        DbCore.DbInventari.clsKodifikimArtikulli kod = new DbCore.DbInventari.clsKodifikimArtikulli(0, "", "", 0, 0, 0, 0, 0, 0);
        //        kodifikimet.Add(kod);
        //        bool llojartikulli;
        //        if (Request.QueryString["llojiart"] == "aqt")
        //            llojartikulli = true;
        //        else llojartikulli = false;
        //        kodifikimet.mbushGjitheKodifikimetArtikulliSipasNdermarrjesJoPrind(idNdermarrje, llojartikulli);
        //        colnew.PropertiesComboBox.DataSource = kodifikimet;
        //        colnew.PropertiesComboBox.TextField = "KodKodifikimi";
        //        colnew.PropertiesComboBox.ValueField = "IdKodifikimi";
        //        colnew.FieldName = "Kodifikimi1Artikulli";
        //        colnew.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        //        DbCore.mySessionObjects.ruajDsComboGrideNeSession(Session, kodifikimet, "kodifikimet");
        //    }
        //    else
        //    {
        //        colnew = (GridViewDataComboBoxColumn)ASPxGridView_Artikull.Columns["Kodifikimi1Artikulli"];
        //        if (colnew.PropertiesComboBox.Items.Count == 0)
        //        {
        //            colnew.PropertiesComboBox.DataSource = DbCore.mySessionObjects.merrDsComboGrideNeSession(Session, "kodifikimet");
        //        }
        //    }
        //}

        //private void shto_Njesi1(int idNdermarrje)
        //{
        //    ASPxGridView_Artikull.Columns.Remove(ASPxGridView_Artikull.Columns["Njesi1Artikulli"]);
        //    GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
        //    DbCore.DbInventari.colNjesiteArtikulli col = new DbCore.DbInventari.colNjesiteArtikulli(idNdermarrje);
        //    //col.Insert(0, new DbCore.DbInventari.clsNjesiArtikulli(0, "", "", 0, 0, 0));
        //    //DbCore.DbInventari.colNjesiteArtikulli col = new DbCore.DbInventari.colNjesiteArtikulli();
        //    //col = dbInventari.merrGjitheNjesiteArtikulliSipasNdermarrjes(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //    //colnew.PropertiesComboBox.DataSource = col;
        //    //colnew.PropertiesComboBox.TextField = "KodNjesia";
        //    //colnew.PropertiesComboBox.ValueField = "IdNjesia";
        //    colnew.PropertiesComboBox.Items.Add("", "0");
        //    foreach (DbCore.DbInventari.clsNjesiArtikulli njesi in col)
        //    {
        //        colnew.PropertiesComboBox.Items.Add(njesi.KodNjesia, njesi.IdNjesia.ToString());
        //    }
        //    colnew.FieldName = "Njesi1Artikulli";
        //    colnew.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        //    ASPxGridView_Artikull.Columns.Add(colnew);
        //}





        //private void shto_Njesi2(int idNdermarrje)
        //{
        //    ASPxGridView_Artikull.Columns.Remove(ASPxGridView_Artikull.Columns["Njesi2Artikulli"]);

        //    GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
        //    DbCore.DbInventari.colNjesiteArtikulli col = new DbCore.DbInventari.colNjesiteArtikulli(idNdermarrje);
        //    col.Insert(0, new DbCore.DbInventari.clsNjesiArtikulli(0, "", "", 0, 0, 0));
        //    //DbCore.DbInventari.colNjesiteArtikulli col = new DbCore.DbInventari.colNjesiteArtikulli();
        //    //col = dbInventari.merrGjitheNjesiteArtikulliSipasNdermarrjes(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //    //colnew.PropertiesComboBox.DataSource = col;
        //    //colnew.PropertiesComboBox.TextField = "KodNjesia";
        //    //colnew.PropertiesComboBox.ValueField = "IdNjesia";
        //    colnew.PropertiesComboBox.Items.Add("", "0");
        //    foreach (DbCore.DbInventari.clsNjesiArtikulli njesi in col)
        //    {
        //        colnew.PropertiesComboBox.Items.Add(njesi.KodNjesia, njesi.IdNjesia.ToString());
        //    }
        //    colnew.FieldName = "Njesi2Artikulli";
        //    colnew.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        //    ASPxGridView_Artikull.Columns.Add(colnew);
        //}

        /// <summary>
        /// perdoret ne rastet kur grida ben callback per ta rimbushur griden
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void ASPxGridView_Artikull_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            if (e.CallbackName == "COLUMNMOVE" && ASPxGridView_Artikull.AllColumns[int.Parse(e.Args[0])].Width.Value == 0)
                ASPxGridView_Artikull.AllColumns[int.Parse(e.Args[0])].Width = Unit.Percentage(3);

            if (e.CallbackName == "APPLYFILTER" && e.Args[0] == "")
            {
                DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
                ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
                cmbFiltra.Text = "";

                if (Request.QueryString["llojiart"] == "afatshkurter")
                {
                    ASPxGridView_Artikull.FilterExpression = "[LlojiArt]=false";
                }
                else
                {
                    ASPxGridView_Artikull.FilterExpression = "[LlojiArt]=true";
                }

            }
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            GridUtil.ToolTipButonaveMbiGride(ASPxGridView_Artikull, cultinf, rm);
            // mbushListeArtikujsh();
        }

        /// <summary>
        /// perdoret per te vendosur filtrat tek headeri i grides
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxGridView_Artikull_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            string TeGjithe = rm.GetString("GridHeaderFilterFillItemTeGjithe", ci);
            string nga = rm.GetString("GridHeaderFilterFillItemNga", ci);
            if (e.Column.FieldName == "KodArtikulli" || e.Column.FieldName == "PershkrimArtikulli" || e.Column.FieldName == "PershkrimiAngArtikulli")
            {
                e.Values.Clear();
                //e.AddShowAll();
                e.AddValue(TeGjithe, string.Empty, "true");
                e.AddValue(nga + " A-D ", string.Empty, e.Column.FieldName + ">'A     ' and " + e.Column.FieldName + " <'DDDDDDD'");
                e.AddValue(nga + " D-G ", string.Empty, e.Column.FieldName + ">'D     ' and " + e.Column.FieldName + "<'GGGGGGG'");
                e.AddValue(nga + " H-K ", string.Empty, e.Column.FieldName + ">'H     ' and " + e.Column.FieldName + "<'KKKKKKK'");
                e.AddValue(nga + " L-O ", string.Empty, e.Column.FieldName + ">'L     ' and " + e.Column.FieldName + "  <'OOOOOOO'");
                e.AddValue(nga + " P-S ", string.Empty, e.Column.FieldName + ">'P     ' and " + e.Column.FieldName + "<'SSSSSSS'");
                e.AddValue(nga + " T-W ", string.Empty, e.Column.FieldName + ">'T     ' and " + e.Column.FieldName + "<'WWWWWWW'");
                e.AddValue(nga + " X-Z ", string.Empty, e.Column.FieldName + ">'X     ' and " + e.Column.FieldName + "<'ZZZZZZZ'");
            }
            else
            {
                e.Values.Clear();
                e.AddValue(TeGjithe, string.Empty, "true");
            }

        }

        /// <summary>
        /// fshin filtrin e zgjedhur dhe aplikuar mbi gride, fshirje nga DB ku ai eshte ruajtur, dhe jo pastrimi i grides nga aplikimi i filtrit
        /// per kete do sherbeje Clear Filter ne fund te grides
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka((int)hfState["idGjuha"], "ASPxGridView_Artikull", komponente, (int)hfState["idNdermarrje"], int.Parse(cmbKonfigurimi.Value.ToString()));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, (int)hfState["idNdermarrje"], koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = (int)hfState["idPerdoruesi"];
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();
                cmbFiltra.Text = "";
                clsToolbarConfig.mbushComboBoxFiltra((int)hfState["idGjuha"], (int)hfState["idNdermarrje"], "ASPxGridView_Artikull", int.Parse(cmbKonfigurimi.Value.ToString()),  komponente);
                percaktoTemplateMenu((int)hfState["idGjuha"], (int)hfState["idViti"], (int)hfState["idPerdoruesi"], (int)hfState["idNdermarrje"], ASPxMenu1);
                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                hfStatusi.Value = "true";
                this.ASPxGridView_Artikull.FilterExpression = String.Empty;

            }
        }

        /// <summary>
        /// sherben per te ruajtur filtrin e zgjedhur
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            DbCore.DbAdmin.clsFiltraGrida filtri = new DbCore.DbAdmin.clsFiltraGrida();
            filtri.FiltraKodi = cmbFiltra.Text;
            filtri.FiltraShenime = cmbFiltra.Text;
            filtri.FiltraUniversal = false;// Universal_ASPxCheckBox.Checked;
            int idNdermarrje = (int)hfState["idNdermarrje"];
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka((int)hfState["idGjuha"], "ASPxGridView_Artikull", komponente, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = ASPxGridView_Artikull.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("KodArtikulli", ASPxGridView_Artikull);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = ASPxGridView_Artikull.GetSortedColumns();
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
            //    filtri.KoloneRenditje = "KodArtikulli";
            //    filtri.DrejtimRenditje = true;
            //}
            filtri.IdPerdoruesi = (int)hfState["idPerdoruesi"];
            filtri.IdNdermarje = idNdermarrje;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();

            filtri.IdStatusDok = 1;
            mesazh = filtri.ruaj();
            clsToolbarConfig.mbushComboBoxFiltra((int)hfState["idGjuha"], idNdermarrje, "ASPxGridView_Artikull", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
            percaktoTemplateMenu((int)hfState["idGjuha"], (int)hfState["idViti"], (int)hfState["idPerdoruesi"], idNdermarrje, ASPxMenu1);
            if (mesazh.Status == true)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            hfStatusi.Value = "true";
            cmbFiltra.Text = "";
        }

        /// <summary>
        /// perdoret per te fshire rreshtat e zgjedhur te artikujve nqs perdoruesi konfirmon fshirjen
        /// </summary>
        ///  :  <see cref="DbCore.DbInventari.clsDatabaseInventari.ktheArtikull(id)"/> 
        ///  :  <see cref="DbCore.DbInventari.clsDatabaseInventari.kaArtikuj(id)"/> 
        ///  :  <see cref="DbCore.DbInventari.clsArtikulli.fshiArtikull()"/> 
        /// <param name="sender"> derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ButtonOk_Click2(object sender, EventArgs e)
        {//fshirja e artikullit
            //clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "", pnlMesazhi);
            List<object> rreshtat;
            if (ASPxPageControl1.ActiveTabIndex == 0)//nqs kemi zgjedhur vetem nje kartele per te pare fshijme vetem ate
                rreshtat = ASPxGridView_Artikull.GetSelectedFieldValues("IdArtikulli");
            else
            {
                rreshtat = new List<object>();
                rreshtat.Add(hfId.Value);
            }
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (rreshtat.Count == 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgShtoArtikullZgjidhNjeArtikull", ci), pnlMesazhi);
                return;
            }
            List<string> artikujTeFshire = new List<string>(), artikujTePaFshire = new List<string>();
            int idNdermarrje = (int)hfState["idNdermarrje"];
            int idPerdorues = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            DbCore.DbRegjistrim.clsDatabaseRegjistrim dbRegjistrim = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
            DbCore.DbInventari.clsArtikulli artikull = new DbCore.DbInventari.clsArtikulli();
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            string postStringTvsh = (string)hfState.Get(clsArtikulli.postStringTvsh);
            for (int i = 0; i < rreshtat.Count; i++)
            {
                artikull.mbushArtikull(Convert.ToInt32(rreshtat[i]));
                //DbCore.DbInventari.colArtikujt colArtikujt = dbInventari.ktheArtikull(id);                
                konf.mbushKonfigAmbjSipasId(artikull.IdKonfig, DbCore.mySessionObjects.ktheGjuhe(Session));
                //konf.IdKonfigAmbjente = artikull.IdKonfig;
                //konf = new DbCore.DbShare.clsDatabaseShare().merrKonfigAmbjSipasId(konf)[0];                
                bool lidhur = dbRegjistrim.eshteDokumentiILidhurCelje(artikull.IdArtikulli.ToString(), konf.IdNivel.ToString());
                if (lidhur)
                {
                    artikujTePaFshire.Add(artikull.KodArtikulli);
                    continue;
                }
                if ((DbCore.mySessionObjects.merrEshteMemeSesioni(Session) && DbCore.DbInventari.clsArtikulli.eshteTransferuarTekBijArtikull(artikull.KodArtikulli, artikull.IdNdermarje)))
                {
                    artikujTePaFshire.Add(artikull.KodArtikulli);
                    continue;
                }
                artikull.IdPerdoruesi = idPerdorues;
                DbCore.clsMesazh mesazhi = artikull.fshi();
                if (artikull.IdArtikulli == 0)
                    continue;
                if (mesazhi.Status)
                {
                    #region Heq artikujt nga grida

                    // ASPxGridView_Artikull.DataSource = dt;
                    hiqArtikullNgaGrida(idPerdorues, idNdermarrje, artikull.IdArtikulli, rm, ci, postStringTvsh);
                    #endregion
                    artikujTeFshire.Add(artikull.KodArtikulli);

                    ASPxPageControl1.ActiveTabIndex = 0;
                    hfStatusi.Value = "true";
                }
                //mbushListeArtikujsh();                 

            }
            dbRegjistrim.Dispose();
            string mesazhInfoGabim = "", mesazhInfoSukses = "";
            if (artikujTePaFshire.Count == 1)
                mesazhInfoGabim = String.Format("{0}{1}{2}", rm.GetString("msgShtoArtikullPrefixNjejes", ci), String.Join(";", artikujTePaFshire), rm.GetString("msgShtoArtikullSuffixNjejesGabimi", ci));
            else
                if (artikujTePaFshire.Count > 1)
                    mesazhInfoGabim = String.Format("{0}{1}{2}", rm.GetString("msgShtoArtikullPrefixShumes", ci), String.Join(";", artikujTePaFshire), rm.GetString("msgShtoArtikullSuffixShumesGabimi", ci));
            if (artikujTeFshire.Count == 1)
                mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("msgShtoArtikullPrefixNjejes", ci), String.Join(";", artikujTeFshire), rm.GetString("suffixMesazhNjejesSuksesi", ci));
            else
                if (artikujTeFshire.Count > 1)
                    mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("msgShtoArtikullPrefixShumes", ci), String.Join(";", artikujTeFshire), rm.GetString("msgClejeArkaBankaSuffixShumesSuksesi", ci));
            if (mesazhInfoGabim != "")
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhInfoGabim, pnlMesazhi);
            if (mesazhInfoSukses != "")
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazhInfoSukses, pnlMesazhi);
            pnlMesazhi.Update();
        }

        private void hiqArtikullNgaGrida(int idPerdorues, int idNdermarrje, int idArtikulli, System.Resources.ResourceManager rm, System.Globalization.CultureInfo ci, string postStringTvsh)
        {
            if (ASPxGridView_Artikull.DataSource != null)
            {
                DataTable dt = (DataTable)ASPxGridView_Artikull.DataSource;
                DataRow[] drs = dt.Select("IdArtikulli = " + idArtikulli);
                if (drs.Length > 1)
                    throw new DbCore.MyException(rm.GetString("msgShtoArtikulGabimNdodhen2ArtikujNeGride", ci));
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                dt.Rows.Remove(dr);
                ASPxGridView_Artikull.DataBind();
            }
            else
            {
                mbushGridArtikujshNgaDB(idPerdorues, idNdermarrje, cbKosto.Checked, cbGjendje.Checked, cbCmime.Checked, cbCmimeMeTvsh.Checked, postStringTvsh, false);
            }
        }

        private void shtoArtikullNeGrid(int idNdermarrje, int idPerdorues, int idArtikulli, System.Resources.ResourceManager rm, System.Globalization.CultureInfo ci, string postStringTvsh)
        {
            if (ASPxGridView_Artikull.DataSource != null)
            {
                DataTable dt = (DataTable)ASPxGridView_Artikull.DataSource;
                DataRow[] drs = dt.Select("IdArtikulli = " + idArtikulli);
                if (drs.Length > 0)
                    throw new Exception(rm.GetString("msgShtoArtikulGabimArtikulliEkzistonNeGride", ci));
                DataRow newArtDr = DbCore.DbInventari.colArtikujt.merrSipasArtikujNdermarrjesAndAutorizimeDR(idNdermarrje, idPerdorues, idArtikulli, cbKosto.Checked, cbGjendje.Checked);
                dt.ImportRow(newArtDr);
            }
            else
            {
                
                mbushGridArtikujshNgaDB(idPerdorues, idNdermarrje, cbKosto.Checked, cbGjendje.Checked, cbCmime.Checked, cbCmimeMeTvsh.Checked, postStringTvsh, false);
            }           
            konfiguroGride(idNdermarrje, cmbKonfigurimi.Text, 402, cbKosto.Checked, cbGjendje.Checked, ci, rm, null, idPerdorues, cbCmime.Checked, cbCmimeMeTvsh.Checked, postStringTvsh);
        }

        private void modifikoArtikullNeGrid(int idNdermarrje, int idPerdorues, int idArtikulli, System.Resources.ResourceManager rm, System.Globalization.CultureInfo ci, string postStringTvsh)
        {
            if (ASPxGridView_Artikull.DataSource != null)
            {
                DataTable dt = (DataTable)ASPxGridView_Artikull.DataSource;
                DataRow[] drs = dt.Select("IdArtikulli = " + idArtikulli);
                if (drs.Length > 1)
                    throw new DbCore.MyException(rm.GetString("msgShtoArtikulGabimNdodhen2ArtikujNeGride", ci));
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                DataRow newArtDr = DbCore.DbInventari.colArtikujt.merrSipasArtikujNdermarrjesAndAutorizimeDR(idNdermarrje, idPerdorues, idArtikulli, cbKosto.Checked, cbGjendje.Checked );
                if (newArtDr != null)
                {
                    object[] arr = newArtDr.ItemArray;
                    dr.ItemArray = arr;
                }
            }
            else
            {
                mbushGridArtikujshNgaDB(idPerdorues, idNdermarrje, cbKosto.Checked, cbGjendje.Checked, cbCmime.Checked, cbCmimeMeTvsh.Checked, postStringTvsh, false);
            }
        }

        /// <summary>
        /// perdoret per te trajtuar ngjarjet e butonave te menuse. Shkon ne faqen e modifikimit te artikujve kur perdoruesi klikon butonin modifiko
        /// ose ne faqen e shtimit te artikujve kur perdoruesi klikon butonin shto
        /// </summary>
        /// <param name="source"> derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {//veprimet e menuse
            if (e.Item.Name == "Ruaj")
            {
                Page.Validate("entries");
                ruajArtikull();
            }
        }

        protected void ASPxGridView_Artikull_CustomJSProperties(object sender, DevExpress.Web.ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = ASPxGridView_Artikull.PageIndex;
            e.Properties["cpPageRow"] = ASPxGridView_Artikull.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = ASPxGridView_Artikull.VisibleRowCount;
        }

        private void konfiguroVleraFillestare(int idPerdoruesi, int idNdermarrje, System.Resources.ResourceManager rm, System.Globalization.CultureInfo ci, int idGjuha, string postStringTvsh)
        { //mbush komboboxet dhe gridat e faqes
            if (Request.QueryString["llojiart"] == "afatshkurter")
            {
                mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 13, "ART", rm, ci, idGjuha);
                ConfigureAspxComboBox.shtoKolonaSkemaArtikulli(btneSkema, false, idNdermarrje);
            }
            else
                if (Request.QueryString["llojiart"] == "aqt")
            {
                mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 13, "AQT", rm, ci, idGjuha);
                ConfigureAspxComboBox.shtoKolonaSkemaArtikulli(btneSkema, true, idNdermarrje);
            }

            cmbKonfigurimi.SelectedIndex = 0;
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()), idGjuha);
            hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;

            merrColKushtet(Convert.ToInt32(cmbKonfigurimi.Value.ToString()));
            mbushListeCmimesh(idPerdoruesi, idNdermarrje);

            ConfigureAspxComboBox.mbushComboKategoriDetajimesh(cmbKategoriDetajimi);
            ConfigureAspxComboBox.mbushComboKlasa(cmbKlasa, true);
            ConfigureAspxComboBox.mbushComboKategoriDetajimesh(cmbKategoriDetajimi2);
            ConfigureAspxComboBox.ShtoKolonaPerKf(txtFurnitori);
            ConfigureAspxComboBox.ShtoKolonaPerLlogarine(btneLlogTretet);
            ConfigureAspxComboBox.ShtoKolonaPerLlogarine(btnLlogShpe);
            ConfigureAspxComboBox.ShtoKolonaPerLlogarine(btneLlogBle);
            ConfigureAspxComboBox.ShtoKolonaPerLlogarine(btneLlogInv);
            ConfigureAspxComboBox.ShtoKolonaPerLlogarine(btneLlogShit);
            ConfigureAspxComboBox.ShtoKolonaPerLlogarine(cmbLlogAmortizimi);
            ConfigureAspxComboBox.ShtoKolonaPerLlogarine(btnLLogariKomisioni);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe( txtFurnitori, btnMagazina, cmbObjektiva, btneKodifikimi1, btneKodifikimi2, btneKodifikimi3, cmbFormatSeriali);
            ConfigureAspxComboBox.percaktoTemplateCombo(false,false,cmbNivelTvsh);
          
            ConfigureAspxComboBox.percaktoTemplateComboMeEnableCallback(cmbLidhMeNdermRap, btneSkema, cmbLlogAmortizimi, btnLlogShpe, btnLlogPakesim,  btneLlogBle, btneLlogInv, btneLlogShit, btneLlogTretet, btneLlogRez, btneLlogPakesimRez, btnLLogariKomisioni);
        
            AspxWebControlUtils.vendosDateEditMask( dteDateAkt, dteDateAk2);
            ConfigureAspxComboBox.mbushComboArtikulli(idPerdoruesi, DbCore.mySessionObjects.ktheNdermRaportuese(Session), postStringTvsh, cmbLidhMeNdermRap);
            ConfigureAspxComboBox.mbushComboMetodeKostoje(cmbMetode, true);
            ConfigureAspxComboBox.mbushComboAplikim(cmbAplikim);
            ConfigureAspxComboBox.mbushComboKMSH(cmbKMSH);
            ConfigureAspxComboBox.mbushComboZevendesim(cmbZevendesim);
            ConfigureAspxComboBox.mbushComboNjesi(idNdermarrje, cmbNjesia1);
            ConfigureAspxComboBox.mbushComboNjesi(idNdermarrje, cmbNjesia2);
            ConfigureAspxComboBox.mbushComboFormateSeriali(idNdermarrje, cmbFormatSeriali);
            ConfigureAspxComboBox.KonfiguroComboBoxTaksat(idPerdoruesi, idNdermarrje, cmbNivelTvsh, DbCore.DbRegjistrim.LlojTakse.Nivel_Tvsh, false, false, true);
           // cmbNivelTvsh.DisplayFormatString = "0.00";
            ConfigureAspxComboBox.mbushComboLlojArt(cmbLloji);
            ConfigureAspxComboBox.mbushComboGarancite(cmbGarancia);
            ConfigureAspxComboBox.mbushComboKodifikim(idNdermarrje, btneKodifikimi1, 1, (Request.QueryString["llojiart"] == "aqt"), true);
            ConfigureAspxComboBox.mbushComboKodifikim(idNdermarrje, btneKodifikimi2, 2, (Request.QueryString["llojiart"] == "aqt"), true);
            ConfigureAspxComboBox.mbushComboKodifikim(idNdermarrje, btneKodifikimi3, 3, (Request.QueryString["llojiart"] == "aqt"), true);
            ConfigureAspxComboBox.mbushComboMagazinat(idNdermarrje, btnMagazina, idPerdoruesi, false, 0, true);

            ConfigureAspxComboBox.KonfiguroComboBoxObjektivaKosto(cmbObjektiva, IdNdermarrja);

            hfSkemaKlasa.Value = ConfigureAspxComboBox.mbushComboSkemaKontabilitetiArtikulli(idNdermarrje, btneSkema, Request.QueryString["llojiart"],"");
            int idMonedheZgjedhur = DbCore.clsFunksione.ktheMonedhePerFormatNumri((int)hfState["idGjuha"], int.Parse(cmbKonfigurimi.Value.ToString()), idNdermarrje, 402, " ", -1, true);
            DbCore.DbShare.clsFormatiKonfig formatNrPerKonfig = new DbCore.DbShare.clsFormatiKonfig();
            formatNrPerKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(int.Parse(cmbKonfigurimi.Value.ToString()));
            DbCore.DbShare.clsFormatKonfigTrup formatMonedhe = clsFormatKonfigTrup.ktheFormatNumriMonedhe(formatNrPerKonfig, idMonedheZgjedhur);
            System.Web.Script.Serialization.JavaScriptSerializer serializusi = new System.Web.Script.Serialization.JavaScriptSerializer();
            hfFormatNumri.Set("formatMonedhe", serializusi.Serialize(formatMonedhe));


            DbCore.DbRegjistrim.colNjesiAdministrative colMagazinat = new DbCore.DbRegjistrim.colNjesiAdministrative();
            colMagazinat.mbushGjitheNjesiAdministrativeAktive(idNdermarrje, idPerdoruesi);
            serializusi.MaxJsonLength = 50000000;
            hfTmpColMag.Value = serializusi.Serialize(colMagazinat);
            hfState.Set("colAutorizime", Newtonsoft.Json.JsonConvert.SerializeObject(new colAutorizimetKoka(IdPerdoruesi)));
        }

        public void mbushComboKonfigurimeshSipasKategorise(int idPerdoruesi, int idNdermarrje, ASPxComboBox combo, int kat, string nivel, System.Resources.ResourceManager rm, System.Globalization.CultureInfo ci, int idGjuha)
        {//mbush griden e popupit me te dhena
            DbCore.DbShare.colKonfigurimAmbjenti col = new DbCore.DbShare.colKonfigurimAmbjenti();
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            if (kat != 0)
            {
                konf.IdKategori = kat;
                konf.IdNdermarje = idNdermarrje;
                int idNivel = DbCore.DbRegjistrim.clsNivelRegjistrimi.ktheIdNivelRegjistrimiSipasKodi(nivel, idNdermarrje);
                col.mbushKonfigAmbjSipasIdKategoriIdNivelMeLloj(konf.IdKategori, idNivel, idPerdoruesi);
            }
            else
            {
                col.mbushGjitheKonfigurimeAmbjentesh(idNdermarrje, idPerdoruesi, 1, idGjuha);
            }
            ListBoxColumn colprove = new ListBoxColumn();
            colprove.FieldName = "KodKonfigAmbjente";
            colprove.Caption = rm.GetString("cmbCmimeArtikulliCaptionKodi", ci);
            ListBoxColumn colemer = new ListBoxColumn();
            colemer.FieldName = "PershkrimKonfigAmbjente";
            colemer.Caption = rm.GetString("cmbCmimeArtikulliCaptionPershkrimi", ci);
            combo.TextFormatString = "{0}";
            combo.Columns.Add(colprove);
            combo.Columns.Add(colemer);
            combo.DataSource = col;
            combo.ValueField = "IdKonfigAmbjente";
            combo.IncrementalFilteringMode = DevExpress.Web.IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        private ArrayList mbushComboPrioriteti()
        {//mbush combon e prioriteteve 
            ArrayList prioriteti = new ArrayList();
            prioriteti.Add("1");
            prioriteti.Add("2");
            prioriteti.Add("3");
            prioriteti.Add("4");
            prioriteti.Add("5");
            prioriteti.Add("6");
            prioriteti.Add("7");
            prioriteti.Add("8");
            prioriteti.Add("9");
            prioriteti.Add("10");
            prioriteti.Add("11");
            prioriteti.Add("12");
            prioriteti.Add("13");
            prioriteti.Add("14");
            prioriteti.Add("15");
            prioriteti.Add("16");
            prioriteti.Add("17");
            prioriteti.Add("18");
            prioriteti.Add("19");
            prioriteti.Add("20");

            return prioriteti;
        }

        private void konfiguroGrideCmimesh(int idPerdoruesi, int idNdermarrje)
        {//konfigurohet grida
 
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            KonfigurimComboGride.ShtoMonedhe(gvCmimet, idNdermarrje, idPerdoruesi, Session, komponente, guidString);
            KonfigurimComboGride.shtoPrindSipasNivelCmimi(gvCmimet, idNdermarrje, Session, komponente, guidString, "IdNivelCmimi");
            KonfigurimComboGride.ShtoDetajimet(gvCmimet, idNdermarrje, idPerdoruesi, Session, komponente, guidString, "IdDetajim");
            GridUtil.konfiguroGrideRegjistrimEvogelPaTheme(gvCmimet, "IdCmimArtikulli", false);
            percaktoTemplateCmimi();
            gvCmimet.SettingsPager.Mode = GridViewPagerMode.ShowPager;
            gvCmimet.SettingsPager.PageSize = 15;
            gvCmimet.SettingsBehavior.AllowSort = false;
            
        }


    
        private void mbushGridArtikujshNgaSession(int idPerdorues, int idNdermarrje,string postStringTvsh)
        {
            DataTable tmpObject;
            bool sukses = DbCore.mySessionObjects.merrGrideNgaSessioni(komponente, Session, out tmpObject);
            if (!sukses)
                mbushGridArtikujshNgaDB(idPerdorues, idNdermarrje, cbKosto.Checked, cbGjendje.Checked, cbCmime.Checked, cbCmimeMeTvsh.Checked, postStringTvsh, false);
          
            else
            {
                //ASPxGridView_Artikull.Columns.Clear();
                ASPxGridView_Artikull.DataSource = tmpObject;
                //ASPxGridView_Artikull.AutoGenerateColumns = true;
                ASPxGridView_Artikull.DataBind();
                tmpObject.Dispose();
            }
        }

        //therritet ne rastin kur eshte rifreskim gride nga MyTitlePanelTemplateMeRefresh
        public override void MbushGrideNgaDb(bool ndryshoFiltrinGrides = false)
        {
            mbushGridArtikujshNgaDB(IdPerdoruesi, IdNdermarrja, cbKosto.Checked, cbGjendje.Checked, cbCmime.Checked, cbCmimeMeTvsh.Checked, (string)hfState.Get(clsArtikulli.postStringTvsh), false);
        }

        private void mbushGridArtikujshNgaDB(int idPerdorues, int idNdermarrje, bool kostoSasi, bool gjendje, bool cmime, bool cmimeMeTvsh, string postStringTvsh, bool autoGenerateColumns = true)
        {//mbush griden e popupit me te dhena
            //bool kostoSasi = cbKosto.Checked || cbGjendje.Checked;
            DataTable dt = DbCore.DbInventari.colArtikujt.merrSipasArtikujNdermarrjesAndAutorizimeDT(idNdermarrje, idPerdorues, kostoSasi, gjendje, cmime, cmimeMeTvsh, postStringTvsh);
            DbCore.mySessionObjects.ruajGrideNeSession(komponente, Session, dt);
            if(autoGenerateColumns)
                ASPxGridView_Artikull.Columns.Clear();
            ASPxGridView_Artikull.DataSource = dt;
            ASPxGridView_Artikull.AutoGenerateColumns = autoGenerateColumns;
            ASPxGridView_Artikull.DataBind();
            dt.Dispose();
        }

        private void mbushListeCmimesh(int idPerdoruesi, int idNdermarrje)
        {//mbushet grida me te dhena
            DbCore.DbInventari.colCmimeArtikujsh col = new DbCore.DbInventari.colCmimeArtikujsh();

            DataTable dt = ((string)hfKushtet.Get("CB") == "Jo") ?
                DbCore.DbInventari.colNiveleCmimesh.merrNiveleNdermarjeDTBlerjeShitjeMeAutorizime(idNdermarrje, 0, idPerdoruesi)
                :
                DbCore.DbInventari.colNiveleCmimesh.merrNiveleNdermarjeDTBlerjeDheShitjeMeAutorizime(idNdermarrje, idPerdoruesi);
            JavaScriptSerializer serializusi = new JavaScriptSerializer();
            int[] formatisasi = new int[dt.Rows.Count];
            int[] formaticmim = new int[dt.Rows.Count];
            int[] formatikursi = new int[dt.Rows.Count];
            int j = 0;
            DbCore.DbAdmin.colMonedhat colMon = new DbCore.DbAdmin.colMonedhat();
            colMon.mbushGjitheMonedhat(idNdermarrje, idPerdoruesi);
            foreach (DataRow dr in dt.Rows)
            {
                int idMonedha = int.Parse(dr["IdMonedha"].ToString());
                DbCore.DbAdmin.clsKurset kursi = new DbCore.DbAdmin.clsKurset(idMonedha, DateTime.Now);
                DbCore.DbInventari.clsCmimArtikulli cm = new DbCore.DbInventari.clsCmimArtikulli(0, 0, int.Parse(dr["IdNivelCmimi"].ToString()), 0, idMonedha, new DateTime(DateTime.Today.Year, 1, 1), new DateTime(9999, 12, 31), 0, 0, 0, idPerdoruesi, idNdermarrje, 0, 1, 0, 0, bool.Parse(dr["NjesiTeVarura"].ToString()), kursi.VleraKursi, new DateTime(DateTime.Today.Year, 1, 1, 0, 0, 0), new DateTime(9999, 12, 31, 23, 59, 59), 0, 0, 0, 0,0, dr["IdCmimRetail"].ToString()=="" ? 0 :int.Parse(dr["IdCmimRetail"].ToString()));
                if (formatNrPerKonfigCmimi.IdFormatKonfig == 0)
                {
                    formatisasi[j] = 2;
                    formaticmim[j] = 2;
                }
                else
                {
                    formatisasi[j] = formatNrPerKonfigCmimi.KonfigTrupi.merrFormatSipasMonedhes(idMonedha).ShifraPasPresjesSasia;
                    formaticmim[j] = formatNrPerKonfigCmimi.KonfigTrupi.merrFormatSipasMonedhes(idMonedha).ShifraPasPresjesCmimi;
                }
                formatikursi[j] = colMon.Find(x => x.IdMonedha == idMonedha).IdFormatNrKursi;
                j++;
                col.Add(cm);
            }
            hfState.Set("formatisasi", serializusi.Serialize(formatisasi));
            hfState.Set("formaticmim", serializusi.Serialize(formaticmim));
            hfState.Set("formatikursi", serializusi.Serialize(formatikursi));
            gvCmimet.DataSource = col;
            gvCmimet.DataBind();
        }

     
        private void mbushListeCmimeshMod(int idNdermarrje, int id, int idPerdorues)
        {//mbushet grida me te dhena
            DbCore.DbInventari.colCmimeArtikujsh col = new DbCore.DbInventari.colCmimeArtikujsh();
            col.mbushCmimArtikulliSipasArtikullitMeKostoMeAutorizime(id, idNdermarrje, idPerdorues, (string)hfKushtet.Get("CB") == "Jo");
           
            JavaScriptSerializer serializusi = new JavaScriptSerializer();
            int[] formatisasi = new int[col.Count];
            int[] formaticmim = new int[col.Count];
            int[] formatikursi = new int[col.Count];
            int j = 0;
            DbCore.DbAdmin.colMonedhat colMon = new DbCore.DbAdmin.colMonedhat();
            colMon.mbushGjitheMonedhat(idNdermarrje, (int)hfState["idPerdoruesi"]);
            foreach (clsCmimArtikulli cm in col)
            {
                int idMonedha = cm.IdMonedha;
                if (formatNrPerKonfigCmimi.IdFormatKonfig == 0)
                {
                    formatisasi[j] = 2;
                    formaticmim[j] = 2;
                }
                else
                {
                    formatisasi[j] = formatNrPerKonfigCmimi.KonfigTrupi.merrFormatSipasMonedhes(idMonedha).ShifraPasPresjesSasia;
                    formaticmim[j] = formatNrPerKonfigCmimi.KonfigTrupi.merrFormatSipasMonedhes(idMonedha).ShifraPasPresjesCmimi;
                }
                formatikursi[j] = colMon.Find(x => x.IdMonedha == idMonedha).IdFormatNrKursi;
                j++;
            }
            hfState.Set("formatisasi", serializusi.Serialize(formatisasi));
            hfState.Set("formaticmim", serializusi.Serialize(formaticmim));
            hfState.Set("formatikursi", serializusi.Serialize(formatikursi));
            gvCmimet.DataSource = col;
            gvCmimet.DataBind();
        }


        private void percaktoTemplateBuxheti()
        {// percaktohen tipet e kolonave per griden e buxheteve
            GridViewDataTextColumn col1 = gvBuxheti.Columns["Muaj"] as GridViewDataTextColumn;
            col1.VisibleIndex = 0;
            col1.DataItemTemplate = new MyLabelTemplate();
            GridViewDataTextColumn col2 = gvBuxheti.Columns["Gjendja"] as GridViewDataTextColumn;
            col2.VisibleIndex = 1;
            col2.DataItemTemplate = new MyLabelTemplate();
            GridViewDataTextColumn col3 = gvBuxheti.Columns["Buxheti_1"] as GridViewDataTextColumn;
            col3.VisibleIndex = 2;
            col3.DataItemTemplate = new MyDoubleTemplate(false, 2, "0"); // "0.00");
            GridViewDataTextColumn col4 = gvBuxheti.Columns["Buxheti_2"] as GridViewDataTextColumn;
            col4.VisibleIndex = 3;
            col4.DataItemTemplate = new MyDoubleTemplate(false, 2, "0"); // "0.00");
            GridViewDataTextColumn col5 = gvBuxheti.Columns["Diferenca_1"] as GridViewDataTextColumn;
            col5.VisibleIndex = 4;
            col5.ReadOnly = true;
            col5.DataItemTemplate = new MyReadOnlyTextTemplate();
            GridViewDataTextColumn col6 = gvBuxheti.Columns["Diferenca_2"] as GridViewDataTextColumn;
            col6.VisibleIndex = 5;
            col6.ReadOnly = true;
            col6.DataItemTemplate = new MyReadOnlyTextTemplate();
        }

        private void percaktoTemplateCmimi()
        {//percaktohen templatet per fushat e grides
            GridViewDataDateColumn col2 = gvCmimet.Columns["DateFillimi"] as GridViewDataDateColumn;
            GridViewDataDateColumn col3 = gvCmimet.Columns["DateMbarimi"] as GridViewDataDateColumn;
            GridViewDataDateColumn col22 = gvCmimet.Columns["KoheFillimi"] as GridViewDataDateColumn;
            GridViewDataDateColumn col23 = gvCmimet.Columns["KoheMbarimi"] as GridViewDataDateColumn;
            GridViewDataTextColumn col4 = gvCmimet.Columns["SasiMin"] as GridViewDataTextColumn;
            GridViewDataTextColumn col5 = gvCmimet.Columns["SasiMax"] as GridViewDataTextColumn;
            GridViewDataTextColumn col7 = gvCmimet.Columns["Cmimi"] as GridViewDataTextColumn;
            GridViewDataTextColumn col8 = gvCmimet.Columns["Cmimi2"] as GridViewDataTextColumn;
            GridViewDataTextColumn col17 = gvCmimet.Columns["CmimiTvsh"] as GridViewDataTextColumn;
            GridViewDataTextColumn col18 = gvCmimet.Columns["Cmimi2Tvsh"] as GridViewDataTextColumn;
            GridViewDataTextColumn col28 = gvCmimet.Columns["Norme"] as GridViewDataTextColumn;
            GridViewDataTextColumn col9 = gvCmimet.Columns["Kosto"] as GridViewDataTextColumn;
            GridViewDataTextColumn col10 = gvCmimet.Columns["Formula"] as GridViewDataTextColumn;
            GridViewDataCheckColumn col0 = gvCmimet.Columns["Update"] as GridViewDataCheckColumn;
            GridViewDataTextColumn col6 = gvCmimet.Columns["Kursi"] as GridViewDataTextColumn;
            GridViewDataColumn col16 = gvCmimet.Columns["IdTvsh"] as GridViewDataColumn;
            col2.DataItemTemplate = new MyCalendarTemplate();
            col2.Width = 100;
            col3.DataItemTemplate = new MyCalendarTemplate();
            col3.Width = 100;
            col22.DataItemTemplate = new MyTimeEditTemplate();
            col23.DataItemTemplate = new MyTimeEditTemplate();
            col16.DataItemTemplate = new MyComboTemplate();
            col4.DataItemTemplate = new MyDoubleTemplate(true, 2, "0");
            col5.DataItemTemplate = new MyDoubleTemplate(true, 2, "0");
            col7.DataItemTemplate = new MyDoubleTemplate(true, 2, "0");
            col8.DataItemTemplate = new MyDoubleTemplate(true, 2, "0");
            col17.DataItemTemplate = new MyDoubleTemplate(true, 2, "0");
            col18.DataItemTemplate = new MyDoubleTemplate(true, 2, "0");
            col28.DataItemTemplate = new MyLabelTemplate();
            col9.DataItemTemplate = new MyDoubleTemplate(true, 2, "0");
            col10.DataItemTemplate = new MyComboTemplate();
            col10.Width = 200;
            col6.DataItemTemplate = new MyDoubleTemplate(true, 2, "0"); //new MyReadOnlyTextTemplate();
            col0.DataItemTemplate = new MyButtonTemplate("Update");
        }

       

        private DbCore.DbInventari.colDetajimePerArt ruajDetajime(int idNdermarrje, int lloji)
        {
            DbCore.DbInventari.colDetajimePerArt colDetArt = new DbCore.DbInventari.colDetajimePerArt();
            //lloji: 1-> per kategorine e pare, 2-> per kategorine e dyte
            if (lloji == 1)
            {
                if (btnDetajim1Nga.Text != "")
                {
                    string[] det = btnDetajim1Nga.Text.Split(',');
                    for (int i = 0; i < det.Length; i++)
                    {
                        DbCore.DbInventari.clsDetajimArtikulli detArtikulli = new DbCore.DbInventari.clsDetajimArtikulli();
                        detArtikulli.mbushDetajimArtikulli(det[i], idNdermarrje);
                        DbCore.DbInventari.clsDetajimPerArt c = new DbCore.DbInventari.clsDetajimPerArt();
                        c.IdDetajimArtikulli = detArtikulli.IdDetajimArtikulli;
                        c.LlojDetajim = 1;
                        colDetArt.Add(c);
                    }
                }
            }
            else if (lloji == 2)
            {
                if (btnDetajim2Nga.Text != "")
                {
                    string[] det = btnDetajim2Nga.Text.Split(',');
                    for (int i = 0; i < det.Length; i++)
                    {
                        DbCore.DbInventari.clsDetajimArtikulli detArtikulli = new DbCore.DbInventari.clsDetajimArtikulli();
                        detArtikulli.mbushDetajimArtikulli(det[i], idNdermarrje);
                        DbCore.DbInventari.clsDetajimPerArt c = new DbCore.DbInventari.clsDetajimPerArt();
                        c.IdDetajimArtikulli = detArtikulli.IdDetajimArtikulli;
                        c.LlojDetajim = 2;
                        colDetArt.Add(c);
                    }
                }
            }
            return colDetArt;
        }

        private DbCore.DbInventari.colArtikujtZevendesues ruajArtikujZevendesues(int idNdermarrje)
        {
            //DbCore.DbInventari.clsDatabaseInventari dbInventari = new DbCore.DbInventari.clsDatabaseInventari();

            DbCore.DbInventari.colArtikujtZevendesues artikujt = new DbCore.DbInventari.colArtikujtZevendesues();
            DbCore.DbInventari.clsArtikullZevendesues artikulli;

            string initVal = hfArtikulli.Value;
            string[] pars1 = initVal.Split(',');
            string initVal2 = hfEmertimiA.Value;
            string[] pars3 = initVal2.Split(',');
            string initVal3 = hfPrioritetiA.Value;
            string[] pars5 = initVal3.Split(',');


            string[] kodi = new string[pars1.Length];
            string[] emri = new string[pars1.Length];
            string[] prioriteti = new string[pars1.Length];
            if (initVal != "")//merren te dhenat e hiden fieldeve te trupave te fleteve kontabel nga javascipti
            {
                for (int i = 0; i < pars1.Length; i++)
                {
                    string[] pars2 = pars1[i].Split(':');
                    kodi[Convert.ToInt32(pars2[0])] = pars2[1];
                }
            }
            if (initVal2 != "")
            {
                for (int i = 0; i < pars3.Length; i++)
                {
                    string[] pars4 = pars3[i].Split(':');
                    emri[Convert.ToInt32(pars4[0])] = pars4[1];
                }
            }
            if (initVal3 != "")
            {
                for (int i = 0; i < pars5.Length; i++)
                {
                    string[] pars6 = pars5[i].Split(':');
                    prioriteti[Convert.ToInt32(pars6[0])] = pars6[1];
                }
            }

            for (int i = 0; i < pars1.Length; i++)//krijohet kolectioni me trupat e fleteve kontabel e futura nga perdoruesi
            {
                artikulli = new DbCore.DbInventari.clsArtikullZevendesues();
                if (kodi[i] != null && kodi[i] != "null" && kodi[i] != "")
                {
                    artikulli.KodArtikulli = kodi[i];
                    artikulli.IdArtikulliZevend = DbCore.DbInventari.clsArtikulli.ktheIdArtikulli(kodi[i], idNdermarrje);
                    //artikulli.IdArtikulliZevend = dbInventari.ktheArtikull(kodi[i], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session))[0].IdArtikulli;
                    if (emri[i] != null && emri[i] != "null" && emri[i] != "")
                        artikulli.PershkrimArtikulli = emri[i];
                    if (prioriteti[i] != null && prioriteti[i] != "null" && prioriteti[i] != "")
                        artikulli.Prioriteti = prioriteti[i];

                    artikujt.Add(artikulli);
                }
            }
            return artikujt;
        }

        private void konfiguroBuxhetGride(int idNdermarrje)
        {//konfiguron griden
            //Per momentin nuk duhet
            gvBuxheti.Visible = false;
            if (gvBuxheti.Visible)
            {
                CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
                ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                GridUtil.konfiguroGrideRegjistrimEvogelPaTheme(gvBuxheti, "IdBuxheti", false);
            }
        }



        private DbCore.DbInventari.colCmimeArtikujsh krijoCmimeArtikulli(int idNjesi1Artikulli, int idNjesi2Artikulli, int idPerdorues, int idNdermarrje)
        {
            colCmimeArtikujsh cmimet = new colCmimeArtikujsh();
            int idKonfigCmime = DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod("CSH", idNdermarrje);
            //colCmimeArtikujsh colCmime = JsonConvert.DeserializeObject<colCmimeArtikujsh>();
            colCmimeArtikujsh colCmime = JsonConvert.DeserializeObject<colCmimeArtikujsh>(hfArtikuj.Value, 
                new JsonSerializerSettings{ DateTimeZoneHandling = DateTimeZoneHandling.Local});
            if (colCmime == null)
                return null;
            foreach (clsCmimArtikulli cm in colCmime)
            {
                cm.IdKonfig = idKonfigCmime;
                cm.IdNjesia = idNjesi1Artikulli;
                cm.IdNjesia2 = idNjesi2Artikulli;
                cm.IdStatusDok = 1;
                cm.IdPerdoruesi = idPerdorues;
                cm.IdNdermarje = idNdermarrje;
                if (cm.IdCmimArtikulli != 0 || cm.Cmimi != 0 || cm.Cmimi2 != 0 || cm.SasiMin != 0 || cm.SasiMax != 0)
                    cmimet.Add(cm);
            }

            cmimet.ShtoCmimeRetailNeKoleksion(colCmime);
            return cmimet;
        }

        //sherben per te ruajtur nje artikull
        private void ruajArtikull()
        {
            if (Page.IsValid == false)
                return;
            int idNdermarrje = (int)hfState["idNdermarrje"];

            DbCore.DbInventari.clsArtikulli artikulli;
            int idPerdorues = (int)hfState["idPerdoruesi"];
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            try
            {
                if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim")
                {
                    artikulli = krijoArtikullin(idNdermarrje, idPerdorues, true, rm, ci);
                    hfArkiva.Set("kopjoArkiven", hfShtimModifikim.Value == "klonim");
                }
                else artikulli = krijoArtikullin(idNdermarrje, idPerdorues, false, rm, ci);
            }
            catch (Exception e)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(e.Message);
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, e.Message, pnlMesazhi);
                hfStatusi.Value = "false";               
                return;
            }

            DbCore.clsMesazh mesazh = new DbCore.clsMesazh(true);
            DbCore.DbInventari.clsArtikullPerberesTemplateKoka template = new DbCore.DbInventari.clsArtikullPerberesTemplateKoka();
            DbCore.DbInventari.colCmimeArtikujsh colCmime = new DbCore.DbInventari.colCmimeArtikujsh();

            if ((bool)hfTeDrejtaCmimi.Get("Amb") && (bool)hfTeDrejtaCmimi.Get("Modifikim"))
                colCmime = krijoCmimeArtikulli(artikulli.Njesi1Artikulli, artikulli.Njesi2Artikulli, idPerdorues, idNdermarrje);
            else colCmime = null;

            if (cmbKlasa.Value.ToString() == "4")   //nese eshte artikull i perbere, ose ndryshe set
            {
                if (hfTemplateArtikujPerberes.Value != "")
                {
                    template = formoKlasenPerTemplate(hfTemplateArtikujPerberes.Value);
                    if (template.Kodi != null && template.Kodi != "")
                    {
                        DbCore.DbInventari.colArtikulliPerberesTemplateTrupi oColTrupi = new DbCore.DbInventari.colArtikulliPerberesTemplateTrupi(artikulli.ColArtikujPerberes);
                        template.oColTrupi = oColTrupi;
                    }
                }

                if (!String.IsNullOrEmpty(cmbFormatSeriali.Text))
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgArtikullSetNukMundTeKeteFormatSeriali"], pnlMesazhi);
                    hfStatusi.Value = "false";
                    return;
                }
            }
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdorues, idNdermarrje, (int)hfState["idViti"], DbCore.clsFunksione.GetKomponente(Page.Request));
            if (!nivelTVShIPranueshem())
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNukKeniPercaktuarNivelTVSH", ci), pnlMesazhi);
                hfStatusi.Value = "false";
                return;
            }
            bool eshteShtim;
            if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim")
            {
                if (!tedrejtaInfo.DShtim)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNukKeniTeDrejta", ci), pnlMesazhi);
                    hfStatusi.Value = "false";
                    return;
                }

               
                mesazh = artikulli.ruaj(hfNrAutoKF, colCmime, template, false, "", "", "", "");
                eshteShtim = true;
            }
            else
            {
                if (!tedrejtaInfo.DMod)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNukKeniTeDrejta", ci), pnlMesazhi);
                    hfStatusi.Value = "false";
                    return;
                }
                DbCore.DbRegjistrim.clsDatabaseRegjistrim dbRegjistrim = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
                DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                konf.mbushKonfigAmbjSipasId(artikulli.IdKonfig, (int)hfState["idGjuha"]);
                artikulli.IdArtikulli = int.Parse(hfId.Value.ToString());
                bool lidhur = dbRegjistrim.eshteDokumentiILidhurCelje(artikulli.IdArtikulli.ToString(), konf.IdNivel.ToString());
                int formatiSerialitMeparshem = int.Parse(hfFormatSeriali.Value.ToString());
                
                if (lidhur.ToString() != hfLidhur.Value)
                {
                    mesazh.Status = false;
                    mesazh.PershkrimMesazhi = rm.GetString("msgShtoArtikullEshteILidhur", ci);
                } 

                if (mesazh.Status && colArtikujt.kaGjendjeArtikulli(artikulli.IdArtikulli))
                {
                    if (formatiSerialitMeparshem == 0 && artikulli.IdFormatSeriali != 0)
                    {
                        mesazh.Status = false;
                        mesazh.PershkrimMesazhi = MessagesResource.Messages["msgArtGjendjeMosVendosFormatSerial"];
                    }
                    else if (formatiSerialitMeparshem != 0 && artikulli.IdFormatSeriali == 0)
                    {
                        mesazh.Status = false;
                        mesazh.PershkrimMesazhi = MessagesResource.Messages["msgArtGjendjeMoHiqFormatSerial"];
                    }
                }

                if (mesazh.Status)
                {
                    mesazh = artikulli.modifiko(template, colCmime, false, "0", string.Empty, string.Empty, string.Empty);
                }
                eshteShtim = false;
                dbRegjistrim.Dispose();
            }

            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                hfStatusi.Value = "false";
            }
            else
            {
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                string postStringTvsh = (string)hfState.Get(clsArtikulli.postStringTvsh);
                if (eshteShtim)
                    shtoArtikullNeGrid(idNdermarrje, idPerdorues, artikulli.IdArtikulli, rm, ci, postStringTvsh);
                else //modifikim
                    modifikoArtikullNeGrid(idNdermarrje, idPerdorues, artikulli.IdArtikulli, rm, ci, postStringTvsh);
                hfStatusi.Value = "true";
                pastroFusha(idPerdorues, idNdermarrje);
                konfiguroGride(idNdermarrje, cmbKonfigurimi.Text, 402, cbKosto.Checked, cbGjendje.Checked, ci, rm, null, idPerdorues, cbCmime.Checked, cbCmimeMeTvsh.Checked, postStringTvsh);
                ASPxPageControl1.ActiveTabIndex = 0;
            }
            pnlMesazhi.Update();
        }

        protected DbCore.DbInventari.colArtikulliPerberes formoColArtikujshPerberes(int idNdermarrje, String vlerat)
        {
            DbCore.DbInventari.colArtikulliPerberes oColArtikujPerberes = new DbCore.DbInventari.colArtikulliPerberes();
            string[] arrArtikujPerberes = vlerat.Split(';');

            string[] arrLloji = arrArtikujPerberes[0].Split('|');
            string[] arrKodi = arrArtikujPerberes[1].Split('|');  //5
            string[] arrKoeficienti = arrArtikujPerberes[2].Split('|'); //3
            string[] arrVlera = arrArtikujPerberes[3].Split('|');  //4
            for (int i = 0; i < arrLloji.Length; i++)
            {
                string[] arrCompLloji = arrLloji[i].Split(':');
                string[] arrCompKodi = arrKodi[i].Split(':');
                string[] arrCompKoeficienti = arrKoeficienti[i].Split(':');
                string[] arrCompVlera = arrVlera[i].Split(':');
                DbCore.DbInventari.clsArtikulliPerberes oArtikull = new DbCore.DbInventari.clsArtikulliPerberes();

                if (arrCompLloji.Length != 1 && arrCompLloji[1].ToString() != "" && arrCompLloji[1].ToString() != " ")

                    oArtikull.Lloji = Convert.ToInt32(arrCompLloji[1]);
                int idlidhese = 0;
                if (arrCompKodi.Length != 1 && arrCompKodi[1].ToString() != "" && arrCompKodi[1].ToString() != " ")
                    if (oArtikull.Lloji == 1)
                    {
                        DbCore.DbInventari.clsArtikulli art = new DbCore.DbInventari.clsArtikulli();
                        art.merrSipasKodArtikullit(arrCompKodi[1].ToString(), idNdermarrje);
                        idlidhese = art.IdArtikulli; oArtikull.IdLidheseArt = idlidhese;
                    }
                    else if (oArtikull.Lloji == 2)
                    {
                        DbCore.DbProdhimi.clsAktiviteteKoka aktivitet = new DbCore.DbProdhimi.clsAktiviteteKoka(arrCompKodi[1].ToString(), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                        idlidhese = aktivitet.IdKoka; oArtikull.IdLidheseAkt = idlidhese;
                    }

                if (arrCompKoeficienti.Length != 1 && arrCompKoeficienti[1].ToString() != "" && arrCompKoeficienti[1].ToString() != " ")
                    oArtikull.Koeficienti = Convert.ToInt32(arrCompKoeficienti[1].ToString());
                if (arrCompVlera.Length != 1 && arrCompVlera[1].ToString() != "" && arrCompVlera[1].ToString() != " ")
                    oArtikull.Scrap = Convert.ToInt32(arrCompVlera[1].ToString());
                if (idlidhese != 0)
                    oColArtikujPerberes.Add(oArtikull);
            }
            return oColArtikujPerberes;
        }

        protected static DbCore.DbInventari.clsArtikullPerberesTemplateKoka formoKlasenPerTemplate(String vlerat)
        {
            string[] arrTemplate = vlerat.Split(';');

            if (arrTemplate[0].ToString() == "false")
                return new DbCore.DbInventari.clsArtikullPerberesTemplateKoka();
            else if (arrTemplate[0] == "true")
            {
                DbCore.DbInventari.clsArtikullPerberesTemplateKoka koka = new DbCore.DbInventari.clsArtikullPerberesTemplateKoka();
                koka.Kodi = arrTemplate[1].ToString();
                koka.Pershkrimi = arrTemplate[2];
                koka.oColTrupi = new DbCore.DbInventari.colArtikulliPerberesTemplateTrupi();
                return koka;
            }
            else return new DbCore.DbInventari.clsArtikullPerberesTemplateKoka();
        }

        private void pastroFusha(int idPerdorues, int idNdermarrje)
        {//pastron fushat
            // pergjigja.Text = "";
            this.btnMagazina.Text = "";
            this.btneKodbari.Text = "";
            this.btneKodifikimi1.SelectedIndex = -1;
            this.btneKodifikimi2.SelectedIndex = -1;
            this.btneKodifikimi3.SelectedIndex = -1;
            this.cbDetajim.Checked = false;
            this.cmbAutorizimiHf.Value = "";
            this.cmbNjesia1.SelectedIndex = -1;
            this.cmbNjesia2.SelectedIndex = -1;
            this.hfAutorizime.Value = "";
            this.txtFurnitori.SelectedIndex = -1;
            this.txtKodi.Text = "";
            this.txtKodiDoganor.Text = "";
            this.txtKoeficienti.Text = "1";
            this.txtLlog.Text = "";
            this.txtOrigjina.Text = "";
            this.txtPershkrimi.Text = "";
            this.txtPershkrimiAng.Text = "";
            this.txtPeshaBruto.Text = "0";
            this.txtPeshaNeto.Text = "0";
            this.txtVendodhja.Text = "";
            this.txtKodi2.Text = "";
            this.txtPershkrimi2.Text = "";
            //     mbushListeFurnitoresh();
            mbushListeCmimesh(idPerdorues, idNdermarrje);
            //this.cmbModeli.SelectedIndex = 0;
            HiddenField1.Value = "";
            this.txtKodi3.Text = "";
            this.txtPershkrimi3.Text = "";
            this.cmbKlasa.SelectedIndex = 0;
            this.btneSkema.Text = "";
            this.btneLlogBle.Text = "";
            this.btneLlogInv.Text = "";
            this.btnLlogPakesim.Text = "";
            this.btneLlogShit.Text = "";
            this.btneLlogTretet.Text = "";
            this.btnLlogShpe.Text = "";
            this.btneLlogPakesimRez.Text = "";
            this.btneLlogRez.Text = "";
            this.btnLLogariKomisioni.Text= "";
            this.txtMinimumi.Text = "0";
            this.txtMaximumi.Text = "0";
            this.cmbMetode.SelectedIndex = -1;
            this.cmbKMSH.SelectedIndex = -1;
            this.cmbZevendesim.SelectedIndex = -1;
            this.btneArtikuj.Text = "";
            this.hfArtikulli.Value = "";
            this.hfEmertimiA.Value = "";
            this.hfPrioritetiA.Value = "";
            hfAutorizime.Value = "";
            hfEmertimiF.Value = "";
            hfFurntiori.Value = "";
            hfPrioritetiF.Value = "";
            //mbushListeBuxhetesh();
            //mbushListeFushashShtese();
            hfBuxheti1.Value = "";
            hfBuxheti2.Value = "";
            this.txtKodi4.Text = "";
            //this.txtKodi5.Text = "";
            this.txtPershkrimi4.Text = "";
            hfSkema.Value = "";
            //this.txtPershkrimi5.Text = "";
            //btnDetajim1Ne.Text = "";
            btnDetajim1Nga.Text = ""; btnDetajim2Nga.Text = "";
            //btnDetajim2Ne.Text = "";
            //btnDetajim2Nga.Text = "";
          //  mbushComboModeli(idPerdorues, idNdermarrje);
            //hfFushatShtese.Value = "";

            //percaktoTemplateFushash();
            //hfKodbar.Set("kod", "");
            hfKodifikime.Value = "";
            cmbLloji.SelectedIndex = -1;
            cbMeBarkodLogjik.Checked = false;
            txtSkemeBarkodi.Text = "";
            cbArtikullVjeter.Checked = false;
            ucFushatShtese.PastroFusha(Convert.ToInt32(hfId.Value));
            hfFormatSeriali.Value = null;
        }

        private colKodbare ruajKodbare()
        {
            if (String.IsNullOrEmpty(hfKodbaret.Value))
                return new colKodbare();

            JavaScriptSerializer serializusi = new JavaScriptSerializer();
            Object[] kodbaret = (Object[])serializusi.DeserializeObject(hfKodbaret.Value);
            return new colKodbare(kodbaret);
        }


        private DbCore.DbInventari.clsArtikulli krijoArtikullin(int idNdermarrje, int idPerdorues, bool shtim, ResourceManager rm, CultureInfo ci)
        {
            var controls = this.GetAsPxTextEditIdValue();
            controls.AddRange(ASPxPageControl1.GetAsPxTextEditIdValue());
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();

            hfNrAuto = (ASPxHiddenField)NrAuto.VendosVleratNrAuto(hfNrAuto, controls);
            //DbCore.DbAdmin.NrAuto.vendosVleratNrAuto(hfNrAuto, this, this.ASPxPageControl1, null, null);

            int idNjesi1Artikulli = 0, idNjesi2Artikulli = 0, kodifikim1 = 0, kodifikim2 = 0, kodifikim3 = 0, idfurnitori = 0, idskema = 0, idlloginv = 0, idLlogariPakesim = 0, idllogble = 0, idllogshit = 0, idllogshpe = 0, idllogtret = 0, idllogamor = 0, idllogrez = 0, idllogpakrez = 0, IdLlojGarancia = 0, idmag = 0, idformatseriali = 0, txtKarakterTAC = 0, idLlogKomision = 0;
            if (cmbNjesia1.Text != "") int.TryParse(cmbNjesia1.Value.ToString(), out idNjesi1Artikulli);
            if (cmbNjesia2.Text != "") int.TryParse(cmbNjesia2.Value.ToString(), out idNjesi2Artikulli);
            int klasa = cmbKlasa.Text != "" ? Convert.ToInt32(cmbKlasa.Value) : 0;
            hfStatusi.Value = "true";
            if (String.IsNullOrEmpty(btneKodifikimi1.Text) && (Request.QueryString["llojiart"] == "aqt"))
            {
                throw new MyException(rm.GetString("msgFushaGrupim1JoBosh", ci));
            }
            if (!String.IsNullOrEmpty(btneKodifikimi1.Text))
            {
                clsKodifikimArtikulli kod1 = new clsKodifikimArtikulli(btneKodifikimi1.Text, idNdermarrje, 1, (cmbLloji.Text == DbCore.DbInventari.clsArtikulli.AfatShkurterLabel ? false : true));
                kodifikim1 = kod1.IdKodifikimi;
            }

            if (!String.IsNullOrEmpty(btneKodifikimi2.Text))
            {
                clsKodifikimArtikulli kod2 = new clsKodifikimArtikulli(btneKodifikimi2.Text, idNdermarrje, 2, (cmbLloji.Text == DbCore.DbInventari.clsArtikulli.AfatShkurterLabel ? false : true));
                kodifikim2 = kod2.IdKodifikimi;
            }
            if (!String.IsNullOrEmpty(btneKodifikimi3.Text))
            {
                clsKodifikimArtikulli kod3 = new clsKodifikimArtikulli(btneKodifikimi3.Text, idNdermarrje, 3, (cmbLloji.Text == DbCore.DbInventari.clsArtikulli.AfatShkurterLabel ? false : true));
                kodifikim3 = kod3.IdKodifikimi;
            }
            DbCore.DbKontabiliteti.clsKlientFurnitor kf = new DbCore.DbKontabiliteti.clsKlientFurnitor();
            if (txtFurnitori.Value != null)
            {
                int.TryParse(txtFurnitori.Value.ToString(), out idfurnitori);
                kf = new DbCore.DbKontabiliteti.clsKlientFurnitor(idfurnitori);
            }
            if (btneSkema.Text != "") int.TryParse(btneSkema.Value.ToString(), out idskema);
            if (btneLlogInv.Text != "")
                idlloginv = DbCore.DbKontabiliteti.clsLlogari.mbushIDLlogariSipasKodit(btneLlogInv.Text, idNdermarrje);
            if (btnLlogPakesim.Text != "")
                idLlogariPakesim = DbCore.DbKontabiliteti.clsLlogari.mbushIDLlogariSipasKodit(btnLlogPakesim.Text, idNdermarrje);
            if (btneLlogBle.Text != "")
                idllogble = DbCore.DbKontabiliteti.clsLlogari.mbushIDLlogariSipasKodit(btneLlogBle.Text, idNdermarrje);
            if (btneLlogShit.Text != "")
                idllogshit = DbCore.DbKontabiliteti.clsLlogari.mbushIDLlogariSipasKodit(btneLlogShit.Text, idNdermarrje);
            if (btneLlogTretet.Text != "")
                idllogtret = DbCore.DbKontabiliteti.clsLlogari.mbushIDLlogariSipasKodit(btneLlogTretet.Text, idNdermarrje);
            if (btnLlogShpe.Text != "")
                idllogshpe = DbCore.DbKontabiliteti.clsLlogari.mbushIDLlogariSipasKodit(btnLlogShpe.Text, idNdermarrje);
            if (cmbLlogAmortizimi.Text != "")
                idllogamor = DbCore.DbKontabiliteti.clsLlogari.mbushIDLlogariSipasKodit(cmbLlogAmortizimi.Text, idNdermarrje);
            if (btneLlogPakesimRez.Text != "")
                idllogpakrez = DbCore.DbKontabiliteti.clsLlogari.mbushIDLlogariSipasKodit(btneLlogPakesimRez.Text, idNdermarrje);
            if (btneLlogRez.Text != "")
                idllogrez = DbCore.DbKontabiliteti.clsLlogari.mbushIDLlogariSipasKodit(btneLlogRez.Text, idNdermarrje);
            if (btnLLogariKomisioni.Text != "")
                idLlogKomision = DbCore.DbKontabiliteti.clsLlogari.mbushIDLlogariSipasKodit(btnLLogariKomisioni.Text, idNdermarrje);

            string kodtakse = cmbNivelTvsh.Text != "" ? DbCore.DbRegjistrim.clsTaksa.ktheKodTakseMeId(Convert.ToInt32(cmbNivelTvsh.Value)) : "";

            hfNrAutoKF = (ASPxHiddenField)NrAuto.ShtoNeRegjistrime(hfNrAutoKF, hfNrAuto, "txtKodi", "KodArtikulli");
            //DbCore.DbAdmin.NrAuto.shtoNeHfRegjistrime(hfNrAutoKF, hfNrAuto, "txtKodi", "KodArtikulli");

            int idobjektiva;
            if (cmbObjektiva.Text != "")
            {
                DbCore.DbQendraKosto.clsObjektivaKosto obj = new DbCore.DbQendraKosto.clsObjektivaKosto(cmbObjektiva.Text, idNdermarrje);
                idobjektiva = obj.Id;
            }
            else idobjektiva = 0;
            if (cmbGarancia.Text != "")
                IdLlojGarancia = int.Parse(cmbGarancia.Value.ToString());

            if (btnMagazina.Text != "")
            {
                DbCore.DbRegjistrim.clsNjesiAdministrative mag1 = new DbCore.DbRegjistrim.clsNjesiAdministrative(btnMagazina.Text, idNdermarrje, idPerdorues);
                idmag = mag1.IdNjesiAdministrative;
            }
            if (cmbFormatSeriali.Text != "")
            {// to do te merret format seriali
                clsSerialeUnikeFormate formate = new clsSerialeUnikeFormate(cmbFormatSeriali.Text, idNdermarrje);
                idformatseriali = formate.ID;
            }
            decimal garancia = 0; bool vlefshme = true;
            if (txtGarancia.Text != "")
                vlefshme = decimal.TryParse(txtGarancia.Text, out garancia);
            if (!vlefshme)
            {
                throw new DbCore.MyException(rm.GetString("msgShtoArtikullGaranciaDuhetNumer", ci));
            }
            //ucFushatShtese.ruajFushat();
            DbCore.DbAdmin.colVleraFushaShtese colvler = ucFushatShtese.merrFushatShtese();

            int idArtRaportuesi = 0;
            string kodArtRaportuesi = cmbLidhMeNdermRap.Text;
            if (kodArtRaportuesi != String.Empty)
            {
                int idNdermarrjeRaportuese = mySessionObjects.ktheNdermRaportuese(Session);
                if (idNdermarrjeRaportuese == 0)
                {
                    throw new MyException(rm.GetString("msgNdermarrjaNukKaNdermRaportuese", ci));
                }
                int idArtikullRaportuesi = clsArtikulli.ktheIdArtRaportuesSipasKodit(kodArtRaportuesi, idNdermarrjeRaportuese);
                if (idArtikullRaportuesi == 0)
                {
                    throw new MyException(rm.GetString("msgNukEkzistonArtMeKeteKodNeNdermRap", ci));
                }
                else
                    idArtRaportuesi = idArtikullRaportuesi;
            }
            int.TryParse(txtKaraktereTAC.Text, out txtKarakterTAC);

            int stokuMaxVfOne = 0;
            if (!String.IsNullOrEmpty(txtStokuMaxVfOne.Text))
            {
                bool konvertuar = Int32.TryParse(txtStokuMaxVfOne.Text, out stokuMaxVfOne);
                if (!konvertuar)
                    throw new MyException("Stoku max per VFONE duhet te jete numer i plote!");
            }
            DbCore.DbInventari.clsArtikulli artikullNew = new DbCore.DbInventari.clsArtikulli
                (int.Parse(hfId.Value), clsFunksione.ktheStringunPaHapesira(txtKodi.Text, true), clsFunksione.ktheStringunPaHapesira(txtPershkrimi.Text, false), clsFunksione.ktheStringunPaHapesira(txtPershkrimiAng.Text, false), txtKodiDoganor.Text,
                txtVendodhja.Text, kodifikim1, kodifikim2, btneKodifikimi1.Text, btneKodifikimi2.Text, txtOrigjina.Text, idNjesi1Artikulli, idNjesi2Artikulli, cmbNjesia1.Text,
                cmbNjesia2.Text, txtKoeficienti.Text != "" ? Convert.ToDecimal(txtKoeficienti.Value) : Convert.ToDecimal(0),
                    idfurnitori,
                    kf.KodKlientFurnitor,
                    txtPeshaBruto.Text != "" ? Convert.ToDecimal(txtPeshaBruto.Value) : Convert.ToDecimal(0),
                    txtPeshaNeto.Text != "" ? Convert.ToDecimal(txtPeshaNeto.Value) : Convert.ToDecimal(0),
                    cbDetajim.Checked,
                    klasa,
                    idskema,
                    cmbKlasa.Text,
                    btneSkema.Text,
                    idlloginv,
                    idllogble,
                    idllogshit,
                    idllogtret,
                 idLlogariPakesim,
                 idllogshpe,
                 idllogamor,
                 idllogrez,
                 idllogpakrez,
                 btneLlogInv.Text,
                 btneLlogBle.Text,
                 btneLlogShit.Text,
                 btnLlogPakesim.Text,
                 btneLlogTretet.Text,
                 btnLlogShpe.Text,
                 cmbLlogAmortizimi.Text,
                 btneLlogRez.Text,
                 btneLlogPakesimRez.Text,
                 txtMinimumi.Text != "" ? Convert.ToDecimal(txtMinimumi.Text) : Convert.ToDecimal(0),
                 txtMaximumi.Text != "" ? Convert.ToDecimal(txtMaximumi.Text) : Convert.ToDecimal(0),
                 Convert.ToInt32(cmbMetode.Value),
                 cmbMetode.Text,
                 Convert.ToInt32(cmbKMSH.Value),
                 Convert.ToInt32(cmbZevendesim.Value),
                 idPerdorues,
                 cbAktiv.Checked,
                 checkKontrollGjendje.Checked,
                 checkKontrollCmimi.Checked /*checkKontrollCmimi.Checked--nestila per momentin nuk shfaqet kjo fushe dhe duhet kur zgjedhim detajime sepse jep probleme ne llogaritjen e kostove*/,
                 checkKontrollGjendjeArtikulli.Checked,
                 klasa == 4 || klasa == 5 || klasa == 6 ? krijoArtikujtPerberes(true) : null,
                 krijoGjendjeArtSipasMag(true),
                 new DbCore.DbInventari.colFurnitoreArtikujsh() /*ruajFurnitoret(idNdermarrje)*/,
                 ruajArtikujZevendesues(idNdermarrje),
                 colvler,
                 null,
                 ruajDetajime(idNdermarrje, 1),
                 (cmbLloji.Text == DbCore.DbInventari.clsArtikulli.AfatShkurterLabel ? false : true),
                 idNdermarrje,
                 cmbNivelTvsh.Text != "" ? Convert.ToInt32(cmbNivelTvsh.Value) : 0,
                 kodtakse,
                 Convert.ToInt32(cmbKonfigurimi.Value),
                 cmbAutorizimiHf.Value,
                 shtim,
                 txtSasiNjesi.Text != "" ? Convert.ToDecimal(txtSasiNjesi.Text) : Convert.ToDecimal(1),
                 Convert.ToDecimal(txtScrap.Value),
                 cbProdhimMePorosi.Checked,
                 cmbKategoriDetajimi.Text != "" ? int.Parse(cmbKategoriDetajimi.Value.ToString()) : 0,
                 cmbKategoriDetajimi.Text,
                 false,
                 cmbKategoriDetajimi2.Text != "" ? Convert.ToInt32(cmbKategoriDetajimi2.Value) : 0,
                 cmbKategoriDetajimi2.Text,
                 ruajDetajime(idNdermarrje, 2),
                 checkKontrollGjendjeDetajim2.Checked,
                 idobjektiva,
                 cmbObjektiva.Text,
                 IdLlojGarancia,
                 txtGarancia.Text != "" ? decimal.Parse(txtGarancia.Text) : 0,
                 idmag, btnMagazina.Text != "" ? btnMagazina.Text : "",
                 cbRezervueshem.Checked,
                 cbPerTransferim.Checked,
                 cbLoan.Checked,
                 cbDhurate.Checked,
                 cmbAplikim.Text != "" ? Convert.ToInt32(cmbAplikim.Value) : 0,
                 txtPike.Text != "" ? decimal.Parse(txtPike.Text) : 0,
                 txtVlere.Text != "" ? decimal.Parse(txtVlere.Text) : 0,
                 txtKodVFOne.Text, cbMeSerial.Checked, cbShitshem.Checked,
                 cbMbetjeShitshem.Checked,
                 ruajTrupin(),
                 hfArkiva,
                 rm,
                 ci,
                 ruajKodbare(),
                 idArtRaportuesi,
                 cbPerPershore.Checked,
                 txtPershkrimiFurnitori.Text,
                 txtSiperfaqjaM2.Text,
                 txtNrKontrate.Text,
                 txtNrPasurie.Text,
                 txtZonaKadastrale.Text,
                 txtShasia.Text,
                 txtMarka.Text,
                 txtModeli.Text,
                 txtVitProdhimi.Text,
                 txtTeDhenaTeknika.Text, cbMeBarkodLogjik.Checked, txtSkemeBarkodi.Text, kodifikim3, btneKodifikimi3.Text, chkbAparatBazaar.Checked, txtKodOferte.Text, cbArtikullVjeter.Checked, krijoArtikujVfone(true), idformatseriali,
                 cbRezRivleresimi.Checked, ruajTrupinNormeAmortizim(), txtKarakterTAC, cbLLogaritKomision.Checked, idLlogKomision, btnLLogariKomisioni.Text, stokuMaxVfOne, txtKodiIBarit.Text, cbIRimbursueshem.Checked);
            return artikullNew;
        }

        protected DbCore.DbInventari.colArtikullVfone krijoArtikujVfone(bool ruaj)
        {
            //ruaj tregon nqs funksioni thirret nga funksioni krijoartikullin, apo nga callback te grides. 
            //Nqs thirret nga callback ath shtohen te colart edhe rreshtat bosh, perndryshe shtohen vetem rreshtat e plotesuar.

            colArtikullVfone colArt = new DbCore.DbInventari.colArtikullVfone();
            try
            {
                JavaScriptSerializer serializues = new JavaScriptSerializer();
                serializues.MaxJsonLength = 500000000;
                object[] artvfone = (object[])serializues.DeserializeObject(this.hfVfone.Value);
                //int i = 0;
                if (artvfone != null)
                    foreach (object oo in artvfone)
                    {
                        clsArtikullVfone artVf = new DbCore.DbInventari.clsArtikullVfone();
                        Dictionary<string, object> rresht = (Dictionary<string, object>)oo;

                        artVf.KodVfone = rresht["txtKodVfone"].ToString();

                        if (rresht["txtPike"].ToString() != null && rresht["txtPike"].ToString() != "null" && rresht["txtPike"].ToString() != string.Empty)
                            artVf.Pike = decimal.Parse(rresht["txtPike"].ToString());
                        if (rresht["txtVlere"].ToString() != null && rresht["txtVlere"].ToString() != "null" && rresht["txtVlere"].ToString() != string.Empty)
                            artVf.Vlere = decimal.Parse(rresht["txtVlere"].ToString());
                        if (artVf.KodVfone != "" && artVf.Pike == 0)
                            throw new Exception("Ju lutem jepni piket per kodin " + artVf.KodVfone + " te vodafone one!");

                        if (ruaj == true)
                        {
                            if ((rresht["txtKodVfone"].ToString() != string.Empty))
                                colArt.Add(artVf);
                        }
                        else colArt.Add(artVf);
                        //i++;

                    }
                return colArt;
            }


            catch (Exception ex)
            {
                throw new MyException(ex.Message);
            }

        }




        protected void gvBuxheti_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {//thirret kur grida ben callback
        }

        protected void gvBuxheti_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        { //sherben per te marre me vone vlerat e futura nga perdoruesi me ane te javascriptit

            if (e.RowType == GridViewRowType.Data)
            {
                GridViewDataColumn colGjendja = ((ASPxGridView)sender).Columns["Gjendja"] as GridViewDataColumn;
                GridViewDataColumn colBuxh1 = ((ASPxGridView)sender).Columns["Buxheti_1"] as GridViewDataColumn;
                GridViewDataColumn colBuxh2 = ((ASPxGridView)sender).Columns["Buxheti_2"] as GridViewDataColumn;
                GridViewDataColumn colDiff1 = ((ASPxGridView)sender).Columns["Diferenca_1"] as GridViewDataColumn;
                GridViewDataColumn colDiff2 = ((ASPxGridView)sender).Columns["Diferenca_2"] as GridViewDataColumn;
                ASPxLabel lblGjendja = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, colGjendja, "lbl") as ASPxLabel;
                ASPxTextBox txtBuxh1 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, colBuxh1, "txtBox") as ASPxTextBox;
                ASPxTextBox txtBuxh2 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, colBuxh2, "txtBox") as ASPxTextBox;
                ASPxTextBox lblDiff1 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, colDiff1, "txtBox") as ASPxTextBox;
                ASPxTextBox lblDiff2 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, colDiff2, "txtBox") as ASPxTextBox;

                if (txtBuxh1 != null && txtBuxh2 != null)
                {
                    lblGjendja.ClientInstanceName = "labelGjendja" + e.VisibleIndex;
                    txtBuxh1.ClientInstanceName = "textboxBuxh1" + e.VisibleIndex;
                    txtBuxh2.ClientInstanceName = "textboxBuxh2" + e.VisibleIndex;
                    lblDiff1.ClientInstanceName = "labelDiff1" + e.VisibleIndex;
                    lblDiff2.ClientInstanceName = "labelDiff2" + e.VisibleIndex;
                    if (e.VisibleIndex != 0)
                    {
                        txtBuxh1.ClientSideEvents.TextChanged = String.Format("function(s,e){{ ShtoBuxhet1(textboxBuxh1{0}, labelGjendja{0},labelDiff1{0},{0});}}", e.VisibleIndex);
                        txtBuxh2.ClientSideEvents.TextChanged = String.Format("function(s,e){{ShtoBuxhet2(textboxBuxh2{0}, labelGjendja{0},labelDiff2{0},{0});}}", e.VisibleIndex);
                    }
                    else
                    {
                        txtBuxh1.ClientSideEvents.TextChanged = String.Format("function(s,e){{ShtoTotal1(textboxBuxh1{0}, labelGjendja{0},labelDiff1{0});}}", e.VisibleIndex);
                        txtBuxh2.ClientSideEvents.TextChanged =	String.Format("function(s,e){{ShtoTotal2(textboxBuxh2{0}, labelGjendja{0},labelDiff2{0});}}", e.VisibleIndex);
                    }
                }
            }
        }

        protected void gvCmimet_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpNoRows"] = gvCmimet.VisibleRowCount;
            e.Properties["cpNoPage"] = gvCmimet.PageIndex;
        }

        protected void gvCmimet_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            DbCore.DbInventari.colCmimeArtikujsh col = new DbCore.DbInventari.colCmimeArtikujsh();
            JavaScriptSerializer serializusi = new JavaScriptSerializer();
            serializusi.MaxJsonLength = 500000000;
            if (hfArtikuj.Value != "")
            {
                object[] o = (object[])serializusi.DeserializeObject(hfArtikuj.Value);
                foreach (object oo in o)
                {
                    DbCore.DbInventari.clsCmimArtikulli cm = new DbCore.DbInventari.clsCmimArtikulli();
                    cm = cm.krijoCmim((Dictionary<string, object>)oo);
                    col.Add(cm);
                }
                gvCmimet.DataSource = col;
                gvCmimet.DataBind();
            }
        }

        protected void gvCmimet_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters.Split(';').Length == 2)
            {
                int idNdermarrje = (int)hfState["idNdermarrje"];
                int idPerdorues = (int)hfState["idPerdoruesi"];
                if (e.Parameters.Split(';')[1] == "modifiko")
                {
                    mbushListeCmimeshMod(idNdermarrje, int.Parse(e.Parameters.Split(';')[0]), idPerdorues);
                }
                else
                {
                    mbushListeCmimesh(idPerdorues, idNdermarrje);
                }
            }
        }

        protected void gvCmimet_DataBound(object sender, EventArgs e)
        {
           
            gvCmimet.KeyFieldName = "IdCmimArtikulli";
            gvCmimet.SettingsBehavior.AllowFocusedRow = true;
            
        }

        protected void gvCmimet_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            bool ugjet;
            if (e.RowType == DevExpress.Web.GridViewRowType.Data)
            {
                GridViewDataDateColumn col1 = ((ASPxGridView)sender).Columns["DateFillimi"] as GridViewDataDateColumn;
                GridViewDataDateColumn col2 = ((ASPxGridView)sender).Columns["DateMbarimi"] as GridViewDataDateColumn;     //jane hequr per momentin por do shtohen me vone

                GridViewDataDateColumn col21 = ((ASPxGridView)sender).Columns["KoheFillimi"] as GridViewDataDateColumn;
                GridViewDataDateColumn col22 = ((ASPxGridView)sender).Columns["KoheMbarimi"] as GridViewDataDateColumn;
                GridViewDataTextColumn col3 = ((ASPxGridView)sender).Columns["SasiMin"] as GridViewDataTextColumn;
                GridViewDataTextColumn col4 = ((ASPxGridView)sender).Columns["SasiMax"] as GridViewDataTextColumn;
                GridViewDataTextColumn col5 = ((ASPxGridView)sender).Columns["Cmimi"] as GridViewDataTextColumn;
                GridViewDataTextColumn col16 = ((ASPxGridView)sender).Columns["Norme"] as GridViewDataTextColumn;
                GridViewDataTextColumn col7 = ((ASPxGridView)sender).Columns["Cmimi2"] as GridViewDataTextColumn;
                GridViewDataTextColumn col15 = ((ASPxGridView)sender).Columns["CmimiTvsh"] as GridViewDataTextColumn;
                GridViewDataTextColumn col17 = ((ASPxGridView)sender).Columns["Cmimi2Tvsh"] as GridViewDataTextColumn;
                GridViewDataTextColumn col8 = ((ASPxGridView)sender).Columns["Kosto"] as GridViewDataTextColumn;
                GridViewDataTextColumn col10 = ((ASPxGridView)sender).Columns["UpdateBtn"] as GridViewDataTextColumn;
                GridViewDataTextColumn col6 = ((ASPxGridView)sender).Columns["Kursi"] as GridViewDataTextColumn;
                GridViewDataTextColumn col11 = ((ASPxGridView)sender).Columns["Formula"] as GridViewDataTextColumn;
                GridViewDataColumn col26 = gvCmimet.Columns["IdTvsh"] as GridViewDataColumn;
                ASPxDateEdit dt1 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col1, "cal") as ASPxDateEdit;
                ASPxDateEdit dt2 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col2, "cal") as ASPxDateEdit;
                ASPxTimeEdit dt11 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col21, "te") as ASPxTimeEdit;
                ASPxTimeEdit dt12 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col22, "te") as ASPxTimeEdit;
                ASPxTextBox txt3 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col3, "txtBox") as ASPxTextBox;
                ASPxTextBox txt4 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col4, "txtBox") as ASPxTextBox;
                ASPxTextBox txt5 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col5, "txtBox") as ASPxTextBox;
                ASPxLabel txt25 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col16, "lbl") as ASPxLabel;
                ASPxTextBox txt6 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col7, "txtBox") as ASPxTextBox;
                ASPxTextBox txt15 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col15, "txtBox") as ASPxTextBox;
                ASPxTextBox txt16 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col17, "txtBox") as ASPxTextBox;
                ASPxTextBox txt7 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col8, "txtBox") as ASPxTextBox;
                ASPxButton btn0 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col10, "btn") as ASPxButton;
                ASPxTextBox txt8 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col6, "txtBox") as ASPxTextBox;
                ASPxComboBox txt9 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col11, "cmbBox") as ASPxComboBox;
                ASPxComboBox txt26 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col26, "cmbBox") as ASPxComboBox;
                ugjet = false;
                bool teDrejtaModCmimi = (bool)hfTeDrejtaCmimi.Get("Modifikim");
                if (dt1 != null)
                {
                    dt1.ClientInstanceName = "DateFillimi" + e.VisibleIndex.ToString();
                    dt1.ClientSideEvents.TextChanged = "function(s,e){TextChangedDataFill(s,e," + e.VisibleIndex + ");}";
                    dt1.ClientEnabled = teDrejtaModCmimi;
                }
                if (dt2 != null)
                {
                    dt2.ClientInstanceName = "DateMbarimi" + e.VisibleIndex.ToString();
                    dt2.ClientSideEvents.TextChanged = "function(s,e){TextChangedDataMbar(s,e," + e.VisibleIndex + ");}";
                    dt2.ClientEnabled = teDrejtaModCmimi;
                }
                if (dt11 != null)
                {
                    dt11.ClientInstanceName = "KoheFillimi" + e.VisibleIndex.ToString();
                    dt11.ClientSideEvents.DateChanged = "function(s,e){TextChangedKohaFill(s,e," + e.VisibleIndex + ");}";
                    dt11.ClientEnabled = teDrejtaModCmimi;
                }
                if (dt12 != null)
                {
                    dt12.ClientInstanceName = "KoheMbarimi" + e.VisibleIndex.ToString();
                    dt12.ClientSideEvents.DateChanged = "function(s,e){TextChangedKohaMbar(s,e," + e.VisibleIndex + ");}";
                    dt12.ClientEnabled = teDrejtaModCmimi;
                }


                if (txt3 != null)
                {
                    txt3.ClientInstanceName = "SasiMin" + e.VisibleIndex.ToString();
                    txt3.ClientSideEvents.TextChanged = "function(s,e){TextChangedSasiMaxMin(SasiMin" + e.VisibleIndex.ToString() + ", " + e.VisibleIndex.ToString() + ", false, e);}";
                    txt3.ClientSideEvents.Init = "function(s,e){Utils.initTxtNumber(s,e)}";
                    txt3.ClientSideEvents.GotFocus = "function(s,e){Utils.gotFocusTxtNumer(s,e);}";
                    txt3.ClientSideEvents.LostFocus = "function(s,e){Utils.lostFocusTxtNumer(s,e);TextChangedSasiMaxMin(SasiMin" + e.VisibleIndex.ToString() + ", " + e.VisibleIndex.ToString() + ", false, e); }";
                    txt3.ClientEnabled = teDrejtaModCmimi;
                    if (e.VisibleIndex == ((ASPxGridView)sender).VisibleRowCount - 2)
                    {
                        if (ugjet)
                        {
                            temptxt = txt3;
                            ugjet = false;
                        }
                    }
                }
                if (txt4 != null)
                {
                    txt4.ClientInstanceName = "SasiMax" + e.VisibleIndex.ToString();
                    txt4.ClientSideEvents.TextChanged = "function(s,e){TextChangedSasiMaxMin(SasiMax" + e.VisibleIndex.ToString() + ", " + e.VisibleIndex.ToString() + ", true, e);}";
                    txt4.ClientSideEvents.Init = "function(s,e){Utils.initTxtNumber(s,e)}";
                    txt4.ClientSideEvents.GotFocus = "function(s,e){Utils.gotFocusTxtNumer(s,e);}";
                    txt4.ClientSideEvents.LostFocus = "function(s,e){Utils.lostFocusTxtNumer(s,e); TextChangedSasiMaxMin(SasiMin" + e.VisibleIndex.ToString() + ", " + e.VisibleIndex.ToString() + ", false, e);}";
                    txt4.ClientEnabled = teDrejtaModCmimi;
                    if (e.VisibleIndex == ((ASPxGridView)sender).VisibleRowCount - 2)
                    {
                        if (ugjet)
                        {
                            temptxt = txt4;
                            ugjet = false;
                        }
                    }
                }
                if (txt5 != null)
                {
                    txt5.ClientEnabled = teDrejtaModCmimi;
                    bool njesiTeVarura = false;
                    if (((ASPxGridView)sender).DataSource != null)
                        njesiTeVarura = (bool)((DbCore.DbInventari.colCmimeArtikujsh)((ASPxGridView)sender).DataSource)[e.VisibleIndex].NjesiTeVarura;
                    txt5.ClientInstanceName = "Cmimi" + e.VisibleIndex.ToString();
                    txt5.ClientSideEvents.TextChanged = "function(s,e){TextChangedCmimet(s,e," + e.VisibleIndex + ", '" + njesiTeVarura.ToString() + "', 'Cmimi1');}";
                    txt5.ClientSideEvents.Init = "function(s,e){Utils.initTxtNumber(s,e)}";
                    txt5.ClientSideEvents.GotFocus = "function(s,e){Utils.gotFocusTxtNumer(s,e);}";
                    txt5.ClientSideEvents.LostFocus = "function(s,e){Utils.lostFocusTxtNumer(s,e)}";

                }

                if (txt25 != null)
                {
                    txt25.ClientInstanceName = "Norme" + e.VisibleIndex.ToString();

                    txt25.ClientSideEvents.Init = "function(s,e){Utils.initTxtNumber(s,e)}";

                }
                if (txt6 != null)
                {
                    txt6.ClientEnabled = teDrejtaModCmimi;
                    bool njesiTeVarura = false;
                    if (((ASPxGridView)sender).DataSource != null)
                        njesiTeVarura = (bool)((DbCore.DbInventari.colCmimeArtikujsh)((ASPxGridView)sender).DataSource)[e.VisibleIndex].NjesiTeVarura;
                    txt6.ClientInstanceName = "Cmimi2" + e.VisibleIndex.ToString();
                    txt6.ClientSideEvents.TextChanged = "function(s,e){TextChangedCmimet(s,e," + e.VisibleIndex + ", '" + njesiTeVarura.ToString() + "', 'Cmimi2');}";
                    txt6.ClientSideEvents.Init = "function(s,e){Utils.initTxtNumber(s,e)}";
                    txt6.ClientSideEvents.GotFocus = "function(s,e){Utils.gotFocusTxtNumer(s,e);}";
                    txt6.ClientSideEvents.LostFocus = "function(s,e){Utils.lostFocusTxtNumer(s,e)}";
                }
                if (txt15 != null)
                {
                    txt15.ClientEnabled = teDrejtaModCmimi;
                    bool njesiTeVarura = false;
                    if (((ASPxGridView)sender).DataSource != null)
                        njesiTeVarura = (bool)((DbCore.DbInventari.colCmimeArtikujsh)((ASPxGridView)sender).DataSource)[e.VisibleIndex].NjesiTeVarura;
                    txt15.ClientInstanceName = "CmimiTvsh" + e.VisibleIndex.ToString();
                    txt15.ClientSideEvents.TextChanged = "function(s,e){TextChangedCmimet(s,e," + e.VisibleIndex + ", '" + njesiTeVarura.ToString() + "', 'Cmimi1MeTvsh');}";
                    txt15.ClientSideEvents.Init = "function(s,e){Utils.initTxtNumber(s,e)}";
                    txt15.ClientSideEvents.GotFocus = "function(s,e){Utils.gotFocusTxtNumer(s,e);}";
                    txt15.ClientSideEvents.LostFocus = "function(s,e){Utils.lostFocusTxtNumer(s,e)}";

                }
                if (txt26 != null)
                {
                    txt26.ClientInstanceName = "IdTvsh" + e.VisibleIndex.ToString();
                    txt26.TextFormatString = "{0}";
                    txt26.Columns.Add(new ListBoxColumn("KodTaksa"));
                    txt26.Columns.Add(new ListBoxColumn("NormaPerqindje"));
                    var colTaksa = new colTaksa((int)hfState["idNdermarrje"], DbCore.DbRegjistrim.LlojTakse.Nivel_Tvsh, (int)hfState["idPerdoruesi"]);
                    colTaksa.Insert(0, clsTaksa.krijoTaksePaTVSH());
                    txt26.DataSource = colTaksa;
                    txt26.TextField = "KodTaksa";
                    txt26.ValueField = "IdTaksa";
                    txt26.DataBind();
                    txt26.ClientSideEvents.TextChanged = "function(s,e){TextChangedIdTvsh(s,e," + e.VisibleIndex + ");}";
                    txt26.ClientSideEvents.Init = "function(s,e){Utils.initTxtNumber(s,e)}";
                    txt26.ClientSideEvents.GotFocus = "function(s,e){Utils.gotFocusTxtNumer(s,e);}";
                    txt26.ClientSideEvents.LostFocus = "function(s,e){Utils.lostFocusTxtNumer(s,e)}";
                    txt26.ClientEnabled = teDrejtaModCmimi;
                }

                if (txt16 != null)
                {
                    txt16.ClientEnabled = teDrejtaModCmimi;
                    bool njesiTeVarura = false;
                    if (((ASPxGridView)sender).DataSource != null)
                        njesiTeVarura = (bool)((DbCore.DbInventari.colCmimeArtikujsh)((ASPxGridView)sender).DataSource)[e.VisibleIndex].NjesiTeVarura;
                    txt16.ClientInstanceName = "Cmimi2Tvsh" + e.VisibleIndex.ToString();
                    txt16.ClientSideEvents.TextChanged = "function(s,e){TextChangedCmimet(s,e," + e.VisibleIndex + ", '" + njesiTeVarura.ToString() + "', 'Cmimi2MeTvsh');}";
                    txt16.ClientSideEvents.Init = "function(s,e){Utils.initTxtNumber(s,e)}";
                    txt16.ClientSideEvents.GotFocus = "function(s,e){Utils.gotFocusTxtNumer(s,e);}";
                    txt16.ClientSideEvents.LostFocus = "function(s,e){Utils.lostFocusTxtNumer(s,e)}";

                }

                if (txt7 != null)
                {
                    txt7.ClientEnabled = false;
                    txt7.ClientInstanceName = "Kosto" + e.VisibleIndex.ToString();
                    txt7.ClientSideEvents.Init = "function(s,e){Utils.initTxtNumber(s,e)}";
                    txt7.ClientSideEvents.GotFocus = "function(s,e){Utils.gotFocusTxtNumer(s,e);}";
                    txt7.ClientSideEvents.LostFocus = "function(s,e){Utils.lostFocusTxtNumer(s,e)}";
                }
                if (btn0 != null)
                {
                    btn0.ClientInstanceName = "UpdateBtn" + e.VisibleIndex;
                    btn0.ClientSideEvents.Click = "function(s,e){ClickUpdateBtn(s,e," + e.VisibleIndex + ");}";
                    btn0.ClientEnabled = teDrejtaModCmimi;
                }
                if (txt8 != null)
                {
                    txt8.ClientInstanceName = "Kursi" + e.VisibleIndex.ToString();
                    txt8.ClientEnabled = false;
                    txt8.ClientSideEvents.Init = "function(s,e){Utils.initTxtNumber(s,e)}";
                    txt8.ClientSideEvents.GotFocus = "function(s,e){Utils.gotFocusTxtNumer(s,e);}";
                    txt8.ClientSideEvents.LostFocus = "function(s,e){Utils.lostFocusTxtNumer(s,e)}";
                }
                if (txt9 != null)
                {
                    txt9.DropDownButton.Visible = false;
                    txt9.Buttons.Add();
                    txt9.TextFormatString = "{0},{1}";
                    txt9.Columns.Add(new ListBoxColumn("KodFormula"));
                    txt9.Columns.Add(new ListBoxColumn("PershkrimFormula"));
                    DataTable dt = DbCore.DbInventari.colFormulat.merrFormulatSipasNdermarrjes((int)hfState["idNdermarrje"]);
                    txt9.DataSource = dt;
                    txt9.TextField = "PershkrimFormula";
                    txt9.ValueField = "IdFormula";
                    txt9.DataBind();
                    dt.Dispose();
                    txt9.ClientInstanceName = "txtFormula" + e.VisibleIndex;
                    txt9.ClientSideEvents.SelectedIndexChanged = "function(s,e){SelectedIndexChangedFormula('txtFormula" + e.VisibleIndex.ToString() + "'," + e.VisibleIndex.ToString() + ");}";
                    txt9.ClientSideEvents.TextChanged = "function(s,e){TextChangedFormula('txtFormula" + e.VisibleIndex.ToString() + "'," + e.VisibleIndex.ToString() + ");}";
                    txt9.ClientSideEvents.KeyPress = String.Format("function(s,e){{var code = _getKeyCode(e.htmlEvent); KeyPressFormula(code,txtFormula{0},{0}); }}", e.VisibleIndex);
                    txt9.ClientSideEvents.LostFocus = "function(s,e){LostFocusFormula('txtFormula" + e.VisibleIndex.ToString() + "'," + e.VisibleIndex.ToString() + ");}";
                    txt9.ClientSideEvents.ButtonClick = "function(s,e){ButtonClickFormula('txtFormula" + e.VisibleIndex.ToString() + "'," + e.VisibleIndex.ToString() + ")}";
                    txt9.DropDownStyle = DropDownStyle.DropDown;
                    if (!teDrejtaModCmimi)
                        txt9.ClientEnabled = false;
                    else
                    {
                        if (Request.QueryString["llojiart"] == "afatshkurter")
                        {
                            if (hfKushtet.Get("FPJM").ToString() == "Po")
                            {
                                txt9.ClientEnabled = true;
                                //txt9.Enabled = true;
                            }
                            else if (hfKushtet.Get("FPJM").ToString() == "Jo")
                            {
                                txt9.ClientEnabled = false;
                                //txt9.Enabled = false;
                            }
                            if (hfKushtet.Get("formula") != null)
                            {
                                string formula = hfKushtet.Get("formula").ToString();
                                if (formula != "")
                                    txt9.Text = formula;
                                else txt9.Text = "";
                            }
                            else txt9.Text = "";
                        }
                    }
                    if (e.VisibleIndex == ((ASPxGridView)sender).VisibleRowCount - 1 - nrRreshtatsh)
                    {
                        if (ugjet)
                        {
                            temptxtNormal = txt9;
                            ugjet = false;
                        }
                        //else if (koloneFocus == "Formula")
                        //{
                        //    ugjet = true;
                        //}
                    }
                }
            }

            if (temptxt != null)
            {
                temptxt.Focus();
            }
            else if (tempcombo != null)
            {
                tempcombo.Focus();
            }
        }



        protected void ASPxGridView_Artikull_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string idkomponente = "";
            string kodkonfigurimi = "";
            int idNdermarrje = (int)hfState["idNdermarrje"];
            int idPerdoruesi = (int)hfState["idPerdoruesi"];
            string[] arr = e.Parameters.Split(';');
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            string postStringTvsh = (string)hfState.Get(clsArtikulli.postStringTvsh);
            if (arr.Length == 3)
            {
                kodkonfigurimi = arr[1];
                idkomponente = arr[0];
                if (e.Parameters.Split(';')[0] == "kerko")
                {
                    mbushGridArtikujshNgaDB(idPerdoruesi, idNdermarrje, cbKosto.Checked, cbGjendje.Checked, cbCmime.Checked, cbCmimeMeTvsh.Checked, (string)hfState.Get(clsArtikulli.postStringTvsh));
                    konfiguroGride(idNdermarrje, cmbKonfigurimi.Text, 402, cbKosto.Checked, cbGjendje.Checked, cultinf, rm, null, idPerdoruesi, cbCmime.Checked, cbCmimeMeTvsh.Checked, postStringTvsh);
                    GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "ASPxGridView_Artikull", ASPxGridView_Artikull, cmbKonfigurimi.Text, Convert.ToString(402), (int)hfState["idGjuha"]);
                    ASPxGridView_Artikull.Columns["Gjendje"].Visible = cbGjendje.Checked;
                    ASPxGridView_Artikull.Columns["Kosto"].Visible = cbKosto.Checked;
                }
                else
                {
                    if (arr[2] == "")
                        if (Request.QueryString["llojiart"] == "afatshkurter")
                        {
                            ASPxGridView_Artikull.FilterExpression = "[LlojiArt]=false";
                        }
                        else
                        {
                            ASPxGridView_Artikull.FilterExpression = "[LlojiArt]=true";
                        }

                    else
                    {
                        DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                        DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka((int)hfState["idGjuha"], "ASPxGridView_Artikull", komponente, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
                        filtra.mbushFilterPerGrideSipasKodit(arr[2], idNdermarrje, koka.IdGridaKoka);
                        if (filtra.FiltraKodi != null)
                        {
                            ASPxGridView_Artikull.FilterExpression = filtra.FiltraVlera;
                            GridUtil.renditGriden(filtra.KoloneRenditje, ASPxGridView_Artikull);
                            hfStatusi.Value = "true";
                        }
                        else
                            hfStatusi.Value = "false";
                    }
                }
            }
            else if (arr.Length == 2)//nese eshte zgjedhur nje konfigurim tek combo e konfigurimeve
            {
                kodkonfigurimi = arr[1];
                idkomponente = arr[0];
                konfiguroGride(idNdermarrje, kodkonfigurimi, Convert.ToInt32(idkomponente), cbKosto.Checked, cbGjendje.Checked, cultinf, rm, null, idPerdoruesi, cbCmime.Checked, cbCmimeMeTvsh.Checked, postStringTvsh);
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "ASPxGridView_Artikull", ASPxGridView_Artikull, cmbKonfigurimi.Text, Convert.ToString(402), (int)hfState["idGjuha"]);
            }
            else
            {
                idkomponente = e.Parameters;
            }
            ASPxGridView_Artikull.Selection.UnselectAll();
        }

        #region filtrimi
        protected void btneSkema_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("btneSkema"))
                {
                    hfSkemaKlasa.Value = ConfigureAspxComboBox.mbushComboSkemaKontabilitetiArtikulli(IdNdermarrja, btneSkema, Request.QueryString["llojiart"], e.Value, Convert.ToInt32(cmbKlasa.Value));
                }
            }

        }
        protected void btneSkema_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("btneSkema"))
                {
                    hfSkemaKlasa.Value = ConfigureAspxComboBox.mbushComboSkemaKontabilitetiArtikulli((int)hfState["idNdermarrje"], btneSkema, Request.QueryString["llojiart"], e.Filter, Convert.ToInt32(cmbKlasa.Value));
                }
            }
        }

        protected void btneLlogInv_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("btneLlogInv"))
                {
                    ConfigureAspxComboBox.mbushComboLlogariaPaKolona(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), btneLlogInv, e);
                }
            }
        }
        protected void btneLlogInv_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("btneLlogInv"))
                {
                    ConfigureAspxComboBox.mbushComboLlogariaPaKolona(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), btneLlogInv, e);
                }
            }
        }


        protected void btneLlogBle_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("btneLlogBle"))
                {
                    ConfigureAspxComboBox.mbushComboLlogariaPaKolona(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), btneLlogBle, e);
                }
            }
        }
        protected void btneLlogBle_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("btneLlogBle"))
                {
                    ConfigureAspxComboBox.mbushComboLlogariaPaKolona(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), btneLlogBle, e);
                }
            }
        }

        protected void btneLlogShit_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("btneLlogShit"))
                {
                    ConfigureAspxComboBox.mbushComboLlogariaPaKolona(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), btneLlogShit, e);
                }
            }
        }
        protected void btneLlogShit_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("btneLlogShit"))
                {
                    ConfigureAspxComboBox.mbushComboLlogariaPaKolona(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), btneLlogShit, e);
                }
            }
        }

        protected void btnLlogPakesim_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("btnLlogPakesim"))
                {
                    ConfigureAspxComboBox.mbushComboLlogariaPaKolona(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), btnLlogPakesim, e);
                }
            }
        }
        protected void btnLlogPakesim_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("btnLlogPakesim"))
                {
                    ConfigureAspxComboBox.mbushComboLlogariaPaKolona(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), btnLlogPakesim, e);
                }
            }
        }

        protected void btneLlogTretet_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("btneLlogTretet"))
                {
                    ConfigureAspxComboBox.mbushComboLlogariaPaKolona(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), btneLlogTretet, e);
                }
            }
        }
        protected void btneLlogTretet_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("btneLlogTretet"))
                {
                    ConfigureAspxComboBox.mbushComboLlogariaPaKolona(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), btneLlogTretet, e);
                }
            }
        }

        protected void btnLlogShpe_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("btnLlogShpe"))
                {
                    ConfigureAspxComboBox.mbushComboLlogariaPaKolona(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), btnLlogShpe, e);
                }
            }
        }
        protected void btnLlogShpe_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("btnLlogShpe"))
                {
                    ConfigureAspxComboBox.mbushComboLlogariaPaKolona(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), btnLlogShpe, e);
                }
            }
        }

        protected void btnLLogariKomisioni_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("btnLLogariKomisioni"))
                {
                    ConfigureAspxComboBox.mbushComboLlogariaPaKolona(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), btnLLogariKomisioni, e);
                }
            }
        }

        protected void btnLLogariKomisioni_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("btnLLogariKomisioni"))
                {
                    ConfigureAspxComboBox.mbushComboLlogariaPaKolona(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), btnLLogariKomisioni, e);
                }
            }
        }
        protected void cmbLlogAmortizimi_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("cmbLlogAmortizimi"))
                {
                    ConfigureAspxComboBox.mbushComboLlogariaPaKolona(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), cmbLlogAmortizimi, e);
                }
            }
        }
        protected void cmbLlogAmortizimi_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("cmbLlogAmortizimi"))
                {
                    ConfigureAspxComboBox.mbushComboLlogariaPaKolona(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), cmbLlogAmortizimi, e);
                }
            }
        }

        protected void txtFurnitori_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("txtFurnitori"))
                {
                    int value = 0;
                    if (e.Value == null || !Int32.TryParse(e.Value.ToString(), out value))
                        return;
                    ConfigureAspxComboBox.KonfiguroComboBoxKlientFurnitoriById((int)hfState["idPerdoruesi"], (int)hfState["idNdermarrje"], (ASPxComboBox)source, value);
                }
            }
        }

        protected void txtFurnitori_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("txtFurnitori"))
                {
                    ConfigureAspxComboBox.KonfiguroComboBoxKlientFurnitor(e.Filter, e.BeginIndex + 1, e.EndIndex + 1, (int)hfState["idPerdoruesi"], (int)hfState["idNdermarrje"], txtFurnitori, 2);
                }
            }
        }

        protected void cmbNivelTvsh_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("cmbNivelTvsh"))
                {
                    ConfigureAspxComboBox.KonfiguroComboBoxTaksat((int)hfState["idPerdoruesi"], (int)hfState["idNdermarrje"], cmbNivelTvsh, DbCore.DbRegjistrim.LlojTakse.Nivel_Tvsh, false);
                }
            }
        }

        protected void cmbNjesia1_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("cmbNjesia1"))
                {
                    ConfigureAspxComboBox.mbushComboNjesi((int)hfState["idNdermarrje"], cmbNjesia1);
                }
            }
        }

        protected void cmbNjesia2_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("cmbNjesia2"))
                {
                    ConfigureAspxComboBox.mbushComboNjesi((int)hfState["idNdermarrje"], cmbNjesia2);
                }
            }
        }

       

        protected void cmbGarancia_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("cmbGarancia"))
                {
                    ConfigureAspxComboBox.mbushComboGarancite(cmbGarancia);
                }
            }
        }

        protected void cmbObjektiva_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("cmbObjektiva"))
                ConfigureAspxComboBox.KonfiguroComboBoxObjektivaKosto(cmbObjektiva, IdNdermarrja);
        }

        protected void btneLlogRez_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("btneLlogRez"))
                {
                    ConfigureAspxComboBox.mbushComboLlogariaPaKolona(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), btneLlogRez, e);
                }
            }
        }
        protected void btneLlogRez_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("btneLlogRez"))
                {
                    ConfigureAspxComboBox.mbushComboLlogariaPaKolona(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), btneLlogRez, e);
                }
            }
        }

        protected void btneLlogPakesimRez_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("btneLlogPakesimRez"))
                {
                    ConfigureAspxComboBox.mbushComboLlogariaPaKolona(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), btneLlogPakesimRez, e);
                }
            }
        }
        protected void btneLlogPakesimRez_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("btneLlogPakesimRez"))
                {
                    ConfigureAspxComboBox.mbushComboLlogariaPaKolona(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), btneLlogPakesimRez, e);
                }
            }
        }


        #endregion filtrimi

        protected void ASPxGridView_Artikull_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if (e.Column.FieldName == "LlojiArt")
                return;
            if (e.Column.FieldName == "Klasa" ||
              e.Column.FieldName == "IdSkemaKontabilitetiArtikulli" || e.Column.FieldName == "Kodifikimi1Artikulli" ||
                e.Column.FieldName == "Njesi1Artikulli" || e.Column.FieldName == "Njesi2Artikulli" || e.Column.FieldName == "AplikimDhurate" || e.Column.FieldName == "IdLlojGarancia" || e.Column.FieldName == "MetodeKostojeArtikulli")
            {
                double vlera = 0;
                bool parse = Double.TryParse(e.Value.ToString(), out vlera);

                if ((parse && vlera == 0) || !parse)
                    e.Criteria = null;
            }
        }

        protected void cmbNivelTvsh_PreRender(object sender, EventArgs e)
        {
          //  cmbNivelTvsh.Items[0].Text = "";
        }

      

        ASPxComboBox temptxtNormal = null;
        private int nrRreshtatsh = 5;

        protected void btn_Click(object sender, EventArgs e)
        {

        }

        protected void RuajKolona_Click(object sender, EventArgs e)
        {
            int idfiltri = 0;
            int idkonf = DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(cmbKonfigurimi.Text.Split(';')[0], (int)hfState["idNdermarrje"]);
            DbCore.clsMesazh mesazh = GridUtil.ruajFiltra((int)hfState["idNdermarrje"], (int)hfState["idPerdoruesi"], (int)hfState["idGjuha"], "ASPxGridView_Artikull", komponente, "FilterDefault", ASPxGridView_Artikull.FilterExpression, ASPxGridView_Artikull, "KodArtikulli", idkonf, out idfiltri);//ruan filtrin e zgjedhur tek filtrat
            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                return;
            }
            mesazh = GridUtil.ruajkonfigurimgride(ASPxGridView_Artikull, cmbKonfigurimi.Text, (int)hfState["idNdermarrje"], (int)hfState["idPerdoruesi"], 402, idfiltri, (int)hfState["idViti"], DbCore.mySessionObjects.ktheCultureInfo(Session), (int)hfState["idGjuha"]);/// ruan konfigurimin e grides dhe filtrin

            clsToolbarConfig.mbushComboBoxFiltra((int)hfState["idGjuha"], (int)hfState["idNdermarrje"], "ASPxGridView_Artikull", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);

            percaktoTemplateMenu((int)hfState["idGjuha"], (int)hfState["idViti"], (int)hfState["idPerdoruesi"], (int)hfState["idNdermarrje"], ASPxMenu1);

            if (mesazh.Status)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
        }

        private void konfiguroGrideArtikujPerberes(string gridaEmri)
        {
            DbCore.DbAdmin.colGridaTrupi vlere;
            DbCore.DbShare.clsKonfigurimAmbjenti konfigurimi = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konfigurimi.mbushKonfigDefaultKomponentes(402, -1);
            //DbCore.DbShare.colKonfigurimAmbjenti konfigurimi = share.ktheKonfigDefaultKomponentes(518, -1);
            vlere = GridUtil.percaktoVisibleColumnsSipasKonfigurimitPerClientSide2(gridaEmri, "Shto_Artikull.aspx?llojiart=afatshkurter", 1, (int)hfState["idGjuha"]);
            JavaScriptSerializer serializusi = new JavaScriptSerializer();
            if (gridaEmri.Equals("gvArtPerberes"))
                this.HfGridCol.Value = serializusi.Serialize(vlere);
            else if (gridaEmri.Equals("gvGjendjeArt"))
                this.HfGridColGjendjeArt.Value = serializusi.Serialize(vlere);
            else if (gridaEmri.Equals("gvArtVfone"))
           
            HfGridColV.Value = serializusi.Serialize(vlere);
        }
        protected DbCore.DbInventari.colGjendjeArtikulli krijoGjendjeArtSipasMag(bool ruaj)
        {
            DbCore.DbInventari.colGjendjeArtikulli colGjendjeArtikulli = new DbCore.DbInventari.colGjendjeArtikulli();
            try
            {
                JavaScriptSerializer serializues = new JavaScriptSerializer();
                serializues.MaxJsonLength = 500000000;
                object[] gjendjeArt = (object[])serializues.DeserializeObject(hfGjendjeArtikulli.Value);
                if (gjendjeArt != null)
                {
                    foreach (object oo in gjendjeArt)
                    {
                        DbCore.DbInventari.clsGjendjeArtikulli gjendjaArtikulliMag = new DbCore.DbInventari.clsGjendjeArtikulli();
                        Dictionary<string, object> rresht = (Dictionary<string, object>)oo;
                        string gjendjaMin = Convert.ToString(rresht["txtGjendjaMin"]);
                        if (!String.IsNullOrEmpty(gjendjaMin) && !gjendjaMin.Equals("null"))
                            gjendjaArtikulliMag.GjendjaMin = Convert.ToDouble(gjendjaMin);
                        string gjendjaMax = Convert.ToString(rresht["txtGjendjaMax"]);
                        if (!String.IsNullOrEmpty(gjendjaMax) && !gjendjaMax.Equals("null"))
                            gjendjaArtikulliMag.GjendjaMax = Convert.ToDouble(gjendjaMax);
                        string magazina = Convert.ToString(rresht["txtMagazina"]);
                        if (!String.IsNullOrEmpty(magazina) && !magazina.Equals("null"))
                            gjendjaArtikulliMag.IdMagazina = DbCore.DbRegjistrim.clsNjesiAdministrative.ktheIdMagazine(magazina, (int)hfState["idNdermarrje"]);
                        gjendjaArtikulliMag.IdArtikulli = Convert.ToInt32(hfId.Value);

                        if (!(gjendjaArtikulliMag.IdMagazina > 0) || (String.IsNullOrEmpty(gjendjaMin) && gjendjaMin.Equals("null") && String.IsNullOrEmpty(gjendjaMax) && gjendjaMax.Equals("null")))
                            continue;

                        colGjendjeArtikulli.Add(gjendjaArtikulliMag);
                    }
                }
                return colGjendjeArtikulli;
            }
            catch (Exception err)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
                return colGjendjeArtikulli;
            }
        }

        protected DbCore.DbInventari.colArtikulliPerberes krijoArtikujtPerberes(bool ruaj)
        {
            //ruaj tregon nqs funksioni thirret nga funksioni krijoartikullin, apo nga callback te grides. 
            //Nqs thirret nga callback ath shtohen te colart edhe rreshtat bosh, perndryshe shtohen vetem rreshtat e plotesuar.

            DbCore.DbInventari.colArtikulliPerberes colArt = new DbCore.DbInventari.colArtikulliPerberes();
            try
            {
                JavaScriptSerializer serializues = new JavaScriptSerializer();
                serializues.MaxJsonLength = 500000000;
                object[] artPerb = (object[])serializues.DeserializeObject(hfArtikujtPerberes.Value);
                //int i = 0;
                if (artPerb != null)
                {
                    foreach (object oo in artPerb)
                    {
                        DbCore.DbInventari.clsArtikulliPerberes artPerberes = new DbCore.DbInventari.clsArtikulliPerberes();
                        Dictionary<string, object> rresht = (Dictionary<string, object>)oo;

                        string lloji = rresht["cmbLloji"].ToString();
                        if (lloji == "Artikull") artPerberes.Lloji = 1; else artPerberes.Lloji = 2;
                        if (rresht["txtKoeficienti"].ToString() != null && rresht["txtKoeficienti"].ToString() != "null" && rresht["txtKoeficienti"].ToString() != "")
                            artPerberes.Koeficienti = decimal.Parse(rresht["txtKoeficienti"].ToString());
                        if (rresht["txtFiro"].ToString() != null && rresht["txtFiro"].ToString() != "null" && rresht["txtFiro"].ToString() != "")
                            artPerberes.Scrap = decimal.Parse(rresht["txtFiro"].ToString());
                        artPerberes.GjithmoneNgaStoku = false;//per momentin nuk perdoret
                        artPerberes.Njesia = rresht["txtNjesia"].ToString();
                        artPerberes.DtNdryshimi = dteDateAkt.Date;
                        if (artPerberes.Lloji == 1)
                        {
                            DbCore.DbInventari.clsArtikulli art = new DbCore.DbInventari.clsArtikulli();
                            if (rresht["txtKodi"] == null || rresht["txtKodi"].ToString() == "") continue;
                            art.merrSipasKodArtikullit(rresht["txtKodi"].ToString(), (int)hfState["idNdermarrje"]);
                            artPerberes.IdLidheseArt = art.IdArtikulli;
                            artPerberes.IdLidhese = art.IdArtikulli;
                        }
                        else if (artPerberes.Lloji == 2)
                        {

                            DbCore.DbProdhimi.clsAktiviteteKoka aktivitet = new DbCore.DbProdhimi.clsAktiviteteKoka(rresht["txtKodi"].ToString(), (int)hfState["idNdermarrje"]);
                            artPerberes.IdLidheseAkt = aktivitet.IdKoka;
                            artPerberes.IdLidhese = aktivitet.IdKoka;
                        }

                        if (ruaj == true)
                        {
                            if ((rresht["txtKodi"].ToString() != ""))
                                colArt.Add(artPerberes);
                        }
                        else colArt.Add(artPerberes);
                        //i++;

                    }
                }
                return colArt;
            }
            catch (Exception err)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
                return colArt;
            }
        }

        private bool nivelTVShIPranueshem()
        {
            if (!tvshEdetyrueshme())
                return true;

            return !(cmbNivelTvsh.Value == null || Convert.ToInt32(cmbNivelTvsh.Value.ToString()) == -1);
        }

        private bool tvshEdetyrueshme()
        {
            
            DbCore.DbShare.clsAtributeTrupi atribute = new DbCore.DbShare.clsAtributeTrupi();
            if (atribute.mbushAtributSipasKompKonfDheKontrollit(0, Convert.ToInt32(cmbKonfigurimi.Value.ToString()), "cmbNivelTvsh", 402))
                return atribute.Detyrueshme;

            return false;
        }

        #region grida e amortizimit
        private void konfiguroVleraFillestareNorma()
        {
            colAseteNormaAmortizimi col = new colAseteNormaAmortizimi();
            int id = 0;
            if (hfShtimModifikim.Value == "modifikim" || hfShtimModifikim.Value == "klonim")
            {
                id = int.Parse(hfId.Value.ToString());
                col.merrArtikullNormaAmortizimiTeGjitha(id, (int)hfState["idNdermarrje"],dteDateAk2.Date);
            }
            else col.merrArtikullNormaAmortizimiFillestare((int)hfState["idNdermarrje"],dteDateAk2.Date);
            gvAmortizimi.DataSource = col;
            gvAmortizimi.DataBind();
        }

        private void konfiguroGrideNorma(int idndermarje)
        {
            KonfigurimComboGride.ShtoStandart(gvAmortizimi, idndermarje, Session, komponente, guidString, "IdStandartAmortizimi");

          
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            GridUtil.konfiguroGrideListeEvogelPaTheme(gvAmortizimi, "IdLidhjeArtikullLlojAmort");
            gvAmortizimi.Settings.ShowFilterRow = false;
            gvAmortizimi.SettingsBehavior.AllowSort = false;
            gvAmortizimi.SettingsBehavior.AllowGroup = false;
            gvAmortizimi.Settings.ShowFilterRowMenu = false;
            gvAmortizimi.Settings.ShowHeaderFilterButton = false;
            gvAmortizimi.SettingsEditing.Mode = GridViewEditingMode.Inline;
            gvAmortizimi.Settings.ShowFilterBar = GridViewStatusBarMode.Hidden;
            gvAmortizimi.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
            percaktoTemplate();

        }
        private void percaktoTemplate()
        {
            GridViewDataColumn col7 = gvAmortizimi.Columns["IdLlojAmortizimi"] as GridViewDataColumn;
            col7.DataItemTemplate = new MyComboTemplate();
            GridViewDataColumn col17 = gvAmortizimi.Columns["NormeMagazine"] as GridViewDataColumn;
            col17.DataItemTemplate = new MyComboTemplate();
            GridViewDataColumn col10 = gvAmortizimi.Columns["Norme"] as GridViewDataColumn;
            col10.DataItemTemplate = new MyDoubleTemplate(false, 2, "0");
            GridViewDataColumn col1 = gvAmortizimi.Columns["IdStandartAmortizimi"] as GridViewDataColumn;
            col1.DataItemTemplate = new MyLabelTemplate();
        }   

        private DbCore.DbAsete.colAseteNormaAmortizimi ruajTrupin()
        {
            JavaScriptSerializer serializusi = new JavaScriptSerializer();
            object[] dokumenti = (object[])serializusi.DeserializeObject(gridDataObject.Value);
            DbCore.DbAsete.colAseteNormaAmortizimi trupat = new DbCore.DbAsete.colAseteNormaAmortizimi();
            int idNdermarrje = (int)hfState["idNdermarrje"];
            for (int i = 0; i < dokumenti.Length; i++)
            {
                DbCore.DbAsete.clsAseteNormaAmortizimi trup = new DbCore.DbAsete.clsAseteNormaAmortizimi(idNdermarrje, (Dictionary<string, object>)dokumenti[i], dteDateAk2.Date.Date);
                trupat.Add(trup);
            }
            return trupat;
        }


        protected void gvAmortizimi_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters != "")
            {
                colAseteNormaAmortizimi col = new colAseteNormaAmortizimi();
                int id = 0;

                int.TryParse(e.Parameters, out id);
                col.ktheArtikullNormaAmortizimiSipasIdKodifikimit(id, (int)hfState["idNdermarrje"],dteDateAk2.Date);

                gvAmortizimi.DataSource = col;
                gvAmortizimi.DataBind();
            }
            else konfiguroVleraFillestareNorma();
            konfiguroGrideNorma(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        }

        protected void gvAmortizimi_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gvAmortizimi.PageIndex;
            e.Properties["cpPageRow"] = gvAmortizimi.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gvAmortizimi.VisibleRowCount;
        }

        protected void gvAmortizimi_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {

            if (e.RowType == DevExpress.Web.GridViewRowType.Data)
            {
                GridViewDataColumn col0 = ((ASPxGridView)sender).Columns["IdLlojAmortizimi"] as GridViewDataColumn;
                ASPxComboBox btn0 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col0, "cmbBox") as ASPxComboBox;
                GridViewDataColumn col1 = ((ASPxGridView)sender).Columns["NormeMagazine"] as GridViewDataColumn;
                ASPxComboBox btn1 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col1, "cmbBox") as ASPxComboBox;
                GridViewDataColumn col10 = gvAmortizimi.Columns["Norme"] as GridViewDataColumn;
                ASPxTextBox btn2 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col10, "txtBox") as ASPxTextBox;
                GridViewDataColumn col11 = gvAmortizimi.Columns["IdStandartAmortizimi"] as GridViewDataColumn;
                ASPxLabel btn3 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col11, "lbl") as ASPxLabel;
                if (btn3 != null)
                {
                    btn3.ClientInstanceName = "lblStandart" + e.VisibleIndex.ToString();
                }
                if (btn0 != null)
                {
                    btn0.ClientInstanceName = "cmbLlojAmortizimi" + e.VisibleIndex.ToString();
                    DbCore.DbAsete.colAseteLlojAmortizimi col = new DbCore.DbAsete.colAseteLlojAmortizimi();
                    col.merrTeGjithaLlojAmortizimesh();
                    btn0.DataSource = col;
                    btn0.TextField = "LlojAmortizimi";
                    btn0.ValueField = "IdLlojAmortizimi";
                    if (btn0.Text == "")
                        btn0.SelectedIndex = 0;
                    btn0.DataBind();

                }
                if (btn1 != null)
                {
                    btn1.ClientInstanceName = "cmbNormeMagazine" + e.VisibleIndex.ToString();
                    btn1.Items.Add("Artikull", "Unchecked");
                    btn1.Items.Add("Magazine", "Checked");
                    btn1.DataBind();
                }
                if (btn2 != null)
                {
                    btn2.ClientInstanceName = "txtNorme" + e.VisibleIndex.ToString();
                    btn2.ClientSideEvents.TextChanged = "function (s,e){if(isNaN(parseFloat(s.GetText()))) {myMesazh.ShtoMesazhGabimi('Norma duhet te jete numer!'); s.SetText(0);} if(parseFloat(s.GetText())<0 ||parseFloat(s.GetText())>100){myMesazh.ShtoMesazhGabimi('Norma duhet te jete midis 0 dhe 100!'); s.SetText(0);} }";
                }
            }
        }
        #endregion

        #region grida norma e amortizimit
        private void konfiguroVleraFillestareNormaAmortizim()
        {
            colAseteNormaAmortizimiAbstract col = new colNormaAmortizimiRezerva();
            int id = 0;
            if (hfShtimModifikim.Value == "modifikim" || hfShtimModifikim.Value == "klonim")
            {
                id = int.Parse(hfId.Value.ToString());
                col.merrArtikullNormaAmortizimiTeGjitha(id, (int)hfState["idNdermarrje"], dteDateAk2.Date);
            }
            else col.merrArtikullNormaAmortizimiFillestare((int)hfState["idNdermarrje"], dteDateAk2.Date);
            gvNormaAmortizimi.DataSource = col;
            gvNormaAmortizimi.DataBind();
        }

        private void konfiguroGrideNormaAmortizimi(int idndermarje)
        {
            KonfigurimComboGride.ShtoStandart(gvNormaAmortizimi, idndermarje, Session, komponente, guidString, "IdStandartAmortizimi");


            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            GridUtil.konfiguroGrideListeEvogelPaTheme(gvNormaAmortizimi, "IdLidhjeArtikullLlojAmort");
            gvNormaAmortizimi.Settings.ShowFilterRow = false;
            gvNormaAmortizimi.SettingsBehavior.AllowSort = false;
            gvNormaAmortizimi.SettingsBehavior.AllowGroup = false;
            gvNormaAmortizimi.Settings.ShowFilterRowMenu = false;
            gvNormaAmortizimi.Settings.ShowHeaderFilterButton = false;
            gvNormaAmortizimi.SettingsEditing.Mode = GridViewEditingMode.Inline;
            gvNormaAmortizimi.Settings.ShowFilterBar = GridViewStatusBarMode.Hidden;
            gvNormaAmortizimi.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
            percaktoTemplateNormeAmortizim();

        }
        private void percaktoTemplateNormeAmortizim()
        {
            GridViewDataColumn col7 = gvNormaAmortizimi.Columns["IdLlojAmortizimi"] as GridViewDataColumn;
            col7.DataItemTemplate = new MyComboTemplate();
            GridViewDataColumn col17 = gvNormaAmortizimi.Columns["NormeMagazine"] as GridViewDataColumn;
            col17.DataItemTemplate = new MyComboTemplate();
            GridViewDataColumn col10 = gvNormaAmortizimi.Columns["Norme"] as GridViewDataColumn;
            col10.DataItemTemplate = new MyDoubleTemplate(false, 2, "0");
            GridViewDataColumn col1 = gvNormaAmortizimi.Columns["IdStandartAmortizimi"] as GridViewDataColumn;
            col1.DataItemTemplate = new MyLabelTemplate();
        }

        private DbCore.DbAsete.colAseteNormaAmortizimiAbstract ruajTrupinNormeAmortizim()
        {
            JavaScriptSerializer serializusi = new JavaScriptSerializer();
            object[] dokumenti = (object[])serializusi.DeserializeObject(gridRezerva.Value);
            DbCore.DbAsete.colAseteNormaAmortizimiAbstract trupat = new DbCore.DbAsete.colNormaAmortizimiRezerva();
            int idNdermarrje = (int)hfState["idNdermarrje"];
            for (int i = 0; i < dokumenti.Length; i++)
            {
                DbCore.DbAsete.clsAseteNormaAmortizimiAbstract trup = new DbCore.DbAsete.clsNormaAmortizimiRezerva(idNdermarrje, (Dictionary<string, object>)dokumenti[i], dteDateAk2.Date.Date);
                trupat.Add(trup);
            }
            return trupat;
        }


        protected void gvNormaAmortizimi_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters != "")
            {
                colAseteNormaAmortizimiAbstract col = new colNormaAmortizimiRezerva();
                int id = 0;

                int.TryParse(e.Parameters, out id);
                col.ktheArtikullNormaAmortizimiSipasIdKodifikimit(id, (int)hfState["idNdermarrje"], dteDateAk2.Date);

                gvNormaAmortizimi.DataSource = col;
                gvNormaAmortizimi.DataBind();
            }
            else konfiguroVleraFillestareNormaAmortizim();
            konfiguroGrideNormaAmortizimi(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        }

        protected void gvNormaAmortizimi_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gvNormaAmortizimi.PageIndex;
            e.Properties["cpPageRow"] = gvNormaAmortizimi.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gvNormaAmortizimi.VisibleRowCount;
        }

        protected void gvNormaAmortizimi_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {

            if (e.RowType == DevExpress.Web.GridViewRowType.Data)
            {
                GridViewDataColumn col0 = ((ASPxGridView)sender).Columns["IdLlojAmortizimi"] as GridViewDataColumn;
                ASPxComboBox btn0 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col0, "cmbBox") as ASPxComboBox;
                GridViewDataColumn col1 = ((ASPxGridView)sender).Columns["NormeMagazine"] as GridViewDataColumn;
                ASPxComboBox btn1 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col1, "cmbBox") as ASPxComboBox;
                GridViewDataColumn col10 = gvNormaAmortizimi.Columns["Norme"] as GridViewDataColumn;
                ASPxTextBox btn2 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col10, "txtBox") as ASPxTextBox;
                GridViewDataColumn col11 = gvNormaAmortizimi.Columns["IdStandartAmortizimi"] as GridViewDataColumn;
                ASPxLabel btn3 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col11, "lbl") as ASPxLabel;
                if (btn3 != null)
                {
                    btn3.ClientInstanceName = "lblStandartR" + e.VisibleIndex.ToString();
                }
                if (btn0 != null)
                {
                    btn0.ClientInstanceName = "cmbLlojAmortizimiR" + e.VisibleIndex.ToString();
                    DbCore.DbAsete.colAseteLlojAmortizimi col = new DbCore.DbAsete.colAseteLlojAmortizimi();
                    col.merrTeGjithaLlojAmortizimesh();
                    btn0.DataSource = col;
                    btn0.TextField = "LlojAmortizimi";
                    btn0.ValueField = "IdLlojAmortizimi";
                    if (btn0.Text == "")
                        btn0.SelectedIndex = 0;
                    btn0.DataBind();

                }
                if (btn1 != null)
                {
                    btn1.ClientInstanceName = "cmbNormeMagazineR" + e.VisibleIndex.ToString();
                    btn1.Items.Add("Artikull", "Unchecked");
                    btn1.Items.Add("Magazine", "Checked");
                    btn1.DataBind();
                }
                if (btn2 != null)
                {
                    btn2.ClientInstanceName = "txtNormeR" + e.VisibleIndex.ToString();
                    btn2.ClientSideEvents.TextChanged = "function (s,e){if(isNaN(parseFloat(s.GetText()))) {myMesazh.ShtoMesazhGabimi('Norma duhet te jete numer!'); s.SetText(0);} if(parseFloat(s.GetText())<0 ||parseFloat(s.GetText())>100){myMesazh.ShtoMesazhGabimi('Norma duhet te jete midis 0 dhe 100!'); s.SetText(0);} }";
                }
            }
        }
        #endregion

        private void mbushHiddenFieldMePerkthime(CultureInfo cultinf, ResourceManager rm)
        {
            hfState.Set("regjisDokZgjidhDokPerTeBashkengjitur", rm.GetString("regjisDokZgjidhDokPerTeBashkengjitur", cultinf));
            hfState.Set("msgShtoArtikullZgjidhArt", rm.GetString("msgShtoArtikullZgjidhArt", cultinf));
            hfState.Set("headerPopUpTextZgjidhNdermarrjet", rm.GetString("headerPopUpTextZgjidhNdermarrjet", cultinf));
            hfState.Set("msgPoTransferohetTeDhenatShtypniPerseriRuaj", rm.GetString("msgPoTransferohetTeDhenatShtypniPerseriRuaj", cultinf));
            hfState.Set("popupAdministrimiUniversal", rm.GetString("popupAdministrimiUniversal", cultinf));
            hfState.Set("msgSkemaNukPerketKlase", rm.GetString("msgSkemaNukPerketKlase", cultinf));
            hfState.Set("msgKoeficJoZeroOseBosh", rm.GetString("msgKoeficJoZeroOseBosh", cultinf));
            hfState.Set("msgKoeficDuhetNje", rm.GetString("msgKoeficDuhetNje", cultinf));
            hfState.Set("msgZgjdhniNjeNgaElementetEListes", rm.GetString("msgZgjdhniNjeNgaElementetEListes", cultinf));
            hfState.Set("msgShtoArtikullZgjidhArt", rm.GetString("msgShtoArtikullZgjidhArt", cultinf));
            hfState.Set("headerPopUpZgjidhKodifikiminArtikullit", rm.GetString("headerPopUpZgjidhKodifikiminArtikullit", cultinf));
            hfState.Set("MsgBlerjeShitjeAutorizime", rm.GetString("MsgBlerjeShitjeAutorizime", cultinf));
            hfState.Set("msgClsArtikulliFiroLigjoreDuhetNga0Deri100", rm.GetString("msgClsArtikulliFiroLigjoreDuhetNga0Deri100", cultinf));
            hfState.Set("msgShtoArtikullFirLigjoreDuhetNumer", rm.GetString("msgShtoArtikullFirLigjoreDuhetNumer", cultinf));
            hfState.Set("msgZgjidhniObjektivenEKostos", rm.GetString("msgZgjidhniObjektivenEKostos", cultinf));
            hfState.Set("msgShtoArtikullFirLigjoreDuhetNumer", rm.GetString("msgShtoArtikullFirLigjoreDuhetNumer", cultinf));
            hfState.Set("msgShtoArtikullFirLigjoreDuhetNumer", rm.GetString("msgShtoArtikullFirLigjoreDuhetNumer", cultinf));
            hfState.Set("msgShtoArtikullFirLigjoreDuhetNumer", rm.GetString("msgShtoArtikullFirLigjoreDuhetNumer", cultinf));
            hfState.Set("msgShtoArtikullFirLigjoreDuhetNumer", rm.GetString("msgShtoArtikullFirLigjoreDuhetNumer", cultinf));
            hfState.Set("msgShtoArtikullGaranciaDuhetNumer", rm.GetString("msgShtoArtikullGaranciaDuhetNumer", cultinf));
            hfState.Set("msgCmimeArtikulliSasiteMinimaleDuhenNumerike", rm.GetString("msgCmimeArtikulliSasiteMinimaleDuhenNumerike", cultinf));
            hfState.Set("msgSasiteMinBosh", rm.GetString("msgSasiteMinBosh", cultinf));
            hfState.Set("msgCmimeArtikulliSasiteMaxDuhenNumerike", rm.GetString("msgCmimeArtikulliSasiteMaxDuhenNumerike", cultinf));
            hfState.Set("msgSasiMaxBosh", rm.GetString("msgSasiMaxBosh", cultinf));
            hfState.Set("msgCmimeArtikulliCmimetDuhenNumerike", rm.GetString("msgCmimeArtikulliCmimetDuhenNumerike", cultinf));
            hfState.Set("msgCmime2Numerike", rm.GetString("msgCmime2Numerike", cultinf));
            hfState.Set("msgGabimLlogCmim", rm.GetString("msgGabimLlogCmim", cultinf));
            hfState.Set("msgZgjidhMagazinen", rm.GetString("msgZgjidhMagazinen", cultinf));
            hfState.Set("msgZgjidhFormuleLupa", rm.GetString("msgZgjidhFormuleLupa", cultinf));
            hfState.Set("msgNdodhiGabimGjateMarrjesSeTeDhenave", rm.GetString("msgNdodhiGabimGjateMarrjesSeTeDhenave", cultinf));
            hfState.Set("msgArtikulliEshteInaktiv", rm.GetString("msgArtikulliEshteInaktiv", cultinf));
            hfState.Set("msgKoeficDuhetNr", rm.GetString("msgKoeficDuhetNr", cultinf));
            hfState.Set("msgKoeficJoZero", rm.GetString("msgKoeficJoZero", cultinf));
            hfState.Set("msgZgjidhniLlojinEVeprimit", rm.GetString("msgZgjidhniLlojinEVeprimit", cultinf));
            hfState.Set("msgLlojiVeprimitIPanjohur", rm.GetString("msgLlojiVeprimitIPanjohur", cultinf));
            hfState.Set("msgFiroNumer", rm.GetString("msgFiroNumer", cultinf));
            hfState.Set("msgFiroVlera", rm.GetString("msgFiroVlera", cultinf));
            hfState.Set("msgSerialetLupa", rm.GetString("msgSerialetLupa", cultinf));
            hfState.Set("msgEkzistonKodiGride", rm.GetString("msgEkzistonKodiGride", cultinf));
            hfState.Set("msgEkzistonArtikullGride", rm.GetString("msgEkzistonArtikullGride", cultinf));
            hfState.Set("msgVendosetVetArtikulli", rm.GetString("msgVendosetVetArtikulli", cultinf));
            hfState.Set("msgZgjidhAktivitetin", rm.GetString("msgZgjidhAktivitetin", cultinf));
            hfState.Set("msgZgjidhDokumentin", rm.GetString("msgZgjidhDokumentin", cultinf));
            hfState.Set("headerZgjidhKlientFurnitorin", rm.GetString("headerZgjidhKlientFurnitorin", cultinf));
            hfState.Set("msgClsArtikulliKoefArtPerberesDuhetNumerPozitiv", rm.GetString("msgClsArtikulliKoefArtPerberesDuhetNumerPozitiv", cultinf));
            hfState.Set("headerPopUpZgjidhArtikullin", rm.GetString("headerPopUpZgjidhArtikullin", cultinf));
            hfState.Set("msgMagNjejte", rm.GetString("msgMagNjejte", cultinf));
            hfState.Set("msgPlotesoniVleren", rm.GetString("msgPlotesoniVleren", cultinf));
            hfState.Set(clsArtikulli.postStringTvsh, rm.GetString(clsArtikulli.postStringTvsh, cultinf));
            hfState.Set("msgPershkrimiNukDuhetTePermbajeKetoKaraktere", rm.GetString("msgPershkrimiNukDuhetTePermbajeKetoKaraktere", cultinf));
            hfState.Set("msgZgjidhKategorineDetajimNje", rm.GetString("msgZgjidhKategorineDetajimNje", cultinf));
            hfState.Set("msgZgjidhKategorineDetajimDy", rm.GetString("msgZgjidhKategorineDetajimDy", cultinf));
            hfState.Set("msgnumRreshtashSelektuar", rm.GetString("msgnumRreshtashSelektuar", cultinf));
            hfState.Set("msgShtoArtikullZgjidhNjeArtikull", rm.GetString("msgShtoArtikullZgjidhNjeArtikull", cultinf));
            hfState.Set("MsgNdaluesPlotesoKodinEBarit", rm.GetString("MsgNdaluesPlotesoKodinEBarit", cultinf));
            
            hfState.Set("headerPopUpTextSkemaKont", MessagesResource.Messages["headerPopUpTextSkemaKont"]);
            hfState.Set("headerPopUpKodBar", MessagesResource.Messages["headerPopUpKodBar"]);
            hfState.Set("msgZevendesimPlusi", MessagesResource.Messages["msgZevendesimPlusi"]);
        }



      
    }
}