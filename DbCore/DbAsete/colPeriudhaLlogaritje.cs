using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DbCore.DbAsete
{
    /// <summary>
    /// MODULI ASETE:
    /// Mban nje list objektesh clsPeriudhaLlogaritje (objekte per fillimin e dhe mbarimin e ndryshem ne standarte te ndryshme) ne modulin e Aseteve.
    /// Te dhenat merret nga tabela T_ASETE_PERIUDHA_LLOGARITJE
    /// </summary>
    public class colPeriudhaLlogaritje : List<clsPeriudhaLlogaritje>
    {
        #region Konstruktore

        /// <summary>
        /// MODULI ASETE:
        /// Krijon nje list objektesh bosh te klases clsPeriudhaLlogaritje per periudhat e llogaritjeve.
        /// </summary>
        public colPeriudhaLlogaritje()
        {
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// MODULI ASETE:
        /// Merr te gjitha objektet e periudhave te llogaritjes per llojin e fillimit te periudhes dhe mbush nje koleksion me keto objekte.
        /// </summary>
        /// <returns>Kthen True nese nuk ndodh asnje gabim gjate marrjes se periudhave te llogaritjes ose False ne te kundert.</returns>
        public bool merrPeriudhaLlogaritjeFillim(int idGjuha)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            bool pergjigja = mbushPeriudhaLlogaritjeList(moduliAsete.kthePeriudhaLlogaritjeSipasPeriudhes(false,idGjuha));
            moduliAsete.Dispose();
            return pergjigja;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr te gjitha objektet e periudhave te llogaritjes per llojin e fundit te periudhes dhe mbush nje koleksion me keto objekte.
        /// </summary>
        /// <returns>Kthen True nese nuk ndodh asnje gabim gjate marrjes se periudhave te llogaritjes ose False ne te kundert.</returns>
        public bool merrPeriudhaLlogaritjeFund(int idGjuha)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            bool pergjigja = mbushPeriudhaLlogaritjeList(moduliAsete.kthePeriudhaLlogaritjeSipasPeriudhes(true,idGjuha));
            moduliAsete.Dispose();
            return pergjigja;
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// MODULI ASETE:
        /// Metode qe sherben per te marre te dhenat nga store procedura te lidhura me tabelen T_ASETE_PERIUDHA_LLOGARITJE ne nje list objektesh clsPeriudhaLlogaritje.
        /// </summary>
        /// <param name="dt">(DataTable) Merr si parameter nje datatable qe kthehet nga store procedura per te mbushur nje list me objekte.</param>
        /// <returns>(bool) Kthen true nese nuk ndodh asnje gabim gjate leximit te te dhenave, ne te kundert false.</returns>
        private bool mbushPeriudhaLlogaritjeList(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsPeriudhaLlogaritje periudha = new clsPeriudhaLlogaritje();
                    //periudha.mbushPeriudhaLlogaritjeObjekt(rreshti);
                    this.Add(new clsPeriudhaLlogaritje(rreshti));
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
