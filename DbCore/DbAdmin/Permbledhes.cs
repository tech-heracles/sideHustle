using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbAdmin
{
    /// <summary>
    /// tregon eshte permbledhes rreshti apo jo. sherben vetem per vizualizimin ne peme te te drejtave.
    /// </summary>
    public enum Permbledhes : int
    {
        /// <summary>
        /// nuk eshte permbledhes
        /// </summary>
        Jo = 0,
        ///// <summary>
        ///// eshte permbledhes ambienti       
        ///// </summary>
        //Ambienti = 1,
        /// <summary>
        /// eshte permbledhes moduli
        /// </summary>
        Moduli = 1
    };
}
