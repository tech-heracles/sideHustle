using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DbCore.DbAsete
{
    /// <summary>
    /// MODULI ASETE:
    /// Klase e ndertuar per te mbajtur standartet e ndryshme te llogaritjes se amortizimit ne modulin e aseteve. Kjo do te thote qe nje kompani mund te mbaje me shume se nje standart amortizimi.
    /// Te dhenat merret nga tabela T_ASETE_STANDART_AMORT
    /// </summary>
    public class clsStandarteAmortizim
    {
        #region Atribute

        private int idStandarti;
        private string emertimi;
        private string pershkrimi;
        private int idStatusDokumenti;
        private int idNdermarrja;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private int idPerdoruesi;
        private int idKrijuesi;
        private DataRow rreshti;

        #endregion

        #region Properties

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id automatike te standartit te amortizimit
        /// </summary>
        public int IdStandarti
        {
            get { return idStandarti; }
            set { idStandarti = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (string) Merr ose jep vlere emrit te standartit te amortizimit.
        /// </summary>
        public string Emertimi
        {
            get { return emertimi; }
            set { emertimi = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (string) Merr ose jep vlere pershkrimit te standartit te amortizimit.
        /// </summary>
        public string Pershkrimi
        {
            get { return pershkrimi; }
            set { pershkrimi = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere se ne cfare gjendje eshte standarti i amortizmit, i ruajtur, fshire, apo modifikuar.
        /// </summary>
        public int IdStatusDokumenti
        {
            get { return idStatusDokumenti; }
            set { idStatusDokumenti = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se ndermarrjes ne te cilen eshte krijuar standarti i amortizimit.
        /// </summary>
        public int IdNdermarrja
        {
            get { return idNdermarrja; }
            set { idNdermarrja = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (DateTime) Merr ose jep vlere dates se krijimit te standartit.
        /// </summary>
        public DateTime DtKrijimi
        {
            get { return dtKrijimi; }
            set { dtKrijimi = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (DateTime) Merr ose jep vlere dates se modifikimit te standartit.
        /// </summary>
        public DateTime DtModifikimi
        {
            get { return dtModifikimi; }
            set { dtModifikimi = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se perdoruesit qe modifikon dokumentin i fundit.
        /// </summary>
        public int IdPerdoruesi
        {
            get { return idPerdoruesi; }
            set { idPerdoruesi = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se krijuesit te standartit te amortizimit.
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
        /// Krijon objektin bosh te klases clsStandarteAmortizim per standartet e amortizimit.
        /// </summary>
        public clsStandarteAmortizim()
        {
        }

        /// <summary>
        /// MODULI ASETE:
        /// Konstruktori me nje parameter.
        /// </summary>
        /// <param name="idStandarti">(int) Id e standartit te amortizimit qe kerkojme.</param>
        public clsStandarteAmortizim(int idStandarti)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            bool pergjigje = mbushStandarteAmortizimiObjekt(moduliAsete.ktheStandartinAmortizimitSipasID(idStandarti));
            moduliAsete.Dispose();
        }

        /// <summary>
        /// MODULI ASETE:
        /// Konstruktori me nje parameter ne transaksion.
        /// </summary>
        /// <param name="idStandarti">(int) Id e standartit te amortizimit qe kerkojme.</param>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        public clsStandarteAmortizim(int idStandarti,  clsDatabazeAsete moduliAsete)
        {

            bool pergjigje = mbushStandarteAmortizimiObjekt(moduliAsete.ktheStandartinAmortizimitSipasID(idStandarti));
  
        }

        public clsStandarteAmortizim(DataRow rreshti)
        {
            
            mbushStandarteAmortizimiObjekt(rreshti);
        } 

        #endregion

        #region Metoda Internal

        /// <summary>
        /// MODULI ASETE:
        /// Metoda perdoret per te mbushur te gjitha atributet e objektit clsStandarteAmortizim sipas te dhenave qe vijne nga cdo rresht i databazes nga tabela T_ASETE_STANDART_AMORT.
        /// </summary>
        /// <param name="dbDataRowStandartAmortizimi">(DataRow) Merr si parameter vetem nje rresht te kthyer nga tabelat ne databaze per te mbushur nje objekt.</param>
        /// <returns>(bool) Kthen true nese nuk ndodh asnje gabim gjate leximit te te dhenave, ne te kundert false.</returns>
        internal bool mbushStandarteAmortizimiObjekt(DataRow dbDataRowStandartAmortizimi)
        {
            if (dbDataRowStandartAmortizimi == null)
                return false;
            try
            {
                int.TryParse(dbDataRowStandartAmortizimi["ID_STANDARTI"].ToString(), out idStandarti);
                emertimi = dbDataRowStandartAmortizimi["EMERTIMI"].ToString();
                pershkrimi = dbDataRowStandartAmortizimi["PERSHKRIMI"].ToString();
                int.TryParse(dbDataRowStandartAmortizimi["IDSTATUSDOK"].ToString(), out idStatusDokumenti);
                int.TryParse(dbDataRowStandartAmortizimi["IDNDERMARJE"].ToString(), out idNdermarrja);
                DateTime.TryParse(dbDataRowStandartAmortizimi["DTKRIJIMI"].ToString(), out dtKrijimi);
                DateTime.TryParse(dbDataRowStandartAmortizimi["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                int.TryParse(dbDataRowStandartAmortizimi["IDKRIJUESI"].ToString(), out idPerdoruesi);
                int.TryParse(dbDataRowStandartAmortizimi["IDPERDORUESI"].ToString(), out idKrijuesi);
                return true;
            }
            catch (InvalidCastException)
            {
                throw new Exception("ERROR: Gabim gjate marrjes se standarteve te amortizimit nga db-ja");
            }
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// MODULI ASETE:
        /// Ruan objektin e standartit te amortizimi nese emertimi ne ndermarrjen ne perdorim nuk eshte i krijuar njehere.
        /// </summary>
        /// <returns>Kthen clsMesazh me atribut statusi True nese ruajtja kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        public clsMesazh ruaj()
        {
            if (!kontrolloEkzistonStandartiAmortizimit(emertimi, idNdermarrja))
            {
                clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
                clsMesazh pergjigja = moduliAsete.ruajStandartAmortizimi(out idStandarti, emertimi, pershkrimi, idStatusDokumenti, idNdermarrja, dtModifikimi, idPerdoruesi, idKrijuesi);
                moduliAsete.Dispose();
                return pergjigja;
            }
            return new clsMesazh(false, "Ekziston nje standart me kete emer!");
        }

        /// <summary>
        /// MODULI ASETE:
        /// Modifikon objektin e standartit te amortizimi nese emertimi ne ndermarrjen ne perdorim eshte i krijuar njehere.
        /// </summary>
        /// <returns>Kthen clsMesazh me atribut statusi True nese modifikimi kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        public clsMesazh modifiko()
        {
            if (kontrolloEkzistonStandartiAmortizimit(emertimi, idNdermarrja))
            {
                clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
                clsMesazh pergjigja = moduliAsete.modifikimiStandartAmortizimi(idStandarti, pershkrimi, idStatusDokumenti, idPerdoruesi);
                moduliAsete.Dispose();
                return pergjigja;
            }
            return new clsMesazh(false, "Nuk ekziston nje standart me kete emer!");
        }

        /// <summary>
        /// MODULI ASETE:
        /// Fshin objektin e standartit te amortizimi nese emertimi ne ndermarrjen ne perdorim eshte i krijuar njehere dhe nuk ka lidhje me objekte te tjera qe varen nga ekzistenca e objektit qe po fshihet.
        /// Objektet qe varen nga ekzistenca e statusit te magazines: clsKarakteristikaStandarti.
        /// </summary>
        /// <returns>Kthen clsMesazh me atribut statusi True nese fshirja kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        public clsMesazh fshi()
        {
            if (kontrolloEkzistonStandartiAmortizimit(emertimi, idNdermarrja) && !clsKarakteristikaStandarti.kontrolloEkzistonKonfigurimiStandartiSipasIdStandarti(idStandarti))
            {
                clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
                clsMesazh pergjigja = moduliAsete.fshiStandartAmortizimi(idStandarti);
                moduliAsete.Dispose();
                return pergjigja;
            }
            return new clsMesazh(false, "Nuk ekziston nje standart me kete emer!");
        }

        /// <summary>
        /// MODULI ASETE:
        /// Fshin standartin duke i ndryshuar statusin
        /// </summary>
        /// <param name="idstandarti">(int) Id e standartit te amortizimit qe kerkojme.</param>
        /// <param name="idperdoruesi">(int) Id e perdoruesit qe kryhen veprimin.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese fshirja kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        public static clsMesazh fshi(int idstandarti, int idperdoruesi)
        {
            clsDatabazeAsete data = new clsDatabazeAsete();
            clsMesazh u_fshi = data.fshiStandartAmortizimiStatus(idstandarti, idperdoruesi);
            data.Dispose();
            return u_fshi;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr objektin e standartit te amortizimit sipas id automatike te standartit.
        /// </summary>
        /// <param name="idStandarti">(int) Id e standartit te amortizimit qe kerkojme.</param>
        /// <returns>Kthen True nese nuk ndodh asnje gabim gjate marrjes se standartit te amortizimit ose False ne te kundert.</returns>
        public bool merrStandartinAmortizimitSipasID(int idStandarti)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            bool pergjigje = mbushStandarteAmortizimiObjekt(moduliAsete.ktheStandartinAmortizimitSipasID(idStandarti));
            moduliAsete.Dispose();
            return pergjigje;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr objektin e standartit te amortizimit sipas emertimit te standartit ne ndermarrjen ne perdorim.
        /// </summary>
        /// <param name="emertimi">(string) Emertimi i standartit te amortizimit qe kerkojme.</param>
        /// <param name="idNdermarrja">(int) Id e ndermarrjes ne perdorim per te cilen do te kthehet standarti i amortizimit.</param>
        /// <returns>Kthen True nese nuk ndodh asnje gabim gjate marrjes se standartit te amortizimit ose False ne te kundert.</returns>
        public bool merrStandartinAmortizimitTeNdermarrjesSipasEmertimi(string emertimi, int idNdermarrja)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            bool pergjigje = mbushStandarteAmortizimiObjekt(moduliAsete.ktheStandartinAmortizimitTeNdermarrjesSipasEmertimit(emertimi, idNdermarrja));
            moduliAsete.Dispose();
            return pergjigje;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr id e standartit te amortizimit sipas emertimit ne ndermarrjen ne perdorim.
        /// </summary>
        /// <param name="emertimi">(string) Emertimi i standartit te amortizimit qe kerkojme.</param>
        /// <param name="idNdermarrja">(int) Id e ndermarrjes ne perdorim per te cilen do te kthehet id e standartit te amortizimit.</param>
        /// <returns>Kthen id e standartit te amortizimit nese gjendet, ne te kundert kthen -1.</returns>
        public static int merrIDStandartinAmortizimitTeNdermarrjesSipasEmertimit(string emertimi, int idNdermarrja)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            int pergjigje = moduliAsete.ktheIDStandartinAmortizimitTeNdermarrjesSipasEmertimit(emertimi, idNdermarrja);
            moduliAsete.Dispose();
            return pergjigje;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr id e standartit te amortizimit sipas pershkrimin ne ndermarrjen ne perdorim.
        /// </summary>
        /// <param name="emertimi">(string) Emertimi i standartit te amortizimit qe kerkojme.</param>
        /// <param name="idNdermarrja">(int) Id e ndermarrjes ne perdorim per te cilen do te kthehet id e standartit te amortizimit.</param>
        /// <returns>Kthen id e standartit te amortizimit nese gjendet, ne te kundert kthen -1.</returns>
        public static int merrIDStandartinAmortizimitTeNdermarrjesSipasPershkrimit(string pershkrimi, int idNdermarrja)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            int pergjigje = moduliAsete.ktheIDStandartinAmortizimitTeNdermarrjesSipasPershkrimit(pershkrimi, idNdermarrja);
            moduliAsete.Dispose();
            return pergjigje;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kontrollon nese emertimi ne ndermarrjen ne perdorim ekziston njehere i ruajtur ne bazen e te dhenave.
        /// </summary>
        /// <param name="emertimi">(string) Emertimi i standartit te amortizimit qe kerkojme.</param>
        /// <param name="idNdermarrja">(int) Id e ndermarrjes ne perdorim per te cilen do te kontrollohet nese ekziston emertimi i standartit.</param>
        /// <returns>Kthen True nese ekziston njehere emertimi i kerkuar per ndermarrjen dhe False ne te kundert.</returns>
        public static bool kontrolloEkzistonStandartiAmortizimit(string emertimi, int idNdermarrja)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            bool pergjigje = moduliAsete.ekzistonStandartiAmortizimit(emertimi, idNdermarrja);
            moduliAsete.Dispose();
            return pergjigje;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kontrollon nese ka veprime te tjera me standartin e amortizimit.
        /// </summary>
        /// <param name="idstandarti">(int) Id e standartit te amortizimit.</param>
        /// <returns>Kthen True nese ekzistojne veprime me id e standartit te amortizimit te kerkuar per ndermarrjen dhe False ne te kundert.</returns>
        public static bool kaVeprimeStandartiAmortizimit(int idstandarti)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            bool pergjigje = moduliAsete.kaveprimeStandartiAmortizimit(idstandarti);
            moduliAsete.Dispose();
            return pergjigje;
        }

        #endregion
    }
}
