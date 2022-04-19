using DbCore.DbAdmin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DbCore.DbShare;
using DbCore.DbRegjistrim;

namespace DbCore.DbAsete
{
    /// <summary>
    /// MODULI ASETE:
    /// Klase e ndertuar per te mbajtur objektin e serialeve te aqt-ve. Ruajtja e gjithe serialeve ne momentin qe gjenerohen.
    /// Te dhenat merret nga tabela T_ASETE_AQTSERIALE.
    /// </summary>
    public class clsAQTSeriale
    {
        #region Atribute

        private int idAQTSerial;
        private string aqtSerialKod;
        private string aqtSerialPershkrim;
        private int idAQTArt;
        private DateTime aqtSerialDataHyrje;
        private DateTime aqtSerialDataMagAktive;
        private DateTime aqtSerialDataAmortizimfillestar;
        private bool meSerialPerCope;
        private int idNjesiAdministrativeAktuale;
        private int idHistorikAktualPaSerial;
        private int idStatusDokumenti;
        private int idNdermarrje;
        private int idPerdoruesi;
        private int idKrijuesi;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private clsHistorikAQTSeriale historikSeriali;
        private DataRow rreshti;
        private colVleraFushaShtese oColVleratFushatShtese;
        private colArkiva oArkiva;
        #endregion

