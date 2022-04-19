using DbCore;
using DbCore.DbInventari;
using DbCore.DbRegjistrim;
using DbCore.IMBUtils;
using DbCore.IMBUtils.Extensions;
using DbCore.IMBUtils.Messages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.SessionState;

namespace RestApi.WebAPI.Models
{
    public class SerialeUnikeRepository
    {

        public static (clsMesazh, clsMesazh) DergoFileTransferimiSerialeUnike(int idNdermarje, string kategoriSeriali, string data, int idLlojDokumentMag, int idMetoda)
        {
            try
            {
                return TransferimSerialeUnike.dergoFileTransferimSerialeUnike(idNdermarje, kategoriSeriali, data, idLlojDokumentMag, idMetoda);
            }
            catch (Exception ex)
            {
                return (new clsMesazh(true, ""),new clsMesazh(false, ex.Message));
            }
        }
        public static (clsMesazh, clsMesazh) DergoFileTransferimiSerialeUnike(int idNdermarje, string kategoriSeriali, string data, int idLlojDokumentMag, int[] idMetoda)
        {
            try
            {
                clsMesazh suksese = new clsMesazh(true, "");
                clsMesazh errore = new clsMesazh(false, "");
                foreach (int i in idMetoda)
                {
                    (clsMesazh sukses, clsMesazh error) = TransferimSerialeUnike.dergoFileTransferimSerialeUnike(idNdermarje, kategoriSeriali, data, idLlojDokumentMag, i);
                    //if (sukses.PershkrimMesazhi == "Nuk ka asnje te dhene per te transferuar ne kete date!")
                    //    return (sukses, new clsMesazh(false, ""));
                    if (!suksese.PershkrimMesazhi.Contains(sukses.PershkrimMesazhi)) suksese.PershkrimMesazhi += sukses.PershkrimMesazhi + "<br>";
                    if (sukses.Tipi == TipMesazhi.Informim) suksese.Tipi = TipMesazhi.Informim;
                    if (!errore.PershkrimMesazhi.Contains(error.PershkrimMesazhi)) errore.PershkrimMesazhi += error.PershkrimMesazhi + "<br>";
                }
                return (suksese, errore);
            }
            catch (Exception ex)
            {
                return (new clsMesazh(true, ""), new clsMesazh(false, ex.Message));
            }
        }

        public static (clsMesazh, clsMesazh) DergoFileTransferimiGjendjeAparate(int idNdermarje, string data, int idMetodeTransferimi)
        {
            try
            {
                return TransferimSerialeUnike.dergoFileTransferimGjendjeAparate(idNdermarje, data, idMetodeTransferimi);
            }
            catch (Exception ex)
            {
                return (new clsMesazh(true, ""), new clsMesazh(false, ex.Message));
            }
        }

        internal static bool HiqSerialet(HttpSessionState session, string guidString)
        {
            mySessionObjects.hiqObjectNeSesion(session, Constants.SERIALE_UNIKE_TE_NGARKUAR, guidString);
            mySessionObjects.hiqObjectNeSesion(session, Constants.SERIALE_UNIKE_PER_TU_SHTUAR, guidString);
            mySessionObjects.hiqObjectNeSesion(session, Constants.SERIALE_UNIKE_PER_TU_SHTUAR_OLD, guidString);
            return true;
        }

        internal static bool MbyllLupeSerialesh(HttpSessionState session, string guidString, bool kaNdryshime)
        {
            var serialeTeNgarkuar = mySessionObjects.MerrNgaSession<colSerialeUnikeMagazina>(session, Constants.SERIALE_UNIKE_TE_NGARKUAR, guidString);
            var serialePerTuShtuar = mySessionObjects.MerrNgaSession<colSerialeUnikeMagazina>(session, Constants.SERIALE_UNIKE_PER_TU_SHTUAR, guidString);
            var serialePerTuShtuarOld = mySessionObjects.MerrNgaSession<colSerialeUnikeMagazina>(session, Constants.SERIALE_UNIKE_PER_TU_SHTUAR_OLD, guidString);
            var serialet = new colSerialeUnikeMagazina();
            if (serialeTeNgarkuar != null)
                serialet.AddRange(serialeTeNgarkuar);
            if (kaNdryshime)
            {
                if (serialePerTuShtuarOld != null)
                    serialet.AddRange(serialePerTuShtuarOld);
            }
            else
            {
                if (serialePerTuShtuar != null)
                    serialet.AddRange(serialePerTuShtuar);
            }
            mySessionObjects.RuajNeSession<colSerialeUnikeMagazina>(session, serialet.Any() ? serialet : null, Constants.SERIALE_UNIKE_TE_NGARKUAR, guidString);
            return true;
        }

