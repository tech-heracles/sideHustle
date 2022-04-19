using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace DbCore.DbInventari
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsKlasaArtikulli
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colKlasaArtikulli : System.Collections.Generic.List<clsKlasaArtikulli>
    {

        #region Konstruktoret

    

        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public colKlasaArtikulli()
        {
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbInventari.clsKlasaArtikulli"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsKlasaArtikulli this[int index]
        {
            get { return ((clsKlasaArtikulli)base[index]); }
        }
        /// <summary>
        /// metoda per shtimin e nje obj clsKlasaArtikulli ne nje arraylist
        /// </summary>
        public bool shtoKlasaArtikulli(clsKlasaArtikulli klasa)
        {
            base.Add(klasa);
            if (base.Contains(klasa))
                return true;
            else return false;
        }
        /// <summary>
        /// metoda per heqjen e nje obj clsKlasaArtikulli ne nje arraylist
        /// </summary>
        public bool fshiKlasaArtikulli(clsKlasaArtikulli klasa)
        {
            base.Remove(klasa);
            if (base.Contains(klasa))
                return false;
            else return true;
        }
        /// <summary>
        /// metoda per heqjen e nje obj clsKlasaArtikulli ne nje arraylist
        /// </summary>
        public bool fshiGjitheKlasaArtikulli()
        {
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// metoda per heqjen e nje obj clsKlasaArtikulli ne nje arraylist
        /// </summary>
        public void fshiKeteKlaseArtikulli(int index)
        {
            base.RemoveAt(index);
        }
        /// <summary>
        /// metoda shton nje obj te ri ne array list ne pozicionin e percaktuar nga index-i
        /// </summary>
        public void shtoKlaseArtikulliNeIndeksin(int index, clsKlasaArtikulli klasa)
        {
            base.Insert(index, klasa);
        }
        /// <summary>
        /// metoda gjen index-in ne liste ne te cilin ndodhet nje obj i caktuar
        /// </summary>
        public int indeksiKlasaArtikullit(clsKlasaArtikulli klasa)
        {
            return base.IndexOf(klasa);
        }
        /// <summary>
        /// metoda kontrollon nqs nje obj ndodhet ne arraylist
        /// </summary>
        public bool ekzistonKlasaArtikullit(clsKlasaArtikulli klasa)
        {
            if (base.Contains(klasa))
                return true;
            else return false;
        }
        /// <summary>
        /// metoda qe kthen nr e obj qe ka arraylist
        /// </summary>
        public int numriKlasaveArtikullit()
        {
            return base.Count;
        }

        /// <summary>
        /// mbush artikullin me te gjitha klasat e tij
        /// </summary>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kunder false</returns>
        public bool mbushGjitheKlasaArtikulli()
        {
            clsDatabaseInventari dbKlasaArtikulli = new clsDatabaseInventari();
            return mbushKlasaArtikulli(dbKlasaArtikulli.ktheGjitheKlasaArtikulli());
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbInventari.clsKlasaArtikulli"/> 
        /// </summary>
        private bool mbushKlasaArtikulli(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsKlasaArtikulli klasa = new clsKlasaArtikulli();
                    //klasa.mbushKlasaArtikull(rreshti);
                    this.Add(new clsKlasaArtikulli(rreshti));
                }

            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }

        #endregion
        [Obsolete("Perdor: bool mbushKlasaArtikulli(DataTable dt)", true)]
        public colKlasaArtikulli mbushArrayListKlasaArtikulli(DataSet ds)
        {
            colKlasaArtikulli klasaartikulli = new colKlasaArtikulli();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsKlasaArtikulli klasa = new clsKlasaArtikulli();

                klasa.IdKlasa = int.Parse(rreshti[0].ToString());
                klasa.PershkrimKlasa = rreshti[1].ToString();


                klasaartikulli.Add(klasa);
            }
            return klasaartikulli;
        }
    }
}
