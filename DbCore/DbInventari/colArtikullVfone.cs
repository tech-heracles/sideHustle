using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DbCore.DbInventari
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsArtikullVfone
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colArtikullVfone : System.Collections.Generic.List<clsArtikullVfone>
    {

        #region Metoda publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbInventari.clsArtikullVfone"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsArtikullVfone this[int index]
        {
            get { return ((clsArtikullVfone)base[index]); }
        }

        /// <summary>
        /// merr perberesit sipas id se artikullit kryesore
        /// </summary>
        /// <param name="id">id e artikullit kryesore</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        /// <param name="db"></param>
        public bool merrSipasIdArtikull(int id, clsDatabaseInventari db)
        {
            return mbushArtikujVfone(db.ktheArtikujVfoneSipasIdArt(id));
        }

        /// <summary>
        /// merr perberesit sipas id se artikullit kryesore
        /// </summary>
        /// <param name="id">id e artikullit kryesore</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        /// <param name="db"></param>
        public bool merrSipasIdArtikull(int id)
        {
            clsDatabaseInventari db = new clsDatabaseInventari();
            bool sukses = merrSipasIdArtikull(id, db);
            db.Dispose();
            return sukses;
        }



        #endregion

        #region Metoda private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        /// nepermjet kesaj metode thirret per nje metode qe ben mbushjen nga databaza
        /// <see cref="DbCore.DbInventari.clsArtikullVfone"/> 
        /// </summary>
        private bool mbushArtikujVfone(DataTable dt)
        {
            try
            {

                foreach (DataRow rreshti in dt.Rows)
                {
                    clsArtikullVfone art = new clsArtikullVfone();
                    art.mbushArtikullVfone(rreshti);
                    this.Add(art);
                }

            }
            catch (Exception)
            {
                return false;
                //throw;
            }
            return true;

        }
        #endregion

    }
}