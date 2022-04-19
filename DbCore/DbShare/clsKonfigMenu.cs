using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.DbShare
{
    public class clsKonfigMenu
    {
        public static object ruajKonfigMenuMajtas(int idPerdoruesi, int idNdermarrje, string konfigurimi)
        {
            using (clsDatabaseShare dbshare = new clsDatabaseShare())
            {
                return dbshare.ruajKonfigMenuMajtas(idPerdoruesi, idNdermarrje, konfigurimi);
            }
        }
        public static string ktheListKonfigMenu(int idNdermarrje, int idPerdoruesi)
        {
            using (clsDatabaseShare dbshare = new clsDatabaseShare())
            {
                return dbshare.ktheKonfigMenuMajtas(idNdermarrje, idPerdoruesi);
            }
        }
    }
}
