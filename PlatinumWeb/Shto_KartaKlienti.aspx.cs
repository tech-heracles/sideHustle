using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DbCore;
using DbCore.DbAdmin;
using DbCore.DbKontabiliteti;
using DbCore.DbRegjistrim;
using DbCore.DbShare;
using DbCore.IMBUtils;
using DbCore.IMBUtils.Extensions;
using DbCore.IMBUtils.Logging;
using DbCore.IMBUtils.Messages;
using DevExpress.Web;
using Newtonsoft.Json;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    /// <summary>
    /// Faqja e shtimit te kartave
    /// </summary>
    public partial class Shto_KartaKlienti : MyPageBase
    {
        private const string Komponente = "Shto_KartaKlienti.aspx";
        private const int IdKomponente = 2017;

        public bool CeljeShpejte { get; set; }
        public int IdKlienti { get; set; }

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
                MbushHfStateMePerkthime();
                

                CeljeShpejte = !string.IsNullOrEmpty(Request.QueryString["celjeShpejte"]) && Request.QueryString["celjeShpejte"] == "po";
                hfState.Set("celjeShpejte", CeljeShpejte);

                int idKlienti;
                if (!string.IsNullOrEmpty(Request.QueryString["idKlienti"]))
                    int.TryParse(Request.QueryString["idKlienti"], out idKlienti);
                else
                    idKlienti = 0;

                IdKlienti = idKlienti;
                hfState.Set("idKlienti", idKlienti);

                KonfiguroVleraFillestare();
                MbushGridKartaNgaDb();

                if (CeljeShpejte)
                {
                    ASPxPageControl1.TabPages[2].ClientVisible = false;
                }

                ASPxPageControl1.ActiveTabIndex = 0;

                var tedrejtaInfo = new clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();

                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, Komponente);
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);

                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(IdNdermarrja, "ASPxGridView_Kartat", ASPxGridView_Kartat, cmbKonfigurimi.Text.Split(';')[0], IdKomponente.ToString(), IdGjuha);
                VendosLlojeNeHfState();
            }
            else
            {
                MbushGridKartaNgaSession();
                CeljeShpejte = (bool)hfState["celjeShpejte"];
                IdKlienti = (int)hfState["idKlienti"];
            }
            KonfigurimComboGride.ShtoMenyrePagese(ASPxGridView_Kartat, rm, ci);
            GridUtil.konfigGrideListeEMadhePaTheme(ASPxGridView_Kartat, "IdKarta");
            clsToolbarConfig.mbushComboBoxFiltraMeValFieldTextField(IdGjuha, IdNdermarrja, "ASPxGridView_Kartat", Komponente, "IdFiltra", "FiltraShenime", int.Parse(cmbKonfigurimi.Value.ToString()));

            GridUtil.ToolTipButonaveMbiGride(ASPxGridView_Kartat, ci, rm);
            ASPxGridView_Kartat.PercaktoTitlePanel(Page, _menuInfo, _pnlMesazhi, hfState, IdPerdoruesi, IdNdermarrja, IdViti, IdGjuha, int.Parse(cmbKonfigurimi.Value.ToString()), Komponente, IdKomponente, "IdKarta", rm, ci);

            konfigurimi_Label.Text = MessagesResource.Messages["lblModeli"];
            lblGjendjepikesh.Visible = false;
            txtGjendjepikesh.Visible = false;
        }

        private void MbushHfStateMePerkthime()
        {
            hfState.Set("headerZgjidhKlientFurnitorin", "Lupa Klient");
            hfState.Set("headerZgjidhKategoriZbritje", "Lupa Kategori Zbritje");
            hfState.Set("headerZgjidhArtikull", "Lupa Artikull");
            hfState.Set("headerPopUpZgjidhKodifikiminArtikullit", "Lupa Kodifikim Artikulli");
            hfState.Set("msgZgjdhniNjeNgaElementetEListes", MessagesResource.Messages["msgZgjdhniNjeNgaElementetEListes"]);
            hfState.Set("KartaKlienti.EkzistonArtikullGrupArtikull", MessagesResource.Messages["KartaKlienti.EkzistonArtikullGrupArtikull"]);
            hfState.Set("KartaKlienti.LimitiNukMundTeFshihet", MessagesResource.Messages["KartaKlienti.LimitiNukMundTeFshihet"]);
        }

        /// <summary>
        /// Vendos emrat e tabeve ne baze te gjuhes se perdoruesit
        /// </summary>
        private void EmrateTabeve()
        {
            ASPxPageControl1.TabPages[0].Text = MessagesResource.Messages["labelRaportTePergjithshme"];
            ASPxPageControl1.TabPages[1].Text = MessagesResource.Messages["labelAdministrimiInformacion"];
            ASPxPageControl1.TabPages[2].Text = MessagesResource.Messages["labelLimitiKartes"];
        }

        /// <summary>
        /// konfiguron vlerat fillestare
        /// </summary>
        private void KonfiguroVleraFillestare()
        {
            ASPxPageControl1.ActiveTabIndex = 0;
            ConfigureAspxComboBox.KonfiguroComboBoxPolitika(cmbPolitike, IdNdermarrja, true);
            ConfigureAspxComboBox.ShtoKolonaPerKategoriZbritje(cmbKategori);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmbKategori, cmbKlienti);
            ConfigureAspxComboBox.KonfiguroComboBoxKategoriZbritje(IdNdermarrja, cmbKategori);
            ConfigureAspxComboBox.KonfiguroComboBoxComboQytete(IdNdermarrja, cmbQyteti);
            ConfigureAspxComboBox.shtoKolonaPerKlientFurnitor(cmbKlienti);
            ConfigureAspxComboBox.KonfiguroComboBoxMetoda(txtMenyrePagese, true);
            ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(IdPerdoruesi, IdNdermarrja, cmbKonfigurimi, 117, rm, ci, IdGjuha);
            cmbKonfigurimi.SelectedIndex = 0;

            var konf = new clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()), IdGjuha);
            hfKonffillestar.Value = $"{konf.KodKonfigAmbjente};{konf.PershkrimKonfigAmbjente}";

            var multiselect = clsAlternativaKushti.getAlternativa(konf.IdKonfigAmbjente, "KK2") == "Po";
            hfKushti.Value = multiselect.ToString();
        }

        private void VendosLlojeNeHfState()
        {
            var list = new List<object>
            {
                new { Label = MessagesResource.Messages["KartaKlienti.LlojiArtikull"], Lloji = 0 },
                new { Label = MessagesResource.Messages["KartaKlienti.LlojiGrupim1Artikulli"], Lloji = 1 }
            };

            hfState.Set("lloji", JsonConvert.SerializeObject(list));
        }

        /// <summary>
        /// perdoret per te fshire rreshtat e zgjedhur te bankave nqs perdoruesi konfirmon fshirjen
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            var rreshtat = ASPxPageControl1.ActiveTabIndex == 0
                ? ASPxGridView_Kartat.GetSelectedFieldValues("IdKarta")
                : new List<object> { hfId.Value };

            if (rreshtat.Count == 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(_menuInfo, MessagesResource.Messages["KartaKlienti.ZgjidhKarte"], _pnlMesazhi);
                return;
            }

            List<string> teFshire = new List<string>(), tePaFshire = new List<string>();

            var idNivel = clsNivelRegjistrimi.ktheIdNivelRegjistrimiSipasKodi("KK", IdNdermarrja);

            var dbRegjistrim = new clsDatabaseRegjistrim();
            foreach (var id in rreshtat)
            {
                var idKarta = Convert.ToInt32(id);
                if (idKarta == 0)
                    continue;

                var karta = new clsKarta(idKarta);
                try
                {
                    if (clsKarta.EshteLidhurKarta(idKarta, idNivel))
                    {
                        throw new MyException(MessagesResource.Messages["KartaKlienti.KartaLidhur"]);
                    }

                    karta.Fshi(IdPerdoruesi);
                    HiqNgaGrida(idKarta);
                    teFshire.Add(karta.Kodi);

                    ASPxPageControl1.ActiveTabIndex = 0;
                    hfStatusi.Value = "true";
                }
                catch (Exception ex)
                {
                    ImbLogger.Error(ex);
                    tePaFshire.Add(karta.Kodi);
                }
            }
            dbRegjistrim.Dispose();

            string mesazhInfoGabim = "", mesazhInfoSukses = "";

            if (tePaFshire.Count == 1)
                mesazhInfoGabim =
                    $"{MessagesResource.Messages["KartaKlienti.KartaMeKod"]} {string.Join("; ", tePaFshire)} {MessagesResource.Messages["KartaKlienti.KarteLidhur"]}";
            else if (tePaFshire.Count > 1)
                mesazhInfoGabim =
                    $"{MessagesResource.Messages["KartaKlienti.KartatMeKod"]} {string.Join(";", tePaFshire)} {MessagesResource.Messages["KartaKlienti.KartaLidhur"]}";

            if (teFshire.Count == 1)
                mesazhInfoSukses =
                    $"{MessagesResource.Messages["KartaKlienti.KartaMeKod"]} {string.Join("; ", teFshire)} {MessagesResource.Messages["KartaKlienti.KarteFshirjeSukses"]}";
            else if (teFshire.Count > 1)
                mesazhInfoSukses =
                    $"{MessagesResource.Messages["KartaKlienti.KartatMeKod"]} {string.Join(";", teFshire)} {MessagesResource.Messages["KartaKlienti.KartaFshirjeSukses"]}";

            if (mesazhInfoGabim != "" && mesazhInfoSukses != "")
                mesazhInfoGabim += MessagesResource.Messages["KartaKlienti.LidhesKurse"] + mesazhInfoSukses;

            if (mesazhInfoGabim != "")
                clsMenuInfo.ShtoMesazhGabimi(_menuInfo, mesazhInfoGabim, _pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhSuksesi(_menuInfo, mesazhInfoSukses, _pnlMesazhi);

            _pnlMesazhi.Update();
        }

        /// <summary>
        /// krijon burimin sipas te dhenave
        /// </summary>
        /// <returns> burimin me te dhenat</returns>
        private clsKarta KrijoKarte()
        {
            int idKarta = 0;
            bool isshtim;

            var politike = !string.IsNullOrEmpty(cmbPolitike.Text)
                ? cmbPolitike.Text.Split(';')[0]
                : string.Empty;

            var kategoria = cmbKategori.Text != ""
                ? cmbKategori.Text.Split(';')[0]
                : string.Empty;

            if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim")
                isshtim = true;
            else
            {
                isshtim = false;
                int.TryParse(hfId.Value, out idKarta);
            }

            var konfig = new clsKonfigurimAmbjenti();
            if (cmbKonfigurimi.Text == string.Empty)
                konfig.mbushKonfigDefaultKomponentes(IdKomponente, IdNdermarrja);
            else
                konfig.mbushKonfigAmbjSipasKod(cmbKonfigurimi.Text, IdNdermarrja);

            var lejoMeTenjejtinKlient = clsAlternativaKushti.getAlternativa(konfig.IdKonfigAmbjente, "KK1") == "Po";

            //TODO Mihal te kalohen limitet nga DataRow ne DTOs
            var limitet = JsonConvert.DeserializeObject<DataTable>(hfState.Get("limitet").ToString()).AsEnumerable().Where(x => Convert.ToInt32(x["IdArtikull"]) != 0);
            KontrolloDublikimeArtikullGrup1Artikull(limitet);

            //bere per te ruajtur piket fillestare te kartes te dhurata me vlere me minus
            //TODO Mihal te krijohet nje tabele qe mban piket per cdo karte, si nga karta, dhurata apo shitja
            int.TryParse(txtGjendjePike.Text, out var pikeDebit);
            var dhurata = new Dhurata(idKarta, 0, -pikeDebit, IdPerdoruesi, IdNdermarrja, IdNdermarrjeVit);
            int idMenyrePagese = clsFunksione.ktheMenyrePageseSipasLlojit(txtMenyrePagese.Text);
            return new clsKarta(idKarta, txtKodi.Text.RemoveSpaces(), txtEmri.Text, txtEmail.Text, txtKontakt.Text, txtShoferi.Text, txtTarga.Text, cmbKlienti.Text.Trim(), politike, kategoria, IdPerdoruesi, IdNdermarrja, IdPerdoruesi, cbAktiv.Checked, isshtim, lejoMeTenjejtinKlient, dteDitelindja.Date, cmbQyteti.Text, txtAdresa.Text, limitet, dhurata, txtDepartamenti.Text, txtModeli.Text, txtId.Text, idMenyrePagese, 0);
        }

        private static void KontrolloDublikimeArtikullGrup1Artikull(IEnumerable<DataRow> data)
        {
            var dataRows = data as DataRow[] ?? data.ToArray();
            if (dataRows.Where((t1, i) => dataRows.Where((t, j) => i != j && !Convert.IsDBNull(t1) && !Convert.IsDBNull(t))
                .Any(t => (Convert.ToInt32(t1["IdArtikull"]) == Convert.ToInt32(t["IdArtikull"]) 
                           || (Convert.ToInt32(t1["IdArtikull"]) != Convert.ToInt32(t["IdArtikull"]) && Convert.ToInt32(t1["IdKodifikimi1"]) == Convert.ToInt32(t["IdArtikull"])))
                           && (Convert.ToDateTime(t1["DtFillimi"]) >= Convert.ToDateTime(t["DtFillimi"])
                               && Convert.ToDateTime(t1["DtFillimi"]) <= Convert.ToDateTime(t["DtMbarimi"])
                               || Convert.ToDateTime(t1["DtMbarimi"]) >= Convert.ToDateTime(t["DtFillimi"])
                                   && Convert.ToDateTime(t1["DtMbarimi"]) <= Convert.ToDateTime(t["DtMbarimi"])))).Any())
            {
                throw new MyException("Ne gride te limiteve ka artikuj ose grupe te perseritur brenda nje intervali date!");
            }
        }

        private void RuajKartaKlienti()
        {
            try
            {
                var tedrejtaInfo = new clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, Komponente);

                if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim")
                {
                    if (!tedrejtaInfo.DShtim)
                    {
                        throw new MyException(MessagesResource.Messages["KartaKlienti.NukKeniTeDrejta"]);
                    }

                    var karte = KrijoKarte();
                    karte.Ruaj();
                    ShtoNeGrid(karte.IdKarta);
                    clsMenuInfo.ShtoMesazhSuksesi(_menuInfo, MessagesResource.Messages["KartaKlienti.RuajtjeSukses"], _pnlMesazhi);
                }
                else
                {
                    if (!tedrejtaInfo.DMod)
                    {
                        throw new MyException(MessagesResource.Messages["KartaKlienti.NukKeniTeDrejta"]);
                    }

                    var karte = KrijoKarte();
                    karte.Modifiko();
                    ModifikoNeGrid(karte.IdKarta);
                    clsMenuInfo.ShtoMesazhSuksesi(_menuInfo, MessagesResource.Messages["KartaKlienti.ModifikimSukses"], _pnlMesazhi);
                }

                hfStatusi.Value = "true";
            }
            catch (MyException ex)
            {
                ImbLogger.Error(ex);
                clsMenuInfo.ShtoMesazhGabimi(_menuInfo, ex.Message, _pnlMesazhi);
                hfStatusi.Value = "false";
            }
            catch (Exception ex)
            {
                ImbLogger.Error(ex);
                clsMenuInfo.ShtoMesazhGabimi(_menuInfo, MessagesResource.Messages["KartaKlienti.RuajtjaGabim"], _pnlMesazhi);
                hfStatusi.Value = "false";
            }
        }

        #region Menu

        /// <summary>
        /// Mbush menune me buttonat perkates sipas faqes.
        /// </summary>
        /// <param name="celjeShpejte">if set to <c>true</c> [celje shpejte].</param>
        protected void PercaktoTemplateMenu(ASPxMenu menu, ASPxMenu menuInfo)
        {
            clsToolbarConfig.percaktoTemplateMenu(IdGjuha, IdViti, IdPerdoruesi, IdNdermarrja, _menu, Komponente, this, _menuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, hfShtimModifikim.Value != "modifikim", false, false, Meme);

            if (!CeljeShpejte)
            {
                _menu.Items.FindByName("OK").Visible = false;
            }
        }

        protected void Menu_ItemClick(object sender, MenuItemEventArgs e)
        {
            if (e.Item.Name == "Ruaj")
            {
                Page.Validate("entries");
                RuajKartaKlienti();
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
            menu_msg_Frame.FshiFilter(ASPxGridView_Kartat, Komponente, int.Parse(cmbKonfigurimi.Value.ToString()), ref hfStatusi);
        }

        /// <summary>
        /// sherben per te ruajtur filtrin e zgjedhur
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
            menu_msg_Frame.RuajFilter(ASPxGridView_Kartat, Komponente, int.Parse(cmbKonfigurimi.Value.ToString()), ref hfStatusi);
        }

        #endregion

        #region Grid Karta

        protected void RuajKolona_Click(object sender, EventArgs e)
        {
            var idkonf = clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(cmbKonfigurimi.Text.Split(';')[0], IdNdermarrja);
            var mesazh = GridUtil.ruajFiltra(IdNdermarrja, IdPerdoruesi, IdGjuha, "ASPxGridView_Kartat ", Komponente, "FilterDefault", ASPxGridView_Kartat.FilterExpression, ASPxGridView_Kartat, "Kodi", idkonf, out var idfiltri);//ruan filtrin e zgjedhur tek filtrat
            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(_menuInfo, mesazh.PershkrimMesazhi, _pnlMesazhi);
                return;
            }

            mesazh = GridUtil.ruajkonfigurimgride(ASPxGridView_Kartat, cmbKonfigurimi.Text, IdNdermarrja, IdPerdoruesi, IdKomponente, idfiltri, IdViti, ci, IdGjuha);/// ruan konfigurimin e grides dhe filtrin

            clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, IdNdermarrja, "ASPxGridView_Kartat ", int.Parse(cmbKonfigurimi.Value.ToString()), Komponente);
            PercaktoTemplateMenu(_menu, _menuInfo);

            if (mesazh.Status)
                clsMenuInfo.ShtoMesazhSuksesi(_menuInfo, mesazh.PershkrimMesazhi, _pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhGabimi(_menuInfo, mesazh.PershkrimMesazhi, _pnlMesazhi);
        }

        /// <summary>
        /// perdoret per te shtuar kolonen e selektimit tek grida dhe per te vendosur disa karakteristika te grides
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void ASPxGridView_Kartat_DataBound(object sender, EventArgs e)
        {
            GridUtil.ShtoCommandColumnNeDatabound(ASPxGridView_Kartat, "#", "IdKarta");
        }

        /// <summary>
        /// perdoret ne rastet kur grida ben callback per ta rimbushur griden
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void ASPxGridView_Kartat_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            GridUtil.GridAfterPerformCallback(sender, e, ASPxGridView_Kartat, _menu);
            GridUtil.ToolTipButonaveMbiGride(ASPxGridView_Kartat, ci, rm);
        }

        /// <summary>
        /// perdoret per te vendosur filtrat tek headeri i grides
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxGridView_Kartat_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            GridUtil.GridHeaderFilterFillItem(sender, e, rm, ci, "Emri", "Kodi");
        }

        /// <summary>
        /// heq nga grida rreshtin e fshire
        /// </summary>
        ///<param name="idKarte"> id e kartes</param>
        private void HiqNgaGrida(int idKarte)
        {
            if (ASPxGridView_Kartat.DataSource != null)
            {
                var dt = (DataTable)ASPxGridView_Kartat.DataSource;
                var drs = dt.Select($"IdKarta = '{idKarte}'");

                if (drs.Length == 0)
                    return;

                dt.Rows.Remove(drs[0]);
                ASPxGridView_Kartat.DataBind();
            }
            else
                MbushGridKartaNgaDb();
            ASPxGridView_Kartat.FilterExpression = string.Empty;
        }

        /// <summary>
        /// Shton nte gride rreshtat e rinj
        /// </summary>
        /// <param name="idKarta"> id e kartes</param>
        private void ShtoNeGrid(int idKarta)
        {
            if (ASPxGridView_Kartat.DataSource != null)
            {
                var dt = (DataTable)ASPxGridView_Kartat.DataSource;

                var newArtDr = clsKarta.MerrKarteSipasId(idKarta);
                if (newArtDr != null)
                    dt.ImportRow(newArtDr);
                else
                    MbushGridKartaNgaDb();
            }
            else
                MbushGridKartaNgaDb();
        }

        /// <summary>
        /// modifikon ne gride komponenten e modifikuar
        /// </summary>
        /// <param name="idKarta"> id e kartes</param>
        private void ModifikoNeGrid(int idKarta)
        {
            if (ASPxGridView_Kartat.DataSource != null)
            {
                var dt = (DataTable)ASPxGridView_Kartat.DataSource;
                var drs = dt.Select("IdKarta = " + idKarta);

                if (drs.Length == 0)
                    return;

                var newArtDr = clsKarta.MerrKarteSipasId(idKarta);

                if (newArtDr != null)
                {
                    drs[0].ItemArray = newArtDr.ItemArray;
                }
                else
                    MbushGridKartaNgaDb();
            }
            else
                MbushGridKartaNgaDb();
        }

        /// <summary>
        /// mbush griden nga sesioni
        /// </summary>
        private void MbushGridKartaNgaSession()
        {
            var sukses = mySessionObjects.merrGrideNgaSessioni(Session, out DataTable tmpObject);
            if (!sukses)
                MbushGridKartaNgaDb();
            else
            {
                ASPxGridView_Kartat.DataSource = tmpObject;
                ASPxGridView_Kartat.DataBind();
                tmpObject.Dispose();
            }
        }

        /// <summary>
        /// mbush griden nga databaza
        /// </summary>
        private void MbushGridKartaNgaDb()
        {
            var dt = CeljeShpejte
                ? IdKlienti == 0
                    ? colKarta.MerrKartatSipasNdermarrjes(IdNdermarrja)
                    : colKarta.MerrKartatSipasKlientit(IdNdermarrja, IdKlienti)
                : colKarta.MerrKartatSipasNdermarrjesAll(IdNdermarrja);

            mySessionObjects.ruajGrideNeSession(Session, dt);
            ASPxGridView_Kartat.DataSource = dt;
            ASPxGridView_Kartat.DataBind();
            dt.Dispose();
        }

        /// <summary>
        /// cakton properti ne javascript
        /// </summary>
        /// <param name="sender">derguesi </param>
        /// <param name="e">argumentat</param>
        protected void ASPxGridView_Kartat_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = ASPxGridView_Kartat.PageIndex;
            e.Properties["cpPageRow"] = ASPxGridView_Kartat.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = ASPxGridView_Kartat.VisibleRowCount;
        }

        /// <summary>
        /// kur grida ben callback
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxGridView_Kartat_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            GridUtil.GridCustomCallbackDefault(sender, e, ASPxGridView_Kartat, Komponente, IdNdermarrja, IdGjuha, cmbKonfigurimi, ref hfStatusi);
        }

        /// <summary>
        /// perdoret per te shfaqur po jo tek komboja e aktivit
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxGridView_Kartat_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName == "Aktiv")
            {
                (e.Editor as ASPxComboBox).Items.Clear();
                (e.Editor as ASPxComboBox).Items.Add("");
                (e.Editor as ASPxComboBox).Items.Add("Po", true);
                (e.Editor as ASPxComboBox).Items.Add("Jo", false);
            }
        }

        #endregion

        #region Combo Events

        /// <summary>
        /// per filtrimin e llogarise 
        /// </summary>
        /// <param name="source">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void cmbPolitike_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].Contains("cmbPolitike"))
                {
                    // mbushComboBoxPolitikat(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                }
            }
        }

        protected void cmbPolitike_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("cmbPolitike"))
            {
                ConfigureAspxComboBox.KonfiguroComboBoxPolitika(cmbPolitike, IdNdermarrja, e.Filter);
            }
        }

        protected void cmbKategori_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("cmbKategori"))
                ConfigureAspxComboBox.KonfiguroComboBoxKategoriZbritje(IdNdermarrja, cmbKategori);
        }

        protected void cmbKlienti_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("cmbKlienti"))
            {
                if (e.Value == null || !int.TryParse(e.Value.ToString(), out var value))
                    return;

                ConfigureAspxComboBox.KonfiguroComboBoxKlientFurnitoriById(IdPerdoruesi, IdNdermarrja, (ASPxComboBox)source, value);
            }
        }

        protected void cmbKlienti_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("cmbKlienti"))
                ConfigureAspxComboBox.KonfiguroComboBoxKlientFurnitor(e.Filter, e.BeginIndex + 1, e.EndIndex + 1,
                    IdPerdoruesi, IdNdermarrja, cmbKlienti, 1);
        }

        #endregion
    }
}