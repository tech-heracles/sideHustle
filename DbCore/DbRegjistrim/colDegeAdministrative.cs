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
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsPikeShitjeFurnizimi
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colDegeAdministrative : System.Collections.Generic.List<clsDegeAdministrative>
    {
        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbRegjistrim.clsPikeShitjeFurnizimi"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary> 
        public new clsDegeAdministrative this[int index]
        {
            get { return ((clsDegeAdministrative)base[index]); }
        }

        /// <summary>
        /// mbush gjithe njesite administrative
        /// </summary>
        /// <param name="idNderm">id e ndermarrjes</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushGjitheDegeAdministrative(int idNderm)
        {
            clsDatabaseRegjistrim dbNjesiAdministrative = new clsDatabaseRegjistrim();
            bool mbush = mbushDegetAdministrative(dbNjesiAdministrative.ktheGjitheDegeAdministrative(idNderm));
            dbNjesiAdministrative.Dispose();
            return mbush;
        }

        public static DataTable ktheGjitheDegeAdministrativeDtSmall(int idNderm)
        {
            using (clsDatabaseRegjistrim dbNjesiAdministrative = new clsDatabaseRegjistrim())
            {

               return dbNjesiAdministrative.ktheGjitheDegeAdministrativeDtSmall(idNderm);
            }
           
        }

        /// <summary>
        /// mbush gjithe njesite administrative aktive
        /// </summary>
        /// <param name="idNderm">id e ndermarrjes</param>
        /// <returns>kthen truen nese mbushja kryhet me sukses, ne te kunder false</returns>
        public bool mbushGjitheDegeAdministrativeAktive(int idNderm)
        {
            clsDatabaseRegjistrim dbNjesiAdministrative = new clsDatabaseRegjistrim();
            bool mbush = mbushDegetAdministrative(dbNjesiAdministrative.ktheGjitheDegeAdministrativeAktive(idNderm));
            dbNjesiAdministrative.Dispose();
            return mbush;
        }
        public bool mbushGjitheDegeAdministrativeAktivePerKonfigurim(int idNderm)
        {
            clsDatabaseRegjistrim dbNjesiAdministrative = new clsDatabaseRegjistrim();
            bool mbush = mbushDegetAdministrativePerKonfigurim(dbNjesiAdministrative.ktheGjitheDegeAdministrativeAktive(idNderm));
            dbNjesiAdministrative.Dispose();
            return mbush;
        }

        public static DataRow merrSipasDegeNdermarrjesDR(int iddege)
        {
            clsDatabaseRegjistrim dbartikuj = new clsDatabaseRegjistrim();
            DataRow dr = dbartikuj.merrSipasDegeNdermarrjesDR(iddege);
            dbartikuj.Dispose();
            return dr;
        }

        public static DataTable merrSipasDegeNdermarrjesDT(int idnderm)
        {
            clsDatabaseRegjistrim dbartikuj = new clsDatabaseRegjistrim();
            DataTable dt = dbartikuj.merrSipasDegeNdermarrjesDT(idnderm);
            dbartikuj.Dispose();
            return dt;
        }

        public static DataTable merrSipasDegeNdermarrjesDTExport(int idnderm)
        {
            clsDatabaseRegjistrim dbartikuj = new clsDatabaseRegjistrim();
            DataTable dt = dbartikuj.merrSipasDegeNdermarrjesDTExport(idnderm);
            dbartikuj.Dispose();
            return dt;
        }

        public static DataTable merrSipasDegeNdermarjePerLupeDege(int idnderm)
        {
            clsDatabaseRegjistrim dbartikuj = new clsDatabaseRegjistrim();
            DataTable dt = dbartikuj.merrSipasDegeNdermarjePerLupeDege(idnderm);
            dbartikuj.Dispose();
            return dt;
        }

        public static DataTable merrDegeAdministrativeSipasNdermarrjesPerKombo(string filter, long startIndex, long endIndex, int idNdermarrje)
        {
            clsDatabaseRegjistrim dbRegjistrim = new clsDatabaseRegjistrim();
            DataTable table = dbRegjistrim.merrDegeAdminSipasNdermarrjeDheKoditPerKombo(filter, startIndex, endIndex, idNdermarrje);
            dbRegjistrim.Dispose();
            return table;
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbRegjistrim.clsPikeShitjeFurnizimi"/> 
        /// </summary>
        private bool mbushDegetAdministrative(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsDegeAdministrative nivel = new clsDegeAdministrative();
                    //nivel.mbushDegeAdministrative(rreshti);
                    Add(new clsDegeAdministrative(rreshti));
                }

            //}
            //catch (Exception)
            //{
            //    return false;
            //    //throw;
            //}
            return true;
        }
        private bool mbushDegetAdministrativePerKonfigurim(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsDegeAdministrative nivel = new clsDegeAdministrative();
                    //nivel.mbushDegeAdministrative(rreshti);
                    Add(new clsDegeAdministrative(rreshti,true));
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
