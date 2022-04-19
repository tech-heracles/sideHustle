using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace DbCore.DbGIS
{
    public class colLayersTrupiGIS : List<clsLayersTrupiGIS>
    {
        private bool v;
        private int iDLAYER;
        private int idNdermarrje;
        private int idGjuha;
        #region Konstruktori
        public colLayersTrupiGIS()
        {
        }
        public colLayersTrupiGIS(IEnumerable<clsLayersTrupiGIS> collection)
            : base(collection)
        {
        }
        
        #endregion

        #region Metoda Publike
        public new clsLayersTrupiGIS this[int index]
        {
            get
            {
                return ((clsLayersTrupiGIS)base[index]);
            }
        }
        public bool shtoLayersTrupi(clsLayersTrupiGIS baseLayer)
        {
            this.Add(baseLayer);
            if (base.Contains(baseLayer))
                return true;
            else return false;
        }
        public bool ekzistonLayersTrupi(clsLayersTrupiGIS baseLayer)
        {
            if (base.Contains(baseLayer))
                return true;
            else return false;
        }

        public bool merrLayersTrupiGIS(int idNdermarrje, int idGjuha)
        {
            using (clsDatabaseGIS db = new clsDatabaseGIS())
            {
                return merrLayersTrupiGIS(db.merrLayersTrupiGIS(idNdermarrje, idGjuha)); 
            }
        }
        #endregion

        #region Metoda Interial
        private bool merrLayersTrupiGIS(DataTable dt)
        {
            try
            {
                for (int i = 0, count = dt.Rows.Count; i < count; i++)
                {
                    Add(new clsLayersTrupiGIS(dt.Rows[i]));
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
