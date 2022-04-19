using DbCore;
using DbCore.DbAdmin;
using DbCore.DbBuxheti;
using DbCore.DbShare;
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
using DbCore.IMBUtils.Logging;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;

namespace PlatinumWeb
{
    public partial class B_Shto_RegjistrimRialokimBuxheti : MyPageBase
    {
        private int _idKomponente;
        private string _komponente;
        private int idKonfigAmbjente;
        private int idKatDok;

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
                hfState.Set("_komponente", _komponente = clsFunksione.GetKomponente(Page.Request, false));
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

            hfShtimModifikim.Value = Request.QueryString["shtim_modifikim"] != null ? Request.QueryString["shtim_modifikim"] : "shtim";
            hfState.Set("idKatDok", idKatDok = 175);
            hfId.Value = Request.QueryString["id"];


            mbushDhePercaktoNenkategorin();
            mbushDhePercaktoKonfigurimin();
            konfiguroTeDrejtaPerMenu();
            mbushVite();
            initDate();
            mbushDhePercaktoComboFormatPrintimi(idKatDok);
            hfState.Set("buxhetiQeveritar", JsonConvert.SerializeObject(new ClsBLlojBuxheti(MessagesResource.Messages, "buxhetiqeveritar", IdNdermarrja)));

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

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }

        private void mbushDhePercaktoComboFormatPrintimi(int idKatDok)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idKatDok);

            ConfigureAspxComboBox.mbushComboFormatPrintimi(cmbFormatiPrintimit, idKatDok, IdNdermarrja, false);
            cmbFormatiPrintimit.SelectedIndex = 0;

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idKatDok);
        }

        private void initDate()
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            hfState.Set("periudha", JsonConvert.SerializeObject(mySessionObjects.merrPeriudheKontabel(Session)));
            AspxWebControlUtils.vendosDateEditMask(dteDate);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }

        private void mbushVite()
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            var periudhaKontabel = mySessionObjects.merrPeriudheKontabel(Session);
            var viti = periudhaKontabel.FillimiPeriudha.Year;
            int nrVitesh = clsKusht.kthevlereSipasKushtitDheIdKonfig(Convert.ToInt32(cmbModeli.Value), "NVB");
            nrVitesh = nrVitesh == 0 ? 3 : nrVitesh;
            var listVitesh = new List<object>();
            for (int i = 0; i < nrVitesh; i++)
            {
                listVitesh.Add(new { idViti = viti + i, viti = (viti + i).ToString() });
            }
            hfState.Set("vitet", JsonConvert.SerializeObject(listVitesh));

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }

        private void mbushDhePercaktoNenkategorin()
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);
            ConfigureAspxComboBox.KonfiguroComboBoxNivelet(cmbNiveli, IdNdermarrja, IdPerdoruesi, idKatDok, IdViti, _komponente, false, false);
            var idNiveli = Convert.ToInt32(Request.QueryString["idNivel"]);
            if (idNiveli > 0)
                cmbNiveli.Value = idNiveli.ToString();

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }

        private void mbushDhePercaktoKonfigurimin()
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            colKonfigurimAmbjenti colKonfig = new colKonfigurimAmbjenti();
            if (cmbNiveli.Value != null)
                ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategoriseDheNivelit(IdPerdoruesi, IdNdermarrja, cmbModeli, idKatDok, Convert.ToInt32(cmbNiveli.Value), rm, ci, IdGjuha);
            else
                ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(IdPerdoruesi, IdNdermarrja, cmbModeli, idKatDok, rm, ci, IdGjuha);

            cmbModeli.SelectedIndex = 0;
            var konf = new clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasId(Convert.ToInt32(cmbModeli.Value), IdGjuha);
            hfKonffillestar.Value = $"{konf.KodKonfigAmbjente};{konf.PershkrimKonfigAmbjente}";
            idKonfigAmbjente = Convert.ToInt32(cmbModeli.Value);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }

        protected void PercaktoTemplateMenu(ASPxMenu menu, ASPxMenu menuInfo)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            clsToolbarConfig.percaktoTemplateMenu(IdGjuha, IdViti, IdPerdoruesi, IdNdermarrja, menu, _komponente, this, menuInfo, null, null, null, null, hfShtimModifikim.Value != "modifikim", true, false, Meme, false, false,false,false,Convert.ToInt32(cmbNiveli.Value));
            if (_menu.Items.FindByName("Anullo") != null)
                _menu.Items.FindByName("Anullo").Text = DbCore.IMBUtils.Messages.MessagesResource.Messages["MenuItemLista"];
            if (_menu.Items.FindByName("Pezullo") != null)
                _menu.Items.FindByName("Pezullo").Text = DbCore.IMBUtils.Messages.MessagesResource.Messages["MenuItemRefuzo"];

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }
        protected void Menu_ItemClick(object source, MenuItemEventArgs e)
        {
            switch (e.Item.Name)
            {
            }
        }

    }
}