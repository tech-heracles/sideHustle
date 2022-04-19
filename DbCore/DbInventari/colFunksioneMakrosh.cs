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
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsFunksionMakro
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colFunksioneMakrosh : System.Collections.Generic.List<clsFunksionMakro>
    {
        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbInventari.clsFunksionMakro"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsFunksionMakro this[int index]
        {
            get { return ((clsFunksionMakro)base[index]); }
        }
        /// <summary>
        /// metoda per shtimin e nje obj clsFunksionMakro ne nje arraylist
        /// </summary>
        public bool shtoFunksionMakro(clsFunksionMakro funksionMakro)
        {
            base.Add(funksionMakro);
            if (base.Contains(funksionMakro))
                return true;
            else return false;
        }
        /// <summary>
        /// metoda per heqjen e nje obj clsFunksionMakro ne nje arraylist
        /// </summary>
        public bool fshiFunksionMakro(clsFunksionMakro funksionMakro)
        {
            base.Remove(funksionMakro);
            if (base.Contains(funksionMakro))
                return false;
            else return true;
        }
        /// <summary>
        /// metoda per heqjen e nje obj clsFunksionMakro ne nje arraylist
        /// </summary>
        public bool fshiGjitheFunksioneMakrosh()
        {
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// metoda per heqjen e nje obj clsFunksionMakro ne nje arraylist
        /// </summary>
        public void fshiKeteFunksionMakro(int index)
        {
            base.RemoveAt(index);
        }
        /// <summary>
        /// metoda shton nje obj te ri ne array list ne pozicionin e percaktuar nga index-i
        /// </summary>
        public void shtoFunksionMakroNeIndeksin(int index, clsFunksionMakro funksionMakro)
        {
            base.Insert(index, funksionMakro);
        }
        /// <summary>
        /// metoda gjen index-in ne liste ne te cilin ndodhet nje obj i caktuar
        /// </summary>
        public int indeksiFunksionMakrot(clsFunksionMakro funksionMakro)
        {
            return base.IndexOf(funksionMakro);
        }
        /// <summary>
        /// metoda kontrollon nqs nje obj ndodhet ne arraylist
        /// </summary>
        public bool ekzistonFunksionMakro(clsFunksionMakro funksionMakro)
        {
            if (base.Contains(funksionMakro))
                return true;
            else return false;
        }
        /// <summary>
        /// metoda qe kthen nr e obj qe ka arraylist
        /// </summary>
        public int numriFunksioneMakrosh()
        {
            return base.Count;
        }

        /// <summary>
        /// mbush gjithe funksionet makro
        /// </summary>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        public bool mbushGjitheFunksioneMakrosh()
        {
            clsDatabaseInventari dbFunksioneMakrosh = new clsDatabaseInventari();
            return mbushFunksioneMakrosh(dbFunksioneMakrosh.ktheGjitheFunksioneMakrosh());
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje dataTable
        ///  <see cref="DbCore.DbInventari.clsFunksionMakro"/> 
        /// </summary>
        private bool mbushFunksioneMakrosh(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsFunksionMakro funksionMakro = new clsFunksionMakro();
                    //funksionMakro.mbushFunksionMakro(rreshti);
                    this.Add(new clsFunksionMakro(rreshti));
                }

            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }

        #endregion
        [Obsolete("Perdor: bool mbushFunksioneMakrosh(DataTable dt)", true)]
        public colFunksioneMakrosh mbushArrayListFunksioneMakrosh(DataSet ds)
        {
            colFunksioneMakrosh funksioneMakrosh = new colFunksioneMakrosh();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsFunksionMakro funksionMakro = new clsFunksionMakro();

                funksionMakro.IdFunksionMakro = int.Parse(rreshti[0].ToString());

                funksionMakro.PershkrimFunksionMakro = rreshti[1].ToString();

                funksioneMakrosh.Add(funksionMakro);
            }
            return funksioneMakrosh;
        }
    }
}

