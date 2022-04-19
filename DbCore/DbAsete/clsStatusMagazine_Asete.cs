using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DbCore.DbAsete
{
    /// <summary>
    /// MODULI ASETE:
    /// Klase e ndertuar per te mbajtur objektin e statusit te magazines ne modulin e aseteve. Statuset e magazines varen nga ndermarrja.
    /// Te dhenat merret nga tabela T_ASETE_STATUS_MAGAZINE
    /// </summary>
    public class clsStatusMagazine_Asete
    {
        #region Atribute

        private int idStatusMagazine;
        private string emertimi;
        private string pershkrimi;
        private bool ePerdorshme;
        private int idNdermarrja;

        #endregion

        #region Properties

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id automatike te statusit te magazines
        /// </summary>
        public int IdStatusMagazine
        {
            get { return idStatusMagazine; }
            set { idStatusMagazine = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (string) Merr ose jep vlere emrit te statusit te magazines.
        /// </summary>
        public string Emertimi
        {
            get { return emertimi; }
            set { emertimi = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (string) Merr ose jep vlere pershkrimit te statusit te magazines.
        /// </summary>
        public string Pershkrimi
        {
            get { return pershkrimi; }
            set { pershkrimi = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (bool) Merr ose jep vlere nese statusi i magazines eshte i perdorshem nga ndermarrja apo jo.
        /// </summary>
        public bool EPerdorshme
        {
            get { return ePerdorshme; }
            set { ePerdorshme = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se ndermarrjes ne te cilen eshte statusi i magazines.
        /// </summary>
        public int IdNdermarrja
        {
            get { return idNdermarrja; }
            set { idNdermarrja = value; }
        }

        /// <summary>
        /// String default per gjendjen aktive te magazines
        /// </summary>
        public static readonly string AKTIVE = "Aktive";

        /// <summary>
        /// String default per gjendjen inaktive te magazines
        /// </summary>
        public static readonly string INAKTIVE = "Inaktive";

        /// <summary>
        /// String default per gjendjen riparim te magazines
        /// </summary>
        public static readonly string RIPARIM = "Riparim";

        /// <summary>
        /// String default per gjendjen deinstalim te magazines
        /// </summary>
        public static readonly string DEINSTALIM = "Deinstalim";
        private DataRow rreshti;

        #endregion

        #region Konstruktori

        /// <summary>
        /// MODULI ASETE:
        /// Krijon objektin bosh te klases clsStatusMagazine_Asete per statuset e magazines.
        /// </summary>
        public clsStatusMagazine_Asete()
        {
        }
        public clsStatusMagazine_Asete(int idstatus):this(idstatus,new  clsDatabazeAsete())
        {

        }
        public clsStatusMagazine_Asete(int idStatusMagazine,clsDatabazeAsete db)
        {
          
            mbushStatusMagazineObjekt(db.TransCache.getStatusMagazine(idStatusMagazine,db));
         
        }
        /// <summary>
        /// MODULI ASETE:
        /// Konstruktore me 1 parameter.
        /// </summary>
        /// <param name="emri">(string) Emertimi i statusit te magazinave qe kerkojme.</param>
        /// <param name="idndermarje">(int) Id e ndermarrjes ne perdorim per te cilen do te kontrollohet nese ekziston emertimi i statusit.</param>
        public clsStatusMagazine_Asete(string emri, int idndermarje)
        {
            DbCore.DbAsete.clsDatabazeAsete db = new clsDatabazeAsete();
            mbushStatusMagazineObjekt(db.ktheStatusMagazineTeNdermarrjesSipasEmertimitPaVaresishtEPerdorshme(emri, idndermarje));
            db.Dispose();
        }

        public clsStatusMagazine_Asete(DataRow rreshti)
        {
            
            mbushStatusMagazineObjekt(rreshti);
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// MODULI ASETE:
        /// Metoda perdoret per te mbushur te gjitha atributet e objektit clsStatusMagazine_Asete sipas te dhenave qe vijne nga cdo rresht i databazes nga tabela T_ASETE_STATUS_MAGAZINE.
        /// </summary>
        /// <param name="dbDataRowStatusMagazine">(DataRow) Merr si parameter vetem nje rresht te kthyer nga tabelat ne databaze per te mbushur nje objekt.</param>
        /// <returns>(bool) Kthen true nese nuk ndodh asnje gabim gjate leximit te te dhenave, ne te kundert false.</returns>
        internal bool mbushStatusMagazineObjekt(DataRow dbDataRowStatusMagazine)
        {
            if (dbDataRowStatusMagazine == null)
                return false;
            try
            {
                int.TryParse(dbDataRowStatusMagazine["ID_STATUS_MAGAZINE"].ToString(), out idStatusMagazine);
                emertimi = dbDataRowStatusMagazine["EMERTIMI"].ToString();
                pershkrimi = dbDataRowStatusMagazine["PERSHKRIMI"].ToString();
                bool.TryParse(dbDataRowStatusMagazine["E_PERDORSHME"].ToString(), out ePerdorshme);
                int.TryParse(dbDataRowStatusMagazine["IDNDERMARRJE"].ToString(), out idNdermarrja);
                return true;
            }
            catch (InvalidCastException)
            {
                throw new Exception("ERROR: Gabim gjate marrjes se statusit te magazines nga db-ja");
            }
        }
        internal void mbushStatusMagazineObjekt(clsStatusMagazine_Asete status)
        {
            this.idStatusMagazine = status.idStatusMagazine;
            this.emertimi = status.emertimi;
            this.pershkrimi = status.pershkrimi;
            this.ePerdorshme = status.ePerdorshme;
            this.idNdermarrja = status.idNdermarrja;
       
           
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// MODULI ASETE:
        /// Ruan statusin e magazines nese emertimi ne ndermarrjen ne perdorim nuk eshte i krijuar njehere.
        /// </summary>
        /// <returns>Kthen clsMesazh me atribut statusi True nese ruajtja kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        public clsMesazh ruaj()
        {
            if (!kontrolloEkzistonStatusMagazines(emertimi, idNdermarrja))
            {
                clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
                clsMesazh pergjigja = moduliAsete.ruajStatusMagazina(out idStatusMagazine, emertimi, pershkrimi, ePerdorshme, idNdermarrja);
                moduliAsete.Dispose();
                return pergjigja;
            }
            return new clsMesazh(false, "Ekziston nje status me kete emer!");
        }

        /// <summary>
        /// MODULI ASETE:
        /// Modifikon objektin e statusit te magazines nese emertimi ne ndermarrjen ne perdorim eshte i krijuar njehere.
        /// </summary>
        /// <returns>Kthen clsMesazh me atribut statusi True nese modifikimi kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        public clsMesazh modifiko()
        {
            if (kontrolloEkzistonStatusMagazines(emertimi, idNdermarrja))
            {
                clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
                clsMesazh pergjigja = moduliAsete.modifikimiStatusMagazina(idStatusMagazine, pershkrimi, ePerdorshme);
                moduliAsete.Dispose();
                return pergjigja;
            }
            return new clsMesazh(false, "Nuk ekziston nje status me kete emer!");
        }

        /// <summary>
        /// MODULI ASETE:
        /// Fshin objektin e statusit te magazines nese emertimi ne ndermarrjen ne perdorim eshte i krijuar njehere dhe nuk ka lidhje me objekte te tjera qe varen nga ekzistenca e objektit qe po fshihet.
        /// Objektet qe varen nga ekzistenca e statusit te magazines: clsKarakteristikaStandarti, clsHistorikStatusMagazine.
        /// </summary>
        /// <returns>Kthen clsMesazh me atribut statusi True nese fshirja kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        public clsMesazh fshi()
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            if (kontrolloEkzistonStatusMagazines(emertimi, idNdermarrja)
                && !clsHistorikStatusMagazine.kontrolloEkzistonHistorikMagazinaSipasIdStatusMagazine(0, idStatusMagazine)) //RIDI
            {

                clsMesazh pergjigja = moduliAsete.fshiStatusMagazina(idStatusMagazine);
                moduliAsete.Dispose();
                return pergjigja;
            }
            return new clsMesazh(false, "Nuk ekziston nje status me kete emer!");
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr objektin e statusit te magazines sipas id automatike te statusit.
        /// </summary>
        /// <param name="idStatusMagazine">(int) Id e statusit te magazines qe kerkojme.</param>
        /// <returns>Kthen True nese nuk ndodh asnje gabim gjate marrjes se statusit te magazines ose False ne te kundert.</returns>
        public bool merrStatusMagazineSipasID(int idStatusMagazine)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            bool pergjigje = mbushStatusMagazineObjekt(moduliAsete.ktheStatusMagazineSipasID(idStatusMagazine));
            moduliAsete.Dispose();
            return pergjigje;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr objektin e statusit te magazines sipas id automatike te statusit.
        /// </summary>
        /// <param name="idStatusMagazine">(int) Id e statusit te magazines qe kerkojme.</param>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen True nese nuk ndodh asnje gabim gjate marrjes se statusit te magazines ose False ne te kundert.</returns>
        public bool merrStatusMagazineSipasID(int idStatusMagazine, clsDatabazeAsete moduliAsete)
        {
            bool pergjigje = mbushStatusMagazineObjekt(moduliAsete.ktheStatusMagazineSipasID(idStatusMagazine));
            return pergjigje;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr objektin e statusit te magazines sipas emertimit te statusit ne ndermarrjen ne perdorim.
        /// </summary>
        /// <param name="emertimi">(string) Emertimi i statusit te magazinave qe kerkojme.</param>
        /// <param name="idNdermarrja">(int) Id e ndermarrjes ne perdorim per te cilen do te kontrollohet nese ekziston emertimi i statusit.</param>
        /// <returns>Kthen True nese nuk ndodh asnje gabim gjate marrjes se statusit te magazines ose False ne te kundert.</returns>
        public bool merrStatusMagazineTeNdermarrjesSipasEmertimit(string emertimi, int idNdermarrja)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            bool pergjigje = mbushStatusMagazineObjekt(moduliAsete.ktheStatusMagazineTeNdermarrjesSipasEmertimit(emertimi, idNdermarrja));
            moduliAsete.Dispose();
            return pergjigje;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr id e statusit te magazines sipas emertimit ne ndermarrjen ne perdorim.
        /// </summary>
        /// <param name="emertimi">(string) Emertimi i statusit te magazinave qe kerkojme.</param>
        /// <param name="idNdermarrja">(int) Id e ndermarrjes ne perdorim per te cilen do te kontrollohet nese ekziston emertimi i statusit.</param>
        /// <returns>Kthen id e statusit te magazines nese gjendet, ne te kundert kthen -1.</returns>
        public static int merrIDStatusMagazinesTeNdermarrjesSipasEmertimit(string emertimi, int idNdermarrja)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            int pergjigje = moduliAsete.ktheIDStatusMagazinesTeNdermarrjesSipasEmertimit(emertimi, idNdermarrja);
            moduliAsete.Dispose();
            return pergjigje;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr emertimin e statusit te magazines ne ndermarrjen ne perdorim.
        /// </summary>
        /// <param name="idStatusMagazine">(int) Id e statusit te magazines qe kerkojme.</param>
        /// <param name="idNdermarrja">(int) Id e ndermarrjes ne perdorim per te cilen do te kontrollohet nese ekziston emertimi i statusit.</param>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen emertimin e statusit te magazines nese gjendet, ne te kundert kthen -1.</returns>
        public static string merrEmertimStatusMagazinesTeNdermarrjesIDStatus(int idStatusMagazine, int idNdermarrja)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            string pergjigje = moduliAsete.ktheEmertimStatusMagazinesTeNdermarrjesSipasIDStatus(idStatusMagazine, idNdermarrja);
            return pergjigje;
        }

        /// <summary>
        /// Kontrollon nese emertimi ne ndermarrjen ne perdorim ekziston njehere i ruajtur ne bazen e te dhenave.
        /// </summary>
        /// <param name="emertimi">(string) Emertimi i statusit te magazinave qe kerkojme.</param>
        /// <param name="idNdermarrja">(int) Id e ndermarrjes ne perdorim per te cilen do te kontrollohet nese ekziston emertimi i statusit.</param>
        /// <returns>Kthen True nese ekziston njehere emertimi i kerkuar per ndermarrjen dhe False ne te kundert.</returns>
        public static bool kontrolloEkzistonStatusMagazines(string emertimi, int idNdermarrja)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            bool pergjigje = moduliAsete.ekzistonStatusMagazines(emertimi, idNdermarrja);
            moduliAsete.Dispose();
            return pergjigje;
        }

        #endregion
    }
}
