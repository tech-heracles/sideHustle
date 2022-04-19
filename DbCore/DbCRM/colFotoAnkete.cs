using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbCRM
{
    class colFotoAnkete: System.Collections.Generic.List<clsFotoAnkete>
    {
        #region Konstruktore

        /// <summary>
        /// konstruktor bosh
        /// </summary>
        public colFotoAnkete()
        {
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbCRM.clsFotoAnkete"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsFotoAnkete this[int index]
        {
            get { return ((clsFotoAnkete)base[index]); }
        }

        /// <summary>
        /// metoda per shtimin e nje obj clsFotoAnkete ne nje arraylist
        /// </summary>
        public bool shtoFotoAnkete(clsFotoAnkete FotoAnkete)
        {
            base.Add(FotoAnkete);
            if (base.Contains(FotoAnkete))
                return true;
            else return false;
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsFotoAnkete ne nje arraylist
        /// </summary>
        public bool fshiFotoAnkete(clsFotoAnkete FotoAnkete)
        {
            base.Remove(FotoAnkete);
            if (base.Contains(FotoAnkete))
                return false;
            else return true;
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsFotoAnkete ne nje arraylist
        /// </summary>
        public bool fshiGjitheFotoAnkete()
        {
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsFotoAnkete ne nje arraylist
        /// </summary>
        public void fshiKeteFotoAnkete(int index)
        {
            base.RemoveAt(index);
        }

        /// <summary>
        /// metoda shton nje obj te ri ne array list ne pozicionin e percaktuar nga index-i
        /// </summary>
        public void shtoFotoAnketeNeIndeksin(int index, clsFotoAnkete FotoAnkete)
        {
            base.Insert(index, FotoAnkete);
        }

        /// <summary>
        /// metoda gjen index-in ne liste ne te cilin ndodhet nje obj i caktuar
        /// </summary>
        public int indeksiFotoAnkete(clsFotoAnkete FotoAnkete)
        {
            return base.IndexOf(FotoAnkete);
        }

        /// <summary>
        /// metoda kontrollon nqs nje obj ndodhet ne arraylist
        /// </summary>
        public bool ekzistonFotoAnkete(clsFotoAnkete FotoAnkete)
        {
            if (base.Contains(FotoAnkete))
                return true;
            else return false;
        }

        /// <summary>
        /// metoda qe kthen nr e obj qe ka arraylist
        /// </summary>
        public int numriFotoAnkete()
        {
            return base.Count;
        }

   

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbCRM.clsFotoAnkete"/> 
        /// </summary>
        private bool mbushFotoAnkete(DataTable dt)
        {
            try
            {
                foreach (DataRow rreshti in dt.Rows)
                {
                    clsFotoAnkete FotoAnkete = new clsFotoAnkete();
                    FotoAnkete.mbushFotoAnketa(rreshti);
                    this.Add(FotoAnkete);
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
