using DbCore;
using DbCore.DbAdmin;
using DbCore.DbShare;
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
    public partial class B_Shto_RegjistrimBuxheti : MyPageBase
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

            var tedrejtaInfo = new clsTeDrejtaRoli();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, _komponente);
            hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
            hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
            hfTeDrejta.Add("Fshi", tedrejtaInfo.DFsh);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }

        private void KonfiguroVleraFillestare()
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            hfShtimModifikim.Value = Request.QueryString["shtim_modifikim"];

            var veprimi = Request.QueryString["planifikim_miratim"];
            switch (veprimi)
            {
                case "miratim":
                    idKatDok = 170;
                    break;
                case "planifikim":
                    idKatDok = 171;
                    break;
            }

            hfState.Set("idKatDok", idKatDok);
            hfId.Value = Request.QueryString["id"];


            mbushDhePercaktoNenkategorin();
            mbushDhePercaktoKonfigurimin();
            mbushVite();
            initDate();
            mbushDhePercaktoComboFormatPrintimi(idKatDok);

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
            cmbViti.Items.Add(viti.ToString(), viti);
            if (idKatDok == 171) // planifikim
            {
                int nrVitesh = clsKusht.kthevlereSipasKushtitDheIdKonfig(Convert.ToInt32(cmbModeli.Value), "NVB");
                nrVitesh = nrVitesh == 0 ? 3 : nrVitesh;
                for (int i = 1; i < nrVitesh; i++)
                {
                    cmbViti.Items.Add((viti + i).ToString(), (viti + i));
                }
            }
            cmbViti.SelectedIndex = 0;
            cmbViti.DropDownStyle = DropDownStyle.DropDownList;

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }

        private void mbushDhePercaktoNenkategorin()
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            ConfigureAspxComboBox.KonfiguroComboBoxNiveletSipasKategoriDtCombo(cmbNiveli, IdNdermarrja, IdPerdoruesi, idKatDok, false, true,false);
            if(hfShtimModifikim.Value == "konvertim")
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
                ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategoriseDheNivelit(IdPerdoruesi, IdNdermarrja, cmbModeli, idKatDok, Convert.ToInt32(cmbNiveli.Value), rm, ci, IdGjuha);
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
            konf.mbushKonfigAmbjSipasId(Convert.ToInt32(cmbModeli.Value), IdGjuha);
            hfKonffillestar.Value = $"{konf.KodKonfigAmbjente};{konf.PershkrimKonfigAmbjente}";
            idKonfigAmbjente = Convert.ToInt32(cmbModeli.Value);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }

        protected void PercaktoTemplateMenu(ASPxMenu menu, ASPxMenu menuInfo)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            clsToolbarConfig.percaktoTemplateMenu(IdGjuha, IdViti, IdPerdoruesi, IdNdermarrja, menu, _komponente, this, menuInfo, null, null, null, null, hfShtimModifikim.Value != "modifikim", true, false, Meme, false);
            if(_menu.Items.FindByName("Anullo")!=null)
            _menu.Items.FindByName("Anullo").Text = DbCore.IMBUtils.Messages.MessagesResource.Messages["MenuItemLista"];

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