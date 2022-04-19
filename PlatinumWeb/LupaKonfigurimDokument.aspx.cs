using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Collections;
using System.Data;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class LupaKonfigurimDokument : MyPageBase
    {

        //private String veprimi;
        protected void Page_Load(object sender, EventArgs e)
        {
            mbushPopUpListe();
            gvLupaKonfigDok.Columns["#"].VisibleIndex = 0;
        }

        private void mbushPopUpListe()
        {
            DbCore.DbShare.colKonfigurimAmbjenti col = new DbCore.DbShare.colKonfigurimAmbjenti();
            //idkatdok per Kategorine Lupa eshte 9
         //   col.mbushKonfigurimKategori(9, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            //col = dbShare.ktheKonfigurimKategori(9, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session),DbCore.mySessionObjects.ktheIdPerdoruesi(Session));                
            col.mbushKonfigurimSuperKategori(3, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            gvLupaKonfigDok.DataSource = col;
            gvLupaKonfigDok.DataBind();
            konfiguroPopupGride();
        }

        private void konfiguroPopupGride()
        {
            GridUtil.percaktoVisibleColumns(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), gvLupaKonfigDok, "gvLupaKonfigDok", "LupaKonfigurimDokument.aspx");
            //funk.konfiguroGrideListeMadhePopupi(gvLupaKonfigDok, "IdKonfigAmbjente");
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            GridUtil.KonfiguroGrideListeMadhePopupiPaTheme(gvLupaKonfigDok, "IdKonfigAmbjente", true, false);
        }

        protected void gvLupaKonfigDok_DataBound(object sender, EventArgs e)
        {
            if (this.gvLupaKonfigDok.Columns["#"] == null)
            {
                DevExpress.Web.GridViewCommandColumn check = new DevExpress.Web.GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true; check.Width = Unit.Percentage(2);
                check.SetColVisibleIndex(0);
                gvLupaKonfigDok.Columns.Add(check);
                gvLupaKonfigDok.SettingsBehavior.AllowSelectByRowClick = true;
            }
            gvLupaKonfigDok.KeyFieldName = "IdKonfigAmbjente";
            gvLupaKonfigDok.Settings.ShowFilterRow = true;
            gvLupaKonfigDok.SettingsBehavior.AllowSelectByRowClick = true;
        }

        protected void gvLupaKonfigDok_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            // gvLupaKonfigDok.Selection.UnselectAll();
        }
    }
}