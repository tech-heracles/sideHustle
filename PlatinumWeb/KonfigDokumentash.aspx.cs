using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using DevExpress.Web;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Resources;
using DbCore.DbRegjistrim;
using DbCore.DbAdmin;
using DbCore.IMBUtils.Types;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{
    public partial class KonfigDokumentash : MyPageBase
    {
        private const string prefixMesazhNjejes = "Konfigurimi me kod: ";
        private const string prefixMesazhShumes = "Konfigurimet me kod: ";
        private const string suffixMesazhNjejesGabimiDefault = " eshte konfigurim default dhe nuk mund te fshihet";
        private const string suffixMesazhShumesGabimiDefault = " jane konfigurime default dhe nuk mund te fshihen";
        private const string suffixMesazhNjejesGabimiPerdorur = " eshte perdorur ne regjistrime. Jeni te sigurt qe doni ta fshini?";
        private const string suffixMesazhShumesGabimiPerdorur = " jane perdorur ne regjistrime. Jeni te sigurt qe doni t'i fshini?";
        private const string suffixMesazhNjejesSuksesi = " u fshi me sukses!";
        private const string suffixMesazhShumesSuksesi = " u fshine me sukses!";
        private const string lidhesMesazhi = ". Kurse ";
        private const string mesazhZgjidhniNje = "Ju lutem zgjidhni te pakten nje konfigurim!";
        private const string STR_NodhiGabimGjateFshirjes = "Ndodhi nje gabim gjate fshirjes";
        private const string STR_KonfigurimeNjejesDhanore = " se konfigurimit me kod: ";
        private const string STR_KonfigurimeShumesDhanore = " se konfigurimeve me kod: ";
        private string komponente = "KonfigDokumentash.aspx";
        private string guidString;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                DbCore.clsFunksione.logout(Session, true, "FaqePaautorizuar");
            }
            int idPerd = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            //if (CacheLayer.GlobalCacheManager.MySessionCache["KodiNdermarrjes"] == null)
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerd);
            }
            int idndermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idviti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            int idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);

            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            percaktoTemplateMenu(ASPxMenu1, idviti, idPerd, idndermarje);
            if (!IsPostBack)
            {
                guidString = Guid.NewGuid().ToString();
                hfState.Set("guidString", guidString);
                ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                if (Request.QueryString["ruaj"] == "ok")
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["mesazhRuajtjeMeSukses"], pnlMesazhi);
                else if (Request.QueryString["ruaj"] == "po")
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["msgModifikimiMeSukses"], pnlMesazhi);
                int indexrow;
                mbushGridDokumentNgaDB(idGjuha);
                konfiguroGride(idGjuha, idndermarje, idPerd);
                GridUtil.percaktoVisibleColumnsMeWidth(idGjuha, idndermarje, gvKonfigAmbjentesh, "gvKonfigAmbjentesh", "KonfigDokumentash.aspx"); this.gvKonfigAmbjentesh.Columns["#"].VisibleIndex = 0;
                GridUtil.konfigGrideListeEMadhePaTheme(gvKonfigAmbjentesh, "IdKonfigAmbjente");
                clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idndermarje, "gvKonfigAmbjentesh", 1, "KonfigDokumentash.aspx");
                mbushHiddenFieldMePerkthime(ci, rm);
                if (int.TryParse(Request.QueryString["indexrow"], out indexrow))
                {
                    gvKonfigAmbjentesh.FocusedRowIndex = indexrow;
                }
            }
            else
            {
                guidString = hfState["guidString"].ToString();
                mbushGridDokumentNgaSession(idGjuha);
                konfiguroGride(idGjuha, idndermarje, idPerd);
            }
        }

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        /// <param name="idViti"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNermarrje"></param>
        private void percaktoTemplateMenu(ASPxMenu aSPxMenu1, int idViti, int idPerdorues, int idNdermarrje)
        {
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, DbCore.clsFunksione.GetKomponente(Page.Request), this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
        }

        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
        }

        private void mbushGridDokumentNgaSession(int idGjuha)
        {
            DataTable tmpObject;
            bool sukses = DbCore.mySessionObjects.merrGrideNgaSessioni(Session, out tmpObject);
            if (!sukses)
                mbushGridDokumentNgaDB(idGjuha);
            else
            {
                gvKonfigAmbjentesh.DataSource = tmpObject;
                gvKonfigAmbjentesh.DataBind();
                tmpObject.Dispose();
            }
        }

        private void mbushGridDokumentNgaDB(int idGjuha)
        {//mbush griden e popupit me te dhena      
            DbCore.DbShare.colKonfigurimAmbjenti col = new DbCore.DbShare.colKonfigurimAmbjenti();
            DataTable dt = new DataTable();
            if (Request.QueryString["idsuperkat"] == "1")
                dt = col.mbushGjitheKonfigurimeAmbjenteshDT(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), 1, idGjuha);
            else if (Request.QueryString["idsuperkat"] == "2")
                dt = col.mbushGjitheKonfigurimeAmbjenteshDT(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), 2, idGjuha);
            else if (Request.QueryString["idsuperkat"] == "3")
                dt = col.mbushGjitheKonfigurimeAmbjenteshDT(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), 3, idGjuha);

            DbCore.mySessionObjects.ruajGrideNeSession(Session, dt);
            gvKonfigAmbjentesh.DataSource = dt;
            gvKonfigAmbjentesh.DataBind();
            dt.Dispose();
        }


        protected void gvKonfigAmbjentesh_DataBound(object sender, EventArgs e)
        {// shton colonen # per selektim dhe disa karakteristika te grides
            if (this.gvKonfigAmbjentesh.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per 
                //perzgjidh
                DevExpress.Web.GridViewCommandColumn check = new DevExpress.Web.GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true; check.Width = Unit.Percentage(2);
                // check.SetColVisibleIndex(0);
                //behet per te afishuar rreshtin qe do sherbej per filtrim
                gvKonfigAmbjentesh.Settings.ShowFilterRow = true;
                gvKonfigAmbjentesh.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                gvKonfigAmbjentesh.Settings.ShowFilterRowMenu = true;
                gvKonfigAmbjentesh.Columns.Add(check);

                gvKonfigAmbjentesh.KeyFieldName = "IdKonfigAmbjente";
                gvKonfigAmbjentesh.SettingsBehavior.AllowSelectByRowClick = true;
                gvKonfigAmbjentesh.SettingsBehavior.AllowFocusedRow = true;
            }
        }

        private void konfiguroGride(int idGjuha, int idNdermarrje, int idPerdoruesi)
        {
            var idNdermarrjeVit = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
            //ShtoAutorizim(idPerdoruesi, idNdermarrje);
            shtoKategoriDok();
      
            KonfigurimComboGride.ShtoNivelMeDataSource(gvKonfigAmbjentesh, () => colNivelRegjistrimi.ktheGjitheNivelRegjistrimi(idNdermarrje, idPerdoruesi, true), Session, komponente, guidString);
            shtoSkeme(idGjuha, idNdermarrje, idNdermarrjeVit, idPerdoruesi);
            //percaktoTamplate();
            shto_Lloji();
        }

        private void shto_Lloji()
        {
            gvKonfigAmbjentesh.KonfiguroComboMeItems("Lloji", () => new ListEditItemCollection()
            {
                new ListEditItem("AlphaWeb",1),
                new ListEditItem("AlphaMobile", 2),
                new ListEditItem("Te dyja", 3)
            });
        }

        public void ShtoAutorizim(int idPerdoruesi, int idNdermarrje)
        {
            gvKonfigAmbjentesh.KonfiguroCombo("IdNivelAutorizimi", "KodiAutorizim", "KodiAutorizim", () => new colAutorizimetKoka(idPerdoruesi), Session, komponente, guidString);

        }

        //sherben per ta bere ne forme combo-je shtyllen e grupeve 
        private void shtoKategoriDok()
        {
            gvKonfigAmbjentesh.KonfiguroCombo("IdKategori", "IdKategori", "Pershkrimi", () =>
            {
                var oKategori = new DbCore.DbRegjistrim.clsKategoriNivelDok();
                var colKategori = new DbCore.DbRegjistrim.colKategoriNiveleDok();
                colKategori.Add(new DbCore.DbRegjistrim.clsKategoriNivelDok(-1, "", 0, 1, false, 0, false));
                if (Request.QueryString["idsuperkat"] == "1")
                    colKategori.AddRange(oKategori.merriTeGjithePaSipasSuperKat(1)); //nuk e marr parasysh ne SP ndermarjen mqs kategorite nuk jane ne nivel ndermarje
                else if (Request.QueryString["idsuperkat"] == "2")
                    colKategori.AddRange(oKategori.merriTeGjithePaSipasSuperKat(2));
                else if (Request.QueryString["idsuperkat"] == "3")
                    colKategori.AddRange(oKategori.merriTeGjithePaSipasSuperKat(3));
                return colKategori;
            }, Session, komponente, guidString);
        }

        private void shtoSkeme(int idGjuha, int idNdermarrje, int idNdermarrjeVit, int idPerdoruesi)
        {

            gvKonfigAmbjentesh.KonfiguroComboMeItems("IdSkemeKontabel", () =>
            {
                var listItems = new ListEditItemCollection();

                var colSkema = new DbCore.DbKontabiliteti.colSkemaKontabelNew(idNdermarrjeVit);
                colSkema.Insert(0, new DbCore.DbKontabiliteti.clsSkemaKontabelNew());
                foreach (DbCore.DbKontabiliteti.clsSkemaKontabelNew sk in colSkema)
                    listItems.Add(sk.KodSkemeKont, sk.IdSkemeKont);

                var col = new DbCore.DbShare.colKonfigurimAmbjenti();
                col.mbushKonfigAmbjSipasIdKategori(5, idNdermarrje, idPerdoruesi, idGjuha);


                foreach (DbCore.DbShare.clsKonfigurimAmbjenti k in col)
                    listItems.Add(k.KodKonfigAmbjente, k.IdKonfigAmbjente);

                col.mbushKonfigAmbjSipasIdKategori(5, -1, idPerdoruesi, idGjuha);
                foreach (DbCore.DbShare.clsKonfigurimAmbjenti k in col)
                    listItems.Add(k.KodKonfigAmbjente, k.IdKonfigAmbjente);
                return listItems;
            });
        }


        //thirret sa here qe behet Callback, dhe ben edhe njeher lidhjen me datasource-in

        protected void gvKonfigAmbjentesh_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {

        }

        //sherben per te vendosur filtra tek header-i i grides (mund te ndryshohet me vone per tu
        //bere  me e konfigurueshme
        protected void gvKonfigAmbjentesh_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {

            if (e.Column.FieldName == "KodKonfigAmbjente" || e.Column.FieldName == "PershkrimKonfigAmbjente")
            {
                e.Values.Clear();
                //e.AddShowAll();
                e.AddValue("(Te gjithe)", string.Empty, "true");
                e.AddValue("Nga A-D ", string.Empty, e.Column.FieldName + ">'A     ' and " + e.Column.FieldName + " <'DDDDDDD'");
                e.AddValue("Nga D-G ", string.Empty, e.Column.FieldName + ">'D     ' and " + e.Column.FieldName + "<'GGGGGGG'");
                e.AddValue("Nga H-K ", string.Empty, e.Column.FieldName + ">'H     ' and " + e.Column.FieldName + "<'KKKKKKK'");
                e.AddValue("Nga L-O ", string.Empty, e.Column.FieldName + ">'L     ' and " + e.Column.FieldName + "  <'OOOOOOO'");
                e.AddValue("Nga P-S ", string.Empty, e.Column.FieldName + ">'P     ' and " + e.Column.FieldName + "<'SSSSSSS'");
                e.AddValue("Nga T-W ", string.Empty, e.Column.FieldName + ">'T     ' and " + e.Column.FieldName + "<'WWWWWWW'");
                e.AddValue("Nga X-Z ", string.Empty, e.Column.FieldName + ">'X     ' and " + e.Column.FieldName + "<'ZZZZZZZ'");
            }
            else
            {
                e.Values.Clear();
                e.AddValue("(Te gjithe)", string.Empty, "true");
            }
        }

        public void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
            //filtra.mbushFiltraGridaSipasFiltraEmri(cmbFiltra.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuha, "gvKonfigAmbjentesh", "KonfigDokumentash.aspx", idNdermarrje);
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            //DbCore.DbAdmin.clsFiltraGrida filtra = dbAdmin.merrFiltraGridaSipasFiltraEmri(cmbFiltra.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();
                //mbushComboBoxFiltra();
                clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idNdermarrje, "gvKonfigAmbjentesh", 1, "KonfigDokumentash.aspx");
                percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);

                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
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
            int idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            DbCore.DbAdmin.clsFiltraGrida filtri = new DbCore.DbAdmin.clsFiltraGrida();
            filtri.FiltraKodi = cmbFiltra.Text;
            filtri.FiltraShenime = cmbFiltra.Text;
            filtri.FiltraUniversal = false;// Universal_ASPxCheckBox.Checked;
            //DbCore.DbAdmin.clsGridaKoka koka = dbAdmin.merrGridaKokaByEmri("gvKonfigAmbjentesh", "RegjistrimMagazine.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuha, "gvKonfigAmbjentesh", "KonfigDokumentash.aspx", idNdermarrje);
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gvKonfigAmbjentesh.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("KodKonfigAmbjente", gvKonfigAmbjentesh);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvKonfigAmbjentesh.GetSortedColumns();
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
            //    filtri.KoloneRenditje = "KodKonfigAmbjente";
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
            clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idNdermarrje, "gvKonfigAmbjentesh", 1, "KonfigDokumentash.aspx");
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);

            if (mesazh.Status == true)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

            cmbFiltra.Text = "";

        }

        protected void ButtonOk2_Click2(object sender, EventArgs e)
        {
            string konfig = "", konfjodefault = "";
            List<object> rreshtat = gvKonfigAmbjentesh.GetSelectedFieldValues("IdKonfigAmbjente");
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            int idndermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DbCore.DbAdmin.clsNdermarrje nderm = new DbCore.DbAdmin.clsNdermarrje(idndermarje);
            int idndermarjenga;
            if (nderm.Lloji == 3)
                idndermarjenga = -3;
            else if (nderm.Lloji == 2)
                idndermarjenga = -2;
            else
                idndermarjenga = -1;
            int idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            foreach (object id in rreshtat)
            {
                DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti(Convert.ToInt32(id), idGjuha);
                if (!konf.DefaultKonfigAmbjente)
                {
                    konfjodefault += konf.KodKonfigAmbjente;
                    continue;
                }
                mesazh = DbCore.DbShare.clsKonfigurimAmbjenti.ktheDefault(Convert.ToInt32(id), idndermarje, idndermarjenga, DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
                if (!mesazh.Status)
                    konfig += konf.KodKonfigAmbjente + ",";
            }
            if (konfig == "" && konfjodefault == "")
            {
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["mesazhRuajtjeMeSukses"], pnlMesazhi);
            }
            else if (konfig != "" && konfjodefault == "")
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, String.Format("Konfigurimet {0} nuk u kthyen ne default!", konfig.Substring(0, konfig.Length - 1)), pnlMesazhi);
            else if (konfig != "" && konfjodefault != "")
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, String.Format("Konfigurimet {0} nuk u kthyen ne default! Kurse konfigurimet {1} nuk jane konfigurime default!", konfig.Substring(0, konfig.Length - 1), konfjodefault.Substring(0, konfjodefault.Length - 1)), pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, String.Format("Konfigurimet {0} nuk jane konfigurime default!", konfjodefault.Substring(0, konfjodefault.Length - 1)), pnlMesazhi);

            mbushGridDokumentNgaDB(idGjuha);
        }

        /// <summary>
        /// fshin rreshtat e selektuara te grides, pasi shtypet ok te popupi: popUp
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            List<object> rreshtat = gvKonfigAmbjentesh.GetSelectedFieldValues("IdKonfigAmbjente");
            if (rreshtat.Count == 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhZgjidhniNje, pnlMesazhi);
                return;
            }
            List<string> teFshire = new List<string>(), tePaFshireDef = new List<string>(), tePafshirePerd = new List<string>();
            //clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "", pnlMesazhi);
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            DbCore.DbShare.clsDatabaseShare dbShare = new DbCore.DbShare.clsDatabaseShare();
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            foreach (object id in rreshtat)
            {
                konf.IdKonfigAmbjente = int.Parse(id.ToString());
                konf.mbushkonfigPaLloj(int.Parse(id.ToString()));
                konf.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                //konf = dbShare.ktheKonfiguriminMeID(id)[0];
                if (konf.DefaultKonfigAmbjente == true)
                {
                    tePaFshireDef.Add(konf.KodKonfigAmbjente);
                    continue;
                }
                else if (dbShare.kaVeprimeKonfigurim(int.Parse(id.ToString())))
                {
                    tePafshirePerd.Add(konf.KodKonfigAmbjente);
                    continue;
                }
                else
                {
                    mesazh = konf.fshi();
                    #region Heq konfigurimet nga grida

                    // ASPxGridView_Artikull.DataSource = dt;
                    hiqDokumentNgaGrida(konf.IdKonfigAmbjente, DbCore.mySessionObjects.ktheGjuhe(Session));
                    teFshire.Add(konf.KodKonfigAmbjente);
                    #endregion
                }
            }
            dbShare.Dispose();
            string mesazhInfoGabim = "", mesazhInfoSukses = "";

            if (tePaFshireDef.Count == 1)
                mesazhInfoGabim = String.Format("{0}{1}{2}", prefixMesazhNjejes, String.Join(";", tePaFshireDef), suffixMesazhNjejesGabimiDefault);
            else
                if (tePaFshireDef.Count > 1)
                mesazhInfoGabim = String.Format("{0}{1}{2}", prefixMesazhShumes, String.Join(";", tePaFshireDef), suffixMesazhShumesGabimiDefault);
            if (tePafshirePerd.Count == 1)
            {
                string pyetje = String.Format("{0}{1}{2}", prefixMesazhNjejes, String.Join(";", tePafshirePerd), suffixMesazhNjejesGabimiPerdorur);
                if (!ClientScript.IsStartupScriptRegistered("showVal"))
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "showVal", "identifikuesPyetje = 'FshiMeVrime'; myMesazh.ShtoPyetje('" + pyetje+ "', true); ", true);
                pyetjeFshiKonfig.Value = pyetje;
                hfshfaq.Value = "shfaq";
            }
            else if (tePafshirePerd.Count > 1)
            {
                string pyetje = String.Format("{0}{1}{2}", prefixMesazhShumes, String.Join(";", tePafshirePerd), suffixMesazhShumesGabimiPerdorur);
                if (!ClientScript.IsStartupScriptRegistered("showVal"))
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "showVal", "identifikuesPyetje = 'FshiMeVeprime'; myMesazh.ShtoPyetje('" + pyetje + "', true); ", true);
                pyetjeFshiKonfig.Value = pyetje;
                hfshfaq.Value = "shfaq";
            }

            if (teFshire.Count == 1)
                mesazhInfoSukses = String.Format("{0}{1}{2}", prefixMesazhNjejes, String.Join(";", teFshire), suffixMesazhNjejesSuksesi);
            else
                if (teFshire.Count > 1)
                mesazhInfoSukses = String.Format("{0}{1}{2}", prefixMesazhShumes, String.Join(";", teFshire), suffixMesazhShumesSuksesi);
            if (mesazhInfoGabim != "" && mesazhInfoSukses != "")
                mesazhInfoGabim += lidhesMesazhi + mesazhInfoSukses;
            if (mesazhInfoGabim != "")
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhInfoGabim, pnlMesazhi);
            else
                if(!String.IsNullOrEmpty(mesazhInfoSukses))
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazhInfoSukses, pnlMesazhi);
            pnlMesazhi.Update();
        }

        /// <summary>
        /// fshin konfigurimet qe kane veprime nese perdoruesi shtyp ok tek popupi: popFshiMeVeprime
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonOk_Click(object sender, EventArgs e)
        {
            //clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "", pnlMesazhi);            
            List<object> rreshtat = gvKonfigAmbjentesh.GetSelectedFieldValues("IdKonfigAmbjente");
            List<string> teFshire = new List<string>(), tePaFshire = new List<string>();
            if (rreshtat.Count == 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhZgjidhniNje, pnlMesazhi);
                return;
            }
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            foreach (object id in rreshtat)
            {
                //DbCore.DbShare.clsDatabaseShare dbShare = new DbCore.DbShare.clsDatabaseShare();
                DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                konf.IdKonfigAmbjente = int.Parse(id.ToString());
                konf.mbushkonfigPaLloj(int.Parse(id.ToString()));
                if (konf != null)
                {
                    konf.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                    //konf = dbShare.ktheKonfiguriminMeID(id)[0];
                    konf.mbushkonfigPaLloj(int.Parse(id.ToString()));
                    mesazh = konf.fshi();
                    if (mesazh.Status)
                    {
                        #region Heq konfigurimet nga grida
                        // ASPxGridView_Artikull.DataSource = dt;
                        hiqDokumentNgaGrida(konf.IdKonfigAmbjente, DbCore.mySessionObjects.ktheGjuhe(Session));
                        teFshire.Add(konf.KodKonfigAmbjente);
                        #endregion
                    }
                    else tePaFshire.Add(konf.KodKonfigAmbjente);
                }
            }
            string mesazhInfoGabim = "", mesazhInfoSukses = "";
            if (tePaFshire.Count == 1)
                mesazhInfoGabim = String.Format("{0}{1}{2}", STR_NodhiGabimGjateFshirjes, STR_KonfigurimeNjejesDhanore, String.Join(";", tePaFshire));
            else
                if (tePaFshire.Count > 1)
                mesazhInfoGabim = String.Format("{0}{1}{2}", STR_NodhiGabimGjateFshirjes, STR_KonfigurimeShumesDhanore, String.Join(";", tePaFshire));
            if (teFshire.Count == 1)
                mesazhInfoSukses = String.Format("{0}{1}{2}", prefixMesazhNjejes, String.Join(";", teFshire), suffixMesazhNjejesSuksesi);
            else
                if (teFshire.Count > 1)
                mesazhInfoSukses = String.Format("{0}{1}{2}", prefixMesazhShumes, String.Join(";", teFshire), suffixMesazhShumesSuksesi);
            if (mesazhInfoGabim != "" && mesazhInfoSukses != "")
                mesazhInfoGabim += lidhesMesazhi + mesazhInfoSukses;
            if (mesazhInfoGabim != "")
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhInfoGabim, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazhInfoSukses, pnlMesazhi);
            pnlMesazhi.Update();
        }

        private void hiqDokumentNgaGrida(int idkoka, int idGjuha)
        {
            if (this.gvKonfigAmbjentesh.DataSource != null)
            {
                DataTable dt = (DataTable)gvKonfigAmbjentesh.DataSource;
                DataRow[] drs = dt.Select("IdKonfigAmbjente = " + idkoka);
                if (drs.Length > 1)
                    throw new Exception("GABIM: Ndodhen 2 konfigurime me te njejten id ne gride");
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                dt.Rows.Remove(dr);
                gvKonfigAmbjentesh.DataSource = dt;
                gvKonfigAmbjentesh.DataBind();
                dt.Dispose();
            }
            else mbushGridDokumentNgaDB(idGjuha);
        }

        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {
            if (e.Item.Name == "Default")
            {

            }

        }

        protected void gvKonfigAmbjentesh_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            //if (e.Column.FieldName == "IdKategori")
            //{
            //    GridViewDataComboBoxColumn colnew = gvKonfigAmbjentesh.Columns["IdNivel"] as GridViewDataComboBoxColumn;
            //    DbCore.DbRegjistrim.clsNivelRegjistrimi oNivel = new DbCore.DbRegjistrim.clsNivelRegjistrimi();
            //    DbCore.DbRegjistrim.colNivelRegjistrimi colNivele = new DbCore.DbRegjistrim.colNivelRegjistrimi();
            //    colNivele.Add(new DbCore.DbRegjistrim.clsNivelRegjistrimi(-1, 0, "", "", 0, false, 0, 0, 0));
            //    string[] a = e.Criteria.ToString().Split("=".ToCharArray());
            //    for (int i = 0; i < a.Length; i++)
            //    {
            //        if (a[i].Contains("[IdKategori] "))
            //        {
            //            string[] spliti = { ".0m" };
            //            string id = a[i + 1].Split(spliti, StringSplitOptions.RemoveEmptyEntries)[0];
            //            id = id.Split(' ')[1];

            //            oNivel.IdKategori = Convert.ToInt32(id);
            //            if (oNivel.IdKategori != -1)
            //                colNivele.AddRange(oNivel.merrGjitheNivelRegjistrimiSipasKategoriPaAll(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session)));
            //            else
            //                colNivele.AddRange(oNivel.merrGjitheNivelRegjistrimi(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session)));

            //        }
            //    }
            //    colnew.PropertiesComboBox.DataSource = colNivele;
            //    colnew.PropertiesComboBox.TextField = "Pershkrimi";
            //    colnew.PropertiesComboBox.ValueField = "IdNivel";
            //    colnew.FieldName = "IdNivel";
            //    if (decimal.Parse(e.Value.ToString()) == -1)
            //    {
            //        e.Criteria = null;
            ////    }
            //}
            if (e.Column.FieldName == "IdNivel" || e.Column.FieldName == "IdKategori")
                if (Converter.ConvertToInt(e.Value) == -1)
                    e.Criteria = null;
        }

        protected void gvKonfigAmbjentesh_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                if (arr[2] == "")
                {

                    gvKonfigAmbjentesh.FilterExpression = "";
                }
                else
                {
                    //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    //filtra.mbushFiltraGridaSipasFiltraKodi(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvKonfigAmbjentesh", "KonfigDokumentash.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
                    //DbCore.DbAdmin.clsFiltraGrida filtra = dbAdmin.merrFiltraGridaSipasFiltraKodi(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    if (filtra.FiltraKodi != null)
                    {
                        gvKonfigAmbjentesh.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvKonfigAmbjentesh);
                    }
                }
            }
            gvKonfigAmbjentesh.Selection.UnselectAll();
        }

        protected void gvKonfigAmbjentesh_CustomJSProperties(object sender, DevExpress.Web.ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gvKonfigAmbjentesh.PageIndex;
            e.Properties["cpPageRow"] = gvKonfigAmbjentesh.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gvKonfigAmbjentesh.VisibleRowCount;
        }

        /// <summary>
        /// Metode qe sherben per te kaluar perkthime te js
        /// </summary>
        /// <param name="cultinf">Merr culture info</param>
        /// <param name="rm">Merr Resource Manager</param>
        private void mbushHiddenFieldMePerkthime(CultureInfo cultinf, ResourceManager rm)
        {
            hfState.Set("msgPyetjeFshirjeKonfigurime", rm.GetString("msgPyetjeFshirjeKonfigurime", cultinf));
            hfState.Set("msgPyetjeFshirjeKonfigurimeNeRegjistrime", rm.GetString("msgPyetjeFshirjeKonfigurimeNeRegjistrime", cultinf));

        }
    }
}

