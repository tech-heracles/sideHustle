using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using AlphaWeb.Core.Interfaces.Data;

namespace AlphaWeb.Infrastructure.Data.OthersDb
{
    public sealed class OldDbManager : IDbManager, IDisposable
    {
        public const string DefaultConnectionName = "connStringAlpha";
        private readonly string connectionName;
        private int _indeksiRadhes;
        private IDbConnection idbConnection;
        private IDataReader idataReader;
        private IDbCommand idbCommand;
        private IDbCommand idbInsertCommand;
        private IDbCommand idbUpdateCommand;

        //private IDbCommand idbDeleteCommand;
        private DataProviderType providerType;

        private IDbTransaction idbTransaction = null;
        private IDbDataParameter[] idbParameters = null;
        private IDbDataParameter[] idbInsertParameters = null;
        private IDbDataParameter[] idbUpdateParameters = null;
        private IDbDataParameter[] idbDeleteParameters = null;
        private string strConnection;

        private bool disposed = false;
        private int commandTimeOut;
        private const int staticCommandTimeOut = 400;
        public OldDbManager()
        {
            _indeksiRadhes = 0;
        }

        public OldDbManager(DataProviderType providerType, string connectionString)
            : this(providerType, staticCommandTimeOut, connectionString)
        {

        }

        public OldDbManager(DataProviderType providerType, int timeout, string connectionString)
        {
            this.providerType = providerType;
            this.ConnectionString = connectionString;
            commandTimeOut = timeout;
            _indeksiRadhes = 0;
        }



        public IDbConnection Connection => idbConnection;

        public string ConnectionName => string.IsNullOrWhiteSpace(connectionName) ? DefaultConnectionName : connectionName;


        public IDataReader DataReader
        {
            get
            {
                return idataReader;
            }
            set
            {
                idataReader = value;
            }
        }

        public DataProviderType ProviderType
        {
            get
            {
                return providerType;
            }
            set
            {
                providerType = value;
            }
        }

        public string ConnectionString
        {
            get
            {
                return strConnection;
            }
            set
            {
                strConnection = value;
            }
        }

        public IDbCommand Command => idbCommand;

        public IDbTransaction Transaction => idbTransaction;

        public IDbDataParameter[] Parameters => idbParameters;

        public IDbDataParameter[] InsertParameters => idbInsertParameters;

        public IDbDataParameter[] DeleteParameters => idbDeleteParameters;

        public IDbDataParameter[] UpdateParameters => idbUpdateParameters;

        public int CommandTimeOut
        {
            get
            {
                return commandTimeOut;
            }

            set
            {
                commandTimeOut = value;
            }
        }

        IList<IDbDataParameter> IDbManager.Parameters => idbParameters;
        /// <summary>
        /// hap nje connection ne db
        /// </summary>
        public void Open()
        {
            if (idbConnection == null)
                idbConnection = DbManagerFactory.GetConnection(providerType);
            if (string.IsNullOrEmpty(idbConnection.ConnectionString))
                idbConnection.ConnectionString = ConnectionString;
            if (idbConnection.State != ConnectionState.Open)
                idbConnection.Open();
            idbCommand = DbManagerFactory.GetCommand(ProviderType);
            _indeksiRadhes = 0;
            ClearParameters();
        }
        /// <summary>
        /// mbyll conectionin e hapur
        /// </summary>
        public void Close()
        {
            if (idbConnection != null)
                if (idbConnection.State != ConnectionState.Closed)
                    idbConnection.Close();
            _indeksiRadhes = 0;
        }

