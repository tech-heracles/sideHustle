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
using PlatinumWeb.Templates;
using System.Globalization;
using System.Resources;
using DbCore;
using DbCore.IMBUtils.Types;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class Shto_RregullaAmortizimi : MyPageBase
    {
       


        public static bool isShtim = true;
        public static int id = 0;
        private int idgjuha, idPerdoruesi, idNdermarrje, idviti;
        protected void Page_Init(object sender, EventArgs e)
        {

        }

        private string komponente = "Shto_RregullaAmortizimi.aspx";
        private string guidString;
        protected void Page_Load(object sender, EventArgs e)
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
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
            idviti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            idgjuha = DbCore.mySessionObjects.ktheGjuhe(Session);

            percaktoTemplateMenu(idgjuha, idviti, idPerdoruesi, idNdermarrje, ASPxMenu1);

            if (!Page.IsPostBack)
            {
                mbushHiddenFieldMePerkthime(cultinf, rm);
                EmrateTabeve(rm, cultinf);
                hfState.Set("idGjuha", idgjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                guidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                hfState.Set("guidString", guidString);
                ASPxPageControl1.ActiveTabIndex = 0;
                konfiguroVleraFillestare(idPerdoruesi, idNdermarrje, rm, cultinf, idgjuha);
                mbushGridNgaDB();
                konfiguroGride(cmbKonfigurimi.Text.Split(';')[0], 1002);
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), "gvRregullat", gvRregullat, cmbKonfigurimi.Text.Split(';')[0], "1002", (int)hfState["idGjuha"]);
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idviti, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idviti, komponente);
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                gvRregullat.Columns["#"].VisibleIndex = 0;
            }
            else
            {
                guidString = (string)hfState["guidString"];
                mbushGridNgaSession();
                konfiguroGride(cmbKonfigurimi.Text.Split(';')[0], 1002);
            }
            GridUtil.konfigGrideListeEMadhePaTheme(gvRregullat, "IdKarakteristika");
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvRregullat", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
            GridUtil.EmrateButonaveMbiGride(gvRregullat);
            AspxWebControlUtils.perkthePopUp(popFshi, rm.GetString("labelKujdes", cultinf), lblMsgbox, rm.GetString("labelAdministrimiMsgJeniSigurt", cultinf), ButtonCancel, rm.GetString("labelAnullo", cultinf));
            konfigurimi_Label.Text = rm.GetString("lblModeli", cultinf);
        }

        /// <summary>
        /// Metode qe sherben per te kaluar perkthime te js
        /// </summary>
        /// <param name="cultinf">Merr culture info</param>
        /// <param name="rm">Merr Resource Manager</param>
        private void mbushHiddenFieldMePerkthime(CultureInfo cultinf, ResourceManager rm)
        {
            hfState.Set("msgCeljeMagazinatDuhetTeZgjidhniNjeMagazine", rm.GetString("msgCeljeMagazinatDuhetTeZgjidhniNjeMagazine", cultinf));
        }

        /// <summary>
        /// Vendos emrat e tabeve ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateTabeve(System.Resources.ResourceManager rm, System.Globalization.CultureInfo cultinf)
        {
            ASPxPageControl1.TabPages[0].Text = rm.GetString("TePergjithshmeTab", cultinf);
            ASPxPageControl1.TabPages[1].Text = rm.GetString("rregullaAmortizimiTab", cultinf);
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
            DbCore.clsMesazh mesazh = GridUtil.ruajFiltra(idNdermarrje, idPerdoruesi, idgjuha, "gvRregullat", komponente, "FilterDefault", gvRregullat.FilterExpression, gvRregullat, "IdKarakteristika", idkonf, out idfiltri);//ruan filtrin e zgjedhur tek filtrat
            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                return;

            }
            mesazh = GridUtil.ruajkonfigurimgride(gvRregullat, cmbKonfigurimi.Text, idNdermarrje, idPerdoruesi, 1002, idfiltri, idviti, DbCore.mySessionObjects.ktheCultureInfo(Session), DbCore.mySessionObjects.ktheGjuhe(Session));/// ruan konfigurimin e grides dhe filtrin

            clsToolbarConfig.mbushComboBoxFiltra(idgjuha, idNdermarrje, "gvRregullat", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);

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
        protected void gvRregullat_DataBound(object sender, EventArgs e)
        {// shton colonen # per selektim dhe disa karakteristika te grides
            if (this.gvRregullat.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per 
                //perzgjidh
                DevExpress.Web.GridViewCommandColumn check = new DevExpress.Web.GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true;

                check.Width = Unit.Percentage(2);
                //behet per te afishuar rreshtin qe do sherbej per filtrim
                gvRregullat.Settings.ShowFilterRow = true;
                gvRregullat.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                gvRregullat.Settings.ShowFilterRowMenu = true;
                gvRregullat.Columns.Add(check);
                gvRregullat.KeyFieldName = "IdKarakteristika";
                gvRregullat.SettingsBehavior.AllowSelectByRowClick = true;
                gvRregullat.SettingsBehavior.AllowFocusedRow = true;
            }

        }
        private void konfiguroGride(string kodKonfigurimi, int idKomponente)
        {//konfiguron griden

            KonfigurimComboGride.Shto_DateFillimiAmortizimi(gvRregullat, idgjuha, Session, komponente, guidString);
            KonfigurimComboGride.shto_DateMbarimiAmortizimi(gvRregullat, idgjuha, Session, komponente, guidString);
            KonfigurimComboGride.ShtoStandart(gvRregullat, idNdermarrje, Session, komponente, guidString, "IdStandart");
            KonfigurimComboGride.ShtoKodifikim(gvRregullat, idNdermarrje, Session, komponente, guidString, "IdKodifikimArtikulli");
          
            percaktoTamplateAutorizime();
            this.gvRregullat.Columns["#"].VisibleIndex = 0;

        }
      
        //thirret sa here qe behet Callback, dhe ben edhe njeher lidhjen me datasource-in
        protected void gvRregullat_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            if (e.CallbackName == "COLUMNMOVE" && gvRregullat.AllColumns[int.Parse(e.Args[0])].Width.Value == 0)
                gvRregullat.AllColumns[int.Parse(e.Args[0])].Width = Unit.Percentage(3);
            if (e.CallbackName == "APPLYFILTER" && e.Args[0] == "")
            {
                DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
                ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
                cmbFiltra.Text = "";


            }
            percaktoTamplateAutorizime();
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            GridUtil.EmrateButonaveMbiGride(gvRregullat);
        }

        //bere  me e konfigurueshme
        protected void gvRregullat_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {

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
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvRregullat", komponente, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvRregullat", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
                percaktoTemplateMenu(idgjuha, idviti, idPerdoruesi, idNdermarrje, ASPxMenu1);

                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                hfStatusi.Value = "true";
                gvRregullat.FilterExpression = String.Empty;
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
            filtri.FiltraUniversal = false;// Universal_ASPxCheckBox.Checked;
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvRregullat", komponente, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gvRregullat.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("IdKarakteristika", gvRregullat);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvRregullat.GetSortedColumns();
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
            //    filtri.KoloneRenditje = "IdKarakteristika";
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
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvRregullat", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
            percaktoTemplateMenu(idgjuha, idviti, idPerdoruesi, idNdermarrje, ASPxMenu1);

            if (mesazh.Status == true)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            hfStatusi.Value = "true";
            cmbFiltra.Text = "";

        }
        /// <summary>
        /// perdoret per te fshire reshtat e zgjedhur ne gride pasi perdoruesi ka konfirmuar fshirjen
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            List<object> rreshtat = gvRregullat.GetSelectedFieldValues("IdKarakteristika");
            if (rreshtat.Count == 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgZgjidhniTePaktenNjeRegull",cultinf), pnlMesazhi);
                return;
            }
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            List<string> TeFshire = new List<string>(), TePaFshire = new List<string>();
            int idPerdorues = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            DbCore.DbRegjistrim.clsDatabaseRegjistrim dbRegjistrim = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
            foreach (object id in rreshtat)
            {
                DbCore.DbAsete.clsKarakteristikaStandarti karakteristika = new DbCore.DbAsete.clsKarakteristikaStandarti();
                karakteristika.merrKonfigurimStandartiSipasID(Convert.ToInt32(id));
                if (karakteristika.IdKarakteristika == 0)
                    continue;
                DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti(karakteristika.IdKonfig);

                bool lidhur = dbRegjistrim.eshteDokumentiILidhurCelje(karakteristika.IdKarakteristika.ToString(), DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdNivel(karakteristika.IdKonfig).ToString());
                if (lidhur)
                {
                    DbCore.DbAsete.clsStandarteAmortizim st = new DbCore.DbAsete.clsStandarteAmortizim(karakteristika.IdStandart);
                    TePaFshire.Add(st.Emertimi);
                    continue;
                }
                mesazh = DbCore.DbAsete.clsKarakteristikaStandarti.fshi(karakteristika.IdKarakteristika, idPerdorues);

                if (mesazh.Status)
                {
                    #region Heq llogarite nga grida

                    hiqNjesiNgaGrida(karakteristika.IdKarakteristika);
                    #endregion
                    DbCore.DbAsete.clsStandarteAmortizim st = new DbCore.DbAsete.clsStandarteAmortizim(karakteristika.IdStandart);
                    TeFshire.Add(st.Emertimi);
                    ASPxPageControl1.ActiveTabIndex = 0;
                    hfStatusi.Value = "true";
                }
            }
            dbRegjistrim.Dispose();
            string mesazhInfoGabim = "", mesazhInfoSukses = "";
            if (TePaFshire.Count == 1)
                mesazhInfoGabim = String.Format("{0}{1}{2}", rm.GetString("msgRegulliMeKod", cultinf), String.Join(";", TePaFshire), rm.GetString("msgShtoLlogariSuffixNjejesGabimi",cultinf));
            else
                if (TePaFshire.Count > 1)
                    mesazhInfoGabim = String.Format("{0}{1}{2}", rm.GetString("msgRregullatMeKod", cultinf), String.Join(";", TePaFshire), rm.GetString("msgShtoLlogariSuffixShumesGabimi", cultinf));
            if (TeFshire.Count == 1)
                mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("msgRegulliMeKod", cultinf), String.Join(";", TeFshire), rm.GetString("regjMagSuffixMesazhNjejesSuksesi", cultinf));
            else
                if (TeFshire.Count > 1)
                    mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("msgRregullatMeKod", cultinf), String.Join(";", TeFshire), rm.GetString("msgCeljeMagazinatSuffixShumesSuksesi", cultinf));
            if (mesazhInfoGabim != "" && mesazhInfoSukses != "")
                mesazhInfoGabim += rm.GetString("msgLidhesMesazhi", cultinf) + mesazhInfoSukses;
            if (mesazhInfoGabim != "")
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhInfoGabim, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazhInfoSukses, pnlMesazhi);
        }

        private void hiqNjesiNgaGrida(int idkarakteristika)
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (this.gvRregullat.DataSource != null)
            {
                DataTable dt = (DataTable)gvRregullat.DataSource;
                DataRow[] drs = dt.Select("IdKarakteristika = " + idkarakteristika);
                if (drs.Length > 1)
                    throw new Exception(rm.GetString("msgGabimGjendenDyRegullaMeTeNjejtenId",cultinf));
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                dt.Rows.Remove(dr);
                gvRregullat.DataBind();
            }
            else mbushGridNgaDB();
        }
        private void shtoNjesiNeGrid(int idKarakteristika)
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (gvRregullat.DataSource != null)
            {
                DataTable dt = (DataTable)gvRregullat.DataSource;
                DataRow[] drs = dt.Select("IdKarakteristika = " + idKarakteristika);
                if (drs.Length > 0)
                    throw new Exception(rm.GetString("msgGabimRregulliEkzistonNeGride",cultinf));
                DataRow newArtDr = DbCore.DbAsete.clsKarakteristikaStandarti.merrSipasNjesiNdermarrjesDR(idKarakteristika);
                dt.ImportRow(newArtDr);
            }
            else mbushGridNgaDB();
        }
        private void modifikoNjesiNeGrid(int idKarakteristika)
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (gvRregullat.DataSource != null)
            {
                DataTable dt = (DataTable)gvRregullat.DataSource;
                DataRow[] drs = dt.Select("IdKarakteristika = " + idKarakteristika);
                if (drs.Length > 1)
                    throw new Exception(rm.GetString("msgGabimGjendenDyRegullaMeTeNjejtenId", cultinf));
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                DataRow newArtDr = DbCore.DbAsete.clsKarakteristikaStandarti.merrSipasNjesiNdermarrjesDR(idKarakteristika);

                object[] arr = newArtDr.ItemArray;
                dr.ItemArray = arr;
            }
            else mbushGridNgaDB();
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
                Page.Validate("entries"); ruajRregull();
            }
        }
        protected void gvRregullat_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if (e.Column.FieldName == "IdKodifikimArtikulli")
                if (Converter.ConvertToInt(e.Value) == -1)
                {
                    e.Criteria = null;
                }
            if (e.Column.FieldName == "IdStandart" || e.Column.FieldName == "IdFillimAmortizimi" || e.Column.FieldName == "IdMbarimAmortizimi")
            {
                if (Converter.ConvertToInt(e.Value)==0 || Converter.ConvertToInt(e.Value) == -1)
                {
                    e.Criteria = null;
                }
            }
        }

        protected void gvRregullat_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (e.Column.FieldName == "Kontabilizim" || e.Column.FieldName == "PerfshihetDitaPare")
            {
                (e.Editor as ASPxComboBox).Items.Clear();
                (e.Editor as ASPxComboBox).Items.Add("");
                (e.Editor as ASPxComboBox).Items.Add(rm.GetString("cmbboxItemFilterAvancPo",cultinf), true);
                (e.Editor as ASPxComboBox).Items.Add(rm.GetString("cmbFilterJo",cultinf), false);
            }
        }
        protected void gvRregullat_CustomJSProperties(object sender, DevExpress.Web.ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gvRregullat.PageIndex;
            e.Properties["cpPageRow"] = gvRregullat.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gvRregullat.VisibleRowCount;
        }


        private void konfiguroVleraFillestare(int idPerdoruesi, int idNdermarrje, System.Resources.ResourceManager rm, System.Globalization.CultureInfo ci, int idGjuha)
        { //mbush komboboxet dhe gridat e faqes  
            ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 85, rm, ci, idGjuha);
            cmbKonfigurimi.SelectedIndex = 0;
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()), idGjuha);
            ConfigureAspxComboBox.mbushComboKodifikimNiveli1(idNdermarrje, cmbGrupi, 1, true);
            cmbGrupi.Items.Add(rm.GetString("cmbboxItemFilterAvancTeGjitha", ci), 0);
            cmbGrupi.Items.Move(cmbGrupi.Items.Count - 1, 0);
            ConfigureAspxComboBox.mbushComboStandartAmortizimi(cmbStandarti, idNdermarrje);
            ConfigureAspxComboBox.mbushComboDateAmortizimi(cmbDtFillimi, true,idGjuha);
            ConfigureAspxComboBox.mbushComboDateAmortizimi(cmbDtMbarimi, false, idGjuha);

            hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;
            //cmbKonfigurimi.SelectedIndex = -1;
        }


        private void mbushGridNgaSession()
        {
            DataTable tmpObject;
            bool sukses = DbCore.mySessionObjects.merrGrideNgaSessioni(Session, out tmpObject);
            if (!sukses)
                mbushGridNgaDB();
            else
            {
                gvRregullat.DataSource = tmpObject;
                gvRregullat.DataBind();
                tmpObject.Dispose();
            }
        }

        private void mbushGridNgaDB()
        {//mbush griden e popupit me te dhena            
            DataTable dt = DbCore.DbAsete.colKarakteristikaStandarti.merrSipasNjesiNdermarrjesDT(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            DbCore.mySessionObjects.ruajGrideNeSession(Session, dt);
            gvRregullat.DataSource = dt;
            gvRregullat.DataBind();
            dt.Dispose();
        }


        private void percaktoTamplateAutorizime()
        {//templatet per kolonat e Autorizimeve

            GridViewDataColumn col = gvRregullat.Columns["PerfshihetDitaPare"] as GridViewDataColumn;
            col.DataItemTemplate = new MyCheckTemplate(true, false);
            GridViewDataColumn col1 = gvRregullat.Columns["Kontabilizim"] as GridViewDataColumn;
            col1.DataItemTemplate = new MyCheckTemplate(true, false);

        }
        private void ruajRregull()
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            DbCore.DbAsete.clsKarakteristikaStandarti rregull = new DbCore.DbAsete.clsKarakteristikaStandarti();
            if (Page.IsValid == false)
                return;
            else
            {


                bool eshteShtim;
                if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim")
                {

                    eshteShtim = true;
                }
                else eshteShtim = false;
                try
                {
                    rregull = krijoRregull(eshteShtim);
                }
                catch (DbCore.MyException e)
                {
                    NLog.LogManager.GetCurrentClassLogger().Error(e.Message);
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, e.Message, pnlMesazhi);
                    hfStatusi.Value = "false";
                    return;
                }
                catch (Exception err)
                {
                    NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNdodhiGabimGjateKrijimitTeRregullit", cultinf), pnlMesazhi);
                    hfStatusi.Value = "false";
                    return;
                }
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), komponente);

                if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim")
                {
                    if (!tedrejtaInfo.DShtim)
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNukKeniTeDrejta",cultinf), pnlMesazhi);
                        hfStatusi.Value = "false";
                        return;
                    }
                    //mesazh = njesia.ruaj();
                    mesazh = rregull.ruaj();
                    eshteShtim = true;
                }
                else
                {
                    if (!tedrejtaInfo.DMod)
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNukKeniTeDrejta", cultinf), pnlMesazhi);
                        hfStatusi.Value = "false";
                        return;
                    }
                    DbCore.DbRegjistrim.clsDatabaseRegjistrim dbRegjistrim = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
                    DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                    konf.IdKonfigAmbjente = rregull.IdKonfig;
                    konf.mbushKonfigAmbjSipasId(konf.IdKonfigAmbjente);
                    eshteShtim = false;
                    rregull.IdKarakteristika = int.Parse(hfId.Value.ToString());
                    bool lidhur = dbRegjistrim.eshteDokumentiILidhurCelje(rregull.IdKarakteristika.ToString(), konf.IdNivel.ToString());
                    if (lidhur.ToString() != hfLidhur.Value)
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgRregulliEshteILidhur", cultinf), pnlMesazhi);
                        return;
                    }
                    mesazh = rregull.modifiko();
                    dbRegjistrim.Dispose();
                }
                if (mesazh.Status)
                {
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("labelRaportMesazhRuajtjaPerfundoiSukses",cultinf), pnlMesazhi);
                    if (eshteShtim)
                        mbushGridNgaDB();  // shtoNjesiNeGrid(rregull.IdKarakteristika);
                    else //modifikim
                        modifikoNjesiNeGrid(rregull.IdKarakteristika);
                    hfStatusi.Value = "true";
                    return;
                }
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                hfStatusi.Value = "false";

            }


        }

        /// <summary>
        /// krijon magazinen qe do te ruhet
        /// </summary>
        /// <returns>kthen clsNjesiAdministrative me magazinen qe do te ruhet</returns>
        private DbCore.DbAsete.clsKarakteristikaStandarti krijoRregull(bool shtim)
        {//krijon nje llogari sipas te dhenave te futura nga perdoruesi
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DbCore.DbShare.clsKonfigurimAmbjenti konfig = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konfig.mbushKonfigAmbjSipasKod(cmbKonfigurimi.Text, idNdermarrje);

            int idstandart = 0;
            if (cmbStandarti.Text != "")
                idstandart = int.Parse(cmbStandarti.Value.ToString());
            int idgrup = 0;
            if (cmbGrupi.Text != "")
                idgrup = int.Parse(cmbGrupi.Value.ToString());
            int iddtfill = 0;
            if (cmbDtFillimi.Text != "")
                iddtfill = int.Parse(cmbDtFillimi.Value.ToString());
            int iddtmbarim = 0;
            if (cmbDtMbarimi.Text != "")
                iddtmbarim = int.Parse(cmbDtMbarimi.Value.ToString());
            DbCore.DbAsete.clsKarakteristikaStandarti rregull = new DbCore.DbAsete.clsKarakteristikaStandarti(0, idstandart, idgrup, konfig.IdKonfigAmbjente, iddtfill, iddtmbarim, cbPerfshiDite.Checked, cbKontabilizim.Checked, idNdermarrje, 1, DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), krijotrupin(idNdermarrje));

            return rregull;
        }
        private DbCore.DbAsete.colKarakteristikaStandartiTrupi krijotrupin(int idNdermarrje)
        {
            DbCore.DbAsete.colKarakteristikaStandartiTrupi col = new DbCore.DbAsete.colKarakteristikaStandartiTrupi();
            DbCore.DbAsete.clsStatusMagazine_Asete status = new DbCore.DbAsete.clsStatusMagazine_Asete("Aktive", idNdermarrje);
            DbCore.DbAsete.clsKarakteristikaStandartiTrupi trupaktive = new DbCore.DbAsete.clsKarakteristikaStandartiTrupi(0, 0, status.IdStatusMagazine, cbAktive.Checked, txtAktivePas.Text != "" ? int.Parse(txtAktivePas.Text) : 0);
            col.Add(trupaktive);
            DbCore.DbAsete.clsStatusMagazine_Asete statusin = new DbCore.DbAsete.clsStatusMagazine_Asete("Inaktive", idNdermarrje);
            DbCore.DbAsete.clsKarakteristikaStandartiTrupi trupinaktive = new DbCore.DbAsete.clsKarakteristikaStandartiTrupi(0, 0, statusin.IdStatusMagazine, cbInaktive.Checked, txtInaktivePas.Text != "" ? int.Parse(txtInaktivePas.Text) : 0);
            col.Add(trupinaktive);
            DbCore.DbAsete.clsStatusMagazine_Asete statusrip = new DbCore.DbAsete.clsStatusMagazine_Asete("Riparim", idNdermarrje);
            DbCore.DbAsete.clsKarakteristikaStandartiTrupi trupriparim = new DbCore.DbAsete.clsKarakteristikaStandartiTrupi(0, 0, statusrip.IdStatusMagazine, cbRiparim.Checked, txtRiparimPas.Text != "" ? int.Parse(txtRiparimPas.Text) : 0);
            col.Add(trupriparim);
            DbCore.DbAsete.clsStatusMagazine_Asete statusdeinstalim = new DbCore.DbAsete.clsStatusMagazine_Asete("Deinstalim", idNdermarrje);
            DbCore.DbAsete.clsKarakteristikaStandartiTrupi trupdeinstalim = new DbCore.DbAsete.clsKarakteristikaStandartiTrupi(0, 0, statusdeinstalim.IdStatusMagazine, cbDeinstalim.Checked, txtDeinstalimPas.Text != "" ? int.Parse(txtDeinstalimPas.Text) : 0);
            col.Add(trupdeinstalim);
            return col;
        }


        /// <summary>
        /// perdoret per te rikonfiguruar griden kur ndryshohet konfigurimi
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvRregullat_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string idkomponente = "";
            string kodkonfigurimi = "";

            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                kodkonfigurimi = arr[1];
                idkomponente = arr[0];
                if (arr[2] == "")
                    gvRregullat.FilterExpression = "";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvRregullat", komponente, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), int.Parse(cmbKonfigurimi.Value.ToString()));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        gvRregullat.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvRregullat);
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
            gvRregullat.Selection.UnselectAll();
        }
    }
}