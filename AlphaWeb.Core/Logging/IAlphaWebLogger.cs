using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaWeb.Core.Logging
{
  public  interface IAlphaWebLogger
    {
        void LogInformation(string information);
    }
}
