using System.Collections.Generic;

namespace AlphaWeb.Infrastructure.Data.AdoNet
{
    public class ConnectionStringsManager
    {
        private static readonly Dictionary<string, string> ConnectionStrings;
        public static readonly ConnectionStringsManager Instance;
        static ConnectionStringsManager()
        {
            ConnectionStrings = new Dictionary<string, string>();
            Instance = new ConnectionStringsManager();
        }

        public string GetConnectionString(string connectionName)
        {
            if (!ConnectionStrings.TryGetValue(connectionName, out var connectionString))
            {
                throw new KeyNotFoundException($"Nuk ekziston ne db nje connection string me emrin {connectionName}!");
            }
            return connectionString;
        }
        public IDictionary<string, string> GetConnectionStrings()
        {
            return ConnectionStrings;
        }
        public void SetConnectionStrings(IDictionary<string, string> connStrings)
        {
            foreach (var item in connStrings)
            {
                ConnectionStrings[item.Key] = item.Value;
            }
        }
        public void SetConnectionstring(string connectionName,string connectionString)
        {
            ConnectionStrings[connectionName] = connectionString;
        }

        public void RefreshConnectionStrings(IDictionary<string, string> connStrings, string connStringNameDefault)
        {
            string connectionStringDefault = ConnectionStrings[connStringNameDefault];
            ConnectionStrings.Clear();
            SetConnectionstring(connStringNameDefault, connectionStringDefault);
            SetConnectionStrings(connStrings);
        }
    }
}
