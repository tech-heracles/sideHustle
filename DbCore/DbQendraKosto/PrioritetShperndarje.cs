using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DbCore.DbQendraKosto
{
    /// <summary>
    /// prioriteti i shperndarjes
    /// </summary>
    public enum PrioritetShperndarje : int
    {
        /// <summary>
        /// tip i papercaktuar 
        /// </summary>
        Undefined = 0,
        /// <summary>
        /// dege administrative
        /// </summary>
        Dege_Administrative = 1,
        /// <summary>
        /// nendepartameti       
        /// </summary>
        Nendepartamenti = 2,
        /// <summary>
        /// Departameti
        /// </summary>
        Departamenti = 3,
        /// <summary>
        /// Llogari kontabel
        /// </summary>
        Llogari_Kontabel = 4,
        /// <summary>
        /// magazina
        /// </summary>
        Magazine=5,
    };
}
