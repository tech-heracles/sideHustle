using AlphaWeb.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaWeb.Infrastructure.Logging
{
    public class AlphaWebLogger : IAlphaWebLogger
    {
        public void LogInformation(string information)
        {
            System.Diagnostics.Debug.Write(information);
        }
    }
}
