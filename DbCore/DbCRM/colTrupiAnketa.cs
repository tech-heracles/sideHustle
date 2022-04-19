using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DbCore.DbCRM
{
    public class colTrupiAnketa : System.Collections.Generic.List<clsTrupiAnketa>
    {
        #region Konstruktore

        /// <summary>
        /// konstruktor bosh
        /// </summary>
        public colTrupiAnketa()
        {
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbCRM.clsTrupiAnketa"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsTrupiAnketa this[int index]
        {
            get { return ((clsTrupiAnketa)base[index]); }
        }

        /// <summary>
        /// metoda per shtimin e nje obj clsTrupiAnketa ne nje arraylist
        /// </summary>
        public bool shtoTrupiAnkete(clsTrupiAnketa trupiAnkete)
        {
            base.Add(trupiAnkete);
            if (base.Contains(trupiAnkete))
                return true;
            else return false;
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsTrupiAnketa ne nje arraylist
        /// </summary>
        public bool fshiTrupiAnkete(clsTrupiAnketa trupiAnkete)
        {
            base.Remove(trupiAnkete);
            if (base.Contains(trupiAnkete))
                return false;
            else return true;
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsTrupiAnketa ne nje arraylist
        /// </summary>
        public bool fshiGjitheTrupiAnkete()
        {
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsTrupiAnketa ne nje arraylist
        /// </summary>
        public void fshiKeteTrupiAnkete(int index)
        {
            base.RemoveAt(index);
        }

        /// <summary>
        /// metoda shton nje obj te ri ne array list ne pozicionin e percaktuar nga index-i
        /// </summary>
        public void shtoTrupiAnketeNeIndeksin(int index, clsTrupiAnketa trupiAnkete)
        {
            base.Insert(index, trupiAnkete);
        }

        /// <summary>
        /// metoda gjen index-in ne liste ne te cilin ndodhet nje obj i caktuar
        /// </summary>
        public int indeksiTrupiAnkete(clsTrupiAnketa trupiAnkete)
        {
            return base.IndexOf(trupiAnkete);
        }

        /// <summary>
        /// metoda kontrollon nqs nje obj ndodhet ne arraylist
        /// </summary>
        public bool ekzistonTrupiAnkete(clsTrupiAnketa trupiAnkete)
        {
            if (base.Contains(trupiAnkete))
                return true;
            else return false;
        }

        /// <summary>
        /// metoda qe kthen nr e obj qe ka arraylist
        /// </summary>
        public int numriTrupiAnkete()
        {
            return base.Count;
        }
        public static colTrupiAnketa merrOpsioneAnketa(int indermarje)
        {
            DbCore.DbCRM.clsDatabaseCRM dbCRM = new DbCore.DbCRM.clsDatabaseCRM();
            colTrupiAnketa trupi = new colTrupiAnketa();
         //   trupi.mbushTrupiAnketeSipasIdKoka(dbCRM.merrOpsionAnketeSipasNdermarrjes(indermarje));
            dbCRM.Dispose();
            return trupi;
        }
        public DataTable merrTrupAnketeSipasIdKoka(int idKoka, int idndermarrje, int visible)
        {
            clsDatabaseCRM dbCRM = new clsDatabaseCRM();
            DataTable dt = dbCRM.merrTrupAnketeSipasIdKoka(idKoka);
            dbCRM.Dispose();
            return dt;
        }

        public bool mbushTrupiAnketeSipasIdKoka(int idKoka)
        {
            clsDatabaseCRM dbCRM = new clsDatabaseCRM();
            bool sukses = mbushTrupiAnkete(dbCRM.merrTrupAnketeSipasIdKoka(idKoka));
            dbCRM.Dispose();
            return sukses;
        }
        public bool merrTrupAnketeAllOpsioneSipasNdermarje(int idndermarje)
        {
            clsDatabaseCRM dbCRM = new clsDatabaseCRM();
            bool sukses = mbushTrupiAnkete(dbCRM.merrTrupAnketeAllOpsioneSipasNdermarje(idndermarje));
            dbCRM.Dispose();
            return sukses;
        }
        public bool merrTrupAnketeOpsioneTePamaraSipasNdermarje(int idndermarje, int idkoka)
        {
            clsDatabaseCRM dbCRM = new clsDatabaseCRM();
            bool sukses = mbushTrupiAnkete(dbCRM.merrTrupAnketeOpsioneTePamaraSipasNdermarje(idndermarje, idkoka));
            dbCRM.Dispose();
            return sukses;
        }
        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbCRM.clsTrupiAnketa"/> 
        /// </summary>
        private bool mbushTrupiAnkete(DataTable dt)
        {
            try
            {
                foreach (DataRow rreshti in dt.Rows)
                {
                    clsTrupiAnketa trupiAnkete = new clsTrupiAnketa();
                    trupiAnkete.mbushTrupiAnketa(rreshti);
                    this.Add(trupiAnkete);
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