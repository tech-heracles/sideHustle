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
    public class colPikaShitjeFurnizimi : System.Collections.Generic.List<clsPikeShitjeFurnizimi>
    {
        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbRegjistrim.clsPikeShitjeFurnizimi"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary> 
        public new clsPikeShitjeFurnizimi this[int index]
        {
            get { return ((clsPikeShitjeFurnizimi)base[index]); }
        }

        /// <summary>
        /// mbush gjithe njesite administrative
        /// </summary>
        /// <param name="idNderm">id e ndermarrjes</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushGjithePikeShitjeFurnizimi(int idNderm)
        {
            clsDatabaseRegjistrim dbNjesiAdministrative = new clsDatabaseRegjistrim();
            bool mbush = mbushPikaShitjeFurnizimi(dbNjesiAdministrative.ktheGjithePikaShitjeFurnizimi(idNderm));
            dbNjesiAdministrative.Dispose();
            return mbush;
        } 
        public static DataTable mbushGjithePikeShitjeFurnizimiDtSmall(int idNderm)
        {
            using (clsDatabaseRegjistrim dbNjesiAdministrative = new clsDatabaseRegjistrim())
            {
               return dbNjesiAdministrative.ktheGjithePikaShitjeFurnizimiDtSmall(idNderm);
            }
            
        }
        public static DataTable ktheKoordinatatGjithePikaveDheShitje(int idnderm, int idPerd)
        {
            using (DbCore.DbRegjistrim.clsDatabaseRegjistrim dbRegj = new DbCore.DbRegjistrim.clsDatabaseRegjistrim())
            {
                return  dbRegj.ktheKoordinatatGjithePikaveDheShitje(idnderm, idPerd);
              
            }
        }
        /// <summary>
        /// mbush gjithe njesite administrative aktive
        /// </summary>
        /// <param name="idNderm">id e ndermarrjes</param>
        /// <returns>kthen truen nese mbushja kryhet me sukses, ne te kunder false</returns>
        public bool mbushGjithePikeShitjeFurnizimiAktive(int idNderm)
        {
            clsDatabaseRegjistrim dbNjesiAdministrative = new clsDatabaseRegjistrim();
            bool mbush = mbushPikaShitjeFurnizimi(dbNjesiAdministrative.ktheGjithePikaShitjeFurnizimiAktive(idNderm));
            dbNjesiAdministrative.Dispose();
            return mbush;
        }
        /// <summary>
        /// mbush gjithe pikat e shitjeve aktive
        /// </summary>
        /// <param name="idNderm">id e ndermarrjes</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushGjithePikeShitjeAktive(int idNderm)
        {
            clsDatabaseRegjistrim dbNjesiAdministrative = new clsDatabaseRegjistrim();
            bool mbush = mbushPikaShitjeFurnizimi(dbNjesiAdministrative.ktheGjithePikaShitjeAktive(idNderm));
            dbNjesiAdministrative.Dispose();
            return mbush;
        }

        /// <summary>
        /// mbush gjithe pikat e furnizimit aktive
        /// </summary>
        /// <param name="idNderm">id e ndermarrjes</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushGjithePikeFurnizimiAktive(int idNderm)
        {
            clsDatabaseRegjistrim dbNjesiAdministrative = new clsDatabaseRegjistrim();
            bool mbush = mbushPikaShitjeFurnizimi(dbNjesiAdministrative.ktheGjithePikaFurnizimiAktive(idNderm));
            dbNjesiAdministrative.Dispose();
            return mbush;
        }

        public bool mbushGjithePikaShitje(int idnderm)
        {
            clsDatabaseRegjistrim dbNjesiAdministrative = new clsDatabaseRegjistrim();
            bool mbush = mbushPikaShitjeFurnizimi(dbNjesiAdministrative.ktheGjithePikaShitje(idnderm));
            dbNjesiAdministrative.Dispose();
            return mbush;
        }

        public bool mbushGjithePikaFurnizimi(int idnderm)
        {
            clsDatabaseRegjistrim dbNjesiAdministrative = new clsDatabaseRegjistrim();
            bool mbush = mbushPikaShitjeFurnizimi(dbNjesiAdministrative.ktheGjithePikaFurnizimi(idnderm));
            dbNjesiAdministrative.Dispose();
            return mbush;
        }

        public bool mbushGjithePikatSipasLlojitDheAktiveOseJo(int idnderm, bool lloji, bool merrDheJoAktive)
        {
            using (clsDatabaseRegjistrim dbNjesiAdministrative = new clsDatabaseRegjistrim())
            {
                return mbushPikaShitjeFurnizimi(dbNjesiAdministrative.ktheGjithePikatSipasLlojitDheAktiveOseJo(idnderm, lloji, merrDheJoAktive));
            }
        }

        public static DataTable mbushGjithePikaShitjeDtSmall(int idnderm)
        {
            using (clsDatabaseRegjistrim dbNjesiAdministrative = new clsDatabaseRegjistrim())
            {
                return dbNjesiAdministrative.ktheGjithePikaShitjeDtSmall(idnderm);
            }
        }

        public static DataTable mbushGjithePikaFurnizimiDtSmall(int idnderm)
        {
            using (clsDatabaseRegjistrim dbNjesiAdministrative = new clsDatabaseRegjistrim())
            {
              return  dbNjesiAdministrative.ktheGjithePikaFurnizimiDtSmall(idnderm);
            }            
        }

        public static DataRow merrSipasPikeNdermarrjesDR(int idnderm, int iddege)
        {
            clsDatabaseRegjistrim dbartikuj = new clsDatabaseRegjistrim();
            DataRow dr = dbartikuj.merrSipasPikeNdermarrjesDR(idnderm, iddege);
            dbartikuj.Dispose();
            return dr;
        }

        public static DataTable merrSipasPikeNdermarrjesDT(int idnderm)
        {
            clsDatabaseRegjistrim dbartikuj = new clsDatabaseRegjistrim();
            DataTable dt = dbartikuj.merrSipasPikeNdermarrjesDT(idnderm);
            dbartikuj.Dispose();
            return dt;
        }

        public static DataTable merrSipasPikeNdermarrjesDTExport(int idnderm)
        {
            clsDatabaseRegjistrim dbartikuj = new clsDatabaseRegjistrim();
            DataTable dt = dbartikuj.merrSipasPikeNdermarrjesDTExport(idnderm);
            dbartikuj.Dispose();
            return dt;
        }

        public static DataTable merrPikeShitjeFurnizimSipasNdermarrjesLupe(int idnderm)
        {
            clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim();
            DataTable dt = dbRegj.merrPikeShitjeFurnizimSipasNdermarrjesLupe(idnderm);
            dbRegj.Dispose();
            return dt;
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbRegjistrim.clsPikeShitjeFurnizimi"/> 
        /// </summary>
        private bool mbushPikaShitjeFurnizimi(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsPikeShitjeFurnizimi nivel = new clsPikeShitjeFurnizimi();
                    //nivel.mbushPikeShitjeFurnizimi(rreshti);
                    Add(new clsPikeShitjeFurnizimi(rreshti));
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
