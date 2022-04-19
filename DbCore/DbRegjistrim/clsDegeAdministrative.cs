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
using DbCore.IMBUtils.Fiskalizimi.Controls;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne  dege administrative
    ///  (Te dhenat  merren nga tabela : T_DEGEADMINISTRATIVE)
    /// </summary>
    public class clsDegeAdministrative
    {
        #region Atribute

        private int idDegeAdministrative;
        private string kodi;
        private string pershkrimi;
        private string adresa;
        private bool aktiv;
        private int idNdermarrje;
        private int idPerdorues;
        private DateTime dateRegjistrimi;
        private int idKonfig;
        private int idStatusDok;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private int qenderKosto;
        private int idSkemaQendraKosto;
        private int llojQendre;
        private string qendra;
        private DataRow rreshti;
        private string kodNjesieBiznesi;
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
        public clsDegeAdministrative(int iddegeadministrative, string kod, string pershk, string adr, bool akt, int nderm, int idPerd, DateTime dtRegj, int idKonfig, int idstatusdok, int qenderkosto, int idskemakosto, int llojqendre, string kodNjesiBiznesi)
        {
            this.idDegeAdministrative = iddegeadministrative;
            kodi = kod;
            pershkrimi = pershk;
            adresa = adr;

            aktiv = akt;
            idNdermarrje = nderm;
            idPerdorues = idPerd;
            dateRegjistrimi = dtRegj;
            this.idKonfig = idKonfig;
            idStatusDok = idstatusdok;
            this.qenderKosto = qenderkosto;
            this.idSkemaQendraKosto = idskemakosto;
            this.llojQendre = llojqendre;
            this.kodNjesieBiznesi = kodNjesiBiznesi;

        }
        public clsDegeAdministrative(int iddegeadministrative, string kod, string pershk, string adr, bool akt, int nderm, int idPerd, DateTime dtRegj, int idKonfig, int qenderkosto, int idskemakosto, int llojqendre,string qendra, bool shtim, ResourceManager rm, CultureInfo ci, string kodNjesieBiznesi)
        {
            try
            {
                this.idDegeAdministrative = iddegeadministrative;
                kodi = kod;
                pershkrimi = pershk;
                adresa = adr;

                aktiv = akt;
                idNdermarrje = nderm;
                idPerdorues = idPerd;
                dateRegjistrimi = dtRegj;
                this.idKonfig = idKonfig;
                idStatusDok = 1;
                this.qenderKosto = qenderkosto;
                this.idSkemaQendraKosto = idskemakosto;
                this.llojQendre = llojqendre;
                this.qendra = qendra;
                this.kodNjesieBiznesi = kodNjesieBiznesi;
                clsMesazh mesazh = this.kontrolloPikeShitje(shtim, rm, ci);
                if (!mesazh.Status)
                    throw new Exception(mesazh.PershkrimMesazhi);
            }
            catch (Exception e)
            {
                ImbLogger.LogErrorShitje(Convert.ToString(e.Message));
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// konstruktor me 2 parametra
        /// </summary>
        /// <param name="kodi">kodi i njesise administrative</param>
        /// <param name="idNderm">id e ndermarrjes</param>
        public clsDegeAdministrative(string kodi, int idNderm)
        {
            clsDatabaseRegjistrim dbNjesiAdministrative = new clsDatabaseRegjistrim();
            if (!mbushDegeAdministrative(dbNjesiAdministrative.ktheDegeAdministrativeSipasKodit(kodi, idNderm)))
                idDegeAdministrative = -1;
            dbNjesiAdministrative.Dispose();
        }
        public clsDegeAdministrative(string kodi, int idNderm, clsDatabaseRegjistrim dbNjesiAdministrative)
        {
            mbushDegeAdministrative(dbNjesiAdministrative.TransCache.getDegaAdministrative(kodi, idNderm, dbNjesiAdministrative));
        }
        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idpikeshitjefurnizimi">id pikeshitjefurnizimi</param>
        public clsDegeAdministrative(int iddegeadministrative):this(iddegeadministrative,new clsDatabaseRegjistrim())
        {
          
        }
        public clsDegeAdministrative(int iddegeadministrative, clsDatabaseRegjistrim dbNjesiAdministrative)
        {
            if (iddegeadministrative > 0)
                mbushDegeAdministrative(dbNjesiAdministrative.TransCache.getDegaAdministrative(iddegeadministrative, dbNjesiAdministrative));
        }

        /// <summary>
        /// Konstruktori i klases pa parametra
        /// </summary>
        public clsDegeAdministrative()
        {
            ImbLogger.LogTraceShitje("Krijohet nje klase e re DegeAdministrative");
        }

        public clsDegeAdministrative(DataRow rreshti)
        {
            
            mbushDegeAdministrative(rreshti);
        }
        public clsDegeAdministrative(DataRow rreshti, bool konfigurim)
        {

            mbushDegeAdministrativePerKonfigurim(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
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
        public string KodNjesieBiznesi
        {
            get { return kodNjesieBiznesi; }
            set { kodNjesieBiznesi = value; }
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
        /// <summary>
        /// id e qendres se kostos
        /// </summary>
        public int QendraKostos
        {
            get
            {
                return qenderKosto;
            }
            set
            {
                qenderKosto = value;
            }
        }
        /// <summary>
        /// id e skemes se kostos
        /// </summary>
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
        /// <summary>
        /// lloji i qendres 1-qender 2- skeme
        /// </summary>
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
        /// <summary>
        /// kodi i qendres ose i skemes
        /// </summary>
        public string Qendra
        {
            get
            {
                return qendra;
            }
        }
        #endregion

        #region Metoda Publike
        public clsDegeAdministrative krijoPerImport(string kodi, string pershkrimi, string adresa, bool aktiv, int idndermarje, int idperdoruesi, DateTime dateregjistrimi, int idkonfig,int lloji,string qendra, bool shtim, ResourceManager rm, CultureInfo ci, string kodNjesieBiznesi)
        {
            try
            {DbQendraKosto.clsQendraKosto q=new DbQendraKosto.clsQendraKosto ();
                DbQendraKosto.clsKokaSkemaQK s=new DbQendraKosto.clsKokaSkemaQK ();
                if (lloji == 1)
                    q = new DbQendraKosto.clsQendraKosto(qendra, idndermarje);
                else s = new DbQendraKosto.clsKokaSkemaQK(qendra, idndermarje);


                return new clsDegeAdministrative(0, kodi, pershkrimi, adresa, aktiv, idndermarje, idperdoruesi, dateregjistrimi, idkonfig,q.Id,s.IdKoka,lloji,qendra, shtim, rm, ci, kodNjesieBiznesi);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        private clsMesazh kontrolloPikeShitje(bool shtim, ResourceManager rm, CultureInfo ci)
        {
            if (kodi == "")
                return new clsMesazh(false, "Plotesoni kodin e deges!");
            clsMesazh kontrollkodi = clsFunksione.kontrolloKaraktereMeMesazh(kodi, FusheKontrolli.Kodi, false);
            if (!kontrollkodi.Status)
                return kontrollkodi;
            if (pershkrimi == "")
                return new clsMesazh(false, "Plotesoni pershkrimin e deges!");
            clsMesazh kontrollpershkrimi = clsFunksione.kontrolloKaraktereMeMesazh(pershkrimi, FusheKontrolli.Pershkrimi, true);
            if (!kontrollpershkrimi.Status)
                return kontrollpershkrimi;

            if (shtim && DbCore.DbRegjistrim.clsDegeAdministrative.ekziston(kodi, idNdermarrje))
            {
                return new clsMesazh(false, "Ekziston nje dege me kete kod. Ju lutem shenoni nje kod tjeter!");
            }

            if (qendra!= "")
            {
                if (llojQendre == 1)
                {
                    if (!DbCore.DbQendraKosto.clsQendraKosto.ekzistonQK(qendra, idNdermarrje))
                    {
                        return new clsMesazh(false, "Qendra e kostos nuk ekziston!");
                       
                    }
                    DbCore.DbQendraKosto.clsQendraKosto obj = new DbCore.DbQendraKosto.clsQendraKosto(qendra, idNdermarrje);
                    if (!obj.Aktiv)
                    {
                        return new clsMesazh(false, "Qendra e kostos nuk eshte aktive!");
                       
                    }
                    DbCore.DbQendraKosto.colQendraKosto col = new DbCore.DbQendraKosto.colQendraKosto();
                    col.mbushQendraSipasPrindit(obj.Id);
                    if (col.Count > 0)
                    {  return new clsMesazh(false,"Qendra e kostos eshte qender prind dhe nuk mund te zgjidhet!") ;
                       
                    }
                }
                else
                {
                    if (!DbCore.DbQendraKosto.clsKokaSkemaQK.ekzistonSkeme(qendra, idNdermarrje))
                    {
                        return new clsMesazh(false, "Skema e qendrave te kostos nuk ekziston!");
                       
                    }
                }
            }
            return new clsMesazh(true, "Kontrollet e deges u kaluan me sukses");
        }



        /// <summary>
        /// Ruan objektin e  njesise administrative ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.ruajNjesiAdministrative"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>
        public clsMesazh ruaj(clsDatabaseRegjistrim data)
        {
           
            int id;

            clsMesazh u_ruajt = data.ruajDegeAdministrative(out id, this.Kodi, this.Pershkrimi, this.Adresa, this.Aktiv, this.DateRegjistrimi, this.IdNdermarje, this.IdPerdorues, this.IdKonfig, this.idStatusDok, this.qenderKosto,this.idSkemaQendraKosto,this.llojQendre,this.kodNjesieBiznesi, clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizim());
            this.idDegeAdministrative = id;
       
            return u_ruajt;
        }

        public clsMesazh ruaj()
        {
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();

            clsMesazh u_ruajt = ruaj(data);
            data.Dispose();
            return u_ruajt;
        }

        /// <summary>
        /// Modifikon objektin e  njesise administrative ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.modifikoNjesiAdministrative"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo</returns>
        public clsMesazh modifiko(clsDatabaseRegjistrim data)
        {
  
            clsMesazh u_modifikua = data.modifikoDegeAdministrative(this.idDegeAdministrative, this.Kodi, this.Pershkrimi, this.Adresa, this.Aktiv, this.DateRegjistrimi, this.IdNdermarje, this.IdPerdorues, this.IdKonfig, this.idStatusDok,this.qenderKosto,this.idSkemaQendraKosto,this.llojQendre, this.kodNjesieBiznesi, clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizim());
           
            return u_modifikua;
        }
        public clsMesazh modifiko()
        {
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            clsMesazh u_modifikua = modifiko(data);
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
            clsMesazh u_fshi = data.fshiDegeAdministrativeStatus(this.idDegeAdministrative, this.idPerdorues);
            data.Dispose();
            return u_fshi;
        }

        /// <summary>
        /// merr te gjithe njesite administrative te nje ndermarje nga tabela perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.ktheGjitheNjesiAdministrative"/> 
        /// </summary>
        /// <returns > nje objekt colNjesiAdministrative me te gjitha njesite administrative te ndermarjeso</returns>
        public colDegeAdministrative merriTeGjithe()
        {
            colDegeAdministrative data = new colDegeAdministrative();
            data.mbushGjitheDegeAdministrative(this.IdNdermarje);
            //clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            //return data.merrGjitheNjesiAdministrative(this.idNdermarrje); //i kalohet idNdermarje
            return data;
        }

        /// <summary>
        /// merr objektin e  njesise administrative sipas kodit nga tabela perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.ktheNjesiAdministrativeSipasKodit"/> 
        /// </summary>
        /// <returns > nje objekt clsPikeShitjeFurnizimi me njesine administrative te kerkuar</returns>
        public clsDegeAdministrative merrDegeSipasKodit()
        {
            clsDegeAdministrative data = new clsDegeAdministrative(this.Kodi, this.IdNdermarje);
            //clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            //return data.merrNjesiAdministrativeSipasKodit(this);
            return data;
        }

        /// <summary>
        /// Metode e klases, jo e objektit. kthen kodin e njesise administrative sipas id
        /// </summary>
        /// <param name="idNjesiAdm">id e njesise administrative</param>
        /// <returns>kodi i njesise administrative</returns>
        public static string mbushKodiDegeAdministrativeSipasiD(int idNjesiAdm)
        {
            using (clsDatabaseRegjistrim dbNjesiAdministrative = new clsDatabaseRegjistrim())
            {
                string kodi = (dbNjesiAdministrative.ktheKodiDegeAdministrativeSipasiD(idNjesiAdm));
                return kodi;
            }
        }

        public static string mbushPershkrimDegeAdministrativeSipasiD(int idNjesiAdm)
        {
            using (clsDatabaseRegjistrim dbNjesiAdministrative = new clsDatabaseRegjistrim())
            {
                string kodi = (dbNjesiAdministrative.kthePershkrimDegeAdministrativeSipasiD(idNjesiAdm));
                return kodi;
            }
        }
        public static string mbushKodiDegeAdministrativeSipasiD(int idNjesiAdm,clsDatabaseRegjistrim dbNjesiAdministrative)
        {
          
            string kodi = (dbNjesiAdministrative.ktheKodiDegeAdministrativeSipasiD(idNjesiAdm));
        
            return kodi;
        }

        public static bool ekziston(string kodi, int indermarje)
        {
            clsDatabaseRegjistrim db = new clsDatabaseRegjistrim();
            bool sukses = db.ekzistonKodDegeAdministrative(kodi, indermarje);
            db.Dispose();
            return sukses;
        }

        public static bool ekziston(string kodi, int indermarje, clsDatabaseRegjistrim db)
        {
            return db.ekzistonKodDegeAdministrative(kodi, indermarje);
        }

        public static int ktheIdDegeAdminSipasKategorise(int idKategoria, int idGjenerues, clsDatabaseRegjistrim db)
        {
            return db.ktheIdDegeAdminSipasKategorise(idKategoria, idGjenerues);
        }

        public static int ktheIdDegeAdminSipasKategorise(int idKategoria, int idGjenerues)
        {
            using (clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim())
            {
                return ktheIdDegeAdminSipasKategorise(idKategoria, idGjenerues, dbRegj);
            }
        }
        
        public static DataRow ktheDegeAdministrativeSipasiD(int idDege)
        {
            clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim();
            DataRow dr = dbRegj.ktheDegeAdministrativeSipasiD(idDege);
            dbRegj.Dispose();
            return dr;
        }

        public static DataTable ktheDegeAdministrativeSipasIDDt(int idDege)
        {
            clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim();
            DataTable dt = dbRegj.ktheDegeAdministrativeSipasIDDt(idDege);
            dbRegj.Dispose();
            return dt;
        }

        public clsMesazh kontrollotransferim(clsDegeAdministrative kod, int idndermarje, clsDatabaseRegjistrim db, int idperdoruesi)
        {    DbShare.clsKonfigurimAmbjenti konf = new DbShare.clsKonfigurimAmbjenti();
        DbShare.clsDatabaseShare dbshare = new DbShare.clsDatabaseShare(db );
          
                konf.mbushKonfigAmbjSipasKod("DA", idndermarje, dbshare);
            ImbLogger.LogTraceShitje("Transferimi mbaroi me sukses!");
            clsMesazh mesazh = new clsMesazh(true, "Transferimi mbaroi me sukses!");
            if (!db.ekzistonKodDegeAdministrative(kod.Kodi, idndermarje))
            {
                kod.idPerdorues = idperdoruesi;
                kod.IdNdermarje = idndermarje;
                kod.idKonfig = konf.IdKonfigAmbjente;
                kod.idSkemaQendraKosto = 0;
                kod.qenderKosto = 0;
                mesazh = kod.ruaj(db);
                if (!mesazh.Status)
                    return mesazh;
            }
            else
            {
                clsDegeAdministrative kodnderm = new clsDegeAdministrative(kod.Kodi, idndermarje,db);   
                kod.idDegeAdministrative = kodnderm.idDegeAdministrative;
                if (kodnderm.dtModifikimi < kod.dtModifikimi)
                {
                    kod.idPerdorues = idperdoruesi;
                    kod.IdNdermarje = idndermarje;
                    kod.idKonfig = konf.IdKonfigAmbjente;
             
                    kod.idSkemaQendraKosto = 0;
                    kod.qenderKosto = 0;
                    mesazh = kod.modifiko(db);
                    if (!mesazh.Status)
                        return mesazh;
                }

            }
            return mesazh;
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush njesine administrative nga databaza
        /// </summary>
        /// <param name="dbDataRowNjesiAdministrative">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushDegeAdministrative(DataRow dbDataRowNjesiAdministrative)
        {
            if (dbDataRowNjesiAdministrative != null)
            {
                try
                {
                    int.TryParse(dbDataRowNjesiAdministrative["IDDEGEADMINISTRATIVE"].ToString(), out idDegeAdministrative);
                    kodi = dbDataRowNjesiAdministrative["KODI"].ToString();
                    pershkrimi = dbDataRowNjesiAdministrative["PERSHKRIMI"].ToString();
                    adresa = dbDataRowNjesiAdministrative["ADRESA"].ToString();
                    bool.TryParse(dbDataRowNjesiAdministrative["AKTIV"].ToString(), out aktiv);
                    int.TryParse(dbDataRowNjesiAdministrative["IDNDERMARJE"].ToString(), out idNdermarrje);
                    int.TryParse(dbDataRowNjesiAdministrative["IDPERDORUESI"].ToString(), out idPerdorues);
                    DateTime.TryParse(dbDataRowNjesiAdministrative["DATEREGJISTRIMI"].ToString(), out dateRegjistrimi);
                    int.TryParse(dbDataRowNjesiAdministrative["IDKONFIG"].ToString(), out idKonfig);
                    int.TryParse(dbDataRowNjesiAdministrative["IDSTATUSDOK"].ToString(), out idStatusDok);
                    DateTime.TryParse(dbDataRowNjesiAdministrative["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRowNjesiAdministrative["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    int.TryParse(dbDataRowNjesiAdministrative["QENDRAKOSTOS"].ToString(), out qenderKosto);
                    int.TryParse(dbDataRowNjesiAdministrative["IDSKEMAQENDRAKOSTO"].ToString(), out idSkemaQendraKosto);
                    qendra = dbDataRowNjesiAdministrative["QENDRA"].ToString();
                    int.TryParse(dbDataRowNjesiAdministrative["LLOJQENDRE"].ToString(), out llojQendre);
                    if(clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizim())
                        kodNjesieBiznesi = dbDataRowNjesiAdministrative["KODNJESIEBIZNES"].ToString();

                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se dege administrative nga db-ja");
                }
            }
            else
                return false;
        }
        internal bool mbushDegeAdministrativePerKonfigurim(DataRow dbDataRowNjesiAdministrative)
        {
            if (dbDataRowNjesiAdministrative != null)
            {
                try
                {
                    int.TryParse(dbDataRowNjesiAdministrative["IDDEGEADMINISTRATIVE"].ToString(), out idDegeAdministrative);
                    kodi = dbDataRowNjesiAdministrative["KODI"].ToString() + " (" + dbDataRowNjesiAdministrative["PERSHKRIMI"].ToString() + ")";
                    pershkrimi = dbDataRowNjesiAdministrative["PERSHKRIMI"].ToString();
                    adresa = dbDataRowNjesiAdministrative["ADRESA"].ToString();
                    bool.TryParse(dbDataRowNjesiAdministrative["AKTIV"].ToString(), out aktiv);
                    int.TryParse(dbDataRowNjesiAdministrative["IDNDERMARJE"].ToString(), out idNdermarrje);
                    int.TryParse(dbDataRowNjesiAdministrative["IDPERDORUESI"].ToString(), out idPerdorues);
                    DateTime.TryParse(dbDataRowNjesiAdministrative["DATEREGJISTRIMI"].ToString(), out dateRegjistrimi);
                    int.TryParse(dbDataRowNjesiAdministrative["IDKONFIG"].ToString(), out idKonfig);
                    int.TryParse(dbDataRowNjesiAdministrative["IDSTATUSDOK"].ToString(), out idStatusDok);
                    DateTime.TryParse(dbDataRowNjesiAdministrative["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRowNjesiAdministrative["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    int.TryParse(dbDataRowNjesiAdministrative["QENDRAKOSTOS"].ToString(), out qenderKosto);
                    int.TryParse(dbDataRowNjesiAdministrative["IDSKEMAQENDRAKOSTO"].ToString(), out idSkemaQendraKosto);
                    qendra = dbDataRowNjesiAdministrative["QENDRA"].ToString();
                    int.TryParse(dbDataRowNjesiAdministrative["LLOJQENDRE"].ToString(), out llojQendre);
                    if(clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizim())
                        kodNjesieBiznesi = dbDataRowNjesiAdministrative["KODNJESIEBIZNES"].ToString();

                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se dege administrative nga db-ja");
                }
            }
            else
                return false;
        }
        internal void mbushDegeAdministrative(clsDegeAdministrative dega)
        {
            ImbLogger.LogTraceShitje("Filloi metoda mbush dege administrative.");
            this.IdDegeAdministrative = dega.IdDegeAdministrative;
            this.kodi = dega.kodi;
            this.pershkrimi = dega.pershkrimi;
            this.adresa = dega.adresa;
            this.aktiv = dega.aktiv;
            this.IdNdermarje = dega.IdNdermarje;
            this.IdPerdorues = dega.IdPerdorues;
            this.dateRegjistrimi = dega.dateRegjistrimi;
            this.IdKonfig = dega.IdKonfig;
            this.IdStatusDok = dega.IdStatusDok;
            this.dtKrijimi = dega.dtKrijimi;
            this.dtModifikimi = dega.dtModifikimi;
            this.qenderKosto = dega.qenderKosto;
            this.idSkemaQendraKosto = dega.idSkemaQendraKosto;
            this.qendra = dega.qendra;
            this.llojQendre = dega.llojQendre;
            ImbLogger.LogTraceShitje("Mbaroi metoda mbush dege administrative.");


        }

        #endregion

    }
}
