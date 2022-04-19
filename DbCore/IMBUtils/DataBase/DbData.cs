using System;
using System.Data;
using AlphaWeb.Core.Extensions;
using AlphaWeb.Core.Interfaces.Data;
using AlphaWeb.Infrastructure.Data.AdoNet;
using AlphaWeb.Infrastructure.Data.OthersDb;

namespace DbCore.IMBUtils.DataBase
{
    public class DbData : IDisposable, IDbData
    {
        protected IDbManager dbManager;
        protected bool disposed;
        protected static readonly string mesazhRuajtje = "Ruajtja perfundoi me sukses!";
        protected static readonly string mesazhModifikimi = "Modifikimi perfundoi me sukses!";
        protected static readonly string mesazhFshirje = "Fshirja perfundoi me sukses!";
        private transactionCache _idbTransactionCache;

        /// <summary>
        ///Nese instanca qe eshte krijuar brenda nje transactionscope, ath cache e ruajme ne nje dictionary brenda
        /// MyTransaction duke perdorur si key,identifikuesin e transaksionit,dhe me pas kur mbyllet scope (ne dispose)
        /// e lirojme memorien qe ka zene.Ne rast te kundert referenca e cache ruhet brenda DbData,dhe shkaterrohet bashke me
        /// objektin e dbData
        /// </summary>
        public transactionCache TransCache
        {
            get
            {
                //nese ekziston nje transaction key,do te thote qe ekziston nje transactionScope ne kete bllok kodi ku ndodhemi
                //kthejme cache koresponduese
                if (!string.IsNullOrEmpty(MyTransactionScope.TransactionKey))
                    return MyTransactionScope.CurrentTransactionCache;
                //ne rast te kundert kthejme objektin qe mbart dbdata
                if (_idbTransactionCache != null)
                    return _idbTransactionCache;

                _idbTransactionCache = new transactionCache();
                return _idbTransactionCache;
            }
            set
            {
                //set eshte funksionale vetem ne rastin kur transactionKey eshte bosh pasi trnsactionScope nuk ka nevoje per te 
                //percjell  cache,kjo eshte bere per backward compatibility te dbdata 
                if (string.IsNullOrEmpty(MyTransactionScope.TransactionKey))
                    _idbTransactionCache = value;
            }
        }
        /// <summary>
        /// instacon nje dbManger ne baze te connName
        /// </summary>
        /// <param name="connectionName"></param>
        /// <returns></returns>
        public DbManager NewDbManager(DataProviderType dataProviderType, string connectionName)
        {
            return new DbManager(dataProviderType, connectionName);
        }
        /// <summary>
        ///te jep qasje mbi dbMangerin e hapur,siguron qe brenda nje scope te kete vetem nje conn active.
        ///jashte scope-it te jep nej dbManager te ri
        /// </summary>
        public IDbManager MyScopeDbManager
        {
            get
            {
                IDbManager myScopeDbManager;
                if (!string.IsNullOrEmpty(MyTransactionScope.TransactionKey))
                {
                    //eshte brenda scope-it
                    myScopeDbManager = MyTransactionScope.CurrentDbConnection;
                    if (myScopeDbManager != null) return myScopeDbManager;
                    //hera e pare qe hapet connection
                    myScopeDbManager = NewDbManager(DataProviderType.SqlServer, MyConnectionsManager.GetSelectedConNameServer());
                    MyTransactionScope.DbConnections[MyTransactionScope.TransactionKey] = myScopeDbManager;
                    return myScopeDbManager;
                }
                //nuk eshte brenda nje scope-i
                myScopeDbManager = NewDbManager(DataProviderType.SqlServer, MyConnectionsManager.GetSelectedConNameServer());
                myScopeDbManager.Open();
                return myScopeDbManager;
            }
        }
        public DbData()
        {
            dbManager = MyScopeDbManager;
        }
        /// <summary>
        /// merr si parameer connection name.Duhet te perdoret ne rastin kur behen veprime ne databaza te ndryshme dhe connectionstring nuk mund ta marrim nga session sepse nuk e kemi aktiv
        /// </summary>
        /// <param name="connectionName"></param>
        public DbData(string connectionName)
        {
            dbManager = !string.IsNullOrWhiteSpace(connectionName) ? NewDbManager(DataProviderType.SqlServer, connectionName) : MyScopeDbManager;
        }
        public DbData(int timeout)
        {
            dbManager = MyScopeDbManager;
            dbManager.CommandTimeOut = timeout;
        }

