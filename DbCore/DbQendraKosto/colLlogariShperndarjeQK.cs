using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbQendraKosto
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsLlogariShperndarjeQK
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colLlogariShperndarjeQK : List<clsLlogariShperndarjeQK>
    {
        #region Konstruktori

        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public colLlogariShperndarjeQK()
        {
        }

        /// <summary>
        /// konstruktori me 1 parametra
        /// merr llogarite qendra kostoje te ndermarjes 
        /// </summary>
        /// <param name="idndermarje">id e ndermarrjes</param>
        public colLlogariShperndarjeQK(int idndermarje)
        {
            clsDatabaseQendraKosto db = new clsDatabaseQendraKosto();
            mbushLlogari(db.ktheLlogariShpernarjeQKSipasNdermarjes(idndermarje));
            db.Dispose();
        }

        public colLlogariShperndarjeQK(int idndermarje, clsDatabaseQendraKosto db)
        {
            mbushLlogari(db.ktheLlogariShpernarjeQKSipasNdermarjes(idndermarje));
        }

        public static bool kaLlogariShpernadrjeQKNdermarrja(int idNdermarrje, clsDatabaseQendraKosto db)
        {
            return db.TransCache.kaLlogariShpernadrjeQKNdermarrja(idNdermarrje, db);
        }

        /// <summary>
        /// konstruktori qe implementon klasen baze
        /// </summary>
        /// <param name="collection">koleksion me clsBurime</param>
        public colLlogariShperndarjeQK(IEnumerable<clsLlogariShperndarjeQK> collection)
            : base(collection)
        {

        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="clsBurime"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsLlogariShperndarjeQK this[int index]
        {
            get
            {
                return ((clsLlogariShperndarjeQK)base[index]);
            }
        }
        
        /// <summary>
        /// mbush te gjitha burimet sipas ndermarrjes 
        /// </summary>
        /// <param name="idnder">id e ndermarrjes</param>
        /// <returns>kthen true kur mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushGjitheLlogariShperndarjeQendraKostojeSipasNdermarjes(int idnder)
        {
            clsDatabaseQendraKosto db = new clsDatabaseQendraKosto();
            bool mbush = mbushLlogari(db.ktheLlogariShpernarjeQKSipasNdermarjes(idnder));
            db.Dispose();
            return mbush;
        }

        public static bool ekzistonLlogariShperndarjeQK(int idNdermarje)
        {
            using (clsDatabaseQendraKosto db = new clsDatabaseQendraKosto())
            {
                return db.ekzistonLlogariShpernarjeQKSipasNdermarjes(idNdermarje);
            }
        }

        /// <summary>
        /// Fshin gjithe llogarite ekzistente per shperndarje te ndermarrjes dhe ruan te rejat.
        /// </summary>
        /// <param name="IdNdermarrje">IdNdermarrje</param>
        /// <returns>Kthen mesazhin e suksesit ose te gabimit</returns>
        public clsMesazh ruaj(int IdNdermarrje)
        {
            clsDatabaseQendraKosto db = new clsDatabaseQendraKosto();
            db.beginTransaksion();
            clsMesazh mesazh = new clsMesazh(true);
            mesazh = clsLlogariShperndarjeQK.fshi(IdNdermarrje, db);
            if (!mesazh.Status)
            {
                db.rollbackTransaksion();
                return mesazh;
            }

            foreach (clsLlogariShperndarjeQK llog in this)
            {
                mesazh = llog.ruaj(db);
                if (!mesazh.Status)
                {
                    db.rollbackTransaksion();
                    return mesazh;
                }
            }
            db.commitTransaksion();
            return mesazh;
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        /// </summary>
        /// <param name="dt">data table me te dhena te tipit llogari shperndarje qendra kosto</param>
        /// <returns>true ose false nqs objekti u mbush ne rregull me te dhena</returns>
        private bool mbushLlogari(DataTable dt)
        {
            //try
            //{
            foreach (DataRow rreshti in dt.Rows)
            {
                //clsLlogariShperndarjeQK llog = new clsLlogariShperndarjeQK();
                //llog.mbushLlogari(rreshti);
                Add(new clsLlogariShperndarjeQK(rreshti));
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
