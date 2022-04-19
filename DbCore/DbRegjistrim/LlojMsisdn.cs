using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DbCore.DbRegjistrim
{
    public enum LlojMsisdn
    {
        /// <summary>
        /// vlere e papercaktuar
        /// </summary>
        Undefined=0,

        /// <summary>
        /// Msisdn eshte per promocionin device with discount
        /// </summary>
        DeviceWithDiscount=1,
        /// <summary>
        /// Msisdn eshte per bazaar
        /// </summary>
        Bazaar=2


    }
}
