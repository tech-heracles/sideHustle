using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbCore.IMBUtils.DataBase;

namespace DbCore.WebAutoFear
{
    public class clsDatabaseWebAutoFear : DbData
    {


        /// <summary>
        /// Konstruktori bosh i klases se veprimeve me databazen clsDatabaseWebAutoFear.
        /// </summary>
        public clsDatabaseWebAutoFear()
            : base()
        {
        }
        public clsDatabaseWebAutoFear(DbData db) : base(db) { }

        #region Artikulli

        internal DataTable merrArtikujPerAutoFear(string artikullil)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@KODARTIKULLI", artikullil, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_WEBSERVICE_AUTOFEAR_TEDHENA_ARTIKUJ");
            return ds.Tables[0];
        }

        internal DataTable merrArtikujBarkodePerAutoFear(string artikullil)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@KODARTIKULLI", artikullil, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_WEBSERVICE_AUTOFEAR_TEDHENA_ARTIKUJ_BARKOD");
            return ds.Tables[0];
        }

        internal DataTable merrArtikujCmimePerAutoFear(string artikulli, string furnitori)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODARTIKULLI", artikulli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODFURNITORI", furnitori, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_WEBSERVICE_AUTOFEAR_TEDHENA_ARTIKUJ_CMIM");
            return ds.Tables[0];
        }

        #endregion

    }
}
