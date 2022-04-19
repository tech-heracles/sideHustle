using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using DbCore.DbKontabiliteti;
using DbCore.IMBUtils.Extensions;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsTrupiShitje
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colTrupiShitje : System.Collections.Generic.List<clsTrupiShitje>
    {
        private List<int> idKokat;

        public colTrupiShitje()
        {
           

        }
        public colTrupiShitje(IEnumerable<clsTrupiShitje> trupat) : base(trupat)
        {

        }

        public colTrupiShitje(List<int> idKokat) : base(new clsDatabaseRegjistrim().MerrTrupaShitjeSipasIdKokash(idKokat))
        {


        }
        public colTrupiShitje(clsDatabaseRegjistrim dbRegj, List<int> idKokat) : base(dbRegj.MerrTrupaShitjeSipasIdKokash(idKokat))
        {


        }

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbRegjistrim.clsTrupiShitje"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary> 
        public new clsTrupiShitje this[int index]
        {
            get { return ((clsTrupiShitje)base[index]); }
        }
        /// <summary>
        /// mbush artikujt bashke me kodbare - krijo nje tjeter nese te duhen artikujt pa kodbare
        /// </summary>
        /// <returns></returns>

        public DbInventari.colArtikujt ktheColArtikuj()
            //TODO JURGENA - ktheje ne funksion qe e merr collectionin direkt nga databaza, dhe jo nje nga nje per cdo rresht
        {
            DbInventari.colArtikujt colArt = new DbInventari.colArtikujt();
            for (int i = 0, trupCount = this.Count; i < trupCount; i++)
            {
                clsTrupiShitje trupMag = this[i];
                if (trupMag.IdLlojVeprimi == 1)
                    colArt.Add(new DbInventari.clsArtikulli(trupMag.IdKodi));
                else
                    colArt.Add(new DbInventari.clsArtikulli());
            }
            return colArt;
        }

        public DbInventari.colKokatMakro ktheColMakrot()
        {
            DbInventari.colKokatMakro colmakro = new DbInventari.colKokatMakro();
            foreach (clsTrupiShitje trupMag in this)
            {
                if (trupMag.IdLlojVeprimi == 2)
                {
                    DbInventari.clsKokaMakro makro = new DbInventari.clsKokaMakro(trupMag.IdKodi);
                    colmakro.Add(makro);
                }
                else colmakro.Add(new DbInventari.clsKokaMakro());
            }
            return colmakro;
        }
        public DbKontabiliteti.colLlogarite ktheColLlogarite()
        {
            DbKontabiliteti.colLlogarite colllog = new DbKontabiliteti.colLlogarite();
            foreach (clsTrupiShitje trupMag in this)
            {
                if (trupMag.IdLlojVeprimi == 3)
                {
                    DbKontabiliteti.clsLlogari llog = new DbKontabiliteti.clsLlogari(trupMag.IdKodi);
                    colllog.Add(llog);
                }
                else colllog.Add(new DbKontabiliteti.clsLlogari());
            }
            return colllog;
        }
        public DbInventari.colDetajimeArtikulli ktheColDetArt()
        {
            DbInventari.colDetajimeArtikulli colDetArt = new DbInventari.colDetajimeArtikulli();
            foreach (clsTrupiShitje trupMag in this)
            {
                DbInventari.clsDetajimArtikulli detArt = new DbInventari.clsDetajimArtikulli(trupMag.IdDetajimArt);
                colDetArt.Add(detArt);
            }
            return colDetArt;
        }
        public DbInventari.colDetajimeArtikulli ktheColDetArt2()
        {
            DbInventari.colDetajimeArtikulli colDetArt = new DbInventari.colDetajimeArtikulli();
            foreach (clsTrupiShitje trupMag in this)
            {
                DbInventari.clsDetajimArtikulli detArt = new DbInventari.clsDetajimArtikulli(trupMag.IdDetajimArt2);
                colDetArt.Add(detArt);
            }
            return colDetArt;
        }
        public colNjesiAdministrative ktheColMag(int idPerdoruesi)
        {
            colNjesiAdministrative colMag = new colNjesiAdministrative();
            foreach (clsTrupiShitje trupMag in this)
            {
                clsNjesiAdministrative mag = new clsNjesiAdministrative(trupMag.IdMagazina);
                colMag.Add(mag);
            }
            return colMag;
        }
        public DbInventari.colNjesiteArtikulli ktheColNjesiArt()
        {
            DbInventari.colNjesiteArtikulli colNjesi = new DbInventari.colNjesiteArtikulli();
            foreach (clsTrupiShitje trupMag in this)
            {
                DbInventari.clsNjesiArtikulli njesiArt = new DbInventari.clsNjesiArtikulli(trupMag.IdNjesia);
                colNjesi.Add(njesiArt);
            }
            return colNjesi;
        }
        public DbCore.DbRegjistrim.colTaksa ktheColTaksa()
        {
            DbCore.DbRegjistrim.colTaksa coltaksa = new DbCore.DbRegjistrim.colTaksa();
            foreach (clsTrupiShitje trupMag in this)
            {
                DbCore.DbRegjistrim.clsTaksa taksa = new clsTaksa(trupMag.Tvsh);
                coltaksa.Add(taksa);
            }
            return coltaksa;
        }

        public IEnumerable<clsKategoriShpenzimi> ktheColKategoriShpenzimi()
        {
            colKategoriShpenzimi colKategoriShpenzimi = new colKategoriShpenzimi()
            {
                Capacity = this.Count
            };
            foreach (clsTrupiShitje trupiShitje in this)
            {
                var kategoriSh = new clsKategoriShpenzimi(trupiShitje.IdKategoriShpenzimi);
                colKategoriShpenzimi.Add(kategoriSh);
            }
            return colKategoriShpenzimi;
        }

        /// <summary>
        /// mbush trupin e shitjes
        /// </summary>
        /// <param name="idShitjeKoka">id e kokes</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushTrupiShitje(int idShitjeKoka)
        {
            clsDatabaseRegjistrim dbTrupShitje = new clsDatabaseRegjistrim();
            bool mbush = mbushTrupatShitje(dbTrupShitje.ktheTrupiShitje(idShitjeKoka));
            dbTrupShitje.Dispose();
            return mbush;
        }
        public bool mbushTrupiShitjeWebhook(int idShitjeKoka)
        {
            clsDatabaseRegjistrim dbTrupShitje = new clsDatabaseRegjistrim();
            bool mbush = mbushTrupatShitjeWebhook(dbTrupShitje.ktheTrupiShitje(idShitjeKoka));
            dbTrupShitje.Dispose();
            return mbush;
        }
        public bool mbushTrupiShitjePerWebhook(int idShitjeKoka)
        {
            clsDatabaseRegjistrim dbTrupShitje = new clsDatabaseRegjistrim();
            bool mbush = mbushTrupatShitjePerWebhook(dbTrupShitje.ktheTrupiShitje(idShitjeKoka),idShitjeKoka);
            dbTrupShitje.Dispose();
            return mbush;
        }
        public bool ktheTrupiShitjeDheAutorizime(int idShitjeKoka, int idperdorues, int idndermarje)
        {
            clsDatabaseRegjistrim dbTrupShitje = new clsDatabaseRegjistrim();
            bool mbush = mbushTrupatShitje(dbTrupShitje.ktheTrupiShitjeDheAutorizime(idShitjeKoka, idperdorues, idndermarje));
            dbTrupShitje.Dispose();
            return mbush;
        }

        /// <summary>
        /// mbush gjithe trupin e shitjes nga koka
        /// </summary>
        /// <param name="idShitjeKoka">id e trupit</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushGjitheTrupiShitjeNgaKoka(int idShitjeKoka)
        {
            using (clsDatabaseRegjistrim dbTrupShitje = new clsDatabaseRegjistrim())
            {
                return mbushTrupatShitje(dbTrupShitje.ktheGjitheTrupiShitjeNgaKoka(idShitjeKoka));
            }
        }

        public bool mbushGjitheTrupiShitjePaArtikujSherbimNgaKoka(int idShitjeKoka)
        {
            using (clsDatabaseRegjistrim dbTrupShitje = new clsDatabaseRegjistrim())
            {
                return mbushTrupatShitje(dbTrupShitje.ktheGjitheTrupiShitjePaArtikujSherbimNgaKoka(idShitjeKoka));
            }
        }
        public bool ktheGjitheTrupiShitjeNgaKokaKonvert(int idShitjeKoka, int idndermarje)
        {
            clsDatabaseRegjistrim dbTrupShitje = new clsDatabaseRegjistrim();
            bool mbush = mbushTrupatShitje(dbTrupShitje.ktheGjitheTrupiShitjeNgaKokaKonvert(idShitjeKoka, idndermarje));
            dbTrupShitje.Dispose();
            return mbush;
        }
        public bool ktheGjitheTrupiShitjePaArtikujSherbimNgaKokaKonvert(int idShitjeKoka, int idndermarje)
        {
            clsDatabaseRegjistrim dbTrupShitje = new clsDatabaseRegjistrim();
            bool mbush = mbushTrupatShitje(dbTrupShitje.ktheGjitheTrupiShitjePaArtikujSherbimNgaKokaKonvert(idShitjeKoka, idndermarje));
            dbTrupShitje.Dispose();
            return mbush;
        }
        public bool ktheGjitheTrupiShitjeNgaKokaKonvertBlerje(int idShitjeKoka, int idndermarje)
        {
            clsDatabaseRegjistrim dbTrupShitje = new clsDatabaseRegjistrim();
            bool mbush = mbushTrupatShitje(dbTrupShitje.ktheGjitheTrupiShitjeNgaKokaKonvertBlerje(idShitjeKoka, idndermarje));
            dbTrupShitje.Dispose();
            return mbush;
        }
        public bool ktheGjitheTrupiShitjePaArtikujSherbimNgaKokaKonvertBlerje(int idShitjeKoka, int idndermarje)
        {
            clsDatabaseRegjistrim dbTrupShitje = new clsDatabaseRegjistrim();
            bool mbush = mbushTrupatShitje(dbTrupShitje.ktheGjitheTrupiShitjePaArtikujSherbimNgaKokaKonvertBlerje(idShitjeKoka, idndermarje));
            dbTrupShitje.Dispose();
            return mbush;
        }
        public bool ktheGjitheTrupiShitjeNgaKokaKthim(int idShitjeKoka, int idndermarje)
        {
            clsDatabaseRegjistrim dbTrupShitje = new clsDatabaseRegjistrim();
            bool mbush = mbushTrupatShitje(dbTrupShitje.ktheGjitheTrupiShitjeNgaKokaKthim(idShitjeKoka, idndermarje));
            dbTrupShitje.Dispose();
            return mbush;
        }
        public bool mbushGjitheTrupiShitjeNgaKokaPerKthimVod(int idShitjeKoka)
        {
            clsDatabaseRegjistrim dbTrupShitje = new clsDatabaseRegjistrim();
            bool mbush = mbushTrupatShitje(dbTrupShitje.mbushGjitheTrupiShitjeNgaKokaPerKthimVod(idShitjeKoka));
            dbTrupShitje.Dispose();
            return mbush;
        }
        public bool mbushGjitheTrupiShitjeNgaKokaPerBlerje(int idShitjeKoka)
        {
            clsDatabaseRegjistrim dbTrupShitje = new clsDatabaseRegjistrim();
            bool mbush = mbushTrupatShitje(dbTrupShitje.mbushGjitheTrupiShitjeNgaKokaPerBlerje(idShitjeKoka));
            dbTrupShitje.Dispose();
            return mbush;
        }
        public bool ktheGjitheTrupiShitjeNgaKokaRezervime(int idShitjeKoka, int idndermarje)
        {
            clsDatabaseRegjistrim dbTrupShitje = new clsDatabaseRegjistrim();
            bool mbush = mbushTrupatShitje(dbTrupShitje.ktheGjitheTrupiShitjeNgaKokaRezervime(idShitjeKoka, idndermarje));
            dbTrupShitje.Dispose();
            return mbush;

        }
        public static DataTable merrTrupiShitjeTransferWKDT(int idtrasferimi)
        {
            clsDatabaseRegjistrim dbartikuj = new clsDatabaseRegjistrim();
            DataTable dt = dbartikuj.merrTrupiShitjeTransferWKDT(idtrasferimi);
            dbartikuj.Dispose();
            return dt;
        }



        public void BashkoTrupin(List<int> artikujPerBashkim)
        {
            var body =
                from trup in this.ShallowCopy().Where(x => artikujPerBashkim.Contains(x.IdKodi))
                group trup by new { IdKodi = trup.IdKodi, IdMag = trup.IdMagazina } into trupGrupuar
                select trupGrupuar.First();

            this.FindAllAndRemove(x => artikujPerBashkim.Contains(x.IdKodi));
            this.AddRange(body);
        }

        public colTrupiShitje ShallowCopy()
        {
            var col = new colTrupiShitje();
            foreach (var trup in this)
                col.Add(trup.ShallowCopy());
            return col;
        }
        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbRegjistrim.clsTrupiShitje"/> 
        /// </summary>
        private bool mbushTrupatShitje(DataTable dt)
        {

            foreach (DataRow rreshti in dt.Rows)
            {
                Add(new clsTrupiShitje(rreshti));
            }
            return true;
        } 
        private bool mbushTrupatShitjeWebhook(DataTable dt)
        {

            foreach (DataRow rreshti in dt.Rows)
            {
                Add(new clsTrupiShitje(rreshti,true));
            }
            return true;
        } 
        private bool mbushTrupatShitjePerWebhook(DataTable dt, int idShitjeKoka)
        {

            foreach (DataRow rreshti in dt.Rows)
            {
                Add(new clsTrupiShitje(rreshti, idShitjeKoka));
            }
            return true;
        }



        public void mbushTrupShitjeSipasRaportit(DataTable dt, int idndermarje, bool merrkonvertuar, out DbInventari.colArtikujt colart, out colNjesiAdministrative colmag, out DbInventari.colNjesiteArtikulli colnjesi, out colTaksa coltaksa)
        {
            DbAdmin.clsNdermarrje nderm = new DbAdmin.clsNdermarrje(idndermarje);
            colart = new DbInventari.colArtikujt();
            colmag = new colNjesiAdministrative();
            colnjesi = new DbInventari.colNjesiteArtikulli();
            coltaksa = new colTaksa();
            foreach (DataRow rreshti in dt.Rows)
            {
                var kodi = rreshti["KODARTIKULLI"].ToString();
                var pershkrimi= rreshti["PERSHKRIMARTIKULLI"].ToString();
                var njesia = rreshti["KODNJESIA"].ToString();
                var magazina=rreshti["KODI"].ToString();
                var idartikulli = int.Parse(rreshti["IDARTIKULLI"].ToString());
                var gjendje = double.Parse(rreshti["GJENDJASASI"].ToString());
                var maximum = double.Parse(rreshti["MAXIMUMARTIKULLI"].ToString());
                var sasipakonvertuar = double.Parse(rreshti["SASIAEPAKONVERTUAR"].ToString());
                DbInventari.clsArtikulli art = new DbInventari.clsArtikulli(idartikulli);
                if (art.Klasa == 5 || art.Klasa == 6)
                    continue;
                var idtvsh = 0;
                if (art.IdTvsh == 0)
                    idtvsh = nderm.IdTakse;
                else idtvsh = art.IdTvsh;
                clsTaksa taks = new clsTaksa(idtvsh);
                DbInventari.clsNjesiArtikulli njesi = new DbInventari.clsNjesiArtikulli();
                njesi.mbushNjesiArtikulliMeKod(njesia, idndermarje);
                var idnjesia = njesi.IdNjesia;
                clsNjesiAdministrative mag = new clsNjesiAdministrative(magazina, idndermarje);
                var idmagazina = mag.IdNjesiAdministrative;
                var sasia=0D;
                if (merrkonvertuar)
                    sasia = maximum - gjendje - sasipakonvertuar;
                else sasia = maximum - gjendje;
                if (sasia <= 0)
                    continue;
                clsTrupiShitje tr = new clsTrupiShitje(0, 0, 1, kodi, pershkrimi, 0, idnjesia, sasia, 0, 0, 0, idtvsh, 0, idartikulli, idmagazina, 
                    1, 1, 1, "", DateTime.Now.Date, DateTime.Now.Date, 0, 0, 0, 0, 0, 0, 0, null, "", 0, 0,0, 0, "", "");
              
                Add(tr);
                colart.Add(art);
                colnjesi.Add(njesi);
                colmag.Add(mag);
                coltaksa.Add(taks);
            }
            return ;
        }




        #endregion
    }
}
