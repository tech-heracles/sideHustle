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

namespace DbCore.IMBUtils.Fiskalizimi.Controls
{
    public static class clsKontrollePerFiskalizimin
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
            catch (Exception ex)
            {
                return "";
            }

        }
        public static bool ktheNeseKlientiEshteAzhornuarPerFiskalizim()
        {
            var dbManager = MyScopeDbManager;
            string queryString = "SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = \'T_KOKASHITJE\' AND COLUMN_NAME = \'eInvoice\'";
            string connectionString = dbManager.ConnectionString;
            if (connectionString.ToString().Contains("alpha-conn-strings") || connectionString.ToString().Contains("10.48.244.153"))
            {
                return true;
            }
            else
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    try
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine(String.Format("{0}, {1}",
                            reader["TABLE_NAME"], reader["COLUMN_NAME"]));
                            if (reader["TABLE_NAME"].ToString() == "T_KOKASHITJE" && reader["COLUMN_NAME"].ToString() == "eInvoice")
                                return true;
                            else
                                return false;
                        }
                    }
                    finally
                    {
                        // Always call Close when done reading.
                        reader.Close();
                        connection.Close();
                    }
                }
            }
            return false;
        }
        public static bool ktheNeseKlientiEshteAzhornuarPerFiskalizimV3()
        {
            var dbManager = MyScopeDbManager;
            string queryString = "SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = \'T_KOKASHITJE\' AND COLUMN_NAME = \'TIPIIVETEFATURIMIT\'";
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
                        Console.WriteLine(String.Format("{0}, {1}",
                        reader["TABLE_NAME"], reader["COLUMN_NAME"]));
                        if (reader["TABLE_NAME"].ToString() == "T_KOKASHITJE" && reader["COLUMN_NAME"].ToString() == "TIPIIVETEFATURIMIT")
                            return true;
                        else
                            return false;
                    }
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                    connection.Close();

                }
            }
            return false;
        }
        public static bool ktheNeseKlientiEshteAzhornuarPerFiskalizimV4()
        {
            var dbManager = MyScopeDbManager;
            string queryString = "SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = \'T_TAKSAT\' AND COLUMN_NAME = \'TIPIIPERJASHTIMIT\'";
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
                        Console.WriteLine(String.Format("{0}, {1}",
                        reader["TABLE_NAME"], reader["COLUMN_NAME"]));
                        if (reader["TABLE_NAME"].ToString() == "T_TAKSAT" && reader["COLUMN_NAME"].ToString() == "TIPIIPERJASHTIMIT")
                            return true;
                        else
                            return false;
                    }
                }
                finally
                {
                    reader.Close();
                    connection.Close();
                }
            }
            return false;
        }
        public static bool ktheNeseKlientiEshteAzhornuarPerFiskalizimV5()
        {
            var dbManager = MyScopeDbManager;
            string queryString = "SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = \'T_BANKA\' AND COLUMN_NAME = \'SHFAQNEEINVOICE\'";
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
                        Console.WriteLine(String.Format("{0}, {1}",
                        reader["TABLE_NAME"], reader["COLUMN_NAME"]));
                        if (reader["TABLE_NAME"].ToString() == "T_BANKA" && reader["COLUMN_NAME"].ToString() == "SHFAQNEEINVOICE")
                            return true;
                        else
                            return false;
                    }
                }
                finally
                {
                    reader.Close();
                    connection.Close();
                }
            }
            return false;
        }
        public static bool ktheNeseKlientiEshteAzhornuarPerFiskalizimV6()
        {
            var dbManager = MyScopeDbManager;
            string queryString = "SELECT * FROM sys.objects WHERE type = \'P\' AND name = \'prc_T_GJENDJEARKEDITORE_selSipasIdArke\'";
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
                        if (reader["name"].ToString() == "prc_T_GJENDJEARKEDITORE_selSipasIdArke")
                            return true;
                        else
                            return false;
                    }
                }
                finally
                {
                    reader.Close();
                    connection.Close();
                }
            }
            return false;
        }
        public static bool ktheNeseKlientiEshteAzhornuarPerDetajime()
        {
            var dbManager = MyScopeDbManager;
            string queryString = "SELECT * FROM sys.objects WHERE type = \'P\' AND name = \'prc_T_TRUPIMAGAZINA_ktheSasineTotaleSipasArtikullitHPDDetajime\'";
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
                        if (reader["name"].ToString() == "prc_T_TRUPIMAGAZINA_ktheSasineTotaleSipasArtikullitHPDDetajime")
                            return true;
                        else
                            return false;
                    }
                }
                finally
                {
                    reader.Close();
                    connection.Close();
                }
            }
            return false;
        }
        public static string ktheDatenEServeritOffset()
        {
            var dbManager = MyScopeDbManager;
            string queryString = "SELECT SYSDATETIMEOFFSET();";
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

                        string data = reader[""].ToString();
                        return data;
                    }
                }
                finally
                {
                    reader.Close();
                    connection.Close();
                }
            }
            return "";
        }
    }
}
