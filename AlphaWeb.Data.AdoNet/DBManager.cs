using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using AlphaWeb.Core.Interfaces.Data;

namespace AlphaWeb.Infrastructure.Data.AdoNet
{
    public sealed class DbManager : IDbManager, IDisposable
    {
        private int _indeksiRadhes;
        private bool _disposed;

        private readonly IDbProviderFactory _dbManagerFactory;

        public DbManager(DataProviderType providerType, string connectionName)
            : this(providerType, Constants.StaticCommandTimeOut, connectionName)
        {

        }
        public DbManager(DataProviderType providerType, int timeout, string connectionName)
        {
            _dbManagerFactory = DbProviderFactoryManager.Create(providerType);
            ProviderType = providerType;
            ConnectionName = string.IsNullOrWhiteSpace(connectionName) ? Constants.DefaultConnectionName : connectionName;
            CommandTimeOut = timeout;
            _indeksiRadhes = 0;
            ConnectionString = ConnectionStringsManager.Instance.GetConnectionString(connectionName);
        }

        public string ConnectionString { get; private set; }

        public IDbConnection Connection { get; private set; }

        public string ConnectionName { get; }

        public IDataReader DataReader { get; set; }

        public DataProviderType ProviderType { get; set; }

        public IDbCommand Command { get; private set; }
        public IDbTransaction Transaction { get; private set; }

        public IList<IDbDataParameter> Parameters { get; private set; }
        public IList<IDbDataParameter> InsertParameters { get; private set; }

        public IList<IDbDataParameter> DeleteParameters { get; private set; }
        public IList<IDbDataParameter> UpdateParameters { get; private set; }
        public IDbCommand IdbInsertCommand { get; private set; }
        public IDbCommand IdbUpdateCommand { get; private set; }
        public int CommandTimeOut { get; set; }
        /// <summary>
        /// hap nje connection ne db
        /// </summary>
        public void Open()
        {
            EnsureConnectionOpen();
            Command = _dbManagerFactory.GetCommand();
            _indeksiRadhes = 0;
            ClearParameters();
        }

        /// <summary>
        /// hap lidhjen vetem nese nuk eshte e hapur, pa prekur komanden dhe parametrat
        /// </summary>
        private void EnsureConnectionOpen()
        {
            if (Connection == null)
                Connection = _dbManagerFactory.GetConnection();
            if (string.IsNullOrEmpty(Connection.ConnectionString))
                Connection.ConnectionString = ConnectionString;
            if (Connection.State != ConnectionState.Open)
                Connection.Open();
        }

        /// <summary>
        /// kthen lidhjen ne pool pas nje komande, pervec kur ky manager ka hapur vete nje SqlTransaction
        /// </summary>
        private void ReleaseConnection()
        {
            if (Transaction == null || System.Transactions.Transaction.Current != null)
                Connection?.Dispose();
        }
        /// <summary>
        /// mbyll conectionin e hapur
        /// </summary>
        public void Close()
        {
            if (Connection != null)
                if (Connection.State != ConnectionState.Closed)
                    Connection.Close();
            _indeksiRadhes = 0;
        }

        ~DbManager()
        {
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
            if (!_disposed)
            {
                if (disposeManagedResources)
                {
                    Close();
                    // dispose managed resources
                    if (Command != null)
                    {
                        Command.Dispose();
                        Command = null;
                    }
                    if (Transaction != null)
                    {
                        Transaction.Dispose();
                        Transaction = null;
                    }
                    if (Connection != null)
                    {
                        Connection.Dispose();
                        Connection = null;
                    }
                }
                // dispose unmanaged resources
                _disposed = true;
            }
            else
            {
            }
        }

        public void ClearParameters()
        {
            Parameters=new List<IDbDataParameter>();
        }

        public void CreateParameters(int paramsCount)
        {
            Parameters = _dbManagerFactory.CreateParameters(paramsCount);
        }

        public void CreateInsertParameters(int paramsCount)
        {
            InsertParameters = _dbManagerFactory.CreateParameters(paramsCount);
        }

