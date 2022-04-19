using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace DbCore.DbGIS
{
    public class colLayersKokaGIS : List<clsLayersKokaGIS>
    {
        #region Konstruktori
        public colLayersKokaGIS()
        {
        }
        public colLayersKokaGIS(IEnumerable<clsLayersKokaGIS> collection)
            : base(collection)
        {
        }

        #endregion

        #region Metoda Publike
        public new clsLayersKokaGIS this[int index]
        {
            get
            {
                return ((clsLayersKokaGIS)base[index]);
            }
        }
        public bool shtoBaseLayer(clsLayersKokaGIS baseLayer)
        {
            this.Add(baseLayer);
            if (base.Contains(baseLayer))
                return true;
            else return false;
        }
        public bool ekzistonBaseLayer(clsLayersKokaGIS baseLayer)
        {
            if (base.Contains(baseLayer))
                return true;
            else return false;
        }

        public bool merrBaseLayersGIS(int idNdermarrje)
        {
            using (clsDatabaseGIS db = new clsDatabaseGIS())
            {
                return merrBaseLayersGIS(db.merrBaseLayersGIS(idNdermarrje)); 
            }
        }
        #endregion

        #region Metoda Interial
        private bool merrBaseLayersGIS(DataTable dt)
        {
            try
            {
                for (int i = 0, count = dt.Rows.Count; i < count; i++)
                {
                    Add(new clsLayersKokaGIS(dt.Rows[i]));
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
