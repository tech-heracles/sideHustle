using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DbCore.DbAsete
{
    /// <summary>
    /// MODULI ASETE:
    /// Mban nje list objektesh colAmortizimiFillestar (objekte per ruajtjen e serialeve te aqt-ve) ne modulin e Aseteve.
    /// </summary>
    public class colAmortizimiFillestar : List<clsAmortizimiFillestar>
    {
        #region Konstruktore

        /// <summary>
        /// MODULI ASETE:
        /// Krijon nje list objektesh bosh te klases clsAmortizimiFillestar per serialet e aqt-ve.
        /// </summary>
        public colAmortizimiFillestar()
        {
        }

        #endregion

        #region Metoda Publike


        public bool ktheAmortizimFillestarSipasStandartit(int idStandart)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            bool pergjigja = mbushAmortizimFillestar(moduliAsete.ktheAmortizimFillestarSipasIdStandarti(idStandart));
            moduliAsete.Dispose();
            return pergjigja;
        }


        #endregion

        #region Metoda Private

        /// <summary>
        /// MODULI ASETE:
        /// Metode qe sherben per te marre te dhenat nga store procedura te lidhura me tabelen T_ASETE_AQTSERIALE ne nje list objektesh clsAmortizimiFillestar.
        /// </summary>
        /// <param name="dt">(DataTable) Merr si parameter nje datatable qe kthehet nga store procedura per te mbushur nje list me objekte.</param>
        /// <returns>(bool) Kthen true nese nuk ndodh asnje gabim gjate leximit te te dhenave, ne te kundert false.</returns>
        private bool mbushAmortizimFillestar(DataTable dt)
        {
            //try
                foreach (DataRow rreshti in dt.Rows)
                {
                    Add(new clsAmortizimiFillestar(rreshti));
                }
            return true;
        }

        #endregion
    }
}
