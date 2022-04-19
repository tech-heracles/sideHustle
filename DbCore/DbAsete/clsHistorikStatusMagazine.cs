using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Resources;
using DbCore.DbKontabiliteti;
using DbCore.IMBUtils.Messages;

namespace DbCore.DbAsete
{
    /// <summary>
    /// MODULI ASETE:
    /// Klase e ndertuar per te mbajtur objektin e statusit te magazines ne modulin e aseteve. Nje status magazine mund te ndryshohet dhe duhet te ruhet si historik statuset e meparshem.
    /// Te dhenat merret nga tabela T_ASETE_NJESIADMINISTRATIVE_HISTORIKU
    /// </summary>
    public class clsHistorikStatusMagazine
    {
        
#region Atribute

        private int idHistorikuNjesiAdministrative;
        private int idNjesiAdministrative;
        private int idStatusMagazine;
        private DateTime dataStatusit;
        private int idStatusDokumenti;
        private int idPerdoruesi;
        private int idKrijuesi;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private DataRow rreshti;

        #endregion
        #region Properties

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id automatike te historiku te njesise administrative.
        /// </summary>
        public int IdHistorikuNjesiAdministrative
        {
            get { return idHistorikuNjesiAdministrative; }
            set { idHistorikuNjesiAdministrative = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se njesise administrative qe po i ruhet historiku.
        /// </summary>
        public int IdNjesiAdministrative
        {
            get { return idNjesiAdministrative; }
            set { idNjesiAdministrative = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se cfare statusi po ruhet njesia administrative.
        /// </summary>
        public int IdStatusMagazine
        {
            get { return idStatusMagazine; }
            set { idStatusMagazine = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (DateTime) Merr ose jep vlere dates kur po behet ndryshimi i dates se statusit te njesise administrative.
        /// </summary>
        public DateTime DataStatusit
        {
            get { return dataStatusit; }
            set { dataStatusit = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere se ne cfare gjendje eshte historiku i njesise administrative, i ruajtur, fshire, apo modifikuar.
        /// </summary>
        public int IdStatusDokumenti
        {
            get { return idStatusDokumenti; }
            set { idStatusDokumenti = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (DateTime) Merr ose jep vlere dates se krijimit te historikut te njesise administrative.
        /// </summary>
        public DateTime DtKrijimi
        {
            get { return dtKrijimi; }
            set { dtKrijimi = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (DateTime) Merr ose jep vlere dates se modifikimit te historikut te njesise administrative.
        /// </summary>
        public DateTime DtModifikimi
        {
            get { return dtModifikimi; }
            set { dtModifikimi = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se perdoruesit qe modifikon historikun e njesise administrative.
        /// </summary>
        public int IdPerdoruesi
        {
            get { return idPerdoruesi; }
            set { idPerdoruesi = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se krijuesit qe e krijon per here te pare historikun e njesise administrative.
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
        /// Krijon objektin bosh te klases clsHistorikStatusMagazine per historikun e njesive administrative.
        /// </summary>
        public clsHistorikStatusMagazine()
        {
        }

        /// <summary>
        /// MODULI ASETE:
        /// Krijon objektin me parametra te klases clsHistorikStatusMagazine per historikun e njesive administrative.
        /// </summary>
        /// <param name="idHistorikuNjesiAdministrative">(int) Id e historikut te njesise administrative.</param>
        /// <param name="idNjesiAdministrative">(int) Id e njesise administrative.</param>
        /// <param name="idStatusMagazine">(int) Id e statusit te magazines.</param>
        /// <param name="dataStatusit">(DateTime) Data e nderrimit te statusit te magazines.</param>
        /// <param name="idStatusDokumenti">(int) Id e gjendjes se dokumentit.</param>
        /// <param name="idPerdoruesi">(int) Id e perdoruesit qe kryhen modifikimin i fundit.</param>
        /// <param name="idKrijuesi">(int) Id e krijuesit te statusit per here te pare.</param>
        public clsHistorikStatusMagazine(int idHistorikuNjesiAdministrative, int idNjesiAdministrative, int idStatusMagazine, DateTime dataStatusit, int idStatusDokumenti, int idPerdoruesi, int idKrijuesi)
        {
            this.idHistorikuNjesiAdministrative = idHistorikuNjesiAdministrative;
            this.idNjesiAdministrative = idNjesiAdministrative;
            this.idStatusMagazine = idStatusMagazine;
            this.dataStatusit = dataStatusit;
            this.idStatusDokumenti = idStatusDokumenti;
            this.idPerdoruesi = idPerdoruesi;
            this.idKrijuesi = idKrijuesi;
        }

        public clsHistorikStatusMagazine(DataRow rreshti)
        {
            
            mbushHistorikNjesiAdministrativeObjekt(rreshti);
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// MODULI ASETE:
        /// Metoda perdoret per te mbushur te gjitha atributet e objektit clsHistorikStatusMagazine sipas te dhenave qe vijne nga cdo rresht i databazes nga tabela T_ASETE_NJESIADMINISTRATIVE_HISTORIKU.
        /// </summary>
        /// <param name="dbDataRowHistoriku">(DataRow) Merr si parameter vetem nje rresht te kthyer nga tabelat ne databaze per te mbushur nje objekt.</param>
        /// <returns>(bool) Kthen true nese nuk ndodh asnje gabim gjate leximit te te dhenave, ne te kundert false.</returns>
        internal bool mbushHistorikNjesiAdministrativeObjekt(DataRow dbDataRowHistoriku)
        {
            if (dbDataRowHistoriku == null)
                return false;
            try
            {
                int.TryParse(dbDataRowHistoriku["ID_HISTORIKU_NJESI_ADMIN"].ToString(), out idHistorikuNjesiAdministrative);
                int.TryParse(dbDataRowHistoriku["ID_NJESI_ADMINISTRATIVE"].ToString(), out idNjesiAdministrative);
                int.TryParse(dbDataRowHistoriku["ID_STATUS_MAGAZINE"].ToString(), out idStatusMagazine);
                DateTime.TryParse(dbDataRowHistoriku["DATA_STATUSIT"].ToString(), out dataStatusit);
                int.TryParse(dbDataRowHistoriku["IDSTATUSDOK"].ToString(), out idStatusDokumenti);
                DateTime.TryParse(dbDataRowHistoriku["DTKRIJIMI"].ToString(), out dtKrijimi);
                DateTime.TryParse(dbDataRowHistoriku["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                int.TryParse(dbDataRowHistoriku["IDPERDORUESI"].ToString(), out idPerdoruesi);
                int.TryParse(dbDataRowHistoriku["IDKRIJUESI"].ToString(), out idKrijuesi);
                return true;
            }
            catch (InvalidCastException)
            {
                throw new Exception("ERROR: Gabim gjate marrjes se historikut te njesive administrative nga db-ja");
            }
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// MODULI ASETE:
        /// Ruan objektin e historikut te statusit te magazines nese magazina nuk eshte ne statusin qe duam te regjistrojme.
        /// </summary>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrje per te cilen do te kryhet ruajtja ne magazine.</param>
        /// <param name="idNderViti">(int) Id e ndermarrjes qe lidhet me vitin fiskal.</param>
        /// <param name="idperiudha">(int) Id e periudhes.</param>
        /// <param name="njesiadm">(DbRegjistrim.clsNjesiAdministrative) Objekti i njesise administrative.</param>
        /// <param name="idnivelgjenerues">(int) Id e nivelit gjenerues.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese ruajtja kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        public clsMesazh ruaj(int idNdermarrje, int idNderViti, int idperiudha, DbRegjistrim.clsNjesiAdministrative njesiadm, int idnivelgjenerues, bool magazineEre)
        {
            if (merrDateMagazineNgaHistorikMagazinaSipasIdNjesiAdministrative(idNjesiAdministrative) < dataStatusit)
            {
                return ruajStatusDokumentiTransaksion(idNdermarrje, idNderViti, idperiudha, njesiadm, idnivelgjenerues, magazineEre);                
            }
            return new clsMesazh(false, MessagesResource.Messages["msgStatusiNukIPlotesonKushtet"]);
        }

        ///// <summary>
        ///// MODULI ASETE:
        ///// Modifikon objektin e historikut te statusit te magazines nese e gjen te regjistruar.
        ///// </summary>
        ///// <param name="idNdermarrje">(int) Id e ndermarrje per te cilen do te kryhet modifikimi ne magazine.</param>
        ///// <param name="idPerdoruesi">(int) Id e perdoruesit qe po kryen veprimin</param>
        ///// <param name="idNderViti">(int) Id e ndermarrjes qe lidhet me vitin fiskal.</param>
        ///// <param name="idperiudha">(int) Id e periudhes.</param>
        ///// <param name="njesiadm">(DbRegjistrim.clsNjesiAdministrative) Objekti i njesise administrative.</param>
        ///// <param name="idnivelgjenerues">(int) Id e nivelit gjenerues.</param>
        ///// <returns>Kthen clsMesazh me atribut statusi True nese modifikimi kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        //public clsMesazh modifiko(int idNdermarrje, int idPerdoruesi, int idNderViti, int idperiudha, DbRegjistrim.clsNjesiAdministrative njesiadm, int idnivelgjenerues, ResourceManager rm, CultureInfo ci)
        //{
        //    clsDatabazeAsete moduliAsete = new clsDatabazeAsete();

        //    if (kontrolloEkzistonHistorikMagazinaSipasIdStatusMagazine(idNjesiAdministrative, idStatusMagazine, moduliAsete))
        //    {
        //        moduliAsete.beginTransaksion();
        //        clsMesazh pergjigja = modifikoStatusDokumentiTransaksion(moduliAsete, idNdermarrje, idPerdoruesi, idNderViti, idperiudha, njesiadm, idnivelgjenerues, rm, ci);
        //        if (pergjigja.Status)
        //            moduliAsete.commitTransaksion();
        //        else
        //            moduliAsete.rollbackTransaksion();
        //        return pergjigja;
        //    }
        //    return new clsMesazh(false, rm.GetString("msgNukKaHistorik", ci));
        //}

        /// <summary>
        /// MODULI ASETE:
        /// Fshin objektin e historikut te statusit te magazines nese statusi qe duam te fshime eshte i krijuar njehere dhe nuk ka lidhje me objekte te tjera qe varen nga ekzistenca e objektit qe po fshihet.
        /// </summary>
        /// <param name="idNdermarrje">(int) Id e ndermarrje per te cilen do te kryhet modifikimi ne magazine.</param>
        /// <param name="idPerdoruesi">(int) Id e perdoruesit qe po kryen veprimin</param>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese fshirja kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        public clsMesazh fshi(int idNdermarrje, int idPerdoruesi)
        {
            clsMesazh pergjigja = new clsMesazh();
            if (kontrolloEkzistonHistorikMagazinaSipasIdStatusMagazine(idNjesiAdministrative, idStatusMagazine))
            {
                pergjigja = fshiStatusDokumentiTransaksion(idNdermarrje, idPerdoruesi);
                return pergjigja;
            }
            return new clsMesazh(false, "Nuk ekziston historik!");
        }

        /// <summary>
        /// MODULI ASETE:
        /// Fshin objektin e historikut te statusit te magazines nese statusi qe duam te fshime eshte i krijuar njehere dhe nuk ka lidhje me objekte te tjera qe varen nga ekzistenca e objektit qe po fshihet.
        /// </summary>
        /// <param name="idNdermarrje">(int) Id e ndermarrje per te cilen do te kryhet modifikimi ne magazine.</param>
        /// <param name="idPerdoruesi">(int) Id e perdoruesit qe po kryen veprimin</param>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese fshirja kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        public clsMesazh fshiTrans(int idNdermarrje, int idPerdoruesi)
        {
            clsMesazh pergjigja = new clsMesazh();
            using (var scope = new MyTransactionScope())
            {
                pergjigja = fshi(idNdermarrje, idPerdoruesi);
                if (!pergjigja.Status)
                    return pergjigja;
                scope.Complete();
            }
            return pergjigja;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr objektin e historikut te statuseve te magazines sipas id automatike te historikut.
        /// </summary>
        /// <param name="idHistorikuNjesiAdministrative">(int) Id automatike e historikut te njesive administrative.</param>
        /// <returns>Kthen True nese nuk ndodh asnje gabim gjate marrjes se historikut te statuseve te magazines ose False ne te kundert.</returns>
        public bool merrHistorikMagazinaSipasIdHistoriku(int idHistorikuNjesiAdministrative)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            bool pergjigje = mbushHistorikNjesiAdministrativeObjekt(moduliAsete.ktheHistorikMagazinaSipasIdHistoriku(idHistorikuNjesiAdministrative));
            moduliAsete.Dispose();
            return pergjigje;
        } 
        public bool merrHistorikMagazinaSipasIdHistoriku(int idHistorikuNjesiAdministrative,clsDatabazeAsete moduliAsete)
        {
          
            bool pergjigje = mbushHistorikNjesiAdministrativeObjekt(moduliAsete.ktheHistorikMagazinaSipasIdHistoriku(idHistorikuNjesiAdministrative));
         
            return pergjigje;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr objektin e historikut te statusit te fundit (aktual) te magazines qe kerkojme.
        /// </summary>
        /// <param name="idNjesiAdministrative">(int) Id e njesise administrative qe duhet ti marrim historikun.</param>
        /// <returns>Kthen True nese nuk ndodh asnje gabim gjate marrjes se historikut te statusit te fundit (aktual) te magazines ose False ne te kundert.</returns>
        public bool merrHistorikMagazinaAktualeSipasIdNjesiAdministrative(int idNjesiAdministrative)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            bool pergjigje = mbushHistorikNjesiAdministrativeObjekt(moduliAsete.ktheHistorikMagazinaAktualeSipasIdNjesiAdministrative(idNjesiAdministrative));
            moduliAsete.Dispose();
            return pergjigje;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr objektin e historikut te statusit te fundit (aktual) te magazines qe kerkojme sipas nje transaksioni te hapur me pare..
        /// </summary>
        /// <param name="idNjesiAdministrative">(int) Id e njesise administrative qe duhet ti marrim historikun.</param>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen True nese nuk ndodh asnje gabim gjate marrjes se historikut te statusit te fundit (aktual) te magazines ose False ne te kundert.</returns>
        public bool merrHistorikMagazinaAktualeSipasIdNjesiAdministrative(int idNjesiAdministrative, clsDatabazeAsete moduliAsete)
        {
            bool pergjigje = mbushHistorikNjesiAdministrativeObjekt(moduliAsete.ktheHistorikMagazinaAktualeSipasIdNjesiAdministrative(idNjesiAdministrative));
            return pergjigje;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr id e historikut te statusit te fundit (aktual) te magazines qe kerkojme.
        /// </summary>
        /// <param name="idNjesiAdministrative">(int) Id e njesise administrative qe duhet ti marrim historikun.</param>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen id e historikut te statusit te fundit (aktual) te magazines nese gjendet, ne te kundert kthen -1.</returns>
        public static int merrIDHistorikMagazinaAktualeSipasIdNjesiAdministrative(int idNjesiAdministrative)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            int pergjigje = moduliAsete.ktheIDHistorikMagazinaAktualeSipasIdNjesiAdministrative(idNjesiAdministrative);
            return pergjigje;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr id e statusit te fundit (aktual) te magazines qe kerkojme.
        /// </summary>
        /// <param name="idNjesiAdministrative">(int) Id e njesise administrative qe duhet ti marrim historikun.</param>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen id e statusit te fundit (aktual) te magazines nese gjendet, ne te kundert kthen -1.</returns>
        public static int merrIDStatusMagazineNgaHistorikMagazinaSipasIdNjesiAdministrative(int idNjesiAdministrative)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            int pergjigje = moduliAsete.ktheIDStatusMagazineNgaHistorikMagazinaSipasIdNjesiAdministrative(idNjesiAdministrative);
            return pergjigje;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr id e statusit me te fundit ne lidhje me daten e vendosur te magazines qe kerkojme.
        /// </summary>
        /// <param name="idNjesiAdministrative">(int) Id e njesise administrative qe duhet ti marrim historikun.</param>
        /// <param name="dataStatusi">(DateTime) Data e statusit qe kerkojme.</param>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen id e statusit me te fundit ne lidhje me daten e vendosur te magazines qe kerkojme nese gjendet, ne te kundert kthen -1.</returns>
        public static int merrIDStatusMagazinaSipasIdNjesiAdministrativeDateStatusi(int idNjesiAdministrative, DateTime dataStatusi)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            int pergjigje = moduliAsete.ktheIDStatusMagazinaSipasIdNjesiAdministrativeDateStatusi(idNjesiAdministrative, dataStatusi);
            return pergjigje;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr daten e statusit te fundit (aktual) te magazines qe kerkojme.
        /// </summary>
        /// <param name="idNjesiAdministrative">(int) Id e njesise administrative qe duhet ti marrim historikun.</param>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen daten e statusit te fundit (aktual) te magazines nese gjendet, ne te kundert kthen "01/01/1900".</returns>
        public static DateTime merrDateMagazineNgaHistorikMagazinaSipasIdNjesiAdministrative(int idNjesiAdministrative)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            DateTime pergjigje = moduliAsete.ktheDateMagazineNgaHistorikMagazinaSipasIdNjesiAdministrative(idNjesiAdministrative);
            return pergjigje;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kontrollon nese ekziston nje ose me shume magazina qe kane qene me pare ne gjendjen e id se statusit te magazines qe kerkojme te ruajtura ne bazen e te dhenave.
        /// </summary>
        /// <param name="idNjesiAdministrative">(int) Id e njesise administrative qe duhet ti marrim historikun.</param>
        /// <returns>Kthen True nese ekziston nje ose me shume magazine qe ka patur si historik gjendjen e id se statusit te magazines qe ne kerkojme dhe False ne te kundert.</returns>
        public static bool kontrolloEkzistonHistorikMagazinaSipasIdNjesiAdministrative(int idNjesiAdministrative)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            bool pergjigje = moduliAsete.ekzistonHistorikMagazinaSipasIdNjesiAdministrative(idNjesiAdministrative);
            moduliAsete.Dispose();
            return pergjigje;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Kontrollon nese ekziston nje ose me shume magazina qe kane qene me pare ne gjendjen e id se statusit te magazines qe kerkojme te ruajtura ne bazen e te dhenave.
        /// </summary>
        /// <param name="idNjesiAdministrative">(int) Id e njesise administrative qe duhet ti marrim historikun.</param>
        /// <param name="idStatusMagazine">(int) Id e statusit te magazines ne te cilen ka qene me pare ose jane magazinat.</param>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen True nese ekziston nje ose me shume magazine qe ka patur si historik gjendjen e id se statusit te magazines qe ne kerkojme dhe False ne te kundert.</returns>
        public static bool kontrolloEkzistonHistorikMagazinaSipasIdStatusMagazine(int idNjesiAdministrative, int idStatusMagazine)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            bool pergjigje = moduliAsete.ekzistonHistorikMagazinaSipasIdStatusMagazine(idNjesiAdministrative, idStatusMagazine);
            return pergjigje;
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// MODULI ASETE:
        /// Ruan objektin e historikut te statusit te magazines.
        /// </summary>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrje per te cilen do te kryhet ruajtja ne magazine.</param>
        /// <param name="idNderViti">(int) Id e ndermarrjes qe lidhet me vitin fiskal.</param>
        /// <param name="idPeriudha">(int) Id e periudhes.</param>
        /// <param name="njesiadm">(DbRegjistrim.clsNjesiAdministrative) Objekti i njesise administrative.</param>
        /// <param name="idnivelgjenerues">(int) Id e nivelit gjenerues.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese ruajtja ne transaksion kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        private clsMesazh ruajStatusDokumentiTransaksion(int idNdermarrje, int idNderViti, int idPeriudha, DbRegjistrim.clsNjesiAdministrative njesiadm, int idnivelgjenerues, bool magazineEre)
        {
          string  mesazhmevonshem = "";
               clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            clsMesazh pergjigja = moduliAsete.ruajHistorikMagazine(out idHistorikuNjesiAdministrative, njesiadm.IdNjesiAdministrative, idStatusMagazine, dataStatusit, idStatusDokumenti, idPerdoruesi, idKrijuesi);
            int nrrreshti = 0;
            string shfaqmesazhapolupe = "jo";
            if (magazineEre) //ALPHAWEB-560
                return pergjigja;
            // Komentuar momentalisht sa te krijohet dokumentat per ndryshimin e statusit
            if (pergjigja.Status)
            {
                colAmortizimiKoka amortizimetKoka = new colAmortizimiKoka();
                DbShare.clsKonfigurimAmbjenti konf = new DbShare.clsKonfigurimAmbjenti();
                DbShare.clsDatabaseShare dbshare = new DbShare.clsDatabaseShare();
                konf.mbushKonfigAmbjSipasKod("FANS", idNdermarrje, dbshare);
                bool pergjigje = amortizimetKoka.krijoAmortizimeKokaNdryshimStatusi(this, njesiadm, idNdermarrje, idNderViti, out nrrreshti, konf, idnivelgjenerues, moduliAsete);
                if (!pergjigje)
                    return new clsMesazh(false, MessagesResource.Messages["msgGabimNeKrijiminEDokTeAmortizimitPerStandartet"]);
                if (amortizimetKoka.Count > 0)
                {
                    pergjigja = amortizimetKoka.ruajListAmortizime(njesiadm.IdNjesiAdministrative, 1, out shfaqmesazhapolupe, new colAmortizimiKoka(), idPeriudha, 86, new  colSerialetMagazine(), false, null,false, out mesazhmevonshem);
                    if (!pergjigja.Status)
                        return pergjigja;
                }
            }
            return pergjigja;
        }

        ///// <summary>
        ///// MODULI ASETE:
        ///// Modifikon objektin e historikut te magazines duke i ndryshuar statusin e dokumentit rreshtit qe po modifikohet dhe me pas duke shtuar si te sapo ruajtur rreshtin e ri.
        ///// </summary>
        ///// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        ///// <param name="idNdermarrje">(int) Id e ndermarrje per te cilen do te kryhet modifikimi ne magazine.</param>
        ///// <param name="idPerdoruesi">(int) Id e perdoruesit qe po kryen veprimin</param>
        ///// <param name="idNderViti">(int) Id e ndermarrjes qe lidhet me vitin fiskal.</param>
        ///// <param name="idperiudha">(int) Id e periudhes.</param>
        ///// <param name="njesiadm">(DbRegjistrim.clsNjesiAdministrative) Objekti i njesise administrative.</param>
        ///// <param name="idnivelgjenerues">(int) Id e nivelit gjenerues.</param>
        ///// <returns>Kthen clsMesazh me atribut statusi True nese modifikimi ne transaksion kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        //private clsMesazh modifikoStatusDokumentiTransaksion(clsDatabazeAsete moduliAsete, int idNdermarrje, int idPerdoruesi, int idNderViti, int idperiudha, DbRegjistrim.clsNjesiAdministrative njesiadm, int idnivelgjenerues, ResourceManager rm, CultureInfo ci)
        //{
        //    clsMesazh pergjigja = fshiStatusDokumentiTransaksion(moduliAsete, idNdermarrje, idPerdoruesi);
        //    if (pergjigja.Status)
        //        pergjigja = ruajStatusDokumentiTransaksion(moduliAsete, idNdermarrje, idNderViti, idperiudha, njesiadm, idnivelgjenerues, rm, ci);
        //    return pergjigja;
        //}

        /// <summary>
        /// MODULI ASETE:
        /// Fshin objektin e historikut te magazines duke i ndryshuar statusin e dokumentit rreshtit qe po modifikohet dhe me pas duke shtuar si te sapo ruajtur rreshtin e ri.
        /// </summary>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrje per te cilen do te kryhet modifikimi ne magazine.</param>
        /// <param name="idPerdoruesi">(int) Id e perdoruesit qe po kryen veprimin.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese fshirja ne transaksion kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        private clsMesazh fshiStatusDokumentiTransaksion(int idNdermarrje, int idPerdoruesi)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            clsMesazh pergjigja = moduliAsete.modifikoHistorikMagazineStatus(merrIDHistorikMagazinaAktualeSipasIdNjesiAdministrative(idNjesiAdministrative), idPerdoruesi);
            if (!pergjigja.Status)
                return pergjigja;
            clsHistorikStatusMagazine historikfundit = new clsHistorikStatusMagazine();
            historikfundit.merrHistorikMagazinaAktualeSipasIdNjesiAdministrative(idNjesiAdministrative);
            if (historikfundit.IdHistorikuNjesiAdministrative > 0)
            {
                int histroikparafundit = moduliAsete.ktheIDHistorikMagazinaParafunditSipasIdNjesiAdministrative(idNjesiAdministrative);
                DbRegjistrim.clsDatabaseRegjistrim dbregj = new DbRegjistrim.clsDatabaseRegjistrim();
                pergjigja = dbregj.modifikoNjesiAdministrative(idNjesiAdministrative, idPerdoruesi, historikfundit.idStatusMagazine, historikfundit.dataStatusit, histroikparafundit);
                if (pergjigja.Status)
                {
                    clsAmortizimiKoka dokumentAmortizimiFANS = new clsAmortizimiKoka();
                    DbShare.clsDatabaseShare dbshare = new DbShare.clsDatabaseShare();
                    DbShare.clsKonfigurimAmbjenti clskonf = new DbShare.clsKonfigurimAmbjenti();
                    clskonf.mbushKonfigAmbjSipasKod("FANS", idNdermarrje, dbshare);
                    colStandarteAmortizimi standarte = new colStandarteAmortizimi(idNdermarrje);
                    foreach (clsStandarteAmortizim standarti in standarte)
                    {
                        dokumentAmortizimiFANS.merrAmortizimKokaSipasMagDateNenKatDok(clskonf.IdKonfigAmbjente, dataStatusit, idNjesiAdministrative, standarti.IdStandarti, idNdermarrje, moduliAsete);
                        if (dokumentAmortizimiFANS.IdAmortizimi > 0)
                            pergjigja = dokumentAmortizimiFANS.fshi(idPerdoruesi, 86, false, new DbCore.DbAsete.colAmortizimiTrupi());
                        if (!pergjigja.Status)
                            return pergjigja;
                    }
                    clsDatabaseKontabilitet dbkontab = new clsDatabaseKontabilitet();
                    pergjigja = fshifleteKontabelNdryshimGjendje(idPerdoruesi, dbkontab);
                    if (!pergjigja.Status)
                        return pergjigja;
                }
            }
            return pergjigja;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Fshin fleten kontabel te krijuar nga ndryshimi i gjendjes.
        /// </summary>
        /// <param name="idPerdoruesi">(int) Id e perdoruesit qe po kryen veprimin</param>
        /// <param name="dbkontab">(clsDatabaseKontabilitet) Merr objektin e transaksionit qe te lidhi veprimet me modulin e kontabilizimit.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese fshirja ne transaksion kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        private clsMesazh fshifleteKontabelNdryshimGjendje(int idPerdoruesi, clsDatabaseKontabilitet dbkontab)
        {

            clsMesazh pergjigja = new clsMesazh(true);
            clsKokaFleteKontabel newclsKokaFleteKontabel = new clsKokaFleteKontabel(idNjesiAdministrative, 23, dataStatusit, dbkontab);///fleta kontabel e dokumentit

         
            if (newclsKokaFleteKontabel.IdKokaFleteKontabel != 0)/// nqs dokumenti eshte i kontabilizuar kalojme dokumentin me status fshire dhe nqs eshte me stornim krijojme dokumentin e kundert
            {
                pergjigja = newclsKokaFleteKontabel.fshiupd(dbkontab);
                if (!pergjigja.Status)
                    return pergjigja;
            }
            return pergjigja;
        }

        #endregion
    }
}
