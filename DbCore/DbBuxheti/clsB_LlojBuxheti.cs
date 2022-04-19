using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Logging;
using DbCore.IMBUtils.Messages;
using DbCore.IMBUtils.Validation;
using System;
using System.Data;
using AlphaWeb.Core.Interfaces.Data;
using AlphaWeb.Core.Interfaces.Localization;

namespace DbCore.DbBuxheti
{
    public class ClsBLlojBuxheti : IDataBase
    {
        #region Atributes
        public int IdLlojBuxheti { get; set; }
        public string Kodi { get; set; }
        public string Pershkrimi { get; set; }
        public int IdNdermarrje { get; set; }
        public int IdModifikuesi { get; set; }
        public int IdKrijuesi { get; set; }
        public int IdStatusDok { get; set; }
        public DateTime? DtKrijimi { get; set; }
        public DateTime? DtModifikimi { get; set; }
        private bool integroBuxhetTeNdermBija;
        private IMessagesResource Messages { get; set; }
        #endregion

        #region Constructors

        public ClsBLlojBuxheti(IMessagesResource messages)
        {
            Messages = messages;
        }

        public ClsBLlojBuxheti(IMessagesResource messages, int idLlojBuxheti)
        {
            Messages = messages;
            using (var db = new ClsDatabaseBuxheti())
                db.MerrLlojBuxhetiSipasIdLlojBuxhetu(idLlojBuxheti, this);
        }

        public ClsBLlojBuxheti(IMessagesResource messages, string kodLlojBuxheti, int idNdermarrje)
        {
            Messages = messages;
            using (var db = new ClsDatabaseBuxheti())
                db.MerrLlojBuxhetiSipasKodLlojBuxheti(kodLlojBuxheti, idNdermarrje, this);
        }

        public ClsBLlojBuxheti(IDataRecord record)
        {
            Mbush(record);
        }

        public ClsBLlojBuxheti(IMessagesResource messages, int idLlojBuxheti, string kodi, string pershkrimi,  int idNdermarrje)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idLlojBuxheti, kodi, pershkrimi, idNdermarrje);

            IdLlojBuxheti = idLlojBuxheti;
            Kodi = kodi;
            Pershkrimi = pershkrimi;
            IdNdermarrje = idNdermarrje;
            Messages = messages;

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idLlojBuxheti, kodi, pershkrimi, idNdermarrje);
        }
        #endregion

        #region Public

        public void Mbush(IDataRecord record)
        {
            IdLlojBuxheti = !Convert.IsDBNull(record["ID"]) ? Convert.ToInt32(record["ID"]) : 0;
            Kodi = !Convert.IsDBNull(record["KODI"]) ? Convert.ToString(record["KODI"]) : String.Empty;
            Pershkrimi = !Convert.IsDBNull(record["PERSHKRIMI"]) ? Convert.ToString(record["PERSHKRIMI"]) : String.Empty;
            IdNdermarrje = !Convert.IsDBNull(record["IDNDERMARRJE"]) ? Convert.ToInt32(record["IDNDERMARRJE"]) : 0;
            IdModifikuesi = !Convert.IsDBNull(record["IDMODIFIKUESI"]) ? Convert.ToInt32(record["IDMODIFIKUESI"]) : 0;
            IdKrijuesi = !Convert.IsDBNull(record["IDKRIJUESI"]) ? Convert.ToInt32(record["IDKRIJUESI"]) : 0;
            IdStatusDok = !Convert.IsDBNull(record["IDSTATUSDOK"]) ? Convert.ToInt32(record["IDSTATUSDOK"]) : 0;
            DtKrijimi = !Convert.IsDBNull(record["DTKRIJIMI"]) ? Convert.ToDateTime(record["DTKRIJIMI"]) : (DateTime?) null;
            DtModifikimi = !Convert.IsDBNull(record["DTMODIFIKIMI"]) ? Convert.ToDateTime(record["DTMODIFIKIMI"]) : (DateTime?) null;
        }

        public clsMesazh Ruaj()
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            clsMesazh mesazh = Valido();
            if (!mesazh.Status)
                return mesazh;

            mesazh = RuajLlojBuxheti();

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
            return mesazh;
        }

        public clsMesazh Fshi()
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            clsMesazh mesazh = LejoFshirje(MessagesResource.Messages, EshteLlojBuxhetiLidhur());
            if(mesazh.Status)
                mesazh = FshiLlojBuxheti();
            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
            return mesazh;
        }

        public clsMesazh Modifiko()
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            clsMesazh mesazh = Valido();
            if (!mesazh.Status)
                return mesazh;

            mesazh = ModifikoLlojBuxheti();
            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
            return mesazh;
        }

        public clsMesazh Valido()
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            var mesazh = ValidoProperty();
            if (!mesazh.Status)
                return mesazh;

            if (EkzistonLlojBuxhetiMeKeteKod() && !(IdLlojBuxheti > 0))
                return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, $"{Messages["msgEkzistonBuxheti"]} {Kodi}");

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
                return kontrolloFushe;

            if (string.IsNullOrEmpty(Pershkrimi))
                return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, Messages["msgPershkrimiBosh"]);

            kontrolloFushe = clsFunksione.kontrolloKaraktereMeMesazh(Pershkrimi, FusheKontrolli.Pershkrimi, Messages, true);
            if (!kontrolloFushe.Status)
                return kontrolloFushe;

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
            return new MesazhSuksesi(Messages["msgKontrolletUKaluanMeSukses"]);
        }

        public clsMesazh LejoFshirje(IMessagesResource messages, bool lidhur)
        {
            if (lidhur)
                return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, $"{Kodi} - {messages["msgBuxhetiLidhur"]}");
            return new MesazhSuksesi();
        }
        public bool GetIntegroBuxhetTeNdermBija()
        {
            return this.integroBuxhetTeNdermBija;
        }

        public void SetIntegroBuxhetTeNdermBija(bool integroBuxhetTeNdermBija)
        {
            this.integroBuxhetTeNdermBija = integroBuxhetTeNdermBija;
        }

        #endregion

        #region Private

        private clsMesazh RuajLlojBuxheti()
        {
            using (var db = new ClsDatabaseBuxheti())
                return db.RuajLlojBuxheti(this);
        }

        private clsMesazh FshiLlojBuxheti()
        {
            using (var db = new ClsDatabaseBuxheti())
                return db.FshiLlojBuxheti(this);
        }

        private clsMesazh ModifikoLlojBuxheti()
        {
            using (var db = new ClsDatabaseBuxheti())
                return db.ModifikoLlojBuxheti(this);
        }

        private bool EshteLlojBuxhetiLidhur()
        {
            using (var db = new ClsDatabaseBuxheti())
                return db.EshteLlojBuxhetiLidhur(this);
        }

        private bool EkzistonLlojBuxhetiMeKeteKod()
        {
            using (var db = new ClsDatabaseBuxheti())
                return db.EkzistonLlojBuxhetiMeKeteKod(this);
        }

        #endregion
    }
}
