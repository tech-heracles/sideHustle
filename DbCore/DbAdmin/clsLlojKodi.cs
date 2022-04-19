using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbAdmin
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne
    ///  llojet e kodeve : Nr Dokumenti, KodSerial etj... (Te dhenat  merren nga tabela : T_LLOJKODI)
    /// </summary>
    //kjo klase perdoret per llojet e kodeve : Nr Dokumenti, KodSerial ...etj
    public class clsLlojKodi
    {
        #region Atribute

        private int idLlojKodi;
        private String llojKodiPershkrimi;
        private DataRow rreshti;

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases.
        /// </summary>
        public clsLlojKodi(int idllojodi,  String llojkodipershkrimi)
        {
            idLlojKodi = idllojodi;
            llojKodiPershkrimi = llojkodipershkrimi;
        }

        /// <summary>
        /// Konstruktori default i klases.
        /// </summary>
        public clsLlojKodi()
        { 
        }

        public clsLlojKodi(DataRow rreshti)
        {
            
            mbushLlojKodi(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdLlojKodi
        {
            get { return idLlojKodi; }
            set { idLlojKodi = value; }
        }

        /// <summary>
        /// Kthen/Vendos pershkrimin e ketij lloji te kodit.
        /// </summary>
        public string LlojKodiPershkrimi
        {
            get { return llojKodiPershkrimi; }
            set { llojKodiPershkrimi = value; }
        }

        #endregion

        #region Metoda Publike

        public static int ktheIDLlojKodi(string kod,clsDatabaseAdmin data) //tocache
        {
           
            int idLlojKodi = (data.merrIDLlojKodi(kod));
          
            return idLlojKodi;
        }
        public static int ktheIDLlojKodi(string kod)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                int idLlojKodi = (data.merrIDLlojKodi(kod));

                return idLlojKodi;
            }
        }
        #endregion

        #region Metoda Internal

        internal bool mbushLlojKodi(DataRow dbDataRowLlojKodi)
        {
            if (dbDataRowLlojKodi != null)
            {
                try
                {
                    int.TryParse(dbDataRowLlojKodi["IDLLOJKODI"].ToString(), out idLlojKodi);
                    llojKodiPershkrimi = dbDataRowLlojKodi["LLOJKODIPERSHKRIMI"].ToString();
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se llojit te kodit nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion

    }
}
