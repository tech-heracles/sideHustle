using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DbCore.DbAsete
{
    /// <summary>
    /// MODULI ASETE:
    /// Mban nje list objektesh clsGrupNormaAmortizimi (objekte per normat e amortizimit sipas grupeve dhe llojeve te amortizimit) ne modulin e Aseteve.
    /// Te dhenat merret nga tabela T_ASETE_LIDHJE_GRUP_LLOJAMORT
    /// </summary>
    public class colGrupNormaAmortizimi : System.Collections.Generic.List<clsGrupNormaAmortizimi>
    {
        #region Konstruktore

        /// <summary>
        /// MODULI ASETE:
        /// Krijon nje list objektesh bosh te klases clsGrupNormaAmortizimi per normat e amortizimit sipas grupeve dhe llojeve te amortizimit.
        /// </summary>
        public colGrupNormaAmortizimi()
        {
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// MODULI ASETE:
        /// Merr te gjithe normat e grupit qe kerkojme pavaresisht nga standarti.
        /// </summary>
        /// <param name="idKonfigArtikulli">(int) Id e grupit qe ka amortizimin.</param>
        /// <param name="idndermarje">(int) Id e ndermarrjes.</param>
        /// <returns>Kthen True nese nuk ndodh asnje gabim gjate marrjes se normave te grupit ose False ne te kundert.</returns>
        public bool merrGrupNormaAmortizimiTeGjitha(int idKonfigArtikulli, int idndermarje)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            bool pergjigja = mbushNormaAmortizimiGrupList(moduliAsete.ktheGrupNormaAmortizimiTeGjitha(idKonfigArtikulli, idndermarje));
            moduliAsete.Dispose();
            return pergjigja;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr normat e grupit per amortizimin fillestar.
        /// </summary>
        /// <param name="idndermarje">(int) Id e ndermarrjes.</param>
        /// <returns>Kthen True nese nuk ndodh asnje gabim gjate marrjes se normave te grupit ose False ne te kundert.</returns>
        public bool merrGrupNormaAmortizimiFillestare(int idndermarje)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            bool pergjigja = mbushNormaAmortizimiGrupList(moduliAsete.ktheGrupNormaAmortizimiFillestare(idndermarje));
            moduliAsete.Dispose();
            return pergjigja;
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// MODULI ASETE:
        /// Metode qe sherben per te marre te dhenat nga store procedura te lidhura me tabelen T_ASETE_LIDHJE_GRUP_LLOJAMORT ne nje list objektesh clsGrupNormaAmortizimi.
        /// </summary>
        /// <param name="dt">(DataTable) Merr si parameter nje datatable qe kthehet nga store procedura per te mbushur nje list me objekte.</param>
        /// <returns>(bool) Kthen true nese nuk ndodh asnje gabim gjate leximit te te dhenave, ne te kundert false.</returns>
        private bool mbushNormaAmortizimiGrupList(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsGrupNormaAmortizimi normaAmortizimiGrup = new clsGrupNormaAmortizimi();
                    //normaAmortizimiGrup.mbushNormaAmortizimiGrupObjekt(rreshti);
                    this.Add(new clsGrupNormaAmortizimi(rreshti));
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