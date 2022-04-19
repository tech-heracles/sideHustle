using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DbCore.DbOTC;
using DbCore.DbAdmin;
using DbCore.IMBUtils.Logging;

namespace DbCore.Integrime.OTC
{
    public class OTCPagesaAdapter
    {
        private static TimeSpan _myTimeout;
        public static TimeSpan MyTimeout
        {
            get
            {
                if (_myTimeout == null || _myTimeout.TotalMilliseconds == 0)
                    _myTimeout = TimeSpan.FromSeconds(clsServerConfiguration.LexoKonfigurimSipasKey<int>(ServerKonfigKey.OTC_PAGESA_TIMEOUT));
                return _myTimeout;
            }
        }

        public OTCPagesaAdapter()
        {
        }
    }
}
