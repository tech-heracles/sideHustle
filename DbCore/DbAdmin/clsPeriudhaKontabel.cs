using System;
using System.Data;
using System.Globalization;
using DbCore.IMBUtils.Logging;

namespace DbCore.DbAdmin
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne nje periudhe kontabel.
    ///  (Te dhenat  merren nga tabela : T_PERIUDHAT)
    /// </summary>
    public class clsPeriudhaKontabel
    {

        #region Atributet

        private int _idPeriudha;
        private int _idViti;
        private string _kodiViti;
        private int _nrPeriudha;
        private DateTime _fillimiPeriudha;
        private DateTime _mbarimiPeriudha;
        private bool _ekycur;
        private string _emerPeriudha;
        private bool _postoepayslip;
        private bool _postomemobonus;
        private bool _postoannualdeclaration;
        public static string PeriudheFillestare = "Periudha fillestare";
        public static string PeriudheMbyllje = "Periudha e mbylljes";
        public static string PeriudheFillestareEng = "Beginning period";
        public static string PeriudheMbylljeEng = "Ending period";
        private DataRow rreshti;

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori default i klases
        /// </summary>
        public clsPeriudhaKontabel()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idPeriudha"></param>

        public clsPeriudhaKontabel(int idPeriudha, int idgjuha)
        {
            using (var adminDb = new clsDatabaseAdmin())
                MbushPeriudhaKontabel(adminDb.kthePeriudhaSipasId(idPeriudha, idgjuha));
        }

        public clsPeriudhaKontabel(int idPeriudha, CultureInfo ci)
        {
            using (var adminDb = new clsDatabaseAdmin())
                MbushPeriudhaKontabel(adminDb.kthePeriudhaSipasId(idPeriudha, ci.Name == "sq-AL" ? 0 : 1));
        }

        public clsPeriudhaKontabel(int idPeriudha, int idNdermarrje, clsDatabaseAdmin adminDb)
        {
            MbushPeriudhaKontabel(adminDb.TransCache.getPeriudhaKontabelById(idPeriudha, idNdermarrje, adminDb));
        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="data"></param>
        public clsPeriudhaKontabel(DateTime dateTime, int idNdermarrje, clsDatabaseAdmin data)
        {
            MbushPeriudhaKontabel(data.TransCache.getPeriudhaKontabel(dateTime, idNdermarrje, data));
        }

        public clsPeriudhaKontabel(DateTime dateTime, int idNdermarrje)
        {
            using (var data = new clsDatabaseAdmin())
                MbushPeriudhaKontabel(data.kthePeriudhen(dateTime, idNdermarrje));
        }

        public clsPeriudhaKontabel(int idViti, int nrPeriudha, int gjuha)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                MbushPeriudhaKontabel(data.kthePeriudhaSipasVititDheMuajit(idViti, nrPeriudha, gjuha));
            }
        }

        public clsPeriudhaKontabel(DataRow rreshti)
        {

            MbushPeriudhaKontabel(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne e vitit se cilit i perket kjo periudhe kontabel.
        /// </summary>
        public int IdViti
        {
            get { return _idViti; }
            set { _idViti = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdPeriudha
        {
            get { return _idPeriudha; }
            set { _idPeriudha = value; }
        }
        /// <summary>
        /// Kthen/Vendos kodin e vitit se cilit i perket kjo periudhe kontabel.
        /// </summary>
        public String KodiViti
        {
            get { return _kodiViti; }
            set { _kodiViti = value; }
        }

        /// <summary>
        /// Kthen/Vendos daten ne te cilen fillon kjo periudhe kontabel.
        /// </summary>
        public DateTime FillimiPeriudha
        {
            get { return _fillimiPeriudha; }
            set { _fillimiPeriudha = value; }
        }

        /// <summary>
        /// Kthen/Vendos daten ne te cilen mbaron kjo periudhe kontabel.
        /// </summary>
        public DateTime MbarimiPeriudha
        {
            get { return _mbarimiPeriudha; }
            set { _mbarimiPeriudha = value; }
        }

        /// <summary>
        /// Kthen/Vendos vleren numerike qe perfaqson kete periudhe kontabel. Kur periudha eshte
        /// mujore tregon muajin, psh 1-Janar, 2- Shkurt etj...
        /// </summary>
        public int NrPeriudha
        {
            get { return _nrPeriudha; }
            set { _nrPeriudha = value; }
        }

        /// <summary>
        /// Kthen/Vendos kycjen e periudhes
        /// </summary>
        public bool Ekycur
        {
            get { return _ekycur; }
            set { _ekycur = value; }
        }

        /// <summary>
        /// Kthen/Vendos emrin e periudhes.
        /// </summary>
        public String EmerPeriudha
        {
            get { return _emerPeriudha; }
            set { _emerPeriudha = value; }
        }

        public bool PostoEpayslip
        {
            get { return _postoepayslip; }
            set { _postoepayslip = value; }

        }

        public bool PostoMemoBonus
        {
            get { return _postomemobonus; }
            set { _postomemobonus = value; }

        }

        public bool PostoAnnualDeclaration
        {
            get { return _postoannualdeclaration; }
            set { _postoannualdeclaration = value; }

        }

        #endregion

        #region Metoda Publike

        public clsMesazh modifikokycurperiudha(int idperiudha, bool eKycur, System.Resources.ResourceManager rm, CultureInfo ci)
        {
            using (var db = new clsDatabaseAdmin())
                return db.modifikoKycurPeriudha(idperiudha, eKycur, rm, ci);
        }

        public clsMesazh modifikoPeriudha(clsDatabaseAdmin db)
        {
            return db.modifikoPeriudha(_idPeriudha, _ekycur, _nrPeriudha, _fillimiPeriudha, _mbarimiPeriudha, _emerPeriudha, PostoEpayslip, PostoMemoBonus, PostoAnnualDeclaration);
        }

        public static int ktheIdPeriudheSipasDatesDheNdermarrjes(DateTime dateTime, int idNdermarrje)
        {
            ImbLogger.LogTraceShitje("Kthimi i periudhes sipas dates: " + Convert.ToString(dateTime) + " dhe ndermarrjes: " + Convert.ToString(idNdermarrje));
            using (var data = new clsDatabaseAdmin())
            {
                return data.ktheIdPeriudheSipasDatesDheNdermarrjes(dateTime, idNdermarrje);
            }
        }

        public static bool eshteKycurPeriudheSipasDateDheNdermarrjes(DateTime dateTime, int idNdermarrje)
        {
            using (var kycur = new clsDatabaseAdmin())
            {
                return kycur.eshteKycurPeriudheSipasDateDheNdermarrjes(dateTime, idNdermarrje);
            }
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// Ben mbushjen e atributeve me vlert e marra nga databaza, qe i kalohen si parametra
        /// </summary>
        /// <param name="dbDataRowPeriudhaKontabel">Si parametere merr DataRow</param>
        /// <returns>kthen true nese ndodh mbushja e atributeve pa error</returns>
        internal bool MbushPeriudhaKontabel(DataRow dbDataRowPeriudhaKontabel)
        {
            if (dbDataRowPeriudhaKontabel != null)
            {

                try
                {
                    int.TryParse(dbDataRowPeriudhaKontabel["IDPERIUDHA"].ToString(), out _idPeriudha);
                    int.TryParse(dbDataRowPeriudhaKontabel["IDVITI"].ToString(), out _idViti);
                    int.TryParse(dbDataRowPeriudhaKontabel["NUMRIPERIUDHA"].ToString(), out _nrPeriudha);
                    if (!DateTime.TryParse(dbDataRowPeriudhaKontabel["DATAFILLIMIT"].ToString(), out _fillimiPeriudha))
                        throw new Exception();
                    if (!DateTime.TryParse(dbDataRowPeriudhaKontabel["DATAMBARIMIT"].ToString(), out _mbarimiPeriudha))
                        throw new Exception();
                    bool.TryParse(dbDataRowPeriudhaKontabel["EKYCUR"].ToString(), out _ekycur);
                    bool.TryParse(dbDataRowPeriudhaKontabel["postoepayslip"].ToString(), out _postoepayslip);
                    bool.TryParse(dbDataRowPeriudhaKontabel["postomemobonus"].ToString(), out _postomemobonus);
                    bool.TryParse(dbDataRowPeriudhaKontabel["postoannualdeclaration"].ToString(), out _postoannualdeclaration);
                    _emerPeriudha = dbDataRowPeriudhaKontabel["EmerPeriudha"].ToString();
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se periudhes kontabel nga db-ja");
                }
            }
            else
                return false;
        }

        internal void MbushPeriudhaKontabel(clsPeriudhaKontabel periudhaKontabel)
        {
            _idPeriudha = periudhaKontabel.IdPeriudha;
            _idViti = periudhaKontabel.IdViti;
            _nrPeriudha = periudhaKontabel.NrPeriudha;
            _fillimiPeriudha = periudhaKontabel.FillimiPeriudha;
            _mbarimiPeriudha = periudhaKontabel.MbarimiPeriudha;
            _ekycur = periudhaKontabel.Ekycur;
            _postoepayslip = periudhaKontabel.PostoEpayslip;
            _postomemobonus = periudhaKontabel.PostoMemoBonus;
            _postoannualdeclaration = periudhaKontabel.PostoAnnualDeclaration;
            _emerPeriudha = periudhaKontabel.EmerPeriudha;
        }

        #endregion

    }
}