        internal static bool HiqSerial(HttpSessionState session, string guidString, string serial)
        {
            var seriale = mySessionObjects.MerrNgaSession<colSerialeUnikeMagazina>(session, Constants.SERIALE_UNIKE_PER_TU_SHTUAR, guidString);
            if (seriale == null)
                seriale = new colSerialeUnikeMagazina();

            HiqSerial(guidString, serial, seriale);

            mySessionObjects.RuajNeSession(session, seriale, Constants.SERIALE_UNIKE_PER_TU_SHTUAR, guidString);

            return true;
        }

        private static clsSerialeUnikeMagazina HiqSerial(string guidString, string serial, colSerialeUnikeMagazina seriale)
        {
            clsSerialeUnikeMagazina seriali = seriale.Find(s => s.SerialiKryesore == serial || s.MerrSerialDytesor() == serial);
            if (seriali != null)
            {
                seriale.Remove(seriali);
                return seriali;
            }

            return null;
        }

        public static bool HiqSerialetPerArtikullDheMagazine(HttpSessionState session, string guidString, int idArtikulli, string kodMag, int idNdermarrje, bool set)
        {
            var seriale = mySessionObjects.MerrNgaSession<colSerialeUnikeMagazina>(session, Constants.SERIALE_UNIKE_TE_NGARKUAR, guidString);
            if (seriale == null) return true;
            IEnumerable<clsSerialeUnikeMagazina> perFshirje = null;

            if (string.IsNullOrEmpty(kodMag))
            {
                if (set)
                    perFshirje = seriale.Where(ser => ser.IdArtikulli == idArtikulli && ser.IdSeti != 0);
                else
                {
                    perFshirje = seriale.Where(ser => ser.IdArtikulli == idArtikulli && ser.IdSeti == 0);

                    if (perFshirje.Count() == 0)
                        perFshirje = seriale.Where(ser => ser.IdSeti == idArtikulli);
                }
                seriale.RemoveFromList(perFshirje?.ToList());
            }
            else
            {
                int idMag = clsNjesiAdministrative.ktheIdMagazine(kodMag, idNdermarrje);
                if (set)
                    perFshirje = seriale.Where(ser => ser.IdArtikulli == idArtikulli && ser.IdSeti != 0);
                else
                {
                    perFshirje = seriale.Where(ser => ser.IdArtikulli == idArtikulli && ser.IdMag == idMag && ser.IdSeti == 0);
                    if (perFshirje.Count() == 0)
                        perFshirje = seriale.Where(ser => ser.IdSeti == idArtikulli && ser.IdMag == idMag);
                }
            }
            seriale.RemoveFromList(perFshirje?.ToList());
            mySessionObjects.RuajNeSession(session, seriale, Constants.SERIALE_UNIKE_TE_NGARKUAR, guidString);
            return seriale.Count > 0;
        }

        internal static object ShtoSerial(HttpSessionState session, string guidString, string seriali, DateTime date, int idNdermarrje, string serialiIVjeter, int sasiaEKerkuar, string ArtikujSet, int idMag, bool gjenerimAutomatik, bool KontrolloSasi, string artikujMeSasi, bool RiktheSerialTevjeter, int idDok, bool Kthim, string IdKthimesh, bool shitje, bool MerrMagazinePerberesi)
        {
            var serialetPerTuShtuar = mySessionObjects.MerrNgaSession<colSerialeUnikeMagazina>(session, Constants.SERIALE_UNIKE_PER_TU_SHTUAR, guidString) ?? new colSerialeUnikeMagazina();
            var serialetTeNgarkuar = mySessionObjects.MerrNgaSession<colSerialeUnikeMagazina>(session, Constants.SERIALE_UNIKE_TE_NGARKUAR, guidString) ?? new colSerialeUnikeMagazina();

            var serialet = new colSerialeUnikeMagazina();
            serialet.AddRange(serialetTeNgarkuar);
            serialet.AddRange(serialetPerTuShtuar);

