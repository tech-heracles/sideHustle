using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace DbCore.DbAdmin
{

    public class colRolPerdorues : List<clsRolPerdorues>
    {
        #region Konstruktoret

        public colRolPerdorues()
        {

        }

        #endregion

        #region Metoda Publike

        public void mbushRolePerdoruesSipasPerdoruesi(int idperdoruesi)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                //data.krijoManager();
                mbushRolePerdoruesi(data.merrRolPerdoruesSipasIdPerdoruesi(idperdoruesi));
            }
        }

        public static int merrRoleSipasPerdoruesiDheKodRoli(int idperdoruesi, String roli)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                return data.merrRolPerdoruesSipasIdPerdoruesiDheKodRol(idperdoruesi, roli);
            }
        }

        public static DataTable merrKodeRoleshSipasPerdoruesit(string usernamePerdoruesi)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            DataTable dt = data.merrKodeRoleshSipasPerdoruesit(usernamePerdoruesi);
            data.Dispose();
            return dt;
        }

        public void mbushRolePerdoruesSipasRoli(int idroli, clsDatabaseAdmin data)
        {

            mbushRolePerdoruesi(data.merrRolPerdoruesSipasIdRoli(idroli));

        }
        public void mbushRolePerdoruesSipasRoli(int idroli)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                mbushRolePerdoruesi(data.merrRolPerdoruesSipasIdRoli(idroli));
            }

        }
        public DataTable ktheRolePerdoruesishSipasIdRoliDT(int idroli)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                return data.ktheRolePerdoruesishSipasIdRoliDT(idroli);
            }
        }

        public new clsRolPerdorues this[int index]
        {
            get { return ((clsRolPerdorues)base[index]); }
        }

        #endregion

        #region Metoda Private

        private bool mbushRolePerdoruesi(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    this.Add(new clsRolPerdorues(rreshti));
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
