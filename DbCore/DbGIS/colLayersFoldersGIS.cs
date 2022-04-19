using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;


namespace DbCore.DbGIS
{
    public class colLayersFoldersGIS : List<clsLayersFoldersGIS>
    {
        #region Konstruktori
        public colLayersFoldersGIS()
        {
        }
        public colLayersFoldersGIS(IEnumerable<clsLayersFoldersGIS> collection)
            : base(collection)
        {
        }

        #endregion

        #region Metoda Publike
        public new clsLayersFoldersGIS this[int index]
        {
            get
            {
                return ((clsLayersFoldersGIS)base[index]);
            }
        }
        public bool shtoLayer(clsLayersFoldersGIS layer)
        {
            this.Add(layer);
            if (base.Contains(layer))
                return true;
            else return false;
        }
        public bool ekzistonLayer(clsLayersFoldersGIS layer)
        {
            if (base.Contains(layer))
                return true;
            else return false;
        }
        public bool merrSipasTeDrejtaveFolderat(int idPerdorues, int gjuha, int idNdermarrje, int idViti, string lloji, int all, string layerName)
        {
            using (clsDatabaseGIS db = new clsDatabaseGIS())
            {
                return mbushLayersFolders(db.merrSipasTeDrejtaveFolderat(idPerdorues, gjuha, idNdermarrje, idViti, lloji, all, layerName));
            }
        }
        #endregion

        #region Metoda Interial
        private bool mbushLayersFolders(DataTable dt)
        {
            try
            {
                for (int i = 0, count = dt.Rows.Count; i < count; i++)
                {
                    Add(new clsLayersFoldersGIS(dt.Rows[i]));
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
