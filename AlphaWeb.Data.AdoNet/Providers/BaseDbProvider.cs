using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using AlphaWeb.Core.Interfaces.Data;

namespace AlphaWeb.Infrastructure.Data.AdoNet.Providers
{
    internal abstract class BaseDbProvider
    {
        public DataProviderType DataProvider { get; internal set; }

        public IDbTransaction GetTransaction(IDbConnection connection, IsolationLevel isolationLevel)
            => connection?.BeginTransaction(isolationLevel);
        public string GetConnectionString(string connectionName)
        {
            return ConnectionStringsManager.Instance.GetConnectionString(connectionName);
        }
    }
}
