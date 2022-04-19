using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DbCore.DbAsete
{
    /// <summary>
    /// MODULI ASETE:
    /// Klase e ndertuar per te mbajtur objektin e llojit te njesise administrative, pra nese njesi administrative do te perdoret vetem per artikujt afatgjate, apo vetem per artikujt normale, apo per te dy artikujt.
    /// Te dhenat merret nga tabela T_ASETE_LLOJ_NJESIADMINISTRATIVE
    /// </summary>
    public class clsLlojNjesiAdministrative
    {
        #region Atribute

        private int idLlojNjesiAdministrative;
        private string emertimi;
        private DataRow rreshti;

        #endregion

        #region Properties

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id automatike te llojit te njesise administrative.
        /// </summary>
        public int IdLlojNjesiAdministrative
        {
            get { return idLlojNjesiAdministrative; }
            set { idLlojNjesiAdministrative = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (string) Merr ose jep vlere emrit te llojit te njesise administrative.
        /// </summary>
        public string Emertimi
        {
            get { return emertimi; }
            set { emertimi = value; }
        }

        #endregion

        #region Konstruktori
        
        /// <summary>
        /// MODULI ASETE:
        /// Krijon objektin bosh te klases clsLlojNjesiAdministrative per llojet e magazines, ne lidhje me artikujt qe do te mbaje.
        /// </summary>
        public clsLlojNjesiAdministrative()
        {
        }

        public clsLlojNjesiAdministrative(DataRow rreshti)
        {
            
            mbushLlojNjesiAdministrativeObjekt(rreshti);
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// MODULI ASETE:
        /// Metoda perdoret per te mbushur te gjitha atributet e objektit clsLlojNjesiAdministrative sipas te dhenave qe vijne nga cdo rresht i databazes nga tabela T_ASETE_LLOJ_NJESIADMINISTRATIVE.
        /// </summary>
        /// <param name="dbDataRowLlojNjesiAdministrative">(DataRow) Merr si parameter vetem nje rresht te kthyer nga tabelat ne databaze per te mbushur nje objekt.</param>
        /// <returns>(bool) Kthen true nese nuk ndodh asnje gabim gjate leximit te te dhenave, ne te kundert false.</returns>
        internal bool mbushLlojNjesiAdministrativeObjekt(DataRow dbDataRowLlojNjesiAdministrative)
        {
            if (dbDataRowLlojNjesiAdministrative == null)
                return false;
            try
            {
                int.TryParse(dbDataRowLlojNjesiAdministrative["ID_LLOJ_NJESIADMINISTRATIVE"].ToString(), out idLlojNjesiAdministrative);
                emertimi = dbDataRowLlojNjesiAdministrative["EMERTIMI"].ToString();
                return true;
            }
            catch (InvalidCastException)
            {
                throw new Exception("ERROR: Gabim gjate marrjes se llojit te njesise administrative nga db-ja");
            }
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// MODULI ASETE:
        /// Merr id e llojit te njesise administrative sipas emertimit qe kerkojme.
        /// </summary>
        /// <param name="emertimi">(string) Emertimi i llojit te njesise administrative qe i kerkojme ID-ne.</param>
        /// <returns>Kthen id e llojit te njesise administrative nese gjendet, ne te kundert kthen -1.</returns>
        public static int merrIDLlojNjesiAdministrativeSipasEmertimi(string emertimi)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            int pergjigje = moduliAsete.ktheIDLlojNjesiAdministrativeSipasEmertimi(emertimi);
            moduliAsete.Dispose();
            return pergjigje;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr emertimin e llojit te njesise administrative sipas id qe kerkojme.
        /// </summary>
        /// <param name="idLlojNjesiAdministrative">(int) Id e llojit te njesise administrative qe kerkojme.</param>
        /// <returns>Kthen emertimin e llojit te njesise administrative nese gjendet, ne te kundert kthen -1.</returns>
        public static string merrEmertimiLlojNjesiAdministrativeSipasID(int idLlojNjesiAdministrative)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            string pergjigje = moduliAsete.ktheEmertimiLlojNjesiAdministrativeSipasID(idLlojNjesiAdministrative);
            moduliAsete.Dispose();
            return pergjigje;
        }

        #endregion
    }
}
