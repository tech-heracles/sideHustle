using System;
using System.Data;
using DbCore.DbShare;

namespace DbCore.DbAdmin
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne filtrin
    ///  qe i aplikohet nje gride(Te dhenat  merren nga tabela : T_FILTRAGRIDA)
    /// </summary>
    public class clsFiltraGrida
    {
        #region Atributet

        private int _idFiltra;
        private string _filtraKodi;
        private string _filtraShenime;
        private string _filtraVlera;
        private int _gridaKokaId;
        private bool _filtraUniversal;
        private int _idPerdoruesi;
        private string _koloneRenditje;
        private bool _drejtimRenditje;
        private int _idNdermarje;
        private int _idStatusDok;
        private DateTime _dtKrijimi;
        private DateTime _dtModifikimi;
        private DataRow rreshti;

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdFiltra
        {
            get { return _idFiltra; }
            set { _idFiltra = value; }
        }

        /// <summary>
        /// Kthen/Vendos kodin e filtrit.
        /// </summary>
        public string FiltraKodi
        {
            get { return _filtraKodi; }
            set { _filtraKodi = value; }
        }

        /// <summary>
        /// Kthen/Vendos shenimet qe jane vendosur kur eshte krijuar ky filter.
        /// </summary>
        public string FiltraShenime
        {
            get { return _filtraShenime; }
            set { _filtraShenime = value; }
        }

        /// <summary>
        /// Kthen/Vendos vlere qe permban ky filter.
        /// </summary>
        public string FiltraVlera
        {
            get { return _filtraVlera; }
            set { _filtraVlera = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e grides nga e cila eshte krijuar si filter.
        /// </summary>
        public int GridaKokaId
        {
            get { return _gridaKokaId; }

            set { _gridaKokaId = value; }
        }

        /// <summary>
        /// Kthen/Vendos nese filtri do shfaqet ne te gjitha gridat apo vetem tek 
        /// grida nga e cila eshte krijuar.
        /// </summary>
        public bool FiltraUniversal
        {
            get { return _filtraUniversal; }
            set { _filtraUniversal = value; }
        }

        /// <summary>
        /// Kthen/Vendos kodin e kolones se grides sipas se ciles duhet te jete e 
        /// renditur grida.
        /// </summary>
        public string KoloneRenditje
        {
            get { return _koloneRenditje; }
            set
            { _koloneRenditje = value; }
        }

        /// <summary>
        /// Kthen/Vendos drejtimin sipas se cilit do te renditet grida.
        /// </summary>
        public bool DrejtimRenditje
        {
            get { return _drejtimRenditje; }
            set { _drejtimRenditje = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e perdoruesit i cili e ka krijuar kete filter.
        /// </summary>
        public int IdPerdoruesi
        {
            get { return _idPerdoruesi; }
            set { _idPerdoruesi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e ndermarrjes se ciles i perket ky filter.
        /// </summary>
        public int IdNdermarje
        {
            get { return _idNdermarje; }
            set { _idNdermarje = value; }
        }
        public int IdStatusDok
        {
            get { return _idStatusDok; }
            set { _idStatusDok = value; }
        }

        public DateTime DtKrijimi => _dtKrijimi;

        public DateTime DtModifikimi => _dtModifikimi;

        #endregion

        #region Konstruktoret

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idFiltra">id e filtrit</param>
        public clsFiltraGrida(int idFiltra)
        {
            if (idFiltra == 0) return;
            using (var data = new clsDatabaseAdmin())
                MbushFilterGrid(data.ktheFiltraGridaSipasId(idFiltra));
        }

        /// <summary>
        /// Konstruktori default i klases
        /// </summary>
        public clsFiltraGrida()
        {
        }

        public clsFiltraGrida(DataRow rreshti)
        {
            MbushFilterGrid(rreshti);
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// Ruan objektin e filtrit ne databaze.
        /// </summary>
        /// <returns> nje objekt clsMesazh qe tregon nese ruajtja ka perfunduar me sukses apo jo</returns>
        public clsMesazh ruaj()
        {
            using (var data = new clsDatabaseAdmin())
            {
                int idfiltri;
                var mesazh = data.ruajFiltraGrida(out idfiltri, FiltraKodi, FiltraShenime, FiltraVlera, GridaKokaId,
                    FiltraUniversal, IdPerdoruesi, KoloneRenditje, DrejtimRenditje, IdNdermarje, _idStatusDok);
                IdFiltra = idfiltri;
                return mesazh;
            }
        }

        /// <summary>
        /// Modifikon objektin e filtrit ne databaze.
        /// </summary>
        public clsMesazh modifiko()
        {
            using (var data = new clsDatabaseAdmin())
                return data.modifikoFiltraGrida(IdFiltra, FiltraKodi, FiltraShenime, FiltraVlera, GridaKokaId, FiltraUniversal, IdPerdoruesi, KoloneRenditje, DrejtimRenditje, IdNdermarje, _idStatusDok);
        }

        /// <summary>
        /// Fshin objektin e filtrit nga databaza.
        /// </summary>
        public clsMesazh fshi()
        {
            using (var data = new clsDatabaseAdmin())
                return data.fshiFiltraGridaStatus(IdFiltra, _idPerdoruesi);
        }

        public bool mbushFilterPerGrideSipasKodit(string filtrakodi, int idnderm, int idGridaKoka)
        {
            using (var db = new clsDatabaseAdmin())
                return MbushFilterGrid(db.ktheFilterPerGrideSipasKodit(filtrakodi, idnderm, idGridaKoka));
        }

        public static bool ekzistonFilterSipasKoditPerGride(string kodi, int idNderm, int idKoka)
        {
            using (var db = new clsDatabaseAdmin())
                return db.ekzistonFilterNew(kodi, idNderm, idKoka);
        }

        public static clsFiltraGrida MerrFilterDefault(int idkonfigAmbjenti)
        {
            var kusht = new clsKusht(idkonfigAmbjenti, "FILTER");
            if (kusht.IdKusht == 0 || kusht.Vlera == 0)
                return null;
            return new clsFiltraGrida(kusht.Vlera);
        }

        #endregion

        #region Metoda Internal

        internal bool MbushFilterGrid(DataRow dbDataRowFilterGrid)
        {
            if (dbDataRowFilterGrid != null)
            {
                try
                {
                    int.TryParse(dbDataRowFilterGrid["IDFILITRI"].ToString(), out _idFiltra);
                    _filtraKodi = dbDataRowFilterGrid["FILTRIKODI"].ToString();
                    _filtraShenime = dbDataRowFilterGrid["FILTRISHENIME"].ToString();
                    _filtraVlera = dbDataRowFilterGrid["FILTRIVLERA"].ToString();
                    int.TryParse(dbDataRowFilterGrid["GRIDKOKAID"].ToString(), out _gridaKokaId);
                    bool.TryParse(dbDataRowFilterGrid["FILTRIUNIVERSAL"].ToString(), out _filtraUniversal);
                    int.TryParse(dbDataRowFilterGrid["IDPERDORUESI"].ToString(), out _idPerdoruesi);
                    _koloneRenditje = dbDataRowFilterGrid["KOLONERENDITJE"].ToString();
                    bool.TryParse(dbDataRowFilterGrid["DREJTIMRENDITJE"].ToString(), out _drejtimRenditje);
                    int.TryParse(dbDataRowFilterGrid["IDNDERMARJE"].ToString(), out _idNdermarje);
                    int.TryParse(dbDataRowFilterGrid["IDSTATUSDOK"].ToString(), out _idStatusDok);
                    DateTime.TryParse(dbDataRowFilterGrid["DTKRIJIMI"].ToString(), out _dtKrijimi);
                    DateTime.TryParse(dbDataRowFilterGrid["DTMODIFIKIMI"].ToString(), out _dtModifikimi);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se filtrit te grides nga db-ja");
                }
            }
            return false;
        }

        #endregion
    }
}
