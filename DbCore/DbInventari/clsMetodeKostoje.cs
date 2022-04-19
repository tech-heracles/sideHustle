using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbInventari
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne metodekostoje
    ///  (Te dhenat  merren nga tabela : T_METODEKOSTOJE)
    /// </summary>
    /// <remarks> eshte tabele ndihmese per metodat e kostos</remarks>
    /// <example> mesatare 1, mesatare 2, mesatare 3, fifo</example>
    public class clsMetodeKostoje
    {
        #region Atributet

        private int idMetodeKostoje;
        private string kodi;
        private string pershkrimi;
        private string shpjegimi;
        private DataRow rreshti;

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdMetodeKostoje
        {
            get
            {
                return idMetodeKostoje;
            }
            set
            {
                idMetodeKostoje = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos kodin e metodes
        /// </summary>
        public string Kodi
        {
            get
            {
                return kodi;
            }
            set
            {
                kodi = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos pershkrimin e metodes
        /// </summary>
        public string Pershkrimi
        {
            get
            {
                return pershkrimi;
            }
            set
            {
                pershkrimi = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos shpjegimi e metodes
        /// </summary>
        public string Shpjegimi
        {
            get
            {
                return shpjegimi;
            }
            set
            {
                shpjegimi = value;
            }
        }
        #endregion

        #region Konstruktoret
        /// <summary>
        /// konstruktori me parametra
        /// </summary>
        /// <param name="idMetodeKostoje">idMetodeKostoje</param>
        /// <param name="kodi">kodi</param>
        /// <param name="pershkrimi">pershkrimi</param>
        /// <param name="shpjegimi">shpjegimi</param>
        public clsMetodeKostoje(int idMetodeKostoje, string kodi, string pershkrimi, string shpjegimi)
        {
            this.idMetodeKostoje = idMetodeKostoje;
            this.kodi = kodi;
            this.shpjegimi = shpjegimi;
            this.pershkrimi = pershkrimi;
        }
        /// <summary>
        /// kontruktori pa parametra
        /// </summary>
        public clsMetodeKostoje()
        {
        }

        public clsMetodeKostoje(DataRow rreshti)
        {
            
            mbushMetodeKostoje(rreshti);
        }

        #endregion

        #region Metoda Internal


        /// <summary>
        ///   mbush metoden e kostos nga databaza
        /// </summary>
        /// <param name="dbDataMetodeKostoje"></param>
        /// <returns></returns>
        internal bool mbushMetodeKostoje(DataRow dbDataMetodeKostoje)
        {
            if (dbDataMetodeKostoje != null)
            {

                try
                {

                    int.TryParse(dbDataMetodeKostoje["IDMETODAKOSTOJE"].ToString(), out idMetodeKostoje);
                    kodi = dbDataMetodeKostoje["KODI"].ToString();
                    pershkrimi = dbDataMetodeKostoje["PERSHKRIMI"].ToString();
                    shpjegimi = dbDataMetodeKostoje["SHPJEGIMI"].ToString();
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se llojeve te makros nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion

        #region metoda publike
        public bool ktheMetodeKostojeSipasId(int id)
        {
            clsDatabaseInventari db = new clsDatabaseInventari();
            bool sukses = mbushMetodeKostoje(db.ktheMetodeKostojeSipasId(id));
            db.Dispose();
            return sukses;
        }
        public bool ktheMetodeKostojeSipasKodit(string kodi)
        {
            clsDatabaseInventari db = new clsDatabaseInventari();
            bool sukses = mbushMetodeKostoje(db.ktheMetodKostojeSipasKodit(kodi));
            db.Dispose();
            return sukses;
        }
        #endregion

    }
}
