using DbCore;
using DbCore.IMBUtils.Fiskalizimi.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace PlatinumWeb
{
    public partial class RestoreDatabase : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var prefix = clsFunksione.getInstanceAndDatabaseRequest();
            var dbName = clsKontrollePerFiskalizimin.ktheInitialCatalogTeLoguar();
            List<string> lista = clsFunksione.getClientDatabaseBackups(dbName + ".gz");
            LinkedList<string> listaRenditur = new LinkedList<string>();
            foreach(string db in lista)
            {
                if (db != "")
                {
                    long gen = long.Parse(db.Split(new string[] { "?generationAsUnix=" }, StringSplitOptions.None)[1]);
                    if (listaRenditur.Count == 0)
                        listaRenditur.AddFirst(db);
                    else
                    {
                        LinkedListNode<string> tmp = listaRenditur.First;
                        while(tmp != null)
                        {
                            if (long.Parse(tmp.Value.Split(new string[] { "?generationAsUnix=" }, StringSplitOptions.None)[1]) < gen)
                            {
                                tmp.List.AddBefore(tmp, db);
                                break;
                            }
                            else if(tmp.Next == null && long.Parse(tmp.Value.Split(new string[] { "?generationAsUnix=" }, StringSplitOptions.None)[1]) > gen)
                            {
                                tmp.List.AddAfter(tmp, db);
                                break;
                            }
                            tmp = tmp.Next;
                        }
                    }
                }
            }
            foreach (string db in listaRenditur)
            {

                ListItem listItem = new ListItem();
                string[] urlAndGeneration = new string[2];
                urlAndGeneration[0] = db.Split(new string[] { "?generationAsUnix=" }, StringSplitOptions.None)[0];
                urlAndGeneration[1] = db.Split(new string[] { "?generationAsUnix=" }, StringSplitOptions.None)[1];
                listItem.Value = urlAndGeneration[0];
                listItem.Attributes.Add("name", prefix + "#" + urlAndGeneration[1]);
                listItem.Attributes.Add("id", urlAndGeneration[1]);
                DateTimeOffset dateBackup = DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(urlAndGeneration[1]) / 1000).LocalDateTime;
                listItem.Text = dbName + " " + dateBackup.ToString();
                select.Items.Add(listItem);
            }
        }
    }
}