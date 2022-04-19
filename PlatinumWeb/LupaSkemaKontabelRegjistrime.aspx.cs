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
            //if (Request.QueryString.ToString() == "")
            //{
            //colSkema = dbKontabiliteti.merrGjitheSkemaKontabelRegjistrim(funk.ktheNdermarrjeVit());
            gvLupaSkemaKontRegj.DataSource = colSkema;
            gvLupaSkemaKontRegj.DataBind();
            //}
            //else
            //{
            //    if (int.Parse(Request.QueryString["default"]) == 1)
            //    {
            //        colSkema = dbKontabiliteti.merrGjitheSkematKontabelDefault(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            //    }
            //    else
            //    {
            //        DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();
            //        oPerdorues = (DbCore.DbAdmin.clsPerdorues)(CacheLayer.GlobalCacheManager.MySessionCache["oClsPerdoruesi"]);
            //        colSkema = dbKontabiliteti.merrGjitheSkematKontabelAndAutorizim(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), oPerdorues.IdPerdorues);
            //    }
            //    gvLupaSkemaKontRegj.DataSource = colSkema;
            //    gvLupaSkemaKontRegj.DataBind();
            //}
        }

        private void konfiguroPopupGride()
        {//konfiguron popupgriden
            GridUtil.percaktoVisibleColumns(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), gvLupaSkemaKontRegj, "gvLupaSkemaKontRegj", "LupaSkemaKontabelRegjistrime.aspx");
            //funk.konfiguroGrideListeMadhePopupi(gvLupaSkemaKontRegj, "IdSkemeKont");
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
