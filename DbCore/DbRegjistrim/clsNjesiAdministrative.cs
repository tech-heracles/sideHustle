using DbCore.DbAdmin;
using DbCore.DbKontabiliteti;
using DbCore.DbShare;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Resources;
using DbCore.IMBUtils.Validation;
using DbCore.IMBUtils.Logging;
using DbCore.IMBUtils.Messages;
using DbCore.IMBUtils.Fiskalizimi.Controls;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne  njesine administrative(magazinen)
    ///  (Te dhenat  merren nga tabela : T_NJESIADMINISTRATIVE)
    /// </summary>
    public sealed class clsNjesiAdministrative
    {
        #region Atribute

        private int idNjesiAdm;
        private string kodi;
        private string pershkrimi;
        private string adresa;
        private int idInventarizimi;
        private bool ndjekjeGjendje;
        private bool aktiv;
        private int idNdermarrje;
        private int idPerdorues;
        private DateTime dateRegjistrimi;
        private int idKonfig;
        private int idDegeAdministrative;
        private int idStatusDok;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private string degeAdministrative;
        private colLidhjetAutorizim oColLidhjetAutorizim;
        private int idLlojMagazine;
        private int idStatusAktualMagazine;
        private DateTime dataNdryshimStatus;
        private int idHistorikFundit;
        private float kohezgjatja;
        private DbAsete.colHistorikStatusMagazine colHistorik;
        private clsKokaFleteKontabel fleteKontabel;
        private string koordinata;
        private bool celPerdoruesTollonash;
        private string shenime;
        private string telefon;
        private DataRow rreshti;
        private int llojLayeri;
        private int idMagPrind;
        private string email;
        private colVleraFushaShtese oColVleratFushatShtese;
        private colArkiva oArkiva;
        private string kodMagPrindi;
        private int idElementiPerIntegrim;
        private int qendraKostos;
        private int idSkemaQendraKosto;
        private int llojQendre;
        private bool kontrollGjendjeDet1;
        private bool kontrollGjendjeDet2;
        private bool ownShop;
        private string tipiMag;
        private int qyteti;
        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases me parametra
        /// </summary>
        /// <param name="idNjes"> id ritese e njesise administrative</param>
        /// <param name="kod">kodi i njesise administrative</param>
        /// <param name="pershk"> pershkrimi</param>
        /// <param name="adr"> adresa e njesise administrative</param>
        /// <param name="idInv">id inventarizimi</param>
        /// <param name="ndjGjend"> ndjekje gjendje </param>
        /// <param name="akt">gjendja nese eshte aktive apo jo</param>
        /// <param name="nderm">id e ndermarjes</param>
        /// <param name="idPerd">id e perdoruesit qe e ka ruajtur</param>
        /// <param name="dtRegj"> data e regjistrimit</param>
        /// <param name="idKonfig">id e konfigurimit</param>
        /// <param name="idDegeAdministrative">id e deges administrative</param>
        /// <param name="idstatusdok">statusi i njesise</param>
        /// <param name="colLidhjet">autorizimet</param>
        /// <param name="idLlojMagazine">id e llojit te magazines se per cfare artikujsh do te perdoret magazina (afatgjate, afatshkurter, apo te dy)</param>
        /// <param name="idStatusAktualMagazine">id e statusit aktual qe ndodhet magazina</param>
        /// <param name="dataNdryshimStatus">data e fundit e ndryshimit te statusit</param>
        /// <param name="idHistorikFundit">id e statusit te fundit te magazines para se te nderrohet statusi</param>
        /// <param name="kohezgjatja">kohezgjatja ne vite e amortizimit te artikujve te magazines</param>
        public clsNjesiAdministrative(int idNjes, string kod, string pershk, string adr, int idInv, bool ndjGjend, bool akt, int nderm, int idPerd, DateTime dtRegj, int idKonfig, int idDegeAdministrative, int idstatusdok, colLidhjetAutorizim colLidhjet, int idLlojMagazine, int idStatusAktualMagazine, DateTime dataNdryshimStatus, int idHistorikFundit, float kohezgjatja, DbAsete.colHistorikStatusMagazine col, string koordinat, bool celPerdTollona, string shenime, string telefon, int llojLayeri, int idMagPrind, string email, string kodMagPrind, int idqendra, int idskema, int llojqendre, bool kontrollgjendjedet1,bool kontrollgjendjedet2, bool ownshop,string tipimag,int qyteti)
        {
            idNjesiAdm = idNjes;
            kodi = kod;
            pershkrimi = pershk;
            adresa = adr;
            idInventarizimi = idInv;
            ndjekjeGjendje = ndjGjend;
            aktiv = akt;
            idNdermarrje = nderm;
            idPerdorues = idPerd;
            dateRegjistrimi = dtRegj;
            this.idKonfig = idKonfig;
            this.idDegeAdministrative = idDegeAdministrative;
            idStatusDok = idstatusdok;
            oColLidhjetAutorizim = colLidhjet;
            colHistorik = col;
            koordinata = koordinat;
            this.shenime = shenime;
            this.telefon = telefon;
            this.llojLayeri = llojLayeri;
            this.idMagPrind = idMagPrind;
            this.email = email;
            this.kodMagPrindi = kodMagPrind;
            this.qendraKostos = idqendra;
            this.idSkemaQendraKosto = idskema;
            this.llojQendre = llojqendre;
            this.kontrollGjendjeDet1 = kontrollgjendjedet1;
            this.kontrollGjendjeDet2 = kontrollgjendjedet2;
            oColVleratFushatShtese = new colVleraFushaShtese();
            this.ownShop = ownshop;
            this.tipiMag = tipimag;
            this.qyteti = qyteti;
        }

        /// <summary>
        /// Konstruktori i klases me parametra
        /// </summary>
        /// <param name="idNjes"> id ritese e njesise administrative</param>
        /// <param name="kod">kodi i njesise administrative</param>
        /// <param name="pershk"> pershkrimi</param>
        /// <param name="adr"> adresa e njesise administrative</param>
        /// <param name="idInv">id inventarizimi</param>
        /// <param name="ndjGjend"> ndjekje gjendje </param>
        /// <param name="akt">gjendja nese eshte aktive apo jo</param>
        /// <param name="nderm">id e ndermarjes</param>
        /// <param name="idPerd">id e perdoruesit qe e ka ruajtur</param>
        /// <param name="dtRegj"> data e regjistrimit</param>
        /// <param name="idKonfig">id e konfigurimit</param>
        /// <param name="idDegeAdministrative">id e deges administrative</param>
        /// <param name="colLidhjet">autorizimet</param>
        /// <param name="idLlojMagazine">id e llojit te magazines se per cfare artikujsh do te perdoret magazina (afatgjate, afatshkurter, apo te dy)</param>
        /// <param name="idStatusAktualMagazine">id e statusit aktual qe ndodhet magazina</param>
        /// <param name="dataNdryshimStatus">data e fundit e ndryshimit te statusit</param>
        /// <param name="idHistorikFundit">id e statusit te fundit te magazines para se te nderrohet statusi</param>
        /// <param name="kohezgjatja">kohezgjatja ne vite e amortizimit te artikujve te magazines</param>
        public clsNjesiAdministrative(int idNjes, string kod, string pershk, string adr, int idInv, bool ndjGjend, bool akt, int nderm, int idPerd, DateTime dtRegj, int idKonfig, int idDegeAdministrative, string degeadm, bool shtim, colLidhjetAutorizim colLidhjet, int idLlojMagazine, int idStatusAktualMagazine, DateTime dataNdryshimStatus, int idHistorikFundit, float kohezgjatja, DbAsete.colHistorikStatusMagazine col, string koordinat, bool celPerdTollona, string shenime, string telefon, int llojLayeri, int idMagPrind, colVleraFushaShtese colVleraFushaShtese, string email, IDictionary<string, object> hfArkiva, string kodMagPrind, int idElementiPerIntegrim, int idqendra, int idskema, int llojqendre, ResourceManager rm, CultureInfo ci, bool kontrollgjendjedet1, bool kontrollgjendjedet2, bool ownShop, string tipimag, int qyteti)
        {
            try
            {
                idNjesiAdm = idNjes;
                kodi = kod;
                pershkrimi = pershk;
                adresa = adr;
                idInventarizimi = idInv;
                ndjekjeGjendje = ndjGjend;
                aktiv = akt;
                idNdermarrje = nderm;
                idPerdorues = idPerd;
                dateRegjistrimi = dtRegj;
                this.idKonfig = idKonfig;
                this.idDegeAdministrative = idDegeAdministrative;
                idStatusDok = 1;
                this.idLlojMagazine = idLlojMagazine;
                this.idStatusAktualMagazine = idStatusAktualMagazine;
                this.dataNdryshimStatus = dataNdryshimStatus;
                this.idHistorikFundit = idHistorikFundit;
                this.kohezgjatja = kohezgjatja;
                degeAdministrative = degeadm;
                oColLidhjetAutorizim = colLidhjet;
                colHistorik = col;
                koordinata = koordinat;
                this.shenime = shenime;
                this.telefon = telefon;
                this.llojLayeri = llojLayeri;
                this.idMagPrind = idMagPrind;
                this.email = email;
                this.kodMagPrindi = kodMagPrind;
                this.idElementiPerIntegrim = idElementiPerIntegrim;
                this.qendraKostos = idqendra;
                this.idSkemaQendraKosto = idskema;
                this.llojQendre = llojqendre;
                kontrollGjendjeDet1 = kontrollgjendjedet1;
                kontrollGjendjeDet2 = kontrollgjendjedet2;
                oColVleratFushatShtese = colVleraFushaShtese;
                this.ownShop = ownShop;
                this.tipiMag = tipimag;
                this.qyteti = qyteti;
                //this.oArkiva = oArkiva;
                clsMesazh mesazh = kontrolloMagazine(shtim, rm, ci);
                if (!mesazh.Status)
                    throw new Exception(mesazh.PershkrimMesazhi);
                this.HfArkiva = hfArkiva;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// konstruktor me 3 parametra
        /// </summary>
        /// <param name="kodi">kodi i njesise administrative</param>
        /// <param name="idNderm">id e ndermarrjes</param>
        public clsNjesiAdministrative(string kodi, int idNderm, int idPerdorues)
        {
            using (clsDatabaseRegjistrim dbNjesiAdministrative = new clsDatabaseRegjistrim())
            {
                if (!mbushNjesiAdministrative(dbNjesiAdministrative.ktheNjesiAdministrativeSipasKodit(kodi, idNderm, idPerdorues)))
                    idNjesiAdm = -1;
            }
        }

        public clsNjesiAdministrative(string kodi, int idNderm)
        {
            using (clsDatabaseRegjistrim dbNjesiAdministrative = new clsDatabaseRegjistrim())
            {
                if (!mbushNjesiAdministrative(dbNjesiAdministrative.ktheNjesiAdministrativeSipasKoditPaAutorizim(kodi, idNderm)))
                    idNjesiAdm = -1;
            }
        }

        public clsNjesiAdministrative(string kodi, int idNderm, int idPerdorues, clsDatabaseRegjistrim dbNjesiAdministrative)
        {
            if (!mbushNjesiAdministrative(dbNjesiAdministrative.ktheNjesiAdministrativeSipasKodit(kodi, idNderm, idPerdorues)))
                idNjesiAdm = -1;
        }

        public clsNjesiAdministrative(string kodi, int idNderm, clsDatabaseRegjistrim dbNjesiAdministrative)
        {
            mbushNjesiAdministrative(dbNjesiAdministrative.TransCache.getNjesiAdministrative(kodi, idNderm, dbNjesiAdministrative));
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idNjesiAdm">id e njesise administrative</param>
        public clsNjesiAdministrative(int idNjesiAdm, int idPerdorues)
        {
            using (clsDatabaseRegjistrim dbNjesiAdministrative = new clsDatabaseRegjistrim())
            {
                mbushNjesiAdministrative(dbNjesiAdministrative.ktheNjesiAdministrativeSipasiD(idNjesiAdm, idPerdorues));
            }
        }

        public clsNjesiAdministrative(int idNjesiAdm)
        {
            using (clsDatabaseRegjistrim dbNjesiAdministrative = new clsDatabaseRegjistrim())
            {
                mbushNjesiAdministrative(dbNjesiAdministrative.ktheNjesiAdministrativeSipasiDPaAutorizime(idNjesiAdm));
            }
        }

        public clsNjesiAdministrative(int idNjesiAdm, int idPerdorues, clsDatabaseRegjistrim dbNjesiAdministrative)
        {
            mbushNjesiAdministrative(dbNjesiAdministrative.ktheNjesiAdministrativeSipasiD(idNjesiAdm, idPerdorues));
        }

        public clsNjesiAdministrative(int idNjesiAdm, clsDatabaseRegjistrim dbNjesiAdministrative)
        {
            mbushNjesiAdministrative(dbNjesiAdministrative.TransCache.getNjesiAdministrative(idNjesiAdm, dbNjesiAdministrative));
            //mbushNjesiAdministrative(dbNjesiAdministrative.ktheNjesiAdministrativeSipasiDPaAutorizime(idNjesiAdm));
        }

        /// <summary>
        /// Konstruktori i klases pa parametra
        /// </summary>
        public clsNjesiAdministrative()
        {
            oColVleratFushatShtese = new colVleraFushaShtese();
        }

        public clsNjesiAdministrative(DataRow rreshti)
        {
            
            mbushNjesiAdministrative(rreshti);
        }

        

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdNjesiAdministrative
        {
            get { return idNjesiAdm; }
            set { idNjesiAdm = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne dege administrative.
        /// </summary>
        public int IdDegeAdministrative
        {
            get { return idDegeAdministrative; }
            set { idDegeAdministrative = value; }
        }

        /// <summary>
        /// Kthen/Vendos adresa e njesise administrative.
        /// </summary>
        public string Adresa
        {
            get { return adresa; }
            set { adresa = value; }
        }

        /// <summary>
        /// Kthen/Vendos kodi i njesise administrative.
        /// </summary>
        public string Kodi
        {
            get { return kodi; }
            set { kodi = value; }
        }

        /// <summary>
        /// Kthen/Vendos pershkrimi i njesise administrative.
        /// </summary>
        public string Pershkrimi
        {
            get { return pershkrimi; }
            set { pershkrimi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e inventarizimit.
        /// <example> i vazhdueshem, hapje, inventar</example>
        /// </summary>
        public int IdInventarizimi
        {
            get { return idInventarizimi; }
            set { idInventarizimi = value; }
        }

        /// <summary>
        /// Kthen/Vendos gjendjen e njesise administrative nese eshte aktiv apo jo.
        /// </summary>
        public Boolean Aktiv
        {
            get { return aktiv; }
            set { aktiv = value; }
        }

        /// <summary>
        /// Kthen/Vendos  ndjekjen e gjendes se artikujve ne njesine administative.
        /// </summary>
        public Boolean NdjekjeGjendje
        {
            get { return ndjekjeGjendje; }
            set { ndjekjeGjendje = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e ndermarjes
        /// </summary>
        public int IdNdermarje
        {
            get { return idNdermarrje; }
            set { idNdermarrje = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e perdoruesit qe e ka kryer veprimin.
        /// </summary>
        public int IdPerdorues
        {
            get { return idPerdorues; }
            set { idPerdorues = value; }
        }

        /// <summary>
        /// Kthen/Vendos data e regjistrimit.
        /// </summary>
        public DateTime DateRegjistrimi
        {
            get { return dateRegjistrimi; }
            set { dateRegjistrimi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e konfigurimit
        /// </summary>
        public int IdKonfig
        {
            get { return idKonfig; }
            set { idKonfig = value; }
        }

        public int IdStatusDok
        {
            get { return idStatusDok; }
            set { idStatusDok = value; }
        }

        public DateTime DtKrijimi
        {
            get { return dtKrijimi; }
        }

        public DateTime DtModifikimi
        {
            get { return dtModifikimi; }
        }

        public DbAsete.colHistorikStatusMagazine ColHistorik
        {
            get
            {
                return colHistorik;
            }
            set
            {
                colHistorik = value;
            }
        }

        public clsKokaFleteKontabel FleteKontabel
        {
            get
            {
                return fleteKontabel;
            }
            set
            {
                fleteKontabel = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos nje collection me objekte te tipit <see cref="clsLidhjeAutorizim"/>
        /// </summary>
        public colLidhjetAutorizim OColLidhjetAutorizim
        {
            get { return oColLidhjetAutorizim; }
            set { oColLidhjetAutorizim = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se llojit te magazines se per cfare artikujsh do te perdoret magazina (afatgjate, afatshkurter, apo te dy). 
        /// </summary>
        public int IdLlojMagazine
        {
            get { return idLlojMagazine; }
            set { idLlojMagazine = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se statusit aktual qe ndodhet magazina. 
        /// </summary>
        public int IdStatusAktualMagazine
        {
            get { return idStatusAktualMagazine; }
            set { idStatusAktualMagazine = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere dates se fundit te ndryshimit te statusit.
        /// </summary>
        public DateTime DataNdryshimStatus
        {
            get { return dataNdryshimStatus; }
            set { dataNdryshimStatus = value; }

        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se statusit te fundit te magazines para se te nderrohet statusi.
        /// </summary>
        public int IdHistorikFundit
        {
            get { return idHistorikFundit; }
            set { idHistorikFundit = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere kohezgjatjes ne vite te amortizimit te artikujve te magazines.
        /// </summary>
        public float Kohezgjatja
        {
            get { return kohezgjatja; }
            set { kohezgjatja = value; }
        }

        public string Koordinata
        {
            get { return koordinata; }
            set { koordinata = value; }
        }

        public bool CelPerdoruesTollonash
        {
            get { return celPerdoruesTollonash; }
            set { celPerdoruesTollonash = value; }
        }
                
        public string Shenime
        {
            get { return shenime; }
            set { shenime = value; }
        }

        public string Telefon
        {
            get { return telefon; }
            set { telefon = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e ndermarjes
        /// </summary>
        public int LlojLayeri
        {
            get { return llojLayeri; }
            set { llojLayeri = value; }
        }

        public int IdElementiPerIntegrim
        {
            get { return idElementiPerIntegrim; }
            set { idElementiPerIntegrim = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e magazines prind.
        /// </summary>
        public int IdMagPrind
        {
            get { return idMagPrind; }
            set { idMagPrind = value; }
        }
        /// <summary>
        /// Kthen/Vendos emailin per magazinen
        /// </summary>
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        /// <summary>
        /// Kthen/Vendos nje collection me objekte te tipit <see cref="DbAdmin.clsVleraFushaShtese"/>
        /// </summary>
        public colVleraFushaShtese OColVleratFushatShtese
        {
            get { return oColVleratFushatShtese; }
            set { oColVleratFushatShtese = value; }
        }

        public colArkiva OArkiva { get { return oArkiva; } set { oArkiva = value; } }

        public IDictionary<string, object> HfArkiva { get; set; }

        public int QendraKostos
        {
            get
            {
                return qendraKostos;
            }

            set
            {
                qendraKostos = value;
            }
        }

        public int IdSkemaQendraKosto
        {
            get
            {
                return idSkemaQendraKosto;
            }

            set
            {
                idSkemaQendraKosto = value;
            }
        }

        public int LlojQendre
        {
            get
            {
                return llojQendre;
            }

            set
            {
                llojQendre = value;
            }
        }

        public bool KontrollGjendjeDet1
        {
            get
            {
                return kontrollGjendjeDet1;
            }

            set
            {
                kontrollGjendjeDet1 = value;
            }
        }

        public bool KontrollGjendjeDet2
        {
            get
            {
                return kontrollGjendjeDet2;
            }

            set
            {
                kontrollGjendjeDet2 = value;
            }
        }

        public bool OwnShop
        {
            get { return ownShop; }
            set { ownShop = value; }
        }
        public string TipiMag
        {
            get { return tipiMag; }
            set { tipiMag = value; }
        }
        public int Qyteti
        {
            get { return qyteti; }
            set { qyteti = value; }
        }
        #endregion

        #region Metoda Publike

        /// <summary>
        /// thjesht kthen idMagazines. Kujdes se nuk kontrollon autorizimet
        /// </summary>
        /// <param name="kodMag"></param>
        /// <param name="idNdermarje"></param>
        /// <returns></returns>
        public static int ktheIdMagazine(string kodMag, int idNdermarje)
        {
            using (clsDatabaseRegjistrim dbNjesiAdministrative = new clsDatabaseRegjistrim())
            {
                return dbNjesiAdministrative.ktheIdNjesiAdministrativeSipasKodit(kodMag, idNdermarje);
            }
        }

        /// <summary>
        /// thjesht kthen idMagazines, vetem per ato magazina qe perdoruesi ka autorizim
        /// </summary>
        /// <param name="kodmag"></param>
        /// <param name="idNdermarje"></param>
        /// <param name="idPerdoruesi"></param>
        /// <returns></returns>
        public static int ktheIdMagazine(string kodMag, int idNdermarje, int idPerdoruesi)
        {
            using (clsDatabaseRegjistrim dbNjesiAdministrative = new clsDatabaseRegjistrim())
            {
                return dbNjesiAdministrative.ktheIdNjesiAdministrativeSipasKodit(kodMag, idNdermarje, idPerdoruesi);
            }
        }

        /// <summary>
        /// Kthen id e prindit te magazines me id = idMag
        /// </summary>
        /// <param name="idMag">Id e magazines qe duam t'i gjejme prindin</param>
        /// <returns>id e prindit te magazines</returns>
        public static int ktheIdPrindMagazine(int idMag)
        {
            using (clsDatabaseRegjistrim dbNjesiAdministrative = new clsDatabaseRegjistrim())
            {
                return dbNjesiAdministrative.ktheIdPrindNjesiAdministrativeSipasId(idMag);
            }
        }

        /// <summary>
        /// kthen pershkrimin e magazines sipas kodit
        /// </summary>
        /// <param name="kodmag"></param>
        /// <param name="idNdermarje"></param>
        /// <param name="idPerdoruesi"></param>
        /// <returns></returns>
        public static String kthePershkrimMagazineSipasKodit(string kodMag, int idNdermarje)
        {
            using (clsDatabaseRegjistrim dbNjesiAdministrative = new clsDatabaseRegjistrim())
            {
                return dbNjesiAdministrative.kthePershkrimMagazineSipasKodit(kodMag, idNdermarje);
            }
        }
        
        public static clsNjesiAdministrative krijoPerImport(string kodi, string pershkrimi, string adresa, string degaadministrative, bool aktiv, int idinventarizimi, bool ndjekjegjendje, int idndermarje, int idperdoruesi, DateTime dateregjistrimi, int idkonfig, bool shtim, string LlojMagazine, string StatusAktualMagazine, DateTime dataNdryshimStatus, int idHistorikFundit, int kohezgjatja, string koordinat, bool celPerdTollonat, String shenime, string telefon, string llojLayeri, string kodMagPrind, string email, string autorizime, ResourceManager rm, CultureInfo ci, bool kontrollgjendjedet1, bool kontrollgjendjedet2, bool ownShop)
        {
            try
            {
                int idDegeAdministrative = new clsDegeAdministrative(degaadministrative, idndermarje).IdDegeAdministrative;
                DbAsete.clsDatabazeAsete asete = new DbAsete.clsDatabazeAsete();
                int idLlojMagazine = asete.ktheIDLlojNjesiAdministrativeSipasEmertimi(LlojMagazine);
                if ((idLlojMagazine == 2 || idLlojMagazine == 3) & StatusAktualMagazine == "")
                    throw new Exception("Plotesoni statusin e njesise administrative!");
                if ((idLlojMagazine == 2 || idLlojMagazine == 3) & dataNdryshimStatus == DateTime.MinValue)
                    throw new Exception("Plotesoni daten e fillimit te statusit!");
                int idStatusAktualMagazine = DbAsete.clsStatusMagazine_Asete.merrIDStatusMagazinesTeNdermarrjesSipasEmertimit(StatusAktualMagazine, idndermarje);
                DbAsete.colHistorikStatusMagazine col = new DbAsete.colHistorikStatusMagazine();
                if (StatusAktualMagazine != "" && idLlojMagazine != 1)
                {
                    DbAsete.clsHistorikStatusMagazine historik = new DbAsete.clsHistorikStatusMagazine(0, 0, idStatusAktualMagazine, dataNdryshimStatus, 1, idperdoruesi, idperdoruesi);
                    col.Add(historik);
                }
                int idLlojLayeri = 0;
                switch (llojLayeri)
                {
                    case "Nënstacion":
                    case "Nenstacion":
                        idLlojLayeri = 2;
                        break;
                    case "Kabinë":
                    case "Kabine":
                        idLlojLayeri = 3;
                        break;
                    case "":
                        idLlojLayeri = 1;
                        break;
                    default:
                        throw new Exception("Lloji i layerit nuk eshte i sakte!");
                        //break;
                }

                colLidhjetAutorizim colLidhje;
                if (autorizime == "")
                    colLidhje = new colLidhjetAutorizim();
                else
                {
                    colLidhjetAutorizim colLidhjet = new colLidhjetAutorizim();
                    string[] pars11 = autorizime.Split(',');
                    for (int i = 0; i < pars11.Length; i++)
                    {
                        bool kaTeDrejtaPerAutorizimin = clsAutorizimKoka.kaAutorizimSipasPerdoruesit(pars11[i], idperdoruesi);
                        if (!kaTeDrejtaPerAutorizimin)
                            throw new MyException("Ju nuk keni te drejta te ky autorizim!");
                        clsLidhjeAutorizim lidhje = new clsLidhjeAutorizim();
                        lidhje.IdAutorizimeKoka = clsAutorizimKoka.ktheIDAutorizim(pars11[i]);
                        colLidhjet.Add(lidhje);
                    }
                    colLidhje = colLidhjet;
                }

                int idMagPrindi = 0;
                if (!String.IsNullOrEmpty(kodMagPrind))
                {
                    idMagPrindi = ktheIdMagazine(kodMagPrind, idndermarje);
                    if (idMagPrindi <= 0)
                        throw new Exception("Magazina vartese nuk ekziston!");
                }

                return new clsNjesiAdministrative(0, kodi, pershkrimi, adresa, idinventarizimi, ndjekjegjendje, aktiv, idndermarje, idperdoruesi, dateregjistrimi, idkonfig, idDegeAdministrative, degaadministrative, shtim, colLidhje, idLlojMagazine, StatusAktualMagazine != "" ? idStatusAktualMagazine : 0, dataNdryshimStatus, idHistorikFundit, kohezgjatja, col, koordinat, celPerdTollonat, shenime, telefon, idLlojLayeri, idMagPrindi, new colVleraFushaShtese(), email, null, kodMagPrind, 0, 0, 0, 1, rm, ci, kontrollgjendjedet1, kontrollgjendjedet2, ownShop,"",0);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private clsMesazh kontrolloMagazine(bool shtim, ResourceManager rm, CultureInfo ci)
        {
            if (kodi == "")
                return new clsMesazh(false, "Plotesoni kodin e magazines!");
            clsMesazh kontrollkodi = clsFunksione.kontrolloKaraktereMeMesazh(kodi, FusheKontrolli.Kodi, false);
            if (!kontrollkodi.Status)
                return kontrollkodi;

            if (idStatusAktualMagazine == -1)
                return new clsMesazh(false, "Statusi i magazines nuk eshte i sakte!");
            if (pershkrimi == "")
                return new clsMesazh(false, "Plotesoni pershkrimin e magazines!");
            clsMesazh kontrollpershkrimi = clsFunksione.kontrolloKaraktereMeMesazh(pershkrimi, FusheKontrolli.Pershkrimi, true);
            if (!kontrollpershkrimi.Status)
                return kontrollpershkrimi;

            if (shtim && ekziston(kodi, idNdermarrje))
            {
                return new clsMesazh(false, "Ekziston nje magazine me kete kod. Ju lutem shenoni nje kod tjeter!");
            }
            else
            {
                string kodMagVjeter = ktheKodiNjesiAdministrativeSipasiD(idNjesiAdm, idPerdorues);
                if (kodMagVjeter != kodi)
                    if (ekziston(kodi, idNdermarrje))
                    {
                        return new clsMesazh(false, "Ekziston nje magazine me kete kod. Ju lutem shenoni nje kod tjeter!");
                    }
            }
            if (kohezgjatja <= 0)
            {
                return new clsMesazh(false, "Jetegjatesia duhet jete nr pozitiv me i madh se 0!");
            }
            if (degeAdministrative != "")
            {
                if (!clsDegeAdministrative.ekziston(degeAdministrative, idNdermarrje))
                {
                    return new clsMesazh(false, "Dega administrative nuk ekziston!");
                }
                clsDegeAdministrative dega = new clsDegeAdministrative(degeAdministrative, idNdermarrje);
                if (!dega.Aktiv)
                    return new clsMesazh(false, "Dega administrative nuk eshte aktive!");
            }
            if (oColLidhjetAutorizim.Count != 0)
            {
                foreach (clsLidhjeAutorizim o in oColLidhjetAutorizim)
                {
                    if (o.IdAutorizimeKoka <= 0)
                        return new clsMesazh(false, "Niveli i autorizimit nuk ekziston!");
                }
            }
            if (idLlojMagazine == -1)
                return new clsMesazh(false, "Ky lloj magazine nuk ekziston!");
            if (!shtim && idMagPrind > 0 && !kontrolloPrind(idNjesiAdm, idMagPrind))
                return new clsMesazh(false, "Nuk mund te zgjidhet kjo magazine vartese, sepse krijon cikel!");
            if (!String.IsNullOrEmpty(kodMagPrindi) && idMagPrind <= 0)
                return new clsMesazh(false, "Magazina vartese me kod " + kodMagPrindi + " nuk ekziston!");

            return new clsMesazh(true, "Kontrollet e magazines u kaluan me sukses");
        }

        /// <summary>
        /// Funksion rekursiv qe kontrollon nese krijohen cikle ne zgjedhjen e prindit te magazines.
        /// </summary>
        /// <param name="idMagazine">id e magazines qe po i zgjedhim prindin</param>
        /// <param name="idPrindi">Si fillim ka vleren e prindit qe po zgjedhim per magazinen, pastaj merr vleren e prindit te prindit qe merr si parameter.</param>
        /// <returns>true nese krijohen cikle, false ne rast te kundert</returns>
        private bool kontrolloPrind(int idMagazine, int idPrindi)
        {
            if (idPrindi > 0)
            {
                if (idMagazine != idPrindi)
                {
                    idPrindi = ktheIdPrindMagazine(idPrindi);
                    return kontrolloPrind(idMagazine, idPrindi);
                }
                else
                    return false;
            }
            else
                return true;
        }

        public clsMesazh kontrollotransferim(clsNjesiAdministrative kod, int idndermarje, clsDatabaseRegjistrim db, int idperdoruesi, int idperiudha)
        {
            ImbLogger.LogTraceShitje("Filloi metoda kontrollotransferim!");
            clsKonfigurimAmbjenti konf = new clsKonfigurimAmbjenti();
            clsDatabaseShare dbshare = new clsDatabaseShare(db);
            konf.mbushKonfigAmbjSipasKod("MAG", idndermarje, dbshare);
            ImbLogger.LogTraceShitje("Mesazhi: Njesi admin transfrimi mbaroi me sukses!");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgNjesiAdminTransferimiMbaroiMeSukses"]);
            if (!db.ekzistonKodNjesiAdministrative(kod.Kodi, idndermarje))
            {
                kod.idPerdorues = idperdoruesi;
                kod.IdNdermarje = idndermarje;
                kod.idKonfig = konf.IdKonfigAmbjente;
                if (kod.idDegeAdministrative > 0)
                {
                    clsDegeAdministrative dege = new clsDegeAdministrative(kod.idDegeAdministrative, db);
                    mesazh = dege.kontrollotransferim(dege, idndermarje, db, idperdoruesi);
                    if (!mesazh.Status)
                        return mesazh;

                    kod.idDegeAdministrative = dege.IdDegeAdministrative;
                }
                kod.oColLidhjetAutorizim = new colLidhjetAutorizim();
                mesazh = kod.ruajMagazine(db, 0, idperiudha, konf.IdNivel);
                if (!mesazh.Status)
                    return mesazh;
            }
            return mesazh;
        }

        /// <summary>
        /// Ruan objektin e  njesise administrative ne tabelen perkatese ne databaze.Therret funksionin
        /// kjo metode thirret vetem per magazinat e reja 
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.ruajNjesiAdministrative"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>

        public clsMesazh ruajMagazine(IDictionary<string, object> hfNrAutoKF, int idNderViti, int idperiudha, int idnivel)
        {
            using (var scope=new MyTransactionScope())
            {
                clsMesazh mesazh;
                clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
                //data.beginTransaksion();
                try
                {
                    bool kaNdryshimNumri;
                    clsMesazh mesazhKontrolli = kontrolloMagazine(out kaNdryshimNumri, data, hfNrAutoKF, false);
                    if (!mesazhKontrolli.Status)
                    {
                        //db.rollbackTransaksion();
                        return mesazhKontrolli;
                    }
                    mesazh = ruajMagazine(data, idNderViti, idperiudha, idnivel, oArkiva);
                    if (!mesazh.Status)
                    {
                        //data.rollbackTransaksion();
                        return mesazh;
                    }
                    if (HfArkiva != null)
                        mesazh = colArkiva.RuajArkiven(idDegeAdministrative, 23, idPerdorues, idNdermarrje, HfArkiva);
                    if (!mesazh.Status)
                        return mesazh;
                    //data.commitTransaksion();
                    scope.Complete();
                    return mesazh;
                }
                catch (Exception e)
                {
                    data.rollbackTransaksion();
                    NLog.LogManager.GetCurrentClassLogger().Error(e, "ruajMagazine(" + idNderViti + ", " + idperiudha + ", " + idnivel + ", " + ") - Ndodhi nje gabim gjate ruajtjes se magazines");
                    return new clsMesazh(false, "Ndodhi nje gabim gjate ruajtjes!");
                }
            }
        }
        private clsMesazh kontrolloNrAutoArt(out bool kaNdryshimNumri, clsDatabaseRegjistrim db, IDictionary<string, object> hfNrAutoKf)
        {
            clsDatabaseAdmin dbadm = new clsDatabaseAdmin(db);

            List<NrAuto> list = clsNrAutom.kontrollogjithenumrat(dbadm, hfNrAutoKf, DateTime.Today);
            if (NrAuto.ktheVlerenEre(list, "Kodi") != "")
                this.Kodi = NrAuto.ktheVlerenEre(list, "Kodi");
            DbCore.clsMesazh mes = NrAuto.ruajvlera(out kaNdryshimNumri, list, DateTime.Today, this.IdPerdorues, this.idNdermarrje, dbadm);
            return new clsMesazh(mes.Status, mes.PershkrimMesazhi);

        }
        private clsMesazh kontrolloMagazine(out bool kaNdryshimNrAuto, clsDatabaseRegjistrim db, IDictionary<string, object> hfNrAutoKF, bool modifikim)
        {
            kaNdryshimNrAuto = false;
            if (kodi == "")
                return new clsMesazh(false, "Kodi i magazines nuk mund te jete bosh");
            if (!modifikim)
            {
                clsMesazh mes = new clsMesazh();
                if (hfNrAutoKF != null)
                {
                    mes = kontrolloNrAutoArt(out kaNdryshimNrAuto, db, hfNrAutoKF);
                    if (!mes.Status)
                        return mes;
                }
                if (db.ekzistonKodNjesiAdministrative(kodi, idNdermarrje))
                    return new clsMesazh(false, "Ekziston nje artikull me kete kod!");
                return new clsMesazh(mes.Status, mes.PershkrimMesazhi);
            }
            return new clsMesazh(true, "Kontrollet u kaluan me sukses");
        }

        /// <summary>
        /// Perdoret per te krijuar nje magazine te re
        /// </summary>
        /// <param name="data"></param>
        /// <param name="idNderviti"></param>
        /// <param name="idperiudha"></param>
        /// <param name="idnivel"></param>
        /// <param name="rm"></param>
        /// <param name="ci"></param>
        /// <returns></returns>
        public clsMesazh ruajMagazine(clsDatabaseRegjistrim data, int idNderviti, int idperiudha, int idnivel, colArkiva oArkiva = null)
        {
            clsMesazh mesazh;
            int id;
            mesazh = data.ruajNjesiAdministrative(out id, kodi, pershkrimi, adresa, idInventarizimi, ndjekjeGjendje, aktiv, idNdermarrje, idPerdorues, dateRegjistrimi, idKonfig, idDegeAdministrative, idStatusDok, idLlojMagazine, idStatusAktualMagazine, dataNdryshimStatus, idHistorikFundit, kohezgjatja, koordinata, celPerdoruesTollonash, shenime, telefon, llojLayeri, idMagPrind, email, idElementiPerIntegrim, qendraKostos, idSkemaQendraKosto, llojQendre, kontrollGjendjeDet1, kontrollGjendjeDet2, ownShop, tipiMag, qyteti, clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizim());
            IdNjesiAdministrative = id;
            if (!mesazh.Status)
            {
                return mesazh;
            }
            clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin(data);
            clsDatabaseKontabilitet dbkont = new clsDatabaseKontabilitet(data);
            foreach (clsLidhjeAutorizim o in oColLidhjetAutorizim)
            {
                o.IdLloji = clsLlojBuxheti.mbushIDLlojBuxheti("Magazina", dbkont);
                o.IdLidhese = IdNjesiAdministrative;
                mesazh = dbAdmin.ruajLidhjeAutorizim(o.IdLidhjeAutorizim, o.IdLidhese, o.IdLloji, o.IdAutorizimeKoka, 1);
                if (!mesazh.Status)
                    return mesazh;
            }
            DbAsete.clsDatabazeAsete dbasete = new DbAsete.clsDatabazeAsete(data);
            foreach (DbAsete.clsHistorikStatusMagazine hist in colHistorik)
            {
                hist.IdNjesiAdministrative = IdNjesiAdministrative;
                mesazh = hist.ruaj(IdNdermarje, idNderviti, idperiudha, this, idnivel, true);
                if (!mesazh.Status)
                    return mesazh;
            }

            if (oColVleratFushatShtese.Count > 0)
            {
                OColVleratFushatShtese.ForEach(x => x.IdLidhese = idNjesiAdm);
                mesazh = OColVleratFushatShtese.Ruaj();
            }
            return mesazh;
        }

        public clsMesazh modifikoMagazine(int idNderViti, int idperiudha, int idnivel, ResourceManager rm, CultureInfo ci)
        {
            clsMesazh mesazh;
            using (var scope=new MyTransactionScope())
            using (clsDatabaseRegjistrim data = new clsDatabaseRegjistrim())
            {
                //data.beginTransaksion();
                mesazh = modifikoMagazine(data, idNderViti, idperiudha, idnivel, rm, ci);
                if (!mesazh.Status)
                {
                    //data.rollbackTransaksion();
                    return mesazh;
                }
                //data.commitTransaksion();
                scope.Complete();
                return mesazh;
            }
        }

        /// <summary>
        /// Modifikon objektin e  njesise administrative ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.modifikoNjesiAdministrative"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo</returns>
        public clsMesazh modifikoMagazine(clsDatabaseRegjistrim data, int idNderviti, int idperiudha, int idnivel, ResourceManager rm, CultureInfo ci)
        {
            try
            {
                clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin(data);
                clsMesazh mesazh;
                colLidhjetAutorizim colLidhjetAutorizimiPara = new colLidhjetAutorizim(idNjesiAdm, "Magazina", dbAdmin);
                mesazh = data.modifikoNjesiAdministrative(idNjesiAdm, kodi, pershkrimi, adresa, idInventarizimi, ndjekjeGjendje, aktiv, idNdermarrje, idPerdorues, dateRegjistrimi, idKonfig, idDegeAdministrative, idStatusDok, idLlojMagazine, idStatusAktualMagazine, dataNdryshimStatus, idHistorikFundit, kohezgjatja, koordinata, celPerdoruesTollonash, shenime, telefon, llojLayeri, idMagPrind, email, idElementiPerIntegrim, qendraKostos, idSkemaQendraKosto, llojQendre, kontrollGjendjeDet1, kontrollGjendjeDet2, ownShop, tipiMag,qyteti,clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizim());
                clsDatabaseShare dbShare = new clsDatabaseShare(data);
                if (oArkiva != null)
                {
                    foreach (clsArkiva ar in oArkiva)
                    {
                        ar.IDDok = IdNjesiAdministrative;
                        mesazh = ar.update(dbShare);
                        if (!mesazh.Status)
                        {
                            return mesazh;
                        }
                    }
                }
                DbKontabiliteti.clsDatabaseKontabilitet dbKont = new DbKontabiliteti.clsDatabaseKontabilitet(data);
                for (int i = 0; i < oColLidhjetAutorizim.Count; i++)
                {
                    int idAutorizimKoka = oColLidhjetAutorizim[i].IdAutorizimeKoka;
                    if (idAutorizimKoka == -1)
                        continue;
                    oColLidhjetAutorizim[i].IdLloji = clsLlojBuxheti.mbushIDLlojBuxheti("Magazina", dbKont);
                    oColLidhjetAutorizim[i].IdLidhese = idNjesiAdm; ;
                    clsLidhjeAutorizim lidhjeNjejte = colLidhjetAutorizimiPara.Find(x => x.IdAutorizimeKoka == idAutorizimKoka);
                    if (lidhjeNjejte != null)
                    {   //i heqim nga collectioni autorizimet qe sjane ndryshuar sepse ne te do ngelen vetem autorizimet qe do te fshihen(vendosen status 2) 
                        colLidhjetAutorizimiPara.Remove(lidhjeNjejte);
                        continue;
                    }
                    mesazh = dbAdmin.ruajLidhjeAutorizim(oColLidhjetAutorizim[i].IdLidhjeAutorizim, oColLidhjetAutorizim[i].IdLidhese, oColLidhjetAutorizim[i].IdLloji, oColLidhjetAutorizim[i].IdAutorizimeKoka, 1);
                    if (!mesazh.Status)
                        return mesazh;
                }
                //fshihen autorizimet e vjetra. Ps: Jo ato qe kane ngelur njesoj, ato do ngelen si jane
               
                mesazh = colLidhjetAutorizim.FshiLidhjeAutorizim(colLidhjetAutorizimiPara, idPerdorues, dbAdmin);
                if (!mesazh.Status)
                {
                    
                    return mesazh;
                }
                if (oColVleratFushatShtese.Count > 0)
                {
                    OColVleratFushatShtese.ForEach(x => x.IdLidhese = idNjesiAdm);
                    mesazh = OColVleratFushatShtese.Ruaj();
                }
                string shfaqmesazhapolupe = "jo";
                foreach (DbAsete.clsHistorikStatusMagazine hist in colHistorik)
                {
                    DbAsete.clsHistorikStatusMagazine histroikfundit = new DbAsete.clsHistorikStatusMagazine();
                    histroikfundit.merrHistorikMagazinaAktualeSipasIdNjesiAdministrative(IdNjesiAdministrative);
                    hist.IdNjesiAdministrative = IdNjesiAdministrative;
                    mesazh = hist.ruaj(IdNdermarje, idNderviti, idperiudha, this, idnivel, false);
                    if (!mesazh.Status)
                        return mesazh;
                    if (IdStatusDok == 1)
                    {
                        clsDatabaseKontabilitet dbkont = new clsDatabaseKontabilitet();
                        DbQendraKosto.colObjektivaKosto objektivat;
                        List<double> vleratobjektiva; List<double> vleratobjektivamonbaze;
                        List<int> idllogobj;
                        DbAsete.colAQTSeriale colseriale = new DbAsete.colAQTSeriale();
                        colseriale.merrAQTSerialSipasIDMagazine(IdNjesiAdministrative, IdNdermarje, dataNdryshimStatus);
                        if (colseriale.Count > 0)
                        {
                            FleteKontabel = clsKokaFleteKontabel.gjeneroKontabilizimNdryshimStatusiMagazine(IdNjesiAdministrative, idnivel, IdKonfig, hist.DataStatusit, Kodi + hist.DataStatusit.ToString("ddMMyyyy"), IdNdermarje, idNderviti, IdPerdorues, DateTime.Now, "Nga ndryshimi i statusit te magazines", 0, 21, idperiudha, 23, out objektivat, out vleratobjektiva, out vleratobjektivamonbaze, out idllogobj, IdDegeAdministrative, 0, 0, out shfaqmesazhapolupe, 0, new DbQendraKosto.colTrupiQendraKosto(), colseriale, histroikfundit.IdStatusMagazine, hist.IdStatusMagazine, dbkont, rm, ci);
                            if (FleteKontabel.OColTrupi.Count > 0)
                            {
                                FleteKontabel.IdGjenerues = IdNjesiAdministrative;
                                FleteKontabel.Kontabilizuar = true;

                                mesazh = FleteKontabel.Ruaj(dbkont);
                                if (!mesazh.Status)
                                    return mesazh;
                            }
                        }
                    }
                }
                return mesazh;
            }
            catch (Exception e)
            {
                return new clsMesazh(false, e.Message);
            }
        }

        /// <summary>
        /// Fshin objektin e  njesise administrative ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.fshiNjesiAdministrative"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public clsMesazh fshi(int idperdorues, int idndermarje)
        {
            using (var scope=new MyTransactionScope())
            {

                clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();

                clsMesazh u_fshi = data.fshiNjesiAdministrativeStatus(IdNjesiAdministrative, idperdorues);
                if (!u_fshi.Status)
                {
                    return u_fshi;
                }
                DbAsete.colHistorikStatusMagazine colHistorik = new DbAsete.colHistorikStatusMagazine();
                colHistorik.merrHistorikMagazinaSipasIdNjesiAdministrative(IdNjesiAdministrative);
                foreach (DbAsete.clsHistorikStatusMagazine historik in colHistorik)
                {
                    historik.fshi(idndermarje, idperdorues);
                    if (!u_fshi.Status)
                    {
                        //   data.rollbackTransaksion();
                        return u_fshi;
                    }
                }
                //  data.commitTransaksion();
                scope.Complete();
                return u_fshi;
            }
        }

        /// <summary>
        /// merr te gjithe njesite administrative te nje ndermarje nga tabela perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.ktheGjitheNjesiAdministrative"/> 
        /// </summary>
        /// <returns > nje objekt colNjesiAdministrative me te gjitha njesite administrative te ndermarjeso</returns>
        public colNjesiAdministrative merriTeGjithe()
        {
            colNjesiAdministrative data = new colNjesiAdministrative();
            data.mbushGjitheNjesiAdministrative(IdNdermarje, IdPerdorues);
            //clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            //return data.merrGjitheNjesiAdministrative(this.idNdermarrje); //i kalohet idNdermarje
            return data;
        }

        /// <summary>
        /// Metode e klases, jo e objektit. kthen kodin e njesise administrative sipas id
        /// </summary>
        /// <param name="idNjesiAdm">id e njesise administrative</param>
        /// <returns>kodi i njesise administrative</returns>
        public static string ktheKodiNjesiAdministrativeSipasiD(int idNjesiAdm, int idPerdorues)
        {
            using (clsDatabaseRegjistrim dbNjesiAdministrative = new clsDatabaseRegjistrim())
            {
                return dbNjesiAdministrative.ktheKodiNjesiAdministrativeSipasiD(idNjesiAdm, idPerdorues);
            }
        }
        public static string ktheKodiNjesiAdministrativeSipasiD(int idNjesiAdm, int idPerdorues,clsDatabaseRegjistrim dbNjesiAdministrative)
        {
            
                return dbNjesiAdministrative.ktheKodiNjesiAdministrativeSipasiD(idNjesiAdm, idPerdorues);
           
        }

        public static string ktheKodiNjesiAdministrativeSipasiDPaAutorizime(int idNjesiAdm)
        {
            using (clsDatabaseRegjistrim dbNjesiAdministrative = new clsDatabaseRegjistrim())
            {
                return dbNjesiAdministrative.ktheKodiNjesiAdministrativeSipasiDPaAutorizime(idNjesiAdm);
            }
        }
        public static string kthePershkrimNjesiAdministrativeSipasiDPaAutorizime(int idNjesiAdm)
        {
            using (clsDatabaseRegjistrim dbNjesiAdministrative = new clsDatabaseRegjistrim())
            {
                return dbNjesiAdministrative.kthePershkrimNjesiAdministrativeSipasiD(idNjesiAdm);
            }
        }

        /// <summary>
        /// MODULI ASETE:
        /// Metode per te marre kohezgjatjen e magazines.
        /// </summary>
        /// <param name="idNjesiAdm">(int) Id e njesise administrative.</param>
        /// <returns>Kthen float kohezgjatjen e njesise administrative.</returns>
        public static double ktheKohezgjatjeNjesiAdministrativeSipasiDPaAutorizime(int idNjesiAdm)
        {
            using (clsDatabaseRegjistrim dbNjesiAdministrative = new clsDatabaseRegjistrim())
            {
                return dbNjesiAdministrative.ktheKohezgjatjeNjesiAdministrativeSipasiDPaAutorizime(idNjesiAdm);
            }
        }

        public static double ktheKohezgjatjeNjesiAdministrativeSipasiDPaAutorizime(int idNjesiAdm, clsDatabaseRegjistrim dbNjesiAdministrative)
        {
            return dbNjesiAdministrative.ktheKohezgjatjeNjesiAdministrativeSipasiDPaAutorizime(idNjesiAdm);
        }

        public static bool ekziston(string kod, int idndermarje)
        {
            using (clsDatabaseRegjistrim db = new clsDatabaseRegjistrim())
            {
                return db.ekzistonKodNjesiAdministrative(kod, idndermarje);
            }
        }

        public static bool ekziston(string kod, int idndermarje, clsDatabaseRegjistrim db)
        {
            return db.ekzistonKodNjesiAdministrative(kod, idndermarje);
        }

        public static bool eshteAktiveKaAutorizimeDheJoFshireNjesiAdministrative(int idNjesiAdministrative, int idPerdorues)
        {
            using (clsDatabaseRegjistrim db = new clsDatabaseRegjistrim())
            {
                return db.eshteAktiveKaAutorizimeDheJoFshireNjesiAdministrative(idNjesiAdministrative, idPerdorues);
            }
        }

        public static string ktheEmailSipasId(int idNjesiAdministrative)
        {
            using (clsDatabaseRegjistrim db = new clsDatabaseRegjistrim())
            {
                return db.ktheEmailMagazina(idNjesiAdministrative);
            }
        }

        public static string ktheEmailSipasIds(string idNjesiAdministrative)
        {
            using (clsDatabaseRegjistrim db = new clsDatabaseRegjistrim())
            {
                return db.ktheEmailePerMagazinat(idNjesiAdministrative);
            }
        }

        public static int ktheIdKonfigSipasId(int idNjesiAdministrative)
        {
            using (clsDatabaseRegjistrim db = new clsDatabaseRegjistrim())
            {
                return db.ktheIdKonfigMagazina(idNjesiAdministrative);
            }
        }

        public static clsNjesiAdministrative ktheMagazineSipasIdNeseEkziston(int idMag)
        {
            using (clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim())
                return clsNjesiAdministrative.ktheMagazineSipasIdNeseEkziston(idMag, dbRegj);
            
        }
        
        public static clsNjesiAdministrative ktheMagazineSipasIdNeseEkziston(int idMag, clsDatabaseRegjistrim dbRegj)
        {
            clsNjesiAdministrative mag = new clsNjesiAdministrative(idMag, dbRegj);
            if (mag.IdNjesiAdministrative > 0)
                return mag;
            return null;
        }

        #endregion

        #region Metoda Internal
        internal void mbushNjesiAdministrative(clsNjesiAdministrative njesia)
        {
            idNjesiAdm = njesia.idNjesiAdm;
            kodi = njesia.kodi;
            pershkrimi = njesia.pershkrimi;
            adresa = njesia.adresa;
            idInventarizimi = njesia.idInventarizimi;
            ndjekjeGjendje = njesia.ndjekjeGjendje;
            aktiv = njesia.aktiv;
            idNdermarrje = njesia.idNdermarrje;
            idPerdorues = njesia.idPerdorues;
            dateRegjistrimi = njesia.dateRegjistrimi;
            idKonfig = njesia.idKonfig;
            idDegeAdministrative = njesia.idDegeAdministrative;
            idStatusDok = njesia.idStatusDok;
            dtKrijimi = njesia.dtKrijimi;
            dtModifikimi = njesia.dtModifikimi;
            degeAdministrative = njesia.degeAdministrative;
            oColLidhjetAutorizim = njesia.oColLidhjetAutorizim;
            idLlojMagazine = njesia.idLlojMagazine;
            idStatusAktualMagazine = njesia.idStatusAktualMagazine;
            dataNdryshimStatus = njesia.dataNdryshimStatus;
            idHistorikFundit = njesia.idHistorikFundit;
            kohezgjatja = njesia.kohezgjatja;
            colHistorik = njesia.colHistorik;
            fleteKontabel = njesia.fleteKontabel;
            koordinata = njesia.koordinata;
            celPerdoruesTollonash = njesia.celPerdoruesTollonash;
            shenime = njesia.shenime;
            telefon = njesia.telefon;
            rreshti = njesia.rreshti;
            llojLayeri = njesia.llojLayeri;
            idMagPrind = njesia.idMagPrind;
            email = njesia.email;
            oColVleratFushatShtese = njesia.oColVleratFushatShtese;
            oArkiva = njesia.oArkiva;
            HfArkiva = njesia.HfArkiva;
            kodMagPrindi = njesia.kodMagPrindi;
            qendraKostos = njesia.qendraKostos;
            idSkemaQendraKosto = njesia.idSkemaQendraKosto;
            kontrollGjendjeDet1 = njesia.kontrollGjendjeDet1;
            kontrollGjendjeDet2 = njesia.kontrollGjendjeDet2;
            llojQendre = njesia.llojQendre;
            ownShop = njesia.ownShop;
            tipiMag = njesia.tipiMag;
            qyteti = njesia.qyteti;
        }
        /// <summary>
        /// mbush njesine administrative nga databaza
        /// </summary>
        /// <param name="dbDataRowNjesiAdministrative">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushNjesiAdministrative(DataRow dbDataRowNjesiAdministrative)
        {
            ImbLogger.LogTraceShitje("Filloi metoda mbushNjesiAdministrative nga Db");
            if (dbDataRowNjesiAdministrative != null)
            {
                try
                {
                    int.TryParse(dbDataRowNjesiAdministrative["IDNJESIADM"].ToString(), out idNjesiAdm);
                    kodi = dbDataRowNjesiAdministrative["KODI"].ToString();
                    pershkrimi = dbDataRowNjesiAdministrative["PERSHKRIMI"].ToString();
                    adresa = dbDataRowNjesiAdministrative["ADRESA"].ToString();
                    int.TryParse(dbDataRowNjesiAdministrative["IDINVENTARIZIMI"].ToString(), out idInventarizimi);
                    bool.TryParse(dbDataRowNjesiAdministrative["NDJEKJEGJENDJE"].ToString(), out ndjekjeGjendje);
                    bool.TryParse(dbDataRowNjesiAdministrative["KONTROLLGJENDJEDET1"].ToString(), out kontrollGjendjeDet1);
                    bool.TryParse(dbDataRowNjesiAdministrative["KONTROLLGJENDJEDET2"].ToString(), out kontrollGjendjeDet2);
                    bool.TryParse(dbDataRowNjesiAdministrative["AKTIV"].ToString(), out aktiv);
                    int.TryParse(dbDataRowNjesiAdministrative["IDNDERMARJE"].ToString(), out idNdermarrje);
                    int.TryParse(dbDataRowNjesiAdministrative["IDPERDORUESI"].ToString(), out idPerdorues);
                    DateTime.TryParse(dbDataRowNjesiAdministrative["DATEREGJISTRIMI"].ToString(), out dateRegjistrimi);
                    int.TryParse(dbDataRowNjesiAdministrative["IDKONFIG"].ToString(), out idKonfig);
                    int.TryParse(dbDataRowNjesiAdministrative["IDDEGEADMINISTRATIVE"].ToString(), out idDegeAdministrative);
                    int.TryParse(dbDataRowNjesiAdministrative["IDSTATUSDOK"].ToString(), out idStatusDok);
                    DateTime.TryParse(dbDataRowNjesiAdministrative["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRowNjesiAdministrative["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    int.TryParse(dbDataRowNjesiAdministrative["IDLLOJMAGAZINE"].ToString(), out idLlojMagazine);
                    int.TryParse(dbDataRowNjesiAdministrative["ID_STATUS_NJESIADMIN"].ToString(), out idStatusAktualMagazine);
                    DateTime.TryParse(dbDataRowNjesiAdministrative["DATA_NDRYSHIM_STATUS"].ToString(), out dataNdryshimStatus);
                    int.TryParse(dbDataRowNjesiAdministrative["ID_HISTORIK_FUNDIT"].ToString(), out idHistorikFundit);
                    float.TryParse(dbDataRowNjesiAdministrative["KOHEZGJATJA"].ToString(), out kohezgjatja);
                    //clsDatabaseAdmin dbadm = new clsDatabaseAdmin(db );
                    //oColLidhjetAutorizim = new DbAdmin.colLidhjetAutorizim(idNjesiAdm, "Magazina", dbadm);
                    colHistorik = new DbAsete.colHistorikStatusMagazine();
                    koordinata = dbDataRowNjesiAdministrative["KOORDINATA"].ToString();
                    bool.TryParse(dbDataRowNjesiAdministrative["CELPERDORUESTOLLONASH"].ToString(), out celPerdoruesTollonash);
                    shenime = dbDataRowNjesiAdministrative["SHENIME"].ToString();
                    telefon = Convert.ToString(dbDataRowNjesiAdministrative["TELEFON"]);
                    int.TryParse(dbDataRowNjesiAdministrative["LLOJLAYERI"].ToString(), out llojLayeri);
                    int.TryParse(dbDataRowNjesiAdministrative["IDMAGPRIND"].ToString(), out idMagPrind);
                    int.TryParse(dbDataRowNjesiAdministrative["IDELEMENTIPERINTEGRIM"].ToString(), out idElementiPerIntegrim);
                    int.TryParse(dbDataRowNjesiAdministrative["QENDRA_KOSTOS"].ToString(), out qendraKostos);
                    int.TryParse(dbDataRowNjesiAdministrative["IDSKEMAQENDRAKOSTO"].ToString(), out idSkemaQendraKosto);

                    int.TryParse(dbDataRowNjesiAdministrative["LLOJQENDRE"].ToString(), out llojQendre);
                    object objEmail = dbDataRowNjesiAdministrative["EMAIL"];
                    email = (objEmail != null && objEmail != DBNull.Value) ? Convert.ToString(objEmail) : "";
                    bool.TryParse(dbDataRowNjesiAdministrative["OWNSHOP"].ToString(), out ownShop);
                    if (clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizim())
                    {
                        tipiMag = dbDataRowNjesiAdministrative["TIPIMAG"].ToString();
                        int.TryParse(dbDataRowNjesiAdministrative["QYTETI"].ToString(), out qyteti);

                    }
                    ImbLogger.LogTraceShitje("Mbaroi metoda mbushNjesiAdministrative nga Db");
                    return true;
                }
                catch (InvalidCastException)
                {
                    ImbLogger.LogErrorShitje("ERROR: Gabim gjate cast-it!");
                    throw new MyException("ERROR: Gabim gjate cast-it!");
                }
                catch (Exception)
                {
                    ImbLogger.LogErrorShitje("ERROR: Gabim gjate marrjes se njesise administrative nga db-ja!");
                    throw new MyException("ERROR: Gabim gjate marrjes se njesise administrative nga db-ja!");
                }
            }
            else
                return false;
        }

        #endregion
    }
}