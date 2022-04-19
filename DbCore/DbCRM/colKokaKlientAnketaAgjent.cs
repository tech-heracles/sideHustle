using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbCRM
{
    public class colKokaKlientAnketaAgjent : System.Collections.Generic.List<clsKokaKlientAnketaAgjent>
    {
        #region Konstruktore

        /// <summary>
        /// konstruktor bosh
        /// </summary>
        public colKokaKlientAnketaAgjent()
        {
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbCRM.clsKokaKlientAnketaAgjent"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsKokaKlientAnketaAgjent this[int index]
        {
            get { return ((clsKokaKlientAnketaAgjent)base[index]); }
        }

        /// <summary>
        /// metoda per shtimin e nje obj clsKokaKlientAnketaAgjent ne nje arraylist
        /// </summary>
        public bool shtokokaAnkete(clsKokaKlientAnketaAgjent kokaKlientAnketaAgjent)
        {
            base.Add(kokaKlientAnketaAgjent);
            if (base.Contains(kokaKlientAnketaAgjent))
                return true;
            else return false;
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsKokaKlientAnketaAgjent ne nje arraylist
        /// </summary>
        public bool fshikokaKlientAnketaAgjent(clsKokaKlientAnketaAgjent kokaKlientAnketaAgjent)
        {
            base.Remove(kokaKlientAnketaAgjent);
            if (base.Contains(kokaKlientAnketaAgjent))
                return false;
            else return true;
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsKokaKlientAnketaAgjent ne nje arraylist
        /// </summary>
        public bool fshiGjithekokaKlientAnketaAgjent()
        {
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsKokaKlientAnketaAgjent ne nje arraylist
        /// </summary>
        public void fshiKetekokaKlientAnketaAgjent(int index)
        {
            base.RemoveAt(index);
        }

        /// <summary>
        /// metoda shton nje obj te ri ne array list ne pozicionin e percaktuar nga index-i
        /// </summary>
        public void shtokokaKlientAnketaAgjentNeIndeksin(int index, clsKokaKlientAnketaAgjent kokaKlientAnketaAgjent)
        {
            base.Insert(index, kokaKlientAnketaAgjent);
        }

        /// <summary>
        /// metoda gjen index-in ne liste ne te cilin ndodhet nje obj i caktuar
        /// </summary>
        public int indeksikokaKlientAnketaAgjent(clsKokaKlientAnketaAgjent kokaKlientAnketaAgjent)
        {
            return base.IndexOf(kokaKlientAnketaAgjent);
        }

        /// <summary>
        /// metoda kontrollon nqs nje obj ndodhet ne arraylist
        /// </summary>
        public bool ekzistonkokaKlientAnketaAgjent(clsKokaKlientAnketaAgjent kokaKlientAnketaAgjent)
        {
            if (base.Contains(kokaKlientAnketaAgjent))
                return true;
            else return false;
        }

        /// <summary>
        /// metoda qe kthen nr e obj qe ka arraylist
        /// </summary>
        public int numrikokaKlientAnketaAgjente()
        {
            return base.Count;
        }


        public static DataTable merrKlientAnketaAgjentSipasNdermjes(int indermarje, string datanga, string dataderi, int idPerdorues)
        {
            using (clsDatabaseCRM db = new clsDatabaseCRM())
            {
                return db.merrKlientAnketaAgjentSipasNdermjes(indermarje,datanga,dataderi, idPerdorues);
            }
        }
        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbCRM.clsKokaKlientAnketaAgjent"/> 
        /// </summary>
        private bool mbushkokaKlientAnketaAgjent(DataTable dt)
        {
            try
            {
                foreach (DataRow rreshti in dt.Rows)
                {
                    clsKokaKlientAnketaAgjent kokaAnkete = new clsKokaKlientAnketaAgjent();
                    kokaAnkete.mbushKokaKlientAnketaAgjent(rreshti);
                    this.Add(kokaAnkete);
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
