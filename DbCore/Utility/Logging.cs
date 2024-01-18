using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DbCore.classes;
using DbCore.DbAdmin;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace DbCore.Utility
{
    public static class Logging
    {
        private const string _logUrl = "https://europe-west1-alphaweb.cloudfunctions.net/logAlphawebView";
        public static async Task<bool> Log(BigQueryLogMessage message)
        {
            try
            {
                string componentName = clsKomponente.merrKomponenteLikeSipasEmrit(message.view.Split('/')[1], true);
                message.view = componentName;
                if (message.view.IsNullOrEmpty()) return true;
                using (HttpClient client = new HttpClient())
                {
                    string json = JsonConvert.SerializeObject(message);
                    StringContent data = new StringContent(json, Encoding.UTF8, "application/json");
                    await client.PostAsync(_logUrl, data);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
