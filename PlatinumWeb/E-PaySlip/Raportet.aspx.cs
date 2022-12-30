using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DbCore.DbShare;
using DbCore.DbAdmin;
using System.Data;
using DevExpress.Web;
using System.Collections.Specialized;
using System.Globalization;
using System.Resources;
using System.Reflection;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Configuration;
using System.Web.UI.HtmlControls;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb.E_PaySlip
{
    public partial class Raportet : MyPageBase
    {
        //colRaporti colrap = new colRaporti();
        //clsRaporti clsrap = new clsRaporti();
        //colModulet colmod = new colModulet();
        //clsModuli clsmod = new clsModuli(DbCore.mySessionObjects.ktheGjuhePerdoruesi);
        ////clsDatabaseAdmin clsadm = new clsDatabaseAdmin();

     
        string filtroRaportTooltip;
        protected void Page_Load(object sender, EventArgs e)
        {
            int idGjuha = 0; CultureInfo ci = null;
            if (Request.QueryString["vjenNga"] != null)
            {
                if (Request.QueryString["vjenNga"].ToString() == "epayslip")
                {
                    idGjuha = 0;
                    ci = new CultureInfo("sq-AL");
                }
            }
            else
            {
                idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
                ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            }
            //clsPeriudhaKontabel periudha = (clsPeriudhaKontabel)CacheLayer.GlobalCacheManager.MySessionCache["oPeriudhaAktuale"];
            clsPeriudhaKontabel periudha = DbCore.mySessionObjects.merrPeriudheKontabel(Session);
            dtdoknga.Value = periudha.FillimiPeriudha;
            dtdokderi.Value = periudha.MbarimiPeriudha;
            if (!IsPostBack)
            {

                //Response.Cache.SetCacheability(HttpCacheability.NoCache);

                EmrateLabelave(ci);
                //if (CacheLayer.GlobalCacheManager.MySessionCache["LoggedIn"].Equals("No"))
                if (!DbCore.mySessionObjects.isLogedIn(Session))
                {
                    Response.Redirect(DbCore.IMBUtils.Paths.loginPathEpaySlip);
                    return;
                }
                int idPerdorues = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                //if (CacheLayer.GlobalCacheManager.MySessionCache["KodiNdermarrjes"] == null)
                if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
                {
                    Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerdorues);
                    return;
                }
                if (Request.QueryString["idmod"] != null)
                {
                    //hdn1.Value = Request.QueryString["idmod"].ToString();
                }

                mbushComboBoxFiltra();
                clsModuli moduli = new clsModuli(idGjuha);
                moduli.IdModuli = Convert.ToInt32(Request.QueryString["idmod"]);
                moduli = moduli.merrModulById(idGjuha);
                moduliLabel.Text = moduli.PershkrimiModuli;
                mbushDataSource(idGjuha);
            }
        }

        private void mbushComboBoxFiltra()
        {
            int idModuli = Convert.ToInt32(Request.QueryString["idmod"]);
            colFilterKoka colFiltra = colFilterKoka.merrFiltratSipasModulit(idModuli, DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            colFiltra.Insert(0, new clsFilterKoka(""));
            ASPxComboBox1.DataSource = colFiltra;
            ASPxComboBox1.ValueField = "IdKokaFilter";
            ASPxComboBox1.TextField = "KokaFilterKodi";
            ASPxComboBox1.DataBind();
            ASPxComboBox1.SelectedIndex = 0;
        }

        protected void btnHelp_Init(object sender, EventArgs e)
        {
            DbCore.DbAdmin.clsKomponente kompRap = new DbCore.DbAdmin.clsKomponente("RaportetAll.aspx");
            this.btnHelp.ClientSideEvents.Click = "function(s,e){openHelpWindow(s,e,\'" + DbCore.clsFunksione.ktheUrlHelpi(kompRap.UrlHelpSuffix).Item1 + "\');}";
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                        System.Reflection.Assembly.Load("App_GlobalResources"));
            lbld.Text = rm.GetString("labelFilterKryesorDtDokumenti", ci);
            ASPxRadioButtonList1.Items[0].Text = rm.GetString("RadioButtonListEditItemAktuale", ci);
            ASPxRadioButtonList1.Items[1].Text = rm.GetString("RadioButtonListEditItemPeriudha", ci);
            ASPxRadioButtonList1.Items[2].Text = rm.GetString("RadioButtonListEditItemVitiUshtrimor", ci);
            ASPxRadioButtonList1.Items[3].Text = rm.GetString("RadioButtonListEditItemGjitheVitet", ci);
            ASPxLabel1.Text = rm.GetString("labelRaportiNga", ci);
            ASPxLabel2.Text = rm.GetString("labelRaportDeri", ci);
            ASPxLabel3.Text = rm.GetString("labelFilterPersonalizuar", ci);
            btnHelp.ToolTip = rm.GetString("tooltipBtnHelp", ci);
            btnCollapseAll.ToolTip = rm.GetString("tooltipBtnCollapseAll", ci);
            btnExpandAll.ToolTip = rm.GetString("tooltipBtnExpandAll", ci);
           filtroRaportTooltip = rm.GetString("tooltipHinkRaportHapPaFilter", ci);
        }

        public void Repeater2_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
                return;
            System.Data.DataRowView rreshti = (System.Data.DataRowView)(e.Item.DataItem);
            ASPxHyperLink linkuFiltro = (ASPxHyperLink)e.Item.FindControl("filtroRaport");
            ASPxHyperLink linkuHapRaport = (ASPxHyperLink)e.Item.FindControl("hapRaport");
            linkuFiltro.Text = rreshti["RAPEMRI"].ToString();
            if (Convert.ToInt32(Request.QueryString["idmod"]) == 19)
            {
                linkuHapRaport.Visible = false;
                linkuFiltro.NavigateUrl = rreshti["NAVIGATEURL"].ToString();
            }
            else
            {
                //linkuFiltro.NavigateUrl = "javascript:filtroButtonClick(" + rreshti["IDRAPORTI"].ToString() + ")";
                linkuFiltro.NavigateUrl = "javascript:filtroButtonClick(" + rreshti["IDRAPORTI"].ToString() + ",'" + rreshti["RAPEMRI"].ToString() + "')";
                linkuHapRaport.ToolTip = filtroRaportTooltip;
                //linkuHapRaport.NavigateUrl = rreshti["NAVIGATEURL"].ToString();
                linkuHapRaport.NavigateUrl = "javascript:hapraportin(" + rreshti["IDRAPORTI"].ToString() + ",'" + rreshti["RAPEMRI"].ToString() + "')";
            }
        }

        private void mbushDataSource(int idGjuha)
        {
            sqldatasourcereports.ConnectionString = ConfigurationManager.ConnectionStrings["connStringAlpha"].ConnectionString;
            sqldatasourcereports.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
            sqldatasourcereports.SelectParameters.Clear();
            Parameter tmpParam = new Parameter("idndermarje", TypeCode.Int32, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session).ToString());
            tmpParam.Direction = ParameterDirection.Input;
            sqldatasourcereports.SelectParameters.Add(tmpParam);
            tmpParam = new Parameter("idgjuha", TypeCode.Int32, idGjuha.ToString()) { Direction = ParameterDirection.Input };
            sqldatasourcereports.SelectParameters.Add(tmpParam);
            //ControlParameter IDMODULI = new ControlParameter("IDMODULI", TypeCode.Int32, "hdn1", "Value");
            //sqldatasourcereports.SelectParameters.Add(IDMODULI);

            tmpParam = new Parameter("IDMODULI", TypeCode.Int32, Request.QueryString["idmod"]) { Direction = ParameterDirection.Input };
            sqldatasourcereports.SelectParameters.Add(tmpParam);

            tmpParam = new Parameter("idperdorues", TypeCode.Int32, DbCore.mySessionObjects.ktheIdPerdoruesi(Session).ToString()) { Direction = ParameterDirection.Input };
            sqldatasourcereports.SelectParameters.Add(tmpParam);
            tmpParam = new Parameter("idviti", TypeCode.Int32, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session).ToString()) { Direction = ParameterDirection.Input };
            sqldatasourcereports.SelectParameters.Add(tmpParam);

            if (Request.QueryString["vjenNga"].ToString() == "epayslip")
            {

                sqldatasourcereports.SelectCommand = "prc_Moduli_RaportetSipasID";

                tmpParam = new Parameter("idraporti", TypeCode.Int32, Request.QueryString["idraporti"].ToString()) { Direction = ParameterDirection.Input };
                sqldatasourcereports.SelectParameters.Add(tmpParam);
            }
            else
            {

                sqldatasourcereports.SelectCommand = "prc_Moduli_RaportetSipasTeDrejtave";

                //sqldatasourcereports.SelectParameters.Add(new SessionParameter("idgjuha", "IdGjuha"));
            }

        }
    }
}