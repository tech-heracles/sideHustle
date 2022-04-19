using System.Collections.Generic;
using System.Data;
using AlphaWeb.Core.Interfaces.Data;

namespace AlphaWeb.Infrastructure.Data.AdoNet
{
    public interface IDbProviderFactory
    {
        DataProviderType DataProvider { get; }
        IDbConnection GetConnection();
        string GetConnectionString(string connectionName);
        IDbCommand GetCommand();

        IDbDataAdapter GetDataAdapter();

        IDbTransaction GetTransaction(IDbConnection connection,IsolationLevel isolationLevel);

        IDataParameter GetParameter();
        IList<IDbDataParameter> CreateParameters(int paramCount);
        
    }
}