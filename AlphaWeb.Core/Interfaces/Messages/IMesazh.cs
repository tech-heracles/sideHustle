using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore
{
   public interface IMesazh
    {
        TipMesazhi Tipi { get; set; }
        bool Status { get; set; }

        string PershkrimMesazhi { get; set; }

    }
}
