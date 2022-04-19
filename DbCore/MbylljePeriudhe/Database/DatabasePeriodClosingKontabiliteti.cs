using DbCore.IMBUtils.DataBase;
using System;
using System.Collections.Generic;
using System.Data;

namespace DbCore.MbylljePeriudhe
{
    public class DatabasePeriodClosingKontabiliteti : DbData, IDatabasePeriodClosing
    {
        public DatabasePeriodClosingKontabiliteti (IDbData dbData): base(dbData) { }

        public void SavePeriodSummaryDetail(DataTable details)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@PeriodSummaryDetails", details, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "T_MP_PeriodSummaryDetailKontabiliteti_insert");
        }

        public void DeletePeriodSummaryDetail(int idPeriodSummary)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters("@IdPeriodSummary", idPeriodSummary, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "T_MP_PeriodSummaryDetailKontabiliteti_delete");
        }

        public IEnumerable<T> GetAnaliticPeriodSummaryDetails<T>(int idNdermarrje, DateTime dtFundMuaji, Func<IDataRecord, T> funcToFill)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters("@IdNdermarrje", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters("@DtFundMuaji", dtFundMuaji, ParameterDirection.Input);
            return dbManager.GetIEnumerbale<T>("T_MP_PeriodSummaryDetailKontabiliteti_GetAnaliticPeriodSummaryDetails", funcToFill);
        }

        public IEnumerable<T> GetLastPeriodSummaryDetails<T>(int idNdermarrje, Func<IDataRecord, T> funcToFill)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters("@IdNdermarrje", idNdermarrje, ParameterDirection.Input);
            return dbManager.GetIEnumerbale<T>("T_MP_PeriodSummaryDetailKontabiliteti_GetLastPeriodSummaryDetails", funcToFill);
        }

        public DataTable GetPeriodSummaryDetailsReport(int idNdermarrje, int viti, int muaji)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters("@IdNdermarrje", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters("@Viti", viti, ParameterDirection.Input);
            dbManager.AddParameters("@Muaji", muaji, ParameterDirection.Input);
            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_Rap_MP_PeriodSummaryDetailKontabiliteti_Select").Tables[0];
        }

        public void DeleteHistoric(int idNdermarrje, int idNdermarrjeVit, string muajt)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters("@IdNdermarrje", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters("@IdNdermarrjeVit", idNdermarrjeVit, ParameterDirection.Input);
            dbManager.AddParameters("@Muajt", muajt, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_MP_Kontabiliteti_DeleteHistoric");
        }
    }
}
