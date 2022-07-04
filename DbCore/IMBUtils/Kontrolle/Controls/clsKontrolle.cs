using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using AlphaWeb.Core.Extensions;
using AlphaWeb.Core.Interfaces.Data;
using AlphaWeb.Infrastructure.Data.AdoNet;
using AlphaWeb.Infrastructure.Data.OthersDb;
using DbCore.IMBUtils.DataBase;

namespace DbCore.IMBUtils.Kontrolle.Controls
{
    public static class clsKontrolle
    {
        public static DbManager NewDbManager(DataProviderType dataProviderType, string connectionName)
        {
            return new DbManager(dataProviderType, connectionName);
        }
        public static IDbManager MyScopeDbManager
        {
            get
            {
                IDbManager myScopeDbManager;
                if (!string.IsNullOrEmpty(MyTransactionScope.TransactionKey))
                {
                    //eshte brenda scope-it
                    myScopeDbManager = MyTransactionScope.CurrentDbConnection;
                    if (myScopeDbManager != null) return myScopeDbManager;
                    //hera e pare qe hapet connection
                    myScopeDbManager = NewDbManager(DataProviderType.SqlServer, MyConnectionsManager.GetSelectedConNameServer());
                    MyTransactionScope.DbConnections[MyTransactionScope.TransactionKey] = myScopeDbManager;
                    return myScopeDbManager;
                }
                //nuk eshte brenda nje scope-i
                myScopeDbManager = NewDbManager(DataProviderType.SqlServer, MyConnectionsManager.GetSelectedConNameServer());
                myScopeDbManager.Open();
                return myScopeDbManager;
            }
        }
        public static string ktheInitialCatalogTeLoguar()
        {
            try
            {
                var dbManager = MyScopeDbManager;
                string connectionString = dbManager.ConnectionString;
                int start = connectionString.IndexOf("Initial Catalog=") + 16;
                //int end = connectionString.LastIndexOf(";Trusted_Connection");
                int end = connectionString.LastIndexOf(";user Id");
                string result = connectionString.Substring(start, end - start);
                return result;
            }
            catch(Exception ex)
            {
                return "";
            }

        }
        public static bool ktheVersioninEServeritPerPagat()
        {
            var dbManager = MyScopeDbManager;
            string queryString = "SELECT DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'T_TATIME' AND COLUMN_NAME = 'NORMA'";
            string connectionString = dbManager.ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {

                        string data = reader["DATA_TYPE"].ToString();
                        if (data == "varchar") return true;
                        else return false;
                    }
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }
            return false;
        }
    }
}
