using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{
    public partial class KodeProfesione : MyPageBase
    {
         private const string kaveprime = "Ka veprime me kete kod profesioni";
        private const string mesazhfshirjegabimi = "Fshirja perfundoi me gabime!";
        private const string gabimEkzistence = "Ekziston nje kod profesioni me kete kod. Ju lutem shenoni nje tjeter!";
        private int idndermarje, idperdoruesi, idnderviti, idviti;
        /// <summary>
        /// kur faqja lodohet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);

            //if (CacheLayer.GlobalCacheManager.MySessionCache["LoggedIn"].Equals("No"))
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                Response.Redirect(DbCore.IMBUtils.Paths.defaultLoginPath);
                return;
            }
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            idndermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            idperdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            idnderviti = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
            idviti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            //if (CacheLayer.GlobalCacheManager.MySessionCache["KodiNdermarrjes"] == null)
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idperdoruesi);
                return;
            }

            percaktoTemplateMenu(ASPxMenu1, idviti, idperdoruesi, idndermarje);

            //mbushComboBoxFiltra();
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idndermarje, "gvKodeProfesione", 1, "KodeProfesione.aspx");
            if (!IsPostBack)
            {
                EmrateTabeve();
                //Session.Add("mesazh", ":Green");
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, ":");
                if (Request.QueryString["ruaj"] == "ok")
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["mesazhRuajtjeMeSukses"], pnlMesazhi);
              
                hfState.Set("idPerdoruesi", idperdoruesi);
                hfState.Set("idGjuha", DbCore.mySessionObjects.ktheGjuhe(Session));
                hfState.Set("idNdermarrje", idndermarje);
                 konfiguroVleraFillestare();
                konfiguroGridat();
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idperdoruesi, idndermarje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "KodeProfesione.aspx");
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idndermarje, "gvKodeProfesione", 1, "KodeProfesione.aspx");
            }

            DbCore.mySessionObjects.ruajMesazhNeSesion(Session, ":");

            konfiguroVleraFillestare();

        }

        /// <summary>
        /// Vendos emrat e tabeve ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateTabeve()
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            //    ASPxPageControl1.TabPages[0].Text = rm.GetString("labelRaportMujore", cultinf);
            //   ASPxPageControl1.TabPages[1].Text = rm.GetString("labelRaportDitore", cultinf);
            //ASPxPageControl1.TabPages[2].Text = rm.GetString("tabOrare", cultinf);
        }

        /// <summary>
        /// mbush gridat me te dhena
        /// </summary>
        private void konfiguroVleraFillestare()
        {
            DbCore.DbListPagesat.colKodeProfesione col1 = new DbCore.DbListPagesat.colKodeProfesione(idndermarje);
            gvKodeProfesione.DataSource = col1;
            gvKodeProfesione.DataBind();

         

        }
        /// <summary>
        /// konfiguron gridat
        /// </summary>
        private void konfiguroGridat()
        {
            konfiguroGride(gvKodeProfesione);

        }
        /// <summary>
        /// konfiguron griden
        /// </summary>
        /// <param name="grida">grida</param>
        private void konfiguroGride(ASPxGridView grida)
        {
            // shtoPage(grida);
            GridUtil.percaktoVisibleColumnsMeWidth(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), gvKodeProfesione, "gvKodeProfesione", "KodeProfesione.aspx");
        
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            GridUtil.konfiguroGrideListeEvogelPaTheme(grida, "Id");
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
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, "KodeProfesione.aspx", this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, hfRuaj.Value == "Ruaj" ? true : false, true, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
        }

        /// <summary>
        /// ben databound menune
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(ASPxMenu1, idviti, idperdoruesi, idndermarje);
        }
        /// <summary>
        /// fshin rreshtin e fokusuar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonOk_Click2(object sender, EventArgs e)
        { //per t'u rregulluar sipas kodifikimit te kf
           
            ASPxGridView grida = gvKodeProfesione;
            int a = grida.FocusedRowIndex;
            grida.Selection.SelectRow(a);
            List<object> rreshtat = grida.GetSelectedFieldValues("Id");

            foreach (int id in rreshtat)
            {
                DbCore.DbListPagesat.clsKodeProfesione kat = new DbCore.DbListPagesat.clsKodeProfesione(id);

                if (DbCore.DbListPagesat.clsKodeProfesione.kaVeprimeKodeProfesione(kat.Id))
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, kaveprime, pnlMesazhi);
                else
                {
                    kat.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                    DbCore.clsMesazh mesazh = kat.fshi();
                    if (mesazh.Status) clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["msgFshirjeMeSukses"], pnlMesazhi);
                    else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhfshirjegabimi, pnlMesazhi);

                }
                konfiguroVleraFillestare();

            }
            pnlGrida.Update();
        }
        /// <summary>
        /// ruan filtrin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje
             ASPxGridView grida = gvKodeProfesione;

            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvKodeProfesione", "KodeProfesione.aspx", idNdermarrje);
            DbCore.DbAdmin.clsFiltraGrida filtri = new DbCore.DbAdmin.clsFiltraGrida() { FiltraKodi = cmbFiltra.Text, FiltraShenime = cmbFiltra.Text, FiltraUniversal = false, GridaKokaId = koka.IdGridaKoka, FiltraVlera = grida.FilterExpression, IdPerdoruesi = idperdoruesi, IdNdermarje = idNdermarrje, IdStatusDok = 1 };
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("Kodi", grida);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = grida.GetSortedColumns();
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
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            mesazh = filtri.ruaj();
            //mbushComboBoxFiltra();
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvKodeProfesione", 1, "KodeProfesione.aspx");
            percaktoTemplateMenu(ASPxMenu1, idviti, idperdoruesi, idndermarje);
            if (mesazh.Status == true)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            cmbFiltra.Text = "";
        }
        /// <summary>
        /// fshin filtrin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje
            ASPxGridView grida = gvKodeProfesione;

            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
            //filtra.mbushFiltraGridaSipasFiltraEmri(cmbFiltra.Text, idndermarje);
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvKodeProfesione", "KodeProfesione.aspx", idNdermarrje);
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                mesazh = filtra.fshi();
                //mbushComboBoxFiltra();
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvKodeProfesione", 1, "KodeProfesione.aspx");
                percaktoTemplateMenu(ASPxMenu1, idviti, idperdoruesi, idndermarje);
                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                grida.FilterExpression = String.Empty;
            }
        }
        /// <summary>
        /// ben insert te rreshtit te ri
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            //merr te dhenat e rreshtit te ri te grides
            ASPxGridView gride = sender as ASPxGridView;
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idperdoruesi, idndermarje, idviti, "KodeProfesione.aspx");
            if (!tedrejtaInfo.DShtim)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ju nuk keni te drejta per kete veprim!:Red");
                return;
            }
            if (e.NewValues["Aktiv"] == null)
                e.NewValues["Aktiv"] = false;
           
            DbCore.DbListPagesat.clsKodeProfesione kategori = new DbCore.DbListPagesat.clsKodeProfesione() { Kodi = e.NewValues["Kodi"].ToString(), Pershkrimi = e.NewValues["Pershkrimi"].ToString(), IdNdermarje = idndermarje, IdPerdoruesi = idperdoruesi, IdStatusDok = 1, IdKrijuesi = idperdoruesi,  Aktiv = bool.Parse(e.NewValues["Aktiv"].ToString()) };
          

            e.Cancel = true;
            gride.CancelEdit();
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            if (!DbCore.DbListPagesat.clsKodeProfesione.ekzistonKodeProfesione(kategori.Kodi, idndermarje))
            {
                mesazh = kategori.ruaj();
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
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ekziston nje kod profesione me kete kod! Ju lutem zgjidhni nje kod tjeter:Red");
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ekziston nje kod profesione me kete kod! Ju lutem zgjidhni nje kod tjeter", pnlMesazhi);
                gride.AddNewRow();
            }
            gride.AddNewRow();
        }
        /// <summary>
        /// ben update te rreshtit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            ASPxGridView gride = sender as ASPxGridView;
            String id = e.Keys["Id"].ToString();
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idperdoruesi, idndermarje, idviti, "KodeProfesione.aspx");
            if (!tedrejtaInfo.DMod)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ju nuk keni te drejta per kete veprim!:Red");
                return;
            }
            DbCore.clsMesazh m = new DbCore.clsMesazh();
            DbCore.DbListPagesat.clsKodeProfesione kategori = new DbCore.DbListPagesat.clsKodeProfesione() { Kodi = e.NewValues["Kodi"].ToString(), Pershkrimi = e.NewValues["Pershkrimi"].ToString(),  IdNdermarje = idndermarje, IdPerdoruesi = idperdoruesi, IdStatusDok = 1, Id = int.Parse(id), Aktiv = bool.Parse(e.NewValues["Aktiv"].ToString())};
            e.Cancel = true;
            m = kategori.modifiko();
            if (m.Status)
            {
                //CacheLayer.GlobalCacheManager.MySessionCache["mesazh"] = m.PershkrimMesazhi + ":Green";
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, m.PershkrimMesazhi + ":Green");
            }
            else
            {
                //CacheLayer.GlobalCacheManager.MySessionCache["mesazh"] = m.PershkrimMesazhi + ":Red";
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, m.PershkrimMesazhi + ":Red");
            }
            gride.CancelEdit();
            konfiguroVleraFillestare();

        }

        /// <summary>
        /// validon te dhenat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {//per tu rregulluar me vone
            //validimi ne jane plotesuar gjithe fushat e detyruara

            ASPxGridView grida = sender as ASPxGridView;
            foreach (GridViewColumn column in grida.Columns)
            {
                if (column.Visible == true)
                {
                    GridViewDataColumn dataColumn = column as GridViewDataColumn;
                    if (dataColumn == null) continue;
                    if (dataColumn.FieldName == "Aktiv" && e.NewValues[dataColumn.FieldName] == null)
                        e.NewValues[dataColumn.FieldName] = false;
                    if (e.NewValues[dataColumn.FieldName] == null)//validimi per kolonat e detyrueshme
                    {
                        e.Errors[dataColumn] = "Vlera nuk mund te jete null.";
                    }
                    if (e.NewValues["Kodi"] == null)
                    {
                        e.Errors[dataColumn] = "Kodi nuk mund te jete bosh.";
                    }
                }
            }
            if (e.NewValues["Kodi"] != null)
            {
                try
                {
                    
                    if (hfRuaj.Value == "Ruaj" && DbCore.DbListPagesat.clsKodeProfesione.ekzistonKodeProfesione(e.NewValues["Kodi"].ToString(), idndermarje))
                    {
                       
                                e.RowError = gabimEkzistence;
                             
                    }
                }
                catch (Exception ex)
                {
                    NLog.LogManager.GetCurrentClassLogger().Error(ex.Message);
                    e.RowError = ex.Message;
                }


            }
            else e.RowError = "Kodi nuk mund te jete bosh.";

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
        /// kur reshti fillon te editohet therret metoden per validim
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void startRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {//kur fillon editimi te behet validimi
            ASPxGridView grida = sender as ASPxGridView;
            if (!grida.IsNewRowEditing)
            {
                grida.DoRowValidation();
            }
        }
        /// <summary>
        /// kur grida ben callback
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridat_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {

        }
        /// <summary>
        /// kur inicializohet rreshti i ri
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void initNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {

        }
        /// <summary>
        /// kur grida ben callback nga perdoruesi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridat_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            ASPxGridView grida= gvKodeProfesione;
            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                if (arr[2] == "")
                    grida.FilterExpression = "";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    //filtra.mbushFiltraGridaSipasFiltraKodi(arr[2], idndermarje);
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvKodeProfesione", "KodeProfesione.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        grida.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, grida);

                        konfiguroVleraFillestare();

                    }
                }
            }
            konfiguroGride(grida);
        }
        /// <summary>
        /// vendos property te grides per tu aksesuar nga javascripti
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridat_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            ASPxGridView grida = sender as ASPxGridView;
            e.Properties["cpPageIndex"] = grida.PageIndex;
            e.Properties["cpPageRow"] = grida.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = grida.VisibleRowCount;
        }
        /// <summary>
        /// filtrimet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridat_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            e.Values.Clear();
            e.AddValue("(Te gjithe)", string.Empty, "true");
        }

        protected void gridat_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {

        }
        protected void btnXlsxExport_Click(object sender, EventArgs e)
        {
            try
            {
                gridExport.WriteXlsxToResponse("Kode Profesioni", true);
            }
            catch (Exception)
            {
            }
        }
        protected void btnPdfExport_Click(object sender, EventArgs e)
        {
            try
            {
                gridExport.WritePdfToResponse("Kode Profesioni", true);
            }
            catch (Exception)
            {
            }
        }
    }
}
   