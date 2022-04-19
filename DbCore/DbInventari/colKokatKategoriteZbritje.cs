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
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsKokaKategoriZbritje
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colKokatKategoriteZbritjes : System.Collections.Generic.List<clsKokaKategoriZbritje>
    {

        #region Konstruktoret

        /// <summary>
        /// konstruktor pa parametra
        /// </summary>
        public colKokatKategoriteZbritjes()
        {

        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idnderm">id e ndermarrjes</param>
        public colKokatKategoriteZbritjes(int idnderm)
        {
            clsDatabaseInventari dbKokatKategoriteZbritje = new clsDatabaseInventari();
            mbushKokaKategoriZbritje(dbKokatKategoriteZbritje.ktheKokaKategoriZbritjeSipasNdermarrjes(idnderm));
            dbKokatKategoriteZbritje.Dispose();
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbInventari.clsKokaKategoriZbritje"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsKokaKategoriZbritje this[int index]
        {
            get { return ((clsKokaKategoriZbritje)base[index]); }
        }
        /// <summary>
        /// metoda per shtimin e nje obj clsKokaKategoriZbritje ne nje arraylist
        /// </summary>
        public bool shtoKokaKategoriZbritje(clsKokaKategoriZbritje kategoriZbritje)
        {
            base.Add(kategoriZbritje);
            if (base.Contains(kategoriZbritje))
                return true;
            else return false;
        }
        /// <summary>
        /// metoda per heqjen e nje obj clsKokaKategoriZbritje ne nje arraylist
        /// </summary>
        public bool fshiKokaKategoriZbritje(clsKokaKategoriZbritje kategoriZbritje)
        {
            base.Remove(kategoriZbritje);
            if (base.Contains(kategoriZbritje))
                return false;
            else return true;
        }
        /// <summary>
        /// metoda per heqjen e nje obj clsKokaKategoriZbritje ne nje arraylist
        /// </summary>
        public bool fshiGjitheKokatKategoriteZbritjes()
        {
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// metoda per heqjen e nje obj clsKokaKategoriZbritje ne nje arraylist
        /// </summary>
        public void fshiKeteKokaKategoriZbritje(int index)
        {
            base.RemoveAt(index);
        }
        /// <summary>
        /// metoda shton nje obj te ri ne array list ne pozicionin e percaktuar nga index-i
        /// </summary>
        public void shtoKokaKategoriZbritjeNeIndeksin(int index, clsKokaKategoriZbritje kategoriZbritje)
        {
            base.Insert(index, kategoriZbritje);
        }
        /// <summary>
        /// metoda gjen index-in ne liste ne te cilin ndodhet nje obj i caktuar
        /// </summary>
        public int indeksiKokaKategoriZbritjes(clsKokaKategoriZbritje kategoriZbritje)
        {
            return base.IndexOf(kategoriZbritje);
        }
        /// <summary>
        /// metoda kontrollon nqs nje obj ndodhet ne arraylist
        /// </summary>
        public bool ekzistonKokaKategoriZbritje(clsKokaKategoriZbritje kategoriZbritje)
        {
            if (base.Contains(kategoriZbritje))
                return true;
            return false;
        }
        /// <summary>
        /// metoda qe kthen nr e obj qe ka arraylist
        /// </summary>
        public int numriKokaveKategoriveZbritje()
        {
            return base.Count;
        }
        public static DataRow merrSipasKategoriNdermarrjesDR(int idnderm, int idperdorues, int idkategori)
        {
            clsDatabaseInventari dbartikuj = new clsDatabaseInventari();
            DataRow rreshti = dbartikuj.merrSipasKategoriNdermarrjesDR(idnderm, idperdorues, idkategori);
            dbartikuj.Dispose();
            return rreshti;

        }
        public static DataTable merrSipasKategoriteNdermarrjesDT(int idnderm, int idperdorues)
        {
            clsDatabaseInventari dbartikuj = new clsDatabaseInventari();
            DataTable tabela = dbartikuj.merrSipasKategoriteNdermarrjesDT(idnderm, idperdorues);
            dbartikuj.Dispose();
            return tabela;
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbInventari.clsKokaKategoriZbritje"/> 
        /// </summary>
        private bool mbushKokaKategoriZbritje(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsKokaKategoriZbritje kategoriZbritje = new clsKokaKategoriZbritje();
                    //kategoriZbritje.mbushKokaKategoriZbritje(rreshti);
                    this.Add(new clsKokaKategoriZbritje(rreshti));
                }

            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }

        #endregion
        [Obsolete("Perdor: bool mbushKokaKategoriZbritje(DataTable dt)", true)]
        public colKokatKategoriteZbritjes mbushArrayListKokashKategorishZbritje(DataSet ds)
        {
            colKokatKategoriteZbritjes kategoriteZbritje = new colKokatKategoriteZbritjes();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsKokaKategoriZbritje kategoriZbritje = new clsKokaKategoriZbritje();

                kategoriZbritje.IdKokaKategoriZbritje = int.Parse(rreshti[0].ToString());
                kategoriZbritje.KodKategoriZbritje = rreshti[1].ToString();
                kategoriZbritje.PershkrimKategoriZbritje = rreshti[2].ToString();              
                kategoriZbritje.IdPerdoruesi = int.Parse(rreshti[3].ToString());
                //kategoriZbritje.IdNderViti = int.Parse(rreshti[4].ToString());
                kategoriZbritje.Zbritja = decimal.Parse(rreshti[4].ToString());
                kategoriZbritje.IdNdermarje = int.Parse(rreshti[5].ToString());
                kategoriteZbritje.Add(kategoriZbritje);
            }
            return kategoriteZbritje;
        }

    }
}
