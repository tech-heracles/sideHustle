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
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsAutomjete
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colAutomjete : System.Collections.Generic.List<clsAutomjete>
    {
        #region Metoda Publike

        public colAutomjete()
        {
        }

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbInventari.clsAutomjete"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsAutomjete this[int index]
        {
            get { return ((clsAutomjete)base[index]); }
        }

        /// <summary>
        /// metoda per shtimin e nje obj clsAutomjete ne nje arraylist
        /// </summary>
        public bool shtoAutomjet(clsAutomjete automjet)
        {
            Add(automjet);
            if (base.Contains(automjet))
                return true;
            else return false;
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsAutomjete ne nje arraylist
        /// </summary>
        public bool fshiAutomjet(clsAutomjete automjet)
        {
            base.Remove(automjet);
            if (base.Contains(automjet))
                return false;
            else return true;
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsAutomjete ne nje arraylist
        /// </summary>
        public bool fshiGjitheArtikujt()
        {
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsAutomjete ne nje arraylist
        /// </summary>
        public void fshiKeteAutomjet(int index)
        {
            base.RemoveAt(index);
        }

        /// <summary>
        /// metoda shton nje obj te ri ne array list ne pozicionin e percaktuar nga index-i
        /// </summary>
        public void shtoAutomjetinNeIndeksin(int index, clsAutomjete automjet)
        {
            base.Insert(index, automjet);
        }

        /// <summary>
        /// metoda gjen index-in ne liste ne te cilin ndodhet nje obj i caktuar
        /// </summary>
        public int indeksiAutomjetit(clsAutomjete automjet)
        {
            return base.IndexOf(automjet);
        }

        /// <summary>
        /// metoda kontrollon nqs nje obj ndodhet ne arraylist
        /// </summary>
        public bool ekzistonAutomjet(clsAutomjete automjet)
        {
            if (base.Contains(automjet))
                return true;
            else return false;
        }

        /// <summary>
        /// metoda qe kthen nr e obj qe ka arraylist
        /// </summary>
        public int numriAutomjeteve()
        {
            return base.Count;
        }

        public static DataTable merrAutomjetetSipasNdermarrjes(int idNdermarrje)
        {
            clsDatabaseInventari dbInventar = new clsDatabaseInventari();
            DataTable table = dbInventar.merrAutomjetSipasNdermarrjes(idNdermarrje);
            dbInventar.Dispose();
            return table;
        }

        public static DataTable merrAutomjetetSipasNdermarrjesDheKlientit(int idNdermarrje, int idKlienti)
        {
            clsDatabaseInventari dbInventar = new clsDatabaseInventari();
            DataTable table = dbInventar.merrAutomjetSipasNdermarrjesDheKlientit(idNdermarrje, idKlienti);
            dbInventar.Dispose();
            return table;
        }

        public static DataTable merrAutomjetetSipasNdermarrjesDheKlientitPerKombo(int idNdermarrje, int idKlienti, string filter, long startIndex, long endIndex)
        {
            clsDatabaseInventari dbInventar = new clsDatabaseInventari();
            DataTable table = dbInventar.merrAutomjetSipasNdermarrjesDheKlientitPerKombo(idNdermarrje, idKlienti, filter, startIndex, endIndex);
            dbInventar.Dispose();
            return table;
        }

        public static DataTable merrAutomjetetSipasNdermarrjesPerKombo(int idNdermarrje, string filter, long startIndex, long endIndex)
        {
            clsDatabaseInventari dbInventar = new clsDatabaseInventari();
            DataTable table = dbInventar.merrAutomjetSipasNdermarrjesPerKombo(idNdermarrje, filter, startIndex, endIndex);
            dbInventar.Dispose();
            return table;
        }

        /// <summary>
        /// metode per mbushjen e nje koleksioni me automjetet e ndermarrjes
        /// </summary>
        /// <param name="idNdermarrje">idNdermarrje</param>        
        /// <returns>kthen true nese mbushet koleksioni me sukses</returns>
        public bool mbushAutomjetetSipasNdermarrjes(int idNdermarrje)
        {
            clsDatabaseInventari dbInventar = new clsDatabaseInventari();
            bool sukses = mbushAutomjetet(dbInventar.merrAutomjetSipasNdermarrjes(idNdermarrje));
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
        /// <param name="dbInventari"></param>
        private bool mbushAutomjetet(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsAutomjete automjet = new clsAutomjete();
                    //automjet.mbushAutomjet(rreshti);
                    this.Add(new clsAutomjete(rreshti));
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