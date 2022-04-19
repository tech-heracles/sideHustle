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
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsTransportues
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colTransportues : System.Collections.Generic.List<clsTransportues>
    {
        #region Metoda Publike

        public colTransportues()
        {
        }


        public colTransportues(int idndermarje)
        {
            clsDatabaseInventari data = new clsDatabaseInventari();
            mbushTransportuesit(data.ktheGjitheTransportuesit(idndermarje));
            data.Dispose();
        }

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbInventari.clsTransportues"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsTransportues this[int index]
        {
            get { return ((clsTransportues)base[index]); }
        }

        /// <summary>
        /// metoda per shtimin e nje obj clsTransportues ne nje arraylist
        /// </summary>
        public bool shtoTransportues(clsTransportues transportues)
        {
            Add(transportues);
            if (base.Contains(transportues))
                return true;
            else return false;
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsTransportues ne nje arraylist
        /// </summary>
        public bool fshiTransportues(clsTransportues transportues)
        {
            base.Remove(transportues);
            if (base.Contains(transportues))
                return false;
            else return true;
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsTransportues ne nje arraylist
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
        /// metoda per heqjen e nje obj clsTransportues ne nje arraylist
        /// </summary>
        public void fshiKeteTransportues(int index)
        {
            base.RemoveAt(index);
        }

        /// <summary>
        /// metoda shton nje obj te ri ne array list ne pozicionin e percaktuar nga index-i
        /// </summary>
        public void shtoTransportuesinNeIndeksin(int index, clsTransportues transportues)
        {
            base.Insert(index, transportues);
        }

        /// <summary>
        /// metoda gjen index-in ne liste ne te cilin ndodhet nje obj i caktuar
        /// </summary>
        public int indeksiTransportuesit(clsTransportues transportues)
        {
            return base.IndexOf(transportues);
        }

        /// <summary>
        /// metoda kontrollon nqs nje obj ndodhet ne arraylist
        /// </summary>
        public bool ekzistonTransportues(clsTransportues transportues)
        {
            if (base.Contains(transportues))
                return true;
            else return false;
        }

        /// <summary>
        /// metoda qe kthen nr e obj qe ka arraylist
        /// </summary>
        public int numriTransportuesve()
        {
            return base.Count;
        }

        public static DataTable merrTransportuesSipasNdermarrjes(int idNdermarrje, bool lupe)
        {
            clsDatabaseInventari dbInventar = new clsDatabaseInventari();
            var table = new DataTable();
            if (lupe)
             table = dbInventar.merrTransportuesSipasNdermarrjesLupe(idNdermarrje);
            else
             table = dbInventar.merrTransportuesSipasNdermarrjes(idNdermarrje);
            dbInventar.Dispose();
            return table;
        }
        public static DataTable merrTransportuesSipasNdermarrjesDtSmall(int idNdermarrje)
        {
            using (clsDatabaseInventari dbInventar = new clsDatabaseInventari())
            {
                return dbInventar.merrTransportuesSipasNdermarrjesDtSmall(idNdermarrje);
            }
        }

        /// <summary>
        /// metode per mbushjen e nje koleksioni me transportuesit e ndermarrjes
        /// </summary>
        /// <param name="idNdermarrje">idNdermarrje</param>        
        /// <returns>kthen true nese mbushet koleksioni me sukses</returns>
        public bool mbushTransportuesitSipasNdermarrjes(int idNdermarrje)
        {
            clsDatabaseInventari dbInventar = new clsDatabaseInventari();
            bool sukses = mbushTransportuesit(dbInventar.merrTransportuesSipasNdermarrjes(idNdermarrje));
            dbInventar.Dispose();
            return sukses;
        }


        public static DataTable ktheTransportuesSipasNdermMeFilter(string filter, long startIndex, long endIndex, int idnderm)
        {
            clsDatabaseInventari dbInventar = new clsDatabaseInventari();
            DataTable tabela = dbInventar.merrTransportuesSipasNdermarrjesMeFilter(filter, startIndex, endIndex, idnderm);
            dbInventar.Dispose();
            return tabela;
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// Metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera nga nje stored procedure.
        /// </summary>
        /// <param name="dt">Merr si parameter nje DataTable.</param>
        /// <returns>Kthen True nese kryhet me sukses, ne te kundert False.</returns>
        /// <param name="dbInventari"></param>
        private bool mbushTransportuesit(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsTransportues transportues = new clsTransportues();
                    //transportues.mbushTransportues(rreshti);
                    this.Add(new clsTransportues(rreshti));
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