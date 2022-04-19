using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Newtonsoft.Json;
using DbCore.IMBUtils;
using DbCore.IMBUtils.Types;

namespace DbCore.DbListPagesat
{
    /// <summary>
    /// Kjo eshte klasa qe sherben per objektet qe perfaqesojne nje komponente te list pageses se punonjesit
    ///  (Te dhenat  merren nga tabela : T_KOMPLISTPAGESE)
    /// </summary>
    public class clsKompListPagese
    {

        #region Atribute

        private int idKompListPagese;
        private int idTrupi;
        private int idKomponentePage;
        private decimal vleraParam;
        private decimal vlera;
        private string kodKomponente;
        private string komponente;
        private int njesia;
        private string emerparam;
        private int tipi;
        private colKomponenteMuaji ocolKomponenteMuaji;
        private bool lejoModVlere;
        private bool eDetyrueshme;
        private string grupi;
        private string import;
        private string shenime;
        private bool modifikuar;
        private string niveli1;
        private string niveli2;
        private int idRenditje1;
        private int idRenditje2;
        private DateTime data;
        private DataRow rreshti;

        #endregion

        #region Konstruktor

        /// <summary>
        /// Konstruktor i klases
        /// </summary>
        /// <param name="idkomplistpagese">Id e komponente list pagese</param>
        /// <param name="idtrupi">Id e trupi</param>
        /// <param name="idkomponente">Id e komponentes</param>
        /// <param name="vleraparam">vlera e parametrit</param>
        /// <param name="vlera"> vlera</param>
        public clsKompListPagese(int idkomplistpagese, int idtrupi, int idkomponente, decimal vleraparam, decimal vlera, string shenime, bool modifikuar)
        {
            idKompListPagese = idkomplistpagese;
            idTrupi = idtrupi;
            idKomponentePage = idkomponente;
            vleraParam = vleraparam;
            this.vlera = vlera;
            this.shenime = shenime;
            this.modifikuar = modifikuar;
        }

        /// <summary>
        /// Konstruktor i klases
        /// </summary>
        public clsKompListPagese()
        {
        }

        /// <summary>
        /// konstruktori me 1 parametra
        /// </summary>
        /// <param name="idkomplistpagese">id komp list pagesa</param>
        /// 
        public clsKompListPagese(int idKompListPagese, int idTrupi, int idKomponentePage, decimal vleraParam, decimal vlera, string kodKomponente, string komponente, int njesia, string emerparam, int tipi, colKomponenteMuaji ocolKomponenteMuaji, bool lejoModVlere, bool eDetyrueshme, string grupi, string import, string shenime, bool modifikuar, string niveli1, string niveli2, int idRenditje1, int idRenditje2, DateTime data, clsKomponentePage komponentePage)
        {
            this.idKompListPagese = idKompListPagese;
            this.idTrupi = idTrupi;
            this.idKomponentePage = idKomponentePage;
            this.vleraParam = vleraParam;
            this.vlera = vlera;
            this.kodKomponente = kodKomponente;
            this.komponente = komponente;
            this.njesia = njesia;
            this.emerparam = emerparam;
            this.tipi = tipi;
            this.ocolKomponenteMuaji = ocolKomponenteMuaji;
            this.lejoModVlere = lejoModVlere;
            this.eDetyrueshme = eDetyrueshme;
            this.grupi = grupi;
            this.import = import;
            this.shenime = shenime;
            this.modifikuar = modifikuar;
            this.niveli1 = niveli1;
            this.niveli2 = niveli2;
            this.idRenditje1 = idRenditje1;
            this.idRenditje2 = idRenditje2;
            this.data = data;
            this.komponentePage = komponentePage;
        }
        public clsKompListPagese(int idkomplistpagese)
        {
            using (clsDatabazeListPagesa db = new clsDatabazeListPagesa())
            {
                db.ktheKompListPagese(idkomplistpagese, this);
            }
        }
        #endregion

