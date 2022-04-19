using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

using DbCore.DbGIS;

namespace DbCore.DbGIS
{
    public class colSkedaretGIS : List<clsSkedaretGIS>
    {
        #region Konstruktori
        public colSkedaretGIS()
        {
        }
        public colSkedaretGIS(IEnumerable<clsSkedaretGIS> collection)
            : base(collection)
        {
        }
        #endregion
        
        #region Metoda Publike
        public new clsSkedaretGIS this[int index]
        {
            get
            {
                return ((clsSkedaretGIS)base[index]);
            }
        }
        public bool shtoKonfigurim(clsSkedaretGIS skedar)
        {
            this.Add(skedar);
            if (base.Contains(skedar))
                return true;
            else return false;
        }
        public bool ekzistonKonfigurim(clsSkedaretGIS skedar)
        {
            if (base.Contains(skedar))
                return true;
            else return false;
        }
               

        public bool mbushSkedare(clsDatabaseGIS db)
        {
            return mbushSkedare(db.getAllSkedare());
        }
        public bool mbushSkedareLloji(string lloji, int idPerdorues)
        {
            clsDatabaseGIS db = new clsDatabaseGIS();
            return mbushSkedare(db.getAllSkedareLloji(lloji, idPerdorues));
        }

        public bool mbushSkedarePerObjektGeo(string idDytesore)
        {
            clsDatabaseGIS db = new clsDatabaseGIS();
          //  return mbushSkedare(db.getAllSkedareLloji(lloji));
            return mbushSkedare(db.getAllSkedareObjektGeo(idDytesore));
        }
        #endregion

        #region Metoda Interial
        private bool mbushSkedare(DataTable dt)
        {
            try
            {
                for (int i = 0, count = dt.Rows.Count; i < count; i++)
                {
                    Add(new clsSkedaretGIS(dt.Rows[i]));
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