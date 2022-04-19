using System;
using System.Data;
using DbCore.IMBUtils.Messages;
using AlphaWebCore;
namespace DbCore.DbAdmin
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne konfigurimin e emailit. Konfigurimi eshte ne nivel ndermarrje.
    ///  (Te dhenat  merren nga tabela : T_KONFIGURIMEMAIL)
    /// </summary>
    public class clsKonfigurimEmail
    {
        #region Atributet
        private static string celesi = "[ImbAlphaWeb876*]";
        private int idKonfigurimEmail;
        private string outgoingSmtp;
        private string dergoEmailNga;
        private string password;
        private int portaSmtp; //defaulti 25
        private int idNdermarrje;
        private int idPerdoruesi;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private bool enableSsl;

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos id-ne qe gjenerohet automatikisht
        /// </summary>
        public int IdKonfigurimEmail
        {
            get
            {
                return idKonfigurimEmail;
            }
            set
            {
                idKonfigurimEmail = value;
            }

        }

        /// <summary>
        /// Kthen/Vendos outgoingSmtp
        /// </summary>
        public string OutgoingSmtp
        {
            get
            {
                return outgoingSmtp;
            }
            set
            {
                outgoingSmtp = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos dergo email nga
        /// </summary>
        public string DergoEmailNga
        {
            get
            {
                return dergoEmailNga;
            }
            set
            {
                dergoEmailNga = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos passwordin
        /// </summary>
        public string Password
        {
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
        public int PortaSmtp
        {
            get
            {
                return portaSmtp;
            }
            set
            {
                portaSmtp = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos id e perdoruesit
        /// </summary>
        public int IdPerdoruesi
        {
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
        public int IdNdermarje
        {
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
        public DateTime DtKrijimi
        {
            get { return dtKrijimi; }

        }

        /// <summary>
        /// Kthen daten e modifikimit te fundit
        /// </summary>
        public DateTime DtModifikimi
        {
            get { return dtModifikimi; }
        }

        /// <summary>
        /// Kthen enable ssl
        /// </summary>
        public bool EnableSsl
        {
            get
            {
                return enableSsl;
            }
            set
            {
                enableSsl = value;
            }
        }

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Kontruktori pa parametra i klases
        /// </summary>
        public clsKonfigurimEmail()
        {
        }
        /// <summary>
        /// mbush konfigurimin e email-it sipas ndermarrjes.
        /// </summary>
        /// <param name="idNdermarrje"></param>
        public clsKonfigurimEmail(int idNdermarrje)
        {
            using (clsDatabaseAdmin dbAdm = new clsDatabaseAdmin())
            {
                if (!mbushKonfigurimEmail(dbAdm.merrKonfigurimEmailSipasIdNdermarrje(idNdermarrje)))
                    throw new MyException("Ndermarrja nuk ka konfigurim email-i!");
            }
        }

        /// <summary>
        /// Kontruktori me parametra i klases
        /// </summary>
        public clsKonfigurimEmail(int idKonfEmail, string outgoingSmtp, string dergoEmailNga, string passwordi, int portaSmtp, int idNderm, int idPerd, DateTime dtKrijimi, DateTime dtModifikimi, bool enableSsl)
        {
            this.idKonfigurimEmail = idKonfEmail;
            this.outgoingSmtp = outgoingSmtp;
            this.dergoEmailNga = dergoEmailNga;
            this.password = passwordi;
            this.portaSmtp = portaSmtp;
            this.idNdermarrje = idNderm;
            this.idPerdoruesi = idPerd;
            this.dtKrijimi = dtKrijimi;
            this.dtModifikimi = dtModifikimi;
            this.enableSsl = enableSsl;
        }

        /// <summary>
        /// Kontruktori me parametra i klases
        /// </summary>
        public clsKonfigurimEmail(int idKonfEmail, string outgoingSmtp, string dergoEmailNga, string passwordi, int portaSmtp, int idNderm, int idPerd, bool enableSsl)
        {
            this.idKonfigurimEmail = idKonfEmail;
            this.outgoingSmtp = outgoingSmtp;
            this.dergoEmailNga = dergoEmailNga;
            this.password = passwordi;
            this.portaSmtp = portaSmtp;
            this.idNdermarrje = idNderm;
            this.idPerdoruesi = idPerd;
            this.portaSmtp = portaSmtp;
            this.enableSsl = enableSsl;
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// Ruan objektin e konfigurimit te emailit ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbAdmin.clsDatabaseAdmin.ruajKonfigurimEmail"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>
        public clsMesazh ruaj()
        {
            clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin();
            int id;
            clsMesazh mesazh;
            mesazh = kontrolloDergimEmail();
            if (!mesazh)
                return mesazh;
            mesazh = dbAdmin.ruajKonfigurimEmail(out id, outgoingSmtp, dergoEmailNga, password, portaSmtp, idNdermarrje, idPerdoruesi, enableSsl);
            this.IdKonfigurimEmail = id;
            dbAdmin.Dispose();
            return mesazh;
        }

        /// <summary>
        /// Modifikon objektin e konfigurimit te emailit ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbAdmin.clsDatabaseAdmin.modifikoKonfigurimEmail"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>
        public clsMesazh modifiko()
        {
            clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin();
            clsMesazh mesazh = kontrolloDergimEmail();
            if (!mesazh)
                return mesazh;
            mesazh = dbAdmin.modifikoKonfigurimEmail(idKonfigurimEmail, outgoingSmtp, dergoEmailNga, password, portaSmtp, idNdermarrje, idPerdoruesi, enableSsl);
            dbAdmin.Dispose();
            return mesazh;
        }

        public clsMesazh kontrolloDergimEmail()
        {
            clsMesazh mesazh = clsMailSender.DergoEmailTest( DergoEmailNga, Password, PortaSmtp, OutgoingSmtp, EnableSsl, MessagesResource.Messages["EmailTest"], MessagesResource.Messages["KonfigurimISakte"]);
            return mesazh;
        }

        /// <summary>
        /// Kontrollon nese ka konfigurim emaili per ndermarrjen
        /// </summary>
        /// <returns>true nqs ekziston dhe false nqs nuk ekziston</returns>
        public static bool kaKonfigurimPerNdermarrje(int idNdermarrje)
        {
            clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin();
            bool ekziston = dbAdmin.kaKonfigurimEmailPerNdermarrjen(idNdermarrje);
            dbAdmin.Dispose();
            return ekziston;
        }

        /// <summary>
        /// mbush konfigurimin e emailit sipas ndermarrjes
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <returns></returns>
        public bool mbushKonfigurimSipasIdNdermarrje(int idNdermarrje)
        {
            clsDatabaseAdmin dbAdm = new clsDatabaseAdmin();
            bool sukses = mbushKonfigurimEmail(dbAdm.merrKonfigurimEmailSipasIdNdermarrje(idNdermarrje));
            dbAdm.Dispose();
            return sukses;
        }

        /// <summary>
        /// mbush konfigurimin e emailit sipas id se konfigurimit
        /// </summary>
        /// <param name="idKonfigurim"></param>
        /// <returns></returns>
        public bool mbushKonfigurimSipasIdKonfigurimi(int idKonfigurim)
        {
            clsDatabaseAdmin dbAdm = new clsDatabaseAdmin();
            bool sukses = mbushKonfigurimEmail(dbAdm.merrKonfigurimEmailSipasIdKonfigurimi(idKonfigurim));
            dbAdm.Dispose();
            return sukses;
        }

        /// <summary>
        /// mbush konfigurimin e emailit sipas id se konfigurimit
        /// </summary>
        /// <param name="idKonfigurim"></param>
        /// <returns></returns>
        public static int ktheIdKonfigEmailSipasIdNDerm(int idNdermarrje)
        {
            DbCore.DbAdmin.clsDatabaseAdmin dbAdm = new DbCore.DbAdmin.clsDatabaseAdmin();
            int idKonf = dbAdm.merrIdKonfigurimEmailNgaNdermarrje(idNdermarrje);
            dbAdm.Dispose();
            return idKonf;
        }
        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush konfigurimin sipas licences se perdoruesit
        /// </summary>
        /// <param name="dbDataRowKonfigurimEmail"></param>
        /// <returns>kthen true nqs mbushja kryhet me sukses, false perndryshe</returns>
        internal bool mbushKonfigurimEmail(DataRow dbDataRowKonfigurimEmail)
        {
            if (dbDataRowKonfigurimEmail != null)
            {
                try
                {
                    int.TryParse(dbDataRowKonfigurimEmail["IDKONFIGURIMEMAIL"].ToString(), out idKonfigurimEmail);
                    outgoingSmtp = dbDataRowKonfigurimEmail["OUTGOINGSMTP"].ToString();
                    dergoEmailNga = dbDataRowKonfigurimEmail["DERGOEMAILNGA"].ToString();
                    password = dbDataRowKonfigurimEmail["PASSWORD"].ToString();
                    int.TryParse(dbDataRowKonfigurimEmail["PORTASMTP"].ToString(), out portaSmtp);
                    int.TryParse(dbDataRowKonfigurimEmail["IDNDERMARJE"].ToString(), out idNdermarrje);
                    int.TryParse(dbDataRowKonfigurimEmail["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    DateTime.TryParse(dbDataRowKonfigurimEmail["DATEKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRowKonfigurimEmail["DATEMODIFIKIMI"].ToString(), out dtModifikimi);
                    bool.TryParse(dbDataRowKonfigurimEmail["ENABLESSL"].ToString(), out enableSsl);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new MyException("ERROR: Gabim casti gjate marrjes se konfigurimit nga databaza!");
                }
                catch (Exception)
                {
                    throw new MyException("ERROR: Gabim gjate marrjes se konfigurimit nga databaza!");
                }
            }
            else
                return false;
        }

        #endregion
    }
}