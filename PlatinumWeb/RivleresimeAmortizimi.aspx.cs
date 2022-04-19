using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Web;
using System.Data;
using PlatinumWeb.Templates;
using DbCore;
using System.Globalization;
using System.Resources;
using DbCore.DbShare;
using DbCore.DbKontabiliteti;
using DbCore.DbRegjistrim;
using DbCore.DbAdmin;
using DbCore.IMBUtils.Types;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.DataBase;

namespace PlatinumWeb
{
    public partial class RivleresimeAmortizimi : MyPageBase
    {
        /// <summary>
        /// id e ndermarjes
        /// </summary>
        private int idndermarje, /// <summary>
            /// id e perdoruesit
            /// </summary>
                                 idperdoruesi, /// <summary>
            /// id e ndermarje vitit
            /// </summary>
                                               idnderviti, idviti;

        private string komponente => DbCore.clsFunksione.GetKomponente(Page.Request);

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
            int idPerdoruesi, idGjuha, idNdermarrje, idViti, idNdermarrjeVit;
            bool eshteOwn, eshteMeme;
            string  guidString, komponente = DbCore.clsFunksione.GetKomponente(Page.Request);
            if (!DbCore.mySessionObjects.isLogedIn(Session))
                DbCore.clsFunksione.logout(Session,true,"FaqePaautorizuar");
            idperdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idperdoruesi);
            idndermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            idviti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            idnderviti = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
             idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            percaktoTemplateMenu(idGjuha, ASPxMenu1, idviti, idperdoruesi, idndermarje);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            System.Globalization.CultureInfo cultinf = DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo(idGjuha);
            if (!IsPostBack)
            {
                idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
                idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
                eshteMeme = DbCore.mySessionObjects.merrEshteMemeSesioni(Session);
                hfState.Set("idPerdoruesi", idPerdoruesi);
                hfState.Set("idGjuha", idGjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("idViti", idViti);
                hfState.Set("eshteMeme", eshteMeme);
                idNdermarrjeVit = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
                eshteOwn = DbCore.mySessionObjects.merrEshteOwnSesioni(Session);

                guidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                hfState.Set("idNdermarrjeVit", idNdermarrjeVit);
                hfState.Set("OwnShop", eshteOwn);
                hfState.Set("guidString", guidString);
                vendosHfMePerkthime(rm, cultinf);
                if (Request.QueryString["lloj"] == "amortizim")
                    ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idperdoruesi, idndermarje, cmbKonfigurimi, 89, "LDAMFI", rm, cultinf, idGjuha);
                else
                    ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idperdoruesi, idndermarje, cmbKonfigurimi, 89, "LDRIAM", rm, cultinf, idGjuha);
                cmbKonfigurimi.SelectedIndex = 0;
                DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()), idGjuha);
                hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;
                mbushGridNgaDB();
                konfiguroGride(idGjuha, rm, cultinf, guidString);
                string idkomponente = "1005";
                if (Request.QueryString["lloj"] == "rivleresim")
                    idkomponente = "1006";
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idndermarje, "gvAmortizimi", gvAmortizimi, cmbKonfigurimi.Text.Split(';')[0], idkomponente, idGjuha);

                gvAmortizimi.FilterExpression = " [IdStatusDokumenti]=1";
                
                if (Request.QueryString["fshi"] == "po")
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("regjisDokMesazhSukesFshirjeDokument", cultinf), pnlMesazhi);
                else if (Request.QueryString["ruaj"] == "po")
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("regjMagModifikimiPerfundoiMeSukses", cultinf), pnlMesazhi);
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idperdoruesi, idndermarje, idviti, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();

            }
            else
            {
                idPerdoruesi = (int)hfState["idPerdoruesi"];
                idGjuha = (int)hfState["idGjuha"];
                idNdermarrje = (int)hfState["idNdermarrje"];
                idViti = (int)hfState["idViti"];
               
                idNdermarrjeVit = (int)hfState["idNdermarrjeVit"];
                eshteMeme = (bool)hfState["eshteMeme"];
                guidString = (string)hfState["guidString"];
                mbushGridNgaSession();
                konfiguroGride(idGjuha, rm, cultinf, guidString);
                Container.Attributes["src"] = "";

            }
            GridUtil.konfigGrideListeEMadhePaTheme(gvAmortizimi, "IdAmortizimi");
            clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idndermarje, "gvAmortizimi", int.Parse(cmbKonfigurimi.Value.ToString()), DbCore.clsFunksione.GetKomponente(Page.Request));
            if (Request.QueryString["indexrow"] != null)
                gvAmortizimi.FocusedRowIndex = int.Parse(Request.QueryString["indexrow"]);
            gvAmortizimi.Columns["#"].VisibleIndex = 0;
            GridUtil.ToolTipButonaveMbiGride(gvAmortizimi, cultinf, rm);
        }

        private void vendosHfMePerkthime(ResourceManager rm, CultureInfo cultinf)
        {
            konfigurimi_Label.Text = rm.GetString("lblModeli", cultinf);
            hfState.Set("regjisDokNukKeniAsnjeDokTeZgjedhur", rm.GetString("regjisDokNukKeniAsnjeDokTeZgjedhur", cultinf));
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

        /// <summary>
        /// ruan konfigurimin e grides dhe filtrin e zgjedhur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RuajKolona_Click(object sender, EventArgs e)
        {
            int idfiltri = 0;
            int idndermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idperdorues = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            int idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            int idgjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            int idkonf = DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(cmbKonfigurimi.Text.Split(';')[0], idndermarrje);
            DbCore.clsMesazh mesazh = GridUtil.ruajFiltra(idndermarrje, idperdorues, idgjuha, "gvAmortizimi", DbCore.clsFunksione.GetKomponente(Page.Request), "FilterDefault", gvAmortizimi.FilterExpression, gvAmortizimi, "IdNiveli", idkonf, out idfiltri);//ruan filtrin e zgjedhur tek filtrat
            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                return;
            }
            int idkomponente = 1005;
            if (Request.QueryString["lloj"] == "rivleresim")
                idkomponente = 1006;
            mesazh = GridUtil.ruajkonfigurimgride(gvAmortizimi, cmbKonfigurimi.Text, idndermarrje, idperdorues, idkomponente, idfiltri, idViti, DbCore.mySessionObjects.ktheCultureInfo(Session), DbCore.mySessionObjects.ktheGjuhe(Session));
            clsToolbarConfig.mbushComboBoxFiltra(idgjuha, idndermarrje, "gvAmortizimi", int.Parse(cmbKonfigurimi.Value.ToString()), DbCore.clsFunksione.GetKomponente(Page.Request));
            percaktoTemplateMenu(idgjuha, ASPxMenu1, idViti, idperdorues, idndermarrje);
            if (mesazh.Status)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
        }

        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), ASPxMenu1, idviti, idperdoruesi, idndermarje);
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
                gvAmortizimi.DataSource = tmpObject;
                gvAmortizimi.DataBind();
                tmpObject.Dispose();
            }
        }

        /// <summary>
        /// mbush griden me te dhena nga db
        /// </summary>
        private void mbushGridNgaDB()
        {//mbush griden e popupit me te dhena   
            DataTable dt;
            if (Request.QueryString["lloj"] == "amortizim")
                dt = DbCore.DbAsete.colAmortizimiKoka.ktheAmortizimKokaSipasDtAmortizimFillestar(idnderviti, DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            else dt = DbCore.DbAsete.colAmortizimiKoka.ktheAmortizimKokaSipasDtRivleresim(idnderviti, DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            DbCore.mySessionObjects.ruajGrideNeSession(komponente, Session, dt);
            gvAmortizimi.DataSource = dt;
            gvAmortizimi.DataBind();
            dt.Dispose();
        }

        /// <summary>
        /// konfiguron griden
        /// </summary>
        private void konfiguroGride(int idGjuha, ResourceManager rm, CultureInfo ci, string guidString)
        {
            KonfigurimComboGride.ShtoStatus(gvAmortizimi, rm, ci, "IdStatusDokumenti");
            KonfigurimComboGride.ShtoModel(gvAmortizimi, 90 , idndermarje, idperdoruesi, idGjuha, Session, komponente, guidString, "IdKonfigurimAmbjenti");
            KonfigurimComboGride.ShtoNivel(gvAmortizimi, 90, idndermarje, idperdoruesi, idGjuha, Session, komponente, guidString, "IdNiveli");
            KonfigurimComboGride.ShtoMagazinaNdermarrje(gvAmortizimi, Session, komponente, guidString, "IdNjesiAdministrative");
            KonfigurimComboGride.ShtoStandart(gvAmortizimi, idndermarje, Session, komponente, guidString, "IdLlojStandarti");
           // KonfigurimComboGride.ShtoLlogariSipasNdermarrjesDhePerdoruesit(gvAmortizimi, idndermarje, idperdoruesi, Session, komponente, guidString, "IdLlogKunderparti");
            string idkomponente = "1005";
            if (Request.QueryString["lloj"] == "rivleresim")
            {
                idkomponente = "1006";
                KonfigurimComboGride.ShtoLlogariSipasNdermarrjesDhePerdoruesit(gvAmortizimi, idndermarje, idperdoruesi, Session, komponente, guidString, "IdLlogKunderparti");
            }

            else
                KonfigurimComboGride.ShtoLlogariSipasNdermarrjesDhePerdoruesit(gvAmortizimi, idndermarje, idperdoruesi, Session, komponente, guidString, "IdLlogKunderparti");

            GridViewDataTextColumn col3 = gvAmortizimi.Columns["AmortizimiShteseTotal"] as GridViewDataTextColumn;
            col3.PropertiesEdit.DisplayFormatString = "0.00";
            gvAmortizimi.Columns["#"].VisibleIndex = 0;
        }




        /// <summary>
        /// kur grida ben databound per ti shtuar kolonen e selektimit
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvAmortizimi_DataBound(object sender, EventArgs e)
        {
            if (gvAmortizimi.Columns["#"] == null)
            {
                GridViewCommandColumn check = new GridViewCommandColumn("#") { ShowSelectCheckbox = true, Width = System.Web.UI.WebControls.Unit.Percentage(2) };
                gvAmortizimi.Settings.ShowFilterRow = true;
                gvAmortizimi.Settings.ShowHeaderFilterButton = true;
                gvAmortizimi.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                gvAmortizimi.Settings.ShowFilterRowMenu = true;
                gvAmortizimi.Columns.Add(check);
                gvAmortizimi.Settings.ShowGroupPanel = true;
                gvAmortizimi.KeyFieldName = "IdAmortizimi";
                gvAmortizimi.SettingsBehavior.AllowSelectByRowClick = true;
                gvAmortizimi.SettingsBehavior.AllowFocusedRow = true;
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
            //filtra.mbushFiltraGridaSipasFiltraEmri(cmbFiltra.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvAmortizimi", DbCore.clsFunksione.GetKomponente(Page.Request), idndermarje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                filtra.IdPerdoruesi = idperdoruesi;
                mesazh = filtra.fshi();
                //mbushComboBoxFiltra();
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idndermarje, "gvAmortizimi", int.Parse(cmbKonfigurimi.Value.ToString()), DbCore.clsFunksione.GetKomponente(Page.Request));
                percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), ASPxMenu1, idviti, idperdoruesi, idndermarje);
                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                gvAmortizimi.FilterExpression = " [IdStatusDokumenti]=1";


            }
        }

        /// <summary>
        /// sherben per te ruajtur filtrin e zgjedhur
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
            int idgjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            //kap item qe ka template ne menune e kesaj faqeje
            MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvAmortizimi", DbCore.clsFunksione.GetKomponente(Page.Request), idndermarje, int.Parse(cmbKonfigurimi.Value.ToString()));
            DbCore.DbAdmin.clsFiltraGrida filtri = new DbCore.DbAdmin.clsFiltraGrida() { FiltraKodi = cmbFiltra.Text, FiltraShenime = cmbFiltra.Text, FiltraUniversal = false, GridaKokaId = koka.IdGridaKoka, FiltraVlera = gvAmortizimi.FilterExpression, IdPerdoruesi = idperdoruesi, IdNdermarje = idndermarje, IdStatusDok = 1 };
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("NrDok", gvAmortizimi);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvAmortizimi.GetSortedColumns();
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
            clsToolbarConfig.mbushComboBoxFiltra(idgjuha, idndermarje, "gvAmortizimi", int.Parse(cmbKonfigurimi.Value.ToString()), DbCore.clsFunksione.GetKomponente(Page.Request));
            percaktoTemplateMenu(idgjuha, ASPxMenu1, idviti, idperdoruesi, idndermarje);
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
              int idGjuha = (int)hfState["idGjuha"];
            CultureInfo cultinf = DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo(idGjuha);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            switch (e.Item.Name)
            {
                case "Riruaj":
                    Riruaj((string)hfState["guidString"], DbCore.clsFunksione.GetKomponente(Page.Request), cultinf, rm, (bool)hfState["eshteMeme"], idGjuha);
                    break;
            }
        }


        protected void Riruaj(string guidString, string komponente, CultureInfo ci, ResourceManager rm, bool eshteMeme, int idGjuha)
        {
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            List<string> TeRuajtur = new List<string>(), Teparuajtur = new List<string>();
            pergjigja.Text = "";
            string mesazhmevonshem = "";
            List<object> rreshtat = gvAmortizimi.GetSelectedFieldValues("IdAmortizimi");
            pergjigja.Text = "";
            string serverUrl = DbCore.clsFunksione.ktheServerUrl(Request);
            DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            int idPerdoruesi = (int)hfState["idPerdoruesi"];
            int idNdermarrje = (int)hfState["idNdermarrje"];
             int idNdermarrjeVit = (int)hfState["idNdermarrjeVit"];
            bool eshteOwn = (bool)hfState["OwnShop"];
            DataTable err = new DataTable();
            err.Columns.Add("Kodi");
            err.Columns.Add("Gabimi");
            err.Columns.Add("Rreshti");
            int nrreshta = 0;
            foreach (object id in rreshtat)
            {
                nrreshta++;
                DbCore.DbAsete.clsAmortizimiKoka clsKoka = new DbCore.DbAsete.clsAmortizimiKoka();
                 clsKoka.ktheAmortizimKokaSipasId(Convert.ToInt32(id));
                clsKonfigurimAmbjenti konfig = new clsKonfigurimAmbjenti(clsKoka.IdKonfigurimAmbjenti);
                if (clsKoka.IdStatusDokumenti == 2)
                    continue;

                if (DbCore.MbylljePeriudhe.PeriodClosing.IsPeriodClosed(clsKoka.DateDokumenti, MyConnectionsManager.GetSelectedConNameServer(), idndermarje, KategoriDokumenti.RivleresimeAmortizimi, clsKoka.IdKonfigurimAmbjenti))
                {
                    object[] arr = { konfig.KodKonfigAmbjente + " " + clsKoka.NrDok + " " + clsKoka.DateDokumenti.ToShortDateString(), DbCore.IMBUtils.Messages.MessagesResource.Messages["msgPeriodIsClosed"], nrreshta };
                    err.Rows.Add(arr);
                    continue;
                }

                DbCore.DbAsete.colAmortizimiTrupiAbstract col;


                   if (clsAlternativaKushti.getAlternativa(clsKoka.IdKonfigurimAmbjenti,"AR").Equals("Po"))
                    col = new DbCore.DbAsete.colAmortizimiTrupiRezerva();
                else col = new DbCore.DbAsete.colAmortizimiTrupi();
                   
                clsKokaMagazina kokam = new clsKokaMagazina();

                kokam.mbushKokaMagazinaSipasIDGjenerues(clsKoka.IdAmortizimi, 1, clsKoka.IdKonfigurimAmbjenti);
                col.merrAmortizimTrupiSipasIdKokaAmortizimi(clsKoka.IdAmortizimi);
                for (int i = 0; i < col.Count; i++)
                {
                    col[i].Artikull = new DbCore.DbInventari.clsArtikulli(col[i].IdArtikulli);
                }

                DbCore.DbAsete.colSerialetMagazine colseriale = new DbCore.DbAsete.colSerialetMagazine();
                //colseriale.merrSerialetMagazineSipasIDDokumenti(kokam.IdKokaMagazina, idNdermarrje, kokam.IdKonfigAmbjente);
                //foreach (DbCore.DbAsete.clsSerialetMagazine s in colseriale)
                //{
                //    s.IdStatusDokumenti = 1;
                //}

               
                DbCore.DbAdmin.clsPeriudhaKontabel periudha = new DbCore.DbAdmin.clsPeriudhaKontabel(clsKoka.DateDokumenti, idNdermarrje); ;

                bool lidhur = clsKoka.eshteILidhur();
               
               
               
                clsKokaFleteKontabel kokfk = new clsKokaFleteKontabel(clsKoka.IdAmortizimi, 90);
                DbCore.DbQendraKosto.clsKokaQendraKosto qend = new DbCore.DbQendraKosto.clsKokaQendraKosto();
                qend.KtheKokaQKSipasIDGjeneruesDheKonfig(kokfk.IdKokaFleteKontabel, kokfk.IdKonfigAmbjente);
                #region validime
                if (clsKoka.IdLlogKunderparti != 0)
                {
                    clsLlogari llog = new clsLlogari(clsKoka.IdLlogKunderparti);
                    if (llog.IdLlogari<1)
                    {
                        object[] arr = { konfig.KodKonfigAmbjente + " " + clsKoka.NrDok + " " + clsKoka.DateDokumenti.ToShortDateString(), rm.GetString("msgLlogNukEkziston", ci), nrreshta };
                        err.Rows.Add(arr);
                        continue;
                       
                    }
                    else if (!llog.Aktiv)
                    {
                        object[] arr = { konfig.KodKonfigAmbjente + " " + clsKoka.NrDok + " " + clsKoka.DateDokumenti.ToShortDateString(), rm.GetString("msgKjoLlogariNukEshteAktive", ci), nrreshta };
                        err.Rows.Add(arr);
                        continue;
                        
                    }
                  
                }

                if (clsKoka.IdNjesiAdministrative != 0)
                {
                    DbCore.DbRegjistrim.clsNjesiAdministrative mag = new clsNjesiAdministrative(clsKoka.IdNjesiAdministrative);
                    if (mag.IdNjesiAdministrative == -1)
                    {
                        object[] arr = { konfig.KodKonfigAmbjente + " " + clsKoka.NrDok + " " + clsKoka.DateDokumenti.ToShortDateString(), rm.GetString("msgKjoMagazineNukEkziston", ci), nrreshta };
                        err.Rows.Add(arr);
                        continue;
                       
                    }
                    else
                    {
                        
                        if (mag.Aktiv == false)
                        {
                            object[] arr = { konfig.KodKonfigAmbjente + " " + clsKoka.NrDok + " " + clsKoka.DateDokumenti.ToShortDateString(), rm.GetString("msgMagazinaNukEshteAktive", ci), nrreshta };
                            err.Rows.Add(arr);
                            continue;
                           
                        }
                    }
                }
               
                


                #endregion
                #region krijimi i kokes se re
                DbCore.DbAsete.clsAmortizimiKoka kokare = new DbCore.DbAsete.clsAmortizimiKoka();
                
                
                int meKontabilizim = clsAlternativaKushti.getAlternativa(clsKoka.IdKonfigurimAmbjenti, "GJK") != "Jo" ? 1 : 0;
                string kodmagazina = "";

                DbCore.DbShare.clsKonfigurimAmbjenti konfmag = new DbCore.DbShare.clsKonfigurimAmbjenti(konfig.IdKonfigurimi, idGjuha);
                if (kokam.IdMagazina != 0)
                {
                    clsNjesiAdministrative magazina = new clsNjesiAdministrative(kokam.IdMagazina);
                    kodmagazina = magazina.Kodi;
                }
                string shfaqmesazhapolupe = "Jo";

                List<double> amortizimiFillestar = new List<double>();
           DbCore.DbAsete.colAmortizimiFillestar colAmor= col.ktheColAmortizimFillestar(mySessionObjects.merrIdNdermarrjeSesioni(Session), clsKoka.IdLlojStandarti, clsKoka.DateDokumenti);
           for (int i = 0; i < colAmor.Count; i++)
                {
                    amortizimiFillestar.Add(colAmor[i].AmortizimiFillestar);
                }
                int idPeriudheZgjedhur = periudha.IdPeriudha;

                mesazh = kokare.krijoDokumentAmortizimiPerAmortizimFillestar(clsKoka.NrDok, clsKoka.IdNiveli, clsKoka.IdKonfigurimAmbjenti, clsKoka.DateDokumenti, clsKoka.DateAmortizimi, clsKoka.IdNjesiAdministrative, clsKoka.IdLlojStandarti, clsKoka.Shenime, clsKoka.IdNderViti, clsKoka.IdNdermarrje, idPerdoruesi, clsKoka.DateRegjistrimi, 1, clsKoka.IdLlogKunderparti, col, colAmor, amortizimiFillestar, clsKoka.IdKrijuesi, out mesazhmevonshem);


                if (!mesazh.Status)
                {
                    object[] arr = { konfig.KodKonfigAmbjente + " " + clsKoka.NrDok + " " + clsKoka.DateDokumenti.ToShortDateString(), mesazh.PershkrimMesazhi, nrreshta };
                    err.Rows.Add(arr);
                    continue;

                }
                kokare.IdAmortizimi = clsKoka.IdAmortizimi;
                #endregion
                DbCore.DbAsete.colAmortizimiKoka koka = new DbCore.DbAsete.colAmortizimiKoka();
                koka.Add(kokare);
               
                  mesazh =    koka[0].modifiko(meKontabilizim, lidhur, idPeriudheZgjedhur, 90, out shfaqmesazhapolupe, new  DbCore.DbAsete.colSerialetMagazine(), false,colAmor  , rm, ci);
                  
                if (!mesazh.Status)
                {
                    object[] arr = { konfig.KodKonfigAmbjente + " " + clsKoka.NrDok + " " + clsKoka.DateDokumenti.ToShortDateString(), mesazh.PershkrimMesazhi, nrreshta };
                    err.Rows.Add(arr);
                    continue;

                }
               
            }
            DbCore.mySessionObjects.ruajTabeleGabimeshImporti(Session, err);
            if (err.Rows.Count > 0)
            {
                clsKokaErrorImporti koka;
                if (Request.QueryString["lloj"] == "amortizim")
                    koka = new clsKokaErrorImporti(0, "Nga riruatja e amortizimit fillestar ", 90, idNdermarrje, idPerdoruesi);
                else koka = new clsKokaErrorImporti();
                //else
                //    koka = new clsKokaErrorImporti(0, "Nga riruatja e blerjeve", 2, idNdermarrje, idPerdoruesi);
                koka.ColTrupi.mbushErrorImportiNgaProgrami(err);
                mesazh = koka.ruajErrorImporti();
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "U riruajten " + (rreshtat.Count - err.Rows.Count) + " rreshta dhe deshtuan " + err.Rows.Count + " rreshta! ", pnlMesazhi);
                Container.Attributes["src"] = "RaportiShpejte.aspx?Sesioni=false&emriReal=gabimeImporti&printo=false&db=jo";
            }
            else
            {
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "U riruajten te gjitha rreshtat!", pnlMesazhi);

            }
            gvAmortizimi.Selection.UnselectAll();
            mbushGridNgaDB();
          //  konfiguroGride(idGjuha, rm,false, ci);

            if (rreshtat.Count == 0)
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("regjMagMesazhZgjidhniNje", ci), pnlMesazhi);
      
        }
        

        /// <summary>
        /// fshin rreshtat e selektuar
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            List<string> TeFshire = new List<string>(), TeLidhur = new List<string>(), PeriudheKycur = new List<string>(), closedPeriod = new List<string>();

            //clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "", pnlMesazhi);
            List<object> rreshtat = gvAmortizimi.GetSelectedFieldValues("IdAmortizimi");
            pergjigja.Text = "";
            //DbCore.DbAdmin.clsPeriudhaKontabel periudha;
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            foreach (object id in rreshtat)
            {
                DbCore.DbAsete.clsAmortizimiKoka clsKoka = new DbCore.DbAsete.clsAmortizimiKoka(Convert.ToInt32(id));

                bool ekycur = DbCore.DbAdmin.clsPeriudhaKontabel.eshteKycurPeriudheSipasDateDheNdermarrjes(clsKoka.DateDokumenti, idndermarje);
                if (ekycur)
                {
                    PeriudheKycur.Add(clsKoka.NrDok);
                    continue;
                }

                if (DbCore.MbylljePeriudhe.PeriodClosing.IsPeriodClosed(clsKoka.DateDokumenti, MyConnectionsManager.GetSelectedConNameServer(), clsKoka.IdNdermarrje, KategoriDokumenti.RivleresimeAmortizimi, clsKoka.IdKonfigurimAmbjenti))
                {
                    closedPeriod.Add(clsKoka.NrDok);
                    continue;
                }

                bool lidhur = clsKoka.eshteILidhur();
                if (lidhur)
                {
                    TeLidhur.Add(clsKoka.NrDok);
                    continue;
                }

                clsKoka.IdPerdoruesi = idperdoruesi;
                bool rivleresim = false;
                if (Request.QueryString["lloj"] == "rivleresim") rivleresim = true;
                mesazh = clsKoka.fshiTrans(idperdoruesi, 90, rivleresim, new DbCore.DbAsete.colAmortizimiTrupi());
                if (mesazh.Status)
                {
                    #region Heq llogarite nga grida

                    hiqNgaGrida(clsKoka.IdAmortizimi, rm, ci);
                    #endregion
                    TeFshire.Add(clsKoka.NrDok);

                }
            }

            string mesazhInfoGabimLidhur = "", mesazhInfoGabimPeridheKycur = "", mesazhInfoSukses = "", mesazhClosedPeriod ="";

            if (TeLidhur.Count == 1)
                mesazhInfoGabimLidhur = String.Format("{0}{1}{2}", rm.GetString("msgAmortizimiPrefixNjejes", ci), String.Join(";", TeLidhur), rm.GetString("msgListaLidhjaSuffixNjejesGabimi", ci));
            else
                if (TeLidhur.Count > 1)
                    mesazhInfoGabimLidhur = String.Format("{0}{1}{2}", rm.GetString("msgAmortizimiPrefixShumes", ci), String.Join(";", TeLidhur), rm.GetString("msgListaLidhjaSuffixShumesGabimi", ci));
            if (PeriudheKycur.Count == 1)
                mesazhInfoGabimPeridheKycur = String.Format("{0}{1}{2}", rm.GetString("msgAmortizimiPrefixNjejes", ci), String.Join(";", PeriudheKycur), rm.GetString("regjMagSuffixMesazhNjejesPeriudheKycurGabimi", ci));
            else
                if (PeriudheKycur.Count > 1)
                    mesazhInfoGabimPeridheKycur = String.Format("{0}{1}{2}", rm.GetString("msgAmortizimiPrefixShumes", ci), String.Join(";", PeriudheKycur), rm.GetString("regjMagSuffixMesazhShumesPeriudheKycurGabimi", ci));

            if (closedPeriod.Count == 1)
                mesazhClosedPeriod = String.Format("{0}{1}{2}", rm.GetString("regjisDokPrefixMesazhNjejes", ci), String.Join(", ", closedPeriod), rm.GetString("suffixMesazhClosedPeriodNjejes", ci));
            else if (closedPeriod.Count > 1)
                mesazhClosedPeriod = String.Format("{0}{1}{2}", rm.GetString("regjisDokPrefixMesazhNjejes", ci), String.Join(", ", closedPeriod), rm.GetString("suffixMesazhClosedPeriodShumes", ci));

            if (TeFshire.Count == 1)
                mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("msgAmortizimiPrefixNjejes", ci), String.Join(";", TeFshire), rm.GetString("suffixMesazhNjejesSuksesi", ci));
            else
                if (TeFshire.Count > 1)
                    mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("msgAmortizimiPrefixShumes", ci), String.Join(";", TeFshire), rm.GetString("suffixMesazhShumesSuksesi", ci));

            mesazhInfoGabimLidhur += mesazhInfoGabimPeridheKycur + mesazhClosedPeriod;
            if (mesazhInfoGabimLidhur != "" && mesazhInfoSukses != "")
            {
                mesazhInfoGabimLidhur += rm.GetString("msgLidhesMesazhi", ci) + mesazhInfoSukses;
                pergjigja.Text = mesazhInfoGabimLidhur;
                pergjigja.ClientVisible = false;
            }
            else
            {
                pergjigja.Text = mesazhInfoSukses;
                pergjigja.ClientVisible = false;
            }

            if (rreshtat.Count == 0)
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("regjMagMesazhZgjidhniNje", ci), pnlMesazhi);
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
        private void hiqNgaGrida(int idkoka, ResourceManager rm, CultureInfo ci)
        {
            if (gvAmortizimi.DataSource != null)
            {
                DataTable dt = (DataTable)gvAmortizimi.DataSource;
                DataRow[] drs = dt.Select("IdAmortizimi = " + idkoka);
                if (drs.Length > 1)
                    throw new MyException(rm.GetString("msgAmortizimiJane2DokMeTeNjejtenIDNeGride", ci));
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                dt.Rows.Remove(dr);
                gvAmortizimi.DataSource = dt;
                gvAmortizimi.DataBind();
                dt.Dispose();
            }
            else mbushGridNgaDB();
        }

        /// <summary>
        /// kur grida ben callback
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvAmortizimi_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            GridUtil.ToolTipButonaveMbiGride(gvAmortizimi, ci, rm);
            if (e.CallbackName == "COLUMNMOVE" && gvAmortizimi.AllColumns[int.Parse(e.Args[0])].Width.Value == 0)
                gvAmortizimi.AllColumns[int.Parse(e.Args[0])].Width = System.Web.UI.WebControls.Unit.Percentage(3);
            if (e.CallbackName == "APPLYFILTER" && e.Args[0] == "")
            {
                MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
                ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
                cmbFiltra.Text = "";
            }
        }

        /// <summary>
        /// kur perdoruesi i ben grides callbak per te mare konfigurimin e ri
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvAmortizimi_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string[] arr = e.Parameters.Split(';');
            string idkomponente = "1005";
            if (Request.QueryString["lloj"] == "rivleresim")
                idkomponente = "1006";
            GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), "gvAmortizimi", gvAmortizimi, cmbKonfigurimi.Text.Split(';')[0], idkomponente, DbCore.mySessionObjects.ktheGjuhe(Session));
            if (arr.Length == 3)
            {
                if (arr[2] == "")
                    gvAmortizimi.FilterExpression = " [IdStatusDokumenti]=1";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    //filtra.mbushFiltraGridaSipasFiltraKodi(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvAmortizimi", DbCore.clsFunksione.GetKomponente(Page.Request), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), int.Parse(cmbKonfigurimi.Value.ToString()));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        gvAmortizimi.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvAmortizimi);
                    }
                }
            }

            gvAmortizimi.Selection.UnselectAll();
        }

        /// <summary>
        /// perdoret per te vendosur karakteristika te griden ne javascript
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvAmortizimi_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gvAmortizimi.PageIndex;
            e.Properties["cpPageRow"] = gvAmortizimi.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gvAmortizimi.VisibleRowCount;
        }

        /// <summary>
        /// perdoret per te hequr filtrimin e grides kur zgjedhim elementin bosh
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvAmortizimi_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if (e.Column.FieldName == "IdNjesiAdministrative" || e.Column.FieldName == "IdKonfigurimAmbjenti" || e.Column.FieldName == "IdLlogKunderparti" || e.Column.FieldName == "IdLlojStandarti" )
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
        protected void gvAmortizimi_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            string TeGjithe = rm.GetString("GridHeaderFilterFillItemTeGjithe", ci);
            string nga = rm.GetString("GridHeaderFilterFillItemNga", ci);
            if (e.Column.FieldName == "NrDok" || e.Column.FieldName == "Shenime")
            {
                e.Values.Clear();
                e.AddValue(TeGjithe, string.Empty, "true");
                e.AddValue(nga + " A-D ", string.Empty, String.Format("{0}>'A     ' and {0} <'DDDDDDD'", e.Column.FieldName));
                e.AddValue(nga + " D-G ", string.Empty, String.Format("{0}>'D     ' and {0}<'GGGGGGG'", e.Column.FieldName));
                e.AddValue(nga + " H-K ", string.Empty, String.Format("{0}>'H     ' and {0}<'KKKKKKK'", e.Column.FieldName));
                e.AddValue(nga + " L-O ", string.Empty, String.Format("{0}>'L     ' and {0}  <'OOOOOOO'", e.Column.FieldName));
                e.AddValue(nga + " P-S ", string.Empty, String.Format("{0}>'P     ' and {0}<'SSSSSSS'", e.Column.FieldName));
                e.AddValue(nga + " T-W ", string.Empty, String.Format("{0}>'T     ' and {0}<'WWWWWWW'", e.Column.FieldName));
                e.AddValue(nga + " X-Z ", string.Empty, String.Format("{0}>'X     ' and {0}<'ZZZZZZZ'", e.Column.FieldName));
            }
            else
            {
                e.Values.Clear();
                e.AddValue(TeGjithe, string.Empty, "true");
            }
        }
    }
}