            var toReturn = ShtoSerial(serialet, serialetPerTuShtuar, guidString, seriali, date, idNdermarrje, serialiIVjeter, sasiaEKerkuar, ArtikujSet, idMag, gjenerimAutomatik, KontrolloSasi, artikujMeSasi, RiktheSerialTevjeter, idDok, Kthim, IdKthimesh, shitje, MerrMagazinePerberesi);

            mySessionObjects.RuajNeSession(session, serialetPerTuShtuar, Constants.SERIALE_UNIKE_PER_TU_SHTUAR, guidString);

            return toReturn;
        }

        private static object ShtoSerial(colSerialeUnikeMagazina serialetTeNgarkuar, colSerialeUnikeMagazina serialetPerTuShtuar, string guidString, string seriali, DateTime date, int idNdermarrje, string serialiIVjeter, int sasiaEKerkuar, string ArtikujSet, int idMag, bool gjenerimAutomatik, bool KontrolloSasi, string artikujMeSasi, bool RiktheSerialTevjeter, int idDok, bool Kthim, string IdKthimesh, bool shitje, bool MerrMagazinePerberesi)
        {
            try
            {
                colSerialeUnikeKategori serialeKategori = new colSerialeUnikeKategori(idNdermarrje);
                (clsSerialeUnikeKategori kategori, clsSerialeUnike serialiUnik, int idFormati) = serialeKategori.MerrKategoriDheSerialKryesorSipasSerialitUnik(seriali);

                if (kategori == null)
                    return new { Mesazh = new MesazhGabimi(MessagesResource.Messages["serialiNukIPerketAsnjeKategorie"].Replace("XXX", seriali)) };

                int sasiaBazePerSerialin = 1;//sasia minimale per nje serial. Eshte 1 per te gjithe me perjashtim te ringarkuesve ku ne varesi te serialit mund te jete 1/10/100
                if (kategori.Kategori.EqualsIgnoreCase(enumKategoriSerialesh.RINGARKUES.ToString()))
                {

                    sasiaBazePerSerialin = (int)clsSerialeUnikeRingarkues.gjejSasine(seriali, serialiUnik.FormuleSpecifike);
                    if (!gjenerimAutomatik)
                    {
                        sasiaEKerkuar = sasiaBazePerSerialin;
                    }
                    else if (sasiaEKerkuar < sasiaBazePerSerialin)
                    {
                        return new { Mesazh = new MesazhGabimi(MessagesResource.Messages["ringarkuesSasiMeEVogelSeSasiaMinimale"].Replace("#SASIA", sasiaBazePerSerialin.ToString()).Replace("#SERIALI", seriali)) };
                    }
                }

                bool fshiSerial = !string.IsNullOrEmpty(serialiIVjeter);
                if (serialetTeNgarkuar.GjendetSerialiNeTrup(seriali))
                {
                    if (sasiaEKerkuar == sasiaBazePerSerialin)
                        return new { Mesazh = new MesazhGabimi(MessagesResource.Messages["serialiEshteNgarkuarNeListe"].Replace("XXX", seriali)) };
                    else if (!kategori.Kategori.EqualsIgnoreCase(enumKategoriSerialesh.RINGARKUES.ToString()))
                    {
                        fshiSerial = fshiSerial && seriali != serialiIVjeter;
                        sasiaEKerkuar -= sasiaBazePerSerialin;
                    }
                    else
                    {
                        return new { Mesazh = new MesazhGabimi(MessagesResource.Messages["serialiEshteNgarkuarNeListe"].Replace("XXX", seriali)) };
                    }
                }
                clsSerialeUnikeMagazina serialiIFshire = null;
                if (fshiSerial)
                    serialiIFshire = HiqSerial(guidString, serialiIVjeter, serialetPerTuShtuar);


                DataTable dt = new DataTable();
                if (Kthim)
                {
                    if ((JsonConvert.DeserializeObject<int[]>(IdKthimesh))[0] == 0)//rasti kur do behen kthim orderat ekzistues ne web dhe porosia eshte marr nga WL Karta dhe jo nga ndermarrja e magazines ne Web. Ne kete rast nuk mund te behet lidhja me fshmag te orderit nepermjet idtrupikthimi, sepse ai nuk gjendet. Kontrolli do behet duke perdorur imeit te FBKTHIM dhe jo te T_SERIALEUNIKE_LIDHJE_MAGAZINE. Vendosur te clsSerialeUnikeMagazina duke mos pasur vend me te mire.
                        dt = clsSerialeUnikeMagazina.MerrSerialPerKthimNgaDetajim(seriali, idDok);

                    else
                        dt = clsSerialeUnikeMagazina.MerrSerialPerKthim(seriali, JsonConvert.DeserializeObject<int[]>(IdKthimesh).Join('_', x => x.ToString()), idDok);

                    if (dt.Rows.Count == 0)
                        return new { Mesazh = new MesazhGabimi(MessagesResource.Messages["serialiNukGjendetNeKthim"]) };

                }
                else
                {
                    dt = clsSerialeUnikeMagazina.MerrGjendjeSeriali(seriali, serialiUnik.SerialKryesor, date, idNdermarrje, kategori.Kategori, idDok);
                    if (dt.Rows.Count == 0)
                        return new { Mesazh = new MesazhGabimi(MessagesResource.Messages["serialiNukEkziston"].Replace("XXX", seriali)) };
                }
                int idArtikulli = Convert.ToInt32(dt.Rows[0]["IDARTIKULL"]);
                double sasi = Convert.ToDouble(dt.Rows[0]["SASI"]);
                var artikulli = new clsArtikulli(idArtikulli);


                if (idMag == 0)
                {
                    idMag = Convert.ToInt32(dt.Rows[0]["IDMAG"]);
                }

                var ArtikujtMeSasi = serialetTeNgarkuar.MerrArtikujMeSasi(artikujMeSasi, KontrolloSasi, false, !shitje);
                colArtikulliPerberes artPerb = MerrArtikujSet(ArtikujSet, date, serialetTeNgarkuar, idNdermarrje, shitje, MerrMagazinePerberesi);

                if (artPerb != null && artPerb.Exists(x => x.IdLidheseArt == artikulli.IdArtikulli && x.getIdMag() == Convert.ToInt32(dt.Rows[0]["IDMAG"]) && x.getSasiENevojshme() >= sasi))
                    idMag = Convert.ToInt32(dt.Rows[0]["IDMAG"]);

                var mesazh = KontrolloGjendje(dt, seriali, sasi, sasiaBazePerSerialin, idMag, Kthim);

                if (!mesazh)
                    return new { Mesazh = mesazh };


                mesazh = KontrolloArtikull(ArtikujtMeSasi, artPerb, artikulli, idMag, gjenerimAutomatik ? sasiaEKerkuar : sasi, KontrolloSasi, seriali, shitje);

                if (!mesazh)
                {
                    if (RiktheSerialTevjeter && serialiIFshire != null)
                        serialetPerTuShtuar.Add(serialiIFshire);
                    return new { Mesazh = mesazh };
                }
                if (gjenerimAutomatik && sasiaEKerkuar > 0)
                {
                    string ekzistues = colSerialeUnikeMagazina.MerrSerialetEkzistues(serialetPerTuShtuar, serialiUnik.SerialKryesor);
                    if (Kthim)
                    {
                        dt = clsSerialeUnikeMagazina.MerrSerialetPerKthimNeRradhe(seriali, JsonConvert.DeserializeObject<int[]>(IdKthimesh).Join('_', x => x.ToString()), idDok, sasiaEKerkuar, ekzistues);
                    }
                    else if (!kategori.Kategori.EqualsIgnoreCase(enumKategoriSerialesh.RINGARKUES.ToString()))
                        dt = clsSerialeUnikeMagazina.MerrSerialetNeRradhe(idArtikulli, idNdermarrje, date, serialiUnik.SerialKryesor, seriali, sasiaEKerkuar, ekzistues, idMag, idDok);
                    else
                        dt = clsSerialeUnikeMagazina.MerrSerialetRingarkuesNeRradhe(idArtikulli, idNdermarrje, date, serialiUnik.SerialKryesor, seriali, sasiaEKerkuar - (int)sasi, ekzistues, idMag, dt, idDok);

                    int sasia = clsSerialeUnikeMagazina.MerrSasine(dt);
                    if (sasia != sasiaEKerkuar)
                    {
                        if (RiktheSerialTevjeter && serialiIFshire != null)
                            serialetPerTuShtuar.Add(serialiIFshire);
                        return new { Mesazh = new MesazhGabimi(MessagesResource.Messages["artikulliNukKaGjendje"].Replace("XXX", artikulli.KodArtikulli).Replace("YYY", sasia.ToString())) };
                    }
                }

                colSerialeUnikeMagazina serialeTeReja = KrijuesKategoriSerialesh.KrijoSeriale(serialeKategori, idNdermarrje, artikulli, dt, artPerb, serialiUnik.SerialKryesor, idMag, Kthim);

                clsMesazh mesazhSerialiIPareNeRradhe = null;
                if (!Kthim && !serialetPerTuShtuar.Exists(s => s.IdArtikulli == idArtikulli))
                    mesazhSerialiIPareNeRradhe = clsSerialeUnikeMagazina.KontrolloSerialIPare(serialeTeReja.GjejSerial(seriali, serialiUnik.SerialKryesor), artikulli.KodArtikulli, idArtikulli, serialiUnik.SerialKryesor, date, idNdermarrje, idDok);

                serialetPerTuShtuar.AddRange(serialeTeReja);


                return new
                {
                    Mesazh = new MesazhSuksesi(),
                    MesazhSerialiRradhe = mesazhSerialiIPareNeRradhe,
                    RreshtaPerGride = serialeTeReja.NdertoTrupinEGrides(serialeKategori, fshiSerial && seriali == serialiIVjeter ? serialiIVjeter : ""),
                    KodMag = clsNjesiAdministrative.ktheKodiNjesiAdministrativeSipasiDPaAutorizime(idMag)
                };
            }
            catch(MyException ex)
            {
                return new { Mesazh = new MesazhGabimi(ex.Message) };
            }           

        }


