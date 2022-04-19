using System;
using System.Collections.Generic;
using DevExpress.Web;
using System.Data;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    /// <summary>
    /// nderfaqja e KonfigurimFormatImportit te prodhimit
    /// </summary>
    public partial class KonfigurimFormatImporti : MyPageBase
    {
        /// <summary>
        /// konstante per mesazhin e fshirjes ne njejes
        /// </summary>
        private const string prefixMesazhNjejes = "Formati me kod: ";
        /// <summary>
        /// konstante per mesazhin e fshirjes ne shumes
        /// </summary>
        private const string prefixMesazhShumes = "Formatet me kod: ";
        /// <summary>
        /// mesazhi per fshirjen ne njejes
        /// </summary>
        private const string suffixMesazhNjejesSuksesi = " u fshi me sukses!";
        /// <summary>
        /// mesazhi per fshirjen ne shumes
        /// </summary>
        private const string suffixMesazhShumesSuksesi = " u fshinë me sukses!";
        /// <summary>
        /// konstante per lidhezen
        /// </summary>
        private const string lidhesMesazhi = ". Kurse ";
        private const string suffixMesazhNjejesLidhurGabimi = " është i lidhur dhe nuk mund të fshihet! ";
        /// <summary>
        /// konstante per mesazhin e mos fshirjes ne shumes per lidhjen
        /// </summary>
        private const string suffixMesazhShumesLidhurGabimi = " janë të lidhur dhe nuk mund të fshihen! ";
        /// <summary>
        /// mesazhi per mos zgjedhjen e asnje dokumenti
        /// </summary>
        private const string STR_JuLutemZgjidhniTePaktenNjeDokument = "Ju lutem zgjidhni të paktën një dokument!";
        /// <summary>
        /// mesazhi kur ekzistojne dy dokumenta me te njejen id ne grid
        /// </summary>
        private const string STR_GABIMNdodhen2DokumentaListPageseMeTeNjejtenIdNeG = "GABIM: Ndodhen 2 formate  me të njejten id ne gridë";

        /// <summary>
        /// id e ndermarjes
        /// </summary>
        private int _idndermarje, _idperdoruesi, _idviti, _idgjuha;

        private string komponente = "KonfigurimFormatImporti.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!DbCore.mySessionObjects.isLogedIn(Session))
                DbCore.clsFunksione.logout(Session,true,"FaqePaautorizuar");
            _idperdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
                Response.Redirect("Login_Ndermarrje.aspx?id=" + _idperdoruesi);
            _idndermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            _idviti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            _idgjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            PercaktoTemplateMenu(_idgjuha, ASPxMenu1, _idviti, _idperdoruesi, _idndermarje);
            if (!IsPostBack)
            {
                PerktheLabel();
                var cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
                var rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(_idperdoruesi, _idndermarje, cmbKonfigurimi, 65, rm, cultinf, _idgjuha);
                cmbKonfigurimi.SelectedIndex = 0;
                var konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()), _idgjuha);
                hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;
                MbushGridNgaDb();
                KonfiguroGride();
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(_idndermarje, "gvKonfigurim", gvKonfigurim, cmbKonfigurimi.Text.Split(';')[0], "174", DbCore.mySessionObjects.ktheGjuhe(Session));

                if (Request.QueryString["fshi"] == "po")
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "Fshirja e dokumentit përfundoi me sukses!", pnlMesazhi);
                else if (Request.QueryString["ruaj"] == "po")
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "Modifikimi përfundoi me sukses!", pnlMesazhi);

                GrupoRreshtatGridesSipasKategorive();

                var tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(_idperdoruesi, _idndermarje, _idviti, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
            }
            else
            {
                MbushGridNgaSession();
                KonfiguroGride();
            }
            GridUtil.konfigGrideListeEMadhePaTheme(gvKonfigurim, "IdKoka");
            gvKonfigurim.Columns["#"].VisibleIndex = 0;
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), _idndermarje, "gvKonfigurim", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
            if (Request.QueryString["indexrow"] != null)
            {
                gvKonfigurim.FocusedRowIndex = int.Parse(Request.QueryString["indexrow"]);
            }
            GridUtil.EmrateButonaveMbiGride(gvKonfigurim);
        }

        public void PerktheLabel()
        {
            var cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            var rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            konfigurimi_Label.Text = rm.GetString("lblModeli", cultinf);
        }

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        /// <param name="idViti"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNermarrje"></param>
        private void PercaktoTemplateMenu(int idGjuha, ASPxMenu aSPxMenu1, int idViti, int idPerdorues, int idNdermarrje) =>
            clsToolbarConfig.percaktoTemplateMenu(idGjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1, komponente, this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));

        protected void RuajKolona_Click(object sender, EventArgs e)
        {
            var idfiltri = 0;
            var idkonf = DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(cmbKonfigurimi.Text.Split(';')[0], _idndermarje);
            var mesazh = GridUtil.ruajFiltra(_idndermarje, _idperdoruesi, _idgjuha, "gvKonfigurim", komponente, "FilterDefault", gvKonfigurim.FilterExpression, gvKonfigurim, "Kodi", idkonf, out idfiltri);//ruan filtrin e zgjedhur tek filtrat
            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                return;
            }
            mesazh = GridUtil.ruajkonfigurimgride(gvKonfigurim, cmbKonfigurimi.Text, _idndermarje, _idperdoruesi, 174, idfiltri, _idviti, DbCore.mySessionObjects.ktheCultureInfo(Session), DbCore.mySessionObjects.ktheGjuhe(Session));/// ruan konfigurimin e grides dhe filtrin

            clsToolbarConfig.mbushComboBoxFiltra(_idgjuha, _idndermarje, "gvKonfigurim", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);

            PercaktoTemplateMenu(_idgjuha, ASPxMenu1, _idviti, _idperdoruesi, _idndermarje);

            if (mesazh.Status) clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
        }

        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e) =>
            PercaktoTemplateMenu(_idgjuha, ASPxMenu1, _idviti, _idperdoruesi, _idndermarje);

        /// <summary>
        /// mbush griden me te dhena te ruajtura ne sesion
        /// </summary>
        private void MbushGridNgaSession()
        {
            DataTable tmpObject;
            var sukses = DbCore.mySessionObjects.merrGrideNgaSessioni(Session, out tmpObject);
            if (!sukses)
                MbushGridNgaDb();
            else
            {
                gvKonfigurim.DataSource = tmpObject;
                gvKonfigurim.DataBind();
                tmpObject.Dispose();
            }
        }

        /// <summary>
        /// mbush griden me te dhena nga db
        /// </summary>
        private void MbushGridNgaDb()
        {
            var dt = DbCore.DbAdmin.colKokaFormatImporti.ktheFormatImportiDTSipasTeDrejtave(_idndermarje, _idviti, _idperdoruesi, komponente);
            DbCore.mySessionObjects.ruajGrideNeSession(Session, dt);
            gvKonfigurim.DataSource = dt;
            gvKonfigurim.DataBind();
            dt.Dispose();
        }

        /// <summary>
        /// konfiguron griden
        /// </summary>
        private void KonfiguroGride()
        {
            ShtoLloj();
           
            gvKonfigurim.Columns["D_SHTIM"].Visible = false;
            gvKonfigurim.Columns["D_MOD"].Visible = false;
            gvKonfigurim.Columns["D_FSH"].Visible = false;
            gvKonfigurim.Columns["#"].VisibleIndex = 0;
        }


        private void GrupoRreshtatGridesSipasKategorive()
        {
            gvKonfigurim.GroupBy(gvKonfigurim.Columns["IdSuperKategori"], 1);
            gvKonfigurim.GroupBy(gvKonfigurim.Columns["Kategori"], 2);
            for (var i = 0; i < ((DataTable)gvKonfigurim.DataSource).Rows.Count; i++)
                if (gvKonfigurim.GetRowLevel(i) == 0)
                    gvKonfigurim.ExpandRow(i, false);
        }

        /// <summary>
        /// kthen kolonen e llojeve
        /// </summary>
        private void ShtoLloj()
        {
            gvKonfigurim.KonfiguroComboMeItems("IdSuperKategori", () => new ListEditItemCollection() {
                { "Konfigurime per celjet", 1 },
                { "Konfigurime per regjistrimet", 2 }
            });
        }

        /// <summary>
        /// kur grida ben databound per ti shtuar kolonen e selektimit
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvKonfigurim_DataBound(object sender, EventArgs e)
        {
            if (gvKonfigurim.Columns["#"] != null) return;
            var check = new GridViewCommandColumn("#")
            {
                ShowSelectCheckbox = true,
                Width = System.Web.UI.WebControls.Unit.Percentage(2)
            };
            gvKonfigurim.Settings.ShowFilterRow = true;
            gvKonfigurim.Settings.ShowHeaderFilterButton = true;
            gvKonfigurim.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
            gvKonfigurim.Settings.ShowFilterRowMenu = true;
            gvKonfigurim.Columns.Add(check);
            gvKonfigurim.Settings.ShowGroupPanel = true;
            gvKonfigurim.KeyFieldName = "IdKoka";
            gvKonfigurim.SettingsBehavior.AllowSelectByRowClick = true;
            gvKonfigurim.SettingsBehavior.AllowFocusedRow = true;
            gvKonfigurim.SettingsBehavior.AllowSelectSingleRowOnly = true;
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
            var itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            var cmbFiltra = ((MenuFilter)itemButton.Template).FindControl("btnFiltra") as ASPxComboBox;
            var idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            var filtra = new DbCore.DbAdmin.clsFiltraGrida();
            var koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvKonfigurim", komponente, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                var mesazh = new DbCore.clsMesazh();
                filtra.IdPerdoruesi = _idperdoruesi;
                mesazh = filtra.fshi();
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvKonfigurim", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
                PercaktoTemplateMenu(_idgjuha, ASPxMenu1, _idviti, _idperdoruesi, _idndermarje);
                if (mesazh.Status)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

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
            var itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            var cmbFiltra = ((MenuFilter)itemButton.Template).FindControl("btnFiltra") as ASPxComboBox;
            var koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvKonfigurim", komponente, _idndermarje, int.Parse(cmbKonfigurimi.Value.ToString()));
            var filtri = new DbCore.DbAdmin.clsFiltraGrida() { FiltraKodi = cmbFiltra?.Text, FiltraShenime = cmbFiltra?.Text, FiltraUniversal = false, GridaKokaId = koka.IdGridaKoka, FiltraVlera = gvKonfigurim.FilterExpression, IdPerdoruesi = _idperdoruesi, IdNdermarje = _idndermarje, IdStatusDok = 1 };
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("Kodi", gvKonfigurim);
            //var kolona = gvKonfigurim.GetSortedColumns();
            //if (kolona.Count > 0)
            //{
            //    filtri.KoloneRenditje = kolona[0].FieldName;
            //    filtri.DrejtimRenditje = kolona[0].SortOrder == DevExpress.Data.ColumnSortOrder.Ascending;
            //}
            //else
            //{
            //    filtri.KoloneRenditje = "Kodi";
            //    filtri.DrejtimRenditje = true;
            //}

            var mesazh = new DbCore.clsMesazh();
            mesazh = filtri.ruaj();
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), _idndermarje, "gvKonfigurim", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
            PercaktoTemplateMenu(_idgjuha, ASPxMenu1, _idviti, _idperdoruesi, _idndermarje);
            if (mesazh.Status)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

            cmbFiltra.Text = "";
        }

        /// <summary>
        /// eventet e menuse
        /// </summary>
        /// <param name="source">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {

        }

        /// <summary>
        /// fshin rreshtat e selektuar
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            var teDrejtaKategoria = new DbCore.DbAdmin.clsTeDrejtaRoli();
            var mesazh = new DbCore.clsMesazh();
            List<string> teFshire = new List<string>(), 
                teLidhur = new List<string>(),
                skaTeDrejta = new List<string>();
            pergjigja.Text = "";


            var rreshtat = gvKonfigurim.GetSelectedFieldValues("IdKoka");
            pergjigja.Text = "";
            
            foreach (var id in rreshtat)
            {
                var clsKoka = new DbCore.DbAdmin.clsKokaFormatImporti(Convert.ToInt32(id));

                teDrejtaKategoria.merrTeDrejtaPerKeteKomponenteDheKategori(_idperdoruesi, _idndermarje, _idviti, komponente, clsKoka.IdKategori);
                if (teDrejtaKategoria.DFsh == false)
                {
                    skaTeDrejta.Add(clsKoka.Kodi);
                    continue;
                }

                if (DbCore.DbAdmin.clsKokaFormatImporti.kaveprime(clsKoka.IdKoka))
                {
                    teLidhur.Add(clsKoka.Kodi);
                    continue;
                }

                clsKoka.IdPerdoruesi = _idperdoruesi;
                mesazh = clsKoka.fshiFormatImporti();
                if (!mesazh.Status) continue;
                HiqNgaGrida(clsKoka.IdKoka);
                teFshire.Add(clsKoka.Kodi);
            }

            //do te perdoret metoda e cls funksione per shfaqjen e mesazheve 
            string mesazhInfoGabimLidhur = "", mesazhInfoSukses = "", mesazhInfoGabimSkaTeDrejta = "";

            if (teLidhur.Count == 1)
                mesazhInfoGabimLidhur = String.Format("{0}{1}{2}", prefixMesazhNjejes, String.Join(";", teLidhur), suffixMesazhNjejesLidhurGabimi);
            else
                if (teLidhur.Count > 1)
                    mesazhInfoGabimLidhur = String.Format("{0}{1}{2}", prefixMesazhShumes, String.Join(";", teLidhur), suffixMesazhShumesLidhurGabimi);
            if (teFshire.Count == 1)
                mesazhInfoSukses = String.Format("{0}{1}{2}", prefixMesazhNjejes, String.Join(";", teFshire), suffixMesazhNjejesSuksesi);
            else
                if (teFshire.Count > 1)
                    mesazhInfoSukses = String.Format("{0}{1}{2}", prefixMesazhShumes, String.Join(";", teFshire), suffixMesazhShumesSuksesi);

            if(skaTeDrejta.Count > 0)
                mesazhInfoGabimSkaTeDrejta = $"{rm.GetString("nukKeniTeDrejtaPerTeFshire", ci)} {String.Join(";", skaTeDrejta)}";

            if (mesazhInfoGabimLidhur != "" && mesazhInfoSukses != "")
            {
                mesazhInfoGabimLidhur += lidhesMesazhi + mesazhInfoSukses;
                pergjigja.Text = mesazhInfoGabimLidhur;
                pergjigja.ClientVisible = false;
            }
            else
            {
                pergjigja.Text = mesazhInfoSukses;
                pergjigja.ClientVisible = false;
            }

            if (mesazhInfoGabimSkaTeDrejta != "")
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhInfoGabimSkaTeDrejta, pnlMesazhi);

            if (rreshtat.Count == 0)
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, STR_JuLutemZgjidhniTePaktenNjeDokument, pnlMesazhi);
            else
                if (mesazhInfoGabimLidhur != "")
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhInfoGabimLidhur, pnlMesazhi);
                else if(mesazhInfoGabimSkaTeDrejta == "")
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazhInfoSukses, pnlMesazhi);

        }

        /// <summary>
        /// heq nga grida reshtin e fshire
        /// </summary>
        /// <param name="idkoka">id e reshtit te fshire</param>
        private void HiqNgaGrida(int idkoka)
        {
            if (gvKonfigurim.DataSource != null)
            {
                var dt = (DataTable)gvKonfigurim.DataSource;
                var drs = dt.Select("IdKoka = " + idkoka);
                if (drs.Length > 1)
                    throw new Exception(STR_GABIMNdodhen2DokumentaListPageseMeTeNjejtenIdNeG);
                if (drs.Length == 0) return;
                var dr = drs[0];
                dt.Rows.Remove(dr);
                gvKonfigurim.DataSource = dt;
                gvKonfigurim.DataBind();
                dt.Dispose();
            }
            else MbushGridNgaDb();
        }

        /// <summary>
        /// kur grida ben callback
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvKonfigurim_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            if (e.CallbackName == "COLUMNMOVE" && gvKonfigurim.AllColumns[int.Parse(e.Args[0])].Width.Value == 0)
                gvKonfigurim.AllColumns[int.Parse(e.Args[0])].Width = System.Web.UI.WebControls.Unit.Percentage(3);
            if (e.CallbackName == "APPLYFILTER" && e.Args[0] == "")
            {
                var itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
                var cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
                cmbFiltra.Text = "";
                for (var i = 0; i < ((DataTable)gvKonfigurim.DataSource).Rows.Count; i++)
                    if (gvKonfigurim.GetRowLevel(i) == 0)
                        gvKonfigurim.ExpandRow(i, false);

            }
        }

        /// <summary>
        /// kur perdoruesi i ben grides callbak per te mare konfigurimin e ri
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvKonfigurim_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            var arr = e.Parameters.Split(';');
            GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), "gvKonfigurim", gvKonfigurim, cmbKonfigurimi.Text.Split(';')[0], "174", DbCore.mySessionObjects.ktheGjuhe(Session));
            if (arr.Length == 3)
            {
                if (arr[2] == "")
                {
                    gvKonfigurim.FilterExpression = "";

                    for (var i = 0; i < ((DataTable)gvKonfigurim.DataSource).Rows.Count; i++)
                        if (gvKonfigurim.GetRowLevel(i) == 0)
                            gvKonfigurim.ExpandRow(i, false);
                }
                else
                {
                    var filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    var koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvKonfigurim", komponente, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), int.Parse(cmbKonfigurimi.Value.ToString()));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        gvKonfigurim.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvKonfigurim);
                    }
                }
            }


            gvKonfigurim.Columns["#"].VisibleIndex = 0;
            gvKonfigurim.Selection.UnselectAll();
        }

        /// <summary>
        /// perdoret per te vendosur karakteristika te griden ne javascript
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvKonfigurim_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gvKonfigurim.PageIndex;
            e.Properties["cpPageRow"] = gvKonfigurim.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gvKonfigurim.VisibleRowCount;
        }

        /// <summary>
        /// perdoret per te hequr filtrimin e grides kur zgjedhim elementin bosh
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvKonfigurim_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {

        }

        /// <summary>
        /// perdoret per te shtuar menyra filtrimi
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvKonfigurim_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            if (e.Column.FieldName == "Kodi" || e.Column.FieldName == "Pershkrimi")
            {
                e.Values.Clear();
                e.AddValue("(Te gjithe)", string.Empty, "true");
                e.AddValue("Nga A-D ", string.Empty, String.Format("{0}>'A     ' and {0} <'DDDDDDD'", e.Column.FieldName));
                e.AddValue("Nga D-G ", string.Empty, String.Format("{0}>'D     ' and {0}<'GGGGGGG'", e.Column.FieldName));
                e.AddValue("Nga H-K ", string.Empty, String.Format("{0}>'H     ' and {0}<'KKKKKKK'", e.Column.FieldName));
                e.AddValue("Nga L-O ", string.Empty, String.Format("{0}>'L     ' and {0}  <'OOOOOOO'", e.Column.FieldName));
                e.AddValue("Nga P-S ", string.Empty, String.Format("{0}>'P     ' and {0}<'SSSSSSS'", e.Column.FieldName));
                e.AddValue("Nga T-W ", string.Empty, String.Format("{0}>'T     ' and {0}<'WWWWWWW'", e.Column.FieldName));
                e.AddValue("Nga X-Z ", string.Empty, String.Format("{0}>'X     ' and {0}<'ZZZZZZZ'", e.Column.FieldName));
            }
            else
            {
                e.Values.Clear();
                e.AddValue("(Te gjithe)", string.Empty, "true");
            }
        }
    }
}