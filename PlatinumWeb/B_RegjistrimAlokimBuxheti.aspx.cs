using DevExpress.Web;
using PlatinumWeb.ApplicationUtils.Pages;
using System;
using DbCore;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using DbCore.DbAdmin;
using DbCore.DbBuxheti;
using DbCore.DbInventari;
using DbCore.DbRegjistrim;
using System.Data;
using System.Globalization;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DbCore.IMBUtils.Extensions;
using System.Collections.Generic;
using DbCore.DbShare;
using System.Linq;
using Newtonsoft.Json;
using DbCore.IMBUtils.Messages;
using System.Resources;
using DbCore.IMBUtils.Logging;

namespace PlatinumWeb
{
    public partial class B_RegjistrimAlokimBuxheti : MyPageBase
    {
        private int _idKomponente;
        private string _komponente => DbCore.clsFunksione.GetKomponente(Page.Request);

        private string filterExpressionDefault = "[IdStatusDok]=1";
        protected void Page_Load(object sender, EventArgs e)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            base.ShtoMenuControlsDheMsgFrame();
            if (IsCallback && Request.Params["__CALLBACKPARAM"].Contains("ROWVALUES"))//kur grida ben callback behet dy here callbacku njehere me ngjarjen qe po ndodh dhe njehere me rowvalues. per te eleminuar marrjen e te dhenave heren e dyte dalim nga funksioni
            {
                ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
                return;
            }
            if (!IsPostBack)
            {
                ShtoVleraTePergjithshmeNeHfState();
                if (!DbCore.mySessionObjects.isLogedIn(Session)) DbCore.clsFunksione.logout(Session, true, "FaqePaautorizuar");

                hfState.Set("_idKomponente", _idKomponente = clsKomponente.MerrIdKomponenteSipasEmrit(_komponente));
                string kodniveli = "LDAB";
                hfState.Set("kodNiveli", kodniveli);

                ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(IdPerdoruesi, IdNdermarrja, cmbKonfigurimi, 173, kodniveli, rm, ci, IdGjuha);
                mbushHiddenFieldMePerkthime();
                cmbKonfigurimi.SelectedIndex = 0;
                DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()), IdGjuha);
                hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;

                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();

                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaDokumenta = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaDokumenta.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, _komponente);
                hfTeDrejtaGjitheDok.Value = tedrejtaDokumenta.DGjitheDok.ToString();

                mbushGridNgaDB(false, Convert.ToBoolean(hfTeDrejtaGjitheDok.Value));
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(IdNdermarrja, gv_dokAlokimBuxheti.ID, gv_dokAlokimBuxheti, cmbKonfigurimi.Text.Split(';')[0], _idKomponente.ToString(), IdGjuha, true);

            }
            else
            {
                mbushGridNgaDB(true, Convert.ToBoolean(hfTeDrejtaGjitheDok.Value));
            }
            clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, IdNdermarrja, "gv_dokAlokimBuxheti", int.Parse(cmbKonfigurimi.Value.ToString()), "B_RegjistrimAlokimBuxheti.aspx");
            konfiguroGride(_komponente);
            if (String.IsNullOrEmpty(gv_dokAlokimBuxheti.FilterExpression) && !IsPostBack)
                gv_dokAlokimBuxheti.FilterExpression = filterExpressionDefault;
            Container.Attributes["src"] = "";
            gv_dokAlokimBuxheti.PercaktoTitlePanel(this, _menuInfo, _pnlMesazhi, hfState, IdPerdoruesi, IdNdermarrja, IdViti, IdGjuha, int.Parse(cmbKonfigurimi.Value.ToString()), _komponente, rm, ci, false);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }

        #region Menu

        protected void PercaktoTemplateMenu(ASPxMenu menu, ASPxMenu menuInfo)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            clsToolbarConfig.percaktoTemplateMenu(IdGjuha, IdViti, IdPerdoruesi, IdNdermarrja, menu, _komponente, this, menuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, null, null, true, true, false, Meme, false);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }

        protected void Menu_ItemClick(object source, MenuItemEventArgs e)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            switch (e.Item.Name)
            {
                case "PrintPreview": 
                    if (gv_dokAlokimBuxheti.FocusedRowIndex == -1)
                    {
                        clsMenuInfo.ShtoMesazh(_menuInfo, new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, MessagesResource.Messages["regjMagMesazhZgjidhniFaturePerPrintim"]), _pnlMesazhi);
                        Container.Attributes["src"] = ""; 
                    }
                    else
                    {
                        string id = gv_dokAlokimBuxheti.GetRowValues(gv_dokAlokimBuxheti.FocusedRowIndex, "IdBuxhetiKoka").ToString();
                        int IdKokaAlokimi = int.Parse(id);
                        int idRapDesign = ClsBKokaBuxheti.ktheIdRaportDesign(IdKokaAlokimi);
                        if (idRapDesign == 0)
                        {
                            clsMenuInfo.ShtoMesazh(_menuInfo, new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, MessagesResource.Messages["regjShitjeMesazhSkaFormatPerPrintim"]), _pnlMesazhi);
                            Container.Attributes["src"] = ""; 
                        }
                        else
                        {
                            int idRaporti = DbCore.DbShare.clsRaporti.KtheIdRaporti(IdGjuha, idRapDesign);
                            Container.Attributes["src"] = "RaportiShpejte.aspx?Sesioni=false&idraporti=" + idRaporti + "&idDokumenti=" + IdKokaAlokimi + "&printo=false&raportdyte=jo&iddesign=" + idRapDesign;
                            konfiguroGride(DbCore.clsFunksione.GetKomponente(Page.Request));
                        }
                    }
                    break;
                default:
                    break;
            }

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }

        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            menu_msg_Frame.RuajFilter(gv_dokAlokimBuxheti, _komponente, int.Parse(cmbKonfigurimi.Value.ToString()), ref hfStatusi);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }

        protected void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            menu_msg_Frame.FshiFilter(gv_dokAlokimBuxheti, _komponente, int.Parse(cmbKonfigurimi.Value.ToString()), ref hfStatusi);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }
        
        #endregion
        
        /// <summary>
        /// Metode qe sherben per te kaluar perkthime te js
        /// </summary>
        private void mbushHiddenFieldMePerkthime()
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            clsFunksione.ShtoPerkthimNeHfState(hfState, "regjisDokZgjidhDokPerTeBashkengjitur", "regjisDokMsgFaturaEshtePrintNeKase", "regjisDokNukKeniAsnjeDokTeZgjedhur", "msgDokTeJeneTeSeNjejtesNenkategori", "msgZgjdhniNjeNgaElementetEListes", "msgNukKeniTeDrejtaNeKeteAmbjent", "labelAdministrimiMsgJeniSigurt", "regjisDokZgjidhniTePakten1Dok", "regjisDokZgjidhniNjeDokument", "hapListenPerMeShumeInfo", "msgRreshtatDeshtuan", "msgJuKeniZgjedhur", "msgAllConverted", "msgReshtaNjeOseDisa", "regjMagMesazhZgjidhniNje");
            konfigurimi_Label.Text = rm.GetString("lblModeli", ci);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }
        
        /// <summary>
        /// Mbush griden me te dhena nga db
        /// </summary>
        private void mbushGridNgaDB(bool ngaSesioni, bool gjitheDokumentat)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, ngaSesioni, gjitheDokumentat);

            var colKokaAlokimi = new ColBKokaBuxheti();
            if (ngaSesioni)
                colKokaAlokimi = mySessionObjects.MerrNgaSession<ColBKokaBuxheti>(Session, _komponente);
            if (!ngaSesioni || colKokaAlokimi == null || !(colKokaAlokimi.Count > 0))
                colKokaAlokimi = new ColBKokaBuxheti(IdNdermarrja, IdViti, IdPerdoruesi, 172, gjitheDokumentat);
            mySessionObjects.RuajNeSession<ColBKokaBuxheti>(Session, colKokaAlokimi, _komponente);
            gv_dokAlokimBuxheti.MbushGride(colKokaAlokimi, Session, GuidString, _komponente);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, ngaSesioni, gjitheDokumentat);
        }

        protected void gv_dokAlokimBuxheti_DataBound(object sender, EventArgs e)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            ASPxGridView grid = (ASPxGridView)sender;
            GridUtil.ShtoCommandColumnNeDatabound(grid, "#", "IdBuxhetiKoka");

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }

        protected void gv_dokAlokimBuxheti_AfterPerformCallback(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            GridUtil.GridAfterPerformCallback(sender, e, gv_dokAlokimBuxheti, _menu);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }

        protected void gv_dokAlokimBuxheti_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            GridUtil.GridCustomCallbackDefault(sender, e, gv_dokAlokimBuxheti, _komponente, IdNdermarrja, IdGjuha, cmbKonfigurimi, ref hfStatusi);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }
        
        protected void gv_dokAlokimBuxheti_CustomJSProperties(object sender, DevExpress.Web.ASPxGridViewClientJSPropertiesEventArgs e)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            e.Properties["cpPageIndex"] = gv_dokAlokimBuxheti.PageIndex;
            e.Properties["cpPageRow"] = gv_dokAlokimBuxheti.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gv_dokAlokimBuxheti.VisibleRowCount;

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }

        protected void gv_dokAlokimBuxheti_HeaderFilterFillItems(object sender, DevExpress.Web.ASPxGridViewHeaderFilterEventArgs e)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            GridUtil.GridHeaderFilterFillItem(sender, e, rm, ci, "Nr Dokumenti");

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }

        private void konfiguroGride(string komponente)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, komponente);

            int idKategoria = 173; 
            KonfigurimComboGride.ShtoModel(gv_dokAlokimBuxheti, idKategoria, IdNdermarrja, IdPerdoruesi, IdGjuha, Session, komponente, GuidString);

            KonfigurimComboGride.ShtoStatusDokBuxheti(gv_dokAlokimBuxheti, rm, ci, idKategoria);

            GridUtil.konfigGrideListeEMadhePaTheme(gv_dokAlokimBuxheti, "IdBuxhetiKoka");

            GridViewDataDateColumn colDtDok = gv_dokAlokimBuxheti.Columns["DtDok"] as GridViewDataDateColumn;
            colDtDok.PropertiesEdit.DisplayFormatString = "dd/MM/yyyy";
            colDtDok.PropertiesDateEdit.EditFormatString = "dd/MM/yyyy";

            if (gv_dokAlokimBuxheti.Columns.Count != 0)
                gv_dokAlokimBuxheti.Columns["#"].VisibleIndex = 0;

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, komponente);
        }
       
    }
}