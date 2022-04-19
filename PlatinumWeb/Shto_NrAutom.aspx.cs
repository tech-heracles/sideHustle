using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Collections;
using System.Drawing;
using DbCore.DbAdmin;
using System.Data;
using System.Web.Script.Serialization;
using PlatinumWeb.Templates;
using System.Globalization;
using System.Resources;
using DbCore.IMBUtils.Types;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{
    public partial class Shto_NrAutom : MyPageBase
    {
        //private DbCore.DbAdmin.clsDatabaseAdmin dbAdmin;
        private const string prefixMesazhNjejes = "Numri automatik me kod: ";
        private const string prefixMesazhShumes = "Numrat automatik me kod: ";
        private const string suffixMesazhNjejesGabimi = " nuk mund të fshihet sepse është i lidhur!";
        private const string suffixMesazhShumesGabimi = " nuk mund të fshihen sepse janë të lidhur!";
        private const string suffixMesazhNjejesSuksesi = " u fshi me sukses!";
        private const string suffixMesazhShumesSuksesi = " u fshinë me sukses!";
        private const string lidhesMesazhi = ". Kurse ";
        private const string mesazhZgjidhniNje = "Ju lutem zgjidhni të paktën një numër automatik!";
        private int idviti, idNdermarrje, idPerdoruesi, idgjuha, idNdermVit;

        //DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();
        protected void Page_Init(object sender, EventArgs e)
        {
            //konfiguroVleraFillestare();
            //konfiguroGrid_NrFunditAutomatik();
            //mbushListeNrFunditAutomatik();
        }
        private string komponente = "Shto_NrAutom.aspx";
        private string guidString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                DbCore.clsFunksione.logout(Session,true,"FaqePaautorizuar");
            }
            idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerdoruesi);
            }
            idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            idNdermVit = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
            idviti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            idgjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            percaktoTemplateMenu(idgjuha, idviti, idPerdoruesi, idNdermarrje, ASPxMenu1);

            if (!Page.IsPostBack)
            {
                CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
                ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                guidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                hfState.Set("guidString", guidString);
                EmrateTabeve(rm, ci);
                hfState.Set("idGjuha", idgjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                konfiguroVleraFillestare(idNdermVit, idPerdoruesi, idNdermarrje, rm, ci, idgjuha);
                ASPxPageControl1.ActiveTabIndex = 0;
                mbushGridNumrashNgaDB();
                konfiguroGride(cmbKonfigurimi.Text.Split(';')[0], 128);
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), "ASPxGridView_Numrat", ASPxGridView_Numrat, cmbKonfigurimi.Text.Split(';')[0], 128.ToString(), DbCore.mySessionObjects.ktheGjuhe(Session));
                int idNrAuto = Convert.ToInt32(ASPxGridView_Numrat.GetRowValues(ASPxGridView_Numrat.FocusedRowIndex, "IdNrAutom"));
                mbushListeNrFunditAutomatik(idNrAuto);
                konfiguroGrid_NrFunditAutomatik();
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), komponente);
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
            }
            else
            {
                guidString = (string)hfState["guidString"];
                mbushGridNumrashNgaSession();
                konfiguroGride(cmbKonfigurimi.Text.Split(';')[0], 128);
                //konfiguroVleraFillestare();
            }
            GridUtil.konfigGrideListeEMadhePaTheme(ASPxGridView_Numrat, "IdNrAutom");
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "ASPxGridView_Numrat", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
            //int idNr = Convert.ToInt32(ASPxGridView_Numrat.GetRowValues(ASPxGridView_Numrat.FocusedRowIndex, "IdNrAutom"));
            //clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "", pnlMesazhi);
            percaktoTemplate();
            GridUtil.EmrateButonaveMbiGride(ASPxGridView_Numrat);
        }

        /// <summary>
        /// Vendos emrat e tabeve ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateTabeve(ResourceManager rm, CultureInfo ci)
        {
            ASPxPageControl1.TabPages[0].Text = rm.GetString("TePergjithshmeTab", ci);
            ASPxPageControl1.TabPages[1].Text = rm.GetString("karakteristikatTab", ci);
            ASPxPageControl1.TabPages[2].Text = rm.GetString("alokimiTab", ci);
            ASPxPageControl1.TabPages[3].Text = rm.GetString("numriFunditTab", ci);
        }


        private void mbushGridNumrashNgaDB()
        {
            int idndermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DataTable dt = DbCore.DbAdmin.colNrAutom.mbushNrAutomTeNdermarrjes(idndermarje);

            DbCore.mySessionObjects.ruajGrideNeSession(Session, dt);
            ASPxGridView_Numrat.DataSource = dt;
            ASPxGridView_Numrat.DataBind();
            dt.Dispose();
        }

        private void mbushGridNumrashNgaSession()
        {
            DataTable tmpObject;
            bool sukses = DbCore.mySessionObjects.merrGrideNgaSessioni(Session, out tmpObject);
            if (!sukses)
                mbushGridNumrashNgaDB();
            else
            {
                ASPxGridView_Numrat.DataSource = tmpObject;
                ASPxGridView_Numrat.DataBind();
                tmpObject.Dispose();
            }
        }

        ///// <summary>
        /////      mbush combon e filtrave
        ///// </summary>
        //private void mbushComboBoxFiltra()
        //{
        //    DbCore.DbAdmin.clsGridaKoka koka = new clsGridaKoka("ASPxGridView_Numrat", "Shto_NrAutom.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //    //DbCore.DbAdmin.clsGridaKoka koka = dbAdmin.merrGridaKokaByEmri("ASPxGridView_Perdoruesit", "Shto_Perdorues.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //    DbCore.DbAdmin.colFiltratGrida colFiltra = new colFiltratGrida(koka.IdGridaKoka, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //    colFiltra.Insert(0, new DbCore.DbAdmin.clsFiltraGrida());//colFiltra = dbAdmin.merrGjitheFiltratGridaByGridaKoka(koka.IdGridaKoka, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //    MenuFilter.colekstioni = colFiltra;
        //    MenuFilter.ValueField = "IdFiltra";
        //    MenuFilter.TextField = "FiltraShenime";
        //}

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
            DbCore.clsMesazh mesazh = GridUtil.ruajFiltra(idNdermarrje, idPerdoruesi, idgjuha, "ASPxGridView_Numrat",komponente, "FilterDefault", ASPxGridView_Numrat.FilterExpression, ASPxGridView_Numrat, "KodiNrAutom", idkonf, out idfiltri);//ruan filtrin e zgjedhur tek filtrat
            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                return;

            }
            mesazh = GridUtil.ruajkonfigurimgride(ASPxGridView_Numrat, cmbKonfigurimi.Text, idNdermarrje, idPerdoruesi, 128, idfiltri, idviti, DbCore.mySessionObjects.ktheCultureInfo(Session), DbCore.mySessionObjects.ktheGjuhe(Session));/// ruan konfigurimin e grides dhe filtrin

            clsToolbarConfig.mbushComboBoxFiltra(idgjuha, idNdermarrje, "ASPxGridView_Numrat", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);

            percaktoTemplateMenu(idgjuha, idviti, idPerdoruesi, idNdermarrje, ASPxMenu1);

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
            percaktoTemplateMenu(idgjuha, idviti, idPerdoruesi, idNdermarrje, ASPxMenu1);

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
            //filtra.mbushFiltraGridaSipasFiltraEmri(cmbFiltra.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "ASPxGridView_Numrat", komponente, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            //DbCore.DbAdmin.clsFiltraGrida filtra = dbAdmin.merrFiltraGridaSipasFiltraEmri(cmbFiltra.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();
                //mbushComboBoxFiltra();
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "ASPxGridView_Numrat", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
                percaktoTemplateMenu(idgjuha, idviti, idPerdoruesi, idNdermarrje, ASPxMenu1);

                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                // konfiguroVleraFillestare();
                hfStatusi.Value = "true";
                ASPxGridView_Numrat.FilterExpression = String.Empty;
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
            //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            DbCore.DbAdmin.clsFiltraGrida filtri = new DbCore.DbAdmin.clsFiltraGrida();
            filtri.FiltraKodi = cmbFiltra.Text;
            filtri.FiltraShenime = cmbFiltra.Text;
            filtri.FiltraUniversal = false;// Universal_ASPxCheckBox.Checked;
            //DbCore.DbAdmin.clsGridaKoka koka = dbAdmin.merrGridaKokaByEmri("ASPxGridView_Llogarite", "Shto_Llogari.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "ASPxGridView_Numrat", komponente, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = ASPxGridView_Numrat.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("KodiNrAutom", ASPxGridView_Numrat);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = ASPxGridView_Numrat.GetSortedColumns();
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
            //    filtri.KoloneRenditje = "KodiNrAutom";
            //    filtri.DrejtimRenditje = true;
            //}
            //DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();
            //oPerdorues = (DbCore.DbAdmin.clsPerdorues)(CacheLayer.GlobalCacheManager.MySessionCache["oClsPerdoruesi"]);
            int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            filtri.IdPerdoruesi = idPerdoruesi;
            filtri.IdNdermarje = idNdermarrje;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            filtri.IdStatusDok = 1;

            mesazh = filtri.ruaj();
            //mbushComboBoxFiltra();
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "ASPxGridView_Numrat", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
            percaktoTemplateMenu(idgjuha, idviti, idPerdoruesi, idNdermarrje, ASPxMenu1);

            if (mesazh.Status == true)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            hfStatusi.Value = "true";
            cmbFiltra.Text = "";
        }

        private void konfiguroVleraFillestare(int idNdermVit, int idPerdoruesi, int idNdermarrje, ResourceManager rm, CultureInfo ci, int idGjuha)
        {
            inicializoObjekte();

            AspxWebControlUtils.vendosDateEditMask(nga_data_DateEdit);
            AspxWebControlUtils.vendosDateEditMask(deri_me_DateEdit);
            txtLajmeroPerpara.Text = "0";
            mbushMeTeDhenaComboBoxet(idNdermVit);
            kategoria_combobox.SelectedIndex = -1;
            //llojperiudha_combobox.Enabled = false;
            ASPxPageControl1.ActiveTabIndex = 0;
            ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 33, rm, ci, idGjuha);
            cmbKonfigurimi.SelectedIndex = 0;

            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()), idGjuha);
            hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;
            //cmbKonfigurimi.SelectedIndex = -1;
        }

        private void inicializoObjekte()
        {
            //dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
        }
 
        /*private void shtoKategoriDok()
        {
            ASPxGridView_Numrat.Columns.Remove(ASPxGridView_Numrat.Columns["IdKategori"]);
            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            DbCore.DbRegjistrim.clsKategoriNivelDok oKategori = new DbCore.DbRegjistrim.clsKategoriNivelDok();
            DbCore.DbRegjistrim.colKategoriNiveleDok colKategori = new DbCore.DbRegjistrim.colKategoriNiveleDok();
            colKategori.Add(new DbCore.DbRegjistrim.clsKategoriNivelDok(-1, "", 0, 1));
            colKategori.AddRange(oKategori.merriTeGjithePa(1)); //nuk e marr parasysh ne SP ndermarjen mqs kategorite nuk jane ne nivel ndermarje

            colnew.PropertiesComboBox.DataSource = colKategori;
            colnew.PropertiesComboBox.TextField = "Pershkrimi";
            colnew.PropertiesComboBox.ValueField = "IdKategori";
            colnew.FieldName = "IdKategori";
            ASPxGridView_Numrat.Columns.Add(colnew);
        }*/

        private void shtoKolonaCombo()
        {
            KonfigurimComboGride.shtoKoloneKategoria(ASPxGridView_Numrat,  Session, komponente, guidString);
            KonfigurimComboGride.shtoKoloneDrejtimi(ASPxGridView_Numrat, Session, komponente, guidString);
            KonfigurimComboGride.shtoKoloneLlojPeriudhe(ASPxGridView_Numrat, Session, komponente, guidString);
        }


        private void konfiguroGride(string kodKonfigurimi, int idKomponente)
        {
            shtoKolonaCombo();

            ASPxGridView_Numrat.SettingsEditing.NewItemRowPosition = DevExpress.Web.GridViewNewItemRowPosition.Top;

            ASPxGridView_Numrat.Columns["#"].VisibleIndex = 0;
        }

        private void mbushMeTeDhenaComboBoxet(int idNdermVit)
        {
            ConfigureAspxComboBox.mbushMeTeDhenaKategoriNrAuto(kategoria_combobox);
            //new DbCore.clsFunksione().mbushMeTeDhenaLlojPeriudhe(llojperiudha_combobox, 3);
            ConfigureAspxComboBox.mbushMeTeDhenaDrejtimi(drejtimi_combobox);
        }

        protected void llojperiudha_combobox_Callback(object source, CallbackEventArgsBase e)
        {
            ConfigureAspxComboBox.mbushMeTeDhenaLlojPeriudhe(llojperiudha_combobox, Convert.ToInt32(e.Parameter));
            llojperiudha_combobox.SelectedIndex = -1;
        }

        protected void ruaj_Button_Click(object sender, EventArgs e)
        {
            ruajNumerAutomatik();
        }

        /// <summary>
        /// metoda per te thirrur veprimet e menuse kur shtypen butonat
        /// </summary>
        /// <param name="source">derguesi</param>
        /// <param name="e">parametrat</param>
        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {
            if (e.Item.Name == "Ruaj")
            {
                Page.Validate("entries"); ruajNumerAutomatik();
            }
        }

        /// <summary>
        /// perdoret per te fshire reshtat e zgjedhur ne gride pasi perdoruesi ka konfirmuar fshirjen
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            List<object> rreshtat;
            if (ASPxPageControl1.ActiveTabIndex == 0)//nqs kemi zgjedhur vetem nje kartele per te pare fshijme vetem ate
                rreshtat = ASPxGridView_Numrat.GetSelectedFieldValues("IdNrAutom");
            else
            {
                rreshtat = new List<object>();
                rreshtat.Add(hfId.Value);
            }
            //List<object> rreshtat = this.ASPxGridView_Numrat.GetSelectedFieldValues("IdNrAutom");
            if (rreshtat.Count == 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhZgjidhniNje, pnlMesazhi);
                return;
            }
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            //clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "", pnlMesazhi);
            List<string> TeFshire = new List<string>(), TePaFshire = new List<string>();

            foreach (object id in rreshtat)
            {
                int idNrAuto = Convert.ToInt32(id);
                DbCore.DbAdmin.clsNrAutom nrAutom = new DbCore.DbAdmin.clsNrAutom(idNrAuto);
                if (DbCore.DbAdmin.clsNrAutom.EshteNrAutoILidhur(idNrAuto))
                {
                    TePaFshire.Add(nrAutom.KodiNrAutom);
                    continue;
                }
                mesazh = nrAutom.fshi();
                if (mesazh.Status)
                {
                    hiqNrAutomNgaGrida(nrAutom.IdNrAutom);
                    TeFshire.Add(nrAutom.KodiNrAutom);

                    ASPxPageControl1.ActiveTabIndex = 0;
                    hfStatusi.Value = "true";
                }
                else
                {
                    TePaFshire.Add(nrAutom.KodiNrAutom);
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
        }

        //sherben per te ruajtur nje numer automatik
        private void ruajNumerAutomatik()
        {
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            DbCore.DbAdmin.clsNrAutom nr = new clsNrAutom();
            if (!Page.IsValid)
                return;
            else
            {
                int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                if (isValid())
                {
                    bool eshteShtim;
                    try
                    {
                        nr = krijoNumerAutomatik(idPerdoruesi);
                    }
                    catch (DbCore.MyException myEx)
                    {
                        NLog.LogManager.GetCurrentClassLogger().Error(myEx.Message);
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, myEx.Message, pnlMesazhi);
                        hfStatusi.Value = "false";                      
                        return;
                    }
                    catch (Exception err)
                    {
                        string mesazhi = "Ndodhi nje gabim gjate krijimit te numrit automatik!";
                        NLog.LogManager.GetCurrentClassLogger().Error(mesazhi, err.Message);
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhi, pnlMesazhi);
                        hfStatusi.Value = "false";        
                        return;
                    }
                    DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                    tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), komponente);
                    if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim")
                    {
                        if (!tedrejtaInfo.DShtim)
                        {
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                            hfStatusi.Value = "false";
                            return;
                        }
                        mesazh = nr.ruaj();
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
                        nr.IdNrAutom = int.Parse(hfId.Value.ToString());
                        if (DbCore.DbAdmin.clsNrAutom.EshteNrAutoILidhur(nr.IdNrAutom))
                        {
                            clsNrAutom nrvjeter = new clsNrAutom(nr.IdNrAutom);
                            if (nrvjeter.KategoriaNrAutom != nr.KategoriaNrAutom)
                            {
                                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Nuk mund te ndryshoni kategorine e numrit automatik sepse eshte i lidhur", pnlMesazhi);
                                hfStatusi.Value = "false";
                                return;
                            }
                        }
                        mesazh = nr.modifiko();
                        eshteShtim = false;
                    }
                    if (mesazh.Status == true)
                    {
                        clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["mesazhRuajtjeMeSukses"], pnlMesazhi);
                        hfStatusi.Value = "true";
                        if (eshteShtim)
                            shtoNumerNeGrid(nr.IdNrAutom);
                        else //modifikim
                            modifikoNumerNeGrid(nr.IdNrAutom);
                        konfiguroGride(cmbKonfigurimi.Text.Split(';')[0], 128);
                    }
                    else
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ndodhi nje gabim. Ruajtja nuk u krye!", pnlMesazhi);
                        hfStatusi.Value = "false";
                    }
                    ASPxPageControl1.ActiveTabIndex = 0;
                }
                else
                {
                    hfStatusi.Value = "false";
                }
            }
        }

        private void shtoNumerNeGrid(int idNrAutomatik)
        {
            if (ASPxGridView_Numrat.DataSource != null)
            {
                DataTable dt = (DataTable)ASPxGridView_Numrat.DataSource;
                DataRow[] drs = dt.Select("IdNrAutom = " + idNrAutomatik);
                if (drs.Length > 0)
                    throw new Exception("GABIM: Numri automatik ekziston ne gride");
                DataRow newArtDr = DbCore.DbAdmin.clsNrAutom.merrNrAutoSipasID(idNrAutomatik);
                dt.ImportRow(newArtDr);
                dt.Rows[dt.Rows.Count - 1].ItemArray = newArtDr.ItemArray;
                //clsFunksione.ruajGrideNeSession(Session, dt);
            }
            else mbushGridNumrashNgaDB();
        }

        private void modifikoNumerNeGrid(int idNrAutomatik)
        {
            if (ASPxGridView_Numrat.DataSource != null)
            {
                DataTable dt = (DataTable)ASPxGridView_Numrat.DataSource;
                DataRow[] drs = dt.Select("IdNrAutom = " + idNrAutomatik);
                if (drs.Length > 1)
                    throw new Exception("GABIM: Ndodhen 2 numra automatik me te njejten id ne gride");
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                DataRow newArtDr = DbCore.DbAdmin.clsNrAutom.merrNrAutoSipasID(idNrAutomatik);
                //  dt.Rows.Remove(dr);
                // dt.ImportRow(newArtDr);
                object[] arr = newArtDr.ItemArray;
                dr.ItemArray = arr;
            }
            else mbushGridNumrashNgaDB();
        }

        private void hiqNrAutomNgaGrida(int idNrAutom)
        {
            if (this.ASPxGridView_Numrat.DataSource != null)
            {
                DataTable dt = (DataTable)ASPxGridView_Numrat.DataSource;
                DataRow[] drs = dt.Select("IdNrAutom = " + idNrAutom);
                if (drs.Length > 1)
                    throw new Exception("GABIM: Ndodhen 2 numra automatik me te njejten id ne gride");
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                dt.Rows.Remove(dr);
                ASPxGridView_Numrat.DataBind();
            }
            else mbushGridNumrashNgaDB();
        }

        private bool isValid()
        {
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            if (!String.IsNullOrEmpty(txtLajmeroPerpara.Text) && Convert.ToInt32(txtLajmeroPerpara.Text) < 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Lajmero perpara duhet te plotesohet me nje numer pozitiv!", pnlMesazhi);
                return false;
            }
            if ((hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim") && clsNrAutom.ekzistonNrAuto(this.kodi_TextBox.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session)))
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ekziston nje nr automatik me kete kod. Ju lutem zgjidhni nje kod tjeter!", pnlMesazhi);
                return false;
            }
            if (hfShtimModifikim.Value == "modifikim" && Convert.ToInt32(txtIntervali.Text) == 0 && clsNrAutom.eshteLidhurMeAtributeTrupiPerMobile(Convert.ToInt32(hfId.Value)))
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ky numer automatik perdoret ne AlphaWeb Mobile. Ju lutem, plotesoni fushen intervali!", pnlMesazhi);
                return false;
            }

            if (fillon_TextBox.Text != "" && mbaron_TextBox.Text != "")
            {

                if (Convert.ToInt32(fillon_TextBox.Text) > Convert.ToInt32(mbaron_TextBox.Text) && int.Parse(drejtimi_combobox.Value.ToString()) == 0) //rrites
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Numri automatik i fillimit nuk duhet te jete me i madh se numri i mbarimit!", pnlMesazhi);
                    return false;
                }

                if (Convert.ToInt32(fillon_TextBox.Text) < Convert.ToInt32(mbaron_TextBox.Text) && int.Parse(drejtimi_combobox.Value.ToString()) == 1) //zbrites
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Numri automatik i fillimit nuk duhet te jete me i vogel se numri i mbarimit!", pnlMesazhi);
                    return false;
                }
            }

            if (!String.IsNullOrEmpty(deri_me_DateEdit.Text) && nga_data_DateEdit.Date > deri_me_DateEdit.Date)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("lblVendosjeDateGabim", ci),pnlMesazhi);
                return false;
            }
            return true;
        }

        //pastron gjithe textboxet
        private void pastroFusha()
        {
            drejtimi_combobox.SelectedIndex = -1;
            kodi_TextBox.Text = "";
            emertimi_TextBox.Text = "";
            txtLajmeroPerpara.Text = "0";
            fillon_TextBox.Text = "";
            mbaron_TextBox.Text = "";
            hapi_TextBox.Text = "";
            txtIntervali.Text = "";
            nga_data_DateEdit.Date = new DateTime();
            deri_me_DateEdit.Date = new DateTime();
            majtas_TextBox.Text = "";
            djathtas_TextBox.Text = "";
            kategoria_combobox.SelectedIndex = -1;
            llojperiudha_combobox.SelectedIndex = -1;
            gjatesia_TextBox.Text = "";
            ASPxGridView_Numrat.AddNewRow();
            this.HiddenField1.Value = "";
        }

        private DbCore.DbAdmin.clsNrAutom krijoNumerAutomatik(int idPerdoruesi)
        {
            DbCore.DbAdmin.clsNrAutom nr = new DbCore.DbAdmin.clsNrAutom();
            nr.KodiNrAutom = kodi_TextBox.Text;
            nr.EmertimiNrAutom = emertimi_TextBox.Text;
            if (fillon_TextBox.Text != String.Empty)
                nr.FillonNrAutom = Convert.ToInt64(fillon_TextBox.Text);
            else
                nr.FillonNrAutom = 0;
            if (mbaron_TextBox.Text != String.Empty)
                nr.MbaronNrAutom = Convert.ToInt64(mbaron_TextBox.Text);
            else 
                nr.MbaronNrAutom = 0;
            nr.HapiNrAutom = int.Parse(hapi_TextBox.Text);
            if (txtIntervali.Text != "")

                nr.Interval = int.Parse(txtIntervali.Text);
            else nr.Interval = 0;
            nr.DrejtimiNrAutom = int.Parse(drejtimi_combobox.Value.ToString());
            nr.NgaDataNrAutom = nga_data_DateEdit.Date;
            if (deri_me_DateEdit.Text != "")
                nr.DeriMeNrAutom = deri_me_DateEdit.Date;
            else
                nr.DeriMeNrAutom = DateTime.MaxValue;
            nr.MajtasNrAutom = majtas_TextBox.Text;
            nr.DjathtasNrAutom = djathtas_TextBox.Text;
            nr.KategoriaNrAutom = nr.PeriudhaNrAutom = int.Parse(kategoria_combobox.Value.ToString());
            nr.PeriudhaNrAutom = int.Parse(llojperiudha_combobox.Value.ToString());
            if (gjatesia_TextBox.Text != String.Empty)
                nr.GjatesiaNrAutom = Convert.ToInt32(gjatesia_TextBox.Text);
            else
                nr.GjatesiaNrAutom = 0;
            nr.IdStatusDok = 1;
            nr.IdNdermarja = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            nr.IdPerdoruesi = idPerdoruesi;
            nr.LajmeroPerparaNrFundit = txtLajmeroPerpara.Text == "" ? 0 : Convert.ToInt32(txtLajmeroPerpara.Text);
            nr.OColNrAutoFundit = merrNrFundit();
            return nr;
        }

        private colNrAutomatikFundit merrNrFundit()
        {
            DataTable dt = DbCore.DbAdmin.colNrAutomatikFundit.merrGjithNrAutoFunditSipasNrAuto(int.Parse(hfId.Value.ToString()));
            JavaScriptSerializer serializusi = new JavaScriptSerializer();
            object[] arr = (object[])serializusi.DeserializeObject(hfVlerat.Value);
            colNrAutomatikFundit col = new colNrAutomatikFundit();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                clsNrAutomatikFundit nrfund = new clsNrAutomatikFundit();
                nrfund.Data = (DateTime)dt.Rows[i]["Data"];
                nrfund.IdNdermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                nrfund.IdNrAutom = int.Parse(hfId.Value.ToString());
                nrfund.IdNrFunditAutomatik = int.Parse(dt.Rows[i]["IdNrFunditAutomatik"].ToString());
                nrfund.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                nrfund.IdStatusDok = 1;
                if (string.IsNullOrWhiteSpace((string)arr[i]))
                    throw new DbCore.MyException("Vlera e numrit te fundit automatik nuk mund te jete bosh!");
                nrfund.Vlera = (string)arr[i];
                col.Add(nrfund);
            }
            return col;
        }

        //thirret sa here qe behet Callback, dhe ben edhe njeher lidhjen me datasource-in
        protected void ASPxGridView_Numrat_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {

            if (e.CallbackName == "COLUMNMOVE" && ASPxGridView_Numrat.AllColumns[int.Parse(e.Args[0])].Width.Value == 0)
                ASPxGridView_Numrat.AllColumns[int.Parse(e.Args[0])].Width = Unit.Percentage(3);
            if (e.CallbackName == "APPLYFILTER" && e.Args[0] == "")
            {
                DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
                ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
                cmbFiltra.Text = "";


            }
        }

        //behen konfigurime nga ana vizuale per griden


        /// <summary>
        /// perdoret per te rikonfiguruar griden kur ndryshohet konfigurimi
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxGridView_Numrat_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string idkomponente = "";
            string kodkonfigurimi = "";

            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                kodkonfigurimi = arr[1];
                idkomponente = arr[0];
                if (arr[2] == "")
                    ASPxGridView_Numrat.FilterExpression = "";
                else
                {
                    //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                    //filtra.mbushFiltraGridaSipasFiltraKodi(arr[2], idNdermarrje);
                    int idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuha, "ASPxGridView_Numrat", komponente, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), int.Parse(cmbKonfigurimi.Value.ToString()));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
                    //DbCore.DbAdmin.clsFiltraGrida filtra = dbAdmin.merrFiltraGridaSipasFiltraKodi(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    if (filtra.FiltraKodi != null)
                    {
                        this.ASPxGridView_Numrat.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, ASPxGridView_Numrat);

                        int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                        System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
                        System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                        konfiguroVleraFillestare(DbCore.mySessionObjects.ktheNdermarrjeVit(Session), idPerdoruesi, idNdermarrje, rm, ci, idGjuha);
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
            //  konfiguroGride();
            //  funksion.percaktoVisibleColumnsGridSipasKodKonfigurimi(grid_ListPerdoruesit, kodkonfigurimi, idkomponente);
            ASPxGridView_Numrat.Selection.UnselectAll();

        }

        /// <summary>
        /// ruan disa karakteristika te grides
        /// </summary>
        /// <param name="sender">dergues</param>
        /// <param name="e">argumentat</param>
        protected void ASPxGridView_Numrat_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = ASPxGridView_Numrat.PageIndex;
            e.Properties["cpPageRow"] = ASPxGridView_Numrat.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = ASPxGridView_Numrat.VisibleRowCount;
        }

        protected void ASPxGridView_Numrat_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
        }

        /// <summary>
        ///   sherben per te vendosur filtra tek header-i i grides (mund te ndryshohet me vone per tu
        ///bere  me e konfigurueshme
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ASPxGridView_Numrat_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {

            if (e.Column.FieldName == "KodiNrAutom")
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

        protected void ASPxGridView_Numrat_DataBound(object sender, EventArgs e)
        {
            if (ASPxGridView_Numrat.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per perzgjidh
                DevExpress.Web.GridViewCommandColumn check = new DevExpress.Web.GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true; check.Width = Unit.Percentage(2);
                //behet per te afishuar rreshtin qe do sherbej per filtrim
                ASPxGridView_Numrat.Settings.ShowFilterRow = true;
                ASPxGridView_Numrat.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                ASPxGridView_Numrat.Settings.ShowFilterRowMenu = true;
                ASPxGridView_Numrat.Columns.Add(check);
                ASPxGridView_Numrat.KeyFieldName = "IdNrAutom";
                ASPxGridView_Numrat.SettingsBehavior.AllowSelectByRowClick = true;
                ASPxGridView_Numrat.SettingsBehavior.AllowFocusedRow = true;
            }
        }

        protected void ASPxGridView_Numrat_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if (e.Column.FieldName == "DrejtimiNrAutom" || e.Column.FieldName == "PeriudhaNrAutom" || e.Column.FieldName == "KategoriaNrAutom")
            {
                if (Converter.ConvertToInt(e.Value) == -3)
                {
                    e.Criteria = null;
                }
            }
        }

        private void percaktoTemplate()
        {
            GridViewDataTextColumn col1 = grid_NrFunditAutomatik.Columns["Vlera"] as GridViewDataTextColumn;
            col1.DataItemTemplate = new MyTextTemplate();

        }

        private void konfiguroGrid_NrFunditAutomatik()
        {
            KonfigurimComboGride.shtoKoloneLlojPeriudhe(grid_NrFunditAutomatik, Session, komponente, guidString);
            GridUtil.percaktoVisibleColumnsMeWidth(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), grid_NrFunditAutomatik, "grid_NrFunditAutomatik", komponente);
            //funk.konfiguroGrideListeMadhe(grid_NrFunditAutomatik, "IdNrFunditAutomatik");
            GridUtil.konfigGrideListeEMadhePaTheme(grid_NrFunditAutomatik, "IdNrFunditAutomatik", false);
            grid_NrFunditAutomatik.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
            grid_NrFunditAutomatik.Columns["PeriudhaNrAutom"].VisibleIndex = 0;
        }

        protected void mbushListeNrFunditAutomatik(int idNrAuto)
        {
            DataTable dt = DbCore.DbAdmin.colNrAutomatikFundit.merrGjithNrAutoFunditSipasNrAuto(idNrAuto);
            grid_NrFunditAutomatik.DataSource = dt;
            grid_NrFunditAutomatik.KeyFieldName = "IdNrFunditAutomatik";
            grid_NrFunditAutomatik.DataBind();
            dt.Dispose();
        }

        protected void grid_NrFunditAutomatik_DataBound(object sender, EventArgs e)
        {

            grid_NrFunditAutomatik.Settings.ShowFilterRow = true;
            grid_NrFunditAutomatik.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
            grid_NrFunditAutomatik.Settings.ShowFilterRowMenu = true;

            grid_NrFunditAutomatik.KeyFieldName = "IdNrFunditAutomatik";
            grid_NrFunditAutomatik.SettingsBehavior.AllowSelectByRowClick = false;
            grid_NrFunditAutomatik.SettingsBehavior.AllowFocusedRow = true;
        }

        protected void grid_NrFunditAutomatik_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            grid_NrFunditAutomatik.Selection.UnselectAll();
            if (e.Parameters != "-1" && e.Parameters != "")
            {
                mbushListeNrFunditAutomatik(Convert.ToInt32(e.Parameters.ToString()));
                KonfigurimComboGride.shtoKoloneLlojPeriudhe(grid_NrFunditAutomatik, Session, komponente, guidString);

            }
            else
            {
                //mbushListeNrFunditAutomatik(Convert.ToInt32(e.Parameters.ToString()));
                grid_NrFunditAutomatik.DataSource = null;
                grid_NrFunditAutomatik.DataBind();
            }

            grid_NrFunditAutomatik.PageIndex = 0;
        }

        protected void grid_NrFunditAutomatik_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if (e.Column.FieldName == "PeriudhaNrAutom")
            {
                if (Converter.ConvertToInt(e.Value) == -3)
                {
                    e.Criteria = null;
                }
            }
        }

        protected void grid_NrFunditAutomatik_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType == DevExpress.Web.GridViewRowType.Data)
            {
                GridViewDataColumn col1 = ((ASPxGridView)sender).Columns["Vlera"] as GridViewDataColumn;


                ASPxTextBox txt1 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col1, "txtBox") as ASPxTextBox;
                if (txt1 != null)
                {
                    txt1.ClientInstanceName = "txtVlera" + e.VisibleIndex.ToString();
                    txt1.ClientSideEvents.TextChanged = "function(s,e){TextChangedVlera(txtVlera" + e.VisibleIndex.ToString() + ",'txtVlera', " + e.VisibleIndex.ToString() + ");}";


                }
            }
        }

        protected void grid_NrFunditAutomatik_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpNoRows"] = grid_NrFunditAutomatik.VisibleRowCount;
        }
    }
}