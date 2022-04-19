using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DbCore.DbAdmin;
using System.Data;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne inventarizimin
    ///  (Te dhenat  merren nga tabela : T_INVENTARIZIM)
    /// </summary>
    /// <remarks > kjo klase eshte klase ndihmese qe sherben per te shfaqur tipet e inventarizimeve tek comboboxet qe i duhen magazines</remarks>
    public class clsInventarizim
    {
        #region Atribute

        private int idInventarizim;
        private string kodi;
        private string pershkrimi;
        private DataRow rreshti;

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases me parametra
        /// </summary>
        /// <param name="idinv">id ritese e inventarizimit</param>
        /// <param name="k"> kodi</param>
        /// <param name="persh"> pershkrimi</param>
        public clsInventarizim(int idinv, string k, string persh)
        {
            idInventarizim = idinv;
            kodi = k;
            pershkrimi = persh;
           
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idInv">id e inventarizimit</param>
        public clsInventarizim(int idInv)
        {
            clsDatabaseRegjistrim dbInventarizim = new clsDatabaseRegjistrim();
            mbushInventar(dbInventarizim.ktheInventarizimSipasId(idInv));
            dbInventarizim.Dispose();
        }

        /// <summary>
        /// Konstruktori i klases pa parametra
        /// </summary>
        public clsInventarizim()
        {
        }

        public clsInventarizim(DataRow rreshti)
        {
            
            mbushInventar(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdInventarizimi
        {
            get { return idInventarizim; }
            set { idInventarizim = value; }
        }

        /// <summary>
        /// Kthen/Vendos pershkrimin.
        /// </summary>
        public String Pershkrimi
        {
            get { return pershkrimi; }
            set { pershkrimi = value; }
        }

        /// <summary>
        /// Kthen/Vendos Kodin.
        /// </summary>
        public String Kodi
        {
            get { return kodi; }
            set { kodi = value; }
        }

        /// <summary>
        /// merr objektin e llojit te inventarizimit nga tabela perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.ktheInventarizimSipasId"/> 
        /// </summary>
        /// <returns > nje objekt clsInventarizim  qe permban tipin e inventarizimit sipas id se dhene</returns>
        public clsInventarizim merrSipasId()
        {
            clsInventarizim data = new clsInventarizim(this.IdInventarizimi);
            return data;
        }

        //public colInventarizim merriTeGjithe()
        //{
        //    clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
        //    return data.merrGjitheInventarizime();
        //}

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush inventarizimin nga databaza
        /// </summary>
        /// <param name="dbDataRowInventar">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushInventar(DataRow dbDataRowInventar)
        {
            if (dbDataRowInventar != null)
            {
                try
                {
                    int.TryParse(dbDataRowInventar["IDINVENTARIZIM"].ToString(), out idInventarizim);
                    kodi = dbDataRowInventar["KODI"].ToString();
                    pershkrimi = dbDataRowInventar["PERSHKRIMI"].ToString();
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se inventarizimit nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}
