using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;

namespace PlatinumWeb
{
    /// <summary>
    /// Shiriti standard i veprimeve (toolbar) i faqeve. Mban vlerat e përbashkëta që më parë
    /// kopjoheshin në çdo faqe. Faqja vendos vetëm ID-në, ngjarjet dhe çfarë ndryshon prej këtyre.
    /// Kodi i faqeve e përdor si ASPxMenu, ndaj nuk ka nevojë për ndryshim.
    /// </summary>
    [ToolboxData("<{0}:AvecToolbar runat=\"server\"></{0}:AvecToolbar>")]
    public class AvecToolbar : ASPxMenu
    {
        public AvecToolbar()
        {
            ClientVisible = false;
            ClientInstanceName = "ASPxMenu1";
            ItemImagePosition = ImagePosition.Top;
            Width = Unit.Percentage(100);
            AutoPostBack = true;
            ShowPopOutImages = DevExpress.Utils.DefaultBoolean.True;

            RootItemSubMenuOffset.FirstItemX = -1;
            RootItemSubMenuOffset.FirstItemY = -1;
            RootItemSubMenuOffset.X = -1;
            RootItemSubMenuOffset.Y = -1;

            SubMenuStyle.GutterWidth = Unit.Pixel(0);
            SubMenuStyle.HorizontalAlign = HorizontalAlign.Justify;

            ClientSideEvents.ItemClick = "function(s, e) { menu_click(s,e); }";
            ClientSideEvents.Init = "function(s) {s.SetClientVisible(true);}";

            ItemImage.Height = Unit.Pixel(32);
            ItemImage.Width = Unit.Pixel(32);
            SubMenuItemImage.Height = Unit.Pixel(16);
            SubMenuItemImage.Width = Unit.Pixel(16);

            ItemStyle.DropDownButtonSpacing = Unit.Pixel(12);
            ItemStyle.PopOutImageSpacing = Unit.Pixel(18);
            ItemStyle.VerticalAlign = VerticalAlign.Middle;
            ItemStyle.Paddings.PaddingBottom = Unit.Pixel(1);
            ItemStyle.Paddings.PaddingTop = Unit.Pixel(9);

            SubMenuItemStyle.Width = Unit.Pixel(32);
        }
    }
}
