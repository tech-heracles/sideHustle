using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;

namespace DbCore.DbInventari
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsLlojMakro
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colLlojeMakrosh : System.Collections.Generic.List<clsLlojMakro>
    {
        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbInventari.clsLlojMakro"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsLlojMakro this[int index]
        {
            get { return ((clsLlojMakro)base[index]); }
        }
        /// <summary>
        /// metoda per shtimin e nje obj clsLlojMakro ne nje arraylist
        /// </summary>
        public bool shtoLlojMakro(clsLlojMakro llojMakro)
        {
            base.Add(llojMakro);
            if (base.Contains(llojMakro))
                return true;
            else return false;
        }
        /// <summary>
        /// metoda per heqjen e nje obj clsLlojMakro ne nje arraylist
        /// </summary>
        public bool fshiLlojMakro(clsLlojMakro llojMakro)
        {
            base.Remove(llojMakro);
            if (base.Contains(llojMakro))
                return false;
            else return true;
        }
        /// <summary>
        /// metoda per heqjen e nje obj clsLlojMakro ne nje arraylist
        /// </summary>
        public bool fshiGjitheLlojeMakrosh()
        {
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// metoda per heqjen e nje obj clsLlojMakro ne nje arraylist
        /// </summary>
        public void fshiKeteLlojMakro(int index)
        {
            base.RemoveAt(index);
        }
        /// <summary>
        /// metoda shton nje obj te ri ne array list ne pozicionin e percaktuar nga index-i
        /// </summary>
        public void shtoLlojMakroNeIndeksin(int index, clsLlojMakro llojMakro)
        {
            base.Insert(index, llojMakro);
        }
        /// <summary>
        /// metoda gjen index-in ne liste ne te cilin ndodhet nje obj i caktuar
        /// </summary>
        public int indeksiLlojMakrot(clsLlojMakro llojMakro)
        {
            return base.IndexOf(llojMakro);
        }
        /// <summary>
        /// metoda kontrollon nqs nje obj ndodhet ne arraylist
        /// </summary>
        public bool ekzistonLlojMakro(clsLlojMakro llojMakro)
        {
            if (base.Contains(llojMakro))
                return true;
            return false;
        }
        /// <summary>
        /// metoda qe kthen nr e obj qe ka arraylist
        /// </summary>
        public int numriLlojeMakrosh()
        {
            return base.Count;
        }

        public bool mbushGjitheLlojeMakro()
        {
            clsDatabaseInventari dbLlojeMakro = new clsDatabaseInventari();
            bool sukses = mbushLlojeMakrosh(dbLlojeMakro.ktheGjitheLlojeMakrosh());
            dbLlojeMakro.Dispose();
            return sukses;
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbInventari.clsLlojMakro"/> 
        /// </summary>
        private bool mbushLlojeMakrosh(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsLlojMakro llojMakro = new clsLlojMakro();
                    //llojMakro.mbushLlojMakro(rreshti);
                    this.Add(new clsLlojMakro(rreshti));
                }

            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }

        #endregion
        [Obsolete("Perdor: bool mbushLlojeMakrosh(DataTable dt)", true)]
        public colLlojeMakrosh mbushArrayListLlojeMakrosh(DataSet ds)
        {
            colLlojeMakrosh llojeMakrosh = new colLlojeMakrosh();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsLlojMakro llojMakro = new clsLlojMakro();

                llojMakro.IdLlojMakro = int.Parse(rreshti[0].ToString());

                llojMakro.PershkrimLlojMakro = rreshti[1].ToString();

                llojeMakrosh.Add(llojMakro);
            }
            return llojeMakrosh;
        }
    }
}

