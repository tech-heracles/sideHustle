using System;
using System.Collections.Concurrent;
using System.Transactions;
using AlphaWeb.Core.Interfaces.Data;
using AlphaWeb.Infrastructure.Data.AdoNet;
using DbCore.IMBUtils.DataBase;

namespace DbCore
{
    public sealed class MyTransactionScope : IMyTransactionScope
    {
        private TransactionScope _transactionScope;
        private readonly string _currentTransactionKey;
        private static ConcurrentDictionary<string, transactionCache> _ambientTransactionCache;
        private static ConcurrentDictionary<string, IDbManager> _dbConnections;

        /// <summary>
        /// kontainer me objekte te tipit transactionCache,qe transaksione te ndryshme te kene pavarsi dhe te
        /// punojne ne paralel pa ndikuar tek njeri tjetri,pasi nje transaksion mbyllet (commit ose rollback) lirohet memoria qe
        /// mbante objektet e ketij transaksioni.E gjithe kjo realizohet duke perdorur nje id unike qe ka transaksioni
        /// </summary>
        private static ConcurrentDictionary<string, transactionCache> AmbientTransactionCache => _ambientTransactionCache ?? (_ambientTransactionCache = new ConcurrentDictionary<string, transactionCache>());

        public static ConcurrentDictionary<string, IDbManager> DbConnections => 
            _dbConnections ?? (_dbConnections = new ConcurrentDictionary<string, IDbManager>());

        /// <summary>
        /// kthen nje objekt transactionCache qe i perket tansaksionit ku jemi (nese jemi brenda nje transactionScope-i )
        /// nese nuk jemi ne transScope kthen null
        /// </summary>
        public static transactionCache CurrentTransactionCache
        {
            get
            {
                if (string.IsNullOrEmpty(TransactionKey))
                    return null;

                if (AmbientTransactionCache.TryGetValue(TransactionKey, out var currentCache))
                    return currentCache;

                currentCache = new transactionCache();
                AmbientTransactionCache[TransactionKey] = currentCache;
                return currentCache;
            }
            set
            {
                AmbientTransactionCache[TransactionKey] = value;
            }
        }
        /// <summary>
        /// kthen nje objekt dbManager qe perdoret nga currentScope
        /// </summary>
        public static IDbManager CurrentDbConnection
        {
            get
            {
                if (string.IsNullOrEmpty(TransactionKey))
                {
                    return null;
                }

                return DbConnections.TryGetValue(TransactionKey, out var currentConnection) ? currentConnection : null;
            }
        }

        public static string TransactionKey => Transaction.Current == null ? null : Transaction.Current.TransactionInformation.LocalIdentifier;

        /// <param name="scopeOption"></param>
        private MyTransactionScope(TransactionScopeOption scopeOption, DbData dbData) : this(scopeOption, dbData, 6000)
        {
        }

        /// <param name="scopeOption"></param>
        private MyTransactionScope(TransactionScopeOption scopeOption, DbData dbData, int timeOut)
        {
            var transCache = dbData?.TransCache;
            var transactionOptions = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted,
                Timeout = TimeSpan.FromSeconds(timeOut)
            };
            _transactionScope = new TransactionScope(scopeOption, transactionOptions);
            _currentTransactionKey = TransactionKey;
            if (dbData != null)
                CurrentTransactionCache = transCache;

        }

        /// <summary>
        /// krijon nje transaksion te ri readCommited
        /// </summary>
        public MyTransactionScope() : this(TransactionScopeOption.RequiresNew, null)
        {
        }

        /// <summary>
        /// krijon nje transaksion te ri readCommited
        /// </summary>
        public MyTransactionScope(DbData dbData, int timeOut)
            : this(TransactionScopeOption.RequiresNew, dbData, timeOut)
        {
        }

        /// <summary>
        /// krijon nje transaksion te ri readCommited
        /// </summary>
        public MyTransactionScope(DbData dbData) 
            : this(TransactionScopeOption.RequiresNew, dbData)
        {
        }
        /// <summary>
        /// krijon nje scope te ri me ose pa transaksion ne varesi te parametrit
        /// </summary>
        /// <param name="paTransaksion">true nese duhet me transaksion,false ne rast te kundert</param>
        public MyTransactionScope(bool paTransaksion) 
            : this(paTransaksion ? TransactionScopeOption.Suppress : TransactionScopeOption.RequiresNew, null)
        {
        }
        public void Complete() => _transactionScope.Complete();

        public void Complete(out DbData dbData)
        {
            var trans = new transactionCache();
            trans = CurrentTransactionCache.ShallowCopy();

            //MyTransactionScope.CurrentTransactionCache
            _transactionScope.Complete();
            dbData = new DbData(trans);
        }

        /// <summary>
        /// heq nga containeri static cache per transaksionin qe do shkaterrohet
        /// </summary>
        public void ClearCache()
        {
            //lirojme memorien e ketij transaksioni
            AmbientTransactionCache.TryRemove(_currentTransactionKey, out _);
            //  return currentCache;
        }
        public void ClearConnection()
        {
            DbConnections.TryRemove(_currentTransactionKey, out IDbManager _);
        }
        public void Dispose()
        {
            ClearCache();
            ClearConnection();
            _transactionScope.Dispose();
            _transactionScope = null;
        }
    }
}