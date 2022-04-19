using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Drawing;
using DevExpress.Web;
using AjaxControlToolkit;
using System.Collections.Generic;
using DbCore.DbKontabiliteti;
using DbCore.DbInventari;
using DbCore.DbRegjistrim;
using System.Web.Script.Serialization;
using PlatinumWeb.Templates;
using DbCore;
using DbCore.DbShare;
using System.Globalization;
using System.Resources;
using DbCore.IMBUtils.Extensions;
using DbCore.IMBUtils.Types;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{
    public partial class Shto_ShperndarjeShpenzimesh : MyPageBase
    {       
        private string komponente = "Shto_ShperndarjeShpenzimesh.aspx";
        
        protected void Page_PreInit(object sender, EventArgs e)
        {
            base.Page_PreInit(sender, e);
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            base.ShtoMenuControlsDheMsgFrame();

            if (!IsPostBack)
            {
                base.ShtoVleraTePergjithshmeNeHfState();
                mbushHiddenFieldMePerkthime(ci, rm);
                konfiguroTeDrejtaPerMenu();
                konfiguroVleraFillestare();
            }
            ASPxNavBar1.Groups[0].Text = rm.GetString("lblFature", ci);
        }

        private void konfiguroTeDrejtaPerMenu()
        {
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, "Konfigurime Gride");
            hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, komponente);
            hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
            hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
            hfTeDrejta.Add("ShtimDraft", tedrejtaInfo.DShtimDraft);
            hfTeDrejta.Add("ModifikimDraft", tedrejtaInfo.DModifikimDraft);
        }


        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        protected void PercaktoTemplateMenu(ASPxMenu menu, ASPxMenu menuInfo)
        {
            clsToolbarConfig.percaktoTemplateMenu(IdGjuha, IdViti, IdPerdoruesi, IdNdermarrja, menu, komponente, this, menuInfo, null, null, null, null, hfShtimModifikim.Value != "modifikim", true, false, Meme, false);
            if (Request.QueryString["id"] != null)
            {
                int idStatusDok = clsShperndarjeShpenzimeKoka.ktheIdStatusDokSipasID(int.Parse(Request.QueryString["id"]));
                bool visible = clsFunksione.merrMenuVisibleDraft(hfShtimModifikim.Value == "modifikim" ? false : true, idStatusDok);
                _menu.Items.FindByName("Draft").ClientVisible = visible;
            }
            _menu.Items.FindByName("Anullo").Text = MessagesResource.Messages["MenuItemLista"];
        }
        
        private void vendosDataDefault()
        {
            dateDtRegjistrimi.Value = DateTime.Today;
        }

        /// <summary>
        /// Vendos vlerat default te disa prej kontrolleve ne rastin kur po bejme shtim
        /// </summary>
        /// <param name="idGjuha"></param>
        /// <param name="idPerdoruesi"></param>
        /// <param name="idNdermarrje"></param>
        private void konfiguroVleraFillestare()
        {
            hfShtimModifikim.Value = String.IsNullOrEmpty(Request.QueryString["shtim_modifikim"]) ? "shtim" : Request.QueryString["shtim_modifikim"];
            hfId.Value = Request.QueryString["id"];
            AspxWebControlUtils.vendosDateEditMask(dateDtRegjistrimi);
            mbushComboNivelesh(cmbLloji);
            mbushComboKonfigurimet(false);
            vendosDataDefault();
            radioShperndaSipas.Items[1].Selected = true;
            mySessionObjects.ruajTrupatNeSession(Session, new colTrupiMagazina());

            if(Request.QueryString["shtim_modifikim"] == "modifikim" && hfId.Value != "" && Convert.ToInt32(hfId.Value) > 0)
            {
                clsShperndarjeShpenzimeKoka koka = new clsShperndarjeShpenzimeKoka(Convert.ToInt32(hfId.Value));
                DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
                DataTable dtlidhur = dbAdmin.MerrDokLidhur(koka.IdKokaShperndarjeShpenz, koka.IdNivel, "T_SHPERNDARJESHPENZIMEKOKA", "IDSHPERNDARJESHPENZ");
                hfLidhur.Value = (!(dtlidhur.Rows.Count == 0)).ToString();
                AspxWebControlUtils.ShtoLidhje(IdPerdoruesi, IdViti, IdNdermarrja, hl, dtlidhur, koka.IdGjenerues, koka.IdNivelGjenerues, koka.IdKonfigGjenerues, IdGjuha);
                dbAdmin.Dispose();
            }
        }


        private void mbushComboKonfigurimet(bool mod)
        {
            ConfigureAspxComboBox.KonfiguroComboBoxKonfigurimeshSipasKategoriseDheNivelitMeLloj(cmbKonfigurimi, IdPerdoruesi, IdNdermarrja, IdGjuha, 7, cmbLloji.SelectedItem.GetFieldValue("Kodi").ToString());
        }

        private void mbushComboNivelesh(ASPxComboBox cmblloji)
        {
            ConfigureAspxComboBox.KonfiguroComboBoxNiveletSipasKategoriDtCombo(cmbLloji, IdNdermarrja, IdPerdoruesi, 7, false, true, true);
            cmbLloji.ClientSideEvents.Init = "function(s,e){TextChangedLloji();}";
        }

        /// <summary>
        /// Metode qe sherben per te kaluar perkthime te js
        /// </summary>
        /// <param name="cultinf">Merr culture info</param>
        /// <param name="rm">Merr Resource Manager</param>
        private void mbushHiddenFieldMePerkthime(CultureInfo cultinf, ResourceManager rm)
        {
            hfState.Set("msgGabimGjateTransferimitTeTeDhenave", MessagesResource.Messages["msgGabimGjateTransferimitTeTeDhenave"]);
            hfState.Set("popupAdministrimiUniversal", MessagesResource.Messages["popupAdministrimiUniversal"]);
            hfState.Set("msgZgjidhDokumentin", MessagesResource.Messages["msgZgjidhDokumentin"]);
            hfState.Set("msgZgjidhniMetodeShperndarje", MessagesResource.Messages["msgZgjidhniMetodeShperndarje"]);
            hfState.Set("headerPopUpText", MessagesResource.Messages["headerPopUpText"]);
            hfState.Set("msgLlogariKlasaGjashte", MessagesResource.Messages["msgLlogariKlasaGjashte"]);
            hfState.Set("msgTotaliLlogKalonVlShperndarje", MessagesResource.Messages["msgTotaliLlogKalonVlShperndarje"]);
            hfState.Set("msgShenoniNumrinEDokumentit", MessagesResource.Messages["msgShenoniNumrinEDokumentit"]);
            hfState.Set("msgVendosniVlShperndarje", MessagesResource.Messages["msgVendosniVlShperndarje"]);
            hfState.Set("msgZgjidhniLlojin", MessagesResource.Messages["msgZgjidhniLlojin"]);
            hfState.Set("msgZgjidhniNjeDateRegjstrimi", MessagesResource.Messages["msgZgjidhniNjeDateRegjstrimi"]);
            hfState.Set("msgShtoLlogariZgjidhniTePaktenNjeLlogari", MessagesResource.Messages["msgShtoLlogariZgjidhniTePaktenNjeLlogari"]);
            hfState.Set("msgTotaliVleftaNdryshemVlShpernd", MessagesResource.Messages["msgTotaliVleftaNdryshemVlShpernd"]);
            hfState.Set("msgKujdesTotalNjejteTotVlLlogari", MessagesResource.Messages["msgKujdesTotalNjejteTotVlLlogari"]);
            hfState.Set("msgDeshironiShperndQendraKostoMag", MessagesResource.Messages["msgDeshironiShperndQendraKostoMag"]);
            hfState.Set("msgShperndarjeNeQendratEKostos", MessagesResource.Messages["msgShperndarjeNeQendratEKostos"]);
            hfState.Set("msgDeshironiShperndQendraKostoMag", MessagesResource.Messages["msgDeshironiShperndQendraKostoMag"]);
            hfState.Set("msgShperndarjeNeQendratEKostos", MessagesResource.Messages["msgShperndarjeNeQendratEKostos"]);
            hfState.Set("msgDeshironiShperndQendraKostoMag", MessagesResource.Messages["msgDeshironiShperndQendraKostoMag"]);
            hfState.Set("msgZgjidhDokumentin", MessagesResource.Messages["msgZgjidhDokumentin"]);
            hfState.Set("labelAdministrimiMsgJeniSigurt", MessagesResource.Messages["labelAdministrimiMsgJeniSigurt"]);
            hfState.Set("msgRuajtjeMeSukses", MessagesResource.Messages["msgRuajtjeMeSukses"]);
            hfState.Set("msgJuLutemPrisniDisaSekonda", MessagesResource.Messages["msgJuLutemPrisniDisaSekonda"]);
        }

    }
}
