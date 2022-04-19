using AlphaWeb.Core.Interfaces.Data;
using System;
using System.Data;

namespace DbCore.DbDashboard
{
    public class clsDashboard : IDataBase
    {
        public int IdDashboard { get; set; }
        public string Name { get; set; }
        public int IdKrijuesi { get; set; }
        public string Krijuesi { get; set; }
        public int RefreshTime { get; set; }
        public string DashboardXml { get; set; }
        public int Rendi { get; set; }

        public clsDashboard(int dashboardId)
        {
            using (var db = new clsDatabaseDashboard())
                db.GetDashboard(dashboardId, this);
        }

        public clsDashboard(int dashboardId, int idPerdoruesi)
        {
            using (var db = new clsDatabaseDashboard())
                db.GetDashboardByUser(dashboardId, idPerdoruesi, this);
        }

        public clsDashboard()
        {
        }

        public clsDashboard(IDataRecord record) => Mbush(record);


        public clsMesazh Ruaj()
        {
            try
            {
                using (var db = new clsDatabaseDashboard())
                    return db.SaveDashboard(this);
            }
            catch (Exception ex)
            {
                return new MesazhGabimi(ex.Message);
            }
        }

        public clsMesazh Modifiko()
        {
            try
            {
                using (var db = new clsDatabaseDashboard())
                    return db.UpdateDashboard(this);
            }
            catch (Exception ex)
            {
                return new MesazhGabimi(ex.Message);
            }
        }

        public clsMesazh Fshi()
        {
            try
            {
                using (var db = new clsDatabaseDashboard())
                    return db.DeleteDashboard(this);
            }
            catch (Exception ex)
            {
                return new MesazhGabimi(ex.Message);
            }
        }

        public static clsMesazh ShtoPerdoruesNeDashboard(int idDashboard, string idPerdoruesish)
        {
            try
            {
                using (var db = new clsDatabaseDashboard())
                    return db.ShareDashboardToUser(idDashboard, idPerdoruesish);
            }
            catch (Exception ex)
            {
                return new MesazhGabimi(ex.Message);
            }
        }

        public static clsMesazh FshiPerdoruesNgaDashboardi(int idDashboard, string idPerdoruesish)
        {
            try
            {
                using (var db = new clsDatabaseDashboard())
                    return db.DeleteDashboardFromUser(idDashboard, idPerdoruesish);
            }
            catch (Exception ex)
            {
                return new MesazhGabimi(ex.Message);
            }
        }

        public static DataTable GetDashboardUsers(int idDashboard, int idPerdorues)
        {
            using (var db = new clsDatabaseDashboard())
                return db.GetDashboardUsers(idDashboard, idPerdorues);
        }

        public clsMesazh UpdateUserDashboard(int idPerdoruesi)
        {
            using (var db = new clsDatabaseDashboard())
                return db.UpdateUserDashboard(this, idPerdoruesi);
        }

        public static clsMesazh LidhPerdoruesMeDashboardDefault(clsDatabaseDashboard db, int idPerdoruesi)
        {
            return db.LidhPerdoruesMeDashboardDefault(idPerdoruesi);
        }

        public static int KtheDashboardOwnerPerLicenceStart(int idPerdoruesi)
        {
            using (var db = new clsDatabaseDashboard())
                return db.KtheDashboardOwnerPerLicenceStart(idPerdoruesi);
        }

        public void Mbush(IDataRecord record)
        {
            IdDashboard = !Convert.IsDBNull(record["IDDASHBOARD"]) ? Convert.ToInt32(record["IDDASHBOARD"]) : 0;
            Name = !Convert.IsDBNull(record["NAME"]) ? Convert.ToString(record["NAME"]) : String.Empty;
            IdKrijuesi = !Convert.IsDBNull(record["IDKRIJUESI"]) ? Convert.ToInt32(record["IDKRIJUESI"]) : 0;
            Krijuesi = !Convert.IsDBNull(record["KRIJUESI"]) ? Convert.ToString(record["KRIJUESI"]) : String.Empty;
            RefreshTime = !Convert.IsDBNull(record["REFRESHTIME"]) ? Convert.ToInt32(record["REFRESHTIME"]) : 0;
            DashboardXml = !Convert.IsDBNull(record["DASHBOARDXML"]) ? Convert.ToString(record["DASHBOARDXML"]) : String.Empty;
            Rendi = !Convert.IsDBNull(record["RENDI"]) ? Convert.ToInt32(record["RENDI"]) : 0;
        }

    }
}
