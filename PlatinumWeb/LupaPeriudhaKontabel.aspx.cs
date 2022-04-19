using DevExpress.Web;
using PlatinumWeb.Templates;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Drawing;
using System.Resources;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.DbShare;

namespace PlatinumWeb
{
    public partial class LupaPeriudhaKontabel : MyPageBase
    {
        //private DbCore.DbAdmin.clsDatabaseAdmin dbAdmin;
        private int idVitiAktual;
        private int idgjuha;
        private CultureInfo ci;

        protected void Page_Load(object sender, EventArgs e)
        {
            idgjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            ci = DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo(idgjuha);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            //StringReader sr = new StringReader(CacheLayer.GlobalCacheManager.MySessionCache["oPeriudhaAktuale"].ToString());
            //DbCore.DbAdmin.clsPeriudhaKontabel periudha = new DbCore.DbAdmin.clsPeriudhaKontabel(sr);
            //DbCore.DbAdmin.clsPeriudhaKontabel periudha = (DbCore.DbAdmin.clsPeriudhaKontabel)(CacheLayer.GlobalCacheManager.MySessionCache["oPeriudhaAktuale"]);
            DbCore.DbAdmin.clsPeriudhaKontabel periudha = DbCore.mySessionObjects.merrPeriudheKontabel(Session);
            idVitiAktual = periudha.IdViti;
            mbushPopUpListePeriudhash(idVitiAktual);
            mbushListeVite();
            percaktoTemplate();
           
            lblViti.Text = rm.GetString("labelVitiAktual", ci);
        }


