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
using System.ComponentModel;
using DbCore.IMBUtils.Messages;
using Newtonsoft.Json;
using DbCore.IMBUtils.Logging;

namespace PlatinumWeb
{
    public partial class B_LlojeBuxheti : MyPageBase
    {
        private int _idKomponente;
        private string _komponente = "B_LlojeBuxheti.aspx";

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
                PercaktoTeDrejtaNeHiddenField();
                hfState.Set("_idKomponente", _idKomponente = clsKomponente.MerrIdKomponenteSipasEmrit(_komponente));
                KonfiguroVleraFillestare();
                MbushGrideNgaDb(false);
                GridUtil.percaktoVisibleColumnsMeWidth(IdGjuha, IdNdermarrja, gvLlojBuxheti, gvLlojBuxheti.ID, _komponente);
            }
            else
            {
                MbushGrideNgaDb(true);
            }

            clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, IdNdermarrja, gvLlojBuxheti.ID, 1, _komponente);
            GridUtil.konfigGrideListeEMadhePaTheme(gvLlojBuxheti, "IdLlojBuxheti");
            GridUtil.konfiguroGrideListeEvogelPaTheme(gvLlojBuxheti, "IdLlojBuxheti");
            DbCore.mySessionObjects.ruajMesazhNeSesion(Session, ":");

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }

        #region menu
        protected void PercaktoTemplateMenu(ASPxMenu menu, ASPxMenu menuInfo)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            clsToolbarConfig.percaktoTemplateMenu(IdGjuha, IdViti, IdPerdoruesi, IdNdermarrja, menu, _komponente, this, menuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, null, null, hfRuaj.Value == "Ruaj", true, false, Meme, false);
            _menu.Items.FindByName("Anullo").Text = MessagesResource.Messages["MenuItemMbyll"];

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }
        protected void Menu_ItemClick(object source, MenuItemEventArgs e)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            switch (e.Item.Name)
            {
                case "Fshi":
                    Fshi();
                    break;
            }

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }
        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            menu_msg_Frame.RuajFilter(gvLlojBuxheti, _komponente, 1, ref hfStatusi);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }
        protected void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            menu_msg_Frame.FshiFilter(gvLlojBuxheti, _komponente, 1, ref hfStatusi);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }

        private ClsBLlojBuxheti KrijoLlojBuxheti()
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            ASPxTextBox txtKodi = (ASPxTextBox)gvLlojBuxheti.FindEditFormTemplateControl("txtKodi");
            ASPxTextBox txtPershkrimi = (ASPxTextBox)gvLlojBuxheti.FindEditFormTemplateControl("txtPershkrimi");
            var kodi = txtKodi.Value != null ? txtKodi.Value.ToString() : "";
            var pershkrimi = txtPershkrimi.Value != null ? txtPershkrimi.Value.ToString() : "";

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
            return new ClsBLlojBuxheti(MessagesResource.Messages, 0, kodi, pershkrimi, IdNdermarrja);
        }
        #endregion

        #region vleraFillestare
        protected void PercaktoTeDrejtaNeHiddenField()
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            var tedrejtaInfo = new clsTeDrejtaRoli();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, clsFunksione.GetKomponente(Page.Request));
            hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
            hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
            hfTeDrejta.Add("Fshi", tedrejtaInfo.DFsh);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }

        protected void KonfiguroVleraFillestare()
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            hfState.Set("_idKomponente", _idKomponente = clsKomponente.MerrIdKomponenteSipasEmrit(_komponente));
            mbushHiddenFieldMePerkthime();

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }
        #endregion

        #region gvLlojeBuxhetimi
        protected void gvLlojBuxheti_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e) =>
            GridUtil.GridHeaderFilterFillItem(sender, e, rm, ci, "Kodi", "Pershkrimi");

        protected void gvLlojBuxheti_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e) {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            GridUtil.GridCustomCallbackDefault(sender, e, gvLlojBuxheti, _komponente, IdNdermarrja, IdGjuha, null, ref hfStatusi);
            var arr = e.Parameters.Split(';');
            if(arr.Length == 1 && arr[0] == "fshi")
                gvLlojBuxheti.JSProperties["cpEditedLlojeBuxheti"] = hfState.Get("editedLlojeBuxheti");

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }

        protected void gvLlojBuxheti_DataBound(object sender, EventArgs e) =>
            GridUtil.ShtoCommandColumnNeDatabound(gvLlojBuxheti, "#", "IdLlojBuxheti");

        protected void gvLlojBuxheti_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e) =>
            GridUtil.GridCustomJsProperties(sender, e, gvLlojBuxheti);

        protected void gvLlojBuxheti_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e) =>
            GridUtil.GridAfterPerformCallback(sender, e, gvLlojBuxheti, _menu);
        
        protected void gvLlojBuxheti_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            for (int i = 0; i < e.NewValues.Count; i++)
            {
                if (e.NewValues[i] == null)
                {
                    e.Cancel = true;
                    ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
                    return;
                }
            }

            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), _komponente);
            if (!tedrejtaInfo.DShtim)
            {
                gvLlojBuxheti.ShtoMesazhErrori(MessagesResource.Messages["msgNukKeniTeDrejta"]);
                e.Cancel = true;
                ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
                return;
            }

            var colLlojBuxheti = (ColBLlojBuxheti)gvLlojBuxheti.DataSource;
            var llojBuxheti = KrijoLlojBuxheti();
            llojBuxheti.IdKrijuesi = IdPerdoruesi;
            llojBuxheti.SetIntegroBuxhetTeNdermBija(true);
            var mesazh = llojBuxheti.Ruaj();

            hfStatusi.Value = mesazh.Status.ToString().ToLower();

            gvLlojBuxheti.ShtoMesazhNeGride(mesazh);

            if (!mesazh.Status)
            {
                e.Cancel = true;
                ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
                return;
            }
            else
                gvLlojBuxheti.JSProperties["cpEditedLlojeBuxheti"] = JsonConvert.SerializeObject(new { llojeBuxheti = new ColBLlojBuxheti(llojBuxheti), veprimi = "shto" });

            colLlojBuxheti.AddIfNotExists(llojBuxheti);
            mySessionObjects.RuajNeSession<ColBLlojBuxheti>(Session, colLlojBuxheti, gvLlojBuxheti.ID);
            CancelEditing(e);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }

        protected void gvLlojBuxheti_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            for (int i = 0; i < e.NewValues.Count; i++)
            {
                if (e.NewValues[i] == null)
                {
                    e.Cancel = true;
                    ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
                    return;
                }
            }

            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), _komponente);
            if (!tedrejtaInfo.DMod)
            {
                gvLlojBuxheti.ShtoMesazhErrori(MessagesResource.Messages["msgNukKeniTeDrejta"]);
                e.Cancel = true;
                ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
                return;
            }

            var colLlojBuxheti = (ColBLlojBuxheti)gvLlojBuxheti.DataSource;
            var llojBuxheti = KrijoLlojBuxheti();
            llojBuxheti.IdLlojBuxheti = Convert.ToInt32(e.Keys["IdLlojBuxheti"]);
            llojBuxheti.IdModifikuesi = IdPerdoruesi;
            llojBuxheti.SetIntegroBuxhetTeNdermBija(true);
            var mesazh = llojBuxheti.Modifiko();

            hfStatusi.Value = mesazh.Status.ToString().ToLower();

            gvLlojBuxheti.ShtoMesazhNeGride(mesazh);

            if (!mesazh.Status)
            {
                e.Cancel = true;
                ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
                return;
            }
            else
                gvLlojBuxheti.JSProperties["cpEditedLlojeBuxheti"] = JsonConvert.SerializeObject(new { llojeBuxheti = new ColBLlojBuxheti(llojBuxheti), veprimi = "shto" });

            colLlojBuxheti.FindAndRemove(x => x.IdLlojBuxheti == llojBuxheti.IdLlojBuxheti);
            colLlojBuxheti.AddIfNotExists(llojBuxheti);
            mySessionObjects.RuajNeSession<ColBLlojBuxheti>(Session, colLlojBuxheti, gvLlojBuxheti.ID);

            CancelEditing(e);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }

        protected void gvLlojBuxheti_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            if (e.RowType == GridViewRowType.EditForm)
            {
                ASPxGridView gridView = (ASPxGridView)sender;
                var shto = gridView.IsNewRowEditing;
                //Kodi
                ASPxTextBox txtBox = (ASPxTextBox)gridView.FindEditFormTemplateControl("txtKodi");
                if (txtBox.Text == "" && !shto)
                {
                    txtBox.Text = e.GetValue("Kodi").ToString();
                    txtBox.Enabled = false;
                }
                //Pershkrimi
                txtBox = (ASPxTextBox)gridView.FindEditFormTemplateControl("txtPershkrimi");
                if (txtBox.Text == "" && !shto)
                {
                    txtBox.Text = e.GetValue("Pershkrimi").ToString();
                }
            }

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }
        protected void CancelEditing(CancelEventArgs e)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            e.Cancel = true;
            gvLlojBuxheti.CancelEdit();

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }
        public void MbushGrideNgaDb(bool ngaSesioni)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, ngaSesioni);

            var colLlojBuxheti = new ColBLlojBuxheti();
            if (ngaSesioni)
                colLlojBuxheti = mySessionObjects.MerrNgaSession<ColBLlojBuxheti>(Session, gvLlojBuxheti.ID);
            if (!ngaSesioni || colLlojBuxheti == null || !(colLlojBuxheti.Count > 0))
            {
                var idNdermarrjeBuxheti = !(String.IsNullOrEmpty(Request.QueryString["idNdermarrje"])) ? Convert.ToInt32(Request.QueryString["idNdermarrje"]) : IdNdermarrja;
                var idLlogaria = !(String.IsNullOrEmpty(Request.QueryString["idLlogaria"])) ? Convert.ToInt32(Request.QueryString["idLlogaria"]) : 0;
                if(idLlogaria > 0)
                    colLlojBuxheti = ColBLlojBuxheti.KtheSipasNdermarrjesDheLlogarise(idNdermarrjeBuxheti, idLlogaria);
                else
                    colLlojBuxheti = ColBLlojBuxheti.KtheSipasNdermarrjes(idNdermarrjeBuxheti);
            }
            mySessionObjects.RuajNeSession<ColBLlojBuxheti>(Session, colLlojBuxheti, gvLlojBuxheti.ID);
            gvLlojBuxheti.MbushGride(colLlojBuxheti, Session, GuidString, _komponente);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, ngaSesioni);
        }
        #endregion

        private void Fshi()
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), _komponente);
            if (!tedrejtaInfo.DFsh)
            {
                clsMenuInfo.ShtoMesazh(_menu, new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, MessagesResource.Messages["msgNukKeniTeDrejta"]), _pnlMesazhi);
                return;
            }

            clsMesazh mesazh;
            var rreshtat = new List<object>();
            List<string> llojeTeFshira = new List<string>(); List<string> llojeTePaFshira = new List<string>();
            var colBuxheteTeFshira = new ColBLlojBuxheti();
            var colBLlojBuxheti = (ColBLlojBuxheti)gvLlojBuxheti.DataSource;

            rreshtat = gvLlojBuxheti.GetSelectedFieldValues("IdLlojBuxheti");

            var llojBuxheti = new ClsBLlojBuxheti(MessagesResource.Messages);
            for (int i = 0; i < rreshtat.Count; i++)
            {
                llojBuxheti = new ClsBLlojBuxheti(MessagesResource.Messages, Convert.ToInt32(rreshtat[i]));
                llojBuxheti.SetIntegroBuxhetTeNdermBija(true);
                mesazh = llojBuxheti.Fshi();

                if (!mesazh.Status)
                {
                    llojeTePaFshira.Add(llojBuxheti.Kodi);
                    continue;
                }
                colBuxheteTeFshira.Add(llojBuxheti);
                llojeTeFshira.Add(llojBuxheti.Kodi);
                colBLlojBuxheti.FindAndRemove(x => x.IdLlojBuxheti == llojBuxheti.IdLlojBuxheti);
                hfStatusi.Value = "true";
            }
            if (llojeTePaFshira.Count > 0)
                clsMenuInfo.ShtoMesazh(_menuInfo, new MesazhGabimi(ImbLogger.LogWarningBuxhetimi,$"{MessagesResource.Messages["msgLlojeBuxhetiTePaFshira"]} {String.Join(",", llojeTePaFshira.ToArray())}"), _pnlMesazhi);
            if (llojeTeFshira.Count > 0)
                clsMenuInfo.ShtoMesazhSuksesi(_menuInfo, $"{MessagesResource.Messages["msgLlojeBuxhetiTeFshira"]} {String.Join(",", llojeTeFshira.ToArray())}", _pnlMesazhi);

            hfState.Set("editedLlojeBuxheti", JsonConvert.SerializeObject(new { llojeBuxheti = colBuxheteTeFshira, veprimi = "fshi" }));

            mySessionObjects.RuajNeSession<ColBLlojBuxheti>(Session, colBLlojBuxheti, gvLlojBuxheti.ID);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }

        #region perkthime
        private void mbushHiddenFieldMePerkthime()
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            hfState.Set("msgLlojeBuxhetiZgjidh", MessagesResource.Messages["msgLlojeBuxhetiZgjidh"]);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }
        #endregion
    }
}