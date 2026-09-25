using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace DbCore.DbAdmin
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne nje sesion te nje perdoruesi.
    ///  (Te dhenat  merren nga tabela : T_USERTRACK)
    /// </summary>
    public class clsOnlineUsers
    {
        private string Session;
        private int Perdorues;
        private DateTime Login;
        private DateTime Logout;
        private Boolean a;

        //konstruktoret
        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsOnlineUsers(String S, int user, DateTime inUser, DateTime outUser, Boolean aktivUser)
        {
            Session=S;
            Perdorues=user;
            Login=inUser;
            Logout=outUser;
            a=aktivUser;
        }

        /// <summary>
        /// Konstruktori default i klases
        /// </summary>
        public clsOnlineUsers()
        {
        }

        /// <summary>
        /// Kthen ID-ne e sesionit qe gjenerohet automatikisht.
        /// </summary>        
        public String SessionID
        {
            get { return Session; }
            set { Session = value; }
        }

        /// <summary>
        /// Kthen/Vendos Id-ne e perdoruesit.
        /// </summary>
        public int idPerdorues
        {
            get { return Perdorues; }
            set { Perdorues = value; }
        }

        /// <summary>
        /// Kthen/Vendos dateb kur eshte loguar ne sistem.
        /// </summary>
        public DateTime LoginDatetime
        {
            get { return Login; }
            set { Login = value; }
        }

        /// <summary>
        /// Kthen/Vendos daten kur ka bere logout nga sistemi.
        /// </summary>
        public DateTime LogoutDatetime
        {
            get { return Logout; }
            set { Logout = value; }
        }

        /// <summary>
        /// Kthen/Vendos statusin nese nje perdorues eshte akoma ne sistem apo jo.
        /// </summary>
        public Boolean aktiv
        {
            get { return a; }
            set { a = value; }
        }

        //public bool ruaj()

        //public bool modifiko()

        //public bool modifikoAllOffline()


        //public void merr()

        //public int merrNrOnline()
        

    }
}