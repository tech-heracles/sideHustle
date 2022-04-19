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
    public partial class B_RegjistrimBuxheti : MyPageBase
    {
        private string lloji;
        private int _idKomponente;
        private int idkatdok;
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
                lloji = Request.QueryString["lloji"];
                hfState.Set("lloji", lloji);
                hfState.Set("_idKomponente", _idKomponente = clsKomponente.MerrIdKomponenteSipasEmrit(_komponente));

                string kodniveli = "";
                switch (lloji)//TODO nje metode qe te marresh te duhuren jo me case
                {
                    case "miratim":
                        kodniveli = "LDMB";
                        idkatdok = 169;
                        break;
                    case "planifikim":
                        kodniveli = "LDPB";
                        idkatdok = 169;
                        break;
                    case "alokim":
                        kodniveli = "LDAB";
                        idkatdok = 173;
                        break;
                    case "rialokim":
                        kodniveli = "LDRB";
                        idkatdok = 174;
                        break;
                    case "perfitim":
                        kodniveli = "LDPBV";
                        idkatdok = 176;
                        break;
                    case "planifikimEkzekutimi":
                        kodniveli = "LDPEB";
                        idkatdok = 178;
                        break;
                    case "ekzekutim":
                        kodniveli = "LDEB";
                        idkatdok = 180;
                        break;
                    default:
                        kodniveli = "LDMB";
                        idkatdok = 169;
                        break;
                }
                hfState.Set("kodNiveli", kodniveli);
                ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(IdPerdoruesi, IdNdermarrja, cmbKonfigurimi, idkatdok, kodniveli, rm, ci, IdGjuha);
                mbushHiddenFieldMePerkthime();
                cmbKonfigurimi.SelectedIndex = 0;
                DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()), IdGjuha);
                hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;

                if(Request.QueryString["lloji"].ContainsAnyIgnoreCase("rialokim", "perfitim", "planifikimEkzekutimi", "ekzekutim"))
                {
                    colTeDrejtaRoli teDrejtaNiveleRregjistrimi = new colTeDrejtaRoli();
                    teDrejtaNiveleRregjistrimi.mbushTeDrejtaPerdoruesiNiveleRegjistrimiPerKomponente(IdPerdoruesi, IdNdermarrja, IdViti, _komponente);
                    hfState.Set("teDrejtaNivele", Newtonsoft.Json.JsonConvert.SerializeObject(teDrejtaNiveleRregjistrimi));
                }

                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();

                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaDokumenta = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaDokumenta.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, _komponente);
                hfTeDrejtaGjitheDok.Value = tedrejtaDokumenta.DGjitheDok.ToString();

                mbushGrid(Convert.ToBoolean(hfTeDrejtaGjitheDok.Value), false);
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(IdNdermarrja, gv_RegjistrimBuxheti.ID, gv_RegjistrimBuxheti, cmbKonfigurimi.Text.Split(';')[0], _idKomponente.ToString(), IdGjuha, true, idkatdok, true);
            }
            else
            {
                lloji = hfState.Get("lloji").ToString();//TODO check if needed
                mbushGrid(Convert.ToBoolean(hfTeDrejtaGjitheDok.Value), true); 
            }
            clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, IdNdermarrja, "gv_RegjistrimBuxheti", int.Parse(cmbKonfigurimi.Value.ToString()), "B_RegjistrimBuxheti.aspx?lloji=" + lloji);
            konfiguroGride(_komponente);

            if (String.IsNullOrEmpty(gv_RegjistrimBuxheti.FilterExpression) && !IsPostBack)
                gv_RegjistrimBuxheti.FilterExpression = filterExpressionDefault;
            Container.Attributes["src"] = "";
            gv_RegjistrimBuxheti.PercaktoTitlePanel(this, _menuInfo, _pnlMesazhi, hfState, IdPerdoruesi, IdNdermarrja, IdViti, IdGjuha, int.Parse(cmbKonfigurimi.Value.ToString()), _komponente, rm, ci, false);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }

        #region Menu

        protected void PercaktoTemplateMenu(ASPxMenu menu, ASPxMenu menuInfo)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            clsToolbarConfig.percaktoTemplateMenu(IdGjuha, IdViti, IdPerdoruesi, IdNdermarrja, menu, _komponente, this, menuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, null, null, true, true, false, Meme, false);
            if(_menu.Items.FindByName("Pezullo") != null)
                _menu.Items.FindByName("Pezullo").Text = DbCore.IMBUtils.Messages.MessagesResource.Messages["MenuItemRefuzo"];

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }

        protected void Menu_ItemClick(object source, MenuItemEventArgs e)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            switch (e.Item.Name)
            {
                case "PrintPreview": 
                    if (gv_RegjistrimBuxheti.FocusedRowIndex == -1)
                    {
                        clsMenuInfo.ShtoMesazh(_menuInfo, new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, MessagesResource.Messages["regjMagMesazhZgjidhniFaturePerPrintim"]), _pnlMesazhi);
                        Container.Attributes["src"] = ""; 
                    }
                    else
                    {
                        string id = gv_RegjistrimBuxheti.GetRowValues(gv_RegjistrimBuxheti.FocusedRowIndex, "IdBuxhetiKoka").ToString();
                        int idBuxhetiKoka = int.Parse(id);
                        int idRapDesign = ClsBKokaBuxheti.ktheIdRaportDesign(idBuxhetiKoka);
                        if (idRapDesign == 0)
                        {
                            clsMenuInfo.ShtoMesazh(_menuInfo, new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, MessagesResource.Messages["regjShitjeMesazhSkaFormatPerPrintim"]), _pnlMesazhi);
                            Container.Attributes["src"] = ""; 
                        }
                        else
                        {
                            int idRaporti = DbCore.DbShare.clsRaporti.KtheIdRaporti(IdGjuha, idRapDesign);
                            Container.Attributes["src"] = "RaportiShpejte.aspx?Sesioni=false&idraporti=" + idRaporti + "&idDokumenti=" + idBuxhetiKoka + "&printo=false&raportdyte=jo&iddesign=" + idRapDesign;
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

            menu_msg_Frame.RuajFilter(gv_RegjistrimBuxheti, _komponente, int.Parse(cmbKonfigurimi.Value.ToString()), ref hfStatusi);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }

        protected void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            menu_msg_Frame.FshiFilter(gv_RegjistrimBuxheti, _komponente, int.Parse(cmbKonfigurimi.Value.ToString()), ref hfStatusi);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }
        
        #endregion


        /// <summary>
        /// Metode qe sherben per te kaluar perkthime te js
        /// </summary>
        /// <param name="cultinf">Merr culture info</param>
        /// <param name="rm">Merr Resource Manager</param>
        private void mbushHiddenFieldMePerkthime()
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            clsFunksione.ShtoPerkthimNeHfState(hfState, "regjisDokZgjidhniTePakten1DokPerKonvertim", "regjisDokZgjidhDokPerTeBashkengjitur", "regjisDokMsgFaturaEshtePrintNeKase", "regjisDokNukKeniAsnjeDokTeZgjedhur", "msgDokTeJeneTeSeNjejtesNenkategori", "msgZgjdhniNjeNgaElementetEListes", "msgNukKeniTeDrejtaNeKeteAmbjent", "labelAdministrimiMsgJeniSigurt", "regjisDokZgjidhniTePakten1Dok", "regjisDokZgjidhniNjeDokument", "msgDokNukMundTeKonvertohet", "hapListenPerMeShumeInfo", "msgRreshtatDeshtuan", "msgJuKeniZgjedhur", "msgAllConverted", "msgUKonvertua", "msgRreshta", "msgReshtaNjeOseDisa", "regjMagMesazhZgjidhniNje");
            konfigurimi_Label.Text = rm.GetString("lblModeli", ci);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }
        
        /// <summary>
        /// Mbush griden me te dhena
        /// </summary>
        /// <param name="gjitheDokumentat"></param>
        /// <param name="ngaSesioni"></param>
        private void mbushGrid(bool gjitheDokumentat, bool ngaSesioni)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, gjitheDokumentat, ngaSesioni);

            var lloji = Request.QueryString["lloji"];
            int idKatDokShtoRegj = (lloji == "miratim") ? 170 : (lloji == "planifikim") ? 171 : (lloji == "alokim") ? 172 : (lloji == "rialokim") ? 175 : (lloji == "planifikimEkzekutimi") ? 179 : (lloji == "ekzekutim") ? 181 : 177; //177 per (lloji=="perfitim")
            var colKokaBuxheti = new ColBKokaBuxheti();
            if (ngaSesioni)
                colKokaBuxheti = mySessionObjects.MerrNgaSession<ColBKokaBuxheti>(Session, _komponente);
            if (!ngaSesioni || colKokaBuxheti == null || !(colKokaBuxheti.Count > 0))
                colKokaBuxheti = new ColBKokaBuxheti(IdNdermarrja, IdViti, IdPerdoruesi, idKatDokShtoRegj, gjitheDokumentat);
            mySessionObjects.RuajNeSession<ColBKokaBuxheti>(Session, colKokaBuxheti, _komponente);
            gv_RegjistrimBuxheti.MbushGride(colKokaBuxheti, Session, GuidString, _komponente);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, gjitheDokumentat, ngaSesioni);
        }

        protected void gv_RegjistrimBuxheti_DataBound(object sender, EventArgs e)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            ASPxGridView grid = (ASPxGridView)sender;
            GridUtil.ShtoCommandColumnNeDatabound(grid, "#", "IdBuxhetiKoka");

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }

        protected void gv_RegjistrimBuxheti_AfterPerformCallback(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            GridUtil.GridAfterPerformCallback(sender, e, gv_RegjistrimBuxheti, _menu);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }

        protected void gv_RegjistrimBuxheti_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            GridUtil.GridCustomCallbackDefault(sender, e, gv_RegjistrimBuxheti, _komponente, IdNdermarrja, IdGjuha, cmbKonfigurimi, ref hfStatusi);
            var arr = e.Parameters.Split(';');
            if (arr.Length == 1 && arr[0] == "true")
                mbushGrid(Convert.ToBoolean(hfTeDrejtaGjitheDok.Value), false);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }
        
        protected void gv_RegjistrimBuxheti_CustomJSProperties(object sender, DevExpress.Web.ASPxGridViewClientJSPropertiesEventArgs e)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            e.Properties["cpPageIndex"] = gv_RegjistrimBuxheti.PageIndex;
            e.Properties["cpPageRow"] = gv_RegjistrimBuxheti.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gv_RegjistrimBuxheti.VisibleRowCount;

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }

        protected void gv_RegjistrimBuxheti_HeaderFilterFillItems(object sender, DevExpress.Web.ASPxGridViewHeaderFilterEventArgs e)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            GridUtil.GridHeaderFilterFillItem(sender, e, rm, ci, "Nr Dokumenti");

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }

        /// <summary>
        /// Konfiguron griden
        /// </summary>
        /// <param name="komponente"></param>
        /// <param name="veprimi"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="idPerdoruesi"></param>
        /// <param name="idGjuha"></param>
        /// <param name="rm"></param>
        /// <param name="ci"></param>
        private void konfiguroGride(string komponente)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, komponente);

            int idKatDokShtoRegj = (lloji == "miratim") ? 170 : (lloji == "planifikim") ? 171 : (lloji == "alokim") ? 172 : (lloji == "rialokim") ? 175 : (lloji == "planifikimEkzekutimi") ? 179 : (lloji == "ekzekutim") ? 181 : 177; //177 per (lloji=="perfitim")

            KonfigurimComboGride.ShtoModel(gv_RegjistrimBuxheti, idKatDokShtoRegj, IdNdermarrja, IdPerdoruesi, IdGjuha, Session, komponente, GuidString);
            KonfigurimComboGride.ShtoStatusDokBuxheti(gv_RegjistrimBuxheti, rm, ci, idKatDokShtoRegj);
            if(idKatDokShtoRegj == 177 || idKatDokShtoRegj == 181)
                KonfigurimComboGride.ShtoLlojVeprimiDokBuxheti(gv_RegjistrimBuxheti, idKatDokShtoRegj);
            if(idKatDokShtoRegj == 179)
                KonfigurimComboGride.ShtoStatusAprovimi(gv_RegjistrimBuxheti, rm, ci);

            GridUtil.konfigGrideListeEMadhePaTheme(gv_RegjistrimBuxheti, "IdBuxhetiKoka");

            konfiguroKolonaDate("DtDok", "DtKrijimi", "DtModifikimi");

            if (idKatDokShtoRegj == 177)
                KonfigurimComboGride.ShtoDoktor(gv_RegjistrimBuxheti, IdNdermarrja, Session, komponente, GuidString);

            if (gv_RegjistrimBuxheti.Columns.Count != 0)
                gv_RegjistrimBuxheti.Columns["#"].VisibleIndex = 0;

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, komponente);
        }

        private void konfiguroKolonaDate(params string[] fushat)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, fushat);

            GridViewDataDateColumn colDtDok = new GridViewDataDateColumn();
            for(int i =0; i<fushat.Length; i++)
            {
                colDtDok = gv_RegjistrimBuxheti.Columns[fushat[i]] as GridViewDataDateColumn;
                colDtDok.PropertiesEdit.DisplayFormatString = "dd/MM/yyyy";
                colDtDok.PropertiesDateEdit.EditFormatString = "dd/MM/yyyy";
            }

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, fushat);
        }
       
    }
}