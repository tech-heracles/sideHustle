using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using DbCore;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class LoginFail : MyPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["arsye"] != null)
            {
                if (Convert.ToString(Request.QueryString["arsye"]).Equals("maxLoginAttempts"))
                    LabelError.Text = "Nuk keni me te drejte te provoni login! Te dhenat ishin te gabuara.";
            }
        }
    }
}
