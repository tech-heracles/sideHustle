using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DbCore.DbKontabiliteti
{

    /// <summary>
    /// porosi aparate porosi karta porosi loan dhurate
    /// </summary>
    public enum LlojPorosie
    {
        /// <summary>
        /// tip i papercaktuar 
        /// </summary>
        Undefined = 0,
        /// <summary>
        /// aparate
        /// </summary>
        Aparate = 1,
        /// <summary>
        /// karta    
        /// </summary>
        Karta = 2,
        /// <summary>
        /// loan
        /// </summary>
        Loan = 3,
        /// <summary>
        /// dhurate
        /// </summary>
        Dhurate = 4,
        /// <summary>
        /// te gjitha
        /// </summary>
        TeGjitha = 5,
        /// <summary>
        /// aparat ekspozitor
        /// </summary>
        Aparate_ekspozitore = 6
    };
}
