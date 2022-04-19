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
using DbCore.IMBUtils.Types;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{

    /// <summary>
    /// Faqja e shtimit te aktiviteteve
    /// </summary>
    public partial class Shto_Aktivitete : MyPageBase
    {
        private const string prefixMesazhNjejes = "Aktiviteti me Kod: ";
        private const string prefixMesazhShumes = "Aktivitetet me Kod: ";
        private const string suffixMesazhNjejesGabimiLidhur = " eshte i lidhur dhe nuk mund te fshihet";
        private const string suffixMesazhShumesGabimiLidhur = " jane te lidhur dhe nuk mund te fshihen";
        private const string suffixMesazhNjejesSuksesi = " u fshi me sukses!";
        private const string suffixMesazhShumesSuksesi = " u fshine me sukses!";
        private const string lidhesMesazhi = " Kurse ";
        private const string gabim2aktivitetenegride = "GABIM: Ndodhen 2 aktivitete me te njejten id ne gride";
        private const string mesazhZgjidhniNje = "Ju lutem zgjidhni te pakten nje aktivitet!";
        private int idperdoruesi, idnderviti, idNdermarrje, idviti, idgjuha;
        private const int idstatusdok = 1;
        private string komponente => "Shto_Aktivitete.aspx";
        private string guidString; 
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
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
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
            CultureInfo cultinf = DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo(idgjuha);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (!Page.IsPostBack)
            {
                hfState.Set("idGjuha", idgjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                guidString = Guid.NewGuid().ToString();
                hfState["guidString"] = guidString;
                EmrateTabeve(rm, cultinf);
                ASPxPageControl1.ActiveTabIndex = 0;
                percaktoTemplateMenu(idgjuha, idviti, idperdoruesi, idNdermarrje, ASPxMenu1);
                konfiguroVleraFillestare(idNdermarrje, rm, cultinf, idgjuha);
                mbushGridAktiviteteNgaDB(idNdermarrje);
                konfiguroGrideTrupi(idNdermarrje);
                mbushComboBoxFiltra(idgjuha, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
                
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idperdoruesi, idNdermarrje, idviti, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idperdoruesi, idNdermarrje, idviti, komponente);
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                konfiguroGride(idNdermarrje, cmbKonfigurimi.Text.Split(';')[0], 802, rm, cultinf);
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "gvAktivitetet", gvAktivitetet, cmbKonfigurimi.Text.Split(';')[0], 802.ToString(), DbCore.mySessionObjects.ktheGjuhe(Session));
            }
            else
            {
                guidString = hfState["guidString"].ToString();
                mbushGridAktiviteteNgaSession(idNdermarrje);
                konfiguroGride(idNdermarrje, cmbKonfigurimi.Text.Split(';')[0], 802, rm, cultinf);
            }
            GridUtil.konfigGrideListeEMadhePaTheme(gvAktivitetet, "IdKoka");
            percaktoTemplateMenu(idgjuha, idviti, idperdoruesi, idNdermarrje, ASPxMenu1);
            percaktoTemplateTrupash();
            mbushComboBoxFiltra(idgjuha, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            GridUtil.ToolTipButonaveMbiGride(gvAktivitetet, cultinf, rm);
        }

        /// <summary>
        /// Vendos emrat e tabeve ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateTabeve(System.Resources.ResourceManager rm, System.Globalization.CultureInfo cultinf)
        {
            ASPxPageControl1.TabPages[0].Text = rm.GetString("labelAdministrimiTePergjithshme", cultinf);
            ASPxPageControl1.TabPages[1].Text = rm.GetString("labelRaportAktiviteti", cultinf);
            ASPxPageControl1.TabPages[2].Text = rm.GetString("MenuItemBurimet", cultinf);
            konfigurimi_Label.Text = rm.GetString("lblModeli", cultinf);
        }

        /// <summary>
        /// mbush combon e filtrave
        /// </summary>
        /// <param name="idNdermarrje">id e ndermarjes</param>
        private static void mbushComboBoxFiltra(int idGjuha, int idNdermarrje, int idkonfig)
        {
            clsToolbarConfig.mbushComboBoxFiltraMeValFieldTextField(idGjuha, idNdermarrje, "gvAktivitetet", "Shto_Aktivitete.aspx", "IdFiltra", "FiltraShenime", idkonfig);
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
            clsToolbarConfig.percaktoTemplateMenu(idGjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1, komponente, this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, hfShtimModifikim.Value == "modifikim" ? false : true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
        }

        protected void RuajKolona_Click(object sender, EventArgs e)
        {

            int idfiltri = 0;
            int idkonf = DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(cmbKonfigurimi.Text.Split(';')[0], idNdermarrje);
            DbCore.clsMesazh mesazh = GridUtil.ruajFiltra(idNdermarrje, idperdoruesi, idgjuha, "gvAktivitetet ", komponente, "FilterDefault", gvAktivitetet.FilterExpression, gvAktivitetet, "Kodi", idkonf, out idfiltri);//ruan filtrin e zgjedhur tek filtrat
            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                return;

            }
            mesazh = GridUtil.ruajkonfigurimgride(gvAktivitetet, cmbKonfigurimi.Text, idNdermarrje, idperdoruesi, 802, idfiltri, idviti, DbCore.mySessionObjects.ktheCultureInfo(Session), DbCore.mySessionObjects.ktheGjuhe(Session));/// ruan konfigurimin e grides dhe filtrin

            clsToolbarConfig.mbushComboBoxFiltra(idgjuha, idNdermarrje, "gvAktivitetet ", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);

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
        protected void gvAktivitetet_DataBound(object sender, EventArgs e)
        {// shton colonen # per selektim dhe disa karakteristika te grides
            if (gvAktivitetet.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per 
                //perzgjidh
                GridViewCommandColumn check = new GridViewCommandColumn("#") { ShowSelectCheckbox = true, Width = System.Web.UI.WebControls.Unit.Percentage(2) };
                //behet per te afishuar rreshtin qe do sherbej per filtrim
                gvAktivitetet.Settings.ShowFilterRow = true;
                gvAktivitetet.Settings.ShowFilterRowMenu = true;
                gvAktivitetet.Columns.Add(check);
                gvAktivitetet.KeyFieldName = "IdKoka";
                gvAktivitetet.SettingsBehavior.AllowSelectByRowClick = true;
                gvAktivitetet.SettingsBehavior.AllowFocusedRow = true;
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

            KonfigurimComboGride.shtoNjesiKohe(gvAktivitetet, rm, ci, "NjesiKohe");

           
            gvAktivitetet.Columns["#"].VisibleIndex = 0;

        }

        /// <summary>
        /// perdoret per te shfaqur njesine e kohes
        /// </summary>
        //private void shtoNjesiKohe()
        //{
        //    gvAktivitetet.Columns.Remove(gvAktivitetet.Columns["NjesiKohe"]);
        //    GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
        //    colnew.PropertiesComboBox.Items.Add("", Convert.ToInt32(DbCore.DbProdhimi.NjesiKohe.Undefined));
        //    colnew.PropertiesComboBox.Items.Add(DbCore.DbProdhimi.NjesiKohe.Sekonda.ToString(), Convert.ToInt32(DbCore.DbProdhimi.NjesiKohe.Sekonda));
        //    colnew.PropertiesComboBox.Items.Add(DbCore.DbProdhimi.NjesiKohe.Minuta.ToString(), Convert.ToInt32(DbCore.DbProdhimi.NjesiKohe.Minuta));
        //    colnew.PropertiesComboBox.Items.Add(DbCore.DbProdhimi.NjesiKohe.Ore.ToString(), Convert.ToInt32(DbCore.DbProdhimi.NjesiKohe.Ore));
        //    colnew.PropertiesComboBox.Items.Add(DbCore.DbProdhimi.NjesiKohe.Dite.ToString(), Convert.ToInt32(DbCore.DbProdhimi.NjesiKohe.Dite));
        //    colnew.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        //    colnew.FieldName = "NjesiKohe";
        //    gvAktivitetet.Columns.Add(colnew);
        //}


        /// <summary>
        /// perdoret ne rastet kur grida ben callback per ta rimbushur griden
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void gvAktivitetet_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            if (e.CallbackName == "COLUMNMOVE" && gvAktivitetet.AllColumns[int.Parse(e.Args[0])].Width.Value == 0)
                gvAktivitetet.AllColumns[int.Parse(e.Args[0])].Width = System.Web.UI.WebControls.Unit.Percentage(3);
            if (e.CallbackName == "APPLYFILTER" && e.Args[0] == "")
            {
                MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
                ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
                cmbFiltra.Text = "";
            }
            CultureInfo cultinf = DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo((int)hfState["idGjuha"]);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            GridUtil.ToolTipButonaveMbiGride(gvAktivitetet, cultinf, rm);
            // konfiguroVleraFillestare(); 
            //  konfiguroGride(idNdermarrje, cmbKonfigurimi.Text.Split(';')[0], 802);

        }

        /// <summary>
        /// perdoret per te vendosur filtrat tek headeri i grides
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvAktivitetet_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            if (e.Column.FieldName == "Emertimi" || e.Column.FieldName == "Kodi")
            {
                e.Values.Clear();
                e.AddValue("(Te gjithe)", string.Empty, "true");
                e.AddValue("Nga A-D ", string.Empty, String.Format("{0}>'A     ' and {0}<'DDDDDDD'", e.Column.FieldName));
                e.AddValue("Nga D-G ", string.Empty, String.Format("{0}>'D     ' and {0}<'GGGGGGG'", e.Column.FieldName));
                e.AddValue("Nga H-K ", string.Empty, String.Format("{0}>'H     ' and {0}<'KKKKKKK'", e.Column.FieldName));
                e.AddValue("Nga L-O ", string.Empty, String.Format("{0}>'L     ' and {0}<'OOOOOOO'", e.Column.FieldName));
                e.AddValue("Nga P-S ", string.Empty, String.Format("{0}>'P     ' and {0}<'SSSSSSS'", e.Column.FieldName));
                e.AddValue("Nga T-W ", string.Empty, String.Format("{0}>'T     ' and {0}<'WWWWWWW'", e.Column.FieldName));
                e.AddValue("Nga X-Z ", string.Empty, String.Format("{0}>'X     ' and {0}<'ZZZZZZZ'", e.Column.FieldName));
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
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        public void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje
            MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            int idNdermarrje = (int)hfState["idNdermarrje"];
            int idGjuha = (int)hfState["idGjuha"];
            DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
            //filtra.mbushFiltraGridaSipasFiltraEmri(cmbFiltra.Text, idNdermarrje);
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuha, "gvAktivitetet", komponente, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();
                mbushComboBoxFiltra(idGjuha, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
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
            int idNdermarrje = (int)hfState["idNdermarrje"];
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka((int)hfState["idGjuha"], "gvAktivitetet", komponente, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gvAktivitetet.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("Kodi", gvAktivitetet);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvAktivitetet.GetSortedColumns();
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
            mbushComboBoxFiltra((int)hfState["idGjuha"], idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
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
                rreshtat = gvAktivitetet.GetSelectedFieldValues("IdKoka");
            else
            {
                rreshtat = new List<object>();
                rreshtat.Add(hfId.Value);
            }
            // List<object> rreshtat = gvAktivitetet.GetSelectedFieldValues("IdKoka");
            if (rreshtat.Count == 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhZgjidhniNje, pnlMesazhi);
                return;
            }
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            //clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "", pnlMesazhi);
            List<string> TeFshire = new List<string>(), TePaFshire = new List<string>();
            DbCore.DbRegjistrim.clsDatabaseRegjistrim dbRegjistrim = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();

            foreach (object id in rreshtat)
            {
                DbCore.DbProdhimi.clsAktiviteteKoka aktivitet = new DbCore.DbProdhimi.clsAktiviteteKoka(Convert.ToInt32(id));
                DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti() { IdKonfigAmbjente = aktivitet.IdKonfig };
                konf.mbushKonfigAmbjSipasId(konf.IdKonfigAmbjente);
                bool lidhur = dbRegjistrim.eshteDokumentiILidhurCelje(aktivitet.IdKoka.ToString(), konf.IdNivel.ToString());
                if (lidhur)
                {
                    TePaFshire.Add(aktivitet.Kodi);
                    continue;
                }

                aktivitet.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                //mesazh = aktivitet.fshi(null);
                mesazh = aktivitet.fshi();
                if (aktivitet.IdKoka == 0)
                    continue;
                if (mesazh.Status)
                {
                    #region Heq llogarite nga grida
                    hiqNgaGrida(aktivitet.IdKoka, aktivitet.IdNdermarje);
                    #endregion
                    TeFshire.Add(aktivitet.Kodi);

                    ASPxPageControl1.ActiveTabIndex = 0;
                    hfStatusi.Value = "true";
                }
            }
            dbRegjistrim.Dispose();
            string mesazhInfoGabim = "", mesazhInfoSukses = "";
            if (TePaFshire.Count == 1)
                mesazhInfoGabim = String.Format("{0}{1}{2}", prefixMesazhNjejes, String.Join(";", TePaFshire), suffixMesazhNjejesGabimiLidhur);
            else
                if (TePaFshire.Count > 1)
                    mesazhInfoGabim = String.Format("{0}{1}{2}", prefixMesazhShumes, String.Join(";", TePaFshire), suffixMesazhShumesGabimiLidhur);
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

        }

        /// <summary>
        /// heq nga grida rreshtat e fshire
        /// </summary>
        /// <param name="idndermarje">idndermarjes</param>
        /// <param name="idkoka"> idkoka</param>
        private void hiqNgaGrida(int idkoka, int idndermarje)
        {
            if (gvAktivitetet.DataSource != null)
            {
                DataTable dt = (DataTable)gvAktivitetet.DataSource;
                DataRow[] drs = dt.Select(String.Format("IdKoka = '{0}'", idkoka));
                if (drs.Length > 1)
                    throw new Exception(gabim2aktivitetenegride);
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                dt.Rows.Remove(dr);
                gvAktivitetet.DataBind();
            }
            else mbushGridAktiviteteNgaDB(idndermarje);
        }

        /// <summary>
        /// shton nte gride rreshtat e rinj
        /// </summary>
        /// <param name="idNdermarrje">idndermarje</param>
        /// <param name="idkoka">id e kokes</param>
        private void shtoNeGrid(int idNdermarrje, int idkoka)
        {
            if (gvAktivitetet.DataSource != null)
            {
                DataTable dt = (DataTable)gvAktivitetet.DataSource;
                DataRow[] drs = dt.Select("IdKoka = " + idkoka);
                if (drs.Length > 0)
                    throw new Exception(gabim2aktivitetenegride);
                DataRow newArtDr = DbCore.DbProdhimi.colAktiviteteKoka.merrAktivitetSipasIdDR(idkoka);
                dt.ImportRow(newArtDr);
            }
            else mbushGridAktiviteteNgaDB(idNdermarrje);
        }

        /// <summary>
        /// modifikon ne gride aktivitetin e modifikuar
        /// </summary>
        /// <param name="idNdermarrje">idndermarje</param>
        ///<param name="idkoka">id koka</param>
        private void modifikoNeGrid(int idNdermarrje, int idkoka)
        {
            if (gvAktivitetet.DataSource != null)
            {
                DataTable dt = (DataTable)gvAktivitetet.DataSource;
                DataRow[] drs = dt.Select("IdKoka = " + idkoka);
                if (drs.Length > 1)
                    throw new Exception(gabim2aktivitetenegride);
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                DataRow newArtDr = DbCore.DbProdhimi.colAktiviteteKoka.merrAktivitetSipasIdDR(idkoka);
                object[] arr = newArtDr.ItemArray;
                dr.ItemArray = arr;
            }
            else mbushGridAktiviteteNgaDB(idNdermarrje);
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
                ruajAktivitete();
            }

        }

        /// <summary>
        /// konfiguron vlerat fillestare
        /// </summary>
        /// <param name="idNdermarrje">id e ndermarjes</param>
        private void konfiguroVleraFillestare(int idNdermarrje, System.Resources.ResourceManager rm, System.Globalization.CultureInfo cultinf, int idGjuha)
        { //mbush komboboxet dhe gridat e faqes
            ASPxPageControl1.ActiveTabIndex = 0;
            ConfigureAspxComboBox.KonfiguroComboBoxNjesiKohe(cmbNjesiKohe, 0);
            mbushListeTrupash();
            ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idperdoruesi, idNdermarrje, cmbKonfigurimi, 43, rm, cultinf, idGjuha);
            cmbKonfigurimi.SelectedIndex = 0;
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()), idGjuha);
            hfKonffillestar.Value = String.Format("{0};{1}", konf.KodKonfigAmbjente, konf.PershkrimKonfigAmbjente);
        }

        /// <summary>
        /// mbush griden nga sesioni
        /// </summary>
        /// <param name="idNdermarrje">id e ndermarjes</param>
        private void mbushGridAktiviteteNgaSession(int idNdermarrje)
        {
            DataTable tmpObject;
            bool sukses = DbCore.mySessionObjects.merrGrideNgaSessioni(Session, out tmpObject);
            if (!sukses)
                mbushGridAktiviteteNgaDB(idNdermarrje);
            else
            {
                gvAktivitetet.DataSource = tmpObject;
                gvAktivitetet.DataBind();
                tmpObject.Dispose();
            }
        }

        /// <summary>
        /// mbush griden nga databaza
        /// </summary>
        /// <param name="idNdermarrje">id e ndermarjes</param>
        private void mbushGridAktiviteteNgaDB(int idNdermarrje)
        {//mbush griden e popupit me te dhena            
            DataTable dt = DbCore.DbProdhimi.colAktiviteteKoka.merrAktivitetetNdermarjeDT(idNdermarrje);
            DbCore.mySessionObjects.ruajGrideNeSession(Session, dt);
            gvAktivitetet.DataSource = dt;
            gvAktivitetet.DataBind();
            dt.Dispose();
        }

        /// <summary>
        /// ruan aktivitete
        /// </summary>
        private void ruajAktivitete()
        {
            if (!Page.IsValid)
                return;
            DbCore.DbProdhimi.clsAktiviteteKoka aktivitete = new DbCore.DbProdhimi.clsAktiviteteKoka();
            try
            {
                aktivitete = krijoAktivitet();
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
            if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim")
            {
                if (!tedrejtaInfo.DShtim)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                    hfStatusi.Value = "false";
                    return;
                }
                mesazh = aktivitete.ruaj();
                eshteShtim = true;
            }
            else
            {
                if (!tedrejtaInfo.DMod)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                    hfStatusi.Value = "false";
                    return;
                }
                DbCore.DbRegjistrim.clsDatabaseRegjistrim dbRegjistrim = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
                DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti() { IdKonfigAmbjente = aktivitete.IdKonfig };
                konf.mbushKonfigAmbjSipasId(konf.IdKonfigAmbjente);
                eshteShtim = false;
                aktivitete.IdKoka = int.Parse(hfId.Value);
                bool lidhur = dbRegjistrim.eshteDokumentiILidhurCelje(aktivitete.IdKoka.ToString(), konf.IdNivel.ToString());
                if (lidhur.ToString() != hfLidhur.Value)
                {
                    mesazh.Status = false;
                    mesazh.PershkrimMesazhi = "Aktiviteti  eshte i lidhur";
                }
                else
                    mesazh = aktivitete.modifiko();
                dbRegjistrim.Dispose();
            }
            if (mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                if (eshteShtim)
                    shtoNeGrid((int)hfState["idNdermarrje"], aktivitete.IdKoka);
                else //modifikim
                    modifikoNeGrid((int)hfState["idNdermarrje"], aktivitete.IdKoka);
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
        private DbCore.DbProdhimi.clsAktiviteteKoka krijoAktivitet()
        {
            int njesikohe = Convert.ToInt32(cmbNjesiKohe.Value);
            bool isshtim;
            if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim")
                isshtim = true;
            else
                isshtim = false;
            DbCore.DbShare.clsKonfigurimAmbjenti konfig = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konfig.mbushKonfigAmbjSipasKod(cmbKonfigurimi.Text, (int)hfState["idNdermarrje"]);
            decimal koha = 0;
            decimal.TryParse(txtKohaPlan.Text, out koha);
            DbCore.DbProdhimi.clsAktiviteteKoka aktivitet = new DbCore.DbProdhimi.clsAktiviteteKoka(0, txtKodi.Text, txtEmertimi.Text, txtPershkrimi.Text, koha, njesikohe, idperdoruesi, idNdermarrje, konfig.IdKonfigAmbjente, idstatusdok, ruajTrup(idNdermarrje), isshtim);

            return aktivitet;
        }

        /// <summary>
        /// cakton properti ne javascript
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvAktivitetet_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gvAktivitetet.PageIndex;
            e.Properties["cpPageRow"] = gvAktivitetet.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gvAktivitetet.VisibleRowCount;
        }

        /// <summary>
        /// kur grida ben callback
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvAktivitetet_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string idkomponente = "";
            string kodkonfigurimi = "";

            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                kodkonfigurimi = arr[1];
                idkomponente = arr[0];
                if (arr[2] == "")
                    gvAktivitetet.FilterExpression = "";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    //filtra.mbushFiltraGridaSipasFiltraKodi(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvAktivitetet", komponente, (int)hfState["idNdermarrje"], int.Parse(cmbKonfigurimi.Value.ToString()));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], (int)hfState["idNdermarrje"], koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        gvAktivitetet.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvAktivitetet);
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

            gvAktivitetet.Selection.UnselectAll();
        }


        /// <summary>
        /// perdoret per te shfaqur po jo tek komboja e aktivit
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvAktivitetet_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {

        }

        /// <summary>
        /// per filtrimin me elementin bosh
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvAktivitetet_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if (e.Column.FieldName == "NjesiKohe")
            {
                if (Converter.ConvertToInt(e.Value) == 0)
                {
                    e.Criteria = null;
                }
            }
        }

        #region Grida e burimeve
        /// <summary>
        /// konfiguron griden e burimeve
        /// </summary>
        /// <param name="idNdermarrje">id e ndermarjes</param>
        private void konfiguroGrideTrupi(int idNdermarrje)
        {//konfigurohet grida
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            GridUtil.percaktoVisibleColumnsMeWidth(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, gvTrupi, "gvTrupi", komponente);
            GridUtil.konfiguroGrideRegjistrimEvogelPaTheme(gvTrupi, "IdTrupi", false);
            gvTrupi.Settings.UseFixedTableLayout = false;
            // gvAktivitetet.Columns["Fshi"].VisibleIndex = 14;
        }

        /// <summary>
        /// mbush griden me rreshta bosh
        /// </summary>
        private void mbushListeTrupash()
        {//mbushet grida me te dhena            
            DbCore.DbProdhimi.colAktiviteteTrupi col = new DbCore.DbProdhimi.colAktiviteteTrupi();
            for (int i = 0; i < 5; i++)
            {
                DbCore.DbProdhimi.clsAktiviteteTrupi o = new DbCore.DbProdhimi.clsAktiviteteTrupi();
                col.Add(o);
            }
            gvTrupi.DataSource = col;
            gvTrupi.DataBind();
        }

        /// <summary>
        /// mbush griden e burimeve me burimet e ketij aktiviteti
        /// </summary>
        /// <param name="id">id e aktivitetit</param>
        private void mbushListeTrupashMod(int id, DateTime dtNdryshimi)
        {//mbushet grida me te dhena
            DbCore.DbProdhimi.colAktiviteteTrupi col = new DbCore.DbProdhimi.colAktiviteteTrupi(id, dtNdryshimi);
            col.Add(new DbCore.DbProdhimi.clsAktiviteteTrupi());
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
            GridViewDataTextColumn col1 = gvTrupi.Columns["KodBurimi"] as GridViewDataTextColumn;
            col1.DataItemTemplate = new MyComboTemplate();
            GridViewDataTextColumn col2 = gvTrupi.Columns["PershkrimBurimi"] as GridViewDataTextColumn;
            col2.DataItemTemplate = new MyReadOnlyMemoTemplate();
            GridViewDataTextColumn col3 = gvTrupi.Columns["Koha"] as GridViewDataTextColumn;
            col3.DataItemTemplate = new MyDoubleTemplate(false, 2, "0"); // "0.00");
            GridViewDataTextColumn col4 = gvTrupi.Columns["Kosto"] as GridViewDataTextColumn;
            col4.DataItemTemplate = new MyLabelTemplate();
            col4.PropertiesEdit.DisplayFormatString = "0.00";
            GridViewDataTextColumn col5 = gvTrupi.Columns["KostoBurimi"] as GridViewDataTextColumn;
            col5.DataItemTemplate = new MyLabelTemplate();
            col5.PropertiesEdit.DisplayFormatString = "0.00";
        }

        /// <summary>
        /// krijon koleksionin me burimet e ketij aktiviteti me te dhenat e grides
        /// </summary>
        /// <param name="idNdermarrje">id e ndermarjes</param>
        /// <returns>koleksionin me burimet</returns>
        private DbCore.DbProdhimi.colAktiviteteTrupi ruajTrup(int idNdermarrje)
        {
            DbCore.DbProdhimi.colAktiviteteTrupi trupi = new DbCore.DbProdhimi.colAktiviteteTrupi();
            DbCore.DbProdhimi.clsAktiviteteTrupi tr = new DbCore.DbProdhimi.clsAktiviteteTrupi();
            JavaScriptSerializer serializusi = new JavaScriptSerializer();
            int rreshta = gvTrupi.VisibleRowCount + 1;
            object[] kodi = (object[])serializusi.DeserializeObject(hfKodi.Value);
            object[] emertimi = (object[])serializusi.DeserializeObject(hfEmertimi.Value);
            object[] koha = (object[])serializusi.DeserializeObject(hfKoha.Value);
            object[] kostoburimi = (object[])serializusi.DeserializeObject(hfKostoBurimi.Value);
            object[] kosto = (object[])serializusi.DeserializeObject(hfKosto.Value);
            for (int i = 0; i < rreshta - 1; i++)//krijohet kolectioni me trupat e fleteve kontabel e futura nga perdoruesi
            {
                tr = new DbCore.DbProdhimi.clsAktiviteteTrupi();
                if (kodi[i] != null && kodi[i].ToString() != "")
                {
                    tr.KodBurimi = kodi[i].ToString();
                    DbCore.DbProdhimi.clsBurime burim = new DbCore.DbProdhimi.clsBurime(kodi[i].ToString(), idNdermarrje);
                    tr.IdBurimi = burim.IdBurimi;

                }
                if (emertimi[i] != null  && emertimi[i].ToString() != "")
                    tr.PershkrimBurimi = emertimi[i].ToString();
                if (koha[i] != null  && koha[i].ToString() != "")
                    tr.Koha = Convert.ToDecimal(koha[i]);
                if (kostoburimi[i] != null && kostoburimi[i].ToString() != "")
                    tr.KostoBurimi = Convert.ToDecimal(kostoburimi[i]);
                if (kosto[i] != null  && kosto[i].ToString() != "")
                    tr.Kosto = Convert.ToDecimal(kosto[i]);
                tr.DtNdryshimi = dteDateAkt.Date;
                if (tr.IdBurimi != -1 && tr.IdBurimi != 0)
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
                            List<string> datat = DbCore.DbProdhimi.colAktiviteteTrupi.merrDataAktiviteti(int.Parse(e.Parameters.Split(';')[0]));
                            if (datat.Count == 0) datat.Add("01/01/2012");
                            mbushListeTrupashMod(int.Parse(e.Parameters.Split(';')[0]), DateTime.Parse(datat[0]));
                        }
                        else
                        {
                            mbushListeTrupash();
                        }

                    }
                    else
                    {
                        if (e.Parameters == "ndryshodate")
                        {
                            mbushListeTrupashMod(int.Parse(hfId.Value), DateTime.Parse(cmbNdryshimi.Text));
                        }
                        else
                        {
                            if (e.Parameters.ToString() != "")
                            {
                                key = int.Parse(e.Parameters.ToString());
                            }
                            DbCore.DbProdhimi.colAktiviteteTrupi trupi = new DbCore.DbProdhimi.colAktiviteteTrupi();
                            DbCore.DbProdhimi.clsAktiviteteTrupi tr = new DbCore.DbProdhimi.clsAktiviteteTrupi();
                            JavaScriptSerializer serializusi = new JavaScriptSerializer();
                            int rreshta = gvTrupi.VisibleRowCount + 1;
                            object[] kodi = (object[])serializusi.DeserializeObject(hfKodi.Value);
                            object[] emertimi = (object[])serializusi.DeserializeObject(hfEmertimi.Value);
                            object[] koha = (object[])serializusi.DeserializeObject(hfKoha.Value);
                            object[] kostoburimi = (object[])serializusi.DeserializeObject(hfKostoBurimi.Value);
                            object[] kosto = (object[])serializusi.DeserializeObject(hfKosto.Value);
                            for (int i = 0; i < rreshta - 1; i++)//krijohet kolectioni me trupat e fleteve kontabel e futura nga perdoruesi
                            {
                                tr = new DbCore.DbProdhimi.clsAktiviteteTrupi();
                                if (kodi[i] != null && kodi[i].ToString() != "")
                                {
                                    tr.KodBurimi = kodi[i].ToString();

                                }
                                if (emertimi[i] != null && emertimi[i].ToString() != "")
                                    tr.PershkrimBurimi = emertimi[i].ToString();
                                if (koha[i] != null && koha[i].ToString() != "")
                                    tr.Koha = Convert.ToDecimal(koha[i]);
                                if (kostoburimi[i] != null  && kostoburimi[i].ToString() != "")
                                    tr.KostoBurimi = Convert.ToDecimal(kostoburimi[i]);
                                if (kosto[i] != null  && kosto[i].ToString() != "")
                                    tr.Kosto = Convert.ToDecimal(kosto[i]);
                                trupi.Add(tr);

                            }
                            if (key != -1)
                                trupi.RemoveAt(key);
                            else
                            {
                                trupi.Add(new DbCore.DbProdhimi.clsAktiviteteTrupi());
                            }
                            if (trupi.Count == 0)
                            {
                                trupi.Add(new DbCore.DbProdhimi.clsAktiviteteTrupi());
                            }
                            this.gvTrupi.DataSource = trupi;
                            this.gvTrupi.DataBind();
                            percaktoTemplateTrupash();
                        }
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
                GridViewDataTextColumn col1 = ((ASPxGridView)sender).Columns["KodBurimi"] as GridViewDataTextColumn;
                GridViewDataTextColumn col2 = ((ASPxGridView)sender).Columns["PershkrimBurimi"] as GridViewDataTextColumn;
                GridViewDataTextColumn col3 = ((ASPxGridView)sender).Columns["Koha"] as GridViewDataTextColumn;
                GridViewDataTextColumn col4 = ((ASPxGridView)sender).Columns["Kosto"] as GridViewDataTextColumn;
                GridViewDataTextColumn col5 = ((ASPxGridView)sender).Columns["KostoBurimi"] as GridViewDataTextColumn;
                ASPxButton btn0 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col0, "btn") as ASPxButton;
                ASPxComboBox cmb1 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col1, "cmbBox") as ASPxComboBox;
                ASPxMemo txt2 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col2, "txtBox") as ASPxMemo;
                ASPxTextBox txt3 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col3, "txtBox") as ASPxTextBox;
                ASPxLabel txt4 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col4, "lbl") as ASPxLabel;
                ASPxLabel txt5 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col5, "lbl") as ASPxLabel;
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
                    cmb1.Columns.Add(new ListBoxColumn("Emertimi"));
                    cmb1.Columns.Add(new ListBoxColumn("KostoPlan"));
                    DataTable dt = DbCore.DbProdhimi.colBurimet.merrBurimeNdermarjeDTAktive((int)hfState["idNdermarrje"]);
                    cmb1.DataSource = dt;
                    cmb1.DataBind();
                    cmb1.ClientInstanceName = String.Format("Kodi{0}", e.VisibleIndex);
                    cmb1.ClientSideEvents.TextChanged = String.Format("function(s,e){{TextChangedBurimi(Kodi{0}, Emertimi{0},{0});}}", e.VisibleIndex);
                    cmb1.ClientSideEvents.KeyPress = String.Format("function(s,e){{var code = _getKeyCode(e.htmlEvent); KeyPressBurimi(code,Kodi{0},{0}); }}", e.VisibleIndex);
                    cmb1.ClientSideEvents.LostFocus = String.Format("function(s,e){{LostFocusBurimi(Kodi{0}, Emertimi{0},{0});}}", e.VisibleIndex);
                    cmb1.ClientSideEvents.ButtonClick = String.Format("function(s,e){{ButtonClickedBurimi(Kodi{0},{0}); }}", e.VisibleIndex);
                    dt.Dispose();
                    if (e.VisibleIndex == ((ASPxGridView)sender).VisibleRowCount - 2)
                    {
                        //if (ugjet)
                        //{
                        //    tempcombo = cmb1;
                        //    ugjet = false;
                        //}
                        //else if (koloneFocus == "KodiKF")
                        //{
                        //    ugjet = true;
                        //}
                    }
                }
                if (txt2 != null)
                {
                    txt2.ClientInstanceName = String.Format("Emertimi{0}", e.VisibleIndex);
                }
                if (txt4 != null)
                {
                    txt4.ClientInstanceName = String.Format("Kosto{0}", e.VisibleIndex);

                }
                if (txt5 != null)
                {
                    txt5.ClientInstanceName = String.Format("KostoBurimi{0}", e.VisibleIndex);
                }
                if (txt3 != null)
                {


                    txt3.ClientInstanceName = "Koha" + e.VisibleIndex.ToString();
                    txt3.ClientSideEvents.TextChanged = String.Format("function(s,e){{TextChangedKoha(Koha{0},{0});}}", e.VisibleIndex);
                    if (e.VisibleIndex == ((ASPxGridView)sender).VisibleRowCount - 2)
                    {
                        //if (ugjet)
                        //{
                        //    tempcombo = cmb4;
                        //    ugjet = false;
                        //}
                        //else if (koloneFocus == "Prioriteti")
                        //{
                        //    ugjet = true;
                        //}
                    }
                }
            }

            //if (temptxt != null)
            //{
            //    temptxt.Focus();
            //}
        }

        #endregion
    }
}