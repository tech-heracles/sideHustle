using System;
using DbCore.IMBUtils.Messages;
using DevExpress.Web;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class LupaHarta : MyPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ShtoMenuControlsDheMsgFrame();
        }

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        protected void PercaktoTemplateMenu(ASPxMenu menu, ASPxMenu menuInfo)
        {
            clsToolbarConfig.percaktoTemplateMenu(IdGjuha, IdViti, IdPerdoruesi, IdNdermarrja, _menu, "LupaHarta.aspx", this, _menuInfo, null, null, true, false, false, Meme);
            _menu.Items.FindByName("Anullo").Text = MessagesResource.Messages["MenuItemMbyll"];
        }
    }
}