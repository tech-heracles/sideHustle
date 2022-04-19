using System;
using System.Collections.Generic;
using System.Linq;
using DbCore.IMBUtils.Messages;
using DevExpress.Web;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class LupaGrupKomponente : MyPageBase
    {
        private int idndermarje, idperdoruesi, idnderviti, idviti;
        protected void Page_Load(object sender, EventArgs e)
        {
            idndermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            idperdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            idnderviti = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
            idviti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            percaktoTemplateMenu(ASPxMenu1, idviti, idperdoruesi, idndermarje);

            if (!Page.IsPostBack)
            {
                //Session.Add("mesazh", ":Green");
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, ":");
                mbushPopUpListeGrupe();
                konfiguroPopupGride();
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "LupaGrupKomponente.aspx");
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                if (!tedrejtaInfo.DShtim)
                    gvGrup.CancelEdit();
            }
            DbCore.mySessionObjects.ruajMesazhNeSesion(Session, ":");
        }
        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        /// <param name="idViti"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNermarrje"></param>
        private void percaktoTemplateMenu(ASPxMenu aSPxMenu1, int idViti, int idPerdorues, int idNdermarrje)
        {
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, "LupaGrupKomponente.aspx", this, MenuInfo, true, true, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
        }


        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));

        }

        /// <summary>
        /// mbush griden me te dhena
        /// </summary>
        private void mbushPopUpListeGrupe()
        {//mbush griden e popupit me te dhena
            DbCore.DbListPagesat.colGrupKomponente col = new DbCore.DbListPagesat.colGrupKomponente (idndermarje);
            gvGrup.DataSource = col;
            gvGrup.DataBind();
        }

        /// <summary>
        /// konfiguron popupgriden
        /// </summary>
        private void konfiguroPopupGride()
        {

            GridUtil.percaktoVisibleColumns(DbCore.mySessionObjects.ktheGjuhe(Session), idndermarje, gvGrup, "gvGrup", "LupaGrupKomponente.aspx");
            //funk.konfiguroGrideListeEvogelPopupi(gvGrup, "IdGrupPunonjesish");

            mbushPopUpListeGrupe();
        }

        /// <summary>
        /// databoundi i grides
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvGrup_DataBound(object sender, EventArgs e)
        {
            gvGrup.SettingsText.CommandUpdate = "Ruaj";
            gvGrup.KeyFieldName = "Id";
            gvGrup.SettingsBehavior.AllowSelectByRowClick = true;
            gvGrup.SettingsBehavior.AllowFocusedRow = true;
        }

        /// <summary>
        ///  kur grida ben callback
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvGrup_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            mbushPopUpListeGrupe();
            gvGrup.Selection.UnselectAll();
        }

        /// <summary>
        /// ruan grupin e ri
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvGrup_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            for (int i = 0; i < e.NewValues.Count; i++)
            {
                if (e.NewValues[i] == null)
                {
                    e.Cancel = true;
                    return;
                }
            }
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idperdoruesi, idndermarje, idviti, "LupaGrupKomponente.aspx");
            if (!tedrejtaInfo.DShtim)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ju nuk keni te drejta per kete veprim!:Red");
                return;
            }
            DbCore.DbListPagesat.clsGrupKomponente grupoverview = new DbCore.DbListPagesat.clsGrupKomponente () { Kodi = e.NewValues["Kodi"].ToString(), Pershkrimi = e.NewValues["Pershkrimi"].ToString(), IdPerdoruesi = idperdoruesi,IdKrijuesi=idperdoruesi, IdNdermarje = idndermarje, IdStatusDok = 1, IdRenditje=0 };
            e.Cancel = true;

            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            mesazh = grupoverview.ruaj();
            if (!mesazh.Status == true)
            {
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, mesazh.PershkrimMesazhi + ":Red");
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            }
            else
            {
                gvGrup.CancelEdit();
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, MessagesResource.Messages["msgRuajtjeMeSuksesGreen"]);
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["mesazhRuajtjeMeSukses"], pnlMesazhi);
                gvGrup.SettingsEditing.Mode = GridViewEditingMode.Inline;
                mbushPopUpListeGrupe();

                gvGrup.AddNewRow();
            }
        }

        /// <summary>
        /// validimi ne jane plotesuar gjithe fushat e detyruara
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvGrup_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {

            foreach (GridViewColumn column in gvGrup.Columns)
            {
                if (column.Visible == true)
                {
                    GridViewDataColumn dataColumn = column as GridViewDataColumn;
                    if (dataColumn == null) continue;

                    if (e.NewValues[dataColumn.FieldName] == null)//validimi per kolonat e detyrueshme
                    {

                        e.Errors[dataColumn] = "Vlera nuk mund te jete null.";
                    }
                }

            }
            if (e.Errors.Count > 0)
            {
                e.RowError = "Ju lutemi, plotesoni te gjitha fushat.";

            }

            if (string.IsNullOrEmpty(e.RowError) && e.Errors.Count > 0)
            {
                e.RowError = "Ju lutemi, korrigjoni te gjithe gabimet.";

            }
            mbushPopUpListeGrupe();
        }

        /// <summary>
        /// kur fillon editimi te behet validimi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvGrup_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            if (!gvGrup.IsNewRowEditing)
            {
                gvGrup.DoRowValidation();
            }
        }

        /// <summary>
        /// modifikimi i rreshtave eksistues
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvGrup_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {//ben modifikimin e nje rreshti

            for (int i = 0; i < e.NewValues.Count; i++)
            {
                if (e.NewValues[i] == null)
                {
                    e.Cancel = true;
                    return;
                }
            }
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idperdoruesi, idndermarje, idviti, "LupaGrupKomponente.aspx");
            if (!tedrejtaInfo.DMod)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ju nuk keni te drejta per kete veprim!:Red");
                return;
            }
            DbCore.DbListPagesat.clsGrupKomponente grup = new DbCore.DbListPagesat.clsGrupKomponente() { Id = int.Parse(e.Keys["Id"].ToString()), Kodi = e.NewValues["Kodi"].ToString(), Pershkrimi = e.NewValues["Pershkrimi"].ToString(), IdPerdoruesi = idperdoruesi, IdStatusDok = 1, IdNdermarje = idndermarje };
            if (grup.Pershkrimi != "" && grup.Kodi != "")
            {
                e.Cancel = true;
                DbCore.clsMesazh mesazh = grup.modifiko();
                if (!mesazh.Status == true)
                {
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, mesazh.PershkrimMesazhi + ":Red");
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                }
                else
                {
                    gvGrup.CancelEdit();
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, MessagesResource.Messages["msgModifikimiMeSuksesGreen"]);
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["msgModifikimiMeSukses"], pnlMesazhi);
                }
            }
            gvGrup.SettingsEditing.Mode = GridViewEditingMode.Inline;
            mbushPopUpListeGrupe();
        }

        /// <summary>
        /// fshin grupin e selektuar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            List<object> rreshtat = gvGrup.GetSelectedFieldValues("Id");

            foreach (int id in rreshtat)
            {
                DbCore.DbListPagesat.clsGrupKomponente clsGrup = new DbCore.DbListPagesat.clsGrupKomponente(id) { IdPerdoruesi = idperdoruesi };
                if (clsGrup.kaKomponente())
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ky grup ka komponente", pnlMesazhi);
                }
                else
                {
                    DbCore.clsMesazh mesazh = clsGrup.fshi();
                    if (mesazh.Status)
                        clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                    else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                    mbushPopUpListeGrupe();
                }
            }
        }

        /// <summary>
        /// veprimet e menuse
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {
            if (e.Item.Name == "Modifiko")
            {
                int indeksi = gvGrup.FocusedRowIndex;
                gvGrup.StartEdit(indeksi);
                gvGrup.SettingsEditing.Mode = GridViewEditingMode.EditForm;

            }
            else if (e.Item.Name == "Pastro")
            {
                gvGrup.SettingsEditing.Mode = GridViewEditingMode.Inline;
                gvGrup.AddNewRow();
            }
            else if (e.Item.Name == "Shto")
            {
                gvGrup.AddNewRow();
                gvGrup.SettingsEditing.Mode = GridViewEditingMode.Inline;
            }
        }
    }
}
