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

            if(DbCore.mySessionObjects.merrEmerPerdoruesiNgaSesioni(Session) != null)
            {
                LabelUser.Text = DbCore.mySessionObjects.merrEmerPerdoruesiNgaSesioni(Session);
            }
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