        public DbData(IDbData db)
        {
            dbManager = db.GetDbManager();
            TransCache = db.GetTransactionCache();
        }

        public DbData(DbData db)
        {
            dbManager = db.dbManager;
            TransCache = db.TransCache;
        }

        public DbData(transactionCache trans)
        {
            dbManager = NewDbManager(DataProviderType.SqlServer, MyConnectionsManager.GetSelectedConNameServer());
            _idbTransactionCache = new transactionCache();
            _idbTransactionCache = trans.ShallowCopy();
        }

        public DbData(transactionCache trans, DataProviderType dataProviderType, string connectionString, bool oldDbManager = true)
        {
            //this method is only for db access
            oldDbManager.ThrowIfNotAllowed(false, nameof(oldDbManager), "This value is not allowed");

            var myDbManager = new OldDbManager(dataProviderType, connectionString);
            dbManager = myDbManager;
            _idbTransactionCache = new transactionCache();
            _idbTransactionCache = trans.ShallowCopy();
        }
        public DbData(DataProviderType dataProvider, string connectionString, bool oldDbManager = true)
        {
            oldDbManager.ThrowIfNotAllowed(false, nameof(oldDbManager), "This value is not allowed");

            var myDbManager = new OldDbManager
            {
                ProviderType = dataProvider,
                ConnectionString = connectionString
            };
            dbManager = myDbManager;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposeManagedResources)
        {
            if (disposed) return;
            if (disposeManagedResources)
            {
                if (dbManager != null)
                {
                    dbManager.Dispose();
                    dbManager = null;
                }
            }
            disposed = true;
        }

        public void vendosManager(IDbManager db)
        {
            dbManager = db;
        }

        public IDbManager merrManager()
        {
            return dbManager;
        }

        #region Metoda per Transaksionet

        /// <summary>
        /// Ben commit transaksionin
        /// </summary>
        /// <returns>True nese transaksioni behet commit me sukses, False perndryshe</returns>
        public bool commitTransaksion()
        {
            if (dbManager == null) return false;
            dbManager.CommitTransaction();
            Dispose();
            return true;
        }

        /// <summary>
        /// Ben rollback transaksionin
        /// </summary>
        /// <returns>True nese transaksioni behet rollback me sukses, False perndryshe</returns>
        public bool rollbackTransaksion()
        {
            if (dbManager == null) return false;
            if (dbManager.Transaction == null) return false;
            dbManager.Transaction.Rollback();
            Dispose();
            return true;
        }

        /// <summary>
        /// fillon nje transaksion
        /// </summary>
        /// <returns>True nese transaksioni krijohet me sukses, False perndryshe</returns>
        public bool beginTransaksion()
        {
            if (dbManager == null) return false;
            dbManager.Open();
            dbManager.BeginTransaction();
            return true;
        }
        public bool beginTransaksion(IsolationLevel level)
        {
            if (dbManager == null) return false;
            dbManager.Open();
            dbManager.BeginTransaction(level);
            return true;
        }
        public bool beginTransaksion(int timeOut)
        {
            if (dbManager == null) return false;
            dbManager.Open();
            dbManager.BeginTransaction(timeOut);

            return true;
        }

        public IDbManager GetDbManager()
        {
            return dbManager;
        }

        public transactionCache GetTransactionCache()
        {
            return TransCache;
        }
        #endregion Metoda per Transaksionet

    }
}