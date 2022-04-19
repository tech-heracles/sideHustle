using System;
using CacheLayer;
using DbCore;
using DbCore.DbAdmin;
using DbCore.DbArkaBanka;
using DbCore.DbInventari;
using DbCore.DbKontabiliteti;
using DbCore.DbShare;
using DbCore.IMBUtils.Extensions;
using DbCore.IMBUtils.Messages;
using DevExpress.Web;
using Newtonsoft.Json;
using NLog;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class LupaKlientShpejte : MyPageBase
    {
        /// <summary>
        /// thirret kur faqja lodohet. Ne te behet kontrolli nese perdoruesi eshte i loguar ne sistem  dhe nqs jo ridrejtohet tek forma e logimit
        /// thirret inicializimi i konfigurimeve fillestare te faqes
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e"> argumenti</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!mySessionObjects.isLogedIn(Session))
                    clsFunksione.logout(Session, true, "FaqePaautorizuar");


                if (mySessionObjects.ktheKodNdermarrje(Session) == null)
                    Response.Redirect("Login_Ndermarrje.aspx?id=" + IdPerdoruesi);

                if ((Request.QueryString["modifikim"] != null && Request.QueryString["modifikim"] != "") || Request.QueryString["klonim"] != null)
                {
                    hfShtimModifikim.Value = Request.QueryString["klonim"] != null ? "klonim" : "modifikim";

                    var kodi = string.Empty;
                    if (Request.QueryString["kodi"] != null && Request.QueryString["kodi"] != "")
                        kodi = Request.QueryString["kodi"];
                    hfState.Set("Kodi", kodi);
                }
                else
                    hfShtimModifikim.Value = "shtim";

                hfState.Set("idGjuha", IdGjuha);
                hfState.Set("idNdermarrje", IdNdermarrja);
                hfState.Set("idPerdoruesi", IdPerdoruesi);
                hfState.Set("idViti", IdViti);

                KonfiguroVleraFillestare();
                VendosHfMePerkthime();
                hfMeme.Value = mySessionObjects.merrEshteMemeSesioni(Session).ToString();
            }

            PercaktoTemplateMenu();
        }

        private void VendosHfMePerkthime()
        {
            hfState.Set("msgZgjidhgrupinkflupa", MessagesResource.Messages["msgZgjidhgrupinkflupa"]);
            hfState.Set("popupAdministrimiUniversal", MessagesResource.Messages["popupAdministrimiUniversal"]);
            hfState.Set("headerPopUpZgjidhAutorizimet", MessagesResource.Messages["headerPopUpZgjidhAutorizimet"]);
            hfState.Set("msgZgjidhniNdermarjeBij", MessagesResource.Messages["msgZgjidhniNdermarjeBij"]);
            hfState.Set("msgZgjidhArkenBankenLupa", MessagesResource.Messages["msgZgjidhArkenBankenLupa"]);
            hfState.Set("msgNivelCmimiNukPerdoretFurnitore", MessagesResource.Messages["msgNivelCmimiNukPerdoretFurnitore"]);
            hfState.Set("msgNivelCmimiNukPerdoretKliente", MessagesResource.Messages["msgNivelCmimiNukPerdoretKliente"]);
            hfState.Set("regjisDokZgjidhDokPerTeBashkengjitur", MessagesResource.Messages["regjisDokZgjidhDokPerTeBashkengjitur"]);
            hfState.Set("msgNivelCmimiPrindLupa", MessagesResource.Messages["msgNivelCmimiPrindLupa"]);
            hfState.Set("msgZgjidhNivelZbritjeLupa", MessagesResource.Messages["msgZgjidhNivelZbritjeLupa"]);
            hfState.Set("msgZgjidhKatZbritjeLupa", MessagesResource.Messages["msgZgjidhKatZbritjeLupa"]);
            konfigurimi_Label.Text = MessagesResource.Messages["labelBlerjeShitjeKonfigurim"];
            hfState.Set("msgZgjidhAgjentShitjeLupa", MessagesResource.Messages["msgZgjidhAgjentShitjeLupa"]);
        }
        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        private void PercaktoTemplateMenu()
        {
            clsToolbarConfig.percaktoTemplateMenu(IdGjuha, IdViti, IdPerdoruesi, IdNdermarrja, ASPxMenu1, "LupaKlientShpejte.aspx", this, MenuInfo, hfShtimModifikim.Value != "modifikim", true, false, Meme);
            ASPxMenu1.Items.FindByName("Anullo").Text = MessagesResource.Messages["MenuItemMbyll"];
        }

        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e) => PercaktoTemplateMenu();

        private void KonfiguroVleraFillestare()
        {
            ConfigureAspxComboBox.KonfiguroComboBoxLlojAdresa(cmbLlojAdrese);
            ConfigureAspxComboBox.ShtoKolonaPerKategoriZbritje(btneKategoriZbritje);
            ConfigureAspxComboBox.ShtoKolonaPerNivelZbritjesh(btneNivelZbritje);
            ConfigureAspxComboBox.KonfiguroComboBoxComboQytete(IdNdermarrja, txtQyteti);
            ConfigureAspxComboBox.ShtoKolonaPerNivelCmimesh(btneNivelCmimi);
            ConfigureAspxComboBox.ShtoKolonaPerLlogarine(txtNr2);
            ConfigureAspxComboBox.KonfiguroComboBoxLlojPorosie(cmbLlojPorosie);
            ConfigureAspxComboBox.KonfiguroComboBoxTitulliKlientFurnitor(cmbTitulli);
            ConfigureAspxComboBox.KonfiguroComboBoxMetoda(cmbMetoda, false, true);
            ConfigureAspxComboBox.percaktoTemplateComboMeEnableCallback(txtLlogDytesor, cmbBij, txtNr2, btneNivelZbritje, btneNivelCmimi, txtEmriBanka);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(btneGrupimi1, btneGrupimi2, cmbAgjentShitjesh, btneGrupimi3, btneKategoriZbritje);
            ConfigureAspxComboBox.KonfiguroComboBoxGrupKf(IdNdermarrja, btneGrupimi1, 1, Request.QueryString["kf"] == "furnitor");
            ConfigureAspxComboBox.KonfiguroComboBoxGrupKf(IdNdermarrja, btneGrupimi2, 2, Request.QueryString["kf"] == "furnitor");
            ConfigureAspxComboBox.KonfiguroComboBoxGrupKf(IdNdermarrja, btneGrupimi3, 3, Request.QueryString["kf"] == "furnitor");
            ConfigureAspxComboBox.KonfiguroComboBoxAgjentesh(IdNdermarrja, 0, cmbAgjentShitjesh);
            ConfigureAspxComboBox.KonfiguroComboBoxKategoriZbritje(IdNdermarrja, btneKategoriZbritje);
            ConfigureAspxComboBox.KonfiguroComboBoxKonfigurimeshSipasKategoriseDheNivelitMeLloj(cmbKonfigurimi,
                IdPerdoruesi, IdNdermarrja, IdGjuha, 48, Request.QueryString["kf"] == "klient" ? "KLSH" : "FRSH");

            var konf = new clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()));
            hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;
        }

        private void RuajKf()
        {
            clsKlientFurnitor kf;
            if (Page.IsValid == false)
                return;

            var tedrejtaInfo = new clsTeDrejtaRoli();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, "LupaKlientShpejte.aspx");
            var eshteShtim = hfShtimModifikim.Value != "modifikim";
            if (eshteShtim)
            {
                if (!tedrejtaInfo.DShtim)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["mesazhRaportNukKeniTeDrejta"], pnlMesazhi);
                    hfStatusi.Value = "true";
                    return;
                }
            }
            else
            {
                if (!tedrejtaInfo.DMod)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                    hfStatusi.Value = "false";
                    return;
                }
            }

            try
            {
                if (hfShtimModifikim.Value == "modifikim" || hfShtimModifikim.Value == "klonim")
                    kf = KrijoKf(false);
                else
                    kf = KrijoKf(true);
            }
            catch (Exception e)
            {
                LogManager.GetCurrentClassLogger().Error(e.Message);
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, e.Message, pnlMesazhi);
                hfStatusi.Value = "false";
                return;
            }

            clsMesazh mesazh;
            if (eshteShtim)
            {
                mesazh = kf.Ruaj(hfNrAutoKF, false, "", "", "", "");
            }
            else
            {
                var konf = new clsKonfigurimAmbjenti();
                konf.mbushKonfigAmbjSipasId(Convert.ToInt32(cmbKonfigurimi.Value), IdGjuha);
                kf.IdKlientFurnitor = Convert.ToInt32(Convert.ToInt32(hfId.Value));
                var klientiVjeter = new clsKlientFurnitor(kf.IdKlientFurnitor);
                kf.IdKrijuesi = klientiVjeter.IdKrijuesi;
                mesazh = kf.Modifiko(JsonConvert.DeserializeObject<DateTime>(hfdateHapje.Value), false, "0", string.Empty, string.Empty, string.Empty);
            }

            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                hfStatusi.Value = "false";
            }
            else
            {
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                hfStatusi.Value = "true";
            }

            //per momentin po e le keshtu
            if (Request.QueryString["vjenNgaRoute"] == "true")
                GlobalCacheManager.MySessionCache["AppFormKF"] = null;

            pnlMesazhi.Update();
        }

        /// <summary>
        /// krijon nje klientfurnitor sipas te dhenave te futura nga perdoruesi
        /// </summary>
        /// <param name="shtim"></param>
        /// <returns></returns>
        private clsKlientFurnitor KrijoKf(bool shtim)
        {
            var kf = new clsKlientFurnitor();
            if (!shtim)
            {
                kf.mbushKlientFurnitorSipasKodit(Request.QueryString["kodi"], IdNdermarrja);
                kf.OColKontaktet = new colKontaktiKlientFurnitor(kf.IdKlientFurnitor);
                kf.OColLidhjetAutorizim = new colLidhjetAutorizim(kf.IdKlientFurnitor, 4);
                kf.OColBuxhetet = new colBuxhetet(kf.IdKlientFurnitor, 4);
                kf.OColVleratFushatShtese = new colVleraFushaShtese(kf.IdKlientFurnitor, null);
            }

            var controls = this.GetAsPxTextEditIdValue();
            controls.AddRange(ASPxPanel1.GetAsPxTextEditIdValue());

            hfNrAuto = (ASPxHiddenField)NrAuto.VendosVleratNrAuto(hfNrAuto, controls);
            hfNrAutoKF = (ASPxHiddenField)NrAuto.ShtoNeRegjistrime(hfNrAutoKF, hfNrAuto, "txtKodi", "KodKlientFurnitor");
            int idllog = 0, titulli, idbanka = 0, idKatZbritje = 0, idNivelCmimi = 0, idNivelZbritje = 0, grup1 = 0, grup3 = 0, grup2 = 0, idagjent = 0, limitipara = 0, limitibllokues = 0, idllogdytesore = 0;
            string nrllog = "", emerkerkimi = "", nipti = "", tel = "", email = "", metoda, kategorizbritje = "", agjenti = "", zbritjeanalitike = "", bankaKod = "", grup1Kod = "", grup2Kod = "", grup3Kod = "", shenime = "";
            string qyteti = "";
            int idqyteti = 0;

            if (txtNr2.Text != "")
            {
                idllog = clsLlogari.mbushIDLlogariSipasKodit(txtNr2.Text.Split(';')[0], IdNdermarrja);
                nrllog = txtNr2.Text;
            }

            if (cmbTitulli.SelectedIndex == -1)
                titulli = 0;
            else
                int.TryParse(cmbTitulli.SelectedItem.Value.ToString(), out titulli);

            var konfigurimi = new clsKonfigurimAmbjenti
            {
                KodKonfigAmbjente = cmbKonfigurimi.Text.Split(';')[0],
                IdNdermarje = IdNdermarrja
            };
            konfigurimi = konfigurimi.merrSipasKodit();

            if (clsAlternativaKushti.getAlternativa(konfigurimi.IdKonfigAmbjente, "NI").ToLower() == "po")
            {
                if (string.IsNullOrEmpty(Convert.ToString(txtNipt.Value)))
                    throw new MyException(MessagesResource.Messages["msgPlotesoNIPT"]);

                if (clsKlientFurnitor.EkzistonKlientFurnitorNipt(Convert.ToString(txtNipt.Value), txtKodi.Text.RemoveSpaces(), IdNdermarrja)
                    && txtNipt.Value != null)
                    throw new MyException(MessagesResource.Messages["msgEkzistonkNIPT"]);
            }

            if (txtEmriBanka.Text != "")
            {
                var banka = new clsBanka();
                banka.mbushBankeSipasKodit(txtEmriBanka.Text, IdNdermarrja);
                idbanka = banka.IdBanka;
                bankaKod = txtEmriBanka.Text;
            }

            if (cmbAgjentShitjesh.Text != "")
            {
                var agjent = new clsAgjentShitje(cmbAgjentShitjesh.Text, IdNdermarrja);
                idagjent = agjent.IdAgjentShitje;
                agjenti = cmbAgjentShitjesh.Text;
            }

            if (btneKategoriZbritje.Text != "")
            {
                idKatZbritje = clsKokaKategoriZbritje.ktheIdKokaKategoriZbritje(btneKategoriZbritje.Text, IdNdermarrja);
                kategorizbritje = btneKategoriZbritje.Text;
            }

            if (btneNivelZbritje.Text != "")
            {
                idNivelZbritje = clsNivelZbritje.ktheIdNivelZbritjeSipasPershkrimit(btneNivelZbritje.Text, IdNdermarrja);
                zbritjeanalitike = btneNivelZbritje.Text;
            }

            if (txtLlogDytesor.Text != "")
                idllogdytesore = clsLlogari.mbushIDLlogariSipasKodit(txtLlogDytesor.Text.Split(';')[0], IdNdermarrja);

            if (btneGrupimi1.Text != "")
            {
                grup1 = new clsGrupeKF(btneGrupimi1.Text, IdNdermarrja, 1, (Request.QueryString["kf"] == "klient") ? 0 : 1).IdGrupi;
                grup1Kod = btneGrupimi1.Text;
            }

            if (btneGrupimi2.Text != "")
            {
                grup2 = new clsGrupeKF(btneGrupimi2.Text, IdNdermarrja, 2, (Request.QueryString["kf"] == "klient") ? 0 : 1).IdGrupi;
                grup2Kod = btneGrupimi2.Text;
            }

            if (btneGrupimi1.Text != "")
            {
                grup3 = new clsGrupeKF(btneGrupimi3.Text, IdNdermarrja, 3, (Request.QueryString["kf"] == "klient") ? 0 : 1).IdGrupi;
                grup3Kod = btneGrupimi3.Text;
            }

            var monndermarje = clsNdermarrje.ktheIdMonedheNdermSipasID(IdNdermarrja);
            var bij = 0;
            if (cmbBij.Text != "")
            {
                int.TryParse(cmbBij.Value.ToString(), out bij);
            }

            var llojporosie = 0;
            if (cmbLlojPorosie.Text != "")
            {
                int.TryParse(cmbLlojPorosie.Value.ToString(), out llojporosie);
            }

            if (txtEmerKerkimi.Text != "")
                emerkerkimi = txtEmerKerkimi.Text;

            if (txtNipt.Text != "")
                nipti = txtNipt.Text;

            if (txtTel.Text != "")
                tel = txtTel.Text;

            if (txtEmail.Text != "")
                email = txtEmail.Text;

            if (txtQyteti.Text != "")
                qyteti = txtQyteti.Text;
            if (txtQyteti.Text != "")
                idqyteti = Convert.ToInt32(txtQyteti.Value);

            var llogdytesore = new clsLlogari(kf.IdLlogariDytesore);

            if (txtLimitiParalajmerues.Text != "")
                int.TryParse(txtLimitiParalajmerues.Text, out limitipara);

            if (txtLimitiBllokues.Text != "")
                int.TryParse(txtLimitiBllokues.Text, out limitibllokues);

            int idMetoda;
            if (cmbMetoda.SelectedIndex != 0)
            {
                int.TryParse(cmbMetoda.SelectedItem.Value.ToString(), out idMetoda);
                metoda = cmbMetoda.SelectedItem.Text;
            }
            else
            {
                idMetoda = -1;
                metoda = "";
            }

            var maturimi = new clsMaturimi(kf.MaturimiKF);


            clsKlientFurnitor kf1;
            if (shtim)
            {
                kf1 = new clsKlientFurnitor(txtKodi.Text.RemoveSpaces(), idllog, txtNr2.Text, (Request.QueryString["kf"] == "klient"), titulli, cmbTitulli.Text, txtAktiviteti.Text, txtEmertimi.Text.RemoveSpaces(), txtEmerKerkimi.Text, txtNipt.Text, txtQyteti.Text != "" ? Convert.ToInt32(txtQyteti.Value) : 0, txtQyteti.Text, "", txtTel.Text, "", "", txtEmail.Text, "", "", "", true, 0, "", idllogdytesore, txtLlogDytesor.Text, 0, "", idMetoda, metoda, 0, "", idKatZbritje, btneKategoriZbritje.Text, limitipara, limitibllokues, 0, "", "", "", "", false, 0, 0, 0, btneNivelCmimi.Text, idagjent, cmbAgjentShitjesh.Text, 0, 0, idNivelZbritje, btneNivelZbritje.Text, 0, IdNdermarrja, mySessionObjects.ktheVitiNdermarrjes(Session), IdPerdoruesi, konfigurimi.IdKonfigAmbjente, "", "", idbanka, txtEmriBanka.Text, "", grup1, grup2, grup3, btneGrupimi1.Text, btneGrupimi2.Text, btneGrupimi3.Text, "", RuajAdresaDheKodPostar(), new colKontaktiKlientFurnitor(), new colBuxhetet(), new colVleraFushaShtese(), new colLidhjetAutorizim(), shtim, monndermarje, false, 0, "", IdPerdoruesi, bij, llojporosie, false, hfArkiva, rm, ci, "", 0, "", false, false, false, false, 0, 0, false, null, "", new colMarreveshjetPerKlient(), 0, "", new DateTime(), 0, 0, "", 0, txtEmertimFature.Text, false, "", txtKodiISKSH.Text, cbMeDogane.Checked,"");
            }
            else
            {
                var marreveshjet = new colMarreveshjetPerKlient();
                marreveshjet.MbushVetemMarreveshjetEKlientit(kf.IdKlientFurnitor);
                kf1 = new clsKlientFurnitor(txtKodi.Text.RemoveSpaces(), idllog, nrllog, Request.QueryString["kf"] == "klient", titulli, cmbTitulli.Text, txtAktiviteti.Text, txtEmertimi.Text.RemoveSpaces(), emerkerkimi, nipti, idqyteti, qyteti, kf.ShtetiKF, tel, kf.FaxKF, kf.CelKF, email, kf.WebPageKF, kf.IBANKF, kf.LlogariBankareKF, kf.AktivKF, kf.IdLlogZbritje, kf.NrLlogZbritje, idllogdytesore, llogdytesore.NrLlogari == null ? "" : llogdytesore.NrLlogari, kf.IdKushtePagese, kf.KodKushtePagese, idMetoda, metoda, kf.MaturimiKF, maturimi.KodMaturimi, idKatZbritje, kategorizbritje, limitipara, limitibllokues, kf.IdKategoriKlienti, kf.KushteDergimi, kf.KushteDergimi, kf.MenyraTransportit, kf.MenyraTransportit, kf.OfertaAutomatike, kf.VleraLimitPorositur, kf.Prioriteti, kf.CmimUlet, btneNivelCmimi.Text, idagjent, agjenti, 0, 0, idNivelZbritje, zbritjeanalitike, kf.ZbritjeTotal, IdNdermarrja, mySessionObjects.ktheVitiNdermarrjes(Session), IdPerdoruesi, konfigurimi.IdKonfigAmbjente, kf.Licenca, kf.Swift, idbanka, bankaKod, kf.AdresaBanka, grup1, grup2, grup3, grup1Kod, grup2Kod, grup3Kod, kf.NrTVSH, RuajAdresaDheKodPostar(), kf.OColKontaktet == null ? new colKontaktiKlientFurnitor() : kf.OColKontaktet, kf.OColBuxhetet == null ? new colBuxhetet() : kf.OColBuxhetet, kf.OColVleratFushatShtese == null ? new colVleraFushaShtese() : kf.OColVleratFushatShtese, kf.OColLidhjetAutorizim == null ? new colLidhjetAutorizim() : kf.OColLidhjetAutorizim, shtim, monndermarje, false, kf.IdObjektivaKosto, kf.Objektiva, kf.IdKrijuesi, bij, llojporosie, kf.Kupon, hfArkiva, rm, ci, kf.Koordinata, 0, "", kf.KlientSpecifik, kf.Fermer, kf.AutoNgarkese, kf.ShitjePaTvsh, kf.PerqindjeAgjenti, kf.PerqindjeAgjenti2, kf.Prospekt, kf.KodiMobile, kf.EmailPerPajisje, marreveshjet, kf.Idklientfurnitorkryesor, kf.Shenime, kf.DteDatelindjaKF, 0, kf.PerqindjeAgjenti3, "", 0, kf.EmertimFature, false, "", txtKodiISKSH.Text, cbMeDogane.Checked, "");
            }
            return kf1;
        }

        private colAdresatKlientFurnitor RuajAdresaDheKodPostar()
        {
            return string.IsNullOrWhiteSpace(hfAdresa.Value)
                ? new colAdresatKlientFurnitor()
                : JsonConvert.DeserializeObject<colAdresatKlientFurnitor>(hfAdresa.Value);
        }

        protected void txtNr2_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("txtNr2"))
                ConfigureAspxComboBox.KonfiguroComboBoxLlogaria(txtNr2, IdPerdoruesi, IdNdermarrja, e);
        }

        protected void txtNr2_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("txtNr2"))
                ConfigureAspxComboBox.KonfiguroComboBoxLlogaria(txtNr2, IdPerdoruesi, IdNdermarrja, e);
        }

        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {
            if (e.Item.Name == "Ruaj")
            {
                Page.Validate("entries");
                RuajKf();
            }
        }

        protected void btneNivelZbritje_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("btneNivelZbritje"))
                ConfigureAspxComboBox.KonfiguroComboBoxNiveleZbritjePrind(IdNdermarrja, btneNivelZbritje);
        }

        protected void btneNivelCmimi_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("btneNivelCmimi"))
                ConfigureAspxComboBox.KonfiguroComboBoxNiveleCmimeshPrind(btneNivelCmimi, IdNdermarrja, e.Value);
        }

        protected void btneNivelCmimi_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("btneNivelCmimi"))
                ConfigureAspxComboBox.KonfiguroComboBoxNiveleCmimeshPrind(IdNdermarrja, btneNivelCmimi, e.Filter, e.BeginIndex, e.EndIndex);
        }

        protected void txtEmriBanka_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback && (Request.Params["__CALLBACKID"].Contains("txtEmriBanka") && e.Value != null))
                ConfigureAspxComboBox.KonfiguroComboBoxBankatSipasFiltrit(txtEmriBanka, IdPerdoruesi, IdNdermarrja,
                    new clsLlogari(txtNr2.Text, IdNdermarrja).IdMonedha, e.Value);
        }

        protected void txtEmriBanka_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("txtEmriBanka"))
                ConfigureAspxComboBox.KonfiguroComboBoxComboBankatSipasFiltrit(IdPerdoruesi, IdNdermarrja,
                    txtEmriBanka, new clsLlogari(txtNr2.Text, IdNdermarrja).IdMonedha, e.Filter,
                    e.BeginIndex, e.EndIndex);
        }

        protected void txtLlogDytesor_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("txtLlogDytesor"))
                ConfigureAspxComboBox.KonfiguroComboBoxLlogaria(txtLlogDytesor, IdPerdoruesi, IdNdermarrja, e);
        }

        protected void txtLlogDytesor_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("txtLlogDytesor"))
                ConfigureAspxComboBox.KonfiguroComboBoxLlogaria(txtLlogDytesor, IdPerdoruesi, IdNdermarrja, e);
        }

        protected void cmbBij_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("cmbBij"))
                ConfigureAspxComboBox.KonfiguroComboBoxNdermarjeBij(IdNdermarrja, cmbBij);
        }
    }
}
