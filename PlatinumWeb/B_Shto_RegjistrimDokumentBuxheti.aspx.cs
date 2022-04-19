using DbCore;
using DbCore.DbAdmin;
using DbCore.DbBuxheti;
using DbCore.DbRegjistrim;
using DbCore.DbShare;
using DbCore.IMBUtils.Extensions;
using DbCore.IMBUtils.Messages;
using DevExpress.Web;
using Newtonsoft.Json;
using PlatinumWeb.ApplicationUtils.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using DbCore.IMBUtils.Logging;
using PlatinumWeb.ApplicationUtils;

namespace PlatinumWeb
{
    public partial class B_Shto_RegjistrimDokumentBuxheti : MyPageBase
    {
        private int _idKomponente;
        private string _komponente;
        private int idKonfigAmbjente;
        private int idKatDok = 0;

        protected void Page_PreInit(object sender, EventArgs e)
        {
            base.Page_PreInit(sender, e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            ShtoMenuControlsDheMsgFrame();
            if (!IsPostBack)
            {
                ShtoVleraTePergjithshmeNeHfState();
                hfState.Set("_komponente", _komponente = clsFunksione.GetKomponente(Page.Request));
                hfState.Set("_idKomponente", _idKomponente = clsKomponente.MerrIdKomponenteSipasEmrit(_komponente));
                PercaktoTeDrejtaNeHiddenField();
                KonfiguroVleraFillestare();
            }
            else
            {
                _komponente = hfState.Get("_komponente").ToString();
                _idKomponente = Convert.ToInt32(hfState.Get("_idKomponente"));
            }

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }

        private void PercaktoTeDrejtaNeHiddenField()
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            colTeDrejtaRoli teDrejtaNiveleRregjistrimi = new colTeDrejtaRoli();
            teDrejtaNiveleRregjistrimi.mbushTeDrejtaPerdoruesiNiveleRegjistrimiPerKomponente(IdPerdoruesi, IdNdermarrja, IdViti, _komponente);
            hfState.Set("teDrejtaNivele", Newtonsoft.Json.JsonConvert.SerializeObject(teDrejtaNiveleRregjistrimi));

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }
        private void KonfiguroVleraFillestare()
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            var lloji = Request.QueryString["lloji"];
            switch (lloji)
            {
                case "perfitim":
                    idKatDok = 177;
                    break;
                case "planifikimEkzekutimi":
                    idKatDok = 179;
                    break;
                case "ekzekutim":
                    idKatDok = 181;
                    break;
            }

