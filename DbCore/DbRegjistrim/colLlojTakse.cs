using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsLlojTakse
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colLlojTakse : System.Collections.Generic.List<clsLlojTakse>
    {
        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbRegjistrim.clsLlojTakse"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsLlojTakse this[int index]
        {
            get { return ((clsLlojTakse)base[index]); }
        }

        /// <summary>
        /// metoda per shtimin e nje obj clsLlojTakse ne nje arraylist
        /// </summary>
        public bool shtoLlojTakse(clsLlojTakse llojTakse)
        {
            base.Add(llojTakse);
            if (base.Contains(llojTakse))
                return true;
            else return false;
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsLlojTakse ne nje arraylist
        /// </summary>
        public bool fshiLlojTakse(clsLlojTakse llojTakse)
        {
            base.Remove(llojTakse);
            if (base.Contains(llojTakse))
                return false;
            else return true;
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsLlojTakse ne nje arraylist
        /// </summary>
        public bool fshiGjitheLlojeTaksash()
        {
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsLlojTakse ne nje arraylist
        /// </summary>
        public void fshiKeteLlojTakse(int index)
        {
            base.RemoveAt(index);
        }

        /// <summary>
        /// metoda shton nje obj te ri ne array list ne pozicionin e percaktuar nga index-i
        /// </summary>
        public void shtoLlojTakseNeIndeksin(int index, clsLlojTakse llojTakse)
        {
            base.Insert(index, llojTakse);
        }

        /// <summary>
        /// metoda gjen index-in ne liste ne te cilin ndodhet nje obj i caktuar
        /// </summary>
        public int indeksiLlojTakset(clsLlojTakse llojTakse)
        {
            return base.IndexOf(llojTakse);
        }

        /// <summary>
        /// metoda kontrollon nqs nje obj ndodhet ne arraylist
        /// </summary>
        public bool ekzistonLlojTakse(clsLlojTakse llojTakse)
        {
            if (base.Contains(llojTakse))
                return true;
            else return false;
        }

        /// <summary>
        /// metoda qe kthen nr e obj qe ka arraylist
        /// </summary>
        public int numriLlojeTaksesh()
        {
            return base.Count;
        }

        /// <summary>
        /// mbush gjithe llojet e taksave
        /// </summary>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        public bool mbushGjitheLlojeTaksash()
        {
            clsDatabaseRegjistrim dbLlojeTaksash = new clsDatabaseRegjistrim();
            bool mbush = mbushLlojTaksash(dbLlojeTaksash.ktheGjitheLlojeTaksash());
            dbLlojeTaksash.Dispose();
            return mbush;
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbRegjistrim.clsLlojTakse"/> 
        /// </summary>
        private bool mbushLlojTaksash(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsLlojTakse llojTakse = new clsLlojTakse();
                    //llojTakse.mbushLlojTakse(rreshti);
                    Add(new clsLlojTakse(rreshti));
                }

            //}
            //catch (Exception)
            //{
            //    return false;
            //    //throw;
            //}
            return true;
        }

        #endregion
        [Obsolete("Perdor: bool mbushLlojTaksash(DataTable dt)", true)]
        public colLlojTakse mbushArrayListLlojTaksash(DataSet ds)
        {
            colLlojTakse llojeTaksesh = new colLlojTakse();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsLlojTakse llojTakse = new clsLlojTakse();

                llojTakse.IdLlojTakse = int.Parse(rreshti[0].ToString());

                llojTakse.Pershkrim = rreshti[1].ToString();

                llojeTaksesh.Add(llojTakse);
            }
            return llojeTaksesh;
        }
    }
}
