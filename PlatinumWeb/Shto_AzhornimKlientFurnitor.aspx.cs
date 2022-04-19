using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DbCore;
using DbCore.DbAdmin;
using DbCore.DbKontabiliteti;
using DbCore.DbQendraKosto;
using DbCore.DbRegjistrim;
using DbCore.DbShare;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Messages;
using DevExpress.Web;
using NLog;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using PlatinumWeb.Templates;

namespace PlatinumWeb
{
    public partial class Shto_AzhornimKlientFurnitor : MyPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!mySessionObjects.isLogedIn(Session))
                clsFunksione.logout(Session, true, "FaqePaautorizuar");

            if (mySessionObjects.ktheKodNdermarrje(Session) == null)
                Response.Redirect("Login_Ndermarrje.aspx?id=" + IdPerdoruesi);

            var periudha = mySessionObjects.merrPeriudheKontabel(Session);
            btnPeriudha.Text = periudha.NrPeriudha.ToString();
            lblPeriudhaAktuale.Text = $"{periudha.FillimiPeriudha.ToShortDateString()}-{periudha.MbarimiPeriudha.ToShortDateString()}";

            PercaktoTemplateMenu();

            if (!IsPostBack)
            {
                MbushHiddenFieldMePerkthime();

                hfState.Set("idGjuha", IdGjuha);
                hfState.Set("idNdermarrje", IdNdermarrja);
                hfState.Set("idPerdoruesi", IdPerdoruesi);
                hfState.Set("idViti", IdViti);
                mySessionObjects.ruajColKFNeSesion(Session, new colKlienteFurnitore());
                KonfiguroGriden();

                if (string.IsNullOrEmpty(Request.QueryString["shtim_modifikim"]) || Request.QueryString["shtim_modifikim"] == "shtim")
                {
                    hfShtimModifikim.Value = "shtim";
                    KonfiguroVleraFillestareShto();
                }
                else
                {
                    hfShtimModifikim.Value = "modifikim";
                    KonfiguroVleraFillestareModifiko();
                }

                PercaktoTemplateMenu();

                var mon = new colMonedhat();
                mon.mbushGjitheMonedhatPozitive(IdNdermarrja, IdPerdoruesi);

                var alternativeKushti = clsAlternativaKushti.getAlternativa(int.Parse(cmbKonfigurimi.Value.ToString()), "LLK");
                var llojkursi = int.Parse(alternativeKushti.Substring(alternativeKushti.Length - 1, 1));

                foreach (var m in mon)
                {
                    var kursi = clsKurset.merrKursinFunditPerMonedheDateDheLloj(m.IdMonedha, DateTime.Today, llojkursi);
                    if (kursi != 0)
                        hfKurset.Value = hfKurset.Value + m.PershkrimiMonedha + "|" + kursi + "||";
                    else
                        hfKurset.Value = hfKurset.Value + m.PershkrimiMonedha + "|1||";
                }

                var tedrejtaInfo = new clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, "Info KF");
                hfTeDrejtaInfoKF.Value = tedrejtaInfo.DAmb.ToString();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, clsFunksione.GetKomponente(Page.Request));

                var perdoruesi = new clsPerdorues(IdPerdoruesi);
                hfHapurMbyllur.Value = perdoruesi.InfoHapur.ToString();
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                hfTeDrejta.Add("ShtimDraft", tedrejtaInfo.DShtimDraft);
                hfTeDrejta.Add("ModifikimDraft", tedrejtaInfo.DModifikimDraft);

                GridUtil.perktheButonaGride(hfState, mySessionObjects.ktheCultureInfo(Session));
            }

            AspxWebControlUtils.perkthePopUp(popFshi, rm.GetString("labelKujdes", ci), lblMsgbox, rm.GetString("labelAdministrimiMsgJeniSigurt", ci), ButtonCancel, rm.GetString("labelAnullo", ci));
            popupUniversal.HeaderText = rm.GetString("headerPopUpText", ci);
            hfMonedhaNder.Value = clsMonedha.ktheMonedhenENdermarrjes(IdNdermarrja);
            btnZgjidhGjitha.ToolTip = rm.GetString("zgjidhTeGjithaBtn", ci);
            btnHiqZgjedhjen.ToolTip = rm.GetString("hiqZgjedhjenBtn", ci);
            gridaSelectTeGjitha.ToolTip = rm.GetString("btnZgjidhTeGjithe", ci);
            gvKF.DataSource = mySessionObjects.merrColKFNgaSesioni(Session);
            gvKF.DataBind();
        }

        private void VendosDataDefault()
        {
            var sot = DateTime.Today;
            var periudha = mySessionObjects.merrPeriudheKontabel(Session);

            if (sot >= periudha.FillimiPeriudha && sot <= periudha.MbarimiPeriudha)
                Data_DateEdit.Value = DateTime.Today;
            else
                Data_DateEdit.Value = periudha.FillimiPeriudha;

            dteDtRegjistrimi.Value = DateTime.Today;
        }

        public void MerrTedhenat(clsAzhornimKFKoka koka)
        {
            ConfigureAspxComboBox.KonfiguroComboBoxKonfigurimeshSipasKategorise(cmbKonfigurimi, true, IdPerdoruesi, IdNdermarrja, IdGjuha, Request.QueryString["vep"] == "azhornim" ? 11 : 64);
            hfKonffillestar.Value = cmbKonfigurimi.SelectedItem.Text;

            var konf = new clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasId(koka.IdKonfigAmbjente, IdGjuha);
            cmbKonfigurimi.Text = konf.KodKonfigAmbjente;
            hfKonffillestar.Value = konf.KodKonfigAmbjente;
            txtNrDokumenti.Text = koka.NrDok;
            Data_DateEdit.Date = koka.DateDok;
            dteDtRegjistrimi.Date = koka.DateRegjistrimi;
            txtPershkrimi.Text = koka.Pershkrimi;
            llogariDebi_ButtonEdit.Text = new clsLlogari(koka.IdLlogariDebi).NrLlogari;
            llogariKredi_ButtonEdit.Text = new clsLlogari(koka.IdLlogariKredi).NrLlogari;

            var dbAdmin = new clsDatabaseAdmin();
            var dtlidhur = dbAdmin.MerrDokLidhur(koka.IdAzhornimKfKoka, koka.IdNivel, "T_AZHORNIMKFKOKA", "IDAZHORNIMKFKOKA");
            hfLidhur.Value = (dtlidhur.Rows.Count != 0).ToString();

            var autorizimet = clsAzhornimKFKoka.KaAutorizime(koka.IdAzhornimKfKoka, IdPerdoruesi);
            hfAutorizimi.Value = autorizimet.ToString();
            if (!autorizimet)
                hfLidhur.Value = "True";

            var formatNrPerKonfig = new clsFormatiKonfig();
            formatNrPerKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(koka.IdKonfigAmbjente);
            if (formatNrPerKonfig.KonfigTrupi.Count == 0)
            {
                formatNrPerKonfig.KonfigTrupi.Add(new clsFormatKonfigTrup(clsFormatKonfigTrup.defaultFormatSasia, clsFormatKonfigTrup.defaultFormatCmimi, clsFormatKonfigTrup.defaultFormatVlefta, clsFormatKonfigTrup.defaultFormatZbritja, clsFormatKonfigTrup.defaultFormatStringSasia, clsFormatKonfigTrup.defaultFormatStringCmimi, clsFormatKonfigTrup.defaultFormatStringVlefta, clsFormatKonfigTrup.defaultFormatStringZbritja));
            }

            var serializusi = new JavaScriptSerializer();
            hfState.Set("formatMonedhe", serializusi.Serialize(formatNrPerKonfig));

            var mon = new colMonedhat();
            mon.mbushGjitheMonedhat(IdNdermarrja, IdPerdoruesi);
            hfState.Set("formatKurset", serializusi.Serialize(mon));

            AspxWebControlUtils.ShtoLidhje(IdPerdoruesi, IdViti, IdNdermarrja, hl, dtlidhur, koka.IdGjenerues, koka.IdNivelGjenerues, koka.IdKonfigGjenerues, IdGjuha);
            dbAdmin.Dispose();
        }

        public void MerrTedhenat2(clsKokaMbylljeKF koka)
        {
            ConfigureAspxComboBox.KonfiguroComboBoxKonfigurimeshSipasKategorise(cmbKonfigurimi, true, IdPerdoruesi, IdNdermarrja, IdGjuha, Request.QueryString["vep"] == "azhornim" ? 11 : 64);
            hfKonffillestar.Value = cmbKonfigurimi.SelectedItem.Text;

            var konf = new clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasId(koka.IdKonfigAmbjente, IdGjuha);

            cmbKonfigurimi.Text = konf.KodKonfigAmbjente;
            hfKonffillestar.Value = konf.KodKonfigAmbjente;

            txtNrDokumenti.Text = koka.NrDok;
            Data_DateEdit.Date = koka.DateDok;
            dteDtRegjistrimi.Date = koka.DateRegjistrimi;
            txtPershkrimi.Text = koka.Pershkrimi;
            llogariDebi_ButtonEdit.Text = new clsLlogari(koka.IdLlogDebi).NrLlogari;
            llogariKredi_ButtonEdit.Text = new clsLlogari(koka.IdLlogKredi).NrLlogari;

            var dbAdmin = new clsDatabaseAdmin();
            var dtlidhur = dbAdmin.MerrDokLidhur(koka.IdKoka, koka.IdNivel, "T_KOKAMBYLLJEKF", "IDKOKA");
            hfLidhur.Value = (dtlidhur.Rows.Count != 0).ToString();

            var autorizimet = clsKokaMbylljeKF.KaAutorizime(koka.IdKoka, IdPerdoruesi);
            hfAutorizimi.Value = autorizimet.ToString();
            if (!autorizimet)
                hfLidhur.Value = "True";

            var formatNrPerKonfig = new clsFormatiKonfig();
            formatNrPerKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(koka.IdKonfigAmbjente);
            if (formatNrPerKonfig.KonfigTrupi.Count == 0)
            {
                formatNrPerKonfig.KonfigTrupi.Add(new clsFormatKonfigTrup(clsFormatKonfigTrup.defaultFormatSasia, clsFormatKonfigTrup.defaultFormatCmimi, clsFormatKonfigTrup.defaultFormatVlefta, clsFormatKonfigTrup.defaultFormatZbritja, clsFormatKonfigTrup.defaultFormatStringSasia, clsFormatKonfigTrup.defaultFormatStringCmimi, clsFormatKonfigTrup.defaultFormatStringVlefta, clsFormatKonfigTrup.defaultFormatStringZbritja));
            }

            var serializusi = new JavaScriptSerializer();
            hfState.Set("formatMonedhe", serializusi.Serialize(formatNrPerKonfig));

            var mon = new colMonedhat();
            mon.mbushGjitheMonedhat(IdNdermarrja, IdPerdoruesi);
            hfState.Set("formatKurset", serializusi.Serialize(mon));

            AspxWebControlUtils.ShtoLidhje(IdPerdoruesi, IdViti, IdNdermarrja, hl, dtlidhur, koka.IdGjenerues, koka.IdNivelGjenerues, koka.IdKonfigGjenerues, IdGjuha);
            dbAdmin.Dispose();
        }

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="ASPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        private void PercaktoTemplateMenu()
        {
            colMenuItem menu = new colMenuItem(mySessionObjects.ktheGjuhe(Session));
            menu.merrMenuItemSipasKomponentesRegjistrime(mySessionObjects.ktheGjuhe(Session), clsFunksione.GetKomponente(Page.Request), IdPerdoruesi, IdNdermarrja, IdViti, hfShtimModifikim.Value == "modifikim" ? false : true);
            clsKokaFleteKontabel kok = new clsKokaFleteKontabel();
            foreach (clsMenuItem m in menu)
            {
                if (m.Name != "ItemFilter" && m.Name != "ItemFrame")
                {
                    clsToolbarConfig.ShtoMenuItem(Theme, ASPxMenu1, m);
                }
                if ((hfLidhur.Value == "True") && m.Name == "Draft")
                {
                    ASPxMenu1.Items[ASPxMenu1.Items.Count - 1].ClientVisible = false;
                }
                if ((hfShtimModifikim.Value != "modifikim") && m.Name == "Fshi")
                    ASPxMenu1.Items[ASPxMenu1.Items.Count - 1].ClientVisible = false;
                if (m.Name == "FletaKontabel")
                {
                    if ((hfShtimModifikim.Value != "modifikim"))
                    {
                        ASPxMenu1.Items[ASPxMenu1.Items.Count - 1].ClientVisible = false;
                    }
                    else
                    {
                        kok = Request.QueryString["vep"] == "azhornim"
                            ? new clsKokaFleteKontabel(int.Parse(Request.QueryString["id"]), 11)
                            : new clsKokaFleteKontabel(int.Parse(Request.QueryString["id"]), 64);
                        if (kok.NrDukumentiKokaFleteKontabel != null)
                            ASPxMenu1.Items[ASPxMenu1.Items.Count - 1].NavigateUrl = "javascript: myFaqeCelje.kontrolloTeDrejta('Shto_FleteKontabel.aspx?shtim_modifikim=modifikim&id=" + kok.IdKokaFleteKontabel + "&numur=" + kok.NrDukumentiKokaFleteKontabel + "')";
                        else
                        {

                            ASPxMenu1.Items[ASPxMenu1.Items.Count - 1].ClientVisible = false;

                        }
                    }
                }
                if (m.Name == "QendraKosto")
                {
                    if ((hfShtimModifikim.Value != "modifikim"))
                    {
                        ASPxMenu1.Items[ASPxMenu1.Items.Count - 1].ClientVisible = false;

                    }
                    else
                    {
                        if (kok.NrDukumentiKokaFleteKontabel != null)
                        {
                            clsKokaQendraKosto qend = new clsKokaQendraKosto();
                            qend.KtheKokaQKSipasIDGjeneruesDheKonfig(kok.IdKokaFleteKontabel, kok.IdKonfigAmbjente);


                            if (qend.NrDok != null)
                                ASPxMenu1.Items[ASPxMenu1.Items.Count - 1].NavigateUrl = "javascript: myButtonClickLupa.LupaUniversal_Click('Shperndarje ne qendrat e kostos','LupaRegjistrimQendraKosto.aspx?shtim_modifikim=modifikim&idDokGjenerues=" + kok.IdKokaFleteKontabel + "&idkonfig=" + kok.IdKonfigAmbjente + "',900,600)";
                            else
                            {
                                ASPxMenu1.Items[ASPxMenu1.Items.Count - 1].ClientVisible = false;

                            }
                        }
                        else ASPxMenu1.Items[ASPxMenu1.Items.Count - 1].ClientVisible = false;
                    }
                }
                if (m.Name == "ItemFrame")
                    clsToolbarConfig.ShtoMenuItemPerFrame(this, ASPxMenu1, clsFunksione.ktheUrlHelpi(m.UrlHelp).Item1);
                if (m.Name == "Ruaj")
                    ASPxMenu1.Items[ASPxMenu1.Items.Count - 1].ClientVisible = true;
                if (m.Name == "Shto" || m.Name == "Kerko" || m.Name == "Pastro" || m.Name == "ItemFilter" || m.Name == "ItemFrame")
                    ASPxMenu1.Items[ASPxMenu1.Items.Count - 1].BeginGroup = true;
            }
            clsMenuInfo.ShtoMenuItemInfo(this, MenuInfo);
            ASPxMenu1.Items.FindByName("Draft").ClientVisible = clsFunksione.merrMenuVisibleDraft(hfShtimModifikim.Value != "modifikim", kok.IdStatusDokumenti);
        }

        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e) => PercaktoTemplateMenu();

        private void KonfiguroGriden()
        {
            var konfigurimi = new clsKonfigurimAmbjenti();
            konfigurimi.mbushKonfigDefaultKomponentes(Request.QueryString["vep"] == "azhornim"
                ? 650
                : 679, IdNdermarrja);

            hfKolonaGride.Value = GridUtil.percaktoVisibleColumnsSipasKonfigurimitPerClientSide(clsFunksione.GetKomponente(Page.Request), konfigurimi.IdKonfigAmbjente, IdGjuha);
        }

        private void KonfiguroVleraFillestareShto()
        {
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(btnPeriudha);
            ConfigureAspxComboBox.ShtoKolonaPerLlogarine(llogariDebi_ButtonEdit);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(llogariDebi_ButtonEdit);
            ConfigureAspxComboBox.ShtoKolonaPerLlogarine(llogariKredi_ButtonEdit);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(llogariKredi_ButtonEdit);

            ConfigureAspxComboBox.KonfiguroComboBoxKonfigurimeshSipasKategorise(cmbKonfigurimi, false, IdPerdoruesi, IdNdermarrja, IdGjuha, Request.QueryString["vep"] == "azhornim" ? 11 : 64);
            hfKonffillestar.Value = cmbKonfigurimi.SelectedItem.Text;

            AspxWebControlUtils.vendosDateEditMask(Data_DateEdit);
            AspxWebControlUtils.vendosDateEditMask(dteDtRegjistrimi);
            VendosDataDefault();
            MbushListeKlientFurnitoresh();
            KonfiguroGride();

            var formatNrPerKonfig = new clsFormatiKonfig();
            formatNrPerKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(int.Parse(cmbKonfigurimi.Value.ToString()));
            if (formatNrPerKonfig.KonfigTrupi.Count == 0)
            {
                formatNrPerKonfig.KonfigTrupi.Add(new clsFormatKonfigTrup(clsFormatKonfigTrup.defaultFormatSasia, clsFormatKonfigTrup.defaultFormatCmimi, clsFormatKonfigTrup.defaultFormatVlefta, clsFormatKonfigTrup.defaultFormatZbritja, clsFormatKonfigTrup.defaultFormatStringSasia, clsFormatKonfigTrup.defaultFormatStringCmimi, clsFormatKonfigTrup.defaultFormatStringVlefta, clsFormatKonfigTrup.defaultFormatStringZbritja));
            }

            var serializusi = new JavaScriptSerializer();
            hfState.Set("formatMonedhe", serializusi.Serialize(formatNrPerKonfig));

            var mon = new colMonedhat();
            mon.mbushGjitheMonedhat(IdNdermarrja, IdPerdoruesi);
            hfState.Set("formatKurset", serializusi.Serialize(mon));
        }

        private void KonfiguroVleraFillestareModifiko()
        {
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(btnPeriudha);
            ConfigureAspxComboBox.ShtoKolonaPerLlogarine(llogariDebi_ButtonEdit);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(llogariDebi_ButtonEdit);
            ConfigureAspxComboBox.ShtoKolonaPerLlogarine(llogariKredi_ButtonEdit);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(llogariKredi_ButtonEdit);
            AspxWebControlUtils.vendosDateEditMask(Data_DateEdit);
            AspxWebControlUtils.vendosDateEditMask(dteDtRegjistrimi);

            if (Request.QueryString["vep"] == "azhornim")
            {
                var koka = new clsAzhornimKFKoka(int.Parse(Request.QueryString["id"]));
                MerrTedhenat(koka);
                MbushHiddenFieldet(koka.OColAzhornimKfTrupi);
            }
            else
            {
                var koka = new clsKokaMbylljeKF(int.Parse(Request.QueryString["id"]));
                MerrTedhenat2(koka);
                MbushHiddenFieldet2(koka.OColTrupi);
            }
        }

        private void MbushHiddenFieldet(colAzhornimKFTrupi col)
        {
            var mon = new clsMonedha();
            mon.mbushMonedhenENdermarrjes(IdNdermarrja);

            var colkf = new colKlienteFurnitore();
            colkf.MbushKlienteFurnitoreNdermarrjesAndAutorizimeMonHuajMeGjendje(IdNdermarrja, IdPerdoruesi, mon.IdMonedha, Data_DateEdit.Date);

            foreach (var t in col)
            {
                var ar = colkf.Where(l => l.IdKlientFurnitor == t.IdKlientFurnitor);
                colkf.Remove(ar.First());
            }

            mySessionObjects.ruajColKFNeSesion(Session, colkf);
            gvKF.DataSource = colkf;
            gvKF.DataBind();
            KonfiguroGride();

            var serializusi = new JavaScriptSerializer { MaxJsonLength = 50000000 };
            HfColTrup.Value = serializusi.Serialize(col);
            HfColLlog.Value = serializusi.Serialize(col.KtheColLlogarite(IdNdermarrja));
            HfColLlogKunder.Value = serializusi.Serialize(col.KtheColLlogariteKunder(IdNdermarrja));
            HfColKF.Value = serializusi.Serialize(col.KtheColKlientFurnitor(IdNdermarrja));
        }

        private void MbushHiddenFieldet2(colTrupiMbylljeKF col)
        {
            var mon = new clsMonedha();
            mon.mbushMonedhenENdermarrjes(IdNdermarrja);

            var colkf = new colKlienteFurnitore();
            colkf.MbushKlienteFurnitoreNdermarrjesAndAutorizimeGjendjeMonLlogDheMonBaze(IdNdermarrja, IdPerdoruesi, Data_DateEdit.Date);

            mySessionObjects.ruajColKFNeSesion(Session, colkf);
            gvKF.DataSource = colkf;
            gvKF.DataBind();
            KonfiguroGride();

            var serializusi = new JavaScriptSerializer { MaxJsonLength = 50000000 };
            HfColTrup.Value = serializusi.Serialize(col);
            HfColLlog.Value = serializusi.Serialize(col.KtheColLlogarite(IdNdermarrja));
            HfColLlogKunder.Value = serializusi.Serialize(col.KtheColLlogariteKunder(IdNdermarrja));
            HfColKF.Value = serializusi.Serialize(col.KtheColKlientFurnitor(IdNdermarrja));
        }
        
        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {
            if (e.Item.Name == "Ruaj")
            {
                Page.Validate();
                if (Request.QueryString["vep"] == "azhornim")
                {
                    RuajVeprimAzhornimi(1);
                    MbushHiddenFieldet(new colAzhornimKFTrupi());
                }
                else
                {
                    RuajVeprimMbyllje(1);
                    MbushHiddenFieldet2(new colTrupiMbylljeKF());
                }
            }
            else if (e.Item.Name == "Draft")
            {
                Page.Validate();
                if (Request.QueryString["vep"] == "azhornim")
                {
                    RuajVeprimAzhornimi(0);
                    MbushHiddenFieldet(new colAzhornimKFTrupi());
                }
                else
                {
                    RuajVeprimMbyllje(0);
                    MbushHiddenFieldet2(new colTrupiMbylljeKF());
                }
            }
        }

        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            pergjigja.Text = "";
            if (Request.QueryString["vep"] == "azhornim")
            {
                var koka = new clsAzhornimKFKoka(int.Parse(Request.QueryString["id"]));
                var dbAdmin = new clsDatabaseAdmin();
                koka.IdPerdorues = IdPerdoruesi;
                if (dbAdmin.eshteDokumentiILidhur(koka.IdAzhornimKfKoka, koka.IdNivel, "T_AZHORNIMKFKOKA", "IDAZHORNIMKFKOKA"))
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgDokLidhurNukFshihet", ci), pnlMesazhi);
                    return;
                }

                if (koka.DateDok.Year != new clsNdermarrjeViti(mySessionObjects.ktheNdermarrjeVit(Session)).Viti)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgDataNukPerketVititUshtrimor", ci), pnlMesazhi);
                    return;
                }

                if (clsPeriudhaKontabel.eshteKycurPeriudheSipasDateDheNdermarrjes(koka.DateDok, IdNdermarrja))
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgPeriudhaEKycur", ci), pnlMesazhi);
                    return;
                }

                if (DbCore.MbylljePeriudhe.PeriodClosing.IsPeriodClosed(koka.DateDok, MyConnectionsManager.GetSelectedConNameServer(), IdNdermarrja, KategoriDokumenti.AzhornimKlientFurnitor, koka.IdKonfigAmbjente))
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgPeriodIsClosed"], pnlMesazhi);
                    return;
                }

                var mesazhi = koka.Fshi();
                dbAdmin.Dispose();
                if (mesazhi.Status)
                    Response.Redirect("AzhornimKlientFurnitor.aspx?vep=azhornim&fshi=po");
                else
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhi.PershkrimMesazhi, pnlMesazhi);
            }
            else
            {
                var koka = new clsKokaMbylljeKF(int.Parse(Request.QueryString["id"]));
                var dbAdmin = new clsDatabaseAdmin();
                koka.IdPerdorues = IdPerdoruesi;

                if (dbAdmin.eshteDokumentiILidhur(koka.IdKoka, koka.IdNivel, "T_KOKAMBYLLJEKF", "IDKOKA"))
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgDokLidhurNukFshihet", ci), pnlMesazhi);
                    return;
                }

                if (koka.DateDok.Year != new clsNdermarrjeViti(mySessionObjects.ktheNdermarrjeVit(Session)).Viti)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgDataNukPerketVititUshtrimor", ci), pnlMesazhi);
                    return;
                }

                if (clsPeriudhaKontabel.eshteKycurPeriudheSipasDateDheNdermarrjes(koka.DateDok, IdNdermarrja))
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgPeriudhaEKycur", ci), pnlMesazhi);
                    return;
                }

                if (DbCore.MbylljePeriudhe.PeriodClosing.IsPeriodClosed(koka.DateDok, MyConnectionsManager.GetSelectedConNameServer(), IdNdermarrja, KategoriDokumenti.AzhornimKlientFurnitor, koka.IdKonfigAmbjente))
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgPeriodIsClosed"], pnlMesazhi);
                    return;
                }

                var mesazhi = koka.Fshi();
                dbAdmin.Dispose();

                if (mesazhi.Status)
                    Response.Redirect("AzhornimKlientFurnitor.aspx?vep=mbyllje&fshi=po");
                else
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhi.PershkrimMesazhi, pnlMesazhi);
            }
        }

        private void RuajVeprimAzhornimi(int status)
        {
            var mesazh = new clsMesazh();
            var meKontabilizim = false;
            string shfaqmesazhapolupe;
            if (Page.IsValid == false)
                return;

            var konf = new clsKonfigurimAmbjenti();
            if (cmbKonfigurimi.Text != "")
                konf.mbushKonfigAmbjSipasKod(cmbKonfigurimi.Text, IdNdermarrja);
            else
                konf.mbushKonfigDefaultKomponentes(650, IdNdermarrja);

            if (IsValidVeprimAzhornimi(status, konf, false))
            {
                if (status == 1)//nese nuk eshte draft do gjeneroje kontabilizim perndryshe jo
                {
                    if (hfKontabilizimi.Value == "1" || hfKontabilizimi.Value == "2")
                    {
                        meKontabilizim = true;
                    }
                }
                clsAzhornimKFKoka koka;
                try
                {
                    koka = KrijoVeprimAzhornimi(status, meKontabilizim, konf, out shfaqmesazhapolupe);
                }
                catch (Exception ex)
                {
                    LogManager.GetCurrentClassLogger().Error(ex.Message);
                    hfStatusRuajtje.Value = "false";
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, ex.Message, pnlMesazhi);
                    return;
                }

                koka.OKokaFleteKontabel.Kontabilizuar = hfKontabilizimi.Value == "1";

                var tedrejtaInfo = new clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, clsFunksione.GetKomponente(Page.Request));

                if (hfShtimModifikim.Value == "shtim")
                {
                    if ((status == 1 && !tedrejtaInfo.DShtim) || (status == 0 && !tedrejtaInfo.DShtimDraft))
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNukKeniTeDrejta", ci), pnlMesazhi);
                        hfStatusRuajtje.Value = "false";
                        return;
                    }
                    mesazh = koka.Ruaj();
                }
                else if (hfShtimModifikim.Value == "modifikim")
                {
                    if ((status == 1 && !tedrejtaInfo.DMod) || (status == 0 && !tedrejtaInfo.DModifikimDraft))
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNukKeniTeDrejta", ci), pnlMesazhi);
                        hfStatusRuajtje.Value = "false";
                        return;
                    }

                    koka.IdAzhornimKfKoka = int.Parse(Request.QueryString["id"]);
                    using (var dbAdmin = new clsDatabaseAdmin())
                    {
                        var lidhur = dbAdmin.eshteDokumentiILidhur(koka.IdAzhornimKfKoka, koka.IdNivel, "T_AZHORNIMKFKOKA", "IDAZHORNIMKFKOKA");

                        if (lidhur.ToString() != hfLidhur.Value)
                        {
                            mesazh.Status = false;
                            mesazh.PershkrimMesazhi = rm.GetString("msgDokumentiEshteILidhur", ci);
                            hfStatusRuajtje.Value = "false";
                        }
                        else
                            mesazh = koka.Modifiko(lidhur);
                    }
                }

                if (mesazh.Status)
                {
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("msgRuajtjeMeSukses", ci), pnlMesazhi);
                    hfqkmesazhi.Value = shfaqmesazhapolupe;

                    if (shfaqmesazhapolupe != "jo")
                    {
                        var kok = new clsKokaFleteKontabel(koka.IdAzhornimKfKoka, 11);
                        hfUrl.Value = "LupaRegjistrimQendraKosto.aspx?shtim_modifikim=modifikim&idDokGjenerues=" + kok.IdKokaFleteKontabel + "&idkonfig=" + kok.IdKonfigAmbjente;
                    }

                    hfStatusRuajtje.Value = "true";
                    hl = new HtmlTable();
                    pnlLidhur.Update();
                    hfShtimModifikim.Value = "shtim";

                }
                else
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                    hfStatusRuajtje.Value = "false";
                }

            }
            else hfStatusRuajtje.Value = "false";
            MbushListeKlientFurnitoresh();
            KonfiguroGride();
        }

        private void RuajVeprimMbyllje(int status)
        {
            var mesazh = new clsMesazh();
            var meKontabilizim = false;
            string shfaqmesazhapolupe;
            if (Page.IsValid == false)
                return;

            var konf = new clsKonfigurimAmbjenti();
            if (cmbKonfigurimi.Text != "")
                konf.mbushKonfigAmbjSipasKod(cmbKonfigurimi.Text, IdNdermarrja);
            else
                konf.mbushKonfigDefaultKomponentes(650, IdNdermarrja);

            if (IsValidVeprimAzhornimi(status, konf, true))
            {
                if (status == 1)//nese nuk eshte draft do gjeneroje kontabilizim perndryshe jo
                {
                    if (hfKontabilizimi.Value == "1" || hfKontabilizimi.Value == "2")
                    {
                        meKontabilizim = true;
                    }
                }
                clsKokaMbylljeKF koka;
                try
                {
                    koka = KrijoVeprimMbyllje(status, meKontabilizim, konf, out shfaqmesazhapolupe);
                }
                catch (Exception ex)
                {
                    LogManager.GetCurrentClassLogger().Error(ex.Message);
                    hfStatusRuajtje.Value = "false";
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, ex.Message, pnlMesazhi);
                    return;
                }

                koka.OKokaFleteKontabel.Kontabilizuar = hfKontabilizimi.Value == "1";

                var tedrejtaInfo = new clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, clsFunksione.GetKomponente(Page.Request));
                if (hfShtimModifikim.Value == "shtim")
                {
                    if (!tedrejtaInfo.DShtim)
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNukKeniTeDrejta", ci), pnlMesazhi);
                        hfStatusRuajtje.Value = "false";
                        return;
                    }

                    mesazh = koka.Ruaj(rm, ci);
                }
                else if (hfShtimModifikim.Value == "modifikim")
                {
                    if (!tedrejtaInfo.DMod)
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNukKeniTeDrejta", ci), pnlMesazhi);
                        hfStatusRuajtje.Value = "false";
                        return;
                    }
                    koka.IdKoka = int.Parse(Request.QueryString["id"]);
                    using (var dbAdmin = new clsDatabaseAdmin())
                    {
                        var lidhur = dbAdmin.eshteDokumentiILidhur(koka.IdKoka, koka.IdNivel, "T_KOKAMBYLLJEKF", "IDKOKA");
                        if (lidhur.ToString() != hfLidhur.Value)
                        {
                            mesazh.Status = false;
                            mesazh.PershkrimMesazhi = rm.GetString("msgDokumentiEshteILidhur", ci);
                            hfStatusRuajtje.Value = "false";
                        }
                        else
                            mesazh = koka.Modifiko(lidhur);
                    }
                }

                if (mesazh.Status)
                {
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("msgRuajtjeMeSukses", ci), pnlMesazhi);
                    hfqkmesazhi.Value = shfaqmesazhapolupe;

                    if (shfaqmesazhapolupe != "jo")
                    {
                        var kok = new clsKokaFleteKontabel(koka.IdKoka, 64);
                        hfUrl.Value = "LupaRegjistrimQendraKosto.aspx?shtim_modifikim=modifikim&idDokGjenerues=" + kok.IdKokaFleteKontabel + "&idkonfig=" + kok.IdKonfigAmbjente;
                    }

                    hfStatusRuajtje.Value = "true";
                    hl = new HtmlTable();
                    pnlLidhur.Update();
                    hfShtimModifikim.Value = "shtim";
                }
                else
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                    hfStatusRuajtje.Value = "false";
                }

            }
            else hfStatusRuajtje.Value = "false";
            MbushListeKlientFurnitoresh();
            KonfiguroGride();
        }

        private clsAzhornimKFKoka KrijoVeprimAzhornimi(int statusdok, bool mekontabilizim, clsKonfigurimAmbjenti konf, out string shfaqmesazhapolupe)
        {
            var idllogdeb = clsLlogari.mbushIDLlogariSipasKodit(llogariDebi_ButtonEdit.Text, IdNdermarrja);
            var idllogkred = clsLlogari.mbushIDLlogariSipasKodit(llogariKredi_ButtonEdit.Text, IdNdermarrja);

            var qend = new clsKokaQendraKosto();
            if (hfShtimModifikim.Value == "modifikim")
            {
                var kokaFleteKontabel = Request.QueryString["vep"] == "azhornim"
                    ? new clsKokaFleteKontabel(int.Parse(Request.QueryString["id"]), 11)
                    : new clsKokaFleteKontabel(int.Parse(Request.QueryString["id"]), 64);

                qend.KtheKokaQKSipasIDGjeneruesDheKonfigMeTrup(kokaFleteKontabel.IdKokaFleteKontabel, kokaFleteKontabel.IdKonfigAmbjente);
            }

            var koka = new clsAzhornimKFKoka();
            koka.KrijoAzhornim(txtNrDokumenti.Text, Data_DateEdit.Date, dteDtRegjistrimi.Date, idllogdeb, idllogkred, llogariDebi_ButtonEdit.Text, llogariKredi_ButtonEdit.Text, txtPershkrimi.Text, 1, IdNdermarrja, IdNdermarrjeVit, IdPerdoruesi, konf.IdNivel, konf.IdKonfigAmbjente, 0, 0, 0, 0, statusdok, RuajTrupin(), mySessionObjects.merrPeriudheKontabel(Session).IdPeriudha, mekontabilizim, out shfaqmesazhapolupe, qend.ColTrupi, IdGjuha, rm, ci);
            return koka;
        }

        private clsKokaMbylljeKF KrijoVeprimMbyllje(int statusdok, bool mekontabilizim, clsKonfigurimAmbjenti konf, out string shfaqmesazhapolupe)
        {
            var idllogdeb = clsLlogari.mbushIDLlogariSipasKodit(llogariDebi_ButtonEdit.Text, IdNdermarrja);
            var idllogkred = clsLlogari.mbushIDLlogariSipasKodit(llogariKredi_ButtonEdit.Text, IdNdermarrja);

            var qend = new clsKokaQendraKosto();
            if (hfShtimModifikim.Value == "modifikim")
            {
                var kokaFleteKontabel = Request.QueryString["vep"] == "azhornim"
                    ? new clsKokaFleteKontabel(int.Parse(Request.QueryString["id"]), 11)
                    : new clsKokaFleteKontabel(int.Parse(Request.QueryString["id"]), 64);

                qend.KtheKokaQKSipasIDGjeneruesDheKonfigMeTrup(kokaFleteKontabel.IdKokaFleteKontabel, kokaFleteKontabel.IdKonfigAmbjente);
            }
            try
            {
                var koka = new clsKokaMbylljeKF();
                var mesazh = koka.KrijoMbyllje(txtNrDokumenti.Text, Data_DateEdit.Date, dteDtRegjistrimi.Date, idllogdeb, idllogkred, llogariDebi_ButtonEdit.Text, llogariKredi_ButtonEdit.Text, txtPershkrimi.Text, IdNdermarrja, IdNdermarrjeVit, IdPerdoruesi, konf.IdNivel, konf.IdKonfigAmbjente, 0, 0, 0, 0, statusdok, RuajTrupin2(), mySessionObjects.merrPeriudheKontabel(Session).IdPeriudha, mekontabilizim, out shfaqmesazhapolupe, qend.ColTrupi, IdGjuha, rm, ci);
                if (mesazh.Status)
                    return koka;
                throw new Exception(mesazh.PershkrimMesazhi);
            }
            catch (Exception e)
            {
                LogManager.GetCurrentClassLogger().Error(e.Message);
                throw new Exception(e.Message);
            }
        }

        private colAzhornimKFTrupi RuajTrupin()
        {
            var serializusi = new JavaScriptSerializer();
            var dokumenti = (object[])serializusi.DeserializeObject(gridDataObject.Value);
            var trupat = new colAzhornimKFTrupi();
            trupat.AddRange(dokumenti.Select(dokument => new clsAzhornimKFTrupi((Dictionary<string, object>)dokument, IdNdermarrja, IdPerdoruesi)).Where(trup => trup.IdKlientFurnitor != -1 && trup.IdLlogariKp != -1));

            return trupat;
        }

        private colTrupiMbylljeKF RuajTrupin2()
        {
            var serializusi = new JavaScriptSerializer();
            var dokumenti = (object[])serializusi.DeserializeObject(gridDataObject.Value);

            var trupat = new colTrupiMbylljeKF();
            trupat.AddRange(dokumenti.Select(dokument => new clsTrupiMbylljeKF((Dictionary<string, object>)dokument, IdNdermarrja, IdPerdoruesi)).Where(trup => trup.IdKf != -1 && trup.IdLlogariKp != -1));

            return trupat;
        }

        private bool IsValidVeprimAzhornimi(int draft, clsKonfigurimAmbjenti konf, bool isMbyllje)
        {
            var periudha = mySessionObjects.merrPeriudheKontabel(Session);
            string mesazhGabimi;
            if (!clsFunksione.checkPeriudheKontabel(out mesazhGabimi, Data_DateEdit.Date, periudha, draft))
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhGabimi, pnlMesazhi);
                return false;
            }

            if (DbCore.MbylljePeriudhe.PeriodClosing.IsPeriodClosed(Data_DateEdit.Date, MyConnectionsManager.GetSelectedConNameServer(), IdNdermarrja, isMbyllje ? KategoriDokumenti.MbylljeKF : KategoriDokumenti.AzhornimKlientFurnitor, konf.IdKonfigAmbjente))
            {

                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgPeriodIsClosed"], pnlMesazhi);
                return false;
            }

            if (Data_DateEdit.Date.Year != new clsNdermarrjeViti(mySessionObjects.ktheNdermarrjeVit(Session)).Viti)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgLidhjaDokDataNukIPerketVitiUshtrTeZgjedhur"), pnlMesazhi);
                return false;
            }

            if (llogariDebi_ButtonEdit.Text != "")
            {
                if (!clsLlogari.ekzistonLlogari(llogariDebi_ButtonEdit.Text, IdNdermarrja))
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgLlogDebiNukEkziston", ci), pnlMesazhi);
                    return false;
                }

                if (!clsLlogari.eshteLlogariAktive(llogariDebi_ButtonEdit.Text, IdNdermarrja))
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgLlogDebiJoAktive", ci), pnlMesazhi);
                    return false;
                }
            }

            if (llogariKredi_ButtonEdit.Text != "")
            {
                if (!clsLlogari.ekzistonLlogari(llogariKredi_ButtonEdit.Text, IdNdermarrja))
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgLlogKrediNukEkziston", ci), pnlMesazhi);
                    return false;
                }

                if (!clsLlogari.eshteLlogariAktive(llogariKredi_ButtonEdit.Text, IdNdermarrja))
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgLlogKrediJoAktive", ci), pnlMesazhi);
                    return false;
                }
            }

            //Kontrollon nese ekzistojne ose kodet e klient/furnitoreve te vendosura ne trupin e veprimeve.
            var kodetKf = hfKod.Value;
            char[] delimiter = { '|', '|' };
            var kodetRreshtat = kodetKf.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
            int rreshta = kodetRreshtat.Length;
            var kodetJoEkzistuese = new string[rreshta];
            if (kodetKf != "")
            {
                int j = 0;
                var kaGabim = false;
                for (int i = 0; i < kodetRreshtat.Length; i++)
                {
                    string[] pars4 = kodetRreshtat[i].Split(':');
                    string kodi = pars4[1];
                    if (kodi != "")
                    {
                        if (!clsKlientFurnitor.EkzistonKlientFurnitor(kodi, IdNdermarrja))
                        {
                            kaGabim = true;
                            kodetJoEkzistuese[j++] = kodi;
                        }
                        else
                        {
                            if (Request.QueryString["vep"] == "azhornim")
                            {
                                var kf = new clsKlientFurnitor();
                                kf.mbushKlientFurnitorSipasKoditAzhornim(kodi, IdNdermarrja, Data_DateEdit.Date);
                                if (kf.DtAzhornimi > Data_DateEdit.Date || kf.DtLidhje > Data_DateEdit.Date)
                                {
                                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgAzhornimPasDtDok"], pnlMesazhi);
                                    return false;
                                }
                            }
                        }
                    }
                }
                if (kaGabim)
                {
                    var kodetStr = string.Join(", ", kodetJoEkzistuese, 0, j);
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNukEkzistonkfMeKod", ci) + kodetStr + "!", pnlMesazhi);
                    return false;
                }
            }

            return true;
        }

        #region Grida Klient/Furnitor

        /// <summary>
        /// mbush griden e popupit me te dhena  
        /// </summary>
        private void MbushListeKlientFurnitoresh()
        {
            var col = new colKlienteFurnitore();
            var mon = new clsMonedha();
            mon.mbushMonedhenENdermarrjes(IdNdermarrja);

            if (Request.QueryString["vep"] == "azhornim")

                col.MbushKlienteFurnitoreNdermarrjesAndAutorizimeMonHuajMeGjendje(IdNdermarrja, IdPerdoruesi, mon.IdMonedha, Data_DateEdit.Date);
            else
                col.MbushKlienteFurnitoreNdermarrjesAndAutorizimeGjendjeMonLlogDheMonBaze(IdNdermarrja, IdPerdoruesi, Data_DateEdit.Date);

            gvKF.DataSource = col;
            gvKF.DataBind();

            mySessionObjects.ruajColKFZgjedhurNeSesion(Session, new colKlienteFurnitore());
            mySessionObjects.ruajColKFNeSesion(Session, col);
        }

        private void KonfiguroGride()
        {
            KonfigurimComboGride.ShtoLlojiKlientFurnitor(gvKF, rm, ci, "LlojiKF");
            GridUtil.percaktoVisibleColumnsMeWidth(IdGjuha, IdNdermarrja, gvKF, "gvKF", clsFunksione.GetKomponente(Page.Request));
            var col3 = gvKF.Columns["Gjendja"] as GridViewDataTextColumn;
            col3.PropertiesEdit.DisplayFormatString = "0.00";
            var col4 = gvKF.Columns["GjendjaMonBaze"] as GridViewDataTextColumn;
            col4.PropertiesEdit.DisplayFormatString = "0.00";
        }

        protected void gvKF_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {

        }

        protected void gvKF_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            switch (e.Parameters.Split(':')[0])
            {
                case "djathtas":
                    btnDjathtas1_Click();
                    break;
                case "djathtasgjithe":
                    btnDjathtasGjitha_Click();
                    break;
                case "majtas":
                    btnMajtas1_Click(e.Parameters.Split(':')[1]);
                    break;
                case "hiq":
                    btnhiq1_Click(e.Parameters.Split(':')[1]);
                    break;
                case "kalotegjithe":
                    btnMajtaGjitha_Click();
                    break;
                case "pastro":
                    gvKF.Selection.UnselectAll();
                    MbushListeKlientFurnitoresh();
                    break;
                case "data":
                    gvKF.Selection.UnselectAll();
                    MbushListeKlientFurnitoreshSipasDates();
                    break;
            }

            gvKF.DataSource = mySessionObjects.merrColKFNgaSesioni(Session);
            gvKF.DataBind();
        }

        private void MbushListeKlientFurnitoreshSipasDates()
        {
            var colDb = new colKlienteFurnitore();//koleksioni i kf qe kthen databaza
            var colRi = new colKlienteFurnitore();//koleksioni ku do vendosen kf qe do shfaqen te grida
            var mon = new clsMonedha();
            mon.mbushMonedhenENdermarrjes(IdNdermarrja);

            if (Request.QueryString["vep"] == "azhornim")
                colDb.MbushKlienteFurnitoreNdermarrjesAndAutorizimeMonHuajMeGjendje(IdNdermarrja, IdPerdoruesi, mon.IdMonedha, Data_DateEdit.Date);
            else
                colDb.MbushKlienteFurnitoreNdermarrjesAndAutorizimeGjendjeMonLlogDheMonBaze(IdNdermarrja, IdPerdoruesi, Data_DateEdit.Date);

            var colKfZgjedhur = mySessionObjects.merrColKFZgjedhurNgaSesioni(Session);
            colRi.AddRange(colDb
                .Select(kf => new { kf, ar = colKfZgjedhur.Where(l => l.IdKlientFurnitor == kf.IdKlientFurnitor) })
                .Where(t => !t.ar.Any())
                .Select(t => t.kf));

            mySessionObjects.ruajColKFNeSesion(Session, colRi);
        }

        protected void gvKF_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpNoRows"] = gvKF.VisibleRowCount;
            e.Properties["cpNoPage"] = gvKF.PageIndex;
        }

        protected void gvKF_DataBound(object sender, EventArgs e)
        {
            if (gvKF.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per 
                //perzgjidh
                var check = new GridViewCommandColumn("#")
                {
                    ShowSelectCheckbox = true,
                    Width = Unit.Percentage(2)
                };
                check.SetColVisibleIndex(0);
                //behet per te afishuar rreshtin qe do sherbej per filtrim
                gvKF.Settings.ShowFilterRow = true;
                gvKF.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                gvKF.Settings.ShowFilterRowMenu = true;
                gvKF.Columns.Add(check);

                gvKF.KeyFieldName = "IdKlientFurnitor";
                gvKF.SettingsBehavior.AllowSelectByRowClick = true;
                gvKF.SettingsBehavior.AllowFocusedRow = true;

            }

            gvKF.Columns["Prospekt"].Visible = false;
            gvKF.Columns["KodiMobile"].Visible = false;
            gvKF.Columns["#"].VisibleIndex = 0;
        }

        /// <summary>
        /// funksioni qe thirret kur selektohet nje klient/furnitor nga lista per t'u azhornuar 
        /// dhe shtypet butoni "v" per ta cuar te jqgrida.
        /// </summary>
        protected void btnDjathtas1_Click()
        {
            var col = mySessionObjects.merrColKFNgaSesioni(Session);
            var colKfZgjedhur = mySessionObjects.merrColKFZgjedhurNgaSesioni(Session);//ruan ne sesion kl/furn e zgjedhur dhe qe kalohen te grida poshte
            var id = gvKF.GetSelectedFieldValues("IdKlientFurnitor");

            foreach (var i in id)
            {
                var kfTmp = new clsKlientFurnitor();
                kfTmp.mbushKlientFurnitorSipasKoditAzhornim(Convert.ToInt32(i), Data_DateEdit.Date);
                if ((Request.QueryString["vep"] == "azhornim" && kfTmp.DtAzhornimi < Data_DateEdit.Date && kfTmp.DtLidhje < Data_DateEdit.Date) || Request.QueryString["vep"] == "mbyllje")
                {
                    var ar = col.Where(l => l.IdKlientFurnitor == Convert.ToInt32(i)).ToList();
                    if (ar.Any())
                    {
                        colKfZgjedhur.Add(ar.First());
                        col.Remove(ar.First());
                        colKfZgjedhur.Add(kfTmp);
                    }
                }
            }

            mySessionObjects.ruajColKFNeSesion(Session, col);
            mySessionObjects.ruajColKFZgjedhurNeSesion(Session, colKfZgjedhur);
            gvKF.Selection.UnselectAll();
            gvKF.DataSource = col;
            gvKF.DataBind();
        }

        protected void btnhiq1_Click(string kodkf)
        {
            var col = mySessionObjects.merrColKFNgaSesioni(Session);
            var art = new clsKlientFurnitor();
            art.mbushKlientFurnitorSipasKoditAzhornim(kodkf, mySessionObjects.merrIdNdermarrjeSesioni(Session), Data_DateEdit.Date);

            if ((Request.QueryString["vep"] == "azhornim" && art.DtAzhornimi < Data_DateEdit.Date && art.DtLidhje < Data_DateEdit.Date) || Request.QueryString["vep"] == "mbyllje")
            {
                var ar = col.Where(l => l.IdKlientFurnitor == art.IdKlientFurnitor).ToList();
                if (ar.Any())
                    col.Remove(ar.First());
            }

            mySessionObjects.ruajColKFNeSesion(Session, col);
            gvKF.Selection.UnselectAll();
            gvKF.DataSource = col;
            gvKF.DataBind();
        }

        /// <summary>
        /// funksioni qe i selekton te gjitha rreshtat e grides se kl/furn dhe i kalon te grida poshte
        /// </summary>
        protected void btnDjathtasGjitha_Click()
        {
            var col = mySessionObjects.merrColKFNgaSesioni(Session);
            var colKfZgjedhur = new colKlienteFurnitore(); //mySessionObjects.merrColKFZgjedhurNgaSesioni(Session);
            var colnew = new colKlienteFurnitore();

            foreach (var k in col)
            {
                if (k.DtAzhornimi >= Data_DateEdit.Date || k.DtLidhje >= Data_DateEdit.Date)
                    colnew.Add(k);
                else
                    colKfZgjedhur.Add(k);
            }

            var tezgjedhur = colKfZgjedhur.Select(x => new
            {
                x.KodKlientFurnitor,
                x.NrLlogKlientFurnitor,
                x.EmertimiKF,
                x.LlojiKF,
                x.IdKlientFurnitor,
                x.Monedha,
                x.DtAzhornimi,
                x.DtLidhje,
                x.IdLlogari

            });

            gvKF.ShtoObjectNeGride(tezgjedhur, "cpKlientTeZgjedhur");
            mySessionObjects.ruajColKFNeSesion(Session, colnew);
            mySessionObjects.ruajColKFZgjedhurNeSesion(Session, colKfZgjedhur);
            gvKF.Selection.UnselectAll();
            gvKF.DataSource = colnew;
            gvKF.DataBind();
        }

        protected void btnMajtas1_Click(string kodkf)
        {
            var col = mySessionObjects.merrColKFNgaSesioni(Session);
            var colKfZgjedhur = mySessionObjects.merrColKFZgjedhurNgaSesioni(Session);

            var art = new clsKlientFurnitor();
            art.mbushKlientFurnitorSipasKoditAzhornim(kodkf, IdNdermarrja, Data_DateEdit.Date);
            col.Add(art);
            colKfZgjedhur.Remove(art);

            mySessionObjects.ruajColKFNeSesion(Session, col);
            gvKF.Selection.UnselectAll();
            gvKF.DataSource = col;
            gvKF.DataBind();
        }

        protected void btnMajtaGjitha_Click()
        {
            var col = new colKlienteFurnitore();
            var mon = new clsMonedha();
            mon.mbushMonedhenENdermarrjes(IdNdermarrja);
            if (Request.QueryString["vep"] == "azhornim")
                col.MbushKlienteFurnitoreNdermarrjesAndAutorizimeMonHuajMeGjendje(IdNdermarrja, IdPerdoruesi, mon.IdMonedha, Data_DateEdit.Date);
            else
                col.MbushKlienteFurnitoreNdermarrjesAndAutorizimeGjendjeMonLlogDheMonBaze(IdNdermarrja, IdPerdoruesi, Data_DateEdit.Date);

            mySessionObjects.ruajColKFZgjedhurNeSesion(Session, new colKlienteFurnitore());
            mySessionObjects.ruajColKFNeSesion(Session, col);
            gvKF.Selection.UnselectAll();
            gvKF.DataSource = col;
            gvKF.DataBind();
        }

        protected void gvKF_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            var colnew = ((ASPxGridView)sender).Columns["DtLidhje"] as GridViewDataDateColumn;
            var colnewaz = ((ASPxGridView)sender).Columns["DtAzhornimi"] as GridViewDataDateColumn;
            var col = mySessionObjects.merrColKFNgaSesioni(Session);

            if (col != null && e.VisibleIndex + 1 >= 0 && e.VisibleIndex + 1 < col.Count)
            {
                if(colnewaz != null)
                colnewaz.DataItemTemplate = col[e.VisibleIndex + 1].DtAzhornimi > Data_DateEdit.Date ? new MyDateGTemplate("Red") : new MyDateGTemplate("Green");
                if(colnew != null)
                    colnew.DataItemTemplate = col[e.VisibleIndex + 1].DtLidhje > Data_DateEdit.Date ? new MyDateGTemplate("Red") : new MyDateGTemplate("Green");
            }
        }

        #endregion

        /// <summary>
        /// Metode qe sherben per te kaluar perkthime te js
        /// </summary>
        private void MbushHiddenFieldMePerkthime()
        {
            hfState.Set("msgGabimGjateTransferimitTeTeDhenave", MessagesResource.Messages["msgGabimGjateTransferimitTeTeDhenave"]);
            hfState.Set("msgLlogNukEkziston", MessagesResource.Messages["msgLlogNukEkziston"]);
            hfState.Set("msgEkzistonkfGride", MessagesResource.Messages["msgEkzistonkfGride"]);
            hfState.Set("msgJoVlefteZero", MessagesResource.Messages["msgJoVlefteZero"]);
            hfState.Set("msgNukZgjidhenkfMonBaze", MessagesResource.Messages["msgNukZgjidhenkfMonBaze"]);
            hfState.Set("msgKlientFurnitoriNukEshteAktiv", MessagesResource.Messages["msgKlientFurnitoriNukEshteAktiv"]);
            hfState.Set("msgAzhornimPasDtDok", MessagesResource.Messages["msgAzhornimPasDtDok"]);
            hfState.Set("popupAdministrimiUniversal", MessagesResource.Messages["popupAdministrimiUniversal"]);
            hfState.Set("msgZgjidhKF", MessagesResource.Messages["msgZgjidhKF"]);
            hfState.Set("msgVendosniDateEDokumentit", MessagesResource.Messages["msgVendosniDateEDokumentit"]);
            hfState.Set("msgShperndarjeNeQendratEKostos", MessagesResource.Messages["msgShperndarjeNeQendratEKostos"]);
            hfState.Set("msgDeshironiShperndarjeQendraKosto", MessagesResource.Messages["msgDeshironiTeBeniShperndarjenNeQendratEKostos"]);
            hfState.Set("msgShenoniNumrinEDokumentit", MessagesResource.Messages["msgShenoniNumrinEDokumentit"]);
            hfState.Set("msgShenoniLlogDebi", MessagesResource.Messages["msgShenoniLlogDebi"]);
            hfState.Set("msgShenoniLlogKredi", MessagesResource.Messages["msgShenoniLlogKredi"]);
            hfState.Set("msgZgjidhniNjeDateDokumenti", MessagesResource.Messages["msgZgjidhniNjeDateDokumenti"]);
            hfState.Set("msgZgjidhniNjeDateRegjstrimi", MessagesResource.Messages["msgZgjidhniNjeDateRegjstrimi"]);
            hfState.Set("msgPrisniPlotesohenTeDhenat", MessagesResource.Messages["msgPrisniPlotesohenTeDhenat"]);
            hfState.Set("msgTrupiDokumentitNukDuhetLeneBosh", MessagesResource.Messages["msgTrupiDokumentitNukDuhetLeneBosh"]);
            hfState.Set("msgRreshtaGridePaLLogKunderparti", MessagesResource.Messages["msgRreshtaGridePaLLogKunderparti"]);
            hfState.Set("msgPerkfMekod", MessagesResource.Messages["msgPerkfMekod"]);
            hfState.Set("msgJoFitimHumbjeAzhornim", MessagesResource.Messages["msgJoFitimHumbjeAzhornim"]);
            hfState.Set("msgDoniTeBeniAzhornimkf", MessagesResource.Messages["msgDoniTeBeniAzhornimkf"]);
            hfState.Set("msgZgjidhDokumentin", MessagesResource.Messages["msgZgjidhDokumentin"]);
            hfState.Set("headerZgjidhKlientFurnitorin", MessagesResource.Messages["headerZgjidhKlientFurnitorin"]);
        }

        protected void llogariDebi_ButtonEdit_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (!IsCallback) return;
            if (Request.Params["__CALLBACKID"].Contains("llogariDebi_ButtonEdit"))
                ConfigureAspxComboBox.KonfiguroComboBoxLlogaria(llogariDebi_ButtonEdit, IdPerdoruesi, IdNdermarrja, e);
        }

        protected void llogariKredi_ButtonEdit_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (!IsCallback) return;
            if (Request.Params["__CALLBACKID"].Contains("llogariKredi_ButtonEdit"))
                ConfigureAspxComboBox.KonfiguroComboBoxLlogaria(llogariKredi_ButtonEdit, IdPerdoruesi, IdNdermarrja, e);
        }
        
        protected void llogariDebi_ButtonEdit_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (!IsCallback) return;
            if (Request.Params["__CALLBACKID"].Contains("llogariDebi_ButtonEdit"))
                ConfigureAspxComboBox.KonfiguroComboBoxLlogaria(llogariDebi_ButtonEdit, IdPerdoruesi, IdNdermarrja, e);
        }

        protected void llogariKredi_ButtonEdit_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (!IsCallback) return;
            if (Request.Params["__CALLBACKID"].Contains("llogariKredi_ButtonEdit"))
                ConfigureAspxComboBox.KonfiguroComboBoxLlogaria(llogariKredi_ButtonEdit, IdPerdoruesi, IdNdermarrja, e);
        }
    }
}