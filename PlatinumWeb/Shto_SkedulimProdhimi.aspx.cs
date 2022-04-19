using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using DevExpress.Web;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using DbCore;
using System.Globalization;
using System.Resources;
using DbCore.DbProdhimi;
using DbCore.DbShare;
using DbCore.IMBUtils.Extensions;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class Shto_SkedulimProdhimi : MyPageBase
    {
        public void MerrTedhenat(int idGjuha, int idPerdoruesi, int idViti, int idNdermarrje, clsKokaSkedulimProdhimi koka, System.Resources.ResourceManager rm, System.Globalization.CultureInfo ci)
        {
            var konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasId(koka.IdKonfigAmbjente, idGjuha);
            cmbKonfigurimi.Text = konf.KodKonfigAmbjente;
            hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;

            if (koka.IdPlanifikimi != 0 && hfShtimModifikim.Value != "klonim")
            {
                var kokaplan = new clsKokaPlanifikim(koka.IdPlanifikimi);
                btnProjekti.Text = kokaplan.NrDok;
                hfPrioriteti.Value = kokaplan.IdKokaPlanifikim.ToString();
                var col = new colTrupiPlanifikim(koka.IdPlanifikimi, koka.IdNdermarrje);
                if (col.Count == 1)
                {
                    var art = new DbCore.DbInventari.clsArtikulli(col[0].IdArtikulli);
                    hfPershkrimProdukti.Value = art.PershkrimArtikulli;
                    hfKodProdukti.Value = art.KodArtikulli;
                }
            }
            var idMonedheZgjedhur = DbCore.clsFunksione.ktheMonedhePerFormatNumri(idGjuha, int.Parse(cmbKonfigurimi.Value.ToString()), idNdermarrje, 809, string.Empty, -1, false);
            var formatNrPerKonfig = new DbCore.DbShare.clsFormatiKonfig();
            formatNrPerKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(int.Parse(cmbKonfigurimi.Value.ToString()));
            var formatMonedhe = clsFormatKonfigTrup.ktheFormatNumriMonedhe(formatNrPerKonfig, idMonedheZgjedhur);
            var serializusi = new System.Web.Script.Serialization.JavaScriptSerializer();
            hfState.Set("formatMonedhe", serializusi.Serialize(formatMonedhe));
            if (koka.IdBurimi != 0)
            {
                var burim = new clsBurime(koka.IdBurimi);
                btnBurimi.Text = burim.Kodi;
                hfPershkrimBurimi.Value = burim.Emertimi;
                hfKosto.Value = burim.KostoPlan.ToString();
            }


            txtNrDok.Text = koka.NrDok;
            dteDtDok.Date = koka.DtDok;
            dteDtRegjistrimi.Date = koka.DtRegj;
            txtShenime.Text = koka.Shenime;

            var dtlidhur = koka.merrIdsDokLidhur();
            hfLidhur.Value = (!(dtlidhur.Rows.Count == 0)).ToString();
            AspxWebControlUtils.ShtoLidhje(idPerdoruesi, idViti, idNdermarrje, hl, dtlidhur, koka.IdGjenerues, koka.IdNivelGjenerues, koka.IdKonfigGjenerues, idGjuha);

            var autorizimet = DbCore.DbProdhimi.clsKokaSkedulimProdhimi.kaAutorizime(koka.IdKoka, idPerdoruesi);
            hfAutorizimi.Value = autorizimet.ToString();
            if (!autorizimet)
            {
                hfLidhur.Value = "True";
            }
        }

  

        protected void Page_Load(object sender, EventArgs e)
        {
            var cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            var rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            int idPerdoruesi;
            int idGjuha;
            int idNdermarrje;
            int idViti;
            if (hfState.Count == 0)
            {
                if (!DbCore.mySessionObjects.isLogedIn(Session))
                {
                    DbCore.clsFunksione.logout(Session,true,"FaqePaautorizuar");
                }
                idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
                idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
                hfState.Set("idPerdoruesi", idPerdoruesi);
                hfState.Set("idGjuha", idGjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("idViti", idViti);
            }
            else
            {
                idPerdoruesi = (int)hfState["idPerdoruesi"];
                idGjuha = (int)hfState["idGjuha"];
                idNdermarrje = (int)hfState["idNdermarrje"];
                idViti = (int)hfState["idViti"];
            }
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerdoruesi);
            }
            konfigGrid();

            if (DbCore.mySessionObjects.merrPeriudheKontabel(Session) != null)
            {
                var periudha = DbCore.mySessionObjects.merrPeriudheKontabel(Session);
                btnPeriudha.Text = periudha.NrPeriudha.ToString();
                lblPeriudhaAktuale.Text = String.Format("{0}-{1}", periudha.FillimiPeriudha.ToShortDateString(), periudha.MbarimiPeriudha.ToShortDateString());
            }
            percaktoTemplateMenu(idPerdoruesi, idViti, idNdermarrje, ASPxMenu1);

            if (!IsPostBack)
            {
                mbushHiddenFieldMePerkthime(cultinf, rm);
                if (hfShtimModifikim.Value == string.Empty)
                {
                    if (Request.QueryString["shtim_modifikim"] == null || Request.QueryString["shtim_modifikim"] == "shtim")
                    {
                        hfShtimModifikim.Value = "shtim";
                        konfiguroVleraFillestareShto(idGjuha, idNdermarrje, rm, cultinf);
                    }
                    else
                    {
                        if (Request.QueryString["shtim_modifikim"] == "klonim")
                        {
                            hfShtimModifikim.Value = "klonim";
                            konfiguroVleraFillestareModifiko(idGjuha, idPerdoruesi, idViti, idNdermarrje, rm, cultinf);
                        }
                        else
                        {
                            hfShtimModifikim.Value = "modifikim";
                            konfiguroVleraFillestareModifiko(idGjuha, idPerdoruesi, idViti, idNdermarrje, rm, cultinf);
                        }
                    }
                    percaktoTemplateMenu(idPerdoruesi, idViti, idNdermarrje, ASPxMenu1);
                }
                else
                {
                    if (hfShtimModifikim.Value == "shtim")
                    {
                        konfiguroVleraFillestareShto(idGjuha, idNdermarrje, rm, cultinf);
                    }
                    else
                    {
                        if (hfShtimModifikim.Value == "klonim")
                        {
                            konfiguroVleraFillestareModifiko(idGjuha, idPerdoruesi, idViti, idNdermarrje, rm, cultinf);
                        }
                        else
                        {
                            if (hfShtimModifikim.Value == "modifikim")
                            {
                                konfiguroVleraFillestareModifiko(idGjuha, idPerdoruesi, idViti, idNdermarrje, rm, cultinf);
                            }
                        }
                    }
                }
                var serializusi = new JavaScriptSerializer();
                serializusi.MaxJsonLength = 50000000;

                var tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();

                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "Shto_SkedulimProdhimi.aspx");
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                hfTeDrejta.Add("ShtimDraft", tedrejtaInfo.DShtimDraft);
                hfTeDrejta.Add("ModifikimDraft", tedrejtaInfo.DModifikimDraft);
            }
            Container.Attributes["src"] = string.Empty;
            GridUtil.perktheButonaGride(hfState, cultinf);
            AspxWebControlUtils.perkthePopUp(popFshi, rm.GetString("labelKujdes", cultinf), lblMsgbox, rm.GetString("labelAdministrimiMsgJeniSigurt", cultinf), ButtonCancel, rm.GetString("labelAnullo", cultinf));
        }

        /// <summary>
        /// Metode qe sherben per te kaluar perkthime te js
        /// </summary>
        /// <param name="cultinf">Merr culture info</param>
        /// <param name="rm">Merr Resource Manager</param>
        private void mbushHiddenFieldMePerkthime(CultureInfo cultinf, ResourceManager rm)
        {
            hfState.Set("msgSasiaDuhetNumer", rm.GetString("msgSasiaDuhetNumer", cultinf));
            hfState.Set("msgArtikulliEshteInaktiv", rm.GetString("msgArtikulliEshteInaktiv", cultinf));
            hfState.Set("NukKryhenVeprimeMeArtikujProdhimOseProdhimNeProces", rm.GetString("NukKryhenVeprimeMeArtikujProdhimOseProdhimNeProces", cultinf));
            hfState.Set("msgNukKryhenVeprimeMeArtikujTePastokueshem", rm.GetString("msgNukKryhenVeprimeMeArtikujTePastokueshem", cultinf));
            hfState.Set("msgNukMundTeKryeniVeprimeMeArtikujTePerbere", rm.GetString("msgNukMundTeKryeniVeprimeMeArtikujTePerbere", cultinf));
            hfState.Set("msgNukMundTaZgjidhniArtikullinPerRezervim", rm.GetString("msgNukMundTaZgjidhniArtikullinPerRezervim", cultinf));
            hfState.Set("msgEkzistonArtikullNeGride", rm.GetString("msgEkzistonArtikullNeGride", cultinf));
            hfState.Set("msgGabimGjateTransferimitTeTeDhenave", rm.GetString("msgGabimGjateTransferimitTeTeDhenave", cultinf));
            hfState.Set("msgZgjidhDokumentin", rm.GetString("msgZgjidhDokumentin", cultinf));
            hfState.Set("msgZgjidhMagazinen", rm.GetString("msgZgjidhMagazinen", cultinf));
            hfState.Set("headerZgjidhKlientFurnitorin", rm.GetString("headerZgjidhKlientFurnitorin", cultinf));
            hfState.Set("msgKaRreshtaMeSasiZero", rm.GetString("msgKaRreshtaMeSasiZero", cultinf));
            hfState.Set("msgZgjidhniLlojinEVeprimit", rm.GetString("msgZgjidhniLlojinEVeprimit", cultinf));
            hfState.Set("roundPanelZgjidhArtikullin", rm.GetString("roundPanelZgjidhArtikullin", cultinf));
            hfState.Set("msgZgjidhMakro", rm.GetString("msgZgjidhMakro", cultinf));
            hfState.Set("msgLlojiVeprimitIPanjohur", rm.GetString("msgLlojiVeprimitIPanjohur", cultinf));
            hfState.Set("msgSasiaNukMundTeJeteZero", rm.GetString("msgSasiaNukMundTeJeteZero", cultinf));
            hfState.Set("msgSasiaNumer", rm.GetString("msgSasiaNumer", cultinf));
            hfState.Set("msgPoTransferohetTeDhenatShtypniPerseriRuaj", rm.GetString("msgPoTransferohetTeDhenatShtypniPerseriRuaj", cultinf));
            hfState.Set("msgZgjidhniLlojin", rm.GetString("msgZgjidhniLlojin", cultinf));
            hfState.Set("msgShenoniNumrinEDokumentit", rm.GetString("msgShenoniNumrinEDokumentit", cultinf));
            hfState.Set("msgZgjidhniNjeDateDokumenti", rm.GetString("msgZgjidhniNjeDateDokumenti", cultinf));
            hfState.Set("msgZgjidhniNjeDateRegjstrimi", rm.GetString("msgZgjidhniNjeDateRegjstrimi", cultinf));
            hfState.Set("msgJepniKF", rm.GetString("msgJepniKF", cultinf));
            hfState.Set("msgNukKeniAutorizimPerTeRuajturKeteDok", rm.GetString("msgNukKeniAutorizimPerTeRuajturKeteDok", cultinf));
            hfState.Set("msgNukKeniAutorizimPerTeFshireDok", rm.GetString("msgNukKeniAutorizimPerTeFshireDok", cultinf));
            hfState.Set("msgNukKeniAutorizimKonvertim", rm.GetString("msgNukKeniAutorizimKonvertim", cultinf));
            hfState.Set("msgTrupiDokumentitNukDuhetLeneBosh", rm.GetString("msgTrupiDokumentitNukDuhetLeneBosh", cultinf));
            hfState.Set("msgKaArtikujPaNjesi", rm.GetString("msgKaArtikujPaNjesi", cultinf));
            hfState.Set("msgZgjidhPeriudheKontabel", rm.GetString("msgZgjidhPeriudheKontabel", cultinf));
            hfState.Set("JQgridShtoArtikull", rm.GetString("JQgridShtoArtikull", cultinf));
            hfState.Set("msgDokNukMundTeKonvertohet", rm.GetString("msgDokNukMundTeKonvertohet", cultinf));
            hfState.Set("MenuKokeDokumenti", rm.GetString("MenuKokeDokumenti", cultinf));
            hfState.Set("MenuTrupDokumenti", rm.GetString("MenuTrupDokumenti", cultinf));
            hfState.Set("MenuFundDokumenti", rm.GetString("MenuFundDokumenti", cultinf));
        }


        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        private void percaktoTemplateMenu(int idPerdoruesi, int idViti, int idNdermarrje, ASPxMenu aSPxMenu1)
        {
            var menu = new DbCore.DbShare.colMenuItem(DbCore.mySessionObjects.ktheGjuhe(Session));
            menu.merrMenuItemSipasKomponentesRegjistrime(DbCore.mySessionObjects.ktheGjuhe(Session), "Shto_SkedulimProdhimi.aspx", idPerdoruesi, idNdermarrje, idViti, hfShtimModifikim.Value == "modifikim" ? false : true);

            var kokmag = new clsKokaSkedulimProdhimi();
            if ((hfShtimModifikim.Value == "modifikim"))
            {
                var id = int.Parse(Request.QueryString["id"]);

                kokmag.mbushKokaSkedulimProdhimiSipasID(id);
            }
            foreach (DbCore.DbShare.clsMenuItem m in menu)
            {
                if (m.Name != "ItemFilter" && m.Name != "ItemFrame")
                {
                    clsToolbarConfig.ShtoMenuItem(this.Theme, aSPxMenu1, m);
                }

                if ((hfLidhur.Value == "True") && m.Name == "Draft")
                {
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                }
                if ((hfShtimModifikim.Value != "modifikim") && m.Name == "PrintPreview")
                {
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                }
                if ((hfShtimModifikim.Value != "modifikim") && m.Name == "Fshi")
                {
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                }
                if ((hfShtimModifikim.Value != "modifikim") && m.Name == "Klono")
                {
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                }
                if (m.Name == "Ruaj")
                {
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = true;
                }


                if (m.Name == "ItemFrame")
                {
                    clsToolbarConfig.ShtoMenuItemPerFrame(this, aSPxMenu1, DbCore.clsFunksione.ktheUrlHelpi(m.UrlHelp).Item1);
                }
                if (m.Name == "Shto" || m.Name == "Kerko" || m.Name == "Pastro" || m.Name == "ItemFilter" || m.Name == "ItemFrame")
                {
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].BeginGroup = true;
                }
            }

            clsMenuInfo.ShtoMenuItemInfo(this, MenuInfo);
            var visible = clsFunksione.merrMenuVisibleDraft(hfShtimModifikim.Value == "modifikim" ? false : true, kokmag.IdStatusDok);
            ASPxMenu1.Items.FindByName("Draft").ClientVisible = visible;
        }
        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.ktheNdermarrjeVit(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), ASPxMenu1);
        }

        private void vendosDataDefault()
        {
            var sot = new DateTime();
            sot = DateTime.Today;
            var periudha = DbCore.mySessionObjects.merrPeriudheKontabel(Session);
            if (sot >= periudha.FillimiPeriudha && sot <= periudha.MbarimiPeriudha)
            {
                this.dteDtDok.Value = DateTime.Today;
            }
            else
            {
                dteDtDok.Value = periudha.FillimiPeriudha;
            }
        }

        /// <summary>
        /// Vendos vlerat default kur po behet shtim
        /// </summary>
        /// <param name="idGjuha"></param>
        /// <param name="idNdermarrje"></param>
        private void konfiguroVleraFillestareShto(int idGjuha, int idNdermarrje, System.Resources.ResourceManager rm, System.Globalization.CultureInfo ci)
        {
            dteDtDok.Date = DateTime.Today;
            AspxWebControlUtils.vendosDateEditMask(dteDtDok);
            dteDtRegjistrimi.Date = DateTime.Today;
            vendosDataDefault();
            var idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            AspxWebControlUtils.vendosDateEditMask(dteDtRegjistrimi);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(btnBurimi);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(btnProjekti);
            btnProjekti.ReadOnly = true;
            ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 97, rm, ci, idGjuha);
            ConfigureAspxComboBox.mbushComboBurimet(idNdermarrje, btnBurimi);
            cmbKonfigurimi.SelectedIndex = 0;
            hfKonffillestar.Value = cmbKonfigurimi.Text;
            var idMonedheZgjedhur = DbCore.clsFunksione.ktheMonedhePerFormatNumri(idGjuha, int.Parse(cmbKonfigurimi.Value.ToString()), idNdermarrje, 809, string.Empty, -1, true);
            var formatNrPerKonfig = new DbCore.DbShare.clsFormatiKonfig();
            formatNrPerKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(int.Parse(cmbKonfigurimi.Value.ToString()));
            var formatMonedhe = clsFormatKonfigTrup.ktheFormatNumriMonedhe(formatNrPerKonfig, idMonedheZgjedhur);
            var serializusi = new System.Web.Script.Serialization.JavaScriptSerializer();
            hfState.Set("formatMonedhe", serializusi.Serialize(formatMonedhe));
        }

        /// <summary>
        /// Vendos vlerat default kur po behet modifikim
        /// </summary>
        /// <param name="idGjuha"></param>
        /// <param name="idPerdoruesi"></param>
        /// <param name="idNderViti"></param>
        /// <param name="idNdermarrje"></param>
        private void konfiguroVleraFillestareModifiko(int idGjuha, int idPerdoruesi, int idViti, int idNdermarrje, System.Resources.ResourceManager rm, System.Globalization.CultureInfo ci)
        {
            AspxWebControlUtils.vendosDateEditMask(dteDtDok);
            AspxWebControlUtils.vendosDateEditMask(dteDtRegjistrimi);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(btnBurimi);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(btnProjekti);
            ConfigureAspxComboBox.mbushComboBurimet(idNdermarrje, btnBurimi);
            ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 97, rm, ci, idGjuha);
            btnProjekti.ReadOnly = true;
            var id = int.Parse(Request.QueryString["id"]);
            var kok = new clsKokaSkedulimProdhimi();
            kok.IdKoka = id;
            kok.mbushKokaSkedulimProdhimiSipasID(kok.IdKoka);
            if (kok != null)
            {
                MerrTedhenat(idGjuha, idPerdoruesi, idViti, idNdermarrje, kok, rm, ci);
            }

            mbushListeRegjistrimTrupiModifiko(idPerdoruesi, idNdermarrje, kok);
        }

        /// <summary>
        /// Merr trupin ekzistues te dokumentit te magazines qe po modifikohet
        /// </summary>
        /// <param name="idPerdoruesi"></param>
        /// <param name="idNdermarrje"></param>
        private void mbushListeRegjistrimTrupiModifiko(int idPerdoruesi, int idNdermarrje, clsKokaSkedulimProdhimi koka)
        {
            var trupi = new colTrupiSkedulimProdhimi(koka.IdKoka, idNdermarrje);
            mbushHiddenFieldet(idPerdoruesi, idNdermarrje, trupi, koka.IdKonfigAmbjente, koka.IdKoka, koka.IdKonfigAmbjente);
            ;
        }

        private void mbushHiddenFieldet(int idPerdoruesi, int idNdermarrje, colTrupiSkedulimProdhimi col, int lloji, int idkokamagazina, int idkonfig)
        {
            var serializusi = new JavaScriptSerializer();
            serializusi.MaxJsonLength = 50000000;
            HfColTrupMag.Value = serializusi.Serialize(col);
            var colArtikuj = col.ktheColArtikuj();
            HfColArt.Value = serializusi.Serialize(colArtikuj);
            HfColBurimi.Value = serializusi.Serialize(col.ktheColBur(idPerdoruesi));
            HfColAktiviteti.Value = serializusi.Serialize(col.ktheColAkt(idPerdoruesi));
            HfColPlanifikime.Value = serializusi.Serialize(col.ktheColPlanifikime(idPerdoruesi));
            var konf = new DbCore.DbShare.clsKonfigurimAmbjenti(lloji);
            HfKonfAmb.Value = serializusi.Serialize(konf);
        }

        private void konfigGrid()
        {
            var emriKomponentes = "Shto_SkedulimProdhimi.aspx";
            var oKomponente = new DbCore.DbAdmin.clsKomponente(emriKomponentes);
            var konfigurimi = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konfigurimi.mbushKonfigDefaultKomponentes(oKomponente.IdKomponente, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            var trupiGrides = new DbCore.DbAdmin.colGridaTrupi(oKomponente.IdKomponente, konfigurimi.IdKonfigAmbjente, (int)hfState["idGjuha"]);
            var serializusi = new JavaScriptSerializer();
            HfGridCol.Value = serializusi.Serialize(trupiGrides);
        }





        /// <summary>
        /// Percakton veprimin qe kryhet kur klikohet nje nga butonat e menuse
        /// </summary>
        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {
            var idPerdoruesi = (int)hfState["idPerdoruesi"];
            var idNdermarrje = (int)hfState["idNdermarrje"];
            if (e.Item.Name == "Ruaj")
            {
                Page.Validate();
                ruajRegjistrimSkedulimi(1, false);
                mbushHiddenFieldet(idPerdoruesi, idNdermarrje, new colTrupiSkedulimProdhimi(), 1, 0, 0);
            }
            if (e.Item.Name == "Draft")
            {
                Page.Validate();
                ruajRegjistrimSkedulimi(0, false);
                mbushHiddenFieldet(idPerdoruesi, idNdermarrje, new colTrupiSkedulimProdhimi(), 1, 0, 0);
            }
        }

        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            var cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            var rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            pergjigja.Text = string.Empty;

            var kokam = new clsKokaSkedulimProdhimi();
            kokam.IdKoka = int.Parse(Request.QueryString["id"]);
            kokam.mbushKokaSkedulimProdhimiSipasID(kokam.IdKoka);
            var mesazh = new DbCore.clsMesazh();
            var lidhur = kokam.eshteILidhur();


            if (lidhur)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgDokLidhurNukFshihet", cultinf), pnlMesazhi);
                this.status1.Value = "false";
                return;
            }
            if (kokam.DtDok.Year != new DbCore.DbAdmin.clsNdermarrjeViti(DbCore.mySessionObjects.ktheNdermarrjeVit(Session)).Viti)
            {
                this.status1.Value = "false";
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgDataNukPerketVititUshtrimor", cultinf), pnlMesazhi);
                return;
            }
            var idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            //var periudha = new DbCore.DbAdmin.clsPeriudhaKontabel(kokam.DtDok, idNdermarrje);
            //var mesazhi = periudha.isPeriudheKycur();
            bool ekycur = DbCore.DbAdmin.clsPeriudhaKontabel.eshteKycurPeriudheSipasDateDheNdermarrjes(kokam.DtDok, idNdermarrje);
            if (ekycur)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgPeriudhaEKycur", cultinf), pnlMesazhi);
                return;
            }

            kokam.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            mesazh = kokam.fshi();

            if (mesazh.Status)
            {
                Response.Redirect("SkedulimProdhimi.aspx?fshi=po");
                this.status1.Value = "true";
            }
            else
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                this.status1.Value = "false";
                return;
            }
        }



        /// <summary>
        /// Ruan/Modifikon nje objekt clsKokaMagazina.
        /// Therret funksionin <see cref="krijoRegjistrimMagazine"/>
        /// </summary>
        /// <param name="statusDokumenti">Statusi me te cilin po ruhet dokumenti (I rregullt apo draft)</param>
        private void ruajRegjistrimSkedulimi(int statusDokumenti, bool printo)
        {
            if (Page.IsValid == false)
            {
                return;
            }
            var mesazhinformues = string.Empty;

            var rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            var ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            if (isValidRegjistrimSkedulimi(statusDokumenti, rm, ci))
            {
                var mesazh = new DbCore.clsMesazh();
                var idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                try
                {
                    var koka = krijoRegjistrimSkedulimi(statusDokumenti, out mesazhinformues);
                    var idPeriudheZgjedhur = DbCore.mySessionObjects.merrPeriudheKontabel(Session).IdPeriudha;

                    if (koka.ColTrupi.Count == 0)
                    {
                        status1.Value = "false";
                        return;
                    }

                    if (mesazhinformues != string.Empty)
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhinformues, pnlMesazhi);
                        status1.Value = "false";
                        return;
                    }

                    var tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                    tedrejtaInfo.merrTeDrejtaPerKeteKomponente(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "Shto_SkedulimProdhimi.aspx");

                    if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim")
                    {
                        if ((statusDokumenti == 1 && !tedrejtaInfo.DShtim) || (statusDokumenti == 0 && !tedrejtaInfo.DShtimDraft))
                        {
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNukKeniTeDrejta"), pnlMesazhi);
                            status1.Value = "false";
                            return;
                        }
                        mesazh = koka.ruaj(hfNrAutoShitje);
                    }
                    else
                    {
                        if (hfShtimModifikim.Value == "modifikim")
                        {
                            if ((statusDokumenti == 1 && !tedrejtaInfo.DMod) || (statusDokumenti == 0 && !tedrejtaInfo.DModifikimDraft))
                            {
                                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNukKeniTeDrejta"), pnlMesazhi);
                                status1.Value = "false";
                                return;
                            }

                            koka.IdKoka = int.Parse(Request.QueryString["id"]);

                            var lidhur = koka.eshteILidhur();



                            if (lidhur.ToString() != hfLidhur.Value)
                            {
                                mesazh.Status = false;
                                mesazh.PershkrimMesazhi = rm.GetString("msgDokumentiEshteILidhur", ci);
                                status1.Value = "false";
                            }
                            else
                            {
                                if (lidhur == true)
                                {
                                    mesazh = koka.modifiko(true);
                                }
                                else
                                {
                                    mesazh = koka.modifiko(false);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    NLog.LogManager.GetCurrentClassLogger().Error(ex.Message);
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, ex.Message, pnlMesazhi);
                    status1.Value = "false";
                    return;
                }
                pergjigja.Text = "ruaj";
                if (mesazh.Status == true)
                {
                    hfShtimModifikim.Value = "shtim";
                    percaktoTemplateMenu(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), idNdermarrje, ASPxMenu1);



                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("msgRuajtjeMeSukses", ci), pnlMesazhi);

                    hl = new HtmlTable();
                    pnlLidhur.Update();
                    status1.Value = "true";
                }
                else
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi, LoadingPanel);
                    status1.Value = "false";
                }
            }
            else
            {
                status1.Value = "false";
            }
            pergjigja.ClientVisible = false;
        }

        /// <summary>
        /// Krijon nje objekt te tipit DbCore.DbRegjistrim.clsKokaSkedulimProdhimi
        /// </summary>
        /// <param name="statusDokumenti">Statusi i dokumentit (I rregullt ose draft)</param>
        /// <param name="klienti">Klienti</param>
        /// <returns>Kthen nje objekt te tipit DbCore.DbRegjistrim.clsKokaSkedulimProdhimi</returns>
        private clsKokaSkedulimProdhimi krijoRegjistrimSkedulimi(int statusDokumenti,  out string mesazhinformues)
        {
            mesazhinformues = string.Empty;

            var controls = this.GetAsPxTextEditIdValue();
            controls.AddRange(ASPxSplitter1.GetAsPxTextEditIdValue());

            hfNrAuto = (ASPxHiddenField)DbCore.DbAdmin.NrAuto.VendosVleratNrAuto(hfNrAuto, controls);
            //DbCore.DbAdmin.NrAuto.vendosVleratNrAuto(hfNrAuto, this, null, ASPxSplitter1, null);

            hfNrAutoShitje = (ASPxHiddenField)DbCore.DbAdmin.NrAuto.ShtoNeRegjistrime(hfNrAutoShitje, hfNrAuto, "txtNrDok", "NrDok");
            //DbCore.DbAdmin.NrAuto.shtoNeHfRegjistrime(hfNrAutoShitje, hfNrAuto, "txtNrDok", "NrDok");

            var idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            var clsKonf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            var koka = new clsKokaSkedulimProdhimi();
            if (cmbKonfigurimi.Text != string.Empty)
            {
                clsKonf.mbushKonfigAmbjSipasKod(cmbKonfigurimi.Text, idNdermarrje);
            }
            else
            {
                clsKonf.mbushKonfigDefaultKomponentes(809, idNdermarrje);
            }
            koka = krijoRegjistrimSkedulimi(statusDokumenti, clsKonf,  ruajTrupinESkedulimit());

            return koka;
        }


        private clsKokaSkedulimProdhimi krijoRegjistrimSkedulimi(int statusDokumenti,  DbCore.DbShare.clsKonfigurimAmbjenti clsKonf,  colTrupiSkedulimProdhimi coltrupi )
        {
            var mag = new clsKokaSkedulimProdhimi();
            var idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            var idnderviti = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
            var idburimi = 0;
            var kodburimi = string.Empty;
            if (btnBurimi.Text != string.Empty)
            {
                idburimi = int.Parse(btnBurimi.Value.ToString());
                kodburimi = btnBurimi.Text;
            }
            var idprojekti = 0;
            int.TryParse(hfPrioriteti.Value, out idprojekti);
            var idperdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);

            var mesazh =    mag.krijoSkedulim(clsKonf.IdNivel, clsKonf.IdKonfigAmbjente, idprojekti, btnProjekti.Text, idburimi, kodburimi, dteDtDok.Date, txtNrDok.Text, statusDokumenti, idNdermarrje, idnderviti, idperdoruesi, dteDtRegjistrimi.Date, txtShenime.Text, idperdoruesi, coltrupi);

            if (!mesazh.Status)
            {
                throw new Exception(mesazh.PershkrimMesazhi);
            }
            return mag;
        }


        private colTrupiSkedulimProdhimi ruajTrupinESkedulimit()
        {
            var cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            var rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            var serializusi = new JavaScriptSerializer();
            var dokumenti = (object[])serializusi.DeserializeObject(gridDataObject.Value);
            var trupat = new colTrupiSkedulimProdhimi();
            var idMagTemp = -1;
            var idprojekttemp = -1;
            var isMagENjejte = true;
            var eshteprojektnjejte = true;
            var idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
            var idPerdoruesi = mySessionObjects.ktheIdPerdoruesi(Session);

            for (var i = 0; i < dokumenti.Length; i++)
            {
                var trupMag = new clsTrupiSkedulimProdhimi(idNdermarrje,  (Dictionary<string, object>)dokumenti[i] );
                if (trupMag.IdBurimi > 0)
                {
                    if (idMagTemp == -1)
                    {
                        idMagTemp = trupMag.IdBurimi;
                    }
                    else
                    {
                        if (isMagENjejte && trupMag.IdBurimi != idMagTemp)
                        {
                            isMagENjejte = false;
                        }
                    }
                    if (idprojekttemp == -1)
                    {
                        idprojekttemp = trupMag.IdPlanifikimi;
                    }
                    else
                    {
                        if (eshteprojektnjejte && trupMag.IdPlanifikimi != idprojekttemp)
                        {
                            eshteprojektnjejte = false;
                        }
                    }
                    trupat.Add(trupMag);
                }
            }

            btnBurimi.Text = string.Empty;
            btnProjekti.Text = string.Empty;
            if (isMagENjejte && trupat.Count > 0)
            {
                var burim = new clsBurime(trupat[0].IdBurimi);
                btnBurimi.Text = burim.Kodi;
            }
            if (eshteprojektnjejte && trupat.Count > 0)
            {
                var burim = new  clsKokaPlanifikim (trupat[0].IdPlanifikimi);
                btnProjekti.Text = burim.NrDok;
                hfPrioriteti.Value = burim.IdKokaPlanifikim.ToString();
            }
            return trupat;
        }


        /// <summary>
        /// Kontrollon nese jane plotesuar te gjitha fushat e detyrueshme te kokes se dokumentit
        /// </summary>
        private bool isValidRegjistrimSkedulimi(int draft, ResourceManager rm, CultureInfo ci)
        {
            var isValid = true;
            var idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            var idPerdorues = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);


            if (this.dteDtRegjistrimi.Text == string.Empty)
            {
                isValid = false;
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgLidhjaDokZgjidh1DateRegjistrimi", ci), pnlMesazhi, LoadingPanel);
                return isValid;
            }
            if (this.dteDtDok.Text == string.Empty)
            {
                isValid = false;
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgLidhjaDokZgjidh1DateDokumenti", ci), pnlMesazhi, LoadingPanel);
                return isValid;
            }
            if (dteDtDok.Date.Year != new DbCore.DbAdmin.clsNdermarrjeViti(DbCore.mySessionObjects.ktheNdermarrjeVit(Session)).Viti)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgDataNukPerketVititUshtrimor", ci), pnlMesazhi);
                return false;
            }
            var periudha = DbCore.mySessionObjects.merrPeriudheKontabel(Session);
            String mesazhGabimi;
            if (!DbCore.clsFunksione.checkPeriudheKontabel(out mesazhGabimi, dteDtDok.Date, periudha, draft))
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhGabimi, pnlMesazhi);
                return false;
            }

            if (btnBurimi.Text != string.Empty)
            {
                var mag = new DbCore.DbProdhimi.clsBurime(btnBurimi.Text, idNdermarrje);
                if (mag.IdBurimi == -1)
                {
                    isValid = false;
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Burimi nuk ekziston", pnlMesazhi, LoadingPanel);

                    return isValid;
                }
                else
                {
                    if (mag.Aktiv == false)
                    {
                        isValid = false;
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Burimi nuk eshte aktiv", pnlMesazhi, LoadingPanel);

                        return isValid;
                    }
                }
            }


            return isValid;
        }
    }
}
