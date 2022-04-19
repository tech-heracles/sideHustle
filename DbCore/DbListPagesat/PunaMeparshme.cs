using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DbCore.DbListPagesat
{
    /// <summary>
    /// puna e meparshme publik/privat/eksperienca e pare
    /// </summary>
    public enum PunaMeparshme : int
    {
        /// <summary>
        /// tip i papercaktuar 
        /// </summary>
        Undefined = -1,
        /// <summary>
        ///  publik
        /// </summary>
        Publik = 1,
        /// <summary>
        /// privat     
        /// </summary>
        Privat = 2,
        /// <summary>
        /// eksperienca e pare
        /// </summary>
        Eksperienca_Pare = 3,
        /// <summary>
        /// Papunesia
        /// </summary>
        Papunesia = 4,
        /// <summary>
        /// Te Tjera
        /// </summary>
        Te_tjera = 5,
        /// <summary>
        /// Pagese Papunesie
        /// </summary>
        Page_Papunesie = 6,

    };
}
