
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Data;
using DbCore.DbRegjistrim;
using System.Globalization;
using System.Resources;
using DbCore;
using DbCore.IMBUtils.Types;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class RegjistrimRezervimi : MyPageBase
    {
        private int idGjuha;

        DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();

        private DbCore.DbRegjistrim.colTrupiRezervime trupat = new DbCore.DbRegjistrim.colTrupiRezervime();
        private string komponente = "RegjistrimRezervimi.aspx";
        private string guidString;


        protected void Page_Load(object sender, EventArgs e)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                DbCore.clsFunksione.logout(Session,true,"FaqePaautorizuar");
            }
            int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerdoruesi);
            }
            oPerdorues = DbCore.mySessionObjects.kthePerdorues(Session);
            int idviti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            CultureInfo cultinf = DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo(idGjuha);
            percaktoTemplateMenu(idGjuha, ASPxMenu1, idviti, idPerdoruesi, idNdermarrje);
            if (!IsPostBack)
            {
                guidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                hfState.Set("guidString", guidString);
                mbushHiddenFieldMePerkthime(cultinf, rm);
                if (Request.QueryString["lloj"] == "hyrje")
                    ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 77, "LDRH", rm, cultinf, idGjuha);
                else ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 77, "LDRD", rm, cultinf, idGjuha);
                cmbKonfigurimi.SelectedIndex = 0;
                DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()), idGjuha);
                hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;
                mbushGridDokumentRezervimiNgaDB();
                if (Request.QueryString["lloj"] == "hyrje")
                    gvRegRZ.FilterExpression = "([Lloji] = 1) and [IdStatusDok]=1";
                else if (Request.QueryString["lloj"] == "dalje")
                    gvRegRZ.FilterExpression = "([Lloji] = 2) and [IdStatusDok]=1";
               
               
                hfLloji.Value = Request.QueryString["lloj"];
                if (Request.QueryString["fshi"] == "po")
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("regjMagFshirjaPerfundoiMeSukses", cultinf), pnlMesazhi);
                else if (Request.QueryString["ruaj"] == "po")
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("regjMagModifikimiPerfundoiMeSukses", cultinf), pnlMesazhi);
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idviti, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
                gvRegRZ.Columns["#"].VisibleIndex = 0;
                konfiguroGride(idGjuha, idNdermarrje, idPerdoruesi, cultinf, rm);
                if (Request.QueryString["lloj"] == "hyrje") GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "gvRegRZ", gvRegRZ, cmbKonfigurimi.Text.Split(';')[0], "537", idGjuha);
                else GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "gvRegRZ", gvRegRZ, cmbKonfigurimi.Text.Split(';')[0], "538", idGjuha);

            }
            else
            {
                guidString = (string)hfState["guidString"];
                mbushGridDokumentMagazineNgaSession();
                konfiguroGride(idGjuha, idNdermarrje, idPerdoruesi, cultinf, rm);
               
            }
            GridUtil.konfigGrideListeEMadhePaTheme(gvRegRZ, "IdKokaRezervime");
            clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idNdermarrje, "gvRegRZ", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
            if (Request.QueryString["indexrow"] != null)
                gvRegRZ.FocusedRowIndex = int.Parse(Request.QueryString["indexrow"]);
            GridUtil.ToolTipButonaveMbiGride(gvRegRZ, cultinf, rm);
            AspxWebControlUtils.perkthePopUp(popFshi, rm.GetString("labelKujdes", cultinf), lblMsgbox, rm.GetString("labelAdministrimiMsgJeniSigurt", cultinf), ButtonCancel, rm.GetString("labelAnullo", cultinf));
            AspxWebControlUtils.perkthePopUp(popKonvertim, rm.GetString("labelKujdes", cultinf), lblKonvertoNe, rm.GetString("lblKonvertoNe", cultinf), ButtonOk2, rm.GetString("btnKonverto", cultinf));
            konfigurimi_Label.Text = rm.GetString("lblModeli",cultinf);
        }

        /// <summary>
        /// Metode qe sherben per te kaluar perkthime te js
        /// </summary>
        /// <param name="cultinf">Merr culture info</param>
        /// <param name="rm">Merr Resource Manager</param>
        private void mbushHiddenFieldMePerkthime(CultureInfo cultinf, ResourceManager rm)
        {
            lblKonfig.Text = rm.GetString("filterLlojiArtBurim", cultinf);
            hfState.Set("regjisDokZgjidhniTePakten1DokPerKonvertim", rm.GetString("regjisDokZgjidhniTePakten1DokPerKonvertim", cultinf));
            hfState.Set("msgDokTeJeneTeSeNjejtesNenkategori", rm.GetString("msgDokTeJeneTeSeNjejtesNenkategori", cultinf));
            hfState.Set("msgDokTeKeneTeNjejtinKF", rm.GetString("msgDokTeKeneTeNjejtinKF", cultinf));
            hfState.Set("msgDokNukMundTeKonvertohet", rm.GetString("msgDokNukMundTeKonvertohet", cultinf));

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
            clsToolbarConfig.percaktoTemplateMenu(idGjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1, DbCore.clsFunksione.GetKomponente(Page.Request), this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, btnPo_Click, btnJo_Click, true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session), false);
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
            DbCore.clsMesazh mesazh = GridUtil.ruajFiltra(idndermarrje, idperdorues, idgjuha, "gvRegRZ", komponente, "FilterDefault", gvRegRZ.FilterExpression, gvRegRZ, "IdNivel", idkonf, out idfiltri);//ruan filtrin e zgjedhur tek filtrat
            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                return;

            }

            if (Request.QueryString["lloj"] == "hyrje")
                mesazh = GridUtil.ruajkonfigurimgride(gvRegRZ, cmbKonfigurimi.Text, idndermarrje, idperdorues, 537, idfiltri, idViti, DbCore.mySessionObjects.ktheCultureInfo(Session), DbCore.mySessionObjects.ktheGjuhe(Session));/// ruan konfigurimin e grides dhe filtrin
            else mesazh = GridUtil.ruajkonfigurimgride(gvRegRZ, cmbKonfigurimi.Text, idndermarrje, idperdorues, 538, idfiltri, idViti, DbCore.mySessionObjects.ktheCultureInfo(Session), DbCore.mySessionObjects.ktheGjuhe(Session));

            clsToolbarConfig.mbushComboBoxFiltra(idgjuha, idndermarrje, "grid_RegRip", int.Parse(cmbKonfigurimi.Value.ToString()), "RegjistrimRiparimi.aspx");

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
            percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));

        }
        private void mbushGridDokumentMagazineNgaSession()
        {
            DataTable tmpObject;
            bool sukses = DbCore.mySessionObjects.merrGrideNgaSessioni(Session, out tmpObject);
            if (!sukses)
                mbushGridDokumentRezervimiNgaDB();
            else
            {
                gvRegRZ.DataSource = tmpObject;
                gvRegRZ.DataBind();
                tmpObject.Dispose();
            }
        }
        private void mbushGridDokumentRezervimiNgaDB()
        {//mbush griden e popupit me te dhena            
            DataTable dt = DbCore.DbRegjistrim.colKokaRezervime.merrKokaRezervimiDT(DbCore.mySessionObjects.ktheNdermarrjeVit(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            DbCore.mySessionObjects.ruajGrideNeSession(Session, dt);
            gvRegRZ.DataSource = dt;
            gvRegRZ.DataBind();
            dt.Dispose();
        }

        private void konfiguroVleraFillestare()
        {
            DbCore.DbRegjistrim.colKokaRezervime colKoka = new DbCore.DbRegjistrim.colKokaRezervime(DbCore.mySessionObjects.ktheNdermarrjeVit(Session));
            gvRegRZ.DataSource = colKoka;
            gvRegRZ.DataBind();
        }

        private void konfiguroGride(int idGjuha, int idNdermarrje, int idPerdoruesi, CultureInfo ci, ResourceManager rm)
        {
            KonfigurimComboGride.ShtoNivel(gvRegRZ, 78, idNdermarrje, idPerdoruesi, idGjuha, Session, komponente, guidString);
            KonfigurimComboGride.ShtoModel(gvRegRZ, 78, idNdermarrje, idPerdoruesi, idGjuha, Session, komponente, guidString);
            KonfigurimComboGride.ShtoMagazinaNdermarrje(gvRegRZ, Session, komponente, guidString);
            KonfigurimComboGride.ShtoMagazinaSipasPerdoruesitDheNdermarrjes(gvRegRZ, idNdermarrje, idPerdoruesi, Session, komponente, guidString, "IdDegeAdministrative");
            KonfigurimComboGride.ShtoStatus(gvRegRZ, rm, ci);
            KonfigurimComboGride.ShtoStatusRezervimi(gvRegRZ, rm, ci);
            KonfigurimComboGride.ShtoLlojHyrjeDaje(gvRegRZ, rm, ci);

            this.gvRegRZ.Columns["#"].VisibleIndex = 0;
        }

     
     
        private void shtoKategori()
        {
            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            if (typeof(GridViewDataComboBoxColumn) != gvRegRZ.Columns["IdKategoria"].GetType())
            {
                gvRegRZ.Columns.Remove(gvRegRZ.Columns["IdKategoria"]);
                gvRegRZ.Columns.Add(colnew);
                DbCore.DbRegjistrim.colKategoriNiveleDok nivelet = new DbCore.DbRegjistrim.colKategoriNiveleDok();
                nivelet.mbushGjitheKategoriNivelDok();
                colnew.PropertiesComboBox.DataSource = nivelet;
                colnew.PropertiesComboBox.TextField = "Pershkrimi";
                colnew.PropertiesComboBox.ValueField = "IdKategori";
                colnew.FieldName = "IdKategoria";
                DbCore.mySessionObjects.ruajDsComboGrideNeSession(Session, nivelet, "kategoria");
            }
            else
            {
                colnew = (GridViewDataComboBoxColumn)gvRegRZ.Columns["IdKategoria"];
                if (colnew.PropertiesComboBox.Items.Count == 0)
                {
                    colnew.PropertiesComboBox.DataSource = DbCore.mySessionObjects.merrDsComboGrideNeSession(Session, "kategoria");
                }
            }
        }



        private void shtoKlient()
        {
            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            if (typeof(GridViewDataComboBoxColumn) != gvRegRZ.Columns["IdKlientFurnitor"].GetType())
            {
                gvRegRZ.Columns.Remove(gvRegRZ.Columns["IdKlientFurnitor"]);
                gvRegRZ.Columns.Add(colnew);
                DbCore.DbKontabiliteti.colKlienteFurnitore colKlientet = new DbCore.DbKontabiliteti.colKlienteFurnitore();
                colKlientet.mbushKlienteFurnitoreNdermarrjes(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                colnew.PropertiesComboBox.DataSource = colKlientet;
                colnew.PropertiesComboBox.TextField = "EmertimiKF";
                colnew.PropertiesComboBox.ValueField = "IdKlientFurnitor";
                colnew.FieldName = "IdKlientFurnitor";
                DbCore.mySessionObjects.ruajDsComboGrideNeSession(Session, colKlientet, "klientfurnitor");
            }
            else
            {
                colnew = (GridViewDataComboBoxColumn)gvRegRZ.Columns["IdKlientFurnitor"];
                if (colnew.PropertiesComboBox.Items.Count == 0)
                {
                    colnew.PropertiesComboBox.DataSource = DbCore.mySessionObjects.merrDsComboGrideNeSession(Session, "klientfurnitor");
                }
            }
        }

 
      
        protected void gvRegRZ_DataBound(object sender, EventArgs e)
        {
            if (this.gvRegRZ.Columns["#"] == null)
            {
                DevExpress.Web.GridViewCommandColumn check = new DevExpress.Web.GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true; check.Width = Unit.Percentage(2);
                // check.SetColVisibleIndex(0);
                gvRegRZ.Settings.ShowFilterRow = true;
                gvRegRZ.Settings.ShowHeaderFilterButton = true;
                gvRegRZ.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                gvRegRZ.Settings.ShowFilterRowMenu = true;
                gvRegRZ.Columns.Add(check);
                gvRegRZ.Settings.ShowGroupPanel = true;
                gvRegRZ.KeyFieldName = "IdKokaRezervime";
                gvRegRZ.SettingsBehavior.AllowSelectByRowClick = true;
                gvRegRZ.SettingsBehavior.AllowFocusedRow = true;
            } //this.gvRegRZ.Columns["#"].VisibleIndex = 0;
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
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvRegRZ", komponente, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            //DbCore.DbAdmin.clsFiltraGrida filtra = dbAdmin.merrFiltraGridaSipasFiltraEmri(cmbFiltra.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();
                //mbushComboBoxFiltra();
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvRegRZ", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
                percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);

                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                // konfiguroVleraFillestare();

                if (Request.QueryString["lloj"] == "hyrje")
                    gvRegRZ.FilterExpression = "([Lloji] = 1) and [IdStatusDok]=1";
                else if (Request.QueryString["lloj"] == "dalje")
                    gvRegRZ.FilterExpression = "([Lloji] = 2) and [IdStatusDok]=1";

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
            //DbCore.DbAdmin.clsGridaKoka koka = dbAdmin.merrGridaKokaByEmri("gvRegRZ", "RegjistrimMagazine.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvRegRZ", komponente, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gvRegRZ.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("IdNivel", gvRegRZ);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvRegRZ.GetSortedColumns();
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
            //    filtri.KoloneRenditje = "IdNivel";
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
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvRegRZ", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
            percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);

            if (mesazh.Status == true)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

            cmbFiltra.Text = "";

        }
        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (e.Item.Name == "PrintPreview")
            {
                if (gvRegRZ.FocusedRowIndex == -1)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("regjMagMesazhZgjidhniFaturePerPrintim", cultinf), pnlMesazhi);
                    Container.Attributes["src"] = "";
                }
                else
                {
                    string id = gvRegRZ.GetRowValues(gvRegRZ.FocusedRowIndex, "IdKokaRezervime").ToString();
                    DbCore.DbRegjistrim.clsKokaRezervime clsKoka = new DbCore.DbRegjistrim.clsKokaRezervime();
                    clsKoka.mbushKokaRezervimiSipasID(int.Parse(id));
                    DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                    konf.mbushKonfiguriminMeID(clsKoka.IdKonfigAmbjente);
                    //if (konf.KodKonfigAmbjente == "FH" || konf.KodKonfigAmbjente == "FD")
                    //{
                    Container.Attributes["src"] = "RaportiShpejte.aspx?Sesioni=false&emriReal=Rap_Format_Printimi_Magazina&idDokumenti=" + clsKoka.IdKokaRezervimi + "&printo=false";
                    //  }
                }

            }

        }

        //fshin rreshtat e selektuar
        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            //clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "", pnlMesazhi);

            pergjigja.Text = "";
            List<object> rreshtat = gvRegRZ.GetSelectedFieldValues("IdKokaRezervime");
            if (rreshtat.Count == 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("regjMagMesazhZgjidhniNje", cultinf), pnlMesazhi);
                return;
            }
            List<string> TeFshire = new List<string>(), TeLidhur = new List<string>(), PeriudheKycur = new List<string>(), GjendjeNegative = new List<string>();
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            DbCore.DbRegjistrim.colTrupiRezervime tr = new DbCore.DbRegjistrim.colTrupiRezervime();
            bool rivleresim = false;
           // DbCore.DbAdmin.clsPeriudhaKontabel periudha;
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            
            foreach (object id in rreshtat)
            {
                DbCore.DbRegjistrim.clsKokaRezervime kok = new DbCore.DbRegjistrim.clsKokaRezervime(Convert.ToInt32(id));
                //DbCore.DbShare.colKusht kushte = new DbCore.DbShare.colKusht();
                //kushte.mbushGjitheKushteKonfigurimi(kok.IdKonfigAmbjente);
                bool lidhur = kok.eshteILidhur();
                string ngjyra = clsKokaRezervime.merrNgjyreKonvertimeRezervime(kok.IdNdermarrje, kok.IdKokaRezervimi);
                if (ngjyra == "kuqe" || ngjyra == "gjelber" || ngjyra == "verdhe")
                    lidhur = true;
                bool autorizimet = DbCore.DbRegjistrim.clsKokaRezervime.kaAutorizime(kok.IdKokaRezervimi, mySessionObjects.ktheIdPerdoruesi(Session));
                if (!autorizimet)
                    lidhur = true;
                if (lidhur)
                {
                    TeLidhur.Add(kok.NrDok);
                    continue;
                }
                if (kok.IdStatusDok == 2)
                    continue;
                //periudha = new DbCore.DbAdmin.clsPeriudhaKontabel(kok.DtDok, idNdermarrje);
                ///DbCore.clsMesazh mesazhi = periudha.isPeriudheKycur();
             
                bool ekycur = DbCore.DbAdmin.clsPeriudhaKontabel.eshteKycurPeriudheSipasDateDheNdermarrjes(kok.DtDok, idNdermarrje);
                if (ekycur)
                {
                    PeriudheKycur.Add(kok.NrDok);
                    continue;
                }
                kok.mbushTrupRezervime();
                //if (!kok.kontrolloGjendjeNeFshirje(new DbCore.DbRegjistrim.colTrupiRezervime(), 0).StatusMesazhi)
                //{
                //    GjendjeNegative.Add(kok.NrDok);
                //    continue;
                //}                

                kok.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                mesazh = kok.fshi();


                DbCore.mySessionObjects.ruajTrupatRezNeSession(Session, trupat);
                if (mesazh.Status)
                {
                    #region Heq llogarite nga grida

                    hiqDokumentRezervimiNgaGrida(kok.IdKokaRezervimi, rm, cultinf);
                    #endregion
                    TeFshire.Add(kok.NrDok);

                }
            }
            string mesazhInfoGabimLidhur = "", mesazhInfoGabimPeridheKycur = "", mesazhInfoSukses = "", mesazhGjendjeNegative = "";
            if (TeLidhur.Count == 1)
                //mesazhInfoGabimLidhur = String.Format("{0}{1}{2}", prefixMesazhNjejes, String.Join(";", TeLidhur), suffixMesazhNjejesLidhurGabimi);
                mesazhInfoGabimLidhur = String.Format("{0}{1}", rm.GetString("msgDokumenti", cultinf), rm.GetString("msgListaLidhjaSuffixNjejesGabimi", cultinf));
            else
                if (TeLidhur.Count > 1)
                    //mesazhInfoGabimLidhur = String.Format("{0}{1}{2}", prefixMesazhShumes, String.Join(";", TeLidhur), suffixMesazhShumesLidhurGabimi);
                    mesazhInfoGabimLidhur = String.Format("{0}{1}", rm.GetString("regjMagPrefixMesazhShumes", cultinf), rm.GetString("regjMagSuffixMesazhShumesLidhurGabimi", cultinf));
            if (PeriudheKycur.Count == 1)
                mesazhInfoGabimPeridheKycur = String.Format("{0}{1}{2}", rm.GetString("msgDokumenti", cultinf), String.Join(";", PeriudheKycur), rm.GetString("regjMagSuffixMesazhNjejesPeriudheKycurGabimi", cultinf));
            else
                if (PeriudheKycur.Count > 1)
                    mesazhInfoGabimPeridheKycur = String.Format("{0}{1}{2}", rm.GetString("regjMagPrefixMesazhShumes", cultinf), String.Join(";", PeriudheKycur), rm.GetString("regjMagSuffixMesazhShumesPeriudheKycurGabimi", cultinf));
            if (GjendjeNegative.Count == 1)
                mesazhGjendjeNegative = String.Format("{0}{1}{2}", rm.GetString("msgDokumenti", cultinf), String.Join(";", GjendjeNegative), rm.GetString("regjMagSuffixMesazhNjejesGjendjeNegative", cultinf));
            else
                if (GjendjeNegative.Count > 1)
                    mesazhGjendjeNegative = String.Format("{0}{1}{2}", rm.GetString("regjMagPrefixMesazhShumes", cultinf), String.Join(";", GjendjeNegative), rm.GetString("regjMagSuffixMesazhShumesGjendjeNegative", cultinf));
            if (TeFshire.Count == 1)
                mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("msgDokumenti", cultinf), String.Join(";", TeFshire), rm.GetString("suffixMesazhNjejesSuksesi", cultinf));
            else
                if (TeFshire.Count > 1)
                    mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("regjMagPrefixMesazhShumes", cultinf), String.Join(";", TeFshire), rm.GetString("suffixMesazhShumesSuksesi", cultinf));

            mesazhInfoGabimLidhur += mesazhInfoGabimPeridheKycur + mesazhGjendjeNegative;
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
            if (rivleresim)
                clsMenuInfo.ShtoPyetje(MenuInfo, rm.GetString("regjMagVeprimiSjellNdryshimNeCmimDalje", cultinf), pnlMesazhi,idGjuha);
            else
                if (mesazhInfoGabimLidhur != "")
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhInfoGabimLidhur, pnlMesazhi);
                else
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazhInfoSukses, pnlMesazhi);

        }
        private void hiqDokumentRezervimiNgaGrida(int idkoka, ResourceManager rm, CultureInfo cultinf)
        {
            if (this.gvRegRZ.DataSource != null)
            {
                DataTable dt = (DataTable)gvRegRZ.DataSource;
                DataRow[] drs = dt.Select("IdKokaRezervime = " + idkoka);
                if (drs.Length > 1)
                    throw new Exception(rm.GetString("regjMagMesazhGabimiNdodhen2DokMagazine", cultinf));
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                dt.Rows.Remove(dr);
                gvRegRZ.DataSource = dt;
                gvRegRZ.DataBind();
                dt.Dispose();
            }
            else mbushGridDokumentRezervimiNgaDB();
        }

        protected void gvRegRZ_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            if (e.CallbackName == "COLUMNMOVE" && gvRegRZ.AllColumns[int.Parse(e.Args[0])].Width.Value == 0)
                gvRegRZ.AllColumns[int.Parse(e.Args[0])].Width = Unit.Percentage(3);
            if (e.CallbackName == "APPLYFILTER" && e.Args[0] == "")
            {
                DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
                ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
                cmbFiltra.Text = "";
            }
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            GridUtil.ToolTipButonaveMbiGride(gvRegRZ, cultinf, rm);
        }

        public void btnJo_Click(object sender, EventArgs e)
        {
            if (pergjigja.Text.Contains("lidhur") || pergjigja.Text.Contains("kycur"))
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, pergjigja.Text, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, pergjigja.Text, pnlMesazhi);
            DbCore.mySessionObjects.ruajTrupatRezNeSession(Session, new DbCore.DbRegjistrim.colTrupiRezervime());
        }

        public void btnPo_Click(object sender, EventArgs e)
        {
            //trupat = DbCore.mySessionObjects.merrTrupatRezNgaSesioni(Session);
            //DbCore.clsMesazh mesazh = new DbCore.clsMesazh(true, "Rivleresimi perfundoi me sukses!");            
            //foreach (DbCore.DbRegjistrim.clsTrupiRezervime t in trupat)
            //{
            //    DbCore.DbInventari.clsArtikulli art = new DbCore.DbInventari.clsArtikulli(t.IdArtikulli);                
            //    mesazh = DbCore.DbInventari.clsArtikulli.rivleresimCmimiMesatar(t.IdArtikulli, art.MetodeKostojeArtikulli, t.IdMag, t.Data, DateTime.Today); 
            //    if (!mesazh.StatusMesazhi)
            //    {
            //        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Rivlerësimi përfundoi me gabime!", pnlMesazhi);
            //        return;
            //    }
            //}            
            //mySessionObjects.ruajTrupatRezNeSession(Session, new DbCore.DbRegjistrim.colTrupiRezervime());
            //if (mesazh.StatusMesazhi)
            //    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "Rivlerësimi përfundoi me sukses!", pnlMesazhi);
        }

        protected void gvRegRZ_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gvRegRZ.PageIndex;
            e.Properties["cpPageRow"] = gvRegRZ.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gvRegRZ.VisibleRowCount;
        }

        protected void gvRegRZ_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string[] arr = e.Parameters.Split(';');
            int idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "gvRegRZ", gvRegRZ, cmbKonfigurimi.Text.Split(';')[0], "513", idGjuha);
            if (arr.Length == 3)
            {
                if (arr[2] == "")
                {
                    if (Request.QueryString["lloj"] == "hyrje")
                        gvRegRZ.FilterExpression = "([Lloji] = 1) and [IdStatusDok]=1";
                    else if (Request.QueryString["lloj"] == "dalje")
                        gvRegRZ.FilterExpression = "([Lloji] = 2) and [IdStatusDok]=1";
                }
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuha, "gvRegRZ", komponente, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], idNdermarrje, koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        gvRegRZ.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvRegRZ);
                    }
                }
            }
            gvRegRZ.Selection.UnselectAll();
        }

        protected void gvRegRZ_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if (e.Column.FieldName == "IdMagazina" || e.Column.FieldName == "IdKonfigAmbjente" || e.Column.FieldName == "IdDegeAdministrative")
                if (Converter.ConvertToInt(e.Value)==0)
                {
                    e.Criteria = null;
                }
        }

        protected void gvRegRZ_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
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