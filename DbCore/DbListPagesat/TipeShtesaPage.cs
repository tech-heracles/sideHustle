using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DbCore.DbListPagesat
{
    /// <summary>
    /// tipe shtesa page funksion perqindje/funksion vlere/ pozicioni/ kualifikimi/ veshtiresia/ vjetersia
    /// </summary>
    public enum TipeShtesaPage : int
    {
        /// <summary>
        /// tip i papercaktuar 
        /// </summary>
        Undefined = 0,
        /// <summary>
        /// funksioniperqindje
        /// </summary>
        FunksioniPerqindje = 1,
        /// <summary>
        /// funksionivlere      
        /// </summary>
        FunksioniVlere = 2,
        /// <summary>
        /// pozicioni
        /// </summary>
        Pozicioni = 3,
        /// <summary>
        /// kualifikimi
        /// </summary>
        Kualifikimi = 4,
        /// <summary>
        /// veshtiresia
        /// </summary>
        Veshtiresia = 5,
        /// <summary>
        /// vjetersia
        /// </summary>
        Vjetersia = 6,
       


    };
}
