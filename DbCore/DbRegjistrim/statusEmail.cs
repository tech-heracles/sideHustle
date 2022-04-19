using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    /// status emaili per dergim, derguar, gabim 
    /// </summary>
    public enum statusEmail
    {
        /// <summary>
        /// tip i papercaktuar 
        /// </summary>
        Undefined = 0,
        /// <summary>
        /// per dergim
        /// </summary>
        perDergim = 1,
        /// <summary>
        /// derguar    
        /// </summary>
        derguar = 2,
        /// <summary>
        /// gabim
        /// </summary>
        gabim = 3
    };
}
