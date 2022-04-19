using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using DevExpress.Web;
using System.Data;
using PlatinumWeb.Templates;
using System.Web.Script.Serialization;
using System.Globalization;
using System.Resources;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    /// <summary>
    /// Faqja e shtimit te skemave
    /// </summary>
    public partial class Shto_SkemaQendraKosto : MyPageBase
    {
        private int idperdoruesi, idnderviti, idNdermarrje, idviti, idgjuha;
        private const int idstatusdok = 1;
        /// <summary>
        /// perdoret per te vendosur themen e devexpresit faqes
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>

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
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (!Page.IsPostBack)
            {
                EmrateTabeve(rm, ci);
                vendosHfMePerkthime(rm, ci);
                hfState.Set("idGjuha", idgjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                ASPxPageControl1.ActiveTabIndex = 0;
                percaktoTemplateMenu(idgjuha, idviti, idperdoruesi, idNdermarrje, ASPxMenu1);
                //mbushComboBoxFiltra(idgjuha, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
                konfiguroVleraFillestare(idNdermarrje, rm, ci, idgjuha);
                mbushGridNgaDB(idNdermarrje);
                konfiguroGrideTrupi(idNdermarrje);
                konfiguroGride(idNdermarrje, cmbKonfigurimi.Text.Split(';')[0], 904);
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "gvSkema", gvSkema, cmbKonfigurimi.Text.Split(';')[0], 904.ToString(), DbCore.mySessionObjects.ktheGjuhe(Session));
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idperdoruesi, idNdermarrje, idviti, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idperdoruesi, idNdermarrje, idviti, "Shto_SkemaQendraKosto.aspx");
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
            }
            else
            {
                mbushGridNgaSession(idNdermarrje);
                konfiguroGride(idNdermarrje, cmbKonfigurimi.Text.Split(';')[0], 904);
            }
            AspxWebControlUtils.perkthePopUp(popFshi, rm.GetString("labelKujdes", ci), lblMsgbox, rm.GetString("labelAdministrimiMsgJeniSigurt", ci), ButtonCancel, rm.GetString("labelAnullo", ci));
            konfigurimi_Label.Text = rm.GetString("lblModeli", ci);
            popupUniversal.HeaderText = rm.GetString("popupAdministrimiUniversal", ci);
            GridUtil.konfigGrideListeEMadhePaTheme(gvSkema, "IdKoka");
            percaktoTemplateMenu(idgjuha, idviti, idperdoruesi, idNdermarrje, ASPxMenu1);
            percaktoTemplateTrupash();
            GridUtil.EmrateButonaveMbiGride(gvSkema);
            mbushComboBoxFiltra(idgjuha, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
        }

        private void vendosHfMePerkthime(ResourceManager rm, CultureInfo ci)
        {
            hfState.Set("msgSkemaQKPerqindjaDuhetNumerMidis0Dhe100", rm.GetString("msgSkemaQKPerqindjaDuhetNumerMidis0Dhe100", ci));
            hfState.Set("msgPerqindjaDuhetJeteNumer", rm.GetString("msgPerqindjaDuhetJeteNumer", ci));
            hfState.Set("msgSkemaQKKjoQKEshtePerdorur1HereNeKeteSkeme", rm.GetString("msgSkemaQKKjoQKEshtePerdorur1HereNeKeteSkeme", ci));
            hfState.Set("msgSkemaQKZgjidhQK", rm.GetString("msgSkemaQKZgjidhQK", ci));
            hfState.Set("msgSkemaQKZgjidhniNjeSkeme", rm.GetString("msgSkemaQKZgjidhniNjeSkeme", ci));
        }

        /// <summary>
        /// Vendos emrat e tabeve ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateTabeve(System.Resources.ResourceManager rm, System.Globalization.CultureInfo ci)
        {
            ASPxPageControl1.TabPages[0].Text = rm.GetString("TePergjithshmeTab", ci);
            ASPxPageControl1.TabPages[1].Text = rm.GetString("skemaTab", ci);
        }

        /// <summary>
        /// mbush combon e filtrave
        /// </summary>
        /// <param name="idNdermarrje">id e ndermarjes</param>
        private static void mbushComboBoxFiltra(int idGjuha, int idNdermarrje, int idkonfig)
        {
            clsToolbarConfig.mbushComboBoxFiltraMeValFieldTextField(idGjuha, idNdermarrje, "gvSkema", "Shto_SkemaQendraKosto.aspx", "IdFiltra", "FiltraShenime", idkonfig);
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
            clsToolbarConfig.percaktoTemplateMenu(idGjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1, "Shto_SkemaQendraKosto.aspx", this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, hfShtimModifikim.Value == "modifikim" ? false : true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
        }

        protected void RuajKolona_Click(object sender, EventArgs e)
        {
            int idfiltri = 0;
            int idndermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idperdorues = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            int idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            int idgjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            int idkonf = DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(cmbKonfigurimi.Text.Split(';')[0], idndermarrje);
            DbCore.clsMesazh mesazh = GridUtil.ruajFiltra(idndermarrje, idperdorues, idgjuha, "gvSkema", "Shto_SkemaQendraKosto.aspx", "FilterDefault", gvSkema.FilterExpression, gvSkema, "Kodi", idkonf, out idfiltri);//ruan filtrin e zgjedhur tek filtrat
            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                return;

            }
            mesazh = GridUtil.ruajkonfigurimgride(gvSkema, cmbKonfigurimi.Text, idndermarrje, idperdorues, 904, idfiltri, idViti, DbCore.mySessionObjects.ktheCultureInfo(Session), DbCore.mySessionObjects.ktheGjuhe(Session));/// ruan konfigurimin e grides dhe filtrin

            clsToolbarConfig.mbushComboBoxFiltra(idgjuha, idndermarrje, "gvSkema", int.Parse(cmbKonfigurimi.Value.ToString()), "Shto_SkemaQendraKosto.aspx");

            percaktoTemplateMenu(idgjuha, idViti, idperdorues, idndermarrje, ASPxMenu1);
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
        private void konfiguroGride(int idNdermarrje, string kodKonfigurimi, int idKomponente)
        {
            
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
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            GridUtil.EmrateButonaveMbiGride(gvSkema);
        }

        /// <summary>
        /// perdoret per te vendosur filtrat tek headeri i grides
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvSkema_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            string TeGjithe = rm.GetString("GridHeaderFilterFillItemTeGjithe", ci);
            string nga = rm.GetString("GridHeaderFilterFillItemNga", ci);
            if (e.Column.FieldName == "Pershkrimi" || e.Column.FieldName == "Kodi")
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
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvSkema", "Shto_SkemaQendraKosto.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), int.Parse(cmbKonfigurimi.Value.ToString()));
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
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvSkema", "Shto_SkemaQendraKosto.aspx", idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
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
            filtri.IdPerdoruesi = oPerdorues.IdPerdorues;
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
            if (ASPxPageControl1.ActiveTabIndex == 0)//nqs kemi zgjedhur vetem nje kartele per te pare fshijme vetem ate
                rreshtat = gvSkema.GetSelectedFieldValues("IdKoka");
            else
            {
                rreshtat = new List<object>();
                rreshtat.Add(hfId.Value);
            }
            // List<object> rreshtat = gvSkema.GetSelectedFieldValues("IdKoka");
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (rreshtat.Count == 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgSkemaQKZgjidhniTePAkten1Skeme", ci), pnlMesazhi);
                return;
            }
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            //clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "", pnlMesazhi);
            List<string> TeFshire = new List<string>(), TePaFshire = new List<string>();
            DbCore.DbRegjistrim.clsDatabaseRegjistrim dbRegjistrim = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();

            foreach (object id in rreshtat)
            {
                DbCore.DbQendraKosto.clsKokaSkemaQK skema = new DbCore.DbQendraKosto.clsKokaSkemaQK(Convert.ToInt32(id));
                DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti() { IdKonfigAmbjente = skema.IdKonfig };
                konf.mbushKonfigAmbjSipasId(konf.IdKonfigAmbjente);
                bool lidhur = dbRegjistrim.eshteDokumentiILidhurCelje(skema.IdKoka.ToString(), konf.IdNivel.ToString());
                if (lidhur)
                {
                    TePaFshire.Add(skema.Kodi);
                    continue;
                }

                skema.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                //mesazh = aktivitet.fshi(null);
                mesazh = skema.fshi();
                if (skema.IdKoka == 0)
                    continue;
                if (mesazh.Status)
                {
                    #region Heq llogarite nga grida
                    hiqNgaGrida(skema.IdKoka, skema.IdNdermarje, rm, ci);
                    #endregion
                    TeFshire.Add(skema.Kodi);

                    ASPxPageControl1.ActiveTabIndex = 0;
                    hfStatusi.Value = "true";
                }
            }
            dbRegjistrim.Dispose();
            string mesazhInfoGabim = "", mesazhInfoSukses = "";
            if (TePaFshire.Count == 1)
                mesazhInfoGabim = String.Format("{0}{1}{2}", rm.GetString("msgSkemaQKPrefixNjejes", ci), String.Join(";", TePaFshire), rm.GetString("msgCeljeMagazinatSuffixNjejesGabimi", ci));
            else
                if (TePaFshire.Count > 1)
                    mesazhInfoGabim = String.Format("{0}{1}{2}", rm.GetString("msgSkemaQKPrefixShumes", ci), String.Join(";", TePaFshire), rm.GetString("msgCeljeMagazinatSuffixShumesGabimi", ci));
            if (TeFshire.Count == 1)
                mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("msgSkemaQKPrefixNjejes", ci), String.Join(";", TeFshire), rm.GetString("msgClejeArkaBankaSuffixNjejesSuksesi", ci));
            else
                if (TeFshire.Count > 1)
                    mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("msgSkemaQKPrefixShumes", ci), String.Join(";", TeFshire), rm.GetString("msgClejeArkaBankaSuffixShumesSuksesi", ci));
            if (mesazhInfoGabim != "" && mesazhInfoSukses != "")
                mesazhInfoGabim += rm.GetString("msgLidhesMesazhi", ci) + mesazhInfoSukses;
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
        private void hiqNgaGrida(int idkoka, int idndermarje, ResourceManager rm, CultureInfo ci)
        {
            if (gvSkema.DataSource != null)
            {
                DataTable dt = (DataTable)gvSkema.DataSource;
                DataRow[] drs = dt.Select(String.Format("IdKoka = '{0}'", idkoka));
                if (drs.Length > 1)
                    throw new DbCore.MyException(rm.GetString("msgSkemaQKNdodhen2SkemaMeTeNjejtenIDNeGride", ci));
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
        private void shtoNeGrid(int idNdermarrje, int idkoka, ResourceManager rm, CultureInfo ci)
        {
            if (gvSkema.DataSource != null)
            {
                DataTable dt = (DataTable)gvSkema.DataSource;
                DataRow[] drs = dt.Select("IdKoka = " + idkoka);
                if (drs.Length > 0)
                    throw new DbCore.MyException(rm.GetString("msgSkemaQKNdodhen2SkemaMeTeNjejtenIDNeGride", ci));
                DataRow newArtDr = DbCore.DbQendraKosto.colKokaSkemaQK.merrSkemenSipasIdDR(idkoka);
                dt.ImportRow(newArtDr);
            }
            else mbushGridNgaDB(idNdermarrje);
        }

        /// <summary>
        /// modifikon ne gride aktivitetin e modifikuar
        /// </summary>
        /// <param name="idNdermarrje">idndermarje</param>
        ///<param name="idkoka">id koka</param>
        private void modifikoNeGrid(int idNdermarrje, int idkoka, ResourceManager rm, CultureInfo ci)
        {
            if (gvSkema.DataSource != null)
            {
                DataTable dt = (DataTable)gvSkema.DataSource;
                DataRow[] drs = dt.Select("IdKoka = " + idkoka);
                if (drs.Length > 1)
                    throw new DbCore.MyException(rm.GetString("msgSkemaQKNdodhen2SkemaMeTeNjejtenIDNeGride", ci));
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                DataRow newArtDr = DbCore.DbQendraKosto.colKokaSkemaQK.merrSkemenSipasIdDR(idkoka);
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
            mbushListeTrupash();
            ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idperdoruesi, idNdermarrje, cmbKonfigurimi, 73, rm, ci, idGjuha);
            cmbKonfigurimi.SelectedIndex = 0;
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()), idgjuha);
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
            DataTable dt = DbCore.DbQendraKosto.colKokaSkemaQK.merrSkemenNdermarjeDT(idNdermarrje);
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
            DbCore.DbQendraKosto.clsKokaSkemaQK skema = new DbCore.DbQendraKosto.clsKokaSkemaQK();
            if (Page.IsValid == false)
                return;
            else
            {
                try
                {
                    skema = krijoSkeme();
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
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idperdoruesi, idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "Shto_SkemaQendraKosto.aspx");
                CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
                ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim")
                {
                    if (!tedrejtaInfo.DShtim)
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNukKeniTeDrejta", ci), pnlMesazhi);
                        hfStatusi.Value = "false";
                        return;
                    }
                    mesazh = skema.ruaj();
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
                    DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti() { IdKonfigAmbjente = skema.IdKonfig };
                    konf.mbushKonfigAmbjSipasId(konf.IdKonfigAmbjente);
                    eshteShtim = false;
                    skema.IdKoka = int.Parse(hfId.Value);
                    bool lidhur = dbRegjistrim.eshteDokumentiILidhurCelje(skema.IdKoka.ToString(), konf.IdNivel.ToString());
                    if (lidhur.ToString() != hfLidhur.Value)
                    {
                        mesazh.Status = false;
                        mesazh.PershkrimMesazhi = rm.GetString("msgSkemaQKSkemaEshteELidhur", ci);
                    }
                    else
                        mesazh = skema.modifiko();
                    dbRegjistrim.Dispose();
                }
                if (mesazh.Status == true)
                {
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                    if (eshteShtim)
                        shtoNeGrid(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), skema.IdKoka, rm, ci);
                    else //modifikim
                        modifikoNeGrid(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), skema.IdKoka, rm, ci);
                    hfStatusi.Value = "true";
                }
                else
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                    hfStatusi.Value = "false";
                }
                ASPxPageControl1.ActiveTabIndex = 0;
            }


        }

        /// <summary>
        /// krijon skemen sipas te dhenave
        /// </summary>
        /// <returns> skemen me te dhenat</returns>
        private DbCore.DbQendraKosto.clsKokaSkemaQK krijoSkeme()
        {

            bool isshtim;
            if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim")
                isshtim = true;
            else
                isshtim = false;
            DbCore.DbShare.clsKonfigurimAmbjenti konfig = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konfig.mbushKonfigAmbjSipasKod(cmbKonfigurimi.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));

            DbCore.DbQendraKosto.clsKokaSkemaQK skema = new DbCore.DbQendraKosto.clsKokaSkemaQK(0, txtKodi.Text, txtPershkrimi.Text, idperdoruesi, idNdermarrje, konfig.IdKonfigAmbjente, idstatusdok, ruajTrup(idNdermarrje), isshtim);

            return skema;
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
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvSkema", "Shto_SkemaQendraKosto.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), int.Parse(cmbKonfigurimi.Value.ToString()));
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

        }

        #region Grida e qendrave te kostos
        /// <summary>
        /// konfiguron griden e qendrave te kostos
        /// </summary>
        /// <param name="idNdermarrje">id e ndermarjes</param>
        private void konfiguroGrideTrupi(int idNdermarrje)
        {//konfigurohet grida
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            GridUtil.percaktoVisibleColumnsMeWidth(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, gvTrupi, "gvTrupi", "Shto_SkemaQendraKosto.aspx");
            GridUtil.konfiguroGrideRegjistrimEvogelPaTheme(gvTrupi, "IdTrupi", false);
            gvTrupi.Settings.UseFixedTableLayout = false;

        }

        /// <summary>
        /// mbush griden me rreshta bosh
        /// </summary>
        private void mbushListeTrupash()
        {//mbushet grida me te dhena            
            DbCore.DbQendraKosto.colTrupiSkemaQK col = new DbCore.DbQendraKosto.colTrupiSkemaQK();
            for (int i = 0; i < 5; i++)
            {
                DbCore.DbQendraKosto.clsTrupiSkemaQK o = new DbCore.DbQendraKosto.clsTrupiSkemaQK();
                col.Add(o);
            }
            gvTrupi.DataSource = col;
            gvTrupi.DataBind();
        }

        /// <summary>
        /// mbush griden e burimeve me qendrat e kesaj skeme
        /// </summary>
        /// <param name="id">id e skemes</param>
        private void mbushListeTrupashMod(int id)
        {//mbushet grida me te dhena
            DbCore.DbQendraKosto.colTrupiSkemaQK col = new DbCore.DbQendraKosto.colTrupiSkemaQK(id);
            col.Add(new DbCore.DbQendraKosto.clsTrupiSkemaQK());
            gvTrupi.DataSource = col;
            gvTrupi.DataBind();
        }

        /// <summary>
        /// percakton templatet e kolonave te grides
        /// </summary>
        private void percaktoTemplateTrupash()
        {//percaktohen templatet per fushat e grides
            GridViewDataTextColumn col0 = gvTrupi.Columns["Fshi"] as GridViewDataTextColumn;
            col0.DataItemTemplate = new MyButtonTemplate("");
            col0.VisibleIndex = 14;
            GridViewDataTextColumn col1 = gvTrupi.Columns["Kodi"] as GridViewDataTextColumn;
            col1.DataItemTemplate = new MyComboTemplate();
            GridViewDataTextColumn col2 = gvTrupi.Columns["Pershkrimi"] as GridViewDataTextColumn;
            col2.DataItemTemplate = new MyReadOnlyMemoTemplate();
            GridViewDataTextColumn col3 = gvTrupi.Columns["Perqindja"] as GridViewDataTextColumn;
            col3.DataItemTemplate = new MyDoubleTemplate(false, 2, "0"); // "0.00");
            col3.PropertiesEdit.DisplayFormatString = "0.00";

        }

        /// <summary>
        /// krijon koleksionin me qendrat e kesaj skeme me te dhenat e grides
        /// </summary>
        /// <param name="idNdermarrje">id e ndermarjes</param>
        /// <returns>koleksionin me qendrat</returns>
        private DbCore.DbQendraKosto.colTrupiSkemaQK ruajTrup(int idNdermarrje)
        {
            DbCore.DbQendraKosto.colTrupiSkemaQK trupi = new DbCore.DbQendraKosto.colTrupiSkemaQK();
            DbCore.DbQendraKosto.clsTrupiSkemaQK tr = new DbCore.DbQendraKosto.clsTrupiSkemaQK();
            JavaScriptSerializer serializusi = new JavaScriptSerializer();
            int rreshta = gvTrupi.VisibleRowCount + 1;
            object[] kodi = (object[])serializusi.DeserializeObject(hfKodi.Value);
            object[] emertimi = (object[])serializusi.DeserializeObject(hfEmertimi.Value);
            object[] perqindja = (object[])serializusi.DeserializeObject(hfKoha.Value);
            for (int i = 0; i < rreshta - 1; i++)
            {
                tr = new DbCore.DbQendraKosto.clsTrupiSkemaQK();
                if (kodi[i] != null && kodi[i].ToString() != "")
                {
                    tr.Kodi = kodi[i].ToString();
                    DbCore.DbQendraKosto.clsQendraKosto qendra = new DbCore.DbQendraKosto.clsQendraKosto(kodi[i].ToString(), idNdermarrje);
                    tr.IdQK = qendra.Id;

                }
                if (emertimi[i] != null &&  emertimi[i].ToString() != "")
                    tr.Pershkrimi = emertimi[i].ToString();
                if (perqindja[i] != null && perqindja[i].ToString() != "")
                    tr.Perqindja = Convert.ToDecimal(perqindja[i]);

                if (tr.IdQK != -1 && tr.IdQK != 0)
                    trupi.Add(tr);

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
                        DbCore.DbQendraKosto.colTrupiSkemaQK trupi = new DbCore.DbQendraKosto.colTrupiSkemaQK();
                        DbCore.DbQendraKosto.clsTrupiSkemaQK tr = new DbCore.DbQendraKosto.clsTrupiSkemaQK();
                        JavaScriptSerializer serializusi = new JavaScriptSerializer();
                        int rreshta = gvTrupi.VisibleRowCount + 1;
                        object[] kodi = (object[])serializusi.DeserializeObject(hfKodi.Value);
                        object[] emertimi = (object[])serializusi.DeserializeObject(hfEmertimi.Value);
                        object[] perqindja = (object[])serializusi.DeserializeObject(hfKoha.Value);
                        for (int i = 0; i < rreshta - 1; i++)//krijohet kolectioni me trupat e fleteve kontabel e futura nga perdoruesi
                        {
                            tr = new DbCore.DbQendraKosto.clsTrupiSkemaQK();
                            if (kodi[i] != null && kodi[i].ToString() != "")
                            {
                                tr.Kodi = kodi[i].ToString();

                            }
                            if (emertimi[i] != null && emertimi[i].ToString() != "")
                                tr.Pershkrimi = emertimi[i].ToString();
                            if (perqindja[i] != null && perqindja[i].ToString() != "")
                                tr.Perqindja = Convert.ToDecimal(perqindja[i]);

                            trupi.Add(tr);

                        }
                        if (key != -1)
                            trupi.RemoveAt(key);
                        else
                        {
                            trupi.Add(new DbCore.DbQendraKosto.clsTrupiSkemaQK());
                        }
                        if (trupi.Count == 0)
                        {
                            trupi.Add(new DbCore.DbQendraKosto.clsTrupiSkemaQK());
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
            if (this.gvTrupi.Columns["Fshi"] == null)
            {
                GridViewDataTextColumn fshi = new GridViewDataTextColumn();
                fshi.Caption = "Fshi";
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
                GridViewDataTextColumn col0 = ((ASPxGridView)sender).Columns["Fshi"] as GridViewDataTextColumn;
                GridViewDataTextColumn col1 = ((ASPxGridView)sender).Columns["Kodi"] as GridViewDataTextColumn;
                GridViewDataTextColumn col2 = ((ASPxGridView)sender).Columns["Pershkrimi"] as GridViewDataTextColumn;
                GridViewDataTextColumn col3 = ((ASPxGridView)sender).Columns["Perqindja"] as GridViewDataTextColumn;
                ASPxButton btn0 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col0, "btn") as ASPxButton;
                ASPxComboBox cmb1 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col1, "cmbBox") as ASPxComboBox;
                ASPxMemo txt2 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col2, "txtBox") as ASPxMemo;
                ASPxTextBox txt3 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col3, "txtBox") as ASPxTextBox;
                //vendosen client side eventet e kolonave
                if (btn0 != null)
                {
                    btn0.ClientInstanceName = String.Format("btnFshi{0}", e.VisibleIndex);
                    btn0.ClientSideEvents.Click = String.Format("function(s,e){{FshiClicked({0});}}", e.VisibleIndex);
                }
                if (cmb1 != null)
                {
                    cmb1.DropDownButton.Visible = false;
                    cmb1.Buttons.Add();
                    cmb1.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                    cmb1.TextFormatString = "{0},{1}";
                    cmb1.Columns.Add(new ListBoxColumn("Kodi"));
                    cmb1.Columns.Add(new ListBoxColumn("Pershkrimi"));
                    DbCore.DbQendraKosto.colQendraKosto col = new DbCore.DbQendraKosto.colQendraKosto();
                    col.mbushGjitheQendraKostoBijSipasNdermarjesAktiv(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    cmb1.DataSource = col;
                    cmb1.DataBind();
                    cmb1.ClientInstanceName = String.Format("Kodi{0}", e.VisibleIndex);
                    cmb1.ClientSideEvents.TextChanged = String.Format("function(s,e){{TextChangedQendra(Kodi{0}, Emertimi{0},{0});}}", e.VisibleIndex);
                    cmb1.ClientSideEvents.KeyPress = String.Format("function(s,e){{var code = _getKeyCode(e.htmlEvent); KeyPressQendra(code,Kodi{0},{0}); }}", e.VisibleIndex);
                    cmb1.ClientSideEvents.LostFocus = String.Format("function(s,e){{LostFocusQendra(Kodi{0}, Emertimi{0},{0});}}", e.VisibleIndex);
                    cmb1.ClientSideEvents.ButtonClick = String.Format("function(s,e){{ButtonClickedQendra(Kodi{0},{0}); }}", e.VisibleIndex);

                }
                if (txt2 != null)
                {
                    txt2.ClientInstanceName = String.Format("Emertimi{0}", e.VisibleIndex);
                }

                if (txt3 != null)
                {


                    txt3.ClientInstanceName = "Perqindja" + e.VisibleIndex.ToString();
                    txt3.ClientSideEvents.TextChanged = String.Format("function(s,e){{TextChangedPerqindja(Perqindja{0},{0});}}", e.VisibleIndex);

                }
            }

        }

        #endregion
    }
}