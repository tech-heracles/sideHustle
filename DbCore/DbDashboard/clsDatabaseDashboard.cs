using AlphaWeb.Core.Interfaces.Data;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Messages;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.DbDashboard
{
    public partial class clsDatabaseDashboard: DbData
    {
        #region Messages
        private string msg_D_saved => MessagesResource.Messages["msg_D_saved"];
        private string msg_D_deleted => MessagesResource.Messages["msg_D_deleted"];
        private string msg_D_removed => MessagesResource.Messages["msg_D_removed"];
        private string msg_D_shared => MessagesResource.Messages["msg_D_shared"];
        #endregion
        public clsDatabaseDashboard() : base()
        {

        }

        public clsDatabaseDashboard(DbData db) : base(db) { }

        internal void GetDashboard(int dashboardId, IDataBaseReader objectToFill)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters("@IDDASHBOARD", dashboardId, ParameterDirection.Input);
            dbManager.FillCollection("prc_T_DASHBOARD_GetDashboard", objectToFill);
        }

        internal void GetDashboardDatasources(int idPerdoruesi, int idNdermarrje, int idViti, IDataBaseReader objectToFill)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters("@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters("@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters("@IDVITI", idViti, ParameterDirection.Input);
            dbManager.FillCollection("prc_T_DASHBOARD_GetDataSources", objectToFill);
        }

        internal void GetDashboardByUser(int dashboardId, int idPerdoruesi, IDataBaseReader objectToFill)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters("@IDDASHBOARD", dashboardId, ParameterDirection.Input);
            dbManager.AddParameters("@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.FillCollection("prc_T_DASHBOARD_GetDashboardByUser", objectToFill);
        }

        internal clsMesazh DeleteDashboard(clsDashboard dashboard)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters("@IDDASHBOARD", dashboard.IdDashboard, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_DASHBOARD_delete");

            return new clsMesazh(true, msg_D_deleted);
        }

        internal void GetDashboardsByUser(int idPerdoruesi, IDataBaseReader objectToFill)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters("@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.FillCollection("prc_T_DASHBOARD_GetDashboardsByUser", objectToFill);
        }


        internal clsMesazh SaveDashboard(clsDashboard dashboard)
        {
            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters("@IDDASHBOARD", dashboard.IdDashboard, ParameterDirection.Output);
            dbManager.AddParameters("@NAME", dashboard.Name, ParameterDirection.Input);
            dbManager.AddParameters("@IDKRIJUESI", dashboard.IdKrijuesi, ParameterDirection.Input);
            dbManager.AddParameters("@REFRESHTIME", dashboard.RefreshTime, ParameterDirection.Input);
            dbManager.AddParameters("@DASHBOARDXML", dashboard.DashboardXml, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_DASHBOARD_insert");
            dashboard.IdDashboard = int.Parse(dbManager.Parameters[0].Value.ToString());

            return new clsMesazh(true, msg_D_saved);
        }

        internal clsMesazh UpdateDashboard(clsDashboard dashboard)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters("@IDDASHBOARD", dashboard.IdDashboard, ParameterDirection.Input);
            dbManager.AddParameters("@NAME", dashboard.Name, ParameterDirection.Input);
            dbManager.AddParameters("@REFRESHTIME", dashboard.RefreshTime, ParameterDirection.Input);
            dbManager.AddParameters("@DASHBOARDXML", dashboard.DashboardXml, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_DASHBOARD_update");

            return new clsMesazh(true, msg_D_saved);
        }

        internal int KtheDashboardOwnerPerLicenceStart(int idPerdoruesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters("@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            var idKrijuesi = dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_DASHBOARD_KtheDashboardOwnerPerLicenceStart");
            if (idKrijuesi == null)
                return 0;
            return Convert.ToInt32(idKrijuesi);
        }

        internal clsMesazh UpdateUserDashboard(clsDashboard dashboard, int idPerdoruesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(6);
            dbManager.AddParameters("@IDDASHBOARD", dashboard.IdDashboard, ParameterDirection.Input);
            dbManager.AddParameters("@NAME", dashboard.Name, ParameterDirection.Input);
            dbManager.AddParameters("@REFRESHTIME", dashboard.RefreshTime, ParameterDirection.Input);
            dbManager.AddParameters("@DASHBOARDXML", dashboard.DashboardXml, ParameterDirection.Input);
            dbManager.AddParameters("@RENDI", dashboard.Rendi, ParameterDirection.Input);
            dbManager.AddParameters("@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_DASHBOARD_UpdateUserDashboard");

            return new clsMesazh(true, msg_D_saved);
        }

        internal clsMesazh DeleteDashboardFromUser(int idDashboard, string idPerdoruesish)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters("@IDDASHBOARD", idDashboard, ParameterDirection.Input);
            dbManager.AddParameters("@IDPERDORUESISH", idPerdoruesish, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_DASHBOARD_X_PERDORUES_delete");

            return new clsMesazh(true, msg_D_removed);
        }
        
        internal clsMesazh ShareDashboardToUser(int idDashboard, string idPerdoruesish)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters("@IDDASHBOARD", idDashboard, ParameterDirection.Input);
            dbManager.AddParameters("@IDPERDORUESISH", idPerdoruesish, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_DASHBOARD_X_PERDORUES_insert");

            return new clsMesazh(true, msg_D_shared);
        }

        internal DataTable GetDashboardUsers(int idDashboard, int idPerdorues)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters("@IDDASHBOARD", idDashboard, ParameterDirection.Input);
            dbManager.AddParameters("@IDPERDORUES", idPerdorues, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_DASHBOARD_X_PERDORUES_GetDashboardUsers");
            return ds.Tables[0];
        }

        internal clsMesazh LidhPerdoruesMeDashboardDefault(int idPerdoruesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters("@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_DASHBOARD_X_PERDORUES_insert_defaults");

            return new clsMesazh(true, msg_D_shared);
        }
    }
}