        public void CreateUpdateParameters(int paramsCount)
        {
            UpdateParameters = _dbManagerFactory.CreateParameters(paramsCount);
        }
        public void AddOutputParameterWithSize(int index, string paramName, string objValue, int size)
        {
            if (index < Parameters.Count)
            {
                Parameters[index].ParameterName = paramName;
                Parameters[index].Value = objValue;
                Parameters[index].Direction = ParameterDirection.Output;
                Parameters[index].Size = size == 0 ? 500 : size;
            }

        }
        public void CreateDeleteParameters(int paramsCount)
        {
            DeleteParameters = _dbManagerFactory.CreateParameters(paramsCount);
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
            if (index < Parameters.Count)
            {
                Parameters[index].ParameterName = paramName;
                if (objValue == null)
                    Parameters[index].Value = DBNull.Value;
                else Parameters[index].Value = (objValue is DateTime) ? GetDatePerSql((DateTime)objValue) : objValue;
                Parameters[index].Direction = paramDirection;
                if (paramDirection == ParameterDirection.Output)
                    Parameters[index].Size = size;
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
            if (_indeksiRadhes >= Parameters.Count)
                throw new IndexOutOfRangeException("Indeksi i radhes ka arritur numrin max te paramterave");
            AddParameters(_indeksiRadhes, paramName, objValue, ParameterDirection.Input);
        }

        public void AddOutputParametersWithSize(string paramName, object objValue, int size)
        {
            if (_indeksiRadhes >= Parameters.Count)
                throw new IndexOutOfRangeException("Indeksi i radhes ka arritur numrin max te paramterave");
            AddParameters(_indeksiRadhes, paramName, objValue, ParameterDirection.Output, size);
        }

        public void AddInsertParameters(int index, string paramName, object objValue, ParameterDirection paramDirection)
        {
            if (index < InsertParameters.Count)
            {
                InsertParameters[index].ParameterName = paramName;
                InsertParameters[index].Value = objValue;
                InsertParameters[index].Direction = paramDirection;
                _indeksiRadhes++;
            }
        }

        public void AddInsertParameters(int index, string paramName, DbType dbType, string sourceColumn)
        {
            if (index < InsertParameters.Count)
            {
                InsertParameters[index].DbType = dbType;
                InsertParameters[index].ParameterName = paramName;
                InsertParameters[index].SourceColumn = sourceColumn;
                _indeksiRadhes++;
            }
        }

        public void AddDeleteParameters(int index, string paramName, object objValue, ParameterDirection paramDirection)
        {
            if (index < DeleteParameters.Count)
            {
                DeleteParameters[index].ParameterName = paramName;
                DeleteParameters[index].Value = objValue;
                DeleteParameters[index].Direction = paramDirection;
                _indeksiRadhes++;
            }
        }

        public void AddDeleteParameters(int index, string paramName, DbType dbType, string sourceColumn)
        {
            if (index < DeleteParameters.Count)
            {
                DeleteParameters[index].DbType = dbType;
                DeleteParameters[index].ParameterName = paramName;
                DeleteParameters[index].SourceColumn = sourceColumn;
                _indeksiRadhes++;
            }
        }

        public void AddUpdateParameters(int index, string paramName, DbType dbType, string sourceColumn)
        {
            if (index < UpdateParameters.Count)
            {
                UpdateParameters[index].DbType = dbType;
                UpdateParameters[index].ParameterName = paramName;
                UpdateParameters[index].SourceColumn = sourceColumn;
                _indeksiRadhes++;
            }
        }

        public void AddUpdateParameters(int index, string paramName, object objValue, ParameterDirection paramDirection)
        {
            if (index < UpdateParameters.Count)
            {
                UpdateParameters[index].ParameterName = paramName;
                UpdateParameters[index].Value = objValue;
                UpdateParameters[index].Direction = paramDirection;
                _indeksiRadhes++;
            }
        }

        public void BeginTransaction()
        {
            BeginTransaction(Constants.StaticCommandTimeOut);
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
            if (Transaction == null && System.Transactions.Transaction.Current == null)
                Transaction = _dbManagerFactory.GetTransaction(Connection, isolationLevel);
            Command.Transaction = Transaction;
            CommandTimeOut = timeout;
        }

        /// <summary>
        /// hap nje sqltransaction me isolation level te percaktuar nga perdoruesi
        /// </summary>
        /// <param name="isolationLevel"></param>
        public void BeginTransaction(IsolationLevel isolationLevel)
        {
            if (Transaction == null && System.Transactions.Transaction.Current == null)
                Transaction = _dbManagerFactory.GetTransaction(Connection, isolationLevel);
            Command.Transaction = Transaction;
            CommandTimeOut = Constants.StaticCommandTimeOut;
        }

        /// <summary>
        /// ben commit veprimet e kryera ne transaksion
        /// </summary>
        public void CommitTransaction()
        {
            Transaction?.Commit();
            Transaction = null;
        }

        public void RollBackTransaction()
        {
            Transaction?.Rollback();
            Transaction = null;
        }

        public IDataReader ExecuteReader(CommandType commandType, string
          commandText)
        {
            Command = _dbManagerFactory.GetCommand();
            PrepareCommand(Command, Transaction, commandType, commandText, Parameters);
            DataReader = Command.ExecuteReader();

            Command.Parameters.Clear();
            ReleaseConnection();

            return DataReader;
        }

        public void CloseReader()
        {
            DataReader?.Close();
        }

        private void AttachParameters(IDbCommand command, IList<IDbDataParameter> commandParameters)
        {
            foreach (IDbDataParameter idbParameter in commandParameters)
            {
                if ((idbParameter.Direction == ParameterDirection.InputOutput) && (idbParameter.Value == null))
                {
                    idbParameter.Value = DBNull.Value;
                }
                command.Parameters.Add(idbParameter);
            }
        }

        private void PrepareCommand(IDbCommand command, IDbTransaction transaction,
                                    CommandType commandType, string commandText, IList<IDbDataParameter> commandParameters
        )
        {
            EnsureConnectionOpen();
            command.Connection = Connection;
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

        /// <summary>
        /// Heq parametrat nga komanda qe te mund te riperdoren dhe liron lidhjen, edhe kur komanda deshton.
        /// Lidhja mbahet e hapur vetem kur ky manager ka hapur vete nje SqlTransaction.
        /// </summary>
        private void MbaroKomanden()
        {
            Command?.Parameters.Clear();
            ReleaseConnection();
        }

        public int ExecuteNonQuery(CommandType commandType, string commandText)
        {
            try
            {
                Command = _dbManagerFactory.GetCommand();
                PrepareCommand(Command, Transaction, commandType, commandText, Parameters);
                Command.CommandTimeout = CommandTimeOut;
                return Command.ExecuteNonQuery();
            }
            finally
            {
                MbaroKomanden();
            }
        }

        public object ExecuteScalar(CommandType commandType, string
          commandText)
        {
            try
            {
                Command = _dbManagerFactory.GetCommand();
                PrepareCommand(Command, Transaction, commandType, commandText, Parameters);
                Command.CommandTimeout = 120;
                return Command.ExecuteScalar();
            }
            finally
            {
                MbaroKomanden();
            }
        }

        public DataSet ExecuteDataSet(CommandType commandType, string
         commandText)
        {
            try
            {
                Command = _dbManagerFactory.GetCommand();
                Command.CommandTimeout = CommandTimeOut;
                PrepareCommand(Command, Transaction, commandType, commandText, Parameters);
                IDbDataAdapter dataAdapter = _dbManagerFactory.GetDataAdapter();
                dataAdapter.SelectCommand = Command;
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
                return dataSet;
            }
            finally
            {
                MbaroKomanden();
            }
        }
        public bool ExecuteUpdate(DataTable dt, CommandType selectCommandType, string selectCommandText, CommandType updateCommandType, string updateCommandText)
        {
            try
            {
                Command = _dbManagerFactory.GetCommand();
                PrepareCommand(Command, Transaction, selectCommandType, selectCommandText, Parameters);
                IDbDataAdapter dataAdapter = _dbManagerFactory.GetDataAdapter();
                dataAdapter.SelectCommand = Command;
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
                DataTable origDt = dataSet.Tables[0];
                DataColumn[] keys = new DataColumn[1];
                keys[0] = origDt.Columns[0];
                origDt.PrimaryKey = keys;
                origDt.Merge(dt);

                IdbUpdateCommand = _dbManagerFactory.GetCommand();
                PrepareCommand(IdbUpdateCommand, Transaction, updateCommandType, updateCommandText, UpdateParameters);
                dataAdapter.UpdateCommand = IdbUpdateCommand;
                dataAdapter.Update(dataSet);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
            finally
            {
                ReleaseConnection();
            }
        }

        public bool ExecuteUpdateMePKey(DataTable dt, CommandType selectCommandType, string selectCommandText, CommandType updateCommandType, string updateCommandText, object[] pkeys)
        {
            try
            {
                Command = _dbManagerFactory.GetCommand();
                PrepareCommand(Command, Transaction, selectCommandType, selectCommandText, Parameters);
                IDbDataAdapter dataAdapter = _dbManagerFactory.GetDataAdapter();
                dataAdapter.SelectCommand = Command;
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
                DataTable origDt = dataSet.Tables[0];
                DataColumn[] keys = new DataColumn[pkeys.Length];
                for (int i = 0; i < pkeys.Length; i++)
                {
                    keys[i] = origDt.Columns[int.Parse(pkeys[i].ToString())];
                }
                origDt.PrimaryKey = keys;
                origDt.Merge(dt);
                IdbUpdateCommand = _dbManagerFactory.GetCommand();
                PrepareCommand(IdbUpdateCommand, Transaction, updateCommandType, updateCommandText, UpdateParameters);
                dataAdapter.UpdateCommand = IdbUpdateCommand;
                dataAdapter.Update(dataSet);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
            finally
            {
                ReleaseConnection();
            }
        }

        public bool ExecuteInsert(DataTable dt, CommandType selectCommandType, string selectCommandText, CommandType insertCommandType, string insertCommandText)
        {
            try
            {
                Command = _dbManagerFactory.GetCommand();
                PrepareCommand(Command, Transaction, selectCommandType,
                    selectCommandText, Parameters);
                IDbDataAdapter dataAdapter = _dbManagerFactory.GetDataAdapter();
                dataAdapter.SelectCommand = Command;
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
                DataTable origDt = dataSet.Tables[0];
                foreach (DataRow row in dt.Rows)
                {
                    origDt.ImportRow(row);
                }
                IdbInsertCommand = _dbManagerFactory.GetCommand();
                PrepareCommand(IdbInsertCommand, Transaction, insertCommandType, insertCommandText, InsertParameters);
                dataAdapter.InsertCommand = IdbInsertCommand;
                dataAdapter.Update(dataSet);
                return true;
            }
            finally
            {
                ReleaseConnection();
            }
        }

        public bool ExecuteInsert(DataTable dt, CommandType insertCommandType, string insertCommandText)
        {
            try
            {
                IDbDataAdapter dataAdapter = _dbManagerFactory.GetDataAdapter();
                DataSet dataSet = new DataSet();
                dataSet.Tables.Add(dt);
                IdbInsertCommand = _dbManagerFactory.GetCommand();
                PrepareCommand(IdbInsertCommand, Transaction, insertCommandType, insertCommandText, InsertParameters);
                dataAdapter.TableMappings.Add("Table", dt.TableName);
                dataAdapter.InsertCommand = IdbInsertCommand;
                dataAdapter.Update(dataSet);
                return true;
            }
            finally
            {
                ReleaseConnection();
            }
        }

        /// <summary>
        /// kjo metode ben map nje objekt mbi nje table ,sipas rregullit qe percaktohet manualisht ne klasen e cila lidhet me table-n
        /// </summary>
        /// <typeparam name="T">Tipi i objektit</typeparam>
        /// <param name="spName">sp-ja qe do exe per te mbushur klasen</param>
        /// <param name="buildObject">funksioni i cili do bej map klasen ku ben pjese</param>
        /// <returns></returns>
        public IEnumerable<T> GetIEnumerbale<T>(string spName, Func<IDataRecord, T> buildObject)
        {
            try
            {
                BeginExecuting(spName);
                using (var reader = Command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        yield return buildObject(reader);
                    }
                }
            }
            finally
            {
                // ekzekutohet edhe kur thirresi ndalon para fundit (p.sh. FirstOrDefault)
                MbaroKomanden();
            }
        }
        public void FillCollection(string spName, IDataBaseReader collectionPerTuMbushur)
        {
            try
            {
                BeginExecuting(spName);
                using (var reader = Command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        collectionPerTuMbushur.Mbush(reader);
                    }
                }
            }
            finally
            {
                MbaroKomanden();
            }
        }

        /// <summary>
        /// mbush objektin qe i kalohet si parameter  nga sp e dhene
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="spName"></param>
        /// <param name="obj"></param>
        /// <param name="BuildObject"></param>
        /// <returns></returns>
        public bool FillObject<T>(string spName, Action<IDataRecord> fillObject)
        {
            try
            {
                BeginExecuting(spName);
                using (var reader = Command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        fillObject(reader);
                    }
                }
                return true;
            }
            finally
            {
                MbaroKomanden();
            }
        }
        public void FillObject(string spName, IDataBaseReader objektiPerTuMbushur)
        {
            try
            {
                BeginExecuting(spName);
                using (var reader = Command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        objektiPerTuMbushur.Mbush(reader);
                    }
                }
            }
            catch
            {
                // Sjellja e meparshme: gabimi nuk i kalon thirresit, objekti mbetet i pambushur.
                Console.WriteLine("Error encountered!");
            }
            finally
            {
                MbaroKomanden();
            }
        }
        public Dictionary<string, object> GetDictionary(string spName)
        {
            try
            {
                BeginExecuting(spName);
                var dic = new Dictionary<string, object>();
                using (var reader = Command.ExecuteReader())
                {
                    while (reader.Read())
                        dic.Add(reader[0].ToString(), reader[1]);
                }
                return dic;
            }
            finally
            {
                MbaroKomanden();
            }
        }

