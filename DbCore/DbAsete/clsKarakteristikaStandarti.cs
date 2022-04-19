using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DbCore.DbAsete
{
    /// <summary>
    /// MODULI ASETE:
    /// Klase e ndertuar per te mbajtur objektin e karakteristikave te kokes se standarteve ne modulin e aseteve.
    /// Te dhenat merret nga tabela T_ASETE_LIDHJE_STANDART_STATUSMAG
    /// </summary>
    public class clsKarakteristikaStandarti
    {
        #region Atribute

        private int idKarakteristika;
        private int idStandart;
        private int idKodifikimArtikulli;
        private int idKonfig;
        private int idFillimAmortizimi;
        private int idMbarimAmortizimi;
        private bool perfshihetDitaPare;
        private bool kontabilizim;
        private int idNdermarrje;
        private int idStatusDokumenti;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private int idPerdoruesi;
        private int idKrijuesi;
        private colKarakteristikaStandartiTrupi colTrupi;
        private DataRow rreshti;

        #endregion

        #region Properties

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id automatike te karakteristikave te standartit
        /// </summary>
        public int IdKarakteristika
        {
            get { return idKarakteristika; }
            set { idKarakteristika = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se standartit per te cilat do te jane karakteristikat
        /// </summary>
        public int IdStandart
        {
            get { return idStandart; }
            set { idStandart = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se grupit qe do te zbatohen karakteristikat e standartit.
        /// </summary>
        public int IdKodifikimArtikulli
        {
            get { return idKodifikimArtikulli; }
            set { idKodifikimArtikulli = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id e konfigurimit te ambjentit te kontrolleve.
        /// </summary>
        public int IdKonfig
        {
            get { return idKonfig; }
            set { idKonfig = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se periudhes se kur do filloj amortizimi ne lidhje me diten e pare te muajit.
        /// </summary>
        public int IdFillimAmortizimi
        {
            get { return idFillimAmortizimi; }
            set { idFillimAmortizimi = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se periudhes se kur do te mbaroj amortizimi ne lidhje me diten e fundit te muajit.
        /// </summary>
        public int IdMbarimAmortizimi
        {
            get { return idMbarimAmortizimi; }
            set { idMbarimAmortizimi = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (bool) Merr ose jep vlere nese do te perfshihet dita e pare pas dates se dokumentit te blerjes.
        /// </summary>
        public bool PerfshihetDitaPare
        {
            get { return perfshihetDitaPare; }
            set { perfshihetDitaPare = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (bool) Merr ose jep vlere nese ky standart duhet apo jo te kontabilizohet
        /// </summary>
        public bool Kontabilizim
        {
            get { return kontabilizim; }
            set { kontabilizim = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere se ne cfare gjendje eshte karakteristika e amortizimit, e ruajtur, fshire, apo modifikuar.
        /// </summary>
        public int IdStatusDokumenti
        {
            get { return idStatusDokumenti; }
            set { idStatusDokumenti = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se ndermarrjes ne te cilen eshte krijuar karakteristika e standartit.
        /// </summary>
        public int IdNdermarrje
        {
            get { return idNdermarrje; }
            set { idNdermarrje = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (DateTime) Merr ose jep vlere dates se krijimit te karakteristikes.
        /// </summary>
        public DateTime DtKrijimi
        {
            get { return dtKrijimi; }
            set { dtKrijimi = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (DateTime) Merr ose jep vlere dates se modifikimit te karakteristikes.
        /// </summary>
        public DateTime DtModifikimi
        {
            get { return dtModifikimi; }
            set { dtModifikimi = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se perdoruesit qe modifikon karakteristiken e standartit.
        /// </summary>
        public int IdPerdoruesi
        {
            get { return idPerdoruesi; }
            set { idPerdoruesi = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se krijuesit qe e krijon per here te pare karakteristiken e standartit.
        /// </summary>
        public int IdKrijuesi
        {
            get { return idKrijuesi; }
            set { idKrijuesi = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (colKarakteristikaStandartiTrupi) Merr ose jep vlere trupit te karakteristikave.
        /// </summary>
        public colKarakteristikaStandartiTrupi ColTrupi
        {
            get { return colTrupi; }
            set { colTrupi = value; }
        }

        #endregion

        #region Konstruktori

        /// <summary>
        /// MODULI ASETE:
        /// Krijon objektin bosh te klases clsKarakteristikaStandarti per karakteristikat e kokes se standarteve.
        /// </summary>
        public clsKarakteristikaStandarti()
        {
            colTrupi = new colKarakteristikaStandartiTrupi();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idStandarti"></param>
        /// <param name="idKodifikimArtikulli"></param>
        /// <param name="kerkoNeDb">percakton nese do kerkohet te merret nga db standarti i x kodifikimi apo jo,rasti qe nuk merret do te thote qe nuk 
        /// eshte me kontabilizim,eshte bere qellimisht per te marre vetem ato qe jane me kontabilizim pasi perdoret vetem ne kontabilitet.</param>
        /// <param name="db"></param>
        public clsKarakteristikaStandarti(int idStandarti,int idKodifikimArtikulli, int idNdermarrje, bool kerkoNeDb, clsDatabazeAsete db)
        {
            mbushKarakteristikaObjekt(db.TransCache.getKarakteristikaStandarti(idStandarti, idKodifikimArtikulli, idNdermarrje, kerkoNeDb, db));
            if(colTrupi == null)
                colTrupi = new colKarakteristikaStandartiTrupi();
        }

        public clsKarakteristikaStandarti(int idGrup, int idStandart, int idNdermarrja)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            mbushKarakteristikaObjekt(moduliAsete.ktheKonfigurimStandartiTeNdermarrjesSipasStatGrupStandart(idGrup, idStandart, idNdermarrja));
            colTrupi = new colKarakteristikaStandartiTrupi();
        }

        /// <summary>
        /// Konstruktori me parametra te plote.
        /// </summary>
        /// <param name="idKarakteristika">(int) Id e karakteristikes.</param>
        /// <param name="idStandart">(int) Id e standartit.</param>
        /// <param name="idKodifikimArtikulli">(int) Id e kodifikimit.</param>
        /// <param name="idKonfig">(int) Id e konfigurimit.</param>
        /// <param name="idFillimAmortizimi">(int) Id e fillimit te amortizimit.</param>
        /// <param name="idMbarimAmortizimi">(int) Id e mbarimit te amortizimit.</param>
        /// <param name="perfshihetDitaPare">(bool) Perfshihet dita e pare.</param>
        /// <param name="kontabilizim">(bool) Me kontabilizim.</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes.</param>
        /// <param name="idStatusDokumenti">(int) Id e statusit te dokumentit.</param>
        /// <param name="idPerdoruesi">(int) Id e perdoruesit.</param>
        /// <param name="idKrijuesi">(int) Id e krijuesit.</param>
        /// <param name="colTrupi">(colKarakteristikaStandartiTrupi) Trupi i dokumentit</param>
        public clsKarakteristikaStandarti(int idKarakteristika, int idStandart, int idKodifikimArtikulli, int idKonfig, int idFillimAmortizimi, int idMbarimAmortizimi, bool perfshihetDitaPare, bool kontabilizim, int idNdermarrje, int idStatusDokumenti, int idPerdoruesi, int idKrijuesi, colKarakteristikaStandartiTrupi colTrupi)
        {
            this.idKarakteristika = idKarakteristika;
            this.idStandart = idStandart;
            this.idKodifikimArtikulli = idKodifikimArtikulli;
            this.idKonfig = idKonfig;
            this.idFillimAmortizimi = idFillimAmortizimi;
            this.idMbarimAmortizimi = idMbarimAmortizimi;
            this.perfshihetDitaPare = perfshihetDitaPare;
            this.kontabilizim = kontabilizim;
            this.idNdermarrje = idNdermarrje;
            this.idStatusDokumenti = idStatusDokumenti;
            this.idPerdoruesi = idPerdoruesi;
            this.idKrijuesi = idKrijuesi;
            this.colTrupi = colTrupi;
            clsMesazh mesazh = kontrolloKarakteristike();
            if (!mesazh.Status)
                throw new Exception(mesazh.PershkrimMesazhi);
        }

        public clsKarakteristikaStandarti(DataRow rreshti)
        {
            mbushKarakteristikaObjekt(rreshti);
            colTrupi = new colKarakteristikaStandartiTrupi();
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// MODULI ASETE:
        /// Metoda perdoret per te mbushur te gjitha atributet e objektit clsKarakteristikaStandarti sipas te dhenave qe vijne nga cdo rresht i databazes nga tabela T_ASETE_LIDHJE_STANDART_STATUSMAG.
        /// </summary>
        /// <param name="dbDataRowKarakteristika">(DataRow) Merr si parameter vetem nje rresht te kthyer nga tabelat ne databaze per te mbushur nje objekt.</param>
        /// <returns>(bool) Kthen true nese nuk ndodh asnje gabim gjate leximit te te dhenave, ne te kundert false.</returns>
        internal bool mbushKarakteristikaObjekt(DataRow dbDataRowKarakteristika)
        {
            if (dbDataRowKarakteristika == null)
                return false;
            try
            {
                int.TryParse(dbDataRowKarakteristika["ID_STANDART_STATUSMAG"].ToString(), out idKarakteristika);
                int.TryParse(dbDataRowKarakteristika["IDSTANDARTI"].ToString(), out idStandart);
                int.TryParse(dbDataRowKarakteristika["IDKODIFIKIMARTIKULLI"].ToString(), out idKodifikimArtikulli);
                int.TryParse(dbDataRowKarakteristika["IDKONFIG"].ToString(), out idKonfig);
                int.TryParse(dbDataRowKarakteristika["IDFILLIMIAMORTIZIMMUJOR"].ToString(), out idFillimAmortizimi);
                int.TryParse(dbDataRowKarakteristika["IDMBARIMAMORTIZMIMUJOR"].ToString(), out idMbarimAmortizimi);
                bool.TryParse(dbDataRowKarakteristika["PERFSHIHET_DITA_PARE"].ToString(), out perfshihetDitaPare);
                bool.TryParse(dbDataRowKarakteristika["KONTABILIZIM"].ToString(), out kontabilizim);
                int.TryParse(dbDataRowKarakteristika["IDNDERMARRJE"].ToString(), out idNdermarrje);
                int.TryParse(dbDataRowKarakteristika["IDSTATUSDOK"].ToString(), out idStatusDokumenti);
                DateTime.TryParse(dbDataRowKarakteristika["DTKRIJIMI"].ToString(), out dtKrijimi);
                DateTime.TryParse(dbDataRowKarakteristika["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                int.TryParse(dbDataRowKarakteristika["IDPERDORUESI"].ToString(), out idPerdoruesi);
                int.TryParse(dbDataRowKarakteristika["IDKRIJUESI"].ToString(), out idKrijuesi);
                return true;
            }
            catch (InvalidCastException)
            {
                throw new Exception("ERROR: Gabim gjate marrjes se karakteristikave te kokes se standartit nga db-ja");
            }
        }
        internal void mbushKarakteristikaObjekt(clsKarakteristikaStandarti karakteristika)
        {
            IdKarakteristika = karakteristika.IdKarakteristika;
            IdKodifikimArtikulli = karakteristika.IdKodifikimArtikulli;
            IdStandart = karakteristika.IdStandart;
            Kontabilizim = karakteristika.kontabilizim;
            idKonfig = karakteristika.IdKonfig;
            idFillimAmortizimi = karakteristika.idFillimAmortizimi;
            idMbarimAmortizimi = karakteristika.IdMbarimAmortizimi;
            perfshihetDitaPare = karakteristika.perfshihetDitaPare;
            idNdermarrje = karakteristika.idNdermarrje;
            idStatusDokumenti = karakteristika.idStatusDokumenti;
            dtKrijimi = karakteristika.dtKrijimi;
            dtModifikimi = karakteristika.DtModifikimi;
            idPerdoruesi = karakteristika.idPerdoruesi;
            idKrijuesi = karakteristika.IdKrijuesi;

        }
        #endregion

        #region Metoda Publike

        /// <summary>
        /// MODULI ASETE:
        /// Ruan konfigurimin e standartit sipas karakteristikave nese konfigurimi me ato karakteristika nuk eshte i krijuar njehere.
        /// </summary>
        /// <returns>Kthen clsMesazh me atribut statusi True nese ruajtja kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        public clsMesazh ruaj()
        {
            DbInventari.colKodifikimeArtikulli col = new DbInventari.colKodifikimeArtikulli();
            if (idKodifikimArtikulli == 0)//te gjitha
            {
                col.mbushGjitheKodifikimetArtikulliSipasNdermarrjesLlojitNiveli1(idNdermarrje, 1, true);//kodifikimet e grupimit 1 te aqt;
            }
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            moduliAsete.beginTransaksion();
            clsMesazh pergjigja = new clsMesazh();
            if (col.Count == 0)
            {
                pergjigja = ruajKarakteristike(moduliAsete);
                if (!pergjigja.Status)
                {
                    moduliAsete.rollbackTransaksion();
                    return pergjigja;
                }
            }
            else
            {
                foreach (DbInventari.clsKodifikimArtikulli kod in col)
                {
                    idKodifikimArtikulli = kod.IdKodifikimi;
                    pergjigja = ruajKarakteristike(moduliAsete);
                    if (!pergjigja.Status)
                    {
                        moduliAsete.rollbackTransaksion();
                        return pergjigja;
                    }
                }
            }

            moduliAsete.commitTransaksion();

            return pergjigja;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Modifikon objektin e konfigurimit te standartit nese konfigurimi me ato karakteristika eshte i krijuar njehere.
        /// </summary>
        /// <returns>Kthen True nese modifikimi perfundon me sukses, ose False nese modifikimi jep gabim ose nuk ekziston njehere konfigurimi me ato karakteristika per standartin.</returns>
        public clsMesazh modifiko()
        {
            if (kontrolloEkzistonKonfigurimiStandartit(idKodifikimArtikulli, idStandart, idNdermarrje))
            {
                clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
                moduliAsete.beginTransaksion();
                clsMesazh pergjigja = modifikoKarakteristikaTransaksion(moduliAsete);
                if (pergjigja.Status)
                    moduliAsete.commitTransaksion();
                else
                    moduliAsete.rollbackTransaksion();
                return pergjigja;
            }
            return new clsMesazh(false, "Nuk ekziston nje rregull me kete standart dhe kete grup.");
        }

        /// <summary>
        /// MODULI ASETE:
        /// Fshin objektin e konfigurimit te standartit nese karakteristika eshte i krijuar njehere dhe nuk ka lidhje me objekte te tjera qe varen nga ekzistenca e objektit qe po fshihet.
        /// </summary>
        /// <returns>Kthen clsMesazh me atribut statusi True nese fshirja kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        public clsMesazh fshi()
        {
            if (kontrolloEkzistonKonfigurimiStandartit(idKodifikimArtikulli, idStandart, idNdermarrje))
            {
                clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
                moduliAsete.beginTransaksion();
                clsMesazh pergjigja = fshiKarakteristikaTransaksion(moduliAsete);
                if (pergjigja.Status)
                    moduliAsete.commitTransaksion();
                else
                    moduliAsete.rollbackTransaksion();
                return pergjigja;
            }
            return new clsMesazh(false, "Nuk ekziston nje rregull me kete standart dhe kete grup."); ;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Fshin karakteristikat duke i ndryshuar statusin.
        /// </summary>
        /// <param name="idkarakteristika">(int) Id automatike e karakteristikave te standartit.</param>
        /// <param name="idperdoruesi">(int) Id e perdoruesit qe ka modifikuar per here te fundit konfigurimin e standartit.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese fshirja kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        public static clsMesazh fshi(int idkarakteristika, int idperdoruesi)
        {
            clsDatabazeAsete data = new clsDatabazeAsete();
            clsMesazh u_fshi = data.fshiKonfigurimStandartiStatus(idkarakteristika, idperdoruesi);
            data.Dispose();
            return u_fshi;
        }

        /// <summary>
        /// Mbush nje DataRow me konfigurimin e standartit. Perdorur me emra konvencional ne store procedure.
        /// </summary>
        /// <param name="id">(int) Id automatike e karakteristikave te standartit.</param>
        /// <returns>Kthen nje DataRow me karakteristikat e standartit,</returns>
        public static DataRow merrSipasNjesiNdermarrjesDR(int id)
        {
            clsDatabazeAsete db = new clsDatabazeAsete();
            DataRow dr = db.merrSipasKonfigurimiStandartitDR(id);
            db.Dispose();
            return dr;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr objektin e konfigurimit te standartit sipas id automatike te konfigurimit.
        /// </summary>
        /// <param name="idKonfigurimStandarti">(int) Id e konfigurimit te standartit qe kerkojme.</param>
        /// <returns>Kthen True nese nuk ndodh asnje gabim gjate marrjes se konfigurimit te standartit ose False ne te kundert.</returns>
        public bool merrKonfigurimStandartiSipasID(int idKonfigurimStandarti)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            bool pergjigje = mbushKarakteristikaObjekt(moduliAsete.ktheKonfigurimStandartiSipasID(idKonfigurimStandarti));
            moduliAsete.Dispose();
            return pergjigje;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr objektin e konfigurimit te standartit sipas karakteristika specifike ne ndermarrjen ne perdorim.
        /// </summary>
        /// <param name="idGrup">(int) Id e grupit per te cilen do te merret konfigurimi i standartit.</param>
        /// <param name="idStandart">(int) Id e standartit per te cilen kerkojme konfigurimin e standartit.</param>
        /// <param name="idNdermarrja">(int) Id e ndermarrjes ne perdorim per te cilen kerkojme konfigurimi i standartit.</param>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen True nese nuk ndodh asnje gabim gjate marrjes se konfigurimit te standartit ose False ne te kundert.</returns>
        public bool merrKonfigurimStandartiTeNdermarrjesSipasStatGrupStandart(int idGrup, int idStandart, int idNdermarrja, clsDatabazeAsete moduliAsete)
        {
            return mbushKarakteristikaObjekt(moduliAsete.ktheKonfigurimStandartiTeNdermarrjesSipasStatGrupStandart(idGrup, idStandart, idNdermarrja));
        }

        /// <summary>
        /// MODULI ASETE:
        /// Konverton daten sipas kerkesave te konfigurimit te standartit.
        /// </summary>
        /// <param name="dataPerKonvertim">(DateTime) Data e dokumentit qe duhet te konvertohet sipas karakteristikave.</param>
        /// <param name="dateFillimi">(bool) True tregon se duhet kontrolluar karakteristikat per datat e fillimit te amortizimit, False per data e mbarimit te amortizimit.</param>
        /// <returns>Kthen daten e konvertuar sipas rregullave.</returns>
        public DateTime konvertoDatenSipasKerkesesKonfigurimit(DateTime dataPerKonvertim, bool dateFillimi, bool shikoDatenSipasPeriudhes)
        {
            if (!shikoDatenSipasPeriudhes)
                return dataPerKonvertim;

            clsPeriudhaLlogaritje periudhaFillimit = new clsPeriudhaLlogaritje();

            //Merr si duhet konvertuar data nese eshte fillim apo mbarim amortizimi.
            if (dateFillimi)
                periudhaFillimit.merrPeriudhaLlogaritjeSipasID(idFillimAmortizimi);
            else
                periudhaFillimit.merrPeriudhaLlogaritjeSipasID(idMbarimAmortizimi);

            //Konverton daten sipas identifikim qe eshte bere per konfigurimin e standartit perkates.
            if (clsPeriudhaLlogaritje.DATEDOK == periudhaFillimit.Emertimi)
                return dataPerKonvertim;
            else if (clsPeriudhaLlogaritje.DITAPAREMUAJKORRENT == periudhaFillimit.Emertimi)
                return new DateTime(dataPerKonvertim.Year, dataPerKonvertim.Month, 1);
            else if (clsPeriudhaLlogaritje.DITAPAREMUAJIPASARDHES == periudhaFillimit.Emertimi)
            {
                DateTime ditaEPareEMuajit = new DateTime(dataPerKonvertim.Year, dataPerKonvertim.Month, 1);
                return ditaEPareEMuajit.AddMonths(1);
            }
            else if (clsPeriudhaLlogaritje.DITAFUNDITMUAJIKORRENT == periudhaFillimit.Emertimi)
            {
                DateTime ditaEPareEMuajit = new DateTime(dataPerKonvertim.Year, dataPerKonvertim.Month, 1);
                return ditaEPareEMuajit.AddMonths(1).AddDays(-1);
            }
            else if (clsPeriudhaLlogaritje.DITAFUNDITMUAJIPARAARDHES == periudhaFillimit.Emertimi)
            {
                DateTime ditaEPareEMuajit = new DateTime(dataPerKonvertim.Year, dataPerKonvertim.Month, 1);
                return ditaEPareEMuajit.AddDays(-1);
            }

            return dataPerKonvertim;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr id e konfigurimit te standartit ne ndermarrjen ne perdorim sipas karakteristika te standartit.
        /// </summary>
        /// <param name="idGrup">(int) Id e grupit per te cilen do te merret konfigurimi i standartit.</param>
        /// <param name="idStandart">(int) Id e standartit per te cilen kerkojme konfigurimin e standartit.</param>
        /// <param name="idNdermarrja">(int) Id e ndermarrjes ne perdorim per te cilen kerkojme konfigurimi i standartit.</param>
        /// <returns>Kthen id e konfigurimit te standartit nese gjendet, ne te kundert kthen -1.</returns>
        public static int merrIDKonfigurimStandartiTeNdermarrjesSipasStatGrupStandart(int idGrup, int idStandart, int idNdermarrja)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            int pergjigje = moduliAsete.ktheIDKonfigurimStandartiTeNdermarrjesSipasStatGrupStandart(idGrup, idStandart, idNdermarrja);
            moduliAsete.Dispose();
            return pergjigje;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kontrollon nese konfigurimi i standartit ne ndermarrjen ne perdorim sipas karakteristikave specifike ekziston njehere i ruajtur ne bazen e te dhenave.
        /// </summary>
        /// <param name="idGrup">(int) Id e grupit per te cilen do te merret konfigurimi i standartit.</param>
        /// <param name="idStandart">(int) Id e standartit per te cilen kerkojme konfigurimin e standartit.</param>
        /// <param name="idNdermarrja">(int) Id e ndermarrjes ne perdorim per te cilen kerkojme konfigurimi i standartit.</param>
        /// <returns>Kthen True nese ekziston njehere konfigurimi i standartit per specifikat karakteristike dhe False ne te kundert.</returns>
        public static bool kontrolloEkzistonKonfigurimiStandartit(int idGrup, int idStandart, int idNdermarrja)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            bool pergjigje = moduliAsete.ekzistonKonfigurimiStandartit(idGrup, idStandart, idNdermarrja);
            moduliAsete.Dispose();
            return pergjigje;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kontrollon nese ekziston nje ose me shume konfigurimeve standartesh sipas id se standartit qe kerkojme te ruajtura ne bazen e te dhenave.
        /// </summary>
        /// <param name="idStandart">(int) Id e standartit per te cilen kerkojme konfigurimin e standartit.</param>
        /// <returns>Kthen True nese ekziston nje ose me shume konfigurime standartesh me id e standartit te kerkuar dhe False ne te kundert.</returns>
        public static bool kontrolloEkzistonKonfigurimiStandartiSipasIdStandarti(int idStandart)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            bool pergjigje = moduliAsete.ekzistonKonfigurimiStandartitSipasIdStandart(idStandart);
            moduliAsete.Dispose();
            return pergjigje;
        }

        ///// <summary>
        ///// MODULI ASETE:
        ///// Merr konfigurimin e standartit nga id e artikullit.
        ///// </summary>
        ///// <param name="idArtikulli">(int) Id e artikullit.</param>
        ///// <param name="idStandarti">(int) Id e standartit.</param>
        ///// <param name="idNdermarrje">(int) Id e ndermarrjes.</param>
        ///// <param name="dbasete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        ///// <returns>Kthen objektin clsKarakteristikaStandarti te mbushur.</returns>
        //public static clsKarakteristikaStandarti merrKonfigurimStandartiNgaIdArtikulli(int idArtikulli, int idStandarti, int idNdermarrje, clsDatabazeAsete dbasete)
        //{
        //    DbInventari.clsArtikulli artikulli = new DbInventari.clsArtikulli();
        //    DbInventari.clsDatabaseInventari dbinv = new DbInventari.clsDatabaseInventari(dbasete );

        //    if (!artikulli.mbushArtikull(idArtikulli, dbinv))
        //        return null;
        //    return merrKonfigurimStandarti(DbInventari.clsKodifikimArtikulli.ktheIdPrindiFillestar(artikulli.Kodifikimi1Artikulli,dbinv), idStandarti, idNdermarrje, dbasete);
        //}

        /// <summary>
        /// MODULI ASETE:
        /// Merr konfigurimin e standartit nga id e artikullit.
        /// </summary>
        /// <param name="idArtikulli">(int) Id e artikullit.</param>
        /// <param name="idStandarti">(int) Id e standartit.</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes.</param>
        /// <param name="dbasete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen objektin clsKarakteristikaStandarti te mbushur.</returns>
        public static clsKarakteristikaStandarti merrKonfigurimStandarti(int idPrindFillestar, int idStandarti, int idNdermarrje)
        {
            //Merr karakteristikat e standartit dhe statusit qe po behet veprimi
            clsKarakteristikaStandarti karakteristikaStandarti = new clsKarakteristikaStandarti(idPrindFillestar, idStandarti, idNdermarrje);            

            if (karakteristikaStandarti.idKarakteristika == 0)
                return null;

            return karakteristikaStandarti;
        }

        public static clsKarakteristikaStandarti KrijoBasic(IDataRecord rec)
        {
            var karak = new clsKarakteristikaStandarti();
            karak.IdKarakteristika =int.Parse(rec["IdKaraketeristika"].ToString());
            karak.idKodifikimArtikulli = int.Parse(rec["idKodifikimi"].ToString());
            karak.IdStandart = int.Parse(rec["IdStandart"].ToString());
            karak.kontabilizim = true;
            return karak;
        }
        

        #endregion

        #region Metoda Private

        /// <summary>
        /// MODULI ASETE:
        /// Kontrollon nese a jane te plotesuar fushe kritike te klases.
        /// </summary>
        /// <returns>Kthen clsMesazh me atribut statusi True nese kontrolli kalon me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        private clsMesazh kontrolloKarakteristike()
        {

            if (IdStandart == 0)
                return new clsMesazh(false, "Plotesoni standartin!");
            if (idFillimAmortizimi == 0)
                return new clsMesazh(false, "Plotesoni daten e fillimit te amortizimit!");
            if (idMbarimAmortizimi == 0)
                return new clsMesazh(false, "Plotesoni daten e mbarimit te amortizimit!");

            return new clsMesazh(true, "Kontrollet e rregullit u kaluan me sukses");
        }

        /// <summary>
        /// MODULI ASETE:
        /// Ruan transaksionin e karakteristikave te standartit pasi kontrollon nese ekziston apo jo karakteristika.
        /// </summary>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen True nese nuk ndodh asnje gabim gjate ruajtjes si te kokes dhe te trupit, ne te kundert kthen False</returns>
        private clsMesazh ruajKarakteristike(clsDatabazeAsete moduliAsete)
        {
            clsMesazh pergjigja = new clsMesazh();
            if (!moduliAsete.ekzistonKonfigurimiStandartit(idKodifikimArtikulli, idStandart, idNdermarrje))
            {
                pergjigja = ruajKarakteristikaTransaksion(moduliAsete);
                if (!pergjigja.Status)
                {
                    return pergjigja;
                }
            }
            else
            {
                return new clsMesazh(false, "Ekziston nje rregull me kete standart dhe kete grup.");
            }
            return pergjigja;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Ruan transaksionin e karakteristikave te standartit
        /// </summary>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen True nese nuk ndodh asnje gabim gjate ruajtjes si te kokes dhe te trupit, ne te kundert kthen False</returns>
        private clsMesazh ruajKarakteristikaTransaksion(clsDatabazeAsete moduliAsete)
        {
            clsMesazh pergjigja = moduliAsete.ruajKonfigurimStandarti(out idKarakteristika, idStandart, idKodifikimArtikulli, idKonfig, idFillimAmortizimi, idMbarimAmortizimi,
                perfshihetDitaPare, kontabilizim, idNdermarrje, idStatusDokumenti, idPerdoruesi, idKrijuesi);
            if (pergjigja.Status)
            {
                foreach (clsKarakteristikaStandartiTrupi rreshtiKarakteristikaTrupi in colTrupi)
                {
                    rreshtiKarakteristikaTrupi.IdKokaKarakteristikStandart = idKarakteristika;
                    pergjigja = rreshtiKarakteristikaTrupi.ruaj(moduliAsete);
                    if (!pergjigja.Status)
                        return pergjigja;
                }
            }
            return pergjigja;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Modifikon transaksionin e karakteristikave te standartit
        /// </summary>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen True nese nuk ndodh asnje gabim gjate modifikimit si te kokes dhe te trupit, ne te kundert kthen False</returns>
        private clsMesazh modifikoKarakteristikaTransaksion(clsDatabazeAsete moduliAsete)
        {
            clsMesazh pergjigja = moduliAsete.modifikimiKonfigurimStandarti(idKarakteristika, idFillimAmortizimi, idMbarimAmortizimi, perfshihetDitaPare, kontabilizim, idStatusDokumenti, idPerdoruesi);
            if (pergjigja.Status)
            {
                pergjigja = moduliAsete.fshiKonfigurimTrupiStandarti(idKarakteristika);
                if (!pergjigja.Status)
                    return pergjigja;
                foreach (clsKarakteristikaStandartiTrupi rreshtiKarakteristikaTrupi in colTrupi)
                {
                    rreshtiKarakteristikaTrupi.IdKokaKarakteristikStandart = idKarakteristika;
                    pergjigja = rreshtiKarakteristikaTrupi.ruaj(moduliAsete);
                    if (!pergjigja.Status)
                        return pergjigja;
                }
            }
            return pergjigja;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Fshin objektin e konfigurimit te standartit nese karakteristika eshte i krijuar njehere dhe nuk ka lidhje me objekte te tjera qe varen nga ekzistenca e objektit qe po fshihet.
        /// </summary>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen True nese nuk ndodh asnje gabim gjate fshirjes si te kokes dhe te trupit, ne te kundert kthen False</returns>
        private clsMesazh fshiKarakteristikaTransaksion(clsDatabazeAsete moduliAsete)
        {
            clsMesazh pergjigja = moduliAsete.fshiKonfigurimTrupiStandarti(idKarakteristika);
            if (pergjigja.Status)
            {
                pergjigja = moduliAsete.fshiKonfigurimStandarti(idKarakteristika);
            }
            return pergjigja;
        }

        #endregion
    }
}
