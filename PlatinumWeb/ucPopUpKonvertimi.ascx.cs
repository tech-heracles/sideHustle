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
    public partial class ucPopUpKonvertimi : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AspxWebControlUtils.perkthePopUp(popKonvertim, MessagesResource.Messages["labelRaportKujdes"], lblKonvertoNe, MessagesResource.Messages["lblKonvertoNe"], null, null, ButtonOk2, MessagesResource.Messages["btnKonverto"]);
            }
        }
    }
}