using DbCore.IMBUtils.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DbCore.IMBUtils.Logging;

namespace DbCore.DbBuxheti
{
    public class ClsBKomponenteLidhje
    {
        #region Properties
        public int Id { get; set; }
        public int IdKomponente { get; set; }
        public int IdNdermarrje { get; set; }
        public string KodNdermarrje { get; set; }
        public string PershkrimNdermarrje { get; set; }
        public int IdKategoriBuxhetimi { get; set; }
        public string KodKategoriBuxhetimi { get; set; }
        public string PershkrimKategoriBuxhetimi { get; set; }
        #endregion

        #region Constructors
        public ClsBKomponenteLidhje() { }

        public ClsBKomponenteLidhje(int id, int idKomponente, int idNdermarrje, string kodNdermarrje, string pershkrimNdermarrje, int idKategoriBuxhetimi, string kodKategoriBuxhetimi, string pershkrimKategoriBuxhetimi)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, id, idKomponente, idNdermarrje, kodNdermarrje, pershkrimNdermarrje, idKategoriBuxhetimi, kodKategoriBuxhetimi, pershkrimKategoriBuxhetimi);

            Id = id;
            IdKomponente = idKomponente;
            IdNdermarrje = idNdermarrje;
            KodNdermarrje = kodNdermarrje;
            PershkrimNdermarrje = pershkrimNdermarrje;
            IdKategoriBuxhetimi = idKategoriBuxhetimi;
            KodKategoriBuxhetimi = kodKategoriBuxhetimi;
            PershkrimKategoriBuxhetimi = pershkrimKategoriBuxhetimi;

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, id, idKomponente, idNdermarrje, kodNdermarrje, pershkrimNdermarrje, idKategoriBuxhetimi, kodKategoriBuxhetimi, pershkrimKategoriBuxhetimi);
        }

        public ClsBKomponenteLidhje(IDataRecord record)
        {
            Mbush(record);
        }
        #endregion

        #region Internal
        internal void Mbush(IDataRecord record)
        {
            Id = !Convert.IsDBNull(record["ID"]) ? Convert.ToInt32(record["ID"]) : 0;
            IdKomponente = !Convert.IsDBNull(record["IDKOMPONENTE"]) ? Convert.ToInt32(record["IDKOMPONENTE"]) : 0;
            IdNdermarrje = !Convert.IsDBNull(record["IDNDERMARRJE"]) ? Convert.ToInt32(record["IDNDERMARRJE"]) : 0;
            KodNdermarrje = !Convert.IsDBNull(record["KODNDERMARRJE"]) ? Convert.ToString(record["KODNDERMARRJE"]) : "";
            PershkrimNdermarrje = !Convert.IsDBNull(record["PERSHKRIMNDERMARRJE"]) ? Convert.ToString(record["PERSHKRIMNDERMARRJE"]) : "";
            IdKategoriBuxhetimi = !Convert.IsDBNull(record["IDKATEGORIBUXHETIMI"]) ? Convert.ToInt32(record["IDKATEGORIBUXHETIMI"]) : 0;
            KodKategoriBuxhetimi = !Convert.IsDBNull(record["KODKATEGORIBUXHETIMI"]) ? Convert.ToString(record["KODKATEGORIBUXHETIMI"]) : "";
            PershkrimKategoriBuxhetimi = !Convert.IsDBNull(record["PERSHKRIMKATEGORIBUXHETIMI"]) ? Convert.ToString(record["PERSHKRIMKATEGORIBUXHETIMI"]) : "";
        }
        #endregion
    }
}
