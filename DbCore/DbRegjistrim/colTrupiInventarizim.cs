using DbCore.DbRegjistrim;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.DbRegjistrim
{ 
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsTrupiInventarizim
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colTrupiInventarizim : System.Collections.Generic.List<clsTrupiInventarizim>
    {
        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbRegjistrim.clsTrupiInventarizim"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary> 
        public new clsTrupiInventarizim this[int index]
        {
            get { return ((clsTrupiInventarizim)base[index]); }
        }

    
        /// <summary>
        /// mbush trupin e ndryshim cmim sasi sipas id se kokes se magazines
        /// </summary>
        /// <param name="idKokaMagazina">id koka e magazines</param>
        /// <returns>kthen true nese mbushja kruhet me sukses, ne te kundert false</returns>
        public bool mbushTrupiInventarizim(int idKoka, clsDatabaseRegjistrim dbtrupMagazine)
        {
            return mbushTrupatInventarizim(dbtrupMagazine.ktheGjitheTrupiInventarizimNgaKoka(idKoka));
        }
        /// <summary>
        /// mbush trupin e ndryshim cmim sasi sipas id se kokes se ndryshim cmim sasi
        /// </summary>
        /// <param name="idKoka">id koka e ndryshim cmim sasi</param>
        /// <returns>kthen true nese mbushja kruhet me sukses, ne te kundert false</returns>
        public bool mbushTrupiInventarizim(int idKoka)
        {
            clsDatabaseRegjistrim dbtrupMagazine = new clsDatabaseRegjistrim();
            bool sukses = mbushTrupatInventarizim(dbtrupMagazine.ktheGjitheTrupiInventarizimNgaKoka(idKoka));
            dbtrupMagazine.Dispose();
            return sukses;
        }

        /// <summary>
        /// mbush trupin e ndryshim cmim sasi sipas id se kokes se ndryshim cmim sasi
        /// </summary>
        /// <param name="idKokaMagazina">id koka e ndryshim cmim sasi</param>
        /// <returns>kthen true nese mbushja kruhet me sukses, ne te kundert false</returns>
        public bool mbushTrupiInventarizim(DataTable dt)
        {
            bool mbush = mbushTrupatInventarizim(dt);
            return mbush;
        }

        /// <summary>
        /// mbush te gjithe trupin e ndryshim cmim sasi nga kokat
        /// </summary>
        /// <param name="idKoka">id e koka ndryshim cmim sasi</param>
        /// <returns>kthen true nese mbushja kryhet me sukses ne te kundert false</returns>
        public bool mbushGjitheTrupiInventarizimNgaKoka(int idKoka)
        {
            clsDatabaseRegjistrim dbtrupMagazine = new clsDatabaseRegjistrim();
            bool mbush = mbushTrupatInventarizim(dbtrupMagazine.ktheGjitheTrupiInventarizimNgaKoka(idKoka));
            dbtrupMagazine.Dispose();
            return mbush;
        }
        public bool mbushGjitheTrupiInventarizimNgaKoka(int idKoka, clsDatabaseRegjistrim dbtrupMagazine)
        {
            bool mbush = mbushTrupatInventarizim(dbtrupMagazine.ktheGjitheTrupiInventarizimNgaKoka(idKoka));

            return mbush;
        }


        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        /// <see cref="DbCore.DbRegjistrim.clsTrupiInventarizim"/> 
        /// </summary>
        private bool mbushTrupatInventarizim(DataTable dt)
        {
            //try
            //{

            foreach (DataRow rreshti in dt.Rows)
            {
                //clsTrupiInventarizim trupi = new clsTrupiInventarizim();
                //trupi.mbushTrupNdryshimCmimSasi(rreshti);
                Add(new clsTrupiInventarizim(rreshti));
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

