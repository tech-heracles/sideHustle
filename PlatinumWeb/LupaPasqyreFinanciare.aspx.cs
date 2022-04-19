using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Collections;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.DbShare;

namespace PlatinumWeb
{
    public partial class LupaPasqyreFinanciare : MyPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            mbushPopUpListeKomponentePrind();
            konfiguroPopupGride();
        }

        private void mbushPopUpListeKomponentePrind()
        {
            String[] arrVlerave = Request.QueryString["array"].Split(',');
            String[] a;
            ArrayList l = new ArrayList();
            for (int i = 0; i < arrVlerave.Length; i++)
            {
                a = arrVlerave[i].Split(':');
                if (a.Length > 1)
                    if (!l.Contains(a[1]))
                        l.Add(a[1]);
            }

            
            DbCore.DbKontabiliteti.colTrupPasqyreFinaciare col = new DbCore.DbKontabiliteti.colTrupPasqyreFinaciare();

            DbCore.DbKontabiliteti.clsTrupPasqyreFinanciare t = new DbCore.DbKontabiliteti.clsTrupPasqyreFinanciare(0, 0, "Bosh", "", 0, "", 0,false,"");
            col.Add(t);

            for (int i = 1; i <= l.Count; i++)
            {
                DbCore.DbKontabiliteti.clsTrupPasqyreFinanciare o = new DbCore.DbKontabiliteti.clsTrupPasqyreFinanciare(i, 0, l[i - 1].ToString(), "", 0, "", 0,false,"");
                col.Add(o);
            }

            gvLupaPasqFin.DataSource = col;
            gvLupaPasqFin.DataBind();

            gvLupaPasqFin.Columns["IdTrupi"].Visible = false;
            gvLupaPasqFin.Columns["IdKoka"].Visible = false;
            gvLupaPasqFin.Columns["PrindiZerit"].Visible = false;
            gvLupaPasqFin.Columns["NiveliZerit"].Visible = false;
            gvLupaPasqFin.Columns["LlojiZerit"].Visible = false; 
            gvLupaPasqFin.Columns["ShfaqBij"].Visible = false;
        }

        private void konfiguroPopupGride()
        {
            //DbCore.clsFunksione funk = new DbCore.clsFunksione( DbCore.mySessionObjects.ktheCultureInfo(Session));
            //funk.konfiguroGrideListeMadhePopupi(gvLupaPasqFin, "IdTrupi");
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            var endlessScroll = clsAlternativaKushti.getAlternativa(clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod("LP/PasqFin", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session)), "ES") == "Po";
            GridUtil.KonfiguroGrideListeMadhePopupiPaTheme(gvLupaPasqFin, "IdTrupi", true, endlessScroll);
        }



        protected void gvLupaPasqFin_DataBound(object sender, EventArgs e)
        {
            gvLupaPasqFin.Settings.ShowFilterRow = true;
            gvLupaPasqFin.KeyFieldName = "IdTrupi";
            gvLupaPasqFin.SettingsBehavior.AllowSelectByRowClick = true;
        }

        protected void gvLupaPasqFin_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            gvLupaPasqFin.Selection.UnselectAll();
        }

    }
}
