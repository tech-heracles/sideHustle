using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Collections;
using System.Drawing;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using PlatinumWeb.Templates;
using System.Globalization;
using System.Resources;
using DbCore;
using DbCore.IMBUtils.Logging;
using DbCore.IMBUtils.Types;
using DbCore.IMBUtils.Validation;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{
    public partial class Shto_KategoriZbritje : MyPageBase
    {
        private const string prefixMesazhNjejes = "Kategoria e zbritjes me Kod: ";
        private const string prefixMesazhShumes = "Kategorite e zbritjes me Kod: ";
        private const string suffixMesazhNjejesGabimi = " eshte e lidhur dhe nuk mund te fshihet";
        private const string suffixMesazhShumesGabimi = " jane te lidhur dhe nuk mund te fshihen";
        private const string suffixMesazhNjejesSuksesi = " u fshi me sukses!";
        private const string suffixMesazhShumesSuksesi = " u fshine me sukses!";
        private const string lidhesMesazhi = ". Kurse ";
        private const string mesazhZgjidhniNje = "Ju lutem zgjidhni te pakten nje kategori zbritjeje!";
        //private string koloneFocus;
        private ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
        ASPxTextBox temptxt = null;
        ASPxComboBox tempcombo = null;
        ASPxDateEdit tempcal = null;
        //private DbCore.DbInventari.clsKokaKategoriZbritje KategoriZbritjeOverview;

        public static int idNdermVit = -1;
        private int idgjuha, idviti, idNdermarje, idPerdoruesi;
        private string komponente = "Shto_KategoriZbritje.aspx";
        private string guidString;
        //DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();

        protected void Page_Init(object sender, EventArgs e)
        {
            //mbushListeKategorish();
            //konfiguroGride();
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                DbCore.clsFunksione.logout(Session, true, "FaqePaautorizuar");
            }
            idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);

            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerdoruesi);
            }
            //oPerdorues = (DbCore.DbAdmin.clsPerdorues)(CacheLayer.GlobalCacheManager.MySessionCache["oClsPerdoruesi"]);
            idviti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session); idNdermVit = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
            idNdermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            idgjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            percaktoTemplateMenu(idgjuha, idviti, idPerdoruesi, idNdermarje, ASPxMenu1);
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (!Page.IsPostBack)
            {
                guidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                hfState.Set("guidString", guidString);
                EmrateTabeve(rm, cultinf);
                hfState.Set("idGjuha", idgjuha);
                hfState.Set("idNdermarje", idNdermarje);
                LoadString(hfMsgZbritje, ci);
                ASPxPageControl1.ActiveTabIndex = 0;
                konfiguroVleraFillestare(idPerdoruesi, idNdermarje, rm, cultinf, idgjuha);
                konfiguroGrideNenKategorish();
                mbushGridKategorishNgaDB();
                konfiguroGride(cmbKonfigurimi.Text.Split(';')[0], 417);
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), "gvKokaKategoriZbritje", gvKokaKategoriZbritje, cmbKonfigurimi.Text.Split(';')[0], 417.ToString(), DbCore.mySessionObjects.ktheGjuhe(Session));

                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), komponente);
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
            }
            else
            {
                guidString = (string)hfState["guidString"];
                mbushGridKategorishNgaSession();
                konfiguroGride(cmbKonfigurimi.Text.Split(';')[0], 417);
            }
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarje, "gvKokaKategoriZbritje", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
            GridUtil.konfigGrideListeEMadhePaTheme(gvKokaKategoriZbritje, "IdKokaKategoriZbritje");
            GridUtil.EmrateButonaveMbiGride(gvKokaKategoriZbritje);
            percaktoTemplateNenKategorish();
        }

        /// <summary>
        /// Vendos emrat e tabeve ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateTabeve(ResourceManager rm, CultureInfo cultinf)
        {
            ASPxPageControl1.TabPages[0].Text = rm.GetString("TePergjithshmeTab", cultinf);
            ASPxPageControl1.TabPages[1].Text = rm.GetString("labelBlerjeShitjeNenkategorite", cultinf);
        }

        ///// <summary>
        /////      mbush combon e filtrave
        ///// </summary>
        //private void mbushComboBoxFiltra()
        //{
        //    //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
        //    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka("gvKokaKategoriZbritje", "Shto_KategoriZbritje.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //    //DbCore.DbAdmin.clsGridaKoka koka = dbAdmin.merrGridaKokaByEmri("gvKokaKategoriZbritje", "Shto_KategoriZbritje.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //    DbCore.DbAdmin.colFiltratGrida colFiltra = new DbCore.DbAdmin.colFiltratGrida(koka.IdGridaKoka, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //    colFiltra.Insert(0, new DbCore.DbAdmin.clsFiltraGrida());  //colFiltra = dbAdmin.merrGjitheFiltratGridaByGridaKoka(koka.IdGridaKoka, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
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
            int idkonf = DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(cmbKonfigurimi.Text.Split(';')[0], idNdermarje);
            DbCore.clsMesazh mesazh = GridUtil.ruajFiltra(idNdermarje, idPerdoruesi, idgjuha, "gvKokaKategoriZbritje ", komponente, "FilterDefault", gvKokaKategoriZbritje.FilterExpression, gvKokaKategoriZbritje, "KodKategoriZbritje", idkonf, out idfiltri);//ruan filtrin e zgjedhur tek filtrat
            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                return;

            }
            mesazh = GridUtil.ruajkonfigurimgride(gvKokaKategoriZbritje, cmbKonfigurimi.Text, idNdermarje, idPerdoruesi, 417, idfiltri, idviti, DbCore.mySessionObjects.ktheCultureInfo(Session), DbCore.mySessionObjects.ktheGjuhe(Session));/// ruan konfigurimin e grides dhe filtrin

            clsToolbarConfig.mbushComboBoxFiltra(idgjuha, idNdermarje, "gvKokaKategoriZbritje ", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);

            percaktoTemplateMenu(idgjuha, idviti, idPerdoruesi, idNdermarje, ASPxMenu1);
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
            percaktoTemplateMenu(idgjuha, idviti, idPerdoruesi, idNdermarje, ASPxMenu1);

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
            //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
            //filtra.mbushFiltraGridaSipasFiltraEmri(cmbFiltra.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvKokaKategoriZbritje", komponente, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            //DbCore.DbAdmin.clsFiltraGrida filtra = dbAdmin.merrFiltraGridaSipasFiltraEmri(cmbFiltra.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();
                //mbushComboBoxFiltra();
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvKokaKategoriZbritje", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
                percaktoTemplateMenu(idgjuha, idviti, idPerdoruesi, idNdermarje, ASPxMenu1);
                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                // konfiguroVleraFillestare();
                hfStatusi.Value = "true";
                gvKokaKategoriZbritje.FilterExpression = String.Empty;
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
            //DbCore.DbAdmin.clsGridaKoka koka = dbAdmin.merrGridaKokaByEmri("gvKokaKategoriZbritje", "Shto_KategoriZbritje.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvKokaKategoriZbritje", komponente, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gvKokaKategoriZbritje.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("KodKategoriZbritje", gvKokaKategoriZbritje);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvKokaKategoriZbritje.GetSortedColumns();
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
            //    filtri.KoloneRenditje = "KodKategoriZbritje";
            //    filtri.DrejtimRenditje = true;
            //}
            //DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();
            //oPerdorues = (DbCore.DbAdmin.clsPerdorues)(CacheLayer.GlobalCacheManager.MySessionCache["oClsPerdoruesi"]);
            filtri.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            filtri.IdNdermarje = idNdermarrje;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            filtri.IdStatusDok = 1;

            mesazh = filtri.ruaj();
            //mbushComboBoxFiltra();
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvKokaKategoriZbritje", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
            percaktoTemplateMenu(idgjuha, idviti, idPerdoruesi, idNdermarje, ASPxMenu1);
            if (mesazh.Status == true)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            hfStatusi.Value = "true";
            cmbFiltra.Text = "";

        }

        protected void ButtonOk_Click2(object sender, EventArgs e)
        {//fshin rreshtat e selektuar

            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            List<object> rreshtat;
            if (ASPxPageControl1.ActiveTabIndex == 0)//nqs kemi zgjedhur vetem nje kartele per te pare fshijme vetem ate
                rreshtat = gvKokaKategoriZbritje.GetSelectedFieldValues("IdKokaKategoriZbritje");
            else
            {
                rreshtat = new List<object>();
                rreshtat.Add(hfId.Value);
            }
            if (rreshtat.Count == 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgBlerjeShitjeZgjidhZbritje", ci), pnlMesazhi);
                return;
            }
            List<string> TeFshire = new List<string>(), TePaFshire = new List<string>();
            foreach (object id in rreshtat)
            {
                DbCore.DbInventari.clsKokaKategoriZbritje cls = new DbCore.DbInventari.clsKokaKategoriZbritje(Convert.ToInt32(id));
                DbCore.DbInventari.colTrupatKategoriteZbritjes trupat = new DbCore.DbInventari.colTrupatKategoriteZbritjes();
                trupat.mbushTrupatKategoriZbritjeSipasKokes(Convert.ToInt32(id));
                if (cls.KaVeprime())
                {
                    TePaFshire.Add(cls.KodKategoriZbritje);
                    continue;
                }
                cls.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                DbCore.clsMesazh mesazh = cls.fshi();
                if (cls.IdKokaKategoriZbritje == 0)
                    continue;
                if (mesazh.Status)
                {
                    #region Heq llogarite nga grida

                    // ASPxGridView_Artikull.DataSource = dt;
                    hiqKategoriNgaGrida(cls.IdKokaKategoriZbritje);
                    #endregion
                    TeFshire.Add(cls.KodKategoriZbritje);

                    ASPxPageControl1.ActiveTabIndex = 0;
                    hfStatusi.Value = "true";
                    hfShtimModifikim.Value = "shtim";
                }
            }
            string mesazhInfoGabim = "", mesazhInfoSukses = "";
            if (TePaFshire.Count == 1)
                mesazhInfoGabim = String.Format("{0}{1}{2}", rm.GetString("msgBlerjeShitjeKategoriZbritje", ci), String.Join(";", TePaFshire), rm.GetString("msgBlerjeShitjeNukFshihetZbritje", ci));
            else
                if (TePaFshire.Count > 1)
                mesazhInfoGabim = String.Format("{0}{1}{2}", rm.GetString("msgBlerjeShitjeKategoriteZbritje", ci), String.Join(";", TePaFshire), rm.GetString("msgBlerjeShitjeNukFshihetShumes", ci));
            if (TeFshire.Count == 1)
                mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("msgBlerjeShitjeKategoriZbritje", ci), String.Join(";", TeFshire), rm.GetString("msgBlerjeShitjeFshirjeSuksesNjejes", ci));
            else
                if (TeFshire.Count > 1)
                mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("msgBlerjeShitjeKategoriteZbritje", ci), String.Join(";", TeFshire), rm.GetString("msgBlerjeShitjeFshirjeSuksesShumes", ci));
            if (mesazhInfoGabim != "" && mesazhInfoSukses != "")
                mesazhInfoGabim += rm.GetString("msgBlerjeShitjelidhesKurse", ci) + mesazhInfoSukses;
            if (mesazhInfoGabim != "")
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhInfoGabim, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazhInfoSukses, pnlMesazhi);

        }
        private void hiqKategoriNgaGrida(int idllogari)
        {

            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);

            if (this.gvKokaKategoriZbritje.DataSource != null)
            {
                try
                {

                    DataTable dt = (DataTable)gvKokaKategoriZbritje.DataSource;
                    DataRow[] drs = dt.Select("IdKokaKategoriZbritje = " + idllogari);

                    if (drs.Length > 1)
                        throw new Exception(rm.GetString("msgBlerjeShitjeKategoriIdNjejte", ci));
                    if (drs.Length == 0) return;
                    DataRow dr = drs[0];
                    dt.Rows.Remove(dr);
                    gvKokaKategoriZbritje.DataBind();
                }
                catch (Exception ex)
                {
                    ImbLogger.Error(ex);
                }
            }
            else mbushGridKategorishNgaDB();
        }
        private void shtoKategoriNeGrid(int idNdermarrje, int idPerdorues, int idllogari)
        {
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            if (gvKokaKategoriZbritje.DataSource != null)
            {
                DataTable dt = (DataTable)gvKokaKategoriZbritje.DataSource;
                DataRow[] drs = dt.Select("IdKokaKategoriZbritje = " + idllogari);
                if (drs.Length > 0)
                    throw new Exception(rm.GetString("msgBlerjeShitjeKategoriaEkziston", ci));
                DataRow newArtDr = DbCore.DbInventari.colKokatKategoriteZbritjes.merrSipasKategoriNdermarrjesDR(idNdermarrje, idPerdorues, idllogari);
                dt.ImportRow(newArtDr);
            }
            else mbushGridKategorishNgaDB();
        }
        private void modifikoKategoriNeGrid(int idNdermarrje, int idPerdorues, int idLlogari)
        {
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            if (gvKokaKategoriZbritje.DataSource != null)
            {
                DataTable dt = (DataTable)gvKokaKategoriZbritje.DataSource;
                DataRow[] drs = dt.Select("IdKokaKategoriZbritje = " + idLlogari);
                if (drs.Length > 1)
                    throw new Exception(rm.GetString("msgBlerjeShitjeKategoriIdNjejte", ci));
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                DataRow newArtDr = DbCore.DbInventari.colKokatKategoriteZbritjes.merrSipasKategoriNdermarrjesDR(idNdermarrje, idPerdorues, idLlogari);
                //  dt.Rows.Remove(dr);
                // dt.ImportRow(newArtDr);
                object[] arr = newArtDr.ItemArray;
                dr.ItemArray = arr;
            }
            else mbushGridKategorishNgaDB();
        }

        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {
            if (e.Item.Name == "Ruaj")
            {
                Page.Validate("entries"); ruajKategoriZbritje();
            }
        }
        private void konfiguroVleraFillestare(int idPerdoruesi, int idNdermarrje, ResourceManager rm, CultureInfo cultinf, int idGjuha)
        { //mbush komboboxet dhe gridat e faqes

            inicializoObjekte();
            // mbushListeKategorish();
            mbushListeNenKategorish();
            ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 24, rm, cultinf, idGjuha);
            cmbKonfigurimi.SelectedIndex = 0;
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()), idGjuha);
            hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;
            //cmbKonfigurimi.SelectedIndex = -1;
            ConfigureAspxComboBox.mbushComboMonedha(idPerdoruesi, idNdermarrje, false, cmbMonedha);
        }

        private void konfiguroGrideNenKategorish()
        {//konfigurohet grida
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            GridUtil.percaktoVisibleColumnsMeWidth(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), gvTrupiKategoriZbritje, "gvTrupiKategoriZbritje", komponente);
            GridUtil.konfiguroGrideRegjistrimEvogelPaTheme(gvTrupiKategoriZbritje, "IdTrupiKategoriZbritje");
            gvTrupiKategoriZbritje.Settings.UseFixedTableLayout = false;

        }

        private void inicializoObjekte()
        {
        }
        private void mbushGridKategorishNgaSession()
        {
            DataTable tmpObject;
            bool sukses = DbCore.mySessionObjects.merrGrideNgaSessioni(Session, out tmpObject);
            if (!sukses)
                mbushGridKategorishNgaDB();
            else
            {
                gvKokaKategoriZbritje.DataSource = tmpObject;
                gvKokaKategoriZbritje.DataBind();
                tmpObject.Dispose();
            }
        }
        private void mbushGridKategorishNgaDB()
        {//mbush griden e popupit me te dhena            

            ImbLogger.Info("po merr nga db");
            DataTable dt = DbCore.DbInventari.colKokatKategoriteZbritjes.merrSipasKategoriteNdermarrjesDT(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            DbCore.mySessionObjects.ruajGrideNeSession(Session, dt);
            gvKokaKategoriZbritje.DataSource = dt;
            gvKokaKategoriZbritje.DataBind();
            dt.Dispose();
            ImbLogger.Info("mbaroi marrja");
        }


        private void mbushListeKategorish()
        {//mbush griden me te dhena


            DbCore.DbInventari.colKokatKategoriteZbritjes col = new DbCore.DbInventari.colKokatKategoriteZbritjes(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            //DbCore.DbInventari.colKokatKategoriteZbritjes  col = dbInventari.merrKokaKategoriZbritjeSipasNdermarrjes ((new DbCore.clsFunksione()).ktheIdNdermarrje());
            gvKokaKategoriZbritje.DataSource = col;
            gvKokaKategoriZbritje.DataBind();
        }

        private void mbushListeNenKategorish()
        {//mbushet grida me te dhena            
            DbCore.DbInventari.colTrupatKategoriteZbritjes col = new DbCore.DbInventari.colTrupatKategoriteZbritjes();
            for (int i = 0; i < 5; i++)
            {
                DbCore.DbInventari.clsTrupiKategoriZbritje o = new DbCore.DbInventari.clsTrupiKategoriZbritje();

                col.Add(o);
            }
            gvTrupiKategoriZbritje.DataSource = col;
            gvTrupiKategoriZbritje.DataBind();
        }
        private void mbushListeNenKategorishMod(int id)
        {//mbush griden me te dhenat            
            //DbCore.DbInventari.colTrupatKategoriteZbritjes trupat=dbInventari .merrTrupatKategoriZbritjeSipasKokes (int.Parse(Request.QueryString["id"]));
            DbCore.DbInventari.colTrupatKategoriteZbritjes trupat = new DbCore.DbInventari.colTrupatKategoriteZbritjes();
            trupat.mbushTrupatKategoriZbritjeSipasKokes(id);
            DbCore.DbInventari.clsTrupiKategoriZbritje trupi = new DbCore.DbInventari.clsTrupiKategoriZbritje();


            int i = 0;
            DbCore.DbInventari.clsTrupiKategoriZbritje trupikryesor = new DbCore.DbInventari.clsTrupiKategoriZbritje();
            foreach (DbCore.DbInventari.clsTrupiKategoriZbritje t in trupat)
            {
                if (t.Prioriteti == 0)
                    trupikryesor = t;
                if (t.DateFillimi == DateTime.Parse("01/01/" + DateTime.Today.Year) && t.DateMbarimi.ToShortDateString() == DateTime.MaxValue.ToShortDateString())
                {
                    t.DateFillimi = new DateTime();
                    t.DateMbarimi = new DateTime();
                }

            }
            trupat.Remove(trupikryesor);
            hfNrRreshta.Value = trupat.Count.ToString();
            foreach (DbCore.DbInventari.clsTrupiKategoriZbritje t in trupat)
            {
                hfPrioritetiPara.Value += i + ":" + t.Prioriteti + ",";
                i++;
            }
            trupat.Add(trupi);
            gvTrupiKategoriZbritje.DataSource = trupat;
            gvTrupiKategoriZbritje.DataBind();
        }
        private void percaktoTemplateNenKategorish()
        {//percaktohen templatet per fushat e grides
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            GridViewDataTextColumn col0 = gvTrupiKategoriZbritje.Columns[rm.GetString("labelBlerjeShitjeFshi", ci)] as GridViewDataTextColumn;
            col0.DataItemTemplate = new MyButtonTemplate("");
            col0.VisibleIndex = 0;
            GridViewDataDateColumn col1 = gvTrupiKategoriZbritje.Columns["DateFillimi"] as GridViewDataDateColumn;
            col1.DataItemTemplate = new MyCalendarTemplate();
            GridViewDataDateColumn col2 = gvTrupiKategoriZbritje.Columns["DateMbarimi"] as GridViewDataDateColumn;
            col2.DataItemTemplate = new MyCalendarTemplate();
            GridViewDataTextColumn col3 = gvTrupiKategoriZbritje.Columns["VleraMin"] as GridViewDataTextColumn;
            col3.DataItemTemplate = new MyDoubleTemplate(false, 2, "0"); // "0.00");
            GridViewDataTextColumn col4 = gvTrupiKategoriZbritje.Columns["VleraMax"] as GridViewDataTextColumn;
            col4.DataItemTemplate = new MyDoubleTemplate(false, 2, "0"); // "0.00");
            GridViewDataTextColumn col5 = gvTrupiKategoriZbritje.Columns["Lloji"] as GridViewDataTextColumn;
            col5.DataItemTemplate = new MyComboTemplate();
            GridViewDataTextColumn col6 = gvTrupiKategoriZbritje.Columns["Zbritja"] as GridViewDataTextColumn;
            col6.DataItemTemplate = new MyDoubleTemplate(false, 2, "0"); // "0.00");
            GridViewDataTextColumn col7 = gvTrupiKategoriZbritje.Columns["Prioriteti"] as GridViewDataTextColumn;
            col7.DataItemTemplate = new MyComboTemplate();
        }

        private DbCore.DbInventari.colTrupatKategoriteZbritjes ruajTrupat(int idPerdoruesi, DbCore.DbInventari.clsTrupiKategoriZbritje trupikryesor)
        {//ruhet collectioni i trupave            
            DbCore.DbInventari.colTrupatKategoriteZbritjes trupat = new DbCore.DbInventari.colTrupatKategoriteZbritjes();
            DbCore.DbInventari.clsTrupiKategoriZbritje trupi;
            trupat.Add(trupikryesor);
            int rreshta = gvTrupiKategoriZbritje.VisibleRowCount + 1;
            string initVal = this.hfDateFillimi.Value;
            string[] pars1 = initVal.Split(',');
            string initVal2 = this.hfDateMbarimi.Value;
            string[] pars3 = initVal2.Split(',');
            string initVal3 = this.hfVleraMin.Value;
            string[] pars5 = initVal3.Split(',');
            string initVal4 = this.hfVleraMax.Value;
            string[] pars7 = initVal4.Split(',');
            string initVal5 = this.hfLloji.Value;
            string[] pars9 = initVal5.Split(',');
            string initVal6 = this.hfZbritja.Value;
            string[] pars11 = initVal6.Split(',');


            string[] datefillimi = new string[rreshta];
            string[] datembarrimi = new string[rreshta];
            string[] vleramin = new string[rreshta];
            string[] vleramax = new string[rreshta];
            string[] lloji = new string[rreshta];
            string[] zbritja = new string[rreshta];
            if (initVal != "")//merren te dhenat e hiden fieldeve te trupave te fleteve kontabel nga javascipti
            {
                for (int i = 0; i < pars1.Length; i++)
                {
                    string[] pars2 = pars1[i].Split(':');
                    datefillimi[Convert.ToInt32(pars2[0])] = pars2[1];
                }
            }
            if (initVal2 != "")
            {
                for (int i = 0; i < pars3.Length; i++)
                {
                    string[] pars4 = pars3[i].Split(':');
                    datembarrimi[Convert.ToInt32(pars4[0])] = pars4[1];
                }
            }
            if (initVal3 != "")
            {
                for (int i = 0; i < pars5.Length; i++)
                {
                    string[] pars6 = pars5[i].Split(':');
                    vleramin[Convert.ToInt32(pars6[0])] = pars6[1];
                }
            }
            if (initVal4 != "")
            {
                for (int i = 0; i < pars7.Length; i++)
                {
                    string[] pars8 = pars7[i].Split(':');
                    vleramax[Convert.ToInt32(pars8[0])] = pars8[1];
                }
            }
            if (initVal5 != "")
            {
                for (int i = 0; i < pars9.Length; i++)
                {
                    string[] pars10 = pars9[i].Split(':');
                    lloji[Convert.ToInt32(pars10[0])] = pars10[1];
                }
            }
            if (initVal6 != "")
            {
                for (int i = 0; i < pars11.Length; i++)
                {
                    string[] pars12 = pars11[i].Split(':');
                    zbritja[Convert.ToInt32(pars12[0])] = pars12[1];
                }
            }
            for (int i = 0; i < rreshta - 1; i++)//krijohet kolectioni me trupati
            {
                trupi = new DbCore.DbInventari.clsTrupiKategoriZbritje();
                if (datefillimi[i] != null && datefillimi[i] != "null")
                {
                    if (datefillimi[i] == "")
                        trupi.DateFillimi = DateTime.Parse("01/01/" + DateTime.Today.Year);
                    else
                        trupi.DateFillimi = DateTime.Parse(datefillimi[i]);
                }
                if (datembarrimi[i] != null && datembarrimi[i] != "null")
                    if (datembarrimi[i] == "")
                        trupi.DateMbarimi = DateTime.MaxValue;
                    else
                        trupi.DateMbarimi = DateTime.Parse(datembarrimi[i]);
                if (vleramin[i] != null && vleramin[i] != "null" && vleramin[i] != "")
                    trupi.VleraMin = decimal.Parse(vleramin[i]);
                if (vleramax[i] != null && vleramax[i] != "null" && vleramax[i] != "")
                    trupi.VleraMax = decimal.Parse(vleramax[i]);
                if (lloji[i] != null && lloji[i] != "null" && lloji[i] != "")
                    trupi.Lloji = int.Parse(lloji[i]);
                if (zbritja[i] != null && zbritja[i] != "null" && zbritja[i] != "")

                    trupi.Zbritja = decimal.Parse(zbritja[i]);
                if (trupi.Zbritja != 0)
                {
                    trupat.Add(trupi);
                }


            }
            DbCore.DbInventari.colTrupatKategoriteZbritjes trupatVlere = new DbCore.DbInventari.colTrupatKategoriteZbritjes();
            DbCore.DbInventari.colTrupatKategoriteZbritjes trupatPaVlere = new DbCore.DbInventari.colTrupatKategoriteZbritjes();
            foreach (DbCore.DbInventari.clsTrupiKategoriZbritje tr in trupat)
            {
                if (tr.VleraMin == 0 && tr.VleraMax == 0)
                    trupatPaVlere.Add(tr);
                else
                    trupatVlere.Add(tr);
            }
            trupatPaVlere = renditSipasDates(trupatPaVlere);
            trupatVlere = renditSipasDates(trupatVlere);
            int prior = 1;//?
            foreach (DbCore.DbInventari.clsTrupiKategoriZbritje tr in trupatPaVlere)
            {
                tr.Prioriteti = prior;
                prior++;
                tr.IdPerdoruesi = idPerdoruesi;
                //tr.IdNderViti = idNdermVit;
            }
            foreach (DbCore.DbInventari.clsTrupiKategoriZbritje tr in trupatVlere)
            {
                tr.Prioriteti = prior;
                prior++;
                tr.IdPerdoruesi = idPerdoruesi;
                //tr.IdNderViti = idNdermVit;
            }
            trupat = new DbCore.DbInventari.colTrupatKategoriteZbritjes();
            trupat.AddRange(trupatPaVlere);
            trupat.AddRange(trupatVlere);
            return trupat;
        }

        private DbCore.DbInventari.colTrupatKategoriteZbritjes ruajTrupatMod(int idPerdoruesi, DbCore.DbInventari.clsTrupiKategoriZbritje trupikryesor)
        {//ruhet collectioni i trupave            
            DbCore.DbInventari.colTrupatKategoriteZbritjes trupat = new DbCore.DbInventari.colTrupatKategoriteZbritjes();
            DbCore.DbInventari.clsTrupiKategoriZbritje trupi;

            int rreshta = gvTrupiKategoriZbritje.VisibleRowCount + 1;
            string initVal = this.hfDateFillimi.Value;
            string[] pars1 = initVal.Split(',');
            string initVal2 = this.hfDateMbarimi.Value;
            string[] pars3 = initVal2.Split(',');
            string initVal3 = this.hfVleraMin.Value;
            string[] pars5 = initVal3.Split(',');
            string initVal4 = this.hfVleraMax.Value;
            string[] pars7 = initVal4.Split(',');
            string initVal5 = this.hfLloji.Value;
            string[] pars9 = initVal5.Split(',');
            string initVal6 = this.hfZbritja.Value;
            string[] pars11 = initVal6.Split(',');
            string initVal7 = this.hfPrioriteti.Value;
            string[] pars13 = initVal7.Split(',');

            string[] datefillimi = new string[rreshta];
            string[] datembarrimi = new string[rreshta];
            string[] vleramin = new string[rreshta];
            string[] vleramax = new string[rreshta];
            string[] lloji = new string[rreshta];
            string[] zbritja = new string[rreshta];
            string[] prioriteti = new string[rreshta];
            if (initVal != "")//merren te dhenat e hiden fieldeve te trupave te fleteve kontabel nga javascipti
            {
                for (int i = 0; i < pars1.Length; i++)
                {
                    string[] pars2 = pars1[i].Split(':');
                    datefillimi[Convert.ToInt32(pars2[0])] = pars2[1];
                }
            }
            if (initVal2 != "")
            {
                for (int i = 0; i < pars3.Length; i++)
                {
                    string[] pars4 = pars3[i].Split(':');
                    datembarrimi[Convert.ToInt32(pars4[0])] = pars4[1];
                }
            }
            if (initVal3 != "")
            {
                for (int i = 0; i < pars5.Length; i++)
                {
                    string[] pars6 = pars5[i].Split(':');
                    vleramin[Convert.ToInt32(pars6[0])] = pars6[1];
                }
            }
            if (initVal4 != "")
            {
                for (int i = 0; i < pars7.Length; i++)
                {
                    string[] pars8 = pars7[i].Split(':');
                    vleramax[Convert.ToInt32(pars8[0])] = pars8[1];
                }
            }
            if (initVal5 != "")
            {
                for (int i = 0; i < pars9.Length; i++)
                {
                    string[] pars10 = pars9[i].Split(':');
                    lloji[Convert.ToInt32(pars10[0])] = pars10[1];
                }
            }
            if (initVal6 != "")
            {
                for (int i = 0; i < pars11.Length; i++)
                {
                    string[] pars12 = pars11[i].Split(':');
                    zbritja[Convert.ToInt32(pars12[0])] = pars12[1];
                }
            }
            if (initVal7 != "")
            {
                for (int i = 0; i < pars13.Length; i++)
                {
                    string[] pars14 = pars13[i].Split(':');
                    prioriteti[Convert.ToInt32(pars14[0])] = pars14[1];
                }
            }
            DbCore.DbInventari.colTrupatKategoriteZbritjes colTrupatmePrioritet = new DbCore.DbInventari.colTrupatKategoriteZbritjes();
            colTrupatmePrioritet.Add(trupikryesor);
            for (int i = 0; i < rreshta - 1; i++)//krijohet kolectioni me trupati
            {
                trupi = new DbCore.DbInventari.clsTrupiKategoriZbritje();
                if (datefillimi[i] != null && datefillimi[i] != "null")
                {
                    if (datefillimi[i] == "")
                        trupi.DateFillimi = DateTime.Parse("01/01/" + DateTime.Today.Year);
                    else
                        trupi.DateFillimi = DateTime.Parse(datefillimi[i]);
                }
                if (datembarrimi[i] != null && datembarrimi[i] != "null")
                    if (datembarrimi[i] == "")
                        trupi.DateMbarimi = DateTime.MaxValue;
                    else
                        trupi.DateMbarimi = DateTime.Parse(datembarrimi[i]);
                if (vleramin[i] != null && vleramin[i] != "null" && vleramin[i] != "")
                    trupi.VleraMin = decimal.Parse(vleramin[i]);
                if (vleramax[i] != null && vleramax[i] != "null" && vleramax[i] != "")
                    trupi.VleraMax = decimal.Parse(vleramax[i]);
                if (lloji[i] != null && lloji[i] != "null" && lloji[i] != "")
                    trupi.Lloji = int.Parse(lloji[i]);
                if (zbritja[i] != null && zbritja[i] != "null" && zbritja[i] != "")

                    trupi.Zbritja = decimal.Parse(zbritja[i]);
                if (prioriteti[i] != null && prioriteti[i] != "null" && prioriteti[i] != "")

                    trupi.Prioriteti = int.Parse(prioriteti[i]);
                if (i < int.Parse(hfNrRreshta.Value))
                {
                    colTrupatmePrioritet.Add(trupi);
                }
                else if (trupi.Zbritja != 0)
                {
                    trupat.Add(trupi);
                }


            }
            DbCore.DbInventari.colTrupatKategoriteZbritjes trupatVlere = new DbCore.DbInventari.colTrupatKategoriteZbritjes();
            DbCore.DbInventari.colTrupatKategoriteZbritjes trupatPaVlere = new DbCore.DbInventari.colTrupatKategoriteZbritjes();
            foreach (DbCore.DbInventari.clsTrupiKategoriZbritje tr in trupat)
            {
                if (tr.VleraMin == 0 && tr.VleraMax == 0)
                    trupatPaVlere.Add(tr);
                else
                    trupatVlere.Add(tr);
            }
            trupatPaVlere = renditSipasDates(trupatPaVlere);
            trupatVlere = renditSipasDates(trupatVlere);
            int prior = 0;
            foreach (DbCore.DbInventari.clsTrupiKategoriZbritje tr in colTrupatmePrioritet)
            {
                if (prior < tr.Prioriteti)
                    prior = tr.Prioriteti;
                tr.IdPerdoruesi = idPerdoruesi;
                //tr.IdNderViti = idNdermVit;
            }
            prior++;
            foreach (DbCore.DbInventari.clsTrupiKategoriZbritje tr in trupatPaVlere)
            {
                tr.Prioriteti = prior;
                prior++;
                tr.IdPerdoruesi = idPerdoruesi;
                //tr.IdNderViti = idNdermVit;
            }
            foreach (DbCore.DbInventari.clsTrupiKategoriZbritje tr in trupatVlere)
            {
                tr.Prioriteti = prior;
                prior++;
                tr.IdPerdoruesi = idPerdoruesi;
                //tr.IdNderViti = idNdermVit;
            }
            trupat = new DbCore.DbInventari.colTrupatKategoriteZbritjes();
            trupat.AddRange(colTrupatmePrioritet);
            trupat.AddRange(trupatPaVlere);
            trupat.AddRange(trupatVlere);
            return trupat;
        }

        private DbCore.DbInventari.colTrupatKategoriteZbritjes renditSipasDates(DbCore.DbInventari.colTrupatKategoriteZbritjes col)
        {
            for (int i = 0; i < col.Count - 1; i++)
            {
                for (int j = i + 1; j < col.Count; j++)
                {
                    if (col[i].DateFillimi > col[j].DateFillimi)
                    {

                        DbCore.DbInventari.clsTrupiKategoriZbritje trup1 = col[i];
                        DbCore.DbInventari.clsTrupiKategoriZbritje trup2 = col[j];
                        col.RemoveAt(i);
                        col.shtoTrupiKategoriZbritjeNeIndeksin(i, trup2);
                        col.RemoveAt(j);
                        col.shtoTrupiKategoriZbritjeNeIndeksin(j, trup1);
                    }
                    else if (col[i].DateFillimi == col[j].DateFillimi)
                    {
                        if (col[i].DateMbarimi < col[j].DateMbarimi)
                        {
                            DbCore.DbInventari.clsTrupiKategoriZbritje trup1 = col[i];
                            DbCore.DbInventari.clsTrupiKategoriZbritje trup2 = col[j];
                            col.RemoveAt(i);
                            col.shtoTrupiKategoriZbritjeNeIndeksin(i, trup2);
                            col.RemoveAt(j);
                            col.shtoTrupiKategoriZbritjeNeIndeksin(j, trup1);
                        }
                    }
                }
            }
            return col;
        }

        private void konfiguroGride(string kodKonfigurimi, int idKomponente)
        {//konfiguron griden

            KonfigurimComboGride.ShtoMonedhe(gvKokaKategoriZbritje, idNdermarje, idPerdoruesi, Session, komponente, guidString);
            gvKokaKategoriZbritje.Columns["#"].VisibleIndex = 0;

        }

        //sherben per te ruajtur nje kategoriZbritje
        private void ruajKategoriZbritje()
        {
            DbCore.DbInventari.clsKokaKategoriZbritje kategori;
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            //DbCore.DbInventari.clsDatabaseInventari dbInventari = new DbCore.DbInventari.clsDatabaseInventari();
            if (Page.IsValid == false)
                return;
            else
            {
                int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                if (isValidKategori() && IsValidZbritje())
                {
                    bool eshteShtim;
                    kategori = krijoKategori(idPerdoruesi);
                    DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                    DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                    tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), komponente);
                    if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim")
                    {
                        if (!tedrejtaInfo.DShtim)
                        {
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgBlerjeShitjeNukKeniTeDrejta", ci), pnlMesazhi);
                            hfStatusi.Value = "false";
                            return;
                        }
                        mesazh = kategori.ruajKategoriZbritje(kategori.IdKokaKategoriZbritje, DbCore.clsFunksione.ktheStringunPaHapesira(kategori.KodKategoriZbritje, true), DbCore.clsFunksione.ktheStringunPaHapesira(kategori.PershkrimKategoriZbritje, false), kategori.IdPerdoruesi, kategori.Zbritja, kategori.IdNdermarje, kategori.OColTrupat, kategori.IdStatusDok, kategori.IdMonedha);
                        eshteShtim = true;
                    }
                    else
                    {
                        if (!tedrejtaInfo.DMod)
                        {
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgBlerjeShitjeNukKeniTeDrejta", ci), pnlMesazhi);
                            hfStatusi.Value = "false";
                            return;
                        }
                        eshteShtim = false;
                        kategori.IdKokaKategoriZbritje = int.Parse(hfId.Value.ToString());
                        mesazh = kategori.modifikoKategoriZbritje(kategori.IdKokaKategoriZbritje, DbCore.clsFunksione.ktheStringunPaHapesira(kategori.KodKategoriZbritje, true), DbCore.clsFunksione.ktheStringunPaHapesira(kategori.PershkrimKategoriZbritje, false), kategori.IdPerdoruesi, kategori.Zbritja, kategori.IdNdermarje, kategori.OColTrupat, kategori.IdStatusDok, kategori.IdMonedha);
                    }
                    if (mesazh.Status == true)
                    {
                        clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("msgBlerjeShitjeRuajtjeMeSukses", ci), pnlMesazhi);
                        hfStatusi.Value = "true";
                        if (eshteShtim)
                            shtoKategoriNeGrid(idNdermarrje, idPerdoruesi, kategori.IdKokaKategoriZbritje);
                        else //modifikim
                            modifikoKategoriNeGrid(idNdermarrje, idPerdoruesi, kategori.IdKokaKategoriZbritje);
                    }
                    else
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgBlerjeShitjeRuajtjeMeGabime", ci), pnlMesazhi);
                        hfStatusi.Value = "false";
                    }
                    ASPxPageControl1.ActiveTabIndex = 0;
                    //    mbushListeKategorish();
                }
                else
                {
                    hfStatusi.Value = "false";
                    //  mbushListeKategorish();
                }
            }
        }

        private void pastroFusha()
        {//pastron fushat
            this.txtKodi2.Text = "";
            this.txtPershkrimi2.Text = "";
            this.txtZbritja.Text = "";
            this.hfDateFillimi.Value = "";
            this.hfDateMbarimi.Value = "";
            this.hfLloji.Value = "";
            this.hfVleraMax.Value = "";
            this.hfVleraMin.Value = "";
            this.hfZbritja.Value = "";
            this.HiddenField1.Value = "";

            mbushListeNenKategorish();
        }

        private DbCore.DbInventari.clsKokaKategoriZbritje krijoKategori(int idPerdoruesi)
        {//krijon nje kategori


            DbCore.DbInventari.clsKokaKategoriZbritje kategori = new DbCore.DbInventari.clsKokaKategoriZbritje();
            //kategori.IdNderViti = idNdermVit;
            kategori.IdStatusDok = 1;
            kategori.KodKategoriZbritje = DbCore.clsFunksione.ktheStringunPaHapesira(txtKodi2.Text, true);
            kategori.PershkrimKategoriZbritje = DbCore.clsFunksione.ktheStringunPaHapesira(txtPershkrimi2.Text, false);
            kategori.Zbritja = decimal.Parse(txtZbritja.Text);
            kategori.IdMonedha = int.Parse(cmbMonedha.Value.ToString());
            kategori.IdPerdoruesi = idPerdoruesi;
            kategori.IdNdermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DbCore.DbInventari.clsTrupiKategoriZbritje trupi = new DbCore.DbInventari.clsTrupiKategoriZbritje();
            trupi.DateFillimi = DateTime.Parse("01/01/" + DateTime.Today.Year);
            trupi.DateMbarimi = DateTime.MaxValue.AddSeconds(-1);
            trupi.Lloji = 1;
            trupi.VleraMax = 0;
            trupi.VleraMin = 0;
            trupi.Zbritja = decimal.Parse(txtZbritja.Text);
            if (hfShtimModifikim.Value == "shtim")
            { kategori.OColTrupat = ruajTrupat(idPerdoruesi, trupi); }
            else if (hfShtimModifikim.Value == "modifikim" || hfShtimModifikim.Value == "klonim")
            { kategori.OColTrupat = ruajTrupatMod(idPerdoruesi, trupi); }
            else kategori.OColTrupat = new DbCore.DbInventari.colTrupatKategoriteZbritjes();

            return kategori;
        }

        //kontrollon nese te dhenat qe jane plotesuara jane 
        //te lejueshme apo jo
        private bool isValidKategori()
        {
            using (var dbInventari = new DbCore.DbInventari.clsDatabaseInventari())
            {
                DbCore.clsMesazh kontrollKodKategoriZbritje = DbCore.clsFunksione.kontrolloKaraktereMeMesazh(DbCore.clsFunksione.ktheStringunPaHapesira(txtKodi2.Text, true), FusheKontrolli.Kodi, false);
                if (!kontrollKodKategoriZbritje.Status)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, kontrollKodKategoriZbritje.PershkrimMesazhi, pnlMesazhi);
                    return false;
                }
                DbCore.clsMesazh kontrolltxtPershkrimi2 = DbCore.clsFunksione.kontrolloKaraktereMeMesazh(DbCore.clsFunksione.ktheStringunPaHapesira(txtPershkrimi2.Text, false), FusheKontrolli.Pershkrimi, true);
                if (!kontrolltxtPershkrimi2.Status)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, kontrolltxtPershkrimi2.PershkrimMesazhi, pnlMesazhi);
                    return false;
                }

                if (dbInventari.ekzistonKategoriZbritje(txtKodi2.Text, IdNdermarrja) && (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim"))
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgBlerjeShitjeZbritjeKodNjejte"], pnlMesazhi);
                    return false;
                }
                return true;
            }
        }

        private bool IsValidZbritje()
        {
            using (var dbInventari = new DbCore.DbInventari.clsDatabaseInventari())
            {
                string veprimi = hfShtimModifikim.Value;

                if (txtZbritja.Text != "0.00")
                {
                    if (dbInventari.ekzistonVlereZbritje(Convert.ToDecimal(txtZbritja.Text), IdNdermarrja, Convert.ToInt32(cmbMonedha.Value))
                        && (veprimi == "shtim" || veprimi == "klonim"))
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgBlerjeShitjeVlereZbritjeNjejte"], pnlMesazhi);
                        return false;
                    }
                }

                if (veprimi == "modifikim")
                {
                    if (new DbCore.DbInventari.clsKokaKategoriZbritje(int.Parse(hfId.Value.ToString())).Zbritja == decimal.Parse(txtZbritja.Text))
                        return true;
                    else if (txtZbritja.Text != "0.00")
                    {
                        if (dbInventari.ekzistonVlereZbritje(Convert.ToDecimal(txtZbritja.Text), IdNdermarrja, Convert.ToInt32(cmbMonedha.Value)))
                        {
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgBlerjeShitjeVlereZbritjeNjejte"], pnlMesazhi);
                            return false;
                        }
                    }
                }

                return true;
            }
        }

        protected void ASPxPageControl1_ActiveTabChanged(object source, DevExpress.Web.TabControlEventArgs e)
        {
        }

        protected void gvKokaKategoriZbritje_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {//thirret kur grida ben callback
            // mbushListeKategorish();
            if (e.CallbackName == "COLUMNMOVE" && gvKokaKategoriZbritje.AllColumns[int.Parse(e.Args[0])].Width.Value == 0)
                gvKokaKategoriZbritje.AllColumns[int.Parse(e.Args[0])].Width = Unit.Percentage(3);
            if (e.CallbackName == "APPLYFILTER" && e.Args[0] == "")
            {
                DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
                ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
                cmbFiltra.Text = "";


            }
        }

        protected void gvTrupiKategoriZbritje_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            gvTrupiKategoriZbritje.DataBind();
        }

        protected void gvTrupiKategoriZbritje_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {//kur grida ben callback te ruajme te dhenat 
            int key = -1;
            if (e.Parameters.ToString().Split(':')[0] != "")
            {
                key = int.Parse(e.Parameters.ToString().Split(':')[0]);
            }
            if (e.Parameters.ToString().Split(':').Length == 2)
            {
                if (e.Parameters.ToString().Split(':')[1] == "modifikim" || e.Parameters.ToString().Split(':')[1] == "klonim")
                {
                    int id = int.Parse(gvKokaKategoriZbritje.GetRowValues(key, "IdKokaKategoriZbritje").ToString()); //int.Parse(Request.QueryString["id"]);
                    mbushListeNenKategorishMod(id);
                }
                else if (e.Parameters.ToString().Split(':')[1] == "shtim") mbushListeNenKategorish();
            }
            else
            {
                DbCore.DbInventari.colTrupatKategoriteZbritjes trupat = new DbCore.DbInventari.colTrupatKategoriteZbritjes();
                DbCore.DbInventari.clsTrupiKategoriZbritje trupi;
                int rreshta = gvTrupiKategoriZbritje.VisibleRowCount + 1;
                string initVal = this.hfDateFillimi.Value;
                string[] pars1 = initVal.Split(',');
                string initVal2 = this.hfDateMbarimi.Value;
                string[] pars3 = initVal2.Split(',');
                string initVal3 = this.hfVleraMin.Value;
                string[] pars5 = initVal3.Split(',');
                string initVal4 = this.hfVleraMax.Value;
                string[] pars7 = initVal4.Split(',');
                string initVal5 = this.hfLloji.Value;
                string[] pars9 = initVal5.Split(',');
                string initVal6 = this.hfZbritja.Value;
                string[] pars11 = initVal6.Split(',');
                string initVal7 = this.hfPrioriteti.Value;
                string[] pars13 = initVal7.Split(',');

                string[] datefillimi = new string[rreshta];
                string[] datembarrimi = new string[rreshta];
                string[] vleramin = new string[rreshta];
                string[] vleramax = new string[rreshta];
                string[] lloji = new string[rreshta];
                string[] zbritja = new string[rreshta];
                string[] prioriteti = new string[rreshta];
                if (initVal != "")//merren te dhenat e hiden fieldeve te trupave te fleteve kontabel nga javascipti
                {
                    for (int i = 0; i < pars1.Length; i++)
                    {
                        string[] pars2 = pars1[i].Split(':');
                        datefillimi[Convert.ToInt32(pars2[0])] = pars2[1];
                    }
                }
                if (initVal2 != "")
                {
                    for (int i = 0; i < pars3.Length; i++)
                    {
                        string[] pars4 = pars3[i].Split(':');
                        datembarrimi[Convert.ToInt32(pars4[0])] = pars4[1];
                    }
                }
                if (initVal3 != "")
                {
                    for (int i = 0; i < pars5.Length; i++)
                    {
                        string[] pars6 = pars5[i].Split(':');
                        vleramin[Convert.ToInt32(pars6[0])] = pars6[1];
                    }
                }
                if (initVal4 != "")
                {
                    for (int i = 0; i < pars7.Length; i++)
                    {
                        string[] pars8 = pars7[i].Split(':');
                        vleramax[Convert.ToInt32(pars8[0])] = pars8[1];
                    }
                }
                if (initVal5 != "")
                {
                    for (int i = 0; i < pars9.Length; i++)
                    {
                        string[] pars10 = pars9[i].Split(':');
                        lloji[Convert.ToInt32(pars10[0])] = pars10[1];
                    }
                }
                if (initVal6 != "")
                {
                    for (int i = 0; i < pars11.Length; i++)
                    {
                        string[] pars12 = pars11[i].Split(':');
                        zbritja[Convert.ToInt32(pars12[0])] = pars12[1];
                    }
                }
                if (initVal7 != "")
                {
                    for (int i = 0; i < pars13.Length; i++)
                    {
                        string[] pars14 = pars13[i].Split(':');
                        prioriteti[Convert.ToInt32(pars14[0])] = pars14[1];
                    }
                }
                for (int i = 0; i < rreshta - 1; i++)//krijohet kolectioni me trupati
                {
                    trupi = new DbCore.DbInventari.clsTrupiKategoriZbritje();
                    if (datefillimi[i] != null && datefillimi[i] != "null" && datefillimi[i] != "")
                    {
                        trupi.DateFillimi = DateTime.Parse(datefillimi[i]);
                    }
                    if (datembarrimi[i] != null && datembarrimi[i] != "null" && datembarrimi[i] != "")
                        trupi.DateMbarimi = DateTime.Parse(datembarrimi[i]);
                    if (vleramin[i] != null && vleramin[i] != "null" && vleramin[i] != "")
                        trupi.VleraMin = decimal.Parse(vleramin[i]);
                    if (vleramax[i] != null && vleramax[i] != "null" && vleramax[i] != "")
                        trupi.VleraMax = decimal.Parse(vleramax[i]);
                    if (lloji[i] != null && lloji[i] != "null" && lloji[i] != "")
                        trupi.Lloji = int.Parse(lloji[i]);
                    if (zbritja[i] != null && zbritja[i] != "null" && zbritja[i] != "")
                        trupi.Zbritja = decimal.Parse(zbritja[i]);
                    if (prioriteti[i] != null && prioriteti[i] != "null" && prioriteti[i] != "")

                        trupi.Prioriteti = int.Parse(prioriteti[i]);

                    trupat.Add(trupi);

                }
                if (key != -1)
                    trupat.RemoveAt(key);
                else
                {
                    DbCore.DbInventari.clsTrupiKategoriZbritje trup = new DbCore.DbInventari.clsTrupiKategoriZbritje();

                    trupat.Add(trup);
                }
                if (trupat.Count == 0)
                {
                    DbCore.DbInventari.clsTrupiKategoriZbritje trup = new DbCore.DbInventari.clsTrupiKategoriZbritje();

                    trupat.Add(trup);
                }
                this.gvTrupiKategoriZbritje.DataSource = trupat;
                this.gvTrupiKategoriZbritje.DataBind();
            }
            percaktoTemplateNenKategorish();
        }

        protected void gvTrupiKategoriZbritje_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpNoRows"] = gvTrupiKategoriZbritje.VisibleRowCount;
        }

        protected void gvTrupiKategoriZbritje_DataBound(object sender, EventArgs e)
        {//shton butonin fshi
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            if (this.gvTrupiKategoriZbritje.Columns[rm.GetString("labelBlerjeShitjeFshi", ci)] == null)
            {
                GridViewDataTextColumn fshi = new GridViewDataTextColumn();
                fshi.Caption = rm.GetString("labelBlerjeShitjeFshi", ci);
                //fshi.Caption = rm.GetString("labelBlerjeShitjeFshi", ci);
                fshi.Width = 50;
                gvTrupiKategoriZbritje.Columns.Add(fshi);

                gvTrupiKategoriZbritje.KeyFieldName = "IdTrupiKategoriZbritje";
                gvTrupiKategoriZbritje.SettingsBehavior.AllowSelectByRowClick = false;
                gvTrupiKategoriZbritje.SettingsBehavior.AllowFocusedRow = true;
            }
        }

        protected void gvTrupiKategoriZbritje_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {//krijon rreshat sipas modelit
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            bool ugjet;
            if (e.RowType == DevExpress.Web.GridViewRowType.Data)
            {
                GridViewDataTextColumn col0 = ((ASPxGridView)sender).Columns[rm.GetString("labelBlerjeShitjeFshi", ci)] as GridViewDataTextColumn;
                GridViewDataDateColumn col1 = ((ASPxGridView)sender).Columns["DateFillimi"] as GridViewDataDateColumn;
                GridViewDataDateColumn col2 = ((ASPxGridView)sender).Columns["DateMbarimi"] as GridViewDataDateColumn;
                GridViewDataTextColumn col3 = ((ASPxGridView)sender).Columns["VleraMin"] as GridViewDataTextColumn;
                GridViewDataTextColumn col4 = ((ASPxGridView)sender).Columns["VleraMax"] as GridViewDataTextColumn;
                GridViewDataTextColumn col5 = ((ASPxGridView)sender).Columns["Lloji"] as GridViewDataTextColumn;
                GridViewDataTextColumn col6 = ((ASPxGridView)sender).Columns["Zbritja"] as GridViewDataTextColumn;
                GridViewDataTextColumn col7 = ((ASPxGridView)sender).Columns["Prioriteti"] as GridViewDataTextColumn;

                ASPxButton btn0 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col0, "btn") as ASPxButton;
                ASPxDateEdit cal1 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col1, "cal") as ASPxDateEdit;
                ASPxDateEdit cal2 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col2, "cal") as ASPxDateEdit;
                ASPxTextBox txt3 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col3, "txtBox") as ASPxTextBox;
                ASPxTextBox txt4 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col4, "txtBox") as ASPxTextBox;
                ASPxComboBox cmb5 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col5, "cmbBox") as ASPxComboBox;
                ASPxTextBox txt6 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col6, "txtBox") as ASPxTextBox;
                ASPxComboBox cmb7 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col7, "cmbBox") as ASPxComboBox;
                ugjet = false;
                //vendosen client side eventet e kolonave
                if (btn0 != null)
                {
                    btn0.ClientInstanceName = "btnFshi" + e.VisibleIndex.ToString();
                    btn0.ClientSideEvents.Click = "function(s,e){FshiClicked(" + e.VisibleIndex.ToString() + ");}";
                }
                if (cmb5 != null)
                {

                    cmb5.Items.Add(rm.GetString("cmbItemBlerjeShitjeperqindje", ci), 1);
                    cmb5.Items.Add(rm.GetString("cmbItemBlerjeShitjevlere", ci), 2);
                    cmb5.DataBind();
                    if (cmb5.SelectedIndex == -1)
                        cmb5.SelectedIndex = 0;
                    cmb5.ClientInstanceName = "cmbLloji" + e.VisibleIndex.ToString();
                    cmb5.ClientSideEvents.TextChanged = "function(s,e){TextChangedLloji(cmbLloji" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + ");}";

                    if (e.VisibleIndex == ((ASPxGridView)sender).VisibleRowCount - 2)
                    {
                        if (ugjet)
                        {
                            tempcombo = cmb5;
                            ugjet = false;
                        }
                        //else if (koloneFocus == "Lloji")
                        //{
                        //    ugjet = true;
                        //}
                    }
                }
                if (cmb7 != null)
                {

                    cmb7.Items.Add("1", 1);
                    cmb7.Items.Add("2", 2);
                    cmb7.Items.Add("3", 3);
                    cmb7.Items.Add("4", 4);
                    cmb7.Items.Add("5", 5);
                    cmb7.Items.Add("6", 6);
                    cmb7.Items.Add("7", 7);
                    cmb7.Items.Add("8", 8);
                    cmb7.Items.Add("9", 9);
                    cmb7.Items.Add("10", 10);
                    cmb7.Items.Add("11", 11);
                    cmb7.Items.Add("12", 12);
                    cmb7.Items.Add("13", 13);
                    cmb7.Items.Add("14", 14);
                    cmb7.Items.Add("15", 15);
                    cmb7.Items.Add("16", 16);
                    cmb7.Items.Add("17", 17);
                    cmb7.Items.Add("18", 18);
                    cmb7.Items.Add("19", 19);
                    cmb7.Items.Add("20", 20);

                    cmb7.DataBind();

                    cmb7.ClientInstanceName = "cmbPrioriteti" + e.VisibleIndex.ToString();
                    cmb7.ClientSideEvents.TextChanged = "function(s,e){TextChangedPrioriteti(cmbPrioriteti" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + ");}";

                    if (e.VisibleIndex == ((ASPxGridView)sender).VisibleRowCount - 2)
                    {
                        if (ugjet)
                        {
                            tempcombo = cmb7;
                            ugjet = false;
                        }
                        //else if (koloneFocus == "Prioriteti")
                        //{
                        //    ugjet = true;
                        //}
                    }
                }
                if (cal1 != null)
                {
                    cal1.ClientInstanceName = "dteDateFillimi" + e.VisibleIndex.ToString();
                    cal1.ClientSideEvents.DateChanged = "function(s,e){TextChangedDateFillimi(dteDateFillimi" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + ");}";

                    if (e.VisibleIndex == ((ASPxGridView)sender).VisibleRowCount - 2)
                    {
                        if (ugjet)
                        {
                            tempcal = cal1;
                            ugjet = false;
                        }
                        //else if (koloneFocus == "DateFillimi")
                        //{
                        //    ugjet = true;
                        //}
                    }
                }
                if (cal2 != null)
                {
                    cal2.ClientInstanceName = "dteDateMbarimi" + e.VisibleIndex.ToString();
                    cal2.ClientSideEvents.DateChanged = "function(s,e){TextChangedDateMbarimi(dteDateMbarimi" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + ");}";

                    if (e.VisibleIndex == ((ASPxGridView)sender).VisibleRowCount - 2)
                    {
                        if (ugjet)
                        {
                            tempcal = cal2;
                            ugjet = false;
                        }
                        //else if (koloneFocus == "DateMbarimi")
                        //{
                        //    ugjet = true;
                        //}
                    }
                }
                if (txt3 != null)
                {
                    txt3.ClientInstanceName = "txtVleraMin" + e.VisibleIndex.ToString();
                    txt3.ClientSideEvents.TextChanged = "function(s,e){TextChangedVleraMin(txtVleraMin" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + ");}";
                    if (e.VisibleIndex == ((ASPxGridView)sender).VisibleRowCount - 2)
                    {
                        if (ugjet)
                        {
                            temptxt = txt3;
                            ugjet = false;
                        }
                        //else if (koloneFocus == "VleraMin")
                        //{
                        //    ugjet = true;
                        //}
                    }
                }
                if (txt4 != null)
                {



                    txt4.ClientInstanceName = "txtVleraMax" + e.VisibleIndex.ToString();
                    txt4.ClientSideEvents.TextChanged = "function(s,e){TextChangedVleraMax(txtVleraMax" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + ");}";
                    if (e.VisibleIndex == ((ASPxGridView)sender).VisibleRowCount - 2)
                    {
                        if (ugjet)
                        {
                            temptxt = txt4;
                            ugjet = false;
                        }
                        //else if (koloneFocus == "VleraMax")
                        //{
                        //    ugjet = true;
                        //}
                    }
                }
                if (txt6 != null)
                {
                    txt6.ClientInstanceName = "txtZbritja" + e.VisibleIndex.ToString();
                    txt6.ClientSideEvents.TextChanged = "function(s,e){TextChangedZbritja(txtZbritja" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + ");}";
                    if (e.VisibleIndex == ((ASPxGridView)sender).VisibleRowCount - 2)
                    {
                        if (ugjet)
                        {
                            temptxt = txt6;
                            ugjet = false;
                        }
                        //else if (koloneFocus == "Zbritja")
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

        }

        protected void gvKokaKategoriZbritje_DataBound(object sender, EventArgs e)
        {
            if (this.gvKokaKategoriZbritje.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per 
                //perzgjidh
                DevExpress.Web.GridViewCommandColumn check = new DevExpress.Web.GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true;
                check.Width = Unit.Percentage(10);
                //   check.SetColVisibleIndex(0);
                //behet per te afishuar rreshtin qe do sherbej per filtrim
                gvKokaKategoriZbritje.Settings.ShowFilterRow = true;
                gvKokaKategoriZbritje.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                gvKokaKategoriZbritje.Settings.ShowFilterRowMenu = true;
                gvKokaKategoriZbritje.Columns.Add(check);

                gvKokaKategoriZbritje.KeyFieldName = "IdKokaKategoriZbritje";
                gvKokaKategoriZbritje.SettingsBehavior.AllowSelectByRowClick = true;
                gvKokaKategoriZbritje.SettingsBehavior.AllowFocusedRow = true;
            } //gvKokaKategoriZbritje.Columns["#"].VisibleIndex = 0;
        }

        protected void gvKokaKategoriZbritje_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            if (e.Column.FieldName == "PershkrimKategoriZbritje")
            {
                e.Values.Clear();
                //e.AddShowAll();
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

        protected void gvKokaKategoriZbritje_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if (e.Column.FieldName == "IdMonedha")
            {
                if (Converter.ConvertToInt(e.Value) == 0 || Converter.ConvertToInt(e.Value) == -1)
                {
                    e.Criteria = null;
                }
            }
        }

        protected void gvKokaKategoriZbritje_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string idkomponente = "";
            string kodkonfigurimi = "";

            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                kodkonfigurimi = arr[1];
                idkomponente = arr[0];
                if (arr[2] == "")
                    gvKokaKategoriZbritje.FilterExpression = "";
                else
                {
                    //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    //filtra.mbushFiltraGridaSipasFiltraKodi(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvKokaKategoriZbritje", komponente, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), int.Parse(cmbKonfigurimi.Value.ToString()));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
                    //DbCore.DbAdmin.clsFiltraGrida filtra = dbAdmin.merrFiltraGridaSipasFiltraKodi(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    if (filtra.FiltraKodi != null)
                    {
                        gvKokaKategoriZbritje.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvKokaKategoriZbritje);
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
            //   konfiguroGride();
            //   funksion.percaktoVisibleColumnsGridSipasKodKonfigurimi(gvKokaKategoriZbritje, kodkonfigurimi, idkomponente);
            gvKokaKategoriZbritje.Selection.UnselectAll();
        }

        protected void gvKokaKategoriZbritje_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gvKokaKategoriZbritje.PageIndex;
            e.Properties["cpPageRow"] = gvKokaKategoriZbritje.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gvKokaKategoriZbritje.VisibleRowCount;
        }
        private void EmrateLabelave(CultureInfo ci)
        {
            popFshi.HeaderText = rm.GetString("labelBlerjeShitjeKujdes", ci);
            lblMsgbox.Text = rm.GetString("labelBlerjeShitjeMesazhJeniiSigurt", ci);
            ButtonOk.Text = rm.GetString("labelBlerjeShitjeOK", ci);
            ButtonCancel.Text = rm.GetString("labelRaportAnullo", ci);
            ASPxPageControl1.TabPages[0].Text = rm.GetString("labelBlerjeShitjeTePergjithshme", ci);
            ASPxPageControl1.TabPages[1].Text = rm.GetString("labelBlerjeShitjeNenkategorite", ci);


            konfigurimi_Label.Text = rm.GetString("labelBlerjeShitjeKonfigurim", ci);
            ((ASPxLabel)ASPxPageControl1.TabPages[1].FindControl("lblKodi")).Text = rm.GetString("labelBlerjeShitjeKodi", ci);
            ((ASPxLabel)ASPxPageControl1.TabPages[1].FindControl("lblPershkrimi")).Text = rm.GetString("labelBlerjeShitjePershkrimi", ci);
            ((ASPxLabel)ASPxPageControl1.TabPages[1].FindControl("lblZbritja")).Text = rm.GetString("labelBlerjeShitjeZbritje", ci);
            txtZbritja.ValidationSettings.RegularExpression.ErrorText = rm.GetString("labelBlerjeShitjeZbritjeErrormsg2", ci);
            lblMonedha.Text = rm.GetString("labelBlerjeShitjeMonedha", ci);
        }
        private void LoadString(ASPxHiddenField hfMsgZbritje, CultureInfo ci)
        {
            hfMsgZbritje.Clear();
            hfMsgZbritje.Add("MsgZgjidhKategori", rm.GetString("MsgBlerjeShitjeZgjidhKategori", ci));
            hfMsgZbritje.Add("MsgRreshtaNjelloj", rm.GetString("MsgBlerjeShitjeRreshtaNjelloj", ci));
            hfMsgZbritje.Add("MsgDataGabim", rm.GetString("MsgBlerjeShitjeGabimData", ci));
            hfMsgZbritje.Add("MsgVleraMinimumPerqindje", rm.GetString("MsgBlerjeShitjeVleraMinimumPerqindje", ci));
            hfMsgZbritje.Add("MsgVleraMaximumPerqindje", rm.GetString("MsgBlerjeShitjeVleraMaximumPerqindje", ci));
            hfMsgZbritje.Add("MsgZbritjaNumer", rm.GetString("MsgBlerjeShitjeZbritjaNumer", ci));
            hfMsgZbritje.Add("MsgZbritjaPozitive", rm.GetString("MsgBlerjeShitjeZbritjaPozitive", ci));
            hfMsgZbritje.Add("MsgZbritjaMaximum100", rm.GetString("MsgBlerjeShitjeZbritjameevogelse100", ci));
            hfMsgZbritje.Add("MsgVleraMinimumVlere", rm.GetString("MsgBlerjeShitjeVleraMinimumVlere", ci));
            hfMsgZbritje.Add("MsgVleraMaximumVlere", rm.GetString("MsgBlerjeShitjeVleraMaximumVlere", ci));
            hfMsgZbritje.Add("MsgLlojKategorie", rm.GetString("MsgBlerjeShitjeLlojKategorie", ci));
        }

        protected void OnPreRender_gvKokaKategoriZbritje(object sender, EventArgs e)
        {
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            EmrateLabelave(ci);

        }
    }
}

