using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Collections;
using System.Drawing;
using System.Data;
using DbCore;
using System.Globalization;
using System.Resources;
using DbCore.IMBUtils.Logging;
using DbCore.IMBUtils.Types;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.DbAdmin;
using DbCore.DbKontabiliteti;

namespace PlatinumWeb
{
    public partial class Shto_Banka : MyPageBase
    {
        private int idgjuha, idviti, idPerd, idNdermarrje;
        private string komponente = "Shto_Banka.aspx";
        private string guidString;
        DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();
        protected void Page_Init(object sender, EventArgs e)
        {
        }
        /// <summary>
        /// thirret kur faqja lodohet. Ne te behet kontrolli nese perdoruesi eshte i loguar ne sistem  dhe nqs jo ridrejtohet tek forma e logimit
        /// thirret inicializimi i konfigurimeve fillestare te faqes
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e"> argumenti</param>
        protected void Page_Load(object sender, EventArgs e)
        {  // Prevent caching, so can't be viewed offline
            ////Response.Cache.SetCacheability(HttpCacheability.NoCache);
            idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            idPerd = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            idviti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            idgjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            CultureInfo cultinf = DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo(idgjuha);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (hfState.Count == 0)
            {
                hfState.Set("llog", -1);
            }
            if (!Page.IsPostBack)
            {
                
                if (!DbCore.mySessionObjects.isLogedIn(Session))
                {
                    DbCore.clsFunksione.logout(Session,true,"FaqePaautorizuar");
                }
                if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
                {
                    Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerd);
                }
                hfState.Set("usernamePerdoruesLoguar", oPerdorues.PerdoruesUsername);
                guidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                hfState.Set("guidString", guidString);
                EmrateTabeve(rm, cultinf);
                vendosHfMePerkthime(rm, cultinf);
                EmratEKontrolleve(rm, cultinf);
                hfState.Set("idGjuha", idgjuha);
                hfState.Set("idPerdoruesi", idPerd);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("llojNdermarrje", DbCore.DbAdmin.clsNdermarrje.ktheLlojNdermSipasID(idNdermarrje));
                ASPxPageControl1.ActiveTabIndex = 0;
                konfiguroVleraFillestare(idNdermarrje, rm, cultinf, idgjuha);
                mbushGridABNgaDB(idNdermarrje, idPerd);
                hfState.Set("colAutorizime", Newtonsoft.Json.JsonConvert.SerializeObject(new colAutorizimetKoka(IdPerdoruesi)));
                if (Request.QueryString["ab"] == "arka")
                {
                    ASPxGridView_Bankat.FilterExpression = "[LlojArkaBanka]=false";
                }
                else
                {
                    ASPxGridView_Bankat.FilterExpression = "[LlojArkaBanka]=true";
                }
                konfiguroGride(idNdermarrje, idPerd, cmbKonfigurimi.Text.Split(';')[0], 121);
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "ASPxGridView_Bankat", ASPxGridView_Bankat, cmbKonfigurimi.Text.Split(';')[0], "121", (int)hfState["idGjuha"]);
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerd, idNdermarrje, idviti, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerd, idNdermarrje, idviti, DbCore.clsFunksione.GetKomponente(Page.Request));
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                txtTCR.ReadOnly = true;
                ASPxGridView_Bankat.Columns["#"].VisibleIndex = 0;
            }
            else
            {
                guidString = (string)hfState["guidString"];
                mbushGridABNgaSession(idNdermarrje, idPerd);
                konfiguroGride(idNdermarrje, idPerd, cmbKonfigurimi.Text.Split(';')[0], 121);
            }
            AspxWebControlUtils.perkthePopUp(popFshi, rm.GetString("labelKujdes", cultinf), lblMsgbox, rm.GetString("labelAdministrimiMsgJeniSigurt", cultinf), ButtonCancel, rm.GetString("labelAnullo", cultinf));
            GridUtil.konfigGrideListeEMadhePaTheme(ASPxGridView_Bankat, "IdBanka");
            percaktoTemplateMenu(idgjuha, idviti, idPerd, idNdermarrje, ASPxMenu1);
            clsToolbarConfig.mbushComboBoxFiltra(idgjuha, idNdermarrje, "ASPxGridView_Bankat", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
            GridUtil.ToolTipButonaveMbiGride(ASPxGridView_Bankat, cultinf, rm);
        }

        private void EmratEKontrolleve(ResourceManager rm, CultureInfo cultinf)
        {
            konfigurimi_Label.Text = rm.GetString("lblModeli", cultinf);
            popupUniversal.HeaderText = rm.GetString("headerPopUpText", cultinf);
        }

        private void vendosHfMePerkthime(ResourceManager rm, CultureInfo cultinf)
        {
            hfState.Set("msgCeljeArkaBankaZgjidhNjeArkeBanke", rm.GetString("msgCeljeArkaBankaZgjidhNjeArkeBanke", cultinf));
            hfState.Set("headerPopUpZgjidhGrupinEArkes", rm.GetString("headerPopUpZgjidhGrupinEArkes", cultinf));
            hfState.Set("headerPopUpZgjidhGrupinEBankes", rm.GetString("headerPopUpZgjidhGrupinEBankes", cultinf));
            hfState.Set("headerPopUpZgjidhAutorizimet", rm.GetString("headerPopUpZgjidhAutorizimet", cultinf));
            hfState.Set("headerPopUpText", rm.GetString("headerPopUpText", cultinf));
            hfState.Set("cmbboxItemFilterAvancBanka", rm.GetString("cmbboxItemFilterAvancBanka", cultinf));
            hfState.Set("cmbboxItemFilterAvancArka", rm.GetString("cmbboxItemFilterAvancArka", cultinf));
            hfState.Set("msgZgjdhniNjeNgaElementetEListes", rm.GetString("msgZgjdhniNjeNgaElementetEListes", cultinf));
            hfState.Set("msgCeljeArkaBankaLlogariaTeFIllojeMe512", rm.GetString("msgCeljeArkaBankaLlogariaTeFIllojeMe512", cultinf));
            hfState.Set("msgCeljeArkaBankaLlogariaTeFIllojeMe520Ose5120", rm.GetString("msgCeljeArkaBankaLlogariaTeFIllojeMe520Ose5120", cultinf));
            hfState.Set("msgCeljeArkaBankaLlogariaTeFIllojeMe531", rm.GetString("msgCeljeArkaBankaLlogariaTeFIllojeMe531", cultinf));
        }

        /// <summary>
        /// Vendos emrat e tabeve ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateTabeve(System.Resources.ResourceManager rm, System.Globalization.CultureInfo cultinf)
        {
            ASPxPageControl1.TabPages[0].Text = rm.GetString("TePergjithshmeTab", cultinf);
            ASPxPageControl1.TabPages[1].Text = rm.GetString("cmbboxItemFilterAvancBanka", cultinf);
            ASPxPageControl1.TabPages[2].Text = rm.GetString("labelRaportAdresa", cultinf);
            ASPxPageControl1.TabPages[3].Text = rm.GetString("labelRaportKontakti", cultinf);
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
            clsToolbarConfig.percaktoTemplateMenu(idGjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1, DbCore.clsFunksione.GetKomponente(Page.Request), this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, hfShtimModifikim.Value == "modifikim" ? false : true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
        }

        protected void RuajKolona_Click(object sender, EventArgs e)
        {
            int idfiltri = 0;
            int idkonf = DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(cmbKonfigurimi.Text.Split(';')[0], idNdermarrje);
            DbCore.clsMesazh mesazh = GridUtil.ruajFiltra(idNdermarrje, idPerd, idgjuha, "ASPxGridView_Bankat", komponente, "FilterDefault", ASPxGridView_Bankat.FilterExpression, ASPxGridView_Bankat, "KodiBanka", idkonf, out idfiltri);//ruan filtrin e zgjedhur tek filtrat
            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                return;
            }
            mesazh = GridUtil.ruajkonfigurimgride(ASPxGridView_Bankat, cmbKonfigurimi.Text, idNdermarrje, idPerd, 121, idfiltri, idviti, DbCore.mySessionObjects.ktheCultureInfo(Session), DbCore.mySessionObjects.ktheGjuhe(Session));/// ruan konfigurimin e grides dhe filtrin

            clsToolbarConfig.mbushComboBoxFiltra(idgjuha, idNdermarrje, "ASPxGridView_Bankat", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);

            percaktoTemplateMenu(idgjuha, idviti, idPerd, idNdermarrje, ASPxMenu1);
            if (mesazh.Status)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
        }

        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(idgjuha, idviti, idPerd, idNdermarrje, ASPxMenu1);
        }

        /// <summary>
        /// perdoret per te shtuar kolonen e selektimit tek grida dhe per te vendosur disa karakteristika te grides
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void ASPxGridView_Bankat_DataBound(object sender, EventArgs e)
        {// shton colonen # per selektim dhe disa karakteristika te grides
            if (this.ASPxGridView_Bankat.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per 
                //perzgjidh
                DevExpress.Web.GridViewCommandColumn check = new DevExpress.Web.GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true; check.Width = Unit.Percentage(2);
                //behet per te afishuar rreshtin qe do sherbej per filtrim
                ASPxGridView_Bankat.Settings.ShowFilterRow = true;
                ASPxGridView_Bankat.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                ASPxGridView_Bankat.Settings.ShowFilterRowMenu = true;
                ASPxGridView_Bankat.Columns.Add(check);
                ASPxGridView_Bankat.KeyFieldName = "IdBanka";
                ASPxGridView_Bankat.SettingsBehavior.AllowSelectByRowClick = true;
                ASPxGridView_Bankat.SettingsBehavior.AllowFocusedRow = true;
            }
        }

        /// <summary>
        /// perdoret per te konfiguruar griden. Per te dhenat mbi kolonat qe do te shfaqen nga databaza si dhe percakton disa karakteristika te grides
        /// </summary>
        /// :  <see cref="GridUtil.percaktoVisibleColumns"/>
        /// :  <see cref="DbCore.clsFunksione.konfiguroGrideListeMadhe(grida,celesigrides)"/>
        /// <param name="idNdermarrje"></param>
        private void konfiguroGride(int idNdermarrje, int idPerdoruesi, string kodKonfigurimi, int idKomponente)
        {
            CultureInfo ci = DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo(idgjuha);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            KonfigurimComboGride.shtoLlojBankeArke(ASPxGridView_Bankat, rm, ci);
        
            if (Convert.ToString(hfState["llog"]) != "-1")
            {
                shtoLlogNeSesion();
                KonfigurimComboGride.ShtoLlogari(ASPxGridView_Bankat, idPerdoruesi, Session, komponente, guidString, "IdLlogariKontabilizimi");
            }
            else
                KonfigurimComboGride.ShtoLlogari(ASPxGridView_Bankat, idPerdoruesi, Session, komponente, guidString, "IdLlogariKontabilizimi");
            KonfigurimComboGride.ShtoMonedhe(ASPxGridView_Bankat, idNdermarrje, idPerdoruesi, Session, komponente, guidString, "IdMonedhaBanka");
            KonfigurimComboGride.ShtoLlogari(ASPxGridView_Bankat, idPerdoruesi, Session, komponente, guidString, "Komisioni");
            KonfigurimComboGride.shto_DegeAdministrative(ASPxGridView_Bankat, idNdermarrje, Session, komponente, guidString, "IdDegeAdministrative");
            KonfigurimComboGride.ShtoAutorizimSipasPerdoruesit(ASPxGridView_Bankat, idPerdoruesi, Session, komponente, guidString, "IdNivelAutorizimi");

            this.ASPxGridView_Bankat.Columns["#"].VisibleIndex = 0;
        }

        private void shtoLlogNeSesion()
        {
            var sessionKey = DbCore.IMBUtils.Cache.SessionKeyUtils.MerrSessionKeyPerCmb(komponente, "IdLlogariKontabilizimi");
            DbCore.DbKontabiliteti.colLlogarite colLlog = new DbCore.DbKontabiliteti.colLlogarite();
            colLlog = mySessionObjects.MerrNgaSession<colLlogarite>(Session, sessionKey, guidString);
            if (colLlog.Find(x => x.IdLlogari == Convert.ToInt32(Convert.ToString(hfState["llog"]))) != null)
                return;
            DbCore.DbKontabiliteti.clsLlogari llogaria = new DbCore.DbKontabiliteti.clsLlogari(Convert.ToInt32(Convert.ToString(hfState["llog"])));
            if (llogaria.IdLlogari <= 0)
                return;
            colLlog.Add(llogaria);
            mySessionObjects.RuajNeSession(sessionKey, guidString, colLlog);
        }

  
        /// <summary>
        /// perdoret ne rastet kur grida ben callback per ta rimbushur griden
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void ASPxGridView_Bankat_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            if (e.CallbackName == "COLUMNMOVE" && ASPxGridView_Bankat.AllColumns[int.Parse(e.Args[0])].Width.Value == 0)
                ASPxGridView_Bankat.AllColumns[int.Parse(e.Args[0])].Width = Unit.Percentage(3);
            if (e.CallbackName == "APPLYFILTER" && e.Args[0] == "")
            {
                DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
                ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
                cmbFiltra.Text = "";
                if (Request.QueryString["ab"] == "arka")
                {
                    ASPxGridView_Bankat.FilterExpression = "[LlojArkaBanka]=false";
                }
                else
                {
                    ASPxGridView_Bankat.FilterExpression = "[LlojArkaBanka]=true";
                }
            }
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            konfiguroVleraFillestare(idNdermarrje, rm, cultinf, DbCore.mySessionObjects.ktheGjuhe(Session));
            GridUtil.ToolTipButonaveMbiGride(ASPxGridView_Bankat, cultinf, rm);
            //  konfiguroGride(idNdermarrje, cmbKonfigurimi.Text.Split(';')[0], 121);
        }
        /// <summary>
        /// perdoret per te vendosur filtrat tek headeri i grides
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxGridView_Bankat_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            string TeGjithe = rm.GetString("GridHeaderFilterFillItemTeGjithe", ci);
            string nga = rm.GetString("GridHeaderFilterFillItemNga", ci);
            if (e.Column.FieldName == "EmerBanka")
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
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "ASPxGridView_Bankat", komponente, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "ASPxGridView_Bankat", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
                percaktoTemplateMenu(idgjuha, idviti, idPerd, idNdermarrje, ASPxMenu1);

                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                cmbFiltra.Text = "";
                if (Request.QueryString["ab"] == "arka")
                {
                    ASPxGridView_Bankat.FilterExpression = "[LlojArkaBanka]=false";
                }
                else
                {
                    ASPxGridView_Bankat.FilterExpression = "[LlojArkaBanka]=true";
                }

                hfStatusi.Value = "true";
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
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DbCore.DbAdmin.clsFiltraGrida filtri = new DbCore.DbAdmin.clsFiltraGrida();
            filtri.FiltraKodi = cmbFiltra.Text;
            filtri.FiltraShenime = cmbFiltra.Text;
            filtri.FiltraUniversal = false;
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "ASPxGridView_Bankat", komponente, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = ASPxGridView_Bankat.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("KodiBanka", ASPxGridView_Bankat);
            
            DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();
            oPerdorues = DbCore.mySessionObjects.kthePerdorues(Session);
            filtri.IdPerdoruesi = oPerdorues.IdPerdorues;
            filtri.IdNdermarje = idNdermarrje;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            filtri.IdStatusDok = 1;

            mesazh = filtri.ruaj();
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "ASPxGridView_Bankat", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
            percaktoTemplateMenu(idgjuha, idviti, idPerd, idNdermarrje, ASPxMenu1);

            if (mesazh.Status == true)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            hfStatusi.Value = "true";
            cmbFiltra.Text = "";
        }
      
        /// <summary>
        /// perdoret per te fshire rreshtat e zgjedhur te bankave nqs perdoruesi konfirmon fshirjen
        /// </summary>
        ///  :  <see cref=DbCore.DbArkaBanka.clsDatabaseArkaBanka.ktheBanke(id)"/> 
        ///  :  <see cref="DbCore.DbArkaBanka.clsBanka.fshi()"/> 
        /// <param name="sender"> derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ButtonOk_Click2(object sender, EventArgs e)
        {//fshin rreshtat e selektuar

            List<object> rreshtat;
            if (ASPxPageControl1.ActiveTabIndex == 0)//nqs kemi zgjedhur vetem nje kartele per te pare fshijme vetem ate
                rreshtat = ASPxGridView_Bankat.GetSelectedFieldValues("IdBanka");
            else
            {
                rreshtat = new List<object>();
                rreshtat.Add(hfId.Value);
            }
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            if (rreshtat.Count == 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgClejeArkaBankaZgjidhniNje", ci), pnlMesazhi);
                return;
            }
            List<string> TeFshire = new List<string>(), TePaFshire = new List<string>();
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            DbCore.DbRegjistrim.clsDatabaseRegjistrim dbRegjistrim = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
            DbCore.DbArkaBanka.clsBanka clsBankat = new DbCore.DbArkaBanka.clsBanka();
            foreach (object id in rreshtat)
            {
                clsBankat.mbushBanke(Convert.ToInt32(id));
                DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                konf.mbushKonfigAmbjSipasId(clsBankat.IdKonfig, DbCore.mySessionObjects.ktheGjuhe(Session));

                bool lidhur = dbRegjistrim.eshteDokumentiILidhurCelje(clsBankat.IdBanka.ToString(), konf.IdNivel.ToString());
                if (lidhur)
                {
                    TePaFshire.Add(clsBankat.KodiBanka);
                    continue;
                }
                int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                clsBankat.IdPerdoruesi = idPerdoruesi;
                mesazh = clsBankat.fshi();
                if (clsBankat.IdBanka == 0) continue;
                if (mesazh.Status)
                {
                    #region Heq llogarite nga grida

                    // ASPxGridView_Artikull.DataSource = dt;
                    hiqABNgaGrida(clsBankat.IdBanka, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), idPerdoruesi, rm, ci);
                    #endregion
                    TeFshire.Add(clsBankat.KodiBanka);

                    ASPxPageControl1.ActiveTabIndex = 0;
                    hfStatusi.Value = "true";
                }
            }
            dbRegjistrim.Dispose();
            string mesazhInfoGabim = "", mesazhInfoSukses = "";
            if (TePaFshire.Count == 1)
                mesazhInfoGabim = String.Format("{0}{1}{2}", rm.GetString("msgClejeArkaBankaPrefixNjejes", ci), String.Join(";", TePaFshire), rm.GetString("msgClejeArkaBankaSuffixNjejesGabimi", ci));
            else
                if (TePaFshire.Count > 1)
                    mesazhInfoGabim = String.Format("{0}{1}{2}", rm.GetString("msgClejeArkaBankaPrefixShumes", ci), String.Join(";", TePaFshire), rm.GetString("msgClejeArkaBankaSuffixShumesGabimi", ci));
            if (TeFshire.Count == 1)
                mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("msgClejeArkaBankaPrefixNjejes", ci), String.Join(";", TeFshire), rm.GetString("msgClejeArkaBankaSuffixNjejesSuksesi", ci));
            else
                if (TeFshire.Count > 1)
                    mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("msgClejeArkaBankaPrefixShumes", ci), String.Join(";", TeFshire), rm.GetString("msgClejeArkaBankaSuffixShumesSuksesi", ci));
            if (mesazhInfoGabim != "" && mesazhInfoSukses != "")
                mesazhInfoGabim += rm.GetString("lidhesMesazhi", ci) + mesazhInfoSukses;
            if (mesazhInfoGabim != "")
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhInfoGabim, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazhInfoSukses, pnlMesazhi);
        }
        private void hiqABNgaGrida(int idbanka, int idNdermarrje, int idPerdorues, ResourceManager rm, CultureInfo ci)
        {
            if (this.ASPxGridView_Bankat.DataSource != null)
            {
                DataTable dt = (DataTable)ASPxGridView_Bankat.DataSource;
                DataRow[] drs = dt.Select("IdBanka = " + idbanka);
                if (drs.Length > 1)
                    throw new MyException(rm.GetString("msgClejeArkaBanka2ArkaBankaMeIdTeNjejteNeGride", ci));
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                dt.Rows.Remove(dr);
                ASPxGridView_Bankat.DataBind();
            }
            else mbushGridABNgaDB(idNdermarrje, idPerdorues);
        }
        private void shtoABNeGrid(int idNdermarrje, int idPerdorues, int idbanka, ResourceManager rm, CultureInfo ci)
        {
            if (ASPxGridView_Bankat.DataSource != null)
            {
                DataTable dt = (DataTable)ASPxGridView_Bankat.DataSource;
                DataRow[] drs = dt.Select("IdBanka = " + idbanka);
                if (drs.Length > 0)
                    throw new MyException(rm.GetString("msgClejeArkaBankaEkzistonArkaBankaNeGride", ci));
                DataRow newArtDr = DbCore.DbArkaBanka.colBankat.merrSipasABNdermarrjesAndAutorizimeDR(idNdermarrje, idPerdorues, idbanka);
                dt.ImportRow(newArtDr);
            }
            else mbushGridABNgaDB(idNdermarrje, idPerdorues);
        }
        private void modifikoABNeGrid(int idNdermarrje, int idPerdorues, int idbanka, ResourceManager rm, CultureInfo ci)
        {
            if (ASPxGridView_Bankat.DataSource != null)
            {
                DataTable dt = (DataTable)ASPxGridView_Bankat.DataSource;
                DataRow[] drs = dt.Select("IdBanka = " + idbanka);
                if (drs.Length > 1)
                    throw new MyException(rm.GetString("msgClejeArkaBanka2ArkaBankaMeIdTeNjejteNeGride", ci));
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                DataRow newArtDr = DbCore.DbArkaBanka.colBankat.merrSipasABNdermarrjesAndAutorizimeDR(idNdermarrje, idPerdorues, idbanka);
                //  dt.Rows.Remove(dr);
                // dt.ImportRow(newArtDr);
                object[] arr = newArtDr.ItemArray;
                dr.ItemArray = arr;
            }
            else mbushGridABNgaDB(idNdermarrje, idPerdorues);
        }
        /// <summary>
        /// perdoret per te trajtuar ngjarjet e butonave te menuse. Shkon ne faqen e modifikimit te bankave kur perdoruesi klikon butonin modifiko
        /// ose ne faqen e shtimit te bankave kur perdoruesi klikon butonin shto
        /// </summary>
        /// <param name="source"> derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {//kryen veprimet e menuse
            if (e.Item.Name == "Ruaj")
            {
                Page.Validate("entries");
                ruajBanke();
            }
        }

        private void konfiguroVleraFillestare(int idNdermarrje, System.Resources.ResourceManager rm, System.Globalization.CultureInfo ci, int idGjuha)
        { //mbush komboboxet dhe gridat e faqes
            ASPxPageControl1.ActiveTabIndex = 0;
            int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            bool llojiArkaBanka = Request.QueryString["ab"] != "arka";
            //inicializoObjekte();
            ConfigureAspxComboBox.ShtoKolonaPerLlogarine(txtNrLlogari);
            ConfigureAspxComboBox.ShtoKolonaPerLlogarine(komisioni_ButtonEdit);
            ConfigureAspxComboBox.mbushComboMonedha(0, idNdermarrje, false, cmbMonedha);
            ConfigureAspxComboBox.mbushComboLlojiBanka(cmbLloji);
            ConfigureAspxComboBox.KonfiguroComboBoxDegeAdministrative(idNdermarrje, cmbDegeAdministrative, false);
            ConfigureAspxComboBox.mbushComboGrupeBankash(idNdermarrje, txtGrupi, llojiArkaBanka);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(txtGrupi,txtNrLlogari);
          
            ConfigureAspxComboBox.percaktoTemplateComboMeEnableCallback(komisioni_ButtonEdit);
            if(!llojiArkaBanka)
            {
                mbushComboKonfigurimeshSipasKategorise(cmbKonfigurimi, 15, "ARC", idNdermarrje, rm, ci, idgjuha);
            }
            else mbushComboKonfigurimeshSipasKategorise(cmbKonfigurimi, 15, "B", idNdermarrje, rm, ci, idgjuha);
            cmbKonfigurimi.SelectedIndex = 0;
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()), idGjuha);
            hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;
        }

        public void mbushComboKonfigurimeshSipasKategorise(ASPxComboBox combo, int kat, string nivel, int idNdermarrje, System.Resources.ResourceManager rm, System.Globalization.CultureInfo ci, int idGjuha)
        {//mbush griden e popupit me te dhena
            DbCore.DbShare.colKonfigurimAmbjenti col = new DbCore.DbShare.colKonfigurimAmbjenti();
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            if (kat != 0)
            {
                konf.IdKategori = kat;
                konf.IdNdermarje = idNdermarrje;
                int idNiveli = DbCore.DbRegjistrim.clsNivelRegjistrimi.ktheIdNivelRegjistrimiSipasKodi(nivel, idNdermarrje);
                col.mbushKonfigAmbjSipasIdKategoriIdNivel(konf.IdKategori, idNiveli, DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idGjuha, true);
            }
            else
            {
                col.mbushGjitheKonfigurimeAmbjentesh(idNdermarrje, DbCore.mySessionObjects.ktheIdPerdoruesi(Session), 1, idGjuha);    //celje
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

        private void inicializoObjekte()
        {

        }

        private void mbushGridABNgaSession(int idNdermarrje, int idPerdorues)
        {
            DataTable tmpObject;
            bool sukses = DbCore.mySessionObjects.merrGrideNgaSessioni(Session, out tmpObject);
            if (!sukses)
                mbushGridABNgaDB(idNdermarrje, idPerdorues);
            else
            {
                ASPxGridView_Bankat.DataSource = tmpObject;
                ASPxGridView_Bankat.DataBind();
                tmpObject.Dispose();
            }
        }

        private void mbushGridABNgaDB(int idNdermarrje, int idPerdorues)
        {//mbush griden e popupit me te dhena            
            DataTable dt = DbCore.DbArkaBanka.colBankat.merrSipasABNdermarrjesAndAutorizimeDT(idNdermarrje, idPerdorues);
            DbCore.mySessionObjects.ruajGrideNeSession(Session, dt);
            ASPxGridView_Bankat.DataSource = dt;
            ASPxGridView_Bankat.DataBind();
            dt.Dispose();
        }

        private void mbushListeBankash(int idNdermarrje)
        {//mbush griden me te dhena                                    
            DbCore.DbArkaBanka.colBankat colBankat = new DbCore.DbArkaBanka.colBankat();
            colBankat.mbushGjitheBankatSipasAutorizimeve(idNdermarrje, DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            ASPxGridView_Bankat.DataSource = colBankat;
            ASPxGridView_Bankat.DataBind();
        }

        private void ruajBanke()
        {
            if(cbShfaqEinvoice.Checked && txtNrLlogariBankare.Text == "")
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Plotesoni fushen Nr. Llogari Bankare!", pnlMesazhi);
                hfStatusi.Value = "false";
                return;
            }
            DbCore.DbArkaBanka.clsBanka banka;
            if (!Page.IsValid)
                return;
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            if (isValidBanke(idNdermarrje, rm, ci))
                {
                    bool eshteShtim;
                try
                {
                    banka = krijoBanke();
                }
                catch (Exception e)
                {
                    ImbLogger.Error(e.Message);
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, e.Message, pnlMesazhi);
                    hfStatusi.Value = "false";
                    return;
                }
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                    DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                    int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                    tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.clsFunksione.GetKomponente(Page.Request));
                    if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim")
                    {
                        if (!tedrejtaInfo.DShtim)
                        {
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNukKeniTeDrejta", ci), pnlMesazhi);
                            hfStatusi.Value = "false";
                            return;
                        }
                        mesazh = banka.ruaj();
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
                        eshteShtim = false;
                        DbCore.DbRegjistrim.clsDatabaseRegjistrim dbRegjistrim = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
                        DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                        konf.mbushKonfigAmbjSipasId(banka.IdKonfig, DbCore.mySessionObjects.ktheGjuhe(Session));
                        banka.IdBanka = int.Parse(hfId.Value.ToString());
                        bool lidhur = dbRegjistrim.eshteDokumentiILidhurCelje(banka.IdBanka.ToString(), konf.IdNivel.ToString());
                        if (lidhur.ToString() != hfLidhur.Value)
                        {
                            mesazh.Status = false;
                            mesazh.PershkrimMesazhi = rm.GetString("msgCeljeArkaBankaEshteILidhur", ci);

                        }
                        else
                            mesazh = banka.modifiko();
                        dbRegjistrim.Dispose();
                    }
                    if (!mesazh.Status == true)
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgGabimiNeRuajtje", ci), pnlMesazhi);
                        hfStatusi.Value = "false";
                    }
                    else
                    {
                        clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("msgRuajtjeMeSukses", ci), pnlMesazhi);
                        pastroFusha();
                        if (eshteShtim)
                            shtoABNeGrid(idNdermarrje, idPerdoruesi, banka.IdBanka, rm, ci);
                        else //modifikim
                            modifikoABNeGrid(idNdermarrje, idPerdoruesi, banka.IdBanka, rm, ci);

                        ASPxPageControl1.ActiveTabIndex = 0;
                        hfStatusi.Value = "true";
                    }
                }
                pnlMesazhi.Update();
        }

        /// <summary>
        /// Pastron fushat
        /// </summary>
        private void pastroFusha()
        {
            this.txtKodi.Text = "";
            this.txtKodiAdresa.Text = "";
            this.txtKodiKontakti.Text = "";
            this.txtEmerBanke.Text = "";
            this.txtEmerBankeAdresa.Text = "";
            this.txtEmerBankeKontakti.Text = "";
            this.cmbTipi.SelectedIndex = -1;
            this.txtNrLlogariBankare.Text = "";
            this.txtIban.Text = "";
            this.txtGrupi.Text = "";
            this.txtShenime.Text = "";
            this.cbAktive.Checked = true;
            this.txtNrLlogari.Text = "";
            this.cmbMonedha.SelectedIndex = -1;
            this.cmbAutorizimiHf.Value = "";
            this.txtRruga.Text = "";
            this.txtQyteti.Text = "";
            this.txtShteti.Text = "";
            this.txtZipKod.Text = "";
            this.txtTel.Text = "";
            this.txtEmer.Text = "";
            this.txtMbiemri.Text = "";
            this.txtTelK.Text = "";
            this.txtFax.Text = "";
            this.txtCel.Text = "";
            this.txtEmail.Text = "";
            this.komisioni_ButtonEdit.Text = "";
            txtNrKlienti.Text = "";
            txtValuta.Text = "";
            txtDega.Text = "";
            txtTipi.Text = "";
            txtKodiLlogarise.Text = "";
            HiddenField1.Value = "";
            txtAdresa.Text = "";
            this.hfAutorizime.Value = "";
            txtTCR.Text = "";
            txtNrRendor.Text = "";
        }

        private DbCore.DbArkaBanka.clsBanka krijoBanke()
        {//krijon nje banke sipas te dhenave te futura nga perdoruesi
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            CultureInfo ci = mySessionObjects.ktheCultureInfo(Session);
            DbCore.DbArkaBanka.clsBanka banka = new DbCore.DbArkaBanka.clsBanka();
            DbCore.DbAdmin.colLidhjetAutorizim colLidhje;
            if (cmbAutorizimiHf.Value == "")
                colLidhje = new DbCore.DbAdmin.colLidhjetAutorizim();
            else
            {
                DbCore.DbAdmin.colLidhjetAutorizim colLidhjet = new DbCore.DbAdmin.colLidhjetAutorizim();
                string[] pars11 = cmbAutorizimiHf.Value.Split(',');
                for (int i = 0; i < pars11.Length; i++)
                {
                    DbCore.DbAdmin.clsLidhjeAutorizim lidhje = new DbCore.DbAdmin.clsLidhjeAutorizim();
                    lidhje.IdAutorizimeKoka = DbCore.DbAdmin.clsAutorizimKoka.ktheIDAutorizim(pars11[i]);
                    colLidhjet.Add(lidhje);
                }
                colLidhje = colLidhjet;
            }

            banka.KodiBanka = clsFunksione.ktheStringunPaHapesira(txtKodi.Text, true);
            banka.EmerBanka = clsFunksione.ktheStringunPaHapesira(txtEmerBanke.Text,false);
            bool lloji = false;
            if (cmbLloji.Text == "Arka" || cmbLloji.Text == "Cash" || cmbLloji.Text == "En espèces")
                lloji = false;
            else lloji = true;
            
            banka.IdTipiBanka = 1;
            banka.NrLlogariBanka = txtNrLlogariBankare.Text;
            banka.IBAN = txtIban.Text;
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);

            int idgrupB;
            if (txtGrupi.Text != "")
                if (new DbCore.DbArkaBanka.clsGrupBanke(txtGrupi.Text, idNdermarrje) != null)
                    idgrupB = new DbCore.DbArkaBanka.clsGrupBanke(txtGrupi.Text, idNdermarrje).IdGrupBanke;
                else
                    idgrupB = 0;
            else
                idgrupB = 0;
            int idDegeAdm = 0;
            if (cmbDegeAdministrative.Text != "")
                idDegeAdm = int.Parse(cmbDegeAdministrative.Value.ToString());
            else idDegeAdm = 0;
            banka.ShenimeBanka = txtShenime.Text;
            banka.AktivBanka = cbAktive.Checked;
            banka.IdNivelAutorizimi = hfAutorizime.Value;
            int idLlogKont = DbCore.DbKontabiliteti.clsLlogari.mbushIDLlogariSipasKodit(txtNrLlogari.Text.Split(';')[0], idNdermarrje);
            int idMonB = new DbCore.DbKontabiliteti.clsLlogari(txtNrLlogari.Text.Split(';')[0], idNdermarrje).IdMonedha;
            
            int komision = 0;
            if (komisioni_ButtonEdit.Text != "")
                if (new DbCore.DbKontabiliteti.clsLlogari(komisioni_ButtonEdit.Text.Split(';')[0], idNdermarrje) != null)
                    komision = DbCore.DbKontabiliteti.clsLlogari.mbushIDLlogariSipasKodit(komisioni_ButtonEdit.Text.Split(';')[0], idNdermarrje);
                else
                    komision = 0;
            else
                komision = 0;
            DbCore.DbShare.clsKonfigurimAmbjenti konfig = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konfig.mbushKonfigAmbjSipasKod(cmbKonfigurimi.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            int idkonfig = konfig.IdKonfigAmbjente;
            banka = new DbCore.DbArkaBanka.clsBanka(0, clsFunksione.ktheStringunPaHapesira(txtKodi.Text, true), clsFunksione.ktheStringunPaHapesira(txtEmerBanke.Text, false), 1, txtNrLlogariBankare.Text, 
                        txtIban.Text, idgrupB, txtShenime.Text, cbAktive.Checked, idLlogKont, idMonB, txtRruga.Text, txtQyteti.Text, txtShteti.Text, txtZipKod.Text, txtTel.Text, txtEmer.Text, txtMbiemri.Text, 
                        txtTelK.Text, txtFax.Text, txtCel.Text, txtEmail.Text, DbCore.mySessionObjects.ktheIdPerdoruesi(Session), komision, idNdermarrje, lloji, txtAdresa.Text, idkonfig, idDegeAdm, 1,
                        txtDega.Text,txtValuta.Text, txtNrKlienti.Text, txtKodiLlogarise.Text, txtTipi.Text, colLidhje, txtTCR.Text, txtNrRendor.Text, rm, ci, cbShfaqEinvoice.Checked);
            return banka;
        }

        //kontrollon nese te dhenat qe jane plotesuara jane te lejueshme apo jo
        private bool isValidBanke(int idNdermarrje, System.Resources.ResourceManager rm, System.Globalization.CultureInfo ci )
        {
            DbCore.DbAdmin.clsNdermarrje nderm = new DbCore.DbAdmin.clsNdermarrje(idNdermarrje);
            if (txtNrLlogari.Text != "")
            {
                if (!DbCore.DbKontabiliteti.clsLlogari.ekzistonLlogari(txtNrLlogari.Text.Split(';')[0], idNdermarrje))
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgCeljeArkaBankaLlogariaNukEkziston", ci), pnlMesazhi);
                    hfStatusi.Value = "false";
                    nderm.Dispose();
                    return false;
                }
                else
                {
                    if (!DbCore.DbKontabiliteti.clsLlogari.eshteLlogariAktive(txtNrLlogari.Text.Split(';')[0], idNdermarrje))
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgCeljeArkaBankaLlogariaNukEshteAktive", ci), pnlMesazhi);
                        hfStatusi.Value = "false";
                        nderm.Dispose();
                        return false;
                    }
                }
            }
            if (komisioni_ButtonEdit.Text != "")
            {
                if (!DbCore.DbKontabiliteti.clsLlogari.ekzistonLlogari(komisioni_ButtonEdit.Text.Split(';')[0], idNdermarrje))
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgCeljeArkaBankaLlogariaEKomisionitNukEkziston", ci), pnlMesazhi);
                    hfStatusi.Value = "false";
                    nderm.Dispose();
                    return false;
                }
                else
                {
                    if (!DbCore.DbKontabiliteti.clsLlogari.eshteLlogariAktive(komisioni_ButtonEdit.Text.Split(';')[0], idNdermarrje))
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgCeljeArkaBankaLlogariaEKomisionitJoAktive", ci), pnlMesazhi);
                        hfStatusi.Value = "false";
                        nderm.Dispose();
                        return false;
                    }
                }
            }
            
            if (this.cmbDegeAdministrative.Text != "")
            {
                DbCore.DbRegjistrim.clsDegeAdministrative dege = new DbCore.DbRegjistrim.clsDegeAdministrative(cmbDegeAdministrative.Text, idNdermarrje);
                if (dege.IdDegeAdministrative == -1)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgCeljeArkaBankaNukEkzistonDegaAdmin", ci), pnlMesazhi);
                    hfStatusi.Value = "false";
                    nderm.Dispose();
                    return false;
                }
                else
                {
                    dege = new DbCore.DbRegjistrim.clsDegeAdministrative(cmbDegeAdministrative.Text, idNdermarrje);
                    //mag = dbRegjistrim.merrNjesiAdministrativeSipasKodit(mag);
                    if (dege.Aktiv == false)
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgCeljeArkaBankaJoAtkiveDegaAdmin", ci), pnlMesazhi);
                        hfStatusi.Value = "false";
                        nderm.Dispose();
                        return false;
                    }
                }
            }
            DbCore.DbArkaBanka.clsBanka banka = new DbCore.DbArkaBanka.clsBanka();
            banka.KodiBanka = DbCore.clsFunksione.ktheStringunPaHapesira(txtKodi.Text, true);
            banka.IdNdermarje = idNdermarrje;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            mesazh = banka.ekzistonBanke();
            if (mesazh.Status && (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim"))
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                mbushListeBankash(idNdermarrje);
                hfStatusi.Value = "false";
                nderm.Dispose();
                return false;
            }

            if (cmbAutorizimiHf.Value != "")
            {
                string[] pars1 = cmbAutorizimiHf.Value.Split(',');
                for (int i = 0; i < pars1.Length; i++)
                {
                    int idAutorizimKoka = DbCore.DbAdmin.clsAutorizimKoka.ktheIDAutorizim(pars1[i]);
                    if (idAutorizimKoka == 0 || idAutorizimKoka == -1)
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgCeljeArkaBankaNiveliAutorizimitNukEkziston", ci), pnlMesazhi);
                        return false;
                    }
                }
            }
            nderm.Dispose();
            return true;
        }

        protected void ASPxGridView_Bankat_CustomJSProperties(object sender, DevExpress.Web.ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = ASPxGridView_Bankat.PageIndex;
            e.Properties["cpPageRow"] = ASPxGridView_Bankat.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = ASPxGridView_Bankat.VisibleRowCount;
        }

        protected void ASPxGridView_Bankat_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string idkomponente = "";
            string kodkonfigurimi = "";

            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                kodkonfigurimi = arr[1];
                idkomponente = arr[0];
                if (arr[2] == "")
                    if (Request.QueryString["ab"] == "arka")
                    {
                        ASPxGridView_Bankat.FilterExpression = "[LlojArkaBanka]=false";
                    }
                    else
                    {
                        ASPxGridView_Bankat.FilterExpression = "[LlojArkaBanka]=true";
                    }

                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "ASPxGridView_Bankat", komponente, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), int.Parse(cmbKonfigurimi.Value.ToString()));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        ASPxGridView_Bankat.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, ASPxGridView_Bankat);
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
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            if (cmbFiltra.Text == "")
            {
                if (Request.QueryString["ab"] == "arka")
                    ASPxGridView_Bankat.FilterExpression = "[LlojArkaBanka]=false";
                else ASPxGridView_Bankat.FilterExpression = "[LlojArkaBanka]=true";
            }
            ASPxGridView_Bankat.Selection.UnselectAll();

        }


        protected void txtGrupi_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            bool lloji = false;
            if (cmbLloji.Text == "Arka")
                lloji = false;
            else lloji = true;
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("txtGrupi"))
                {
                    ConfigureAspxComboBox.mbushComboGrupeBankash(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), txtGrupi, lloji);
                }
            }
        }

        protected void txtNrLlogari_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].Contains("txtNrLlogari"))
                {
                    ConfigureAspxComboBox.mbushComboLlogariaPaKolona(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), txtNrLlogari,e);
                }
            }

        }
        

        protected void komisioni_ButtonEdit_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("komisioni_ButtonEdit"))
                {
                    ConfigureAspxComboBox.mbushComboLlogariaPaKolona(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), komisioni_ButtonEdit,e);
                }
            }
        }

        protected void txtNrLlogari_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].Contains("txtNrLlogari"))
                {
                    ConfigureAspxComboBox.mbushComboLlogariaPaKolona(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), txtNrLlogari, e);
                }
            }
        }

        
        protected void ASPxGridView_Bankat_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if (e.Column.FieldName == "Komisioni" || e.Column.FieldName == "IdMonedhaBanka" || e.Column.FieldName == "IdLlogariKontabilizimi" || e.Column.FieldName == "IdDegeAdministrative")
            {
                if (Converter.ConvertToInt(e.Value)==0)
                {
                    e.Criteria = null;
                }
            }
        }

        protected void btnXlsxExport_Click(object sender, EventArgs e)
        {

            try
            {
                string emri;
                if (Request.QueryString["ab"] == "arka")
                    emri = "Arkat";
                else emri = "Bankat";
                gridExport.WriteXlsxToResponse(emri, true);
            }
            catch (Exception)
            { }
        }

        protected void btnPdfExport_Click(object sender, EventArgs e)
        {
            try
            {
                string emri;
                if (Request.QueryString["ab"] == "arka")
                    emri = "Arkat";
                else emri = "Bankat";
                gridExport.WritePdfToResponse(emri, true);
            }
            catch (Exception)
            { }
        }
    }
}