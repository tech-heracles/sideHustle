using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using AlphaWeb.Core.Interfaces.Data;
using DbCore.DbRegjistrim;
using DbCore.IMBUtils.DataBase;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsKokaRecetaOptike
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    /// <seealso cref="clsKokaRecetaOptike"/>
    public class colKokaRecetaOptike : List<clsKokaRecetaOptike>, IDataBase
    {
        #region Konstruktori

        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public colKokaRecetaOptike()
        {
        }
        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idNdermVit">id qe lidh ndermarrjen me vitin</param>
        public colKokaRecetaOptike(int idNdermarrje)
        {
            using (clsDatabaseRegjistrim db = new clsDatabaseRegjistrim())
                db.ktheGjitheKokaRecetaOptike(idNdermarrje, this);

        }
        


        /// <summary>
        /// konstruktori qe implementon klasen baze
        /// </summary>
        /// <param name="collection">koleksion me clsKokaRecetaOPtike</param>
        public colKokaRecetaOptike(IEnumerable<clsKokaRecetaOptike> collection)
            : base(collection)
        {

        }


        #endregion

        #region Metoda Publike


        public void Mbush(IDataRecord record)
        {
            Add(new clsKokaRecetaOptike(record));
        }


        public clsMesazh Ruaj() { throw new NotImplementedException(); }

        public clsMesazh Modifiko() { throw new NotImplementedException(); }

        public clsMesazh Fshi() { throw new NotImplementedException(); }

        public static DataTable MerrRecetaSipasPeriudhes(int idNdermarrje, string datanga, string dataderi)
        {
            using (clsDatabaseRegjistrim db = new clsDatabaseRegjistrim())
               return db.merrKokaRecetaOptikeSipasPeriudhes(idNdermarrje, datanga, dataderi);
        }


        #endregion
        #region Metoda Private


        #endregion

    }

}

