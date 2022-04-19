using DevExpress.Web;
using PlatinumWeb.Templates;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using DbCore;
using System.Linq;
using System.Resources;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.DbInventari;
using Newtonsoft.Json;
using DbCore.IMBUtils.Extensions;
using DbCore.DbAdmin;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{
    public partial class Shto_KategoriSeriali : MyPageBase
    {
        private const string prefixMesazhNjejes = "Kategoria me Kod: ";
        private const string prefixMesazhShumes = "Kategorite me Kod: ";
        private const string suffixMesazhNjejesGabimiLidhur = " eshte e lidhur dhe nuk mund te fshihet";
        private const string suffixMesazhShumesGabimiLidhur = " jane te lidhur dhe nuk mund te fshihen";
        private const string suffixMesazhNjejesSuksesi = " u fshi me sukses!";
        private const string suffixMesazhShumesSuksesi = " u fshine me sukses!";
        private const string lidhesMesazhi = " Kurse ";
        private const string gabim2burimenegride = "GABIM: Ndodhen 2 kategori me te njejten id ne gride";
        private const string mesazhZgjidhniNje = "Ju lutem zgjidhni te pakten nje kategori!";
        private int idperdoruesi, idnderviti, idNdermarrje, idviti, idgjuha;
        private const int idstatusdok = 1;
        /// <summary>
        /// perdoret per te vendosur themen e devexpresit faqes
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>

        private string komponente = "Shto_KategoriSeriali.aspx";
        private string guidString;
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
            idgjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            idnderviti = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
            CultureInfo cultinf = DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo(idgjuha);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (!Page.IsPostBack)
            {
                guidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                hfState.Set("guidString", guidString);
                EmrateTabeve(rm, cultinf);
                hfState.Set("idGjuha", idgjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                ASPxPageControl1.ActiveTabIndex = 0;
                percaktoTemplateMenu(idgjuha, idviti, idperdoruesi, idNdermarrje, ASPxMenu1);
                konfiguroVleraFillestare(idNdermarrje, rm, cultinf, idgjuha);
                mbushComboBoxFiltra(idNdermarrje, idgjuha, int.Parse(cmbKonfigurimi.Value.ToString()));
                mbushGridKategoriNgaDB(idNdermarrje);
                //konfiguroGrideTrupi(idNdermarrje);
                konfiguroGrideImportiTrupi(idNdermarrje);
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idperdoruesi, idNdermarrje, idviti, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idperdoruesi, idNdermarrje, idviti, komponente);
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                konfiguroGride(idNdermarrje, idperdoruesi, cmbKonfigurimi.Text.Split(';')[0], 447, rm, cultinf);
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "gvKategorite", gvKategorite, cmbKonfigurimi.Text.Split(';')[0], 447.ToString(), DbCore.mySessionObjects.ktheGjuhe(Session));
            }
            else
            {
                guidString = (string)hfState["guidString"];
                mbushGridKategoriNgaSession(idNdermarrje);
                konfiguroGride(idNdermarrje, idperdoruesi, cmbKonfigurimi.Text.Split(';')[0], 447, rm, cultinf);
            }
            //percaktoTemplateTrupash();
            percaktoTemplateTrupashFushat();
            GridUtil.konfigGrideListeEMadhePaTheme(gvKategorite, "ID");
            percaktoTemplateMenu(idgjuha, idviti, idperdoruesi, idNdermarrje, ASPxMenu1);
            mbushComboBoxFiltra(idNdermarrje, idgjuha, int.Parse(cmbKonfigurimi.Value.ToString()));
            GridUtil.ToolTipButonaveMbiGride(gvKategorite, cultinf, rm);
            gvKategorite.Columns["#"].VisibleIndex = 0;
            gvImporti.Columns["#"].VisibleIndex = 0;
        }

        /// <summary>
        /// Vendos emrat e tabeve ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateTabeve(ResourceManager rm, CultureInfo cultinf)
        {
            ASPxPageControl1.TabPages[0].Text = rm.GetString("labelRaportTePergjithshme", cultinf);
            ASPxPageControl1.TabPages[1].Text = rm.GetString("KarakteristikaTab", cultinf);
            konfigurimi_Label.Text = rm.GetString("lblModeli", cultinf);
        }

        /// <summary>
        /// mbush combon e filtrave
        /// </summary>
        /// <param name="idNdermarrje">id e ndermarjes</param>
        private static void mbushComboBoxFiltra(int idNdermarrje, int idgjuhe, int idkonfig)
        {
            clsToolbarConfig.mbushComboBoxFiltraMeValFieldTextField(idgjuhe, idNdermarrje, "gvKategorite", "Shto_KategoriSeriali.aspx", "IdFiltra", "FiltraShenime", idkonfig);
        }
        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="idNderVit"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        private void percaktoTemplateMenu(int idGjuha, int idViti, int idPerdorues, int idNdermarrje, DevExpress.Web.ASPxMenu aSPxMenu1)
        {
            clsToolbarConfig.percaktoTemplateMenu(idGjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1, komponente, this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, hfShtimModifikim.Value == "modifikim" ? false : true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
        }


        protected void RuajKolona_Click(object sender, EventArgs e)
        {
            int idfiltri = 0;
            int idkonf = DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(cmbKonfigurimi.Text.Split(';')[0], idNdermarrje);
            DbCore.clsMesazh mesazh = GridUtil.ruajFiltra(idNdermarrje, idperdoruesi, idgjuha, "gvKategorite ", komponente, "FilterDefault", gvKategorite.FilterExpression, gvKategorite, "Kodi", idkonf, out idfiltri);//ruan filtrin e zgjedhur tek filtrat
            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                return;

            }
            mesazh = GridUtil.ruajkonfigurimgride(gvKategorite, cmbKonfigurimi.Text, idNdermarrje, idperdoruesi, 447, idfiltri, idviti, DbCore.mySessionObjects.ktheCultureInfo(Session), DbCore.mySessionObjects.ktheGjuhe(Session));/// ruan konfigurimin e grides dhe filtrin

            clsToolbarConfig.mbushComboBoxFiltra(idgjuha, idNdermarrje, "gvKategorite ", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);

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
        protected void gvKategorite_DataBound(object sender, EventArgs e)
        {// shton colonen # per selektim dhe disa karakteristika te grides
            if (gvKategorite.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per 
                //perzgjidh
                GridViewCommandColumn check = new GridViewCommandColumn("#") { ShowSelectCheckbox = true, Width = Unit.Percentage(2) };
                //behet per te afishuar rreshtin qe do sherbej per filtrim
                gvKategorite.Settings.ShowFilterRow = true;
                gvKategorite.Settings.ShowFilterRowMenu = true;
                gvKategorite.Columns.Add(check);
                gvKategorite.KeyFieldName = "ID";
                gvKategorite.SettingsBehavior.AllowSelectByRowClick = true;
                gvKategorite.SettingsBehavior.AllowFocusedRow = true;
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
        private void konfiguroGride(int idNdermarrje, int idPerdoruesi, string kodKonfigurimi, int idKomponente, ResourceManager rm, CultureInfo ci)
        {
            gvKategorite.Columns["#"].VisibleIndex = 0;
            gvImporti.Columns["#"].VisibleIndex = 0;
        }

        /// <summary>
        /// perdoret ne rastet kur grida ben callback per ta rimbushur griden
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void gvKategorite_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            if (e.CallbackName == "COLUMNMOVE" && gvKategorite.AllColumns[int.Parse(e.Args[0])].Width.Value == 0)
                gvKategorite.AllColumns[int.Parse(e.Args[0])].Width = Unit.Percentage(3);
            if (e.CallbackName == "APPLYFILTER" && e.Args[0] == "")
            {
                DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
                ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
                cmbFiltra.Text = "";

            }
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            GridUtil.ToolTipButonaveMbiGride(gvKategorite, cultinf, rm);


        }



        /// <summary>
        /// perdoret per te vendosur filtrat tek headeri i grides
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvKategorite_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            if (e.Column.FieldName == "Emertimi" || e.Column.FieldName == "Kategori")
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
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
            //filtra.mbushFiltraGridaSipasFiltraEmri(cmbFiltra.Text, idNdermarrje);
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvKategorite", komponente, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), int.Parse(cmbKonfigurimi.Value.ToString()));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();
                mbushComboBoxFiltra(idNdermarrje, DbCore.mySessionObjects.ktheGjuhe(Session), int.Parse(cmbKonfigurimi.Value.ToString()));
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
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvKategorite", komponente, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gvKategorite.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("Kategori", gvKategorite);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvKategorite.GetSortedColumns();
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
            //    filtri.KoloneRenditje = "Kategori";
            //    filtri.DrejtimRenditje = true;
            //}
            DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();
            oPerdorues = DbCore.mySessionObjects.kthePerdorues(Session);
            filtri.IdPerdoruesi = oPerdorues.IdPerdorues;
            filtri.IdNdermarje = idNdermarrje;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            filtri.IdStatusDok = 1;

            mesazh = filtri.ruaj();
            mbushComboBoxFiltra(idNdermarrje, DbCore.mySessionObjects.ktheGjuhe(Session), int.Parse(cmbKonfigurimi.Value.ToString()));
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
                rreshtat = gvKategorite.GetSelectedFieldValues("ID");
            else
            {
                rreshtat = new List<object>();
                rreshtat.Add(hfId.Value);
            }
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
                DbCore.DbInventari.clsSerialeUnikeKategori serial = new DbCore.DbInventari.clsSerialeUnikeKategori(Convert.ToInt32(id));
                DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti() { IdKonfigAmbjente = int.Parse(cmbKonfigurimi.Value.ToString()) };
                konf.mbushKonfigAmbjSipasId(konf.IdKonfigAmbjente);
                bool lidhur = dbRegjistrim.eshteDokumentiILidhurCelje(serial.ID.ToString(), konf.IdNivel.ToString());
                if (lidhur)
                {
                    TePaFshire.Add(serial.Kategori);
                    continue;
                }
                serial.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                mesazh = serial.Fshi();
                if (serial.ID == 0)
                    continue;
                if (mesazh.Status)
                {
                    #region Heq llogarite nga grida
                    hiqNgaGrida(serial.ID, serial.IdNdermarje);
                    #endregion
                    TeFshire.Add(serial.Kategori);

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
        /// <param name="idndermarje">id e ndermarjes</param>
        ///<param name="idburim"> id e burimit</param>
        private void hiqNgaGrida(int id, int idndermarje)
        {
            if (gvKategorite.DataSource != null)
            {
                DataTable dt = (DataTable)gvKategorite.DataSource;
                DataRow[] drs = dt.Select(String.Format("ID = '{0}'", id));
                if (drs.Length > 1)
                    throw new Exception(gabim2burimenegride);
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                dt.Rows.Remove(dr);
                gvKategorite.DataBind();
            }
            else mbushGridKategoriNgaDB(idndermarje);
        }

        /// <summary>
        /// shton nte gride rreshtat e rinj
        /// </summary>
        /// <param name="idburim"> id e burimit</param>
        /// <param name="idNdermarrje">id e ndermarjes</param>
        private void shtoNeGrid(int idNdermarrje, int id)
        {
            if (gvKategorite.DataSource != null)
            {
                DataTable dt = (DataTable)gvKategorite.DataSource;
                DataRow[] drs = dt.Select("ID = " + id);
                if (drs.Length > 0)
                    throw new Exception(gabim2burimenegride);
                DataRow newArtDr = DbCore.DbInventari.colSerialeUnikeKategori.merrSerialeNdermarjeDR(id);
                if (newArtDr != null)
                    dt.ImportRow(newArtDr);
                else mbushGridKategoriNgaDB(idNdermarrje);
            }
            else mbushGridKategoriNgaDB(idNdermarrje);
        }

        /// <summary>
        /// modifikon ne gride komponenten e modifikuar
        /// </summary>
        /// <param name="idburim"> id e burimit</param>
        /// <param name="idNdermarrje">id e ndermarjes</param>
        private void modifikoNeGrid(int idNdermarrje, int id)
        {
            if (gvKategorite.DataSource != null)
            {
                DataTable dt = (DataTable)gvKategorite.DataSource;
                DataRow[] drs = dt.Select("ID = " + id);
                if (drs.Length > 1)
                    throw new Exception(gabim2burimenegride);
                if (drs.Length == 0) return;
                    DataRow dr = drs[0];
                DataRow newArtDr = DbCore.DbInventari.colSerialeUnikeKategori.merrSerialeNdermarjeDR(id);
                if (newArtDr != null)
                {
                    object[] arr = newArtDr.ItemArray;
                    dr.ItemArray = arr;
                }
                else mbushGridKategoriNgaDB(idNdermarrje);
            }
            else mbushGridKategoriNgaDB(idNdermarrje);
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
                ruajKategori();
            }

        }

        /// <summary>
        /// konfiguron vlerat fillestare
        /// </summary>
        /// <param name="idNdermarrje">id e ndermarjes</param>
        private void konfiguroVleraFillestare(int idNdermarrje, ResourceManager rm, CultureInfo cultinf, int idGjuha)
        { //mbush komboboxet dhe gridat e faqes
            ASPxPageControl1.ActiveTabIndex = 0;
            //mbushListeTrupash();
            mbushGrideFushaImporti();

            ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idperdoruesi, idNdermarrje, cmbKonfigurimi, 159, rm, cultinf, idGjuha);
            cmbKonfigurimi.SelectedIndex = 0;
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()), idGjuha);
            hfKonffillestar.Value = String.Format("{0};{1}", konf.KodKonfigAmbjente, konf.PershkrimKonfigAmbjente);
            hfState.Set("colKonfigurimeFtp", JsonConvert.SerializeObject(new colKonfigurimFtp(idNdermarrje)));
        }

        /// <summary>
        /// mbush griden nga sesioni
        /// </summary>
        /// <param name="idNdermarrje">id e ndermarjes</param>
        private void mbushGridKategoriNgaSession(int idNdermarrje)
        {
            DataTable tmpObject;
            bool sukses = DbCore.mySessionObjects.merrGrideNgaSessioni(Session, out tmpObject);
            if (!sukses)
                mbushGridKategoriNgaDB(idNdermarrje);
            else
            {
                gvKategorite.DataSource = tmpObject;
                gvKategorite.DataBind();
                tmpObject.Dispose();
            }
        }

        /// <summary>
        /// mbush griden nga databaza
        /// </summary>
        /// <param name="idNdermarrje"> id e ndermarjes</param>
        private void mbushGridKategoriNgaDB(int idNdermarrje)
        {//mbush griden e popupit me te dhena            
            DataTable dt = DbCore.DbInventari.colSerialeUnikeKategori.merrSerialeNdermarjeDT(idNdermarrje);
            DbCore.mySessionObjects.ruajGrideNeSession(Session, dt);
            gvKategorite.DataSource = dt;
            gvKategorite.DataBind();
            dt.Dispose();
        }

        /// <summary>
        /// ruan burimet 
        /// </summary>
        private void ruajKategori()
        {
            DbCore.DbInventari.clsSerialeUnikeKategori seriali = new DbCore.DbInventari.clsSerialeUnikeKategori();
            if (Page.IsValid == false)
                return;
            else
            {
                try
                {
                    seriali = krijoKategori();
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
                    mesazh = seriali.Ruaj();
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
                    DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti() { IdKonfigAmbjente = int.Parse(cmbKonfigurimi.Value.ToString()) };
                    konf.mbushKonfigAmbjSipasId(konf.IdKonfigAmbjente);
                    eshteShtim = false;
                    seriali.ID = int.Parse(hfId.Value);
                    bool lidhur = dbRegjistrim.eshteDokumentiILidhurCelje(seriali.ID.ToString(), konf.IdNivel.ToString());
                    if (lidhur.ToString() != hfLidhur.Value)
                    {
                        mesazh.Status = false;
                        mesazh.PershkrimMesazhi = "Kategoria eshte e lidhur";
                    }
                    else
                        mesazh = seriali.Modifiko();
                    dbRegjistrim.Dispose();
                }
                if (mesazh.Status == true)
                {
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                    if (eshteShtim)
                        shtoNeGrid(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), seriali.ID);
                    else //modifikim
                        modifikoNeGrid(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), seriali.ID);
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
        /// krijon burimin sipas te dhenave
        /// </summary>
        /// <returns> burimin me te dhenat</returns>
        private DbCore.DbInventari.clsSerialeUnikeKategori krijoKategori()
        {//krijon nje banke sipas te dhenave te futura nga perdoruesi
            
            bool isshtim;
            if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim")
                isshtim = true;
            else
                isshtim = false;
            DbCore.DbShare.clsKonfigurimAmbjenti konfig = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konfig.mbushKonfigAmbjSipasKod(cmbKonfigurimi.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));

            DbCore.DbInventari.clsSerialeUnikeKategori serial = new DbCore.DbInventari.clsSerialeUnikeKategori(txtKodi.Text, txtEmertimi.Text, rbTipi.Value == null ? "TXT" : rbTipi.Value.ToString(), cbSimboliNdares.Checked ? "TAB" : txtSimboliNdares.Text, idperdoruesi, idNdermarrje, idstatusdok, cmbKonfigurimeFtpHf.Value, cbMeEmertimKolone.Checked);
            serial.ColSerialeFusha = ruajTrupFusha();
            return serial;
        }

        /// <summary>
        /// cakton properti ne javascript
        /// </summary>
        /// <param name="sender">derguesi </param>
        /// <param name="e">argumentat</param>
        protected void gvKategorite_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gvKategorite.PageIndex;
            e.Properties["cpPageRow"] = gvKategorite.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gvKategorite.VisibleRowCount;
        }

        /// <summary>
        /// kur grida ben callback
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvKategorite_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string idkomponente = "";
            string kodkonfigurimi = "";

            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                kodkonfigurimi = arr[1];
                idkomponente = arr[0];
                if (arr[2] == "")
                    gvKategorite.FilterExpression = "";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    //filtra.mbushFiltraGridaSipasFiltraKodi(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvKategorite", komponente, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), int.Parse(cmbKonfigurimi.Value.ToString()));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        gvKategorite.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvKategorite);
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

            gvKategorite.Selection.UnselectAll();
        }



        /// <summary>
        /// perdoret per te shfaqur po jo tek komboja e aktivit
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvKategorite_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {

        }

        /// <summary>
        /// per filtrimin me elementin bosh
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvKategorite_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {

        }


        #region Grida e Fushave te Importeve
        /// <summary>
        /// konfiguron griden e Fushave te Importit
        /// </summary>
        /// <param name="idNdermarrje">id e ndermarjes</param>
        private void konfiguroGrideImportiTrupi(int idNdermarrje)
        { //konfigurohet grida
            CultureInfo cultinf = mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            GridUtil.percaktoVisibleColumnsMeWidth(mySessionObjects.ktheGjuhe(Session), idNdermarrje, gvImporti, "gvImporti", komponente);
            GridUtil.konfiguroGrideRegjistrimEvogelPaTheme(gvImporti, "ID", false);
            gvImporti.Settings.UseFixedTableLayout = false;
        }

        private void mbushGrideFushaImporti()
        {
            colSerialeUnikeFusha col = new colSerialeUnikeFusha();
            col.merrTegjithaFushat();
            gvImporti.DataSource = col;
            gvImporti.DataBind();
        }




        /// <summary>
        /// mbush griden e Fushave te importit me fushat e kategorise perkatese
        /// </summary>
        /// <param name="id">id e aktivitetit</param>
        private void mbushListeFushashMod(int id)
        {//mbushet grida me te dhena
            colSerialeUnikeFusha colFushat = new colSerialeUnikeFusha();
            colFushat.merrTegjithaFushat();

            colSerialeUnikeFusha col = new colSerialeUnikeFusha(id);
            col.ForEach(x => {
                colFushat.ForEach(z =>
                {   if (x.ID == z.ID)
                    {
                        z.Emertimi = x.Emertimi;
                    }
                });
            });

            gvImporti.DataSource = colFushat;
            gvImporti.DataBind();
            col.ForEach(x =>{ gvImporti.Selection.SelectRowByKey(x.ID); });
        }


        /// <summary>
        /// perdoret per te shtuar kolonen e selektimit tek grida dhe per te vendosur disa karakteristika te grides
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void gvImporti_DataBound(object sender, EventArgs e)
        {// shton colonen # per selektim dhe disa karakteristika te grides
            if (gvKategorite.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per 
                //perzgjidh
                GridViewCommandColumn check = new GridViewCommandColumn("#") { ShowSelectCheckbox = true, Width = Unit.Percentage(2) };
                gvImporti.Columns.Add(check);
                gvImporti.Columns["IsSelected"].Visible = false;
                gvImporti.KeyFieldName = "ID";
                gvImporti.SettingsBehavior.AllowSelectByRowClick = true;
                gvImporti.SettingsBehavior.AllowFocusedRow = true;
            }

        }

        /// <summary>
        /// percakton templatet e kolonave te grides
        /// </summary>
        private void percaktoTemplateTrupashFushat()
        {//percaktohen templatet per fushat e grides
            GridViewDataTextColumn col1 = gvImporti.Columns["Fusha"] as GridViewDataTextColumn;
            col1.DataItemTemplate = new MyLabelTemplate();
            GridViewDataTextColumn col2 = gvImporti.Columns["Emertimi"] as GridViewDataTextColumn;
            col2.DataItemTemplate = new MyTextTemplate();

        }


        /// <summary>
        /// krijon koleksionin me burimet e ketij aktiviteti me te dhenat e grides
        /// </summary>
        /// <param name="idNdermarrje">id e ndermarjes</param>
        /// <returns>koleksionin me burimet</returns>
        private colSerialeUnikeFusha ruajTrupFusha()
        {
           var trupi =  JsonConvert.DeserializeObject<colSerialeUnikeFusha>(hfFushat.Value);
            trupi.RemoveAll(x =>x.IsSelected == false);
            return trupi;
        }






        /// <summary>
        /// perdoret ne rastet kur grida ben callback per ta rimbushur griden
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void gvImporti_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            gvImporti.DataBind();
        }

        /// <summary>
        /// kur grida ben callback
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvImporti_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {//kur grida ben callback te ruajme te dhenat
            int key = -1;
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("gvImporti"))
                {
                    if (e.Parameters.Split(';').Length == 2)
                    {
                        if (e.Parameters.Split(';')[1] == "modifiko")
                        {

                            mbushListeFushashMod(int.Parse(e.Parameters.Split(';')[0]));
                        }
                        else
                        {
                            mbushGrideFushaImporti();
                        }

                    }
                    else
                    {


                        if (e.Parameters.ToString() != "")
                        {
                            key = int.Parse(e.Parameters.ToString());
                        }
                        colSerialeUnikeFusha trupi = new colSerialeUnikeFusha();
                        trupi = ruajTrupFusha();
                        if (key != -1)
                            trupi.RemoveAt(key);
                        else
                        {
                            trupi.Add(new clsSerialeUnikeFusha());
                        }
                        if (trupi.Count == 0)
                        {
                            trupi.Add(new clsSerialeUnikeFusha());
                        }
                        this.gvImporti.DataSource = trupi;
                        this.gvImporti.DataBind();
                        percaktoTemplateTrupashFushat();

                    }
                }
            }
        }


        /// <summary>
        /// cakton properti ne javascript
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvImporti_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpImportNoRows"] = gvImporti.VisibleRowCount;
        }

        /// <summary>
        /// kur krijohen rreshtat e grides
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvImporti_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {//krijon rreshat sipas modelit
            if (e.RowType == DevExpress.Web.GridViewRowType.Data)
            {
                GridViewDataTextColumn col1 = ((ASPxGridView)sender).Columns["Fusha"] as GridViewDataTextColumn;
                GridViewDataTextColumn col2 = ((ASPxGridView)sender).Columns["Emertimi"] as GridViewDataTextColumn;

                ASPxLabel lbl1 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col1, "lbl") as ASPxLabel;
                ASPxTextBox txt1 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col2, "txtBox") as ASPxTextBox;


                if (lbl1 != null)
                {
                    lbl1.ClientInstanceName = String.Format("Fusha{0}", e.VisibleIndex);
                }
                if (txt1 != null)
                {
                    txt1.ClientInstanceName = "Emertimi" + e.VisibleIndex.ToString();
                }

            }


        }


        #endregion
    }
}