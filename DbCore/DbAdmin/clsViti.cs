using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DbCore.IMBUtils.Logging;
using DbCore.IMBUtils.Messages;

namespace DbCore.DbAdmin
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne vitet
    ///  (Te dhenat  merren nga tabela : T_VITET)
    /// </summary>
    public class clsViti
    {
        #region Atribute

        private int idViti;
        private String kodiViti;
        private DateTime fillimiViti;
        private DateTime mbarimiViti;
        private String periudhaLloji;
        private Boolean  periudhaHapjes;
        private Boolean   periudhaMbylljes;
        private int idPerdoruesi;
        private int idKonfig;
        private int idNdermarje;
        private int idStatusDok;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private DateTime mbyllurMe;
        private int idLlogMbylljeViti;
        private DataRow rreshti;
        #endregion

        #region Konstuktoret
        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsViti(int idviti, String kodiviti, DateTime fillimiviti, DateTime mbarimiviti, String periudhalloji, bool periudhahapjes, bool periudhambylljes , int idperdoruesi, int idKonfig,int idNdermarje, int idstatusdok, DateTime mbyllurme, int idllogmbylljeviti)
        {
            idViti = idviti;
            kodiViti = kodiviti;
            fillimiViti = fillimiviti;
            mbarimiViti = mbarimiviti;
            periudhaLloji = periudhalloji;
            periudhaHapjes = periudhahapjes;
            periudhaMbylljes = periudhambylljes;
            idPerdoruesi = idperdoruesi;
            this.idKonfig = idKonfig;
            this.idNdermarje = idNdermarje;
            this.idStatusDok = idstatusdok;
            mbyllurMe = mbyllurme;
            idLlogMbylljeViti = idllogmbylljeviti;
        }

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsViti(String kodiviti, DateTime fillimiviti, DateTime mbarimiviti, String periudhalloji, bool periudhahapjes, bool periudhambylljes, int idperdoruesi, int idKonfig, int idNdermarje, int idstatusdok, DateTime mbyllurme, int idllogmbylljeviti)
        {
            kodiViti = kodiviti;
            fillimiViti = fillimiviti;
            mbarimiViti = mbarimiviti;
            periudhaLloji = periudhalloji;
            periudhaHapjes = periudhahapjes;
            periudhaMbylljes = periudhambylljes;
            idPerdoruesi = idperdoruesi; 
            this.idKonfig = idKonfig;
            this.idNdermarje = idNdermarje;      
            this.idStatusDok = idstatusdok;
            mbyllurMe = mbyllurme;
            idLlogMbylljeViti = idllogmbylljeviti;
        }
        
        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsViti()
        {
            ImbLogger.LogTraceShitje("U krijua nje objekt clsViti!");
        }
        public clsViti(int idViti)
        {
            clsDatabaseAdmin dbadmin = new clsDatabaseAdmin();
            mbushViti(dbadmin.merrVit(idViti));
            dbadmin.Dispose();
        }
        public clsViti(int idViti, clsDatabaseAdmin dbadmin)
        {
            mbushViti(dbadmin.TransCache.getViti(idViti, dbadmin));

        }
        public clsViti(int idndermarje, string kodi, clsDatabaseAdmin dbadmin)
        {
            mbushViti(dbadmin.TransCache.getViti(kodi, idndermarje, dbadmin));
        }

        public clsViti(int idndermarje,string kodi )
        {using (clsDatabaseAdmin dbadmin = new DbAdmin.clsDatabaseAdmin())
                mbushViti(dbadmin.merrVit(kodi, idndermarje));
        }
        
        public clsViti(DataRow rreshti)
        {
            
            mbushViti(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdViti
        {
            get { return idViti; }
            set { idViti = value; }
        }

        /// <summary>
        /// Kthen/Vendos kodin e vitit.
        /// </summary>
        public String KodiViti
        {
            get { return kodiViti; }
            set { kodiViti = value; }
        }

        /// <summary>
        /// Kthen/Vendos daten kur fillon viti.
        /// </summary>
        public DateTime FillimiViti
        {
            get { return fillimiViti; }
            set { fillimiViti = value; }
        }

        /// <summary>
        /// Kthen/Vendos daten kur mbaron viti.
        /// </summary>
        public DateTime MbarimiViti
        {
            get { return mbarimiViti; }
            set { mbarimiViti = value; }
        }

        /// <summary>
        /// Kthen/Vendos llojin e periudhes (1 mujore, 2 mujore etj).
        /// </summary>
        public String PeriudhaLloji
        {
            get { return periudhaLloji; }
            set { periudhaLloji = value; }
        }

        /// <summary>
        /// Kthen/Vendos ????
        /// </summary>
        public Boolean PeriudhaHapjes
        {
            get { return periudhaHapjes; }
            set { periudhaHapjes = value; }
        }

        /// <summary>
        /// Kthen/Vendos ????
        /// </summary>
        public Boolean  PeriudhaMbylljes
        {
            get { return periudhaMbylljes; }
            set { periudhaMbylljes = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e perdoruesit qe po cel vitin.
        /// </summary>
        public int IdPerdoruesi
        {
            get { return idPerdoruesi ; }
            set { idPerdoruesi  = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne konfigurimit.
        /// </summary>
        public int IdKonfig
            {
            get { return idKonfig; }
            set { idKonfig = value; }
            }
        /// <summary>
        /// Kthen/Vendos ID-ne e ndermarjes.
        /// </summary>
        public int IdNdermarje
            {
            get { return idNdermarje; }
            set { idNdermarje = value; }
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
        public DateTime MbyllurMe
        {
            get { return mbyllurMe; }
            set { mbyllurMe = value; }

        }
        public int IdLlogMbylljeViti
        {
            get { return idLlogMbylljeViti; }
            set { idLlogMbylljeViti = value; }
        }
        #endregion

        #region Metoda Publike

        /// <summary>
        /// Ruan vitin dhe periudhat ne tabelat perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbAdmin.clsViti.ruajVitPeriudhe"/> 
        /// </summary>
        public clsMesazh ruaj(colPeriudhaKontabel periudhat, int idKonfigurimiNdermarjeDefault, int idkonfigNderKos, clsDatabaseAdmin db)
        {
            return ruajVitPeriudhe(this, periudhat, idKonfigurimiNdermarjeDefault, idkonfigNderKos, db);
        }
        /// <summary>
        /// Ruan vitin dhe periudhat ne tabelat perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbAdmin.clsViti.ruajVitPeriudhe"/> 
        /// </summary>
        public clsMesazh ruaj(colPeriudhaKontabel periudhat, int idKonfigurimiNdermarjeDefault, int idkonfigNderKos)
        {
            return ruajVitPeriudhe(this, periudhat, idKonfigurimiNdermarjeDefault, idkonfigNderKos);
        }
        /// <summary>
        /// Modifikon vitin dhe periudhat e ketij viti ne tabelat perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbAdmin.clsViti.modifikoVitPeriudhe"/> 
        /// </summary>
        public clsMesazh modifiko(colPeriudhaKontabel periudhat, System.Web.SessionState.HttpSessionState Session, int idgjuha)
        {
            return modifikoVitPeriudhe(this, periudhat, Session, idgjuha);
        }

        /// <summary>
        /// Fshin vitin dhe periudhat ne tabelat perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbAdmin.clsViti.fshiVitinDhePeriudhat"/> 
        /// </summary>
        public clsMesazh fshi()
        {
            using (clsDatabaseAdmin dbadmin = new clsDatabaseAdmin())
            {
                return dbadmin.fshiVitStatus(this.idViti, this.idPerdoruesi);
            }
        }

        /// <summary>
        /// mbush nje klase me vite sipas id se vitit
        /// </summary>
        /// <param name="id">merr id e vitit</param>
        /// <returns>kthen true nese kryhet mbushja me sukses, ne te kundert false</returns>
        public bool mbushVitetMet(int id)
        {
            using (clsDatabaseAdmin dbadmin = new clsDatabaseAdmin())
            {
                return mbushViti(dbadmin.merrVit(id));
            }
        }

        /// <summary>
        /// mbush nje klase me vite sipas id se vitit
        /// </summary>
        /// <param name="id">merr id e vitit</param>
        /// <returns>kthen true nese kryhet mbushja me sukses, ne te kundert false</returns>
        public bool mbushVitetMet(int id, clsDatabaseAdmin dbadmin)
        {
            return mbushViti(dbadmin.merrVit(id));
        }

        /// <summary>
        /// mbush nje klase me vite sipas kodit te vitit dhe id se ndermarrjes
        /// </summary>
        /// <param name="kodi">merr si string kodin e vitit</param>
        /// <returns>kthen true nese eshte e vertete, ne te kundert kthen false</returns>
        public bool mbushVitetMet(string kodi, int idndermarje)
        {
            using (clsDatabaseAdmin dbadmin = new clsDatabaseAdmin())
            {
                return mbushViti(dbadmin.merrVit(kodi, idndermarje));
            }
        }
        /// <summary>
        /// mbush nje klase me vite sipas kodit te vitit dhe id se ndermarrjes
        /// </summary>
        /// <param name="kodi">merr si string kodin e vitit</param>
        /// <returns>kthen true nese eshte e vertete, ne te kundert kthen false</returns>
        public bool mbushVitetMet(string kodi, int idndermarje, clsDatabaseAdmin dbadmin)
        {
            if (dbadmin == null)
                dbadmin = new clsDatabaseAdmin();
            return mbushViti(dbadmin.merrVit(kodi, idndermarje));
        }
        public clsMesazh shtoPeriudhString(int idviti, colPeriudhaKontabel periudhat, clsDatabaseAdmin dbAdmin)
        {
            clsMesazh mesazh = new clsMesazh();       
            string sqlstr = "";
            //bool postoE,postoM,postoA = false;

            //if (periudhat[i].PostoEpayslip == null) postoE = false;

            sqlstr += "insert into T_PERIUDHAT (IDVITI,NUMRIPERIUDHA,DATAFILLIMIT, DATAMBARIMIT,EKYCUR, EMERPERIUDHA,POSTOEPAYSLIP,POSTOMEMOBONUS,POSTOANNUALDECLARATION) values";
            for (int i = 0; i < periudhat.Count; i++)
            {
                sqlstr += "(" + idviti + "," + periudhat[i].NrPeriudha + ",convert(datetime,'" + periudhat[i].FillimiPeriudha + "', 103),convert(datetime,'" + periudhat[i].MbarimiPeriudha + "', 103),'" + periudhat[i].Ekycur + "','" + periudhat[i].EmerPeriudha + "','" + periudhat[i].PostoEpayslip + "','" + periudhat[i].PostoMemoBonus + "','" + periudhat[i].PostoAnnualDeclaration + "'),";

            }
            sqlstr = sqlstr.Remove(sqlstr.Length - 1);
            mesazh = dbAdmin.shtoPeriudhat(sqlstr);
            return mesazh;
        }

        public clsMesazh fshiVitinDhePeriudhat(clsViti viti)
        {
            clsMesazh mesazh;
            using (clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin())
            {
                dbAdmin.beginTransaksion();
                try
                {
                    //fshihen periudhat qe i perkasin ketij
                    mesazh = dbAdmin.fshiPeriudhatVitit(viti.IdViti);
                    if (!mesazh.Status)
                    {
                        dbAdmin.rollbackTransaksion();
                        return mesazh;
                    }
                    //fshihet viti pasi jane fshire te gjitha periudhat qe i perkisnin atij
                    mesazh = dbAdmin.fshiVit(viti.IdViti);
                    if (!mesazh.Status)
                    {
                        dbAdmin.rollbackTransaksion();
                        return mesazh;
                    }
                    dbAdmin.commitTransaksion();
                    mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
                    return mesazh;
                }
                catch (Exception ce)
                {
                    dbAdmin.rollbackTransaksion();
                    return new clsMesazh(false, ce.Message);
                }
            }
        }
        public clsMesazh ruajVitPeriudhe(clsViti viti, colPeriudhaKontabel periudhat, int idkonfigurimiNdermarjeDefault, int idkonfigNdermarjeKos)
        {
            using (clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin())
            {
                dbAdmin.beginTransaksion();
                clsMesazh uRuajt = ruajVitPeriudhe(viti, periudhat, idkonfigurimiNdermarjeDefault, idkonfigNdermarjeKos, dbAdmin);
                if (!uRuajt.Status)
                {
                    dbAdmin.rollbackTransaksion();
                    return uRuajt;
                }
                dbAdmin.commitTransaksion();
                return uRuajt;
            }
        }
        public clsMesazh ruajVitPeriudhe(clsViti viti, colPeriudhaKontabel periudhat, int idkonfigurimiNdermarjeDefault, int idkonfigNdermarjeKos, clsDatabaseAdmin dbAdmin)
        {
            colVitet vitetdefault = new colVitet();
            vitetdefault.merrGjitheVitetENdermarjes(-1, dbAdmin);
            colVitet vitetkos = new colVitet();
            vitetkos.merrGjitheVitetENdermarjes(-3, dbAdmin);
            clsMesazh mesazh;
            try
            {
                int idV;
                mesazh = dbAdmin.ruajVit(out idV, viti.KodiViti, viti.FillimiViti, viti.MbarimiViti, viti.PeriudhaLloji, viti.PeriudhaHapjes, viti.PeriudhaMbylljes, viti.IdPerdoruesi, viti.IdKonfig, viti.IdNdermarje, viti.idStatusDok, viti.mbyllurMe, viti.idLlogMbylljeViti);
                if (!mesazh.Status)
                    return mesazh;

                //per vitin qe sapo u ruajt shtohen periudhat ushtrimore perkatese
                viti.IdViti = idV;
                mesazh = viti.shtoPeriudhString(viti.IdViti, periudhat, dbAdmin);
                if (!mesazh.Status)
                    return mesazh;
                bool ekziston = false;
                foreach (clsViti v in vitetdefault)
                    if (v.KodiViti == viti.KodiViti)
                        ekziston = true;
                if (!ekziston)
                {
                    viti.IdKonfig = idkonfigurimiNdermarjeDefault;
                    viti.IdNdermarje = -1;
                    int idVit;
                    mesazh = dbAdmin.ruajVit(out idVit, viti.KodiViti, viti.FillimiViti, viti.MbarimiViti, viti.PeriudhaLloji, viti.PeriudhaHapjes, viti.PeriudhaMbylljes, viti.IdPerdoruesi, viti.IdKonfig, viti.IdNdermarje, viti.idStatusDok, viti.mbyllurMe, viti.idLlogMbylljeViti);
                    if (!mesazh.Status)
                        return mesazh;
                    //per vitin qe sapo u ruajt shtohen periudhat ushtrimore perkatese

                    mesazh = viti.shtoPeriudhString(idVit, periudhat, dbAdmin);
                    if (!mesazh.Status)
                        return mesazh;
                }
                colVitet vitetBuxhetor = new colVitet();
                vitetBuxhetor.merrGjitheVitetENdermarjes(-2, dbAdmin);
                if (vitetBuxhetor.Count > 0)
                {
                    if (!vitetBuxhetor.Contains(vitetBuxhetor.Find(x => x.KodiViti == viti.KodiViti)))
                    {
                        viti.IdKonfig = DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdSipasKategoriDheNderm(22, -2, new DbCore.DbShare.clsDatabaseShare(dbAdmin ));
                        viti.IdNdermarje = -2;
                        int idVit;
                        mesazh = dbAdmin.ruajVit(out idVit, viti.KodiViti, viti.FillimiViti, viti.MbarimiViti, viti.PeriudhaLloji, viti.PeriudhaHapjes, viti.PeriudhaMbylljes, viti.IdPerdoruesi, viti.IdKonfig, viti.IdNdermarje, viti.idStatusDok, viti.mbyllurMe, vitetBuxhetor[0].idLlogMbylljeViti);
                        if (!mesazh.Status)
                            return mesazh;
                        //per vitin qe sapo u ruajt shtohen periudhat ushtrimore perkatese
                        mesazh = viti.shtoPeriudhString(idVit, periudhat, dbAdmin);
                        if (!mesazh.Status)
                            return mesazh;
                    }
                }
                if (vitetkos.Count > 0)
                {
                    ekziston = false;
                    foreach (clsViti v in vitetkos)
                        if (v.KodiViti == viti.KodiViti)
                            ekziston = true;
                    if (!ekziston)
                    {
                        viti.IdKonfig = idkonfigNdermarjeKos;
                        viti.IdNdermarje = -3;
                        int idVit;
                        mesazh = dbAdmin.ruajVit(out idVit, viti.KodiViti, viti.FillimiViti, viti.MbarimiViti, viti.PeriudhaLloji, viti.PeriudhaHapjes, viti.PeriudhaMbylljes, viti.IdPerdoruesi, viti.IdKonfig, viti.IdNdermarje, viti.idStatusDok, viti.mbyllurMe, vitetkos[0].idLlogMbylljeViti);
                        if (!mesazh.Status)
                            return mesazh;
                        //per vitin qe sapo u ruajt shtohen periudhat ushtrimore perkatese
                        mesazh = viti.shtoPeriudhString(idVit, periudhat, dbAdmin);
                        if (!mesazh.Status)
                            return mesazh;
                    }
                }
                return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            }
            catch (Exception ce)
            {
                return new clsMesazh(false, ce.Message);
            }
        }

        public clsMesazh modifikoVitPeriudhe(clsViti viti, colPeriudhaKontabel periudhat, System.Web.SessionState.HttpSessionState Session, int idgjuha)
        {
            clsMesazh mesazh;
            using (clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin())
            {
                dbAdmin.beginTransaksion();
                try
                {
                    //azhornohet viti me te dheat e reja
                    mesazh = dbAdmin.modifikoVit(viti.IdViti, viti.KodiViti, viti.FillimiViti, viti.MbarimiViti, viti.PeriudhaLloji, viti.PeriudhaHapjes, viti.PeriudhaMbylljes, viti.IdPerdoruesi, viti.IdKonfig, viti.IdNdermarje, viti.idStatusDok, viti.mbyllurMe, viti.idLlogMbylljeViti);
                    if (!mesazh.Status)
                    {
                        dbAdmin.rollbackTransaksion();
                        return mesazh;
                    }
                    //mesazh = dbAdmin.fshiPeriudhatVitit(viti.IdViti);//fshirja na prish pune tek idperiudha qe ruhet tek kokafletes kontabel
                    //if (!mesazh.Status)
                    //{
                    //    dbAdmin.rollbackTransaksion();
                    //    return mesazh;
                    //}
                    mesazh = modifikoPeriudhat(periudhat, dbAdmin);
                   
                        if (!mesazh.Status)
                        {
                            dbAdmin.rollbackTransaksion();
                            return mesazh;
                        }
                  
                    ////shtohen periudhat e percaktuara mbi te dhenat e reja te vitit
                    //mesazh = viti.shtoPeriudhString(viti.IdViti, periudhat, dbAdmin);
                    //if (!mesazh.Status)
                    //{
                    //    dbAdmin.rollbackTransaksion();
                    //    return mesazh;
                    //}
                    dbAdmin.commitTransaksion();
                     clsPeriudhaKontabel periudhaakt=     mySessionObjects.merrPeriudheKontabel(Session);
                    if(periudhaakt.IdViti==viti.IdViti)
                    mySessionObjects.ruajPeriudheKontabelNeSesion(new clsPeriudhaKontabel(viti.IdViti, mySessionObjects.merrPeriudheKontabel(Session).NrPeriudha, idgjuha), Session);
                    mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
                    return mesazh;
                }
                catch (Exception ce)
                {
                    dbAdmin.rollbackTransaksion();
                    return new clsMesazh(false, ce.Message);
                }
            }
        }
        private static clsMesazh modifikoPeriudhat(colPeriudhaKontabel periudhat, clsDatabaseAdmin dbAdmin)
        {
            clsMesazh mesazh = new clsMesazh(true);
            foreach (clsPeriudhaKontabel p in periudhat)
            {
                mesazh = p.modifikoPeriudha(dbAdmin);
                if (!mesazh.Status)
                {
                    return mesazh;
                }
            }
            return mesazh;
        }
        public static clsMesazh modifikoVitPeriudheEPaySlip( colPeriudhaKontabel periudhat)
        {
            clsMesazh mesazh;
            using (clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin())
            {
                dbAdmin.beginTransaksion();
                try
                {
                    mesazh = modifikoPeriudhat(periudhat, dbAdmin);
                    if (!mesazh.Status)
                    {
                        dbAdmin.rollbackTransaksion();
                        return mesazh;
                    }
                    dbAdmin.commitTransaksion();
                    mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
                    return mesazh;
                }
                catch (Exception ce)
                {
                    dbAdmin.rollbackTransaksion();
                    return new clsMesazh(false, ce.Message);
                }
            }
        }

        public static bool ekzistonVitPerNdermarrjen(string viti, int idNdermarrje)
        {
            using (clsDatabaseAdmin dbadmin = new clsDatabaseAdmin())
            {
                return !dbadmin.ekzistonVit(viti, idNdermarrje).Status;
            }
        }
        public static int ktheIdVitPerNdermarrjenSipasKodit(int idNdermarrje, string kodViti)
        {
            using (clsDatabaseAdmin dbadmin = new clsDatabaseAdmin())
            {
                return dbadmin.ktheIdVitPerNdermarrjenSipasKodit(idNdermarrje, kodViti);
            }
        }
        public static string ktheKodVitSipasID(int idVit)
        {
            using (clsDatabaseAdmin dbadmin = new clsDatabaseAdmin())
            {
                return dbadmin.ktheKodVitSipasID(idVit);
            }
        }

        public static bool ekzistonVitPerNdermarrjen(string viti, int idNdermarrje, clsDatabaseAdmin dbadmin)
        {
            return !dbadmin.ekzistonVit(viti, idNdermarrje).Status;
        }

        #endregion

        #region Metoda Internal

        internal bool mbushViti(DataRow dbDataRowViti)
        {
            if (dbDataRowViti != null)
            {
                try
                {
                    int.TryParse(dbDataRowViti["IDVITI"].ToString(), out idViti);
                    kodiViti = dbDataRowViti["KODIVITI"].ToString();
                    DateTime.TryParse(dbDataRowViti["FILLIMIVITI"].ToString(), out fillimiViti);
                    DateTime.TryParse(dbDataRowViti["MBARIMIVITI"].ToString(), out mbarimiViti);
                    periudhaLloji = dbDataRowViti["PERIUDHALLOJI"].ToString();
                    bool.TryParse(dbDataRowViti["PERIUDHAHAPJES"].ToString(), out periudhaHapjes);
                    bool.TryParse(dbDataRowViti["PERIUDHAMBYLLJES"].ToString(), out periudhaMbylljes);
                    int.TryParse(dbDataRowViti["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    int.TryParse(dbDataRowViti["IDKONFIG"].ToString(), out idKonfig);
                    int.TryParse(dbDataRowViti["IDNDERMARJE"].ToString(), out idNdermarje);
                    int.TryParse(dbDataRowViti["IDSTATUSDOK"].ToString(), out idStatusDok);
                    DateTime.TryParse(dbDataRowViti["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRowViti["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    DateTime.TryParse(dbDataRowViti["MBYLLURME"].ToString(), out mbyllurMe);
                    int.TryParse(dbDataRowViti["IDLLOGMBYLLJEVITI"].ToString(), out idLlogMbylljeViti);
                   return true;
                }
                catch (InvalidCastException)
                {
                    ImbLogger.LogErrorShitje("ERROR: Gabim gjate marrjes se vitit nga db - ja");
                    throw new Exception("ERROR: Gabim gjate marrjes se vitit nga db-ja");
                }
            }
            else
                return false;
        }

        public clsMesazh mbushViti(clsViti viti)
        {
            ImbLogger.LogTraceShitje("Filloi metoda mbushViti");
            idViti = viti.IdViti;
            kodiViti = viti.KodiViti;
            fillimiViti = viti.FillimiViti;
            mbarimiViti = viti.MbarimiViti;
            periudhaLloji = viti.PeriudhaLloji;
            periudhaHapjes = viti.PeriudhaHapjes;
            periudhaMbylljes = viti.PeriudhaMbylljes;
            idPerdoruesi = viti.idPerdoruesi;
            idKonfig = viti.IdKonfig;
            idNdermarje = viti.IdNdermarje;
            idStatusDok = viti.IdStatusDok;
            dtKrijimi = viti.DtKrijimi;
            dtModifikimi = viti.DtModifikimi;
            mbyllurMe = viti.MbyllurMe;
            idLlogMbylljeViti = viti.idLlogMbylljeViti;
            ImbLogger.LogTraceShitje($"Mbushja e vitit me kod {viti.KodiViti} u be me sukses!");
            return new clsMesazh(true, $"Mbushja e vitit me kod {viti.KodiViti} u be me sukses!");
        }

        #endregion
    }
}
