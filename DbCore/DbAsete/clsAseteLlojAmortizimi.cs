using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DbCore.DbAsete
{
    /// <summary>
    /// MODULI ASETE:
    /// Klase e ndertuar per te mbajtur objektin e llojit te amortizimit.
    /// Te dhenat merret nga tabela T_ASETE_LLOJAMORTIZIMI
    /// </summary>
    public class clsAseteLlojAmortizimi
    {
        #region Atribute

        private int idLlojAmortizimi;
        private string llojAmortizimi;
        private string pershkrimi;

        #endregion

        #region Properties

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id automatike llojit te amortizimit.
        /// </summary>
        public int IdLlojAmortizimi
        {
            get { return idLlojAmortizimi; }
            set { idLlojAmortizimi = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (string) Merr ose jep vlere emrit te llojit te amortizimit.
        /// </summary>
        public string LlojAmortizimi
        {
            get { return llojAmortizimi; }
            set { llojAmortizimi = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (string) Merr ose jep vlere pershkrimit te llojit te amortizimit.
        /// </summary>
        public string Pershkrimi
        {
            get { return pershkrimi; }
            set { pershkrimi = value; }
        }

        /// <summary>
        /// String default per llojin e amortizimit pa amortizim
        /// </summary>
        public static readonly string PA_AMORTIZIM = "Pa amortizim";

        /// <summary>
        /// String default per llojin e amortizimit me amortizim linear
        /// </summary>
        public static readonly string AMORTIZIM_LINEAR = "Amortizim linear";

        /// <summary>
        /// String default per llojin e amortizimit me amortizim mbi vleren e shtuar
        /// </summary>
        public static readonly string AMORTIZIM_MBI_VLERE_TE_SHTUAR = "Amortizim mbi vleren e mbetur";
        private DataRow rreshti;

        #endregion

        #region Konstruktori

        /// <summary>
        /// MODULI ASETE:
        /// Krijon objektin bosh te klases clsAseteLlojAmortizimi per llojin e amortizimit.
        /// </summary>
        public clsAseteLlojAmortizimi()
        {
        }

        public clsAseteLlojAmortizimi(DataRow rreshti)
        {
            
            mbushLlojAmortizimiObjekt(rreshti);
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// MODULI ASETE:
        /// Metoda perdoret per te mbushur te gjitha atributet e objektit clsAseteLlojAmortizimi sipas te dhenave qe vijne nga cdo rresht i databazes nga tabela T_ASETE_LLOJAMORTIZIMI.
        /// </summary>
        /// <param name="dbDataRowAmortizimi">(DataRow) Merr si parameter vetem nje rresht te kthyer nga tabelat ne databaze per te mbushur nje objekt.</param>
        /// <returns>(bool) Kthen true nese nuk ndodh asnje gabim gjate leximit te te dhenave, ne te kundert false.</returns>
        internal bool mbushLlojAmortizimiObjekt(DataRow dbDataRowAmortizimi)
        {
            if (dbDataRowAmortizimi == null)
                return false;
            try
            {
                int.TryParse(dbDataRowAmortizimi["ID_LLOJ_AMORTIZIMI"].ToString(), out idLlojAmortizimi);
                llojAmortizimi = dbDataRowAmortizimi["LLOJ_AMORTIZIMI"].ToString();
                pershkrimi = dbDataRowAmortizimi["PERSHKRIMI"].ToString();
                return true;
            }
            catch (InvalidCastException)
            {
                throw new Exception("ERROR: Gabim gjate marrjes se llojit te amortizimit nga db-ja");
            }
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// MODULI ASETE:
        /// Merr emeritizmin e llojit te amortizimit sipas ID ne transaksion.
        /// </summary>
        /// <param name="idLlojAmortizimi">(int) Id automatike e llojit te amortizimit.</param>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen emertimin e llojit te amortizimit sipas id nese gjendet, ne te kundert kthen "".</returns>
        public static string merrEmeritimiLlojAmortizimiSipasID(int idLlojAmortizimi)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            string pergjigje = moduliAsete.ktheEmeritimiLlojAmortizimiSipasID(idLlojAmortizimi);
            return pergjigje;
        }

        public static int ktheIdLlojAmortizimiSipasEmertimit(string LlojAmortizimi)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            int pergjigje = moduliAsete.ktheIdLlojAmortizimiSipasEmertimit(LlojAmortizimi);
            moduliAsete.Dispose();
            return pergjigje;
        }

        #endregion
    }
}
