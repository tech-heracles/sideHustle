using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DbCore;
using DbCore.DbAdmin;
using DbCore.DbArkaBanka;
using DbCore.DbKontabiliteti;
using DbCore.DbQendraKosto;
using DbCore.DbRegjistrim;
using DbCore.DbShare;
using DbCore.IMBUtils.Extensions;
using DevExpress.Web;
using Newtonsoft.Json;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using PlatinumWeb.ApplicationUtils.Filters;
using PlatinumWeb.ApplicationUtils.Pages;
using PlatinumWeb.Templates;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using DbCore.IMBUtils.Messages;
using System.Data;
using PlatinumWeb.ApplicationUtils;
using DbCore.IMBUtils.DataBase;

namespace PlatinumWeb
{
    public partial class LidhjaDokumentave : MyPageBase
    {
        private readonly ASPxComboBox _tempcombo = null;
        private ASPxTextBox _temptxt;
        private readonly TextBox _temptxtNormal = null;
        private TitlePeriudha _periudha;
        private const string Komponente = "LidhjaDokumentave.aspx";
        public TitlePeriudha Periudha => _periudha ?? (_periudha = this.MerrPeriudhe(hfState));
        private string guidString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!mySessionObjects.isLogedIn(Session))
                clsFunksione.logout(Session, true, "FaqePaautorizuar");

            if (mySessionObjects.ktheKodNdermarrje(Session) == null)
                Response.Redirect("Login_Ndermarrje.aspx?id=" + IdPerdoruesi);

            PercaktoTemplateMenu();
            VendosHfMePerkthime();

            if (!IsPostBack)
            {
                ShtoVleraTePergjithshmeNeHfState();
                guidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                hfState.Set("guidString", guidString);
            }
            else
            {
                guidString = (string)hfState["guidString"];
                grid_dokKryesor.PercaktoTitlePanelMePeriudhe(this, MenuInfo, pnlMesazhi, hfState, IdPerdoruesi, IdNdermarrja, IdViti, IdGjuha, GetIdKonfigurimi(), Komponente, 217, "IDSHITJEKOKA", rm, ci, true, false, true);

                mbushGridDokumenteshNgaSession();

                return;
            }

            if (string.IsNullOrEmpty(Request.QueryString["shtim_modifikim"]) || Request.QueryString["shtim_modifikim"] == "shtim")
            {
                hfShtimModifikim.Value = "shtim";
                KonfiguroVleraFillestare();
            }
            else
            {
                hfShtimModifikim.Value = "modifikim";
                KonfiguroVleraFillestareModifiko();
                MerrTedhenat();
                PercaktoTemplateMenu();                
            }
            grid_dokKryesor.PercaktoTitlePanelMePeriudhe(this, MenuInfo, pnlMesazhi, hfState, IdPerdoruesi, IdNdermarrja, IdViti, IdGjuha, GetIdKonfigurimi(), Komponente, 217, "IDSHITJEKOKA", rm, ci, true, false, true);

