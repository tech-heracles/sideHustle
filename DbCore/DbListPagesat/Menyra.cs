using System;
using System.Collections.Generic;
using System.Linq;

namespace DbCore.DbListPagesat
{
    /// <summary>
    /// menyra  e llogarites se tatimeve progresive/totale
    /// </summary>
    public enum Menyra : int
    {
        /// <summary>
        /// tip i papercaktuar 
        /// </summary>
        Undefined = -1,
        /// <summary>
        /// progresive
        /// </summary>
        Progresive = 0,
        /// <summary>
        /// totale      
        /// </summary>
        Totale = 1,
    };
}
