using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Messages;

namespace DbCore.DbAuth
{
    class clsDatabaseAuthentication : DbData
    {

        public clsDatabaseAuthentication() : base()
        {
        }

        public clsDatabaseAuthentication(DbData db) : base(db)
        {
        }
        public clsDatabaseAuthentication(string connectionName) : base(connectionName)
        {
        }

        internal clsMesazh RuajRefreshToken(string  guid, string authenticationTicket )
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddInputParameters("@GUID", guid);
            dbManager.AddInputParameters("@AUTHENTICATION_TICKET", authenticationTicket);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_OAUTH_REFRESH_TOKEN_ins");
            return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        }

        internal string  MerrAuthTicketdb(string guid)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddInputParameters("@GUID", guid);
            return Convert.ToString(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_OAUTH_REFRESH_TOKEN_sel"));
          
        }

    }
}
