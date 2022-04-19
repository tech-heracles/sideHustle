using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;
using System.Globalization;
using System.Resources;
using PlatinumWeb.ApplicationUtils.Pages;


namespace PlatinumWeb
{
    public partial class PreviewThemeAmbjente : MyPageBase
    {
   

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                EmrateTabeve();
            }
            if (Request.QueryString["ThemeFramet"] != null && Request.QueryString["ThemeFramet"].ToString() != "")
            {
                string emerTheme = Request.QueryString["ThemeFramet"].ToString();                
                if (emerTheme.IndexOf(" ") != -1)
                    emerTheme = emerTheme.Replace(" ", string.Empty);
                //ASPxNavBar1.CssFilePath = "~/App_Themes/" + emerTheme + "/{0}/styles.css";
                //ASPxNavBar1.CssPostfix = emerTheme;
                ASPxNavBar1.Theme = emerTheme;
                //ASPxSplitter1.CssFilePath = "~/App_Themes/" + emerTheme + "/{0}/styles.css";
                //ASPxSplitter1.CssPostfix = emerTheme;
                ASPxSplitter1.Theme = emerTheme;
            }
            else
            {
                //ASPxNavBar1.CssFilePath = "~/App_Themes/BlackGlass/{0}/styles.css";
                //ASPxNavBar1.CssPostfix = "BlackGlass";
                //ASPxSplitter1.CssFilePath = "~/App_Themes/BlackGlass/{0}/styles.css";
                //ASPxSplitter1.CssPostfix = "BlackGlass";
                ASPxNavBar1.Theme = "BlackGlass";
                ASPxSplitter1.Theme = "BlackGlass";
            }

            if (Request.QueryString["ThemeJQuery"] != null && Request.QueryString["ThemeJQuery"] != "")
            {
                string pathi = new DbCore.DbAdmin.clsThemesDevExpressJQuery(Request.QueryString["ThemeJQuery"].ToString()).Path;
                themeJQuery.Href = @pathi;
            }
            else //themeJQuery.Href = @"~\js\cssedmond\jquery-ui-1.8.9.custom.css";
                themeJQuery.Href = new DbCore.DbAdmin.clsThemesDevExpressJQuery(DbCore.DbAdmin.clsThemesDevExpressJQuery.defaultJQueryTheme).Path; // theme Redmond

            string pathBck = "height: 100%; width: 100%;";
            if (Request.QueryString["BackImg"] != null)
            {
                string pathBackImg = new DbCore.DbAdmin.clsTheme(Request.QueryString["BackImg"].ToString()).PathTheme;
                if (pathBackImg == "") pathBackImg = "images/backgrounds/imagesJ105.jpg";
                pathBck += "background-image:url('" + pathBackImg + "');";
            }
            else pathBck += "background-image:url('images/backgrounds/imagesJ105.jpg');";
            backDiv.Attributes.Add("style", pathBck);
            mbushGriden();
        }

        /// <summary>
        /// Vendos emrat e tabeve ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateTabeve()
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            ASPxPageControl2.TabPages[0].Text = rm.GetString("ambjentiKryesorTab", cultinf);
            ASPxPageControl2.TabPages[1].Text = rm.GetString("gridatRegjistrimeTab", cultinf);
        }

        private void mbushGriden()
        {
            DataTable dt = GetTable();
            grida.DataSource = dt;
            grida.KeyFieldName = "Kodi";
            grida.DataBind();
            dt.Dispose();
            grida.Visible = true;
        }

        private DataTable GetTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Kodi", typeof(String));
            dt.Columns.Add("Pershkrimi", typeof(String));
            dt.Columns.Add("Data", typeof(DateTime));
            dt.Columns.Add("Lloji", typeof(String));
            dt.Columns.Add("Aktiv", typeof(Boolean));

            dt.Rows.Add("Kod1", "Pershkrim 1", DateTime.Now, "Lloji 1", true);
            dt.Rows.Add("Kod2", "Pershkrim 2", DateTime.Now, "Lloji 2", false);
            dt.Rows.Add("Kod3", "Pershkrim 3", DateTime.Now, "Lloji 3", true);

            return dt;
        }
    }
}