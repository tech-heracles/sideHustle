using System.Data;
using System;
using DbCore.DbAdmin;
using System.Text;
using System.IO;
using AlphaWeb.Core.Interfaces.Data;

namespace DbCore.DbOTC
{
    public class OTCUserMPESA : IDataBaseReader
    {

        #region atribute
        private int idPerdoruesiMPESA;
        private int idPerdoruesiAlphaWeb;
        private int idNdermarrje;
        private string userName;
        private string dyqani;
        private string emerDyqani;
        private string emer;
        private string mbiemer;
        private string adreseDyqani;
        private string emerDealer;
        #endregion atribute
        #region properties
        public int IdPerdoruesiMPESA { get { return idPerdoruesiMPESA; } set { idPerdoruesiMPESA = value; } }
        public int IdPerdoruesiAlphaWeb { get { return idPerdoruesiAlphaWeb; } set { idPerdoruesiAlphaWeb = value; } }
        public int IdNdermarrje { get { return idNdermarrje; } set { idNdermarrje = value; } }
        public string Username { get { return userName; } set { userName = value; } }
        public string Password { get; set; }
        public string Dyqani { get { return dyqani; } set { dyqani = value; } }
        public string EmerDyqani { get { return emerDyqani; } set { emerDyqani = value; } }
        public string AdreseDyqani { get { return adreseDyqani; } set { adreseDyqani = value; } }
        public string Emer { get { return emer; } set { emer = value; } }
        public string Mbiemer { get { return mbiemer; } set { mbiemer = value; } }
        public string EmerDealer { get { return emerDealer; } set { emerDealer = value; } }

        #endregion properties

        #region konstruktore
        /// <summary>
        /// Mbush objektin me perdoruesin mpesa qe i perket ketij perdoruesi ne web
        /// </summary>
        /// <param name="idPerdoruesiAlphaWeb"></param>
        public OTCUserMPESA(int idPerdoruesiMPESA)
        {
            MerrUserinMPESA(idPerdoruesiMPESA);
        }

        public OTCUserMPESA(string username)
        {
            MerrUserMPESASipasUserName(username);
        }

        public OTCUserMPESA() { }
        #endregion

        public void MerrUserMPESASipasUserName(string username)
        {
            using (clsDatabaseOTC db = new clsDatabaseOTC())
                db.MerrPerdoruesinMPESA(username, this);
        }

        internal string GetPasswordEncrypted()
        {
            byte[] fileBytes = File.ReadAllBytes($"{System.Web.HttpRuntime.AppDomainAppPath}\\OTC\\key.3des.dat");
            return TripleDes.Encrypt(Password, fileBytes);
        }
        internal static string GetPasswordDecrypted(string pass)
        {
            byte[] fileBytes = File.ReadAllBytes($"{System.Web.HttpRuntime.AppDomainAppPath}\\OTC\\key.3des.dat");
            return TripleDes.Decrypt(pass, fileBytes);
        }
        public clsMesazh Ruaj()
        {
            using (var myScope = new MyTransactionScope())
            {
                using (clsDatabaseOTC db = new clsDatabaseOTC())
                {
                    int id = db.RuajUserMPESA(idPerdoruesiAlphaWeb, idNdermarrje, userName, dyqani, emerDyqani, emer, mbiemer, adreseDyqani, emerDealer, GetPasswordEncrypted());
                    if (id == 0) return new clsMesazh(false, "Perdoruesi nuk u ruajt me sukses!");
                    idPerdoruesiMPESA = id;
                    myScope.Complete();
                    return new clsMesazh(true, "Perdoruesi u ruajt me sukses!");
                }

            }
        }

        public bool KaNdryshuarNdonjeFushe(OTCUserMPESA user)
        {
            return user.AdreseDyqani != AdreseDyqani
                || user.Dyqani != Dyqani
                || user.Emer != Emer
                || user.Mbiemer != Mbiemer
                || user.EmerDealer != EmerDealer
                || user.EmerDyqani != EmerDyqani
                || user.GetPasswordEncrypted() != GetPasswordEncrypted();
        }

        public clsMesazh Modifiko()
        {
            using (var myScope = new MyTransactionScope())
            {
                clsMesazh mesazh;
                using (clsDatabaseOTC db = new clsDatabaseOTC())
                {
                    mesazh = db.ModifikoUserMPESA(idPerdoruesiMPESA, idPerdoruesiAlphaWeb, idNdermarrje, userName, dyqani, emerDyqani, emer, mbiemer, adreseDyqani, emerDealer, GetPasswordEncrypted());
                    if (mesazh) myScope.Complete();
                    return mesazh;
                }

            }

        }

        public void MerrUserinMPESANgaUserAlphaWeb(int idPerdoruesiAlphaWeb)
        {
            using (clsDatabaseOTC db = new clsDatabaseOTC())
                db.MerrPerdoruesinMPESASipasUserAlphaWeb(idPerdoruesiAlphaWeb, this);
        }
        public void MerrUserinMPESA(int idPerdoruesiMPESA)
        {
            using (clsDatabaseOTC db = new clsDatabaseOTC())
                db.MerrPerdoruesinMPESA(idPerdoruesiMPESA, this);
        }
        public void Mbush(IDataRecord record)
        {

            int.TryParse(record["IDNDERMARRJE"]?.ToString(), out idNdermarrje);
            int.TryParse(record["IDPERDORUESIMPESA"]?.ToString(), out idPerdoruesiMPESA);
            int.TryParse(record["IDPERDORUESIALPHAWEB"]?.ToString(), out idPerdoruesiAlphaWeb);
            userName = record["USERNAME"]?.ToString();
            dyqani = record["DYQANI"]?.ToString();
            emerDyqani = record["EMERDYQANI"]?.ToString();
            adreseDyqani = record["ADRESEDYQANI"]?.ToString();
            emer = record["EMER"]?.ToString();
            mbiemer = record["MBIEMER"]?.ToString();
            emerDealer = record["EMERDEALER"]?.ToString();
            //perdorim versionin e paenkriptuar per ta patur njesoj gjithandej
            var encryptedPass = record["PASSWORD"]?.ToString();
            if (!string.IsNullOrWhiteSpace(encryptedPass))
                Password = GetPasswordDecrypted(encryptedPass);
            else Password = "";
        }
        
    }
}
