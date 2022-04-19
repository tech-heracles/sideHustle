using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;

namespace DbCore.DbRegjistrim
{
    public class colKokaRezervime:System.Collections.Generic.List<clsKokaRezervime>
    {

        
        #region Konstruktor

        /// <summary>
        /// 
        /// </summary>
        public colKokaRezervime() 
        { 
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idNdermVit">id qe lidh ndermarrjen me vitin</param>
        public colKokaRezervime(int idNdermVit)
        {
            clsDatabaseRegjistrim db = new clsDatabaseRegjistrim();
            mbushKokatRezervime(db.ktheGjitheKokaRezervime(idNdermVit));
            db.Dispose();
        }

        public static DataTable merrKokaRezervimiDT(int idndermvit, int idperdoruesi)
        {
            clsDatabaseRegjistrim dbartikuj = new clsDatabaseRegjistrim();
            DataTable dt = dbartikuj.merrKokaRezervimeDT(idndermvit, idperdoruesi);
            dbartikuj.Dispose();
            return dt;
        }

        /// <summary>
        /// mbush koken e rezervimit sipas id gjenerues
        /// </summary>
        /// <param name="idKonfigGjen"></param>
        /// <param name="idGjenerues">id e gjeneruesit</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        public bool mbushKokaRezervimeSipasIDGjenerues(int idKonfigGjen, int idGjenerues)
        {
            clsDatabaseRegjistrim db = new clsDatabaseRegjistrim();
            bool mbush = mbushKokaRezervimeSipasIDGjenerues(idKonfigGjen, idGjenerues, db);
            db.Dispose();
            return mbush;
        }

        /// <summary>
        /// mbush koken e rezervimit sipas id gjenerues
        /// </summary>
        /// <param name="idKonfigGjen"></param>
        /// <param name="idGjenerues">id e gjeneruesit</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        public bool mbushKokaRezervimeSipasIDGjenerues(int idKonfigGjen, int idGjenerues, clsDatabaseRegjistrim db)
        {
            return mbushKokatRezervime(db.ktheKokaRezervimiSipasIDGjenerues(idKonfigGjen, idGjenerues));
        }
        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbRegjistrim.clsKokaRezervime"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary> 
        public new clsKokaRezervime this[int index]
        {
            get { return ((clsKokaRezervime)base[index]); }
        }

        #endregion 

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbRegjistrim.clsKokaRezervime"/> 
        /// </summary>
        private bool mbushKokatRezervime(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsKokaRezervime koka = new clsKokaRezervime();
                    //koka.mbushKokaRezervime(rreshti);
                    Add(new clsKokaRezervime(rreshti));
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
