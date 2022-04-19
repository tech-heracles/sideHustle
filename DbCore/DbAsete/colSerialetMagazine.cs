using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using CacheLayer;
using DbCore.DbInventari;
using DbCore.DbRegjistrim;
using DbCore.IMBUtils.Extensions;
using DbCore.IMBUtils.Messages;

namespace DbCore.DbAsete
{
    /// <summary>
    /// MODULI ASETE:
    /// Mban nje list objektesh clsAmortizimiKoka (objekte per serialet qe preken nga dokumentat e magazines) ne modulin e Aseteve.
    /// Te dhenat merret nga tabela T_ASETE_LIDHJE_MAGAZINE_SERIAL.
    /// </summary>
    public class colSerialetMagazine : List<clsSerialetMagazine>
    {

        #region Konstruktore

        /// <summary>
        /// MODULI ASETE:
        /// Krijon nje list objektesh bosh te klases clsSerialetMagazine per serialet qe preken nga dokumentat e magazines.
        /// </summary>
        public colSerialetMagazine()
        {
        }

        public colSerialetMagazine(IEnumerable<clsSerialetMagazine> enumerable) : base(enumerable)
        {

        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// MODULI ASETE:
        /// Merr te gjithe dokumentat me serialet e aqt-ve sipas id se serialit qe kerkojme dhe mbush nje koleksion me keto objekte.
        /// </summary>
        /// <param name="idSerialAQT">(int) Id e serialit aqt qe po kerkojme.</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes ne perdorim per te cilen do te merret serialet e dokumentit te magazines.</param>
        /// <returns>Kthen True nese nuk ndodh asnje gabim gjate marrjes se dokumentave me seriale te aqt-ve ose False ne te kundert.</returns>
        public bool merrSerialetMagazineSipasIDSeriali(int idSerialAQT, int idNdermarrje)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            bool pergjigja = MbushSerialetMagazinaList(moduliAsete.ktheSerialetMagazineSipasIDSeriali(idSerialAQT, idNdermarrje));
            moduliAsete.Dispose();
            return pergjigja;
        }
        public static bool ekzistonBlerjePerKeteSerial(int idSerialAQT, int idNdermarrje)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            bool pergjigja = moduliAsete.ekzistonBlerjePerKeteSerial(idSerialAQT, idNdermarrje);
            moduliAsete.Dispose();
            return pergjigja;
        }
        public static bool ktheSerialetMagazineKaVeprimePas(int idSerialAQT, int idNdermarrje, int nrrendor, DateTime data)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            bool pergjigja = ktheSerialetMagazineKaVeprimePas(idSerialAQT, idNdermarrje, nrrendor, data, moduliAsete);
            moduliAsete.Dispose();
            return pergjigja;
        }
        public static bool ktheSerialetMagazineKaVeprimePas(int idSerialAQT, int idNdermarrje, int nrrendor, DateTime data, clsDatabazeAsete moduliAsete)
        {

            bool pergjigja = moduliAsete.ktheSerialetMagazineKaVeprimePas(idSerialAQT, idNdermarrje, nrrendor, data);

            return pergjigja;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr te gjithe dokumentat me serialet e aqt-ve sipas id se dokumentit dhe mbush nje koleksion me keto objekte.
        /// </summary>
        /// <param name="idDok">(int) Id e dokumentit te magazines qe ka perdorur serialin qe po kerkojme.</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes ne perdorim per te cilen do te merret serialet e dokumentit te magazines.</param>
        /// <param name="idkonfigambjente">(int) Id e konfigurimit te ambjentit.</param>
        /// <returns>Kthen True nese nuk ndodh asnje gabim gjate  marrjes se dokumentave me seriale te aqt-ve ose False ne te kundert.</returns>
        public bool merrSerialetMagazineSipasIDDokumenti(int idDok, int idNdermarrje, int idkonfigambjente)
        {
            using (var moduliAsete = new clsDatabazeAsete())
                return MbushSerialetMagazinaList(moduliAsete.ktheSerialetMagazineSipasIDDokumenti(idDok, idNdermarrje, idkonfigambjente));
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr te gjithe dokumentat me serialet e aqt-ve qe ndodhen ne nje magazine sipas nje seriali te caktuar dhe mbush nje koleksion me keto objekte.
        /// </summary>
        /// <param name="idSerialAQT">(int) Id e serialit aqt qe po kerkojme.</param>
        /// <param name="idNjesiAdministrative">(int) Id e njesi administrative qe po kerkojme serialin.</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes ne perdorim per te cilen do te merret serialet e dokumentit te magazines.</param>
        /// <returns>Kthen True nese nuk ndodh asnje gabim gjate  marrjes se dokumentave me seriale te aqt-ve ose False ne te kundert.</returns>
        public bool merrSerialetMagazineSipasIDSerialIDMag(int idSerialAQT, int idNjesiAdministrative, int idNdermarrje)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            bool pergjigja = MbushSerialetMagazinaList(moduliAsete.ktheSerialetMagazineSipasIDSerialIDMag(idSerialAQT, idNjesiAdministrative, idNdermarrje));
            moduliAsete.Dispose();
            return pergjigja;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kthen nese ekziston ndonje dokument me serialet e aqt-ve qe ndodhen ne nje magazine sipas nje seriali te caktuar dhe mbush nje koleksion me keto objekte.
        /// </summary>
        /// <param name="idSerialAQT">(int) Id e serialit aqt qe po kerkojme.</param>
        /// <param name="idNjesiAdministrative">(int) Id e njesi administrative qe po kerkojme serialin.</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes ne perdorim per te cilen do te merret serialet e dokumentit te magazines.</param>
        /// <returns>true nqs ekziston dhe false ne te kundert</returns>
        public static bool ekzistonSerialetMagazineSipasIDSerialIDMag(int idSerialAQT, int idNjesiAdministrative, int idNdermarrje)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            bool ekziston = moduliAsete.ekzistonSerialetMagazineSipasIDSerialIDMag(idSerialAQT, idNjesiAdministrative, idNdermarrje);
            moduliAsete.Dispose();
            return ekziston;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr te gjithe dokumentat me serialet e aqt-ve qe ndodhen ne nje magazine sipas nje lloji dokumenti te nje seriali te caktuar dhe mbush nje koleksion me keto objekte.
        /// </summary>
        /// <param name="idSerialAQT">(int) Id e serialit aqt qe po kerkojme.</param>
        /// <param name="idNjesiAdministrative">(int) Id e njesi administrative qe po kerkojme serialin.</param>
        /// <param name="idNivel">(int) Id e nivelit te pare te llojit te dokumentit.</param>
        /// <param name="idKonfigAmbjenti">(int) Id e nenllojit te dokumentit.</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes ne perdorim per te cilen do te merret serialet e dokumentit te magazines.</param>
        /// <returns>Kthen True nese nuk ndodh asnje gabim gjate  marrjes se dokumentave me seriale te aqt-ve ose False ne te kundert.</returns>
        public bool merrSerialetMagazineSipasIDSerialIDMagLlojDok(int idSerialAQT, int idNjesiAdministrative, int idNivel, int idKonfigAmbjenti, int idNdermarrje)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            bool pergjigja = MbushSerialetMagazinaList(moduliAsete.ktheSerialetMagazineSipasIDSerialIDMagLlojDok(idSerialAQT, idNjesiAdministrative, idNivel, idKonfigAmbjenti, idNdermarrje));
            moduliAsete.Dispose();
            return pergjigja;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Metode qe sherben per te kontrolluar sa jane plotesuar rreshtat me seriale qe te perkojne me sasine ne dokumentin e magazines.
        /// </summary>
        /// <param name="dokMagazine">(DbRegjistrim.clsKokaMagazina) Dokumenti i magazines per te cilin do kryen kontrollet e serialeve.</param>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese kontrolli kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        public clsMesazh kontrolloNevojatArtikullitPerSeriale(clsKokaMagazina dokMagazine, clsDatabazeAsete moduliAsete)
        {
            var mesazhi = new clsMesazh(true, "Gjenerimi u krye me sukses!");
            int rreshti = -1;

            foreach (var trupiMagazine in dokMagazine.OcolTrupiMagazina)
            {
                rreshti++;

                if (trupiMagazine.IdLlojVeprimi != 1) continue;

                clsArtikulli artikulli = (clsArtikulli)trupiMagazine.Element;

                if (!artikulli.LlojiArt) continue;

                //Kontrollon nese seriali eshte serial i ndashem ose jo. Nese artikulli eshte MeSerial ath cdo cope duhet te kete serialin e saj ne te kundert konsiderohet nje serial e gjithe sasia.
                float diferencaSasi;

                if (artikulli.MeSerial)
                    diferencaSasi = (float)(trupiMagazine.Sasia * trupiMagazine.Koeficenti) - FindAll(x => x.IdArtikulli == trupiMagazine.IdArtikulli && x.IdNjesiAdministrative == trupiMagazine.IdMag && x.NrRendor == rreshti).Count;
                else
                    diferencaSasi = 1 - (float)FindAll(x => x.IdArtikulli == trupiMagazine.IdArtikulli && x.IdNjesiAdministrative == trupiMagazine.IdMag && x.NrRendor == rreshti).Count;

                //Kontrollon diferencen mes serialeve dhe sasise se dokumentit.
                if (diferencaSasi < 0)
                    return new clsMesazh(false, "Ekziston njehere seriali i aqt-se!");

                if (diferencaSasi > 0)
                {
                    mesazhi = GjeneroSerialeSipasNevojes(artikulli, diferencaSasi, dokMagazine, trupiMagazine, rreshti, moduliAsete);

                    if (!mesazhi.Status)
                        return mesazhi;
                }
            }

            return mesazhi;
        }

        private static clsMesazh KtheMesazhGjendje(int idArtikulli, int idNdermarrje, int idMagazina, clsAQTSeriale serial)
        {
            var serialeTePerdoruraNeDokDraft = colAQTSeriale.KtheAqtSerialSipasIdArtikullDheMagazineTePerdoruraDraft(idArtikulli, idNdermarrje, idMagazina);
            if (serialeTePerdoruraNeDokDraft.Rows.Count > 0)
            {
                if (serialeTePerdoruraNeDokDraft.Rows.Count <= 3)
                {
                    var mesazhSerial = new StringBuilder();
                    foreach (DataRow row in serialeTePerdoruraNeDokDraft.Rows)
                        mesazhSerial.Append($"Seriali {row[1]} eshte perdorur ne dokumentin me numer {row[3]} dhe date {row[2]} te llojit {row[4]}! ");

                    return new clsMesazh(false, "Sasia e daljes eshte me e madhe se gjendja e serialit" + serial.AqtSerialKod + "! " + mesazhSerial);
                }

                GlobalCacheManager.MyPageCache.Add("serialeTePerdoruraNeDokDraft", serialeTePerdoruraNeDokDraft);
            }

            return new clsMesazh(false, "Sasia e daljes eshte me e madhe se gjendja e serialit" + serial.AqtSerialKod + "!");
        }

        private static clsMesazh KtheMesazhGjendje(int idArtikulli, int idNdermarrje, int idMagazina, string kodiArtikull, int count)
        {
            var serialeTePerdoruraNeDokDraft = colAQTSeriale.KtheAqtSerialSipasIdArtikullDheMagazineTePerdoruraDraft(idArtikulli, idNdermarrje, idMagazina);
            if (serialeTePerdoruraNeDokDraft.Rows.Count > 0)
            {
                if (serialeTePerdoruraNeDokDraft.Rows.Count <= 3)
                {
                    var mesazhSerial = new StringBuilder();
                    foreach (DataRow row in serialeTePerdoruraNeDokDraft.Rows)
                    {
                        mesazhSerial.Append(
                            $"Seriali {row[1]} eshte perdorur ne dokumentin me numer {row[3]} dhe date {DateTime.ParseExact(row[2].ToString(), "dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture).ToShortDateString()} te llojit {row[4]}! ");
                    }

                    return new clsMesazh(false, "Sasia e daljes është më e madhe se gjendja " + count + " e serialeve te artikullit: " + kodiArtikull + "! " + mesazhSerial);
                }

                GlobalCacheManager.MyPageCache.Add("serialeTePerdoruraNeDokDraft", serialeTePerdoruraNeDokDraft);
            }

            return new clsMesazh(false, "Sasia e daljes është më e madhe se gjendja " + count + " e serialeve te artikullit: " + kodiArtikull);
        }

        /// <summary>
        /// MODULI ASETE:
        /// Marrja e serialeve ne rradhe sipas FIFO per dokumetat qe perdorin seriale te gjeneruar me pare.
        /// </summary>
        /// <param name="o">(DbRegjistrim.clsTrupiMagazina) Trupi i magazines qe do i bashkengjitet seriali.</param>
        /// <param name="artikulli">(DbInventari.clsArtikulli) Artikulli ku do te bashkengjitet seriali.</param>
        /// <param name="colHistorik">(colHistorikAQTSeriale) Historiku i serialit nese eshte serial me ndarje.</param>
        /// <param name="rreshti">(int) Rreshti qe eshte duke u perpunuar.</param>
        /// <param name="idndermarje">(int) Id e ndermarrjes ku po kryhet gjenerimi</param>
        /// <param name="idNiv">(int) Id e nivelit te llojit te dokumentit</param>
        /// <param name="idKonf">(int) Lloji i dokumentit</param>
        /// <param name="idstatusdok">(int) Id se ne cfare statusi ndodhet dokumenti, i modifikuar i fshire apo aktiv.</param>
        /// <param name="idPer">(int) Id e perdoruesit qe po kryen krijmin e serialit</param>
        /// <param name="dtdok">(DateTime) Data e dokumentit.</param>
        /// <param name="totaliArtikullitperserial">(Double) Totali i artikullit per serial</param>
        /// <param name="countTrupa">(int) Numri i trupave te gjeneruar.</param>
        /// <param name="trupatEShtuar">(DbRegjistrim.colTrupiMagazina) Koleksion i trupave te shtuar rishtaz.</param>
        /// <param name="dbasete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese kontrolli kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        public clsMesazh KontrolloSerialdheMerrSerialinSipasFifo(clsTrupiMagazina o, clsArtikulli artikulli, colHistorikAQTSeriale colHistorik, int rreshti, int idndermarje, int idNiv, int idKonf, int idstatusdok, int idPer, DateTime dtdok, double totaliArtikullitperserial, int countTrupa, colTrupiMagazina trupatEShtuar, colSerialetMagazine serialetranf, int idmagtransf, colTrupiMagazina trupatEShtuarTransferim, clsDatabazeAsete dbasete, int iddoktransferimi)
        {
            o.Vlefta = 0;
            var mesazh = new clsMesazh(true, "Kontrolli perfundoi me sukses!");
            var colSerialeTeKetijArtikulliPerKeteRresht = FindAll(x => x.IdArtikulli == o.IdArtikulli && x.IdNjesiAdministrative == o.IdMag && x.NrRendor == rreshti);
            var colSerialeTeKetijArtikulliPerKeteRreshttrans = serialetranf.FindAll(x => x.IdArtikulli == o.IdArtikulli && x.IdNjesiAdministrative == idmagtransf && x.NrRendor == rreshti);///marim serialet qe ka shtuar perdoruesi per kete resht

            foreach (var serial in colSerialeTeKetijArtikulliPerKeteRresht)
            {
                serial.Cmimi = clsSerialetMagazine.ktheSerialetMagazineCmimiSipasIDSerialDokFundit(serial.IdAQTSeriali, idndermarje, dtdok, serial.NrRendDitor, dbasete);
                serial.Vlefta = serial.Sasia * serial.Cmimi;
                o.Vlefta += serial.Vlefta;
            }

            foreach (var serial in colSerialeTeKetijArtikulliPerKeteRreshttrans)
            {
                serial.Cmimi = clsSerialetMagazine.ktheSerialetMagazineCmimiSipasIDSerialDokFundit(serial.IdAQTSeriali, idndermarje, dtdok, serial.NrRendDitor, dbasete);
                serial.Vlefta = serial.Sasia * serial.Cmimi;
            }

            #region meSerial

            if (artikulli.MeSerial)
            {
                foreach (var serialMagazine in colSerialeTeKetijArtikulliPerKeteRresht)
                    if (FindAll(x => x.IdAQTSeriali == serialMagazine.IdAQTSeriali).Count > 1)
                    {
                        var serial = new clsAQTSeriale();
                        serial.merrAQTSerialSipasID(serialMagazine.IdAQTSeriali);
                        return KtheMesazhGjendje(o.IdArtikulli, idndermarje, o.IdMag, serial);
                    }

                // kontrollojme ne lidhje me sasine
                if (colSerialeTeKetijArtikulliPerKeteRresht.Count < o.Sasia)
                {
                    // marrim gjithe serialet e ketij artikulli te perdorura ne kete fature
                    var colSerialeTeKetijArtikulli = FindAll(x => x.IdArtikulli == o.IdArtikulli && x.IdNjesiAdministrative == o.IdMag);

                    var serialeTeMagazines = new colAQTSeriale();
                    serialeTeMagazines.ktheAQTSerialSipasIDArtikulliDheMagazineTeperdorura(o.IdArtikulli, idndermarje, o.IdMag, dtdok, dbasete);// marrim serialet qe ndodhen ne kete magazine

                    if (serialeTeMagazines.Count < totaliArtikullitperserial) //kontroll per gjendje
                        return KtheMesazhGjendje(o.IdArtikulli, idndermarje, o.IdMag, o.KodiArtikull, serialeTeMagazines.Count);

                    double sasi = colSerialeTeKetijArtikulliPerKeteRresht.Count;
                    foreach (var serial in serialeTeMagazines)
                    {
                        //nqs eshte arritur sasia dil
                        if (sasi == o.Sasia)
                            break;

                        //nqs ky serial eshte ne kete fature
                        if (colSerialeTeKetijArtikulli.Find(x => x.IdAQTSeriali == serial.IdAQTSerial) != null)
                            continue;

                        var serialiIRi = new clsSerialetMagazine(0, 0, rreshti, o.IdArtikulli, serial.IdAQTSerial, serial.AqtSerialKod, idNiv, idKonf, o.IdMag, 1, clsSerialetMagazine.ktheSerialetMagazineCmimiSipasIDSerialDokFundit(serial.IdAQTSerial, idndermarje, dtdok, 0, dbasete), clsSerialetMagazine.ktheSerialetMagazineCmimiSipasIDSerialDokFundit(serial.IdAQTSerial, idndermarje, dtdok, 0, dbasete), idstatusdok, idndermarje, idPer, idPer, 0);//krijojme serialin e ri 
                        var serialiIRitransf = new clsSerialetMagazine(0, 0, rreshti, o.IdArtikulli, serial.IdAQTSerial, serial.AqtSerialKod, idNiv, idKonf, idmagtransf, 1, clsSerialetMagazine.ktheSerialetMagazineCmimiSipasIDSerialDokFundit(serial.IdAQTSerial, idndermarje, dtdok, 0, dbasete), clsSerialetMagazine.ktheSerialetMagazineCmimiSipasIDSerialDokFundit(serial.IdAQTSerial, idndermarje, dtdok, 0, dbasete), idstatusdok, idndermarje, idPer, idPer, 0);//krijojme serialin e ri
                        Add(serialiIRi);
                        serialetranf.Add(serialiIRitransf);
                        o.Vlefta += serialiIRi.Vlefta;
                        sasi++;
                    }
                }
                else if (colSerialeTeKetijArtikulliPerKeteRresht.Count > o.Sasia)
                {
                    int count = colSerialeTeKetijArtikulliPerKeteRresht.Count - int.Parse(o.Sasia.ToString());

                    for (int c = 0; c < count; c++)
                    {
                        Remove(colSerialeTeKetijArtikulliPerKeteRresht[colSerialeTeKetijArtikulliPerKeteRresht.Count - 1 - c]);

                        if (serialetranf.Count >= colSerialeTeKetijArtikulliPerKeteRresht.Count - 1 - c)
                            serialetranf.Remove(colSerialeTeKetijArtikulliPerKeteRreshttrans[colSerialeTeKetijArtikulliPerKeteRresht.Count - 1 - c]);
                    }
                }

                o.Cmimi = o.Vlefta / o.Sasia;

                return mesazh;
            }

            #endregion

            #region paserial

            var merrSipasFifo = false;
            var sasimbetur = o.Sasia * o.Koeficenti; //to do njesia e dyte
            var sasifillestare = sasimbetur;
            int rreshtiRi;

            // marrim serialet qe ndodhen ne kete magazine
            var serialeTeMagazinesPaserial = new colAQTSeriale();
            serialeTeMagazinesPaserial.ktheAQTSerialSipasIDArtikulliDheMagazineTeperdorura(o.IdArtikulli, idndermarje, o.IdMag, dtdok, dbasete);

            // per serialet paserial kemi vetem nje serial per rresht
            if (colSerialeTeKetijArtikulliPerKeteRresht.Count == 1)
            {
                var serial = new clsAQTSeriale();
                serial.merrAQTSerialSipasID(colSerialeTeKetijArtikulliPerKeteRresht[0].IdAQTSeriali, dbasete);

                var colSerialePara = FindAll(x => x.IdArtikulli == serial.IdAQTArt && x.IdNjesiAdministrative == o.IdMag && (x.NrRendor < rreshti || x.NrRendor >= countTrupa));

                //Ndryshoi Ridi sipas ndryshimit qe bem me nestilen tek e njejta metode por te kushti ku merr Fifon.
                var sasiPerdorur = colSerialePara.Where(serialetMagazine => serialetMagazine.IdAQTSeriali == serial.IdAQTSerial).Sum(serialetMagazine => serialetMagazine.Sasia);

                var histroik = new clsHistorikAQTSeriale();
                histroik.merrHistorikAQTSerialSipasID(serial.IdHistorikAktualPaSerial, idndermarje, dbasete);

                var cmimi = clsSerialetMagazine.ktheSerialetMagazineCmimiSipasIDSerialDokFundit(serial.IdAQTSerial, idndermarje, dtdok, colSerialeTeKetijArtikulliPerKeteRresht[0].NrRendDitor, dbasete);
                var sasiaperserial = o.Sasia * o.Koeficenti;

                //nqs nuk ka gjendje marrim aq sa ka gjendje
                if (histroik.SasiaProgresive - sasiPerdorur < sasiaperserial)
                {
                    sasiaperserial = histroik.SasiaProgresive - sasiPerdorur;
                    merrSipasFifo = true;
                    sasimbetur -= sasiaperserial;
                }

                //nqs ka sasi per serialin e zgjedhur nga perdoruesi plotesojme sasine dhe cmimin sipas te dhenave
                if (sasiaperserial > 0)
                {
                    colSerialeTeKetijArtikulliPerKeteRresht[0].Sasia = sasiaperserial;
                    colSerialeTeKetijArtikulliPerKeteRresht[0].Cmimi = cmimi;
                    colSerialeTeKetijArtikulliPerKeteRresht[0].Vlefta = cmimi * sasiaperserial;
                    o.Vlefta = colSerialeTeKetijArtikulliPerKeteRresht[0].Vlefta;//nesnes
                    o.Sasia = sasiaperserial / o.Koeficenti;
                    rreshtiRi = countTrupa + trupatEShtuar.Count;

                    if (serialetranf.Count > 0)
                    {
                        //nqs jemi ne modifikim marrim serialet ekzistues te fht
                        if (iddoktransferimi != 0)
                        {
                            var colhis = new colHistorikAQTSeriale();
                            colhis.ktheHistorikAQTSerialSipasIDDokTeFshira(iddoktransferimi, idndermarje, dbasete);
                            var colhisseriali = colhis.FindAll(x => x.IdPrindi == histroik.IdAQTSeriale);

                            if (colhisseriali.Count > 0)
                            {
                                //marim serialet qe ka shtuar perdoruesi per kete resht
                                var colSerialeTeKetijArtikulliPerKeteRreshtTransf = serialetranf.FindAll(x => x.IdArtikulli == o.IdArtikulli && x.IdNjesiAdministrative == idmagtransf && x.NrRendor == rreshti);
                                foreach (var serialHistorik in colhisseriali)
                                {
                                    if (colHistorik.FindAll(x => x.IdAQTSeriale == serialHistorik.IdAQTSeriale).Count == 0)
                                        colHistorik.Add(serialHistorik);

                                    if (serialetranf.FindAll(x => x.IdAQTSeriali == serialHistorik.IdAQTSeriale).Count == 0)
                                    {
                                        colSerialeTeKetijArtikulliPerKeteRreshtTransf[0].IdAQTSeriali = serialHistorik.IdAQTSeriale;
                                        break;
                                    };
                                }
                            }
                            else
                            {
                                mesazh = KrijoSerialinBij(o, colHistorik, rreshti, idndermarje, idstatusdok, idPer, dtdok, sasiaperserial, serialetranf, idmagtransf, dbasete);//nesnes

                                if (!mesazh.Status)
                                    return mesazh;
                            }
                        }
                        else
                        {
                            mesazh = KrijoSerialinBij(o, colHistorik, rreshti, idndermarje, idstatusdok, idPer, dtdok, sasiaperserial, serialetranf, idmagtransf, dbasete);//nesnes
                            if (!mesazh.Status)
                                return mesazh;
                        }
                    }
                }
                else
                {
                    o.Vlefta = 0;
                    rreshtiRi = rreshti;//nqs nuk ka sasi duhet ta shtojme serialin per kete resht
                    Remove(colSerialeTeKetijArtikulliPerKeteRresht[0]);
                }
            }
            else
            {
                rreshtiRi = rreshti;
                merrSipasFifo = true;
            }

            if (merrSipasFifo)
            {
                //nuk ka fare seriale per kete artikull
                if (serialeTeMagazinesPaserial.Count == 0)
                    return KtheMesazhGjendje(o.IdArtikulli, idndermarje, o.IdMag, o.KodiArtikull, serialeTeMagazinesPaserial.Count);

                foreach (var serial in serialeTeMagazinesPaserial)
                {
                    //marrim sasine e te gjithe artikujve te perdorur me perpara qe kane kete serial ne kete magazine
                    var colSerialePara = FindAll(x => x.IdArtikulli == serial.IdAQTArt && x.IdNjesiAdministrative == o.IdMag && (x.NrRendor <= rreshti || x.NrRendor >= countTrupa));

                    var sasiPerdorur = colSerialePara.Where(serialPara => serialPara.IdAQTSeriali == serial.IdAQTSerial).Sum(serialPara => serialPara.Sasia);

                    var histroik = new clsHistorikAQTSeriale();
                    histroik.merrHistorikAQTSerialSipasID(serial.IdHistorikAktualPaSerial, idndermarje, dbasete);

                    //nuk ka me sasi per kete serial
                    if (histroik.SasiaProgresive <= sasiPerdorur)
                        continue;

                    var cmimi = clsSerialetMagazine.ktheSerialetMagazineCmimiSipasIDSerialDokFundit(serial.IdAQTSerial, idndermarje, dtdok, 0, dbasete);
                    var sasiaperserial = sasimbetur;

                    //nqs nuk ka gjendje marrim aq sa ka gjendje
                    if (histroik.SasiaProgresive - sasiPerdorur < sasimbetur)
                        sasiaperserial = histroik.SasiaProgresive - sasiPerdorur;

                    sasimbetur -= sasiaperserial;

                    if (sasiaperserial > 0)
                    {
                        var serialiIRi = new clsSerialetMagazine(0, 0, rreshtiRi, o.IdArtikulli, serial.IdAQTSerial, serial.AqtSerialKod, idNiv, idKonf, o.IdMag, sasiaperserial, cmimi, cmimi * sasiaperserial, idstatusdok, idndermarje, idPer, idPer, 0);//krijojme serialin e ri
                        Add(serialiIRi);

                        if (idmagtransf > 0)
                        {
                            var serialiIRi1 = new clsSerialetMagazine(0, 0, rreshtiRi, o.IdArtikulli, 0, idNiv, idKonf, idmagtransf, sasiaperserial, cmimi, cmimi * sasiaperserial, idstatusdok, idndermarje, idPer, idPer, 0);//krijojme serialin e ri

                            if (Count > serialetranf.Count)
                                serialetranf.Add(serialiIRi1);

                            mesazh = KrijoSerialinBij(o, colHistorik, rreshtiRi, idndermarje, idstatusdok, idPer, dtdok, sasiaperserial, serialetranf, idmagtransf, dbasete);//nesnes

                            if (!mesazh.Status)
                                return mesazh;
                        }

                        if (rreshtiRi != rreshti)// shtojme rreshtin e ri te magazines
                        {
                            var trupi = new clsTrupiMagazina(0, o.IdKokaMagazina, o.IdLlojVeprimi, o.IdArtikulli, o.KodiArtikull, o.PershkrimArtikull, o.IdNjesia, sasiaperserial, cmimi, cmimi * sasiaperserial, o.Koeficenti, o.Shenja, o.SasiProgresive, o.VleftaProgresive, o.IdMag, o.Data, o.IdStatusDok, o.IdRenditjes, o.IdDetajimi, o.SasiProgresiveDetajimi, o.VlefteProgresiveDetajimi, o.IdDetajimi2, o.IdTrupiRezervimi, o.IdTrupiKonvertimFSH, o.IdTrupiKonvertimUSH, o.IdTrupiKonvertimUD, o.IdKthimi, o.IdTrupiShitjeGjenerimi, o.Element, "", o.IdArtikullSet, o.IdBarkodi);
                            trupatEShtuar.Add(trupi);

                            if (idmagtransf > 0)
                            {
                                var trupitr = new clsTrupiMagazina(0, o.IdKokaMagazina, o.IdLlojVeprimi, o.IdArtikulli, o.KodiArtikull, o.PershkrimArtikull, o.IdNjesia, sasiaperserial, cmimi, cmimi * sasiaperserial, o.Koeficenti, -o.Shenja, o.SasiProgresive, o.VleftaProgresive, idmagtransf, o.Data, o.IdStatusDok, o.IdRenditjes, o.IdDetajimi, o.SasiProgresiveDetajimi, o.VlefteProgresiveDetajimi, o.IdDetajimi2, o.IdTrupiRezervimi, o.IdTrupiKonvertimFSH, o.IdTrupiKonvertimUSH, o.IdTrupiKonvertimUD, o.IdKthimi, o.IdTrupiShitjeGjenerimi, o.Element, "", o.IdArtikullSet, o.IdBarkodi);
                                trupatEShtuarTransferim.Add(trupitr);
                            }
                        }
                        else
                        {
                            o.Vlefta += serialiIRi.Vlefta;
                            o.Sasia = sasiaperserial / o.Koeficenti;
                        }

                        rreshtiRi = countTrupa + trupatEShtuar.Count;
                    }

                    //eshte arritur sasia keshtu qe dil nga cikli
                    if (sasimbetur == 0)
                        break;
                }

                if (sasimbetur != 0)
                    return KtheMesazhGjendje(o.IdArtikulli, idndermarje, o.IdMag, o.KodiArtikull, Convert.ToInt32(sasifillestare - sasimbetur));
            }

            o.Cmimi = o.Vlefta / o.Sasia;

            return mesazh;
            #endregion
        }

        /// <summary>
        /// kthen nje datatable me asetet qe nuk jane vendosur ende ne harte
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="idnderviti"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idLayerType"></param>
        /// <returns></returns>
        public static DataTable MerrAseteJoNeHarte(int idNdermarrje, int idnderviti, int idPerdorues, int idLayerType)
        {
            using (var dbAsete = new clsDatabazeAsete())
                return dbAsete.MerrAseteJoNeHarte(idNdermarrje, idnderviti, idPerdorues, idLayerType);
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// MODULI ASETE:
        /// Metode qe sherben per te marre te dhenat nga store procedura te lidhura me tabelen T_ASETE_LIDHJE_MAGAZINE_SERIAL ne nje list objektesh clsSerialetMagazine.
        /// </summary>
        /// <param name="dt">(DataTable) Merr si parameter nje datatable qe kthehet nga store procedura per te mbushur nje list me objekte.</param>
        /// <returns>(bool) Kthen true nese nuk ndodh asnje gabim gjate leximit te te dhenave, ne te kundert false.</returns>
        private bool MbushSerialetMagazinaList(DataTable dt)
        {
            if (dt == null)
                return true;

            foreach (DataRow rreshti in dt.Rows)
                Add(new clsSerialetMagazine(rreshti));

            return true;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Metode qe sherben per te gjeneruar seriali aq sa jane lene pa plotesuar.
        /// </summary>
        /// <param name="nrSerialevePerGjenerim">(float) Numri i serialeve qe duhet te gjenerohen.</param>
        /// <param name="dokMagazine">(DbRegjistrim.clsKokaMagazina) Dokumenti i magazines per te cilin do kryen kontrollet e serialeve.</param>
        /// <param name="trupiMagazine">(DbRegjistrim.clsTrupiMagazina) Njeri rresht i trupit te dokumentit te magazines per te cilen do te gjenerohet seriali.</param>
        /// <param name="nrreshti">(int) Rreshti qe eshte duke u perpunuar.</param>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese kontrolli kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        private clsMesazh GjeneroSerialeSipasNevojes(DbInventari.clsArtikulli artikulli, float nrSerialevePerGjenerim, clsKokaMagazina dokMagazine, clsTrupiMagazina trupiMagazine, int nrreshti, clsDatabazeAsete moduliAsete)
        {
            var serialobj = new clsAQTSeriale();
            var seralHistoik = new clsHistorikAQTSeriale();
            var mesazhi = new clsMesazh(true, "Gjenerimi u krye me sukses!");
            clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim(moduliAsete);
            colAQTSeriale colAQTSeriale = new colAQTSeriale();
            colHistorikAQTSeriale colHistorik = new colHistorikAQTSeriale();
            string vleraPasardhese = "";
            DbInventari.clsDatabaseInventari dbinv = new DbInventari.clsDatabaseInventari(moduliAsete);
            DbAdmin.clsDatabaseAdmin dbadm = new DbAdmin.clsDatabaseAdmin(moduliAsete);
            //Gjen grupin ku ben pjese artikulli
            DbInventari.clsKodifikimArtikulli grupiArtikulli = new DbInventari.clsKodifikimArtikulli(artikulli.Kodifikimi1Artikulli, dbinv);
            if (grupiArtikulli.IdKodifikimi < 1)
                throw new DbCore.MyException("Mungon grupi i pare i artikullit afatgjate " + artikulli.KodArtikulli);
            //Gjen formatin e nr automatik qe perdor grupi i artikullit
            DbAdmin.clsNrAutom nrAutom = DbAdmin.clsNrAutom.merrNumrinAutomatikSipasId(grupiArtikulli.IdFormatiSerial, dbadm);
            bool aktiv = nrAutom.eshteAktivNrAutomatik(dokMagazine.DtDok, dbadm);
            if (nrAutom.IdNrAutom == 0 || !aktiv)
                throw new DbCore.MyException("Nuk ka numer automatik aktiv per serialet!");
            DbCore.DbAdmin.clsNrAutomatikFundit nrFundit = nrAutom.ktheNrAutomatikFundit(dokMagazine.DtDok, dbadm);
            clsStatusMagazine_Asete StatusMag = new clsStatusMagazine_Asete();
            StatusMag.Emertimi = clsStatusMagazine_Asete.merrEmertimStatusMagazinesTeNdermarrjesIDStatus(clsHistorikStatusMagazine.merrIDStatusMagazineNgaHistorikMagazinaSipasIdNjesiAdministrative(trupiMagazine.IdMag), dokMagazine.IdNdermarrje);
            DateTime dtMagInaktive = DateTime.ParseExact("01/01/1900 00:00:00,000", "dd/MM/yyyy HH:mm:ss,fff", System.Globalization.CultureInfo.InvariantCulture);
            for (int i = 0; i < nrSerialevePerGjenerim; i++)
            {
                //Krijimi dhe ruajtja e serialeve
                try
                {
                    clsAQTSeriale serialAqt = new clsAQTSeriale();
                    vleraPasardhese = nrAutom.ktheVlerenParsardheseNrAutomatik(nrFundit, dokMagazine.DtDok, true, dbadm);

                    serialAqt.krijoNrSerialAQT(artikulli, colHistorik, vleraPasardhese, dokMagazine, trupiMagazine, moduliAsete, StatusMag.Emertimi, dtMagInaktive);
                    colAQTSeriale.Add(serialAqt);
                    //Shton serialin e gjeneruar te koleksioni i serialeve qe po ruhet ne dokument.
                    //Nese artikulli eshte me serial ath sasia do te jete gjithmone 1 ne te kundert do te jete sa sasia te trupi i magazines.
                    Add(artikulli.MeSerial
                        ? new clsSerialetMagazine(0, trupiMagazine.IdKokaMagazina, nrreshti, trupiMagazine.IdArtikulli,
                            serialAqt.IdAQTSerial, serialAqt.AqtSerialKod, dokMagazine.IdNivel, dokMagazine.IdKonfigAmbjente,
                            trupiMagazine.IdMag, 1, trupiMagazine.Cmimi / trupiMagazine.Koeficenti,
                            (trupiMagazine.Cmimi / trupiMagazine.Koeficenti), dokMagazine.IdStatusDok,
                            dokMagazine.IdNdermarrje, dokMagazine.IdPerdoruesi, dokMagazine.IdPerdoruesi, 0)
                        : new clsSerialetMagazine(0, trupiMagazine.IdKokaMagazina, nrreshti, trupiMagazine.IdArtikulli,
                            serialAqt.IdAQTSerial, serialAqt.AqtSerialKod, dokMagazine.IdNivel, dokMagazine.IdKonfigAmbjente,
                            trupiMagazine.IdMag, (trupiMagazine.Sasia * trupiMagazine.Koeficenti),
                            (trupiMagazine.Cmimi / trupiMagazine.Koeficenti), trupiMagazine.Vlefta,
                            dokMagazine.IdStatusDok, dokMagazine.IdNdermarrje, dokMagazine.IdPerdoruesi,
                            dokMagazine.IdPerdoruesi, 0));

                    if (nrFundit == null)
                    {
                        nrFundit = new DbAdmin.clsNrAutomatikFundit();
                        nrFundit.IdNrAutom = nrAutom.IdNrAutom;
                        nrFundit.Vlera = vleraPasardhese;
                        nrFundit.Data = dokMagazine.DtDok;
                        nrFundit.IdNdermarje = dokMagazine.IdNdermarrje;
                        nrFundit.IdPerdoruesi = dokMagazine.IdPerdoruesi;
                        nrFundit.IdStatusDok = 1;
                        nrAutom.OColNrAutoFundit.Add(nrFundit);
                        DbCore.clsMesazh mesazh = nrFundit.ruaj(dbadm);
                        if (!mesazh.Status)
                            throw new Exception(mesazh.PershkrimMesazhi);
                    }

                    nrFundit.Vlera = vleraPasardhese;
                }
                catch (MyException ex)
                {
                    return new clsMesazh(false, ex.Message);
                }
            }

            nrFundit.Vlera = vleraPasardhese;
            nrFundit.Data = dokMagazine.DtDok;
            nrFundit.IdPerdoruesi = dokMagazine.IdPerdoruesi;
            mesazhi = nrFundit.modifiko(dbadm);
            if (!mesazhi.Status)
                throw new Exception(mesazhi.PershkrimMesazhi);


            //kthen ne datatable collection me objekte
            DataTable dtSerialHistorik = seralHistoik.KtheNeDataTableColAQTSeriale(colHistorik);
            DataTable serialDt = serialobj.KtheNeDataTableColAQTSeriale(colAQTSeriale);
            DataTable DTSerialeruajtur = dbRegj.ruajSeriale(serialDt, dtSerialHistorik);
            if (DTSerialeruajtur != null)
                mesazhi = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            else
                return mesazhi = new clsMesazh(false, "Ndodhi nje gabim gjate ruajtjes se serialeve!");
            for (int i = 0; i < nrSerialevePerGjenerim; i++)
            {
                var uGjet = this.Find(x => x.KodSerialAqt == DTSerialeruajtur.Rows[i].ItemArray[1].ToString());
                if (uGjet != null)
                    uGjet.IdAQTSeriali = Convert.ToInt32(DTSerialeruajtur.Rows[i].ItemArray[0]);
            }
            return mesazhi;
        }


        /// <summary>
        /// MODULI ASETE:
        /// Krijon serialet bije te nje prindi, ne transaksion.
        /// </summary>
        /// <param name="o">(DbRegjistrim.clsTrupiMagazina) Merr trupin e magazines per te cilen do te gjeneroje serialet bije.</param>
        /// <param name="colHistorik">(colHistorikAQTSeriale) Merr historikun e serialit qe do gjeneroje bija.</param>
        /// <param name="rreshti">(int) Rreshti qe eshte duke u perpunuar.</param>
        /// <param name="idndermarje">(int) Id e ndermarrjes ku po kryhet gjenerimi</param>
        /// <param name="idstatusdok">(int) Id se ne cfare statusi ndodhet dokumenti, i modifikuar i fshire apo aktiv.</param>
        /// <param name="idPer">(int) Id e perdoruesit qe po kryen krijmin e serialit</param>
        /// <param name="dtdok">(DateTime) Data e dokumentit.</param>
        /// <param name="sasi">(float) Sasia qe do perfaqesoje seriali.</param>
        /// <param name="dbasete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese kontrolli kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        private clsMesazh KrijoSerialinBij(clsTrupiMagazina o, colHistorikAQTSeriale colHistorik, int rreshti, int idndermarje, int idstatusdok, int idPer, DateTime dtdok, double sasi, colSerialetMagazine serialetransf, int idmagtrans, clsDatabazeAsete dbasete)
        {
            var mesazh = new clsMesazh(true, "Kontrolli perfundoi me sukses!");
            var colSerialeTeKetijArtikulliPerKeteRresht = FindAll(x => x.IdArtikulli == o.IdArtikulli && x.IdNjesiAdministrative == o.IdMag && x.NrRendor == rreshti);
            var colSerialeTeKetijArtikulliPerKeteRreshtTransf = serialetransf.FindAll(x => x.IdArtikulli == o.IdArtikulli && x.IdNjesiAdministrative == idmagtrans && x.NrRendor == rreshti);

            var serialiPrind = new clsAQTSeriale();
            //eshte 0 sepse ne rastet e serialeve te ndashme kemi vetem 1 serial  
            serialiPrind.merrAQTSerialSipasID(colSerialeTeKetijArtikulliPerKeteRresht[0].IdAQTSeriali, dbasete);
            var histroik = new clsHistorikAQTSeriale();
            histroik.merrHistorikAQTSerialSipasID(serialiPrind.IdHistorikAktualPaSerial, idndermarje, dbasete);

            var serialAqt = new clsAQTSeriale();
            var meSerial = serialAqt.krijoNrSerialAQT(o.Element, dtdok, idmagtrans, idstatusdok, idndermarje, idPer, dbasete);
            serialAqt.AqtSerialDataAmortizimfillestar = serialiPrind.AqtSerialDataAmortizimfillestar;
            serialAqt.AqtSerialDataHyrje = serialiPrind.AqtSerialDataHyrje;
            serialAqt.AqtSerialDataMagAktive = serialiPrind.AqtSerialDataMagAktive;

            //Krijon historikun e serialit nese artikulli eshte me serial te ndashem.
            if (!meSerial)
                serialAqt.HistorikSeriali = new clsHistorikAQTSeriale(serialAqt.IdAQTSerial, String.Empty, serialiPrind.IdAQTSerial, histroik.IdPrindiFillestar == 0 ? serialiPrind.IdAQTSerial : histroik.IdPrindiFillestar, o.IdKokaMagazina, sasi, colSerialeTeKetijArtikulliPerKeteRresht[0].Cmimi, colSerialeTeKetijArtikulliPerKeteRresht[0].Cmimi * sasi, idstatusdok, idndermarje, idPer, idPer, DateTime.MinValue, DateTime.MinValue);
            mesazh = serialAqt.ruaj(dbasete);

            if (!mesazh.Status)
                return mesazh;

            colHistorik.Add(serialAqt.HistorikSeriali);
            colSerialeTeKetijArtikulliPerKeteRreshtTransf[0].IdAQTSeriali = serialAqt.IdAQTSerial;

            return mesazh;
        }

        #endregion
    }
}
