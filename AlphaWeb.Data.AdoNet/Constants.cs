using System.Transactions;

namespace AlphaWeb.Infrastructure.Data.AdoNet
{
    public class Constants
    {
        public const string DefaultConnectionName = "connStringAlpha";

        public const int TransactionTimeout = 6000;
        //Connection timeout in seconds
        public const int ConnectionTimout = 120;
        public const int StaticCommandTimeOut = 200;
        public const IsolationLevel IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
    }
}
