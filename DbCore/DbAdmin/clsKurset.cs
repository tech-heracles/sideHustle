using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DbCore.IMBUtils.Logging;

namespace DbCore.DbAdmin
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne kursin qe i caktohet nje monedhe te caktuar.
    ///  (Te dhenat  merren nga tabela : T_KURSET)
    /// </summary>
    public class clsKurset
    {
        #region Atributet

        private int idKursi;
        private int llojKursi;
        private double vleraKursi;
        private DateTime dataKursit;
        private int idMonedha;
        private int njesiaKursit;
        private string pershkrimLlojKursi;

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsKurset(int idkursi, int llojkursi, DateTime datakursit, double vlerakursi, int idmonedha, int njesia, string pershkllojkursi)
        {
            idKursi = idkursi;
            llojKursi = llojkursi;
            vleraKursi = vlerakursi;
            dataKursit = datakursit;
            idMonedha = idmonedha;
            njesiaKursit = njesia;
            pershkrimLlojKursi = pershkllojkursi;
        }

        /// <summary>
        /// konstruktor me 2 parametra nje int dhe nje datetime
        /// </summary>
        /// <param name="id">id e monedhes</param>
        /// <param name="date">data</param>
        public clsKurset(int id, DateTime date)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                mbushKurs(data.ktheKursinSipasMonedhesAndDates(id, date));
            }
        }

        /// <summary>
        /// konstruktor me 4 parametra 3 int dhe nje datetime 
        /// </summary>
        /// <param name="id">id e monedhes</param>
        /// <param name="date">data</param>
        /// <param name="Idkonfig">id e konfigurimit te dokumentit</param>
        public clsKurset(int idMon, DateTime date, int idKonfigurim, int idndermarrje)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                mbushKurs(data.ktheKursinSipasMonedhDatesAndIdkonfigurim(idMon, date, idKonfigurim, idndermarrje));
            }
        }

        /// <summary>
        /// konstruktor me 2 parametra nje int dhe nje datetime
        /// </summary>
        /// <param name="id">id e monedhes</param>
        /// <param name="date">data</param>
        public clsKurset(int id, DateTime date, int llojKursi)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                mbushKurs(data.ktheKursSipasMonedhesAndDatesDheLlojit(id, date, llojKursi));
            }
        }
               
        public clsKurset(int id, DateTime date, int llojKursi, clsDatabaseAdmin data)
        {
            mbushKurs(data.ktheKursSipasMonedhesAndDatesDheLlojit(id, date, llojKursi));
        }

        /// <summary>
        /// konstruktor me 2 parametra nje int dhe nje datetime
        /// </summary>
        /// <param name="idMonedha">id e monedhes</param>
        /// <param name="date">data</param>
        public clsKurset(int idMonedha, DateTime date, clsDatabaseAdmin data)
        {
            mbushKurs(data.ktheKursinSipasMonedhesAndDates(idMonedha, date));
        }

        public static clsKurset getKursSipasIdMonedhaFromCache(int idMonedha, DateTime date, clsDatabaseAdmin data)
        {
            return data.TransCache.ktheKursinSipasMonedhesAndDates(idMonedha, date, data);
        }

        public static clsKurset getKursSipasIdMonedhaDheLlojiFromCache(int idMonedha, DateTime date, int lloji, clsDatabaseAdmin data)
        {
            return data.TransCache.ktheKursinSipasMonedhesAndDatesAndLloji(idMonedha, date, lloji, data);
        }

        /// <summary>
        /// konstruktor me 3 parametra nje string dhe nje date
        /// </summary>
        /// <param name="kodi">kodi i monedhes</param>
        /// <param name="date">data</param>
        /// <param name="idndermarje">id e ndermarrjes</param>
        public clsKurset(string kodi, string date, int idndermarje)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                mbushKurs(data.ktheKursetSipasKoditMonedhesAndDates(kodi, date, idndermarje));
            }
        }

        /// <summary>
        /// Konstruktori default i klases
        /// </summary>
        public clsKurset()
        {
        }

        public clsKurset(DataRow rreshti)
        {
            
            mbushKurs(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdKursi
        {
            get { return idKursi; }
            set { idKursi = value; }
        }

        /// <summary>
        /// Kthen/Vendos Llojin e kursit, nese eshte Kursi1,Kursi2  etj...
        /// </summary>
        public int LlojKursi
        {
            get { return llojKursi; }
            set { llojKursi = value; }
        }

        /// <summary>
        /// Kthen/Vendos vleren e kursit per kete objekt.
        /// </summary>
        public double VleraKursi
        {
            get { return vleraKursi; }
            set { vleraKursi = value; }
        }

        /// <summary>
        /// Kthen/Vendos Daten e kursit per kete objekt.
        /// </summary>
        public DateTime DataKursit
        {
            get { return dataKursit; }
            set { dataKursit = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e monedhes se ciles i perket ky kurs.
        /// </summary>
        public int IdMonedha
        {
            get { return idMonedha; }
            set { idMonedha = value; }
        }

        /// <summary>
        /// Kthen/Vendos njesine e kursit, te ciles i perket vlera e caktuar.
        /// </summary>
        public int NjesiaKursit
        {
            get { return njesiaKursit; }
            set { njesiaKursit = value; }
        }
        /// <summary>
        /// Kthen/Vendos pershkrimin e llojit te kursit
        /// </summary>
        public string PershkrimLlojKursi
        {
            get
            {
                return pershkrimLlojKursi;
            }
            set { pershkrimLlojKursi = value; }
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// Kontrollon nese ekziston kursi apo jo
        /// </summary>
        /// <param name="llojkursi">lloji i kursit</param>
        /// <param name="datakursit">data e kursit</param>
        /// <param name="vlerakursi">vlera e kursit</param>
        /// <param name="idmonedha">monedha e kursit</param>
        /// <returns>Kthen true nese ekziston, false perndyshe</returns>
        public static bool ekzistonKursi(int llojkursi, DateTime datakursit, double vlerakursi, int idmonedha)
        {
            try
            {
                using (clsDatabaseAdmin data = new clsDatabaseAdmin())
                {
                    return data.ekzistonKurs(llojkursi, datakursit, vlerakursi, idmonedha);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static double merrKursinEFundit(int idMonedha, int lloji)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                return data.ktheKursinSipasMonedhesAndllojit(idMonedha, lloji);
            }
        }

        public static double merrKursinFunditPerMonedheDateDheLloj(int idMonedha, DateTime datakursit, int lloji, clsDatabaseAdmin data)
        {
            clsKurset kursi = getKursSipasIdMonedhaDheLlojiFromCache(idMonedha, datakursit, lloji, data);
            return kursi.VleraKursi;
        }

        public static double merrKursinFunditPerMonedheDateDheLloj(int idMonedha, DateTime datakursit, int lloji)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                return data.ktheKursinFunditPerMonedheDateDheLloj(idMonedha, datakursit, lloji);
            }
        }

        public static double merrKursinFunditSipasKodMonedheDateDheLloj(string kodMonedha, string datakursit, int idNdermarrje, int lloji)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                return merrKursinFunditSipasKodMonedheDateDheLloj(kodMonedha, datakursit, idNdermarrje, lloji, data);
            }
        }

        public static double merrKursinFunditSipasKodMonedheDateDheLloj(string kodMonedha, string datakursit, int idNdermarrje, int lloji, clsDatabaseAdmin data)
        {
            return data.ktheKursetSipasKoditMonedhesDatesDheLlojit(kodMonedha, datakursit, idNdermarrje, lloji);
        }

        public static string[] merrKursinFunditTeKonfigPerMonedhatNdermarrjesSipasDates(int idNdermarrja, int idPerdoruesi, int idKonfigAmbjent, DateTime data)
        {
            using (clsDatabaseAdmin db = new clsDatabaseAdmin())
            {
                return db.merrKursinFunditTeKonfigPerMonedhatNdermarrjesSipasDates(idNdermarrja, idPerdoruesi, idKonfigAmbjent, data);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idMonedha"></param>
        /// <param name="vleraKursit"></param>
        /// <param name="dataKursit"></param>
        /// <returns></returns>
        public static clsMesazh ruajKursinTeHistoriku(int idMonedha, double vleraKursit, DateTime dataKursit, string pershkrimllojkursi)
        {
            int llojKursi = 1;//kurs banke
            string pershkrimLlojKursi = "Kursi1";
            if (pershkrimllojkursi != null)
                pershkrimLlojKursi = pershkrimllojkursi;
            int njesiaKursit = 1;
            if (vleraKursit > 0)
            {
                DbCore.DbAdmin.clsKurset kursi = new DbCore.DbAdmin.clsKurset();
                return kursi.krijoKurs(llojKursi, dataKursit, vleraKursit, idMonedha, njesiaKursit, pershkrimLlojKursi);
            }
            return new clsMesazh(false, "Kursi eshte ekziston ne sistem");
        }

        /// <summary>
        /// Krijon kursin ne DB dhe inicializon objektin nese krijimi kryhet me sukses
        /// </summary>
        /// <param name="llojKursi"></param>
        /// <param name="dataKursit"></param>
        /// <param name="vleraKursit"></param>
        /// <param name="monedhaKursit"></param>
        /// <param name="njesiaKursit"></param>
        /// <param name="pershkrimKursi"></param>
        /// <returns></returns>
        public clsMesazh krijoKurs(int llojKursi, DateTime dataKursit, double vleraKursit, int monedhaKursit, int njesiaKursit, string pershkrimKursi)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                clsMesazh mesazhi = data.ruajKurs(out idKursi, llojKursi, dataKursit, vleraKursit, monedhaKursit, njesiaKursit, pershkrimKursi);
                if (mesazhi.Status)
                {
                    this.llojKursi = llojKursi;
                    this.dataKursit = dataKursit;
                    this.vleraKursi = vleraKursit;
                    this.idMonedha = monedhaKursit;
                    this.njesiaKursit = njesiaKursit;
                    this.pershkrimLlojKursi = pershkrimKursi;
                }
                return mesazhi;
            }
        }

        /// <summary>
        /// Krijon kursin ne db dhe i jep vlere id-se se objektit
        /// </summary>
        /// <returns></returns>
        public clsMesazh krijoKurs(clsDatabaseAdmin db)
        {
            return db.ruajKurs(out idKursi, llojKursi, dataKursit, vleraKursi, idMonedha, njesiaKursit, pershkrimLlojKursi);
        }

        /// <summary>
        /// Krijon kursin ne db dhe i jep vlere id-se se objektit
        /// </summary>
        /// <returns></returns>
        public clsMesazh ruajKurs()
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                return data.ruajKurs(out idKursi, llojKursi, dataKursit, vleraKursi, idMonedha, njesiaKursit, pershkrimLlojKursi);
            }
        }

        public clsKurset krijoKursin(Dictionary<string, object> rresht, int idNdermarrje)
        {
            if (rresht["LlojKursi"] == null || rresht["LlojKursi"].ToString() == "")
                throw new MyException("Lloji i kursit nuk eshte i sakte!");
            if (rresht["VleraKursi"] == null || rresht["VleraKursi"].ToString() == "")
                throw new MyException("Vlera e kursit nuk eshte e sakte!");
            if (rresht["DataKursit"] == null && rresht["DataKursit"].ToString() == "")
                throw new MyException("Data e kursit nuk eshte e sakte!");
            if (rresht["NjesiaKursit"] == null && rresht["NjesiaKursit"].ToString() == "")
                throw new MyException("Njesia e kursit nuk eshte e sakte!");
            this.llojKursi = int.Parse(Convert.ToString(rresht["LlojKursi"]));
            this.pershkrimLlojKursi = "Kursi" + this.llojKursi.ToString();
            this.vleraKursi = double.Parse(Convert.ToString(rresht["VleraKursi"]));
            this.dataKursit = DateTime.Parse(Convert.ToString(rresht["DataKursit"]));
            this.njesiaKursit = int.Parse(Convert.ToString(rresht["NjesiaKursit"]));
            return this;
        }

        #endregion

        #region Metoda Internal

        internal bool mbushKurs(DataRow dbDataRowKurset)
        {
            if (dbDataRowKurset != null)
            {
                try
                {
                    int.TryParse(dbDataRowKurset["IDKURSI"].ToString(), out idKursi);
                    int.TryParse(dbDataRowKurset["LLOJIKURSIT"].ToString(), out llojKursi);
                    double.TryParse(dbDataRowKurset["VLERAKURSIT"].ToString(), out vleraKursi);
                    DateTime.TryParse(dbDataRowKurset["DATAKURSIT"].ToString(), out dataKursit);
                    int.TryParse(dbDataRowKurset["IDMONEDHA"].ToString(), out idMonedha);
                    int.TryParse(dbDataRowKurset["NJESIA"].ToString(), out njesiaKursit);
                    pershkrimLlojKursi = dbDataRowKurset["PERSHKRIMLLOJKURSI"].ToString();
                    return true;
                }
                catch (InvalidCastException)
                {
                    ImbLogger.LogErrorShitje("ERROR: Gabim gjate marrjes se kursit nga db-ja");
                    throw new Exception("ERROR: Gabim gjate marrjes se kursit nga db-ja");
                }
            }
            else
                return false;
        }

        internal clsKurset ShallowCopy()
        {
            return (clsKurset)this.MemberwiseClone();
        }

        #endregion
    }
}
