using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Data;
using System.Web.Security;
using System.Configuration;
using CacheLayer;
using DbCore.DbAdmin;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.Types;

namespace PlatinumWeb
{
    public partial class Login_Ndermarrje : MyPageBase
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            int idPerdoruesi = IdPerdoruesi;
            if (!IsPostBack)
            {
                var licenca = new clsLicenca();
                licenca.mbushLicencen(IdPerdoruesi);
                hfState.Set("idGjuha", IdGjuha);
                hfState.Set("chatAktiv", licenca.ChatAktiv);
                hfState.Set("chatLink", licenca.ChatLink);
                hfState.Set("chatPortHttp", licenca.ChatPortHttp);
                hfState.Set("chatPortHttps", licenca.ChatPortHttps);
                konfiguroVleraFillestare(idPerdoruesi);
                if (grid_ListLoginNdermarrje.VisibleRowCount == 1) //rasti kur kemi nje ndermarrje
                    merrNdermarrjenPerPune(0, idPerdoruesi);
                AplikoFilterVitiSipasKonfigurimit();
            }
            else
                konfiguroVleraFillestare(idPerdoruesi);

        }

        private void AplikoFilterVitiSipasKonfigurimit()
        {
            if (clsServerConfiguration.LexoKonfigurimSipasKey<string>(ServerKonfigKey.LOGIN_DEFAULT_VIT_AKTUAL) == "PO")
                grid_ListLoginNdermarrje.FilterExpression = $"[VITI]={DateTime.Now.Year}";
        }

        private void konfiguroVleraFillestare(int idPerdoruesi)
        {
            if (idPerdoruesi == 0)
            {
                //DbCore.clsFunksione.logout(Session,true,"FaqePaautorizuar");
                DbCore.clsFunksione.logout(Session, true, "FaqePaautorizuar");
                return;
            }
            popNdermarrjePaPeriudha.HeaderText = rm.GetString("labelAdministrimiKujdes", ci);
            lblNukEkzistojnePeriudhat.Text = rm.GetString("msgLoginNdermarrjeKujdesNukEkzistojnePeriudhat", ci);
            lblUserEmriKESH.Text = lblUserEmri.Text = DbCore.mySessionObjects.kthePerdorues(Session).PerdoruesUsername;
            ASPxHyperLink2.NavigateUrl = $"{DbCore.IMBUtils.Paths.defaultLoginPath}?arsye=logout";
            ASPxHyperLink3.NavigateUrl = $"{DbCore.IMBUtils.Paths.defaultLoginPath}?arsye=logout";
            ASPxHyperLink2.Text = rm.GetString("labelLogOut", ci);

            using (var dbAdmin = new clsDatabaseAdmin())
            {


                grid_ListLoginNdermarrje.DataSource = dbAdmin.merrNdermarrjetEPerdoruesitDataTable(idPerdoruesi);

                grid_ListLoginNdermarrje.DataBind();




            }
        }

        protected void grid_ListLoginNdermarrje_DataBound(object sender, EventArgs e)
        {
            if (grid_ListLoginNdermarrje.Columns["#"] == null)
            {
                grid_ListLoginNdermarrje.KeyFieldName = grid_ListLoginNdermarrje.Columns[0].ToString();
                grid_ListLoginNdermarrje.Settings.ShowFilterRow = true;
                grid_ListLoginNdermarrje.Settings.ShowFilterRowMenu = true;
                grid_ListLoginNdermarrje.SettingsPager.NumericButtonCount = 5;
                grid_ListLoginNdermarrje.Columns[0].Visible = false;
                grid_ListLoginNdermarrje.Columns[1].Caption = rm.GetString("koloneLoginNdermarrjeKodi", ci);
                grid_ListLoginNdermarrje.Columns[1].VisibleIndex = 1;
                grid_ListLoginNdermarrje.Columns[1].Width = Unit.Percentage(20);

                GridViewDataColumn col1 = grid_ListLoginNdermarrje.Columns[1] as GridViewDataColumn;
                col1.Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                grid_ListLoginNdermarrje.Columns[2].Caption = rm.GetString("koloneLoginNdermarrjeNdermarrja", ci);
                grid_ListLoginNdermarrje.Columns[2].VisibleIndex = 2;
                grid_ListLoginNdermarrje.Columns[2].Width = Unit.Percentage(70);
                grid_ListLoginNdermarrje.Columns[2].MinWidth = 50;

                GridViewDataColumn col2 = grid_ListLoginNdermarrje.Columns[2] as GridViewDataColumn;
                col2.Settings.AutoFilterCondition = AutoFilterCondition.Contains;

                grid_ListLoginNdermarrje.Columns[3].Caption = rm.GetString("koloneLoginNdermarrjeViti", ci);
                grid_ListLoginNdermarrje.Columns[3].VisibleIndex = 3;
                grid_ListLoginNdermarrje.Columns[3].Width = Unit.Percentage(10);
                grid_ListLoginNdermarrje.Columns[3].MinWidth = 70;
                //GridViewDataTextColumn colnew = new GridViewDataTextColumn();
                //colnew = grid_ListLoginNdermarrje.Columns[3] as GridViewDataTextColumn;
                //colnew.Settings.FilterMode = ColumnFilterMode.DisplayText;
                //GridViewDataColumn col3 = grid_ListLoginNdermarrje.Columns[3] as GridViewDataColumn;
                //col3.Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                grid_ListLoginNdermarrje.Columns[4].Visible = false;
                grid_ListLoginNdermarrje.Columns[4].Width = 0;

                grid_ListLoginNdermarrje.SettingsBehavior.AllowFocusedRow = true;
                grid_ListLoginNdermarrje.SettingsText.Title = rm.GetString("lupaLoginNdermarrjeZgjidhNdermarrjen", ci);
                grid_ListLoginNdermarrje.Settings.ShowTitlePanel = true;

            }
        }

        protected void ok_ASPxButton_Click(object sender, EventArgs e)
        {
            int idRreshtit = grid_ListLoginNdermarrje.FocusedRowIndex;
            merrNdermarrjenPerPune(idRreshtit, IdPerdoruesi);
        }

        private void merrNdermarrjenPerPune(int idRreshtit, int idPerdoruesi)
        {
            DataRow rreshti = grid_ListLoginNdermarrje.GetDataRow(idRreshtit);
            int idNdermarrjes = Convert.ToInt32(rreshti[0]);
            String kodiNdermarrjes = rreshti[1].ToString();
            String vitiNdermarrjes = rreshti[3].ToString();
            int idNdermarrjeVit = Convert.ToInt32(rreshti[4]);
            string scopeId;
            var mesazh = DbCore.clsFunksione.merrNdermarrjenPerPune(Request, Response, Session, idNdermarrjes, kodiNdermarrjes, vitiNdermarrjes, idNdermarrjeVit, idPerdoruesi, true, out scopeId, null);

            if (!mesazh)
            {
                popNdermarrjePaPeriudha.ShowOnPageLoad = true;
                lblNukEkzistojnePeriudhat.Text = mesazh.PershkrimMesazhi;
                return;
            }

          
        }
    }
}