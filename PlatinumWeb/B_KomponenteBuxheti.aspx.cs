using DbCore;
using DbCore.DbAdmin;
using DbCore.DbBuxheti;
using DbCore.DbShare;
using DbCore.IMBUtils.Messages;
using DevExpress.Web;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DbCore.IMBUtils.Logging;

namespace PlatinumWeb
{
    public partial class B_KomponenteBuxheti : MyPageBase
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            base.Page_PreInit(sender, e);
        }

        private int IdKatDok = 205;
        private string Komponente => clsFunksione.GetKomponente(Page.Request);
        private int IdKomponente => clsKomponente.MerrIdKomponenteSipasEmrit(Komponente);
        private int IdKonfigAmbjente;
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
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(IdNdermarrja, gvKomponenteBuxheti.ID, gvKomponenteBuxheti, cmbKonfigurimi.Text, Convert.ToString(IdKomponente), IdGjuha);
            }
            else
            {
                IdKonfigAmbjente = int.Parse(cmbKonfigurimi.Value.ToString());
                MbushGrideNgaDb(true);
            }

            clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, IdNdermarrja, gvKomponenteBuxheti.ID, IdKonfigAmbjente, Komponente);
            GridUtil.konfigGrideListeEMadhePaTheme(gvKomponenteBuxheti, "Id");
            gvKomponenteBuxheti.PercaktoTitlePanel(this, _menuInfo, _pnlMesazhi, hfState, IdPerdoruesi, IdNdermarrja, IdViti, IdGjuha, IdKonfigAmbjente, Komponente, rm, ci, false);
            gvKomponenteBuxheti.ToolTip = MessagesResource.Messages["MenuItem_01_KategoriBuxhetimi"];
            GridUtil.ToolTipButonaveMbiGride(gvKomponenteBuxheti, ci, rm);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }

        protected void PercaktoTemplateMenu(ASPxMenu menu, ASPxMenu menuInfo)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            clsToolbarConfig.percaktoTemplateMenu(IdGjuha, IdViti, IdPerdoruesi, IdNdermarrja, menu, Komponente, this, menuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, null, null, hfShtimModifikim.Value != "modifikim", true, false, Meme, false);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }

        private void KonfiguroVleraFillestare()
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            hfState.Set("_idKomponente", IdKomponente);
            ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(IdPerdoruesi, IdNdermarrja, cmbKonfigurimi, IdKatDok, rm, ci, IdGjuha);
            cmbKonfigurimi.SelectedIndex = 0;
            var konf = new clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()), IdGjuha);
            hfKonffillestar.Value = $"{konf.KodKonfigAmbjente};{konf.PershkrimKonfigAmbjente}";
            IdKonfigAmbjente = int.Parse(cmbKonfigurimi.Value.ToString());
            mbushHiddenFieldMePerkthime();

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }

        private void PercaktoTeDrejtaNeHiddenField()
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            var tedrejtaInfo = new clsTeDrejtaRoli();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, "Konfigurime Gride");
            hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, Komponente);
            hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
            hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
            hfTeDrejta.Add("Fshi", tedrejtaInfo.DFsh);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }

        private void MbushGrideNgaDb(bool ngaSession)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, ngaSession);

            var colKomponente = new ColBKomponente();
            if (ngaSession)
                colKomponente = mySessionObjects.MerrNgaSession<ColBKomponente>(Session, gvKomponenteBuxheti.ID);
            if (!ngaSession || ngaSession == null || !(colKomponente.Count > 0))
                colKomponente = new ColBKomponente(IdNdermarrja);
            mySessionObjects.RuajNeSession<ColBKomponente>(Session, colKomponente, gvKomponenteBuxheti.ID);
            gvKomponenteBuxheti.MbushGride(colKomponente, Session, GuidString, Komponente);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, ngaSession);
        }

        protected void KonfiguroCombo()
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            ConfigureAspxComboBox.shtoKolonaPerCombo(cmbBuxheti, "{0}", new string[] { "Kodi;Kodi", "Pershkrimi;Pershkrimi" }, new string[] { "IdLlojBuxheti" });
            ConfigureAspxComboBox.mbushComboEnumeration<EnumBLlojKufizimi>(cmbLlojKufizimi, true);
            ConfigureAspxComboBox.mbushComboEnumeration<EnumBNjesiKomponente>(cmbNjesia, false);
            ConfigureAspxComboBox.mbushComboEnumeration<EnumBTipKomponente>(cmbTipi, false);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmbBuxheti);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }
        #region Komponente
        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e) => 
            menu_msg_Frame.RuajFilter(gvKomponenteBuxheti, Komponente, IdKonfigAmbjente, ref hfStatusi);
        protected void FshiFilter_ASPxButton_Click(object sender, EventArgs e) => 
            menu_msg_Frame.FshiFilter(gvKomponenteBuxheti, Komponente, IdKonfigAmbjente, ref hfStatusi);
        protected void gvKomponenteBuxheti_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e) => 
            GridUtil.GridHeaderFilterFillItem(sender, e, rm, ci, "Kodi", "Pershkrimi");
        protected void gvKomponenteBuxheti_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e) => 
            GridUtil.GridCustomCallbackDefault(sender, e, gvKomponenteBuxheti, Komponente, IdNdermarrja, IdGjuha, cmbKonfigurimi, ref hfStatusi);
        protected void gvKomponenteBuxheti_DataBound(object sender, EventArgs e) =>
            GridUtil.ShtoCommandColumnNeDatabound(gvKomponenteBuxheti, "#", "Id");
        protected void gvKomponenteBuxheti_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e) =>
            GridUtil.GridCustomJsProperties(sender, e, gvKomponenteBuxheti);
        protected void gvKomponenteBuxheti_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e) =>
            GridUtil.GridAfterPerformCallback(sender, e, gvKomponenteBuxheti, _menu);
        #endregion


        protected void cmbBuxheti_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs eValue)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            if (!(IsCallback && Request.Params["__CALLBACKID"].Contains("cmbBuxheti")))
            {
                ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
                return;
            }
            ConfigureAspxComboBox.mbushComboLlojeBuxhetiSipasFiltrimit(IdNdermarrja, cmbBuxheti, cmbBuxheti.ID, false, eValue, null);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }

        protected void cmbBuxheti_ItemRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs eFilter)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            if (!(IsCallback && Request.Params["__CALLBACKID"].Contains("cmbBuxheti")))
            {
                ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
                return;
            }
            ConfigureAspxComboBox.mbushComboLlojeBuxhetiSipasFiltrimit(IdNdermarrja, cmbBuxheti, cmbBuxheti.ID, true, null, eFilter);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }

        private void mbushHiddenFieldMePerkthime()
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            clsFunksione.ShtoPerkthimNeHfState(hfState, "msgKompZgjidh", "lblLlojBuxheti", "msgKategoriBuxhetimiZgjidhPerModifikim");

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }
    }
}