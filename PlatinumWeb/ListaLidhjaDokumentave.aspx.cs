using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Data;
using System.Globalization;
using System.Resources;
using DbCore.IMBUtils.Types;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.DbRegjistrim;
using DbCore.IMBUtils.DataBase;

namespace PlatinumWeb
{
    public partial class ListaLidhjaDokumentave : MyPageBase
    {
        private int idgjuha;
        DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();
        private DbCore.DbRegjistrim.colDokumentLidhesTrupi trupat = new DbCore.DbRegjistrim.colDokumentLidhesTrupi();
        private string komponente = "ListaLidhjaDokumentave.aspx";
        private string guidString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                DbCore.clsFunksione.logout(Session,true,"FaqePaautorizuar");
            }
            int idPerd = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerd);
            }
            oPerdorues = DbCore.mySessionObjects.kthePerdorues(Session);
            int idviti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            idgjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            percaktoTemplateMenu(idgjuha, ASPxMenu1, idviti, oPerdorues.IdPerdorues, idNdermarrje);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            System.Globalization.CultureInfo cultinf = DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo(idgjuha);
            if (!IsPostBack)
            {
                guidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                hfState.Set("guidString", guidString);
                EmratEKontrolleve(rm, cultinf);
                ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idPerd, idNdermarrje, cmbKonfigurimi, 54, rm, cultinf, idgjuha);
                cmbKonfigurimi.SelectedIndex = 0;
                DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()), idgjuha);
                hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;
                mbushGridDokumentNgaDB();
                gvDokumenta.FilterExpression = "[IdStatusDok]=1";
                konfiguroGride(idNdermarrje, idPerd, idgjuha, rm, cultinf);
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "gvDokumenta", gvDokumenta, cmbKonfigurimi.Text.Split(';')[0], "229", idgjuha);

                hfLloji.Value = Request.QueryString["lloj"];
                if (Request.QueryString["fshi"] == "po")
                {
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("regjMagFshirjaPerfundoiMeSukses", cultinf), pnlMesazhi);
                }
                else if (Request.QueryString["ruaj"] == "po")
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("regjMagModifikimiPerfundoiMeSukses", cultinf), pnlMesazhi);
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(oPerdorues.IdPerdorues, idNdermarrje, idviti, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
            }
            else
            {
                guidString = (string)hfState["guidString"];
                mbushGridDokumentNgaSession();
                konfiguroGride(idNdermarrje, idPerd, idgjuha, rm, cultinf);
            }
            AspxWebControlUtils.perkthePopUp(popFshi, rm.GetString("labelKujdes", cultinf), lblMsgbox, rm.GetString("labelAdministrimiMsgJeniSigurt", cultinf), ButtonCancel, rm.GetString("labelAnullo", cultinf));
            GridUtil.konfigGrideListeEMadhePaTheme(gvDokumenta, "IdKoka");
            clsToolbarConfig.mbushComboBoxFiltra(idgjuha, idNdermarrje, "gvDokumenta", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
            if (Request.QueryString["indexrow"] != null)
            {
                gvDokumenta.FocusedRowIndex = int.Parse(Request.QueryString["indexrow"]);
            }
            gvDokumenta.PercaktoTitlePanel(this, MenuInfo, pnlMesazhi, hfState, idPerd, idNdermarrje, idviti, idgjuha, int.Parse(cmbKonfigurimi.Value.ToString()), "ListaLidhjaDokumentave.aspx", rm, cultinf);
        }

        private void EmratEKontrolleve(ResourceManager rm, CultureInfo cultinf)
        {
            konfigurimi_Label.Text = rm.GetString("lblModeli", cultinf);
        }

        ///// <summary>
        /////      mbush combon e filtrave
        ///// </summary>
        //private void mbushComboBoxFiltra()
        //{
        //    //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();

        //    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka("gvDokumenta", "ListaLidhjaDokumentave.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //    DbCore.DbAdmin.colFiltratGrida colFiltra = new DbCore.DbAdmin.colFiltratGrida(koka.IdGridaKoka, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //    colFiltra.Insert(0, new DbCore.DbAdmin.clsFiltraGrida());
        //    MenuFilter.colekstioni = colFiltra;
        //    MenuFilter.ValueField = "IdFiltra";
        //    MenuFilter.TextField = "FiltraShenime";
        //}
        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        /// <param name="idViti"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNermarrje"></param>
        private void percaktoTemplateMenu(int idGjuha, ASPxMenu aSPxMenu1, int idViti, int idPerdorues, int idNdermarrje)
        {
            clsToolbarConfig.percaktoTemplateMenu(idGjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1, komponente, this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
        }

        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(idgjuha, ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));

        }
        private void mbushGridDokumentNgaSession()
        {
            DataTable tmpObject;
            bool sukses = DbCore.mySessionObjects.merrGrideNgaSessioni(Session, out tmpObject);
            if (!sukses)
                mbushGridDokumentNgaDB();
            else
            {
                gvDokumenta.DataSource = tmpObject;
                gvDokumenta.DataBind();
                tmpObject.Dispose();
            }
        }
        private void mbushGridDokumentNgaDB()
        {
            //mbush griden e popupit me te dhena            
            DataTable dt = DbCore.DbRegjistrim.colDokumentLidhesKoka.merrDokumentLidhesDT(DbCore.mySessionObjects.ktheNdermarrjeVit(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            DbCore.mySessionObjects.ruajGrideNeSession(Session, dt);
            gvDokumenta.DataSource = dt;
            gvDokumenta.DataBind();
            dt.Dispose();
        }

        private void konfiguroVleraFillestare()
        {
            DbCore.DbRegjistrim.colDokumentLidhesKoka colKoka = new DbCore.DbRegjistrim.colDokumentLidhesKoka(DbCore.mySessionObjects.ktheNdermarrjeVit(Session));
            gvDokumenta.DataSource = colKoka;
            gvDokumenta.DataBind();
        }

        private void konfiguroGride(int idNdermarrje, int idPerdoruesi, int idgjuha, ResourceManager rm, CultureInfo ci)
        {
            KonfigurimComboGride.ShtoNivel(gvDokumenta, 10, idNdermarrje, idPerdoruesi, idgjuha, Session, komponente, guidString);
            KonfigurimComboGride.ShtoModel(gvDokumenta, 10, idNdermarrje, idPerdoruesi, idgjuha, Session, komponente, guidString);
            KonfigurimComboGride.ShtoModelMeDataSource(gvDokumenta,()=> {

                var colKonf = new DbCore.DbShare.colKonfigurimAmbjenti();

                var idKategori = 3;
                colKonf.Add(new DbCore.DbShare.clsKonfigurimAmbjenti());
                colKonf.mbushKonfigAmbjSipasIdKategori(idKategori, idNdermarrje, idPerdoruesi, idgjuha);
                idKategori = 4;
                colKonf.mbushKonfigAmbjSipasIdKategori(idKategori, idNdermarrje, idPerdoruesi, idgjuha);
                idKategori = 20;
                colKonf.mbushKonfigAmbjSipasIdKategori(idKategori, idNdermarrje, idPerdoruesi, idgjuha);
                idKategori = 1;
                colKonf.mbushKonfigAmbjSipasIdKategori(idKategori, idNdermarrje, idPerdoruesi, idgjuha);
                idKategori = 2;
                colKonf.mbushKonfigAmbjSipasIdKategori(idKategori, idNdermarrje, idPerdoruesi, idgjuha);
                return colKonf;

            },Session, komponente, guidString, "IdKonfigGjenerues");
            KonfigurimComboGride.ShtoStatus(gvDokumenta, rm, ci);
            this.gvDokumenta.Columns["#"].VisibleIndex = 0;
        }

     


        //private void shtoKlient(int idNdermarrje)
        //{
        //    //DbCore.DbKontabiliteti.clsDatabaseKontabilitet dbKontab = new DbCore.DbKontabiliteti.clsDatabaseKontabilitet();
        //    gvDokumenta.Columns.Remove(gvDokumenta.Columns["IdKlientFurnitor"]);
        //    GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
        //    DbCore.DbKontabiliteti.colKlienteFurnitore colKlientet = new DbCore.DbKontabiliteti.colKlienteFurnitore();
        //    colKlientet.Add(new DbCore.DbKontabiliteti.clsKlientFurnitor(0, "", 0, false, 0, "", "", "", "", 0, "", "", "", "", "", "", "", "", true, 0, 0, 0, 0, 0, 0, 0, 0, 0, "", "", false, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "", "", 0, "", 0, 0, 0, "", 0, 0, 0, 0, false));
        //    colKlientet.mbushKlienteFurnitoreNdermarrjes(idNdermarrje);
        //    //DbCore.DbKontabiliteti.colKlienteFurnitore colKlientet = dbKontab.merrKlienteFurnitoreNdermarrjes(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //    colnew.PropertiesComboBox.DataSource = colKlientet;
        //    colnew.PropertiesComboBox.TextField = "EmertimiKF";
        //    colnew.PropertiesComboBox.ValueField = "IdKlientFurnitor";
        //    colnew.FieldName = "IdKlientFurnitor";
        //    gvDokumenta.Columns.Add(colnew);
        //}

        protected void gvDokumenta_DataBound(object sender, EventArgs e)
        {
            if (this.gvDokumenta.Columns["#"] == null)
            {
                DevExpress.Web.GridViewCommandColumn check = new DevExpress.Web.GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true; check.Width = Unit.Percentage(2);
                gvDokumenta.Settings.ShowFilterRow = true;
                gvDokumenta.Settings.ShowHeaderFilterButton = true;
                gvDokumenta.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                gvDokumenta.Settings.ShowFilterRowMenu = true;
                gvDokumenta.Columns.Add(check);
                gvDokumenta.Settings.ShowGroupPanel = true;
                gvDokumenta.KeyFieldName = "IdKoka";
                gvDokumenta.SettingsBehavior.AllowSelectByRowClick = true;
                gvDokumenta.SettingsBehavior.AllowFocusedRow = true;
            }
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
            int idPerodruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            int idGjuhaPerd = DbCore.mySessionObjects.ktheGjuhe(Session);
            DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuhaPerd, "gvDokumenta", komponente, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                filtra.IdPerdoruesi = idPerodruesi;
                mesazh = filtra.fshi();
                clsToolbarConfig.mbushComboBoxFiltra(idGjuhaPerd, idNdermarrje, "gvDokumenta", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
                percaktoTemplateMenu(idgjuha, ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), idPerodruesi, idNdermarrje);
                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                cmbFiltra.Text = "";
                gvDokumenta.FilterExpression = "[IdStatusDok]=1";

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
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvDokumenta",komponente, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gvDokumenta.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("IdNivel", gvDokumenta);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvDokumenta.GetSortedColumns();
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
            oPerdorues = DbCore.mySessionObjects.kthePerdorues(Session);
            filtri.IdPerdoruesi = oPerdorues.IdPerdorues;
            filtri.IdNdermarje = idNdermarrje;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            filtri.IdStatusDok = 1;
            mesazh = filtri.ruaj();
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvDokumenta", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
            percaktoTemplateMenu(idgjuha, ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);
            if (mesazh.Status == true)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

            cmbFiltra.Text = "";

        }
        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {
            //if (e.Item.Name == "Shto")
            //{
            //    Response.Redirect("Shto_RegjistrimMagazine.aspx?shtim_modifikim=shtim&lloj="+hfLloji .Value.ToString ());
            //}
            //else if (e.Item.Name == "Modifiko")
            //{
            //    int indeksi = gvDokumenta.FocusedRowIndex;
            //    string id;

            //    if (gvDokumenta.GetRowValues(indeksi, "IdKoka") != null)
            //        id = gvDokumenta.GetRowValues(indeksi, "IdKoka").ToString();
            //    else id = null;

            //    Response.Redirect("Shto_RegjistrimMagazine.aspx?id=" + id + "&indexrow=" + gvDokumenta.FocusedRowIndex + "&shtim_modifikim=modifikim&lloj="+hfLloji .Value.ToString ());
            //}
        }

        //fshin rreshtat e selektuar
        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            List<object> rreshtat = gvDokumenta.GetSelectedFieldValues("IdKoka");
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (rreshtat.Count == 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("regjMagMesazhZgjidhniNje", ci), pnlMesazhi);
                return;
            }
            List<string> TeFshire = new List<string>(), TeLidhur = new List<string>(), PeriudheKycur = new List<string>(), closedPeriod = new List<string>();
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            pergjigja.Text = "";
            //DbCore.DbAdmin.clsPeriudhaKontabel periudha;
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            foreach (object id in rreshtat)
            {
                DbCore.DbRegjistrim.clsDokumentLidhesKoka kok = new DbCore.DbRegjistrim.clsDokumentLidhesKoka();
                kok.IdKoka = Convert.ToInt32(id);
                kok.IdPerdorues = IdPerdoruesi;
                kok.merrSipasId(true);

                bool ekycur = DbCore.DbAdmin.clsPeriudhaKontabel.eshteKycurPeriudheSipasDateDheNdermarrjes(kok.DateDokumenti, idNdermarrje);
                if (ekycur)
                {
                    PeriudheKycur.Add(kok.NrLidhje);
                    continue;
                }

                if (DbCore.MbylljePeriudhe.PeriodClosing.IsPeriodClosed(kok.DateDokumenti, MyConnectionsManager.GetSelectedConNameServer(), idNdermarrje, KategoriDokumenti.LidhjeDokumentash, kok.IdKonfigAmbjente))
                {
                    closedPeriod.Add(kok.NrLidhje);
                    continue;
                }

                bool lidhur = dbAdmin.eshteDokumentiILidhur(kok.IdKoka, kok.IdNivel, "T_DOKUMENTLIDHESKOKA", "IDKOKA");
                if (lidhur)
                {
                    TeLidhur.Add(kok.NrLidhje);
                    continue;
                }
                if (kok.IdStatusDok == 2)
                    continue;
                //periudha = new DbCore.DbAdmin.clsPeriudhaKontabel(kok.DateDokumenti, idNdermarrje);
                //DbCore.clsMesazh mesazhi = periudha.isPeriudheKycur();
              
                mesazh = kok.fshi(rm, ci);
                if (mesazh.Status)
                {
                    #region Heq llogarite nga grida
                    hiqDokumentNgaGrida(kok.IdKoka, rm, ci);
                    #endregion
                    TeFshire.Add(kok.NrLidhje);
                }
            }
            dbAdmin.Dispose();
            string mesazhInfoGabimLidhur = "", mesazhInfoGabimPeridheKycur = "", mesazhInfoSukses = "", mesazhClosedPeriod = "";
            if (TeLidhur.Count == 1)
                mesazhInfoGabimLidhur = String.Format("{0}{1}{2}", rm.GetString("msgListaLidhjaDokPrefixNjejes", ci), String.Join(";", TeLidhur), rm.GetString("msgListaLidhjaSuffixNjejesGabimi", ci));
            else
                if (TeLidhur.Count > 1)
                    mesazhInfoGabimLidhur = String.Format("{0}{1}{2}", rm.GetString("msgListaLidhjaDokPrefixShumes", ci), String.Join(";", TeLidhur), rm.GetString("msgListaLidhjaSuffixShumesGabimi", ci));
            if (PeriudheKycur.Count == 1)
                mesazhInfoGabimPeridheKycur = String.Format("{0}{1}{2}", rm.GetString("msgListaLidhjaDokPrefixNjejes", ci), String.Join(";", PeriudheKycur), rm.GetString("regjMagSuffixMesazhNjejesPeriudheKycurGabimi", ci));
            else
                if (PeriudheKycur.Count > 1)
                    mesazhInfoGabimPeridheKycur = String.Format("{0}{1}{2}", rm.GetString("msgListaLidhjaDokPrefixShumes", ci), String.Join(";", PeriudheKycur), rm.GetString("regjMagSuffixMesazhShumesPeriudheKycurGabimi", ci));

            if (closedPeriod.Count == 1)
                mesazhClosedPeriod = String.Format("{0}{1}{2}", rm.GetString("regjisDokPrefixMesazhNjejes", ci), String.Join(", ", closedPeriod), rm.GetString("suffixMesazhClosedPeriodNjejes", ci));
            else if (closedPeriod.Count > 1)
                mesazhClosedPeriod = String.Format("{0}{1}{2}", rm.GetString("regjisDokPrefixMesazhNjejes", ci), String.Join(", ", closedPeriod), rm.GetString("suffixMesazhClosedPeriodShumes", ci));

            if (TeFshire.Count == 1)
                mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("msgListaLidhjaDokPrefixNjejes", ci), String.Join(";", TeFshire), rm.GetString("suffixMesazhNjejesSuksesi", ci));
            else
                if (TeFshire.Count > 1)
                    mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("msgListaLidhjaDokPrefixShumes", ci), String.Join(";", TeFshire), rm.GetString("suffixMesazhShumesSuksesi", ci));
            mesazhInfoGabimLidhur += mesazhInfoGabimPeridheKycur + mesazhClosedPeriod;
            if (mesazhInfoGabimLidhur != "" && mesazhInfoSukses != "")
                mesazhInfoGabimLidhur += rm.GetString("lidhesMesazhi", ci) + mesazhInfoSukses;
            if (mesazhInfoGabimLidhur != "")
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhInfoGabimLidhur, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazhInfoSukses, pnlMesazhi);
        }

        private void hiqDokumentNgaGrida(int idkoka, ResourceManager rm, CultureInfo ci)
        {
            if (this.gvDokumenta.DataSource != null)
            {
                DataTable dt = (DataTable)gvDokumenta.DataSource;
                DataRow[] drs = dt.Select("IdKoka = " + idkoka);
                if (drs.Length > 1)
                    throw new DbCore.MyException(rm.GetString("msgListaLidhjaDokJane2DokMeNjeIDNeGride", ci));
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                dt.Rows.Remove(dr);
                gvDokumenta.DataSource = dt;
                gvDokumenta.DataBind();
                dt.Dispose();
            }
            else mbushGridDokumentNgaDB();
        }

        protected void gvDokumenta_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            GridUtil.ToolTipButonaveMbiGride(gvDokumenta, ci, rm);
            if (e.CallbackName == "COLUMNMOVE" && gvDokumenta.AllColumns[int.Parse(e.Args[0])].Width.Value == 0)
                gvDokumenta.AllColumns[int.Parse(e.Args[0])].Width = Unit.Percentage(3);
            if (e.CallbackName == "APPLYFILTER" && e.Args[0] == "")
            {
                DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
                ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
                cmbFiltra.Text = "";
            }
        }

        protected void gvDokumenta_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gvDokumenta.PageIndex;
            e.Properties["cpPageRow"] = gvDokumenta.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gvDokumenta.VisibleRowCount;
        }

        protected void gvDokumenta_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string[] arr = e.Parameters.Split(';');
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idGjuhaPerd = DbCore.mySessionObjects.ktheGjuhe(Session);
            GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "gvDokumenta", gvDokumenta, cmbKonfigurimi.Text.Split(';')[0], "229", idGjuhaPerd);
            if (arr.Length == 3)
            {
                if (arr[2] == "")
                    gvDokumenta.FilterExpression = "[IdStatusDok]=1";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuhaPerd, "gvDokumenta", komponente, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], idNdermarrje, koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        gvDokumenta.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvDokumenta);
                    }
                }
            }
            gvDokumenta.Selection.UnselectAll();
        }

        protected void gvDokumenta_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if (e.Column.FieldName == "IdKlientFurnitor" || e.Column.FieldName == "IdKonfigAmbjente"  || e.Column.FieldName == "IdKonfigGjenerues")
                if (Converter.ConvertToInt(e.Value)==0)
                {
                    e.Criteria = null;
                }
        }

        protected void gvDokumenta_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            string TeGjithe = rm.GetString("GridHeaderFilterFillItemTeGjithe", ci);
            string nga = rm.GetString("GridHeaderFilterFillItemNga", ci);
            if (e.Column.FieldName == "NrLidhje")
            {
                e.Values.Clear();
                e.AddValue(TeGjithe, string.Empty, "true");
                e.AddValue(nga + " A-D ", string.Empty, e.Column.FieldName + ">'A     ' and " + e.Column.FieldName + " <'DDDDDDD'");
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