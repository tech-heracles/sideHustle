using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

using DbCore.DbGIS; 

namespace DbCore.DbGIS
{
    public class colKonfigurimeGIS : List<clsKonfigurimeGIS>
    {
        #region Konstruktori
        public colKonfigurimeGIS()
        {
        }
        public colKonfigurimeGIS(IEnumerable<clsKonfigurimeGIS> collection)
            : base(collection)
        {
        }
        #endregion
        
        #region Metoda Publike
        public new clsKonfigurimeGIS this[int index]
        {
            get
            {
                return ((clsKonfigurimeGIS)base[index]);
            }
        }
        public bool shtoKonfigurim(clsKonfigurimeGIS konfig)
        {
            this.Add(konfig);
            if (base.Contains(konfig))
                return true;
            else return false;
        }
        public bool ekzistonKonfigurim(clsKonfigurimeGIS konfig)
        {
            if (base.Contains(konfig))
                return true;
            else return false;
        }
               

        public bool mbushKonfigurime(int tableId, clsDatabaseGIS db)
        {
            return mbushKonfigurime(db.getAllPerkthime(tableId));
        }

        #endregion

        #region Metoda Interial
        private bool mbushKonfigurime(DataTable dt)
        {
            try
            {
                for (int i = 0, count = dt.Rows.Count; i < count; i++)
                {
                    Add(new clsKonfigurimeGIS(dt.Rows[i]));
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