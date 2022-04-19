using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DbCore.DbAsete
{
    /// <summary>
    /// MODULI ASETE:
    /// Mban nje list objektesh clsStandarteAmortizimi (objekte standartesh te ndryshme amortizimi) ne modulin e Aseteve.
    /// Te dhenat merret nga tabela T_ASETE_STANDART_AMORT
    /// </summary>
    public class colStandarteAmortizimi : List<clsStandarteAmortizim>
    {
        #region Konstruktore

        /// <summary>
        /// MODULI ASETE:
        /// Krijon nje list objektesh bosh te klases clsStandarteAmortizim per standartet e amortizimit.
        /// </summary>
        public colStandarteAmortizimi()
        {
        }

        /// <summary>
        /// MODULI ASETE:
        /// Konstruktori me nje parameter.
        /// </summary>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes ne perdorim per te cilen do te kthehen standartet e amortizimit.</param>
        public colStandarteAmortizimi(int idNdermarrje)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            mbushStandarteAmortizimiList(moduliAsete.ktheStandarteAmortizimiTeNdermarrjes(idNdermarrje));
            moduliAsete.Dispose();
        } 

        #endregion

        #region Metoda Publike

        /// <summary>
        /// MODULI ASETE:
        /// Merr te gjitha objektet e standarteve te amortizimit per ndermarrjen ne perdorim dhe mbush nje koleksion me keto objekte.
        /// </summary>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes ne perdorim per te cilen do te kthehen standartet e amortizimit.</param>
        /// <returns>Kthen True nese nuk ndodh asnje gabim gjate marrjes se standarteve te amortizimit ose False ne te kundert.</returns>
        public bool merrStandarteAmortizimiTeNdermarrjes(int idNdermarrje)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            bool pergjigja = mbushStandarteAmortizimiList(moduliAsete.ktheStandarteAmortizimiTeNdermarrjes(idNdermarrje));
            moduliAsete.Dispose();
            return pergjigja;
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// MODULI ASETE:
        /// Metode qe sherben per te marre te dhenat nga store procedura te lidhura me tabelen T_ASETE_STANDART_AMORT ne nje list objektesh clsStandarteAmortizim.
        /// </summary>
        /// <param name="dt">(DataTable) Merr si parameter nje datatable qe kthehet nga store procedura per te mbushur nje list me objekte.</param>
        /// <returns>(bool) Kthen true nese nuk ndodh asnje gabim gjate leximit te te dhenave, ne te kundert false.</returns>
        private bool mbushStandarteAmortizimiList(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsStandarteAmortizim standarte = new clsStandarteAmortizim();
                    //standarte.mbushStandarteAmortizimiObjekt(rreshti);
                    Add(new clsStandarteAmortizim(rreshti));
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
