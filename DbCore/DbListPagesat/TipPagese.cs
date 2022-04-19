using System;
using System.Collections.Generic;
using System.Linq;

namespace DbCore.DbListPagesat
{
    /// <summary>
    /// tip pagese  pagese/ndalese/llogaritese
    /// </summary>
    public enum TipPagese : int
    {
        /// <summary>
        /// tip i papercaktuar 
        /// </summary>
        Undefined = 0,
        /// <summary>
        /// pagese
        /// </summary>
        Pagese = 1,
        /// <summary>
        /// ndalese       
        /// </summary>
        Ndalese = 2,
        /// <summary>
        /// llogaritese
        /// </summary>
        Llogaritese = 3,

    };
}
