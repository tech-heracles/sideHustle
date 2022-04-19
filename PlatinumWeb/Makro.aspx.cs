using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using DevExpress.Web;
using System.Drawing;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{
    public partial class Makro : MyPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //behet kontrolli nese perdoruesi eshte i loguar ne sistem
            //n.q.s jo, atehere ridrejtohet edhe njeher tek forma e loginit
            //if (CacheLayer.GlobalCacheManager.MySessionCache["LoggedIn"].Equals("No"))
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                DbCore.clsFunksione.logout(Session,true,"FaqePaautorizuar");
            }
            int idPerd = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            //if (CacheLayer.GlobalCacheManager.MySessionCache["KodiNdermarrjes"] == null)
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerd);
            }
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idNderViti = Convert.ToInt32((string)DbCore.mySessionObjects.ktheNdermarrjeVit(Session).ToString());
            hfState.Set("idPerd", idPerd);
            hfState.Set("idNdermarrje", idNdermarrje);
            hfState.Set("idNderViti", idNderViti);
            if (!IsPostBack)
            {
                if (Request.QueryString["ruaj"] == "ok")
                {
                    pergjigja.Text = MessagesResource.Messages["mesazhRuajtjeMeSukses"];
                    pergjigja.ForeColor = Color.Green;
                }

                konfiguroVleraFillestare();
                konfiguroGride();

                AspxWebControlUtils.konfiguroMenuPaTheme(ASPxMenu1);
                AspxWebControlUtils.percaktoTedrejtatPerKeteFaqe("Makro.aspx", ASPxMenu1);
            }
            konfiguroVleraFillestare();

            
        }

        private void konfiguroVleraFillestare()
        {//mbush griden me te dhena
            DbCore.DbInventari.colKokatMakro col = new DbCore.DbInventari.colKokatMakro(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            //DbCore.DbInventari.colKokatMakro  col = dbInventari.merrMakroSipasNdermarrjesAndAutorizime(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            this.gvMakro.DataSource = col;
            this.gvMakro.DataBind();
        }

        protected void gvMakro_DataBound(object sender, EventArgs e)
        {// shton colonen # per selektim dhe disa karakteristika te grides
            if (this.gvMakro.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per 
                //perzgjidh
                DevExpress.Web.GridViewCommandColumn check = new DevExpress.Web.GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true; check.Width = Unit.Percentage(2);
                gvMakro.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                gvMakro.Settings.ShowFilterRowMenu = true;
                check.SetColVisibleIndex(0);
                //behet per te afishuar rreshtin qe do sherbej per filtrim
                gvMakro.Settings.ShowFilterRow = true;
                gvMakro.Columns.Add(check);

                gvMakro.KeyFieldName = "IdKokaMakro";
                gvMakro.SettingsBehavior.AllowSelectByRowClick = true;
                gvMakro.SettingsBehavior.AllowFocusedRow = true;
            }
        }

        private void konfiguroGride()
        {           
            GridUtil.percaktoVisibleColumnsMeWidth(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), gvMakro, "gvMakro", "Makro.aspx");
            //funk.konfiguroGrideListeMadhe(gvMakro, "IdKokaMakro");
            GridUtil.konfigGrideListeEMadhePaTheme(gvMakro, "IdKokaMakro");
        }

        protected void gvMakro_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            konfiguroVleraFillestare();
        }

        //sherben per te vendosur filtra tek header-i i grides (mund te ndryshohet me vone per tu
        //bere  me e konfigurueshme
        protected void gvMakro_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            if (e.Column.FieldName == "PershkrimiKokaMakro")
            {
                e.Values.Clear();
                //e.AddShowAll();
                e.AddValue("(Te gjithe)", string.Empty, "true");
                e.AddValue("Nga A-D ", string.Empty, e.Column.FieldName + ">'A     ' and " + e.Column.FieldName + " <'DDDDDDD'");
                e.AddValue("Nga D-G ", string.Empty, e.Column.FieldName + ">'D     ' and " + e.Column.FieldName + "<'GGGGGGG'");
                e.AddValue("Nga H-K ", string.Empty, e.Column.FieldName + ">'H     ' and " + e.Column.FieldName + "<'KKKKKKK'");
                e.AddValue("Nga L-O ", string.Empty, e.Column.FieldName + ">'L     ' and " + e.Column.FieldName + "  <'OOOOOOO'");
                e.AddValue("Nga P-S ", string.Empty, e.Column.FieldName + ">'P     ' and " + e.Column.FieldName + "<'SSSSSSS'");
                e.AddValue("Nga T-W ", string.Empty, e.Column.FieldName + ">'T     ' and " + e.Column.FieldName + "<'WWWWWWW'");
                e.AddValue("Nga X-Z ", string.Empty, e.Column.FieldName + ">'X     ' and " + e.Column.FieldName + "<'ZZZZZZZ'");
            }
            else
            {
                e.Values.Clear();
                e.AddValue("(Te gjithe)", string.Empty, "true");
            }
        }

        protected void gvMakro_AutoFilterCellEditorInitialize1(object sender, ASPxGridViewEditorEventArgs e)
        {
        }

        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            List<object> rreshtat = null;

            rreshtat = gvMakro.GetSelectedFieldValues("IdKokaMakro");
DbCore.DbInventari.clsDatabaseInventari dbInventari = new DbCore.DbInventari.clsDatabaseInventari();
            foreach (int id in rreshtat)
            {
                
                DbCore.DbInventari.clsKokaMakro cls = new DbCore.DbInventari.clsKokaMakro(id);
                //DbCore.DbInventari.colKokatMakro col = dbInventari.ktheKokaMakro(id);

                //foreach (DbCore.DbInventari.clsKokaMakro  k in col)
                //{
                   
                    if (!dbInventari.kaVeprimeMakro(cls.IdKokaMakro))
                        {
                        cls.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                        cls.fshi();
                    }
                    else
                    {
                        lblPergjigja.Text = "Ka veprime me kete Makro";
                    }
                //}
            }
            dbInventari.Dispose();
            if (lblPergjigja.Text == "")
                Response.Redirect("Makro.aspx");
        }

        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {
            if (e.Item.Name == "Modifiko")
            {
                int indeksi;
                string id;
                string nr;

                indeksi = gvMakro.FocusedRowIndex;

                if (gvMakro.GetRowValues(indeksi, "IdKokaMakro") != null)
                {
                    id = gvMakro.GetRowValues(indeksi, "IdKokaMakro").ToString();
                    nr = gvMakro.GetRowValues(indeksi, "KodiKokaMakro").ToString();
                }
                else
                {
                    id = null;
                    nr = null;
                }

                Response.Redirect("~/Modifiko_Makro.aspx?id=" + id + "&kod=" + nr + "&indexrow=" + indeksi);
            }
            else if (e.Item.Name == "Shto")
            {
                Response.Redirect("Shto_Makro.aspx");
            }
        }

        protected void gvMakro_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            //if (e.Column.FieldName == "NiveliKPF")
            //{
            //    if (ImbUtil.ConvertToInt(e.Value) == -3)
            //    {
            //        e.Criteria = null;
            //    }
            //}
        }

    }
}
