using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.IMBUtils.Logging
{
    public interface ILoggerImb
    {
        #region Loget e Buxhetimit
        void LogErrorBuxhetimi(string error);
        void LogWarningBuxhetimi(string warning);
        void LogInfoBuxhetimi(string info);
        #endregion
    }
}
