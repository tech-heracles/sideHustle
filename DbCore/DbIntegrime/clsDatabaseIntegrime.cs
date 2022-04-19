using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaWeb.Core.Interfaces.Data;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Messages;

namespace DbCore.DbIntegrime
{
    internal class clsDatabaseIntegrime : DbData
    {



        public clsDatabaseIntegrime() : base()
        {
        }



        public clsDatabaseIntegrime(DbData db) : base(db)
        {
        }
        public clsDatabaseIntegrime(string connectionName) : base(connectionName)
        {
        }

        internal void merrJobAutomatike(IDataBaseReader collection)
        {
            dbManager.Open();
            dbManager.CreateParameters(0);
            dbManager.FillCollection("prc_T_JOB_AUTOMATIKE_sel", collection);
        }

        internal void merrJobAutomatikeParametra(int idskeduleri, IDataBaseReader collection)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddInputParameters("@ID_SKEDULERI", idskeduleri);
            dbManager.FillCollection("prc_T_JOB_AUTOMATIKE_PARAMETRA_sel", collection);
        }

        internal clsMesazh modifikoJobAutomatike(int idSkeduleri, int hereTeEkzekutuara, bool aktive, DateTime dataFunditEkzekutuar)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddInputParameters("@ID_SKEDULERI", idSkeduleri);
            dbManager.AddInputParameters("@HERE_TE_EKZEKUTUARA", hereTeEkzekutuara);
            dbManager.AddInputParameters("@AKTIVE", aktive);
            dbManager.AddInputParameters("@DATAFUNDIT_EKZEKUTUAR", dataFunditEkzekutuar);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_JOB_AUTOMATIKE_UPDATE");
            return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        }
    }
}
