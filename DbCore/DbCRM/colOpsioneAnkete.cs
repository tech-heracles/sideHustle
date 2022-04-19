using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DbCore.DbCRM
{
    public class colOpsioneAnkete : System.Collections.Generic.List<clsOpsioneAnkete>
    {
        #region Konstruktore

        /// <summary>
        /// konstruktor bosh
        /// </summary>
        public colOpsioneAnkete()
        {
        }
        public colOpsioneAnkete(int idnder)
        {
            clsDatabaseCRM dbOpsioneAnkete = new clsDatabaseCRM();
            mbushOpsioneAnkete(dbOpsioneAnkete.merrOpsionAnketeSipasNdermarrjes(idnder));
            dbOpsioneAnkete.Dispose();
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
        public bool shtoOpsionAnkete(clsOpsioneAnkete opsionAnkete)
        {
            base.Add(opsionAnkete);
            if (base.Contains(opsionAnkete))
                return true;
            else return false;
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsOpsioneAnkete ne nje arraylist
        /// </summary>
        public bool fshiOpsionAnkete(clsOpsioneAnkete opsionAnkete)
        {
            base.Remove(opsionAnkete);
            if (base.Contains(opsionAnkete))
                return false;
            else return true;
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsOpsioneAnkete ne nje arraylist
        /// </summary>
        public bool fshiGjitheOpsioneAnkete()
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
        public void fshiKeteOpsionAnkete(int index)
        {
            base.RemoveAt(index);
        }

        /// <summary>
        /// metoda shton nje obj te ri ne array list ne pozicionin e percaktuar nga index-i
        /// </summary>
        public void shtoOpsionAnketeNeIndeksin(int index, clsOpsioneAnkete opsionAnkete)
        {
            base.Insert(index, opsionAnkete);
        }

        /// <summary>
        /// metoda gjen index-in ne liste ne te cilin ndodhet nje obj i caktuar
        /// </summary>
        public int indeksiOpsionAnkete(clsOpsioneAnkete opsionAnkete)
        {
            return base.IndexOf(opsionAnkete);
        }

        /// <summary>
        /// metoda kontrollon nqs nje obj ndodhet ne arraylist
        /// </summary>
        public bool ekzistonOpsionAnkete(clsOpsioneAnkete opsionAnkete)
        {
            if (base.Contains(opsionAnkete))
                return true;
            else return false;
        }

        /// <summary>
        /// metoda qe kthen nr e obj qe ka arraylist
        /// </summary>
        public int numriOpsionAnkete()
        {
            return base.Count;
        }

        public static DataTable merrOpsioneAnketePerNdermarrje(int idNdermarrje)
        {
            clsDatabaseCRM dbCRM = new clsDatabaseCRM();
            DataTable dt = dbCRM.merrOpsionAnketeSipasNdermarrjes(idNdermarrje);
            dbCRM.Dispose();
            return dt;
        }

        public bool mbushOpsioneAnketePerNdermarrje(int idNdermarrje)
        {
            clsDatabaseCRM dbCRM = new clsDatabaseCRM();
            bool sukses = mbushOpsioneAnkete(dbCRM.merrOpsionAnketeSipasNdermarrjes(idNdermarrje));
            dbCRM.Dispose();
            return sukses;
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbCRM.clsOpsioneAnkete"/> 
        /// </summary>
        private bool mbushOpsioneAnkete(DataTable dt)
        {
            try
            {
                foreach (DataRow rreshti in dt.Rows)
                {
                    clsOpsioneAnkete opsioneAnkete = new clsOpsioneAnkete();
                    opsioneAnkete.mbushOpsioneAnkete(rreshti);
                    this.Add(opsioneAnkete);
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