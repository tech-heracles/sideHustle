using DbCore.DbAdmin;
using DbCore.DbBuxheti;
using DbCore.DbInventari;
using DbCore.DbKontabiliteti;
using DbCore.DbRegjistrim;
using DbCore.DbShare;
using DbCore.IMBUtils;
using DbCore.IMBUtils.Extensions;
using DbCore.IMBUtils.Logging;
using DbCore.IMBUtils.Messages;
using DbCore.IMBUtils.Validation;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaWeb.Core.Interfaces.Localization;
using AlphaWebCommon.TreeStructure;
namespace DbCore.DbBuxheti
{
    public class ClsBValidimBuxheti
    {
        public ClsBValidimBuxheti()
        {
        }

        #region Public

        public clsMesazh Valido(ClsBKokaBuxheti objBuxheti)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, objBuxheti);
            clsMesazh msg = ValidoSintakseNrDok(MessagesResource.Messages, objBuxheti.NrDok);
            if (!msg)
                return msg;

            switch (objBuxheti.IdKatDok)
            {
                case 170:
                case 171:
                    msg = validoDokumentMiratimPlanfikimi(objBuxheti);
                    break;
                case 172:
                    msg = validoDokumentAlokimi(objBuxheti);
                    break;
                case 175:
                    msg = validoDokumentRialokimi(objBuxheti);
                    break;
                case 177:
                case 179:
                case 181:
                    msg = validoDokumentBuxheti(objBuxheti);
                    break;
            }
            if(!msg)
                return msg;

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, objBuxheti);
            return new MesazhSuksesi("Kontrollet u kaluan me sukses.");
        }

        public IEnumerable<TreeStructure<Dictionary<string, object>>> krijoStruktureTrupi(List<Dictionary<string,object>> trup,ColBKategoriBuxhetimi kategoriBuxhetimi)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, trup, kategoriBuxhetimi);
            var childNodes = new List<Dictionary<string, object>>();
            var node = new Dictionary<string, object>();
            var kategoria = new ClsBKategoriBuxhetimi();
            foreach (var rreshti in trup)
            {
                node = new Dictionary<string, object>();
                kategoria = kategoriBuxhetimi.FirstOrDefault(kat => kat.IdKategoriBuxhetimi == Convert.ToInt32(rreshti["IdKategoriBuxhetimi"]));
                if (kategoria == null || !(kategoria.IdKategoriBuxhetimi > 0))
                    continue;
                node.Add("idKategoriBuxhetimi", kategoria.IdKategoriBuxhetimi);
                node.Add("kodi", kategoria.Kodi);
                node.Add("idKategoriBuxhetimiPrind", kategoria.IdPrindi);
                node.Add("idLlogaria", kategoria.IdLlogaria);
                node.Add("niveli", kategoria.NivelKategorie);
                node.Add("objekti", rreshti);
                childNodes.Add(node);
            }
            var generatedTree = childNodes.GenerateTree(c => Convert.ToInt32(c["idKategoriBuxhetimi"]), c => Convert.ToInt32(c["idKategoriBuxhetimiPrind"]));

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, trup, kategoriBuxhetimi);
            return generatedTree;
        }

        public clsMesazh kontrolloTotaletDokumentBuxheti(ClsBKokaBuxheti objBuxheti, decimal totaliPaTvshTrup, decimal totaliMeTvshTrup)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, objBuxheti, totaliPaTvshTrup, totaliMeTvshTrup);

            if (totaliMeTvshTrup != objBuxheti.Totali)
                return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, "Totali i vleres me tvsh ne trup te dokumentin nuk eshte i barabarte me totalin e vleres me tvsh ne fund te dokumentit.");
            if (totaliPaTvshTrup != objBuxheti.TotaliPaTvsh)
                return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, "Totali i vleres pa tvsh ne trup te dokumentin nuk eshte i barabarte me totalin e vleres pa tvsh ne fund te dokumentit.");
            if ((objBuxheti.TotaliPaTvsh + (totaliMeTvshTrup - totaliPaTvshTrup)) != objBuxheti.Totali)
                return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, "Totali i vleres me tvsh ne trup te dokumentin nuk eshte i barabarte me shumen e totalit te vleres me tvsh me vleren e tvsh ne fund te dokumentit.");
            //SHENIM ky kontrolli i fundit duket sikur nuk ka mundesi te ndodhe
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, objBuxheti, totaliPaTvshTrup, totaliMeTvshTrup);
            return new MesazhSuksesi("Kontrollet u kaluan me sukses.");
        }

        public static clsMesazh RefuzoVepriminSipasStatusit(int statusi, int idStatusDok)
        {
            var folja = statusi == 2 ? "fshihet" : "modifikohet";
            switch (idStatusDok)
            {
                case 1:
                    return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, $"Dokumenti eshte ruajtuar ndaj nuk mund te {folja}.");
                case 8:
                    return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, $"Dokumenti eshte refuzuar ndaj nuk mund te {folja}.");
                case 9:
                    return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, $"Dokumenti eshte postuar ndaj nuk mund te {folja}.");
                default:
                    return new MesazhSuksesi();
            }
        }

        public static clsMesazh kontrolloTotaletMiratimPlanifikimKtheMesazh(decimal totaliFaktikTrupi, decimal totaliPlanifikuarTrupi, decimal totali, decimal totaliPlanifikuar, decimal maxSingleErr)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, totaliFaktikTrupi, totaliPlanifikuarTrupi, totali, totaliPlanifikuar, maxSingleErr);

            if (Math.Abs(totaliPlanifikuarTrupi - totaliPlanifikuar) > maxSingleErr)
                return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, "Buxheti i planifikuar ne koken e dokumentit nuk eshte i barabarte me totalin e planifikuar te rreshtave te trupit.");

            if (Math.Abs(totaliFaktikTrupi - totali) > maxSingleErr)
                return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, "Buxheti faktik ne koken e dokumentit nuk eshte i barabarte me totalin faktik te rreshtave te trupit.");

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, totaliFaktikTrupi, totaliPlanifikuarTrupi, totali, totaliPlanifikuar, maxSingleErr);
            return new MesazhSuksesi("Kontrollet u kaluan me sukses.");
        }

        public static clsMesazh KtheMesazhSipasKushtit(string kvk, string kvt, string enja, decimal totali, bool kaMeVlereProzitive, bool kaMeVlereNegative, bool kaMeVlereZero, int nrRreshtaTrupiMeTeNjejtenKategori, int nrRreshtaTrupiDistinctKategori, int rreshtaNr)
        {
            switch (kvk.ToLower())
            {
                case "negative":
                    if (!(totali < 0))
                        return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, "Totali i vlerave te trupit te dokumentit duhet te jete negativ");
                    break;
                case "pozitive":
                    if (!(totali > 0))
                        return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, "Totali i vlerave te trupit te dokumentit duhet te jete pozitiv");
                    break;
                case "0":
                    if (!(totali == 0))
                        return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, "Totali i vlerave te trupit te dokumentit duhet te jete zero");
                    break;
            }
            switch (kvt.ToLower())
            {
                case "negative":
                    if (!kaMeVlereProzitive)
                        return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, "Trupi i dokumentit duhet te kete vetem rreshta me vlera negative");
                    break;
                case "pozitive":
                    if (!kaMeVlereNegative)
                        return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, "Trupi i dokumentit duhet te kete vetem rreshta me vlera pozitive");
                    break;
                default:
                    if (!kaMeVlereZero)
                        return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, "Trupi i dokumentit nuk mund te kete rreshta me vlere zero");
                    break;
            }
            switch (enja.ToLower())
            {
                case "po":
                    if (nrRreshtaTrupiMeTeNjejtenKategori != rreshtaNr)
                        return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, "Trupi i dokumentit duhet te kete te njejten analize ne te gjithe rreshtat.");
                    break;
                case "jo":
                    if (nrRreshtaTrupiDistinctKategori != rreshtaNr)
                        return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, "Trupi i dokumentit nuk duhet te kete te njejten analize ne disa rreshta.");
                    break;
            }
            return new MesazhSuksesi();
        }

        public static clsMesazh ValidoPostim(int idKoka, int idStatusDok, int idKatDok)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idKoka, idStatusDok, idKatDok);

            if (!(idKoka > 0))
                return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, "Nuk ka asnje dokument per postim.");

            if (idStatusDok == 8)
                return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, "Ky dokument eshte refuzuar dhe nuk mund te postohet.");

            if (idStatusDok == 9)
                return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, "Ky dokument eshte postuar njehere.");

            if (idStatusDok != 0 && (idKatDok == 172 || idKatDok == 175 || idKatDok == 179))
                return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, "Nuk mund te postohen dokumenta me status jo draft.");

            if (idStatusDok == 0 && idKatDok == 170)
                return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, "Nuk mund te postohen dokumenta me status draft.");

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idKoka, idStatusDok, idKatDok);
            return new MesazhSuksesi();
        }

        public clsMesazh ValidoSintakseNrDok(IMessagesResource message, string nrDok)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, message, nrDok);
            if (string.IsNullOrEmpty(nrDok))
                return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, "Nr dokumentit nuk duhet te jete bosh.");

            clsMesazh msg = clsFunksione.kontrolloKaraktereMeMesazh(nrDok, FusheKontrolli.Kodi, message, false);
            if (!msg)
            {
                ImbLogger.LogWarningBuxhetimi(msg.PershkrimMesazhi);
                return msg;
            }

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, message, nrDok);
            return new MesazhSuksesi("Kontrollet u kaluan me sukses.");
        }

        public clsMesazh kontrolloVleratMuajve(ClsBMuajt muajtPrindi, ClsBMuajt muajtBija, string kodi)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, muajtPrindi, muajtBija, kodi);

            decimal marzhGabimi = decimal.Parse((clsFunksione.krijoNumer(7, "0") + "1"), System.Globalization.CultureInfo.InvariantCulture);
            if (Math.Abs(muajtPrindi.Janar - muajtBija.Janar) > marzhGabimi)
                return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, $"Per muajin janar vlera e alokuar per kategorine {kodi} nuk eshte e barabarte me totalin e vlerave per kategorite bija");
            if (Math.Abs(muajtPrindi.Shkurt - muajtBija.Shkurt) > marzhGabimi)
                return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, $"Per muajin shkurt vlera e alokuar per kategorine {kodi} nuk eshte e barabarte me totalin e vlerave per kategorite bija");
            if (Math.Abs(muajtPrindi.Mars - muajtBija.Mars) > marzhGabimi)
                return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, $"Per muajin mars vlera e alokuar per kategorine {kodi} nuk eshte e barabarte me totalin e vlerave per kategorite bija");
            if (Math.Abs(muajtPrindi.Prill - muajtBija.Prill) > marzhGabimi)
                return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, $"Per muajin prill vlera e alokuar per kategorine {kodi} nuk eshte e barabarte me totalin e vlerave per kategorite bija");
            if (Math.Abs(muajtPrindi.Maj - muajtBija.Maj) > marzhGabimi)
                return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, $"Per muajin maj vlera e alokuar per kategorine {kodi} nuk eshte e barabarte me totalin e vlerave per kategorite bija");
            if (Math.Abs(muajtPrindi.Qershor - muajtBija.Qershor) > marzhGabimi)
                return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, $"Per muajin qershor vlera e alokuar per kategorine {kodi} nuk eshte e barabarte me totalin e vlerave per kategorite bija");
            if (Math.Abs(muajtPrindi.Korrik - muajtBija.Korrik) > marzhGabimi)
                return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, $"Per muajin korrik vlera e alokuar per kategorine {kodi} nuk eshte e barabarte me totalin e vlerave per kategorite bija");
            if (Math.Abs(muajtPrindi.Gusht - muajtBija.Gusht) > marzhGabimi)
                return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, $"Per muajin gusht vlera e alokuar per kategorine {kodi} nuk eshte e barabarte me totalin e vlerave per kategorite bija");
            if (Math.Abs(muajtPrindi.Shtator - muajtBija.Shtator) > marzhGabimi)
                return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, $"Per muajin shtator vlera e alokuar per kategorine {kodi} nuk eshte e barabarte me totalin e vlerave per kategorite bija");
            if (Math.Abs(muajtPrindi.Tetor - muajtBija.Tetor) > marzhGabimi)
                return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, $"Per muajin tetor vlera e alokuar per kategorine {kodi} nuk eshte e barabarte me totalin e vlerave per kategorite bija");
            if (Math.Abs(muajtPrindi.Nentor - muajtBija.Nentor) > marzhGabimi)
                return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, $"Per muajin nentor vlera e alokuar per kategorine {kodi} nuk eshte e barabarte me totalin e vlerave per kategorite bija");
            if (Math.Abs(muajtPrindi.Dhjetor - muajtBija.Dhjetor) > marzhGabimi)
                return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, $"Per muajin dhjetor vlera e alokuar per kategorine {kodi} nuk eshte e barabarte me totalin e vlerave per kategorite bija");

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, muajtPrindi, muajtBija, kodi);
            return new MesazhSuksesi();
        }

        #endregion

        #region Internal

        internal static clsMesazh validoVeprimPerStatus(ClsBKokaBuxheti koka, int statusi = 1)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, koka, statusi);
            switch (koka.IdKatDok)
            {
                case 170:
                case 172:
                case 175:
                case 179:
                    return RefuzoVepriminSipasStatusit(statusi, koka.IdStatusDok);
                case 177:
                    if (statusi != 2)
                        break;
                    return KontrolloTrupDheKategoriBuxhetimi(koka, koka.IdBuxhetiKoka);
            }

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, koka, statusi);
            return new MesazhSuksesi();
        }

        internal clsMesazh ValidoPostim(ClsBKokaBuxheti clsBKokaBuxheti)
        {
            return ValidoPostim(clsBKokaBuxheti.IdBuxhetiKoka, clsBKokaBuxheti.IdStatusDok, clsBKokaBuxheti.IdKatDok);
        }
        #endregion

        #region Private

        private clsMesazh validoDokumentAlokimi(ClsBKokaBuxheti objBuxheti)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, objBuxheti);

            var dtDok = (DateTime)objBuxheti.DtDok;
            if (objBuxheti.Viti != dtDok.Year)
                return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, "Viti i dokumentit nuk eshte i njejte me vitin e periudhes kontabel te zgjedhur.");

            clsMesazh mesazh = kontrolloNumerAutomatik(objBuxheti, "txtNrDok", 3069);

            if (EkzistonDokBuxhetiPerVit(objBuxheti.Viti, objBuxheti.IdKatDok, objBuxheti.IdNderm, objBuxheti.IdBuxhetiKoka, objBuxheti.KodNdermPostuesi))
                return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, $"Për vitin {objBuxheti.Viti}, ekziston dokument Alokim Buxheti.");

            if (EkzistonDokBuxhetimiTjeterMeNrDokDate(objBuxheti.NrDok, objBuxheti.DtDok, objBuxheti.IdKatDok, objBuxheti.IdNderm, objBuxheti.IdBuxhetiKoka, objBuxheti.KodNdermPostuesi, objBuxheti.IdNivel, objBuxheti.IdKonfigAmbjente))
                return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, $"Ekziston nje dokument Alokim Buxheti me kete numer per daten {objBuxheti.DtDok}.");

            if (objBuxheti.IdBuxhetiKoka > 0)
            {
                var kokaEkzistuese = new ClsBKokaBuxheti();
                kokaEkzistuese.MbushKokePaTrup(objBuxheti.IdBuxhetiKoka);
                if (kokaEkzistuese.IdStatusDok == 1)
                    return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, "Dokumenti eshte ruajtuar ndaj nuk mund te modifikohet.");

                if (kokaEkzistuese.IdStatusDok == 9 && kokaEkzistuese.IdDokPostuesi != 0)
                    return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, "Dokumenti eshte postuar ndaj nuk mund te modifikohet.");
            }

            clsMesazh msg = kontrolloTotaletAlokimi(objBuxheti.IdKonfigAmbjente, objBuxheti.IdNderm, objBuxheti.OColBTrupiBuxheti);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, objBuxheti);
            return msg;
        }

        private clsMesazh validoDokumentMiratimPlanfikimi(ClsBKokaBuxheti objBuxheti)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, objBuxheti);
            clsMesazh msg = new MesazhSuksesi();
            DateTime dtDok = (DateTime)objBuxheti.DtDok;
            if (objBuxheti.IdKatDok == 170 && objBuxheti.Viti != dtDok.Year)
                return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, "Viti i dokumentit nuk eshte i njejte me vitin e periudhes kontabel te zgjedhur.");

            string miratimPlanifikim = objBuxheti.IdKatDok == 170 ? "Miratim" : "Planifikim";

            if (EkzistonDokBuxhetiPerVit(objBuxheti.Viti, objBuxheti.IdKatDok, objBuxheti.IdNderm, objBuxheti.IdBuxhetiKoka, objBuxheti.KodNdermPostuesi) && !(objBuxheti.IdBuxhetiKoka > 0))
                return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, $"Për vitin {objBuxheti.Viti}, ekziston dokument Planifikim ose Miratim Buxheti.");

            if (EkzistonDokBuxhetimiTjeterMeNrDokDate(objBuxheti.NrDok, objBuxheti.DtDok, objBuxheti.IdKatDok, objBuxheti.IdNderm, objBuxheti.IdBuxhetiKoka, objBuxheti.KodNdermPostuesi, objBuxheti.IdNivel, objBuxheti.IdKonfigAmbjente) && !(objBuxheti.IdBuxhetiKoka > 0))
                return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, $"Ekziston nje dokument {miratimPlanifikim} Buxheti me kete numer per daten {dtDok.ToShortDateString()}.");

            if (objBuxheti.IdBuxhetiKoka > 0)
            {
                var kokaEkzistuese = new ClsBKokaBuxheti();
                kokaEkzistuese.MbushKokePaTrup(objBuxheti.IdBuxhetiKoka);

                msg = validoVeprimPerStatus(kokaEkzistuese);
                if (!msg)
                    return msg;

                msg = ClsBKokaBuxheti.EshteKonvertuarDokMPBuxhetiMeId(objBuxheti.IdBuxhetiKoka);
                if (!msg)
                    return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, "Dokumenti eshte konvertuar ndaj nuk mund te modifikohet.");
            }

            msg = kontrolloTotaletMiratimPlanifikim(objBuxheti.OColBTrupiBuxheti, objBuxheti.IdKonfigAmbjente, objBuxheti.IdNderm, objBuxheti.Totali, objBuxheti.TotaliPlanifikuar);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, objBuxheti);
            return msg;
        }

        private clsMesazh validoDokumentRialokimi(ClsBKokaBuxheti objBuxheti)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, objBuxheti);
            var dtDok = (DateTime)objBuxheti.DtDok;
            var mesazh = kontrolloNumerAutomatik(objBuxheti, "txtNrDok", 3072);

            if (objBuxheti.OColBTrupiBuxheti.Count() < 1)
                return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, "Trupi i dokumentit nuk duhet te jete bosh.");
            if (EkzistonDokBuxhetimiTjeterMeNrDokDate(objBuxheti.NrDok, objBuxheti.DtDok, objBuxheti.IdKatDok, objBuxheti.IdNderm, objBuxheti.IdBuxhetiKoka, objBuxheti.KodNdermPostuesi, objBuxheti.IdNivel, objBuxheti.IdKonfigAmbjente))
                return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, $"Ekziston nje dokument Rialokim Buxheti me kete numer per daten {dtDok.ToShortDateString()}.");
            if (objBuxheti.IdBuxhetiKoka > 0)
            {
                var kokaEkzistuese = new ClsBKokaBuxheti();
                kokaEkzistuese.MbushKokePaTrup(objBuxheti.IdBuxhetiKoka);
                var msg = validoVeprimPerStatus(kokaEkzistuese);
                if (!msg)
                    return msg;
            }
            mesazh = kontrolloVleraPerDokumentRialokimi(objBuxheti, dtDok);
           
            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, objBuxheti);
            return mesazh;
        }

        private clsMesazh validoDokumentBuxheti(ClsBKokaBuxheti objBuxheti)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, objBuxheti);

            var dtDok = (DateTime)objBuxheti.DtDok;
            var kategori = new clsKategoriNivelDok(objBuxheti.IdKatDok);
            var mesazh = kontrolloNumerAutomatik(objBuxheti, "txtNrDok", kategori.IdKomponente);
            var lloji =  objBuxheti.IdKatDok == 177 ? "Perfitim" : objBuxheti.IdKatDok == 179 ? "Planifikim Ekzekutim" : "Ekzekutim";

            if (objBuxheti.OColBTrupiBuxheti.Count() < 1)
                return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, "Trupi i dokumentit nuk duhet te jete bosh.");

            if (EkzistonDokBuxhetimiTjeterMeNrDokDate(objBuxheti.NrDok, objBuxheti.DtDok, objBuxheti.IdKatDok, objBuxheti.IdNderm, objBuxheti.IdBuxhetiKoka, objBuxheti.KodNdermPostuesi, objBuxheti.IdNivel, objBuxheti.IdKonfigAmbjente))
                return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, $"Ekziston nje dokument {lloji} Buxheti me kete numer per daten {objBuxheti.DtDok}.");

            if (objBuxheti.IdBuxhetiKoka > 0 && objBuxheti.IdKatDok == 179)
            {
                var kokaEkzistuese = new ClsBKokaBuxheti();
                kokaEkzistuese.MbushKokePaTrup(objBuxheti.IdBuxhetiKoka);
                var msg = validoVeprimPerStatus(kokaEkzistuese);
                if (!msg)
                    return msg;
            }
            var kushtGjenerimDokumenti = clsAlternativaKushti.getAlternativa(objBuxheti.IdKonfigAmbjente, "GJDV");
            string kushtiTKLL_B = clsAlternativaKushti.getAlternativa(objBuxheti.IdKonfigAmbjente, "TKLL_B");
            string[] llogariteNgaZLL_B = clsAlternativaKushti.merrLlogariDheBijaPerKushtin(objBuxheti.IdKonfigAmbjente, "ZLL_B", objBuxheti.IdNderm);
            if (kushtGjenerimDokumenti.ToLower() == "po")
            {
                if (objBuxheti.IdEntiteti <= 0)
                    return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, "Entiteti nuk mund te jete bosh.");

                var konfigDokGjenerues = 0;
                if (objBuxheti.LlojVeprimiGjenerimi == (int)EnumBLlojeVeprimi.Shitje)
                    konfigDokGjenerues = clsKusht.kthevlereSipasKushtitDheIdKonfig(objBuxheti.IdKonfigAmbjente, "ZKDSH");
                else if (objBuxheti.LlojVeprimiGjenerimi == (int)EnumBLlojeVeprimi.Arketim)
                    konfigDokGjenerues = clsKusht.kthevlereSipasKushtitDheIdKonfig(objBuxheti.IdKonfigAmbjente, "ZKDA");
                else if (objBuxheti.LlojVeprimiGjenerimi == (int)EnumBLlojeVeprimi.Blerje)
                    konfigDokGjenerues = clsKusht.kthevlereSipasKushtitDheIdKonfig(objBuxheti.IdKonfigAmbjente, "ZKDB");
                else if (objBuxheti.LlojVeprimiGjenerimi == (int)EnumBLlojeVeprimi.Pagese)
                    konfigDokGjenerues = clsKusht.kthevlereSipasKushtitDheIdKonfig(objBuxheti.IdKonfigAmbjente, "ZKDPAG");

                if (konfigDokGjenerues <= 0)
                    return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, "Ky dokument nuk mund te ruhet pasi nuk keni konfigurim te percaktuar per kete lloj veprimi.");
            }

            mesazh = kontrolloTrupDokumentBuxheti(objBuxheti, dtDok, kushtiTKLL_B, llogariteNgaZLL_B);
            
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, objBuxheti);
            return mesazh;
        }

        private clsMesazh kontrolloNumerAutomatik(ClsBKokaBuxheti objBuxheti,string kodKontrolli, int idKomponente)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, objBuxheti, kodKontrolli, idKomponente);
            clsMesazh msg = new MesazhSuksesi();
            bool modifikim = (objBuxheti.IdBuxhetiKoka > 0);

            switch (modifikim)
            {
                case true:
                    break;
                default:
                    int idnrautonrdok = clsAtributeTrupi.merrNrAutomatikSipasKontrollitDheKonfigurimit(objBuxheti.IdKonfigAmbjente, kodKontrolli, idKomponente);
                    string nrDokAuto = clsNrAutom.merrVlerenNrAutomatik(idnrautonrdok, (DateTime)objBuxheti.DtDok);

                    if (nrDokAuto != objBuxheti.NrDok)
                        break;

                    IDictionary<string, object> hidden = new Dictionary<string, object>();
                    if (!String.IsNullOrEmpty(nrDokAuto)) //nqs ka nr automatik
                    {
                        NrAuto nrdokshi = new NrAuto();
                        nrdokshi.kodKontrolli = kodKontrolli;
                        nrdokshi.idNrAuto = idnrautonrdok;
                        nrdokshi.vlereNrAuto = nrDokAuto;
                        hidden.Add(kodKontrolli, JsonConvert.SerializeObject(nrdokshi));
                        bool kaNdryshimNrAuto;
                        msg = kontrolloNrAutoArt(out kaNdryshimNrAuto, objBuxheti, hidden);
                        if (!msg.Status)
                            ImbLogger.LogWarningBuxhetimi(msg.PershkrimMesazhi);
                    }
                    break;
            }

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, objBuxheti, kodKontrolli, idKomponente);
            return msg;
        }

        private static clsMesazh KontrolloTrupDheKategoriBuxhetimi(ClsBKokaBuxheti koka, int idBuxhetiKoka)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, koka, idBuxhetiKoka);

            ClsBValidimBuxheti validim = new ClsBValidimBuxheti();
            koka.OColBTrupiBuxheti = new ColBTrupiBuxheti(idBuxhetiKoka);
            foreach (var rresht in koka.OColBTrupiBuxheti)
            {
                decimal totalObjekti = 0;
                decimal rreshtGjendje = 0;
                string dtDokGjendje = Convert.ToDateTime(koka.DtDok).ToShortDateString();
                koka.OColBTrupiBuxheti.FindAll(x => x.IdKategoriBuxhetimi == rresht.IdKategoriBuxhetimi && x.IdBuxheti == rresht.IdBuxheti).ForEach(x => totalObjekti += x.Vlera);
                if (totalObjekti < 0)
                    continue;
                var gjendjePerDate = validim.ktheGjendjePerKategoriBuxhetimi(koka.IdNderm, koka.DtDok, ((DateTime)koka.DtDok).Month, rresht.IdKategoriBuxhetimi, koka.IdBuxhetiKoka, rresht.IdBuxheti, 0, koka.IdKatDok);
                if (gjendjePerDate != null)
                {
                    rreshtGjendje = Convert.ToDecimal(gjendjePerDate["GJENDJE"]);
                    dtDokGjendje = gjendjePerDate["DTDOK"].ToString();
                }
                if (koka.IdStatusDok == 1 && rreshtGjendje < 0)
                    return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, $"Nuk mund te fshihet dokumenti pasi gjeneron gjendje negative ne daten {dtDokGjendje}");
            }

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, koka, idBuxhetiKoka);
            return new MesazhSuksesi("Kontrollet u kaluan me sukses.");
        }

        private bool EkzistonDokBuxhetiPerVit(int viti, int idKatDok, int idNderm, int idKoka, string kodNdermPostuesi)
        {
            using (var db = new ClsDatabaseBuxheti())
                return db.EkzistonDokTjeterPerVit(viti, idKatDok, idNderm, idKoka, kodNdermPostuesi);
        }

        private bool EkzistonDokBuxhetimiTjeterMeNrDokDate(string nrDok, DateTime? dtDok, int idKatDok, int idNderm, int idBuxhetiKoka, string kodiNdermPostuesi, int idNivel, int idKonfigAmbjenti)
        {
            using (var db = new ClsDatabaseBuxheti())
                return db.EkzistonDokBuxhetimiTjeterMeNrDokDate(nrDok, dtDok, idKatDok, idNderm, idBuxhetiKoka, kodiNdermPostuesi, idNivel, idKonfigAmbjenti);
        }

        private clsMesazh kontrolloTotaletMiratimPlanifikim(ColBTrupiBuxheti oColBTrupiBuxheti, int idKonfigAmbjente, int idNderm, decimal totali, decimal totaliPlanifikuar)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, oColBTrupiBuxheti, idKonfigAmbjente, idNderm, totali, totaliPlanifikuar);

            decimal maxSingleErr = clsFunksione.ktheMarzhGabimiSipasKonfigurimit(idKonfigAmbjente, idNderm);

            decimal totaliFaktikTrupi = 0;
            decimal totaliPlanifikuarTrupi = 0;

            oColBTrupiBuxheti.ForEach(x => {
                totaliFaktikTrupi += x.Vlera;
                totaliPlanifikuarTrupi += x.VleraPlanifikuar;
            });

            clsMesazh mesazh =  kontrolloTotaletMiratimPlanifikimKtheMesazh(totaliFaktikTrupi, totaliPlanifikuarTrupi, totali, totaliPlanifikuar, maxSingleErr);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, oColBTrupiBuxheti, idKonfigAmbjente, idNderm, totali, totaliPlanifikuar);
            return mesazh;
        }

        private clsMesazh kontrolloVleraPerDokumentRialokimi(ClsBKokaBuxheti objBuxheti, DateTime dtDok)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, objBuxheti, dtDok);
            var kvk = clsAlternativaKushti.getAlternativa(objBuxheti.IdKonfigAmbjente, "KVK_B");
            var kvt = clsAlternativaKushti.getAlternativa(objBuxheti.IdKonfigAmbjente, "KVT_B");
            var enja = clsAlternativaKushti.getAlternativa(objBuxheti.IdKonfigAmbjente, "ENJA");
            var kgjrn = clsAlternativaKushti.getAlternativa(objBuxheti.IdKonfigAmbjente, "KGJRN_B");
            bool kaMeVlereProzitive = objBuxheti.OColBTrupiBuxheti.Find(x => x.Vlera > 0) == null;
            bool kaMeVlereNegative = objBuxheti.OColBTrupiBuxheti.Find(x => x.Vlera < 0) == null;
            bool kaMeVlereZero = objBuxheti.OColBTrupiBuxheti.Find(x => x.Vlera == 0) == null;
            int rreshtaNr = objBuxheti.OColBTrupiBuxheti.Count;
            int nrRreshtaTrupiMeTeNjejtenKategori = objBuxheti.OColBTrupiBuxheti.FindAll(x => x.IdKategoriBuxhetimi == objBuxheti.OColBTrupiBuxheti.First().IdKategoriBuxhetimi).Count;
            int nrRreshtaTrupiDistinctKategori = objBuxheti.OColBTrupiBuxheti.Select(g => g.IdKategoriBuxhetimi).Distinct().Count();

            clsMesazh mesazh = KtheMesazhSipasKushtit(kvk, kvt, enja, objBuxheti.Totali, kaMeVlereProzitive, kaMeVlereNegative, kaMeVlereZero, nrRreshtaTrupiMeTeNjejtenKategori, nrRreshtaTrupiDistinctKategori, rreshtaNr);
            if (!mesazh)
                return mesazh;

            if (kgjrn.ToLower() == "po")
                mesazh = kontrolloVleraPerDokumentRialokimiSipasKushtitKGJRN(objBuxheti, dtDok);
            if (!mesazh)
                return mesazh;

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, objBuxheti, dtDok);
            return new MesazhSuksesi();
        }

        private clsMesazh kontrolloVleraPerDokumentRialokimiSipasKushtitKGJRN(ClsBKokaBuxheti objBuxheti, DateTime dtDok)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, objBuxheti, dtDok);

            DataTable llojeBuxhetiPerKategoriBuxheti = ClsBKategoriBuxhetimi.merrLlojeBuxhetiKategoriBuxhetiSipasNdermarrje(objBuxheti.IdNderm);
            foreach (var rresht in objBuxheti.OColBTrupiBuxheti)
            {
                decimal gjendjeKategorie = 0;
                decimal totalObjekti = 0;
                var kategoriBuxhetimi = new ClsBKategoriBuxhetimi(MessagesResource.Messages, rresht.IdKategoriBuxhetimi);

                if (!EkzistonDokumentBuxhetiPerKategori(rresht.IdKategoriBuxhetimi, 172, rresht.Periudha, dtDok))//check a ekziston dokument alokimi per periudhe dhe kategori
                    return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, $"Per periudhen {ClsBMuajt.Muaji(rresht.Periudha)} {dtDok.Year} per kategorine {kategoriBuxhetimi.Kodi} nuk eshte rregjistruar asnje dokument Alokim Buxheti");

                var buxheti = llojeBuxhetiPerKategoriBuxheti.Select($"ID = {rresht.IdBuxheti} AND IdKategoriBuxhetimi = {rresht.IdKategoriBuxhetimi}");
                if (buxheti.Count() < 1)
                    return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, $"Kategoria {kategoriBuxhetimi.Kodi} nuk eshte e lidhur me buxhetin perkates te zgjedhur");

                if (!(kategoriBuxhetimi.IdLlogaria > 0))
                    return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, $"Kategoria e buxhetimit {kategoriBuxhetimi.Kodi} nuk eshte e lidhur me llogari.");

                objBuxheti.OColBTrupiBuxheti.FindAll(x => x.IdKategoriBuxhetimi == rresht.IdKategoriBuxhetimi && x.IdBuxheti == rresht.IdBuxheti && x.Periudha == rresht.Periudha).ForEach(x => totalObjekti += x.Vlera);
                var gjendjePerDate = ktheGjendjePerKategoriBuxhetimi(objBuxheti.IdNderm, objBuxheti.DtDok, rresht.Periudha, rresht.IdKategoriBuxhetimi, objBuxheti.IdBuxhetiKoka, rresht.IdBuxheti, totalObjekti, objBuxheti.IdKatDok);
                if (gjendjePerDate == null)
                    return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, $"Gjendja per artikullin/analizen {kategoriBuxhetimi.Kodi} per periudhen {ClsBMuajt.Muaji(rresht.Periudha)} {dtDok.Year} eshte 0.");
                gjendjeKategorie = Convert.ToDecimal(gjendjePerDate["GJENDJE"]);
                if (rresht.Vlera < 0 && gjendjeKategorie < 0)
                    return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, $"Gjendja per artikullin/analizen {kategoriBuxhetimi.Kodi} per periudhen {ClsBMuajt.Muaji(rresht.Periudha)} {dtDok.Year} eshte {gjendjeKategorie.ToString()}.");
            }

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, objBuxheti, dtDok);
            return new MesazhSuksesi();
        }

        private bool EkzistonDokumentBuxhetiPerKategori(int idKategoriBuxhetimi, int idKatDok, int periudha, DateTime dtDok)
        {
            using (var db = new ClsDatabaseBuxheti())
                return db.EkzistonDokumentBuxhetiPerKategori(idKategoriBuxhetimi, idKatDok, periudha, dtDok);
        }

        private DataRow ktheGjendjePerKategoriBuxhetimi(int idNderm, DateTime? dtDok, int periudha, int idKategoriBuxhetimi, int idBuxhetiKoka, int idBuxheti, decimal vlera, int idKatDok)
        {
            using (var db = new ClsDatabaseBuxheti())
                return db.MerrGjendjePerKategoriBuxhetimi(idNderm, dtDok, periudha, idKategoriBuxhetimi, idBuxhetiKoka, idBuxheti, vlera, idKatDok);
        }

        private decimal MerrGjendjeMbeturNgaPerfitimi(int idNderm, DateTime? dtDok, int idBuxhetiKoka, int idBuxheti)
        {
            using (var db = new ClsDatabaseBuxheti())
                return db.MerrGjendjeMbeturNgaPerfitimi(idNderm, dtDok, idBuxhetiKoka, idBuxheti);
        }

        private DataRow ktheGjendjePerKategoriBuxhetimiNgaKonvertimi(int idKategoriBuxhetimi, int idTrupKonvertimiNga, int idBuxheti, int idBuxhetiKoka)
        {
            using (var db = new ClsDatabaseBuxheti())
                return db.MerrGjendjePerKategoriBuxhetimiNgaKonvertimi(idKategoriBuxhetimi, idTrupKonvertimiNga, idBuxheti, idBuxhetiKoka);
        }

        private clsMesazh kontrolloTotaletAlokimi(int idKonfigAmbjente, int idNderm, ColBTrupiBuxheti ocolBTrupiAlokimi)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idKonfigAmbjente, idNderm, ocolBTrupiAlokimi);

            var maxSingleErr = clsFunksione.ktheMarzhGabimiSipasKonfigurimit(idKonfigAmbjente, idNderm);
            var kategoriBuxhetimi = new ColBKategoriBuxhetimi(ocolBTrupiAlokimi[0].IdNdermarrje);
            var trupiGrupuar = konvertoTrupBuxhetiNeFormatGride(ocolBTrupiAlokimi);
            var trupiTree = krijoStruktureTrupi(trupiGrupuar, kategoriBuxhetimi);
            var mesazh = kontrolloRreshtaTrupiAlokimi(trupiTree, maxSingleErr);
            if (!mesazh)
                return mesazh;

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idKonfigAmbjente, idNderm, ocolBTrupiAlokimi);
            return new MesazhSuksesi("Kontrollet u kaluan me sukses.");
        }

        private List<Dictionary<string, object>> konvertoTrupBuxhetiNeFormatGride(ColBTrupiBuxheti ocolBTrupiAlokimi)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, ocolBTrupiAlokimi);
            var trupat = ocolBTrupiAlokimi.GroupBy(x => x.IdKategoriBuxhetimi).ToList();
            var listTrupash = new List<Dictionary<string, object>>();
            for (var i = 0; i < trupat.Count(); i++)
            {
                var trupMuajsh = trupat[i].ToList();
                var rowedData = new Dictionary<string, object>();
                rowedData.Add("IdKategoriBuxhetimi", trupMuajsh.First().IdKategoriBuxhetimi);
                rowedData.Add("Totali", trupMuajsh.First().VleraKategorise);
                rowedData.Add("TotaliMiratuar", trupMuajsh.First().VleraPlanifikuar);
                for (var j = 0; j < trupMuajsh.Count(); j++)
                {
                    rowedData.Add(ClsBMuajt.Muaji(trupMuajsh[j].Periudha), trupMuajsh[j].Vlera);
                }
                listTrupash.Add(rowedData);
            }
            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, ocolBTrupiAlokimi);
            return listTrupash;
        }
        
        private clsMesazh kontrolloNrAutoArt(out bool kaNdryshimNrAuto, ClsBKokaBuxheti objBuxheti, IDictionary<string, object> hidden)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, objBuxheti, hidden);

            clsDatabaseAdmin db = new clsDatabaseAdmin();
            List<NrAuto> list = DbAdmin.clsNrAutom.kontrollogjithenumrat(db, hidden, (DateTime)objBuxheti.DtDok);
            if (NrAuto.ktheVlerenEre(list, "txtNrDok") != "")
                objBuxheti.NrDok = NrAuto.ktheVlerenEre(list, "txtNrDok");
            DbCore.clsMesazh mes = NrAuto.ruajvlera(out kaNdryshimNrAuto, list, (DateTime)objBuxheti.DtDok, objBuxheti.IdPerdoruesi > 0 ? objBuxheti.IdPerdoruesi : objBuxheti.IdKrijuesi, objBuxheti.IdNderm, db);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, objBuxheti, hidden);
            return new clsMesazh(mes.Status, mes.PershkrimMesazhi);
        }

        private clsMesazh kontrolloTrupDokumentBuxheti(ClsBKokaBuxheti objBuxheti, DateTime dtDok, string kushtiTKLL_B, string[] llogariteNgaZLL_B)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, objBuxheti, dtDok);

            clsMesazh mesazh;
            int periudha = dtDok.Month;
            if(objBuxheti.IdKatDok == 179 && !(objBuxheti.OColBTrupiBuxheti.Find(x => x.Vlera < 0) == null))
                return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, "Trupi i dokumentit duhet te kete vetem rreshta me vlera pozitive");

            if (!(objBuxheti.OColBTrupiBuxheti.Find(x => x.Vlera == 0) == null))
                return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, "Trupi i dokumentit nuk mund te kete rreshta me vlere zero");
            string kodNiveli = clsNivelRegjistrimi.ktheKodNivelRegjistrimi(objBuxheti.IdNivel);
            decimal totaliPaTvshTrup = 0;
            decimal totaliMeTvshTrup = 0;
            int i = 0;
            ColBLlojBuxheti llojeBuxheti = ColBLlojBuxheti.KtheSipasNdermarrjesDheBijave(objBuxheti.IdNderm);
            DataTable kategoriBashkeMePrinder = ClsBKategoriBuxhetimi.MerrGjitheBijatBashkeMePrinderitFillestare(objBuxheti.IdNderm);
            foreach (var rresht in objBuxheti.OColBTrupiBuxheti)
            {
                i++;
                clsLlogari llogaria = new clsLlogari();
                clsArtikulli artikulli = new clsArtikulli();
                ClsBLlojBuxheti buxheti = llojeBuxheti.FirstOrDefault(x => x.IdLlojBuxheti == rresht.IdBuxheti);

                mesazh = kontrolloObjektTrupi(ref llogaria, ref artikulli, objBuxheti, rresht, kodNiveli, llogariteNgaZLL_B, kushtiTKLL_B);
                if (!mesazh)
                    return mesazh;

                if (!(objBuxheti.IdDokPostuesi > 0 ) && (buxheti == null || buxheti.IdLlojBuxheti <= 0))
                    return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, $"Ju nuk keni nje buxhet te zgjedhur per zerin {llogaria.NrLlogari}");

               mesazh = kontrolloGjendjePerKategoriBuxhetimi(objBuxheti, rresht, buxheti, llogaria, artikulli, kategoriBashkeMePrinder, periudha, i);
                if (!mesazh)
                    return mesazh;

                totaliMeTvshTrup += rresht.Vlera;
                totaliPaTvshTrup += rresht.VleraPaTvsh;
            }
            
            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, objBuxheti, dtDok);
            return kontrolloTotaletDokumentBuxheti(objBuxheti, totaliPaTvshTrup, totaliMeTvshTrup);
        }

        /// <summary>
        /// kontrollon gjendjen e mbetur per kategorite e buxhetimit sipas llojit te buxhetit dhe periudhes
        /// </summary>
        /// <param name="objBuxheti">koka e dokumenitit</param>
        /// <param name="rresht">rreshti i dokumentit</param>
        /// <param name="buxheti">lloji i buxhetit ne trup</param>
        /// <param name="llogaria">llogaria e zgjedhur si ze ne trup</param>
        /// <param name="kategoriBashkeMePrinder">gjithe kategorite e buxhetimit per kete ndermarrje sebashku me prinderit</param>
        /// <param name="periudha">muaji i zgjedhur ne rreshtin e trupit, ose te data e dokumentit</param>
        /// <param name="i">nr i rreshtit</param>
        /// <returns>kthen mesazh gabimi nese nuk ka gjendje</returns>
        private clsMesazh kontrolloGjendjePerKategoriBuxhetimi(ClsBKokaBuxheti objBuxheti, ClsBTrupiBuxheti rresht, ClsBLlojBuxheti buxheti, clsLlogari llogaria, clsArtikulli artikulli, DataTable kategoriBashkeMePrinder, int periudha, int i)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, objBuxheti, rresht, buxheti, llogaria, kategoriBashkeMePrinder, periudha, i);

            bool isLlogari = (rresht.LlojObjekti == (int)llojRreshtiShitje.Llogari);
            string voice = isLlogari ? llogaria.NrLlogari : artikulli.KodArtikulli;
            decimal rreshtGjendje = 0;
            decimal totalObjekti = 0;
            string dtDokGjendje = Convert.ToDateTime(objBuxheti.DtDok).ToShortDateString();

            if (buxheti.Kodi == "buxhetisekondar" && objBuxheti.IdKatDok != 177)
            {
                int idPrindFillestar = Convert.ToInt32((kategoriBashkeMePrinder.Select($"ID = {rresht.IdKategoriBuxhetimi}"))[0]["IDPRINDIFILLESTAR"]);
                objBuxheti.OColBTrupiBuxheti.FindAll(x => Convert.ToInt32((kategoriBashkeMePrinder.Select($"ID = {x.IdKategoriBuxhetimi}"))[0]["IDPRINDIFILLESTAR"]) == idPrindFillestar && x.IdBuxheti == rresht.IdBuxheti).ForEach(x => totalObjekti += x.Vlera);
            }
            else
                objBuxheti.OColBTrupiBuxheti.FindAll(x => x.IdKategoriBuxhetimi == rresht.IdKategoriBuxhetimi && x.IdBuxheti == rresht.IdBuxheti).ForEach(x => totalObjekti += x.Vlera);

            if (objBuxheti.IdKatDok == 181 && objBuxheti.IdKokaKonvertimiNga > 0)
            {
                clsMesazh mesazh= kontrolloGjendjeKonvertimiPerKategoriBuxhetimi(rresht, isLlogari, voice, i);
                if (!mesazh)
                    return mesazh;
            }
            else
            {
                var gjendjePerDate = ktheGjendjePerKategoriBuxhetimi(objBuxheti.IdNderm, objBuxheti.DtDok, periudha, rresht.IdKategoriBuxhetimi, objBuxheti.IdBuxhetiKoka, rresht.IdBuxheti, (objBuxheti.IdKatDok == 177 && objBuxheti.IdBuxhetiKoka <= 0) ? -totalObjekti : totalObjekti, objBuxheti.IdKatDok);
                if (gjendjePerDate != null)
                {
                    rreshtGjendje = Convert.ToDecimal(gjendjePerDate["GJENDJE"]);
                    dtDokGjendje = gjendjePerDate["DTDOK"].ToString();
                }

                if (((objBuxheti.IdKatDok == 177 && objBuxheti.IdStatusDok == 1) || objBuxheti.IdKatDok == 179 || (objBuxheti.IdKatDok == 181 && objBuxheti.IdStatusDok == 1)) && rreshtGjendje < 0)
                    return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, $"Gjendja e buxhetit {buxheti.Kodi} {(objBuxheti.IdKatDok == 177 ? "" : $"per zerin {voice}" )} ne daten {dtDokGjendje} shkon {(gjendjePerDate != null ? rreshtGjendje.ToString() : "negative")}");
            }

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, objBuxheti, rresht, buxheti, llogaria, kategoriBashkeMePrinder, periudha, i);
            return new MesazhSuksesi();
        }
        
        private clsMesazh kontrolloGjendjeKonvertimiPerKategoriBuxhetimi(ClsBTrupiBuxheti rresht, bool isLlogari, string voice, int rreshtNr)
        {
            DataRow gjendjeKovertimi = ktheGjendjePerKategoriBuxhetimiNgaKonvertimi(rresht.IdKategoriBuxhetimi, rresht.IdTrupiKonvertimiNga, rresht.IdBuxheti, rresht.IdBuxhetiKoka);
            decimal gjendje = Convert.ToDecimal(gjendjeKovertimi["VLERA"]);
            decimal sasi = Convert.ToDecimal(gjendjeKovertimi["SASIA"]);
            if (!isLlogari && sasi - Math.Abs((decimal)rresht.Sasia) < 0)
                return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, $"Sasia e mbetur e nga konvertimi per zerin {voice} ne rreshtin {rreshtNr} eshte {sasi}");
            if (gjendje - Math.Abs(rresht.Vlera) < 0)
                return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, $"Gjendja e mbetur e buxhetit nga konvertimi per zerin {voice} ne rreshtin {rreshtNr} eshte {gjendje}");
            return new MesazhSuksesi();
        }

        /// <summary>
        /// Kontrollon nese objekti i zgjedhur tek trupi i dokumentit eshte i vendosur sakte dhe nese eshte i lidhur me nje kategori ose buxhet
        /// </summary>
        /// <param name="llogaria">llogaria e zgjedhur si ze ne trup te dokumentit</param>
        /// <param name="objBuxheti">koka e dokumentit</param>
        /// <param name="rresht">rreshti i trupit te dokumentit</param>
        /// <param name="kodNiveli">niveli i rregjistrimit i dokumentit</param>
        /// <returns>Kthen mesazh gabimi nese nuk plotesohen kushtet, mesazh suksesi ne te kundert</returns>
        private clsMesazh kontrolloObjektTrupi(ref clsLlogari llogaria, ref clsArtikulli artikulli, ClsBKokaBuxheti objBuxheti, ClsBTrupiBuxheti rresht, string kodNiveli, string[] llogariteNgaZLL_B, string kushtiTKLL_B)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, llogaria, objBuxheti, rresht, kodNiveli);
            int idLlogaria = 0;
            string mesazhGabimiNukPerdorLlogari = string.Empty;
            string mesazhGabimiNukLlogariPaBuxhet = string.Empty;
            switch (rresht.LlojObjekti)
            {
                case (int)llojRreshtiShitje.Llogari:
                    idLlogaria = rresht.IdObjekti;
                    llogaria = new DbKontabiliteti.clsLlogari(idLlogaria);
                    mesazhGabimiNukPerdorLlogari = $"Llogaria me numer {llogaria.NrLlogari} nuk mund te perdoret!";
                    mesazhGabimiNukLlogariPaBuxhet = $"Llogaria me numer {llogaria.NrLlogari} nuk ka një kategori ose buxhet të specifikuar!";
                    break;
                case (int)llojRreshtiShitje.Artikull:
                    artikulli = new clsArtikulli(rresht.IdObjekti, new clsDatabaseInventari());
                    if ((artikulli.Klasa != 1 && ((artikulli.LlojiArt && kodNiveli.EqualsAnyIgnoreCase("PEB", "EB")) || (!artikulli.LlojiArt && kodNiveli.EqualsAnyIgnoreCase("PIB", "IB")))) || (artikulli.Klasa != 3 && objBuxheti.IdKatDok == 177))
                        return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, $"Artikulli me kod {artikulli.KodArtikulli} nuk mund te perdoret pasi nuk i perket llojit ose klases se duhur!");
                    idLlogaria = artikulli.LlojiArt ? artikulli.IdLlogariTeTrete : artikulli.IdLlogariBlerje;
                    llogaria = new DbKontabiliteti.clsLlogari(idLlogaria);
                    mesazhGabimiNukPerdorLlogari = $"Llogaria e blerjes se artikullit me kod {artikulli.KodArtikulli} nuk eshte e sakte!";
                    mesazhGabimiNukLlogariPaBuxhet = $"Llogaria e blerjes se artikullit me kod {artikulli.KodArtikulli} nuk ka një kategori ose buxhet të specifikuar!";
                    break;
            }

            if ((kushtiTKLL_B.ToLower() == "perjashtues" && llogariteNgaZLL_B.Contains(llogaria.NrLlogari)) || (kushtiTKLL_B.ToLower() == "perfshires" && !llogariteNgaZLL_B.Contains(llogaria.NrLlogari)))
                return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, mesazhGabimiNukPerdorLlogari);
            if (rresht.IdKategoriBuxhetimi <= 0 && objBuxheti.IdKatDok != 177)
                return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, mesazhGabimiNukLlogariPaBuxhet);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, llogaria, objBuxheti, rresht, kodNiveli);
            return new MesazhSuksesi();
        }
        
        private clsMesazh kontrolloRreshtaTrupiAlokimi(IEnumerable<TreeStructure<Dictionary<string, object>>> trupat, decimal marzhiGabimit)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, trupat, marzhiGabimit); string msg;
            foreach (var trupi in trupat)
            {
                if (trupi.Children.Count() <= 0)
                {
                    if (Convert.ToInt32(trupi.Item["idLlogaria"]) <= 0)
                        return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, $"Kategoria e buxhetimit {trupi.Item["kodi"].ToString()} nuk eshte e lidhur me llogari"); 

                    continue;
                }
                var prindi = (Dictionary<string, object>)trupi.Item["objekti"];
                if (Convert.ToInt32(trupi.Item["niveli"]) == 1 && (Math.Abs(Convert.ToDecimal(prindi["Totali"]) - Convert.ToDecimal(prindi["TotaliMiratuar"])) > marzhiGabimit))
                    return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, $"Totali i alokuar per kategorine e buxhetimit {trupi.Item["kodi"].ToString()} nuk eshte i barabarte me vleren e miratuar per kete kategori.");

                var muajtPrindi = new ClsBMuajt();
                muajtPrindi.Janar = Convert.ToDecimal(prindi["Janar"]);
                muajtPrindi.Shkurt = Convert.ToDecimal(prindi["Shkurt"]);
                muajtPrindi.Mars = Convert.ToDecimal(prindi["Mars"]);
                muajtPrindi.Prill = Convert.ToDecimal(prindi["Prill"]);
                muajtPrindi.Maj = Convert.ToDecimal(prindi["Maj"]);
                muajtPrindi.Qershor = Convert.ToDecimal(prindi["Qershor"]);
                muajtPrindi.Korrik = Convert.ToDecimal(prindi["Korrik"]);
                muajtPrindi.Gusht = Convert.ToDecimal(prindi["Gusht"]);
                muajtPrindi.Shtator = Convert.ToDecimal(prindi["Shtator"]);
                muajtPrindi.Tetor = Convert.ToDecimal(prindi["Tetor"]);
                muajtPrindi.Nentor = Convert.ToDecimal(prindi["Nentor"]);
                muajtPrindi.Dhjetor = Convert.ToDecimal(prindi["Dhjetor"]);

                var muajtBija = new ClsBMuajt();

                foreach (var trupiBije in trupi.Children)
                {
                    var bija = (Dictionary<string, object>)trupiBije.Item["objekti"];
                    muajtBija.Janar += Convert.ToDecimal(bija["Janar"]);
                    muajtBija.Shkurt += Convert.ToDecimal(bija["Shkurt"]);
                    muajtBija.Mars += Convert.ToDecimal(bija["Mars"]);
                    muajtBija.Prill += Convert.ToDecimal(bija["Prill"]);
                    muajtBija.Maj += Convert.ToDecimal(bija["Maj"]);
                    muajtBija.Qershor += Convert.ToDecimal(bija["Qershor"]);
                    muajtBija.Korrik += Convert.ToDecimal(bija["Korrik"]);
                    muajtBija.Gusht += Convert.ToDecimal(bija["Gusht"]);
                    muajtBija.Shtator += Convert.ToDecimal(bija["Shtator"]);
                    muajtBija.Tetor += Convert.ToDecimal(bija["Tetor"]);
                    muajtBija.Nentor += Convert.ToDecimal(bija["Nentor"]);
                    muajtBija.Dhjetor += Convert.ToDecimal(bija["Dhjetor"]);
                }


                var mesazh = kontrolloRreshtaTrupiAlokimi(trupi.Children, marzhiGabimit);
                if (!mesazh)
                    return mesazh;

                mesazh = kontrolloVleratMuajve(muajtPrindi, muajtBija, trupi.Item["kodi"].ToString());
                if (!mesazh)
                    return mesazh;
            }
            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, trupat, marzhiGabimit);
            return new MesazhSuksesi();
        }
        #endregion
        
    }
}
