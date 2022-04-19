using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DbCore.DbCRM
{
    public class colKokaAnketa : System.Collections.Generic.List<clsKokaAnketa>
    {
        #region Konstruktore

        /// <summary>
        /// konstruktor bosh
        /// </summary>
        public colKokaAnketa()
        {
        }
           /// <summary>
        /// konstruktor me 1 parameter integer
        /// </summary>
        /// <param name="idnder">id e ndermarrjes</param>
        public colKokaAnketa(int idnder)
        {
            clsDatabaseCRM dbKokaAnkete = new clsDatabaseCRM();
            mbushKokeAnkete(dbKokaAnkete.merrKokaAnketeSipasNdermarrjes(idnder));
            dbKokaAnkete.Dispose();

        }
        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbCRM.clsKokaAnketa"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsKokaAnketa this[int index]
        {
            get { return ((clsKokaAnketa)base[index]); }
        }

        /// <summary>
        /// metoda per shtimin e nje obj clsKokaAnketa ne nje arraylist
        /// </summary>
        public bool shtokokaAnkete(clsKokaAnketa kokaAnkete)
        {
            base.Add(kokaAnkete);
            if (base.Contains(kokaAnkete))
                return true;
            else return false;
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsKokaAnketa ne nje arraylist
        /// </summary>
        public bool fshikokaAnkete(clsKokaAnketa kokaAnkete)
        {
            base.Remove(kokaAnkete);
            if (base.Contains(kokaAnkete))
                return false;
            else return true;
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsKokaAnketa ne nje arraylist
        /// </summary>
        public bool fshiGjithekokaAnkete()
        {
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsKokaAnketa ne nje arraylist
        /// </summary>
        public void fshiKetekokaAnkete(int index)
        {
            base.RemoveAt(index);
        }

        /// <summary>
        /// metoda shton nje obj te ri ne array list ne pozicionin e percaktuar nga index-i
        /// </summary>
        public void shtokokaAnketeNeIndeksin(int index, clsKokaAnketa kokaAnkete)
        {
            base.Insert(index, kokaAnkete);
        }

        /// <summary>
        /// metoda gjen index-in ne liste ne te cilin ndodhet nje obj i caktuar
        /// </summary>
        public int indeksikokaAnkete(clsKokaAnketa kokaAnkete)
        {
            return base.IndexOf(kokaAnkete);
        }

        /// <summary>
        /// metoda kontrollon nqs nje obj ndodhet ne arraylist
        /// </summary>
        public bool ekzistonkokaAnkete(clsKokaAnketa kokaAnkete)
        {
            if (base.Contains(kokaAnkete))
                return true;
            else return false;
        }

        /// <summary>
        /// metoda qe kthen nr e obj qe ka arraylist
        /// </summary>
        public int numrikokaAnkete()
        {
            return base.Count;
        }

        public DataTable merrKokaAnketePerNdermarrje(int idNdermarrje)
        {
            clsDatabaseCRM dbCRM = new clsDatabaseCRM();
            DataTable dt = dbCRM.merrKokaAnketeSipasNdermarrjes(idNdermarrje);
            dbCRM.Dispose();
            return dt;
        }

        public bool mbushkokaAnketePerNdermarrje(int idNdermarrje)
        {
            clsDatabaseCRM dbCRM = new clsDatabaseCRM();
            bool sukses = mbushKokeAnkete(dbCRM.merrKokaAnketeSipasNdermarrjes(idNdermarrje));
            dbCRM.Dispose();
            return sukses;
        }
        public bool merrKokaAnketeSipasNdermarrjesAktive(int idNdermarrje)
        {
            clsDatabaseCRM dbCRM = new clsDatabaseCRM();
            bool sukses = mbushKokeAnkete(dbCRM.merrKokaAnketeSipasNdermarrjesAktive(idNdermarrje));
            dbCRM.Dispose();
            return sukses;
        }
        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbCRM.clsKokaAnketa"/> 
        /// </summary>
        private bool mbushKokeAnkete(DataTable dt)
        {
            try
            {
                foreach (DataRow rreshti in dt.Rows)
                {
                    clsKokaAnketa kokaAnkete = new clsKokaAnketa();
                    kokaAnkete.mbushKokaAnketa(rreshti);
                    this.Add(kokaAnkete);
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
