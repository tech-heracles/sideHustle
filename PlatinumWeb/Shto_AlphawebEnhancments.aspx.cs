using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using DbCore;
using DevExpress.Web;
using DbCore.DbRegjistrim;
using System.Globalization;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;

namespace PlatinumWeb
{
    public partial class Shto_AlphawebEnhancments : System.Web.UI.Page
    {

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Request.QueryString["idTheme"] != null && Request.QueryString["idTheme"] != "")
                DbCore.clsFunksione.percaktoThemeAmbjenteDheJQueryMeId(Page, Convert.ToInt32(Request.QueryString["idTheme"]));
            else
                DbCore.clsFunksione.percaktoThemeAmbjenteDheJQuery(Page, DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            int idPerdoruesi;
            int idGjuha;
            int idNdermarrje;
            int idViti;
            int idNdermarrjeVit;
            if (hfState.Count == 0)
            {
                if (!DbCore.mySessionObjects.isLogedIn(Session))
                {
                    Response.Redirect("login.aspx?arsye=FaqePaautorizuar");
                }
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
                hfState.Set("idNdermarrjeVit", idNdermarrjeVit);
                hfLlojNumri.Value = "1";
                hfState.Set("Meme", DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
            }
            else
            {
                idPerdoruesi = (int)hfState["idPerdoruesi"];
                idGjuha = (int)hfState["idGjuha"];
                idNdermarrje = (int)hfState["idNdermarrje"];
                idViti = (int)hfState["idViti"];
                idNdermarrjeVit = (int)hfState["idNdermarrjeVit"];
            }

            if (!IsCallback)
                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                    FormsAuthentication.RedirectToLoginPage();
            if (!Page.IsPostBack)
            {
                konfiguroVleraFillestare(idNdermarrje);
                ASPxPageControl1.ActiveTabIndex = 0;
                mbushGridAWEnhacmentsNgaDB(idNdermarrje);
                konfiguroGride(idNdermarrje, idPerdoruesi);
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "gv_AlphawebEnhancments", gv_AlphawebEnhancments, cmbKonfigurimi.Text, Convert.ToString(4008), idGjuha);
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, "Konfigurime Gride");

                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, Request.RawUrl.Split('/')[1]);
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
            }
            else
            {
                mbushGridAWEnhacmentsNgaSesioni(idNdermarrje);
                konfiguroGride(idNdermarrje, idPerdoruesi);
            }
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            GridUtil.konfigGrideListeEMadhePaTheme(gv_AlphawebEnhancments, "idKlientMeMSISDN");
            percaktoTemplateMenu(idViti, idPerdoruesi, idNdermarrje, ASPxMenu1);
            clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idNdermarrje, "gv_AlphawebEnhancments", int.Parse(cmbKonfigurimi.Value.ToString()), "Shto_AlphawebEnhancments.aspx");
        }

        private void konfiguroVleraFillestare(int idNdermarrje)
        { //mbush komboboxet dhe gridat e faqes
            int idPerdoruesi = (int)hfState["idPerdoruesi"];

            ConfigureAspxComboBox.mbushComboNjesiAdm(idNdermarrje, idPerdoruesi, cmbDegeAdmin);

            cmbDegeAdmin.SelectedIndex = 0;

            mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 202, "AE");
            cmbKonfigurimi.SelectedIndex = 0;
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()));
            hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;
        }

        public void mbushComboKonfigurimeshSipasKategorise(int idPerdoruesi, int idNdermarrje, ASPxComboBox combo, int kat, string nivel)
        {//mbush griden e popupit me te dhena
            DbCore.DbShare.colKonfigurimAmbjenti col = new DbCore.DbShare.colKonfigurimAmbjenti();
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            int idGjuha = (int)hfState["idGjuha"];
            if (kat != 0)
            {
                konf.IdKategori = kat;
                konf.IdNdermarje = idNdermarrje;
                int idNivel = DbCore.DbRegjistrim.clsNivelRegjistrimi.ktheIdNivelRegjistrimiSipasKodi(nivel, idNdermarrje);
                col.mbushKonfigAmbjSipasIdKategoriIdNivel(konf.IdKategori, idNivel, idPerdoruesi, idGjuha, false);
            }
            else
            {
                col.mbushGjitheKonfigurimeAmbjentesh(idNdermarrje, idPerdoruesi, 1, idGjuha);
            }
            ListBoxColumn colprove = new ListBoxColumn();
            colprove.FieldName = "KodKonfigAmbjente";
            colprove.Caption = "Kodi";
            ListBoxColumn colemer = new ListBoxColumn();
            colemer.FieldName = "PershkrimKonfigAmbjente";
            colemer.Caption = "Pershkrimi";
            combo.TextFormatString = "{0}";
            combo.Columns.Add(colprove);
            combo.Columns.Add(colemer);
            combo.DataSource = col;
            combo.ValueField = "IdKonfigAmbjente";
            combo.IncrementalFilteringMode = DevExpress.Web.IncrementalFilteringMode.Contains;
            combo.DataBind();
        }


        private void mbushGridAWEnhacmentsNgaDB(int idNdermarrje)
        {//mbush griden me te dhena            
            DataTable dt = DbCore.DbRegjistrim.colKlientMeMSISDN.merrPromocioneTePerdoruraSipasNdermarrjes(idNdermarrje);
            mySessionObjects.ruajGrideNeSession(Session, dt);
            gv_AlphawebEnhancments.DataSource = dt;
            gv_AlphawebEnhancments.DataBind();
            dt.Dispose();
        }

        private void mbushGridAWEnhacmentsNgaSesioni(int idNdermarrje)
        {
            DataTable tmpObject;
            bool sukses = mySessionObjects.merrGrideNgaSessioni(Session, out tmpObject);
            if (!sukses)
                mbushGridAWEnhacmentsNgaDB(idNdermarrje);
            else
            {
                gv_AlphawebEnhancments.DataSource = tmpObject;
                gv_AlphawebEnhancments.DataBind();
                tmpObject.Dispose();
            }
        }


        /// <summary>
        /// perdoret per te konfiguruar griden. Per te dhenat mbi kolonat qe do te shfaqen nga databaza si dhe percakton disa karakteristika te grides
        /// </summary>
        /// :  <see cref="GridUtil.percaktoVisibleColumns"/>
        /// :  <see cref="DbCore.clsFunksione.konfiguroGrideListeMadhe(grida,celesigrides)"/>
        /// <param name="idNdermarrje"></param>
        private void konfiguroGride(int idNdermarrje, int idPerdorues)
        {
            //Konfigurimi i grides            
            shtoLlojDokumenti();
            gv_AlphawebEnhancments.Columns["#"].VisibleIndex = 0;
        }

        private void shtoLlojDokumenti()
        {
            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            if (typeof(GridViewDataComboBoxColumn) != gv_AlphawebEnhancments.Columns["lloji"].GetType())
            {
                gv_AlphawebEnhancments.Columns.Remove(gv_AlphawebEnhancments.Columns["lloji"]);
                gv_AlphawebEnhancments.Columns.Add(colnew);
                colnew.PropertiesComboBox.Items.Add("", null);
                colnew.PropertiesComboBox.Items.Add("Golden numbers", 1);
                colnew.PropertiesComboBox.Items.Add("Normal numbers", 2);
                colnew.FieldName = "lloji";
                mySessionObjects.ruajDsComboGrideNeSession(Session, colnew.PropertiesComboBox.Items, "colLjoDokMSISDN");
            }
            else
            {
                colnew = (GridViewDataComboBoxColumn)gv_AlphawebEnhancments.Columns["lloji"];
                if (colnew.PropertiesComboBox.Items.Count == 0)
                {
                    //gv_AlphawebEnhancments.Columns.Remove(gv_AlphawebEnhancments.Columns["lloji"]);
                    colnew.PropertiesComboBox.Items.AddRange((ListEditItemCollection)DbCore.mySessionObjects.merrDsComboGrideNeSession(Session, "colLjoDokMSISDN"));
                    //gv_AlphawebEnhancments.Columns.Add(colnew);
                }
            }
        }

        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            int idPerdoruesi = (int)hfState["idPerdoruesi"];
            int idNdermarrje = (int)hfState["idNdermarrje"];
            int idViti = (int)hfState["idViti"];
            percaktoTemplateMenu(idViti, idPerdoruesi, idNdermarrje, ASPxMenu1);
        }

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="idNderVit"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        private void percaktoTemplateMenu(int idViti, int idPerdorues, int idNdermarrje, ASPxMenu aSPxMenu1)
        {
            int idGjuha = (int)hfState["idGjuha"];
            bool meme = (bool)hfState["Meme"];
            clsToolbarConfig.percaktoTemplateMenu(idGjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1, Request.RawUrl.Split('/')[1].Split('?')[0], this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, true, false, false, meme);
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
            filtri.FiltraUniversal = false;
            int idViti = (int)hfState["idViti"];
            int idNdermarrje = (int)hfState["idNdermarrje"];
            int idGjuhePerdoruesi = (int)hfState["idGjuha"];
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuhePerdoruesi, "gv_AlphawebEnhancments", "Shto_AlphawebEnhancments.aspx", idNdermarrje);
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gv_AlphawebEnhancments.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("idKlientMeMSISDN", gv_AlphawebEnhancments);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gv_AlphawebEnhancments.GetSortedColumns();
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
            //    filtri.KoloneRenditje = "Targa";
            //    filtri.DrejtimRenditje = true;
            //}
            int idPerdorues = (int)hfState["idPerdoruesi"];
            filtri.IdPerdoruesi = idPerdorues;
            filtri.IdNdermarje = idNdermarrje;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            filtri.IdStatusDok = 1;
            mesazh = filtri.ruaj();
            clsToolbarConfig.mbushComboBoxFiltra(idGjuhePerdoruesi, idNdermarrje, "gv_AlphawebEnhancments", int.Parse(cmbKonfigurimi.Value.ToString()), "Shto_AlphawebEnhancments.aspx");
            percaktoTemplateMenu(idViti, idPerdorues, idNdermarrje, ASPxMenu1);
            if (mesazh.Status == true)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            hfStatusi.Value = "true";
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
            int idViti = (int)hfState["idViti"];
            int idNdermarrje = (int)hfState["idNdermarrje"];
            int idGjuhePerdoruesi = (int)hfState["idGjuha"];
            int idPerdorues = (int)hfState["idPerdoruesi"];
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuhePerdoruesi, "gv_AlphawebEnhancments", "Shto_AlphawebEnhancments.aspx", idNdermarrje);
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = idPerdorues;
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();

                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                clsToolbarConfig.mbushComboBoxFiltra(idGjuhePerdoruesi, idNdermarrje, "gv_AlphawebEnhancments", int.Parse(cmbKonfigurimi.Value.ToString()), "Shto_AlphawebEnhancments.aspx");
                percaktoTemplateMenu(idViti, idPerdorues, idNdermarrje, ASPxMenu1);
                konfiguroVleraFillestare(idNdermarrje);
                hfStatusi.Value = "true";
                this.gv_AlphawebEnhancments.FilterExpression = String.Empty;
            }
        }


        /// <summary>
        /// perdoret per te trajtuar ngjarjet e butonave te menuse. Shkon ne faqen e modifikimit te artikujve kur perdoruesi klikon butonin modifiko
        /// ose ne faqen e shtimit te artikujve kur perdoruesi klikon butonin shto
        /// </summary>
        /// <param name="source"> derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {//veprimet e menuse
            if (e.Item.Name == "Ruaj")
            {
                Page.Validate("entries");
                ruajAwEnhancment();
            }
        }

        private void ruajAwEnhancment()
        {
            if (Page.IsValid == false)
                return;
            int idPerdoruesi = (int)hfState["idPerdoruesi"];
            int idGjuha = (int)hfState["idGjuha"];
            int idNdermarrje = (int)hfState["idNdermarrje"];
            int idViti = (int)hfState["idViti"];

            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, Request.RawUrl.Split('/')[1]);

            if (!tedrejtaInfo.DShtim)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ju nuk keni te drejta per te kryer kete veprim!", pnlMesazhi);
                hfStatusi.Value = "false";
                return;
            }
            LlojPromocionMsisdn llojNumri;

            switch (Convert.ToInt32(hfLlojNumri.Value))
            {
                case 1:
                    llojNumri = LlojPromocionMsisdn.Golden;
                    break;
                case 2:
                    llojNumri = LlojPromocionMsisdn.Normal;
                    break;
                default:
                    llojNumri = LlojPromocionMsisdn.Undefined;
                    break;
            }

            int idMag = clsNjesiAdministrative.ktheIdMagazine(cmbDegeAdmin.Text, idNdermarrje);

            clsKlientMeMSISDN enhancment = new clsKlientMeMSISDN(0, txtMsisdn.Text, txtKodiFitues.Text, true, llojNumri, txtMsisdnERe.Text, DateTime.Now, idPerdoruesi, idNdermarrje, idMag);

            clsMesazh mesazh = enhancment.Modifiko();

            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                hfStatusi.Value = "false";
            }
            else
            {
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                //shtoRreshtinERiNeGrid(idNdermarrje, idPerdoruesi, enhancment.IdKlientMeMSISDN);
                hfStatusi.Value = "true";
                mbushGridAWEnhacmentsNgaDB(idNdermarrje);
                konfiguroGride(idNdermarrje, idPerdoruesi);
            }
        }

        private void shtoRreshtinERiNeGrid(int idNdermarrje, int idPerdorues, int id)
        {
            if (gv_AlphawebEnhancments.DataSource != null)
            {
                DataTable dt = (DataTable)gv_AlphawebEnhancments.DataSource;
                DataRow[] drs = dt.Select("idKlientMeMSISDN = " + id);
                if (drs.Length > 0)
                    throw new Exception("GABIM: Klienti ekziston ne gride!");
                //DataRow newDr = DbCore.DbRegjistrim.clsKlientMeMSISDN.ktheBLABLA(id);
                //dt.ImportRow(newDr);
            }
            else
                mbushGridAWEnhacmentsNgaDB(idNdermarrje);
            konfiguroGride(idNdermarrje, idPerdorues);
        }

        protected void btnPdfExport_Click(object sender, EventArgs e)
        {
            try
            {
                gridExport.WritePdfToResponse("PromocioniPlus", true);
            } catch (Exception)
            {
            }
        }

        protected void btnXlsxExport_Click(object sender, EventArgs e)
        {
            try
            {
                gridExport.WriteXlsxToResponse("PromocioniPlus", true);
            } catch (Exception)
            {
            }
        }

        protected void btnCsvExport_Click(object sender, EventArgs e)
        {
            try
            {
                gridExport.WriteCsvToResponse("PromocioniPlus", true);
            } catch (Exception)
            {
            }
        }

        /// <summary>
        /// perdoret ne rastet kur grida ben callback per ta rimbushur griden
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void gv_AlphawebEnhancments_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            if (e.CallbackName == "COLUMNMOVE" && gv_AlphawebEnhancments.AllColumns[int.Parse(e.Args[0])].Width.Value == 0)
                gv_AlphawebEnhancments.AllColumns[int.Parse(e.Args[0])].Width = Unit.Percentage(3);
        }

        /// <summary>
        /// perdoret per te vendosur filtrat tek headeri i grides
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gv_AlphawebEnhancments_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            //if (e.Column.FieldName == "Klienti" || e.Column.FieldName == "Targa" || e.Column.FieldName == "ModelAlphawebEnhancment")
            //{
            //    e.Values.Clear();
            //    e.AddValue("(Te gjithe)", string.Empty, "true");
            //    e.AddValue("Nga A-D ", string.Empty, e.Column.FieldName + ">'A     ' and " + e.Column.FieldName + " <'DDDDDDD'");
            //    e.AddValue("Nga D-G ", string.Empty, e.Column.FieldName + ">'D     ' and " + e.Column.FieldName + "<'GGGGGGG'");
            //    e.AddValue("Nga H-K ", string.Empty, e.Column.FieldName + ">'H     ' and " + e.Column.FieldName + "<'KKKKKKK'");
            //    e.AddValue("Nga L-O ", string.Empty, e.Column.FieldName + ">'L     ' and " + e.Column.FieldName + "  <'OOOOOOO'");
            //    e.AddValue("Nga P-S ", string.Empty, e.Column.FieldName + ">'P     ' and " + e.Column.FieldName + "<'SSSSSSS'");
            //    e.AddValue("Nga T-W ", string.Empty, e.Column.FieldName + ">'T     ' and " + e.Column.FieldName + "<'WWWWWWW'");
            //    e.AddValue("Nga X-Z ", string.Empty, e.Column.FieldName + ">'X     ' and " + e.Column.FieldName + "<'ZZZZZZZ'");
            //}
            //else
            //{
            //    e.Values.Clear();
            //    e.AddValue("(Te gjithe)", string.Empty, "true");
            //}
        }

        protected void gv_AlphawebEnhancments_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string idkomponente = "";
            string kodkonfigurimi = "";
            int idNdermarrje = (int)hfState["idNdermarrje"];
            int idGjuhePerdoruesi = (int)hfState["idGjuha"];
            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                kodkonfigurimi = arr[1];
                idkomponente = arr[0];
                if (arr[2] != "")
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuhePerdoruesi, "gv_AlphawebEnhancments", "Shto_AlphawebEnhancments.aspx", idNdermarrje);
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], idNdermarrje, koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        gv_AlphawebEnhancments.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gv_AlphawebEnhancments);                        
                        konfiguroVleraFillestare(idNdermarrje);
                        hfStatusi.Value = "true";
                    }
                    else
                        hfStatusi.Value = "false";
                }
            }
            else
                if (arr.Length == 2)//nese eshte zgjedhur nje konfigurim tek combo e konfigurimeve
            {
                kodkonfigurimi = arr[1];
                idkomponente = arr[0]; //konfiguroGride(idNdermarrje, kodkonfigurimi, Convert.ToInt32(idkomponente));
            }
            else
            {
                idkomponente = e.Parameters;
            }
            gv_AlphawebEnhancments.Selection.UnselectAll();
        }

        protected void gv_AlphawebEnhancments_CustomJSProperties(object sender, DevExpress.Web.ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gv_AlphawebEnhancments.PageIndex;
            e.Properties["cpPageRow"] = gv_AlphawebEnhancments.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gv_AlphawebEnhancments.VisibleRowCount;
        }

        /// <summary>
        /// perdoret per te shtuar kolonen e selektimit tek grida dhe per te vendosur disa karakteristika te grides
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void gv_AlphawebEnhancments_DataBound(object sender, EventArgs e)
        {// shton colonen # per selektim dhe disa karakteristika te grides
            if (this.gv_AlphawebEnhancments.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per 
                //perzgjidh
                DevExpress.Web.GridViewCommandColumn check = new DevExpress.Web.GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true;
                check.Width = Unit.Percentage(2);
                check.VisibleIndex = 0;
                gv_AlphawebEnhancments.Settings.ShowFilterRow = true;
                gv_AlphawebEnhancments.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                gv_AlphawebEnhancments.Settings.ShowFilterRowMenu = true;
                gv_AlphawebEnhancments.Columns.Add(check);
                gv_AlphawebEnhancments.KeyFieldName = "idKlientMeMSISDN";
                gv_AlphawebEnhancments.SettingsBehavior.AllowSelectByRowClick = true;
                gv_AlphawebEnhancments.SettingsBehavior.AllowFocusedRow = true;
            }
        }

        protected void gv_AlphawebEnhancments_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {

        }

        protected void ButtonOk_Click2(object sender, EventArgs e)
        {//fshirja e klientit me MSISDN
            List<object> rreshtat = gv_AlphawebEnhancments.GetSelectedFieldValues("idKlientMeMSISDN");
            if (rreshtat.Count == 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ju lutem zgjidhni te pakten nje rresht!", pnlMesazhi);
                return;
            }
            List<string> automjeteTeFshire = new List<string>(), automjeteTePaFshire = new List<string>();
            int idNdermarrje = (int)hfState["idNdermarrje"];
            int idPerdorues = (int)hfState["idPerdoruesi"];
            clsDatabaseRegjistrim dbRegjistrim = new clsDatabaseRegjistrim();
            clsKlientMeMSISDN kl = new clsKlientMeMSISDN();
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            for (int i = 0; i < rreshtat.Count; i++)
            {
                clsKlientMeMSISDN enhancment = new clsKlientMeMSISDN(Convert.ToInt32(rreshtat[i]));

                clsMesazh mesazh = enhancment.Fshi();

                if (!mesazh)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                    hfStatusi.Value = "false";
                }
                else
                {
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                    hfStatusi.Value = "true";
                    ASPxPageControl1.ActiveTabIndex = 0;

                    ASPxMenu1.Items.FindByName("GoldenNumbers").ClientVisible = true;
                    ASPxMenu1.Items.FindByName("NormalNumbers").ClientVisible = true;
                    ASPxMenu1.Items.FindByName("Fshi").ClientVisible = true;

                    ASPxMenu1.Items.FindByName("Ruaj").ClientVisible = false;
                    ASPxMenu1.Items.FindByName("Pastro").ClientVisible = false;
                    ASPxMenu1.Items.FindByName("Anullo").ClientVisible = false;

                    mbushGridAWEnhacmentsNgaDB(idNdermarrje);
                    konfiguroGride(idNdermarrje, idPerdorues);
                }
            }
        }
    }
}