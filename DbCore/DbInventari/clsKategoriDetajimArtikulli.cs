using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbInventari
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne  kategorite e detajimeve
    ///  (Te dhenat  merren nga tabela : T_KATEGORIDETAJIMARTIKULLI)
    /// </summary>
    /// <remarks> eshte tabele ndihmese per detajimet e artikujve</remarks>
    /// <example> detajim, serial, date skadence</example>
    public class clsKategoriDetajimArtikulli
    {
        #region Atribute

        private int idKategoriDetajimi;
        private String kodiKategoriDetajimi;
        private String pershkrimiKategoriDetajimi;
        private DataRow rreshti;

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdKategoriDetajimi
        {
            get{ return idKategoriDetajimi; }
            set { idKategoriDetajimi = value; }
        }
        /// <summary>
        /// Kthen/Vendos kodi i kategorise se detajimit.
        /// </summary>
        public String KodiKategoriDetajimi
        {
            get {return kodiKategoriDetajimi;}
            set { kodiKategoriDetajimi = value; }
        }
        /// <summary>
        /// Kthen/Vendos pershkrimin e kategorise se detajimit.
        /// </summary>
        public String PershkrimiKategoriDetajimi
        {
            get { return pershkrimiKategoriDetajimi; }
            set { pershkrimiKategoriDetajimi = value; }
        }

        #endregion

        #region Konstruktore

        /// <summary>
        /// konstruktori me parametra
        /// </summary>
        /// <param name="id"> id ritese e kategorise se detajimit</param>
        /// <param name="kodi">kodi i kategorise se detajimeve</param>
        /// <param name="pershkrimi"> pershkrimi i kategorise se detajimeve</param>
        public clsKategoriDetajimArtikulli(int id, String kodi, String pershkrimi)
        {
            idKategoriDetajimi = id;
            kodiKategoriDetajimi = kodi;
            pershkrimiKategoriDetajimi = pershkrimi;
        }

        /// <summary>
        /// Konstruktor me 1 parameter
        /// </summary>
        /// <param name="id">id e kategorise</param>
        public clsKategoriDetajimArtikulli(int id)
        {
            clsDatabaseInventari dbKategoriDetajimArtikulli = new clsDatabaseInventari();
            mbushKategoriDetajimArtikulli(dbKategoriDetajimArtikulli.merrKategoriDetajimiSipasID(id));
            dbKategoriDetajimArtikulli.Dispose();
        }
        public clsKategoriDetajimArtikulli(string kodi)
        {
            clsDatabaseInventari dbKategoriDetajimArtikulli = new clsDatabaseInventari();
            mbushKategoriDetajimArtikulli(dbKategoriDetajimArtikulli.merrKategoriDetajimiSipasKodit(kodi));
            dbKategoriDetajimArtikulli.Dispose();
        }

        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public clsKategoriDetajimArtikulli()
        {
        }

        public clsKategoriDetajimArtikulli(DataRow rreshti)
        {
            
            mbushKategoriDetajimArtikulli(rreshti);
        }

        #endregion

        #region Metoda Internal

        internal bool mbushKategoriDetajimArtikulli(DataRow dbDataRowKategoriDetajimArtikulli)
        {
            if (dbDataRowKategoriDetajimArtikulli != null)
            {

                try
                {
                    int.TryParse(dbDataRowKategoriDetajimArtikulli["IDKATEGORIDETAJIMI"].ToString(), out idKategoriDetajimi);
                    kodiKategoriDetajimi = dbDataRowKategoriDetajimArtikulli["KODIKATEGORIDETAJIMI"].ToString();
                    pershkrimiKategoriDetajimi = dbDataRowKategoriDetajimArtikulli["PERSHKRIMIKATEGORIDETAJIMI"].ToString();

                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se kategorive te detajimit te artikullit nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}
