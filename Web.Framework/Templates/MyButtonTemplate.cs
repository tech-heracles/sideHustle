using DevExpress.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PlatinumWeb.Templates
{
    public class MyButtonTemplate : ITemplate
    {
        private string text;
        private string id;
        private string commandName;
        public MyButtonTemplate(string text, string id = "btn", string commandName = "fshi")
        {
            this.text = text;
            this.id = id;
            this.commandName = commandName;

        }
        public void InstantiateIn(Control Container)
        {
            ASPxButton button = new ASPxButton();
            GridViewDataItemTemplateContainer gridContainer = (GridViewDataItemTemplateContainer)Container;
            button.ID = id;
            button.Text = text;
            button.CommandName = commandName;
            button.AutoPostBack = false;
            button.Enabled = true;
            button.Width = Unit.Percentage(100);
            switch (text)
            {
                case "":
                    button.Image.Url = "~/images/fileclose-16.png";
                    break;
                case "shiko":
                    button.Image.Url = "~/images/add_point_off.png";
                    button.Text = "";
                    break;
                case "Ridergo":
                    button.Image.Url = "~/images/DergoEmail.png";
                    button.ClientSideEvents.Click = "function(s,e){ grid_HistorikuEmail.PerformCallback(\"" + gridContainer.VisibleIndex + ";" + gridContainer.KeyValue + "\");}";
                    button.Text = "";
                    break;
                default:
                    break;
            }
            Container.Controls.Add(button);
        }
    }
}