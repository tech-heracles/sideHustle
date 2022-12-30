using System;
using DbCore;
using DbCore.DbAdmin;
using DbCore.DbKontabiliteti;
using DbCore.IMBUtils;
using DbCore.IMBUtils.Cache;
using DbCore.IMBUtils.Extensions;
using DbCore.IMBUtils.Messages;
using DbCore.IMBUtils.Types;
using DbCore.IMBUtils.Validation;
using DevExpress.Data;
using DevExpress.Web;
using DevExpress.Web.Data;
using PlatinumWeb.ApplicationUtils.Pages;
using PlatinumWeb.Templates;

namespace PlatinumWeb
{
    public partial class GrupimeKlientFurnitor : MyPageBase
    {
        private const string mesazhPlotesoniKodin = "Ju lutemi plotesoni kodin!";
        private const string mesazhPlotesoniPershkrimin = "Ju lutemi plotesoni pershkrimin!";
        private const string mesazhKodiEkziston = "Ekziston nje grup me kete kod. Ju lutem zgjidhni nje kod tjeter!";
        private const string mesazhGrupiKaVeprim = "Ky grup eshte perdorur ne veprime dhe nuk mund te detajohet. Ju lutem zgjidhni nje grup tjeter per prind!";
        private const string mesazhPrindiGrupTjeter = "Nuk lejohet qe prindi te beje pjese ne nje grupim tjeter. Ju lutem zgjidhni nje prind tjeter!";
        private const string mesazhPrindiJoVlefshem = "Prindi qe keni zgjedhur nuk eshte i vlefshem. Ju lutemi zgjidhni nje prind tjeter!";
        private const string mesazhGrupiVeprimFshirje = "Ky grup ka veprime dhe nuk mund te fshihet!";
        private const string mesazhGabimGjateRuajtjes = "Ndodhi nje gabim gjate ruajtjes!";
        private string mesazhRuajtjaPerfundoiSukses = MessagesResource.Messages["mesazhRuajtjeMeSukses"];
        private string mesazhFshirjaPerfundoiSukses = MessagesResource.Messages["msgFshirjeMeSukses"];

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!mySessionObjects.isLogedIn(Session))
            {
                Response.Redirect(Paths.defaultLoginPath);
                return;
            }

