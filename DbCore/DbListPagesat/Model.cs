using System;
using System.Collections.Generic;
using System.Linq;

namespace DbCore.DbListPagesat
{
     /// <summary>
    /// model /default/ nga perdoruesi
    /// </summary>
    public enum Model : int
    {
        /// <summary>
        /// tip i papercaktuar 
        /// </summary>
        Undefined = -1,
        /// <summary>
        /// komponentet model
        /// </summary>
        Modeli = 0,
        /// <summary>
        /// komponentet default te ndermarjes qe nuk mund te fshihen       
        /// </summary>
        DefaultNdermarje = 1,
        /// <summary>
        /// komponente te krijuara nga perdoruesi qe mund te fshihen
        /// </summary>
        NgaPerdoruesi = 2,

    };
}
