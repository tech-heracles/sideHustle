using DbCore.IMBUtils.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DbCore.DbCRM
{
    public class colOpsineAnketa : System.Collections.Generic.List<clsOpsioneAnkete>
    {
        #region Konstruktore

        /// <summary>
        /// konstruktor bosh
        /// </summary>
        public colOpsineAnketa()
        {
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbCRM.clsOpsioneAnkete"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsOpsioneAnkete this[int index]
        {
            get { return ((clsOpsioneAnkete)base[index]); }
        }

        /// <summary>
        /// metoda per shtimin e nje obj clsOpsioneAnkete ne nje arraylist
        /// </summary>
        public bool shtoopsioneAnkete(clsOpsioneAnkete opsioneAnkete)
        {
            base.Add(opsioneAnkete);
            if (base.Contains(opsioneAnkete))
                return true;
            else return false;
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsOpsioneAnkete ne nje arraylist
        /// </summary>
        public bool fshiopsioneAnkete(clsOpsioneAnkete opsioneAnkete)
        {
            base.Remove(opsioneAnkete);
            if (base.Contains(opsioneAnkete))
                return false;
            else return true;
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsOpsioneAnkete ne nje arraylist
        /// </summary>
        public bool fshiGjitheopsioneAnkete()
        {
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsOpsioneAnkete ne nje arraylist
        /// </summary>
        public void fshiKeteopsioneAnkete(int index)
        {
            base.RemoveAt(index);
        }

        /// <summary>
        /// metoda shton nje obj te ri ne array list ne pozicionin e percaktuar nga index-i
        /// </summary>
        public void shtoopsioneAnketeNeIndeksin(int index, clsOpsioneAnkete opsioneAnkete)
        {
            base.Insert(index, opsioneAnkete);
        }

        /// <summary>
        /// metoda gjen index-in ne liste ne te cilin ndodhet nje obj i caktuar
        /// </summary>
        public int indeksiopsioneAnkete(clsOpsioneAnkete opsioneAnkete)
        {
            return base.IndexOf(opsioneAnkete);
        }

        /// <summary>
        /// metoda kontrollon nqs nje obj ndodhet ne arraylist
        /// </summary>
        public bool ekzistonopsioneAnkete(clsOpsioneAnkete opsioneAnkete)
        {
            if (base.Contains(opsioneAnkete))
                return true;
            else return false;
        }

        /// <summary>
        /// metoda qe kthen nr e obj qe ka arraylist
        /// </summary>
        public int numriopsioneAnkete()
        {
            return base.Count;
        }
        public static colOpsineAnketa merrOpsioneAnketa(int indermarje)
        {
            DbCore.DbCRM.clsDatabaseCRM dbCRM = new DbCore.DbCRM.clsDatabaseCRM();
            colOpsineAnketa trupi = new colOpsineAnketa();
            trupi.mbushColOpsioneAnketa(dbCRM.merrOpsionAnketeSipasNdermarrjes(indermarje));
            dbCRM.Dispose();
            return trupi;
        }
        
        public DataTable merropsioneAnketePerNdermarrje(int idNdermarrje)
        {
            clsDatabaseCRM dbCRM = new clsDatabaseCRM();
            DataTable dt = dbCRM.merrOpsionAnketeSipasNdermarrjes(idNdermarrje);
            dbCRM.Dispose();
            return dt;
        }

        public bool mbushopsioneAnketePerNdermarrje(int idNdermarrje)
        {
            clsDatabaseCRM dbCRM = new clsDatabaseCRM();
            bool sukses = mbushColOpsioneAnketa(dbCRM.merrOpsionAnketeSipasNdermarrjes(idNdermarrje));
            dbCRM.Dispose();
            return sukses;
        }

        #endregion

        #region Metoda Private

       
        private bool mbushColOpsioneAnketa(DataTable dt)
        {
            try
            {
                foreach (DataRow rreshti in dt.Rows)
                { 
                    clsOpsioneAnkete opsioneAnkete = new clsOpsioneAnkete();
                  //  opsioneAnkete.mbushOpsioneAnkete(rreshti);
                    Add(opsioneAnkete);
                }
            }
            catch (Exception err)
            {
                ImbLogger.Error(err.Message);
                return false;
            }
            return true;
        }

        #endregion
    }
}


        //#region Metoda Publike

        ///// <summary>
        ///// kthen objektin <see cref="DbCore.DbCRM.clsOpsioneAnkete"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        ///// </summary>
        //public new clsOpsioneAnkete this[int index]
        //{
        //    get { return ((clsOpsioneAnkete)base[index]); }
        //}

       
        //#endregion

        //#region Metoda Private

        //private bool mbushColOpsioneAnketa(DataTable dt)
        //{
        //    try
        //    {
        //        foreach (DataRow rreshti in dt.Rows)
        //        {
        //            clsOpsioneAnkete opsione = new clsOpsioneAnkete();
        //            opsione.mbushOpsioneAnkete(rreshti);
        //            Add(opsione);
        //        }

        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //    return true;
        //}

        //#endregion

 