        ~OldDbManager()
        {
            //System.Diagnostics.Trace.WriteLine("DBManager: Destructor");
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposeManagedResources)
        {
            // process only if mananged and unmanaged resources have
            // not been disposed of.
            if (!disposed)
            {
                //System.Diagnostics.Trace.WriteLine("DBManager: Resources not disposed");
                if (disposeManagedResources)
                {
                    //System.Diagnostics.Trace.WriteLine("DBManager: Closing connection");
                    Close();
                    //System.Diagnostics.Trace.WriteLine("DBManager: Disposing managed resources");
                    // dispose managed resources
                    if (idbCommand != null)
                    {
                        idbCommand.Dispose();
                        idbCommand = null;
                    }
                    if (idbTransaction != null)
                    {
                        idbTransaction.Dispose();
                        idbTransaction = null;
                    }
                    if (idbConnection != null)
                    {
                        idbConnection.Dispose();
                        idbConnection = null;
                    }
                }
                // dispose unmanaged resources
                //System.Diagnostics.Trace.WriteLine("DBManager: Disposing unmanaged resouces");
                disposed = true;
            }
            else
            {
                //System.Diagnostics.Trace.WriteLine("DBManager: Resources already disposed");
            }
        }

        public void ClearParameters()
        {
            idbParameters = new IDbDataParameter[0];
            idbParameters = DbManagerFactory.GetParameters(ProviderType, 0);
        }

        public void CreateParameters(int paramsCount)
        {
            idbParameters = new IDbDataParameter[paramsCount];
            idbParameters = DbManagerFactory.GetParameters(ProviderType,
              paramsCount);
        }

        public void CreateInsertParameters(int paramsCount)
        {
            idbInsertParameters = new IDbDataParameter[paramsCount];
            idbInsertParameters = DbManagerFactory.GetParameters(ProviderType,
              paramsCount);
        }

        public void CreateUpdateParameters(int paramsCount)
        {
            idbUpdateParameters = new IDbDataParameter[paramsCount];
            idbUpdateParameters = DbManagerFactory.GetParameters(ProviderType,
              paramsCount);
        }
        public void AddOutputParameterWithSize(int index, string paramName, string objValue, int size)
        {
            if (index < idbParameters.Length)
            {
                idbParameters[index].ParameterName = paramName;
                idbParameters[index].Value = objValue;
                idbParameters[index].Direction = ParameterDirection.Output;
                idbParameters[index].Size = 500;
            }
        }
        public void CreateDeleteParameters(int paramsCount)
        {
            idbDeleteParameters = new IDbDataParameter[paramsCount];
            idbDeleteParameters = DbManagerFactory.GetParameters(ProviderType,
              paramsCount);
        }

        public void AddParameters(string paramName, object objValue, ParameterDirection paramDirection)
        {
            AddParameters(_indeksiRadhes, paramName, objValue, paramDirection, 0);
        }

        public void AddParameters(int index, string paramName, object objValue, ParameterDirection paramDirection)
        {
            AddParameters(index, paramName, objValue, paramDirection, 0);
        }
        public void AddParameters(int index, string paramName, object objValue, ParameterDirection paramDirection, int size)
        {
            if (index < idbParameters.Length)
            {
                idbParameters[index].ParameterName = paramName;
                if (objValue == null)
                    idbParameters[index].Value = DBNull.Value;
                else idbParameters[index].Value = (objValue is DateTime) ? GetDatePerSql((DateTime)objValue) : objValue;
                idbParameters[index].Direction = paramDirection;
                if (paramDirection == ParameterDirection.Output)
                    idbParameters[index].Size = size;
                _indeksiRadhes++;
            }
        }

        private object GetDatePerSql(DateTime date)
        {
            if (((DateTime)SqlDateTime.MinValue < date))
                return date;
            return DBNull.Value;
        }

        public void AddInputParameters(string paramName, object objValue)
        {
            if (_indeksiRadhes >= idbParameters.Length)
                throw new Exception("Indeksi i radhes ka arritur numrin max te paramterave");
            AddParameters(_indeksiRadhes, paramName, objValue, ParameterDirection.Input);
        }

