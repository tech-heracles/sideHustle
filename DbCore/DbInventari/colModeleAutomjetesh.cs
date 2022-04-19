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
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsModelAutomjeti
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colModeleAutomjetesh : System.Collections.Generic.List<clsModelAutomjeti>
    {
       #region Metoda Publike

        public colModeleAutomjetesh()
        {
        }

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbInventari.clsModelAutomjeti"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsModelAutomjeti this[int index]
        {
            get { return ((clsModelAutomjeti)base[index]); }
        }

        /// <summary>
        /// metoda per shtimin e nje obj clsModelAutomjeti ne nje arraylist
        /// </summary>
        public bool shtoModelAutomjeti(clsModelAutomjeti modelAutomjeti)
        {
            Add(modelAutomjeti);
            if (base.Contains(modelAutomjeti))
                return true;
            else return false;
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsModelAutomjeti ne nje arraylist
        /// </summary>
        public bool fshiModelAutomjeti(clsModelAutomjeti modelAutomjeti)
        {
            base.Remove(modelAutomjeti);
            if (base.Contains(modelAutomjeti))
                return false;
            else return true;
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsModelAutomjeti ne nje arraylist
        /// </summary>
        public bool fshiGjitheModeleAutomjetesh()
        {
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsModelAutomjeti ne nje arraylist
        /// </summary>
        public void fshiKeteModelAutomjeti(int index)
        {
            base.RemoveAt(index);
        }

        /// <summary>
        /// metoda shton nje obj te ri ne array list ne pozicionin e percaktuar nga index-i
        /// </summary>
        public void shtoModelAutomjetiNeIndeksin(int index, clsModelAutomjeti modelAutomjeti)
        {
            base.Insert(index, modelAutomjeti);
        }

        /// <summary>
        /// metoda gjen index-in ne liste ne te cilin ndodhet nje obj i caktuar
        /// </summary>
        public int indeksiModelitTeAutomjetit(clsModelAutomjeti modelAutomjeti)
        {
            return base.IndexOf(modelAutomjeti);
        }

        /// <summary>
        /// metoda kontrollon nqs nje obj ndodhet ne arraylist
        /// </summary>
        public bool ekzistonModelAutomjeti(clsModelAutomjeti modelAutomjeti)
        {
            if (base.Contains(modelAutomjeti))
                return true;
            else return false;
        }

        /// <summary>
        /// metoda qe kthen nr e obj qe ka arraylist
        /// </summary>
        public int numriModeleveTeAutomjeteve()
        {
            return base.Count;
        }

        public static DataTable merrModeleAutomjeteshSipasNdermarrjes(int idNdermarrje)
        {
            clsDatabaseInventari dbInventar = new clsDatabaseInventari();
            DataTable table = dbInventar.merrModeleAutomjeteshSipasNdermarrjes(idNdermarrje);
            dbInventar.Dispose();
            return table;
        }

        public static DataTable merrModeleAutomjeteshSipasNdermarrjesPerKombo(string filter, long startIndex, long endIndex, int idNdermarrje)
        {
            clsDatabaseInventari dbInventar = new clsDatabaseInventari();
            DataTable table = dbInventar.merrModelAutomjetiSipasNdermarrjeDheKoditPerKombo(filter, startIndex, endIndex, idNdermarrje);
            dbInventar.Dispose();
            return table;
        }

        /// <summary>
        /// metode per mbushjen e nje koleksioni me automjetet e ndermarrjes
        /// </summary>
        /// <param name="idNdermarrje">idNdermarrje</param>        
        /// <returns>kthen true nese mbushet koleksioni me sukses</returns>
        public bool mbushModeleAutomjeteshSipasNdermarrjes(int idNdermarrje)
        {
            clsDatabaseInventari dbInventar = new clsDatabaseInventari();
            bool sukses = mbushModeleAutomjetesh(dbInventar.merrModeleAutomjeteshSipasNdermarrjes(idNdermarrje));
            dbInventar.Dispose();
            return sukses;
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// Metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera nga nje stored procedure.
        /// </summary>
        /// <param name="dt">Merr si parameter nje DataTable.</param>
        /// <returns>Kthen True nese kryhet me sukses, ne te kundert False.</returns>       
        private bool mbushModeleAutomjetesh(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsModelAutomjeti modelAutomjeti = new clsModelAutomjeti();
                    //modelAutomjeti.mbushModelAutomjeti(rreshti);
                    this.Add(new clsModelAutomjeti(rreshti));
                }
            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }

        #endregion

    }
}
