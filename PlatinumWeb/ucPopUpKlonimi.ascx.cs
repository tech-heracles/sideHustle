using DbCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DbCore.IMBUtils.Messages;
using PlatinumWeb.ApplicationUtils;


namespace PlatinumWeb
{
    public partial class ucPopUpKlonimi : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AspxWebControlUtils.perkthePopUp(popUpKlonimi, MessagesResource.Messages["labelRaportKujdes"], lblKlonoNe, MessagesResource.Messages["lblKlonoNe"], null, null, btnKlono, MessagesResource.Messages["btnKlono"]);
                lblKonfig.Text = MessagesResource.Messages["lblLloji"];                
            }
        }
    }
}