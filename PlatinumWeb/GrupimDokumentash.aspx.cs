using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using PlatinumWeb.Templates;
using System.Globalization;
using System.Resources;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{
    public partial class GrupimDokumentash : MyPageBase
    {
        private const string mesazhPlotesoniKodin = "Ju lutemi plotesoni kodin!";
        private const string mesazhPlotesoniPershkrimin = "Ju lutemi plotesoni pershkrimin!";
        private const string mesazhKodiEkziston = "Ekziston nje grup me kete kod. Ju lutem zgjidhni nje kod tjeter!";
        private const string mesazhGrupiVeprimFshirje = "Ky grup ka veprime dhe nuk mund te fshihet!";
        private const string mesazhGabimGjateRuajtjes = "Ndodhi nje gabim gjate ruajtjes!";
        private const string STR_JuLutemZgjidhniTePaktenNjeKategori = "Ju lutem zgjidhni te pakten nje kategori!";
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);

            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                Response.Redirect(DbCore.IMBUtils.Paths.defaultLoginPath);
                return;
            }
            int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerdoruesi);
                return;
            }
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);

            //mbushComboBoxFiltra();
           
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvGrupet", 1, "GrupimDokumentash.aspx");
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (!IsPostBack)
            {
                EmrateTabeve();
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, ":");
                if (Request.QueryString["ruaj"] == "ok")
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("labelRaportMesazhRuajtjaPerfundoiSukses"), pnlMesazhi);
                konfiguroVleraFillestare();
                konfiguroGridat();
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "GrupimDokumentash.aspx");
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                mbushHiddenFieldMePerkthime(ci, rm);
            }
            DbCore.mySessionObjects.ruajMesazhNeSesion(Session, ":");
            konfiguroVleraFillestare();
            AspxWebControlUtils.perkthePopUp(popFshi, rm.GetString("labelKujdes", cultinf), lblMsgbox, rm.GetString("labelAdministrimiMsgJeniSigurt", cultinf), ButtonCancel, rm.GetString("labelAnullo", cultinf));
            konfiguroGridat();
            hfTeDrejta.Set("msgNukKeniDrejtaPerVeprim", rm.GetString("msgNukKeniDrejtaPerVeprim", cultinf));
        }

        /// <summary>
        /// Vendos emrat e tabeve ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateTabeve()
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            ASPxPageControl1.TabPages[0].Text = rm.GetString("labelGrupimKlientPare", cultinf);
            ASPxPageControl1.TabPages[1].Text = rm.GetString("labelGrupimKlientDyte", cultinf);
            ASPxPageControl1.TabPages[2].Text = rm.GetString("filterGrupimi3", cultinf);
        }
        /// <summary>
        /// Metode qe sherben per te kaluar perkthime te js
        /// </summary>
        /// <param name="cultinf">Merr culture info</param>
        /// <param name="rm">Merr Resource Manager</param>
        private void mbushHiddenFieldMePerkthime(CultureInfo cultinf, ResourceManager rm)
        {
            clsFunksione.ShtoPerkthimNeHfState(hfState,
            "regjisDokNukKeniAsnjeDokTeZgjedhur"
            );
        }

        private void konfiguroVleraFillestare()
        {
            konfigVleraFillestarePerGride(gvGrupet);
            konfigVleraFillestarePerGride(gvGrupet2);
            konfigVleraFillestarePerGride(gvGrupet3);
        }

        private void konfigVleraFillestarePerGride(ASPxGridView grida)
        {
            int grupi;
            if (grida.ClientInstanceName == "gvGrupet")
                grupi = 1;
            else if (grida.ClientInstanceName == "gvGrupet2")
                grupi = 2;
            else grupi = 3;
            DbCore.DbRegjistrim.colGrupimDokumentiKoka col = new DbCore.DbRegjistrim.colGrupimDokumentiKoka();
            col.merrGrupeSipasGrupit(grupi, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            grida.DataSource = col;
            grida.DataBind();
        }

        private void konfiguroGridat()
        {
            konfiguroGride(gvGrupet);
            konfiguroGride(gvGrupet2);
            konfiguroGride(gvGrupet3);
        }

        private void konfiguroGride(ASPxGridView grida)
        {
            percaktoTamplatePrindi(grida, 0);
            GridUtil.percaktoVisibleColumnsMeWidth(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), grida, "gvGrupet", "GrupimDokumentash.aspx");
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            GridUtil.konfiguroGrideListeEvogelPaTheme(grida, "IdGrupimKoka");
        }

        private void shtoKategori(ASPxGridView grid)
        {
            grid.Columns.Remove(grid.Columns["Kategoria"]);
            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            colnew.UnboundType = DevExpress.Data.UnboundColumnType.String;
            DbCore.DbRegjistrim.colKategoriNiveleDok col = new DbCore.DbRegjistrim.colKategoriNiveleDok();
            DbCore.DbRegjistrim.clsKategoriNivelDok kat = new DbCore.DbRegjistrim.clsKategoriNivelDok("Shitje");
            col.Add(kat);
            kat = new DbCore.DbRegjistrim.clsKategoriNivelDok(2);
            col.Add(kat);
            kat = new DbCore.DbRegjistrim.clsKategoriNivelDok(3);
            col.Add(kat);
            kat = new DbCore.DbRegjistrim.clsKategoriNivelDok(4);
            col.Add(kat);
            kat = new DbCore.DbRegjistrim.clsKategoriNivelDok(6);
            col.Add(kat);
            kat = new DbCore.DbRegjistrim.clsKategoriNivelDok(44);
            col.Add(kat);
            kat = new DbCore.DbRegjistrim.clsKategoriNivelDok(45);
            col.Add(kat);
            kat = new DbCore.DbRegjistrim.clsKategoriNivelDok(46);
            col.Add(kat);

            colnew.PropertiesComboBox.DataSource = col;
            colnew.PropertiesComboBox.TextField = "Pershkrimi";
            colnew.PropertiesComboBox.ValueField = "Pershkrimi";
            colnew.FieldName = "Kategoria";
            colnew.Caption = "Kategoria";
            grid.Columns.Add(colnew);
        }
        private void shtoLloje(ASPxGridView grid)
        {
            grid.Columns.Remove(grid.Columns["Lloji"]);
            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            colnew.UnboundType = DevExpress.Data.UnboundColumnType.String;
            DbCore.DbShare.colKonfigurimAmbjenti col = new DbCore.DbShare.colKonfigurimAmbjenti();
            int idndermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idperdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            col.mbushKonfigAmbjSipasIdKategoriJoVartese(1, idndermarje, idperdoruesi);
            col.mbushKonfigAmbjSipasIdKategoriJoVartese(2, idndermarje, idperdoruesi);
            col.mbushKonfigAmbjSipasIdKategoriJoVartese(3, idndermarje, idperdoruesi);
            col.mbushKonfigAmbjSipasIdKategoriJoVartese(4, idndermarje, idperdoruesi);
            col.mbushKonfigAmbjSipasIdKategoriJoVartese(6, idndermarje, idperdoruesi);
            col.mbushKonfigAmbjSipasIdKategoriJoVartese(44, idndermarje, idperdoruesi);
            col.mbushKonfigAmbjSipasIdKategoriJoVartese(45, idndermarje, idperdoruesi);
            col.mbushKonfigAmbjSipasIdKategoriJoVartese(46, idndermarje, idperdoruesi);

            colnew.PropertiesComboBox.DataSource = col;
            colnew.PropertiesComboBox.TextField = "KodKonfigAmbjente";
            colnew.PropertiesComboBox.ValueField = "KodKonfigAmbjente";
            colnew.FieldName = "Lloji";
            colnew.Caption = "Lloji";
            grid.Columns.Add(colnew);
        }
        private void percaktoTamplatePrindi(ASPxGridView grida, int llojikf)
        {
            GridViewDataColumn col = grida.Columns["Kategoria"] as GridViewDataColumn;
            col.EditItemTemplate = new MyListboxTemplate();
            GridViewDataColumn col2 = grida.Columns["Lloji"] as GridViewDataColumn;
            col2.EditItemTemplate = new MyListboxTemplate();
            GridViewDataColumn colAutorizimet = grida.Columns["Autorizimet"] as GridViewDataColumn;
            colAutorizimet.EditItemTemplate = new MyComboTemplate();
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
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, "GrupimDokumentash.aspx", this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, hfRuaj.Value == "Ruaj" ? true : false, true, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
        }

        //private void mbushComboBoxFiltra()
        //{          
        //    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka("gvGrupet", "GrupimDokumentash.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //    DbCore.DbAdmin.colFiltratGrida colFiltra = new DbCore.DbAdmin.colFiltratGrida(koka.IdGridaKoka, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //    colFiltra.Insert(0, new DbCore.DbAdmin.clsFiltraGrida());
        //    MenuFilter.colekstioni = colFiltra;
        //    MenuFilter.ValueField = "IdFiltra";
        //    MenuFilter.TextField = "FiltraShenime";
        //}

        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        
        }

        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            int index = ASPxPageControl1.ActiveTabIndex;
            ASPxGridView grida;
            if (index == 0) grida = gvGrupet;
            else if (index == 1) grida = gvGrupet2;
            else grida = gvGrupet3;
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int a = grida.FocusedRowIndex;
            grida.Selection.SelectRow(a);
            List<object> rreshtat = grida.GetSelectedFieldValues("IdGrupimKoka");
            int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            foreach (int id in rreshtat)
            {
                DbCore.DbRegjistrim.clsGrupimDokumentiKoka koka = new DbCore.DbRegjistrim.clsGrupimDokumentiKoka(id);

                if (DbCore.DbRegjistrim.clsGrupimDokumentiKoka.kaVeprimeGrup(id))
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhGrupiVeprimFshirje, pnlMesazhi);
                else
                {
                    koka.IdPerdoruesi = idPerdoruesi;
                    DbCore.clsMesazh mes = koka.fshi();
                    if (mes.Status)
                    {
                        clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["msgFshirjeMeSukses"], pnlMesazhi);
                        ASPxMenu1.Items.FindByText("Ruaj").ClientVisible = false;
                    }
                    else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mes.PershkrimMesazhi, pnlMesazhi);
                }
                konfigVleraFillestarePerGride(grida);
            }
            pnlGrida.Update();
        }

        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje
            int index = ASPxPageControl1.ActiveTabIndex;
            ASPxGridView grida;
            if (index == 0) grida = gvGrupet;
            else if (index == 1) grida = gvGrupet2;
            else grida = gvGrupet3;
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;

            DbCore.DbAdmin.clsFiltraGrida filtri = new DbCore.DbAdmin.clsFiltraGrida();
            filtri.FiltraKodi = cmbFiltra.Text;
            filtri.FiltraShenime = cmbFiltra.Text;
            filtri.FiltraUniversal = false;

            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvGrupet", "GrupimDokumentash.aspx", idNdermarrje);
            filtri.GridaKokaId = koka.IdGridaKoka;


            filtri.FiltraVlera = grida.FilterExpression;
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


            filtri.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            filtri.IdNdermarje = idNdermarrje;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();

            filtri.IdStatusDok = 1;
            mesazh = filtri.ruaj();
            //mbushComboBoxFiltra();
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvGrupet", 1, "GrupimDokumentash.aspx");
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);

            if (mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                ASPxMenu1.Items.FindByText("Ruaj").ClientVisible = false;
            }
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            cmbFiltra.Text = "";
        }

        public void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje
            int index = ASPxPageControl1.ActiveTabIndex;
            ASPxGridView grida;
            if (index == 0) grida = gvGrupet;
            else if (index == 1) grida = gvGrupet2;
            else grida = gvGrupet3;
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;

            DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
            //filtra.mbushFiltraGridaSipasFiltraEmri(cmbFiltra.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvGrupet", "GrupimDokumentash.aspx", idNdermarrje);
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                mesazh = filtra.fshi();
                //mbushComboBoxFiltra();
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvGrupet", 1, "GrupimDokumentash.aspx");
                percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session),idNdermarrje );

                if (mesazh.Status)
                {
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                    ASPxMenu1.Items.FindByText("Ruaj").ClientVisible = false;
                }
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                grida.FilterExpression = String.Empty;
            }
        }

        protected void rowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            //merr te dhenat e rreshtit te ri te grides
            ASPxGridView gride = sender as ASPxGridView;

            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DbCore.DbRegjistrim.clsGrupimDokumentiKoka koka = new DbCore.DbRegjistrim.clsGrupimDokumentiKoka();

            koka.Kodi = e.NewValues["Kodi"].ToString();
            koka.Pershkrimi = e.NewValues["Pershkrimi"].ToString();
            if (gride.ClientInstanceName == "gvGrupet") koka.Grupi = 1;
            else if (gride.ClientInstanceName == "gvGrupet2") koka.Grupi = 2;
            else if (gride.ClientInstanceName == "gvGrupet3") koka.Grupi = 3;
            koka.IdNdermarje = idNdermarrje;
            koka.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            koka.IdStatusDok = 1;
            koka.ColTrupi = new DbCore.DbRegjistrim.colGrupimDokumentiTrupi();
            GridViewDataComboBoxColumn colAutorizimet = gride.Columns["Autorizimet"] as GridViewDataComboBoxColumn;
            ASPxComboBox cmbAutorizimi = gride.FindEditRowCellTemplateControl(colAutorizimet, "cmbBox") as ASPxComboBox;
            koka.Autorizimet = cmbAutorizimi.Text;
            GridViewDataColumn col2 = gride.Columns["Lloji"] as GridViewDataColumn;
            ASPxListBox lstBox2 = ((ASPxGridView)sender).FindEditRowCellTemplateControl(col2, "lbx") as ASPxListBox;
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "GrupimDokumentash.aspx");
            if (!tedrejtaInfo.DShtim)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, MessagesResource.Messages["msgNukKeniTeDrejtaRed"]);
                return;
            }
            DevExpress.Web.SelectedValueCollection b = lstBox2.SelectedValues;
            if (b.Count == 0)
                lstBox2.SelectAll();
            b = lstBox2.SelectedValues;
            for (int i = 0; i < b.Count; i++)
            {
                DbCore.DbRegjistrim.clsGrupimDokumentiTrupi t = new DbCore.DbRegjistrim.clsGrupimDokumentiTrupi();
                t.IdKonfig = Convert.ToInt32(b[i]);
                koka.ColTrupi.Add(t);
            }
            e.Cancel = true;
            gride.CancelEdit();
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            if (DbCore.DbRegjistrim.clsGrupimDokumentiKoka.ekzistonGrup(koka.Kodi, idNdermarrje, koka.Grupi))
            {
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, mesazhKodiEkziston + ":Red");
                konfiguroGride(gride);
                gride.AddNewRow();
                return;
            }
            mesazh = koka.ruaj();
            if (!mesazh.Status == true)
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, mesazhGabimGjateRuajtjes + ":Red");
            else
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, MessagesResource.Messages["mesazhRuajtjeMeSukses"] + ":Green");

            konfigVleraFillestarePerGride(gride);

            konfiguroGride(gride);
            pnlGrida.Update();

        }

        protected void rowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            ASPxGridView gride = sender as ASPxGridView;
            String id = e.Keys["IdGrupimKoka"].ToString();
            DbCore.clsMesazh m = new DbCore.clsMesazh();
            string kodi = e.NewValues["Kodi"].ToString();
            string pershkrimi = e.NewValues["Pershkrimi"].ToString();
            int grupi;
            if (gride.ClientInstanceName == "gvGrupet") grupi = 1;
            else if (gride.ClientInstanceName == "gvGrupet2") grupi = 2;
            else grupi = 3;
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "GrupimDokumentash.aspx");
            if (!tedrejtaInfo.DMod)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, MessagesResource.Messages["msgNukKeniTeDrejtaRed"]);
                return;
            }
            GridViewDataComboBoxColumn colAutorizimet = gride.Columns["Autorizimet"] as GridViewDataComboBoxColumn;
            ASPxComboBox cmbAutorizimi = gride.FindEditRowCellTemplateControl(colAutorizimet, "cmbBox") as ASPxComboBox;
            DbCore.DbRegjistrim.clsGrupimDokumentiKoka grupikoka = new DbCore.DbRegjistrim.clsGrupimDokumentiKoka(int.Parse(id), kodi, pershkrimi, grupi, idPerdoruesi, idNdermarrje, 1, cmbAutorizimi.Text);
            grupikoka.ColTrupi = new DbCore.DbRegjistrim.colGrupimDokumentiTrupi();
            GridViewDataColumn col2 = gride.Columns["Lloji"] as GridViewDataColumn;
            ASPxListBox lstBox2 = ((ASPxGridView)sender).FindEditRowCellTemplateControl(col2, "lbx") as ASPxListBox;

            DevExpress.Web.SelectedValueCollection b = lstBox2.SelectedValues;
            if (b.Count == 0)
                lstBox2.SelectAll();
            b = lstBox2.SelectedValues;
            for (int i = 0; i < b.Count; i++)
            {
                DbCore.DbRegjistrim.clsGrupimDokumentiTrupi t = new DbCore.DbRegjistrim.clsGrupimDokumentiTrupi();
                t.IdKonfig = Convert.ToInt32(b[i]);
                grupikoka.ColTrupi.Add(t);
            }
            m = grupikoka.modifiko();

            if (m.Status)
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, m.PershkrimMesazhi + ":Green");

            else
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, m.PershkrimMesazhi + ":Red");
            e.Cancel = true;
            gride.CancelEdit();
            konfigVleraFillestarePerGride(gride);
            konfiguroGridat();

        }


        protected void rowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {//per tu rregulluar me vone
            //validimi ne jane plotesuar gjithe fushat e detyruara

            ASPxGridView grida = sender as ASPxGridView;
            if (e.NewValues["Kodi"] == null || e.NewValues["Kodi"].ToString() == "")
            {
                e.RowError = mesazhPlotesoniKodin;
                return;
            }
            if (e.NewValues["Pershkrimi"] == null || e.NewValues["Pershkrimi"].ToString() == "")
            {
                e.RowError = mesazhPlotesoniPershkrimin;
                return;
            }
            GridViewDataColumn col1 = grida.Columns["Kategoria"] as GridViewDataColumn;
            ASPxListBox lstBox = ((ASPxGridView)sender).FindEditRowCellTemplateControl(col1, "lbx") as ASPxListBox;
            DevExpress.Web.SelectedValueCollection kategoriteSel = lstBox.SelectedValues;
            if (kategoriteSel.Count == 0)
            {
                e.RowError = STR_JuLutemZgjidhniTePaktenNjeKategori; return;
            }
            int llojKod = 0;
            if (grida.ClientInstanceName == "gvGrupet") llojKod = 1;
            else if (grida.ClientInstanceName == "gvGrupet2") llojKod = 2;
            else if (grida.ClientInstanceName == "gvGrupet3") llojKod = 3;
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);

            if (e.Keys["IdGrupimKoka"] == null) //nqs eshte shtim
                if (DbCore.DbRegjistrim.clsGrupimDokumentiKoka.ekzistonGrup(e.NewValues["Kodi"].ToString(), idNdermarrje, llojKod))
                {
                    e.RowError = mesazhKodiEkziston;
                    return;
                }
        }

        protected void startRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
        }

        protected void gridat_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            int index = ASPxPageControl1.ActiveTabIndex;
            //ASPxGridView grida;
            //if (index == 0) grida = gvGrupet;
            //else if (index == 1) grida = gvGrupet2;
            //else grida = gvGrupet3;
            //konfiguroGride(grida);

        }
        protected void HtmlRowCreated(object sender, ASPxGridViewEditFormEventArgs e)
        {
            ASPxGridView grida = sender as ASPxGridView;
            GridViewDataColumn col1 = grida.Columns["Kategoria"] as GridViewDataColumn;
            ASPxListBox lstBox = ((ASPxGridView)sender).FindEditRowCellTemplateControl(col1, "lbx") as ASPxListBox;
            GridViewDataColumn col2 = grida.Columns["Lloji"] as GridViewDataColumn;
            ASPxListBox lstBox2 = ((ASPxGridView)sender).FindEditRowCellTemplateControl(col2, "lbx") as ASPxListBox;
            if (lstBox != null)
            {
                DbCore.DbRegjistrim.colKategoriNiveleDok col = new DbCore.DbRegjistrim.colKategoriNiveleDok();
                DbCore.DbRegjistrim.clsKategoriNivelDok kat = new DbCore.DbRegjistrim.clsKategoriNivelDok("Shitje");
                col.Add(kat);
                kat = new DbCore.DbRegjistrim.clsKategoriNivelDok(2);
                col.Add(kat);
                kat = new DbCore.DbRegjistrim.clsKategoriNivelDok(3);
                col.Add(kat);
                kat = new DbCore.DbRegjistrim.clsKategoriNivelDok(4);
                col.Add(kat);
                kat = new DbCore.DbRegjistrim.clsKategoriNivelDok(6);
                col.Add(kat);
                kat = new DbCore.DbRegjistrim.clsKategoriNivelDok(44);
                col.Add(kat);
                kat = new DbCore.DbRegjistrim.clsKategoriNivelDok(45);
                col.Add(kat);
                kat = new DbCore.DbRegjistrim.clsKategoriNivelDok(46);
                col.Add(kat);
                lstBox.ClientSideEvents.SelectedIndexChanged = "function (s,e){merrLloje(s,e)}";
                lstBox.Height = 240 ;
                lstBox.ClientSideEvents.Init = "function (s,e){InitKat(s,e)}";
                lstBox.ClientInstanceName = "lbxKategoria";
                lstBox.DataSource = col;
                lstBox.TextField = "Pershkrimi";
                lstBox.ValueField = "IdKategori";
                lstBox.DataBind();

            }
            if (lstBox2 != null)
            {
                


                DbCore.DbShare.colKonfigurimAmbjenti col = new DbCore.DbShare.colKonfigurimAmbjenti();
                int idndermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                int idperdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                int idgjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
                col.mbushKonfigAmbjSipasIdKategori(1, idndermarje, idperdoruesi, idgjuha);
                col.mbushKonfigAmbjSipasIdKategori(2, idndermarje, idperdoruesi, idgjuha);
                col.mbushKonfigAmbjSipasIdKategori(3, idndermarje, idperdoruesi, idgjuha);
                col.mbushKonfigAmbjSipasIdKategori(4, idndermarje, idperdoruesi, idgjuha);
                col.mbushKonfigAmbjSipasIdKategori(6, idndermarje, idperdoruesi, idgjuha);
                col.mbushKonfigAmbjSipasIdKategori(44, idndermarje, idperdoruesi, idgjuha);
                col.mbushKonfigAmbjSipasIdKategori(45, idndermarje, idperdoruesi, idgjuha);
                col.mbushKonfigAmbjSipasIdKategori(46, idndermarje, idperdoruesi, idgjuha);
                lstBox2.ClientInstanceName = "lbxLloji";
                lstBox2.DataSource = col;
                lstBox2.TextField = "KodKonfigAmbjente";
                lstBox2.ValueField = "IdKonfigAmbjente"; lstBox2.DataBind();
            }
            GridViewDataComboBoxColumn colAutorizimet = grida.Columns["Autorizimet"] as GridViewDataComboBoxColumn; 
            ASPxComboBox cmbAutorizimi = ((ASPxGridView)sender).FindEditRowCellTemplateControl(colAutorizimet, "cmbBox") as ASPxComboBox;
            if (cmbAutorizimi != null)
            {
                cmbAutorizimi.ClientInstanceName = "cmbAutorizimi";
                cmbAutorizimi.ClientSideEvents.ButtonClick = "function(s, e) { Autorizime_Click(); }";
                ConfigureAspxComboBox.percaktoTemplateComboMeEnableCallback(cmbAutorizimi);
                ConfigureAspxComboBox.mbushComboAutorizime(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), cmbAutorizimi);
            }

        }
        protected void initNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {

        }
    
        protected void gridat_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
           
            int index = ASPxPageControl1.ActiveTabIndex;
            ASPxGridView grida;
            if (index == 0) grida = gvGrupet;
            else if (index == 1) grida = gvGrupet2;
            else grida = gvGrupet3;

            string[] arr = e.Parameters.Split(';');

            if (arr.Contains("Klonim"))
            {
                int keyValue = int.Parse(arr[1]);
             
                ShtoRreshtPerKlonimNeFillim(grida, keyValue);
                grida.StartEdit(0);
                grida.AddNewRow();//StartEdit(0);
               


            }
            else if (arr.Length == 3)
            {
                if (arr[2] == "")
                    grida.FilterExpression = "";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    //filtra.mbushFiltraGridaSipasFiltraKodi(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvGrupet", "GrupimDokumentash.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        grida.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, grida);

                        konfigVleraFillestarePerGride(grida);

                    }
                }
            }
            konfiguroGride(grida);
        }

        private void ShtoRreshtPerKlonimNeFillim(ASPxGridView grida, int keyValue)
        {

            int grupi;
            if (grida.ClientInstanceName == "gvGrupet")
                grupi = 1;
            else if (grida.ClientInstanceName == "gvGrupet2")
                grupi = 2;
            else
                grupi = 3;
            DbCore.DbRegjistrim.colGrupimDokumentiKoka col = new DbCore.DbRegjistrim.colGrupimDokumentiKoka();
            col.merrGrupeSipasGrupit(grupi, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session));

            DbCore.DbRegjistrim.clsGrupimDokumentiKoka kokaOld = col.Find(x => x.IdGrupimKoka == keyValue);
            DbCore.DbRegjistrim.clsGrupimDokumentiKoka kokaNew = kokaOld.Clone();
            kokaNew.IdGrupimKoka = 0;
            col.Insert(0, kokaNew);
            grida.DataSource = col;
            grida.DataBind();
            grida.JSProperties["cpKategoria"] =Newtonsoft.Json.JsonConvert.SerializeObject(kokaNew);
           

        }

        protected void gridat_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            ASPxGridView grida = sender as ASPxGridView;
            e.Properties["cpPageIndex"] = grida.PageIndex;
            e.Properties["cpPageRow"] = grida.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = grida.VisibleRowCount;
        }

        protected void gridat_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            e.Values.Clear();
            e.AddValue("(Te gjithe)", string.Empty, "true");
        }

        protected void gridat_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {

        }


    }
}