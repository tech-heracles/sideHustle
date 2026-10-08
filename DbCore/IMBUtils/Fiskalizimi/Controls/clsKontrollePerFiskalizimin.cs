using System;
using System.Collections.Concurrent;
using System.Data;
using System.Data.SqlClient;
using AlphaWeb.Core.Interfaces.Data;
using AlphaWeb.Infrastructure.Data.AdoNet;
using DbCore.IMBUtils.DataBase;

namespace DbCore.IMBUtils.Fiskalizimi.Controls
{
    public static class clsKontrollePerFiskalizimin
    {
        public static bool checkIfColumnExists(IDataRecord record, string columnToCheck)
        {
            for (int i = 0; i < record.FieldCount; i++)
                if (record.GetName(i).Equals(columnToCheck))
                    return true;
            return false;
        }
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
                int end = connectionString.LastIndexOf(";user Id");
                string result = connectionString.Substring(start, end - start);
                return result;
            }
            catch (Exception ex)
            {
                return "";
            }

        }
        // Kontrollet e meposhtme pyesin nese databaza e klientit ka nje kolone / procedure (versioni i skemes). Therriten
        // per cdo rresht ne importe dhe dokumente (p.sh. 7 386 here ne importin e klienteve), secila me lidhje te re, por
        // pergjigjja ndryshon vetem kur perditesohet databaza. Ruhen per cdo databaze per 10 minuta.
        private static readonly ConcurrentDictionary<string, Tuple<bool, DateTime>> skemaDb = new ConcurrentDictionary<string, Tuple<bool, DateTime>>();
        private static readonly TimeSpan JetegjatesiaSkemes = TimeSpan.FromMinutes(10);

        /// <summary>
        /// Connection string-u i databazes aktive. Brenda nje transaksioni merret nga lidhja e transaksionit (si me pare);
        /// jashte tij vetem lexohet, pa hapur lidhje (me pare hapej nje lidhje qe nuk mbyllej).
        /// </summary>
        private static string ConnectionStringAktiv()
        {
            if (!string.IsNullOrEmpty(MyTransactionScope.TransactionKey))
                return MyScopeDbManager.ConnectionString;
            return NewDbManager(DataProviderType.SqlServer, MyConnectionsManager.GetSelectedConNameServer()).ConnectionString;
        }

        /// <summary>Ekzekuton <paramref name="pyetja"/> dhe kthen <paramref name="kontrollo"/> mbi rreshtin e pare (false pa rreshta); rezultati ruhet.</summary>
        private static bool KontrolloSkemen(string connectionString, string pyetja, Func<SqlDataReader, bool> kontrollo)
        {
            string celesi = connectionString + "|" + pyetja;
            if (skemaDb.TryGetValue(celesi, out var ruajtur) && DateTime.UtcNow - ruajtur.Item2 < JetegjatesiaSkemes)
                return ruajtur.Item1;

            bool rezultati;
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(pyetja, connection))
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                    rezultati = reader.Read() && kontrollo(reader);
            }
            skemaDb[celesi] = Tuple.Create(rezultati, DateTime.UtcNow);
            return rezultati;
        }

        private static bool KaKolone(string tabela, string kolona)
        {
            return KontrolloSkemen(ConnectionStringAktiv(),
                "SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" + tabela + "' AND COLUMN_NAME = '" + kolona + "'",
                r => r["TABLE_NAME"].ToString() == tabela && r["COLUMN_NAME"].ToString() == kolona);
        }

        private static bool KaProcedure(string emri)
        {
            return KontrolloSkemen(ConnectionStringAktiv(),
                "SELECT * FROM sys.objects WHERE type = 'P' AND name = '" + emri + "'",
                r => r["name"].ToString() == emri);
        }

        public static bool ktheNeseKlientiEshteAzhornuarPerFiskalizim()
        {
            string connectionString = ConnectionStringAktiv();
            if (connectionString.Contains("alpha-conn-strings") || connectionString.Contains("10.48.244.153"))
                return true;
            return KaKolone("T_KOKASHITJE", "eInvoice");
        }
        public static bool ktheNeseKlientiEshteAzhornuarPerFiskalizimV3() => KaKolone("T_KOKASHITJE", "TIPIIVETEFATURIMIT");
        public static bool ktheNeseKlientiEshteAzhornuarPerFiskalizimV4() => KaKolone("T_TAKSAT", "TIPIIPERJASHTIMIT");
        public static bool ktheNeseKlientiEshteAzhornuarPerFiskalizimV5() => KaKolone("T_BANKA", "SHFAQNEEINVOICE");
        public static bool ktheNeseKlientiEshteAzhornuarPerFiskalizimV6() => KaProcedure("prc_T_GJENDJEARKEDITORE_selSipasIdArke");
        public static bool ktheNeseKlientiEshteAzhornuarPerDetajime() => KaProcedure("prc_T_TRUPIMAGAZINA_ktheSasineTotaleSipasArtikullitHPDDetajime");
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
