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
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsTrupiKategoriZbritje
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colTrupatKategoriteZbritjes : System.Collections.Generic.List<clsTrupiKategoriZbritje >
    {

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbInventari.clsTrupiKategoriZbritje"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsTrupiKategoriZbritje this[int index]
        {
            get { return ((clsTrupiKategoriZbritje)base[index]); }
        }
        /// <summary>
        /// metoda per shtimin e nje obj clsTrupiKategoriZbritje ne nje arraylist
        /// </summary>
        public bool shtoTrupiKategoriZbritje(clsTrupiKategoriZbritje kategoriZbritje)
        {
            base.Add(kategoriZbritje);
            if (base.Contains(kategoriZbritje))
                return true;
            else return false;
        }
        /// <summary>
        /// metoda per heqjen e nje obj clsTrupiKategoriZbritje ne nje arraylist
        /// </summary>
        public bool fshiTrupiKategoriZbritje(clsTrupiKategoriZbritje kategoriZbritje)
        {
            base.Remove(kategoriZbritje);
            if (base.Contains(kategoriZbritje))
                return false;
            else return true;
        }
        /// <summary>
        /// metoda per heqjen e nje obj clsTrupiKategoriZbritje ne nje arraylist
        /// </summary>
        public bool fshiGjitheTrupatKategoriteZbritjes()
        {
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// metoda per heqjen e nje obj clsTrupiKategoriZbritje ne nje arraylist
        /// </summary>
        public void fshiKeteTrupiKategoriZbritje(int index)
        {
            base.RemoveAt(index);
        }
        /// <summary>
        /// metoda shton nje obj te ri ne array list ne pozicionin e percaktuar nga index-i
        /// </summary>
        public void shtoTrupiKategoriZbritjeNeIndeksin(int index, clsTrupiKategoriZbritje kategoriZbritje)
        {
            base.Insert(index, kategoriZbritje);
        }
        /// <summary>
        /// metoda gjen index-in ne liste ne te cilin ndodhet nje obj i caktuar
        /// </summary>
        public int indeksiTrupiKategoriZbritjes(clsTrupiKategoriZbritje kategoriZbritje)
        {
            return base.IndexOf(kategoriZbritje);
        }
        /// <summary>
        /// metoda kontrollon nqs nje obj ndodhet ne arraylist
        /// </summary>
        public bool ekzistonTrupiKategoriZbritje(clsTrupiKategoriZbritje kategoriZbritje)
        {
            if (base.Contains(kategoriZbritje))
                return true;
            else return false;
        }
        /// <summary>
        /// metoda qe kthen nr e obj qe ka arraylist
        /// </summary>
        public int numriTrupaveKategoriveZbritje()
        {
            return base.Count;
        }

        /// <summary>
        /// mbush gjithe trupat kategori zbritjes sipas ndermarrjes
        /// </summary>
        /// <param name="idnderviti">id e ndermarrjes se vitit</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kunder false</returns>
        public bool mbushGjitheTrupatKategoriZbritjeSipasNdermarrjes(int idnderviti)
        {
            clsDatabaseInventari dbKategoriZbritje = new clsDatabaseInventari();
            bool sukses = mbushTrupatKategoriZbritje(dbKategoriZbritje.ktheGjitheTrupatKategoriZbritjeSipasNdermarrjes(idnderviti));
            dbKategoriZbritje.Dispose();
            return sukses;
        }

        /// <summary>
        /// mbush te gjithe trupat sipas id se kokes
        /// </summary>
        /// <param name="idKokaKategoriZbritje">id e kokes se kategorise zbritje</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushTrupatKategoriZbritjeSipasKokes(int idKokaKategoriZbritje)
        {
            clsDatabaseInventari dbKategoriZbritje = new clsDatabaseInventari();
            bool sukses = mbushTrupatKategoriZbritje(dbKategoriZbritje.ktheTrupatKategoriZbritjeSipasKokes(idKokaKategoriZbritje));
            dbKategoriZbritje.Dispose();
            return sukses;
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbInventari.clsTrupiKategoriZbritje"/> 
        /// </summary>
        private bool mbushTrupatKategoriZbritje(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsTrupiKategoriZbritje kategoriZbritje = new clsTrupiKategoriZbritje();
                    //kategoriZbritje.mbushTrupiKategoriZbritje(rreshti);
                    this.Add(new clsTrupiKategoriZbritje(rreshti));
                }

            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }

        #endregion
        [Obsolete("Perdor: bool mbushTrupatKategoriZbritje(DataTable dt)", true)]
        public colTrupatKategoriteZbritjes mbushArrayListTrupashKategorishZbritje(DataSet ds)
        {
            colTrupatKategoriteZbritjes kategoriteZbritje = new colTrupatKategoriteZbritjes();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsTrupiKategoriZbritje kategoriZbritje = new clsTrupiKategoriZbritje();

                kategoriZbritje.IdTrupiKategoriZbritje = int.Parse(rreshti[0].ToString());
                kategoriZbritje.IdKokaKategoriZbritje  = int.Parse (rreshti[1].ToString());
               
                kategoriZbritje.DateFillimi= DateTime.Parse(rreshti[2].ToString());
                kategoriZbritje.DateMbarimi = DateTime.Parse (rreshti[3].ToString());
                kategoriZbritje.VleraMin  = decimal.Parse (rreshti[4].ToString());
                kategoriZbritje.VleraMax  = decimal.Parse (rreshti[5].ToString());
                kategoriZbritje.Lloji  = int.Parse (rreshti[6].ToString());
                kategoriZbritje.Zbritja = decimal.Parse (rreshti[7].ToString());
                kategoriZbritje.IdPerdoruesi = int.Parse(rreshti[8].ToString());
                //kategoriZbritje.IdNderViti = int.Parse(rreshti[9].ToString());
                kategoriZbritje.Prioriteti = int.Parse(rreshti[9].ToString());
                kategoriteZbritje.Add(kategoriZbritje);
            }
            return kategoriteZbritje;
        }
    }
}