        private static clsMesazh KontrolloGjendje(DataTable dt, string seriali, double sasiGjendje, double sasiaBaze, int idMag, bool kthim)
        {
            if (!kthim)
            {

                if (sasiGjendje == 0)
                    return new MesazhGabimi(MessagesResource.Messages["serialiNukKaGjendje"].Replace("XXX", seriali));
                if (sasiGjendje != sasiaBaze)
                    return new MesazhGabimi(MessagesResource.Messages["ringarkuesJoGjendje"].Replace("XXX", seriali));
            }


            if (idMag != 0 && idMag != Convert.ToInt32(dt.Rows[0]["IDMAG"]))
            {
                var magGjendje = clsNjesiAdministrative.kthePershkrimNjesiAdministrativeSipasiDPaAutorizime(Convert.ToInt32(dt.Rows[0]["IDMAG"]));
                return new MesazhGabimi(MessagesResource.Messages["serialiNukKaGjendjeNeKeteMAgazine"].Replace("#SERIALI", seriali).Replace("#MAG", magGjendje));
            }

            if (kthim && sasiGjendje > 0)
            {
                var magGjendje = clsNjesiAdministrative.kthePershkrimNjesiAdministrativeSipasiDPaAutorizime(Convert.ToInt32(dt.Rows[0]["IDMAG"]));
                return new MesazhGabimi(MessagesResource.Messages["serialiKaGjendje"].Replace("XXX", seriali).Replace("YYY", magGjendje));
            }

            return new MesazhSuksesi();
        }

