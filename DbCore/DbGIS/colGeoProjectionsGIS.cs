using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace DbCore.DbGIS
{
    public class colGeoProjectionsGIS : List<clsGeoProjectionsGIS>
    {
        #region Konstruktori
        public colGeoProjectionsGIS()
        {
        }
        public colGeoProjectionsGIS(IEnumerable<clsGeoProjectionsGIS> collection)
            : base(collection)
        {
        }

        #endregion

        #region Metoda Publike
        public new clsGeoProjectionsGIS this[int index]
        {
            get
            {
                return ((clsGeoProjectionsGIS)base[index]);
            }
        }
        public bool shtoProjection(clsGeoProjectionsGIS projection)
        {
            this.Add(projection);
            if (base.Contains(projection))
                return true;
            else return false;
        }
        public bool ekzistonProjection(clsGeoProjectionsGIS projection)
        {
            if (base.Contains(projection))
                return true;
            else return false;
        }

        public bool merrGeoProjectionsGIS()
        {
            using (clsDatabaseGIS db = new clsDatabaseGIS())
            {
                return mbushGeoProjectionsGIS(db.merrGeoProjectionsGIS());
            }
        }
        #endregion

        #region Metoda Interial
        private bool mbushGeoProjectionsGIS(DataTable dt)
        {
            try
            {
                for (int i = 0, count = dt.Rows.Count; i < count; i++)
                {
                    Add(new clsGeoProjectionsGIS(dt.Rows[i]));
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
