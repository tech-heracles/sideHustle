using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using DevExpress.Web;
using System.Data;
using PlatinumWeb.Templates;
using System.Web.Script.Serialization;
using DbCore.DbAdmin;
using System.Globalization;
using System.Resources;
using DbCore.IMBUtils.Types;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{
    /// <summary>
    /// Faqja e shtimit te skemave
    /// </summary>
    public partial class Shto_SkemaWorkFlow : MyPageBase
    {
        System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
  
        private const string gabim2aktivitetenegride = "GABIM: Ndodhen 2 skema me te njejten id ne gride";
        private int  idperdoruesi,idnderviti, idNdermarrje, idviti, idlicenca, idgjuha;
        private const int idstatusdok = 1;
        /// <summary>
        /// perdoret per te vendosur themen e devexpresit faqes
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        private string komponente = "Shto_SkemaWorkFlow.aspx";
        /// <summary>
        /// thirret kur faqja lodohet. Ne te behet kontrolli nese perdoruesi eshte i loguar ne sistem  dhe nqs jo ridrejtohet tek forma e logimit
        /// thirret inicializimi i konfigurimeve fillestare te faqes
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e"> argumenti</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                Response.Redirect(DbCore.IMBUtils.Paths.defaultLoginPath);
                return;
            }
            idperdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idperdoruesi);
                return;
            }
            idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            idviti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            idgjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            idnderviti = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
            idlicenca = DbCore.DbAdmin.clsLicenca.merrIdLicencePerdoruesi(idperdoruesi);
            if (!Page.IsPostBack)
            {
                
                EmrateTabeve();
                hfState.Set("idGjuha", idgjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                DbCore.mySessionObjects.ruajKaDokAprovimi(Session, false);
                ASPxPageControl1.ActiveTabIndex = 0;
                percaktoTemplateMenu(idgjuha, idviti, idperdoruesi, idNdermarrje, ASPxMenu1);
                konfiguroVleraFillestare(idNdermarrje, rm, ci, idgjuha);
                mbushComboBoxFiltra(idgjuha, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
                mbushGridNgaDB(idNdermarrje);
                konfiguroGrideTrupi(idNdermarrje, ci, rm);
                konfiguroGride(idNdermarrje, cmbKonfigurimi.Text.Split(';')[0], 178, rm , ci);
                mbushHiddenFieldMePerkthime();
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "gvSkema", gvSkema, cmbKonfigurimi.Text.Split(';')[0], 178.ToString(), idgjuha);
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idperdoruesi, idNdermarrje, idviti, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idperdoruesi, idNdermarrje, idviti, komponente);
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
            }
            else
            {
                mbushGridNgaSession(idNdermarrje);
                konfiguroGride(idNdermarrje, cmbKonfigurimi.Text.Split(';')[0], 178, rm, ci);
            }
            GridUtil.konfigGrideListeEMadhePaTheme(gvSkema, "IdKoka");
            percaktoTemplateMenu(idgjuha, idviti, idperdoruesi, idNdermarrje, ASPxMenu1);
            percaktoTemplateTrupash();
            mbushComboBoxFiltra(idgjuha, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            AspxWebControlUtils.perkthePopUp(popFshi, MessagesResource.Messages["labelAdministrimiKujdes"], lblMsgbox, MessagesResource.Messages["labelAdministrimiMsgJeniSigurt"], ButtonCancel, MessagesResource.Messages["labelAnullo"]);
            GridUtil.EmrateButonaveMbiGride(gvSkema);
        }

        /// <summary>
        /// Vendos emrat e tabeve ne baze te gjuhes se perdoruesit
        /// </summary>
        private void EmrateTabeve()
        {
            ASPxPageControl1.TabPages[0].Text = MessagesResource.Messages["TePergjithshmeTab"];
            ASPxPageControl1.TabPages[1].Text = MessagesResource.Messages["MenuItemSkemaWorkFlow"];
            konfigurimi_Label.Text = MessagesResource.Messages["lblModeli"];
            popFshi.HeaderText = MessagesResource.Messages["labelKujdes"];
            lblMsgbox.Text = MessagesResource.Messages["labelAdministrimiMsgJeniSigurt"];
            ButtonOk.Text = MessagesResource.Messages["labelOk"];
            ButtonCancel.Text = MessagesResource.Messages["btnCancel"];
        }
        private void mbushHiddenFieldMePerkthime()
        {
            hfState.Set("msgDelegues", MessagesResource.Messages["msgDelegues"]);
            hfState.Set("msgDeleguesEmail", MessagesResource.Messages["msgDeleguesEmail"]);
            hfState.Set("msgPerdorues", MessagesResource.Messages["msgPerdorues"]);
            hfState.Set("msgPerdoruesEmail", MessagesResource.Messages["msgPerdoruesEmail"]);
            hfState.Set("msgPunonjesJoNjejtePerDelegim", MessagesResource.Messages["msgPunonjesJoNjejtePerDelegim"]);
            hfState.Set("msgZgjidhPerdoruesin", MessagesResource.Messages["msgZgjidhPerdoruesin"]);
            hfState.Set("msgZgjidhRolin", MessagesResource.Messages["msgZgjidhRolin"]);
            hfState.Set("msgVleraLimitNumer", MessagesResource.Messages["msgVleraLimitNumer"]);
            hfState.Set("msgAdresaEmailEPavlefshme", MessagesResource.Messages["msgAdresaEmailEPavlefshme"]);
            hfState.Set("msgDitetDuhenNumerike", MessagesResource.Messages["msgDitetDuhenNumerike"]);
            hfState.Set("msgKyRolNukEkziston", MessagesResource.Messages["msgKyRolNukEkziston"]);
            hfState.Set("msgSkemaQKZgjidhniNjeSkeme", MessagesResource.Messages["msgSkemaQKZgjidhniNjeSkeme"]);
        }
        /// <summary>
        /// mbush combon e filtrave
        /// </summary>
        /// <param name="idNdermarrje">id e ndermarjes</param>
        private static void mbushComboBoxFiltra(int idgjuha, int idNdermarrje, int idkonfig)
        {
            clsToolbarConfig.mbushComboBoxFiltraMeValFieldTextField(idgjuha, idNdermarrje, "gvSkema", "Shto_SkemaWorkFlow.aspx", "IdFiltra", "FiltraShenime", idkonfig);
        }
        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="idNderVit"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        private void percaktoTemplateMenu(int IdGjuha, int idViti, int idPerdorues, int idNdermarrje, ASPxMenu aSPxMenu1)
        {
            clsToolbarConfig.percaktoTemplateMenu(IdGjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1, komponente, this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, hfShtimModifikim.Value == "modifikim" ? false : true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
        }

        protected void RuajKolona_Click(object sender, EventArgs e)
        {
            int idfiltri = 0;
            int idkonf = DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(cmbKonfigurimi.Text.Split(';')[0], idNdermarrje);
            DbCore.clsMesazh mesazh = GridUtil.ruajFiltra(idNdermarrje, idperdoruesi, idgjuha, "gvSkema", komponente, "FilterDefault", gvSkema.FilterExpression, gvSkema, "Kodi", idkonf, out idfiltri);//ruan filtrin e zgjedhur tek filtrat
            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                return;

            }
            mesazh = GridUtil.ruajkonfigurimgride(gvSkema, cmbKonfigurimi.Text, idNdermarrje, idperdoruesi, 178, idfiltri, idviti, DbCore.mySessionObjects.ktheCultureInfo(Session), DbCore.mySessionObjects.ktheGjuhe(Session));/// ruan konfigurimin e grides dhe filtrin

            clsToolbarConfig.mbushComboBoxFiltra(idgjuha, idNdermarrje, "gvSkema", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);

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
        protected void gvSkema_DataBound(object sender, EventArgs e)
        {// shton colonen # per selektim dhe disa karakteristika te grides
            if (gvSkema.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per 
                //perzgjidh
                GridViewCommandColumn check = new GridViewCommandColumn("#") { ShowSelectCheckbox = true, Width = System.Web.UI.WebControls.Unit.Percentage(2) };
                //behet per te afishuar rreshtin qe do sherbej per filtrim
                gvSkema.Settings.ShowFilterRow = true;
                gvSkema.Settings.ShowFilterRowMenu = true;
                gvSkema.Columns.Add(check);
                gvSkema.KeyFieldName = "IdKoka";
                gvSkema.SettingsBehavior.AllowSelectByRowClick = true;
                gvSkema.SettingsBehavior.AllowFocusedRow = true;
            }

        }

        /// <summary>
        /// perdoret per te konfiguruar griden. Per te dhenat mbi kolonat qe do te shfaqen nga databaza si dhe percakton disa karakteristika te grides
        /// </summary>
        /// :  <see cref="GridUtil.percaktoVisibleColumns"/>
        /// :  <see cref="DbCore.clsFunksione.konfiguroGrideListeMadhe(grida,celesigrides)"/>
        /// <param name="idNdermarrje">id e ndermarjes</param>
        /// <param name="idKomponente"> id e komponentes</param>
        /// <param name="kodKonfigurimi">kodi i konfigurimit</param>
        private void konfiguroGride(int idNdermarrje, string kodKonfigurimi, int idKomponente, ResourceManager rm, CultureInfo ci)
        {
            KonfigurimComboGride.shto_Formule(gvSkema, rm, ci);

                gvSkema.Columns["#"].VisibleIndex = 0;

        }

      

        /// <summary>
        /// perdoret ne rastet kur grida ben callback per ta rimbushur griden
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void gvSkema_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            if (e.CallbackName == "COLUMNMOVE" && gvSkema.AllColumns[int.Parse(e.Args[0])].Width.Value == 0)
                gvSkema.AllColumns[int.Parse(e.Args[0])].Width = System.Web.UI.WebControls.Unit.Percentage(3);
            if (e.CallbackName == "APPLYFILTER" && e.Args[0] == "")
            {
                MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
                ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
                cmbFiltra.Text = "";

            }
            // konfiguroVleraFillestare(); 
            //  konfiguroGride(idNdermarrje, cmbKonfigurimi.Text.Split(';')[0], 802);

        }

        /// <summary>
        /// perdoret per te vendosur filtrat tek headeri i grides
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvSkema_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
           CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
           ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            string TeGjithe = MessagesResource.Messages["GridHeaderFilterFillItemTeGjithe"];
            string nga = MessagesResource.Messages["GridHeaderFilterFillItemNga"];
            if (e.Column.FieldName == "Emertimi" || e.Column.FieldName == "Kodi" || e.Column.FieldName == "Description" || e.Column.FieldName == "Code")
            {
                    e.Values.Clear();
                    e.AddValue(TeGjithe, string.Empty, "true");
                    e.AddValue(nga + " A-D", string.Empty, e.Column.FieldName + ">'A     ' and " + e.Column.FieldName + " <'DDDDDDD'");
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
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        public void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje
            MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
            //filtra.mbushFiltraGridaSipasFiltraEmri(cmbFiltra.Text, idNdermarrje);
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvSkema", komponente, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), int.Parse(cmbKonfigurimi.Value.ToString()));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();
                mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
                percaktoTemplateMenu(idgjuha, idviti, idperdoruesi, idNdermarrje, ASPxMenu1);

                if (mesazh.Status == true)
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
            MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;

            DbCore.DbAdmin.clsFiltraGrida filtri = new DbCore.DbAdmin.clsFiltraGrida() { FiltraKodi = cmbFiltra.Text, FiltraShenime = cmbFiltra.Text, FiltraUniversal = false };
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvSkema", komponente, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gvSkema.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("Kodi", gvSkema);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvSkema.GetSortedColumns();
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
            filtri.IdPerdoruesi = oPerdorues.IdPerdoruesi;
            filtri.IdNdermarje = idNdermarrje;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            filtri.IdStatusDok = 1;

            mesazh = filtri.ruaj();
            mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            percaktoTemplateMenu(idgjuha, idviti, idperdoruesi, idNdermarrje, ASPxMenu1);

            if (mesazh.Status == true)
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
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            if (ASPxPageControl1.ActiveTabIndex == 0)//nqs kemi zgjedhur vetem nje kartele per te pare fshijme vetem ate
                rreshtat = gvSkema.GetSelectedFieldValues("IdKoka");
            else
            {
                rreshtat = new List<object>();
                rreshtat.Add(hfId.Value);
            }
            //List<object> rreshtat = gvSkema.GetSelectedFieldValues("IdKoka");
            if (rreshtat.Count == 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgSkemaQKZgjidhniTePAkten1Skeme"], pnlMesazhi);
                return;
            }
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            //clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "", pnlMesazhi);
            List<string> TeFshire = new List<string>(), TePaFshire = new List<string>();
            DbCore.DbRegjistrim.clsDatabaseRegjistrim dbRegjistrim = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();

            foreach (object id in rreshtat)
            {
                DbCore.DbAdmin.clsKokaSkemaWorkFlow koka = new DbCore.DbAdmin.clsKokaSkemaWorkFlow(Convert.ToInt32(id));
                DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti() { IdKonfigAmbjente = koka.IdKonfig };
                konf.mbushKonfigAmbjSipasId(konf.IdKonfigAmbjente);
                bool lidhur = dbRegjistrim.eshteDokumentiILidhurCelje(koka.IdKoka.ToString(), konf.IdNivel.ToString());
                if (lidhur)
                {
                    TePaFshire.Add(koka.Kodi);
                    continue;
                }

                mesazh = koka.fshiSkemeWorkFlow(Convert.ToInt32(id), idperdoruesi, ci,rm);
                if (koka.IdKoka == 0)
                    continue;
                if (mesazh.Status)
                {
                    #region Heq llogarite nga grida
                    hiqNgaGrida(koka.IdKoka, koka.IdNdermarje);
                    #endregion
                    TeFshire.Add(koka.Kodi);

                    ASPxPageControl1.ActiveTabIndex = 0;
                    hfStatusi.Value = "true";
                }
            }
            dbRegjistrim.Dispose();
            string mesazhInfoGabim = "", mesazhInfoSukses = "";
            if (TePaFshire.Count == 1)
                mesazhInfoGabim = String.Format("{0} {1} {2}", MessagesResource.Messages["msgSkemaQKPrefixNjejes"], String.Join("; ", TePaFshire), MessagesResource.Messages["msgStrukturaAdministrativeSuffixNjejesGabimi"] );
            else
                if (TePaFshire.Count > 1)
                mesazhInfoGabim = String.Format("{0} {1} {2}", MessagesResource.Messages["msgSkemaQKPrefixShumes"], String.Join(";", TePaFshire), MessagesResource.Messages["msgStrukturaAdministrativeSuffixShumesGabimi"] );
            if (TeFshire.Count == 1)
                mesazhInfoSukses = String.Format("{0} {1} {2}", MessagesResource.Messages["msgSkemaQKPrefixNjejes"], String.Join(";", TeFshire), MessagesResource.Messages["msgStrukturaAdministrativeSuffixNjejesSuksesi"]);
            else
                if (TeFshire.Count > 1)
                mesazhInfoSukses = String.Format("{0} {1} {2}", MessagesResource.Messages["msgSkemaQKPrefixShumes"], String.Join(";", TeFshire), MessagesResource.Messages["msgStrukturaAdministrativeSuffixShumesSuksesi"]);
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
        /// <param name="idndermarje">idndermarjes</param>
        /// <param name="idkoka"> idkoka</param>
        private void hiqNgaGrida(int idkoka, int idndermarje)
        {
            if (gvSkema.DataSource != null)
            {
                DataTable dt = (DataTable)gvSkema.DataSource;
                DataRow[] drs = dt.Select(String.Format("IdKoka = '{0}'", idkoka));
                if (drs.Length > 1)
                    throw new Exception(gabim2aktivitetenegride);
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                dt.Rows.Remove(dr);
                gvSkema.DataBind();
            }
            else mbushGridNgaDB(idndermarje);
        }

        /// <summary>
        /// shton nte gride rreshtat e rinj
        /// </summary>
        /// <param name="idNdermarrje">idndermarje</param>
        /// <param name="idkoka">id e kokes</param>
        private void shtoNeGrid(int idNdermarrje, int idkoka)
        {
            if (gvSkema.DataSource != null)
            {
                DataTable dt = (DataTable)gvSkema.DataSource;
                DataRow[] drs = dt.Select("IdKoka = " + idkoka);
                if (drs.Length > 0)
                    throw new Exception(gabim2aktivitetenegride);
                DataRow newArtDr = DbCore.DbAdmin.colKokaSkemaWorkFlow.merrKokaSkemaWorkFlowNdermarrjesDR(idkoka);
                dt.ImportRow(newArtDr);
            }
            else mbushGridNgaDB(idNdermarrje);
        }

        /// <summary>
        /// modifikon ne gride aktivitetin e modifikuar
        /// </summary>
        /// <param name="idNdermarrje">idndermarje</param>
        ///<param name="idkoka">id koka</param>
        private void modifikoNeGrid(int idNdermarrje, int idkoka)
        {
            if (gvSkema.DataSource != null)
            {
                DataTable dt = (DataTable)gvSkema.DataSource;
                DataRow[] drs = dt.Select("IdKoka = " + idkoka);
                if (drs.Length > 1)
                    throw new Exception(gabim2aktivitetenegride);
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                DataRow newArtDr = DbCore.DbAdmin.colKokaSkemaWorkFlow.merrKokaSkemaWorkFlowNdermarrjesDR(idkoka);
                object[] arr = newArtDr.ItemArray;
                dr.ItemArray = arr;
            }
            else mbushGridNgaDB(idNdermarrje);
        }

        /// <summary>
        /// perdoret per te trajtuar ngjarjet e butonave te menuse. 
        /// </summary>
        /// <param name="source"> derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {//kryen veprimet e menuse
            if (e.Item.Name == "Ruaj")
            {
                Page.Validate("entries");
                ruajSkeme();
            }

        }

        /// <summary>
        /// konfiguron vlerat fillestare
        /// </summary>
        /// <param name="idNdermarrje">id e ndermarjes</param>
        private void konfiguroVleraFillestare(int idNdermarrje, System.Resources.ResourceManager rm, System.Globalization.CultureInfo ci, int idGjuha)
        { //mbush komboboxet dhe gridat e faqes
            ASPxPageControl1.ActiveTabIndex = 0;
            ConfigureAspxComboBox.KonfiguroComboBoxFormula(cmbFormula);
            mbushListeTrupash();
            ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idperdoruesi, idNdermarrje, cmbKonfigurimi, 68, rm, ci, idGjuha);
            cmbKonfigurimi.SelectedIndex = 0;
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()), idGjuha);
            hfKonffillestar.Value = String.Format("{0};{1}", konf.KodKonfigAmbjente, konf.PershkrimKonfigAmbjente);

        }

        /// <summary>
        /// mbush griden nga sesioni
        /// </summary>
        /// <param name="idNdermarrje">id e ndermarjes</param>
        private void mbushGridNgaSession(int idNdermarrje)
        {
            DataTable tmpObject;
            bool sukses = DbCore.mySessionObjects.merrGrideNgaSessioni(Session, out tmpObject);
            if (!sukses)
                mbushGridNgaDB(idNdermarrje);
            else
            {
                gvSkema.DataSource = tmpObject;
                gvSkema.DataBind();
                tmpObject.Dispose();
            }
        }

        /// <summary>
        /// mbush griden nga databaza
        /// </summary>
        /// <param name="idNdermarrje">id e ndermarjes</param>
        private void mbushGridNgaDB(int idNdermarrje)
        {//mbush griden e popupit me te dhena            
            DataTable dt = DbCore.DbAdmin.colKokaSkemaWorkFlow.merrKokaSkemaWorkFlowNdermarrjesDT(idNdermarrje);
            DbCore.mySessionObjects.ruajGrideNeSession(Session, dt);
            gvSkema.DataSource = dt;
            gvSkema.DataBind();
            dt.Dispose();
        }

        /// <summary>
        /// ruan skemen
        /// </summary>
        private void ruajSkeme()
        {
            if (!Page.IsValid)
                return;


            DbCore.DbAdmin.clsKokaSkemaWorkFlow koka = new DbCore.DbAdmin.clsKokaSkemaWorkFlow();
            try
            {
                koka = krijoSkeme();
            }
            catch (Exception e)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(e.Message);
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, e.Message, pnlMesazhi);
                hfStatusi.Value = "false";
                return;
            }
            bool eshteShtim;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idperdoruesi, idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), komponente);
                CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim")
            {
                if (!tedrejtaInfo.DShtim)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["mesazhRaportNukKeniTeDrejta"], pnlMesazhi);
                    hfStatusi.Value = "false";
                    return;
                }
                  
                mesazh = koka.ruajSkemeWorkFlow(ci,rm);
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
                DbCore.DbRegjistrim.clsDatabaseRegjistrim dbRegjistrim = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
                DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti() { IdKonfigAmbjente = koka.IdKonfig };
                konf.mbushKonfigAmbjSipasId(konf.IdKonfigAmbjente);
                eshteShtim = false;
                koka.IdKoka = int.Parse(hfId.Value);
                bool lidhur = dbRegjistrim.eshteDokumentiILidhurCelje(koka.IdKoka.ToString(), konf.IdNivel.ToString());
                if (lidhur.ToString() != hfLidhur.Value)
                {
                    mesazh.Status = false;
                    mesazh.PershkrimMesazhi = MessagesResource.Messages["msgAktivitetiLidhur"];
                }
                else
                mesazh = koka.modifikoSkemeWorkFlow(ci);
                dbRegjistrim.Dispose();
            }
            if (mesazh)
            {
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                if (eshteShtim)
                    shtoNeGrid(idNdermarrje, koka.IdKoka);
                else //modifikim
                    modifikoNeGrid(idNdermarrje, koka.IdKoka);
                hfStatusi.Value = "true";
            }
            else
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                hfStatusi.Value = "false";
            }
            ASPxPageControl1.ActiveTabIndex = 0;
            


        }

        /// <summary>
        /// krijon aktivitetin sipas te dhenave
        /// </summary>
        /// <returns> aktitivetetin me te dhenat</returns>
        private DbCore.DbAdmin.clsKokaSkemaWorkFlow krijoSkeme()
        {
            int formula = Convert.ToInt32(cmbFormula.Value);
            bool isshtim = (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim");
            DbCore.DbShare.clsKonfigurimAmbjenti konfig = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konfig.mbushKonfigAmbjSipasKod(cmbKonfigurimi.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            int nr = 0;
            int.TryParse(txtNr.Text, out nr);
            DbCore.DbAdmin.clsKokaSkemaWorkFlow koka = new DbCore.DbAdmin.clsKokaSkemaWorkFlow(0, txtKodi.Text, txtEmertimi.Text, idperdoruesi, formula, cbNjoftim.Checked, idNdermarrje, 1, konfig.IdKonfigAmbjente, nr, ruajTrup(idNdermarrje), isshtim, cbDergo.Checked, txtEmail.Text);


            return koka;
        }

        /// <summary>
        /// cakton properti ne javascript
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvSkema_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gvSkema.PageIndex;
            e.Properties["cpPageRow"] = gvSkema.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gvSkema.VisibleRowCount;
        }

        /// <summary>
        /// kur grida ben callback
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvSkema_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string idkomponente = "";
            string kodkonfigurimi = "";

            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                kodkonfigurimi = arr[1];
                idkomponente = arr[0];
                if (arr[2] == "")
                    gvSkema.FilterExpression = "";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    //filtra.mbushFiltraGridaSipasFiltraKodi(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvSkema", komponente, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), int.Parse(cmbKonfigurimi.Value.ToString()));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        gvSkema.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvSkema);
                        hfStatusi.Value = "true";
                    }
                    else hfStatusi.Value = "false";
                }
            }
            else if (arr.Length == 2)//nese eshte zgjedhur nje konfigurim tek combo e konfigurimeve
            {
                kodkonfigurimi = arr[1];
                idkomponente = arr[0];
            }
            else
            {
                idkomponente = e.Parameters;
            }

            gvSkema.Selection.UnselectAll();
        }


        /// <summary>
        /// perdoret per te shfaqur po jo tek komboja e aktivit
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvSkema_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {

        }

        /// <summary>
        /// per filtrimin me elementin bosh
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvSkema_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if (e.Column.FieldName == "Formula")
            {
                if (Converter.ConvertToInt(e.Value) == 0)
                {
                    e.Criteria = null;
                }
            }
        }

        #region Grida e perdoruesve
        /// <summary>
        /// konfiguron griden e perdoruesive
        /// </summary>
        /// <param name="idNdermarrje">id e ndermarjes</param>
        private void konfiguroGrideTrupi(int idNdermarrje, CultureInfo ci, ResourceManager rm)
        {//konfigurohet grida
            GridUtil.percaktoVisibleColumnsMeWidth(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, gvTrupi, "gvTrupi", komponente);
            GridUtil.konfiguroGrideRegjistrimEvogelPaTheme(gvTrupi, "IdTrupi", false);
            gvTrupi.Settings.UseFixedTableLayout = false;
            // gvSkema.Columns["Fshi"].VisibleIndex = 14;
        }

        /// <summary>
        /// mbush griden me rreshta bosh
        /// </summary>
        private void mbushListeTrupash()
        {//mbushet grida me te dhena            
            DbCore.DbAdmin.colTrupiSkemaWorkFlow col = new DbCore.DbAdmin.colTrupiSkemaWorkFlow();
            for (int i = 0; i < 5; i++)
            {
                DbCore.DbAdmin.clsTrupiSkemaWorkFlow o = new DbCore.DbAdmin.clsTrupiSkemaWorkFlow();
                col.Add(o);
            }
            DbCore.mySessionObjects.ruajKaDokAprovimi(Session, false);
            gvTrupi.DataSource = col;
            gvTrupi.DataBind();
        }

        /// <summary>
        /// mbush griden  me perdoruesit e kesaj skeme
        /// </summary>
        /// <param name="id">id e skmes</param>
        private void mbushListeTrupashMod(int id)
        {//mbushet grida me te dhena
            DbCore.DbAdmin.colTrupiSkemaWorkFlow col = new DbCore.DbAdmin.colTrupiSkemaWorkFlow();
            col.mbushTrupatSkemeWorkFlowSipasKokes(id);
            col.Add(new DbCore.DbAdmin.clsTrupiSkemaWorkFlow());
            DbCore.mySessionObjects.ruajKaDokAprovimi(Session, DbCore.DbAdmin.clsKokaSkemaWorkFlow.kaDokumentaPerAprovimSkemaWorkFlow(id));
            gvTrupi.DataSource = col;
            gvTrupi.DataBind();
        }

        /// <summary>
        /// percakton templatet e kolonave te grides
        /// </summary>
        private void percaktoTemplateTrupash()
        {//percaktohen templatet per fushat e grides

            GridViewDataTextColumn col0 = gvTrupi.Columns["Fshi"] as GridViewDataTextColumn;
 // if (DbCore.mySessionObjects.ktheGjuhe(Session) == 1) col0.Caption = "Delete";
            col0.DataItemTemplate = new MyButtonTemplate("");
          
            col0.VisibleIndex = 16;
             GridViewDataTextColumn col1 = gvTrupi.Columns["Lloji"] as GridViewDataTextColumn;
            col1.DataItemTemplate = new MyComboTemplate();
            GridViewDataTextColumn col11 = gvTrupi.Columns["Perdoruesi"] as GridViewDataTextColumn;
            col11.DataItemTemplate = new MyComboTemplate();
            GridViewDataTextColumn col2 = gvTrupi.Columns["Niveli"] as GridViewDataTextColumn;
            col2.DataItemTemplate = new MyComboTemplate();
            GridViewDataTextColumn col21 = gvTrupi.Columns["Pershkrimi"] as GridViewDataTextColumn;
            col21.DataItemTemplate = new  MyTextTemplate();
            GridViewDataTextColumn col3 = gvTrupi.Columns["VleraLimit"] as GridViewDataTextColumn;
            col3.DataItemTemplate = new MyDoubleTemplate(false, 2, "0"); // "0.00"); 
            col3.PropertiesEdit.DisplayFormatString = "0.00";
            GridViewDataTextColumn col6 = gvTrupi.Columns["LlojGrupiKf"] as GridViewDataTextColumn;
            col6.DataItemTemplate = new MyComboTemplate();
            GridViewDataTextColumn col7 = gvTrupi.Columns["GrupeKF"] as GridViewDataTextColumn;
            col7.DataItemTemplate = new MyComboTemplate();
            GridViewDataTextColumn col8 = gvTrupi.Columns["NiveliApr"] as GridViewDataTextColumn;
            col8.DataItemTemplate = new MyComboTemplate();
            GridViewDataCheckColumn col4 = gvTrupi.Columns["Modifiko"] as GridViewDataCheckColumn;
            col4.DataItemTemplate = new MyCheckTemplate(false, false);

            GridViewDataTextColumn col13 = gvTrupi.Columns["Dite"] as GridViewDataTextColumn;
            col13.DataItemTemplate = new MyDoubleTemplate(false,2, "0.00");
            GridViewDataTextColumn col5 = gvTrupi.Columns["Email"] as GridViewDataTextColumn;
            col5.DataItemTemplate = new MyTextTemplate();
            GridViewDataTextColumn col12 = gvTrupi.Columns["Delegimi"] as GridViewDataTextColumn;
            col12.DataItemTemplate = new MyComboTemplate();
        }

        /// <summary>
        /// krijon koleksionin me burimet e ketij aktiviteti me te dhenat e grides
        /// </summary>
        /// <param name="idNdermarrje">id e ndermarjes</param>
        /// <returns>koleksionin me burimet</returns>
        private DbCore.DbAdmin.colTrupiSkemaWorkFlow ruajTrup(int idNdermarrje)
        {
            DbCore.DbAdmin.colTrupiSkemaWorkFlow trupi = new DbCore.DbAdmin.colTrupiSkemaWorkFlow();
            DbCore.DbAdmin.clsTrupiSkemaWorkFlow tr = new DbCore.DbAdmin.clsTrupiSkemaWorkFlow();
            JavaScriptSerializer serializusi = new JavaScriptSerializer();
            int rreshta = gvTrupi.VisibleRowCount + 1;
            object[] lloji = (object[])serializusi.DeserializeObject(hfLloji.Value);
            object[] perdoruesi = (object[])serializusi.DeserializeObject(hfPerdoruesi.Value);
            object[] niveli = (object[])serializusi.DeserializeObject(hfNiveli.Value);
            object[] vleralimit = (object[])serializusi.DeserializeObject(hfVleraLimit.Value);
            object[] palimit = (object[])serializusi.DeserializeObject(hfPaLimit.Value);
            object[] email = (object[])serializusi.DeserializeObject(hfEmail.Value);
            object[] deleguesi = (object[])serializusi.DeserializeObject(this.hfDelegimi.Value);
            object[] llojgrupi = (object[])serializusi.DeserializeObject(this.hfLlojGrupimi.Value);
            object[] grupikf = (object[])serializusi.DeserializeObject(this.hfGrupKf.Value);
            object[] niveliapr = (object[])serializusi.DeserializeObject(this.hfNiveliApr.Value);
            object[] dite = (object[])serializusi.DeserializeObject(this.hfDite.Value);
            object[] pershkrimi = (object[])serializusi.DeserializeObject(this.hfPershkrimi.Value);
            for (int i = 0; i < rreshta - 1; i++)
            {
                tr = new DbCore.DbAdmin.clsTrupiSkemaWorkFlow();
                if (lloji[i] != null && lloji[i].ToString() != "")
                    tr.Lloji = int.Parse(lloji[i].ToString());

                if (perdoruesi[i] != null && perdoruesi[i].ToString() != "")
                {
                    tr.Perdoruesi = perdoruesi[i].ToString();
                    if (lloji[i].ToString() == "1")
                    {
                        colPerdoruesit col = clsPerdorues.merrUserNgaLogin(perdoruesi[i].ToString());
                        if (col.Count > 0)
                            tr.IdPerdRol = col[0].IdPerdorues;

                    }
                    else
                    {
                        tr.IdPerdRol = clsRoli.ktheIdRoli(perdoruesi[i].ToString(), idlicenca);
                    }


                }
                if (dite[i] != null && dite[i] != "null" && dite[i] != "")
                    tr.Dite = Convert.ToDecimal(dite[i]);
                if (niveli[i] != null && niveli[i].ToString() != "")
                    tr.Niveli = int.Parse(niveli[i].ToString());

                if (vleralimit[i] != null && vleralimit[i].ToString() != "")
                    tr.VleraLimit = Convert.ToDouble(vleralimit[i]);
                if (palimit[i] != null && palimit[i].ToString() != "")
                    tr.Modifiko = Convert.ToBoolean(palimit[i]);
                if (email[i] != null && email[i].ToString() != "")
                    tr.Email = email[i].ToString();
                if (deleguesi[i] != null && deleguesi[i].ToString() != "")
                {
                    tr.Delegimi = deleguesi[i].ToString();


                    colPerdoruesit col = clsPerdorues.merrUserNgaLogin(deleguesi[i].ToString());
                    if (col.Count > 0)
                        tr.IdDelegimi = col[0].IdPerdorues;
                }
                if (llojgrupi[i] != null && llojgrupi[i].ToString() != "")
                    tr.LlojGrupiKf = int.Parse(llojgrupi[i].ToString());
                if (grupikf[i] != null && grupikf[i].ToString() != "")
                {
                    tr.GrupeKF = grupikf[i].ToString();
                    string[] arrgrup = tr.GrupeKF.Split(',');
                    for (int j = 0; j < arrgrup.Length; j++)
                    {
                        DbCore.DbKontabiliteti.clsGrupeKF g = new DbCore.DbKontabiliteti.clsGrupeKF(arrgrup[j], idNdermarrje, tr.LlojGrupiKf, 0);
                        if (g.IdGrupi > 0)
                            tr.ColGrupeKF.Add(g);
                    }

                }
                if (niveliapr[i] != null && niveliapr[i].ToString() != "")
                    tr.NiveliApr = int.Parse(niveliapr[i].ToString());

                if(pershkrimi[i] != null && pershkrimi[i].ToString() != "")
                {
                    tr.Pershkrimi = pershkrimi[i].ToString();
                }

                if (tr.IdPerdRol != -1 && tr.IdPerdRol != 0)
                {

                    trupi.Add(tr);
                }

            }

            return trupi;
        }

        /// <summary>
        /// perdoret ne rastet kur grida ben callback per ta rimbushur griden
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void gvTrupi_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            gvTrupi.DataBind();
        }

        /// <summary>
        /// kur grida ben callback
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvTrupi_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {//kur grida ben callback te ruajme te dhenat
            int key = -1;
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("gvTrupi"))
                {
                    if (e.Parameters.Split(';').Length == 2)
                    {
                        if (e.Parameters.Split(';')[1] == "modifiko")
                        {

                            mbushListeTrupashMod(int.Parse(e.Parameters.Split(';')[0]));
                        }
                        else
                        {
                            mbushListeTrupash();
                        }

                    }
                    else
                    {
                        if (e.Parameters.ToString() != "")
                        {
                            key = int.Parse(e.Parameters.ToString());
                        }
                        DbCore.DbAdmin.colTrupiSkemaWorkFlow trupi = new DbCore.DbAdmin.colTrupiSkemaWorkFlow();
                        DbCore.DbAdmin.clsTrupiSkemaWorkFlow tr = new DbCore.DbAdmin.clsTrupiSkemaWorkFlow();
                        JavaScriptSerializer serializusi = new JavaScriptSerializer();
                        int rreshta = gvTrupi.VisibleRowCount + 1;
                        object[] lloji = (object[])serializusi.DeserializeObject(hfLloji.Value);
                        object[] perdoruesi = (object[])serializusi.DeserializeObject(hfPerdoruesi.Value);
                        object[] niveli = (object[])serializusi.DeserializeObject(hfNiveli.Value);
                        object[] vleralimit = (object[])serializusi.DeserializeObject(hfVleraLimit.Value);
                        object[] palimit = (object[])serializusi.DeserializeObject(hfPaLimit.Value);
                        object[] email = (object[])serializusi.DeserializeObject(hfEmail.Value);
                        object[] deleguesi = (object[])serializusi.DeserializeObject(this.hfDelegimi.Value);
                        object[] llojgrupi = (object[])serializusi.DeserializeObject(this.hfLlojGrupimi.Value);
                        object[] grupikf = (object[])serializusi.DeserializeObject(this.hfGrupKf.Value);
                        object[] niveliapr = (object[])serializusi.DeserializeObject(this.hfNiveliApr.Value);
                        object[] dite = (object[])serializusi.DeserializeObject(this.hfDite.Value);
                        object[] pershkrimi = (object[])serializusi.DeserializeObject(this.hfPershkrimi.Value);
                        for (int i = 0; i < rreshta - 1; i++)
                        {
                            tr = new DbCore.DbAdmin.clsTrupiSkemaWorkFlow();
                            if (lloji[i] != null && lloji[i].ToString() != "")
                                tr.Lloji = int.Parse(lloji[i].ToString());

                            if (perdoruesi[i] != null && perdoruesi[i].ToString() != "")
                            {
                                tr.Perdoruesi = perdoruesi[i].ToString();
                                if (lloji[i].ToString() == "1")
                                {

                                    colPerdoruesit col = clsPerdorues.merrUserNgaLogin(perdoruesi[i].ToString());
                                    if (col.Count > 0)
                                        tr.IdPerdRol = col[0].IdPerdorues;

                                }
                                else
                                {
                                    tr.IdPerdRol = clsRoli.ktheIdRoli(perdoruesi[i].ToString(), idlicenca);
                                }


                            }
                            if (dite[i] != null && dite[i] != "null" && dite[i] != "")
                                tr.Dite = Convert.ToDecimal(dite[i]);
                            if (niveli[i] != null && niveli[i].ToString() != "")
                                tr.Niveli = int.Parse(niveli[i].ToString());
                            if (niveliapr[i] != null && niveliapr[i].ToString() != "")
                                tr.NiveliApr = int.Parse(niveliapr[i].ToString());
                            if (llojgrupi[i] != null && llojgrupi[i].ToString() != "")
                                tr.LlojGrupiKf = int.Parse(llojgrupi[i].ToString());
                            if (vleralimit[i] != null && vleralimit[i].ToString() != "")
                                tr.VleraLimit = Convert.ToDouble(vleralimit[i]);
                            if (palimit[i] != null && palimit[i].ToString() != "")
                                tr.Modifiko = Convert.ToBoolean(palimit[i]);
                            if (email[i] != null && email[i].ToString() != "")
                                tr.Email = email[i].ToString();
                            if (grupikf[i] != null && grupikf[i].ToString() != "")
                                tr.GrupeKF = grupikf[i].ToString();
                            if (deleguesi[i] != null && deleguesi[i].ToString() != "")
                            {
                                tr.Delegimi = deleguesi[i].ToString();


                                colPerdoruesit col = clsPerdorues.merrUserNgaLogin(deleguesi[i].ToString());
                                if (col.Count > 0)
                                    tr.IdDelegimi = col[0].IdPerdorues;
                            }
                            if (pershkrimi[i] != null && pershkrimi[i].ToString() != "")
                            {
                                tr.Pershkrimi = pershkrimi[i].ToString();
                            }

                            trupi.Add(tr);

                        }
                        if (key != -1)
                            trupi.RemoveAt(key);
                        else
                        {
                            trupi.Add(new DbCore.DbAdmin.clsTrupiSkemaWorkFlow());
                        }
                        if (trupi.Count == 0)
                        {
                            trupi.Add(new DbCore.DbAdmin.clsTrupiSkemaWorkFlow());
                        }
                        this.gvTrupi.DataSource = trupi;
                        this.gvTrupi.DataBind();
                        percaktoTemplateTrupash();

                    }
                }
            }
        }

        /// <summary>
        /// cakton properti ne javascript
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvTrupi_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpNoRows"] = gvTrupi.VisibleRowCount;
        }

        /// <summary>
        /// perdoret per te shtuar kolonen e selektimit tek grida dhe per te vendosur disa karakteristika te grides
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void gvTrupi_DataBound(object sender, EventArgs e)
        {//shton butonin fshi
            string emri = MessagesResource.Messages["lblFshiBtn"];
            if (this.gvTrupi.Columns["Fshi"] == null)
            {
                GridViewDataTextColumn fshi = new GridViewDataTextColumn();
                fshi.Name = "Fshi";
                fshi.Caption = emri;
                fshi.Width = 30;
                gvTrupi.Columns.Add(fshi);

                gvTrupi.KeyFieldName = "IdTrupi";
                gvTrupi.SettingsBehavior.AllowSelectByRowClick = false;
                gvTrupi.SettingsBehavior.AllowFocusedRow = true;
            }
        }

        /// <summary>
        /// kur krijohen rreshtat e grides
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvTrupi_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {//krijon rreshat sipas modelit
            if (e.RowType == DevExpress.Web.GridViewRowType.Data)
            {
                bool kadokapr = DbCore.mySessionObjects.merrKaDokAprovimi(Session);
                GridViewDataTextColumn col0 = ((ASPxGridView)sender).Columns["Fshi"] as GridViewDataTextColumn;
                GridViewDataTextColumn col1 = ((ASPxGridView)sender).Columns["Lloji"] as GridViewDataTextColumn;
                GridViewDataTextColumn col2 = ((ASPxGridView)sender).Columns["Perdoruesi"] as GridViewDataTextColumn;
                GridViewDataTextColumn col3 = ((ASPxGridView)sender).Columns["Niveli"] as GridViewDataTextColumn;
                GridViewDataTextColumn col20 = ((ASPxGridView)sender).Columns["Pershkrimi"] as GridViewDataTextColumn;
                GridViewDataTextColumn col4 = ((ASPxGridView)sender).Columns["VleraLimit"] as GridViewDataTextColumn;
                GridViewDataCheckColumn col5 = ((ASPxGridView)sender).Columns["Modifiko"] as GridViewDataCheckColumn;
                GridViewDataTextColumn col6 = ((ASPxGridView)sender).Columns["Email"] as GridViewDataTextColumn;
                GridViewDataTextColumn col7 = ((ASPxGridView)sender).Columns["Delegimi"] as GridViewDataTextColumn;
                GridViewDataTextColumn col16 = gvTrupi.Columns["LlojGrupiKf"] as GridViewDataTextColumn;
                GridViewDataTextColumn col17 = gvTrupi.Columns["GrupeKF"] as GridViewDataTextColumn;
                GridViewDataTextColumn col18 = gvTrupi.Columns["NiveliApr"] as GridViewDataTextColumn;
                GridViewDataTextColumn col19 = gvTrupi.Columns["Dite"] as GridViewDataTextColumn;
                ASPxButton btn0 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col0, "btn") as ASPxButton;
                ASPxComboBox cmb1 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col1, "cmbBox") as ASPxComboBox;
                ASPxComboBox cmb2 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col2, "cmbBox") as ASPxComboBox;
                ASPxComboBox cmb3 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col3, "cmbBox") as ASPxComboBox;
                ASPxTextBox txt31 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col4, "txtBox") as ASPxTextBox;
                ASPxTextBox txt20 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col20, "txtBox") as ASPxTextBox;
                ASPxTextBox txt2 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col4, "txtBox") as ASPxTextBox;
                ASPxCheckBox txt3 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col5, "cb") as ASPxCheckBox;
                ASPxTextBox txt4 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col6, "txtBox") as ASPxTextBox;
                ASPxComboBox txt5 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col7, "cmbBox") as ASPxComboBox;
                ASPxComboBox cmb11 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col16, "cmbBox") as ASPxComboBox;
                ASPxComboBox cmb12 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col17, "cmbBox") as ASPxComboBox;
                ASPxComboBox cmb13 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col18, "cmbBox") as ASPxComboBox;
                ASPxTextBox txt19 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col19, "txtBox") as ASPxTextBox;

                //vendosen client side eventet e kolonave
                if (btn0 != null)
                {
                    btn0.ClientInstanceName = String.Format("btnFshi{0}", e.VisibleIndex);
                    btn0.ClientSideEvents.Click = String.Format("function(s,e){{FshiClicked({0});}}", e.VisibleIndex);
                    if (kadokapr)
                        btn0.ClientEnabled = false;
                }
                if (cmb1 != null)
                {
                    cmb1.Items.Add("Perdorues", 1);
                    cmb1.Items.Add("Rol", 2);
                    if (cmb1.Text == "")
                        cmb1.SelectedIndex = 0;
                    cmb1.DataBind();
                    cmb1.ClientInstanceName = String.Format("Lloji{0}", e.VisibleIndex);
                    cmb1.ClientSideEvents.TextChanged = String.Format("function(s,e){{TextChangedLloji(Lloji{0}, Perdoruesi{0},{0});}}", e.VisibleIndex);
                    if (kadokapr)
                        cmb1.ClientEnabled = false;
                }
                if (cmb11 != null)
                {
                    cmb11.Items.Add("Grupimi 1", 1);
                    cmb11.Items.Add("Grupimi 2", 2);
                    cmb11.Items.Add("Grupimi 3", 3);
                    if (cmb11.Text == "")
                        cmb11.SelectedIndex = 0;
                    cmb11.DataBind();
                    cmb11.ClientInstanceName = String.Format("LlojiGrupi{0}", e.VisibleIndex);
                    cmb11.ClientSideEvents.SelectedIndexChanged = String.Format("function(s,e){{TextChangedLlojiGrupi(LlojiGrupi{0}, Grupi{0},{0});}}", e.VisibleIndex);

                    if (kadokapr)
                        cmb11.ClientEnabled = false;
                }
                if (cmb3 != null)
                {
                    ConfigureAspxComboBox.KonfiguroComboBoxPrioriteti(cmb3);
                    if (cmb3.Text == "")
                        cmb3.SelectedIndex = 0;
                    cmb3.ClientInstanceName = String.Format("Niveli{0}", e.VisibleIndex);
                    cmb3.ClientSideEvents.SelectedIndexChanged = String.Format("function(s,e){{TextChangedNiveli(Niveli{0},{0});}}", e.VisibleIndex);
                    if (kadokapr)
                        cmb3.ClientEnabled = false;

                }
                if (cmb13 != null)
                {
                    ConfigureAspxComboBox.mbushComboPrioritetiMeFillim(cmb13, int.Parse(cmb3.Value.ToString()) + 1);
                    if (cmb13.Text == "")
                        cmb13.SelectedIndex = 0;
                    cmb13.ClientInstanceName = String.Format("NiveliApr{0}", e.VisibleIndex);
                    if (kadokapr || (cmb12.Text == "" && txt2.Text == "0.00"))
                        cmb13.ClientEnabled = false;

                }
                if (cmb12 != null)
                {
                    cmb12.DropDownButton.Visible = false;
                    cmb12.Buttons.Add();
                    cmb12.AutoPostBack = false;
                    cmb12.EnableCallbackMode = false;

                    cmb12.ClientInstanceName = String.Format("Grupi{0}", e.VisibleIndex);
                    cmb12.ClientSideEvents.ButtonClick = String.Format("function(s,e){{ButtonClickedGrupi(Grupi{0},{0}); }}", e.VisibleIndex);
                    cmb12.ClientSideEvents.KeyUp = String.Format("function(s,e){{TextChangedGrupi(Grupi{0},{0}); }}", e.VisibleIndex);
                    if (kadokapr)
                        cmb12.ClientEnabled = false;
                }
                if (cmb2 != null)
                {
                    cmb2.DropDownButton.Visible = false;
                    cmb2.Buttons.Add();
                    cmb2.AutoPostBack = false;
                    cmb2.EnableCallbackMode = false;

                    cmb2.TextFormatString = "{0}";
                    cmb2.Columns.Add(new ListBoxColumn("Kodi"));
                    cmb2.Columns.Add(new ListBoxColumn("Emertimi"));

                    cmb2.ClientInstanceName = String.Format("Perdoruesi{0}", e.VisibleIndex);

                    cmb2.DropDownStyle = DropDownStyle.DropDown;
                    cmb2.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                    cmb2.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedPerdoruesi(Perdoruesi{0},{0});}}", e.VisibleIndex);
                    cmb2.ClientSideEvents.KeyUp = String.Format("function(s,e){{var code = _getKeyCode(e.htmlEvent); KeyPressPerdoruesi(code,Perdoruesi{0},{0}); }}", e.VisibleIndex);
                    cmb2.ClientSideEvents.ButtonClick = String.Format("function(s,e){{ButtonClickedPerdoruesi(Perdoruesi{0},{0}); }}", e.VisibleIndex);
                    if (kadokapr)
                        cmb2.ClientEnabled = false;
                }
                if (txt5 != null)
                {
                    txt5.DropDownButton.Visible = false;
                    txt5.Buttons.Add();
                    txt5.AutoPostBack = false;
                    //txt5.EnableCallbackMode = false;
                    txt5.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                    txt5.TextFormatString = "{0}";
                    txt5.Columns.Add(new ListBoxColumn("PerdoruesUsername"));
                    txt5.Columns.Add(new ListBoxColumn("EmriPerdorues"));
                    txt5.Columns.Add(new ListBoxColumn("MbiemriPerdorues"));

                    DataTable dt = DbCore.DbAdmin.colPerdoruesit.merrPerdoruesitSipasLicencesDTJoSuper(idperdoruesi, idlicenca);
                    txt5.DataSource = dt;
                    txt5.DataBind();
                    txt5.ClientInstanceName = String.Format("Deleguesi{0}", e.VisibleIndex);
                    txt5.DropDownStyle = DropDownStyle.DropDown;
                    txt5.ClientSideEvents.TextChanged = String.Format("function(s,e){{TextChangedDeleguesi(Deleguesi{0},{0});}}", e.VisibleIndex);
                    txt5.ClientSideEvents.KeyPress = String.Format("function(s,e){{var code = _getKeyCode(e.htmlEvent); KeyPressDeleguesi(code,Deleguesi{0},{0}); }}", e.VisibleIndex);
                    txt5.ClientSideEvents.ButtonClick = String.Format("function(s,e){{ButtonClickedDeleguesi(Deleguesi{0},{0}); }}", e.VisibleIndex);
                    dt.Dispose();
                    if (e.VisibleIndex == ((ASPxGridView)sender).VisibleRowCount - 2)
                    {

                    }
                }
                if (txt2 != null)
                {
                    txt2.ClientInstanceName = String.Format("VleraLimit{0}", e.VisibleIndex);
                    txt2.ClientSideEvents.TextChanged = "function (s,e){TextChangedVleraLimit(VleraLimit" + e.VisibleIndex + "," + e.VisibleIndex + ")}";
                    if (kadokapr)
                        txt2.ClientEnabled = false;
                }
                if (txt4 != null)
                {
                    if (cmb1.Value.ToString() == "2")
                        txt4.ClientEnabled = false;
                    txt4.ClientInstanceName = String.Format("Email{0}", e.VisibleIndex);
                    txt4.ClientSideEvents.TextChanged = "function (s,e){TextChangedEmail(Email" + e.VisibleIndex + "," + e.VisibleIndex + ")}";
                }
                if (txt19 != null)
                {

                    txt19.ClientInstanceName = String.Format("Dite{0}", e.VisibleIndex);
                    txt19.ClientSideEvents.TextChanged = "function (s,e){TextChangedDite(Dite" + e.VisibleIndex + "," + e.VisibleIndex + ")}";
                }
                if (txt3 != null)
                {
                    txt3.ClientInstanceName = String.Format("Modifiko{0}", e.VisibleIndex);

                }
                if (txt20 != null)
                {
                    txt20.ClientInstanceName = String.Format("Pershkrimi{0}", e.VisibleIndex);
                    txt20.ClientSideEvents.TextChanged = "function (s,e){TextChangedPershkrimi(Pershkrimi" + e.VisibleIndex + "," + e.VisibleIndex + ")}";
                }
            }

        }

        #endregion
    }
}