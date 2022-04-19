using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.DbAsete
{
    /// <summary>
    /// MODULI ASETE:
    /// Mban nje list objektesh clsSerialPerRivleresimKoka (objekte per ruajtjen e kokave te dokumentave te rivleresimit) ne modulin e Aseteve.
    /// Te dhenat merret nga tabela T_ASETE_SERIALxRIVLERESIM_KOKA.
    /// </summary>
    public class colSerialetPerRivlersimKoka : List<clsSerialPerRivleresimKoka>
    {
        #region Konstruktore

        /// <summary>
        /// MODULI ASETE:
        /// Krijon nje list objektesh bosh te klases clsSerialPerRivleresimKoka per kokat e dokumentave te rivleresimit.
        /// </summary>
        public colSerialetPerRivlersimKoka()
        {
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// MODULI ASETE:
        /// Metode qe sherben per te marre te dhenat nga store procedura te lidhura me tabelen T_ASETE_SERIALxRIVLERESIM_KOKA ne nje list objektesh clsSerialPerRivleresimKoka.
        /// </summary>
        /// <param name="dt">(DataTable) Merr si parameter nje datatable qe kthehet nga store procedura per te mbushur nje list me objekte.</param>
        /// <returns>(bool) Kthen true nese nuk ndodh asnje gabim gjate leximit te te dhenave, ne te kundert false.</returns>
        private bool mbushRivleresimiKokaList(DataTable dt)
        {
            foreach (DataRow rreshti in dt.Rows)
            {
                Add(new clsSerialPerRivleresimKoka(rreshti));
            }
            return true;
        }

        #endregion
    }
}
