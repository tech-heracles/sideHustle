using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Data;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class LupaViti : MyPageBase
    {
        //private DbCore.DbAdmin.clsDatabaseAdmin dbAdmin;
        //private int veprimi;

        protected void Page_Load(object sender, EventArgs e)
        {
            //dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            if (!IsPostBack)
            {
                mbushPopUpListeNgaDB();
            }
            else
            {
                mbushPopUpListeNgaSession();
            }
            konfiguroPopupGride();
        }

        private void mbushPopUpListeNgaSession()
        {
            Object tmpObject;
            DbCore.mySessionObjects.merrGrideNgaSessioniLupa(Session, out tmpObject);
            if (tmpObject == null)
                mbushPopUpListeNgaDB();
            else
            {
                gvLupaViti.DataSource = tmpObject;
                gvLupaViti.DataBind();
            }
        }

        private void mbushPopUpListeNgaDB()
        {//mbush griden e popupit me te dhena            
            DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();
            //oPerdorues = (DbCore.DbAdmin.clsPerdorues)(CacheLayer.GlobalCacheManager.MySessionCache["oClsPerdoruesi"]);
            oPerdorues = DbCore.mySessionObjects.kthePerdorues(Session);
            DbCore.DbKontabiliteti.colLlogarite colLlog = new DbCore.DbKontabiliteti.colLlogarite();
            DataTable dt = DbCore.DbAdmin.colVitet.merrVitetNdermarjeDT(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            //DbCore.DbInventari.colArtikujt colArtikujt = dbInventari.merrArtikujAktivNdermarrjesAndAutorizime(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), oPerdorues.IdPerdorues);
            DbCore.mySessionObjects.ruajGrideNeSessionLupa(Session, dt);
            gvLupaViti.DataSource = dt;

            //gvLupaArtikull.DataSource = colArtikujt;
            gvLupaViti.DataBind();
            dt.Dispose();
        }
        private void mbushPopUpListe()
        {
            DbCore.DbAdmin.colVitet col = new DbCore.DbAdmin.colVitet();
            DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();
            //oPerdorues = (DbCore.DbAdmin.clsPerdorues)(CacheLayer.GlobalCacheManager.MySessionCache["oClsPerdoruesi"]);
            oPerdorues = DbCore.mySessionObjects.kthePerdorues(Session);
            col.merrGjitheVitetENdermarjes(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            //col = dbAdmin.merrGjitheVitet();
           
            gvLupaViti.DataSource = col;
            gvLupaViti.DataBind();
            konfiguroPopupGride();
        }

        private void konfiguroPopupGride()
        {
            GridUtil.percaktoVisibleColumns(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), gvLupaViti, "gvLupaViti", "LupaViti.aspx");
            //funk.konfiguroGrideListeMadhePopupi(gvLupaViti, "IdViti");
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            GridUtil.KonfiguroGrideListeMadhePopupiPaTheme(gvLupaViti, "IdViti", true, false);
            this.gvLupaViti.Columns["#"].VisibleIndex = 0;

        }

        protected void gvLupaViti_DataBound(object sender, EventArgs e)
        {// shton colonen # per selektim dhe disa karakteristika te grides
            if (this.gvLupaViti.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per 
                //perzgjidh
                DevExpress.Web.GridViewCommandColumn check = new DevExpress.Web.GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true; check.Width = Unit.Percentage(2);
                gvLupaViti.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                gvLupaViti.Settings.ShowFilterRowMenu = true;
              //  check.SetColVisibleIndex(0);
                //behet per te afishuar rreshtin qe do sherbej per filtrim
                gvLupaViti.Settings.ShowFilterRow = true;
                gvLupaViti.Columns.Add(check);

                gvLupaViti.KeyFieldName = "IdViti";
                gvLupaViti.SettingsBehavior.AllowSelectByRowClick = true;
                gvLupaViti.SettingsBehavior.AllowFocusedRow = true;
            }
        }



        protected void gvLupaViti_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            gvLupaViti.Selection.UnselectAll();
        }

    }
}