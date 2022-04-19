using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbInventari
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsKategoriDetajimArtikulli
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colKategoriDetajimArtikulli : System.Collections.Generic.List<clsKategoriDetajimArtikulli>
    {

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbInventari.clsKategoriDetajimArtikulli"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsKategoriDetajimArtikulli this[int index]
        {
            get { return ((clsKategoriDetajimArtikulli)base[index]); }
        }

        /// <summary>
        /// metoda shton nje obj te ri ne array list ne pozicionin e percaktuar nga index-i
        /// </summary>
        public void shtoKategoriDetajimiNeIndeksin(int index, clsKategoriDetajimArtikulli kategori)
        {
            base.Insert(index, kategori);
        }

        /// <summary>
        /// mbush gjithe kategorite e detajimit te artikullit
        /// </summary>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert</returns>
        public bool mbushGjitheKategoriteDetajimit()
        {
            clsDatabaseInventari dbKategoriDetajimit = new clsDatabaseInventari();
            return mbushKategoriDetajimesh(dbKategoriDetajimit.ktheGjitheKategoriteDetajimit());
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbInventari.clsKategoriDetajimArtikulli"/> 
        /// </summary>
        private bool mbushKategoriDetajimesh(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsKategoriDetajimArtikulli kat = new clsKategoriDetajimArtikulli();
                    //kat.mbushKategoriDetajimArtikulli(rreshti);
                    this.Add(new clsKategoriDetajimArtikulli(rreshti));
                }

            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }

        #endregion
        [Obsolete("Perdor: bool mbushKategoriDetajimesh(DataTable dt)", true)]
        public colKategoriDetajimArtikulli mbushArrayListkategoriDetajimesh(DataSet ds)
        {
            colKategoriDetajimArtikulli kategorite = new colKategoriDetajimArtikulli();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsKategoriDetajimArtikulli kat = new clsKategoriDetajimArtikulli();

                kat.IdKategoriDetajimi = int.Parse(rreshti[0].ToString());
                kat.KodiKategoriDetajimi = rreshti[1].ToString();
                kat.PershkrimiKategoriDetajimi = rreshti[2].ToString();

                kategorite.Add(kat);
            }
            return kategorite;
        }
    }
}

