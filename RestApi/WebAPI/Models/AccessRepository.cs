using DbCore.DbAdmin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbCore.IMBUtils.DataBase;

namespace RestApi.WebAPI.Models
{
    public class AccessRepository
    {
        public static DataTable GetAllConnections()
        {
           
            return clsLicenca.MerrLicencatMeDB(MyConnectionsManager.ConnStringNameDefault);
        }
    }
}
