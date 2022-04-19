using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;

using DbCore.DbGIS;

namespace DbCore.DbGIS
{
    public class colLayersTypeGIS : List<clsLayersTypeGIS>
    {
        #region Konstruktori

        public colLayersTypeGIS()
        {
        }
        public colLayersTypeGIS(IEnumerable<clsLayersTypeGIS> collection)
            : base(collection)
        {
        }

        #endregion

        #region Metoda Publike

        public new clsLayersTypeGIS this[int index]
        {
            get
            {
                return ((clsLayersTypeGIS)base[index]);
            }
        }
        public bool shtoListe(clsLayersTypeGIS list)
        {
            this.Add(list);
            if (base.Contains(list))
                return true;
            else return false;
        }
        public bool ekzistonListe(clsLayersTypeGIS list)
        {
            if (base.Contains(list))
                return true;
            else return false;
        }

        /// <summary>
        /// mbush nje collection me te gjithe tipet e layerave 
        /// </summary>
        /// <param name="dbGis">Connection me db</param>
        /// <returns>coleksionin e kerkuar</returns>
        public bool mbushLayersType()
        {   
            clsDatabaseGIS dbGis = new clsDatabaseGIS();
            bool mbush = mbushLayersType(dbGis.ktheLayersType());
            dbGis.Dispose();
            return mbush;
        }

        #endregion

        #region Metoda Interial
        private bool mbushLayersType(DataTable dt)
        {
            for (int i = 0, count = dt.Rows.Count; i < count; i++)
            {
                Add(new clsLayersTypeGIS(dt.Rows[i]));
            }
            return true;
        }

        #endregion
    }
}