        public void AddOutputParametersWithSize(string paramName, object objValue, int size)
        {
            if (_indeksiRadhes >= idbParameters.Length)
                throw new Exception("Indeksi i radhes ka arritur numrin max te paramterave");
            AddParameters(_indeksiRadhes, paramName, objValue, ParameterDirection.Output, size);
        }

        public void AddInsertParameters(int index, string paramName, object objValue, ParameterDirection paramDirection)
        {
            if (index < idbInsertParameters.Length)
            {
                idbInsertParameters[index].ParameterName = paramName;
                idbInsertParameters[index].Value = objValue;
                idbInsertParameters[index].Direction = paramDirection;
                _indeksiRadhes++;
            }
        }

        public void AddInsertParameters(int index, string paramName, DbType dbType, string sourceColumn)
        {
            if (index < idbInsertParameters.Length)
            {
                idbInsertParameters[index].DbType = dbType;
                idbInsertParameters[index].ParameterName = paramName;
                idbInsertParameters[index].SourceColumn = sourceColumn;
                _indeksiRadhes++;
            }
        }

        public void AddDeleteParameters(int index, string paramName, object objValue, ParameterDirection paramDirection)
        {
            if (index < idbDeleteParameters.Length)
            {
                idbDeleteParameters[index].ParameterName = paramName;
                idbDeleteParameters[index].Value = objValue;
                idbDeleteParameters[index].Direction = paramDirection;
                _indeksiRadhes++;
            }
        }

        public void AddDeleteParameters(int index, string paramName, DbType dbType, string sourceColumn)
        {
            if (index < idbDeleteParameters.Length)
            {
                idbDeleteParameters[index].DbType = dbType;
                idbDeleteParameters[index].ParameterName = paramName;
                idbDeleteParameters[index].SourceColumn = sourceColumn;
                _indeksiRadhes++;
            }
        }

        public void AddUpdateParameters(int index, string paramName, DbType dbType, string sourceColumn)
        {
            if (index < idbUpdateParameters.Length)
            {
                idbUpdateParameters[index].DbType = dbType;
                idbUpdateParameters[index].ParameterName = paramName;
                idbUpdateParameters[index].SourceColumn = sourceColumn;
                _indeksiRadhes++;
            }
        }

        public void AddUpdateParameters(int index, string paramName, object objValue, ParameterDirection paramDirection)
        {
            if (index < idbUpdateParameters.Length)
            {
                idbUpdateParameters[index].ParameterName = paramName;
                idbUpdateParameters[index].Value = objValue;
                idbUpdateParameters[index].Direction = paramDirection;
                _indeksiRadhes++;
            }
        }

        public void BeginTransaction()
        {
            BeginTransaction(staticCommandTimeOut);
        }
        /// <summary>
        /// hap nje sqltransaction vetem nese nuk ka ambient transaction
        /// </summary>
        /// <param name="timeout"></param>
        public void BeginTransaction(int timeout)
        {
            BeginTransaction(timeout, IsolationLevel.RepeatableRead);

        }

        public void BeginTransaction(int timeout, IsolationLevel isolationLevel)
        {
            if (idbTransaction == null && System.Transactions.Transaction.Current == null)
                idbTransaction = DbManagerFactory.GetTransaction(ProviderType, idbConnection, isolationLevel);
            idbCommand.Transaction = idbTransaction;
            commandTimeOut = timeout;
            //this.idbConnection.ConnectionTimeout = 0;
        }

        /// <summary>
        /// hap nje sqltransaction me isolation level te percaktuar nga perdoruesi
        /// </summary>
        /// <param name="isolationLevel"></param>
        public void BeginTransaction(IsolationLevel isolationLevel)
        {
            if (idbTransaction == null && System.Transactions.Transaction.Current == null)
                idbTransaction = DbManagerFactory.GetTransaction(ProviderType, idbConnection, isolationLevel);
            idbCommand.Transaction = idbTransaction;
            commandTimeOut = staticCommandTimeOut;
            //this.idbConnection.ConnectionTimeout = 0;
        }

