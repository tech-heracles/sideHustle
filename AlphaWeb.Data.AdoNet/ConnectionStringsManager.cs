using System.Collections.Concurrent;
using System.Collections.Generic;

namespace AlphaWeb.Infrastructure.Data.AdoNet
{
    public class ConnectionStringsManager
    {
        // lexohet ne cdo query dhe ndryshohet kur rifreskohen licencat: duhet te jete i sigurt per shume thread-e
        private static readonly ConcurrentDictionary<string, string> ConnectionStrings;
        public static readonly ConnectionStringsManager Instance;
        static ConnectionStringsManager()
        {
            ConnectionStrings = new ConcurrentDictionary<string, string>();
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

    }
}
