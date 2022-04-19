using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DbCore.DbAsete
{
    /// <summary>
    /// MODULI ASETE:
    /// Mban nje list objektesh clsAseteLlojAmortizimi (objekte lloje te ndryshme amortizimi) ne modulin e Aseteve.
    /// Te dhenat merret nga tabela T_ASETE_LLOJAMORTIZIMI
    /// </summary>
    public class colAseteLlojAmortizimi : System.Collections.Generic.List<clsAseteLlojAmortizimi>
    {
        #region Konstruktoret

        /// <summary>
        /// MODULI ASETE:
        /// Krijon nje list objektesh bosh te klases clsAseteLlojAmortizimi per llojin e amortizimit.
        /// </summary>
        public colAseteLlojAmortizimi()
        {
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// MODULI ASETE:
        /// Merr te gjithe menyrat e amortizimit.
        /// </summary>
        /// <returns>Kthen True nese nuk ndodh asnje gabim gjate marrjes se llojit te amoritizimit ose False ne te kundert.</returns>
        public bool merrTeGjithaLlojAmortizimesh()
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            bool pergjigja = mbushLlojeAmortizimiList(moduliAsete.ktheLlojAmortizimeshTeGjitha());
            moduliAsete.Dispose();
            return pergjigja;
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// MODULI ASETE:
        /// Metode qe sherben per te marre te dhenat nga store procedura te lidhura me tabelen T_ASETE_LLOJAMORTIZIMI ne nje list objektesh clsAseteLlojAmortizimi.
        /// </summary>
        /// <param name="dt">(DataTable) Merr si parameter nje datatable qe kthehet nga store procedura per te mbushur nje list me objekte.</param>
        /// <returns>(bool) Kthen true nese nuk ndodh asnje gabim gjate leximit te te dhenave, ne te kundert false.</returns>
        private bool mbushLlojeAmortizimiList(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsAseteLlojAmortizimi llojeAmortizimi = new clsAseteLlojAmortizimi();
                    //llojeAmortizimi.mbushLlojAmortizimiObjekt(rreshti);
                    this.Add(new clsAseteLlojAmortizimi(rreshti));
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
