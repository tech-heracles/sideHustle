using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    /// konfigurimin e tvsh gjate regjistrimit , tvsh ndermarje, artikulli apo pa tvsh
    /// </summary>
    public enum KonfigurimTVSHGjateRregj
    {
        /// <summary>
        /// tip i papercaktuar 
        /// </summary>
        Undefined = 0,
        /// <summary>
        /// per aprovim
        /// </summary>
        Ndermarrje = 1,
        /// <summary>
        /// aprovuar    
        /// </summary>
        Sipas_Artikullit = 2,
        /// <summary>
        /// refuzuar
        /// </summary>
        Pa_TVSH = 3,
        ///<summary>
        ///sipas klientit
        /// </summary>
        Sipas_Klientit=4
    };
}
