using System;
using System.Runtime.Serialization;
using AlphaWeb.Core.Interfaces.Data;

namespace AlphaWeb.Infrastructure.Data.AdoNet.Exception
{
    [Serializable]
    public sealed class DbProviderFactoryMissingException : System.Exception
    {
        public DbProviderFactoryMissingException(DataProviderType dataProvider)
            : this(dataProvider, "dataProvider Factory is missing")
        {
        }

        public DbProviderFactoryMissingException(DataProviderType dataProvider, string message)
            : base(message)
        {
            DataProvider = dataProvider;
        }

        public DbProviderFactoryMissingException(DataProviderType dataProvider, string message, System.Exception innerException)
            : base(message, innerException)
        {
            DataProvider = dataProvider;
        }

        private DbProviderFactoryMissingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public DataProviderType DataProvider { get; set; }
    }
}