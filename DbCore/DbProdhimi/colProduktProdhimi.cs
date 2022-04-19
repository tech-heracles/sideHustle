using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using DbCore.DbRegjistrim;

namespace DbCore.DbProdhimi
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsProduktProdhimi
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colProduktProdhimi : List<clsProduktProdhimi>
    {
        #region konstruktoret

        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public colProduktProdhimi()
        {

        }

        /// <summary>
        /// konstruktori me nje parameter
        /// </summary>
        /// <param name="Idkoka">id e kokes</param>
        public colProduktProdhimi(int Idkoka)
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            mbushProdukt(db.ktheGjitheProdukteProdhimiNgaKoka(Idkoka));
            db.Dispose();
        }

        /// <summary>
        /// konstruktori qe implementon klasen baze
        /// </summary>
        /// <param name="collection">koleksion me clsProduktProdhimi</param>
        public colProduktProdhimi(IEnumerable<clsProduktProdhimi> collection)
            : base(collection)
        {

        }
        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbProdhimi.clsProduktProdhimi"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary> 
        public new clsProduktProdhimi this[int index]
        {
            get { return ((clsProduktProdhimi)base[index]); }
        }

        /// <summary>
        /// merr artikujt qe ndodhne ne trup
        /// </summary>
        /// <returns>nje koleksion me te gjithe artikujt qe ndodhen ne trup</returns>
        public DbInventari.colArtikujt ktheColArtikuj()
        {
            DbInventari.colArtikujt colArt = new DbInventari.colArtikujt();
            foreach (clsProduktProdhimi trup in this)
            {                
                colArt.Add(new DbInventari.clsArtikulli(trup.IdArtikulli));
            }
            return colArt;
        }

        /// <summary>
        /// merr gjithe magazinat qe ndodhen ne trup
        /// </summary>
        /// <returns>nje koleksion me te gjitha magazinat qe ndodhen ne trup</returns>
        public colNjesiAdministrative ktheColMag(int idPerdorues)
        {
            colNjesiAdministrative colMag = new colNjesiAdministrative();
            foreach (clsProduktProdhimi trup in this)
            {
                clsNjesiAdministrative mag = new clsNjesiAdministrative(trup.IdMag);
                colMag.Add(mag);
            }
            return colMag;
        }

        /// <summary>
        /// mbush produktin sipas idkoka
        /// </summary>
        /// <param name="idKoka">id koka</param>
        /// <param name="db">clsDatabazeProdhimi ne rast transaksioni</param>
        /// <returns>kthen true nese mbushja kruhet me sukses, ne te kundert false</returns>
        public bool mbushProduktSipasIdkoka(int idKoka)
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            bool mbush = mbushProduktSipasIdkoka(idKoka, db);
            db.Dispose();
            return mbush;
        }

        /// <summary>
        /// mbush produktin sipas idkoka
        /// </summary>
        /// <param name="idKoka">id koka</param>
        /// <param name="db">clsDatabazeProdhimi ne rast transaksioni</param>
        /// <returns>kthen true nese mbushja kruhet me sukses, ne te kundert false</returns>
        public bool mbushProduktSipasIdkoka(int idKoka, clsDatabazeProdhimi db)
        {
            //if (db == null)
            //    db = new clsDatabazeProdhimi();
            bool mbush = mbushProdukt(db.ktheGjitheProdukteProdhimiNgaKoka(idKoka));
            return mbush;
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        /// </summary>
        /// <param name="dt">data table me te dhenat e tipit produkt</param>
        /// <returns>true ose false ne se coleksioni u mbush ne rregull</returns>
        private bool mbushProdukt(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsProduktProdhimi trupi = new clsProduktProdhimi();
                    //trupi.mbushProdukt(rreshti);
                    Add(new clsProduktProdhimi(rreshti));
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
