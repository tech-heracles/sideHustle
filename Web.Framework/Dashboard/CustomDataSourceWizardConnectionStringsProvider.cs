using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Native;
using DevExpress.DataAccess.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Framework.Dashboard
{
    public class CustomDataSourceWizardConnectionStringsProvider : IDataSourceWizardConnectionStringsProvider
    {
        string connectionString;

        public CustomDataSourceWizardConnectionStringsProvider(string connectionString)
            : base()
        {
            this.connectionString = connectionString;
        }

        public Dictionary<string, string> GetConnectionDescriptions()
        {
            Dictionary<string, string> connections = new Dictionary<string, string>();

            // Customize the loaded connections list.  
            connections.Remove("LocalSqlServer");
            connections.Add("dashboardConnection", "Dashboard Datasource Connection");
            return connections;
        }

        public DataConnectionParametersBase GetDataConnectionParameters(string name)
        {
            // Return custom connection parameters for the custom connection.
            if (name == "dashboardConnection")
            {
                System.Data.SqlClient.SqlConnectionStringBuilder builder = new System.Data.SqlClient.SqlConnectionStringBuilder(this.connectionString);
                if(builder.IntegratedSecurity)
                    return new MsSqlConnectionParameters(builder.DataSource, builder.InitialCatalog, "", "", MsSqlAuthorizationType.Windows);
                return new MsSqlConnectionParameters(builder.DataSource, builder.InitialCatalog, builder.UserID, builder.Password, MsSqlAuthorizationType.SqlServer);
            }
            throw new System.Exception("The connection string is undefined.");
        }
    }
}