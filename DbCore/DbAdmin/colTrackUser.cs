using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;


namespace DbCore.DbAdmin
{
    public class colTrackUser : System.Collections.Generic.List<clsTrackUser>
    {
        #region Metoda Publike

        public new clsTrackUser this[int index]
        {
            get { return ((clsTrackUser)base[index]); }
        }

        public bool shtoOnlineUsers(clsTrackUser OnlUser)
        {
            base.Add(OnlUser);
            if (base.Contains(OnlUser))
                return true;
            else return false;
        }

        public bool fshiOnlineUsers(clsTrackUser OnlUser)
        {
            base.Remove(OnlUser);
            if (base.Contains(OnlUser))
                return false;
            else return true;
        }

        public bool fshiGjitheOnlineUsers()
        {
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
        }

        public void fshiKeteOnlineUser(int index)
        {
            base.RemoveAt(index);
        }

        public void shtoOnlinUsersNeIndeksin(int index, clsTrackUser OnlUsr)
        {
            base.Insert(index, OnlUsr);
        }

        public bool ekzistonOnlineUser(clsTrackUser onlUsr)
        {
            if (base.Contains(onlUsr))
                return true;
            else return false;
        }

        public int numriOnlineUsers()
        {
            return base.Count;
        }

        public bool mbushAllTrackUser(int idLicenca, int idperdorues)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool sukses = mbushTrackUserat(data.ktheAllTrackUser(idLicenca, idperdorues));
            data.Dispose();
            return sukses;
        }

        #endregion

        #region Metoda Private

        private bool mbushTrackUserat(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsTrackUser OnlUsr = new clsTrackUser();
                    //OnlUsr.mbushTrackUser(rreshti);
                    Add(new clsTrackUser(rreshti));
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