            if (mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + IdPerdoruesi);
                return;
            }

            PercaktoTemplateMenu();

            clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, IdNdermarrja, "gvKodifikimKlientFurnitor", 1, "GrupimeKlientFurnitor.aspx");
            if (!IsPostBack)
            {
                EmrateTabeve();
                mySessionObjects.ruajMesazhNeSesion(Session, ":");
                if (Request.QueryString["ruaj"] == "ok")
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["mesazhRuajtjeMeSukses"], pnlMesazhi);

                var tedrejtaInfo = new clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, clsFunksione.GetKomponente(Page.Request));
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
            }

            KonfiguroVleraFillestare();
            KonfiguroGridat();

            mySessionObjects.ruajMesazhNeSesion(Session, ":");
        }

        /// <summary>
        /// Vendos emrat e tabeve ne baze te gjuhes se perdoruesit
        /// </summary>
        private void EmrateTabeve()
        {
            ASPxPageControl1.TabPages[0].Text = MessagesResource.Messages["labelGrupimKlientPare"];
            ASPxPageControl1.TabPages[1].Text = MessagesResource.Messages["labelGrupimKlientDyte"];
            ASPxPageControl1.TabPages[2].Text = MessagesResource.Messages["filterGrupimi3"];
        }

        private void KonfiguroVleraFillestare()
        {
            KonfigVleraFillestarePerGride(gvKodifikimKlientFurnitor);
            KonfigVleraFillestarePerGride(gvKodifikimKlientFurnitorGrupim2);
            KonfigVleraFillestarePerGride(gvKodifikimKlientFurnitorGrupim3);
        }

        private void KonfigVleraFillestarePerGride(ASPxGridView grida)
        {
            var klientapofurnitor = Request.QueryString["kf"] == "klient" ? 0 : 1;
            int grupikf;
            switch (grida.ClientInstanceName)
            {
                case "gvKodifikimKlientFurnitor":
                    grupikf = 1;
                    break;
                case "gvKodifikimKlientFurnitorGrupim2":
                    grupikf = 2;
                    break;
                default:
                    grupikf = 3;
                    break;
            }

            var col = new colGrupeKF();
            col.MerrGrupeKfSipasLlojKodifikimiDheLlojKf(grupikf, IdNdermarrja, klientapofurnitor);
            grida.DataSource = col;
            grida.DataBind();
        }

        private void KonfiguroGridat()
        {
            KonfiguroGride(gvKodifikimKlientFurnitor);
            KonfiguroGride(gvKodifikimKlientFurnitorGrupim2);
            KonfiguroGride(gvKodifikimKlientFurnitorGrupim3);
        }

        private void KonfiguroGride(ASPxGridView grida)
        {
            ShtoPrind(grida);
            if (!IsPostBack)
                GridUtil.percaktoVisibleColumnsMeWidth(IdGjuha, IdNdermarrja, grida, "gvKodifikimKlientFurnitor", "GrupimeKlientFurnitor.aspx");

            GridUtil.konfiguroGrideListeEvogelPaTheme(grida, "IdGrupi");
        }

        private void ShtoGrupNeSession(clsGrupeKF grupi, ASPxGridView grida)
        {
            var sessionKey = SessionKeyUtils.MerrSessionKeyPerCmb(Request.Url.AbsoluteUri, "IdPrindi");
            DbCore.DbKontabiliteti.colGrupeKF grupet = mySessionObjects.MerrNgaSession<colGrupeKF>(Session, sessionKey, grida.ID);
            if (grupet.Find(x => x.IdGrupi == grupi.IdGrupi) != null)
                return;
            if (grupi.IdGrupi <= 0)
                return;
            grupet.Add(grupi);
            mySessionObjects.RuajNeSession(sessionKey, grida.ID, grupet);
        }

        private void ShtoPrind(ASPxGridView grida)
        {
            var klientapofurnitor = Request.QueryString["kf"] == "klient" ? 0 : 1;
            grida.KonfiguroCombo("IdPrindi", "IdGrupi", "PershkrimGrupi", () =>
            {
                var grupet = new colGrupeKF
                {
                    new clsGrupeKF()
                };

                if (grida == gvKodifikimKlientFurnitor)
                    grupet.MerrGrupeKfSipasLlojKodifikimiDheLlojKf(1, IdNdermarrja, klientapofurnitor);
                else if (grida == gvKodifikimKlientFurnitorGrupim2)
                    grupet.MerrGrupeKfSipasLlojKodifikimiDheLlojKf(2, IdNdermarrja, klientapofurnitor);
                else if (grida == gvKodifikimKlientFurnitorGrupim3)
                    grupet.MerrGrupeKfSipasLlojKodifikimiDheLlojKf(3, IdNdermarrja, klientapofurnitor);

                return grupet;
            }, Session, Request.Url.AbsoluteUri, grida.ID);

            PercaktoTamplatePrindi(grida, klientapofurnitor);
        }

        private void PercaktoTamplatePrindi(ASPxGridView grida, int llojikf)
        {
            var col7 = grida.Columns["IdPrindi"] as GridViewDataComboBoxColumn;

            switch (grida.ClientInstanceName)
            {
                case "gvKodifikimKlientFurnitor":
                    col7.EditItemTemplate = new MyTemplatePrindiGrupKF(IdNdermarrja, 1, llojikf);
                    break;
                case "gvKodifikimKlientFurnitorGrupim2":
                    col7.EditItemTemplate = new MyTemplatePrindiGrupKF(IdNdermarrja, 2, llojikf);
                    break;
                default:
                    col7.EditItemTemplate = new MyTemplatePrindiGrupKF(IdNdermarrja, 3, llojikf);
                    break;
            }

            col7.ReadOnly = false;
        }

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        private void PercaktoTemplateMenu()
        {
            clsToolbarConfig.percaktoTemplateMenu(IdGjuha, IdViti, IdPerdoruesi, IdNdermarrja, ASPxMenu1, clsFunksione.GetKomponente(Page.Request), this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, hfRuaj.Value == "Ruaj", true, false, Meme);
        }

        protected bool KontrolloPrind(int niveli, int idKodifikimi, int idPrindi)
        {
            if (niveli > 1)
            {
                if (idKodifikimi == idPrindi) return false;

                niveli--;
                idPrindi = new clsGrupeKF(idPrindi).IdPrindi;
                return KontrolloPrind(niveli, idKodifikimi, idPrindi);//si idKodifikimi do i kalohet idPrindi fillestar; si idPrindi do i kalohet idPrindi e kodifikimit me idPrindi fillestar
            }

            return idKodifikimi != idPrindi;
        }

        protected void NdryshoNivelBijte(int niveli, int idKodifikimi)
        {
            var colGrupeKf = new colGrupeKF();
            colGrupeKf.KtheBijte(idKodifikimi, IdNdermarrja);
            foreach (var grupeKf in colGrupeKf)
            {
                idKodifikimi = grupeKf.IdGrupi;
                var kodif = new clsGrupeKF(idKodifikimi) { NivelGrupi = niveli + 1 };
                kodif.Modifiko();
                NdryshoNivelBijte(kodif.NivelGrupi, idKodifikimi);
            }
        }

        #region Events

        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            PercaktoTemplateMenu();
        }

        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            //per t'u rregulluar sipas kodifikimit te kf
            ASPxGridView grida;
            switch (ASPxPageControl1.ActiveTabIndex)
            {
                case 0:
                    grida = gvKodifikimKlientFurnitor;
                    break;
                case 1:
                    grida = gvKodifikimKlientFurnitorGrupim2;
                    break;
                default:
                    grida = gvKodifikimKlientFurnitorGrupim3;
                    break;
            }

            grida.Selection.SelectRow(grida.FocusedRowIndex);
            var rreshtat = grida.GetSelectedFieldValues("IdGrupi");

            foreach (int id in rreshtat)
            {
                var grup = new clsGrupeKF(id);
                var eshtePrind = clsGrupeKF.EshtePrind(grup.IdGrupi, IdNdermarrja);

                if (clsGrupeKF.KaVeprimeGrupKf(grup.IdGrupi) || eshtePrind)
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhGrupiVeprimFshirje, pnlMesazhi);
                else
                {
                    grup.IdPerdoruesi = IdPerdoruesi;
                    grup.Fshi(); clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazhFshirjaPerfundoiSukses, pnlMesazhi);
                }

                KonfigVleraFillestarePerGride(grida);
            }
            pnlGrida.Update();
        }

        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje
            ASPxGridView grida;
            switch (ASPxPageControl1.ActiveTabIndex)
            {
                case 0:
                    grida = gvKodifikimKlientFurnitor;
                    break;
                case 1:
                    grida = gvKodifikimKlientFurnitorGrupim2;
                    break;
                default:
                    grida = gvKodifikimKlientFurnitorGrupim3;
                    break;
            }
            
            var cmbFiltra = ((MenuFilter)ASPxMenu1.Items.FindByName("TemplatedItemFilter").Template).FindControl("btnFiltra") as ASPxComboBox;

            var filtri = new clsFiltraGrida
            {
                FiltraKodi = cmbFiltra.Text,
                FiltraShenime = cmbFiltra.Text,
                FiltraUniversal = false
            };

            var koka = new clsGridaKoka(IdGjuha, "gvKodifikimKlientFurnitor", "GrupimeKlientFurnitor.aspx", IdNdermarrja);
            filtri.GridaKokaId = koka.IdGridaKoka;

            filtri.FiltraVlera = grida.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("KodGrupi", grida);
            //var kolona = grida.GetSortedColumns();
            //if (kolona.Count > 0)
            //{
            //    filtri.KoloneRenditje = kolona[0].FieldName;
            //    filtri.DrejtimRenditje = kolona[0].SortOrder == ColumnSortOrder.Ascending;
            //}
            //else
            //{
            //    filtri.KoloneRenditje = "KodGrupi";
            //    filtri.DrejtimRenditje = true;
            //}

            filtri.IdPerdoruesi = IdPerdoruesi;
            filtri.IdNdermarje = IdNdermarrja;
            filtri.IdStatusDok = 1;

            var mesazh = filtri.ruaj();
            clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, IdNdermarrja, "gvKodifikimKlientFurnitor", 1, "GrupimeKlientFurnitor.aspx");
            PercaktoTemplateMenu();

            if (mesazh.Status)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            cmbFiltra.Text = "";
        }

        public void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje
            ASPxGridView grida;
            switch (ASPxPageControl1.ActiveTabIndex)
            {
                case 0:
                    grida = gvKodifikimKlientFurnitor;
                    break;
                case 1:
                    grida = gvKodifikimKlientFurnitorGrupim2;
                    break;
                default:
                    grida = gvKodifikimKlientFurnitorGrupim3;
                    break;
            }

            var cmbFiltra = ((MenuFilter)(ASPxMenu1.Items.FindByName("TemplatedItemFilter").Template)).FindControl("btnFiltra") as ASPxComboBox;
            var filtra = new clsFiltraGrida();
            var koka = new clsGridaKoka(IdGjuha, "gvKodifikimKlientFurnitor", "GrupimeKlientFurnitor.aspx", IdNdermarrja);
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, IdNdermarrja, koka.IdGridaKoka);

            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = IdPerdoruesi;
                var mesazh = filtra.fshi();

                clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, IdNdermarrja, "gvKodifikimKlientFurnitor", 1, "GrupimeKlientFurnitor.aspx");
                PercaktoTemplateMenu();

                if (mesazh.Status)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                grida.FilterExpression = string.Empty;
            }
        }

        protected void rowInserting(object sender, ASPxDataInsertingEventArgs e)
        {
            var tedrejtaInfo = new clsTeDrejtaRoli();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, clsFunksione.GetKomponente(Page.Request));
            if (!tedrejtaInfo.DShtim)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                mySessionObjects.ruajMesazhNeSesion(Session, "Ju nuk keni te drejta per kete veprim!:Red");
                return;
            }
            var grup = new clsGrupeKF
            {
                KodGrupi = e.NewValues["KodGrupi"].ToString().RemoveSpaces(),
                PershkrimGrupi = e.NewValues["PershkrimGrupi"].ToString().RemoveSpaces()
            };

            var gride = sender as ASPxGridView;
            switch (gride.ClientInstanceName)
            {
                case "gvKodifikimKlientFurnitor":
                    grup.LlojKodifikimi = 1;
                    break;
                case "gvKodifikimKlientFurnitorGrupim2":
                    grup.LlojKodifikimi = 2;
                    break;
                case "gvKodifikimKlientFurnitorGrupim3":
                    grup.LlojKodifikimi = 3;
                    break;
            }

            grup.LlojKF = Request.QueryString["kf"] == "klient" ? 0 : 1;
            grup.IdPrindi = hfPrindi.Value != "" ? new clsGrupeKF(hfPrindi.Value, IdNdermarrja, grup.LlojKodifikimi, grup.LlojKF).IdGrupi : 0;
            grup.NivelGrupi = int.Parse(hfNiveli.Value);
            grup.IdNdermarje = IdNdermarrja;
            grup.IdPerdoruesi = IdPerdoruesi;
            grup.IdStatusDok = 1;

            e.Cancel = true;
            gride.CancelEdit();

            if (clsGrupeKF.EkzistonGrupKfSipasKodLloje(grup.KodGrupi, IdNdermarrja, grup.LlojKodifikimi, grup.LlojKF))
            {
                mySessionObjects.ruajMesazhNeSesion(Session, mesazhKodiEkziston + ":Red");
                KonfiguroGride(gride);
                gride.AddNewRow();
                return;
            }

            var mesazh = grup.Ruaj();
            if (!mesazh.Status)
                mySessionObjects.ruajMesazhNeSesion(Session, mesazhGabimGjateRuajtjes + ":Red");
            else
                mySessionObjects.ruajMesazhNeSesion(Session, mesazhRuajtjaPerfundoiSukses + ":Green");

            pnlGrida.Update();

            KonfigVleraFillestarePerGride(gride);
            ShtoGrupNeSession(grup, gride);
            KonfiguroGride(gride);
        }

        protected void rowUpdating(object sender, ASPxDataUpdatingEventArgs e)
        {
            var tedrejtaInfo = new clsTeDrejtaRoli();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, clsFunksione.GetKomponente(Page.Request));
            if (!tedrejtaInfo.DMod)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                mySessionObjects.ruajMesazhNeSesion(Session, "Ju nuk keni te drejta per kete veprim!:Red");
                return;
            }

            var idGrupi = int.Parse(e.Keys["IdGrupi"].ToString());

            var gride = sender as ASPxGridView;
            int llojkodifikimi;
            switch (gride.ClientInstanceName)
            {
                case "gvKodifikimKlientFurnitor":
                    llojkodifikimi = 1;
                    break;
                case "gvKodifikimKlientFurnitorGrupim2":
                    llojkodifikimi = 2;
                    break;
                default:
                    llojkodifikimi = 3;
                    break;
            }

            var llojiKf = Request.QueryString["kf"] == "klient" ? 0 : 1;
            var niveli = int.Parse(e.NewValues["NivelGrupi"].ToString());

            var prindiRi = new clsGrupeKF(hfPrindi.Value, IdNdermarrja, llojkodifikimi, llojiKf);//marrim prindin e ri qe duam t'i vendosim kodifikimit

            e.Cancel = true;

            var mesazh = new clsMesazh();

            var joCikel = KontrolloPrind(prindiRi.NivelGrupi, idGrupi, prindiRi.IdGrupi); //jocikel = true kur nuk formohen cikle dhe mund te vazhdoje modifikimi me tej; = false kur krijohen cikle, atehere modifikimi nderpritet
            if (!joCikel)
            {
                gride.CancelEdit();
                mesazh.Status = false;
                mesazh.PershkrimMesazhi = mesazhPrindiJoVlefshem;
                return;
            }

            if (new clsGrupeKF(idGrupi).NivelGrupi != niveli)
            {
                if (clsGrupeKF.EshtePrind(idGrupi, IdNdermarrja))
                    NdryshoNivelBijte(niveli, idGrupi); //i kalojme si parametra nivelin e ri qe do kete dhe id e elementit qe do spostojme
            }

            var kodifikim = new clsGrupeKF(idGrupi, e.NewValues["KodGrupi"].ToString().RemoveSpaces(), e.NewValues["PershkrimGrupi"].ToString().RemoveSpaces(), prindiRi.IdGrupi, niveli, IdPerdoruesi, IdNdermarrja, 1, llojkodifikimi, llojiKf);
            mesazh = kodifikim.Modifiko();
            if (mesazh.Status)
                mySessionObjects.ruajMesazhNeSesion(Session, mesazh.PershkrimMesazhi + ":Green");

            else
                mySessionObjects.ruajMesazhNeSesion(Session, mesazh.PershkrimMesazhi + ":Red");

            gride.CancelEdit();
            KonfigVleraFillestarePerGride(gride);
            KonfiguroGridat();
            hfNiveli.Value = "";
            hfPrindi.Value = "";
        }

        /// <summary>
        /// Validimi ne jane plotesuar gjithe fushat e detyruara
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rowValidating(object sender, ASPxDataValidationEventArgs e)
        {
            if (e.NewValues["KodGrupi"] == null || e.NewValues["KodGrupi"].ToString() == "")
            {
                e.RowError = mesazhPlotesoniKodin;
                return;
            }

            var kontrollKodi = clsFunksione.kontrolloKaraktereMeMesazh(clsFunksione.ktheStringunPaHapesira(e.NewValues["KodGrupi"].ToString(), true), FusheKontrolli.Kodi, false);
            if (!kontrollKodi.Status)
            {
                e.RowError = kontrollKodi.PershkrimMesazhi;
                return;
            }

            if (e.NewValues["PershkrimGrupi"] == null || e.NewValues["PershkrimGrupi"].ToString() == "")
            {
                e.RowError = mesazhPlotesoniPershkrimin;
                return;
            }

            var kontrollPershkrimi = clsFunksione.kontrolloKaraktereMeMesazh(clsFunksione.ktheStringunPaHapesira(e.NewValues["PershkrimGrupi"].ToString(), false), FusheKontrolli.Pershkrimi, true);
            if (!kontrollPershkrimi.Status)
            {
                e.RowError = kontrollPershkrimi.PershkrimMesazhi;
                return;
            }

            var grida = sender as ASPxGridView;

            int llojKod;
            switch (grida.ClientInstanceName)
            {
                case "gvKodifikimKlientFurnitor":
                    llojKod = 1;
                    break;
                case "gvKodifikimKlientFurnitorGrupim2":
                    llojKod = 2;
                    break;
                case "gvKodifikimKlientFurnitorGrupim3":
                    llojKod = 3;
                    break;
                default:
                    llojKod = 0;
                    break;
            }

            var llojkf = Request.QueryString["kf"] == "klient" ? 0 : 1;

            if (e.Keys["IdGrupi"] == null) //nqs eshte shtim
                if (clsGrupeKF.EkzistonGrupKfSipasKodLloje(e.NewValues["KodGrupi"].ToString(), IdNdermarrja, llojKod, llojkf))
                {
                    e.RowError = mesazhKodiEkziston;
                    return;
                }

            if (hfPrindi.Value != "")
            {
                var prindiRi = new clsGrupeKF(hfPrindi.Value, IdNdermarrja, llojKod, llojkf);
                if (clsGrupeKF.KaVeprimeGrupKf(prindiRi.IdGrupi))
                {
                    e.RowError = mesazhGrupiKaVeprim;
                    return;
                }

                if (llojKod != prindiRi.LlojKodifikimi)
                {
                    e.RowError = mesazhPrindiGrupTjeter;
                }
            }
        }

        protected void startRowEditing(object sender, ASPxStartRowEditingEventArgs e)
        {//kur fillon editimi te behet validimi
            //ASPxGridView grida = sender as ASPxGridView;
            //if (!grida.IsNewRowEditing)
            //{
            //    grida.DoRowValidation();
            //}
        }

        protected void gridat_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            ASPxGridView grida;
            switch (ASPxPageControl1.ActiveTabIndex)
            {
                case 0:
                    grida = gvKodifikimKlientFurnitor;
                    break;
                case 1:
                    grida = gvKodifikimKlientFurnitorGrupim2;
                    break;
                default:
                    grida = gvKodifikimKlientFurnitorGrupim3;
                    break;
            }

            KonfiguroGride(grida);
        }

        protected void initNewRow(object sender, ASPxDataInitNewRowEventArgs e)
        {
            var grida = sender as ASPxGridView;
            var llojkf = Request.QueryString["kf"] == "klient" ? 0 : 1;
            PercaktoTamplatePrindi(grida, llojkf);
        }

        protected void gridat_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            ASPxGridView grida;
            switch (ASPxPageControl1.ActiveTabIndex)
            {
                case 0:
                    grida = gvKodifikimKlientFurnitor;
                    break;
                case 1:
                    grida = gvKodifikimKlientFurnitorGrupim2;
                    break;
                default:
                    grida = gvKodifikimKlientFurnitorGrupim3;
                    break;
            }

            var arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                if (arr[2] == "")
                    grida.FilterExpression = "";
                else
                {
                    var filtra = new clsFiltraGrida();
                    var koka = new clsGridaKoka(IdGjuha, "gvKodifikimKlientFurnitor", "GrupimeKlientFurnitor.aspx", IdNdermarrja);
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], IdNdermarrja, koka.IdGridaKoka);

                    if (filtra.FiltraKodi != null)
                    {
                        grida.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, grida);
                        KonfigVleraFillestarePerGride(grida);
                    }
                }
            }

            KonfiguroGride(grida);
        }

        protected void gridat_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            var grida = sender as ASPxGridView;
            e.Properties["cpPageIndex"] = grida.PageIndex;
            e.Properties["cpPageRow"] = grida.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = grida.VisibleRowCount;
        }

        protected void gridat_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            e.Values.Clear();
            e.AddValue("(Te gjithe)", string.Empty, "true");
        }

        protected void gridat_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if (e.Column.FieldName == "IdPrindi" && Converter.ConvertToInt(e.Value) == 0)
                e.Criteria = null;
        }

        #endregion
    }
}