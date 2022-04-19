using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbRegjistrim
{ 
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne  nje lloj dokumenti magazine
    ///  (Te dhenat  merren nga tabela : T_LLOJDOKUMENTIMAGAZINE)
    ///  <example> Hyrje, Dalje, Transferim, Hyrje nga Blerja, Dalje nga Shitja</example>
    /// </summary>
    /// <remarks> kjo eshte nje klase ndihmese per te shfaqur tipet e dokumentave per magazinen</remarks>
    public   class clsLlojDokumentiMagazine
    {
        #region Atributet

        private int idLlojDokumentiMagazine;
        private String pershkrimi;
        private DataRow rreshti;

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases me parametra
        /// </summary>
        /// <param name="idllojdokmag">id ritese e llojit te dokumentit te magazines</param>
        /// <param name="persh">pershkrimi</param>
        public clsLlojDokumentiMagazine(int idllojdokmag , string persh)
        {
            idLlojDokumentiMagazine = idllojdokmag;
            pershkrimi = persh;
        }

        /// <summary>
        /// mbush llojet e dokumetave te magazines sipas id-se
        /// </summary>
        /// <param name="id">id e llojit te dokumentit te magazines</param>
        public clsLlojDokumentiMagazine(int id)
        {
            clsDatabaseRegjistrim dbLlojDokumetashMagazine = new clsDatabaseRegjistrim();
            mbushLlojDokumentiMagazine(dbLlojDokumetashMagazine.ktheLlojDokumentashMagazineSipasId(id));
            dbLlojDokumetashMagazine.Dispose();
        }

        /// <summary>
        /// Konstruktori i klases pa parametra
        /// </summary>
        public clsLlojDokumentiMagazine()
        {
        }

        public clsLlojDokumentiMagazine(DataRow rreshti)
        {
            
            mbushLlojDokumentiMagazine(rreshti);
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdLlojDokumentiMagazine
        {
            get { return idLlojDokumentiMagazine; }
            set { idLlojDokumentiMagazine = value; }
        }

        /// <summary>
        /// Kthen/Vendos pershkrimi.
        /// </summary>
        public String Pershkrimi
        {
            get { return pershkrimi; }
            set { pershkrimi = value; }
        }

        /// <summary>
        /// Merr gjithe llojet e dokumentave te magazines nga tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.ktheGjitheLlojDokumentashMagazine"/> 
        /// </summary>
            /// <returns > nje objekt colLlojDokumentashMagazine qe permban nje koleksion me te gjithe llojet e dokumentave per magazinen</returns>
        public colLlojDokumentashMagazine merrTeGjitha()
        {
            colLlojDokumentashMagazine data = new colLlojDokumentashMagazine();
            data.mbushGjitheLlojDokumentashMagazine();
            return data;
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush llojet e dokumetit te magazines nga databaza
        /// </summary>
        /// <param name="dbDataRowLlojDokumenti">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushLlojDokumentiMagazine(DataRow dbDataRowLlojDokumenti)
        {
            if (dbDataRowLlojDokumenti != null)
            {
                try
                {
                    int.TryParse(dbDataRowLlojDokumenti["IDLLOJDOKUMENTIMAGAZINE"].ToString(), out idLlojDokumentiMagazine);
                    pershkrimi = dbDataRowLlojDokumenti["PERSHKRIMI"].ToString();
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se llojeve te dokumentave te magazines nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}
