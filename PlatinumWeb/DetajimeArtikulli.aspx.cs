using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Collections;
using PlatinumWeb.Templates;
using System.Globalization;
using DbCore.IMBUtils.Validation;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using System.Data;
using System.Resources;
using DbCore;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{
    public partial class DetajimeArtikulli : MyPageBase
    {
        ArrayList vlerat = new ArrayList();
        private DbCore.DbInventari.clsDetajimArtikulli DetajimOverview;
        private static string STR_DateSkadenceJoVlefshme = "Kodi i datë skadencës duhet të jetë në format date dd/MM/yyyy!";
        private static string STR_DetajimiLidhur = "Detajimi nuk mund të fshihet, sepse është i lidhur!";
        private const string mesazhZgjidhniNje = "Ju lutem zgjidhni një detajim!";
        private const string STR_EmertimiNumerikGabim = "Jepni një vlerë numerike për emërtimin!";
        private const string emerKomponente = "DetajimeArtikulli.aspx";
        private const string emerGride = "gvDetajimArtikulli";
        protected void Page_Load(object sender, EventArgs e)
        {
            int idPerdoruesi;
            int idGjuha;
            int idNdermarrje;
            int idViti;
            string veprimi;
            int idNdermarrjeVit;
            bool lupe = false;

            if (hfState.Count == 0)
            {
                if (!DbCore.mySessionObjects.isLogedIn(Session))
                {
                    DbCore.clsFunksione.logout(Session, true, "FaqePaautorizuar");
                }

                if (Request.QueryString["shitje_blerje"] == "shitje")
                    veprimi = "shitje";
                else
                    veprimi = "blerje";
                idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
                {
                    Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerdoruesi);
                }
                idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
                idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
                idNdermarrjeVit = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
                hfState.Set("idPerdoruesi", idPerdoruesi);
                hfState.Set("idGjuha", idGjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("idViti", idViti);
                hfState.Set("veprimi", veprimi);
                hfState.Set("idNdermarrjeVit", idNdermarrjeVit);
                if (!String.IsNullOrEmpty(Request.QueryString["lupe"]) && Request.QueryString["lupe"] == "true")
                    lupe = true;
                else
                    lupe = false;
                hfState.Set("lupe", lupe);
            }
            else
            {
                idPerdoruesi = (int)hfState["idPerdoruesi"];
                idGjuha = (int)hfState["idGjuha"];
                idNdermarrje = (int)hfState["idNdermarrje"];
                idViti = (int)hfState["idViti"];
                veprimi = (string)hfState["veprimi"];
                idNdermarrjeVit = (int)hfState["idNdermarrjeVit"];
                lupe = (bool)hfState["lupe"];
            }

            percaktoTemplateMenu(idGjuha, ASPxMenu1, idViti, idPerdoruesi, idNdermarrje, lupe);
            clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idNdermarrje, emerGride, 1, emerKomponente);
            if (!IsPostBack)
            {
                hfState.Add("kategoria", 0); //ruhet kategoria e detajimit qe po modifikohet
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, ":");
                if (Request.QueryString["ruaj"] == "ok")
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["mesazhRuajtjeMeSukses"], pnlMesazhi);

                mbushGrideNgaDb(idPerdoruesi, idNdermarrje);
                konfiguroGride(idGjuha, idNdermarrje);
                GridUtil.percaktoVisibleColumnsMeWidth(idGjuha, idNdermarrje, gvDetajimArtikulli, emerGride, emerKomponente);
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, emerKomponente);
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
            }
            else
            {

                mbushGrideNgaSesioni(idPerdoruesi, idNdermarrje);
                konfiguroGride(idGjuha, idNdermarrje);
            }
            DbCore.mySessionObjects.ruajMesazhNeSesion(Session, ":");
            gvDetajimArtikulli.PercaktoTitlePanel(this, _menu, pnlMesazhi, hfState, IdPerdoruesi, IdNdermarrja, IdViti, IdGjuha, 1, "DetajimeArtikulli.aspx", rm, ci);



        }

        protected List<int> pozicionoNeFillimDetajimetEZgjedhura(int idNdermarrje, DataTable dt)
        {
            List<int> idte = new List<int>();
            if (!(bool)hfState["lupe"])
                return idte;
            if (String.IsNullOrEmpty(Request.QueryString["detajime"]))
                return idte;
            string detajimet = Request.QueryString["detajime"];
            string[] det = detajimet.Split(',');

            for (int i = det.Length - 1; i >= 0; i--)
            {
                DbCore.DbInventari.clsDetajimArtikulli detaj = new DbCore.DbInventari.clsDetajimArtikulli();
                detaj.mbushDetajimArtikulli(det[i], idNdermarrje);
                DataRow[] dr = dt.Select("IdDetajimArtikulli = " + detaj.IdDetajimArtikulli);
                DataRow newRow = dt.NewRow();
                newRow.ItemArray = dr[0].ItemArray;
                dt.Rows.Remove(dr[0]);
                dt.Rows.InsertAt(newRow, 0);
                idte.Add(detaj.IdDetajimArtikulli);
            }
            return idte;
        }

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="idGjuha"></param>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        /// <param name="idViti"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNdermarrje"></param>
        private void percaktoTemplateMenu(int idGjuha, ASPxMenu aSPxMenu1, int idViti, int idPerdorues, int idNdermarrje, bool lupe)
        {
            clsToolbarConfig.percaktoTemplateMenu(idGjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1, emerKomponente, this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, hfRuaj.Value == "Ruaj" ? true : false, true, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
            if (lupe)
                aSPxMenu1.Items.FindByName("OK").Visible = true;
            else
                aSPxMenu1.Items.FindByName("OK").Visible = false;
        }

        /// <summary>
        /// sherben per te ruajtur filtrin e zgjedhur
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
            int idNdermarrje = (int)hfState["idNdermarrje"];
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            DbCore.DbAdmin.clsFiltraGrida filtri = new DbCore.DbAdmin.clsFiltraGrida();
            filtri.FiltraKodi = cmbFiltra.Text;
            filtri.FiltraShenime = cmbFiltra.Text;
            filtri.FiltraUniversal = false;// Universal_ASPxCheckBox.Checked;
            int idGjuha = (int)hfState["idGjuha"];
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuha, emerGride, emerKomponente, idNdermarrje);
            filtri.GridaKokaId = koka.IdGridaKoka;


            filtri.FiltraVlera = gvDetajimArtikulli.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("IdDetajimArtikulli", gvDetajimArtikulli);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvDetajimArtikulli.GetSortedColumns();
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
            //    filtri.KoloneRenditje = "IdDetajimArtikulli";
            //    filtri.DrejtimRenditje = true;
            //}


            //DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();
            //oPerdorues = (DbCore.DbAdmin.clsPerdorues)(CacheLayer.GlobalCacheManager.MySessionCache["oClsPerdoruesi"]);
            int idPerdoruesi = (int)hfState["idPerdoruesi"];
            filtri.IdPerdoruesi = idPerdoruesi;
            filtri.IdNdermarje = idNdermarrje;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();

            filtri.IdStatusDok = 1;
            mesazh = filtri.ruaj();
            clsToolbarConfig.mbushComboBoxFiltraMeValFieldTextField(idGjuha, idNdermarrje, emerGride, emerKomponente, "IdFiltra", "FiltraShenime", 1);
            //gvDetajimArtikulli.SortBy('IdDetajimArtikulli', 'DSC');
            percaktoTemplateMenu(idGjuha, ASPxMenu1, (int)hfState["idViti"], idPerdoruesi, idNdermarrje, (bool)hfState["lupe"]);
            if (mesazh.Status == true)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            cmbFiltra.Text = "";
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
            int idNdermarrje = (int)hfState["idNdermarrje"];
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;

            //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
            //filtra.mbushFiltraGridaSipasFiltraEmri(cmbFiltra.Text, idNderm);
            int idGjuha = (int)hfState["idGjuha"];
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuha, emerGride, emerKomponente, idNdermarrje);
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);

            //DbCore.DbAdmin.clsFiltraGrida filtra = dbAdmin.merrFiltraGridaSipasFiltraEmri(cmbFiltra.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            if (filtra.FiltraKodi != null)
            {
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                int idPerdoruesi = (int)hfState["idPerdoruesi"];
                filtra.IdPerdoruesi = idPerdoruesi;
                mesazh = filtra.fshi();
                //mbushComboBoxFiltra(idNderm);
                clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idNdermarrje, emerGride, 1, emerKomponente);
                percaktoTemplateMenu(idGjuha, ASPxMenu1, (int)hfState["idViti"], idPerdoruesi, idNdermarrje, (bool)hfState["lupe"]);
                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                //   konfiguroVleraFillestare();
                gvDetajimArtikulli.FilterExpression = String.Empty;
            }
        }

        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu((int)hfState["idGjuha"], ASPxMenu1, (int)hfState["idViti"], (int)hfState["idPerdoruesi"], (int)hfState["idNdermarrje"], (bool)hfState["lupe"]);
        }

        private void mbushGrideNgaDb(int idPerdoruesi, int idNdermarrje)
        {
            DataTable dt = new DataTable();
            gvDetajimArtikulli.DataSource = null;
            int kategoria = -1;
            bool hapurNgaLupa = (bool)hfState["lupe"];
            if (!hapurNgaLupa)
            {
                dt = DbCore.DbInventari.colDetajimeArtikulli.merrDetajimeSipasNdermarrjesAndAutorizimDt(idNdermarrje, idPerdoruesi);
            }
            else if (!String.IsNullOrEmpty(Request.QueryString["veprimi"]))
            {
                kategoria = int.Parse(Request.QueryString["veprimi"]);
                dt = DbCore.DbInventari.colDetajimeArtikulli.merrDetajimeSipasNdermarrjesAndAutorizimeSipasKategoriseDt(idNdermarrje, idPerdoruesi, kategoria);
            }
            List<int> idte = pozicionoNeFillimDetajimetEZgjedhura(idNdermarrje, dt);
            DbCore.mySessionObjects.ruajGrideNeSession(emerKomponente, Session, dt);
            gvDetajimArtikulli.DataSource = dt;
            gvDetajimArtikulli.DataBind();
            selektoDetajimetEZgjedhura(idte);
        }

        private void selektoDetajimetEZgjedhura(List<int> idte)
        {
            if (!(bool)hfState["lupe"])
                return;

            foreach (int idja in idte)
            {
                if (idja == 0)
                    continue;
                gvDetajimArtikulli.Selection.SelectRowByKey(idja);
            }
        }

        private void mbushGrideNgaSesioni(int idPerdoruesi, int idNdermarrje)
        {
            DataTable tmpObject;
            bool sukses = DbCore.mySessionObjects.merrGrideNgaSessioni(emerKomponente, Session, out tmpObject);
            if (!sukses)
                mbushGrideNgaDb(idPerdoruesi, idNdermarrje);
            else
            {
                gvDetajimArtikulli.DataSource = tmpObject;
                gvDetajimArtikulli.DataBind();
                tmpObject.Dispose();
            }
        }

        private void konfiguroGride(int idGjuha, int idNdermarrje)
        {

            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            KonfigurimComboGride.shto_KategoriDetajimi(gvDetajimArtikulli, rm, ci);
            KonfigurimComboGride.shto_LlojDetajimArtikulli(gvDetajimArtikulli, rm, ci);
            GridUtil.konfigGrideListeEMadhePaTheme(gvDetajimArtikulli, "IdDetajimArtikulli");
            gvDetajimArtikulli.Columns["#"].VisibleIndex = 0;
        }


        private void shto_Autorizim(int idPerdoruesi)
        {//shtohen komboja me Autorizimeve tek grida e KPF

            gvDetajimArtikulli.Columns.Remove(gvDetajimArtikulli.Columns["IdNivelAutorizimi"]);

            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();

            DbCore.DbAdmin.colAutorizimetKoka colAutorizim = new DbCore.DbAdmin.colAutorizimetKoka(idPerdoruesi);
            //colAutorizim = dbAdmin.merrAutorizimKokaPerPerdorues (DbCore.mySessionObjects.ktheIdPerdoruesi(Session) );
            colnew.PropertiesComboBox.DataSource = colAutorizim;
            colnew.PropertiesComboBox.TextField = "KodiAutorizim";
            colnew.PropertiesComboBox.ValueField = "IdAutorizimKoka";
            colnew.FieldName = "IdNivelAutorizimi";
            gvDetajimArtikulli.Columns.Add(colnew);
        }



        protected void gvDetajimArtikulli_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {//funksioni qe theret javascriptin per kalimin e te dhenave nga grida tek textboxet e tabeve te tjera         
            if ((e.Column.FieldName == "LlojDetajimArtikulli") || (e.Column.FieldName == "PershkrimDetajimArtikulli"))
            {
                e.Editor.DisabledStyle.Border.BorderColor = System.Drawing.Color.LightGray;
                e.Editor.DisabledStyle.ForeColor = System.Drawing.Color.LightGray;
            }
            if (e.Editor.GetType().Name == "ASPxTextBox")
            {
                ASPxTextBox currentEditor = e.Editor as ASPxTextBox;
                //currentEditor.ClientSideEvents.TextChanged = "function(s,e){ProcessTextChanged('" + e.Column.FieldName + "',s.GetText());}";
                currentEditor.ClientInstanceName = e.Column.FieldName;
                if (e.Column == gvDetajimArtikulli.Columns["KodDetajimArtikulli"])
                {
                    if (hfRuaj.Value == "Modifiko")
                        e.Column.ReadOnly = true;
                }
            }
            else if (e.Editor.GetType().Name == "ASPxComboBox")
            {
                ASPxComboBox currentEditor = e.Editor as ASPxComboBox;
                if (e.Column.FieldName == "KategoriDetajimi")
                    currentEditor.ClientSideEvents.TextChanged = "function(s,e){TextChangedKategoria(gvDetajimArtikulli.GetFocusedRowIndex());}";
                currentEditor.ClientInstanceName = e.Column.FieldName;
            }
        }

        protected void gvDetajimArtikulli_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            if (e.Column.FieldName == "KodDetajimArtikulli" || e.Column.FieldName == "PershkrimDetajimArtikulli")
            {
                e.Values.Clear();
                e.AddValue("(Te gjithe)", string.Empty, "true");
            }
        }

        protected void gvDetajimArtikulli_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {//validimi ne jane plotesuar gjithe fushat e detyruara            

            int idNdermarrje = (int)hfState["idNdermarrje"];

            foreach (GridViewColumn column in gvDetajimArtikulli.Columns)
            {
                if (column.Visible == true)
                {
                    GridViewDataColumn dataColumn = column as GridViewDataColumn;
                    if (dataColumn == null) continue;
                    if (dataColumn.FieldName == "IdNivelAutorizimi")
                        continue;
                    if (e.NewValues["KategoriDetajimi"] != null && (e.NewValues["KategoriDetajimi"].ToString() == "3" || e.NewValues["KategoriDetajimi"].ToString() == "4") && dataColumn.FieldName == "PershkrimDetajimArtikulli")
                        continue;
                    //   if (String.IsNullOrEmpty(hfAutorizime.Value))
                    //       e.Errors[dataColumn] = "Vlera nuk mund te jete null.";

                    if (e.NewValues[dataColumn.FieldName] == null && dataColumn.FieldName != "IdNivelAutorizimi")//validimi per kolonat e detyrueshme
                    {
                        e.Errors[dataColumn] = "Vlera nuk mund te jete null.";
                        return;
                    }
                    if (dataColumn.FieldName == "LlojDetajimArtikulli" || dataColumn.FieldName == "KategoriDetajimi")
                    {
                        if (e.NewValues[dataColumn.FieldName] != null && int.Parse(e.NewValues[dataColumn.FieldName].ToString()) == 0)
                        {
                            e.Errors[dataColumn] = "Vlera nuk mund te jete null.";
                            return;
                        }
                    }
                }
            }
            if (e.Keys.Count == 0)//shtim
            {
                if (e.NewValues["KodDetajimArtikulli"] != null)
                {
                    System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
                    System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                    DbCore.clsMesazh msgKaraktereTePalejuara = DbCore.clsFunksione.kontrolloKaraktereMeMesazh(e.NewValues["KodDetajimArtikulli"].ToString(), FusheKontrolli.Kodi, false);
                    if (!msgKaraktereTePalejuara.Status)
                    {
                        e.RowError = msgKaraktereTePalejuara.PershkrimMesazhi;
                        return;
                    }
                    DbCore.DbInventari.clsDatabaseInventari dbInventari = new DbCore.DbInventari.clsDatabaseInventari();
                    if (dbInventari.ekzistonDetajim(e.NewValues["KodDetajimArtikulli"].ToString(), idNdermarrje))
                    {
                        e.RowError = "Ekziston nje detajim me kete kod. Ju lutemi zgjidhni nje kod tjeter!";
                        dbInventari.Dispose();
                        return;
                    }
                }
            }

            if (e.NewValues["LlojDetajimArtikulli"].ToString() == "3")
            {

                if (e.NewValues["KategoriDetajimi"].ToString() == "3")
                {
                    String kodi = e.NewValues["KodDetajimArtikulli"].ToString();
                    DateTime date;
                    bool dateVlefshme = DateTime.TryParseExact(kodi, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date);
                    if (!dateVlefshme)
                    {
                        e.RowError = STR_DateSkadenceJoVlefshme;
                        return;
                    }
                }
            }

            if (e.NewValues["LlojDetajimArtikulli"].ToString() == "2" && e.NewValues["KategoriDetajimi"].ToString() == "2")
            {
                string emertimi = e.NewValues["PershkrimDetajimArtikulli"].ToString();
                long numer = 0;
                bool vlefshem = Int64.TryParse(emertimi, out numer);
                if (!vlefshem)
                {
                    e.RowError = STR_EmertimiNumerikGabim;
                    return;
                }
            }
            if (e.Errors.Count > 0)
            {
                e.RowError = "Ju lutemi, plotesoni te gjitha fushat.";
                //percaktoTamplate();
                return;
            }
            if (string.IsNullOrEmpty(e.RowError) && e.Errors.Count > 0)
            {
                e.RowError = "Ju lutemi, korrigjoni te gjithe gabimet.";
                //percaktoTamplate();
                return;
            }
        }

        protected void gvDetajimArtikulli_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {//kur fillon editimi te behet validimi
            if (hfRuaj.Value == "Ruaj")
                if (!gvDetajimArtikulli.IsNewRowEditing)
                {
                    gvDetajimArtikulli.DoRowValidation();
                }
        }

        protected void gvDetajimArtikulli_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            String id = e.Keys["IdDetajimArtikulli"].ToString();
            string kodi;
            string niveli;
            DbCore.DbInventari.clsDetajimArtikulli detArt = new DbCore.DbInventari.clsDetajimArtikulli(int.Parse(id));
            kodi = e.NewValues["KodDetajimArtikulli"].ToString();
            if (String.IsNullOrEmpty(hfAutorizime.Value))
            {
                detArt.mbushAutorizime();
                niveli = detArt.IdNivelAutorizimi;
            }
            else
                niveli = hfAutorizime.Value;

            e.Cancel = true;
            DbCore.DbInventari.clsDetajimArtikulli detajim = new DbCore.DbInventari.clsDetajimArtikulli();
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            detajim.IdDetajimArtikulli = int.Parse(id);
            detajim.KodDetajimArtikulli = kodi;
            detajim.LlojDetajimArtikulli = int.Parse(e.NewValues["LlojDetajimArtikulli"].ToString());
            detajim.PershkrimDetajimArtikulli = e.NewValues["PershkrimDetajimArtikulli"].ToString();
            int idPerdoruesi = (int)hfState["idPerdoruesi"];
            detajim.IdPerdoruesi = idPerdoruesi;
            detajim.KategoriDetajimi = int.Parse(e.NewValues["KategoriDetajimi"].ToString());
            int idNdermarrje = (int)hfState["idNdermarrje"];
            detajim.IdNdermarje = idNdermarrje;
            detajim.IdNivelAutorizimi = niveli;
            detajim.IdStatusDok = 1;
            DbCore.DbInventari.clsDetajimArtikulli detekzistuese = new DbCore.DbInventari.clsDetajimArtikulli(detajim.IdDetajimArtikulli);
            detajim.Loan = detekzistuese.Loan;
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, (int)hfState["idViti"], emerKomponente);
            if (!tedrejtaInfo.DMod)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ju nuk keni te drejta per kete veprim!:Red");
                return;
            }
            mesazh = detajim.modifiko();
            if (mesazh.Status)
            {
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, MessagesResource.Messages["msgModifikimiMeSuksesGreen"]);
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["mesazhRuajtjeMeSukses"], pnlMesazhi);
            }
            else
            {
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Modifikimi perfundoi me gabime!:Red");
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ndodhi nje gabim. Ruajtja nuk u krye!", pnlMesazhi);
            }
            gvDetajimArtikulli.CancelEdit();
            mbushGrideNgaDb(idPerdoruesi, idNdermarrje);

            //konfiguroGride((int)hfState["idGjuha"], idNdermarrje, true);
            hfAutorizime.Value = "";
        }
        private void hiqDetajimeNgaGrida(int idPerdoruesi, int idNdermarrje, int IdDetajimArtikulli, System.Resources.ResourceManager rm, System.Globalization.CultureInfo ci)
        {
            if (gvDetajimArtikulli.DataSource != null)
            {
                DataTable dt = (DataTable)gvDetajimArtikulli.DataSource;
                DataRow[] drs = dt.Select("IdDetajimArtikulli = " + IdDetajimArtikulli);
                if (drs.Length > 1)
                    throw new DbCore.MyException(rm.GetString("msgShtoArtikulGabimNdodhen2ArtikujNeGride", ci));
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                dt.Rows.Remove(dr);
                gvDetajimArtikulli.DataSource = dt;
                gvDetajimArtikulli.DataBind();
            }
            else
            {

                mbushGrideNgaDb(idPerdoruesi, idNdermarrje);
            }
        }
        private void FshiDetajime(List<string> TeFshire, List<string> TePaFshire)
        {
            int a = gvDetajimArtikulli.FocusedRowIndex;
            gvDetajimArtikulli.Selection.SelectRow(a);
            List<object> rreshtat = gvDetajimArtikulli.GetSelectedFieldValues("IdDetajimArtikulli");
            if (rreshtat.Count == 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhZgjidhniNje, pnlMesazhi);
                return;
            }
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            int idNdermarrje = (int)hfState["idNdermarrje"];
            int idPerdoruesi = (int)hfState["idPerdoruesi"];

            DbCore.DbInventari.clsDetajimArtikulli detArt = new DbCore.DbInventari.clsDetajimArtikulli();
            var mesazhInfoGabim = string.Empty;
            var mesazhInfoSukses = string.Empty;
            foreach (object id in rreshtat)
            {
                detArt.mbushDetajimArtikulliSipasId(Convert.ToInt32(id));
                detArt.IdPerdoruesi = idPerdoruesi;
                if (DbCore.DbInventari.clsDetajimArtikulli.eshteDetajimLidhurMeArtikull(detArt.KodDetajimArtikulli, idNdermarrje))
                {
                    TePaFshire.Add(detArt.KodDetajimArtikulli);
                    continue;
                }
                else
                {
                    mesazh = detArt.fshi();
                    if (!mesazh.Status)
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                    else
                        TeFshire.Add(detArt.KodDetajimArtikulli);
                }
                mbushGrideNgaDb(idPerdoruesi, idNdermarrje);
            }
        }
        private void KrijoMesazh(List<string> TeFshire, List<string> TePaFshire, String mesazhInfoGabim, String mesazhInfoSukses)
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (TePaFshire.Count == 1)
            {
                mesazhInfoGabim = String.Format("{0}{1}{2}", rm.GetString("msgdetajimiMeKod", cultinf), String.Join(";", TePaFshire), rm.GetString("msgDetajimiNukFshihetNjejes", cultinf));
            }
            else
            {
                if (TePaFshire.Count > 1)
                {
                    mesazhInfoGabim = String.Format("{0}{1}{2}", rm.GetString("msgDetajimetMeKod", cultinf), String.Join(";", TePaFshire), rm.GetString("msgDetajimetNukFshihenShumes", cultinf));
                }
            }
            if (TeFshire.Count == 1)
            {
                mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("msgdetajimiMeKod", cultinf), String.Join(";", TeFshire), rm.GetString("msgGrupimiFshirjeMeSukses", cultinf));
            }
            else
            {
                if (TeFshire.Count > 1)
                {
                    mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("msgDetajimetMeKod", cultinf), String.Join(";", TeFshire), rm.GetString("msgGrupimiFshirjeMeSuksesShumes", cultinf));
                }
            }
            clsMesazh mesazhG = new clsMesazh(false, mesazhInfoGabim);
            clsMesazh mesazhiS = new clsMesazh(true, mesazhInfoSukses);

            var msg = new List<clsMesazh>();
            msg.Add(mesazhG);
            msg.Add(mesazhiS);
            gvDetajimArtikulli.ShtoMesazhNeGride(msg);


        }
        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {
            if (e.Item.Name == "Modifiko")
            {
                int indeksi = gvDetajimArtikulli.FocusedRowIndex;
                gvDetajimArtikulli.StartEdit(indeksi);
                //mbushGrideNgaSesioni((int)hfState["idPerdoruesi"], (int)hfState["idNdermarrje"]);
                hfAutorizime.Value = "";
            }
        }

        protected void gvDetajimArtikulli_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if (e.Column.FieldName == "LlojDetajimArtikulli" || e.Column.FieldName == "KategoriDetajimi")
            {
                if (String.IsNullOrEmpty(e.Value))
                {
                    e.Criteria = null;
                    return;
                }

                int vlera;
                bool parse = Int32.TryParse(e.Value, out vlera);
                if (vlera == 0)
                {
                    e.Criteria = null;
                    return;
                }

            }
        }

        protected void gvDetajimArtikulli_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {//merr te dhenat e rreshtit te ri te grides     
            String kodi = DbCore.clsFunksione.ktheStringunPaHapesira(e.NewValues["KodDetajimArtikulli"].ToString(), true);
            int kategoria = int.Parse(e.NewValues["KategoriDetajimi"].ToString());
            int lloji = int.Parse(e.NewValues["LlojDetajimArtikulli"].ToString());
            String pershkrimi;
            if (kategoria == 3 || kategoria == 4)
                pershkrimi = " ";
            else pershkrimi = DbCore.clsFunksione.ktheStringunPaHapesira(e.NewValues["PershkrimDetajimArtikulli"].ToString(), false);

            int idNdermarrje = (int)hfState["idNdermarrje"];
            int idPerdoruesi = (int)hfState["idPerdoruesi"];
            DetajimOverview = new DbCore.DbInventari.clsDetajimArtikulli(kodi, lloji, pershkrimi, idPerdoruesi, kategoria, idNdermarrje, hfAutorizime.Value, 1, 0);
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            if (kodi != "" && pershkrimi != "")
            {
                e.Cancel = true;
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, (int)hfState["idViti"], emerKomponente);
                if (!tedrejtaInfo.DShtim)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ju nuk keni te drejta per kete veprim!:Red");
                    return;
                }
                mesazh = DetajimOverview.ruaj();
                if (!mesazh.Status)
                {
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ndodhi nje gabim. Ruajtja nuk u krye!:Red");
                    return;
                }

                gvDetajimArtikulli.CancelEdit();
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, MessagesResource.Messages["msgRuajtjeMeSuksesGreen"]);
                shtoDetajimNePozicioninEPare(idNdermarrje, idPerdoruesi, DetajimOverview.IdDetajimArtikulli);
            }
        }

        private void shtoDetajimNePozicioninEPare(int idNdermarrje, int idPerdorues, int idDetajimi)
        {
            if (gvDetajimArtikulli.DataSource != null)
            {
                DataTable dt = (DataTable)gvDetajimArtikulli.DataSource;
                DataRow[] drs = dt.Select("IdDetajimArtikulli = " + idDetajimi);
                if (drs.Length > 0)
                    throw new DbCore.MyException($"Detajimi me id {idDetajimi} ekziston nje here ne gride!");
                DataRow newArtDr;
                if ((bool)hfState["lupe"])
                {
                    newArtDr = dt.NewRow();
                    newArtDr.ItemArray = DbCore.DbInventari.clsDetajimArtikulli.merrDetajimArtikulliSipasIdDr(idDetajimi).ItemArray;
                    dt.Rows.InsertAt(newArtDr, 0);
                    gvDetajimArtikulli.Selection.SelectRowByKey(idDetajimi);
                }
                else
                {
                    newArtDr = DbCore.DbInventari.clsDetajimArtikulli.merrDetajimArtikulliSipasIdDr(idDetajimi);
                    dt.ImportRow(newArtDr);
                }
            }
            else mbushGrideNgaDb(idPerdorues, idNdermarrje);
        }


        protected void gvDetajimArtikulli_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            int idNdermarrje = (int)hfState["idNdermarrje"];
            string[] arr = e.Parameters.Split(';');
            int idPerdoruesi = (int)hfState["idPerdoruesi"];
            int idGjuha = (int)hfState["idGjuha"];
            var mesazhInfoGabim = string.Empty;
            var mesazhInfoSukses = string.Empty;
            var TeFshire = new List<string>();
            var TePaFshire = new List<string>();
            if (e.Parameters.Contains("Fshi"))
                FshiDetajime(TeFshire, TePaFshire);
            KrijoMesazh(TeFshire, TePaFshire, mesazhInfoGabim, mesazhInfoSukses);
            if (arr.Length == 3)
            {
                if (arr[2] == "")
                    gvDetajimArtikulli.FilterExpression = "";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuha, emerGride, emerKomponente, idNdermarrje);
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], idNdermarrje, koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        gvDetajimArtikulli.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvDetajimArtikulli);
                    }
                }
            }
            mbushGrideNgaSesioni(idPerdoruesi, idNdermarrje);
            konfiguroGride(idGjuha, idNdermarrje);
        }

        protected void gvDetajimArtikulli_DataBound(object sender, EventArgs e)
        {
            if (gvDetajimArtikulli.Columns["#"] == null)
            {

                DevExpress.Web.GridViewCommandColumn check = new DevExpress.Web.GridViewCommandColumn("#");

                check.ShowSelectCheckbox = true;
                check.Width = Unit.Percentage(2);
                check.Index = 0;
                //check.VisibleIndex = 0;



                gvDetajimArtikulli.Settings.ShowFilterRow = true;
                gvDetajimArtikulli.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                gvDetajimArtikulli.Settings.ShowFilterRowMenu = true;
                //check.SetColVisibleIndex(0);

                gvDetajimArtikulli.Columns.Add(check);
                gvDetajimArtikulli.KeyFieldName = "IdDetajimArtikulli";
                gvDetajimArtikulli.SettingsBehavior.AllowSelectByRowClick = true;
                gvDetajimArtikulli.SettingsBehavior.AllowFocusedRow = true;

            }

        }
    }
}