        private void mbushPopUpListePeriudhash(int idViti)
        {
            //dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
           
            DbCore.DbAdmin.colPeriudhaKontabel colPeriudha = new DbCore.DbAdmin.colPeriudhaKontabel();
            colPeriudha.merrSipasViti(idViti, ci);
            gvLupaPerKont.DataSource = colPeriudha;
            gvLupaPerKont.DataBind();
            GridUtil.percaktoVisibleColumns(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), gvLupaPerKont, "gvLupaPerKont", "LupaPeriudhaKontabel.aspx");
            //funk.konfiguroGrideListeMadhePopupi(gvLupaPerKont, "IdPeriudha");
            var endlessScroll = clsAlternativaKushti.getAlternativa(clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod("LP/PerKont", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session)), "ES") == "Po";
            GridUtil.KonfiguroGrideListeMadhePopupiPaTheme(gvLupaPerKont, "IdPeriudha", true, endlessScroll);
            gvLupaPerKont.SettingsPager.PageSize = 12;
        }

        private void mbushListeVite()
        {
            DbCore.DbAdmin.colNdermarrjeVitet vitet = new DbCore.DbAdmin.colNdermarrjeVitet();
            vitet.mbushGjitheViteELidhuraMeNdermarrje(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            //vitet = dbAdmin.merrGjitheViteELidhuraMeNdermarrje(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            cmbVitiAktual.DataSource = vitet;

            cmbVitiAktual.TextField = "NdermarrjeViti";
            cmbVitiAktual.ValueField = "IdViti";
            cmbVitiAktual.DataBind();
            cmbVitiAktual.SelectedIndex = idVitiAktual;

        }

        protected void gvLupaPerKont_DataBound(object sender, EventArgs e)
        {
            gvLupaPerKont.Settings.ShowFilterRow = true;
            gvLupaPerKont.KeyFieldName = "IdPeriudha";
            gvLupaPerKont.SettingsBehavior.AllowSelectByRowClick = true;
        }

        protected void gvLupaPerKont_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            gvLupaPerKont.Selection.UnselectAll();
            percaktoTemplate();
        }

        //protected void btnFshi_Click(object sender, EventArgs e)
        //    {
        //    List<object> rreshtat = gvLupaPerKont.GetSelectedFieldValues("IdPeriudha");

        //    foreach (int id in rreshtat)
        //        {
        //        // DbCore.DbKontabiliteti.clsDatabaseKontabilitet dbKontabilitet = new DbCore.DbKontabiliteti.clsDatabaseKontabilitet();
        //        DbCore.DbAdmin.clsPeriudhaKontabel oPeriudha = new DbCore.DbAdmin.clsPeriudhaKontabel(id);
        //        // oPeriudha.fshi();

        //        }
        //    mbushPopUpListePeriudhash(idVitiAktual);
        //    mbushListeVite();
        //    }


        protected void cmbVitiAktual_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            idVitiAktual = Convert.ToInt32(cmbVitiAktual.SelectedItem.Value.ToString());
            mbushPopUpListePeriudhash(idVitiAktual);
        }

        protected void ASPxCallback1_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {
            List<object> rreshtat = gvLupaPerKont.GetSelectedFieldValues("IdPeriudha");
            foreach (int id in rreshtat)
            {
                DbCore.DbAdmin.clsPeriudhaKontabel oPeriudha = new DbCore.DbAdmin.clsPeriudhaKontabel(id, idgjuha);
                //CacheLayer.GlobalCacheManager.MySessionCache["oPeriudhaAktuale"] = oPeriudha;
                DbCore.mySessionObjects.ruajPeriudheKontabelNeSesion(oPeriudha, Session);
            }
        }

        protected void gvLupaPerKont_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (((ASPxGridView)sender).Columns.Count != 0)
            {
                if (e.VisibleIndex != -1)
                {
                    bool ekycur = bool.Parse(gvLupaPerKont.GetRowValues(e.VisibleIndex, "Ekycur").ToString());
                    if (ekycur)
                    {
                        e.Row.Enabled = false;
                        GridViewDataTextColumn col1 = ((ASPxGridView)sender).Columns["NrPeriudha"] as GridViewDataTextColumn;
                        ASPxLabel lbl1 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col1, "lbl") as ASPxLabel;
                        GridViewDataDateColumn col2 = ((ASPxGridView)sender).Columns["FillimiPeriudha"] as GridViewDataDateColumn;
                        ASPxLabel lbl2 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col2, "lbl") as ASPxLabel;
                        GridViewDataDateColumn col3 = ((ASPxGridView)sender).Columns["MbarimiPeriudha"] as GridViewDataDateColumn;
                        ASPxLabel lbl3 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col3, "lbl") as ASPxLabel;
                        GridViewDataTextColumn col4 = ((ASPxGridView)sender).Columns["EmerPeriudha"] as GridViewDataTextColumn;
                        ASPxLabel lbl4 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col4, "lbl") as ASPxLabel;
                        if (lbl1 != null)
                            lbl1.ForeColor = Color.Gray;
                        if (lbl2 != null)
                            lbl2.ForeColor = Color.Gray;
                        if (lbl3 != null)
                            lbl3.ForeColor = Color.Gray;
                        if (lbl4 != null)
                            lbl4.ForeColor = Color.Gray;
                    }
                }

            }
        }

        private void percaktoTemplate()
        {
            GridViewDataTextColumn col1 = gvLupaPerKont.Columns["NrPeriudha"] as GridViewDataTextColumn;
            col1.DataItemTemplate = new MyLabelTemplate();

            GridViewDataDateColumn col2 = gvLupaPerKont.Columns["FillimiPeriudha"] as GridViewDataDateColumn;
            col2.DataItemTemplate = new MyLabelTemplate();

            GridViewDataDateColumn col3 = gvLupaPerKont.Columns["MbarimiPeriudha"] as GridViewDataDateColumn;
            col3.DataItemTemplate = new MyLabelTemplate();

            GridViewDataTextColumn col4 = gvLupaPerKont.Columns["EmerPeriudha"] as GridViewDataTextColumn;
            col4.DataItemTemplate = new MyLabelTemplate();
        }
    }
}