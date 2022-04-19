using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DbCore.DbAsete
{
    /// <summary>
    /// MODULI ASETE:
    /// Mban nje list objektesh clsStatusMagazine_Asete (objekte per te mbajtur statuset e ndryshme qe mund te kete nje magazine ne varesi te ndermarrjeve) ne modulin e Aseteve.
    /// Te dhenat merret nga tabela T_ASETE_STATUS_MAGAZINE.
    /// </summary>
    public class colStatusMagazine_Asete : List<clsStatusMagazine_Asete>
    {
        #region Konstruktor

        /// <summary>
        /// MODULI ASETE:
        /// Krijon nje list objektesh bosh te klases clsStatusMagazine_Asete per statuset e magazines.
        /// </summary>
        public colStatusMagazine_Asete()
        {
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// MODULI ASETE:
        /// Merr te gjitha statuset e perdorshem te magazinave per ndermarrjen ne perdorim dhe mbush nje koleksion me keto objekte.
        /// </summary>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes ne perdorim per te cilen do te kthehen statuset e perdorshem te magazinave.</param>
        /// <returns>Kthen True nese nuk ndodh asnje gabim gjate marrjes se statuseve te perdorshem te magazinave ose False ne te kundert.</returns>
        public bool merrStatusMagazineTePerdorshmeTeNdermarrjes(int idNdermarrje)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            bool pergjigja = mbushStatusMagazineList(moduliAsete.ktheStatusMagazineTePerdorshmeTeNdermarrjes(idNdermarrje));
            moduliAsete.Dispose();
            return pergjigja;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr te gjitha objektet e statuseve te magazinave per ndermarrjen ne perdorim dhe mbush nje koleksion me keto objekte.
        /// </summary>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes ne perdorim per te cilen do te kthehen statuset e magazinave.</param>
        /// <returns>Kthen True nese nuk ndodh asnje gabim gjate marrjes se statuseve te magazinave ose False ne te kundert.</returns>
        public bool merrStatusMagazineTeNdermarrjes(int idNdermarrje)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            bool pergjigja = mbushStatusMagazineList(moduliAsete.ktheStatusMagazineTeNdermarrjes(idNdermarrje));
            moduliAsete.Dispose();
            return pergjigja;
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// MODULI ASETE:
        /// Metode qe sherben per te marre te dhenat nga store procedura te lidhura me tabelen T_ASETE_STATUS_MAGAZINE ne nje list objektesh clsStatusMagazine_Asete.
        /// </summary>
        /// <param name="dt">(DataTable) Merr si parameter nje datatable qe kthehet nga store procedura per te mbushur nje list me objekte.</param>
        /// <returns>(bool) Kthen true nese nuk ndodh asnje gabim gjate leximit te te dhenave, ne te kundert false.</returns>
        private bool mbushStatusMagazineList(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsStatusMagazine_Asete statusMagazine = new clsStatusMagazine_Asete();
                    //statusMagazine.mbushStatusMagazineObjekt(rreshti);
                    Add(new clsStatusMagazine_Asete(rreshti));
                }
            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }

        #endregion
    }
}
