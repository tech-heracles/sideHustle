using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsTrupiRiparime
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colTrupiRiparime : List<clsTrupiRiparime>
    {
        #region konstruktoret

        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public colTrupiRiparime()
        {

        }

        /// <summary>
        /// konstruktori me nje parameter
        /// </summary>
        /// <param name="idkoka"></param>
        public colTrupiRiparime(int idkoka, int idndermarje)
        {
            clsDatabaseRegjistrim db = new clsDatabaseRegjistrim();
            mbushTrupatRiparime(db.ktheTrupiRiparime(idkoka, idndermarje));
            db.Dispose();
        }


        /// <summary>
        /// konstruktori qe implementon klasen baze
        /// </summary>
        /// <param name="collection">koleksion me clsTrupiRiparime</param>
        public colTrupiRiparime(IEnumerable<clsTrupiRiparime> collection)
            : base(collection)
        {

        }
        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="clsTrupiRiparime"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary> 
        public new clsTrupiRiparime this[int index]
        {
            get { return ((clsTrupiRiparime)base[index]); }
        }

        ///// <summary>
        ///// merr artikujt qe ndodhne ne trup
        ///// </summary>
        ///// <returns>nje koleksion me te gjithe artikujt qe ndodhen ne trup</returns>
        //public DbInventari.colArtikujt ktheColArtikuj()
        //{
        //    DbInventari.colArtikujt colArt = new DbInventari.colArtikujt();
        //    foreach (clsTrupiRiparime trup in this)
        //    {
        //        DbInventari.clsArtikulli art = new DbInventari.clsArtikulli(trup.IdArtikulli);
        //        colArt.Add(art);
        //    }
        //    return colArt;
        //}

        ///// <summary>
        ///// merr gjithe magazinat qe ndodhen ne trup
        ///// </summary>
        ///// <returns>nje koleksion me te gjitha magazinat qe ndodhen ne trup</returns>
        //public colNjesiAdministrative ktheColMag(int idPerdorues)
        //{
        //    colNjesiAdministrative colMag = new colNjesiAdministrative();
        //    foreach (clsTrupiRiparime trup in this)
        //    {
        //        clsNjesiAdministrative mag = new clsNjesiAdministrative(trup.IdMag, idPerdorues);
        //        colMag.Add(mag);
        //    }
        //    return colMag;
        //}

        ///// <summary>
        ///// merr gjithe njesite e artikujve qe ndodhen ne trup
        ///// </summary>
        ///// <returns>nje koleksion me te gjithe njesite e artikujve qe ndodhen ne trup</returns>
        //public DbInventari.colNjesiteArtikulli ktheColNjesiArt()
        //{
        //    DbInventari.colNjesiteArtikulli colNjesi = new DbInventari.colNjesiteArtikulli();
        //    foreach (clsTrupiRiparime trup in this)
        //    {
        //        DbInventari.clsNjesiArtikulli njesiArt = new DbInventari.clsNjesiArtikulli(trup.IdNjesia);
        //        colNjesi.Add(njesiArt);
        //    }
        //    return colNjesi;
        //}

        /// <summary>
        /// mbush trupin e riparimit sipas id se kokes se riparimit
        /// </summary>
        /// <param name="idkoka">id koka e riparimit</param>
        /// <returns>kthen true nese mbushja kruhet me sukses, ne te kundert false</returns>
        public bool mbushTrupiRiparime(int idkoka, int idndermarje)
        {
            clsDatabaseRegjistrim db = new clsDatabaseRegjistrim();
            bool mbush = mbushTrupiRiparime(idkoka, idndermarje, db);
            db.Dispose();
            return mbush;
        }

        /// <summary>
        /// mbush trupin e riparim sipas id se kokes se riparim
        /// </summary>
        /// <param name="idkoka">id koka e planifikimit</param>
        /// <param name="db">clsDatabazeProdhimi ne rast transaksioni</param>
        /// <returns>kthen true nese mbushja kruhet me sukses, ne te kundert false</returns>
        public bool mbushTrupiRiparime(int idkoka, int idndermarje, clsDatabaseRegjistrim db)
        {

            bool mbush = mbushTrupatRiparime(db.ktheTrupiRiparime(idkoka, idndermarje));
            return mbush;
        }
        public static DataTable ktheTrupiRiparimeDT(int idkoka)
        {
            clsDatabaseRegjistrim db = new clsDatabaseRegjistrim();
            return db.ktheTrupiRiparimeDT(idkoka);

        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        /// </summary>
        /// <param name="dt">data table me te dhenat e tipit trupi riparim</param>
        /// <returns>true ose false ne se coleksioni u mbush ne rregull</returns>
        private bool mbushTrupatRiparime(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsTrupiRiparime trupi = new clsTrupiRiparime();
                    //trupi.mbushTrupRiparime(rreshti);
                    Add(new clsTrupiRiparime(rreshti));
                }

            //}
            //catch (Exception)
            //{
            //    return false;
            //    //throw;
            //}
            return true;
        }

        #endregion

    }
}
