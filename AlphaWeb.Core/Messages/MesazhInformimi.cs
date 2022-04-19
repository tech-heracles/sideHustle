using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore
{
    public class MesazhInformimi : clsMesazh
    {

        public MesazhInformimi(string mesazhi) : base(TipMesazhi.Informim, mesazhi) { }
        public MesazhInformimi() : base(TipMesazhi.Informim, string.Empty) { }
    }
}
