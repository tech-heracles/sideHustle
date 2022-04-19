using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using DbCore.DbInventari;
using DbCore.DbKontabiliteti;
using DbCore.IMBUtils.Extensions;
using DbCore.IMBUtils.Logging;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsKokaShitje
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colKokaShitje : System.Collections.Generic.List<clsKokaShitje>
    {
        #region Konstruktor

        /// <summary>
        /// konstruktor pa parametra
        /// </summary>
        public colKokaShitje()
        {

        }
        public colKokaShitje(IEnumerable<clsKokaShitje> colleciceron) : base(colleciceron)
        {
            //colKokaShitje col = new colKokaShitje { Capacity = 1000000 };
            //Parallel.ForEach(colleciceron,new ParallelOptions { MaxDegreeOfParallelism=4}, x => {
            //    Add(x);
            //});

        }

        public colKokaShitje(string idte)
        {
            using (clsDatabaseRegjistrim db = new clsDatabaseRegjistrim())
            {
                mbushKokatShitjeDT(db.merrKokaShitjeSipasIdve(idte));
            }
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbRegjistrim.clsKokaShitje"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary> 
        public new clsKokaShitje this[int index]
        {
            get { return ((clsKokaShitje)base[index]); }
        }

        public static DataTable merrKokaShitjeDT(int idndermvit, int idkategori, int idperdorues, int idndermarje, bool merrstatusprodhimi, bool merrTegjitha, bool merrprodhuar, bool merrKonvertuarPlote, string datanga, string dataderi, int discountdevice, bool merrDokKonvertuar,string kodKonfigambjente)
        {
            try
            {
                using (clsDatabaseRegjistrim dbartikuj = new clsDatabaseRegjistrim())
                {
                    return dbartikuj.merrKokaShitjeDT(idndermvit, idkategori, idperdorues, idndermarje, merrstatusprodhimi, merrTegjitha, merrprodhuar, merrKonvertuarPlote, datanga, dataderi,discountdevice, merrDokKonvertuar, kodKonfigambjente);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static DataTable merrZbritjeSipasKokave(int[] idkokat)
        {
            try
            {
                using (clsDatabaseRegjistrim dbartikuj = new clsDatabaseRegjistrim())
                {
                    return dbartikuj.merrZbritjeSipasKokave(idkokat);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static DataRow merrKokaShitjeDR(int idkokashitje, int idndermarje)
        {
            try
            {
                using (clsDatabaseRegjistrim dbartikuj = new clsDatabaseRegjistrim())
                {
                    return dbartikuj.merrKokaShitjeDR(idkokashitje, idndermarje);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static DataTable merrKokaShitjePerTransferimNeISKSH(int idKatDok, string dataNga, string dataDeri, int idNdermarrja)
        {
            using (clsDatabaseRegjistrim db = new clsDatabaseRegjistrim())
                return db.merrKokaShitjePerTransferimNeISKSH(idKatDok, dataNga, dataDeri, idNdermarrja);
        }
        public static DataTable merrKokaShitjeMeTrupPerTransferimNeISKSH(string idTe)
        {
            using (clsDatabaseRegjistrim db = new clsDatabaseRegjistrim())
                return db.merrKokaShitjeMeTrupPerTransferimNeISKSH(idTe);
        }

        public static int hiqRezervimetLlotariKupon()
        {
            using (clsDatabaseRegjistrim dbRegjistrim = new clsDatabaseRegjistrim())
            {
                int rezervime = dbRegjistrim.nrRezervimeTeAnulluara();

                return rezervime;
            }
        }

        public static DataTable merrKokaShitjeDTTePaGjeneruara(int idndermvit, string llojdate, int idperdorues, int idndermarje, bool merrTegjitha, string datanga, string dataderi)
        {
            try
            {
                using (clsDatabaseRegjistrim dbartikuj = new clsDatabaseRegjistrim())
                {
                    return dbartikuj.merrKokaShitjeDTTePaGjeneruara(idndermvit, llojdate, idperdorues, idndermarje, merrTegjitha, datanga, dataderi);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static DataTable merrKokaShitjePaFatureTatimoreDT(int idperdorues, int idndermarje, int idndermarjevit, string dataNga, string dataDeri)
        {
            using (var dbartikuj = new clsDatabaseRegjistrim())
                return dbartikuj.merrKokaShitjePaFatureTatimoreDT(idperdorues, idndermarje, idndermarjevit, dataNga, dataDeri);
        }

        public static DataTable merrKokaShitjePaFatureTatimoreDTNew(int idperdorues, int idndermarje, bool merrTegjitha, int idndermarjevit, string dataNga, string dataDeri, string top, string filter)
        {
            clsDatabaseRegjistrim dbartikuj = new clsDatabaseRegjistrim();
            DataTable dt = dbartikuj.merrKokaShitjePaFatureTatimoreDTNew(idperdorues, idndermarje, merrTegjitha, idndermarjevit, dataNga, dataDeri, top, filter);
            dbartikuj.Dispose();
            return dt;
        }

        public static DataTable merrKokaShitjeDTKthimBlerjePrind(int idndermarje)
        {
            clsDatabaseRegjistrim dbartikuj = new clsDatabaseRegjistrim();
            DataTable dt = dbartikuj.merrKokaShitjeDTKthimBlerjePrind(idndermarje);
            dbartikuj.Dispose();
            return dt;
        }
        
        public static DataTable merrKokaShitjeTransferWKDT()
        {
            clsDatabaseRegjistrim dbartikuj = new clsDatabaseRegjistrim();
            DataTable dt = dbartikuj.merrKokaShitjeTransferWKDT();
            dbartikuj.Dispose();
            return dt;
        }

        public static DataTable merrShitjePerImport(string emerTabKoka, string emerTabTrupi, string ndermarrjeKey, string ndermarjeKodi, string nenkategoria, int lloji, string primaryKey, bool merrTePaImportuara, bool rimerrTeImportuara, int? nrDokumentash)
        {
            using (var dbImportShitje = new clsDatabaseRegjistrim()) 
                return dbImportShitje.merrShitjePerImport(emerTabKoka, emerTabTrupi, ndermarrjeKey, ndermarjeKodi, nenkategoria, lloji, primaryKey, merrTePaImportuara, rimerrTeImportuara, nrDokumentash);
          
        }

        public static DataTable merrShitjePerEksport(int idnderm, int idperdorues, int idKatDok, int idNdermViti, int lloji, string emerTabKoka, string emerFusheID, string idDokPerEksport, bool merrDokTeModifikuar, bool merrDokTeFshire, bool hiqRreshtaKomisioni, bool artikujSet, bool serialeUnike, string filterString, bool ekspAutomatik)
        {
            using (var dbImportShitje = new clsDatabaseRegjistrim())
                return dbImportShitje.ktheShitjePerExport(idnderm, idperdorues, idKatDok, idNdermViti, lloji, emerTabKoka, emerFusheID, idDokPerEksport, merrDokTeModifikuar, merrDokTeFshire, hiqRreshtaKomisioni, artikujSet, serialeUnike, filterString, ekspAutomatik);
           
        }

        public static DataTable merrShitjeTeGrupuaraPerEksport(int idnderm, int idperdorues, int idKatDok, int idNdermViti, int lloji, string emerTabKoka, string emerFusheID, string idDokPerEksport)
        {
            clsDatabaseRegjistrim dbImportShitje = new clsDatabaseRegjistrim();
            DataTable tabela = dbImportShitje.ktheShitjeTeGrupuaraPerExport(idnderm, idperdorues, idKatDok, idNdermViti, lloji, emerTabKoka, emerFusheID, idDokPerEksport);
            dbImportShitje.Dispose();
            return tabela;
        }

        public clsMesazh fshiUshPermbledhur(int idPerdoruesi, clsDatabaseRegjistrim dbRegj)
        {
            return dbRegj.fshiUshPermbledhur(idPerdoruesi, this.Select(x => x.IdShitjeKoka).ToList());
        }

        public clsMesazh KontrolloPermbledhur()
        {
            using (clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim())
            {
                try
                {
                    return dbRegj.KontrolloPermbledhur(this.Select(x => x.IdShitjeKoka).ToList());
                }
                catch (Exception ex)
                {
                    ImbLogger.LogErrorShitje($"Exception: {ex}");
                    return new clsMesazh(false, ex.Message);
                }
            }
        }

        public void MbushElementTrupi()
        {
            var teGjithaTrupat = this.SelectMany(x => x.OColTrupiShitje);

            var grupimiSipaLlojit = teGjithaTrupat.GroupBy(x => x.IdLlojVeprimi).Select(x => new
            {
                IdLlojVeprimi = x.Key,
                colTrupat = new colTrupiShitje(x)
            });
            var col = new Dictionary<int, colTrupiShitje>(2);
            foreach (var item in grupimiSipaLlojit)
            {
                col.Add(item.IdLlojVeprimi, item.colTrupat);
            }
            if (col.ContainsKey((int)llojRreshtiShitje.Artikull))
            {
                var trupaMeArtikujt = col[(int)llojRreshtiShitje.Artikull];

                List<int> idArtikujsh = new List<int>(trupaMeArtikujt.Select(y => y.IdKodi).Distinct());
                var artikujt = new colArtikujt(idArtikujsh);
                foreach (var trupi in trupaMeArtikujt)
                {
                    trupi.Element = artikujt.First(art => art.IdArtikulli == trupi.IdKodi);
                }
            }
            if (col.ContainsKey((int)llojRreshtiShitje.Llogari))
            {
                var trupatMeLlogari = col[(int)llojRreshtiShitje.Llogari];
                List<int> idLlogarish = new List<int>(trupatMeLlogari.Select(y => y.IdKodi).Distinct());
                var llogarit = new colLlogarite(idLlogarish);

                foreach (var trupi in trupatMeLlogari)
                {
                    trupi.Element = llogarit.First(llog => llog.IdLlogari == trupi.IdKodi);
                }
            }
        }

        public void MbushTrupat()
        {
            var idKokat = this.Select(x => x.IdShitjeKoka).ToList();
            var teGjithaTrupat = new colTrupiShitje(idKokat);
            //mbush gjithe kokat me trupat perkates
            ForEach(x =>
            {
                x.OColTrupiShitje = new colTrupiShitje(teGjithaTrupat.Where(trupi => trupi.IdShitjeKoka == x.IdShitjeKoka));
            });

        }
        #endregion

        #region Metoda Private

        public bool mbushKokatShitjeDT(DataTable dt)
        {
            return mbushKokatShitjeRowArray(dt.Select());
        }

        public bool mbushKokatShitjeRowArray(DataRow[] drs)
        {
            foreach (DataRow rreshti in drs)
            {
                Add(new clsKokaShitje(rreshti));
            }
            return true;
        }

        internal static DataRow MerrShitjeSipasNivf(string nivfKthim)
        {
            using (clsDatabaseRegjistrim db = new clsDatabaseRegjistrim())
            {
                return db.merrShitjeSipasNivf(nivfKthim);
            }
        }

        #endregion
    }
}
