
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbCRM
{
    public class colDetyreKlientAgjent : System.Collections.Generic.List<clsDetyreKlientAgjent>
    {
        #region Konstruktore

        /// <summary>
        /// konstruktor bosh
        /// </summary>
        public colDetyreKlientAgjent()
        {
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbCRM.clsDetyreKlientAgjent"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsDetyreKlientAgjent this[int index]
        {
            get { return ((clsDetyreKlientAgjent)base[index]); }
        }

        /// <summary>
        /// metoda per shtimin e nje obj clsDetyreKlientAgjent ne nje arraylist
        /// </summary>
        public bool shtokokaAnkete(clsDetyreKlientAgjent DetyreKlientAgjent)
        {
            base.Add(DetyreKlientAgjent);
            if (base.Contains(DetyreKlientAgjent))
                return true;
            else return false;
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsDetyreKlientAgjent ne nje arraylist
        /// </summary>
        public bool fshiDetyreKlientAgjent(clsDetyreKlientAgjent DetyreKlientAgjent)
        {
            base.Remove(DetyreKlientAgjent);
            if (base.Contains(DetyreKlientAgjent))
                return false;
            else return true;
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsDetyreKlientAgjent ne nje arraylist
        /// </summary>
        public bool fshiGjitheDetyreKlientAgjent()
        {
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsDetyreKlientAgjent ne nje arraylist
        /// </summary>
        public void fshiKeteDetyreKlientAgjent(int index)
        {
            base.RemoveAt(index);
        }

        /// <summary>
        /// metoda shton nje obj te ri ne array list ne pozicionin e percaktuar nga index-i
        /// </summary>
        public void shtoDetyreKlientAgjentNeIndeksin(int index, clsDetyreKlientAgjent DetyreKlientAgjent)
        {
            base.Insert(index, DetyreKlientAgjent);
        }

        /// <summary>
        /// metoda gjen index-in ne liste ne te cilin ndodhet nje obj i caktuar
        /// </summary>
        public int indeksiDetyreKlientAgjent(clsDetyreKlientAgjent DetyreKlientAgjent)
        {
            return base.IndexOf(DetyreKlientAgjent);
        }

        /// <summary>
        /// metoda kontrollon nqs nje obj ndodhet ne arraylist
        /// </summary>
        public bool ekzistonDetyreKlientAgjent(clsDetyreKlientAgjent DetyreKlientAgjent)
        {
            if (base.Contains(DetyreKlientAgjent))
                return true;
            else return false;
        }

        /// <summary>
        /// metoda qe kthen nr e obj qe ka arraylist
        /// </summary>
        public int numriDetyreKlientAgjente()
        {
            return base.Count;
        }


  
        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbCRM.clsDetyreKlientAgjent"/> 
        /// </summary>
        private bool mbushDetyreKlientAgjent(DataTable dt)
        {
            try
            {
                foreach (DataRow rreshti in dt.Rows)
                {
                    clsDetyreKlientAgjent obj = new clsDetyreKlientAgjent();
                    obj.mbushDetyreKlientAgjent(rreshti);
                    this.Add(obj);
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        #endregion

        /// <summary>
        /// merr gjithe detyrat qe ka realizuar nje agjent tek nje klient ne nje date te caktuar
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="idPerdoruesi"></param>
        /// <param name="idAgjenti"></param>
        /// <param name="idKlienti"></param>
        /// <param name="dtTakimi"></param>
        /// <returns></returns>
        public static DataTable MerrDetyrat(int idNdermarrje, int idAgjenti, int idKlienti, string dtTakimi,int idPerdoruesi)
        {
            clsDatabaseCRM dbCRM = new clsDatabaseCRM();
            DataTable dt = dbCRM.merrDetyraKlientAgjent(idNdermarrje, idAgjenti, idKlienti, dtTakimi,idPerdoruesi);
            dbCRM.Dispose();
            return dt;
        }
    }
}
