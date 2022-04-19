using System;
using System.Collections.Generic;
using System.Data;

namespace DbCore.DbAdmin
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne cdo kolone qe i perket
    ///  nje konfigurimi te cakutar te grides.(Te dhenat  merren nga tabela : T_GRIDATRUPI)
    /// </summary>
    public class clsGridaTrupi
    {
        #region Atribute

        private int idTrupi;
        private int idKoka;
        private int indexTrupi;
        private bool visibleTrupi;
        private string kodiTrupi;
        private string pershkrimiTrupi;
        private bool readonlyTrupi;
        private int widthTrupi;
        private int indexTrupiOrigjinal;

        private int idKonfigAmbjenteLupa;
        private string kodKonfigLupa;
        private string idKonfigLupaMultiple;
        private string pershkrimiTrupi_en;
        private int tipi;
        private bool shfaqMobile;
        private double renditjaMobile;
        private string gridKokaEmri;
        private int llojFormatFushe;
        private string pershkrimiTrupi_fr;

        /// <summary>
        /// tregon nese fusha do shfaqet ne ambjentin e kostumizimit apo jo
        /// </summary>
        private bool visibleCostumize;

        private DataRow rreshti;

        #endregion Atribute

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsGridaTrupi(int idtrupi, int idkoka, int indextrupi, bool visibletrupi, string koditrupi,
                            string pershkrimitrupi, bool readonlytrupi, int widthtrupi, int indextrupiorigjinal, string idLpMult, int tipi, bool shfaqmobile, double renditjamobile, int llojformatfushe)
        {
            idTrupi = idtrupi;
            idKoka = idkoka;
            indexTrupi = indextrupi;
            visibleTrupi = visibletrupi;
            kodiTrupi = koditrupi;
            pershkrimiTrupi = pershkrimitrupi;
            readonlyTrupi = readonlytrupi;
            widthTrupi = widthtrupi;
            indexTrupiOrigjinal = indextrupiorigjinal;
            idKonfigLupaMultiple = idLpMult;
            Tipi = tipi;
            shfaqMobile = shfaqmobile;
            renditjaMobile = renditjamobile;
            llojFormatFushe = llojformatfushe;
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idTrupi">id e trupit</param>
        public clsGridaTrupi(int idTrupi)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            mbushGridTrupi(data.ktheLupaMultipleSipasGridaTrupi(idTrupi));
            data.Dispose();
        }

        /// <summary>
        /// Konstruktori default i klases
        /// </summary>
        public clsGridaTrupi()
        {
        }

        public clsGridaTrupi(DataRow rreshti)
        {
            mbushGridTrupi(rreshti);
        }

        #endregion Konstruktoret

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdTrupi
        {
            get { return idTrupi; }
            set { idTrupi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e konfigurimit te grides se ciles i perket kjo kolone.
        /// </summary>
        public int IdKoka
        {
            get { return idKoka; }
            set { idKoka = value; }
        }

        /// <summary>
        /// Kthen/Vendos Indexin e kolones, qe perfaqson pozicionin e kolones.
        /// </summary>
        public int IndexTrupi
        {
            get { return indexTrupi; }
            set { indexTrupi = value; }
        }

        /// <summary>
        /// tregon nese fusha do shfaqet ne ambjentin e kostumizimit apo jo
        /// </summary>
        public bool VisibleCostumize
        {
            get
            {
                return visibleCostumize;
            }
            set
            {
                visibleCostumize = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos atributin qe tregon nese kjo kolone do jete e dukshme apo jo.
        /// </summary>
        public bool VisibleTrupi
        {
            get { return visibleTrupi; }
            set { visibleTrupi = value; }
        }

        /// <summary>
        /// Kthen/Vendos kodin e kolones. Duhet te jete e njejte me emrin e atributit perkates te klases
        /// qe perbehet collectioni qe do mbushe griden se ciles i perket kjo kolone.
        /// </summary>
        public String KodiTrupi
        {
            get { return kodiTrupi; }
            set { kodiTrupi = value; }
        }

        /// <summary>
        /// Kthen/Vendos pershkrimin e kolones. Ky pershkrim do i shfaqet perdoruesit.
        /// </summary>
        public String PershkrimiTrupi
        {
            get { return pershkrimiTrupi; }
            set { pershkrimiTrupi = value; }
        }

        /// <summary>
        /// Kthen/Vendos atributin e kolones qe tregon nese kolona do jete e editueshme apo jo.
        /// </summary>
        public bool ReadonlyTrupi
        {
            get { return readonlyTrupi; }
            set { readonlyTrupi = value; }
        }

        /// <summary>
        /// Kthen/Vendos gjeresine e kolones. Kjo vlere eshte ne perqindje.
        /// </summary>
        public int WidthTrupi
        {
            get { return widthTrupi; }
            set { widthTrupi = value; }
        }

        /// <summary>
        /// Kthen/Vendos Indexin e kolones, qe perfaqson pozicionin e kolones.
        /// </summary>
        public int IndexTrupiOrigjinal
        {
            get { return indexTrupiOrigjinal; }
            set { indexTrupiOrigjinal = value; }
        }

        /// <summary>
        /// Kthen/Vendos Id e konfigurimit te lupes (eshte konfiguruar si ambjent)
        /// </summary>
        public int IdKonfigAmbjenteLupa
        {
            get { return idKonfigAmbjenteLupa; }
            set { idKonfigAmbjenteLupa = value; }
        }

        /// <summary>
        /// Kthen/Vendos Indexin e kolones, qe perfaqson pozicionin e kolones.
        /// </summary>
        public String KodLupa
        {
            get { return kodKonfigLupa; }
            set { kodKonfigLupa = value; }
        }

        /// <summary>
        /// Kthen/Vendos id-te e lupave ne rastin kur nje kolone e grides ka disa lloje lupash
        /// formohen si string te ndara nga char -, ne kete rast IdKonfigAmbjenteLupa = 1
        /// </summary>
        public String IdKonfigLupaMultiple
        {
            get { return idKonfigLupaMultiple; }
            set { idKonfigLupaMultiple = value; }
        }

        /// <summary>
        /// Kthen/Vendos pershkriminne anglisht te kolones. Ky pershkrim do i shfaqet perdoruesit.
        /// </summary>
        public string PershkrimiTrupi_en
        {
            get { return pershkrimiTrupi_en; }
            set { pershkrimiTrupi_en = value; }
        }

        public int Tipi
        {
            get { return tipi; }
            set { tipi = value; }
        }
        public bool ShfaqMobile
        {
            get { return shfaqMobile; }
            set { shfaqMobile = value; }
        }

        public double RenditjaMobile
        {
            get { return renditjaMobile; }
            set { renditjaMobile = value; }
        }

        public String GridKokaEmri
        {
            get { return gridKokaEmri; }
            set { gridKokaEmri = value; }
        }
        /// <summary>
        /// ka vlere ne varesi te llojit te formatit qe do perdoret (sasi, cmim, vlere, zbritje)
        /// </summary>
        public int LlojFormatFushe
        {
            get { return llojFormatFushe; }
            set { llojFormatFushe = value; }
        }

        public string PershkrimiTrupi_fr
        {
            get { return pershkrimiTrupi_fr; }
            set { pershkrimiTrupi_fr = value; }
        }
        #endregion Properties

        #region Metoda Publike

        public clsMesazh ruajMultipleLupa(int idKonfig, clsDatabaseAdmin data)
        {
            //if (data == null) data = new clsDatabaseAdmin();
            clsMesazh u_ruajt = data.ruajLupaMultiple(this.IdTrupi, this.IdKonfigAmbjenteLupa, idKonfig);
            return u_ruajt;
        }

        public clsMesazh fshiLupaMultiple(clsDatabaseAdmin data, int idkonfig)
        {
            //if (data == null) data = new clsDatabaseAdmin();
            clsMesazh u_fshi = data.fshiLupaMultiple(idkonfig);
            return u_fshi;
        }

        public clsMesazh ruajTrup(clsDatabaseAdmin db, int idGjuha)
        {
            return db.modifikoGridaTrupi(idTrupi, idKoka, indexTrupi, visibleTrupi, kodiTrupi, pershkrimiTrupi, pershkrimiTrupi_en, pershkrimiTrupi_fr, readonlyTrupi, widthTrupi, indexTrupiOrigjinal, idKonfigAmbjenteLupa, visibleCostumize, idGjuha, tipi, shfaqMobile, renditjaMobile, llojFormatFushe);
        }

        public static int merrKonfigurimLupeSipasKonfigAmbjentiDheKodit(int idKonfigAmbjenti, string kodi)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                return data.merrKonfigurimLupeSipasKonfigAmbjentiDheKodit(idKonfigAmbjenti, kodi);
            }
        }

        #endregion Metoda Publike

        #region Metoda Internal

        /// <summary>
        /// mbush trupin e grides nga databaza
        /// </summary>
        /// <param name="dbDataRowGridTrup">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushGridTrupi(DataRow dbDataRowGridTrup)
        {
            if (dbDataRowGridTrup != null)
            {
                try
                {
                    int.TryParse(dbDataRowGridTrup["GRIDATRUPIID"].ToString(), out idTrupi);
                    int.TryParse(dbDataRowGridTrup["GRIDAKOKAID"].ToString(), out idKoka);
                    int.TryParse(dbDataRowGridTrup["GRIDATRUPIINDEX"].ToString(), out indexTrupi);
                    bool.TryParse(dbDataRowGridTrup["GRIDATRUPIVISIBLE"].ToString(), out visibleTrupi);
                    kodiTrupi = dbDataRowGridTrup["GRIDATRUPIKODI"].ToString();
                    pershkrimiTrupi = dbDataRowGridTrup["GRIDATRUPIPERSHKRIMI"].ToString();
                    pershkrimiTrupi_en = dbDataRowGridTrup["GRIDATRUPIPERSHKRIMI_en"].ToString();
                    pershkrimiTrupi_fr = dbDataRowGridTrup["GRIDATRUPIPERSHKRIMI_fr"].ToString();
                    bool.TryParse(dbDataRowGridTrup["GRIDATRUPIREADONLY"].ToString(), out readonlyTrupi);
                    int.TryParse(dbDataRowGridTrup["GRIDATRUPIWIDTH"].ToString(), out widthTrupi);
                    int.TryParse(dbDataRowGridTrup["GRIDATRUPIINDEXORIGJINAL"].ToString(), out indexTrupiOrigjinal);
                    int.TryParse(dbDataRowGridTrup["IDKONFIGLUPA"].ToString(), out idKonfigAmbjenteLupa);
                    kodKonfigLupa = dbDataRowGridTrup["KODKONFIGLUPA"].ToString();
                    idKonfigLupaMultiple = dbDataRowGridTrup["IDKONFIGLUPA1"].ToString();
                    bool.TryParse(dbDataRowGridTrup["VISIBLECOSTUMIZE"].ToString(), out visibleCostumize);
                    int.TryParse(dbDataRowGridTrup["TIPI"].ToString(), out tipi);
                    bool.TryParse(dbDataRowGridTrup["SHFAQMOBILE"].ToString(), out shfaqMobile);
                    Double.TryParse(dbDataRowGridTrup["RENDITJAMOBILE"].ToString(), out renditjaMobile);
                    gridKokaEmri = dbDataRowGridTrup["GRIDKOKAEMRI"].ToString();
                    int.TryParse(dbDataRowGridTrup["LLOJFORMATFUSHE"].ToString(), out llojFormatFushe);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se trupit te grides nga db-ja");
                }
            }
            else
                return false;
        }

        /// <summary>
        /// mbush trupin e grides bashke me pershkrimin anglisht nga databaza
        /// </summary>
        /// <param name="dbDataRowGridTrup">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushGridTrupiEng(DataRow dbDataRowGridTrup)
        {
            if (dbDataRowGridTrup != null)
            {
                try
                {
                    int.TryParse(dbDataRowGridTrup["GRIDATRUPIID"].ToString(), out idTrupi);
                    int.TryParse(dbDataRowGridTrup["GRIDAKOKAID"].ToString(), out idKoka);
                    int.TryParse(dbDataRowGridTrup["GRIDATRUPIINDEX"].ToString(), out indexTrupi);
                    bool.TryParse(dbDataRowGridTrup["GRIDATRUPIVISIBLE"].ToString(), out visibleTrupi);
                    kodiTrupi = dbDataRowGridTrup["GRIDATRUPIKODI"].ToString();
                    pershkrimiTrupi = dbDataRowGridTrup["GRIDATRUPIPERSHKRIMI"].ToString();
                    bool.TryParse(dbDataRowGridTrup["GRIDATRUPIREADONLY"].ToString(), out readonlyTrupi);
                    int.TryParse(dbDataRowGridTrup["GRIDATRUPIWIDTH"].ToString(), out widthTrupi);
                    int.TryParse(dbDataRowGridTrup["GRIDATRUPIINDEXORIGJINAL"].ToString(), out indexTrupiOrigjinal);
                    int.TryParse(dbDataRowGridTrup["IDKONFIGLUPA"].ToString(), out idKonfigAmbjenteLupa);
                    kodKonfigLupa = dbDataRowGridTrup["KODKONFIGLUPA"].ToString();
                    idKonfigLupaMultiple = dbDataRowGridTrup["IDKONFIGLUPA1"].ToString();
                    bool.TryParse(dbDataRowGridTrup["VISIBLECOSTUMIZE"].ToString(), out visibleCostumize);
                    int.TryParse(dbDataRowGridTrup["TIPI"].ToString(), out tipi);
                    if (dbDataRowGridTrup["GRIDATRUPIPERSHKRIMI_en"] != null)
                        pershkrimiTrupi_en = dbDataRowGridTrup["GRIDATRUPIPERSHKRIMI_en"].ToString();
                    if (dbDataRowGridTrup["GRIDATRUPIPERSHKRIMI_fr"] != null)
                        pershkrimiTrupi_fr = dbDataRowGridTrup["GRIDATRUPIPERSHKRIMI_fr"].ToString();
                    bool.TryParse(dbDataRowGridTrup["SHFAQMOBILE"].ToString(), out shfaqMobile);
                    Double.TryParse(dbDataRowGridTrup["RENDITJAMOBILE"].ToString(), out renditjaMobile);
                    gridKokaEmri = dbDataRowGridTrup["GRIDKOKAEMRI"].ToString();
                    int.TryParse(dbDataRowGridTrup["LLOJFORMATFUSHE"].ToString(), out llojFormatFushe);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se trupit te grides nga db-ja");
                }
            }
            else
                return false;
        }

        /// <summary>
        /// ky funksion mbush kete objekt,duke u kaluar si parameter tek metoda e cila do bej exe e sp ne DB
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        internal static clsGridaTrupi Create(IDataRecord record)
        {
            return new clsGridaTrupi
            {
                idTrupi = !Convert.IsDBNull(record["GRIDATRUPIID"]) ? Convert.ToInt32(record["GRIDATRUPIID"]) : 0,
                idKoka = !Convert.IsDBNull(record["GRIDAKOKAID"]) ? Convert.ToInt32(record["GRIDAKOKAID"]) : 0,
                indexTrupi = !Convert.IsDBNull(record["GRIDATRUPIINDEX"]) ? Convert.ToInt32(record["GRIDATRUPIINDEX"]) : 0,
                visibleTrupi = !Convert.IsDBNull(record["GRIDATRUPIVISIBLE"]) ? Convert.ToBoolean(record["GRIDATRUPIVISIBLE"]) : false,
                kodiTrupi = !Convert.IsDBNull(record["GRIDATRUPIKODI"]) ? Convert.ToString(record["GRIDATRUPIKODI"]) : null,
                pershkrimiTrupi = !Convert.IsDBNull(record["GRIDATRUPIPERSHKRIMI"]) ? record["GRIDATRUPIPERSHKRIMI"].ToString() : null,
                readonlyTrupi = !Convert.IsDBNull(record["GRIDATRUPIREADONLY"]) ? Convert.ToBoolean(record["GRIDATRUPIREADONLY"]) : false,
                widthTrupi = !Convert.IsDBNull(record["GRIDATRUPIWIDTH"]) ? Convert.ToInt32(record["GRIDATRUPIWIDTH"]) : 0,
                indexTrupiOrigjinal = !Convert.IsDBNull(record["GRIDATRUPIINDEXORIGJINAL"]) ? Convert.ToInt32(record["GRIDATRUPIINDEXORIGJINAL"]) : 0,
                IdKonfigAmbjenteLupa = !Convert.IsDBNull(record["IDKONFIGLUPA"]) ? Convert.ToInt32(record["IDKONFIGLUPA"]) : 0,
                kodKonfigLupa = !Convert.IsDBNull(record["KODKONFIGLUPA"]) ? record["KODKONFIGLUPA"].ToString() : null,
                idKonfigLupaMultiple = !Convert.IsDBNull(record["IDKONFIGLUPA1"]) ? Convert.ToString(record["IDKONFIGLUPA1"]) : null,
                visibleCostumize = !Convert.IsDBNull(record["VISIBLECOSTUMIZE"]) ? Convert.ToBoolean(record["VISIBLECOSTUMIZE"]) : false,
                tipi = !Convert.IsDBNull(record["TIPI"]) ? Convert.ToInt32(record["TIPI"]) : 0,
                shfaqMobile = !Convert.IsDBNull(record["SHFAQMOBILE"]) ? Convert.ToBoolean(record["SHFAQMOBILE"]) : false,
                renditjaMobile = !Convert.IsDBNull(record["RENDITJAMOBILE"]) ? Convert.ToInt32(record["RENDITJAMOBILE"]) : 0,
                gridKokaEmri = !Convert.IsDBNull(record["GRIDKOKAEMRI"]) ? Convert.ToString(record["GRIDKOKAEMRI"]) : null,
                llojFormatFushe = !Convert.IsDBNull(record["LLOJFORMATFUSHE"]) ? Convert.ToInt32(record["LLOJFORMATFUSHE"]) : 0,
            };
        }

        #endregion Metoda Internal
    }
}