using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using DbCore.DbKontabiliteti;

namespace DbCore.DbInventari
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsKokaMakro
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colKokatMakro : System.Collections.Generic.List<clsKokaMakro>
    {
        #region Konstruktoret

        /// <summary>
        /// konstruktor pa parametra
        /// </summary>
        public colKokatMakro()
        {
        }

        /// <summary>
        /// konstruktor me 2 parametra
        /// </summary>
        /// <param name="idndermarje">id e ndermarrjes</param>
        /// <param name="idperdorues">id e perdoruesit</param>
        public colKokatMakro(int idndermarje, int idperdorues)
        {
            clsDatabaseInventari dbKokaMakro = new clsDatabaseInventari();
            mbushKokaMakrosh(dbKokaMakro.ktheMakroSipasNdermarrjesAndAutorizime(idndermarje, idperdorues));
            dbKokaMakro.Dispose();
        }

        /// <summary>
        /// konstruktor me 3 parametra
        /// </summary>
        /// <param name="idndermarje">id e ndermarrjes</param>
        /// <param name="idperdorues">id e perdoruesit</param>
        /// <param name="kodi">kodi</param>
        public colKokatMakro(int idndermarje, int idperdorues, string kodi)
        {
            clsDatabaseInventari dbKokaMakro = new clsDatabaseInventari();
            mbushKokaMakrosh(dbKokaMakro.ktheMakroSipasNdermarrjesAndAutorizimeLike(idndermarje, idperdorues, kodi));
            dbKokaMakro.Dispose();
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbInventari.clsKokaMakro"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsKokaMakro this[int index]
        {
            get { return ((clsKokaMakro)base[index]); }
        }
        /// <summary>
        /// metoda per shtimin e nje obj clsKokaMakro ne nje arraylist
        /// </summary>
        public bool shtoKokaMakro(clsKokaMakro kokaMakro)
        {
            base.Add(kokaMakro);
            if (base.Contains(kokaMakro))
                return true;
            else return false;
        }
        /// <summary>
        /// metoda per heqjen e nje obj clsKokaMakro ne nje arraylist
        /// </summary>
        public bool fshiKokaMakro(clsKokaMakro kokaMakro)
        {
            base.Remove(kokaMakro);
            if (base.Contains(kokaMakro))
                return false;
            else return true;
        }
        /// <summary>
        /// metoda per heqjen e nje obj clsKokaMakro ne nje arraylist
        /// </summary>
        public bool fshiGjitheKokatMakro()
        {
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// metoda per heqjen e nje obj clsKokaMakro ne nje arraylist
        /// </summary>
        public void fshiKeteKokaMakro(int index)
        {
            base.RemoveAt(index);
        }
        /// <summary>
        /// metoda shton nje obj te ri ne array list ne pozicionin e percaktuar nga index-i
        /// </summary>
        public void shtoKokaMakroNeIndeksin(int index, clsKokaMakro kokaMakro)
        {
            base.Insert(index, kokaMakro);
        }
        /// <summary>
        /// metoda gjen index-in ne liste ne te cilin ndodhet nje obj i caktuar
        /// </summary>
        public int indeksiKokaMakrot(clsKokaMakro kokaMakro)
        {
            return base.IndexOf(kokaMakro);
        }
        /// <summary>
        /// metoda kontrollon nqs nje obj ndodhet ne arraylist
        /// </summary>
        public bool ekzistonKokaMakro(clsKokaMakro kokaMakro)
        {
            if (base.Contains(kokaMakro))
                return true;
            else return false;
        }
        /// <summary>
        /// metoda qe kthen nr e obj qe ka arraylist
        /// </summary>
        public int numriKokaMakrove()
        {
            return base.Count;
        }

        #endregion

        #region Metoda private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbInventari.clsKokaMakro"/> 
        /// </summary>
        private bool mbushKokaMakrosh(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsKokaMakro kokaMakro = new clsKokaMakro();
                    //kokaMakro.mbushKokaMakro(rreshti);
                    this.Add(new clsKokaMakro(rreshti));
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
