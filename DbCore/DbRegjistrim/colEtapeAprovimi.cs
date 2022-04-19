using System;
using System.Collections.Generic;
using System.Data;
namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsEtapeAprovimi
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colEtapeAprovimi: List<clsEtapeAprovimi>
    {
        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbRegjistrim.clsKokaShitje"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary> 
        public new clsEtapeAprovimi this[int index]
        {
            get { return ((clsEtapeAprovimi)base[index]); }
        }
        public static DataTable merrEtapaSipasPerdoruesitDheStatusit(int idperdorues,int idndermarje, int status, int idgjuha)
        {
            clsDatabaseRegjistrim db = new clsDatabaseRegjistrim();
            DataTable dt = db.ktheEtapaSipasPerdoruesitDheStatusit(idndermarje, idperdorues,status, idgjuha);
            db.Dispose();
            return dt;
        }  
        public static DataTable merrEtapaSipasPerdoruesit(int idperdorues, int idndermarje, int idgjuha)
        {
            clsDatabaseRegjistrim db = new clsDatabaseRegjistrim();
            DataTable dt = db.ktheEtapaSipasPerdoruesit(idperdorues, idndermarje, idgjuha);
            db.Dispose();
            return dt;
        }
        public static bool eshtePerdoruesiNeEtapeAprovimiPerKokaShitje(int idperdorues, int kokashitje, int idkategoria)
        {
            using (clsDatabaseRegjistrim db = new clsDatabaseRegjistrim())
            {
               return db.eshtePerdoruesiNeEtapeAprovimiPerKokaShitje(idperdorues, kokashitje, idkategoria);
            }

        }

        public static bool NjoftimMeEmailSipasSkemesSePunes(int idSkema)
        {
            using (clsDatabaseRegjistrim db = new clsDatabaseRegjistrim())
            {

                return db.NjoftimMeEmailSipasSkemesSePunes(idSkema);
            }

        }


        public void mbushEtapaSipasNrProcesiDheNiveliPerAprovim(int nrprocesi, int idndermarje, int niveli, int idkategoria)
        {
            using (clsDatabaseRegjistrim db = new clsDatabaseRegjistrim())
            {
                mbushEtapat(db.ktheEtapaSipasNrProcesiDheNiveliPerAprovim(nrprocesi, niveli, idndermarje,idkategoria));
            }
        }
        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        /// </summary>
        internal bool mbushEtapat(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsEtapeAprovimi koka = new clsEtapeAprovimi();
                    //koka.mbushEtape(rreshti);
                    Add(new clsEtapeAprovimi(rreshti));
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
