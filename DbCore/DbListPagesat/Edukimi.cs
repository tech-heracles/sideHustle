using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DbCore.DbListPagesat
{ 
    /// <summary>
    /// edukimi shkolle e mesme/e larte/master
    /// </summary>
    public enum Edukimi : int
    {
        /// <summary>
        /// tip i papercaktuar 
        /// </summary>
        Undefined = -1,
        /// <summary>
        ///  shkolle e mesme
        /// </summary>
        Shkolle_Mesme = 1,
        /// <summary>
        /// shkolle e larte     
        /// </summary>
        Shkolle_Larte = 2,
        /// <summary>
        /// master
        /// </summary>
        Master = 3,

    };
}
