using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbInventari
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne  klasat e artikujve
    ///  (Te dhenat  merren nga tabela : T_KLASAARTIKULLI)
    /// </summary>
    /// <remarks> eshte tabele ndihmese per artikujt</remarks>
    /// <example> inventar, i pastokueshem, i perbere, sherbim, prodhim</example>
    public class clsKlasaArtikulli
    {
        #region Atribute

        private int idKlasa;
        private string pershkrimKlasa;
        private DataRow rreshti;

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdKlasa
        {
            get { return idKlasa; }
            set { idKlasa = value; }
        }
        /// <summary>
        /// Kthen/Vendos pershkrimin e klases.
        /// </summary>
        public String PershkrimKlasa
        {
            get { return pershkrimKlasa; }
            set { pershkrimKlasa = value; }
        }

        #endregion

        #region Konstruktoret

        /// <summary>
        /// konstruktori me parametra
        /// </summary>
        /// <param name="idklasa"> id ritese e klases </param>
        /// <param name="pershkrimklasa"> pershkrimi i klases</param>
        public clsKlasaArtikulli(int idklasa,string pershkrimklasa)
        {
            idKlasa = idklasa;
            pershkrimKlasa = pershkrimklasa;
        }

        /// <summary>
        /// konstruktori me 1 parameter
        /// </summary>
        /// <param name="idKlasa">id e klases se artikullit</param>
        public clsKlasaArtikulli(int idKlasa)
        {
            clsDatabaseInventari dbKlasaArtikulli = new clsDatabaseInventari();
            mbushKlasaArtikull(dbKlasaArtikulli.ktheKlasaArtikulliSipasId(idKlasa));
            dbKlasaArtikulli.Dispose();
        }

        public clsKlasaArtikulli(string pershkrimi)
        {
            clsDatabaseInventari dbKlasaArtikulli = new clsDatabaseInventari();
            mbushKlasaArtikull(dbKlasaArtikulli.ktheKlasaArtikulliSipasPershkrimit(pershkrimi));
            dbKlasaArtikulli.Dispose();
        }
        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public clsKlasaArtikulli()
        {

        }

        public clsKlasaArtikulli(DataRow rreshti)
        {
            
            mbushKlasaArtikull(rreshti);
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush klasat e nje artikulli nga databaza
        /// </summary>
        /// <param name="dbDataRowKlasaArtikull">datarow qe duhet mbushur nga databaza</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushKlasaArtikull(DataRow dbDataRowKlasaArtikull)
        {
            if (dbDataRowKlasaArtikull != null)
            {

                try
                {
                    int.TryParse(dbDataRowKlasaArtikull["IDKLASAARTIKULLI"].ToString(), out idKlasa);
                    pershkrimKlasa = dbDataRowKlasaArtikull["PERSHKRIMIKLASAARTIKULLI"].ToString();

                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se klasave te artikullit nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion

    }
}
