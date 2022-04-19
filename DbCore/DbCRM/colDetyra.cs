using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DbCore.DbCRM
{
    public class colDetyra:List<clsDetyra>
    {
         #region Konstruktore

        /// <summary>
        /// konstruktor bosh
        /// </summary>
        public colDetyra()
        {
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbCRM.clsDetyra"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsDetyra this[int index]
        {
            get { return ((clsDetyra)base[index]); }
        }

        /// <summary>
        /// metoda per shtimin e nje obj clsDetyra ne nje arraylist
        /// </summary>
        public bool shtoDetyre(clsDetyra detyra)
        {
            base.Add(detyra);
            if (base.Contains(detyra))
                return true;
            else return false;
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsDetyra ne nje arraylist
        /// </summary>
        public bool fshiDetyre(clsDetyra detyra)
        {
            base.Remove(detyra);
            if (base.Contains(detyra))
                return false;
            else return true;
        }

        /// <summary>
        /// metoda per heqjen e  objekteve clsDetyra ne nje arraylist
        /// </summary>
        public bool fshiGjitheDetyrat()
        {
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsDetyra ne nje arraylist
        /// </summary>
        public void fshiKeteDetyre(int index)
        {
            base.RemoveAt(index);
        }

        /// <summary>
        /// metoda shton nje obj te ri ne array list ne pozicionin e percaktuar nga index-i
        /// </summary>
        public void shtoDetyraNeIndeksin(int index, clsDetyra detyra)
        {
            base.Insert(index, detyra);

        }
        /// <summary>
        /// metoda gjen index-in ne liste ne te cilin ndodhet nje obj i caktuar
        /// </summary>
        public int indeksiDetyra(clsDetyra detyra)
        {
            return base.IndexOf(detyra);
        }

        /// <summary>
        /// metoda kontrollon nqs nje obj ndodhet ne arraylist
        /// </summary>
        public bool ekzistonDetyra(clsDetyra detyra)
        {
            if (base.Contains(detyra))
                return true;
            else return false;
        }

        /// <summary>
        /// metoda qe kthen nr e obj qe ka arraylist
        /// </summary>
        public int numriDetyra()
        {
            return base.Count;
        }

        /// <summary>
        /// Merr detyrat sipas ndermarrjes.
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <returns></returns>
        public static DataTable merrDetyratPerNdermarrje(int idNdermarrje)
        {
            clsDatabaseCRM dbCRM = new clsDatabaseCRM();
            DataTable dt = dbCRM.merrDetyratPerNdermarrje(idNdermarrje);
            dbCRM.Dispose();
            return dt;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="idPerdoruesi"></param>
        /// <param name="kategoria"></param>
        /// <returns></returns>
        public static DataTable merrDetyratPerNdermarrjeKategoriAutorizim(int idNdermarrje,int idPerdoruesi,int kategoria)
        {
            clsDatabaseCRM dbCRM = new clsDatabaseCRM();
            DataTable dt = dbCRM.merrDetyratPerNdermarrjeKategoriAutorizim(idNdermarrje,idPerdoruesi,kategoria);
            dbCRM.Dispose();
            return dt;
        }
        public static DataTable merrHistorikDetyraAgjentesh(int idNdermarrje, int idPerdoruesi)
        {
            clsDatabaseCRM dbCrm = new clsDatabaseCRM();
            DataTable dt = dbCrm.merrDetyreHistoriku(idNdermarrje, idPerdoruesi);
            dbCrm.Dispose();
            return dt;
        }

        public bool mbushDetyraPerNdermarrje(int idNdermarrje)
        {
            clsDatabaseCRM dbCRM = new clsDatabaseCRM();
            bool sukses = mbushDetyra(dbCRM.merrDetyratPerNdermarrje(idNdermarrje));
            dbCRM.Dispose();
            return sukses;
        }
       public static DataTable merrDetyratSipasKategorisDheNdermarrjes(int idndermarrje,int kategoria)
       {
           clsDatabaseCRM dbCRM = new clsDatabaseCRM();
           DataTable dt = dbCRM.merrDetyratSipasKategorisDheNdermarrjes(idndermarrje,kategoria);
           dbCRM.Dispose();
           return dt;
       }
        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbCRM.clsDetyra"/> 
        /// </summary>
        private bool mbushDetyra(DataTable dt)
        {
            try
            {
                foreach (DataRow rreshti in dt.Rows)
                {
                    clsDetyra detyra = new clsDetyra();
                    detyra.mbushDetyre(rreshti);
                    this.Add(detyra);
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
