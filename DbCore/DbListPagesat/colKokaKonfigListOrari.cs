using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbListPagesat
{
    public class colKokaKonfigListOrari : System.Collections.Generic.List<clsKokaKonfigListOrari>
    {
        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="clsKokaKonfigListOrari"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsKokaKonfigListOrari this[int index]
        {
            get { return ((clsKokaKonfigListOrari)base[index]); }
        }


        /// <summary>
        /// mbush gjithe kodifikimet e artikullit sipas ndermarrjes
        /// </summary>
        /// <param name="idndermarje">id e ndermarrjes</param>
        /// <returns>kthen true nese mbushja kryhet me sukses,ne te kundert false</returns>
        public bool mbushGjitheKonfigurimeListOrariSipasNdermarrjes(int idndermarje, int idgjuha)
        {
            clsDatabazeListPagesa dbKodifikimKF = new clsDatabazeListPagesa();
            bool sukses = mbushKonfigurimeListOrari(dbKodifikimKF.ktheGjitheKonfigurimeListOrariSipasNdermarrjes(idndermarje, idgjuha));
            dbKodifikimKF.Dispose();
            return sukses;
        }

        #endregion


        #region Metoda Private

        private bool mbushKonfigurimeListOrari(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsKokaKonfigListOrari grupKF = new clsKokaKonfigListOrari();
                    //grupKF.mbushKonfigurimListOrari(rreshti);
                    this.Add(new clsKokaKonfigListOrari(rreshti));
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
