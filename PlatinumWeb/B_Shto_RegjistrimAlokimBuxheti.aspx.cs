using DbCore;
using DbCore.DbAdmin;
using DbCore.DbBuxheti;
using DbCore.DbShare;
using DevExpress.DataAccess.Native.Data;
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
    public partial class B_Shto_RegjistrimAlokimBuxheti : MyPageBase
    {
        private int _idKomponente;
        private string _komponente;
        private int idKonfigAmbjente;
        private int idKatDok = 172;
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

            var tedrejtaInfo = new clsTeDrejtaRoli();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, _komponente);
            hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
            hfTeDrejta.Add("ShtimDraft", tedrejtaInfo.DShtimDraft);
            hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
            hfTeDrejta.Add("ModifikimDraft", tedrejtaInfo.DModifikimDraft);
            hfTeDrejta.Add("Fshi", tedrejtaInfo.DFsh);
            hfTeDrejta.Add("Eksporto", tedrejtaInfo.DEksporto);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }

        private void KonfiguroVleraFillestare()
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            hfShtimModifikim.Value = Request.QueryString["shtim_modifikim"];
            hfId.Value = Request.QueryString["id"];
            mbushDhePercaktoNenkategorin();
            mbushDhePercaktoKonfigurimin();
            ConfigureAspxComboBox.mbushComboViteBuxheti(Session, Convert.ToInt32(cmbModeli.Value), cmbViti); //mbushVite();
            mbushDhePercaktoComboFormatPrintimi();
            initDate();

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }

        private void mbushDhePercaktoComboFormatPrintimi()
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            ConfigureAspxComboBox.mbushComboFormatPrintimi(cmbFormatiPrintimit, idKatDok, IdNdermarrja, false);
            cmbFormatiPrintimit.SelectedIndex = 0;

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
            ConfigureAspxComboBox.KonfiguroComboBoxNiveletSipasKategoriDtCombo(cmbNiveli, IdNdermarrja, IdPerdoruesi, idKatDok, false, true,false);
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

            clsToolbarConfig.percaktoTemplateMenu(IdGjuha, IdViti, IdPerdoruesi, IdNdermarrja, menu, _komponente, this, menuInfo, null, null, null, null, hfShtimModifikim.Value != "modifikim", true, false, Meme, false);
            _menu.Items.FindByName("Anullo").Text = DbCore.IMBUtils.Messages.MessagesResource.Messages["MenuItemLista"];

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }

        protected void Menu_ItemClick(object source, MenuItemEventArgs e)
        {
           
        }
    }
}