        #region Properties

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id automatike te serialit te aqt-se.
        /// </summary>
        public int IdAQTSerial
        {
            get { return idAQTSerial; }
            set { idAQTSerial = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (string) Merr ose jep vlere kodit te serialit te aqt-se.
        /// </summary>
        public string AqtSerialKod
        {
            get { return aqtSerialKod; }
            set { aqtSerialKod = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (string) Merr ose jep vlere pershkrimit te serialit te aqt-se.
        /// </summary>
        public string AqtSerialPershkrim
        {
            get { return aqtSerialPershkrim; }
            set { aqtSerialPershkrim = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se artikullit per te cilen eshte id e serialit.
        /// </summary>
        public int IdAQTArt
        {
            get { return idAQTArt; }
            set { idAQTArt = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (DateTime) Merr ose jep daten kur seriali i aqt-se eshte bere hyrje ose blerje per here te pare.
        /// </summary>
        public DateTime AqtSerialDataHyrje
        {
            get { return aqtSerialDataHyrje; }
            set { aqtSerialDataHyrje = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (DateTime) Merr ose jep vlere dates kur seriali i aqt-se eshte bere hyrje per here te pare ne nje magazine aktive.
        /// </summary>
        public DateTime AqtSerialDataMagAktive
        {
            get { return aqtSerialDataMagAktive; }
            set { aqtSerialDataMagAktive = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (DateTime) Merr ose jep vlere dates kur seriali i aqt-se eshte amortizuar per here te pare.
        /// </summary>
        public DateTime AqtSerialDataAmortizimfillestar
        {
            get { return aqtSerialDataAmortizimfillestar; }
            set { aqtSerialDataAmortizimfillestar = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (bool) Merr ose jep vlere nese seriali eshte eshte unik per cdo njesi apo eshte unik per te gjithe sasine e blere, apo levizur.
        /// Merr vleren True nese seriali eshte unik per cdo njesi.
        /// </summary>
        public bool MeSerialPerCope
        {
            get { return meSerialPerCope; }
            set { meSerialPerCope = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se njesise administrative aktuale ku eshte seriali.
        /// </summary>
        public int IdNjesiAdministrativeAktuale
        {
            get { return idNjesiAdministrativeAktuale; }
            set { idNjesiAdministrativeAktuale = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se historikut te serialit qe eshte unik per te gjithe sasine e blere.
        /// </summary>
        public int IdHistorikAktualPaSerial
        {
            get { return idHistorikAktualPaSerial; }
            set { idHistorikAktualPaSerial = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere se ne cfare gjendje eshte seriali i aqt-se, i ruajtur, fshire, apo modifikuar.
        /// </summary>
        public int IdStatusDokumenti
        {
            get { return idStatusDokumenti; }
            set { idStatusDokumenti = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se ndermarrjes ne te cilen eshte seriali i aqt-se.
        /// </summary>
        public int IdNdermarrje
        {
            get { return idNdermarrje; }
            set { idNdermarrje = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se perdoruesit qe ka kryer modifikimi e serialit te aqt-se.
        /// </summary>
        public int IdPerdoruesi
        {
            get { return idPerdoruesi; }
            set { idPerdoruesi = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se krijuesit te serialit te aqt-se.
        /// </summary>
        public int IdKrijuesi
        {
            get { return idKrijuesi; }
            set { idKrijuesi = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (DateTime) Merr ose jep vlere dates se krijimit te serialit te aqt-se.
        /// </summary>
        public DateTime DtKrijimi
        {
            get { return dtKrijimi; }
            set { dtKrijimi = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (DateTime) Merr ose jep vlere dates se modifikimit te serialit te aqt-se.
        /// </summary>
        public DateTime DtModifikimi
        {
            get { return dtModifikimi; }
            set { dtModifikimi = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (clsHistorikAQTSeriale) Merr ose jep vlere historikut te ketij seriali.
        /// </summary>
        public clsHistorikAQTSeriale HistorikSeriali
        {
            get { return historikSeriali; }
            set { historikSeriali = value; }
        }

        /// <summary>
        /// Kthen/Vendos nje collection me objekte te tipit <see cref="DbAdmin.clsVleraFushaShtese"/>
        /// </summary>
        public colVleraFushaShtese OColVleratFushatShtese
        {
            get { return oColVleratFushatShtese; }
            set { oColVleratFushatShtese = value; }
        }

        public colArkiva OArkiva
        {
            get { return oArkiva; }
            set { oArkiva = value; }
        }

        public IDictionary<string, object> HfArkiva { get; set; }

        #endregion

        #region Konstruktori

        /// <summary>
        /// MODULI ASETE:
        /// Krijon objektin bosh te klases clsAQTSeriale per serialet e aqt-ve .
        /// </summary>
        public clsAQTSeriale()
        {
        }

        /// <summary>
        /// MODULI ASETE:
        /// Krijon objektin plote te klases clsAQTSeriale per serialet e aqt-ve .
        /// </summary>
        /// <param name="rreshtDokuKlient">(Dictionary) Objekti i cili do te mbushe klasen.</param>
        public clsAQTSeriale(Dictionary<string, object> rreshtDokuKlient)
        {
            idAQTSerial = Convert.ToInt32(rreshtDokuKlient["IdAQTSerial"]);
            aqtSerialKod = rreshtDokuKlient["AqtSerialKod"].ToString();
            aqtSerialPershkrim = rreshtDokuKlient["AqtSerialPershkrim"].ToString();
            idAQTArt = Convert.ToInt32(rreshtDokuKlient["IdAQTArt"]);
            aqtSerialDataHyrje = Convert.ToDateTime(rreshtDokuKlient["AqtSerialDataHyrje"]);
            aqtSerialDataMagAktive = Convert.ToDateTime(rreshtDokuKlient["AqtSerialDataMagAktive"]);
            aqtSerialDataAmortizimfillestar = Convert.ToDateTime(rreshtDokuKlient["AqtSerialDataAmortizimfillestar"]);
            meSerialPerCope = Convert.ToBoolean(rreshtDokuKlient["MeSerialPerCope"]);
            idNjesiAdministrativeAktuale = Convert.ToInt32(rreshtDokuKlient["IdNjesiAdministrativeAktuale"]);
            idHistorikAktualPaSerial = Convert.ToInt32(rreshtDokuKlient["IdHistorikAktualPaSerial"]);
            idStatusDokumenti = Convert.ToInt32(rreshtDokuKlient["IdStatusDokumenti"]);
            idNdermarrje = Convert.ToInt32(rreshtDokuKlient["IdNdermarrje"]);
            idPerdoruesi = Convert.ToInt32(rreshtDokuKlient["IdPerdoruesi"]);
            idKrijuesi = Convert.ToInt32(rreshtDokuKlient["IdKrijuesi"]);
            dtKrijimi = Convert.ToDateTime(rreshtDokuKlient["DtKrijimi"]);
            dtModifikimi = Convert.ToDateTime(rreshtDokuKlient["DtModifikimi"]);
        }

        public clsAQTSeriale(DataRow rreshti)
        {
            
            mbushAQTSerialObjekt(rreshti);
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// MODULI ASETE:
        /// Metoda perdoret per te mbushur te gjitha atributet e objektit clsAQTSeriale sipas te dhenave qe vijne nga cdo rresht i databazes nga tabela T_ASETE_AQTSERIALE.
        /// </summary>
        /// <param name="dbDataRowAQTSerial">(DataRow) Merr si parameter vetem nje rresht te kthyer nga tabelat ne databaze per te mbushur nje objekt.</param>
        /// <returns>(bool) Kthen true nese nuk ndodh asnje gabim gjate leximit te te dhenave, ne te kundert false.</returns>
        internal bool mbushAQTSerialObjekt(DataRow dbDataRowAQTSerial)
        {
            if (dbDataRowAQTSerial == null)
                return false;
            try
            {
                int.TryParse(dbDataRowAQTSerial["ID_AQTSERIAL"].ToString(), out idAQTSerial);
                aqtSerialKod = dbDataRowAQTSerial["AQTSERIALKOD"].ToString();
                aqtSerialPershkrim = dbDataRowAQTSerial["AQTSERIALPERSHK"].ToString();
                int.TryParse(dbDataRowAQTSerial["IDAQTART"].ToString(), out idAQTArt);
                DateTime.TryParse(dbDataRowAQTSerial["AQTSERIALDATAHYRJE"].ToString(), out aqtSerialDataHyrje);
                DateTime.TryParse(dbDataRowAQTSerial["AQTSERIALDATAMAGAKTIVE"].ToString(), out aqtSerialDataMagAktive);
                DateTime.TryParse(dbDataRowAQTSerial["AQTSERIALDATAAMORTFILLESTAR"].ToString(), out aqtSerialDataAmortizimfillestar);
                bool.TryParse(dbDataRowAQTSerial["MESERIALPERCOPE"].ToString(), out meSerialPerCope);
                int.TryParse(dbDataRowAQTSerial["IDNJESIADMINISTRATIVEAKTUALE"].ToString(), out idNjesiAdministrativeAktuale);
                int.TryParse(dbDataRowAQTSerial["IDHISTORIKAKTUALPASERIAL"].ToString(), out idHistorikAktualPaSerial);
                int.TryParse(dbDataRowAQTSerial["IDSTATUSDOK"].ToString(), out idStatusDokumenti);
                int.TryParse(dbDataRowAQTSerial["IDNDERMARJE"].ToString(), out idNdermarrje);
                int.TryParse(dbDataRowAQTSerial["IDPERDORUESI"].ToString(), out idPerdoruesi);
                int.TryParse(dbDataRowAQTSerial["IDKRIJUESI"].ToString(), out idKrijuesi);
                DateTime.TryParse(dbDataRowAQTSerial["DTKRIJIMI"].ToString(), out dtKrijimi);
                DateTime.TryParse(dbDataRowAQTSerial["DTMODIFIKIMI"].ToString(), out dtModifikimi);

                return true;
            }
            catch (InvalidCastException)
            {
                throw new Exception("ERROR: Gabim gjate marrjes se serialit te aqt-se nga db-ja");
            }
        }

        /// <summary>
        /// MODULI ASETE:
        /// Modifikon atributin e amortizimit fillestar te objektit te serialit te aqt-se.
        /// </summary>
        /// <param name="serialDataAmortizimfillestar">(DateTime) Data e amortizimit kur ka filluar per here te pare.</param>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese modifikimi ne transaksion kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        internal clsMesazh modifikimAQTSerialDataAmortizimfillestar(DateTime serialDataAmortizimfillestar, clsDatabazeAsete moduliAsete)
        {
            clsMesazh pergjigja = moduliAsete.modifikoAQTSerialDataAmortizimfillestar(idAQTSerial, serialDataAmortizimfillestar);
            return pergjigja;
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// MODULI ASETE:
        /// Ruan objektin e serialit te aqt-se nese seriali nuk ekziston.
        /// </summary>
        /// <returns>Kthen clsMesazh me atribut statusi True nese ruajtja kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        public clsMesazh ruaj()
        {
            using (var scope=new MyTransactionScope())
            {
                clsDatabazeAsete dbasete = new clsDatabazeAsete();
                clsMesazh mesazh = ruaj(dbasete);
                if (!mesazh) return mesazh;

                DbAdmin.clsDatabaseAdmin dbAdmin = new DbAdmin.clsDatabaseAdmin(dbasete);
                if (OColVleratFushatShtese != null && oColVleratFushatShtese.Count > 0)
                {
                    OColVleratFushatShtese.ForEach(x => x.IdLidhese = idAQTSerial);
                    mesazh = OColVleratFushatShtese.Ruaj();
                }
                if (!mesazh) return mesazh;
                if (HfArkiva != null)
                    mesazh = DbShare.colArkiva.RuajArkiven(idAQTArt, 116, idPerdoruesi, idNdermarrje, HfArkiva);
                if (!mesazh) return mesazh;
                scope.Complete();
                return mesazh;
            }
        }

        /// <summary>
        /// MODULI ASETE:
        /// Modifikon objektin e serialit te aqt-se nese e gjen te regjistruar.
        /// </summary>
        /// <returns>Kthen clsMesazh me atribut statusi True nese modifikimi kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        public clsMesazh modifiko()
        {

            using (var scope=new MyTransactionScope())
            {
                clsDatabazeAsete dbasete = new clsDatabazeAsete();
                clsMesazh mesazh = modifiko(dbasete);
                if (!mesazh) return mesazh;
                
                DbAdmin.clsDatabaseAdmin dbAdmin = new DbAdmin.clsDatabaseAdmin(dbasete);
                mesazh = colVleraFushaShtese.fshiFushaShtese(idAQTSerial, "Seriale", idNdermarrje, idPerdoruesi, dbAdmin);
                if (!mesazh)  return mesazh;
                
                if(OColVleratFushatShtese.Count > 0)
                {
                    OColVleratFushatShtese.ForEach(x => x.IdLidhese = idAQTSerial);
                    mesazh = OColVleratFushatShtese.Ruaj();
                }

                scope.Complete();
                return mesazh;
            }

        }

        /// <summary>
        /// MODULI ASETE:
        /// Fshin objektin e serialit te aqt-se duke i modifikuar statusin nese eshte i krijuar njehere.
        /// </summary>
        /// <returns>Kthen clsMesazh me atribut statusi True nese fshirja kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        public clsMesazh fshi()
        {
            clsMesazh mesazh;
            using (var scope=new MyTransactionScope())
            {
                clsDatabazeAsete dbasete = new clsDatabazeAsete();
                //dbasete.beginTransaksion();
                mesazh = fshi(dbasete);
                if (!mesazh.Status)
                {
                    //dbasete.rollbackTransaksion();
                    return mesazh;
                }
                mesazh = colArkiva.UpdateStatusDokFshi(idAQTSerial, 116, idPerdoruesi);
                if (!mesazh.Status)
                {
                    return mesazh;
                }
                scope.Complete();
                //dbasete.commitTransaksion();

            }
            return mesazh;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Ruan objektin e serialit te aqt-se nese seriali nuk ekziston.
        /// </summary>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese ruajtja kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        public clsMesazh ruaj(clsDatabazeAsete moduliAsete)
        {
            if (!kontrolloEkzistonAQTSerial(aqtSerialKod, idNdermarrje, moduliAsete))
            {
                clsMesazh pergjigja = ruajAQTSerialTransaksion(moduliAsete);
                return pergjigja;
            }
            return new clsMesazh(false, "Ekziston njehere seriali i aqt-se!");
        }

        /// <summary>
        /// MODULI ASETE:
        /// Modifikon objektin e serialit te aqt-se nese e gjen te regjistruar.
        /// </summary>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese modifikimi kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        public clsMesazh modifiko(clsDatabazeAsete moduliAsete)
        {
            if (kontrolloEkzistonAQTSerial(aqtSerialKod, idNdermarrje, moduliAsete))
            {
                clsMesazh pergjigja = modifikoAQTSerialTransaksionPaFshirje(moduliAsete);
                return pergjigja;
            }
            return new clsMesazh(false, "Nuk ekziston seriali i aqt-se!");
        }

        /// <summary>
        /// MODULI ASETE:
        /// Modifikon objektin e serialit te aqt-se nese e gjen te regjistruar.
        /// </summary>
        /// <param name="idHistorikSeriali">(int) Id e historikut te serialit te ndashem.</param>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese modifikimi kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        public clsMesazh modifikoHistorik(int idHistorikSeriali, clsDatabazeAsete moduliAsete)
        {
            if (kontrolloEkzistonAQTSerial(aqtSerialKod, idNdermarrje))
            {
                clsMesazh pergjigja = modifikoAQTSerialHistorikTransaksion(idHistorikSeriali, moduliAsete);
                return pergjigja;
            }
            return new clsMesazh(false, "Nuk ekziston seriali i aqt-se!");
        }

        /// <summary>
        /// MODULI ASETE:
        /// Fshin objektin e serialit te aqt-se duke i modifikuar statusin nese eshte i krijuar njehere.
        /// </summary>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese fshirja kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        public clsMesazh fshi(clsDatabazeAsete moduliAsete)
        {
            if (kontrolloEkzistonAQTSerial(aqtSerialKod, idNdermarrje, moduliAsete))
            {
                clsMesazh pergjigja = fshiAQTSerialTransaksion(moduliAsete);
                return pergjigja;
            }
            return new clsMesazh(false, "Nuk ekziston seriali i aqt-se!");
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr objektin e serialit te aqt-se sipas id se serialit.
        /// </summary>
        /// <param name="idAQTSerial">(int) Id automatike e serialit te aqt-se qe kerkojme.</param>
        /// <returns>Kthen True nese nuk ndodh asnje gabim gjate marrjes se serialit te aqt-se ose False ne te kundert.</returns>
        public bool merrAQTSerialSipasID(int idAQTSerial)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            bool pergjigje = mbushAQTSerialObjekt(moduliAsete.ktheAQTSerialSipasID(idAQTSerial));
            moduliAsete.Dispose();
            return pergjigje;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr objektin e serialit te aqt-se sipas id se serialit ne transaksion.
        /// </summary>
        /// <param name="idAQTSerial">(int) Id automatike e serialit te aqt-se qe kerkojme.</param>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen True nese nuk ndodh asnje gabim gjate marrjes se serialit te aqt-se ose False ne te kundert.</returns>
        public bool merrAQTSerialSipasID(int idAQTSerial, clsDatabazeAsete moduliAsete)
        {
            bool pergjigje = mbushAQTSerialObjekt(moduliAsete.ktheAQTSerialSipasID(idAQTSerial));
            return pergjigje;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr objektin e serialit te aqt-se sipas kodit te aqt-se.
        /// </summary>
        /// <param name="aqtKod">(string) Kodi i serialit te aqt-se qe kerkojme.</param>
        /// <param name="idNdermarrja">(int) Id e ndermarrjes ne perdorim per te cilen do te merren serialet e aqt-ve.</param>
        /// <returns>Kthen True nese nuk ndodh asnje gabim gjate marrjes se serialit te aqt-se ose False ne te kundert.</returns>
        public bool merrAQTSerialSipasKodAQT(string aqtKod, int idNdermarrja)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            bool pergjigje = mbushAQTSerialObjekt(moduliAsete.ktheAQTSerialSipasKodAQT(aqtKod, idNdermarrja));
            moduliAsete.Dispose();
            return pergjigje;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr id e serialit te aqt-se sipas kodit te aqt-se.
        /// </summary>
        /// <param name="aqtKod">(string) Kodi i serialit te aqt-se qe kerkojme.</param>
        /// <param name="idNdermarrja">(int) Id e ndermarrjes ne perdorim per te cilen do te merren serialet e aqt-ve.</param>
        /// <returns>Kthen id e serialit te aqt-se sipas kodit te serialit te aqt-se qe kerkojme nese gjendet, ne te kundert kthen -1.</returns>
        public static int merrIDAQTSerialSipasKodAQT(string aqtKod, int idNdermarrja)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            int pergjigje = moduliAsete.ktheIDAQTSerialSipasKodAQT(aqtKod, idNdermarrja);
            moduliAsete.Dispose();
            return pergjigje;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr id e artikullit sipas id se serialit.
        /// </summary>
        /// <param name="idAQTSerial">(int) Id automatike e serialit te aqt-se qe kerkojme.</param>
        /// <returns>Kthen id e artikullit nese nuk ndodh asnje gabim gjate marrjes se id se artikullit ose False ne te kundert.</returns>
        public static int merrIDArtikullAQTSerialSipasID(int idAQTSerial)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            int pergjigje = moduliAsete.ktheIDArtikullAQTSerialSipasID(idAQTSerial);
            moduliAsete.Dispose();
            return pergjigje;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr id e artikullit sipas id se serialit ne nje transaksion
        /// </summary>
        /// <param name="idAQTSerial">(int) Id automatike e serialit te aqt-se qe kerkojme.</param>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen id e artikullit nese nuk ndodh asnje gabim gjate marrjes se id se artikullit ose False ne te kundert.</returns>
        public static int merrIDArtikullAQTSerialSipasID(int idAQTSerial, clsDatabazeAsete moduliAsete)
        {
            int pergjigje = moduliAsete.ktheIDArtikullAQTSerialSipasID(idAQTSerial);
            return pergjigje;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kontrollon nese ekziston kodi i serialit te aqt-se qe kerkojme te ruajtura ne bazen e te dhenave.
        /// </summary>
        /// <param name="aqtKod">(string) Kodi i serialit te aqt-se qe kerkojme.</param>
        /// <param name="idNdermarrja">(int) Id e ndermarrjes ne perdorim per te cilen do te merren serialet e aqt-ve.</param>
        /// <returns>Kthen True nese ekziston njehere kodi i serialit per ndermarrjen dhe False ne te kundert.</returns>
        public static bool kontrolloEkzistonAQTSerial(string aqtKod, int idNdermarrja)
        {
            using (clsDatabazeAsete moduliAsete = new clsDatabazeAsete())
            {
                return kontrolloEkzistonAQTSerial(aqtKod, idNdermarrja, moduliAsete);
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="aqtKod"></param>
        /// <param name="idNdermarrja"></param>
        /// <param name="moduliAsete"></param>
        /// <returns></returns>
        public static bool kontrolloEkzistonAQTSerial(string aqtKod, int idNdermarrja, clsDatabazeAsete moduliAsete)
        {

            bool pergjigje = moduliAsete.ekzistonAQTSerial(aqtKod, idNdermarrja);

            return pergjigje;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kontrollon nese ekziston id e serialit te aqt-se qe kerkojme te ruajtura ne bazen e te dhenave.
        /// </summary>
        /// <param name="idserial">(int) Id automatike e serialit te aqt-se qe kerkojme.</param>
        /// <param name="idNdermarrja">(int) Id e ndermarrjes ne perdorim per te cilen do te merren serialet e aqt-ve.</param>
        /// <returns>Kthen True nese ekziston njehere kodi i serialit per ndermarrjen dhe False ne te kundert.</returns>
        public static bool kaveprimeAQTSerial(int idserial, int idNdermarrja)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            bool pergjigje = moduliAsete.kaveprimeAQTSerial(idserial, idNdermarrja);
            moduliAsete.Dispose();
            return pergjigje;
        }

        public static DataRow ktheAqtSerialSipasId(int idAqtSerial)
        {
            using (clsDatabazeAsete moduliAsete = new clsDatabazeAsete())
            {
                return moduliAsete.ktheAQTSerialSipasIdDr(idAqtSerial);
            }
        }

        /// <summary>
        /// MODULI ASETE:
        /// Krijon serialin automatik per artikullin.
        /// </summary>
        /// <param name="idAQTArt">(int) Id e artikullit te serialit.</param>
        /// <param name="aqtSerialDataHyrje">(DateTime) Data kur ka hyre seriali ose eshte blere.</param>
        /// <param name="idNjesiAdministrative">(int) Id e njesise administrative ku eshte seriali.</param>
        /// <param name="idStatusDokumenti">(int) Id e gjendjes se dokumentit.</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes.</param>
        /// <param name="idKrijuesi">(DateTime) Data e krijimit te serialit.</param>
        /// <param name="dbasete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen True nese artikulli eshte me serial te pandashem dhe false ne te kundert.</returns>
        public bool krijoNrSerialAQT(object elemt, DateTime aqtSerialDataHyrje, int idNjesiAdministrative, int idStatusDokumenti, int idNdermarrje, int idKrijuesi, clsDatabazeAsete dbasete)
        {
            string vleraPasardhese;
            DbInventari.clsDatabaseInventari dbinv = new DbInventari.clsDatabaseInventari(dbasete);
            DbAdmin.clsDatabaseAdmin dbadm = new DbAdmin.clsDatabaseAdmin(dbasete);
            //Kapet artikulli per te cilen do behet gjenerimi i nrSerial
            DbInventari.clsArtikulli artikulli = (DbInventari.clsArtikulli)(elemt);
            //Gjen grupin ku ben pjese artikulli
            DbInventari.clsKodifikimArtikulli grupiArtikulli = new DbInventari.clsKodifikimArtikulli(artikulli.Kodifikimi1Artikulli, dbinv);
            if (grupiArtikulli.IdKodifikimi < 1)
                throw new DbCore.MyException("Mungon grupi i pare i artikullit afatgjate " + artikulli.KodArtikulli);
            //Gjen formatin e nr automatik qe perdor grupi i artikullit
            DbAdmin.clsNrAutom nrAutom = DbAdmin.clsNrAutom.merrNumrinAutomatikSipasId(grupiArtikulli.IdFormatiSerial, dbadm);
            bool aktiv = nrAutom.eshteAktivNrAutomatik(aqtSerialDataHyrje, dbadm);
            if (nrAutom.IdNrAutom == 0 || !aktiv)
                throw new DbCore.MyException("Nuk ka numer automatik aktiv per serialet!");

            vleraPasardhese = nrAutom.ktheVlerenParsardheseNrAutomatik(aqtSerialDataHyrje, true, dbadm);
            if (clsStatusMagazine_Asete.AKTIVE == clsStatusMagazine_Asete.merrEmertimStatusMagazinesTeNdermarrjesIDStatus(clsHistorikStatusMagazine.merrIDStatusMagazineNgaHistorikMagazinaSipasIdNjesiAdministrative(idNjesiAdministrative), idNdermarrje))
                krijoObjekt(vleraPasardhese, vleraPasardhese, artikulli.IdArtikulli, aqtSerialDataHyrje, aqtSerialDataHyrje, !artikulli.MeSerial, idNjesiAdministrative, 0, idStatusDokumenti, idNdermarrje, idKrijuesi, idKrijuesi, DateTime.Now, DateTime.Now);
            else
                krijoObjekt(vleraPasardhese, vleraPasardhese, artikulli.IdArtikulli, aqtSerialDataHyrje, DateTime.MinValue, !artikulli.MeSerial, idNjesiAdministrative, 0, idStatusDokumenti, idNdermarrje, idKrijuesi, idKrijuesi, DateTime.Now, DateTime.Now);

            DbCore.DbAdmin.clsNrAutomatikFundit nrFundit = nrAutom.ktheNrAutomatikFundit(aqtSerialDataHyrje, dbadm);
            if (nrFundit != null)
            {
                nrFundit.Vlera = vleraPasardhese;
                nrFundit.Data = aqtSerialDataHyrje;
                nrFundit.IdPerdoruesi = idPerdoruesi;
                DbCore.clsMesazh mesazh = nrFundit.modifiko(dbadm);
                if (!mesazh.Status)
                    throw new Exception(mesazh.PershkrimMesazhi);
            }
            else
            {
                nrFundit = new DbCore.DbAdmin.clsNrAutomatikFundit();
                nrFundit.IdNrAutom = nrAutom.IdNrAutom;
                nrFundit.Vlera = vleraPasardhese;
                nrFundit.Data = aqtSerialDataHyrje;
                nrFundit.IdNdermarje = idNdermarrje;
                nrFundit.IdPerdoruesi = idPerdoruesi;
                nrFundit.IdStatusDok = 1;
                DbCore.clsMesazh mesazh = nrFundit.ruaj(dbadm);
                if (!mesazh.Status)
                    throw new Exception(mesazh.PershkrimMesazhi);
            }

            return artikulli.MeSerial;
        }
        public void krijoNrSerialAQT(DbInventari.clsArtikulli artikulli, colHistorikAQTSeriale colHistorik, string vleraPasardhese, clsKokaMagazina dokMagazine, clsTrupiMagazina trupiMagazine,clsDatabazeAsete dbasete, string Emertimi, DateTime dtMagInaktive)
        {
            if (clsStatusMagazine_Asete.AKTIVE == Emertimi)
            {
                krijoObjekt(vleraPasardhese, vleraPasardhese, artikulli.IdArtikulli, dokMagazine.DtDok, dokMagazine.DtDok, !artikulli.MeSerial, trupiMagazine.IdMag, 0, dokMagazine.IdStatusDok, dokMagazine.IdNdermarrje, dokMagazine.IdPerdoruesi, dokMagazine.IdPerdoruesi, DateTime.Now, DateTime.Now);
                //Krijon historikun e serialit nese artikulli eshte me serial te ndashem.
                if (!artikulli.MeSerial)
                {
                    HistorikSeriali = new clsHistorikAQTSeriale(IdAQTSerial, vleraPasardhese, 0, 0, trupiMagazine.IdKokaMagazina, (trupiMagazine.Sasia * trupiMagazine.Koeficenti), (trupiMagazine.Cmimi / trupiMagazine.Koeficenti), trupiMagazine.Vlefta, dokMagazine.IdStatusDok, dokMagazine.IdNdermarrje, dokMagazine.IdPerdoruesi, dokMagazine.IdPerdoruesi, dokMagazine.DtKrijimi, dokMagazine.DtModifikimi);
                    colHistorik.Add(historikSeriali);
                }
            }

            else {
                krijoObjekt(vleraPasardhese, vleraPasardhese, artikulli.IdArtikulli, dokMagazine.DtDok,dtMagInaktive, !artikulli.MeSerial, trupiMagazine.IdMag, 0, dokMagazine.IdStatusDok, dokMagazine.IdNdermarrje, dokMagazine.IdPerdoruesi, dokMagazine.IdPerdoruesi, DateTime.Now, DateTime.Now);
                //Krijon historikun e serialit nese artikulli eshte me serial te ndashem.
                if (!artikulli.MeSerial)
                {
                    HistorikSeriali = new clsHistorikAQTSeriale(IdAQTSerial, vleraPasardhese, 0, 0, trupiMagazine.IdKokaMagazina, (trupiMagazine.Sasia * trupiMagazine.Koeficenti), (trupiMagazine.Cmimi / trupiMagazine.Koeficenti), trupiMagazine.Vlefta, dokMagazine.IdStatusDok, dokMagazine.IdNdermarrje, dokMagazine.IdPerdoruesi, dokMagazine.IdPerdoruesi, dokMagazine.DtKrijimi, dokMagazine.DtModifikimi);
                    colHistorik.Add(historikSeriali);
                }
            }

        }
        #endregion

        #region Metoda Private

        /// <summary>
        /// MODULI ASETE:
        /// Krijon objektin e plote per klasen clsAQTSeriale
        /// </summary>
        /// <param name="aqtSerialKod">(string) Kodi i serialit aqt.</param>
        /// <param name="aqtSerialPershkrim">(string) Pershkrimi i kodit te serialit aqt.</param>
        /// <param name="idAQTArt">(int) Id e artikullit te serialit.</param>
        /// <param name="aqtSerialDataHyrje">(DateTime) Data kur ka hyre seriali ose eshte blere.</param>
        /// <param name="aqtSerialDataMagAktive">(DateTime) Data e kalimit ne magazine aktive te serialit.</param>
        /// <param name="meSerialPerCope">(bool) Nese seriali eshte i ndashem apo jo.</param>
        /// <param name="idNjesiAdministrativeAktuale">(int) Id e njesise administrative ku eshte seriali.</param>
        /// <param name="idHistorikAktualPaSerial">(int) Id e historikut te serialit te ndashem.</param>
        /// <param name="idStatusDokumenti">(int) Id e gjendjes se dokumentit.</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes.</param>
        /// <param name="idPerdoruesi">(int) Id e perdoruesit.</param>
        /// <param name="idKrijuesi">(int) Id e krijuesit te serialit.</param>
        /// <param name="dtKrijimi">(DateTime) Data e krijimit te serialit.</param>
        /// <param name="dtModifikimi">(DateTime) Data e modifikimit te serialit.</param>
        private void krijoObjekt(string aqtSerialKod, string aqtSerialPershkrim, int idAQTArt, DateTime aqtSerialDataHyrje, DateTime aqtSerialDataMagAktive, bool meSerialPerCope, int idNjesiAdministrativeAktuale, int idHistorikAktualPaSerial, int idStatusDokumenti, int idNdermarrje, int idPerdoruesi, int idKrijuesi, DateTime dtKrijimi, DateTime dtModifikimi)
        {
            this.aqtSerialKod = aqtSerialKod;
            this.aqtSerialPershkrim = aqtSerialPershkrim;
            this.idAQTArt = idAQTArt;
            this.aqtSerialDataHyrje = aqtSerialDataHyrje;
            this.aqtSerialDataMagAktive = aqtSerialDataMagAktive;
            this.meSerialPerCope = meSerialPerCope;
            this.idNjesiAdministrativeAktuale = idNjesiAdministrativeAktuale;
            this.idHistorikAktualPaSerial = idHistorikAktualPaSerial;
            this.idStatusDokumenti = idStatusDokumenti;
            this.idNdermarrje = idNdermarrje;
            this.idPerdoruesi = idPerdoruesi;
            this.idKrijuesi = idKrijuesi;
            this.dtKrijimi = dtKrijimi;
            this.dtModifikimi = dtModifikimi;

            HistorikSeriali = new clsHistorikAQTSeriale();
        }

        /// <summary>
        /// MODULI ASETE:
        /// Ruan objektin e serialit te aqt-se.
        /// </summary>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese ruajtja ne transaksion kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        private clsMesazh ruajAQTSerialTransaksion(clsDatabazeAsete moduliAsete)
        {
            clsMesazh pergjigja = moduliAsete.ruajAQTSerial(out idAQTSerial, aqtSerialKod, aqtSerialPershkrim, idAQTArt, aqtSerialDataHyrje, aqtSerialDataMagAktive, aqtSerialDataAmortizimfillestar, meSerialPerCope, idNjesiAdministrativeAktuale, idHistorikAktualPaSerial, idStatusDokumenti, idNdermarrje, idPerdoruesi, idKrijuesi);
            if (pergjigja.Status && meSerialPerCope)
            {
                historikSeriali.IdAQTSeriale = idAQTSerial;
                pergjigja = historikSeriali.ruaj(moduliAsete);
                if (!pergjigja.Status)
                    return pergjigja;
                pergjigja = modifikoAQTSerialHistorikTransaksion(historikSeriali.IdHistoriku, moduliAsete);
            }
            return pergjigja;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Modifikon objektin e serialit te aqt-se.
        /// </summary>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese modifikimi ne transaksion kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        private clsMesazh modifikoAQTSerialTransaksion(clsDatabazeAsete moduliAsete)
        {
            clsMesazh pergjigja = fshiAQTSerialTransaksion(moduliAsete);
            if (pergjigja.Status)
                pergjigja = ruajAQTSerialTransaksion(moduliAsete);
            return pergjigja;
        }

        private clsMesazh modifikoAQTSerialTransaksionPaFshirje(clsDatabazeAsete moduliAsete)
        {
            clsMesazh pergjigja = ModifikimAQTSerialTransaksion(moduliAsete);
            return pergjigja;
        }


        /// <summary>
        /// MODULI ASETE:
        /// Modifikon atributin e id se historikut te objektit te serialit te aqt-se.
        /// </summary>
        /// <param name="idHistorikSeriali">(int) Id e historikut te serialit te ndashem.</param>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese modifikimi ne transaksion kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        private clsMesazh modifikoAQTSerialHistorikTransaksion(int idHistorikSeriali, clsDatabazeAsete moduliAsete)
        {
            clsMesazh pergjigja = moduliAsete.modifikoAQTSerialHistorik(idAQTSerial, idHistorikSeriali);
            return pergjigja;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Fshin objektin e serialit te aqt-se duke i modifikuar statusin.
        /// </summary>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese fshirja ne transaksion kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        private clsMesazh fshiAQTSerialTransaksion(clsDatabazeAsete moduliAsete)
        {
            clsMesazh pergjigja = moduliAsete.modifikoAQTSerialStatus(merrIDAQTSerialSipasKodAQT(aqtSerialKod, idNdermarrje), idPerdoruesi);
            if (pergjigja.Status && meSerialPerCope)
                pergjigja = historikSeriali.fshi(moduliAsete);
            return pergjigja;
        }

        private clsMesazh ModifikimAQTSerialTransaksion(clsDatabazeAsete moduliAsete)
        {
            clsMesazh pergjigja = moduliAsete.modifikoAQTSerialStatusPaFshirje(merrIDAQTSerialSipasKodAQT(aqtSerialKod, idNdermarrje), idPerdoruesi, aqtSerialKod, aqtSerialPershkrim, idAQTArt, aqtSerialDataHyrje, aqtSerialDataMagAktive, aqtSerialDataAmortizimfillestar, meSerialPerCope, idNjesiAdministrativeAktuale, idHistorikAktualPaSerial, idStatusDokumenti, idNdermarrje, idKrijuesi);

            return pergjigja;
        }
        public clsMesazh fshiSipasArtikullit(clsDatabazeAsete db, int idartikulli, int idperdoruesi)
        {
            return db.modifikoAQTSerialStatusSipasArtikullit(idartikulli, idperdoruesi);
        }
        public DataTable KtheNeDataTableColAQTSeriale(colAQTSeriale colAQTSeriale)
        {
            var dt =KrijoKolonaDt(new DataTable());
            int i = 0;
            foreach (var col in colAQTSeriale)
            {
                DataRow datarow = dt.NewRow();
                datarow.ItemArray = new object[] {i,col.aqtSerialKod,col.aqtSerialPershkrim,col.idAQTArt,col.aqtSerialDataHyrje,col.aqtSerialDataMagAktive,null,col.meSerialPerCope,col.idNjesiAdministrativeAktuale,null,col.idNdermarrje,col.idPerdoruesi,col.idKrijuesi, col.aqtSerialDataHyrje, null,col.idStatusDokumenti};
                dt.Rows.Add(datarow);
                i++;
            }
            return dt;
        }
        private DataTable KrijoKolonaDt(DataTable dt)
        {
            dt.Columns.AddRange(new[] {
            new DataColumn("ID_AQTSERIAL", typeof(decimal)),
            new DataColumn("AQTSERIALKOD", typeof(string)),
            new DataColumn("AQTSERIALPERSHK", typeof(string)),
            new DataColumn("IDAQTART", typeof(decimal)),
            new DataColumn("AQTSERIALDATAHYRJE", typeof(DateTime)),
            new DataColumn("AQTSERIALDATAMAGAKTIVE", typeof(DateTime)),
            new DataColumn("AQTSERIALDATAAMORTFILLESTAR", typeof(DateTime)),
            new DataColumn("MESERIALPERCOPE", typeof(bool)),
            new DataColumn("IDNJESIADMINISTRATIVEAKTUALE", typeof(decimal)),
            new DataColumn("IDHISTORIKAKTUALPASERIAL", typeof(decimal)),
            new DataColumn("IDNDERMARJE", typeof(decimal)),
            new DataColumn("IDPERDORUESI", typeof(decimal)),
            new DataColumn("IDKRIJUESI", typeof(decimal)),
            new DataColumn("DTKRIJIMI", typeof(DateTime)),
            new DataColumn("DTMODIFIKIMI", typeof(DateTime)),
            new DataColumn("IDSTATUSDOK", typeof(decimal)) });
            return dt;
        }

        #endregion

    }
}