            hfShtimModifikim.Value = Request.QueryString["shtim_modifikim"];
            hfId.Value = Request.QueryString["id"];
            mbushDhePercaktoNenkategorin();
            mbushDhePercaktoKonfigurimin();
            konfiguroTeDrejtaPerMenu();
            mbushComboLlojeVeprimi();
            mbushDhePercaktoComboFormatPrintimi(idKatDok);
            initDate();
            vendosLlojeObjektiNeHfState();
            hfState.Set("buxhetiSekondar", JsonConvert.SerializeObject(new ClsBLlojBuxheti(MessagesResource.Messages, "buxhetisekondar", IdNdermarrja)));
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmbEntiteti);
            hfState.Set("idKatDok", idKatDok);
            if (!String.IsNullOrEmpty(hfId.Value) && Convert.ToInt32(hfId.Value) > 0)
                hfState.Set("eshteDokGjenerues", ClsBKokaBuxheti.KtheIdGjeneruarDokumenti(Convert.ToInt32(hfId.Value)) > 0);
            if (idKatDok == 179 && hfShtimModifikim.Value == "modifikim" && !String.IsNullOrEmpty(hfId.Value) && Convert.ToInt32(hfId.Value) > 0)
            {
                int stsAprv = ClsBKokaBuxheti.KtheStatusAprovimiDokumenti(Convert.ToInt32(hfId.Value));
                if (stsAprv != 0)
                    lblStatusAprovimi.Text = ((StatusAprovimi)stsAprv).ToString().Replace('_', ' ');
            }
            if (idKatDok == 177)
            {
                hfState.Set("msgZgjidhAutomjetin", MessagesResource.Messages["msgZgjidhAutomjetin"]);
                ConfigureAspxComboBox.mbushComboLlojeBurimi(cmbBurimi);
                ConfigureAspxComboBox.percaktoTemplateComboMeLupe(btnDoktori);
            }

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }

        private void mbushDhePercaktoComboFormatPrintimi(int idKatDok)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idKatDok);

            ConfigureAspxComboBox.mbushComboFormatPrintimi(cmbFormatiPrintimit, idKatDok, IdNdermarrja, false);
            cmbFormatiPrintimit.SelectedIndex = 0;

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idKatDok);
        }

        private void vendosLlojeObjektiNeHfState()
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            var llojeObjekti = new List<object>();
            foreach (llojRreshtiShitje item in Enum.GetValues(typeof(llojRreshtiShitje)))
            {
                if(item.ToString().EqualsAnyIgnoreCase("Llogari","Artikull"))
                    llojeObjekti.Add(new { LlojObjekti = item.ToString(), IdLlojObjekti = (int)item });
            }
            hfState.Set("llojeObjekti", JsonConvert.SerializeObject(llojeObjekti));

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }
        private void mbushComboLlojeVeprimi()
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            var llojeVeprimi = new List<object>();
            foreach (EnumBLlojeVeprimi item in Enum.GetValues(typeof(EnumBLlojeVeprimi)))
            {
                if ((idKatDok == 177 && item.ToString().EqualsAnyIgnoreCase("Shitje", "Arketim")) || (idKatDok != 177 && item.ToString().EqualsAnyIgnoreCase("Blerje", "Pagese")))
                    llojeVeprimi.Add(new { LlojVeprimi = item.ToString(), IdLlojVeprimi = (int)item });
            }
            cmbLlojVeprimi.DataSource = llojeVeprimi;
            cmbLlojVeprimi.ValueField = "IdLlojVeprimi";
            cmbLlojVeprimi.TextField = "LlojVeprimi";
            cmbLlojVeprimi.DataBind();
            cmbLlojVeprimi.Value = Convert.ToString((int)EnumBLlojeVeprimi.Arketim);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }
        private void initDate()
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            hfState.Set("periudha", JsonConvert.SerializeObject(mySessionObjects.merrPeriudheKontabel(Session)));
            AspxWebControlUtils.vendosDateEditMask(dteDate);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }

        private void mbushDhePercaktoNenkategorin()
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            ConfigureAspxComboBox.mbushComboNiveletSipasTeDrejtaveMeKolona(cmbNiveli, IdNdermarrja, IdPerdoruesi, idKatDok, IdViti, _komponente, false, true);
            var idNiveli = Convert.ToInt32(Request.QueryString["idNivel"]);
            if (idNiveli > 0)
                cmbNiveli.Value = idNiveli.ToString();
            if (hfShtimModifikim.Value == "konvertim")
            {
                int idNiveliZgjedhur = 0;
                int.TryParse(Request.QueryString["niveli"], out idNiveliZgjedhur);
                if (idNiveliZgjedhur > 0)
                    cmbNiveli.Value = idNiveliZgjedhur.ToString();
            }

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }

        private void mbushDhePercaktoKonfigurimin()
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            colKonfigurimAmbjenti colKonfig = new colKonfigurimAmbjenti();
            if (cmbNiveli.Value != null)
                ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategoriseDheNivelit(IdPerdoruesi, IdNdermarrja, cmbModeli, idKatDok, int.Parse(cmbNiveli.Value.ToString()), rm, ci, IdGjuha);
            else
                ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(IdPerdoruesi, IdNdermarrja, cmbModeli, idKatDok, rm, ci, IdGjuha);

            cmbModeli.SelectedIndex = 0;
            if (hfShtimModifikim.Value == "konvertim")
            {
                int idKonfigZgjedhur = 0;
                int.TryParse(Request.QueryString["konfigurim"], out idKonfigZgjedhur);
                if (idKonfigZgjedhur > 0)
                    cmbModeli.Value = idKonfigZgjedhur.ToString();
                hfState.Set("llojKonvertimiNga", Request.QueryString["llojKonvertimiNga"]);
            }
            var konf = new clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasId(int.Parse(cmbModeli.SelectedItem.Value.ToString()), IdGjuha);
            hfKonffillestar.Value = $"{konf.KodKonfigAmbjente};{konf.PershkrimKonfigAmbjente}";
            idKonfigAmbjente = int.Parse(cmbModeli.Value.ToString());

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }

        protected void PercaktoTemplateMenu(ASPxMenu menu, ASPxMenu menuInfo)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            clsToolbarConfig.percaktoTemplateMenu(IdGjuha, IdViti, IdPerdoruesi, IdNdermarrja, menu, _komponente, this, menuInfo, null, null, null, null, hfShtimModifikim.Value != "modifikim", true, false, Meme, false, false, false, false, Convert.ToInt32(cmbNiveli.Value));
            if (_menu.Items.FindByName("Anullo") != null)
                _menu.Items.FindByName("Anullo").Text = DbCore.IMBUtils.Messages.MessagesResource.Messages["MenuItemLista"];
            if (_menu.Items.FindByName("Pezullo") != null)
                _menu.Items.FindByName("Pezullo").Text = DbCore.IMBUtils.Messages.MessagesResource.Messages["MenuItemRefuzo"];

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }
        protected void Menu_ItemClick(object source, MenuItemEventArgs e)
        {
        }
        protected void cmbEntiteti_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            if (!IsCallback)
            {
                ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
                return;
            }
            if (Request.Params["__CALLBACKID"].Contains("cmbEntiteti"))
            {
                int value = 0;
                if (e.Value == null || !Int32.TryParse(e.Value.ToString(), out value))
                {
                    ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
                    return;
                }
                switch (Convert.ToInt32(cmbLlojVeprimi.Value))
                {
                    case (int)EnumBLlojeVeprimi.Shitje:
                    case (int)EnumBLlojeVeprimi.Blerje:
                        ConfigureAspxComboBox.KonfiguroComboBoxKlientFurnitoriById(IdPerdoruesi, IdNdermarrja, (ASPxComboBox)source, value);
                        break;
                    case (int)EnumBLlojeVeprimi.Arketim:
                    case (int)EnumBLlojeVeprimi.Pagese:
                        ConfigureAspxComboBox.mbushComboArkaBankaById((ASPxComboBox)source, value);
                        break;
                }
            }

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }
        protected void cmbEntiteti_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            if (!IsCallback)
            {
                ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
                return;
            }
            if (Request.Params["__CALLBACKID"].Contains("cmbEntiteti"))
            {
                switch (Convert.ToInt32(cmbLlojVeprimi.Value))
                {
                    case (int)EnumBLlojeVeprimi.Shitje:
                        ConfigureAspxComboBox.KonfiguroComboBoxKlientFurnitor(e.Filter, e.BeginIndex + 1, e.EndIndex + 1, IdPerdoruesi, IdNdermarrja, (ASPxComboBox)source, 1);
                        break;
                    case (int)EnumBLlojeVeprimi.Blerje:
                        ConfigureAspxComboBox.KonfiguroComboBoxKlientFurnitor(e.Filter, e.BeginIndex + 1, e.EndIndex + 1, IdPerdoruesi, IdNdermarrja, (ASPxComboBox)source, 2);
                        break;
                    case (int)EnumBLlojeVeprimi.Arketim:
                    case (int)EnumBLlojeVeprimi.Pagese:
                        ConfigureAspxComboBox.mbushComboArkaBankaSipasFilter(e.Filter, e.BeginIndex + 1, e.EndIndex + 1, (ASPxComboBox)source, "arketim", IdPerdoruesi, IdNdermarrja);
                        break;

                }
            }

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }
        private void konfiguroTeDrejtaPerMenu()
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            var colTeDrejtat = Newtonsoft.Json.JsonConvert.DeserializeObject<colTeDrejtaRoli>(hfState.Get("teDrejtaNivele").ToString());
            var tedrejtaInfo = colTeDrejtat.Find(x => x.IdNivelRegjistrimi == Convert.ToInt32(cmbNiveli.Value));
            if (tedrejtaInfo == null)
            {
                tedrejtaInfo = new clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, _komponente);
            }

            hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
            hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
            hfTeDrejta.Add("ShtimDraft", tedrejtaInfo.DShtimDraft);
            hfTeDrejta.Add("ModifikimDraft", tedrejtaInfo.DModifikimDraft);
            hfTeDrejta.Add("Fshi", tedrejtaInfo.DFsh);
            hfTeDrejta.Add("Pezullo", tedrejtaInfo.DPezullo);
            hfTeDrejta.Add("Posto", tedrejtaInfo.DAutoKonverto);
            hfTeDrejta.Add("NdryshoCmimShitje", tedrejtaInfo.DNdryshoCmimShitje);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }

        protected void btnDoktori_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].Contains("btnDoktori"))
                {
                    int value = 0;
                    if (e.Value == null || !Int32.TryParse(e.Value.ToString(), out value))
                    {
                        ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
                        return;
                    }
                    ConfigureAspxComboBox.mbushComboAutomjetiByID((ASPxComboBox)source, value);
                }
            }

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }

        protected void btnDoktori_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].Contains("btnDoktori"))
                    ConfigureAspxComboBox.mbushComboAutomjetesh(IdNdermarrja, btnDoktori,e);
               

            }

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }
    }
}