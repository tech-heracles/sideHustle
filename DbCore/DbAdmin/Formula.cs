using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DbCore.DbAdmin
{
    /// <summary>
    /// formula e dates se kujteses
    /// </summary>
    public enum Formula : int
    {
        /// <summary>
        /// tip i papercaktuar 
        /// </summary>
        Undefined = 0,
        /// <summary>
        ///  dite
        /// </summary>
        Dite = 1,
        /// <summary>
        /// jave       
        /// </summary>
        Jave = 2,
        /// <summary>
        ///  muaj
        /// </summary>
        Muaj = 3,

    };
}
