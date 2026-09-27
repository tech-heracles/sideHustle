using DbCore.DbAdmin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Licencimi;

namespace RestApi.WebAPI.Models
{
    public class AccessRepository
    {
        /// <summary>
        /// Kompanite e ketij instalimi (vetem id dhe emri, si ne listen e login-it; pa databaza dhe lidhje).
        /// </summary>
        public static object GetAllConnections()
        {
            return LicencatAvec.Merr().Where(l => l.Aktive).OrderBy(l => l.Emri).Select(l => new { l.Id, l.Emri }).ToList();
        }
    }
}
