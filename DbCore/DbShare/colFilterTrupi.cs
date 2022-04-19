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
        //{
        //    get { return ((clsFilterTrupi)base[index]); }
        //}

        //public bool merriTeGjithe(int idKokaFilter)
        //{
        //    clsDatabaseShare db = new clsDatabaseShare();
        //    return mbushColFilterTrupi(db.merrFilterTrupin(idKokaFilter));

        //}

        //public colFilterTrupi mbushArrayListFilterTrupi(DataSet ds)
        //{
        //    colFilterTrupi trupat = new colFilterTrupi();

        //    foreach (DataRow rreshti in ds.Tables[0].Rows)
        //    {
        //        clsFilterTrupi trupi = new clsFilterTrupi();

        //        trupi.IdTrupiFilter = int.Parse(rreshti[0].ToString());
        //        trupi.IdKokaFilter = int.Parse(rreshti[1].ToString());
        //        trupi.Kontrolli = rreshti[2].ToString();
        //        trupi.Vlera = rreshti[3].ToString();
        //        //trupi.LidhesaLogjike = rreshti[4].ToString();                
        //        trupat.Add(trupi);
        //    }
        //    return trupat;
        //}

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
                //throw;
            }
            return trupat;
        }

        #endregion

        #region Metoda Private

        private bool mbushColFilterTrupi(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    clsFilterTrupi trupi = new clsFilterTrupi();
                    if (trupi.mbushFilterTrupi(rreshti))
                        Add(trupi);
                }
                return true;
            //}
            //catch (Exception)
            //{
                
            //    return false;
            //}
        }

        #endregion
    }
}
