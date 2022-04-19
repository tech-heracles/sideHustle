using DbCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCoreTests.Fake
{
    public class FakeClsMesazh : IMesazh
    {
        private TipMesazhi _tipi;
        private bool _status;
        private string _pershkrimMesazhi;

        public TipMesazhi Tipi {
            get { return _tipi; }
            set { _tipi = value; }
        }
        public bool Status
        {
            get { return _status; }
            set { _status = value; }
        }
        public string PershkrimMesazhi
        {
            get { return _pershkrimMesazhi; }
            set { _pershkrimMesazhi = value; }
        }
    }
}
