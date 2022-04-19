using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using DbCore.DbInventari;
using DbCore.IMBUtils.Logging;
using DbCore.IMBUtils.Extensions;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsTrupiMagazina
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colTrupiMagazina : System.Collections.Generic.List<clsTrupiMagazina>
    {
        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbRegjistrim.clsTrupiMagazina"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary> 
        public new clsTrupiMagazina this[int index]
        {
            get { return ((clsTrupiMagazina)base[index]); }
        }

        public DbInventari.colArtikujt ktheColArtikuj() 
            //TODO JURGENA - ktheje ne funksion qe e merr collectionin direkt nga databaza, dhe jo nje nga nje per cdo rresht
        {
            DbInventari.colArtikujt colArt = new DbInventari.colArtikujt();
            foreach (clsTrupiMagazina trupMag in this)
                colArt.Add(new DbInventari.clsArtikulli(trupMag.IdArtikulli));
            return colArt;
        }

        public DbInventari.colArtikujt ktheColArtikujSet()
        {
            DbInventari.colArtikujt colArt = new DbInventari.colArtikujt();
            foreach (clsTrupiMagazina trupMag in this)
            {
               DbInventari.clsArtikulli art= new DbInventari.clsArtikulli(trupMag.IdArtikullSet);
                colArt.Add(art);
            }
            return colArt;
        }

        public DbInventari.colDetajimeArtikulli ktheColDetArt()
        {
            DbInventari.colDetajimeArtikulli colDetArt = new DbInventari.colDetajimeArtikulli();
            foreach (clsTrupiMagazina trupMag in this)
            {
                DbInventari.clsDetajimArtikulli detArt = new DbInventari.clsDetajimArtikulli(trupMag.IdDetajimi);
                colDetArt.Add(detArt);
            }
            return colDetArt;
        }

        public DbInventari.colDetajimeArtikulli ktheColDetArt2()
        {
            DbInventari.colDetajimeArtikulli colDetArt = new DbInventari.colDetajimeArtikulli();
            foreach (clsTrupiMagazina trupMag in this)
            {
                DbInventari.clsDetajimArtikulli detArt = new DbInventari.clsDetajimArtikulli(trupMag.IdDetajimi2);
                colDetArt.Add(detArt);
            }
            return colDetArt;
        }

        public colNjesiAdministrative ktheColMag(int idPerdorues)
        {
            colNjesiAdministrative colMag = new colNjesiAdministrative();
            foreach (clsTrupiMagazina trupMag in this)
            {
                clsNjesiAdministrative mag = new clsNjesiAdministrative(trupMag.IdMag);
                colMag.Add(mag);
            }
            return colMag;
        }

        public colNjesiAdministrative ktheColMagDest(int idkokamagazina, int idkonfig, int idPerdoruesi)
        {
            colNjesiAdministrative colMag = new colNjesiAdministrative();
            DbCore.DbRegjistrim.clsKokaMagazina kokatra = new clsKokaMagazina();
            kokatra.mbushKokaMagazinaSipasIDGjenerues(idkokamagazina, 1, idkonfig);
            colTrupiMagazina col = new colTrupiMagazina();
            col.mbushGjitheTrupiMagazinaNgaKoka(kokatra.IdKokaMagazina);
            foreach (clsTrupiMagazina trmagTras in col)
            {
                clsNjesiAdministrative mag = new clsNjesiAdministrative(trmagTras.IdMag);
                colMag.Add(mag);
            }
            return colMag;
        }

        public DbInventari.colNjesiteArtikulli ktheColNjesiArt()
        {
            DbInventari.colNjesiteArtikulli colNjesi = new DbInventari.colNjesiteArtikulli();
            foreach (clsTrupiMagazina trupMag in this)
            {
               
                DbInventari.clsNjesiArtikulli njesiArt = new DbInventari.clsNjesiArtikulli(trupMag.IdNjesia);
                colNjesi.Add(njesiArt);
            }
            return colNjesi;
        }
        
        /// <summary>
        /// mbush trupin e magazines sipas id se kokes se magazines
        /// </summary>
        /// <param name="idKokaMagazina">id koka e magazines</param>
        /// <returns>kthen true nese mbushja kruhet me sukses, ne te kundert false</returns>
        public bool mbushTrupiMagazina(int idKokaMagazina, clsDatabaseRegjistrim dbtrupMagazine)
        {
            return mbushTrupatMagazine(dbtrupMagazine.ktheTrupiMagazina(idKokaMagazina,false));
        }

        /// <summary>
        /// mbush trupin e magazines sipas id se kokes se magazines
        /// </summary>
        /// <param name="idKokaMagazina">id koka e magazines</param>
        /// <param name="PerjashtoArtAfgj">Ne rastet qe do te jete true nuk do te marri artikujt afategjate</param>
        /// <returns>kthen true nese mbushja kruhet me sukses, ne te kundert false</returns>
        public bool mbushTrupiMagazina(int idKokaMagazina,bool PerjashtoArtAfgj)
        {
            clsDatabaseRegjistrim dbtrupMagazine = new clsDatabaseRegjistrim();
            bool sukses = mbushTrupatMagazine(dbtrupMagazine.ktheTrupiMagazina(idKokaMagazina, PerjashtoArtAfgj));
            dbtrupMagazine.Dispose();
            return sukses;
        }    

        public static DataTable merrDataVeprimiPas(DateTime data, int idndermarje, int idmag, int idartikulli, clsDatabaseRegjistrim db)
        {
            if (db == null)
                db = new clsDatabaseRegjistrim();
            return db.merrDataVeprimiPas(data, idndermarje, idmag, idartikulli);
        }
        
        public void ShtoTrupinNeCollectionSeBashkuMeSerialet(clsTrupiMagazina trupMag, colSerialeUnikeMagazina serialetUnike, int shenja, int tvsh, bool krijuar)
        {
            ImbLogger.LogTraceShitje("Filloi metoda shtotrupinNeCollectionSeBashkuMeSerialet!");
            trupMag.MerrSerialetUnike(serialetUnike, shenja == -1, tvsh);
            if (trupMag.IdArtikulli != -1 && krijuar) //ky kusht duhet pare kur te shtohen makrot
            {
                if (trupMag.IdMag == -1)
                {
                    ImbLogger.LogErrorShitje("Magazina nuk ekziston!");
                    throw new Exception("Magazina nuk ekziston!");
                }
                trupMag.Shenja = shenja;
                this.Add(trupMag);
            }
            ImbLogger.LogTraceShitje("Mbaroi metoda shtotrupinNeCollectionSeBashkuMeSerialet!");
        }

        public void BashkoTrupin(colSerialeUnikeKategori kategorite, colTrupiMagazina trupi)
        {
            var newBody = this.ShallowCopy();
            var body = from trup in newBody.ShallowCopy().Where(x =>
            {
                var kat = kategorite.MerrKategoriSipasIdFormatit(((clsArtikulli)x.Element).IdFormatSeriali);
                return ((clsArtikulli)x.Element).DetajimArtikulli && kat != null && kat.Kategori.EqualsIgnoreCase(enumKategoriSerialesh.APARATE.ToString());
            })

            group trup by new { IdArtikulli = trup.IdArtikulli, KodiArtikull = trup.KodiArtikull, PershkrimArtikull = trup.PershkrimArtikull, IdLlojVeprimi = trup.IdLlojVeprimi, IdKokaMagazina = trup.IdKokaMagazina, Shenja = trup.Shenja, IdArtikullSet = trup.IdArtikullSet} into trupGrupuar
            select new clsTrupiMagazina
            {
                IdArtikulli = trupGrupuar.Key.IdArtikulli,
                KodiArtikull = trupGrupuar.Key.KodiArtikull,
                PershkrimArtikull = trupGrupuar.Key.PershkrimArtikull,
                IdLlojVeprimi = trupGrupuar.Key.IdLlojVeprimi,
                IdNjesia = trupGrupuar.First().IdNjesia,
                IdKokaMagazina = trupGrupuar.Key.IdKokaMagazina,
                Koeficenti = trupGrupuar.First().Koeficenti,
                Shenja = trupGrupuar.Key.Shenja,
                IdMag = trupGrupuar.First().IdMag,
                Data = trupGrupuar.First().Data,
                Element = trupGrupuar.First().Element,
                IdArtikullSet = trupGrupuar.Key.IdArtikullSet,
                IdBarkodi = trupGrupuar.First().IdBarkodi,
                Sasia = trupGrupuar.Sum(x => x.Sasia),
                Cmimi = trupGrupuar.Sum(x => x.Sasia) == 0 ? 0 : trupGrupuar.Sum(x => x.Vlefta) / trupGrupuar.Sum(x => x.Sasia),
                Vlefta = trupGrupuar.Sum(x => x.Vlefta),
                Shenime = trupGrupuar.First().Shenime
            };

            newBody.FindAllAndRemove(x =>
            {
                var kat = kategorite.MerrKategoriSipasIdFormatit(((clsArtikulli)x.Element).IdFormatSeriali);
                return ((clsArtikulli)x.Element).DetajimArtikulli && kat != null && kat.Kategori.EqualsIgnoreCase(enumKategoriSerialesh.APARATE.ToString());
            });
            newBody.AddRange(body);

            trupi.Clear();
            trupi.AddRange(newBody.AsEnumerable());
        }

        public void BashkoTrupin(colSerialeUnikeKategori kategorite)
        {

            BashkoTrupin(kategorite, this);
            
        }

        public void BashkoTrupin(List<Tuple<int, int>> artikujPerBashkim)
        {
            var body =
                from trup in this.ShallowCopy().Where(x => artikujPerBashkim.Exists(art => art.Item1 == x.IdArtikulli && art.Item2 == x.IdArtikullSet))
                group trup by new { IdKodi = trup.IdArtikulli, IdMag = trup.IdMag } into trupGrupuar
                select trupGrupuar.First();

            this.FindAllAndRemove(x => artikujPerBashkim.Exists(art => art.Item1 == x.IdArtikulli && art.Item2 == x.IdArtikullSet));
            this.AddRange(body);
        }

        public bool ValidoArtikujSet(DateTime dt)
        {
            var sete = this.Select(x => x.IdArtikullSet).Where(x => x != 0).Distinct();
            foreach (var set in sete)
            {
                var perbers = new colArtikulliPerberes();
                perbers.ktheArtikujPerberesSipasIdArtikullitKryesorDheDatesMeTeAfert(set, dt);
                var body = from trup in this.Where(x => x.IdArtikullSet == set)
                           let p = perbers.FirstOrDefault(x => x.IdLidheseArt == trup.IdArtikulli && x.IdArtikulliKryesor == trup.IdArtikullSet)
                           group trup by new
                           {
                               IdArtikulli = trup.IdArtikulli,
                               IdArtikullSet = trup.IdArtikullSet,
                               Koeficenti = p.Koeficienti
                           } into trupGrup
                           select new
                           {
                               IdArtikulli = trupGrup.Key.IdArtikulli,
                               IdArtikullSet = trupGrup.Key.IdArtikullSet,
                               Sasia = trupGrup.Sum(x => x.Sasia),
                               SasiaSet = trupGrup.Sum(x => x.Sasia) / (double)trupGrup.Key.Koeficenti
                           };

                if (body.Select(x => x.SasiaSet).Distinct().Count() > 1)
                    return false;

                if (body.Count() != perbers.Count())
                    return false;

            }

            return true;
        }

        private colTrupiMagazina ShallowCopy()
        {
            var col = new colTrupiMagazina();
            foreach (var trup in this)
                col.Add(trup.ShallowCopy());
            return col;
        }

        /// <summary>
        /// mbush te gjithe trupin e magazines nga kokat
        /// </summary>
        /// <param name="idKokaMagazina">id e koka magazines</param>
        /// <returns>kthen true nese mbushja kryhet me sukses ne te kundert false</returns>
        public bool mbushGjitheTrupiMagazinaNgaKoka(int idKokaMagazina)
        {
            clsDatabaseRegjistrim dbtrupMagazine = new clsDatabaseRegjistrim();
            bool mbush = mbushTrupatMagazine(dbtrupMagazine.ktheGjitheTrupiMagazinaNgaKoka(idKokaMagazina,false));
            dbtrupMagazine.Dispose();
            return mbush;
        }

        public bool mbushGjitheTrupiMagazinaDaljeNgahyrja(int idKokaMagazina)
        {
            clsDatabaseRegjistrim dbtrupMagazine = new clsDatabaseRegjistrim();
            bool mbush = mbushTrupatMagazine(dbtrupMagazine.mbushGjitheTrupiMagazinaDaljeNgahyrja(idKokaMagazina));
            dbtrupMagazine.Dispose();
            return mbush;
        }

        public bool mbushGjitheTrupiMagazinaNgaKoka(int idKokaMagazina, clsDatabaseRegjistrim dbtrupMagazine)
        {
            bool mbush = mbushTrupatMagazine(dbtrupMagazine.ktheGjitheTrupiMagazinaNgaKoka(idKokaMagazina,false));

            return mbush;
        }

        /// <summary>
        /// mbush rreshtat per fifo te artikujve te detajuar
        /// </summary>
        /// <param name="idArt">id e artikullit</param>
        /// <param name="iddetajimi">id e detajimit</param>
        /// <param name="idmag">id e magazines</param>
        /// <param name="dt">data</param>
        /// <param name="idrenditjes">id e renditjes</param>
        /// <param name="fifo2">false nqs eshte fifo1, true nqs eshte fifo2</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        public bool mbushReshtaPerFifoArtikulliDetajim(int idArt, int iddetajimi, int idmag, DateTime dt, int idrenditjes, clsDatabaseRegjistrim dbRegj, bool fifo2)
        {
            return mbushTrupatMagazine(dbRegj.ktheReshtaPerFifoArtikulliDetajim(idArt, iddetajimi, idmag, dt, idrenditjes, fifo2));
        }

        public bool MerrGjendjeArtikulliMeImeiPerDaten(int idartikulli, DateTime data)
        {

            clsDatabaseRegjistrim db = new clsDatabaseRegjistrim();
            return mbushTrupatMagazine(db.MerrGjendjeArtikulliMeImeiPerDaten(idartikulli, data));
        }
        

        /// <summary>
        /// mbush rreshtat per rivlersim fifo te artikullit
        /// </summary>
        /// <param name="idartikulli">id e artikullit</param>
        /// <param name="idmagazina">id e magazines</param>
        /// <param name="datanga">data nga </param>
        /// <param name="dataderi">data deri</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        public bool mbushReshtaPerRivleresimFifoArtikulli(int idartikulli, int idmagazina, DateTime datanga, DateTime dataderi)
        {
            using (clsDatabaseRegjistrim dbtrupMagazine = new clsDatabaseRegjistrim()) {
                return mbushReshtaPerRivleresimFifoArtikulli(idartikulli, idmagazina, datanga, dataderi, dbtrupMagazine);
            }
        }
        
        /// <summary>
        /// mbush Element me artikujt nga db-ja. Ocoltrupi duhet te jete i mbushur qe te funksionoj
        /// </summary>
        /// <returns></returns>
        internal clsMesazh mbushArtikujTrupi(int idKokaMagazina)
        {            
            DbInventari.colArtikujt colArtikujsh = new DbInventari.colArtikujt(idKokaMagazina, new DbInventari.clsDatabaseInventari());
            int i = 0;
            if (colArtikujsh.Count != this.Count)
                return new clsMesazh(false,"Artikujt e dokumentit te magazines me id:"+ idKokaMagazina + " nuk perkon me numrin e artikujve te marre nga db-ja");
            foreach (DbRegjistrim.clsTrupiMagazina trup in this)
            {
                if (trup.IdLlojVeprimi == 1)
                    trup.Element = colArtikujsh[i];
                i++;
            }
            return new clsMesazh(true, "artikujt e dokumentit te magazines me id:"+ idKokaMagazina + " u mbushen me sukses nga db-ja");
        }

        public bool mbushReshtaPerRivleresimFifoArtikulli(int idartikulli, int idmagazina, DateTime datanga, DateTime dataderi, int maxRetry, DbAdmin.clsLogRivleresimInventari log)
        {
            int retry = maxRetry;
            do
            {
                try
                {
                    return mbushReshtaPerRivleresimFifoArtikulli(idartikulli, idmagazina, datanga, dataderi);
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    if (ex.Number == 1205 || ex.Number == 121 || ex.Number == 1236)
                    {
                        System.Threading.Thread.Sleep(1435);
                        log.logRetry(maxRetry - retry + 1, String.Format("mbushReshtaPerRivleresimFifoArtikulli({0},{1},{2},{3})", idartikulli, idmagazina, datanga, dataderi), ex.Number, ex.Message);
                        if (--retry == 0)
                        {
                            log.logError(maxRetry + ": Tentativat per Rivleresimin mbaruan ju lutem riprovoni me vone");
                            throw new MyException(maxRetry + ": Tentativat per Rivleresimin mbaruan ju lutem riprovoni me vone");
                        }
                    }
                    else
                    {
                        log.logError(ex.Number, ex.Message);
                        throw ex;
                    }
                }
                catch (Exception ex)
                {
                    log.logError(ex.Message);
                    throw ex;
                }
            } while (retry > 0);
            throw new MyException("ERROR: nuk u arrit leximi i: " + String.Format("mbushReshtaPerRivleresimFifoArtikulli({0},{1},{2},{3})", idartikulli, idmagazina, datanga, dataderi));
        }

        /// <summary>
        /// mbush rreshtat per rivlersim fifo te artikullit
        /// </summary>
        /// <param name="idartikulli">id e artikullit</param>
        /// <param name="idmagazina">id e magazines</param>
        /// <param name="datanga">data nga </param>
        /// <param name="dataderi">data deri</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        public bool mbushReshtaPerRivleresimFifoArtikulli(int idartikulli, int idmagazina, DateTime datanga, DateTime dataderi, clsDatabaseRegjistrim dbtrupMagazine)
        {            
            return mbushTrupatMagazine(dbtrupMagazine.ktheReshtaPerRivleresimFifoArtikulli(idartikulli, idmagazina, datanga, dataderi));            
        }

        public bool ktheGjitheTrupiMagazinaNgaKokaRezervime(int idShitjeKoka, int idndermarje)
        {
            clsDatabaseRegjistrim dbTrupShitje = new clsDatabaseRegjistrim();
            bool mbush = mbushTrupatMagazine(dbTrupShitje.ktheGjitheTrupiMagazinaNgaKokaRezervime(idShitjeKoka, idndermarje));
            dbTrupShitje.Dispose();
            return mbush;
        }

        public static DataTable ktheGjitheTrupiMagazinaMerrKodbargjendjeSipasDatesdheMagazines(int idmag, int idndermarje, DateTime data)
        {
            using (clsDatabaseRegjistrim dbTrupShitje = new clsDatabaseRegjistrim())
            {
               return dbTrupShitje.ktheGjitheTrupiMagazinaMerrKodbargjendjeSipasDatesdheMagazines(idmag, idndermarje, data);
               
            }
        }

        public static DataTable ktheGjitheTrupiMagazinaMerrSerialgjendjeSipasDatesdheMagazines(int idmag, int idndermarje, DateTime data)
        {
            using (clsDatabaseRegjistrim dbTrupShitje = new clsDatabaseRegjistrim())
            {
                return dbTrupShitje.ktheGjitheTrupiMagazinaMerrSerialgjendjeSipasDatesdheMagazines(idmag, idndermarje, data);
               
            }
        }

        public static DataTable ktheGjitheTrupiMagazinaMerrSerialgjendjeSipasSerialitdheMagazines(int idmag, int idndermarje, int idserial)
        {
            using (clsDatabaseRegjistrim dbTrupShitje = new clsDatabaseRegjistrim())
            {
                return dbTrupShitje.ktheGjitheTrupiMagazinaMerrSerialgjendjeSipasSerialitdheMagazines(idmag, idndermarje, idserial);
               
            }
        }

        public bool ktheGjitheTrupiMagazinaNgaKokaKonvertim(int idShitjeKoka, int idndermarje,bool afgj)
        {
            clsDatabaseRegjistrim dbTrupShitje = new clsDatabaseRegjistrim();
            bool mbush = mbushTrupatMagazine(dbTrupShitje.ktheGjitheTrupiMagazinaNgaKokaKonvertim(idShitjeKoka, idndermarje,afgj));
            dbTrupShitje.Dispose();
            return mbush;
        }

        public bool ktheGjitheTrupiMagazinaNgaKokaKlonim(int idKatDok, int id)
        {
            using (clsDatabaseInventari db = new clsDatabaseInventari())
                return mbushTrupatMagazine(db.ktheGjitheTrupiMagazinaNgaKokaKlonim(idKatDok, id));
        }

        public bool ktheGjitheTrupiMagazinaNgaKokaKonvertimUSH(int idShitjeKoka, int idndermarje,bool afgj)
        {
            clsDatabaseRegjistrim dbTrupShitje = new clsDatabaseRegjistrim();
            bool mbush = mbushTrupatMagazine(dbTrupShitje.ktheGjitheTrupiMagazinaNgaKokaKonvertimUSH(idShitjeKoka, idndermarje,afgj));
            dbTrupShitje.Dispose();
            return mbush;
        }

        public bool ktheGjitheTrupiMagazinaNgaKokaKonvertimUD(int idShitjeKoka, int idndermarje,bool afgj)
        {
            clsDatabaseRegjistrim dbTrupShitje = new clsDatabaseRegjistrim();
            bool mbush = mbushTrupatMagazine(dbTrupShitje.ktheGjitheTrupiMagazinaNgaKokaKonvertimUD(idShitjeKoka, idndermarje,afgj));
            dbTrupShitje.Dispose();
            return mbush;
        }
        public void mbushTrupMagazinaSipasRaportit(DataTable dt, int idndermarje,int idperdorues,string magdest, out DbInventari.colArtikujt colart, out colNjesiAdministrative colmag, out colNjesiAdministrative colmagdest, out DbInventari.colNjesiteArtikulli colnjesi, out DbInventari.colDetajimeArtikulli coldet2, out DbInventari.colDetajimeArtikulli coldet1)
        {
            var desti = "";
            DataTable da = DbCore.DbRegjistrim.colNjesiAdministrative.merrSipasNjesiNdermarrjesDT(idndermarje, idperdorues);

            desti = (magdest == "" ? da.Rows[0]["Kodi"].ToString() : magdest);
             
            DbAdmin.clsNdermarrje nderm = new DbAdmin.clsNdermarrje(idndermarje);
            colart = new DbInventari.colArtikujt();
            colmag = new colNjesiAdministrative();
            colmagdest = new colNjesiAdministrative();
            colnjesi = new DbInventari.colNjesiteArtikulli();
            coldet1 = new DbInventari.colDetajimeArtikulli();
            coldet2 = new DbInventari.colDetajimeArtikulli();
            foreach (DataRow rreshti in dt.Rows)
            {
                var kodi = rreshti["KODI"].ToString();
                var pershkrimi = rreshti["PERSHKRIMI"].ToString();
                var njesia = rreshti["NJESIA"].ToString();
                var magazina = rreshti["MAGAZINA"].ToString();
                var idMagDestinacion=0;
                var gjendje = double.Parse(rreshti["GJENDJA"].ToString());
                var detajim1= rreshti["DETAJIMI1"].ToString();
                var detajim2 = rreshti["DETAJIMI2"].ToString();
                int iddet1 = clsDetajimArtikulli.ktheIdDetajimi(detajim1, idndermarje);
                int iddet2 = clsDetajimArtikulli.ktheIdDetajimi(detajim2, idndermarje);
                DbInventari.clsDetajimArtikulli det1 = new DbInventari.clsDetajimArtikulli(iddet1);
                DbInventari.clsDetajimArtikulli det2 = new DbInventari.clsDetajimArtikulli(iddet2);
                int idartikulli = clsArtikulli.ktheIdArtikulli(kodi, idndermarje);
                DbInventari.clsArtikulli art = new DbInventari.clsArtikulli(idartikulli);
                var idtvsh = 0;
                if (art.IdTvsh == 0)
                    idtvsh = nderm.IdTakse;
                else idtvsh = art.IdTvsh;
                clsTaksa taks = new clsTaksa(idtvsh);
                DbInventari.clsNjesiArtikulli njesi = new DbInventari.clsNjesiArtikulli();
                njesi.mbushNjesiArtikulliMeKod(njesia, idndermarje);
                var idnjesia = njesi.IdNjesia;
                clsNjesiAdministrative mag = new clsNjesiAdministrative(magazina, idndermarje);

                clsNjesiAdministrative magdes = new clsNjesiAdministrative(desti, idndermarje);
                var idmagazina = mag.IdNjesiAdministrative;
                var idmagdest = magdes.IdNjesiAdministrative;
                clsTrupiMagazina tr = new clsTrupiMagazina(0, 0, 1,idartikulli, kodi, pershkrimi, idnjesia, gjendje, 1, 1, 0, 0, 0,0, idmagazina, DateTime.Now.Date, 1, 0, iddet1, 0, 0, iddet2, 0,0,0,0 ,0,0,null, "",  0,0);
                
                Add(tr);
                colmagdest.Add(magdes);
                coldet2.Add(det2);
                coldet1.Add(det1);
                colart.Add(art);
                colnjesi.Add(njesi);
                colmag.Add(mag);
               
            }
            return;

        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        /// <see cref="DbCore.DbRegjistrim.clsTrupiMagazina"/> 
        /// </summary>
        private bool mbushTrupatMagazine(DataTable dt)
        {
            for (int i = 0, count = dt.Rows.Count; i < count; i++)
            {
                Add(new clsTrupiMagazina(dt.Rows[i]));
            }
            return true;
        }

        #endregion
    }
}