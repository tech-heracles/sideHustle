using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DbCore.DbOTC
{
    public enum StatusOTC
    {
        Undefined = -1,
        /// <summary>
        /// Pergjigja eshte kthyer me sukses por pritet qe mpesa te thirr ws ne otc per te kthyer rezultatin
        /// </summary>
        Pending = 0,
        /// <summary>
        /// rezultati eshte kthyer me sukses
        /// </summary>
        Completed = 1,
        /// <summary>
        /// kthimi i pergjigjes ka deshtuar
        /// </summary>
        Fail = 2


    }
}
