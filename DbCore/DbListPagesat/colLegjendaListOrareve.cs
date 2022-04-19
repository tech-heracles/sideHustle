using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DbCore.DbListPagesat
{
    public class colLegjendaListOrareve : System.Collections.Generic.List<clsLegjendaListOrareve>
    {
        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="clsLegjendaListOrareve"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsLegjendaListOrareve this[int index]
        {
            get { return ((clsLegjendaListOrareve)base[index]); }
        }


        /// <summary>
        /// mbush gjithe kodifikimet e artikullit sipas ndermarrjes
        /// </summary>
        /// <param name="idndermarje">id e ndermarrjes</param>
        /// <returns>kthen true nese mbushja kryhet me sukses,ne te kundert false</returns>
        public bool ktheGjitheLegjendaListOrariSipasNdermarrjes(int idndermarje)
        {
            clsDatabazeListPagesa dbKodifikimKF = new clsDatabazeListPagesa();
            bool sukses = mbushLegjendaListOrari(dbKodifikimKF.ktheGjitheLegjendaListOrariSipasNdermarrjes(idndermarje));
            dbKodifikimKF.Dispose();
            return sukses;
        }

        #endregion


        #region Metoda Private

        private bool mbushLegjendaListOrari(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsLegjendaListOrareve grupKF = new clsLegjendaListOrareve();
                    //grupKF.mbushLegjendeListOrari(rreshti);
                    this.Add(new clsLegjendaListOrareve(rreshti));
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

