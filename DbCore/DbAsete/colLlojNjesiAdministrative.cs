using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DbCore.DbAsete
{
    /// <summary>
    /// MODULI ASETE:
    /// Mban nje list objektesh clsLlojNjesiAdministrative (objekte per te mbajtur llojet e ndryshme te njesive administrative ne raport me artikujt qe do te mbajne) ne modulin e Aseteve.
    /// Te dhenat merret nga tabela T_ASETE_LLOJ_NJESIADMINISTRATIVE.
    /// </summary>
    public class colLlojNjesiAdministrative: System.Collections.Generic.List<clsLlojNjesiAdministrative>
    {
        #region Konstruktore

        /// <summary>
        /// MODULI ASETE:
        /// Krijon nje list objektesh bosh te klases clsLlojNjesiAdministrative per llojet e njesive administrative.
        /// </summary>
        public colLlojNjesiAdministrative()
        {
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// MODULI ASETE:
        /// Merr te gjitha objektet e llojeve te njesive administrative dhe mbush nje koleksion me keto objekte.
        /// </summary>
        /// <returns>Kthen True nese nuk ndodh asnje gabim gjate marrjes se llojeve te njesive administrative ose False ne te kundert.</returns>
        public bool merrLlojNjesiAdministrative()
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            bool pergjigja = mbushLlojNjesiAdministrativeList(moduliAsete.ktheLlojNjesiAdministrative());
            moduliAsete.Dispose();
            return pergjigja;
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// MODULI ASETE:
        /// Metode qe sherben per te marre te dhenat nga store procedura te lidhura me tabelen T_ASETE_LLOJ_NJESIADMINISTRATIVE ne nje list objektesh clsLlojNjesiAdministrative.
        /// </summary>
        /// <param name="dt">(DataTable) Merr si parameter nje datatable qe kthehet nga store procedura per te mbushur nje list me objekte.</param>
        /// <returns>(bool) Kthen true nese nuk ndodh asnje gabim gjate leximit te te dhenave, ne te kundert false.</returns>
        private bool mbushLlojNjesiAdministrativeList(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsLlojNjesiAdministrative llojNjesiAdministrative = new clsLlojNjesiAdministrative();
                    //llojNjesiAdministrative.mbushLlojNjesiAdministrativeObjekt(rreshti);
                    this.Add(new clsLlojNjesiAdministrative(rreshti));
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
