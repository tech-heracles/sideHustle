using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Collections;
using System.Drawing;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.DbShare;

namespace PlatinumWeb
{
    public partial class LupaSkemaKontabelRegjistrime : MyPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            mbushPopUpListeSkemaKontabelRegjistrime();
            konfiguroPopupGride();
        }

        private void mbushPopUpListeSkemaKontabelRegjistrime()
        {//mbush griden e popupit me te dhena
            DbCore.DbKontabiliteti.colSkemaKontabelNew colSkema = new DbCore.DbKontabiliteti.colSkemaKontabelNew( DbCore.mySessionObjects.ktheNdermarrjeVit(Session));
            gvLupaSkemaKontRegj.DataSource = colSkema;
            gvLupaSkemaKontRegj.DataBind();
            //else
            //    else
        }

        private void konfiguroPopupGride()
        {//konfiguron popupgriden
            GridUtil.percaktoVisibleColumns(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), gvLupaSkemaKontRegj, "gvLupaSkemaKontRegj", "LupaSkemaKontabelRegjistrime.aspx");
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            var endlessScroll = clsAlternativaKushti.getAlternativa(clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod("LP/SkKontRegj", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session)), "ES") == "Po";
            GridUtil.KonfiguroGrideListeMadhePopupiPaTheme(gvLupaSkemaKontRegj, "IdSkemeKont", true, endlessScroll);
        }

        protected void gvLupaSkemaKontRegj_DataBound(object sender, EventArgs e)
        {
            gvLupaSkemaKontRegj.Settings.ShowFilterRow = true;

            gvLupaSkemaKontRegj.KeyFieldName = "IdSkemeKont";
            gvLupaSkemaKontRegj.SettingsBehavior.AllowSelectByRowClick = true;
        }

        protected void gvLupaSkemaKontRegj_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            gvLupaSkemaKontRegj.Selection.UnselectAll();
        }

    }
}
