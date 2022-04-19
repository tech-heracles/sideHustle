using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore
{
    public class MesazhGabimi : clsMesazh
    {
        public MesazhGabimi() : base(false, string.Empty) { }
        public MesazhGabimi(string mesazhi) : base(false, mesazhi) { }
        public MesazhGabimi(Action<string> logger, string mesazhi) : base(false, mesazhi) { logger(mesazhi); }
    }
}
