using System;
using System.Collections.Generic;
using DevExpress.Web;
using System.Data;
using DbCore.DbRegjistrim;
using DbCore;
using DbCore.IMBUtils.Messages;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.DataBase;

namespace PlatinumWeb
{

    /// <summary>
    /// nderfaqja e Azhornimkf te prodhimit
    /// </summary>
    public partial class AzhornimKlientFurnitor : MyPageBase
    {
        private const string Komponente = "AzhornimKlientFurnitor.aspx";

        /// <summary>
        /// kur lodohet faqja  mbushet me te dhena dhe kontrollohen ne eshte i autorizuar perdoruesi
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!mySessionObjects.isLogedIn(Session))
                clsFunksione.logout(Session, true, "FaqePaautorizuar");

            if (mySessionObjects.ktheKodNdermarrje(Session) == null)
                Response.Redirect("Login_Ndermarrje.aspx?id=" + IdPerdoruesi);

            PercaktoTemplateMenu();

            if (!IsPostBack)
            {
                EmrateLabelave();

                ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(IdPerdoruesi, IdNdermarrja, cmbKonfigurimi,
                    Request.QueryString["vep"] == "azhornim" ? 58 : 63, rm, ci, IdGjuha);
                cmbKonfigurimi.SelectedIndex = 0;

                var konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()), IdGjuha);
                hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;

                MbushGridNgaDb();
                grid_AzhornimKF.FilterExpression = "[IdStatusDok]=1";

                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(IdNdermarrja, "grid_AzhornimKF", grid_AzhornimKF,
                    cmbKonfigurimi.Text.Split(';')[0], Request.QueryString["vep"] == "azhornim" ? "651" : "678", IdGjuha);

                if (Request.QueryString["fshi"] == "po")
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("regjisDokMesazhSukesFshirjeDokument", ci), pnlMesazhi);
                else if (Request.QueryString["ruaj"] == "po")
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("regjMagModifikimiPerfundoiMeSukses", ci), pnlMesazhi);

                var tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
            }
            else
            {
                MbushGridNgaSession();
            }

            KonfiguroGride();

            GridUtil.konfigGrideListeEMadhePaTheme(grid_AzhornimKF, Request.QueryString["vep"] == "azhornim" ? "IdAzhornimKFKoka" : "IdKoka");
            clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, IdNdermarrja, "grid_AzhornimKF", int.Parse(cmbKonfigurimi.Value.ToString()), Komponente);
            if (Request.QueryString["indexrow"] != null)
            {
                grid_AzhornimKF.FocusedRowIndex = int.Parse(Request.QueryString["indexrow"]);
            }

            AspxWebControlUtils.perkthePopUp(popFshi, rm.GetString("labelKujdes", ci), lblMsgbox, rm.GetString("labelAdministrimiMsgJeniSigurt", ci), ButtonCancel, rm.GetString("labelAnullo", ci));
            GridUtil.ToolTipButonaveMbiGride(grid_AzhornimKF, ci, rm);
            GridUtil.EmrateButonaveMbiGride(grid_AzhornimKF);
        }

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        private void PercaktoTemplateMenu()
        {
            clsToolbarConfig.percaktoTemplateMenu(IdGjuha, IdViti, IdPerdoruesi, IdNdermarrja, ASPxMenu1, clsFunksione.GetKomponente(Page.Request), this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, true, false, false, Meme);
        }

        protected void RuajKolona_Click(object sender, EventArgs e)
        {
            int idfiltri;
            int idkonf = DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(cmbKonfigurimi.Text.Split(';')[0], IdNdermarrja);

            var mesazh = GridUtil.ruajFiltra(IdNdermarrja, IdPerdoruesi, IdGjuha, "grid_AzhornimKF", Komponente, "FilterDefault", grid_AzhornimKF.FilterExpression, grid_AzhornimKF, "NrDok", idkonf, out idfiltri);//ruan filtrin e zgjedhur tek filtrat
            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                return;
            }

            mesazh = GridUtil.ruajkonfigurimgride(grid_AzhornimKF, cmbKonfigurimi.Text, IdNdermarrja, IdPerdoruesi, Request.QueryString["vep"] == "azhornim" ? 651 : 678, idfiltri, IdViti, ci, IdGjuha);
            clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, IdNdermarrja, "grid_AzhornimKF", int.Parse(cmbKonfigurimi.Value.ToString()), Komponente);

            PercaktoTemplateMenu();

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
            PercaktoTemplateMenu();
        }

        /// <summary>
        /// mbush griden me te dhena te ruajtura ne sesion
        /// </summary>
        private void MbushGridNgaSession()
        {
            DataTable tmpObject;
            var sukses = mySessionObjects.merrGrideNgaSessioni(Session, out tmpObject);
            if (!sukses)
                MbushGridNgaDb();
            else
            {
                grid_AzhornimKF.DataSource = tmpObject;
                grid_AzhornimKF.DataBind();
                tmpObject.Dispose();
            }
        }

        /// <summary>
        /// mbush griden me te dhena nga db
        /// </summary>
        private void MbushGridNgaDb()
        {
            var dt = Request.QueryString["vep"] == "azhornim"
                ? colAzhornimKFKoka.MerrAzhornimKfdt(IdNdermarrjeVit, IdPerdoruesi)
                : colKokaMbylljeKF.MerrMbylljeKfdt(IdNdermarrjeVit, IdPerdoruesi);
            mySessionObjects.ruajGrideNeSession(Session, dt);
            grid_AzhornimKF.DataSource = dt;
            grid_AzhornimKF.DataBind();
            dt.Dispose();
        }

        /// <summary>
        /// konfiguron griden
        /// </summary>
        private void KonfiguroGride()
        {
            KonfigurimComboGride.ShtoStatus(grid_AzhornimKF, rm, ci);
            grid_AzhornimKF.Columns["#"].VisibleIndex = 0;
        }

        /// <summary>
        /// kur grida ben databound per ti shtuar kolonen e selektimit
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void grid_AzhornimKF_DataBound(object sender, EventArgs e)
        {
            if (grid_AzhornimKF.Columns["#"] != null) return;

            var check = new GridViewCommandColumn("#") { ShowSelectCheckbox = true, Width = System.Web.UI.WebControls.Unit.Percentage(2) };
            grid_AzhornimKF.Settings.ShowFilterRow = true;
            grid_AzhornimKF.Settings.ShowHeaderFilterButton = true;
            grid_AzhornimKF.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
            grid_AzhornimKF.Settings.ShowFilterRowMenu = true;
            grid_AzhornimKF.Columns.Add(check);
            grid_AzhornimKF.Settings.ShowGroupPanel = true;
            grid_AzhornimKF.KeyFieldName = Request.QueryString["vep"] == "azhornim" ? "IdAzhornimKFKoka" : "IdKoka";
            grid_AzhornimKF.SettingsBehavior.AllowSelectByRowClick = true;
            grid_AzhornimKF.SettingsBehavior.AllowFocusedRow = true;
        }

        /// <summary>
        /// fshin filtrin e zgjedhur dhe aplikuar mbi gride, fshirje nga DB ku ai eshte ruajtur, dhe jo pastrimi i grides nga aplikimi i filtrit
        /// per kete do sherbeje Clear Filter ne fund te grides
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        public void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
            var itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            var cmbFiltra = ((MenuFilter)itemButton.Template).FindControl("btnFiltra") as ASPxComboBox;

            var filtra = new DbCore.DbAdmin.clsFiltraGrida();
            var koka = new DbCore.DbAdmin.clsGridaKoka(IdGjuha, "grid_AzhornimKF", Komponente, IdNdermarrja, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, IdNdermarrja, koka.IdGridaKoka);

            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = IdPerdoruesi;
                var mesazh = filtra.fshi();
                clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, IdNdermarrja, "grid_AzhornimKF", int.Parse(cmbKonfigurimi.Value.ToString()), Komponente);
                PercaktoTemplateMenu();

                if (mesazh.Status)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                grid_AzhornimKF.FilterExpression = " [IdStatusDok]=1 ";
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
            var koka = new DbCore.DbAdmin.clsGridaKoka(IdGjuha, "grid_AzhornimKF", clsFunksione.GetKomponente(Page.Request), IdNdermarrja, int.Parse(cmbKonfigurimi.Value.ToString()));
            //var kolona = grid_AzhornimKF.GetSortedColumns();

            var filtri = new DbCore.DbAdmin.clsFiltraGrida
            {
                FiltraKodi = cmbFiltra.Text,
                FiltraShenime = cmbFiltra.Text,
                FiltraUniversal = false,
                GridaKokaId = koka.IdGridaKoka,
                FiltraVlera = grid_AzhornimKF.FilterExpression,
                IdPerdoruesi = IdPerdoruesi,
                IdNdermarje = IdNdermarrja,
                IdStatusDok = 1
            };
            //if (kolona.Count > 0)
            //{
            //    filtri.KoloneRenditje = kolona[0].FieldName;
            //    filtri.DrejtimRenditje = kolona[0].SortOrder == DevExpress.Data.ColumnSortOrder.Ascending;
            //}
            //else
            //{
            //    filtri.KoloneRenditje = "NrDok";
            //    filtri.DrejtimRenditje = true;
            //}
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("NrDok", grid_AzhornimKF);
            var mesazh = filtri.ruaj();
            clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, IdNdermarrja, "grid_AzhornimKF", int.Parse(cmbKonfigurimi.Value.ToString()), Komponente);
            PercaktoTemplateMenu();

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
            List<string> teFshire = new List<string>(), teLidhur = new List<string>(), periudheKycur = new List<string>(), closedPeriod = new List<string>();
            var rreshtat = grid_AzhornimKF.GetSelectedFieldValues(Request.QueryString["vep"] == "azhornim" ? "IdAzhornimKFKoka" : "IdKoka");
            pergjigja.Text = "";

            var dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            foreach (var id in rreshtat)
            {
                bool lidhur;
                clsMesazh mesazh;
                if (Request.QueryString["vep"] == "azhornim")
                {
                    var clsKoka = new clsAzhornimKFKoka(Convert.ToInt32(id));

                    lidhur = dbAdmin.eshteDokumentiILidhur(clsKoka.IdAzhornimKfKoka, clsKoka.IdNivel, "T_AZHORNIMKFKOKA", "IDAZHORNIMKFKOKA");
                    var autorizimet = clsAzhornimKFKoka.KaAutorizime(clsKoka.IdAzhornimKfKoka, IdPerdoruesi);
                    if (!autorizimet)
                        lidhur = true;

                    if (lidhur)
                    {
                        teLidhur.Add(clsKoka.NrDok);
                        continue;
                    }

                    if (clsKoka.IdStatusDok == 2)
                        continue;

                    var ekycur = DbCore.DbAdmin.clsPeriudhaKontabel.eshteKycurPeriudheSipasDateDheNdermarrjes(clsKoka.DateDok, IdNdermarrja);
                    if (ekycur)
                    {
                        periudheKycur.Add(clsKoka.NrDok);
                        continue;
                    }

                    if (DbCore.MbylljePeriudhe.PeriodClosing.IsPeriodClosed(clsKoka.DateDok, MyConnectionsManager.GetSelectedConNameServer(), IdNdermarrja, KategoriDokumenti.AzhornimKlientFurnitor, clsKoka.IdKonfigAmbjente))
                    {
                        closedPeriod.Add(clsKoka.NrDok);
                        continue;
                    }

                    clsKoka.IdPerdorues = IdPerdoruesi;
                    mesazh = clsKoka.Fshi();

                    if (mesazh.Status)
                    {
                        HiqNgaGrida(clsKoka.IdAzhornimKfKoka);
                        teFshire.Add(clsKoka.NrDok);
                    }
                }
                else
                {
                    var clsKoka = new clsKokaMbylljeKF(Convert.ToInt32(id));

                    lidhur = dbAdmin.eshteDokumentiILidhur(clsKoka.IdKoka, clsKoka.IdNivel, "T_KOKAMBYLLJEKF", "IDKOKA");
                    var autorizimet = clsKokaMbylljeKF.KaAutorizime(clsKoka.IdKoka, IdPerdoruesi);
                    if (!autorizimet)
                        lidhur = true;

                    if (lidhur)
                    {
                        teLidhur.Add(clsKoka.NrDok);
                        continue;
                    }

                    if (clsKoka.IdStatusDok == 2)
                        continue;

                    var ekycur = DbCore.DbAdmin.clsPeriudhaKontabel.eshteKycurPeriudheSipasDateDheNdermarrjes(clsKoka.DateDok, IdNdermarrja);

                    if (ekycur)
                    {
                        periudheKycur.Add(clsKoka.NrDok);
                        continue;
                    }

                    if (DbCore.MbylljePeriudhe.PeriodClosing.IsPeriodClosed(clsKoka.DateDok, MyConnectionsManager.GetSelectedConNameServer(), IdNdermarrja, KategoriDokumenti.AzhornimKlientFurnitor, clsKoka.IdKonfigAmbjente))
                    {
                        closedPeriod.Add(clsKoka.NrDok);
                        continue;
                    }

                    clsKoka.IdPerdorues = IdPerdoruesi;
                    mesazh = clsKoka.Fshi();

                    if (mesazh.Status)
                    {
                        HiqNgaGrida(clsKoka.IdKoka);
                        teFshire.Add(clsKoka.NrDok);
                    }
                }
            }
            dbAdmin.Dispose();

            string mesazhInfoGabimLidhur = "", mesazhInfoGabimPeridheKycur = "", mesazhInfoSukses = "", mesazhClosedPeriod ="";

            if (teLidhur.Count == 1)
                mesazhInfoGabimLidhur = string.Format("{0}{1}{2}", rm.GetString("regjisDokPrefixMesazhNjejes", ci), string.Join(";", teLidhur), rm.GetString("regjMagSuffixMesazhNjejesLidhurGabimi", ci));
            else
                if (teLidhur.Count > 1)
                mesazhInfoGabimLidhur = string.Format("{0}{1}{2}", rm.GetString("regjisDokprefixMesazhShumes", ci), string.Join(";", teLidhur), rm.GetString("regjMagSuffixMesazhShumesLidhurGabimi", ci));
            if (periudheKycur.Count == 1)
                mesazhInfoGabimPeridheKycur = string.Format("{0}{1}{2}", rm.GetString("regjisDokPrefixMesazhNjejes", ci), string.Join(";", periudheKycur), rm.GetString("suffixMesazhNjejesPeriudheKycurGabimi", ci));
            else
                if (periudheKycur.Count > 1)
                mesazhInfoGabimPeridheKycur = string.Format("{0}{1}{2}", rm.GetString("regjisDokprefixMesazhShumes", ci), string.Join(";", periudheKycur), rm.GetString("suffixMesazhShumesPeriudheKycurGabimi", ci));

            if (closedPeriod.Count == 1)
                mesazhClosedPeriod = String.Format("{0}{1}{2}", rm.GetString("regjisDokPrefixMesazhNjejes", ci), String.Join(", ", closedPeriod), rm.GetString("suffixMesazhClosedPeriodNjejes", ci));
            else if (closedPeriod.Count > 1)
                mesazhClosedPeriod = String.Format("{0}{1}{2}", rm.GetString("regjisDokPrefixMesazhNjejes", ci), String.Join(", ", closedPeriod), rm.GetString("suffixMesazhClosedPeriodShumes", ci));

            if (teFshire.Count == 1)
                mesazhInfoSukses = string.Format("{0}{1}{2}", rm.GetString("regjisDokPrefixMesazhNjejes", ci), string.Join(";", teFshire), rm.GetString("regjMagSuffixMesazhNjejesSuksesi", ci));
            else
                if (teFshire.Count > 1)
                mesazhInfoSukses = string.Format("{0}{1}{2}", rm.GetString("regjisDokprefixMesazhShumes", ci), string.Join(";", teFshire), rm.GetString("regjMagSuffixMesazhShumesSuksesi", ci));

            mesazhInfoGabimLidhur += mesazhInfoGabimPeridheKycur + mesazhClosedPeriod;

            if (!string.IsNullOrEmpty(mesazhInfoGabimLidhur) && !string.IsNullOrEmpty(mesazhInfoSukses))
            {
                mesazhInfoGabimLidhur += rm.GetString("lidhesMesazhi", ci) + mesazhInfoSukses;
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

            else if (!string.IsNullOrEmpty(mesazhInfoGabimLidhur))
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhInfoGabimLidhur, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazhInfoSukses, pnlMesazhi);
        }

        /// <summary>
        /// heq nga grida reshtin e fshire
        /// </summary>
        /// <param name="idkoka">id e reshtit te fshire</param>String.IsNullOrEmpty(arr[2])
        private void HiqNgaGrida(int idkoka)
        {
            if (grid_AzhornimKF.DataSource != null)
            {
                var dt = (DataTable)grid_AzhornimKF.DataSource;
                var drs = Request.QueryString["vep"] == "azhornim" ? dt.Select("IdAzhornimKFKoka = " + idkoka) : dt.Select("IdKoka = " + idkoka);

                if (drs.Length > 1)
                    throw new MyException(MessagesResource.Messages["regjisDokNdodhen2DokMeTeNjejtenID"]);

                if (drs.Length == 0) return;

                dt.Rows.Remove(drs[0]);
                grid_AzhornimKF.DataSource = dt;
                grid_AzhornimKF.DataBind();
                dt.Dispose();
            }
            else
                MbushGridNgaDb();
        }

        /// <summary>
        /// kur grida ben callback
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void grid_AzhornimKF_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            if (e.CallbackName == "COLUMNMOVE" && grid_AzhornimKF.AllColumns[int.Parse(e.Args[0])].Width.Value == 0)
                grid_AzhornimKF.AllColumns[int.Parse(e.Args[0])].Width = System.Web.UI.WebControls.Unit.Percentage(3);

            if (e.CallbackName == "APPLYFILTER" && string.IsNullOrEmpty(e.Args[0]))
            {
                var itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
                var cmbFiltra = ((MenuFilter)itemButton.Template).FindControl("btnFiltra") as ASPxComboBox;
                cmbFiltra.Text = "";
            }

            GridUtil.EmrateButonaveMbiGride(grid_AzhornimKF);
            GridUtil.ToolTipButonaveMbiGride(grid_AzhornimKF, ci, rm);
        }

        /// <summary>
        /// kur perdoruesi i ben grides callbak per te mare konfigurimin e ri
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void grid_AzhornimKF_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            var arr = e.Parameters.Split(';');
            GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(IdNdermarrja, "grid_AzhornimKF", grid_AzhornimKF,
                cmbKonfigurimi.Text.Split(';')[0], Request.QueryString["vep"] == "azhornim" ? "651" : "678", IdGjuha);
            if (arr.Length == 3)
            {
                if (string.IsNullOrEmpty(arr[2]))
                    grid_AzhornimKF.FilterExpression = " [IdStatusDok]=1 ";
                else
                {
                    var filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    var koka = new DbCore.DbAdmin.clsGridaKoka(IdGjuha, "grid_AzhornimKF", clsFunksione.GetKomponente(Page.Request), IdNdermarrja, int.Parse(cmbKonfigurimi.Value.ToString()));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], IdNdermarrja, koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        grid_AzhornimKF.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, grid_AzhornimKF);
                    }
                }
            }

            grid_AzhornimKF.Selection.UnselectAll();
        }

        /// <summary>
        /// perdoret per te vendosur karakteristika te griden ne javascript
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void grid_AzhornimKF_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = grid_AzhornimKF.PageIndex;
            e.Properties["cpPageRow"] = grid_AzhornimKF.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = grid_AzhornimKF.VisibleRowCount;
        }

        /// <summary>
        /// perdoret per te hequr filtrimin e grides kur zgjedhim elementin bosh
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void grid_AzhornimKF_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {

        }

        /// <summary>
        /// perdoret per te shtuar menyra filtrimi
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void grid_AzhornimKF_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            var teGjithe = MessagesResource.Messages["GridHeaderFilterFillItemTeGjithe"];
            var nga = MessagesResource.Messages["GridHeaderFilterFillItemNga"];
            if (e.Column.FieldName == "NrDok")
            {
                e.Values.Clear();
                e.AddValue(teGjithe, string.Empty, "true");
                e.AddValue(nga + " A-D ", string.Empty, string.Format("{0}>'A     ' and {0} <'DDDDDDD'", e.Column.FieldName));
                e.AddValue(nga + " D-G ", string.Empty, string.Format("{0}>'D     ' and {0}<'GGGGGGG'", e.Column.FieldName));
                e.AddValue(nga + " H-K ", string.Empty, string.Format("{0}>'H     ' and {0}<'KKKKKKK'", e.Column.FieldName));
                e.AddValue(nga + " L-O ", string.Empty, string.Format("{0}>'L     ' and {0}  <'OOOOOOO'", e.Column.FieldName));
                e.AddValue(nga + " P-S ", string.Empty, string.Format("{0}>'P     ' and {0}<'SSSSSSS'", e.Column.FieldName));
                e.AddValue(nga + " T-W ", string.Empty, string.Format("{0}>'T     ' and {0}<'WWWWWWW'", e.Column.FieldName));
                e.AddValue(nga + " X-Z ", string.Empty, string.Format("{0}>'X     ' and {0}<'ZZZZZZZ'", e.Column.FieldName));
            }
            else
            {
                e.Values.Clear();
                e.AddValue(teGjithe, string.Empty, "true");
            }
        }

        /// <summary>
        /// Vendos emrat e label ne baze te gjuhes se perdoruesit
        /// </summary>
        private void EmrateLabelave()
        {
            konfigurimi_Label.Text = MessagesResource.Messages["lblLloji"];
        }
    }
}
