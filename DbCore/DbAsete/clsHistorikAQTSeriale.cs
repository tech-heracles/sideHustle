using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DbCore.DbAsete
{
    /// <summary>
    /// MODULI ASETE:
    /// Klase e ndertuar per te mbajtur objektin e historikut te serialit. Ruajtja e gjithe levizjeve, zberthimet ne seriale te tjere, te serialeve qe gjenerohen vetem nje here per dokument blerje.
    /// Te dhenat merret nga tabela T_ASETE_SERIALE_HISTORIKU.
    /// </summary>
    public class clsHistorikAQTSeriale
    {
        #region Atribute

        private int idHistoriku;
        private int idAQTSeriale;
        private string kodiAQTSeriale;
        private int idPrindi;
        private int idPrindiFillestar;
        private int idDok;
        private double sasiaProgresive;
        private double cmimiProgresive;
        private double vleftaProgresive;
        private int idStatusDokumenti;
        private int idNdermarrje;
        private int idPerdoruesi;
        private int idKrijuesi;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private DataRow rreshti;

        #endregion

        #region Properties

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id automatike te historikut te serialeve per dokument hyrje ose blerjeje.
        /// </summary>
        public int IdHistoriku
        {
            get { return idHistoriku; }
            set { idHistoriku = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se serialit grup.
        /// </summary>
        public int IdAQTSeriale
        {
            get { return idAQTSeriale; }
            set { idAQTSeriale = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se prindit te serialit nga eshte gjeneruar. Nese eshte serial fillestar do te jete me vlere 0.
        /// </summary>
        public int IdPrindi
        {
            get { return idPrindi; }
            set { idPrindi = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se prindit te pare qe eshte bere hyrje. Nese eshte serial fillestar do te jete me vlere 0.
        /// </summary>
        public int IdPrindiFillestar
        {
            get { return idPrindiFillestar; }
            set { idPrindiFillestar = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se dokumentit qe e ka gjeneruar serialin grup.
        /// </summary>
        public int IdDok
        {
            get { return idDok; }
            set { idDok = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (double) Merr ose jep vlere sasise progresive qe ka seriali pas gjithe ndarjeve qe mund te kete pesuar. Nese eshte serial fillestar sasia do te jete sa sasia ne dokumenti e hyrjes ose blerjes.
        /// </summary>
        public double SasiaProgresive
        {
            get { return sasiaProgresive; }
            set { sasiaProgresive = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (double) Merr ose jep vlere cmimit qe ka seriali pas gjithe ndarjeve qe mund te kete pesuar. Nese eshte serial fillestar sasia do te jete sa cmimi ne dokumenti e hyrjes ose blerjes.
        /// </summary>
        public double CmimiProgresive
        {
            get { return cmimiProgresive; }
            set { cmimiProgresive = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (double) Merr ose jep vlere vleftes progresive qe ka seriali pas gjithe ndarjeve qe mund te kete pesuar. Nese eshte serial fillestar vlefta do te jete sa vlefta ne dokumenti e hyrjes ose blerjes.
        /// </summary>
        public double VleftaProgresive
        {
            get { return vleftaProgresive; }
            set { vleftaProgresive = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere se ne cfare gjendje eshte historiku i serialit grup, i ruajtur, fshire, apo modifikuar.
        /// </summary>
        public int IdStatusDokumenti
        {
            get { return idStatusDokumenti; }
            set { idStatusDokumenti = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se ndermarrjes ku eshte seriali grup.
        /// </summary>
        public int IdNdermarrje
        {
            get { return idNdermarrje; }
            set { idNdermarrje = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (DateTime) Merr ose jep vlere dates se krijimit te historikut te serialit grup.
        /// </summary>
        public DateTime DtKrijimi
        {
            get { return dtKrijimi; }
            set { dtKrijimi = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (DateTime) Merr ose jep vlere dates se modifikimit te historikut te serialit grup.
        /// </summary>
        public DateTime DtModifikimi
        {
            get { return dtModifikimi; }
            set { dtModifikimi = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se perdoruesit qe modifikon historikun e serialit grup.
        /// </summary>
        public int IdPerdoruesi
        {
            get { return idPerdoruesi; }
            set { idPerdoruesi = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se krijuesit qe e krijon per here te pare rreshtin e historikut te serialit grup.
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
        /// Krijon objektin bosh te klases clsHistorikAQTSeriale per historikun e serialeve grup qe krijohen ne baze te dokumentave te hyrjeve dhe blerjeve.
        /// </summary>
        public clsHistorikAQTSeriale()
        {
        }

        /// <summary>
        /// MODULI ASETE:
        /// Krijon objektin e plote per kete klase clsHistorikAQTSeriale.
        /// </summary>
        /// <param name="idAQTSeriale">(int) Id e serialit.</param>
        /// <param name="idPrindi">(int) Id e prindit nga ka rrjedhur.</param>
        /// <param name="idPrindiFillestar">(int) Id e prindit fillestar nga eshte krijuar.</param>
        /// <param name="idDok">(int) Id e dokumentit qe e ka prodhuar kete serial.</param>
        /// <param name="sasiaProgresive">(double) Sasia progresive e serialit.</param>
        /// <param name="cmimiProgresive">(double) Cmimi progresive i serialit.</param>
        /// <param name="vleftaProgresive">(double) Vlefta progresive e serialit.</param>
        /// <param name="idStatusDokumenti">(int) Id e gjendjes se dokumentit.</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes.</param>
        /// <param name="idPerdoruesi">(int) Id e perdoruesit.</param>
        /// <param name="idKrijuesi">(int) Id e krijuesit te serialit.</param>
        /// <param name="dtKrijimi">(DateTime) Data e krijimit te serialit.</param>
        /// <param name="dtModifikimi">(DateTime) Data e modifikimit te serialit.</param>
        public clsHistorikAQTSeriale(int idAQTSeriale,string kodiAQTSeriale,  int idPrindi, int idPrindiFillestar, int idDok, double sasiaProgresive, double cmimiProgresive, double vleftaProgresive, int idStatusDokumenti, int idNdermarrje, int idPerdoruesi, int idKrijuesi, DateTime dtKrijimi, DateTime dtModifikimi)
        {
            krijoObjekt(idAQTSeriale, kodiAQTSeriale, idPrindi, idPrindiFillestar, idDok, sasiaProgresive, cmimiProgresive, vleftaProgresive, idStatusDokumenti, idNdermarrje, idPerdoruesi, idKrijuesi, dtKrijimi, dtModifikimi);
        }
        public clsHistorikAQTSeriale(DataRow rreshti)
        {
            
            mbushHistorikAQTSerialObjekt(rreshti);
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// MODULI ASETE:
        /// Metoda perdoret per te mbushur te gjitha atributet e objektit clsHistorikAQTSeriale sipas te dhenave qe vijne nga cdo rresht i databazes nga tabela T_ASETE_SERIALE_HISTORIKU.
        /// </summary>
        /// <param name="dbDataRowHistorikAQTSerial">(DataRow) Merr si parameter vetem nje rresht te kthyer nga tabelat ne databaze per te mbushur nje objekt.</param>
        /// <returns>(bool) Kthen true nese nuk ndodh asnje gabim gjate leximit te te dhenave, ne te kundert false.</returns>
        internal bool mbushHistorikAQTSerialObjekt(DataRow dbDataRowHistorikAQTSerial)
        {
            if (dbDataRowHistorikAQTSerial == null)
                return false;
            try
            {
                int.TryParse(dbDataRowHistorikAQTSerial["ID_HISTORIKU"].ToString(), out idHistoriku);
                int.TryParse(dbDataRowHistorikAQTSerial["IDAQTSERIALE"].ToString(), out idAQTSeriale);
                int.TryParse(dbDataRowHistorikAQTSerial["IDPRINDI"].ToString(), out idPrindi);
                int.TryParse(dbDataRowHistorikAQTSerial["IDPRINDIFILLESTAR"].ToString(), out idPrindiFillestar);
                int.TryParse(dbDataRowHistorikAQTSerial["IDDOK"].ToString(), out idDok);
                double.TryParse(dbDataRowHistorikAQTSerial["SASIAPROGRESIVE"].ToString(), out sasiaProgresive);
                double.TryParse(dbDataRowHistorikAQTSerial["CMIMI"].ToString(), out cmimiProgresive);
                double.TryParse(dbDataRowHistorikAQTSerial["VLEFTAPROGRESIVE"].ToString(), out vleftaProgresive);
                int.TryParse(dbDataRowHistorikAQTSerial["IDSTATUSDOK"].ToString(), out idStatusDokumenti);
                int.TryParse(dbDataRowHistorikAQTSerial["IDNDERMARJE"].ToString(), out idNdermarrje);
                int.TryParse(dbDataRowHistorikAQTSerial["IDPERDORUESI"].ToString(), out idPerdoruesi);
                int.TryParse(dbDataRowHistorikAQTSerial["IDKRIJUESI"].ToString(), out idKrijuesi);
                DateTime.TryParse(dbDataRowHistorikAQTSerial["DTKRIJIMI"].ToString(), out dtKrijimi);
                DateTime.TryParse(dbDataRowHistorikAQTSerial["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                return true;
            }
            catch (InvalidCastException)
            {
                throw new Exception("ERROR: Gabim gjate marrjes se historikut te serialeve grup nga db-ja");
            }
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// MODULI ASETE:
        /// Ruan objektin e historikut te serialit aqt ne grup nese nuk ekziston.
        /// </summary>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese ruajtja kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        public clsMesazh ruaj(clsDatabazeAsete moduliAsete)
        {
            if (!kontrolloEkzistonHistorikuAQTSerial(idAQTSeriale, idNdermarrje, moduliAsete))
            {
                clsMesazh pergjigja = ruajHistorikAQTSerialTransaksion(moduliAsete);
                return pergjigja;
            }
            return new clsMesazh(false, "Ekziston njehere historiku i serialit te aqt-se!");
        }

        /// <summary>
        /// MODULI ASETE:
        /// Modifikon objektin e historikut te serialit aqt ne grup nese ekziston.
        /// </summary>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese modifikimi kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        public clsMesazh modifiko(clsDatabazeAsete moduliAsete)
        {
            if (kontrolloEkzistonHistorikuAQTSerial(idAQTSeriale, idNdermarrje, moduliAsete))
            {
                clsMesazh pergjigja = modifikoHistorikAQTSerialTransaksion(moduliAsete);
                return pergjigja;
            }
            return new clsMesazh(false, "Nuk ekziston historiku i serialit te aqt-se!");
        }

        /// <summary>
        /// MODULI ASETE:
        /// Fshin objektin e historikut te serialit aqt ne grup me ndryshim e statusit nese nuk ekziston.        
        /// </summary>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese fshirja kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        public clsMesazh fshi(clsDatabazeAsete moduliAsete)
        {
            if (kontrolloEkzistonHistorikuAQTSerial(idAQTSeriale, idNdermarrje, moduliAsete))
            {
                clsMesazh pergjigja = fshiHistorikAQTSerialTransaksion(moduliAsete);
                return pergjigja;
            }
            return new clsMesazh(false, "Nuk ekziston historiku i serialit te aqt-se!");
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr objektin e historikut te serialit te aqt-se sipas id se historikut te serialit.
        /// </summary>
        /// <param name="idHistorikAQTSerial">(int) Id automatike e historikut te serialit aqt per artikujt grup.</param>
        /// <param name="idNdermarrja">(int) Id e ndermarrjes ne perdorim per te cilen do te merret historiku i aqt-se.</param>
        /// <returns>Kthen True nese nuk ndodh asnje gabim gjate marrjes se historikut te serialit te aqt-se ose False ne te kundert.</returns>
        public bool merrHistorikAQTSerialSipasID(int idHistorikAQTSerial, int idNdermarrja)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            bool pergjigje = mbushHistorikAQTSerialObjekt(moduliAsete.ktheHistorikAQTSerialSipasID(idHistorikAQTSerial, idNdermarrja));
            moduliAsete.Dispose();
            return pergjigje;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr objektin e historikut te serialit te aqt-se sipas id se historikut te serialit ne transaksion.
        /// </summary>
        /// <param name="idHistorikAQTSerial">(int) Id automatike e historikut te serialit aqt per artikujt grup.</param>
        /// <param name="idNdermarrja">(int) Id e ndermarrjes ne perdorim per te cilen do te merret historiku i aqt-se.</param>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen True nese nuk ndodh asnje gabim gjate marrjes se historikut te serialit te aqt-se ose False ne te kundert.</returns>
        public bool merrHistorikAQTSerialSipasID(int idHistorikAQTSerial, int idNdermarrja, clsDatabazeAsete moduliAsete)
        {
            bool pergjigje = mbushHistorikAQTSerialObjekt(moduliAsete.ktheHistorikAQTSerialSipasID(idHistorikAQTSerial, idNdermarrja));

            return pergjigje;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr objektin e historikut te serialit te aqt-se sipas id se serialit te aqt-se.
        /// </summary>
        /// <param name="idAQTSerial">(int) Id e serialit te artikullit AQT per te cilen po kerkohet historiku.</param>
        /// <param name="idNdermarrja">(int) Id e ndermarrjes ne perdorim per te cilen do te merret historiku i aqt-se.</param>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen True nese nuk ndodh asnje gabim gjate marrjes se historikut te serialit te aqt-se ose False ne te kundert.</returns>
        public bool merrHistorikAQTSerialSipasIDAQT(int idAQTSerial, int idNdermarrja)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            bool pergjigje = mbushHistorikAQTSerialObjekt(moduliAsete.ktheHistorikAQTSerialSipasIDAQT(idAQTSerial, idNdermarrja));
            return pergjigje;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr id e historikut te serialit te aqt-se sipas id se serialit te aqt-se.
        /// </summary>
        /// <param name="idAQTSerial">(int) Id e serialit te artikullit AQT per te cilen po kerkohet historiku.</param>
        /// <param name="idNdermarrja">(int) Id e ndermarrjes ne perdorim per te cilen do te merret historiku i aqt-se.</param>
        /// <returns>Kthen id e historikut te serialit te aqt-se sipas id se serialit te aqt-se qe kerkojme nese gjendet, ne te kundert kthen -1.</returns>
        public static int merrIDHistorikuAQTSerialSipasIDAQT(int idAQTSerial, int idNdermarrja)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            int pergjigje = moduliAsete.ktheIDHistorikuAQTSerialSipasIDAQT(idAQTSerial, idNdermarrja);
            moduliAsete.Dispose();
            return pergjigje;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr sasine aktuale te serialit te aqt-se sipas id se serialit te aqt-se.
        /// </summary>
        /// <param name="idAQTSerial">(int) Id e serialit te artikullit AQT per te cilen po kerkohet historiku.</param>
        /// <param name="idNdermarrja">(int) Id e ndermarrjes ne perdorim per te cilen do te merret historiku i aqt-se.</param>
        /// <returns>Kthen sasine aktuale te serialit te aqt-se sipas id se serialit te aqt-se qe kerkojme nese gjendet, ne te kundert kthen -1.</returns>
        public static double merrHistorikuAQTSerialSasiSipasIDAQTSerialit(int idAQTSerial, int idNdermarrja)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            double pergjigje = moduliAsete.ktheHistorikuAQTSerialSasiSipasIDAQTSerialit(idAQTSerial, idNdermarrja);
            return pergjigje;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr vleften aktuale te serialit te aqt-se sipas id se serialit te aqt-se.
        /// </summary>
        /// <param name="idAQTSerial">(int) Id e serialit te artikullit AQT per te cilen po kerkohet historiku.</param>
        /// <param name="idNdermarrja">(int) Id e ndermarrjes ne perdorim per te cilen do te merret historiku i aqt-se.</param>
        /// <returns>Kthen vleften aktuale te serialit te aqt-se sipas id se serialit te aqt-se qe kerkojme nese gjendet, ne te kundert kthen -1.</returns>
        public static double merrHistorikuAQTSerialVlefteSipasIDAQTSerialit(int idAQTSerial, int idNdermarrja)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            double pergjigje = moduliAsete.ktheHistorikuAQTSerialVleftaSipasIDAQTSerialit(idAQTSerial, idNdermarrja);
            moduliAsete.Dispose();
            return pergjigje;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr vleften aktuale te serialit te aqt-se sipas id se serialit te aqt-se ne transaksion.
        /// </summary>
        /// <param name="idAQTSerial">(int) Id e serialit te artikullit AQT per te cilen po kerkohet historiku.</param>
        /// <param name="idNdermarrja">(int) Id e ndermarrjes ne perdorim per te cilen do te merret historiku i aqt-se.</param>
        /// <returns>Kthen vleften aktuale te serialit te aqt-se sipas id se serialit te aqt-se qe kerkojme nese gjendet, ne te kundert kthen -1.</returns>
        public static double merrHistorikuAQTSerialVlefteSipasIDAQTSerialit(int idAQTSerial, int idNdermarrja, clsDatabazeAsete moduliAsete)
        {
            double pergjigje = moduliAsete.ktheHistorikuAQTSerialVleftaSipasIDAQTSerialit(idAQTSerial, idNdermarrja);
            return pergjigje;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr shumen totale te sasise se artikullit te ndashem.
        /// </summary>
        /// <param name="idArtikulli">(int) Id e artikullit.</param>
        /// <returns>Kthen shumen totale te sasise se artikullit te ndashem nese gjendet, ne te kundert kthen -1.</returns>
        public static double merrIDHistorikuAQTSerialSipasIDAQT(int idArtikulli, int idnjesiadm, DateTime date)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            double pergjigje = moduliAsete.ktheHistorikuAQTSerialShumaTotaleSasi(idArtikulli, idnjesiadm,date);
            return pergjigje;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kontrollon nese ekziston historiku i serialit te aqt-se qe kerkojme te ruajtura ne bazen e te dhenave.
        /// </summary>
        /// <param name="idAQTSerial">(int) Id e serialit te artikullit AQT per te cilen po kerkohet historiku.</param>
        /// <param name="idNdermarrja">(int) Id e ndermarrjes ne perdorim per te cilen do te merret historiku i aqt-se.</param>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen True nese ekziston njehere historiku i serialit per ndermarrjen dhe False ne te kundert.</returns>
        public static bool kontrolloEkzistonHistorikuAQTSerial(int idAQTSerial, int idNdermarrja, clsDatabazeAsete moduliAsete)
        {

            bool pergjigje = moduliAsete.ekzistonHistorikuAQTSerial(idAQTSerial, idNdermarrja);
            return pergjigje;
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// MODULI ASETE:
        /// Krijon objektin e plote per kete klase clsHistorikAQTSeriale.
        /// </summary>
        /// <param name="idAQTSeriale">(int) Id e serialit.</param>
        /// /// <param name="kodiAQTSeriale">(string) kodi e serialit.</param>
        /// <param name="idPrindi">(int) Id e prindit nga ka rrjedhur.</param>
        /// <param name="idPrindiFillestar">(int) Id e prindit fillestar nga eshte krijuar.</param>
        /// <param name="idDok">(int) Id e dokumentit qe e ka prodhuar kete serial.</param>
        /// <param name="sasiaProgresive">(double) Sasia progresive e serialit.</param>
        /// <param name="cmimiProgresive">(double) Cmimi progresive i serialit.</param>
        /// <param name="vleftaProgresive">(double) Vlefta progresive e serialit.</param>
        /// <param name="idStatusDokumenti">(int) Id e gjendjes se dokumentit.</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes.</param>
        /// <param name="idPerdoruesi">(int) Id e perdoruesit.</param>
        /// <param name="idKrijuesi">(int) Id e krijuesit te serialit.</param>
        /// <param name="dtKrijimi">(DateTime) Data e krijimit te serialit.</param>
        /// <param name="dtModifikimi">(DateTime) Data e modifikimit te serialit.</param>

        private void krijoObjekt(int idAQTSeriale,string kodiAQTSeriale, int idPrindi, int idPrindiFillestar, int idDok, double sasiaProgresive, double cmimiProgresive, double vleftaProgresive, int idStatusDokumenti, int idNdermarrje, int idPerdoruesi, int idKrijuesi, DateTime dtKrijimi, DateTime dtModifikimi)
        {
            this.idAQTSeriale = idAQTSeriale;
            this.kodiAQTSeriale = kodiAQTSeriale;
            this.idPrindi = idPrindi;
            this.idPrindiFillestar = idPrindiFillestar;
            this.idDok = idDok;
            this.sasiaProgresive = sasiaProgresive;
            this.cmimiProgresive = cmimiProgresive;
            this.vleftaProgresive = vleftaProgresive;
            this.idStatusDokumenti = idStatusDokumenti;
            this.idNdermarrje = idNdermarrje;
            this.idPerdoruesi = idPerdoruesi;
            this.idKrijuesi = idKrijuesi;
            this.dtKrijimi = dtKrijimi;
            this.dtModifikimi = dtModifikimi;
        }
        /// <summary>
        /// MODULI ASETE:
        /// Ruan objektin e historikut te serialit aqt ne grup.
        /// </summary>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese ruajtja ne transaksion kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        private clsMesazh ruajHistorikAQTSerialTransaksion(clsDatabazeAsete moduliAsete)
        {
            clsMesazh pergjigja = moduliAsete.ruajHistorikAQTSerial(out idHistoriku, idAQTSeriale, idPrindi, idPrindiFillestar, idDok, sasiaProgresive, cmimiProgresive, vleftaProgresive, idStatusDokumenti, idNdermarrje, idPerdoruesi, idKrijuesi, dtModifikimi);
            return pergjigja;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Modifikon objektin e historikut te serialit aqt ne grup.
        /// </summary>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese modifikimi ne transaksion kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        private clsMesazh modifikoHistorikAQTSerialTransaksion(clsDatabazeAsete moduliAsete)
        {
            clsMesazh pergjigja = fshiHistorikAQTSerialTransaksion(moduliAsete);
            if (pergjigja.Status)
                pergjigja = ruajHistorikAQTSerialTransaksion(moduliAsete);
            return pergjigja;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Fshin objektin e historikut te serialit aqt ne grup me ndryshim e statusit.  
        /// </summary>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese fshirja ne transaksion kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        private clsMesazh fshiHistorikAQTSerialTransaksion(clsDatabazeAsete moduliAsete)
        {
            clsMesazh pergjigja = moduliAsete.modifikoHistorikAQTSerialStatusSipasHistorikID(merrIDHistorikuAQTSerialSipasIDAQT(idAQTSeriale, idNdermarrje), idPerdoruesi);
            return pergjigja;
        }

        public DataTable KtheNeDataTableColAQTSeriale(colHistorikAQTSeriale colHistorik)
        {
            var dt = KrijoKolonaDtHistorik(new DataTable());
            int i = 0;
            foreach (var col in colHistorik)
            {
                DataRow datarow = dt.NewRow();
                DateTime dtkrijimi = new DateTime();
                DateTime dtModifikimi = new DateTime();
                if (col.dtKrijimi == DateTime.MinValue)
                    dtkrijimi = DateTime.ParseExact("01/01/1900 00:00:00,000", "dd/MM/yyyy HH:mm:ss,fff", System.Globalization.CultureInfo.InvariantCulture);
                else
                    dtkrijimi = col.dtKrijimi;
                if (col.dtModifikimi == DateTime.MinValue)
                    dtModifikimi = DateTime.ParseExact("01/01/1900 00:00:00,000", "dd/MM/yyyy HH:mm:ss,fff", System.Globalization.CultureInfo.InvariantCulture);
                else
                    dtModifikimi = col.dtModifikimi;
                datarow.ItemArray = new object[] { i,col.idAQTSeriale,col.kodiAQTSeriale,col.idPrindi,col.idPrindiFillestar,col.idDok,col.sasiaProgresive,col.cmimiProgresive,col.vleftaProgresive,col.idStatusDokumenti,col.idNdermarrje,col.idPerdoruesi,col.idKrijuesi, dtkrijimi,dtModifikimi};
                dt.Rows.Add(datarow);
                i++;
            }
            return dt;
        }
        public DataTable KrijoKolonaDtHistorik(DataTable dt)
        {
            dt.Columns.AddRange(new[] {
            new DataColumn("ID_HISTORIKU", typeof(decimal)),
            new DataColumn("IDAQTSERIALE", typeof(decimal)),
            new DataColumn("KODIAQTSERIALE", typeof(string)),
            new DataColumn("IDPRINDI", typeof(decimal)),
            new DataColumn("IDPRINDIFILLESTAR", typeof(decimal)),
            new DataColumn("IDDOK", typeof(decimal)),
            new DataColumn("SASIAPROGRESIVE", typeof(float)),
            new DataColumn("CMIMI", typeof(float)),
            new DataColumn("VLEFTAPROGRESIVE", typeof(float)),
            new DataColumn("IDSTATUSDOK", typeof(decimal)),
            new DataColumn("IDNDERMARJE", typeof(decimal)),
            new DataColumn("IDPERDORUESI", typeof(decimal)),
            new DataColumn("IDKRIJUESI", typeof(decimal)),
            new DataColumn("DTKRIJIMI", typeof(DateTime)),
            new DataColumn("DTMODIFIKIMI", typeof(DateTime))});
            return dt;
        }
        #endregion
    }
}
