using System;
using System.Collections.Generic;
using System.Linq;


namespace DbCore.DbListPagesat
{
    /// <summary>
    /// Njesi pagese nr/tab/formule
    /// </summary>
    public enum NjesiPagese : int
    { 
        /// <summary>
        /// tip i papercaktuar 
        /// </summary>
        Undefined = -1,
        /// <summary>
        /// numer
        /// </summary>
        Nr = 0,
        /// <summary>
        /// tabele       
        /// </summary>
        Tab = 1,
        /// <summary>
        /// formule
        /// </summary>
        For = 2,
       
    };
}