        /// <summary>
        /// ben commit veprimet e kryera ne transaksion
        /// </summary>
        public void CommitTransaction()
        {
            idbTransaction?.Commit();
            idbTransaction = null;
        }

        public void RollBackTransaction()
        {
            idbTransaction?.Rollback();
            idbTransaction = null;
        }

        public IDataReader ExecuteReader(CommandType commandType, string
          commandText)
        {
            idbCommand = DbManagerFactory.GetCommand(ProviderType);
            idbCommand.Connection = Connection;
            PrepareCommand(idbCommand, Connection, Transaction,
             commandType,
              commandText, Parameters);
            DataReader = idbCommand.ExecuteReader();
            idbCommand.Parameters.Clear();
            if (Transaction == null || System.Transactions.Transaction.Current != null)
                Connection.Dispose();
            return DataReader;
        }

        public void CloseReader()
        {
            DataReader?.Close();
        }

        private void AttachParameters(IDbCommand command,
          IDbDataParameter[] commandParameters)
        {
            foreach (IDbDataParameter idbParameter in commandParameters)
            {
                if ((idbParameter.Direction == ParameterDirection.InputOutput)
                &&
                  (idbParameter.Value == null))
                {
                    idbParameter.Value = DBNull.Value;
                }
                command.Parameters.Add(idbParameter);
            }
        }

        private void PrepareCommand(IDbCommand command, IDbConnection
          connection,
          IDbTransaction transaction, CommandType commandType, string
          commandText,
          IDbDataParameter[] commandParameters)
        {
            command.Connection = connection;
            command.CommandText = commandText;
            command.CommandType = commandType;

            if (transaction != null)
            {
                command.Transaction = transaction;
            }

            if (commandParameters != null)
            {
                AttachParameters(command, commandParameters);
            }
        }

        public int ExecuteNonQuery(CommandType commandType, string
          commandText)
        {
            idbCommand = DbManagerFactory.GetCommand(ProviderType);
            PrepareCommand(idbCommand, Connection, Transaction,
            commandType, commandText, Parameters);
            //if (this.Transaction != null)
            idbCommand.CommandTimeout = commandTimeOut;
            //else
            //    idbCommand.CommandTimeout = 600;
            int returnValue = idbCommand.ExecuteNonQuery();
            idbCommand.Parameters.Clear();
            if (Transaction == null || System.Transactions.Transaction.Current != null)
                Connection.Dispose();
            //idbParameters = null;
            return returnValue;
        }

        public object ExecuteScalar(CommandType commandType, string
          commandText)
        {
            idbCommand = DbManagerFactory.GetCommand(ProviderType);
            PrepareCommand(idbCommand, Connection, Transaction,
            commandType,
              commandText, Parameters);
            idbCommand.CommandTimeout = 120;
            object returnValue = idbCommand.ExecuteScalar();
            idbCommand.Parameters.Clear();
            if (Transaction == null || System.Transactions.Transaction.Current != null)
                Connection.Dispose();
            return returnValue;
        }

