using System;
using System.Configuration;
using AlphaWeb.Core.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PlatinumWebTests.Infrastructure
{
    [TestClass]
    public class ConfigurationsTest
    {
        [TestMethod]
        public void ReadAlphaWebSection()
        {
            var alphaWebConfig = ConfigurationManager.GetSection("AlphaWebConfig") as AlphaWebConfig;
            Assert.IsNotNull(alphaWebConfig);
            Assert.IsNotNull(alphaWebConfig.DataSettings);
        }
    }
}
