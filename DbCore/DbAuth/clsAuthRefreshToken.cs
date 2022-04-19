using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaWeb.Core.Interfaces.Data;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Logging;

namespace DbCore.DbAuth
{
    public class clsAuthRefreshToken : IDataBase
    {

        #region Atribute

        private int idRefreshToken;
        private string guid;
        private string authenticationTicket;
        private DateTime dateKrijimi;
        private DateTime dateModifikimi;
        private int statusi;
        private string serverName;
        #endregion

        #region Properties

        public int IdRefreshToken
        {
            get { return idRefreshToken; }
            set { idRefreshToken = value; }
        }

        public string Guid
        {
            get { return guid; }
            set { guid = value; }
        }

        public string AuthenticationTicket
        {
            get { return authenticationTicket; }
            set { authenticationTicket = value; }
        }

        public DateTime DateKrijimi
        {
            get { return dateKrijimi; }
            set { dateKrijimi = value; }
        }

        public DateTime DateModifikimi
        {
            get { return dateModifikimi; }
            set { dateModifikimi = value; }
        }

        public int Statusi
        {
            get { return statusi; }
            set { statusi = value; }
        }

        #endregion

        #region Konstruktoret
        /// <summary>
        /// Konstruktori i klases pa parametra
        /// </summary>
        public clsAuthRefreshToken(string serverName)
        {
            this.serverName = serverName;
        }
        #endregion

        #region MetodaPublike

        public string MerrAuthTicket()
        {
            using (clsDatabaseAuthentication db = new clsDatabaseAuthentication(serverName))
            {
                return db.MerrAuthTicketdb(guid);
            }
        }

        public clsMesazh Fshi()
        {
            throw new NotImplementedException();
        }

        public void Mbush(IDataRecord record)
        {
            throw new NotImplementedException();
        }

        public clsMesazh Modifiko()
        {
            throw new NotImplementedException();
        }

        public clsMesazh Ruaj()
        {
            try
            {
                using (var myScope = new MyTransactionScope())
                {
                    using (clsDatabaseAuthentication data = new clsDatabaseAuthentication(serverName))
                    {
                        clsMesazh u_ruajt = data.RuajRefreshToken(guid, authenticationTicket);
                        if (u_ruajt) myScope.Complete();
                        return u_ruajt;
                    }
                }
            }
            catch (Exception ex)
            {
                ImbLogger.Error(ex);
                return new MesazhGabimi();
            }

        }
    }

    #endregion  
}
