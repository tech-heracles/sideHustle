using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using DbCore.DbShare;
using DbCore.DbListPagesat;
using DbCore.IMBUtils.Types;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using System.CodeDom.Compiler;
using DbCore.IMBUtils.Messages;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using DbCore.IMBUtils.Extensions;

namespace PlatinumWeb
{
    public partial class Shto_KomponentePage : MyPageBase
    {

        private int idperdoruesi, idnderviti, idgjuha, idNdermarrje, idviti;
        private const int idstatusdok = 1;
        private string komponente = "Shto_KomponentePage.aspx";
        private string guidString;


        /// <summary>
        /// thirret kur faqja lodohet. Ne te behet kontrolli nese perdoruesi eshte i loguar ne sistem  dhe nqs jo ridrejtohet tek forma e logimit
        /// thirret inicializimi i konfigurimeve fillestare te faqes
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e"> argumenti</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                Response.Redirect(DbCore.IMBUtils.Paths.defaultLoginPath);
            }

            idperdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idperdoruesi);
            }
            idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            idviti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            idnderviti = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
            idgjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (!Page.IsPostBack)
            {
                guidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                hfState.Set("guidString", guidString);
                VendosPerkthime();
                hfState.Set("idGjuha", idgjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                ASPxPageControl1.ActiveTabIndex = 0;
                percaktoTemplateMenu(idgjuha, idviti, idperdoruesi, idNdermarrje, ASPxMenu1);
                konfiguroVleraFillestare(idNdermarrje, idgjuha);
                mbushGridKomponenteNgaDB(idNdermarrje);
                mbushComboBoxFiltra(idNdermarrje, idgjuha, int.Parse(cmbKonfigurimi.Value.ToString()));
                DbCore.mySessionObjects.ruajIdShtimiNeSesion(Session, 0);
                if (Request.QueryString["lloji"] == "false")
                {
                    gvKomponentePage.FilterExpression = "[Lloji]=false";
                }
                else
                {
                    gvKomponentePage.FilterExpression = "[Lloji]=true";
                    gvKomponentePage.SettingsBehavior.AutoExpandAllGroups = true;
                }

                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idperdoruesi, idNdermarrje, idviti, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idperdoruesi, idNdermarrje, idviti,
                    DbCore.clsFunksione.GetKomponente(Page.Request));
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                konfiguroGride(idNdermarrje, idperdoruesi, cmbKonfigurimi.Text.Split(';')[0], 703, rm, ci);
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "gvKomponentePage", gvKomponentePage, cmbKonfigurimi.Text.Split(';')[0], 703.ToString(), DbCore.mySessionObjects.ktheGjuhe(Session));
            }
            else
            {
                guidString = (string)hfState["guidString"];
                mbushGridKomponenteNgaSession(idNdermarrje);
                konfiguroGride(idNdermarrje, idperdoruesi, cmbKonfigurimi.Text.Split(';')[0], 703, rm, ci);
            }
            GridUtil.konfigGrideListeEMadhePaTheme(gvKomponentePage,
                "IdKomponentePage");
            percaktoTemplateMenu(idgjuha, idviti, idperdoruesi, idNdermarrje, ASPxMenu1);
            string konfigValue = cmbKonfigurimi.Value != null ? cmbKonfigurimi.Value.ToString() : clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(cmbKonfigurimi.Text.Split(';')[0], idNdermarrje).ToString();
            mbushComboBoxFiltra(idNdermarrje, idgjuha, int.Parse(konfigValue));
            GridUtil.EmrateButonaveMbiGride(gvKomponentePage);
            AspxWebControlUtils.perkthePopUp(popFshi, MessagesResource.Messages["labelKujdes"], lblMsgbox, MessagesResource.Messages["labelAdministrimiMsgJeniSigurt"], ButtonCancel, MessagesResource.Messages["labelAnullo"]);
        }

        /// <summary>
        /// Vendos emrat e tabeve ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void VendosPerkthime()
        {
            ASPxPageControl1.TabPages[0].Text = MessagesResource.Messages["labelBlerjeShitjeTePergjithshme"];
            ASPxPageControl1.TabPages[1].Text = MessagesResource.Messages["komponenteTab"];
            konfigurimi_Label.Text = MessagesResource.Messages["lblModeli"];
            hfState.Set("msgZgjidhDatenEAktivizimit", MessagesResource.Messages["msgZgjidhDatenEAktivizimit"]);
            hfState.Set("msgPyetjeFshirje", MessagesResource.Messages["msgPyetjeFshirje"]);
            hfState.Set("msgSigurte", MessagesResource.Messages["msgSigurte"]);
            hfState.Set("msgFshikomponenteEkzistuese", MessagesResource.Messages["msgFshikomponenteEkzistuese"]);
            hfState.Set("msgZgjidhniKomponente", MessagesResource.Messages["msgZgjidhniKomponente"]);
            hfState.Set("msgListeKompNdryshimeteParuajtura", MessagesResource.Messages["msgListeKompNdryshimeteParuajtura"]);
            hfState.Set("msgListeKompNdryshimeteParuajturaNdryshimDate",MessagesResource.Messages["msgListeKompNdryshimeteParuajturaNdryshimDate"]);
        }

        /// <summary>
        /// mbush combon e filtrave
        /// </summary>
        /// <param name="idNdermarrje"></param>
        private static void mbushComboBoxFiltra(int idNdermarrje, int idGjuha, int idkonfig)
        {
            clsToolbarConfig.mbushComboBoxFiltraMeValFieldTextField(idGjuha, idNdermarrje, "gvKomponentePage", "Shto_KomponentePage.aspx", "IdFiltra", "FiltraShenime", idkonfig);
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
            clsToolbarConfig.percaktoTemplateMenu(idGjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1,
                DbCore.clsFunksione.GetKomponente(Page.Request), this, MenuInfo, Ruaj_ASPxButton_Click,
                FshiFilter_ASPxButton_Click, hfShtimModifikim.Value != "modifikim", false, false,
                DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
        }

        protected void RuajKolona_Click(object sender, EventArgs e)
        {
            int idfiltri = 0;
            int idkonf = DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(cmbKonfigurimi.Text.Split(';')[0],
                idNdermarrje);
            DbCore.clsMesazh mesazh = GridUtil.ruajFiltra(idNdermarrje, idperdoruesi, idgjuha,
                "gvKomponentePage ", "Shto_KomponentePage.aspx", "FilterDefault", gvKomponentePage.FilterExpression,
                gvKomponentePage, "Kodi", idkonf, out idfiltri); //ruan filtrin e zgjedhur tek filtrat
            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                return;
            }
            mesazh = GridUtil.ruajkonfigurimgride(gvKomponentePage, cmbKonfigurimi.Text, idNdermarrje,
                idperdoruesi, 703, idfiltri, idviti, DbCore.mySessionObjects.ktheCultureInfo(Session),
                DbCore.mySessionObjects.ktheGjuhe(Session)); /// ruan konfigurimin e grides dhe filtrin

            string konfigValue = cmbKonfigurimi.Value != null ? cmbKonfigurimi.Value.ToString() : clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(cmbKonfigurimi.Text.Split(';')[0], idNdermarrje).ToString();
            clsToolbarConfig.mbushComboBoxFiltra(idgjuha, idNdermarrje, "gvKomponentePage ", int.Parse(konfigValue), "Shto_KomponentePage.aspx");

            percaktoTemplateMenu(idgjuha, idviti, idperdoruesi, idNdermarrje, ASPxMenu1);
            if (mesazh.Status) clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
        }

        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(idgjuha, idviti, idperdoruesi, idNdermarrje, ASPxMenu1);
        }

        /// <summary>
        /// perdoret per te shtuar kolonen e selektimit tek grida dhe per te vendosur disa karakteristika te grides
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void gvKomponentePage_DataBound(object sender, EventArgs e)
        {
            // shton colonen # per selektim dhe disa karakteristika te grides
            if (gvKomponentePage.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per 
                //perzgjidh
                GridViewCommandColumn check = new GridViewCommandColumn("#")
                {
                    ShowSelectCheckbox = true,
                    Width = Unit.Percentage(2)
                };
                //behet per te afishuar rreshtin qe do sherbej per filtrim
                gvKomponentePage.Settings.ShowFilterRow = true;
                gvKomponentePage.Settings.ShowFilterRowMenu = true;
                gvKomponentePage.Columns.Add(check);
                gvKomponentePage.KeyFieldName = "IdKomponentePage";
                gvKomponentePage.SettingsBehavior.AllowSelectByRowClick = true;
                gvKomponentePage.SettingsBehavior.AllowFocusedRow = true;
            }
        }

        /// <summary>
        /// perdoret per te konfiguruar griden. Per te dhenat mbi kolonat qe do te shfaqen nga databaza si dhe percakton disa karakteristika te grides
        /// </summary>
        /// :  <see cref="GridUtil.percaktoVisibleColumns"/>
        /// :  <see cref="DbCore.clsFunksione.konfiguroGrideListeMadhe(grida,celesigrides)"/>
        /// <param name="idNdermarrje"></param>
        private void konfiguroGride(int idNdermarrje, int idPerdoruesi, string kodKonfigurimi, int idKomponente, ResourceManager rm, CultureInfo ci)
        {

            KonfigurimComboGride.shtoNjesiPagese(gvKomponentePage, rm, ci);
            KonfigurimComboGride.ShtoLlogariSipasNdermarrjesDhePerdoruesit(gvKomponentePage, idNdermarrje, idPerdoruesi, Session, komponente, guidString, "IdLlogDebi");
            KonfigurimComboGride.ShtoLlogariSipasNdermarrjesDhePerdoruesit(gvKomponentePage, idNdermarrje, idPerdoruesi, Session, komponente, guidString, "IdLlogKredi");
            KonfigurimComboGride.shtoTipPagese(gvKomponentePage, rm, ci);
            KonfigurimComboGride.shtoLlojKomponentePage(gvKomponentePage, rm, ci);
            KonfigurimComboGride.shtoAktiv(gvKomponentePage, rm, ci);
            KonfigurimComboGride.shtoNjesiParam(gvKomponentePage, rm, ci);
            KonfigurimComboGride.shtogrupKomponente(gvKomponentePage, idNdermarrje, Session, komponente, guidString);
            gvKomponentePage.Columns["#"].VisibleIndex = 0;
            if (Request.QueryString["lloji"] == "true")
            {
                gvKomponentePage.GroupBy(gvKomponentePage.Columns["Tipi"]);
                gvKomponentePage.SettingsBehavior.AutoExpandAllGroups = true;
            }
        }



        /// <summary>
        /// perdoret ne rastet kur grida ben callback per ta rimbushur griden
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void gvKomponentePage_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            if (e.CallbackName == "COLUMNMOVE" && gvKomponentePage.AllColumns[int.Parse(e.Args[0])].Width.Value == 0)
                gvKomponentePage.AllColumns[int.Parse(e.Args[0])].Width = Unit.Percentage(3);
            if (e.CallbackName == "APPLYFILTER" && e.Args[0] == "")
            {
                DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
                ASPxComboBox cmbFiltra = ((MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
                cmbFiltra.Text = "";
                if (Request.QueryString["lloji"] == "false")
                {
                    gvKomponentePage.FilterExpression = "[Lloji]=false";
                }
                else
                {
                    gvKomponentePage.FilterExpression = "[Lloji]=true";
                }
            }
            // konfiguroVleraFillestare(); 
            // konfiguroGride(cmbKonfigurimi.Text.Split(';')[0], 703);
        }

        /// <summary>
        /// perdoret per te vendosur filtrat tek headeri i grides
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvKomponentePage_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            if (e.Column.FieldName == "Pershkrimi" || e.Column.FieldName == "ParamEmri")
            {
                e.Values.Clear();
                e.AddValue("(Te gjithe)", string.Empty, "true");
                e.AddValue("Nga A-D ", string.Empty, string.Format("{0}>'A     ' and {0}<'DDDDDDD'", e.Column.FieldName));
                e.AddValue("Nga D-G ", string.Empty, string.Format("{0}>'D     ' and {0}<'GGGGGGG'", e.Column.FieldName));
                e.AddValue("Nga H-K ", string.Empty, string.Format("{0}>'H     ' and {0}<'KKKKKKK'", e.Column.FieldName));
                e.AddValue("Nga L-O ", string.Empty, string.Format("{0}>'L     ' and {0}<'OOOOOOO'", e.Column.FieldName));
                e.AddValue("Nga P-S ", string.Empty, string.Format("{0}>'P     ' and {0}<'SSSSSSS'", e.Column.FieldName));
                e.AddValue("Nga T-W ", string.Empty, string.Format("{0}>'T     ' and {0}<'WWWWWWW'", e.Column.FieldName));
                e.AddValue("Nga X-Z ", string.Empty, string.Format("{0}>'X     ' and {0}<'ZZZZZZZ'", e.Column.FieldName));
            }
            else
            {
                e.Values.Clear();
                e.AddValue("(Te gjithe)", string.Empty, "true");
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
            ASPxComboBox cmbFiltra = ((MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
            //filtra.mbushFiltraGridaSipasFiltraEmri(cmbFiltra.Text, idNdermarrje);
            DbCore.DbAdmin.clsGridaKoka koka =
                new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvKomponentePage",
                    komponente, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session),
                    int.Parse(cmbKonfigurimi.Value.ToString()));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text,
                DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();
                mbushComboBoxFiltra(idNdermarrje, DbCore.mySessionObjects.ktheGjuhe(Session),
                    int.Parse(cmbKonfigurimi.Value.ToString()));
                percaktoTemplateMenu(idgjuha, idviti, idperdoruesi, idNdermarrje, ASPxMenu1);
                if (mesazh.Status)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                cmbFiltra.Text = "";
                if (Request.QueryString["lloji"] == "false")
                {
                    gvKomponentePage.FilterExpression = "[LlojArkaBanka]=false";
                }
                else
                {
                    gvKomponentePage.FilterExpression = "[LlojArkaBanka]=true";
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
            ASPxComboBox cmbFiltra = ((MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;

            DbCore.DbAdmin.clsFiltraGrida filtri = new DbCore.DbAdmin.clsFiltraGrida
            {
                FiltraKodi = cmbFiltra.Text,
                FiltraShenime = cmbFiltra.Text,
                FiltraUniversal = false
            };
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DbCore.DbAdmin.clsGridaKoka koka =
                new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvKomponentePage",
                    komponente, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gvKomponentePage.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("Kodi", gvKomponentePage);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona =
            //    gvKomponentePage.GetSortedColumns();
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
            DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();
            //oPerdorues = (DbCore.DbAdmin.clsPerdorues)(CacheLayer.GlobalCacheManager.MySessionCache["oClsPerdoruesi"]);
            oPerdorues = DbCore.mySessionObjects.kthePerdorues(Session);
            filtri.IdPerdoruesi = oPerdorues.IdPerdorues;
            filtri.IdNdermarje = idNdermarrje;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            filtri.IdStatusDok = 1;

            mesazh = filtri.ruaj();
            mbushComboBoxFiltra(idNdermarrje, DbCore.mySessionObjects.ktheGjuhe(Session),
                int.Parse(cmbKonfigurimi.Value.ToString()));
            percaktoTemplateMenu(idgjuha, idviti, idperdoruesi, idNdermarrje, ASPxMenu1);
            if (mesazh.Status)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            hfStatusi.Value = "true";
            cmbFiltra.Text = "";
        }

        /// <summary>
        /// perdoret per te fshire rreshtat e zgjedhur te bankave nqs perdoruesi konfirmon fshirjen
        /// </summary>
        /// 
        /// <param name="sender"> derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            //fshin rreshtat e selektuar
            string prefixMesazhNjejes = " " + MessagesResource.Messages["msgKomponentja"]; 
            string prefixMesazhShumes = " " + MessagesResource.Messages["msgKomponentet"]; 
            string suffixMesazhNjejesGabimi = " " + MessagesResource.Messages["msgKomponenteDefault"]; 
            string suffixMesazhShumesGabimi = " " + MessagesResource.Messages["msgKomponenteDefaultShumes"];
            string suffixMesazhNjejesGabimiLidhur = " " + MessagesResource.Messages["msgKomponenteLidhur"]; 
            string suffixMesazhShumesGabimiLidhur = " " + MessagesResource.Messages["msgKomponentetLidhur"]; 
            string suffixMesazhNjejesGabimiFormule = " " + MessagesResource.Messages["msgKomponenteGabimFormule"]; 
            string suffixMesazhShumesGabimiFormule = " " + MessagesResource.Messages["msgKomponentetGabimFormule"]; 
            string suffixMesazhNjejesSuksesi = " " + MessagesResource.Messages["msgKomponenteFshi"];
            string suffixMesazhShumesSuksesi = " " + MessagesResource.Messages["msgKomponentetFshi"]; 
            string lidhesMesazhi = " " + MessagesResource.Messages["msgLidhes"] + " ";
            List<object> rreshtat;
            if (ASPxPageControl1.ActiveTabIndex == 0)
                //nqs kemi zgjedhur vetem nje kartele per te pare fshijme vetem ate
                rreshtat = gvKomponentePage.GetSelectedFieldValues("IdKomponentePage", "Kodi");
            else
            {
                rreshtat = new List<object>();
                string[] rreshti = { hfId.Value, txtKodi.Text };
                rreshtat.Add(rreshti);
            }
            //List<object> rreshtat = gvKomponentePage.GetSelectedFieldValues("IdKomponentePage", "Kodi");
            if (rreshtat.Count == 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgKompZgjidh"], pnlMesazhi);
                return;
            }
            //clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "", pnlMesazhi);
            List<string> TeFshire = new List<string>(), TePaFshire = new List<string>();
            List<string> TePaFshiraFormule = new List<string>(), TeLidhur = new List<string>();
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DbCore.DbRegjistrim.clsDatabaseRegjistrim dbregjistrim = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
            foreach (object id in rreshtat)
            {
                if (Convert.ToInt32(((object[])id)[0]) <= 0)
                {
                    string kodi = ((object[])id)[1].ToString();
                    if (kontrollo(kodi))
                    {
                        TePaFshiraFormule.Add(kodi);
                        continue;
                    }
                    hiqNgaGrida(idNdermarrje, kodi);
                    TeFshire.Add(kodi);
                    hfKaNdryshimeNeGride.Value = bool.TrueString;
                    ASPxPageControl1.ActiveTabIndex = 0;
                    hfStatusi.Value = "true";
                    continue;
                }
                DbCore.DbListPagesat.clsKomponentePage komp =
                    new DbCore.DbListPagesat.clsKomponentePage(Convert.ToInt32(((object[])id)[0]));
                DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                konf.mbushKonfigAmbjSipasId(komp.IdKonfig);
                bool lidhur = dbregjistrim.eshteDokumentiILidhurCelje(komp.IdKomponentePage.ToString(),
                    konf.IdNivel.ToString());

                if (komp.Model == 1)
                {
                    TePaFshire.Add(komp.Kodi);
                    continue;
                }
                if (lidhur)
                {
                    TeLidhur.Add(komp.Kodi);
                    continue;
                }
                if (kontrollo(komp.Kodi))
                {
                    TePaFshiraFormule.Add(komp.Kodi);
                    continue;
                }
                if (DbCore.DbListPagesat.clsKomponentePage.ekzistonKomponenteNeFormule(komp.Kodi, komp.IdNdermarje))
                {
                    TePaFshiraFormule.Add(komp.Kodi);
                    continue;
                }
                komp.IdPerdoruesi = idperdoruesi;
                #region Heq llogarite nga grida

                // ASPxGridView_Artikull.DataSource = dt;
                hiqNgaGrida(idNdermarrje, komp.Kodi);
                hfKaNdryshimeNeGride.Value = bool.TrueString;
                #endregion

                TeFshire.Add(komp.Kodi);

                ASPxPageControl1.ActiveTabIndex = 0;
                hfStatusi.Value = "true";

            }
            dbregjistrim.Dispose();
            string mesazhInfoGabim = "", mesazhInfoSukses = "", mesazhGabimFormule = "", mesazhLidhur = "";
            if (TePaFshire.Count == 1)
                mesazhInfoGabim = string.Format("{0}{1}{2}", prefixMesazhNjejes , string.Join(";", TePaFshire),
                    suffixMesazhNjejesGabimi);
            else if (TePaFshire.Count > 1)
                mesazhInfoGabim = string.Format("{0}{1}{2}", prefixMesazhShumes, string.Join(";", TePaFshire),
                    suffixMesazhShumesGabimi);
            if (TeLidhur.Count == 1)
                mesazhLidhur = string.Format("{0}{1}{2}", prefixMesazhNjejes, string.Join(";", TeLidhur),
                    suffixMesazhNjejesGabimiLidhur);
            else if (TeLidhur.Count > 1)
                mesazhLidhur = string.Format("{0}{1}{2}", prefixMesazhShumes, string.Join(";", TeLidhur),
                    suffixMesazhShumesGabimiLidhur);
            if (TePaFshiraFormule.Count == 1)
                mesazhGabimFormule = string.Format("{0}{1}{2}", prefixMesazhNjejes, string.Join(";", TePaFshiraFormule),
                    suffixMesazhNjejesGabimiFormule);
            else if (TePaFshiraFormule.Count > 1)
                mesazhGabimFormule = string.Format("{0}{1}{2}", prefixMesazhShumes, string.Join(";", TePaFshiraFormule),
                    suffixMesazhShumesGabimiFormule);
            if (TeFshire.Count == 1)
                mesazhInfoSukses = string.Format("{0}{1}{2}", prefixMesazhNjejes, string.Join(";", TeFshire),
                    suffixMesazhNjejesSuksesi);
            else if (TeFshire.Count > 1)
                mesazhInfoSukses = string.Format("{0}{1}{2}", prefixMesazhShumes, string.Join(";", TeFshire),
                    suffixMesazhShumesSuksesi);
            mesazhInfoGabim += mesazhGabimFormule + mesazhLidhur;
            if ((mesazhInfoGabim != "") && mesazhInfoSukses != "")
                mesazhInfoGabim += lidhesMesazhi + mesazhInfoSukses;
            if (mesazhInfoGabim != "")
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhInfoGabim, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazhInfoSukses, pnlMesazhi);
            //mbushGridKomponenteNgaDB(idNdermarrje);
            pnlMesazhi.Update();
        }

        /// <summary>
        /// perdoret per te fshire rreshtat e zgjedhur te bankave nqs perdoruesi konfirmon fshirjen
        /// </summary>
        /// 
        /// <param name="sender"> derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ButtonOk2_Click2(object sender, EventArgs e)
        {
            //fshin rreshtat e selektuar

            Page.Validate("entries2");
            ruajDefault(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), true);
        }

        /// <summary>
        /// kontrollon nese kodi eshte perdorur ne formule
        /// </summary>
        /// <param name="kodi"></param>
        /// <returns></returns>
        private bool kontrollo(string kodi)
        {
            foreach (DataRow row in ((DataTable)gvKomponentePage.DataSource).Rows)
            {
                string formula = row["Formula"].ToString();
                if (formula == "")
                    continue;
                formula =
                    formula.Replace("(", "")
                        .Replace(")", "")
                        .Replace("+", "&")
                        .Replace("-", "&")
                        .Replace("*", "&")
                        .Replace("/", "&");
                string[] parametra = formula.Split('&');
                for (int i = 0; i < parametra.Length; i++)
                {
                    if (kodi == parametra[i] && bool.Parse(row["Aktivizimi"].ToString()) && row["ParamKodi"].ToString() != kodi)//parameter formule per nje komponente aktive 
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// heq nga grida rreshtat e fshire
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="kodi"> kodi i reshtit</param>
        private void hiqNgaGrida(int idNdermarrje, string kodi)
        {
            if (gvKomponentePage.DataSource != null)
            {
                DataTable dt = (DataTable)gvKomponentePage.DataSource;
                DataRow[] drs = dt.Select(string.Format("Kodi = '{0}'", kodi));
                if (drs.Length > 1)
                    throw new Exception(MessagesResource.Messages["msgKompDublicate"]);
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                dt.Rows.Remove(dr);
                gvKomponentePage.DataBind();
            }
            else mbushGridKomponenteNgaDB(idNdermarrje);
        }

        /// <summary>
        /// shton nte gride rreshtat e rinj
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="komp">komponentja qe do shtohet</param>
        private void shtoNeGrid(int idNdermarrje, DbCore.DbListPagesat.clsKomponentePage komp)
        {
            if (gvKomponentePage.DataSource != null)
            {
                DataTable dt = (DataTable)gvKomponentePage.DataSource;
                DataRow[] drs = dt.Select(string.Format("Kodi = '{0}'", komp.Kodi));
                if (drs.Length > 0 ||
                    DbCore.DbListPagesat.clsKomponentePage.ekzistonKomponente(komp.Kodi, komp.IdNdermarje, komp.Data))
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgKompEkziston"], pnlMesazhi);
                    hfStatusi.Value = "false";
                    return;
                }
                object[] newrow =
                {
                    komp.IdKomponentePage, komp.Kodi, komp.Pershkrimi, komp.Njesi, komp.ParamKodi,
                    komp.ParamEmri, komp.Formula, komp.IdLlogDebi, komp.IdLlogKredi, komp.Aktivizimi, komp.Tipi,
                    komp.Lloji, komp.Model, komp.Data, komp.IdNdermarje, komp.IdPerdoruesi, komp.IdStatusDok,
                    komp.IdKonfig, komp.DtKrijimi, komp.DtModifikimi, cmbLlogDebi.Text, cmbLlogKredi.Text,
                    komp.ParamNjesi, komp.AplikoPageMuaji, komp.AplikoDiteMuaji, komp.ShfaqDefault,
                    komp.IdGrupKomponente, komp.LejoModVlere, komp.LlogaritGjithmone, cmbGrup.Text, komp.IdGrupNivel1, komp.IdGrupNivel2, komp.IdRenditje, komp.shenime
                };
                dt.Rows.Add(newrow);
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["msgKomponentjaShtim"], pnlMesazhi);
                hfStatusi.Value = "true";
            }
            else mbushGridKomponenteNgaDB(idNdermarrje);
        }

        /// <summary>
        /// modifikon ne gride komponenten e modifikuar
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="komp">komponentja e modifikuar</param>
        private void modifikoNeGrid(int idNdermarrje, DbCore.DbListPagesat.clsKomponentePage komp)
        {
            if (gvKomponentePage.DataSource != null)
            {
                DataTable dt = (DataTable)gvKomponentePage.DataSource;
                DataRow[] drs = dt.Select(string.Format("Kodi = '{0}'", komp.Kodi));
                if (drs.Length > 1)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgKompEkziston"], pnlMesazhi);
                    hfStatusi.Value = "false";
                    return;
                }
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                object[] arr =
                {
                    komp.IdKomponentePage, komp.Kodi, komp.Pershkrimi, komp.Njesi, komp.ParamKodi,
                    komp.ParamEmri, komp.Formula, komp.IdLlogDebi, komp.IdLlogKredi, komp.Aktivizimi, komp.Tipi,
                    komp.Lloji, komp.Model, komp.Data, komp.IdNdermarje, komp.IdPerdoruesi, komp.IdStatusDok,
                    komp.IdKonfig, komp.DtKrijimi, komp.DtModifikimi, cmbLlogDebi.Text, cmbLlogKredi.Text,
                    komp.ParamNjesi, komp.AplikoPageMuaji, komp.AplikoDiteMuaji, komp.ShfaqDefault,
                    komp.IdGrupKomponente, komp.LejoModVlere, komp.LlogaritGjithmone, cmbGrup.Text,komp.IdGrupNivel1,komp.IdGrupNivel2,komp.IdRenditje,komp.shenime
                };
                dr.ItemArray = arr;
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["msgKompModifikimSukses"], pnlMesazhi);
                hfStatusi.Value = "true";
            }
            else mbushGridKomponenteNgaDB(idNdermarrje);
        }

        /// <summary>
        /// perdoret per te trajtuar ngjarjet e butonave te menuse. Shkon ne faqen e modifikimit te bankave kur perdoruesi klikon butonin modifiko
        /// ose ne faqen e shtimit te bankave kur perdoruesi klikon butonin shto
        /// </summary>
        /// <param name="source"> derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {
            //kryen veprimet e menuse
            if (e.Item.Name == "Ruaj")
            {
                Page.Validate("entries");
                ruajKomponente();
            }
            else if (e.Item.Name == "Default")
            {
            }
            else if (e.Item.Name == "RuajList")
            {
                Page.Validate("entries2");

                ruajDefault(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), false);
            }
        }

        /// <summary>
        /// ruan listen e komponenteve
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="aktivizodef"> tregon nqs true te ruhen komponentet default nqs false ato qe jane ne gride</param>
        private void ruajDefault(int idNdermarrje, bool aktivizodef)
        {
            DbCore.DbListPagesat.clsKomponentePage komp = new DbCore.DbListPagesat.clsKomponentePage();
            DbCore.DbAdmin.clsNdermarrje nderm = new DbCore.DbAdmin.clsNdermarrje(idNdermarrje);
            var lloji = Convert.ToBoolean(Request.QueryString["lloji"]);
            var dtAktivizimi = dteDateAkt.Date;
            if (!Page.IsValid) return;
            if (isValidKomponenteAll(dtAktivizimi, idNdermarrje, lloji))
            {
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                if (aktivizodef) //komponentet default jane te ndryshme per ndermarje buxhetore dhe ndermarjet normale
                    if (nderm.Lloji == 2)
                        mesazh = DbCore.DbListPagesat.clsKomponentePage.ruajDefault(dtAktivizimi, lloji, idNdermarrje, idperdoruesi, -2);
                    else
                        mesazh = DbCore.DbListPagesat.clsKomponentePage.ruajDefault(dtAktivizimi, lloji, idNdermarrje, idperdoruesi, -1);
                else
                    mesazh = komp.ruajKomp((DataTable)gvKomponentePage.DataSource, dtAktivizimi, lloji, idNdermarrje, idperdoruesi);
                if (!mesazh.Status)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                    hfStatusi.Value = "false";
                }
                else
                {
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                    ConfigureAspxComboBox.mbushComboDataKomponente(idNdermarrje, cmbNdryshimi,
                        Convert.ToBoolean(Request.QueryString["lloji"]));

                    mbushGridKomponenteNgaDB(idNdermarrje);
                    cmbNdryshimi_pnlNdryshimi.Update();
                    ASPxPageControl1.ActiveTabIndex = 0;
                    hfStatusi.Value = "true";
                    hfKaNdryshimeNeGride.Value = bool.FalseString;
                }
            }
        }

        /// <summary>
        /// konfiguron vlerat fillestare
        /// </summary>
        /// <param name="idNdermarrje"></param>
        private void konfiguroVleraFillestare(int idNdermarrje, int idGjuha)
        {

            //mbush komboboxet dhe gridat e faqes
            ASPxPageControl1.ActiveTabIndex = 0;
            AspxWebControlUtils.vendosDateEditMask(dteDateAkt);
            ConfigureAspxComboBox.shtoKolonaPerLlogarineSelectVetemNr(cmbLlogDebi);
            ConfigureAspxComboBox.shtoKolonaPerLlogarineSelectVetemNr(cmbLlogKredi);
            ConfigureAspxComboBox.KonfiguroComboBoxNjesiPagese(cmbNjesia);
            ConfigureAspxComboBox.mbushComboNjesiParamPagese(cmbNjesiaParam);
            ConfigureAspxComboBox.mbushComboTipePagese(cmbTipi);
            ConfigureAspxComboBox.mbushComboGjendja(cmbAktivizimi);
            ConfigureAspxComboBox.percaktoTemplateComboMeEnableCallback(cmbGrup);
            ConfigureAspxComboBox.percaktoTemplateComboMeEnableCallback(cmbLlogDebi);
            ConfigureAspxComboBox.percaktoTemplateComboMeEnableCallback(cmbLlogKredi);
            dteDateAkt.Date = DbCore.mySessionObjects.merrPeriudheKontabel(Session).FillimiPeriudha;
            ConfigureAspxComboBox.mbushComboDataKomponente(idNdermarrje, cmbNdryshimi,
                Convert.ToBoolean(Request.QueryString["lloji"]));

            if (Request.QueryString["lloji"] == "false")
            {
                mbushComboKonfigurimeshSipasKategorise(cmbKonfigurimi, 34, "KP", idGjuha);
            }
            else mbushComboKonfigurimeshSipasKategorise(cmbKonfigurimi, 34, "KLP", idGjuha);
            cmbKonfigurimi.SelectedIndex = 0;
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()), idgjuha);
            hfKonffillestar.Value = string.Format("{0};{1}", konf.KodKonfigAmbjente, konf.PershkrimKonfigAmbjente);
            // cmbKonfigurimi.SelectedIndex = -1;
        }

        /// <summary>
        /// mbush kombon e konfigurimit sipas nivelit
        /// </summary>
        /// <param name="combo">komboja</param>
        /// <param name="kat">kategoria</param>
        /// <param name="nivel">niveli</param>
        public void mbushComboKonfigurimeshSipasKategorise(ASPxComboBox combo, int kat, string nivel, int idGjuha)
        {
            //mbush griden e popupit me te dhena
            DbCore.DbShare.colKonfigurimAmbjenti col = new DbCore.DbShare.colKonfigurimAmbjenti();
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            if (kat != 0)
            {
                konf.IdKategori = kat;
                konf.IdNdermarje = idNdermarrje;
                //DbCore.DbRegjistrim.clsNivelRegjistrimi niv = new DbCore.DbRegjistrim.clsNivelRegjistrimi();
                //niv.mbushNivelRegjistrimiSipasKodiMeKonvertime(nivel, idNdermarrje);
                int idNiveli = DbCore.DbRegjistrim.clsNivelRegjistrimi.ktheIdNivelRegjistrimiSipasKodi(nivel,
                    idNdermarrje);
                col.mbushKonfigAmbjSipasIdKategoriIdNivelMeLloj(konf.IdKategori, idNiveli, idperdoruesi);
            }
            else
            {
                col.mbushGjitheKonfigurimeAmbjentesh(idNdermarrje, idperdoruesi, 1, idGjuha); //celje
            }
            ListBoxColumn colprove = new ListBoxColumn { FieldName = "KodKonfigAmbjente", Caption = "Kodi" };
            ListBoxColumn colemer = new ListBoxColumn { FieldName = "PershkrimKonfigAmbjente", Caption = "Pershkrimi" };
            combo.TextFormatString = "{0};{1}";
            combo.Columns.Add(colprove);
            combo.Columns.Add(colemer);
            combo.DataSource = col;
            combo.ValueField = "IdKonfigAmbjente";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        /// <summary>
        /// mbush griden nga sesioni
        /// </summary>
        /// <param name="idNdermarrje"></param>
        private void mbushGridKomponenteNgaSession(int idNdermarrje)
        {
            DataTable tmpObject;
            bool sukses = DbCore.mySessionObjects.merrGrideNgaSessioni(Session, out tmpObject);
            if (!sukses)
                mbushGridKomponenteNgaDB(idNdermarrje);
            else
            {
                gvKomponentePage.DataSource = tmpObject;
                gvKomponentePage.DataBind();
                tmpObject.Dispose();
            }
        }

        /// <summary>
        /// mbush griden nga databaza
        /// </summary>
        /// <param name="idNdermarrje"></param>
        private void mbushGridKomponenteNgaDB(int idNdermarrje)
        {
            //mbush griden e popupit me te dhena            
            DataTable dt =
                DbCore.DbListPagesat.colKomponentePage.merrKomponentePageNdermarjeDTSipasLlojitDheDates(idNdermarrje,
                    bool.Parse(Request.QueryString["lloji"]), Convert.ToDateTime(cmbNdryshimi.Value));
            DbCore.mySessionObjects.ruajGrideNeSession(Session, dt);
            gvKomponentePage.DataSource = dt;
            gvKomponentePage.DataBind();
            dt.Dispose();
        }

        /// <summary>
        /// ruan komponentet duke e shtuar ne gride por jo ne db
        /// </summary>
        private void ruajKomponente()
        {
            DbCore.DbListPagesat.clsKomponentePage komp;
            if (!Page.IsValid) return;

            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            if (isValidKomponente(idNdermarrje))
            {

                komp = krijoKomponente(idNdermarrje);
                //   komp.shenime = txtShenime.Text;
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idperdoruesi, idNdermarrje,
                    DbCore.mySessionObjects.ktheIdVitNdermarrje(Session),
                    DbCore.clsFunksione.GetKomponente(Page.Request));

                if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim")
                {
                    if (!tedrejtaInfo.DShtim)
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejtaVeprimi"], pnlMesazhi);
                        hfStatusi.Value = "false";
                        return;
                    }
                    //int id = int.Parse(CacheLayer.GlobalCacheManager.MySessionCache["idshtimi"].ToString());
                    int id = DbCore.mySessionObjects.merrIdShtimiNgaSesioni(Session);
                    komp.IdKomponentePage = id;
                    shtoNeGrid(idNdermarrje, komp);
                    hfKaNdryshimeNeGride.Value = bool.TrueString;
                    //CacheLayer.GlobalCacheManager.MySessionCache["idshtimi"] = id - 1;
                    DbCore.mySessionObjects.ruajIdShtimiNeSesion(Session, id - 1);
                }
                else
                {
                    if (!tedrejtaInfo.DMod)
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejtaVeprimi"], pnlMesazhi);
                        hfStatusi.Value = "false";
                        return;
                    }
                    komp.IdKomponentePage = int.Parse(hfId.Value);
                    DbCore.DbListPagesat.clsKomponentePage kompivjeter =
                        new DbCore.DbListPagesat.clsKomponentePage(komp.IdKomponentePage);
                    komp.IdGrupNivel1 = kompivjeter.IdGrupNivel1;
                    komp.IdGrupNivel2 = kompivjeter.IdGrupNivel2;
                    komp.IdRenditje = kompivjeter.IdRenditje;
                    if (kompivjeter.Model != 0)
                        komp.Model = kompivjeter.Model;
                    //komp.shenime = kompivjeter.shenime;
                    modifikoNeGrid(idNdermarrje, komp);
                    hfKaNdryshimeNeGride.Value = bool.TrueString;
                }
            }

        }

        /// <summary>
        /// krijon komponenten sipas te dhenave
        /// </summary>
        /// <returns> komponenten me te dhenat</returns>
        /// <param name="idNdermarrje"></param>
        private DbCore.DbListPagesat.clsKomponentePage krijoKomponente(int idNdermarrje)
        {
            //krijon nje banke sipas te dhenave te futura nga perdoruesi
            int iddebi;
            int idkredi;
            int tipi = 0;

            if (cmbLlogDebi.Text != "")
                iddebi = DbCore.DbKontabiliteti.clsLlogari.mbushIDLlogariSipasKodit(cmbLlogDebi.Text, idNdermarrje);
            else
                iddebi = 0;
            if (cmbLlogKredi.Text != "")
                idkredi = DbCore.DbKontabiliteti.clsLlogari.mbushIDLlogariSipasKodit(cmbLlogKredi.Text, idNdermarrje);
            else
                idkredi = 0;
            int idgrup;
            if (cmbGrup.Text != "")
            {
                DbCore.DbListPagesat.clsGrupKomponente grup = new DbCore.DbListPagesat.clsGrupKomponente(cmbGrup.Text,
                    idNdermarrje);
                idgrup = grup.Id;
            }
            else
                idgrup = 0;
            if (Request.QueryString["lloji"] == "false")
                tipi = Convert.ToInt32(DbCore.DbListPagesat.TipPagese.Undefined);
            else
                tipi = Convert.ToInt32(cmbTipi.Value);

            DbCore.DbShare.clsKonfigurimAmbjenti konfig = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konfig.mbushKonfigAmbjSipasKod(cmbKonfigurimi.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));

            DbCore.DbListPagesat.clsKomponentePage komp = new DbCore.DbListPagesat.clsKomponentePage(0, txtKodi.Text,
                txtPershkrimi.Text, Convert.ToInt32(cmbNjesia.Value), txtParamKodi.Text, txtParamEmri.Text,
                txtFormula.Text, iddebi, idkredi, Convert.ToBoolean(cmbAktivizimi.Value), tipi,
                bool.Parse(Request.QueryString["lloji"]), Convert.ToInt32(DbCore.DbListPagesat.Model.NgaPerdoruesi),
                Convert.ToDateTime(cmbNdryshimi.Text), idperdoruesi, idNdermarrje, konfig.IdKonfigAmbjente, idstatusdok,
                Convert.ToInt32(cmbNjesiaParam.Value), cbAplikoPage.Checked, cbAplikoDite.Checked,
                cbShfaqDefault.Checked, idgrup, cbLejoModVlere.Checked, cbLlogaritGjithmone.Checked, 0, 0, 999, txtShenime.Text);
            //  komp.shenime = txtShenime.Text;
            return komp;
        }

        /// <summary>
        /// kontrollon nese te dhenat qe jane plotesuara jane te lejueshme apo jo
        /// </summary>
        /// <returns> true ose false</returns>
        /// <param name="idNdermarrje"></param>
        private bool isValidKomponente(int idNdermarrje)
        {
            if (!DbCore.IMBUtils.Validation.CodeProvider.emerIVlefshemVariable(this.txtKodi.Text))
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgKodiKomponentes"], pnlMesazhi);
                return false;
            }

            if (cmbLlogDebi.Text != "")
            {
                if (!DbCore.DbKontabiliteti.clsLlogari.ekzistonLlogari(cmbLlogDebi.Text.Split(';')[0], idNdermarrje))
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgLlogariDebi"], pnlMesazhi);
                    hfStatusi.Value = "false";
                    return false;
                }
                if (!DbCore.DbKontabiliteti.clsLlogari.eshteLlogariAktive(cmbLlogDebi.Text.Split(';')[0], idNdermarrje))
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgLlogariDebiJoAktive"], pnlMesazhi);
                    hfStatusi.Value = "false";
                    return false;
                }
            }
            if (cmbLlogKredi.Text != "")
            {
                if (!DbCore.DbKontabiliteti.clsLlogari.ekzistonLlogari(cmbLlogKredi.Text.Split(';')[0], idNdermarrje))
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgLlogariKredi"], pnlMesazhi);
                    hfStatusi.Value = "false";
                    return false;
                }
                if (!DbCore.DbKontabiliteti.clsLlogari.eshteLlogariAktive(cmbLlogKredi.Text.Split(';')[0], idNdermarrje))
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgLlogariKrediJoAktive"], pnlMesazhi);
                    hfStatusi.Value = "false";
                    return false;
                }
            }
            if (cmbGrup.Text != "")
            {
                if (!DbCore.DbListPagesat.clsGrupKomponente.ekziston(cmbGrup.Text, idNdermarrje))
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["grupimiNukEkziston"], pnlMesazhi);
                    hfStatusi.Value = "false";
                    return false;
                }
            }
            if (cmbNjesia.Text == DbCore.DbListPagesat.NjesiPagese.For.ToString() && txtFormula.Text == "")
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgFormula"], pnlMesazhi);
                hfStatusi.Value = "false";
                return false;
            }
            //if (cmbNjesia.Text == DbCore.DbListPagesat.NjesiPagese.For.ToString() && (cmbTipi.Text == DbCore.DbListPagesat.TipPagese.Ndalese.ToString() || cmbTipi.Text == DbCore.DbListPagesat.TipPagese.Pagese.ToString()) && txtParamKodi.Text == "")
            //{
            //    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Jepni parametrin e formules per komponenten!", pnlMesazhi);
            //    hfStatusi.Value = "false";
            //    return false;
            //}

            if (cmbNjesia.Text == DbCore.DbListPagesat.NjesiPagese.For.ToString() && txtParamKodi.Text != "")
            {
                if (!kontrolloparameter(idNdermarrje))
                    return false;
            }
            if (cmbAktivizimi.Text == "Inaktive")
            {
                if (kontrollo(txtKodi.Text))
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgKomponentjaEshteNeFormuleNukBehetInaktive"], pnlMesazhi);
                    hfStatusi.Value = "false";
                    return false;
                }
                else if (txtParamKodi.Text != "" && kontrollo(txtParamKodi.Text))
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgParamiKomponentesEshteNeFormuleNukBehetInaktive"], pnlMesazhi);
                    hfStatusi.Value = "false";
                    return false;
                }
            }
            if (cmbNjesia.Text == DbCore.DbListPagesat.NjesiPagese.For.ToString() && txtFormula.Text != "")
            {
                return kontrolloformule(idNdermarrje);
            }
            
            return true;
        }

        private bool kontrolloparameter(int idNdermarrje)
        {
            string param = txtParamKodi.Text;
            string kodi = txtKodi.Text;

            if (param == string.Empty)
                return true;

            foreach (DataRow row in ((DataTable)gvKomponentePage.DataSource).Rows)
            {
                if (param == row["ParamKodi"].ToString() && kodi != row["Kodi"].ToString())
                /// eshte kod ekzistues ne gride por akoma nuk eshte ruajtur
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["parametriEkzistonTeKomponente"] + " " +  row["Kodi"],
                        pnlMesazhi);
                    hfStatusi.Value = "false";
                    return false;
                }
                if (param == row["Kodi"].ToString()) /// eshte kod ekzistues ne gride por akoma nuk eshte ruajtur
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["parametriEkzistonSiKomponente"], pnlMesazhi);
                    hfStatusi.Value = "false";
                    return false;
                }
            }

            if (DbCore.DbListPagesat.clsKomponentePage.ekzistonKomponente(param, idNdermarrje, !Convert.ToBoolean(Request.QueryString["lloji"]))) //kontrollohet ne baze per komponente te llojit tjeter pasi per kete lloj komponente u kontrollua lart ne gride
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["parametriEkzistonSiKomponente"], pnlMesazhi);
                hfStatusi.Value = "false";
                return false;
            }
            if (DbCore.DbListPagesat.clsKomponentePage.ekzistonKomponenteParametri(param, idNdermarrje, kodi, !Convert.ToBoolean(Request.QueryString["lloji"])))
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["parametriEkzistonteKomponenteTjeter"], pnlMesazhi);
                hfStatusi.Value = "false";
                return false;
            }

            return true;
        }

        /// <summary>
        /// kontrollon nese formula eshte e sakte nga ana matematikore
        /// </summary>
        /// <returns>true ose false</returns>
        /// <param name="idNdermarrje"></param>
        private bool kontrolloformule(int idNdermarrje)
        {
            string formula = txtFormula.Text;
            string param = txtParamKodi.Text;
            int klapafillimi = formula.Count(f => f == '(');
            int klapambarimi = formula.Count(f => f == ')');
            if (klapafillimi != klapambarimi)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgKllapat"], pnlMesazhi);
                hfStatusi.Value = "false";
                return false;
            }
            formula = formula.Replace("(", "")
                    .Replace(")", "")
                    .Replace("+", "&")
                    .Replace("-", "&")
                    .Replace("*", "&")
                    .Replace("/", "&")
                    .Replace("?", "&")
                    .Replace(">", "&")
                    .Replace(":", "&");
            string[] parametra = formula.Split('&');
            for (int i = 0; i < parametra.Length; i++)
            {
                if (parametra[i] == "")
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgFormulaGabim"], pnlMesazhi);
                    hfStatusi.Value = "false";
                    return false;
                }
                if (!parametra.Contains(param)&& (!String.IsNullOrEmpty(param)))
                    {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgParameterNukEkziston"], pnlMesazhi);
                    hfStatusi.Value = "false";
                    return false;
                }
                if (parametra[i] == param) // eshte parametri i komponentes
                    continue;
                if (parametra[i] == "DP") // eshte dp
                    continue;
                double nr = 0;
                if (double.TryParse(parametra[i], out nr)) // eshte nr
                    continue;
                bool ekziston = false;
                bool inaktive = false;
                foreach (DataRow row in ((DataTable)gvKomponentePage.DataSource).Rows)
                {
                    if (parametra[i] == row["Kodi"].ToString())
                    /// eshte kod ekzistues ne gride por akoma nuk eshte ruajtur
                    {
                        if (!bool.Parse(row["Aktivizimi"].ToString()))
                        {
                            inaktive = true;
                            break;
                        }
                        ekziston = true;
                        break;
                    }
                    if (parametra[i] == row["ParamKodi"].ToString())
                    /// eshte kod parametri ekzistues ne gride por akoma nuk eshte ruajtur
                    {
                        if (!bool.Parse(row["Aktivizimi"].ToString()))
                        {
                            inaktive = true;
                            break;
                        }
                        ekziston = true;
                        break;
                    }
                }
                if (ekziston)
                    continue;
                if (inaktive)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgKomponentjaEshteInaktive"], pnlMesazhi);
                    return false;
                }
                    
                if (!DbCore.DbListPagesat.clsKomponentePage.ekzistonKomponente(parametra[i], idNdermarrje, !Convert.ToBoolean(Request.QueryString["lloji"])) &&
                    !DbCore.DbListPagesat.clsKomponentePage.ekzistonKomponenteParametri(parametra[i], idNdermarrje,
                        txtKodi.Text, !Convert.ToBoolean(Request.QueryString["lloji"]))) /// kontrollohet ne baze per komponente te llojit tjeter pasi per kete lloj komponente u kontrollua lart ne gride
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgParameterNukEkziston"], pnlMesazhi);
                    hfStatusi.Value = "false";
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// kontrollon nese eshte zgjedhur data e aktivizimit apo jo
        /// </summary>
        /// <returns> true ose false</returns>
        private bool isValidKomponenteAll(DateTime dtAktivizimi, int idNdermarrje, bool lloji)
        {
            if (dteDateAkt.Text == "")
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgDateAktivizimi"], pnlMesazhi);
                hfStatusi.Value = "false";
                return false;
            }

            var dataKomponetesh = colKomponentePage.merrDataKomponente(idNdermarrje, lloji);
            var dtPasardhese = Utils.MerrDatenMeTeAfertPasardhese(dtAktivizimi, dataKomponetesh);
            if (clsKokaListPagese.ekzistonListePagesa(dtAktivizimi, dtPasardhese, idNdermarrje))
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNdryshim"] +  dtAktivizimi.ToShortDateString() + MessagesResource.Messages["msgDateTjeterAktivizimi"], pnlMesazhi);
                hfStatusi.Value = "false";
                return false;
            }
            return true;
        }

        /// <summary>
        /// cakton properti ne javascript
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvKomponentePage_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gvKomponentePage.PageIndex;
            e.Properties["cpPageRow"] = gvKomponentePage.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gvKomponentePage.VisibleRowCount;
        }

        /// <summary>
        /// kur grida ben callback
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvKomponentePage_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string idkomponente = "";
            string kodkonfigurimi = "";

            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                kodkonfigurimi = arr[1];
                idkomponente = arr[0];
                if (arr[2] == "")
                    if (Request.QueryString["lloji"] == "false")
                    {
                        gvKomponentePage.FilterExpression = "[Lloji]=false";
                    }
                    else
                    {
                        gvKomponentePage.FilterExpression = "[Lloji]=true";
                    }

                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    //filtra.mbushFiltraGridaSipasFiltraKodi(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    DbCore.DbAdmin.clsGridaKoka koka =
                        new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvKomponentePage",
                            komponente, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session),
                            int.Parse(cmbKonfigurimi.Value.ToString()));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2],
                        DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        gvKomponentePage.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvKomponentePage);
                        hfStatusi.Value = "true";
                    }
                    else hfStatusi.Value = "false";
                }
            }
            else if (arr.Length == 2) //nese eshte zgjedhur nje konfigurim tek combo e konfigurimeve
            {
                kodkonfigurimi = arr[1];
                idkomponente = arr[0];

                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "gvKomponentePage", gvKomponentePage, kodkonfigurimi, idkomponente.ToString(), (int)hfState["idGjuha"]);
                gvKomponentePage.Columns["#"].VisibleIndex = 0;
            }
            else
            {
                idkomponente = e.Parameters;
            }
            if (e.Parameters == "ndryshodate")
            {
                mbushGridKomponenteNgaDB(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            }
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            if (cmbFiltra.Text == "")
            {
                if (Request.QueryString["lloji"] == "false")
                    gvKomponentePage.FilterExpression = "[Lloji]=false";
                else gvKomponentePage.FilterExpression = "[Lloji]=true";
            }
            gvKomponentePage.Selection.UnselectAll();
        }

        protected void cmbLlogDebi_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].Contains("cmbLlogDebi"))
                {
                    ConfigureAspxComboBox.mbushComboLlogariaPaKolona(DbCore.mySessionObjects.ktheIdPerdoruesi(Session),
                        DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), cmbLlogDebi, e);
                }
            }
        }

        protected void cmbLlogKredi_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].Contains("cmbLlogKredi"))
                {
                    ConfigureAspxComboBox.mbushComboLlogariaPaKolona(DbCore.mySessionObjects.ktheIdPerdoruesi(Session),
                        DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), cmbLlogKredi, e);
                }
            }
        }

        /// <summary>
        /// per filtrimin e llogarise debi
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void cmbLlogDebi_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].Contains("cmbLlogDebi"))
                {
                    ConfigureAspxComboBox.mbushComboLlogariaPaKolona(DbCore.mySessionObjects.ktheIdPerdoruesi(Session),
                        DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), cmbLlogDebi, e);
                }
            }
        }

        protected void cmbGrup_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("cmbGrup"))
                cmbGrup.ConfigureAndFill(() => new colGrupKomponente(IdNdermarrja), "Kodi", "Id");
        }

        /// <summary>
        /// per filtrimin e llogarise kredi
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void cmbLlogKredi_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].Contains("cmbLlogKredi"))
                {
                    ConfigureAspxComboBox.mbushComboLlogariaPaKolona(DbCore.mySessionObjects.ktheIdPerdoruesi(Session),
                        DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), cmbLlogKredi, e);
                }
            }
        }

        /// <summary>
        /// per filtrimin me elementin bosh
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvKomponentePage_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if (e.Column.FieldName == "IdLlogDebi" || e.Column.FieldName == "IdLlogKredi" ||
                e.Column.FieldName == "Tipi" || e.Column.FieldName == "IdGrupKomponente")
            {
                if (Converter.ConvertToInt(e.Value) == 0)
                {
                    e.Criteria = null;
                }
            }
            if (e.Column.FieldName == "ParamNjesi")
            {
                if (e.Value == "")
                {
                    e.Criteria = null;
                }
            }
        }
    }
}