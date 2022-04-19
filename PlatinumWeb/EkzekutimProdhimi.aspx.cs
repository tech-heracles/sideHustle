using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Web;
using System.Data;
using DbCore.DbRegjistrim;
using DbCore;
using System.Globalization;
using System.Resources;
using DbCore.DbInventari;
using DbCore.DbProdhimi;
using DbCore.DbShare;
using DbCore.DbAdmin;
using DbCore.IMBUtils.Types;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.DataBase;

namespace PlatinumWeb
{

    /// <summary>
    /// nderfaqja e EkzekutimProdhimit te prodhimit
    /// </summary>
    public partial class EkzekutimProdhimi : MyPageBase
    {
        private string komponente = "EkzekutimProdhimi.aspx";
        private string guidString;

        /// <summary>
        /// vendos themen e faqes
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>

        /// <summary>
        /// kur lodohet faqja  mbushet me te dhena dhe kontrollohen ne eshte i autorizuar perdoruesi
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            int idnderviti, idViti, idGjuha, idNdermarrje, idPerdoruesi, idNderViti;
            bool eshteMeme, eshteOwn;

            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (hfState.Count == 0)
            {
                idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
                idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
                eshteMeme = DbCore.mySessionObjects.merrEshteMemeSesioni(Session);
                idNderViti = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
                eshteOwn = DbCore.mySessionObjects.merrEshteOwnSesioni(Session);
                hfState.Set("idPerdoruesi", idPerdoruesi);
                hfState.Set("idGjuha", idGjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("idViti", idViti);
                hfState.Set("idNderViti", idNderViti);
                hfState.Set("eshteMeme", eshteMeme);
                hfState.Set("eshteOwn", eshteOwn);
                guidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                hfState.Set("guidString", guidString);
            }
            else
            {
                guidString = (string)hfState["guidString"];
                idPerdoruesi = (int)hfState["idPerdoruesi"];
                idNdermarrje = (int)hfState["idNdermarrje"];
                idViti = (int)hfState["idViti"];
                idGjuha = (int)hfState["idGjuha"];
                idNderViti = (int)hfState["idNderViti"];
                eshteMeme = (bool)hfState["eshteMeme"];
                eshteOwn = (bool)hfState["eshteOwn"];
            }
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                DbCore.clsFunksione.logout(Session, true, "FaqePaautorizuar");
            }

            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerdoruesi);
            }

            System.Globalization.CultureInfo ci = DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo(idGjuha);
            percaktoTemplateMenu(idGjuha, ASPxMenu1, idViti, idPerdoruesi, idNdermarrje);
            if (!IsPostBack)
            {
                ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 62, rm, ci, idGjuha);
                cmbKonfigurimi.SelectedIndex = 0;
                DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()), idGjuha);
                hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;
                mbushGridNgaDB();
                gvEkzekutimi.FilterExpression = "[IdStatusDok]=1";
                konfiguroGride(idGjuha, idNdermarrje, idPerdoruesi, rm, ci);
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi((int)hfState["idNdermarrje"], "gvEkzekutimi", gvEkzekutimi, cmbKonfigurimi.Text.Split(';')[0], "805", idGjuha);
                if (Request.QueryString["fshi"] == "rivleresimjo")
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("regjisDokMesazhGabimRivleresimi", ci), pnlMesazhi);
                }
                else if (Request.QueryString["fshi"] == "rivleresimpo")
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("regjisDokMesazhSuksesiRivleresim", ci), pnlMesazhi);
                else if (Request.QueryString["fshi"] == "po")
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("regjisDokMesazhSukesFshirjeDokument", ci), pnlMesazhi);
                else if (Request.QueryString["ruaj"] == "po")
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("regjMagModifikimiPerfundoiMeSukses", ci), pnlMesazhi);

                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
            }
            else
            {
                mbushGridNgaSession();
                konfiguroGride(idGjuha, idNdermarrje, idPerdoruesi, rm, ci);
                Container.Attributes["src"] = "";
            }
            GridUtil.konfigGrideListeEMadhePaTheme(gvEkzekutimi, "IdKokaEkzekutim");
                        
            clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idNdermarrje, "gvEkzekutimi", int.Parse(cmbKonfigurimi.Value.ToString()), "EkzekutimProdhimi.aspx");
            if (Request.QueryString["indexrow"] != null)
            {
                gvEkzekutimi.FocusedRowIndex = int.Parse(Request.QueryString["indexrow"]);
            }
            AspxWebControlUtils.perkthePopUp(popFshi, rm.GetString("labelKujdes", ci), lblMsgbox, rm.GetString("labelAdministrimiMsgJeniSigurt", ci), ButtonCancel, rm.GetString("labelAnullo", ci));
            gvEkzekutimi.PercaktoTitlePanel(this, MenuInfo, pnlMesazhi, hfState, idPerdoruesi, idNdermarrje, idViti, idGjuha, int.Parse(cmbKonfigurimi.Value.ToString()), "EkzekutimProdhimi.aspx", rm, ci);
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
            clsToolbarConfig.percaktoTemplateMenu(idGjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1, "EkzekutimProdhimi.aspx", this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, btnPo_Click, btnJo_Click, true, false, false, (bool)hfState["eshteMeme"], false);
        }
        

        public void btnJo_Click(object sender, EventArgs e)
        {
            //clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "", pnlMesazhi);
            if (pergjigja.Text.Contains("lidhur") || pergjigja.Text.Contains("kycur"))
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, pergjigja.Text, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, pergjigja.Text, pnlMesazhi);
            //CacheLayer.GlobalCacheManager.MySessionCache["trupat"] = new DbCore.DbRegjistrim.colTrupiMagazina();
            DbCore.mySessionObjects.ruajTrupatNeSession(Session, new DbCore.DbRegjistrim.colTrupiMagazina());
        }

        public void btnPo_Click(object sender, EventArgs e)
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            DbCore.DbRegjistrim.colTrupiMagazina trupat = DbCore.mySessionObjects.merrTrupatNgaSesioni(Session);

            DbCore.clsMesazh mesazh = new DbCore.clsMesazh(true, rm.GetString("regjMagMesazhSuksesRivleresimi", cultinf));
            string fileLogPath = Server.MapPath("~/log/log.txt");

            DbCore.DbAdmin.clsLogRivleresimInventari log = new DbCore.DbAdmin.clsLogRivleresimInventari();
            try
            {
                log = new DbCore.DbAdmin.clsLogRivleresimInventari((int)hfState["idPerdoruesi"], (int)hfState["idNdermarrje"]);
            }
            catch (Exception err)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
                throw new DbCore.MyException(rm.GetString("msgGabimGjateRuajtjesSeRivleresimitNeLog", cultinf));
            }
            foreach (DbCore.DbRegjistrim.clsTrupiMagazina t in trupat)
            {
                //DbCore.DbInventari.clsArtikulli art = new DbCore.DbInventari.clsArtikulli(t.IdArtikulli);
                mesazh = DbCore.DbInventari.clsArtikulli.rivleresimCmimiMesatar(t.IdArtikulli, DbCore.DbInventari.clsArtikulli.ktheMetodeKostoje(t.IdArtikulli), t.IdMag, t.Data, DateTime.Today, log, cultinf, rm, (int)hfState["idNdermarrje"],(int)hfState["idPerdoruesi"]);
                if (!mesazh.Status)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("regjisDokMesazhGabimRivleresimi", cultinf), pnlMesazhi);
                    log.logStopRivleresim("U ndalua rivleresimi shkurt", t.KodiArtikull);
                }
            }

            //CacheLayer.GlobalCacheManager.MySessionCache["trupat"] = new DbCore.DbRegjistrim.colTrupiMagazina();
            DbCore.mySessionObjects.ruajTrupatNeSession(Session, new DbCore.DbRegjistrim.colTrupiMagazina());
            if (mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("regjisDokMesazhSuksesiRivleresim", cultinf), pnlMesazhi);
                log.logFinishedRivleresim("Rivleresimi perfundoi me sukses - maxDelay: " + log.MaxTs.ToString());
            }
        }


        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu((int)hfState["idGjuha"], ASPxMenu1, (int)hfState["idViti"], (int)hfState["idPerdoruesi"], (int)hfState["idNdermarrje"]);
        }

        /// <summary>
        /// mbush griden me te dhena te ruajtura ne sesion
        /// </summary>
        private void mbushGridNgaSession()
        {
            DataTable tmpObject;
            bool sukses = DbCore.mySessionObjects.merrGrideNgaSessioni(komponente, Session, out tmpObject);
            if (!sukses)
                mbushGridNgaDB();
            else
            {
                gvEkzekutimi.DataSource = tmpObject;
                gvEkzekutimi.DataBind();
                tmpObject.Dispose();
            }
        }

        /// <summary>
        /// mbush griden me te dhena nga db
        /// </summary>
        private void mbushGridNgaDB()
        {//mbush griden e popupit me te dhena            
            DataTable dt = DbCore.DbProdhimi.colKokaEkzekutim.merrKokaEkzekutimDT((int)hfState["idNderViti"], (int)hfState["idPerdoruesi"]);
            DbCore.mySessionObjects.ruajGrideNeSession(komponente, Session, dt);
            gvEkzekutimi.DataSource = dt;
            gvEkzekutimi.DataBind();
            dt.Dispose();
        }

        /// <summary>
        /// konfiguron griden
        /// </summary>
        private void konfiguroGride(int idGjuha, int idndermarje,int idperdoruesi,  ResourceManager rm, CultureInfo ci)
        {
            KonfigurimComboGride.ShtoStatus(gvEkzekutimi, rm, ci);
            KonfigurimComboGride.ShtoModel(gvEkzekutimi, 45, idndermarje, idperdoruesi, idGjuha, Session, komponente, guidString);
            KonfigurimComboGride.ShtoNivel(gvEkzekutimi, 45, idndermarje, idperdoruesi, idGjuha, Session, komponente, guidString);
            KonfigurimComboGride.ShtoMagazinaNdermarrje(gvEkzekutimi, Session, komponente, guidString, "IdMagProdukti"); 
            KonfigurimComboGride.ShtoMagazinaNdermarrje(gvEkzekutimi, Session, komponente, guidString, "IdMagReceptura");

            gvEkzekutimi.Columns["#"].VisibleIndex = 0;
        }

        //private void shtoStatus() 
        //{
        //    DbCore.DbArkaBanka.clsDatabaseArkaBanka data = new DbCore.DbArkaBanka.clsDatabaseArkaBanka();
        //    gvEkzekutimi.Columns.Remove(gvEkzekutimi.Columns["IdStatusDok"]);
        //    GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
        //    DataSet ds = data.merrStatusinDokumentave();
        //    data.Dispose();
        //    DataRow dr = ds.Tables[0].NewRow();
        //    object[] rowArray = new object[2]; rowArray[0] = null; rowArray[1] = "";
        //    dr.ItemArray = rowArray;
        //    ds.Tables[0].Rows.InsertAt(dr, 0);
        //    colnew.PropertiesComboBox.DataSource = ds;
        //    colnew.PropertiesComboBox.TextField = ds.Tables[0].Columns[1].ToString();
        //    colnew.PropertiesComboBox.ValueField = ds.Tables[0].Columns[0].ToString();
        //    colnew.FieldName = "IdStatusDok"; 
        //    gvEkzekutimi.Columns.Add(colnew);
        //    ds.Dispose();
        //}



        /// <summary>
        /// kur grida ben databound per ti shtuar kolonen e selektimit
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvEkzekutimi_DataBound(object sender, EventArgs e)
        {
            if (gvEkzekutimi.Columns["#"] == null)
            {
                GridViewCommandColumn check = new GridViewCommandColumn("#") { ShowSelectCheckbox = true, Width = System.Web.UI.WebControls.Unit.Percentage(2) };
                gvEkzekutimi.Settings.ShowFilterRow = true;
                gvEkzekutimi.Settings.ShowHeaderFilterButton = true;
                gvEkzekutimi.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                gvEkzekutimi.Settings.ShowFilterRowMenu = true;
                gvEkzekutimi.Columns.Add(check);
                gvEkzekutimi.Settings.ShowGroupPanel = true;
                gvEkzekutimi.KeyFieldName = "IdKokaEkzekutim";
                gvEkzekutimi.SettingsBehavior.AllowSelectByRowClick = true;
                gvEkzekutimi.SettingsBehavior.AllowFocusedRow = true;
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
            MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;

            DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka((int)hfState["idGjuha"], "gvEkzekutimi", "EkzekutimProdhimi.aspx", (int)hfState["idNdermarrje"], int.Parse(cmbKonfigurimi.Value.ToString()));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, (int)hfState["idNdermarrje"], koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                filtra.IdPerdoruesi = (int)hfState["idPerdoruesi"];
                mesazh = filtra.fshi();
                //mbushComboBoxFiltra();
                clsToolbarConfig.mbushComboBoxFiltra((int)hfState["idGjuha"], (int)hfState["idNdermarrje"], "gvEkzekutimi", int.Parse(cmbKonfigurimi.Value.ToString()), "EkzekutimProdhimi.aspx");
                percaktoTemplateMenu((int)hfState["idGjuha"], ASPxMenu1, (int)hfState["idViti"], (int)hfState["idPerdoruesi"], (int)hfState["idNdermarrje"]);
                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                gvEkzekutimi.FilterExpression = " [IdStatusDok]=1 ";


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
            MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka((int)hfState["idGjuha"], "gvEkzekutimi", "EkzekutimProdhimi.aspx", (int)hfState["idNdermarrje"], int.Parse(cmbKonfigurimi.Value.ToString()));
            DbCore.DbAdmin.clsFiltraGrida filtri = new DbCore.DbAdmin.clsFiltraGrida() { FiltraKodi = cmbFiltra.Text, FiltraShenime = cmbFiltra.Text, FiltraUniversal = false, GridaKokaId = koka.IdGridaKoka, FiltraVlera = gvEkzekutimi.FilterExpression, IdPerdoruesi = (int)hfState["idPerdoruesi"], IdNdermarje = (int)hfState["idNdermarrje"], IdStatusDok = 1 };
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("NrDok", gvEkzekutimi);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvEkzekutimi.GetSortedColumns();
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

            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            mesazh = filtri.ruaj();
            //mbushComboBoxFiltra();
            clsToolbarConfig.mbushComboBoxFiltra((int)hfState["idGjuha"], (int)hfState["idNdermarrje"], "gvEkzekutimi", int.Parse(cmbKonfigurimi.Value.ToString()), "EkzekutimProdhimi.aspx");
            percaktoTemplateMenu((int)hfState["idGjuha"], ASPxMenu1, (int)hfState["idViti"], (int)hfState["idPerdoruesi"], (int)hfState["idNdermarrje"]);
            if (mesazh.Status == true)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

            cmbFiltra.Text = "";
        }

        /// <summary>
        /// eventet e menuse
        /// </summary>
        /// <param name="source">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            switch (e.Item.Name)
            {
                case "PrintPreview":
                    if (gvEkzekutimi.FocusedRowIndex == -1)
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgZgjidhniNjeDokumentPerTePrintuar", cultinf), pnlMesazhi);
                        Container.Attributes["src"] = "";
                    }
                    else
                    {
                        string id = gvEkzekutimi.GetRowValues(gvEkzekutimi.FocusedRowIndex, "IdKokaEkzekutim").ToString();
                        Container.Attributes["src"] = "RaportiShpejte.aspx?Sesioni=false&emriReal=urdherPune&idDokumenti=" + int.Parse(id) + "&printo=false";
                    }
                    break;
                case "Riruaj":
                    Riruaj(DbCore.clsFunksione.GetKomponente(Page.Request), cultinf, rm, (bool)hfState["eshteMeme"], (int)hfState["idGjuha"]);
                    break;
            }
        }

        protected void Riruaj(string komponente, CultureInfo ci, ResourceManager rm, bool eshteMeme, int idGjuha)
        {
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            List<string> TeRuajtur = new List<string>(), Teparuajtur = new List<string>();
            pergjigja.Text = "";

            List<object> rreshtat = gvEkzekutimi.GetSelectedFieldValues("IdKokaEkzekutim");
            pergjigja.Text = "";          

            DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            int idPerdoruesi = (int)hfState["idPerdoruesi"];
            int idNdermarrje = (int)hfState["idNdermarrje"];
            int idNdermarrjeVit = (int)hfState["idNderViti"];
            
            bool eshteOwn = (bool)hfState["eshteOwn"];
            DataTable err = new DataTable();
            err.Columns.Add("Kodi");
            err.Columns.Add("Gabimi");
            err.Columns.Add("Rreshti");
            int nrreshta = 0;

            if (rreshtat.Count == 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("regjMagMesazhZgjidhniNje", ci), pnlMesazhi);
                return;
            }

            foreach (object id in rreshtat)
            {
                nrreshta++;
                clsKokaEkzekutim clsKoka = new clsKokaEkzekutim();

                clsKoka.mbushKokaEkzekutimSipasIDPaTrup(Convert.ToInt32(id));
                string kodKonfigurimi = clsKonfigurimAmbjenti.ktheKodKonfigurimi(clsKoka.IdKonfigAmbjente);
                if (DbCore.MbylljePeriudhe.PeriodClosing.IsPeriodClosed(clsKoka.DtDok, MyConnectionsManager.GetSelectedConNameServer(), (int)hfState["idNdermarrje"], KategoriDokumenti.EkzekutimProdhimi, clsKoka.IdKonfigAmbjente))
                {
                    object[] arr = { kodKonfigurimi + " " + clsKoka.NrDok + " " + clsKoka.DtDok.ToShortDateString(), DbCore.IMBUtils.Messages.MessagesResource.Messages["msgPeriodIsClosed"], nrreshta };
                    err.Rows.Add(arr);
                    continue;
                }

                if (clsKoka.IdStatusDok == 2)
                    continue;

                bool lidhur = clsKoka.eshteILidhur();

                colProduktProdhimi colProdukte = new colProduktProdhimi();
                colProdukte = clsKoka.merrProdukteProdhimi();

                for (int j = 0; j < colProdukte.Count; j++)
                {
                    colProdukte[j].ColReceptura = new colRecepturaProdhimi(colProdukte[j].Id);
                }

                //DbCore.DbRegjistrim.clsKokaMagazina maghyrje = new DbCore.DbRegjistrim.clsKokaMagazina();
                //DbCore.DbRegjistrim.clsKokaMagazina magdalje = new DbCore.DbRegjistrim.clsKokaMagazina();
                //magdalje.mbushKokaMagazinaSipasIDGjenerues(clsKoka.IdKokaEkzekutim, 2, clsKoka.IdKonfigAmbjente);
                //maghyrje.mbushKokaMagazinaSipasIDGjenerues(clsKoka.IdKokaEkzekutim, 1, clsKoka.IdKonfigAmbjente);

                clsKokaEkzekutim clsKokaRe = new clsKokaEkzekutim();
                string kodMag = "", kodMagRec = "", kodNjesiProdhimi = "", shfaqmesazhapolupe;

                if (clsKoka.IdMagProdukti > 0)
                    kodMag = clsNjesiAdministrative.ktheKodiNjesiAdministrativeSipasiD(clsKoka.IdMagProdukti, idPerdoruesi);

                if (clsKoka.IdMagReceptura > 0)
                    kodMagRec = clsNjesiAdministrative.ktheKodiNjesiAdministrativeSipasiD(clsKoka.IdMagReceptura, idPerdoruesi);

                if (clsKoka.IdNjesiProdhimi > 0)
                    kodNjesiProdhimi = clsNjesiProdhimi.ktheKodNjesiProdhimiSipasId(clsKoka.IdNjesiProdhimi);

                mesazh = clsKokaRe.krijoEkzekutim(clsKoka.IdNivel, clsKoka.IdKonfigAmbjente, clsKoka.IdMagProdukti, kodMag, clsKoka.IdMagReceptura, kodMagRec, clsKoka.DtDok, clsKoka.NrDok, 1, idNdermarrje, idNdermarrjeVit, idPerdoruesi, clsKoka.DtRegj, clsKoka.Shenime, clsKoka.Totali, clsKoka.IdGrup1, clsKoka.IdGrup2, clsKoka.IdGrup3, colProdukte, clsKoka.IdNjesiProdhimi, kodNjesiProdhimi);

                if (!mesazh.Status)
                {
                    object[] arr = { kodKonfigurimi + " " + clsKoka.NrDok + " " + clsKoka.DtDok.ToShortDateString(), mesazh.PershkrimMesazhi, nrreshta };
                    err.Rows.Add(arr);
                    continue;
                }

                clsKokaRe.IdKokaEkzekutim = clsKoka.IdKokaEkzekutim;
                //DbCore.DbAdmin.clsPeriudhaKontabel periudha = new DbCore.DbAdmin.clsPeriudhaKontabel(clsKoka.DtDok, idNdermarrje);
                int idPeriudhaKontabel = clsPeriudhaKontabel.ktheIdPeriudheSipasDatesDheNdermarrjes(clsKoka.DtDok, idNdermarrje);
                bool meKontabilizim = false;
                if (clsAlternativaKushti.getAlternativa(clsKoka.IdKonfigAmbjente, "GJK") != "Jo")
                    meKontabilizim = true;
                colPlanifikimEkzekutim colPlanifikime = merrPlanifikimEkzekutimi(clsKoka.IdKokaEkzekutim);

                DbCore.DbShare.clsKusht kushtHyrje = new DbCore.DbShare.clsKusht(clsKoka.IdKonfigAmbjente, "ZKDM");
                DbCore.DbShare.clsKonfigurimAmbjenti konfigHyrje = new DbCore.DbShare.clsKonfigurimAmbjenti(kushtHyrje.Vlera);

                DbCore.DbShare.clsKusht kushtDalje = new DbCore.DbShare.clsKusht(clsKoka.IdKonfigAmbjente, "ZKDM2");
                DbCore.DbShare.clsKonfigurimAmbjenti konfigDalje = new DbCore.DbShare.clsKonfigurimAmbjenti(kushtDalje.Vlera);

                mesazh = clsKokaRe.modifiko(lidhur, colPlanifikime, idPeriudhaKontabel, meKontabilizim, konfigDalje, konfigHyrje, out shfaqmesazhapolupe, eshteOwn, idGjuha, rm, ci, true);

                if (!mesazh.Status)
                {
                    object[] arr = { kodKonfigurimi + " " + clsKoka.NrDok + " " + clsKoka.DtDok.ToShortDateString(), mesazh.PershkrimMesazhi, nrreshta };
                    err.Rows.Add(arr);
                    continue;
                }
            }

            DbCore.mySessionObjects.ruajTabeleGabimeshImporti(Session, err);
            if (err.Rows.Count > 0)
            {
                clsKokaErrorImporti koka = new clsKokaErrorImporti(0, "Nga riruatja e dokumenteve te prodhimit", 45, idNdermarrje, idPerdoruesi);
                koka.ColTrupi.mbushErrorImportiNgaProgrami(err);
                mesazh = koka.ruajErrorImporti();
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "U riruajten " + (rreshtat.Count - err.Rows.Count) + " rreshta dhe deshtuan " + err.Rows.Count + " rreshta! ", pnlMesazhi);
                Container.Attributes["src"] = "RaportiShpejte.aspx?Sesioni=false&emriReal=gabimeImporti&printo=false&db=jo";
            }
            else
            {
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "U riruajten te gjitha rreshtat!", pnlMesazhi);
            }

            gvEkzekutimi.Selection.UnselectAll();
            mbushGridNgaDB();
            konfiguroGride(idGjuha, idNdermarrje, idPerdoruesi, rm, ci);
        }

        private colPlanifikimEkzekutim merrPlanifikimEkzekutimi(int idKokaDok)
        {
            colPlanifikimEkzekutim colPlane = new colPlanifikimEkzekutim();
            colPlanifikimEkzekutim colPlanifikimEkzekutimi = new colPlanifikimEkzekutim(idKokaDok);

            for (int i = 0; i < colPlanifikimEkzekutimi.Count; i++)
            {
                clsPlanifikimEkzekutim planifikimEkzekutimi = new clsPlanifikimEkzekutim();
                planifikimEkzekutimi.IdPlanifikimi = colPlanifikimEkzekutimi[i].IdPlanifikimi;
                clsKokaPlanifikim kokaPlanifikim = new clsKokaPlanifikim(planifikimEkzekutimi.IdPlanifikimi);
                planifikimEkzekutimi.IdKonfigAmbjentePlanifikimi = kokaPlanifikim.IdKonfigAmbjente;
                colPlane.Add(planifikimEkzekutimi);
            }
            return colPlane;
        }

        /// <summary>
        /// fshin rreshtat e selektuar
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            List<string> TeFshire = new List<string>(), TeLidhur = new List<string>(), PeriudheKycur = new List<string>(), GjendjeNegative = new List<string>(), closedPeriod = new List<string>();
            pergjigja.Text = "";
            DbCore.DbRegjistrim.colTrupiMagazina tr = new DbCore.DbRegjistrim.colTrupiMagazina();
            DbCore.DbRegjistrim.colTrupiMagazina trupat = new DbCore.DbRegjistrim.colTrupiMagazina();
            bool rivleresim = false;
            //clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "", pnlMesazhi);
            List<object> rreshtat = gvEkzekutimi.GetSelectedFieldValues("IdKokaEkzekutim");
            pergjigja.Text = "";
           // DbCore.DbAdmin.clsPeriudhaKontabel periudha;
            DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            foreach (object id in rreshtat)
            {
                DbCore.DbProdhimi.clsKokaEkzekutim clsKoka = new DbCore.DbProdhimi.clsKokaEkzekutim(Convert.ToInt32(id));
                bool ekycur = DbCore.DbAdmin.clsPeriudhaKontabel.eshteKycurPeriudheSipasDateDheNdermarrjes(clsKoka.DtDok, (int)hfState["idNdermarrje"]);
                if (ekycur)
                {
                    PeriudheKycur.Add(clsKoka.NrDok);
                    continue;
                }

                if (DbCore.MbylljePeriudhe.PeriodClosing.IsPeriodClosed(clsKoka.DtDok, MyConnectionsManager.GetSelectedConNameServer(), (int)hfState["idNdermarrje"], KategoriDokumenti.EkzekutimProdhimi, clsKoka.IdKonfigAmbjente))
                {
                    closedPeriod.Add(clsKoka.NrDok);
                    continue;
                }


                bool kontrollorivleresim = clsAlternativaKushti.getAlternativa(clsKoka.IdKonfigAmbjente, "KR") == "Po";
                bool lidhur = dbAdmin.eshteDokumentiILidhur(clsKoka.IdKokaEkzekutim, clsKoka.IdNivel, "T_KOKAEKZEKUTIMPRODHIMI", "IDKOKA");
                bool autorizimet = DbCore.DbProdhimi.clsKokaEkzekutim.kaAutorizime(clsKoka.IdKokaEkzekutim, (int)hfState["idPerdoruesi"]);
                if (!autorizimet)
                    lidhur = true;
                if (lidhur)
                {
                    TeLidhur.Add(clsKoka.NrDok);
                    continue;
                }

                DbCore.DbRegjistrim.clsKokaMagazina kokhyrje = new clsKokaMagazina();
                DbCore.DbRegjistrim.clsKokaMagazina kokdalje = new clsKokaMagazina();

                kokdalje.mbushKokaMagazinaSipasIDGjenerues(clsKoka.IdKokaEkzekutim, 2, clsKoka.IdKonfigAmbjente);
                kokhyrje.mbushKokaMagazinaSipasIDGjenerues(clsKoka.IdKokaEkzekutim, 1, clsKoka.IdKonfigAmbjente);
                if (kokdalje.IdKokaMagazina != 0)
                {
                    kokdalje.mbushTrupMagazine(false);
                    colArtikujt coleksistues1 = new colArtikujt(kokdalje.IdKokaMagazina, new clsDatabaseInventari());
                    int i2 = 0;
                    foreach (clsTrupiMagazina trup in kokdalje.OcolTrupiMagazina)
                    {
                        if (trup.IdLlojVeprimi == 1)
                            trup.Element = coleksistues1[i2];
                        i2++;
                    }
                    if (!kokdalje.kontrolloGjendjeNeFshirje(new DbCore.DbRegjistrim.colTrupiMagazina(), 0).Status)
                    {
                        GjendjeNegative.Add(kokdalje.NrDok);
                        continue;
                    }
                }
                if (kokhyrje.IdKokaMagazina != 0)
                {
                    kokhyrje.mbushTrupMagazine(false);
                    colArtikujt coleksistues1 = new colArtikujt(kokhyrje.IdKokaMagazina, new clsDatabaseInventari());
                    int i2 = 0;
                    foreach (clsTrupiMagazina trup in kokhyrje.OcolTrupiMagazina)
                    {
                        if (trup.IdLlojVeprimi == 1)
                            trup.Element = coleksistues1[i2];
                        i2++;
                    }
                    if (!kokhyrje.kontrolloGjendjeNeFshirje(new DbCore.DbRegjistrim.colTrupiMagazina(), 0).Status)
                    {
                        GjendjeNegative.Add(kokhyrje.NrDok);
                        continue;
                    }
                }
                if (kokdalje.IdKokaMagazina != 0 && kontrollorivleresim && kokdalje.IdStatusDok != 0)
                    if (kokdalje.rivleresim())
                    {
                        rivleresim = true;
                        tr.mbushGjitheTrupiMagazinaNgaKoka(kokdalje.IdKokaMagazina);
                        trupat.AddRange(tr);
                    }
                if (kokhyrje.IdKokaMagazina != 0 && kontrollorivleresim && kokhyrje.IdStatusDok != 0)
                    if (kokhyrje.rivleresim())
                    {
                        rivleresim = true;
                        tr.mbushGjitheTrupiMagazinaNgaKoka(kokhyrje.IdKokaMagazina);
                        trupat.AddRange(tr);
                    }
                clsKoka.IdPerdoruesi = (int)hfState["idPerdoruesi"];
                mesazh = clsKoka.fshi();
                //Session.Add("trupat", trupat);
                DbCore.mySessionObjects.ruajTrupatNeSession(Session, trupat);
                if (mesazh.Status)
                {
                    hiqNgaGrida(clsKoka.IdKokaEkzekutim);
                    TeFshire.Add(clsKoka.NrDok);
                }
            }
            dbAdmin.Dispose();
            string mesazhInfoGabimLidhur = "", mesazhInfoGabimPeridheKycur = "", mesazhInfoSukses = "", mesazhGjendjeNegative = "", mesazhClosedPeriod ="";

            if (TeLidhur.Count == 1)
                mesazhInfoGabimLidhur = String.Format("{0}{1}{2}", rm.GetString("msgDokumentiEkzekutimProdhimitMeNr", cultinf), String.Join(";", TeLidhur), rm.GetString("msgListaLidhjaSuffixNjejesGabimi", cultinf));
            else
                if (TeLidhur.Count > 1)
                mesazhInfoGabimLidhur = String.Format("{0}{1}{2}", rm.GetString("msgDokumentatEEkzekutimProdhimitMeNr", cultinf), String.Join(";", TeLidhur), rm.GetString("msgListaLidhjaSuffixShumesGabimi", cultinf));
            if (PeriudheKycur.Count == 1)
                mesazhInfoGabimPeridheKycur = String.Format("{0}{1}{2}", rm.GetString("msgDokumentiEkzekutimProdhimitMeNr", cultinf), String.Join(";", PeriudheKycur), rm.GetString("regjMagSuffixMesazhNjejesPeriudheKycurGabimi", cultinf));
            else
                if (PeriudheKycur.Count > 1)
                mesazhInfoGabimPeridheKycur = String.Format("{0}{1}{2}", rm.GetString("msgDokumentatEEkzekutimProdhimitMeNr", cultinf), String.Join(";", PeriudheKycur), rm.GetString("regjMagSuffixMesazhShumesPeriudheKycurGabimi", cultinf));
            if (GjendjeNegative.Count == 1)
                mesazhGjendjeNegative = String.Format("{0}{1}{2}", rm.GetString("msgDokumentiEkzekutimProdhimitMeNr", cultinf), String.Join(";", GjendjeNegative), rm.GetString("regjMagSuffixMesazhNjejesGjendjeNegative", cultinf));
            else
                if (GjendjeNegative.Count > 1)
                mesazhGjendjeNegative = String.Format("{0}{1}{2}", rm.GetString("msgDokumentatEEkzekutimProdhimitMeNr", cultinf), String.Join(";", GjendjeNegative), rm.GetString("regjMagSuffixMesazhShumesGjendjeNegative", cultinf));


            if (closedPeriod.Count == 1)
                mesazhClosedPeriod = String.Format("{0}{1}{2}", rm.GetString("regjisDokPrefixMesazhNjejes", ci), String.Join(", ", closedPeriod), rm.GetString("suffixMesazhClosedPeriodNjejes", ci));
            else if (closedPeriod.Count > 1)
                mesazhClosedPeriod = String.Format("{0}{1}{2}", rm.GetString("regjisDokPrefixMesazhNjejes", ci), String.Join(", ", closedPeriod), rm.GetString("suffixMesazhClosedPeriodShumes", ci));

            if (TeFshire.Count == 1)
                mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("msgDokumentiEkzekutimProdhimitMeNr", cultinf), String.Join(";", TeFshire), rm.GetString("msgCeljeMagazinatSuffixNjejesSuksesi", cultinf));
            else
                if (TeFshire.Count > 1)
                mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("msgDokumentatEEkzekutimProdhimitMeNr", cultinf), String.Join(";", TeFshire), rm.GetString("regjMagSuffixMesazhShumesSuksesi", cultinf));

            mesazhInfoGabimLidhur += mesazhInfoGabimPeridheKycur + mesazhGjendjeNegative + mesazhClosedPeriod;
            if (mesazhInfoGabimLidhur != "" && mesazhInfoSukses != "")
            {
                mesazhInfoGabimLidhur += rm.GetString("msgLidhesMesazhi", cultinf) + mesazhInfoSukses;
                pergjigja.Text = mesazhInfoGabimLidhur;
                pergjigja.ClientVisible = false;
            }
            else
            {
                pergjigja.Text = mesazhInfoSukses;
                pergjigja.ClientVisible = false;
            }
            if (rreshtat.Count == 0)
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("regjMagMesazhZgjidhniNje", cultinf), pnlMesazhi);
            else if (rivleresim)
                clsMenuInfo.ShtoPyetje(MenuInfo, rm.GetString("regjMagVeprimiSjellNdryshimNeCmimDalje", cultinf), pnlMesazhi, (int)hfState["idGjuha"]);
            else
                if (mesazhInfoGabimLidhur != "")
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhInfoGabimLidhur, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazhInfoSukses, pnlMesazhi);

        }

        /// <summary>
        /// heq nga grida reshtin e fshire
        /// </summary>
        /// <param name="idkoka">id e reshtit te fshire</param>
        private void hiqNgaGrida(int idkoka)
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (gvEkzekutimi.DataSource != null)
            {
                DataTable dt = (DataTable)gvEkzekutimi.DataSource;
                DataRow[] drs = dt.Select("IdKokaEkzekutim = " + idkoka);
                if (drs.Length > 1)
                    throw new Exception(rm.GetString("msgNdodhen2DokumentaEkzekutimProdhimi"));
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                dt.Rows.Remove(dr);
                gvEkzekutimi.DataSource = dt;
                gvEkzekutimi.DataBind();
                dt.Dispose();
            }
            else mbushGridNgaDB();
        }

        /// <summary>
        /// kur grida ben callback
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvEkzekutimi_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            if (e.CallbackName == "COLUMNMOVE" && gvEkzekutimi.AllColumns[int.Parse(e.Args[0])].Width.Value == 0)
                gvEkzekutimi.AllColumns[int.Parse(e.Args[0])].Width = System.Web.UI.WebControls.Unit.Percentage(3);
            if (e.CallbackName == "APPLYFILTER" && e.Args[0] == "")
            {
                MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
                ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
                cmbFiltra.Text = "";
            }
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            GridUtil.ToolTipButonaveMbiGride(gvEkzekutimi, ci, rm);
            // konfiguroGride();
        }

        /// <summary>
        /// kur perdoruesi i ben grides callbak per te mare konfigurimin e ri
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvEkzekutimi_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string[] arr = e.Parameters.Split(';');
            GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi((int)hfState["idNdermarrje"], "gvEkzekutimi", gvEkzekutimi, cmbKonfigurimi.Text.Split(';')[0], "805", (int)hfState["idGjuha"]);
            if (arr.Length == 3)
            {
                if (arr[2] == "")
                    gvEkzekutimi.FilterExpression = " [IdStatusDok]=1 ";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka((int)hfState["idGjuha"], "gvEkzekutimi", "EkzekutimProdhimi.aspx", (int)hfState["idNdermarrje"], int.Parse(cmbKonfigurimi.Value.ToString()));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], (int)hfState["idNdermarrje"], koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        gvEkzekutimi.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvEkzekutimi);
                    }
                }
            }


            gvEkzekutimi.Selection.UnselectAll();
        }

        /// <summary>
        /// perdoret per te vendosur karakteristika te griden ne javascript
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvEkzekutimi_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gvEkzekutimi.PageIndex;
            e.Properties["cpPageRow"] = gvEkzekutimi.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gvEkzekutimi.VisibleRowCount;
        }

        /// <summary>
        /// perdoret per te hequr filtrimin e grides kur zgjedhim elementin bosh
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvEkzekutimi_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if (e.Column.FieldName == "IdMagProdukti" ||
                 e.Column.FieldName == "IdMagReceptura" || e.Column.FieldName == "IdKonfigAmbjente")
                if (Converter.ConvertToInt(e.Value) == 0)
                {
                    e.Criteria = null;
                }
        }

        /// <summary>
        /// perdoret per te shtuar menyra filtrimi
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvEkzekutimi_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
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
    }
}