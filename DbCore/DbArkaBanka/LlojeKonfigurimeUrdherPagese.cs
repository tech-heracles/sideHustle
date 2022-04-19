using System;
using System.Collections.Generic;
using System.Linq;


namespace DbCore.DbArkaBanka
{
    /// <summary>
    /// lloje konfigurime urdher pagese
    /// </summary>
    public enum LlojeKonfigurimeUrdherPagese : int
    {
        /// <summary>
        /// tip i papercaktuar 
        /// </summary>
        Undefined = 0,
        /// <summary>
        /// grup
        /// </summary>
        Grup = 1,
        /// <summary>
        /// titull       
        /// </summary>
        Titull = 2,
        /// <summary>
        /// kapitull
        /// </summary>
        Kapitull = 3,

    };
}
