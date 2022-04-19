using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using AlphaWeb.Core.Interfaces.Data;

namespace AlphaWeb.Infrastructure.Data.AdoNet.Providers
{
    internal class SqlServerProviderFactory :BaseDbProvider,IDbProviderFactory
    {
        public SqlServerProviderFactory()
        {
            DataProvider = DataProviderType.SqlServer;
        }

        public IDbConnection GetConnection() => new SqlConnection();

        public IDbCommand GetCommand() => new SqlCommand();

        public IDbDataAdapter GetDataAdapter() => new SqlDataAdapter();

        public IDataParameter GetParameter() => new SqlParameter();
        public IList<IDbDataParameter> CreateParameters(int paramCount)
        {
            var parameters = new List<IDbDataParameter>(paramCount);
            for (int i = 0; i < paramCount; i++)
            {
                parameters.Add(new SqlParameter());
            }
            return parameters;
        }
    }
}
