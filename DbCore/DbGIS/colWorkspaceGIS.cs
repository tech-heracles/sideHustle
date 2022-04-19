using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;

using DbCore.DbGIS;

namespace DbCore.DbGIS
{
    public class colWorkspaceGIS : List<clsWorkspaceGIS>
    {
        #region Konstruktori

        public colWorkspaceGIS()
        {
        }
        public colWorkspaceGIS(IEnumerable<clsWorkspaceGIS> collection)
            : base(collection)
        {
        }

        public colWorkspaceGIS(bool teGjithe)
        {
            if (teGjithe)
            {
                using (clsDatabaseGIS dbGIS = new clsDatabaseGIS())
                {
                    mbushWorkspaceGIS(dbGIS.merrAllWorkspace());
                }
            }
        }
        #endregion

        #region Metoda Publike

        public new clsWorkspaceGIS this[int index]
        {
            get
            {
                return ((clsWorkspaceGIS)base[index]);
            }
        }
        public bool shtoListe(clsWorkspaceGIS list)
        {
            this.Add(list);
            if (base.Contains(list))
                return true;
            else return false;
        }
        public bool ekzistonListe(clsWorkspaceGIS list)
        {
            if (base.Contains(list))
                return true;
            else return false;
        }
        #endregion

        #region Metoda Interial

        /// <summary>
        /// Merr te gjithe workspace te celura ne program
        /// </summary>
        /// <returns>DataTable me te gjithe objektet</returns>
        public static DataTable merrAllWorkspaceGrideAmbjenti()
        {
            DataTable table;
            using (clsDatabaseGIS dbGIS = new clsDatabaseGIS())
            {
                table = dbGIS.merrAllWorkspaceGrideAmbjenti();
            }
            return table;
        }

        private bool mbushWorkspaceGIS(DataTable dt)
        {
            try
            {
                for (int i = 0, count = dt.Rows.Count; i < count; i++)
                {
                    Add(new clsWorkspaceGIS(dt.Rows[i]));
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        #endregion
    }
}
