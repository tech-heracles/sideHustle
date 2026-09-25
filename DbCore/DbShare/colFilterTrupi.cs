using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using DbCore.DbAdmin;

namespace DbCore.DbShare
{
    public class colFilterTrupi : System.Collections.Generic.List<clsFilterTrupi>
    {
        
        //public new clsFilterTrupi this[int index]
        //    get { return ((clsFilterTrupi)base[index]); }

        //public bool merriTeGjithe(int idKokaFilter)


        #region Konstruktoret

        
        public colFilterTrupi()
        {           
        }

        public colFilterTrupi(int idKokaFilter)
        {
            clsDatabaseShare data = new clsDatabaseShare();
            mbushColFilterTrupi(data.merrFilterTrupin(idKokaFilter));
            data.Dispose();
        }
        
        
        #endregion

        #region Metoda Internal


        /// <summary>
        /// fshin gjithe koleksionin 
        /// </summary>
        /// <param name="data"> clsdatabaseshare nese do te perdoresh transaksion, null perndryshe</param>
        /// <returns>true nese fshirja kryhet me sukse, false perndryshe</returns>
        internal bool fshi(clsDatabaseShare data)
        {
            try
            {
                foreach (clsFilterTrupi rreshti in this)
                    rreshti.fshi(data);
            }
            catch (Exception e)
            {
                return false;
                throw e;
            }
            return true;
        }
        internal DataTable mbushColFilterTrupi()
        {
            if (this.Count == 0)
                return null;
            DataTable trupat = null;
            try
            {
                foreach (clsFilterTrupi filterTrupi in this)
                {
                    DataRow rreshti = filterTrupi.mbushFilterTrupi();
                    if (trupat == null)
                        trupat = rreshti.Table;
                    trupat.LoadDataRow(rreshti.ItemArray, false);                    
                }
            }
            catch (Exception)
            {
                return null;
            }
            return trupat;
        }

        #endregion

        #region Metoda Private

        private bool mbushColFilterTrupi(DataTable dt)
        {
            //try
                foreach (DataRow rreshti in dt.Rows)
                {
                    clsFilterTrupi trupi = new clsFilterTrupi();
                    if (trupi.mbushFilterTrupi(rreshti))
                        Add(trupi);
                }
                return true;
                
        }

        #endregion
    }
}