        internal static object merrTrupSerialeUnike(HttpSessionState session, string guidString, int idNdermarrje, int idSeti)
        {
            var serialeTeNgarkuar = mySessionObjects.MerrNgaSession<colSerialeUnikeMagazina>(session, Constants.SERIALE_UNIKE_TE_NGARKUAR, guidString);

            if (serialeTeNgarkuar == null)
            {
                mySessionObjects.hiqObjectNeSesion(session, Constants.SERIALE_UNIKE_PER_TU_SHTUAR, guidString);
                return null;
            }

            var serialePerTuShtuar = serialeTeNgarkuar?.Clone();
            serialePerTuShtuar.RemoveAll(x => x.IdSeti != idSeti);
            serialeTeNgarkuar.RemoveAll(x => x.IdSeti == idSeti);            

            mySessionObjects.RuajNeSession(session, serialePerTuShtuar, Constants.SERIALE_UNIKE_PER_TU_SHTUAR, guidString);

            var serialePerTuShtuarOld = serialePerTuShtuar?.Clone();
            mySessionObjects.RuajNeSession(session, serialePerTuShtuarOld, Constants.SERIALE_UNIKE_PER_TU_SHTUAR_OLD, guidString);

            return serialePerTuShtuar.Count == 0 ? null : serialePerTuShtuar.NdertoTrupinEGrides(idNdermarrje);
        }

