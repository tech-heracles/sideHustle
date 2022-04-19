using System;
using System.Collections.Generic;
using System.Linq;

namespace DbCore.DbProdhimi
{
    /// <summary>
    /// tip burimi  makineri/mjete/punonjes
    /// </summary>
    public enum TipBurimi : int
    {
        /// <summary>
        /// tip i papercaktuar 
        /// </summary>
        Undefined = 0,
        /// <summary>
        /// makineri
        /// </summary>
        Makineri = 1,
        /// <summary>
        /// mjete       
        /// </summary>
        Mjet = 2,
        /// <summary>
        /// Punonjes
        /// </summary>
        Punonjes = 3,

    };
}
