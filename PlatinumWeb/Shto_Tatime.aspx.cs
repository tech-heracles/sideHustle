using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using DevExpress.Web;
using System.Data;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Resources;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{
    public partial class Shto_Tatime : MyPageBase
    {
        private const string prefixMesazhNjejes = "Tatimi me Min: ";
        private const string prefixMesazhShumes = "Tatimet me Min: ";
        private const string suffixMesazhNjejesGabimi = " eshte tatim default dhe nuk mund te fshihet";
        private const string suffixMesazhShumesGabimi = " jane taime default dhe nuk mund te fshihen";
        private const string suffixMesazhNjejesSuksesi = " u fshi me sukses!";
        private const string suffixMesazhShumesSuksesi = " u fshine me sukses!";
        private const string lidhesMesazhi = ". Kurse ";
        private const string kompdefault = "Ky tatim eshte tatim default dhe nuk mund te modifikohet! ";
        private const string gabim2tatimenegride = "GABIM: Ndodhen 2 tatime me te njejten id ne gride";
        private const string gabimmin = "Minimumi nuk duhet te jete me i madh se maximumi!";
        private int idperdoruesi, idnderviti, idviti, idNdermarrje, idgjuha;
        private const string mesazhaktivizimi = "Duhet te zgjidhni nje date aktivizimi! ";
        private const int idstatusdok = 1;
        private const string ilidhur = "Tatimi  eshte i lidhur";
        private const string mesazhZgjidhniNje = "Ju lutem zgjidhni te pakten nje tatim!";

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
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            idperdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idperdoruesi);
            }
            idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            idnderviti = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
            idviti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            idgjuha = DbCore.mySessionObjects.ktheGjuhe(Session);

            if (!Page.IsPostBack)
            {
              
                EmrateTabeve(rm, cultinf);
                hfState.Set("idGjuha", idgjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                ASPxPageControl1.ActiveTabIndex = 0; 
                percaktoTemplateMenu(idgjuha, idviti, idperdoruesi, idNdermarrje, ASPxMenu1);                
                konfiguroVleraFillestare(idNdermarrje, rm, cultinf, idgjuha);
                //clsToolbarConfig.mbushComboBoxFiltra(idgjuha, idNdermarrje, "gvTatimet", int.Parse(cmbKonfigurimi.Value.ToString()), "Shto_Tatime.aspx");
                mbushGridTatimeNgaDB(idNdermarrje);
                konfiguroGride(idNdermarrje, cmbKonfigurimi.Text.Split(';')[0], 706, rm, cultinf);
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "gvTatimet", gvTatimet, cmbKonfigurimi.Text.Split(';')[0], 706.ToString(), (int)hfState["idGjuha"]);

                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idperdoruesi, idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idperdoruesi, idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "Shto_Tatime.aspx");
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
            }
            else
            {
                mbushGridTatimeNgaSession(idNdermarrje);
                konfiguroGride(idNdermarrje, cmbKonfigurimi.Text.Split(';')[0], 706, rm, cultinf);
            }
            GridUtil.konfigGrideListeEMadhePaTheme(gvTatimet, "IdTatime");
            percaktoTemplateMenu(idgjuha, idviti, idperdoruesi, idNdermarrje, ASPxMenu1);
            clsToolbarConfig.mbushComboBoxFiltra(idgjuha, idNdermarrje, "gvTatimet", int.Parse(cmbKonfigurimi.Value.ToString()), "Shto_Tatime.aspx");
            gvTatimet.PercaktoTitlePanel(this, MenuInfo, pnlMesazhi, hfState, idperdoruesi, idNdermarrje, idviti, idgjuha, int.Parse(cmbKonfigurimi.Value.ToString()), "Shto_Tatime.aspx", new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources")), DbCore.mySessionObjects.ktheCultureInfo(Session));
        }

        /// <summary>
        /// Vendos emrat e tabeve ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateTabeve(ResourceManager rm, CultureInfo cultinf)
        {
            ASPxPageControl1.TabPages[0].Text = rm.GetString("TePergjithshmeTab", cultinf);
            ASPxPageControl1.TabPages[1].Text = rm.GetString("tatimetTab", cultinf);
            konfigurimi_Label.Text = rm.GetString("lblModeli", cultinf);
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
            clsToolbarConfig.percaktoTemplateMenu(idGjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1, "Shto_Tatime.aspx", this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, hfShtimModifikim.Value == "modifikim" ? false : true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
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
        protected void gvTatimet_DataBound(object sender, EventArgs e)
        {// shton colonen # per selektim dhe disa karakteristika te grides
            if (gvTatimet.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per 
                //perzgjidh
                GridViewCommandColumn check = new GridViewCommandColumn("#") { ShowSelectCheckbox = true, Width = Unit.Percentage(2) };
                //behet per te afishuar rreshtin qe do sherbej per filtrim
                gvTatimet.Settings.ShowFilterRow = true;
                gvTatimet.Settings.ShowFilterRowMenu = true;
                gvTatimet.Columns.Add(check);
                gvTatimet.KeyFieldName = "IdTatime";
                gvTatimet.SettingsBehavior.AllowSelectByRowClick = true;
                gvTatimet.SettingsBehavior.AllowFocusedRow = true;
            }

        }

        /// <summary>
        /// perdoret per te konfiguruar griden. Per te dhenat mbi kolonat qe do te shfaqen nga databaza si dhe percakton disa karakteristika te grides
        /// </summary>
        /// :  <see cref="GridUtil.percaktoVisibleColumns"/>
        /// :  <see cref="DbCore.clsFunksione.konfiguroGrideListeMadhe(grida,celesigrides)"/>
        /// <param name="idNdermarrje"></param>
        private void konfiguroGride(int idNdermarrje, string kodKonfigurimi, int idKomponente, ResourceManager rm, CultureInfo ci)
        {
            KonfigurimComboGride.ShtoMenyreSipasTatimit(gvTatimet, rm, ci);
            gvTatimet.Columns["#"].VisibleIndex = 0;
        }


        /// <summary>
        /// perdoret ne rastet kur grida ben callback per ta rimbushur griden
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void gvTatimet_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            if (e.CallbackName == "COLUMNMOVE" && gvTatimet.AllColumns[int.Parse(e.Args[0])].Width.Value == 0)
                gvTatimet.AllColumns[int.Parse(e.Args[0])].Width = Unit.Percentage(3);
            if (e.CallbackName == "APPLYFILTER" && e.Args[0] == "")
            {
                DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
                ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
                cmbFiltra.Text = "";
                gvTatimet.FilterExpression = "";
            }
            // konfiguroVleraFillestare(); 
            // konfiguroGride(cmbKonfigurimi.Text.Split(';')[0], 706);

        }

        /// <summary>
        /// perdoret per te vendosur filtrat tek headeri i grides
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvTatimet_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {

            e.Values.Clear();
            e.AddValue("(Te gjithe)", string.Empty, "true");

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
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            //filtra.mbushFiltraGridaSipasFiltraEmri(cmbFiltra.Text, idNdermarrje);
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvTatimet", "Shto_Tatime.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), int.Parse(cmbKonfigurimi.Value.ToString()));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();
                //mbushComboBoxFiltra(idNdermarrje);
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvTatimet", int.Parse(cmbKonfigurimi.Value.ToString()), "Shto_Tatime.aspx");
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
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            DbCore.DbAdmin.clsFiltraGrida filtri = new DbCore.DbAdmin.clsFiltraGrida() { FiltraKodi = cmbFiltra.Text, FiltraShenime = cmbFiltra.Text, FiltraUniversal = false };
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvTatimet", "Shto_Tatime.aspx", idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gvTatimet.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("Min", gvTatimet);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvTatimet.GetSortedColumns();
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
            //    filtri.KoloneRenditje = "Min";
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
            //mbushComboBoxFiltra(idNdermarrje);
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvTatimet", int.Parse(cmbKonfigurimi.Value.ToString()), "Shto_Tatime.aspx");
            percaktoTemplateMenu(idgjuha, idviti, idperdoruesi, idNdermarrje, ASPxMenu1);

            if (mesazh.Status == true)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            hfStatusi.Value = "true";
            cmbFiltra.Text = "";

        }

        /// <summary>
        /// perdoret per te fshire rreshtat e zgjedhur te tatimet nqs perdoruesi konfirmon fshirjen
        /// </summary>
        /// 
        /// <param name="sender"> derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ButtonOk_Click2(object sender, EventArgs e)
        {//fshin rreshtat e selektuar
            List<object> rreshtat;
            if (ASPxPageControl1.ActiveTabIndex == 0)//nqs kemi zgjedhur vetem nje kartele per te pare fshijme vetem ate
                rreshtat = gvTatimet.GetSelectedFieldValues("IdTatime", "Min");
            else
            {
                rreshtat = new List<object>();
                string[] rreshti = { hfId.Value, txtMin.Text };
                rreshtat.Add(rreshti);
            }
            // List<object> rreshtat = gvTatimet.GetSelectedFieldValues("IdTatime", "Min");
            if (rreshtat.Count == 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhZgjidhniNje, pnlMesazhi);
                return;
            }
            //clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "", pnlMesazhi);
            List<string> TeFshire = new List<string>(), TePaFshire = new List<string>();
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();

            foreach (object id in rreshtat)
            {

                DbCore.DbListPagesat.clsTatime tatime = new DbCore.DbListPagesat.clsTatime(Convert.ToInt32(((object[])id)[0]));
                // DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                // konf.mbushKonfigAmbjSipasId(komp.IdKonfig);
                //  DbCore.DbRegjistrim.clsDatabaseRegjistrim dbregjistrim = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
                // bool lidhur = dbregjistrim.eshteDokumentiILidhurCelje(komp.IdTatime.ToString(), konf.IdNivel.ToString());
                if (tatime.Model == 1)
                {
                    TePaFshire.Add(tatime.Min.ToString());
                    continue;
                }
                tatime.IdPerdoruesi = idperdoruesi;
                mesazh = tatime.fshi();
                if (tatime.IdTatime == 0) continue;
                if (mesazh.Status)
                {
                    TeFshire.Add(tatime.Min.ToString());

                    ASPxPageControl1.ActiveTabIndex = 0;
                    hfStatusi.Value = "true";
                }

            }
            string mesazhInfoGabim = "", mesazhInfoSukses = "";
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
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            ConfigureAspxComboBox.mbushComboDataTatime(idNdermarrje, cmbNdryshimi);

            mbushGridTatimeNgaDB(idNdermarrje);
            cmbNdryshimi_pnlNdryshimi.Update();

        }

        /// <summary>
        /// perdoret per te trajtuar ngjarjet e butonave te menuse. Shkon ne faqen e modifikimit te tatimeve kur perdoruesi klikon butonin modifiko
        /// ose ne faqen e shtimit te bankave kur perdoruesi klikon butonin shto
        /// </summary>
        /// <param name="source"> derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {//kryen veprimet e menuse
            if (e.Item.Name == "Ruaj")
            {
                Page.Validate("entries");
                ruajTatim();
            }

        }

        /// <summary>
        /// konfiguron vlerat fillestare
        /// </summary>
        /// <param name="idNdermarrje"></param>
        private void konfiguroVleraFillestare(int idNdermarrje, ResourceManager rm, CultureInfo cultinf, int idGjuha)
        { //mbush komboboxet dhe gridat e faqes
            ASPxPageControl1.ActiveTabIndex = 0;

            ConfigureAspxComboBox.mbushComboMenyraTatime(cmbMenyra);
            dteDateAkt.Date = DbCore.mySessionObjects.merrPeriudheKontabel(Session).FillimiPeriudha;
            ConfigureAspxComboBox.mbushComboDataTatime(idNdermarrje, cmbNdryshimi);
            ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idperdoruesi, idNdermarrje, cmbKonfigurimi, 36, rm, cultinf, idGjuha);
            cmbKonfigurimi.SelectedIndex = 0;
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()), idgjuha);
            hfKonffillestar.Value = String.Format("{0};{1}", konf.KodKonfigAmbjente, konf.PershkrimKonfigAmbjente);
            // cmbKonfigurimi.SelectedIndex = -1;
        }

        /// <summary>
        /// mbush griden nga sesioni
        /// </summary>
        /// <param name="idNdermarrje"></param>
        private void mbushGridTatimeNgaSession(int idNdermarrje)
        {
            DataTable tmpObject;
            bool sukses = DbCore.mySessionObjects.merrGrideNgaSessioni(Session, out tmpObject);
            if (!sukses)
                mbushGridTatimeNgaDB(idNdermarrje);
            else
            {
                gvTatimet.DataSource = tmpObject;
                gvTatimet.DataBind();
                tmpObject.Dispose();
            }
        }

        /// <summary>
        /// mbush griden nga databaza
        /// </summary>
        /// <param name="idNdermarrje"></param>
        private void mbushGridTatimeNgaDB(int idNdermarrje)
        {//mbush griden e popupit me te dhena            
            DataTable dt = DbCore.DbListPagesat.colTatimet.merrTatimeNdermarjeDTSipasDates(idNdermarrje, Convert.ToDateTime(cmbNdryshimi.Value));
            DbCore.mySessionObjects.ruajGrideNeSession(Session, dt);
            gvTatimet.DataSource = dt;
            gvTatimet.DataBind();
            dt.Dispose();
        }

        /// <summary>
        /// ruan tatimet  
        /// </summary>
        private void ruajTatim()
        {

            if (Page.IsValid == false)
                return;
            else
            {

                if (isValidTatim())
                {
                    int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                    DbCore.DbListPagesat.clsTatime tatime = krijoTatim(idNdermarrje);
                    DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                    bool eshteShtim;
                    DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

                    tedrejtaInfo.merrTeDrejtaPerKeteKomponente(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "Shto_Tatime.aspx");
                    if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim")
                    {
                        if (!tedrejtaInfo.DShtim)
                        {
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                            hfStatusi.Value = "false";
                            return;
                        }
                        mesazh = tatime.ruaj();
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
                        DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                        konf.mbushKonfigAmbjSipasId(tatime.IdKonfig);
                        tatime.IdTatime = int.Parse(hfId.Value);
                        DbCore.DbListPagesat.clsTatime tativjeter = new DbCore.DbListPagesat.clsTatime(tatime.IdTatime);
                        tatime.Model = tativjeter.Model;
                        bool lidhur = dbRegjistrim.eshteDokumentiILidhurCelje(tatime.IdTatime.ToString(), konf.IdNivel.ToString());
                        if (lidhur.ToString() != hfLidhur.Value)
                        {
                            mesazh.Status = false;
                            mesazh.PershkrimMesazhi = ilidhur;
                        }
                        else
                            mesazh = tatime.modifiko();
                        eshteShtim = false;
                        dbRegjistrim.Dispose();
                    }
                    if (mesazh.Status == true)
                    {
                        clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                        if (eshteShtim)
                        {

                            ConfigureAspxComboBox.mbushComboDataTatime(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), cmbNdryshimi);
                            mbushGridTatimeNgaDB(idNdermarrje);
                            cmbNdryshimi_pnlNdryshimi.Update();
                        }
                        else //modifikim
                            mbushGridTatimeNgaDB(idNdermarrje);
                        hfStatusi.Value = "true";
                        ASPxPageControl1.ActiveTabIndex = 0;
                    }
                    else
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                        hfStatusi.Value = "false";
                    }

                }

            } pnlMesazhi.Update();
        }

        /// <summary>
        /// krijon tatimin sipas te dhenave
        /// </summary>
        /// <returns> tatimin me te dhenat</returns>
        /// <param name="idNdermarrje"></param>
        private DbCore.DbListPagesat.clsTatime krijoTatim(int idNdermarrje)
        {
            DbCore.DbShare.clsKonfigurimAmbjenti konfig = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konfig.mbushKonfigAmbjSipasKod(cmbKonfigurimi.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            DbCore.DbListPagesat.clsTatime tatim = new DbCore.DbListPagesat.clsTatime(0, dteDateAkt.Date, Convert.ToDecimal(txtMin.Text), Convert.ToDecimal(txtMax.Text), txtNorma.Text, Convert.ToInt32(cmbMenyra.Value), Convert.ToInt32(DbCore.DbListPagesat.Model.NgaPerdoruesi), idperdoruesi, idNdermarrje, konfig.IdKonfigAmbjente, idstatusdok);
            return tatim;
        }

        /// <summary>
        /// kontrollon nese te dhenat qe jane plotesuara jane te lejueshme apo jo
        /// </summary>
        /// <returns> true ose false</returns>
        private bool isValidTatim()
        {
            //if (hfModel.Value == "1")
            //{
            //    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, kompdefault, pnlMesazhi);
            //    hfStatusi.Value = "false";
            //    return false;
            //}
            if (dteDateAkt.Text == "")
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhaktivizimi, pnlMesazhi);
                hfStatusi.Value = "false";
                return false;
            }
            if (Convert.ToDecimal(txtMin.Text) > Convert.ToDecimal(txtMax.Text))
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, gabimmin, pnlMesazhi);
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
        protected void gvTatimet_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gvTatimet.PageIndex;
            e.Properties["cpPageRow"] = gvTatimet.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gvTatimet.VisibleRowCount;
        }

        /// <summary>
        /// kur grida ben callback
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvTatimet_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string idkomponente = "";
            string kodkonfigurimi = "";

            string[] arr = e.Parameters.Split(';');

            if (arr.Length == 3)
            {
                kodkonfigurimi = arr[1];
                idkomponente = arr[0];
                if (arr[2] == "")
                    gvTatimet.FilterExpression = "";

                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    //filtra.mbushFiltraGridaSipasFiltraKodi(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvTatimet", "Shto_Tatime.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), int.Parse(cmbKonfigurimi.Value.ToString()));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        gvTatimet.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvTatimet);
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
            if (e.Parameters == "ndryshodate")
            {
                mbushGridTatimeNgaDB(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            }
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            if (cmbFiltra.Text == "")
            {

                gvTatimet.FilterExpression = "";

            }
            gvTatimet.Selection.UnselectAll();

        }

        /// <summary>
        /// per filtrimin me elementin bosh
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvTatimet_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {

        }
    }
}