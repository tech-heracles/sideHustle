using System;
using System.Data;
using System.Linq;
using AlphaWeb.Core.Interfaces.Data;
using AlphaWeb.Core.Interfaces.Localization;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Validation;
using DbCore.IMBUtils.Logging;
using DbCore.IMBUtils.Messages;
using Newtonsoft.Json;

namespace DbCore.DbBuxheti
{
    public class ClsBKategoriBuxhetimi : IDataBase
    {
        #region Atributes
        public int IdKategoriBuxhetimi { get; set; }
        public string Kodi { get; set; }
        public string Pershkrimi { get; set; }
        public string Pershkrimi2 { get; set; }
        public int IdNdermarrje { get; set; }
        public int IdModifikuesi { get; set; }
        public int IdKrijuesi { get; set; }
        public DateTime? DtKrijimi { get; set; }
        public DateTime? DtModifikimi { get; set; }
        public int IdPrindi { get; set; }
        public int NivelKategorie { get; set; }
        public ColBLlojBuxheti LlojeBuxheti { get; set; }
        public string Buxheti { get; set; }
        public int IdLlogaria { get; set; }
        public decimal? Koeficenti { get; set; }
        public bool Aktive { get; set; }
        private bool integroKategoriTeNdermBija;
        private IMessagesResource Messages { get; set; }
        #endregion

        #region Constructors
        public ClsBKategoriBuxhetimi(IMessagesResource messages)
        {
            Messages = messages;
            LlojeBuxheti = new ColBLlojBuxheti();
        }

        public ClsBKategoriBuxhetimi(IMessagesResource messages, int idKategoriBuxhetimi)
        {
            Messages = messages;
            using (var db = new ClsDatabaseBuxheti())
                db.MerrKategoriBuxhetimiSipasIdKategoriBuxhetimi(idKategoriBuxhetimi, this);
        }

        public ClsBKategoriBuxhetimi(IMessagesResource messages, int idKategoriBuxhetimi, string kodi, string pershkrimi, string pershkrimi2, int idNdermarrje, int idPrindi, int nivelKategorie, ColBLlojBuxheti llojeBuxheti, decimal? koeficenti, int idLlogaria, bool integroKategoriTeNdermBija, bool aktive)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, messages, idKategoriBuxhetimi, kodi, pershkrimi, pershkrimi2, idNdermarrje, idPrindi, nivelKategorie, llojeBuxheti, koeficenti, idLlogaria, integroKategoriTeNdermBija, aktive);

