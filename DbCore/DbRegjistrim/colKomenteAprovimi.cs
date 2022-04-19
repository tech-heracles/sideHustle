using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
namespace DbCore.DbRegjistrim
{

    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsKomenteAprovimi
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colKomenteAprovimi : System.Collections.Generic.List<clsKomenteAprovimi>
    {
        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbRegjistrim.clsKomenteAprovimi"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsKomenteAprovimi this[int index]
        {
            get { return ((clsKomenteAprovimi)base[index]); }
        }


        /// <summary>
        /// mbush komentet e kesaj etape
        /// </summary>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public bool merrKomenteSipasEtapes(int idetapa,clsDatabaseRegjistrim db )
        {
           return mbushFurnitoreArtikujsh(db.ktheKomentAprovimiSipasIdEtape(idetapa));
        }
        /// <summary>
        /// mbush komentet e kesaj etape
        /// </summary>
        /// <param name="idetapa">id e etapes</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public bool merrKomenteSipasEtapes(int idetapa)
        {
            using (clsDatabaseRegjistrim db = new clsDatabaseRegjistrim())
            {
                return merrKomenteSipasEtapes(idetapa, db);
            }
        }

         /// <summary>
        /// mbush komentet e ketij procesi
        /// </summary>
        /// <param name="nrProcesi">nr i procesit</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public bool merrKomenteSipasProcesit(int nrProcesi, int idndermarje)
        {
            using (clsDatabaseRegjistrim db = new clsDatabaseRegjistrim())
            {
                return mbushFurnitoreArtikujsh(db.ktheKomentAprovimiSipasNrProcesi(nrProcesi, idndermarje));
            }
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbInventari.clsFurnitoreArtikulli"/> 
        /// </summary>
        private bool mbushFurnitoreArtikujsh(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsKomenteAprovimi koment = new clsKomenteAprovimi();
                    //koment.mbushKomenteAprovimi(rreshti);
                    this.Add(new clsKomenteAprovimi(rreshti));
                }

            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }

        #endregion
    
    }
}
