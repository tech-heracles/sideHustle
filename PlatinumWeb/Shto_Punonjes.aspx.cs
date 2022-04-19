using DbCore;
using DbCore.DbAdmin;
using DbCore.DbArkaBanka;
using DbCore.DbKontabiliteti;
using DbCore.DbListPagesat;
using DbCore.DbQendraKosto;
using DbCore.DbRegjistrim;
using DbCore.DbShare;
using DevExpress.Data;
using DevExpress.Web;
using Newtonsoft.Json;
using NLog;
using PlatinumWeb.Templates;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlphaWebCommon.WebUtils.ASPxControlExtensions;
using DbCore.IMBUtils.Cache;
using DbCore.IMBUtils.Extensions;
using DbCore.IMBUtils.Logging;
using DbCore.IMBUtils.Types;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{
    public partial class Shto_Punonjes : MyPageBase
    {
        public const string punonjesitSessionKey = "gvPunonjesitSession";
        public const string qendraKostoSessionKey = "gvQkSessionKey";
        public const string bandSessionKey = "gvBandaSessionKey";
        public const string filtraSessionKey = "filtartSessionKey";
        private int idperdoruesi;
        private int idnderviti;
        private int idviti;
        private int idNdermarrje;
        private int idgjuha;
        private int idKonfig;
        private const int idstatusdok = 1;
        private ResourceManager _rm;
        private CultureInfo _ci;
        private CultureInfo cultinf => _ci ?? (_ci = MessagesResource.KtheCultureInfo(mySessionObjects.ktheGjuhe(Session)));
        private ResourceManager rm => _rm ?? (_rm = MessagesResource.CurrentResourceManager);
        private int idVitNdermarrje;
        private string komponente = "Shto_Punonjes.aspx";
        private string guidString;
        /// <summary>
        /// thirret kur faqja lodohet. Ne te behet kontrolli nese perdoruesi eshte i ImbLoggerar ne sistem  dhe nqs jo ridrejtohet tek forma e logimit
        /// thirret inicializimi i konfigurimeve fillestare te faqes
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e"> argumenti</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!mySessionObjects.isLogedIn(Session))
            {
                Response.Redirect(DbCore.IMBUtils.Paths.defaultLoginPath);
            }
            idperdoruesi = mySessionObjects.ktheIdPerdoruesi(Session);
            if (mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idperdoruesi);
            }
            if (!Page.IsPostBack)
            {
                ClearSessionCache();
                idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
                idviti = mySessionObjects.ktheIdVitNdermarrje(Session);
                idgjuha = mySessionObjects.ktheGjuhe(Session);
                idnderviti = mySessionObjects.ktheNdermarrjeVit(Session);
                hfId.Value = "0";
                hfState.Set("idGjuha", idgjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("idViti", idviti);
                hfState.Set("idNderVit", idnderviti);
                guidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                hfState.Set("guidString", guidString);
                EmrateTabeve();
                EmrateLabelave();
                vendosHfMePerkthime();
                ASPxPageControl1.ActiveTabIndex = 0;
                percaktoTemplateMenu(idgjuha, idviti, idperdoruesi, idNdermarrje, ASPxMenu1);
                konfiguroVleraFillestare(idNdermarrje, idgjuha);
                int.Parse(cmbKonfigurimi.Value.ToString());

                //mbushComboBoxFiltra(idgjuha, idNdermarrje, idKonfig);
                mbushGrIdPunonjesNgaDB(idNdermarrje, idgjuha);
                konfiguroGridePunonjesit(idNdermarrje, cmbKonfigurimi.Text.Split(';')[0], 708, rm, cultinf);
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "gvPunonjesit", gvPunonjesit, cmbKonfigurimi.Text.Split(';')[0], 708.ToString(), mySessionObjects.ktheGjuhe(Session));

                mbushPunesim(0);
                mbushQendra(0);
                mbushBanda(0);
                mbushPagaShtesa(idNdermarrje);
                mbushKomponenteListPagese(idNdermarrje);
                konfiguroGridePunesim(idNdermarrje);
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "gvPunesim", gvPunesim, cmbKonfigurimiPun.Text.Split(';')[0], "708", mySessionObjects.ktheGjuhe(Session), false);
                konfiguroGrideQendra(idNdermarrje);
                konfiguroGrideBanda(idNdermarrje);
                
                hfLejoNrLlogBank.Value = (clsAlternativaKushti.getAlternativa(Convert.ToInt32(cmbKonfigurimi.Value), "LSHNRB") == "Po").ToString();

                //Session.Add("idshtimi", 0);
                mySessionObjects.ruajIdShtimiNeSesion(Session, 0);
                clsTeDrejtaRoli tedrejtaInfo = new clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idperdoruesi, idNdermarrje, idviti, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idperdoruesi, idNdermarrje, idviti, komponente);
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);


                int idMonedheZgjedhur = clsFunksione.ktheMonedhePerFormatNumri(idgjuha, idKonfig, idNdermarrje, 708, "", -1, true);
                clsFormatiKonfig formatNrPerKonfig = new clsFormatiKonfig();
                formatNrPerKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(int.Parse(cmbKonfigurimi.Value.ToString()));
                clsFormatKonfigTrup formatMonedhe = DbCore.DbShare.clsFormatKonfigTrup.ktheFormatNumriMonedhe(formatNrPerKonfig, idMonedheZgjedhur);
                hfState.SetObject("formatMonedhe", formatMonedhe);
                mySessionObjects.ruajFormatVlefta(formatMonedhe.ShifraPasPresjesVlefta, Session);

                GridUtil.konfigGrideListeEMadhePaTheme(gvPunonjesit, "IdPunonjes");
                mbushComboBoxFiltraNgaDb(idgjuha, idNdermarrje, idKonfig);
                konfigurimi_Label.Text = MessagesResource.Messages["lblModeli"];
            }
            else
            {
                ClearSessionCache();
                guidString = (string)hfState["guidString"];
                idKonfig = int.Parse(cmbKonfigurimi.Value.ToString());
                idNdermarrje = (int)hfState["idNdermarrje"];
                idgjuha = (int)hfState["idGjuha"];
                idviti = (int)hfState["idViti"];
                idnderviti = (int)hfState["idNderVit"];
                mbushGridPunonjesNgaSession(idNdermarrje, idgjuha);
                konfiguroGridePunonjesit(idNdermarrje, cmbKonfigurimi.Text.Split(';')[0], 708, rm, cultinf);
                mbushPunesimSession();
                mbushQendraSession();
                mbushBandaSession();
                mbushGridKomponenteshNgaSessioni();
                konfiguroGridePunesim(idNdermarrje);
                // mbushComboBoxFiltra(idgjuha, idNdermarrje, idKonfig);
            }
            percaktoTemplateMenu(idgjuha, idviti, idperdoruesi, idNdermarrje, ASPxMenu1);
            konfiguroGrideKomponenteListPagese(idNdermarrje);
            konfiguroGridePagaShtesa(idNdermarrje);
            AspxWebControlUtils.perkthePopUp(popFshi, MessagesResource.Messages["labelKujdes"], lblMsgbox, MessagesResource.Messages["labelAdministrimiMsgJeniSigurt"], ButtonCancel, MessagesResource.Messages["labelAnullo"]);
            gvPunonjesit.PercaktoTitlePanel(Page, MenuInfo, pnlMesazhi, hfState, idperdoruesi, idNdermarrje, idviti, idgjuha, idKonfig, komponente, 708, "Detyra", rm, cultinf);
            gvQendra.PercaktoTitlePanel(this, MenuInfo, pnlMesazhi, hfState, idperdoruesi, idNdermarrje, idviti, idgjuha, 1, komponente, rm, cultinf);
            gvBanda.PercaktoTitlePanel(this, MenuInfo, pnlMesazhi, hfState, idperdoruesi, idNdermarrje, idviti, idgjuha, 1, komponente, rm, cultinf);
            gvPunesim.PercaktoTitlePanel(this, MenuInfo, pnlMesazhi, hfState, idperdoruesi, idNdermarrje, idviti, idgjuha, int.Parse(cmbKonfigurimiPun.Value.ToString()), komponente, rm, cultinf);
            gvKomponenteListPagese.PercaktoTitlePanel(this, MenuInfo, pnlMesazhi, hfState, idperdoruesi, idNdermarrje, idviti, idgjuha, 1, komponente, rm, cultinf);
            
            clsToolbarConfig.mbushComboBoxFiltra(idgjuha, idNdermarrje, "gvPunonjesit", idKonfig, komponente);
        }

        public static void ClearSessionCache(params String[] keys)
        {
            CacheDataProvider.ClearSessionCache("StrukturaAdministrative", "colStrukturatAdministrative");
        }

        private void vendosHfMePerkthime()
        {
            hfState.Set("msgPunonjesDuhetTeZgjidhni1Punonjes", MessagesResource.Messages["msgPunonjesDuhetTeZgjidhni1Punonjes"]);
            hfState.Set("msgZgjidhniTipinEKontrates", MessagesResource.Messages["msgZgjidhniTipinEKontrates"]);
            hfState.Set("msgLlogariaBankareDuhetTePermbajVetemNumra", MessagesResource.Messages["msgLlogariaBankareDuhetTePermbajVetemNumra"]);
            hfState.Set("msgZgjidhniBankenLupa", MessagesResource.Messages["msgZgjidhniBankenLupa"]);
            hfState.Set("msgLupaLlogariShpejteZgjidhniGrupin", MessagesResource.Messages["msgLupaLlogariShpejteZgjidhniGrupin"]);
            hfState.Set("msgZgjidhniNenDepartamentin", MessagesResource.Messages["msgZgjidhniNenDepartamentin"]);
            hfState.Set("msgZgjidhniDepartamentin", MessagesResource.Messages["msgZgjidhniDepartamentin"]);
            hfState.Set("msgZgjidhniObjektivenEKostos", MessagesResource.Messages["msgZgjidhniObjektivenEKostos"]);
            hfState.Set("msgZgjdhniNjeNgaElementetEListes", MessagesResource.Messages["msgZgjdhniNjeNgaElementetEListes"]);
            hfState.Set("msgZgjidhProfesionin", MessagesResource.Messages["msgZgjidhProfesionin"]);
            hfState.Set("msgZgjidhPozicionin", MessagesResource.Messages["msgZgjidhPozicionin"]);
            hfState.Set("msgZgjidhKodProfesionin", MessagesResource.Messages["msgZgjidhKodProfesionin"]);
            hfState.Set("headerPopUpZgjidhQendrenKostos", MessagesResource.Messages["headerPopUpZgjidhQendrenKostos"]);
            hfState.Set("msgZgjidhGrupimLokalGlobal", MessagesResource.Messages["msgZgjidhGrupimLokalGlobal"]);
            hfState.Set("msgPyetjeFshirjePunesim", MessagesResource.Messages["msgPyetjeFshirjePunesim"]);
            hfState.Set("msgZgjidhniPunesim", MessagesResource.Messages["msgZgjidhniPunesim"]);
            hfState.Set("regjisDokZgjidhDokPerTeBashkengjitur", MessagesResource.Messages["regjisDokZgjidhDokPerTeBashkengjitur"]);
            hfState.Set("msgZgjidhniLlogarine", MessagesResource.Messages["msgZgjidhniLlogarine"]);
        }

        /// <summary>
        /// Vendos emrat e tabeve ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateTabeve()
        {
            for (int i = 0; i < ASPxPageControl1.TabPages.Count; i++)
            {
                clsDrejtaTabi tabi = new clsDrejtaTabi();
                tabi.merrTeDrejtaTabi(idviti, idperdoruesi, idNdermarrje, ASPxPageControl1.TabPages[i].Name);
                ASPxPageControl1.TabPages[i].ClientVisible = tabi.DAmb;
                if (i == 3 || i == 4)
                {
                    hftabe.Add(i + "Fshi", tabi.DFsh);
                    hftabe.Add(i + "Mod", tabi.DMod && tabi.DShtim);
                }
                hftabe.Add(i.ToString(), tabi.DFsh && tabi.DMod && tabi.DShtim);
            }

            ASPxPageControl1.TabPages[0].Text = MessagesResource.Messages["TePergjithshmeTab"];
            ASPxPageControl1.TabPages[1].Text = MessagesResource.Messages["MenuItemPunonjes"];
            ASPxPageControl1.TabPages[2].Text = MessagesResource.Messages["kontaktiEmergjencesTab"];
            ASPxPageControl1.TabPages[3].Text = MessagesResource.Messages["punesimetTab"];
            ASPxPageControl1.TabPages[4].Text = MessagesResource.Messages["HistorikuPunTab"];
            ASPxPageControl1.TabPages[5].Text = MessagesResource.Messages["QendraKostoTab"];
            ASPxPageControl1.TabPages[6].Text = MessagesResource.Messages["BandaKostoTab"];
            ASPxPageControl1.TabPages[7].Text = MessagesResource.Messages["pagatDheShtesaTab"];
            ASPxPageControl1.TabPages[8].Text = MessagesResource.Messages["komponenteListPageTab"];
        }

        private void EmrateLabelave()
        {
            ((ASPxButton)ASPxPageControl1.TabPages[6].FindControl("btnshow")).Text = MessagesResource.Messages["btnshow"];
        }
        /// <summary>
        /// mbush combon e filtrave
        /// </summary>
        /// <param name="idNdermarrje"></param>
        private void mbushComboBoxFiltra(int idGjuha, int idNdermarrje, int idkonfig)
        {

            var ngaSessioni = mySessionObjects.merrObjectNgaSesioni(Session, filtraSessionKey) as Tuple<clsGridaKoka, colFiltratGrida>;
            if (ngaSessioni == null || ngaSessioni.Item1.IdKonfigurim != idkonfig)
            {
                //nese eshte hera e pare qe hapet 
                mbushComboBoxFiltraNgaDb(idGjuha, idNdermarrje, idkonfig);
            }
            var model = new DbCore.MyMenuFilterModel
            {
                ValueField = "IdFiltra",
                TextField = "FiltraShenime",
                DataSource = ngaSessioni.Item2
            };
            clsFiltraGrida filtriDef = DbCore.clsFunksione.merrFilterDefault(idkonfig);
            if (filtriDef != null) model.ValueToSelect = filtriDef.FiltraKodi;
            mySessionObjects.RuajNeSession(System.Web.HttpContext.Current.Session, model, $"filtraGride_{idkonfig}");
        }
        private void mbushComboBoxFiltraNgaDb(int idGjuha, int idNdermarrje, int idkonfig)
        {
            clsGridaKoka koka = new clsGridaKoka(idGjuha, "gvPunonjesit", komponente, idNdermarrje, idkonfig);
            colFiltratGrida colFiltra = new colFiltratGrida(koka.IdGridaKoka, idNdermarrje);
            colFiltra.Insert(0, new clsFiltraGrida());
            mySessionObjects.ruajObjectNeSesion(Session, new Tuple<clsGridaKoka, colFiltratGrida>(koka, colFiltra), filtraSessionKey);
            var model = new DbCore.MyMenuFilterModel
            {
                ValueField = "IdFiltra",
                TextField = "FiltraShenime",
                DataSource = colFiltra
            };
            clsFiltraGrida filtriDef = DbCore.clsFunksione.merrFilterDefault(idkonfig);
            if (filtriDef != null) model.ValueToSelect = filtriDef.FiltraKodi;  
            mySessionObjects.RuajNeSession(System.Web.HttpContext.Current.Session, model, $"filtraGride_{idkonfig}");
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
            clsToolbarConfig.percaktoTemplateMenu(idGjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1, komponente, this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, hfShtimModifikim.Value == "modifikim" ? false : true, false, false, mySessionObjects.merrEshteMemeSesioni(Session));
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
        protected void gvPunonjesit_DataBound(object sender, EventArgs e)
        {// shton colonen # per selektim dhe disa karakteristika te grides
            if (gvPunonjesit.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per
                //perzgjidh
                GridViewCommandColumn check = new GridViewCommandColumn("#") { ShowSelectCheckbox = true, Width = Unit.Percentage(2) };
                //behet per te afishuar rreshtin qe do sherbej per filtrim
                gvPunonjesit.Settings.ShowFilterRow = true;
                gvPunonjesit.Settings.ShowFilterRowMenu = true;
                gvPunonjesit.Columns.Add(check);
                gvPunonjesit.KeyFieldName = "IdPunonjes";
                gvPunonjesit.SettingsBehavior.AllowSelectByRowClick = true;
                gvPunonjesit.SettingsBehavior.AllowFocusedRow = true;
            }
            if (gvPunonjesit.Columns["IdNenDepartament"].GetType() == typeof(GridViewDataComboBoxColumn))
                mbushNenDepartamentet();
        }

        /// <summary>
        /// perdoret per te konfiguruar griden. Per te dhenat mbi kolonat qe do te shfaqen nga databaza si dhe percakton disa karakteristika te grides
        /// </summary>
        /// :  <see cref="GridUtil.percaktoVisibleColumns"/>
        /// :  <see cref="DbCore.clsFunksione.konfiguroGrideListeMadhe(grida,celesigrides)"/>
        /// <param name="idNdermarrje"></param>
        private void konfiguroGridePunonjesit(int idNdermarrje, string kodKonfigurimi, int idKomponente, System.Resources.ResourceManager rm, CultureInfo ci)
        {
            hfState.Set("SHFAQNENDEP", clsAlternativaKushti.getAlternativa(Convert.ToInt32(cmbKonfigurimi.Value), "SHFAQNENDEP"));

            shtoDepartament(idNdermarrje);
            shtoNenDepartament(idNdermarrje);
            shtoKombesia();

            KonfigurimComboGride.shtoEdukimi(gvPunonjesit, idNdermarrje, idgjuha, Session, komponente, guidString);
            KonfigurimComboGride.shtoPuna(gvPunonjesit, rm, ci);
            KonfigurimComboGride.shtoSeks(gvPunonjesit, rm, ci);
            KonfigurimComboGride.shtoGrupPunonjes(gvPunonjesit, idNdermarrje, Session, komponente, guidString);
            GridViewDataDateColumn col = gvPunonjesit.Columns["Datelindja"] as GridViewDataDateColumn;
            AspxWebControlUtils.vendosDateEditMask(col.PropertiesDateEdit);
            gvPunonjesit.Columns["IdNenDepartament"].Visible = (hfState.Get("SHFAQNENDEP").ToString() == "Po");
            gvPunonjesit.Columns["IdNenDepartament"].ShowInCustomizationForm = (hfState.Get("SHFAQNENDEP").ToString() != "Po");
            gvPunonjesit.Columns["#"].VisibleIndex = 0;
        }

        /// <summary>
        /// perdoret ne rastet kur grida ben callback per ta rimbushur griden
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void gvPunonjesit_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            gvPunonjesit.JSProperties["cpCallbackCompleted"] = true;
        }

        /// <summary>
        /// perdoret per te vendosur filtrat tek headeri i grides
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvPunonjesit_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            CultureInfo ci = mySessionObjects.ktheCultureInfo(Session);

            string TeGjithe = MessagesResource.Messages["GridHeaderFilterFillItemTeGjithe"];
            string nga = MessagesResource.Messages["GridHeaderFilterFillItemNga"];
            if (e.Column.FieldName == "NrPersonal" || e.Column.FieldName == "Emer" || e.Column.FieldName == "Mbiemer")
            {
                e.Values.Clear();
                e.AddValue(TeGjithe, string.Empty, "true");
                e.AddValue(nga + " A-D ", string.Empty, String.Format("{0}>'A     ' and {0}<'DDDDDDD'", e.Column.FieldName));
                e.AddValue(nga + " D-G ", string.Empty, String.Format("{0}>'D     ' and {0}<'GGGGGGG'", e.Column.FieldName));
                e.AddValue(nga + " H-K ", string.Empty, String.Format("{0}>'H     ' and {0}<'KKKKKKK'", e.Column.FieldName));
                e.AddValue(nga + " L-O ", string.Empty, String.Format("{0}>'L     ' and {0}<'OOOOOOO'", e.Column.FieldName));
                e.AddValue(nga + " P-S ", string.Empty, String.Format("{0}>'P     ' and {0}<'SSSSSSS'", e.Column.FieldName));
                e.AddValue(nga + " T-W ", string.Empty, String.Format("{0}>'T     ' and {0}<'WWWWWWW'", e.Column.FieldName));
                e.AddValue(nga + " X-Z ", string.Empty, String.Format("{0}>'X     ' and {0}<'ZZZZZZZ'", e.Column.FieldName));
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
            var itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;

            clsFiltraGrida filtra = new clsFiltraGrida();
            int idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
            //filtra.mbushFiltraGridaSipasFiltraEmri(cmbFiltra.Text, idNdermarrje);
            clsGridaKoka koka = new clsGridaKoka(mySessionObjects.ktheGjuhe(Session), "gvPunonjesit", komponente, mySessionObjects.merrIdNdermarrjeSesioni(Session), int.Parse(cmbKonfigurimi.Value.ToString()));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = mySessionObjects.ktheIdPerdoruesi(Session);
                clsMesazh mesazh = new clsMesazh();
                mesazh = filtra.fshi();
                clsToolbarConfig.mbushComboBoxFiltra(mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvPunonjesit", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
                
                percaktoTemplateMenu(idgjuha, idviti, idperdoruesi, idNdermarrje, ASPxMenu1);

                if (mesazh.Status)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                cmbFiltra.Text = "";
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
            var itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;

            clsFiltraGrida filtri = new clsFiltraGrida
            { FiltraKodi = cmbFiltra.Text, FiltraShenime = cmbFiltra.Text, FiltraUniversal = false };
            int idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
            clsGridaKoka koka = new clsGridaKoka(mySessionObjects.ktheGjuhe(Session), "gvPunonjesit", komponente, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gvPunonjesit.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("IdPunonjes", gvPunonjesit);
        
            filtri.IdPerdoruesi = idperdoruesi;
            filtri.IdNdermarje = idNdermarrje;
            clsMesazh mesazh = new clsMesazh();
            filtri.IdStatusDok = 1;

            mesazh = filtri.ruaj();
            clsToolbarConfig.mbushComboBoxFiltra(mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvPunonjesit", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
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
        {//fshin rreshtat e selektuar
            List<object> rreshtat;
            if (ASPxPageControl1.ActiveTabIndex == 0)//nqs kemi zgjedhur vetem nje kartele per te pare fshijme vetem ate
                rreshtat = gvPunonjesit.GetSelectedFieldValues("IdPunonjes", "NrPersonal");
            else
            {
                rreshtat = new List<object>();
                string[] rreshti = { hfId.Value, txtNrPersonal.Text };
                rreshtat.Add(rreshti);
            }
            // List<object> rreshtat = gvPunonjesit.GetSelectedFieldValues("IdPunonjes", "NrPersonal");
            CultureInfo ci = mySessionObjects.ktheCultureInfo(Session);

            if (rreshtat.Count == 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgPunonjesZgjidhniTePakten1Punonjes"], pnlMesazhi);
                return;
            }
            //clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "", pnlMesazhi);
            List<string> TeFshire = new List<string>(), TePaFshire = new List<string>();
            clsMesazh mesazh = new clsMesazh();
            int idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
            clsDatabaseRegjistrim dbregjistrim = new clsDatabaseRegjistrim();
            foreach (object id in rreshtat)
            {
                clsPunonjes punonjes = new clsPunonjes(Convert.ToInt32(((object[])id)[0]));
                clsKonfigurimAmbjenti konf = new clsKonfigurimAmbjenti();
                konf.mbushKonfigAmbjSipasId(punonjes.IdKonfig);
                bool lidhur = dbregjistrim.eshteDokumentiILidhurCelje(punonjes.IdPunonjes.ToString(), konf.IdNivel.ToString());
                if (lidhur)
                {
                    TePaFshire.Add(punonjes.NrPersonal);
                    continue;
                }
                punonjes.IdPerdoruesi = idperdoruesi;
                mesazh = punonjes.fshi();
                if (punonjes.IdPunonjes == 0) continue;
                if (mesazh.Status)
                {
                    #region Heq llogarite nga grida

                    // ASPxGridView_Artikull.DataSource = dt;
                    hiqNgaGrida(idNdermarrje, punonjes.IdPunonjes, rm, ci);

                    #endregion Heq llogarite nga grida

                    TeFshire.Add(punonjes.NrPersonal);

                    ASPxPageControl1.ActiveTabIndex = 0;
                    hfStatusi.Value = "true";
                }
            }
            dbregjistrim.Dispose();
            string mesazhInfoGabim = "", mesazhInfoSukses = "";
            if (TePaFshire.Count == 1)
                mesazhInfoGabim = String.Format("{0}{1} {2}", MessagesResource.Messages["msgPunonjesiPrefixNjejes"], String.Join(";", TePaFshire), MessagesResource.Messages["msgShtoArtikullSuffixNjejesGabimi"]);
            else
                 if (TePaFshire.Count > 1)
                mesazhInfoGabim = String.Format("{0}{1} {2}", MessagesResource.Messages["msgPunonjesiPrefixShumes"], String.Join(";", TePaFshire), MessagesResource.Messages["msgShtoArtikullSuffixShumesGabimi"]);
            if (TeFshire.Count == 1)
                mesazhInfoSukses = String.Format("{0}{1} {2}", MessagesResource.Messages["msgPunonjesiPrefixNjejes"], String.Join(";", TeFshire), MessagesResource.Messages["regjMagSuffixMesazhNjejesSuksesi"]);
            else
                if (TeFshire.Count > 1)
                mesazhInfoSukses = String.Format("{0}{1} {2}", MessagesResource.Messages["msgPunonjesiPrefixShumes"], String.Join(";", TeFshire), MessagesResource.Messages["msgCeljeMagazinatSuffixShumesSuksesi"]);
            if (mesazhInfoGabim != "" && mesazhInfoSukses != "")
                mesazhInfoGabim += $" {MessagesResource.Messages["msgLidhes"]} {mesazhInfoSukses}";
            if (mesazhInfoGabim != "")
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhInfoGabim, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazhInfoSukses, pnlMesazhi);
        }

        /// <summary>
        /// heq nga grida rreshtat e fshire
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="id"> id e reshtit</param>
        private void hiqNgaGrida(int idNdermarrje, int id, ResourceManager rm, CultureInfo ci)
        {
            if (gvPunonjesit.DataSource != null)
            {
                DataTable dt = (DataTable)gvPunonjesit.DataSource;
                DataRow[] drs = dt.Select(String.Format("IdPunonjes = {0}", id));
                if (drs.Length > 1)
                    throw new MyException(MessagesResource.Messages["msgPunonjesiNdodhen2PunonjesMeTeNjejtenIDNeGride"]);
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                dt.Rows.Remove(dr);
                gvPunonjesit.DataBind();
            }
            else mbushGrIdPunonjesNgaDB(idNdermarrje, idgjuha);
        }

        /// <summary>
        /// shton nte gride rreshtat e rinj
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="id">id e punonjesit</param>
        private void shtoNeGrid(int idNdermarrje, int id, ResourceManager rm, CultureInfo ci)
        {
            if (gvPunonjesit.DataSource != null)
            {
                DataTable dt = (DataTable)gvPunonjesit.DataSource;
                DataRow[] drs = dt.Select(String.Format("IdPunonjes = {0}", id));
                if (drs.Length > 0)
                    throw new MyException(MessagesResource.Messages["msgPunonjesiNdodhen2PunonjesMeTeNjejtenIDNeGride"]);
                DataRow newArtDr = colPunonjes.merrPunonjesitDR(id);
                dt.ImportRow(newArtDr);
            }
            else mbushGrIdPunonjesNgaDB(idNdermarrje, idgjuha);
        }

        /// <summary>
        /// modifikon ne gride komponenten e modifikuar
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="id">id e punonjesit</param>
        private void modifikoNeGrid(int idNdermarrje, int id, ResourceManager rm, CultureInfo ci)
        {
            if (gvPunonjesit.DataSource != null)
            {
                DataTable dt = (DataTable)gvPunonjesit.DataSource;
                DataRow[] drs = dt.Select(String.Format("IdPunonjes = {0}", id));
                if (drs.Length > 1)
                {
                    throw new MyException(MessagesResource.Messages["msgPunonjesiNdodhen2PunonjesMeTeNjejtenIDNeGride"]);
                }
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                DataRow newArtDr = colPunonjes.merrPunonjesitDR(id);
                //  dt.Rows.Remove(dr);
                // dt.ImportRow(newArtDr);
                object[] arr = newArtDr.ItemArray;
                dr.ItemArray = arr;
            }
            else mbushGrIdPunonjesNgaDB(idNdermarrje, idgjuha);
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
                ruajPunonjes(rm, ci);
            }
            if (e.Item.Name == "RuajPunesim")
            {
                var mesazh = ruajPunesim(mySessionObjects.merrIdNdermarrjeSesioni(Session));

                if (mesazh)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

            }
        }

        private void shtoKombesia()
        {
            var txtField = idgjuha == 0 ? "PershkrimiShq" : "Pershkrimi";

            gvPunonjesit.KonfiguroCombo("Kombesia", "Id", txtField, () => clsPunonjes.merrKombesiaDT()
            , Session, komponente, guidString);
        }



        /// <summary>
        /// shton ne gride departamentin ne forme komboje
        /// </summary>
        /// <param name="idNdermarrje"></param>
        private void shtoDepartament(int idNdermarrje)
        {

            var oldColumn = gvPunonjesit.Columns["IdDepartament"];
            var comboColumn = oldColumn as GridViewDataComboBoxColumn;
            if (comboColumn == null)
            {
                comboColumn = GridUtil.KrijoGridViewDataComboBoxColumnSipasKolonesEkzistuese(oldColumn);
                gvPunonjesit.Columns.Remove(oldColumn);
                comboColumn.PropertiesComboBox.TextField = "Emri";
                comboColumn.PropertiesComboBox.ValueField = "IdStrukturaAdm";
                comboColumn.FieldName = "IdDepartament";
                comboColumn.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                comboColumn.PropertiesComboBox.ClientSideEvents.SelectedIndexChanged = "function(s,e){hfState.Set('cmbIdPrindi', s.GetValue());hfState.Set('NdryshoiDepi',true);}";
                gvPunonjesit.Columns.Add(comboColumn);
            }
            colStrukturatAdministrative col = new colStrukturatAdministrative();
            if (hfState.Get("SHFAQNENDEP").ToString() == "Po")
                col.mbushGjitheStrukturaAdmPrindiSipasNdermarjes(idNdermarrje, true);
            else
                col.mbushGjitheStrukturaAdmSipasNdermarjes(idNdermarrje);
            mySessionObjects.ruajObjectNeSesion(Session, col, "strukturaAdminCombo");
            col.Insert(0, new clsStrukturaAdministrative(0, "", "", "", "", "", 0, 0, 0, 0, 0, 0, 0, false));
            comboColumn.PropertiesComboBox.DataSource = col;

        }

        /// <summary>
        /// shton ne gride nendepartamentin ne forme komboje
        /// </summary>
        /// <param name="idNdermarrje"></param>
        private void shtoNenDepartament(int idNdermarrje)
        {
            var oldColumn = gvPunonjesit.Columns["IdNenDepartament"];
            var comboColumn = oldColumn as GridViewDataComboBoxColumn;
            if (comboColumn == null)
            {
                comboColumn = GridUtil.KrijoGridViewDataComboBoxColumnSipasKolonesEkzistuese(oldColumn);
                gvPunonjesit.Columns.Remove(oldColumn);
                comboColumn.PropertiesComboBox.TextField = "Emri";
                comboColumn.PropertiesComboBox.ValueField = "IdStrukturaAdm";
                comboColumn.FieldName = "IdNenDepartament";
                comboColumn.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                gvPunonjesit.Columns.Add(comboColumn);
            }

            colStrukturatAdministrative col = new colStrukturatAdministrative();
            colStrukturatAdministrative colPrinderit = (mySessionObjects.merrObjectNgaSesioni(Session, "strukturaAdminCombo") as colStrukturatAdministrative);
            CacheDataProvider.ClearSessionCache("sipasPrinderve");
            if (hfState.Get("SHFAQNENDEP").ToString() == "Po")
                col.mbushStrukturaAdmSipasShumePrind(string.Join(",", colPrinderit.Select(x => x.IdStrukturaAdm).ToArray()));

            col.Insert(0, new clsStrukturaAdministrative(0, "", "", "", "", "", 0, 0, 0, 0, 0, 0, 0, false));
            comboColumn.PropertiesComboBox.DataSource = col;
        }

        private void mbushNenDepartamentet()
        {
            int idPrindi = 0;
            bool ndryshoiDepi = false;

            if (hfState.Contains("cmbIdPrindi"))
                idPrindi = Convert.ToInt32(hfState.Get("cmbIdPrindi"));
            if (hfState.Contains("NdryshoiDepi"))
                ndryshoiDepi = Convert.ToBoolean(hfState.Get("NdryshoiDepi"));

            colStrukturatAdministrative col = new colStrukturatAdministrative();
            if (idPrindi == 0)
            {
                colStrukturatAdministrative colPrinderit = (mySessionObjects.merrObjectNgaSesioni(Session, "strukturaAdminCombo") as colStrukturatAdministrative);

                if (hfState.Get("SHFAQNENDEP").ToString() == "Po")
                    col.mbushStrukturaAdmSipasShumePrind(string.Join(",", colPrinderit.Select(x => x.IdStrukturaAdm).ToArray()));
            }
            else
                col.mbushStrukturaAdmSipasPrindit(idPrindi, true);
            col.Insert(0, new clsStrukturaAdministrative(0, "", "", "", "", "", 0, 0, 0, 0, 0, 0, 0, false));
            (gvPunonjesit.Columns["IdNenDepartament"] as GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = col;

            if (ndryshoiDepi)
                gvPunonjesit.AutoFilterByColumn(gvPunonjesit.Columns["IdNenDepartament"], "0");
        }

        /// <summary>
        /// konfiguron vlerat fillestare
        /// </summary>
        /// <param name="idNdermarrje"></param>
        private void konfiguroVleraFillestare(int idNdermarrje, int idGjuha)
        { //mbush komboboxet dhe gridat e faqes
            ASPxPageControl1.ActiveTabIndex = 0;
            dteDateAkt.Date = mySessionObjects.merrPeriudheKontabel(Session).FillimiPeriudha;
            dteDateAk2.Date = dteDateAkt.Date.Date;
            AspxWebControlUtils.vendosDateEditMask(dteDatelindja, dteDateAktPun, dteDateAktQK, dteDateAktBanda, dteDtPerfundimi, dteDtFillimi, dteDtLargimi, dteDtNenshkrimi, dteDateAkt, dteDateAk2);
            ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idperdoruesi, idNdermarrje, cmbKonfigurimi, 37, rm, cultinf, idGjuha);
            ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idperdoruesi, idNdermarrje, cmbKonfigurimiPun, 109, rm, cultinf, idGjuha);
            ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idperdoruesi, idNdermarrje, cmbKonfigurimiKomp, 118, rm, cultinf, idGjuha);
            ConfigureAspxComboBox.percaktoTemplateComboMeEnableCallback(cmbObjektiva, cmbQK1, cmbKodeProfesione, cmbQK2, cmbLocal);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmbProfesioni, cmbTitull, cmbTipKontrate, cmbDepartamenti, cmbBanka, cmbGrupi, cmbNenDepartamenti, cmbGlobal);
            ConfigureAspxComboBox.mbushComboGrupimeGlobalLocal(cmbGlobal, idNdermarrje, 1, "");
            ConfigureAspxComboBox.mbushComboProfesioneTituj(cmbProfesioni, idNdermarrje, 1, idgjuha);
            ConfigureAspxComboBox.KonfiguroComboBoxComboQytete(IdNdermarrja, cmbQyteti);
            ConfigureAspxComboBox.mbushComboGrupePunonjesish(cmbGrupi, idNdermarrje);
            ConfigureAspxComboBox.mbushComboArsye(txtArsyeja);
            ConfigureAspxComboBox.shtokolonavendodhje(cmbVendndodhjet);
            ConfigureAspxComboBox.mbushComboVendndodhjet(idNdermarrje, cmbVendndodhjet);
            ConfigureAspxComboBox.mbushComboGjinia(cmbGjinia, cultinf, rm);
            ConfigureAspxComboBox.mbushComboStatusi(cmbStatusi, cultinf, rm);
            ConfigureAspxComboBox.mbushComboEdukimi(cmbEdukimi, idNdermarrje, idgjuha);
            ConfigureAspxComboBox.mbushComboKombesi(cmbKombesia, idgjuha);
            ConfigureAspxComboBox.mbushComboNdryshimPozicioni(cmbNryshimPozicioni, idgjuha);
            ConfigureAspxComboBox.KonfiguroComboBoxPunaMeparshme(cmbPunaMeparshme);
            ConfigureAspxComboBox.mbushComboMonedha(idperdoruesi, idNdermarrje, false, cmbMonedha);
            ConfigureAspxComboBox.mbushComboSkemaSigurimi(cmbSkemaSigurimi, idNdermarrje, dteDateAk2.Date);
            ConfigureAspxComboBox.mbushComboLlojPagese(cmbLlojPagese);
            ConfigureAspxComboBox.mbushComboTipKontrate(cmbTipKontrate, idNdermarrje, idgjuha);
            ConfigureAspxComboBox.mbushComboStrukturaAdm(cmbDepartamenti, 0, idNdermarrje);
            ConfigureAspxComboBox.mbushComboProfesioneTituj(cmbTitull, idNdermarrje, 2, idgjuha);
            ConfigureAspxComboBox.KonfiguroComboBoxObjektivaKosto(cmbObjektiva, IdNdermarrja);
            ConfigureAspxComboBox.mbushComboBankat(idperdoruesi, idNdermarrje, cmbBanka, false);
            ConfigureAspxComboBox.mbushComboLlogaria(idNdermarrje, idperdoruesi, cmbIdLlogari);
            ConfigureAspxComboBox.percaktoTemplateComboMeEnableCallback(cmbIdLlogari);
            cmbKonfigurimi.SelectedIndex = 0;
            cmbKonfigurimiPun.SelectedIndex = 0; cmbKonfigurimiKomp.SelectedIndex = 0;
            clsKonfigurimAmbjenti konf = new clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()), idgjuha);
            hfKonffillestar.Value = String.Format("{0};{1}", konf.KodKonfigAmbjente, konf.PershkrimKonfigAmbjente);
            // cmbKonfigurimi.SelectedIndex = -1;
            idKonfig = konf.IdKonfigAmbjente;
        }

        /// <summary>
        /// mbush griden nga sesioni
        /// </summary>
        /// <param name="idNdermarrje"></param>
        private void mbushGridPunonjesNgaSession(int idNdermarrje, int idGjuha)
        {
            var objNgaSessioni = mySessionObjects.merrObjectNgaSesioni(Session, punonjesitSessionKey) as DataTable;
            if (objNgaSessioni == null)
                mbushGrIdPunonjesNgaDB(idNdermarrje, idGjuha);
            else
            {
                gvPunonjesit.DataSource = objNgaSessioni;
                gvPunonjesit.DataBind();
                objNgaSessioni.Dispose();
            }
        }

        /// <summary>
        /// mbush griden nga databaza
        /// </summary>
        /// <param name="idNdermarrje"></param>
        private void mbushGrIdPunonjesNgaDB(int idNdermarrje, int idGjuha)
        {//mbush griden e popupit me te dhena
            var dt = colPunonjes.merrPunonjesNdermarjeDT(idNdermarrje, idGjuha);
            mySessionObjects.ruajObjectNeSesion(Session, dt, punonjesitSessionKey);
            gvPunonjesit.DataSource = dt;
            gvPunonjesit.DataBind();
            dt.Dispose();
        }

        /// <summary>
        /// ruan punonjesin
        /// </summary>
        private void ruajPunonjes(ResourceManager rm, CultureInfo ci)
        {
            if (Page.IsValid == false)
                return;
            int idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
            if (isValidPunonjes(idNdermarrje, rm, ci))
            {
                try
                {
                    var mesazhPunesimi = ruajPunesim(idNdermarrje);
                    if (!mesazhPunesimi)
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhPunesimi.PershkrimMesazhi, pnlMesazhi);

                    clsPunonjes punonjes = krijoPunonjes(idNdermarrje, rm, ci);

                    clsMesazh mesazh = new clsMesazh();
                    bool eshteShtim; clsTeDrejtaRoli tedrejtaInfo = new clsTeDrejtaRoli();

                    tedrejtaInfo.merrTeDrejtaPerKeteKomponente(mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje, mySessionObjects.ktheIdVitNdermarrje(Session), komponente);
                    if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim")
                    {
                        if (!tedrejtaInfo.DShtim)
                        {
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["mesazhRaportNukKeniTeDrejta"], pnlMesazhi);
                            hfStatusi.Value = "false";
                            return;
                        }
                        hfArkiva.Set("kopjoArkiven", hfShtimModifikim.Value == "klonim");
                        mesazh = punonjes.ruaj(hfNrAutoKF, ci, rm);
                        eshteShtim = true;
                        ConfigureAspxComboBox.mbushComboArsye(txtArsyeja);
                    }
                    else
                    {
                        if (!tedrejtaInfo.DMod)
                        {
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["mesazhRaportNukKeniTeDrejta"], pnlMesazhi);
                            hfStatusi.Value = "false";
                            return;
                        }
                        clsDatabaseRegjistrim dbRegjistrim = new clsDatabaseRegjistrim();
                        clsKonfigurimAmbjenti konf = new clsKonfigurimAmbjenti();
                        konf.mbushKonfigAmbjSipasId(punonjes.IdKonfig);
                        punonjes.IdPunonjes = int.Parse(hfId.Value);
                        bool lidhur = dbRegjistrim.eshteDokumentiILidhurCelje(punonjes.IdPunonjes.ToString(), konf.IdNivel.ToString());
                        if (lidhur.ToString() != hfLidhur.Value)
                        {
                            mesazh.Status = false;
                            mesazh.PershkrimMesazhi = MessagesResource.Messages["msgPunonjesiEshteILidhur"];
                        }
                        else
                            mesazh = punonjes.modifiko(ci, rm);
                        eshteShtim = false;
                        dbRegjistrim.Dispose();
                    }
                    if (mesazh.Status)
                    {
                        clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                        if (eshteShtim)
                            shtoNeGrid(idNdermarrje, punonjes.IdPunonjes, rm, ci);
                        else //modifikim
                            modifikoNeGrid(idNdermarrje, punonjes.IdPunonjes, rm, ci);
                        konfiguroGridePunonjesit(idNdermarrje, cmbKonfigurimi.Text.Split(';')[0], 708, rm, ci);
                        hfStatusi.Value = "true";
                        ASPxPageControl1.ActiveTabIndex = 0;
                        gvKomponenteListPagese.ClearSort();
                        gvKomponenteListPagese.CollapseAll();
                        gvKomponenteListPagese.FilterExpression = "";
                        gvPunonjesit.FilterExpression = "";
                    }
                    else
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                        hfStatusi.Value = "false";
                        hfStatusiPunesim.Value = "false";
                    }
                }
                catch (Exception ex)
                {
                    ImbLogger.Error(ex.Message);
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, ex.Message, pnlMesazhi);
                    hfStatusi.Value = "false";
                    hfStatusiPunesim.Value = "false";
                }
            }
            pnlMesazhi.Update();
        }

        private void fshiPunesiminEfundit(colPunesim col)
        {
            if (col.Count > 0)//ka punesime per te fshire
                col.RemoveRange(col.Count - 1, col.Count);
            gvPunesim.DataSource = col;
            gvPunesim.DataBind();
            mySessionObjects.ruajPunesimNeSesion(Session, col);
        }

        /// <summary>
        /// krijon punonjesin sipas te dhenave
        /// </summary>
        /// <returns> punonjesin me te dhenat</returns>
        /// <param name="idNdermarrje"></param>
        private clsPunonjes krijoPunonjes(int idNdermarrje, ResourceManager rm, CultureInfo ci)
        {//krijon nje banke sipas te dhenave te futura nga perdoruesi
            var controls = this.GetAsPxTextEditIdValue();
            controls.AddRange(ASPxPageControl1.GetAsPxTextEditIdValue());

            hfNrAuto = (ASPxHiddenField)NrAuto.VendosVleratNrAuto(hfNrAuto, controls);
            //NrAuto.vendosVleratNrAuto(hfNrAuto, this, ASPxPageControl1, null, null);

            hfNrAutoKF = (ASPxHiddenField)NrAuto.ShtoNeRegjistrime(hfNrAutoKF, hfNrAuto, "txtNrPersonal", "NrPersonal");
            //NrAuto.shtoNeHfRegjistrime(hfNrAutoKF, hfNrAuto, "txtNrPersonal", "NrPersonal");

            colPunesim col = new colPunesim();

            var dt = mySessionObjects.merrPunesimNgaSesioni(Session);
            col = new colPunesim(dt.ToList<clsPunesim>());

            int nrrendor = 0;
            int.TryParse(txtNrRendor.Text, out nrrendor);
            clsPunonjes punonjes = new clsPunonjes(txtNrPersonal.Text, txtEmri.Text, txtMbiemri.Text, txtAtesia.Text, dteDatelindja.Date, txtNrSig.Text, cmbQyteti.Text, txtAdresa.Text, txtTel.Text, txtEmail.Text, cbAktiv.Checked, txtEmriKontakti.Text, txtMbiemriKontakti.Text, txtTelKontakti.Text, txtAdresaKontakti.Text, txtEmailKontakti.Text, txtShenimeKontakti.Text, cmbGrupi.Text, Convert.ToInt32(cmbLlojPagese.Value), Convert.ToInt32(cmbMonedha.Value), idperdoruesi, idNdermarrje, cmbKonfigurimi.Text, idstatusdok, cmbObjektiva.Text, cbLlogaritNgaListorare.Checked, idperdoruesi, txtSap.Text, txtNrPashaporte.Text, cmbGjinia.Text, cmbKombesia.Text, cbKryefamiliar.Checked, cmbEdukimi.Text, cmbPunaMeparshme.Text, cmbVendndodhjet.Text, txtNrJupiter.Text, txtUsername.Text, txtShenime.Text, nrrendor, txtLejePune.Text, cmbIdLlogari.Text, "", rm, ci, idgjuha, (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim"), hfArkiva) { OColKomponente = ruajKomponente(col, ci), OColSkemat = ruajSkema(), OColPagaShtesa = ruajPagaShtesa(col, ci), OColPunesimet = col, QendraKostoPunonjes = krijoQendra(idNdermarrje), BandaPunonjes = krijoBanda(idNdermarrje), BankaPunonjes = krijoBanka(idNdermarrje) };

            return punonjes;
        }

        /// <summary>
        /// ruan skemen e sigurimit
        /// </summary>
        /// <returns></returns>
        private colSkemaSigurimi ruajSkema()
        {
            colSkemaSigurimi col = new colSkemaSigurimi();
            clsSkemaSigurimi cls = new clsSkemaSigurimi(0, 0, Convert.ToInt32(cmbSkemaSigurimi.Value), dteDateAk2.Date.Date, idperdoruesi);
            col.Add(cls);
            return col;
        }

        /// <summary>
        /// kontrollon nese te dhenat qe jane plotesuara jane te lejueshme apo jo
        /// </summary>
        /// <returns> true ose false</returns>
        /// <param name="idNdermarrje"></param>
        private bool isValidPunonjes(int idNdermarrje, ResourceManager rm, CultureInfo ci)
        {
            clsKonfigurimAmbjenti konfig = new clsKonfigurimAmbjenti();
            konfig.mbushKonfigAmbjSipasKod(cmbKonfigurimi.Text, idNdermarrje);

            if (dteDateAkt.Text == "")
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgPunonjesZgjidhniDateAktivizimi"], pnlMesazhi);
                hfStatusi.Value = "false";
                return false;
            }
            if (dteDateAk2.Text == "")
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgPunonjesZgjidhniDateAktivizimi"], pnlMesazhi);
                hfStatusi.Value = "false";
                return false;
            }

            if (txtLlogBankare.Text != "")
            {
                clsKusht kusht = new clsKusht(konfig.IdKonfigAmbjente, "GJLLB");
                if (kusht.Vlera != 0)
                {
                    if (txtLlogBankare.Text.Length > kusht.Vlera)
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgGjatesiaLlogBank"] + " " + kusht.Vlera + MessagesResource.Messages["msgKaraktereVogel"] + " !", pnlMesazhi);
                        hfStatusi.Value = "false";
                        return false;
                    }
                    double sap = 0;
                    if (!double.TryParse(txtLlogBankare.Text, out sap) && hfLejoNrLlogBank.Value == "False")
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgFushaLLogBank"], pnlMesazhi);
                        hfStatusi.Value = "false";
                        return false;
                    }
                }
            }
            if (cmbSkemaSigurimi.Text == "")
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgSkemaSigurimeve"], pnlMesazhi);
                hfStatusi.Value = "false";
                return false;
            }
            if (cbLarguar.Checked && (dteDtLargimi.Date == null || dteDtLargimi.Date < dteDtFillimi.Date))
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgVendosDateLargimiMeTeMadheSeDtFillimi"], pnlMesazhi);
                hfStatusi.Value = "false";
                return false;
            }
            if (cmbDepartamenti.Text != string.Empty && cmbDepartamenti.Value!=null)
            {
                clsStrukturaAdministrative dep = new clsStrukturaAdministrative(int.Parse(cmbDepartamenti.Value.ToString()));
                
                if (dep.Aktive == false )
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, string.Format(MessagesResource.Messages["msgdepartamentijoaktiv"],dep.Kodi), pnlMesazhi);
                    hfStatusi.Value = "false";
                    return false;

                }
                else if (  cmbNenDepartamenti.Text != string.Empty && cmbNenDepartamenti.Value!=null)
                {
                    clsStrukturaAdministrative nendep = new clsStrukturaAdministrative(int.Parse(cmbNenDepartamenti.Value.ToString()));
                  
                    if (nendep.Aktive == false)
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, string.Format(MessagesResource.Messages["msgdepartamentijoaktiv"], nendep.Kodi), pnlMesazhi);
                        hfStatusi.Value = "false";
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// cakton properti ne javascript
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvPunonjesit_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gvPunonjesit.PageIndex;
            e.Properties["cpPageRow"] = gvPunonjesit.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gvPunonjesit.VisibleRowCount;
        }

        /// <summary>
        /// kur grida ben callback
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvPunonjesit_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string idkomponente = "";
            string kodkonfigurimi = "";

            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                kodkonfigurimi = arr[1];
                idkomponente = arr[0];
                if (arr[2] == "")
                    gvPunonjesit.FilterExpression = "";
                else
                {
                    clsFiltraGrida filtra = new clsFiltraGrida();
                    //filtra.mbushFiltraGridaSipasFiltraKodi(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    clsGridaKoka koka = new clsGridaKoka(mySessionObjects.ktheGjuhe(Session), "gvPunonjesit", komponente, mySessionObjects.merrIdNdermarrjeSesioni(Session), int.Parse(cmbKonfigurimi.Value.ToString()));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        gvPunonjesit.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvPunonjesit);
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

            var itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            if (cmbFiltra.Text == "")
            {
                gvPunonjesit.FilterExpression = "";
            }
            gvPunonjesit.Selection.UnselectAll();
        }

        /// <summary>
        /// per filtrimin me elementin bosh
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvPunonjesit_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if (e.Column.FieldName == "IdDepartament" ||
                             e.Column.FieldName == "IdNenDepartament" || e.Column.FieldName == "IdGrupPunonjesish" || e.Column.FieldName == "Edukimi" || e.Column.FieldName == "PunaMeparshme")
            {
                if (Converter.ConvertToInt(e.Value) == 0)
                {
                    e.Criteria = null;
                }
            }
            if (e.Column.FieldName == "Kombesia")
            {
                if (Converter.ConvertToInt(e.Value) == 1)
                {
                    e.Criteria = null;
                }
            }
        }

        /// <summary>
        /// perdoret per te shfaqur aktiv inaktiv
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvPunonjesit_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName == "Aktiv" || e.Column.FieldName == "LlogaritNgaListorare")
            {
                CultureInfo ci = mySessionObjects.ktheCultureInfo(Session);

                (e.Editor as ASPxComboBox).Items.Clear();
                (e.Editor as ASPxComboBox).Items.Add("");
                (e.Editor as ASPxComboBox).Items.Add(MessagesResource.Messages["cmbPerdoruesitAktiv"], true);
                (e.Editor as ASPxComboBox).Items.Add(MessagesResource.Messages["cmbPerdoruesitJoAktiv"], false);
            }
            else if (e.Column.FieldName == "Larguar")
            {
                CultureInfo ci = mySessionObjects.ktheCultureInfo(Session);

                (e.Editor as ASPxComboBox).Items.Clear();
                (e.Editor as ASPxComboBox).Items.Add("");
                (e.Editor as ASPxComboBox).Items.Add(MessagesResource.Messages["cmbFilterPo"], true);
                (e.Editor as ASPxComboBox).Items.Add(MessagesResource.Messages["cmbFilterJo"], false);
            }
            else if (e.Column.FieldName == "IdNenDepartament")
            {
                //ASPxComboBox cmb = e.Editor as ASPxComboBox;
                //cmb.ClientInstanceName = "cmbf2";
                //cmb.Callback += new DevExpress.Web.CallbackEventHandlerBase(cmb_Callback);

                // cmb.DataSourceID = "cityads";
            }
            //else if (e.Column.FieldName == "IdDepartament")
            //{
            //    ASPxComboBox cmb = e.Editor as ASPxComboBox;
            //    cmb.ClientInstanceName = "cmbf1";
            //    cmb.ClientSideEvents.SelectedIndexChanged = "function (s, e) { cmbf2.PerformCallback(s.GetValue()); }";
            //    cmb.ClientSideEvents.Init = "function (s, e) { cmbf2.PerformCallback(s.GetValue()); }";
            //}
        }

        protected void gvPunonjesit_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName == "IdNenDepartament")
            {
                ASPxComboBox cmb = e.Editor as ASPxComboBox;
                cmb.ClientInstanceName = "cmbIdNenDepartament";
                cmb.Callback += cmb_Callback;
            }
            else if (e.Column.FieldName == "IdDepartament")
            {
                ASPxComboBox cmb = e.Editor as ASPxComboBox;
                cmb.ClientInstanceName = "cmbIdDepartament";
                cmb.ClientSideEvents.SelectedIndexChanged = "function (s, e) { cmbIdNenDepartament.PerformCallback(s.GetValue()); }";
                //cmb.ClientSideEvents.Init = "function (s, e) { cmbIdNenDepartament.PerformCallback(s.GetValue()); }";
            }
        }

        private void cmb_Callback(object sender, CallbackEventArgsBase e)
        {
            ASPxComboBox cmb = sender as ASPxComboBox;
            colStrukturatAdministrative col = null;
            if (String.IsNullOrEmpty(e.Parameter))
                col = new colStrukturatAdministrative(idNdermarrje);
            else
            {
                col.mbushStrukturaAdmSipasPrindit(int.Parse(e.Parameter),false);
                col.Insert(0, new clsStrukturaAdministrative(0, "", "", "", "", "", 0, 0, 0, 0, 0, 0, 0,false));

                cmb.TextField = "Emri";
                cmb.ValueField = "IdStrukturaAdm";
            }
            cmb.DataSource = col;
            cmb.DataBind();
        }

        #region Punesim

        /// <summary>
        /// mbush punesimet
        /// </summary>
        private void mbushPunesim(int id)
        {
            colPunesim col;
            if (hfShtimModifikim.Value == "klonim")
                col = colPunesim.KthePunesimSipasIdPuneonjesiDt(0, idgjuha);
            else col = colPunesim.KthePunesimSipasIdPuneonjesiDt(id, idgjuha);
            //Session.Add("Punesim", col);
            mySessionObjects.ruajPunesimNeSesion(Session, col);
            gvPunesim.DataSource = col;
            gvPunesim.DataBind();
        }

        private void mbushPunesimSession()
        {
            colPunesim col = mySessionObjects.merrPunesimNgaSesioni(Session);
            gvPunesim.DataSource = col;
            gvPunesim.DataBind();
        }

        //private void shtoStatus()
        //{
        //    gvPunesim.Columns.Remove(gvPunesim.Columns["Statusi"]);
        //    GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
        //    colnew.PropertiesComboBox.Items.Add("", 0);
        //    colnew.PropertiesComboBox.Items.Add(MessagesResource.Messages["Shef_departamenti"], 1);
        //    colnew.PropertiesComboBox.Items.Add(MessagesResource.Messages["Punonjes"], 2);
        //    colnew.FieldName = "Statusi";
        //    colnew.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        //    gvPunesim.Columns.Add(colnew);
        //}

        //private void shtoNdryshimPozicioni()
        //{
        //    gvPunesim.Columns.Remove(gvPunesim.Columns["NdryshimPozicioni"]);
        //    GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
        //    DataTable dt = clsPunonjes.merrNdryshimPozicioniDT(idgjuha);

        //    colnew.PropertiesComboBox.DataSource = dt;
        //    colnew.PropertiesComboBox.TextField = "Pershkrimi";
        //    colnew.PropertiesComboBox.ValueField = "Id";

        //    colnew.FieldName = "NdryshimPozicioni";
        //    colnew.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        //    gvPunesim.Columns.Add(colnew);
        //}

        /// <summary>
        /// perdoret per te konfiguruar griden. Per te dhenat mbi kolonat qe do te shfaqen nga databaza si dhe percakton disa karakteristika te grides
        /// </summary>
        /// :  <see cref="GridUtil.percaktoVisibleColumns"/>
        /// :  <see cref="DbCore.clsFunksione.konfiguroGrideListeMadhe(grida,celesigrides)"/>
        /// <param name="idNdermarrje"></param>
        private void konfiguroGridePunesim(int idNdermarrje)
        {

            KonfigurimComboGride.shtoStatusPunesimi(gvPunesim, rm, cultinf);
            KonfigurimComboGride.shtoNdryshimPozicioni(gvPunesim, idNdermarrje, idgjuha, Session, komponente, guidString);
            //"275543" = mySessionObjects.ktheCultureInfo(Session);
            GridUtil.konfiguroGrideRegjistrimEvogelPaTheme(gvPunesim, "IdPunesim", false);
            gvPunesim.SettingsBehavior.AllowSort = true;
            gvPunesim.Columns["#"].VisibleIndex = 0;
            gvPunesim.SortBy(gvPunesim.Columns["DtAktivizimi"], ColumnSortOrder.Descending);
        }

        /// <summary>
        /// kur grida ben callback
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvPunesim_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
        }

        /// <summary>
        /// kur perdoruesi i ben grides callback
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvPunesim_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters == "Ri")
                mbushPunesim(int.Parse(hfId.Value));
            else if(e.Parameters == "FshiPunesim")
            {
                fshiPunesim();
            }
            else
            {
                //DbCore.DbListPagesat.colPunesim col = (DbCore.DbListPagesat.colPunesim)CacheLayer.GlobalCacheManager.MySessionCache["Punesim"];
                colPunesim col = mySessionObjects.merrPunesimNgaSesioni(Session);
                gvPunesim.DataSource = col;
                gvPunesim.DataBind();
            }
        }

        /// <summary>
        /// cakton properti ne javascript
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvPunesim_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gvPunesim.PageIndex;
            e.Properties["cpPageRow"] = gvPunesim.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gvPunesim.VisibleRowCount;
        }

        /// <summary>
        /// kur grida ben databound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvPunesim_DataBound(object sender, EventArgs e)
        {
            if (gvPunesim.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per
                //perzgjidh
                GridViewCommandColumn check = new GridViewCommandColumn("#") { ShowSelectCheckbox = true, Width = Unit.Percentage(2) };
                //behet per te afishuar rreshtin qe do sherbej per filtrim
                gvPunesim.Settings.ShowFilterRow = true;
                gvPunesim.Settings.ShowFilterRowMenu = true;
                gvPunesim.Columns.Add(check);
                gvPunesim.KeyFieldName = "IdPunesim";
                gvPunesim.SettingsBehavior.AllowSelectByRowClick = true;
                gvPunesim.SettingsBehavior.AllowFocusedRow = true;
            }
        }

        /// <summary>
        /// ruan punesimin ne gride por jo ne databaze
        /// </summary>
        private clsMesazh ruajPunesim(int idNdermarrje)
        {
            colPunesim col = mySessionObjects.merrPunesimNgaSesioni(Session);
            var isValid = isValidPunesim(idNdermarrje, col);
            if (isValid)
            {
                clsPunesim punesim = krijoPunesim(idNdermarrje, idgjuha);

                //zgjidhje nga halli derisa te ndryshohet menyra TODO GETSON

                //nese eshte shtim
                if (hfShtimModifikimPunesim.Value == "shtim")
                {
                    col.RemoveAll(x => x.DtAktivizimi.ToShortDateString() == punesim.DtAktivizimi.ToShortDateString());
                    int id = mySessionObjects.merrIdShtimiNgaSesioni(Session);
                    punesim.IdPunesim = id;

                    var controls = this.GetAsPxTextEditIdValue();
                    controls.AddRange(ASPxPageControl1.GetAsPxTextEditIdValue());

                    hfNrAuto = (ASPxHiddenField)NrAuto.VendosVleratNrAuto(hfNrAuto, controls);
                    //NrAuto.vendosVleratNrAuto(hfNrAuto, this, ASPxPageControl1, null, null);

                    hfNrAutoKF = (ASPxHiddenField)NrAuto.ShtoNeRegjistrime(hfNrAutoKF, hfNrAuto, "txtNrKontrate", "NrKontrate");
                    //NrAuto.shtoNeHfRegjistrime(hfNrAutoKF, hfNrAuto, "txtNrKontrate", "NrKontrate");

                    mySessionObjects.ruajIdShtimiNeSesion(Session, id - 1);
                    col.Add(punesim);
                    mySessionObjects.ruajPunesimNeSesion(Session, new colPunesim(col.OrderByDescending(x => x.DtAktivizimi)));

                    hfStatusiPunesim.Value = "true";
                    isValid = new MesazhSuksesi(MessagesResource.Messages["msgPunonjesPunesimiUShtuaMeSukses"]);

                }
                else
                {
                    //modifikim
                    if (col.Count > 0)
                    {
                        punesim.IdPunesim = int.Parse(hfIdPunesim.Value == "" ? "0" : hfIdPunesim.Value);
                        var pun = col.FirstOrDefault(x => x.IdPunesim == punesim.IdPunesim);
                        
                        if (pun.DtAktivizimi.ToShortDateString() == punesim.DtAktivizimi.ToShortDateString())
                        {
                            col.RemoveAll(x => x.IdPunesim == pun.IdPunesim);
                        }
                        else {
                            var punDate = col.FirstOrDefault(x => x.DtAktivizimi == punesim.DtAktivizimi);
                            if (punDate != null && punDate.IdPunesim != 0)
                            {
                                hfIdPunesim.Value = punDate.IdPunesim.ToString();
                                punesim.IdPunesim = punDate.IdPunesim;
                                col.RemoveAll(x => x.IdPunesim == punDate.IdPunesim);
                            } 
                            else
                            {
                                int id = mySessionObjects.merrIdShtimiNgaSesioni(Session);
                                punesim.IdPunesim = id;
                                mySessionObjects.ruajIdShtimiNeSesion(Session, id - 1);
                            }
                        }
                        col.Add(punesim);
                        mySessionObjects.ruajPunesimNeSesion(Session, new colPunesim(col.OrderByDescending(x => x.DtAktivizimi)));
                        hfStatusiPunesim.Value = "true";
                        isValid = new MesazhSuksesi(MessagesResource.Messages["msgPunonjesPunesimiUModifikuaMeSukses"]);
                    }
                }
            }
            gvPunesim.DataSource = col;
            gvPunesim.DataBind();
            return isValid;
        }

        /// <summary>
        /// fshin punesimin ne gride por jo ne databaze
        /// </summary>
        private void fshiPunesim()
        {
            colPunesim col = mySessionObjects.merrPunesimNgaSesioni(Session);
            List<object> rreshtat = gvPunesim.GetSelectedFieldValues("IdPunesim");
            foreach (var rresht in rreshtat)
            {
                col.RemoveAll(x => x.IdPunesim == Convert.ToInt32(rresht));
            }
            mySessionObjects.ruajPunesimNeSesion(Session, col);
            gvPunesim.DataSource = col;
            gvPunesim.DataBind();
            gvPunesim.ShtoMesazhNeGride(new MesazhSuksesi(MessagesResource.Messages["msgFshirjaPerfundoiMeSukses"]));
        }

        /// <summary>
        /// krijon punesimin
        /// </summary>
        /// <returns></returns>
        /// <param name="idNdermarrje"></param>
        private clsPunesim krijoPunesim(int idNdermarrje, int idgjuha)
        {
           
            var punesim = new clsPunesim("", cmbDepartamenti.Text, cmbNenDepartamenti.Text, txtDetyra.Text, txtNrKontrate.Text, cmbTipKontrate.Text, dteDtFillimi.Date, dteDtPerfundimi.Date, cbLarguar.Checked, dteDtLargimi.Date, txtArsyeja.Text, txtPeriudhaNjoftimi.Text, cbNeProve.Checked, txtPeriudhaProve.Text, cmbGrupi.Text, cmbProfesioni.Text, cmbTitull.Text, cbShifte.Checked, cbStandBy.Checked, cmbStatusi.Text, cmbNryshimPozicioni.Text, dteDtNenshkrimi.Date, txtShenimePun.Text, dteDateAktPun.Date, idperdoruesi, cbKomisione.Checked, cmbKodeProfesione.Text, idNdermarrje, idgjuha, Convert.ToInt32(cmbProfesioni.Value), Convert.ToInt32(cmbTitull.Value), Convert.ToInt32(cmbDepartamenti.Value), Convert.ToInt32(cmbNenDepartamenti.Value));

            return punesim;
        }

        protected void gvPunesim_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if (e.Column.FieldName == "NdryshimPozicioni" ||
                            e.Column.FieldName == "Statusi")
            {
                if (Converter.ConvertToInt(e.Value) == 0)
                {
                    e.Criteria = null;
                }
            }
        }

        protected void gridExport_RenderBrick(object sender, ASPxGridViewExportRenderingEventArgs e)
        {
            CultureInfo ci = mySessionObjects.ktheCultureInfo(Session);
            if (e.Column.Caption == "Larguar" || e.Column.Caption == "Me Komisione" || e.Column.Caption == "Ne Prove" || e.Column.Caption == "Me Turne" || e.Column.Caption == "Ne Gatishmeri" || e.Column.Caption == "Left" || e.Column.Caption == "With commisions" || e.Column.Caption == "Test" || e.Column.Caption == "Shifts" || e.Column.Caption == "Stand by")
                if (e.Text == "Checked")
                    e.Text = MessagesResource.Messages["btnPO"];
                else if (e.Text == "Unchecked") e.Text = MessagesResource.Messages["btnJO"];
        }

        /// <summary>
        /// kontrollon nese punesimi eshte i vlefshem
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        private clsMesazh isValidPunesim(int idNdermarrje, colPunesim col)
        {
            if (cmbBanka.Text != "")
            {
                if (!clsBanka.ekziston(cmbBanka.Text, idNdermarrje).Status)
                {
                    hfStatusiPunesim.Value = "false";
                    return new MesazhGabimi(MessagesResource.Messages["msgKjoBankeNukEkziston"]);
                }

                clsBanka banka = new clsBanka();
                banka.mbushBankeSipasKoditMeAutorizime(cmbBanka.Text, idNdermarrje, idperdoruesi);
                if (banka.IdBanka < 1)
                    return new MesazhGabimi(MessagesResource.Messages["msgNukKeniAutorizimAB"]);
                if (!banka.AktivBanka)
                    return new MesazhGabimi(MessagesResource.Messages["msgArkaBankaNukEshteAktive"]);

            }
            if (cbLarguar.Checked && (dteDtLargimi.Date == null || dteDtLargimi.Date < dteDtFillimi.Date))
                return new MesazhGabimi(MessagesResource.Messages["msgVendosDateLargimiMeTeMadheSeDtFillimi"]);
            return new MesazhSuksesi();
        }

        #endregion Punesim

        #region Qendra Kosto

        /// <summary>
        /// mbush punesimet
        /// </summary>
        private void mbushQendra(int id)
        {
            DataTable col;
            if (hfShtimModifikim.Value == "klonim")
                col = colQendraKostoPunonjes.ktheQendraPunonjesi(0);
            else
                col = colQendraKostoPunonjes.ktheQendraPunonjesi(id);
            //Session.Add("Punesim", col);
            mySessionObjects.ruajObjectNeSesion(Session, col, qendraKostoSessionKey);
            gvQendra.DataSource = col;
            gvQendra.DataBind();
        }

        private void mbushQendraSession()
        {
            DataTable col = (DataTable)mySessionObjects.merrObjectNgaSesioni(Session, qendraKostoSessionKey);
            gvQendra.DataSource = col;
            gvQendra.DataBind();
        }

    

        /// <summary>
        /// perdoret per te konfiguruar griden. Per te dhenat mbi kolonat qe do te shfaqen nga databaza si dhe percakton disa karakteristika te grides
        /// </summary>
        /// :  <see cref="GridUtil.percaktoVisibleColumns"/>
        /// :  <see cref="DbCore.clsFunksione.konfiguroGrideListeMadhe(grida,celesigrides)"/>
        /// <param name="idNdermarrje"></param>
        private void konfiguroGrideQendra(int idNdermarrje)
        {
            KonfigurimComboGride.shtoQK(gvQendra, idNdermarrje, Session, komponente, guidString, "IdQenderKosto1");
            KonfigurimComboGride.shtoQK(gvQendra, idNdermarrje, Session, komponente, guidString, "IdQenderKosto2");
           
            //"275543" = mySessionObjects.ktheCultureInfo(Session);
            GridUtil.percaktoVisibleColumns(mySessionObjects.ktheGjuhe(Session), idNdermarrje, gvQendra, "gvQendra", komponente);
            GridUtil.konfiguroGrideRegjistrimEvogelPaTheme(gvQendra, "Id", false);

            gvQendra.Columns["#"].VisibleIndex = 0;
        }

        /// <summary>
        /// kur grida ben callback
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvQendra_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            ////DbCore.DbListPagesat.colPunesim col = (DbCore.DbListPagesat.colPunesim)CacheLayer.GlobalCacheManager.MySessionCache["Punesim"];
            //DataTable col = (DataTable)mySessionObjects.merrObjectNgaSesioni(Session);
            //gvQendra.DataSource = col;
            //gvQendra.DataBind();
            //gvQendra.JSProperties["cpCallbackCompleted"] = true;
        }

        /// <summary>
        /// kur perdoruesi i ben grides callback
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvQendra_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters == "Ri")
                mbushQendra(int.Parse(hfId.Value));
            else
            {
                ////DbCore.DbListPagesat.colPunesim col = (DbCore.DbListPagesat.colPunesim)CacheLayer.GlobalCacheManager.MySessionCache["Punesim"];
                //DataTable col = (DataTable)mySessionObjects.merrObjectNgaSesioni(Session, qendraKostoSessionKey);
                //gvQendra.DataSource = col;
                //gvQendra.DataBind();
            }
        }

        /// <summary>
        /// cakton properti ne javascript
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvQendra_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gvQendra.PageIndex;
            e.Properties["cpPageRow"] = gvQendra.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gvQendra.VisibleRowCount;
        }

        /// <summary>
        /// kur grida ben databound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvQendra_DataBound(object sender, EventArgs e)
        {
            if (gvQendra.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per
                //perzgjidh
                GridViewCommandColumn check = new GridViewCommandColumn("#") { ShowSelectCheckbox = true, Width = Unit.Percentage(2) };
                //behet per te afishuar rreshtin qe do sherbej per filtrim
                gvQendra.Settings.ShowFilterRow = true;
                gvQendra.Settings.ShowFilterRowMenu = true;
                gvQendra.Columns.Add(check);
                gvQendra.KeyFieldName = "Id";
                gvQendra.SettingsBehavior.AllowSelectByRowClick = true;
                gvQendra.SettingsBehavior.AllowFocusedRow = true;
            }
        }

        protected void gvQendra_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if (e.Column.FieldName == "IdQenderKosto1" ||
                            e.Column.FieldName == "IdQenderKosto2" )
            {
                if (Converter.ConvertToInt(e.Value) == 0)
                {
                    e.Criteria = null;
                }
            }
        }

        /// <summary>
        /// krijon punesimin
        /// </summary>
        /// <returns></returns>
        /// <param name="idNdermarrje"></param>
        private clsQendraKostoPunonjes krijoQendra(int idNdermarrje)
        {
            clsQendraKostoPunonjes qk = new clsQendraKostoPunonjes("", cmbQK1.Text, cmbQK2.Text,  dteDateAktQK.Date.Date, idperdoruesi, idNdermarrje);

            return qk;
        }

        #endregion Qendra Kosto
   
        #region Banda

        /// <summary>
        /// mbush punesimet
        /// </summary>
        private void mbushBanda(int id)
        {
            DataTable col;
            if (hfShtimModifikim.Value == "klonim")
                col = colBandaPunonjes.ktheBandaPunonjesi(0);
            else
                col = colBandaPunonjes.ktheBandaPunonjesi(id);
            //Session.Add("Punesim", col);
            mySessionObjects.ruajObjectNeSesion(Session, col, bandSessionKey);
            gvBanda.DataSource = col;
            gvBanda.DataBind();
        }

        private void mbushBandaSession()
        {
            DataTable col = (DataTable)mySessionObjects.merrObjectNgaSesioni(Session, bandSessionKey);
            gvBanda.DataSource = col;
            gvBanda.DataBind();
        }

        

        /// <summary>
        /// perdoret per te konfiguruar griden. Per te dhenat mbi kolonat qe do te shfaqen nga databaza si dhe percakton disa karakteristika te grides
        /// </summary>
        /// :  <see cref="GridUtil.percaktoVisibleColumns"/>
        /// :  <see cref="DbCore.clsFunksione.konfiguroGrideListeMadhe(grida,celesigrides)"/>
        /// <param name="idNdermarrje"></param>
        private void konfiguroGrideBanda(int idNdermarrje)
        {
          
            KonfigurimComboGride.shtoGlobalLocal(gvBanda, idNdermarrje, Session, komponente, guidString, "IdGrupimGlobal");
            KonfigurimComboGride.shtoGlobalLocal(gvBanda, idNdermarrje, Session, komponente, guidString, "IdGrupimLokal");
            //"275543" = mySessionObjects.ktheCultureInfo(Session);
            GridUtil.percaktoVisibleColumns(mySessionObjects.ktheGjuhe(Session), idNdermarrje, gvBanda, "gvBanda", komponente);
            GridUtil.konfiguroGrideRegjistrimEvogelPaTheme(gvBanda, "Id", false);

            gvBanda.Columns["#"].VisibleIndex = 0;
        }

        /// <summary>
        /// kur grida ben callback
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvBanda_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            
        }

        /// <summary>
        /// kur perdoruesi i ben grides callback
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvBanda_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters == "Ri")
                mbushBanda(int.Parse(hfId.Value));
           
        }

        /// <summary>
        /// cakton properti ne javascript
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvBanda_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gvBanda.PageIndex;
            e.Properties["cpPageRow"] = gvBanda.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gvBanda.VisibleRowCount;
        }

        /// <summary>
        /// kur grida ben databound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvBanda_DataBound(object sender, EventArgs e)
        {
            if (gvBanda.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per
                //perzgjidh
                GridViewCommandColumn check = new GridViewCommandColumn("#") { ShowSelectCheckbox = true, Width = Unit.Percentage(2) };
                //behet per te afishuar rreshtin qe do sherbej per filtrim
                gvBanda.Settings.ShowFilterRow = true;
                gvBanda.Settings.ShowFilterRowMenu = true;
                gvBanda.Columns.Add(check);
                gvBanda.KeyFieldName = "Id";
                gvBanda.SettingsBehavior.AllowSelectByRowClick = true;
                gvBanda.SettingsBehavior.AllowFocusedRow = true;
            }
        }

        protected void gvBanda_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if ( e.Column.FieldName == "IdGrupimGlobal" || e.Column.FieldName == "IdGrupimLokal")
            {
                if (Converter.ConvertToInt(e.Value) == 0)
                {
                    e.Criteria = null;
                }
            }
        }

        /// <summary>
        /// krijon punesimin
        /// </summary>
        /// <returns></returns>
        /// <param name="idNdermarrje"></param>
        private clsBandaPunonjes krijoBanda(int idNdermarrje)
        {
            clsBandaPunonjes qk = new clsBandaPunonjes("", cmbGlobal.Text, cmbLocal.Text, dteDateAktBanda.Date.Date, idperdoruesi, idNdermarrje);

            return qk;
        }

        #endregion Qendra Kosto
        #region PagaShtesa

        /// <summary>
        /// mbush paga dhe shtesa
        /// </summary>
        /// <param name="idNdermarrje"></param>
        private void mbushPagaShtesa(int idNdermarrje)
        {
            colPagaShtesa col = new colPagaShtesa();
            if (cmbNdryshimi.Text == "")
                col = new colPagaShtesa(false, dteDateAkt.Date, idNdermarrje);
            else col = new colPagaShtesa(false, dteDateAkt.Date, idNdermarrje, Convert.ToDateTime(cmbNdryshimi.Value), int.Parse(hfId.Value));
            mySessionObjects.ruajPageShteseNeSesion(Session, col);
            gvPagaShtesa.DataSource = col;
            gvPagaShtesa.DataBind();
        }

        /// <summary>
        /// percakton kolonat template
        /// </summary>
        /// <param name="idNdermarrje"></param>
        private void percaktoTemplatePagaShtesa(int idNdermarrje)
        {
            int formatvlefta;
            try
            {
                formatvlefta = mySessionObjects.merrFormatVleftaSesioni(Session);
            }
            catch (MyException ex)
            {
                ImbLogger.Error(ex.Message);
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, ex.Message, pnlMesazhi);
                return;
            }
            clsNdermarrje nderm = new clsNdermarrje(idNdermarrje);
            GridViewDataTextColumn col1 = gvPagaShtesa.Columns["VleraParam"] as GridViewDataTextColumn;
            if (nderm.Lloji == 2) col1.DataItemTemplate = new MyComboTemplate();
            else col1.DataItemTemplate = new MyDoubleTemplate(false, formatvlefta, "0"); //  "0.00");
            GridViewDataTextColumn col2 = gvPagaShtesa.Columns["Vlera"] as GridViewDataTextColumn;
            col2.DataItemTemplate = new MyDoubleTemplate(false, formatvlefta, "0"); // "0.00");

            GridViewDataTextColumn col3 = gvPagaShtesa.Columns["Komponente"] as GridViewDataTextColumn;
            col3.DataItemTemplate = new MyLabelTemplate();
        }
        /// <summary>
        /// perdoret per te konfiguruar griden. Per te dhenat mbi kolonat qe do te shfaqen nga databaza si dhe percakton disa karakteristika te grides
        /// </summary>
        /// :  <see cref="GridUtil.percaktoVisibleColumns"/>
        /// :  <see cref="DbCore.clsFunksione.konfiguroGrideListeMadhe(grida,celesigrides)"/>
        /// <param name="idNdermarrje"></param>
        private void konfiguroGridePagaShtesa(int idNdermarrje)
        {
            KonfigurimComboGride.shtoNjesiKomponenteLP(gvPagaShtesa, rm, cultinf);
            percaktoTemplatePagaShtesa(idNdermarrje);
            //"275543" = mySessionObjects.ktheCultureInfo(Session);
            GridUtil.percaktoVisibleColumnsMeWidth(mySessionObjects.ktheGjuhe(Session), idNdermarrje, gvPagaShtesa, "gvPagaShtesa", komponente);
            GridUtil.konfiguroGrideRegjistrimEvogelPaTheme(gvPagaShtesa, "IdPagaShtesa", false);
            gvPagaShtesa.SettingsBehavior.AllowSort = false;
        }

        /// <summary>
        /// kur grida ben callback
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvPagaShtesa_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            gvPagaShtesa.JSProperties["cpCallbackCompleted"] = true;
        }

        /// <summary>
        /// kur perdoruesi i ben callback grides
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvPagaShtesa_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            mbushPagaShtesa(mySessionObjects.merrIdNdermarrjeSesioni(Session));
        }

        /// <summary>
        /// cakton properti ne javascript
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvPagaShtesa_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gvPagaShtesa.PageIndex;
            e.Properties["cpPageRow"] = gvPagaShtesa.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gvPagaShtesa.VisibleRowCount;
        }

        /// <summary>
        /// kur krijohen rreshtat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvPagaShtesa_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            //DbCore.DbListPagesat.colPagaShtesa colpaga = (DbCore.DbListPagesat.colPagaShtesa)CacheLayer.GlobalCacheManager.MySessionCache["PagaShtesa"];
            colPagaShtesa colpaga = mySessionObjects.merrPageShteseNgaSesioni(Session);
            if (e.RowType == GridViewRowType.Data)
            {
                GridViewDataTextColumn col = ((ASPxGridView)sender).Columns["VleraParam"] as GridViewDataTextColumn;
                GridViewDataTextColumn col2 = ((ASPxGridView)sender).Columns["Vlera"] as GridViewDataTextColumn;
                GridViewDataTextColumn col3 = ((ASPxGridView)sender).Columns["Komponente"] as GridViewDataTextColumn;
                ASPxTextBox txtVlera = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col2, "txtBox") as ASPxTextBox;
                ASPxLabel lblKomponente = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col3, "lbl") as ASPxLabel;
                int idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
                CultureInfo ci = mySessionObjects.ktheCultureInfo(Session);
                if (txtVlera != null)
                {
                    if (colpaga.Count > e.VisibleIndex && colpaga[e.VisibleIndex].Njesia != 0)
                        txtVlera.ClientEnabled = false;
                    if (colpaga.Count > e.VisibleIndex && colpaga[e.VisibleIndex].Komponente == MessagesResource.Messages["msgPunonjesiPagaMeShtesat"])
                    {
                        txtVlera.BackColor = Color.DeepSkyBlue;
                        txtVlera.ForeColor = Color.Blue;
                        txtVlera.Font.Bold = true;
                    }
                    txtVlera.ClientInstanceName = "txtVlera" + e.VisibleIndex;
                    txtVlera.ClientSideEvents.TextChanged = String.Format("function (s,e){{ TextChangedVlera(s,e,'txtVlera',{0})}}", e.VisibleIndex);
                    if ((bool)hftabe.Get("7") == false) txtVlera.ClientEnabled = false;
                }
                if (colpaga.Count > e.VisibleIndex && colpaga[e.VisibleIndex].Njesia == 1 && colpaga[e.VisibleIndex].Emerparam != "")
                {
                    ASPxComboBox cmb = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col, "cmbBox") as ASPxComboBox;
                    if (colpaga.Count > e.VisibleIndex && cmb != null)
                    {
                        cmb.TextFormatString = "{0}";
                        ListBoxColumn colkodi = new ListBoxColumn(), colprove = new ListBoxColumn(), colemer = new ListBoxColumn();
                        switch (colpaga[e.VisibleIndex].Komponente)
                        {
                            case "Paga baze/njesi":
                                colKategoriPage colkat = new colKategoriPage(idNdermarrje, Convert.ToInt32(LlojPagese.Mujore));
                                colkat.Insert(0, new clsKategoriPage());
                                cmb.DataSource = colkat;
                                cmb.TextField = "Kodi";
                                cmb.ValueField = "IdKategoriPage";
                                cmb.DropDownStyle = DropDownStyle.DropDownList;
                                colkodi = new ListBoxColumn();
                                colkodi.FieldName = "Kodi";
                                colprove = new ListBoxColumn();
                                colprove.FieldName = "Pershkrimi";
                                colemer = new ListBoxColumn();
                                colemer.FieldName = "Paga";
                                cmb.ClientSideEvents.TextChanged = String.Format("function (s,e){{ TextChangedVleraCombo(s,e,'txtVleraParam',{0})}}", e.VisibleIndex);
                                break;

                            case "Shtesa Funksioni Perqindje":
                                colShtesaPage colfun = new colShtesaPage(idNdermarrje, Convert.ToInt32(TipeShtesaPage.FunksioniPerqindje));
                                colfun.Insert(0, new clsShtesaPage());
                                cmb.DataSource = colfun;
                                cmb.TextField = "Kodi";
                                cmb.ValueField = "IdShtesaPage";
                                cmb.DropDownStyle = DropDownStyle.DropDownList;
                                colkodi = new ListBoxColumn();
                                colkodi.FieldName = "Kodi";
                                colprove = new ListBoxColumn();
                                colprove.FieldName = "Klasa";
                                colemer = new ListBoxColumn();
                                colemer.FieldName = "Vlera";
                                cmb.ClientSideEvents.TextChanged = String.Format("function (s,e){{ TextChangedVleraShtesa(s,e,'txtVleraParam',{0})}}", e.VisibleIndex);
                                break;

                            case "Shtese Funksioni ne Vlere":
                                colfun = new colShtesaPage(idNdermarrje, Convert.ToInt32(TipeShtesaPage.FunksioniVlere));
                                colfun.Insert(0, new clsShtesaPage());
                                cmb.DataSource = colfun;
                                cmb.TextField = "Kodi";
                                cmb.ValueField = "IdShtesaPage";
                                cmb.DropDownStyle = DropDownStyle.DropDownList;
                                colkodi = new ListBoxColumn();
                                colkodi.FieldName = "Kodi";
                                colprove = new ListBoxColumn();
                                colprove.FieldName = "Klasa";
                                colemer = new ListBoxColumn();
                                colemer.FieldName = "Vlera";
                                cmb.ClientSideEvents.TextChanged = String.Format("function (s,e){{ TextChangedVleraShtesa(s,e,'txtVleraParam',{0})}}", e.VisibleIndex);
                                break;

                            case "Shtese Pozicioni":
                                colfun = new colShtesaPage(idNdermarrje, Convert.ToInt32(TipeShtesaPage.Pozicioni));
                                colfun.Insert(0, new clsShtesaPage());
                                cmb.DataSource = colfun;
                                cmb.TextField = "Kodi";
                                cmb.ValueField = "IdShtesaPage";
                                cmb.DropDownStyle = DropDownStyle.DropDownList;
                                colkodi = new ListBoxColumn();
                                colkodi.FieldName = "Kodi";
                                colprove = new ListBoxColumn();
                                colprove.FieldName = "Klasa";
                                colemer = new ListBoxColumn();
                                colemer.FieldName = "Vlera";
                                cmb.ClientSideEvents.TextChanged = String.Format("function (s,e){{ TextChangedVleraShtesa(s,e,'txtVleraParam',{0})}}", e.VisibleIndex);
                                break;

                            case "Shtese Kualifikimi":
                                colfun = new colShtesaPage(idNdermarrje, Convert.ToInt32(TipeShtesaPage.Kualifikimi));
                                colfun.Insert(0, new clsShtesaPage());
                                cmb.DataSource = colfun;
                                cmb.TextField = "Kodi";
                                cmb.ValueField = "IdShtesaPage";
                                cmb.DropDownStyle = DropDownStyle.DropDownList;
                                colkodi = new ListBoxColumn();
                                colkodi.FieldName = "Kodi";
                                colprove = new ListBoxColumn();
                                colprove.FieldName = "Klasa";
                                colemer = new ListBoxColumn();
                                colemer.FieldName = "Vlera";
                                cmb.ClientSideEvents.TextChanged = String.Format("function (s,e){{ TextChangedVleraShtesa(s,e,'txtVleraParam',{0})}}", e.VisibleIndex);
                                break;

                            case "Shtese Veshtiresie":
                                colfun = new colShtesaPage(idNdermarrje, Convert.ToInt32(TipeShtesaPage.Veshtiresia));
                                colfun.Insert(0, new clsShtesaPage());
                                cmb.DataSource = colfun;
                                cmb.TextField = "Kodi";
                                cmb.ValueField = "IdShtesaPage";
                                cmb.DropDownStyle = DropDownStyle.DropDownList;
                                colkodi = new ListBoxColumn();
                                colkodi.FieldName = "Kodi";
                                colprove = new ListBoxColumn();
                                colprove.FieldName = "Klasa";
                                colemer = new ListBoxColumn();
                                colemer.FieldName = "Vlera";
                                cmb.ClientSideEvents.TextChanged = String.Format("function (s,e){{ TextChangedVleraShtesa(s,e,'txtVleraParam',{0})}}", e.VisibleIndex);
                                break;

                            case "Shtese Vjetersie":
                                colfun = new colShtesaPage(idNdermarrje, Convert.ToInt32(TipeShtesaPage.Vjetersia));
                                colfun.Insert(0, new clsShtesaPage());
                                cmb.DataSource = colfun;
                                cmb.TextField = "Kodi";
                                cmb.ValueField = "IdShtesaPage";
                                cmb.DropDownStyle = DropDownStyle.DropDownList;
                                colkodi = new ListBoxColumn();
                                colkodi.FieldName = "Kodi";
                                colprove = new ListBoxColumn();
                                colprove.FieldName = "Klasa";
                                colemer = new ListBoxColumn();
                                colemer.FieldName = "Vlera";
                                cmb.ClientSideEvents.TextChanged = String.Format("function (s,e){{ TextChangedVleraShtesa(s,e,'txtVleraParam',{0})}}", e.VisibleIndex);
                                break;

                            case "Sigurimi Suplementare":
                                colSigurimeSuplementare colsig = new colSigurimeSuplementare();
                                colsig.mbushGjitheSigurimeSuplementareSipasNdermarjesDheDates(idNdermarrje, dteDateAkt.Date);
                                colsig.Insert(0, new clsSigurimeSuplementare());
                                cmb.DataSource = colsig;
                                cmb.TextField = "Kodi";
                                cmb.ValueField = "IdSigurimeSuplementare";
                                cmb.DropDownStyle = DropDownStyle.DropDownList;
                                colkodi = new ListBoxColumn();
                                colkodi.FieldName = "Kodi";
                                colprove = new ListBoxColumn();
                                colprove.FieldName = "Grupi";
                                colemer = new ListBoxColumn();
                                colemer.FieldName = "Perqindja";
                                cmb.ClientSideEvents.TextChanged = String.Format("function (s,e){{ TextChangedVleraSig(s,e,'txtVleraParam',{0})}}", e.VisibleIndex);
                                break;

                            default:
                                break;
                        }
                        cmb.Columns.Add(colkodi);
                        cmb.Columns.Add(colprove);
                        cmb.Columns.Add(colemer);
                        cmb.DataBind();
                        if (colpaga.Count > e.VisibleIndex && colpaga[e.VisibleIndex].Emerparam == "")
                            cmb.ClientEnabled = false;

                        cmb.ClientInstanceName = "txtVleraParam" + e.VisibleIndex;
                        if ((bool)hftabe.Get("7") == false) cmb.ClientEnabled = false;
                    }
                }
                else
                {
                    ASPxTextBox txtVleraParam = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col, "txtBox") as ASPxTextBox;

                    if (txtVleraParam != null)
                    {
                        if (colpaga.Count > e.VisibleIndex && colpaga[e.VisibleIndex].Emerparam == "")
                            txtVleraParam.ClientEnabled = false;
                        if (colpaga.Count > e.VisibleIndex && colpaga[e.VisibleIndex].Komponente == MessagesResource.Messages["msgPunonjesiPagaMeShtesat"])
                        {
                            txtVleraParam.BackColor = Color.DeepSkyBlue;
                            txtVleraParam.ForeColor = Color.Blue;
                            txtVleraParam.Font.Bold = true;
                        }
                        txtVleraParam.ClientInstanceName = "txtVleraParam" + e.VisibleIndex;
                        txtVleraParam.ClientSideEvents.TextChanged = String.Format("function (s,e){{ TextChangedVlera(s,e,'txtVleraParam',{0})}}", e.VisibleIndex);
                        if ((bool)hftabe.Get("7") == false) txtVleraParam.ClientEnabled = false;
                    }
                }
                if (lblKomponente != null)
                {
                    lblKomponente.ClientInstanceName = "lblKomponente" + e.VisibleIndex;
                }
                clsNdermarrje nderm = new clsNdermarrje(idNdermarrje);
                if (nderm.Lloji == 2)
                    if (e.VisibleIndex + 1 < colpaga.Count)
                    {
                        if (colpaga[e.VisibleIndex + 1].Njesia == 1 && colpaga[e.VisibleIndex + 1].Emerparam != "")//vendoset modeli i rreshtit tjeter
                            col.DataItemTemplate = new MyComboTemplate();
                        else col.DataItemTemplate = new MyTextTemplate();
                    }
                    else col.DataItemTemplate = new MyComboTemplate();
            }
        }

        /// <summary>
        /// ruan paga dhe shtesa
        /// </summary>
        /// <returns></returns>
        private colPagaShtesa ruajPagaShtesa(colPunesim colpun, CultureInfo ci)
        {
            var dtAktivizimi = dteDateAkt.Date.Date;
            string dtAktivizimiPaOren = dtAktivizimi.ToString("dd/MM/yyyy");
            if (colpun.Count > 0 && colpun.Min(x => x.DtFillimi.Date.Date) > dtAktivizimi)
            {
                string mesazhGabimiDate = MessagesResource.Messages["msgDateAktivizimiPunonjes"];
                mesazhGabimiDate = mesazhGabimiDate.Replace("#dtAktivizimiPaOren#", dtAktivizimiPaOren);
                throw new MyException(mesazhGabimiDate);
            }
            //TODO Julia vendose ne resource manager Data e aktivizimit #dtAktivizimi# te pages 
            //nuk mund te jete me e vogel se data e fillimit  te punesimit!"
            //me pas bej replace #dtAktivizimi#,shif dhe per mesazhe te tjera ketu
            var col = mySessionObjects.merrPageShteseNgaSesioni(Session);
            object[] vleraparam = JsonConvert.DeserializeObject<object[]>(hfParam.Value);
            object[] vlera = JsonConvert.DeserializeObject<object[]>(hfVlera.Value);
            for (int i = 0; i < col.Count; i++)
            {
                col[i].VleraParam = Converter.MerrVlereOseDefault<decimal>(vleraparam[i]);
                col[i].Vlera = Converter.MerrVlereOseDefault<decimal>(vlera[i]);
                col[i].DtAktivizimi = dtAktivizimi;
                col[i].IdPerdoruesi = idperdoruesi;
            }
            return col;
        }



        #endregion PagaShtesa

        #region KomponenteListPagese

        /// <summary>
        /// mbush komponentet e listpageses
        /// </summary>
        /// <param name="idNdermarrje"></param>
        private void mbushKomponenteListPagese(int idNdermarrje)
        {
            colKomponenteListPagesePunonjesi col = new colKomponenteListPagesePunonjesi();
            if (cmbNdryshim2.Text == "")
            {
                col = new colKomponenteListPagesePunonjesi(true, dteDateAk2.Date.Date, idNdermarrje);
                string[] kompselect = { "DM", "PP", "PT", "PPS", "SP" };
                foreach (clsKomponenteListPagesePunonjesi kom in col.Where(x => kompselect.Contains(x.KodKomponente)))
                {
                    // kom.Edukshme = true;
                    kom.Edetyrueshme = true;
                }

                col.Find(x => x.KodKomponente == "OD").VleraDefault = 8;
            }
            else col.ktheKomponenteListPagesePunonjesiSipasPunonjesitDatesLlojitNdermarjes(true, dteDateAk2.Date.Date, idNdermarrje, Convert.ToDateTime(cmbNdryshim2.Value), int.Parse(hfId.Value));
            hfKomponente.Clear();
            hfKomponente2.Clear();
            hfKomponente3.Clear();
            hfKodeKomp.Clear();
            int i = 0;
            hfKodeKomp.Add("count", col.Count);
            foreach (clsKomponenteListPagesePunonjesi cls in col)
            {
                hfKomponente.Add(cls.KodKomponente, cls.Edukshme);
                hfKomponente2.Add(cls.KodKomponente, cls.Edetyrueshme);
                hfKomponente3.Add(cls.KodKomponente, cls.VleraDefault);
                hfKodeKomp.Add(i.ToString(), cls.KodKomponente);
                i++;
            }
            //Session.Add("KomponenteListPagesePunonjes", col);
            mySessionObjects.ruajKompListPagesPunonjesiNeSesion(Session, col);

            gvKomponenteListPagese.DataSource = col;
            gvKomponenteListPagese.DataBind();
        }

        public void mbushGridKomponenteshNgaSessioni()
        {
            colKomponenteListPagesePunonjesi colcom = mySessionObjects.merrKompListPagesPunonjesiNgaSesioni(Session);
            gvKomponenteListPagese.DataSource = colcom;
            gvKomponenteListPagese.DataBind();
        }

        /// <summary>
        /// percakton kolonat template
        /// </summary>
        private void percaktoTemplateKomponente()
        {
            int formatvlefta;
            try
            {
                formatvlefta = mySessionObjects.merrFormatVleftaSesioni(Session);
            }
            catch (MyException ex)
            {
                ImbLogger.Error(ex.Message);
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, ex.Message, pnlMesazhi);
                return;
            }
            GridViewDataTextColumn col13 = gvKomponenteListPagese.Columns["KodKomponente"] as GridViewDataTextColumn;
            col13.DataItemTemplate = new MyLabelTemplate();
            GridViewDataCheckColumn col1 = gvKomponenteListPagese.Columns["Edukshme"] as GridViewDataCheckColumn;
            col1.DataItemTemplate = new MyCheckTemplate(false, false);
            GridViewDataCheckColumn col2 = gvKomponenteListPagese.Columns["Edetyrueshme"] as GridViewDataCheckColumn;
            col2.DataItemTemplate = new MyCheckTemplate(false, false);
            GridViewDataTextColumn col3 = gvKomponenteListPagese.Columns["VleraDefault"] as GridViewDataTextColumn;
            col3.DataItemTemplate = new MyDoubleTemplate(false, formatvlefta, "0");
        }

        /// <summary>
        /// perdoret per te shfaqur llojin ne vend te true/false si dhe filtri i llojit te shfaqet ne forme komboje
        /// </summary>
        //private static void shtoNjesi(ASPxGridView grida)
        //{
        //    grida.Columns.Remove(grida.Columns["Njesia"]);
        //    GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
        //    colnew.PropertiesComboBox.Items.Add("", Convert.ToInt32(NjesiPagese.Undefined));
        //    colnew.PropertiesComboBox.Items.Add(NjesiPagese.Nr.ToString(), Convert.ToInt32(NjesiPagese.Nr));
        //    colnew.PropertiesComboBox.Items.Add(NjesiPagese.Tab.ToString(), Convert.ToInt32(NjesiPagese.Tab));
        //    colnew.PropertiesComboBox.Items.Add(NjesiPagese.For.ToString(), Convert.ToInt32(NjesiPagese.For));
        //    colnew.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        //    colnew.FieldName = "Njesia";

        //    grida.Columns.Add(colnew);
        //}

        /// <summary>
        /// perdoret per te konfiguruar griden. Per te dhenat mbi kolonat qe do te shfaqen nga databaza si dhe percakton disa karakteristika te grides
        /// </summary>
        /// :  <see cref="GridUtil.percaktoVisibleColumns"/>
        /// :  <see cref="DbCore.clsFunksione.konfiguroGrideListeMadhe(grida,celesigrides)"/>
        /// <param name="idNdermarrje"></param>
        private void konfiguroGrideKomponenteListPagese(int idNdermarrje)
        {
            KonfigurimComboGride.shtoNjesiKomponenteLP(gvKomponenteListPagese, rm, cultinf);
            percaktoTemplateKomponente();
            //"275543" = mySessionObjects.ktheCultureInfo(Session);
            GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "gvKomponenteListPagese", gvKomponenteListPagese, cmbKonfigurimiKomp.Text.Split(';')[0], "708", mySessionObjects.ktheGjuhe(Session), true, 118);
            //DbCore.clsFunksione.percaktoVisibleColumnsMeWidth(DbCore.mySessionObjects.ktheGjuhePerdoruesi(Session), idNdermarrje, gvKomponenteListPagese, "gvKomponenteListPagese", "Shto_Punonjes.aspx");
            GridUtil.konfiguroGrideRegjistrimEvogelPaTheme(gvKomponenteListPagese, "IdKomponenteListPagesePunonjes", false);
            gvKomponenteListPagese.SettingsBehavior.AllowSort = false;
            gvKomponenteListPagese.SettingsBehavior.AllowGroup = true;
            gvKomponenteListPagese.Settings.ShowFilterBar = GridViewStatusBarMode.Auto;
            gvKomponenteListPagese.Settings.ShowFilterRow = true;
            gvKomponenteListPagese.GroupBy(gvKomponenteListPagese.Columns["Grup"]);
            // gvKomponenteListPagese.SettingsBehavior.AutoExpandAllGroups = true;
        }

        /// <summary>
        /// kur grida ben callback
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvKomponenteListPagese_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            gvKomponenteListPagese.JSProperties["cpCallbackCompleted"] = true;
        }

        /// <summary>
        ///  kur perdoruesi i ben callback grides
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvKomponenteListPagese_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            mbushKomponenteListPagese(mySessionObjects.merrIdNdermarrjeSesioni(Session));
            string idkomponente = "";
            string kodkonfigurimi = "";

            string[] arr = e.Parameters.Split(';');

            if (arr.Length == 2)//nese eshte zgjedhur nje konfigurim tek combo e konfigurimeve
            {
                kodkonfigurimi = arr[1];
                idkomponente = arr[0];
                konfiguroGrideKomponenteListPagese(mySessionObjects.merrIdNdermarrjeSesioni(Session));
            }
            else
            {
                idkomponente = e.Parameters;
            }
        }

        /// <summary>
        /// cakton properti ne javascript
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvKomponenteListPagese_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gvKomponenteListPagese.PageIndex;
            e.Properties["cpPageRow"] = gvKomponenteListPagese.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gvKomponenteListPagese.VisibleRowCount;
        }

        /// <summary>
        /// kur krijohet rreshti
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvKomponenteListPagese_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType == GridViewRowType.Data)
            {
                colKomponenteListPagesePunonjesi colcom = mySessionObjects.merrKompListPagesPunonjesiNgaSesioni(Session);
                GridViewDataCheckColumn col = ((ASPxGridView)sender).Columns["Edukshme"] as GridViewDataCheckColumn;
                ASPxCheckBox cbDukshme = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col, "cb") as ASPxCheckBox;
                GridViewDataTextColumn col11 = ((ASPxGridView)sender).Columns["KodKomponente"] as GridViewDataTextColumn;
                ASPxLabel lbl = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col11, "lbl") as ASPxLabel;
                if (lbl != null)
                {
                    lbl.ClientInstanceName = "lblKod" + e.VisibleIndex;
                }
                if (cbDukshme != null)
                {
                    cbDukshme.ClientInstanceName = "cbDukshme" + e.VisibleIndex;
                    cbDukshme.ClientSideEvents.CheckedChanged = "function (s,e){duksmecheck('" + lbl.Text + "','cbDukshme" + e.VisibleIndex + "')}";//+ hfKomponente.Set("+colcom[e.VisibleIndex].KodKomponente+",+".GetChecked()) }";
                    if ((bool)hftabe.Get("8") == false) cbDukshme.ClientEnabled = false;
                }
                GridViewDataCheckColumn col2 = ((ASPxGridView)sender).Columns["Edetyrueshme"] as GridViewDataCheckColumn;
                ASPxCheckBox cbDukshme2 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col2, "cb") as ASPxCheckBox;
                if (cbDukshme2 != null)
                {
                    cbDukshme2.ClientInstanceName = "cbDetyrueshme" + e.VisibleIndex;
                    cbDukshme2.ClientSideEvents.CheckedChanged = "function (s,e){detyrueshmecheck('" + lbl.Text + "','cbDetyrueshme" + e.VisibleIndex + "')}";//+ hfKomponente.Set("+colcom
                    if ((bool)hftabe.Get("8") == false) cbDukshme2.ClientEnabled = false;
                }
                GridViewDataTextColumn col3 = gvKomponenteListPagese.Columns["VleraDefault"] as GridViewDataTextColumn;
                ASPxTextBox txt = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col3, "txtBox") as ASPxTextBox;
                if (txt != null)
                {
                    if (colcom.Find(x => x.KodKomponente == lbl.Text).Njesia == 0) txt.ClientEnabled = true; else txt.ClientEnabled = false;
                    txt.ClientInstanceName = "txtVleraC" + e.VisibleIndex;
                    txt.ClientSideEvents.TextChanged = "function (s,e){vleracheck('" + lbl.Text + "','txtVleraC" + e.VisibleIndex + "')}";//+ hfKomponente.Set
                    if ((bool)hftabe.Get("8") == false) txt.ClientEnabled = false;
                }
            }
        }

        /// <summary>
        /// ruan kompnentet
        /// </summary>
        /// <returns></returns>
        private colKomponenteListPagesePunonjesi ruajKomponente(colPunesim colpun, CultureInfo ci)
        {
            var dtAktivizimi = dteDateAk2.Date.Date;
            if (colpun.Count > 0 && colpun.Min(x => x.DtFillimi.Date) > dtAktivizimi)
            {
                string mesazhGabimi = MessagesResource.Messages["msgDataAktvKomp"];
                throw new Exception(mesazhGabimi);
            }
            var col = mySessionObjects.merrKompListPagesPunonjesiNgaSesioni(Session);
            for (int i = 0; i < col.Count; i++)
            {
                col[i].Edukshme = Converter.MerrVlereOseDefault<bool>(hfKomponente.Get(col[i].KodKomponente));
                col[i].Edetyrueshme = Converter.MerrVlereOseDefault<bool>(hfKomponente2.Get(col[i].KodKomponente));
                col[i].VleraDefault = Converter.MerrVlereOseDefault<decimal>(hfKomponente3.Get(col[i].KodKomponente));
                col[i].DtAktivizimi = dtAktivizimi;
                col[i].IdPerdoruesi = idperdoruesi;
            }

            return col;
        }



        #endregion KomponenteListPagese

        private clsBankaPunonjes krijoBanka(int idNdermarrje)
        {
            int idbanka;
            if (cmbBanka.Text != "")
            {
                clsBanka dep = new clsBanka();
                dep.mbushBankeSipasKodit(cmbBanka.Text, idNdermarrje);

                idbanka = dep.IdBanka;
            }
            else idbanka = 0;

            clsBankaPunonjes qk = new clsBankaPunonjes(0, 0, txtLlogBankare.Text, idbanka, decimal.Parse(txtLimitTel.Text), decimal.Parse(txtLimitInternet.Text), dteDateAkt.Date.Date, idperdoruesi);

            return qk;
        }

        #region autocomplete

        /// <summary>
        /// perdoret per te mbushur combon e departamenteve ne momentin qe perdoruesi fillon te shkruaj
        /// </summary>
        /// <param name="source">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void cmbDepartamenti_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].Contains("cmbDepartamenti"))
                {
                    ConfigureAspxComboBox.mbushComboStrukturaAdm(cmbDepartamenti, 0, mySessionObjects.merrIdNdermarrjeSesioni(Session));
                }
            }
        }

        /// <summary>
        /// perdoret per te mbushur combon e nendepartamenteve ne momentin qe perdoruesi fillon te shkruaj
        /// </summary>
        /// <param name="source">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void cmbNenDepartamenti_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].Contains("cmbNenDepartamenti"))
                {
                    if (e.Value == null) return;
                    int id;
                    if (cmbDepartamenti.Text != "")
                        id = Convert.ToInt32(cmbDepartamenti.Value);
                    else
                        id = 0;

                    DbCore.DbListPagesat.colStrukturatAdministrative col = new DbCore.DbListPagesat.colStrukturatAdministrative();
                    if (id == 0)
                        col.mbushGjitheStrukturaAdmPrindiSipasNdermarjes(idNdermarrje,true);
                    else col.mbushStrukturaAdmSipasPrindit(id,true);
                    cmbNenDepartamenti.DataSource = col.Where(x => x.IdStrukturaAdm == Convert.ToInt32(e.Value));
                    cmbNenDepartamenti.TextField = "Emri";
                    cmbNenDepartamenti.ValueField = "IdStrukturaAdm";

                    //clsFunksione.mbushComboStrukturaAdm(cmbNenDepartamenti, id, mySessionObjects.merrIdNdermarrjeSesioni(Session));
                }
            }
        }
        protected void cmbNenDepartamenti_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].Contains("cmbNenDepartamenti"))
                {
                    if (string.IsNullOrWhiteSpace(e.Filter)) return;
                    int id;
                    if (cmbDepartamenti.Text != "")
                        id = Convert.ToInt32(cmbDepartamenti.Value);
                    else
                        id = 0;

                    DbCore.DbListPagesat.colStrukturatAdministrative col = new DbCore.DbListPagesat.colStrukturatAdministrative();
                    if (id == 0)
                        col.mbushGjitheStrukturaAdmPrindiSipasNdermarjes(idNdermarrje,true);
                    else col.mbushStrukturaAdmSipasPrindit(id,true);
                    var dsReal = col.Where(x => x.Emri.IndexOf(e.Filter, StringComparison.InvariantCultureIgnoreCase) > -1).Skip(e.BeginIndex).Take(e.EndIndex + 1);
                    cmbNenDepartamenti.TextField = "Emri";
                    cmbNenDepartamenti.ValueField = "IdStrukturaAdm";
                    cmbNenDepartamenti.DataSource = dsReal.ToList();
                    cmbNenDepartamenti.DataBind();
                   

                }
            }
        }

        /// <summary>
        /// perdoret per te mbushur combon e grupeve te punonjesve ne momentin qe perdoruesi fillon te shkruaj
        /// </summary>
        /// <param name="source">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void cmbGrupi_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].Contains("cmbGrupi"))
                {
                    ConfigureAspxComboBox.mbushComboGrupePunonjesish(cmbGrupi, mySessionObjects.merrIdNdermarrjeSesioni(Session));
                }
            }
        }

        /// <summary>
        /// perdoret per te mbushur combon e grupeve te punonjesve ne momentin qe perdoruesi fillon te shkruaj
        /// </summary>
        /// <param name="source">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void cmbObjektiva_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("cmbObjektiva"))
                ConfigureAspxComboBox.KonfiguroComboBoxObjektivaKosto(cmbObjektiva, IdNdermarrja);
        }

        /// <summary>
        /// perdoret per te mbushur combon e tip kontrate ne momentin qe perdoruesi fillon te shkruaj
        /// </summary>
        /// <param name="source">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void cmbTipKontrate_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].Contains("cmbTipKontrate"))
                {
                    ConfigureAspxComboBox.mbushComboTipKontrate(cmbTipKontrate, mySessionObjects.merrIdNdermarrjeSesioni(Session), idgjuha);
                }
            }
        }

  
        protected void cmbIdLlogari_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("cmbIdLlogari"))
            {
                ConfigureAspxComboBox.mbushComboLlogariaPaKolona(IdPerdoruesi, IdNdermarrja, cmbIdLlogari, e);
            }
        }

        protected void cmbIdLlogari_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("cmbIdLlogari"))
            {

                ConfigureAspxComboBox.mbushComboLlogariaPaKolona(IdPerdoruesi, IdNdermarrja, cmbIdLlogari, e);
            }
        }

        protected void cmbProfesioni_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].Contains("cmbProfesioni"))
                {
                    //clsFunksione.mbushComboProfesioneTituj(cmbProfesioni, mySessionObjects.merrIdNdermarrjeSesioni(Session), 1);
                    if (e.Value == null) return;
                    idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
                    var lloji = 1;
                    DbCore.DbListPagesat.colProfesioneTitujPune col = new DbCore.DbListPagesat.colProfesioneTitujPune();
                    col.ktheGjitheProfesioneTitujPunetSipasNdermarjesDheLlojitAktiv(idNdermarrje, lloji);

                    cmbProfesioni.TextField = _ci.Name == "sq-AL" ? "Pershkrimi" : "PershkrimiAng";
                    cmbProfesioni.ValueField = "Id";
                    cmbProfesioni.DataSource = col.Where(x => x.Id == Convert.ToInt32(e.Value));
                    cmbProfesioni.DataBind();
                }
            }
        }
        protected void cmbProfesioni_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].Contains("cmbProfesioni"))
                {

                    if (string.IsNullOrEmpty(e.Filter)) return;
                    idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
                    var lloji = 1;
                    DbCore.DbListPagesat.colProfesioneTitujPune col = new DbCore.DbListPagesat.colProfesioneTitujPune();
                    col.ktheGjitheProfesioneTitujPunetSipasNdermarjesDheLlojitAktiv(idNdermarrje, lloji);
                    cmbProfesioni.DataSource = col.Where(x => x.Pershkrimi.Contains(e.Filter)).Skip(e.BeginIndex).Take(e.EndIndex);
                    cmbProfesioni.TextField = _ci.Name == "sq-AL" ? "Pershkrimi" : "PershkrimiAng";
                    cmbProfesioni.ValueField = "Id";
                    cmbProfesioni.DataBind();
                }
            }
        }



        protected void cmbKodeProfesione_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].Contains("cmbKodeProfesione"))
                {
                    ConfigureAspxComboBox.mbushComboKodeProfesione(cmbKodeProfesione, mySessionObjects.merrIdNdermarrjeSesioni(Session));
                }
            }
        }

        protected void cmbQK1_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].Contains("cmbQK1"))
                {
                    ConfigureAspxComboBox.mbushComboQendraKostoPrindNiveli1(mySessionObjects.merrIdNdermarrjeSesioni(Session), cmbQK1);

                }
            }
        }

        protected void cmbQK1_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].Contains("cmbQK1"))
                {
                    if (string.IsNullOrWhiteSpace(e.Filter)) return;
                    DbCore.DbQendraKosto.colQendraKosto col = new DbCore.DbQendraKosto.colQendraKosto();

                    col.ktheGjitheQendraKostoPrindiNiveli1SipasNdermarjesAktiv(mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    cmbQK1.DataSource = col.Where(x => x.Kodi.IndexOf(e.Filter, StringComparison.InvariantCultureIgnoreCase) > -1).Skip(e.BeginIndex).Take(e.EndIndex + 1);

                    cmbQK1.TextField = "Kodi";
                    cmbQK1.ValueField = "Id";
                    cmbQK1.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                    cmbQK1.DataBind();


                }
            }
        }

        protected void cmbQK2_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].Contains("cmbQK2"))
                {
                    if (cmbQK1.Value != null)
                        ConfigureAspxComboBox.mbushComboQendraKostoBijSipasPrindit(int.Parse(cmbQK1.Value.ToString()), cmbQK2);
                    else ConfigureAspxComboBox.mbushComboQendraKostoBij(mySessionObjects.merrIdNdermarrjeSesioni(Session), cmbQK2);
                }
            }
        }

        protected void cmbQK2_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].Contains("cmbQK2"))
                {
                    if (string.IsNullOrWhiteSpace(e.Filter)) return;
                    DbCore.DbQendraKosto.colQendraKosto col = new DbCore.DbQendraKosto.colQendraKosto();
                    if (cmbQK1.Value != null)
                        col.mbushQendraSipasPrindit(int.Parse(cmbQK1.Value.ToString()));
                    else
                        col.mbushGjitheQendraKostoBijSipasNdermarjesAktiv(mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    cmbQK2.DataSource = col.Where(x => x.Kodi.IndexOf(e.Filter, StringComparison.InvariantCultureIgnoreCase) > -1).Skip(e.BeginIndex).Take(e.EndIndex + 1);

                    cmbQK2.TextField = "Kodi";
                    cmbQK2.ValueField = "Id";
                    cmbQK2.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                    cmbQK2.DataBind();


                }
            }
        }
        protected void cmbLocal_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].Contains("cmbLocal") && e.Value != null)
                {
                    if (cmbGlobal.Value != null)
                        ConfigureAspxComboBox.mbushComboGrupimeGlobalLocalSipasPrindit(cmbLocal, mySessionObjects.merrIdNdermarrjeSesioni(Session), int.Parse(cmbGlobal.Value.ToString()), Convert.ToInt32(e.Value));
                    else ConfigureAspxComboBox.mbushComboGrupimeGlobalLocal(cmbLocal, mySessionObjects.merrIdNdermarrjeSesioni(Session), 2, Convert.ToInt32(e.Value));
                }
            }
        }
        protected void cmbLocal_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("cmbLocal") && !string.IsNullOrWhiteSpace(e.Filter))
            {
                if (cmbGlobal.Value != null)
                    ConfigureAspxComboBox.mbushComboGrupimeGlobalLocalSipasPrindit(cmbLocal, mySessionObjects.merrIdNdermarrjeSesioni(Session), int.Parse(cmbGlobal.Value.ToString()), e.Filter);
                else ConfigureAspxComboBox.mbushComboGrupimeGlobalLocal(cmbLocal, mySessionObjects.merrIdNdermarrjeSesioni(Session), 2, e.Filter);
            }
        }

        /// <summary>
        /// perdoret per te mbushur combon e bankes ne momentin qe perdoruesi fillon te shkruaj
        /// </summary>
        /// <param name="source">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void cmbBanka_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].Contains("cmbBanka"))
                {
                    ConfigureAspxComboBox.mbushComboBankat(mySessionObjects.ktheIdPerdoruesi(Session), mySessionObjects.merrIdNdermarrjeSesioni(Session), cmbBanka, false);
                }
            }
        }

        protected void cmbVendndodhjet_Callback(object sender, CallbackEventArgsBase e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].Contains("cmbVendndodhjet"))
                {
                    ConfigureAspxComboBox.mbushComboVendndodhjet(idNdermarrje, cmbVendndodhjet);
                }
            }
        }

        #endregion autocomplete

        protected void ASPxCallback1_Callback(object sender, CallbackEventArgsBase e)
        {
            mbushKomponenteListPagese(mySessionObjects.merrIdNdermarrjeSesioni(Session));
        }

        #region WEBMETHODS

        [WebMethod(EnableSession = true)]
        public static object GetPunonjesSipasID(int idPunonjesi)
        {
            if (HttpContext.Current == null)
            {
                return null;
                ImbLogger.Error($"Ndodhi nje rast i rralle,nuk ka session ");
            }
            var punonjesit = mySessionObjects.merrObjectNgaSesioni(HttpContext.Current.Session, punonjesitSessionKey) as DataTable;
            if (punonjesit == null)
            {
                ImbLogger.Warn("Nuk u gjet nje list me punonjes ne session! metoda 'GetPunonjesSipasID'");
                punonjesit = colPunonjes.merrPunonjesNdermarjeDT(mySessionObjects.merrIdNdermarrjeSesioni(HttpContext.Current.Session), mySessionObjects.ktheGjuhe(HttpContext.Current.Session));
                mySessionObjects.ruajObjectNeSesion(HttpContext.Current.Session, punonjesit, punonjesitSessionKey);
            }
            var rowPerTuKthyer = punonjesit.Select("IdPunonjes =" + idPunonjesi).AsEnumerable().Select(x => new
            {
                IdPunonjes = x.Field<object>("IdPunonjes"),
                NrPersonal = x.Field<object>("NrPersonal"),
                Emer = x.Field<object>("Emer"),
                Mbiemer = x.Field<object>("Mbiemer"),
                Atesia = x.Field<object>("Atesia"),
                Datelindja = x.Field<object>("Datelindja"),
                NrSig = x.Field<object>("NrSig"),
                IdQyteti = x.Field<object>("IdQyteti"),
                Adresa = x.Field<object>("Adresa"),
                Telefon = x.Field<object>("Telefon"),
                Email = x.Field<object>("Email"),
                Aktiv = x.Field<object>("Aktiv"),
                EmerKontakti = x.Field<object>("EmerKontakti"),
                MbiemerKontakti = x.Field<object>("MbiemerKontakti"),
                TelKontakti = x.Field<object>("TelKontakti"),
                AdresaKontakti = x.Field<object>("AdresaKontakti"),
                EmailKontakti = x.Field<object>("EmailKontakti"),
                ShenimeKontakti = x.Field<object>("ShenimeKontakti"),
                IdGrupPunonjesish = x.Field<object>("IdGrupPunonjesish"),
                LlojPagese = x.Field<object>("LlojPagese"),
                IdMonedha = x.Field<object>("IdMonedha"),
                Qyteti = x.Field<object>("Qyteti"),
                GrupPunonjesish = x.Field<object>("GrupPunonjesish"),
                Monedha = x.Field<object>("Monedha"),
                IdObjektivaKosto = x.Field<object>("IdObjektivaKosto"),
                Objektiva = x.Field<object>("Objektiva"),
                SapId = x.Field<object>("SapId"),
                LlogaritNgaListorare = x.Field<object>("LlogaritNgaListorare"),
                NrPashaporte = x.Field<object>("NrPashaporte"),
                Gjinia = x.Field<object>("Gjinia"),
                Kombesia = x.Field<object>("Kombesia"),
                Kryefamiliar = x.Field<object>("Kryefamiliar"),
                Edukimi = x.Field<object>("Edukimi"),
                PunaMeparshme = x.Field<object>("PunaMeparshme"),
                Vendndodhja = x.Field<object>("Vendndodhja"),
                NrJupiter = x.Field<object>("NrJupiter"),
                Username = x.Field<object>("Username"),
                Shenime = x.Field<object>("Shenime"),
                PershkrimiVendodhja = x.Field<object>("PershkrimiVendodhja"),
                NrRendor = x.Field<object>("NrRendor"),
                LejePune = x.Field<object>("LejePune"),
                IdLlogari = x.Field<object>("IdLlogari"),
            }).FirstOrDefault();
            return JsonConvert.SerializeObject(rowPerTuKthyer);
            return rowPerTuKthyer;
            //  return JsonConvert.SerializeObject(row);
        }

        [WebMethod(EnableSession = true)]
        public static object GetPunonesimSipasID(int idPunesim, int idPunonjesi)
        {
            if (HttpContext.Current == null)
            {
                return null;
                ImbLogger.Error($"Ndodhi nje rast i rralle,nuk ka session ");
            }

            var punesimet = mySessionObjects.merrPunesimNgaSesioni(HttpContext.Current.Session);
            if (punesimet == null)
            {
                punesimet = colPunesim.KthePunesimSipasIdPuneonjesiDt(idPunonjesi, mySessionObjects.ktheGjuhe(HttpContext.Current.Session));
                mySessionObjects.ruajPunesimNeSesion(HttpContext.Current.Session, punesimet);
            }
            return JsonConvert.SerializeObject(punesimet.Find(x => x.IdPunesim == idPunesim));
        }

        [WebMethod(EnableSession = true)]
        public static object GetQendraKostoSipasID(int id, int idPunonjesi)
        {
            var qendraNgaSessioni = (DataTable)mySessionObjects.merrObjectNgaSesioni(HttpContext.Current.Session, qendraKostoSessionKey);
            if (qendraNgaSessioni == null)
            {
                DataTable col = colQendraKostoPunonjes.ktheQendraPunonjesi(idPunonjesi);
                mySessionObjects.ruajObjectNeSesion(HttpContext.Current.Session, col, qendraKostoSessionKey);
            }
            var perTuKthyer = qendraNgaSessioni.Select("Id=" + id).Select(x => new
            {
                Id = x.Field<object>("Id"),
                IdPunonjes = x.Field<object>("IdPunonjes"),
                IdQenderKosto1 = x.Field<object>("IdQenderKosto1"),
                IdQenderKosto2 = x.Field<object>("IdQenderKosto2"),
                DtAktivizimi = x.Field<object>("DtAktivizimi"),
                Qendra1 = x.Field<object>("Qendra1"),
                Qendra2 = x.Field<object>("Qendra2"),
               
            }).FirstOrDefault();
            return JsonConvert.SerializeObject(perTuKthyer);
        }
        [WebMethod(EnableSession = true)]
        public static object GetBandaSipasID(int id, int idPunonjesi)
        {
            var bandaNgaSessioni = (DataTable)mySessionObjects.merrObjectNgaSesioni(HttpContext.Current.Session, bandSessionKey);
            if (bandaNgaSessioni == null)
            {
                DataTable col = colBandaPunonjes.ktheBandaPunonjesi(idPunonjesi);
                mySessionObjects.ruajObjectNeSesion(HttpContext.Current.Session, col, bandSessionKey);
            }
            var perTuKthyer = bandaNgaSessioni.Select("Id=" + id).Select(x => new
            {
                Id = x.Field<object>("Id"),
                IdPunonjes = x.Field<object>("IdPunonjes"),
                IdGrupimGlobal = x.Field<object>("IdGrupimGlobal"),
                IdGrupimLokal = x.Field<object>("IdGrupimLokal"),
                DtAktivizimi = x.Field<object>("DtAktivizimi"),
                Global = x.Field<object>("Global"),
                Local = x.Field<object>("Local"),
            }).FirstOrDefault();
            return JsonConvert.SerializeObject(perTuKthyer);
        }




        #endregion WEBMETHODS

        protected void gvPunonjesit_CustomColumnSort(object sender, CustomColumnSortEventArgs e)
        {
            if (e.Column.FieldName == ("NrPersonal"))
            {
                int vl1 = 0;
                int vl2 = 0;

                if (int.TryParse(e.Value1.ToString(), out vl1) && int.TryParse(e.Value2.ToString(), out vl2))
                {
                    if (vl1 > vl2)
                        e.Result = 1;
                    else
                        e.Result = vl1 == vl2 ? 0 : -1;
                    e.Handled = true;
                }
                else
                    e.Handled = false;
            }
        }
    }
}