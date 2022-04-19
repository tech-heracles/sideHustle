using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using AlphaWeb.Core.Interfaces.Data;

namespace DbCore.DbAdmin
{
    partial class clsDatabaseAdmin
    {

        public DataTable MerrFushaModeli(int idmodeli, int idGjuha)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddInputParameters("@IDMODELI", idmodeli);
            dbManager.AddInputParameters("@IDGJUHA", idGjuha);
            var ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_FUSHATSHTESE_merrKolonaFushaShtese");
            return ds.Tables[0];
        }
        internal DataTable MerrVleraFushaShteseSipasModelitDheFushave(int idNdermarrje, int idModeli, string kolonaGride, string filter, int gjuha, int subjekti)
        {
            dbManager.Open();
            dbManager.CreateParameters(6);
            dbManager.AddInputParameters("@IDNDERMARRJE", idNdermarrje);
            dbManager.AddInputParameters("@IDMODELI", idModeli);
            dbManager.AddInputParameters("@FUSHATEGRIDES", kolonaGride);
            dbManager.AddInputParameters("@FILTER", filter);
            dbManager.AddInputParameters("@GJUHAID", gjuha);
            dbManager.AddInputParameters("@SUBJEKTI", subjekti);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_VLERAFUSHASHTESE_merrTeDhenaKerkimi");
            return ds.Tables[0];

        }
        internal void MerrGjitheConnectionStrings(IDataBaseReader colServerConnectionStrings)
        {
            dbManager.Open();
            dbManager.FillCollection("prc_T_SERVER_CONNECTIONSTRINGS_selAll", colServerConnectionStrings);
        }

        internal bool KaAutorizimPerdoruesi(int idPerdoruesi, int idLidhese, string llojBuxheti)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddInputParameters("@IDPERDORUESI", idPerdoruesi);
            dbManager.AddInputParameters("@IDLIDHESE", idLidhese);
            dbManager.AddInputParameters("@LLOJBUXHETI", llojBuxheti);
            return (bool)dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_PERDORUESI_KaAutorizimPerdoruesi");
        }

        internal DataTable GetMonedhaLookupSimpleTable(int idNdermarrje, int idPerdoruesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters("@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters("@IDPERDORUES", idPerdoruesi, ParameterDirection.Input);
            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MONEDHA_GetMonedhaLookupSimpleTable").Tables[0];
        }
    }
}
