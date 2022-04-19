using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class LogOut : MyPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DbCore.clsFunksione.logout(Session, true);
            //Session.Abandon();
            //bool ndryshuar = false;
            ////Session.Clear();
            ////Session.Abandon();
            //DbCore.DbAdmin.clsTrackUser onlUser = new DbCore.DbAdmin.clsTrackUser();
            //onlUser.LogoutDatetime = DateTime.Now.ToString();
            //ndryshuar = onlUser.modifikoAllOffline();
        }
    }
}