            var tedrejtaInfo = new clsTeDrejtaRoli();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, Komponente);
            hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
            hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
        }

        /// <summary>
        /// mbush kontrollet kur hapet dokumenti per ta pare
        /// </summary>
        public void MerrTedhenat()
        {
            var koka = new clsDokumentLidhesKoka { IdKoka = int.Parse(Request.QueryString["id"]) };
            koka.merrSipasId(true);

            if (koka.IdKlientFurnitor != 0)
                ConfigureAspxComboBox.KonfiguroComboBoxKlientFurnitoriById(klientFurnitor_ButtonEdit, koka.IdKlientFurnitor);

            dtDokumenti_DateEdit.Date = koka.DateDokumenti;
            dtRegjistrimi_DateEdit.Date = koka.DateRegjistrimi;
            nrLidhje_TextBox.Text = koka.NrLidhje;

            var konf = new clsKonfigurimAmbjenti();
            konf.mbushKonfiguriminMeID(koka.IdKonfigAmbjente);

            if (koka.IdKonfigAmbjente != 0)
                konfigurimi_ComboBox.Text = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;

            var dbAdmin = new clsDatabaseAdmin();
            var dtlidhur = dbAdmin.MerrDokLidhur(koka.IdKoka, koka.IdNivel, "T_DOKUMENTLIDHESKOKA", "IDKOKA");
            hfLidhur.Value = (dtlidhur.Rows.Count != 0).ToString();
            AspxWebControlUtils.ShtoLidhje(IdPerdoruesi, IdViti, IdNdermarrja, hl, dtlidhur, koka.IdGjenerues, koka.IdNivelGjenerues, koka.IdKonfigGjenerues, IdGjuha);

            var dokumentat = MbushGrideKryesore(koka.IdKoka, koka.IdKlientFurnitor);
            MbushGrideLidhes(koka.IdKoka, koka.IdKlientFurnitor, dokumentat);

            KonfiguroGrideLlogarite();  //llogarite nuk perdoren me
            PercaktoTemplateKryesor();
            PercaktoTemplateLidhes();
            dbAdmin.Dispose();
        }

        /// <summary>
        /// merr dokumentat kryesor te klientit dhe e mbush griden vetem me ato dokumenta me te cilet eshte bere lidhja
        /// </summary>
        /// <param name="id"> id koka dokumenti lidhes</param>
        /// <param name="idkf">id klient furnitori</param>
        /// <returns>kthen dokumentat kryesore te lidhjes</returns>
        private colDokumentat MbushGrideKryesore(int id, int idkf)
        {
            grid_dokKryesor.PercaktoTitlePanelMePeriudhe(this, MenuInfo, pnlMesazhi, hfState, IdPerdoruesi, IdNdermarrja, IdViti, IdGjuha, GetIdKonfigurimi(), Komponente, 217, "IDSHITJEKOKA", rm, ci, true, false, true);
            double totalkryesor = 0;

            var dokumentat = grid_dokKryesor.MerrDataSourceMePeriduheNeSession<colDokumentat>(Session, Komponente, Periudha, guidString);
            if (dokumentat == null)
            {
                dokumentat = new colDokumentat();
                dokumentat.MbushKokaShitjeSipasKlientit(idkf, IdNdermarrja, id);
            }

            var col = new colDokumentat();
            foreach (var t in new colDokumentLidhesTrupi(id))
            {
                foreach (var dok in dokumentat)
                {
                    if (dok.IdDokumenti != t.IdDokumenti) continue;

                    dok.VleftaLikuiduar = t.VleraLidhjes;

                    dok.VleftaLikuiduarMon = dok.DtAzhornimi > dok.DtDokumenti
                        ? t.VleraLidhjes * dok.KursAzhornimi
                        : t.VleraLidhjes * dok.Kursi;

                    totalkryesor += dok.VleftaLikuiduarMon;
                    col.Add(dok);

                    break;
                }
            }

            grid_dokKryesor.DataSource = col;
            grid_dokKryesor.DataBind();
            grid_dokKryesor.RuajDataSourceMePeriduheNeSession(Session, Komponente, Periudha, col, guidString);
            KonfiguroGrideKoka(true);
            txtTotali.Text = totalkryesor.ToString();            
            return col;
        }
     
        /// <summary>
        /// merr dokumentat lidhes te klientit dhe e mbush griden me ato dokumenta me te cilet eshte bere lidhaj
        /// </summary>
        /// <param name="id">id koka dokumenti lidhes</param>
        /// <param name="idkf"> id klient furnitori</param>
        /// <param name="dokkryesor"> koleksioni me dokumentat kryesor</param>
        private void MbushGrideLidhes(int id, int idkf, colDokumentat dokkryesor)
        {
            double totalkryesor = 0;

            var dokumentat = new colDokumentat();
            dokumentat.mbushVeprimeBankaSipasKlientit(idkf, IdNdermarrja, id);

            var col = new colDokumentat();

            foreach (var t in new colDokumentLidhesTrupi(id))
            {
                foreach (var dok in dokumentat)
                {
                    if (dok.IdDokumenti != t.IdDokumenti) continue;

                    var ekziston = false;
                    double kvkmk;           // gjejme kv/kmk sipas kushtit qe nqs monedha e dok kryesor e njejte me mon e dok lidhes atehere kmk=1 prn kmk=kursin e monedhes se dokumentit kryesor ne diten e dokumentit lidhes
                    if (dokkryesor[0].IdMonedha == dok.IdMonedha)
                        kvkmk = 1;
                    else
                    {
                        var kursi = new clsKurset(dokkryesor[0].IdMonedha, dok.DtDokumenti);
                        if (kursi.VleraKursi == 0)
                            kursi.VleraKursi = 1;
                        kvkmk = dok.Kursi / kursi.VleraKursi;
                    }

                    foreach (var d in col)
                    {
                        if (d.IdDokumenti != t.IdDokumenti) continue;

                        d.VleftaLikuiduar += t.VleraLidhjes;         //vendosim vlerat e lidhes
                        d.VleftaLikuiduarMon += t.VleraLidhjes * kvkmk;    // eshte vlera e dokumentit lidhes sipas monedhes se dokumentit kryesor
                        totalkryesor += t.VleraLidhjes * dok.Kursi; ;
                        ekziston = true;
                        break;
                    }

                    if (ekziston) continue;

                    dok.VleftaLikuiduar = t.VleraLidhjes;
                    dok.VleftaLikuiduarMon = t.VleraLidhjes * kvkmk;
                    totalkryesor += t.VleraLidhjes * dok.Kursi; ;
                    col.Add(dok);
                }
            }

            grid_dokLidhes.DataSource = col;
            grid_dokLidhes.DataBind();
            KonfiguroGrideTrupi(true);
            txtTotali2.Text = totalkryesor.ToString();
            txtDiferenca.Text = Math.Round(totalkryesor - double.Parse(txtTotali.Text), 5).ToString();
        }

        private void VendosHfMePerkthime()
        {
            hfState.Set("msgZgjidhDokumentin", MessagesResource.Messages["msgZgjidhDokumentin"]);
            hfState.Set("msgDoTeBeshShperndarjenNeQKostoTeDifTeKursit", MessagesResource.Messages["msgDoTeBeshShperndarjenNeQKostoTeDifTeKursit"]);
            hfState.Set("msgShperndarjeNeQendratEKostos", MessagesResource.Messages["msgShperndarjeNeQendratEKostos"]);
            hfState.Set("msgLidhjaDokNukMundTeVendosniVlereMeTeMadheSeVleraEPalikujduar", MessagesResource.Messages["msgLidhjaDokNukMundTeVendosniVlereMeTeMadheSeVleraEPalikujduar"]);
            hfState.Set("msgLidhjaDokNukMundTeLidhniShumeMeShumeDok", MessagesResource.Messages["msgLidhjaDokNukMundTeLidhniShumeMeShumeDok"]);
            hfState.Set("msgLidhjaDokShenoNrElidhjes", MessagesResource.Messages["msgLidhjaDokShenoNrElidhjes"]);
            hfState.Set("msgLidhjaDokZgjidhTePaktenNjeDokKryesor", MessagesResource.Messages["msgLidhjaDokZgjidhTePaktenNjeDokKryesor"]);
            hfState.Set("msgLidhjaDokZgjidh1DateDokumenti", MessagesResource.Messages["msgLidhjaDokZgjidh1DateDokumenti"]);
            hfState.Set("msgLidhjaDokZgjidh1DateRegjistrimi", MessagesResource.Messages["msgLidhjaDokZgjidh1DateRegjistrimi"]);
            hfState.Set("msgLidhjaDokZgjidh1DokumentLidhes", MessagesResource.Messages["msgLidhjaDokZgjidh1DokumentLidhes"]);
            hfState.Set("msgLidhjaDokVeprimiNukEshteIKuadruar", MessagesResource.Messages["msgLidhjaDokVeprimiNukEshteIKuadruar"]);
            hfState.Set("msgZgjidhKF", MessagesResource.Messages["msgZgjidhKF"]);
        }

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        private void PercaktoTemplateMenu()
        {
            var menu = new colMenuItem(IdGjuha);
            menu.merrMenuItemSipasKomponentes(IdGjuha, Komponente, IdPerdoruesi, IdNdermarrja, IdViti, hfShtimModifikim.Value != "modifikim");

            var kok = new clsKokaFleteKontabel();
            foreach (var m in menu)
            {
                if (m.Name != "ItemFilter" && m.Name != "ItemFrame")
                    clsToolbarConfig.ShtoMenuItem(Theme, ASPxMenu1, m);

                if (m.Name == "Ruaj" && hfShtimModifikim.Value != "modifikim")
                    ASPxMenu1.Items[ASPxMenu1.Items.Count - 1].ClientVisible = true;

                if (m.Name == "Shto" && hfShtimModifikim.Value != "modifikim")
                    ASPxMenu1.Items[ASPxMenu1.Items.Count - 1].ClientVisible = true;

                if (hfShtimModifikim.Value != "modifikim" && m.Name == "Fshi")
                    ASPxMenu1.Items[ASPxMenu1.Items.Count - 1].ClientVisible = false;

                switch (m.Name)
                {
                    case "FletaKontabel":
                        if (hfShtimModifikim.Value != "modifikim")
                            ASPxMenu1.Items[ASPxMenu1.Items.Count - 1].ClientVisible = false;
                        else
                        {
                            kok = new clsKokaFleteKontabel(int.Parse(Request.QueryString["id"]), 10);

                            if (kok.NrDukumentiKokaFleteKontabel != null)
                                ASPxMenu1.Items[ASPxMenu1.Items.Count - 1].NavigateUrl = "javascript: myFaqeCelje.kontrolloTeDrejta('Shto_FleteKontabel.aspx?shtim_modifikim=modifikim&id=" + kok.IdKokaFleteKontabel + "&numur=" + kok.NrDukumentiKokaFleteKontabel + "')";
                            else
                                ASPxMenu1.Items[ASPxMenu1.Items.Count - 1].ClientVisible = false;
                        }
                        break;
                    case "QendraKosto":
                        if (hfShtimModifikim.Value != "modifikim")
                            ASPxMenu1.Items[ASPxMenu1.Items.Count - 1].ClientVisible = false;
                        else
                        {
                            if (kok.NrDukumentiKokaFleteKontabel != null)
                            {
                                var qend = new clsKokaQendraKosto();
                                qend.KtheKokaQKSipasIDGjeneruesDheKonfig(kok.IdKokaFleteKontabel, kok.IdKonfigAmbjente);

                                if (qend.NrDok != null)
                                {
                                    var headerText = rm.GetString("msgShperndarjeNeQendratEKostos", ci);
                                    ASPxMenu1.Items[ASPxMenu1.Items.Count - 1].NavigateUrl = "javascript: myButtonClickLupa.LupaUniversal_Click('" + headerText + "','LupaRegjistrimQendraKosto.aspx?shtim_modifikim=modifikim&idDokGjenerues=" + kok.IdKokaFleteKontabel + "&idkonfig=" + kok.IdKonfigAmbjente + "',900,600)";
                                }
                                else
                                    ASPxMenu1.Items[ASPxMenu1.Items.Count - 1].ClientVisible = false;
                            }
                            else
                                ASPxMenu1.Items[ASPxMenu1.Items.Count - 1].ClientVisible = false;
                        }
                        break;
                    case "ItemFrame":
                        clsToolbarConfig.ShtoMenuItemPerFrame(this, ASPxMenu1, clsFunksione.ktheUrlHelpi(m.UrlHelp).Item1);
                        break;
                }

                if (m.Name == "Shto" || m.Name == "Kerko" || m.Name == "Pastro" || m.Name == "ItemFilter" || m.Name == "ItemFrame")
                    ASPxMenu1.Items[ASPxMenu1.Items.Count - 1].BeginGroup = true;
            }

            clsMenuInfo.ShtoMenuItemInfo(this, MenuInfo);
        }

        /// <summary>
        /// Ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e) => PercaktoTemplateMenu();

        /// <summary>
        /// Mbush griden e dokumentave kryesore dhe te dokumentave vartes me vlerat default
        /// </summary>
        private void KonfiguroVleraFillestare()
        {
            ConfigureAspxComboBox.ShtoKolonaPerKf(klientFurnitor_ButtonEdit);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(klientFurnitor_ButtonEdit);
            ConfigureAspxComboBox.mbushComboKonfigurimet(IdPerdoruesi, IdNdermarrja, konfigurimi_ComboBox, 217, false, IdGjuha);
            //vendoset fiks sepse ai eshte konfigurimi default per ambjentin e flets kontabel
            konfigurimi_ComboBox.SelectedIndex = 0;
            var konf = new clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasId(int.Parse(konfigurimi_ComboBox.SelectedItem.Value.ToString()), IdGjuha);
            hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;
            mbushGridDokKryesor(konf);
            var col = new colDokumentat();//te dokumentat vartes ne fillim nuk shfaqim dokumenta.
            grid_dokLidhes.DataSource = col;
            grid_dokLidhes.DataBind();

            var llogarite = new colTrupatFletetKontabel();
            grid_llogarite.DataSource = llogarite;
            grid_llogarite.DataBind();

            KonfiguroGrideKoka(true);
            var formatNrPerKonfig = new clsFormatiKonfig();

            if (formatNrPerKonfig.KonfigTrupi.Count == 0)
            {
                var trup = new clsFormatKonfigTrup(clsFormatKonfigTrup.defaultFormatSasia, clsFormatKonfigTrup.defaultFormatCmimi, clsFormatKonfigTrup.defaultFormatVlefta, clsFormatKonfigTrup.defaultFormatZbritja, clsFormatKonfigTrup.defaultFormatStringSasia, clsFormatKonfigTrup.defaultFormatStringCmimi, clsFormatKonfigTrup.defaultFormatStringVlefta, clsFormatKonfigTrup.defaultFormatStringZbritja);
                formatNrPerKonfig.KonfigTrupi.Add(trup);
            }

            var serializusi = new JavaScriptSerializer();
            hfState.Set("formatMonedhe", serializusi.Serialize(formatNrPerKonfig));

            var mon = new colMonedhat();
            mon.mbushGjitheMonedhat(IdNdermarrja, IdPerdoruesi);

            var formate = new object[2];
            formate[0] = formatNrPerKonfig;
            formate[1] = mon;
            mySessionObjects.ruajFormatNr(formate, Session);

            PercaktoTemplateKryesor();
            KonfiguroGrideTrupi(true);
            PercaktoTemplateLidhes();
            KonfiguroGrideLlogarite();
        }

        private void mbushGridDokKryesor(clsKonfigurimAmbjenti konf)
        {
            grid_dokKryesor.PercaktoTitlePanelMePeriudhe(this, MenuInfo, pnlMesazhi, hfState, IdPerdoruesi, IdNdermarrja, IdViti, IdGjuha, konf.IdKonfigAmbjente, Komponente, 217, "IDSHITJEKOKA", rm, ci, true, false, true);
            var dokumentat = new colDokumentat();
            dokumentat.MbushGjitheDokumentatKryesore(IdNdermarrja, Periudha.DataDokNga, Periudha.DataDokDeri);
            grid_dokKryesor.DataSource = dokumentat;
            grid_dokKryesor.DataBind();
            grid_dokKryesor.RuajDataSourceMePeriduheNeSession(Session, Komponente, Periudha, dokumentat, guidString);
        }

        private void mbushGridDokumenteshNgaSession()
        {
            var tmpObject = grid_dokKryesor.MerrDataSourceMePeriduheNeSession<colDokumentat>(Session, Komponente, Periudha, guidString);
            if (tmpObject == null)
            {
                var konf = new clsKonfigurimAmbjenti();
                konf.mbushKonfigAmbjSipasId(GetIdKonfigurimi(), IdGjuha);
                hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;
                mbushGridDokKryesor(konf);
            }
            else
            {
                grid_dokKryesor.DataSource = tmpObject;
                grid_dokKryesor.DataBind();
                //tmpObject.Dispose();
            }
        }

        /// <summary>
        /// mbush kontrollet per rastin e hapjes se dokumentit
        /// </summary>
        private void KonfiguroVleraFillestareModifiko()
        {
            ConfigureAspxComboBox.ShtoKolonaPerKf(klientFurnitor_ButtonEdit);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(klientFurnitor_ButtonEdit);
            ConfigureAspxComboBox.mbushComboKonfigurimet(IdPerdoruesi, IdNdermarrja, konfigurimi_ComboBox, 217, false, IdGjuha);

            konfigurimi_ComboBox.SelectedIndex = 0;
            var konf = new clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasId(int.Parse(konfigurimi_ComboBox.SelectedItem.Value.ToString()), IdGjuha);
            hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;

            var llogarite = new colTrupatFletetKontabel();
            grid_llogarite.DataSource = llogarite;
            grid_llogarite.DataBind();

            dtDokumenti_DateEdit.Date = DateTime.Today;
            dtRegjistrimi_DateEdit.Date = DateTime.Today;

            var formatNrPerKonfig = new clsFormatiKonfig();
            formatNrPerKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(GetIdKonfigurimi());

            if (formatNrPerKonfig.KonfigTrupi.Count == 0)
            {
                var trup = new clsFormatKonfigTrup(clsFormatKonfigTrup.defaultFormatSasia, clsFormatKonfigTrup.defaultFormatCmimi, clsFormatKonfigTrup.defaultFormatVlefta, clsFormatKonfigTrup.defaultFormatZbritja, clsFormatKonfigTrup.defaultFormatStringSasia, clsFormatKonfigTrup.defaultFormatStringCmimi, clsFormatKonfigTrup.defaultFormatStringVlefta, clsFormatKonfigTrup.defaultFormatStringZbritja);
                formatNrPerKonfig.KonfigTrupi.Add(trup);
            }

            var serializusi = new JavaScriptSerializer();
            hfState.Set("formatMonedhe", serializusi.Serialize(formatNrPerKonfig));

            var mon = new colMonedhat();
            mon.mbushGjitheMonedhat(IdNdermarrja, IdPerdoruesi);

            var formate = new object[2];
            formate[0] = formatNrPerKonfig;
            formate[1] = mon;
            mySessionObjects.ruajFormatNr(formate, Session);
        }

        /// <summary>
        /// Konfiguron griden e dokumentave kryesore
        /// </summary>
        /// <param name="idPerdoruesi"></param>
        private void KonfiguroGrideKoka(bool visibleindex)
        {
            KonfigurimComboGride.ShtoNivelMeDataSource(grid_dokKryesor, () =>
            {
                var nivelet = new DbCore.DbRegjistrim.colNivelRegjistrimi();
                nivelet.mbushGjitheNivelRegjistrimiSipasSuperKat(IdNdermarrja, IdPerdoruesi, 2);
                return nivelet;
            }, Session, Komponente, guidString, "IdNiveli", "Kodi");
            KonfigurimComboGride.ShtoMonedhe(grid_dokKryesor, IdNdermarrja, IdPerdoruesi, Session, Komponente, guidString);
            KonfigurimComboGride.shtoKlient(grid_dokKryesor, IdNdermarrja, IdPerdoruesi, Session, Komponente, guidString, "IdKlientFurnitori");
            KonfigurimComboGride.ShtoModelMeDataSource(grid_dokKryesor, () =>
            {
                var konfigurimet = new colKonfigurimAmbjenti();
                konfigurimet.mbushKonfigAmbjSipasIdKategori(1, IdNdermarrja, IdPerdoruesi, IdGjuha);
                konfigurimet.mbushKonfigAmbjSipasIdKategori(2, IdNdermarrja, IdPerdoruesi, IdGjuha);
                konfigurimet.mbushKonfigAmbjSipasIdKategori(3, IdNdermarrja, IdPerdoruesi, IdGjuha);
                konfigurimet.mbushKonfigAmbjSipasIdKategori(4, IdNdermarrja, IdPerdoruesi, IdGjuha);
                konfigurimet.mbushKonfigAmbjSipasIdKategori(20, IdNdermarrja, IdPerdoruesi, IdGjuha);
                return konfigurimet;
            }, Session, Komponente, guidString);

            if (visibleindex)
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(IdNdermarrja, "grid_dokKryesor", grid_dokKryesor, konfigurimi_ComboBox.Text.Split(';')[0], "217", IdGjuha);
            else
                GridUtil.percaktoVisibleColumnsGridSipasKodKonfigurimiPaVisibleIndex(IdNdermarrja, grid_dokKryesor, konfigurimi_ComboBox.Text.Split(';')[0], "217", IdGjuha);

            GridUtil.konfigGrideListeEMadhePaTheme(grid_dokKryesor, "IdDokumenti", false);

            grid_dokKryesor.KeyFieldName = "IdDokumenti";
            grid_dokKryesor.Settings.UseFixedTableLayout = true;
            grid_dokKryesor.Settings.ShowTitlePanel = true;
            grid_dokKryesor.SettingsText.Title = rm.GetString("msgLidhjaDokQeRrisinDetyriminEKF", ci);
            grid_dokKryesor.Columns["#"].VisibleIndex = 0;
        }

        /// <summary>
        /// fshin rreshtat e selektuar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            pergjigja.Text = "";
            var kok = new clsDokumentLidhesKoka { IdKoka = int.Parse(Request.QueryString["id"]) };
            kok.IdPerdorues = IdPerdoruesi;
            
            kok.merrSipasId(true);

            var dbAdmin = new clsDatabaseAdmin();

            var lidhur = dbAdmin.eshteDokumentiILidhur(kok.IdKoka, kok.IdNivel, "T_DOKUMENTLIDHESKOKA", "IDKOKA");

            if (lidhur)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("vepBankaMsgDokEshteILidhurNukFshihet", ci), pnlMesazhi);
                hfStatusRuajtje.Value = "false";
                return;
            }

            if (kok.DateDokumenti.Year != new clsNdermarrjeViti(IdNdermarrjeVit).Viti)
            {
                hfStatusRuajtje.Value = "false";
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgLidhjaDokDataNukIPerketVitiUshtrTeZgjedhur", ci), pnlMesazhi);
                return;
            }

            var ekycur = clsPeriudhaKontabel.eshteKycurPeriudheSipasDateDheNdermarrjes(kok.DateDokumenti, IdNdermarrja);

            if (ekycur)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgPeriudhaEKycur", ci), pnlMesazhi);
                return;
            }

            if (DbCore.MbylljePeriudhe.PeriodClosing.IsPeriodClosed(kok.DateDokumenti, MyConnectionsManager.GetSelectedConNameServer(), IdNdermarrja, KategoriDokumenti.LidhjeDokumentash, kok.IdKonfigAmbjente))
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgPeriodIsClosed"], pnlMesazhi);
                return;
            }

            var mesazhi = new clsMesazh();

            dbAdmin.Dispose();
            mesazhi = kok.fshi(rm, ci);

            if (mesazhi.Status)
            {
                hfStatusRuajtje.Value = "true";
                Response.Redirect("ListaLidhjaDokumentave.aspx?fshi=po");
            }
            else
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhi.PershkrimMesazhi, pnlMesazhi);
                hfStatusRuajtje.Value = "false";
            }

            KonfiguroVleraFillestare();
        }

        /// <summary>
        /// Konfiguron griden e dokumentave lidhes
        /// </summary>
        /// <param name="IdNdermarrja"></param>
        /// <param name="IdPerdoruesi"></param>
        private void KonfiguroGrideTrupi(bool visibleindex)
        {
            KonfigurimComboGride.ShtoNivelMeDataSource(grid_dokLidhes, () =>
            {
                var nivelet = new DbCore.DbRegjistrim.colNivelRegjistrimi();
                nivelet.mbushGjitheNivelRegjistrimiSipasSuperKat(IdNdermarrja, IdPerdoruesi, 2);
                return nivelet;
            }, Session, Komponente, guidString, "IdNiveli", "Kodi");
            KonfigurimComboGride.ShtoModelMeDataSource(grid_dokLidhes, () =>
            {
                var konfigurimet = new colKonfigurimAmbjenti();
                konfigurimet.mbushKonfigAmbjSipasIdKategori(1, IdNdermarrja, IdPerdoruesi, IdGjuha);
                konfigurimet.mbushKonfigAmbjSipasIdKategori(2, IdNdermarrja, IdPerdoruesi, IdGjuha);
                konfigurimet.mbushKonfigAmbjSipasIdKategori(3, IdNdermarrja, IdPerdoruesi, IdGjuha);
                konfigurimet.mbushKonfigAmbjSipasIdKategori(4, IdNdermarrja, IdPerdoruesi, IdGjuha);
                konfigurimet.mbushKonfigAmbjSipasIdKategori(20, IdNdermarrja, IdPerdoruesi, IdGjuha);
                return konfigurimet;
            }, Session, Komponente, guidString);
            KonfigurimComboGride.ShtoMonedhe(grid_dokLidhes, IdNdermarrja, IdPerdoruesi, Session, Komponente, guidString);

            if (visibleindex)
                GridUtil.percaktoVisibleColumnsMeWidth(IdGjuha, IdNdermarrja, grid_dokLidhes, "grid_dokKryesor", Komponente);
            else
                GridUtil.percaktoVisibleColumnsMeWidthPaVisibleIndex(IdGjuha, IdNdermarrja, grid_dokLidhes, "grid_dokKryesor", Komponente);

            GridUtil.konfigGrideListeEMadhePaTheme(grid_dokLidhes, "IdDokumenti", false);

            grid_dokLidhes.KeyFieldName = "IdDokumenti";
            grid_dokLidhes.Settings.UseFixedTableLayout = true;
            grid_dokLidhes.Settings.ShowTitlePanel = true; grid_dokLidhes.SettingsBehavior.AllowSelectByRowClick = true;
            grid_dokLidhes.SettingsText.Title = rm.GetString("msgLidhjaDokQeZvogelojneDetyriminEKF", ci);
            grid_dokLidhes.Columns["#"].VisibleIndex = 0;
        }

        /// <summary>
        /// veprimet e menuse
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {
            switch (e.Item.Name)
            {
                case "Ruaj":
                    Page.Validate();
                    RuajLidhje(1);
                    break;
                case "Ruaj si draft":
                    Page.Validate();
                    RuajLidhje(0);
                    break;
            }
        }

        /// <summary>
        /// Konfiguron griden e llogarive qe preken nese ka diferenca nga kursi
        /// </summary>   //grida e llogarive nuk perdoret me
        private void KonfiguroGrideLlogarite()
        {
            for (var i = 0; i < grid_llogarite.Columns.Count; i++)
                grid_llogarite.Columns[i].Visible = false;

            grid_llogarite.Columns["NrLlogari"].Visible = true;
            grid_llogarite.Columns["NrLlogari"].VisibleIndex = 1;
            grid_llogarite.Columns["EmerLlogari"].Visible = true;
            grid_llogarite.Columns["EmerLlogari"].VisibleIndex = 2;
            grid_llogarite.Columns["VleftaDebiMonBazeTrupiFleteKontabel"].Visible = true;
            grid_llogarite.Columns["VleftaDebiMonBazeTrupiFleteKontabel"].Caption = MessagesResource.Messages["labelRaportiDebi"];
            grid_llogarite.Columns["VleftaDebiMonBazeTrupiFleteKontabel"].VisibleIndex = 3;
            grid_llogarite.Columns["VleftaKrediMonBazeTrupiFleteKontabel"].Visible = true;
            grid_llogarite.Columns["VleftaKrediMonBazeTrupiFleteKontabel"].Caption = MessagesResource.Messages["labelRaportiKredi"];
            grid_llogarite.Columns["VleftaKrediMonBazeTrupiFleteKontabel"].VisibleIndex = 4;

            grid_llogarite.Settings.UseFixedTableLayout = true;
            grid_llogarite.Settings.ShowTitlePanel = true;
            grid_llogarite.SettingsText.Title = MessagesResource.Messages["labelDiferencatNgaKursi"];
        }

        /// <summary>
        /// Shton nje checkbox per selektim rreshtash ne griden e dokumentave kryesore
        /// </summary>
        protected void grid_dokKryesor_DataBound(object sender, EventArgs e)
        {
            if (grid_dokKryesor.Columns["#"] != null) return;

            var check = new GridViewCommandColumn("#")
            {
                ShowSelectCheckbox = true,
                Width = Unit.Percentage(2)
            };
            check.Width = 20;

            grid_dokKryesor.Columns.Add(check);
        }

        /// <summary>
        /// Kthen id-te e dokumentave kryesore te selektuar
        /// </summary>
        private ArrayList KtheIdDokKryesoreSelektuar()
        {
            var ids = new ArrayList();
            for (var i = 0; i < grid_dokKryesor.Selection.Count; i++)
            {
                if (grid_dokKryesor.GetSelectedFieldValues("IdDokumenti")[i] == null) continue;

                ids.Add(grid_dokKryesor.GetSelectedFieldValues("IdDokumenti")[i].ToString());
            }
            return ids;
        }

        /// <summary>
        /// Shton nje checkbox per selektim rreshtash ne griden e dokumentave vartes
        /// </summary>
        protected void grid_dokLidhes_DataBound(object sender, EventArgs e)
        {
            if (grid_dokLidhes.Columns["#"] != null) return;

            var check = new GridViewCommandColumn("#")
            {
                ShowSelectCheckbox = true,
                Width = Unit.Percentage(2)
            };
            check.Width = 20;

            grid_dokLidhes.Columns.Add(check);
        }

        /// <summary>
        /// Kur ruhet veprimi i lidhjes.
        /// </summary>  //nuk perdoret me sepse eshte hequr popup qe shfaqte diferencat nga kursi
        protected void ruajLidhje_Button_Click(object sender, EventArgs e) => RuajLidhje(1);

        /// <summary>
        /// ruan lidhen
        /// </summary>
        /// <param name="idstatus"></param>
        private void RuajLidhje(int idstatus)
        {
            if (!Page.IsValid) return;

            var konfigkoka = new clsKonfigurimAmbjenti();
            konfigkoka.mbushKonfigAmbjSipasKod(konfigurimi_ComboBox.Text, IdNdermarrja);

            if (DbCore.MbylljePeriudhe.PeriodClosing.IsPeriodClosed(dtDokumenti_DateEdit.Date, MyConnectionsManager.GetSelectedConNameServer(), IdNdermarrja, KategoriDokumenti.LidhjeDokumentash, konfigkoka.IdKonfigAmbjente))
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgPeriodIsClosed"], pnlMesazhi);
                hfStatusRuajtje.Value = "false";
                return;
            }

            hfKuadruar.Value = "true";

            var VDKtotale = LlogaritVdkTotale();

            if (VDKtotale != 0)
                hfDiferenca.Value = "true";
            else
            {
                hfDiferenca.Value = "false";
                hfMeKontabilizim.Value = "0";
            }

            double totali, totalilidhes;
            var dokKryesore = MerrDokumentatkryesore(out totali);
            var idmonedha = dokKryesore.Count != 0 ? dokKryesore[0].IdMonedha : 0;
            var dokLidhes = MerrDokumentatLidhes(out totalilidhes, idmonedha);

            if (dokKryesore.Count == 0 || dokLidhes.Count == 0)
            {
                hfStatusRuajtje.Value = "false";
                return;
            }

            if (Math.Round(totalilidhes, 2) != Math.Round(totali, 2))
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgVeprimiNukEshteIKuadruar", ci), pnlMesazhi);
                hfStatusRuajtje.Value = "false";
                return;
            }

            if (dtDokumenti_DateEdit.Date.Year != new clsNdermarrjeViti(IdNdermarrjeVit).Viti)
            {
                hfStatusRuajtje.Value = "false";
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgLidhjaDokDataNukIPerketVitiUshtrTeZgjedhur", ci), pnlMesazhi);
                return;
            }

            if (hfMeKontabilizim.Value != "0" && hfKuadruar.Value == "true" || hfMeKontabilizim.Value != "0" && hfDiferenca.Value == "false" || hfMeKontabilizim.Value == "0")//veprimi do ruhet nese eshte me kontabilizim dhe i kuadruar, ose nese eshte me kontabilizim por ska diferenca nga kursi, ose nese eshte pa kontabilizim
            {
                var meKontabilizim = hfMeKontabilizim.Value == "1" || hfMeKontabilizim.Value == "2";
                var qend = new clsKokaQendraKosto();

                if (hfShtimModifikim.Value == "modifikim")
                {
                    var kok = new clsKokaFleteKontabel();
                    kok = new clsKokaFleteKontabel(int.Parse(Request.QueryString["id"]), 10);
                    qend.KtheKokaQKSipasIDGjeneruesDheKonfigMeTrup(kok.IdKokaFleteKontabel, kok.IdKonfigAmbjente);
                }


                var dokumentiKoka = new clsDokumentLidhesKoka();

                var controls = this.GetAsPxTextEditIdValue();
                controls.AddRange(ASPxSplitter1.GetAsPxTextEditIdValue());

                hfNrAuto = (ASPxHiddenField)NrAuto.VendosVleratNrAuto(hfNrAuto, controls);
                hfNrAutoShitje = (ASPxHiddenField)NrAuto.ShtoNeRegjistrime(hfNrAutoShitje, hfNrAuto, "nrLidhje_TextBox", "NrLidhje");

                string shfaqmesazhapolupe;
                dokumentiKoka.krijoDokumentLidhes(nrLidhje_TextBox.Text, dtDokumenti_DateEdit.Date, dtRegjistrimi_DateEdit.Date, int.Parse(grid_dokKryesor.GetSelectedFieldValues("IdKlientFurnitori")[0].ToString()), 0, 10, IdNdermarrja, IdNdermarrjeVit, konfigkoka.IdNivel, konfigkoka.IdKonfigAmbjente, 0, 0, 0, idstatus, colDokumentLidhesTrupi.krijoTrup(dokLidhes, dokKryesore), mySessionObjects.ktheIdPerdoruesi(Session), dokLidhes, dokKryesore, mySessionObjects.merrPeriudheKontabel(Session).IdPeriudha, totali, meKontabilizim, out shfaqmesazhapolupe, qend.ColTrupi, IdGjuha, rm, ci);

                var tedrejtaInfo = new clsTeDrejtaRoli();

                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, Komponente);
                if (!tedrejtaInfo.DShtim)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNukKeniTeDrejta", ci), pnlMesazhi);
                    hfStatusRuajtje.Value = "false";
                    return;
                }

                var mesazh2 = dokumentiKoka.ruaj(meKontabilizim, hfNrAutoShitje, IdPerdoruesi);

                if (!mesazh2.Status)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh2.PershkrimMesazhi, pnlMesazhi);
                    hfStatusRuajtje.Value = "false";
                }

                hfStatusRuajtje.Value = "true";
                hfqkmesazhiVDK.Value = shfaqmesazhapolupe;

                if (shfaqmesazhapolupe != "jo")
                {
                    var kok = new clsKokaFleteKontabel(dokumentiKoka.IdKoka, 10);
                    hfUrlVDK.Value += "LupaRegjistrimQendraKosto.aspx?shtim_modifikim=modifikim&idDokGjenerues=" + kok.IdKokaFleteKontabel + "&idkonfig=" + kok.IdKonfigAmbjente + ";";
                }

                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh2.PershkrimMesazhi, pnlMesazhi);
                hl = new HtmlTable();
                pnlLidhur.Update();
            }
        }

        /// <summary>
        /// Llogarit diferencen e kursit sipas formules
        /// </summary>
        /// <param name="kv">kursi i dokumentit vartes</param>
        /// <param name="kk">kursi i dokumentit kryesor</param>
        /// <param name="kmk">kursi i dokumentit kryesor ne daten e dokumentit vartes</param>
        /// <param name="vv">vlera e dokumentit vartes</param>
        protected double LlogaritDiferenceKursi(double kv, double kk, double kmk, double vv)
        {
            if (kmk != 0)
                return vv * kv - vv * kv * kk / kmk;
            return 0;
        }

        /// <summary>
        /// Kthen kahun per dokumentat
        /// </summary>
        /// <param name="idNiveli">Niveli i dokumentit</param>
        /// <param name="idDok">ID e dokumentit</param>
        /// <returns>Kthen nje string "Debi" ose "Kredi" qe tregon kahun e dokumentit</returns>
        protected string KtheKahunDokumentitSipasIdDok(int idNiveli, int idDok)
        {
            var kahu = "";
            var idKatDok = clsNivelRegjistrimi.ktheIdKategoriaNivelRegjistrimi(idNiveli);
            var dokumenti = new clsKokaShitje();

            switch (idKatDok)
            {
                case 1: //Shitje   
                    dokumenti.mbushKokaShitjeSipasIDPaTrup(idDok);
                    if (dokumenti.Totali < 0)
                        kahu = "Kredi";//"-Debi";
                    else
                    if (dokumenti.Totali > 0)
                        kahu = "Debi";
                    break;
                case 2: //Blerje
                    dokumenti.mbushKokaShitjeSipasIDPaTrup(idDok);
                    if (dokumenti.Totali < 0)
                        kahu = "Debi";// "-Kredi";
                    else
                    if (dokumenti.Totali > 0)
                        kahu = "Kredi";
                    break;
                case 3:
                case 4://Arka ose Banka
                    var dokumentBanke = new clsVeprimBankaKoka(idDok);
                    if (dokumentBanke.LlojiVeprimit == "Derdhje" || dokumentBanke.LlojiVeprimit == "Arketim")
                        kahu = "Kredi";
                    else
                    if (dokumentBanke.LlojiVeprimit == "Terheqje" || dokumentBanke.LlojiVeprimit == "Pagese")
                        kahu = "Debi";
                    break;
                case 20://veprimekf
                    var dokumentKf = new colVeprimeKFTrupi();
                    dokumentKf.MbushVeprimeKfTrupi(idDok);
                    if (dokumentKf[0].DebiKredi == 1)
                        kahu = "Debi";
                    else if (dokumentKf[0].DebiKredi == 2)
                        kahu = "Kredi";
                    break;
            }

            return kahu;
        }

        /// <summary>
        /// Kontrollon nese dokumentat e selektuar te grida e dokumentave kryesore kane kah te njejte apo jo
        /// </summary>
        /// <returns>Kthen true nese kane kah te njejte, perndryshe false</returns>
        protected bool KaneKahTeNjejte()
        {
            var njejte = false;
            var kahet = new ArrayList();
            var debi = 0;
            var kredi = 0;

            for (var i = 0; i < grid_dokKryesor.Selection.Count; i++)
            {
                var niveli = grid_dokKryesor.GetSelectedFieldValues("IdNiveli")[i].ToString();
                var idDok = int.Parse(grid_dokKryesor.GetSelectedFieldValues("IdDokumenti")[i].ToString());
                var kahuDok = KtheKahunDokumentitSipasIdDok(int.Parse(niveli), idDok);
                if (kahuDok == "Kredi") kredi++;
                if (kahuDok == "Debi") debi++;
                kahet.Add(kahuDok);
            }

            if (kahet.Count == kredi || kahet.Count == debi)
                njejte = true;

            return njejte;
        }

        protected void grid_dokLidhes_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
        }

        /// <summary>
        /// Template per griden e dokumentave kryesore
        /// </summary>
        private void PercaktoTemplateKryesor()
        {
            var col1 = grid_dokKryesor.Columns["IdNiveli"] as GridViewDataComboBoxColumn;
            col1.DataItemTemplate = new MyLabelTemplate();

            var col19 = grid_dokKryesor.Columns["IdKonfigAmbjente"] as GridViewDataComboBoxColumn;
            col19.DataItemTemplate = new MyLabelTemplate();

            var col2 = grid_dokKryesor.Columns["NrDokumenti"] as GridViewDataTextColumn;
            col2.DataItemTemplate = new MyLabelTemplate();

            var col3 = grid_dokKryesor.Columns["Kursi"] as GridViewDataTextColumn;
            col3.DataItemTemplate = new MyLabelTemplate();

            var col4 = grid_dokKryesor.Columns["VleftaPaLikujduar"] as GridViewDataTextColumn;
            col4.DataItemTemplate = new MyLabelTemplate();

            var col5 = grid_dokKryesor.Columns["Vlefta"] as GridViewDataTextColumn;
            col5.DataItemTemplate = new MyLabelTemplate();
            col5.Caption = "Vlefta";

            var col15 = grid_dokKryesor.Columns["KursAzhornimi"] as GridViewDataTextColumn;
            col15.DataItemTemplate = new MyLabelGTemplate();
            col15.Caption = "Kurs Azhornimi";

            var col9 = grid_dokKryesor.Columns["VleftaLikuiduar"] as GridViewDataTextColumn;

           // col9.DataItemTemplate = hfShtimModifikim.Value == "shtim"
          //      ? (ITemplate)new MyDoubleTemplate(false, 2, "0")
           //     : new MyLabelTemplate();
            col9.DataItemTemplate = (ITemplate)new MyDoubleTemplate(false, 2, "0");
            col9.Caption = "Vlefta e lidhjes";

            var col10 = grid_dokKryesor.Columns["VleftaLikuiduarMon"] as GridViewDataTextColumn;
            col10.DataItemTemplate = new MyDoubleTemplate(false, 2, "0");
            col10.Visible = false;

            var col7 = grid_dokKryesor.Columns["IdMonedha"] as GridViewDataComboBoxColumn;
            col7.DataItemTemplate = new MyLabelTemplate();

            var col8 = grid_dokKryesor.Columns["DtDokumenti"] as GridViewDataDateColumn;
            col8.DataItemTemplate = new MyLabelTemplate();

            var col18 = grid_dokKryesor.Columns["DtAzhornimi"] as GridViewDataDateColumn;
            col18.DataItemTemplate = new MyDateGTemplate("");

            if (hfShtimModifikim.Value == "modifikim")
            {
                grid_dokKryesor.Columns["KursAzhornimi"].SetColVisible(false);
                grid_dokKryesor.Columns["DtAzhornimi"].SetColVisible(false);
                grid_dokKryesor.Columns["Kursi"].SetColVisible(false);
            }
        }

        /// <summary>
        /// Template per griden e dokumentave vartes
        /// </summary>
        private void PercaktoTemplateLidhes()
        {
            var col1 = grid_dokLidhes.Columns["IdNiveli"] as GridViewDataComboBoxColumn;
            col1.DataItemTemplate = new MyLabelTemplate();

            var col19 = grid_dokLidhes.Columns["IdKonfigAmbjente"] as GridViewDataComboBoxColumn;
            col19.DataItemTemplate = new MyLabelTemplate();

            var col2 = grid_dokLidhes.Columns["NrDokumenti"] as GridViewDataTextColumn;
            col2.DataItemTemplate = new MyLabelTemplate();

            var col3 = grid_dokLidhes.Columns["Kursi"] as GridViewDataTextColumn;
            col3.DataItemTemplate = new MyLabelTemplate();

            var col4 = grid_dokLidhes.Columns["VleftaPaLikujduar"] as GridViewDataTextColumn;
            col4.DataItemTemplate = new MyLabelTemplate();

            var col5 = grid_dokLidhes.Columns["Vlefta"] as GridViewDataTextColumn;
            col5.DataItemTemplate = new MyLabelTemplate();
            col5.Caption = "Vlefta";

            var col9 = grid_dokLidhes.Columns["VleftaLikuiduar"] as GridViewDataTextColumn;

            //col9.DataItemTemplate = hfShtimModifikim.Value == "shtim"
            //    ? (ITemplate)new MyDoubleTemplate(false, 2, "0")
             //   : new MyLabelTemplate();
            col9.DataItemTemplate = (ITemplate)new MyDoubleTemplate(false, 2, "0");
            col9.Caption = "Vlefta e lidhjes";

            var col10 = grid_dokLidhes.Columns["VleftaLikuiduarMon"] as GridViewDataTextColumn;
            //col10.DataItemTemplate = hfShtimModifikim.Value == "shtim"
            //    ? (ITemplate)new MyDoubleTemplate(false, 2, "0")
            //    : new MyLabelTemplate();
            col10.DataItemTemplate = (ITemplate)new MyDoubleTemplate(false, 2, "0");

            var col7 = grid_dokLidhes.Columns["IdMonedha"] as GridViewDataComboBoxColumn;
            col7.DataItemTemplate = new MyLabelTemplate();

            var col8 = grid_dokLidhes.Columns["DtDokumenti"] as GridViewDataDateColumn;
            col8.DataItemTemplate = new MyLabelTemplate();

            var col15 = grid_dokLidhes.Columns["KursAzhornimi"] as GridViewDataTextColumn;
            col15.DataItemTemplate = new MyLabelGTemplate();
            col15.Caption = "Kurs Azhornimi";
            col15.Visible = false;

            var col18 = grid_dokLidhes.Columns["DtAzhornimi"] as GridViewDataDateColumn;
            col18.DataItemTemplate = new MyDateGTemplate("");
            col18.Visible = false;

            if (hfShtimModifikim.Value == "modifikim")
            {
                grid_dokLidhes.Columns["Kursi"].SetColVisible(false);
            }            
        }

        /// <summary>
        /// I vendos nje emer identifikues kontrolleve te grides se dok kryesore dhe percakton funksinet qe manipulojne ato ne client side
        /// </summary>
        protected void grid_dokKryesor_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType == GridViewRowType.Data)
            {
                var col1 = ((ASPxGridView)sender).Columns["IdNiveli"] as GridViewDataComboBoxColumn;
                var lbl1 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col1, "lbl") as ASPxLabel;
                var col2 = ((ASPxGridView)sender).Columns["NrDokumenti"] as GridViewDataTextColumn;
                var lbl2 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col2, "lbl") as ASPxLabel;
                var col3 = ((ASPxGridView)sender).Columns["Kursi"] as GridViewDataTextColumn;
                var lbl3 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col3, "lbl") as ASPxLabel;
                var col4 = ((ASPxGridView)sender).Columns["VleftaPaLikujduar"] as GridViewDataTextColumn;
                var lbl4 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col4, "lbl") as ASPxLabel;
                var col5 = ((ASPxGridView)sender).Columns["Vlefta"] as GridViewDataTextColumn;
                var lbl7 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col5, "lbl") as ASPxLabel;
                var col9 = ((ASPxGridView)sender).Columns["VleftaLikuiduar"] as GridViewDataTextColumn;
                var txt = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col9, "txtBox") as ASPxTextBox;
                var txt1 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col9, "lbl") as ASPxLabel;
                var col7 = ((ASPxGridView)sender).Columns["IdMonedha"] as GridViewDataComboBoxColumn;
                var lbl5 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col7, "lbl") as ASPxLabel;
                var col8 = ((ASPxGridView)sender).Columns["DtDokumenti"] as GridViewDataDateColumn;
                var lbl6 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col8, "lbl") as ASPxLabel;
                var col18 = ((ASPxGridView)sender).Columns["DtAzhornimi"] as GridViewDataDateColumn;
                var lbl16 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col18, "lbl") as ASPxLabel;
                var col13 = ((ASPxGridView)sender).Columns["KursAzhornimi"] as GridViewDataTextColumn;
                var lbl13 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col13, "lbl") as ASPxLabel;
                var trup = new clsFormatKonfigTrup(clsFormatKonfigTrup.defaultFormatSasia, clsFormatKonfigTrup.defaultFormatCmimi, clsFormatKonfigTrup.defaultFormatVlefta, clsFormatKonfigTrup.defaultFormatZbritja, clsFormatKonfigTrup.defaultFormatStringSasia, clsFormatKonfigTrup.defaultFormatStringCmimi, clsFormatKonfigTrup.defaultFormatStringVlefta, clsFormatKonfigTrup.defaultFormatStringZbritja);
                var formatkursi = 2;

                if (lbl5 != null)
                {
                    var formatNrPerKonfig = (clsFormatiKonfig)mySessionObjects.merrFormatNRSesioni(Session)[0];
                    var mon = (colMonedhat)mySessionObjects.merrFormatNRSesioni(Session)[1];

                    foreach (var format in formatNrPerKonfig.KonfigTrupi)
                        if (format.KodMonedhe == lbl5.Text)
                        {
                            trup = format;
                            break;
                        }
                    foreach (var m in mon)
                        if (m.KodiMonedha == lbl5.Text)
                        {
                            formatkursi = m.IdFormatNrKursi = clsFunksione.MerrVleraFormatKursi(m.IdMonedha);
                            break;
                        }
                }

                if (lbl1 != null)
                    lbl1.ClientInstanceName = "lblNiveli" + e.VisibleIndex;

                if (lbl2 != null)
                    lbl2.ClientInstanceName = "lblNrDokumenti" + e.VisibleIndex;

                if (lbl6 != null)
                    lbl6.ClientInstanceName = "lblDtDokumenti" + e.VisibleIndex;

                if (lbl16 != null)
                {
                    if (lbl16.Text != "")
                        if (DateTime.Parse(lbl6.Text) <= DateTime.Parse(lbl16.Text))
                            lbl16.ForeColor = Color.Green;

                    lbl16.ClientInstanceName = "lblDtAzhornimi" + e.VisibleIndex;
                }

                if (lbl3 != null)
                {
                    if (lbl16.Text == "" || DateTime.Parse(lbl6.Text) > DateTime.Parse(lbl16.Text))
                        lbl3.ForeColor = Color.Green;

                    lbl3.ClientInstanceName = "lblKursi" + e.VisibleIndex;
                    lbl3.Text = Convert.ToDouble(lbl3.Text).ToString("F" + formatkursi);
                }

                if (lbl13 != null)
                {
                    if (lbl16.Text != "")
                        if (DateTime.Parse(lbl6.Text) <= DateTime.Parse(lbl16.Text))
                            lbl13.ForeColor = Color.Green;

                    lbl13.ClientInstanceName = "lblKursAzhornimi" + e.VisibleIndex;

                    if (lbl13.Text != "")
                        lbl13.Text = Convert.ToDouble(lbl13.Text).ToString("F" + formatkursi);
                }

                if (lbl4 != null)
                {
                    lbl4.ClientInstanceName = "lblVleftaPaLikujduar" + e.VisibleIndex;
                    lbl4.Text = Convert.ToDouble(lbl4.Text).ToString("F" + trup.ShifraPasPresjesVlefta);
                }
                if (lbl7 != null)
                    lbl7.Text = Convert.ToDouble(lbl7.Text).ToString("F" + trup.ShifraPasPresjesVlefta);

                if (lbl5 != null)
                    lbl5.ClientInstanceName = "lblMonedha" + e.VisibleIndex;

                if (txt1 != null)
                    txt1.Text = Convert.ToDouble(txt1.Text).ToString("F" + trup.ShifraPasPresjesVlefta);

                if (txt != null)
                {
                    txt.ClientInstanceName = "txtVlefta" + e.VisibleIndex;
                    txt.ClientEnabled = hfShtimModifikim.Value == "shtim";
                    txt.DisplayFormatString = clsFunksione.krijoNumer(trup.ShifraPasPresjesVlefta, "0");
                    txt.ClientSideEvents.TextChanged = "function(s,e){TextChangedVlefta(txtVlefta" + e.VisibleIndex + ", " + e.VisibleIndex + ");}";
                }
            }

            if (_temptxt != null)
                _temptxt.Focus();
            else if (_tempcombo != null)
                _tempcombo.Focus();
            else if (_temptxtNormal != null)
                _temptxtNormal.Focus();
        }

        /// <summary>
        /// I vendos nje emer identifikues kontrolleve te grides se dok vartes dhe percakton funksinet qe manipulojne ato ne client side
        /// </summary>
        protected void grid_dokLidhes_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType == GridViewRowType.Data)
            {
                var col1 = ((ASPxGridView)sender).Columns["IdNiveli"] as GridViewDataComboBoxColumn;
                var lbl1 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col1, "lbl") as ASPxLabel;
                var col2 = ((ASPxGridView)sender).Columns["NrDokumenti"] as GridViewDataTextColumn;
                var lbl2 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col2, "lbl") as ASPxLabel;
                var col3 = ((ASPxGridView)sender).Columns["Kursi"] as GridViewDataTextColumn;
                var lbl3 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col3, "lbl") as ASPxLabel;
                var col4 = ((ASPxGridView)sender).Columns["VleftaPaLikujduar"] as GridViewDataTextColumn;
                var lbl4 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col4, "lbl") as ASPxLabel;
                var col5 = ((ASPxGridView)sender).Columns["Vlefta"] as GridViewDataTextColumn;
                var lbl7 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col5, "lbl") as ASPxLabel;
                var col9 = ((ASPxGridView)sender).Columns["VleftaLikuiduar"] as GridViewDataTextColumn;
                var txt = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col9, "txtBox") as ASPxTextBox;
                var txt1 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col9, "lbl") as ASPxLabel;
                var col10 = ((ASPxGridView)sender).Columns["VleftaLikuiduarMon"] as GridViewDataTextColumn;
                var lbl10 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col10, "txtBox") as ASPxTextBox;
                var txt2 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col10, "lbl") as ASPxLabel;
                var col7 = ((ASPxGridView)sender).Columns["IdMonedha"] as GridViewDataComboBoxColumn;
                var lbl5 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col7, "lbl") as ASPxLabel;
                var col8 = ((ASPxGridView)sender).Columns["DtDokumenti"] as GridViewDataDateColumn;
                var lbl6 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col8, "lbl") as ASPxLabel;

                var trup = new clsFormatKonfigTrup(clsFormatKonfigTrup.defaultFormatSasia, clsFormatKonfigTrup.defaultFormatCmimi, clsFormatKonfigTrup.defaultFormatVlefta, clsFormatKonfigTrup.defaultFormatZbritja, clsFormatKonfigTrup.defaultFormatStringSasia, clsFormatKonfigTrup.defaultFormatStringCmimi, clsFormatKonfigTrup.defaultFormatStringVlefta, clsFormatKonfigTrup.defaultFormatStringZbritja);
                var formatkursi = 2;

                if (lbl5 != null)
                {
                    var formatNrPerKonfig = (clsFormatiKonfig)mySessionObjects.merrFormatNRSesioni(Session)[0];
                    var mon = (colMonedhat)mySessionObjects.merrFormatNRSesioni(Session)[1];

                    foreach (var format in formatNrPerKonfig.KonfigTrupi)
                        if (format.KodMonedhe == lbl5.Text)
                        {
                            trup = format;
                            break;
                        }
                    foreach (var m in mon)
                        if (m.KodiMonedha == lbl5.Text)
                        {
                            formatkursi = clsFunksione.MerrVleraFormatKursi(m.IdMonedha);
                            break;
                        }
                }

                if (lbl1 != null)
                    lbl1.ClientInstanceName = "lblNiveliLidhes" + e.VisibleIndex;

                if (lbl2 != null)
                    lbl2.ClientInstanceName = "lblNrDokumentiLidhes" + e.VisibleIndex;

                if (lbl3 != null)
                {
                    lbl3.ClientInstanceName = "lblKursiLidhes" + e.VisibleIndex;
                    lbl3.Text = Convert.ToDouble(lbl3.Text).ToString("F" + formatkursi);
                }

                if (lbl4 != null)
                {
                    lbl4.ClientInstanceName = "lblVleftaPaLikujduarLidhes" + e.VisibleIndex;
                    lbl4.Text = Convert.ToDouble(lbl4.Text).ToString("F" + trup.ShifraPasPresjesVlefta);
                }

                if (lbl5 != null)
                    lbl5.ClientInstanceName = "lblMonedhaLidhes" + e.VisibleIndex;

                if (lbl6 != null)
                    lbl6.ClientInstanceName = "lblDtDokumentiLidhes" + e.VisibleIndex;

                if (lbl7 != null)
                    lbl7.Text = Convert.ToDouble(lbl7.Text).ToString("F" + trup.ShifraPasPresjesVlefta);

                if (txt1 != null)
                    txt1.Text = Convert.ToDouble(txt1.Text).ToString("F" + trup.ShifraPasPresjesVlefta);

                if (txt2 != null)
                    txt2.Text = Convert.ToDouble(txt2.Text).ToString("F" + trup.ShifraPasPresjesVlefta);

                if (txt != null)
                {
                    txt.ClientEnabled = hfShtimModifikim.Value == "shtim";
                    txt.ClientInstanceName = "txtVleftaLidhes" + e.VisibleIndex;
                    txt.DisplayFormatString = clsFunksione.krijoNumer(trup.ShifraPasPresjesVlefta, "0");
                    txt.ClientSideEvents.TextChanged = "function(s,e){TextChangedVleftaLidhes(txtVleftaLidhes" + e.VisibleIndex + ", " + e.VisibleIndex + ");}";
                }

                if (lbl10 != null)
                {
                    lbl10.ClientInstanceName = "txtVleftaMonBazeLidhes" + e.VisibleIndex;
                    lbl10.DisplayFormatString = clsFunksione.krijoNumer(trup.ShifraPasPresjesVlefta, "0");
                    lbl10.ClientEnabled = hfShtimModifikim.Value == "shtim";

                    if (hfShtimModifikim.Value != "modifikim" && decimal.Parse(lbl3.Text) != 0)    //vendos vleften dokumenti lidhes ne monedhe dok kryesor
                    {
                        var idmon = int.Parse(grid_dokKryesor.GetSelectedFieldValues("IdMonedha")[0].ToString());
                        if (clsMonedha.ktheKodMonedheSipasId(idmon) == lbl5.Text)
                            lbl10.Text = Math.Round((decimal.Parse(txt.Text)), 2).ToString();
                        else
                        {
                            var kursi = new clsKurset(idmon, DateTime.Parse(lbl6.Text));

                            if (kursi.VleraKursi == 0)
                                kursi.VleraKursi = 1;

                            lbl10.Text = Math.Round((decimal.Parse(txt.Text) * decimal.Parse(lbl3.Value.ToString()) / decimal.Parse((kursi.VleraKursi).ToString())), 2).ToString();
                        }
                    }
                }
            }

            if (_temptxt != null)
                _temptxt.Focus();
            else if (_tempcombo != null)
                _tempcombo.Focus();
            else if (_temptxtNormal != null)
                _temptxtNormal.Focus();
        }

        private colDokumentat MerrDokumentatkryesore(out double totali)
        {
            totali = 0;
            var serializusi = new JavaScriptSerializer { MaxJsonLength = 500000000 };
            var nivel = (object[])serializusi.DeserializeObject(hfNiveli.Value);
            var nrdok = (object[])serializusi.DeserializeObject(hfNrDokumenti.Value);
            var vleftapalikuiduar = (object[])serializusi.DeserializeObject(hfVleftaPaLikujduar.Value);
            var vlefta = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(hfVlefta.Value);
            var kursi = (object[])serializusi.DeserializeObject(hfKursi.Value);
            var monedha = (object[])serializusi.DeserializeObject(hfMonedha.Value);
            var dtdok = (object[])serializusi.DeserializeObject(hfDtDokumenti.Value);
            var id = JsonConvert.DeserializeObject<List<string>>(hfIdDokKryesor.Value);
            var col = new colDokumentat();

            for (var i = 0; i < nivel.Length; i++)
            {
                if (nivel[i] == null) continue;

                var dok = new clsDokumenti(nivel[i], nrdok[i], dtdok[i], vleftapalikuiduar[i], GetStringValueById(vlefta, int.Parse(id[i])), kursi[i], monedha[i], IdNdermarrja, Convert.ToInt32(klientFurnitor_ButtonEdit.Value), id[i], new clsDatabaseRegjistrim());
                totali += dok.Vlefta;
                if (dok.Vlefta == 0)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Nuk lejohet vlere lidhje 0!", pnlMesazhi);
                    return new colDokumentat();
                }
                if (dok.IdNiveli != -1) col.Add(dok);
            }

            return col;
        }

        private colDokumentat MerrDokumentatLidhes(out double totali, int idmonedhakryesore)
        {
            totali = 0;
            var serializusi = new JavaScriptSerializer { MaxJsonLength = 500000000 };
            var nivel = (object[])serializusi.DeserializeObject(hfNiveli1.Value);
            var nrdok = (object[])serializusi.DeserializeObject(hfNrDokumenti1.Value);
            var vleftapalikuiduar = (object[])serializusi.DeserializeObject(hfVleftaPaLikujduar1.Value);
            var vlefta = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(hfVlefta1.Value);
            var kursi = (object[])serializusi.DeserializeObject(hfKursi1.Value);
            var monedha = (object[])serializusi.DeserializeObject(hfMonedha1.Value);
            var dtdok = (object[])serializusi.DeserializeObject(hfDtDokumenti1.Value);
            var id = JsonConvert.DeserializeObject<List<string>>(hfIdDokLidhes.Value);
            var col = new colDokumentat();
            for (var i = 0; i < nivel.Length; i++)
            {
                if (nivel[i] == null) continue;

                var dok = new clsDokumenti(nivel[i], nrdok[i], dtdok[i], vleftapalikuiduar[i], GetStringValueById(vlefta, int.Parse(id[i])), kursi[i], monedha[i], IdNdermarrja, Convert.ToInt32(klientFurnitor_ButtonEdit.Value), id[i], new clsDatabaseRegjistrim());

                if (idmonedhakryesore == dok.IdMonedha)
                    totali += dok.Vlefta;
                else
                {
                    var kursit = new clsKurset(idmonedhakryesore, dok.DtDokumenti);
                    if (kursit.VleraKursi == 0)
                        kursit.VleraKursi = 1;
                    totali += dok.Vlefta * dok.Kursi / kursit.VleraKursi;
                }

                if (dok.Vlefta == 0)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Nuk lejohet vlere lidhje 0!", pnlMesazhi);
                    return new colDokumentat();
                }

                if (dok.IdNiveli != -1)
                    col.Add(dok);
            }

            return col;
        }

        protected void grid_dokKryesor_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpNoRows"] = grid_dokKryesor.VisibleRowCount;
            e.Properties["cpNoPage"] = grid_dokKryesor.PageIndex;
        }

        protected void grid_dokLidhes_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e) =>
            e.Properties["cpNoRows"] = grid_dokLidhes.VisibleRowCount;

        /// <summary>
        /// Vendos ne griden e llogarive collectionin e llogarive qe preken nga diferencat nga kursi
        /// </summary>
        protected void grid_llogarite_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
        }

        /// <summary>
        /// Mbledh diferencat nga kursi per te gjithe dokumentat e selektuar.
        /// </summary>
        /// <returns>Kthen shumen e diferencave nga kursi</returns>
        private double LlogaritVdkTotale()
        {
            double vdKtotale = 0;

            double totali;
            var dokumentatKryesore = MerrDokumentatkryesore(out totali);

            var idmonedha = 0;
            if (dokumentatKryesore.Count != 0)
                idmonedha = dokumentatKryesore[0].IdMonedha;

            double totalilidhes;
            var dokumentatLidhes = MerrDokumentatLidhes(out totalilidhes, idmonedha);

            if (dokumentatKryesore.Count == 1 && dokumentatLidhes.Count >= 1)
            {
                vdKtotale = 0;
                var kursiDokKryesor = dokumentatKryesore[0].Kursi;
                var monedhaDokKryesor = dokumentatKryesore[0].IdMonedha;
                foreach (var dok in dokumentatLidhes)
                {
                    var kurs = new clsKurset(monedhaDokKryesor, dok.DtDokumenti);
                    var kmk = dok.IdMonedha == monedhaDokKryesor ? dok.Kursi : kurs.VleraKursi;
                    var vdk = LlogaritDiferenceKursi(dok.Kursi, kursiDokKryesor, kmk, dok.Vlefta);
                    vdKtotale += vdk;
                }
            }
            else if (dokumentatKryesore.Count >= 1 && dokumentatLidhes.Count == 1)
            {
                vdKtotale = 0;
                var vleftaLidhjesDokLidhes = dokumentatLidhes[0].Vlefta;
                var kursiDokLidhes = dokumentatLidhes[0].Kursi;
                var dataDokLidhes = dokumentatLidhes[0].DtDokumenti;

                foreach (var dok in dokumentatKryesore)
                {
                    var kurs = new clsKurset(dok.IdMonedha, dataDokLidhes);
                    var kmk = dokumentatLidhes[0].IdMonedha == dok.IdMonedha ? kursiDokLidhes : kurs.VleraKursi;
                    var vdk = LlogaritDiferenceKursi(kursiDokLidhes, dok.Kursi, kmk, vleftaLidhjesDokLidhes);
                    vdKtotale += vdk;
                }
            }

            return vdKtotale;
        }

        private static string GetStringValueById(List<Dictionary<string, object>> myArrObj, int id)
        {
            foreach (var myTmpObj in myArrObj)
            {
                if (int.Parse(myTmpObj["id"].ToString()) == id)
                    return myTmpObj["vlefta"].ToString();
            }

            return "";
        }

        private int GetIdKonfigurimi()
        {
            if (konfigurimi_ComboBox.Value == null)
                return ktheIdKonfigurimiSipasKoditOseDefault();
            else
            {
                int id;
                bool konvertim = int.TryParse(konfigurimi_ComboBox.Value.ToString(), out id);
                if (konvertim)
                    return id;
                else
                    return ktheIdKonfigurimiSipasKoditOseDefault();
            }
        }

        private int ktheIdKonfigurimiSipasKoditOseDefault()
        {
            if (konfigurimi_ComboBox.Text == String.Empty)
                return DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod("DL", IdNdermarrja);
            else
                return DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(konfigurimi_ComboBox.Text, IdNdermarrja);
        }


        /// <summary>
        /// Tregon veprimin qe kryhet kur behat callback i grides se dok kryesore (psh kur ndryshon selection-i)
        /// </summary>
        protected void grid_dokKryesor_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            grid_dokKryesor.PercaktoTitlePanelMePeriudhe(this, MenuInfo, pnlMesazhi, hfState, IdPerdoruesi, IdNdermarrja, IdViti, IdGjuha, GetIdKonfigurimi(), Komponente, 217, "IDSHITJEKOKA", rm, ci, true, false, true);
            var vlefta = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(hfVlefta.Value);
            var colDok = new colDokumentat();
            string kf = string.Empty;
            if (e.Args.Length == 0)
                colDok = grid_dokKryesor.MerrDataSourceMePeriduheNeSession<colDokumentat>(Session, Komponente, Periudha, guidString);
                if (colDok == null)
                colDok.MbushGjitheDokumentatKryesore(IdNdermarrja, Periudha.DataDokNga, Periudha.DataDokDeri);
            else
            {
                var argumentat = e.Args[0].Split(';');

                if (argumentat.Length == 2 && argumentat[1] == "select")//kur selekton nje dokument kryesor
                {
                    var filtriFillestar = grid_dokKryesor.FilterExpression;
                    mySessionObjects.ruajFilterNeSessioni(Session, grid_dokKryesor.FilterExpression);

                    if (grid_dokKryesor.GetRowValues(grid_dokKryesor.FocusedRowIndex, "IdKlientFurnitori") != null)
                    {
                        kf = grid_dokKryesor.GetRowValues(grid_dokKryesor.FocusedRowIndex, "IdKlientFurnitori").ToString();
                        if (!filtriFillestar.Contains("[IdKlientFurnitori] = " + kf + ""))
                        {
                            if (filtriFillestar != "")
                                grid_dokKryesor.FilterExpression = filtriFillestar + " And [IdKlientFurnitori] = " + kf + "";
                            else if (filtriFillestar == "")
                                grid_dokKryesor.FilterExpression = "[IdKlientFurnitori] = " + kf + "";
                        }

                        colDok.mbushGjitheDokumentatSipasKlientit(int.Parse(kf), IdNdermarrja, Periudha.DataDokNga, Periudha.DataDokDeri);
                    }
                }
                else if (argumentat.Length == 1 && argumentat[0] == "unselect")//kur unselecton rreshtin e selektuar me pare
                {
                    if (mySessionObjects.merrFilterNgaSessioni(Session) != null)
                        grid_dokKryesor.FilterExpression = mySessionObjects.merrFilterNgaSessioni(Session);

                    colDok.MbushGjitheDokumentatKryesore(IdNdermarrja, Periudha.DataDokNga, Periudha.DataDokDeri);
                }
                else if (argumentat.Length == 2 && argumentat[0] == "unselect" && argumentat[1] == "shtim") //unselect dhe shtim
                {
                    grid_dokKryesor.Selection.UnselectAll();
                    grid_dokKryesor.FilterExpression = String.Empty;
                    mySessionObjects.ruajFilterNeSessioni(Session, String.Empty);
                    colDok.MbushGjitheDokumentatKryesore(IdNdermarrja, Periudha.DataDokNga, Periudha.DataDokDeri);
                }
                else
                    colDok = grid_dokKryesor.MerrDataSourceMePeriduheNeSession<colDokumentat>(Session, Komponente, Periudha, guidString);
                if (colDok == null)
                    colDok.MbushGjitheDokumentatKryesore(IdNdermarrja, Periudha.DataDokNga, Periudha.DataDokDeri);
            }

            if (vlefta != null && vlefta.Count != 0)
                foreach (var d in colDok)
                {
                    var myStringVlefta = GetStringValueById(vlefta, d.IdDokumenti);
                    if (myStringVlefta != "")
                        d.VleftaLikuiduar = double.Parse(myStringVlefta);
                }

            grid_dokKryesor.DataSource = colDok;
            grid_dokKryesor.DataBind();
            grid_dokKryesor.RuajDataSourceMePeriduheNeSession(Session, Komponente, Periudha, colDok, guidString);
            if (kf != string.Empty)
            {
                grid_dokKryesor.Selection.SelectRowByKey(kf);
                grid_dokKryesor.FocusedRowIndex = grid_dokKryesor.FindVisibleIndexByKeyValue(kf);
            }
            KonfiguroGrideKoka(false);
            PercaktoTemplateKryesor();
        }

        /// <summary>
        /// Tregon veprimin qe kryhet kur behet callback i grides se dok vartes (psh kur ndryshon selection-i)
        /// </summary
        protected void grid_dokLidhes_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            var vlefta = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(hfVlefta1.Value);
            var col = new colDokumentat();

            if (e.Args.Length == 0)
                col = new colDokumentat();
            else
            {
                var argumentat = e.Args[0].Split(';');

                if ((argumentat.Length == 2 && argumentat[1] == "select" && grid_dokKryesor.Selection.Count > 0) || e.Args.Length > 1 || e.Args[0].Contains("PN") || e.Args[0] == "")
                {
                    var ids = KtheIdDokKryesoreSelektuar();
                    if (grid_dokKryesor.Selection.Count > 1)
                    {
                        var kf = grid_dokKryesor.GetSelectedFieldValues("IdKlientFurnitori")[0].ToString();
                        if (KaneKahTeNjejte())
                            col.mbushGjitheDokumentatLidhesSipasKlientitDheKahut(int.Parse(kf), IdNdermarrja, KtheKahunDokumentitSipasIdDok(int.Parse(grid_dokKryesor.GetSelectedFieldValues("IdNiveli")[0].ToString()), int.Parse(grid_dokKryesor.GetSelectedFieldValues("IdDokumenti")[0].ToString())));
                        else
                            col.mbushGjitheDokumentatLidhesSipasKlientit(int.Parse(grid_dokKryesor.GetSelectedFieldValues("IdKlientFurnitori")[0].ToString()), IdNdermarrja);
                    }
                    else if (grid_dokKryesor.Selection.Count == 1)
                        col.mbushGjitheDokumentatLidhesSipasKlientitDheKahut(int.Parse(grid_dokKryesor.GetSelectedFieldValues("IdKlientFurnitori")[0].ToString()), IdNdermarrja, KtheKahunDokumentitSipasIdDok(int.Parse(grid_dokKryesor.GetSelectedFieldValues("IdNiveli")[0].ToString()), int.Parse(grid_dokKryesor.GetSelectedFieldValues("IdDokumenti")[0].ToString())));

                    var colnew = new colDokumentat();
                    colnew.AddRange(col.Where(dok => !ids.Contains(dok.IdDokumenti.ToString())));

                    col = colnew;
                }
                else if (argumentat.Length == 1 && argumentat[0] == "unselect")
                {
                    if (grid_dokKryesor.Selection.Count == 0)
                    {
                        col = new colDokumentat();
                        grid_dokLidhes.Selection.UnselectAll();
                    }
                }
                else
                    col = new colDokumentat();
            }

            if (vlefta != null && vlefta.Count != 0)
                foreach (var d in col)
                {
                    var myStringVlefta = GetStringValueById(vlefta, d.IdDokumenti);
                    if (myStringVlefta != "")
                        d.VleftaLikuiduar = double.Parse(myStringVlefta);
                }

            grid_dokLidhes.DataSource = col;
            grid_dokLidhes.DataBind();
            KonfiguroGrideTrupi(false);
            PercaktoTemplateLidhes();
        }

        protected void grid_dokKryesor_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
        }

        protected void grid_dokLidhes_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
        }

        /// <summary>
        /// Combo klient/furnitorit me autocomplete
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void klientFurnitor_ButtonEdit_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (!IsCallback || !Request.Params["__CALLBACKID"].Contains("klientFurnitor_ButtonEdit")) return;

            int value;
            if (e.Value == null || !int.TryParse(e.Value.ToString(), out value))
                return;

            ConfigureAspxComboBox.KonfiguroComboBoxKlientFurnitoriById(IdPerdoruesi, IdNdermarrja, (ASPxComboBox)source, value);
        }

        /// <summary>
        /// Combo klient/furnitorit me autocomplete
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void klientFurnitor_ButtonEdit_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("klientFurnitor_ButtonEdit"))
                ConfigureAspxComboBox.KonfiguroComboBoxKlientFurnitor(e.Filter, e.BeginIndex + 1, e.EndIndex + 1,
                    mySessionObjects.ktheIdPerdoruesi(Session), mySessionObjects.merrIdNdermarrjeSesioni(Session),
                    klientFurnitor_ButtonEdit, 0);
        }

        protected void grid_dokKryesor_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {

        }
    }
}