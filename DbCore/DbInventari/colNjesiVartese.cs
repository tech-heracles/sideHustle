 using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
namespace DbCore.DbInventari
{ /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsNjesiVartese
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colNjesiVartese: System.Collections.Generic.List<clsNjesiVartese>

    {
        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbRegjistrim.clsNjesiVartese"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary> 
        public new clsNjesiVartese this[int index]
        {
            get { return ((clsNjesiVartese)base[index]); }
        }

        /// <summary>
        /// mbush gjithe njesite administrative
        /// </summary>
        /// <param name="idNderm">id e ndermarrjes</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushGjitheNjesiVartese(int idNderm)
        {
            clsDatabaseInventari dbNjesiVartese = new clsDatabaseInventari();
            bool sukses = mbushNjesiteVartese(dbNjesiVartese.ktheGjitheNjesiVartese(idNderm));
            dbNjesiVartese.Dispose();
            return sukses;
        }

        
        public static DataRow merrSipasNjesiNdermarrjesDR( int iddege)
            {
            clsDatabaseInventari dbartikuj = new clsDatabaseInventari();
            DataRow rreshti = dbartikuj.merrSipasNjesiNdermarrjesDR(iddege);
            dbartikuj.Dispose();
            return rreshti;
            }
        public static DataTable merrSipasNjesiNdermarrjesDT(int idnderm)
        {
            clsDatabaseInventari dbartikuj = new clsDatabaseInventari();
            DataTable tabela = dbartikuj.merrSipasNjesiNdermarrjesDT(idnderm);
            dbartikuj.Dispose();
            return tabela;
        }

        public static DataTable merrSipasNjesiNdermarrjesDTMeFilter(string filter, long startIndex, long endIndex, int idnderm)
            {
            clsDatabaseInventari dbartikuj = new clsDatabaseInventari();
            DataTable tabela = dbartikuj.merrSipasNjesiNdermarrjesDTMeFilter(filter, startIndex, endIndex, idnderm);
            dbartikuj.Dispose();
            return tabela;
            }

        public static DataTable merrSipasNjesiNdermarrjesDTMeID(int idnderm, int value)
        {
            clsDatabaseInventari dbartikuj = new clsDatabaseInventari();
            DataTable tabela = dbartikuj.merrSipasNjesiNdermarrjesDTMeID(idnderm, value);
            dbartikuj.Dispose();
            return tabela;
        }


        #endregion
        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbRegjistrim.clsNjesiVartese"/> 
        /// </summary>
        private bool mbushNjesiteVartese(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsNjesiVartese nivel = new clsNjesiVartese();
                    //nivel.mbushNjesiVartese(rreshti);
                    Add(new clsNjesiVartese(rreshti));
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

