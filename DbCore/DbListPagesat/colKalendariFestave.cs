using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbListPagesat
{
    public class colKalendariFestave : System.Collections.Generic.List<clsKalendariFestave>
    {
        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="clsKalendariFestave"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsKalendariFestave this[int index]
        {
            get { return ((clsKalendariFestave)base[index]); }
        }


        /// <summary>
        /// mbush gjithe kodifikimet e artikullit sipas ndermarrjes
        /// </summary>
        /// <param name="idndermarje">id e ndermarrjes</param>
        /// <returns>kthen true nese mbushja kryhet me sukses,ne te kundert false</returns>
        public bool mbushGjitheKalendarFestashSipasNdermarrjes(int idndermarje)
        {
            clsDatabazeListPagesa dbKodifikimKF = new clsDatabazeListPagesa();
            bool sukses = mbushKalendarFestash(dbKodifikimKF.ktheGjitheKalendarFestashSipasNdermarrjes(idndermarje));
            dbKodifikimKF.Dispose();
            return sukses;
        }

        #endregion


        #region Metoda Private

        private bool mbushKalendarFestash(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsKalendariFestave grupKF = new clsKalendariFestave();
                    //grupKF.mbushKalendarFestash(rreshti);
                    this.Add(new clsKalendariFestave(rreshti));
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
