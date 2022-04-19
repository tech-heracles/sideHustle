using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbQendraKosto
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsObjektivaKosto
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    /// <seealso cref="clsObjektivaKosto"/>
    public class colObjektivaKosto: List<clsObjektivaKosto>
    {
        #region Konstruktori

        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public colObjektivaKosto()
        {
        }

        /// <summary>
        /// konstruktori me 1 parametra
        /// </summary>
        /// <param name="idndermarje">id e ndermarrjes</param>
        public colObjektivaKosto( int idndermarje)
        {
            clsDatabaseQendraKosto db = new clsDatabaseQendraKosto();
            mbushObjektivaKosto(db.ktheGjitheObjektivaKostoSipasNdermarjes(idndermarje));
            db.Dispose();
        }

        /// <summary>
        ///  konstruktori qe implementon klasen baze
        /// </summary>
        /// <param name="collection">koleksion me clsObjektivaKosto </param>
        public colObjektivaKosto(IEnumerable<clsObjektivaKosto> collection)
            : base(collection)
        {
            
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="clsObjektivaKosto"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsObjektivaKosto this[int index]
        {
            get
            {
                return ((clsObjektivaKosto)base[index]);
            }
        }

        /// <summary>
        /// merr objektiven sipas id ne forme data row
        /// </summary>
        /// <param name="id"> id objektiva</param>
        /// <returns> kthen data row me kete strukture</returns>
        public static DataRow merrObjektivaKostoDR(int id)
        {
            clsDatabaseQendraKosto db = new clsDatabaseQendraKosto();
            DataRow dr = db.merrObjektivKostoDR(id);
            db.Dispose();
            return dr;
        }

        /// <summary>
        /// merr objektivat sipas ndermarje ne forme data table 
        /// </summary>
        /// <param name="idnderm"> idndermarje</param>
        /// <returns> kthen data table me keto objektiva</returns>
        public static DataTable merrObjektivaKostoDT(int idnderm)
        {
            clsDatabaseQendraKosto db = new clsDatabaseQendraKosto();
            DataTable dt = db.merrObjektivKostoDT(idnderm);
            db.Dispose();
            return dt;
        }

        /// <summary>
        /// mbush te gjitha objektivat sipas ndermarrjes
        /// </summary>
        /// <param name="idnder">id e ndermarrjes</param>
        /// <returns>kthen true kur mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushGjitheObjektivatKostoSipasNdermarjes(int idnder)
        {
            clsDatabaseQendraKosto db = new clsDatabaseQendraKosto();
            bool mbush = mbushObjektivaKosto(db.ktheGjitheObjektivaKostoSipasNdermarjes(idnder));
            db.Dispose();
            return mbush;
        } 
        /// <summary>
        /// mbush te gjitha objektivat sipas ndermarrjes aktiv
        /// </summary>
        /// <param name="idnder">id e ndermarrjes</param>
        /// <returns>kthen true kur mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushGjitheObjketivatKostoSipasNdermarjesAktiv(int idnder)
        {
            clsDatabaseQendraKosto db = new clsDatabaseQendraKosto();
            bool mbush = mbushObjektivaKosto(db.ktheGjitheObjektivaKostoSipasNdermarjesAktiv(idnder));
            db.Dispose();
            return mbush;
        }


        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        /// </summary>
        /// <param name="dt"> data table me te dhenat e tipit objektiva kosto</param>
        /// <returns>true ose false nqs objekti u mbush ne rregull me te dhena</returns>
        private bool mbushObjektivaKosto(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsObjektivaKosto objektiva = new clsObjektivaKosto();
                    //objektiva.mbushObjektivKosto(rreshti);
                    Add(new clsObjektivaKosto(rreshti));
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