        public DataSet ExecuteDataSet(CommandType commandType, string
         commandText)
        {
            idbCommand = DbManagerFactory.GetCommand(ProviderType);
            //if (this.Transaction == null)
            idbCommand.CommandTimeout = commandTimeOut;
            PrepareCommand(idbCommand, Connection, Transaction,
           commandType,
              commandText, Parameters);
            IDbDataAdapter dataAdapter = DbManagerFactory.GetDataAdapter
              (ProviderType);
            dataAdapter.SelectCommand = idbCommand;
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            idbCommand.Parameters.Clear();
            if (Transaction == null || System.Transactions.Transaction.Current != null)
                Connection.Dispose();
            return dataSet;
        }
        public bool ExecuteUpdate(DataTable dt, CommandType selectCommandType, string selectCommandText, CommandType updateCommandType, string updateCommandText)
        {
            try
            {
                idbCommand = DbManagerFactory.GetCommand(ProviderType);
                PrepareCommand(idbCommand, Connection, Transaction, selectCommandType,
                    selectCommandText, Parameters);
                IDbDataAdapter dataAdapter = DbManagerFactory.GetDataAdapter(ProviderType);
                dataAdapter.SelectCommand = idbCommand;
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
                DataTable origDt = dataSet.Tables[0];
                DataColumn[] keys = new DataColumn[1];
                keys[0] = origDt.Columns[0];
                origDt.PrimaryKey = keys;
                origDt.Merge(dt);

                idbUpdateCommand = DbManagerFactory.GetCommand(ProviderType);
                PrepareCommand(idbUpdateCommand, Connection, Transaction, updateCommandType, updateCommandText, UpdateParameters);
                dataAdapter.UpdateCommand = idbUpdateCommand;
                dataAdapter.Update(dataSet);
                if (Transaction == null || System.Transactions.Transaction.Current != null)
                    Connection.Dispose();
                return true;
            }
            catch (Exception)
            {
                return false;
                //throw;
            }
        }

        public bool ExecuteUpdateMePKey(DataTable dt, CommandType selectCommandType, string selectCommandText, CommandType updateCommandType, string updateCommandText, object[] pkeys)
        {
            try
            {
                idbCommand = DbManagerFactory.GetCommand(ProviderType);
                PrepareCommand(idbCommand, Connection, Transaction, selectCommandType,
                    selectCommandText, Parameters);
                IDbDataAdapter dataAdapter = DbManagerFactory.GetDataAdapter(ProviderType);
                dataAdapter.SelectCommand = idbCommand;
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
                DataTable origDt = dataSet.Tables[0];
                DataColumn[] keys = new DataColumn[pkeys.Length];
                for (int i = 0; i < pkeys.Length; i++)
                {
                    keys[i] = origDt.Columns[int.Parse(pkeys[i].ToString())];
                }
                //keys[0] = origDt.Columns[0];
                //keys[1] = origDt.Columns[1];
                origDt.PrimaryKey = keys;
                origDt.Merge(dt);
                //foreach (DataRow rreshti in dt.Rows)
                //{
                //    origDt.LoadDataRow(rreshti.ItemArray, true);
                //}
                idbUpdateCommand = DbManagerFactory.GetCommand(ProviderType);
                PrepareCommand(idbUpdateCommand, Connection, Transaction, updateCommandType, updateCommandText, UpdateParameters);
                dataAdapter.UpdateCommand = idbUpdateCommand;
                dataAdapter.Update(dataSet);
                if (Transaction == null || System.Transactions.Transaction.Current != null)
                    Connection.Dispose();
                return true;
            }
            catch (Exception)
            {
                return false;
                //throw;
            }
        }

        public bool ExecuteInsert(DataTable dt, CommandType selectCommandType, string selectCommandText, CommandType insertCommandType, string insertCommandText)
        {
            idbCommand = DbManagerFactory.GetCommand(ProviderType);
            PrepareCommand(idbCommand, Connection, Transaction, selectCommandType,
                selectCommandText, Parameters);
            IDbDataAdapter dataAdapter = DbManagerFactory.GetDataAdapter(ProviderType);
            dataAdapter.SelectCommand = idbCommand;
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            DataTable origDt = dataSet.Tables[0];
            foreach (DataRow row in dt.Rows)
            {
                origDt.ImportRow(row);
            }
            idbInsertCommand = DbManagerFactory.GetCommand(ProviderType);
            PrepareCommand(idbInsertCommand, Connection, Transaction, insertCommandType, insertCommandText, InsertParameters);
            dataAdapter.InsertCommand = idbInsertCommand;
            dataAdapter.Update(dataSet);
            if (Transaction == null || System.Transactions.Transaction.Current != null)
                Connection.Dispose();
            return true;
        }

