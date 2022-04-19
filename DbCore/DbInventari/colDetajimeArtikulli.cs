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
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsDetajimArtikulli
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colDetajimeArtikulli : System.Collections.Generic.List<clsDetajimArtikulli>
    {
        public colDetajimeArtikulli()
        {

        }
        /// <summary>
        /// 
        /// Konstruktor qe sherben per mbushjen e detajimeve per shitjen dhe blerjen
        /// </summary>
        /// <param name="idkokashitje">
        /// id e fatures
        /// </param>
        /// <param name="lloji">
        /// tregon nese do meren detajimet e para ose te dyta
        /// </param>
        public colDetajimeArtikulli(int idkokashitje, int lloji)
        {
            if (idkokashitje > 1)
                using (clsDatabaseInventari db = new clsDatabaseInventari())
                {
                    if (lloji == 1)
                        mbushDetajimArtikujsh(db.merrDetajimeFature(idkokashitje));
                    else mbushDetajimArtikujsh(db.merrDetajime2Fature(idkokashitje));
                }
        }
        public colDetajimeArtikulli(int idkokashitje, int lloji, colDetajimeArtikulli colDetArt)
        {
            if (idkokashitje > 1)
                using (clsDatabaseInventari db = new clsDatabaseInventari())
                {
                    if (lloji == 1)
                        mbushDetajimArtikujsh(db.merrDetajimeFature(idkokashitje), colDetArt);
                    else mbushDetajimArtikujsh(db.merrDetajime2Fature(idkokashitje), colDetArt);
                }
        }

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbInventari.clsDetajimArtikulli"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsDetajimArtikulli this[int index]
        {
            get { return ((clsDetajimArtikulli)base[index]); }
        }

        /// <summary>
        /// metoda per shtimin e nje obj clsDetajimArtikulli ne nje arraylist
        /// </summary>
        public bool shtoDetajimArtikulli(clsDetajimArtikulli detajimArtikulli)
        {
            base.Add(detajimArtikulli);
            if (base.Contains(detajimArtikulli))
                return true;
            else return false;
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsDetajimArtikulli ne nje arraylist
        /// </summary>
        public bool fshiDetajimArtikulli(clsDetajimArtikulli detajimArtikulli)
        {
            base.Remove(detajimArtikulli);
            if (base.Contains(detajimArtikulli))
                return false;
            else return true;
        }

        public static DataTable GetDetajimeLookupSimpleTable(int idNdermarrje, int idPerdoruesi)
        {
            using (clsDatabaseInventari db = new clsDatabaseInventari())
                return db.GetDetajimeLookupSimpleTable(idNdermarrje, idPerdoruesi);
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsDetajimArtikulli ne nje arraylist
        /// </summary>
        public bool fshiGjitheDetajimeArtikulli()
        {
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsDetajimArtikulli ne nje arraylist
        /// </summary>
        public void fshiKeteDetajimArtikulli(int index)
        {
            base.RemoveAt(index);
        }

        /// <summary>
        /// metoda shton nje obj te ri ne array list ne pozicionin e percaktuar nga index-i
        /// </summary>
        public void shtoDetajimArtikulliNeIndeksin(int index, clsDetajimArtikulli detajimArtikulli)
        {
            base.Insert(index, detajimArtikulli);
        }

        /// <summary>
        /// metoda gjen index-in ne liste ne te cilin ndodhet nje obj i caktuar
        /// </summary>
        public int indeksiDetajimArtikullit(clsDetajimArtikulli detajimArtikulli)
        {
            return base.IndexOf(detajimArtikulli);
        }

        /// <summary>
        /// metoda kontrollon nqs nje obj ndodhet ne arraylist
        /// </summary>
        public bool ekzistonDetajimArtikulli(clsDetajimArtikulli detajimArtikulli)
        {
            if (base.Contains(detajimArtikulli))
                return true;
            else return false;
        }

        /// <summary>
        /// metoda qe kthen nr e obj qe ka arraylist
        /// </summary>
        public int numriDetajimArtikullive()
        {
            return base.Count;
        }

        /// <summary>
        /// mbush detajimet e artikullit sipas id se ndermarrjes dhe id se perdoruesit
        /// </summary>
        /// <param name="idndermarje">id e ndermarrjes</param>
        /// <param name="idperdorues">id e perdoruesit</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        public bool mbushDetajimeSipasNdermarrjesAndAutorizim(int idndermarje, int idperdorues)
        {
            clsDatabaseInventari dbDetajimArtikulli = new clsDatabaseInventari();
            bool sukses = mbushDetajimArtikujsh(dbDetajimArtikulli.ktheDetajimeSipasNdermarrjesAndAutorizime(idndermarje, idperdorues));
            dbDetajimArtikulli.Dispose();
            return sukses;
        }

        public static DataTable merrDetajimeSipasNdermarrjesAndAutorizimDt(int idndermarje, int idperdorues)
        {
            using (clsDatabaseInventari dbDetajimArtikulli = new clsDatabaseInventari())
            {
                return dbDetajimArtikulli.ktheDetajimeSipasNdermarrjesAndAutorizime(idndermarje, idperdorues);
            }
        }

        /// <summary>
        /// mbush detajimet sipas artikullit ndermarrjes dhe personit te autorizuar
        /// </summary>
        /// <param name="kodiArtikullit">kodi i artikullit</param>
        /// <param name="idndermarje">id e ndermarrjes</param>
        /// <param name="idperdorues">id e perdoruesit</param>
        /// <returns></returns>
        public bool mbushDetajimeSipasArtikullitAndNdermarrjesAndAutorizime(string kodiArtikullit, int idndermarje, int idperdorues)
        {
            clsDatabaseInventari dbDetajimArtikulli = new clsDatabaseInventari();
            bool sukses = mbushDetajimArtikujsh(dbDetajimArtikulli.ktheDetajimeSipasArtikullitAndNdermarrjesAndAutorizime(kodiArtikullit, idndermarje, idperdorues));
            dbDetajimArtikulli.Dispose();
            return sukses;
        }

        public static DataTable ktheDetajimeMeLlojSipasArtikullitAndNdermarrjesAndAutorizime(string kodiArtikullit, int idndermarje, int idperdorues)
        {
            clsDatabaseInventari dbDetajimArtikulli = new clsDatabaseInventari();
            DataTable dt = dbDetajimArtikulli.ktheDetajimeSipasMeLlojArtikullitAndNdermarrjesAndAutorizime(kodiArtikullit, idndermarje, idperdorues);
            dbDetajimArtikulli.Dispose();
            return dt;
        }
        public static DataTable merrDetajimeSipasArtikullitMagazinesDheRadhesSeHyrjes(string kodiArtikullit, int idndermarje, string kodmagazine, DateTime data, bool promocione, clsDatabaseInventari dbDetajimArtikulli)
        {

            DataTable tabela = dbDetajimArtikulli.merrDetajimeSipasArtikullitMagazinesDheRadhesSeHyrjes(kodiArtikullit, idndermarje, kodmagazine, data, promocione);

            return tabela;
        }
        /// <summary>
        /// mbush detajimet sipas artikullit ndermarrjes dhe personit te autorizuar
        /// </summary>
        /// <param name="kodiArtikullit">kodi i artikullit</param>
        /// <param name="idndermarje">id e ndermarrjes</param>
        /// <param name="idperdorues">id e perdoruesit</param>
        /// <returns></returns>
        public bool mbushDetajimeSipasArtikullitAndNdermarrjesAndAutorizime(string kodiArtikullit, int idndermarje, int idperdorues, clsDatabaseInventari dbDetajimArtikulli)
        {
            bool sukses = mbushDetajimArtikujsh(dbDetajimArtikulli.ktheDetajimeSipasArtikullitAndNdermarrjesAndAutorizime(kodiArtikullit, idndermarje, idperdorues));
            return sukses;
        }

        /// <summary>
        /// mbush detajimet sipas artikullit ndermarrjes dhe personit te autorizuar
        /// </summary>
        /// <param name="kodiArtikullit">kodi i artikullit</param>
        /// <param name="idndermarje">id e ndermarrjes</param>
        /// <param name="idperdorues">id e perdoruesit</param>
        /// <param name="lloji"> lloji i detajimit i pare apo i dyte</param>
        /// <returns></returns>
        public bool ktheDetajimeSipasArtikullitAndNdermarrjesAndAutorizimeSipasLlojit(string kodiArtikullit, int idndermarje, int idperdorues, int lloji)
        {
            clsDatabaseInventari dbDetajimArtikulli = new clsDatabaseInventari();
            bool sukses = mbushDetajimArtikujsh(dbDetajimArtikulli.ktheDetajimeSipasArtikullitAndNdermarrjesAndAutorizimeSipasLlojit(kodiArtikullit, idndermarje, idperdorues, lloji));
            dbDetajimArtikulli.Dispose();
            return sukses;
        }

        public bool ktheDetajimeSipasIdArtikullitAndNdermarrjesAndAutorizimeSipasLlojit(int idArtikullit, int idndermarje, int idperdorues, int lloji)
        {
            clsDatabaseInventari dbDetajimArtikulli = new clsDatabaseInventari();
            bool sukses = mbushDetajimArtikujsh(dbDetajimArtikulli.ktheDetajimeSipasIdArtikullitAndNdermarrjesAndAutorizimeSipasLlojit(idArtikullit, idndermarje, idperdorues, lloji));
            dbDetajimArtikulli.Dispose();
            return sukses;
        }

        /// <summary>
        /// mbush detajimet sipas ndermarrjes dhe autorizimit nga nje kategori ne tjetren
        /// </summary>
        /// <param name="idndermarje">id e ndermarrjes</param>
        /// <param name="idperdorues">id e perdoruesit</param>
        /// <param name="pershkriminga">pershkrmi fillestar i kategorise</param>
        /// <param name="pershkrimideri">deri ne cfare kategorie</param>
        /// <param name="kategori">kategoria</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushDetajimeSipasNdermarrjesAndAutorizimeAndKategoribetweenPershkrimi(int idndermarje, int idperdorues, string pershkriminga, string pershkrimideri, int kategori)
        {
            clsDatabaseInventari dbDetajimArtikulli = new clsDatabaseInventari();
            bool sukses = mbushDetajimArtikujsh(dbDetajimArtikulli.ktheDetajimeSipasNdermarrjesAndAutorizimeAndKategoribetweenPershkrimi(idndermarje, idperdorues, pershkriminga, pershkrimideri, kategori));
            dbDetajimArtikulli.Dispose();
            return sukses;
        }

        /// <summary>
        /// mbush detajimet sipas ndermarrjes autorizimit dhe kategorise
        /// </summary>
        /// <param name="idndermarje">id e ndermarrjes</param>
        /// <param name="idperdorues">id e perdoruesit</param>
        /// <param name="kategoriDetajimi">kategoria</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushDetajimeSipasNdermarrjesAndAutorizimeSipasKategorise(int idndermarje, int idperdorues, int kategoriDetajimi)
        {
            clsDatabaseInventari dbDetajimArtikulli = new clsDatabaseInventari();
            bool sukses = mbushDetajimArtikujsh(dbDetajimArtikulli.ktheDetajimeSipasNdermarrjesAndAutorizimeSipasKategorise(idndermarje, idperdorues, kategoriDetajimi));
            dbDetajimArtikulli.Dispose();
            return sukses;
        }

        public static DataTable merrDetajimeSipasNdermarrjesAndAutorizimeSipasKategoriseDt(int idndermarje, int idperdorues, int kategoriDetajimi)
        {
            using (clsDatabaseInventari dbDetajimArtikulli = new clsDatabaseInventari())
            {
                return dbDetajimArtikulli.ktheDetajimeSipasNdermarrjesAndAutorizimeSipasKategorise(idndermarje, idperdorues, kategoriDetajimi);
            }
        }

        public bool ktheDetajimeSipasNdermarrjesAndAutorizimeSipasKategoriseJoArtikulli(int idndermarje, int idperdorues, int kategoriDetajimi, int idartikulli)
        {
            clsDatabaseInventari dbDetajimArtikulli = new clsDatabaseInventari();
            bool sukses = mbushDetajimArtikujsh(dbDetajimArtikulli.ktheDetajimeSipasNdermarrjesAndAutorizimeSipasKategoriseJoArtikulli(idndermarje, idperdorues, kategoriDetajimi, idartikulli));
            dbDetajimArtikulli.Dispose();
            return sukses;
        }

        /// <summary>
        /// mbush detajimet sipas artikullit ndermarrjes dhe autorizimit, por sipas like ne sql
        /// </summary>
        /// <param name="idNdermarje">id e ndermarrjes</param>
        /// <param name="idperdorues">id e perdoruesit</param>
        /// <param name="kodDetajimi">kodi i detajimit</param>
        /// <param name="kodArtikulli">kodi i artikullit</param>
        /// <param name="lloji">lloji i detajimit</param>
        /// <returns>kthen true nese mbushja kryhet me sukses ne te kundert false</returns>
        public bool mbushDetajimeSipasArtikullitAndNdermarrjesAndAutorizimeLike(int idNdermarje, int idperdorues, string kodDetajimi, string kodArtikulli, int lloji)
        {
            clsDatabaseInventari dbDetajimArtikulli = new clsDatabaseInventari();
            bool sukses = mbushDetajimArtikujsh(dbDetajimArtikulli.ktheDetajimeSipasArtikullitAndNdermarrjesAndAutorizimeLike(idNdermarje, idperdorues, kodDetajimi, kodArtikulli, lloji));
            dbDetajimArtikulli.Dispose();
            return sukses;
        }

        public static DataTable mbushDetajimeSipasArtikullitAndNdermarrjesAndAutorizimeLikeNew(int idNdermarje, int idperdorues, string kodDetajimi, string kodArtikulli, int lloji)
        {
            clsDatabaseInventari dbDetajimArtikulli = new clsDatabaseInventari();
            DataTable tabela = dbDetajimArtikulli.ktheDetajimeSipasArtikullitAndNdermarrjesAndAutorizimeLike(idNdermarje, idperdorues, kodDetajimi, kodArtikulli, lloji);
            dbDetajimArtikulli.Dispose();
            return tabela;
        }

        public static DataTable mbushDetajimeSipasArtikullitAndNdermarrjesAndAutorizimeLikeNewPati(int idNdermarje, int idperdorues, string kodDetajimi, string kodArtikulli, int lloji, bool sipasGjendjes)
        {
            using (var dbDetajimArtikulli = new clsDatabaseInventari())
                return dbDetajimArtikulli.ktheDetajimeSipasArtikullitAndNdermarrjesAndAutorizimeLikeNewPati(idNdermarje, idperdorues, kodDetajimi, kodArtikulli, lloji, sipasGjendjes);
        }

        public static DataTable mbushDetajimeSipasArtikujveAndNdermarrjesAndAutorizimeLike(int idNdermarje, int idperdorues, string kodDetajimi, string kodArtikulli, int lloji, bool sipasGjendjes)
        {
            using (var dbDetajimArtikulli = new clsDatabaseInventari())
                return dbDetajimArtikulli.ktheDetajimeSipasArtikujveAndNdermarrjesAndAutorizimeLike(idNdermarje, idperdorues, kodDetajimi, kodArtikulli, lloji, sipasGjendjes);
        }
        public static DataTable ktheDetajimeSipasArtikullitNdermarrjesDheLlojit(string kodiArtikullit, int idndermarje, int idPerdorues, int lloji)
        {
            clsDatabaseInventari dbDetajimArtikulli = new clsDatabaseInventari();
            DataTable tabela = dbDetajimArtikulli.ktheDetajimeSipasArtikullitNdermarrjesDheLlojit(kodiArtikullit, idndermarje, idPerdorues, lloji);
            dbDetajimArtikulli.Dispose();
            return tabela;
        }

        public static DataTable ktheDetajimeSipasArtikullitNdermarrjesDheLlojit(string kodiArtikullit, int idndermarje, int idPerdorues, int lloji, clsDatabaseInventari dbDetajimArtikulli)
        {
            DataTable tabela = dbDetajimArtikulli.ktheDetajimeSipasArtikullitNdermarrjesDheLlojit(kodiArtikullit, idndermarje, idPerdorues, lloji);
            return tabela;
        }

        public bool mbushDetajimeSipasArtikullitNdermarrjesDheLlojit(string kodiArtikullit, int idndermarje, int idPerdorues, int lloji, clsDatabaseInventari dbInv)
        {
            return mbushDetajimArtikujsh(dbInv.ktheDetajimeSipasArtikullitNdermarrjesDheLlojit(kodiArtikullit, idndermarje, idPerdorues, lloji));
        }

        public bool mbushDetajimeSipasArtikullitNdermarrjesDheLlojit(string kodiArtikullit, int idndermarje, int idPerdorues, int lloji)
        {
            clsDatabaseInventari dbDetajimArtikulli = new clsDatabaseInventari();
            bool sukses = mbushDetajimArtikujsh(dbDetajimArtikulli.ktheDetajimeSipasArtikullitNdermarrjesDheLlojit(kodiArtikullit, idndermarje, idPerdorues, lloji));
            dbDetajimArtikulli.Dispose();
            return sukses;
        }

        /// <summary>
        /// kthen nje datatable me detajimet sipas ndermarrjes autorizimit dhe kategorise
        /// </summary>
        /// <param name="idndermarje">id e ndermarrjes</param>
        /// <param name="idperdorues">id e perdoruesit</param>
        /// <param name="kategoriDetajimi">kategoria</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public static DataTable ktheDetajimeSipasNdermarrjesAndAutorizimeSipasKategorise(int idndermarje, int idperdorues, int kategoriDetajimi)
        {
            clsDatabaseInventari dbDetajimArtikulli = new clsDatabaseInventari();
            DataTable tabela = dbDetajimArtikulli.ktheDetajimeSipasNdermarrjesAndAutorizimeSipasKategorise(idndermarje, idperdorues, kategoriDetajimi);
            dbDetajimArtikulli.Dispose();
            return tabela;
        }

        /// <summary>
        /// kthen nje datatable me detajimet sipas ndermarrjes
        /// </summary>
        /// <param name="idndermarje">id e ndermarrjes</param>
        /// <param name="idperdorues">id e perdoruesit</param>
        /// <param name="kategoriDetajimi">kategoria</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public static DataTable ktheDetajimeSipasAutorizimeDheKategorise(int idndermarje, int idperdorues)
        {
            clsDatabaseInventari dbDetajimArtikulli = new clsDatabaseInventari();
            DataTable tabela = dbDetajimArtikulli.ktheDetajimeSipasNdermarrjesAndAutorizime(idndermarje, idperdorues);
            dbDetajimArtikulli.Dispose();
            return tabela;
        }

        public static DataTable merrDetajimePerEksport(int idnderm)
        {
            clsDatabaseInventari db = new clsDatabaseInventari();
            DataTable tabela = db.merrDetajimePerEksport(idnderm);
            db.Dispose();
            return tabela;
        }

        public static List<int> ktheDetajimetMeGjendje(List<int> col, clsArtikulli art, int lloji, int detajimi1, int idMag, DateTime dateDok)
        {
            List<int> colGjendje = new List<int>();
            double sasi = 0;
            foreach (int idDet in col)
            {
                if (lloji == 2 && detajimi1 != 0)
                    sasi = DbRegjistrim.clsTrupiMagazina.merrSasiDetajimitDyteDheDetajimPare(art, idMag, dateDok, detajimi1, idDet);
                else
                    sasi = DbRegjistrim.clsTrupiMagazina.merrSasiSipasDetajimit(art, idMag, dateDok, idDet, lloji);

                if (sasi > 0)
                    colGjendje.Add(idDet);
            }
            return colGjendje;
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbInventari.clsDetajimArtikulli"/> 
        /// </summary>
        private bool mbushDetajimArtikujsh(DataTable dt)
        {
            //try
            //{

            foreach (DataRow rreshti in dt.Rows)
            {
                //clsDetajimArtikulli detajimArtikulli = new clsDetajimArtikulli();
                //detajimArtikulli.mbushDetajimArtikulli(rreshti);
                this.Add(new clsDetajimArtikulli(rreshti));
            }

            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;

        }
        private bool mbushDetajimArtikujsh(DataTable dt, colDetajimeArtikulli colDetArt)
        {
            //try
            //{

            foreach (DataRow rreshti in dt.Rows)
            {
                if (!rreshti.IsNull("IDDETAJIMARTIKULLI"))
                    this.Add(colDetArt.Where(x => x.IdDetajimArtikulli == int.Parse(rreshti["IDDETAJIMARTIKULLI"].ToString())).FirstOrDefault());
                else
                {
                    this.Add(new clsDetajimArtikulli(rreshti));
                }
                //clsDetajimArtikulli detajimArtikulli = new clsDetajimArtikulli();
                //detajimArtikulli.mbushDetajimArtikulli(rreshti);
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