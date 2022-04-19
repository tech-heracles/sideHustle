
using AlphaWeb.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaWeb.Services
{
    public class TestService : ITestService
    {
        private readonly IAlphaWebLogger _logger;
        public TestService(IAlphaWebLogger logger)
        {
            _logger = logger;
        }
        public string GetTestData()
        {
            _logger.LogInformation("GetTestData Called!");
            return Guid.NewGuid().ToString();
        }
    }
}
