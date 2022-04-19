using AlphaWeb.Core.Interfaces.Data;
using DbCore.IMBUtils.DataBase;
using System;
using System.Data;

namespace DbCore.MbylljePeriudhe
{
    public class DatabasePeriodClosingCommon : DbData, IDatabasePeriodClosingCommon
    {
        public DatabasePeriodClosingCommon(IDbData dbData) : base(dbData) { }

        public void GetLastClosedMonth(int idNdermarrje, int idNdermarrjeVit, int idModuli, IDataBaseReader objectToFill)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters("@IdNdermarrje", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters("@IdNdermarrjeVit", idNdermarrjeVit, ParameterDirection.Input);
            dbManager.AddParameters("@IdModuli", idModuli, ParameterDirection.Input);
            dbManager.FillObject("T_MP_PeriodSummary_GetLastClosedMonth", objectToFill);
        }

        public void SaveProcess(IProcess process)
        {
            dbManager.Open();
            dbManager.CreateParameters(8);
            dbManager.AddParameters("@IdProcess", process.IdProcess, ParameterDirection.Output);
            dbManager.AddParameters("@IdModuli", process.IdModuli, ParameterDirection.Input);
            dbManager.AddParameters("@IdNdermarrje", process.IdNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters("@IdNdermarrjeVit", process.IdNdermarrjeVit, ParameterDirection.Input);
            dbManager.AddParameters("@IdKrijuesi", process.IdKrijuesi, ParameterDirection.Input);
            dbManager.AddParameters("@DtFillimi", process.DtFillimi, ParameterDirection.Input);
            dbManager.AddParameters("@DtMbarimi", process.DtMbarimi, ParameterDirection.Input);
            dbManager.AddParameters("@Statusi", process.Statusi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "T_MP_Process_insert");
            process.IdProcess = Convert.ToInt32(dbManager.Parameters[0].Value);
        }

        public void UpdateProcess(IProcess process)
        {
            dbManager.Open();
            dbManager.CreateParameters(8);
            dbManager.AddParameters("@IdProcess", process.IdProcess, ParameterDirection.Input);
            dbManager.AddParameters("@IdModuli", process.IdModuli, ParameterDirection.Input);
            dbManager.AddParameters("@IdNdermarrje", process.IdNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters("@IdNdermarrjeVit", process.IdNdermarrjeVit, ParameterDirection.Input);
            dbManager.AddParameters("@IdKrijuesi", process.IdKrijuesi, ParameterDirection.Input);
            dbManager.AddParameters("@DtFillimi", process.DtFillimi, ParameterDirection.Input);
            dbManager.AddParameters("@DtMbarimi", process.DtMbarimi, ParameterDirection.Input);
            dbManager.AddParameters("@Statusi", process.Statusi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "T_MP_Process_update");
        }

        public void SavePeriodSummary(PeriodSummary periodSummary)
        {
            dbManager.Open();
            dbManager.CreateParameters(11);
            dbManager.AddParameters("@IdPeriodSummary", periodSummary.IdPeriodSummary, ParameterDirection.Output);
            dbManager.AddParameters("@IdProcess", periodSummary.IdProcess, ParameterDirection.Input);
            dbManager.AddParameters("@IdNdermarrje", periodSummary.IdNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters("@IdNdermarrjeVit", periodSummary.IdNdermarrjeVit, ParameterDirection.Input);
            dbManager.AddParameters("@IdModuli", periodSummary.IdModuli, ParameterDirection.Input);
            dbManager.AddParameters("@Pershkrimi", periodSummary.Pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters("@Muaji", periodSummary.Muaji, ParameterDirection.Input);
            dbManager.AddParameters("@DtFundMuaji", periodSummary.DtFundMuaji, ParameterDirection.Input);
            dbManager.AddParameters("@DtFillimi", periodSummary.DtFillimi, ParameterDirection.Input);
            dbManager.AddParameters("@DtMbarimi", periodSummary.DtMbarimi, ParameterDirection.Input);
            dbManager.AddParameters("@Statusi", periodSummary.Statusi, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "T_MP_PeriodSummary_insert");
            periodSummary.IdPeriodSummary = Convert.ToInt32(dbManager.Parameters[0].Value);
        }

        public DateTime GetLastClosedDate(int companyId, Modul modul)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters("@IdNdermarrje", companyId, ParameterDirection.Input);
            dbManager.AddParameters("@Moduli", modul.ToString(), ParameterDirection.Input);
            object lastClosedDate = dbManager.ExecuteScalar(CommandType.StoredProcedure, "T_MP_PeriodSummary_GetLastClosedDate");
            DateTime date = new DateTime();
            DateTime.TryParse(lastClosedDate.ToString(), out date);
            return date;
        }

        public void DeletePeriodSummary(PeriodSummary periodSummary)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters("@IdPeriodSummary", periodSummary.IdPeriodSummary, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "T_MP_PeriodSummary_delete");
        }

        public void SavePeriodSummaryToHistorik(PeriodSummary periodSummary, int llojVeprimi)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters("@IdPeriodSummary", periodSummary.IdPeriodSummary, ParameterDirection.Input);
            dbManager.AddParameters("@LlojVeprimi", llojVeprimi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "T_MP_PeriodSummary_Historik_insert");
        }

        public void GetPeriodSummary<T>(int idNdermarrjeVit, int muaji, Action<IDataRecord> funcToFill)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters("@IdNdermarrjeVit", idNdermarrjeVit, ParameterDirection.Input);
            dbManager.AddParameters("@Muaji", muaji, ParameterDirection.Input);
            dbManager.FillObject<T>("T_MP_PeriodSummary_GetPeriodSummary", funcToFill);
        }
        public void GetLastPeriodSummary<T>(int idNdermarrjeVit, Action<IDataRecord> funcToFill)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters("@IdNdermarrjeVit", idNdermarrjeVit, ParameterDirection.Input);
            dbManager.FillObject<T>("T_MP_PeriodSummary_GetLastPeriodSummary", funcToFill);
        }

        public DataTable GetAllPeriodSummaryByIdNdermarrje(int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IdNdermarrje", idNdermarrje, ParameterDirection.Input);

            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "T_MP_PeriodSummary_GetAll").Tables[0];
        }
    }
}
