using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbAdmin
{
    public class colRoli: List<clsRoli>
    {
        #region Konstruktoret

        public colRoli()
        { 
            
        }

        #endregion

        #region Metoda Publike
        
        public bool mbushRoletSipasIdPerdoruesi(int idperdorues)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                return mbushRolet(data.merrGjitheRoletSipasIdPerdoruesi(idperdorues));
            }
        }

        public bool mbushRoletLike(int idlicenca, string text)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                return mbushRolet(data.merrGjitheRoletLike(idlicenca, text));
            }
        }

        public new clsRoli this[int index]
        {
            get { return ((clsRoli)base[index]); }
        }
              
        public static DataTable merrRoletDT(int idlicence, int idperdorues)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                return data.merrRoletDT(idlicence, idperdorues);
            }
        }

        public static DataTable merrRoletDTJoSuper(int idlicence, int idperdorues)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                return data.merrRoletDTJoSuper(idlicence, idperdorues);
            }
        }

        public static DataTable merrRoletDTSipasLidhes(int idlicence, int idperdorues, int idperdoruesroli)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                return data.merrRoletDTSipasLidhjes(idlicence, idperdorues, idperdoruesroli);
            }
        }

        #endregion

        #region metoda private

        private bool mbushRolet(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {

                    //clsRoli roli = new clsRoli();
                    //roli.mbushRolin(rreshti);
                    this.Add(new clsRoli(rreshti));
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
