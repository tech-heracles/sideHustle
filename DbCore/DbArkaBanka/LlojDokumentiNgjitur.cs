using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DbCore.DbArkaBanka
{ /// <summary>
    /// lloje dokumenti ngjitur
    /// </summary>
  public enum    LlojDokumentiNgjitur: int
    
  {
        /// <summary>
        /// tip i papercaktuar 
        /// </summary>
        Undefined = 0,
        /// <summary>
        /// cek
        /// </summary>
        Cek = 1,
        /// <summary>
        /// xhirim       
        /// </summary>
        Xhirim = 2,
        /// <summary>
        /// te tjera
        /// </summary>
        TeTjera = 3,

    };
}