            IdKategoriBuxhetimi = idKategoriBuxhetimi;
            Kodi = kodi;
            Pershkrimi = pershkrimi;
            Pershkrimi2 = pershkrimi2;
            IdNdermarrje = idNdermarrje;
            IdPrindi = idPrindi;
            NivelKategorie = nivelKategorie;
            LlojeBuxheti = llojeBuxheti;
            Koeficenti = koeficenti;
            IdLlogaria = idLlogaria;
            this.integroKategoriTeNdermBija = integroKategoriTeNdermBija;
            Aktive = aktive;
            Messages = messages;

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, messages, idKategoriBuxhetimi, kodi, pershkrimi, pershkrimi2, idNdermarrje, idPrindi, nivelKategorie, llojeBuxheti, koeficenti, idLlogaria, integroKategoriTeNdermBija, aktive);
        }

        public ClsBKategoriBuxhetimi(IDataRecord record)
        {
            Mbush(record);
        }

        public ClsBKategoriBuxhetimi()
        {
        }
        
        #endregion

        #region Public
        
        public void Mbush(IDataRecord record)
        {
            IdKategoriBuxhetimi = !Convert.IsDBNull(record["ID"]) ? Convert.ToInt32(record["ID"]) : 0;
            Kodi = !Convert.IsDBNull(record["KODI"]) ? Convert.ToString(record["KODI"]) : String.Empty;
            Pershkrimi = !Convert.IsDBNull(record["PERSHKRIMI"]) ? Convert.ToString(record["PERSHKRIMI"]) : String.Empty;
            Pershkrimi2 = !Convert.IsDBNull(record["PERSHKRIMI2"]) ? Convert.ToString(record["PERSHKRIMI2"]) : String.Empty;
            IdNdermarrje = !Convert.IsDBNull(record["IDNDERMARRJE"]) ? Convert.ToInt32(record["IDNDERMARRJE"]) : 0;
            IdModifikuesi = !Convert.IsDBNull(record["IDMODIFIKUESI"]) ? Convert.ToInt32(record["IDMODIFIKUESI"]) : 0;
            IdKrijuesi = !Convert.IsDBNull(record["IDKRIJUESI"]) ? Convert.ToInt32(record["IDKRIJUESI"]) : 0;
            DtKrijimi = !Convert.IsDBNull(record["DTKRIJIMI"]) ? Convert.ToDateTime(record["DTKRIJIMI"]) : (DateTime?)null;
            DtModifikimi = !Convert.IsDBNull(record["DTMODIFIKIMI"]) ? Convert.ToDateTime(record["DTMODIFIKIMI"]) : (DateTime?)null;
            IdPrindi = !Convert.IsDBNull(record["IDPRINDI"]) ? Convert.ToInt32(record["IDPRINDI"]) : 0;
            NivelKategorie = !Convert.IsDBNull(record["NIVELKATEGORIE"]) ? Convert.ToInt32(record["NIVELKATEGORIE"]) : 0;
            Buxheti = !Convert.IsDBNull(record["BUXHETI"]) ? Convert.ToString(record["BUXHETI"]) : String.Empty;
            IdLlogaria = !Convert.IsDBNull(record["IDLLOGARIA"]) ? Convert.ToInt32(record["IDLLOGARIA"]) : 0;
            Koeficenti = !Convert.IsDBNull(record["KOEFICENTI"]) ? Convert.ToDecimal(record["KOEFICENTI"]) : (decimal?)null;
            Aktive = !Convert.IsDBNull(record["AKTIVE"]) ? Convert.ToBoolean(record["AKTIVE"]) : false;
        }

        public clsMesazh Ruaj()
        {
            try
            {
                ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

                using (var scope = new MyTransactionScope())
                {
                    var mesazh = Valido();
                    if (!mesazh.Status) return mesazh;

                    mesazh = RuajKategoriBuxhetimi();
                    if (!mesazh.Status) return mesazh;

                    mesazh = RuajKategoriBuxhetimiNeHistorik();
                    if (!mesazh.Status) return mesazh;

                    mesazh = RuajLlojeBuxhetiPerKategoriBuxhetimi();

                    if (mesazh.Status)
                        scope.Complete();

                    ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
                    return mesazh;
                }
            }
            catch (Exception ex)
            {
                ImbLogger.LogErrorBuxhetimi(ex);
                return new MesazhGabimi(ex.Message);
            }
        }

        public clsMesazh Modifiko()
        {
            try
            {
                ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);
                using (var scope = new MyTransactionScope())
                {
                    var mesazh = Valido();
                    if (!mesazh.Status) return mesazh;

                    mesazh = ModifikoKategoriBuxhetimi();
                    if (!mesazh.Status) return mesazh;

                    mesazh = RuajKategoriBuxhetimiNeHistorik();
                    if (!mesazh.Status) return mesazh;

                    mesazh = FshiLlojeBuxhetiPerKategoriBuxhetimi();
                    if (!mesazh.Status) return mesazh;

                    mesazh = RuajLlojeBuxhetiPerKategoriBuxhetimi();

                    if (mesazh.Status)
                        scope.Complete();

                    ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
                    return mesazh;
                }
            }
            catch (Exception ex)
            {
                ImbLogger.LogErrorBuxhetimi(ex);
                return new MesazhGabimi(ex.Message);
            }
        }

        public clsMesazh Fshi()
        {
            try
            {
                ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);
                using (var scope = new MyTransactionScope())
                {

                    clsMesazh mesazh = LejoFshirje(MessagesResource.Messages, EshteKategoriBuxhetimiLidhur());
                    if (!mesazh.Status) return mesazh;

                    mesazh = FshiLlojeBuxhetiPerKategoriBuxhetimi();
                    if (!mesazh.Status) return mesazh;

                    mesazh = RuajKategoriBuxhetimiNeHistorik();
                    if (!mesazh.Status) return mesazh;

                    mesazh = FshiKategoriBuxhetimi();

                    if (mesazh.Status)
                        scope.Complete();

                    ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
                    return mesazh;
                }
            }
            catch (Exception ex)
            {
                ImbLogger.LogErrorBuxhetimi(ex);
                return new MesazhGabimi(ex.Message);
            }
        }

        public clsMesazh Valido()
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);
            var mesazh = ValidoProperty();
            if (!mesazh.Status)
                return mesazh;

            var eshtePrind = IdKategoriBuxhetimi > 0 ? EshteKategoriBuxhetimiPrind() : false;

            if (eshtePrind && IdLlogaria > 0)
                return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, Messages["msgKategoriBuxhetimiNukZgjedhDotLlogari"]);

            if (IdKategoriBuxhetimi > 0)
            {
                ClsBKategoriBuxhetimi kokaEkzistuese = new ClsBKategoriBuxhetimi(Messages, IdKategoriBuxhetimi);
                if (EshteLlogariKategoriBuxhetimiLidhur() && IdLlogaria != kokaEkzistuese.IdLlogaria)
                    return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, Messages["msgLlogariKategoriBuxhetimiLidhur"]);

                if (EshteKategoriBuxhetimiLidhur() && IdPrindi != kokaEkzistuese.IdPrindi)
                    return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, Messages["msgKategoriBuxhetimiLidhurMosNdryshoPrind"]);
            }

            if (EkzistonKategoriBuxhetimiMeKeteKod() && !(IdKategoriBuxhetimi > 0))
                return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, $"{Messages["msgEkzistonKategoriBuxhetimi"]} {Kodi}");

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
            return new MesazhSuksesi(Messages["msgKontrolletUKaluanMeSukses"]);
        }

        public clsMesazh ValidoProperty()
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            clsMesazh kontrolloFushe;
            if (string.IsNullOrEmpty(Kodi))
                return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, Messages["msgKodiBosh"]);

            kontrolloFushe = clsFunksione.kontrolloKaraktereMeMesazh(Kodi, FusheKontrolli.Kodi, Messages, false);
            if (!kontrolloFushe.Status)
            {
                ImbLogger.LogWarningBuxhetimi(kontrolloFushe.PershkrimMesazhi);
                return kontrolloFushe;
            }

            kontrolloFushe = clsFunksione.kontrolloKaraktereMeMesazh(Pershkrimi, FusheKontrolli.Pershkrimi, Messages, true);
            if (!kontrolloFushe.Status)
            {
                ImbLogger.LogWarningBuxhetimi(kontrolloFushe.PershkrimMesazhi);
                return kontrolloFushe;
            }

            kontrolloFushe = clsFunksione.kontrolloKaraktereMeMesazh(Pershkrimi2, FusheKontrolli.Pershkrimi, Messages, true);
            if (!kontrolloFushe.Status)
            {
                ImbLogger.LogWarningBuxhetimi(kontrolloFushe.PershkrimMesazhi);
                return kontrolloFushe;
            }

            if (Koeficenti != null && (Koeficenti <= 0 || Koeficenti > 1))
                return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, Messages["msgKategoriBuxhetimiKoeficenti"]);

            if (IdKategoriBuxhetimi > 0 && IdKategoriBuxhetimi == IdPrindi)
                return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, Messages["msgKategoriBuxhetimiPrindVetvetja"]);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
            return new MesazhSuksesi(Messages["msgKontrolletUKaluanMeSukses"]);
        }
        
        public clsMesazh LejoFshirje(IMessagesResource messages, bool lidhur)
        {
            if (lidhur)
                return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, messages["msgKategoriBuxhetimiLidhur"]);
            return new MesazhSuksesi();
        }
        public bool GetIntegroKategoriTeNdermBija()
        {
            return this.integroKategoriTeNdermBija;
        }

        public void SetIntegroKategoriTeNdermBija(bool integroKategoriTeNdermBija)
        {
            this.integroKategoriTeNdermBija = integroKategoriTeNdermBija;
        }

        public static DataTable merrLlojeBuxhetiKategoriBuxhetiSipasNdermarrje(int idNdermarrje)
        {
            using (var db = new ClsDatabaseBuxheti())
                return db.merrLlojeBuxhetiKategoriBuxhetiSipasNdermarrje(idNdermarrje);
        }

        public static DataTable MerrGjitheBijatBashkeMePrinderitFillestare(int idNdermarrje)
        {
            using (var db = new ClsDatabaseBuxheti())
                return db.MerrGjitheBijatBashkeMePrinderitFillestare(idNdermarrje);
        }

        #endregion

        #region Private

        private clsMesazh RuajKategoriBuxhetimi()
        {
            using (var db = new ClsDatabaseBuxheti())
                return db.RuajKategoriBuxhetimi(this);
        }

        private clsMesazh RuajKategoriBuxhetimiNeHistorik()
        {
            using (var db = new ClsDatabaseBuxheti())
                return db.RuajKategoriBuxhetimiNeHistorik(this);
        }

        private bool EkzistonKategoriBuxhetimiMeKeteKod()
        {
            using (var db = new ClsDatabaseBuxheti())
                return db.EkzistonKategoriBuxhetimiMeKeteKod(this.Kodi, this.IdNdermarrje, this.GetIntegroKategoriTeNdermBija());
        }

        private bool EshteKategoriBuxhetimiLidhur()
        {
            using (var db = new ClsDatabaseBuxheti())
                return db.EshteKategoriBuxhetimiLidhur(this);
        }

        private bool EshteLlogariKategoriBuxhetimiLidhur()
        {
            using (var db = new ClsDatabaseBuxheti())
                return db.EshteLlogariKategoriBuxhetimiLidhur(this);
        }

        private bool EshteKategoriBuxhetimiPrind()
        {
            using (var db = new ClsDatabaseBuxheti())
                return db.EshteKategoriBuxhetimiPrind(this);
        }

        private clsMesazh RuajLlojeBuxhetiPerKategoriBuxhetimi()
        {
            if (LlojeBuxheti != null && LlojeBuxheti.Count <= 0)
                return new MesazhSuksesi(Messages["msgRuajtjeMeSukses"]);
            var idBuxhete = LlojeBuxheti.Select(x => x.IdLlojBuxheti).ToArray();
            using (var db = new ClsDatabaseBuxheti())
                return db.RuajLlojeBuxhetiPerKategoriBuxhetimi(this, String.Join(",", idBuxhete));
        }

        private clsMesazh FshiLlojeBuxhetiPerKategoriBuxhetimi()
        {
            if (LlojeBuxheti != null && LlojeBuxheti.Count <= 0)
                return new MesazhSuksesi(Messages["msgFshirjeMeSukses"]);
            using (var db = new ClsDatabaseBuxheti())
                return db.FshiLlojeBuxhetiPerKategoriBuxhetimi(this);
        }

        private clsMesazh ModifikoKategoriBuxhetimi()
        {
            using (var db = new ClsDatabaseBuxheti())
                return db.ModifikoKategoriBuxhetimi(this);
        }

        private clsMesazh FshiKategoriBuxhetimi()
        {
            using (var db = new ClsDatabaseBuxheti())
                return db.FshiKategoriBuxhetimi(this);
        }

        #endregion
    }
}