        #region Properties

        public bool EDetyrueshme
        {
            get
            {
                return eDetyrueshme;
            }
            set { eDetyrueshme = value; }
        }
        [JsonIgnore]
        public string Grupi
        {
            get
            {
                return grupi;
            }
            set
            {
                grupi = value;
            }
        }
        public int IdKompListPagese
        {
            get { return idKompListPagese; }
            set { idKompListPagese = value; }
        }
        [JsonIgnore]
        public int IdRenditje1
        {
            get
            {
                return idRenditje1;
            }
            set
            {
                idRenditje1 = value;
            }
        }
        [JsonIgnore]
        public int IdRenditje2
        {
            get
            {
                return idRenditje2;
            }
            set
            {
                idRenditje2 = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e trupit
        /// </summary>
        public int IdTrupi
        {
            get { return idTrupi; }
            set { idTrupi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e komponentes se pages
        /// </summary>
        public int IdKomponentePage
        {
            get
            {
                return idKomponentePage;
            }
            set
            {
                idKomponentePage = value;
            }
        }

        [JsonIgnore]
        public string Import
        {
            get
            {
                return import;
            }
            set { import = value; }
        }
        public bool LejoModVlere
        {
            get
            {
                return lejoModVlere;
            }
            set
            {
                lejoModVlere = value;
            }

        }
        public bool Modifikuar
        {
            get
            {
                return modifikuar;
            }
            set
            {
                modifikuar = value;
            }
        }
        [JsonIgnore]
        public string Niveli1
        {
            get
            {
                return niveli1;
            }
            set
            {
                niveli1 = value;
            }
        }
        [JsonIgnore]
        public string Niveli2
        {
            get
            {
                return niveli2;
            }
            set
            {
                niveli2 = value;
            }
        }
        [JsonIgnore]
        public colKomponenteMuaji OcolKomponenteMuaji
        {
            get
            {
                return ocolKomponenteMuaji;
            }
            set
            {
                ocolKomponenteMuaji = value;
            }
        }
        public string Shenime
        {
            get
            {
                return shenime;
            }
            set
            {
                shenime = value;
            }
        }
        /// <summary>
        /// kthen/vendos vleren e parametrit
        /// </summary>
        public decimal VleraParam
        {
            get
            {
                return vleraParam;
            }
            set
            {
                vleraParam = value;
            }
        }

        /// <summary>
        /// kthen/vendos vleren
        /// </summary>
        public decimal Vlera
        {
            get
            {
                return vlera;
            }
            set
            {
                vlera = value;
            }
        }

        /// <summary>
        /// kthen kodin e komponentes
        /// </summary>
        public string KodKomponente
        {
            get
            {
                return kodKomponente;
            }
            set
            {
                kodKomponente = value;
            }
        }

        /// <summary>
        /// emri i komponentes
        /// </summary>
        public string Komponente
        {
            get
            {
                return komponente;
            }
        }

        /// <summary>
        /// njesia
        /// </summary>
        public int Njesia
        {
            get
            {
                return njesia;
            }
        }

        /// <summary>
        /// emer parametri
        /// </summary>
        public string Emerparam
        {
            get
            {
                return emerparam;
            }
        }

        /// <summary>
        /// tipi i komponentes
        /// </summary>
        public int Tipi
        {
            get { return tipi; }
            set { tipi = value; }
        }

        [JsonIgnore]
        public DateTime Data
        {
            get
            {
                return data.Date;
            }

            set
            {
                data = value;
            }
        }
        [JsonIgnore]
        public clsKomponentePage komponentePage { get; set; }
        #endregion

        #region Metoda Publike

        public static IEnumerable<clsKompListPagese> MerrKompListPagese(List<int> idTrupashLp)
        {
            return new clsDatabazeListPagesa().MerrKomponenteListPageseSipasTrupave(idTrupashLp);
        }
        public static clsKompListPagese Krijo(IDataRecord record)
        {
            var clsKompListPagese = new clsKompListPagese();
            clsKompListPagese.mbushKomponenteListPagese(record);
            return clsKompListPagese;
        }
        public static clsKompListPagese KrijoBasic(IDataRecord record)
        {
            var clsKompListPagese = new clsKompListPagese();
            clsKompListPagese.mbushKomponenteListPageseBasic(record);
            return clsKompListPagese;
        }
        /// <summary>
        /// Ruan objektin e paga shtesa ne tabelen perkatese ne databaze.Therret funksionin
        /// <see cref="clsDatabazeListPagesa.ruajPagaShtesa"/>
        /// </summary>
        /// <returns>Kthen true nese ruajtja perfundoi me sukses</returns>
        public bool ruaj()
        {
            clsDatabazeListPagesa data = new clsDatabazeListPagesa();
            int id;
            clsMesazh u_ruajt = data.ruajKompListPagesa(out id, idTrupi, idKomponentePage, vleraParam, vlera, shenime, modifikuar);
            data.Dispose();
            return u_ruajt.Status;

        }

        /// <summary>
        /// Modifikon objektin e paga shtesa ne tabelen perkatese ne databaze.Therret funksionin
        /// <see cref="clsDatabazeListPagesa.modifikoPagaShtesa"/>
        /// </summary>
        /// <returns>Kthen true nese modifikimi perfundoi me sukses</returns>
        public bool modifiko()
        {
            clsDatabazeListPagesa data = new clsDatabazeListPagesa();
            clsMesazh u_modifikua = data.modifikoKompListPagese(idKompListPagese, idTrupi, idKomponentePage, vleraParam, vlera, shenime, modifikuar);
            data.Dispose();
            return u_modifikua.Status;
        }

        /// <summary>
        /// Fshin objektin e paga shtesa ne tabelen perkatese ne databaze.Therret funksionin
        /// <see cref="clsDatabazeListPagesa.fshiKomponenteListPagese"/>
        /// </summary>
        /// <returns>Kthen true nese fshirja perfundoi me sukses</returns>
        public bool fshi()
        {
            clsDatabazeListPagesa data = new clsDatabazeListPagesa();
            clsMesazh u_fshi = data.fshiKompListPagese(idKompListPagese);
            data.Dispose();
            return u_fshi.Status;
        }

        /// <summary>
        /// merr PagaShtesan sipas id
        /// </summary>
        public void merr()
        {
            using (clsDatabazeListPagesa data = new clsDatabazeListPagesa())
            {
                data.ktheKompListPagese(idKompListPagese, this);
            }
        }

        /// <summary>
        /// merr ditet e harxhuara te lejes
        /// </summary>
        /// <param name="idpunonjesi"></param>
        /// <param name="data"></param>
        /// <param name="idnderviti"></param>
        /// <returns></returns>
        public static decimal merrDiteTeHarxhuara(int idpunonjesi, DateTime data, int idnderviti)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            decimal dec = db.ktheKompListPageseDiteTeHarxhuara(idpunonjesi, data, idnderviti);
            db.Dispose();
            return dec;
        }
        public static Dictionary<int, decimal> merrDiteTeHarxhuara(List<int> idpunonjesish, DateTime data, int idnderviti)
        {
            using (clsDatabazeListPagesa db = new clsDatabazeListPagesa())
            {
                return db.ktheKompListPageseDiteTeHarxhuara(idpunonjesish, data, idnderviti);
            }
        }


        public clsKompListPagese Clone()
        {
            return new clsKompListPagese(idKompListPagese, idTrupi, IdKomponentePage, vleraParam, vlera, kodKomponente, komponente, njesia, emerparam, tipi, ocolKomponenteMuaji == null ? null : ocolKomponenteMuaji.Clone(), lejoModVlere, eDetyrueshme, grupi, import, shenime, modifikuar, niveli1, niveli2, idRenditje1, idRenditje2, data, komponentePage == null ? null : komponentePage.Clone());
        }

        #endregion

        #region Metoda Internal
        /// <summary>
        /// kjo metode perdoret per mbushjen e collectionit qe perdoret per llogaritjen e listpagesave
        /// </summary>
        /// <param name="dbDataRowKomponenteListPagese"></param>
        internal void mbushKomponenteListPageseBasic(IDataRecord dbDataRowKomponenteListPagese, bool kaDate = false)
        {
            Converter.ParseExact(dbDataRowKomponenteListPagese["ID"].ToString(), out idKompListPagese, "idKompListPagese");
            Converter.Parse(dbDataRowKomponenteListPagese["IDTRUPI"].ToString(), out idTrupi, "idTrupi");
            Converter.ParseExact(dbDataRowKomponenteListPagese["IDKOMPONENTE"].ToString(), out idKomponentePage, "idKomponente");
            Converter.Parse(dbDataRowKomponenteListPagese["VLERAPARAM"].ToString(), out vleraParam, "vleraParam");
            Converter.Parse(dbDataRowKomponenteListPagese["VLERA"].ToString(), out vlera, "vlera");
            bool.TryParse(dbDataRowKomponenteListPagese["EDetyrueshme"].ToString(), out eDetyrueshme);
            kodKomponente = dbDataRowKomponenteListPagese["KodKomponente"].ToString();
            emerparam = dbDataRowKomponenteListPagese["EmerParam"].ToString();
            if (kaDate) DateTime.TryParse(dbDataRowKomponenteListPagese["Data"].ToString(), out data);
        }
        /// <summary>
        /// mbush skema sigurimi nga databaza
        /// </summary>
        /// <param name="dbDataRowKomponenteListPagese">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal void mbushKomponenteListPagese(IDataRecord dbDataRowKomponenteListPagese)
        {
            try
            {
                mbushKomponenteListPageseBasic(dbDataRowKomponenteListPagese);
                Converter.Parse(dbDataRowKomponenteListPagese["Modifikuar"].ToString(), out modifikuar, "modifikuar");
                Converter.Parse(dbDataRowKomponenteListPagese["LejoModVlere"].ToString(), out lejoModVlere, "lejoModVlere");
                komponente = dbDataRowKomponenteListPagese["Komponente"].ToString();
                kodKomponente = dbDataRowKomponenteListPagese["KodKomponente"].ToString();
                Converter.Parse(dbDataRowKomponenteListPagese["Njesia"].ToString(), out njesia, "njesia");
                emerparam = dbDataRowKomponenteListPagese["EmerParam"].ToString();
                grupi = dbDataRowKomponenteListPagese["Grupi"].ToString();
                niveli1 = dbDataRowKomponenteListPagese["Niveli1"].ToString();
                niveli2 = dbDataRowKomponenteListPagese["Niveli2"].ToString();
                import = dbDataRowKomponenteListPagese["Import"].ToString();
                shenime = dbDataRowKomponenteListPagese["Shenime"].ToString();
                Converter.Parse(dbDataRowKomponenteListPagese["Tipi"].ToString(), out tipi, "tipi");
                Converter.Parse(dbDataRowKomponenteListPagese["IdRenditje1"].ToString(), out idRenditje1, "idRenditje1");
                Converter.Parse(dbDataRowKomponenteListPagese["IdRenditje2"].ToString(), out idRenditje2, "idRenditje2");
            }
            catch (MyException myex)
            {
                throw new MyException("gabim ne mbushjen e komponenteve te listpageses per :{0} {1}", idKompListPagese, myex.Message);
            }

        }

        internal static clsKompListPagese KrijoBasicMeDate(IDataRecord arg)
        {
            var clsKompListPagese = new clsKompListPagese();
            clsKompListPagese.mbushKomponenteListPageseBasic(arg, true);
            return clsKompListPagese;
        }
        #endregion
    }
}

