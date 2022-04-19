using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DbCore.DbAsete
{
    /// <summary>
    /// MODULI ASETE:
    /// Mban nje list objektesh clsHistorikStatusMagazine (objekte per historikun e ndryshimit te statuseve te magazinave) ne modulin e Aseteve.
    /// Te dhenat merret nga tabela T_ASETE_NJESIADMINISTRATIVE_HISTORIKU
    /// </summary>
    public class colHistorikStatusMagazine : List<clsHistorikStatusMagazine>
    {
        #region Konstruktore

        /// <summary>
        /// MODULI ASETE:
        /// Krijon nje list objektesh bosh te klases clsHistorikStatusMagazine per historikun e njesive administrative.
        /// </summary>
        public colHistorikStatusMagazine()
        {
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// MODULI ASETE:
        /// Merr te gjithe historikun e magazines qe kerkojme dhe mbush nje koleksion me keto objekte.
        /// </summary>
        /// <param name="idNjesiAdministrative">(int) Id e njesise administrative qe duhet ti marrim historikun.</param>
        /// <returns>Kthen True nese nuk ndodh asnje gabim gjate marrjes se historikut te magazines ose False ne te kundert.</returns>
        public bool merrHistorikMagazinaSipasIdNjesiAdministrative(int idNjesiAdministrative)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            bool pergjigja = mbushHistorikNjesiAdministrativeList(moduliAsete.ktheHistorikMagazinaSipasIdNjesiAdministrative(idNjesiAdministrative));
            moduliAsete.Dispose();
            return pergjigja;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr te gjithe historikun e magazines qe kerkojme dhe mbush nje koleksion me keto objekte ne transaksion.
        /// </summary>
        /// <param name="idNjesiAdministrative">(int) Id e njesise administrative qe duhet ti marrim historikun.</param>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen True nese nuk ndodh asnje gabim gjate marrjes se historikut te magazines ose False ne te kundert.</returns>
        public bool merrHistorikMagazinaSipasIdNjesiAdministrative(int idNjesiAdministrative, clsDatabazeAsete moduliAsete)
        {

            bool pergjigja = mbushHistorikNjesiAdministrativeList(moduliAsete.ktheHistorikMagazinaSipasIdNjesiAdministrative(idNjesiAdministrative));

            return pergjigja;
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// MODULI ASETE:
        /// Metode qe sherben per te marre te dhenat nga store procedura te lidhura me tabelen T_ASETE_NJESIADMINISTRATIVE_HISTORIKU ne nje list objektesh clsHistorikStatusMagazine.
        /// </summary>
        /// <param name="dt">(DataTable) Merr si parameter nje datatable qe kthehet nga store procedura per te mbushur nje list me objekte.</param>
        /// <returns>(bool) Kthen true nese nuk ndodh asnje gabim gjate leximit te te dhenave, ne te kundert false.</returns>
        private bool mbushHistorikNjesiAdministrativeList(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsHistorikStatusMagazine historikNjesiAdministrative = new clsHistorikStatusMagazine();
                    //historikNjesiAdministrative.mbushHistorikNjesiAdministrativeObjekt(rreshti);
                    Add(new clsHistorikStatusMagazine(rreshti));
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
