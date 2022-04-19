using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    /// llojet e taksave nivel tvsh, tvsh, takse doganore
    /// </summary>
    public enum LlojTakse
    {
        /// <summary>
        /// tip i papercaktuar 
        /// </summary>
        Undefined = 0,
        /// <summary>
        /// per aprovim
        /// </summary>
        Nivel_Tvsh = 1,
        /// <summary>
        /// aprovuar    
        /// </summary>
        TVSH = 2,
        /// <summary>
        /// refuzuar
        /// </summary>
        Takse_Doganore = 3
    };
}
