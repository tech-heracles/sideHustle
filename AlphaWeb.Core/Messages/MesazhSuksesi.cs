using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore
{
    public class MesazhSuksesi : clsMesazh, IMesazh
    {

        public MesazhSuksesi(string mesazhi) : base(true, mesazhi)
        {
        }
        public MesazhSuksesi() : base(true, string.Empty)
        {
        }


    }
}