        internal static object MergeSerialeTeReja(HttpSessionState session, string guidString)
        {
            var serialeTeReja = mySessionObjects.MerrNgaSession<colSerialeUnikeMagazina>(session, Constants.SERIALE_UNIKE_PER_TU_SHTUAR, guidString);
            var serialeTeVjetra = mySessionObjects.MerrNgaSession<colSerialeUnikeMagazina>(session, Constants.SERIALE_UNIKE_TE_NGARKUAR, guidString);

            colSerialeUnikeMagazina seriale = new colSerialeUnikeMagazina();
            seriale.AddRange(serialeTeReja.GroupBy(x => x.SerialiKryesore).Select(x => x.Last()));

            if (serialeTeVjetra != null)
                seriale.AddRange(serialeTeVjetra);

            mySessionObjects.RuajNeSession(session, seriale, Constants.SERIALE_UNIKE_TE_NGARKUAR, guidString);
            if (seriale == null)
                return null;

            if (serialeTeVjetra == null) serialeTeVjetra = new colSerialeUnikeMagazina();

            mySessionObjects.hiqObjectNeSesion(session, Constants.SERIALE_UNIKE_PER_TU_SHTUAR, guidString);

            var njesiAdm = new colNjesiAdministrative(seriale.Union(serialeTeVjetra).Select(x => x.IdMag).Distinct().ToList());
            //grupo artikujt dhe llogarit sasine per secilin, nuk perfshihen artikujt set pasi ato nuk do te shtohen ne gride
            var artikujt = seriale.Where(s => s.IdSeti == 0)
            .GroupBy(ac => new
            {
                ac.IdArtikulli,
                ac.IdMag
            }).Select(ac => new
            {
                ac.Key.IdArtikulli,
                ac.Key.IdMag,
                KodMag = njesiAdm.Find(x => x.IdNjesiAdministrative == ac.Key.IdMag)?.Kodi,
                Sasia = ac.Sum(acs => acs.Sasia),
            });

            var artikujtTeVjeter = serialeTeVjetra.Where(s => s.IdSeti == 0)
            .GroupBy(ac => new
            {
                ac.IdArtikulli,
                ac.IdMag
            }).Select(ac => new
            {
                ac.Key.IdArtikulli,
                ac.Key.IdMag,
                KodMag = njesiAdm.Find(x => x.IdNjesiAdministrative == ac.Key.IdMag)?.Kodi,
                Sasia = ac.Sum(acs => acs.Sasia),
            });
            //nga gjithe serialet e fshire merr ato idArtikuj qe nuk gjenden me tek artikujt e serialeve ekzistues
            //kjo behet sepse nese fshihen te gjithe artikujt e nje seriali atehere ai artikull duhet fshire nga grida
            var idArtFshire = artikujtTeVjeter.Select(a => new { IdArtikulli = a.IdArtikulli, KodMag = a.KodMag }).Except(artikujt.Select(a => new { IdArtikulli = a.IdArtikulli, KodMag = a.KodMag }));
            return new
            {
                idArtFshire = idArtFshire,
                artikujt = artikujt,
                NrSeriale = seriale.Count()

            };
        }

        public static object merrKategoriSeriali(HttpSessionState Session)
        {
            return new colSerialeUnikeKategori(mySessionObjects.merrIdNdermarrjeSesioni(Session));
        }

