using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DbCore.DbAsete
{
    /// <summary>
    /// MODULI ASETE:
    /// Klase e ndertuar per te mbajtur objektin e serialeve te lidhura me dokumentet qe prekin keto seriale.
    /// Te dhenat merret nga tabela T_ASETE_LIDHJE_MAGAZINE_SERIAL.
    /// </summary>
    public class clsSerialetMagazine
    {
        #region Atribute

        private int idSerialTrupi;
        private int idDok;
        private int nrRendor;
        private int idArtikulli;
        private int idAQTSeriali;
        private string kodSerialAqt;
        private int idNiveli;
        private int idKonfigAmbjenti;
        private int idNjesiAdministrative;
        private double sasia;
        private double cmimi;
        private double vlefta;
        private int idStatusDokumenti;
        private int idNdermarrje;
        private int idPerdoruesi;
        private int idKrijuesi;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private int nrRendDitor;
        private DataRow rreshti;


        #endregion

        #region Properties

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id automatike te veprimit ne magazine qe eshte bere me serialin.
        /// </summary>
        public int IdSerialTrupi
        {
            get { return idSerialTrupi; }
            set { idSerialTrupi = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se dokumentit qe eshte bere veprim me serialin.
        /// </summary>
        public int IdDok
        {
            get { return idDok; }
            set { idDok = value; }
        }

        public int NrRendDitor
        {
            get
            {
                return nrRendDitor;
            }
            set
            {
                nrRendDitor = value;
            }
        }
        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id e rreshtit ku eshte perdorur seriali te trupi i dokumentit te magazines.
        /// </summary>
        public int NrRendor
        {
            get { return nrRendor; }
            set { nrRendor = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id e artikullit qe eshte vepruar ne dokumentin e magazines.
        /// </summary>
        public int IdArtikulli
        {
            get { return idArtikulli; }
            set { idArtikulli = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se serialit qe eshte vepruar ne dokumentin e magazines.
        /// </summary>
        public int IdAQTSeriali
        {
            get { return idAQTSeriali; }
            set { idAQTSeriali = value; }
        }
        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se serialit qe eshte vepruar ne dokumentin e magazines.
        /// </summary>
        public string KodSerialAqt
        {
            get { return kodSerialAqt; }
            set { kodSerialAqt = value; }
        }
        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se llojit te dokumentit qe eshte bere veprimi i dokumentit te magazines.
        /// </summary>
        public int IdNiveli
        {
            get { return idNiveli; }
            set { idNiveli = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se nenkategorise se perdorur ne dokumentin e magazines.
        /// </summary>
        public int IdKonfigAmbjenti
        {
            get { return idKonfigAmbjenti; }
            set { idKonfigAmbjenti = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se magazines qe eshte bere veprimi ne dokumentin e magazines.
        /// </summary>
        public int IdNjesiAdministrative
        {
            get { return idNjesiAdministrative; }
            set { idNjesiAdministrative = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (double) Merr ose jep vlere id se sasise se prekur nga dokumenti i magazines per serialin.
        /// </summary>
        public double Sasia
        {
            get { return sasia; }
            set { sasia = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (double) Merr ose jep vlere cmimit te serialit te prekur nga dokumenti i magazines.
        /// </summary>
        public double Cmimi
        {
            get { return cmimi; }
            set { cmimi = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (double) Merr ose jep vlere vleftes se serialit te prekur nga dokumenti i magazines.
        /// </summary>
        public double Vlefta
        {
            get { return vlefta; }
            set { vlefta = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere se ne cfare gjendje eshte dokumenti i magazines per serialin, i ruajtur, fshire, apo modifikuar.
        /// </summary>
        public int IdStatusDokumenti
        {
            get { return idStatusDokumenti; }
            set { idStatusDokumenti = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se ndermarrjes ne te cilen eshte dokumenti i magazines per serialin.
        /// </summary>
        public int IdNdermarrje
        {
            get { return idNdermarrje; }
            set { idNdermarrje = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (DateTime) Merr ose jep vlere dates se krijimit te dokumentit te magazines per serialin.
        /// </summary>
        public DateTime DtKrijimi
        {
            get { return dtKrijimi; }
            set { dtKrijimi = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (DateTime) Merr ose jep vlere dates se modifikimit te dokumentit te magazines per serialin.
        /// </summary>
        public DateTime DtModifikimi
        {
            get { return dtModifikimi; }
            set { dtModifikimi = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se perdoruesit qe modifikon te dokumentin e magazines per serialin.
        /// </summary>
        public int IdPerdoruesi
        {
            get { return idPerdoruesi; }
            set { idPerdoruesi = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se krijuesit qe e krijon per here te pare dokumentin e magazines per serialin.
        /// </summary>
        public int IdKrijuesi
        {
            get { return idKrijuesi; }
            set { idKrijuesi = value; }
        }

        #endregion

        #region Konstruktori

        /// <summary>
        /// MODULI ASETE:
        /// Krijon objektin bosh te klases clsSerialetMagazine per id e serialeve qe preken ne trupat e dokumentave te magazines .
        /// </summary>
        public clsSerialetMagazine()
        {
        }

        /// <summary>
        /// MODULI ASETE:
        /// Krijon objektin bosh te klases clsSerialetMagazine per id e serialeve qe preken ne trupat e dokumentave te magazines.
        /// </summary>
        /// <param name="idSerialTrupi">(int) Id automatike e lidhjes se serialit me dokumentin e magazines.</param>
        /// <param name="idDok">(int) Id e dokumentit te magazines.</param>
        /// <param name="nrRendor">(int) Id e rreshtit qe ndodhet seriali.</param>
        /// <param name="idArtikulli">(int) Id e artikullit qe po regjistrohet.</param>
        /// <param name="idAQTSeriali">(int) Id e serialit te aqt-se qe po regjistrohet.</param>
        /// <param name="idNiveli">(int) Id e lloji kryesor te dokumentit.</param>
        /// <param name="idKonfigAmbjenti">(int) Id e nenllojit te dokumentit.</param>
        /// <param name="idNjesiAdministrative">(int) Id e magazines qe ka prekur dokumenti.</param>
        /// <param name="sasia">(double) Sasia e prekur nga dokumenti i magazines.</param>
        /// <param name="cmimi">(double) Cmimi i prekur nga dokumenti i magazines.</param>
        /// <param name="vlefta">(double) Vlefta e prekur nga dokumenti i magazines.</param>
        /// <param name="idStatusDokumenti">(int) Id se ne cfare gjendje eshte lidhja mes dokumentit dhe serialit, i ruajtur, fshire, apo modifikuar.</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes per te cilen po behet regjistrimi i lidhjes.</param>
        /// <param name="idPerdoruesi">(int) Id e perdoruesit te fundit qe ka modifikuar lidhjen.</param>
        /// <param name="idKrijuesi">(int) Id e krijuesit per here te pare te lidhjes.</param>
        public clsSerialetMagazine(int idSerialTrupi, int idDok, int nrRendor, int idArtikulli, int idAQTSeriali,string kodSerialAqt, int idNiveli, int idKonfigAmbjenti, int idNjesiAdministrative, double sasia, double cmimi, double vlefta, int idStatusDokumenti, int idNdermarrje, int idPerdoruesi, int idKrijuesi, int nrRendDitor)
        {
            this.idSerialTrupi = idSerialTrupi;
            this.idDok = idDok;
            this.nrRendor = nrRendor;
            this.idArtikulli = idArtikulli;
            this.idAQTSeriali = idAQTSeriali;
            this.kodSerialAqt = kodSerialAqt;
            this.idNiveli = idNiveli;
            this.idKonfigAmbjenti = idKonfigAmbjenti;
            this.idNjesiAdministrative = idNjesiAdministrative;
            this.sasia = sasia;
            this.cmimi = cmimi;
            this.vlefta = vlefta;
            this.idStatusDokumenti = idStatusDokumenti;
            this.idNdermarrje = idNdermarrje;
            this.idPerdoruesi = idPerdoruesi;
            this.idKrijuesi = idKrijuesi;
            this.nrRendDitor = nrRendDitor;

        }
        public clsSerialetMagazine(int idSerialTrupi, int idDok, int nrRendor, int idArtikulli, int idAQTSeriali, int idNiveli, int idKonfigAmbjenti, int idNjesiAdministrative, double sasia, double cmimi, double vlefta, int idStatusDokumenti, int idNdermarrje, int idPerdoruesi, int idKrijuesi, int nrRendDitor)
        {
            this.idSerialTrupi = idSerialTrupi;
            this.idDok = idDok;
            this.nrRendor = nrRendor;
            this.idArtikulli = idArtikulli;
            this.idAQTSeriali = idAQTSeriali;
            this.idNiveli = idNiveli;
            this.idKonfigAmbjenti = idKonfigAmbjenti;
            this.idNjesiAdministrative = idNjesiAdministrative;
            this.sasia = sasia;
            this.cmimi = cmimi;
            this.vlefta = vlefta;
            this.idStatusDokumenti = idStatusDokumenti;
            this.idNdermarrje = idNdermarrje;
            this.idPerdoruesi = idPerdoruesi;
            this.idKrijuesi = idKrijuesi;
            this.nrRendDitor = nrRendDitor;
        }

        public clsSerialetMagazine(DataRow rreshti)
        {
            
            mbushSerialetMagazineObjekt(rreshti);
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// MODULI ASETE:
        /// Metoda perdoret per te mbushur te gjitha atributet e objektit clsSerialetMagazine sipas te dhenave qe vijne nga cdo rresht i databazes nga tabela T_ASETE_LIDHJE_MAGAZINE_SERIAL.
        /// </summary>
        /// <param name="dbDataRowSerialetMagazine">(DataRow) Merr si parameter vetem nje rresht te kthyer nga tabelat ne databaze per te mbushur nje objekt.</param>
        /// <returns>(bool) Kthen true nese nuk ndodh asnje gabim gjate leximit te te dhenave, ne te kundert false.</returns>
        internal bool mbushSerialetMagazineObjekt(DataRow dbDataRowSerialetMagazine)
        {
            if (dbDataRowSerialetMagazine == null)
                return false;
            try
            {
                int.TryParse(dbDataRowSerialetMagazine["ID_SERIALTRUPI"].ToString(), out idSerialTrupi);
                int.TryParse(dbDataRowSerialetMagazine["IDDOK"].ToString(), out idDok);
                int.TryParse(dbDataRowSerialetMagazine["NRRENDDOK"].ToString(), out nrRendor);
                int.TryParse(dbDataRowSerialetMagazine["NRRENDDITOR"].ToString(), out nrRendDitor);
                int.TryParse(dbDataRowSerialetMagazine["IDARTIKULLI"].ToString(), out idArtikulli);
                int.TryParse(dbDataRowSerialetMagazine["IDSERIALI"].ToString(), out idAQTSeriali);
                int.TryParse(dbDataRowSerialetMagazine["IDNIVELI"].ToString(), out idNiveli);
                int.TryParse(dbDataRowSerialetMagazine["IDKONFIGAMBJENTI"].ToString(), out idKonfigAmbjenti);
                int.TryParse(dbDataRowSerialetMagazine["IDNJESIADMINISTRATIVE"].ToString(), out idNjesiAdministrative);
                double.TryParse(dbDataRowSerialetMagazine["SASIA"].ToString(), out sasia);
                double.TryParse(dbDataRowSerialetMagazine["CMIMI"].ToString(), out cmimi);
                double.TryParse(dbDataRowSerialetMagazine["VLEFTA"].ToString(), out vlefta);
                int.TryParse(dbDataRowSerialetMagazine["IDSTATUSDOK"].ToString(), out idStatusDokumenti);
                int.TryParse(dbDataRowSerialetMagazine["IDNDERMARJE"].ToString(), out idNdermarrje);
                int.TryParse(dbDataRowSerialetMagazine["IDPERDORUESI"].ToString(), out idPerdoruesi);
                int.TryParse(dbDataRowSerialetMagazine["IDKRIJUESI"].ToString(), out idKrijuesi);
                DateTime.TryParse(dbDataRowSerialetMagazine["DTKRIJIMI"].ToString(), out dtKrijimi);
                DateTime.TryParse(dbDataRowSerialetMagazine["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                return true;
            }
            catch (InvalidCastException)
            {
                throw new Exception("ERROR: Gabim gjate marrjes se serialeve per veprimin e dokumentit te magazines nga db-ja");
            }
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// MODULI ASETE:
        /// Ruan objektin e lidhjes se serialeve me dokumentin e magazines.
        /// </summary>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese ruajtja kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        public clsMesazh ruaj()
        {
            clsMesazh pergjigja = ruajSerialetMagazineTransaksion();
            return pergjigja;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Modifikon objektin e lidhjes se serialeve me dokumentin e magazines.
        /// </summary>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese modifikimi kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        public clsMesazh modifiko()
        {
            clsMesazh pergjigja = modifikoSerialetMagazineTransaksion();
            return pergjigja;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Ruan objektin e lidhjes se serialeve me dokumentin e magazines.
        /// </summary>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese fshirja kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        public clsMesazh fshi()
        {
            clsMesazh pergjigja = fshiSerialetMagazineTransaksion();
            return pergjigja;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr objektin e dokumentit qe prek serialin te aqt-se sipas id se serialit dhe id se dokumentit.
        /// </summary>
        /// <param name="idDok">(int) Id e dokumentit te magazines qe ka perdorur serialin qe po kerkojme.</param>
        /// <param name="idSerial">(int) Id e serialit aqt qe po kerkojme.</param>
        /// <param name="idNdermarrja">(int) Id e ndermarrjes ne perdorim per te cilen do te merret serialet e dokumentit te magazines.</param>
        /// <returns>Kthen True nese nuk ndodh asnje gabim gjate marrjes se dokumentit me serialin e aqt-se ose False ne te kundert.</returns>
        public bool merrSerialetMagazineSipasIDSerialIDDok(int idDok, int idSerial, int idNdermarrja)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            bool pergjigje = mbushSerialetMagazineObjekt(moduliAsete.ktheSerialetMagazineSipasIDSerialIDDok(idDok, idSerial, idNdermarrja));
            moduliAsete.Dispose();
            return pergjigje;
        } 
        public static double merrSerialetMagazineSipasIDSerialIDDokSasi(int idDok, int idSerial, int idNdermarrja)
        {
            using (clsDatabazeAsete moduliAsete = new clsDatabazeAsete())
            {
                double pergjigje = moduliAsete.ktheSerialetMagazineSipasIDSerialIDDokSasi(idDok, idSerial, idNdermarrja);
                return pergjigje;
            }
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr id e lidhjes me serialit dhe magazines qe kerkojme.
        /// </summary>
        /// <param name="idDok">(int) Id e dokumentit te magazines qe ka perdorur serialin qe po kerkojme.</param>
        /// <param name="idSerial">(int) Id e serialit aqt qe po kerkojme.</param>
        /// <param name="idNdermarrja">(int) Id e ndermarrjes ne perdorim per te cilen do te merret serialet e dokumentit te magazines.</param>
        /// <returns>Kthen id e serialit dhe magazines te serialit te aqt-se nese gjendet, ne te kundert kthen -1.</returns>
        public static int merrIDSerialetMagazineSipasIDSerialIDDok(int idDok, int idSerial, int idNdermarrja)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            int pergjigje = moduliAsete.ktheIDSerialetMagazineSipasIDSerialIDDok(idDok, idSerial, idNdermarrja);
            moduliAsete.Dispose();
            return pergjigje;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr vleren e serialit ne dokumentin e fundit te serialit ne transaksion, PER ARTIKUJT E PANDASHEM.
        /// PER ARTIIKUJT E NDASHEM SHIKO: merrHistorikuAQTSerialVlefteSipasIDAQTSerialit
        /// </summary>
        /// <param name="idSerial">(int) Id e serialit aqt qe po kerkojme.</param>
        /// <param name="idNdermarrja">(int) Id e ndermarrjes ne perdorim per te cilen do te merret serialet e dokumentit te magazines.</param>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen vleren e serialit ne dokumentin e fundit te serialit nese gjendet, ne te kundert kthen -1.</returns>
        public static double merrSerialetMagazineSipasIDSerialDokFundit(int idSerial, int idNdermarrja, DateTime data, int nrrend,clsDatabazeAsete moduliAsete)
        {
            double pergjigje = moduliAsete.ktheSerialetMagazineSipasIDSerialDokFundit(idSerial, idNdermarrja,data, nrrend);
            return pergjigje;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr vleren e serialit ne dokumentin e fundit te serialit, PER ARTIKUJT E PANDASHEM.
        /// PER ARTIIKUJT E NDASHEM SHIKO: merrHistorikuAQTSerialVlefteSipasIDAQTSerialit
        /// </summary>
        /// <param name="idSerial">(int) Id e serialit aqt qe po kerkojme.</param>
        /// <param name="idNdermarrja">(int) Id e ndermarrjes ne perdorim per te cilen do te merret serialet e dokumentit te magazines.</param>
        /// <returns>Kthen vleren e serialit ne dokumentin e fundit te serialit nese gjendet, ne te kundert kthen -1.</returns>

        public static double merrSerialetMagazineSipasIDSerialDokFundit(int idSerial, int idNdermarrja, DateTime data, int nrrend)
        {
            using (clsDatabazeAsete moduliAsete = new clsDatabazeAsete())
            {

                double pergjigje = moduliAsete.ktheSerialetMagazineSipasIDSerialDokFundit(idSerial, idNdermarrja, data, nrrend);
                return pergjigje;
            }
        }

        public static double ktheSerialetMagazineSipasIDSerialDheDates(int idSerial, int idNdermarrja, DateTime data, int nrrend)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            double pergjigje = moduliAsete.ktheSerialetMagazineSipasIDSerialDheDates(idSerial, idNdermarrja, data, nrrend);
            moduliAsete.Dispose();
            return pergjigje;
        }

        public static double ktheSerialetMagazineSipasIDSerialDheDates(int idSerial, int idNdermarrja, DateTime data,int nrrend, clsDatabazeAsete moduliAsete)
        {
            double pergjigje = moduliAsete.ktheSerialetMagazineSipasIDSerialDheDates(idSerial, idNdermarrja, data, nrrend);
            return pergjigje;
        }

        public bool ktheSerialetMagazineSipasIDSerialDokFunditmerrVepriminFundit(int idSerial, int idNdermarrja, DateTime data, int nrrend)
        {
            using (clsDatabazeAsete moduliAsete = new clsDatabazeAsete())
            {
                bool pergjigje = mbushSerialetMagazineObjekt(moduliAsete.ktheSerialetMagazineSipasIDSerialDokFunditmerrVepriminFundit(idSerial, idNdermarrja, data, nrrend));
                return pergjigje;
            }
        }

        public bool ktheSerialetMagazineSipasIDSerialDokFunditmerrVepriminFundit(int idSerial, int idNdermarrja, DateTime data,int nrrend, clsDatabazeAsete moduliAsete)
        {
            bool pergjigje = mbushSerialetMagazineObjekt(moduliAsete.ktheSerialetMagazineSipasIDSerialDokFunditmerrVepriminFundit(idSerial, idNdermarrja,data,nrrend));

            return pergjigje;
        }
        public int ktheSerialetMagazineSipasIdmagFundit(int idSerial, int idNdermarrja, DateTime data, clsDatabazeAsete moduliAsete)
        {
            int pergjigje = moduliAsete.ktheSerialetMagazineSipasIdmagFundit(idSerial, idNdermarrja,data);

            return pergjigje;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr cmimin e serialit ne dokumentin e fundit te serialit.
        /// </summary>
        /// <param name="idSerial">(int) Id e serialit aqt qe po kerkojme.</param>
        /// <param name="idNdermarrja">(int) Id e ndermarrjes ne perdorim per te cilen do te merret serialet e dokumentit te magazines.</param>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen cmimin e serialit ne dokumentin e fundit te serialit nese gjendet, ne te kundert kthen -1.</returns>
        public static double ktheSerialetMagazineCmimiSipasIDSerialDokFundit(int idSerial, int idNdermarrja, DateTime data, int nrrendor, clsDatabazeAsete moduliAsete)
        {

            double pergjigje = moduliAsete.ktheSerialetMagazineCmimiSipasIDSerialDokFundit(idSerial, idNdermarrja,data, nrrendor);

            return pergjigje;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr gjendjen totale te artikullit ne magazine.
        /// </summary>
        /// <param name="idartikull">(int) Id e artikullit.</param>
        /// <param name="idNdermarrja">(int) Id e ndermarrjes ne perdorim per te cilen do te merret serialet e dokumentit te magazines.</param>
        /// <param name="idmagazine">(int) Id e magazines.</param>
        /// <returns>Kthen gjendjen totale ne vlere te artikullit ne magazine, ne te kundert kthen -1.</returns>
        public static double ktheSerialetMagazineGjendjeTotaleArtikulliNeMagazine(int idartikull, int idNdermarrja, int idmagazine, DateTime data)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            double pergjigje = moduliAsete.ktheSerialetMagazineGjendjeTotaleArtikulliNeMagazine(idartikull, idNdermarrja, idmagazine, data);
            moduliAsete.Dispose();
            return pergjigje;
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// MODULI ASETE:
        /// Ruan objektin e historikut te statusit te magazines.
        /// </summary>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese ruajtja ne transaksion kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        private clsMesazh ruajSerialetMagazineTransaksion()
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            clsMesazh pergjigja = moduliAsete.ruajSerialetMagazine(out idSerialTrupi, idDok, nrRendor, idArtikulli, idAQTSeriali, idNiveli, idKonfigAmbjenti, idNjesiAdministrative, sasia, cmimi, vlefta, idStatusDokumenti, idNdermarrje, idPerdoruesi, idKrijuesi, dtModifikimi);
            return pergjigja;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Modifikon objektin e lidhjes se serialeve me dokumentin e magazines.
        /// </summary>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese modifikimi ne transaksion kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        private clsMesazh modifikoSerialetMagazineTransaksion()
        {
            clsMesazh pergjigja = fshiSerialetMagazineTransaksion();
            if (pergjigja.Status)
                pergjigja = ruajSerialetMagazineTransaksion();
            return pergjigja;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Fshin objektin e lidhjes se serialeve me dokumentin e magazines nepermjet ndryshimit te statusit.
        /// </summary>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese fshirja ne transaksion kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        private clsMesazh fshiSerialetMagazineTransaksion()
        {
            using (clsDatabazeAsete moduliAsete = new clsDatabazeAsete())
            {
                return moduliAsete.modifikoSerialetMagazineStatus(idSerialTrupi, idPerdoruesi);
            }
        }

        public DataTable ktheNeDataTableCol(colSerialetMagazine colSerialMag , int iddok)
        {
            DataTable dt = KrijoKolonaDataTable(new DataTable());
            int i = 0;
            foreach (clsSerialetMagazine col in colSerialMag)
            {
                DataRow dataRow = dt.NewRow();
                dataRow.ItemArray = new object[] {i, iddok, col.nrRendor,col.idArtikulli,col.idAQTSeriali,col.idNiveli,col.idKonfigAmbjenti,col.idNjesiAdministrative,col.sasia,col.cmimi,col.vlefta,col.IdStatusDokumenti,col.idNdermarrje,col.idPerdoruesi,col.idKrijuesi, DateTime.Now.Date };
                dt.Rows.Add(dataRow);
                i++;
            }
            return dt;
        }
        private DataTable KrijoKolonaDataTable(DataTable dt)
        {
            dt.Columns.AddRange(new[] {
            new DataColumn("ID_SERIALTRUPI", typeof(decimal)),
            new DataColumn("IDDOK",typeof(decimal)),
            new DataColumn("NRRENDDOK", typeof(decimal)),
            new DataColumn("IDARTIKULLI", typeof(decimal)),
            new DataColumn("IDSERIALI", typeof(decimal)),
            new DataColumn("IDNIVELI", typeof(decimal)),
            new DataColumn("IDKONFIGAMBJENTI", typeof(decimal)),
            new DataColumn("IDNJESIADMINISTRATIVE", typeof(decimal)),
            new DataColumn("SASIA", typeof(float)),
            new DataColumn("CMIMI", typeof(float)),
            new DataColumn("VLEFTA", typeof(float)),
            new DataColumn("IDSTATUSDOK", typeof(decimal)),
            new DataColumn("IDNDERMARJE", typeof(decimal)),
            new DataColumn("IDPERDORUESI", typeof(decimal)),
            new DataColumn("IDKRIJUESI", typeof(decimal)),
            new DataColumn("DTMODIFIKIMI", typeof(DateTime)) });
            return dt;
        }
        #endregion
    }
}
