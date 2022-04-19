using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;

namespace DbCore.DbAdmin
{
    public class colOnlineUsers : System.Collections.Generic.List<clsOnlineUsers>
    {
        //private ArrayList OnlineUsers = new ArrayList();
        public new clsOnlineUsers this[int index]
        {
            get { return ((clsOnlineUsers)base[index]); }
        }



        public bool shtoOnlineUsers(clsOnlineUsers OnlUser)
        {
            base.Add(OnlUser);
            if (base.Contains(OnlUser))
                return true;
            else return false;
        }
        public bool fshiOnlineUsers(clsOnlineUsers OnlUser)
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
        public void shtoOnlinUsersNeIndeksin(int index, clsOnlineUsers OnlUsr)
        {
            base.Insert(index, OnlUsr);
        }
        public bool ekzistonOnlineUser(clsOnlineUsers onlUsr)
        {
            if (base.Contains(onlUsr))
                return true;
            else return false;
        }
        public int numriOnlineUsers()
        {
            return base.Count;
        }

        public colOnlineUsers mbushArrayListOnlineUsers(DataSet ds)
        {
            colOnlineUsers onlineUsers = new colOnlineUsers();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsOnlineUsers OnlUsr = new clsOnlineUsers();

                OnlUsr.SessionID = rreshti[0].ToString();
                OnlUsr.idPerdorues = int.Parse(rreshti[1].ToString());
                OnlUsr.LoginDatetime = (DateTime)rreshti[2];
                OnlUsr.LogoutDatetime = (DateTime)(rreshti[3]);
                OnlUsr.aktiv = Boolean.Parse(rreshti[4].ToString());
                onlineUsers.Add(OnlUsr);
            }
            return onlineUsers;
        }

    }
}