        public bool ExecuteInsert(DataTable dt, CommandType insertCommandType, string insertCommandText)
        {
            IDbDataAdapter dataAdapter = DbManagerFactory.GetDataAdapter(ProviderType);
            DataSet dataSet = new DataSet();
            dataSet.Tables.Add(dt);
            idbInsertCommand = DbManagerFactory.GetCommand(ProviderType);
            PrepareCommand(idbInsertCommand, Connection, Transaction, insertCommandType, insertCommandText, InsertParameters);
            dataAdapter.TableMappings.Add("Table", dt.TableName);
            dataAdapter.InsertCommand = idbInsertCommand;
            dataAdapter.Update(dataSet);
            if (Transaction == null || System.Transactions.Transaction.Current != null)
                Connection.Dispose();
            return true;
        }

        /// <summary>
        /// kjo metode ben map nje objekt mbi nje table ,sipas rregullit qe percaktohet manualisht ne klasen e cila lidhet me table-n
        /// </summary>
        /// <typeparam name="T">Tipi i objektit</typeparam>
        /// <param name="spName">sp-ja qe do exe per te mbushur klasen</param>
        /// <param name="BuildObject">funksioni i cili do bej map klasen ku ben pjese</param>
        /// <returns></returns>
        public IEnumerable<T> GetIEnumerbale<T>(string spName, Func<IDataRecord, T> BuildObject)
        {
            idbCommand = DbManagerFactory.GetCommand(ProviderType);
            idbCommand.Connection = Connection;
            idbCommand.CommandTimeout = commandTimeOut;
            PrepareCommand(idbCommand, Connection, Transaction, CommandType.StoredProcedure, spName, Parameters);

            var reader = idbCommand.ExecuteReader();
            while (reader.Read())
            {
                yield return BuildObject(reader);
            }
            reader.Close();
            idbCommand.Parameters.Clear();
            if (Transaction == null || System.Transactions.Transaction.Current != null)
                Connection.Dispose();
        }
        public void FillCollection(string spName, IDataBaseReader collectionPerTuMbushur)
        {
            idbCommand = DbManagerFactory.GetCommand(ProviderType);
            idbCommand.Connection = Connection;
            idbCommand.CommandTimeout = commandTimeOut;
            PrepareCommand(idbCommand, Connection, Transaction, CommandType.StoredProcedure, spName, Parameters);

            var reader = idbCommand.ExecuteReader();
            while (reader.Read())
            {
                collectionPerTuMbushur.Mbush(reader);
            }
            reader.Close();
            idbCommand.Parameters.Clear();
            if (Transaction == null || System.Transactions.Transaction.Current != null)
                Connection.Dispose();
        }

