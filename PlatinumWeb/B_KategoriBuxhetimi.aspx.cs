using DevExpress.Web;
using PlatinumWeb.ApplicationUtils.Pages;
using System;
using DbCore;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using DbCore.DbAdmin;
using DbCore.DbBuxheti;
using DbCore.IMBUtils.Extensions;
using System.Collections.Generic;
using DbCore.DbShare;
using System.Linq;
using Newtonsoft.Json;
using DbCore.IMBUtils.Messages;
using System.Resources;
using DbCore.DbRegjistrim;
using DbCore.DbKontabiliteti;
using DbCore.IMBUtils.Logging;

namespace PlatinumWeb
{
    public partial class BKategoriBuxhetimi : MyPageBase
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            base.Page_PreInit(sender,e);
        }

        private int IdKatDok;
        private int _idKomponente;
        private string _komponente => clsFunksione.GetKomponente(Page.Request);
        private bool shfaqKontrollePerNdermBije;
        private bool shfaqKontrollePerNdermPrind;
        private bool integroKategoriTeNdermBija;
        private int idKonfigAmbjente;
        protected void Page_Load(object sender, EventArgs e)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            base.ShtoMenuControlsDheMsgFrame();
            if (!IsPostBack)
            {

                ShtoVleraTePergjithshmeNeHfState();
                KonfiguroVleraFillestare();
                PercaktoTeDrejtaNeHiddenField();
                MbushGrideNgaDb(false);
                KonfiguroCombo();
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(IdNdermarrja, gvKategoriBuxhetimi.ID, gvKategoriBuxhetimi, cmbKonfigurimi.Text, Convert.ToString(_idKomponente), IdGjuha);
                hfState.Set("colBuxheti", Newtonsoft.Json.JsonConvert.SerializeObject(ColBLlojBuxheti.KtheSipasNdermarrjes(IdNdermarrja)));
            }
            else
            {
                if (!(bool)hfState.Get("lupe"))
                {
                    shfaqKontrollePerNdermBije = (bool)hfState.Get("shfaqKontrollePerNdermBije");
                    shfaqKontrollePerNdermPrind = (bool)hfState.Get("shfaqKontrollePerNdermPrind");
                    integroKategoriTeNdermBija = (bool)hfState.Get("integroKategoriTeNdermBija");
                }
                idKonfigAmbjente = int.Parse(cmbKonfigurimi.Value.ToString());
                MbushGrideNgaDb(true);
            }

            clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, IdNdermarrja, gvKategoriBuxhetimi.ID, idKonfigAmbjente, _komponente);
            KonfiguroGride();
            GridUtil.konfigGrideListeEMadhePaTheme(gvKategoriBuxhetimi, "IdKategoriBuxhetimi");
            gvKategoriBuxhetimi.PercaktoTitlePanel(this, _menuInfo, _pnlMesazhi, hfState, IdPerdoruesi, IdNdermarrja, IdViti, IdGjuha, idKonfigAmbjente, _komponente, rm, ci, false);
            gvKategoriBuxhetimi.ToolTip = MessagesResource.Messages["MenuItem_01_KategoriBuxhetimi"];
            GridUtil.ToolTipButonaveMbiGride(gvKategoriBuxhetimi, ci, rm);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }


        #region veprimeMeObjektin
        protected void Ruaj()
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            var kategoriBuxhetimi = KrijoKategoriBuxhetimi(integroKategoriTeNdermBija);
            kategoriBuxhetimi.Buxheti = string.Join(",",kategoriBuxhetimi.LlojeBuxheti.Select(x => x.Kodi).ToArray());
            clsMesazh mesazh;

            if (hfShtimModifikim.Value.EqualsAnyIgnoreCase("shtim", "modifikim") && !(kategoriBuxhetimi.NivelKategorie > 1))
            {
                clsMenuInfo.ShtoMesazh(_menuInfo, new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, MessagesResource.Messages["msgKategoriBuxhetimiNukRuanPrind"]), _pnlMesazhi);
                return;
            }
            else if(hfShtimModifikim.Value.EqualsAnyIgnoreCase("shtimPrindi", "modifikimPrindi") && kategoriBuxhetimi.NivelKategorie > 1)
            {
                clsMenuInfo.ShtoMesazh(_menuInfo, new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, MessagesResource.Messages["msgKategoriBuxhetimiNukRuanBije"]), _pnlMesazhi);
                return;
            }
            var colKategoriBuxhetimi = (ColBKategoriBuxhetimi)gvKategoriBuxhetimi.DataSource;
            
            if (hfShtimModifikim.Value.EqualsAnyIgnoreCase("shtim", "shtimPrindi"))
            {
                mesazh = RuajShtimKategoriBuxhetimi(kategoriBuxhetimi);
            }
            else
            {
                mesazh = RuajModifikimKategoriBuxhetimi(kategoriBuxhetimi);
                if(mesazh.Status)
                    colKategoriBuxhetimi.FindAndRemove(x => x.IdKategoriBuxhetimi == kategoriBuxhetimi.IdKategoriBuxhetimi);
            }

            if (mesazh.Status)
            {
                colKategoriBuxhetimi.AddIfNotExists(kategoriBuxhetimi);
                mySessionObjects.RuajNeSession<ColBKategoriBuxhetimi>(Session, colKategoriBuxhetimi, gvKategoriBuxhetimi.ID);
                hfState.Set("editedKategoriBuxhetimi", JsonConvert.SerializeObject(new { kategoriBuxhetimi = new ColBKategoriBuxhetimi(kategoriBuxhetimi), veprimi = "shto" }));
            }
            hfStatusi.Value = mesazh.Status.ToString().ToLower();
            clsMenuInfo.ShtoMesazh(_menuInfo, mesazh, _pnlMesazhi);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }

        private clsMesazh RuajShtimKategoriBuxhetimi(ClsBKategoriBuxhetimi kategoriBuxhetimi)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, kategoriBuxhetimi);

            kategoriBuxhetimi.IdKrijuesi = IdPerdoruesi;

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, kategoriBuxhetimi);
            return kategoriBuxhetimi.Ruaj();
        }

        private clsMesazh RuajModifikimKategoriBuxhetimi(ClsBKategoriBuxhetimi kategoriBuxhetimi)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, kategoriBuxhetimi);

            kategoriBuxhetimi.IdKategoriBuxhetimi = Convert.ToInt32(hfId.Value);
            kategoriBuxhetimi.IdModifikuesi = IdPerdoruesi;

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, kategoriBuxhetimi);
            return kategoriBuxhetimi.Modifiko();
        }

        private void Fshi()
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            clsMesazh mesazh;
            var rreshtat = new List<object>();
            var fshiPrind = (bool)hfState.Get("fshiPrindi");
            var kategoriTeFshira = new ColBKategoriBuxhetimi();
            List<string> kategoriTePaFshira = new List<string>();
            var colKategoriBuxhetimi = (ColBKategoriBuxhetimi)gvKategoriBuxhetimi.DataSource;
            if (PageControl1.ActiveTabIndex == 0)//nqs kemi zgjedhur vetem nje kartele per te pare fshijme vetem ate
                rreshtat = gvKategoriBuxhetimi.GetSelectedFieldValues("IdKategoriBuxhetimi");
            else
                rreshtat.Add(hfId.Value);

            for(int i = 0; i< rreshtat.Count; i++)
            {
                var kategoriBuxhetimi = new ClsBKategoriBuxhetimi(MessagesResource.Messages,Convert.ToInt32(rreshtat[i]));
                if(!fshiPrind && kategoriBuxhetimi.NivelKategorie == 1)
                {
                    clsMenuInfo.ShtoMesazh(_menuInfo, new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, MessagesResource.Messages["msgKategoriBuxhetimiNukFshinPrind"]), _pnlMesazhi);
                    return;
                }

                kategoriBuxhetimi.SetIntegroKategoriTeNdermBija(integroKategoriTeNdermBija);
                mesazh = kategoriBuxhetimi.Fshi();
                if (!mesazh.Status)
                {
                    kategoriTePaFshira.Add(kategoriBuxhetimi.Kodi);
                    continue;
                }
                kategoriTeFshira.Add(kategoriBuxhetimi);
                colKategoriBuxhetimi.FindAndRemove(x => x.IdKategoriBuxhetimi == kategoriBuxhetimi.IdKategoriBuxhetimi);
                hfStatusi.Value = "true";
            }
            if (kategoriTePaFshira.Count > 0)
                clsMenuInfo.ShtoMesazh(_menuInfo, new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, $"{MessagesResource.Messages["msgKategoriBuxhetimiTePaFshira"]} {String.Join(",", kategoriTePaFshira.ToArray())}"),_pnlMesazhi);
            if (kategoriTeFshira.Count > 0)
                clsMenuInfo.ShtoMesazhSuksesi(_menuInfo, $"{MessagesResource.Messages["msgKategoriBuxhetimiTeFshira"]} {String.Join(",", kategoriTeFshira.Select(x => x.Kodi).ToArray())}", _pnlMesazhi);

            hfState.Set("editedKategoriBuxhetimi", JsonConvert.SerializeObject(new { kategoriBuxhetimi = kategoriTeFshira, veprimi = "fshi" }));
            mySessionObjects.RuajNeSession<ColBKategoriBuxhetimi>(Session, colKategoriBuxhetimi, gvKategoriBuxhetimi.ID);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }
        #endregion

        #region menu
        protected void PercaktoTemplateMenu(ASPxMenu menu, ASPxMenu menuInfo)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            clsToolbarConfig.percaktoTemplateMenu(IdGjuha, IdViti, IdPerdoruesi, IdNdermarrja, menu, _komponente, this, menuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, null, null, hfShtimModifikim.Value != "modifikim", true, false, Meme, false);
            if (!(bool)hfState.Get("lupe"))
            {
                clsToolbarConfig.HideMenuItems(_menu, "OK", "Anullo");
                if(!shfaqKontrollePerNdermBije)
                    clsToolbarConfig.HideMenuItems(_menu, "Shto", "Modifiko", "Fshi");
                if (!shfaqKontrollePerNdermPrind)
                    clsToolbarConfig.HideMenuItems(_menu, "ShtoPrind", "ModifikoPrind", "FshiPrind");
                if(!shfaqKontrollePerNdermBije && !shfaqKontrollePerNdermPrind)
                    clsToolbarConfig.HideMenuItems(_menu, "Fshi", "FshiPrind", "Ruaj");
            }
            else
            {
                clsToolbarConfig.HideMenuItems(_menu, "Shto", "ShtoPrind", "Modifiko", "ModifikoPrind", "Fshi", "FshiPrind", "Ruaj");
                PageControl1.TabPages[1].ClientVisible = false;
                _menu.Items.FindByName("Anullo").Text = MessagesResource.Messages["MenuItemMbyll"];
            }

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }
        protected void Menu_ItemClick(object source, MenuItemEventArgs e)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            switch (e.Item.Name)
            {
                case "Ruaj":
                    Ruaj();
                    break;
                case "Fshi":
                case "FshiPrind":
                    Fshi();
                    break;
            }

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }

        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            menu_msg_Frame.RuajFilter(gvKategoriBuxhetimi, _komponente, idKonfigAmbjente,ref hfStatusi);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }
        protected void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            menu_msg_Frame.FshiFilter(gvKategoriBuxhetimi, _komponente, idKonfigAmbjente, ref hfStatusi);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }
        #endregion

        #region gvKategoriBuxhetimi
        protected void gvKategoriBuxhetimi_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e) =>
            GridUtil.GridHeaderFilterFillItem(sender, e, rm, ci, "Kodi", "Pershkrimi" );

        protected void gvKategoriBuxhetimi_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            GridUtil.GridCustomCallbackDefault(sender, e, gvKategoriBuxhetimi, _komponente, IdNdermarrja, IdGjuha, cmbKonfigurimi, ref hfStatusi);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }

        protected void gvKategoriBuxhetimi_DataBound(object sender, EventArgs e)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            GridUtil.ShtoCommandColumnNeDatabound(gvKategoriBuxhetimi, "#", "IdKategoriBuxhetimi");

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }

        protected void gvKategoriBuxhetimi_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            GridUtil.GridCustomJsProperties(sender, e, gvKategoriBuxhetimi);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }

        protected void gvKategoriBuxhetimi_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            GridUtil.GridAfterPerformCallback(sender, e, gvKategoriBuxhetimi, _menu);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }

        public void MbushGrideNgaDb(bool ngaSesioni)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, ngaSesioni);

            var colKategoriBuxhetimi = new ColBKategoriBuxhetimi();
            if (ngaSesioni)
                colKategoriBuxhetimi = mySessionObjects.MerrNgaSession<ColBKategoriBuxhetimi>(Session, gvKategoriBuxhetimi.ID);
            if (!ngaSesioni || colKategoriBuxhetimi == null || !(colKategoriBuxhetimi.Count > 0))
            {
                var idNdermarrjeKategorish = !(String.IsNullOrEmpty(Request.QueryString["idNdermarrje"])) ? Convert.ToInt32(Request.QueryString["idNdermarrje"]) : IdNdermarrja;
                if (IdKatDok == 204)
                    colKategoriBuxhetimi = ColBKategoriBuxhetimi.merrKategoriBuxhetimiAktiveSipasNdermarrjes(idNdermarrjeKategorish);
                else
                    colKategoriBuxhetimi = new ColBKategoriBuxhetimi(idNdermarrjeKategorish);
            }
            mySessionObjects.RuajNeSession<ColBKategoriBuxhetimi>(Session, colKategoriBuxhetimi, gvKategoriBuxhetimi.ID);
            gvKategoriBuxhetimi.MbushGride(colKategoriBuxhetimi, Session, GuidString, _komponente);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, ngaSesioni);
        }

        protected void KonfiguroGride()
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            KonfigurimComboGride.ShtoPrindKategoriBuxhetimi(gvKategoriBuxhetimi, IdNdermarrja, Session, _komponente, GuidString, "IdPrindi");
            KonfigurimComboGride.ShtoLlogariSipasNdermarrjes(gvKategoriBuxhetimi, IdNdermarrja, Session, _komponente, GuidString, true, "IdLlogaria");
            var columnKoeficenti = (GridViewEditDataColumn)gvKategoriBuxhetimi.Columns["Koeficenti"];
            columnKoeficenti.PropertiesEdit.DisplayFormatString = "G29";

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }
        #endregion

        #region vleraFillestare
        protected void PercaktoTeDrejtaNeHiddenField()
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            var tedrejtaInfo = new clsTeDrejtaRoli();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, "Konfigurime Gride");
            hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, _komponente);
            hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
            hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
            hfTeDrejta.Add("Fshi", tedrejtaInfo.DFsh);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }

        protected void KonfiguroVleraFillestare()
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            bool lupe = Request.QueryString["lupe"].ToLower() == "true";
            IdKatDok = lupe ? 204 : 168;
            hfState.Set("_idKomponente", _idKomponente = clsKomponente.MerrIdKomponenteSipasEmrit(_komponente));
            hfState.Set("lupe", lupe);
            ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(IdPerdoruesi, IdNdermarrja, cmbKonfigurimi, IdKatDok, rm, ci, IdGjuha);
            cmbKonfigurimi.SelectedIndex = 0;
            var konf = new clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()), IdGjuha);
            hfKonffillestar.Value = $"{konf.KodKonfigAmbjente};{konf.PershkrimKonfigAmbjente}";
            idKonfigAmbjente = int.Parse(cmbKonfigurimi.Value.ToString());

            shfaqKontrollePerNdermBije = clsAlternativaKushti.getAlternativa(idKonfigAmbjente, "SHIPSHKB").ToLower() == "po";
            shfaqKontrollePerNdermPrind = clsAlternativaKushti.getAlternativa(idKonfigAmbjente, "SHIPSHKP").ToLower() == "po";
            integroKategoriTeNdermBija = clsAlternativaKushti.getAlternativa(idKonfigAmbjente, "IKKTB").ToLower() == "po";

            mbushHiddenFieldMePerkthime();

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }
        #endregion

        #region veprimeMeKategoriBuxhetimi
        protected ClsBKategoriBuxhetimi KrijoKategoriBuxhetimi(bool integroKategoriTeNdermBija)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, integroKategoriTeNdermBija);

            var kodi = txtKodi.Text;
            var pershkrimi = txtEmertimi.Text;
            var pershkrimi2 = txtEmertimi2.Text;
            var buxheti = cmbBuxhetiHf.Value;
            var niveli = !string.IsNullOrWhiteSpace(txtNiveli.Text) ? Convert.ToInt32(txtNiveli.Value) : 0;
            var koeficenti = !string.IsNullOrWhiteSpace(txtKoeficenti.Text) ? Convert.ToDecimal(txtKoeficenti.Value) : (decimal?)null;
            var idPrindi = btnePrindi.Value != null ? Convert.ToInt32(btnePrindi.Value) : 0;
            var idLlogaria = btneLlogaria.Value != null ? Convert.ToInt32(btneLlogaria.Value) : 0;
            var aktive = (bool)cbAktive.Value;

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, integroKategoriTeNdermBija);
            return new ClsBKategoriBuxhetimi(MessagesResource.Messages,0,kodi,pershkrimi, pershkrimi2, IdNdermarrja, idPrindi, niveli, ColBLlojBuxheti.KtheSipasBuxheteve(buxheti), koeficenti, idLlogaria, integroKategoriTeNdermBija, aktive);
        }
        #endregion

        #region comboboxes
        protected void KonfiguroCombo()
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            ConfigureAspxComboBox.shtoKolonaPerCombo(btnePrindi, "{0}", new string[] { "Kodi;Kodi", "Pershkrimi;Pershkrimi"}, new string[] { "IdKategoriBuxheti" });
            ConfigureAspxComboBox.shtoKolonaPerCombo(btneLlogaria, "{0}", new string[] { "NrLlogari;NrLlogari", "EmerLlogari1;EmerLlogari1" }, new string[] { "IdLlogari" });
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(btnePrindi, btneLlogaria);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }
        protected void btnePrindi_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs eValue)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            if (!(IsCallback && Request.Params["__CALLBACKID"].Contains("btnePrindi")))
            {
                ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
                return;
            }
            ConfigureAspxComboBox.mbushComboKategoriBuxhetiSipasFiltrimit(IdNdermarrja, btnePrindi, gvKategoriBuxhetimi.ID, true, eValue, null);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }
        protected void btnePrindi_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs eFilter)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            if (!(IsCallback && Request.Params["__CALLBACKID"].Contains("btnePrindi")))
            {
                ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
                return;
            }
            ConfigureAspxComboBox.mbushComboKategoriBuxhetiSipasFiltrimit(IdNdermarrja, btnePrindi, gvKategoriBuxhetimi.ID, true, null, eFilter);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }
        protected void btneLlogaria_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs eValue)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            if (!(IsCallback && Request.Params["__CALLBACKID"].Contains("btneLlogaria")))
            {
                ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
                return;
            }
            ConfigureAspxComboBox.mbushComboLlogariaPaKolona(IdPerdoruesi, IdNdermarrja, btneLlogaria, eValue);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }
        protected void btneLlogaria_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs eFilter)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            if (!(IsCallback && Request.Params["__CALLBACKID"].Contains("btneLlogaria")))
            {
                ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
                return;
            }
            ConfigureAspxComboBox.mbushComboLlogariaPaKolona(IdPerdoruesi, IdNdermarrja, btneLlogaria, eFilter);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }


        #endregion

        #region perkthime
        private void mbushHiddenFieldMePerkthime()
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            clsFunksione.ShtoPerkthimNeHfState(hfState, "msgKategoriBuxhetimiNukZgjedhMeShumeSeNjePrind", "msgKategoriBuxhetimiNukZgjedhPrindMeLlogari", "msgKategoriBuxhetimiZgjidhPrind",
                "msgKategoriBuxhetimiZgjidhPerModifikim", "msgKategoriBuxhetimiKoeficenti", "msgKategoriBuxhetimiLupaName", "msgKategoriBuxhetimiPrindVetvetja", "msgZgjidhVetemNjeArtikullBuxhetimiNgaLista");

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }
        #endregion

    }
}