using System;
using System.Collections.Generic;
using System.Data;
using AlphaWeb.Core.Configuration;

namespace AlphaWeb.Core.Interfaces.Data
{
    public interface IDbManager
    {
        DataProviderType ProviderType
        {
            get;
            set;
        }
        string ConnectionName { get; }
        string ConnectionString { get; }

        IDbConnection Connection
        {
            get;
        }
        IDbTransaction Transaction
        {
            get;
        }

        IDataReader DataReader
        {
            get;
        }
        IDbCommand Command
        {
            get;
        }

        IList<IDbDataParameter> Parameters { get; }
        int CommandTimeOut { get; set; }
        void Open();
        void BeginTransaction();
        void BeginTransaction(int timeout);
        void BeginTransaction(IsolationLevel isolationLevel);
        void CommitTransaction();
        void RollBackTransaction();
        void CreateParameters(int paramsCount);
        /// <summary>
        /// Pastron parametrat per select. 
        /// Perdoret ne pergjithesi kur ke nje transaksion dhe ne veprimin e dyte te duhet te pastrosh parametrat
        /// </summary>
        void ClearParameters();
        void CreateInsertParameters(int paramsCount);
        void CreateUpdateParameters(int paramsCount);

        void AddParameters(string paramName, object objValue, ParameterDirection paramDirection);
        void AddParameters(int index, string paramName, object objValue, ParameterDirection paramDirection);
        void AddInputParameters(string paramName, object objValue);
        void AddOutputParametersWithSize(string paramName, object objValue, int size);
        void AddInsertParameters(int index, string paramName, object objValue, ParameterDirection paramDirection);
        void AddInsertParameters(int index, string paramName, DbType dbType, string sourceColumn);
        void AddUpdateParameters(int index, string paramName, object objValue, ParameterDirection paramDirection);
        void AddUpdateParameters(int index, string paramName, DbType dbType, string sourceColumn);
        IDataReader ExecuteReader(CommandType commandType, string commandText);
        /// <summary>
        /// te perdoret kur duam te marrim nje string si output param
        /// </summary>
        /// <param name="index"></param>
        /// <param name="paramName"></param>
        /// <param name="objValue"></param>
        /// <param name="size"></param>
        void AddOutputParameterWithSize(int index, string paramName, string objValue, int size);

        DataSet ExecuteDataSet(CommandType commandType, string commandText);
        bool ExecuteInsert(DataTable dt, CommandType selectCommandType, string selectCommandText, CommandType insertCommandType, string insertCommandText);
        bool ExecuteInsert(DataTable dt, CommandType insertCommandType, string insertCommandText);
        bool ExecuteUpdate(DataTable dt, CommandType selectCommandType, string selectCommandText, CommandType updateCommandType, string updateCommandText);
        bool ExecuteUpdateMePKey(DataTable dt, CommandType selectCommandType, string selectCommandText, CommandType updateCommandType, string updateCommandText, object[] keys);
        object ExecuteScalar(CommandType commandType, string commandText);
        int ExecuteNonQuery(CommandType commandType, string commandText);

        IEnumerable<T> GetIEnumerbale<T>(string spName, Func<IDataRecord, T> buildObject);
        void FillCollection(string spName, IDataBaseReader collectionPerTuMbushur);
        void FillObject(string spName, IDataBaseReader objektiPerTuMbushur);
        bool FillObject<T>(string spName, Action<IDataRecord> fillObject);
        Dictionary<string, object> GetDictionary(string spName);
        Dictionary<TKey, TValue> GetDictionary<TKey, TValue>(string spName);
        List<T> GetList<T>(string spName);
        void CloseReader();
        void Close();
        void Dispose();
    
        void ExecuteSqlBulk(string tableName, DataTable dataTable);
    }
}