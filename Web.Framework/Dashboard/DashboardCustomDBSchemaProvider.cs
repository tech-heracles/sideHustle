using DbCore;
using DbCore.DbDashboard;
using DevExpress.DataAccess.Sql;
using DevExpress.Xpo.DB;
using System.Linq;

namespace PlatinumWeb
{
    public class DashboardCustomDBSchemaProvider : DBSchemaProviderEx
    {
        private int idPerdoruesi;
        private int idNdermarrja;
        private int idViti;

        public DashboardCustomDBSchemaProvider(int idPerdoruesi, int idNdermarrja, int idViti)
        {
            this.idPerdoruesi = idPerdoruesi;
            this.idNdermarrja = idNdermarrja;
            this.idViti = idViti;
        }

        public override void LoadColumns(SqlDataConnection connection, params DBTable[] tables)
        {
            base.LoadColumns(connection, tables);
        }

        public override DBStoredProcedure[] GetProcedures(SqlDataConnection connection, params string[] procedureList)
        {
            string key = $"userDashboardPrcs_{idPerdoruesi}_{idNdermarrja}_{idViti}";
            DBStoredProcedure[] storedProcedures = mySessionObjects.MerrNgaSession<DBStoredProcedure[]>(System.Web.HttpContext.Current.Session, key);

            if (storedProcedures == null)
            {
                colDashboardDatasource dashboardDatasources = new colDashboardDatasource(idPerdoruesi, idNdermarrja, idViti);
                storedProcedures = base.GetProcedures(connection, dashboardDatasources.Select(x => x.SpName).ToArray());
                mySessionObjects.RuajNeSession(System.Web.HttpContext.Current.Session, storedProcedures, key);
            }

            return storedProcedures;
        }

        public override DBTable[] GetTables(SqlDataConnection connection, params string[] tableList)
        {
            DBTable[] tables = new DBTable[0];
            return tables;
        }

        public override DBTable[] GetViews(SqlDataConnection connection, params string[] viewList)
        {
            DBTable[] tables = new DBTable[0];
            return tables;
        }
    }
}
