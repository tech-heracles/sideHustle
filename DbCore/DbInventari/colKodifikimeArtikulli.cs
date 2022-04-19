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
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsKodifikimArtikulli
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colKodifikimeArtikulli : System.Collections.Generic.List<clsKodifikimArtikulli>
    {
        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbInventari.clsKodifikimArtikulli"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsKodifikimArtikulli this[int index]
        {
            get { return ((clsKodifikimArtikulli)base[index]); }
        }
        /// <summary>
        /// metoda per shtimin e nje obj clsKodifikimArtikulli ne nje arraylist
        /// </summary>
        public bool shtoKodifikimArtikulli(clsKodifikimArtikulli kodifikimArtikulli)
        {
            base.Add(kodifikimArtikulli);
            if (base.Contains(kodifikimArtikulli))
                return true;
            else return false;
        }
        /// <summary>
        /// metoda per heqjen e nje obj clsKodifikimArtikulli ne nje arraylist
        /// </summary>
        public bool fshiKodifikimArtikulli(clsKodifikimArtikulli kodifikimArtikulli)
        {
            base.Remove(kodifikimArtikulli);
            if (base.Contains(kodifikimArtikulli))
                return false;
            else return true;
        }
        /// <summary>
        /// metoda per heqjen e nje obj clsKodifikimArtikulli ne nje arraylist
        /// </summary>
        public bool fshiGjitheKodifikimeArtikulli()
        {
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// metoda per heqjen e nje obj clsKodifikimArtikulli ne nje arraylist
        /// </summary>
        public void fshiKeteKodifikimArtikulli(int index)
        {
            base.RemoveAt(index);
        }

        public static DataTable GetKodifikimeArtikulliLookupSimpleTable(int idNdermarrje)
        {
            using (clsDatabaseInventari db = new clsDatabaseInventari())
                return db.GetKodifikimeArtikulliLookupSimpleTable(idNdermarrje);
        }

        /// <summary>
        /// metoda shton nje obj te ri ne array list ne pozicionin e percaktuar nga index-i
        /// </summary>
        public void shtoKodifikimArtikulliNeIndeksin(int index, clsKodifikimArtikulli kodifikimArtikulli)
        {
            base.Insert(index, kodifikimArtikulli);
        }
        /// <summary>
        /// metoda gjen index-in ne liste ne te cilin ndodhet nje obj i caktuar
        /// </summary>
        public int indeksiKodifikimArtikulli(clsKodifikimArtikulli kodifikimArtikulli)
        {
            return base.IndexOf(kodifikimArtikulli);
        }
        /// <summary>
        /// metoda kontrollon nqs nje obj ndodhet ne arraylist
        /// </summary>
        public bool ekzistonKodifikimArtikulli(clsKodifikimArtikulli kodifikimArtikulli)
        {
            if (base.Contains(kodifikimArtikulli))
                return true;
            else return false;
        }
        /// <summary>
        /// metoda qe kthen nr e obj qe ka arraylist
        /// </summary>
        public int numriKodifikimeveArtikulli()
        {
            return base.Count;
        }

        /// <summary>
        /// mbush gjithe kodifikimet e artikullit sipas ndermarrjes
        /// </summary>
        /// <param name="idndermarje">id e ndermarrjes</param>
        /// <returns>kthen true nese mbushja kryhet me sukses,ne te kundert false</returns>
        public bool mbushGjitheKodifikimetArtikulliSipasNdermarrjes(int idndermarje)
        {
            clsDatabaseInventari dbKodifikimArtikujsh = new clsDatabaseInventari();
            bool sukses = mbushKodifikimArtikujsh(dbKodifikimArtikujsh.ktheGjitheKodifikimetArtikulliSipasNdermarrjes(idndermarje));
            dbKodifikimArtikujsh.Dispose();
            return sukses;
        }

        public static DataTable merrKodifikimArtikulliExport(int idndermarje, int lloji, string emerTabKoka, string emerFusheID)
        {
            using (clsDatabaseInventari db = new clsDatabaseInventari())
            {
                return db.ktheGjitheKodifikimetArtikulliSipasNdermarrjesExport(idndermarje, lloji, emerTabKoka, emerFusheID);
            }
        }

        public bool merrKodifikimArtikulliSipasLlojit(int llojkodifikimi, int idndermarrje, bool llojartikulli)
        {
            clsDatabaseInventari dbKodifikimArtikujsh = new clsDatabaseInventari();
            bool sukses = mbushKodifikimArtikujsh(dbKodifikimArtikujsh.merrKodifikimArtikulliSipasLlojit(llojkodifikimi, idndermarrje,llojartikulli));
            dbKodifikimArtikujsh.Dispose();
            return sukses;
        }
               
        /// <summary>
        /// Metode qe perdoret per te mbushr autocomplete.
        /// </summary>
        /// <param name="llojkodifikimi">Lloji i kodifikimit.</param>
        /// <param name="idndermarrje">Identifikuesi i ndermarrjes.</param>
        /// <param name="llojartikulli">if set to <c>true</c> aqt.</param>
        /// <returns></returns>
        public static DataTable MerrKodifikimArtikulliSipasLlojitAc(int llojkodifikimi, int idndermarrje, bool llojartikulli, string kodi)
        {
            using (var databaseInventari = new clsDatabaseInventari())
                return databaseInventari.MerrKodifikimArtikulliSipasLlojitAc(llojkodifikimi, idndermarrje, llojartikulli, kodi);
        }

        public bool merrKodifikimArtikulliSipasLlojKodifikimit(int llojkodifikimi, int idndermarrje)
        {
            clsDatabaseInventari dbKodifikimArtikujsh = new clsDatabaseInventari();
            bool sukses = mbushKodifikimArtikujsh(dbKodifikimArtikujsh.merrKodifikimArtikulliSipasLlojKodifikimit(llojkodifikimi, idndermarrje));
            dbKodifikimArtikujsh.Dispose();
            return sukses;
        }

        public bool merrKodifikimArtikulliSipasLlojitDheNivelit(int llojkodifikimi, int idndermarrje, bool llojartikulli, int nivelKodifikimi)
        {
            clsDatabaseInventari dbKodifikimArtikujsh = new clsDatabaseInventari();
            bool sukses = mbushKodifikimArtikujsh(dbKodifikimArtikujsh.merrKodifikimArtikulliSipasLlojitDheNivelKodifikimit(llojkodifikimi, idndermarrje, llojartikulli, nivelKodifikimi));
            dbKodifikimArtikujsh.Dispose();
            return sukses;
        }

        public bool merrKodifikimArtikulliSipasLlojDheNivelKodifikimit(int llojkodifikimi, int idndermarrje, int nivelKodifikimi)
        {
            clsDatabaseInventari dbKodifikimArtikujsh = new clsDatabaseInventari();
            bool sukses = mbushKodifikimArtikujsh(dbKodifikimArtikujsh.merrKodifikimArtikulliSipasLlojKodDheNivelKodifikimit(llojkodifikimi, idndermarrje, nivelKodifikimi));
            dbKodifikimArtikujsh.Dispose();
            return sukses;
        }

        public static DataTable merrKodifikimArtikulliSipasLlojitDt(int llojkodifikimi, int idndermarrje, bool llojartikulli)
        {
            using (clsDatabaseInventari dbKodifikimArtikujsh = new clsDatabaseInventari())
            {
              return  dbKodifikimArtikujsh.merrKodifikimArtikulliSipasLlojitDt(llojkodifikimi, idndermarrje, llojartikulli);
            }
    
        }
        /// <summary>
        /// mbush gjithe kodifikimet e artikullit sipas ndermarrjes jo prind
        /// </summary>
        /// <param name="idndermarje">id e ndermarrjes</param>
        /// <returns>kthen true nese mbushja kryhet me sukses,ne te kundert false</returns>
        public bool mbushGjitheKodifikimetArtikulliSipasNdermarrjesJoPrind(int idndermarje, bool llojartikulli)
        {
            clsDatabaseInventari dbKodifikimArtikujsh = new clsDatabaseInventari();
            bool sukses = mbushKodifikimArtikujsh(dbKodifikimArtikujsh.ktheGjitheKodifikimetArtikulliSipasNdermarrjesJoPrind(idndermarje,llojartikulli));
            dbKodifikimArtikujsh.Dispose();
            return sukses;
        }

        public bool mbushGjitheKodifikimetArtikulliSipasNdermarrjesJoPrindDheLlojit(int idndermarje, int llojkodifikimi, bool llojartikulli)
        {
            clsDatabaseInventari dbKodifikimArtikujsh = new clsDatabaseInventari();
            bool sukses = mbushKodifikimArtikujsh(dbKodifikimArtikujsh.mbushGjitheKodifikimetArtikulliSipasNdermarrjesJoPrindDheLlojit(idndermarje, llojkodifikimi, llojartikulli));
            dbKodifikimArtikujsh.Dispose();
            return sukses;
        }
        public bool mbushGjitheKodifikimetArtikulliSipasNdermarrjesLlojitNiveli1(int idndermarje, int llojkodifikimi, bool llojartikulli)
        {
            clsDatabaseInventari dbKodifikimArtikujsh = new clsDatabaseInventari();
            bool sukses = mbushKodifikimArtikujsh(dbKodifikimArtikujsh.mbushGjitheKodifikimetArtikulliSipasNdermarrjesLlojitNiveli1(idndermarje, llojkodifikimi, llojartikulli));
            dbKodifikimArtikujsh.Dispose();
            return sukses;
        }

        /// <summary>
        /// mbush kodifikimin e artikullit sipas id se prindit
        /// </summary>
        /// <param name="idprindi">id e prindit</param>
        /// <returns>kthen true nese mbushja kryhet me sukses,ne te kundert false</returns>
        public bool mbushKodifikimArtikulliSipasPrindit(int idprindi)
        {
            clsDatabaseInventari dbKodifikimArtikujsh = new clsDatabaseInventari();
            bool sukses = mbushKodifikimArtikujsh(dbKodifikimArtikujsh.ktheKodifikimArtikulliSipasPrindit(idprindi));
            dbKodifikimArtikujsh.Dispose();
            return sukses;
        }

        public bool mbushKodifikimArtikulliSipasPershkrimit(string pershkrimi, int idndermarrje)
        {
            clsDatabaseInventari dbKodifikimArtikujsh = new clsDatabaseInventari();
            bool sukses = mbushKodifikimArtikujsh(dbKodifikimArtikujsh.ktheKodifikimArtikulliSipasPershkrimit(pershkrimi, idndermarrje));
            dbKodifikimArtikujsh.Dispose();
            return sukses;
        }

        public bool mbushKodifikimArtikulliSipasPrinditDheLlojit(int idprindi, int llojkodifikimi)
        {
            clsDatabaseInventari dbKodifikimArtikujsh = new clsDatabaseInventari();
            bool sukses = mbushKodifikimArtikujsh(dbKodifikimArtikujsh.mbushKodifikimArtikulliSipasPrinditDheLlojit(idprindi, llojkodifikimi));
            dbKodifikimArtikujsh.Dispose();
            return sukses;
        }
        public bool ktheBijte(int idKodifikim, int idNdermarje)
        {
            clsDatabaseInventari dbKodifikimArtikujsh = new clsDatabaseInventari();
            bool sukses = mbushKodifikimArtikujsh(dbKodifikimArtikujsh.ktheBijte(idKodifikim, idNdermarje));
            dbKodifikimArtikujsh.Dispose();
            return sukses;
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbInventari.clsKodifikimArtikulli"/> 
        /// </summary>
        private bool mbushKodifikimArtikujsh(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsKodifikimArtikulli kodifikimArtikulli = new clsKodifikimArtikulli();
                    //kodifikimArtikulli.mbushKodifikimArtikulli(rreshti);
                    this.Add(new clsKodifikimArtikulli(rreshti));
                }

            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }

        #endregion
        [Obsolete("Perdor: bool mbushKodifikimArtikujsh(DataTable dt)", true)]
        public colKodifikimeArtikulli mbushArrayListKodifikimeshArtikulli(DataSet ds)
        {
            colKodifikimeArtikulli kodifikimeArtikulli = new colKodifikimeArtikulli();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsKodifikimArtikulli kodifikimArtikulli = new clsKodifikimArtikulli();

                kodifikimArtikulli.IdKodifikimi = int.Parse(rreshti[0].ToString());
                kodifikimArtikulli.KodKodifikimi = rreshti[1].ToString();
                kodifikimArtikulli.PershkrimKodifikimi = rreshti[2].ToString();
                kodifikimArtikulli.IdPrindi = int.Parse(rreshti[3].ToString());
                kodifikimArtikulli.NivelKodifikimi = int.Parse(rreshti[4].ToString());
                kodifikimArtikulli.IdPerdoruesi = int.Parse(rreshti[5].ToString());
                //kodifikimArtikulli.IdNderViti = int.Parse(rreshti[6].ToString());
                kodifikimArtikulli.IdNdermarje = int.Parse(rreshti[6].ToString());
                kodifikimArtikulli.LlojKodifikimi = int.Parse(rreshti[7].ToString());
                kodifikimeArtikulli.Add(kodifikimArtikulli);
            }
            return kodifikimeArtikulli;
        }
    }
}
