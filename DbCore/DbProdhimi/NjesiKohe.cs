using System;
using System.Collections.Generic;
using System.Linq;

namespace DbCore.DbProdhimi
{
    /// <summary>
    /// njesi kohe sek/min/ore/dite
    /// </summary>
    public enum NjesiKohe : int
    {
        /// <summary>
        /// njesi i papercaktuar 
        /// </summary>
        Undefined = 0,
        /// <summary>
        ///sekonda
        /// </summary>
        Sekonda = 1,
        /// <summary>
        /// Minuta       
        /// </summary>
        Minuta = 2,
        /// <summary>
        /// Ore
        /// </summary>
        Ore = 3,
        /// <summary>
        /// Dite
        /// </summary>
        Dite = 4,

    };
}
