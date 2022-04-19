using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DbCore.DbRegjistrim
{
   
    /// <summary>
    /// status transferimi i patrasferuar, transferuar, konvertuar
    /// </summary>
    public enum StatusTrasferimi
    {
        /// <summary>
        /// tip i patransferuar
        /// </summary>
        PaTransferuar = 0,
        /// <summary>
        /// transferuar ne winlinekarta
        /// </summary>
        Transferuar = 1,
        /// <summary>
        /// transferuar nga winlinekarta dhe i konvertuar ne fsh   
        /// </summary>
        Konvertuar = 2
    };
}