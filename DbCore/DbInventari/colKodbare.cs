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
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsKodbari
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colKodbare : System.Collections.Generic.List<clsKodbari>
    {
        #region Konstruktoret

        public colKodbare()
        {
        }
        public colKodbare(int idArtikulli, clsDatabaseInventari dbKodbari)
        {
            mbushKodbare(dbKodbari.ktheKodbarSipasIdArtikulli(idArtikulli));
        }
        public colKodbare(int idArtikulli)
        {
            using (clsDatabaseInventari dbKodbari = new clsDatabaseInventari())
            {
                mbushKodbare(dbKodbari.ktheKodbarSipasIdArtikulli(idArtikulli));
            }
        }

        public colKodbare(Object[] kodbaret)
        {
            for (int i = 0; i < kodbaret.Length; i++)
            {
                this.Add(new clsKodbari((System.Collections.Generic.Dictionary<string, object>)kodbaret[i]));
            }
        }

        #endregion

        #region Metoda Publike

        public colKodbare merrKodbarePerShitjen(int idKokeShitje)
        {
            if (idKokeShitje > 0)
                using (clsDatabaseInventari db = new clsDatabaseInventari())
                {
                    mbushKodbare(db.merrKodbarSipasNjesivePareSipasIdve(idKokeShitje));
                    return this;
                }
            else return new colKodbare();
        }
        public colKodbare merrKodbarePerMagazinen(int idKoke)
        {
            if (idKoke > 0)
                using (clsDatabaseInventari db = new clsDatabaseInventari())
                {
                    mbushKodbare(db.merrKodbarSipasNjesivePareSipasIdveMag(idKoke));
                    return this;
                }
            else return new colKodbare();
        }
        /// <summary>
        /// kthen objektin <see cref="DbCore.DbInventari.clsKodbari"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsKodbari this[int index]
        {
            get { return ((clsKodbari)base[index]); }
        }
        /// <summary>
        /// metoda per shtimin e nje obj clsKodbari ne nje arraylist
        /// </summary>
        public bool shtoKodbar(clsKodbari kodbar)
        {
            base.Add(kodbar);
            if (base.Contains(kodbar))
                return true;
            else return false;
        }
        public static colKodbare kodBare(int idArtikulli, clsDatabaseInventari dbinventari)
        {
            return new colKodbare(idArtikulli, dbinventari);
        }
        public static colKodbare kodBare(int idArtikulli)
        {
            using (clsDatabaseInventari dbinventari = new clsDatabaseInventari())
            {
                return kodBare(idArtikulli, dbinventari);
            }
        }

        /// <summary>
        /// Metoda qe merr barkodin e artikullit sipas IdShitje ne trup te dokumentit per shitjen
        /// </summary>
        /// <param name="idDok"></param>
        /// <returns></returns>
        /// 
        public static DataTable ktheKodbarePerEksport(int idndermarje)
        {
            using (clsDatabaseInventari dbinv = new clsDatabaseInventari())
            {
                return dbinv.ktheKodbarePerEksport(idndermarje);
            }
        }


        public static DataTable merrKodbarArtikulliNeTrupDokShitje(int idDok)
        {
            using (clsDatabaseInventari dbinv = new clsDatabaseInventari())
            {
                return dbinv.merrKodbarSipasNjesivePareSipasIdve(idDok);
            }
        }
        /// Metoda qe merr barkodin e artikullit sipas IdMagazine ne trup te dokumentit per Magazinen
        public static DataTable merrKodbarArtikulliNeTrupDokMagazine(int idDok)
        {
            using (clsDatabaseInventari dbinv = new clsDatabaseInventari())
            {
                return dbinv.merrKodbarSipasNjesivePareSipasIdveMag(idDok);
            }
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsKodbari ne nje arraylist
        /// </summary>
        public bool fshiKodbar(clsKodbari kodbar)
        {
            base.Remove(kodbar);
            if (base.Contains(kodbar))
                return false;
            else return true;
        }
        /// <summary>
        /// metoda per heqjen e nje obj clsKodbari ne nje arraylist
        /// </summary>
        public bool fshiGjitheKodbaret()
        {
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// metoda per heqjen e nje obj clsKodbari ne nje arraylist
        /// </summary>
        public void fshiKeteKodbar(int index)
        {
            base.RemoveAt(index);
        }
        /// <summary>
        /// metoda shton nje obj te ri ne array list ne pozicionin e percaktuar nga index-i
        /// </summary>
        public void shtoKodbarinNeIndeksin(int index, clsKodbari kodbar)
        {
            base.Insert(index, kodbar);
        }
        /// <summary>
        /// metoda gjen index-in ne liste ne te cilin ndodhet nje obj i caktuar
        /// </summary>
        public int indeksiKodbarit(clsKodbari kodbar)
        {
            return base.IndexOf(kodbar);
        }
        /// <summary>
        /// metoda kontrollon nqs nje obj ndodhet ne arraylist
        /// </summary>
        public bool ekzistonKodbar(clsKodbari kodbar)
        {
            if (base.Contains(kodbar))
                return true;
            else return false;
        }
        /// <summary>
        /// metoda qe kthen nr e obj qe ka arraylist
        /// </summary>
        public int numriKodbareve()
        {
            return base.Count;
        }

        public bool mbushGjitheKodbaret()
        {
            clsDatabaseInventari dbKodbare = new clsDatabaseInventari();
            bool sukses = mbushKodbare(dbKodbare.ktheGjitheKodbaret());
            dbKodbare.Dispose();
            return sukses;
        }

        public static DataTable merrArtikujKodbareNdermarrjesAndAutorizimePerLupeKodbari(int idnderm, int idperdorues)
        {
            clsDatabaseInventari dbartikuj = new clsDatabaseInventari();
            DataTable tabela = dbartikuj.ktheArtikujKodbareNdermarrjesAndAutorizimePerLupeKodbari(idnderm, idperdorues);
            dbartikuj.Dispose();
            return tabela;
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbInventari.clsKodbari"/> 
        /// </summary>
        private bool mbushKodbare(DataTable dt)
        {
            //try
            //{
            foreach (DataRow rreshti in dt.Rows)
            {
                //clsKodbari kodbar = new clsKodbari();
                //kodbar.mbushKodbarin(rreshti);
                this.Add(new clsKodbari(rreshti));
            }

            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }

        #endregion
        [Obsolete("Perdor: bool mbushKodbare(DataTable dt)", true)]
        public colKodbare mbushArrayListKodbaresh(DataSet ds)
        {
            colKodbare kodbare = new colKodbare();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsKodbari kodbar = new clsKodbari();

                kodbar.IdKodbari = int.Parse(rreshti[0].ToString());
                kodbar.IdArtikulli = int.Parse(rreshti[1].ToString());
                kodbar.Pershkrimi = rreshti[2].ToString();

                kodbare.Add(kodbar);
            }
            return kodbare;
        }

        /// <summary>
        /// kthen numrin e kodbareve per artikullin
        /// </summary>
        /// <param name="idArtikulli"></param>
        /// <returns></returns>
        public static int kodBareCount(int idArtikulli)
        {
            using (clsDatabaseInventari db = new clsDatabaseInventari())
            {
                return db.ktheNrKodBarePerArtikullin(idArtikulli);
            }
        }
    }
}