        /// <summary>
        /// Merr Artikujt perberes te gjithe artikujve set dhe u vendos sasite qe kane te plotesuara nga gjithe serialet qe jane ne gride
        /// </summary>
        /// <param name="ArtikujSet">Liste me objekte clsArtikujPerberesMeSasi e serializuar</param>
        /// <param name="date"> data e dokumentit</param>
        /// <param name="seriale">serialet e ngarkuar</param>
        /// <returns></returns>
        public static colArtikulliPerberes MerrArtikujSet(string ArtikujSet, DateTime date, colSerialeUnikeMagazina seriale, int idNdermarrje, bool shitje, bool merrMagazinePerberesi)
        {
            var artSet = clsArtikujMeSasi.Grupo(JsonConvert.DeserializeObject<List<clsArtikujMeSasi>>(ArtikujSet));
            if (artSet.Count == 0)
                return null;
            //merr IdArtikujt per artikuj
            colArtikulliPerberes artPer = new colArtikulliPerberes(artSet.Select(x => x.IdSeti).Distinct().ToList().Join('_', art => art.ToString()), date);

            if (shitje)
                artSet = MerrPerberesit(artSet, artPer, merrMagazinePerberesi);
            artPer.VendosSasitePerArtikujSet(artSet, idNdermarrje);

            foreach (var serial in seriale)
            {
                if (serial.IdSeti == 0)
                    continue;

                var artPerbere = artPer.Find(ap => ap.IdArtikulliKryesor == serial.IdSeti && ap.IdLidheseArt == serial.IdArtikulli && ap.getIdMag() == serial.IdMag);
                if (artPerbere == null)
                    continue;
                artPerbere.shtoSasiPlotesuar(serial.Sasia);
            }



            return artPer;
        }

        public static List<clsArtikujMeSasi> MerrPerberesit(List<clsArtikujMeSasi> artikujSet, colArtikulliPerberes artPerb, bool merrMagazinePerberesi)
        {
            var artikuj = new List<clsArtikujMeSasi>();
            foreach (var set in artikujSet)
            {
                var perberesit = artPerb.FindAll(x => x.IdArtikulliKryesor == set.IdSeti);
                foreach (var art in perberesit)
                {
                    var artMeSasi = new clsArtikujMeSasi
                    {
                        IdSeti = set.IdSeti,
                        IdArtikulli = art.IdLidheseArt,
                        Sasi = set.Sasi,
                        Mag = set.Mag
                    };

                    if (merrMagazinePerberesi)
                    {

                        var a = new clsArtikulli(artMeSasi.IdArtikulli);
                        if (a.IdMagazina > 0)
                            artMeSasi.Mag = clsNjesiAdministrative.ktheKodiNjesiAdministrativeSipasiDPaAutorizime(a.IdMagazina);
                    }

                    artikuj.Add(artMeSasi);
                }
            }

            return artikuj;
        }


        public static clsMesazh KontrolloArtikull(List<clsArtikujMeSasi> artikuj, colArtikulliPerberes artikujPerberes, clsArtikulli artikulli, int idMag, double sasia, bool KontrolloSasi, string seriali, bool MeTePerbere)
        {
            clsArtikulliPerberes artPerb = null;

            if (artikujPerberes != null)
            {
                artPerb = artikujPerberes.Find(x => x.IdLidheseArt == artikulli.IdArtikulli && idMag == x.getIdMag());
                if (artPerb == null)
                    return new MesazhGabimi(MessagesResource.Messages["artikulliISerialitNukGjendetNeGride"].Replace("#SERIALI", seriali).Replace("#ARTIKULL", artikulli.KodArtikulli));

                if (artPerb.eshtePlotesuarSasia())
                    return new MesazhGabimi("Nuk duhet te tejkaloni sasine e setit!");
            }

            if (!KontrolloSasi)
                return new MesazhSuksesi();

            var art = artikuj.Find(x => x.IdArtikulli == artikulli.IdArtikulli && clsNjesiAdministrative.ktheKodiNjesiAdministrativeSipasiDPaAutorizime(idMag) == x.Mag);
            
            if (art == null && artPerb == null)
                return new MesazhGabimi(MessagesResource.Messages["artikulliISerialitNukGjendetNeGride"].Replace("#SERIALI", seriali).Replace("#ARTIKULL", artikulli.KodArtikulli));

            float sasiENevojshme = art == null ? 0 : art.Sasi;
            if (artPerb != null)
                sasiENevojshme += artPerb.getSasiENevojshme();

            if (Math.Abs(sasiENevojshme) - Math.Abs(sasia) < 0)
                return new MesazhGabimi(MessagesResource.Messages["nukMundTeTejkaloshsAsine"].Replace("#ARTIKULLI", artikulli.KodArtikulli).Replace("#SASIA", sasiENevojshme.ToString()));

            return new MesazhSuksesi();
        }

    }
}