using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using DbCore.DbRegjistrim;

namespace DbCore.DbProdhimi
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsTrupiPlanifikim
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colTrupiPlanifikim : List<clsTrupiPlanifikim>
    {
        #region konstruktoret

        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public colTrupiPlanifikim()
        {

        }

        /// <summary>
        /// konstruktori me nje parameter
        /// </summary>
        /// <param name="idkoka"></param>
        public colTrupiPlanifikim(int idkoka, int idndermarje)
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            mbushTrupatPlanifikim(db.ktheTrupiPlanifikim(idkoka, idndermarje));
            db.Dispose();
        }

        public colTrupiPlanifikim(int idkoka, int idndermarje, int idperdoruesi)
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            mbushTrupatPlanifikim(db.ktheTrupiPlanifikimSipasAutorizimit(idkoka, idndermarje, idperdoruesi));
            db.Dispose();
        }

        /// <summary>
        /// konstruktori qe implementon klasen baze
        /// </summary>
        /// <param name="collection">koleksion me clsKokaPlanifikim</param>
        public colTrupiPlanifikim(IEnumerable<clsTrupiPlanifikim> collection)
            : base(collection)
        {

        }
        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbProdhimi.clsTrupiPlanifikim"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary> 
        public new clsTrupiPlanifikim this[int index]
        {
            get { return ((clsTrupiPlanifikim)base[index]); }
        }

        /// <summary>
        /// merr artikujt qe ndodhne ne trup
        /// </summary>
        /// <returns>nje koleksion me te gjithe artikujt qe ndodhen ne trup</returns>
        public DbInventari.colArtikujt ktheColArtikuj()
        {
            DbInventari.colArtikujt colArt = new DbInventari.colArtikujt();
            foreach (clsTrupiPlanifikim trup in this)
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
            foreach (clsTrupiPlanifikim trup in this)
            {
                clsNjesiAdministrative mag = new clsNjesiAdministrative(trup.IdMag);
                colMag.Add(mag);
            }
            return colMag;
        }

        /// <summary>
        /// merr gjithe njesite e artikujve qe ndodhen ne trup
        /// </summary>
        /// <returns>nje koleksion me te gjithe njesite e artikujve qe ndodhen ne trup</returns>
        public DbInventari.colNjesiteArtikulli ktheColNjesiArt()
        {
            DbInventari.colNjesiteArtikulli colNjesi = new DbInventari.colNjesiteArtikulli();
            foreach (clsTrupiPlanifikim trup in this)
            {
                DbInventari.clsNjesiArtikulli njesiArt = new DbInventari.clsNjesiArtikulli(trup.IdNjesia);
                colNjesi.Add(njesiArt);
            }
            return colNjesi;
        }

        /// <summary>
        /// mbush trupin e planifikimit sipas id se kokes se planifikimit
        /// </summary>
        /// <param name="idkoka">id koka e planifikimit</param>
        /// <returns>kthen true nese mbushja kruhet me sukses, ne te kundert false</returns>
        public bool mbushTrupiPlanifikimi(int idkoka, int idndermarje)
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            bool mbush = mbushTrupiPlanifikimi(idkoka, idndermarje, db);
            db.Dispose();
            return mbush;
        }

        /// <summary>
        /// mbush trupin e planifikimit sipas id se kokes se planifikimit
        /// </summary>
        /// <param name="idkoka">id koka e planifikimit</param>
        /// <param name="db">clsDatabazeProdhimi ne rast transaksioni</param>
        /// <returns>kthen true nese mbushja kruhet me sukses, ne te kundert false</returns>
        public bool mbushTrupiPlanifikimi(int idkoka, int idndermarje, clsDatabazeProdhimi db)
        {
            //if (db == null)
            //    db = new clsDatabazeProdhimi();
            bool mbush = mbushTrupatPlanifikim(db.ktheTrupiPlanifikim(idkoka, idndermarje));
            return mbush;
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        /// </summary>
        /// <param name="dt">data table me te dhenat e tipit trupi planifikim</param>
        /// <returns>true ose false ne se coleksioni u mbush ne rregull</returns>
        private bool mbushTrupatPlanifikim(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsTrupiPlanifikim trupi = new clsTrupiPlanifikim();
                    //trupi.mbushTrupPlanifikim(rreshti);
                    Add(new clsTrupiPlanifikim(rreshti));
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
