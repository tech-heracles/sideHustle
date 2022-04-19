using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Data;


using System.Resources;
using System.Globalization;
using DbCore;
using DbCore.DbShare;
using DbCore.DbAdmin;
using System.Web.SessionState;
using DbCore.DbRegjistrim;
using DbCore.IMBUtils.Types;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{
    public partial class FleteDoganore : MyPageBase
    {
        private const string prefixMesazhNjejes = "Fleta Doganore me Nr: ";
        private const string prefixMesazhShumes = "Fleta Doganore me Nr: ";
        private const string suffixMesazhNjejesLidhurGabimi = " është i lidhur dhe nuk mund të fshihet! ";
        private const string suffixMesazhShumesLidhurGabimi = " janë të lidhura dhe nuk mund të fshihen! ";
        private const string suffixMesazhNjejesPeriudheKycurGabimi = " i përket një periudhe të kyçur dhe nuk mund të fshihet! ";
        private const string suffixMesazhShumesPeriudheKycurGabimi = " i përkasin periudhave të kycura dhe nuk mund të fshihen! ";
        private const string suffixMesazhNjejesSuksesi = " u fshi me sukses!";
        private const string suffixMesazhShumesSuksesi = " u fshinë me sukses!";
        private const string lidhesMesazhi = ". Kurse ";
        private int idPerdoruesi, idviti, idgjuha, idNdermarrje;
        private string guidString;
        private string komponente => DbCore.clsFunksione.GetKomponente(Page.Request);
        //DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();

        //string veprimi;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!DbCore.mySessionObjects.isLogedIn(Session))
                DbCore.clsFunksione.logout(Session, true, "FaqePaautorizuar");
            idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerdoruesi);
            string datanga, dataderi;
            int idgjuha, idviti, idNdermarrje, idNdermarrjeVit;
            idgjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            if (hfState.Count == 0)
            {
                idgjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
                idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                idviti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
                idNdermarrjeVit = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
                hfState.Set("idPerdoruesi", idPerdoruesi);
                hfState.Set("idGjuha", idgjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("idViti", idviti);
                hfState.Set("idNdermarrjeVit", idNdermarrjeVit);
            }
            else
            {
                idPerdoruesi = (int)hfState["idPerdoruesi"];
                idNdermarrje = (int)hfState["idNdermarrje"];
                idviti = (int)hfState["idViti"];
                idgjuha = (int)hfState["idGjuha"];
                idNdermarrjeVit = (int)hfState["idNdermarrjeVit"];
            }

            var ci = DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo(idgjuha);
            var rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (!IsPostBack)
            {
                guidString = Guid.NewGuid().ToString();
                EmrateLabelave(ci, rm);
                mbushHiddenFieldMePerkthime(ci, rm);
                if (Request.QueryString["lloji"] == "import")
                    ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 55, "LDFDI", rm, ci, idgjuha);
                else ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 55, "LDFDE", rm, ci, idgjuha);
                cmbKonfigurimi.SelectedIndex = 0;
                DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()), idgjuha);
                hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;
                string periudheDok = clsAlternativaKushti.getAlternativa(konf.IdKonfigAmbjente, "SHDPER");
                clsPeriudhaKontabel oPeriudha = DbCore.mySessionObjects.merrPeriudheKontabel(Session);
                clsFunksione.ruajPeriudhatNeHiddenField(IsPostBack, hfState, periudheDok, oPeriudha, out datanga, out dataderi);
                DbCore.mySessionObjects.ruajGrideNeSession(komponente + "&periudhaDok=Aktuale" + idNdermarrjeVit, Session, null);
                DbCore.mySessionObjects.ruajGrideNeSession(komponente + "&periudhaDok=3 Mujore" + idNdermarrjeVit, Session, null);
                DbCore.mySessionObjects.ruajGrideNeSession(komponente + "&periudhaDok=Vit ushtrimor" + idNdermarrjeVit, Session, null);
                DbCore.mySessionObjects.ruajGrideNeSession(komponente + "&periudhaDok=Javore" + idNdermarrjeVit, Session, null);
                DbCore.mySessionObjects.ruajGrideNeSession(komponente + "&periudhaDok=Ditore" + idNdermarrjeVit, Session, null);
                mbushGridFleteDoganoreNgaDB(komponente, hfState.Get("periudhaDok").ToString(), idNdermarrjeVit, idNdermarrje, idPerdoruesi, datanga, dataderi);
                if (Request.QueryString["lloji"] == "import")
                    grid_FleteDoganore.FilterExpression = "[ImportExport]=1 and [IdStatusDok]=1";
                else grid_FleteDoganore.FilterExpression = "[ImportExport]=2 and [IdStatusDok]=1";

                if (Request.QueryString["fshi"] == "po")
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("regjMagFshirjaPerfundoiMeSukses", ci), pnlMesazhi);
                else if (Request.QueryString["ruaj"] == "po")
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("regjMagModifikimiPerfundoiMeSukses", ci), pnlMesazhi);
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idviti, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
                konfiguroGride(idNdermarrje, idPerdoruesi, idgjuha,rm,ci);
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "grid_FleteDoganore", grid_FleteDoganore, cmbKonfigurimi.Text.Split(';')[0], "523", DbCore.mySessionObjects.ktheGjuhe(Session));
                hfState.Set("guidString", guidString);
            }
            else
            {
                guidString = (string)hfState.Get("guidString");
                ASPxRadioButtonList radDtDok = (grid_FleteDoganore.FindTitleTemplateControl("radDtDok") as ASPxRadioButtonList);
                string periudhaSelektuar;
                if (hfState.Contains("PeriudhaSelektuar"))
                {
                    //if (radDtDok.SelectedItem == null || radDtDok.SelectedIndex == -1)
                    radDtDok.SelectedIndex = Convert.ToInt16(hfState.Get("PeriudhaSelektuar"));
                    periudhaSelektuar = radDtDok.SelectedItem.Text;
                }
                else
                {
                    periudhaSelektuar = hfState.Get("Periudha").ToString();
                    radDtDok.SelectedItem = radDtDok.Items.FindByText(periudhaSelektuar);
                }
                clsFunksione.ruajPeriudhatNeHiddenField(IsPostBack, hfState, periudhaSelektuar, null, out datanga, out dataderi);
                mbushGridFleteDoganoreNgaSession(komponente, hfState.Get("periudhaDok").ToString(), DbCore.mySessionObjects.ktheNdermarrjeVit(Session), idNdermarrje, idPerdoruesi, datanga, dataderi);
                konfiguroGride(idNdermarrje, idPerdoruesi, idgjuha,rm,ci);
            }
            GridUtil.konfigGrideListeEMadhePaTheme(grid_FleteDoganore, "IdFleteDoganoreKoka");
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "grid_FleteDoganore", int.Parse(cmbKonfigurimi.Value.ToString()), "FleteDoganore.aspx");
            AspxWebControlUtils.perkthePopUp(popFshi, rm.GetString("labelKujdes", ci), lblMsgbox, rm.GetString("labelAdministrimiMsgJeniSigurt", ci), ButtonCancel, rm.GetString("labelAnullo", ci));
            GridUtil.ToolTipButonaveMbiGride(grid_FleteDoganore, ci, rm);
            if (Request.QueryString["indexrow"] != null)
                grid_FleteDoganore.FocusedRowIndex = int.Parse(Request.QueryString["indexrow"]);
            percaktoTemplateMenu(idgjuha, ASPxMenu1, idviti, idPerdoruesi, idNdermarrje);
        }

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        /// <param name="idViti"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNermarrje"></param>
        private void percaktoTemplateMenu(int idGjuha, ASPxMenu aSPxMenu1, int idViti, int idPerdorues, int idNdermarrje)
        {
            clsToolbarConfig.percaktoTemplateMenu(idGjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1, DbCore.clsFunksione.GetKomponente(Page.Request), this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
        }

        protected void RuajKolona_Click(object sender, EventArgs e)
        {
            int idfiltri = 0;
            int idndermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idperdorues = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            int idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            int idgjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            int idkonf = DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(cmbKonfigurimi.Text.Split(';')[0], idndermarrje);
            DbCore.clsMesazh mesazh = GridUtil.ruajFiltra(idndermarrje, idperdorues, idgjuha, "grid_FleteDoganore", "FleteDoganore.aspx", "FilterDefault", grid_FleteDoganore.FilterExpression, grid_FleteDoganore, "NrDok", idkonf, out idfiltri);//ruan filtrin e zgjedhur tek filtrat
            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                return;

            }
            mesazh = GridUtil.ruajkonfigurimgride(grid_FleteDoganore, cmbKonfigurimi.Text, idndermarrje, idperdorues, 523, idfiltri, idViti, DbCore.mySessionObjects.ktheCultureInfo(Session), DbCore.mySessionObjects.ktheGjuhe(Session));/// ruan konfigurimin e grides dhe filtrin

            clsToolbarConfig.mbushComboBoxFiltra(idgjuha, idndermarrje, "grid_FleteDoganore", int.Parse(cmbKonfigurimi.Value.ToString()), "FleteDoganore.aspx");

            percaktoTemplateMenu(idgjuha, ASPxMenu1, idViti, idperdorues, idndermarrje);
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
            percaktoTemplateMenu(idgjuha, ASPxMenu1, idviti, idPerdoruesi, idNdermarrje);
        }
        private void mbushGridFleteDoganoreNgaSession(string komponente, string periudheDok, int idNdermarrjeVit, int idNdermarrje, int idPerdoruesi, string datanga, string dataderi)
        {
            DataTable tmpObject;
            bool sukses = DbCore.mySessionObjects.merrGrideNgaSessioni(komponente + periudheDok + idNdermarrjeVit, Session, out tmpObject);
            if (!sukses)
                mbushGridFleteDoganoreNgaDB(komponente, periudheDok, idNdermarrjeVit, idNdermarrje, idPerdoruesi, datanga, dataderi);
            else
            {
                grid_FleteDoganore.DataSource = tmpObject;
                grid_FleteDoganore.DataBind();
                tmpObject.Dispose();
            }
        }
        private void mbushGridFleteDoganoreNgaDB(string komponente, string periudheDok, int idNdermarrjeVit, int idNdermarrje, int idPerdoruesi, string datanga, string dataderi)
        {//mbush griden e popupit me te dhena            
            DataTable dt = DbCore.DbRegjistrim.colFleteDoganoreKoka.merrFleteDoganoreDT(idNdermarrjeVit, idPerdoruesi, datanga, dataderi);
            DbCore.mySessionObjects.ruajGrideNeSession(komponente + periudheDok + idNdermarrjeVit, Session, dt);
            grid_FleteDoganore.DataSource = dt;
            grid_FleteDoganore.DataBind();
            dt.Dispose();
        }

        private void konfiguroGride(int idNdermarrje, int idPerdoruesi, int idGjuha,ResourceManager rm,CultureInfo ci)
        {
            KonfigurimComboGride.ShtoNivel(grid_FleteDoganore,8,idNdermarrje,idPerdoruesi,idGjuha,Session,komponente,guidString);
            KonfigurimComboGride.ShtoModel(grid_FleteDoganore, 8, idNdermarrje, idPerdoruesi, idGjuha, Session, komponente, guidString);
            KonfigurimComboGride.ShtoStatus(grid_FleteDoganore, rm,ci, "IdStatusDok");
            KonfigurimComboGride.ShtoImportExport(grid_FleteDoganore, rm, ci);


            GridViewDataTextColumn col5 = grid_FleteDoganore.Columns["VlFaturuar"] as GridViewDataTextColumn;
            col5.PropertiesEdit.DisplayFormatString = "0.00";
            GridViewDataTextColumn col7 = grid_FleteDoganore.Columns["VlMb"] as GridViewDataTextColumn;
            col7.PropertiesEdit.DisplayFormatString = "0.00";
            GridViewDataTextColumn col8 = grid_FleteDoganore.Columns["VlTransport"] as GridViewDataTextColumn;
            col8.PropertiesEdit.DisplayFormatString = "0.00";
            GridViewDataTextColumn col9 = grid_FleteDoganore.Columns["VlSiguracion"] as GridViewDataTextColumn;
            col9.PropertiesEdit.DisplayFormatString = "0.00";
            GridViewDataTextColumn col10 = grid_FleteDoganore.Columns["VlTjera"] as GridViewDataTextColumn;
            col10.PropertiesEdit.DisplayFormatString = "0.00";
            GridViewDataTextColumn col11 = grid_FleteDoganore.Columns["VlDoganim"] as GridViewDataTextColumn;
            col11.PropertiesEdit.DisplayFormatString = "0.00";
            this.grid_FleteDoganore.Columns["#"].VisibleIndex = 0;
        }
 


        protected void grid_FleteDoganore_DataBound(object sender, EventArgs e)
        {
            if (this.grid_FleteDoganore.Columns["#"] == null)
            {
                DevExpress.Web.GridViewCommandColumn check = new DevExpress.Web.GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true; check.Width = Unit.Percentage(2);
                //  check.SetColVisibleIndex(0);
                grid_FleteDoganore.Settings.ShowFilterRow = true;
                grid_FleteDoganore.Settings.ShowHeaderFilterButton = true;
                grid_FleteDoganore.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                grid_FleteDoganore.Settings.ShowFilterRowMenu = true;
                grid_FleteDoganore.Columns.Add(check);
                grid_FleteDoganore.Settings.ShowGroupPanel = true;
                grid_FleteDoganore.KeyFieldName = "IdFleteDoganoreKoka";
                grid_FleteDoganore.SettingsBehavior.AllowSelectByRowClick = true;
                grid_FleteDoganore.SettingsBehavior.AllowFocusedRow = true;
            }  //  this.grid_FleteDoganore.Columns["#"].VisibleIndex = 0;
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
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "grid_FleteDoganore", "FleteDoganore.aspx", idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            //DbCore.DbAdmin.clsFiltraGrida filtra = dbAdmin.merrFiltraGridaSipasFiltraEmri(cmbFiltra.Text,  DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();
                //mbushComboBoxFiltra();
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "grid_FleteDoganore", int.Parse(cmbKonfigurimi.Value.ToString()), "FleteDoganore.aspx");
                percaktoTemplateMenu(idgjuha, ASPxMenu1, idviti, idPerdoruesi, idNdermarrje);
                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                //  konfiguroVleraFillestare();
                if (Request.QueryString["lloji"] == "import")
                    grid_FleteDoganore.FilterExpression = "[ImportExport]=1 and [IdStatusDok]=1";
                else grid_FleteDoganore.FilterExpression = "[ImportExport]=2 and [IdStatusDok]=1";
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

            DbCore.DbAdmin.clsFiltraGrida filtri = new DbCore.DbAdmin.clsFiltraGrida();
            filtri.FiltraKodi = cmbFiltra.Text;
            filtri.FiltraShenime = cmbFiltra.Text;
            filtri.FiltraUniversal = false;// Universal_ASPxCheckBox.Checked;
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "grid_FleteDoganore", "FleteDoganore.aspx", idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = grid_FleteDoganore.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("NrDok", grid_FleteDoganore);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = grid_FleteDoganore.GetSortedColumns();
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
            //    filtri.KoloneRenditje = "NrDok";
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
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "grid_FleteDoganore", int.Parse(cmbKonfigurimi.Value.ToString()), "FleteDoganore.aspx");
            percaktoTemplateMenu(idgjuha, ASPxMenu1, idviti, idPerdoruesi, idNdermarrje);
            if (mesazh.Status == true)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

            cmbFiltra.Text = "";
        }


        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {
            //if (e.Item.Name == "Shto")
            //{
            //    Response.Redirect("Shto_FleteDoganore.aspx?shtim_modifikim=shtim");
            //}
            //else if (e.Item.Name == "Modifiko")
            //{
            //    int indeksi = grid_FleteDoganore.FocusedRowIndex;
            //    string id;

            //    if (grid_FleteDoganore.GetRowValues(indeksi, "IdFleteDoganoreKoka") != null)
            //        id = grid_FleteDoganore.GetRowValues(indeksi, "IdFleteDoganoreKoka").ToString();
            //    else id = null;
            //    Response.Redirect("Shto_FleteDoganore.aspx?id=" + id + "&indexrow=" + grid_FleteDoganore.FocusedRowIndex + "&shtim_modifikim=modifikim");
            //}
        }

        //fshin rreshtat e selektuar
        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            //clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "", pnlMesazhi);
            List<object> rreshtat = grid_FleteDoganore.GetSelectedFieldValues("IdFleteDoganoreKoka");
            List<string> TeFshire = new List<string>(), TeLidhur = new List<string>(), PeriudheKycur = new List<string>(), closedPeriod = new List<string>();
            DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            foreach (object id in rreshtat)
            {
                DbCore.DbRegjistrim.clsFleteDoganoreKoka koka = new DbCore.DbRegjistrim.clsFleteDoganoreKoka(Convert.ToInt32(id));
                //DbCore.DbRegjistrim.clsFleteDoganoreKoka koka = dbRegjistrim.merrFleteDoganoreKokaSipasId(id);
                //foreach (DbCore.DbRegjistrim.clsFleteDoganoreKoka koka in colKoka)
                //{

                //}
                bool lidhur = dbAdmin.eshteDokumentiILidhur(koka.IdFleteDoganoreKoka, koka.IdNivel, "T_FLETEDOGANOREKOKA", "IDFLETEDOGANORE");
                if (lidhur)
                {
                    TeLidhur.Add(koka.NrDok);
                    continue;
                }
                if (koka.IdStatusDok == 2)
                    continue;

                DbCore.DbAdmin.clsPeriudhaKontabel periudha = new DbCore.DbAdmin.clsPeriudhaKontabel();
                periudha.KodiViti = koka.DtDok.Date.Year.ToString();
                DbCore.DbAdmin.clsViti vit = new DbCore.DbAdmin.clsViti();
                vit.mbushVitetMet(periudha.KodiViti, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                periudha.IdViti = vit.IdViti;
                DbCore.DbAdmin.colPeriudhaKontabel periudhat = new DbCore.DbAdmin.colPeriudhaKontabel();
                periudhat.merrSipasViti(periudha.IdViti);

                bool perketPeriudheKycur = false;
                for (int i = 0; i < periudhat.Count; i++)
                {
                    if (koka.DtDok >= periudhat[i].FillimiPeriudha && koka.DtDok <= periudhat[i].MbarimiPeriudha.AddDays(1))
                    {
                        if (periudhat[i].Ekycur)
                        {
                            PeriudheKycur.Add(koka.NrDok);
                            perketPeriudheKycur = true;
                            break;
                        }
                    }
                }
                if (perketPeriudheKycur)
                    continue;

                if (DbCore.MbylljePeriudhe.PeriodClosing.IsPeriodClosed(koka.DtDok, MyConnectionsManager.GetSelectedConNameServer(), IdNdermarrja, KategoriDokumenti.FleteDoganore, koka.IdKonfigAmbjente))
                {
                    closedPeriod.Add(koka.NrDok);
                    continue;
                }

                koka.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                DbCore.clsMesazh mesazh = koka.fshi();
                if (mesazh.Status)
                {
                    #region Heq llogarite nga grida

                    // ASPxGridView_Artikull.DataSource = dt;
                    hiqFleteDoganoreNgaGrida(koka.IdFleteDoganoreKoka);
                    #endregion
                    TeFshire.Add(koka.NrDok);

                }
            }
            dbAdmin.Dispose();
            string mesazhInfoGabimLidhur = "", mesazhInfoGabimPeridheKycur = "", mesazhInfoSukses = "", mesazhClosedPeriod = "";
            if (rreshtat.Count == 0)
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ju lutem zgjidhni të paktën një dokument!", pnlMesazhi);
            else
            {
                if (TeLidhur.Count == 1)
                    mesazhInfoGabimLidhur = String.Format("{0}{1}{2}", rm.GetString("magFDNumer", ci), String.Join(";", TeLidhur), rm.GetString("suffixMesazhNjejesLidhurGabimi", ci));
                else
                    if (TeLidhur.Count > 1)
                    mesazhInfoGabimLidhur = String.Format("{0}{1}{2}", rm.GetString("magFDNumer", ci), String.Join(";", TeLidhur), rm.GetString("suffixMesazhShumesLidhurGabimi", ci));
                if (PeriudheKycur.Count == 1)
                    mesazhInfoGabimPeridheKycur = String.Format("{0}{1}{2}", rm.GetString("magFDNumer", ci), String.Join(";", PeriudheKycur), rm.GetString("suffixMesazhNjejesPeriudheKycurGabimi", ci));
                else
                    if (PeriudheKycur.Count > 1)
                    mesazhInfoGabimPeridheKycur = String.Format("{0}{1}{2}", rm.GetString("magFDNumer", ci), String.Join(";", PeriudheKycur), rm.GetString("suffixMesazhShumesPeriudheKycurGabimi", ci));

                if (closedPeriod.Count == 1)
                    mesazhClosedPeriod = String.Format("{0}{1}{2}", rm.GetString("regjisDokPrefixMesazhNjejes", ci), String.Join(", ", closedPeriod), rm.GetString("suffixMesazhClosedPeriodNjejes", ci));
                else if (closedPeriod.Count > 1)
                    mesazhClosedPeriod = String.Format("{0}{1}{2}", rm.GetString("regjisDokPrefixMesazhNjejes", ci), String.Join(", ", closedPeriod), rm.GetString("suffixMesazhClosedPeriodShumes", ci));

                if (TeFshire.Count == 1)
                    mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("magFDNumer", ci), String.Join(";", TeFshire), rm.GetString("suffixMesazhNjejesSuksesi", ci));
                else
                    if (TeFshire.Count > 1)
                    mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("magFDNumer", ci), String.Join(";", TeFshire), rm.GetString("suffixMesazhShumesSuksesi", ci));


                mesazhInfoGabimLidhur += mesazhInfoGabimPeridheKycur;
                mesazhInfoGabimLidhur += mesazhClosedPeriod;
                if (mesazhInfoGabimLidhur != "" && mesazhInfoSukses != "")
                    mesazhInfoGabimLidhur += rm.GetString("lidhesMesazhi", ci) + mesazhInfoSukses;
                if (mesazhInfoGabimLidhur != "")
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhInfoGabimLidhur, pnlMesazhi);
                else
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazhInfoSukses, pnlMesazhi);
            }
        }
        private void hiqFleteDoganoreNgaGrida(int idkokafletedog)
        {
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            if (this.grid_FleteDoganore.DataSource != null)
            {
                DataTable dt = (DataTable)grid_FleteDoganore.DataSource;
                DataRow[] drs = dt.Select("IdFleteDoganoreKoka = " + idkokafletedog);
                if (drs.Length > 1)
                    throw new Exception(rm.GetString("msgGabimFDIdNjejte", ci));
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                dt.Rows.Remove(dr);
                grid_FleteDoganore.DataSource = dt;
                grid_FleteDoganore.DataBind();
                dt.Dispose();
            }
            else mbushGridFleteDoganoreNgaDB(DbCore.clsFunksione.GetKomponente(Page.Request), hfState.Get("periudhaDok").ToString(), DbCore.mySessionObjects.ktheNdermarrjeVit(Session), idNdermarrje, idPerdoruesi, hfState.Get("DataDokNga").ToString(), hfState.Get("DataDokDeri").ToString());
        }

        protected void grid_FleteDoganore_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (e.CallbackName == "COLUMNMOVE" && grid_FleteDoganore.AllColumns[int.Parse(e.Args[0])].Width.Value == 0)
                grid_FleteDoganore.AllColumns[int.Parse(e.Args[0])].Width = Unit.Percentage(3);
            if (e.CallbackName == "APPLYFILTER" && e.Args[0] == "")
            {
                DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
                ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
                cmbFiltra.Text = "";
            }
            GridUtil.ToolTipButonaveMbiGride(grid_FleteDoganore, ci, rm);
        }

        protected void grid_FleteDoganore_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string[] arr = e.Parameters.Split(';');
            int idNdermarrje = (int)hfState["idNdermarrje"];
            int idGjuha = (int)hfState["idGjuha"];
            if (arr.Length == 1) //rasti kur behet callback nga filtri i periudhes duhet te ngelen filtrat ne gride dhe te mos aplikohet filtri default
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "ASPxGridView_Numrat", grid_FleteDoganore, cmbKonfigurimi.Text.Split(';')[0], "523", idGjuha, false);
            else
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "ASPxGridView_Numrat", grid_FleteDoganore, cmbKonfigurimi.Text.Split(';')[0], "523", idGjuha);
            if (arr.Length == 2)
            {
                int idPerdoruesi = (int)hfState["idPerdoruesi"];
                string periudheDok = DbCore.DbShare.clsAlternativaKushti.getAlternativa(Convert.ToInt32(cmbKonfigurimi.Value), "SHDPER");
                string datanga, dataderi;
                clsFunksione.ruajPeriudhatNeHiddenField(IsPostBack, hfState, periudheDok, null, out datanga, out dataderi);
                mbushGridFleteDoganoreNgaSession(DbCore.clsFunksione.GetKomponente(Page.Request), hfState.Get("periudhaDok").ToString(), (int)hfState["idNdermarrjeVit"], idNdermarrje, idPerdoruesi, datanga, dataderi);
            }
            if (arr.Length == 3)
            {
                if (arr[2] == "")
                    if (Request.QueryString["lloji"] == "import")
                        grid_FleteDoganore.FilterExpression = "[ImportExport]=1 and [IdStatusDok]=1";
                    else grid_FleteDoganore.FilterExpression = "[ImportExport]=2 and [IdStatusDok]=1";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    //filtra.mbushFiltraGridaSipasFiltraKodi(arr[2],  DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "grid_FleteDoganore", "FleteDoganore.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), int.Parse(cmbKonfigurimi.Value.ToString()));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
                    //DbCore.DbAdmin.clsFiltraGrida filtra = dbAdmin.merrFiltraGridaSipasFiltraKodi(arr[2],  DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    if (filtra.FiltraKodi != null)
                    {
                        grid_FleteDoganore.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, grid_FleteDoganore);
                    }
                }
            }


            grid_FleteDoganore.Selection.UnselectAll();
        }

        protected void grid_FleteDoganore_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = grid_FleteDoganore.PageIndex;
            e.Properties["cpPageRow"] = grid_FleteDoganore.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = grid_FleteDoganore.VisibleRowCount;
        }

        protected void grid_FleteDoganore_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if (e.Column.FieldName == "IdKonfigAmbjente")
                if (Converter.ConvertToInt(e.Value)==0)
                {
                    e.Criteria = null;
                }
        }

        protected void grid_FleteDoganore_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            string TeGjithe = rm.GetString("GridHeaderFilterFillItemTeGjithe", ci);
            string nga = rm.GetString("GridHeaderFilterFillItemNga", ci);
            if (e.Column.FieldName == "NrDok" || e.Column.FieldName == "Shenime")
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
        /// Vendos emrat e label ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo cultinf, ResourceManager rm)
        {
            konfigurimi_Label.Text = rm.GetString("lblLloji", cultinf);
        }
        /// <summary>
        /// Metode qe sherben per te kaluar perkthime te js
        /// </summary>
        /// <param name="cultinf">Merr culture info</param>
        /// <param name="rm">Merr Resource Manager</param>
        private void mbushHiddenFieldMePerkthime(CultureInfo cultinf, ResourceManager rm)
        {


        }
        protected void radDtDok_PreRender(object sender, EventArgs e)
        {
            ASPxRadioButtonList radDtDok = sender as ASPxRadioButtonList;
            radDtDok.SelectedItem = radDtDok.Items.FindByValue(hfState.Get("Periudha").ToString());
        }

    }
}