        /// <summary>
        /// mbush objektin qe i kalohet si parameter  nga sp e dhene
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="spName"></param>
        /// <param name="obj"></param>
        /// <param name="BuildObject"></param>
        /// <returns></returns>
        public bool FillObject<T>(string spName, Action<IDataRecord> FillObject)
        {
            idbCommand = DbManagerFactory.GetCommand(ProviderType);
            idbCommand.Connection = Connection;
            idbCommand.CommandTimeout = commandTimeOut;
            PrepareCommand(idbCommand, Connection, Transaction, CommandType.StoredProcedure, spName, Parameters);

            var reader = idbCommand.ExecuteReader();
            int countRows = 0;
            while (reader.Read())
            {
                FillObject(reader);
                countRows++;
            }
            if (countRows > 1)
            {
                // TO DO GETSON  throw new Exception($"SP '{idbCommand.CommandText}' e thirrur nga metoda  '{FillObject.Method.Name}' ktheu me teper se nje rresht!");
            }
            reader.Close();
            idbCommand.Parameters.Clear();
            if (Transaction == null || System.Transactions.Transaction.Current != null)
                Connection.Dispose();
            return true;
        }
        public void FillObject(string spName, IDataBaseReader objektiPerTuMbushur)
        {
            idbCommand = DbManagerFactory.GetCommand(ProviderType);
            idbCommand.Connection = Connection;
            idbCommand.CommandTimeout = commandTimeOut;
            PrepareCommand(idbCommand, Connection, Transaction, CommandType.StoredProcedure, spName, Parameters);

            var reader = idbCommand.ExecuteReader();
            int countRows = 0;
            while (reader.Read())
            {
                objektiPerTuMbushur.Mbush(reader);
                countRows++;
            }
            if (countRows > 1)
            {
                // TO DO GETSON  throw new Exception($"SP '{idbCommand.CommandText}' e thirrur nga metoda  '{FillObject.Method.Name}' ktheu me teper se nje rresht!");
            }
            reader.Close();
            idbCommand.Parameters.Clear();
            if (Transaction == null || System.Transactions.Transaction.Current != null)
                Connection.Dispose();

        }
        public Dictionary<string, object> GetDictionary(string spName)
        {
            idbCommand = DbManagerFactory.GetCommand(ProviderType);
            idbCommand.Connection = Connection;
            idbCommand.CommandTimeout = commandTimeOut;
            PrepareCommand(idbCommand, Connection, Transaction, CommandType.StoredProcedure, spName, Parameters);

            var reader = idbCommand.ExecuteReader();
            var dic = new Dictionary<string, object>();
            while (reader.Read())
                dic.Add(reader[0].ToString(), reader[1]);
            reader.Close();
            idbCommand.Parameters.Clear();
            if (Transaction == null || System.Transactions.Transaction.Current != null)
                Connection.Dispose();
            return dic;
        }

        public Dictionary<TKey, TValue> GetDictionary<TKey, TValue>(string spName)
        {
            idbCommand = DbManagerFactory.GetCommand(ProviderType);
            idbCommand.Connection = Connection;
            idbCommand.CommandTimeout = commandTimeOut;
            PrepareCommand(idbCommand, Connection, Transaction, CommandType.StoredProcedure, spName, Parameters);

            var reader = idbCommand.ExecuteReader();
            var dic = new Dictionary<TKey, TValue>();
            while (reader.Read())
                dic.Add((TKey)Convert.ChangeType(reader[0], typeof(TKey)), (TValue)Convert.ChangeType(reader[1], typeof(TValue)));

            reader.Close();
            idbCommand.Parameters.Clear();
            if (Transaction == null || System.Transactions.Transaction.Current != null)
                Connection.Dispose();
            return dic;
        }

        public List<T> GetList<T>(string spName)
        {
            idbCommand = DbManagerFactory.GetCommand(ProviderType);
            idbCommand.Connection = Connection;
            idbCommand.CommandTimeout = commandTimeOut;
            PrepareCommand(idbCommand, Connection, Transaction, CommandType.StoredProcedure, spName, Parameters);

            var reader = idbCommand.ExecuteReader();
            var list = new List<T>();
            while (reader.Read())
            {
                var vlera = reader[0];
                if (vlera == DBNull.Value)
                    continue;
                list.Add((T)vlera);
            }
            reader.Close();
            idbCommand.Parameters.Clear();
            if (Transaction == null || System.Transactions.Transaction.Current != null)
                Connection.Dispose();
            return list;
        }

        public void ExecuteSqlBulk(string tableName, DataTable dataTable)
        {
            var sqlBulk = new SqlBulkCopy((SqlConnection)this.Connection, SqlBulkCopyOptions.FireTriggers,
                (SqlTransaction)this.Transaction)
            { DestinationTableName = tableName };
            this.Open();
            sqlBulk.WriteToServer(dataTable);

            if (Transaction == null || System.Transactions.Transaction.Current != null)
                Connection.Dispose();
        }
    }
}