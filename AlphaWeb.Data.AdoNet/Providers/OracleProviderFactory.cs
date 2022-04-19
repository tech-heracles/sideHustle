using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using AlphaWeb.Core.Interfaces.Data;

namespace AlphaWeb.Infrastructure.Data.AdoNet.Providers
{
    internal class OracleProviderFactory : BaseDbProvider, IDbProviderFactory
    {
        public OracleProviderFactory()
        {
            DataProvider = DataProviderType.Oracle;
        }
        public IDbConnection GetConnection() => new OracleConnection();
        public IDbCommand GetCommand() => new OracleCommand();

        public IDbDataAdapter GetDataAdapter() => new OracleDataAdapter();

        public IDataParameter GetParameter() => new OracleParameter();
        public IList<IDbDataParameter> CreateParameters(int paramCount)
        {
            var parameters = new List<IDbDataParameter>(paramCount);
            for (int i = 0; i < paramCount; i++)
            {
                parameters.Add(new OracleParameter());
            }
            return parameters;
        }
    }
}
