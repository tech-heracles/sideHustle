using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace DbCore.DbGIS
{
    public class colLayersColsGIS : List<clsLayersColsGIS>
    {
        #region Konstruktori
        public colLayersColsGIS()
        {
        }
        public colLayersColsGIS(IEnumerable<clsLayersColsGIS> collection)
            : base(collection)
        {
        }

        #endregion

        #region Metoda Publike
        public new clsLayersColsGIS this[int index]
        {
            get
            {
                return ((clsLayersColsGIS)base[index]);
            }
        }
        public bool shtoLayerCol(clsLayersColsGIS layerCol)
        {
            this.Add(layerCol);
            if (base.Contains(layerCol))
                return true;
            else return false;
        }
        public bool ekzistonLayerCol(clsLayersColsGIS layerCol)
        {
            if (base.Contains(layerCol))
                return true;
            else return false;
        }


        public bool merrTeGjitheKolonat(int idNdermarrje, int gjuha)
        {
            using (clsDatabaseGIS db = new clsDatabaseGIS())
            {
                return mbushLayerCols(db.merrTeGjitheKolonat(idNdermarrje, gjuha));
            }
        }


        public bool merrTeGjitheKolonatSipasIdLayer(int idLayer, int gjuha)
        {
            using (clsDatabaseGIS db = new clsDatabaseGIS())
            {
                return mbushLayerCols(db.merrTeGjitheKolonatSipasIdLayer(idLayer, gjuha));
            }
        }

        public bool merrTeGjitheKolonatSipasLayerTypeDheStatusi(int layerType, int statusi, int gjuha, int idNdermarrje)
        {
            using (clsDatabaseGIS db = new clsDatabaseGIS())
            {
                return mbushLayerCols(db.merrTeGjitheKolonatSipasLayerTypeDheStatusit(layerType, statusi, gjuha, idNdermarrje));
            }
        }

        #endregion

        #region Metoda Interial
        private bool mbushLayerCols(DataTable dt)
        {
            try
            {
                if (dt == null)
                    return false;
                for (int i = 0, count = dt.Rows.Count; i < count; i++)
                {
                    Add(new clsLayersColsGIS(dt.Rows[i]));
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
