using PlatinumWeb.ApplicationUtils.Pages;
using System;
using AlphaWebCommon.WebUtils.ASPxControlExtensions;
using DbCore.DbAdmin;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{
    public partial class LupaSerialeUnike : MyPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                var guidString = Request.QueryString["guidString"];
                if (string.IsNullOrEmpty(guidString))
                    guidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                hfState.Set("guidString", guidString);
                hfState.SetObject("IdNdermarrje", IdNdermarrja);
                hfState.SetObject("colGridaTrupi", new colGridaTrupi(Convert.ToInt32(Komponente.LupaSerialeUnike), 1, IdGjuha));
                MbushHiddenFieldMePerkthime();
            }
            
            PercaktoTemplateMenu();
            ASPxMenu1.Items.FindByName("Anullo").Text = MessagesResource.Messages["MenuItemMbyll"];
        }

        /// <summary>
        /// Metode qe sherben per te kaluar perkthime te js
        /// </summary>
        private void MbushHiddenFieldMePerkthime()
        {
            hfState.Set("msgNdryshimeTeParuajtura", MessagesResource.Messages["msgNdryshimeTeParuajtura"]);
            hfState.Set("msgSerialMeProbleme", MessagesResource.Messages["msgSerialMeProbleme"]);
        }

        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            PercaktoTemplateMenu();
            ASPxMenu1.Items.FindByName("Anullo").Text = MessagesResource.Messages["MenuItemMbyll"];
        }

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        private void PercaktoTemplateMenu()
        {
            clsToolbarConfig.percaktoTemplateMenu(IdGjuha, IdViti, IdPerdoruesi, IdNdermarrja, ASPxMenu1, "LupaSerialeUnike.aspx", this, MenuInfo, true, false, false, Meme);            
        }
    }
}
