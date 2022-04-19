using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.SqlClient;
using AlphaWeb.Core.Interfaces.Data;

namespace AlphaWeb.Infrastructure.Data.OthersDb
{

    //TODO NEDJAN refactor kete sipas librarise ne .net standard
    public static class DbManagerFactory
    {

        public static IDbConnection GetConnection(DataProviderType
         providerType)
        {
            IDbConnection iDbConnection = null;
            switch (providerType)
            {
                case DataProviderType.SqlServer:
                    iDbConnection = new SqlConnection();
                    break;
                case DataProviderType.OleDb:
                    iDbConnection = new OleDbConnection();
                    break;
                case DataProviderType.Odbc:
                    iDbConnection = new OdbcConnection();
                    break;
                default:
                    return null;
            }
            return iDbConnection;
        }

        public static IDbCommand GetCommand(DataProviderType providerType)
        {
            switch (providerType)
            {
                case DataProviderType.SqlServer:
                    return new SqlCommand();
                case DataProviderType.OleDb:
                    return new OleDbCommand();
                case DataProviderType.Odbc:
                    return new OdbcCommand();
                default:
                    return null;
            }
        }

        public static IDbDataAdapter GetDataAdapter(DataProviderType
        providerType)
        {
            switch (providerType)
            {
                case DataProviderType.SqlServer:
                    return new SqlDataAdapter();
                case DataProviderType.OleDb:
                    return new OleDbDataAdapter();
                case DataProviderType.Odbc:
                    return new OdbcDataAdapter();
                default:
                    return null;
            }
        }

        public static IDbTransaction GetTransaction(DataProviderType providerType, IDbConnection connAlpha)
        {
            return GetTransaction(providerType, connAlpha, IsolationLevel.RepeatableRead); //IsolationLevel.ReadCommitted //uncomment for the default one
        }

        public static IDbTransaction GetTransaction(DataProviderType providerType, IDbConnection connAlpha, IsolationLevel isolationLevel)
        {
            IDbConnection iDbConnection = connAlpha;
            IDbTransaction iDbTransaction = iDbConnection.BeginTransaction(isolationLevel);
            return iDbTransaction;
        }

        public static IDataParameter GetParameter(DataProviderType
     providerType)
        {
            IDataParameter iDataParameter = null;
            switch (providerType)
            {
                case DataProviderType.SqlServer:
                    iDataParameter = new SqlParameter();
                    break;
                case DataProviderType.OleDb:
                    iDataParameter = new OleDbParameter();
                    break;
                case DataProviderType.Odbc:
                    iDataParameter = new OdbcParameter();
                    break;

            }
            return iDataParameter;
        }

        public static IDbDataParameter[] GetParameters(DataProviderType
         providerType,
          int paramsCount)
        {
            IDbDataParameter[] idbParams = new IDbDataParameter[paramsCount];

            switch (providerType)
            {
                case DataProviderType.SqlServer:
                    for (int i = 0; i < paramsCount; ++i)
                    {
                        idbParams[i] = new SqlParameter();
                    }
                    break;
                case DataProviderType.OleDb:
                    for (int i = 0; i < paramsCount; ++i)
                    {
                        idbParams[i] = new OleDbParameter();
                    }
                    break;
                case DataProviderType.Odbc:
                    for (int i = 0; i < paramsCount; ++i)
                    {
                        idbParams[i] = new OdbcParameter();
                    }
                    break;
                default:
                    idbParams = null;
                    break;
            }
            return idbParams;
        }
    }
}