using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using DbCore;
using DbCore.DbAdmin;
using DbCore.DbRegjistrim;
using DbCore.IMBUtils;
using DbCore.IMBUtils.Messages;
using DevExpress.Web;
using DevExpress.Web.Data;
using NLog;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    /// <summary>
    /// Faqja e shtimit te politikave
    /// </summary>
    public partial class Shto_PolitikeKartaKlienti : MyPageBase
    {
        private const string EmerKomponente = "Shto_PolitikeKartaKlienti.aspx";

        private bool _isValidKategori;
        private bool _lupe;

        /// <summary>
        /// thirret kur faqja lodohet. Ne te behet kontrolli nese perdoruesi eshte i loguar ne sistem  dhe nqs jo ridrejtohet tek forma e logimit
        /// thirret inicializimi i konfigurimeve fillestare te faqes
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e"> argumenti</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!mySessionObjects.isLogedIn(Session))
            {
                Response.Redirect(Paths.defaultLoginPath);
            }

            if (mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + IdPerdoruesi);
            }

            ShtoMenuControlsDheMsgFrame();

            if (!Page.IsPostBack)
            {
                ShtoVleraTePergjithshmeNeHfState();
                EmrateTabeve();
                hfState.Set("msgKategoriPolitkashRuajtje", MessagesResource.Messages["msgKategoriPolitkashRuajtje"]);
                hfState.Set("komponente", EmerKomponente);

                _lupe = !string.IsNullOrEmpty(Request.QueryString["lupe"]) && Request.QueryString["lupe"] == "true";
                hfState.Set("lupe", _lupe);

                ASPxPageControl1.ActiveTabIndex = 0;
                KonfiguroVleraFillestare();
                MbushComboBoxFiltra();
                MbushGridPolitikeNgaDb();
                MbushGridKategoriNgaDb(0);
                KonfiguroGridePolitika();
                KonfiguroGrideKategoriPike();

                var tedrejtaInfo = new clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();

                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, EmerKomponente);
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);

                GridUtil.konfigGrideListeEMadhePaTheme(ASPxGridView_Politikat, "IdPolitike");
            }
            else
            {
                MbushGridpolitikeNgaSession();
                int.TryParse(hfId.Value, out var idPolitike);
                MbushGridKategoriNgaSession(idPolitike);
                KonfiguroGridePolitika();
                KonfiguroGrideKategoriPike();
                _lupe = (bool)hfState["lupe"];
            }
            
            MbushComboBoxFiltra();
            GridUtil.ToolTipButonaveMbiGride(ASPxGridView_Politikat, ci, rm);
        }

        /// <summary>
        /// Vendos emrat e tabeve ne baze te gjuhes se perdoruesit
        /// </summary>
        private void EmrateTabeve()
        {
            ASPxPageControl1.TabPages[0].Text = MessagesResource.Messages["labelRaportTePergjithshme"];
            ASPxPageControl1.TabPages[1].Text = MessagesResource.Messages["labelAdministrimiInformacion"];
            lblKodi.Text = MessagesResource.Messages["lblKodi"];
            lblLloji.Text = MessagesResource.Messages["lblLlojPolitike"];
            chkPike.Text = MessagesResource.Messages["lblMePike"];
        }

        /// <summary>
        /// konfiguron vlerat fillestare
        /// </summary>
        private void KonfiguroVleraFillestare()
        {
            ASPxPageControl1.ActiveTabIndex = 0;
        }

        /// <summary>
        /// perdoret per te fshire rreshtat e zgjedhur te bankave nqs perdoruesi konfirmon fshirjen
        /// </summary> 
        /// <param name="sender"> derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            var rreshtat = ASPxPageControl1.ActiveTabIndex == 0
                ? ASPxGridView_Politikat.GetSelectedFieldValues("IdPolitike")
                : new List<object> { hfId.Value };

            if (rreshtat.Count == 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(_menuInfo, MessagesResource.Messages["PolitikeKartaKlienti.ZgjidhniNjePolitike"], _pnlMesazhi);
                return;
            }

            List<string> teFshire = new List<string>(), tePaFshire = new List<string>();

            var dbRegjistrim = new clsDatabaseRegjistrim();
            foreach (var id in rreshtat)
            {
                var politik = new clsPolitikeKarta(Convert.ToInt32(id));

                bool lidhur = dbRegjistrim.MosLejoFshirjePolitike(politik.IdPolitike, politik.IdNdermarrje);
                if (lidhur)
                {
                    tePaFshire.Add(politik.Kodi);
                    continue;
                }

                politik.IdPerdoruesi = IdPerdoruesi;

                var mesazh = politik.Fshi(politik.IdPerdoruesi);
                if (politik.IdPolitike == 0)
                    continue;

                if (mesazh.Status)
                {
                    HiqNgaGrida(politik.IdPolitike);
                    teFshire.Add(politik.Kodi);
                    ASPxPageControl1.ActiveTabIndex = 0;
                    hfStatusi.Value = "true";
                    ASPxGridView_Politikat.FilterExpression = "";
                }
            }
            dbRegjistrim.Dispose();

            string mesazhInfoGabim = "", mesazhInfoSukses = "";

            if (tePaFshire.Count == 1)
                mesazhInfoGabim = $"{MessagesResource.Messages["PolitikeKartaKlienti.PolitikaMeKod"]}{string.Join(";", tePaFshire)} {MessagesResource.Messages["PolitikeKartaKlienti.PolitikeLidhur"]}";
            else if (tePaFshire.Count > 1)
                mesazhInfoGabim = $"{MessagesResource.Messages["PolitikeKartaKlienti.PolitikatMeKod"]}{string.Join(";", tePaFshire)} {MessagesResource.Messages["PolitikeKartaKlienti.PolitikaLidhur"]}";

            if (teFshire.Count == 1)
                mesazhInfoSukses = $"{MessagesResource.Messages["PolitikeKartaKlienti.PolitikaMeKod"]}{string.Join(";", teFshire)} {MessagesResource.Messages["PolitikeKartaKlienti.PolitikeFshirjeSukses"]}";
            else if (teFshire.Count > 1)
                mesazhInfoSukses = $"{MessagesResource.Messages["PolitikeKartaKlienti.PolitikatMeKod"]}{string.Join(";", teFshire)} {MessagesResource.Messages["PolitikeKartaKlienti.PolitikaFshirjeSukses"]}";

            if (mesazhInfoGabim != "" && mesazhInfoSukses != "")
                mesazhInfoGabim += MessagesResource.Messages["PolitikeKartaKlienti.LidhesKurse"] + mesazhInfoSukses;

            if (mesazhInfoGabim != "")
                clsMenuInfo.ShtoMesazhGabimi(_menuInfo, mesazhInfoGabim, _pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhSuksesi(_menuInfo, mesazhInfoSukses, _pnlMesazhi);
        }

        /// <summary>
        /// ruan politiket 
        /// </summary>
        private void RuajPolitike()
        {
            if (Page.IsValid == false)
                return;
            try
            {
                var tedrejtaInfo = new clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, EmerKomponente);

                if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim")
                {
                    if (!tedrejtaInfo.DShtim)
                    {
                        throw new MyException(MessagesResource.Messages["PolitikeKartaKlienti.NukKeniTeDrejta"]);
                    }

                    var politikeKarta = KrijoPolitikeKarte();
                    politikeKarta.Ruaj();
                    clsMenuInfo.ShtoMesazhSuksesi(_menuInfo, MessagesResource.Messages["PolitikeKartaKlienti.RuajtjeSukses"], _pnlMesazhi);
                    ShtoNeGrid(politikeKarta.IdPolitike);
                    hfStatusi.Value = "true";
                }
                else
                {
                    if (!tedrejtaInfo.DMod)
                    {
                        throw new MyException(MessagesResource.Messages["PolitikeKartaKlienti.NukKeniTeDrejta"]);
                    }

                    var politikeKarta = KrijoPolitikeKarte();
                    politikeKarta.Modifiko();
                    clsMenuInfo.ShtoMesazhSuksesi(_menuInfo, MessagesResource.Messages["PolitikeKartaKlienti.ModifikimSukses"], _pnlMesazhi);
                    ModifikoNeGrid(politikeKarta.IdPolitike);
                    hfStatusi.Value = "true";
                }
            }
            catch (MyException ex)
            {
                LogManager.GetCurrentClassLogger().Error(ex.Message);
                clsMenuInfo.ShtoMesazhGabimi(_menuInfo, ex.Message, _pnlMesazhi);
                hfStatusi.Value = "false";
            }
            catch (Exception e)
            {
                LogManager.GetCurrentClassLogger().Error(e.Message);
                clsMenuInfo.ShtoMesazhGabimi(_menuInfo, MessagesResource.Messages["KartaKlienti.RuajtjaGabim"], _pnlMesazhi);
                hfStatusi.Value = "false";
            }
        }

        private clsPolitikeKarta KrijoPolitikeKarte()
        {
            int idPolitike = 0;
            int lloji = 0;
            double pike = 0;
            bool eshteShtim;
            if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim")
                eshteShtim = true;
            else
            {
                eshteShtim = false;
                int.TryParse(hfId.Value, out idPolitike);
            }

            if (chkZbritje.Checked)
                lloji = 0;
            if (chkPike.Checked)
                lloji = 1;
            if (chkPike.Checked && chkZbritje.Checked)
                lloji = 2;

            if (txtVleraPike.Text != "")
                double.TryParse(txtVleraPike.Text, out pike);

            var kategorite = chkPike.Checked
                ? mySessionObjects.merrObjectNgaSesioni(Session, "kategoriPikesh") as colTrupiPolitikeKarta
                : new colTrupiPolitikeKarta();

            return new clsPolitikeKarta(idPolitike, txtKodi.Text, lloji, pike, IdPerdoruesi, IdNdermarrja, kategorite, eshteShtim);
        }

        #region Menu

        /// <summary>
        /// mbush combon e filtrave
        /// </summary>
        private void MbushComboBoxFiltra()
        {
            clsToolbarConfig.mbushComboBoxFiltraMeValFieldTextField(IdGjuha, IdNdermarrja, "ASPxGridView_Politikat", EmerKomponente, "IdFiltra", "FiltraShenime", 1);
        }

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        protected void PercaktoTemplateMenu(ASPxMenu menu, ASPxMenu menuInfo)
        {
            clsToolbarConfig.percaktoTemplateMenu(IdGjuha, IdViti, IdPerdoruesi, IdNdermarrja, menu, EmerKomponente, this, menuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, hfShtimModifikim.Value != "modifikim", false, false, Meme);
            if (_lupe)
            {
                menu.Items.FindByName("OK").Visible = true;
                menu.Items.FindByName("Anullo").Visible = true;
                menu.Items.FindByName("Modifiko").Visible = false;
                menu.Items.FindByName("Fshi").Visible = false;
                menu.Items.FindByName("Shto").Visible = false;
                menu.Items.FindByName("Ruaj").Visible = false;
                menu.Items.FindByName("TemplatedItemFrame").Visible = false;
                menu.Items.FindByName("TemplatedItemFilter").Visible = false;
                menu.Items.FindByName("Anullo").Text = MessagesResource.Messages["MenuItemMbyll"];
            }
            else
            {
                menu.Items.FindByName("OK").Visible = false;
                menu.Items.FindByName("Anullo").Visible = false;
            }
        }

        protected void RuajKolona_Click(object sender, EventArgs e)
        {
            var mesazh = GridUtil.ruajFiltra(IdNdermarrja, IdPerdoruesi, IdGjuha, "ASPxGridView_Politikat", EmerKomponente, "FilterDefault", ASPxGridView_Politikat.FilterExpression, ASPxGridView_Politikat, "Kodi", 1, out var idfiltri);//ruan filtrin e zgjedhur tek filtrat
            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(_menuInfo, mesazh.PershkrimMesazhi, _pnlMesazhi);
                return;
            }

            mesazh = GridUtil.ruajkonfigurimgride(ASPxGridView_Politikat, "", IdNdermarrja, IdPerdoruesi, 2017, idfiltri, IdViti, ci, IdGjuha);/// ruan konfigurimin e grides dhe filtrin

            clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, IdNdermarrja, "ASPxGridView_Politikat", 1, EmerKomponente); //int.Parse(cmbKonfigurimi.Value.ToString())

            PercaktoTemplateMenu(_menu, _menuInfo);

            if (mesazh.Status)
                clsMenuInfo.ShtoMesazhSuksesi(_menuInfo, mesazh.PershkrimMesazhi, _pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhGabimi(_menuInfo, mesazh.PershkrimMesazhi, _pnlMesazhi);
        }

        /// <summary>
        /// fshin filtrin e zgjedhur dhe aplikuar mbi gride, fshirje nga DB ku ai eshte ruajtur, dhe jo pastrimi i grides nga aplikimi i filtrit
        /// per kete do sherbeje Clear Filter ne fund te grides
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        public void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
            menu_msg_Frame.FshiFilter(ASPxGridView_Politikat, EmerKomponente, int.Parse(cmbKonfigurimi.Value.ToString()), ref hfStatusi);
        }

        /// <summary>
        /// sherben per te ruajtur filtrin e zgjedhur
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
            menu_msg_Frame.RuajFilter(ASPxGridView_Politikat, EmerKomponente, int.Parse(cmbKonfigurimi.Value.ToString()), ref hfStatusi);
        }

        protected void Menu_ItemClick(object sender, MenuItemEventArgs e)
        {
            if (e.Item.Name == "Ruaj")
            {
                Page.Validate("entries");
                RuajPolitike();
            }
        }

        #endregion

        #region Grid Politika

        private void KonfiguroGridePolitika()
        {
            GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(IdNdermarrja, "ASPxGridView_Politikat", ASPxGridView_Politikat, "", "2018", IdGjuha);
            ASPxGridView_Politikat.Columns["#"].VisibleIndex = 0;
        }

        /// <summary>
        /// heq nga grida rreshtat e fshire
        /// </summary>
        ///<param name="idPolitike">Identifikuesi i politikes.</param>
        private void HiqNgaGrida(int idPolitike)
        {
            if (ASPxGridView_Politikat.DataSource != null)
            {
                var dt = (DataTable)ASPxGridView_Politikat.DataSource;
                var drs = dt.Select($"IdPolitike = '{idPolitike}'");

                if (drs.Length == 0)
                    return;

                dt.Rows.Remove(drs[0]);
                ASPxGridView_Politikat.DataBind();

            }
            else
                MbushGridPolitikeNgaDb();
        }

        /// <summary>
        /// shton nte gride rreshtat e rinj
        /// </summary>
        /// <param name="idpolitike">Identifikuesi i politikes.</param>
        private void ShtoNeGrid(int idpolitike)
        {
            if (ASPxGridView_Politikat.DataSource != null)
            {
                var dt = (DataTable)ASPxGridView_Politikat.DataSource;

                var newArtDr = clsPolitikeKarta.MerrPolitikeSipasId(idpolitike);
                if (newArtDr != null)
                    dt.ImportRow(newArtDr);
                else
                    MbushGridPolitikeNgaDb();
            }
            else
                MbushGridPolitikeNgaDb();
        }

        /// <summary>
        /// modifikon ne gride komponenten e modifikuar
        /// </summary>
        /// <param name="idpolitike">Identifikuesi i politikes.</param>
        private void ModifikoNeGrid(int idpolitike)
        {
            if (ASPxGridView_Politikat.DataSource != null)
            {
                var dt = (DataTable)ASPxGridView_Politikat.DataSource;
                var drs = dt.Select("IdPolitike = " + idpolitike);

                if (drs.Length == 0)
                    return;

                var newArtDr = clsPolitikeKarta.MerrPolitikeSipasId(idpolitike);
                if (newArtDr != null)
                {
                    drs[0].ItemArray = newArtDr.ItemArray;
                }
                else
                    MbushGridPolitikeNgaDb();
            }
            else
                MbushGridPolitikeNgaDb();
        }

        /// <summary>
        /// mbush griden nga sesioni
        /// </summary>
        private void MbushGridpolitikeNgaSession()
        {
            var sukses = mySessionObjects.merrGrideNgaSessioni(EmerKomponente, Session, out DataTable tmpObject);
            if (!sukses)
                MbushGridPolitikeNgaDb();
            else
            {
                ASPxGridView_Politikat.DataSource = tmpObject;
                ASPxGridView_Politikat.DataBind();
                tmpObject.Dispose();
            }
        }

        /// <summary>
        /// mbush griden nga databaza
        /// </summary>
        private void MbushGridPolitikeNgaDb()
        {
            var dt = clsPolitikeKarta.MerrPolitikeKarteSipasNdermarrjes(IdNdermarrja);
            mySessionObjects.ruajGrideNeSession(EmerKomponente, Session, dt);
            ASPxGridView_Politikat.DataSource = dt;
            ASPxGridView_Politikat.DataBind();
            dt.Dispose();
        }

        /// <summary>
        /// cakton properti ne javascript
        /// </summary>
        /// <param name="sender">derguesi </param>
        /// <param name="e">argumentat</param>
        protected void ASPxGridView_Politikat_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = ASPxGridView_Politikat.PageIndex;
            e.Properties["cpPageRow"] = ASPxGridView_Politikat.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = ASPxGridView_Politikat.VisibleRowCount;
        }

        /// <summary>
        /// kur grida ben callback
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxGridView_Politikat_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            ASPxGridView_Politikat.Selection.UnselectAll();
        }

        /// <summary>
        /// perdoret per te shtuar kolonen e selektimit tek grida dhe per te vendosur disa karakteristika te grides
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void ASPxGridView_Politikat_DataBound(object sender, EventArgs e)
        {
            GridUtil.GridDataBound(sender, e, ASPxGridView_Politikat, "IdPolitike");
        }

        /// <summary>
        /// perdoret ne rastet kur grida ben callback per ta rimbushur griden
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void ASPxGridView_Politikat_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            if (e.CallbackName == "COLUMNMOVE" && ASPxGridView_Politikat.AllColumns[int.Parse(e.Args[0])].Width.Value == 0)
                ASPxGridView_Politikat.AllColumns[int.Parse(e.Args[0])].Width = Unit.Percentage(3);

            if (e.CallbackName == "APPLYFILTER" && e.Args[0] == "")
            {
                var itemButton = _menu.Items.FindByName("TemplatedItemFilter");
                var cmbFiltra = ((MenuFilter)itemButton.Template).FindControl("btnFiltra") as ASPxComboBox;
                cmbFiltra.Text = "";
            }

            GridUtil.ToolTipButonaveMbiGride(ASPxGridView_Politikat, ci, rm);
        }

        /// <summary>
        /// perdoret per te vendosur filtrat tek headeri i grides
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxGridView_Politikat_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            GridUtil.GridHeaderFilterFillItem(sender, e, rm, ci, "Emri", "Kodi");
        }

        #endregion

        #region Grid KategoriPike

        private void KonfiguroGrideKategoriPike()
        {
            GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(IdNdermarrja, "ASPxGridView_KategoriPike", ASPxGridView_KategoriPike, "", "2018", IdGjuha);
            GridUtil.konfiguroGrideListeEvogelPaTheme(ASPxGridView_KategoriPike, "IdKategoria");
            ASPxGridView_KategoriPike.SettingsBehavior.AllowSelectByRowClick = true;
            ASPxGridView_KategoriPike.Settings.ShowGroupPanel = false;
            ASPxGridView_KategoriPike.Settings.ShowHeaderFilterButton = false;
            ASPxGridView_KategoriPike.Settings.ShowFilterBar = GridViewStatusBarMode.Hidden;
            ASPxGridView_KategoriPike.Settings.ShowFilterRow = false;
            ASPxGridView_KategoriPike.Settings.ShowFilterRowMenu = false;
        }

        private bool ValidoKategori(int idPolitike)
        {
            if (idPolitike == 0) return true; //eshte shtim politike e re
            return !clsPolitikeKarta.EkzistonPolitikeLidhurMeKarte(idPolitike, IdNdermarrja);
        }

        protected void ASPxGridView_KategoriPike_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            var shfaqKategori = false;

            var arr = e.Parameters.Split(';');
            if (arr.Length == 1)
            {
                bool.TryParse(arr[0], out shfaqKategori);
            }

            if (shfaqKategori)
            {
                int.TryParse(hfId.Value, out var idPolitike);
                MbushGridKategoriNgaDb(idPolitike);
            }
            else
                MbushGridKategoriNgaDb(0);
        }

        protected void ASPxGridView_KategoriPike_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = ASPxGridView_KategoriPike.PageIndex;
            e.Properties["cpPageRow"] = ASPxGridView_KategoriPike.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = ASPxGridView_KategoriPike.VisibleRowCount;
        }

        protected void ASPxGridView_KategoriPike_DataBound(object sender, EventArgs e)
        {
            ASPxGridView_KategoriPike.KeyFieldName = "IdKategoria";
            ASPxGridView_KategoriPike.SettingsBehavior.AllowSelectByRowClick = true;
            ASPxGridView_KategoriPike.SettingsBehavior.AllowFocusedRow = true;
        }

        protected void ASPxGridView_KategoriPike_RowInserting(object sender, ASPxDataInsertingEventArgs e)
        {
            int.TryParse(hfId.Value, out var idPolitike);
            var kategorite = mySessionObjects.merrObjectNgaSesioni(Session, "kategoriPikesh") as colTrupiPolitikeKarta ?? new colTrupiPolitikeKarta(idPolitike);
            if (!_isValidKategori)
            {
                e.Cancel = true;
                MbushGridKategoriNgaSession(idPolitike);
                ASPxGridView_KategoriPike.ShtoMesazhErrori(MessagesResource.Messages["PolitikeKartaKlienti.ValidimPikeKategori"]);
                return;
            }



            var tedrejtaInfo = new clsTeDrejtaRoli();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, EmerKomponente);
            if (!tedrejtaInfo.DShtim)
            {
                ASPxGridView_KategoriPike.ShtoMesazhErrori(MessagesResource.Messages["PolitikeKartaKlienti.NukKeniTeDrejta"]);
                MbushGridKategoriNgaSession(idPolitike);
                e.Cancel = true;
                return;
            }

            double.TryParse(e.NewValues["Pike"].ToString(), out var pike);
            var kategoria = new clsTrupiPolitikeKarta(idPolitike, e.NewValues["Kategoria"].ToString(), pike);

            kategorite.Add(kategoria);

            e.Cancel = true;
            ASPxGridView_KategoriPike.CancelEdit();

            mySessionObjects.ruajObjectNeSesion(Session, kategorite, "kategoriPikesh");
            MbushGridKategoriNgaSession(idPolitike);

            KonfiguroVleraFillestare();
        }

        protected void ASPxGridView_KategoriPike_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
        {
            var tedrejtaInfo = new clsTeDrejtaRoli();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, EmerKomponente);

            if (!tedrejtaInfo.DMod)
            {
                ASPxGridView_KategoriPike.ShtoMesazhErrori(MessagesResource.Messages["PolitikeKartaKlienti.NukKeniTeDrejta"]);
                e.Cancel = true;
                return;
            }

            int.TryParse(hfId.Value, out var idPolitike);
            var kategorite = mySessionObjects.merrObjectNgaSesioni(Session, "kategoriPikesh") as colTrupiPolitikeKarta ?? new colTrupiPolitikeKarta(idPolitike);

            if (!ValidoKategori(idPolitike))
            {
                e.Cancel = true;

                MbushGridKategoriNgaSession(idPolitike);
                ASPxGridView_KategoriPike.ShtoMesazhErrori(MessagesResource.Messages["PolitikeKartaKlienti.ValidimKategori"]);
                return;
            }

            var idKategoria = Convert.ToInt32(e.Keys[ASPxGridView_KategoriPike.KeyFieldName]);
            var kategoria = kategorite.Find(x => x.IdKategoria == idKategoria);
            kategoria.Kategoria = e.NewValues["Kategoria"].ToString();

            double.TryParse(e.NewValues["Pike"].ToString(), out var pike);

            kategoria.IdPolitike = idPolitike;
            kategoria.Pike = pike;

            mySessionObjects.ruajObjectNeSesion(Session, kategorite, "kategoriPikesh");

            e.Cancel = true;
            ASPxGridView_KategoriPike.CancelEdit();
            MbushGridKategoriNgaSession(idPolitike);

            KonfiguroVleraFillestare();
        }

        protected void ASPxGridView_KategoriPike_RowValidating(object sender, ASPxDataValidationEventArgs e)
        {
            if (e.NewValues["Kategoria"] != null && e.NewValues["Pike"] != null)
                _isValidKategori = true;
            else
                _isValidKategori = false;
        }

        protected void ASPxGridView_KategoriPike_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            e.Editor.ReadOnly = false;
        }

        protected void ASPxGridView_KategoriPike_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
        {
            var tedrejtaInfo = new clsTeDrejtaRoli();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, EmerKomponente);
            if (!tedrejtaInfo.DFsh)
            {
                ASPxGridView_KategoriPike.ShtoMesazhErrori(MessagesResource.Messages["PolitikeKartaKlienti.NukKeniTeDrejta"]);
                e.Cancel = true;
                return;
            }

            int.TryParse(hfId.Value, out var idPolitike);

            if (!ValidoKategori(idPolitike))
            {
                e.Cancel = true;

                MbushGridKategoriNgaSession(idPolitike);
                ASPxGridView_KategoriPike.ShtoMesazhErrori(MessagesResource.Messages["PolitikeKartaKlienti.ValidimKategori"]);
                return;
            }

            var kategorite = mySessionObjects.merrObjectNgaSesioni(Session, "kategoriPikesh") as colTrupiPolitikeKarta ?? new colTrupiPolitikeKarta(idPolitike);
            var idKategoria = Convert.ToInt32(e.Keys[ASPxGridView_KategoriPike.KeyFieldName]);

            e.Cancel = true;
            kategorite = kategorite.Where(k => k.IdKategoria != idKategoria) as colTrupiPolitikeKarta;

            mySessionObjects.ruajObjectNeSesion(Session, kategorite, "kategoriPikesh");

            MbushGridKategoriNgaSession(idPolitike);
            KonfiguroVleraFillestare();
        }

        private void MbushGridKategoriNgaDb(int idPolitike)
        {
            var kategorite = new colTrupiPolitikeKarta(idPolitike);
            mySessionObjects.ruajObjectNeSesion(Session, kategorite, "kategoriPikesh");
            ASPxGridView_KategoriPike.DataSource = kategorite;
            ASPxGridView_KategoriPike.DataBind();
        }

        private void MbushGridKategoriNgaSession(int idPolitike)
        {
            var kategorite = mySessionObjects.merrObjectNgaSesioni(Session, "kategoriPikesh") as colTrupiPolitikeKarta ?? new colTrupiPolitikeKarta(idPolitike);
            mySessionObjects.ruajObjectNeSesion(Session, kategorite, "kategoriPikesh");
            ASPxGridView_KategoriPike.DataSource = kategorite;
            ASPxGridView_KategoriPike.DataBind();
        }

        #endregion
    }
}