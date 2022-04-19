using System;
using System.Collections.Generic;
using System.Linq;

namespace DbCore.DbListPagesat
{
    /// <summary>
    /// lloj pagese mujore/ditore/orare
    /// </summary>
    public enum LlojPagese : int
    {
        /// <summary>
        /// lloj i papercaktuar 
        /// </summary>
        Undefined = 0,
        /// <summary>
        /// mujore
        /// </summary>
        Mujore = 1,
        /// <summary>
        /// ditore       
        /// </summary>
        Ditore = 2,
        /// <summary>
        /// orare
        /// </summary>
        Orare = 3,

    };
}
