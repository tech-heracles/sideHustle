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
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsTrupiNdryshimCmimSasi
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colTrupiNdryshimCmimSasi : System.Collections.Generic.List<clsTrupiNdryshimCmimSasi>
    {
        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbRegjistrim.clsTrupiNdryshimCmimSasi"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary> 
        public new clsTrupiNdryshimCmimSasi this[int index]
        {
            get { return ((clsTrupiNdryshimCmimSasi)base[index]); }
        }

        public DbInventari.colArtikujt ktheColArtikuj()
        {
            DbInventari.colArtikujt colArt = new DbInventari.colArtikujt();
            foreach (clsTrupiNdryshimCmimSasi trupMag in this)
            {                
                colArt.Add(new DbInventari.clsArtikulli(trupMag.IdArtikulli));
            }
            return colArt;
        }
       
        public colNjesiAdministrative ktheColMag(int idPerdorues)
        {
            colNjesiAdministrative colMag = new colNjesiAdministrative();
            foreach (clsTrupiNdryshimCmimSasi trupMag in this)
            {
                clsNjesiAdministrative mag = new clsNjesiAdministrative(trupMag.IdMag);
                colMag.Add(mag);
            }
            return colMag;
        }
        
        public DbInventari.colNjesiteArtikulli ktheColNjesiArt()
        {
            DbInventari.colNjesiteArtikulli colNjesi = new DbInventari.colNjesiteArtikulli();
            foreach (clsTrupiNdryshimCmimSasi trupMag in this)
            {
                DbInventari.clsNjesiArtikulli njesiArt = new DbInventari.clsNjesiArtikulli(trupMag.IdNjesia);
                colNjesi.Add(njesiArt);
            }
            return colNjesi;
        }
        /// <summary>
        /// mbush trupin e ndryshim cmim sasi sipas id se kokes se magazines
        /// </summary>
        /// <param name="idKokaMagazina">id koka e magazines</param>
        /// <returns>kthen true nese mbushja kruhet me sukses, ne te kundert false</returns>
        public bool mbushTrupiNdryshimCmimSasi(int idKoka, clsDatabaseRegjistrim dbtrupMagazine)
        {
            return mbushTrupatNdryshimCmimSasi(dbtrupMagazine.ktheTrupiNdryshimCmimSasi(idKoka));
        }
        /// <summary>
        /// mbush trupin e ndryshim cmim sasi sipas id se kokes se ndryshim cmim sasi
        /// </summary>
        /// <param name="idKoka">id koka e ndryshim cmim sasi</param>
        /// <returns>kthen true nese mbushja kruhet me sukses, ne te kundert false</returns>
        public bool mbushTrupiNdryshimCmimSasi(int idKoka)
        {
            clsDatabaseRegjistrim dbtrupMagazine = new clsDatabaseRegjistrim();
            bool sukses = mbushTrupatNdryshimCmimSasi(dbtrupMagazine.ktheTrupiNdryshimCmimSasi(idKoka));
            dbtrupMagazine.Dispose();
            return sukses;
        }
    
        /// <summary>
        /// mbush trupin e ndryshim cmim sasi sipas id se kokes se ndryshim cmim sasi
        /// </summary>
        /// <param name="idKokaMagazina">id koka e ndryshim cmim sasi</param>
        /// <returns>kthen true nese mbushja kruhet me sukses, ne te kundert false</returns>
        public bool mbushTrupiNdryshimCmimSasi(DataTable dt)
        {
            bool mbush = mbushTrupatNdryshimCmimSasi(dt);
            return mbush;
        }

        /// <summary>
        /// mbush te gjithe trupin e ndryshim cmim sasi nga kokat
        /// </summary>
        /// <param name="idKoka">id e koka ndryshim cmim sasi</param>
        /// <returns>kthen true nese mbushja kryhet me sukses ne te kundert false</returns>
        public bool mbushGjitheTrupiNdryshimCmimSasiNgaKoka(int idKoka)
        {
            clsDatabaseRegjistrim dbtrupMagazine = new clsDatabaseRegjistrim();
            bool mbush = mbushTrupatNdryshimCmimSasi(dbtrupMagazine.ktheGjitheTrupiNdryshimCmimSasiNgaKoka(idKoka));
            dbtrupMagazine.Dispose();
            return mbush;
        }
        public bool mbushGjitheTrupiNdryshimCmimSasiNgaKoka(int idKoka, clsDatabaseRegjistrim dbtrupMagazine)
        {
            bool mbush = mbushTrupatNdryshimCmimSasi(dbtrupMagazine.ktheGjitheTrupiNdryshimCmimSasiNgaKoka(idKoka));

            return mbush;
        }

      
        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        /// <see cref="DbCore.DbRegjistrim.clsTrupiNdryshimCmimSasi"/> 
        /// </summary>
        private bool mbushTrupatNdryshimCmimSasi(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsTrupiNdryshimCmimSasi trupi = new clsTrupiNdryshimCmimSasi();
                    //trupi.mbushTrupNdryshimCmimSasi(rreshti);
                    Add(new clsTrupiNdryshimCmimSasi(rreshti));
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