        public Dictionary<TKey, TValue> GetDictionary<TKey, TValue>(string spName)
        {
            try
            {
                BeginExecuting(spName);
                var dic = new Dictionary<TKey, TValue>();
                using (var reader = Command.ExecuteReader())
                {
                    while (reader.Read())
                        dic.Add((TKey)Convert.ChangeType(reader[0], typeof(TKey)), (TValue)Convert.ChangeType(reader[1], typeof(TValue)));
                }
                return dic;
            }
            finally
            {
                MbaroKomanden();
            }
        }

        public List<T> GetList<T>(string spName)
        {
            try
            {
                BeginExecuting(spName);
                var list = new List<T>();
                using (var reader = Command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var vlera = reader[0];
                        if (vlera == DBNull.Value)
                            continue;
                        list.Add((T)vlera);
                    }
                }
                return list;
            }
            finally
            {
                MbaroKomanden();
            }
        }

        private void BeginExecuting(string spName)
        {
            Command = _dbManagerFactory.GetCommand();
            Command.CommandTimeout = CommandTimeOut;
            PrepareCommand(Command, Transaction, CommandType.StoredProcedure, spName, Parameters);
        }

        public void ExecuteSqlBulk(string tableName, DataTable dataTable)
        {
            try
            {
                Open();
                using (var sqlBulk = new SqlBulkCopy((SqlConnection)Connection, SqlBulkCopyOptions.FireTriggers, (SqlTransaction)Transaction)
                {
                    DestinationTableName = tableName,
                    BulkCopyTimeout = 0
                })
                {
                    sqlBulk.WriteToServer(dataTable);
                }
            }
            finally
            {
                ReleaseConnection();
            }
        }
    }
}