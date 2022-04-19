using DbCore.IMBUtils.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.DbBuxheti
{
    public class ClsBKomponenteVlere
    {
        #region Properties
        public int Id { get; set; }
        public int IdKoka { get; set; }
        public int IdNdermarrje { get; set; }
        public int IdKrijuesi { get; set; }
        public int IdModifikuesi { get; set; }
        public DateTime? DtKrijimi { get; set; }
        public DateTime? DtModifikimi { get; set; }
        public int IdKomponente { get; set; }
        public int IdQendraShendetesore { get; set; }
        public decimal Vlera { get; set; }
        public decimal? VleraMin { get; set; }
        public decimal? VleraMax { get; set; }
        public int? Tipi { get; set; }
        public bool NgaGjenerimi { get; set; }
        public string QendraShendetesore { get; set; }
        public string Komponente { get; set; }
        #endregion
        
        #region Konstruktore
        public ClsBKomponenteVlere() { }

        public ClsBKomponenteVlere(int id, int idKoka, int idNdermarrje, int idKrijuesi, int idModifikuesi, DateTime? dtKrijimi, DateTime? dtModifikimi, int idKomponente, int idQendraShendetesore, decimal vlera, decimal? vleraMin, decimal? vleraMax, int? tipi, bool ngaGjenerimi, string qendraShendetesore, string komponente)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, id, idKoka, idNdermarrje, idKrijuesi, idModifikuesi, dtKrijimi, dtModifikimi, idKomponente, idQendraShendetesore, vlera, vleraMin, vleraMax, tipi, ngaGjenerimi, qendraShendetesore, komponente);

            Id = id;
            IdKoka = idKoka;
            IdNdermarrje = idNdermarrje;
            IdKrijuesi = idKrijuesi;
            IdModifikuesi = idModifikuesi;
            DtKrijimi = dtKrijimi;
            DtModifikimi = dtModifikimi;
            IdKomponente = idKomponente;
            IdQendraShendetesore = idQendraShendetesore;
            Vlera = vlera;
            VleraMin = vleraMin;
            VleraMax = vleraMax;
            Tipi = tipi;
            NgaGjenerimi = ngaGjenerimi;
            QendraShendetesore = qendraShendetesore;
            Komponente = komponente;

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, id, idKoka, idNdermarrje, idKrijuesi, idModifikuesi, dtKrijimi, dtModifikimi, idKomponente, idQendraShendetesore, vlera, vleraMin, vleraMax, tipi, ngaGjenerimi, qendraShendetesore, komponente);
        }

        public ClsBKomponenteVlere(IDataRecord record)
        {
            Mbush(record);
        }
        #endregion

        #region Internal
        internal void Mbush(IDataRecord record)
        {
            Id = !Convert.IsDBNull(record["ID"]) ? Convert.ToInt32(record["ID"]) : 0;
            IdKoka = !Convert.IsDBNull(record["IDKOKA"]) ? Convert.ToInt32(record["IDKOKA"]) : 0;
            IdNdermarrje = !Convert.IsDBNull(record["IDNDERMARRJE"]) ? Convert.ToInt32(record["IDNDERMARRJE"]) : 0;
            IdKrijuesi = !Convert.IsDBNull(record["IDKRIJUESI"]) ? Convert.ToInt32(record["IDKRIJUESI"]) : 0;
            IdModifikuesi = !Convert.IsDBNull(record["IDMODIFIKUESI"]) ? Convert.ToInt32(record["IDMODIFIKUESI"]) : 0;
            DtKrijimi = !Convert.IsDBNull(record["DTKRIJIMI"]) ? Convert.ToDateTime(record["DTKRIJIMI"]) : (DateTime?)null;
            DtModifikimi = !Convert.IsDBNull(record["DTMODIFIKIMI"]) ? Convert.ToDateTime(record["DTMODIFIKIMI"]) : (DateTime?)null;
            IdKomponente = !Convert.IsDBNull(record["IDKOMPONENTE"]) ? Convert.ToInt32(record["IDKOMPONENTE"]) : 0;
            IdQendraShendetesore = !Convert.IsDBNull(record["IDQENDRASHENDETESORE"]) ? Convert.ToInt32(record["IDQENDRASHENDETESORE"]) : 0;
            Vlera = !Convert.IsDBNull(record["VLERA"]) ? Convert.ToDecimal(record["VLERA"]) : 0;
            VleraMin = !Convert.IsDBNull(record["VLERAMIN"]) ? Convert.ToDecimal(record["VLERAMIN"]) : (decimal?)null;
            VleraMax = !Convert.IsDBNull(record["VLERAMAX"]) ? Convert.ToDecimal(record["VLERAMAX"]) : (decimal?)null;
            Tipi = !Convert.IsDBNull(record["TIPI"]) ? Convert.ToInt32(record["TIPI"]) : (int?)null;
            NgaGjenerimi = !Convert.IsDBNull(record["NGAGJENERIMI"]) ? Convert.ToBoolean(record["NGAGJENERIMI"]) : false;
            QendraShendetesore = !Convert.IsDBNull(record["QENDRASHENDETESORE"]) ? Convert.ToString(record["QENDRASHENDETESORE"]) : "";
            Komponente = !Convert.IsDBNull(record["KOMPONENTE"]) ? Convert.ToString(record["KOMPONENTE"]) : "";
        }

        #endregion        
    }
}
