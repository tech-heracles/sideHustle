using System;
using System.Collections.Generic;
using System.Linq;
using DbCore.DbShare;
using DbCore.IMBUtils.Messages;
using DevExpress.Web;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class LupaTipKontrate : MyPageBase
    {

        private int idndermarje, idperdoruesi, idviti;
        protected void Page_Load(object sender, EventArgs e)
        {
            idndermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            idperdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            idviti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            percaktoTemplateMenu(ASPxMenu1, idviti, idperdoruesi, idndermarje);

            if (!Page.IsPostBack)
            {
                //Session.Add("mesazh", ":Green");
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, ":");
                mbushPopUpListeTipKontrate();
                konfiguroPopupGride();
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                hfGjuha.Value = DbCore.mySessionObjects.ktheGjuhe(Session).ToString();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idperdoruesi, idndermarje, idviti, "LupaTipKontrate.aspx");
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                if (!tedrejtaInfo.DShtim) gvLupaTipKontrate.CancelEdit();
            }

            //if (CacheLayer.GlobalCacheManager.MySessionCache["mesazh"].ToString().Split(':')[1] == "Green")
            //if (DbCore.mySessionObjects.merrMesazhNgaSesioni(Session).Split(':')[1] == "Green")
            //{
            //    //clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, CacheLayer.GlobalCacheManager.MySessionCache["mesazh"].ToString().Split(':')[0], pnlMesazhi);
            //    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, DbCore.mySessionObjects.merrMesazhNgaSesioni(Session).Split(':')[0], pnlMesazhi);
            //}
            //else if (DbCore.mySessionObjects.merrMesazhNgaSesioni(Session).Split(':')[1] == "Red")
            //{
            //    //clsMenuInfo.ShtoMesazhGabimi(MenuInfo, CacheLayer.GlobalCacheManager.MySessionCache["mesazh"].ToString().Split(':')[0], pnlMesazhi);
            //    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, DbCore.mySessionObjects.merrMesazhNgaSesioni(Session).Split(':')[0], pnlMesazhi);
            //}
            //CacheLayer.GlobalCacheManager.MySessionCache["mesazh"] = ":Green";
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
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, "LupaTipKontrate.aspx", this, MenuInfo, true, true, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));


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
        private void mbushPopUpListeTipKontrate()
        {//mbush griden e popupit me te dhena
            DbCore.DbListPagesat.colTipeKontrate col = new DbCore.DbListPagesat.colTipeKontrate(idndermarje);
            gvLupaTipKontrate.DataSource = col;
            gvLupaTipKontrate.DataBind();
        }

        /// <summary>
        /// konfiguron popupgriden
        /// </summary>
        private void konfiguroPopupGride()
        {
            GridUtil.percaktoVisibleColumns(DbCore.mySessionObjects.ktheGjuhe(Session), idndermarje, gvLupaTipKontrate, "gvLupaTipKontrate", "LupaTipKontrate.aspx");
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            GridUtil.konfiguroGrideListeEvogelPopupiPaTheme(gvLupaTipKontrate, "IdTipKontrate", false);
            mbushPopUpListeTipKontrate();
        }

        /// <summary>
        /// databoundi i grides
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvLupaTipKontrate_DataBound(object sender, EventArgs e)
        {
            gvLupaTipKontrate.SettingsText.CommandUpdate = "Ruaj";
            gvLupaTipKontrate.KeyFieldName = "IdTipKontrate";
            gvLupaTipKontrate.SettingsBehavior.AllowSelectByRowClick = true;
            gvLupaTipKontrate.SettingsBehavior.AllowFocusedRow = true;
        }

        /// <summary>
        ///  kur grida ben callback
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvLupaTipKontrate_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            mbushPopUpListeTipKontrate();
            gvLupaTipKontrate.Selection.UnselectAll();
        }

        /// <summary>
        /// ruan grupin e ri
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvLupaTipKontrate_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
           
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idperdoruesi, idndermarje, idviti, "LupaTipKontrate.aspx");
            if (!tedrejtaInfo.DShtim)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ju nuk keni te drejta per kete veprim!:Red");
                return;
            }
            DbCore.DbListPagesat.clsTipeKontrate grupoverview = new DbCore.DbListPagesat.clsTipeKontrate() { Kodi = e.NewValues["Kodi"].ToString(), IdPerdoruesi = idperdoruesi, IdNdermarje = idndermarje, IdStatusDok = 1 , KodiAng = e.NewValues["KodiAng"]==null?"": e.NewValues["KodiAng"].ToString() };
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
                gvLupaTipKontrate.CancelEdit();
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, MessagesResource.Messages["msgRuajtjeMeSuksesGreen"]);
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["mesazhRuajtjeMeSukses"], pnlMesazhi);
                gvLupaTipKontrate.SettingsEditing.Mode = GridViewEditingMode.Inline;
                mbushPopUpListeTipKontrate();

                gvLupaTipKontrate.AddNewRow();
            }



        }

        /// <summary>
        /// validimi ne jane plotesuar gjithe fushat e detyruara
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvLupaTipKontrate_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {

            foreach (GridViewColumn column in gvLupaTipKontrate.Columns)
            {
                if (column.Visible == true)
                {
                    GridViewDataColumn dataColumn = column as GridViewDataColumn;
                    if (dataColumn == null) continue;

                    if (e.NewValues[dataColumn.FieldName] == null && dataColumn.FieldName!="KodiAng")//validimi per kolonat e detyrueshme
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
            mbushPopUpListeTipKontrate();
        }

        /// <summary>
        /// kur fillon editimi te behet validimi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvLupaTipKontrate_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            if (!gvLupaTipKontrate.IsNewRowEditing)
            {
                gvLupaTipKontrate.DoRowValidation();
            }
        }

        /// <summary>
        /// modifikimi i rreshtave eksistues
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvLupaTipKontrate_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {//ben modifikimin e nje rreshti

        
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idperdoruesi, idndermarje, idviti, "LupaTipKontrate.aspx");
            if (!tedrejtaInfo.DShtim)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ju nuk keni te drejta per kete veprim!:Red");
                return;
            }
            DbCore.DbListPagesat.clsTipeKontrate grup = new DbCore.DbListPagesat.clsTipeKontrate() { IdTipKontrate = int.Parse(e.Keys["IdTipKontrate"].ToString()), Kodi = e.NewValues["Kodi"].ToString(), IdPerdoruesi = idperdoruesi, IdStatusDok = 1, IdNdermarje = idndermarje, KodiAng = e.NewValues["KodiAng"] == null ? "" : e.NewValues["KodiAng"].ToString() };
            if (grup.Kodi != "")
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
                    gvLupaTipKontrate.CancelEdit();
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, MessagesResource.Messages["msgModifikimiMeSuksesGreen"]);
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["msgModifikimiMeSukses"], pnlMesazhi);
                }
            }
            gvLupaTipKontrate.SettingsEditing.Mode = GridViewEditingMode.Inline;
            mbushPopUpListeTipKontrate();
        }

        /// <summary>
        /// fshin grupin e selektuar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            List<object> rreshtat = gvLupaTipKontrate.GetSelectedFieldValues("IdTipKontrate");

            foreach (int id in rreshtat)
            {
                DbCore.DbListPagesat.clsTipeKontrate clsTip = new DbCore.DbListPagesat.clsTipeKontrate(id) { IdPerdoruesi = idperdoruesi };
                if (clsTip.kaPunesim())
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ky tip eshte perdorur ne punesime dhe nuk mund te fshihet", pnlMesazhi);
                }
                else
                {
                    DbCore.clsMesazh mesazh = clsTip.fshi();
                    if (mesazh.Status)
                        clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                    else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                    mbushPopUpListeTipKontrate();
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
                int indeksi = gvLupaTipKontrate.FocusedRowIndex;
                gvLupaTipKontrate.StartEdit(indeksi);
                gvLupaTipKontrate.SettingsEditing.Mode = GridViewEditingMode.EditForm;

            }
            else if (e.Item.Name == "Pastro")
            {
                gvLupaTipKontrate.SettingsEditing.Mode = GridViewEditingMode.Inline;
                gvLupaTipKontrate.AddNewRow();
            }
            else if (e.Item.Name == "Shto")
            {
                gvLupaTipKontrate.AddNewRow();
                gvLupaTipKontrate.SettingsEditing.Mode = GridViewEditingMode.Inline;
            }
        }

    }
}
