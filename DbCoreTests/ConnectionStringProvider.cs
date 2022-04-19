using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCoreTests
{
    public class ConnectionStringProvider
    {
        public ConnectionStringProvider()
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (config.ConnectionStrings.ConnectionStrings["connStringAlpha"] == null)
                config.ConnectionStrings.ConnectionStrings.Add(new ConnectionStringSettings("connStringAlpha", MerrConnectionStringunEDuhur()));
            config.Save(ConfigurationSaveMode.Full, true);
            Debug.WriteLine(config.ConnectionStrings.ConnectionStrings["connStringAlpha"].ToString());
        }

        private static string MerrConnectionStringunEDuhur()
        {
            return @"Data Source=APPSERVER2012\MSSQLSERVER2012;Persist Security Info=True;Initial Catalog=webinfTest;User Id=webtestim;password=webtestim;Min pool size=0;Max pool size=1000000;pooling=yes";
            /*
            var filePath = @"\\server2012\alphaWeb\Web.config";
            var map = new ExeConfigurationFileMap { ExeConfigFilename = filePath };
            var configFile = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);


           // return "Data Source=IMB85;Persist Security Info=True;Initial Catalog=webinf;Trusted_Connection=true;Min pool size=0;Max pool size=1000000;pooling=yes";
            return configFile.ConnectionStrings.ConnectionStrings["connStringAlpha"].ToString();
            */

        }
    }
}
