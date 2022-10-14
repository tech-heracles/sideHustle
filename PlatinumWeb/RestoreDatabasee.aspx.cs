using DbCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Google.Apis.Auth.OAuth2;
using Google.Apis.SQLAdmin.v1beta4;
using Google.Apis.Services;
using Newtonsoft.Json;
using System.Threading.Tasks;
namespace PlatinumWeb
{
    public partial class RestoreDatabase : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<string> lista = clsFunksione.getClientDatabaseBackups();
            foreach(string db in lista)
            {

                ListItem listItem = new ListItem();
                string[] urlAndGeneration = new string[2];
                urlAndGeneration[0] = db.Split(new string[] { "?generationAsUnix="}, StringSplitOptions.None)[0];
                urlAndGeneration[1] = db.Split(new string[] { "?generationAsUnix="}, StringSplitOptions.None)[1];
                listItem.Value = urlAndGeneration[0];
                DateTimeOffset dateBackup = DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(urlAndGeneration[1])/1000);
                listItem.Text = "il-camminotestime " + dateBackup.ToString();

                select.Items.Add(listItem);
            }
        }
    }
}