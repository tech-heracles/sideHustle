using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PlatinumWeb
{
    public partial class TimeoutControl : System.Web.UI.UserControl
    {
        private CultureInfo Ci => DbCore.mySessionObjects.ktheCultureInfo(Session);
        private ResourceManager Rm => new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

        protected void Page_Load(object sender, EventArgs e)
        {
            TimeoutPopup.HeaderText = Rm.GetString("msgPoMbaronSesioni", Ci);
            KlikoniOkLabel.Text = Rm.GetString("msgKlikoniOkPerTeVazhduar", Ci);
        }

    }
}