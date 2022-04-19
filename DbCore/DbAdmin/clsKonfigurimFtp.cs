using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Logging;
using System;
using System.Data;
using AlphaWeb.Core.Interfaces.Data;

namespace DbCore.DbAdmin
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne konfigurimin e ftp. Konfigurimi eshte ne nivel ndermarrje.
    ///  (Te dhenat  merren nga tabela : T_KONFIGURIMEFTP)
    /// </summary>
    public class clsKonfigurimFtp:IDataBase

    {
        #region Atributet
        private static string celesi = "[ImbAlphaWeb876*]";
        private int idKonfigurimFtp;
        private string kodi;
        private string hostName;
        private string username;
        private string password;
        private int port; //defaulti 25
        private int idNdermarrje;
        private int idPerdoruesi;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        bool enableSSL;
        private int idStatusDok;
        private int metoda; // 0 tregon metode transferimi manual dhe 1 tregon metode transferimi automatik
        private IDataRecord record;
        private bool eshteSFTP;
        private string folderPath;
        #endregion

        #region Properties

        public int IdKonfigurimFtp {
            get
            {
                return idKonfigurimFtp;
            }
            set
            {
                idKonfigurimFtp = value;
            }

        }
        public string Kodi {
            get
            {
                return kodi;
            }
            set
            {
                kodi = value;
            }
        }
        public string HostName {
            get
            {
                return hostName;
            }
            set
            {
                hostName = value;
            }
        }
        public string Username {
            get
            {
                return username;
            }
            set
            {
                username = value;
            }
        }        
        public string Password {
            get
            {
                return StringCipher.Decrypt(password, celesi);
            }
            set
            {
                password = StringCipher.Encrypt(value, celesi);
            }
        }
        /// <summary>
        /// Kthen/Vendos porten smtp
        /// </summary>
        public int Port {
            get
            {
                return port;
            }
            set
            {
                port = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos id e perdoruesit
        /// </summary>
        public int IdPerdoruesi {
            get
            {
                return idPerdoruesi;
            }
            set
            {
                idPerdoruesi = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos id e ndermarrjes
        /// </summary>
        public int IdNdermarje {
            get
            {
                return idNdermarrje;
            }
            set
            {
                idNdermarrje = value;
            }
        }
        /// <summary>
        /// Kthen daten e krijimit
        /// </summary>
        public DateTime DtKrijimi {
            get
            {
                return dtKrijimi;
            }

        }
        /// <summary>
        /// Kthen daten e modifikimit te fundit
        /// </summary>
        public DateTime DtModifikimi {
            get
            {
                return dtModifikimi;
            }
        }
        public bool EnableSSL {
            get
            {
                return enableSSL;
            }
            set
            {
                enableSSL = value;
            }
        }
        public bool EshteSFTP { get { return eshteSFTP; } set { eshteSFTP = value; } }
        public string FolderPath
        {
            get
            {
                return folderPath;
            }
            set
            {
                folderPath = value;
            }
        }
        public int IdStatusDok {
            get
            {
                return idStatusDok;
            }
            set
            {
                idStatusDok = value;
            }
        }
        public int Metoda {
            get
            {
                return metoda;
            }
            set
            {
                metoda = value;
            }
        }

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Kontruktori pa parametra i klases
        /// </summary>
        public clsKonfigurimFtp()
        {
        }
        public clsKonfigurimFtp(IDataRecord record)
        {
            Mbush(record);
        }
        /// <summary>
        /// mbush konfigurimin e ftp-se sipas ndermarrjes.
        /// </summary>
        /// <param name="idNdermarrje"></param>
        public clsKonfigurimFtp(int idNdermarrje)
        {
            using (clsDatabaseAdmin dbAdm = new clsDatabaseAdmin())
                dbAdm.mbushKonfigurimFtpSipasIdNdermarrje(idNdermarrje, this);
        }

        public clsKonfigurimFtp(int idKonfigurim, int idNdermarrje)
        {
            using (clsDatabaseAdmin dbAdm = new clsDatabaseAdmin())
            {
                dbAdm.merrKonfigurimFtpSipasId(idKonfigurim, idNdermarrje, this);
            }
        }

        /// <summary>
        /// Kontruktori me parametra i klases
        /// </summary>
        public clsKonfigurimFtp(int idKonfigurimFtp, string kodi, string hostName, string username, string passwordi, int port, int idNdermarrje, int idPerdoruesi, bool enableSSL, int idStatusDok, int metoda, bool eshteSFTP, string folderPath)
        {
            this.idKonfigurimFtp = idKonfigurimFtp;
            this.kodi = kodi;
            this.hostName = hostName;
            this.username = username;
            this.Password = passwordi;
            this.idNdermarrje = idNdermarrje;
            this.idPerdoruesi = idPerdoruesi;
            this.port = port;
            this.enableSSL = enableSSL;
            this.idStatusDok = idStatusDok;
            this.metoda = metoda;
            this.eshteSFTP = eshteSFTP;
            this.folderPath = folderPath;
        }

        //public clsKonfigurimFtp(IDataRecord record)
        //{
        //    this.record = record;
        //}

        #endregion

        #region Metoda Publike

        public clsMesazh Ruaj()
        {
            clsMesazh mesazh = new clsMesazh(false);
            using (clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin())
            {
                mesazh = ruajKonfigurimTeRi(dbAdmin);
            }
            return mesazh;
        }

        public clsMesazh Modifiko()
        {
            using (clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin())
                return modifiko(dbAdmin);
            
        }
        /// <summary>
        /// Ruan objektin e konfigurimit te ftp ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbAdmin.clsDatabaseAdmin.ruajKonfigurimFtp"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>
        public clsMesazh ruajKonfigurimTeRi(clsDatabaseAdmin dbAdmin)
        {
            int id;
            clsMesazh u_ruajt = dbAdmin.ruajKonfigurimFtp(out id, kodi, hostName, username, password, port, idNdermarrje, idPerdoruesi, enableSSL, metoda, eshteSFTP, folderPath);
            this.idKonfigurimFtp = id;
            return u_ruajt;
        }

        /// <summary>
        /// Modifikon objektin e konfigurimit te ftp ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbAdmin.clsDatabaseAdmin.modifikoKonfigurimFtp"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>
        public clsMesazh modifiko(clsDatabaseAdmin dbAdmin)
        {
            return dbAdmin.modifikoKonfigurimFtp(idKonfigurimFtp, kodi, hostName, username, password, port, idNdermarrje, idPerdoruesi, enableSSL, metoda, eshteSFTP, folderPath);
        }

        /// <summary>
        /// mbush konfigurimin e emailit sipas ndermarrjes
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <returns></returns>
        public void mbushKonfigurimSipasIdNdermarrje(int idNdermarrje)
        {
            using (clsDatabaseAdmin dbAdm = new clsDatabaseAdmin())
                dbAdm.mbushKonfigurimFtpSipasIdNdermarrje(idNdermarrje, this);
        }

        public clsMesazh Fshi()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            clsMesazh u_fshi = data.fshiKonfigurimFtp(this.IdKonfigurimFtp);
            data.Dispose();
            return u_fshi;
        }

        public static DataTable merrKonfigurimeFtpSipasNdermDhePerdorues(int idNdermarrje)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            DataTable tabela = data.ktheGjitheKonfigurimetFtpSipasNdermarrje(idNdermarrje);
            data.Dispose();
            return tabela;
        }

        public static DataRow merrKonfigurimFtpSipasIdDR(int idKonfigurimFtp)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            DataRow dr = data.ktheKonfigurimFtpSipasIdDR(idKonfigurimFtp);
            data.Dispose();
            return dr;
        }

        public static bool ekzistonKonfigurimiFtp(string kodi, int idNdermarrje)
        {
            using (var data = new clsDatabaseAdmin())
                return data.ekzistonKonfigurimFtpMeKeteKodPerKeteNdermarje(kodi, idNdermarrje);
        }

        public static int ktheIDKonfigurimFtp(string kod, int idNdermarrje)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
                return data.merrIDKonfigurmiFtp(kod, idNdermarrje);
        }
        public void mbushKonfigurimFtpManualPerNdermarrjen(int idNdermarrje)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
                data.merrKonfiguriminEPareManualFtp(idNdermarrje, this);
        }

        public static bool eshteKonfigurimiFtpILidhur(string IdKonfigurimFtp, string IdNivel)
        {
            using (DbCore.DbRegjistrim.clsDatabaseRegjistrim dbRegjistrim = new DbCore.DbRegjistrim.clsDatabaseRegjistrim())
                return dbRegjistrim.eshteDokumentiILidhurCelje(IdKonfigurimFtp, IdNivel);
        }

        public void Mbush(IDataRecord record)
        {
            try
            {
                int.TryParse(record["IDKONFIGURIMEFTP"].ToString(), out idKonfigurimFtp);
                kodi = record["KODI"].ToString();
                username = record["USERNAME"].ToString();
                hostName = record["HOSTNAME"].ToString();
                password = record["PASSWORD"].ToString();
                int.TryParse(record["PORT"].ToString(), out port);
                bool.TryParse(record["ENABLESSL"].ToString(), out enableSSL);
                int.TryParse(record["IDNDERMARRJE"].ToString(), out idNdermarrje);
                int.TryParse(record["IDPERDORUESI"].ToString(), out idPerdoruesi);
                DateTime.TryParse(record["DTKRIJIMI"].ToString(), out dtKrijimi);
                DateTime.TryParse(record["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                int.TryParse(record["IDSTATUSDOK"].ToString(), out idStatusDok);
                int.TryParse(record["METODA"].ToString(), out metoda);
                bool.TryParse(record["ESHTE_SFTP"].ToString(), out eshteSFTP);
                folderPath = record["FOLDER_PATH"].ToString();
            } catch (InvalidCastException ex)
            {
                ImbLogger.Error(ex);
                throw new MyException("ERROR: Gabim casti gjate marrjes se konfigurimit nga databaza!");
            } catch (Exception ex)
            {
                ImbLogger.Error(ex);
                throw new MyException("ERROR: Gabim gjate marrjes se konfigurimit nga databaza!");
            }
        }

        #endregion

    }
}