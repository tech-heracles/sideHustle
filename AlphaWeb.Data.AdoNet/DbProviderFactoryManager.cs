using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AlphaWeb.Core.Common;
using AlphaWeb.Core.Interfaces.Data;
using AlphaWeb.Infrastructure.Data.AdoNet.Exception;


namespace AlphaWeb.Infrastructure.Data.AdoNet
{
    public class DbProviderFactoryManager
    {
        private static readonly IDbProviderFactory[] Factories;

        static DbProviderFactoryManager()
        {
            if (Factories != null) return;

            Factories = ReflectionHelper.GetInterfaces<IDbProviderFactory>
            (
                Assembly.GetExecutingAssembly(),
                i => i.Name == typeof(IDbProviderFactory).Name
            );
        }

        internal static IDbProviderFactory Create(DataProviderType dataProvider)
        {
            var factory = Factories.FirstOrDefault(item => item.DataProvider == dataProvider);

            return factory ?? throw new DbProviderFactoryMissingException(dataProvider);
        }
    }
}