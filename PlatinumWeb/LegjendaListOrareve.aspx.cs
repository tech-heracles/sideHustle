using DevExpress.Web;
using PlatinumWeb.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{
    public partial class LegjendaListOrareve : MyPageBase
    {

        /// <summary>
        /// metoda preinit per vendosjen e themes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <summary>
        /// metoda page load per mbushjen e te dhenave
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                Response.Redirect(DbCore.IMBUtils.Paths.defaultLoginPath);
                return;
            }
            int idPerd = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerd);
                return;
            }
            DbCore.DbAdmin.clsPerdorues oPerdorues = DbCore.mySessionObjects.kthePerdorues(Session);
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLegjenda", 1, "LegjendaListOrareve.aspx");
            if (!IsPostBack)
            {
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, ":");
                if (Request.QueryString["ruaj"] == "ok")
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["mesazhRuajtjeMeSukses"], pnlMesazhi);
                konfiguroVleraFillestare();
                konfiguroGride();
                GridUtil.percaktoVisibleColumnsMeWidth(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), gvLegjenda, "gvLegjenda", "LegjendaListOrareve.aspx");

                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "LegjendaListOrareve.aspx");
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
            }
            DbCore.mySessionObjects.ruajMesazhNeSesion(Session, ":");
            konfiguroVleraFillestare();
            konfiguroGride();
        }

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        /// <param name="idViti"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNdermarrje"></param>
        private void percaktoTemplateMenu(ASPxMenu aSPxMenu1, int idViti, int idPerdorues, int idNdermarrje)
        {
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, "LegjendaListOrareve.aspx", this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, hfRuaj.Value == "Ruaj" ? true : false, true, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
        }

        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        }
        /// <summary>
        /// mbush griden me te dhena
        /// </summary>
        private void konfiguroVleraFillestare()
        {
            DbCore.DbListPagesat.colLegjendaListOrareve col = new  DbCore.DbListPagesat.colLegjendaListOrareve ();
            col.ktheGjitheLegjendaListOrariSipasNdermarrjes(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            gvLegjenda.DataSource = col;
            gvLegjenda.DataBind();
        }
        private void percaktoTemplate()
        {
            GridViewDataTextColumn colk = gvLegjenda.Columns["Koeficienti"] as GridViewDataTextColumn;
            colk.PropertiesTextEdit.ValidationSettings.EnableCustomValidation = true;
            colk.PropertiesTextEdit.ClientSideEvents.Validation = "function (s,e){validate(s,e);}";///perdoret per te validuar nje fushe sipas menyres tone dhe jo te devexpresit
            GridViewDataColumn col1 = gvLegjenda.Columns["OreFillimi"] as GridViewDataColumn;
            col1.PropertiesEdit.DisplayFormatString = "HH:mm";///formaton daten ne kete format

            col1.EditItemTemplate = new MySpinTemplate();
            GridViewDataColumn col2 = gvLegjenda.Columns["OreMbarimi"] as GridViewDataColumn;
            col2.PropertiesEdit.DisplayFormatString = "HH:mm";///formaton daten ne kete format
            col2.EditItemTemplate = new MySpinTemplate();
        }
        /// <summary>
        /// konfiguron griden
        /// </summary>
        private void konfiguroGride()
        {
            percaktoTemplate();
       
            shto_Komponente(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            GridUtil.konfiguroGrideListeEvogelPaTheme(gvLegjenda, "Id");
        }

        private void shto_Komponente(int idNdermarrje)
        {
            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            if (typeof(GridViewDataComboBoxColumn) != gvLegjenda.Columns["IdKomponente"].GetType())
            {
                gvLegjenda.Columns.Remove(gvLegjenda.Columns["IdKomponente"]);
                gvLegjenda.Columns.Add(colnew);
               DbCore.DbListPagesat.colKomponentePage col = new DbCore.DbListPagesat.colKomponentePage();
                col.ktheGjitheKomponentePageSipasNdermarjesDheLlojitDheDatesMeTeAfert(idNdermarrje, true,DateTime.Today);
               
                col.Insert(0,new DbCore.DbListPagesat.clsKomponentePage  ());
                colnew.PropertiesComboBox.DataSource = col;
                colnew.PropertiesComboBox.TextField = "Kodi";
                colnew.PropertiesComboBox.ValueField = "IdKomponentePage";
                colnew.FieldName = "IdKomponente";
                colnew.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                DbCore.mySessionObjects.ruajDsComboGrideNeSession(Session, col, "njesite1");
            }
            else
            {
                colnew = (GridViewDataComboBoxColumn)gvLegjenda.Columns["IdKomponente"];

                if (colnew.PropertiesComboBox.Items.Count == 0)
                {
                    colnew.PropertiesComboBox.DataSource = DbCore.mySessionObjects.merrDsComboGrideNeSession(Session, "njesite1");
                    //ASPxGridView_Artikull.Columns.Remove(ASPxGridView_Artikull.Columns["Njesi1Artikulli"]);
                    //ASPxGridView_Artikull.Columns.Add();
                }
            }
        }
        /// <summary>
        /// vendos fushen te gjitha tek filtrat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvLegjenda_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            e.Values.Clear();
            e.AddValue("(Te gjithe)", string.Empty, "true");
        }

        protected void gvLegjenda_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
        }
        /// <summary>
        /// metoda per shtimin e rreshtit ne gride
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvLegjenda_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {

            DbCore.DbListPagesat.clsLegjendaListOrareve njesi = new  DbCore.DbListPagesat.clsLegjendaListOrareve ();
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "LegjendaListOrareve.aspx");
            if (!tedrejtaInfo.DShtim)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ju nuk keni te drejta per kete veprim!:Red");
                return;
            }
            njesi.Kodi = e.NewValues["Kodi"].ToString();
            njesi.OreFillimi = hfOreFillimi.Value.ToString();
            njesi.OreMbarimi = hfOreMbarimi.Value.ToString();
            njesi.Pershkrimi = e.NewValues["Pershkrimi"].ToString();
            njesi.Koeficienti = decimal.Parse(e.NewValues["Koeficienti"].ToString());
            if (e.NewValues["IdKomponente"] == null)
                njesi.IdKomponente = 0;
            else
            njesi.IdKomponente = int.Parse(e.NewValues["IdKomponente"].ToString());
            //njesi.IdNderViti = new DbCore.clsFunksione().ktheNdermarrjeVit();
            njesi.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            njesi.IdKrijuesi = njesi.IdPerdoruesi;
            njesi.IdNdermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            njesi.IdStatusDok = 1;
            e.Cancel = true;
            gvLegjenda.CancelEdit();
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            if (!DbCore.DbListPagesat.clsLegjendaListOrareve.ekzistonLegjenda(njesi.Kodi, njesi.IdNdermarje))
            {
                mesazh = njesi.ruaj();
                if (!mesazh.Status == true)
                {
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ndodhi nje gabim. Ruajtja nuk u krye!:Red");
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ndodhi nje gabim. Ruajtja nuk u krye!", pnlMesazhi);
                }
                else
                {
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, MessagesResource.Messages["msgRuajtjeMeSuksesGreen"]);
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["mesazhRuajtjeMeSukses"], pnlMesazhi);
                }
                konfiguroVleraFillestare();
            }
            else
            {
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ekziston nje legjende me kete kod! Ju lutem zgjidhni nje kod tjeter:Red");
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ekziston nje legjende me kete kod! Ju lutem zgjidhni nje kod tjeter", pnlMesazhi);
                konfiguroGride();
                gvLegjenda.AddNewRow();
            }
        }
        /// <summary>
        /// metoda per validimin e rreshtit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvLegjenda_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {//validimi ne jane plotesuar gjithe fushat e detyruara
            foreach (GridViewColumn column in gvLegjenda.Columns)
            {
                if (column.Visible == true)
                {
                    GridViewDataColumn dataColumn = column as GridViewDataColumn;
                    if (dataColumn == null) continue;

                    if (e.NewValues[dataColumn.FieldName] == null && dataColumn.FieldName!="IdKomponente"&& dataColumn.FieldName!="OreFillimi"&& dataColumn.FieldName!="OreMbarimi")//validimi per kolonat e detyrueshme
                    {

                        e.Errors[dataColumn] = "Vlera nuk mund te jete null.";
                    }
                }
            }
            if (hfRuaj.Value == "Ruaj")
            {
                if (e.NewValues["Kodi"] != null)
                {
                    DbCore.DbListPagesat.clsLegjendaListOrareve njesi = new  DbCore.DbListPagesat.clsLegjendaListOrareve ();

                    njesi.Kodi = e.NewValues["Kodi"].ToString();

                    njesi.IdNdermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);

                    if (DbCore.DbListPagesat.clsLegjendaListOrareve.ekzistonLegjenda(njesi.Kodi, njesi.IdNdermarje))
                    {

                        e.RowError = "Ekziston nje legjende me kete kod! Ju lutem zgjidhni nje kod tjeter";
                    }
                }
                else e.RowError = "Ju lutemi, plotesoni te gjitha fushat";
            }
            if (e.Errors.Count > 0)
            {
                e.RowError = "Ju lutemi, plotesoni te gjitha fushat.";
            }
            if (string.IsNullOrEmpty(e.RowError) && e.Errors.Count > 0)
            {
                e.RowError = "Ju lutemi, korrigjoni te gjithe gabimet.";
            }
        }
        /// <summary>
        /// metoda per fillimin e editimit te rreshtit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvLegjenda_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {//kur fillon editimi te behet validimi
            if (hfRuaj.Value == "Ruaj")
                if (!gvLegjenda.IsNewRowEditing)
                {
                    gvLegjenda.DoRowValidation();
                }
        }
        /// <summary>
        /// metoda per modifikimin e rreshtit ne gride
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvLegjenda_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            String id = e.Keys["Id"].ToString();
            e.Cancel = true;
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "LegjendaListOrareve.aspx");
            if (!tedrejtaInfo.DMod)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ju nuk keni te drejta per kete veprim!:Red");
                return;
            }
            DbCore.DbListPagesat.clsLegjendaListOrareve njesi = new DbCore.DbListPagesat.clsLegjendaListOrareve ();
            njesi.Id = int.Parse(id);
            njesi.Kodi = e.NewValues["Kodi"].ToString();
            njesi.OreFillimi = hfOreFillimi.Value.ToString();
            njesi.OreMbarimi =hfOreMbarimi.Value.ToString();
            njesi.Pershkrimi = e.NewValues["Pershkrimi"].ToString();
            njesi.Koeficienti = decimal.Parse(e.NewValues["Koeficienti"].ToString());
            if (e.NewValues["IdKomponente"] == null)
                njesi.IdKomponente = 0;
            else
            njesi.IdKomponente = int.Parse(e.NewValues["IdKomponente"].ToString());
            njesi.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            //njesi.IdNderViti = new DbCore.clsFunksione().ktheNdermarrjeVit();
            njesi.IdNdermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            njesi.IdStatusDok = 1;
            DbCore.clsMesazh m = njesi.modifiko();
            if (m.Status)
            {
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, m.PershkrimMesazhi + ":Green");
            }
            else
            {
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, m.PershkrimMesazhi + ":Red");
            }
            gvLegjenda.CancelEdit();
            konfiguroVleraFillestare();      
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
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLegjenda", "LegjendaListOrareve.aspx", idNdermarrje);
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            //DbCore.DbAdmin.clsFiltraGrida filtra = dbAdmin.merrFiltraGridaSipasFiltraEmri(cmbFiltra.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            if (filtra.FiltraKodi != null)
            {
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                mesazh = filtra.fshi();
                //mbushComboBoxFiltra();
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLegjenda", 1, "LegjendaListOrareve.aspx");
                percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);

                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                //ASPxButton btnRuaj = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("Button1") as ASPxButton;
                //ASPxButton btnFshiFilter = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFshi") as ASPxButton;

                //  btnRuaj.ClientEnabled = false;
                //  btnFshiFilter.ClientEnabled = false;
                //  konfiguroVleraFillestare();
                gvLegjenda.FilterExpression = String.Empty;
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
            //DbCore.DbAdmin.clsGridaKoka koka = dbAdmin.merrGridaKokaByEmri("gvLegjenda", "LegjendaListOrareve.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLegjenda", "LegjendaListOrareve.aspx", idNdermarrje);
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gvLegjenda.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("Kodi", gvLegjenda);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvLegjenda.GetSortedColumns();
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
            //mbushComboBoxFiltra();
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLegjenda", 1, "LegjendaListOrareve.aspx");
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);

            if (mesazh.Status == true)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            cmbFiltra.Text = "";

        }
        /// <summary>
        /// metoda per fshirjen e rreshtit pasi e ka konfirmuar perdoruesi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            int a = gvLegjenda.FocusedRowIndex;
            gvLegjenda.Selection.SelectRow(a);
            List<object> rreshtat = gvLegjenda.GetSelectedFieldValues("Id");
            DbCore.DbInventari.clsDatabaseInventari dbInventari = new DbCore.DbInventari.clsDatabaseInventari();
            foreach (int id in rreshtat)
            {
                DbCore.DbListPagesat.clsLegjendaListOrareve col = new DbCore.DbListPagesat.clsLegjendaListOrareve(id);
                //DbCore.DbInventari.colNjesiteArtikulli col = dbInventari.ktheNjesiAritkulli(id);
                col.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                if (DbCore.DbListPagesat.clsLegjendaListOrareve.kaVeprimeSimboli(col.Id))
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ka veprime me kete simbol", pnlMesazhi);
                else
                {
                    col.fshi();
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["msgFshirjeMeSukses"], pnlMesazhi);
                }

                konfiguroVleraFillestare();

            }
            dbInventari.Dispose();
            pnlGrida.Update();
        }

        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {
            //if (e.Item.Name == "Modifiko")
            //{
            //    int indeksi = gvLegjenda.FocusedRowIndex;
            //    gvLegjenda.StartEdit(indeksi);
            //    konfiguroGride();             
            //}

            //else if (e.Item.Name == "Shto")
            //{
            //    gvLegjenda.AddNewRow();
            //    konfiguroGride();               
            //}
        }

        protected void gvLegjenda_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
        }

        protected void gvLegjenda_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            //  konfiguroVleraFillestare();            
        }

        protected void gvLegjenda_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
        }
        /// <summary>
        /// metoda kur bejme callback nga javascripti vendos filtrin e zgjdhur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvLegjenda_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                if (arr[2] == "")
                    gvLegjenda.FilterExpression = "";
                else
                {

                    //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    var idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                    //filtra.mbushFiltraGridaSipasFiltraKodi(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(base.Session), "gvLegjenda", "LegjendaListOrareve.aspx", idNdermarrje);
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], idNdermarrje, koka.IdGridaKoka);
                    //DbCore.DbAdmin.clsFiltraGrida filtra = dbAdmin.merrFiltraGridaSipasFiltraKodi(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    if (filtra.FiltraKodi != null)
                    {
                        gvLegjenda.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvLegjenda);
                    }
                }
            }
            konfiguroGride();
        }
        /// <summary>
        /// metoda per vendosjen e disa fushave per tu aksesuar nga javascripti
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvLegjenda_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gvLegjenda.PageIndex;
            e.Properties["cpPageRow"] = gvLegjenda.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gvLegjenda.VisibleRowCount;
        }

        protected void gvLegjenda_HtmlRowCreated(object sender, ASPxGridViewEditFormEventArgs e)
        {
            ASPxGridView grida = sender as ASPxGridView;

            GridViewDataColumn col1 = grida.Columns["OreFillimi"] as GridViewDataColumn;
            ASPxTimeEdit txt1 = ((ASPxGridView)sender).FindEditRowCellTemplateControl(col1, "txtBox") as ASPxTimeEdit;
            GridViewDataColumn col3 = grida.Columns["OreMbarimi"] as GridViewDataColumn;
            ASPxTimeEdit txt2 = ((ASPxGridView)sender).FindEditRowCellTemplateControl(col3, "txtBox") as ASPxTimeEdit;
            
            if (txt1 != null)
            {
                txt1.ClientInstanceName = "txtOreFillimi";
                txt1.ClientSideEvents.DateChanged = "function(s,e){DateFillimiChanged(s,e)}";
                txt1.ClientSideEvents.Init = "function(s,e){DateFillimiChanged1(s,e)}";
            }
            if (txt2 != null)
            {
                txt2.ClientInstanceName = "txtOreMbarimi";
                txt2.ClientSideEvents.DateChanged = "function(s,e){DateMbarimiChanged(s,e)}";
                txt2.ClientSideEvents.Init = "function(s,e){DateMbarimiChanged1(s,e)}";
            }

        }
    }
}