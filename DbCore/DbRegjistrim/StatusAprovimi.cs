using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    /// status aprovimi per aprovim,aprovuar,refuzuar
    /// </summary>
    public enum StatusAprovimi
    {
        /// <summary>
        /// tip i papercaktuar 
        /// </summary>
        Undefined = 0,
        /// <summary>
        /// per aprovim
        /// </summary>
        Per_Aprovim = 1,
        /// <summary>
        /// aprovuar    
        /// </summary>
        Aprovuar = 2,
        /// <summary>
        /// refuzuar
        /// </summary>
        Refuzuar = 3,
        /// <summary>
        /// deleguar
        /// </summary>
        Deleguar=4,
        /// <summary>
        /// modifikuar gjate ciklit te aprovimit
        /// </summary>
        Modifikuar=5
    };

    
}
