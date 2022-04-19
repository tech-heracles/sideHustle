using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbInventari
    {
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsMetodeKostoje
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colMetodeKostoje : System.Collections.Generic.List<clsMetodeKostoje>
        {
        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbInventari.clsMetodeKostoje"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsMetodeKostoje this[int index]
            {
            get { return ((clsMetodeKostoje)base[index]); }
            }
        /// <summary>
        /// metoda per shtimin e nje obj clsMetodeKostoje ne nje arraylist
        /// </summary>
        public bool shtoMetodeKostoje(clsMetodeKostoje metode)
            {
            base.Add(metode);
            if (base.Contains(metode))
                return true;
            else return false;
            }
        /// <summary>
        /// metoda per heqjen e nje obj clsMetodeKostoje ne nje arraylist
        /// </summary>
        public bool fshiMetodeKostoje(clsMetodeKostoje metode)
            {
            base.Remove(metode);
            if (base.Contains(metode))
                return false;
            else return true;
            }
        /// <summary>
        /// metoda per heqjen e nje obj clsMetodeKostoje ne nje arraylist
        /// </summary>
        public bool fshiGjitheMetodeKostoje()
            {
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
            }
        /// <summary>
        /// metoda per heqjen e nje obj clsMetodeKostoje ne nje arraylist
        /// </summary>
        public void fshiKeteMetodeKostoje(int index)
            {
            base.RemoveAt(index);
            }
        /// <summary>
        /// metoda shton nje obj te ri ne array list ne pozicionin e percaktuar nga index-i
        /// </summary>
        public void shtoLlojMakroNeIndeksin(int index, clsMetodeKostoje metode)
            {
            base.Insert(index, metode);
            }
        /// <summary>
        /// metoda gjen index-in ne liste ne te cilin ndodhet nje obj i caktuar
        /// </summary>
        public int indeksiMetodeKostoje(clsMetodeKostoje metode)
            {
            return base.IndexOf(metode);
            }
        /// <summary>
        /// metoda kontrollon nqs nje obj ndodhet ne arraylist
        /// </summary>
        public bool ekzistonMetodeKostoje(clsMetodeKostoje metode)
            {
            if (base.Contains(metode))
                return true;
            else return false;
            }
        /// <summary>
        /// metoda qe kthen nr e obj qe ka arraylist
        /// </summary>
        public int numriMetodeKostoje()
            {
            return base.Count;
            }

        public bool mbushGjitheMetodeKostoje()
        {
            clsDatabaseInventari dbMetodeKostoje = new clsDatabaseInventari();
            bool sukses = mbushMetodeKostoje(dbMetodeKostoje.ktheGjitheMetodeKostoje());
            dbMetodeKostoje.Dispose();
            return sukses;
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbInventari.clsMetodeKostoje"/> 
        /// </summary>
        private bool mbushMetodeKostoje(DataTable dt)
            {
            //try
            //    {

                foreach (DataRow rreshti in dt.Rows)
                    {
                    //clsMetodeKostoje MetodeKostoje = new clsMetodeKostoje();
                    //MetodeKostoje.mbushMetodeKostoje(rreshti);
                    this.Add(new clsMetodeKostoje(rreshti));
                    }

            //    }
            //catch (Exception)
            //    {
            //    return false;
            //    }
            return true;
            }

        #endregion
    
        }
    }
