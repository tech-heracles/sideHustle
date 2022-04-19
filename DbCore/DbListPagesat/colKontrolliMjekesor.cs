using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DbCore.DbListPagesat
{
 public    class colKontrolliMjekesor : System.Collections.Generic.List<clsKontrolliMjekesor>
    {
        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="clsKontrolliMjekesor"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsKontrolliMjekesor this[int index]
        {
            get { return ((clsKontrolliMjekesor)base[index]); }
        }


        /// <summary>
        /// mbush gjithe kodifikimet e artikullit sipas ndermarrjes
        /// </summary>
        /// <param name="idndermarje">id e ndermarrjes</param>
        /// <returns>kthen true nese mbushja kryhet me sukses,ne te kundert false</returns>
        public bool ktheGjitheKontrolliMjekesorSipasNdermarrjes(int idndermarje)
        {
            clsDatabazeListPagesa dbKodifikimKF = new clsDatabazeListPagesa();
            bool sukses = mbushKontrolliMjekesor(dbKodifikimKF.ktheGjitheKontrolliMjekesorSipasNdermarrjes(idndermarje));
            dbKodifikimKF.Dispose();
            return sukses;
        }
        public static DataTable ktheGjitheKontrolliMjekesorExport(int idndermarje)
        {
            using (clsDatabazeListPagesa dbKodifikimKF = new clsDatabazeListPagesa())
            {
                return dbKodifikimKF.merrKontrolliMjekesorPerExport(idndermarje);
            }

        }
     
        public bool ktheGjitheKontrolliMjekesorSipasPunonjesit(int idpunonjesi)
        {
            clsDatabazeListPagesa dbKodifikimKF = new clsDatabazeListPagesa();
            bool sukses = mbushKontrolliMjekesor(dbKodifikimKF.ktheGjitheKontrolliMjekesorSipasPunonjesit(idpunonjesi));
            dbKodifikimKF.Dispose();
            return sukses;
        }
     
        #endregion


        #region Metoda Private

        private bool mbushKontrolliMjekesor(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsKontrolliMjekesor grupKF = new clsKontrolliMjekesor();
                    //grupKF.mbushKontrolliMjekesor(rreshti);
                    this.Add(new clsKontrolliMjekesor(rreshti));
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

