using System;
using System.Collections.Generic;
using System.Data;

namespace DbCore.DbCRM
{
    public class colDetyreKlient : List<clsDetyreKlient>
    {
        #region Konstruktore

        /// <summary>
        /// konstruktor bosh 
        /// </summary>
        public colDetyreKlient(int idklienti)
        {
            using (clsDatabaseCRM db = new clsDatabaseCRM())
            {
                mbushDetyreKlient(db.merrDetyreKlientSipasKlientit(idklienti));
            }
        }

        #endregion Konstruktore

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbCRM.clsDetyreKlient" /> qe ndodhet ne nje index te
        /// caktuar te arraylist-es
        /// </summary>
        public new clsDetyreKlient this[int index]
        {
            get { return ((clsDetyreKlient)base[index]); }
        }

        public static DataTable merrDetyratPerKlientMeDataVlefshmerie(int idNdermarrje, int idPerdoruesi,int idKlienti)
        {
            clsDatabaseCRM dbCRM = new clsDatabaseCRM();
            DataTable dt = dbCRM.merrDetyratPerKlientMeDataVlefshmerie(idNdermarrje, idPerdoruesi,idKlienti);
            dbCRM.Dispose();
            return dt;
        }

        /// <summary>
        /// metoda per shtimin e nje obj clsDetyreKlient ne nje arraylist 
        /// </summary>
        public bool shtodetyreKlient(clsDetyreKlient detyreKlient)
        {
            base.Add(detyreKlient);
            if (base.Contains(detyreKlient))
                return true;
            else return false;
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsDetyreKlient ne nje arraylist 
        /// </summary>
        public bool fshidetyreKlient(clsDetyreKlient detyreKlient)
        {
            base.Remove(detyreKlient);
            if (base.Contains(detyreKlient))
                return false;
            else return true;
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsDetyreKlient ne nje arraylist 
        /// </summary>
        public bool fshiGjithedetyreKlient()
        {
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsDetyreKlient ne nje arraylist 
        /// </summary>
        public void fshiKetedetyreKlient(int index)
        {
            base.RemoveAt(index);
        }

        /// <summary>
        /// metoda shton nje obj te ri ne array list ne pozicionin e percaktuar nga index-i 
        /// </summary>
        public void shtodetyreKlientNeIndeksin(int index, clsDetyreKlient detyreKlient)
        {
            base.Insert(index, detyreKlient);
        }

        /// <summary>
        /// metoda gjen index-in ne liste ne te cilin ndodhet nje obj i caktuar 
        /// </summary>
        public int indeksidetyreKlient(clsDetyreKlient detyreKlient)
        {
            return base.IndexOf(detyreKlient);
        }

        /// <summary>
        /// metoda kontrollon nqs nje obj ndodhet ne arraylist 
        /// </summary>
        public bool ekzistonDetyreKlient(clsDetyreKlient detyreKlient)
        {
            if (base.Contains(detyreKlient))
                return true;
            else return false;
        }

        /// <summary>
        /// metoda qe kthen nr e obj qe ka arraylist 
        /// </summary>
        public int numriDetyreKlient()
        {
            return base.Count;
        }

        #endregion Metoda Publike

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store
        /// procedure ne formatin e nje datatable <see cref="DbCore.DbCRM.clsDetyreKlient" />
        /// </summary>
        private bool mbushDetyreKlient(DataTable dt)
        {
            try
            {
                foreach (DataRow rreshti in dt.Rows)
                {
                    clsDetyreKlient detyreKlient = new clsDetyreKlient();
                    detyreKlient.mbushDetyreKlient(rreshti);
                    this.Add(detyreKlient);
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        #endregion Metoda Private

        /// <summary>
        /// kthen detyrat dhe klientet te cilat i jane lidhur nje klienti specifik
        /// </summary>
        /// <param name="klientID"></param>
        /// <returns></returns>
        public static DataTable merrDetyratDheAnketat(int klientID,int idNdermarrje,bool vetemTeVlefshmet)
        {
            using (clsDatabaseCRM dbCRM = new clsDatabaseCRM())
            {
               return dbCRM.merrDetyratDheAnketat(klientID,idNdermarrje,vetemTeVlefshmet);
            }
        }

        /// <summary>
        /// merr te gjitha detyrat e lidhura me nje takim te caktuar
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static DataTable merrDetyratPerKlient(int idTakimi)
        {
            using (clsDatabaseCRM dbCRM = new clsDatabaseCRM())
            {
                return dbCRM.merrDetyratPerTakim(idTakimi);
            }

        }
    }
}