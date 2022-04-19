using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using DbCore.DbAdmin;

namespace DbCore.DbInventari
{
    public class colPajisjet : System.Collections.Generic.List<clsPajisje>
    {
        #region Metoda Publike

        public colPajisjet()
        {

        }

        public colPajisjet(int idNdermarje, bool aktiv)
        {
            DataTable dt = (aktiv) ? colPajisjet.merrPajisjetSipasNdermarrjesAktive(idNdermarje) : colPajisjet.merrPajisjetSipasNdermarrjes(idNdermarje);
            mbushPajisje(dt);
        }
        public static DataTable merrPajisjetSipasNdermarrjes(int idNdermarrje)
        {
            clsDatabaseInventari dbInventar = new clsDatabaseInventari();
            DataTable table = dbInventar.merrPajisjetSipasNdermarrjes(idNdermarrje);
            dbInventar.Dispose();
            return table;
        }

        public static DataTable merrPajisjetSipasNdermarrjesAktive(int idNdermarrje)
        {
            clsDatabaseInventari dbInventar = new clsDatabaseInventari();
            DataTable table = dbInventar.merrPajisjetSipasNdermarrjesAktive(idNdermarrje);
            dbInventar.Dispose();
            return table;
        }

        public static DataTable merrLlojeKonvertimeshPerPajisje()
        {
            using (clsDatabaseInventari dbInventar = new clsDatabaseInventari())
            {
                return dbInventar.merrLlojeKonvertimeXPajisje();
            }
        }


        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbKontabiliteti.clsPajisje"/> 
        /// </summary>
        private bool mbushPajisje(DataTable dt)
        {
            foreach (DataRow rreshti in dt.Rows)
            {
                Add(new clsPajisje(rreshti));
            }
            return true;
        }
        public bool mbushGjithePajisjet()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool sukses = mbushPajisje(data.ktheGjithePajisjet());
            data.Dispose();
            return sukses;
        }
        #endregion
    }
}