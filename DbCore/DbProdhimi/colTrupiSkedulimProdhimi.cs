using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using DbCore.DbRegjistrim;

namespace DbCore.DbProdhimi
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsTrupiSkedulimProdhimi
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colTrupiSkedulimProdhimi : List<clsTrupiSkedulimProdhimi>
    {
        #region konstruktoret

        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public colTrupiSkedulimProdhimi()
        {

        }

        /// <summary>
        /// konstruktori me nje parameter
        /// </summary>
        /// <param name="idkoka"></param>
        public colTrupiSkedulimProdhimi(int idkoka, int idndermarje)
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            mbushTrupatSkedulimit(db.ktheTrupiSkedulimProdhimi(idkoka, idndermarje));
            db.Dispose();
        }

        public colTrupiSkedulimProdhimi(int idkoka, int idndermarje, int idperdoruesi)
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            mbushTrupatSkedulimit(db.ktheTrupiSkedulimProdhimiSipasAutorizimit(idkoka, idndermarje, idperdoruesi));
            db.Dispose();
        }

        /// <summary>
        /// konstruktori qe implementon klasen baze
        /// </summary>
        /// <param name="collection">koleksion me clsKokaPlanifikim</param>
        public colTrupiSkedulimProdhimi(IEnumerable<clsTrupiSkedulimProdhimi> collection)
            : base(collection)
        {

        }
        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbProdhimi.clsTrupiSkedulimProdhimi"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary> 
        public new clsTrupiSkedulimProdhimi this[int index]
        {
            get { return ((clsTrupiSkedulimProdhimi)base[index]); }
        }

        /// <summary>
        /// merr artikujt qe ndodhne ne trup
        /// </summary>
        /// <returns>nje koleksion me te gjithe artikujt qe ndodhen ne trup</returns>
        public DbInventari.colArtikujt ktheColArtikuj()
        {
            DbInventari.colArtikujt colArt = new DbInventari.colArtikujt();
            foreach (clsTrupiSkedulimProdhimi trup in this)
            {                
                colArt.Add(new DbInventari.clsArtikulli(trup.IdProdukti));
            }
            return colArt;
        }

        /// <summary>
        /// merr gjithe magazinat qe ndodhen ne trup
        /// </summary>
        /// <returns>nje koleksion me te gjitha magazinat qe ndodhen ne trup</returns>
        public colBurimet ktheColBur(int idPerdorues)
        {
            colBurimet colMag = new colBurimet();
            foreach (clsTrupiSkedulimProdhimi trup in this)
            {
                clsBurime mag = new  clsBurime (trup.IdBurimi);
                colMag.Add(mag);
            }
            return colMag;
        }
        public colAktiviteteKoka ktheColAkt(int idPerdorues)
        {
            colAktiviteteKoka colMag = new colAktiviteteKoka();
            foreach (clsTrupiSkedulimProdhimi trup in this)
            {
                clsAktiviteteKoka mag = new   clsAktiviteteKoka  (trup.IdAktiviteti);
              
                colMag.Add(mag);
            }
            return colMag;
        }
        public colKokaPlanifikim ktheColPlanifikime(int idPerdorues)
        {
            colKokaPlanifikim colMag = new colKokaPlanifikim();
            foreach (clsTrupiSkedulimProdhimi trup in this)
            {
                clsKokaPlanifikim mag = new    clsKokaPlanifikim   (trup.IdPlanifikimi);
                colMag.Add(mag);
            }
            return colMag;
        }
        ///// <summary>
        ///// merr gjithe njesite e artikujve qe ndodhen ne trup
        ///// </summary>
        ///// <returns>nje koleksion me te gjithe njesite e artikujve qe ndodhen ne trup</returns>
        //public DbInventari.colNjesiteArtikulli ktheColNjesiArt()
        //{
        //    DbInventari.colNjesiteArtikulli colNjesi = new DbInventari.colNjesiteArtikulli();
        //    foreach (clsTrupiSkedulimProdhimi trup in this)
        //    {
        //        DbInventari.clsNjesiArtikulli njesiArt = new DbInventari.clsNjesiArtikulli(trup.IdNjesia);
        //        colNjesi.Add(njesiArt);
        //    }
        //    return colNjesi;
        //}

        /// <summary>
        /// mbush trupin e skedulimit sipas id se kokes se skedulimit
        /// </summary>
        /// <param name="idkoka">id koka e skedulimit</param>
        /// <returns>kthen true nese mbushja kruhet me sukses, ne te kundert false</returns>
        public bool ktheTrupiSkedulimProdhimi(int idkoka, int idndermarje)
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            bool mbush = ktheTrupiSkedulimProdhimi(idkoka, idndermarje, db);
            db.Dispose();
            return mbush;
        }

        /// <summary>
        /// mbush trupin e skedulimit sipas id se kokes se skedulimit
        /// </summary>
        /// <param name="idkoka">id koka e skedulimit</param>
        /// <param name="db">clsDatabazeProdhimi ne rast transaksioni</param>
        /// <returns>kthen true nese mbushja kruhet me sukses, ne te kundert false</returns>
        public bool ktheTrupiSkedulimProdhimi(int idkoka, int idndermarje, clsDatabazeProdhimi db)
        {
            //if (db == null)
            //    db = new clsDatabazeProdhimi();
            bool mbush = mbushTrupatSkedulimit(db.ktheTrupiSkedulimProdhimi(idkoka, idndermarje));
            return mbush;
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        /// </summary>
        /// <param name="dt">data table me te dhenat e tipit trupi skedulim</param>
        /// <returns>true ose false ne se coleksioni u mbush ne rregull</returns>
        private bool mbushTrupatSkedulimit(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsTrupiSkedulimProdhimi trupi = new clsTrupiSkedulimProdhimi();
                    //trupi.mbushTrupSkedulim(rreshti);
                    Add(new clsTrupiSkedulimProdhimi(rreshti));
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
