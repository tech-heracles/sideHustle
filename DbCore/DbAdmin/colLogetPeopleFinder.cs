using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.DbAdmin
{
    public class colLogetPeopleFinder : System.Collections.Generic.List<clsLoguPeopleFinder>
    {
        #region Konstruktoret

        /// <summary>
        /// Konstruktori bosh i koleksionit te log-eve
        /// </summary>
        public colLogetPeopleFinder()
        { 
            
        }

        #endregion

        #region Metoda private

        /// <summary>
        /// Metoda per mbushjen e collectionit nga datatable i kthyer nga DB-ja.
        /// </summary>
        /// <param name="dt">(DataTable) Datatable i kthyer nga DB-ja.</param>
        /// <returns>Kthen True nese mbushja kryhet ne rregull, dhe False ne te kundert.</returns>
        private bool mbushLoget(DataTable dt)
        {
            foreach (DataRow rreshti in dt.Rows)
            {
                this.Add(new clsLoguPeopleFinder(rreshti));
            }
            return true;
        }

        #endregion
    }
}
