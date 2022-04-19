using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DbCore.DbAdmin;
using System.Data;
using System.Globalization;
using System.Resources;
using DbCore.IMBUtils.Validation;
using DbCore.IMBUtils.Logging;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne  piken e shitjes furnizimi
    ///  (Te dhenat  merren nga tabela : T_PIKESHITJEFURNIZIMI)
    /// </summary>
    public class clsPikeShitjeFurnizimi
    {
        #region Atribute

        private int idPikeShitjeFurnizimi;
        private string kodi;
        private string pershkrimi;
        private string adresa;
        private bool aktiv;
        private int idNdermarrje;
        private int idPerdorues;
        private DateTime dateRegjistrimi;
        private int idKonfig;
        private bool shitjeFurnizim;
        private int idDegeAdministrative;
        private int idStatusDok;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private string degeAdministrative;
        private string koordinata;
        private DataRow rreshti;

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases me parametra
        /// </summary>
        /// <param name="adr"> adresa e njesise administrative</param>
        /// <param name="akt">gjendja nese eshte aktive apo jo</param>
        /// <param name="dtRegj"> data e regjistrimit</param>
        /// <param name="idAut">id autorizimi</param>
        /// <param name="idInv">id inventarizimi</param>
        /// <param name="idNjes"> id ritese e njesise administrative</param>
        /// <param name="idPerd">id e perdoruesit qe e ka ruajtur</param>
        /// <param name="kod">kodi i njesise administrative</param>
        /// <param name="nderm">id e ndermarjes</param>
        /// <param name="ndjGjend"> ndjekje gjendje </param>
        /// <param name="pershk"> pershkrimi</param>
        public clsPikeShitjeFurnizimi(int idpikeshitjefurnizimi, string kod, string pershk, string adr, bool akt, int nderm, int idPerd, DateTime dtRegj, int idKonfig, bool shitjefurnizim, int iddegeadministrative, int idstatusdok, string koordinata)
        {
            idPikeShitjeFurnizimi = idpikeshitjefurnizimi;
            kodi = kod;
            pershkrimi = pershk;
            adresa = adr;
            aktiv = akt;
            idNdermarrje = nderm;
            idPerdorues = idPerd;
            dateRegjistrimi = dtRegj;
            this.idKonfig = idKonfig;
            this.shitjeFurnizim = shitjefurnizim;
            this.idDegeAdministrative = iddegeadministrative;
            this.idStatusDok = idstatusdok;
            this.koordinata = koordinata;
        }
        
        public clsPikeShitjeFurnizimi(int idpikeshitjefurnizimi, string kod, string pershk, string adr, bool akt, int nderm, int idPerd, DateTime dtRegj, int idKonfig, bool shitjefurnizim, int iddegeadministrative, string degeadm, bool shtim, string koordinata, ResourceManager rm, CultureInfo ci)
        {
            try
            {
                idPikeShitjeFurnizimi = idpikeshitjefurnizimi;
                kodi = kod;
                pershkrimi = pershk;
                adresa = adr;
                aktiv = akt;
                idNdermarrje = nderm;
                idPerdorues = idPerd;
                dateRegjistrimi = dtRegj;
                this.idKonfig = idKonfig;
                this.shitjeFurnizim = shitjefurnizim;
                this.idDegeAdministrative = iddegeadministrative;
                this.idStatusDok = 1;
                this.degeAdministrative = degeadm;
                this.koordinata = koordinata;
                clsMesazh mesazh = this.kontrolloPikeShitje(shtim, rm, ci);
                if (!mesazh.Status)
                    throw new Exception(mesazh.PershkrimMesazhi);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// konstruktor me 2 parametra
        /// </summary>
        /// <param name="kodi">kodi i njesise administrative</param>
        /// <param name="idNderm">id e ndermarrjes</param>
        public clsPikeShitjeFurnizimi(string kodi, int idNderm)
        {
            clsDatabaseRegjistrim dbNjesiAdministrative = new clsDatabaseRegjistrim();
            if (!mbushPikeShitjeFurnizimi(dbNjesiAdministrative.kthePikeShitjeFurnizimiSipasKodit(kodi, idNderm)))
                idPikeShitjeFurnizimi = -1;
            dbNjesiAdministrative.Dispose();
        }

        /// konstruktor me 2 parametra
        /// </summary>
        /// <param name="kodi">kodi i njesise administrative</param>
        /// <param name="idNderm">id e ndermarrjes</param>
        public clsPikeShitjeFurnizimi(string kodi, int idNderm, clsDatabaseRegjistrim dbPikeShitje)
        {
            mbushPikeShitjeFurnizimi(dbPikeShitje.TransCache.getPikeShitjeFurnizimi(kodi, idNderm, dbPikeShitje));
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idpikeshitjefurnizimi">id pikeshitjefurnizimi</param>
        public clsPikeShitjeFurnizimi(int idpikeshitjefurnizimi)
        {
            clsDatabaseRegjistrim dbNjesiAdministrative = new clsDatabaseRegjistrim();
            mbushPikeShitjeFurnizimi(dbNjesiAdministrative.kthePikeShitjeFurnizimiSipasiD(idpikeshitjefurnizimi));
            dbNjesiAdministrative.Dispose();
        }

        /// <summary>
        /// Konstruktori i klases pa parametra
        /// </summary>
        public clsPikeShitjeFurnizimi()
        {

        }

        public clsPikeShitjeFurnizimi(DataRow rreshti)
        {
            
            mbushPikeShitjeFurnizimi(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdPikeShitjeFurnizimi
        {
            get { return idPikeShitjeFurnizimi; }
            set { idPikeShitjeFurnizimi = value; }
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
        /// Kthen/Vendos gjendjen e njesise administrative nese eshte aktiv apo jo.
        /// </summary>
        public Boolean Aktiv
        {
            get { return aktiv; }
            set { aktiv = value; }
        }

        /// <summary>
        /// Kthen/Vendos  ne eshte pike shitje apo pike furnizimi
        /// </summary>
        public Boolean ShitjeFurnizimi
        {
            get { return shitjeFurnizim; }
            set { shitjeFurnizim = value; }
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
        
        /// <summary>
        /// Kthen/Vendos ID-ne e konfigurimit
        /// </summary>
        public int IdDegeAdministrative
        {
            get { return idDegeAdministrative; }
            set { idDegeAdministrative = value; }
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

        /// <summary>
        /// Kthen/Vendos koordinata e pikes se shitjes/furnizimit.
        /// </summary>
        public string Koordinata
        {
            get { return koordinata; }
            set { koordinata = value; }
        }

        #endregion

        #region Metoda Publike

        public clsPikeShitjeFurnizimi krijoPerImport(string kodi, string pershkrimi, string adresa, string degaadministrative, bool aktiv, int idndermarje, int idperdoruesi, DateTime dateregjistrimi, int idkonfig, bool pikeshitjefurnizimi, bool shtim, ResourceManager rm, CultureInfo ci)
        {
            try
            {
                idDegeAdministrative = new clsDegeAdministrative(degaadministrative, idndermarje).IdDegeAdministrative;
                return new clsPikeShitjeFurnizimi(0, kodi, pershkrimi, adresa, aktiv, idndermarje, idperdoruesi, dateregjistrimi, idkonfig, pikeshitjefurnizimi, idDegeAdministrative, degaadministrative, shtim, "", rm, ci);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        
        private clsMesazh kontrolloPikeShitje(bool shtim, ResourceManager rm, CultureInfo ci)
        {
            if (kodi == "")
                return new clsMesazh(false, "Plotesoni kodin e pikes!");
            clsMesazh kontrollkodi = clsFunksione.kontrolloKaraktereMeMesazh(kodi, FusheKontrolli.Kodi, false);
            if (!kontrollkodi.Status)
                return kontrollkodi;
            if (pershkrimi == "")
                return new clsMesazh(false, "Plotesoni pershkrimin e pikes!");
            clsMesazh kontrollpershkrimi = clsFunksione.kontrolloKaraktereMeMesazh(pershkrimi, FusheKontrolli.Pershkrimi, true);
            if (!kontrollpershkrimi.Status)
                return kontrollpershkrimi;

            if (shtim && DbCore.DbRegjistrim.clsPikeShitjeFurnizimi.ekziston(kodi, idNdermarrje))
            {
                return new clsMesazh(false, "Ekziston nje pike me kete kod. Ju lutem shenoni nje kod tjeter!");
            }
            if (degeAdministrative != "")
            {
                if (!DbRegjistrim.clsDegeAdministrative.ekziston(degeAdministrative, idNdermarrje))
                {
                    return new clsMesazh(false, "Dega administrative nuk ekziston!");
                }
                DbRegjistrim.clsDegeAdministrative dega = new clsDegeAdministrative(degeAdministrative, idNdermarrje);
                if (!dega.Aktiv)
                    return new clsMesazh(false, "Dega administrative nuk eshte aktive!");
            }

            return new clsMesazh(true, "Kontrollet e pikes u kaluan me sukses");
        }
        
        /// <summary>
        /// Ruan objektin e  njesise administrative ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.ruajNjesiAdministrative"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>
        public clsMesazh ruaj(clsDatabaseRegjistrim db)
        {
            int id;
            clsMesazh u_ruajt = db.ruajPikeShitjeFurnizimi(out id, this.Kodi, this.Pershkrimi, this.Adresa, this.Aktiv, this.DateRegjistrimi, this.IdNdermarje, this.IdPerdorues, this.IdKonfig, this.shitjeFurnizim, this.idDegeAdministrative, this.idStatusDok, this.koordinata);
            this.idPikeShitjeFurnizimi = id;
            return u_ruajt;
        }
        
        public clsMesazh ruaj(IDictionary<string, object> hfNrAutoKF)
        {
            bool kaNdryshimNumri;
            clsDatabaseRegjistrim db = new clsDatabaseRegjistrim();
            db.beginTransaksion();
            clsMesazh mesazhKontrolli = kontrolloPikeShitjeFurnizimi(out kaNdryshimNumri, db, hfNrAutoKF, false);

            if (!mesazhKontrolli.Status)
            {
                return mesazhKontrolli;
            }
            clsMesazh u_ruajt = ruaj(db);
            if (!u_ruajt.Status)
            {
                db.rollbackTransaksion();
                return u_ruajt;
            }
            db.commitTransaksion();

            if (kaNdryshimNumri)
                return mesazhKontrolli;
            return u_ruajt;
        }

        private clsMesazh kontrolloPikeShitjeFurnizimi(out bool kaNdryshimNrAuto, clsDatabaseRegjistrim db, IDictionary<string, object> hfNrAutoKF, bool modifikim)
        {
            kaNdryshimNrAuto = false;
            if (kodi == "")
                return new clsMesazh(false, "Kodi i Pike Shitje/ Furnizimi nuk mund te jete bosh");
            if (!modifikim)
            {
                clsMesazh mes = new clsMesazh();
                if (hfNrAutoKF != null)
                {
                    mes = kontrolloNrAutoKF(out kaNdryshimNrAuto, db, hfNrAutoKF);
                    if (!mes.Status)
                        return mes;
                }

                if (db.ekzistonKodPikeShitjeFurnizim(kodi, idNdermarrje))
                    return new clsMesazh(false, "Ekziston nje pike shitje/furnizim me kete kod!");
                return new clsMesazh(mes.Status, mes.PershkrimMesazhi);
            }
            return new clsMesazh(true, "Kontrollet u kaluan me sukses");
        }

        private clsMesazh kontrolloNrAutoKF(out bool kaNdryshimNumri, clsDatabaseRegjistrim db, IDictionary<string, object> hfNrAutoKf)
        {
            clsDatabaseAdmin dbadm = new clsDatabaseAdmin(db );
            List<NrAuto> list = DbAdmin.clsNrAutom.kontrollogjithenumrat(dbadm, hfNrAutoKf, DateTime.Today);
            if (NrAuto.ktheVlerenEre(list, "Kodi") != "")
                this.Kodi = NrAuto.ktheVlerenEre(list, "Kodi");
            DbCore.clsMesazh mes = NrAuto.ruajvlera(out kaNdryshimNumri, list, DateTime.Today, this.IdPerdorues, this.idNdermarrje, dbadm);
            return new clsMesazh(mes.Status, mes.PershkrimMesazhi);
        }

        /// <summary>
        /// Modifikon objektin e  njesise administrative ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.modifikoNjesiAdministrative"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo</returns>
        public clsMesazh modifiko()
        {
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            clsMesazh u_modifikua = data.modifikoPikeShitjeFurnizimi(this.idPikeShitjeFurnizimi, this.Kodi, this.Pershkrimi, this.Adresa, this.Aktiv, this.DateRegjistrimi, this.IdNdermarje, this.IdPerdorues, this.IdKonfig, this.shitjeFurnizim, this.idDegeAdministrative, this.idStatusDok, this.koordinata);
            data.Dispose();
            return u_modifikua;
        }

        /// <summary>
        /// Fshin objektin e  njesise administrative ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.fshiNjesiAdministrative"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public clsMesazh fshi()
        {
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            clsMesazh u_fshi = data.fshiPikeShitjeFurnizimiStatus(this.idPikeShitjeFurnizimi, this.idPerdorues);
            data.Dispose();
            return u_fshi;
        }

        /// <summary>
        /// merr te gjithe njesite administrative te nje ndermarje nga tabela perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.ktheGjitheNjesiAdministrative"/> 
        /// </summary>
        /// <returns > nje objekt colNjesiAdministrative me te gjitha njesite administrative te ndermarjeso</returns>
        public colPikaShitjeFurnizimi merriTeGjithe()
        {
            colPikaShitjeFurnizimi data = new colPikaShitjeFurnizimi();
            data.mbushGjithePikeShitjeFurnizimi(this.IdNdermarje);
            //clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            //return data.merrGjitheNjesiAdministrative(this.idNdermarrje); //i kalohet idNdermarje
            return data;
        }

        /// <summary>
        /// merr objektin e  njesise administrative sipas kodit nga tabela perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.ktheNjesiAdministrativeSipasKodit"/> 
        /// </summary>
        /// <returns > nje objekt clsPikeShitjeFurnizimi me njesine administrative te kerkuar</returns>
        public clsPikeShitjeFurnizimi merrPikeSipasKodit()
        {
            clsPikeShitjeFurnizimi data = new clsPikeShitjeFurnizimi(this.Kodi, this.IdNdermarje);
            //clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            //return data.merrNjesiAdministrativeSipasKodit(this);
            return data;
        }

        /// <summary>
        /// Metode e klases, jo e objektit. kthen kodin e njesise administrative sipas id
        /// </summary>
        /// <param name="idNjesiAdm">id e njesise administrative</param>
        /// <returns>kodi i njesise administrative</returns>
        public static string mbushKodiPikeShitjeFurnizimiSipasiD(int idNjesiAdm)
        {
            clsDatabaseRegjistrim dbNjesiAdministrative = new clsDatabaseRegjistrim();
            string kodi = dbNjesiAdministrative.ktheKodiPikeShitjeFurnizimiSipasiD(idNjesiAdm);
            dbNjesiAdministrative.Dispose();
            return kodi;
        }
        
        public static bool ekziston(string kodi, int idndermarje)
        {
            clsDatabaseRegjistrim db = new clsDatabaseRegjistrim();
            bool ekziston = db.ekzistonKodPikeShitjeFurnizim(kodi, idndermarje);
            db.Dispose();
            return ekziston;
        }
        
        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush njesine administrative nga databaza
        /// </summary>
        /// <param name="dbDataRowNjesiAdministrative">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushPikeShitjeFurnizimi(DataRow dbDataRowNjesiAdministrative)
        {
            if (dbDataRowNjesiAdministrative != null)
            {
                try
                {
                    int.TryParse(dbDataRowNjesiAdministrative["IDPIKESHITJEFURNIZIMI"].ToString(), out idPikeShitjeFurnizimi);
                    kodi = dbDataRowNjesiAdministrative["KODI"].ToString();
                    pershkrimi = dbDataRowNjesiAdministrative["PERSHKRIMI"].ToString();
                    adresa = dbDataRowNjesiAdministrative["ADRESA"].ToString();
                    bool.TryParse(dbDataRowNjesiAdministrative["AKTIV"].ToString(), out aktiv);
                    int.TryParse(dbDataRowNjesiAdministrative["IDNDERMARJE"].ToString(), out idNdermarrje);
                    int.TryParse(dbDataRowNjesiAdministrative["IDPERDORUESI"].ToString(), out idPerdorues);
                    DateTime.TryParse(dbDataRowNjesiAdministrative["DATEREGJISTRIMI"].ToString(), out dateRegjistrimi);
                    int.TryParse(dbDataRowNjesiAdministrative["IDKONFIG"].ToString(), out idKonfig);
                    bool.TryParse(dbDataRowNjesiAdministrative["SHITJEFURNIZIM"].ToString(), out shitjeFurnizim);
                    int.TryParse(dbDataRowNjesiAdministrative["IDDEGEADMINISTRATIVE"].ToString(), out idDegeAdministrative);
                    int.TryParse(dbDataRowNjesiAdministrative["IDSTATUSDOK"].ToString(), out idStatusDok);
                    DateTime.TryParse(dbDataRowNjesiAdministrative["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRowNjesiAdministrative["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    koordinata = dbDataRowNjesiAdministrative["KOORDINATA"].ToString();
                    return true;
                }
                catch (InvalidCastException)
                {
                    ImbLogger.LogErrorShitje("ERROR: Gabim gjate marrjes se njesise administrative nga db-ja");
                    throw new Exception("ERROR: Gabim gjate marrjes se njesise administrative nga db-ja");
                }
            }
            else
            {
                ImbLogger.LogTraceShitje("Nuk u mor gje per njesia administrative nga db-ja");
                return false;
            }
        }

        public clsMesazh mbushPikeShitjeFurnizimi(clsPikeShitjeFurnizimi pika)
        {
            ImbLogger.LogTraceShitje("Filloi metoda mbushPikeShitjeFurnizimi");
            idPikeShitjeFurnizimi = pika.IdPikeShitjeFurnizimi;
            kodi = pika.Kodi;
            pershkrimi = pika.Pershkrimi;
            adresa = pika.Adresa;
            aktiv = pika.Aktiv;
            idNdermarrje = pika.IdNdermarje;
            idPerdorues = pika.IdPerdorues;
            dateRegjistrimi = pika.DateRegjistrimi;
            idKonfig = pika.IdKonfig;
            shitjeFurnizim = pika.ShitjeFurnizimi;
            idDegeAdministrative = pika.IdDegeAdministrative;
            idStatusDok = pika.IdStatusDok;
            dtKrijimi = pika.DtKrijimi;
            dtModifikimi = pika.DtModifikimi;
            koordinata = pika.Koordinata;
            ImbLogger.LogTraceShitje("Mbaroi metoda mbushPikeShitjeFurnizimi");
            ImbLogger.LogTraceShitje($"Mbushja e pikes {pika.Kodi} u be me sukses!");
            return new clsMesazh(true, $"Mbushja e pikes {pika.Kodi} u be me sukses!");
        }

        #endregion
    }
}