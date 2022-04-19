using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PlatinumWeb
{
    public partial class design : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Prevent caching, so can't be viewed offline
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);

            //if (CacheLayer.GlobalCacheManager.MySessionCache["perdoruesi"] != null)
            if(DbCore.mySessionObjects.merrEmerPerdoruesiNgaSesioni(Session) != null)
            {
                //LabelUser.Text = Convert.ToString(CacheLayer.GlobalCacheManager.MySessionCache["perdoruesi"]);
                LabelUser.Text = DbCore.mySessionObjects.merrEmerPerdoruesiNgaSesioni(Session);
            }
            //LabelDate.Text = Convert.ToString(DateTime.Now);
        }

        protected void btnLogOut_Click(object sender, ImageClickEventArgs e)
        {
                 
            DbCore.clsFunksione.logout(Session, true);
            
        }

        protected void ASPxNavBar1_ItemClick(object source, DevExpress.Web.NavBarItemEventArgs e)
        {

        }
        
    }


}
