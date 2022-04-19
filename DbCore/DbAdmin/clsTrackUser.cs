using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Drawing;
using System.Threading.Tasks;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Logging;

namespace DbCore.DbAdmin
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqesojne historikun e perdoruesve qe jane loguar ne sistem.
    ///  (Te dhenat  merren nga tabela : T_USERTRACK)
    /// </summary>
    /// 
    public class clsTrackUser
    {

        #region Atributet

        private string Session; //runa id e sessionit te krijuar nga perdoruesi kur eshte loguar, ka lidhje me browserin
        private int PerdoruesId; //id e perdoruesit te loguar
        private string Login; //ora dhe data kur eshte loguar perdoruesi, e konvertuar ne string
        private string Logout; //ora dhe data kur perdoruesi eshte kaluar ne logout, e konvertuar ne string
        private Boolean a; //eshte aktiv apo jo perdoruesi
        private string emri; //emri i perdoruesit te loguar
        private string username; //username i perdoruesit te loguar
        private string ip; //ip e kompjuterit nga eshte loguar perdoruesi
        private string emriUserLogin; //username me te cilin po tentohet te logohet ne sistem
        private string arsyeFailLogin;//pershkrimi qe tregon arsyen qe perdoruesi nuk arriti te logohej ne sistem
        private string[] connectionNames;
        #endregion

        #region Konstruktoret

        public clsTrackUser(IEnumerable<string> connectionNames)
        {
            this.connectionNames = connectionNames.ToArray();
        }
        /// <summary>
        /// mbush objektin e clsTrackUser ne baze te id-se se perdoruesit qe eshte aktiv
        /// </summary>
        /// <param name="idPerdoruesi"></param>
        public clsTrackUser(int idPerdoruesi, string connName) : this(connName)
        {

            using (clsDatabaseAdmin db = new clsDatabaseAdmin())
            {
                mbushUserTrack(db.ktheUserTrack(idPerdoruesi));
            }
        }

        public clsTrackUser(DataRow rreshti)
        {
            
            mbushTrackUser(rreshti);
        }

        public clsTrackUser(string connectionName)
        {
            connectionNames = new[] { connectionName };
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne e sessionit te perdoruesit
        /// </summary>
        public String SessionID
        {
            get { return Session; }
            set { Session = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne perdoruesit
        /// </summary>
        public int idPerdorues
        {
            get { return PerdoruesId; }
            set { PerdoruesId = value; }
        }

        /// <summary>
        /// Kthen/Vendos emrin e perdoruesit
        /// </summary>
        public string EmriPerdorues
        {
            get { return emri; }
            set { emri = value; }
        }

        /// <summary>
        /// Kthen/Vendos username te perdoruesit
        /// </summary>
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        /// <summary>
        /// Kthen/Vendos daten dhe oren e logimit ne sistem
        /// </summary>
        public string LoginDatetime
        {
            get { return Login; }
            set { Login = value; }
        }

        /// <summary>
        /// Kthen/Vendos daten dhe oren e daljes nga sistemi
        /// </summary>
        public string LogoutDatetime
        {
            get { return Logout; }
            set { Logout = value; }
        }

        /// <summary>
        /// Kthen/Vendos vleren nqs eshte aktiv apo jo perdoruesi
        /// aktiv = login
        /// joaktiv = logout
        /// </summary>
        public Boolean aktiv
        {
            get { return a; }
            set { a = value; }
        }

        /// <summary>
        /// Kthen/Vendos ip-ne e kompjuterit ne rrjet nga i cili perdoruesi eshte loguar
        /// </summary>
        public string IpAdress
        {
            get { return ip; }
            set { ip = value; }
        }

        /// <summary>
        /// Kthen/Vendos emrin me te cilin po tentohet te logohet ne sistem
        /// </summary>
        public string EmriUserLogin
        {
            get { return emriUserLogin; }
            set { emriUserLogin = value; }
        }

        /// <summary>
        /// Kthen/Vendos arsyen qe nuk u login-i nuk qe i suksesshem
        /// </summary>
        public string ArsyeFailLogin
        {
            get { return arsyeFailLogin; }
            set { arsyeFailLogin = value; }
        }



        #endregion

        #region Metoda Publike

        /// <summary>
        /// Ruan objektin e veprimit ne tabelen T_USERTRACK ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbAdmin.clsDatabaseAdmin.ruajUserTrack"/> 
        /// </summary>
        public bool ruaj()
        {

            foreach (var connName in connectionNames)
            {
                using (var data = new clsDatabaseAdmin(connName))
                    return data.ruajUserTrack(this.SessionID, this.idPerdorues, this.LoginDatetime, this.IpAdress, this.a, this.EmriUserLogin, this.ArsyeFailLogin);
            }
            return false;

        }

        /// <summary>
        /// Modifikon objektin e veprimit ne tabelen T_USERTRACK ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbAdmin.clsDatabaseAdmin.modifikoOnlineUser"/> 
        /// </summary>
        public void modifiko()
        {

            using (var data = new clsDatabaseAdmin(connectionNames[0]))
                data.modifikoOnlineUser(this.SessionID);

        }

        /// <summary>
        /// Modifikon objektin e veprimit ne tabelen T_USERTRACK ne databaze. 
        /// Therret funksionin:  <see cref="DbCore.DbAdmin.clsDatabaseAdmin.modifikoOnlineUserAllOffline"/> 
        /// </summary>
        public void modifikoAllOffline(DateTime logutDateTime)
        {
            Parallel.ForEach(connectionNames, connName =>
            {
                try
                {
                    using (var data = new clsDatabaseAdmin(connName))
                        data.modifikoOnlineUserAllOffline(logutDateTime.ToString());
                }
                catch (Exception ex)
                {
                    //mund te ndodhi qe ndonje nga db eshte e pa arritshme,
                    //aplikacioni nuk duhet te fail pavarsisht se ndonje db eshte e paarritshme
                    ImbLogger.Error(ex);
                }
            });
        }

        /// <summary>
        /// Kthen nje bashkesi te objekteve te tipit <see cref="DbCore.DbAdmin.colTrackUser"/>, te cilin e merr nga databaza 
        /// duke kthyer te gjithe rreshtat me status aktiv.
        /// </summary>
        public Dictionary<string, colTrackUser> merr(int idLicenca, int idperdorues)
        {
            var dictionary = new Dictionary<string, colTrackUser>(connectionNames.Length);
            foreach (var connName in connectionNames)
            {
                using (var data = new clsDatabaseAdmin(connName))
                {
                    colTrackUser dataUsers = new colTrackUser();
                    dataUsers.mbushAllTrackUser(idLicenca, idperdorues);
                    dictionary[connName] = dataUsers;
                }
            }
            return dictionary;
        }

        /// <summary>
        /// Kthen numrin e rreshtave ne DB ne tabelen T_USERTRACK me status aktiv = true. 
        /// <see cref="DbCore.DbAdmin.merrOnlineUserCount"/>
        /// </summary>
        public Dictionary<string, int> merrNrOnline()
        {
            var dictionary = new Dictionary<string, int>(connectionNames.Length);
            foreach (var connName in connectionNames)
            {
                using (var data = new clsDatabaseAdmin(connName))
                {
                    int nr = data.merrOnlineUserCount();
                    dictionary[connName] = nr;
                }
            }
            return dictionary;
        }


        /// <summary>
        /// ruan te dhenat e perdoruesit qe nuk logohet me sukses
        /// </summary>
        /// <param name="arsyeLoginFail"></param>
        /// <param name="username">username me te cilin eshte tentuar te logohet</param>
        /// <param name="sessionID"></param>
        /// <returns></returns>
        public static bool shtoUserLoginFail(string arsyeLoginFail, string username, string sessionID, string ipAddress)
        {
            clsTrackUser newTrack = new clsTrackUser(MyConnectionsManager.GetSelectedConNameServer(sessionID));
            newTrack.SessionID = sessionID;
            newTrack.aktiv = false;
            newTrack.LoginDatetime = DateTime.Now.ToString();
            newTrack.IpAdress = ipAddress;
            newTrack.ArsyeFailLogin = arsyeLoginFail;
            newTrack.EmriUserLogin = username;
            return newTrack.ruaj();
        }

        #endregion

        #region Metoda Internal

        internal bool mbushTrackUser(DataRow dbDataRowTrackUser)
        {
            if (dbDataRowTrackUser != null)
            {
                try
                {
                    Session = dbDataRowTrackUser["SESSION_ID"].ToString();
                    idPerdorues = Convert.ToInt32(dbDataRowTrackUser["IDPERDORUES"]);
                    Login = dbDataRowTrackUser["LOGINTIME"].ToString();
                    Logout = dbDataRowTrackUser["LOGOUTTIME"].ToString();
                    aktiv = Boolean.Parse(dbDataRowTrackUser["AKTIV"].ToString());
                    emri = dbDataRowTrackUser["PERDORUESEMRI"].ToString() + " " + dbDataRowTrackUser["PERDORUESMBIEMRI"].ToString();
                    username = dbDataRowTrackUser["PERDORUESUSERNAME"] != null ? dbDataRowTrackUser["PERDORUESUSERNAME"].ToString() : null;
                    ip = dbDataRowTrackUser["IP"].ToString();
                    emriUserLogin = dbDataRowTrackUser["Username"] != null ? dbDataRowTrackUser["Username"].ToString() : null;
                    arsyeFailLogin = dbDataRowTrackUser["ArsyeLoginFail"] != null ? dbDataRowTrackUser["ArsyeLoginFail"].ToString() : null;
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se track-ut te perdoruesit nga db-ja");
                }
            }
            else
                return false;
        }

        internal bool mbushUserTrack(DataRow dbDataRowTrackUser)
        {
            if (dbDataRowTrackUser != null)
            {
                try
                {
                    Session = dbDataRowTrackUser["SESSION_ID"].ToString();
                    idPerdorues = Convert.ToInt32(dbDataRowTrackUser["IDPERDORUES"]);
                    Login = dbDataRowTrackUser["LOGINTIME"].ToString();
                    Logout = dbDataRowTrackUser["LOGOUTTIME"].ToString();
                    aktiv = Boolean.Parse(dbDataRowTrackUser["AKTIV"].ToString());
                    ip = dbDataRowTrackUser["IP"].ToString();
                    emriUserLogin = dbDataRowTrackUser["Username"] != null ? dbDataRowTrackUser["Username"].ToString() : null;
                    arsyeFailLogin = dbDataRowTrackUser["ArsyeLoginFail"] != null ? dbDataRowTrackUser["ArsyeLoginFail"].ToString() : null;
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se track-ut te perdoruesit nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}